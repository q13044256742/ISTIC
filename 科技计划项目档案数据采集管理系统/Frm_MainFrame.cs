using System;
using System.Drawing;
using System.Windows.Forms;
using 科技计划项目档案数据采集管理系统.Properties;

namespace 科技计划项目档案数据采集管理系统
{
    public partial class Frm_MainFrame : DevExpress.XtraEditors.XtraForm
    {
        public Frm_MainFrame()
        {
            InitializeComponent();
            InitalForm();
        }

        /// <summary>
        /// 初始化界面数据
        /// </summary>
        private void InitalForm()
        {
            //左侧菜单栏添加动态效果
            foreach (Control item in pal_LeftMenu.Controls)
            {
                if (item is Panel)
                {
                    item.MouseEnter += Item_MouseEnter;
                    item.MouseLeave += Item_MouseLeave;
                }
            }
            //下部分tab动态效果
            foreach (Control item in pal_TabMenu.Controls)
            {
                if (item is Label && "1".Equals(item.Tag))
                {

                    item.MouseEnter += Item_MouseEnter1;
                    item.MouseLeave += Item_MouseLeave1;
                }
            }

            //工作动态（加载七条数据）
            //lbl_listTitle
            for (int i = 0; i < 7; i++)
            {
                Label label = new Label();
                label.AutoSize = false;
                label.Font = lbl_listTitle.Font;
                label.BorderStyle = lbl_listTitle.BorderStyle;
                label.Top += lbl_listTitle.Top + (i * (lbl_listTitle.Height + 5));
                label.Left = lbl_listTitle.Left;
                label.Size = lbl_listTitle.Size;
                label.TextAlign = lbl_listTitle.TextAlign;
                label.Text = lbl_listTitle.Text;
                label.Cursor = lbl_listTitle.Cursor;
                label.ForeColor = lbl_listTitle.ForeColor;
                label.MouseEnter += lbl_listTitle_MouseEnter;
                label.MouseLeave += lbl_listTitle_MouseLeave;
                pal_FirstFrame_Bottom.Controls.Add(label);

                Label date = new Label();
                date.AutoSize = false;
                date.Font = lbl_listDate.Font;
                date.BorderStyle = lbl_listDate.BorderStyle;
                date.Top += lbl_listDate.Top + (i * (lbl_listDate.Height + 5));
                date.Left = lbl_listDate.Left;
                date.Size = lbl_listDate.Size;
                date.TextAlign = lbl_listDate.TextAlign;
                date.Text = lbl_listDate.Text;
                date.ForeColor = lbl_listDate.ForeColor;
                pal_FirstFrame_Bottom.Controls.Add(date);
            }

        }

        private void Item_MouseLeave1(object sender, EventArgs e)
        {
            Label label = sender as Label;
            label.Image = null;
        }

        private void Item_MouseEnter1(object sender, EventArgs e)
        {
            Label label = sender as Label;
            label.Image = Resources.current2;
        }

        private void Item_MouseEnter(object sender, EventArgs e)
        {
            Panel panel = sender as Panel;
            panel.BackColor = Color.FromArgb(255, 70, 149, 208);
            Cursor = Cursors.Hand;
        }
        private void Item_MouseLeave(object sender, EventArgs e)
        {
            Panel panel = sender as Panel;
            panel.BackColor = Color.Transparent;
            Cursor = Cursors.Default;
        }



        private void Frm_MainFrame_FormClosing(object sender, System.Windows.Forms.FormClosingEventArgs e)
        {
            Application.Exit();
        }

        /// <summary>
        /// 移交登记
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Pal_YJDJ_Click(object sender, EventArgs e)
        {
            pal_FirstFrame.Hide();
            Frm_YJDJ_FirstFrame frm_YJDJ_First = new Frm_YJDJ_FirstFrame();
            frm_YJDJ_First.MdiParent = this;
            frm_YJDJ_First.Show();
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
    }
}
