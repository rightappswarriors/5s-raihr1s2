using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Human_Resource_Information_System
{
    public partial class t_addLogs : Form
    {
        Boolean seltbp = false;
        private Boolean isnew = false;
        private GlobalClass gc;
        private GlobalMethod gm;
        thisDatabase db = new thisDatabase();
        t_BatchTimeLog bt;

        public t_addLogs()
        {
            InitializeComponent();
        }

        public t_addLogs(t_BatchTimeLog bt)
        {
            InitializeComponent();
            this.bt = bt;
        }

        private void t_addLogs_Load(object sender, EventArgs e)
        {
            dtp_timeLogs.CustomFormat = "hh:mm tt";
            dtp_timeLogs.ShowUpDown = true;           
        }       

        private void btn_save_Click(object sender, EventArgs e)
        {
            String work_date = "", time_logs, status = "", statusname="";
            String time_in = "", time_out = "";
            work_date = dtp_work_date.Value.ToShortDateString();
            time_logs = dtp_timeLogs.Value.ToShortTimeString();
            time_in = dtp_timein.Value.ToShortTimeString();
            time_out = dtp_timeout.Value.ToShortTimeString();

            if(cbo_status.SelectedIndex == 0)
            {
                status = "I";
                statusname = "TIME IN";
            }
            else if (cbo_status.SelectedIndex == 1)
            {
                status = "BO"; statusname = "BREAK OUT";
            }
            else if (cbo_status.SelectedIndex == 2)
            {
                status = "BI"; statusname = "BREAK IN";
            }
            else
            {
                status = "O"; statusname = "TIME OUT";
            }

            this.bt.set_logs(work_date, time_logs, status, statusname, true);
            this.Close();
        }

        public void set_time_log(String work_date,String time_in,String time_out) {
            btn_save.Text = "Save";
            dtp_work_date.Value = DateTime.Parse(work_date);
            //dtp_timeLogs.Value = Convert.ToDateTime(DateTime.Now.ToString("M/d/yyyy") + " " + time_log);
            dtp_timein.Value = Convert.ToDateTime(DateTime.Now.ToString("M/d/yyyy") + " " + time_in);
            dtp_timeout.Value = Convert.ToDateTime(DateTime.Now.ToString("M/d/yyyy") + " " + time_out);

        }

        public void set_log(String work_date, String time_log, String status)
        {
            btn_save.Text = "Save";
            dtp_work_date.Enabled = false;
            dtp_timeLogs.Enabled = false;
            dtp_work_date.Value = DateTime.Parse(work_date);
            dtp_timeLogs.Value = Convert.ToDateTime(DateTime.Now.ToString("M/d/yyyy") + " " + time_log);

            if (status == "IN")
            {
                cbo_status.SelectedIndex = 0;
            } 
            else if(status == "BO")
            {
                cbo_status.SelectedIndex = 1;
            }
            else if (status == "BI")
            {
                cbo_status.SelectedIndex = 2;
            }
            else
            {
                cbo_status.SelectedIndex = 3;
            }
           
        }

        private void btn_back_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dtp_timeLogs_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void dtp_work_date_ValueChanged(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
