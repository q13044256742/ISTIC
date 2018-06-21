﻿using DevExpress.XtraBars.Navigation;
using DevExpress.XtraEditors;
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

            //默认查看状态为全部
            cbo_Status.SelectedIndex = 0;

            dgv_SWDJ.ColumnHeadersDefaultCellStyle.Font = new Font("微软雅黑", 10.5f, FontStyle.Bold);
            dgv_GPDJ.ColumnHeadersDefaultCellStyle.Font = new Font("微软雅黑", 10.5f, FontStyle.Bold);
        }

        /// <summary>
        /// 加载来源单位列表
        /// </summary>
        private void LoadCompanySource()
        {
            string querySql = "SELECT dd_id, dd_name FROM data_dictionary WHERE dd_pId=" +
                "(SELECT dd_id FROM data_dictionary WHERE dd_code = 'dic_key_company_source') ORDER BY dd_sort";
            DataTable table = SqlHelper.ExecuteQuery(querySql);
            for (int i = 0; i < table.Rows.Count; i++)
            {
                AccordionControlElement element = new AccordionControlElement()
                {
                    Style = ElementStyle.Item,
                    Name = GetValue(table.Rows[i]["dd_id"]),
                    Text = GetValue(table.Rows[i]["dd_name"]),
                };
                element.Click += new EventHandler(Element_Click);
                acg_Register.Elements.Add(element);
            }

            ac_LeftMenu.SelectedElement = ace_all;
        }

        private string GetValue(object v) => v == null ? string.Empty : v.ToString();

        /// <summary>
        /// 来源单位点击事件
        /// </summary>
        private void Element_Click(object sender, EventArgs e)
        {
            AccordionControlElement element = sender as AccordionControlElement;
            if ("ace_all".Equals(element.Name))
            {
                if (tc_ToR.SelectedTabPageIndex == 0)
                    LoadPCDataScoure(null);
                else if (tc_ToR.SelectedTabPageIndex == 1)
                    LoadGPDJ(null);
                currentUnit = null;
            }
            else
            {
                if (tc_ToR.SelectedTabPageIndex == 0)
                {
                    StringBuilder querySql = new StringBuilder("SELECT ");
                    querySql.Append("pc.trp_id, dd_name, trp_name, trp_code, trp_submit_status, trp_cd_amount");
                    querySql.Append(" FROM transfer_registration_pc pc,data_dictionary dd");
                    querySql.Append(" WHERE com_id='" + element.Name + "'");
                    querySql.Append(" AND pc.com_id = dd.dd_id AND trp_submit_status=1");
                    LoadPCDataScoure(querySql.ToString());
                    dgv_SWDJ.Tag = SqlHelper.ExecuteOnlyOneQuery("SELECT dd_code FROM data_dictionary WHERE dd_id ='" + element.Name + "'");
                }
                else if(tc_ToR.SelectedTabPageIndex == 1)
                {
                    StringBuilder querySql = new StringBuilder("SELECT trc_id,dd_name,trc_code,trc_name,trc_project_amount,trc_subject_amount,trc_status");
                    querySql.Append(" FROM transfer_registraion_cd trc");
                    querySql.Append(" LEFT JOIN(");
                    querySql.Append(" SELECT trp.trp_id, dd_id, dd_name, dd_sort FROM transfer_registration_pc trp, data_dictionary dd WHERE trp.com_id = dd.dd_id ) tb");
                    querySql.Append(" ON trc.trp_id = tb.trp_id");
                    querySql.Append(" WHERE tb.dd_id='" + element.Name + "'");
                    querySql.Append(" ORDER BY CASE WHEN dd_name IS NULL THEN 1 ELSE 0 END, dd_sort ASC, trc_code ASC");
                    LoadGPDJ(querySql.ToString());
                }
                currentUnit = element.Name;
            }
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
            DataGridViewStyleHelper.ResetDataGridView(dgv_SWDJ, true);

            DataTable dataTable = null;
            //加载实物登记数据【默认加载状态为1（未提交）的数据】
            if (string.IsNullOrEmpty(_querySql))
            {
                StringBuilder querySql = new StringBuilder("SELECT pc.trp_id, dd_name, trp_name, trp_code,trp_submit_status,trp_cd_amount");
                querySql.Append(" FROM transfer_registration_pc pc LEFT JOIN data_dictionary dd ON pc.com_id = dd.dd_id");
                querySql.Append($" WHERE trp_submit_status={(int)SubmitStatus.NonSubmit}");
                dataTable = SqlHelper.ExecuteQuery(querySql.ToString());
            }
            else
                dataTable = SqlHelper.ExecuteQuery(_querySql);

            dgv_SWDJ.Columns.AddRange(new DataGridViewColumn[]
            {
                new DataGridViewTextBoxColumn(){Name = "trp_id", HeaderText = "主键", FillWeight = 10 },
                new DataGridViewTextBoxColumn(){Name = "dd_name", HeaderText = "来源单位", FillWeight = 20 },
                new DataGridViewTextBoxColumn(){Name = "trp_name", HeaderText = "批次名称", FillWeight = 25 },
                new DataGridViewTextBoxColumn(){Name = "trp_code", HeaderText = "批次编号", FillWeight = 20 },
                new DataGridViewLinkColumn(){Name = "trp_cd_amount", HeaderText = "光盘数", FillWeight = 8 },
                new DataGridViewButtonColumn(){Name = "addpc", HeaderText = "添加光盘", FillWeight = 10, Text = "添加", UseColumnTextForButtonValue = true },
                new DataGridViewButtonColumn(){Name = "submit", HeaderText = "提交", FillWeight = 10 },
            });
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                DataRow row = dataTable.Rows[i];
                int index = dgv_SWDJ.Rows.Add();
                dgv_SWDJ.Rows[index].Cells["trp_id"].Value = row["trp_id"];
                dgv_SWDJ.Rows[index].Cells["dd_name"].Value = row["dd_name"];
                dgv_SWDJ.Rows[index].Cells["trp_name"].Value = row["trp_name"];
                dgv_SWDJ.Rows[index].Cells["trp_code"].Value = row["trp_code"];
                dgv_SWDJ.Rows[index].Cells["trp_cd_amount"].Value = row["trp_cd_amount"];
                dgv_SWDJ.Rows[index].Cells["submit"].Value = Convert.ToInt32(row["trp_submit_status"]) == 1 ? "提交" : "已提交";
            }

            //设置链接按钮样式
            DataGridViewStyleHelper.SetAlignWithCenter(dgv_SWDJ, new string[] { "trp_cd_amount" });
            dgv_SWDJ.Columns["trp_id"].Visible = false;
            btn_Back.Enabled = false;
            btn_Add.Enabled = true;

            dgv_SWDJ.Tag = "PC";
        }
     
        /// <summary>
        /// 加载光盘数据
        /// </summary>
        /// <param name="pid">批次主键</param>
        private void LoadCDDataScoure(object pid)
        {
            DataGridViewStyleHelper.ResetDataGridView(dgv_SWDJ, true);
            dgv_SWDJ.Columns.AddRange(new DataGridViewColumn[]
            {
                new DataGridViewTextBoxColumn(){ Name = "trc_id", HeaderText = "主键"},
                new DataGridViewTextBoxColumn(){ Name = "trc_code", HeaderText = "光盘编号", FillWeight = 8},
                new DataGridViewTextBoxColumn(){ Name = "trc_name", HeaderText = "光盘名称", FillWeight = 10},
                new DataGridViewTextBoxColumn(){ Name = "trc_remark", HeaderText = "备注", FillWeight = 20},
            });
            string querySql = $"SELECT trc_id, trc_name, trc_code, trc_remark FROM transfer_registraion_cd WHERE trp_id='{pid}' ORDER BY trc_sort";
            DataTable dataTable = SqlHelper.ExecuteQuery(querySql);

            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                DataRow row = dataTable.Rows[i];
                int index = dgv_SWDJ.Rows.Add();
                dgv_SWDJ.Rows[index].Cells["trc_id"].Value = row["trc_id"];
                dgv_SWDJ.Rows[index].Cells["trc_code"].Value = row["trc_code"];
                dgv_SWDJ.Rows[index].Cells["trc_name"].Value = row["trc_name"];
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
            if (e.RowIndex != -1 && e.ColumnIndex != -1)
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
                            querySql.Append("pc.trp_id, dd_name, trp_name, trp_code, trp_submit_status, trp_cd_amount");
                            querySql.Append(" FROM transfer_registration_pc pc,data_dictionary cs");
                            querySql.Append(" WHERE pc.com_id = cs.dd_id AND trp_submit_status=1");
                            querySql.Append(" AND com_id='" + currentUnit + "'");
                            LoadPCDataScoure(querySql.ToString());
                            dgv_SWDJ.Tag = SqlHelper.ExecuteOnlyOneQuery($"SELECT cs_code FROM data_dictionary WHERE dd_id ='{currentUnit}'");
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
                        string querySql = $"SELECT COUNT(trc_id) FROM transfer_registraion_cd WHERE trp_id='{currentRowId}' AND trc_status<>{(int)ReadStatus.ReadSuccess}";
                        int logAmount =SqlHelper.ExecuteCountQuery(querySql);
                        if (logAmount == 0)
                        {
                            if (XtraMessageBox.Show("确定要提交当前选中项吗？", "确认提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                string updateSql = $"UPDATE transfer_registration_pc SET trp_submit_status={(int)ObjectSubmitStatus.SubmitSuccess} WHERE trp_id='{currentRowId}'";
                                SqlHelper.ExecuteNonQuery(updateSql);

                                XtraMessageBox.Show("提交成功。");
                                dgv_SWDJ.Rows.RemoveAt(e.RowIndex);
                            }
                        }
                        else
                            XtraMessageBox.Show("当前批次下存在尚未处理的光盘。", "提交失败", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                }
            }
        }
     
        /// <summary>
        /// 返回上一页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Back_Click(object sender, EventArgs e) => LoadPCDataScoure(null);
   
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
                if (XtraMessageBox.Show("确定要删除选中的数据吗?", "确认提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk) == DialogResult.OK)
                {
                    int deleteAmount = 0;
                    if ("PC".Equals(dgv_SWDJ.Tag))
                    {
                        StringBuilder sb = new StringBuilder();
                        foreach (DataGridViewRow row in dgv_SWDJ.SelectedRows)
                        {
                            object pid = row.Cells["trp_id"].Value;
                            sb.Append($"DELETE FROM transfer_registration_pc WHERE trp_id = '{pid}';");
                            deleteAmount++;
                        }
                        SqlHelper.ExecuteNonQuery(sb.ToString());
                        LoadPCDataScoure(null);
                    }
                    else if ("CD".Equals(dgv_SWDJ.Tag))
                    {
                        StringBuilder sb = new StringBuilder();
                        object pid = null;
                        foreach (DataGridViewRow row in dgv_SWDJ.SelectedRows)
                        {
                            object cid = row.Cells["trc_id"].Value;
                            sb.Append($"DELETE FROM transfer_registraion_cd WHERE trc_id = '{cid}';");
                            deleteAmount++;
                        }
                        sb.Append($"UPDATE transfer_registration_pc SET trp_cd_amount=(SELECT COUNT(trc_id) FROM transfer_registraion_cd WHERE trp_id = '{pid}') WHERE trp_id = '{pid}';");
                        SqlHelper.ExecuteNonQuery(sb.ToString());
                        LoadCDDataScoure(pid);
                    }
                    XtraMessageBox.Show(deleteAmount + "条数据已被删除!", "操作成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                XtraMessageBox.Show("请先至少选择一条要删除的数据!", "尚未选择数据", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
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
                querySql.Append("dd_name,");
                querySql.Append("trp_name,");
                querySql.Append("trp_code,");
                querySql.Append("trp_cd_amount,");
                querySql.Append("trp_submit_status");
                querySql.Append(" FROM transfer_registration_pc pc LEFT JOIN data_dictionary cs ON pc.com_id = cs.dd_id");
                querySql.Append(" WHERE dd_name LIKE '%" + searchKey + "%' ");
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
            int index = tc_ToR.SelectedTabPageIndex;
            if(index == 1)//光盘
            {
                Cbo_Status_SelectionChangeCommitted(null, null);
            }
        }
        
        /// <summary>
        /// 加载光盘列表
        /// </summary>
        private void LoadGPDJ(string _querySql)
        {
            dgv_GPDJ.Rows.Clear();
            dgv_GPDJ.Columns.Clear();

            dgv_GPDJ.Columns.AddRange(new DataGridViewColumn[]
            {
                new DataGridViewTextBoxColumn(){Name = "trc_id"},
                new DataGridViewTextBoxColumn(){Name = "dd_name", HeaderText = "来源单位", FillWeight = 15 },
                new DataGridViewTextBoxColumn(){Name = "trc_code", HeaderText = "光盘编号", FillWeight = 15 },
                new DataGridViewTextBoxColumn(){Name = "trc_name", HeaderText = "光盘名称", FillWeight = 15 },
                new DataGridViewTextBoxColumn(){Name = "trc_project_amount", HeaderText = "项目数", FillWeight = 6 },
                new DataGridViewTextBoxColumn(){Name = "trc_subject_amount", HeaderText = "课题数", FillWeight = 6 },
                new DataGridViewTextBoxColumn(){Name = "trc_file_amount", HeaderText = "文件数", FillWeight = 6 },
                new DataGridViewTextBoxColumn(){Name = "trc_status", HeaderText = "读写状态", FillWeight = 10 },
                new DataGridViewButtonColumn(){Name = "control", HeaderText = "操作", FillWeight = 7, Text = "读写", UseColumnTextForButtonValue = true },
            });

            DataTable table = null;
            if (_querySql == null)
            {
                StringBuilder querySql = new StringBuilder("SELECT trc_id, trp_submit_status, dd_name, trc_code, trc_name, trc_status");
                querySql.Append(" FROM transfer_registraion_cd trc LEFT JOIN(");
                querySql.Append(" SELECT trp.trp_id, trp.trp_submit_status, dd_name, dd_sort FROM transfer_registration_pc trp, data_dictionary dd WHERE trp.com_id = dd.dd_id ) tb");
                querySql.Append(" ON trc.trp_id = tb.trp_id WHERE 1=1 AND trp_submit_status=1 ORDER BY CASE WHEN dd_name IS NULL THEN 1 ELSE 0 END, trc_status ASC, dd_sort, trc_sort");
                table = SqlHelper.ExecuteQuery(querySql.ToString());
            }
            else
                table = SqlHelper.ExecuteQuery(_querySql);
            foreach (DataRow row in table.Rows)
            {
                int _index = dgv_GPDJ.Rows.Add();
                dgv_GPDJ.Rows[_index].Cells["trc_id"].Value = row["trc_id"];
                dgv_GPDJ.Rows[_index].Cells["dd_name"].Value = row["dd_name"];
                dgv_GPDJ.Rows[_index].Cells["trc_code"].Value = row["trc_code"];
                dgv_GPDJ.Rows[_index].Cells["trc_name"].Value = row["trc_name"];
                dgv_GPDJ.Rows[_index].Cells["trc_project_amount"].Value = GetProjectAmount(row["trc_id"]);
                dgv_GPDJ.Rows[_index].Cells["trc_subject_amount"].Value = GetSubjectAmount(row["trc_id"]);
                dgv_GPDJ.Rows[_index].Cells["trc_file_amount"].Value = GetFileAmount(row["trc_id"]);
                dgv_GPDJ.Rows[_index].Cells["trc_status"].Tag = row["trc_status"];
                dgv_GPDJ.Rows[_index].Cells["trc_status"].Value = GetReadStatus(GetInt32(row["trc_status"]));
            }
            if (dgv_GPDJ.Columns.Count > 0)
                dgv_GPDJ.Columns[0].Visible = false;
        }
    
        /// <summary>
        /// 根据光盘ID获取文件数
        /// </summary>
        /// <param name="cdid">光盘ID</param>
        private object GetFileAmount(object cdid) => SqlHelper.ExecuteOnlyOneQuery($"SELECT COUNT(bfi_id) FROM backup_files_info WHERE bfi_type=0 AND bfi_trcid='{cdid}'");

        /// <summary>
        /// 根据光盘ID获取项目数总和
        /// </summary>
        private int GetProjectAmount(object trcId) => SqlHelper.ExecuteCountQuery($"SELECT COUNT(pi_id) FROM project_info WHERE trc_id = '{trcId}'");

        /// <summary>
        /// 根据光盘ID获取课题数总和
        /// </summary>
        private object GetSubjectAmount(object trcId) => SqlHelper.ExecuteCountQuery($"SELECT COUNT(ti_id) FROM topic_info WHERE trc_id ='{trcId}'");

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
            string key = txt_CDSearch.Text;
            string queryCondition = null;
            if (!string.IsNullOrEmpty(key))
                queryCondition = " WHERE dd_name LIKE '%" + key + "%' OR trc_code LIKE '%" + key + "%' OR trc_name LIKE '%" + key + "%'";

            StringBuilder querySql = new StringBuilder("SELECT trc_id,dd_name,trc_code,trc_name,trc_project_amount,trc_subject_amount,trc_status");
            querySql.Append(" FROM transfer_registraion_cd trc LEFT JOIN(");
            querySql.Append(" SELECT trp.trp_id, dd_name, dd_sort FROM transfer_registration_pc trp, data_dictionary cs WHERE trp.com_id = cs.dd_id ) tb");
            querySql.Append(" ON trc.trp_id = tb.trp_id");
            querySql.Append(queryCondition);
            querySql.Append(" ORDER BY CASE WHEN dd_name IS NULL THEN 1 ELSE 0 END, trc_status ASC, dd_sort ASC");

            LoadGPDJ(querySql.ToString());
        }

        /// <summary>
        /// 光盘页单元格点击事件
        /// </summary>
        private void Dgv_GPDJ_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex != -1 && e.ColumnIndex != -1)
            {
                object trcId = dgv_GPDJ.Rows[e.RowIndex].Cells["trc_id"].Value;
                if(trcId != null)
                {
                    if("control".Equals(dgv_GPDJ.Columns[e.ColumnIndex].Name))
                    {
                        int state = Convert.ToInt32(dgv_GPDJ.Rows[e.RowIndex].Cells["trc_status"].Tag);
                        if(state == 2)
                        {
                            string msg = "此光盘已读取，重新读取会覆盖旧数据。\r\n是否确认继续?";
                            if(XtraMessageBox.Show(msg, "确认提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                SqlHelper.ExecuteNonQuery($"DELETE FROM backup_files_info WHERE bfi_trcid='{trcId}'");
                            else
                                return;
                        }
                        Frm_CDRead read = new Frm_CDRead(trcId);
                        if(read.ShowDialog() == DialogResult.OK)
                        {
                            //更新光盘信息
                            string updateSql = $"UPDATE transfer_registraion_cd SET trc_status='{(int)ReadStatus.ReadSuccess}' WHERE trc_id='{trcId}'";
                            SqlHelper.ExecuteNonQuery(updateSql);
                            LoadGPDJ(null);
                        }
                    }
                }
            }
        }

        private void Cbo_Status_SelectionChangeCommitted(object sender, EventArgs e)
        {
            int index = cbo_Status.SelectedIndex;
            if (index != -1)
            {
                StringBuilder querySql = new StringBuilder("SELECT trc_id, tb.trp_submit_status, dd_name, trc_code, trc_name, trc_project_amount, trc_subject_amount, trc_status");
                querySql.Append(" FROM transfer_registraion_cd trc LEFT JOIN(");
                querySql.Append(" SELECT trp.trp_id, trp.trp_submit_status, dd_id, dd_name, dd_sort FROM transfer_registration_pc trp, data_dictionary cs WHERE trp.com_id = cs.dd_id ) tb");
                querySql.Append(" ON trc.trp_id = tb.trp_id WHERE 1=1 AND trp_submit_status=1");
                if (index != 0) querySql.Append(" AND trc_status='" + index + "'");
                querySql.Append(" ORDER BY CASE WHEN dd_name IS NULL THEN 1 ELSE 0 END, trc_status ASC, dd_sort ASC");
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
                if (XtraMessageBox.Show("确定要删除选中的数据吗?", "确认提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk) == DialogResult.OK)
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
                    XtraMessageBox.Show(deleteAmount + "条数据已被删除!", "操作成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                XtraMessageBox.Show("请先至少选择一条要删除的数据!", "尚未选择数据", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }

        private void Txt_Search_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if(e.Button.Index == 1)
                btn_Search_Click(sender, e);
        }

        private void txt_Search_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
                btn_Search_Click(sender, e);
        }

        private void SearchControl1_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if(e.Button.Index == 1)
                Btn_CD_Search_Click(sender, e);
        }

        private void txt_CDSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
                btn_Search_Click(sender, e);
        }
    }
}
