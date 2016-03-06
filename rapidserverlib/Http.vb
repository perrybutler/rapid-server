Namespace Http

    ''' <summary>
    ''' Global variables used throughout the library.
    ''' TODO: since the server and client apps use their own instance of the library, when the server sets these values,
    ''' the client does not see them but tries to use them anyway, which breaks the client functionality, move this into
    ''' the server and create another for the client
    ''' </summary>
    ''' <remarks></remarks>
    Public Module Module1
        
    End Module

    ''' <summary>
    ''' A site is simply a website defined in the config file.
    ''' </summary>
    ''' <remarks></remarks>
    Public Class Site
        Public Title As String
        Public Path As String
        Public Host As String
        Public Port As Integer
        Public RootPath As String
        Public RootUrl As String
        Public Socket As Net.Sockets.Socket
        Public Role As String
        Public Upstream As String
    End Class

    ''' <summary>
    ''' A simple http header is just an object which contains the key and value of the header. Used for cookies, since there might be multiple cookies and we can't add multiple items with the same key to a hashtable.
    ''' </summary>
    ''' <remarks></remarks>
    Public Class SimpleHttpHeader
        Public Key As String
        Public Value As String
    End Class

    ''' <summary>
    ''' A simple object for parsing raw http data into strings, inherited by Request and Response classes
    ''' </summary>
    ''' <remarks></remarks>
    Public Class SimpleRequestResponse
        Public Headers As New Hashtable
        Public Cookies As New ArrayList
        Public Content() As Byte
        Public ErrorMessage As String = ""
        Public HeaderString As String = ""
        Public ContentString As String = ""
        Public ContentStringLength As String = ""
        Public CacheAllowed As Boolean = True
        Sub Parse(ByVal payload As String)
            ' parse the responseString into header/content parts
            'Dim headerPart As String = ""
            Dim headerPart As New Text.StringBuilder
            Dim contentPart As String = ""
            Try
                Dim i As Integer = payload.IndexOf(vbCrLf & vbCrLf, StringComparison.Ordinal)
                ' TODO: this is very slow, use a string builder's substring instead
                'headerPart = payload.Substring(0, i).Trim
                headerPart.Append(payload, 0, i)
                contentPart = payload.Substring(i + 4, payload.Length - i - 4)
                Me.HeaderString = headerPart.ToString
                Me.ContentString = contentPart
                ' TODO: also parse headers from Request Body (for certain POST requests)
                Me.ContentStringLength = contentPart.Length
                Me.Content = Text.Encoding.ASCII.GetBytes(Me.ContentString)
            Catch ex As Exception
                ' couldn't parse the responseString as expected, probably an error
                'ErrorMessage = "Could not parse responseString for new SimpleResponse. responseString: " & responseString
                ErrorMessage = payload
                Me.Content = Text.Encoding.ASCII.GetBytes(ErrorMessage)
            End Try

            ' parse the header string
            Dim headerStringParts() As String = Me.HeaderString.Split(vbCrLf)
            For Each header As String In headerStringParts
                Dim headerClean As String = header.Trim()
                If headerClean <> "" Then
                    If headerClean.Contains(":") Then
                        Dim pos As Integer = headerClean.IndexOf(":")
                        Dim headerParts() As String = SplitFirst(headerClean, ":", True)
                        ' there can be multiple cookies, can't store them raw in our hashtable so use an arraylist and merge both before sending the response
                        If headerParts(0).ToLower = "set-cookie" Then
                            Dim h As New SimpleHttpHeader
                            h.Key = "Set-Cookie"
                            h.Value = headerParts(1)
                            Me.Cookies.Add(h)
                        End If
                        If headerParts(0).ToLower = "cache-control" Then
                            If headerParts(1).ToLower = "no-cache" Then
                                Me.CacheAllowed = False
                            End If
                        End If
                        ' only set the same header once
                        If Me.Headers.ContainsKey(headerParts(0)) = False Then
                            Me.Headers.Add(headerParts(0), headerParts(1))
                        End If
                    End If
                End If
            Next
        End Sub
    End Class

    ''' <summary>
    ''' Handlers are external processes or API calls that can be automated to do work or return results. Such handlers include PHP, ASP.NET, Windows Shell, etc. Interpreters are made available through custom definitions specified in the interpreters.xml file.
    ''' </summary>
    ''' <remarks></remarks>
    Public Class Handlers
        Private _handlers As New Hashtable
        Private _webRoot As String
        Sub Add(ByVal h As Handler)
            _handlers.Add(h.Name, h)
        End Sub
        Default Property Item(ByVal index As Integer) As Handler
            Get
                Return CType(_handlers.Values(index), Handler)
            End Get
            Set(ByVal value As Handler)

            End Set
        End Property
        Default Property Item(ByVal name As String) As Handler
            Get
                Return CType(_handlers(name), Handler)
            End Get
            Set(ByVal value As Handler)

            End Set
        End Property
    End Class

    Public Class Handler
        Public Name As String
        Public ExecutablePath As String
        Overridable Function HandleRequest(ByVal req As Http.Type1.Request) As String
            Return Nothing
        End Function
    End Class

    ''' <summary>
    ''' A CGI handler for .php scripts.
    ''' </summary>
    ''' <remarks></remarks>
    Public Class PhpCgiHandler
        Inherits Handler
        Sub New()
            Me.Name = "PhpCgi"
        End Sub
        Overrides Function HandleRequest(ByVal req As Http.Type1.Request) As String
            Dim results As String

            ' create the php process:
            Dim p As New Process
            p.StartInfo.FileName = ExecutablePath
            p.StartInfo.UseShellExecute = False
            p.StartInfo.RedirectStandardInput = True
            p.StartInfo.RedirectStandardOutput = True
            p.StartInfo.RedirectStandardError = True
            p.StartInfo.CreateNoWindow = True

            ' parse the host header from the request and set the env vars for cgi:
            Dim host As String = req.Headers("Host")
            Dim hostParts() As String = host.Split(":")
            p.StartInfo.EnvironmentVariables.Add("SCRIPT_FILENAME", req.AbsPath)
            p.StartInfo.EnvironmentVariables.Add("QUERY_STRING", req.QueryString.TrimStart("?"))
            p.StartInfo.EnvironmentVariables.Add("REQUEST_METHOD", req.Method)
            p.StartInfo.EnvironmentVariables.Add("REQUEST_URI", req.Uri)
            p.StartInfo.EnvironmentVariables.Add("HTTP_HOST", host)
            p.StartInfo.EnvironmentVariables.Add("SERVER_SOFTWARE", "Rapid Server")
            p.StartInfo.EnvironmentVariables.Add("SERVER_ADDR", hostParts(0))

            ' set the SERVER_PORT if it's available:
            If (hostParts.Length > 1) Then
                p.StartInfo.EnvironmentVariables.Add("SERVER_PORT", hostParts(1))
            End If
            ' make sure we pass cookies:
            If req.Headers.ContainsKey("Cookie") Then
                p.StartInfo.EnvironmentVariables.Add("HTTP_COOKIE", req.Headers("Cookie"))
            End If
            ' if its an http post, set the content_length and content_type headers:
            If req.Method = "POST" Then
                p.StartInfo.EnvironmentVariables.Add("CONTENT_LENGTH", req.ContentLength)
                p.StartInfo.EnvironmentVariables.Add("CONTENT_TYPE", "application/x-www-form-urlencoded")
            End If

            ' these are a "must" according to CGI spec:
            p.StartInfo.EnvironmentVariables.Add("SCRIPT_NAME", req.ScriptName) 'req.Uri & req.ScriptName)
            p.StartInfo.EnvironmentVariables.Add("GATEWAY_INTERFACE", "CGI/1.1")
            p.StartInfo.EnvironmentVariables.Add("SERVER_NAME", hostParts(0))
            p.StartInfo.EnvironmentVariables.Add("SERVER_PROTOCOL", "HTTP/1.1")
            p.StartInfo.EnvironmentVariables.Add("REMOTE_ADDR", hostParts(0))

            ' start the php process:
            p.Start()
            ' if its an http post, write the post data (aka querystring) to the input stream (aka stdin):
            If req.Method = "POST" Then
                p.StandardInput.Write(req.ContentString)
                p.StandardInput.Flush()
                p.StandardInput.Close()
            End If

            ' read the output stream (aka stdout) to get the processed request from php:
            ' TODO: ReadToEnd is thread blocking, we can use the async BeginOutputReadLine() function, but we must remember that we're 
            '   spawning a separate php-cgi.exe process (thus new thread) for every php request; I benchmarked them and found no 
            '   performance difference, no need to over-optimize this yet when our real goal is implementing a FastCGI handler...
            results = p.StandardOutput.ReadToEnd

            ' close the process (not really needed - garbage collector takes care of it)
            p.Close()
            p.Dispose()

            ' return the processed request to the calling function where we'll continue to handle it:
            Return results
        End Function
    End Class

    ''' <summary>
    ''' A FastCGI handler for .php scripts.
    ''' </summary>
    ''' <remarks></remarks>
    Public Class PhpFastCgiHandler

    End Class

    ''' <summary>
    ''' An ASP.NET handler. TODO: implement this using ISAPI dll or CGI/FastCGI (see Abyss Web Server)
    ''' </summary>
    ''' <remarks></remarks>
    Public Class AspDotNetHandler
        Inherits Handler
        Public Overrides Function HandleRequest(ByVal req As Type1.Request) As String
            Return Nothing
        End Function
    End Class
End Namespace

'UNDONE: maybe use this later
'Namespace FastCgi
'    Class Record
'        Public version As Char = "1"
'        Public type As Char = "1"
'        Public requestIdB1 As Char
'        Public requestIdB0 As Char
'        Public contentLengthB1 As Char
'        Public contentLengthB0 As Char
'        Public paddingLength As Char
'        Public reserved As Char
'        Public contentData() As Char
'        Public paddingData() As Char
'    End Class
'End Namespace