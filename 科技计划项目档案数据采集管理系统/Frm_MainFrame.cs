using System;
using System.Drawing;
using System.Windows.Forms;

namespace 科技计划项目档案数据采集管理系统
{
    public partial class Frm_MainFrame : DevExpress.XtraEditors.XtraForm
    {
        public Frm_MainFrame() {
            InitializeComponent();
            InitalForm();
        }

        private void InitalForm()
        {
            foreach (Control item in pal_LeftMenu.Controls)
            {
                if(item is Panel)
                {
                    item.MouseEnter += Item_MouseEnter;
                    item.MouseLeave += Item_MouseLeave;
                }
            }
            //默认打开移交登记首页
            Frm_YJDJ_FirstFrame fyf = new Frm_YJDJ_FirstFrame();
            fyf.MdiParent = this;
            fyf.Show();
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



        private void Frm_MainFrame_FormClosing(object sender, System.Windows.Forms.FormClosingEventArgs e) {
            Application.Exit();
        }

        /// <summary>
        /// 移交登记
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Pal_YJDJ_Click(object sender, EventArgs e)
        {
            
        }
    }
}
