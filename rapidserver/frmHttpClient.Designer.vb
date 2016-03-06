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
        Dim ChartArea4 As System.Windows.Forms.DataVisualization.Charting.ChartArea = New System.Windows.Forms.DataVisualization.Charting.ChartArea()
        Dim Legend4 As System.Windows.Forms.DataVisualization.Charting.Legend = New System.Windows.Forms.DataVisualization.Charting.Legend()
        Dim Title4 As System.Windows.Forms.DataVisualization.Charting.Title = New System.Windows.Forms.DataVisualization.Charting.Title()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.cboUrl = New System.Windows.Forms.ComboBox()
        Me.btnGo = New System.Windows.Forms.Button()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.txtRaw = New System.Windows.Forms.TextBox()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.TabPage3 = New System.Windows.Forms.TabPage()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.TabPage6 = New System.Windows.Forms.TabPage()
        Me.txtLog = New System.Windows.Forms.TextBox()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.chkEnableLog = New System.Windows.Forms.CheckBox()
        Me.chkWrapLog = New System.Windows.Forms.CheckBox()
        Me.TabPage4 = New System.Windows.Forms.TabPage()
        Me.ComboBox1 = New System.Windows.Forms.ComboBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Chart1 = New System.Windows.Forms.DataVisualization.Charting.Chart()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.txtBenchmarkConcurrency = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtBenchmarkNumber = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.TabPage5 = New System.Windows.Forms.TabPage()
        Me.chkFetchSubResources = New System.Windows.Forms.CheckBox()
        Me.btnDetectSystemInfo = New System.Windows.Forms.Button()
        Me.Panel1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        Me.TabPage3.SuspendLayout()
        Me.TabPage6.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.TabPage4.SuspendLayout()
        CType(Me.Chart1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage5.SuspendLayout()
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
        Me.cboUrl.Items.AddRange(New Object() {"http://127.0.0.1:30000/", "http://127.0.0.1:9666/test-static/", "http://127.0.0.1/", "http://127.0.0.1:30000/aabc/", "http://127.0.0.1:9666/test-static/aabc/", "http://127.0.0.1/aabc/", "http://127.0.0.1:30001/wp/", "http://127.0.0.1:9666/test-dynamic/wp2/", "http://127.0.0.1/wp3/"})
        Me.cboUrl.Location = New System.Drawing.Point(4, 4)
        Me.cboUrl.Name = "cboUrl"
        Me.cboUrl.Size = New System.Drawing.Size(565, 21)
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
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Controls.Add(Me.TabPage3)
        Me.TabControl1.Controls.Add(Me.TabPage6)
        Me.TabControl1.Controls.Add(Me.TabPage4)
        Me.TabControl1.Controls.Add(Me.TabPage5)
        Me.TabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControl1.Location = New System.Drawing.Point(0, 0)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(615, 389)
        Me.TabControl1.TabIndex = 2
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.txtRaw)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(607, 363)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Raw"
        Me.TabPage1.UseVisualStyleBackColor = True
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
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.Label3)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(607, 363)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Gantt"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label3.Location = New System.Drawing.Point(3, 3)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(601, 357)
        Me.Label3.TabIndex = 1
        Me.Label3.Text = "A future project..."
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TabPage3
        '
        Me.TabPage3.Controls.Add(Me.Label2)
        Me.TabPage3.Location = New System.Drawing.Point(4, 22)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage3.Size = New System.Drawing.Size(607, 363)
        Me.TabPage3.TabIndex = 2
        Me.TabPage3.Text = "Rendered"
        Me.TabPage3.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label2.Location = New System.Drawing.Point(3, 3)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(601, 357)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "A future project..."
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TabPage6
        '
        Me.TabPage6.Controls.Add(Me.txtLog)
        Me.TabPage6.Controls.Add(Me.Panel3)
        Me.TabPage6.Location = New System.Drawing.Point(4, 22)
        Me.TabPage6.Name = "TabPage6"
        Me.TabPage6.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage6.Size = New System.Drawing.Size(607, 363)
        Me.TabPage6.TabIndex = 5
        Me.TabPage6.Text = "Log"
        Me.TabPage6.UseVisualStyleBackColor = True
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
        'TabPage4
        '
        Me.TabPage4.Controls.Add(Me.btnDetectSystemInfo)
        Me.TabPage4.Controls.Add(Me.ComboBox1)
        Me.TabPage4.Controls.Add(Me.Label6)
        Me.TabPage4.Controls.Add(Me.Chart1)
        Me.TabPage4.Controls.Add(Me.TextBox1)
        Me.TabPage4.Controls.Add(Me.txtBenchmarkConcurrency)
        Me.TabPage4.Controls.Add(Me.Label5)
        Me.TabPage4.Controls.Add(Me.txtBenchmarkNumber)
        Me.TabPage4.Controls.Add(Me.Label4)
        Me.TabPage4.Location = New System.Drawing.Point(4, 22)
        Me.TabPage4.Name = "TabPage4"
        Me.TabPage4.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage4.Size = New System.Drawing.Size(607, 363)
        Me.TabPage4.TabIndex = 3
        Me.TabPage4.Text = "Benchmark"
        Me.TabPage4.UseVisualStyleBackColor = True
        '
        'ComboBox1
        '
        Me.ComboBox1.FormattingEnabled = True
        Me.ComboBox1.Items.AddRange(New Object() {"ab"})
        Me.ComboBox1.Location = New System.Drawing.Point(74, 57)
        Me.ComboBox1.Name = "ComboBox1"
        Me.ComboBox1.Size = New System.Drawing.Size(79, 21)
        Me.ComboBox1.TabIndex = 11
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(3, 62)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(31, 13)
        Me.Label6.TabIndex = 7
        Me.Label6.Text = "Tool:"
        '
        'Chart1
        '
        Me.Chart1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        ChartArea4.Name = "ChartArea1"
        Me.Chart1.ChartAreas.Add(ChartArea4)
        Legend4.Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Bottom
        Legend4.Name = "Legend1"
        Me.Chart1.Legends.Add(Legend4)
        Me.Chart1.Location = New System.Drawing.Point(159, 5)
        Me.Chart1.Name = "Chart1"
        Me.Chart1.Size = New System.Drawing.Size(445, 355)
        Me.Chart1.TabIndex = 6
        Me.Chart1.Text = "Chart1"
        Title4.Name = "Title1"
        Title4.Text = "Requests Per Second (RPS)"
        Me.Chart1.Titles.Add(Title4)
        '
        'TextBox1
        '
        Me.TextBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.TextBox1.Font = New System.Drawing.Font("Consolas", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBox1.Location = New System.Drawing.Point(6, 110)
        Me.TextBox1.Multiline = True
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.TextBox1.Size = New System.Drawing.Size(147, 245)
        Me.TextBox1.TabIndex = 5
        '
        'txtBenchmarkConcurrency
        '
        Me.txtBenchmarkConcurrency.Location = New System.Drawing.Point(74, 31)
        Me.txtBenchmarkConcurrency.Name = "txtBenchmarkConcurrency"
        Me.txtBenchmarkConcurrency.Size = New System.Drawing.Size(44, 20)
        Me.txtBenchmarkConcurrency.TabIndex = 3
        Me.txtBenchmarkConcurrency.Text = "100"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(3, 35)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(70, 13)
        Me.Label5.TabIndex = 2
        Me.Label5.Text = "Concurrency:"
        '
        'txtBenchmarkNumber
        '
        Me.txtBenchmarkNumber.Location = New System.Drawing.Point(74, 5)
        Me.txtBenchmarkNumber.Name = "txtBenchmarkNumber"
        Me.txtBenchmarkNumber.Size = New System.Drawing.Size(44, 20)
        Me.txtBenchmarkNumber.TabIndex = 1
        Me.txtBenchmarkNumber.Text = "1000"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(3, 9)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(47, 13)
        Me.Label4.TabIndex = 0
        Me.Label4.Text = "Number:"
        '
        'TabPage5
        '
        Me.TabPage5.Controls.Add(Me.chkFetchSubResources)
        Me.TabPage5.Location = New System.Drawing.Point(4, 22)
        Me.TabPage5.Name = "TabPage5"
        Me.TabPage5.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage5.Size = New System.Drawing.Size(607, 363)
        Me.TabPage5.TabIndex = 4
        Me.TabPage5.Text = "Options"
        Me.TabPage5.UseVisualStyleBackColor = True
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
        'btnDetectSystemInfo
        '
        Me.btnDetectSystemInfo.Location = New System.Drawing.Point(6, 84)
        Me.btnDetectSystemInfo.Name = "btnDetectSystemInfo"
        Me.btnDetectSystemInfo.Size = New System.Drawing.Size(147, 23)
        Me.btnDetectSystemInfo.TabIndex = 12
        Me.btnDetectSystemInfo.Text = "Detect System Info"
        Me.btnDetectSystemInfo.UseVisualStyleBackColor = True
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
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        Me.TabPage2.ResumeLayout(False)
        Me.TabPage3.ResumeLayout(False)
        Me.TabPage6.ResumeLayout(False)
        Me.TabPage6.PerformLayout()
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        Me.TabPage4.ResumeLayout(False)
        Me.TabPage4.PerformLayout()
        CType(Me.Chart1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage5.ResumeLayout(False)
        Me.TabPage5.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents btnGo As System.Windows.Forms.Button
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents txtRaw As System.Windows.Forms.TextBox
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents TabPage3 As System.Windows.Forms.TabPage
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents TabPage4 As System.Windows.Forms.TabPage
    Friend WithEvents txtBenchmarkConcurrency As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtBenchmarkNumber As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents TabPage5 As System.Windows.Forms.TabPage
    Friend WithEvents chkFetchSubResources As System.Windows.Forms.CheckBox
    Friend WithEvents cboUrl As System.Windows.Forms.ComboBox
    Friend WithEvents TabPage6 As System.Windows.Forms.TabPage
    Friend WithEvents txtLog As System.Windows.Forms.TextBox
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents chkEnableLog As System.Windows.Forms.CheckBox
    Friend WithEvents chkWrapLog As System.Windows.Forms.CheckBox
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents Chart1 As System.Windows.Forms.DataVisualization.Charting.Chart
    Friend WithEvents ComboBox1 As System.Windows.Forms.ComboBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents btnDetectSystemInfo As System.Windows.Forms.Button
End Class
