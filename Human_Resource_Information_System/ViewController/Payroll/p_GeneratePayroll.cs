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
    public partial class p_GeneratePayroll : Form
    {

      
        private GlobalClass gc;
        private GlobalMethod gm = new GlobalMethod();
        thisDatabase db = new thisDatabase();
       
        public p_GeneratePayroll()
        {
            gc = new GlobalClass();
            gm = new GlobalMethod();
            InitializeComponent();
        }

        private void btn_generate_Click(object sender, EventArgs e)
        {
            bg_worker.RunWorkerAsync();
            btn_generate.Enabled = true;
        }

        private void bg_worker_DoWork(object sender, DoWorkEventArgs e)
        {
            String table = "hr_emp_payroll", col = "", val = "", emp_pay_code = "", sss = "0.00", philhealth = "0.00", pagibig = "0.00", other_earnings = "0.00", other_deductions = "0.00", p_date = "2023-01-01";
            String month13amnt = "0.00";
            String summ_code = "", empid = "", days_worked = "0.00", absences = "0.00", late = "", undertime = "", overtime = "", ppid = "";
            Double total_late = 0.00, total_under_time = 0.00, total_overtime = 0.00, legal_hol_ot = 0.00, special_hol_ot = 0.00, dayoff_ot_total = 0.00, wtax = 0.00;
            Double legal_hol_ot_b = 0.00, daily_rate = 0.00, hour_rate = 0.00, special_hot_ot_b, legal_hol_pay, netpay, special_hol_ot_p;
            Double legal_hour = 0.00, special_hour = 0.00, regpay = 0.00, total_loan;
            Double vl_a = 0.00, vl_b = 0.00, sl_a = 0.00, sl_b = 0.00, pl_a = 0.00, pl_b = 0.00;
            Double total_late_min = 0.00, minute_rate = 0.00, late_amt = 0.00, ut_amt = 0.00, t_amt = 0.00, absent_amt = 0.00, basic_pay = 0.00, reg_ot_b = 0.00, day_off_ot_b = 0.00, special_hol_p = 0.00;

            Double sssd = 0.00, phild = 0.00, pagibigd = 0.00;
            String total_days = "0", leave_amnt = "0", leave_type = "0", ltotal = "0", g = "0", d="0";
            DataTable dtr = get_generated_dtr();
            Boolean success = false;
            int bar = 1;
            DataTable holidays = null, emp_payrate = null;
            DataTable payroll = db.QueryBySQLCode("SELECT DISTINCT(dtr.ppid), pp.d_sss_c,pp.d_philhealth,pp.d_pagibig,pp.gen_13_month from rssys.hr_dtr_sum_employees dtr LEFT JOIN rssys.hr_payrollpariod pp on dtr.ppid = pp.pay_code WHERE dtr.isgenerated = '0'");

            if (dtr.Rows.Count > 0)
            {
                //try
                //{
                pbar.Invoke(new Action(() =>
                {
                    pbar.Maximum = dtr.Rows.Count;
                }));

                for (int r = 0; r < dtr.Rows.Count; r++)
                {
                    legal_hol_ot = 0;  special_hol_ot = 0;

                    ppid = dtr.Rows[r]["ppid"].ToString();
                    empid = dtr.Rows[r]["empid"].ToString();
                    days_worked = dtr.Rows[r]["days_worked"].ToString();
                    absences = dtr.Rows[r]["absences"].ToString();
                    late = dtr.Rows[r]["late"].ToString();
                    undertime = dtr.Rows[r]["undertime"].ToString();
                    overtime = dtr.Rows[r]["total_overtime"].ToString();

                    legal_hol_ot = get_legal_hol_ot(empid, ppid);
                    legal_hol_ot_b = get_legal_hol_ot_b(empid, ppid);
                    //Double hrate = Math.Round(hour_rate * gm.toNormalDoubleFormat((payroll.Rows[0]["legal_hol_ot_a"] ?? "0.00").ToString()),2);
                    special_hol_ot = get_special_hol_ot(empid, ppid);
                    dayoff_ot_total = get_dayoff_ot_total(empid, ppid);

                    total_late = gm.toNormalDoubleFormat(TimeSpan.Parse(late).TotalHours);
                    total_late_min = gm.toNormalDoubleFormat(TimeSpan.Parse(late).TotalHours);
                    total_under_time = gm.toNormalDoubleFormat(TimeSpan.Parse(undertime).TotalHours);
                    total_overtime = gm.toNormalDoubleFormat(TimeSpan.Parse(overtime).TotalHours);

                    emp_payrate = db.QueryBySQLCode("SELECT pay_rate,rate_type FROM rssys.hr_employee WHERE empid = '" + empid + "'");

                    if (payroll.Rows[0]["d_sss_c"].ToString() == "Y")
                    {
                        sss = get_sss_deduction(empid);
                    }
                    if (payroll.Rows[0]["d_philhealth"].ToString() == "Y")
                    {
                        philhealth = get_philhealth_deduction(emp_payrate.Rows[0]["pay_rate"].ToString(), empid, emp_payrate.Rows[0]["rate_type"].ToString());
                    }
                    if (payroll.Rows[0]["d_pagibig"].ToString() == "Y")
                    {
                        pagibig = get_pagibig_deduction(emp_payrate.Rows[0]["pay_rate"].ToString(), empid);
                    }

                    total_days = get_total_day(ppid, empid);
                    //String rate_type = emp_payrate.Rows[0]["rate_type"].ToString();
                    leave_amnt = get_leaves_amount(empid, ppid);
                    leave_type = get_leaves_type(empid, ppid);
                    ltotal = get_leaves_days(empid, ppid);

                    if (emp_payrate.Rows[0]["rate_type"].ToString() == "D")
                    {
                        daily_rate = gm.toNormalDoubleFormat(emp_payrate.Rows[0]["pay_rate"].ToString());
                        sssd = gm.toNormalDoubleFormat(sss) * gm.toNormalDoubleFormat(total_days.ToString());
                        sss = sssd.ToString("0.00");
                        phild = gm.toNormalDoubleFormat(philhealth) * gm.toNormalDoubleFormat(total_days.ToString());
                        philhealth = phild.ToString("0.00");
                        pagibigd = gm.toNormalDoubleFormat(pagibig) * gm.toNormalDoubleFormat(total_days.ToString());
                        pagibig = pagibigd.ToString("0.00");
                        regpay = daily_rate * (gm.toNormalDoubleFormat(total_days) - gm.toNormalDoubleFormat(ltotal));
                    }
                    else if (emp_payrate.Rows[0]["rate_type"].ToString() == "M")
                    {
                        daily_rate = (gm.toNormalDoubleFormat(emp_payrate.Rows[0]["pay_rate"].ToString()) * 12) / 314;
                        regpay = gm.toNormalDoubleFormat(emp_payrate.Rows[0]["pay_rate"].ToString()) / 2;
                    }
                    else if (emp_payrate.Rows[0]["rate_type"].ToString() == "W")
                    {
                        daily_rate = gm.toNormalDoubleFormat(emp_payrate.Rows[0]["pay_rate"].ToString());
                        regpay = daily_rate * (gm.toNormalDoubleFormat(total_days) - gm.toNormalDoubleFormat(ltotal) - 1);
                    }

                    if (leave_type == "VL")
                    {
                        vl_a = gm.toNormalDoubleFormat(ltotal.ToString());
                        vl_b = gm.toNormalDoubleFormat(leave_amnt.ToString());
                    }
                    else if (leave_type == "SL")
                    {
                        sl_a = gm.toNormalDoubleFormat(ltotal.ToString());
                        sl_b = gm.toNormalDoubleFormat(leave_amnt.ToString());
                    }
                    else if (leave_type == "PL")
                    {
                        pl_a = gm.toNormalDoubleFormat(ltotal.ToString());
                        pl_b = gm.toNormalDoubleFormat(leave_amnt.ToString());
                    }

                    hour_rate = (daily_rate / 8);
                    minute_rate = (hour_rate / 60);

                    //Double late_amt = (gm.toNormalDoubleFormat(late.ToString()) * 60) * minute_rate;
                    late_amt = (total_late * 60) * minute_rate;
                    ut_amt = (total_under_time * 60) * minute_rate;
                    t_amt = late_amt + ut_amt;
                    absent_amt = gm.toNormalDoubleFormat(absences) * daily_rate;
                    basic_pay = Math.Round((hour_rate - (t_amt + absent_amt)), 2);

                    if (payroll.Rows[0]["gen_13_month"].ToString() == "Y")
                    {
                        month13amnt = get_13_month_pay(empid, ppid, basic_pay);
                    }

                    p_date = get_payroll_date(empid, ppid);
                    reg_ot_b = hour_rate * gm.toNormalDoubleFormat(total_overtime.ToString("0.00")) * 1.25;
                    
                    //legal_hol_ot_b = Math.Round(hour_rate * legal_hol_ot,2);
                    //checkDutyOnHol(String empid,String datein)
                    total_loan = get_loan(empid, ppid);

                    legal_hour = get_legal_hol_hour(empid, ppid);
                    special_hour = get_special_hol_hour(empid, ppid);
                    legal_hol_pay = get_legal_hol_pay(empid, ppid);
                    special_hot_ot_b = get_special_hol_ot_b(empid, ppid);
                    day_off_ot_b = get_day_off_pay(empid, ppid);
                    other_earnings = get_other_earnings(empid, ppid);
                    other_deductions = get_other_deductions(empid, ppid);
                    wtax = get_with_tax(empid);
                    // Double sprate = daily_rate * (30 / 100.00);
                    special_hol_p = get_special_hol_pay(empid, ppid);

                    g = calculate_gross(basic_pay.ToString("0.00"), reg_ot_b.ToString("0.00"), day_off_ot_b.ToString("0.00"), legal_hol_ot_b.ToString("0.00"), special_hot_ot_b.ToString("0.00"), "0.00", other_earnings, "0.00", "0.00", "0.00", month13amnt, leave_amnt);
                    //String d = total_deduction(sss, "0.00", pagibig, wtax, other_deductions, "0.00", "0.00");
                    d = calculate_deduction(sss, philhealth, pagibig, wtax.ToString("0.00"), other_deductions, total_loan.ToString("0.00"), "0.00");
                    netpay = gm.toNormalDoubleFormat(g) - gm.toNormalDoubleFormat(d);
                    //total_deduction(String sss,String phil_health,String pagibig,String wtax,String other_deduction,String loan,String others)
                    emp_pay_code = db.get_pk("emp_pay_code");

                    //add basic_pay,vl_a,vl_b,sl_a,sl_b,pl_a,pl_b,reqular_ot_b
                    //reg_ot_b_total = hour_rate * gm.toNormalDoubleFormat(txt_reg_ot_a.Text.ToString()) ;
                    col = "emp_pay_code, empid, days_worked, regular_pay, abcences, late, undertime, basic_pay, vl_a, vl_b, sl_a, sl_b, pl_a, pl_b, overtime, regular_ot_a, reqular_ot_b, ppid, legal_hol_ot_a, legal_hol_ot_b, special_hol_ot_a, special_hol_ot_b, legal_hol_pay_a, legal_hol_pay_b, spl_hol_pay_a, spl_hol_pay_b, dayoff_ot_a, dayoff_ot_b, sss_cont_a, philhealth_cont_a, pag_ibig_a, other_earnings, other_deduction, advances_loans, w_tax, absent_amnt, late_amnt, leave_amnt, netpay, leave_type, leave_days, amnt13month, hour_rate";

                    val = "'" + emp_pay_code + "','" + empid + "','" + gm.toNormalDoubleFormat(days_worked) + "','" + gm.toNormalDoubleFormat(regpay.ToString()) + "','" + gm.toNormalDoubleFormat(absences) + "','" + gm.toNormalDoubleFormat(total_late.ToString("0.00")) + "','" + total_under_time.ToString("0.00") + "','" + basic_pay.ToString("0.00") + "','" + vl_a.ToString("0.00") + "','" + vl_b.ToString("0.00") + "','" + sl_a.ToString("0.00") + "','" + sl_b.ToString("0.00") + "','" + pl_a.ToString("0.00") + "','" + pl_b.ToString("0.00") + "','" + total_overtime.ToString("0.00") + "','" + total_overtime.ToString("0.00") + "','" + reg_ot_b.ToString("0.00") + "','" + ppid + "','" + legal_hol_ot.ToString("0.00") + "','" + legal_hol_ot_b.ToString("0.00") + "','" + special_hol_ot.ToString("0.00") + "','" + special_hot_ot_b.ToString("0.00") + "','" + legal_hour.ToString("0.00") + "','" + legal_hol_pay.ToString("0.00") + "','" + special_hour.ToString("0.00") + "','" + special_hol_p.ToString("0.00") + "','" + dayoff_ot_total.ToString("0.00") + "','" + day_off_ot_b.ToString("0.00") + "','" + gm.toNormalDoubleFormat(sss) + "','" + gm.toNormalDoubleFormat(philhealth) + "','" + gm.toNormalDoubleFormat(pagibig) + "','" + gm.toNormalDoubleFormat(other_earnings) + "','" + gm.toNormalDoubleFormat(other_deductions) + "','" + total_loan.ToString("0.00") + "','" + wtax.ToString("0.00") + "','" + absent_amt.ToString("0.00") + "','" + late_amt.ToString("0.00") + "','" + leave_amnt + "','" + netpay + "','" + leave_type + "','" + ltotal.ToString() + "','" + month13amnt + "','" + gm.toNormalDoubleFormat(hour_rate) + "'";

                    //error
                    if (db.InsertOnTable(table, col, val))
                    {
                        col = "isgenerated='1'";

                        if (db.UpdateOnTable("hr_dtr_sum_employees", col, "empid='" + empid + "' AND ppid='" + ppid + "'"))
                        {
                            success = true;
                            inc_pbar(bar, dtr.Rows.Count);
                            bar++;
                        }
                        db.set_pkm99("emp_pay_code", db.get_nextincrementlimitchar(emp_pay_code, 8));
                    }
                }
                if (success)
                {
                    String period = get_payrol_period(ppid);
                    MessageBox.Show("New payroll was generated From " + period + " .");
                }
                // }
                // catch(Exception ex)
                // {
                //     MessageBox.Show("Payroll cannot be generated. Something went wrong. " + ex.Message);
                // }
            }
            else
            {
                MessageBox.Show("No generated DTR is available.");
            }

            this.Invoke((MethodInvoker)delegate
            {
                this.btn_generate.Enabled = true;
            });
        }

        private String get_payroll_date(String empid,String ppid) {
            DataTable payroll = db.QueryBySQLCode("SELECT date_from,date_to FROM rssys.hr_payrollpariod WHERE pay_code = '" + ppid + "'");
            return payroll.Rows[0]["date_to"].ToString();
        }

        private String get_13_month_pay(String empid,String ppid,Double currentbpay) 
        {
            //SELECT *FROM rssys.hr_emp_payroll where empid = '00000009' AND payroll_date BETWEEN '2018-01-01' AND '2018-01-01';
            Double result = 0.00;
            Double total_b_pay = 0.00;
            DataTable payroll = db.QueryBySQLCode("SELECT * FROM rssys.hr_payrollpariod WHERE pay_code = '" + ppid + "'");
            DateTime date_from, date_to;
            date_from = DateTime.Parse(payroll.Rows[0]["gen_13month_from"].ToString());
            date_to = DateTime.Parse(payroll.Rows[0]["gen_13month_to"].ToString());
            DataTable emp_payroll = db.QueryBySQLCode("SELECT rssys.hr_emp_payroll.basic_pay,rssys.hr_payrollpariod.date_from,rssys.hr_payrollpariod.date_to FROM rssys.hr_emp_payroll INNER JOIN rssys.hr_payrollpariod ON rssys.hr_emp_payroll.ppid=rssys.hr_payrollpariod.pay_code WHERE rssys.hr_emp_payroll.empid = '" + empid + "' and rssys.hr_payrollpariod.date_to BETWEEN '" + date_from + "' AND '" + date_to + "'");
            for (int i = 0; i < emp_payroll.Rows.Count;i++)
            {
               if(emp_payroll.Rows[0]["basic_pay"].ToString() != ""){
                   Double b_pay = gm.toNormalDoubleFormat(emp_payroll.Rows[0]["basic_pay"].ToString());
                   total_b_pay = total_b_pay + b_pay;
               } 
               
            }

            total_b_pay = total_b_pay + currentbpay;
            result = Math.Round(total_b_pay / 12,2);
            return result.ToString("0.00");        
        }

        private Double get_loan(String empid,String ppid)
        {
            DataTable payroll = db.QueryBySQLCode("SELECT date_from,date_to FROM rssys.hr_payrollpariod WHERE pay_code = '" + ppid + "'");
            DataTable loan = db.QueryBySQLCode("SELECT * FROM rssys.hr_loanhdr where employee_no = '"+empid+"'");
            DateTime date_from, date_to;
            Double total_loan_amt = 0.00;
            date_from = DateTime.Parse(payroll.Rows[0]["date_from"].ToString());
            date_to = DateTime.Parse(payroll.Rows[0]["date_to"].ToString());
            for (int l = 0; l < loan.Rows.Count; l++ )
            {
                DateTime sdeduc = Convert.ToDateTime(loan.Rows[l]["deduction_date"]);
                //loan_amount,loan_deduction
                Double loanamt = gm.toNormalDoubleFormat(loan.Rows[l]["loan_amount"].ToString());
                Double loandeduc = gm.toNormalDoubleFormat(loan.Rows[l]["loan_deduction"].ToString());
                Double tdaysdeduc = loanamt / loandeduc;
                int tdays = (int)tdaysdeduc;
                DateTime tdeduc = sdeduc.AddDays(tdays);
                 foreach (DateTime day in EachDay(date_from, date_to))
                 {
                     foreach (DateTime lnday in EachDay(sdeduc, tdeduc)) {
                         int res = DateTime.Compare(lnday, day);
                         int checkiflastday = DateTime.Compare(lnday, tdeduc);
                         if (res == 0 && checkiflastday != 0)
                         {
                             total_loan_amt = total_loan_amt + loandeduc;
                             break;
                         }
                         if(checkiflastday == 0){
                             Double deduction = loanamt - (loandeduc * tdays);
                             total_loan_amt = total_loan_amt + deduction;
                             break;
                         }
                     
                     }
                     /*
                     int res = DateTime.Compare(lday, day);
                     if (res == 0) {                     
                     } 
                      */
                 }
            }
            return total_loan_amt;
        }

        private String get_total_day(String ppid,String empid)
        {
            String result = "";
            int total = 0;
            String datein = "";
            DateTime date_from, date_to;
            DataTable payroll = db.QueryBySQLCode("SELECT date_from,date_to FROM rssys.hr_payrollpariod WHERE pay_code = '" + ppid + "'");

            if(payroll.Rows.Count>0)
            {
                date_from = DateTime.Parse(payroll.Rows[0]["date_from"].ToString());
                date_to = DateTime.Parse(payroll.Rows[0]["date_to"].ToString());

                foreach (DateTime day in EachDay(date_from, date_to))
                {
                    total = total + 1;
                }
            }
            return total.ToString();
        }

        public Boolean checkHoliday(String date) {
            DataTable holiday = db.QueryBySQLCode("SELECT date_holiday,holiday_type FROM rssys.hr_holidays WHERE date_holiday = '" + date + "' AND holiday_type = 'L'");
            if (holiday.Rows.Count > 0)
            {
                return true;
            }
            return false;
        }

        public String calculate_gross(String basicp,String reg_pay,String day_off_pay,String legal_hol_pay,String special_hol_pay,String ndiff,String other_earning,String vl_b,String sl_b,String pl_b,String month13amnt,String leaveamnt) {
            Double total = gm.toNormalDoubleFormat(basicp) + gm.toNormalDoubleFormat(reg_pay) + gm.toNormalDoubleFormat(day_off_pay)
                + gm.toNormalDoubleFormat(legal_hol_pay) + gm.toNormalDoubleFormat(special_hol_pay) + gm.toNormalDoubleFormat(ndiff)
                + gm.toNormalDoubleFormat(other_earning) + gm.toNormalDoubleFormat(vl_b) + gm.toNormalDoubleFormat(sl_b) + gm.toNormalDoubleFormat(pl_b) + gm.toNormalDoubleFormat(month13amnt) + gm.toNormalDoubleFormat(leaveamnt);

            return total.ToString("0.00");
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
                }
            }
            catch (Exception)
            {

            }
        }

        public String calculate_deduction(String sss, String phil_health, String pagibig, String wtax, String other_deduction, String loan, String others)
        {
            Double totald = gm.toNormalDoubleFormat(sss) + gm.toNormalDoubleFormat(phil_health) + gm.toNormalDoubleFormat(pagibig) + gm.toNormalDoubleFormat(wtax) + gm.toNormalDoubleFormat(other_deduction) + gm.toNormalDoubleFormat(loan) + gm.toNormalDoubleFormat(others);
            return totald.ToString("0.00");

        }

        public void disp_list()
        {
            DataTable dt = db.QueryBySQLCode("");

        }

        private DataTable get_generated_dtr()
        {
            DataTable dt = null;
            try
            {
                dt = db.QueryBySQLCode("SELECT * from rssys.hr_dtr_sum_employees WHERE isgenerated = '0'");
            }
            catch { }

            return dt;
        }

        private String get_leaves_amount(String empid,String ppid) {

            String result = "0.00";
            Double total = 0.00;
            String leave_code = "";
            DateTime pdate_from, pdate_to;
            String datein;
            //get the empid,get the leaves entr 
            DataTable emp_payrate = db.QueryBySQLCode("SELECT pay_rate,rate_type FROM rssys.hr_employee WHERE empid = '" + empid + "'");
            DataTable payroll = db.QueryBySQLCode("SELECT date_from,date_to FROM rssys.hr_payrollpariod WHERE pay_code = '" + ppid + "'");
            
            if (payroll.Rows.Count > 0)
            {
                pdate_from = DateTime.Parse(payroll.Rows[0]["date_from"].ToString());
                pdate_to = DateTime.Parse(payroll.Rows[0]["date_to"].ToString());
               
                    try
                    {
                        
                        leave_code = get_leave_code(payroll.Rows[0]["date_from"].ToString(), payroll.Rows[0]["date_to"].ToString(), empid);
                        DataTable leaves = db.QueryBySQLCode("SELECT * FROM rssys.hr_leaves where lvcode = '" + leave_code + "'");
                        if (leaves.Rows.Count > 0)
                        {
                            DateTime date_from = DateTime.Parse(leaves.Rows[0]["leave_from"].ToString());
                            DateTime date_to = DateTime.Parse(leaves.Rows[0]["leave_to"].ToString());


                            foreach (DateTime lday in EachDay(date_from, date_to)) {
                                //12-5-2018
                                foreach (DateTime day in EachDay(pdate_from, pdate_to))
                                {
                                    //12-5-2018 ok,12-5-2018==2018-12-6
                                    //
                                    int res = DateTime.Compare(lday, day);
                                    if (res == 0)
                                    {
                                        if (leaves.Rows[0]["leave_pay"].ToString() == "YES")
                                        {
                                            total = total + gm.toNormalDoubleFormat(leaves.Rows[0]["leave_amount"].ToString());
                                            result = total.ToString("0.00");
                                            
                                        }
                                        /*else
                                        {
                                            Double drate;
                                            if (emp_payrate.Rows[0]["rate_type"].ToString() == "D")
                                            {
                                                drate = gm.toNormalDoubleFormat(emp_payrate.Rows[0]["pay_rate"].ToString());
                                            }
                                            else
                                            {
                                                drate = (gm.toNormalDoubleFormat(emp_payrate.Rows[0]["pay_rate"].ToString()) * 12) / 314;
                                            }

                                            total = total + drate;
                                            result = total.ToString("0.00");

                                        }*/
                                        break;
                                    }
                                    //
                                } 
                                

                                // int res = DateTime.Compare(datetime_to, datetime_out);

                                // if (res < 0)
                                // {
                                //TimeSpan diff = datetime_out.Subtract(datetime_to);
                                //   MessageBox.Show("Out Time : " + datetime_to + " Time Out : " + datetime_out + " Overtime : " + diff);

                                // }
                            
                            }
                        }
                    }
                    catch
                    {

                    }                
            }
            return result;
        
        }

        private String get_leaves_type(String empid, String ppid)
        {

            String result = "none";
            Double total = 0.00;
            String leave_code = "";
            DateTime pdate_from, pdate_to;
            String datein;
            //get the empid,get the leaves entr 
            DataTable emp_payrate = db.QueryBySQLCode("SELECT pay_rate,rate_type FROM rssys.hr_employee WHERE empid = '" + empid + "'");
            DataTable payroll = db.QueryBySQLCode("SELECT date_from,date_to FROM rssys.hr_payrollpariod WHERE pay_code = '" + ppid + "'");
            if (payroll.Rows.Count > 0)
            {
                pdate_from = DateTime.Parse(payroll.Rows[0]["date_from"].ToString());
                pdate_to = DateTime.Parse(payroll.Rows[0]["date_to"].ToString());

                try
                {

                    leave_code = get_leave_code(payroll.Rows[0]["date_from"].ToString(), payroll.Rows[0]["date_to"].ToString(), empid);
                    DataTable leaves = db.QueryBySQLCode("SELECT * FROM rssys.hr_leaves where lvcode = '" + leave_code + "'");
                    if (leaves.Rows.Count > 0)
                    {

                        result = leaves.Rows[0]["leave_type"].ToString();


                   }
                }
                catch
                {

                }


            }



            return result;




        }
        public Boolean checkLeaveIfWithPay(String empid,String datefrom,String dateto) {
            String lcode = get_leave_code(datefrom, dateto, empid);
             DataTable leaves = db.QueryBySQLCode("SELECT * FROM rssys.hr_leaves where lvcode = '" + lcode + "'");
             if (leaves.Rows.Count > 0)
             {
                  if(leaves.Rows[0]["leave_pay"].ToString() == "YES"){
                      return true;
                  }
             }
            return false;
        }

        public Boolean checkLeave(String empid, String datefrom, String dateto)
        {
            String lcode = get_leave_code(datefrom, dateto, empid);
            DataTable leaves = db.QueryBySQLCode("SELECT * FROM rssys.hr_leaves where lvcode = '" + lcode + "'");
            if (leaves.Rows.Count > 0)
            {
                if (leaves.Rows[0]["leave_pay"].ToString() == "YES")
                {
                    return true;
                }
            }
            return false;
        }
        public String get_pay_rate(String empid)
        {
            DataTable emp_payrate = db.QueryBySQLCode("SELECT pay_rate,rate_type FROM rssys.hr_employee WHERE empid = '" + empid + "'");
            return emp_payrate.Rows[0]["pay_rate"].ToString();

        }

        private String get_leaves_days(String empid, String ppid)
        {

            String result = "0";
            Double total = 0.00;
            String leave_code = "";
            DateTime pdate_from, pdate_to;
            String datein;
            //get the empid,get the leaves entr 
            DataTable emp_payrate = db.QueryBySQLCode("SELECT pay_rate,rate_type FROM rssys.hr_employee WHERE empid = '" + empid + "'");
            DataTable payroll = db.QueryBySQLCode("SELECT date_from,date_to FROM rssys.hr_payrollpariod WHERE pay_code = '" + ppid + "'");
            if(payroll.Rows.Count>0){
                pdate_from = DateTime.Parse(payroll.Rows[0]["date_from"].ToString());
                pdate_to = DateTime.Parse(payroll.Rows[0]["date_to"].ToString());

                try
                {

                    leave_code = get_leave_code(payroll.Rows[0]["date_from"].ToString(), payroll.Rows[0]["date_to"].ToString(), empid);
                    DataTable leaves = db.QueryBySQLCode("SELECT * FROM rssys.hr_leaves where lvcode = '" + leave_code + "'");
                    if (leaves.Rows.Count > 0)
                    {
                        DateTime ldate_from = DateTime.Parse(leaves.Rows[0]["leave_from"].ToString());
                        DateTime ldate_to = DateTime.Parse(leaves.Rows[0]["leave_to"].ToString());


                        foreach (DateTime lday in EachDay(ldate_from, ldate_to))
                        {
                            //12-5-2018
                            foreach (DateTime day in EachDay(pdate_from, pdate_to))
                            {
                                //12-5-2018 ok,12-5-2018==2018-12-6
                                //
                                int res = DateTime.Compare(lday, day);
                                if (res == 0)
                                {
                                    total = total + 1;
                                    break;
                                }
                                //
                            }




                        }
                        result = total.ToString("0");
                    }
                }
                catch
                {

                }

            }
            
            

            return result;




        }


        private String get_leave_code(String datefrom, String dateto, String empid)
        {
            String lvcode = "";
            //"SELECT * FROM rssys.hr_leaves where (leave_from BETWEEN '"+datefrom+"' AND '"+dateto+"' or leave_to BETWEEN '"+datefrom+"' AND '"+dateto+"') and  empid = '"+empid+"'"
            DataTable leaves = db.QueryBySQLCode("SELECT * FROM rssys.hr_leaves where (leave_from BETWEEN '" + datefrom + "' AND '" + dateto + "' or leave_to BETWEEN '" + datefrom + "' AND '" + dateto + "') and  empid = '" + empid + "'");
            if (leaves.Rows.Count > 0)
            {
                lvcode = leaves.Rows[0]["lvcode"].ToString();
            }
            return lvcode;
        }

        private String get_payrol_period(String ppid)
        {
            String period = "";
            DataTable dt = null;
            try
            {
                dt = db.QueryBySQLCode("SELECT concat(to_char(date_from, 'mm/dd/yyyy'),' To ',to_char(date_to, 'mm/dd/yyyy')) as period FROM rssys.hr_payrollpariod WHERE pay_code='" + ppid + "'");
            }
            catch { }
            if(dt.Rows.Count > 0)
            {
                period = dt.Rows[0]["period"].ToString();
            }
            return period;
        }

        public Boolean checkDutyOnDayOff(String empid, String datein)
        {
            Boolean check = false;
            String query = "SELECT DISTINCT e.empid,work_date,(SELECT MAX(time_log) FROM rssys.hr_tito2 st WHERE work_date=t.work_date AND status='O' AND empid=t.empid) AS timeout FROM rssys.hr_tito2 t LEFT JOIN rssys.hr_employee e ON t.empid=e.empid WHERE t.empid = '" + empid + "' AND t.work_date BETWEEN '" + gm.toDateString(datein, "") + "' AND '" + gm.toDateString(datein, "") + "' ORDER BY work_date";
            DataTable logs = db.QueryBySQLCode(query);
            if (logs.Rows.Count > 0)
            {
                check = true;
            }
            return check;

        }

        public Double get_shift_day_off_pay(String empid,String ppid){
            DateTime pdate_from, pdate_to;
            String timein = "", timeout = "";
            String time_from = "", time_to = "";
            int twork = 0;
            TimeSpan total = new TimeSpan(0, 0, 0, 0, 0);
            String result = "";
            String overtime = "";
            String datein = "";
            String query = "";
            
            String str_dayoff1 = "", str_dayoff_2 = "";
            String[] d = new String[100];
            Double dtotal = 0;
            try
            {
                DataTable payperiod = db.QueryBySQLCode("SELECT date_from, date_to FROM rssys.hr_payrollpariod WHERE pay_code = '" + ppid + "'");
                DataTable sched = db.QueryBySQLCode("SELECT rssys.hr_emp_shift.esid,rssys.hr_shift_schedule.code,rssys.hr_employee.empid,concat(rssys.hr_employee.firstname,' ',rssys.hr_employee.lastname) as name, rssys.hr_shift_schedule.time_in, rssys.hr_shift_schedule.time_out,to_char(rssys.hr_emp_shift.date_from, 'yyyy-MM-dd') AS date_from,to_char(rssys.hr_emp_shift.date_to, 'yyyy-MM-dd') AS date_to FROM ((rssys.hr_emp_shift INNER JOIN rssys.hr_shift_schedule ON rssys.hr_emp_shift.shiftcode = rssys.hr_shift_schedule.code) INNER JOIN rssys.hr_employee ON rssys.hr_emp_shift.empid = rssys.hr_employee.empid) WHERE COALESCE(rssys.hr_emp_shift.cancel,rssys.hr_emp_shift.cancel,'')<>'Y' and rssys.hr_emp_shift.empid = '" + empid + "'");
                DataTable emp_pay_rate = db.QueryBySQLCode("SELECT rate_type,pay_rate FROM rssys.hr_employee where empid = '" + empid + "'");
                String date_from = payperiod.Rows[0]["date_from"].ToString();
                String date_to = payperiod.Rows[0]["date_to"].ToString();
                if (sched.Rows.Count > 0)
                {
                    for (int i = 0; i < sched.Rows.Count; i++)
                    {
                        String sdate_from = sched.Rows[i]["date_from"].ToString();
                        String sdate_to = sched.Rows[i]["date_to"].ToString();
                        //compare if the sched is less than in the pay period
                        DateTime pay_date_to = Convert.ToDateTime(date_to);
                        DateTime sched_date_to = Convert.ToDateTime(sdate_to);
                        DateTime pay_date_from = Convert.ToDateTime(date_from);
                        DateTime sched_date_from = Convert.ToDateTime(sdate_from);
                        int pdatetocheck = DateTime.Compare(sched_date_to, pay_date_to);
                        int pdatetofromcheck = DateTime.Compare(sched_date_from, pay_date_to);
                        int pdatefromcheck = DateTime.Compare(sched_date_from, pay_date_from);
                        int pdatefromtocheck = DateTime.Compare(sched_date_to, pay_date_from);

                        if (((pdatefromcheck == -1 || pdatefromcheck == 0) || (pdatefromtocheck == -1 || pdatefromtocheck == 0)) ||
                           ((pdatetocheck == -1 || pdatetocheck == 0) || (pdatetofromcheck == -1 || pdatetofromcheck == 0)))
                        {
                            if (pdatetocheck == 1)
                            {
                                sdate_to = date_to;
                            }

                            if (pdatefromcheck == -1)
                            {
                                sdate_from = date_from;
                            }

                            DateTime StartDate = DateTime.Parse(sdate_from);
                            DateTime EndDate = DateTime.Parse(sdate_to);
                            foreach (DateTime day in EachDay(StartDate, EndDate))
                            {
                                d[twork++] = day.ToString("yyyy-MM-dd");

                            }

                        }
                    }
                }
                pdate_from = DateTime.Parse(date_from);
                pdate_to = DateTime.Parse(date_to);




                foreach (DateTime day in EachDay(pdate_from, pdate_to))
                {
                    Boolean check = false;

                    for (int i = 0; i < twork; i++)
                    {
                        if (d[i] == day.ToString("yyyy-MM-dd"))
                        {
                            check = true;
                            break;
                        }
                    }

                    if (check == false)
                    {
                        if (checkDutyOnDayOff(empid,day.ToString("yyyy-MM-dd"))){
                            if (emp_pay_rate.Rows[0]["rate_type"].ToString() == "M")
                            {
                                Double daily_rate = (gm.toNormalDoubleFormat(emp_pay_rate.Rows[0]["pay_rate"].ToString()) * 12) / 314;
                                Double d_pay = daily_rate * (30 / 100.00);
                                dtotal = dtotal + d_pay;
                            }
                            else if (emp_pay_rate.Rows[0]["rate_type"].ToString() == "D")
                            {
                                Double d_pay = gm.toNormalDoubleFormat(emp_pay_rate.Rows[0]["pay_rate"].ToString());
                                dtotal = dtotal + d_pay;

                            }
                        }
                       
                    }

                }


            }
            catch { }

            return Math.Round(dtotal);
        }
        public Double get_shift_day_off(String empid,String ppid){
            DateTime pdate_from, pdate_to;
            String timein = "", timeout = "";
            String time_from = "", time_to = "";
            int twork = 0;
            TimeSpan total = new TimeSpan(0, 0, 0, 0, 0);
            String result = "";
            String overtime = "";
            String datein = "";
            String query = "";
            Double dototal = 0;
            String str_dayoff1 = "", str_dayoff_2 = "";
            String[] d = new String[100];
            
            try {
                DataTable payperiod = db.QueryBySQLCode("SELECT date_from, date_to FROM rssys.hr_payrollpariod WHERE pay_code = '" + ppid + "'");
                DataTable sched = db.QueryBySQLCode("SELECT rssys.hr_emp_shift.esid,rssys.hr_shift_schedule.code,rssys.hr_employee.empid,concat(rssys.hr_employee.firstname,' ',rssys.hr_employee.lastname) as name, rssys.hr_shift_schedule.time_in, rssys.hr_shift_schedule.time_out,to_char(rssys.hr_emp_shift.date_from, 'yyyy-MM-dd') AS date_from,to_char(rssys.hr_emp_shift.date_to, 'yyyy-MM-dd') AS date_to FROM ((rssys.hr_emp_shift INNER JOIN rssys.hr_shift_schedule ON rssys.hr_emp_shift.shiftcode = rssys.hr_shift_schedule.code) INNER JOIN rssys.hr_employee ON rssys.hr_emp_shift.empid = rssys.hr_employee.empid) WHERE COALESCE(rssys.hr_emp_shift.cancel,rssys.hr_emp_shift.cancel,'')<>'Y' and rssys.hr_emp_shift.empid = '" + empid + "'");
                String date_from = payperiod.Rows[0]["date_from"].ToString();
                String date_to = payperiod.Rows[0]["date_to"].ToString();
                if (sched.Rows.Count > 0)
                {
                    for (int i = 0; i < sched.Rows.Count; i++)
                    {
                        String sdate_from = sched.Rows[i]["date_from"].ToString();
                        String sdate_to = sched.Rows[i]["date_to"].ToString();
                        //compare if the sched is less than in the pay period
                        DateTime pay_date_to = Convert.ToDateTime(date_to);
                        DateTime sched_date_to = Convert.ToDateTime(sdate_to);
                        DateTime pay_date_from = Convert.ToDateTime(date_from);
                        DateTime sched_date_from = Convert.ToDateTime(sdate_from);
                        int pdatetocheck = DateTime.Compare(sched_date_to, pay_date_to);
                        int pdatetofromcheck = DateTime.Compare(sched_date_from, pay_date_to);
                        int pdatefromcheck = DateTime.Compare(sched_date_from, pay_date_from);
                        int pdatefromtocheck = DateTime.Compare(sched_date_to, pay_date_from);

                        if (((pdatefromcheck == -1 || pdatefromcheck == 0) || (pdatefromtocheck == -1 || pdatefromtocheck == 0)) ||
                           ((pdatetocheck == -1 || pdatetocheck == 0) || (pdatetofromcheck == -1 || pdatetofromcheck == 0)))
                        {
                            if (pdatetocheck == 1)
                            {
                                sdate_to = date_to;
                            }

                            if (pdatefromcheck == -1)
                            {
                                sdate_from = date_from;
                            }

                            DateTime StartDate = DateTime.Parse(sdate_from);
                            DateTime EndDate = DateTime.Parse(sdate_to);
                            foreach (DateTime day in EachDay(StartDate, EndDate))
                            {
                                d[twork++] = day.ToString("yyyy-MM-dd");

                            }

                        }
                    }
                    pdate_from = DateTime.Parse(date_from);
                    pdate_to = DateTime.Parse(date_to);
                    foreach (DateTime day in EachDay(pdate_from, pdate_to))
                    {
                        Boolean check = false;

                        for (int i = 0; i < twork; i++)
                        {
                            if (d[i] == day.ToString("yyyy-MM-dd"))
                            {
                                check = true;
                                break;
                            }
                        }

                        if (check == false)
                        {
                            if (checkDutyOnDayOff(empid, day.ToString("yyyy-MM-dd")))
                            {
                                overtime = "8:00:00";
                                dototal += gm.toNormalDoubleFormat(TimeSpan.Parse(overtime).TotalHours);
                            }

                        }

                    }
                }
                else {
                    
                }
                
                   


                     
                   
                
            }
            catch { }

            return Math.Round(dototal);
        }


        public Double get_fix_day_off_total(String empid,String ppid) {
            DateTime date_from, date_to;
            String timein = "", timeout = "";
            String time_from = "", time_to = "";
            TimeSpan total_late = new TimeSpan(0, 0, 0, 0, 0);
            String result = "";
            String overtime = "";
            String datein = "";
            String query = "";
            Double dototal = 0;
            String str_dayoff1 = "", str_dayoff_2 = "";
            DataTable dayoff = db.QueryBySQLCode("SELECT dayoff1,dayoff2 FROM rssys.hr_employee WHERE empid ='" + empid + "'");
            DataTable payperiod = db.QueryBySQLCode("SELECT date_from, date_to FROM rssys.hr_payrollpariod WHERE pay_code = '" + ppid + "'");
            DataTable dayoff1 = db.QueryBySQLCode("SELECT dayname FROM rssys.hr_days WHERE day = '" + dayoff.Rows[0]["dayoff1"].ToString() + "'");
            DataTable dayoff2 = db.QueryBySQLCode("SELECT dayname FROM rssys.hr_days WHERE day = '" + dayoff.Rows[0]["dayoff2"].ToString() + "'");
            if (dayoff1.Rows.Count > 0)
            {
                str_dayoff1 = dayoff1.Rows[0]["dayname"].ToString().ToUpper();

            }
            //error
            if (dayoff2.Rows.Count > 0)
            {
                str_dayoff_2 = dayoff2.Rows[0]["dayname"].ToString().ToUpper();
            }


            if (payperiod.Rows.Count > 0)
            {
                date_from = DateTime.Parse(payperiod.Rows[0]["date_from"].ToString());
                date_to = DateTime.Parse(payperiod.Rows[0]["date_to"].ToString());

                foreach (DateTime day in EachDay(date_from, date_to))
                {
                    if (day.DayOfWeek.ToString().ToUpper() == str_dayoff1 || day.DayOfWeek.ToString().ToUpper() == str_dayoff_2)
                    {
                        query = "SELECT DISTINCT work_date,(SELECT MIN(time_log) FROM rssys.hr_tito2 st WHERE work_date=t.work_date AND status='I' AND empid=t.empid) AS timein, (SELECT MAX(time_log) FROM rssys.hr_tito2 st WHERE work_date=t.work_date AND status='O' AND empid=t.empid) AS timeout FROM rssys.hr_tito2 t LEFT JOIN rssys.hr_employee e ON t.empid=e.empid WHERE t.work_date = '" + day.ToString("yyyy-MM-dd") + "' AND e.empid ='" + empid + "' ORDER BY work_date ";
                        //System.Diagnostics.Debug.Write(query);
                        DataTable logs = db.QueryBySQLCode(query);
                        if (logs != null && logs.Rows.Count > 0)
                        {
                            overtime = "8:00:00";
                            dototal += gm.toNormalDoubleFormat(TimeSpan.Parse(overtime).TotalHours);
                        }
                    }
                }
            }
            return Math.Round(dototal);
        }

        public Boolean checkiffixsched(String empid)
        {
            DataTable emp = db.QueryBySQLCode("SELECT fixed_sched FROM rssys.hr_employee where empid = '" + empid + "'");
            if (emp.Rows.Count > 0)
            {
                if (emp.Rows[0]["fixed_sched"].ToString() == "Y")
                {
                    return true;
                }
            }
            return false;
        }

        public Double get_dayoff_ot_total(String empid, String ppid)
        {
            Double result = 0.00;
            if (checkiffixsched(empid))
            {
                result = get_fix_day_off_total(empid,ppid);
            }
            else
            {
                result = get_shift_day_off(empid,ppid);

            }



            return result;
            
        }

        /*original code
            public Double get_dayoff_ot_total(String empid, String ppid)
        {
            DateTime date_from, date_to;
            String timein = "", timeout = "";
            String time_from = "", time_to = "";
            TimeSpan total_late = new TimeSpan(0, 0, 0, 0, 0);
            String result = "";
            String overtime = "";
            String datein = "";
            String query = "";
            Double total = 0;
            String str_dayoff1 = "", str_dayoff_2 = "";
            DataTable dayoff = db.QueryBySQLCode("SELECT dayoff1,dayoff2 FROM rssys.hr_employee WHERE empid ='" + empid + "'");
            DataTable payperiod = db.QueryBySQLCode("SELECT date_from, date_to FROM rssys.hr_payrollpariod WHERE pay_code = '" + ppid + "'");
            DataTable dayoff1 = db.QueryBySQLCode("SELECT dayname FROM rssys.hr_days WHERE day = '" + dayoff.Rows[0]["dayoff1"].ToString() + "'");
            DataTable dayoff2 = db.QueryBySQLCode("SELECT dayname FROM rssys.hr_days WHERE day = '" + dayoff.Rows[0]["dayoff2"].ToString() + "'");
            if (dayoff1.Rows.Count > 0) 
            {
                str_dayoff1 = dayoff1.Rows[0]["dayname"].ToString().ToUpper();
            
            }
            //error
            if (dayoff2.Rows.Count > 0)
            {
                str_dayoff_2 = dayoff2.Rows[0]["dayname"].ToString().ToUpper();           
            }
            

            if (payperiod.Rows.Count > 0)
            {
                date_from = DateTime.Parse(payperiod.Rows[0]["date_from"].ToString());
                date_to = DateTime.Parse(payperiod.Rows[0]["date_to"].ToString());
                
                foreach (DateTime day in EachDay(date_from, date_to))
                {
                    if (day.DayOfWeek.ToString().ToUpper() == str_dayoff1 || day.DayOfWeek.ToString().ToUpper() == str_dayoff_2)
                    {
                        query = "SELECT DISTINCT work_date,(SELECT MIN(time_log) FROM rssys.hr_tito2 st WHERE work_date=t.work_date AND status='I' AND empid=t.empid) AS timein, (SELECT MAX(time_log) FROM rssys.hr_tito2 st WHERE work_date=t.work_date AND status='O' AND empid=t.empid) AS timeout FROM rssys.hr_tito2 t LEFT JOIN rssys.hr_employee e ON t.empid=e.empid WHERE t.work_date = '" + day.ToString("yyyy-MM-dd") + "' AND e.empid ='" +empid +"' ORDER BY work_date ";
                        //System.Diagnostics.Debug.Write(query);
                        DataTable logs = db.QueryBySQLCode(query);
                        if (logs != null && logs.Rows.Count > 0)
                        {
                            for (int r = 0; r < logs.Rows.Count; r++)
                            {
                                if (logs.Rows[r]["timein"].ToString() != "")
                                {
                                    timein = logs.Rows[r]["timein"].ToString();
                                }
                                if (logs.Rows[r]["timeout"].ToString() != "")
                                {
                                    timeout = logs.Rows[r]["timeout"].ToString();
                                }

                                DateTime datetime_out = Convert.ToDateTime(DateTime.Now.ToString("M/d/yyyy") + " " + timeout);
                                DateTime datetime_in = Convert.ToDateTime(DateTime.Now.ToString("M/d/yyyy") + " " + timein);
                                int res = DateTime.Compare(datetime_out, datetime_in);

                                if (res > 0)
                                {
                                    TimeSpan diff = datetime_out.Subtract(datetime_in);
                                    //DateTime minusOneHour = Convert.ToDateTime(DateTime.Now.ToString("M/d/yyyy") + " " + "1:00");
                                    DateTime ttl = Convert.ToDateTime(DateTime.Now.ToString("M/d/yyyy") + " " + diff.ToString());
                                    TimeSpan t = ttl.Subtract( Convert.ToDateTime(DateTime.Now.ToString("M/d/yyyy") + " " + "1:00") );
                                    total_late = total_late + t;
                                }
                            }
                        }
                    }
                }
            }
            return gm.toNormalDoubleFormat(total_late.TotalHours);
        }
         */
        public Double get_with_tax(String empid) 
        {
            
            double pay_rate = 0;
            String bracket = "";
            double total_with_tax = 0.00;
            DataTable tax_bracket = db.QueryBySQLCode("SELECT pay_rate,tax_bracket FROM rssys.hr_employee WHERE empid = '" + empid + "' LIMIT 1");
            if(tax_bracket.Rows.Count > 0){
               
                pay_rate = gm.toNormalDoubleFormat(tax_bracket.Rows[0]["pay_rate"].ToString());
               
                bracket = tax_bracket.Rows[0]["tax_bracket"].ToString();
                DataTable wtax = db.QueryBySQLCode("SELECT * FROM rssys.hr_wtax where code = '"+ bracket + "'");
                if (wtax.Rows.Count > 0)
                {

                    String[] b = new String[] { "bracket1", "bracket2", "bracket3", "bracket4", "bracket5", "bracket6", "bracket7", "bracket8", "bracket9", "bracket10" };
                    String[] f = new String[] { "factor1", "factor2", "factor3", "factor4", "factor5", "factor6", "factor7", "factor8", "factor9", "factor10" };
                    String[] a = new String[] { "add_on1", "add_on2", "add_on3", "add_on4", "add_on5", "add_on6", "add_on7", "add_on8", "add_on9", "add_on10" };




                    int brk_temp = 0;
                    int brk_temp2 = 0;
                    int pay_temp = (int)pay_rate;

                    int f_temp = 0;
                    double p_temp = 0;
                    int ad_temp = 0;
                    for (int i = 0; i < b.Length - 1; i++)
                    {

                        //formula (salary- minumum of Less Compensation Range Column belong) * 30% + compesation Column Range Add on


                        brk_temp = (int)gm.toNormalDoubleFormat(wtax.Rows[0][b[i]].ToString());
                        ad_temp = (int)gm.toNormalDoubleFormat(wtax.Rows[0][a[i]].ToString());
                        if (brk_temp == 0)
                        {

                            f_temp = (int)gm.toNormalDoubleFormat(wtax.Rows[0][f[i]].ToString());
                            total_with_tax = (pay_temp - brk_temp2);

                            break;
                        }
                        else if (pay_temp <= brk_temp)
                        {

                            f_temp = (int)gm.toNormalDoubleFormat(wtax.Rows[0][f[i]].ToString());
                            p_temp = f_temp / 100.00;
                            if (f_temp == 0 && pay_rate <= brk_temp)
                            {
                                break;
                            }


                            total_with_tax = Math.Round(((pay_temp - brk_temp2) * p_temp) + ad_temp, 2);

                            break;

                        }
                        brk_temp2 = (int)gm.toNormalDoubleFormat(wtax.Rows[0][b[i]].ToString());


                    }
                }

               
                //conditon of no wtax rows
            }
            return total_with_tax;
        }

        public Double get_fix_day_off_pay(String empid,String ppid) {
            DateTime date_from, date_to;
            DataTable holiday = null;
            String overtime = "";
            String datein = "";
            String str_dayoff1 = "", str_dayoff_2 = "";
            Double dtotal = 0.00;
            DataTable payroll = db.QueryBySQLCode("SELECT date_from,date_to FROM rssys.hr_payrollpariod WHERE pay_code = '" + ppid + "'");
            DataTable emp_pay_rate = db.QueryBySQLCode("SELECT rate_type,pay_rate FROM rssys.hr_employee where empid = '" + empid + "'");
            DataTable dayoff = db.QueryBySQLCode("SELECT dayoff1,dayoff2 FROM rssys.hr_employee WHERE empid ='" + empid + "'");
            DataTable dayoff1 = db.QueryBySQLCode("SELECT dayname FROM rssys.hr_days WHERE day = '" + dayoff.Rows[0]["dayoff1"].ToString() + "'");
            DataTable dayoff2 = db.QueryBySQLCode("SELECT dayname FROM rssys.hr_days WHERE day = '" + dayoff.Rows[0]["dayoff2"].ToString() + "'");
            if (dayoff1.Rows.Count > 0)
            {
                str_dayoff1 = dayoff1.Rows[0]["dayname"].ToString().ToUpper();

            }
            //error
            if (dayoff2.Rows.Count > 0)
            {
                str_dayoff_2 = dayoff2.Rows[0]["dayname"].ToString().ToUpper();
            }
            if (payroll.Rows.Count > 0)
            {
                date_from = DateTime.Parse(payroll.Rows[0]["date_from"].ToString());
                date_to = DateTime.Parse(payroll.Rows[0]["date_to"].ToString());
                foreach (DateTime day in EachDay(date_from, date_to))
                {
                    try
                    {
                        datein = day.ToString("yyyy-MM-dd");
                        if (day.DayOfWeek.ToString().ToUpper() == str_dayoff1 || day.DayOfWeek.ToString().ToUpper() == str_dayoff_2)
                        {
                            if (checkDutyOnDayOff(empid, datein))
                            {
                                if (emp_pay_rate.Rows[0]["rate_type"].ToString() == "M")
                                {
                                    Double daily_rate = (gm.toNormalDoubleFormat(emp_pay_rate.Rows[0]["pay_rate"].ToString()) * 12) / 314;
                                    Double d_pay = daily_rate * (30 / 100.00);
                                    dtotal = dtotal + d_pay;
                                }
                                else if (emp_pay_rate.Rows[0]["rate_type"].ToString() == "D")
                                {
                                    Double d_pay = gm.toNormalDoubleFormat(emp_pay_rate.Rows[0]["pay_rate"].ToString());
                                    dtotal = dtotal + d_pay;

                                }


                            }

                        }
                    }
                    catch
                    {

                    }

                }
            }
            return Math.Round(dtotal, 2);
        }
        public Double get_day_off_pay(String empid,String ppid) {

            Double result = 0.00;
            if (checkiffixsched(empid))
            {
                result = get_fix_day_off_pay(empid, ppid);
            }
            else
            {
                result = get_shift_day_off_pay(empid, ppid);

            }



            return result;
        
        }
        public Double get_special_hol_pay(String empid,String ppid) {
            DateTime date_from, date_to;
            DataTable holiday = null;
            String datein = "";
            Double stotal = 0;
            DataTable legal_hol = db.QueryBySQLCode("SELECT date_from,date_to FROM rssys.hr_payrollpariod WHERE pay_code = '" + ppid + "'");
            DataTable emp_pay_rate = db.QueryBySQLCode("SELECT rate_type,pay_rate FROM rssys.hr_employee where empid = '" + empid + "'");
            if (legal_hol.Rows.Count > 0)
            {
                date_from = DateTime.Parse(legal_hol.Rows[0]["date_from"].ToString());
                date_to = DateTime.Parse(legal_hol.Rows[0]["date_to"].ToString());
                foreach (DateTime day in EachDay(date_from, date_to))
                {
                    try
                    {
                        datein = day.ToString("yyyy-MM-dd");
                        holiday = db.QueryBySQLCode("SELECT date_holiday,holiday_type FROM rssys.hr_holidays WHERE date_holiday = '" + datein + "' AND holiday_type = 'S'");
                        if (holiday.Rows.Count > 0)
                        {
                            if (checkDutyOnHol(empid, datein))
                            {
                                if (emp_pay_rate.Rows[0]["rate_type"].ToString() == "M")
                                {
                                    Double daily_rate = (gm.toNormalDoubleFormat(emp_pay_rate.Rows[0]["pay_rate"].ToString()) * 12) / 314;
                                   
                                    //Math.Round(daily_rate * (30 / 100.00))
                                    Double add_on = daily_rate * (30 / 100.00);
                                    stotal = stotal +add_on;
                                }
                                else if (emp_pay_rate.Rows[0]["rate_type"].ToString() == "D")
                                {
                                    Double daily_rate = gm.toNormalDoubleFormat(emp_pay_rate.Rows[0]["pay_rate"].ToString());
                                    Double add_on = daily_rate * (30 / 100.00);
                                    stotal = stotal + daily_rate;
                                }
                                else if (emp_pay_rate.Rows[0]["rate_type"].ToString() == "W")
                                {
                                    Double daily_rate = gm.toNormalDoubleFormat(emp_pay_rate.Rows[0]["pay_rate"].ToString());
                                    Double add_on = daily_rate * (30 / 100.00);
                                    stotal = stotal + add_on;
                                }
                            }
                            else
                            {
                                /*
                                if (emp_pay_rate.Rows[0]["rate_type"].ToString() == "M")
                                {
                                    Double daily_rate = (gm.toNormalDoubleFormat(emp_pay_rate.Rows[0]["pay_rate"].ToString()) * 12) / 314;
                                    stotal =  stotal + daily_rate;
                                }
                                 * */

                            }

                        }
                    }
                    catch
                    {

                    }

                }
            }
            return Math.Round(stotal,2);
        
        }

        public Double get_special_hol_hour(String empid, String ppid)
        {

            DateTime date_from, date_to;
            DataTable holiday = null;

            String datein = "";
            Double ltotal = 0.00;
            /*
            DataTable legal_hol = db.QueryBySQLCode("SELECT date_from,date_to FROM rssys.hr_payrollpariod WHERE pay_code = '" + ppid + "'");
            DataTable emp_pay_rate = db.QueryBySQLCode("SELECT rate_type,pay_rate FROM rssys.hr_employee where empid = '" + empid + "'");
            if (legal_hol.Rows.Count > 0)
            {
                date_from = DateTime.Parse(legal_hol.Rows[0]["date_from"].ToString());
                date_to = DateTime.Parse(legal_hol.Rows[0]["date_to"].ToString());
                foreach (DateTime day in EachDay(date_from, date_to))
                {
                    try
                    {
                        datein = day.ToString("yyyy-MM-dd");
                        holiday = db.QueryBySQLCode("SELECT date_holiday,holiday_type FROM rssys.hr_holidays WHERE date_holiday = '" + datein + "' AND holiday_type = 'S'");
                        if (holiday.Rows.Count > 0)
                        {
                            ltotal += gm.toNormalDoubleFormat(TimeSpan.Parse("8:00:00").TotalHours);
                        }
                    }
                    catch
                    {

                    }

                }
            }*/
            return Math.Round(ltotal, 2);
        }

        public Double get_legal_hol_hour(String empid, String ppid)
        {

            DateTime date_from, date_to;
            DataTable holiday = null;

            String datein = "";
            Double ltotal = 0.00;
            /*
            DataTable legal_hol = db.QueryBySQLCode("SELECT date_from,date_to FROM rssys.hr_payrollpariod WHERE pay_code = '" + ppid + "'");
            DataTable emp_pay_rate = db.QueryBySQLCode("SELECT rate_type,pay_rate FROM rssys.hr_employee where empid = '" + empid + "'");
            if (legal_hol.Rows.Count > 0)
            {
                date_from = DateTime.Parse(legal_hol.Rows[0]["date_from"].ToString());
                date_to = DateTime.Parse(legal_hol.Rows[0]["date_to"].ToString());
                foreach (DateTime day in EachDay(date_from, date_to))
                {
                    try
                    {
                        datein = day.ToString("yyyy-MM-dd");
                        holiday = db.QueryBySQLCode("SELECT date_holiday,holiday_type FROM rssys.hr_holidays WHERE date_holiday = '" + datein + "' AND holiday_type = 'L' AND COALESCE(cancel,cancel,'')<>'Y'");
                        if (holiday.Rows.Count > 0)
                        {
                                ltotal += gm.toNormalDoubleFormat(TimeSpan.Parse("8:00:00").TotalHours);        
                        }
                    }
                    catch
                    {

                    }

                }
            }*/
            return Math.Round(ltotal, 2);
        }
        public Double get_legal_hol_pay(String empid, String ppid)
        {

            DateTime date_from, date_to;
            DataTable holiday = null;
            String datein = "";
            Double ltotal = 0.00;
            DataTable legal_hol = db.QueryBySQLCode("SELECT date_from,date_to FROM rssys.hr_payrollpariod WHERE pay_code = '" + ppid + "'");
            DataTable emp_pay_rate = db.QueryBySQLCode("SELECT rate_type,pay_rate FROM rssys.hr_employee where empid = '"+empid+"'");
            if (legal_hol.Rows.Count > 0)
            {
                date_from = DateTime.Parse(legal_hol.Rows[0]["date_from"].ToString());
                date_to = DateTime.Parse(legal_hol.Rows[0]["date_to"].ToString());
                foreach (DateTime day in EachDay(date_from, date_to))
                {
                    try
                    {
                        datein = day.ToString("yyyy-MM-dd");
                        holiday = db.QueryBySQLCode("SELECT date_holiday,holiday_type FROM rssys.hr_holidays WHERE date_holiday = '" + datein + "' AND holiday_type = 'L' AND COALESCE(cancel,cancel,'')<>'Y'");
                        if (holiday.Rows.Count > 0)
                        {

                            if (checkDutyOnHol(empid, datein))
                            {
                                if (emp_pay_rate.Rows[0]["rate_type"].ToString() == "M")
                                {
                                    Double daily_rate = (gm.toNormalDoubleFormat(emp_pay_rate.Rows[0]["pay_rate"].ToString()) * 12) / 314;
                                    
                                    ltotal = ltotal + daily_rate;
                                }
                                else if (emp_pay_rate.Rows[0]["rate_type"].ToString() == "D")
                                {
                                    Double daily_rate = gm.toNormalDoubleFormat(emp_pay_rate.Rows[0]["pay_rate"].ToString());
                                    ltotal = ltotal + daily_rate;
                                }
                                else if (emp_pay_rate.Rows[0]["rate_type"].ToString() == "W")
                                {
                                    Double daily_rate = gm.toNormalDoubleFormat(emp_pay_rate.Rows[0]["pay_rate"].ToString());
                                    ltotal = ltotal + daily_rate;
                                }
                            }
                            else {
                                /*
                                if (emp_pay_rate.Rows[0]["rate_type"].ToString() == "M")
                                {
                                    Double daily_rate = (gm.toNormalDoubleFormat(emp_pay_rate.Rows[0]["pay_rate"].ToString()) * 12) / 314;
                                    ltotal = ltotal + daily_rate;
                                }
                                 * */

                            }
                           

                           
                        }
                    }
                    catch
                    {

                    }

                }
            }
            return Math.Round(ltotal,2);
        }


        public Double get_legal_hol_ot(String empid,String ppid)
        {
            
            DateTime date_from , date_to;
            DataTable holiday = null;
            String overtime = "";
            String datein = "";
            Double total = 0.00;
            /*
            DataTable legal_hol = db.QueryBySQLCode("SELECT date_from,date_to FROM rssys.hr_payrollpariod WHERE pay_code = '" + ppid + "'");
            if (legal_hol.Rows.Count > 0)
            {
                date_from = DateTime.Parse(legal_hol.Rows[0]["date_from"].ToString());
                date_to = DateTime.Parse(legal_hol.Rows[0]["date_to"].ToString());
                foreach (DateTime day in EachDay(date_from, date_to))
                {
                    try
                    {
                        datein = day.ToString("yyyy-MM-dd");
                        holiday = db.QueryBySQLCode("SELECT date_holiday,holiday_type FROM rssys.hr_holidays WHERE date_holiday = '" + datein + "' AND holiday_type = 'L' AND COALESCE(cancel,cancel,'')<>'Y'");
                        if (holiday.Rows.Count > 0 )
                        {
                            if (checkDutyOnHol(empid, datein)) {
                                overtime = "8:00:00";
                                total += gm.toNormalDoubleFormat(TimeSpan.Parse(overtime).TotalHours);
                            }
                            
                        }
                    }
                    catch
                    {

                    }
                    
                }
            }*/
            return Math.Round(total,2);
        }

        public Double get_legal_hol_ot_b(String empid, String ppid)
        {

            DateTime date_from, date_to;
            DataTable holiday = null;
            String overtime = "";
            String datein = "";
            Double total = 0.00;
            DataTable legal_hol = db.QueryBySQLCode("SELECT date_from,date_to FROM rssys.hr_payrollpariod WHERE pay_code = '" + ppid + "'");
            DataTable emp_pay_rate = db.QueryBySQLCode("SELECT rate_type,pay_rate FROM rssys.hr_employee where empid = '" + empid + "'");
            if (legal_hol.Rows.Count > 0)
            {
                date_from = DateTime.Parse(legal_hol.Rows[0]["date_from"].ToString());
                date_to = DateTime.Parse(legal_hol.Rows[0]["date_to"].ToString());
                foreach (DateTime day in EachDay(date_from, date_to))
                {
                    try
                    {
                        datein = day.ToString("yyyy-MM-dd");
                        holiday = db.QueryBySQLCode("SELECT date_holiday,holiday_type FROM rssys.hr_holidays WHERE date_holiday = '" + datein + "' AND holiday_type = 'L' AND COALESCE(cancel,cancel,'')<>'Y'");
                        if (holiday.Rows.Count > 0)
                        {
                            if (checkDutyOnHol(empid, datein)) {
                                Double daily_rate;
                                if (emp_pay_rate.Rows[0]["rate_type"].ToString() == "W")
                                {
                                    daily_rate = gm.toNormalDoubleFormat(emp_pay_rate.Rows[0]["pay_rate"].ToString());
                                }
                                else {
                                    daily_rate = (gm.toNormalDoubleFormat(emp_pay_rate.Rows[0]["pay_rate"].ToString()) * 12) / 314;
                                }
                                Double add_on = daily_rate * (30 / 100.00);
                                total = total + daily_rate;
                            }

                        }
                    }
                    catch
                    {

                    }

                }
            }
            return Math.Round(total, 2);
        }

        /*original code
         public Double get_legal_hol_ot(String empid,String ppid)
        {
            
            DateTime date_from , date_to;
            DataTable holiday = null;
            String overtime = "";
            String datein = "";
            Double total = 0;
            DataTable legal_hol = db.QueryBySQLCode("SELECT date_from,date_to FROM rssys.hr_payrollpariod WHERE pay_code = '" + ppid + "'");
            if (legal_hol.Rows.Count > 0)
            {
                date_from = DateTime.Parse(legal_hol.Rows[0]["date_from"].ToString());
                date_to = DateTime.Parse(legal_hol.Rows[0]["date_to"].ToString());
                foreach (DateTime day in EachDay(date_from, date_to))
                {
                    try
                    {
                        datein = day.ToString("yyyy-MM-dd");
                        holiday = db.QueryBySQLCode("SELECT date_holiday,holiday_type FROM rssys.hr_holidays WHERE date_holiday = '" + datein + "' AND holiday_type = 'L'");
                        if (holiday.Rows.Count > 0 )
                        {
                            overtime = compute_holiday_overtime(empid, datein);
                            total += gm.toNormalDoubleFormat(TimeSpan.Parse(overtime).TotalHours);
                        }
                    }
                    catch
                    {

                    }
                    
                }
            }
            return total;
        }
         */

        
        public Boolean checkDutyOnHol(String empid,String datein) {
            Boolean check = false;
            String query = "SELECT DISTINCT e.empid,work_date,(SELECT MAX(time_log) FROM rssys.hr_tito2 st WHERE work_date=t.work_date AND status='O' AND empid=t.empid) AS timeout FROM rssys.hr_tito2 t LEFT JOIN rssys.hr_employee e ON t.empid=e.empid WHERE t.empid = '" + empid + "' AND t.work_date BETWEEN '" + gm.toDateString(datein, "") + "' AND '" + gm.toDateString(datein, "") + "' ORDER BY work_date";
            DataTable logs = db.QueryBySQLCode(query);
            if(logs.Rows.Count>0){
                check =  true;
            }

            return check;
        
        }
        //
        private String compute_holiday_overtime(String empid, String datein)
        {
            String result = "00:00:00";

            String query = "";
            String timein = "", timeout = "";
            String time_from = "", time_to = "";
            TimeSpan total_legal_ot = new TimeSpan(0, 0, 0, 0, 0);

            DataTable sched = db.QueryBySQLCode("SELECT shift_sched_from,shift_sched_to FROM rssys.hr_employee WHERE empid = '" + empid + "'");
            if (sched.Rows.Count > 0)
            {

                time_from = sched.Rows[0]["shift_sched_from"].ToString();
                time_to = sched.Rows[0]["shift_sched_to"].ToString();


                query = "SELECT DISTINCT e.empid,work_date,(SELECT MAX(time_log) FROM rssys.hr_tito2 st WHERE work_date=t.work_date AND status='O' AND empid=t.empid) AS timeout FROM rssys.hr_tito2 t LEFT JOIN rssys.hr_employee e ON t.empid=e.empid WHERE t.empid = '" + empid + "' AND t.work_date BETWEEN '" + gm.toDateString(datein, "") + "' AND '" + gm.toDateString(datein, "") + "' ORDER BY work_date";

                DataTable logs = db.QueryBySQLCode(query);
                if (logs != null && logs.Rows.Count > 0)
                {
                    for (int r = 0; r < logs.Rows.Count; r++)
                    {
                        timeout = logs.Rows[r]["timeout"].ToString();

                        DateTime datetime_out = Convert.ToDateTime(DateTime.Now.ToString("M/d/yyyy") + " " + timeout);
                        DateTime datetime_to = Convert.ToDateTime(DateTime.Now.ToString("M/d/yyyy") + " " + time_from);
                        DateTime minusOneHour = Convert.ToDateTime(DateTime.Now.ToString("M/d/yyyy") + " " + "1:00");
                        TimeSpan diff = datetime_out.Subtract(datetime_to);
                        
                        DateTime total = Convert.ToDateTime(DateTime.Now.ToString("M/d/yyyy") + " " + diff.ToString());
                        TimeSpan t = total.Subtract(minusOneHour);
                        
                        total_legal_ot = total_legal_ot + t;
                        result = total_legal_ot.ToString();
                       // int res = DateTime.Compare(datetime_to, datetime_out);

                       // if (res < 0)
                       // {
                            //TimeSpan diff = datetime_out.Subtract(datetime_to);
                            //   MessageBox.Show("Out Time : " + datetime_to + " Time Out : " + datetime_out + " Overtime : " + diff);
                            
                       // }
                    }
                }

            }

            return result;
        }
        private String compute_overtime(String empid, String datein)
        {
            String result = "00:00:00";

            String query = "";
            String timein = "", timeout = "";
            String time_from = "", time_to = "";
            TimeSpan total_late = new TimeSpan(0, 0, 0, 0, 0);

            DataTable sched = db.QueryBySQLCode("SELECT shift_sched_from,shift_sched_to FROM rssys.hr_employee WHERE empid = '" + empid + "'");
            if (sched.Rows.Count > 0)
            {

                time_from = sched.Rows[0]["shift_sched_from"].ToString();
                time_to = sched.Rows[0]["shift_sched_to"].ToString();


                query = "SELECT DISTINCT e.empid,work_date,(SELECT MAX(time_log) FROM rssys.hr_tito2 st WHERE work_date=t.work_date AND status='O' AND empid=t.empid) AS timeout FROM rssys.hr_tito2 t LEFT JOIN rssys.hr_employee e ON t.empid=e.empid WHERE t.empid = '" + empid + "' AND t.work_date BETWEEN '" + gm.toDateString(datein, "") + "' AND '" + gm.toDateString(datein, "") + "' ORDER BY work_date";

                DataTable logs = db.QueryBySQLCode(query);
                if (logs != null && logs.Rows.Count > 0)
                {
                    for (int r = 0; r < logs.Rows.Count; r++)
                    {
                        timeout = logs.Rows[r]["timeout"].ToString();

                        DateTime datetime_out = Convert.ToDateTime(DateTime.Now.ToString("M/d/yyyy") + " " + timeout);
                        DateTime datetime_to = Convert.ToDateTime(DateTime.Now.ToString("M/d/yyyy") + " " + time_to);
                        int res = DateTime.Compare(datetime_to, datetime_out);

                        if (res < 0)
                        {
                            TimeSpan diff = datetime_out.Subtract(datetime_to);
                           
                            total_late = total_late + diff;
                            result = total_late.ToString();
                        }
                    }
                }

            }

            return result;
        }
        public Double get_special_hol_ot(String empid, String ppid)
        {
            DateTime date_from, date_to;
            DataTable holiday = null;
            String overtime = "";
            String datein = "";
            Double total = 0;
            /*
            DataTable legal_hol = db.QueryBySQLCode("SELECT date_from,date_to FROM rssys.hr_payrollpariod WHERE pay_code = '" + ppid + "'");
            if (legal_hol.Rows.Count > 0)
            {
                date_from = DateTime.Parse(legal_hol.Rows[0]["date_from"].ToString());
                date_to = DateTime.Parse(legal_hol.Rows[0]["date_to"].ToString());
                foreach (DateTime day in EachDay(date_from, date_to))
                {
                    try
                    {
                        datein = day.ToString("yyyy-MM-dd");
                        holiday = db.QueryBySQLCode("SELECT date_holiday,holiday_type FROM rssys.hr_holidays WHERE date_holiday = '" + datein + "' AND holiday_type = 'S' AND COALESCE(cancel,cancel,'')<>'Y'");
                        if (holiday.Rows.Count > 0) 
                        {
                            if(checkDutyOnHol(empid,datein)){
                                overtime = "8:00:00";
                                total += gm.toNormalDoubleFormat(TimeSpan.Parse(overtime).TotalHours);
                            }
                            
                        }
                    }
                    catch
                    {

                    }

                }
            }*/
            return total;
        }

        public Double get_special_hol_ot_b(String empid,String ppid) {

            DateTime date_from, date_to;
            DataTable holiday = null;
            String overtime = "";
            String datein = "";
            Double total = 0.00;
            DataTable legal_hol = db.QueryBySQLCode("SELECT date_from,date_to FROM rssys.hr_payrollpariod WHERE pay_code = '" + ppid + "'");
            DataTable emp_pay_rate = db.QueryBySQLCode("SELECT rate_type,pay_rate FROM rssys.hr_employee where empid = '" + empid + "'");
            if (legal_hol.Rows.Count > 0)
            {
                date_from = DateTime.Parse(legal_hol.Rows[0]["date_from"].ToString());
                date_to = DateTime.Parse(legal_hol.Rows[0]["date_to"].ToString());
                foreach (DateTime day in EachDay(date_from, date_to))
                {
                    try
                    {
                        datein = day.ToString("yyyy-MM-dd");
                        holiday = db.QueryBySQLCode("SELECT date_holiday,holiday_type FROM rssys.hr_holidays WHERE date_holiday = '" + datein + "' AND holiday_type = 'S' AND COALESCE(cancel,cancel,'')<>'Y'");
                        if (holiday.Rows.Count > 0)
                        {
                            if (checkDutyOnHol(empid, datein))
                            {
                                Double daily_rate;
                                if (emp_pay_rate.Rows[0]["rate_type"].ToString() == "W")
                                {
                                    daily_rate = gm.toNormalDoubleFormat(emp_pay_rate.Rows[0]["pay_rate"].ToString());
                                }
                                else {
                                    daily_rate = (gm.toNormalDoubleFormat(emp_pay_rate.Rows[0]["pay_rate"].ToString()) * 12) / 314;
                                }
                                Double add_on = daily_rate * (30 / 100.00);
                                total = total + add_on;
                            }

                        }
                    }
                    catch
                    {

                    }

                }
            }
            return Math.Round(total, 2);
        }
        /*original code
          public Double get_special_hol_ot(String empid, String ppid)
        {
            DateTime date_from, date_to;
            DataTable holiday = null;
            String overtime = "";
            String datein = "";
            Double total = 0;
            DataTable legal_hol = db.QueryBySQLCode("SELECT date_from,date_to FROM rssys.hr_payrollpariod WHERE pay_code = '" + ppid + "'");
            if (legal_hol.Rows.Count > 0)
            {
                date_from = DateTime.Parse(legal_hol.Rows[0]["date_from"].ToString());
                date_to = DateTime.Parse(legal_hol.Rows[0]["date_to"].ToString());
                foreach (DateTime day in EachDay(date_from, date_to))
                {
                    try
                    {
                        datein = day.ToString("yyyy-MM-dd");
                        holiday = db.QueryBySQLCode("SELECT date_holiday,holiday_type FROM rssys.hr_holidays WHERE date_holiday = '" + datein + "' AND holiday_type = 'S' ");
                        if (holiday.Rows.Count > 0) 
                        {
                            overtime = compute_holiday_overtime(empid, datein);
                            total += gm.toNormalDoubleFormat(TimeSpan.Parse(overtime).TotalHours);
                        }
                    }
                    catch
                    {

                    }

                }
            }
            return total;
        }
         
         */
        public DataTable get_holidays(String date_from,String date_to)
        {
            String query = "SELECT * FROM rssys.hr_holidays WHERE COALESCE(cancel,cancel,'')<>'Y' AND date_holiday BETWEEN '" + date_from + "' AND '" + date_to + "'";
            return db.QueryBySQLCode(query);
        }

        public IEnumerable<DateTime> EachDay(DateTime from, DateTime thru)
        {
            for (var day = from.Date; day.Date <= thru.Date; day = day.AddDays(1))
                yield return day;
        }
        private void p_GeneratePayroll_Load(object sender, EventArgs e)
        {
            disp_list_history();
        }

        
        private void disp_list_history()
        {
            dgv_list.Rows.Clear();
            try
            {

                DataTable dt = db.QueryBySQLCode("SELECT sh.*, concat(firstname,' ',lastname) AS employee FROM rssys.hr_dtr_sum_hdr sh LEFT JOIN rssys.hr_employee e ON e.empid=sh.empid  ORDER BY date_generated DESC, time_generated DESC");

                if (dt.Rows.Count > 0)
                {
                    for (int r = 0; dt.Rows.Count > r; r++)
                    {
                        int i = dgv_list.Rows.Add();
                        DataGridViewRow row = dgv_list.Rows[i];

                        row.Cells["dgvl_date"].Value = gm.toDateString(dt.Rows[r]["date_generated"].ToString(), "");
                        row.Cells["dgvl_time"].Value = dt.Rows[r]["time_generated"].ToString();
                        row.Cells["dgvl_payroll"].Value = gm.toDateString(dt.Rows[r]["date_from"].ToString(), "") + " TO " + gm.toDateString(dt.Rows[r]["date_to"].ToString(), "");

                        row.Cells["dgvl_userid"].Value = dt.Rows[r]["empid"].ToString();
                        //cbo_employee.SelectedValue = dt.Rows[r]["empid"].ToString();
                        row.Cells["dgvl_employee"].Value = dt.Rows[r]["employee"].ToString();

                    }
                }
            }
            catch { }

        }

        public String get_sss_deduction(String empid)
        {
            Double total = 0;
            DataTable sss = null;
            String code = "";
            Double ee = 0.00, ec = 0.00, er = 0.00;
            try
            {
                //error change sss_table to sss_bracket
                DataTable dt = db.QueryBySQLCode("SELECT sss_bracket FROM rssys.hr_employee WHERE empid = '" + empid + "' LIMIT 1");
                if (dt.Rows.Count > 0)
                {
                    code = dt.Rows[0]["sss_bracket"].ToString();
                    sss = db.QueryBySQLCode("SELECT * FROM rssys.hr_sss WHERE code = '" + code + "'");
                    er = gm.toNormalDoubleFormat(sss.Rows[0]["empshare_sc"].ToString());
                    ee = gm.toNormalDoubleFormat(sss.Rows[0]["empshare_ec"].ToString());
                    ec = gm.toNormalDoubleFormat(sss.Rows[0]["s_ec"].ToString());
                    //total = er + ee + ec;
                }
                


            }
            catch (Exception ex)
            {
                //MessageBox.Show("get_sss" + ex.Message);
            }
            return ee.ToString("0.00");
        }
        public String get_philhealth_deduction(String pay_rate,String empid,String ratetype)
         {
            Double result = 0.00;
            Double payrate;

            if (ratetype == "W")
            {
                payrate = (gm.toNormalDoubleFormat(pay_rate) * 6);
            }
            else {
                payrate = gm.toNormalDoubleFormat(pay_rate);
            }

            try {
                DataTable dtemp = db.QueryBySQLCode("SELECT pay_rate FROM rssys.hr_employee WHERE empid = '" + empid + "' LIMIT 1");
                if (dtemp.Rows.Count > 0)
                {
                    DataTable dtphil = db.QueryBySQLCode("SELECT * FROM rssys.hr_philhealth");
                    if (dtphil.Rows.Count > 0)
                    {
                        foreach (DataRow _phil in dtphil.Rows)
                        {
                            int bracket1 = (int)gm.toNormalDoubleFormat(_phil["bracket1"].ToString());
                            int b2 = (int)gm.toNormalDoubleFormat(_phil["bracket2"].ToString());
                            if (bracket1 != 0)
                            {
                                if(bracket1 == 1 && payrate <= b2){
                                    result = gm.toNormalDoubleFormat(_phil["emp_ee"].ToString());
                                }
                                    
                                result = (payrate * (2.75/100.00)) / 2;
                                
                            }
                        }
                        
                    }
                }
            
            
            }catch(Exception e){
                MessageBox.Show("get_philhealth" + e.Message);
            }



            /*result = (2.75 / 100) * payrate;
            result = result / 2.00;*/


            return result.ToString("0.00");
        }

        public String get_pagibig_deduction(String pay_rate,String empid)
        {
            Double result = 0;
            Double payrate = gm.toNormalDoubleFormat(pay_rate);

            try
            {
                DataTable dtemp = db.QueryBySQLCode("SELECT pay_rate FROM rssys.hr_employee WHERE empid = '" + empid + "' LIMIT 1");
                if (dtemp.Rows.Count > 0)
                {
                    DataTable dthdmf = db.QueryBySQLCode("SELECT * FROM rssys.hr_hdmf");
                    if (dthdmf.Rows.Count > 0)
                    {
                        foreach (DataRow _hdmf in dthdmf.Rows)
                        {
                            
                            int bracket1 = (int)gm.toNormalDoubleFormat(_hdmf["bracket1"].ToString());
                            if (bracket1 != 0)
                            {
                                if(bracket1 == 1500 && payrate <= bracket1){
                                    int emp_ee = (int)gm.toNormalDoubleFormat(_hdmf["emp_ee"].ToString());
                                    result = payrate * (emp_ee / 100.00);
                                    break;
                                }
                                if (payrate >= bracket1)
                                {
                                    if(payrate>=5000){
                                        result = 100;
                                        break;
                                    }
                                    int emp_ee = (int)gm.toNormalDoubleFormat(_hdmf["emp_ee"].ToString());
                                    result = payrate * (emp_ee / 100.00);
                                    break;
                               }
                            }
                          

                        }
                    }
                   
                }
            }
            catch (Exception e)
            {

                MessageBox.Show("get_pagibig " + e.Message);

            }

            /*if(payrate < 5000.00)
            {
                if(payrate <= 1500.00)
                {
                    result = (1 / 100) * payrate;
                }else if(payrate > 1500.00)
                {
                    result = (2 / 100) * payrate;
                }
            }if(payrate >= 5000.00)
            {
                result = 100;
            }*/
            return result.ToString("0.00");
        }

        public String get_other_earnings(String empid, String ppid)
        {
            Double result = 0.00;
            DataTable hee = db.QueryBySQLCode("SELECT amount FROM rssys.hr_earning_entry WHERE  payroll_period = '" + ppid + "' AND emp_no = '" + empid + "'");
            foreach (DataRow _hee in hee.Rows)
            {
                result += gm.toNormalDoubleFormat(_hee["amount"].ToString());
            }
            return result.ToString("0.00");
        }

        public String get_other_deductions(String empid, String ppid)
        {
            Double result = 0.00;
            DataTable hee = db.QueryBySQLCode("SELECT amount FROM rssys.hr_deduction_entry WHERE  payroll_period = '" + ppid + "' AND emp_no = '" + empid + "'");
            foreach (DataRow _hee in hee.Rows)
            {
                result += gm.toNormalDoubleFormat(_hee["amount"].ToString());
            }
            return result.ToString("0.00");
        }

    }
}
