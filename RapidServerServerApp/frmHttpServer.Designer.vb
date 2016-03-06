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
        Dim ChartArea4 As System.Windows.Forms.DataVisualization.Charting.ChartArea = New System.Windows.Forms.DataVisualization.Charting.ChartArea()
        Dim Series4 As System.Windows.Forms.DataVisualization.Charting.Series = New System.Windows.Forms.DataVisualization.Charting.Series()
        Dim Title4 As System.Windows.Forms.DataVisualization.Charting.Title = New System.Windows.Forms.DataVisualization.Charting.Title()
        Dim ChartArea5 As System.Windows.Forms.DataVisualization.Charting.ChartArea = New System.Windows.Forms.DataVisualization.Charting.ChartArea()
        Dim Series5 As System.Windows.Forms.DataVisualization.Charting.Series = New System.Windows.Forms.DataVisualization.Charting.Series()
        Dim Title5 As System.Windows.Forms.DataVisualization.Charting.Title = New System.Windows.Forms.DataVisualization.Charting.Title()
        Dim ChartArea6 As System.Windows.Forms.DataVisualization.Charting.ChartArea = New System.Windows.Forms.DataVisualization.Charting.ChartArea()
        Dim Series6 As System.Windows.Forms.DataVisualization.Charting.Series = New System.Windows.Forms.DataVisualization.Charting.Series()
        Dim Title6 As System.Windows.Forms.DataVisualization.Charting.Title = New System.Windows.Forms.DataVisualization.Charting.Title()
        Me.btnStart = New System.Windows.Forms.Button()
        Me.btnStop = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.pnlMain = New System.Windows.Forms.Panel()
        Me.tabsMain = New System.Windows.Forms.TabControl()
        Me.tabInfo = New System.Windows.Forms.TabPage()
        Me.LinkLabel2 = New System.Windows.Forms.LinkLabel()
        Me.LinkLabel1 = New System.Windows.Forms.LinkLabel()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.tabLog = New System.Windows.Forms.TabPage()
        Me.txtLog = New System.Windows.Forms.TextBox()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.chkEnableLog = New System.Windows.Forms.CheckBox()
        Me.chkWrapLog = New System.Windows.Forms.CheckBox()
        Me.tabPerformance = New System.Windows.Forms.TabPage()
        Me.tabsPerformance = New System.Windows.Forms.TabControl()
        Me.tabConnections = New System.Windows.Forms.TabPage()
        Me.chartConnections = New System.Windows.Forms.DataVisualization.Charting.Chart()
        Me.tabCpu = New System.Windows.Forms.TabPage()
        Me.chartCpu = New System.Windows.Forms.DataVisualization.Charting.Chart()
        Me.tabRam = New System.Windows.Forms.TabPage()
        Me.chartRam = New System.Windows.Forms.DataVisualization.Charting.Chart()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.Column1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.ctxMain = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.SpawnClientToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.KillPhpcgiexeProcessesToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.btnPurgeCache = New System.Windows.Forms.Button()
        Me.cboServer = New System.Windows.Forms.ComboBox()
        Me.CheckBox1 = New System.Windows.Forms.CheckBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.timPerformance = New System.Windows.Forms.Timer(Me.components)
        Me.pnlMain.SuspendLayout()
        Me.tabsMain.SuspendLayout()
        Me.tabInfo.SuspendLayout()
        Me.tabLog.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.tabPerformance.SuspendLayout()
        Me.tabsPerformance.SuspendLayout()
        Me.tabConnections.SuspendLayout()
        CType(Me.chartConnections, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tabCpu.SuspendLayout()
        CType(Me.chartCpu, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tabRam.SuspendLayout()
        CType(Me.chartRam, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage1.SuspendLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.btnStop.Size = New System.Drawing.Size(71, 23)
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
        Me.Label1.Size = New System.Drawing.Size(590, 33)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "A very high performance web server utilizing .NET sockets and async I/O."
        '
        'pnlMain
        '
        Me.pnlMain.Controls.Add(Me.tabsMain)
        Me.pnlMain.Controls.Add(Me.Panel4)
        Me.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlMain.Location = New System.Drawing.Point(0, 65)
        Me.pnlMain.Name = "pnlMain"
        Me.pnlMain.Size = New System.Drawing.Size(590, 294)
        Me.pnlMain.TabIndex = 9
        '
        'tabsMain
        '
        Me.tabsMain.Controls.Add(Me.tabInfo)
        Me.tabsMain.Controls.Add(Me.tabLog)
        Me.tabsMain.Controls.Add(Me.tabPerformance)
        Me.tabsMain.Controls.Add(Me.TabPage1)
        Me.tabsMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tabsMain.Location = New System.Drawing.Point(0, 8)
        Me.tabsMain.Name = "tabsMain"
        Me.tabsMain.SelectedIndex = 0
        Me.tabsMain.Size = New System.Drawing.Size(590, 286)
        Me.tabsMain.TabIndex = 9
        '
        'tabInfo
        '
        Me.tabInfo.Controls.Add(Me.LinkLabel2)
        Me.tabInfo.Controls.Add(Me.LinkLabel1)
        Me.tabInfo.Controls.Add(Me.Label3)
        Me.tabInfo.Controls.Add(Me.Label2)
        Me.tabInfo.Location = New System.Drawing.Point(4, 22)
        Me.tabInfo.Name = "tabInfo"
        Me.tabInfo.Padding = New System.Windows.Forms.Padding(3)
        Me.tabInfo.Size = New System.Drawing.Size(582, 260)
        Me.tabInfo.TabIndex = 2
        Me.tabInfo.Text = "Info"
        Me.tabInfo.UseVisualStyleBackColor = True
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
        Me.tabLog.Size = New System.Drawing.Size(582, 260)
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
        Me.txtLog.Size = New System.Drawing.Size(576, 230)
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
        Me.Panel3.Size = New System.Drawing.Size(576, 24)
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
        'tabPerformance
        '
        Me.tabPerformance.Controls.Add(Me.tabsPerformance)
        Me.tabPerformance.Location = New System.Drawing.Point(4, 22)
        Me.tabPerformance.Name = "tabPerformance"
        Me.tabPerformance.Padding = New System.Windows.Forms.Padding(3)
        Me.tabPerformance.Size = New System.Drawing.Size(582, 260)
        Me.tabPerformance.TabIndex = 3
        Me.tabPerformance.Text = "Performance"
        Me.tabPerformance.UseVisualStyleBackColor = True
        '
        'tabsPerformance
        '
        Me.tabsPerformance.Controls.Add(Me.tabConnections)
        Me.tabsPerformance.Controls.Add(Me.tabCpu)
        Me.tabsPerformance.Controls.Add(Me.tabRam)
        Me.tabsPerformance.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tabsPerformance.Location = New System.Drawing.Point(3, 3)
        Me.tabsPerformance.Name = "tabsPerformance"
        Me.tabsPerformance.SelectedIndex = 0
        Me.tabsPerformance.Size = New System.Drawing.Size(576, 254)
        Me.tabsPerformance.TabIndex = 1
        '
        'tabConnections
        '
        Me.tabConnections.Controls.Add(Me.chartConnections)
        Me.tabConnections.Location = New System.Drawing.Point(4, 22)
        Me.tabConnections.Name = "tabConnections"
        Me.tabConnections.Padding = New System.Windows.Forms.Padding(3)
        Me.tabConnections.Size = New System.Drawing.Size(568, 228)
        Me.tabConnections.TabIndex = 0
        Me.tabConnections.Text = "Connections"
        Me.tabConnections.UseVisualStyleBackColor = True
        '
        'chartConnections
        '
        ChartArea4.AxisY.Title = "connection count"
        ChartArea4.Name = "ChartArea1"
        Me.chartConnections.ChartAreas.Add(ChartArea4)
        Me.chartConnections.Dock = System.Windows.Forms.DockStyle.Fill
        Me.chartConnections.Location = New System.Drawing.Point(3, 3)
        Me.chartConnections.Name = "chartConnections"
        Series4.ChartArea = "ChartArea1"
        Series4.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine
        Series4.Name = "Series1"
        Me.chartConnections.Series.Add(Series4)
        Me.chartConnections.Size = New System.Drawing.Size(562, 222)
        Me.chartConnections.TabIndex = 0
        Me.chartConnections.Text = "Chart1"
        Title4.Name = "Title1"
        Title4.Text = "Connections"
        Me.chartConnections.Titles.Add(Title4)
        '
        'tabCpu
        '
        Me.tabCpu.Controls.Add(Me.chartCpu)
        Me.tabCpu.Location = New System.Drawing.Point(4, 22)
        Me.tabCpu.Name = "tabCpu"
        Me.tabCpu.Padding = New System.Windows.Forms.Padding(3)
        Me.tabCpu.Size = New System.Drawing.Size(568, 228)
        Me.tabCpu.TabIndex = 1
        Me.tabCpu.Text = "CPU"
        Me.tabCpu.UseVisualStyleBackColor = True
        '
        'chartCpu
        '
        ChartArea5.AxisY.Maximum = 100.0R
        ChartArea5.AxisY.Minimum = 0.0R
        ChartArea5.AxisY.Title = "cpu usage %"
        ChartArea5.Name = "ChartArea1"
        Me.chartCpu.ChartAreas.Add(ChartArea5)
        Me.chartCpu.Dock = System.Windows.Forms.DockStyle.Fill
        Me.chartCpu.Location = New System.Drawing.Point(3, 3)
        Me.chartCpu.Name = "chartCpu"
        Series5.ChartArea = "ChartArea1"
        Series5.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine
        Series5.Name = "Series1"
        Me.chartCpu.Series.Add(Series5)
        Me.chartCpu.Size = New System.Drawing.Size(562, 222)
        Me.chartCpu.TabIndex = 19
        Me.chartCpu.Text = "Chart2"
        Title5.Name = "titCpuMain"
        Title5.Text = "CPU"
        Me.chartCpu.Titles.Add(Title5)
        '
        'tabRam
        '
        Me.tabRam.Controls.Add(Me.chartRam)
        Me.tabRam.Location = New System.Drawing.Point(4, 22)
        Me.tabRam.Name = "tabRam"
        Me.tabRam.Padding = New System.Windows.Forms.Padding(3)
        Me.tabRam.Size = New System.Drawing.Size(568, 228)
        Me.tabRam.TabIndex = 2
        Me.tabRam.Text = "RAM"
        Me.tabRam.UseVisualStyleBackColor = True
        '
        'chartRam
        '
        ChartArea6.AxisY.Title = "megabytes"
        ChartArea6.Name = "ChartArea1"
        Me.chartRam.ChartAreas.Add(ChartArea6)
        Me.chartRam.Dock = System.Windows.Forms.DockStyle.Fill
        Me.chartRam.Location = New System.Drawing.Point(3, 3)
        Me.chartRam.Name = "chartRam"
        Series6.ChartArea = "ChartArea1"
        Series6.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine
        Series6.Name = "Series1"
        Me.chartRam.Series.Add(Series6)
        Me.chartRam.Size = New System.Drawing.Size(562, 222)
        Me.chartRam.TabIndex = 20
        Me.chartRam.Text = "Chart2"
        Title6.Name = "Title1"
        Title6.Text = "RAM"
        Me.chartRam.Titles.Add(Title6)
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.DataGridView1)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(582, 260)
        Me.TabPage1.TabIndex = 4
        Me.TabPage1.Text = "Sites"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.AllowUserToDeleteRows = False
        Me.DataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Column1, Me.Column2, Me.Column3})
        Me.DataGridView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataGridView1.Location = New System.Drawing.Point(3, 3)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.ReadOnly = True
        Me.DataGridView1.Size = New System.Drawing.Size(576, 254)
        Me.DataGridView1.TabIndex = 0
        '
        'Column1
        '
        Me.Column1.HeaderText = "Name"
        Me.Column1.Name = "Column1"
        Me.Column1.ReadOnly = True
        Me.Column1.Width = 60
        '
        'Column2
        '
        Me.Column2.HeaderText = "URL"
        Me.Column2.Name = "Column2"
        Me.Column2.ReadOnly = True
        Me.Column2.Width = 54
        '
        'Column3
        '
        Me.Column3.HeaderText = "Path"
        Me.Column3.Name = "Column3"
        Me.Column3.ReadOnly = True
        Me.Column3.Width = 54
        '
        'Panel4
        '
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel4.Location = New System.Drawing.Point(0, 0)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(590, 8)
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
        Me.Panel2.Size = New System.Drawing.Size(590, 32)
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
        'btnPurgeCache
        '
        Me.btnPurgeCache.Location = New System.Drawing.Point(137, 4)
        Me.btnPurgeCache.Name = "btnPurgeCache"
        Me.btnPurgeCache.Size = New System.Drawing.Size(72, 23)
        Me.btnPurgeCache.TabIndex = 11
        Me.btnPurgeCache.Text = "Purge"
        Me.btnPurgeCache.UseVisualStyleBackColor = True
        '
        'cboServer
        '
        Me.cboServer.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cboServer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboServer.FormattingEnabled = True
        Me.cboServer.Location = New System.Drawing.Point(471, 5)
        Me.cboServer.Name = "cboServer"
        Me.cboServer.Size = New System.Drawing.Size(112, 21)
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
        'timPerformance
        '
        Me.timPerformance.Enabled = True
        Me.timPerformance.Interval = 500
        '
        'frmHttpServer
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(590, 359)
        Me.Controls.Add(Me.pnlMain)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Label1)
        Me.MinimumSize = New System.Drawing.Size(263, 103)
        Me.Name = "frmHttpServer"
        Me.Text = "Rapid Web Server"
        Me.pnlMain.ResumeLayout(False)
        Me.tabsMain.ResumeLayout(False)
        Me.tabInfo.ResumeLayout(False)
        Me.tabInfo.PerformLayout()
        Me.tabLog.ResumeLayout(False)
        Me.tabLog.PerformLayout()
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        Me.tabPerformance.ResumeLayout(False)
        Me.tabsPerformance.ResumeLayout(False)
        Me.tabConnections.ResumeLayout(False)
        CType(Me.chartConnections, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tabCpu.ResumeLayout(False)
        CType(Me.chartCpu, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tabRam.ResumeLayout(False)
        CType(Me.chartRam, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage1.ResumeLayout(False)
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents pnlMain As System.Windows.Forms.Panel
    Friend WithEvents tabsMain As System.Windows.Forms.TabControl
    Friend WithEvents tabLog As System.Windows.Forms.TabPage
    Friend WithEvents txtLog As System.Windows.Forms.TextBox
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents chkWrapLog As System.Windows.Forms.CheckBox
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents CheckBox1 As System.Windows.Forms.CheckBox
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents tabInfo As System.Windows.Forms.TabPage
    Friend WithEvents tabPerformance As System.Windows.Forms.TabPage
    Friend WithEvents LinkLabel2 As System.Windows.Forms.LinkLabel
    Friend WithEvents LinkLabel1 As System.Windows.Forms.LinkLabel
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents chkEnableLog As System.Windows.Forms.CheckBox
    Friend WithEvents btnPurgeCache As System.Windows.Forms.Button
    Friend WithEvents timPerformance As System.Windows.Forms.Timer
    Friend WithEvents tabsPerformance As System.Windows.Forms.TabControl
    Friend WithEvents tabConnections As System.Windows.Forms.TabPage
    Friend WithEvents tabCpu As System.Windows.Forms.TabPage
    Friend WithEvents chartCpu As System.Windows.Forms.DataVisualization.Charting.Chart
    Friend WithEvents chartConnections As System.Windows.Forms.DataVisualization.Charting.Chart
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents Column1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents tabRam As System.Windows.Forms.TabPage
    Friend WithEvents chartRam As System.Windows.Forms.DataVisualization.Charting.Chart

End Class
