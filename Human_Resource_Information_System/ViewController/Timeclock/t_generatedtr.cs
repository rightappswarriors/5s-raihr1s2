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

namespace Human_Resource_Information_System
{
    public partial class t_generatedtr : Form
    {
        private GlobalClass gc;
        private GlobalMethod gm;
        dbHRIS db = new dbHRIS();

        Boolean isUseCboEmp = false;
        public t_generatedtr()
        {
            InitializeComponent();
        }

        private void t_generatedtr_Load(object sender, EventArgs e)
        {
            gc = new GlobalClass();
            gm = new GlobalMethod();

            gc.load_employee(cbo_employee);
            gc.load_payroll_period(cbo_payroll_period);

            disp_list_history();
        }

        private void btn_generate_Click(object sender, EventArgs e)
        {
            if (cbo_payroll_period.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a payroll period.");
                cbo_payroll_period.DroppedDown = true;
                return;
            }
            
            bgWorker.RunWorkerAsync();
            btn_generate.Enabled = false;
           
        }

        private String compute_fix_late(String empid,String date_from,String date_to){
            
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


        private String compute_shift_late(String empid,String date_from,String date_to){
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
             * */


            return result;
        }
        private String compute_late(String empid, String date_from, String date_to)
        {
            String result = "00:00:00";
            if (checkiffixsched(empid))
            {
                result = compute_fix_late(empid, date_from, date_to);
            }
            else {
                result = compute_shift_late(empid,date_from,date_to);
            }
            return result;
        }

        /*original
          private String compute_late(String empid, String date_from, String date_to)
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

                        if(day_of_week == "Saturday")
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
         
         */

        public Boolean checkiffixsched(String empid) {
            DataTable emp = db.QueryBySQLCode("SELECT fixed_sched FROM rssys.hr_employee where empid = '"+empid+"'");
            if (emp.Rows.Count > 0) { 
             if(emp.Rows[0]["fixed_sched"].ToString() == "Y"){
                 return true;
             }
            }
            return false;
        }


        private String compute_shift_undertime(String empid,String date_from,String date_to) {

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

            if (sched.Rows.Count > 0){
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

                                int check = DateTime.Compare(sched_datetime_out, datetime_out);

                                int res = DateTime.Compare(sched_datetime_out, datetime_out);

                                if (res > 0)
                                {
                                    TimeSpan diff = sched_datetime_out.Subtract(datetime_out);

                                    total_late = total_late + diff;
                                    result = total_late.ToString();
                                }
                            }
                        }
                    }


                } 
            }
            else {
                //query = "SELECT DISTINCT e.empid,work_date,(SELECT MIN(time_log) FROM rssys.hr_tito2 st WHERE work_date=t.work_date AND status='I' AND empid=t.empid) AS timein, (SELECT MAX(time_log) FROM rssys.hr_tito2 st WHERE work_date=t.work_date AND status='O' AND empid=t.empid) AS timeout FROM rssys.hr_tito2 t LEFT JOIN rssys.hr_employee e ON t.empid=e.empid WHERE t.work_date BETWEEN '" + gm.toDateString(date_from, "") + "' AND '" + gm.toDateString(date_to, "") + "' AND t.empid ='" + empid + "' ORDER BY work_date";

                //DataTable logs = db.QueryBySQLCode(query);
                //if (logs != null && logs.Rows.Count > 0)
                //{
                //    for (int r = 0; r < logs.Rows.Count; r++)
                //    {
                //        timeout = logs.Rows[r]["timeout"].ToString();
                //        timein = logs.Rows[r]["timein"].ToString();
                //        DateTime datetime_out = Convert.ToDateTime(DateTime.Now.ToString("M/d/yyyy") + " " + timeout);
                //        DateTime datetimein = Convert.ToDateTime(DateTime.Now.ToString("M/d/yyyy") + " " + timein);
                //        DateTime requiretime = Convert.ToDateTime(DateTime.Now.ToString("M/d/yyyy") + " " + "9:00");

                //        TimeSpan diff = datetime_out.Subtract(datetimein);
                //        DateTime worktime = Convert.ToDateTime(DateTime.Now.ToString("M/d/yyyy") + " " + diff);
                //        int res = DateTime.Compare(requiretime, worktime);
                //        if (res == 1)
                //        {
                //            TimeSpan utime = requiretime.Subtract(worktime);
                //            total_late = total_late + utime;
                //            result = total_late.ToString();

                //        }

                //    }
                //}
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
        
        private String compute_undertime(String empid, String date_from,String date_to)
        {

            String result = "00:00:00";
            if (checkiffixsched(empid))
            {
                result = compute_fix_undertime(empid, date_from, date_to);
            }
            else {
                result = compute_shift_undertime(empid,date_from,date_to);

            }



            return result;

        }

        /* original
         private String compute_undertime(String empid, String date_from,String date_to)
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

         */
        /*
         private String compute_overtime(String empid, String date_from, String date_to)
        {
            String result = "00:00:00";

            String query = "";
            String timein = "", timeout = "";
            String time_from = "", time_to = "", work_date = "";
            TimeSpan total_overtime = new TimeSpan(0, 0, 0, 0, 0);
            DataTable ot_time = db.QueryBySQLCode("SELECT time_start FROM rssys.hr_ot_start");
            DataTable sched = db.QueryBySQLCode("SELECT shift_sched_from,shift_sched_to,shift_sched_sat_from,shift_sche d_sat_to FROM rssys.hr_employee WHERE empid = '" + empid + "'");
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
                        if(ot_ok > 0)
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
         
         */

        private String compute_fix_overtime(String empid,String date_from,String date_to)
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

        private String compute_shift_overtime(String empid,String date_from,String date_to){
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
        private String compute_overtime(String empid, String date_from, String date_to)
        {
            String result = "00:00:00";
            if (checkiffixsched(empid))
            {
                result = compute_fix_overtime(empid, date_from, date_to);
            }
            else {
                result = compute_shift_overtime(empid, date_from, date_to);
            }

            return result;
     
        }
        private TimeSpan countovertime(TimeSpan diff){
            TimeSpan total = new TimeSpan(0, 0, 0, 0, 0);
             TimeSpan min =TimeSpan.FromMinutes(30);
            
             //01:30:00
             DateTime d = Convert.ToDateTime(DateTime.Now.ToString("M/d/yyyy") + " " + diff);
             DateTime temp = Convert.ToDateTime(DateTime.Now.ToString("M/d/yyyy")+ " "+ min);
             int compare = DateTime.Compare(d, temp);
             while (DateTime.Compare(d, temp)>=0)
             {
                 total = total + min;
                 TimeSpan t = d.Subtract(temp);
                 d = Convert.ToDateTime(DateTime.Now.ToString("M/d/yyyy") + " " + t);
                 

             }
             

             return total;
        }
        private String compute_daysworked(String empid, String date_from, String date_to)
        {
            String result = "0";
            DataTable sched = db.QueryBySQLCode("SELECT shift_sched_from,shift_sched_to FROM rssys.hr_employee WHERE empid = '" + empid + "'");
            String query = "";
            
            if (sched.Rows.Count > 0) {

                String time_from = gm.toDateString(sched.Rows[0]["shift_sched_from"].ToString(), "yyyy-MM-dd");
                String time_to = gm.toDateString(sched.Rows[0]["shift_sched_to"].ToString(), "yyyy-MM-dd");
                

                int count = 0;
                query = "SELECT DISTINCT e.empid,work_date,(SELECT MIN(time_log) FROM rssys.hr_tito2 st WHERE work_date=t.work_date AND status='I' AND empid=t.empid) AS timein, (SELECT MAX(time_log) FROM rssys.hr_tito2 st WHERE work_date=t.work_date AND status='O' AND empid=t.empid) AS timeout FROM rssys.hr_tito2 t LEFT JOIN rssys.hr_employee e ON t.empid=e.empid WHERE t.work_date BETWEEN '" + gm.toDateString(date_from, "") + "' AND '" + gm.toDateString(date_to, "") + "' AND t.empid ='" + empid + "' ORDER BY work_date";

                DataTable logs = db.QueryBySQLCode(query);

                try
                {
                    if (logs != null && logs.Rows.Count > 0)
                    {
                        result = logs.Rows.Count.ToString();
                    }
                    else
                    {
                        result = "0";
                    }
                }
                catch { result = "0"; }
            
            }
            return result;
        }


        private String get_total_day(String datefrom,String dateto){
           
            int total = 0;
            DateTime StartDate = DateTime.Parse(datefrom);
            DateTime EndDate = DateTime.Parse(dateto);
            foreach (DateTime day in EachDay(StartDate, EndDate))
            {
                total = total + 1;
            }


            return total.ToString();
        }

        public Boolean checkonDuty(String empid, String datein)
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

        public Boolean checkHoliday(String datein) {
            DataTable sholiday = db.QueryBySQLCode("SELECT date_holiday,holiday_type FROM rssys.hr_holidays WHERE date_holiday = '" + datein + "' AND holiday_type = 'S'");
            if(sholiday.Rows.Count>0){
                return true;
            }
            DataTable lholiday = db.QueryBySQLCode("SELECT date_holiday,holiday_type FROM rssys.hr_holidays WHERE date_holiday = '" + datein + "' AND holiday_type = 'L'");
            if(lholiday.Rows.Count>0){
                return true;
            }
            return false;
        
        }

        
        private String compute_shift_absent(String empid, String date_from, String date_to, String total_worked)
        {
            String result = "0";
            String ltotal = "";
            int total = 0;
            DateTime work_date;
            
           
            try {
                DataTable sched = db.QueryBySQLCode("SELECT rssys.hr_emp_shift.esid,rssys.hr_shift_schedule.code,rssys.hr_employee.empid,concat(rssys.hr_employee.firstname,' ',rssys.hr_employee.lastname) as name, rssys.hr_shift_schedule.time_in, rssys.hr_shift_schedule.time_out,to_char(rssys.hr_emp_shift.date_from, 'yyyy-MM-dd') AS date_from,to_char(rssys.hr_emp_shift.date_to, 'yyyy-MM-dd') AS date_to FROM ((rssys.hr_emp_shift INNER JOIN rssys.hr_shift_schedule ON rssys.hr_emp_shift.shiftcode = rssys.hr_shift_schedule.code) INNER JOIN rssys.hr_employee ON rssys.hr_emp_shift.empid = rssys.hr_employee.empid) WHERE COALESCE(rssys.hr_emp_shift.cancel,rssys.hr_emp_shift.cancel,'')<>'Y' and rssys.hr_emp_shift.empid = '" + empid + "'");
                if (sched.Rows.Count > 0) {
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

                                String datein = day.ToString("yyyy-MM-dd");
                                if (!checkHoliday(datein))
                                {
                                    total++;
                                }
                                else
                                {
                                    if (checkonDuty(empid, datein))
                                    {
                                        total++;
                                    }

                                }
                            }

                        }

                    }
                }
                else
                {
                    DateTime fStartDate = DateTime.Parse(date_from);
                    DateTime fEndDate = DateTime.Parse(date_to);
                    foreach (DateTime day in EachDay(fStartDate, fEndDate))
                    {
                        String datein = day.ToString("yyyy-MM-dd");
                        if (!checkHoliday(datein))
                        {
                            total++;
                        }
                        else
                        {
                            if (checkonDuty(empid, datein))
                            {
                                total++;
                            }

                        }
                    }

                    
                }

                ltotal = get_leaves_days(empid, date_from, date_to);
                total = total - Convert.ToInt32(ltotal) - 1;
                result = (Convert.ToInt32(total) - Convert.ToInt32(total_worked)).ToString();
                int checktotal = total - Convert.ToInt32(total_worked);
                if (checktotal < 0) { result = "0"; }
                
                
            }
            catch { }
            return result;
        }

        private String compute_fix_absent(String empid, String date_from, String date_to, String total_worked)
        {
            String result = "0";
            String ltotal = "";
            int total = 0;
            String query = "SELECT dayoff1,dayoff2 from rssys.hr_employee where empid = '" + empid + "'";
            DataTable dt = db.QueryBySQLCode(query);
            DateTime work_date;
            String dayoff1 = dt.Rows[0]["dayoff1"].ToString();
            String dayoff2 = dt.Rows[0]["dayoff2"].ToString();
            int dayno = 0;
            DateTime StartDate = DateTime.Parse(date_from);
            DateTime EndDate = DateTime.Parse(date_to);
            int off1 = 0, off2 = 0;

            try
            {
                off1 = Convert.ToInt32(dayoff1[0].ToString()) - 1;
            } catch { }
            //error off 2 
            //int off2 = Convert.ToInt32(dayoff2[0].ToString());

            try
            {
                if (dayoff2 != "")
                {
                    off2 = Convert.ToInt32(dayoff2[0].ToString()) - 1;
                }
            }
            catch { }

            //COMPUTER NUMBER OF WORKING DAYS
            /*foreach (DateTime day in EachDay(StartDate, EndDate))
            {

                //dayno = Convert.ToInt32(day.DayOfWeek.ToString("d")) +1;
                dayno = Convert.ToInt32(day.DayOfWeek.ToString("d"));
                if (dayno !=  off1 && dayno != off2)
                {
                    total++;
                }
            }*/
            foreach (DateTime day in EachDay(StartDate, EndDate))
            {

                //dayno = Convert.ToInt32(day.DayOfWeek.ToString("d")) +1;
                dayno = Convert.ToInt32(day.DayOfWeek.ToString("d"));
                String datein = day.ToString("yyyy-MM-dd");

                /*if (dayno != off1 && dayno != off2)
                {
                    total++;
                }
                else {
                    //if dayoff check if on duty
                    if (checkonDuty(empid, datein))
                    {
                        total++;
                    }
                }
                if (!checkHoliday(datein))
                {
                    if (checkonDuty(empid, datein))
                    {
                        total++;
                    }
                }*/
                if (dayno != off1 && dayno != off2 && !checkHoliday(datein))
                {
                    total++;
                }
                else
                {
                    if (checkonDuty(empid, datein))
                    {
                        total++;
                    }
                }
            }

            ltotal = get_leaves_days(empid, date_from, date_to);
            total = total - Convert.ToInt32(ltotal);
            try
            {
                result = (Convert.ToInt32(total) - Convert.ToInt32(total_worked)).ToString();
                int checktotal = total - Convert.ToInt32(total_worked);
                if(checktotal<0){result = "0";}
                // MessageBox.Show("Total working days :" + total + " Total worked days : " + total_worked + "Result is : " + result);
            }
            catch (Exception ex)
            {
                result = "0";
            }

            return result;
        }
        private String compute_absent(String empid, String date_from, String date_to, String total_worked)
        {
            String result = "00:00:00";
            if (checkiffixsched(empid))
            {
                result = compute_fix_absent(empid, date_from, date_to,total_worked);
            }
            else
            {
                result = compute_shift_absent(empid, date_from, date_to,total_worked);
            }
            return result;
        }


        private String get_leaves_days(String empid, String date_from,String date_to)
        {

            String result = "0";
            Double total = 0.00;
            String leave_code = "";
            DateTime pdate_from, pdate_to;
            String datein;
            //get the empid,get the leaves entr 
            DataTable emp_payrate = db.QueryBySQLCode("SELECT pay_rate,rate_type FROM rssys.hr_employee WHERE empid = '" + empid + "'");
           
                pdate_from = DateTime.Parse(date_from);
                pdate_to = DateTime.Parse(date_to);

                try
                {

                    leave_code = get_leave_code(date_from, date_to, empid);
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

            return result;




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

        private String get_leave_code(String datefrom, String dateto, String empid)
        {
            String lvcode = "";
            //"SELECT * FROM rssys.hr_leaves where (leave_from BETWEEN '"+datefrom+"' AND '"+dateto+"' or leave_to BETWEEN '"+datefrom+"' AND '"+dateto+"') and  empid = '"+empid+"'"
            //"SELECT * FROM rssys.hr_leaves where empid = '"+empid+"' and leave_to BETWEEN '"+datefrom+"' AND '"+dateto+"'"
            DataTable leaves = db.QueryBySQLCode("SELECT * FROM rssys.hr_leaves where (leave_from BETWEEN '" + datefrom + "' AND '" + dateto + "' or leave_to BETWEEN '" + datefrom + "' AND '" + dateto + "') and  empid = '" + empid + "'");
            if (leaves.Rows.Count > 0)
            {
                lvcode = leaves.Rows[0]["lvcode"].ToString();
            }
            return lvcode;
        }

        public IEnumerable<DateTime> EachDay(DateTime from, DateTime thru)
        {
            for (var day = from.Date; day.Date <= thru.Date; day = day.AddDays(1))
                yield return day;
        }


        public Boolean save_summary(String code)
        {
            Boolean ok = false;
            String table = "hr_dtr_sum_employees", col = "", val = "", empid = "", summ_code = "", days_worked = "", absences = "", late = "", undertime = "", total_overtime = "", summary_code="";
            Boolean success = false;
            int selectedIndx = - 1;

            try
            {

                for (int r = dgv_list_logs.Rows.Count - 1; r >= 0; r--)
                {
                    Boolean use = false;
                    cbo_employee.Invoke(new Action(() =>
                    {
                        empid = dgv_list_logs["empid", r].Value.ToString();

                        if (cbo_employee.SelectedIndex == -1)
                        {
                            use = true;
                        }
                        else
                        {
                            if (empid != cbo_employee.SelectedValue)
                            {
                                use = true;
                                selectedIndx++;
                            }
                        }

                      //  if (use && selectedIndx < 1) 
                      //  {
                            days_worked = dgv_list_logs["days_worked", r].Value.ToString();
                            absences = dgv_list_logs["absent", r].Value.ToString();
                            late = dgv_list_logs["total_late", r].Value.ToString();
                            undertime = dgv_list_logs["undertime", r].Value.ToString();
                            total_overtime = dgv_list_logs["overtime", r].Value.ToString();

                            col = "empid,days_worked,absences,late,undertime,total_overtime,ppid";
                            val = "'" + empid + "','" + days_worked + "','" + absences + "','" + late + "','" + undertime + "','" + total_overtime + "','" + code + "'";

                            db.DeleteOnTable(table, "ppid='" + code + "' AND empid='" + empid + "' AND isgenerated=0");
                            if (!is_generated_sum(empid, code))
                            {
                                if (db.InsertOnTable(table, col, val))
                                {
                                    success = true;
                                    //db.set_pkm99("summ_code", db.get_nextincrementlimitchar(summ_code, 8));
                                }
                            }
                            else
                            {
                                MessageBox.Show("The payroll period for this employee no. " + empid + " is already generated to Payroll System. DTR can not be re-generated.");
                                success = false;
                            }
                       // }

                    }));

                }
            }
            catch (Exception er) { MessageBox.Show(er.Message); }
           

            return success;
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

        private void bgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            String empid = "", date_from = "2023-01-11", date_to = "2023-05-12", d_from = "", d_to = "", d_now = "", t_now = "";
            String WHERE = "", table = "", code = "", col = "", val = "", query = "", total_worked = "", pay_code = "";
            DataTable dt = null, pay_period = null;
            Boolean success = false;
            int j = 0, bar = 1, indx = -1;

            try { dgv_list_logs.Rows.Clear(); } catch (Exception) { }
            //try { dgv_list.Rows.Clear(); } catch { }

            cbo_payroll_period.Invoke(new Action(() =>
            {
                indx = cbo_employee.SelectedIndex;

                if (cbo_payroll_period.SelectedIndex != -1)
                {
                    pay_code = cbo_payroll_period.SelectedValue.ToString();
                    pay_period = get_date(pay_code);

                    if (pay_period.Rows.Count > 0)
                    {
                        date_from = gm.toDateString(pay_period.Rows[0]["date_from"].ToString(), "yyyy-MM-dd");
                        date_to = gm.toDateString(pay_period.Rows[0]["date_to"].ToString(), "yyyy-MM-dd");

                        WHERE += " AND e.gen_date BETWEEN '"+ date_from + "' AND '"+ date_to + "' ";
                    }
                }
            }));            
           /*
            query = @"SELECT empid, employeename AS name, SUM(work_hr) AS days_worked, 0 AS absent, '00:00:00' AS total_late, '00:00:00' AS undertime, '00:00:00' AS overtime
                FROM
                (
                    SELECT to_char(pp.gen_date,'MM/DD/YY DY') AS gen_date,
                        CASE WHEN dy.day=dayoff1 OR dy.day=dayoff2 THEN 'Day Off' ELSE '' END AS dayoff, e.empid ,
                        e.lastname||','||e.firstname||' '||e.mi AS employeename,  e.dept_name ||' - '||  e.position_name AS department, 

                        to_char(CASE WHEN trim(to_char(pp.gen_date,'day'))='saturday' THEN total_hr_sat ELSE  total_hr END,'hh24:mi') AS total_hr, 
                        to_char(wk.work_hr,'hh24:mi') AS work_hr_time, 
                        CASE WHEN wk.work_hr IS NULL THEN '0' ELSE round((extract(epoch from wk.work_hr::time)/3600)::numeric,2) END AS work_hr,
                        wk.work_date::date, wk.time_in AS timein, wk.break_out AS breakout, wk.break_in AS breakin, wk.time_out AS timeout, 

                        CASE WHEN COALESCE(wk.empid,'')<>'' OR (dy.day=dayoff1 OR dy.day=dayoff2) THEN 0 ELSE 1 END AS absent, 

                        CASE WHEN trim(to_char(pp.gen_date,'day'))='saturday' THEN 
		                        CASE 
			                        WHEN wk.time_in::time-e.sched_timein_sat::time>'00:00'::time 
                                        THEN round((extract(epoch from to_char(wk.time_in::time-e.sched_timein_sat::time,'hh24:mi')::time)/3600)::numeric,2)  
                                    ELSE '0' END        
		                        ELSE 
			                        CASE WHEN wk.time_in::time-e.sched_timein::time>'00:00'::time 
                                        THEN round((extract(epoch from to_char(wk.time_in::time-e.sched_timein::time,'hh24:mi')::time)/3600)::numeric,2)  
                                    ELSE '0' END 
		                        END AS late, 
		
                        CASE WHEN trim(to_char(pp.gen_date,'day'))='saturday' THEN 
		                        CASE 
	 		                        WHEN wk.time_out::time-e.sched_timeout_sat::time>'00:00'::time 
                                        THEN 
                                            round((extract(epoch from to_char(wk.time_out::time-e.sched_timeout_sat::time,'hh24:mi')::time)/3600)::numeric,2)
                                    ELSE '0' END      
		                        ELSE 
			                        CASE WHEN wk.time_out::time-e.sched_timeout::time>'00:00'::time 
                                        THEN round((extract(epoch from to_char(wk.time_out::time-e.sched_timeout::time,'hh24:mi')::time)/3600)::numeric,2)  
                                    ELSE '0' END 
		                        END AS undertime, 
		
                        CASE WHEN trim(to_char(pp.gen_date,'day'))='saturday' THEN 
		                        CASE 
			                        WHEN e.sched_timeout_sat::time-wk.time_out::time>'00:00'::time 
                                        THEN round((extract(epoch from to_char(e.sched_timeout_sat::time-wk.time_out::time,'hh24:mi')::time)/3600)::numeric,2)  
                                    ELSE '0' END       
	  	                        ELSE 
			                        CASE WHEN e.sched_timeout::time-wk.time_out::time>'00:00'::time 
                                        THEN round((extract(epoch from to_char(e.sched_timeout::time-wk.time_out::time,'hh24:mi')::time)/3600)::numeric,2)  
                                    ELSE '0' END 
		                        END AS overtime, 
		

                        lv.description AS leave_desc, lv.leave_type,  
                        CASE WHEN dy.day=dayoff1 OR dy.day=dayoff2 THEN 'Day Off' ELSE 
                        TO_CHAR(sched.time_in::TIME, 'hh12:mi AM') ||' to '|| TO_CHAR(sched.time_out::TIME, 'hh12:mi AM') END  AS mon 

                        FROM (SELECT generate_series('"+ date_from + "'::date, '"+ date_to + @"'::date, '1 day'::interval)::date AS gen_date ) pp 
                        JOIN rssys.view_emp_profile_payroll  e ON (e.date_hired<>'1900-01-01' AND e.date_hired<=pp.gen_date) 	AND ((e.empstatus!='9RS' AND e.empstatus!='9TM') OR (e.empstatus='9RS' AND e.date_resigned>=pp.gen_date) OR (e.empstatus='9TM' AND e.date_terminated>=pp.gen_date))
                        
                        LEFT JOIN rssys.view_emp_dtr wk ON wk.work_date=pp.gen_date AND wk.empid=e.empid
                        LEFT JOIN rssys.hr_days dy ON (dy.dayname=TRIM(to_char(pp.gen_date,'DAY'))) 
                        LEFT JOIN (SELECT lv.*, lt.description FROM rssys.hr_leaves lv LEFT JOIN rssys.hr_leave_type lt ON (lt.code=lv.leave_type) WHERE 1=1 ) lv 
                        ON (e.empid=lv.empid AND pp.gen_date BETWEEN lv.leave_from AND lv.leave_to)  
                        LEFT JOIN rssys.hr_emp_shift es ON es.empid=e.empid
                        LEFT JOIN rssys.hr_shift_schedule sched ON sched.code=es.shiftcode
                         WHERE 1=1   AND e.department BETWEEN '0001'  AND '004'     ORDER BY e.dept_name, e.position_name, e.lastname, pp.gen_date
						 
                    ) tbl GROUP BY empid, employeename;"; */

            cbo_employee.Invoke(new Action(() =>
            {
                if (cbo_employee.SelectedIndex != -1)
                {
                    empid = cbo_employee.SelectedValue.ToString();
                    //query += " WHERE empid='" + empid + "'";
                    WHERE += " AND empid = '" + empid + "'";
                }
            }));
            //query += " ORDER BY empid ASC";

            //dt = db.QueryBySQLCode(query);
            dt = db.get_listof_emp_dtr_summary(WHERE);

            if (dt!=null)
            {
                pbar.Invoke(new Action(() =>
                {
                    pbar.Maximum = dt.Rows.Count;
                }));
                for (int r = 0; r < dt.Rows.Count; r++)
                {

                    empid = dt.Rows[r]["empid"].ToString();
                    dgv_list_logs.Invoke(new Action(() =>
                    {
                        j = dgv_list_logs.Rows.Add();
                        DataGridViewRow row = dgv_list_logs.Rows[j];
                        row.Cells["empid"].Value = dt.Rows[r]["empid"].ToString();
                        row.Cells["name"].Value = dt.Rows[r]["name"].ToString();
                        row.Cells["days_worked"].Value = total_worked = dt.Rows[r]["days_worked"].ToString();
                        row.Cells["absent"].Value = dt.Rows[r]["absent"].ToString(); //work_hr
                        row.Cells["work_hr"].Value = dt.Rows[r]["work_hr"].ToString(); //

                        row.Cells["total_late"].Value = dt.Rows[r]["total_late"].ToString();
                        row.Cells["undertime"].Value = dt.Rows[r]["undertime"].ToString();
                        row.Cells["overtime"].Value = dt.Rows[r]["overtime"].ToString();
                        j++;
                        inc_pbar(bar, dt.Rows.Count);
                        bar++;
                    }));

                }

                DialogResult result = MessageBox.Show("Do you want to save generated " + (indx == -1 ? "all" : "") + " employee's DTR?", "Confirmation", MessageBoxButtons.YesNoCancel);

                if (result == DialogResult.Yes)
                {
                    cbo_employee.Invoke(new Action(() =>
                    {
                        String[] empids = new String[1];

                        if (cbo_employee.SelectedIndex != -1)
                        {
                            empids[0] = cbo_employee.SelectedValue.ToString();
                        }
                        else
                        {
                            empids = new String[cbo_employee.Items.Count];
                            for (int i = 0; i < empids.Length; i++)
                            {
                                cbo_employee.SelectedIndex = i;
                                empids[i] = cbo_employee.SelectedValue.ToString();
                            }
                        }

                        foreach (String _empid in empids)
                        {
                            table = "hr_dtr_sum_hdr";
                            code = pay_code;
                            d_from = gm.toDateString(date_from, "");
                            d_to = gm.toDateString(date_to, "");
                            d_now = DateTime.Now.ToString("yyyy-MM-dd");
                            t_now = DateTime.Now.ToString("HH:mm");
                            //col = "ppid,date_from,date_to,date_generated";
                            //val = "'" + code + "','" + d_from + "','" + d_to + "','" + d_now + "'";
                            col = "empid,ppid,date_from,date_to,date_generated,time_generated";
                            val = "'" + _empid + "','" + code + "','" + d_from + "','" + d_to + "','" + d_now + "','" + t_now + "'";

                            db.InsertOnTable(table, col, val);
                        }

                    }));


                    success = true;

                    if (save_summary(code))
                    {
                        // try { dgv_list_logs.Rows.Clear(); }
                        // catch (Exception) { }
                        cbo_employee.Invoke(new Action(() =>
                        {
                            cbo_employee.SelectedIndex = -1;
                            disp_list_history();
                        }));
                        cbo_payroll_period.Invoke(new Action(() =>
                        {
                            cbo_payroll_period.SelectedIndex = -1;
                        }));

                        MessageBox.Show("New DTR summary saved.");

                        pbar.Invoke(new Action(() =>
                        {
                            pbar.Value = 0;
                        }));

                    }
                    else
                    {
                        success = false;
                        db.DeleteOnTable(table, "summary_code='" + code + "'");
                        MessageBox.Show("Failed on saving.");
                    }

                }

            }
            this.Invoke((MethodInvoker)delegate
            {
                this.btn_generate.Enabled = true;
            });
           
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

        private Boolean is_generated_sum(String empid, String code)
        {
            Boolean ok = false;
            try
            {
                DataTable dt = db.QueryBySQLCode("SELECT empid FROM rssys.hr_dtr_sum_employees WHERE empid = '" + empid + "' AND ppid ='" + code + "'");

                if (dt.Rows.Count > 0)
                {
                    ok = true;
                }
            }
            catch { }
           
            return ok;
        }

        private void cbo_employee_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isUseCboEmp == false) 
                disp_list_history();
        }
        private void cbo_payroll_period_SelectedIndexChanged(object sender, EventArgs e)
        {
            disp_list_history();
        }

        private void disp_list_history()
        {
            dgv_list.Rows.Clear();
            try
            {
                String WHERE = "WHERE";

                if (cbo_employee.SelectedIndex != -1)
                {
                    WHERE += " empid='" + cbo_employee.SelectedValue + "'";
                }
                if (cbo_payroll_period.SelectedIndex != -1)
                {
                    WHERE += (WHERE == "WHERE" ? "" : " AND ");
                    WHERE += " ppid='" + cbo_payroll_period.SelectedValue + "'";
                }

                if (WHERE == "WHERE")
                    WHERE = "";

                DataTable dt = db.QueryBySQLCode("SELECT * FROM rssys.hr_dtr_sum_hdr " + WHERE + "  ORDER BY date_generated DESC, time_generated DESC");

                if (dt.Rows.Count > 0)
                {
                    for (int r = 0; dt.Rows.Count > r; r++) {
                        int i = dgv_list.Rows.Add();
                        DataGridViewRow row = dgv_list.Rows[i];

                        row.Cells["dgvl_date"].Value = gm.toDateString(dt.Rows[r]["date_generated"].ToString(), "");
                        row.Cells["dgvl_time"].Value = dt.Rows[r]["time_generated"].ToString();
                        row.Cells["dgvl_payroll"].Value = gm.toDateString(dt.Rows[r]["date_from"].ToString(), "") + " TO " + gm.toDateString(dt.Rows[r]["date_to"].ToString(), "");

                        row.Cells["dgvl_userid"].Value = dt.Rows[r]["empid"].ToString();

                    }
                }
            }
            catch { }
            
        }

        private void dgv_list_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
             
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }



        
    }
}
