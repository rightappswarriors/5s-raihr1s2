namespace Human_Resource_Information_System
{
    partial class t_generatedtr
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(t_generatedtr));
            this.pnl_side = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dgv_list = new System.Windows.Forms.DataGridView();
            this.dgvl_date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvl_time = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvl_payroll = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvl_noofemployee = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvl_userid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cbo_department = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cbo_branch = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cbo_employmenttype = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cbo_payouttype = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cbo_payroll_period = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cbo_employee = new System.Windows.Forms.ComboBox();
            this.pbar = new System.Windows.Forms.ProgressBar();
            this.btn_generate = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.dgv_list_logs = new System.Windows.Forms.DataGridView();
            this.empid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.days_worked = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.total_late = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.undertime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.overtime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.work_hr = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.leavecredit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.absent = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fixed_sched = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.poutdesc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.empstatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bgWorker = new System.ComponentModel.BackgroundWorker();
            this.pnl_side.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_list)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_list_logs)).BeginInit();
            this.SuspendLayout();
            // 
            // pnl_side
            // 
            this.pnl_side.BackColor = System.Drawing.Color.SteelBlue;
            this.pnl_side.Controls.Add(this.groupBox1);
            this.pnl_side.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnl_side.Location = new System.Drawing.Point(0, 0);
            this.pnl_side.Name = "pnl_side";
            this.pnl_side.Size = new System.Drawing.Size(405, 471);
            this.pnl_side.TabIndex = 2;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dgv_list);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(405, 471);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Generated DTR History";
            // 
            // dgv_list
            // 
            this.dgv_list.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_list.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgvl_date,
            this.dgvl_time,
            this.dgvl_payroll,
            this.dgvl_noofemployee,
            this.dgvl_userid});
            this.dgv_list.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_list.Location = new System.Drawing.Point(3, 17);
            this.dgv_list.Name = "dgv_list";
            this.dgv_list.ReadOnly = true;
            this.dgv_list.RowHeadersWidth = 15;
            this.dgv_list.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_list.Size = new System.Drawing.Size(399, 451);
            this.dgv_list.TabIndex = 0;
            this.dgv_list.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_list_CellContentClick);
            // 
            // dgvl_date
            // 
            this.dgvl_date.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dgvl_date.HeaderText = "Date Generated";
            this.dgvl_date.Name = "dgvl_date";
            this.dgvl_date.ReadOnly = true;
            this.dgvl_date.Width = 109;
            // 
            // dgvl_time
            // 
            this.dgvl_time.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dgvl_time.HeaderText = "Time";
            this.dgvl_time.Name = "dgvl_time";
            this.dgvl_time.ReadOnly = true;
            this.dgvl_time.Width = 60;
            // 
            // dgvl_payroll
            // 
            this.dgvl_payroll.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dgvl_payroll.HeaderText = "Payroll Period";
            this.dgvl_payroll.Name = "dgvl_payroll";
            this.dgvl_payroll.ReadOnly = true;
            this.dgvl_payroll.Width = 99;
            // 
            // dgvl_noofemployee
            // 
            this.dgvl_noofemployee.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dgvl_noofemployee.HeaderText = "No. Of Employees";
            this.dgvl_noofemployee.Name = "dgvl_noofemployee";
            this.dgvl_noofemployee.ReadOnly = true;
            this.dgvl_noofemployee.Width = 119;
            // 
            // dgvl_userid
            // 
            this.dgvl_userid.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dgvl_userid.HeaderText = "User ID";
            this.dgvl_userid.Name = "dgvl_userid";
            this.dgvl_userid.ReadOnly = true;
            this.dgvl_userid.Width = 68;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cbo_department);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.cbo_branch);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.cbo_employmenttype);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.cbo_payouttype);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.cbo_payroll_period);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.cbo_employee);
            this.groupBox2.Controls.Add(this.pbar);
            this.groupBox2.Controls.Add(this.btn_generate);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox2.Location = new System.Drawing.Point(405, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(705, 252);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Generate Finalized DTR";
            this.groupBox2.Enter += new System.EventHandler(this.groupBox2_Enter);
            // 
            // cbo_department
            // 
            this.cbo_department.BackColor = System.Drawing.Color.DarkGray;
            this.cbo_department.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cbo_department.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbo_department.FormattingEnabled = true;
            this.cbo_department.Location = new System.Drawing.Point(379, 45);
            this.cbo_department.Name = "cbo_department";
            this.cbo_department.Size = new System.Drawing.Size(272, 23);
            this.cbo_department.TabIndex = 108;
            this.cbo_department.Visible = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(379, 27);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(72, 15);
            this.label5.TabIndex = 107;
            this.label5.Text = "Department";
            this.label5.Visible = false;
            // 
            // cbo_branch
            // 
            this.cbo_branch.BackColor = System.Drawing.Color.DarkGray;
            this.cbo_branch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo_branch.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cbo_branch.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbo_branch.FormattingEnabled = true;
            this.cbo_branch.Location = new System.Drawing.Point(51, 45);
            this.cbo_branch.Name = "cbo_branch";
            this.cbo_branch.Size = new System.Drawing.Size(289, 23);
            this.cbo_branch.TabIndex = 106;
            this.cbo_branch.Visible = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(48, 26);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(46, 15);
            this.label6.TabIndex = 105;
            this.label6.Text = "Branch";
            this.label6.Visible = false;
            // 
            // cbo_employmenttype
            // 
            this.cbo_employmenttype.BackColor = System.Drawing.Color.DarkGray;
            this.cbo_employmenttype.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cbo_employmenttype.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbo_employmenttype.FormattingEnabled = true;
            this.cbo_employmenttype.Location = new System.Drawing.Point(382, 134);
            this.cbo_employmenttype.Name = "cbo_employmenttype";
            this.cbo_employmenttype.Size = new System.Drawing.Size(272, 23);
            this.cbo_employmenttype.TabIndex = 104;
            this.cbo_employmenttype.Visible = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(382, 116);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(105, 15);
            this.label4.TabIndex = 103;
            this.label4.Text = "Employment Type";
            this.label4.Visible = false;
            // 
            // cbo_payouttype
            // 
            this.cbo_payouttype.BackColor = System.Drawing.Color.DarkGray;
            this.cbo_payouttype.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cbo_payouttype.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbo_payouttype.FormattingEnabled = true;
            this.cbo_payouttype.Location = new System.Drawing.Point(379, 90);
            this.cbo_payouttype.Name = "cbo_payouttype";
            this.cbo_payouttype.Size = new System.Drawing.Size(272, 23);
            this.cbo_payouttype.TabIndex = 102;
            this.cbo_payouttype.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(379, 72);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 15);
            this.label2.TabIndex = 101;
            this.label2.Text = "Payout Type";
            this.label2.Visible = false;
            // 
            // cbo_payroll_period
            // 
            this.cbo_payroll_period.BackColor = System.Drawing.Color.DarkGray;
            this.cbo_payroll_period.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo_payroll_period.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cbo_payroll_period.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbo_payroll_period.FormattingEnabled = true;
            this.cbo_payroll_period.Location = new System.Drawing.Point(51, 90);
            this.cbo_payroll_period.Name = "cbo_payroll_period";
            this.cbo_payroll_period.Size = new System.Drawing.Size(289, 23);
            this.cbo_payroll_period.TabIndex = 100;
            this.cbo_payroll_period.SelectedIndexChanged += new System.EventHandler(this.cbo_payroll_period_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(48, 116);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(108, 15);
            this.label3.TabIndex = 99;
            this.label3.Text = "Specific Employee";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(48, 71);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 15);
            this.label1.TabIndex = 97;
            this.label1.Text = "Payroll Period";
            // 
            // cbo_employee
            // 
            this.cbo_employee.BackColor = System.Drawing.Color.DarkGray;
            this.cbo_employee.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cbo_employee.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbo_employee.FormattingEnabled = true;
            this.cbo_employee.Location = new System.Drawing.Point(51, 134);
            this.cbo_employee.Name = "cbo_employee";
            this.cbo_employee.Size = new System.Drawing.Size(289, 23);
            this.cbo_employee.TabIndex = 96;
            this.cbo_employee.SelectedIndexChanged += new System.EventHandler(this.cbo_employee_SelectedIndexChanged);
            // 
            // pbar
            // 
            this.pbar.Location = new System.Drawing.Point(51, 163);
            this.pbar.Name = "pbar";
            this.pbar.Size = new System.Drawing.Size(603, 23);
            this.pbar.TabIndex = 1;
            // 
            // btn_generate
            // 
            this.btn_generate.BackColor = System.Drawing.Color.DarkSlateGray;
            this.btn_generate.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_generate.ForeColor = System.Drawing.SystemColors.Info;
            this.btn_generate.Location = new System.Drawing.Point(51, 206);
            this.btn_generate.Name = "btn_generate";
            this.btn_generate.Size = new System.Drawing.Size(226, 34);
            this.btn_generate.TabIndex = 0;
            this.btn_generate.Text = "Generate Finalized DTR";
            this.btn_generate.UseVisualStyleBackColor = false;
            this.btn_generate.Click += new System.EventHandler(this.btn_generate_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.dgv_list_logs);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(405, 252);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(705, 219);
            this.groupBox3.TabIndex = 7;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Display Generated DTR";
            // 
            // dgv_list_logs
            // 
            this.dgv_list_logs.AllowUserToAddRows = false;
            this.dgv_list_logs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_list_logs.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.empid,
            this.name,
            this.days_worked,
            this.total_late,
            this.undertime,
            this.overtime,
            this.work_hr,
            this.leavecredit,
            this.absent,
            this.fixed_sched,
            this.poutdesc,
            this.empstatus});
            this.dgv_list_logs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_list_logs.Location = new System.Drawing.Point(3, 17);
            this.dgv_list_logs.Name = "dgv_list_logs";
            this.dgv_list_logs.ReadOnly = true;
            this.dgv_list_logs.RowHeadersWidth = 15;
            this.dgv_list_logs.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_list_logs.Size = new System.Drawing.Size(699, 199);
            this.dgv_list_logs.TabIndex = 0;
            // 
            // empid
            // 
            this.empid.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.empid.FillWeight = 50F;
            this.empid.HeaderText = "Emp ID";
            this.empid.Name = "empid";
            this.empid.ReadOnly = true;
            this.empid.Width = 68;
            // 
            // name
            // 
            this.name.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.name.FillWeight = 58.14433F;
            this.name.HeaderText = "Name";
            this.name.Name = "name";
            this.name.ReadOnly = true;
            this.name.Width = 66;
            // 
            // days_worked
            // 
            this.days_worked.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.days_worked.FillWeight = 58.14433F;
            this.days_worked.HeaderText = "Days Worked";
            this.days_worked.Name = "days_worked";
            this.days_worked.ReadOnly = true;
            this.days_worked.Width = 96;
            // 
            // total_late
            // 
            this.total_late.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.total_late.FillWeight = 58.14433F;
            this.total_late.HeaderText = "Late";
            this.total_late.Name = "total_late";
            this.total_late.ReadOnly = true;
            this.total_late.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.total_late.Width = 37;
            // 
            // undertime
            // 
            this.undertime.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.undertime.HeaderText = "Undertime";
            this.undertime.Name = "undertime";
            this.undertime.ReadOnly = true;
            this.undertime.Width = 90;
            // 
            // overtime
            // 
            this.overtime.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.overtime.FillWeight = 58.14433F;
            this.overtime.HeaderText = "Total Overtime";
            this.overtime.Name = "overtime";
            this.overtime.ReadOnly = true;
            this.overtime.Width = 102;
            // 
            // work_hr
            // 
            this.work_hr.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.work_hr.DefaultCellStyle = dataGridViewCellStyle1;
            this.work_hr.HeaderText = "Net Worked Hours";
            this.work_hr.Name = "work_hr";
            this.work_hr.ReadOnly = true;
            this.work_hr.Width = 121;
            // 
            // leavecredit
            // 
            this.leavecredit.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.leavecredit.HeaderText = "Leave";
            this.leavecredit.Name = "leavecredit";
            this.leavecredit.ReadOnly = true;
            this.leavecredit.Width = 65;
            // 
            // absent
            // 
            this.absent.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.absent.FillWeight = 58.14433F;
            this.absent.HeaderText = "Absences";
            this.absent.Name = "absent";
            this.absent.ReadOnly = true;
            this.absent.Width = 85;
            // 
            // fixed_sched
            // 
            this.fixed_sched.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.fixed_sched.HeaderText = "Fixed Sched";
            this.fixed_sched.Name = "fixed_sched";
            this.fixed_sched.ReadOnly = true;
            this.fixed_sched.Width = 92;
            // 
            // poutdesc
            // 
            this.poutdesc.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.poutdesc.HeaderText = "Payout Type";
            this.poutdesc.Name = "poutdesc";
            this.poutdesc.ReadOnly = true;
            this.poutdesc.Width = 90;
            // 
            // empstatus
            // 
            this.empstatus.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.empstatus.HeaderText = "Employment Type";
            this.empstatus.Name = "empstatus";
            this.empstatus.ReadOnly = true;
            this.empstatus.Width = 119;
            // 
            // bgWorker
            // 
            this.bgWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgWorker_DoWork);
            // 
            // t_generatedtr
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Info;
            this.ClientSize = new System.Drawing.Size(1110, 471);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.pnl_side);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "t_generatedtr";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Generate DTR";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.t_generatedtr_Load);
            this.pnl_side.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_list)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_list_logs)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnl_side;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView dgv_list;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ProgressBar pbar;
        private System.Windows.Forms.Button btn_generate;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.DataGridView dgv_list_logs;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbo_employee;
        private System.Windows.Forms.ComboBox cbo_payroll_period;
        private System.ComponentModel.BackgroundWorker bgWorker;
        private System.Windows.Forms.ComboBox cbo_payouttype;
        private System.Windows.Forms.ComboBox cbo_employmenttype;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridViewTextBoxColumn empid;
        private System.Windows.Forms.DataGridViewTextBoxColumn name;
        private System.Windows.Forms.DataGridViewTextBoxColumn days_worked;
        private System.Windows.Forms.DataGridViewTextBoxColumn total_late;
        private System.Windows.Forms.DataGridViewTextBoxColumn undertime;
        private System.Windows.Forms.DataGridViewTextBoxColumn overtime;
        private System.Windows.Forms.DataGridViewTextBoxColumn work_hr;
        private System.Windows.Forms.DataGridViewTextBoxColumn leavecredit;
        private System.Windows.Forms.DataGridViewTextBoxColumn absent;
        private System.Windows.Forms.DataGridViewTextBoxColumn fixed_sched;
        private System.Windows.Forms.DataGridViewTextBoxColumn poutdesc;
        private System.Windows.Forms.DataGridViewTextBoxColumn empstatus;
        private System.Windows.Forms.ComboBox cbo_department;
        private System.Windows.Forms.ComboBox cbo_branch;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvl_date;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvl_time;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvl_payroll;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvl_noofemployee;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvl_userid;
    }
}