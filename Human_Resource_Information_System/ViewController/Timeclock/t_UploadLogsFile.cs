using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Excel = Microsoft.Office.Interop.Excel;
using System.Text.RegularExpressions;

namespace Human_Resource_Information_System
{
    public partial class t_UploadLogsFile : Form
    {

        private GlobalClass gc;
        private GlobalMethod gm;
        thisDatabase db = new thisDatabase();
        //public SDKHelper SDK = new SDKHelper();
        DataTable dtperiod;
        public t_UploadLogsFile()
        {
            InitializeComponent();
        }

        private void t_UploadLogsFile_Load(object sender, EventArgs e)
        {
            //dips_list();
            dispfrombiometric();
            //bgWorker.RunWorkerAsync();
            //pbar.Hide();
            //dtp_frm.Value = DateTime.Now.ToString("yyyy-MM-dd");
        }

        private void bgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            
        }

        private void dispfrombiometric(){
            DataTable dt_period = new DataTable("dt_period");
            dgv_list.AutoGenerateColumns = true;
            dgv_list.Columns.Clear();
            dgv_list.Rows.Clear();

            dt_period.Columns.Add("ID", System.Type.GetType("System.Int32"));
            dt_period.Columns.Add("BIOMETRIC_ID", System.Type.GetType("System.String"));
            dt_period.Columns.Add("EMPLOYEE_NAME", System.Type.GetType("System.String"));
            dt_period.Columns.Add("WORK_DATE", System.Type.GetType("System.String"));
            dt_period.Columns.Add("WORK_TIME", System.Type.GetType("System.String"));
            dt_period.Columns.Add("STATUS", System.Type.GetType("System.String"));
            
            dgv_list.DataSource = dt_period;
            
            dgv_list.Columns[0].Visible = false;
            dgv_list.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgv_list.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgv_list.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgv_list.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgv_list.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgv_list.FirstDisplayedScrollingRowIndex = dgv_list.Rows.Count - 1;
            //dgv_list.CurrentCell = dgv_list.Rows[0].Cells[1];
            //dgv_list.CurrentCell.Selected = true;
            //dgv_list.Rows[].Selected = true;
            dgv_list.Sort(dgv_list.Columns[0], ListSortDirection.Descending);
            
            //dgv_list.BeginEdit(true);
            //(dgv_list.DataSource as DataTable).DefaultView.RowFilter = string.Format("EMPLOYEE_NAME = '{0}'", "Ryan");

            //Login.SDK.sta_readAttLog(dt_period);
            this.dtperiod = dt_period;
        }

