using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using iTextSharp;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.fonts;
using iTextSharp.text.pdf.fonts.cmaps;
using System.Globalization;

namespace Human_Resource_Information_System
{
    public partial class rpt_print_dtr : Form
    {
        thisDatabase db = new thisDatabase();
        String fileloc_dtr = "\\\\RIGHTAPPS\\RightApps\\SACRED\\Reports\\payroll_pdf_report\\dtr\\";
        
        private GlobalClass gc;
        private GlobalMethod gm;
        public rpt_print_dtr()
        {
            gc = new GlobalClass();
            gm = new GlobalMethod();
            
            InitializeComponent();
        }

        private void rpt_print_dtr_Load(object sender, EventArgs e)
        {
            pic_loading.Visible = false;
            //fileloc_dtr = System.IO.Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.FullName;
            //MessageBox.Show(fileloc_dtr);
            fileloc_dtr = "\\\\RIGHTAPPS\\RightApps\\SACRED\\Reports\\payroll_pdf_report\\dtr\\";
            
            gc.load_employee(cbo_employee);
            gc.load_payroll_period(cbo_payollperiod);
            display_list();
        }
        private void display_list()
        {
            dgvl_dtrfiles.Invoke(new Action(() => {
                try { dgvl_dtrfiles.Rows.Clear(); }
                catch (Exception) { }
                int i = 0;
                String query = "SELECT * FROM rssys.hr_dtr_files ORDER BY date_created";

                try
                {
                    DataTable dt = db.QueryBySQLCode(query);

                    for (int r = 0; r < dt.Rows.Count; r++)
                    {
                        i = dgvl_dtrfiles.Rows.Add();
                        DataGridViewRow row = dgvl_dtrfiles.Rows[i];

                        row.Cells["dtr_id"].Value = dt.Rows[r]["dtr_id"].ToString();
                        row.Cells["filename"].Value = dt.Rows[r]["filename"].ToString();
                        row.Cells["date_created"].Value = dt.Rows[r]["date_created"].ToString();

                        i++;
                    }
                }
                catch { }
            }));
            
        }
        private void btn_submit_Click(object sender, EventArgs e)
        {
            
            if (cbo_payollperiod.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a payroll period.");
                cbo_payollperiod.DroppedDown = true;
                return;
            }
            btn_submit.Enabled = false;
            pic_loading.Visible = true;
            bgworker.RunWorkerAsync();
            
        }

        private String compute_shift_undertime(String empid, String date_from, String date_to,String time_in,String time_out)
        {

            String result = "00:00:00";

            String query = "";
            String timein = "", timeout = "";
            String sched_time_out = "", time_to = "";
            String sat_time_from = "";
            String sat_time_to = "";
            TimeSpan total_late = new TimeSpan(0, 0, 0, 0, 0);

            DateTime StartDate = DateTime.Parse(date_from);
            DateTime EndDate = DateTime.Parse(date_to);


            DataTable sched = db.QueryBySQLCode("SELECT rssys.hr_emp_shift.esid,rssys.hr_shift_schedule.code,rssys.hr_employee.empid,concat(rssys.hr_employee.firstname,' ',rssys.hr_employee.lastname) as name, rssys.hr_shift_schedule.time_in, rssys.hr_shift_schedule.time_out,to_char(rssys.hr_emp_shift.date_from, 'yyyy-MM-dd') AS date_from,to_char(rssys.hr_emp_shift.date_to, 'yyyy-MM-dd') AS date_to FROM ((rssys.hr_emp_shift INNER JOIN rssys.hr_shift_schedule ON rssys.hr_emp_shift.shiftcode = rssys.hr_shift_schedule.code) INNER JOIN rssys.hr_employee ON rssys.hr_emp_shift.empid = rssys.hr_employee.empid) WHERE COALESCE(rssys.hr_emp_shift.cancel,rssys.hr_emp_shift.cancel,'')<>'Y' and rssys.hr_emp_shift.empid = '" + empid + "'");

            if (sched.Rows.Count > 0)
            {
                
            }
            else
            {
                timeout = time_out;
                timein = time_in;
                DateTime datetime_out = Convert.ToDateTime(DateTime.Now.ToString("M/d/yyyy") + " " + timeout);
                DateTime datetimein = Convert.ToDateTime(DateTime.Now.ToString("M/d/yyyy") + " " + timein);
                DateTime requiretime = Convert.ToDateTime(DateTime.Now.ToString("M/d/yyyy") + " " + "9:00");

                TimeSpan diff = datetime_out.Subtract(datetimein);
                DateTime worktime = Convert.ToDateTime(DateTime.Now.ToString("M/d/yyyy") + " " + diff);
                int res = DateTime.Compare(requiretime, worktime);
                if (res == 1)
                {
                    TimeSpan utime = requiretime.Subtract(worktime);
                    total_late = total_late + utime;
                    result = total_late.ToString();

                }
            }


            


            return result;

        }

