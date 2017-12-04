using System;
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

            dgv_GPDJ.ColumnHeadersDefaultCellStyle.Font = new Font("微软雅黑", 10f, FontStyle.Bold);
            dgv_GPDJ.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv_GPDJ.ColumnHeadersDefaultCellStyle.BackColor = Color.AliceBlue;

            //列设置
            dgv_SWDJ.DefaultCellStyle.Font = new Font("微软雅黑", 9f);
            dgv_GPDJ.DefaultCellStyle.Font = new Font("微软雅黑", 9f);

            //加载数据
            dgv_SWDJ.DataSource = GetDataTable(0, null);
            dgv_GPDJ.DataSource = GetDataTable(1, null);

            //默认下拉框事件
            cbo_TypeSelect.SelectedIndex = 0;
            cbo_Company.SelectedIndex = 0;
        }

        private void Dgv_SWDJ_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                if (e.ColumnIndex == 1)
                {
                    if ("批次名称".Equals(dgv_SWDJ.Columns[e.ColumnIndex].HeaderText))
                    {
                        Frm_PcDetail frm = new Frm_PcDetail();
                        frm.ShowDialog();
                    }
                }
                else if (e.ColumnIndex == 5)
                {
                    if ("光盘数".Equals(dgv_SWDJ.Columns[e.ColumnIndex].HeaderText))
                        dgv_SWDJ.DataSource = GetDataTable(2, null);
                }
                else if (e.ColumnIndex == 6)
                {
                    if ("添加光盘".Equals(dgv_SWDJ.Columns[e.ColumnIndex].HeaderText))
                    {
                        Frm_AddCD frm = new Frm_AddCD();
                        frm.ShowDialog();
                    }
                }
                else if (e.ColumnIndex == 7)
                {
                    if ("操作".Equals(dgv_SWDJ.Columns[e.ColumnIndex].HeaderText))
                    {
                        if (MessageBox.Show("是否确认提交?", "确认提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk) == DialogResult.OK) {
                            MessageBox.Show("提交成功！","结果");
                            dgv_SWDJ.Rows.RemoveAt(e.RowIndex);
                        }
                    }
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

        private DataTable GetDataTable(int index, string remark)
        {
            DataTable dataTable = new DataTable();
            if (index == 0)//实物登记
            {
                string filePath = Application.StartupPath + "/Datas/swdj.txt";
                if (File.Exists(filePath))
                {
                    dataTable.Columns.Add(new DataColumn("来源单位"));
                    dataTable.Columns.Add(new DataColumn("批次名称"));
                    dataTable.Columns.Add(new DataColumn("批次号"));
                    dataTable.Columns.Add(new DataColumn("加工完成时间"));
                    dataTable.Columns.Add(new DataColumn("纸本数"));
                    dataTable.Columns.Add(new DataColumn("光盘数"));
                    dataTable.Columns.Add(new DataColumn("添加光盘"));
                    dataTable.Columns.Add(new DataColumn("操作"));
                    string[] texts = File.ReadAllLines(filePath);
                    for (int i = 0; i < texts.Length; i++)
                    {
                        DataRow dr = dataTable.NewRow();
                        for (int j = 0; j < 8; j++)
                        {
                            dr[j] = texts[i].Split(',')[j].Replace("\"", string.Empty).Replace("[", string.Empty).Replace("]", string.Empty);
                        }
                        dataTable.Rows.Add(dr);
                    }
                }
            }else if(index == 1)//光盘登记
            {
                string filePath = Application.StartupPath + "/Datas/gpdj.txt";
                if (File.Exists(filePath))
                {
                    dataTable.Columns.Add(new DataColumn("来源单位"));
                    dataTable.Columns.Add(new DataColumn("光盘编号"));
                    dataTable.Columns.Add(new DataColumn("光盘名称"));
                    dataTable.Columns.Add(new DataColumn("项目数"));
                    dataTable.Columns.Add(new DataColumn("课题数"));
                    dataTable.Columns.Add(new DataColumn("文件数"));
                    dataTable.Columns.Add(new DataColumn("操作"));
                    dataTable.Columns.Add(new DataColumn("状态"));
                    string[] staList = new string[] { "读写成功", "尚未读写", "解析异常", "读写异常" };
                    string[] texts = File.ReadAllLines(filePath);
                    Random random = new Random();
                    for (int i = 0; i < texts.Length; i++)
                    {
                        DataRow dr = dataTable.NewRow();
                        for (int j = 0; j < 8; j++)
                        {
                            if (remark != null && j == 7)
                            {
                                if ("dxcg".Equals(remark))
                                    dr[j] = staList[0];
                                else if ("swdx".Equals(remark))
                                    dr[j] = staList[1];
                                else if ("jxyc".Equals(remark))
                                    dr[j] = staList[2];
                                else if ("dxyc".Equals(remark))
                                    dr[j] = staList[3];
                                else if ("sy".Equals(remark))
                                    dr[j] = staList[random.Next(4)];
                            }
                            else
                                dr[j] = texts[i].Split(',')[j].Replace("\"", string.Empty).Replace("[", string.Empty).Replace("]", string.Empty);
                        }
                        dataTable.Rows.Add(dr);
                    }
                }
            }
            else if (index == 2)//光盘列表
            {
                string filePath = Application.StartupPath + "/Datas/gplb.txt";
                if (File.Exists(filePath))
                {
                    dataTable.Columns.Add(new DataColumn("光盘名称"));
                    dataTable.Columns.Add(new DataColumn("光盘编号"));
                    dataTable.Columns.Add(new DataColumn("编号"));
                    string[] texts = File.ReadAllLines(filePath);
                    for (int i = 0; i < texts.Length; i++)
                    {
                        DataRow dr = dataTable.NewRow();
                        for (int j = 0; j < 3; j++)
                        {
                            dr[j] = texts[i].Split(',')[j].Replace("\"", string.Empty).Replace("[", string.Empty).Replace("]", string.Empty);
                        }
                        dataTable.Rows.Add(dr);
                    }
                }
            }
            return dataTable;
        }

        private void btn_Back_Click(object sender, System.EventArgs e)
        {
            dgv_SWDJ.DataSource = GetDataTable(0, null);
        }

        private void cbo_TypeSelect_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if(cbo_TypeSelect.SelectedIndex == 0)
                dgv_GPDJ.DataSource = GetDataTable(1, "swdx");
            else if (cbo_TypeSelect.SelectedIndex == 1)
                dgv_GPDJ.DataSource = GetDataTable(1, "dxcg");
            else if (cbo_TypeSelect.SelectedIndex == 2)
                dgv_GPDJ.DataSource = GetDataTable(1, "jxyc");
            else if (cbo_TypeSelect.SelectedIndex == 3)
                dgv_GPDJ.DataSource = GetDataTable(1, "dxyc");
            else if(cbo_TypeSelect.SelectedIndex == 4)
                dgv_GPDJ.DataSource = GetDataTable(1, "sy");
        }

        private void dgv_GPDJ_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex != -1)
            {
                if (e.ColumnIndex ==  6)
                {
                    if ("操作".Equals(dgv_GPDJ.Columns[e.ColumnIndex].HeaderText))
                    {
                        Frm_ReadCD frm = new Frm_ReadCD();
                        if(frm.ShowDialog() == DialogResult.OK)
                        {
                            dgv_GPDJ.Rows.RemoveAt(e.RowIndex);
                        }
                        
                    }
                }

            }
        }

        //提交事件
        private void lbl_Submit_Click(object sender, EventArgs e)
        {
            if(dgv_GPDJ.SelectedRows.Count > 0)
            {
                if(MessageBox.Show("确定要提交当前行吗?", "提交确认", MessageBoxButtons.OK, MessageBoxIcon.Asterisk) == DialogResult.OK)
                {

                }
            }
            else
            {
                MessageBox.Show("请先选中一行数据进行操作!", "尚未选择任何数据");
            }
        }
    }
}
