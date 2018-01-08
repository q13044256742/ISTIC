using System;
using System.Drawing;
using System.Windows.Forms;

namespace 科技计划项目档案数据采集管理系统.TransferOfRegistration
{
    public partial class Frm_ToR : Form
    {
        public Frm_ToR()
        {
            InitializeComponent();
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
    }
}
