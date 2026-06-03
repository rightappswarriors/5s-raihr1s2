namespace Human_Resource_Information_System
{
    partial class journalize
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(journalize));
            this.btn_viewreport = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.cbo_payperiod = new System.Windows.Forms.ComboBox();
            this.lbl_typ = new System.Windows.Forms.Label();
            this.cbo_branch = new System.Windows.Forms.ComboBox();
            this.bgWorker = new System.ComponentModel.BackgroundWorker();
            this.pbar = new System.Windows.Forms.ProgressBar();
            this.btn_close = new System.Windows.Forms.Button();
            this.cbo_journal = new System.Windows.Forms.ComboBox();
            this.btn_proceed = new System.Windows.Forms.Button();
            this.lbl_journal = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cbo_period = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.dtp_to = new System.Windows.Forms.DateTimePicker();
            this.dtp_frm = new System.Windows.Forms.DateTimePicker();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_viewreport
            // 
            this.btn_viewreport.BackColor = System.Drawing.Color.DarkSlateGray;
            this.btn_viewreport.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_viewreport.ForeColor = System.Drawing.SystemColors.Info;
            this.btn_viewreport.Location = new System.Drawing.Point(346, 158);
            this.btn_viewreport.Name = "btn_viewreport";
            this.btn_viewreport.Size = new System.Drawing.Size(95, 50);
            this.btn_viewreport.TabIndex = 16;
            this.btn_viewreport.Text = "View Report";
            this.btn_viewreport.UseVisualStyleBackColor = false;
            this.btn_viewreport.Click += new System.EventHandler(this.btn_viewreport_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 13);
            this.label2.TabIndex = 20;
            this.label2.Text = "Payroll Period";
            // 
            // cbo_payperiod
            // 
            this.cbo_payperiod.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.cbo_payperiod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo_payperiod.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbo_payperiod.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbo_payperiod.FormattingEnabled = true;
            this.cbo_payperiod.Location = new System.Drawing.Point(112, 46);
            this.cbo_payperiod.Name = "cbo_payperiod";
            this.cbo_payperiod.Size = new System.Drawing.Size(461, 24);
            this.cbo_payperiod.TabIndex = 19;
            // 
            // lbl_typ
            // 
            this.lbl_typ.AutoSize = true;
            this.lbl_typ.Location = new System.Drawing.Point(10, 22);
            this.lbl_typ.Name = "lbl_typ";
            this.lbl_typ.Size = new System.Drawing.Size(41, 13);
            this.lbl_typ.TabIndex = 18;
            this.lbl_typ.Text = "Branch";
            // 
            // cbo_branch
            // 
            this.cbo_branch.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.cbo_branch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo_branch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbo_branch.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbo_branch.FormattingEnabled = true;
            this.cbo_branch.Location = new System.Drawing.Point(112, 19);
            this.cbo_branch.Name = "cbo_branch";
            this.cbo_branch.Size = new System.Drawing.Size(463, 24);
            this.cbo_branch.TabIndex = 17;
            // 
            // bgWorker
            // 
            this.bgWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgWorker_DoWork);
            // 
            // pbar
            // 
            this.pbar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pbar.Location = new System.Drawing.Point(0, 231);
            this.pbar.Name = "pbar";
            this.pbar.Size = new System.Drawing.Size(585, 23);
            this.pbar.TabIndex = 14;
            // 
            // btn_close
            // 
            this.btn_close.BackColor = System.Drawing.Color.DarkSlateGray;
            this.btn_close.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_close.ForeColor = System.Drawing.SystemColors.Info;
            this.btn_close.Location = new System.Drawing.Point(144, 158);
            this.btn_close.Name = "btn_close";
            this.btn_close.Size = new System.Drawing.Size(95, 50);
            this.btn_close.TabIndex = 12;
            this.btn_close.Text = "Close";
            this.btn_close.UseVisualStyleBackColor = false;
            this.btn_close.Click += new System.EventHandler(this.btn_close_Click);
            // 
            // cbo_journal
            // 
            this.cbo_journal.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.cbo_journal.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo_journal.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbo_journal.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbo_journal.FormattingEnabled = true;
            this.cbo_journal.Location = new System.Drawing.Point(112, 107);
            this.cbo_journal.Name = "cbo_journal";
            this.cbo_journal.Size = new System.Drawing.Size(460, 24);
            this.cbo_journal.TabIndex = 7;
            // 
            // btn_proceed
            // 
            this.btn_proceed.BackColor = System.Drawing.Color.DarkSlateGray;
            this.btn_proceed.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_proceed.ForeColor = System.Drawing.SystemColors.Info;
            this.btn_proceed.Location = new System.Drawing.Point(245, 158);
            this.btn_proceed.Name = "btn_proceed";
            this.btn_proceed.Size = new System.Drawing.Size(95, 50);
            this.btn_proceed.TabIndex = 13;
            this.btn_proceed.Text = "Proceed";
            this.btn_proceed.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btn_proceed.UseVisualStyleBackColor = false;
            this.btn_proceed.Click += new System.EventHandler(this.btn_proceed_Click);
            // 
            // lbl_journal
            // 
            this.lbl_journal.AutoSize = true;
            this.lbl_journal.Location = new System.Drawing.Point(10, 112);
            this.lbl_journal.Name = "lbl_journal";
            this.lbl_journal.Size = new System.Drawing.Size(75, 13);
            this.lbl_journal.TabIndex = 8;
            this.lbl_journal.Text = "Payroll Journal";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.cbo_period);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.dtp_to);
            this.groupBox1.Controls.Add(this.dtp_frm);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.cbo_payperiod);
            this.groupBox1.Controls.Add(this.lbl_typ);
            this.groupBox1.Controls.Add(this.cbo_branch);
            this.groupBox1.Controls.Add(this.lbl_journal);
            this.groupBox1.Controls.Add(this.cbo_journal);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.ForeColor = System.Drawing.SystemColors.Control;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(585, 149);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Journalize Options";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 81);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 13);
            this.label1.TabIndex = 27;
            this.label1.Text = "Accounting Period";
            // 
            // cbo_period
            // 
            this.cbo_period.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.cbo_period.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo_period.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbo_period.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbo_period.FormattingEnabled = true;
            this.cbo_period.Location = new System.Drawing.Point(111, 77);
            this.cbo_period.Name = "cbo_period";
            this.cbo_period.Size = new System.Drawing.Size(226, 24);
            this.cbo_period.TabIndex = 26;
            this.cbo_period.SelectedIndexChanged += new System.EventHandler(this.cbo_period_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(444, 81);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(20, 13);
            this.label3.TabIndex = 25;
            this.label3.Text = "To";
            // 
            // dtp_to
            // 
            this.dtp_to.Enabled = false;
            this.dtp_to.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtp_to.Location = new System.Drawing.Point(471, 78);
            this.dtp_to.Name = "dtp_to";
            this.dtp_to.Size = new System.Drawing.Size(92, 20);
            this.dtp_to.TabIndex = 24;
            // 
            // dtp_frm
            // 
            this.dtp_frm.CalendarMonthBackground = System.Drawing.Color.Transparent;
            this.dtp_frm.CalendarTitleBackColor = System.Drawing.Color.Transparent;
            this.dtp_frm.Enabled = false;
            this.dtp_frm.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtp_frm.Location = new System.Drawing.Point(343, 79);
            this.dtp_frm.Name = "dtp_frm";
            this.dtp_frm.Size = new System.Drawing.Size(95, 20);
            this.dtp_frm.TabIndex = 23;
            // 
            // journalize
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SteelBlue;
            this.ClientSize = new System.Drawing.Size(585, 254);
            this.Controls.Add(this.btn_viewreport);
            this.Controls.Add(this.pbar);
            this.Controls.Add(this.btn_close);
            this.Controls.Add(this.btn_proceed);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "journalize";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Journalize Payroll";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_viewreport;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbo_payperiod;
        private System.Windows.Forms.Label lbl_typ;
        private System.Windows.Forms.ComboBox cbo_branch;
        private System.ComponentModel.BackgroundWorker bgWorker;
        private System.Windows.Forms.ProgressBar pbar;
        private System.Windows.Forms.Button btn_close;
        private System.Windows.Forms.ComboBox cbo_journal;
        private System.Windows.Forms.Button btn_proceed;
        private System.Windows.Forms.Label lbl_journal;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbo_period;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dtp_to;
        private System.Windows.Forms.DateTimePicker dtp_frm;
    }
}