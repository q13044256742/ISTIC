using DevExpress.XtraBars.Navigation;
using System;
using System.Windows.Forms;

namespace 科技计划项目档案数据采集管理系统
{
    public partial class Frm_MainFrameManager : DevExpress.XtraEditors.XtraForm
    {
        public User user;
        private Form parentForm;
        public Frm_MainFrameManager(Form parentForm, User user)
        {
            this.user = user;
            this.parentForm = parentForm;
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

        private void Frm_MainFrame_FormClosing(object sender, FormClosingEventArgs e)
        {
            parentForm.Show();
            parentForm.Activate();
        }


        private void Frm_MainFrame_Load(object sender, EventArgs e)
        {
            lbl_OtherInfo.Text = $"当前时间：{DateTime.Now.Year}年{DateTime.Now.Month}月{DateTime.Now.Day}日 {System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.GetDayName(DateTime.Now.DayOfWeek)}";
            UserInfo_Click(null, null);
        }

        private void ExitSystem_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            foreach(Form item in MdiChildren)
                item.Close();
            Frm_Login frm_Login = GetFromHelper.GetLoginForm();
            frm_Login.Show();
            Hide();
        }

        private void UserGroup_Click(object sender, EventArgs e)
        {
            foreach(Form item in MdiChildren)
                item.Close();
            Frm_userGroup frm = new Frm_userGroup();
            frm.MdiParent = this;
            frm.Show();
        }

        private void UserInfo_Click(object sender, EventArgs e)
        {
            foreach(Form item in MdiChildren)
                item.Close();
            Frm_userInfo frm = new Frm_userInfo();
            frm.MdiParent = this;
            frm.Show();
        }

        private void Dictionary_Click(object sender, EventArgs e)
        {
            foreach(Form item in MdiChildren)
                item.Close();
            AccordionControlElement element = sender as AccordionControlElement;
            Manager.Frm_Manager frm = new Manager.Frm_Manager(element.Tag);
            frm.MdiParent = this;
            frm.Show();
        }

        private void Demo_Click(object sender, EventArgs e)
        {
            foreach(Form item in MdiChildren)
                item.Close();
            string element = (sender as AccordionControlElement).Name;
            //编码规则
            if("ace_CodeRule".Equals(element))
            {
                Frm_CodeRule frm = new Frm_CodeRule(UserHelper.GetUser().UserKey);
                frm.MdiParent = this;
                frm.Show();
            }
        }

        private void Ace_LoginLog_Click(object sender, EventArgs e)
        {
            foreach(Form item in MdiChildren)
                item.Close();
            Frm_LogList frm = new Frm_LogList();
            frm.MdiParent = this;
            frm.Text = "登录日志";
            frm.Show();
        }
    }
}
