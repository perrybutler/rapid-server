<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmHttpClient
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
        Dim ChartArea1 As System.Windows.Forms.DataVisualization.Charting.ChartArea = New System.Windows.Forms.DataVisualization.Charting.ChartArea()
        Dim Legend1 As System.Windows.Forms.DataVisualization.Charting.Legend = New System.Windows.Forms.DataVisualization.Charting.Legend()
        Dim Title1 As System.Windows.Forms.DataVisualization.Charting.Title = New System.Windows.Forms.DataVisualization.Charting.Title()
        Dim ChartArea2 As System.Windows.Forms.DataVisualization.Charting.ChartArea = New System.Windows.Forms.DataVisualization.Charting.ChartArea()
        Dim Legend2 As System.Windows.Forms.DataVisualization.Charting.Legend = New System.Windows.Forms.DataVisualization.Charting.Legend()
        Dim Title2 As System.Windows.Forms.DataVisualization.Charting.Title = New System.Windows.Forms.DataVisualization.Charting.Title()
        Dim ChartArea3 As System.Windows.Forms.DataVisualization.Charting.ChartArea = New System.Windows.Forms.DataVisualization.Charting.ChartArea()
        Dim Legend3 As System.Windows.Forms.DataVisualization.Charting.Legend = New System.Windows.Forms.DataVisualization.Charting.Legend()
        Dim Title3 As System.Windows.Forms.DataVisualization.Charting.Title = New System.Windows.Forms.DataVisualization.Charting.Title()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.cboUrl = New System.Windows.Forms.ComboBox()
        Me.btnGo = New System.Windows.Forms.Button()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.tabRaw = New System.Windows.Forms.TabPage()
        Me.txtRaw = New System.Windows.Forms.TextBox()
        Me.tabBenchmark = New System.Windows.Forms.TabPage()
        Me.btnClear = New System.Windows.Forms.Button()
        Me.TabControl3 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.cboBenchmarkTool = New System.Windows.Forms.ComboBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtBenchmarkNumber = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtBenchmarkConcurrency = New System.Windows.Forms.TextBox()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.cboBenchmarkTool2 = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtBenchmarkDuration = New System.Windows.Forms.TextBox()
        Me.TabPage3 = New System.Windows.Forms.TabPage()
        Me.cboBenchmarkTool3 = New System.Windows.Forms.ComboBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.txtBenchmarkRampNumber = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.txtBenchmarkRampConcurrency = New System.Windows.Forms.TextBox()
        Me.TabControl2 = New System.Windows.Forms.TabControl()
        Me.tabBenchmarkBarChart = New System.Windows.Forms.TabPage()
        Me.Chart1 = New System.Windows.Forms.DataVisualization.Charting.Chart()
        Me.tabBenchmarkCompletedRequests = New System.Windows.Forms.TabPage()
        Me.Chart2 = New System.Windows.Forms.DataVisualization.Charting.Chart()
        Me.tabBenchmarkLineChart = New System.Windows.Forms.TabPage()
        Me.Chart3 = New System.Windows.Forms.DataVisualization.Charting.Chart()
        Me.btnDetectSystemInfo = New System.Windows.Forms.Button()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.tabLog = New System.Windows.Forms.TabPage()
        Me.txtLog = New System.Windows.Forms.TextBox()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.chkEnableLog = New System.Windows.Forms.CheckBox()
        Me.chkWrapLog = New System.Windows.Forms.CheckBox()
        Me.tabOptions = New System.Windows.Forms.TabPage()
        Me.chkFetchSubResources = New System.Windows.Forms.CheckBox()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.Panel1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.tabRaw.SuspendLayout()
        Me.tabBenchmark.SuspendLayout()
        Me.TabControl3.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        Me.TabPage3.SuspendLayout()
        Me.TabControl2.SuspendLayout()
        Me.tabBenchmarkBarChart.SuspendLayout()
        CType(Me.Chart1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tabBenchmarkCompletedRequests.SuspendLayout()
        CType(Me.Chart2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tabBenchmarkLineChart.SuspendLayout()
        CType(Me.Chart3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tabLog.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.tabOptions.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.SystemColors.ControlDark
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlLightLight
        Me.Label1.Location = New System.Drawing.Point(0, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Padding = New System.Windows.Forms.Padding(2)
        Me.Label1.Size = New System.Drawing.Size(615, 33)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "An HTTP client for testing request/response cycles and benchmarking web server pe" & _
    "rformance under various heavy load scenarios."
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.cboUrl)
        Me.Panel1.Controls.Add(Me.btnGo)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 33)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(615, 29)
        Me.Panel1.TabIndex = 5
        '
        'cboUrl
        '
        Me.cboUrl.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cboUrl.FormattingEnabled = True
        Me.cboUrl.Location = New System.Drawing.Point(4, 4)
        Me.cboUrl.Name = "cboUrl"
        Me.cboUrl.Size = New System.Drawing.Size(565, 21)
        Me.cboUrl.Sorted = True
        Me.cboUrl.TabIndex = 5
        '
        'btnGo
        '
        Me.btnGo.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnGo.Location = New System.Drawing.Point(575, 3)
        Me.btnGo.Name = "btnGo"
        Me.btnGo.Size = New System.Drawing.Size(37, 23)
        Me.btnGo.TabIndex = 3
        Me.btnGo.Text = "Go"
        Me.btnGo.UseVisualStyleBackColor = True
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.TabControl1)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel2.Location = New System.Drawing.Point(0, 62)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(615, 389)
        Me.Panel2.TabIndex = 6
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.tabRaw)
        Me.TabControl1.Controls.Add(Me.tabBenchmark)
        Me.TabControl1.Controls.Add(Me.tabLog)
        Me.TabControl1.Controls.Add(Me.tabOptions)
        Me.TabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControl1.Location = New System.Drawing.Point(0, 0)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(615, 389)
        Me.TabControl1.TabIndex = 2
        '
        'tabRaw
        '
        Me.tabRaw.Controls.Add(Me.txtRaw)
        Me.tabRaw.Location = New System.Drawing.Point(4, 22)
        Me.tabRaw.Name = "tabRaw"
        Me.tabRaw.Padding = New System.Windows.Forms.Padding(3)
        Me.tabRaw.Size = New System.Drawing.Size(607, 363)
        Me.tabRaw.TabIndex = 0
        Me.tabRaw.Text = "Raw"
        Me.tabRaw.UseVisualStyleBackColor = True
        '
        'txtRaw
        '
        Me.txtRaw.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtRaw.Font = New System.Drawing.Font("Consolas", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRaw.Location = New System.Drawing.Point(3, 3)
        Me.txtRaw.Multiline = True
        Me.txtRaw.Name = "txtRaw"
        Me.txtRaw.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtRaw.Size = New System.Drawing.Size(601, 357)
        Me.txtRaw.TabIndex = 2
        '
        'tabBenchmark
        '
        Me.tabBenchmark.Controls.Add(Me.btnClear)
        Me.tabBenchmark.Controls.Add(Me.TabControl3)
        Me.tabBenchmark.Controls.Add(Me.TabControl2)
        Me.tabBenchmark.Controls.Add(Me.btnDetectSystemInfo)
        Me.tabBenchmark.Controls.Add(Me.TextBox1)
        Me.tabBenchmark.Location = New System.Drawing.Point(4, 22)
        Me.tabBenchmark.Name = "tabBenchmark"
        Me.tabBenchmark.Padding = New System.Windows.Forms.Padding(3)
        Me.tabBenchmark.Size = New System.Drawing.Size(607, 363)
        Me.tabBenchmark.TabIndex = 3
        Me.tabBenchmark.Text = "Benchmark"
        Me.tabBenchmark.UseVisualStyleBackColor = True
        '
        'btnClear
        '
        Me.btnClear.Location = New System.Drawing.Point(3, 145)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(154, 23)
        Me.btnClear.TabIndex = 19
        Me.btnClear.Text = "Clear Results"
        Me.btnClear.UseVisualStyleBackColor = True
        '
        'TabControl3
        '
        Me.TabControl3.Controls.Add(Me.TabPage1)
        Me.TabControl3.Controls.Add(Me.TabPage2)
        Me.TabControl3.Controls.Add(Me.TabPage3)
        Me.TabControl3.Location = New System.Drawing.Point(3, 6)
        Me.TabControl3.Name = "TabControl3"
        Me.TabControl3.SelectedIndex = 0
        Me.TabControl3.Size = New System.Drawing.Size(154, 108)
        Me.TabControl3.TabIndex = 18
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.cboBenchmarkTool)
        Me.TabPage1.Controls.Add(Me.Label6)
        Me.TabPage1.Controls.Add(Me.Label4)
        Me.TabPage1.Controls.Add(Me.txtBenchmarkNumber)
        Me.TabPage1.Controls.Add(Me.Label5)
        Me.TabPage1.Controls.Add(Me.txtBenchmarkConcurrency)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(146, 82)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Speed"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'cboBenchmarkTool
        '
        Me.cboBenchmarkTool.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboBenchmarkTool.FormattingEnabled = True
        Me.cboBenchmarkTool.Location = New System.Drawing.Point(41, 6)
        Me.cboBenchmarkTool.Name = "cboBenchmarkTool"
        Me.cboBenchmarkTool.Size = New System.Drawing.Size(99, 21)
        Me.cboBenchmarkTool.Sorted = True
        Me.cboBenchmarkTool.TabIndex = 23
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(4, 9)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(31, 13)
        Me.Label6.TabIndex = 22
        Me.Label6.Text = "Tool:"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(6, 36)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(47, 13)
        Me.Label4.TabIndex = 0
        Me.Label4.Text = "Number:"
        '
        'txtBenchmarkNumber
        '
        Me.txtBenchmarkNumber.Location = New System.Drawing.Point(79, 33)
        Me.txtBenchmarkNumber.Name = "txtBenchmarkNumber"
        Me.txtBenchmarkNumber.Size = New System.Drawing.Size(61, 20)
        Me.txtBenchmarkNumber.TabIndex = 1
        Me.txtBenchmarkNumber.Text = "1000"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(6, 62)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(70, 13)
        Me.Label5.TabIndex = 2
        Me.Label5.Text = "Concurrency:"
        '
        'txtBenchmarkConcurrency
        '
        Me.txtBenchmarkConcurrency.Location = New System.Drawing.Point(79, 59)
        Me.txtBenchmarkConcurrency.Name = "txtBenchmarkConcurrency"
        Me.txtBenchmarkConcurrency.Size = New System.Drawing.Size(61, 20)
        Me.txtBenchmarkConcurrency.TabIndex = 3
        Me.txtBenchmarkConcurrency.Text = "100"
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.cboBenchmarkTool2)
        Me.TabPage2.Controls.Add(Me.Label3)
        Me.TabPage2.Controls.Add(Me.Label2)
        Me.TabPage2.Controls.Add(Me.txtBenchmarkDuration)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(146, 82)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Time"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'cboBenchmarkTool2
        '
        Me.cboBenchmarkTool2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboBenchmarkTool2.FormattingEnabled = True
        Me.cboBenchmarkTool2.Location = New System.Drawing.Point(41, 6)
        Me.cboBenchmarkTool2.Name = "cboBenchmarkTool2"
        Me.cboBenchmarkTool2.Size = New System.Drawing.Size(99, 21)
        Me.cboBenchmarkTool2.Sorted = True
        Me.cboBenchmarkTool2.TabIndex = 25
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(4, 9)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(31, 13)
        Me.Label3.TabIndex = 24
        Me.Label3.Text = "Tool:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(6, 36)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(50, 13)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Duration:"
        '
        'txtBenchmarkDuration
        '
        Me.txtBenchmarkDuration.Location = New System.Drawing.Point(79, 33)
        Me.txtBenchmarkDuration.Name = "txtBenchmarkDuration"
        Me.txtBenchmarkDuration.Size = New System.Drawing.Size(61, 20)
        Me.txtBenchmarkDuration.TabIndex = 3
        Me.txtBenchmarkDuration.Text = "60"
        '
        'TabPage3
        '
        Me.TabPage3.Controls.Add(Me.cboBenchmarkTool3)
        Me.TabPage3.Controls.Add(Me.Label7)
        Me.TabPage3.Controls.Add(Me.Label8)
        Me.TabPage3.Controls.Add(Me.txtBenchmarkRampNumber)
        Me.TabPage3.Controls.Add(Me.Label9)
        Me.TabPage3.Controls.Add(Me.txtBenchmarkRampConcurrency)
        Me.TabPage3.Location = New System.Drawing.Point(4, 22)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage3.Size = New System.Drawing.Size(146, 82)
        Me.TabPage3.TabIndex = 2
        Me.TabPage3.Text = "Ramp"
        Me.TabPage3.UseVisualStyleBackColor = True
        '
        'cboBenchmarkTool3
        '
        Me.cboBenchmarkTool3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboBenchmarkTool3.FormattingEnabled = True
        Me.cboBenchmarkTool3.Location = New System.Drawing.Point(41, 6)
        Me.cboBenchmarkTool3.Name = "cboBenchmarkTool3"
        Me.cboBenchmarkTool3.Size = New System.Drawing.Size(99, 21)
        Me.cboBenchmarkTool3.Sorted = True
        Me.cboBenchmarkTool3.TabIndex = 29
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(4, 9)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(31, 13)
        Me.Label7.TabIndex = 28
        Me.Label7.Text = "Tool:"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(6, 36)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(47, 13)
        Me.Label8.TabIndex = 24
        Me.Label8.Text = "Number:"
        '
        'txtBenchmarkRampNumber
        '
        Me.txtBenchmarkRampNumber.Location = New System.Drawing.Point(79, 33)
        Me.txtBenchmarkRampNumber.Name = "txtBenchmarkRampNumber"
        Me.txtBenchmarkRampNumber.Size = New System.Drawing.Size(61, 20)
        Me.txtBenchmarkRampNumber.TabIndex = 25
        Me.txtBenchmarkRampNumber.Text = "1000"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(6, 62)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(70, 13)
        Me.Label9.TabIndex = 26
        Me.Label9.Text = "Concurrency:"
        '
        'txtBenchmarkRampConcurrency
        '
        Me.txtBenchmarkRampConcurrency.Location = New System.Drawing.Point(79, 59)
        Me.txtBenchmarkRampConcurrency.Name = "txtBenchmarkRampConcurrency"
        Me.txtBenchmarkRampConcurrency.Size = New System.Drawing.Size(61, 20)
        Me.txtBenchmarkRampConcurrency.TabIndex = 27
        Me.txtBenchmarkRampConcurrency.Text = "100"
        '
        'TabControl2
        '
        Me.TabControl2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TabControl2.Controls.Add(Me.tabBenchmarkBarChart)
        Me.TabControl2.Controls.Add(Me.tabBenchmarkCompletedRequests)
        Me.TabControl2.Controls.Add(Me.tabBenchmarkLineChart)
        Me.TabControl2.Location = New System.Drawing.Point(159, 6)
        Me.TabControl2.Name = "TabControl2"
        Me.TabControl2.SelectedIndex = 0
        Me.TabControl2.Size = New System.Drawing.Size(445, 354)
        Me.TabControl2.TabIndex = 17
        '
        'tabBenchmarkBarChart
        '
        Me.tabBenchmarkBarChart.Controls.Add(Me.Chart1)
        Me.tabBenchmarkBarChart.Location = New System.Drawing.Point(4, 22)
        Me.tabBenchmarkBarChart.Name = "tabBenchmarkBarChart"
        Me.tabBenchmarkBarChart.Padding = New System.Windows.Forms.Padding(3)
        Me.tabBenchmarkBarChart.Size = New System.Drawing.Size(437, 328)
        Me.tabBenchmarkBarChart.TabIndex = 0
        Me.tabBenchmarkBarChart.Text = "RPS"
        Me.tabBenchmarkBarChart.UseVisualStyleBackColor = True
        '
        'Chart1
        '
        ChartArea1.AxisX.Title = "run"
        ChartArea1.AxisY.Title = "requests"
        ChartArea1.Name = "ChartArea1"
        Me.Chart1.ChartAreas.Add(ChartArea1)
        Me.Chart1.Dock = System.Windows.Forms.DockStyle.Fill
        Legend1.Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Bottom
        Legend1.Name = "Legend1"
        Me.Chart1.Legends.Add(Legend1)
        Me.Chart1.Location = New System.Drawing.Point(3, 3)
        Me.Chart1.Name = "Chart1"
        Me.Chart1.Size = New System.Drawing.Size(431, 322)
        Me.Chart1.TabIndex = 15
        Me.Chart1.Text = "Chart1"
        Title1.Name = "Title1"
        Title1.Text = "Requests Per Second (RPS)"
        Me.Chart1.Titles.Add(Title1)
        '
        'tabBenchmarkCompletedRequests
        '
        Me.tabBenchmarkCompletedRequests.Controls.Add(Me.Chart2)
        Me.tabBenchmarkCompletedRequests.Location = New System.Drawing.Point(4, 22)
        Me.tabBenchmarkCompletedRequests.Name = "tabBenchmarkCompletedRequests"
        Me.tabBenchmarkCompletedRequests.Padding = New System.Windows.Forms.Padding(3)
        Me.tabBenchmarkCompletedRequests.Size = New System.Drawing.Size(437, 328)
        Me.tabBenchmarkCompletedRequests.TabIndex = 2
        Me.tabBenchmarkCompletedRequests.Text = "Completed Requests"
        Me.tabBenchmarkCompletedRequests.UseVisualStyleBackColor = True
        '
        'Chart2
        '
        ChartArea2.AxisX.Title = "run"
        ChartArea2.AxisY.Title = "requests"
        ChartArea2.Name = "ChartArea1"
        Me.Chart2.ChartAreas.Add(ChartArea2)
        Me.Chart2.Dock = System.Windows.Forms.DockStyle.Fill
        Legend2.Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Bottom
        Legend2.Name = "Legend1"
        Me.Chart2.Legends.Add(Legend2)
        Me.Chart2.Location = New System.Drawing.Point(3, 3)
        Me.Chart2.Name = "Chart2"
        Me.Chart2.Size = New System.Drawing.Size(431, 322)
        Me.Chart2.TabIndex = 16
        Me.Chart2.Text = "Chart3"
        Title2.Name = "Title1"
        Title2.Text = "Completed Requests"
        Me.Chart2.Titles.Add(Title2)
        '
        'tabBenchmarkLineChart
        '
        Me.tabBenchmarkLineChart.Controls.Add(Me.Chart3)
        Me.tabBenchmarkLineChart.Location = New System.Drawing.Point(4, 22)
        Me.tabBenchmarkLineChart.Name = "tabBenchmarkLineChart"
        Me.tabBenchmarkLineChart.Padding = New System.Windows.Forms.Padding(3)
        Me.tabBenchmarkLineChart.Size = New System.Drawing.Size(437, 328)
        Me.tabBenchmarkLineChart.TabIndex = 1
        Me.tabBenchmarkLineChart.Text = "Response Time"
        Me.tabBenchmarkLineChart.UseVisualStyleBackColor = True
        '
        'Chart3
        '
        ChartArea3.AxisX.Title = "request"
        ChartArea3.AxisY.Title = "milliseconds"
        ChartArea3.Name = "ChartArea1"
        Me.Chart3.ChartAreas.Add(ChartArea3)
        Me.Chart3.Dock = System.Windows.Forms.DockStyle.Fill
        Legend3.Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Bottom
        Legend3.IsDockedInsideChartArea = False
        Legend3.Name = "Legend1"
        Me.Chart3.Legends.Add(Legend3)
        Me.Chart3.Location = New System.Drawing.Point(3, 3)
        Me.Chart3.Name = "Chart3"
        Me.Chart3.Size = New System.Drawing.Size(431, 322)
        Me.Chart3.TabIndex = 16
        Me.Chart3.Text = "Chart2"
        Title3.Name = "titMain"
        Title3.Text = "Response Time"
        Me.Chart3.Titles.Add(Title3)
        '
        'btnDetectSystemInfo
        '
        Me.btnDetectSystemInfo.Location = New System.Drawing.Point(3, 118)
        Me.btnDetectSystemInfo.Name = "btnDetectSystemInfo"
        Me.btnDetectSystemInfo.Size = New System.Drawing.Size(154, 23)
        Me.btnDetectSystemInfo.TabIndex = 12
        Me.btnDetectSystemInfo.Text = "Detect System Info"
        Me.btnDetectSystemInfo.UseVisualStyleBackColor = True
        '
        'TextBox1
        '
        Me.TextBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.TextBox1.Font = New System.Drawing.Font("Consolas", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBox1.Location = New System.Drawing.Point(6, 174)
        Me.TextBox1.Multiline = True
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.TextBox1.Size = New System.Drawing.Size(147, 181)
        Me.TextBox1.TabIndex = 5
        '
        'tabLog
        '
        Me.tabLog.Controls.Add(Me.txtLog)
        Me.tabLog.Controls.Add(Me.Panel3)
        Me.tabLog.Location = New System.Drawing.Point(4, 22)
        Me.tabLog.Name = "tabLog"
        Me.tabLog.Padding = New System.Windows.Forms.Padding(3)
        Me.tabLog.Size = New System.Drawing.Size(607, 363)
        Me.tabLog.TabIndex = 5
        Me.tabLog.Text = "Log"
        Me.tabLog.UseVisualStyleBackColor = True
        '
        'txtLog
        '
        Me.txtLog.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtLog.Location = New System.Drawing.Point(3, 27)
        Me.txtLog.Multiline = True
        Me.txtLog.Name = "txtLog"
        Me.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtLog.Size = New System.Drawing.Size(601, 333)
        Me.txtLog.TabIndex = 3
        Me.txtLog.WordWrap = False
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.chkEnableLog)
        Me.Panel3.Controls.Add(Me.chkWrapLog)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel3.Location = New System.Drawing.Point(3, 3)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(601, 24)
        Me.Panel3.TabIndex = 12
        '
        'chkEnableLog
        '
        Me.chkEnableLog.AutoSize = True
        Me.chkEnableLog.Checked = True
        Me.chkEnableLog.CheckState = System.Windows.Forms.CheckState.Checked
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
        'tabOptions
        '
        Me.tabOptions.Controls.Add(Me.chkFetchSubResources)
        Me.tabOptions.Location = New System.Drawing.Point(4, 22)
        Me.tabOptions.Name = "tabOptions"
        Me.tabOptions.Padding = New System.Windows.Forms.Padding(3)
        Me.tabOptions.Size = New System.Drawing.Size(607, 363)
        Me.tabOptions.TabIndex = 4
        Me.tabOptions.Text = "Options"
        Me.tabOptions.UseVisualStyleBackColor = True
        '
        'chkFetchSubResources
        '
        Me.chkFetchSubResources.AutoSize = True
        Me.chkFetchSubResources.Location = New System.Drawing.Point(8, 8)
        Me.chkFetchSubResources.Name = "chkFetchSubResources"
        Me.chkFetchSubResources.Size = New System.Drawing.Size(122, 17)
        Me.chkFetchSubResources.TabIndex = 4
        Me.chkFetchSubResources.Text = "Fetch sub-resources"
        Me.chkFetchSubResources.UseVisualStyleBackColor = True
        '
        'Timer1
        '
        Me.Timer1.Enabled = True
        '
        'frmHttpClient
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(615, 451)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Label1)
        Me.Name = "frmHttpClient"
        Me.Text = "Rapid Web Client"
        Me.Panel1.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.TabControl1.ResumeLayout(False)
        Me.tabRaw.ResumeLayout(False)
        Me.tabRaw.PerformLayout()
        Me.tabBenchmark.ResumeLayout(False)
        Me.tabBenchmark.PerformLayout()
        Me.TabControl3.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        Me.TabPage2.ResumeLayout(False)
        Me.TabPage2.PerformLayout()
        Me.TabPage3.ResumeLayout(False)
        Me.TabPage3.PerformLayout()
        Me.TabControl2.ResumeLayout(False)
        Me.tabBenchmarkBarChart.ResumeLayout(False)
        CType(Me.Chart1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tabBenchmarkCompletedRequests.ResumeLayout(False)
        CType(Me.Chart2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tabBenchmarkLineChart.ResumeLayout(False)
        CType(Me.Chart3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tabLog.ResumeLayout(False)
        Me.tabLog.PerformLayout()
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        Me.tabOptions.ResumeLayout(False)
        Me.tabOptions.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents btnGo As System.Windows.Forms.Button
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents tabRaw As System.Windows.Forms.TabPage
    Friend WithEvents txtRaw As System.Windows.Forms.TextBox
    Friend WithEvents tabBenchmark As System.Windows.Forms.TabPage
    Friend WithEvents txtBenchmarkConcurrency As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtBenchmarkNumber As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents tabOptions As System.Windows.Forms.TabPage
    Friend WithEvents chkFetchSubResources As System.Windows.Forms.CheckBox
    Friend WithEvents cboUrl As System.Windows.Forms.ComboBox
    Friend WithEvents tabLog As System.Windows.Forms.TabPage
    Friend WithEvents txtLog As System.Windows.Forms.TextBox
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents chkEnableLog As System.Windows.Forms.CheckBox
    Friend WithEvents chkWrapLog As System.Windows.Forms.CheckBox
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents btnDetectSystemInfo As System.Windows.Forms.Button
    Friend WithEvents TabControl2 As System.Windows.Forms.TabControl
    Friend WithEvents tabBenchmarkBarChart As System.Windows.Forms.TabPage
    Friend WithEvents Chart1 As System.Windows.Forms.DataVisualization.Charting.Chart
    Friend WithEvents tabBenchmarkLineChart As System.Windows.Forms.TabPage
    Friend WithEvents Chart3 As System.Windows.Forms.DataVisualization.Charting.Chart
    Friend WithEvents TabControl3 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtBenchmarkDuration As System.Windows.Forms.TextBox
    Friend WithEvents tabBenchmarkCompletedRequests As System.Windows.Forms.TabPage
    Friend WithEvents Chart2 As System.Windows.Forms.DataVisualization.Charting.Chart
    Friend WithEvents btnClear As System.Windows.Forms.Button
    Friend WithEvents cboBenchmarkTool As System.Windows.Forms.ComboBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents cboBenchmarkTool2 As System.Windows.Forms.ComboBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents TabPage3 As System.Windows.Forms.TabPage
    Friend WithEvents cboBenchmarkTool3 As System.Windows.Forms.ComboBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents txtBenchmarkRampNumber As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents txtBenchmarkRampConcurrency As System.Windows.Forms.TextBox
End Class
