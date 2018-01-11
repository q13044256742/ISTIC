using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using 科技计划项目档案数据采集管理系统.Properties;

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
            //默认加载批次列表
            LoadPCDataScoure(null);

            //加载来源单位列表
            LoadCompanySource();
            btn_Back.Enabled = false;
        }

        /// <summary>
        /// 加载来源单位列表
        /// </summary>
        private void LoadCompanySource()
        {
            Image[] imgs = new Image[] { Resources.pic1, Resources.pic2, Resources.pic3, Resources.pic4, Resources.pic5, Resources.pic6, Resources.pic7, Resources.pic8 };
            //加载一级菜单
            List<CreateKyoPanel.KyoPanel> list = new List<CreateKyoPanel.KyoPanel>();
            list.Add(new CreateKyoPanel.KyoPanel
            {
                Name = "ToR",
                Text = "移交登记",
                Image = imgs[0]
            });
            CreateKyoPanel.SetPanel(pal_LeftMenu, list);
            //加载二级菜单
            list.Clear();
            list.Add(new CreateKyoPanel.KyoPanel
            {
                Name = "ace_all",
                Text = "全部来源单位",
                Image = Resources.pic1,
            });

            string querySql = "SELECT cs_id,cs_name FROM company_source ORDER BY sorting ASC";
            DataTable table = SqlHelper.ExecuteQuery(querySql);
            for (int i = 0; i < table.Rows.Count; i++)
            {
                list.Add(new CreateKyoPanel.KyoPanel
                {
                    Name = table.Rows[i]["cs_id"].ToString(),
                    Text = table.Rows[i]["cs_name"].ToString()
                });
            }
            Panel basicPanel = CreateKyoPanel.SetSubPanel(pal_LeftMenu.Controls.Find("ToR", false)[0] as Panel, list, Element_Click);

        }

        /// <summary>
        /// 来源单位点击事件
        /// </summary>
        private void Element_Click(object sender, EventArgs e)
        {
            Panel panel = null;
            if (sender is Panel)
                panel = sender as Panel;
            else if (sender is Label)
                panel = (sender as Label).Parent as Panel;
            if ("ace_all".Equals(panel.Name))
                LoadPCDataScoure(null);
            else
            {
                StringBuilder querySql = new StringBuilder("SELECT ");
                querySql.Append("pc.trp_id,");
                querySql.Append("cs_name,");
                querySql.Append("trp_name,");
                querySql.Append("trp_code,");
                querySql.Append("trp_cd_amount");
                querySql.Append(" FROM transfer_registration_pc pc,company_source cs");
                querySql.Append(" WHERE com_id='" + panel.Name + "'");
                querySql.Append(" AND pc.com_id = cs.cs_id");
                querySql.Append(" AND trp_status=1");
                LoadPCDataScoure(querySql.ToString());

                querySql = new StringBuilder("SELECT cs_code FROM company_source WHERE cs_id ='" + panel.Name + "'");
                dgv_SWDJ.Tag = SqlHelper.ExecuteOnlyOneQuery(querySql.ToString());
            }

            //当前Panel为选中状态
            foreach (Panel item in panel.Parent.Controls)
            {
                item.BackColor = Color.Transparent;
                item.Tag = false;
            }
            panel.BackColor = Color.Purple;
            panel.Tag = true;
        }

        /// <summary>
        /// 添加批次信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Add_Click(object sender, EventArgs e)
        {
            //获取当前列表所属来源单位id
            string unitCode = dgv_SWDJ.Tag == null ? string.Empty : dgv_SWDJ.Tag.ToString();
            Frm_AddPC frm = new Frm_AddPC(unitCode);
            if(frm.ShowDialog() == DialogResult.OK)
            {
                LoadPCDataScoure(null);
            }
        }

        /// <summary>
        /// 加载批次数据
        /// </summary>
        /// <param name="querySql">待加载的数据SQL</param>
        private void LoadPCDataScoure(string _querySql)
        {
            dgv_SWDJ.DataSource = null;
            dgv_SWDJ.Columns.Clear();
            dgv_SWDJ.Rows.Clear();

            DataTable dataTable = null;
            //加载实物登记数据【默认加载状态为1（未提交）的数据】
            if (string.IsNullOrEmpty(_querySql))
            {
                StringBuilder querySql = new StringBuilder("SELECT ");
                querySql.Append("pc.trp_id,");
                querySql.Append("cs_name,");
                querySql.Append("trp_name,");
                querySql.Append("trp_code,");
                querySql.Append("trp_cd_amount");
                querySql.Append(" FROM transfer_registration_pc pc,company_source cs");
                querySql.Append(" WHERE trp_status=1");
                querySql.Append(" AND pc.com_id = cs.cs_id");
                dataTable = SqlHelper.ExecuteQuery(querySql.ToString());
            }
            else
                dataTable = SqlHelper.ExecuteQuery(_querySql);

            //将数据源转化成DataGridView表数据
            dgv_SWDJ.Columns.Add("trp_id", "主键");
            dgv_SWDJ.Columns.Add("cs_name", "来源单位");
            dgv_SWDJ.Columns.Add("trp_name", "批次名称");
            dgv_SWDJ.Columns.Add("trp_code", "批次编号");
            dgv_SWDJ.Columns.Add("trp_cd_amount", "光盘数");
            dgv_SWDJ.Columns.Add("addpc", "添加光盘");
            dgv_SWDJ.Columns.Add("submit", "提交");
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                DataRow row = dataTable.Rows[i];
                int index = dgv_SWDJ.Rows.Add();
                dgv_SWDJ.Rows[index].Cells["trp_id"].Value = row["trp_id"];
                dgv_SWDJ.Rows[index].Cells["cs_name"].Value = row["cs_name"];
                dgv_SWDJ.Rows[index].Cells["trp_name"].Value = row["trp_name"];
                dgv_SWDJ.Rows[index].Cells["trp_code"].Value = row["trp_code"];
                dgv_SWDJ.Rows[index].Cells["trp_cd_amount"].Value = row["trp_cd_amount"];
                dgv_SWDJ.Rows[index].Cells["addpc"].Value = "添加";
                dgv_SWDJ.Rows[index].Cells["submit"].Value = "提交";
            }
            //设置最小列宽
            dgv_SWDJ.Columns["cs_name"].MinimumWidth = 300;
            dgv_SWDJ.Columns["trp_name"].MinimumWidth = 220;
            //设置链接按钮样式
            DataGridViewStyleHelper.SetAlignWithCenter(dgv_SWDJ, new int[] { dgv_SWDJ.Columns.Count - 1, dgv_SWDJ.Columns.Count - 2, dgv_SWDJ.Columns.Count - 3 });
            DataGridViewStyleHelper.SetLinkStyle(dgv_SWDJ, new int[] { dgv_SWDJ.Columns.Count - 1, dgv_SWDJ.Columns.Count - 2, dgv_SWDJ.Columns.Count - 3 }, true);

            dgv_SWDJ.Columns["trp_id"].Visible = false;

            btn_Back.Enabled = false;
            btn_Add.Enabled = true;
        }

        /// <summary>
        /// 加载光盘数据
        /// </summary>
        /// <param name="pid">批次主键</param>
        private void LoadCDDataScoure(string pid)
        {
            dgv_SWDJ.DataSource = null;
            dgv_SWDJ.Columns.Clear();
            dgv_SWDJ.Rows.Clear();

            StringBuilder querySql = new StringBuilder("SELECT ");
            querySql.Append("trc_id,");
            querySql.Append("trc_name,");
            querySql.Append("trc_code,");
            querySql.Append("trc_remark");
            querySql.Append(" FROM transfer_registraion_cd");
            querySql.Append(" WHERE trp_id='" + pid + "'");
            DataTable dataTable = SqlHelper.ExecuteQuery(querySql.ToString());

            //将数据源转化成DataGridView表数据
            dgv_SWDJ.Columns.Add("trc_id", "主键");
            dgv_SWDJ.Columns.Add("trc_name", "光盘名称");
            dgv_SWDJ.Columns.Add("trc_code", "光盘编号");
            dgv_SWDJ.Columns.Add("trc_remark", "备注");
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                DataRow row = dataTable.Rows[i];
                int index = dgv_SWDJ.Rows.Add();
                dgv_SWDJ.Rows[index].Cells["trc_id"].Value = row["trc_id"];
                dgv_SWDJ.Rows[index].Cells["trc_name"].Value = row["trc_name"];
                dgv_SWDJ.Rows[index].Cells["trc_code"].Value = row["trc_code"];
                dgv_SWDJ.Rows[index].Cells["trc_remark"].Value = row["trc_remark"];
            }
            dgv_SWDJ.Columns["trc_id"].Visible = false;

            btn_Back.Enabled = true;
            btn_Add.Enabled = false;
        }

        /// <summary>
        /// 单元格点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgv_SWDJ_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1 && e.ColumnIndex != -1 && e.RowIndex != dgv_SWDJ.RowCount - 1)
            {
                //当前点击单元格的值
                object value = dgv_SWDJ.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;

                //光盘数-点击事件
                if ("trp_cd_amount".Equals(dgv_SWDJ.Columns[e.ColumnIndex].Name))
                {
                    object currentRowId = dgv_SWDJ.Rows[e.RowIndex].Cells["trp_id"].Value;
                    if (Convert.ToInt32(value) != 0)
                    {
                        LoadCDDataScoure(currentRowId.ToString());
                    }
                }
                //添加光盘-点击事件
                else if ("addpc".Equals(dgv_SWDJ.Columns[e.ColumnIndex].Name))
                {
                    object currentRowId = dgv_SWDJ.Rows[e.RowIndex].Cells["trp_id"].Value;
                    Frm_AddCD frm = new Frm_AddCD(currentRowId.ToString());
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        LoadPCDataScoure(null);
                    }
                }
            }
        }

        /// <summary>
        /// 返回上一页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Back_Click(object sender, EventArgs e)
        {
            LoadPCDataScoure(null);
        }

        /// <summary>
        /// 删除选中数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Delete_Click(object sender, EventArgs e)
        {
            int amount = dgv_SWDJ.SelectedRows.Count;
            if (amount > 0)
            {
                if (MessageBox.Show("确定要删除选中的数据吗?", "确认提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk) == DialogResult.OK)
                {
                    int deleteAmount = 0;
                    foreach (DataGridViewRow row in dgv_SWDJ.SelectedRows)
                    {
                        string pid = row.Cells["trp_id"].Value.ToString();
                        string deleteSql = "DELETE FROM transfer_registration_pc WHERE trp_id = '" + pid + "'";
                        SqlHelper.ExecuteNonQuery(deleteSql);
                        deleteAmount++;
                    }
                    MessageBox.Show(deleteAmount + "条数据已被删除!", "操作成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadPCDataScoure(null);
                }
            }
            else
            {
                MessageBox.Show("请先至少选择一条要删除的数据!", "尚未选择数据", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }

        /// <summary>
        /// 搜索
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Search_Click(object sender, EventArgs e)
        {
            string searchKey = txt_Search.Text.Trim();
            if (!string.IsNullOrEmpty(searchKey))
            {
                StringBuilder querySql = new StringBuilder("SELECT ");
                querySql.Append("pc.trp_id,");
                querySql.Append("cs_name,");
                querySql.Append("trp_name,");
                querySql.Append("trp_code,");
                querySql.Append("trp_cd_amount");
                querySql.Append(" FROM transfer_registration_pc pc,company_source cs");
                querySql.Append("WHERE cs_name LIKE '%" + searchKey + "%' ");
                querySql.Append("OR trp_code LIKE '%" + searchKey + "%' ");
                querySql.Append("OR trp_name LIKE '%" + searchKey + "%'");
                querySql.Append(" AND pc.com_id = cs.cs_id");
                LoadPCDataScoure(querySql.ToString());
            }
            else
                LoadPCDataScoure(null);
        }

    }
}
