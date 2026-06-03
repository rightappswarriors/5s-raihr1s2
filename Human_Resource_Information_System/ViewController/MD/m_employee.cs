using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;
using CrystalDecisions.ReportAppServer.DataDefModel;
using System.Data.Common;
using System.IO;
using Excel = Microsoft.Office.Interop.Excel;
using System.DirectoryServices.ActiveDirectory;
using System.Runtime.InteropServices;
using CrystalDecisions.ReportAppServer.CubeDefModel;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrackBar;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TreeView;

namespace Human_Resource_Information_System
{
    public partial class m_employee : Form
    {
        Boolean seltbp = false;
        private Boolean isnew = false;
        private GlobalClass gc;
        private GlobalMethod gm;
        thisDatabase db = new thisDatabase();
        public m_employee()
        {
            InitializeComponent();
        }


        private void m_employee_Load(object sender, EventArgs e)
        {
            db = new thisDatabase();
            gc = new GlobalClass();
            gm = new GlobalMethod();

            if (GlobalClass.username == "ADMIN")
            {
                rtxt_msg.Visible = true;
            }
            else
            {
                rtxt_msg.Visible = false;
            }

            date_shift_sched_from.CustomFormat = "hh:mm tt";
            date_shift_sched_from.ShowUpDown = true;
            date_shift_sched_to.CustomFormat = "hh:mm tt";
            date_shift_sched_to.ShowUpDown = true;

            date_sift_sched_sat_from.CustomFormat = "hh:mm tt";
            date_sift_sched_sat_from.ShowUpDown = true;

            date_sift_sched_sat_to.CustomFormat = "hh:mm tt";
            date_sift_sched_sat_to.ShowUpDown = true;

            dtp_resigned.Visible = false;
            dtp_terminated.Visible = false;
            dtp_contractual.Visible = false;
            dtp_probitioned.Visible = false;
            dtp_regularized.Visible = false;
            dtp_retired.Visible = false;

            gc.load_emp_stat(cbo_status);
            gc.load_rate_type(cbo_rate_type);
            gc.load_wtax(cbo_tax_bracket);
            gc.load_customer(cbo_customer, "type='Employee'");

            gc.load_days(cbo_dayoff1);
            gc.load_days(cbo_dayoff2);
            //cbo_cledger
            gc.load_account_for_cust_ledger(cbo_cledger);

            gc.load_dept(cbo_department);
            gc.load_position(cbo_position);
            gc.load_civil_status(cbo_civil_stat);
            gc.load_cbo_sss(cbo_sss);
            gc.load_branch(cbo_branch);
            gc.load_payroll_type(cbo_payouttype);


            cbo_searchby.Items.Insert(0, "CODE");
            cbo_searchby.Items.Insert(1, "EMPLOYEE");
            try { cbo_searchby.SelectedIndex = 1; } catch { }
            disp_list();
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
            tab_details.SelectedTab = tpg_emp_info;
            tpg_emp_info.Show();
        }

        private void tbcntrl_option_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (seltbp == false)
                e.Cancel = true;
        }

