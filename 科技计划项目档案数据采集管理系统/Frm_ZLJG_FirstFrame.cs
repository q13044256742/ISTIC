using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace 科技计划项目档案数据采集管理系统
{
    public partial class Frm_ZLJG_FirstFrame : Form
    {
        public Frm_ZLJG_FirstFrame()
        {
            InitializeComponent();
        }

        private void Frm_ZLJG_FirstFrame_Load(object sender, EventArgs e)
        {
            dgv_JGDJ.DataSource = GetTableOfIndex(0);
            dgv_JGDJ.Tag = 0;

            //表头设置
            dgv_JGDJ.ColumnHeadersDefaultCellStyle.Font = new Font("微软雅黑", 10f, FontStyle.Bold);
            dgv_JGDJ.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv_JGDJ.ColumnHeadersDefaultCellStyle.BackColor = Color.AliceBlue;

            dgv_JGDJ.DefaultCellStyle.Font = new Font("微软雅黑", 9f);
            dgv_JGDJ.DefaultCellStyle.Font = new Font("微软雅黑", 9f);
        }

        private void dgv_JGDJ_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                switch (Convert.ToInt32(dgv_JGDJ.Tag))
                {
                    case 0://批次
                        {
                            //光盘数 >> 光盘列表
                            if (e.ColumnIndex == 4)
                            {
                                if ("光盘数".Equals(dgv_JGDJ.Columns[e.ColumnIndex].HeaderText))
                                {
                                    dgv_JGDJ.DataSource = GetTableOfIndex(1);
                                    dgv_JGDJ.Tag = 1;
                                }
                            }
                            //批次列表加工事件
                            else if (e.ColumnIndex == 6)
                            {
                                GotoWork();
                            }
                        }
                        break;
                    case 1://光盘
                        {
                            //总数
                            if(e.ColumnIndex == 3)
                            {
                                if ("总数".Equals(dgv_JGDJ.Columns[e.ColumnIndex].HeaderText))
                                {
                                    //前两行项目/课题
                                    if(e.RowIndex == 1 || e.RowIndex == 2)
                                    {
                                        dgv_JGDJ.DataSource = GetTableOfIndex(2);
                                        dgv_JGDJ.Tag = 2;
                                    }
                                    //后两行课题/子课题
                                    else if(e.RowIndex == 3 || e.RowIndex == 4)
                                    {
                                        dgv_JGDJ.DataSource = GetTableOfIndex(3);
                                        dgv_JGDJ.Tag = 3;
                                    }
                                }
                            }else if(e.ColumnIndex == 6)
                            {
                                GotoWork();
                            }
                        }
                        break;
                    case 2://项目/课题
                        {
                            if(e.ColumnIndex == 4)
                            {
                                if ("总数".Equals(dgv_JGDJ.Columns[e.ColumnIndex].HeaderText))
                                {
                                    dgv_JGDJ.DataSource = GetTableOfIndex(3);
                                    dgv_JGDJ.Tag = 3;
                                }
                            }else if(e.ColumnIndex == 7)
                            {
                                GotoWork();
                            }
                        }
                        break;
                    case 3://课题/子课题
                        {
                            if(e.ColumnIndex == 3)
                            {
                                if ("总数".Equals(dgv_JGDJ.Columns[e.ColumnIndex].HeaderText))
                                {
                                    
                                }
                            }
                        }
                        break;
                }
            }
        }

        /// <summary>
        /// 设置表格的数据源
        /// </summary>
        /// <param name="index">
        /// 0）批次
        /// 1）光盘
        /// 2）项目/课题
        /// 3）课题/子课题
        /// </param>
        /// <returns></returns>
        private DataTable GetTableOfIndex(int index)
        {
            DataTable dataTable = new DataTable();
            if (index == 0)
            {
                dataTable.Columns.Add(new DataColumn("来源单位"));
                dataTable.Columns.Add(new DataColumn("批次名称"));
                dataTable.Columns.Add(new DataColumn("批次号"));
                dataTable.Columns.Add(new DataColumn("完成时间"));
                dataTable.Columns.Add(new DataColumn("光盘数"));
                dataTable.Columns.Add(new DataColumn("纸本数"));
                dataTable.Columns.Add(new DataColumn("操作"));

                string filePath = Application.StartupPath + "/Datas/jgdj.txt";
                if (File.Exists(filePath))
                {
                    string[] texts = File.ReadAllLines(filePath, Encoding.UTF8);
                    for (int i = 0; i < texts.Length; i++)
                    {
                        DataRow dr = dataTable.NewRow();
                        for (int j = 0; j < 7; j++)
                        {
                            dr[j] = texts[i].Split(',')[j].Replace("\"", string.Empty).Replace("[", string.Empty).Replace("]", string.Empty);
                        }
                        dataTable.Rows.Add(dr);
                    }
                }
            }else if(index == 1)
            {
                dataTable.Columns.Add(new DataColumn("来源单位"));
                dataTable.Columns.Add(new DataColumn("光盘名称"));
                dataTable.Columns.Add(new DataColumn("光盘编号"));
                dataTable.Columns.Add(new DataColumn("总数"));
                dataTable.Columns.Add(new DataColumn("已领取数"));
                dataTable.Columns.Add(new DataColumn("文件数"));
                dataTable.Columns.Add(new DataColumn("操作"));

                string filePath = Application.StartupPath + "/Datas/gplb_jg.txt";
                if (File.Exists(filePath))
                {
                    string[] texts = File.ReadAllLines(filePath, Encoding.UTF8);
                    for (int i = 0; i < texts.Length; i++)
                    {
                        DataRow dr = dataTable.NewRow();
                        for (int j = 0; j < 7; j++)
                        {
                            dr[j] = texts[i].Split(',')[j].Replace("\"", string.Empty).Replace("[", string.Empty).Replace("]", string.Empty);
                        }
                        dataTable.Rows.Add(dr);
                    }
                }
            }
            else if (index == 2)
            {
                dataTable.Columns.Add(new DataColumn("来源单位"));
                dataTable.Columns.Add(new DataColumn("项目/课题编号"));
                dataTable.Columns.Add(new DataColumn("项目/课题名称"));
                dataTable.Columns.Add(new DataColumn("承担单位"));
                dataTable.Columns.Add(new DataColumn("总数"));
                dataTable.Columns.Add(new DataColumn("已领取数"));
                dataTable.Columns.Add(new DataColumn("文件数"));
                dataTable.Columns.Add(new DataColumn("操作"));

                string filePath = Application.StartupPath + "/Datas/gplb_lydw.txt";
                if (File.Exists(filePath))
                {
                    string[] texts = File.ReadAllLines(filePath, Encoding.UTF8);
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
            }
            else if (index == 3)
            {
                dataTable.Columns.Add(new DataColumn("来源单位"));
                dataTable.Columns.Add(new DataColumn("课题/子课题编号"));
                dataTable.Columns.Add(new DataColumn("课题/子课题名称"));
                dataTable.Columns.Add(new DataColumn("总数"));
                dataTable.Columns.Add(new DataColumn("已领取数"));
                dataTable.Columns.Add(new DataColumn("文件数"));
                dataTable.Columns.Add(new DataColumn("操作"));

                string filePath = Application.StartupPath + "/Datas/gplb_kt.txt";
                if (File.Exists(filePath))
                {
                    string[] texts = File.ReadAllLines(filePath, Encoding.UTF8);
                    for (int i = 0; i < texts.Length; i++)
                    {
                        DataRow dr = dataTable.NewRow();
                        for (int j = 0; j < 7; j++)
                        {
                            dr[j] = texts[i].Split(',')[j].Replace("\"", string.Empty).Replace("[", string.Empty).Replace("]", string.Empty);
                        }
                        dataTable.Rows.Add(dr);
                    }
                }
            }
            return dataTable;
        }

        private void btn_Back_Click(object sender, EventArgs e)
        {
            dgv_JGDJ.DataSource = GetTableOfIndex(0);
            dgv_JGDJ.Tag = 0;
        }

        //将当前行数据记录到到加工中表单
        private void GotoWork()
        {
            if (MessageBox.Show("是否确认加工当前选中数据?", "确认提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk) == DialogResult.OK)
            {
                Frm_OnWorking frm = new Frm_OnWorking(int.Parse(dgv_JGDJ.Tag.ToString()));
                frm.MdiParent = Frm_MainFrame.ActiveForm;
                frm.Show();
                Hide();
            }
        }
    }
}