        private String compute_fix_undertime(String empid, String date_from, String date_to)
        {
            String result = "00:00:00";

            String query = "";
            String timein = "", timeout = "";
            String time_from = "", time_to = "";
            TimeSpan total_late = new TimeSpan(0, 0, 0, 0, 0);

            DataTable sched = db.QueryBySQLCode("SELECT shift_sched_from,shift_sched_to,shift_sched_sat_from,shift_sched_sat_to FROM rssys.hr_employee WHERE empid = '" + empid + "'");
            if (sched.Rows.Count > 0)
            {
                time_from = sched.Rows[0]["shift_sched_from"].ToString();
                time_to = sched.Rows[0]["shift_sched_to"].ToString();

                query = "SELECT DISTINCT e.empid,work_date,(SELECT MAX(time_log) FROM rssys.hr_tito2 st WHERE work_date=t.work_date AND status='O' AND empid=t.empid) AS timeout FROM rssys.hr_tito2 t LEFT JOIN rssys.hr_employee e ON t.empid=e.empid WHERE t.empid = '" + empid + "' AND t.work_date BETWEEN '" + gm.toDateString(date_from, "") + "' AND '" + gm.toDateString(date_to, "") + "' ORDER BY work_date";
                //System.Diagnostics.Debug.Write(query);
                DataTable logs = db.QueryBySQLCode(query);
                if (logs != null && logs.Rows.Count > 0)
                {
                    for (int r = 0; r < logs.Rows.Count; r++)
                    {
                        if (logs.Rows[r]["timeout"].ToString() != "")
                        {
                            timeout = logs.Rows[r]["timeout"].ToString();
                        }
                        else
                        {
                            timeout = time_from;
                        }

                        String work_date = logs.Rows[r]["work_date"].ToString();

                        DateTime date = Convert.ToDateTime(work_date);
                        String day_of_week = date.DayOfWeek.ToString();

                        if (day_of_week == "Saturday")
                        {
                            time_to = sched.Rows[0]["shift_sched_sat_to"].ToString();
                        }
                        else
                        {
                            time_to = sched.Rows[0]["shift_sched_to"].ToString();
                        }


                        DateTime datetime_out = Convert.ToDateTime(DateTime.Now.ToString("M/d/yyyy") + " " + timeout);
                        DateTime datetime_to = Convert.ToDateTime(DateTime.Now.ToString("M/d/yyyy") + " " + time_to);
                        int res = DateTime.Compare(datetime_to, datetime_out);

                        if (res > 0)
                        {
                            TimeSpan diff = datetime_to.Subtract(datetime_out);
                            //MessageBox.Show("Out Time : " + datetime_to + " Time Out : " + datetime_out + " Late : " + diff);
                            total_late = total_late + diff;
                            result = total_late.ToString();
                        }
                    }
                }
            }

            return result;
        }

        private String compute_undertime(String empid,String timein, String timeout,String datein,String date_from,String date_to)
        {
            String result = "00:00:00";
            /*
            if (checkiffixsched(empid))
            {
                result = compute_fix_undertime(empid, date_from, date_to);
            }
            else
            {
                result = compute_shift_undertime(empid, date_from, date_to,timein,timeout);

            }
             */

            return result;
        }
        private String compute_late(String empid,String timein,String datein,String date_from,String date_to)
        {
            String result = "00:00:00";
            if (checkiffixsched(empid))
            {
                result = compute_fix_late(empid, date_from, date_to);
            }
            else
            {
                result = compute_shift_late(empid, date_from, date_to);
            }
            return result;
        }
        public IEnumerable<DateTime> EachDay(DateTime from, DateTime thru)
        {
            for (var day = from.Date; day.Date <= thru.Date; day = day.AddDays(1))
                yield return day;
        }

