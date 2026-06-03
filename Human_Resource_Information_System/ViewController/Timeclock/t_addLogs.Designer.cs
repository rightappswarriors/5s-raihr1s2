namespace Human_Resource_Information_System
{
    partial class t_addLogs
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
            this.label1 = new System.Windows.Forms.Label();
            this.dtp_work_date = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.cbo_status = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.dtp_timeLogs = new System.Windows.Forms.DateTimePicker();
            this.btn_save = new System.Windows.Forms.Button();
            this.btn_back = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.dtp_timein = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.dtp_timeout = new System.Windows.Forms.DateTimePicker();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 30);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Work Date";
            // 
            // dtp_work_date
            // 
            this.dtp_work_date.Location = new System.Drawing.Point(96, 25);
            this.dtp_work_date.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dtp_work_date.Name = "dtp_work_date";
            this.dtp_work_date.Size = new System.Drawing.Size(154, 22);
            this.dtp_work_date.TabIndex = 1;
            this.dtp_work_date.ValueChanged += new System.EventHandler(this.dtp_work_date_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 60);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 16);
            this.label2.TabIndex = 2;
            this.label2.Text = "Time Log";
            // 
            // cbo_status
            // 
            this.cbo_status.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.cbo_status.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo_status.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbo_status.FormattingEnabled = true;
            this.cbo_status.Items.AddRange(new object[] {
            "TIME IN",
            "BREAK OUT",
            "BREAK IN",
            "TIME OUT"});
            this.cbo_status.Location = new System.Drawing.Point(96, 84);
            this.cbo_status.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cbo_status.Name = "cbo_status";
            this.cbo_status.Size = new System.Drawing.Size(281, 24);
            this.cbo_status.TabIndex = 67;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(17, 87);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(44, 16);
            this.label4.TabIndex = 66;
            this.label4.Text = "Status";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // dtp_timeLogs
            // 
            this.dtp_timeLogs.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dtp_timeLogs.Location = new System.Drawing.Point(96, 54);
            this.dtp_timeLogs.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dtp_timeLogs.Name = "dtp_timeLogs";
            this.dtp_timeLogs.Size = new System.Drawing.Size(154, 22);
            this.dtp_timeLogs.TabIndex = 92;
            this.dtp_timeLogs.ValueChanged += new System.EventHandler(this.dtp_timeLogs_ValueChanged);
            // 
            // btn_save
            // 
            this.btn_save.BackColor = System.Drawing.Color.DarkSlateGray;
            this.btn_save.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_save.ForeColor = System.Drawing.SystemColors.Info;
            this.btn_save.Location = new System.Drawing.Point(209, 162);
            this.btn_save.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btn_save.Name = "btn_save";
            this.btn_save.Size = new System.Drawing.Size(127, 53);
            this.btn_save.TabIndex = 93;
            this.btn_save.Text = "Save Logs";
            this.btn_save.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btn_save.UseVisualStyleBackColor = false;
            this.btn_save.Click += new System.EventHandler(this.btn_save_Click);
            // 
            // btn_back
            // 
            this.btn_back.BackColor = System.Drawing.Color.DarkOrange;
            this.btn_back.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_back.ForeColor = System.Drawing.SystemColors.Info;
            this.btn_back.Location = new System.Drawing.Point(74, 162);
            this.btn_back.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btn_back.Name = "btn_back";
            this.btn_back.Size = new System.Drawing.Size(127, 53);
            this.btn_back.TabIndex = 95;
            this.btn_back.Text = "Cancel";
            this.btn_back.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btn_back.UseVisualStyleBackColor = false;
            this.btn_back.Click += new System.EventHandler(this.btn_back_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(258, 30);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(51, 16);
            this.label3.TabIndex = 96;
            this.label3.Text = "Time In";
            this.label3.Visible = false;
            // 
            // dtp_timein
            // 
            this.dtp_timein.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dtp_timein.Location = new System.Drawing.Point(293, 24);
            this.dtp_timein.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dtp_timein.Name = "dtp_timein";
            this.dtp_timein.Size = new System.Drawing.Size(97, 22);
            this.dtp_timein.TabIndex = 97;
            this.dtp_timein.Visible = false;
            this.dtp_timein.ValueChanged += new System.EventHandler(this.dateTimePicker1_ValueChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(258, 59);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(61, 16);
            this.label5.TabIndex = 98;
            this.label5.Text = "Time Out";
            this.label5.Visible = false;
            // 
            // dtp_timeout
            // 
            this.dtp_timeout.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dtp_timeout.Location = new System.Drawing.Point(293, 55);
            this.dtp_timeout.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dtp_timeout.Name = "dtp_timeout";
            this.dtp_timeout.Size = new System.Drawing.Size(97, 22);
            this.dtp_timeout.TabIndex = 99;
            this.dtp_timeout.Visible = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.dtp_timeout);
            this.groupBox1.Controls.Add(this.dtp_work_date);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.dtp_timein);
            this.groupBox1.Controls.Add(this.dtp_timeLogs);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.cbo_status);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(403, 125);
            this.groupBox1.TabIndex = 100;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Time Log Details";
            // 
            // t_addLogs
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Info;
            this.ClientSize = new System.Drawing.Size(403, 228);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btn_back);
            this.Controls.Add(this.btn_save);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "t_addLogs";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Add time logs";
            this.Load += new System.EventHandler(this.t_addLogs_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtp_work_date;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbo_status;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker dtp_timeLogs;
        private System.Windows.Forms.Button btn_save;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btn_back;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dtp_timein;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker dtp_timeout;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}