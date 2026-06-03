using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CrystalDecisions.Shared;
namespace Human_Resource_Information_System
{
    public partial class Report : Form
    {
        String fileloc_acctg = "";
        String fileloc_hotel = "";
        String fileloc_inv = "";
        String fileloc_md = "";
        String fileloc_srvc = "";
        String fileloc_sales = "";
        String comp_name, comp_addr, comp_tel = "";
        String j_code = "", j_title, jtype_name;
        String j_numfrm = "", j_numto = "";
        int action = 0;
        ReportDocument myReportDocument;
        ParameterFieldDefinition crParameterFieldDefinition;
        ParameterValues crParameterValues;
        ParameterDiscreteValue crParameterDiscreteValue;
        ParameterFieldDefinitions crParameterFieldDefinitions;


        public Report()
        {
            InitializeComponent();
            try
            {
                thisDatabase db = new thisDatabase();
                myReportDocument = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
                crParameterValues = new ParameterValues();
                crParameterDiscreteValue = new ParameterDiscreteValue();

                String system_loc = db.get_system_loc();

                fileloc_acctg = system_loc + "\\Reports\\Accounting\\";
                fileloc_hotel = system_loc + "\\Reports\\Hotel\\";
                fileloc_inv = system_loc + "\\Reports\\Inventory\\";
                fileloc_md = system_loc + "\\Reports\\MD\\";
                fileloc_srvc = system_loc + "\\Reports\\Service\\";
                fileloc_sales = system_loc + "\\Reports\\Sale\\";
                //fileloc_acctg = "../..\\Reports/Accounting/";
                //fileloc_hotel = "../..\\Reports/Hotel/";
                //fileloc_inv = "../..\\Reports/Inventory/";
                //fileloc_md = "../..\\Reports/MD/";

                crParameterValues = new ParameterValues();
                crParameterDiscreteValue = new ParameterDiscreteValue();
                
            }
            catch (Exception er) { MessageBox.Show("Report Form Error Message:" + er.Message); }
        }
        public Report(String type)
        {
            InitializeComponent();

            try
            {
                thisDatabase db = new thisDatabase();
                myReportDocument = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
                crParameterValues = new ParameterValues();
                crParameterDiscreteValue = new ParameterDiscreteValue();
                
                String system_loc = db.get_system_loc();

                fileloc_acctg = system_loc + "\\Reports\\Accounting\\";
                fileloc_hotel = system_loc + "\\Reports\\Hotel\\";
                fileloc_inv = system_loc + "\\Reports\\Inventory\\";
                fileloc_md = system_loc + "\\Reports\\MD\\";
                fileloc_srvc = system_loc + "\\Reports\\Service\\";
                fileloc_sales = system_loc + "\\Reports\\Sale\\";

                //fileloc_acctg = "../..\\Reports/Accounting/";
                //fileloc_hotel = "../..\\Reports/Hotel/";
                //fileloc_inv = "../..\\Reports/Inventory/";
                //fileloc_md = "../..\\Reports/MD/";

                crParameterValues = new ParameterValues();
                crParameterDiscreteValue = new ParameterDiscreteValue();
               
            }
            catch (Exception er) { MessageBox.Show("Report Form Error Message:" + er.Message); }
        }

        private void Report_Load(object sender, EventArgs e)
        {
            thisDatabase db = new thisDatabase();

            comp_name = db.get_m99comp_name();
            comp_addr = db.get_m99comp_addr();
        }

        public void print_journalized_new(String ljtitle, String ljcode, String ljnumfrm, String ljnumto)
        {
            action = 7781;

            j_code = ljcode;
            j_title = ljtitle;
            j_numfrm = ljnumfrm;
            j_numto = ljnumto;

            bgWorker.RunWorkerAsync();
        }



        private void bgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            thisDatabase db = new thisDatabase();
            GlobalMethod gm = new GlobalMethod();
            GlobalClass gc = new GlobalClass();
            NumberToEnglish_orig amtinwords = new NumberToEnglish_orig();
            String _schema = db.get_schema();
            String WHERE = "";
            String sql_item1 = "", sql_item2 = "", sql_item_sum1 = "", sql_item_sum2 = "", sql_itemOnly1 = "", sql_itemOnly2 = "", sql_LaborOnly1 = "", sql_LaborOnly2 = "";
            int LIMIT_billing_items = 15;
            int LIMIT_billing_labor = 7;
            comp_name = db.get_m99comp_name();
            comp_addr = db.get_m99comp_addr();
            comp_tel = db.get_m99comp_tel();


            

            