        private String compute_shift_overtime(String empid, String date_from, String date_to)
        {
            String result = "00:00:00";

            String query = "";
            String timein = "", timeout = "";
            String sched_time_out = "", time_to = "";
            String sat_time_from = "";
            String sat_time_to = "";
            TimeSpan total_overtime = new TimeSpan(0, 0, 0, 0, 0);

            DateTime StartDate = DateTime.Parse(date_from);
            DateTime EndDate = DateTime.Parse(date_to);

            DataTable sched = db.QueryBySQLCode("SELECT rssys.hr_emp_shift.esid,rssys.hr_shift_schedule.code,rssys.hr_employee.empid,concat(rssys.hr_employee.firstname,' ',rssys.hr_employee.lastname) as name, rssys.hr_shift_schedule.time_in, rssys.hr_shift_schedule.time_out,to_char(rssys.hr_emp_shift.date_from, 'yyyy-MM-dd') AS date_from,to_char(rssys.hr_emp_shift.date_to, 'yyyy-MM-dd') AS date_to FROM ((rssys.hr_emp_shift INNER JOIN rssys.hr_shift_schedule ON rssys.hr_emp_shift.shiftcode = rssys.hr_shift_schedule.code) INNER JOIN rssys.hr_employee ON rssys.hr_emp_shift.empid = rssys.hr_employee.empid) WHERE COALESCE(rssys.hr_emp_shift.cancel,rssys.hr_emp_shift.cancel,'')<>'Y' and rssys.hr_emp_shift.empid = '" + empid + "'");

            for (int i = 0; i < sched.Rows.Count; i++)
            {
                String sdate_from = sched.Rows[i]["date_from"].ToString();
                String sdate_to = sched.Rows[i]["date_to"].ToString();
                //compare if the sched is less than in the pay period
                DateTime pay_date_to = Convert.ToDateTime(date_to);
                DateTime sched_date_to = Convert.ToDateTime(sdate_to);
                DateTime pay_date_from = Convert.ToDateTime(date_from);
                DateTime sched_date_from = Convert.ToDateTime(sdate_from);
                sched_time_out = sched.Rows[i]["time_out"].ToString();
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

                    query = "SELECT DISTINCT e.empid,work_date,(SELECT MIN(time_log) FROM rssys.hr_tito2 st WHERE work_date=t.work_date AND status='I' AND empid=t.empid) AS timein, (SELECT MAX(time_log) FROM rssys.hr_tito2 st WHERE work_date=t.work_date AND status='O' AND empid=t.empid) AS timeout FROM rssys.hr_tito2 t LEFT JOIN rssys.hr_employee e ON t.empid=e.empid WHERE t.work_date BETWEEN '" + gm.toDateString(sdate_from, "") + "' AND '" + gm.toDateString(sdate_to, "") + "' AND t.empid ='" + empid + "' ORDER BY work_date";
                    DataTable logs = db.QueryBySQLCode(query);


                    if (logs != null && logs.Rows.Count > 0)
                    {
                        for (int r = 0; r < logs.Rows.Count; r++)
                        {



                            timeout = logs.Rows[r]["timeout"].ToString();
                            DateTime datetime_out = Convert.ToDateTime(DateTime.Now.ToString("M/d/yyyy") + " " + timeout);
                            DateTime sched_datetime_out = Convert.ToDateTime(DateTime.Now.ToString("M/d/yyyy") + " " + sched_time_out);
                            DateTime ot_start = sched_datetime_out.AddHours(1);
                            DateTime ot_ss = Convert.ToDateTime(DateTime.Now.ToString("M/d/yyyy") + " " + ot_start.ToString("HH:mm:ss tt"));
                            int ot_ok = DateTime.Compare(datetime_out, ot_start);
                            if (ot_ok > 0)
                            {
                                TimeSpan diff = datetime_out.Subtract(ot_ss);
                                TimeSpan t = countovertime(diff);

                                total_overtime = total_overtime + t;
                                result = total_overtime.ToString();

                            }


                        }
                    }
                }


            }

            return result;
        }

        private String compute_fix_overtime(String empid, String date_from, String date_to)
        {
            String result = "00:00:00";

            String query = "";
            String timein = "", timeout = "";
            String time_from = "", time_to = "", work_date = "";
            TimeSpan total_overtime = new TimeSpan(0, 0, 0, 0, 0);
            DataTable ot_time = db.QueryBySQLCode("SELECT time_start FROM rssys.hr_ot_start");
            DataTable sched = db.QueryBySQLCode("SELECT shift_sched_from,shift_sched_to,shift_sched_sat_from,shift_sched_sat_to FROM rssys.hr_employee WHERE empid = '" + empid + "'");
            if (sched.Rows.Count > 0)
            {

                time_from = sched.Rows[0]["shift_sched_from"].ToString();
                time_to = sched.Rows[0]["shift_sched_to"].ToString();


                query = "SELECT DISTINCT e.empid,work_date,(SELECT MAX(time_log) FROM rssys.hr_tito2 st WHERE work_date=t.work_date AND status='O' AND empid=t.empid) AS timeout FROM rssys.hr_tito2 t LEFT JOIN rssys.hr_employee e ON t.empid=e.empid WHERE t.empid = '" + empid + "' AND t.work_date BETWEEN '" + gm.toDateString(date_from, "") + "' AND '" + gm.toDateString(date_to, "") + "' ORDER BY work_date";




                DataTable logs = db.QueryBySQLCode(query);
                if (logs != null && logs.Rows.Count > 0)
                {
                    for (int r = 0; r < logs.Rows.Count; r++)
                    {

                        timeout = logs.Rows[r]["timeout"].ToString();

                        work_date = logs.Rows[r]["work_date"].ToString();

                        DateTime date = Convert.ToDateTime(work_date);
                        String day_of_week = date.DayOfWeek.ToString();

                        if (day_of_week == "Saturday")
                        {
                            time_to = sched.Rows[0]["shift_sched_sat_to"].ToString();
                        }
                        else
                        {
                            time_to = sched.Rows[0]["shift_sched_to"].ToString();
                        }

                        DateTime datetime_to = Convert.ToDateTime(DateTime.Now.ToString("M/d/yyyy") + " " + time_to);

                        DateTime ot_start = datetime_to.AddHours(1);

                        DateTime datetime_out = Convert.ToDateTime(DateTime.Now.ToString("M/d/yyyy") + " " + timeout);

                        DateTime ot_ss = Convert.ToDateTime(DateTime.Now.ToString("M/d/yyyy") + " " + ot_start.ToString("HH:mm:ss tt"));

                        int ot_ok = DateTime.Compare(datetime_out, ot_start);
                        if (ot_ok > 0)
                        {


                            TimeSpan diff = datetime_out.Subtract(ot_ss);
                            TimeSpan t = countovertime(diff);
                            //   MessageBox.Show("Out Time : " + datetime_to + " Time Out : " + datetime_out + " Overtime : " + diff);
                            total_overtime = total_overtime + t;
                            result = total_overtime.ToString();


                        }
                    }
                }

            }

            return result;
        }

