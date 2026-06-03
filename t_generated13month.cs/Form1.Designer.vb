<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
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
        Me.bgWorker = New System.ComponentModel.BackgroundWorker()
        Me.undertime = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.total_late = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.absent = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.days_worked = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.name = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.empid = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgv_list_logs = New System.Windows.Forms.DataGridView()
        Me.overtime = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.groupBox3 = New System.Windows.Forms.GroupBox()
        Me.cbo_payroll_period = New System.Windows.Forms.ComboBox()
        Me.label3 = New System.Windows.Forms.Label()
        Me.label1 = New System.Windows.Forms.Label()
        Me.cbo_employee = New System.Windows.Forms.ComboBox()
        Me.pbar = New System.Windows.Forms.ProgressBar()
        Me.btn_generate = New System.Windows.Forms.Button()
        Me.groupBox2 = New System.Windows.Forms.GroupBox()
        Me.dgvl_userid = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgvl_employee = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgvl_payroll = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgvl_time = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgvl_date = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.groupBox1 = New System.Windows.Forms.GroupBox()
        Me.dgv_list = New System.Windows.Forms.DataGridView()
        Me.pnl_side = New System.Windows.Forms.Panel()
        CType(Me.dgv_list_logs, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.groupBox3.SuspendLayout()
        Me.groupBox2.SuspendLayout()
        Me.groupBox1.SuspendLayout()
        CType(Me.dgv_list, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnl_side.SuspendLayout()
        Me.SuspendLayout()
        '
        'undertime
        '
        Me.undertime.HeaderText = "Undertime"
        Me.undertime.Name = "undertime"
        Me.undertime.ReadOnly = True
        '
        'total_late
        '
        Me.total_late.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.total_late.FillWeight = 58.14433!
        Me.total_late.HeaderText = "Late"
        Me.total_late.Name = "total_late"
        Me.total_late.ReadOnly = True
        Me.total_late.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'absent
        '
        Me.absent.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.absent.FillWeight = 58.14433!
        Me.absent.HeaderText = "Absences"
        Me.absent.Name = "absent"
        Me.absent.ReadOnly = True
        '
        'days_worked
        '
        Me.days_worked.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.days_worked.FillWeight = 58.14433!
        Me.days_worked.HeaderText = "Days Worked"
        Me.days_worked.Name = "days_worked"
        Me.days_worked.ReadOnly = True
        '
        'name
        '
        Me.name.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.name.FillWeight = 58.14433!
        Me.name.HeaderText = "Name"
        Me.name.Name = "name"
        Me.name.ReadOnly = True
        '
        'empid
        '
        Me.empid.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.empid.FillWeight = 50.0!
        Me.empid.HeaderText = "Employee ID"
        Me.empid.Name = "empid"
        Me.empid.ReadOnly = True
        '
        'dgv_list_logs
        '
        Me.dgv_list_logs.AllowUserToAddRows = False
        Me.dgv_list_logs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgv_list_logs.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.empid, Me.name, Me.days_worked, Me.absent, Me.total_late, Me.undertime, Me.overtime})
        Me.dgv_list_logs.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgv_list_logs.Location = New System.Drawing.Point(3, 16)
        Me.dgv_list_logs.Name = "dgv_list_logs"
        Me.dgv_list_logs.ReadOnly = True
        Me.dgv_list_logs.RowHeadersWidth = 25
        Me.dgv_list_logs.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgv_list_logs.Size = New System.Drawing.Size(722, 157)
        Me.dgv_list_logs.TabIndex = 0
        '
        'overtime
        '
        Me.overtime.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.overtime.FillWeight = 58.14433!
        Me.overtime.HeaderText = "Total Overtime"
        Me.overtime.Name = "overtime"
        Me.overtime.ReadOnly = True
        '
        'groupBox3
        '
        Me.groupBox3.Controls.Add(Me.dgv_list_logs)
        Me.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.groupBox3.Location = New System.Drawing.Point(405, 174)
        Me.groupBox3.Name = "groupBox3"
        Me.groupBox3.Size = New System.Drawing.Size(728, 176)
        Me.groupBox3.TabIndex = 10
        Me.groupBox3.TabStop = False
        Me.groupBox3.Text = "Display Generated DTR"
        '
        'cbo_payroll_period
        '
        Me.cbo_payroll_period.BackColor = System.Drawing.Color.DarkGray
        Me.cbo_payroll_period.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbo_payroll_period.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.cbo_payroll_period.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbo_payroll_period.FormattingEnabled = True
        Me.cbo_payroll_period.Location = New System.Drawing.Point(181, 26)
        Me.cbo_payroll_period.Name = "cbo_payroll_period"
        Me.cbo_payroll_period.Size = New System.Drawing.Size(244, 23)
        Me.cbo_payroll_period.TabIndex = 100
        '
        'label3
        '
        Me.label3.AutoSize = True
        Me.label3.Location = New System.Drawing.Point(69, 64)
        Me.label3.Name = "label3"
        Me.label3.Size = New System.Drawing.Size(53, 13)
        Me.label3.TabIndex = 99
        Me.label3.Text = "Employee"
        '
        'label1
        '
        Me.label1.AutoSize = True
        Me.label1.Location = New System.Drawing.Point(69, 34)
        Me.label1.Name = "label1"
        Me.label1.Size = New System.Drawing.Size(71, 13)
        Me.label1.TabIndex = 97
        Me.label1.Text = "Payroll Period"
        '
        'cbo_employee
        '
        Me.cbo_employee.BackColor = System.Drawing.Color.DarkGray
        Me.cbo_employee.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbo_employee.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.cbo_employee.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbo_employee.FormattingEnabled = True
        Me.cbo_employee.Location = New System.Drawing.Point(181, 61)
        Me.cbo_employee.Name = "cbo_employee"
        Me.cbo_employee.Size = New System.Drawing.Size(244, 23)
        Me.cbo_employee.TabIndex = 96
        '
        'pbar
        '
        Me.pbar.Location = New System.Drawing.Point(70, 94)
        Me.pbar.Name = "pbar"
        Me.pbar.Size = New System.Drawing.Size(603, 23)
        Me.pbar.TabIndex = 1
        '
        'btn_generate
        '
        Me.btn_generate.BackColor = System.Drawing.Color.DarkSlateGray
        Me.btn_generate.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btn_generate.ForeColor = System.Drawing.SystemColors.Info
        Me.btn_generate.Location = New System.Drawing.Point(70, 128)
        Me.btn_generate.Name = "btn_generate"
        Me.btn_generate.Size = New System.Drawing.Size(123, 34)
        Me.btn_generate.TabIndex = 0
        Me.btn_generate.Text = "Generate"
        Me.btn_generate.UseVisualStyleBackColor = False
        '
        'groupBox2
        '
        Me.groupBox2.Controls.Add(Me.cbo_payroll_period)
        Me.groupBox2.Controls.Add(Me.label3)
        Me.groupBox2.Controls.Add(Me.label1)
        Me.groupBox2.Controls.Add(Me.cbo_employee)
        Me.groupBox2.Controls.Add(Me.pbar)
        Me.groupBox2.Controls.Add(Me.btn_generate)
        Me.groupBox2.Dock = System.Windows.Forms.DockStyle.Top
        Me.groupBox2.Location = New System.Drawing.Point(405, 0)
        Me.groupBox2.Name = "groupBox2"
        Me.groupBox2.Size = New System.Drawing.Size(728, 174)
        Me.groupBox2.TabIndex = 9
        Me.groupBox2.TabStop = False
        Me.groupBox2.Text = "Generate DTR"
        '
        'dgvl_userid
        '
        Me.dgvl_userid.HeaderText = "User ID"
        Me.dgvl_userid.Name = "dgvl_userid"
        Me.dgvl_userid.ReadOnly = True
        Me.dgvl_userid.Width = 77
        '
        'dgvl_employee
        '
        Me.dgvl_employee.HeaderText = "Employee"
        Me.dgvl_employee.Name = "dgvl_employee"
        Me.dgvl_employee.ReadOnly = True
        '
        'dgvl_payroll
        '
        Me.dgvl_payroll.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.dgvl_payroll.HeaderText = "Payroll Period"
        Me.dgvl_payroll.Name = "dgvl_payroll"
        Me.dgvl_payroll.ReadOnly = True
        '
        'dgvl_time
        '
        Me.dgvl_time.HeaderText = "Time Generated"
        Me.dgvl_time.Name = "dgvl_time"
        Me.dgvl_time.ReadOnly = True
        Me.dgvl_time.Width = 55
        '
        'dgvl_date
        '
        Me.dgvl_date.HeaderText = "Date Generated"
        Me.dgvl_date.Name = "dgvl_date"
        Me.dgvl_date.ReadOnly = True
        Me.dgvl_date.Width = 77
        '
        'groupBox1
        '
        Me.groupBox1.Controls.Add(Me.dgv_list)
        Me.groupBox1.Dock = System.Windows.Forms.DockStyle.Left
        Me.groupBox1.Location = New System.Drawing.Point(0, 0)
        Me.groupBox1.Name = "groupBox1"
        Me.groupBox1.Size = New System.Drawing.Size(399, 350)
        Me.groupBox1.TabIndex = 0
        Me.groupBox1.TabStop = False
        Me.groupBox1.Text = "Generated DTR History"
        '
        'dgv_list
        '
        Me.dgv_list.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgv_list.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.dgvl_date, Me.dgvl_time, Me.dgvl_payroll, Me.dgvl_employee, Me.dgvl_userid})
        Me.dgv_list.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgv_list.Location = New System.Drawing.Point(3, 16)
        Me.dgv_list.Name = "dgv_list"
        Me.dgv_list.ReadOnly = True
        Me.dgv_list.RowHeadersWidth = 25
        Me.dgv_list.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgv_list.Size = New System.Drawing.Size(393, 331)
        Me.dgv_list.TabIndex = 0
        '
        'pnl_side
        '
        Me.pnl_side.BackColor = System.Drawing.Color.SteelBlue
        Me.pnl_side.Controls.Add(Me.groupBox1)
        Me.pnl_side.Dock = System.Windows.Forms.DockStyle.Left
        Me.pnl_side.Location = New System.Drawing.Point(0, 0)
        Me.pnl_side.Name = "pnl_side"
        Me.pnl_side.Size = New System.Drawing.Size(405, 350)
        Me.pnl_side.TabIndex = 8
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1133, 350)
        Me.Controls.Add(Me.groupBox3)
        Me.Controls.Add(Me.groupBox2)
        Me.Controls.Add(Me.pnl_side)
        Me.name = "Form1"
        Me.Text = "Form1"
        CType(Me.dgv_list_logs, System.ComponentModel.ISupportInitialize).EndInit()
        Me.groupBox3.ResumeLayout(False)
        Me.groupBox2.ResumeLayout(False)
        Me.groupBox2.PerformLayout()
        Me.groupBox1.ResumeLayout(False)
        CType(Me.dgv_list, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnl_side.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Private WithEvents bgWorker As System.ComponentModel.BackgroundWorker
    Private WithEvents undertime As System.Windows.Forms.DataGridViewTextBoxColumn
    Private WithEvents total_late As System.Windows.Forms.DataGridViewTextBoxColumn
    Private WithEvents absent As System.Windows.Forms.DataGridViewTextBoxColumn
    Private WithEvents days_worked As System.Windows.Forms.DataGridViewTextBoxColumn
    Private WithEvents name As System.Windows.Forms.DataGridViewTextBoxColumn
    Private WithEvents empid As System.Windows.Forms.DataGridViewTextBoxColumn
    Private WithEvents dgv_list_logs As System.Windows.Forms.DataGridView
    Private WithEvents overtime As System.Windows.Forms.DataGridViewTextBoxColumn
    Private WithEvents groupBox3 As System.Windows.Forms.GroupBox
    Private WithEvents cbo_payroll_period As System.Windows.Forms.ComboBox
    Private WithEvents label3 As System.Windows.Forms.Label
    Private WithEvents label1 As System.Windows.Forms.Label
    Private WithEvents cbo_employee As System.Windows.Forms.ComboBox
    Private WithEvents pbar As System.Windows.Forms.ProgressBar
    Private WithEvents btn_generate As System.Windows.Forms.Button
    Private WithEvents groupBox2 As System.Windows.Forms.GroupBox
    Private WithEvents dgvl_userid As System.Windows.Forms.DataGridViewTextBoxColumn
    Private WithEvents dgvl_employee As System.Windows.Forms.DataGridViewTextBoxColumn
    Private WithEvents dgvl_payroll As System.Windows.Forms.DataGridViewTextBoxColumn
    Private WithEvents dgvl_time As System.Windows.Forms.DataGridViewTextBoxColumn
    Private WithEvents dgvl_date As System.Windows.Forms.DataGridViewTextBoxColumn
    Private WithEvents groupBox1 As System.Windows.Forms.GroupBox
    Private WithEvents dgv_list As System.Windows.Forms.DataGridView
    Private WithEvents pnl_side As System.Windows.Forms.Panel

End Class
