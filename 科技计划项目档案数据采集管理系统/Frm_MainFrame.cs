using System.Windows.Forms;

namespace 科技计划项目档案数据采集管理系统
{
    public partial class Frm_MainFrame : DevExpress.XtraEditors.XtraForm
    {
        public Frm_MainFrame() {
            InitializeComponent();
        }
       
        private void Frm_MainFrame_FormClosing(object sender, System.Windows.Forms.FormClosingEventArgs e) {
            Application.Exit();
        }
    }
}
