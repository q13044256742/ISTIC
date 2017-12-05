using System;
using System.Drawing;
using System.Windows.Forms;

namespace 科技计划项目档案数据采集管理系统
{
    public partial class frm_Login : DevExpress.XtraEditors.XtraForm
    {
        public frm_Login() {
            InitializeComponent();
            InitalForm();
        }

        private void InitalForm()
        {
            BackColor = Color.FromArgb(255, 246, 243, 243);
        }

        private void btn_Login_Click(object sender, EventArgs e) {
            int i = cbo_Identity.SelectedIndex;
            if (i == 1)//移交登记
            {
                Frm_MainFrame fm = new Frm_MainFrame(Tools.Identity.CurrentIdentity.YJDJ);
                fm.Show();
                Hide();
            }
            else if(i == 2)//著录加工
            {
                Frm_MainFrame fm = new Frm_MainFrame(Tools.Identity.CurrentIdentity.ZLJG);
                fm.Show();
                Hide();
            }
        }

        private void frm_Login_FormClosing(object sender, System.Windows.Forms.FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}
