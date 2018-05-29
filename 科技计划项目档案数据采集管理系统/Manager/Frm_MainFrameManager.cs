using DevExpress.XtraBars.Navigation;
using System;
using System.Windows.Forms;
using 科技计划项目档案数据采集管理系统.TransferOfRegistration;

namespace 科技计划项目档案数据采集管理系统
{
    public partial class Frm_MainFrameManager : DevExpress.XtraEditors.XtraForm
    {
        public User user;
        public Frm_MainFrameManager(User user)
        {
            this.user = user;
            InitializeComponent();
            InitalForm();
        }

        private void InitalForm()
        {
            //当前登录人信息
            txt_RealName.Text = user.RealName;
            if("1".Equals(user.Remark))
            {
                Frm_ToR frm = new Frm_ToR();
                frm.MdiParent = this;
                frm.Show();
            }
            else if ("2".Equals(user.Remark))
            {
                Frm_CG frm = new Frm_CG();
                frm.MdiParent = this;
                frm.Show();
            }
        }

        private void Frm_MainFrame_FormClosing(object sender, System.Windows.Forms.FormClosingEventArgs e)
        {
            Application.Exit();
        }


        private void Frm_MainFrame_Load(object sender, EventArgs e)
        {
            lbl_OtherInfo.Text = $"当前时间：{DateTime.Now.Year}年{DateTime.Now.Month}月{DateTime.Now.Day}日 {System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.GetDayName(DateTime.Now.DayOfWeek)}";
            UserInfo_Click(null, null);
        }

        private void ExitSystem_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Frm_Login frm_Login = new Frm_Login();
            frm_Login.Show();
            Hide();
        }

        private void UserGroup_Click(object sender, EventArgs e)
        {
            Frm_userGroup frm = new Frm_userGroup();
            frm.MdiParent = this;
            frm.Show();
        }

        private void UserInfo_Click(object sender, EventArgs e)
        {
            Frm_userInfo frm = new Frm_userInfo();
            frm.MdiParent = this;
            frm.Show();
        }

        private void Dictionary_Click(object sender, EventArgs e)
        {
            AccordionControlElement element = sender as AccordionControlElement;
            Manager.Frm_Manager frm = new Manager.Frm_Manager(element.Tag);
            frm.MdiParent = this;
            frm.Show();
        }

        private void Demo_Click(object sender, EventArgs e)
        {
            Control control = null;
            if(sender is Panel)
                control = sender as Control;
            else
                control = (sender as Control).Parent;

            if(!string.IsNullOrEmpty(control.Name))
            {
                Manager.Frm_template frm_Template = new Manager.Frm_template(control.Name);
                frm_Template.MdiParent = this;
                frm_Template.Show();
            }
        }
    }
}
