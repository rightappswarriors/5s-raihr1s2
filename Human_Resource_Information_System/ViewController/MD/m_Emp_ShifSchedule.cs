using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Human_Resource_Information_System.ViewController.MD
{
    public partial class m_Emp_ShifSchedule : Form
    {
        Boolean seltbp = false;
        private Boolean isnew = false;
        private GlobalClass gc;
        private GlobalMethod gm;
        thisDatabase db;

        public m_Emp_ShifSchedule()
        {
            InitializeComponent();
        }


        private void m_Emp_ShifSchedule_Load(object sender, EventArgs e)
        {
            db = new thisDatabase();
            gc = new GlobalClass();
            gm = new GlobalMethod();
            gc.load_shift_employee(cbo_employee);
            gc.load_shiftname(cbo_shift);
            cbo_searchby.Items.Insert(0, "SHIFTCODE");
            cbo_searchby.Items.Insert(1,"EMP ID");
            cbo_searchby.Items.Insert(2,"EMPLOYEE");
            disp_list();
        }


        private void frm_clear()
        {
            txt_code.Text = "";

            dtp_time_in.Value = DateTime.Now;
            dtp_time_out.Value = DateTime.Now;
            cbo_employee.SelectedIndex = -1;
            cbo_shift.SelectedIndex = -1;
            dtp_date_from.Value = DateTime.Now;
            dtp_date_to.Value = DateTime.Now;
        }

        private void goto_win2()
        {
            seltbp = true;
            tbcntrl_option.SelectedTab = tpg_opt_2;
            tpg_opt_2.Show();

            tbcntrl_main.SelectedTab = tpg_info;
            tpg_info.Show();
            seltbp = false;
         
        }


        private void goto_win1()
        {
            seltbp = true;
            tbcntrl_option.SelectedTab = tpg_opt_1;
            tpg_opt_1.Show();

            tbcntrl_main.SelectedTab = tpg_list;
            tpg_list.Show();
            seltbp = false;
        }


        private void tbcntrl_main_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (seltbp == false)
            {
                e.Cancel = true;
            }
        }


        
        public void disp_list()
        {
            try { dgv_list.Rows.Clear(); }
            catch (Exception) { }
            DataTable dt = db.QueryBySQLCode("SELECT rssys.hr_emp_shift.esid,rssys.hr_shift_schedule.code,rssys.hr_employee.empid,concat(rssys.hr_employee.firstname,' ',rssys.hr_employee.lastname) as name, rssys.hr_shift_schedule.time_in, rssys.hr_shift_schedule.time_out,to_char(rssys.hr_emp_shift.date_from, 'yyyy-MM-dd') AS date_from,to_char(rssys.hr_emp_shift.date_to, 'yyyy-MM-dd') AS date_to FROM ((rssys.hr_emp_shift INNER JOIN rssys.hr_shift_schedule ON rssys.hr_emp_shift.shiftcode = rssys.hr_shift_schedule.code) INNER JOIN rssys.hr_employee ON rssys.hr_emp_shift.empid = rssys.hr_employee.empid) WHERE COALESCE(rssys.hr_emp_shift.cancel,rssys.hr_emp_shift.cancel,'')<>'Y'");
            try
            {
                if (dt.Rows.Count > 0)
                {
                    for (int r = 0; r < dt.Rows.Count; r++)
                    {
                        int i = dgv_list.Rows.Add();
                        DataGridViewRow row = dgv_list.Rows[i];
                       
                        row.Cells["esid"].Value = dt.Rows[r]["esid"].ToString();
                        row.Cells["shiftcode"].Value = dt.Rows[r]["code"].ToString();
                        row.Cells["emp_id"].Value = dt.Rows[r]["empid"].ToString();
                        row.Cells["employee"].Value = dt.Rows[r]["name"].ToString();
                        row.Cells["time_in"].Value = gm.toDateString(dt.Rows[r]["time_in"].ToString(), "hh:mm tt");
                        row.Cells["time_out"].Value = gm.toDateString(dt.Rows[r]["time_out"].ToString(), "hh:mm tt");
                        row.Cells["date_from"].Value = dt.Rows[r]["date_from"].ToString();
                        row.Cells["date_to"].Value = dt.Rows[r]["date_to"].ToString();
                    }
                }
            }
            catch { }          
            
        }

        private void btn_additem_Click(object sender, EventArgs e)
        {
            isnew = true;
            frm_clear();
            goto_win2();
        }

        private void btn_upditem_Click(object sender, EventArgs e)
        {
            isnew = false;
            frm_clear();

            try
            {
                int r = dgv_list.CurrentRow.Index;
                String code = dgv_list["esid", r].Value.ToString();
                String emp_id = dgv_list["emp_id", r].Value.ToString();
                String shiftcode = dgv_list["shiftcode", r].Value.ToString();

                if (dgv_list.Rows.Count > 1 && !String.IsNullOrEmpty(code))
                {
                    try
                    {
                        txt_code.Text = code;

                        if (!String.IsNullOrEmpty(dgv_list["time_in", r].Value.ToString()) && !String.IsNullOrEmpty(dgv_list["time_out", r].Value.ToString()))
                        {
                            dtp_time_in.Value = DateTime.Parse(dgv_list["time_in", r].Value.ToString());
                            dtp_time_out.Value = DateTime.Parse(dgv_list["time_out", r].Value.ToString());
                        }

                        if (!String.IsNullOrEmpty(dgv_list["date_from", r].Value.ToString()) && !String.IsNullOrEmpty(dgv_list["date_to", r].Value.ToString()))
                        {
                            dtp_date_from.Value = DateTime.Parse(dgv_list["date_from", r].Value.ToString());
                            dtp_date_to.Value = DateTime.Parse(dgv_list["date_to", r].Value.ToString());
                        }
                    }
                    catch { }

                    try { cbo_employee.SelectedValue = emp_id;  }
                    catch { }
                    try { cbo_shift.SelectedValue = shiftcode; } catch {} 

                    goto_win2();
                }
                else
                {
                    MessageBox.Show("No shift schedule selected.");
                }
            }
            catch
            {
                MessageBox.Show("No shift schedule selected.");
            }
        }

        private void btn_delitem_Click(object sender, EventArgs e)
        {
            int r = -1;
            try
            {
                r = dgv_list.CurrentRow.Index;
                String code = dgv_list["esid", r].Value.ToString();

                if (dgv_list.Rows.Count > 1 && !String.IsNullOrEmpty(code))
                {
                    if (MessageBox.Show("Are you sure you want to cancel this shift schedule?", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {

                        try
                        {
                            db.UpdateOnTable("hr_emp_shift", "cancel='Y'", "esid='" + code + "'");

                            disp_list();
                            MessageBox.Show("Cancelled successfully");
                        }
                        catch
                        {
                            MessageBox.Show("Invalid to cancel.");
                        }
                    }

                }
                else
                {
                    MessageBox.Show("No shift schedule selected.");
                }
            }
            catch
            {
                MessageBox.Show("No shift schedule selected.");
            }
        }

        private void btn_print_Click(object sender, EventArgs e)
        {

        }

        private void btn_search_Click(object sender, EventArgs e)
        {

            String col = "";
            String search = txt_search.Text;
            if (cbo_searchby.SelectedIndex == 0)
            {
                col = "rssys.hr_emp_shift.shiftcode LIKE '%" + search + "%'";
                disp_search_list(col);
            }else if(cbo_searchby.SelectedIndex == 1){

                col = "rssys.hr_emp_shift.empid LIKE '%" + search + "%'";
                disp_search_list(col);
            }else if(cbo_searchby.SelectedIndex == 2){
                col = "rssys.hr_employee.lastname LIKE '%" + search + "%' OR rssys.hr_employee.firstname LIKE '%"+search+"%'";
                disp_search_list(col);
            }
        }

        public void disp_search_list(String col)
        {
            try { dgv_list.Rows.Clear(); }
            catch (Exception) { }
            DataTable dt = db.QueryBySQLCode("SELECT rssys.hr_emp_shift.esid,rssys.hr_shift_schedule.code,rssys.hr_employee.empid,concat(rssys.hr_employee.firstname,' ',rssys.hr_employee.lastname) as name, rssys.hr_shift_schedule.time_in, rssys.hr_shift_schedule.time_out,to_char(rssys.hr_emp_shift.date_from, 'yyyy-MM-dd') AS date_from,to_char(rssys.hr_emp_shift.date_to, 'yyyy-MM-dd') AS date_to FROM ((rssys.hr_emp_shift INNER JOIN rssys.hr_shift_schedule ON rssys.hr_emp_shift.shiftcode = rssys.hr_shift_schedule.code) INNER JOIN rssys.hr_employee ON rssys.hr_emp_shift.empid = rssys.hr_employee.empid) WHERE COALESCE(rssys.hr_emp_shift.cancel,rssys.hr_emp_shift.cancel,'')<>'Y' and " + col);
            try
            {
                if (dt.Rows.Count > 0)
                {
                    for (int r = 0; r < dt.Rows.Count; r++)
                    {
                        int i = dgv_list.Rows.Add();
                        DataGridViewRow row = dgv_list.Rows[i];

                        row.Cells["esid"].Value = dt.Rows[r]["esid"].ToString();
                        row.Cells["shiftcode"].Value = dt.Rows[r]["code"].ToString();
                        row.Cells["emp_id"].Value = dt.Rows[r]["empid"].ToString();
                        row.Cells["employee"].Value = dt.Rows[r]["name"].ToString();
                        row.Cells["time_in"].Value = gm.toDateString(dt.Rows[r]["time_in"].ToString(), "hh:mm tt");
                        row.Cells["time_out"].Value = gm.toDateString(dt.Rows[r]["time_out"].ToString(), "hh:mm tt");
                        row.Cells["date_from"].Value = dt.Rows[r]["date_from"].ToString();
                        row.Cells["date_to"].Value = dt.Rows[r]["date_to"].ToString();
                    }
                }
            }
            catch { }
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            Boolean success = false, ok = false;
            String notificationText = "";
            z_Notification notify = new z_Notification();

            String branch = GlobalClass.branch;
            String col = "", val = "";
            String notifyadd = null;
            String table = "hr_emp_shift";
            String esid = "";
            //String code = "", time_in = "00:00", time_out = "00:00", name = "";
            String shiftcode = "", empid = "", date_from = "", date_to = "";

            if (cbo_employee.SelectedIndex == -1)
            {
                MessageBox.Show("Please select an employee.");
                cbo_employee.DroppedDown = true;
                return;
            }

            if (cbo_shift.SelectedIndex == -1)
            {
                MessageBox.Show("Please select an Shift.");
                cbo_shift.DroppedDown = true;
                return;
            }
            /*if (String.IsNullOrEmpty(txt_code.Text))
            {
                MessageBox.Show("Please enter the required fields.");
                return;
            }*/
            
            empid = cbo_employee.SelectedValue.ToString();
            shiftcode = cbo_shift.SelectedValue.ToString();
            date_from = dtp_date_from.Value.ToString("yyyy-MM-dd");
            date_to = dtp_date_to.Value.ToString("yyyy-MM-dd");

           

            col = "shiftcode,empid,date_from,date_to";
            val = "" + db.str_E(shiftcode) + ",'" + empid + "','" + date_from + "','" + date_to + "'";

            if (isnew)
            {


                //db.DeleteOnTable(table, "code=" + db.str_E(code) + "");// use to replace new data in cancel PK
                if (db.InsertOnTable(table, col, val))
                {
                    success = true;
                }
                else
                {
                    success = false;
                    //db.DeleteOnTable(table, "deptid='" + code + "'");
                    MessageBox.Show("Failed on saving.");
                }
            }
            else
            {
                esid = txt_code.Text;
                //update item 
                col = "shiftcode=" + db.str_E(shiftcode) + ", empid='" + empid + "', date_from='" + date_from + "', date_to='" + date_to + "'";
                if (db.UpdateOnTable(table, col, "esid=" + db.str_E(esid) + ""))
                {
                    success = true;
                }
                else
                {
                    success = false;
                    MessageBox.Show("Failed on saving.");
                }
            }

            if (success)
            {
                disp_list();
                goto_win1();
                frm_clear();
            }
        }

        private void btn_back_Click(object sender, EventArgs e)
        {
            goto_win1();
        }

        private void cbo_shift_SelectedIndexChanged(object sender, EventArgs e)
        {
            String shiftcode = null;

            try 
            {
                if(cbo_shift.SelectedIndex > -1)
                {
                    shiftcode = cbo_shift.SelectedValue.ToString();
                    DataTable shift = db.QueryBySQLCode("SELECT code, time_in, time_out, name, cancel FROM rssys.hr_shift_schedule where code = '" + shiftcode + "'");

                    if (shift.Rows.Count > 0)
                    {
                        dtp_time_in.Value = Convert.ToDateTime(shift.Rows[0]["time_in"].ToString());
                        dtp_time_out.Value = Convert.ToDateTime(shift.Rows[0]["time_out"].ToString());
                    }
                }                
            }
            catch { }
            
        }
    }
}
