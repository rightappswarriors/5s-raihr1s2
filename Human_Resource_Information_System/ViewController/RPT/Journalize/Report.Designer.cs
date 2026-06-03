namespace Human_Resource_Information_System
{
    partial class Report
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
            this.pnl_pbar = new System.Windows.Forms.Panel();
            this.lbl_status = new System.Windows.Forms.Label();
            this.pbar = new System.Windows.Forms.ProgressBar();
            this.crptviewer = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.bgWorker = new System.ComponentModel.BackgroundWorker();
            this.txt_notification = new System.Windows.Forms.TextBox();
            this.rxt_msg = new System.Windows.Forms.RichTextBox();
            this.pnl_pbar.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnl_pbar
            // 
            this.pnl_pbar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.pnl_pbar.Controls.Add(this.lbl_status);
            this.pnl_pbar.Controls.Add(this.pbar);
            this.pnl_pbar.Cursor = System.Windows.Forms.Cursors.WaitCursor;
            this.pnl_pbar.Location = new System.Drawing.Point(103, 101);
            this.pnl_pbar.Name = "pnl_pbar";
            this.pnl_pbar.Size = new System.Drawing.Size(210, 26);
            this.pnl_pbar.TabIndex = 7;
            this.pnl_pbar.UseWaitCursor = true;
            // 
            // lbl_status
            // 
            this.lbl_status.AutoSize = true;
            this.lbl_status.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_status.Location = new System.Drawing.Point(52, 7);
            this.lbl_status.Name = "lbl_status";
            this.lbl_status.Size = new System.Drawing.Size(114, 16);
            this.lbl_status.TabIndex = 1;
            this.lbl_status.Text = "PROCESSING . . .";
            this.lbl_status.UseWaitCursor = true;
            // 
            // pbar
            // 
            this.pbar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pbar.Location = new System.Drawing.Point(0, 3);
            this.pbar.Name = "pbar";
            this.pbar.Size = new System.Drawing.Size(210, 23);
            this.pbar.TabIndex = 0;
            this.pbar.UseWaitCursor = true;
            // 
            // crptviewer
            // 
            this.crptviewer.ActiveViewIndex = -1;
            this.crptviewer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.crptviewer.Cursor = System.Windows.Forms.Cursors.Default;
            this.crptviewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.crptviewer.Location = new System.Drawing.Point(0, 38);
            this.crptviewer.Name = "crptviewer";
            this.crptviewer.SelectionFormula = "";
            this.crptviewer.ShowLogo = false;
            this.crptviewer.Size = new System.Drawing.Size(441, 275);
            this.crptviewer.TabIndex = 8;
            this.crptviewer.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None;
            this.crptviewer.ViewTimeSelectionFormula = "";
            // 
            // bgWorker
            // 
            this.bgWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgWorker_DoWork);
            // 
            // txt_notification
            // 
            this.txt_notification.BackColor = System.Drawing.Color.RoyalBlue;
            this.txt_notification.Dock = System.Windows.Forms.DockStyle.Top;
            this.txt_notification.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_notification.ForeColor = System.Drawing.SystemColors.Info;
            this.txt_notification.Location = new System.Drawing.Point(0, 17);
            this.txt_notification.Name = "txt_notification";
            this.txt_notification.Size = new System.Drawing.Size(441, 21);
            this.txt_notification.TabIndex = 10;
            this.txt_notification.Text = "Notifcation Message";
            this.txt_notification.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txt_notification.Visible = false;
            // 
            // rxt_msg
            // 
            this.rxt_msg.Dock = System.Windows.Forms.DockStyle.Top;
            this.rxt_msg.Location = new System.Drawing.Point(0, 0);
            this.rxt_msg.Name = "rxt_msg";
            this.rxt_msg.Size = new System.Drawing.Size(441, 17);
            this.rxt_msg.TabIndex = 9;
            this.rxt_msg.Text = "";
            // 
            // Report
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(441, 313);
            this.Controls.Add(this.pnl_pbar);
            this.Controls.Add(this.crptviewer);
            this.Controls.Add(this.txt_notification);
            this.Controls.Add(this.rxt_msg);
            this.Name = "Report";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Report";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Report_Load);
            this.pnl_pbar.ResumeLayout(false);
            this.pnl_pbar.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnl_pbar;
        private System.Windows.Forms.Label lbl_status;
        private System.Windows.Forms.ProgressBar pbar;
        private CrystalDecisions.Windows.Forms.CrystalReportViewer crptviewer;
        private System.ComponentModel.BackgroundWorker bgWorker;
        private System.Windows.Forms.TextBox txt_notification;
        private System.Windows.Forms.RichTextBox rxt_msg;
    }
}