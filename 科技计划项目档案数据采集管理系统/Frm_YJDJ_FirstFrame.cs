using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace 科技计划项目档案数据采集管理系统
{
    public partial class Frm_YJDJ_FirstFrame : DevExpress.XtraEditors.XtraForm
    {
        public Frm_YJDJ_FirstFrame()
        {
            InitializeComponent();
            InitialForm();
        }

        private void InitialForm()
        {
            //表头设置
            dgv_SWDJ.ColumnHeadersDefaultCellStyle.Font = new Font("微软雅黑", 10f, FontStyle.Bold);
            dgv_SWDJ.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv_SWDJ.ColumnHeadersDefaultCellStyle.BackColor = Color.AliceBlue;

            //列设置
            dgv_SWDJ.DefaultCellStyle.Font = new Font("微软雅黑", 9f);

            //加载数据
            DataTable dataTable = new DataTable();
            string[] texts = File.ReadAllLines(Application.StartupPath + "/Datas/swdj.txt");
            for (int k = 0; k < texts.Length; k++)
            {
                int rid = dgv_SWDJ.Rows.Add();
                string[] infos = texts[k].Split(',');
                for (int i = 0; i < infos.Length; i++)
                {
                    //不居中
                    if (i != 0 && i != 1)
                    {
                        dgv_SWDJ.Rows[rid].Cells[i].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    }
                    dgv_SWDJ.Rows[rid].Cells[i].Value = infos[i].Replace("\"", string.Empty).Replace("[", string.Empty).Replace("]", string.Empty);
                }
            }

        }

        private void Dgv_SWDJ_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                if (e.ColumnIndex == 1)
                {
                    MessageBox.Show("批次名称：" + dgv_SWDJ.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);
                }
                else if (e.ColumnIndex == 5)
                {
                    MessageBox.Show("光盘数：" + dgv_SWDJ.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);
                }
            }
        }

        private void Dgv_SWDJ_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1 && (e.ColumnIndex == 1 || e.ColumnIndex == 5))
            {
                dgv_SWDJ.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.ForeColor = Color.Blue;
            }

        }

        private void Dgv_SWDJ_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1 && (e.ColumnIndex == 1 || e.ColumnIndex == 5))
            {
                dgv_SWDJ.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.ForeColor = Color.Black;
            }
        }

        private void btn_Find_MouseEnter(object sender, System.EventArgs e)
        {
            Cursor = Cursors.Hand;
        }

        private void btn_Find_MouseLeave(object sender, System.EventArgs e)
        {
            Cursor = Cursors.Default;
        }

        private void btn_Add_Click(object sender, System.EventArgs e)
        {
            Frm_AddPC frm = new Frm_AddPC();
            frm.ShowDialog();
        }
    }
}
