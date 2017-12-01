using System;

namespace 科技计划项目档案数据采集管理系统
{
    public partial class frm_Login : DevExpress.XtraEditors.XtraForm
    {
        public frm_Login() {
            InitializeComponent();
        }

        private void btn_Login_Click(object sender, EventArgs e) {
            int i = cbo_Identity.SelectedIndex;
            i = 1;
            ////移交登记
            if(i == 1)
            {
                Frm_MainFrame fm = new Frm_MainFrame();
                fm.Show();
                Hide();
            }
        }
    }
}
