using System;
using System.Drawing;
using System.Windows.Forms;
using 科技计划项目档案数据采集管理系统.TransferOfRegistration;

namespace 科技计划项目档案数据采集管理系统
{
    public partial class Frm_MainFrame : DevExpress.XtraEditors.XtraForm
    {
        public User user;
        public Frm_MainFrame(User user)
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
            }else if ("2".Equals(user.Remark))
            {
                Frm_CG frm = new Frm_CG();
                frm.MdiParent = this;
                frm.Show();
            }
        }

        private void Frm_MainFrame_FormClosing(object sender, System.Windows.Forms.FormClosingEventArgs e)
        {
            Environment.Exit(0);
        }

        private void lst_DataList_DrawItem(object sender, DrawItemEventArgs e)
        {
            e.Graphics.FillRectangle(new SolidBrush(e.BackColor), e.Bounds);
            if (e.Index >= 0)
            {
                StringFormat sStringFormat = new StringFormat();
                sStringFormat.LineAlignment = StringAlignment.Center;
                e.Graphics.DrawString(((ListBox)sender).Items[e.Index].ToString(), e.Font, new SolidBrush(e.ForeColor), e.Bounds, sStringFormat);
            }
            e.DrawFocusRectangle();
        }

        private void lst_DataList_MeasureItem(object sender, MeasureItemEventArgs e)
        {
            e.ItemHeight = e.ItemHeight + 20;
        }

        private void lbl_listTitle_MouseEnter(object sender, EventArgs e)
        {
            Label label = sender as Label;
            label.Font = new Font(label.Font, FontStyle.Underline);
            label.ForeColor = Color.Gainsboro;
        }

        private void lbl_listTitle_MouseLeave(object sender, EventArgs e)
        {
            Label label = sender as Label;
            label.Font = new Font(label.Font, FontStyle.Regular);
            label.ForeColor = Color.DimGray;
        }

        private void Frm_MainFrame_Load(object sender, EventArgs e)
        {
            lbl_OtherInfo.Text = $"当前时间：{DateTime.Now.Year}年{DateTime.Now.Month}月{DateTime.Now.Day}日 {System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.GetDayName(DateTime.Now.DayOfWeek)}";
        }

        private void lbl_ExitSystem_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Frm_Login frm_Login = new Frm_Login();
            frm_Login.Show();
            Hide();
        }
    }
}
