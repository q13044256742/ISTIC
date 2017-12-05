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
        }

        private void dgv_JGDJ_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                if(e.ColumnIndex == 4)
                {
                    if ("光盘数".Equals(dgv_JGDJ.Columns[e.ColumnIndex].HeaderText))
                    {
                        dgv_JGDJ.DataSource = GetTableOfIndex(1);
                    }
                }else if(e.ColumnIndex == 6)
                {
                    if ("操作".Equals(dgv_JGDJ.Columns[e.ColumnIndex].HeaderText))
                    {
                        if(MessageBox.Show("是否确认加工当前选中数据?", "确认提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk) == DialogResult.OK)
                        {

                        }
                    }
                }
            }
        }

        /// <summary>
        /// 设置表格的数据源
        /// </summary>
        /// <param name="index">0）加工登记 1）光盘列表</param>
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
            return dataTable;
        }

        private void btn_Back_Click(object sender, EventArgs e)
        {
            dgv_JGDJ.DataSource = GetTableOfIndex(0);
        }
    }
}