        private TimeSpan countovertime(TimeSpan diff)
        {
            TimeSpan total = new TimeSpan(0, 0, 0, 0, 0);
            TimeSpan min = TimeSpan.FromMinutes(30);

            //01:30:00
            DateTime d = Convert.ToDateTime(DateTime.Now.ToString("M/d/yyyy") + " " + diff);
            DateTime temp = Convert.ToDateTime(DateTime.Now.ToString("M/d/yyyy") + " " + min);
            int compare = DateTime.Compare(d, temp);
            while (DateTime.Compare(d, temp) >= 0)
            {
                total = total + min;
                TimeSpan t = d.Subtract(temp);
                d = Convert.ToDateTime(DateTime.Now.ToString("M/d/yyyy") + " " + t);


            }


            return total;
        }


        private String compute_overtime(String empid, String timeout, String datein,String date_from,String date_to)
        {
            String result = "00:00:00";
            if (checkiffixsched(empid))
            {
                result = compute_fix_overtime(empid, date_from, date_to);
            }
            else
            {
                result = compute_shift_overtime(empid, date_from, date_to);
            }

            return result;
        }

        public string RandomString(int length)
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
       

        private DataTable get_date(String code)
        {
            DataTable dt = null;
            try
            {
                dt = db.QueryBySQLCode("SELECT date_from,date_to from rssys.hr_payrollpariod where pay_code='" + code + "'");
            }
            catch { }
            return dt;
        }

        private void groupBox5_Enter(object sender, EventArgs e)
        {

        }

        private void groupBox5_Enter_1(object sender, EventArgs e)
        {

        }

        private void btn_print_Click(object sender, EventArgs e)
        {
            int r = -1;
            String dtr_filename = "";
            //String sys_dir = "\\\\RIGHTAPPS\\RightApps\\Eastland\\payroll_reports\\dtr\\";
            String sys_dir = fileloc_dtr;


            try
            {
                if (dgvl_dtrfiles.Rows.Count > 1)
                {
                    r = dgvl_dtrfiles.CurrentRow.Index;
                    try
                    {
                        dtr_filename = dgvl_dtrfiles["filename", r].Value.ToString();
                        
                        try
                        {
                            System.Diagnostics.Process.Start("chrome.exe", sys_dir + dtr_filename);
                        }
                        catch(Exception ex)
                        {
                            System.Diagnostics.Process.Start("chrome.exe", sys_dir + dtr_filename);
                        }
                        catch
                        {
                            System.Diagnostics.Process.Start("iexplore.exe", sys_dir + dtr_filename);
                        }
                        
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Select a filename");
                    }
                }
                else
                {
                    MessageBox.Show("DTR files is empty.");
                }
            }
            catch (Exception ex)
            {

            }

        }

