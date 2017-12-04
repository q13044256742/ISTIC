using System;
using System.Drawing;
using System.Windows.Forms;

namespace 科技计划项目档案数据采集管理系统
{
    public partial class Frm_AddPC : DevExpress.XtraEditors.XtraForm
    {
        public Frm_AddPC()
        {
            InitializeComponent();
        }

        private void btn_UploadFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            DialogResult result = dialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                txt_UploadFile.Text = dialog.FileName;
            }
        }

        private void Frm_AddPC_Load(object sender, EventArgs e)
        {
            //表头设置
            dgv_CDlist.ColumnHeadersDefaultCellStyle.Font = new Font("微软雅黑", 10f, FontStyle.Bold);
            dgv_CDlist.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv_CDlist.ColumnHeadersDefaultCellStyle.BackColor = Color.AliceBlue;

            //列设置
            dgv_CDlist.DefaultCellStyle.Font = new Font("微软雅黑", 9f);
        }

        private void btn_Save_MouseEnter(object sender, EventArgs e)
        {
            Cursor = Cursors.Hand;
        }

        private void btn_Save_MouseLeave(object sender, EventArgs e)
        {
            Cursor = Cursors.Default;
        }

        private void Frm_AddPC_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("请确保所有数据已保存,是否继续关闭！", "关闭确认", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) != DialogResult.OK)
            {
                e.Cancel = true;
            }
        }
    }
}
