Namespace Http
    Namespace Type1
        ''' <summary>
        ''' An http server with an async I/O model implemented via IAsyncResult (.NET 2.0+). Utilizes the event-based asynchronous pattern (EAP) and the asynchronous programming model (APM) pattern.
        ''' </summary>
        ''' <remarks></remarks>
        Public Class Server
            Public SendBufferSize As Integer
            Public ReceiveBufferSize As Integer
            'Public BufferSize As Integer
            Private _handlers As New Handlers
            Private _connections As Integer
            Public Sites As New Hashtable
            Public MimeTypes As New Hashtable
            Public OutputCache As New Concurrent.ConcurrentDictionary(Of String, Response)
            Public RequestCache As New Concurrent.ConcurrentDictionary(Of String, Request)
            Public DefaultDocuments As New ArrayList
            Public ResponseHeaders As New ArrayList
            Public ConnectedClients As Integer
            Public ConnectedA As Integer
            Public DisconnectedA As Integer
            Public DisconnectedB As Integer
            Public DisconnectedC As Integer

            ' the server should function out-of-box by handling its own events internally, but the events can also be overridden during implementation for custom handling
            Event SiteStarted()
            Event ServerStarted()
            Event ServerShutdown()
            Event HandleRequest(ByVal req As Request, ByVal client As Net.Sockets.Socket)
            Event ProxyRequest(req As Request, server_address As String, client As Net.Sockets.Socket)
            Event ClientConnecting(ByVal req As Request, ByVal socket As Net.Sockets.Socket, ByVal head As String)
            Event ClientConnected(ByVal argClientSocket As Net.Sockets.Socket)
            Event ClientDisconnected(ByVal argClientSocket As Net.Sockets.Socket)
            Event LogMessage(ByVal message As String)

            Public EnableOutputCache As Boolean
            Public EnableDirectoryListing As Boolean

            Dim WithEvents Proxy1 As New Client(False)

            ''' <summary>
            ''' Constructs a new HTTP server using the config file.
            ''' </summary>
            ''' <remarks></remarks>
            Sub New()
                ' we need to load the config once so Form_Load() can populate the form
                ' TODO: we need to unload and reload the config when the server is stopped and restarted via the form
                LoadConfig()
            End Sub

            ''' <summary>
            ''' Loads the server config file http.xml from disk and configures the server to operate as defined by the config.
            ''' </summary>
            ''' <remarks></remarks>
            Sub LoadConfig()
                ' TODO: Xml functions are very picky after load, if we try to access a key that doesn't exist it will throw a 
                '   vague error that does not stop the debugger on the error line, and the innerexception states 'object reference 
                '   not set to an instance of an object'. a custom function GetValue() helps avoid nulls but not this. default values should
                '   be assumed by the server for cases when the value can't be loaded from the config, or the server should regenerate the config 
                '   per its known format and then load it.
                If IO.File.Exists("http.xml") = False Then CreateConfig()
                Dim cfg As New Xml.XmlDocument
                Try
                    cfg.Load("http.xml")
                Catch ex As Exception
                    DebugMessage("Could not parse config file - malformed xml detected.", DebugMessageType.ErrorMessage, "LoadConfig", ex.Message)
                    ' TODO: we need to notify the user that the config couldn't be loaded instead of just dying...
                    Exit Sub
                End Try
                Dim root As Xml.XmlNode = cfg("Settings")
                ' parse the sites (aka virtual hosts):
                For Each n As Xml.XmlNode In root("Sites")
                    Dim s As New Site
                    s.Title = n("Title").GetValue
                    s.Path = n("Path").GetValue
                    s.Host = n("Host").GetValue
                    s.Port = n("Port").GetValue
                    s.RootPath = s.Path ' TODO: convert relpath to abspath
                    s.RootUrl = "http://" & s.Host & ":" & s.Port
                    s.Upstream = n("Upstream").GetValue
                    If s.Upstream <> "" Then
                        s.Role = "Load Balancer"
                    Else
                        s.Role = "Standard"
                    End If
                    Me.Sites.Add(s.Title, s)
                Next
                ' parse the basic options:
                Me.SendBufferSize = root("Options")("SendBufferSize").GetValue
                Me.ReceiveBufferSize = root("Options")("ReceiveBufferSize").GetValue
                Me.EnableDirectoryListing = CBool(root("Options")("DirectoryListing").Attributes("Enabled").InnerText) = True
                Me.EnableOutputCache = CBool(root("Options")("OutputCache").Attributes("Enabled").InnerText) = True
                ' parse the MIME types, letting us know what compression and expiration settings to use when serving them to clients:
                For Each n As Xml.XmlNode In root("MimeTypes")
                    Dim fileExtensions() As String = n.Attributes("FileExtension").GetValue.Split(CChar(","))
                    For Each ext As String In fileExtensions
                        Dim m As New MimeType
                        m.Name = n.GetValue
                        m.FileExtension = ext
                        m.Compress = CType([Enum].Parse(GetType(CompressionMethod), n.Attributes("Compress").GetValue, True), CompressionMethod)
                        m.Expires = n.Attributes("Expires").GetValue
                        m.Handler = n.Attributes("Handler").GetValue
                        MimeTypes.Add(m.FileExtension, m)
                    Next
                Next
                ' parse the default documents, which are used when the request uri is a directory instead of a document:
                For Each n As Xml.XmlNode In root("DefaultDocuments")
                    DefaultDocuments.Add(n.InnerText)
                Next
                ' parse the response headers, which let us include certain headers in the http response by default:
                If CType(root("ResponseHeaders").Attributes("Enabled").InnerText, Boolean) = True Then
                    For Each n As Xml.XmlNode In root("ResponseHeaders")
                        ResponseHeaders.Add(n.InnerText)
                    Next
                End If
                ' parse the handlers, which let us use external programs and api calls, such as a php script parser or ldap query:
                For Each n As Xml.XmlNode In root("Handlers")
                    If CBool(n.Attributes("Enabled").InnerText) = True Then
                        Dim handlerName As String = n("Name").InnerText
                        Dim handlerPath As String = n("ExecutablePath").InnerText
                        ' parse the handler name and create a matching handler object if one exists
                        If handlerName = "PhpCgi" Then
                            Dim h As New PhpCgiHandler
                            h.Name = handlerName
                            h.ExecutablePath = handlerPath
                            _handlers.Add(h)
                        End If
                    End If
                Next
            End Sub

            Sub CreateConfig()
                Dim f As New IO.StreamWriter("http.xml")
                Dim str As String = _
