namespace Human_Resource_Information_System
{
    partial class m_ShiftSchedule
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
            this.label4 = new System.Windows.Forms.Label();
            this.txt_code = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.tpg_info = new System.Windows.Forms.TabPage();
            this.label1 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txt_name = new System.Windows.Forms.TextBox();
            this.dtp_time_out = new System.Windows.Forms.DateTimePicker();
            this.dtp_time_in = new System.Windows.Forms.DateTimePicker();
            this.label8 = new System.Windows.Forms.Label();
            this.tpg_list = new System.Windows.Forms.TabPage();
            this.dgv_list = new System.Windows.Forms.DataGridView();
            this.code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.time_in = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.time_out = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tbcntrl_main = new System.Windows.Forms.TabControl();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btn_save = new System.Windows.Forms.Button();
            this.btn_back = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.btn_print = new System.Windows.Forms.Button();
            this.btn_additem = new System.Windows.Forms.Button();
            this.btn_delitem = new System.Windows.Forms.Button();
            this.btn_upditem = new System.Windows.Forms.Button();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.cbo_searchby = new System.Windows.Forms.ComboBox();
            this.txt_search = new System.Windows.Forms.TextBox();
            this.btn_search = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.tpg_opt_1 = new System.Windows.Forms.TabPage();
            this.tbcntrl_option = new System.Windows.Forms.TabControl();
            this.tpg_opt_2 = new System.Windows.Forms.TabPage();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tpg_info.SuspendLayout();
            this.tpg_list.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_list)).BeginInit();
            this.tbcntrl_main.SuspendLayout();
            this.panel2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.tpg_opt_1.SuspendLayout();
            this.tbcntrl_option.SuspendLayout();
            this.tpg_opt_2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Red;
            this.label4.Location = new System.Drawing.Point(214, 19);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(16, 20);
            this.label4.TabIndex = 46;
            this.label4.Text = "*";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // txt_code
            // 
            this.txt_code.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_code.Location = new System.Drawing.Point(84, 19);
            this.txt_code.MaxLength = 8;
            this.txt_code.Name = "txt_code";
            this.txt_code.Size = new System.Drawing.Size(123, 20);
            this.txt_code.TabIndex = 35;
            this.txt_code.TextChanged += new System.EventHandler(this.txt_code_TextChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(12, 77);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(42, 13);
            this.label10.TabIndex = 33;
            this.label10.Text = "Time In";
            this.label10.Click += new System.EventHandler(this.label10_Click);
            // 
            // tpg_info
            // 
            this.tpg_info.BackColor = System.Drawing.SystemColors.Info;
            this.tpg_info.Controls.Add(this.label1);
            this.tpg_info.Controls.Add(this.label5);
            this.tpg_info.Controls.Add(this.txt_name);
            this.tpg_info.Controls.Add(this.dtp_time_out);
            this.tpg_info.Controls.Add(this.dtp_time_in);
            this.tpg_info.Controls.Add(this.label4);
            this.tpg_info.Controls.Add(this.txt_code);
            this.tpg_info.Controls.Add(this.label8);
            this.tpg_info.Controls.Add(this.label10);
            this.tpg_info.Location = new System.Drawing.Point(4, 22);
            this.tpg_info.Name = "tpg_info";
            this.tpg_info.Padding = new System.Windows.Forms.Padding(3);
            this.tpg_info.Size = new System.Drawing.Size(536, 481);
            this.tpg_info.TabIndex = 2;
            this.tpg_info.Text = "Shift Schedule Info";
            this.tpg_info.Click += new System.EventHandler(this.tpg_info_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 102);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 13);
            this.label1.TabIndex = 107;
            this.label1.Text = "Time Out ";
            this.label1.Click += new System.EventHandler(this.label1_Click_1);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(15, 55);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(60, 13);
            this.label5.TabIndex = 106;
            this.label5.Text = "Description";
            this.label5.Click += new System.EventHandler(this.label5_Click);
            // 
            // txt_name
            // 
            this.txt_name.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_name.Location = new System.Drawing.Point(84, 45);
            this.txt_name.MaxLength = 2000;
            this.txt_name.Name = "txt_name";
            this.txt_name.Size = new System.Drawing.Size(301, 20);
            this.txt_name.TabIndex = 105;
            this.txt_name.TextChanged += new System.EventHandler(this.txt_name_TextChanged);
            // 
            // dtp_time_out
            // 
            this.dtp_time_out.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dtp_time_out.Location = new System.Drawing.Point(84, 97);
            this.dtp_time_out.Name = "dtp_time_out";
            this.dtp_time_out.Size = new System.Drawing.Size(103, 20);
            this.dtp_time_out.TabIndex = 93;
            this.dtp_time_out.ValueChanged += new System.EventHandler(this.dtp_time_out_ValueChanged);
            // 
            // dtp_time_in
            // 
            this.dtp_time_in.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dtp_time_in.Location = new System.Drawing.Point(84, 71);
            this.dtp_time_in.Name = "dtp_time_in";
            this.dtp_time_in.Size = new System.Drawing.Size(103, 20);
            this.dtp_time_in.TabIndex = 92;
            this.dtp_time_in.ValueChanged += new System.EventHandler(this.dtp_time_in_ValueChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(12, 26);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(32, 13);
            this.label8.TabIndex = 32;
            this.label8.Text = "Code";
            this.label8.Click += new System.EventHandler(this.label8_Click);
            // 
            // tpg_list
            // 
            this.tpg_list.Controls.Add(this.dgv_list);
            this.tpg_list.Location = new System.Drawing.Point(4, 22);
            this.tpg_list.Name = "tpg_list";
            this.tpg_list.Padding = new System.Windows.Forms.Padding(3);
            this.tpg_list.Size = new System.Drawing.Size(536, 481);
            this.tpg_list.TabIndex = 1;
            this.tpg_list.Text = "Shift Schedule List";
            this.tpg_list.UseVisualStyleBackColor = true;
            this.tpg_list.Click += new System.EventHandler(this.tpg_list_Click);
            // 
            // dgv_list
            // 
            this.dgv_list.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_list.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.code,
            this.name,
            this.time_in,
            this.time_out});
            this.dgv_list.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_list.Location = new System.Drawing.Point(3, 3);
            this.dgv_list.Name = "dgv_list";
            this.dgv_list.ReadOnly = true;
            this.dgv_list.RowHeadersWidth = 25;
            this.dgv_list.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_list.Size = new System.Drawing.Size(530, 475);
            this.dgv_list.TabIndex = 1;
            this.dgv_list.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_list_CellContentClick);
            // 
            // code
            // 
            this.code.HeaderText = "CODE";
            this.code.Name = "code";
            this.code.ReadOnly = true;
            this.code.Width = 125;
            // 
            // name
            // 
            this.name.HeaderText = "DESCRIPTION";
            this.name.Name = "name";
            this.name.ReadOnly = true;
            this.name.Width = 125;
            // 
            // time_in
            // 
            this.time_in.HeaderText = "TIME IN";
            this.time_in.Name = "time_in";
            this.time_in.ReadOnly = true;
            this.time_in.Width = 125;
            // 
            // time_out
            // 
            this.time_out.HeaderText = "TIME OUT";
            this.time_out.Name = "time_out";
            this.time_out.ReadOnly = true;
            this.time_out.Width = 125;
            // 
            // tbcntrl_main
            // 
            this.tbcntrl_main.Controls.Add(this.tpg_list);
            this.tbcntrl_main.Controls.Add(this.tpg_info);
            this.tbcntrl_main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbcntrl_main.Location = new System.Drawing.Point(0, 0);
            this.tbcntrl_main.Name = "tbcntrl_main";
            this.tbcntrl_main.SelectedIndex = 0;
            this.tbcntrl_main.Size = new System.Drawing.Size(544, 507);
            this.tbcntrl_main.TabIndex = 7;
            this.tbcntrl_main.SelectedIndexChanged += new System.EventHandler(this.tbcntrl_main_SelectedIndexChanged);
            this.tbcntrl_main.Selecting += new System.Windows.Forms.TabControlCancelEventHandler(this.tbcntrl_main_Selecting);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.tbcntrl_main);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(221, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(544, 507);
            this.panel2.TabIndex = 9;
            this.panel2.Paint += new System.Windows.Forms.PaintEventHandler(this.panel2_Paint);
            // 
            // btn_save
            // 
            this.btn_save.BackColor = System.Drawing.Color.DarkSlateGray;
            this.btn_save.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_save.Location = new System.Drawing.Point(12, 24);
            this.btn_save.Name = "btn_save";
            this.btn_save.Size = new System.Drawing.Size(160, 46);
            this.btn_save.TabIndex = 8;
            this.btn_save.Text = "Save";
            this.btn_save.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btn_save.UseVisualStyleBackColor = false;
            this.btn_save.Click += new System.EventHandler(this.btn_save_Click);
            // 
            // btn_back
            // 
            this.btn_back.BackColor = System.Drawing.Color.DarkSlateGray;
            this.btn_back.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_back.Location = new System.Drawing.Point(12, 73);
            this.btn_back.Name = "btn_back";
            this.btn_back.Size = new System.Drawing.Size(160, 46);
            this.btn_back.TabIndex = 1;
            this.btn_back.Text = "Back";
            this.btn_back.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btn_back.UseVisualStyleBackColor = false;
            this.btn_back.Click += new System.EventHandler(this.btn_back_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btn_save);
            this.groupBox3.Controls.Add(this.btn_back);
            this.groupBox3.ForeColor = System.Drawing.SystemColors.Info;
            this.groupBox3.Location = new System.Drawing.Point(10, 14);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(184, 130);
            this.groupBox3.TabIndex = 48;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Option";
            this.groupBox3.Enter += new System.EventHandler(this.groupBox3_Enter);
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.SteelBlue;
            this.panel4.Controls.Add(this.groupBox3);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(3, 3);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(207, 475);
            this.panel4.TabIndex = 0;
            this.panel4.Paint += new System.Windows.Forms.PaintEventHandler(this.panel4_Paint);
            // 
            // btn_print
            // 
            this.btn_print.BackColor = System.Drawing.Color.DarkSlateGray;
            this.btn_print.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_print.Location = new System.Drawing.Point(12, 204);
            this.btn_print.Name = "btn_print";
            this.btn_print.Size = new System.Drawing.Size(160, 55);
            this.btn_print.TabIndex = 9;
            this.btn_print.Text = "Print List";
            this.btn_print.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btn_print.UseVisualStyleBackColor = false;
            this.btn_print.Click += new System.EventHandler(this.btn_print_Click);
            // 
            // btn_additem
            // 
            this.btn_additem.BackColor = System.Drawing.Color.DarkSlateGray;
            this.btn_additem.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_additem.Location = new System.Drawing.Point(12, 21);
            this.btn_additem.Name = "btn_additem";
            this.btn_additem.Size = new System.Drawing.Size(160, 55);
            this.btn_additem.TabIndex = 8;
            this.btn_additem.Text = "Add New";
            this.btn_additem.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btn_additem.UseVisualStyleBackColor = false;
            this.btn_additem.Click += new System.EventHandler(this.btn_additem_Click);
            // 
            // btn_delitem
            // 
            this.btn_delitem.BackColor = System.Drawing.Color.Maroon;
            this.btn_delitem.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_delitem.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btn_delitem.Location = new System.Drawing.Point(12, 143);
            this.btn_delitem.Name = "btn_delitem";
            this.btn_delitem.Size = new System.Drawing.Size(160, 55);
            this.btn_delitem.TabIndex = 7;
            this.btn_delitem.Text = "Delete";
            this.btn_delitem.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btn_delitem.UseVisualStyleBackColor = false;
            this.btn_delitem.Click += new System.EventHandler(this.btn_delitem_Click);
            // 
            // btn_upditem
            // 
            this.btn_upditem.BackColor = System.Drawing.Color.DarkSlateGray;
            this.btn_upditem.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_upditem.Location = new System.Drawing.Point(12, 82);
            this.btn_upditem.Name = "btn_upditem";
            this.btn_upditem.Size = new System.Drawing.Size(160, 55);
            this.btn_upditem.TabIndex = 1;
            this.btn_upditem.Text = "Update";
            this.btn_upditem.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btn_upditem.UseVisualStyleBackColor = false;
            this.btn_upditem.Click += new System.EventHandler(this.btn_upditem_Click);
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.btn_print);
            this.groupBox7.Controls.Add(this.btn_additem);
            this.groupBox7.Controls.Add(this.btn_delitem);
            this.groupBox7.Controls.Add(this.btn_upditem);
            this.groupBox7.ForeColor = System.Drawing.SystemColors.Info;
            this.groupBox7.Location = new System.Drawing.Point(11, 13);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(184, 273);
            this.groupBox7.TabIndex = 46;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Main Option";
            this.groupBox7.Enter += new System.EventHandler(this.groupBox7_Enter);
            // 
            // cbo_searchby
            // 
            this.cbo_searchby.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.cbo_searchby.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo_searchby.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbo_searchby.FormattingEnabled = true;
            this.cbo_searchby.Location = new System.Drawing.Point(12, 33);
            this.cbo_searchby.Name = "cbo_searchby";
            this.cbo_searchby.Size = new System.Drawing.Size(160, 21);
            this.cbo_searchby.TabIndex = 10;
            this.cbo_searchby.SelectedIndexChanged += new System.EventHandler(this.cbo_searchby_SelectedIndexChanged);
            // 
            // txt_search
            // 
            this.txt_search.Location = new System.Drawing.Point(12, 72);
            this.txt_search.Name = "txt_search";
            this.txt_search.Size = new System.Drawing.Size(160, 20);
            this.txt_search.TabIndex = 9;
            this.txt_search.TextChanged += new System.EventHandler(this.txt_search_TextChanged);
            // 
            // btn_search
            // 
            this.btn_search.BackColor = System.Drawing.Color.DarkSlateGray;
            this.btn_search.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_search.Location = new System.Drawing.Point(12, 100);
            this.btn_search.Name = "btn_search";
            this.btn_search.Size = new System.Drawing.Size(160, 55);
            this.btn_search.TabIndex = 8;
            this.btn_search.Text = "Search Now";
            this.btn_search.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btn_search.UseVisualStyleBackColor = false;
            this.btn_search.Click += new System.EventHandler(this.btn_search_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cbo_searchby);
            this.groupBox2.Controls.Add(this.txt_search);
            this.groupBox2.Controls.Add(this.btn_search);
            this.groupBox2.ForeColor = System.Drawing.SystemColors.Info;
            this.groupBox2.Location = new System.Drawing.Point(11, 292);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(184, 165);
            this.groupBox2.TabIndex = 47;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Search Option";
            this.groupBox2.Enter += new System.EventHandler(this.groupBox2_Enter);
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.SteelBlue;
            this.panel3.Controls.Add(this.groupBox2);
            this.panel3.Controls.Add(this.groupBox7);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(3, 3);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(207, 475);
            this.panel3.TabIndex = 0;
            this.panel3.Paint += new System.Windows.Forms.PaintEventHandler(this.panel3_Paint);
            // 
            // tpg_opt_1
            // 
            this.tpg_opt_1.Controls.Add(this.panel3);
            this.tpg_opt_1.Location = new System.Drawing.Point(4, 4);
            this.tpg_opt_1.Name = "tpg_opt_1";
            this.tpg_opt_1.Padding = new System.Windows.Forms.Padding(3);
            this.tpg_opt_1.Size = new System.Drawing.Size(213, 481);
            this.tpg_opt_1.TabIndex = 0;
            this.tpg_opt_1.UseVisualStyleBackColor = true;
            this.tpg_opt_1.Click += new System.EventHandler(this.tpg_opt_1_Click);
            // 
            // tbcntrl_option
            // 
            this.tbcntrl_option.Alignment = System.Windows.Forms.TabAlignment.Bottom;
            this.tbcntrl_option.Controls.Add(this.tpg_opt_1);
            this.tbcntrl_option.Controls.Add(this.tpg_opt_2);
            this.tbcntrl_option.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbcntrl_option.Location = new System.Drawing.Point(0, 0);
            this.tbcntrl_option.Name = "tbcntrl_option";
            this.tbcntrl_option.SelectedIndex = 0;
            this.tbcntrl_option.Size = new System.Drawing.Size(221, 507);
            this.tbcntrl_option.TabIndex = 49;
            this.tbcntrl_option.SelectedIndexChanged += new System.EventHandler(this.tbcntrl_option_SelectedIndexChanged);
            this.tbcntrl_option.Selecting += new System.Windows.Forms.TabControlCancelEventHandler(this.tbcntrl_option_Selecting);
            // 
            // tpg_opt_2
            // 
            this.tpg_opt_2.BackColor = System.Drawing.SystemColors.HotTrack;
            this.tpg_opt_2.Controls.Add(this.panel4);
            this.tpg_opt_2.Location = new System.Drawing.Point(4, 4);
            this.tpg_opt_2.Name = "tpg_opt_2";
            this.tpg_opt_2.Padding = new System.Windows.Forms.Padding(3);
            this.tpg_opt_2.Size = new System.Drawing.Size(213, 481);
            this.tpg_opt_2.TabIndex = 1;
            this.tpg_opt_2.UseVisualStyleBackColor = true;
            this.tpg_opt_2.Click += new System.EventHandler(this.tpg_opt_2_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.SteelBlue;
            this.panel1.Controls.Add(this.tbcntrl_option);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(221, 507);
            this.panel1.TabIndex = 8;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // m_ShiftSchedule
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Info;
            this.ClientSize = new System.Drawing.Size(765, 507);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "m_ShiftSchedule";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Shift Schedule";
            this.Load += new System.EventHandler(this.m_ShiftSchedule_Load);
            this.tpg_info.ResumeLayout(false);
            this.tpg_info.PerformLayout();
            this.tpg_list.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_list)).EndInit();
            this.tbcntrl_main.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.groupBox7.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.tpg_opt_1.ResumeLayout(false);
            this.tbcntrl_option.ResumeLayout(false);
            this.tpg_opt_2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txt_code;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TabPage tpg_info;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TabPage tpg_list;
        private System.Windows.Forms.DataGridView dgv_list;
        private System.Windows.Forms.TabControl tbcntrl_main;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btn_save;
        private System.Windows.Forms.Button btn_back;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Button btn_print;
        private System.Windows.Forms.Button btn_additem;
        private System.Windows.Forms.Button btn_delitem;
        private System.Windows.Forms.Button btn_upditem;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.ComboBox cbo_searchby;
        private System.Windows.Forms.TextBox txt_search;
        private System.Windows.Forms.Button btn_search;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.TabPage tpg_opt_1;
        private System.Windows.Forms.TabControl tbcntrl_option;
        private System.Windows.Forms.TabPage tpg_opt_2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DateTimePicker dtp_time_out;
        private System.Windows.Forms.DateTimePicker dtp_time_in;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txt_name;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridViewTextBoxColumn code;
        private System.Windows.Forms.DataGridViewTextBoxColumn name;
        private System.Windows.Forms.DataGridViewTextBoxColumn time_in;
        private System.Windows.Forms.DataGridViewTextBoxColumn time_out;
    }
}