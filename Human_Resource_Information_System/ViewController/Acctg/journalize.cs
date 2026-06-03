using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Human_Resource_Information_System
{
    public partial class journalize : Form
    {
        Report rpt;
        thisDatabase db = new thisDatabase();
        GlobalClass gc = new GlobalClass();
        GlobalMethod gm = new GlobalMethod();
        String rpt_j_code = "", rpt_j_num_frm = "", rpt_j_num_to = "";
        //1Report rpt;
        public journalize()
        {
            InitializeComponent();
            gc.load_openperiod(cbo_period);
            gc.load_journal(cbo_journal);
            gc.load_payroll_periods(cbo_payperiod);
            gc.load_branch(cbo_branch);
            cbo_journal.SelectedValue = "PJ";
            rpt = new Report();
            btn_viewreport.Enabled = false;
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cbo_period_SelectedIndexChanged(object sender, EventArgs e)
        {
            String[] str;
            String val = "";

            try
            {

                if (cbo_period.SelectedIndex > -1)
                {
                    val = cbo_period.SelectedValue.ToString();
                    str = val.Split('-');
                    String _fy = str[0].ToString();
                    String _mo = str[1].ToString();

                    dtp_frm.Value = gm.toDateValue(db.get_period_datefrom(_fy, _mo));
                    dtp_to.Value = gm.toDateValue(db.get_period_dateto(_fy, _mo));


                    //refresh_invoice();
                }
            }
            catch { }
        }

        private void btn_proceed_Click(object sender, EventArgs e)
        {
            Boolean proceed = false;
            if (cbo_journal.SelectedIndex != -1)
            {

                if (cbo_branch.SelectedIndex == -1)
                {
                    proceed = false;
                    MessageBox.Show("Please select branch.");
                    cbo_branch.DroppedDown = true;
                    return;
                    
                }else if(cbo_payperiod.SelectedIndex == -1){
                    proceed = false;
                    MessageBox.Show("Please select payroll period.");
                    cbo_payperiod.DroppedDown = true;
                    return;
                }else if (cbo_period.SelectedIndex == -1)
                {
                    proceed = false;
                    MessageBox.Show("Please select period.");
                    cbo_period.DroppedDown = true;
                    return;
                }
                else
                {


                    String[] pdate = cbo_get_text(cbo_payperiod).Split('-');
                    DateTime payto = gm.toDateValue(pdate.GetValue(1).ToString());
                    String mo = payto.Month.ToString();

                    String[] splt = cbo_get_value(cbo_period).Split('-');
                    String pmo = splt.GetValue(1).ToString();

                    String pperiod = cbo_get_value(cbo_payperiod);

                    //check if payroll already journalize
                    DataTable j = db.QueryBySQLCode("SELECT * FROM rssys.tr01 WHERE j_code = 'PJ' AND t_desc LIKE '%SALARIES%' AND t_date = '" + pdate.GetValue(1).ToString() + "' AND COALESCE(cancel,'')<>'Y'");
                    //employee
                    DataTable emp = db.QueryBySQLCode("SELECT at_code FROM rssys.hr_employee WHERE at_code IS NULL");
                    String message = "";
                    if(j.Rows.Count != 0)
                    {
                        proceed = false;
                        message = "Already Journalize Payroll Transaction.";
                    }
                    else if(emp.Rows.Count !=0)
                    {
                        proceed = false;
                        message = "Some Employee has no employee ledger.";
                    }
                    else 
                    {
                        proceed = true;
                    }

                    if (proceed)
                    {
                        if (pmo != mo)
                        {
                            if (MessageBox.Show("Payroll Period and Accounting Period are conflicted on dates. Are you sure you want to proceed?", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
                            {
                                bgWorker.RunWorkerAsync();
                            }
                            else { MessageBox.Show("Journalized Transaction cancelled."); }
                        }
                        else
                        {
                            if (MessageBox.Show("Are you sure you want to generate this Transaction?", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
                            {
                                bgWorker.RunWorkerAsync();
                            }
                            else { MessageBox.Show("Journalized Transaction cancelled."); }
                        }

                    }
                    else {
                        MessageBox.Show(message);
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select journal.");
            }
        }

        private void input_enable(Boolean bol)
        {
            dtp_frm.Invoke(new Action(() =>
            {
                dtp_frm.Enabled = bol;
            }));

            dtp_to.Invoke(new Action(() =>
            {
                dtp_to.Enabled = bol;
            }));

            cbo_journal.Invoke(new Action(() =>
            {
                cbo_journal.Enabled = bol;
            }));

            btn_close.Invoke(new Action(() =>
            {
                btn_close.Enabled = bol;
            }));

            btn_proceed.Invoke(new Action(() =>
            {
                btn_proceed.Enabled = bol;
            }));

            btn_viewreport.Invoke(new Action(() =>
            {
                btn_viewreport.Enabled = bol;
            }));

            cbo_period.Invoke(new Action(() =>
            {
                cbo_period.Enabled = bol;
            }));
            cbo_branch.Invoke(new Action(() =>
            {
                cbo_branch.Enabled = bol;
            }));
        }

        private void inc_pbar(int i)
        {
            try
            {
                pbar.Invoke(new Action(() =>
                {
                    pbar.Value += i;
                }));
            }
            catch (Exception) { reset_pbar(); }
        }

        private void reset_pbar()
        {
            pbar.Invoke(new Action(() =>
            {
                pbar.Value = 0;
            }));
        }

        public String cbo_get_value(ComboBox cbo)
        {
            String val = "";
            cbo.Invoke(new Action(() => { val = cbo.SelectedValue.ToString(); }));
            return val;
        }
        public String cbo_get_text(ComboBox cbo)
        {
            String val = "";
            cbo.Invoke(new Action(() => { val = cbo.Text; }));
            return val;
        }

        private void bgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            input_enable(false);
            thisDatabase db = new thisDatabase();
            Boolean success = false;


            //tr01
            String fy = "", mo = "", j_code = "", j_num = "", t_date = "", t_desc = "", user_id = "",systime = "",sysdate = "",branch = "";


            String[] splt = cbo_get_value(cbo_period).Split('-');
            String[] pdate = cbo_get_text(cbo_payperiod).Split('-');
            fy = splt.GetValue(0).ToString();
            mo = splt.GetValue(1).ToString();
            j_code = cbo_get_value(cbo_journal);
            j_num = db.get_colval("m05","j_num","j_code = '"+j_code+"'");
            t_date = pdate.GetValue(1).ToString();
            t_desc = "SALARIES " + t_date;
            user_id = GlobalClass.username;
            systime = db.get_systemtime();
            sysdate = db.get_systemdate("yyyy-MM-dd");
            branch = cbo_get_value(cbo_branch);

            DataTable dep = db.QueryBySQLCode("SELECT DISTINCT e.department,d.dept_name,d.at_code,SUM(netpay + sss_cont_a + philhealth_cont_a + pag_ibig_a + other_deduction) as grosspay FROM rssys.hr_employee e INNER JOIN rssys.hr_department d ON e.department = d.deptid INNER JOIN rssys.hr_emp_payroll p ON p.empid = e.empid WHERE p.ppid = '" + cbo_get_value(cbo_payperiod) + "' GROUP BY e.department,d.dept_name,d.at_code");
            int cdep = dep.Rows.Count;
            //insert tr01
            String colt1 = "", valt1 = "";

            colt1 = "fy,mo,j_code,j_num,t_date,t_desc,user_id,systime,sysdate,branch";

            valt1 = "'" + fy + "','" + mo + "','" + j_code + "','" + j_num + "','" + t_date + "','" + t_desc + "','" + user_id + "','" + systime + "','" + sysdate + "','" + branch + "'";


            Double netpay = 0;


            if (db.InsertOnTable("tr01", colt1, valt1))
            {
                success = true;
                /**/
                //SELECT p.*,concat(e.firstname,' ',e.lastname) as ename FROM rssys.hr_emp_payroll p LEFT JOIN rssys.hr_employee e ON p.empid = e.empid WHERE p.ppid = '11/21/19'
                DataTable dt = db.QueryBySQLCode("SELECT p.*,concat(e.firstname,' ',e.lastname) as ename,e.sl_code,e.empid FROM rssys.hr_emp_payroll p LEFT JOIN rssys.hr_employee e ON p.empid = e.empid WHERE p.ppid = '" + cbo_get_value(cbo_payperiod) + "'");

                pbar.Invoke(new Action(() =>
                {
                    pbar.Maximum = dt.Rows.Count + 1;
                }));

                /*
                DataTable dtswages = db.QueryBySQLCode("SELECT SUM(basic_pay) as basicpay FROM rssys.hr_emp_payroll WHERE ppid = '" + cbo_get_value(cbo_payperiod) + "'");
                
                String swages = dtswages.Rows[0]["basicpay"].ToString();*/
                int seqnum = cdep;
                String atcode = db.get_colval("m99", "wages_code","");
                Double total = 0.00;
                String slcode = "", slname = "";

                

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    slcode = dt.Rows[i]["sl_code"].ToString();
                    slname = db.get_colval("m06", "d_name", "d_code = '"+slcode+"'");
                    //wages payable
                    if (dt.Rows[i]["netpay"].ToString() != "0.00")
                    {
                        seqnum = seqnum + 1;
                        
                        if (addtr02(j_code, j_num, seqnum.ToString(), atcode, slcode, slname, "001", "", "0.00", dt.Rows[i]["netpay"].ToString(), "", "Netpay of " + dt.Rows[i]["ename"].ToString()))
                        {
                            
                        }
                        total = total + gm.toNormalDoubleFormat(dt.Rows[i]["netpay"].ToString());
                    }
                    //end of wages payable

                     //sss,philhealth,pagibig
                    if (dt.Rows[i]["sss_cont_a"].ToString() != "0.00" || dt.Rows[i]["philhealth_cont_a"].ToString() != "0.00" ||
                        dt.Rows[i]["pag_ibig_a"].ToString() != "0.00")
                    {
                        seqnum = seqnum + 1;
                        Double benefits = gm.toNormalDoubleFormat(dt.Rows[i]["sss_cont_a"].ToString()) +
                                           gm.toNormalDoubleFormat(dt.Rows[i]["philhealth_cont_a"].ToString()) +
                                           gm.toNormalDoubleFormat(dt.Rows[i]["pag_ibig_a"].ToString());
                        if (addtr02(j_code, j_num, seqnum.ToString(), "21027", slcode, slname, "001", "", "0.00", benefits.ToString(), "", "Benefits of " + dt.Rows[i]["ename"].ToString()))
                        {

                        }
                        total = total + benefits;
                    }
                    //sss,philhealth,pagibig 

                    //other_deduction
                    if (dt.Rows[i]["other_deduction"].ToString() != "0.00")
                    {
                        String oatcode = "";
                        String empid = dt.Rows[i]["empid"].ToString();
                        DataTable c = db.QueryBySQLCode("SELECT d.at_code,de.amount FROM rssys.hr_other_deductions d INNER JOIN rssys.hr_deduction_entry de ON d.code = de.deduction_code WHERE de.emp_no = '" + empid + "' AND de.payroll_period = '" + cbo_get_value(cbo_payperiod) + "'");
                        if(c.Rows.Count != 0){
                            
                            for (int j = 0; j<c.Rows.Count;j++)
                            {
                                seqnum = seqnum + 1;
                                oatcode = c.Rows[j]["at_code"].ToString();
                                if (addtr02(j_code, j_num, seqnum.ToString(), oatcode, slcode, slname, "001", "", "0.00", c.Rows[j]["amount"].ToString(), "", "Deductions of " + dt.Rows[i]["ename"].ToString()))
                                {
                                      
                                }
                                
                            }
                            total = total + gm.toNormalDoubleFormat(dt.Rows[i]["other_deduction"].ToString());
                        }
            
                    }

                    // end of other_deduction
                    //UPDATE rssys.hr_emp_payroll SET jrnlz = 'Y' WHERE ppid = '' AND empid = ''
                    db.UpdateOnTable("hr_emp_payroll", "jrnlz='Y'", "ppid = '" + cbo_get_value(cbo_payperiod) + "' AND empid = '" + dt.Rows[i]["empid"].ToString() + "'");
                    inc_pbar(i); 
                }
                

                //insert salaries and wages
                for (int i = 0; i < dep.Rows.Count;i++)
                {
                    int seq = i + 1;
                    Double swages = gm.toNormalDoubleFormat(dep.Rows[i]["grosspay"].ToString());
                    if (addtr02(j_code, j_num, seq.ToString(), dep.Rows[i]["at_code"].ToString(), "", "", "001", "", swages.ToString(), "0.00", "", "DEPARMENT OF " + dep.Rows[i]["dept_name"].ToString()))
                    {
                        inc_pbar(dt.Rows.Count + i + 1);
                    }
                }
                
                rpt_j_code = j_code;
                rpt_j_num_frm = j_num;
                rpt_j_num_to = j_num;
                
                db.UpdateOnTable("m05", "j_num='" + db.get_nextincrement(j_num) + "'", "j_code='" + j_code + "'");
                 

            }
            else {
                success = false;
            }

     

            input_enable(true);
            if (success)
            {
                MessageBox.Show("Successfully Journalize Payroll Transactions");
            }
            else { 
                
            }
            

            
        }

        private Boolean addtr02(String j_code, String j_num, String seq_num, String at_code, String sl_code, String sl_name, String cc_code, String prj_code, String debit, String credit, String invoice, String seq_desc)
        {
            Boolean flag = false;
            String colt2 = "j_code, j_num, seq_num, at_code, sl_code, sl_name, cc_code, prj_code, debit, credit, invoice, seq_desc";
            String atcode = db.get_colval("m99", "wages_code", "");
            String valt2 = "'" + j_code + "','" + j_num + "', '"+seq_num+"','"+at_code+"','"+sl_code+"','"+sl_name+"','"+cc_code+"','','" + debit + "','"+credit+"','"+invoice+"','"+seq_desc+"'";


            //insert salaries and wages
            if (db.InsertOnTable("tr02", colt2, valt2))
            {
                flag = true;
            }
            return flag;
        }

        private void btn_viewreport_Click(object sender, EventArgs e)
        {
            view_report();
        }

        private void view_report()
        {

            BeginInvoke(new Action(() =>
            {
                rpt.print_journalized_new("Journalize Payroll Transactions", rpt_j_code, rpt_j_num_frm, rpt_j_num_to);
                rpt.Show();
                /*if (j_type == "H")
                {
                    rpt.print_journalizedhotel(j_code, dtfrm, dtto);
                }*/
            }));
        }

        


    }
}