            try
            {
                if (action == 7781)
                {
                    String sql = "SELECT t1.fy, t1.mo, t1.j_code, t1.j_num, to_char(t1.t_date, 'MM/dd/yyyy') AS t_date, t1.t_desc, t1.user_id, t2.seq_num, t2.at_code, m4.at_desc, t2.sl_code, t2.sl_name, t2.debit, t2.credit, t2.invoice, t2.seq_desc, t2.item_code, t2.item_desc, t2.unit, t2.recv_qty, t2.price, t2.scc_code FROM " + db.schema + ".tr02 t2 LEFT JOIN " + db.schema + ".tr01 t1 ON (t1.j_code=t2.j_code AND t1.j_num=t2.j_num) LEFT JOIN " + db.schema + ".m04 m4 ON m4.at_code=t2.at_code WHERE t1.j_code='" + j_code + "' AND t1.j_num BETWEEN '" + j_numfrm + "' AND '" + j_numto + "' ORDER BY t1.j_code,t1.j_num,t2.seq_num";
                    //_chg_dtto _chg_dtfrm j_title j_code

                    DataTable dt = db.QueryBySQLCode(sql);

                    rxt_msg.Invoke(new Action(() =>
                    {
                        rxt_msg.Text = sql;
                    }));

                    //sysdate systime

                    inc_pbar(10);
                    myReportDocument.Load(fileloc_acctg + "jrnlz_transaction.rpt");
                    myReportDocument.Database.Tables[0].SetDataSource(dt);
                    inc_pbar(10);
                    add_fieldparam("comp_name", comp_name);
                    add_fieldparam("comp_addr", comp_addr);
                    add_fieldparam("userid", GlobalClass.username);
                    add_fieldparam("journalize_title", j_title);
                    add_fieldparam("year", db.get_m99fy());
                    add_fieldparam("voucher_title", db.get_j_desc(j_code));
                }
                if (action > 0)
                {
                    inc_pbar(4);
                    disp_reportviewer(myReportDocument);
                }
                pbar_panl_hide();
            }
            catch (Exception er) { MessageBox.Show(":" + er.Message); }
            


        }

        private void pbar_panl_hide()
        {
            pnl_pbar.Invoke(new Action(() =>
            {
                pnl_pbar.Hide();
            }));
        }


        private void disp_reportviewer(ReportDocument myReportDocument)
        {
            try
            {
                crptviewer.Invoke(new Action(() =>
                {
                    try { crptviewer.ReportSource = myReportDocument; }
                    catch { }
                }));

                crptviewer.Invoke(new Action(() =>
                {
                    crptviewer.Refresh();
                }));
            }
            catch (Exception) { }
        }

        private void clr_param()
        {
            try
            {
                crParameterValues.Clear();
                crParameterValues.Add(crParameterDiscreteValue);
                crParameterFieldDefinition.ApplyCurrentValues(crParameterValues);
            }
            catch (Exception) { }
        }

        private void add_fieldparam(String col, String val)
        {
            crParameterDiscreteValue.Value = val;
            crParameterFieldDefinitions = myReportDocument.DataDefinition.ParameterFields;
            crParameterFieldDefinition = crParameterFieldDefinitions[col];
            crParameterValues = crParameterFieldDefinition.CurrentValues;
            clr_param();
            inc_pbar(10);
        }

        private void reset_pbar()
        {
            try
            {
                pbar.Invoke(new Action(() =>
                {
                    try { pbar.Value = 0; }
                    catch { }
                }));
            }
            catch (Exception er)
            {
                //MessageBox.Show(er.Message);
            }
        }


        private void inc_pbar(int i)
        {
            try
            {
                if (pbar.Value + i <= 100)
                {
                    pbar.Invoke(new Action(() =>
                    {
                        pbar.Value += i;
                    }));
                }
                else
                {
                    reset_pbar();
                }

            }
            catch (Exception)
            {
                reset_pbar();
            }
        }

        
    }
}
