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
    public partial class t_BatchTimeLog : Form
    {

        Boolean seltbp = false;
        private Boolean isnew = false;
        private GlobalClass gc;
        private GlobalMethod gm;
        thisDatabase db = new thisDatabase();
        t_BatchTimeLog bt;
        //public SDKHelper SDK = new SDKHelper();

        private String old_date = "", old_logs = "";


        Boolean update_log = false, isBtnGo = false;
        String isEmp = "";

        public t_BatchTimeLog()
        {
            InitializeComponent();
            dispfrombiometric();
        }


        private void dispfrombiometric(){
            System.Diagnostics.Debug.WriteLine("Running");
        }

        private void btn_itemadd_Click(object sender, EventArgs e)
        {
            try
            {
                if (cbo_employee.SelectedIndex == -1)
                {
                    MessageBox.Show("Please select an employee.");
                    cbo_employee.DroppedDown = true;
                    return;
                }
                if (isEmp != cbo_employee.SelectedValue.ToString() && isBtnGo)
                {
                    MessageBox.Show("Invalid selection of employee.");
                    return;
                }

                update_log = false;
                isnew = true;
                t_addLogs add = new t_addLogs(this);
                add.ShowDialog();
            }
            catch { MessageBox.Show("Invalid selection of employee. Please try again."); }           
        }

        private void t_BatchTimeLog_Load(object sender, EventArgs e)
        {
            gm = new GlobalMethod();
            gc = new GlobalClass();

            gc.load_employee(cbo_employee);
        }


        public void set_logs(String work_date,String time_logs,String status, String statusname, Boolean new_logs)
        {
            int i = 0;
            if (update_log == false)
            {
                i = dgv_list.Rows.Add();
            }
            else {
                i = dgv_list.CurrentRow.Index;
            }
            //bt.set_logs(work_date,time_in,"I",time_out,"O");
            DataGridViewRow row = dgv_list.Rows[i];

            row.Cells["work_date"].Value = gm.toDateString(work_date, "MMMM dd yyyy");
            row.Cells["time_log"].Value = gm.toDateString(time_logs, "hh:mm tt");
            row.Cells["is_new"].Value = new_logs;
            row.Cells["status"].Value = status;
            row.Cells["statusname"].Value = statusname;

            /*
            if (status == "I")
            {
                row.Cells["statusname"].Value = "IN";
            }
            else if (status == "BO")
            {
                row.Cells["statusname"].Value = "BREAK OUT";
            }
            else if (status == "BI")
            {
                row.Cells["statusname"].Value = "BREAK IN";
            }
            else if (status == "O")
            {
                row.Cells["statusname"].Value = "OUT";
            }*/
        }


        public void set_time_logs(String work_date, String time_in, String time_out, Boolean new_logs)
        {
            int i = 0;
            if (update_log == false)
            {
                i = dgv_list.Rows.Add();
            }
            else
            {
                i = dgv_list.CurrentRow.Index;
            }

            DataGridViewRow row = dgv_list.Rows[i];

            row.Cells["work_date"].Value = gm.toDateString(work_date,"");
            row.Cells["time_log"].Value = gm.toDateString(time_in, "HH:mm");
            row.Cells["status"].Value = gm.toDateString(time_out, "HH:mm");
            row.Cells["is_new"].Value = new_logs;

            /*row.Cells["work_date"].Value = gm.toDateString(work_date, "");
            row.Cells["time_log"].Value = gm.toDateString(time_logs, "HH:mm");
            row.Cells["is_new"].Value = new_logs;
            if (status == "I")
            {
                row.Cells["status"].Value = "IN";
            }
            else
            {
                row.Cells["status"].Value = "OUT";
            }*/

        }

        private void btn_mainsave_Click(object sender, EventArgs e)
        {
            String col = "", val = "";
            Boolean success = false;
            String table = "hr_tito2";
            String empid = "", work_date = "", time_log = "", status = "", source = "";

            String date_from = dtp_frm.Value.ToString("yyyy-MM-dd");
            String date_to = dtp_to.Value.ToString("yyyy-MM-dd");


            if (cbo_employee.SelectedIndex == -1)
            {
                MessageBox.Show("Please select an employee.");
                cbo_employee.DroppedDown = true;
                return;
            }

            if (dgv_list.Rows.Count <= 1)
            {
                MessageBox.Show("Empty batch time list.");
                return;
            }
            if (isEmp != cbo_employee.SelectedValue.ToString() && isBtnGo)
            {
                MessageBox.Show("Invalid selection of employee.");
                return;
            }

            empid = cbo_employee.SelectedValue.ToString();
            source = "M";

            if (isBtnGo == true)
            {
                db.UpdateOnTable(table, "cancel='Y'", "work_date BETWEEN '" + date_from + "' AND '" + date_to + "' AND empid ='" + empid + "'");
            }

            for (int r = 0; r < dgv_list.Rows.Count - 1; r++)
            {
                String new_logs = "";

                try { new_logs = dgv_list["is_new", r].Value.ToString(); }
                catch { }

                work_date = dgv_list["work_date", r].Value.ToString();
                DateTime dt = DateTime.Parse(dgv_list["time_log", r].Value.ToString());
                time_log = dt.ToString("HH:mm");
                status = dgv_list["status", r].Value.ToString();

                if (new_logs == "True")
                {
                    String logs_id = db.get_pk("logs_id");
                    col = "work_date,time_log,empid,status,source,logs_id";
                    val = "'" + work_date + "','" + time_log + "','" + empid + "','" + status + "','" + source + "','"+logs_id+"'";
                    db.InsertOnTable(table, col, val);
                    db.set_pkm99("logs_id", db.get_nextincrementlimitchar(logs_id, 8));
                    success = true;
                }
                else
                {
                    db.UpdateOnTable(table, "cancel=''", "empid='" + empid + "' AND work_date='" + work_date + "' AND time_log ='" + time_log + "'");
                }
            }

            if (isBtnGo == true)
            {
                db.DeleteOnTable(table, "cancel='Y'");
            }

            if (success == true)
            {
                MessageBox.Show("New time logs was saved for Employee ID : " + empid);

                old_logs = "";
                old_date = "";
                update_log = false;
                isBtnGo = true;
                disp_logs();
            }
            else
            {
                MessageBox.Show("Some time logs was removed for Employee ID : " + empid);
                disp_logs();
            }
            clear();
        }
        private void disp_logs()
        {
            int i = 0;
            String empid = "", date_from = "", date_to = "", sql = "", status_prev = "";

            try {    dgv_list.Rows.Clear();   } catch { }

            try
            {
                if (cbo_employee.SelectedIndex != -1)
                {
                    empid = cbo_employee.SelectedValue.ToString();
                    date_from = dtp_frm.Value.ToString("yyyy-MM-dd");
                    date_to = dtp_to.Value.ToString("yyyy-MM-dd");
                    sql = "SELECT logs_id, to_char(work_date, 'yyyy-MM-dd') AS work_date, time_log, status, CASE WHEN status='I' THEN 'IN' WHEN status='BO' THEN 'BREAK OUT' WHEN status='BI' THEN 'BREAK IN' ELSE 'OUT' END AS statusname, CASE WHEN source='M' THEN 'Manual' WHEN source='B' THEN 'Biometric' WHEN source='LB' THEN 'LogBox' ELSE '' END AS  source FROM rssys.hr_tito2 WHERE work_date BETWEEN '" + date_from + "' AND '" + date_to + "' AND empid ='" + empid + "' ORDER BY work_date ASC, time_log  ASC, status ASC, logs_id";

                    DataTable dt = db.QueryBySQLCode(sql);

                    if(dt != null)
                    {
                        for (int r = 0; r < dt.Rows.Count; r++)
                        {
                            i = dgv_list.Rows.Add();
                            DataGridViewRow row = dgv_list.Rows[i];

                            row.Cells["work_date"].Value = gm.toDateString(dt.Rows[r]["work_date"].ToString(), "MMMM dd yyyy");
                            row.Cells["days"].Value = gm.toDateValue(dt.Rows[r]["work_date"].ToString()).DayOfWeek;
                            row.Cells["time_log"].Value = gm.toDateString(dt.Rows[r]["time_log"].ToString(), "hh:mm tt");
                            row.Cells["status"].Value = dt.Rows[r]["status"].ToString();
                            row.Cells["statusname"].Value = dt.Rows[r]["statusname"].ToString();
                            row.Cells["source"].Value = dt.Rows[r]["source"].ToString(); ;
                            row.Cells["logs_id"].Value = dt.Rows[r]["logs_id"].ToString();

                            status_prev = dt.Rows[r]["status"].ToString();
                        }
                    }
                }
            }
            catch { MessageBox.Show("Please select an employee."); }
        }

       
        private void button1_Click(object sender, EventArgs e) // GO BUTTON
        {
            if (cbo_employee.SelectedIndex == -1)
            {
                MessageBox.Show("Please select an employee.");
                cbo_employee.DroppedDown = true;
                return;
            }

            isEmp = cbo_employee.SelectedIndex != -1 ? cbo_employee.SelectedValue.ToString() : "";
            isBtnGo = true;
            disp_logs();
        }

        private void btn_itemupd_Click(object sender, EventArgs e)
        {
            if (cbo_employee.SelectedIndex == -1)
            {
                MessageBox.Show("Please select an employee.");
                cbo_employee.DroppedDown = true;
                return;
            }
            if (isEmp != cbo_employee.SelectedValue.ToString() && isBtnGo)
            {
                MessageBox.Show("Invalid selection of employee.");
                return;
            }

            try
            {
                int r = dgv_list.CurrentRow.Index;
                String status = dgv_list["status", r].Value.ToString();

                if (dgv_list.Rows.Count > 1 && !String.IsNullOrEmpty(status))
                {
                    update_log = true;

                    try
                    {
                        String work_date = dgv_list["work_date", r].Value.ToString();
                        String time_log = dgv_list["time_log", r].Value.ToString();

                        old_date = work_date;
                        old_logs = time_log;

                        t_addLogs add = new t_addLogs(this);
                        add.set_log(work_date, time_log, status);
                        add.ShowDialog();
                       
                    }
                    catch { }

                }
                else
                {
                    MessageBox.Show("No batch time selected.");
                }
            }
            catch 
            {
                MessageBox.Show("No batch time selected.");
            }
        }

        private void cbo_employee_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void btn_itemremove_Click(object sender, EventArgs e)
        {
            if (cbo_employee.SelectedIndex == -1)
            {
                MessageBox.Show("Please select an employee.");
                cbo_employee.DroppedDown = true;
                return;
            }
            if (isEmp != cbo_employee.SelectedValue.ToString() && isBtnGo)
            {
                MessageBox.Show("Invalid selection of employee.");
                return;
            }

            try
            {
                int r = dgv_list.CurrentRow.Index;
                String code = dgv_list["status", r].Value.ToString();

                if (dgv_list.Rows.Count > 1 && !String.IsNullOrEmpty(code))
                {
                    DialogResult result = MessageBox.Show("Are you sure you want to cancel this time log?", "", MessageBoxButtons.YesNoCancel);
                    if (result == DialogResult.Yes)
                    {
                        dgv_list.Rows.RemoveAt(r);
                    }
                }
                else
                {
                    MessageBox.Show("No batch time selected.");
                }
            }
            catch
            {
                MessageBox.Show("No batch time selected.");
            }
        }

        private void btn_mainexit_Click(object sender, EventArgs e)
        {
            clear();
        }

        private void dgv_list_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            
        }

        private void btn_duplicates_Click(object sender, EventArgs e)
        {
            Boolean success = false;
            String date_from = dtp_frm.Value.ToString("yyyy-MM-dd");
            String date_to = dtp_to.Value.ToString("yyyy-MM-dd");

            if (cbo_employee.SelectedIndex == -1)
            {
                MessageBox.Show("Select an employee");
                cbo_employee.DroppedDown = true;
                return;
            }

            String empid = cbo_employee.SelectedValue.ToString();
            DialogResult result = MessageBox.Show("Are you sure you want to delete all DUPLICATE logs for all employees? For your information, Duplicate Logs are same value in the following: date, time, status and employee. The first logs are left.", "Confirmation", MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                String query = "DELETE  FROM rssys.hr_tito2   WHERE logs_id IN (    SELECT logs_id FROM rssys.hr_tito2 EXCEPT SELECT MIN(logs_id) FROM rssys.hr_tito2    GROUP BY work_date, time_log, status, empid  );";
                db.QueryBySQLCode(query);
                success = true;
                if (success == true)
                {
                    dgv_list.Rows.Clear();
                    MessageBox.Show("Duplicate Time logs succesfully deleted.");
                }
            }
        }

        void clear()
        {
            cbo_employee.SelectedIndex = -1;
            dgv_list.Rows.Clear();
        }

        private void btn_removeAllLogs_Click(object sender, EventArgs e)
        {
            Boolean success = false;
            String date_from = dtp_frm.Value.ToString("yyyy-MM-dd");
            String date_to = dtp_to.Value.ToString("yyyy-MM-dd");

            if (cbo_employee.SelectedIndex == -1)
            {
                MessageBox.Show("Select an employee");
                cbo_employee.DroppedDown = true;
                return;
            }

            String empid = cbo_employee.SelectedValue.ToString();
            DialogResult result = MessageBox.Show("Are you sure you want to delete all logs within these dates between '" + date_from + "' AND '" + date_to + "'?", "Confirmation", MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                String query = "DELETE FROM rssys.hr_tito2 WHERE empid ='" + empid + "' AND work_date BETWEEN '" + date_from + "' AND '" + date_to + "'";
                db.QueryBySQLCode(query);
                success = true;
                if (success == true)
                {
                    dgv_list.Rows.Clear();
                    MessageBox.Show("Time logs succesfully deleted.");
                }
            }
        }
    }
}