        private void btn_deletefile_Click(object sender, EventArgs e)
        {
            int r = -1;
            String dtr_filename = "", dtr_sum_id = "";
            String sys_dir = fileloc_dtr;
            //String sys_dir = fileloc_dtr + "\\ViewController\\RPT\\TimeKeeping\\dtr_pdf\\";
            try
            {
                if (dgvl_dtrfiles.Rows.Count > 1)
                {
                    r = dgvl_dtrfiles.CurrentRow.Index;

                    try
                    {
                        dtr_filename = dgvl_dtrfiles["filename", r].Value.ToString();
                        dtr_sum_id = dgvl_dtrfiles["dtr_id", r].Value.ToString();
                        DialogResult result = MessageBox.Show("Are you sure you want to delete this file?", "Confirmation", MessageBoxButtons.YesNo);
                        if (result == DialogResult.Yes)
                        {
                            File.Delete(sys_dir + dtr_filename);
                            String query = "DELETE FROM rssys.hr_dtr_files WHERE dtr_id = '" + dtr_sum_id + "'";
                            db.QueryBySQLCode(query);
                            MessageBox.Show("File successfully deleted");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Failed to remove file. It may not exist");
                    }
                }
                else
                {
                    MessageBox.Show("Empty files.");
                }
            }
            catch (Exception ex)
            {

            }
            display_list();

        }

        private String compute_fix_late(String empid, String date_from, String date_to)
        {

            String result = "00:00:00";

            String query = "";
            String timein = "", timeout = "";
            String time_from = "", time_to = "";
            String sat_time_from = "";
            String sat_time_to = "";
            TimeSpan total_late = new TimeSpan(0, 0, 0, 0, 0);



            DataTable sched = db.QueryBySQLCode("SELECT shift_sched_from,shift_sched_to,shift_sched_sat_from,shift_sched_sat_to FROM rssys.hr_employee WHERE empid = '" + empid + "'");
            if (sched.Rows.Count > 0)
            {

                query = "SELECT DISTINCT e.empid,work_date,(SELECT MIN(time_log) FROM rssys.hr_tito2 st WHERE work_date=t.work_date AND status='I' AND empid=t.empid) AS timein, (SELECT MAX(time_log) FROM rssys.hr_tito2 st WHERE work_date=t.work_date AND status='O' AND empid=t.empid) AS timeout FROM rssys.hr_tito2 t LEFT JOIN rssys.hr_employee e ON t.empid=e.empid WHERE t.work_date BETWEEN '" + gm.toDateString(date_from, "") + "' AND '" + gm.toDateString(date_to, "") + "' AND t.empid ='" + empid + "' ORDER BY work_date";

                DataTable logs = db.QueryBySQLCode(query);

                if (logs != null && logs.Rows.Count > 0)
                {
                    for (int r = 0; r < logs.Rows.Count; r++)
                    {
                        timein = logs.Rows[r]["timein"].ToString();

                        DateTime date = Convert.ToDateTime(logs.Rows[r]["work_date"].ToString());
                        String day_of_week = date.DayOfWeek.ToString();

                        if (day_of_week == "Saturday")
                        {
                            time_from = sched.Rows[0]["shift_sched_sat_from"].ToString();
                        }
                        else
                        {
                            time_from = sched.Rows[0]["shift_sched_from"].ToString();
                        }

                        DateTime datetime_in = Convert.ToDateTime(DateTime.Now.ToString("M/d/yyyy") + " " + timein);

                        DateTime datetime_from = Convert.ToDateTime(DateTime.Now.ToString("M/d/yyyy") + " " + time_from);

                        int res = DateTime.Compare(datetime_from, datetime_in);

                        if (res < 0)
                        {
                            TimeSpan diff = datetime_in.Subtract(datetime_from);
                            total_late = total_late + diff;
                            result = total_late.ToString();
                        }
                    }
                }

            }
            return result;
        }

        private String compute_shift_late(String empid, String date_from, String date_to)
        {
            String result = "00:00:00";

            /*
              
             String query = "";
           String timein = "", timeout = "";
           String sched_time_in = "", time_to = "";
           String sat_time_from = "";
           String sat_time_to = "";
           TimeSpan total_late = new TimeSpan(0, 0, 0, 0, 0);

           DateTime StartDate = DateTime.Parse(date_from);
           DateTime EndDate = DateTime.Parse(date_to);

            DataTable sched = db.QueryBySQLCode("SELECT rssys.hr_emp_shift.esid,rssys.hr_shift_schedule.code,rssys.hr_employee.empid,concat(rssys.hr_employee.firstname,' ',rssys.hr_employee.lastname) as name, rssys.hr_shift_schedule.time_in, rssys.hr_shift_schedule.time_out,to_char(rssys.hr_emp_shift.date_from, 'yyyy-MM-dd') AS date_from,to_char(rssys.hr_emp_shift.date_to, 'yyyy-MM-dd') AS date_to FROM ((rssys.hr_emp_shift INNER JOIN rssys.hr_shift_schedule ON rssys.hr_emp_shift.shiftcode = rssys.hr_shift_schedule.code) INNER JOIN rssys.hr_employee ON rssys.hr_emp_shift.empid = rssys.hr_employee.empid) WHERE COALESCE(rssys.hr_emp_shift.cancel,rssys.hr_emp_shift.cancel,'')<>'Y' and rssys.hr_emp_shift.empid = '" + empid + "'");

            for (int i = 0; i < sched.Rows.Count;i++)
            {
                String sdate_from = sched.Rows[i]["date_from"].ToString();
                String sdate_to = sched.Rows[i]["date_to"].ToString();
                //compare if the sched is less than in the pay period
                DateTime pay_date_to = Convert.ToDateTime(date_to);
                DateTime sched_date_to = Convert.ToDateTime(sdate_to);
                DateTime pay_date_from = Convert.ToDateTime(date_from);
                DateTime sched_date_from = Convert.ToDateTime(sdate_from);
                sched_time_in = sched.Rows[i]["time_in"].ToString();
                int pdatetocheck = DateTime.Compare(sched_date_to, pay_date_to);
                int pdatetofromcheck = DateTime.Compare(sched_date_from, pay_date_to);
                int pdatefromcheck = DateTime.Compare(sched_date_from, pay_date_from);
                int pdatefromtocheck = DateTime.Compare(sched_date_to,pay_date_from);

                if(((pdatefromcheck == -1 || pdatefromcheck == 0) || (pdatefromtocheck == -1 || pdatefromtocheck == 0)) || 
                    ((pdatetocheck == -1 || pdatetocheck == 0) || (pdatetofromcheck == -1 || pdatetofromcheck == 0))){
                    if (pdatetocheck == 1)
                    {
                        sdate_to = date_to;
                    }

                    if (pdatefromcheck == -1)
                    {
                        sdate_from = date_from;
                    }

                    query = "SELECT DISTINCT e.empid,work_date,(SELECT MIN(time_log) FROM rssys.hr_tito2 st WHERE work_date=t.work_date AND status='I' AND empid=t.empid) AS timein, (SELECT MAX(time_log) FROM rssys.hr_tito2 st WHERE work_date=t.work_date AND status='O' AND empid=t.empid) AS timeout FROM rssys.hr_tito2 t LEFT JOIN rssys.hr_employee e ON t.empid=e.empid WHERE t.work_date BETWEEN '" + gm.toDateString(sdate_from, "") + "' AND '" + gm.toDateString(sdate_to, "") + "' AND t.empid ='" + empid + "' ORDER BY work_date";
                    DataTable logs = db.QueryBySQLCode(query);


                    if (logs != null && logs.Rows.Count > 0)
                    {
                        for (int r = 0; r < logs.Rows.Count; r++)
                        {
                            timein = logs.Rows[r]["timein"].ToString();
                            DateTime datetime_in = Convert.ToDateTime(DateTime.Now.ToString("M/d/yyyy") + " " + timein);
                            DateTime sched_datetime_in = Convert.ToDateTime(DateTime.Now.ToString("M/d/yyyy") + " " + sched_time_in);

                            int check = DateTime.Compare(sched_datetime_in, datetime_in);

                            if (check < 0)
                            {
                                TimeSpan diff = datetime_in.Subtract(sched_datetime_in);
                                total_late = total_late + diff;
                                result = total_late.ToString();
                            }
                        }
                    }
                }


            }    
             */

            return result;
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
        private void bgworker_DoWork(object sender, DoWorkEventArgs e)
        { 
            String query = "", empid = "", date_from = "", date_to = "", pay_code = "", table = "hr_dtr_files", filename = "", code = "", col = "", val = "", date_in = "";
            DataTable pay_period = null;
            

            query = "SELECT empid, firstname, lastname FROM rssys.hr_employee";
            cbo_employee.Invoke(new Action(() => {
                if (cbo_employee.SelectedIndex != -1)
                {
                    empid = cbo_employee.SelectedValue.ToString();
                    query += " WHERE empid='" + empid + "'";
                }
            }));

            query += " ORDER BY empid ASC";
            
            DataTable employees = db.QueryBySQLCode(query);
            cbo_payollperiod.Invoke(new Action(() => {
                pay_code = cbo_payollperiod.SelectedValue.ToString();
            }));
            
            pay_period = get_date(pay_code);

            if (pay_period.Rows.Count > 0)
            {
                date_from = gm.toDateString(pay_period.Rows[0]["date_from"].ToString(), "yyyy-MM-dd");
                date_to = gm.toDateString(pay_period.Rows[0]["date_to"].ToString(),"yyyy-MM-dd");
            }
            DateTime StartDate = DateTime.Parse(date_from);
            DateTime EndDate = DateTime.Parse(date_to);
            try
            {

                filename = RandomString(5) + "_" + DateTime.Now.ToString("yyyy-MM-dd");
                filename += ".pdf";

                System.IO.FileStream fs = new FileStream(fileloc_dtr + filename, FileMode.Create);
                
                //System.IO.FileStream fs = new FileStream(fileloc_dtr + "\\ViewController\\RPT\\TimeKeeping\\dtr_pdf\\" + filename, FileMode.Create);
                Document document = new Document(PageSize.LEGAL, 25, 25, 30, 30);

                PdfWriter.GetInstance(document, fs);
                document.Open();
                if (employees.Rows.Count > 0)
                {

                    for (int r = 0; r < employees.Rows.Count; r++)
                    {

                        Paragraph paragraph = new Paragraph();
                        paragraph.Alignment = Element.ALIGN_CENTER;
                        paragraph.Font = FontFactory.GetFont("Arial", 12);
                        paragraph.SetLeading(1, 1);
                        paragraph.Add("DAILY TIME RECORD");
                        Phrase line_break = new Phrase("\n");
                        document.Add(paragraph);
                        document.Add(line_break);

                        empid = employees.Rows[r]["empid"].ToString();

                        Paragraph emp_name = new Paragraph();
                        emp_name.Alignment = Element.ALIGN_CENTER;
                        emp_name.Font = FontFactory.GetFont("Arial", 18);
                        emp_name.SetLeading(1, 1);
                        emp_name.Add(employees.Rows[r]["firstname"].ToString() + " " + employees.Rows[r]["lastname"].ToString());
                        document.Add(emp_name);

                        Paragraph horizontal_line = new Paragraph();
                        horizontal_line.Alignment = Element.ALIGN_CENTER;
                        horizontal_line.Font = FontFactory.GetFont("Arial", 10);
                        horizontal_line.SetLeading(1, 1);
                        horizontal_line.Add("--------------------------------------------------------------------------------------");
                        document.Add(horizontal_line);

                        Paragraph label_name = new Paragraph();
                        label_name.Alignment = Element.ALIGN_CENTER;
                        label_name.SetLeading(1, 1);
                        label_name.Font = FontFactory.GetFont("Arial", 8);
                        label_name.Add("Name");

                        document.Add(label_name);

                        

                        
                       
                        var boldFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12);
                        PdfPTable t = new PdfPTable(9);
                        float[] widths = new float[] { 10, 20, 20, 20, 20, 20, 20, 20, 20 };
                        t.WidthPercentage = 100;
                        t.SetWidths(widths);
                        t.AddCell(new PdfPCell(new Phrase(new Chunk("DATE",boldFont))) { Colspan = 2, Rowspan = 2, HorizontalAlignment = Element.ALIGN_CENTER});
                        t.AddCell(new PdfPCell(new Phrase(new Chunk("AM", boldFont))) { Colspan = 2, HorizontalAlignment = Element.ALIGN_CENTER });
                        t.AddCell(new PdfPCell(new Phrase(new Chunk("PM", boldFont))) { Colspan = 2, HorizontalAlignment = Element.ALIGN_CENTER });
                        t.AddCell(new PdfPCell(new Phrase(new Chunk("UT/OT", boldFont))) { Colspan = 3, HorizontalAlignment = Element.ALIGN_CENTER });



                        t.AddCell(new PdfPCell(new Phrase(new Chunk("IN", boldFont))) { HorizontalAlignment = Element.ALIGN_CENTER });
                        t.AddCell(new PdfPCell(new Phrase(new Chunk("OUT", boldFont))) { HorizontalAlignment = Element.ALIGN_CENTER });
                        t.AddCell(new PdfPCell(new Phrase(new Chunk("IN", boldFont))) { HorizontalAlignment = Element.ALIGN_CENTER });
                        t.AddCell(new PdfPCell(new Phrase(new Chunk("OUT", boldFont))) { HorizontalAlignment = Element.ALIGN_CENTER });
                        t.AddCell(new PdfPCell(new Phrase(new Chunk("LATE", boldFont))) { HorizontalAlignment = Element.ALIGN_CENTER });
                        t.AddCell(new PdfPCell(new Phrase(new Chunk("UT", boldFont))) { HorizontalAlignment = Element.ALIGN_CENTER });
                        t.AddCell(new PdfPCell(new Phrase(new Chunk("OT", boldFont))) { HorizontalAlignment = Element.ALIGN_CENTER });

                        

                        query = "SELECT DISTINCT CONCAT(lastname,' ',firstname) AS name, t.source, e.empid, to_char(work_date, 'yyyy-MM-dd') AS work_date, (SELECT MIN(time_log) FROM rssys.hr_tito2 st WHERE work_date=t.work_date AND status='I' AND empid=t.empid) AS timein, (SELECT MAX(time_log) FROM rssys.hr_tito2 st WHERE work_date=t.work_date AND status='O' AND empid=t.empid) AS timeout FROM rssys.hr_tito2 t LEFT JOIN rssys.hr_employee e ON t.empid=e.empid WHERE e.empid = '" + empid + "' AND t.work_date BETWEEN '" + date_from + "' AND '" + date_to + "' ORDER BY work_date";
                        

                        DataTable dt = db.QueryBySQLCode(query);

                        String date_name = "", am_in = "", am_out = "", pm_in = "", pm_out = "", log_date = "", late = "", ut = "", ot_total = "";
                        int index = 0;
                        
                        foreach (DateTime day in EachDay(StartDate, EndDate))
                        {
                            if (index != dt.Rows.Count)
                            {
                                if (day.ToShortDateString() == DateTime.Parse(dt.Rows[index]["work_date"].ToString()).ToShortDateString())
                                {
                                    log_date = DateTime.Parse(dt.Rows[index]["work_date"].ToString()).ToShortDateString();
                                    am_in = dt.Rows[index]["timein"].ToString();
                                    pm_out = dt.Rows[index]["timeout"].ToString();
                                    index++;
                                }
                            }
                            date_name = day.ToString("MMM d, yyyy ddd", CultureInfo.InvariantCulture);

                            if (day.ToShortDateString() != log_date)
                            {
                                am_in = "";
                                pm_out = "";
                            }

                            t.AddCell(new PdfPCell(new Phrase(date_name)) { Colspan = 2, HorizontalAlignment = Element.ALIGN_LEFT });
                            t.AddCell(new PdfPCell(new Phrase(gm.toDateString(am_in, "hh:mm tt"))) { HorizontalAlignment = Element.ALIGN_CENTER });
                            t.AddCell(new PdfPCell(new Phrase("")) { HorizontalAlignment = Element.ALIGN_CENTER });
                            t.AddCell(new PdfPCell(new Phrase("")) { HorizontalAlignment = Element.ALIGN_CENTER });
                            t.AddCell(new PdfPCell(new Phrase(gm.toDateString(pm_out, "hh:mm tt"))) { HorizontalAlignment = Element.ALIGN_CENTER });
                            if (am_in != "")
                            {
                                late = compute_late(empid, am_in, day.ToShortDateString(),date_from,date_to);
                            }
                            else { late = ""; }


                            t.AddCell(new PdfPCell(new Phrase(late)) { HorizontalAlignment = Element.ALIGN_CENTER });
                            if (pm_out != "")
                            {
                                ut = compute_undertime(empid,am_in,pm_out, day.ToShortDateString(), date_from, date_to);
                            }
                            else { ut = ""; }



                            t.AddCell(new PdfPCell(new Phrase(ut)) { HorizontalAlignment = Element.ALIGN_CENTER });

                            if (pm_out != "")
                            {
                                ot_total = compute_overtime(empid, pm_out, day.ToString("yyyy-MM-dd"),date_from,date_to);
                            }

                            t.AddCell(new PdfPCell(new Phrase(ot_total)) { HorizontalAlignment = Element.ALIGN_CENTER });
                            ut = "";
                            late = "";
                            am_in = "";
                            pm_out = "";
                            ot_total = "";
                        }

                        

                        document.Add(t);

                        Phrase line_break_1 = new Phrase("\n");
                        line_break_1.SetLeading(0.5f, 0.5f);
                        document.Add(line_break_1);



                        empid = employees.Rows[r]["empid"].ToString();

                        Paragraph label_name1 = new Paragraph();
                        label_name1.Alignment = Element.ALIGN_LEFT;
                        label_name1.SetLeading(1, 1);
                        label_name1.Font = FontFactory.GetFont("Arial", 8);
                        label_name1.Add("Prepared By:");
                        //document.Add(label_name1);
                      

                        Chunk chunk1 = new Chunk(db.get_colval("x08", "opr_name", "uid='" + GlobalClass.username + "'"));
                        chunk1.SetUnderline(2, -3);
                        //document.Add(new Phrase(chunk1));



                        
                        document.Add(line_break_1);
                        document.Add(line_break_1);
                        document.Add(line_break_1);
                          




                        empid = employees.Rows[r]["empid"].ToString();
                        
                        Paragraph label_name2 = new Paragraph();
                        label_name2.Alignment = Element.ALIGN_LEFT;
                        label_name2.SetLeading(1, 1);
                        label_name2.Font = FontFactory.GetFont("Arial", 8);
                        label_name2.Add("Received By:");

                        //document.Add(label_name2);

                        Chunk chunk2 = new Chunk(employees.Rows[r]["firstname"].ToString() + " " + employees.Rows[r]["lastname"].ToString());
                        chunk2.SetUnderline(2, -3);
                        //document.Add(new Phrase(chunk2));


                        PdfPTable t2 = new PdfPTable(2);
                        t2.WidthPercentage = 100;
                        
                        
                        t2.AddCell(new PdfPCell(new Phrase("Prepared By:")) { Border = 0,HorizontalAlignment = Element.ALIGN_LEFT });
                        t2.AddCell(new PdfPCell(new Phrase("Received By:")) { Border = 0, HorizontalAlignment = Element.ALIGN_RIGHT });
                        t2.AddCell(new PdfPCell(new Phrase(chunk1)) { Border = 0, HorizontalAlignment = Element.ALIGN_LEFT });
                        t2.AddCell(new PdfPCell(new Phrase(chunk2)) { Border = 0, HorizontalAlignment = Element.ALIGN_RIGHT });
                        document.Add(t2);
 
                       

                        document.NewPage();
                    }
                }


                document.Close();
                code = db.get_pk("dtr_id");
                col = "dtr_id,filename,date_created";
                val = "'" + code + "','" + filename + "','" + DateTime.Now.ToShortDateString() + "'";

                if (db.InsertOnTable(table, col, val))
                {
                    db.set_pkm99("dtr_id", db.get_nextincrementlimitchar(code, 8)); //changes from 'hr_empid'
                    MessageBox.Show("DTR PRINTED");
                }
                else
                {
                    MessageBox.Show("Failed on saving.");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Program Error. \n Please contact the software provider. \n " + ex.Message + "at Line : " +ex.StackTrace );
            }
            //bgworker.RunWorkerAsync();
            pic_loading.Invoke(new Action(() => {
                pic_loading.Visible = false;
            }));

            btn_submit.Invoke(new Action(() => {
                btn_submit.Enabled = true;
            }));
            display_list();
        }
    }
}
