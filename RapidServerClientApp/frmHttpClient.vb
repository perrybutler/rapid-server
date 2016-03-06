Imports RapidServer

Public Class frmHttpClient
    Dim WithEvents client As New RapidServer.Http.Type1.Client(False)
    Public Sites As New Hashtable
    Public Tools As New Hashtable
    Public stdout As String
    Delegate Sub HandleResponseDelegate(ByVal res As String)
    Delegate Sub ConnectFailedDelegate()
    Delegate Sub LogMessageDelegate(ByVal message As String)
    ' use invoke to handle server events in a thread safe way
    Private Sub client_ConnectFailed() Handles client.ConnectFailed
        Invoke(New ConnectFailedDelegate(AddressOf ConnectFailed))
    End Sub
    Private Sub client_HandleResponse(ByVal res As String, state As Object) Handles client.HandleResponse
        Invoke(New HandleResponseDelegate(AddressOf HandleResponse), res)
    End Sub
    Private Sub client_LogMessage(ByVal message As String) Handles client.LogMessage
        Invoke(New LogMessageDelegate(AddressOf LogMessage), message)
    End Sub

    ' form load
    Private Sub frmHttpClient_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ' load the config
        LoadConfig()
        ' add the sites
        For Each s As Site In Me.Sites.Values
            cboUrl.Items.Add(s.Url)
        Next
        cboUrl.SelectedItem = cboUrl.Items(0)
        ' add the tools
        For Each t As Tool In Me.Tools.Values
            cboBenchmarkTool.Items.Add(t.Name)
            cboBenchmarkTool2.Items.Add(t.Name)
            cboBenchmarkTool3.Items.Add(t.Name)
        Next
        cboBenchmarkTool.SelectedItem = cboBenchmarkTool.Items(0)
        cboBenchmarkTool2.SelectedItem = cboBenchmarkTool2.Items(0)
        cboBenchmarkTool3.SelectedItem = cboBenchmarkTool3.Items(0)
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
        If IO.File.Exists("client.xml") = False Then CreateConfig()
        Dim cfg As New Xml.XmlDocument
        Try
            cfg.Load("client.xml")
        Catch ex As Exception
            ' TODO: we need to notify the user that the config couldn't be loaded instead of just dying...
            Console.WriteLine(ex.Message)
            Exit Sub
        End Try
        Dim root As Xml.XmlNode = cfg("Settings")
        ' parse the sites:
        For Each n As Xml.XmlNode In root("Sites")
            Dim s As New Site
            s.Name = n("Name").GetValue
            s.Description = n("Description").GetValue
            s.Url = n("Url").GetValue
            Me.Sites.Add(s.Name, s)
        Next
        ' parse the tools:
        For Each n As Xml.XmlNode In root("Tools")
            Dim t As New Tool
            t.Name = n("Name").GetValue
            t.Path = n("Path").GetValue
            t.Speed = n("Speed").GetValue
            t.Time = n("Time").GetValue
            For Each nn As Xml.XmlNode In n("Data")
                If nn.Name = "RPS" Then
                    t.Data.RPS = nn.InnerText
                ElseIf nn.Name = "CompletedRequests" Then
                    t.Data.CompletedRequests = nn.InnerText
                ElseIf nn.Name = "ResponseTime" Then
                    t.Data.ResponseTime = nn.InnerText
                End If
            Next
            Me.Tools.Add(t.Name, t)
        Next
    End Sub

    Sub CreateConfig()

    End Sub

    Sub DetectSystemInfo()
        If Chart1.Titles.Count = 1 Then
            ' get os
            Dim wmios As New Management.ManagementObjectSearcher("SELECT * FROM  Win32_OperatingSystem")
            Dim os = wmios.Get.Cast(Of Management.ManagementObject).First
            Dim osName As String = os("Name")
            If osName.Contains("Windows 7") Then
                osName = "Win7"
            End If
            osName = osName & " " & os("OSArchitecture").trim
            ' get cpu
            Dim wmicpu As New Management.ManagementObjectSearcher("SELECT * FROM  Win32_Processor")
            Dim cpu = wmicpu.Get.Cast(Of Management.ManagementObject).First
            Dim cpuName As String = cpu("Name")
            cpuName = cpuName.Replace("(R)", "").Replace("(r)", "").Replace("(TM)", "").Replace("(tm)", "").Replace("CPU ", "").Trim
            ' get ram
            Dim wmiram As New Management.ManagementObjectSearcher("SELECT * FROM  Win32_ComputerSystem")
            Dim ram = wmiram.Get.Cast(Of Management.ManagementObject).First
            Dim totalRam As Integer = ram("TotalPhysicalMemory") / 1024 / 1024 / 1024
            ' print results to chart title
            Chart1.Titles.Add(osName & " - " & cpuName & " - " & totalRam & "GB")
            Chart3.Titles.Add(osName & " - " & cpuName & " - " & totalRam & "GB")
            Chart2.Titles.Add(osName & " - " & cpuName & " - " & totalRam & "GB")
        End If
    End Sub

    ' runs the benchmark tool with selected parameters
    Sub RunBenchmark()
        If TabControl3.SelectedTab.Text = "Speed" Then
            SpeedBenchmark()
        ElseIf TabControl3.SelectedTab.Text = "Time" Then
            TimeBenchmark()
        Else

        End If
    End Sub

    ' TODO: this gets an unhandled exception when it tries to parse data that doesn't exist, we shouldn't assume 
    '   we'll always have the data and use a try...catch here with error reporting
    Private Function SubstringBetween(ByVal s As String, ByVal startTag As String, ByVal endTag As String) As String
        Try
            Dim ss As String = ""
            Dim i As Integer
            If startTag.ToLower = "vbcrlf" Then startTag = vbCrLf
            If endTag.ToLower = "vbcrlf" Then endTag = vbCrLf
            i = s.IndexOf(startTag)
            ss = s.Substring(i + startTag.Length)
            i = ss.IndexOf(endTag)
            ss = ss.Substring(0, i)
            Return ss.Trim
        Catch ex As Exception
            Console.WriteLine(ex.Message)
            Return ""
        End Try        
    End Function

    Private Function ParseAny(ByVal s As String) As String()
        If s = "" Then
            Return New String() {"FAIL WHALE!"}
        End If
        Dim results(0) As String
        Dim spl() As String = s.Split(",")
        If spl(0) = "stdout" Then
            ' data is in stdout
            If spl(1) = "between" Then
                results(0) = SubstringBetween(Me.stdout, spl(2), spl(3))
            End If
        Else
            ' data is in a file
            Dim f As New IO.StreamReader(spl(0))
            Dim delim As String = ""
            If spl(1) = "tabs" Then
                delim = vbTab
            Else
                delim = ","
            End If

            ' determine if the tool data contains a formula
            Dim formula() As String = Nothing
            Dim useFormula As Boolean = False
            If spl(3).Contains("+") Then
                ' the data we want requires a formula rather than a single value
                formula = spl(3).Split("+")
                useFormula = True
            End If

            ' TODO: check if we should read the file as rows or as summary...

            ' read the file as rows
            Dim lines As New ArrayList
            While f.Peek <> -1
                lines.Add(f.ReadLine)
            End While
            f.Close()
            f.Dispose()

            ' filter the rows
            If spl(2) = "first" Then
                ' grab the first row only
                For i As Integer = 0 To lines.Count - 1
                    lines.RemoveAt(1)
                Next
                Dim line As String = lines(0)
                Dim fields() As String = line.Split(delim)
                If useFormula = True Then
                    Dim val As Integer = 0
                    For i As Integer = 0 To formula.Length - 1
                        val += fields(formula(i))
                    Next
                    results(0) = val
                Else
                    results(0) = fields(spl(3)).Trim
                End If
            ElseIf spl(2) = "last" Then
                ' grab the last row only
                For i As Integer = 0 To lines.Count - 2
                    lines.RemoveAt(0)
                Next
                Dim line As String = lines(0)
                Dim fields() As String = line.Split(delim)
                If useFormula = True Then
                    Dim val As Integer = 0
                    For i As Integer = 0 To formula.Length - 1
                        val += fields(formula(i))
                    Next
                    results(0) = val
                Else
                    results(0) = fields(spl(3)).Trim
                End If
            Else
                ' grab all the rows after a specific row index
                For i As Integer = 0 To spl(2) - 1
                    lines.RemoveAt(i)
                Next
                For i As Integer = 0 To lines.Count - 1
                    Dim line As String = lines(i)
                    Dim fields() As String = line.Split(delim)
                    If useFormula = True Then
                        Dim val As Integer = 0
                        For ii As Integer = 0 To formula.Length - 1
                            val += fields(formula(ii))
                        Next
                        results(results.Length - 1) = val
                    Else
                        results(results.Length - 1) = fields(spl(3)).Trim
                    End If
                    ReDim Preserve results(results.Length)
                Next
            End If
        End If
        Return results
    End Function

    Sub ParseResults(ByVal results As String)
        Me.stdout = results
        Dim currentTool As Tool = Me.Tools(cboBenchmarkTool.Text)
        ' parse it
        Dim requestsPerSecond() As String = ParseAny(currentTool.Data.RPS)
        Dim completedRequests() As String = ParseAny(currentTool.Data.CompletedRequests)
        'Dim time() As String = ParseAny(currentTool.Data.ResponseTime)
        ' chart it

        Dim failedParse As Boolean = False
        If requestsPerSecond(0) = "" Or completedRequests(0) = "" Then
            failedParse = True
        End If

        If failedParse = False Then
            Dim currentSite As Site = Nothing
            ' get the name of the site
            For Each s As Site In Me.Sites.Values
                If s.Url = cboUrl.Text Then
                    currentSite = s
                End If
            Next

            Dim seriesName As String = cboUrl.Text
            If currentSite IsNot Nothing Then seriesName = currentSite.Name

            ' update the rps log
            ' TODO: remove sitename and match text color to legend color
            TextBox1.AppendText(seriesName & " - " & requestsPerSecond(0) & vbCrLf)

            ' plot the requests completed value to the bar chart
            If Chart1.Series.IndexOf(seriesName) = -1 Then
                Chart1.Series.Add(seriesName)
            End If
            Chart1.Series(seriesName).Points.AddXY(0, requestsPerSecond(0))

            ' plot the requests completed value to the bar chart
            If Chart2.Series.IndexOf(seriesName) = -1 Then
                Chart2.Series.Add(seriesName)
            End If
            Chart2.Series(seriesName).Points.AddXY(0, completedRequests(0))

            ' plot the gnuplot data to the line graph
            If Chart3.Series.IndexOf(seriesName) = -1 Then
                ' create the series for this url
                Chart3.Series.Add(seriesName)
                Chart3.Series(seriesName).ChartType = DataVisualization.Charting.SeriesChartType.FastLine
            Else
                ' series was already plotted, clear the series and replot it
                Chart3.Series(seriesName).Points.Clear()
            End If
            'For Each s As String In time
            '    Chart3.Series(seriesName).Points.AddXY(0, s)
            'Next
        Else
            ' update the rps log
            TextBox1.AppendText("FAIL WHALE!" & vbCrLf)
        End If
    End Sub

    Sub TimeBenchmark()
        Dim t As Tool = Me.Tools(cboBenchmarkTool.Text)
        Dim cmd As String = t.Time
        cmd = cmd.Replace("%time", txtBenchmarkDuration.Text)
        cmd = cmd.Replace("%url", cboUrl.Text.TrimEnd("/") & "/")
        LogMessage(t.Path & cmd)
        Dim p As New ManagedProcess(t.Path, cmd)
        txtRaw.Text = p.Output.ToString
        ParseResults(p.Output.ToString)
    End Sub

    Sub SpeedBenchmark()
        Dim t As Tool = Me.Tools(cboBenchmarkTool.Text)
        Dim cmd As String = t.Speed
        cmd = cmd.Replace("%num", txtBenchmarkNumber.Text)
        cmd = cmd.Replace("%conc", txtBenchmarkConcurrency.Text)
        cmd = cmd.Replace("%url", cboUrl.Text.TrimEnd("/") & "/")
        LogMessage(t.Path & cmd)
        Dim p As New ManagedProcess(t.Path, cmd)
        ' TODO: this throws an exception due to multithreading...
        '   https://stackoverflow.com/questions/24181910/stringbuilder-thread-safety?rq=1
        txtRaw.Text = p.Output.ToString
        ParseResults(p.Output.ToString)
    End Sub

    ' ramp by increasing concurrency each iteration: http://wiki.dreamhost.com/Web_Server_Performance_Comparison
    Sub RampBenchmark()

    End Sub

    ' append message to log
    Sub LogMessage(ByVal message As String)
        ' prepare the date
        Dim clrDate As String = ""
        clrDate = Date.Now.ToString("dd/MMM/yyyy:hh:mm:ss zzz")
        clrDate = clrDate.Remove(clrDate.LastIndexOf(":"), 1)
        ' log access events using CLF (combined log format):
        txtLog.AppendText("127.0.0.1")
        txtLog.AppendText(" -") ' remote log name - leave null for now
        txtLog.AppendText(" -") ' client username - leave null for now
        txtLog.AppendText(" [" & clrDate & "]")
        txtLog.AppendText(" """ & message.Replace(vbCrLf, " ").TrimEnd(" "))
        txtLog.AppendText("""")
        txtLog.AppendText(vbCrLf)
    End Sub

    ' connect to server failed (invoked server event)
    Sub ConnectFailed()
        txtRaw.Text = "Could not connect." & vbCrLf
        txtLog.Text &= "Could not connect." & vbCrLf
    End Sub

    ' response is being handled by the server (invoked server event)
    Sub HandleResponse(ByVal res As String)
        txtRaw.Text &= res
    End Sub

    Private Sub btnGo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGo.Click
        btnGo.Enabled = False
        txtRaw.Text = ""
        If TabControl1.SelectedTab.Text = "Benchmark" Then
            RunBenchmark()
        Else
            client.Go(cboUrl.Text, Nothing)
            If cboUrl.Items.Contains(cboUrl.Text) Then

            Else
                cboUrl.Items.Add(cboUrl.Text)
            End If
        End If
        btnGo.Enabled = True
    End Sub

    Private Sub chkWrapLog_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkWrapLog.CheckedChanged
        If chkWrapLog.Checked = True Then
            txtLog.WordWrap = True
        Else
            txtLog.WordWrap = False
        End If
    End Sub

    Private Sub btnDetectSystemInfo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDetectSystemInfo.Click
        DetectSystemInfo()
    End Sub

    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        While Chart1.Series.Count > 0
            Chart1.Series.RemoveAt(0)
        End While
        While Chart3.Series.Count > 0
            Chart3.Series.RemoveAt(0)
        End While
        While Chart2.Series.Count > 0
            Chart2.Series.RemoveAt(0)
        End While
        TextBox1.Text = ""
    End Sub

    Private Sub cboBenchmarkTool_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboBenchmarkTool.SelectedIndexChanged, cboBenchmarkTool2.SelectedIndexChanged
        Dim t As Tool = Me.Tools(cboBenchmarkTool.Text)
        If t.Speed.Contains("%num") Then
            txtBenchmarkNumber.Enabled = True
        Else
            txtBenchmarkNumber.Enabled = False
        End If

        If t.Speed.Contains("%conc") Then
            txtBenchmarkConcurrency.Enabled = True
        Else
            txtBenchmarkConcurrency.Enabled = False
        End If

        If t.Time.Contains("%time") Then
            txtBenchmarkDuration.Enabled = True
        Else
            txtBenchmarkDuration.Enabled = False
        End If

        cboBenchmarkTool.SelectedIndex = CType(sender, ComboBox).SelectedIndex
        cboBenchmarkTool2.SelectedIndex = CType(sender, ComboBox).SelectedIndex
        cboBenchmarkTool3.SelectedIndex = CType(sender, ComboBox).SelectedIndex
    End Sub
End Class



Class DataPoint
    Public Topics As New Hashtable
    ' add each topic to the hashtable with its own key
    ' ctime = 0
    ' dtime = 2
    ' etc..
End Class

Class ManagedProcess
    Public Process As New Process
    Public Output As New Text.StringBuilder
    Sub New(ByVal filename, ByVal commandline)
        ' use a process to run the benchmark tool and read its results
        Dim results As String = ""
        Dim p As Process = Me.Process
        AddHandler p.OutputDataReceived, AddressOf ReadOutputAsync
        p.StartInfo.CreateNoWindow = True
        p.StartInfo.UseShellExecute = False
        p.StartInfo.RedirectStandardOutput = True
        p.StartInfo.RedirectStandardError = True
        p.StartInfo.FileName = filename
        p.StartInfo.Arguments = commandline
        Try
            p.Start()
            p.BeginOutputReadLine()
            ' TODO: siege -c1000 causes a hang with WaitForExit() and no timeout...
            p.WaitForExit()
            'p.Close()
            'p.Dispose()
        Catch ex As Exception
            Me.Output.Append("the tool process failed to run")
        End Try
    End Sub
    Sub ReadOutputAsync(ByVal sender As Object, ByVal e As DataReceivedEventArgs)
        Output.AppendLine(e.Data)
    End Sub
End Class

Class Site
    Public Name As String
    Public Description As String
    Public Url As String
End Class

Class Tool
    Public Name As String
    Public Path As String
    Public Speed As String
    Public Time As String
    Public Data As New ToolData
End Class

Class ToolData
    Public RPS As String
    Public CompletedRequests As String
    Public ResponseTime As String
End Class