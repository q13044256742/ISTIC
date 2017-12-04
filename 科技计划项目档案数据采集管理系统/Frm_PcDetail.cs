using System;
using System.Windows.Forms;

namespace 科技计划项目档案数据采集管理系统
{
    public partial class Frm_PcDetail : DevExpress.XtraEditors.XtraForm
    {
        public Frm_PcDetail()
        {
            InitializeComponent();
        }

        private void btn_Modify_Click(object sender, EventArgs e)
        {
            Frm_AddPC frm = new Frm_AddPC();
            frm.ShowDialog();
            Hide();
        }
    }
}
