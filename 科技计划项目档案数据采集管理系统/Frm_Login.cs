using DevExpress.XtraEditors;
using System;
using System.Drawing;
using System.Windows.Forms;
using 科技计划项目档案数据采集管理系统.FirstPage;

namespace 科技计划项目档案数据采集管理系统
{
    public partial class Frm_Login : XtraForm
    {
        public Frm_Login() {
            InitializeComponent();
            InitalForm();
        }

        private void InitalForm()
        {
            BackColor = Color.FromArgb(255, 246, 243, 243);
        }

        private void btn_Login_Click(object sender, EventArgs e)
        {
            string loginName = txt_loginName.Text.Trim();
            string loginPassword = txt_loginPassword.Text.Trim();
            if(!string.IsNullOrEmpty(loginName) && !string.IsNullOrEmpty(loginPassword))
            {
                UserLogin userLogin = new UserLogin();
                bool result = userLogin.IsExist(new User(loginName, loginPassword));
                if(result)
                {
                    User user = userLogin.GetUser(loginName, loginPassword);
                    UserHelper.SetUser(user);
                    UserHelper.SetLogin(true);
                    Hide();
                    Frm_FirstPage frm = GetFormHelper.GetFirstPage(this);
                    frm.Show();
                    frm.Activate();
                }
                else
                    XtraMessageBox.Show("用户名或密码错误。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
                XtraMessageBox.Show("用户名和密码不能为空。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void Frm_Login_FormClosing(object sender, System.Windows.Forms.FormClosingEventArgs e) {
            Application.Exit();
        }

        private void Txt_loginPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
                btn_Login_Click(null, null);
        }
    }
}
