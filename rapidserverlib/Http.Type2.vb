Namespace Http
    Namespace Type2
        ''' <summary>
        ''' An http server with an async I/O model implemented via SocketAsyncEventArgs (.NET 3.5+). Utilizes a special async model designed for the Socket class which consists of a shared buffer and pre-allocated object pool for async state objects to avoid object instantiation and memory thrashing/fragmentation during every http request.
        ''' The MSDN code example for this pattern is very poor. The issues (and solution) are explained in the Background section in this tutorial: http://www.codeproject.com/Articles/83102/C-SocketAsyncEventArgs-High-Performance-Socket-Cod?fid=1573061
        ''' </summary>
        ''' <remarks></remarks>
        Public Class Server
            Private _serverSocket As Net.Sockets.Socket
            Private _interops As New Interops
            Private _numConnections As Integer
            Private _maxConnections As Integer
            Private _bufferManager As BufferManager
            Private _readWritePool As SocketAsyncEventArgsPool
            Private _totalBytesRead As Integer
            Private _maxNumberAcceptedClients As Threading.Semaphore
            Public MimeTypes As New Hashtable
            Public OutputCache As New Hashtable
            Public WebRoot As String
            Public DefaultDocuments As New ArrayList
            Public ResponseHeaders As New ArrayList
            ' events - the server should function out-of-box by calling its own events, but these events can also be overridden during implementation for custom handling if desired.
            Event ServerStarted()
            Event ServerShutdown()
            Event HandleRequest(ByVal req As Request, ByVal res As Response, ByVal client As Net.Sockets.SocketAsyncEventArgs)
            Event ClientConnecting(ByVal req As Request, ByVal socket As Net.Sockets.Socket, ByVal head As String)
            Event ClientConnected(ByVal argClientSocket As Net.Sockets.Socket)
            Event ClientDisconnected(ByVal argClientSocket As Net.Sockets.Socket)

            ''' <summary>
            ''' Constructs a new HTTP server given a desired web root path.
            ''' </summary>
            ''' <param name="rootPath"></param>
            ''' <remarks></remarks>
            Sub New(ByVal rootPath As String)
                WebRoot = rootPath
                LoadConfig()
                Const maxConnections As Integer = 10000      ' pull this from the config, but for now we just make them usable
                Const receiveBufferSize As Integer = 4096   ' pull this from the config, but for now we just make them usable
                _maxConnections = maxConnections
                _bufferManager = New BufferManager(receiveBufferSize * _maxConnections * 2, receiveBufferSize)
                _readWritePool = New SocketAsyncEventArgsPool(_maxConnections)
                _maxNumberAcceptedClients = New Threading.Semaphore(_maxConnections, _maxConnections)
            End Sub

            ''' <summary>
            ''' Loads the server config file http.xml from disk and configures the server to operate as defined by the config.
            ''' </summary>
            ''' <remarks></remarks>
            Sub LoadConfig()
                Dim cfg As New Xml.XmlDocument
                Try
                    cfg.Load("http.xml")
                Catch ex As Exception
                    DebugMessage("Could not parse config file - malformed xml detected.", DebugMessageType.ErrorMessage, "LoadConfig", ex.Message)
                    ' TODO: should we terminate the server now, or allow it to startup without a config file?
                    Exit Sub
                End Try
                Dim root As Xml.XmlNode = cfg("Settings")
                ' parse the MIME types, letting us know what compression and expiration settings to use when serving them to clients:
                For Each n As Xml.XmlNode In root("MimeTypes")
                    Dim fileExtensions() As String = n.Attributes("FileExtension").GetValue.Split(",")
                    For Each ext As String In fileExtensions
                        Dim m As New MimeType
                        m.Name = n.GetValue
                        m.FileExtension = ext
                        m.Compress = [Enum].Parse(GetType(CompressionMethod), n.Attributes("Compress").GetValue, True)
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
                If root("ResponseHeaders").Attributes("Enabled").InnerText = True Then
                    For Each n As Xml.XmlNode In root("ResponseHeaders")
                        ResponseHeaders.Add(n.InnerText)
                    Next
                End If
                ' parse the virtual hosts, which let us define more than one functional site from various paths on disk:
                For Each n As Xml.XmlNode In root("VirtualHosts")
                    ' TODO: implement support for virtual hosts here...
                Next
                '' parse the interops, which let us use external programs and api calls, such as a php script parser or ldap query:
                'For Each n As Xml.XmlNode In root("Interops")
                '    If n("Enabled").InnerText = True Then
                '        Dim i As New Interop
                '        i.Name = n("Name").InnerText
                '        i.ExecutablePath = n("ExecutablePath").InnerText
                '        i.MaxInstances = n("MaxInstances").InnerText
                '        i.UsesPerInstance = n("UsesPerInstance").InnerText
                '        i.LifetimePerInstance = n("LifetimePerInstance").InnerText
                '        _interops.AddInterop(i)
                '    End If
                'Next
            End Sub

            ''' <summary>
            ''' Matches the requested file to a MimeType based on the FileType (extension). MimeTypes are defined in the server config file and include attributes that determine how the server will handle compression and expiration for the resource.
            ''' </summary>
            ''' <param name="path"></param>
            ''' <returns></returns>
            ''' <remarks></remarks>
            Function GetContentType(ByVal path As String)
                Dim ext As String = IO.Path.GetExtension(path).TrimStart(".")
                Dim m As MimeType = MimeTypes(ext)
                Dim contentType As String
                If m IsNot Nothing Then
                    contentType = m.Name
                Else
                    contentType = "text/plain"
                    ' TODO: implement the default MimeType from the config file...
                    '  e.g. contentType = MimeTypes.Default.Name
                End If
                Return contentType
            End Function

            ''' <summary>
            ''' Starts the server, allowing clients to connect and make requests.
            ''' </summary>
            ''' <param name="Ip"></param>
            ''' <param name="Port"></param>
            ''' <remarks></remarks>
            Sub StartServer(ByVal Ip As String, ByVal Port As Integer)
                ' new async pattern
                _bufferManager.InitBuffer()
                Dim readWriteEventArg As New Net.Sockets.SocketAsyncEventArgs
                ' allocate enough memory in the shared buffer, and enough SocketAsyncEventArgs objects, for the max connections that we wish to support
                For i = 0 To _maxConnections - 1
                    readWriteEventArg = New Net.Sockets.SocketAsyncEventArgs
                    AddHandler readWriteEventArg.Completed, AddressOf IoCompleted
                    readWriteEventArg.UserToken = New AsyncUserToken
                    _bufferManager.SetBuffer(readWriteEventArg)
                    _readWritePool.Push(readWriteEventArg)
                Next
                Dim endPoint As Net.IPEndPoint = AddressToEndpoint(Ip, Port)
                _serverSocket = New System.Net.Sockets.Socket(endPoint.AddressFamily, Net.Sockets.SocketType.Stream, Net.Sockets.ProtocolType.Tcp)
                _serverSocket.Bind(endPoint)
                _serverSocket.Listen(20000)
                StartAccept(Nothing)
                RaiseEvent ServerStarted()
            End Sub

            Sub StartAccept(ByVal acceptEventArg As Net.Sockets.SocketAsyncEventArgs)
                If (acceptEventArg Is Nothing) Then
                    acceptEventArg = New Net.Sockets.SocketAsyncEventArgs
                    AddHandler acceptEventArg.Completed, AddressOf AcceptEventArgCompleted
                Else
                    acceptEventArg.AcceptSocket = Nothing
                End If
                _maxNumberAcceptedClients.WaitOne()
                Dim willRaiseEvent As Boolean = _serverSocket.AcceptAsync(acceptEventArg)
                If willRaiseEvent = False Then
                    ProcessAccept(acceptEventArg)
                End If
            End Sub

            Sub AcceptEventArgCompleted(ByVal sender As Object, ByVal e As Net.Sockets.SocketAsyncEventArgs)
                ProcessAccept(e)
            End Sub

            ''' <summary>
            ''' Process the accepted client connection, start receiving data from that client, and start accepting more client connections.
            ''' </summary>
            ''' <param name="e"></param>
            ''' <remarks></remarks>
            Sub ProcessAccept(ByVal e As Net.Sockets.SocketAsyncEventArgs)
                Threading.Interlocked.Increment(_numConnections)
                Dim readEventArgs As Net.Sockets.SocketAsyncEventArgs = _readWritePool.Pop
                DirectCast(readEventArgs.UserToken, AsyncUserToken).Socket = e.AcceptSocket
                Dim willRaiseEvent As Boolean = e.AcceptSocket.ReceiveAsync(readEventArgs)
                If Not willRaiseEvent Then
                    ProcessReceive(readEventArgs) ' NEVER GETS CALLED...due to keep-alive?
                End If
                ' accept the next incoming client connection
                StartAccept(e) ' maybe move this up higher in the method
            End Sub

            Sub IoCompleted(ByVal sender As Object, ByVal e As Net.Sockets.SocketAsyncEventArgs)
                Dim s As String = Text.Encoding.UTF8.GetString(e.Buffer, e.Offset, e.BytesTransferred)
                Select Case e.LastOperation
                    Case Net.Sockets.SocketAsyncOperation.Receive
                        ProcessReceive(e)
                        Exit Select
                    Case Net.Sockets.SocketAsyncOperation.Send
                        ProcessSend(e)
                        Exit Select
                    Case Else
                        Beep()
                End Select
            End Sub

            Sub ProcessReceive(ByVal e As Net.Sockets.SocketAsyncEventArgs)
                ' get the client who we are receiving data from
                Dim token As AsyncUserToken = e.UserToken
                ' handle the received data by parsing it into a request and sending a response in return
                If e.BytesTransferred > 0 And e.SocketError = Net.Sockets.SocketError.Success Then
                    Threading.Interlocked.Add(_totalBytesRead, e.BytesTransferred)
                    ' place the received data into the shared buffer to avoid frequent memory allocations as we process it
                    'e.SetBuffer(e.Offset, e.BytesTransferred) ' if bytesTransferred is very small (40 bytes) it will set the buffer size to 40 as well..and it will be 40 the next time receive...BAD NEWS
                    'e.SetBuffer(e.Offset, 4096)
                    ' get the data
                    'Dim b(e.BytesTransferred - 1) As Byte
                    'Buffer.BlockCopy(e.Buffer, e.Offset, b, 0, e.BytesTransferred)
                    Dim s As String = Text.Encoding.UTF8.GetString(e.Buffer, e.Offset, e.BytesTransferred)
                    ' TODO: process the data (below methods use old async, fix this)
                    ' construct the req/res and pass it to the event handler for final handling
                    ' probably pass the raw request to a threadpool thread for processing, where we can construct the req/res

                    Dim req As New Request(s, Me)
                    Dim res As New Response(Me, req, token.Socket)

                    Dim rs As String = "HTTP/1.1 200 OK" & vbCrLf & "Content-Length: 2" & vbCrLf & vbCrLf & "hi"
                    Dim rb() As Byte = System.Text.Encoding.ASCII.GetBytes(rs)
                    'rb = res.GetResponseBytes
                    'e.SetBuffer(resb, 0, resb.Length)
                    'e.SetBuffer(e.Offset, 4096)
                    e.SetBuffer(rb, 0, rb.Length)
                    'Buffer.BlockCopy(rb, 0, e.Buffer, e.Offset, rb.Length)

                    Dim willRaiseEvent As Boolean = token.Socket.SendAsync(e)

                    'Dim willRaiseEvent As Boolean = token.Socket.ReceiveAsync(e)

                    'Dim o As New ProcessRequestObject
                    'o.requestBytes = b
                    'o.server = Me
                    'o.clientSocket = token.Socket
                    'o.e = e
                    'Threading.ThreadPool.QueueUserWorkItem(AddressOf PrepareRequest, o)

                    'Dim pr As New ProcessRequestObject()
                    'pr.requestBytes = b
                    'Dim test As String = Text.Encoding.ASCII.GetString(pr.requestBytes)
                    'pr.server = Me
                    'pr.clientSocket = token.Socket
                    'pr.e = e
                    'Threading.ThreadPool.QueueUserWorkItem(AddressOf ProcessRequest, pr)

                    'Dim s As String = System.Text.Encoding.ASCII.GetString(e.Buffer, e.Offset, e.BytesTransferred) ' TODO: contains the http request
                    'Dim req As New HttpRequest(s, Me)
                    'Dim res As New HttpResponse(Me, req, token.Socket)
                    'RaiseEvent HandleRequest(req, res, token.Socket)

                    ' TODO: what to do with s? what is the point of willRaiseEvent?
                    'Dim willRaiseEvent As Boolean = token.Socket.SendAsync(e)
                    'If Not willRaiseEvent Then
                    '    ProcessSend(e)
                    'End If
                Else
                    CloseClientSocket(e)
                End If
            End Sub

            Sub ProcessSend(ByVal e As Net.Sockets.SocketAsyncEventArgs)
                If e.SocketError = Net.Sockets.SocketError.Success Then
                    Dim token As AsyncUserToken = e.UserToken
                    '' TODO: LEFT OFF HERE
                    'Dim x As Net.Sockets.SocketAsyncEventArgs = _readWritePool.Pop
                    'x.UserToken = token
                    'Dim willRaiseEvent As Boolean = token.Socket.ReceiveAsync(e)
                    CloseClientSocket(e)
                    'If Not willRaiseEvent Then
                    '    ProcessReceive(e)
                    'End If
                Else
                    CloseClientSocket(e)
                End If
            End Sub

            ''' <summary>
            ''' This prepares the request/response. Should be called from an IOCP thread with Threadpool.QueueUserWorkItem() to free up the IOCP thread as soon as possible and do the processing on a threadpool thread. Finally, this passes the request/response to the HandleRequest event for default HTTP handling, which can be overridden during implementation for custom handling.
            ''' </summary>
            ''' <remarks></remarks>
            Private Sub PrepareRequest(ByVal o As ProcessRequestObject)
                ' convert the raw request bytes/string into an HttpRequest object for ease of use
                Dim req As New Request(o.requestBytes, o.server)
                ' prepare the response that will be sent back to the client (e.g. load and send a static page e.g. txt or html, load and send a resource e.g. jpg or xml, parse a dynamic script e.g. php or asp.net)
                ' TODO: here we construct a response, but when serving a response from the output cache we shouldn't construct one at all!
                Dim res As New Response(Me, req, o.clientSocket)
                ' raise an event to handle the request/response cycle (this can be overridden during implementation to allow for custom handling)
                RaiseEvent HandleRequest(req, res, o.e)
            End Sub

            ''' <summary>
            ''' Handles the client request/response cycle, replying to a request with a response by default. May be overridden during implementation for custom response handling.
            ''' </summary>
            ''' <param name="req"></param>
            ''' <param name="res"></param>
            ''' <param name="client"></param>
            ''' <remarks></remarks>
            Private Sub _HandleRequest(ByVal req As Request, ByVal res As Response, ByVal client As Net.Sockets.SocketAsyncEventArgs) Handles Me.HandleRequest
                ' serve the requested resource from the output cache or from disk; better yet, store the entire response and serve that up to save some time
                If OutputCache.ContainsKey(req.AbsoluteUrl) = True Then
                    Dim cachedResponse As Response = OutputCache(req.AbsoluteUrl)
                    res = cachedResponse
                    DebugMessage("Serving resource from cache: " & req.AbsoluteUrl & ".", DebugMessageType.UsageMessage, "ClientRequest event")
                    ' TODO: we might want to move the res.GetAllBytes call into the Else condition here so it doesn't get called again for an already cached response
                Else
                    ' serve the file from disk
                    ' TODO: depending on TransferMethod requested by the client, we should implement StoreAndForward or ChunkedEncoding, but for now we will just use StoreAndForward
                    If IO.File.Exists(req.AbsoluteUrl) = True Then
                        ' determine how to the process the client's request based on the requested uri's file type
                        ' this is where we might load a resourince, maybe using Interops to parse dynamic scripts (e.g. PHP and ASP.NET)
                        If req.MimeType.Handler = "" Then
                            ' no custom handler for this mimetype, serve as a static file
                            res.ContentType = req.MimeType.Name 'GetContentType(req.AbsoluteUrl)
                            res.SetContent(IO.File.ReadAllBytes(req.AbsoluteUrl))
                            res.StatusCode = 200
                        Else
                            ' TODO: there is a custom handler assigned to this filetype, but handlers are not implemented yet so we just serve it as a static file
                            res.ContentType = req.MimeType.Name 'GetContentType(req.AbsoluteUrl)
                            res.SetContent(IO.File.ReadAllBytes(req.AbsoluteUrl))
                            res.StatusCode = 200
                        End If
                        ' cache the response for future use to improve performance, avoiding the need to process the same response frequently
                        If OutputCache.ContainsKey(req.AbsoluteUrl) = False Then OutputCache.Add(req.AbsoluteUrl, res)
                    Else
                        ' page not found, return 404 status code
                        res.StatusCode = 404
                    End If
                End If

                ' send the response to the client who made the initial request
                Dim responseBytes() As Byte = res.GetResponseBytes
                'Dim sendEventArg As Net.Sockets.SocketAsyncEventArgs = _readWritePool.Pop
                'Dim ar As AsyncUserToken = client
                'sendEventArg.UserToken = ar
                'sendEventArg.SetBuffer(responseBytes, 0, responseBytes.Length)
                client.SetBuffer(responseBytes, 0, responseBytes.Length)
                Dim token As AsyncUserToken = client.UserToken
                Dim willRaiseEvent As Boolean = token.Socket.SendAsync(client)
                If Not willRaiseEvent Then
                    ' TODO: this never fires!! why not?
                    Beep()
                End If

                ' handle keep-alive or disconnect
                If res.Headers("Connection") = "keep-alive" Then
                    ' receive more from the client
                    'Dim readEventArgs As Net.Sockets.SocketAsyncEventArgs = _readWritePool.Pop
                    'token.Socket.ReceiveAsync(readEventArgs)
                Else
                    'token.Socket.Disconnect(False)
                    'token.Socket.Close()
                    ' TODO: maybe move this into the CompleteReceive where we call ReceiveAsync...
                    CloseClientSocket(client)
                End If

                'Dim sendState As New AsyncSendState(client)
                'sendState.BytesToSend = responseBytes
                'sendState.Tag = req.AbsoluteUrl
                'If res.Headers("Connection") = "keep-alive" Then
                '    sendState.Persistent = True
                'End If
                'Try
                '    client.BeginSend(responseBytes, 0, responseBytes.Length, Net.Sockets.SocketFlags.None, AddressOf CompleteSend, sendState)
                'Catch ex As Exception
                '    LogEvent("Could not send data to this client. An unhandled exception occurred.", LogEventType.UsageMessage, "ClientRequest", ex.Message)
                'End Try

                '' call BeginReceive again, so we can receive more data from this client socket (either not needed for http server or only for persistent connections...reimplement for binary rpc server)
                'If sendState.Persistent = True Then
                '    Dim receiveState As New AsyncReceiveState
                '    receiveState.Socket = client
                '    Try
                '        receiveState.Socket.BeginReceive(receiveState.Buffer, 0, gBufferSize, Net.Sockets.SocketFlags.None, New AsyncCallback(AddressOf CompleteRequest), receiveState)
                '    Catch ex As Exception
                '        LogEvent("Could not receive more data from this client. An unhandled exception occurred.", LogEventType.UsageMessage, "ClientRequest", ex.Message)
                '    End Try
                'End If

            End Sub

            Sub CloseClientSocket(ByVal e As Net.Sockets.SocketAsyncEventArgs)
                Dim token As AsyncUserToken = e.UserToken
                Try
                    token.Socket.Shutdown(Net.Sockets.SocketShutdown.Send)
                Catch ex As Exception

                End Try
                token.Socket.Close()
                Threading.Interlocked.Decrement(_numConnections)
                _maxNumberAcceptedClients.Release()
                _readWritePool.Push(e)
                Console.WriteLine(_readWritePool.Count)
            End Sub

            ''' <summary>
            ''' Stops the server, disconnecting any current clients and terminating any pending requests/responses.
            ''' </summary>
            ''' <remarks></remarks>
            Sub StopServer()
                Try
                    ' try to safely shutdown first, allowing pending transmissions to finish which helps prevent prevent data loss
                    _serverSocket.Shutdown(Net.Sockets.SocketShutdown.Both)
                Catch ex As Exception
                    DebugMessage("Server put up a fight while shutting down.", DebugMessageType.WarningMessage, "StopServer", ex.Message)
                End Try
                ' kill the server socket, terminating any clients abrubtly (data loss may occur if Shutdown is unsuccessful)
                _serverSocket.Close()
            End Sub

            Class ProcessRequestObject
                Public requestBytes() As Byte
                Public server As Server
                Public clientSocket As Net.Sockets.Socket
                Public e As Net.Sockets.SocketAsyncEventArgs
            End Class
        End Class

        ''' <summary>
        ''' An http request, usually sent by a client.
        ''' </summary>
        ''' Clients will send requests to the server using an http request with the following signature:
        ''' ---------------------------
        ''' GET /file.html HTTP/1.1\r\n
        ''' User-Agent: Chrome/1.0\r\n
        ''' Host: example.com\r\n
        ''' Accept: */*
        ''' ---------------------------
        ''' The first line in the request is the Request-Line which is mandatory. All subsequent lines are Header-Lines which are optional. Every line must be terminated with \r\n.
        ''' <remarks></remarks>
        Public Class Request
            Private _server As Server
            Public Method As String             ' the request method (GET or POST)
            Public Uri As String                ' the Uri requested by the client
            Public Headers As New Hashtable     ' the request Headers (key:value pairs)
            Public FileType As String           ' the requested Uri's file type
            Public QueryString As String        ' the Query String which sometimes appears as a subcomponent of the Uri (e.g. "?user=Perry")
            Public AbsoluteUrl As String
            Public MimeType As MimeType

            Sub New(ByVal requestString As String, ByVal server As Server)
                _server = server
                ParseRequestString(requestString)
            End Sub

            Sub New(ByVal buffer() As Byte, ByVal server As Server)
                _server = server
                Dim requestString = System.Text.Encoding.ASCII.GetString(buffer)
                ParseRequestString(requestString)
            End Sub

            ''' <summary>
            ''' Parses the raw request string received from the client socket
            ''' </summary>
            ''' <param name="requestString"></param>
            ''' <remarks></remarks>
            Sub ParseRequestString(ByVal requestString As String)
                Dim requestStringParts() As String = requestString.Split(vbCrLf)

                ' parse the request-line which is the first line in the request string (e.g. "GET /file.html HTTP/1.1")
                Dim httpRequestLine = requestStringParts(0) 'requestString.Split(vbNewLine)(0)

                ' parse the uri (including query string) from the request-line
                ' TODO: during a few refreshes, an exception is thrown here because the requestString is fragmented - "36 Accept(-Encoding) : gzip, deflate, sdch Accept-Language: en-US,en;q=0.8
                Me.Uri = httpRequestLine.Substring(4, httpRequestLine.Length - 13).Replace("/", "\")

                ' split the uri and query string into their separate components
                Dim qsIndex As Integer = Me.Uri.IndexOf("?")
                If qsIndex <> -1 Then
                    QueryString = Me.Uri.Substring(qsIndex)
                    Me.Uri = Me.Uri.Substring(0, qsIndex)
                End If

                ' determine the absolute path for the requested resource
                Me.AbsoluteUrl = _server.WebRoot & Me.Uri

                ' if the client requested a directory, provide a directory listing or prepare to serve up the default document
                If IO.Directory.Exists(AbsoluteUrl) = True Then
                    For Each d As String In _server.DefaultDocuments
                        If IO.File.Exists(_server.WebRoot & "/" & d) Then
                            Me.Uri &= d
                            Me.AbsoluteUrl = _server.WebRoot & Me.Uri
                            Exit For
                        End If
                    Next
                End If

                ' parse the requested resource's file type (extension) and path
                Me.FileType = IO.Path.GetExtension(Me.Uri).TrimStart(".")

                ' parse the requested resource's mime type
                Dim m As MimeType = _server.MimeTypes(FileType)
                If m Is Nothing Then
                    m = _server.MimeTypes("")
                End If
                Me.MimeType = m

                ' parse the remaining request headers
                For i = 1 To requestStringParts.Length - 2
                    Dim delimIndex As Integer = requestStringParts(i).IndexOf(": ")
                    If delimIndex > 0 Then
                        Dim key As String = requestStringParts(i).Substring(1, delimIndex - 1)
                        Dim value As String = requestStringParts(i).Substring(delimIndex + 2, requestStringParts(i).Length - delimIndex - 2)
                        Headers.Add(key, value)
                    End If
                Next

            End Sub
        End Class

        ''' <summary>
        ''' An http response, normally sent from the server back to the client who made the initial request.
        ''' </summary>
        ''' The server will respond to clients using and http response with the following signature:
        ''' ---------------------------
        ''' HTTP/1.1 200 OK\r\n
        ''' Content-Type: text/plain\r\n
        ''' Content-Length: 13\r\n
        ''' Connection: close\r\n
        ''' \r\n
        ''' hello world
        ''' ---------------------------
        ''' The first line in the response code which is mandatory. Subsequent lines are header-lines. The second and third lines (header-lines) describe the content, which are mandatory. Every line in the response must be terminated with \r\n. Two line terminators delimit the content from the headers, which is mandatory.
        ''' <remarks></remarks>
        Public Class Response
            Private _server As Server                   ' a reference to the server instance
            Private _content() As Byte                  ' contains the content bytes for the response
            Private _headers As New Hashtable           ' contains the header strings for the response
            Public MimeType As MimeType                 ' the requested uri's mimetype
            Public TransferMethod As TransferMethod     ' the transfer method to be used (store and forward, chunked encoding)
            Public StatusCode As Integer                ' the http status code (e.g. 200 = OK, 404 = Page not found)
            Public ContentType As String                ' the requested uri's content type, which is pulled from the mimetype
            Public ContentLength As String              ' the number of bytes representing the content

            Sub New(ByVal server As Server, ByVal req As Request, ByVal client As Net.Sockets.Socket)
                If req.Headers.ContainsKey("Connection") = True Then
                    If req.Headers("Connection").ToString.ToLower = "keep-alive" Then
                        SetHeader("Connection", "keep-alive")
                    End If
                End If
                ' set any headers required by the requested resource's mimetype
                MimeType = req.MimeType
                If MimeType.Compress <> CompressionMethod.None Then
                    SetHeader("Content-Encoding", [Enum].GetName(GetType(CompressionMethod), MimeType.Compress).ToLower)
                End If
                ' set any custom request headers defined in the config file
                For Each s As String In server.ResponseHeaders
                    'Dim spl() As String = s.Split(": ")
                    'SetHeader(spl(0), spl(1))
                    Dim delimIndex As Integer = s.IndexOf(": ")
                    If delimIndex > 0 Then
                        Dim key As String = s.Substring(0, delimIndex)
                        Dim value As String = s.Substring(delimIndex + 2, s.Length - delimIndex - 2)
                        SetHeader(key, value)
                    End If
                Next
            End Sub

            Public ReadOnly Property Content()
                Get
                    Return _content
                End Get
            End Property

            Public ReadOnly Property Headers()
                Get
                    Return _headers
                End Get
            End Property

            ''' <summary>
            ''' The primary method for setting the response content. Any other methods which also set the content should ultimately route through this method.
            ''' </summary>
            ''' <param name="contentBytes"></param>
            ''' <remarks></remarks>
            Sub SetContent(ByVal contentBytes() As Byte)
                ' TODO: conditionally set Content-Length if needed - the header is not always necessary (e.g. when TransferMethod = ChunkedEncoding)
                Dim ms As New IO.MemoryStream
                If contentBytes IsNot Nothing Then
                    If MimeType.Compress = CompressionMethod.Gzip Then
                        Dim gZip As New System.IO.Compression.GZipStream(ms, IO.Compression.CompressionMode.Compress, True)
                        gZip.Write(contentBytes, 0, contentBytes.Length)
                        ' make sure we close the compression stream or else it won't flush the full buffer! see: http://stackoverflow.com/questions/6334463/gzipstream-compression-problem-lost-byte
                        gZip.Close()
                        gZip.Dispose()
                    ElseIf MimeType.Compress = CompressionMethod.Deflate Then
                        Dim deflate As New System.IO.Compression.DeflateStream(ms, IO.Compression.CompressionMode.Compress, True)
                        deflate.Write(contentBytes, 0, contentBytes.Length)
                        ' make sure we close the compression stream or else it won't flush the full buffer! see: http://stackoverflow.com/questions/6334463/gzipstream-compression-problem-lost-byte
                        deflate.Close()
                        deflate.Dispose()
                    Else
                        ' no compression should be used on this resource, write the data as-is (uncompressed or already-compressed)
                        ms.Write(contentBytes, 0, contentBytes.Length)
                    End If
                End If
                Dim cbuf(ms.Length - 1) As Byte ' create a buffer exactly the size of the memorystream length (not its buffer length)
                Dim mbuf() As Byte = ms.GetBuffer
                ms.Close()
                ms.Dispose()
                Buffer.BlockCopy(mbuf, 0, cbuf, 0, cbuf.Length)
                Me.ContentLength = cbuf.Length
                _content = cbuf
            End Sub

            Sub SetContent(ByVal contentString As String)
                ' just pass the string as bytes to the primary SetContent method
                SetContent(System.Text.Encoding.ASCII.GetBytes(contentString))
            End Sub

            Sub SetHeader(ByVal key As String, ByVal value As String)
                ' TODO: this seems to be trying to set the Reponse Headers several times per page load... e.g. _headers.Add(key, key & value) says item already exists
                '_headers(key) = key & ": " & value
                _headers(key) = value
            End Sub

            Function GetHeaderString() As String
                Dim s As String = ""
                ' append the common headers
                s &= "HTTP/1.1 " & StatusCode & " " & StatusCodeMessage() & vbCrLf
                s &= "Content-Length: " & ContentLength & vbCrLf
                s &= "Content-Type: " & ContentType & vbCrLf
                s &= "Date: " & DateTime.Now.ToString("r") & vbCrLf
                ' append the headers that have been dynamically or conditionally set (request headers, compression, etc)
                For Each h As String In _headers.Keys
                    s &= h & ": " & _headers(h) & vbCrLf
                Next
                ' one extra cr/lf is required for delimiting the header from the content, per http specs
                s &= vbCrLf
                Return s
            End Function

            ''' <summary>
            ''' Gets the bytes that represent the final response including the headers and content.
            ''' </summary>
            ''' <returns></returns>
            ''' <remarks></remarks>
            Function GetResponseBytes() As Byte()
                Dim ms As New IO.MemoryStream
                ' get the header bytes and add it to the response
                Dim headerBytes() As Byte = System.Text.Encoding.ASCII.GetBytes(GetHeaderString)
                ms.Write(headerBytes, 0, headerBytes.Length)
                ' if there is content, add it to the response
                If _content IsNot Nothing Then
                    ms.Write(_content, 0, _content.Length)
                End If
                Dim rbuf(ms.Length - 1) As Byte
                Dim mbuf() As Byte = ms.GetBuffer
                Buffer.BlockCopy(mbuf, 0, rbuf, 0, rbuf.Length)
                Return rbuf
            End Function

            ''' <summary>
            ''' Gets a standard message for an http status code.
            ''' </summary>
            ''' <returns></returns>
            ''' <remarks></remarks>
            Function StatusCodeMessage() As String
                Dim msg As String = ""
                Select Case StatusCode
                    Case 200
                        msg = "OK"
                    Case 404
                        msg = "Page not found."
                End Select
                Return msg
            End Function

            Sub WriteHeader(ByVal statusCode)
                ' TODO: write the full header to the client stream
            End Sub

            Sub Send(ByVal statusCode As Integer, ByVal message As String)

            End Sub

            Sub SendFile(ByVal statusCode As Integer, ByVal file As Byte())

            End Sub

            Sub Finish()
                ' TODO: finish the response cycle by flushing buffered data in the client stream
            End Sub

        End Class

        ''' <summary>
        ''' A shared buffer which the SocketAsyncEventArgsPool utilizes to read/write data without memory thrashing/fragmentation.
        ''' </summary>
        ''' <remarks></remarks>
        Class BufferManager
            Private m_numBytes As Integer
            Private m_buffer As Byte() ' the total number of bytes controlled by the buffer pool
            Private m_freeIndexPool As Stack(Of Integer) ' the underlying byte array maintained by the Buffer Manager
            Private m_currentIndex As Integer
            Private m_bufferSize As Integer

            Public Sub New(ByVal totalBytes As Integer, ByVal bufferSize As Integer)
                m_numBytes = totalBytes
                m_currentIndex = 0
                m_bufferSize = bufferSize
                m_freeIndexPool = New Stack(Of Integer)()
            End Sub

            ' Allocates buffer space used by the buffer pool 
            Public Sub InitBuffer()
                ' create one big large buffer and divide that  
                ' out to each SocketAsyncEventArg object
                m_buffer = New Byte(m_numBytes - 1) {}
            End Sub

            ' Assigns a buffer from the buffer pool to thespecified SocketAsyncEventArgs object returns true if the buffer was successfully set, else false.
            Public Function SetBuffer(ByVal args As Net.Sockets.SocketAsyncEventArgs) As Boolean

                If m_freeIndexPool.Count > 0 Then
                    args.SetBuffer(m_buffer, m_freeIndexPool.Pop(), m_bufferSize)
                Else
                    If (m_numBytes - m_bufferSize) < m_currentIndex Then
                        Return False
                    End If
                    args.SetBuffer(m_buffer, m_currentIndex, m_bufferSize)
                    m_currentIndex += m_bufferSize
                End If
                Return True
            End Function

            ' Removes the buffer from a SocketAsyncEventArg object. This frees the buffer back to the buffer pool.
            Public Sub FreeBuffer(ByVal args As Net.Sockets.SocketAsyncEventArgs)
                m_freeIndexPool.Push(args.Offset)
                args.SetBuffer(Nothing, 0, 0)
            End Sub

        End Class

        ''' <summary>
        ''' A client state object representing a single client.
        ''' </summary>
        ''' <remarks></remarks>
        Public Class AsyncUserToken
            Private _socket As Net.Sockets.Socket
            Public Content As String
            Sub New()

            End Sub
            Sub New(ByVal socket As Net.Sockets.Socket)
                _socket = socket
            End Sub
            Public Property Socket() As Net.Sockets.Socket
                Get
                    Return _socket
                End Get
                Set(ByVal value As Net.Sockets.Socket)
                    _socket = value
                End Set
            End Property
        End Class

        ''' <summary>
        ''' A pool of reusable SocketAsyncEventArgs objects.
        ''' </summary>
        ''' <remarks></remarks>
        Class SocketAsyncEventArgsPool
            Private m_pool As Stack(Of Net.Sockets.SocketAsyncEventArgs)

            ' Initializes the object pool to the specified size 
            ' 
            ' The "capacity" parameter is the maximum number of 
            ' SocketAsyncEventArgs objects the pool can hold 
            Public Sub New(ByVal capacity As Integer)
                m_pool = New Stack(Of Net.Sockets.SocketAsyncEventArgs)(capacity)
            End Sub

            ' Add a SocketAsyncEventArg instance to the pool 
            ' 
            'The "item" parameter is the SocketAsyncEventArgs instance 
            ' to add to the pool 
            Public Sub Push(ByVal item As Net.Sockets.SocketAsyncEventArgs)
                If item Is Nothing Then
                    Throw New ArgumentNullException("Items added to a SocketAsyncEventArgsPool cannot be null")
                End If
                SyncLock m_pool
                    m_pool.Push(item)
                End SyncLock
            End Sub

            ' Removes a SocketAsyncEventArgs instance from the pool 
            ' and returns the object removed from the pool 
            Public Function Pop() As Net.Sockets.SocketAsyncEventArgs
                SyncLock m_pool
                    Return m_pool.Pop()
                End SyncLock
            End Function

            ' The number of SocketAsyncEventArgs instances in the pool 
            Public ReadOnly Property Count() As Integer
                Get
                    Return m_pool.Count
                End Get
            End Property

        End Class
    End Namespace
End Namespace