<![CDATA[<?xml version="1.0" encoding="utf-8" ?>
<Settings>

<!-- any node with Enabled="False" should be ignored/not loaded/not supported by the server instance -->
<!-- any node group with only one type of node may include a <Default> node to imply how the server should behave for unhandled cases -->

<Sites>
	<Site>
		<Title>site1</Title> <!-- a simple title for identifying the site in the UI and logs (e.g. My First Site) -->
		<Path>c:\site1</Path> <!-- relative or absolute physical path to the site's web directory (e.g. c:\myweb1) -->
		<Host>127.0.0.1</Host> <!-- ip address or dns domain name the server will utilize (e.g. localhost / 127.0.0.1) -->
		<Port>9999</Port> <!-- port on which the server will listen for incoming connections -->
	</Site>
</Sites>

<Options>
	<SendBufferSize>4096</SendBufferSize> <!--  -->
	<ReceiveBufferSize>4096</ReceiveBufferSize>
	<KeepAlive Enabled="True">
		<MaxRequests>10000</MaxRequests> <!-- limits how many requests can be made by the keep-alive connection before that connection is forced closed -->
		<Timeout>10</Timeout> <!-- limits how long, in seconds, that a keep-alive connection can remain open before that connection is forced closed -->
	</KeepAlive>
	<OutputCache Enabled="True">
		<FileQuota>20</FileQuota> <!-- limits how many files can be in the cache -->
		<SizeQuota>50</SizeQuota> <!-- limits ram usage (in MB) -->
	</OutputCache>
	<Gzip Enabled="True"> <!-- GzipStream is internal to the .NET framework; module does not rely on an external module -->
		<MinimumFileSize>600</MinimumFileSize> <!-- prevents compressing files smaller than this (in Bytes); compressing files smaller than 150 Bytes can increase their size and compressing many smaller files during a request can tax the cpu. -->
	</Gzip>
	<Deflate Enabled="True"> <!-- DeflateStream is internal to the .NET framework; module does not rely on an external module -->
		<MinimumFileSize>600</MinimumFileSize> <!-- prevents compressing files smaller than this (in Bytes); compressing files smaller than 150 Bytes can increase their size and compressing many smaller files during a request can tax the cpu. -->
	</Deflate>
    <DirectoryListing Enabled="True">
    </DirectoryListing>
</Options>

<MimeTypes>
	<Default FileExtension="" Compress="none" Expires="access plus 1 month" Handler="">text/plain</Default>
	<MimeType FileExtension="js" Compress="gzip" Expires="access plus 1 year">application/javascript</MimeType>
	<MimeType FileExtension="css" Compress="gzip" Expires="access plus 1 year">text/css</MimeType>
	<MimeType FileExtension="txt" Compress="gzip" Expires="access plus 1 year">text/plain</MimeType>
	<MimeType FileExtension="htm,html" Compress="none" Expires="access plus 1 year">text/html</MimeType>
	<MimeType FileExtension="php" Compress="none" Expires="access plus 1 year" Handler="PhpCgi">text/html</MimeType>
	<MimeType FileExtension="json" Compress="gzip" Expires="access plus 1 year">application/json</MimeType>
	<MimeType FileExtension="jpg,jpeg" Compress="none" Expires="access plus 1 year">image/jpeg</MimeType>
	<MimeType FileExtension="png" Compress="none" Expires="access plus 1 year">image/png</MimeType>
	<MimeType FileExtension="svg" Compress="none" Expires="access plus 1 year">image/svg+xml</MimeType>
	<MimeType FileExtension="gif" Compress="none" Expires="access plus 1 year">image/gif</MimeType>
</MimeTypes>

<Handlers>
	<Handler Enabled="True">
		<Name>PhpCgi</Name>
		<ExecutablePath>c:\php\php-cgi.exe</ExecutablePath>
	</Handler>
	<Handler Enabled="True">
		<Name>PhpFastCgi</Name>
		<ExecutablePath>c:\php\php-cgi.exe</ExecutablePath>
	</Handler>
</Handlers>

<DefaultDocuments>
	<Document>index.php</Document>
	<Document>index.htm</Document>
	<Document>index.html</Document>
	<Document>default.htm</Document>
</DefaultDocuments>

<ResponseHeaders Enabled="True">
	<Header>Server: Rapid-Server</Header>
	<Header>X-Powered-By: Rapid-Server</Header>
</ResponseHeaders>

</Settings>]]>.Value

                f.Write(str)
                f.Close()
                f.Dispose()
                LoadConfig()
            End Sub

            ''' <summary>
            ''' Matches the requested file to a MimeType based on the FileType (extension). MimeTypes are defined in the server config file and include attributes that determine how the server will handle compression and expiration for the resource.
            ''' </summary>
            ''' <param name="path"></param>
            ''' <returns></returns>
            ''' <remarks></remarks>
            Function GetContentType(ByVal path As String) As String
                Dim ext As String = IO.Path.GetExtension(path).TrimStart(CChar("."))
                Dim m As MimeType = CType(MimeTypes(ext), MimeType)
                Dim contentType As String
                If m IsNot Nothing Then
                    contentType = m.Name
                Else
                    contentType = "text/plain"
                    ' TODO: implement the default MimeType from the config file...
                    '   e.g. contentType = MimeTypes.Default.Name
                End If
                Return contentType
            End Function

            ''' <summary>
            ''' Starts the server, allowing clients to connect and make requests to one or more of the sites specified in the config.
            ''' </summary>
            ''' <remarks></remarks>
            Sub StartServer()
                'LoadConfig()
                ' bind each site to it's address and start listening for client connections
                For Each s As Site In Me.Sites.Values
                    Try
                        Dim ep As Net.IPEndPoint = AddressToEndpoint(s.Host, s.Port)
                        s.Socket = New System.Net.Sockets.Socket(Net.Sockets.AddressFamily.InterNetwork, Net.Sockets.SocketType.Stream, Net.Sockets.ProtocolType.Tcp)
                        s.Socket.Bind(ep)
                        s.Socket.Listen(20000)
                        s.Socket.BeginAccept(0, New AsyncCallback(AddressOf ClientConnectedAsync), s)
                        DebugMessage("Site started...", DebugMessageType.InfoMessage, "StartServer")
                        RaiseEvent SiteStarted()
                    Catch ex As System.Net.Sockets.SocketException
                        DebugMessage("Could not start the site '" & s.Title & "'. Make sure it's address (" & s.Host & ":" & s.Port & ") is not already in use.", DebugMessageType.ErrorMessage, "StartServer", ex.Message)
                    Catch ex2 As Exception
                        DebugMessage("Unhandled exception in StartServer: " & ex2.Message, DebugMessageType.ErrorMessage, "StartServer", ex2.Message)
                    End Try
                Next
                RaiseEvent ServerStarted()
            End Sub

            ''' <summary>
            ''' Stops the server, shutting down each Site.
            ''' </summary>
            ''' <remarks></remarks>
            Sub StopServer()
                ' shutdown each site:
                ' TODO: this throws a bunch of exceptions. To test, just Start then Stop.
                For Each s As Site In Me.Sites.Values
                    Try
                        ' try to safely shutdown first, allowing pending transmissions to finish which helps prevent prevent data loss
                        s.Socket.Shutdown(Net.Sockets.SocketShutdown.Both)
                    Catch ex As Exception
                        DebugMessage("The site '" & s.Title & "' failed to shut down gracefully and will now be terminated.", DebugMessageType.WarningMessage, "StopServer", ex.Message)
                    End Try
                    ' kill the Site socket, terminating any clients abrubtly (data loss may occur if Shutdown is unsuccessful)
                    s.Socket.Close()
                    s.Socket.Disconnect(False)
                Next
                RaiseEvent ServerShutdown()
            End Sub

            ''' <summary>
            ''' Accepts the client that is attempting to connect, allows another client to connect (whenever that may be), and finally begins receiving data from the client that just connected.
            ''' </summary>
            ''' <param name="ar"></param>
            ''' <remarks></remarks>
            Sub ClientConnectedAsync(ByVal ar As IAsyncResult)
                ' get the async state object from the async BeginAccept method, which contains the server's listening socket
                Dim s As Site = CType(ar.AsyncState, Site)
                ' try to accept the client connection
                Dim clientSocket As System.Net.Sockets.Socket = Nothing
                Try
                    ' accept the client connection, giving us the client socket to work with:
                    clientSocket = s.Socket.EndAccept(ar)
                    ' update the connected client count in a thread safe way
                    Threading.Interlocked.Increment(Me.ConnectedClients)
                    'RaiseEvent ClientConnected(clientSocket)
                    'DebugMessage("Client connected. Total connections: " & _connections, DebugMessageType.UsageMessage, "ClientConnectedAsync")
                    ' begin accepting another client connection:
                    s.Socket.BeginAccept(0, New AsyncCallback(AddressOf ClientConnectedAsync), s)
                Catch ex1 As ObjectDisposedException
                    ' if we get an ObjectDisposedException it that means the server socket terminated while this async method was still active
                    DebugMessage("The server was closed before the async method could complete.", DebugMessageType.WarningMessage, "ClientConnectedAsync", ex1.Message)
                    Exit Sub
                Catch ex2 As Exception
                    DebugMessage("An unhandled exception occurred in ClientConnectedAsync.", DebugMessageType.ErrorMessage, "ClientConnectedAsync", ex2.Message)
                    Exit Sub
                End Try

                ' begin receiving data (http requests) from the client socket:
                Dim asyncState As New AsyncReceiveState(Me.ReceiveBufferSize, Nothing)
                asyncState.Site = s
                asyncState.Socket = clientSocket
                asyncState.Socket.BeginReceive(asyncState.Buffer, 0, Me.ReceiveBufferSize, Net.Sockets.SocketFlags.None, New AsyncCallback(AddressOf RequestReceivedAsync), asyncState)
            End Sub

            ''' <summary>
            ''' Accepts the client request, builds the request/response objects and passes them to the event handler where the response 
            ''' object will be finalized and sent back to the client.
            ''' </summary>
            ''' <param name="ar"></param>
            ''' <remarks></remarks>
            Sub RequestReceivedAsync(ByVal ar As IAsyncResult)
                ' get the async state object:
                Dim asyncState As AsyncReceiveState = CType(ar.AsyncState, AsyncReceiveState)
                ' check the socket for data:
                Dim numBytesReceived As Integer
                Try
                    ' call EndReceive which will give us the number of bytes received
                    numBytesReceived = asyncState.Socket.EndReceive(ar)
                Catch ex As Net.Sockets.SocketException
                    ' if we get a ConnectionReset exception, it could indicate that the client has disconnected
                    If ex.SocketErrorCode = Net.Sockets.SocketError.ConnectionReset Then
                        DebugMessage("EndReceive on the client socket failed because the client has disconnected.", DebugMessageType.UsageMessage, "RequestReceivedAsync", ex.Message)
                        ' update the connected client count in a thread safe way
                        Threading.Interlocked.Decrement(Me.ConnectedClients)
                        'RaiseEvent ClientDisconnected(asyncState.Socket)
                        Exit Sub
                    End If
                End Try
                ' if we get numBytesReceived equal to zero, it could indicate that the client has disconnected
                ' TODO: does this actually disconnect the client though, or just assume it was?
                If numBytesReceived = 0 Then
                    ' update the connected client count in a thread safe way
                    Threading.Interlocked.Decrement(Me.ConnectedClients)
                    'RaiseEvent ClientDisconnected(asyncState.Socket)
                    Exit Sub
                End If

                ' if we've reached this point, we were able to parse the IAsyncResult which contains our raw request bytes, so proceed 
                '   to handle it on a separate ThreadPool thread; it is important that we free up the I/O completion port being 
                '   used for this async operation as soon as possible, therefore we don't even attempt to parse the requestBytes at 
                '   this point and just immediately pass the raw request bytes to a ThreadPool thread for further processing
                Threading.ThreadPool.QueueUserWorkItem(AddressOf HandleRequestAsync, asyncState)
            End Sub

            ' handles the request on a separate ThreadPool thread.
            Sub HandleRequestAsync(ByVal asyncState As AsyncReceiveState)
                ' TODO: first try pull the request from the request cache, otherwise parse it now
                ' convert the raw request bytes/string into an HttpRequest object for ease of use
                Dim reqString As String = System.Text.Encoding.ASCII.GetString(asyncState.Buffer).Trim(Chr(0))
                Dim req As Request

                ' pull the request object from the request cache, or create it now
                If RequestCache.ContainsKey(reqString) Then
                    req = RequestCache(reqString)
                Else
                    req = New Request(reqString, Me, asyncState.Socket, asyncState.Site)
                    RequestCache.TryAdd(reqString, req)
                End If

                ' first try to serve the response from the output cache
                Dim servedFromCache As Boolean = False
                Dim cacheAllowed As Boolean = True
                If Me.EnableOutputCache = True Then
                    If req.CacheAllowed = True Then
                        If OutputCache.ContainsKey(req.AbsPath & req.QueryString) = True Then
                            Dim res As Response = CType(OutputCache(req.AbsPath & req.QueryString), Response)
                            SendResponse(req, res, asyncState.Socket)
                            servedFromCache = True
                            DebugMessage("Serving resource from cache: " & req.AbsPath & ".", DebugMessageType.UsageMessage, "HandleRequestAsync")
                        End If
                    End If
                End If

                ' response couldn't be served from cache, handle the request according to the site role
                If servedFromCache = False Then
                    ' if the current site is a reverse proxy/load balancer with upstream servers defined, forward the request to another server,
                    '   otherwise handle the request by this server like normal
                    If asyncState.Site.Role = "Standard" Then
                        ' raise an event to handle the request/response cycle (this can be overridden during implementation to allow for custom handling)
                        RaiseEvent HandleRequest(req, asyncState.Socket)
                    ElseIf asyncState.Site.Role = "Load Balancer" Then
                        ' parse the upstream servers and select one using the defined algorithm
                        Dim upstreams() As String = asyncState.Site.Upstream.Split(",")
                        Dim r As New Random
                        Dim i As Integer = r.Next(0, upstreams.Length)
                        ' forward the request to the selected upstream server
                        RaiseEvent ProxyRequest(req, upstreams(i), asyncState.Socket)
                    End If
                End If
            End Sub

            ' makes a GET request to the upstream server on behalf of the client
            Private Sub HttpServer_ProxyRequest(req As Request, server_address As String, client As Net.Sockets.Socket) Handles Me.ProxyRequest
                Dim newUri As String = server_address & req.Uri
                Dim ps As New ProxyState
                ps.client = client
                ps.req = req
                Proxy1.Go(newUri, ps)
            End Sub

            ' sends the response from the upstream server back to the original client which made the request
            Private Sub Proxy1_HandleResponse(responseString As String, state As Object) Handles Proxy1.HandleResponse
                Dim ps As ProxyState = state
                Dim req As Request = ps.req
                Dim client As Net.Sockets.Socket = ps.client
                Dim res As New Response(Me, ps.req, ps.client)
                res.ResponseBytes = Text.Encoding.ASCII.GetBytes(responseString)
                TryCache(req, res)
                SendResponse(req, res, client)
            End Sub

            ' try to cache the response, if it hasn't already been
            Sub TryCache(req As Request, res As Response)
                If Me.EnableOutputCache = True And req.CacheAllowed = True And req.Method <> "POST" Then
                    OutputCache.TryAdd(req.AbsPath & req.QueryString, res)
                End If
            End Sub

            ''' <summary>
            ''' Handles the client request, implements the full request/response cycle. This event is triggered by HandleRequestAsync.
            ''' Can be listened to in a windows form for custom req/res inspection and handling, similar to node.
            ''' </summary>
            ''' <param name="req"></param>
            ''' <param name="client"></param>
            ''' <remarks></remarks>
            Private Sub HttpServer_HandleRequest(ByVal req As Request, ByVal client As Net.Sockets.Socket) Handles Me.HandleRequest
                Dim res As New Response(Me, req, client)

                ' if the Uri is missing a trailing slash, 301 redirect to the correct Uri
                If req.FixPath301 = True Then
                    res.Headers.Remove("Connection") ' don't keep-alive for a 404
                    res.Headers.Add("Location", req.Uri & "/")
                    res.StatusCode = "301"
                End If

                ' serve the static or dynamic file from disk:
                If req.FixPath301 = False Then
                    If IO.File.Exists(req.AbsPath) = True Then
                        ' handle the request using an appropriate handler:
                        If req.MimeType.Handler = "PhpCgi" Then
                            Dim data As String = _handlers("PhpCgi").HandleRequest(req)
                            ' php returned a response, parse it and continue building the final response
                            res.Parse(data)
                            res.SetContent(res.Content)
                            If res.Headers.ContainsKey("Status") Then
                                ' parse the response code from the Status header:
                                res.StatusCode = res.Headers("Status").split(" ")(0)
                            Else
                                ' status code not found in the response headers, just use 200 OK:
                                ' TODO: something tells me this is not obeying RFC protocol, look into it...
                                res.StatusCode = "200"
                            End If
                        Else
                            ' custome handler not found, serve as static file:
                            res.ContentType = req.MimeType.Name
                            res.SetContent(IO.File.ReadAllBytes(req.AbsPath))
                            res.StatusCode = 200
                        End If

                        ' cache the response for this request:
                        ' TODO: ab -n 100000 -c 1000 breaks this if socket reuse = True in SendAsync()
                        ' TODO: caching a POST request (such as WordPress login) breaks stuff, so we should only do it when necessary instead of always...
                        '   https://stackoverflow.com/questions/626057/is-it-possible-to-cache-post-methods-in-http
                        TryCache(req, res)
                    Else
                        ' file not found, return directory list or 404:
                        If Me.EnableDirectoryListing = True And IO.Directory.Exists(req.AbsPath) Then
                            Dim listing As String = BuildDirectoryListing(req)
                            res.SetContent(Text.Encoding.ASCII.GetBytes(listing))
                            res.Headers.Remove("Connection") ' don't keep-alive for a 404
                            res.StatusCode = 200
                        Else
                            res.SetContent(Text.Encoding.ASCII.GetBytes("FAIL WHALE!"))
                            res.Headers.Remove("Connection") ' don't keep-alive for a 404
                            res.StatusCode = 404
                        End If
                    End If
                End If

                ' the response has been finalized, send it to the client
                res.ResponseBytes = res.BuildResponseBytes
                SendResponse(req, res, client)
            End Sub

            ' builds a directory listing using html for basic navigation
            Function BuildDirectoryListing(req As Request)
                Dim listing As String = "<h1>Directory Listing</h1>"
                For Each d As IO.DirectoryInfo In New IO.DirectoryInfo(req.AbsPath).GetDirectories
                    listing &= "<div><a href='" & req.Uri.TrimEnd("/") & "/" & d.Name & "'>" & d.Name & "</a></div>"
                Next
                For Each f As IO.FileInfo In New IO.DirectoryInfo(req.AbsPath).GetFiles
                    'Dim ms As New IO.MemoryStream
                    'Dim ico As System.Drawing.Icon
                    'ico = System.Drawing.Icon.ExtractAssociatedIcon(f.FullName)
                    'ico.ToBitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Png)
                    'listing &= "<div>" & "<image style='height:16px' src='data:image/png;base64," & Convert.ToBase64String(ms.ToArray) & "' /><a href='" & f.Name & "'>" & f.Name & "</a></div>"
                    listing &= "<div>" & "<a href='" & f.Name & "'>" & f.Name & "</a></div>"
                Next
                Return listing
            End Function

            ' sends the http response to the client
            Sub SendResponse(req As Request, res As Response, client As Net.Sockets.Socket)
                ' convert the response into bytes and package it in an async object for callback purposes:
                'Dim responseBytes() As Byte = res.BuildResponseBytes
                Dim sendState As New AsyncSendState(client, Me.SendBufferSize, Nothing)
                sendState.BytesToSend = res.ResponseBytes
                sendState.Tag = req.AbsPath

                ' start sending the response to the client in an async fashion:
                Try
                    client.BeginSend(res.ResponseBytes, 0, res.ResponseBytes.Length, Net.Sockets.SocketFlags.None, AddressOf SendResponseAsync, sendState)
                Catch ex As Exception
                    DebugMessage("Unhandled exception in SendResponse when trying to send data to the client.", DebugMessageType.UsageMessage, "SendResponse", ex.Message)
                End Try

                ' determine whether or not to continue receiving more data from the client:
                ' TODO: tidy this up a bit instead of setting a property to true then checking it right afterwards...
                ' IMPORTANT: for keep-alive connections we should make a final receive call to the client and if the client does not send a Connection: keep-alive header then we know to disconnect
                If res.Headers.ContainsKey("Connection") = True Then
                    If res.Headers("Connection").ToString.ToLower = "keep-alive" Then
                        sendState.Persistent = True
                    End If
                End If

                ' start receiving more data from the client in an async fashion
                If sendState.Persistent = True Then
                    Dim receiveState As New AsyncReceiveState(Me.ReceiveBufferSize, Nothing)
                    receiveState.Site = req.Site
                    receiveState.Socket = client
                    Try
                        receiveState.Socket.BeginReceive(receiveState.Buffer, 0, ReceiveBufferSize, Net.Sockets.SocketFlags.None, New AsyncCallback(AddressOf RequestReceivedAsync), receiveState)
                    Catch ex As Exception
                        DebugMessage("SendResponse encountered an exception when trying to BeginReceive on the client socket. " & ex.Message, DebugMessageType.ErrorMessage, "ClientRequest", ex.Message)
                    End Try
                End If
            End Sub

            ''' <summary>
            ''' Sends the http response to the client and closes the connection using a separate thread.
            ''' </summary>
            ''' <param name="ar"></param>
            ''' <remarks></remarks>
            Sub SendResponseAsync(ByVal ar As IAsyncResult)
                Dim asyncState As AsyncSendState = CType(ar.AsyncState, AsyncSendState)
                Try
                    asyncState.Socket.EndSend(ar)
                    ' disconnect the client if not keep-alive:
                    If asyncState.Persistent = False Then
                        'asyncState.Socket.Disconnect(True)
                    End If
                    DebugMessage("Sent " & asyncState.Tag & " to " & asyncState.Socket.RemoteEndPoint.ToString & ".", DebugMessageType.UsageMessage, "SendAsync")
                Catch ex As Exception
                    ' UNDONE: this DebugMessage() didn't used to trigger errors, but now it does...
                    'DebugMessage("Failed to send " & asyncState.Tag & " to " & asyncState.Socket.RemoteEndPoint.ToString & ". The exception was: " & ex.Message, DebugMessageType.UsageMessage, "SendAsync")
                    DebugMessage("SendResponseAsync encountered an exception when trying to send data to the client.", DebugMessageType.ErrorMessage, "SendResponseAsync", ex.Message)
                End Try
                ' finally terminate the socket after allowing pending transmissions to complete. this eliminates ERR_CONNECTION_RESET that would happen occasionally on random resources:
                If asyncState.Persistent = False Then
                    ' TODO: we check Persistent attribute twice in this method...if we put Close below Disconnect it will throw the exception that we can't access the socket because it is disposed
                    'asyncState.Socket.Close()
                    ' update the connected client count in a thread safe way
                    'Threading.Interlocked.Decrement(Me.ConnectedClients)
                    'RaiseEvent ClientDisconnected(asyncState.Socket)
                End If
            End Sub
        End Class

        Class ProxyState
            Public req As Request
            Public client As Net.Sockets.Socket
        End Class
    End Namespace
End Namespace
