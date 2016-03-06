' ==================================
' RAPID SERVER
' ==================================
' A client/server communications library for building or serving high performance network applications, websites and webapps.

Option Explicit On
Option Strict On

''' <summary>
''' Global enums and helper methods for use anywhere in the library.
''' </summary>
''' <remarks></remarks>
Public Module Globals
    ''' <summary>
    ''' Types of compression methods.
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum CompressionMethod
        None = 0
        Gzip = 1
        Deflate = 2
    End Enum

    ''' <summary>
    ''' The transfer method used for sending data to a client.
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum TransferMethod
        StoreAndForward = 0 ' HTTP/1.0 - offers the "Connection: close" header, but generally HTTP/1.0 does not support persistent connections so we should always close the socket whether or not this header is present. Content-Length can be omitted as long as the socket is always closed after the response has been sent.
        ChunkedEncoding = 1 ' HTTP/1.1 - offers the "Transfer-Encoding: chunked" header as a replacement to "Content-Length: xxx" header for streaming data without having to buffer it and determine its size for Content-Length
    End Enum

    ''' <summary>
    ''' Defines an http mimetype.
    ''' </summary>
    ''' <remarks></remarks>
    Public Class MimeType
        Public Name As String = ""                  ' e.g. text/html | application/javascript
        Public FileExtension As String = ""         ' e.g. jpg,jpeg | txt | htm,html
        Public Compress As CompressionMethod        ' e.g. none | gzip | deflate
        Public Expires As String = ""               ' e.g. access plus 1 month | (DATE)
        Public Handler As String = ""               ' e.g. PhpCgi | AspDotNet
    End Class

    ''' <summary>
    ''' Kills all running processes matching the procName.
    ''' </summary>
    ''' <param name="procName"></param>
    ''' <remarks></remarks>
    Public Sub KillAll(ByVal procName As String)
        'Process.Start("taskkill.exe /f /im " & procName)
        ' terminate all running php-cgi.exe FastCGI daemons
        Dim TaskKill As New Process
        TaskKill.StartInfo.CreateNoWindow = True
        TaskKill.StartInfo.UseShellExecute = False
        TaskKill.StartInfo.FileName = "taskkill.exe"
        TaskKill.StartInfo.Arguments = "/F /IM " & procName
        TaskKill.Start()
        ' wait for the process to finish, otherwise if we try to start a new instance before TaskKill has spun up/down it will kill our new instance too
        TaskKill.WaitForExit()
    End Sub

    Public Function SplitFirst(ByVal input As String, ByVal delimiter As String, ByVal trim As Boolean) As Array
        Dim spl() As String
        Dim parts(1) As String
        spl = input.Split(CChar(delimiter))
        If trim = True Then
            For i = 0 To spl.Length - 1
                spl(i) = spl(i).Trim
            Next
        End If
        parts(0) = spl(0)
        parts(1) = String.Join(delimiter, spl, 1, spl.Length - 1)
        Return parts
    End Function

    ''' <summary>
    ''' Converts the given ip address and port into an endpoint that can be used with a socket.
    ''' </summary>
    ''' <param name="IP"></param>
    ''' <param name="Port"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function AddressToEndpoint(ByVal IP As String, ByVal Port As Integer) As Net.IPEndPoint
        Dim ipAddress As Net.IPAddress = Net.IPAddress.Parse(IP)
        Dim endPoint As Net.IPEndPoint = New Net.IPEndPoint(ipAddress, Port)
        Return endPoint
    End Function

    Public Class AsyncReceiveState
        Public Site As RapidServer.Http.Site
        Public Socket As System.Net.Sockets.Socket
        Public Buffer() As Byte
        Public PacketBufferStream As New IO.MemoryStream ' a buffer for appending received data to build the packet
        Public Packet As String
        Public ReceiveSize As Integer ' the size (in bytes) of the Packet
        Public TotalBytesReceived As Integer ' the total bytes received for the Packet so far
        Public ContentOffset As Integer
        Public State As Object
        Sub New(ByVal argBufferSize As Integer, argState As Object)
            ReDim Me.Buffer(argBufferSize - 1)
            Me.State = argState
        End Sub
    End Class

    Public Class AsyncSendState
        Public Socket As System.Net.Sockets.Socket
        Public BytesToSend() As Byte
        Public Progress As Integer
        Public Tag As String
        Public Persistent As Boolean
        Public BufferSize As Integer
        Public State As Object
        Sub New(ByVal argSocket As System.Net.Sockets.Socket, ByVal argBufferSize As Integer, argState As Object)
            Me.Socket = argSocket
            Me.BufferSize = argBufferSize
            Me.State = argState
        End Sub
        Function NextOffset() As Integer
            Return Progress
        End Function
        Function NextLength() As Integer
            If BytesToSend.Length - Progress > Me.BufferSize Then
                Return Me.BufferSize
            Else
                Return BytesToSend.Length - Progress
            End If
        End Function
    End Class

    ''' <summary>
    ''' Extends the XmlNode class to return an empty string instead of null.
    ''' </summary>
    ''' <param name="x"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <System.Runtime.CompilerServices.Extension()> _
    Public Function GetValue(ByVal x As Xml.XmlNode) As String
        If x Is Nothing Then
            Return ""
        Else
            Return x.InnerText
        End If
    End Function

    ''' <summary>
    ''' Extends the String class.
    ''' </summary>
    ''' <param name="s"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <System.Runtime.CompilerServices.Extension()> _
    Public Function Slice(ByVal s As String, ByVal firstOccurrenceOf As String) As String()
        If firstOccurrenceOf Is Nothing Then
            Return Nothing
        Else
            Dim spl() As String = s.Split(CChar(firstOccurrenceOf))
            Dim firstSlice As String = spl(0)
            Dim secondSlice As String = ""
            For i As Integer = 1 To spl.Length - 1
                secondSlice &= "/" & spl(i)
            Next
            Return New String() {firstSlice, secondSlice}
        End If
    End Function

    ''' <summary>
    ''' Extends the String class.
    ''' </summary>
    ''' <param name="s"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <System.Runtime.CompilerServices.Extension()> _
    Public Function SubstringEx(ByVal s As String, ByVal s1 As String, ByVal s2 As String) As String
        Dim ret As String = ""
        Dim i1 As Integer
        Dim i2 As Integer
        i1 = s.IndexOf(s1) + s1.Length
        i2 = s.IndexOf(s2, i1)
        If s.Contains(s1) = True Then
            ret = s.Substring(i1, i2 - i1)
        End If
        Return ret
    End Function

    ''' <summary>
    ''' The type of event to be logged. Some events will be enabled/disabled in the LogEvent method, depending on our current debugging goals.
    ''' </summary>
    ''' <remarks></remarks>
    Enum DebugMessageType
        InfoMessage = 0
        WarningMessage = 1
        ErrorMessage = 2
        UsageMessage = 3
        UnhandledMessage = 4
    End Enum

    ''' <summary>
    ''' Logs the event to a console or file. Rather than use Console.WriteLine throughout the library we call LogEvent instead, which can be enabled or disabled in one step. Since Console.WriteLine is useful for debugging but also performance-heavy, we need the ability to enable/disable it at will.
    ''' </summary>
    ''' <param name="message"></param>
    ''' <remarks></remarks>
    Sub DebugMessage(ByVal message As String, Optional ByVal level As DebugMessageType = DebugMessageType.InfoMessage, Optional ByVal caller As String = "", Optional ByVal internalException As String = "")
        ' TODO: implement the caller, for logging purposes and easy bug reporting
        ' TODO: this can slow down the server dramatically
        Select Case level
            Case DebugMessageType.InfoMessage
                Console.WriteLine(message)
            Case DebugMessageType.WarningMessage
                Console.WriteLine(message)
            Case DebugMessageType.ErrorMessage
                Console.WriteLine(message)
            Case DebugMessageType.UsageMessage
                'Console.WriteLine(message)
        End Select
    End Sub
End Module