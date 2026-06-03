using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Deployment.Application;
using MetroFramework.Forms;

namespace Human_Resource_Information_System
{
    public partial class Login : MetroForm
    {
        thisDatabase db = new thisDatabase();
        GlobalClass gc;
        GlobalMethod gm;
        public String ok = "ok";

        //public static SDKHelper SDK = new SDKHelper();
        public Login()
        {
            InitializeComponent();
            
            try
            {
                gc = new GlobalClass();
                gm = new GlobalMethod();
                lbl_server.Text = thisDatabase.servers;

                if (ApplicationDeployment.IsNetworkDeployed)
                {
                    lbl_version.Text = string.Format("Version {0}", ApplicationDeployment.CurrentDeployment.CurrentVersion.ToString(4));
                }
            }
            catch (Exception er)
            {
                MessageBox.Show("Wrong Database information. \n" + er.Message);
            }
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
                String brn = db.get_m99branch();

                cbo_db.SelectedIndex = 0;

                cbo_branch.SelectedValue = brn;
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

        private void getDeviceInfo()
        {
            string sFirmver = "";
            string sMac = "";
            string sPlatform = "";
            string sSN = "";
            string sProductTime = "";
            string sDeviceName = "";
            int iFPAlg = 0;
            int iFaceAlg = 0;
            string sProducter = "";
            //SDK.sta_GetDeviceInfo(out sFirmver, out sMac, out sPlatform, out sSN, out sProductTime, out sDeviceName, out iFPAlg, out iFaceAlg, out sProducter);

            string device = sDeviceName;

            /*

            Terminal.SDK.sta_GetDeviceInfo(Terminal.lbSysOutputInfo, out sFirmver, out sMac, out sPlatform, out sSN, out sProductTime, out sDeviceName, out iFPAlg, out iFaceAlg, out sProducter);
            txtFirmwareVer.Text = sFirmver;
            txtMac.Text = sMac;
            txtSerialNumber.Text = sSN;
            txtPlatForm.Text = sPlatform;
            txtDeviceName.Text = sDeviceName;
            txtFPAlg.Text = iFPAlg.ToString().Trim();
            txtFaceAlg.Text = iFaceAlg.ToString().Trim();
            txtManufacturer.Text = sProducter;
            txtManufactureTime.Text = sProductTime;
             */

        }

        public void connectbiometric()
        {/*
            if (!SDK.GetConnectState())
            {
                //SDK.sta_getBiometricType();
                int ret = SDK.sta_ConnectTCP(thisDatabase.biometricip, thisDatabase.biometricport, "0");
                if (ret == 1)
                {
                    //getDeviceInfo();
                    System.Diagnostics.Debug.WriteLine("Connected");
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("Not Connected");
                }
            }*/            
        }

        private void enter_login()
        {
            try
            {
                //connectbiometric();
                db = new thisDatabase();

                String branch = "";
                String comp = "";
                String user = "";
                Boolean ismain = false;

                if (cbo_db.SelectedIndex == -1)
                {
                    MessageBox.Show("Please the database.");
                    cbo_db.DroppedDown = true;
                }
                else if (cbo_branch.SelectedIndex == -1)
                {
                    MessageBox.Show("Please select a branch.");
                    cbo_branch.DroppedDown = true;
                }
                else
                {
                    if (ok == "ok")
                    {
                        branch = db.get_m99branch();
                        comp = db.get_m99comp_name();
                        user = db.validate_login(txt_user.Text, txt_pass.Text, comp);
                        ismain = db.get_isMainBranch();

                        if (cbo_branch.SelectedIndex != -1)
                        {
                            branch = cbo_branch.SelectedValue.ToString();
                        }
                        if (String.IsNullOrEmpty(user) == false)
                        {
                            GlobalClass.username = user;
                            GlobalClass.branch = branch;
                            GlobalClass.isMainBranch = ismain;
                            GlobalClass.server_ip = lbl_server.Text;
                            DialogResult = DialogResult.OK;
                        }
                        else
                        {
                            MessageBox.Show("Invalid username and password. Pls try again.");
                            txt_pass.Text = "";
                        }
                    }
                }
            }
            catch { }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //System.Diagnostics.Process.Start("https://web.facebook.com/RightAppsSolutions/");

            MessageBox.Show("For information, contact 0915-806-0792 / 0942-734-7599.\nAlso like us on facebook \n https://web.facebook.com/RightAppsOfficial/");
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
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
                    thisDatabase.db_name = "live_mukja";   //live_mukja   live_ocs    live_44joseph
                    GlobalClass.dbcompany = "";
                    //GlobalClass.dbcompany = "MY COMPANY";
                    gc.load_branch(cbo_branch);
                    //pbox_db.Image = Properties.Resources.durosland;
                    pbox_db.Visible = true;

                }
            }
            catch { }

            try
            {
                thisDatabase ldb = new thisDatabase();

                branch = ldb.get_m99branch();
                isMain = ldb.get_isMainBranch();

                cbo_branch.SelectedValue = branch;

                if (isMain)
                {
                    cbo_branch.Enabled = true;
                }
                else
                {
                    cbo_branch.Enabled = false;
                }
            }
            catch { }
        }

        private void lbl_test_side_Click(object sender, EventArgs e)
        {

        }
    }
}
