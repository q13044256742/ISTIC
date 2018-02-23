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
        private object currentUnit;
        public Frm_ToR()
        {
            InitializeComponent();
        }

        private void Frm_ToR_Load(object sender, EventArgs e)
        {
            //默认加载批次列表
            LoadPCDataScoure(null);

            //加载来源单位列表
            LoadCompanySource();
            btn_Back.Enabled = false;

            dgv_SWDJ.ColumnHeadersDefaultCellStyle = DataGridViewStyleHelper.GetHeaderStyle();
            dgv_GPDJ.ColumnHeadersDefaultCellStyle = DataGridViewStyleHelper.GetHeaderStyle();

            //默认查看状态为全部
            cbo_Status.SelectedIndex = 0;
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
                Image = imgs[0],
                HasNext = true
            });
            CreateKyoPanel.SetPanel(pal_LeftMenu, list);
            //加载二级菜单
            list.Clear();
            list.Add(new CreateKyoPanel.KyoPanel
            {
                Name = "ace_all",
                Text = "全部来源单位",
                Image = Resources.pic1,
                HasNext = false
            });

            string querySql = "SELECT dd_id, dd_name FROM data_dictionary WHERE dd_pId=" +
                "(SELECT dd_id FROM data_dictionary WHERE dd_code = 'dic_key_company_source') ORDER BY dd_sort";
            DataTable table = SqlHelper.ExecuteQuery(querySql);
            for (int i = 0; i < table.Rows.Count; i++)
            {
                list.Add(new CreateKyoPanel.KyoPanel
                {
                    Name = GetValue(table.Rows[i]["dd_id"]),
                    Text = GetValue(table.Rows[i]["cs_name"]),
                    HasNext = false
                });
            }
            Panel basicPanel = CreateKyoPanel.SetSubPanel(pal_LeftMenu.Controls.Find("ToR", false)[0] as Panel, list, Element_Click);

        }

        private string GetValue(object v)
        {
            return v == null ? string.Empty : v.ToString();
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
            {
                if (tc_ToR.SelectedIndex == 0)
                    LoadPCDataScoure(null);
                else if (tc_ToR.SelectedIndex == 1)
                    LoadGPDJ(null);
                currentUnit = null;
            }
            else
            {
                if (tc_ToR.SelectedIndex == 0)
                {
                    StringBuilder querySql = new StringBuilder("SELECT ");
                    querySql.Append("pc.trp_id, cs_name, trp_name, trp_code, trp_submit_status, trp_cd_amount");
                    querySql.Append(" FROM transfer_registration_pc pc,company_source cs");
                    querySql.Append(" WHERE com_id='" + panel.Name + "'");
                    querySql.Append(" AND pc.com_id = cs.cs_id AND trp_submit_status=1");
                    LoadPCDataScoure(querySql.ToString());
                    dgv_SWDJ.Tag = SqlHelper.ExecuteOnlyOneQuery("SELECT cs_code FROM company_source WHERE cs_id ='" + panel.Name + "'");
                }
                else if(tc_ToR.SelectedIndex == 1)
                {
                    StringBuilder querySql = new StringBuilder("SELECT trc_id,cs_name,trc_code,trc_name,trc_project_amount,trc_subject_amount,trc_status");
                    querySql.Append(" FROM transfer_registraion_cd trc");
                    querySql.Append(" LEFT JOIN(");
                    querySql.Append(" SELECT trp.trp_id, cs_id, cs_name, sorting FROM transfer_registration_pc trp, company_source cs WHERE trp.com_id = cs.cs_id ) tb");
                    querySql.Append(" ON trc.trp_id = tb.trp_id");
                    querySql.Append(" WHERE tb.cs_id='" + panel.Name + "'");
                    querySql.Append(" ORDER BY CASE WHEN cs_name IS NULL THEN 1 ELSE 0 END, sorting ASC, trc_code ASC");
                    LoadGPDJ(querySql.ToString());
                }
                currentUnit = panel.Name;
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
        private void Btn_Add_Click(object sender, EventArgs e)
        {
            //获取当前列表所属来源单位id
            string unitCode = dgv_SWDJ.Tag == null ? string.Empty : dgv_SWDJ.Tag.ToString();
            Frm_AddPC frm = new Frm_AddPC(true, unitCode);
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
            DataGridViewStyleHelper.ResetDataGridView(dgv_SWDJ);

            DataTable dataTable = null;
            //加载实物登记数据【默认加载状态为1（未提交）的数据】
            if (string.IsNullOrEmpty(_querySql))
            {
                StringBuilder querySql = new StringBuilder("SELECT pc.trp_id, cs_name, trp_name, trp_code,trp_submit_status,trp_cd_amount");
                querySql.Append(" FROM transfer_registration_pc pc LEFT JOIN company_source cs ON pc.com_id = cs.cs_id");
                querySql.Append($" WHERE trp_submit_status={(int)SubmitStatus.NonSubmit}");
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
                dgv_SWDJ.Rows[index].Cells["submit"].Value = Convert.ToInt32(row["trp_submit_status"]) == 1 ? "提交" : "已提交";
            }
            //设置最小列宽
            dgv_SWDJ.Columns["cs_name"].MinimumWidth = 200;
            dgv_SWDJ.Columns["trp_name"].MinimumWidth = 220;

            //设置链接按钮样式
            DataGridViewStyleHelper.SetLinkStyle(dgv_SWDJ, new string[] { "submit", "addpc", "trp_cd_amount" }, true);
            dgv_SWDJ.Columns[2].DefaultCellStyle = new DataGridViewCellStyle { ForeColor = Color.Blue };

            dgv_SWDJ.Columns["trp_id"].Visible = false;

            btn_Back.Enabled = false;
            btn_Add.Enabled = true;

            dgv_SWDJ.Tag = "PC";
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

            StringBuilder querySql = new StringBuilder("SELECT trc_id, trc_name, trc_code, trc_remark");
            querySql.Append($" FROM transfer_registraion_cd WHERE trp_id='{pid}'");
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

            dgv_SWDJ.Tag = "CD";
        }

        /// <summary>
        /// 单元格点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Dgv_SWDJ_CellClick(object sender, DataGridViewCellEventArgs e)
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
                        if(currentUnit != null)
                        {
                            StringBuilder querySql = new StringBuilder("SELECT ");
                            querySql.Append("pc.trp_id, cs_name, trp_name, trp_code, trp_submit_status, trp_cd_amount");
                            querySql.Append(" FROM transfer_registration_pc pc,company_source cs");
                            querySql.Append(" WHERE pc.com_id = cs.cs_id AND trp_submit_status=1");
                            querySql.Append(" AND com_id='" + currentUnit + "'");
                            LoadPCDataScoure(querySql.ToString());
                            dgv_SWDJ.Tag = SqlHelper.ExecuteOnlyOneQuery($"SELECT cs_code FROM company_source WHERE cs_id ='{currentUnit}'");
                        }
                        else
                            LoadPCDataScoure(null);
                    }
                }
                //批次名称-点击事件
                else if ("trp_name".Equals(dgv_SWDJ.Columns[e.ColumnIndex].Name))
                {
                    object currentRowId = dgv_SWDJ.Rows[e.RowIndex].Cells["trp_id"].Value;
                    Frm_AddPC frm = new Frm_AddPC(false, currentRowId.ToString());
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        LoadPCDataScoure(null);
                    }
                }
                //提交 - 点击事件
                else if ("submit".Equals(dgv_SWDJ.Columns[e.ColumnIndex].Name))
                {
                    object currentRowId = dgv_SWDJ.Rows[e.RowIndex].Cells["trp_id"].Value;
                    if (currentRowId != null && !"已提交".Equals(dgv_SWDJ.Rows[e.RowIndex].Cells[e.ColumnIndex].Value))
                    {
                        //如果当前批次下存在光盘且未读取，或者存在读取失败的记录，则不允许提交
                        string querySql = $"SELECT COUNT(*) FROM transfer_registraion_cd WHERE trp_id='{currentRowId}' AND trc_status<>{(int)ReadStatus.ReadSuccess}";
                        int logAmount = Convert.ToInt32(SqlHelper.ExecuteOnlyOneQuery(querySql));
                        if (logAmount == 0)
                        {
                            if (MessageBox.Show("确定要提交当前选中项吗？", "确认提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                string updateSql = $"UPDATE transfer_registration_pc SET trp_submit_status={(int)ObjectSubmitStatus.SubmitSuccess} WHERE trp_id='{currentRowId}'";
                                SqlHelper.ExecuteNonQuery(updateSql);
                                LoadPCDataScoure(null);
                            }
                        }
                        else
                        {
                            MessageBox.Show("当前批次下存在尚未处理的光盘！", "提交失败", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        }
                    }
                }
            }
        }
        /// <summary>
        /// 返回上一页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Back_Click(object sender, EventArgs e)
        {
            LoadPCDataScoure(null);
        }
        /// <summary>
        /// 删除选中数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Delete_Click(object sender, EventArgs e)
        {
            int amount = dgv_SWDJ.SelectedRows.Count;
            if (amount > 0)
            {
                if (MessageBox.Show("确定要删除选中的数据吗?", "确认提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk) == DialogResult.OK)
                {
                    int deleteAmount = 0;
                    if ("PC".Equals(dgv_SWDJ.Tag))
                    {
                        foreach (DataGridViewRow row in dgv_SWDJ.SelectedRows)
                        {
                            string pid = row.Cells["trp_id"].Value.ToString();
                            string deleteSql = $"DELETE FROM transfer_registration_pc WHERE trp_id = '{pid}'";
                            SqlHelper.ExecuteNonQuery(deleteSql);
                            deleteAmount++;
                        }
                        LoadPCDataScoure(null);
                    }
                    else if ("CD".Equals(dgv_SWDJ.Tag))
                    {
                        string pid = null;
                        foreach (DataGridViewRow row in dgv_SWDJ.SelectedRows)
                        {
                            string cid = row.Cells["trc_id"].Value.ToString();
                            pid = SqlHelper.ExecuteOnlyOneQuery($"SELECT trp_id FROM transfer_registraion_cd WHERE trc_id='{cid}'").ToString();

                            string deleteSql = $"DELETE FROM transfer_registraion_cd WHERE trc_id = '{cid}'";
                            SqlHelper.ExecuteNonQuery(deleteSql);

                            string updateSql = $"UPDATE transfer_registration_pc SET trp_cd_amount=(SELECT COUNT(*) FROM transfer_registraion_cd WHERE trp_id = '{pid}') WHERE trp_id = '{pid}'";
                            SqlHelper.ExecuteNonQuery(updateSql);

                            deleteAmount++;
                        }
                        LoadCDDataScoure(pid);
                    }
                    MessageBox.Show(deleteAmount + "条数据已被删除!", "操作成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                querySql.Append("trp_cd_amount,");
                querySql.Append("trp_submit_status");
                querySql.Append(" FROM transfer_registration_pc pc LEFT JOIN company_source cs ON pc.com_id = cs.cs_id");
                querySql.Append(" WHERE cs_name LIKE '%" + searchKey + "%' ");
                querySql.Append("OR trp_code LIKE '%" + searchKey + "%' ");
                querySql.Append("OR trp_name LIKE '%" + searchKey + "%'");
                LoadPCDataScoure(querySql.ToString());
            }
            else
                LoadPCDataScoure(null);
        }

        /// <summary>
        /// 选项卡切换事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Tc_ToR_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = tc_ToR.SelectedIndex;
            if(index == 1)//光盘
            {
                if (dgv_GPDJ.Columns.Count == 0)
                    LoadGPDJ(null);
            }
        }

        /// <summary>
        /// 加载光盘列表
        /// </summary>
        private void LoadGPDJ(string _querySql)
        {
            dgv_GPDJ.Rows.Clear();
            dgv_GPDJ.Columns.Clear();

            dgv_GPDJ.Columns.Add("trc_id", "主键");
            dgv_GPDJ.Columns.Add("cs_name", "来源单位");
            dgv_GPDJ.Columns.Add("trc_code", "光盘编号");
            dgv_GPDJ.Columns.Add("trc_name", "光盘名称");
            dgv_GPDJ.Columns.Add("trc_project_amount", "项目数");
            dgv_GPDJ.Columns.Add("trc_subject_amount", "课题数");
            dgv_GPDJ.Columns.Add("trc_file_amount", "文件数");
            dgv_GPDJ.Columns.Add("trc_status", "读写状态");
            dgv_GPDJ.Columns.Add("control", "操作");

            DataTable table = null;
            if (_querySql == null)
            {
                StringBuilder querySql = new StringBuilder("SELECT trc_id,cs_name,trc_code,trc_name,trc_status");
                querySql.Append(" FROM transfer_registraion_cd trc LEFT JOIN(");
                querySql.Append(" SELECT trp.trp_id, cs_name,sorting FROM transfer_registration_pc trp, company_source cs WHERE trp.com_id = cs.cs_id ) tb");
                querySql.Append(" ON trc.trp_id = tb.trp_id ORDER BY CASE WHEN cs_name IS NULL THEN 1 ELSE 0 END,trc_status ASC, sorting ASC");
                table = SqlHelper.ExecuteQuery(querySql.ToString());
            }
            else
                table = SqlHelper.ExecuteQuery(_querySql);
            foreach (DataRow row in table.Rows)
            {
                int _index = dgv_GPDJ.Rows.Add();
                dgv_GPDJ.Rows[_index].Cells["trc_id"].Value = row["trc_id"];
                dgv_GPDJ.Rows[_index].Cells["cs_name"].Value = row["cs_name"];
                dgv_GPDJ.Rows[_index].Cells["trc_code"].Value = row["trc_code"];
                dgv_GPDJ.Rows[_index].Cells["trc_name"].Value = row["trc_name"];
                dgv_GPDJ.Rows[_index].Cells["trc_project_amount"].Value = GetProjectAmount(row["trc_id"]);
                dgv_GPDJ.Rows[_index].Cells["trc_subject_amount"].Value = GetSubjectAmount(row["trc_id"]);
                dgv_GPDJ.Rows[_index].Cells["trc_file_amount"].Value = GetFileAmount(row["trc_id"]);
                dgv_GPDJ.Rows[_index].Cells["trc_status"].Value = GetReadStatus(GetInt32(row["trc_status"]));
                dgv_GPDJ.Rows[_index].Cells["control"].Value = "读写";
            }
            if (dgv_GPDJ.Columns.Count > 0)
                dgv_GPDJ.Columns[0].Visible = false;

            List<KeyValuePair<string, int>> list = new List<KeyValuePair<string, int>>();
            list.Add(new KeyValuePair<string, int>("cs_name", 250));
            list.Add(new KeyValuePair<string, int>("trc_code", 200));
            list.Add(new KeyValuePair<string, int>("trc_name", 200));

            list.Add(new KeyValuePair<string, int>("trc_project_amount", 90));
            list.Add(new KeyValuePair<string, int>("trc_subject_amount", 90));
            list.Add(new KeyValuePair<string, int>("trc_file_amount", 90));
            list.Add(new KeyValuePair<string, int>("control", 100));
            list.Add(new KeyValuePair<string, int>("trc_status", 100));
            DataGridViewStyleHelper.SetWidth(dgv_GPDJ, list);

            DataGridViewStyleHelper.SetAlignWithCenter(dgv_GPDJ, new string[] { "trc_status" });
            DataGridViewStyleHelper.SetLinkStyle(dgv_GPDJ, new string[] { "control" }, false);
            
        }
        /// <summary>
        /// 根据光盘ID获取文件数
        /// </summary>
        /// <param name="cdid">光盘ID</param>
        private object GetFileAmount(object cdid)
        {
            return SqlHelper.ExecuteOnlyOneQuery($"SELECT COUNT(pfl_id) FROM processing_file_list WHERE pfl_obj_id='{cdid}'");
        }
        /// <summary>
        /// 根据光盘ID获取项目数总和
        /// </summary>
        private int GetProjectAmount(object trcId)
        {
            int proAmount = 0;
            if (trcId != null)
                proAmount = Convert.ToInt32(SqlHelper.ExecuteOnlyOneQuery($"SELECT COUNT(*) FROM project_info WHERE pi_obj_id=(SELECT pi_id FROM project_info WHERE trc_id='{trcId}')"));
            return proAmount;
        }

        /// <summary>
        /// 根据光盘ID获取课题数总和
        /// </summary>
        private object GetSubjectAmount(object trcId)
        {
            int amount = 0;
            if(trcId != null)
                amount = Convert.ToInt32(SqlHelper.ExecuteOnlyOneQuery($"SELECT COUNT(si_id) FROM subject_info WHERE pi_id IN " +
                    $"(SELECT pi_id FROM project_info WHERE pi_obj_id=" +
                    $"(SELECT pi_id FROM project_info WHERE trc_id='{trcId}'))"));
            return amount;
        }

        /// <summary>
        /// 根据状态id获取光盘的读写状态
        /// </summary>
        private string GetReadStatus(int index)
        {
            if (index == 1) return "尚未读写";
            else if (index == 2) return "读写成功";
            else if (index == 3) return "解析异常";
            else return string.Empty;
        }

        /// <summary>
        /// 将Object对象转换成Int对象
        /// </summary>
        private int GetInt32(object _obj)
        {
            int temp = 0;
            if (_obj == null)
                return temp;
            int.TryParse(_obj.ToString(), out temp);
            return temp;
        }

        /// <summary>
        /// 光盘页搜索
        /// </summary>
        private void Btn_CD_Search_Click(object sender, EventArgs e)
        {
            string key = txt_CD_Search.Text;
            string queryCondition = null;
            if (!string.IsNullOrEmpty(key))
                queryCondition = " WHERE cs_name LIKE '%" + key + "%' OR trc_code LIKE '%" + key + "%' OR trc_name LIKE '%" + key + "%'";

            StringBuilder querySql = new StringBuilder("SELECT trc_id,cs_name,trc_code,trc_name,trc_project_amount,trc_subject_amount,trc_status");
            querySql.Append(" FROM transfer_registraion_cd trc");
            querySql.Append(" LEFT JOIN(");
            querySql.Append(" SELECT trp.trp_id, cs_name,sorting FROM transfer_registration_pc trp, company_source cs WHERE trp.com_id = cs.cs_id ) tb");
            querySql.Append(" ON trc.trp_id = tb.trp_id");
            querySql.Append(queryCondition);
            querySql.Append(" ORDER BY CASE WHEN cs_name IS NULL THEN 1 ELSE 0 END, trc_status ASC, sorting ASC");

            LoadGPDJ(querySql.ToString());
        }

        /// <summary>
        /// 光盘页单元格点击事件
        /// </summary>
        private void Dgv_GPDJ_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex!=-1 && e.ColumnIndex != -1)
            {
                object trcId = dgv_GPDJ.Rows[e.RowIndex].Cells["trc_id"].Value;
                if ("control".Equals(dgv_GPDJ.Columns[e.ColumnIndex].Name))
                {
                    Frm_CDRead read = new Frm_CDRead(trcId);
                    if(read.ShowDialog() == DialogResult.OK)
                    {
                        LoadGPDJ(null);
                    }
                }
            }
        }

        private void Cbo_Status_SelectionChangeCommitted(object sender, EventArgs e)
        {
            int index = cbo_Status.SelectedIndex;
            if (index != -1)
            {
                StringBuilder querySql = new StringBuilder("SELECT trc_id,cs_name,trc_code,trc_name,trc_project_amount,trc_subject_amount,trc_status");
                querySql.Append(" FROM transfer_registraion_cd trc");
                querySql.Append(" LEFT JOIN(");
                querySql.Append(" SELECT trp.trp_id, cs_id, cs_name, sorting FROM transfer_registration_pc trp, company_source cs WHERE trp.com_id = cs.cs_id ) tb");
                querySql.Append(" ON trc.trp_id = tb.trp_id");
                if (index != 0) querySql.Append(" WHERE trc_status='" + index + "'");
                querySql.Append(" ORDER BY CASE WHEN cs_name IS NULL THEN 1 ELSE 0 END, trc_status ASC, sorting ASC");
                LoadGPDJ(querySql.ToString());
            }
        }

        /// <summary>
        /// 光盘列表删除事件
        /// </summary>
        private void Btn_CD_Delete_Click(object sender, EventArgs e)
        {
            int amount = dgv_GPDJ.SelectedRows.Count;
            if (amount > 0)
            {
                if (MessageBox.Show("确定要删除选中的数据吗?", "确认提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk) == DialogResult.OK)
                {
                    int deleteAmount = 0;
                    string pid = null;
                    foreach (DataGridViewRow row in dgv_GPDJ.SelectedRows)
                    {
                        string cid = row.Cells["trc_id"].Value.ToString();
                        pid = SqlHelper.ExecuteOnlyOneQuery($"SELECT trp_id FROM transfer_registraion_cd WHERE trc_id='{cid}'").ToString();

                        string deleteSql = $"DELETE FROM transfer_registraion_cd WHERE trc_id = '{cid}'";
                        SqlHelper.ExecuteNonQuery(deleteSql);

                        string updateSql = $"UPDATE transfer_registration_pc SET trp_cd_amount=(SELECT COUNT(*) FROM transfer_registraion_cd WHERE trp_id = '{pid}') WHERE trp_id = '{pid}'";
                        SqlHelper.ExecuteNonQuery(updateSql);

                        deleteAmount++;
                    }
                    LoadGPDJ(null);
                    MessageBox.Show(deleteAmount + "条数据已被删除!", "操作成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("请先至少选择一条要删除的数据!", "尚未选择数据", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }
    }
}
