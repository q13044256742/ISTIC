using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using 科技计划项目档案数据采集管理系统.Properties;
using 科技计划项目档案数据采集管理系统.Tools;

namespace 科技计划项目档案数据采集管理系统
{
    public partial class Frm_YJDJ_FirstFrame : DevExpress.XtraEditors.XtraForm
    {
        public Frm_YJDJ_FirstFrame() {
            InitializeComponent();
            InitialForm();
        }

        protected void InitialForm() {
            //表头设置
            dgv_SWDJ.ColumnHeadersDefaultCellStyle.Font = new Font("微软雅黑", 10f, FontStyle.Bold);
            dgv_SWDJ.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv_SWDJ.ColumnHeadersDefaultCellStyle.BackColor = Color.AliceBlue;

            //表头设置
            dgv_GPDJ.ColumnHeadersDefaultCellStyle.Font = new Font("微软雅黑", 10f, FontStyle.Bold);
            dgv_GPDJ.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv_GPDJ.ColumnHeadersDefaultCellStyle.BackColor = Color.AliceBlue;

            //列设置
            dgv_SWDJ.DefaultCellStyle.Font = new Font("微软雅黑", 9f);
            dgv_GPDJ.DefaultCellStyle.Font = new Font("微软雅黑", 9f);
            //加载数据
            dgv_SWDJ.DataSource = GetTableByIndex(0);

            if (File.Exists(Application.StartupPath + "/Datas/gpdj.txt"))
            {
                string[] texts = File.ReadAllLines(Application.StartupPath + "/Datas/gpdj.txt");
                for (int k = 0; k < texts.Length; k++)
                {
                    int rid = dgv_GPDJ.Rows.Add();
                    string[] infos = texts[k].Split(',');
                    for (int i = 0; i < infos.Length; i++)
                    {
                        //不居中
                        if (i != 0 && i != 1)
                        {
                            dgv_GPDJ.Rows[rid].Cells[i].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        }
                        dgv_GPDJ.Rows[rid].Cells[i].Value = infos[i].Replace("\"", string.Empty).Replace("[", string.Empty).Replace("]", string.Empty);
                    }
                }
            }
            //隐藏返回按钮
            btn_Back.Hide();
        }

        //表格点击事件
        protected internal void Dgv_SWDJ_CellContentClick(object sender, DataGridViewCellEventArgs e) {
            if (e.RowIndex != -1)
            {
                if (e.ColumnIndex == 1)
                {
                    MessageBox.Show("批次名称：" + dgv_SWDJ.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);
                }
                else if (e.ColumnIndex == 5)
                {
                    if (File.Exists(Application.StartupPath + "/Datas/gplb.txt"))
                    {
                        DataTable dt = new DataTable();
                        dt.Columns.Add(new DataColumn("光盘名称"));
                        dt.Columns.Add(new DataColumn("光盘编号"));
                        dt.Columns.Add(new DataColumn("备注"));
                        string[] texts = File.ReadAllLines(Application.StartupPath + "/Datas/gplb.txt", Encoding.UTF8);
                        for (int i = 0; i < texts.Length; i++)
                        {
                            DataRow dr = dt.NewRow();
                            dr[0] = texts[i].Split(',')[0].Replace("\"", string.Empty).Replace("[", string.Empty).Replace("]", string.Empty);
                            dr[1] = texts[i].Split(',')[1].Replace("\"", string.Empty).Replace("[", string.Empty).Replace("]", string.Empty);
                            dr[2] = texts[i].Split(',')[2].Replace("\"", string.Empty).Replace("[", string.Empty).Replace("]", string.Empty);
                            dt.Rows.Add(dr);
                        }
                        dgv_SWDJ.DataSource = dt;
                        dgv_SWDJ.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                        //添加返回按钮
                        btn_Back.Show();
                        btn_Add.Hide();
                        btn_Delete.Hide();
                    }
                }
            }
        }

        private void Dgv_SWDJ_CellMouseEnter(object sender, DataGridViewCellEventArgs e) {
            if (e.RowIndex != -1 && (e.ColumnIndex == 1 || e.ColumnIndex == 5))
            {
                dgv_SWDJ.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.ForeColor = Color.Blue;
            }

        }

        private void Dgv_SWDJ_CellMouseLeave(object sender, DataGridViewCellEventArgs e) {
            if (e.RowIndex != -1 && (e.ColumnIndex == 1 || e.ColumnIndex == 5))
            {
                dgv_SWDJ.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.ForeColor = Color.Black;
            }
        }

        private void btn_Find_MouseEnter(object sender, System.EventArgs e) {
            Cursor = Cursors.Hand;
        }

        private void btn_Find_MouseLeave(object sender, System.EventArgs e) {
            Cursor = Cursors.Default;
        }

        private void btn_Add_Click(object sender, System.EventArgs e) {
            Frm_AddPC frm = new Frm_AddPC();
            frm.ShowDialog();
        }

        private void btn_Back_Click(object sender, System.EventArgs e) {

        }

        private DataTable GetTableByIndex(int index) {
            DataTable dt = new DataTable();
            if (index == 0)//实物登记
            {
                string filePath = Application.StartupPath + "/Datas/swdj.txt";
                if (File.Exists(filePath))
                {
                    dt.Columns.Add(new DataColumn("来源单位"));
                    dt.Columns.Add(new DataColumn("批次名称"));
                    dt.Columns.Add(new DataColumn("批次号"));
                    dt.Columns.Add(new DataColumn("加工完成时间"));
                    dt.Columns.Add(new DataColumn("纸本数"));
                    dt.Columns.Add(new DataColumn("光盘数"));
                    dt.Columns.Add(new DataColumn("添加光盘"));
                    dt.Columns.Add(new DataColumn("操作"));
                    string[] texts = File.ReadAllLines(filePath);
                    for (int i = 0; i < texts.Length; i++)
                    {
                        DataRow dr = dt.NewRow();
                        for (int j = 0; j < 8; j++)
                        {
                            if(j == 6)
                            {
                                //dgv_SWDJ.Columns[j]. = Type.GetType("");
                                DataGridViewButtonCell btn = new DataGridViewButtonCell();
                            }
                            dr[j] = texts[i].Split(',')[j].Replace("\"", string.Empty).Replace("[", string.Empty).Replace("]", string.Empty); 
                        }
                        dt.Rows.Add(dr);
                    }
                }
            }
            return dt;
        }
    }
}

