namespace Human_Resource_Information_System
{
    partial class Login
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Login));
            this.label4 = new System.Windows.Forms.Label();
            this.cbo_db = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txt_user = new System.Windows.Forms.TextBox();
            this.txt_pass = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btn_access = new System.Windows.Forms.Button();
            this.btn_timelog = new System.Windows.Forms.Button();
            this.btn_logbox_instruction = new System.Windows.Forms.Button();
            this.btn_register = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.pbox_logo = new System.Windows.Forms.PictureBox();
            this.pbox_company = new System.Windows.Forms.PictureBox();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.txt_version = new DevComponents.DotNetBar.LabelX();
            this.btn_close = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbox_logo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbox_company)).BeginInit();
            this.SuspendLayout();
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Tai Le", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(1, 80);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(61, 16);
            this.label4.TabIndex = 83;
            this.label4.Text = "Database :";
            // 
            // cbo_db
            // 
            this.cbo_db.AccessibleRole = System.Windows.Forms.AccessibleRole.SplitButton;
            this.cbo_db.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.cbo_db.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo_db.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbo_db.Font = new System.Drawing.Font("Microsoft Tai Le", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbo_db.FormattingEnabled = true;
            this.cbo_db.Items.AddRange(new object[] {
            "Official Company Database",
            "Beta Version Database"});
            this.cbo_db.Location = new System.Drawing.Point(79, 77);
            this.cbo_db.Name = "cbo_db";
            this.cbo_db.Size = new System.Drawing.Size(296, 24);
            this.cbo_db.TabIndex = 82;
            this.cbo_db.SelectedIndexChanged += new System.EventHandler(this.cbo_db_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Tai Le", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(1, 340);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(262, 14);
            this.label5.TabIndex = 7;
            this.label5.Text = "Best Screen Size Resolution for at least 1024 x 768";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Tai Le", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(1, 136);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 16);
            this.label2.TabIndex = 11;
            this.label2.Text = "Password :";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Tai Le", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(1, 111);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 16);
            this.label1.TabIndex = 10;
            this.label1.Text = "Username:";
            // 
            // txt_user
            // 
            this.txt_user.Font = new System.Drawing.Font("Microsoft Tai Le", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_user.Location = new System.Drawing.Point(79, 108);
            this.txt_user.Margin = new System.Windows.Forms.Padding(4);
            this.txt_user.Name = "txt_user";
            this.txt_user.Size = new System.Drawing.Size(296, 24);
            this.txt_user.TabIndex = 8;
            this.txt_user.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_user_KeyDown);
            // 
            // txt_pass
            // 
            this.txt_pass.Font = new System.Drawing.Font("Microsoft Tai Le", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_pass.Location = new System.Drawing.Point(79, 133);
            this.txt_pass.Margin = new System.Windows.Forms.Padding(4);
            this.txt_pass.Name = "txt_pass";
            this.txt_pass.PasswordChar = '*';
            this.txt_pass.Size = new System.Drawing.Size(296, 24);
            this.txt_pass.TabIndex = 9;
            this.txt_pass.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_pass_KeyDown);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Transparent;
            this.panel2.Controls.Add(this.btn_access);
            this.panel2.Controls.Add(this.btn_timelog);
            this.panel2.Controls.Add(this.btn_logbox_instruction);
            this.panel2.Controls.Add(this.btn_register);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.cbo_db);
            this.panel2.Controls.Add(this.pbox_logo);
            this.panel2.Controls.Add(this.pbox_company);
            this.panel2.Controls.Add(this.txt_user);
            this.panel2.Controls.Add(this.txt_pass);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 81);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(398, 360);
            this.panel2.TabIndex = 85;
            // 
            // btn_access
            // 
            this.btn_access.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btn_access.BackColor = System.Drawing.Color.DodgerBlue;
            this.btn_access.FlatAppearance.BorderSize = 0;
            this.btn_access.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_access.ForeColor = System.Drawing.SystemColors.Info;
            this.btn_access.Location = new System.Drawing.Point(113, 164);
            this.btn_access.Name = "btn_access";
            this.btn_access.Size = new System.Drawing.Size(181, 48);
            this.btn_access.TabIndex = 95;
            this.btn_access.Text = "Access";
            this.btn_access.UseVisualStyleBackColor = false;
            this.btn_access.Click += new System.EventHandler(this.btn_access_Click);
            // 
            // btn_timelog
            // 
            this.btn_timelog.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btn_timelog.BackColor = System.Drawing.Color.White;
            this.btn_timelog.FlatAppearance.BorderSize = 0;
            this.btn_timelog.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_timelog.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_timelog.ForeColor = System.Drawing.Color.DodgerBlue;
            this.btn_timelog.Image = global::Human_Resource_Information_System.Properties.Resources.upload_logs_30;
            this.btn_timelog.Location = new System.Drawing.Point(4, 278);
            this.btn_timelog.Name = "btn_timelog";
            this.btn_timelog.Size = new System.Drawing.Size(78, 53);
            this.btn_timelog.TabIndex = 94;
            this.btn_timelog.Text = "LogBox";
            this.btn_timelog.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btn_timelog.UseVisualStyleBackColor = false;
            this.btn_timelog.Click += new System.EventHandler(this.btn_timelog_Click);
            // 
            // btn_logbox_instruction
            // 
            this.btn_logbox_instruction.BackColor = System.Drawing.Color.Transparent;
            this.btn_logbox_instruction.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_logbox_instruction.FlatAppearance.BorderSize = 0;
            this.btn_logbox_instruction.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_logbox_instruction.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_logbox_instruction.ForeColor = System.Drawing.SystemColors.Info;
            this.btn_logbox_instruction.Location = new System.Drawing.Point(285, 337);
            this.btn_logbox_instruction.Name = "btn_logbox_instruction";
            this.btn_logbox_instruction.Size = new System.Drawing.Size(99, 20);
            this.btn_logbox_instruction.TabIndex = 93;
            this.btn_logbox_instruction.Text = "Troubleshoot?";
            this.btn_logbox_instruction.UseVisualStyleBackColor = false;
            // 
            // btn_register
            // 
            this.btn_register.BackColor = System.Drawing.Color.Transparent;
            this.btn_register.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_register.FlatAppearance.BorderSize = 0;
            this.btn_register.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_register.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_register.ForeColor = System.Drawing.Color.DodgerBlue;
            this.btn_register.Location = new System.Drawing.Point(1, 215);
            this.btn_register.Name = "btn_register";
            this.btn_register.Size = new System.Drawing.Size(395, 27);
            this.btn_register.TabIndex = 91;
            this.btn_register.Text = "Register";
            this.btn_register.UseVisualStyleBackColor = false;
            this.btn_register.Click += new System.EventHandler(this.btn_register_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Tai Le", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(110, 262);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(70, 14);
            this.label3.TabIndex = 87;
            this.label3.Text = "Powered by:";
            // 
            // pbox_logo
            // 
            this.pbox_logo.BackColor = System.Drawing.Color.White;
            this.pbox_logo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbox_logo.Image = global::Human_Resource_Information_System.Properties.Resources.RA_Logo2023_200x750px1;
            this.pbox_logo.Location = new System.Drawing.Point(107, 277);
            this.pbox_logo.Name = "pbox_logo";
            this.pbox_logo.Size = new System.Drawing.Size(181, 54);
            this.pbox_logo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbox_logo.TabIndex = 81;
            this.pbox_logo.TabStop = false;
            this.pbox_logo.Click += new System.EventHandler(this.pbox_logo_Click);
            // 
            // pbox_company
            // 
            this.pbox_company.BackColor = System.Drawing.Color.Transparent;
            this.pbox_company.Dock = System.Windows.Forms.DockStyle.Top;
            this.pbox_company.ErrorImage = null;
            this.pbox_company.Image = global::Human_Resource_Information_System.Properties.Resources.icon_timekeeping_and_payroll;
            this.pbox_company.Location = new System.Drawing.Point(0, 0);
            this.pbox_company.Name = "pbox_company";
            this.pbox_company.Size = new System.Drawing.Size(398, 62);
            this.pbox_company.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbox_company.TabIndex = 79;
            this.pbox_company.TabStop = false;
            this.pbox_company.Click += new System.EventHandler(this.pbox_company_Click);
            // 
            // labelX1
            // 
            this.labelX1.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelX1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelX1.ForeColor = System.Drawing.Color.White;
            this.labelX1.Location = new System.Drawing.Point(0, 0);
            this.labelX1.Name = "labelX1";
            this.labelX1.SingleLineColor = System.Drawing.Color.White;
            this.labelX1.Size = new System.Drawing.Size(398, 81);
            this.labelX1.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2013;
            this.labelX1.SymbolColor = System.Drawing.Color.White;
            this.labelX1.TabIndex = 86;
            this.labelX1.Text = "Human Resource Information System";
            this.labelX1.TextAlignment = System.Drawing.StringAlignment.Center;
            this.labelX1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.frm_MouseDown);
            this.labelX1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.frm_MouseMove);
            this.labelX1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.frm_MouseUp);
            // 
            // txt_version
            // 
            this.txt_version.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.txt_version.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txt_version.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_version.ForeColor = System.Drawing.Color.White;
            this.txt_version.Location = new System.Drawing.Point(132, 59);
            this.txt_version.Name = "txt_version";
            this.txt_version.Size = new System.Drawing.Size(131, 17);
            this.txt_version.TabIndex = 88;
            this.txt_version.Text = "Version 0.0.0.0";
            this.txt_version.TextAlignment = System.Drawing.StringAlignment.Center;
            // 
            // btn_close
            // 
            this.btn_close.FlatAppearance.BorderSize = 0;
            this.btn_close.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_close.ForeColor = System.Drawing.Color.Transparent;
            this.btn_close.Location = new System.Drawing.Point(373, 4);
            this.btn_close.Name = "btn_close";
            this.btn_close.Size = new System.Drawing.Size(25, 25);
            this.btn_close.TabIndex = 89;
            this.btn_close.Text = "X";
            this.btn_close.UseVisualStyleBackColor = true;
            this.btn_close.Click += new System.EventHandler(this.btn_close_Click_2);
            // 
            // button1
            // 
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.ForeColor = System.Drawing.Color.Transparent;
            this.button1.Location = new System.Drawing.Point(348, 4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(25, 25);
            this.button1.TabIndex = 90;
            this.button1.Text = "__";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(64)))), ((int)(((byte)(109)))));
            this.BackgroundImage = global::Human_Resource_Information_System.Properties.Resources.DotNetBarBackground;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(398, 441);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btn_close);
            this.Controls.Add(this.txt_version);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.labelX1);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "Login";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Human Resource Information System";
            this.Load += new System.EventHandler(this.Login_Load);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbox_logo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbox_company)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_user;
        private System.Windows.Forms.TextBox txt_pass;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.PictureBox pbox_company;
        private System.Windows.Forms.PictureBox pbox_logo;
        private System.Windows.Forms.ComboBox cbo_db;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btn_register;
        private System.Windows.Forms.Button btn_logbox_instruction;
        private DevComponents.DotNetBar.LabelX labelX1;
        private System.Windows.Forms.Button btn_timelog;
        private System.Windows.Forms.Button btn_access;
        private DevComponents.DotNetBar.LabelX txt_version;
        private System.Windows.Forms.Button btn_close;
        private System.Windows.Forms.Button button1;
        //private MetroFramework.Controls.MetroButton btn_access;
    }
}

