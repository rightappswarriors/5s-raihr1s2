//using Human_Resource_Information_System.Managers;
using Human_Resource_Information_System.Properties;
using MetroFramework.Forms;
using System;
using System.Data;
using System.Deployment.Application;
using System.Net.NetworkInformation;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace Human_Resource_Information_System
{
    public partial class Login : MetroForm
    {
        thisDatabase db;
        GlobalClass gc;
        GlobalMethod gm;
        String db_comp = "";
        public string ok = "";
        string cipher = "", mac_add = "", decipher = "";

        public Login()
        {
            InitializeComponent();
            GetMACAddress();

            try
            {
                gc = new GlobalClass();
                gm = new GlobalMethod();
            } catch (Exception er) { MessageBox.Show("Error on GM/GC. " + er.Message ); }

            try
            {
                db = new thisDatabase();
            }
            catch (Exception er) { MessageBox.Show("Wrong Database information. " + er.Message); }

            try
            {
                txt_server_db.Text = thisDatabase.servers + ":" + thisDatabase.svr_port + "/" + thisDatabase.db_name;

                if (ApplicationDeployment.IsNetworkDeployed)
                {
                    //this.Text = this.Text + " " + string.Format("Ver.{0}", ApplicationDeployment.CurrentDeployment.CurrentVersion.ToString(4));
                    txt_version.Text = string.Format("Version {0}", ApplicationDeployment.CurrentDeployment.CurrentVersion.ToString(4));
                }
            }
            catch (Exception er)
            {
                MessageBox.Show("Version display error. \n" + er.Message);
            }
        }

        public static string Encrypt(string input, string key)
        {
            byte[] inputArray = UTF8Encoding.UTF8.GetBytes(input);
            TripleDESCryptoServiceProvider tripleDES = new TripleDESCryptoServiceProvider();
            tripleDES.Key = UTF8Encoding.UTF8.GetBytes(key);
            tripleDES.Mode = CipherMode.ECB;
            tripleDES.Padding = PaddingMode.PKCS7;
            ICryptoTransform cTransform = tripleDES.CreateEncryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(inputArray, 0, inputArray.Length);
            tripleDES.Clear();

            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }

        public static string Decrypt(string input, string key)
        {
            byte[] inputArray = Convert.FromBase64String(input);
            TripleDESCryptoServiceProvider tripleDES = new TripleDESCryptoServiceProvider();
            tripleDES.Key = UTF8Encoding.UTF8.GetBytes(key);
            tripleDES.Mode = CipherMode.ECB;
            tripleDES.Padding = PaddingMode.PKCS7;
            ICryptoTransform cTransform = tripleDES.CreateDecryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(inputArray, 0, inputArray.Length);
            tripleDES.Clear();

            return UTF8Encoding.UTF8.GetString(resultArray);
        }

        private void btn_access_Click(object sender, EventArgs e)
        {
            enter_login();
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Login_Load(object sender, EventArgs e)
        {
            try
            {
                cbo_db.SelectedIndex = 0;
            }
            catch { }
        }

        private void txt_user_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                enter_login();
            }
        }

        private void txt_pass_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                enter_login();
            }
        }

        public string GetMACAddress()
        {
            NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
            String sMacAddress = string.Empty;

            foreach (NetworkInterface adapter in nics)
            {
                if (sMacAddress == String.Empty)// only return MAC Address from first card
                {
                    IPInterfaceProperties properties = adapter.GetIPProperties();
                    sMacAddress = adapter.GetPhysicalAddress().ToString();
                }
            }
            this.mac_add = sMacAddress;

            return sMacAddress;
        }

        void auto_cipher()
        {
            if (mac_add != string.Empty)
            {

                if (mac_add != string.Empty)
                {
                    //Here key is of 128 bit  
                    //Key should be either of 128 bit or of 192 bit  
                    cipher = Login.Encrypt(mac_add, "sblw-3hn8-sqoy19");
                }
                if (cipher != string.Empty)
                {
                    //Key shpuld be same for encryption and decryption  
                    decipher = Login.Decrypt(cipher, "sblw-3hn8-sqoy19");
                }

                DataTable dt = new DataTable();

                dt = db.QueryBySQLCode("SELECT license_id from rssys.x09 WHERE licensed_pc='" + cipher + "'");

                if (dt.Rows.Count <= 0)
                {
                    GlobalClass.licensedkey = null;
                    activation_form ac = new activation_form(this, mac_add, cipher);
                    ac.ShowDialog();
                }
                else
                {
                    try { GlobalClass.licensedkey = dt.Rows[0][0].ToString(); }
                    catch { }
                    ok = "ok";
                }
            }
        }

        private void enter_login()
        {
            try
            {
                String branch = "", bid = "", bname = "", cityid = "", ctname = "", provid = "", provname = "", cntry_code = "", cntry_desc = "", grp_desc = "", rep_code="", rep_name = "";
                String comp = "";
                String user = "", grp_id = "";
                Boolean ismain = false, r_override = false, r_approve_po = false, r_finalized_jrnl = false , r_view_stkadj = false;
                DataTable dt = null;

                if (cbo_db.SelectedIndex == -1)
                {
                    MessageBox.Show("Please select the database.");
                    cbo_db.DroppedDown = true;
                }
                else
                {
                    //auto_cipher();

                    //if(db.is_branch_online() == false) 
                    //if(thisDatabase.lcl_db == "live_sanhec" || thisDatabase.lcl_db == "past_sanhec" || thisDatabase.lcl_db == "sanhec" || thisDatabase.lcl_db == "live_ocs" || thisDatabase.lcl_db == "past_ocs" || thisDatabase.lcl_db == "ocs")
                    /*{
                        MessageBox.Show("Your branch is not accessible for the moment. Please login later.\nYou may contact the provider for more concerns. Thank you.","Branch Temporarily Offline");
                    }
                    else
                    {*/
                    ok = "ok";

                    if (ok == "ok")
                    {
                        user = txt_user.Text.ToUpper();
                        dt = db.validate_login_return_dt(txt_user.Text, txt_pass.Text);

                        //try
                        //{
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                grp_id = dt.Rows[i]["grp_id"].ToString();
                                grp_desc = dt.Rows[i]["grp_desc"].ToString();
                                branch = dt.Rows[i]["branch"].ToString();
                                bid = dt.Rows[i]["bid"].ToString();
                                bname = dt.Rows[i]["bname"].ToString();
                                cityid = dt.Rows[i]["cityid"].ToString();
                                ctname = dt.Rows[i]["ctname"].ToString();
                                provid = dt.Rows[i]["provid"].ToString();
                                provname = dt.Rows[i]["provname"].ToString();
                                cntry_code = dt.Rows[i]["cntry_code"].ToString();
                                cntry_desc = dt.Rows[i]["cntry_desc"].ToString();
                                rep_code = dt.Rows[i]["rep_code"].ToString();
                                rep_name = dt.Rows[i]["rep_name"].ToString();

                                r_override = gm.toBooleanFormat(dt.Rows[i]["override"].ToString());
                                r_approve_po = gm.toBooleanFormat(dt.Rows[i]["approve_po"].ToString());
                                r_finalized_jrnl = gm.toBooleanFormat(dt.Rows[i]["finalized_jrnl"].ToString());
                                r_view_stkadj = gm.toBooleanFormat(dt.Rows[i]["view_stkadj"].ToString());
                            }
                       // }
                       // catch (Exception) { }

                        if (String.IsNullOrEmpty(branch) == false) { branch = branch.ToUpper(); }

                        if (String.IsNullOrEmpty(branch) == false && db.is_branch_online(branch) == false && user != "ADMIN")
                        {
                            MessageBox.Show("Your branch is not accessible for the moment. Please login later.\nYou may contact the administrator or the provider for more concerns. Thank you.", "Branch Temporarily Offline");
                        }
                        else
                        {
                            comp = db.get_m99comp_name(branch);
                            ismain = db.get_isMainBranch(branch);

                            if (String.IsNullOrEmpty(branch) == false)
                            {
                                GlobalClass.username = user;
                                GlobalClass.user_fullname = db.getFullName(GlobalClass.username);
                                GlobalClass.grp_id = grp_id;
                                GlobalClass.grpdesc = grp_desc;

                                GlobalClass.right_override = r_override;
                                GlobalClass.right_approve_po = r_approve_po;
                                GlobalClass.right_finalized_jrnl = r_finalized_jrnl;
                                GlobalClass.right_view_stkadj = r_view_stkadj;

                                GlobalClass.branch = branch;
                                GlobalClass.branch_name = db.get_branchname(GlobalClass.branch);
                                GlobalClass.isMainBranch = ismain;
                                GlobalClass.server_ip = txt_server_db.Text;
                                GlobalClass.dbcompany = comp;

                                GlobalClass.bid = bid;
                                GlobalClass.bname = bname;
                                GlobalClass.cityid = cityid;
                                GlobalClass.ctname = ctname;
                                GlobalClass.provid = provid;
                                GlobalClass.provname = provname;
                                GlobalClass.cntry_code = cntry_code;
                                GlobalClass.cntry_desc = cntry_desc;

                                GlobalClass.rep_code = rep_code;
                                GlobalClass.rep_name = rep_name;

                                GlobalClass.projcompany = db.get_m99_systemtype();

                                set_main_module_rights();

                                DialogResult = DialogResult.OK;
                            }
                            else
                            {
                                MessageBox.Show("Invalid username and password. Pls try again.");
                                txt_pass.Text = "";
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Invalid username and password. Pls try again.");
                        txt_pass.Text = "";
                    }
                    //}                    
                }
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message + "\n\nInvalid username and password.\nPls try again.");
                txt_pass.Text = "";
            }
        }

        private void set_main_module_rights()
        {
            GlobalClass.is_playhouse = false;

            if (thisDatabase.lcl_db == "live_mukja")
            {
                GlobalClass.is_acctg = true;
                GlobalClass.is_crm = false;
                GlobalClass.is_sales = true;
                GlobalClass.is_production = false;
                GlobalClass.is_inventory = true;
                GlobalClass.is_loans = false;
                GlobalClass.is_property = false;
            }
            //Property Managemetn System  ( PMS )
            else if (thisDatabase.lcl_db == "live_44joseph" || GlobalClass.projcompany == "PMS")
            {
                GlobalClass.is_acctg = true;
                GlobalClass.is_crm = false;
                GlobalClass.is_sales = false;
                GlobalClass.is_production = false;
                GlobalClass.is_inventory = false;
                GlobalClass.is_loans = false;
                GlobalClass.is_property = true;
            }// Hotel System  ( HMS )
            else if (thisDatabase.lcl_db == "live_sierra" || GlobalClass.projcompany == "HMS")
            {
                GlobalClass.is_acctg = true;
                GlobalClass.is_crm = false;
                GlobalClass.is_sales = true;
                GlobalClass.is_production = false;
                GlobalClass.is_inventory = true;
                GlobalClass.is_loans = false;
                GlobalClass.is_property = false;
            }
            else if (GlobalClass.projcompany == "FLMS")
            {
                GlobalClass.is_acctg = true;
                GlobalClass.is_crm = false;
                GlobalClass.is_sales = false;
                GlobalClass.is_production = false;
                GlobalClass.is_inventory = true;
                GlobalClass.is_loans = true;
                GlobalClass.is_property = false;
            }
            //Sales Inventory for basic package
            else if (GlobalClass.projcompany == "IS")
            {
                GlobalClass.is_acctg = false;
                GlobalClass.is_crm = false;
                GlobalClass.is_sales = true;
                GlobalClass.is_production = false;
                GlobalClass.is_inventory = true;
                GlobalClass.is_loans = false;
                GlobalClass.is_property = false;
            }
            //Sales Inventory CRM
            else if (GlobalClass.projcompany == "CIS")
            {
                GlobalClass.is_acctg = false;
                GlobalClass.is_crm = true;
                GlobalClass.is_sales = true;
                GlobalClass.is_production = false;
                GlobalClass.is_inventory = true;
                GlobalClass.is_loans = false;
                GlobalClass.is_property = false;
            }
            //Sales Inventory Accounting CRM for advance package
            else if (GlobalClass.projcompany == "ACIS")
            {
                GlobalClass.is_acctg = true;
                GlobalClass.is_crm = true;
                GlobalClass.is_sales = true;
                GlobalClass.is_production = false;
                GlobalClass.is_inventory = true;
                GlobalClass.is_loans = false;
                GlobalClass.is_property = false;
            }
            //Sales Inventory Production Accounting for advance package
            else if (GlobalClass.projcompany == "APIS")
            {
                GlobalClass.is_acctg = true;
                GlobalClass.is_crm = true;
                GlobalClass.is_sales = true;
                GlobalClass.is_production = true;
                GlobalClass.is_inventory = true;
                GlobalClass.is_loans = false;
                GlobalClass.is_property = false;
            }
            //Sales Inventory  for Playhouse System ( PIS )
            else if (GlobalClass.projcompany == "PIS")
            {
                GlobalClass.is_acctg = false;
                GlobalClass.is_crm = false;
                GlobalClass.is_sales = false;
                GlobalClass.is_playhouse = true;
                GlobalClass.is_production = false;
                GlobalClass.is_inventory = false;
                GlobalClass.is_loans = false;
                GlobalClass.is_property = false;
            }
            //Sales Inventory Accounting for Essential System ( AIS )
            else
            {
                GlobalClass.is_acctg = true;
                GlobalClass.is_crm = false;
                GlobalClass.is_sales = true;
                GlobalClass.is_production = false;
                GlobalClass.is_inventory = true;
                GlobalClass.is_loans = false;
                GlobalClass.is_property = false;
            }
            GlobalClass.is_hr = true;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //System.Diagnostics.Process.Start("https://www.facebook.com/RightAppsIncorporated");

            MessageBox.Show("For information, contact 0915-806-0792 / 0917-777-9713.\nAlso like us on facebook \nhttps://www.facebook.com/RightAppsIncorporated");
        }

        private void pbox_logo_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.facebook.com/RightAppsIncorporated");

        }

        private void linklbl_web_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //linklbl_web.LinkVisited = true;
            System.Diagnostics.Process.Start("https://www.rightapps.solutions/");
        }

        private void linklbl_fb_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //linklbl_fb.LinkVisited = true;
            System.Diagnostics.Process.Start("https://www.facebook.com/RightAppsIncorporated");
        }

        private void linklbl_email_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //linklbl_email.LinkVisited = true;
            System.Diagnostics.Process.Start("mailto:rightappsofficial@gmail.com");
        }

        private void btn_register_Click(object sender, EventArgs e)
        {
            GlobalClass.licensedkey = null;
            activation_form ac = new activation_form(this, mac_add, cipher);
            ac.ShowDialog();
        }

        private void btn_timelog_Click(object sender, EventArgs e)
        {
            String strURL = thisDatabase.hris_url;

            try
            {
                //Launch Chrome in a new window
                System.Diagnostics.Process.Start("chrome", strURL + " --new-window");
            }
            catch
            {
                try
                {
                    //Chrome not found ... launch Firefox in a new window
                    System.Diagnostics.Process.Start("firefox", "-new-window " + strURL);
                }
                catch
                {
                    //WARN THE USER TO INSTALL A BROWSER...
                }
            }
        }

        private void btn_settings_Click(object sender, EventArgs e)
        {

        }

        private void pbox_company_Click(object sender, EventArgs e)
        {

        }

        private void cbo_db_SelectedIndexChanged(object sender, EventArgs e)
        {
            String branch = "";
            Boolean isMain = false;

            try
            {
                if (cbo_db.SelectedIndex == 0)
                {
                    thisDatabase.db_name = thisDatabase.lcl_db;
                    pbox_company.Visible = true;
                    pbox_beta.Visible = false;
                    GlobalClass.is_beta = false;

                }
                else
                {
                    thisDatabase.db_name = thisDatabase.lcl_past_db;
                    pbox_company.Visible = true;
                    pbox_beta.Visible = true;
                    GlobalClass.is_beta = true;
                }
            }
            catch { }
        }
    }
}