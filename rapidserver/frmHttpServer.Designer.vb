<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmHttpServer
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.btnStart = New System.Windows.Forms.Button()
        Me.btnStop = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage3 = New System.Windows.Forms.TabPage()
        Me.LinkLabel2 = New System.Windows.Forms.LinkLabel()
        Me.LinkLabel1 = New System.Windows.Forms.LinkLabel()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.tabLog = New System.Windows.Forms.TabPage()
        Me.txtLog = New System.Windows.Forms.TextBox()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.chkEnableLog = New System.Windows.Forms.CheckBox()
        Me.chkWrapLog = New System.Windows.Forms.CheckBox()
        Me.TabPage4 = New System.Windows.Forms.TabPage()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.ctxMain = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.SpawnClientToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.KillPhpcgiexeProcessesToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.cboServer = New System.Windows.Forms.ComboBox()
        Me.CheckBox1 = New System.Windows.Forms.CheckBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.btnPurgeCache = New System.Windows.Forms.Button()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.Panel1.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.TabPage3.SuspendLayout()
        Me.tabLog.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.TabPage4.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.ctxMain.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnStart
        '
        Me.btnStart.Location = New System.Drawing.Point(4, 4)
        Me.btnStart.Name = "btnStart"
        Me.btnStart.Size = New System.Drawing.Size(52, 23)
        Me.btnStart.TabIndex = 1
        Me.btnStart.Text = "Start"
        Me.btnStart.UseVisualStyleBackColor = True
        '
        'btnStop
        '
        Me.btnStop.Location = New System.Drawing.Point(60, 4)
        Me.btnStop.Name = "btnStop"
        Me.btnStop.Size = New System.Drawing.Size(52, 23)
        Me.btnStop.TabIndex = 2
        Me.btnStop.Text = "Stop"
        Me.btnStop.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.SystemColors.ControlDark
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlLightLight
        Me.Label1.Location = New System.Drawing.Point(0, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Padding = New System.Windows.Forms.Padding(2)
        Me.Label1.Size = New System.Drawing.Size(344, 33)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "A very high performance web server utilizing .NET sockets and async I/O."
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.TabControl1)
        Me.Panel1.Controls.Add(Me.Panel4)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 65)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(344, 194)
        Me.Panel1.TabIndex = 9
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage3)
        Me.TabControl1.Controls.Add(Me.tabLog)
        Me.TabControl1.Controls.Add(Me.TabPage4)
        Me.TabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControl1.Location = New System.Drawing.Point(0, 8)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(344, 186)
        Me.TabControl1.TabIndex = 9
        '
        'TabPage3
        '
        Me.TabPage3.Controls.Add(Me.LinkLabel2)
        Me.TabPage3.Controls.Add(Me.LinkLabel1)
        Me.TabPage3.Controls.Add(Me.Label3)
        Me.TabPage3.Controls.Add(Me.Label2)
        Me.TabPage3.Location = New System.Drawing.Point(4, 22)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage3.Size = New System.Drawing.Size(336, 160)
        Me.TabPage3.TabIndex = 2
        Me.TabPage3.Text = "Info"
        Me.TabPage3.UseVisualStyleBackColor = True
        '
        'LinkLabel2
        '
        Me.LinkLabel2.AutoSize = True
        Me.LinkLabel2.Location = New System.Drawing.Point(68, 24)
        Me.LinkLabel2.Name = "LinkLabel2"
        Me.LinkLabel2.Size = New System.Drawing.Size(59, 13)
        Me.LinkLabel2.TabIndex = 13
        Me.LinkLabel2.TabStop = True
        Me.LinkLabel2.Text = "LinkLabel2"
        '
        'LinkLabel1
        '
        Me.LinkLabel1.AutoSize = True
        Me.LinkLabel1.Location = New System.Drawing.Point(68, 8)
        Me.LinkLabel1.Name = "LinkLabel1"
        Me.LinkLabel1.Size = New System.Drawing.Size(59, 13)
        Me.LinkLabel1.TabIndex = 12
        Me.LinkLabel1.TabStop = True
        Me.LinkLabel1.Text = "LinkLabel1"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(8, 8)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(61, 13)
        Me.Label3.TabIndex = 11
        Me.Label3.Text = "Root Path: "
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(8, 24)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(49, 13)
        Me.Label2.TabIndex = 10
        Me.Label2.Text = "Root Url:"
        '
        'tabLog
        '
        Me.tabLog.Controls.Add(Me.txtLog)
        Me.tabLog.Controls.Add(Me.Panel3)
        Me.tabLog.Location = New System.Drawing.Point(4, 22)
        Me.tabLog.Name = "tabLog"
        Me.tabLog.Padding = New System.Windows.Forms.Padding(3)
        Me.tabLog.Size = New System.Drawing.Size(336, 160)
        Me.tabLog.TabIndex = 0
        Me.tabLog.Text = "Log"
        Me.tabLog.UseVisualStyleBackColor = True
        '
        'txtLog
        '
        Me.txtLog.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtLog.Location = New System.Drawing.Point(3, 27)
        Me.txtLog.Multiline = True
        Me.txtLog.Name = "txtLog"
        Me.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtLog.Size = New System.Drawing.Size(330, 130)
        Me.txtLog.TabIndex = 8
        Me.txtLog.WordWrap = False
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.chkEnableLog)
        Me.Panel3.Controls.Add(Me.chkWrapLog)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel3.Location = New System.Drawing.Point(3, 3)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(330, 24)
        Me.Panel3.TabIndex = 11
        '
        'chkEnableLog
        '
        Me.chkEnableLog.AutoSize = True
        Me.chkEnableLog.Location = New System.Drawing.Point(4, 4)
        Me.chkEnableLog.Name = "chkEnableLog"
        Me.chkEnableLog.Size = New System.Drawing.Size(59, 17)
        Me.chkEnableLog.TabIndex = 10
        Me.chkEnableLog.Text = "Enable"
        Me.chkEnableLog.UseVisualStyleBackColor = True
        '
        'chkWrapLog
        '
        Me.chkWrapLog.AutoSize = True
        Me.chkWrapLog.Location = New System.Drawing.Point(68, 4)
        Me.chkWrapLog.Name = "chkWrapLog"
        Me.chkWrapLog.Size = New System.Drawing.Size(52, 17)
        Me.chkWrapLog.TabIndex = 9
        Me.chkWrapLog.Text = "Wrap"
        Me.chkWrapLog.UseVisualStyleBackColor = True
        '
        'TabPage4
        '
        Me.TabPage4.Controls.Add(Me.Label4)
        Me.TabPage4.Location = New System.Drawing.Point(4, 22)
        Me.TabPage4.Name = "TabPage4"
        Me.TabPage4.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage4.Size = New System.Drawing.Size(336, 160)
        Me.TabPage4.TabIndex = 3
        Me.TabPage4.Text = "Performance"
        Me.TabPage4.UseVisualStyleBackColor = True
        '
        'Label4
        '
        Me.Label4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label4.Location = New System.Drawing.Point(3, 3)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(330, 154)
        Me.Label4.TabIndex = 2
        Me.Label4.Text = "A future project..."
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Panel4
        '
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel4.Location = New System.Drawing.Point(0, 0)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(344, 8)
        Me.Panel4.TabIndex = 13
        '
        'Panel2
        '
        Me.Panel2.ContextMenuStrip = Me.ctxMain
        Me.Panel2.Controls.Add(Me.btnPurgeCache)
        Me.Panel2.Controls.Add(Me.cboServer)
        Me.Panel2.Controls.Add(Me.btnStart)
        Me.Panel2.Controls.Add(Me.btnStop)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(0, 33)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(344, 32)
        Me.Panel2.TabIndex = 10
        '
        'ctxMain
        '
        Me.ctxMain.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SpawnClientToolStripMenuItem1, Me.KillPhpcgiexeProcessesToolStripMenuItem1})
        Me.ctxMain.Name = "ContextMenuStrip1"
        Me.ctxMain.Size = New System.Drawing.Size(210, 48)
        '
        'SpawnClientToolStripMenuItem1
        '
        Me.SpawnClientToolStripMenuItem1.Name = "SpawnClientToolStripMenuItem1"
        Me.SpawnClientToolStripMenuItem1.Size = New System.Drawing.Size(209, 22)
        Me.SpawnClientToolStripMenuItem1.Text = "Spawn Client"
        '
        'KillPhpcgiexeProcessesToolStripMenuItem1
        '
        Me.KillPhpcgiexeProcessesToolStripMenuItem1.Name = "KillPhpcgiexeProcessesToolStripMenuItem1"
        Me.KillPhpcgiexeProcessesToolStripMenuItem1.Size = New System.Drawing.Size(209, 22)
        Me.KillPhpcgiexeProcessesToolStripMenuItem1.Text = "Kill php-cgi.exe processes"
        '
        'cboServer
        '
        Me.cboServer.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cboServer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboServer.FormattingEnabled = True
        Me.cboServer.Location = New System.Drawing.Point(216, 5)
        Me.cboServer.Name = "cboServer"
        Me.cboServer.Size = New System.Drawing.Size(121, 21)
        Me.cboServer.Sorted = True
        Me.cboServer.TabIndex = 10
        '
        'CheckBox1
        '
        Me.CheckBox1.AutoSize = True
        Me.CheckBox1.Location = New System.Drawing.Point(4, 4)
        Me.CheckBox1.Name = "CheckBox1"
        Me.CheckBox1.Size = New System.Drawing.Size(52, 17)
        Me.CheckBox1.TabIndex = 9
        Me.CheckBox1.Text = "Wrap"
        Me.CheckBox1.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(145, 1)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(80, 23)
        Me.Button1.TabIndex = 9
        Me.Button1.Text = "Open Site"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(57, 1)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(84, 23)
        Me.Button2.TabIndex = 8
        Me.Button2.Text = "Open Folder"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'btnPurgeCache
        '
        Me.btnPurgeCache.Location = New System.Drawing.Point(116, 4)
        Me.btnPurgeCache.Name = "btnPurgeCache"
        Me.btnPurgeCache.Size = New System.Drawing.Size(67, 23)
        Me.btnPurgeCache.TabIndex = 11
        Me.btnPurgeCache.Text = "Purge"
        Me.btnPurgeCache.UseVisualStyleBackColor = True
        '
        'Timer1
        '
        Me.Timer1.Enabled = True
        Me.Timer1.Interval = 1000
        '
        'frmHttpServer
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(344, 259)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Label1)
        Me.MinimumSize = New System.Drawing.Size(263, 103)
        Me.Name = "frmHttpServer"
        Me.Text = "Rapid Web Server"
        Me.Panel1.ResumeLayout(False)
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage3.ResumeLayout(False)
        Me.TabPage3.PerformLayout()
        Me.tabLog.ResumeLayout(False)
        Me.tabLog.PerformLayout()
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        Me.TabPage4.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.ctxMain.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnStart As System.Windows.Forms.Button
    Friend WithEvents btnStop As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents ctxMain As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents SpawnClientToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents KillPhpcgiexeProcessesToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cboServer As System.Windows.Forms.ComboBox
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents tabLog As System.Windows.Forms.TabPage
    Friend WithEvents txtLog As System.Windows.Forms.TextBox
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents chkWrapLog As System.Windows.Forms.CheckBox
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents CheckBox1 As System.Windows.Forms.CheckBox
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents TabPage3 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage4 As System.Windows.Forms.TabPage
    Friend WithEvents LinkLabel2 As System.Windows.Forms.LinkLabel
    Friend WithEvents LinkLabel1 As System.Windows.Forms.LinkLabel
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents chkEnableLog As System.Windows.Forms.CheckBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents btnPurgeCache As System.Windows.Forms.Button
    Friend WithEvents Timer1 As System.Windows.Forms.Timer

End Class
