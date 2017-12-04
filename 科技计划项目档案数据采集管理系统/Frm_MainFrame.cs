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

        private void lbl_tzgg_Click(object sender, EventArgs e)
        {
            //通知公告（加载七条数据）
            //lbl_listTitle
            foreach (Control item in pal_FirstFrame_Bottom.Controls)
            {
                if(item is Label && !"1".Equals(item.Tag))
                {
                    pal_FirstFrame_Bottom.Controls.Remove(item);
                }
            }
            for (int i = 0; i < 7; i++)
            {
                Label label = new Label();
                label.Text = "关于“重大新药创制”和“传染病防治”科技重大专项2016年结题课题逾期未提交财务验收审计报告的公告 ";
                label.AutoSize = false;
                label.Font = lbl_listTitle.Font;
                label.BorderStyle = lbl_listTitle.BorderStyle;
                label.Top += lbl_listTitle.Top + (i * (lbl_listTitle.Height + 5));
                label.Left = lbl_listTitle.Left;
                label.Size = lbl_listTitle.Size;
                label.TextAlign = lbl_listTitle.TextAlign;
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
        //加工进度
        private void label17_Click(object sender, EventArgs e)
        {
            pal_DataList.Controls.Clear();

            //label_1
            Label l = new Label();
            l.Text = "国家高科技研究发展计划（863计划）";
            l.Size = new Size(240, 40);
            l.Top = 20;
            l.Left = 60;
            pal_DataList.Controls.Add(l);

            //picture_1
            PictureBox p = new PictureBox();
            p.Image = Resources.jidu1;
            p.Size = new Size(240, 40);
            p.Top = 50;
            p.Left = 50;
            pal_DataList.Controls.Add(p);

            //label_2
            Label l1 = new Label();
            l1.Text = "国家重点基础研究发展计划（973计划）";
            l1.Size = new Size(240, 40);
            l1.Top = 20;
            l1.Left = 350;
            pal_DataList.Controls.Add(l1);

            //picture_2
            PictureBox p1 = new PictureBox();
            p1.Image = Resources.jidu2;
            p1.Size = new Size(240, 40);
            p1.Top = 50;
            p1.Left = 350;
            pal_DataList.Controls.Add(p1);

            //label_3
            Label l2 = new Label();
            l2.Text = "国家科技支撑计划 ";
            l2.Size = new Size(240, 40);
            l2.Top = 20;
            l2.Left = 680;
            pal_DataList.Controls.Add(l2);

            //picture_3
            PictureBox p2 = new PictureBox();
            p2.Image = Resources.jidu3;
            p2.Size = new Size(240, 40);
            p2.Top = 50;
            p2.Left = 670;
            pal_DataList.Controls.Add(p2);

            //label_4
            Label l3 = new Label();
            l3.Text = "星火计划 ";
            l3.Size = new Size(240, 40);
            l3.Top = 100;
            l3.Left = 80;
            pal_DataList.Controls.Add(l3);

            //picture_4
            PictureBox p3 = new PictureBox();
            p3.Image = Resources.jidu1;
            p3.Size = new Size(240, 40);
            p3.Top = 130;
            p3.Left = 50;
            pal_DataList.Controls.Add(p3);

            //label_5
            Label l4 = new Label();
            l4.Text = "火炬计划  ";
            l4.Size = new Size(240, 40);
            l4.Top = 100;
            l4.Left = 370;
            pal_DataList.Controls.Add(l4);

            //picture_5
            PictureBox p4 = new PictureBox();
            p4.Image = Resources.jidu2;
            p4.Size = new Size(240, 40);
            p4.Top = 130;
            p4.Left = 350;
            pal_DataList.Controls.Add(p4);

            //label_6
            Label l5 = new Label();
            l5.Text = "重点新产品  ";
            l5.Size = new Size(240, 40);
            l5.Top = 100;
            l5.Left = 700;
            pal_DataList.Controls.Add(l5);

            //picture_6
            PictureBox p5 = new PictureBox();
            p5.Image = Resources.jidu3;
            p5.Size = new Size(240, 40);
            p5.Top = 130;
            p5.Left = 670;
            pal_DataList.Controls.Add(p5);
        }
    }
}