        private void tbcntrl_main_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (seltbp == false)
                e.Cancel = true;
        }

        private void btn_back_Click(object sender, EventArgs e)
        {
            goto_win1();
            disp_list();
        }

        private void btn_save_Click(object sender, EventArgs e)
        {

            Boolean success = false, ok = false;
            String notificationText = "";
            z_Notification notify = new z_Notification();

            String branch = GlobalClass.branch;
            String col = "", val = "", add_col = "", add_val = "";
            String notifyadd = null;
            String table = "hr_employee";
            String code = "", lastname = "", firstname = "", mi = "", section = "", position = "", picture = "", department = "", date_hired = "1900-01-01", contractual_date = "1900-01-01", prohibition_date = "1900-01-01", date_regular = "1900-01-01", date_resigned = "1900-01-01", date_terminated = "1900-01-01", date_retired = "1900-01-01", empstatus = "", contract_days = "0", prc = "", ctc = "", rate_type = "", pay_rate = "", biometric = "", sss = "", pagibig = "", philhealth = "", payroll_account = "", tin = "", tax_bracket = "", shift_sched_from = "", dayoff1 = "", dayoff2 = "", sex = "", birth = "", civil_status = "", religion = "", height = "0.00", weight = "0.00", father = "", father_address = "", father_contact = "", father_job = "", mother = "", mother_address = "", mother_contact = "", mother_job = "", emp_contact = "", home_tel = "", email = "", home_address = "", emergency_name = "", emergency_contact = "", em_home_address = "", relationship = "", shift_sched_sat_from = "", shift_sched_to = "", shift_sched_sat_to = "", fixed_rate = "", primary = "", secondary = "", tertiary = "", graduate = "", post_graduate = "", sss_table = "",fix_sched="",sl_code = "",at_code = "", potcode="";

            if (String.IsNullOrEmpty(txt_lastname.Text))
            {
                MessageBox.Show("Please enter the required fields.");
                return;
            }
            if (String.IsNullOrEmpty(txt_firstname.Text))
            {
                MessageBox.Show("Please enter the required fields.");
                return;
            }

            /*if (String.IsNullOrEmpty(txt_mi.Text))
            {
                MessageBox.Show("Please enter the required fields.");
                return;
            }*/

            if (cbo_branch.SelectedIndex == -1)
            {
                MessageBox.Show("Please select the assigned branch.");
                cbo_branch.DroppedDown = true;
                return;
            }

            if (cbo_department.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a department.");
                cbo_department.DroppedDown = true;
                return;
            }

            /*if (cbo_section.Items.Count > 0)
            {
                if (cbo_section.SelectedIndex == -1)
                {
                    MessageBox.Show("Please select department section.");
                    cbo_section.DroppedDown = true;
                    return;
                }
            }*/


            if (cbo_position.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a position.");
                cbo_position.DroppedDown = true;
                return;
            }

            if (cbo_status.SelectedIndex == -1)
            {
                MessageBox.Show("Please select employee status.");
                cbo_status.DroppedDown = true;
                return;
            }
            if (dtp_hired.Value.ToShortDateString() == null)
            {
                MessageBox.Show("Please enter the required fields.");
                return;
            }
            if (cbo_rate_type.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a rate type.");
                cbo_rate_type.DroppedDown = true;
                return;
            }
            /*if (cbo_tax_bracket.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a tax bracket.");
                cbo_tax_bracket.DroppedDown = true;
                return;
            }*/
            if (cbo_gender.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a gender.");
                cbo_gender.DroppedDown = true;
                return;
            }


            lastname = txt_lastname.Text;
            firstname = txt_firstname.Text;
            mi = txt_mi.Text;

            if (cbo_branch.SelectedIndex != -1)
            {
                branch = cbo_branch.SelectedValue.ToString();
            }

            if (cbo_department.SelectedIndex != -1)
            {
                department = cbo_department.SelectedValue.ToString();
            }

            if (cbo_section.SelectedIndex != -1)
            {
                section = cbo_section.SelectedValue.ToString();
            }
            if (cbo_position.SelectedIndex != -1)
            {
                position = cbo_position.SelectedValue.ToString();
            }

            date_hired = dtp_hired.Value.ToString("yyyy-MM-dd");

            if (cbo_status.SelectedIndex != -1)
            {
                empstatus = cbo_status.SelectedValue.ToString();
            }

            if (txt_contract_days.Text != "")
            {
                contract_days = txt_contract_days.Text;
            }
            prc = txt_prc_number.Text;
            ctc = txt_ctc_num.Text;

            if (cbo_rate_type.SelectedIndex != -1)
            {
                rate_type = cbo_rate_type.SelectedValue.ToString();
            }

            try
            {
                pay_rate = Convert.ToDouble(txt_pay_rate.Text).ToString();
            }

            catch (Exception ex) { MessageBox.Show("Pay rate must be numeric."); return; }

            if (cbo_payouttype.SelectedIndex != -1)
            {
                potcode = cbo_payouttype.SelectedValue.ToString();
            }


            biometric = txt_biometric.Text;
            sss = txt_sss_number.Text;
            if(cbo_sss.SelectedIndex != -1){
                sss_table = cbo_sss.SelectedValue.ToString();
            }
             
            pagibig = txt_pagibig.Text;
            philhealth = txt_philhealth.Text;
            payroll_account = txt_payroll_act.Text;
            tin = txt_tin.Text;
            if (cbo_tax_bracket.SelectedIndex != -1)
            {
                tax_bracket = cbo_tax_bracket.SelectedValue.ToString();
            }

            shift_sched_from = date_shift_sched_from.Value.ToString("HH:mm");
            shift_sched_to = date_shift_sched_to.Value.ToString("HH:mm");
            shift_sched_sat_from = date_sift_sched_sat_from.Value.ToString("HH:mm");
            shift_sched_sat_to = date_sift_sched_sat_to.Value.ToString("HH:mm");

           


            if (cbo_gender.SelectedIndex != -1)
            {
                sex = cbo_gender.SelectedItem.ToString();
            }

            birth = date_birth.Value.ToShortDateString();
            if (cbo_civil_stat.SelectedIndex != -1)
            {
                civil_status = cbo_civil_stat.SelectedValue.ToString();
            }

            religion = txt_religion.Text;
            if(txt_height.Text != ""){
                try
                {
                    height = Convert.ToDouble(txt_height.Text).ToString();
                }
                catch (Exception ex) { MessageBox.Show("Height must be numeric"); return; }
            }
            
            if(txt_weight.Text != ""){
                try { 
                    weight = Convert.ToDouble(txt_weight.Text).ToString(); 
                }
                catch (Exception ex) { MessageBox.Show("Weight must be numeric"); return; }

            }

            

            if (chk_fixed_rate.Checked == true)
            {
                fixed_rate = "1";
            }

            if (rb_yes_fix.Checked == true)
            {
                fix_sched = "Y";
            }
            else if (rb_excempted_fix.Checked == true)
            {
                fix_sched = "E";
            }
            else
            {
                fix_sched = "N";
            }

            father = txt_father.Text;
            father_address = txt_father_address.Text;
            father_contact = txt_father_contact.Text;
            father_job = txt_father_occupation.Text;
            mother = txt_mother.Text;
            mother_address = txt_mother_address.Text;
            mother_contact = txt_mother_contact.Text;
            mother_job = txt_mother_occupation.Text;
            emp_contact = txt_contact_no.Text;
            home_tel = txt_home_tel.Text;
            email = txt_email.Text;
            home_address = txt_home_address.Text;
            emergency_name = txt_ctc_name.Text;
            emergency_contact = txt_ctc_no.Text;
            em_home_address = txt_home_add.Text;
            relationship = txt_relation.Text;
            primary = txt_primary.Text;
            secondary = txt_secondary.Text;
            tertiary = txt_tertiary.Text;
            graduate = txt_graduate.Text;
            post_graduate = txt_post_graduate.Text;

            if (cbo_dayoff1.SelectedIndex != -1)
            {
                dayoff1 = cbo_dayoff1.SelectedValue.ToString();

            }
            else
            {
                dayoff1 = "";
            }
            
            if (cbo_dayoff2.SelectedIndex != -1)
            {
                dayoff2 = cbo_dayoff2.SelectedValue.ToString();
            }
            else
            {
                dayoff2 = "";
             
            }

            if (chk_resigned.Checked == true)
            {
                date_resigned = dtp_resigned.Value.ToString("yyyy-MM-dd");
            }
            if (chk_terminated.Checked == true)
            {
                date_terminated = dtp_terminated.Value.ToString("yyyy-MM-dd");
            }
            if (chk_retired.Checked == true)
            {
                date_retired = dtp_retired.Value.ToString("yyyy-MM-dd");
            }
            if (chk_contractual.Checked == true)
            {
                contractual_date = dtp_contractual.Value.ToString("yyyy-MM-dd");
            }
            if (chk_probition.Checked == true)
            {
                prohibition_date = dtp_probitioned.Value.ToString("yyyy-MM-dd");
            }
            if (chk_regular.Checked == true)
            {
                date_regular = dtp_regularized.Value.ToString("yyyy-MM-dd");
            }
            sl_code = "";
            try { sl_code = cbo_customer.SelectedValue.ToString(); } catch { }
            at_code = "";
            try { at_code = cbo_cledger.SelectedValue.ToString(); }
            catch { }
            
            if (isnew)
            {
                code = code = db.get_pk("empid"); //changes from 'hr_empid'
                col = "empid,lastname,firstname,mi,positions,department,section,date_hired,contractual_date,date_resigned,date_terminated,date_retired,prohibition_date,date_regular,empstatus,contract_days,prc,ctc,rate_type,pay_rate,biometric,sss,pagibig,philhealth,payroll_account,tin,tax_bracket,shift_sched_from,dayoff1,dayoff2,sex,birth,civil_status,religion,height,weight,father,father_address,father_contact,father_job,mother,mother_address,mother_contact,mother_job,emp_contact,home_tel,email,home_address,emergency_name,emergency_contact,em_home_address,relationship,shift_sched_sat_from,shift_sched_to,shift_sched_sat_to,fixed_rate,primary_ed,secondary_ed,tertiary_ed,graduate,post_graduate, sss_bracket,fixed_sched,sl_code,at_code, branch, potcode";

                val = "" + db.str_E(code) + "," + db.str_E(lastname) + "," + db.str_E(firstname) + "," + db.str_E(mi) + "," + db.str_E(position) + "," + db.str_E(department) + "," + db.str_E(section) + ",'" + date_hired + "','" + contractual_date + "','" + date_resigned + "','" + date_terminated + "','" + date_retired + "','" + prohibition_date + "','" + date_regular + "'," + db.str_E(empstatus) + "," + db.str_E(contract_days) + "," + db.str_E(prc) + "," + db.str_E(ctc) + "," + db.str_E(rate_type) + "," + db.str_E(pay_rate) + "," + db.str_E(biometric) + "," + db.str_E(sss) + "," + db.str_E(pagibig) + ", " + db.str_E(philhealth) + "," + db.str_E(payroll_account) + "," + db.str_E(tin) + "," + db.str_E(tax_bracket) + "," + db.str_E(shift_sched_from) + "," + db.str_E(dayoff1) + "," + db.str_E(dayoff2) + "," + db.str_E(sex) + "," + db.str_E(birth) + "," + db.str_E(civil_status) + "," + db.str_E(religion) + "," + db.str_E(height) + "," + db.str_E(weight) + "," + db.str_E(father) + "," + db.str_E(father_address) + "," + db.str_E(father_contact) + "," + db.str_E(father_job) + "," + db.str_E(mother) + "," + db.str_E(mother_address) + "," + db.str_E(mother_contact) + "," + db.str_E(mother_job) + "," + db.str_E(emp_contact) + "," + db.str_E(home_tel) + "," + db.str_E(email) + "," + db.str_E(home_address) + "," + db.str_E(emergency_name) + "," + db.str_E(emergency_contact) + "," + db.str_E(em_home_address) + "," + db.str_E(relationship) + "," + db.str_E(shift_sched_sat_from) + "," + db.str_E(shift_sched_to) + "," + db.str_E(shift_sched_sat_to) + "," + db.str_E(fixed_rate) + "," + db.str_E(primary) + "," + db.str_E(secondary) + "," + db.str_E(tertiary) + "," + db.str_E(graduate) + "," + db.str_E(post_graduate) + ",'" + sss_table + "','" + fix_sched + "'," + db.str_E(sl_code) + "," + db.str_E(at_code) + "," + db.str_E(branch) + "," + db.str_E(potcode) + "";

                //db.DeleteOnTable(table, "empid='" + code + "' AND cancel='Y'");
                if (db.InsertOnTable(table, col, val))
                {
                    success = true;
                    db.set_pkm99("empid", db.get_nextincrementlimitchar(code, 8)); //changes from 'hr_empid'
                }
                else
                {
                    success = false;
                    //db.DeleteOnTable(table, "empid='" + code + "'");
                    MessageBox.Show("Failed on saving.");
                }
            }
            else 
            {
                col = "lastname=" + db.str_E(lastname) + ",firstname=" + db.str_E(firstname) + ",mi=" + db.str_E(mi) + ",positions=" + db.str_E(position) + ",department=" + db.str_E(department) + ",section =" + db.str_E(section) + ",date_hired='" + date_hired + "',contractual_date='" + contractual_date + "',date_resigned = '" + date_resigned + "',date_terminated='" + date_terminated + "',date_retired='" + date_retired + "',prohibition_date = '" + prohibition_date + "',date_regular ='" + date_regular + "',empstatus=" + db.str_E(empstatus) + ",contract_days=" + db.str_E(contract_days) + ",prc=" + db.str_E(prc) + ",ctc=" + db.str_E(ctc) + ",rate_type=" + db.str_E(rate_type) + ",pay_rate=" + db.str_E(pay_rate) + ",biometric=" + db.str_E(biometric) + ",sss=" + db.str_E(sss) + ",pagibig=" + db.str_E(pagibig) + ",philhealth=" + db.str_E(philhealth) + ",payroll_account=" + db.str_E(payroll_account) + ",tin=" + db.str_E(tin) + ",tax_bracket=" + db.str_E(tax_bracket) + ", shift_sched_from=" + db.str_E(shift_sched_from) + ",dayoff1=" + db.str_E(dayoff1) + ",dayoff2=" + db.str_E(dayoff2) + ",sex=" + db.str_E(sex) + ",birth=" + db.str_E(birth) + ",civil_status=" + db.str_E(civil_status) + ",religion=" + db.str_E(religion) + ",height=" + db.str_E(height) + ",weight=" + db.str_E(weight) + ",father=" + db.str_E(father) + ",father_address=" + db.str_E(father_address) + ", father_contact=" + db.str_E(father_contact) + ",father_job=" + db.str_E(father_job) + ",mother=" + db.str_E(mother) + ", mother_address=" + db.str_E(mother_address) + ",mother_contact=" + db.str_E(mother_contact) + ", mother_job=" + db.str_E(mother_job) + ", emp_contact=" + db.str_E(emp_contact) + ", home_tel=" + db.str_E(home_tel) + ",email=" + db.str_E(email) + ", home_address=" + db.str_E(home_address) + ",emergency_name=" + db.str_E(emergency_name) + ", emergency_contact=" + db.str_E(emergency_contact) + ",em_home_address=" + db.str_E(em_home_address) + ",relationship=" + db.str_E(relationship) + ",shift_sched_sat_from=" + db.str_E(shift_sched_sat_from) + ",shift_sched_to=" + db.str_E(shift_sched_to) + ",shift_sched_sat_to=" + db.str_E(shift_sched_sat_to) + ",fixed_rate=" + db.str_E(fixed_rate) + ",primary_ed=" + db.str_E(primary) + ",secondary_ed=" + db.str_E(secondary) + ",tertiary_ed=" + db.str_E(tertiary) + ",graduate=" + db.str_E(graduate) + ",post_graduate=" + db.str_E(post_graduate) + ", sss_bracket=" + db.str_E(sss_table) + ",fixed_sched='" + fix_sched + "',sl_code = " + db.str_E(sl_code) + ",at_code = " + db.str_E(at_code) + ",branch = " + db.str_E(branch) + ",potcode = " + db.str_E(potcode) + "";
                code = txt_code.Text;
                if (db.UpdateOnTable(table, col, "empid=" + db.str_E(code) + ""))
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

        private void disp_list(String WHERE ="")
        {
            try { dgv_list.Rows.Clear(); }
            catch (Exception) { }

            if(String.IsNullOrEmpty(WHERE) == false)
            {
                WHERE = " WHERE " + WHERE;
            }

            DataTable dt = db.QueryBySQLCode(@"SELECT empid, firstname, lastname, mi, d.dept_name, stat.description AS status_name, 
                        CASE WHEN fixed_sched='Y' THEN 'Yes'  WHEN  fixed_sched='E' THEN 'Exempted' ELSE 'No' END  AS fixed_sched, 
                        rt.description AS rate_type, b.name AS branchname, pot.description AS potdescription 
                        FROM rssys.hr_employee e 
                        LEFT JOIN rssys.hr_department d ON e.department=d.deptid
                        LEFT JOIN rssys.branch b ON b.code=e.branch 
                        LEFT JOIN rssys.hr_payout_type pot ON pot.potcode = e.potcode
                        LEFT JOIN rssys.hr_emp_status stat ON stat.statcode=e.empstatus 
                        LEFT JOIN rssys.hr_rate_type rt ON rt.ratecode=e.rate_type 
                        " + WHERE + " ORDER BY empid ASC");

            if (dt != null)
            {

                for (int r = 0; r < dt.Rows.Count; r++)
                {
                    int i = dgv_list.Rows.Add();
                    DataGridViewRow row = dgv_list.Rows[i];

                    row.Cells["ID"].Value = dt.Rows[r]["empid"].ToString();
                    row.Cells["name"].Value = dt.Rows[r]["firstname"].ToString() + " " + dt.Rows[r]["lastname"].ToString() + " " + dt.Rows[r]["mi"].ToString();
                    row.Cells["dept_name"].Value = dt.Rows[r]["dept_name"].ToString();
                    row.Cells["status_name"].Value = dt.Rows[r]["status_name"].ToString(); 
                    row.Cells["fix_schedule"].Value = dt.Rows[r]["fixed_sched"].ToString();
                    row.Cells["rate_type"].Value = dt.Rows[r]["rate_type"].ToString(); 

                    row.Cells["branchname"].Value = dt.Rows[r]["branchname"].ToString();
                    row.Cells["potdescription"].Value = dt.Rows[r]["potdescription"].ToString();
                }
            }
        }


        private void cbo_department_SelectionChangeCommitted(object sender, EventArgs e)
        {
            gc.load_section(cbo_section, cbo_department.SelectedValue.ToString());
            cbo_section.DroppedDown = true;
        }

        private void dateTimePicker4_ValueChanged(object sender, EventArgs e)
        {

        }

        private void tpg_background_Click(object sender, EventArgs e)
        {

        }

        private void tpg_education_Click(object sender, EventArgs e)
        {

        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_resigned.Checked == false)
            {
                dtp_resigned.Visible = false;
            }
            else
            {
                dtp_resigned.Visible = true;
            }
        }

        private void chk_terminated_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_terminated.Checked == false)
            {
                dtp_terminated.Visible = false;
            }
            else
            {
                dtp_terminated.Visible = true;
            }
        }

        private void chk_contractual_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_contractual.Checked == false)
            {
                dtp_contractual.Visible = false;
            }
            else
            {
                dtp_contractual.Visible = true;
            }
        }

        private void chk_probition_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_probition.Checked == false)
            {
                dtp_probitioned.Visible = false;
            }
            else
            {
                dtp_probitioned.Visible = true;
            }
        }

        private void chk_regular_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_regular.Checked == false)
            {
                dtp_regularized.Visible = false;
            }
            else
            {
                dtp_regularized.Visible = true;
            }
        }

        private void btn_additem_Click(object sender, EventArgs e)
        {
            frm_clear();

            isnew = true;
            goto_win2();
        }

        private void btn_upditem_Click(object sender, EventArgs e)
        {
            isnew = false;

            int r = -1;
            String code = "", name = "";
            try
            {
                if (dgv_list.Rows.Count > 1)
                {
                    r = dgv_list.CurrentRow.Index;

                    try
                    {
                        code = dgv_list["ID", r].Value.ToString();

                        display_employee(code);
                    }
                    catch { }

                    goto_win2();
                }
                else
                {
                    MessageBox.Show("Employee list is empty.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void display_employee(String code)
        {
            if(String.IsNullOrEmpty(code) == false)
            {
                DataTable dt = db.QueryBySQLCode("SELECT distinct emp.*,civil.*,day.*,dept.*,section.*,emp_status.*,pos.*,wtax.*,rate_type.* FROM rssys.hr_employee emp LEFT JOIN rssys.hr_civil_status civil ON civil.code = emp.civil_status LEFT JOIN rssys.hr_days day ON day.day = emp.dayoff1 LEFT JOIN rssys.hr_department dept ON dept.deptid = emp.department LEFT JOIN rssys.hr_depsection section ON section.secid = emp.section LEFT JOIN rssys.hr_emp_status emp_status ON emp_status.statcode = emp.empstatus LEFT JOIN rssys.hr_position pos ON pos.postid = emp.positions LEFT JOIN rssys.hr_rate_type rate_type ON rate_type.ratecode = emp.rate_type LEFT JOIN rssys.hr_wtax wtax ON wtax.code = emp.tax_bracket WHERE emp.empid ='" + code + "' LIMIT 1");

                if (dt.Rows.Count > 0)
                {
                    txt_code.Text = dt.Rows[0]["empid"].ToString();
                    txt_firstname.Text = dt.Rows[0]["firstname"].ToString();
                    txt_lastname.Text = dt.Rows[0]["lastname"].ToString();
                    txt_mi.Text = dt.Rows[0]["mi"].ToString();
                    cbo_department.SelectedValue = dt.Rows[0]["department"].ToString();
                    cbo_customer.SelectedValue = dt.Rows[0]["sl_code"].ToString();
                    cbo_cledger.SelectedValue = dt.Rows[0]["at_code"].ToString();
                    gc.load_section(cbo_section, cbo_department.SelectedValue.ToString());
                    cbo_section.SelectedValue = dt.Rows[0]["section"].ToString();
                    cbo_position.SelectedValue = dt.Rows[0]["positions"].ToString();
                    dtp_hired.Value = gm.toDateValue(dt.Rows[0]["date_hired"].ToString());
                    String name = dt.Rows[0]["firstname"].ToString() + " " + dt.Rows[0]["lastname"].ToString();

                    dtp_resigned.Hide();
                    dtp_terminated.Hide();
                    dtp_retired.Hide();
                    dtp_contractual.Hide();
                    dtp_probitioned.Hide();
                    dtp_regularized.Hide();

                    if (!String.IsNullOrEmpty(dt.Rows[0]["date_resigned"].ToString()))
                    {
                        if (gm.toDateValue(dt.Rows[0]["date_resigned"].ToString()) > gm.toDateValue("1900-01-01"))
                        {
                            chk_resigned.Checked = true; dtp_resigned.Show();
                            dtp_resigned.Value = gm.toDateValue(dt.Rows[0]["date_resigned"].ToString());
                        }
                    }
                    if (!String.IsNullOrEmpty(dt.Rows[0]["date_terminated"].ToString()))
                    {
                        if (gm.toDateValue(dt.Rows[0]["date_terminated"].ToString()) > gm.toDateValue("1900-01-01"))
                        {
                            chk_terminated.Checked = true; dtp_terminated.Show();
                            dtp_terminated.Value = gm.toDateValue(dt.Rows[0]["date_terminated"].ToString());
                        }
                    }
                    if (!String.IsNullOrEmpty(dt.Rows[0]["date_retired"].ToString()))
                    {
                        if (gm.toDateValue(dt.Rows[0]["date_retired"].ToString()) > gm.toDateValue("1900-01-01"))
                        {
                            chk_retired.Checked = true; dtp_retired.Show();
                            dtp_retired.Value = gm.toDateValue(dt.Rows[0]["date_retired"].ToString());
                        }
                    }
                    if (!String.IsNullOrEmpty(dt.Rows[0]["contractual_date"].ToString()))
                    {
                        if (gm.toDateValue(dt.Rows[0]["contractual_date"].ToString()) > gm.toDateValue("1900-01-01"))
                        {
                            chk_contractual.Checked = true; dtp_contractual.Show();
                            dtp_contractual.Value = gm.toDateValue(dt.Rows[0]["contractual_date"].ToString());
                        }
                    }
                    if (!String.IsNullOrEmpty(dt.Rows[0]["prohibition_date"].ToString()))
                    {
                        if (gm.toDateValue(dt.Rows[0]["prohibition_date"].ToString()) > gm.toDateValue("1900-01-01"))
                        {
                            chk_probition.Checked = true; dtp_probitioned.Show();
                            dtp_probitioned.Value = gm.toDateValue(dt.Rows[0]["prohibition_date"].ToString());
                        }
                    }
                    if (!String.IsNullOrEmpty(dt.Rows[0]["date_regular"].ToString()))
                    {
                        if (gm.toDateValue(dt.Rows[0]["date_regular"].ToString()) > gm.toDateValue("1900-01-01"))
                        {
                            chk_regular.Checked = true; dtp_regularized.Show();
                            dtp_regularized.Value = gm.toDateValue(dt.Rows[0]["date_regular"].ToString());
                        }
                    }

                    //cbo_department.SelectedValue = dt.Rows[0]["department"].ToString();
                    cbo_status.SelectedValue = dt.Rows[0]["empstatus"].ToString();
                    txt_contract_days.Text = dt.Rows[0]["contract_days"].ToString();
                    txt_prc_number.Text = dt.Rows[0]["prc"].ToString();
                    txt_ctc_num.Text = dt.Rows[0]["ctc"].ToString();
                    cbo_rate_type.SelectedValue = dt.Rows[0]["rate_type"].ToString();
                    try { txt_pay_rate.Text = Convert.ToDouble(dt.Rows[0]["pay_rate"]).ToString("N", new CultureInfo("en-US")); }
                    catch { txt_pay_rate.Text = "0.00"; }
                    txt_biometric.Text = dt.Rows[0]["biometric"].ToString();
                    cbo_sss.SelectedValue = dt.Rows[0]["sss_bracket"].ToString();
                    txt_sss_number.Text = dt.Rows[0]["sss"].ToString();
                    txt_pagibig.Text = dt.Rows[0]["pagibig"].ToString();
                    txt_philhealth.Text = dt.Rows[0]["philhealth"].ToString();
                    txt_payroll_act.Text = dt.Rows[0]["payroll_account"].ToString();
                    txt_tin.Text = dt.Rows[0]["tin"].ToString();
                    cbo_tax_bracket.SelectedValue = dt.Rows[0]["tax_bracket"].ToString();
                    txt_primary.Text = dt.Rows[0]["primary_ed"].ToString();
                    txt_secondary.Text = dt.Rows[0]["secondary_ed"].ToString();
                    txt_graduate.Text = dt.Rows[0]["graduate"].ToString();
                    txt_post_graduate.Text = dt.Rows[0]["post_graduate"].ToString();
                    txt_tertiary.Text = dt.Rows[0]["tertiary_ed"].ToString();

                    if (dt.Rows[0]["fixed_rate"].ToString() == "1")
                    {
                        chk_fixed_rate.Checked = true;
                    }
                    if (dt.Rows[0]["fixed_sched"].ToString() == "Y")
                    {
                        rb_yes_fix.Checked = true;
                        date_shift_sched_from.Enabled = true;
                        date_shift_sched_to.Enabled = true;
                        date_sift_sched_sat_from.Enabled = true;
                        date_sift_sched_sat_to.Enabled = true;

                    }
                    else if (dt.Rows[0]["fixed_sched"].ToString() == "N")
                    {
                        cbo_dayoff1.SelectedIndex = -1;
                        cbo_dayoff2.SelectedIndex = -1;
                        rb_no_fix.Checked = true;
                    }
                    else if (dt.Rows[0]["fixed_sched"].ToString() == "E")
                    {
                        cbo_dayoff1.SelectedIndex = -1;
                        cbo_dayoff2.SelectedIndex = -1;
                        cbo_dayoff2.SelectedIndex = -1;
                        rb_yes_fix.Checked = false;
                        rb_no_fix.Checked = false;
                        rb_excempted_fix.Checked = true;
                    }


                    date_shift_sched_from.Value = Convert.ToDateTime(DateTime.Now.ToString("M/d/yyyy") + " " + dt.Rows[0]["shift_sched_from"].ToString());
                    date_shift_sched_to.Value = Convert.ToDateTime(DateTime.Now.ToString("M/d/yyyy") + " " + dt.Rows[0]["shift_sched_to"].ToString());
                    date_sift_sched_sat_from.Value = Convert.ToDateTime(DateTime.Now.ToString("M/d/yyyy") + " " + dt.Rows[0]["shift_sched_sat_from"].ToString());
                    date_sift_sched_sat_to.Value = Convert.ToDateTime(DateTime.Now.ToString("M/d/yyyy") + " " + dt.Rows[0]["shift_sched_sat_to"].ToString());

                    cbo_dayoff1.SelectedValue = dt.Rows[0]["dayoff1"].ToString();
                    cbo_dayoff2.SelectedValue = dt.Rows[0]["dayoff2"].ToString();
                    cbo_gender.SelectedItem = dt.Rows[0]["sex"].ToString();
                    date_birth.Value = gm.toDateValue(dt.Rows[0]["birth"].ToString());
                    cbo_civil_stat.SelectedValue = dt.Rows[0]["civil_status"].ToString();
                    txt_religion.Text = dt.Rows[0]["religion"].ToString();

                    try { txt_height.Text = Convert.ToDouble(dt.Rows[0]["height"]).ToString("N", new CultureInfo("en-US")); }
                    catch { txt_height.Text = "0.00"; }

                    try { txt_weight.Text = Convert.ToDouble(dt.Rows[0]["weight"]).ToString("N", new CultureInfo("en-US")); }
                    catch { txt_weight.Text = "0.00"; }

                    txt_father.Text = dt.Rows[0]["father"].ToString();
                    txt_father_address.Text = dt.Rows[0]["father_address"].ToString();
                    txt_father_contact.Text = dt.Rows[0]["father_contact"].ToString();
                    txt_father_occupation.Text = dt.Rows[0]["father_job"].ToString();
                    txt_mother.Text = dt.Rows[0]["mother"].ToString();
                    txt_mother_address.Text = dt.Rows[0]["mother_address"].ToString();
                    txt_mother_contact.Text = dt.Rows[0]["mother_contact"].ToString();
                    txt_mother_occupation.Text = dt.Rows[0]["mother_job"].ToString();
                    txt_contact_no.Text = dt.Rows[0]["emp_contact"].ToString();
                    txt_home_tel.Text = dt.Rows[0]["home_tel"].ToString();
                    txt_email.Text = dt.Rows[0]["email"].ToString();
                    txt_home_address.Text = dt.Rows[0]["home_address"].ToString();
                    txt_ctc_name.Text = dt.Rows[0]["emergency_name"].ToString();
                    txt_ctc_no.Text = dt.Rows[0]["emergency_contact"].ToString();
                    txt_home_add.Text = dt.Rows[0]["em_home_address"].ToString();
                    txt_relation.Text = dt.Rows[0]["relationship"].ToString();

                    try { cbo_branch.SelectedValue = gm.toStr(dt.Rows[0]["branch"].ToString()); } catch { }
                    try { cbo_payouttype.SelectedValue = gm.toStr(dt.Rows[0]["potcode"].ToString()); } catch { }
                }
            }            
        }

        private void frm_clear()
        {
            txt_code.Text = "";
            txt_lastname.Text = "";
            txt_firstname.Text = "";
            txt_mi.Text = "";
            cbo_department.SelectedIndex = -1;
            cbo_section.SelectedIndex = -1;
            cbo_position.SelectedIndex = -1;
            dtp_hired.ResetText();

            chk_resigned.Checked = false;
            dtp_resigned.Visible = false;

            date_shift_sched_from.Enabled = false;
            date_shift_sched_to.Enabled = false;
            date_sift_sched_sat_from.Enabled = false;
            date_sift_sched_sat_to.Enabled = false;
            chk_contractual.Checked = false;
            dtp_contractual.Visible = false;

            chk_terminated.Checked = false;
            dtp_terminated.Visible = false;

            chk_retired.Checked = false;
            dtp_retired.Visible = false;

            chk_probition.Checked = false;
            dtp_probitioned.Visible = false;

            chk_regular.Checked = false;
            dtp_regularized.Visible = false;
            rb_yes_fix.Checked = false;
            cbo_status.SelectedIndex = -1;
            txt_contract_days.Text = "";
            txt_prc_number.Text = "";
            txt_ctc_num.Text = "";
            cbo_rate_type.SelectedIndex = -1;
            txt_pay_rate.Text = "";
            txt_biometric.Text = "";
            cbo_sss.SelectedIndex = -1;
            txt_sss_number.Text = "";
            txt_pagibig.Text = "";
            txt_philhealth.Text = "";
            txt_payroll_act.Text = "";
            txt_tin.Text = "";
            cbo_tax_bracket.SelectedIndex = -1;

            date_shift_sched_from.ResetText();
            date_sift_sched_sat_from.ResetText();

            date_shift_sched_to.ResetText();
            date_sift_sched_sat_from.ResetText();
            date_sift_sched_sat_to.ResetText();


            cbo_dayoff1.SelectedIndex = -1;
            cbo_dayoff2.SelectedIndex = -1;
            cbo_gender.SelectedIndex = -1;
            date_birth.ResetText();
            cbo_civil_stat.SelectedIndex = -1;
            txt_religion.Text = "";
            txt_height.Text = "";
            txt_weight.Text = "";
            txt_father.Text = "";
            txt_father_address.Text = "";
            txt_father_contact.Text = "";
            txt_father_occupation.Text = "";
            txt_mother.Text = "";
            txt_mother_address.Text = "";
            txt_mother_contact.Text = "";
            txt_mother_occupation.Text = "";
            txt_contact_no.Text = "";
            txt_home_tel.Text = "";
            txt_email.Text = "";
            txt_home_address.Text = "";
            txt_ctc_name.Text = "";
            txt_ctc_no.Text = "";
            txt_home_add.Text = "";
            txt_relation.Text = "";
            txt_primary.Text = "";
            txt_secondary.Text = "";
            txt_tertiary.Text = "";
            txt_graduate.Text = "";
            txt_post_graduate.Text = "";
        }

        private void btn_delitem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Unable to use this proccess.");
        }

        private void tpg_contribution_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label73_Click(object sender, EventArgs e)
        {

        }

        private void rb_yes_fix_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_yes_fix.Checked == true)
            {
                date_shift_sched_from.Enabled = true;
                date_shift_sched_to.Enabled = true;
                date_sift_sched_sat_from.Enabled = true;
                date_sift_sched_sat_to.Enabled = true;
                cbo_dayoff1.Enabled = true;
                cbo_dayoff2.Enabled = true;
            }
            else {
                date_shift_sched_from.Enabled = false;
                date_shift_sched_to.Enabled = false;
                date_sift_sched_sat_from.Enabled = false;
                date_sift_sched_sat_to.Enabled = false;
                
            }
        }

        private void rb_no_fix_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_no_fix.Checked == true)
            {
                cbo_dayoff1.SelectedIndex = -1;
                cbo_dayoff2.SelectedIndex = -1;
                date_shift_sched_from.Enabled = false;
                date_shift_sched_to.Enabled = false;
                date_sift_sched_sat_from.Enabled = false;
                date_sift_sched_sat_to.Enabled = false;
                cbo_dayoff1.Enabled = false;
                cbo_dayoff2.Enabled = false;
            }
        }

        private void rb_excempted_fix_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_excempted_fix.Checked == true)
            {
                cbo_dayoff1.SelectedIndex = -1;
                cbo_dayoff2.SelectedIndex = -1;
                date_shift_sched_from.Enabled = false;
                date_shift_sched_to.Enabled = false;
                date_sift_sched_sat_from.Enabled = false;
                date_sift_sched_sat_to.Enabled = false;
                cbo_dayoff1.Enabled = false;
                cbo_dayoff2.Enabled = false;
            }
        }

        private void btn_search_Click(object sender, EventArgs e)
        {
            String col = "";
            String search = txt_search.Text;

            if (cbo_searchby.SelectedIndex == 0)
            {
                col = "empid  LIKE '%" + search + "%'";
                disp_list(col);
            }
            if (cbo_searchby.SelectedIndex == 1)
            {
                col = "lastname LIKE '%"+search+"%' OR firstname LIKE '%"+search+"%'";
                disp_list(col);
            }
        }

        private void cbo_searchby_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void disp_search_list(String col)
        {
            try { dgv_list.Rows.Clear(); }
            catch (Exception) { }

            DataTable dt = db.QueryBySQLCode("SELECT * FROM rssys.hr_employee WHERE "+col);

            for (int r = 0; r < dt.Rows.Count; r++)
            {
                int i = dgv_list.Rows.Add();
                DataGridViewRow row = dgv_list.Rows[i];

                row.Cells["ID"].Value = dt.Rows[r]["empid"].ToString();
                row.Cells["name"].Value = dt.Rows[r]["firstname"].ToString() + " " + dt.Rows[r]["lastname"].ToString() + " " + dt.Rows[r]["mi"].ToString();

            }
        }

        private void txt_search_TextChanged(object sender, EventArgs e)
        {

        }

        private void cbo_status_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            int value = cbo_status.SelectedIndex;

            if (value != 0)
            {
                cbo_tax_bracket.Enabled = false;
                cbo_tax_bracket.SelectedIndex = -1;
            }
            else {
                cbo_tax_bracket.Enabled = true;
            }
            
            
        }

        private void cbo_customer_SelectedIndexChanged(object sender, EventArgs e)
        {
            String code = (cbo_customer.SelectedValue ?? "").ToString();
            cbo_cledger.SelectedValue = db.get_colval("m06", "at_code", "d_code = '" + code + "'");
        }

        private void chk_retired_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_retired.Checked == false)
            {
                dtp_retired.Visible = false;
            }
            else
            {
                dtp_retired.Visible = true;
            }
        }

        private void inc_pbar(int i, int rw)
        {
            try
            {
                if (pbar.Value <= rw)
                {
                    pbar.Invoke(new Action(() =>
                    {
                        pbar.Value = i;
                    }));
                }
                else
                {
                    pbar.Invoke(new Action(() =>
                    {
                        pbar.Value = rw;
                    }));

                    btnBrowse.Invoke(new Action(() =>
                    {
                        btnBrowse.Enabled = true;
                    }));
                }
            }
            catch (Exception)
            {

            }
        }

        private void bgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            Excel.Application xlApp;
            Excel.Workbook xlWorkBook;
            Excel.Worksheet xlWorkSheet;
            Excel.Range range;

            Boolean isnew = true, success = false, clear_inventory = false; 
            int rCnt = 0, cCnt = 0, rw = 0, cl = 0, i = 0;
            String filename = "", filenameOnly = "", importType = "", notificationText = "", notifyadd = null;
            String table = "hr_employee";
            String col = "", val = "", add_col = "", add_val = "";

            String empid = "", lastname = "", firstname = "", mi = "", section = "", positions = "", picture = "", department = "", date_hired = "", contractual_date = "", prohibition_date = "", date_regular = "", date_resigned = "", date_terminated = "", empstatus = "", contract_days = "", prc = "", ctc = "", rate_type = "", pay_rate = "", biometric = "", sss = "", pagibig = "", philhealth = "", payroll_account = "", tin = "", tax_bracket = "", dayoff1 = "", dayoff2 = "", sex = "", birth = "", civil_status = "", religion = "", height = "", weight = "", father = "", father_address = "", father_contact = "", father_job = "", mother = "", mother_address = "", mother_contact = "", mother_job = "", emp_contact = "", home_tel = "", email = "", home_address = "", emergency_name = "", emergency_contact = "", em_home_address = "", relationship = "", shift_sched_from = "", shift_sched_sat_from = "", shift_sched_to = "", shift_sched_sat_to = "", fixed_rate = "", graduate = "", primary_ed = "", tertiary_ed = "", secondary_ed = "", post_graduate = "", pagibig_bracket = "", philhealth_bracket = "", shift_sched = "", shift_sched_sat = "", sss_bracket = "", fixed_sched = "", sl_code = "", at_code = "";

            DateTime dt_temp = DateTime.Parse(db.get_systemdate(""));

            btnImport.Invoke(new Action(() =>
            {
                btnImport.Enabled = false;
            }));
            btnBrowse.Invoke(new Action(() =>
            {
                btnBrowse.Enabled = false;
            }));

            txt_status.Invoke(new Action(() =>
            {
                txt_status.Text = "Please wait while importing to database.";
            }));

            notificationText = "has added: ";

            try
            {
                int count = 0;
                String item_code = "";

                filename = openFileDialog1.FileName;
                filenameOnly = Path.GetFileName(filename);
                xlApp = new Excel.Application();
                xlWorkBook = xlApp.Workbooks.Open(@filename, 0, true, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
                xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
                range = xlWorkSheet.UsedRange;
                rw = range.Rows.Count;
                cl = range.Columns.Count;

                if (rw > 0)
                {
                    if (clear_inventory)
                    {
                        clear_allemployees();
                    }

                    for (rCnt = 4; rCnt <= rw; rCnt++)
                    {
                        empid = ""; 
                        lastname = ""; firstname = ""; mi = ""; 
                        section = ""; 
                        positions = ""; 
                        picture = "";
                        department = ""; 
                        date_hired = ""; 
                        contractual_date = ""; 
                        prohibition_date = ""; 
                        date_regular = ""; date_resigned = ""; date_terminated = ""; 
                        empstatus = ""; contract_days = ""; 
                        prc = ""; ctc = ""; 
                        rate_type = ""; pay_rate = ""; 
                        biometric = ""; 
                        sss = ""; pagibig = ""; philhealth = ""; payroll_account = ""; tin = ""; 
                        tax_bracket = ""; 
                        dayoff1 = ""; dayoff2 = ""; 
                        sex = ""; birth = ""; civil_status = ""; religion = ""; 
                        height = ""; weight = ""; 
                        father = ""; father_address = ""; father_contact = ""; father_job = ""; 
                        mother = ""; mother_address = ""; mother_contact = ""; mother_job = ""; 
                        emp_contact = ""; home_tel = ""; email = ""; home_address = ""; 
                        emergency_name = ""; emergency_contact = ""; em_home_address = ""; 
                        relationship = ""; 
                        shift_sched_from = ""; shift_sched_sat_from = ""; shift_sched_to = ""; shift_sched_sat_to = ""; 
                        fixed_rate = ""; 
                        graduate = ""; primary_ed = ""; tertiary_ed = ""; secondary_ed = ""; post_graduate = ""; 
                        pagibig_bracket = ""; philhealth_bracket = ""; 
                        shift_sched = ""; shift_sched_sat = ""; sss_bracket = ""; 
                        fixed_sched = ""; 
                        sl_code = ""; at_code = "";

                        if (rCnt != 1 && !String.IsNullOrEmpty(lastname))
                        {
                            item_code = db.get_pk("item_code");

                            col = "empid, lastname, firstname, mi, section, positions, picture, department, date_hired, contractual_date, prohibition_date, date_regular, date_resigned, date_terminated, empstatus, contract_days, prc, ctc, rate_type, pay_rate, biometric, sss, pagibig, philhealth, payroll_account, tin, tax_bracket, dayoff1, dayoff2, sex, birth, civil_status, religion, height, weight, father, father_address, father_contact, father_job, mother, mother_address, mother_contact, mother_job, emp_contact, home_tel, email, home_address, emergency_name, emergency_contact, em_home_address, relationship, shift_sched_from, shift_sched_sat_from, shift_sched_to, shift_sched_sat_to, fixed_rate, graduate, primary_ed, tertiary_ed, secondary_ed, post_graduate, pagibig_bracket, philhealth_bracket, shift_sched, shift_sched_sat, sss_bracket, fixed_sched, sl_code, at_code";
                            val = "" + empid + ", " + db.str_E(lastname) + ", " + db.str_E(firstname) + ", 'Y', 'S', " + db.str_E(mi) + ", " + section + ", " + db.str_E(positions) + ", " + db.str_E(department) + ", " + date_hired + ", " + contractual_date + "";
                            rtxt_msg.Invoke(new Action(() =>
                            {
                                rtxt_msg.Text = "INSERT INTO " + db.schema + ".items (" + col + ") VALUES (" + val + ")"; ;
                            }));
                            if (db.InsertOnTable("items", col, val))
                            {
                                success = true;
                                db.set_pkm99("item_code", db.get_nextincrementlimitchar(item_code, 12));
                            }
                            else
                            {
                                success = false;
                                MessageBox.Show("Error on inserting at counter number " + rCnt.ToString() + " of part number " + db.str_E(lastname) + "");
                            }

                            if (success == false)
                            {
                                MessageBox.Show("Failed on saving of item no. " + empid + " " + lastname + " at row no." + rCnt + ".");
                            }
                            else
                            {
                                count++;
                                lbl_minimum.Invoke(new Action(() =>
                                {
                                    lbl_minimum.Text = count.ToString();
                                }));
                                inc_pbar(count, rw);
                            }
                        }
                    }

                    MessageBox.Show("Number of rows inserted : " + (count));

                    btnImport.Invoke(new Action(() =>
                    {
                        btnImport.Enabled = true;
                    }));

                    if (count >= rw)
                    {
                        txt_status.Invoke(new Action(() =>
                        {
                            txt_status.Text = "Import completed";
                        }));
                    }

                    inc_pbar(rw, rw);
                }
                else
                {
                    MessageBox.Show("Empty Worksheet");
                }
                xlWorkBook.Close(true, null, null);
                xlApp.Quit();

                Marshal.ReleaseComObject(xlWorkSheet);
                Marshal.ReleaseComObject(xlWorkBook);
                Marshal.ReleaseComObject(xlApp);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error : " + ex.Message);
            }


            btn_back.Invoke(new Action(() =>
            {
                btn_back.Enabled = true;
            }));
            btnImport.Invoke(new Action(() =>
            {
                btnImport.Enabled = true;
            }));
            btnBrowse.Invoke(new Action(() =>
            {
                btnBrowse.Enabled = true;
            }));
        }

        private void clear_allemployees()
        {
            db.DeleteOnTable("hr_employee", "");
        }
    }

}
