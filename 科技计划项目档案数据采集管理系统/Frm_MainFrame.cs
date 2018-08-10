using System;
using System.Drawing;
using System.Windows.Forms;

namespace 科技计划项目档案数据采集管理系统
{
    public partial class Frm_MainFrame : DevExpress.XtraEditors.XtraForm
    {
        private Form parentForm;
        private Form subForm;
        public Frm_MainFrame(Form parentForm, Form subForm)
        {
            this.parentForm = parentForm;
            this.subForm = subForm;
            InitializeComponent();
            InitalForm();
        }

        private void InitalForm()
        {
            //当前登录人信息
            txt_RealName.Text = UserHelper.GetUser().RealName;
            subForm.MdiParent = this;
            subForm.WindowState = FormWindowState.Maximized;
            subForm.Show();
        }

        private void Frm_MainFrame_FormClosing(object sender, FormClosingEventArgs e)
        {
            parentForm.Show();
            parentForm.Activate();
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

        private void lbl_ExitSystem_Click(object sender, EventArgs e)
        {
            Frm_Login frm_Login = GetFromHelper.GetLoginForm();
            frm_Login.Show();
            Hide();
        }
    }
}
