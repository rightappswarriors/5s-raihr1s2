using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Deployment.Application;
using System.Net.NetworkInformation;
using System.Security.Cryptography;
using DevComponents.DotNetBar.Metro;
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Controls;

namespace Human_Resource_Information_System
{
    public partial class Login : MetroForm
    {
        Main frm;
        thisDatabase db;
        GlobalClass gc;
        GlobalMethod gm;
        String db_comp = "";
        public string ok = "";
        string cipher = "", mac_add = "", decipher = "";
        private bool mouseDown;
        private Point lastLocation;
        public Login()
        {
            InitializeComponent();
            GetMACAddress();
            frm = new Main();

            try
            {
                gc = new GlobalClass();
                gm = new GlobalMethod();

                btn_register.Text = thisDatabase.servers + ":" + thisDatabase.svr_port + "/" + thisDatabase.db_name;
                
                if (ApplicationDeployment.IsNetworkDeployed)
                {
                    //this.Text = this.Text + " " + string.Format("Ver.{0}", ApplicationDeployment.CurrentDeployment.CurrentVersion.ToString(4));
                    txt_version.Text = string.Format("Version {0}", ApplicationDeployment.CurrentDeployment.CurrentVersion.ToString(4));
                }
            }
            catch(Exception er)
            {
                MessageBox.Show("Wrong Database information. \n" + er.Message);
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

            return ﻿sMacAddress;
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
                    try {  GlobalClass.licensedkey = dt.Rows[0][0].ToString(); }
                    catch { }
                    ok = "ok";
                }
            }
        }
        private void enter_login()
        {
            try
            {
                db = new thisDatabase();

                String branch = "";
                String comp = "";
                String user = "";
                Boolean ismain = false;

                if (cbo_db.SelectedIndex == -1 )
                {
                    MessageBox.Show("Please select the database.");
                    cbo_db.DroppedDown = true;
                }       
                else
                {
                    //auto_cipher();
                    ok = "ok";

                    if (ok == "ok")
                    {
                        user = txt_user.Text.ToUpper();
                        branch = db.validate_login_return_branch(txt_user.Text, txt_pass.Text, comp).ToUpper();
                        comp = db.get_m99comp_name(branch);
                        ismain = db.get_isMainBranch(branch);

                        /*if (cbo_branch.SelectedIndex != -1)
                        {
                           // branch = cbo_branch.SelectedValue.ToString();
                        }*/
                        if (String.IsNullOrEmpty(branch) == false)
                        {
                            GlobalClass.username = user;
                            GlobalClass.branch = branch;
                            GlobalClass.isMainBranch = ismain;
                            GlobalClass.server_ip = btn_register.Text;
                            GlobalClass.dbcompany = comp;
                            DialogResult = DialogResult.OK;
                        }
                        else
                        {
                            MessageBox.Show("Invalid username and password. Pls try again.");
                            txt_pass.Text = "";
                        }
                    }
                    else
                    {
                        MessageBox.Show("Invalid username and password. Pls try again.");
                        txt_pass.Text = "";
                    }
                }                
            }
            catch { }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //System.Diagnostics.Process.Start("https://web.facebook.com/RightApps.Solutions/");

            MessageBox.Show("For information, contact 0915-806-0792 / 0942-734-7599.\nAlso like us on facebook \n https://web.facebook.com/RightAppsOfficial/");
        }

        private void pbox_logo_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://web.facebook.com/RightApps.Solutions/");

        }

        private void linklbl_web_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //linklbl_web.LinkVisited = true;
            System.Diagnostics.Process.Start("https://www.rightapps.solutions/");
        }

        private void linklbl_fb_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //linklbl_fb.LinkVisited = true;
            System.Diagnostics.Process.Start("https://www.facebook.com/RightApps.Solutions");
        }

        private void linklbl_email_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //linklbl_email.LinkVisited = true;
            System.Diagnostics.Process.Start("mailto:rightappsofficial@gmail.com");
        }

        private void cbo_branch_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txt_dbcompany_TextChanged(object sender, EventArgs e)
        {

        }

        private void btn_register_Click(object sender, EventArgs e)
        {
            GlobalClass.licensedkey = null;
            activation_form ac = new activation_form(this, mac_add, cipher);
            ac.ShowDialog();
        }

        private void btn_login_Click(object sender, EventArgs e)
        {

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

        private void btn_close_Click_1(object sender, EventArgs e)
        {
            this.Close();
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
                    //pbox_beta.Visible = false;
                    GlobalClass.is_beta = false;

                }
                else
                {
                    thisDatabase.db_name = thisDatabase.lcl_past_db;
                    pbox_company.Visible = true;
                    //pbox_beta.Visible = true;
                    GlobalClass.is_beta = true;
                }
            }
            catch { }
        }

        private void frm_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDown = true;
            lastLocation = e.Location;
        }

        private void btn_close_Click_2(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.MinimizeBox = true;
        }

        private void frm_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseDown)
            {
                this.Location = new Point(
                    (this.Location.X - lastLocation.X) + e.X, (this.Location.Y - lastLocation.Y) + e.Y);

                this.Update();
            }
        }

        private void frm_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;
        }
    }
}