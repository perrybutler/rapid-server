Public Class frmHttpServer
    Dim WithEvents server As New RapidServer.Http.Type1.Server()
    Delegate Sub HandleRequestDelegate(ByVal req As RapidServer.Http.Type1.Request, ByVal res As RapidServer.Http.Type1.Response)

    Sub SpawnClient()
        Dim f As New frmHttpClient
        f.Show()
    End Sub

    Sub HandleRequest(ByVal req As RapidServer.Http.Type1.Request, ByVal res As RapidServer.Http.Type1.Response)
        ' prepare the date
        Dim clrDate As String = ""
        clrDate = Date.Now.ToString("dd/MMM/yyyy:hh:mm:ss zzz")
        clrDate = clrDate.Remove(clrDate.LastIndexOf(":"), 1)
        ' log access events using CLF (combined log format):
        'If TextBox1.Text <> "" Then TextBox1.AppendText(vbCrLf)
        txtLog.AppendText(req.clientAddress)
        txtLog.AppendText(" -") ' remote log name - leave null for now
        txtLog.AppendText(" -") ' client username - leave null for now
        txtLog.AppendText(" [" & clrDate & "]")
        ' TextBox1.AppendText(" """ & req.RequestString.Replace(vbCrLf, "").Trim)
        txtLog.AppendText(" """ & req.RequestLine)
        txtLog.AppendText("""")
        txtLog.AppendText(" " & res.StatusCode)
        txtLog.AppendText(" " & res.ContentLength)
        txtLog.AppendText(vbCrLf)
    End Sub

    Sub PopulateServerInfo()
        ' update the info tab
        LinkLabel1.Text = server.Sites(cboServer.Text).RootPath
        LinkLabel2.Text = server.Sites(cboServer.Text).RootUrl
    End Sub

    Sub StartServerByName(ByVal name As String)
        'Dim s As RapidServer.Http.Site = server.Sites(name)
        'server.StartServer()
    End Sub

    Private Sub frmHttpServer_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated

    End Sub

    Private Sub frmServer_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Width = 360 '263
        Me.Height = 103
        ' start server
        server.StartServer()
        ' put the available sites into the combobox:
        For Each s As RapidServer.Http.Site In server.Sites.Values
            cboServer.Items.Add(s.Title)
        Next
        cboServer.SelectedIndex = 0
        ' update the info tab
        PopulateServerInfo()
        ' spawn a client for testing
        SpawnClient()
        
    End Sub

    Private Sub server_HandleRequest(ByVal req As RapidServer.Http.Type1.Request, ByVal res As RapidServer.Http.Type1.Response, ByVal client As System.Net.Sockets.Socket) Handles server.HandleRequest
        If chkEnableLog.Checked = True Then
            Invoke(New HandleRequestDelegate(AddressOf HandleRequest), New Object() {req, res})
        End If
    End Sub

    Private Sub server_ServerShutdown() Handles server.ServerShutdown
        btnStart.Enabled = True
        btnStop.Enabled = False
        Debug.WriteLine("Server stopped!")
    End Sub

    Private Sub server_ServerStarted() Handles server.ServerStarted
        btnStart.Enabled = False
        btnStop.Enabled = True
        Debug.WriteLine("Server started!")
    End Sub

    Private Sub btnStart_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnStart.Click
        'StartServerByName(cboServer.Text)
        server.StartServer()
    End Sub

    Private Sub btnStop_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnStop.Click
        server.StopServer()
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        server.StopServer()
        Me.Close()
    End Sub

    Private Sub SpawnClientToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SpawnClientToolStripMenuItem1.Click
        SpawnClient()
    End Sub

    Private Sub KillPhpcgiexeProcessesToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles KillPhpcgiexeProcessesToolStripMenuItem1.Click
        RapidServer.KillAll("php-cgi.exe")
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboServer.SelectedIndexChanged
        PopulateServerInfo()
    End Sub

    Private Sub LinkLabel1_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        Process.Start(LinkLabel1.Text)
    End Sub

    Private Sub LinkLabel2_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabel2.LinkClicked
        Process.Start(LinkLabel2.Text)
    End Sub

    Private Sub chkWrapAccessLog_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkWrapLog.CheckedChanged
        If chkWrapLog.Checked = True Then
            txtLog.WordWrap = True
        Else
            txtLog.WordWrap = False
        End If
    End Sub

    Private Sub btnPurgeCache_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPurgeCache.Click
        server.OutputCache.Clear()
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        btnPurgeCache.Text = "Purge " & server.OutputCache.Count
    End Sub
End Class
