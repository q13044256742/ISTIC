using System;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using 科技计划项目档案数据采集管理系统.Properties;
using static 科技计划项目档案数据采集管理系统.Tools.Identity;

namespace 科技计划项目档案数据采集管理系统
{
    public partial class Frm_MainFrame : DevExpress.XtraEditors.XtraForm
    {
        public Frm_MainFrame(CurrentIdentity identity)
        {
            InitializeComponent();
            InitalForm(identity);
        }

        Panel _panel_log = null;
        Panel _panel_word = null;

        /// <summary>
        /// 初始化界面数据
        /// </summary>
        /// <param name="identity">当前登录人身份</param>
        private void InitalForm(CurrentIdentity identity)
        {
            //生成全部菜单项
            Panel[] panels = GetPanelList(identity);
            int flag = 0;
            if(identity == CurrentIdentity.YJDJ)
            {
                for (int i = 0; i < panels.Length; i++)
                {
                    if (!"采集加工".Equals(panels[i].Name))
                    {
                        panels[i].Location = new Point(-1, pal_XTSY.Top + pal_XTSY.Height + (flag++ * panels[i].Height));
                        pal_LeftMenu.Controls.Add(panels[i]);
                    }
                }
            }else if(identity == CurrentIdentity.ZLJG)
            {
                for (int i = 0; i < panels.Length; i++)
                {
                    if ("采集加工".Equals(panels[i].Name) || "查询借阅".Equals(panels[i].Name))
                    {
                        panels[i].Location = new Point(-1, pal_XTSY.Top + pal_XTSY.Height + (flag++ * panels[i].Height));
                        pal_LeftMenu.Controls.Add(panels[i]);
                    }
                }
            }

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

        /// <summary>
        /// 获取左侧菜单栏列表
        /// </summary>
        /// <returns></returns>
        private Panel[] GetPanelList(CurrentIdentity identity)
        {
            string filePath = Application.StartupPath + "/Datas/gncd.txt";
            Image[] imgs = new Image[] {Resources.pic1, Resources.pic2, Resources.pic3, Resources.pic4, Resources.pic5, Resources.pic6, Resources.pic7 };
            if (File.Exists(filePath))
            {
                string[] list = File.ReadAllLines(filePath, Encoding.UTF8);
                Panel[] ps = new Panel[list.Length];
                for (int i = 0; i < list.Length; i++)
                {
                    Panel panel = new Panel();
                    panel.Size = new Size(282, 53);
                    panel.BorderStyle = BorderStyle.FixedSingle;
                    panel.Tag = "a";
                    panel.BackColor = Color.Transparent;
                    panel.Name = list[i];

                    Label label = new Label();
                    label.Font = new Font("Tahoma", 11f);
                    label.Size = new Size(107, 36);
                    label.Location = new Point(62, 8);
                    label.ForeColor = Color.White;
                    label.ImageAlign = ContentAlignment.MiddleLeft;
                    label.TextAlign = ContentAlignment.MiddleRight;
                    label.Text = list[i];
                    label.Image = imgs[i];

                    if ("移交登记".Equals(list[i].Trim()))
                    {
                        panel.Click += Panel_Click;
                    }
                    else if ("采集加工".Equals(list[i].Trim()))
                    {
                        if (identity == CurrentIdentity.ZLJG)
                        {
                            panel.Click += ZLJG_Click;
                        }
                    }
                    panel.Controls.Add(label);
                    ps[i] = panel;
                }
                return ps;
            }
            return null;
        }

        /// <summary>
        /// 著录加工 - 点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ZLJG_Click(object sender, EventArgs e)
        {
            Control control = sender as Control;
            _panel_work = _panel_work ?? GetWorkList();
            if (_panel_work != null)
            {
                if (_panel_work.Visible)
                {
                    _panel_work.Visible = false;
                    foreach (Control item in pal_LeftMenu.Controls)
                    {
                        if ("a".Equals(item.Tag) && control.Top < item.Top)
                        {
                            item.Top -= _panel_work.Height;
                        }
                    }
                }
                else
                {
                    _panel_work.Visible = true;

                    int top = control.Top + control.Height;
                    int left = control.Left;
                    _panel_work.Location = new Point(left, top);
                    foreach (Control item in pal_LeftMenu.Controls)
                    {
                        if ("a".Equals(item.Tag) && control.Top < item.Top)
                        {
                            item.Top += _panel_work.Height;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 获取著录加工下拉菜单
        /// </summary>
        /// <returns></returns>
        private Panel GetWorkList()
        {
            Panel pal_Group = new Panel();
            pal_Group.Visible = false;
            int height = 0;
            string filePath = Application.StartupPath + "/Datas/zljg.txt";
            if (File.Exists(filePath))
            {
                string[] cl = File.ReadAllLines(filePath, Encoding.UTF8);
                for (int i = 0; i < cl.Length; i++)
                {
                    Panel pal_Com = new Panel();
                    pal_Com.Size = new Size(287, 33);
                    pal_Com.Location = new Point(-1, i * pal_Com.Height);
                    pal_Com.BorderStyle = BorderStyle.FixedSingle;

                    Label label = new Label();
                    label.AutoSize = true;
                    label.Text = cl[i];
                    label.Location = new Point(85, 8);
                    label.Font = new Font("微软雅黑", 9f);
                    label.MouseEnter += Panel_MouseEnter;
                    label.MouseLeave += Panel_MouseLeave;
                    pal_Com.Controls.Add(label);

                    pal_Com.MouseEnter += Panel_MouseEnter;
                    pal_Com.MouseLeave += Panel_MouseLeave;
                    pal_Group.Controls.Add(pal_Com);
                    height += pal_Com.Height;

                    if ("加工登记".Equals(cl[i].Trim()))
                        pal_Com.Click += Pal_Com_Click;
                    else if ("加工中".Equals(cl[i].Trim()))
                    {

                    }

                }
            }
            pal_Group.Size = new Size(282, height);

            pal_LeftMenu.Controls.Add(pal_Group);

            return pal_Group;
        }

        /// <summary>
        /// 采集加工 - 我的加工事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Pal_Com_Click(object sender, EventArgs e)
        {
            pal_FirstFrame.Hide();
            Frm_ZLJG_FirstFrame frm = new Frm_ZLJG_FirstFrame();
            frm.MdiParent = this;
            frm.Show();
        }

        Panel _panel_work = null;
        /// <summary>
        /// 移交登记/档案接收 - 列出来源单位事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Panel_Click(object sender, EventArgs e)
        {
            Control control = sender as Control;
            Panel panel = null;
            if ("移交登记".Equals(control.Name))
            {
                if (_panel_word != null && _panel_word.Visible)
                    return;
                panel = _panel_log = _panel_log == null ? GetCompanyList(1) : _panel_log;
            }
            else if ("档案加工".Equals(control.Name))
                panel = _panel_word = _panel_word == null ? GetCompanyList(2) : _panel_word;
            if (panel != null)
            {
                if (panel.Visible)
                {
                    panel.Visible = false;
                    foreach (Control item in pal_LeftMenu.Controls)
                    {
                        if ("a".Equals(item.Tag) && control.Top < item.Top)
                        {
                            item.Top -= panel.Height;
                        }
                    }
                }
                else
                {
                    int top = control.Top + control.Height;
                    int left = control.Left;
                    panel.Location = new Point(left, top);
                    panel.Visible = true;

                    foreach (Control item in pal_LeftMenu.Controls)
                    {
                        if ("a".Equals(item.Tag) && control.Top < item.Top)
                        {
                            item.Top += panel.Height;
                        }
                    }
                }
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

        /// <summary>
        /// 生成单位列表
        /// </summary>
        /// <param name="index">1）移交登记 2）档案接收</param>
        /// <returns></returns>
        private Panel GetCompanyList(int index)
        {
            Panel pal_Group = new Panel();
            pal_Group.Visible = false;
            int height = 0;
            string filePath = Application.StartupPath + "/Datas/companyList.txt";
            if (File.Exists(filePath))
            {
                string[] cl = File.ReadAllLines(filePath, Encoding.UTF8);
                for (int i = 0; i < cl.Length; i++)
                {
                    Panel pal_Com = new Panel();
                    pal_Com.Size = new Size(287, 33);
                    pal_Com.Location = new Point(-1, i * pal_Com.Height);
                    pal_Com.BorderStyle = BorderStyle.FixedSingle;

                    Label label = new Label();
                    label.AutoSize = true;
                    label.Text = cl[i];
                    label.Location = new Point(85, 8);
                    label.Font = new Font("微软雅黑", 9f);
                    label.MouseEnter += Panel_MouseEnter;
                    label.MouseLeave += Panel_MouseLeave;
                    if (index == 1)
                    {
                        label.Click += Label_Click;
                        pal_Com.Click += Label_Click;
                    }
                    pal_Com.Controls.Add(label);

                    pal_Com.MouseEnter += Panel_MouseEnter;
                    pal_Com.MouseLeave += Panel_MouseLeave;
                    pal_Group.Controls.Add(pal_Com);
                    height += pal_Com.Height;
                }
            }
            pal_Group.Size = new Size(282, height);

            pal_LeftMenu.Controls.Add(pal_Group);

            return pal_Group;
        }

        /// <summary>
        /// 移交登记（全部来源单位） - 来源单位列表点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Label_Click(object sender, EventArgs e)
        {
            pal_FirstFrame.Hide();
            Frm_YJDJ_FirstFrame frm_YJDJ_First = new Frm_YJDJ_FirstFrame();
            frm_YJDJ_First.MdiParent = this;
            frm_YJDJ_First.Show();
        }

        private void Panel_MouseLeave(object sender, EventArgs e)
        {

            if (sender is Label)
            {
                (sender as Label).Parent.BackColor = Color.Transparent;
            }
            else if (sender is Panel)
            {
                (sender as Panel).BackColor = Color.Transparent;
            }
        }

        private void Panel_MouseEnter(object sender, EventArgs e)
        {
            if (sender is Label)
            {
                (sender as Label).Parent.BackColor = Color.FromArgb(255, 199, 237, 204);
            }
            else if (sender is Panel)
            {
                (sender as Panel).BackColor = Color.FromArgb(255, 199, 237, 204);
            }
        }

        private void lbl_ExitLogin_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (MessageBox.Show("确认注销当前登录用户吗?", "确认提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk) == DialogResult.OK)
            {
                frm_Login frm = new frm_Login();
                frm.Show();
                Hide();
            }
        }
    }
}
