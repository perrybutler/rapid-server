Namespace Http
    Namespace Type1
        ''' <summary>
        ''' An object for passing off the request from an IOCP thread to a threadpool (worker) thread for processing.
        ''' </summary>
        ''' <remarks></remarks>
        Class HandleRequestObject
            Public requestBytes() As Byte
            Public server As RapidServer.Http.Type1.Server
            Public clientSocket As Net.Sockets.Socket
            Public site As Site
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
            Inherits SimpleRequestResponse
            Private _server As Server
            Public Site As Site
            Public ClientAddress As String
            Public RequestString As String
            Public RequestLine As String        ' the first line of the request string which contains the method, uri and protocol
            Public Method As String             ' the request method (GET or POST)
            Public FileName As String
            Public ScriptName As String
            Public Uri As String                ' the uri of the resource (e.g. /index.php)
            Public RelPath As String            ' the relative path of the resource on disk (e.g. \index.php)
            Public Protocol As String           ' the http protocol (e.g. HTTP/1.1)
            Public FileType As String           ' the file type of the resource (e.g. jpg)
            Public QueryString As String = ""   ' the query string which sometimes appears as a subcomponent of the uri (e.g. "?user=Perry")
            Public ContentLength As String
            Public AbsPath As String            ' the absolute path of the resource on disk (e.g. c:\site1\index.php)
            Public MimeType As MimeType         ' the mime type of the resource (e.g. image/jpeg)
            Public FixPath301 As Boolean

            Sub New(requestString As String, ByVal server As Server, ByVal client As Net.Sockets.Socket, ByVal site As Site)
                MyBase.new()
                Me._server = server
                Me.RequestString = requestString
                Me.Site = site
                Me.ClientAddress = client.RemoteEndPoint.ToString
                ' convert the raw request data into a string and parse it
                'Me.RequestString = System.Text.Encoding.Default.GetString(buffer).Replace(Chr(0), "")

                ' parse raw data into strings
                Me.Parse(Me.RequestString)
                ' parse strings into strongly typed properties
                Me.ParseRequestString(Me.RequestString)
            End Sub

            'Sub New(ByVal buffer() As Byte, ByVal server As Server, ByVal client As Net.Sockets.Socket, ByVal site As Site)
            '    MyBase.new()
            '    _server = server
            '    Me.Site = site
            '    Me.ClientAddress = client.RemoteEndPoint.ToString
            '    ' convert the raw request data into a string and parse it
            '    'Me.RequestString = System.Text.Encoding.Default.GetString(buffer).Replace(Chr(0), "")
            '    Me.RequestString = System.Text.Encoding.ASCII.GetString(buffer).Trim(Chr(0))
            '    ' parse raw data into strings
            '    Me.Parse(Me.RequestString)
            '    ' parse strings into strongly typed properties
            '    Me.ParseRequestString(Me.RequestString)
            'End Sub

            ''' <summary>
            ''' Parses the raw request string received from the client socket
            ''' </summary>
            ''' <param name="requestString"></param>
            ''' <remarks></remarks>
            Sub ParseRequestString(ByVal requestString As String)
                ' parse the requestString to build up the request object
                Dim headerStringParts() As String = Me.HeaderString.Split(vbCrLf)
                If (headerStringParts(0).StartsWith("HEAD") Or headerStringParts(0).StartsWith("GET") Or headerStringParts(0).StartsWith("POST")) Then
                    ' parse the request line
                    Me.RequestLine = headerStringParts(0)
                    Dim requestLineParts() As String
                    requestLineParts = Me.RequestLine.Split(" ")
                    Me.Method = requestLineParts(0)
                    Me.Uri = requestLineParts(1)
                    Me.Protocol = requestLineParts(2)

                    ' build the relative and absolute path to the file
                    Me.RelPath = Me.Uri.Replace("/", "\")
                    If Me.Uri.Contains("?") Then
                        Dim uriParts() As String = Me.Uri.Split("?")
                        Me.RelPath = uriParts(0).Replace("/", "\")
                        Me.QueryString = "?" & uriParts(1)
                    End If
                    Me.AbsPath = Me.Site.RootPath & Me.RelPath

                    ' if the requested path was a directory, use the default document
                    If IO.Directory.Exists(Me.AbsPath) = True Then
                        ' if the requested path was a directory, but the Uri was missing a trailing slash, we need to 301 redirect to the correct Uri
                        If Me.Uri.EndsWith("/") = False Then
                            Me.FixPath301 = True
                        End If
                        ' build the paths to the requested resource
                        ' TODO: file.exists is one of the slowest operations here, cache it...
                        '   however, we are already caching responses and we should skip this function for cached responses which we are not currently doing since we parse before handling cache...
                        For Each doc As String In _server.DefaultDocuments
                            If IO.File.Exists(Me.Site.RootPath & Me.RelPath & "\" & doc) Then
                                Me.FileName = doc
                                Me.RelPath = Me.RelPath & "\" & Me.FileName
                                Me.AbsPath = Me.Site.RootPath & Me.RelPath
                                Exit For
                            End If
                        Next

                        'Me.FileName = "index.html"
                        'Me.RelPath = Me.RelPath & "\" & Me.FileName
                        'Me.AbsPath = Me.Site.RootPath & Me.RelPath
                    Else
                        ' not a directory, get filename from abspath
                        Me.FileName = IO.Path.GetFileName(Me.AbsPath)
                    End If

                    ' TODO: if the directory was empty, Me.FileName will be empty here and we can't proceed with it; serve directory listing or return a 40X status code...
                    If Me.FileName <> Nothing Then
                        ' build the scriptname
                        If Me.Uri.Contains(Me.FileName) Then
                            Me.ScriptName = Me.Uri
                        Else
                            Me.ScriptName = Me.Uri & Me.FileName & Me.QueryString
                        End If
                        ' strip the querystring from the scriptname, causes problems with WP customizer
                        If Me.QueryString <> "" Then
                            If Me.ScriptName.Contains(Me.QueryString) Then
                                Me.ScriptName = Me.ScriptName.Replace(Me.QueryString, "")
                            End If
                        End If

                        ' parse the requested resource's file type (extension) for use determining the mime type
                        Me.FileType = IO.Path.GetExtension(Me.AbsPath).TrimStart(CChar("."))
                        ' parse the requested resource's mime type
                        Dim m As MimeType = CType(_server.MimeTypes(FileType), MimeType)
                        If m Is Nothing Then
                            m = CType(_server.MimeTypes(""), MimeType)
                        End If
                        Me.MimeType = m
                        ' set content length
                        Me.ContentLength = Me.ContentStringLength
                        ' set the content bytes
                        Me.Content = Text.Encoding.ASCII.GetBytes(Me.ContentString)
                    End If
                End If
            End Sub
        End Class

        ''' <summary>
        ''' An http response, normally sent from the server back to the client who made the initial request.
        ''' </summary>
        ''' <remarks></remarks>
        Public Class Response
            Inherits SimpleRequestResponse
            Private _server As Http.Type1.Server        ' a reference to the server instance
            Private _content() As Byte                  ' content payload
            Public MimeType As MimeType                 ' requested uri's mimetype
            'Public TransferMethod As TransferMethod    ' TODO: the transfer method to be used (store and forward, chunked encoding)
            Public ContentType As String                ' requested uri's content type, which is pulled from the mimetype
            Public ContentLength As String              ' number of bytes representing the content
            Public StatusCode As String                 ' status code of the response (e.g. 200, 302, 404)
            Public ScriptName As String
            Public Request As Request
            Public ResponseBytes() As Byte

            Sub New(ByVal server As Server, ByVal req As Request, ByVal client As Net.Sockets.Socket)
                MyBase.new()
                Me.ScriptName = req.ScriptName
                Me.Request = req
                ' if the request includes a Connection: keep-alive header, we need to add it to the response:
                If req.Headers.ContainsKey("Connection") = True Then
                    If req.Headers("Connection").ToString.ToLower = "keep-alive" Then
                        Me.Headers("Connection") = "Keep-Alive"
                    End If
                End If
                ' set the Content-Encoding header to properly represent the requested resource's mimetype:
                If req.MimeType IsNot Nothing Then
                    Me.MimeType = req.MimeType
                    If Me.MimeType.Compress <> CompressionMethod.None Then
                        Me.Headers("Content-Encoding") = [Enum].GetName(GetType(CompressionMethod), Me.MimeType.Compress).ToLower
                    End If
                End If
                ' set any custom response headers defined in the config file:
                ' UNDONE: conflicts with load balancer mode...
                'For Each s As String In server.ResponseHeaders
                '    Dim delimIndex As Integer = s.IndexOf(": ")
                '    If delimIndex > 0 Then
                '        Dim key As String = s.Substring(0, delimIndex)
                '        Dim value As String = s.Substring(delimIndex + 2, s.Length - delimIndex - 2)
                '        Me.Headers(key) = value
                '    End If
                'Next
            End Sub

            ''' <summary>
            ''' The primary method for setting the response content. Any other methods which also set the content should ultimately route through this method.
            ''' </summary>
            ''' <param name="contentBytes"></param>
            ''' <remarks></remarks>
            Sub SetContent(ByVal contentBytes() As Byte)
                ' TODO: conditionally set Content-Length if needed - the header is not always necessary (e.g. when TransferMethod = ChunkedEncoding)
                Dim ms As New IO.MemoryStream
                If contentBytes IsNot Nothing Then
                    If Me.MimeType IsNot Nothing Then
                        If Me.MimeType.Compress = CompressionMethod.Gzip Then
                            Dim gZip As New System.IO.Compression.GZipStream(ms, IO.Compression.CompressionMode.Compress, True)
                            gZip.Write(contentBytes, 0, contentBytes.Length)
                            ' make sure we close the compression stream or else it won't flush the full buffer! see: http://stackoverflow.com/questions/6334463/gzipstream-compression-problem-lost-byte
                            gZip.Close()
                            gZip.Dispose()
                        ElseIf Me.MimeType.Compress = CompressionMethod.Deflate Then
                            Dim deflate As New System.IO.Compression.DeflateStream(ms, IO.Compression.CompressionMode.Compress, True)
                            deflate.Write(contentBytes, 0, contentBytes.Length)
                            ' make sure we close the compression stream or else it won't flush the full buffer! see: http://stackoverflow.com/questions/6334463/gzipstream-compression-problem-lost-byte
                            deflate.Close()
                            deflate.Dispose()
                        Else
                            ' no compression should be used on this resource, just write the data as-is:
                            ms.Write(contentBytes, 0, contentBytes.Length)
                        End If
                    Else
                        ' no mimetype, just write the data as is:
                        ms.Write(contentBytes, 0, contentBytes.Length)
                    End If
                End If
                Dim cbuf(CInt(ms.Length - 1)) As Byte ' create a buffer exactly the size of the memorystream length (not its buffer length)
                Dim mbuf() As Byte = ms.GetBuffer
                ms.Close()
                ms.Dispose()
                Buffer.BlockCopy(mbuf, 0, cbuf, 0, cbuf.Length)
                Me.ContentLength = cbuf.Length.ToString
                _content = cbuf
            End Sub
            Sub SetContent(ByVal contentString As String)
                ' just pass the string as bytes to the primary SetContent method
                SetContent(System.Text.Encoding.UTF8.GetBytes(contentString))
            End Sub

            Function BuildHeaderString() As String
                Dim s As String = ""
                ' append the common headers
                s &= "HTTP/1.1 " & Me.StatusCode & " " & Me.StatusCodeMessage() & vbCrLf
                s &= "Content-Length: " & Me.ContentLength & vbCrLf
                s &= "Content-Type: " & Me.ContentType & vbCrLf
                's &= "Date: " & DateTime.Now.ToString("r") & vbCrLf ' TODO: high cost detected in profiler...reimplement using a faster date method
                ' append the headers that have been dynamically or conditionally set (request headers, compression, etc)
                For Each h As String In Me.Headers.Keys
                    s &= h & ": " & Me.Headers(h).ToString & vbCrLf
                Next
                Return s
            End Function

            ' TODO: merge this into BuildHeaderString(), doesn't need it's own func...
            Function BuildCookieString() As String
                Dim s As String = ""
                For Each h As SimpleHttpHeader In Me.Cookies
                    s &= h.Key & ": " & h.Value & vbCrLf
                Next
                Return s
            End Function

            ''' <summary>
            ''' Gets the bytes that represent the final response including the headers and content.
            ''' </summary>
            ''' <returns></returns>
            ''' <remarks></remarks>
            Function BuildResponseBytes() As Byte()
                Dim ms As New IO.MemoryStream
                Dim fullHeaderString As String = Me.BuildHeaderString & Me.BuildCookieString & vbCrLf ' one extra cr/lf separates header from content
                Dim fullHeaderBytes() As Byte = System.Text.Encoding.ASCII.GetBytes(fullHeaderString)
                ms.Write(fullHeaderBytes, 0, fullHeaderBytes.Length)
                '' get the header bytes and add it to the response
                'Dim headerBytes() As Byte = System.Text.Encoding.ASCII.GetBytes(Me.BuildHeaderString)
                'ms.Write(headerBytes, 0, headerBytes.Length)
                '' TODO: get the cookies and add it to the response
                'Dim cookieBytes() As Byte = System.Text.Encoding.ASCII.GetBytes(Me.BuildCookieString)
                'ms.Write(cookieBytes, 0, cookieBytes.Length)
                ' if there is content, add it to the response
                If _content IsNot Nothing Then
                    ms.Write(_content, 0, _content.Length)
                End If
                Dim rbuf(CInt(ms.Length - 1)) As Byte
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
                    Case 100
                        msg = "Continue"
                    Case 101
                        msg = "Switching Protocols"
                    Case 200
                        msg = "OK"
                    Case 301
                        msg = "Moved Permanently"
                    Case 302
                        msg = "Found"
                    Case 303
                        msg = "See Other"
                    Case 304
                        msg = "Not Modified"
                    Case 305
                        msg = "Use Proxy"
                    Case 306
                        msg = "Unused StatusCode (Deprecated)"
                    Case 307
                        msg = "Temporary Redirect"
                    Case 400
                        msg = "Bad Request"
                    Case 401
                        msg = "Unauthorized"
                    Case 402
                        msg = "Payment Required"
                    Case 403
                        msg = "Forbidden"
                    Case 404
                        msg = "Page Not Found"
                    Case 405
                        msg = "Method Not Allowed"
                    Case 406
                        msg = "Not Acceptable"
                    Case 407
                        msg = "Proxy Authentication Required"
                    Case 408
                        msg = "Request Timeout"
                    Case 409
                        msg = "Conflict"
                    Case 410
                        msg = "Gone"
                    Case 411
                        msg = "Length Required"
                    Case 412
                        msg = "Precondition Failed"
                    Case 413
                        msg = "Request Entity Too Large"
                    Case 414
                        msg = "Request-URI Too Long"
                    Case 415
                        msg = "Unsupported Media Type"
                    Case 416
                        msg = "Requested Range Not Satisfiable"
                    Case 417
                        msg = "Expectation Failed"
                    Case 500
                        msg = "Internal Server Error"
                    Case 501
                        msg = "Not Implemented"
                    Case 502
                        msg = "Bad Gateway"
                    Case 503
                        msg = "Service Unavailable"
                    Case 504
                        msg = "Gateway Timeout"
                    Case 505
                        msg = "HTTP Version Not Supported"
                End Select
                Return msg
            End Function
        End Class
    End Namespace
End Namespace