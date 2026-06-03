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
    public partial class p_OtherDeductionEntry : Form
    {
        GlobalClass gc = new GlobalClass();
        GlobalMethod gm = new GlobalMethod();
        thisDatabase db = new thisDatabase();
        Boolean yah = false;
        public p_OtherDeductionEntry()
        {
            InitializeComponent();
            gc.load_payroll_period(cbo_payroll_period);
            gc.load_other_deductions(cbo_earnings_code);

            try { cbo_earnings_code.SelectedIndex = 0; }
            catch { cbo_earnings_code.SelectedIndex = -1; }

            try { cbo_payroll_period.SelectedIndex = 0; }
            catch { cbo_payroll_period.SelectedIndex = -1; }

            yah = true;
            disp_list();
        }

        private void btn_new_Click(object sender, EventArgs e)
        {
            p_AddOtherDeductionsEntry frm = new p_AddOtherDeductionsEntry(this, "", true);
            frm.ShowDialog();
            disp_list();
        }
        public void disp_list()
        {
            DataTable dt = null;
            dgv_list.Rows.Clear();

            dt = db.QueryBySQLCode("SELECT de.*,od.description AS deduction FROM rssys.hr_deduction_entry  de LEFT JOIN rssys.hr_other_deductions od ON de.deduction_code=od.code ORDER BY de.dedcode ASC");
            for (int r = 0; r < dt.Rows.Count; r++)
            {
                int i = dgv_list.Rows.Add();    
                DataGridViewRow row = dgv_list.Rows[i];

                row.Cells["dgvl_code"].Value = dt.Rows[r]["dedcode"].ToString();
                row.Cells["dgvl_emp_no"].Value = dt.Rows[r]["emp_no"].ToString();
                row.Cells["dgvl_emp_name"].Value = dt.Rows[r]["emp_name"].ToString();
                row.Cells["dgvl_amount"].Value = dt.Rows[r]["amount"].ToString();
                row.Cells["dgvl_payroll_code"].Value = dt.Rows[r]["payroll_period"].ToString();
                row.Cells["dgvl_deduction"].Value = dt.Rows[r]["deduction"].ToString();
            }
        }

        public void disp_list2()
        {
            DataTable dt = null;
            dgv_list.Rows.Clear();
            dt = db.QueryBySQLCode("SELECT de.*,od.description AS deduction FROM rssys.hr_deduction_entry  de LEFT JOIN rssys.hr_other_deductions od ON de.deduction_code=od.code WHERE payroll_period='" + cbo_payroll_period.SelectedValue.ToString() + "' AND deduction_code='" + cbo_earnings_code.SelectedValue.ToString() + "' ORDER BY de.dedcode");
            for (int r = 0; r < dt.Rows.Count; r++)
            {
                int i = dgv_list.Rows.Add();
                DataGridViewRow row = dgv_list.Rows[i];

                row.Cells["dgvl_code"].Value = dt.Rows[r]["dedcode"].ToString();
                row.Cells["dgvl_emp_no"].Value = dt.Rows[r]["emp_no"].ToString();
                row.Cells["dgvl_emp_name"].Value = dt.Rows[r]["emp_name"].ToString();
                row.Cells["dgvl_amount"].Value = dt.Rows[r]["amount"].ToString();
                row.Cells["dgvl_payroll_code"].Value = dt.Rows[r]["payroll_period"].ToString();
                row.Cells["dgvl_deduction"].Value = dt.Rows[r]["deduction"].ToString();
            }
        }

        private void btn_upd_Click(object sender, EventArgs e)
        {
            int r = 0;
            String code;
            if (dgv_list.Rows.Count > 1)
            {
                try {
                    r = dgv_list.CurrentRow.Index;
                    code = dgv_list["dgvl_code", r].Value.ToString();
                    p_AddOtherDeductionsEntry frm = new p_AddOtherDeductionsEntry(this, code, false);
                    frm.ShowDialog();
                    disp_list();
                }
                catch
                {
                    MessageBox.Show("No selected item.");
                    return;
                }
               
            }
            else
            {

                MessageBox.Show("No records to be selected.");
            }
        }

        private void cbo_payroll_period_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (yah)
                disp_list2();
        }

        private void cbo_earnings_code_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (yah)
                disp_list2();
        }

        private void p_OtherDeductionEntry_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            disp_list();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            int r = 0;
            String code;
            if (dgv_list.Rows.Count > 1)
            {
                r = dgv_list.CurrentRow.Index;
                code = dgv_list["dgvl_code", r].Value.ToString();
                DialogResult result = MessageBox.Show("Are you sure you want to cancel this entry?", "Confirmation", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    db.QueryBySQLCode("DELETE FROM rssys.hr_deduction_entry WHERE dedcode = '" + code + "'");
                    disp_list();
                }
                
            }
            else
            {
                MessageBox.Show("No records to be selected.");
            }
        }

        private void btn_print_Click(object sender, EventArgs e)
        {
            int r = 0;
            String code,pcode;
            if (dgv_list.Rows.Count > 1)
            {
                try
                {
                    Double deduc = 0.00;
                    Double deducsum = 0.00;
                    r = dgv_list.CurrentRow.Index;
                    code = dgv_list["dgvl_emp_no", r].Value.ToString();
                    pcode = dgv_list["dgvl_payroll_code", r].Value.ToString();
                    deduc = gm.toNormalDoubleFormat(dgv_list["dgvl_amount", r].Value.ToString());
                    DataTable d = db.QueryBySQLCode("SELECT other_deduction FROM rssys.hr_emp_payroll WHERE empid = '" + code + "' AND ppid = '" + pcode + "'");
                    DataTable dsum = db.QueryBySQLCode("SELECT SUM(amount) as amount FROM rssys.hr_deduction_entry WHERE emp_no = '"+code+"' AND payroll_period = '"+pcode+"'");
                    deducsum = gm.toNormalDoubleFormat(dsum.Rows[r]["amount"].ToString());
                    if(d.Rows.Count>0){
                        if (deducsum != gm.toNormalDoubleFormat(d.Rows[r]["other_deduction"].ToString()))
                        {
                            deduc += gm.toNormalDoubleFormat(d.Rows[r]["other_deduction"].ToString());
                            db.UpdateOnTable("hr_emp_payroll", "other_deduction='" + deduc.ToString() + "'", "empid = '" + code + "' AND ppid = '" + pcode + "'");
                            MessageBox.Show("Successfully add to payroll");
                        }
                        else {
                            MessageBox.Show("Already add to payroll");
                        }

                    }
                }
                catch
                {
                    MessageBox.Show("No selected item.");
                    return;
                }

            }
            else
            {

                MessageBox.Show("No records to be selected.");
            }
        }
    }
}
