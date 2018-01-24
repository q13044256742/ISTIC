using System;
using System.Drawing;
using System.Windows.Forms;
using 科技计划项目档案数据采集管理系统.Properties;

namespace 科技计划项目档案数据采集管理系统
{
    public partial class Frm_Login : DevExpress.XtraEditors.XtraForm
    {
        public Frm_Login() {
            InitializeComponent();
            InitalForm();
        }

        private void InitalForm()
        {
            BackColor = Color.FromArgb(255, 246, 243, 243);
        }

        private void btn_Login_Click(object sender, EventArgs e) {
            string loginName = txt_loginName.Text.Trim();
            string loginPassword = txt_loginPassword.Text.Trim();
            if (!string.IsNullOrEmpty(loginName) && !string.IsNullOrEmpty(loginPassword))
            {
                UserLogin userLogin = new UserLogin();
                bool result = userLogin.IsExist(new User(loginName, loginPassword));
                if (result)
                {
                    User user = userLogin.GetUser(loginName, loginPassword);
                    int i = cbo_Identity.SelectedIndex;
                    user.Remark = i.ToString();

                    Frm_MainFrame fm = new Frm_MainFrame(user);
                    fm.WindowState = FormWindowState.Maximized;
                    fm.Show();
                    Hide();
                }
                else
                {
                    MessageBox.Show("用户名或密码错误！");
                }
            }else
            {
                MessageBox.Show("用户名和密码不能为空!");
            }
        }

        private void frm_Login_FormClosing(object sender, System.Windows.Forms.FormClosingEventArgs e) {
            Application.Exit();
        }
    }
}
