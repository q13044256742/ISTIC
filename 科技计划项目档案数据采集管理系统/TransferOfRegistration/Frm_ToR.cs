using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace 科技计划项目档案数据采集管理系统.TransferOfRegistration
{
    public partial class Frm_ToR : Form
    {
        public Frm_ToR()
        {
            InitializeComponent();
            InitalForm();
        }
        /// <summary>
        /// 初始化界面数据
        /// </summary>
        private void InitalForm()
        {
            //列表标题居中
            dgv_SWDJ.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }

        private void Frm_ToR_Load(object sender, EventArgs e)
        {
            LoadDataScoure();
        }

        private void btn_Add_Click(object sender, EventArgs e)
        {
            Frm_AddPC frm = new Frm_AddPC();
            if(frm.ShowDialog() == DialogResult.OK)
            {
                LoadDataScoure();
            }
        }

        /// <summary>
        /// 加载表单数据
        /// </summary>
        private void LoadDataScoure()
        {
            dgv_SWDJ.DataSource = null;
            dgv_SWDJ.Columns.Clear();
            dgv_SWDJ.Rows.Clear();


            //加载实物登记数据
            StringBuilder querySql = new StringBuilder("SELECT ");
            querySql.Append("com_id,");
            querySql.Append("trp_name,");
            querySql.Append("trp_code,");
            querySql.Append("trp_cd_amount");
            querySql.Append(" FROM transfer_registration_pc");
            DataTable dataTable = SqlHelper.ExecuteQuery(querySql.ToString());

            //将数据源转化成DataGridView表数据
            dgv_SWDJ.Columns.Add("com_id", "来源单位");
            dgv_SWDJ.Columns.Add("trp_name", "批次名称");
            dgv_SWDJ.Columns.Add("trp_code", "批次编号");
            dgv_SWDJ.Columns.Add("trp_cd_amount", "光盘数");
            dgv_SWDJ.Columns.Add("addpc", "添加光盘");
            dgv_SWDJ.Columns.Add("submit", "提交");
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                DataRow row = dataTable.Rows[i];
                int index = dgv_SWDJ.Rows.Add();
                dgv_SWDJ.Rows[index].Cells["com_id"].Value = row["com_id"];
                dgv_SWDJ.Rows[index].Cells["trp_name"].Value = row["trp_name"];
                dgv_SWDJ.Rows[index].Cells["trp_code"].Value = row["trp_code"];
                dgv_SWDJ.Rows[index].Cells["trp_cd_amount"].Value = row["trp_cd_amount"];
                dgv_SWDJ.Rows[index].Cells["addpc"].Value = "添加";
                dgv_SWDJ.Rows[index].Cells["submit"].Value = "提交";
            }
            //设置最小列宽
            dgv_SWDJ.Columns["com_id"].MinimumWidth = 300;
            dgv_SWDJ.Columns["trp_name"].MinimumWidth = 220;
            //设置链接按钮样式
            DataGridViewStyleHelper.SetAlignWithCenter(dgv_SWDJ, new int[] { dgv_SWDJ.Columns.Count - 1, dgv_SWDJ.Columns.Count - 2 });
            DataGridViewStyleHelper.SetLinkStyle(dgv_SWDJ, new int[] { dgv_SWDJ.Columns.Count - 1, dgv_SWDJ.Columns.Count - 2 });
        }
    }
}