        private void getDataFromBiometric(){
            button2.Enabled = false;
            String date_from = dtp_frm.Value.ToString("yyyy-MM-dd");
            String date_to = dtp_to.Value.ToString("yyyy-MM-dd");
            String query = "DELETE FROM rssys.hr_tito2 WHERE work_date BETWEEN '" + date_from + "' AND '" + date_to + "'";
            db.QueryBySQLCode(query);
            int rCnt = 0;
            String empid = "", logs_id = "", time_log = "", status = "", source = "", dt = "", c_tlog = "", in_out = "", staticval = "";
            DateTime work_date = new DateTime();
            DataTable data;
            String col = "", val = "";
            DataTable dt_period = this.dtperiod;
            String table = "hr_tito2";
            for (int i = 0; i < dt_period.Rows.Count; i++)
            {
                string id = dt_period.Rows[i].ItemArray[1].ToString();
                string name = dt_period.Rows[i].ItemArray[2].ToString();
                string date = dt_period.Rows[i].ItemArray[3].ToString();
                string time = DateTime.Parse(dt_period.Rows[i].ItemArray[4].ToString()).ToString("HH:mm");
                status = dt_period.Rows[i].ItemArray[5].ToString();
                DataTable bio_empid = null;
                
                if (rCnt != 100 || rCnt < 100)
                {
                    pbar2.Value = rCnt++;
                }

                bio_empid = db.QueryBySQLCode("SELECT empid FROM rssys.hr_employee WHERE biometric = '" + id + "' LIMIT 1");
                if (bio_empid != null && bio_empid.Rows.Count > 0)
                {
                    empid = bio_empid.Rows[0]["empid"].ToString();
                    work_date = Convert.ToDateTime(date); 
                    source = "B";
                    in_out = (status.Equals("IN")) ? "I" : "O";

                    data = db.QueryBySQLCode("SELECT * FROM rssys.hr_tito2 WHERE empid = '" + empid + "' AND work_date='" + work_date.ToString("yyyy-MM-dd") + "' AND time_log='" + time + "' AND status = '" + in_out + "'");
                    if (data != null && data.Rows.Count <= 0)
                    {
                        System.Diagnostics.Debug.WriteLine("Not Found");
                        DataTable check = db.QueryBySQLCode("SELECT * FROM rssys.hr_tito2 WHERE empid = '" + empid + "' and work_date = '" + work_date.ToString("yyyy-MM-dd") + "' and status = '"+in_out+"'");
                        if (check != null && check.Rows.Count <= 0)
                        {
                            logs_id = db.get_pk("logs_id");
                            col = "logs_id,work_date,time_log,empid,status,source";
                            val = "'" + logs_id + "','" + work_date.ToString("yyyy-MM-dd") + "','" + time + "','" + empid + "','" + in_out + "','" + source + "'";
                            db.InsertOnTable(table, col, val);
                            db.set_pkm99("logs_id", db.get_nextincrementlimitchar(logs_id, 8));
                            data = null;
                        }
                        else
                        {
                            //if the the user twice time out the last record of time_out will be record
                            /*if (in_out.Equals("O")){
                                String delete = "DELETE FROM rssys.hr_tito2 WHERE empid = '" + empid + "' and work_date = '" + work_date.ToString("yyyy-MM-dd") + "' and status = '" + in_out + "'";
                                db.QueryBySQLCode(delete);
                                logs_id = db.get_pk("logs_id");
                                col = "logs_id,work_date,time_log,empid,status,source";
                                val = "'" + logs_id + "','" + work_date.ToString("yyyy-MM-dd") + "','" + time + "','" + empid + "','" + in_out + "','" + source + "'";
                                db.InsertOnTable(table, col, val);
                                db.set_pkm99("logs_id", db.get_nextincrementlimitchar(logs_id, 8));
                                data = null;
                            }
                            else {
                                continue;
                            }*/
                        }
                       
                        
                    }
                     

                }
                else{
                    continue;
                }

                


            }
            DialogResult results = MessageBox.Show("Generate Successfully", "Confirmation", MessageBoxButtons.OK);
            if (results == DialogResult.OK)
            {
                pbar2.Value = 0;
                textBox1.Text = "";
                button2.Enabled = true;
            }
        }

        private void dgv_list_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        void dips_list() {

            DataTable dt;
            dt = db.QueryBySQLCode("SELECT * FROM rssys.hr_tito2 ORDER BY work_date DESC");
            dgv_list.DataSource = dt;
        }
        private void btn_browse_Click_1(object sender, EventArgs e)
        {
            
            OpenFileDialog fDialog = new OpenFileDialog();
            fDialog.Title = "Select file to be upload";
            fDialog.Filter = "(*.txt)|*.txt";
            if (fDialog.ShowDialog() == DialogResult.OK)
            {
                //textBox1.Text = fDialog.FileName.ToString();
                   
            }

        }

