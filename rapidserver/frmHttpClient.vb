Public Class frmHttpClient
    Dim WithEvents client As New RapidServer.Http.Type1.Client(False)
    Delegate Sub HandleResponseDelegate(ByVal res As String)
    Delegate Sub ConnectFailedDelegate()
    Delegate Sub LogMessageDelegate(message As String)

    ' use invoke to handle server events in a thread safe way
    Private Sub client_ConnectFailed() Handles client.ConnectFailed
        Invoke(New ConnectFailedDelegate(AddressOf ConnectFailed))
    End Sub
    Private Sub client_HandleResponse(ByVal res As String) Handles client.HandleResponse
        Invoke(New HandleResponseDelegate(AddressOf HandleResponse), res)
    End Sub
    Private Sub client_LogMessage(message As String) Handles client.LogMessage
        Invoke(New HandleResponseDelegate(AddressOf LogMessage), message)
    End Sub

    ' form load
    Private Sub frmHttpClient_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        cboUrl.SelectedItem = cboUrl.Items(0)
        ComboBox1.SelectedItem = ComboBox1.Items(0)
    End Sub

    Sub DetectSystemInfo()
        If Chart1.Titles.Count = 1 Then
            Dim wmios As New Management.ManagementObjectSearcher("SELECT * FROM  Win32_OperatingSystem")
            Dim os = wmios.Get.Cast(Of Management.ManagementObject).First
            Dim osName As String = os("Name")
            If osName.Contains("Windows 7") Then
                osName = "Win7"
            End If
            osName = osName & " " & os("OSArchitecture")

            Dim wmicpu As New Management.ManagementObjectSearcher("SELECT * FROM  Win32_Processor")
            Dim cpu = wmicpu.Get.Cast(Of Management.ManagementObject).First
            Dim cpuName As String = cpu("Name")
            cpuName = cpuName.Replace("(R)", "").Replace("(TM)", "").Replace("CPU ", "")

            Dim wmiram As New Management.ManagementObjectSearcher("SELECT * FROM  Win32_ComputerSystem")
            Dim ram = wmiram.Get.Cast(Of Management.ManagementObject).First
            Dim totalRam As Integer = ram("TotalPhysicalMemory") / 1024 / 1024 / 1024
            Chart1.Titles.Add(osName & " - " & cpuName & " - " & totalRam & "GB")
        End If
    End Sub

    ' runs the benchmark tool with selected parameters
    Sub RunBenchmark()
        ' use a process to run the benchmark tool and read its results
        Dim results As String = ""
        Dim p As New Process
        p.StartInfo.CreateNoWindow = True
        p.StartInfo.UseShellExecute = False
        p.StartInfo.RedirectStandardOutput = True
        p.StartInfo.FileName = "ab"
        p.StartInfo.Arguments = "-n " & txtBenchmarkNumber.Text & " -c " & txtBenchmarkConcurrency.Text & " " & cboUrl.Text.TrimEnd("/") & "/"
        LogMessage("ab " & p.StartInfo.Arguments)
        p.Start()
        results = p.StandardOutput.ReadToEnd()
        p.Close()
        txtRaw.Text = results
        ' try to extract the requests per second value from the raw results
        Dim rps As String = ""
        Try
            Dim startTag As String = "Requests per second:    "
            Dim endTag As String = " [#/sec]"
            Dim i As Integer
            i = results.IndexOf(startTag)
            rps = results.Substring(i + startTag.Length)
            i = rps.IndexOf(endTag)
            rps = rps.Substring(0, i)
        Catch ex As Exception
            rps = "FAIL WHALE!"
        End Try
        TextBox1.AppendText(rps & vbCrLf)
        ' update the chart
        If Chart1.Series.IndexOf(cboUrl.Text) = -1 Then
            Chart1.Series.Add(cboUrl.Text)
        End If
        Chart1.Series(cboUrl.Text).Points.AddXY(0, rps)
    End Sub

    ' append message to log
    Sub LogMessage(message As String)
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
        txtRaw.Text = ""
        If TabControl1.SelectedTab.Text = "Benchmark" Then
            RunBenchmark()
        Else
            client.Go(cboUrl.Text)
            If cboUrl.Items.Contains(cboUrl.Text) Then

            Else
                cboUrl.Items.Add(cboUrl.Text)
            End If
        End If
    End Sub

    Private Sub chkWrapLog_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkWrapLog.CheckedChanged
        If chkWrapLog.Checked = True Then
            txtLog.WordWrap = True
        Else
            txtLog.WordWrap = False
        End If
    End Sub

    Private Sub btnDetectSystemInfo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDetectSystemInfo.Click
        DetectSystemInfo()
    End Sub
End Class