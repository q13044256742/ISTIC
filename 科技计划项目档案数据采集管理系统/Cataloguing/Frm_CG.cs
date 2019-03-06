using DevExpress.XtraBars.Navigation;
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace 科技计划项目档案数据采集管理系统
{
    public partial class Frm_CG : Form
    {
        /// <summary>
        /// 页码顺序记录
        /// </summary>
        private string[] LastIdLog;
        public Frm_CG()
        {
            InitializeComponent();
        }

        private void Frm_CG_Load(object sender, EventArgs e)
        {
            //默认来源单位
            LoadCompanyList();
            SetBackWorkNumber();
            ace_LeftMenu.SelectElement(ac_Login);
            comboBox1.SelectedIndex = 0;
            //列头样式
            view.ColumnHeadersDefaultCellStyle = DataGridViewStyleHelper.GetHeaderStyle();
            LoadPCList(null, null);
        }

        private void SetBackWorkNumber()
        {
            int num = SqlHelper.ExecuteCountQuery($"SELECT COUNT(wm_id) FROM work_myreg WHERE wm_status='{(int)QualityStatus.QualityBack}' AND wm_user='{UserHelper.GetUser().UserKey}'");
            ac_Worked.Text = $"已返工({num})";
        }

        /// <summary>
        /// 加载来源单位ComboBox下拉表
        /// </summary>
        private void LoadCompanyList()
        {
            DataTable table = SqlHelper.GetCompanyList();
            DataRow dataRow = table.NewRow();
            dataRow["dd_id"] = "all";
            dataRow["dd_name"] = "全部来源单位";
            table.Rows.InsertAt(dataRow, 0);
            cbo_CompanyList.DataSource = table;
            cbo_CompanyList.DisplayMember = "dd_name";
            cbo_CompanyList.ValueMember = "dd_id";
            cbo_CompanyList.SelectedIndex = 0;
        }

        /// <summary>
        /// 待领取批次列表
        /// </summary>
        /// <param name="querySql">指定的查询语句</param>
        /// <param name="csid">来源单位ID</param>
        private void LoadPCList(StringBuilder querySql, object csid)
        {
            DataGridViewStyleHelper.ResetDataGridView(view);
            view.Columns.AddRange(new DataGridViewColumn[]
            {
                new DataGridViewTextBoxColumn(){ Name = "trp_id"},
                new DataGridViewTextBoxColumn(){ Name = "dd_name", HeaderText= "来源单位", FillWeight = 18, SortMode = DataGridViewColumnSortMode.NotSortable },
                new DataGridViewTextBoxColumn(){ Name = "trp_name", HeaderText= "批次名称", FillWeight = 25, SortMode = DataGridViewColumnSortMode.NotSortable },
                new DataGridViewTextBoxColumn(){ Name = "trp_code", HeaderText= "批次编号", FillWeight = 20, SortMode = DataGridViewColumnSortMode.NotSortable },
                new DataGridViewTextBoxColumn(){ Name = "trp_finishtime", HeaderText= "完成时间", FillWeight = 10, SortMode = DataGridViewColumnSortMode.NotSortable },
                new DataGridViewLinkColumn(){ Name = "trp_cd_amount", HeaderText= "光盘数", FillWeight = 7, SortMode = DataGridViewColumnSortMode.NotSortable },
                new DataGridViewButtonColumn(){ Name = "trp_control", HeaderText= "操作", FillWeight = 8, Text = "领取", UseColumnTextForButtonValue = true, SortMode = DataGridViewColumnSortMode.NotSortable },
                new DataGridViewButtonColumn(){ Name = "trp_finish", HeaderText= "结束", FillWeight = 8, Text = "完结批次", UseColumnTextForButtonValue = true, SortMode = DataGridViewColumnSortMode.NotSortable },
            });
            if(UserHelper.GetUserRole() == UserRole.DocManager)
            {
                DataGridViewButtonColumn buttonColumn = view.Columns["trp_control"] as DataGridViewButtonColumn;
                buttonColumn.Text = "领取情况";
            }
            if(querySql == null)
            {
                querySql = new StringBuilder("SELECT pc.trp_id, dd_name, trp_name, trp_code, trp_cd_amount FROM transfer_registration_pc pc " +
                    "LEFT JOIN data_dictionary dd ON pc.com_id = dd.dd_id WHERE pc.trp_work_status=1 AND pc.trp_submit_status=2 ");//已提交但待领取
                if(csid != null) querySql.Append($" AND dd.dd_id='{csid}'");
                querySql.Append(" ORDER BY trp_code");
            }
            DataTable dataTable = SqlHelper.ExecuteQuery(querySql.ToString());
            for(int i = 0; i < dataTable.Rows.Count; i++)
            {
                DataRow row = dataTable.Rows[i];
                int x = GetCDAmount(row["trp_id"], false);
                int y = GetCDAmount(row["trp_id"], true);
                if(x > 0 && y == 0)//如果当前批次下有光盘，且光盘已完成，则不显示此批次记录
                    continue;
                else
                {
                    int index = view.Rows.Add();
                    view.Rows[index].Cells["trp_id"].Value = row["trp_id"];
                    view.Rows[index].Cells["dd_name"].Value = row["dd_name"];
                    view.Rows[index].Cells["trp_name"].Value = row["trp_name"];
                    view.Rows[index].Cells["trp_code"].Value = row["trp_code"];
                    view.Rows[index].Cells["trp_finishtime"].Value = null;//完成时间字段待定
                    view.Rows[index].Cells["trp_cd_amount"].Value = row["trp_cd_amount"];
                }
            }
            view.Columns["trp_id"].Visible = false;
            LastIdLog = new string[] { string.Empty, $"PC_{csid}" };

            view.Tag = "PC_LIST";
        }

        /// <summary>
        /// 获取指定批次下光盘数
        /// </summary>
        /// <param name="trpId">指定批次主键</param>
        /// <param name="isNonWork">是否只查询未加工</param>
        private int GetCDAmount(object trpId, bool isNonWork)
        {
            string querySql = $"SELECT COUNT(trc_id) FROM transfer_registraion_cd WHERE trp_id='{trpId}'";
            if(isNonWork)
                querySql += " AND trc_complete_status =1";
            int index = SqlHelper.ExecuteCountQuery(querySql);

            if(isNonWork)//如果光盘已被领取，则判断光盘下是否存在未领取的项目/课题
            {
                querySql = $"SELECT trc_id FROM transfer_registraion_cd WHERE trp_id='{trpId}'";
                object[] ids = SqlHelper.ExecuteSingleColumnQuery(querySql);

                for(int i = 0; i < ids.Length; i++)
                {
                    index += SqlHelper.ExecuteCountQuery($"SELECT COUNT(pi_id) FROM project_info WHERE trc_id='{ids[i]}' AND pi_work_status=1");
                    index += SqlHelper.ExecuteCountQuery($"SELECT COUNT(ti_id) FROM topic_info WHERE trc_id='{ids[i]}' AND ti_work_status=1");
                }
            }
            return index;
        }

        /// <summary>
        /// 获取当前用户的已返工数
        /// </summary>
        /// <returns></returns>
        private object BackWorkAmount => SqlHelper.ExecuteOnlyOneQuery($"SELECT COUNT(*) FROM work_myreg WHERE wm_status=4 AND wm_user='{UserHelper.GetUser().UserKey}'");

        /// <summary>
        /// 二级菜单点击事件（加工登记/加工中/已返工）
        /// </summary>
        private void Sub_Menu_Click(object sender, EventArgs e)
        {
            AccordionControlElement element = sender as AccordionControlElement;
            panel1.Visible = false;
            panel2.Visible = false;
            if ("ac_Login".Equals(element.Name))//加工登记
            {
                cbo_CompanyList.SelectedIndex = 0;
                LoadPCList(null, null);
                panel1.Visible = true;
            }
            else if ("ac_Working".Equals(element.Name))//加工中
                LoadWorkList(null, WorkStatus.WorkSuccess);
            else if ("ac_Worked".Equals(element.Name))//已返工
                LoadWorkBackList();
            else if ("ac_MyWork".Equals(element.Name))//我的加工
            {
                panel2.Visible = true;
                DataGridViewStyleHelper.ResetDataGridView(view, true);
            }
        }

        /// <summary>
        /// 我的加工
        /// </summary>
        private void LoadMyWorkedList(string sData, string fDate)
        {
            DataGridViewStyleHelper.ResetDataGridView(view);
            view.Columns.AddRange(new DataGridViewColumn[]
            {
                new DataGridViewTextBoxColumn(){ Name = "mw_id", Visible = false },
                new DataGridViewTextBoxColumn(){ Name = "mw_unit", HeaderText = "来源单位", FillWeight = 12, SortMode = DataGridViewColumnSortMode.NotSortable  },
                new DataGridViewTextBoxColumn(){ Name = "mw_pcode", HeaderText = "批次号", FillWeight = 8, SortMode = DataGridViewColumnSortMode.NotSortable  },
                new DataGridViewTextBoxColumn(){ Name = "mw_code", HeaderText = "项目/课题编号", FillWeight = 12, SortMode = DataGridViewColumnSortMode.NotSortable  },
                new DataGridViewTextBoxColumn(){ Name = "mw_name", HeaderText = "项目/课题名称", FillWeight = 35, SortMode = DataGridViewColumnSortMode.NotSortable },
                new DataGridViewTextBoxColumn(){ Name = "mw_fileAmount", HeaderText = "文件数", FillWeight = 5, SortMode = DataGridViewColumnSortMode.NotSortable },
                new DataGridViewTextBoxColumn(){ Name = "mw_state", HeaderText = "状态", FillWeight = 8, SortMode = DataGridViewColumnSortMode.NotSortable  },
                new DataGridViewButtonColumn(){ Name = "mw_control", HeaderText = "操作", FillWeight = 6, Text = "查看", UseColumnTextForButtonValue = true, SortMode = DataGridViewColumnSortMode.NotSortable },
            });

            string querySql = "SELECT dd.dd_name, wm.wm_id, A.pi_id, A.pi_code, A.pi_name, A.pi_obj_id, trp.trp_code, wm.wm_status FROM( " +
                "SELECT pi_id, pi_code, pi_name, pi_obj_id, pi_worker_id, pi_worker_date FROM project_info WHERE pi_categor=2 UNION ALL " +
                "SELECT ti_id, ti_code, ti_name, ti_obj_id, ti_worker_id, ti_worker_date FROM topic_info WHERE ti_categor=-3) AS A " +
                "LEFT JOIN work_myreg wm ON A.pi_id = wm.wm_obj_id " +
                "LEFT JOIN work_registration AS wr ON wr.wr_id = wm.wr_id " +
                "LEFT JOIN transfer_registration_pc AS trp ON wr.trp_id = trp.trp_id " +
                "LEFT JOIN data_dictionary AS dd ON dd.dd_id = trp.com_id " +
               $"WHERE wm.wm_type = '{(int)WorkType.ProjectWork}' AND A.pi_worker_id = '{UserHelper.GetUser().UserKey}' AND wm_status<>{(int)QualityStatus.QualityBack} " +
               $"{sData} {fDate} ORDER BY dd_sort, wm_date desc";
            DataTable table = SqlHelper.ExecuteQuery(querySql);
            foreach(DataRow row in table.Rows)
            {
                int i = view.Rows.Add();
                view.Rows[i].Cells["mw_id"].Tag = row["pi_obj_id"];
                view.Rows[i].Cells["mw_id"].Value = row["pi_id"];
                view.Rows[i].Cells["mw_unit"].Value = row["dd_name"];
                view.Rows[i].Cells["mw_pcode"].Value = row["trp_code"];
                view.Rows[i].Cells["mw_code"].Value = row["pi_code"];
                view.Rows[i].Cells["mw_name"].Value = row["pi_name"];
                view.Rows[i].Cells["mw_fileAmount"].Value = GetFileAmountById(row["pi_id"]);
                view.Rows[i].Cells["mw_state"].Value = GetMyWorkState(row["wm_status"]);
            }
            view.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            view.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private string GetMyWorkState(object state)
        {
            if(state != null)
            {
                QualityStatus status = (QualityStatus)(int)state;
                if(status == QualityStatus.NonQuality)
                    return "待质检";
                else if(status == QualityStatus.Qualitting)
                    return "质检中";
                else if(status == QualityStatus.QualityFinish)
                    return "质检通过";
                else if(status == QualityStatus.QualityBack)
                    return "被返工中";
            }
            return string.Empty;
        }

        /// <summary>
        /// 根据计划ID获取其下所有文件总数
        /// </summary>
        private object GetFileAmountById(object fileId) => SqlHelper.ExecuteCountQuery($"SELECT COUNT(pfl_id) FROM processing_file_list WHERE pfl_obj_id='{fileId}'");

        /// <summary>
        /// 已返工列表
        /// </summary>
        private void LoadWorkBackList()
        {
            DataGridViewStyleHelper.ResetDataGridView(view);
            view.Columns.AddRange(new DataGridViewColumn[] {
                new DataGridViewTextBoxColumn(){ Name = "bk_id" },
                new DataGridViewTextBoxColumn(){ Name = "bk_code", HeaderText = "编号", FillWeight = 15, SortMode = DataGridViewColumnSortMode.NotSortable  },
                new DataGridViewTextBoxColumn(){ Name = "bk_name", HeaderText = "名称", FillWeight = 20, SortMode = DataGridViewColumnSortMode.NotSortable  },
                new DataGridViewTextBoxColumn(){ Name = "bk_reason", HeaderText = "返工原因", FillWeight = 15, SortMode = DataGridViewColumnSortMode.NotSortable },
                new DataGridViewButtonColumn(){ Name = "bk_edit", HeaderText = "操作", FillWeight = 7, Text = "编辑", UseColumnTextForButtonValue = true, SortMode = DataGridViewColumnSortMode.NotSortable  },
                new DataGridViewButtonColumn(){ Name = "bk_submit", HeaderText = "提交", FillWeight = 7, Text = "提交质检", UseColumnTextForButtonValue = true, SortMode = DataGridViewColumnSortMode.NotSortable },
            });

            string querySql = $"SELECT * FROM work_myreg WHERE wm_status='{(int)QualityStatus.QualityBack}' ";
            if (UserHelper.GetUserRole() != UserRole.DocManager)
                querySql += $"AND wm_user='{UserHelper.GetUser().UserKey}' ";
            DataTable table = SqlHelper.ExecuteQuery(querySql);
            for(int i = 0; i < table.Rows.Count; i++)
            {
                WorkType type = (WorkType)Convert.ToInt32(table.Rows[i]["wm_type"]);
                object id = table.Rows[i]["wm_obj_id"];
                if(type == WorkType.PaperWork_Imp || type == WorkType.CDWork_Imp)
                {
                    DataRow _row = SqlHelper.ExecuteSingleRowQuery($"SELECT imp_id, imp_code, imp_name, imp_intro, imp_obj_id FROM imp_info WHERE imp_id='{id}'");
                    int index = view.Rows.Add();
                    view.Rows[index].Tag = type;
                    view.Rows[index].Cells["bk_id"].Tag = _row["imp_obj_id"];
                    view.Rows[index].Cells["bk_id"].Value = _row["imp_id"];
                    view.Rows[index].Cells["bk_code"].Tag = table.Rows[i]["wm_id"];
                    view.Rows[index].Cells["bk_code"].Value = _row["imp_code"];
                    view.Rows[index].Cells["bk_name"].Value = _row["imp_name"];
                    view.Rows[index].Cells["bk_reason"].Value = GetAdvicesById(_row["imp_id"]);
                }
                else if(type == WorkType.PaperWork_Plan)
                {
                    DataTable _table = SqlHelper.ExecuteQuery($"SELECT pi_id, pi_code, pi_name, pi_obj_id FROM project_info WHERE pi_id='{id}'");
                    if(_table.Rows.Count > 0)
                    {
                        DataRow _row = _table.Rows[0];
                        int index = view.Rows.Add();
                        view.Rows[index].Tag = type;
                        view.Rows[index].Cells["bk_id"].Tag = _row["pi_obj_id"];
                        view.Rows[index].Cells["bk_id"].Value = _row["pi_id"];
                        view.Rows[index].Cells["bk_code"].Tag = table.Rows[i]["wm_id"];//供删除使用
                        view.Rows[index].Cells["bk_code"].Value = _row["pi_code"];
                        view.Rows[index].Cells["bk_name"].Value = _row["pi_name"];
                        view.Rows[index].Cells["bk_reason"].Value = GetAdvicesById(_row["pi_id"]);
                    }
                }
                else if(type == WorkType.PaperWork_Special)
                {
                    DataRow _row = SqlHelper.ExecuteSingleRowQuery($"SELECT imp_id, imp_code, imp_name, imp_intro, imp_obj_id FROM imp_dev_info WHERE imp_id='{id}'");
                    int index = view.Rows.Add();
                    view.Rows[index].Tag = type;
                    view.Rows[index].Cells["bk_id"].Tag = _row["imp_obj_id"];
                    view.Rows[index].Cells["bk_id"].Value = _row["imp_id"];
                    view.Rows[index].Cells["bk_code"].Tag = table.Rows[i]["wm_id"];//供删除使用
                    view.Rows[index].Cells["bk_code"].Value = _row["imp_code"];
                    view.Rows[index].Cells["bk_name"].Value = _row["imp_name"];
                    view.Rows[index].Cells["bk_reason"].Value = GetAdvicesById(_row["imp_id"]);
                }
                else if(type == WorkType.ProjectWork)
                {
                    DataTable _table = SqlHelper.ExecuteQuery($"SELECT pi_id, pi_code, pi_name, pi_obj_id FROM project_info WHERE pi_id='{id}' UNION ALL " +
                        $"SELECT ti_id, ti_code, ti_name, ti_obj_id FROM topic_info WHERE ti_id='{id}'");
                    foreach(DataRow _row in _table.Rows)
                    {
                        int index = view.Rows.Add();
                        view.Rows[index].Tag = type;
                        view.Rows[index].Cells["bk_id"].Tag = _row["pi_obj_id"];
                        view.Rows[index].Cells["bk_id"].Value = _row["pi_id"];
                        view.Rows[index].Cells["bk_code"].Tag = table.Rows[i]["wm_id"];//供删除使用
                        view.Rows[index].Cells["bk_code"].Value = _row["pi_code"];
                        view.Rows[index].Cells["bk_name"].Value = _row["pi_name"];
                        view.Rows[index].Cells["bk_reason"].Value = GetAdvicesById(_row["pi_id"]);
                    }
                }
            }

            view.Columns["bk_id"].Visible = false;
        }
       
        /// <summary>
        /// 获取质检意见
        /// </summary>
        private string GetAdvicesById(object objid)
        {
            StringBuilder sb = new StringBuilder();
            List<object[]> _obj = SqlHelper.ExecuteColumnsQuery($"SELECT distinct(qa_type) FROM quality_advices WHERE qa_obj_id='{objid}'", 1);
            for(int i = 0; i < _obj.Count; i++)
            {
                int index = Convert.ToInt32(_obj[i][0]);
                if(index == 0)
                    sb.Append("[基本信息]、");
                else if(index == 1)
                    sb.Append("[文件列表]、");
                else if(index == 2)
                    sb.Append("[文件核查]、");
                else if(index == 3)
                    sb.Append("[案盒信息]、");
            }
            return sb.Length > 0 ? sb.ToString().Substring(0, sb.Length - 1) : null;
        }
    
        /// <summary>
        /// 根据加工登记主键获取对应项目/课题信息
        /// </summary>
        private static object[] GetObjectListById(object objid)
        {
            string querySql = $" SELECT wr_id, wr_type,wr_obj_id FROM work_registration wr LEFT JOIN(" +
                            $"SELECT trp_id, dd_id FROM transfer_registration_pc LEFT JOIN data_dictionary ON com_id = dd_id) tb " +
                            $"ON wr.trp_id = tb.trp_id WHERE wr_id='{objid}'";
            object[] list = SqlHelper.ExecuteRowsQuery(querySql);
            List<DataTable> resultList = new List<DataTable>();
            WorkType type = (WorkType)list[1];
            object id = list[2];
            string _querySql = null;
            switch(type)
            {
                case WorkType.PaperWork:
                    _querySql = $"SELECT '{(int)type}','{list[0]}','{id}',trp_code,trp_name,dd_name FROM transfer_registration_pc LEFT JOIN " +
                        $"data_dictionary ON com_id = dd_id WHERE trp_id='{id}'";
                    break;
                case WorkType.CDWork:
                    _querySql = $"SELECT '{(int)type}','{list[0]}','{id}',trc_code,trc_name,dd_name FROM transfer_registraion_cd trc LEFT JOIN(" +
                        $"SELECT trp_id, dd_name FROM transfer_registration_pc LEFT JOIN data_dictionary ON com_id = dd_id ) tb1 " +
                        $"ON tb1.trp_id = trc.trp_id WHERE trc_id='{id}'";
                    break;
                case WorkType.ProjectWork:
                    _querySql = $"SELECT '{(int)type}','{list[0]}','{id}',pi_code,pi_name,dd_name FROM project_info pi " +
                        $"LEFT JOIN(SELECT trc_id, dd_name FROM transfer_registraion_cd trc " +
                        $"LEFT JOIN(SELECT trp_id, dd_name FROM transfer_registration_pc trp " +
                        $"LEFT JOIN data_dictionary ON dd_id = trp.com_id)tb1 ON trc.trp_id = tb1.trp_id) tb2 ON tb2.trc_id = pi.trc_id " +
                        $"WHERE pi_id='{id}'";
                    break;
                case WorkType.TopicWork:
                    _querySql = $"SELECT '{(int)type}','{list[0]}','{id}',si_code,si_name,dd_name FROM subject_info si LEFT JOIN(" +
                       $"SELECT pi_id,dd_name FROM project_info pi " +
                       $"LEFT JOIN(SELECT trc_id, dd_name FROM transfer_registraion_cd trc " +
                       $"LEFT JOIN(SELECT trp_id, dd_name FROM transfer_registration_pc trp " +
                       $"LEFT JOIN data_dictionary ON dd_id = trp.com_id)tb1 ON trc.trp_id = tb1.trp_id) tb2 ON tb2.trc_id = pi.trc_id " +
                       $") tb3 ON tb3.pi_id = si.pi_id WHERE si.si_id='{id}'";
                    break;
            }
            return SqlHelper.ExecuteRowsQuery(_querySql);
        }
        
        /// <summary>
        /// 加工中列表
        /// </summary>
        /// <param name="unitId">来源单位（默认全部）</param>
        /// <param name="workStatus">加工状态</param>
        private void LoadWorkList(object unitId, WorkStatus workStatus)
        {
            DataGridViewStyleHelper.ResetDataGridView(view);
            string querySql = $" SELECT wr_id, wr_type, wr.wr_obj_id, wr_source_id FROM work_registration wr LEFT JOIN transfer_registration_pc trp ON wr.trp_id = trp.trp_id LEFT JOIN data_dictionary dd ON trp.com_id = dd.dd_id " +
                $"WHERE wr_status = {(int)workStatus} AND wr_submit_status={(int)ObjectSubmitStatus.NonSubmit} ";
            if (UserHelper.GetUserRole() != UserRole.DocManager)
                querySql += $"AND wr_source_id='{UserHelper.GetUser().UserKey}' ";
            if (unitId != null)
                querySql += $"AND dd.dd_id='{unitId}' ";
            List<object[]> list = SqlHelper.ExecuteColumnsQuery(querySql, 4);
            List<object[]> resultList = new List<object[]>();
            for(int i = 0; i < list.Count; i++)
            {
                WorkType type = (WorkType)list[i][1];
                string userName = UserHelper.GetUserNameById(list[i][3]);
                object id = list[i][2];
                string _querySql = null;
                switch(type)
                {
                    case WorkType.PaperWork:
                        _querySql = $"SELECT '{list[i][0]}','{id}', trp_code, trp_name, '{(int)type}', '{userName}' FROM transfer_registration_pc WHERE trp_id='{id}'";
                        break;
                    case WorkType.CDWork:
                        _querySql = $"SELECT '{list[i][0]}','{id}', trc_code, trc_name, '{(int)type}', '{userName}' FROM transfer_registraion_cd WHERE trc_id='{id}'";
                        break;
                    case WorkType.ProjectWork:
                        _querySql = $"SELECT '{list[i][0]}','{id}', pi_code, pi_name, '{(int)type}', '{userName}' FROM project_info WHERE pi_id='{id}' " +
                            $"UNION ALL SELECT '{list[i][0]}','{id}', ti_code, ti_name, '{(int)type}', '{userName}' FROM topic_info WHERE ti_id='{id}';";
                        break;
                    case WorkType.TopicWork:
                        _querySql = $"SELECT '{list[i][0]}','{id}', ti_code, ti_name, '{(int)type}', '{userName}' FROM topic_info WHERE ti_id = '{id}' " +
                         $"UNION ALL SELECT '{list[i][0]}','{id}', si_code, si_name, '{(int)type}', '{userName}' FROM subject_info WHERE si_id='{id}'";
                        break;
                    default:
                        _querySql = string.Empty;
                        break;
                }
                object[] _obj = SqlHelper.ExecuteRowsQuery(_querySql);
                if(_obj != null)
                    resultList.Add(_obj);
            }
            view.Columns.AddRange(new DataGridViewColumn[]
            {
                new DataGridViewTextBoxColumn(){Name = "id"},
                new DataGridViewTextBoxColumn(){Name = "code", HeaderText = "编号", FillWeight = 15, SortMode = DataGridViewColumnSortMode.NotSortable },
                new DataGridViewTextBoxColumn(){Name = "name", HeaderText = "名称", FillWeight = 20, SortMode = DataGridViewColumnSortMode.NotSortable },
                new DataGridViewTextBoxColumn(){Name = "dd_name", HeaderText = "来源单位", FillWeight = 20, SortMode = DataGridViewColumnSortMode.NotSortable },
                new DataGridViewTextBoxColumn(){Name = "type", HeaderText = "加工类型", FillWeight = 15, SortMode = DataGridViewColumnSortMode.NotSortable },
                new DataGridViewButtonColumn(){Name = "edit", HeaderText = "操作", FillWeight = 7, Text = "编辑", UseColumnTextForButtonValue = true, SortMode = DataGridViewColumnSortMode.NotSortable },
                new DataGridViewButtonColumn(){Name = "submit", HeaderText = "提交", FillWeight = 7, Text = "提交质检", UseColumnTextForButtonValue = true, SortMode = DataGridViewColumnSortMode.NotSortable },
            });
            for(int i = 0; i < resultList.Count; i++)
            {
                int index = view.Rows.Add();
                view.Rows[index].Cells["id"].Tag = resultList[i][0];
                view.Rows[index].Cells["id"].Value = resultList[i][1];
                view.Rows[index].Cells["code"].Value = resultList[i][2];
                view.Rows[index].Cells["name"].Value = resultList[i][3];
                object[] _obj = SqlHelper.GetCompanyByParam(resultList[i][1], resultList[i][4]);
                view.Rows[index].Cells["dd_name"].Tag = GetValue(_obj[0]);
                view.Rows[index].Cells["dd_name"].Value = GetValue(_obj[1]);
                view.Rows[index].Cells["type"].Value = GetTypeValue(resultList[i][4]);
                view.Rows[index].Cells["type"].Tag = (WorkType)Convert.ToInt32(resultList[i][4]);
                if (UserHelper.GetUserRole() == UserRole.DocManager)
                {
                    DataGridViewButtonCell buttonColumn = view.Rows[index].Cells["edit"] as DataGridViewButtonCell;
                    buttonColumn.UseColumnTextForButtonValue = false;
                    buttonColumn.Value = $"[{resultList[i][5]}]";
                }
            }
            view.Columns["id"].Visible = false;
            DataGridViewStyleHelper.SetAlignWithCenter(view, new string[] { "type" });

            view.Tag = "WORK_LIST";
        }
       
        /// <summary>
        /// 根据类型获取对应文本描述
        /// </summary>
        private object GetTypeValue(object index)
        {
            WorkType type = (WorkType)Convert.ToInt32(index);
            if(type == WorkType.PaperWork)
                return "纸本加工";
            else if(type == WorkType.CDWork)
                return "光盘加工";
            else if(type == WorkType.ProjectWork)
                return "项目/课题加工";
            else if(type == WorkType.TopicWork)
                return "课题/子课题加工";
            else
                return null;
        }

        /// <summary>
        /// 单元格点击事件
        /// </summary>
        private void Dgv_WorkLog_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex != -1 && e.ColumnIndex != -1 && view.Rows[e.RowIndex].Cells[0].Value != null)
            {
                object columnName = view.Columns[e.ColumnIndex].Name;
                //光盘数
                if ("trp_cd_amount".Equals(columnName))
                {
                    if (!view.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.Equals(0))
                    {
                        object trpid = view.Rows[e.RowIndex].Cells["trp_id"].Value;
                        LoadCDList(trpid);
                    }
                }
                //批次完结（仅批次管理员可用）
                else if ("trp_finish".Equals(columnName))
                {
                    if (UserHelper.GetUserRole() == UserRole.W_Q_Manager
                        || UserHelper.GetUserRole() == UserRole.DocManager)
                    {
                        if (XtraMessageBox.Show("是否确认完结当前批次。", "确认提示", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk) == DialogResult.Yes)
                        {
                            object trpid = view.Rows[e.RowIndex].Cells["trp_id"].Value;
                            string updateSql = $"UPDATE transfer_registration_pc SET trp_work_status={(int)WorkStatus.WorkSuccess}, trp_complete_user='{UserHelper.GetUser().UserKey}' WHERE trp_id='{trpid}'";
                            SqlHelper.ExecuteNonQuery(updateSql);
                            view.Rows.RemoveAt(e.RowIndex);
                        }
                    }
                    else
                        XtraMessageBox.Show("仅允许管理员(线上)完结批次。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
                //光盘页 - 总数
                else if ("trc_total_amount".Equals(columnName))
                {
                    if (0 != Convert.ToInt32(view.Rows[e.RowIndex].Cells[e.ColumnIndex].Value))
                    {
                        object trcid = view.Rows[e.RowIndex].Cells["trc_id"].Value;
                        LoadProjectList(trcid);
                    }
                }
                //项目/课题 - 总数
                else if ("pi_total_amount".Equals(columnName))
                {
                    if (0 != Convert.ToInt32(view.Rows[e.RowIndex].Cells[e.ColumnIndex].Value))
                    {
                        object pid = view.Rows[e.RowIndex].Cells["pi_id"].Value;
                        LoadTopicList(pid);
                    }
                }
                //光盘 - 加工
                else if ("trc_control".Equals(columnName))
                {
                    if (CheckCanReceive())
                    {
                        int totalAmount = Convert.ToInt32(view.Rows[e.RowIndex].Cells["trc_total_amount"].Value);
                        if (totalAmount == 0)
                        {
                            object trcid = view.Rows[e.RowIndex].Cells["trc_id"].Value;
                            if (XtraMessageBox.Show("是否确认开始加工当前光盘?", "确认提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                            {
                                object trpId = SqlHelper.ExecuteOnlyOneQuery($"SELECT trp_id FROM transfer_registraion_cd WHERE trc_id='{trcid}'");

                                string insertSql = $"INSERT INTO work_registration ([wr_id], [wr_status], [trp_id], [wr_type], [wr_date], [wr_obj_id], [wr_submit_status], [wr_receive_status], [wr_source_id], [wr_qtcount]) " +
                                    $"VALUES('{Guid.NewGuid().ToString()}', 2, '{trpId}' , {(int)WorkType.CDWork}, '{DateTime.Now}', '{trcid}', 1, 1, '{UserHelper.GetUser().UserKey}', 0);";
                                insertSql += $"UPDATE transfer_registraion_cd SET trc_complete_status={(int)WorkStatus.WorkSuccess}, trc_complete_user='{UserHelper.GetUser().UserKey}' WHERE trc_id='{trcid}';";

                                SqlHelper.ExecuteNonQuery(insertSql);

                                view.Rows.RemoveAt(e.RowIndex);
                            }
                        }
                        else
                            XtraMessageBox.Show("不能直接领取结构化光盘。", "操作失败", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                    else
                        XtraMessageBox.Show("请先完成当前已领取且尚未完成的加工项。", "领取失败", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
                //批次 - 加工
                else if ("trp_control".Equals(columnName))
                {
                    object trpID = view.Rows[e.RowIndex].Cells["trp_id"].Value;
                    //管理员直接查看领取详情
                    if (UserHelper.GetUserRole() == UserRole.DocManager)
                    {
                        BatchRecInfo batchRecInfo = new BatchRecInfo(trpID);
                        batchRecInfo.ShowDialog();
                    }
                    else
                    {
                        if (CheckCanReceive())
                        {
                            //int totalAmount = Convert.ToInt32(view.Rows[e.RowIndex].Cells["trp_cd_amount"].Value);
                            //if(totalAmount == 0)
                            //{
                            
                            if (XtraMessageBox.Show("确认领取此批次吗？", "确认提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                            {
                                object primaryKey = Guid.NewGuid().ToString();
                                string insertSql = $"INSERT INTO work_registration VALUES('{primaryKey}', {(int)WorkStatus.WorkSuccess}, '{trpID}', {(int)WorkType.PaperWork}," +
                                    $"'{DateTime.Now}', null, '{trpID}', {(int)ObjectSubmitStatus.NonSubmit}, {(int)ReceiveStatus.NonReceive}, '{UserHelper.GetUser().UserKey}', 0)";
                                SqlHelper.ExecuteNonQuery(insertSql);
                                XtraMessageBox.Show("领取成功。");
                            }
                            //}
                            //else
                            //    XtraMessageBox.Show("不允许直接领取此批次。", "领取失败", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        }
                        else
                            XtraMessageBox.Show("请先完成当前已领取且尚未完成的加工项。", "领取失败");
                    }
                }
                //项目/课题 - 加工
                else if ("pi_control".Equals(columnName))
                {
                    if (CheckCanReceive())
                    {
                        int totalAmount = GetInt32(view.Rows[e.RowIndex].Cells["pi_total_amount"].Value);
                        if (XtraMessageBox.Show($"是否确认加工当前选中项目/课题{(totalAmount <= 10 ? "（包括所有课题/子课题）" : string.Empty)}？", "确认提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                        {
                            /* 领取当前任务和直属上级任务 */
                            object objId = view.Rows[e.RowIndex].Cells["pi_id"].Value;
                            object trcid = SqlHelper.ExecuteOnlyOneQuery($"SELECT trc_id FROM project_info WHERE pi_id='{objId}' UNION ALL SELECT trc_id FROM topic_info WHERE ti_id='{objId}';");
                            object trpid = SqlHelper.ExecuteOnlyOneQuery($"SELECT trp_id FROM transfer_registraion_cd WHERE trc_id='{trcid}'");

                            StringBuilder sb = new StringBuilder();
                            //第一个领取人同时领取当前项目/课题直属计划【包括光盘|批次】
                            sb.Append($"UPDATE transfer_registration_pc SET trp_complete_status=2, trp_complete_user='{UserHelper.GetUser().UserKey}' WHERE trp_id='{trpid}';");
                            sb.Append($"UPDATE transfer_registraion_cd SET trc_complete_status=2, trc_complete_user='{UserHelper.GetUser().UserKey}' WHERE trc_id='{trcid}';");

                            //领取当前选定任务
                            sb.Append($"INSERT INTO work_registration(wr_id, wr_status, trp_id, wr_type, wr_date, wr_obj_id, wr_submit_status, wr_receive_status, wr_source_id, wr_qtcount) " +
                                $"VALUES ('{Guid.NewGuid().ToString()}', 2, '{trpid}', {(int)WorkType.ProjectWork}, '{DateTime.Now}', '{objId}', 1, 1, '{UserHelper.GetUser().UserKey}', 0);");
                            sb.Append($"UPDATE project_info SET pi_work_status={(int)WorkStatus.WorkSuccess}, pi_worker_id='{UserHelper.GetUser().UserKey}' WHERE pi_id='{objId}';");
                            sb.Append($"UPDATE topic_info SET ti_work_status={(int)WorkStatus.WorkSuccess}, ti_worker_id='{UserHelper.GetUser().UserKey}' WHERE ti_id='{objId}';");

                            SqlHelper.ExecuteNonQuery(sb.ToString());
                            view.Rows.RemoveAt(e.RowIndex);
                            XtraMessageBox.Show("领取成功。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

                            /* 如果当前项目下的子课题数<=10，则同时领取其下全部任务 */
                            //if(totalAmount <= 10)
                            /* --默认全部领取-- */
                            if (true)
                            {
                                StringBuilder sbStr = new StringBuilder();
                                //领取子级【课题/子课题】
                                object[] topicColumns = SqlHelper.ExecuteSingleColumnQuery($"SELECT ti_id FROM topic_info WHERE ti_obj_id='{objId}' AND ti_work_status=1 " +
                                    $"UNION ALL SELECT si_id FROM subject_info WHERE si_obj_id='{objId}' AND si_work_status=1");
                                for (int j = 0; j < topicColumns.Length; j++)
                                {
                                    sbStr.Append($"UPDATE topic_info SET ti_work_status=2, ti_worker_id='{UserHelper.GetUser().UserKey}' WHERE ti_id='{topicColumns[j]}';" +
                                        $"UPDATE subject_info SET si_work_status=2, si_worker_id='{UserHelper.GetUser().UserKey}' WHERE si_id='{topicColumns[j]}';");
                                    //【子课题】
                                    object[] _subjectColumns = SqlHelper.ExecuteSingleColumnQuery($"SELECT si_id FROM subject_info WHERE si_obj_id='{topicColumns[j]}' AND si_work_status=1;");
                                    for (int k = 0; k < _subjectColumns.Length; k++)
                                        sbStr.Append($"UPDATE subject_info SET si_work_status=2, si_worker_id='{UserHelper.GetUser().UserKey}' WHERE si_id='{_subjectColumns[k]}';");
                                }
                                SqlHelper.ExecuteNonQuery(sbStr.ToString());
                            }
                        }
                    }
                    else
                        XtraMessageBox.Show("请先完成当前已领取且尚未完成的加工项。", "领取失败");
                }
                //课题/子课题 - 加工
                else if ("si_control".Equals(columnName))
                {
                    if (CheckCanReceive())
                    {
                        if (XtraMessageBox.Show("是否确认加工当前选中课题/子课题？", "确认提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                        {
                            object siid = view.Rows[e.RowIndex].Cells["si_id"].Value;
                            object trpid = SqlHelper.ExecuteOnlyOneQuery($"SELECT trp.trp_id FROM transfer_registration_pc trp, transfer_registraion_cd trc, project_info pi, subject_info si WHERE trp.trp_id = trc.trp_id AND pi.trc_id = trc.trc_id AND si.pi_id = pi.pi_id AND si.si_id = '{siid}'");

                            object primaryKey = Guid.NewGuid().ToString();
                            string insertSql = $"INSERT INTO work_registration VALUES('{primaryKey}',{(int)WorkStatus.WorkSuccess},'{trpid}',{(int)WorkType.TopicWork},'{DateTime.Now}'" +
                                $",null,'{siid}',{(int)ObjectSubmitStatus.NonSubmit},{(int)ReceiveStatus.NonReceive},'{UserHelper.GetUser().UserKey}',0)";
                            SqlHelper.ExecuteNonQuery(insertSql);

                            string updateSql = $"UPDATE subject_info SET si_work_status={(int)WorkStatus.WorkSuccess},si_worker_id='{UserHelper.GetUser().UserKey}' WHERE si_id='{siid}'";
                            SqlHelper.ExecuteNonQuery(updateSql);
                            //【子课题】
                            List<object[]> _obj3 = SqlHelper.ExecuteColumnsQuery($"SELECT si_id FROM subject_info WHERE pi_id='{siid}' AND si_work_status={(int)WorkStatus.NonWork}", 1);
                            for (int k = 0; k < _obj3.Count; k++)
                                SqlHelper.ExecuteOnlyOneQuery($"UPDATE subject_info SET si_work_status={(int)WorkStatus.WorkSuccess}, si_worker_id='{UserHelper.GetUser().UserKey}' WHERE si_id='{_obj3[k][0]}'");

                        }
                    }
                    else
                        XtraMessageBox.Show("请先完成当前已领取且尚未完成的加工项。", "领取失败");
                }
                //编辑 - 开始加工
                else if ("edit".Equals(columnName))
                {
                    object cellValue = view.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                    if (!"编辑".Equals(cellValue)) return;
                    object objId = view.Rows[e.RowIndex].Cells["id"].Value;
                    string typeValue = GetValue(view.Rows[e.RowIndex].Cells["type"].Value);
                    if (typeValue.Contains("光盘"))
                    {
                        object planId = SqlHelper.ExecuteOnlyOneQuery($"SELECT pi_id FROM project_info WHERE trc_id='{objId}' AND pi_categor=1");
                        if (planId != null)//普通计划
                        {
                            Frm_MyWork frm = new Frm_MyWork(WorkType.CDWork_Plan, planId, objId, ControlType.Plan, false);
                            frm.unitCode = view.Rows[e.RowIndex].Cells["dd_name"].Tag;
                            frm.Show();
                        }
                        else
                        {
                            planId = SqlHelper.ExecuteOnlyOneQuery($"SELECT pi_id FROM project_info WHERE pi_obj_id='{objId}'");
                            if (planId == null)
                            {
                                planId = SqlHelper.ExecuteOnlyOneQuery($"SELECT imp_id FROM imp_info WHERE imp_obj_id='{objId}'");
                                //重大专项|重点研发
                                if (planId != null)
                                {
                                    Frm_MyWork frm = new Frm_MyWork(WorkType.CDWork_Imp, planId, objId, ControlType.Imp, false);
                                    //frm.planCode = GetValue(SqlHelper.ExecuteOnlyOneQuery($"SELECT pi_code FROM project_info WHERE pi_id='{planId}'"));
                                    frm.unitCode = view.Rows[e.RowIndex].Cells["dd_name"].Tag;
                                    frm.trcId = objId;
                                    frm.Show();
                                }
                                else
                                {
                                    Frm_ProTypeSelect frm = GetFormHelper.GetProTypeSelecter(WorkType.CDWork, objId);
                                    frm.unitCode = view.Rows[e.RowIndex].Cells["dd_name"].Tag;
                                    frm.trcId = objId;
                                    frm.Show();
                                    frm.Activate();
                                }
                            }
                            else
                            {
                                Frm_MyWork frm = new Frm_MyWork(WorkType.CDWork, planId, objId, ControlType.Plan, false);
                                frm.planCode = GetValue(SqlHelper.ExecuteOnlyOneQuery($"SELECT pi_code FROM project_info WHERE pi_id='{planId}'"));
                                frm.unitCode = view.Rows[e.RowIndex].Cells["dd_name"].Tag;
                                frm.Show();
                            }
                        }
                    }
                    else if (typeValue.Contains("纸本"))
                    {
                        //普通计划
                        object planId = SqlHelper.ExecuteOnlyOneQuery($"SELECT pi_id FROM project_info WHERE trc_id='{objId}'");
                        if (planId != null)
                        {
                            object[] trcIds = SqlHelper.ExecuteSingleColumnQuery($"SELECT trc_id FROM transfer_registraion_cd WHERE trp_id='{objId}'");
                            Frm_MyWork frm = new Frm_MyWork(WorkType.PaperWork_Plan, planId, objId, ControlType.Plan, false);
                            frm.unitCode = view.Rows[e.RowIndex].Cells["dd_name"].Tag;
                            frm.trcId = GetFullStringBySplit(trcIds, ";", string.Empty);
                            frm.Show();
                        }
                        //重点研发计划
                        else
                        {
                            planId = SqlHelper.ExecuteOnlyOneQuery($"SELECT imp_id FROM imp_info WHERE imp_obj_id='{objId}'");
                            if (planId != null)
                            {
                                Frm_MyWork frm = new Frm_MyWork(WorkType.PaperWork_Imp, planId, objId, ControlType.Imp, false);
                                frm.Tag = SqlHelper.ExecuteOnlyOneQuery($"SELECT imp_code FROM imp_dev_info WHERE imp_obj_id='{planId}'");
                                frm.unitCode = view.Rows[e.RowIndex].Cells["dd_name"].Tag;
                                frm.Show();
                            }
                            else
                            {
                                planId = SqlHelper.ExecuteOnlyOneQuery($"SELECT pi_id FROM project_info WHERE pi_obj_id='{objId}'");
                                if (planId == null)
                                {
                                    Frm_ProTypeSelect frm = GetFormHelper.GetProTypeSelecter(WorkType.PaperWork, objId);
                                    frm.unitCode = view.Rows[e.RowIndex].Cells["dd_name"].Tag;
                                    frm.Show();
                                    frm.Activate();
                                }
                                else
                                {
                                    Frm_MyWork frm = new Frm_MyWork(WorkType.PaperWork, planId, objId, ControlType.Plan, false);
                                    frm.planCode = GetValue(SqlHelper.ExecuteOnlyOneQuery($"SELECT pi_code FROM project_info WHERE pi_id='{planId}'"));
                                    frm.unitCode = view.Rows[e.RowIndex].Cells["dd_name"].Tag;
                                    frm.Show();
                                }
                            }
                        }
                    }
                    else
                    {
                        object trpId = view.Rows[e.RowIndex].Cells["id"].Tag;
                        //根据当前id获取根节点ID
                        if (typeValue.Contains("项目/课题"))
                        {
                            Frm_MyWork frm = new Frm_MyWork(WorkType.ProjectWork, trpId, objId, ControlType.Default, false);
                            frm.SetUnitSourceId(view.Rows[e.RowIndex].Cells["dd_name"].Tag);
                            frm.Show();
                        }
                    }
                }
                //提交质检
                else if ("submit".Equals(columnName))
                {
                    object objId = view.Rows[e.RowIndex].Cells["id"].Tag;
                    WorkType workType = (WorkType)view.Rows[e.RowIndex].Cells["type"].Tag;
                    if (workType == WorkType.TopicWork)
                        XtraMessageBox.Show("此操作不被允许！", "提交失败", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    else if (workType == WorkType.PaperWork)
                    {
                        WorkType _type = WorkType.PaperWork;
                        //重点研发
                        object completeUser = SqlHelper.ExecuteOnlyOneQuery("SELECT imp_source_id FROM imp_info WHERE imp_obj_id=" +
                            $"(SELECT trp_id FROM work_registration WHERE wr_id='{objId}')");
                        //普通
                        if (string.IsNullOrEmpty(GetValue(completeUser)))
                        {
                            completeUser = SqlHelper.ExecuteOnlyOneQuery($"SELECT trp_complete_user FROM transfer_registration_pc WHERE trp_id=(SELECT trp_id FROM work_registration WHERE wr_id='{objId}')");
                            _type = WorkType.PaperWork_Plan;
                        }
                        else
                            _type = WorkType.PaperWork_Imp;
                        if (CanSubmitToQT(objId, _type))
                        {
                            if (XtraMessageBox.Show("确定要将当前批次提交到质检吗？", "提交确认", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                            {
                                StringBuilder sb = new StringBuilder();
                                object trpId = view.Rows[e.RowIndex].Cells["id"].Value;
                                object wrId = view.Rows[e.RowIndex].Cells["id"].Tag;
                                if (_type == WorkType.PaperWork_Plan)
                                {
                                    DataRow planRow = SqlHelper.ExecuteSingleRowQuery($"SELECT pi_id, pi_worker_id FROM project_info WHERE trc_id='{trpId}';");
                                    //计划
                                    if (planRow != null)
                                    {
                                        if (UserHelper.GetUser().UserKey.Equals(planRow["pi_worker_id"]))
                                        {
                                            int count = SqlHelper.ExecuteCountQuery($"SELECT COUNT(wm_id) FROM work_myreg WHERE wm_obj_id='{planRow["pi_id"]}'");
                                            if (count == 0)
                                                sb.Append($"INSERT INTO work_myreg(wm_id, wr_id, wm_status, wm_user, wm_date, wm_type, wm_obj_id, wm_ticker) VALUES " +
                                                    $"('{Guid.NewGuid().ToString()}', '{wrId}', 1, '{UserHelper.GetUser().UserKey}', '{DateTime.Now}', '{(int)_type}', '{planRow["pi_id"]}', 0);");
                                        }
                                        //项目|课题
                                        object[] list = SqlHelper.ExecuteSingleColumnQuery("SELECT pi_id FROM (" +
                                            "SELECT pi_id, pi_worker_id, pi_obj_id FROM project_info WHERE pi_categor=2 UNION ALL " +
                                            "SELECT ti_id, ti_worker_id, ti_obj_id FROM topic_info WHERE ti_categor=-3) tb1 " +
                                           $"WHERE tb1.pi_obj_id='{planRow["pi_id"]}' AND tb1.pi_id NOT IN (SELECT wm_obj_id FROM work_myreg) AND pi_worker_id='{UserHelper.GetUser().UserKey}';");
                                        for (int i = 0; i < list.Length; i++)
                                            sb.Append($"INSERT INTO work_myreg(wm_id, wr_id, wm_status, wm_user, wm_date, wm_type, wm_obj_id, wm_ticker) VALUES " +
                                                $"('{Guid.NewGuid().ToString()}', '{wrId}', 1, '{UserHelper.GetUser().UserKey}', '{DateTime.Now}', '{(int)WorkType.ProjectWork}', '{list[i]}', 0);");
                                    }

                                    sb.Append($"UPDATE work_registration SET wr_submit_status =2, wr_submit_date='{DateTime.Now}' WHERE wr_id='{wrId}';");
                                }
                                else if (_type == WorkType.PaperWork_Imp)
                                {
                                    DataRow impRow = SqlHelper.ExecuteSingleRowQuery($"SELECT imp_id, imp_source_id FROM imp_info WHERE imp_obj_id='{trpId}'");
                                    if (impRow["imp_source_id"].Equals(UserHelper.GetUser().UserKey))
                                    {
                                        int count = SqlHelper.ExecuteCountQuery($"SELECT COUNT(wm_id) FROM work_myreg WHERE wm_obj_id='{impRow["imp_id"]}'");
                                        if (count == 0)
                                            //重大专项|重点研发
                                            sb.Append($"INSERT INTO work_myreg(wm_id, wr_id, wm_status, wm_user, wm_date, wm_type, wm_obj_id, wm_ticker) VALUES " +
                                               $"('{Guid.NewGuid().ToString()}', '{wrId}', 1, '{UserHelper.GetUser().UserKey}', '{DateTime.Now}', '{(int)_type}', '{impRow["imp_id"]}', 0);");
                                    }
                                    DataRow speRow = SqlHelper.ExecuteSingleRowQuery($"SELECT imp_id, imp_source_id FROM imp_dev_info WHERE imp_obj_id='{impRow["imp_id"]}'");
                                    if (speRow != null)
                                    {
                                        if (speRow["imp_source_id"].Equals(UserHelper.GetUser().UserKey))
                                        {
                                            int count = SqlHelper.ExecuteCountQuery($"SELECT COUNT(wm_id) FROM work_myreg WHERE wm_obj_id='{speRow["imp_id"]}'");
                                            if (count == 0)
                                                //专项信息
                                                sb.Append($"INSERT INTO work_myreg(wm_id, wr_id, wm_status, wm_user, wm_date, wm_type, wm_obj_id, wm_ticker) VALUES " +
                                                $"('{Guid.NewGuid().ToString()}', '{wrId}', 1, '{UserHelper.GetUser().UserKey}', '{DateTime.Now}', '{(int)WorkType.PaperWork_Special}', '{speRow["imp_id"]}', 0);");
                                        }
                                        //项目|课题
                                        object[] list = SqlHelper.ExecuteSingleColumnQuery($"SELECT pi_id FROM (SELECT pi_id, pi_worker_id FROM project_info WHERE pi_obj_id='{speRow["imp_id"]}' UNION ALL " +
                                            $"SELECT ti_id, ti_worker_id FROM topic_info WHERE ti_obj_id='{speRow["imp_id"]}' AND ti_worker_id='{UserHelper.GetUser().UserKey}') tb1 " +
                                            $"WHERE tb1.pi_id NOT IN (SELECT wm_obj_id FROM work_myreg) AND pi_worker_id='{UserHelper.GetUser().UserKey}';");
                                        for (int i = 0; i < list.Length; i++)
                                        {
                                            sb.Append($"INSERT INTO work_myreg(wm_id, wr_id, wm_status, wm_user, wm_date, wm_type, wm_obj_id, wm_ticker) VALUES " +
                                               $"('{Guid.NewGuid().ToString()}', '{wrId}', 1, '{UserHelper.GetUser().UserKey}', '{DateTime.Now}', '{(int)WorkType.ProjectWork}', '{list[i]}', 0);");
                                        }
                                    }
                                    sb.Append($"UPDATE work_registration SET wr_submit_status =2, wr_submit_date='{DateTime.Now}' WHERE wr_id='{wrId}';");
                                }
                                SqlHelper.ExecuteNonQuery(sb.ToString());
                                XtraMessageBox.Show("提交成功。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                                view.Rows.RemoveAt(e.RowIndex);
                            }
                        }
                        else
                            XtraMessageBox.Show("当前批次下尚有未提交的数据。", "提交失败", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                    else if (workType == WorkType.CDWork)
                    {
                        if (CanSubmitToQT(objId, WorkType.CDWork))
                        {
                            if (XtraMessageBox.Show("确定要将当前行数据提交到质检吗？", "提交确认", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                            {
                                StringBuilder sb = new StringBuilder();
                                object trpId = view.Rows[e.RowIndex].Cells["id"].Value;
                                object wrId = view.Rows[e.RowIndex].Cells["id"].Tag;
                                object pId = SqlHelper.ExecuteOnlyOneQuery($"SELECT pi_id FROM project_info WHERE trc_id='{trpId}'");
                                if (pId != null)//普通计划
                                {
                                    int count = SqlHelper.ExecuteCountQuery($"SELECT COUNT(wm_id) FROM work_myreg WHERE wm_obj_id='{pId}'");
                                    if (count == 0)
                                    {    //计划
                                        sb.Append($"INSERT INTO work_myreg(wm_id, wr_id, wm_status, wm_user, wm_date, wm_type, wm_obj_id, wm_ticker) VALUES " +
                                         $"('{Guid.NewGuid().ToString()}', '{wrId}', 1, '{UserHelper.GetUser().UserKey}', '{DateTime.Now}', '{(int)WorkType.CDWork_Plan}', '{pId}', 0);");
                                    }
                                    //项目|课题
                                    object[] list = SqlHelper.ExecuteSingleColumnQuery($"SELECT pi_id FROM (SELECT pi_id, pi_worker_id FROM project_info WHERE pi_obj_id='{pId}' UNION ALL " +
                                        $"SELECT ti_id, ti_worker_id FROM topic_info WHERE ti_obj_id='{pId}') tb1 " +
                                        $"WHERE tb1.pi_id NOT IN (SELECT wm_obj_id FROM work_myreg) AND pi_worker_id='{UserHelper.GetUser().UserKey}';");
                                    for (int i = 0; i < list.Length; i++)
                                        sb.Append($"INSERT INTO work_myreg(wm_id, wr_id, wm_status, wm_user, wm_date, wm_type, wm_obj_id, wm_ticker) VALUES " +
                                            $"('{Guid.NewGuid().ToString()}', '{wrId}', 1, '{UserHelper.GetUser().UserKey}', '{DateTime.Now}', '{(int)WorkType.ProjectWork}', '{list[i]}', 0);");

                                    sb.Append($"UPDATE work_registration SET wr_submit_status =2, wr_submit_date='{DateTime.Now}' WHERE wr_id='{wrId}';");

                                    SqlHelper.ExecuteNonQuery(sb.ToString());

                                    XtraMessageBox.Show("提交成功。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                                    view.Rows.RemoveAt(e.RowIndex);
                                }
                                else//重点研发
                                {
                                    pId = SqlHelper.ExecuteOnlyOneQuery($"SELECT imp_id FROM imp_info WHERE imp_obj_id='{trpId}'");
                                    if (pId != null)
                                    {
                                        int count = SqlHelper.ExecuteCountQuery($"SELECT COUNT(wm_id) FROM work_myreg WHERE wm_obj_id='{pId}'");
                                        if (count == 0)
                                            //计划
                                            sb.Append($"INSERT INTO work_myreg(wm_id, wr_id, wm_status, wm_user, wm_date, wm_type, wm_obj_id, wm_ticker) VALUES " +
                                           $"('{Guid.NewGuid().ToString()}', '{wrId}', 1, '{UserHelper.GetUser().UserKey}', '{DateTime.Now}', '{(int)WorkType.CDWork_Imp}', '{pId}', 0);");

                                        //专项
                                        object[] speList = SqlHelper.ExecuteSingleColumnQuery($"SELECT imp_id FROM imp_dev_info WHERE imp_obj_id='{pId}'");
                                        for (int i = 0; i < speList.Length; i++)
                                        {
                                            int count2 = SqlHelper.ExecuteCountQuery($"SELECT COUNT(wm_id) FROM work_myreg WHERE wm_obj_id='{speList[i]}'");
                                            if (count2 == 0)
                                                sb.Append($"INSERT INTO work_myreg(wm_id, wr_id, wm_status, wm_user, wm_date, wm_type, wm_obj_id, wm_ticker) VALUES " +
                                                $"('{Guid.NewGuid().ToString()}', '{wrId}', 1, '{UserHelper.GetUser().UserKey}', '{DateTime.Now}', '{(int)WorkType.CDWork_Special}', '{speList[i]}', 0);");

                                            //项目|课题
                                            object[] list = SqlHelper.ExecuteSingleColumnQuery($"SELECT pi_id FROM (SELECT pi_id, pi_worker_id FROM project_info WHERE pi_obj_id='{speList[i]}' UNION ALL " +
                                                $"SELECT ti_id, ti_worker_id FROM topic_info WHERE ti_obj_id='{speList[i]}') tb1 " +
                                                $"WHERE tb1.pi_id NOT IN (SELECT wm_obj_id FROM work_myreg) AND pi_worker_id='{UserHelper.GetUser().UserKey}';");
                                            for (int j = 0; j < list.Length; j++)
                                                sb.Append($"INSERT INTO work_myreg(wm_id, wr_id, wm_status, wm_user, wm_date, wm_type, wm_obj_id, wm_ticker) VALUES " +
                                                    $"('{Guid.NewGuid().ToString()}', '{wrId}', 1, '{UserHelper.GetUser().UserKey}', '{DateTime.Now}', '{(int)WorkType.ProjectWork}', '{list[j]}', 0);");
                                        }

                                        sb.Append($"UPDATE work_registration SET wr_submit_status =2, wr_submit_date='{DateTime.Now}' WHERE wr_id='{wrId}';");
                                        SqlHelper.ExecuteNonQuery(sb.ToString());

                                        XtraMessageBox.Show("提交成功。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                                        view.Rows.RemoveAt(e.RowIndex);
                                    }
                                }
                            }
                        }
                        else
                            XtraMessageBox.Show("当前数据尚未加工完成。", "提交失败", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                    else if (workType == WorkType.ProjectWork)
                    {
                        object proId = view.Rows[e.RowIndex].Cells["id"].Value;
                        if (CanSubmitToQT(proId, WorkType.ProjectWork))
                        {
                            if (XtraMessageBox.Show("确定要将当前行数据提交到质检吗？", "提交确认", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                            {
                                string sqlString = $"INSERT INTO work_myreg (wm_id, wr_id, wm_status, wm_user, wm_date, wm_type, wm_obj_id) VALUES " +
                                    $"('{Guid.NewGuid().ToString()}', '{objId}', 1, '{UserHelper.GetUser().UserKey}', '{DateTime.Now}', '{(int)WorkType.ProjectWork}', '{proId}');";
                                //同时提交当前课题所属计划
                                sqlString += $"UPDATE project_info SET pi_submit_status=2 WHERE pi_id=(SELECT pi_obj_id FROM project_info WHERE pi_id='{proId}');";
                                sqlString += $"UPDATE work_registration SET wr_submit_status=2, wr_submit_date='{DateTime.Now}' WHERE wr_id='{objId}';";
                                SqlHelper.ExecuteNonQuery(sqlString);

                                XtraMessageBox.Show("提交成功。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                                view.Rows.RemoveAt(e.RowIndex);
                            }
                        }
                        else
                            XtraMessageBox.Show("当前数据下尚有未提交项。", "操作失败", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                }
                //返工 - 编辑
                else if ("bk_edit".Equals(columnName))
                {
                    WorkType type = (WorkType)Convert.ToInt32(view.Rows[e.RowIndex].Tag);
                    if (type == WorkType.PaperWork_Imp || type == WorkType.CDWork_Imp)
                    {
                        object impId = view.Rows[e.RowIndex].Cells["bk_id"].Value;
                        object trpId = view.Rows[e.RowIndex].Cells["bk_id"].Tag;
                        Frm_MyWork frm = new Frm_MyWork(type, impId, trpId, ControlType.Imp, true);
                        frm.Show();
                    }
                    else if (type == WorkType.PaperWork_Plan)
                    {
                        object piId = view.Rows[e.RowIndex].Cells["bk_id"].Value;
                        object trpId = view.Rows[e.RowIndex].Cells["bk_id"].Tag;
                        Frm_MyWork frm = new Frm_MyWork(type, piId, trpId, ControlType.Plan, true);
                        frm.Show();
                    }
                    else if (type == WorkType.PaperWork_Special)
                    {
                        object impId = view.Rows[e.RowIndex].Cells["bk_id"].Value;
                        object trpId = view.Rows[e.RowIndex].Cells["bk_id"].Tag;
                        Frm_MyWork frm = new Frm_MyWork(type, impId, trpId, ControlType.Special, true);
                        frm.Show();
                    }
                    else if (type == WorkType.ProjectWork)
                    {
                        object piId = view.Rows[e.RowIndex].Cells["bk_id"].Value;
                        object trpId = view.Rows[e.RowIndex].Cells["bk_id"].Tag;
                        object trcId = SqlHelper.ExecuteOnlyOneQuery($"SELECT trc_id FROM project_info WHERE pi_id='{piId}' UNION ALL SELECT trc_id FROM topic_info WHERE ti_id='{piId}'");
                        Frm_MyWork frm = new Frm_MyWork(type, piId, trcId, ControlType.Project, true);
                        frm.trcId = trcId;
                        frm.Show();
                    }
                }
                //返工 - 提交
                else if ("bk_submit".Equals(columnName))
                {
                    WorkType type = (WorkType)Convert.ToInt32(view.Rows[e.RowIndex].Tag);
                    if (type == WorkType.PaperWork_Imp || type == WorkType.CDWork_Imp)
                    {
                        object impId = view.Rows[e.RowIndex].Cells["bk_id"].Value;
                        object status = SqlHelper.ExecuteOnlyOneQuery($"SELECT imp_submit_status FROM imp_info WHERE imp_id='{impId}'");
                        if (status.Equals(2))
                        {
                            if (XtraMessageBox.Show("确定要将当前数据提交至质检吗？", "确认提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk) == DialogResult.OK)
                            {
                                object wmid = view.Rows[e.RowIndex].Cells["bk_code"].Tag;//返工表主键
                                SqlHelper.ExecuteNonQuery($"UPDATE work_myreg SET wm_accepter_date='{DateTime.Now}', wm_status='{(int)QualityStatus.NonQuality}', wm_ticker+=1 WHERE wm_id='{wmid}'");

                                SetBackWorkNumber();
                                LoadWorkBackList();
                            }
                        }
                        else
                            XtraMessageBox.Show("请检查是否提交所有数据。", "操作失败", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                    else if (type == WorkType.PaperWork_Plan)
                    {
                        object piId = view.Rows[e.RowIndex].Cells["bk_id"].Value;
                        object status = SqlHelper.ExecuteOnlyOneQuery($"SELECT pi_submit_status FROM project_info WHERE pi_id='{piId}'");
                        if (status.Equals(2))
                        {
                            if (XtraMessageBox.Show("确定要将当前数据提交至质检吗？", "确认提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk) == DialogResult.OK)
                            {
                                object wmid = view.Rows[e.RowIndex].Cells["bk_code"].Tag;//返工表主键
                                SqlHelper.ExecuteNonQuery($"UPDATE work_myreg SET wm_accepter_date='{DateTime.Now}', wm_status='{(int)QualityStatus.NonQuality}', wm_user='{UserHelper.GetUser().UserKey}', wm_ticker+=1 WHERE wm_id='{wmid}'");

                                SetBackWorkNumber();
                                LoadWorkBackList();
                            }
                        }
                        else
                            XtraMessageBox.Show("请检查是否提交所有数据。", "操作失败", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                    else if (type == WorkType.PaperWork_Special)
                    {
                        object impId = view.Rows[e.RowIndex].Cells["bk_id"].Value;
                        object status = SqlHelper.ExecuteOnlyOneQuery($"SELECT imp_submit_status FROM imp_dev_info WHERE imp_id='{impId}'");
                        if (status.Equals(2))
                        {
                            if (XtraMessageBox.Show("确定要将当前数据提交至质检吗？", "确认提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk) == DialogResult.OK)
                            {
                                object wmid = view.Rows[e.RowIndex].Cells["bk_code"].Tag;//返工表主键
                                SqlHelper.ExecuteNonQuery($"UPDATE work_myreg SET wm_accepter_date='{DateTime.Now}', wm_status='{(int)QualityStatus.NonQuality}', wm_user='{UserHelper.GetUser().UserKey}', wm_ticker+=1 WHERE wm_id='{wmid}'");

                                SetBackWorkNumber();
                                LoadWorkBackList();
                            }
                        }
                        else
                            XtraMessageBox.Show("请检查是否提交所有数据。", "操作失败", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                    else if (type == WorkType.ProjectWork)
                    {
                        object piId = view.Rows[e.RowIndex].Cells["bk_id"].Value;
                        int count = SqlHelper.ExecuteCountQuery("SELECT COUNT(pi.pi_id) FROM project_info pi " +
                            $"LEFT JOIN topic_info ti ON pi.pi_id = ti.ti_obj_id AND ti_worker_id='{UserHelper.GetUser().UserKey}' " +
                            $"LEFT JOIN subject_info si ON ti.ti_id = si.si_obj_id AND si_worker_id='{UserHelper.GetUser().UserKey}' " +
                            $"WHERE pi.pi_id = '{piId}' AND (pi_submit_status = 1 OR ti.ti_submit_status = 1 OR si.si_submit_status = 1)");

                        count += SqlHelper.ExecuteCountQuery("SELECT COUNT(ti.ti_id) FROM topic_info ti " +
                            $"LEFT JOIN subject_info si ON ti.ti_id = si.si_obj_id AND si_worker_id='{UserHelper.GetUser().UserKey}' " +
                            $"WHERE ti.ti_id = '{piId}' AND (ti.ti_submit_status = 1 OR si.si_submit_status = 1)");
                        if (count == 0)
                        {
                            if (XtraMessageBox.Show("确定要将当前数据提交至质检吗？", "确认提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk) == DialogResult.OK)
                            {
                                object wmid = view.Rows[e.RowIndex].Cells["bk_code"].Tag;//返工表主键
                                //返工记录（记录最新时间，优先显示在质检人列表中；质检次数+1，统计被返工数）
                                SqlHelper.ExecuteNonQuery($"UPDATE work_myreg SET wm_accepter_date='{DateTime.Now}', wm_status='{(int)QualityStatus.Qualitting}', wm_ticker+=1 WHERE wm_id='{wmid}';");
                                SetBackWorkNumber();
                                LoadWorkBackList();
                            }
                        }
                        else
                            XtraMessageBox.Show("请检查是否提交所有数据。", "操作失败", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                }
                //我的加工 - 查看
                else if ("mw_control".Equals(columnName))
                {
                    object piId = view.Rows[e.RowIndex].Cells["mw_id"].Value;
                    object trpId = view.Rows[e.RowIndex].Cells["mw_id"].Tag;
                    object trcId = SqlHelper.ExecuteOnlyOneQuery($"SELECT trc_id FROM project_info WHERE pi_id='{piId}' UNION ALL SELECT trc_id FROM topic_info WHERE ti_id='{piId}'");
                    Frm_MyWork frm = GetFormHelper.GetMyWork(WorkType.ProjectWork, piId, trcId, ControlType.Project, true, true);
                    frm.Show();
                    frm.Activate();
                }
            }
        }

        /// <summary>
        /// 将字符串数组转换成指定分隔符组合成的字符串
        /// </summary>
        /// <param name="_str">字符串数组</param>
        /// <param name="flag">分隔符</param>
        /// <param name="param">引号类型</param>
        private string GetFullStringBySplit(object[] _str, string flag, string param)
        {
            string str = string.Empty;
            for(int i = 0; i < _str.Length; i++)
            {
                str += $"{param}{_str[i]}{param}{flag}";
            }
            return string.IsNullOrEmpty(str) ? string.Empty : str.Substring(0, str.Length - 1);
        }

        /// <summary>
        /// 根据指定ID查看是否其所属项目/课题是否全部提交
        /// </summary>
        /// <param name="objId">Work_Reg登记表主键</param>
        private bool CanSubmitToQT(object objId, WorkType workType)
        {
            if(workType == WorkType.PaperWork_Plan)
            {
                //计划
                DataRow firstRow = SqlHelper.ExecuteSingleRowQuery($"SELECT pi_id, pi_submit_status, pi_worker_id FROM project_info WHERE trc_id=" +
                    $"(SELECT wr_obj_id FROM work_registration WHERE wr_id='{objId}')");
                if(firstRow != null)
                {
                    int fstate = Convert.ToInt32(firstRow["pi_submit_status"]);
                    if(fstate == 1 && firstRow["pi_worker_id"].Equals(UserHelper.GetUser().UserKey)) return false;
                    else
                    {
                        //项目|课题
                        DataTable proTable = SqlHelper.ExecuteQuery($"SELECT pi_id, pi_submit_status, pi_worker_id FROM project_info WHERE pi_obj_id='{firstRow["pi_id"]}' UNION ALL " +
                            $"SELECT ti_id, ti_submit_status, ti_worker_id FROM topic_info WHERE ti_obj_id='{firstRow["pi_id"]}'");
                        foreach(DataRow proRow in proTable.Rows)
                        {
                            int state = Convert.ToInt32(proRow["pi_submit_status"]);
                            if(state == 1 && proRow["pi_worker_id"].Equals(UserHelper.GetUser().UserKey)) return false;
                            else
                            {
                                //课题|子课题
                                DataTable topTable = SqlHelper.ExecuteQuery($"SELECT ti_id, ti_submit_status, ti_worker_id FROM topic_info WHERE ti_obj_id='{proRow["pi_id"]}' UNION ALL " +
                                    $"SELECT si_id, si_submit_status, si_worker_id FROM subject_info WHERE si_obj_id='{proRow["pi_id"]}'");
                                foreach(DataRow topRow in topTable.Rows)
                                {
                                    int _state = Convert.ToInt32(topRow["ti_submit_status"]);
                                    if(_state == 1 && topRow["ti_worker_id"].Equals(UserHelper.GetUser().UserKey)) return false;
                                    else
                                    {
                                        DataTable subTable = SqlHelper.ExecuteQuery($"SELECT si_id, si_submit_status, si_worker_id FROM subject_info WHERE si_obj_id='{topRow["ti_id"]}'");
                                        foreach(DataRow subRow in subTable.Rows)
                                        {
                                            int __state = Convert.ToInt32(subRow["si_submit_status"]);
                                            if(__state == 1 && subRow["si_worker_id"].Equals(UserHelper.GetUser().UserKey)) return false;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            else if(workType == WorkType.PaperWork_Imp)
            {
                DataRow firstRow = SqlHelper.ExecuteSingleRowQuery($"SELECT imp_id, imp_submit_status, imp_source_id FROM imp_info WHERE imp_obj_id=(SELECT wr_obj_id FROM work_registration WHERE wr_id='{objId}')");
                if(firstRow != null)
                {
                    int fstate = Convert.ToInt32(firstRow["imp_submit_status"]);
                    if(fstate == 1 && firstRow["imp_source_id"].Equals(UserHelper.GetUser().UserKey)) return false;
                    else
                    {
                        DataTable sepTable = SqlHelper.ExecuteQuery($"SELECT imp_id, imp_submit_status, imp_source_id FROM imp_dev_info WHERE imp_obj_id = '{firstRow["imp_id"]}'");
                        foreach(DataRow speRow in sepTable.Rows)
                        {
                            int speState = Convert.ToInt32(speRow["imp_submit_status"]);
                            if(speState == 1 && speRow["imp_source_id"].Equals(UserHelper.GetUser().UserKey)) return false;
                            else
                            {
                                DataTable proTable = SqlHelper.ExecuteQuery($"SELECT pi_id, pi_submit_status, pi_worker_id FROM project_info WHERE pi_obj_id='{speRow["imp_id"]}' UNION ALL " +
                                    $"SELECT ti_id, ti_submit_status, ti_worker_id FROM topic_info WHERE ti_obj_id='{speRow["imp_id"]}'");
                                foreach(DataRow proRow in proTable.Rows)
                                {
                                    int state = Convert.ToInt32(proRow["pi_submit_status"]);
                                    if(state == 1 && proRow["pi_worker_id"].Equals(UserHelper.GetUser().UserKey)) return false;
                                    else
                                    {
                                        DataTable topTable = SqlHelper.ExecuteQuery($"SELECT ti_id, ti_submit_status, ti_worker_id FROM topic_info WHERE ti_obj_id='{proRow["pi_id"]}' UNION ALL " +
                                            $"SELECT si_id, si_submit_status, si_worker_id FROM subject_info WHERE si_obj_id='{proRow["pi_id"]}'");
                                        foreach(DataRow topRow in topTable.Rows)
                                        {
                                            int _state = Convert.ToInt32(topRow["ti_submit_status"]);
                                            if(_state == 1 && topRow["ti_worker_id"].Equals(UserHelper.GetUser().UserKey)) return false;
                                            else
                                            {
                                                DataTable subTable = SqlHelper.ExecuteQuery($"SELECT si_id, si_submit_status, si_worker_id FROM subject_info WHERE si_obj_id='{topRow["ti_id"]}'");
                                                foreach(DataRow subRow in subTable.Rows)
                                                {
                                                    int __state = Convert.ToInt32(subRow["si_submit_status"]);
                                                    if(__state == 1 && subRow["si_worker_id"].Equals(UserHelper.GetUser().UserKey)) return false;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            else if(workType == WorkType.PaperWork)
            {
                object[] imp = SqlHelper.ExecuteRowsQuery($"SELECT imp_id, imp_submit_status FROM imp_info WHERE imp_obj_id=(SELECT trp_id FROM work_registration WHERE wr_id='{objId}')");
                if(imp != null)
                {
                    if((ObjectSubmitStatus)Convert.ToInt32(imp[1]) == ObjectSubmitStatus.NonSubmit)
                        return false;
                    else
                    {
                        List<object[]> obj1 = SqlHelper.ExecuteColumnsQuery($"SELECT imp_id, imp_submit_status FROM imp_dev_info WHERE imp_obj_id='{imp[0]}'", 2);
                        for(int m = 0; m < obj1.Count; m++)
                        {
                            if((ObjectSubmitStatus)Convert.ToInt32(obj1[m][1]) == ObjectSubmitStatus.NonSubmit)
                                return false;
                            else
                            {
                                List<object[]> _obj2 = SqlHelper.ExecuteColumnsQuery($"SELECT pi_id, pi_submit_status FROM project_info WHERE pi_obj_id='{obj1[m][0]}'", 2);
                                for(int i = 0; i < _obj2.Count; i++)
                                {
                                    if(Convert.ToInt32(_obj2[i][1]) == (int)ObjectSubmitStatus.NonSubmit)
                                        return false;
                                    List<object[]> _obj3 = SqlHelper.ExecuteColumnsQuery($"SELECT si_id,si_submit_status FROM subject_info WHERE pi_id='{_obj2[i][0]}'", 2);
                                    for(int j = 0; j < _obj3.Count; j++)
                                    {
                                        if(Convert.ToInt32(_obj3[j][1]) == (int)ObjectSubmitStatus.NonSubmit)
                                            return false;
                                        List<object[]> _obj4 = SqlHelper.ExecuteColumnsQuery($"SELECT si_id,si_submit_status FROM subject_info WHERE pi_id='{_obj3[j][0]}'", 2);
                                        for(int k = 0; k < _obj4.Count; k++)
                                        {
                                            if(Convert.ToInt32(_obj4[k][1]) == (int)ObjectSubmitStatus.NonSubmit)
                                                return false;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                else
                    return false;
            }
            else if(workType == WorkType.CDWork)
            {
                //查询光盘下普通计划
                DataRow firstRow = SqlHelper.ExecuteSingleRowQuery($"SELECT pi_id, pi_submit_status FROM project_info WHERE trc_id=(SELECT wr_obj_id FROM work_registration WHERE wr_id='{objId}')");
                if(firstRow != null)
                {
                    int fstate = Convert.ToInt32(firstRow["pi_submit_status"]);
                    if(fstate == 1) return false;
                    else
                    {
                        DataTable proTable = SqlHelper.ExecuteQuery($"SELECT pi_id, pi_submit_status FROM project_info WHERE pi_obj_id='{firstRow["pi_id"]}' UNION ALL " +
                            $"SELECT ti_id, ti_submit_status FROM topic_info WHERE ti_obj_id='{firstRow["pi_id"]}'");
                        foreach(DataRow proRow in proTable.Rows)
                        {
                            int state = Convert.ToInt32(proRow["pi_submit_status"]);
                            if(state == 1) return false;
                            else
                            {
                                DataTable topTable = SqlHelper.ExecuteQuery($"SELECT ti_id, ti_submit_status FROM topic_info WHERE ti_obj_id='{proRow["pi_id"]}' UNION ALL " +
                                    $"SELECT si_id, si_submit_status FROM subject_info WHERE si_obj_id='{proRow["pi_id"]}'");
                                foreach(DataRow topRow in topTable.Rows)
                                {
                                    int _state = Convert.ToInt32(topRow["ti_submit_status"]);
                                    if(_state == 1) return false;
                                    else
                                    {
                                        DataTable subTable = SqlHelper.ExecuteQuery($"SELECT si_id, si_submit_status FROM subject_info WHERE si_obj_id='{topRow["ti_id"]}'");
                                        foreach(DataRow subRow in subTable.Rows)
                                        {
                                            int __state = Convert.ToInt32(subRow["si_submit_status"]);
                                            if(__state == 1) return false;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    //光盘下的重大专项|重点研发
                    firstRow = SqlHelper.ExecuteSingleRowQuery($"SELECT imp_id, imp_submit_status FROM imp_info WHERE imp_obj_id=(SELECT wr_obj_id FROM work_registration WHERE wr_id='{objId}')");
                    if(firstRow != null)
                    {
                        int fstate = Convert.ToInt32(firstRow["imp_submit_status"]);
                        if(fstate == 1) return false;
                        else
                        {
                            DataTable speTable = SqlHelper.ExecuteQuery($"SELECT imp_id, imp_submit_status FROM imp_dev_info WHERE imp_obj_id='{firstRow["imp_id"]}'");
                            foreach(DataRow speRow in speTable.Rows)
                            {
                                int sstate = Convert.ToInt32(speRow["imp_submit_status"]);
                                if(sstate == 1) return false;
                                else
                                {
                                    DataTable proTable = SqlHelper.ExecuteQuery($"SELECT pi_id, pi_submit_status FROM project_info WHERE pi_obj_id='{speRow["imp_id"]}' UNION ALL " +
                                        $"SELECT ti_id, ti_submit_status FROM topic_info WHERE ti_obj_id='{speRow["imp_id"]}'");
                                    foreach(DataRow proRow in proTable.Rows)
                                    {
                                        int state = Convert.ToInt32(proRow["pi_submit_status"]);
                                        if(state == 1) return false;
                                        else
                                        {
                                            DataTable topTable = SqlHelper.ExecuteQuery($"SELECT ti_id, ti_submit_status FROM topic_info WHERE ti_obj_id='{proRow["pi_id"]}' UNION ALL " +
                                                $"SELECT si_id, si_submit_status FROM subject_info WHERE si_obj_id='{proRow["pi_id"]}'");
                                            foreach(DataRow topRow in topTable.Rows)
                                            {
                                                int _state = Convert.ToInt32(topRow["ti_submit_status"]);
                                                if(_state == 1) return false;
                                                else
                                                {
                                                    DataTable subTable = SqlHelper.ExecuteQuery($"SELECT si_id, si_submit_status FROM subject_info WHERE si_obj_id='{topRow["ti_id"]}'");
                                                    foreach(DataRow subRow in subTable.Rows)
                                                    {
                                                        int __state = Convert.ToInt32(subRow["si_submit_status"]);
                                                        if(__state == 1) return false;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

            }
            else if(workType == WorkType.ProjectWork)
            {
                DataRow proRow = SqlHelper.ExecuteSingleRowQuery($"SELECT pi_id, pi_submit_status FROM project_info WHERE pi_id='{objId}'");
                if(proRow != null)
                {
                    int statu = Convert.ToInt32(proRow["pi_submit_status"]);
                    if(statu == 2)
                    {
                        DataTable _topRow = SqlHelper.ExecuteQuery($"SELECT ti_id, ti_submit_status FROM topic_info WHERE ti_obj_id='{proRow["pi_id"]}' AND ti_worker_id='{UserHelper.GetUser().UserKey}';");
                        foreach(DataRow item in _topRow.Rows)
                        {
                            int _statu = Convert.ToInt32(item["ti_submit_status"]);
                            if(_statu == 2)
                            {
                                DataTable _subRow = SqlHelper.ExecuteQuery($"SELECT si_id, si_submit_status FROM subject_info WHERE si_obj_id='{item["ti_id"]}' AND si_worker_id='{UserHelper.GetUser().UserKey}';");
                                foreach(DataRow _item in _subRow.Rows)
                                {
                                    int __statu = Convert.ToInt32(_item["si_submit_status"]);
                                    if(__statu != 2)
                                        return false;
                                }
                            }
                            else return false;
                        }
                    }
                    else return false;
                }
                else
                {
                    DataRow topRow = SqlHelper.ExecuteSingleRowQuery($"SELECT ti_id, ti_submit_status FROM topic_info WHERE ti_id='{objId}'");
                    if(topRow != null)
                    {
                        int _statu = Convert.ToInt32(topRow["ti_submit_status"]);
                        if(_statu == 2)
                        {
                            DataTable _subRow = SqlHelper.ExecuteQuery($"SELECT si_id, si_submit_status FROM subject_info WHERE si_obj_id='{topRow["ti_id"]}' AND si_worker_id='{UserHelper.GetUser().UserKey}';");
                            foreach(DataRow _item in _subRow.Rows)
                            {
                                int __statu = Convert.ToInt32(_item["si_submit_status"]);
                                if(__statu != 2)
                                    return false;
                            }
                        }
                        else return false;
                    }
                }
            }
            else
            {
                object[] _obj = SqlHelper.ExecuteRowsQuery($"SELECT wr_type, wr_obj_id FROM work_registration WHERE wr_id='{objId}'");
                if(!string.IsNullOrEmpty(GetValue(_obj)))
                {
                    WorkType type = (WorkType)_obj[0];
                    object rootId = GetRootId(_obj[1], type);
                    List<object[]> _obj2 = new List<object[]>();
                    if(type == WorkType.PaperWork)
                    {
                        object rootStatu = SqlHelper.ExecuteOnlyOneQuery($"SELECT pi_submit_status FROM project_info WHERE pi_id='{rootId}'");
                        if(rootStatu == null || (Convert.ToInt32(rootStatu) == 1))
                            return false;
                        _obj2 = SqlHelper.ExecuteColumnsQuery($"SELECT pi_id, pi_submit_status FROM project_info WHERE pi_obj_id='{rootId}'", 2);
                        for(int i = 0; i < _obj2.Count; i++)
                        {
                            if(Convert.ToInt32(_obj2[i][1]) == 1)
                                return false;
                            List<object[]> _obj3 = SqlHelper.ExecuteColumnsQuery($"SELECT ti_id, ti_submit_status FROM topic_info WHERE ti_obj_id='{_obj2[i][0]}'", 2);
                            for(int j = 0; j < _obj3.Count; j++)
                            {
                                if(Convert.ToInt32(_obj3[j][1]) == 1)
                                    return false;
                                List<object[]> _obj4 = SqlHelper.ExecuteColumnsQuery($"SELECT si_id, si_submit_status FROM subject_info WHERE si_obj_id='{_obj3[j][0]}'", 2);
                                for(int k = 0; k < _obj4.Count; k++)
                                {
                                    if(Convert.ToInt32(_obj4[k][1]) == 1)
                                        return false;
                                }
                            }
                        }
                    }
                    else
                    {
                        if(type == WorkType.TopicWork)
                            _obj2 = SqlHelper.ExecuteColumnsQuery($"SELECT pi_id, pi_submit_status FROM project_info WHERE pi_obj_id='{rootId}'", 2);
                        else
                            _obj2 = SqlHelper.ExecuteColumnsQuery($"SELECT pi_id, pi_submit_status FROM project_info WHERE pi_obj_id='{rootId}' AND pi_worker_id='{UserHelper.GetUser().UserKey}'", 2);
                        for(int i = 0; i < _obj2.Count; i++)
                        {
                            if(Convert.ToInt32(_obj2[i][1]) == (int)ObjectSubmitStatus.NonSubmit)
                                return false;
                            List<object[]> _obj3 = SqlHelper.ExecuteColumnsQuery($"SELECT si_id,si_submit_status FROM subject_info WHERE pi_id='{_obj2[i][0]}' AND si_worker_id='{UserHelper.GetUser().UserKey}'", 2);
                            for(int j = 0; j < _obj3.Count; j++)
                            {
                                if(Convert.ToInt32(_obj3[j][1]) == (int)ObjectSubmitStatus.NonSubmit)
                                    return false;
                                List<object[]> _obj4 = SqlHelper.ExecuteColumnsQuery($"SELECT si_id,si_submit_status FROM subject_info WHERE pi_id='{_obj3[j][0]}' AND si_worker_id='{UserHelper.GetUser().UserKey}'", 2);
                                for(int k = 0; k < _obj4.Count; k++)
                                {
                                    if(Convert.ToInt32(_obj4[k][1]) == (int)ObjectSubmitStatus.NonSubmit)
                                        return false;
                                }
                            }
                        }
                    }
                }
            }
            return true;
        }
     
        /// <summary>
        /// 获取指定项目/课题获取其所属计划ID
        /// </summary>
        private object GetRootId(object objId, WorkType type)
        {
            if(type == WorkType.CDWork)
            {
                return SqlHelper.ExecuteOnlyOneQuery($"SELECT pi_id FROM project_info WHERE trc_id='{objId}'") ?? SqlHelper.ExecuteOnlyOneQuery($"SELECT pi_id FROM project_info WHERE pi_obj_id='{objId}'");
            }
            else if(type == WorkType.PaperWork)
            {
                return SqlHelper.ExecuteOnlyOneQuery($"SELECT pi_id FROM project_info WHERE trc_id='{objId}'");
            }
            if(type == WorkType.ProjectWork)
            {
                return SqlHelper.ExecuteOnlyOneQuery($"SELECT pi_obj_id FROM project_info WHERE pi_id='{objId}'") ?? SqlHelper.ExecuteOnlyOneQuery($"SELECT ti_obj_id FROM topic_info WHERE ti_id='{objId}'");
            }
            else if(type == WorkType.TopicWork)
            {
                object pid = SqlHelper.ExecuteOnlyOneQuery($"SELECT pi_id FROM subject_info WHERE si_id='{objId}'");
                return GetRootId(pid, WorkType.ProjectWork);
            }
            return null;
        }
     
        /// <summary>
        /// 判断当前加工是否只有一条
        /// </summary>
        /// <returns></returns>
        private bool CheckCanReceive()
        {
            int amount = Convert.ToInt32(SqlHelper.ExecuteOnlyOneQuery($"SELECT COUNT(wr_id) FROM work_registration WHERE wr_status={(int)WorkStatus.WorkSuccess} AND wr_submit_status ={(int)ObjectSubmitStatus.NonSubmit} AND wr_source_id='{UserHelper.GetUser().UserKey}'"));
            /* 测试注释 */
                            return amount == 0 ? true : false;
        }

        private string GetValue(object value) => value == null ? string.Empty : value.ToString();
     
        /// <summary>
        /// 根据光盘编号获取对应的项目下的课题总数
        /// </summary>
        /// <param name="trcid">光盘ID</param>
        private int GetSubjectAmountWithProject(object trcid)
        {
            int totalAmount = 0;
            List<object[]> list = SqlHelper.ExecuteColumnsQuery($"SELECT pi_id FROM project_info WHERE pi_obj_id=" +
                $"(SELECT pi_id FROM project_info WHERE trc_id='{trcid}')", 1);
            for (int i = 0; i < list.Count; i++)
                totalAmount += Convert.ToInt32(SqlHelper.ExecuteOnlyOneQuery($"SELECT COUNT(*) FROM subject_info WHERE pi_id='{list[i][0]}'"));
            return totalAmount;
        }
      
        /// <summary>
        /// 加载课题/子课题列表
        /// </summary>
        /// <param name="pid">项目/课题ID</param>
        private void LoadTopicList(object pid)
        {
            DataGridViewStyleHelper.ResetDataGridView(view);
            view.Columns.AddRange(new DataGridViewColumn[]
            {
                new DataGridViewTextBoxColumn(){ Name = "si_id", SortMode = DataGridViewColumnSortMode.NotSortable },
                new DataGridViewTextBoxColumn(){ Name = "si_code", HeaderText = "课题/子课题编号", FillWeight = 12 , SortMode = DataGridViewColumnSortMode.NotSortable },
                new DataGridViewTextBoxColumn(){ Name = "si_name", HeaderText = "课题/子课题名称", FillWeight = 15 , SortMode = DataGridViewColumnSortMode.NotSortable },
                new DataGridViewTextBoxColumn(){ Name = "si_file_amount", HeaderText = "文件数", FillWeight = 5 , SortMode = DataGridViewColumnSortMode.NotSortable },
            });

            DataTable table = null;
            string querySql = $"SELECT ti_id, ti_code, ti_name FROM topic_info WHERE ti_obj_id='{pid}' AND ti_work_status = 1 " +
                 "UNION ALL " +
                $"SELECT si_id, si_code, si_name FROM subject_info WHERE si_obj_id = '{pid}' AND si_work_status = 1 " +
                $"ORDER BY ti_code";
            table = SqlHelper.ExecuteQuery(querySql);
            foreach (DataRow row in table.Rows)
            {
                int _index = view.Rows.Add();
                view.Rows[_index].Cells["si_id"].Value = row["ti_id"];
                view.Rows[_index].Cells["si_code"].Value = row["ti_code"];
                view.Rows[_index].Cells["si_name"].Value = row["ti_name"];
                view.Rows[_index].Cells["si_file_amount"].Value = GetFileAmount(row["ti_id"]);
            }
            if (view.Columns.Count > 0)
                view.Columns[0].Visible = false;

            DataGridViewStyleHelper.SetAlignWithCenter(view, new string[] { "si_file_amount" });

            view.Columns["si_id"].Visible = false;

            LastIdLog = new string[] { LastIdLog[1], $"Subject_{pid}" };
        }
        
        /// <summary>
        /// 光盘列表
        /// </summary>
        /// <param name="trpId">批次主键</param>
        private void LoadCDList(object trpId)
        {
            DataGridViewStyleHelper.ResetDataGridView(view);
            view.Columns.AddRange(new DataGridViewColumn[]
            {
                new DataGridViewTextBoxColumn(){ Name = "trc_id"},
                new DataGridViewTextBoxColumn(){ Name = "dd_name", FillWeight = 18 , SortMode = DataGridViewColumnSortMode.NotSortable },
                new DataGridViewTextBoxColumn(){ Name = "trc_code", FillWeight = 20 , SortMode = DataGridViewColumnSortMode.NotSortable },
                new DataGridViewTextBoxColumn(){ Name = "trc_name", FillWeight = 20 , SortMode = DataGridViewColumnSortMode.NotSortable },
                new DataGridViewLinkColumn(){ Name = "trc_total_amount", FillWeight = 10 , SortMode = DataGridViewColumnSortMode.NotSortable },
                new DataGridViewTextBoxColumn(){ Name = "trc_receive_amount", FillWeight = 10 , SortMode = DataGridViewColumnSortMode.NotSortable },
                new DataGridViewTextBoxColumn(){ Name = "trc_file_amount", FillWeight = 7, SortMode = DataGridViewColumnSortMode.NotSortable  },
                new DataGridViewButtonColumn(){ Name = "trc_control", FillWeight = 7, Text = "领取", UseColumnTextForButtonValue = true, SortMode = DataGridViewColumnSortMode.NotSortable  },
            });
            //增加TreeView
            TreeView tv = new TreeView();
            tv.Nodes.AddRange(new TreeNode[] {
                new TreeNode("主键"),
                new TreeNode("来源单位"),
                new TreeNode("光盘编号"),
                new TreeNode("光盘名称"),
                new TreeNode("项目/课题"),
                new TreeNode("文件数"),
                new TreeNode("操作"),
            });
            tv.Nodes[4].Nodes.AddRange(new TreeNode[] {
                new TreeNode("总数"),
                new TreeNode("已领取数"),
            });
            DataGridViewStyleHelper.SetTreeViewHeader(view, tv);

            DataTable table = null;
            StringBuilder querySql = new StringBuilder("SELECT trc_id, dd_name, trc_code, trc_name, trc_status, trc_complete_status");
            querySql.Append(" FROM transfer_registraion_cd trc LEFT JOIN(");
            querySql.Append(" SELECT trp.trp_id, dd_name, dd_sort FROM transfer_registration_pc trp, data_dictionary dd WHERE trp.com_id = dd.dd_id ) tb");
            querySql.Append($" ON trc.trp_id = tb.trp_id WHERE trc.trp_id='{trpId}' ORDER BY dd_sort ASC, trc_code ASC");
            table = SqlHelper.ExecuteQuery(querySql.ToString());
            foreach(DataRow row in table.Rows)
            {
                int totalAmount = GetProjectAmount(row["trc_id"]);
                int receiveAmount = GetReceiveAmount(row["trc_id"]);
                WorkStatus status = (WorkStatus)Convert.ToInt32(row["trc_complete_status"]);
                //如果是非结构化 且 已被领取，则不显示
                if(totalAmount == 0 && status == WorkStatus.WorkSuccess)
                    continue;
                int _index = view.Rows.Add();
                view.Rows[_index].Cells["trc_id"].Value = row["trc_id"];
                view.Rows[_index].Cells["dd_name"].Value = row["dd_name"];
                view.Rows[_index].Cells["trc_code"].Value = row["trc_code"];
                view.Rows[_index].Cells["trc_name"].Value = row["trc_name"];
                view.Rows[_index].Cells["trc_total_amount"].Value = totalAmount;
                view.Rows[_index].Cells["trc_receive_amount"].Value = receiveAmount;
                view.Rows[_index].Cells["trc_file_amount"].Value = GetFileAmount(row["trc_id"]);
            }

            DataGridViewStyleHelper.SetAlignWithCenter(view, new string[] { "trc_receive_amount", "trc_file_amount", "trc_total_amount" });

            view.Columns["trc_id"].Visible = false;
            LastIdLog = new string[] { LastIdLog[1], $"CD_{trpId}" };
        }
        
        /// <summary>
        /// 根据光盘ID获取文件数
        /// </summary>
        /// <param name="fid">光盘ID</param>
        private int GetFileAmount(object fid) => SqlHelper.ExecuteCountQuery($"SELECT COUNT(pfl_id) FROM processing_file_list WHERE pfl_obj_id='{fid}'");
        
        /// <summary>
        /// 根据光盘ID获取已领取项目总数
        /// </summary>
        private int GetReceiveAmount(object trcId)
        {
            int proAmount = 0;
            if(trcId != null)
            {
                proAmount = SqlHelper.ExecuteCountQuery($"SELECT COUNT(pi_id) FROM project_info WHERE trc_id='{trcId}' AND pi_work_status={(int)WorkStatus.WorkSuccess}");
                proAmount += SqlHelper.ExecuteCountQuery($"SELECT COUNT(ti_id) FROM topic_info WHERE trc_id='{trcId}' AND ti_work_status={(int)WorkStatus.WorkSuccess}");
            }
            return proAmount;
        }

        /// <summary>
        /// 根据光盘ID获取项目总数
        /// </summary>
        private int GetProjectAmount(object trcId)
        {
            int proAmount = 0;
            if(trcId != null)
                proAmount = SqlHelper.ExecuteCountQuery($"SELECT COUNT(pi_id) + (SELECT COUNT(ti_id) FROM topic_info WHERE trc_id='{trcId}') FROM project_info WHERE trc_id='{trcId}'");
            return proAmount;
        }
    
        /// <summary>
        /// 加载光盘下的项目/课题列表
        /// </summary>
        /// <param name="trcId">光盘ID</param>
        private void LoadProjectList(object trcId)
        {
            DataGridViewStyleHelper.ResetDataGridView(view);
            view.Columns.AddRange(new DataGridViewColumn[] {
                new DataGridViewTextBoxColumn(){ Name = "pi_id"},
                new DataGridViewTextBoxColumn(){ Name = "pi_code", HeaderText = "项目/课题编号", FillWeight = 12, SortMode = DataGridViewColumnSortMode.NotSortable },
                new DataGridViewTextBoxColumn(){ Name = "pi_name", HeaderText = "项目/课题名称", FillWeight = 25, SortMode = DataGridViewColumnSortMode.NotSortable },
                new DataGridViewTextBoxColumn(){ Name = "pi_company", HeaderText = "承担单位", FillWeight = 20, SortMode = DataGridViewColumnSortMode.NotSortable },
                new DataGridViewLinkColumn(){ Name = "pi_total_amount", HeaderText = "总数", FillWeight = 9, SortMode = DataGridViewColumnSortMode.NotSortable },
                new DataGridViewTextBoxColumn(){ Name = "pi_receive_amount", HeaderText = "已领取数", FillWeight = 9, SortMode = DataGridViewColumnSortMode.NotSortable },
                new DataGridViewTextBoxColumn(){ Name = "pi_file_amount", HeaderText = "文件数", FillWeight = 9, SortMode = DataGridViewColumnSortMode.NotSortable },
                new DataGridViewButtonColumn() { Name = "pi_control", HeaderText = "操作", FillWeight = 10, SortMode = DataGridViewColumnSortMode.NotSortable }
            });
            TreeView tree = new TreeView();
            tree.Nodes.AddRange(new TreeNode[] {
                new TreeNode(),
                new TreeNode("项目/课题编号"),
                new TreeNode("项目/课题名称"),
                new TreeNode("承担单位"),
                new TreeNode("课题/子课题"),
                new TreeNode("文件数"),
                new TreeNode("操作")
            }); 
            tree.Nodes[4].Nodes.AddRange(new TreeNode[]
            {
                new TreeNode("总数"),
                new TreeNode("已领取数"),
            });
            DataGridViewStyleHelper.SetTreeViewHeader(view, tree);

            DataTable table = null;
            string querySql = $"SELECT pi_id, pi_code, pi_name, pi_unit, pi_work_status FROM project_info pi " +
                $"WHERE trc_id = '{trcId}' AND pi_work_status = '1' " +
                $"UNION ALL " +
                $"SELECT ti_id, ti_code, ti_name, ti_unit, ti_work_status FROM topic_info ti " +
                $"WHERE trc_id = '{trcId}' AND ti_work_status = '1' " +
                $"ORDER BY pi_code";
            table = SqlHelper.ExecuteQuery(querySql);
            foreach (DataRow row in table.Rows)
            {
                int totalAmount = GetTopicAmount(row["pi_id"]);
                int receiveAmount = GetTopicReceiveAmount(row["pi_id"]);
                if(totalAmount == 0 || totalAmount != receiveAmount)
                {
                    int _index = view.Rows.Add();
                    view.Rows[_index].Cells["pi_id"].Value = row["pi_id"];
                    view.Rows[_index].Cells["pi_code"].Value = row["pi_code"];
                    view.Rows[_index].Cells["pi_name"].Value = row["pi_name"];
                    view.Rows[_index].Cells["pi_company"].Value = row["pi_unit"];
                    view.Rows[_index].Cells["pi_total_amount"].Value = totalAmount;
                    view.Rows[_index].Cells["pi_receive_amount"].Value = receiveAmount;
                    view.Rows[_index].Cells["pi_file_amount"].Value = 0;//文件数待处理
                    view.Rows[_index].Cells["pi_control"].Value = GetWorkValue(row["pi_work_status"]);
                }
            }
            if (view.Columns.Count > 0)
                view.Columns[0].Visible = false;

            DataGridViewStyleHelper.SetAlignWithCenter(view, new string[] { "pi_total_amount", "pi_receive_amount", "pi_file_amount" });

            view.Columns["pi_id"].Visible = false;

            LastIdLog = new string[] { LastIdLog[1], $"Project_{trcId}" };
        }
        
        /// <summary>
        /// 获取加工结果
        /// </summary>
        private object GetWorkValue(object index) => (WorkStatus)Convert.ToInt32(index) == WorkStatus.NonWork ? "领取" : "已领取";
     
        /// <summary>
        ///  根据父级ID获取子级已领取列表
        /// </summary>
        private int GetTopicReceiveAmount(object pid)
        {
            int topAmount = 0;
            if (pid != null)
                topAmount = SqlHelper.ExecuteCountQuery($"SELECT COUNT(ti_id)+(SELECT COUNT(si_id) FROM subject_info WHERE si_obj_id='{pid}' AND si_work_status='2') FROM topic_info WHERE ti_obj_id='{pid}' AND ti_work_status='2'");
            return topAmount;
        }
 
        /// <summary>
        /// 根据项目id获取课题数
        /// </summary>
        private int GetTopicAmount(object pid)
        {
            int topAmount = 0;
            if (pid != null)
                topAmount = SqlHelper.ExecuteCountQuery($"SELECT COUNT(ti_id)+(SELECT COUNT(si_id) FROM subject_info WHERE si_obj_id='{pid}') FROM topic_info WHERE ti_obj_id='{pid}'");
            return topAmount;
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
        /// 返回上一页
        /// </summary>
        private void Btn_Back_Click(object sender, EventArgs e)
        {
            if ("CD".Equals(LastIdLog[1].Split('_')[0]))
            {
                LoadPCList(null, null);
            }
            else if ("Project".Equals(LastIdLog[1].Split('_')[0]))
            {
                LoadCDList(LastIdLog[0].Split('_')[1]);
            }
            else if ("Subject".Equals(LastIdLog[1].Split('_')[0]))
            {
                LoadPCList(null, null);
            }
        }
    
        /// <summary>
        /// 来源单位切换事件
        /// </summary>
        private void Cbo_CompanyList_SelectionChangeCommitted(object sender, EventArgs e)
        {
            object csid = cbo_CompanyList.SelectedValue;
            if ("all".Equals(csid))
            {
                if ("WORK_LIST".Equals(view.Tag))
                    LoadWorkList(null, WorkStatus.WorkSuccess);
                else
                    LoadPCList(null, null);
            }
            else
            {
                if ("WORK_LIST".Equals(view.Tag))
                    LoadWorkList(csid, WorkStatus.WorkSuccess);
                else
                    LoadPCList(null, csid);
            }
        }

        private void Txt_Search_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            Txt_Search_KeyDown(sender, new KeyEventArgs(Keys.Enter));
        }

        private void Ace_LeftMenu_SelectedElementChanged(object sender, SelectedElementChangedEventArgs e)
        {
            pal_UnitList.Visible = "ac_Login".Equals(e.Element.Name);
        }

        int index = -1;
        private void Txt_Search_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                if("PC_LIST".Equals(view.Tag))
                {
                    string key = txt_Search.Text;
                    if(!string.IsNullOrEmpty(key))
                    {
                        view.ClearSelection();
                        foreach(DataGridViewRow row in view.Rows)
                        {
                            if(row.Index > index)
                            {
                                string code = GetValue(row.Cells["trp_code"].Value);
                                string name = GetValue(row.Cells["trp_name"].Value);
                                if(code.Contains(key) || name.Contains(key) ||
                                    code.Contains(key.ToUpper()) || name.Contains(key.ToUpper()))
                                {
                                    view.FirstDisplayedScrollingRowIndex = row.Index;
                                    row.Selected = true;
                                    index = row.Index;
                                    return;
                                }
                            }
                        }
                        index = -1;
                    }
                }
            }
        }

        private void btn_MyWorkQuery_Click(object sender, EventArgs e)
        {
            string sData = txt_SearchDate_S.Text;
            string fData = txt_SearchData_F.Text;
            if (!string.IsNullOrEmpty(sData))
                sData = $"AND TRY_CAST(ISNULL(wm.wm_date, A.pi_worker_date) AS DATE) >= '{sData}'";
            if (!string.IsNullOrEmpty(fData))
                fData = $"AND TRY_CAST(ISNULL(wm.wm_date, A.pi_worker_date) AS DATE) <= '{fData}'";
            LoadMyWorkedList(sData, fData);
        }

        private void comboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            int i = comboBox1.SelectedIndex;
            if (i == 0)
            {
                txt_SearchDate_S.ResetText();
                txt_SearchData_F.ResetText();
            }
            else
            {
                DateTime now = DateTime.Now;
                txt_SearchData_F.Text = now.ToString("yyyy-MM-dd");
                if (i == 1)//最近三天
                {
                    txt_SearchDate_S.Text = now.AddDays(-3).ToString("yyyy-MM-dd");
                }
                else if (i == 2)//最近一周
                {
                    txt_SearchDate_S.Text = now.AddDays(-7).ToString("yyyy-MM-dd");
                }
                else if (i == 3)//最近一个月
                {
                    txt_SearchDate_S.Text = now.AddMonths(-1).ToString("yyyy-MM-dd");
                }
            }
        }

        private void Frm_CG_FormClosing(object sender, FormClosingEventArgs e)
        {
            Controls.Clear();
        }
    }
}
