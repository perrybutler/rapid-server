Namespace Http
    Namespace Type1
        ''' <summary>
        ''' An HTTP client.
        ''' </summary>
        ''' <remarks></remarks>
        Public Class Client
            Public SendBufferSize As Integer = 4096
            Public ReceiveBufferSize As Integer = 4096

            Private _clientSocket As Net.Sockets.Socket
            Private _request As String
            Private _req As UriBuilder
            Private _keepAlive As Boolean
            Event HandleResponse(ByVal res As String, state As Object)
            Event ConnectSucceeded()
            Event ConnectFailed()
            Event LogMessage(ByVal message As String)

            Sub New(ByVal keepAlive As Boolean)
                _keepAlive = keepAlive
            End Sub

            ''' <summary>
            ''' Issues an HTTP GET request to the URL specified in the textbox.
            ''' </summary>
            ''' <param name="url"></param>
            ''' <remarks></remarks>
            Sub Go(ByVal url As String, state As Object)
                ' use uribuilder to format the url:
                Connect(New UriBuilder(url), state)
            End Sub

            Function GetHostIP(ByVal uri As UriBuilder) As String
                Dim hostAddress As String = ""
                Dim ipExists As System.Net.IPAddress = Nothing
                If System.Net.IPAddress.TryParse(uri.Host, ipExists) Then
                    ' localhost
                    hostAddress = uri.Host
                    _request = uri.Path
                Else
                    ' TODO: this could halt with an error if the host doesn't exist (we should return name_not_resolved)
                    Dim hostEntry As System.Net.IPHostEntry
                    hostEntry = System.Net.Dns.GetHostEntry(uri.Host)
                    For Each ip As System.Net.IPAddress In hostEntry.AddressList
                        hostAddress = ip.ToString
                    Next
                End If
                Return hostAddress
            End Function

            Sub Connect(ByVal req As UriBuilder, state As Object)
                ' store the request in a global so we can use it during async callbacks
                _req = req
                ' extract ip address from _req or _req Url/Host
                Dim ip As String = GetHostIP(req)
                Dim port As Integer = req.Port
                ' create endpoint
                Dim endPoint As Net.IPEndPoint = AddressToEndpoint(ip, port)
                _clientSocket = New Net.Sockets.Socket(Net.Sockets.AddressFamily.InterNetwork, Net.Sockets.SocketType.Stream, Net.Sockets.ProtocolType.Tcp)
                ' connect to server async
                Try
                    _clientSocket.BeginConnect(endPoint, New AsyncCallback(AddressOf AsyncClientConnected), New AsyncSendState(_clientSocket, Me.SendBufferSize, state))
                Catch ex As Exception
                    DebugMessage("Could not connect to server.", DebugMessageType.ErrorMessage, "Connect", ex.Message)
                    RaiseEvent ConnectFailed()
                End Try
            End Sub

            Sub AsyncClientConnected(ByVal ar As IAsyncResult)
                ' get the async state object returned by the callback
                Dim asyncState As AsyncSendState = CType(ar.AsyncState, AsyncSendState)
                Dim state As Object = asyncState.State
                ' end the async connection request so we can check if we are connected to the server
                Dim connectSuccessful As Boolean = False
                Try
                    ' call the EndConnect method which will succeed or throw an error depending on the result of the connection
                    asyncState.Socket.EndConnect(ar)
                    ' at this point, the EndConnect succeeded and we are connected to the server! handle the success outside this Try block.
                    connectSuccessful = True
                    RaiseEvent ConnectSucceeded()
                Catch ex As Exception
                    ' at this point, the EndConnect failed and we are NOT connected to the server!
                    DebugMessage("Could not connect to the server.", DebugMessageType.ErrorMessage, "Connect", ex.Message)
                    RaiseEvent ConnectFailed()
                End Try

                ' if the client has connected, proceed
                If connectSuccessful = True Then
                    ' start waiting for messages from the server
                    Dim receiveState As New AsyncReceiveState(Me.ReceiveBufferSize, state)
                    receiveState.Socket = asyncState.Socket
                    receiveState.Socket.BeginReceive(receiveState.Buffer, 0, ReceiveBufferSize, Net.Sockets.SocketFlags.None, New AsyncCallback(AddressOf DataReceived), receiveState)
                    ' make a request to the server
                    Dim sendState As New AsyncSendState(asyncState.Socket, Me.SendBufferSize, state)
                    ' if the path is a directory, ensure it has a trailing /
                    'If IO.Path.GetExtension(_request) = "" Then
                    '    If _request.EndsWith("/") = False Then
                    '        _request &= "/"
                    '    End If
                    'End If

                    ' construct the GET request string and byte array:
                    ' NOTE: node.js requires two vbCrLf terminator where other servers only require one. IIS 7.5 requires HTTP/1.1 and Host 
                    '   header or will not return headers with the response.
                    Dim reqString As String = ""
                    Dim reqBytes() As Byte = Nothing
                    reqString = "GET " & _req.Path & " HTTP/1.1" & vbCrLf & "Host: " & _req.Host & vbCrLf & vbCrLf
                    reqBytes = System.Text.Encoding.ASCII.GetBytes(reqString)

                    ' send the reqBytes data to the server
                    RaiseEvent LogMessage(reqString)
                    sendState.Socket.BeginSend(reqBytes, 0, reqBytes.Length, Net.Sockets.SocketFlags.None, New AsyncCallback(AddressOf DataSent), sendState)
                End If
            End Sub

            Sub Disconnect()
                _clientSocket.Disconnect(False)
            End Sub

            Sub DataSent(ByVal ar As IAsyncResult)
                ' get the async state object returned by the callback
                Dim asyncState As AsyncSendState = CType(ar.AsyncState, AsyncSendState)
                Try
                    asyncState.Socket.EndSend(ar)
                Catch ex As Exception
                    Console.WriteLine("DataSent exception: " & ex.Message)
                End Try
            End Sub

            ''' <summary>
            ''' This callback fires when the client socket receives data (an HTTP response) asynchronously from the server.
            ''' </summary>
            ''' NOTE:
            '''     Since HTTP is by nature a stream, a response will be sent by the server which is broken down into pieces per the
            '''     server's configured SendBufferSize, so we must continue issuing BeginReceive's on the socket until the end of the stream. 
            '''     We can properly detect the end of stream per the HTTP spec which states that the Content-Length header should be used to stop 
            '''     issuiing BeginReceive's when the total bytes received equals the Header + Content length, or in the case of a 
            '''     "Transfer-Encoding: chunked" header we look for a null character which signals termination of the chunked stream.
            ''' <param name="ar"></param>
            ''' <remarks></remarks>
            Sub DataReceived(ByVal ar As IAsyncResult)
                ' get the async state object returned by the callback
                Dim asyncState As AsyncReceiveState = CType(ar.AsyncState, AsyncReceiveState)
                Dim responseChunk As String = System.Text.Encoding.ASCII.GetString(asyncState.Buffer).TrimEnd(vbNullChar)
                Dim responseString As String = asyncState.Packet & responseChunk

                ' if we haven't determined the Content-Length yet, try doing so now by attempting to extract it from the responseChunk:
                ' TODO: this halts on an error when we try a random URL.
                ' TODO: we need to handle the various transfer types here...check for chunked encoding and parse the size etc...
                If asyncState.ReceiveSize = 0 Then
                    Dim contentLength As String = ""
                    Dim transferEncoding As String = ""
                    contentLength = responseChunk.SubstringEx("Content-Length: ", vbCrLf)
                    asyncState.ReceiveSize = contentLength
                End If

                ' if we haven't determined the Content offset yet, try doing so now. content is located after the header and two newlines (crlf) which is 4 bytes.
                If asyncState.ContentOffset = 0 Then
                    Dim contentOffset As Integer
                    contentOffset = responseChunk.IndexOf(vbCrLf & vbCrLf) + 4
                    asyncState.ContentOffset = contentOffset
                End If

                ' add the responseChunk's length to the total received bytes count:
                asyncState.TotalBytesReceived += responseChunk.Length

                ' if we haven't received all the bytes yet, issue another BeginReceive, otherwise we have all the data so we handle the response
                If asyncState.TotalBytesReceived - asyncState.ContentOffset < asyncState.ReceiveSize Then
                    Dim receiveState As New AsyncReceiveState(Me.ReceiveBufferSize, asyncState.State)
                    receiveState.Socket = asyncState.Socket
                    receiveState.Packet = responseString
                    receiveState.ReceiveSize = asyncState.ReceiveSize
                    receiveState.TotalBytesReceived = asyncState.TotalBytesReceived
                    receiveState.Socket.BeginReceive(receiveState.Buffer, 0, ReceiveBufferSize, Net.Sockets.SocketFlags.None, New AsyncCallback(AddressOf DataReceived), receiveState)
                Else
                    RaiseEvent HandleResponse(responseString, asyncState.State)
                End If
            End Sub
        End Class
    End Namespace
End Namespace

