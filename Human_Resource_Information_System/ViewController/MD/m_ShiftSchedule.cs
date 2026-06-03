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
    public partial class m_ShiftSchedule: Form
    {

        Boolean seltbp = false;
        private Boolean isnew = false;
        private GlobalClass gc;
        private GlobalMethod gm;
        thisDatabase db;
        public m_ShiftSchedule()
        {
            InitializeComponent();
        }

        private void m_ShiftSchedule_Load(object sender, EventArgs e)
        {
            db = new thisDatabase();
            gc = new GlobalClass();
            gm = new GlobalMethod();

            dtp_time_in.CustomFormat = "hh:mm tt";
            dtp_time_in.ShowUpDown = true;

            dtp_time_out.CustomFormat = "hh:mm tt";
            dtp_time_out.ShowUpDown = true;

            cbo_searchby.Items.Insert(0,"CODE");

            disp_list();
        }


        private void btn_additem_Click(object sender, EventArgs e)
        {
            txt_code.Enabled = true;
            isnew = true;
            frm_clear();
            goto_win2();
        }

        private void tbcntrl_option_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if(seltbp == false)
            {
                e.Cancel = true;
            }
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
            if(seltbp == false)
            {
                e.Cancel = true;
            }
        }

        private void btn_back_Click(object sender, EventArgs e)
        {
            goto_win1();
        }

        private void frm_clear()
        {
            txt_code.Text = "";

            dtp_time_in.Value = DateTime.Now;
            dtp_time_out.Value = DateTime.Now;
           
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            Boolean success = false, ok = false;
            String notificationText = "";
            z_Notification notify = new z_Notification();

            String branch = GlobalClass.branch;
            String col = "", val = "";
            String notifyadd = null;
            String table = "hr_shift_schedule";

            String code = "", time_in = "00:00", time_out = "00:00", name = "";

            if (String.IsNullOrEmpty(txt_code.Text))
            {
                MessageBox.Show("Please enter the required fields.");
                return;
            }

            name = txt_name.Text;
            time_in = dtp_time_in.Value.ToString("HH:mm");
            time_out = dtp_time_out.Value.ToString("HH:mm");

            code = txt_code.Text;
           

            col = "code, time_in, time_out, name";
            val = "" + db.str_E(code) + ",'" + time_in + "','" + time_out + "'," + db.str_E(name) + "";

            if (isnew) 
            {

                db.DeleteOnTable(table, "code=" + db.str_E(code) + "");// use to replace new data in cancel PK
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
                col = "code=" + db.str_E(code) + ", time_in='" + time_in + "', time_out='" + time_out + "', name=" + db.str_E(name) + "";

                if (db.UpdateOnTable(table, col, "code=" + db.str_E(code) + ""))
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

        private void disp_list()
        {
            try { dgv_list.Rows.Clear(); }
            catch (Exception) { }

            //DataTable dt = db.QueryBySQLCode("SELECT * FROM rssys.hr_shift_schedule WHERE COALESCE(cancel,cancel,'')<>'Y'");
            DataTable dt = db.QueryBySQLCode("SELECT * FROM rssys.hr_shift_schedule WHERE COALESCE(cancel,cancel,'')<>'Y'");
            try
            {
                if (dt.Rows.Count > 0)
                {
                    for (int r = 0; r < dt.Rows.Count; r++)
                    {
                        int i = dgv_list.Rows.Add();
                        DataGridViewRow row = dgv_list.Rows[i];

                        row.Cells["code"].Value = dt.Rows[r]["code"].ToString();
                        row.Cells["name"].Value = dt.Rows[r]["name"].ToString();
                        row.Cells["time_in"].Value = gm.toDateString(dt.Rows[r]["time_in"].ToString(), "hh:mm tt");
                        row.Cells["time_out"].Value = gm.toDateString(dt.Rows[r]["time_out"].ToString(), "hh:mm tt");
                    }
                }
            }
            catch { }            
        }


        private void btn_delitem_Click(object sender, EventArgs e)
        {
            int r = -1;
            try
            {
                r = dgv_list.CurrentRow.Index;
                String code = dgv_list["code", r].Value.ToString();

                if (dgv_list.Rows.Count > 1 && !String.IsNullOrEmpty(code))
                {
                    if (MessageBox.Show("Are you sure you want to cancel this shift schedule?", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {

                        try
                        {
                            db.UpdateOnTable("hr_shift_schedule", "cancel='Y'", "code='" + code + "'");

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

        private void btn_upditem_Click(object sender, EventArgs e)
        {
            txt_code.Enabled = false;
            isnew = false;
            frm_clear();


            try
            {
                int r = dgv_list.CurrentRow.Index;
                String code = dgv_list["code", r].Value.ToString();

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
                    }
                    catch { }

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

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btn_print_Click(object sender, EventArgs e)
        {
            RPT_RES_entry frm = new RPT_RES_entry("MD04", "Shift Schedule List");
            frm.print_master_data();
            frm.ShowDialog();
        }

        private void tbcntrl_main_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void txt_code_TextChanged(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void txt_name_TextChanged(object sender, EventArgs e)
        {

        }

        private void dtp_time_out_ValueChanged(object sender, EventArgs e)
        {

        }

        private void dtp_time_in_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void tpg_list_Click(object sender, EventArgs e)
        {

        }

        private void dgv_list_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void tpg_info_Click(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void tbcntrl_option_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void groupBox7_Enter(object sender, EventArgs e)
        {

        }

        private void cbo_searchby_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txt_search_TextChanged(object sender, EventArgs e)
        {
            if(txt_search.Text == ""){
                disp_list();
                cbo_searchby.SelectedIndex = -1;
            }
        }

        private void btn_search_Click(object sender, EventArgs e)
        {
            String col = "";
            String search = txt_search.Text;
            if (cbo_searchby.SelectedIndex == 0)
            {

                col = "code LIKE '%" + search + "%'";
                disp_search_list(col);
            }
           
        }

        private void disp_search_list(String col)
        {
            try { dgv_list.Rows.Clear(); }
            catch (Exception) { }

            //DataTable dt = db.QueryBySQLCode("SELECT * FROM rssys.hr_shift_schedule WHERE COALESCE(cancel,cancel,'')<>'Y'");
            DataTable dt = db.QueryBySQLCode("SELECT * FROM rssys.hr_shift_schedule WHERE COALESCE(cancel,cancel,'')<>'Y' and "+col);
            try
            {
                if (dt.Rows.Count > 0)
                {
                    for (int r = 0; r < dt.Rows.Count; r++)
                    {
                        int i = dgv_list.Rows.Add();
                        DataGridViewRow row = dgv_list.Rows[i];

                        row.Cells["code"].Value = dt.Rows[r]["code"].ToString();
                        row.Cells["name"].Value = dt.Rows[r]["name"].ToString();
                        row.Cells["time_in"].Value = gm.toDateString(dt.Rows[r]["time_in"].ToString(), "hh:mm tt");
                        row.Cells["time_out"].Value = gm.toDateString(dt.Rows[r]["time_out"].ToString(), "hh:mm tt");
                    }
                }
            }
            catch { }
        }


        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tpg_opt_1_Click(object sender, EventArgs e)
        {

        }

        private void tpg_opt_2_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }


    }
}
