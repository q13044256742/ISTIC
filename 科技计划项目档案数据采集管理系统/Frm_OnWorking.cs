using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace 科技计划项目档案数据采集管理系统
{
    public partial class Frm_OnWorking : Form
    {
        public Frm_OnWorking(int index)
        {
            InitializeComponent();
            InitialForm(index);
        }

        /// <summary>
        /// 根据类型初始化表格
        /// </summary>
        /// <param name="index">
        /// 1）批次
        /// 2）光盘
        /// 3）项目/课题
        /// 4）课题/子课题
        /// </param>
        private void InitialForm(int index)
        {
            string filePath = string.Empty;
            DataTable dataTable = new DataTable();
            if (index == 0)
            {
                dataTable.Columns.Add(new DataColumn("批次号"));
                dataTable.Columns.Add(new DataColumn("批次名称"));
                dataTable.Columns.Add(new DataColumn("来源单位"));
                dataTable.Columns.Add(new DataColumn("加工内容"));
                dataTable.Columns.Add(new DataColumn("操作"));
                filePath = Application.StartupPath + "/Datas/jgz_1.txt";
            }
            else if (index == 1)
            {
                dataTable.Columns.Add(new DataColumn("光盘编号"));
                dataTable.Columns.Add(new DataColumn("光盘名称"));
                dataTable.Columns.Add(new DataColumn("来源单位"));
                dataTable.Columns.Add(new DataColumn("加工内容"));
                dataTable.Columns.Add(new DataColumn("操作"));
                filePath = Application.StartupPath + "/Datas/jgz_2.txt";
            }
            else if (index == 2)
            {
                dataTable.Columns.Add(new DataColumn("项目/课题编号"));
                dataTable.Columns.Add(new DataColumn("项目/课题名称"));
                dataTable.Columns.Add(new DataColumn("来源单位"));
                dataTable.Columns.Add(new DataColumn("加工内容"));
                dataTable.Columns.Add(new DataColumn("操作"));
                filePath = Application.StartupPath + "/Datas/jgz_3.txt";
            }
            else if (index == 3)
            {
                dataTable.Columns.Add(new DataColumn("课题/子课题编号"));
                dataTable.Columns.Add(new DataColumn("课题/子课题名称"));
                dataTable.Columns.Add(new DataColumn("来源单位"));
                dataTable.Columns.Add(new DataColumn("加工内容"));
                dataTable.Columns.Add(new DataColumn("操作"));
                filePath = Application.StartupPath + "/Datas/jgz_4.txt";
            }
            if (File.Exists(filePath))
            {
                string[] texts = File.ReadAllLines(filePath, Encoding.UTF8);
                for (int j = 0; j < texts.Length; j++)
                {
                    DataRow dataRow = dataTable.NewRow();
                    for (int i = 0; i < 5; i++)
                    {
                        dataRow[i] = texts[j].Split(',')[i].Replace("\"", string.Empty).Replace("[", string.Empty).Replace("]", string.Empty);
                    }
                    dataTable.Rows.Add(dataRow);
                }
            }
            dgv_WorkList.DataSource = dataTable;
            dgv_WorkList.Tag = index;
        }

        private void Frm_OnWorking_Load(object sender, System.EventArgs e)
        {
            dgv_WorkList.ColumnHeadersDefaultCellStyle.Font = new Font("微软雅黑", 10f, FontStyle.Bold);
            dgv_WorkList.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv_WorkList.ColumnHeadersDefaultCellStyle.BackColor = Color.AliceBlue;

            //列设置
            dgv_WorkList.DefaultCellStyle.Font = new Font("微软雅黑", 9f);
            dgv_WorkList.DefaultCellStyle.Font = new Font("微软雅黑", 9f);
        }

        private void dgv_WorkList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex != -1)
            {
                if(e.ColumnIndex == 4)
                {
                    if ("操作".Equals(dgv_WorkList.Columns[e.ColumnIndex].HeaderText))
                    {
                        switch (Convert.ToInt32(dgv_WorkList.Tag))
                        {
                            case 0:
                                {
                                    Frm_ProTypeSelect frm = new Frm_ProTypeSelect();
                                    frm.ShowDialog();
                                }
                                break;
                            case 1:
                                break;
                            case 2:
                                break;
                            case 3:
                                break;
                        }
                    }
                }
            }
        }
    }
}
