using DevExpress.XtraBars.Navigation;
using DevExpress.XtraEditors;
using System;
using System.Data;
using System.Text;
using System.Windows.Forms;
using 科技计划项目档案数据采集管理系统.TransferOfRegistration;

namespace 科技计划项目档案数据采集管理系统
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

            dgv_SWDJ.ColumnHeadersDefaultCellStyle = DataGridViewStyleHelper.GetHeaderStyle();
            dgv_GPDJ.ColumnHeadersDefaultCellStyle = DataGridViewStyleHelper.GetHeaderStyle();

            dgv_SWDJ.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgv_GPDJ.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
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
                    StringBuilder querySql = new StringBuilder("SELECT pc.trp_id, dd_name, trp_name, trp_code, trp_submit_status, trp_cd_amount FROM transfer_registration_pc pc " +
                        "LEFT JOIN data_dictionary dd ON pc.com_id = dd.dd_id WHERE com_id='" + element.Name + "' ");
                    if(!chk_AllP.Checked)
                        querySql.Append("AND pc.com_id = dd.dd_id AND trp_submit_status=1");
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
                StringBuilder querySql = new StringBuilder("SELECT pc.trp_id, dd_name, trp_name, trp_code,trp_submit_status,trp_cd_amount " +
                    "FROM transfer_registration_pc pc LEFT JOIN data_dictionary dd ON pc.com_id = dd.dd_id WHERE 1=1 ");
                if(!chk_AllP.Checked)
                    querySql.Append($"AND trp_submit_status={(int)SubmitStatus.NonSubmit} ");
                querySql.Append("ORDER BY trp_submit_status, trp_code;");
                dataTable = SqlHelper.ExecuteQuery(querySql.ToString());
            }
            else
                dataTable = SqlHelper.ExecuteQuery(_querySql);

            dgv_SWDJ.Columns.AddRange(new DataGridViewColumn[]
            {
                new DataGridViewTextBoxColumn(){Name = "trp_id", HeaderText = "主键", FillWeight = 10 , SortMode = DataGridViewColumnSortMode.NotSortable },
                new DataGridViewTextBoxColumn(){Name = "dd_name", HeaderText = "来源单位", FillWeight = 20, SortMode = DataGridViewColumnSortMode.NotSortable  },
                new DataGridViewTextBoxColumn(){Name = "trp_name", HeaderText = "批次名称", FillWeight = 25, SortMode = DataGridViewColumnSortMode.NotSortable  },
                new DataGridViewTextBoxColumn(){Name = "trp_code", HeaderText = "批次编号", FillWeight = 20, SortMode = DataGridViewColumnSortMode.NotSortable  },
                new DataGridViewLinkColumn(){Name = "trp_cd_amount", HeaderText = "光盘数", FillWeight = 8, SortMode = DataGridViewColumnSortMode.NotSortable  },
                new DataGridViewButtonColumn(){Name = "addpc", HeaderText = "添加光盘", FillWeight = 10, Text = "添加", UseColumnTextForButtonValue = true, SortMode = DataGridViewColumnSortMode.NotSortable  },
                new DataGridViewButtonColumn(){Name = "submit", HeaderText = "提交", FillWeight = 10 , SortMode = DataGridViewColumnSortMode.NotSortable },
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

            btn_Delete.Tag = 1;
        }

        /// <summary>
        /// 批次ID，批次名称【仅针对光盘列表】
        /// </summary>
        object TrpId = null, TrpName = null;
        /// <summary>
        /// 加载光盘数据
        /// </summary>
        /// <param name="trpId">批次主键</param>
        private void LoadCDDataScoure(object trpId, object trpName)
        {
            DataGridViewStyleHelper.ResetDataGridView(dgv_SWDJ, true);
            dgv_SWDJ.Columns.AddRange(new DataGridViewColumn[]
            {
                new DataGridViewTextBoxColumn(){ Name = "trc_id", HeaderText = "主键", SortMode = DataGridViewColumnSortMode.NotSortable },
                new DataGridViewTextBoxColumn(){ Name = "trc_code", HeaderText = "光盘编号", FillWeight = 8, SortMode = DataGridViewColumnSortMode.NotSortable },
                new DataGridViewTextBoxColumn(){ Name = "trc_name", HeaderText = "光盘名称", FillWeight = 10, SortMode = DataGridViewColumnSortMode.NotSortable },
                new DataGridViewTextBoxColumn(){ Name = "trc_remark", HeaderText = "备注", FillWeight = 20, SortMode = DataGridViewColumnSortMode.NotSortable },
                new DataGridViewButtonColumn(){ Name = "trc_read", HeaderText = "操作", FillWeight = 5, Text = "读写", UseColumnTextForButtonValue = true, SortMode = DataGridViewColumnSortMode.NotSortable },
            });
            string querySql = $"SELECT trc_id, trc_name, trc_code, trc_remark FROM transfer_registraion_cd WHERE trp_id='{trpId}' ORDER BY trc_sort";
            DataTable dataTable = SqlHelper.ExecuteQuery(querySql);

            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                DataRow row = dataTable.Rows[i];
                int index = dgv_SWDJ.Rows.Add();
                dgv_SWDJ.Rows[index].Cells["trc_id"].Value = row["trc_id"];
                dgv_SWDJ.Rows[index].Cells["trc_code"].Value = row["trc_code"];
                dgv_SWDJ.Rows[index].Cells["trc_name"].Value = row["trc_name"];
                dgv_SWDJ.Rows[index].Cells["trc_remark"].Value = row["trc_remark"];
                dgv_SWDJ.Rows[index].Tag = trpId;
                dgv_SWDJ.Rows[index].Cells["trc_read"].Tag = trpName;
            }
            dgv_SWDJ.Columns["trc_id"].Visible = false;

            btn_Back.Enabled = true;
            btn_Add.Enabled = false;

            btn_Delete.Tag = 2;
            this.TrpId = trpId;
            this.TrpName = trpName;
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
                string name = dgv_SWDJ.Columns[e.ColumnIndex].Name;
                //光盘数-点击事件
                if ("trp_cd_amount".Equals(name))
                {
                    if (Convert.ToInt32(value) != 0)
                    {
                        object trpId = dgv_SWDJ.Rows[e.RowIndex].Cells["trp_id"].Value;
                        object trpName = dgv_SWDJ.Rows[e.RowIndex].Cells["trp_name"].Value;
                        LoadCDDataScoure(trpId, trpName);
                    }
                }
                //添加光盘-点击事件
                else if ("addpc".Equals(name))
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
                //提交 - 点击事件
                else if ("submit".Equals(name))
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
                //光盘读写事件
                else if("trc_read".Equals(name))
                {
                    object trcId = dgv_SWDJ.Rows[e.RowIndex].Cells["trc_id"].Value;
                    int count = SqlHelper.ExecuteCountQuery($"SELECT COUNT(bfi_id) FROM backup_files_info WHERE bfi_trcid='{trcId}'");
                    if(count > 0)
                    {
                        if(XtraMessageBox.Show("此光盘已读取，重复读取会删除旧数据，是否继续？", "确认提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk) == DialogResult.OK)
                            SqlHelper.ExecuteNonQuery($"DELETE FROM backup_files_info WHERE bfi_trcid='{trcId}'");
                        else
                            return;
                    }
                    object trpName = dgv_SWDJ.Rows[e.RowIndex].Cells["trc_read"].Tag;
                    Frm_CDRead read = new Frm_CDRead(trcId, trpName);
                    if(read.ShowDialog() == DialogResult.OK)
                    {
                        StringBuilder sb = new StringBuilder();
                        //更新光盘信息
                        //如果当前光盘所属批次已提交且光盘是首次被读取，则证明是批次后补录光盘，则已录批次下所有项目课题均关联此光盘ID
                        int pstate = (int)SqlHelper.ExecuteOnlyOneQuery($"SELECT trp_submit_status FROM transfer_registration_pc WHERE trp_id=" +
                            $"(SELECT trp_id FROM transfer_registraion_cd WHERE trc_id='{trcId}');");
                        int cstate = (int)SqlHelper.ExecuteOnlyOneQuery($"SELECT trc_status FROM transfer_registraion_cd WHERE trc_id='{trcId}';");
                        if(pstate == 2 && cstate == 1)
                        {
                            /*-- 批次下的重大专项 - 项目/课题 --*/
                            object trpId = dgv_SWDJ.Rows[e.RowIndex].Tag;
                            object impId = SqlHelper.ExecuteOnlyOneQuery($"SELECT imp_id FROM imp_dev_info WHERE imp_obj_id=(SELECT imp_id FROM imp_info WHERE imp_obj_id = '{trpId}');");
                            if(impId != null)
                                UpdateTrcId(impId, trcId, ref sb);

                            /*-- 批次下的计划 - 项目/课题 --*/
                            object piId = SqlHelper.ExecuteOnlyOneQuery($"SELECT pi_id FROM project_info WHERE trc_id='{trpId}'");
                            if(piId != null)
                                UpdateTrcId(piId, trcId, ref sb);
                        }
                        sb.Append($"UPDATE transfer_registraion_cd SET trc_status='{(int)ReadStatus.ReadSuccess}' WHERE trc_id='{trcId}';");
                        SqlHelper.ExecuteNonQuery(sb.ToString());
                        XtraMessageBox.Show("读写成功。");
                    }
                }
            }
        }

        /// <summary>
        /// 将已存在数据更新绑定光盘编号为新增光盘
        /// </summary>
        /// <param name="parentId">父级ID</param>
        /// <param name="trcId">光盘ID</param>
        private void UpdateTrcId(object parentId, object trcId, ref StringBuilder sb)
        {
            object[] proList = SqlHelper.ExecuteSingleColumnQuery($"SELECT pi_id FROM project_info WHERE pi_obj_id='{parentId}' UNION ALL " +
                                $"SELECT ti_id FROM topic_info WHERE ti_obj_id = '{parentId}'; ");
            foreach(object proId in proList)
            {
                sb.Append($"UPDATE project_info SET trc_id=ISNULL(trc_id,'')+'{trcId};' WHERE pi_id='{proId}'; " +
                    $"UPDATE topic_info SET trc_id=ISNULL(trc_id,'')+'{trcId};' WHERE ti_id='{proId}'; ");
                object[] topList = SqlHelper.ExecuteSingleColumnQuery($"SELECT si_id FROM subject_info WHERE si_obj_id='{proId}' UNION ALL " +
                $"SELECT ti_id FROM topic_info WHERE ti_obj_id = '{proId}'; ");
                foreach(object topId in topList)
                {
                    sb.Append($"UPDATE subject_info SET trc_id=ISNULL(trc_id,'')+'{trcId};' WHERE si_id='{topId}'; " +
                    $"UPDATE topic_info SET trc_id=ISNULL(trc_id,'')+'{trcId};' WHERE ti_id='{topId}'; ");
                    object[] subList = SqlHelper.ExecuteSingleColumnQuery($"SELECT si_id FROM subject_info WHERE si_obj_id='{topId}';");
                    foreach(object subId in subList)
                    {
                        sb.Append($"UPDATE subject_info SET trc_id=ISNULL(trc_id,'')+'{trcId};' WHERE si_id='{subId}'; ");
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
                int index = ToolHelper.GetIntValue(btn_Delete.Tag, -1);
                string tipString = $"此操作会删除选中{(index == 1 ? "批次" : "光盘")}下所有已存在数据，是否确认继续？";
                if(XtraMessageBox.Show(tipString, "删除确认", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                {
                    int type = tc_ToR.SelectedTabPageIndex;
                    if (type == 0)
                    {
                        if (index == 1)
                        {
                            foreach (DataGridViewRow row in dgv_SWDJ.SelectedRows)
                            {
                                object pid = row.Cells["trp_id"].Value;
                                DeleteBatchById(pid);
                            }
                            LoadPCDataScoure(null);
                        }
                        else if (index == 2)
                        {
                            foreach (DataGridViewRow row in dgv_SWDJ.SelectedRows)
                            {
                                object cid = row.Cells["trc_id"].Value;
                                DeleteCDById(cid);
                            }
                            RefreshCDAmountByPid(TrpId);
                            LoadCDDataScoure(TrpId, TrpName);
                        }
                    }
                    //else if(type == 1)
                    //{
                    //    StringBuilder sb = new StringBuilder();
                    //    object pid = null, pname = null;
                    //    foreach(DataGridViewRow row in dgv_SWDJ.SelectedRows)
                    //    {
                    //        object cid = row.Cells["trc_id"].Value;
                    //        pid = row.Tag;
                    //        pname = row.Cells["trc_read"].Tag;
                    //        sb.Append($"DELETE FROM work_registration WHERE wr_type=2 AND wr_obj_id='{cid}';");
                    //        sb.Append($"DELETE FROM transfer_registraion_cd WHERE trc_id = '{cid}';");
                    //    }
                    //    sb.Append($"UPDATE transfer_registration_pc SET trp_cd_amount=(SELECT COUNT(trc_id) FROM transfer_registraion_cd WHERE trp_id = '{pid}') WHERE trp_id = '{pid}';");
                    //    SqlHelper.ExecuteNonQuery(sb.ToString());
                    //    LoadCDDataScoure(pid, pname);
                    //}
                    XtraMessageBox.Show("删除成功。", "提示");
                }
            }
            else
            {
                XtraMessageBox.Show("请先至少选择一条要删除的数据!", "尚未选择数据", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }

        /// <summary>
        /// 刷新批次下的光盘数
        /// </summary>
        /// <param name="trpId">批次ID</param>
        private void RefreshCDAmountByPid(object trpId)
        {
            string updateSQL = "UPDATE transfer_registration_pc SET trp_cd_amount= " +
                "(SELECT COUNT(trc_id) FROM transfer_registraion_cd WHERE trp_id = '" + trpId + "') WHERE trp_id = '" + trpId + "';";
            SqlHelper.ExecuteNonQuery(updateSQL);
        }

        /// <summary>
        /// 删除光盘信息
        /// </summary>
        /// <param name="cid">光盘ID</param>
        private void DeleteCDById(object cid)
        {
            StringBuilder stringBuilder = new StringBuilder();
            //删除当前光盘下的文件记录
            stringBuilder.Append($"DELETE FROM backup_files_info WHERE bfi_trcid='{cid}';");
            //删除当前光盘记录
            stringBuilder.Append($"DELETE FROM transfer_registraion_cd WHERE trc_id='{cid}';");
            SqlHelper.ExecuteNonQuery(stringBuilder.ToString());
        }

        /// <summary>
        /// 删除指定批次下所有数据
        /// </summary>
        /// <param name="pid">批次ID</param>
        private void DeleteBatchById(object pid)
        {
            string deleteSQL = $"DELETE FROM transfer_registration_pc WHERE trp_id='{pid}';";
            object[] wrIds = SqlHelper.ExecuteSingleColumnQuery($"SELECT wr_id FROM work_registration WHERE trp_id='{pid}'");
            //删除【加工登记】记录
            deleteSQL += $"DELETE FROM work_registration WHERE trp_id='{pid}';";
            for(int i = 0; i < wrIds.Length; i++)
                //删除【档案质检】记录
                deleteSQL += $"DELETE FROM work_myreg WHERE wr_id='{wrIds[i]}';";

            object impId = SqlHelper.ExecuteOnlyOneQuery($"SELECT imp_id FROM imp_info WHERE imp_obj_id='{pid}'");
            if(impId != null)//专项
            {
                object speId = SqlHelper.ExecuteOnlyOneQuery($"SELECT imp_id FROM imp_dev_info WHERE imp_obj_id='{impId}'");
                //删除【专项】记录
                deleteSQL += $"DELETE FROM processing_file_list WHERE pfl_obj_id='{impId}';";
                deleteSQL += $"DELETE FROM processing_file_list WHERE pfl_obj_id='{speId}';";
                deleteSQL += $"DELETE FROM imp_dev_info WHERE imp_id='{speId}';";
                deleteSQL += $"DELETE FROM imp_info WHERE imp_id='{impId}';";

                object[] projectIds = SqlHelper.ExecuteSingleColumnQuery($"SELECT pi_id FROM (" +
                     "SELECT pi_id, pi_obj_id FROM project_info WHERE pi_categor=2 UNION ALL " +
                    $"SELECT ti_id, ti_obj_id FROM topic_info WHERE ti_categor=-3) A WHERE A.pi_obj_id='{speId}'");
                //删除【项目/课题】记录
                deleteSQL += $"DELETE FROM project_info WHERE pi_categor=2 AND pi_obj_id='{speId}';" +
                             $"DELETE FROM topic_info WHERE ti_categor=-3 AND ti_obj_id='{speId}';";
                for(int i = 0; i < projectIds.Length; i++)
                {
                    deleteSQL += $"DELETE FROM processing_file_list WHERE pfl_obj_id='{projectIds[i]}';";
                    object[] topicIds = SqlHelper.ExecuteSingleColumnQuery($"SELECT ti_id FROM (" +
                         "SELECT ti_id, ti_obj_id FROM topic_info WHERE ti_categor=3 UNION ALL " +
                        $"SELECT si_id, si_obj_id FROM subject_info) A WHERE A.ti_obj_id='{projectIds[i]}'");
                    //删除【课题/子课题】记录
                    deleteSQL += $"DELETE FROM topic_info WHERE ti_categor=3 AND ti_obj_id='{projectIds[i]}';" +
                                 $"DELETE FROM subject_info WHERE si_obj_id='{projectIds[i]}';";
                    for(int j = 0; j < topicIds.Length; j++)
                    {
                        deleteSQL += $"DELETE FROM processing_file_list WHERE pfl_obj_id='{topicIds[j]}';";
                        object[] subjectIds = SqlHelper.ExecuteSingleColumnQuery($"SELECT si_id FROM subject_info WHERE si_obj_id='{topicIds[j]}'");
                        deleteSQL += $"DELETE FROM subject_info WHERE si_obj_id='{topicIds[j]}';";
                        for(int k = 0; k < subjectIds.Length; k++)
                        {
                            deleteSQL += $"DELETE FROM processing_file_list WHERE pfl_obj_id='{subjectIds[k]}';";
                        }
                    }
                }

            }
            else//计划
            {
                object planId = SqlHelper.ExecuteOnlyOneQuery($"SELECT pi_id FROM project_info WHERE pi_categor=1 AND trc_id='{pid}'");
                if(planId != null)
                {
                    //删除【计划】记录
                    deleteSQL += $"DELETE FROM processing_file_list WHERE pfl_obj_id='{planId}';";
                    deleteSQL += $"DELETE FROM project_info WHERE pi_id='{planId}';";
                    object[] projectIds = SqlHelper.ExecuteSingleColumnQuery($"SELECT pi_id FROM (" +
                         "SELECT pi_id, pi_obj_id FROM project_info WHERE pi_categor=2 UNION ALL " +
                        $"SELECT ti_id, ti_obj_id FROM topic_info WHERE ti_categor=-3) A WHERE A.pi_obj_id='{planId}'");
                    deleteSQL += $"DELETE FROM project_info WHERE pi_categor=2 AND pi_obj_id='{planId}';" +
                                 $"DELETE FROM topic_info WHERE ti_categor=-3 AND ti_obj_id='{planId}';";
                    for(int i = 0; i < projectIds.Length; i++)
                    {
                        deleteSQL += $"DELETE FROM processing_file_list WHERE pfl_obj_id='{projectIds[i]}';";
                        object[] topicIds = SqlHelper.ExecuteSingleColumnQuery($"SELECT ti_id FROM (" +
                             "SELECT ti_id, ti_obj_id FROM topic_info WHERE ti_categor=3 UNION ALL " +
                            $"SELECT si_id, si_obj_id FROM subject_info) A WHERE A.ti_obj_id='{projectIds[i]}'");
                        deleteSQL += $"DELETE FROM topic_info WHERE ti_categor=3 AND ti_obj_id='{projectIds[i]}';" +
                                     $"DELETE FROM subject_info WHERE si_obj_id='{projectIds[i]}';";
                        for(int j = 0; j < topicIds.Length; j++)
                        {
                            deleteSQL += $"DELETE FROM processing_file_list WHERE pfl_obj_id='{topicIds[j]}';";
                            object[] subjectIds = SqlHelper.ExecuteSingleColumnQuery($"SELECT si_id FROM subject_info WHERE si_obj_id='{topicIds[j]}'");
                            deleteSQL += $"DELETE FROM subject_info WHERE si_obj_id='{topicIds[j]}';";
                            for(int k = 0; k < subjectIds.Length; k++)
                            {
                                deleteSQL += $"DELETE FROM processing_file_list WHERE pfl_obj_id='{subjectIds[k]}';";
                            }
                        }
                    }
                }
            }
            SqlHelper.ExecuteNonQuery(deleteSQL);
        }

        /// <summary>
        /// 搜索
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Search_Click(object sender, EventArgs e)
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
                new DataGridViewTextBoxColumn(){Name = "dd_name", HeaderText = "来源单位", FillWeight = 15 , SortMode = DataGridViewColumnSortMode.NotSortable },
                new DataGridViewTextBoxColumn(){Name = "trc_code", HeaderText = "光盘编号", FillWeight = 15, SortMode = DataGridViewColumnSortMode.NotSortable  },
                new DataGridViewTextBoxColumn(){Name = "trc_name", HeaderText = "光盘名称", FillWeight = 15 , SortMode = DataGridViewColumnSortMode.NotSortable },
                new DataGridViewTextBoxColumn(){Name = "trc_project_amount", HeaderText = "项目数", FillWeight = 6, SortMode = DataGridViewColumnSortMode.NotSortable  },
                new DataGridViewTextBoxColumn(){Name = "trc_subject_amount", HeaderText = "课题数", FillWeight = 6, SortMode = DataGridViewColumnSortMode.NotSortable  },
                new DataGridViewTextBoxColumn(){Name = "trc_file_amount", HeaderText = "文件数", FillWeight = 6, SortMode = DataGridViewColumnSortMode.NotSortable  },
                new DataGridViewTextBoxColumn(){Name = "trc_status", HeaderText = "读写状态", FillWeight = 10, SortMode = DataGridViewColumnSortMode.NotSortable  },
                new DataGridViewButtonColumn(){Name = "control", HeaderText = "操作", FillWeight = 7, Text = "读写", UseColumnTextForButtonValue = true, SortMode = DataGridViewColumnSortMode.NotSortable  },
            });

            DataTable table = null;
            if (_querySql == null)
            {
                StringBuilder querySql = new StringBuilder("SELECT trc_id, trp_submit_status, dd_name, trc_code, trc_name, trc_status, trp_name " +
                    "FROM transfer_registraion_cd trc LEFT JOIN(" +
                    "SELECT trp.trp_id, trp.trp_submit_status, dd_name, dd_sort, trp_name FROM transfer_registration_pc trp, data_dictionary dd WHERE trp.com_id = dd.dd_id ) tb " +
                    "ON trc.trp_id = tb.trp_id WHERE 1=1 AND trp_submit_status=1 ORDER BY CASE WHEN dd_name IS NULL THEN 1 ELSE 0 END, trc_status ASC, dd_sort, trc_sort, trc_code");
                table = SqlHelper.ExecuteQuery(querySql.ToString());
            }
            else
                table = SqlHelper.ExecuteQuery(_querySql);
            foreach (DataRow row in table.Rows)
            {
                int _index = dgv_GPDJ.Rows.Add();
                dgv_GPDJ.Rows[_index].Tag = row["trp_name"];
                dgv_GPDJ.Rows[_index].Cells["trc_id"].Value = row["trc_id"];
                dgv_GPDJ.Rows[_index].Cells["dd_name"].Value = row["dd_name"];
                dgv_GPDJ.Rows[_index].Cells["trc_code"].Value = row["trc_code"];
                dgv_GPDJ.Rows[_index].Cells["trc_name"].Value = row["trc_name"];
                dgv_GPDJ.Rows[_index].Cells["trc_project_amount"].Value = GetProjectAmount(row["trc_id"]);
                dgv_GPDJ.Rows[_index].Cells["trc_subject_amount"].Value = GetSubjectAmount(row["trc_id"]);
                dgv_GPDJ.Rows[_index].Cells["trc_file_amount"].Value = GetFileAmount(row["trc_id"]);
                dgv_GPDJ.Rows[_index].Cells["trc_status"].Tag = row["trc_status"];
                int statuNum = ToolHelper.GetIntValue(row["trc_status"]);
                if (statuNum != 2)
                    dgv_GPDJ.Rows[_index].Cells["trc_status"].Style.ForeColor = System.Drawing.Color.DarkRed;
                dgv_GPDJ.Rows[_index].Cells["trc_status"].Value = GetReadStatus(statuNum);
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
        /// 光盘页搜索
        /// </summary>
        private void Btn_CD_Search_Click(object sender, EventArgs e)
        {
            string key = txt_CDSearch.Text;
            string queryCondition = null;
            if (!string.IsNullOrEmpty(key))
                queryCondition = " WHERE dd_name LIKE '%" + key + "%' OR trc_code LIKE '%" + key + "%' OR trc_name LIKE '%" + key + "%'";

            StringBuilder querySql = new StringBuilder("SELECT trc_id, dd_name, trc_code, trc_name, trc_project_amount, trc_subject_amount, trc_status, tb.trp_name");
            querySql.Append(" FROM transfer_registraion_cd trc LEFT JOIN(");
            querySql.Append(" SELECT trp.trp_id, dd_name, dd_sort, trp_name FROM transfer_registration_pc trp, data_dictionary cs WHERE trp.com_id = cs.dd_id ) tb");
            querySql.Append(" ON trc.trp_id = tb.trp_id");
            querySql.Append(queryCondition);
            querySql.Append(" ORDER BY CASE WHEN dd_name IS NULL THEN 1 ELSE 0 END, trc_status ASC, dd_sort ASC, trc_code");

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
                        object trpName = dgv_GPDJ.Rows[e.RowIndex].Tag;
                        Frm_CDRead read = new Frm_CDRead(trcId, trpName);
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
                StringBuilder querySql = new StringBuilder("SELECT trc_id, tb.trp_submit_status, dd_name, trc_code, trc_name, trc_project_amount, trc_subject_amount, trc_status, trp_name");
                querySql.Append(" FROM transfer_registraion_cd trc LEFT JOIN(");
                querySql.Append(" SELECT trp.trp_id, trp.trp_submit_status, dd_id, dd_name, dd_sort, trp_name FROM transfer_registration_pc trp, data_dictionary cs WHERE trp.com_id = cs.dd_id ) tb");
                querySql.Append(" ON trc.trp_id = tb.trp_id WHERE 1=1 AND trp_submit_status=1");
                if (index != 0) querySql.Append(" AND trc_status='" + index + "'");
                querySql.Append(" ORDER BY CASE WHEN dd_name IS NULL THEN 1 ELSE 0 END, trc_status ASC, dd_sort ASC, trc_code");
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
                Btn_Search_Click(sender, e);
        }

        private void txt_Search_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
                Btn_Search_Click(sender, e);
        }

        private void SearchControl1_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if(e.Button.Index == 1)
                Btn_CD_Search_Click(sender, e);
        }

        private void txt_CDSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
                Btn_Search_Click(sender, e);
        }

        private void chk_AllP_CheckedChanged(object sender, EventArgs e)
        {
            LoadPCDataScoure(null);
        }

        private void dgv_SWDJ_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex!=-1 && e.ColumnIndex != -1)
            {
                string name = dgv_SWDJ.Columns[e.ColumnIndex].Name;
                //批次名称-点击事件
                if("trp_name".Equals(name))
                {
                    object currentRowId = dgv_SWDJ.Rows[e.RowIndex].Cells["trp_id"].Value;
                    Frm_AddPC frm = new Frm_AddPC(false, currentRowId.ToString());
                    if(frm.ShowDialog() == DialogResult.OK)
                    {
                        LoadPCDataScoure(null);
                    }
                }
            }
        }
    }
}
