using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Windows.Forms;
using System.Reflection;
using Npgsql;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TreeView;
using System.Collections;

namespace Human_Resource_Information_System
{
    public class dbHRIS : thisDatabase
    {
        GlobalClass gc;
        GlobalMethod gm;

        public dbHRIS()
        {
            gc = new GlobalClass();
            gm = new GlobalMethod();
        }

        public DataTable get_view_emp_profile_payroll(String col = "*", String WHERE = "", String ext = "")
        {
            return QueryOnTableWithParams("view_emp_profile_payroll", col, WHERE, ext);
        }

        public DataTable get_view_emp_dtr(String col = "*", String WHERE = "", String ext = "")
        {
            return QueryOnTableWithParams("view_emp_dtr", col, WHERE, ext);
        }

        public DataTable get_view_emp_dtr_current(String col = "*", String WHERE = "", String ext = "")
        {
            return QueryOnTableWithParams("view_emp_dtr_current", col, WHERE, ext);
        }

        public DataTable get_view_emp_dtr_summary(String col = "*", String WHERE = "", String ext = "")
        {
            return QueryOnTableWithParams("view_emp_dtr_summary", col, WHERE, ext);
        }

        public String get_print_view_emp_dtr_summary_str(String WHERE = "", Boolean isMinute = false)
        {
            String minutes = "";

            if(isMinute)
            { minutes = "_min";  }

            return @"SELECT to_char(e.gen_date,'MM/DD/YY DY') AS gen_date, dayoff, 
                                e.lastname||','||e.firstname||' '||e.mi  ||' - '|| e.status_name AS header_right,  
                                e.dept_name ||' - '||  e.position_name ||' ['|| e.empid ||']' AS header_left, 
	                            work_date, timein, breakout, breakin, timeout, total_hr, work_hr, work_hr_min,  
                                late" + minutes + " AS late, undertime" + minutes + " AS undertime, overtime" + minutes + @" AS overtime, 
	                            work_hr_min, late_min, undertime_min, overtime_min, 
	                            (work_hr - late - undertime + overtime) AS work_nettotal, 
	                            absent, leavecredit, leave_desc, leave_type, mon
	                            FROM rssys.view_emp_dtr_summary e 
	                            WHERE 1=1 " + WHERE + @"
                                ORDER BY e.empid, e.gen_date";
        }

        public DataTable get_print_view_emp_dtr_summary(String WHERE = "")
        {
            String query = @"SELECT to_char(e.gen_date,'MM/DD/YY DY') AS gen_date, dayoff, 
                                e.lastname||','||e.firstname||' '||e.mi  ||' - '|| e.status_name AS header_right,  
                                e.dept_name ||' - '||  e.position_name ||' ['|| e.empid ||']' AS header_left, 
	                            work_date, timein, breakout, breakin, timeout, total_hr, work_hr, work_hr_min,  
                                CASE WHEN late >=8 THEN late-1 ELSE late END AS late, undertime, overtime, 
	                            work_hr_min, late_min, undertime_min, overtime_min, 
	                            (work_hr - late - undertime + overtime) AS work_nettotal, 
	                            absent, leavecredit, leave_desc, leave_type, mon
	                            FROM rssys.view_emp_dtr_summary e 
	                            WHERE 1=1 " + WHERE + @"
                                ORDER BY e.empid, e.gen_date";

            return QueryBySQLCode(query);
        }

        public DataTable get_listof_emp_dtr_summary(String WHERE = "")
        {
            String query = @"SELECT DISTINCT empid, lastname ||', '|| firstname  ||', '|| mi AS name, status_name, dept_name, position_name, 
                            SUM(work_nettotal) AS work_hr, SUM(late_min) AS total_late, SUM(undertime_min) AS undertime, 
                            SUM(overtime_min) AS overtime, SUM(work_nettotal) AS work_nettotal, 
                            SUM(days_worked) AS days_worked, SUM(absent) AS absent, SUM(leavecredit) AS leavecredit
                            FROM (
                                SELECT to_char(e.gen_date,'MM/DD/YY DY') AS gen_date, dayoff, 
	                            e.empid, e.lastname, e.firstname, e.mi, e.status_name, e.dept_name,  e.position_name, 
	                            work_date, timein, breakout, breakin, timeout, total_hr, 
	                            work_hr,  
                                CASE WHEN late >=8 THEN late-1 ELSE late END AS late, undertime, overtime, 
	                            work_hr_min, late_min, undertime_min, overtime_min, 
	                            CASE WHEN (work_hr - late - undertime + overtime) > 4 THEN (work_hr - late - undertime + overtime) - 1
	                            ELSE (work_hr - late - undertime + overtime) END AS work_nettotal, 
	                            days_worked, absent, leavecredit, leave_desc, leave_type, mon
	                            FROM rssys.view_emp_dtr_summary e 
	                            WHERE 1=1 " + WHERE + @"
                                ORDER BY e.empid, e.gen_date
                            ) e 
                            GROUP BY empid, lastname, firstname, mi, status_name, dept_name, position_name";

            return QueryBySQLCode(query);
        }
    }
}