        private void btn_upload_Click(object sender, EventArgs e)
        {


            int rCnt = 0;
            int rw = 0;
            int cl = 0;
            String filename = "";
            String col = "", val = "";
            DataTable data;
            String line = "";
            String table = "hr_tito2";
            String empid = "", logs_id="",time_log = "", status = "", source = "",dt = "",c_tlog="",in_out="",staticval="";
            DateTime work_date;
            //DateTime excel_time;

            
            if (String.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("Please select a file to be uploaded.");
            }
            else
            {
                      
                String pattern = "\\s+";
                String replacement = " ";
                Regex rgx = new Regex(pattern);
                String input = textBox1.Text;
                StreamReader sr = new StreamReader(textBox1.Text);
                DataTable bio_empid = null;
                String bio_id = "";
                string temp = "";
                int row_line = 0;
                while(line !=null)
                {
                    data = null;
                    line= sr.ReadLine();
                    if(line!=null)
                    {
                       try
                       {
                            String yow = line;
                            string result = rgx.Replace(yow, replacement);
                            string[] split = result.Split(' ');

                            bio_id = split[0]; //get employee id WHERE biometric='split[0]'

                            bio_empid = db.QueryBySQLCode("SELECT empid FROM rssys.hr_employee WHERE biometric = '" + bio_id + "' LIMIT 1");
                            if (bio_empid != null && bio_empid.Rows.Count > 0)
                            {
                                empid = bio_empid.Rows[0]["empid"].ToString();
                                work_date = Convert.ToDateTime(split[1]);

                                time_log = temp = split[2];
                                in_out = split[3];
                                staticval = split[4];

                               // MessageBox.Show("Empid : " + empid + " Workdate : " + work_date + " Line : " + row_line + "Time Log : " + time_log);
                                pbar.Show();
                                if (in_out == staticval)
                                {
                                    status = "O";
                                }
                                else
                                {
                                    status = "I";
                                }
                                source = "M";

                                
                                data = db.QueryBySQLCode("SELECT * FROM rssys.hr_tito2 WHERE empid = '" + empid + "' AND work_date='" + work_date.ToString("yyyy-MM-dd") + "' AND time_log='" + time_log + "' AND status = '" + status +"'");
                                if (data != null && data.Rows.Count <= 0)
                                {
                                    logs_id = db.get_pk("logs_id");
                                    col = "logs_id,work_date,time_log,empid,status,source";
                                    val = "'" + logs_id + "','" + work_date.ToString("yyyy-MM-dd") + "','" + time_log + "','" + empid + "','" + status + "','" + source + "'";

                                    db.InsertOnTable(table, col, val);
                                    db.set_pkm99("logs_id", db.get_nextincrementlimitchar(logs_id, 8));
                                    data = null;
                                }
                                
                                if (rCnt != 100 || rCnt < 100)
                                {
                                    pbar.Value = rCnt++;
                                }
                            }
                            else { continue; }
                            
                        }
                        catch (Exception er)
                        {
                            MessageBox.Show(er.StackTrace + "\n : Bio ID :" + bio_id + " Temp :" + temp + "Empid : " +empid  + "Row : " + row_line);
                        }
                    }
                    row_line++;
                }
                sr.Close();
                DialogResult results = MessageBox.Show("File Uploaded", "Confirmation", MessageBoxButtons.OK);
                if (results == DialogResult.OK)
                {
                    pbar.Value = 0;
                    pbar.Hide();
                    textBox1.Text = "";
                }
            }

            dips_list();
                
        }


        public String getTimeString(Excel.Range range, int row, int col)
        {
            DateTime dt = DateTime.Now;
            String dtstr = "";
            if (range != null)
            {
                try
                {
                    dtstr = getString(range, row, col);
                    try { dt = DateTime.Parse(dt.ToString("yyyy-MM-dd ") + dtstr); }
                    catch { dt = DateTime.FromOADate(Double.Parse(dtstr)); }
                }
                catch { }
            }
            return dt.ToString("HH:mm");
        }
        public String getString(Excel.Range range, int row, int col)
        {
            String str = "";
            if (range != null)
            {
                try
                {
                    str = Convert.ToString((range.Cells[row, col] as Excel.Range).Value2 ?? "");
                }
                catch { }
            }
            return str;
        }   
        public String getDateString(Excel.Range range, int row, int col)
        {
            DateTime dt = DateTime.Now;
            String dtstr = "";
            if (range != null)
            {
                try
                {
                    dtstr = getString(range, row, col);
                    try { dt = DateTime.Parse(dtstr); }
                    catch { dt = DateTime.FromOADate(Double.Parse(dtstr)); }
                }
                catch { }
            }
            return dt.ToString("yyyy-MM-dd");
        }
        private void dgv_list_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            String date_from = dtp_frm.Value.ToString("yyyy-MM-dd");
            String date_to = dtp_to.Value.ToString("yyyy-MM-dd");
            (dgv_list.DataSource as DataTable).DefaultView.RowFilter = "WORK_DATE >= #" + date_from + "# AND WORK_DATE <= #" + date_to + "#";
            dgv_list.Update();
            dgv_list.Refresh();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            getDataFromBiometric();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        


       
       
    }
}
