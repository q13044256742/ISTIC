using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using 科技计划项目档案数据采集管理系统.Properties;

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
            InitialLeftMenu();

            LoadPCList(null, null);

            //列头样式
            dgv_WorkLog.ColumnHeadersDefaultCellStyle = DataGridViewStyleHelper.GetHeaderStyle();

            //默认来源单位
            LoadCompanyList();
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
        /// 待加工批次列表
        /// </summary>
        /// <param name="querySql">指定的查询语句</param>
        /// <param name="csid">来源单位ID</param>
        private void LoadPCList(StringBuilder querySql, object csid)
        {
            DataGridViewStyleHelper.ResetDataGridView(dgv_WorkLog);
            dgv_WorkLog.Columns.AddRange(new DataGridViewColumn[]
            {
                new DataGridViewTextBoxColumn(){ Name = "trp_id"},
                new DataGridViewTextBoxColumn(){ Name = "dd_name", HeaderText= "来源单位", FillWeight = 15},
                new DataGridViewTextBoxColumn(){ Name = "trp_name", HeaderText= "批次名称", FillWeight = 20},
                new DataGridViewTextBoxColumn(){ Name = "trp_code", HeaderText= "批次编号", FillWeight = 20},
                new DataGridViewTextBoxColumn(){ Name = "trp_finishtime", HeaderText= "完成时间", FillWeight = 10},
                new DataGridViewLinkColumn(){ Name = "trp_cd_amount", HeaderText= "光盘数", FillWeight = 10},
                new DataGridViewButtonColumn(){ Name = "trp_control", HeaderText= "操作", FillWeight = 10, Text = "加工", UseColumnTextForButtonValue = true},
            });
            if(querySql == null)
            {
                querySql = new StringBuilder("SELECT pc.trp_id, dd_name, trp_name, trp_code, trp_cd_amount");
                querySql.Append(" FROM transfer_registration_pc pc LEFT JOIN data_dictionary dd ON pc.com_id = dd.dd_id");
                querySql.Append(" WHERE pc.trp_work_status=1");
                if(csid != null)
                    querySql.Append($" AND dd.dd_id='{csid}'");
            }
            DataTable dataTable = SqlHelper.ExecuteQuery(querySql.ToString());
            for(int i = 0; i < dataTable.Rows.Count; i++)
            {
                DataRow row = dataTable.Rows[i];
                int index = dgv_WorkLog.Rows.Add();
                dgv_WorkLog.Rows[index].Cells["trp_id"].Value = row["trp_id"];
                dgv_WorkLog.Rows[index].Cells["dd_name"].Value = row["dd_name"];
                dgv_WorkLog.Rows[index].Cells["trp_name"].Value = row["trp_name"];
                dgv_WorkLog.Rows[index].Cells["trp_code"].Value = row["trp_code"];
                dgv_WorkLog.Rows[index].Cells["trp_finishtime"].Value = null;//完成时间字段待定
                dgv_WorkLog.Rows[index].Cells["trp_cd_amount"].Value = row["trp_cd_amount"];
            }
            DataGridViewStyleHelper.SetAlignWithCenter(dgv_WorkLog, new string[] { "trp_cd_amount", "trp_control" });
            dgv_WorkLog.Columns["trp_id"].Visible = false;
            LastIdLog = new string[] { string.Empty, $"PC_{csid}" };

            dgv_WorkLog.Tag = "PC_LIST";
        }
    
        /// <summary>
        /// 加载左侧菜单
        /// </summary>
        private void InitialLeftMenu()
        {
            pal_LeftMenu.Controls.Clear();
            Image[] imgs = new Image[] { Resources.pic1, Resources.pic2, Resources.pic3, Resources.pic4, Resources.pic5, Resources.pic6, Resources.pic7, Resources.pic8 };
            List<CreateKyoPanel.KyoPanel> list = new List<CreateKyoPanel.KyoPanel>();
            list.Add(new CreateKyoPanel.KyoPanel
            {
                Name = "CG",
                Text = "著录加工",
                Image = imgs[0],
                HasNext = true
            });
            CreateKyoPanel.SetPanel(pal_LeftMenu, list);

            list.Clear();
            list.AddRange(new CreateKyoPanel.KyoPanel[]
            {
                new CreateKyoPanel.KyoPanel
                {
                    Name ="CG_LOGIN",
                    Text="加工登记",
                    HasNext = false
                },
                new CreateKyoPanel.KyoPanel
                {
                    Name ="CG_WORK_ING",
                    Text="加工中",
                    HasNext = false
                },
                new CreateKyoPanel.KyoPanel
                {
                    Name="CG_WORK_ED",
                    Text=$"已返工({BackWorkAmount})",
                    HasNext = false
                }
            });
            Panel parentPanel = pal_LeftMenu.Controls.Find("CG", false)[0] as Panel;
            CreateKyoPanel.SetSubPanel(parentPanel, list, Sub_Menu_Click);
        }

        /// <summary>
        /// 获取当前用户的已返工数
        /// </summary>
        /// <returns></returns>
        private object BackWorkAmount => SqlHelper.ExecuteOnlyOneQuery($"SELECT COUNT(*) FROM work_myreg WHERE wm_status=4 AND wm_user='{UserHelper.GetInstance().User.UserKey}'");

        /// <summary>
        /// 二级菜单点击事件（加工登记/加工中/已返工）
        /// </summary>
        private void Sub_Menu_Click(object sender, EventArgs e)
        {
            Panel panel = null;
            if(sender is Panel) panel = sender as Panel;
            else if(sender is Label) panel = (sender as Label).Parent as Panel;
            if("CG_LOGIN".Equals(panel.Name))//加工登记
            {
                LoadPCList(null, null);
                cbo_CompanyList.SelectedIndex = 0;
            } else if("CG_WORK_ING".Equals(panel.Name))//加工中
            {
                LoadWorkList(null, WorkStatus.WorkSuccess);
            }
            else if("CG_WORK_ED".Equals(panel.Name))//已返工
            {
                LoadWorkBackList();
            }
            foreach(Panel item in panel.Parent.Controls)
            {
                if(item.Equals(panel))
                    item.BackColor = Color.Purple;
                else
                    item.BackColor = Color.Transparent;
            }
        }
        
        /// <summary>
        /// 加载已返工列表
        /// </summary>
        private void LoadWorkBackList()
        {
            DataGridViewStyleHelper.ResetDataGridView(dgv_WorkLog);
            dgv_WorkLog.Columns.AddRange(new DataGridViewColumn[] {
                new DataGridViewTextBoxColumn(){ Name = "bk_id" },
                new DataGridViewTextBoxColumn(){ Name = "bk_code", HeaderText = "编号", FillWeight = 15 },
                new DataGridViewTextBoxColumn(){ Name = "bk_name", HeaderText = "名称", FillWeight = 20 },
                new DataGridViewTextBoxColumn(){ Name = "bk_reason", HeaderText = "返工原因", FillWeight = 15 },
                new DataGridViewButtonColumn(){ Name = "bk_edit", HeaderText = "操作", FillWeight = 7, Text = "编辑", UseColumnTextForButtonValue = true },
                new DataGridViewButtonColumn(){ Name = "bk_submit", HeaderText = "提交", FillWeight = 7, Text = "提交质检", UseColumnTextForButtonValue = true},
            });

            string querySql = $"SELECT * FROM work_myreg WHERE wm_status='{(int)QualityStatus.QualityBack}' AND wm_user='{UserHelper.GetInstance().User.UserKey}'";
            DataTable table = SqlHelper.ExecuteQuery(querySql);
            for(int i = 0; i < table.Rows.Count; i++)
            {
                int type = (int)table.Rows[i]["wm_type"];
                object id = table.Rows[i]["wm_obj_id"];
                if(type == 0)
                {
                    DataRow _row = SqlHelper.ExecuteSingleRowQuery($"SELECT imp_id, imp_code, imp_name, imp_intro, imp_obj_id FROM imp_info WHERE imp_id='{id}'");
                    int index = dgv_WorkLog.Rows.Add();
                    dgv_WorkLog.Rows[index].Tag = type;
                    dgv_WorkLog.Rows[index].Cells["bk_id"].Tag = _row["imp_obj_id"];
                    dgv_WorkLog.Rows[index].Cells["bk_id"].Value = _row["imp_id"];
                    dgv_WorkLog.Rows[index].Cells["bk_code"].Tag = table.Rows[i]["wm_id"];
                    dgv_WorkLog.Rows[index].Cells["bk_code"].Value = _row["imp_code"];
                    dgv_WorkLog.Rows[index].Cells["bk_name"].Value = _row["imp_name"];
                    dgv_WorkLog.Rows[index].Cells["bk_reason"].Value = GetAdvicesById(_row["imp_id"]);
                }
                else if(type == -1)
                {
                    DataTable _table = SqlHelper.ExecuteQuery($"SELECT pi_id, pi_code, pi_name, pi_obj_id FROM project_info WHERE pi_id='{id}'");
                    if(_table.Rows.Count > 0)
                    {
                        DataRow _row = _table.Rows[0];
                        int index = dgv_WorkLog.Rows.Add();
                        dgv_WorkLog.Rows[index].Tag = type;
                        dgv_WorkLog.Rows[index].Cells["bk_id"].Tag = _row["pi_obj_id"];
                        dgv_WorkLog.Rows[index].Cells["bk_id"].Value = _row["pi_id"];
                        dgv_WorkLog.Rows[index].Cells["bk_code"].Tag = table.Rows[i]["wm_id"];//供删除使用
                        dgv_WorkLog.Rows[index].Cells["bk_code"].Value = _row["pi_code"];
                        dgv_WorkLog.Rows[index].Cells["bk_name"].Value = _row["pi_name"];
                        dgv_WorkLog.Rows[index].Cells["bk_reason"].Value = GetAdvicesById(_row["pi_id"]);
                    }
                }
                else if(type == 1)
                {
                    DataRow _row = SqlHelper.ExecuteSingleRowQuery($"SELECT imp_id, imp_code, imp_name, imp_intro, imp_obj_id FROM imp_dev_info WHERE imp_id='{id}'");
                    int index = dgv_WorkLog.Rows.Add();
                    dgv_WorkLog.Rows[index].Tag = type;
                    dgv_WorkLog.Rows[index].Cells["bk_id"].Tag = _row["imp_obj_id"];
                    dgv_WorkLog.Rows[index].Cells["bk_id"].Value = _row["imp_id"];
                    dgv_WorkLog.Rows[index].Cells["bk_code"].Tag = table.Rows[i]["wm_id"];//供删除使用
                    dgv_WorkLog.Rows[index].Cells["bk_code"].Value = _row["imp_code"];
                    dgv_WorkLog.Rows[index].Cells["bk_name"].Value = _row["imp_name"];
                    dgv_WorkLog.Rows[index].Cells["bk_reason"].Value = GetAdvicesById(_row["imp_id"]);
                }
                else if(type == 2)
                {
                    DataTable _table = SqlHelper.ExecuteQuery($"SELECT pi_id, pi_code, pi_name, pi_obj_id FROM project_info WHERE pi_id='{id}'");
                    if(_table.Rows.Count > 0)
                    {
                        DataRow _row = _table.Rows[0];
                        int index = dgv_WorkLog.Rows.Add();
                        dgv_WorkLog.Rows[index].Tag = type;
                        dgv_WorkLog.Rows[index].Cells["bk_id"].Tag = _row["pi_obj_id"];
                        dgv_WorkLog.Rows[index].Cells["bk_id"].Value = _row["pi_id"];
                        dgv_WorkLog.Rows[index].Cells["bk_code"].Tag = table.Rows[i]["wm_id"];//供删除使用
                        dgv_WorkLog.Rows[index].Cells["bk_code"].Value = _row["pi_code"];
                        dgv_WorkLog.Rows[index].Cells["bk_name"].Value = _row["pi_name"];
                        dgv_WorkLog.Rows[index].Cells["bk_reason"].Value = GetAdvicesById(_row["pi_id"]);
                    }
                }
            }
            dgv_WorkLog.Columns["bk_id"].Visible = false;
        }
       
        /// <summary>
        /// 获取质检意见
        /// </summary>
        private object GetAdvicesById(object objid)
        {
            StringBuilder sb = new StringBuilder();
            List<object[]> _obj = SqlHelper.ExecuteColumnsQuery($"SELECT distinct(qa_type) FROM quality_advices WHERE qa_obj_id='{objid}'", 1);
            for(int i = 0; i < _obj.Count; i++)
            {
                int index = Convert.ToInt32(_obj[i][0]);
                if(index == 0)
                    sb.Append("基本信息 ");
                else if(index == 1)
                    sb.Append("文件列表 ");
                else if(index == 2)
                    sb.Append("文件核查 ");
                else if(index == 3)
                    sb.Append("案卷信息 ");
                else if(index == 4)
                    sb.Append("案卷盒");
            }
            return sb.ToString();
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
                case WorkType.SubjectWork:
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
            DataGridViewStyleHelper.ResetDataGridView(dgv_WorkLog);
            string querySql = $" SELECT wr_id, wr_type, wr.wr_obj_id FROM work_registration wr LEFT JOIN transfer_registration_pc trp ON wr.trp_id = trp.trp_id LEFT JOIN data_dictionary dd ON trp.com_id = dd.dd_id " +
                $"WHERE wr_status = {(int)workStatus} AND wr_submit_status={(int)ObjectSubmitStatus.NonSubmit} " +
                $"AND wr_source_id='{UserHelper.GetInstance().User.UserKey}'";
            if(unitId != null)
                querySql += $" AND dd.dd_id='{unitId}'";
            List<object[]> list = SqlHelper.ExecuteColumnsQuery(querySql, 3);
            List<object[]> resultList = new List<object[]>();
            for(int i = 0; i < list.Count; i++)
            {
                WorkType type = (WorkType)list[i][1];
                object id = list[i][2];
                string _querySql = null;
                switch(type)
                {
                    case WorkType.PaperWork:
                        _querySql = $"SELECT '{list[i][0]}','{id}', trp_code, trp_name, '{(int)type}' FROM transfer_registration_pc WHERE trp_id='{id}'";
                        break;
                    case WorkType.CDWork:
                        _querySql = $"SELECT '{list[i][0]}','{id}', trc_code, trc_name, '{(int)type}' FROM transfer_registraion_cd WHERE trc_id='{id}'";
                        break;
                    case WorkType.ProjectWork:
                        _querySql = $"SELECT '{list[i][0]}','{id}', pi_code, pi_name, '{(int)type}' FROM project_info WHERE pi_id='{id}'";
                        break;
                    case WorkType.SubjectWork:
                        _querySql = $"SELECT '{list[i][0]}','{id}', si_code, si_name, '{(int)type}' FROM subject_info WHERE si_id='{id}'";
                        break;
                    default:
                        _querySql = string.Empty;
                        break;
                }
                object[] _obj = SqlHelper.ExecuteRowsQuery(_querySql);
                if(_obj != null)
                    resultList.Add(_obj);
            }
            dgv_WorkLog.Columns.AddRange(new DataGridViewColumn[]
            {
                new DataGridViewTextBoxColumn(){Name = "id"},
                new DataGridViewTextBoxColumn(){Name = "code", HeaderText = "编号", FillWeight = 15},
                new DataGridViewTextBoxColumn(){Name = "name", HeaderText = "名称", FillWeight = 20},
                new DataGridViewTextBoxColumn(){Name = "dd_name", HeaderText = "来源单位", FillWeight = 20},
                new DataGridViewTextBoxColumn(){Name = "type", HeaderText = "加工类型", FillWeight = 15},
                new DataGridViewButtonColumn(){Name = "edit", HeaderText = "操作", FillWeight = 7, Text = "编辑", UseColumnTextForButtonValue = true},
                new DataGridViewButtonColumn(){Name = "submit", HeaderText = "提交", FillWeight = 7, Text = "提交质检", UseColumnTextForButtonValue = true},
            });
            for(int i = 0; i < resultList.Count; i++)
            {
                int index = dgv_WorkLog.Rows.Add();
                dgv_WorkLog.Rows[index].Cells["id"].Tag = resultList[i][0];
                dgv_WorkLog.Rows[index].Cells["id"].Value = resultList[i][1];
                dgv_WorkLog.Rows[index].Cells["code"].Value = resultList[i][2];
                dgv_WorkLog.Rows[index].Cells["name"].Value = resultList[i][3];
                object[] _obj = SqlHelper.GetCompanyByParam(resultList[i][1], resultList[i][4]);
                dgv_WorkLog.Rows[index].Cells["dd_name"].Tag = _obj[0];
                dgv_WorkLog.Rows[index].Cells["dd_name"].Value = _obj[1];
                dgv_WorkLog.Rows[index].Cells["type"].Value = GetTypeValue(resultList[i][4]);
                dgv_WorkLog.Rows[index].Cells["type"].Tag = (WorkType)Convert.ToInt32(resultList[i][4]);
            }
            dgv_WorkLog.Columns["id"].Visible = false;
            DataGridViewStyleHelper.SetAlignWithCenter(dgv_WorkLog, new string[] { "type" });

            dgv_WorkLog.Tag = "WORK_LIST";
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
            else if(type == WorkType.SubjectWork)
                return "课题/子课题加工";
            else
                return null;
        }
        
        /// <summary>
        /// 单元格点击事件
        /// </summary>
        private void Dgv_WorkLog_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex != -1 && e.ColumnIndex != -1 && dgv_WorkLog.Rows[e.RowIndex].Cells[0].Value!= null)
            {
                object columnName = dgv_WorkLog.Columns[e.ColumnIndex].Name;
                //光盘数
                if("trp_cd_amount".Equals(columnName))
                {
                    if(!dgv_WorkLog.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.Equals(0))
                    {
                        object trpid = dgv_WorkLog.Rows[e.RowIndex].Cells["trp_id"].Value;
                        LoadCDList(trpid);
                    }
                }
                //光盘页 - 总数
                else if("trc_total_amount".Equals(columnName))
                {
                    if(0 != Convert.ToInt32(dgv_WorkLog.Rows[e.RowIndex].Cells[e.ColumnIndex].Value))
                    {
                        object trcid = dgv_WorkLog.Rows[e.RowIndex].Cells["trc_id"].Value;
                        LoadProjectList(trcid);
                    }
                }
                //项目/课题 - 总数
                else if("pi_total_amount".Equals(columnName))
                {
                    if(0 != Convert.ToInt32(dgv_WorkLog.Rows[e.RowIndex].Cells[e.ColumnIndex].Value))
                    {
                        object pid = dgv_WorkLog.Rows[e.RowIndex].Cells["pi_id"].Value;
                        LoadSubjectList(pid);
                    }
                }
                //光盘 - 加工
                else if("trc_control".Equals(columnName))
                {
                    if(CheckCanReceive())
                    {
                        int totalAmount = Convert.ToInt32(dgv_WorkLog.Rows[e.RowIndex].Cells["trc_total_amount"].Value);
                        //不能直接领取结构化光盘
                        if(totalAmount == 0)
                        {
                            object trcid = dgv_WorkLog.Rows[e.RowIndex].Cells["trc_id"].Value;
                            string msg = $"本张光盘包括{GetProjectAmount(trcid)}个项目，{GetSubjectAmountWithProject(trcid)}个课题；是否开始加工？";
                            if(MessageBox.Show(msg, "确认提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                            {
                                object trpId = SqlHelper.ExecuteOnlyOneQuery($"SELECT trp_id FROM transfer_registraion_cd WHERE trc_id='{trcid}'");
                                
                                object primaryKey = Guid.NewGuid().ToString();
                                string insertSql = $"INSERT INTO work_registration VALUES('" +
                                    $"{primaryKey}',{(int)WorkStatus.WorkSuccess},'{trpId}',{(int)WorkType.CDWork},'{DateTime.Now}',null,'{trcid}',{(int)ObjectSubmitStatus.NonSubmit}," +
                                    $"{(int)ReceiveStatus.NonReceive},'{UserHelper.GetInstance().User.UserKey}',0)";
                                SqlHelper.ExecuteNonQuery(insertSql);

                                SqlHelper.ExecuteNonQuery($"UPDATE transfer_registraion_cd SET trc_complete_status={(int)WorkStatus.WorkSuccess}, trc_complete_user='{UserHelper.GetInstance().User.UserKey}' WHERE trc_id='{trcid}'");
                                //领取光盘的同时 - 领取其下所有未领取的项目 / 课题 / 子课题
                                object pid = SqlHelper.ExecuteOnlyOneQuery($"SELECT pi_id FROM project_info WHERE trc_id='{trcid}'");
                                //【计划】
                                SqlHelper.ExecuteOnlyOneQuery($"UPDATE project_info SET pi_work_status={(int)WorkStatus.WorkSuccess}, pi_worker_id='{UserHelper.GetInstance().User.UserKey}' WHERE pi_id='{pid}'");
                                //【项目 / 课题】
                                List<object[]> _obj1 = SqlHelper.ExecuteColumnsQuery($"SELECT pi_id FROM project_info WHERE pi_obj_id='{pid}' AND pi_work_status={(int)WorkStatus.NonWork}", 1);
                                for(int i = 0; i < _obj1.Count; i++)
                                {
                                    SqlHelper.ExecuteOnlyOneQuery($"UPDATE project_info SET pi_work_status={(int)WorkStatus.WorkSuccess}, pi_worker_id='{UserHelper.GetInstance().User.UserKey}' WHERE pi_id='{_obj1[i][0]}'");
                                    //【课题 / 子课题】
                                    List<object[]> _obj2 = SqlHelper.ExecuteColumnsQuery($"SELECT si_id FROM subject_info WHERE pi_id='{_obj1[i][0]}' AND si_work_status={(int)WorkStatus.NonWork}", 1);
                                    for(int j = 0; j < _obj2.Count; j++)
                                    {
                                        SqlHelper.ExecuteOnlyOneQuery($"UPDATE subject_info SET si_work_status={(int)WorkStatus.WorkSuccess}, si_worker_id='{UserHelper.GetInstance().User.UserKey}' WHERE si_id='{_obj2[j][0]}'");
                                        //【子课题】
                                        List<object[]> _obj3 = SqlHelper.ExecuteColumnsQuery($"SELECT si_id FROM subject_info WHERE pi_id='{_obj2[j][0]}' AND si_work_status={(int)WorkStatus.NonWork}", 1);
                                        for(int k = 0; k < _obj3.Count; k++)
                                        {
                                            SqlHelper.ExecuteOnlyOneQuery($"UPDATE subject_info SET si_work_status={(int)WorkStatus.WorkSuccess}, si_worker_id='{UserHelper.GetInstance().User.UserKey}' WHERE si_id='{_obj3[k][0]}'");
                                        }
                                    }
                                }
                                //跳转到我的加工页面
                                LoadWorkList(null, WorkStatus.WorkSuccess);
                            }
                        }
                        else
                            MessageBox.Show("此操作不被允许！", "领取失败", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                    else
                        MessageBox.Show("请先完成当前已领取且尚未完成的加工项。", "领取失败");
                }
                //批次 - 加工
                else if("trp_control".Equals(columnName))
                {
                    if(CheckCanReceive())
                    {
                        int totalAmount = Convert.ToInt32(dgv_WorkLog.Rows[e.RowIndex].Cells["trp_cd_amount"].Value);
                        if(totalAmount == 0)
                        {
                            object trpid = dgv_WorkLog.Rows[e.RowIndex].Cells["trp_id"].Value;
                            //如果当前纸本加工尚未被首次领取，则判断当前登录人是否是【管理员】，否则不允许领取
                            object completeUser = SqlHelper.ExecuteOnlyOneQuery($"SELECT trp_complete_user FROM transfer_registration_pc WHERE trp_id='{trpid}'");
                            if(string.IsNullOrEmpty(GetValue(completeUser)))
                            {
                                if(UserHelper.GetInstance().GetUserRole() == UserRole.Worker)
                                {
                                    if(MessageBox.Show("是否确认加工当前选中批次？", "确认提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                                    {
                                        object primaryKey = Guid.NewGuid().ToString();
                                        string insertSql = $"INSERT INTO work_registration VALUES('{primaryKey}', {(int)WorkStatus.WorkSuccess}, '{trpid}', {(int)WorkType.PaperWork}," +
                                            $"'{DateTime.Now}', null, '{trpid}', {(int)ObjectSubmitStatus.NonSubmit}, {(int)ReceiveStatus.NonReceive}, '{UserHelper.GetInstance().User.UserKey}', 0)";
                                        SqlHelper.ExecuteNonQuery(insertSql);
                                        string updateSql = $"UPDATE transfer_registration_pc SET trp_work_status={(int)WorkStatus.WorkSuccess}, trp_complete_user='{UserHelper.GetInstance().User.UserKey}' WHERE trp_id='{trpid}'";
                                        SqlHelper.ExecuteNonQuery(updateSql);

                                        //跳转到我的加工页面
                                        LoadWorkList(null, WorkStatus.WorkSuccess);
                                    }
                                }
                                else//非管理人员不允许首次领取
                                    MessageBox.Show("此操作不被允许！", "领取失败", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                            }
                            else//非首次领取，则可直接以普通用户身份领取【继承管理员的加工】
                            {
                                if(MessageBox.Show("是否确认加工当前选中批次？", "确认提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                                {
                                    object primaryKey = Guid.NewGuid().ToString();
                                    string insertSql = $"INSERT INTO work_registration VALUES('{primaryKey}',{(int)WorkStatus.WorkSuccess},'{trpid}',{(int)WorkType.PaperWork}," +
                                        $"'{DateTime.Now}',null,'{trpid}',{(int)ObjectSubmitStatus.NonSubmit},{(int)ReceiveStatus.NonReceive},'{UserHelper.GetInstance().User.UserKey}',0)";
                                    SqlHelper.ExecuteNonQuery(insertSql);

                                    //跳转到我的加工页面
                                    LoadWorkList(null, WorkStatus.WorkSuccess);
                                }
                            }
                        }
                        else
                            MessageBox.Show("此操作不被允许！", "领取失败", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                    else
                        MessageBox.Show("请先完成当前已领取且尚未完成的加工项。", "领取失败");
                }
                //项目/课题 - 加工
                else if("pi_control".Equals(columnName))
                {
                    if(CheckCanReceive())
                    {
                        int totalAmount = GetInt32(dgv_WorkLog.Rows[e.RowIndex].Cells["pi_total_amount"].Value);
                        if(MessageBox.Show($"是否确认加工当前选中项目/课题{(totalAmount <= 10 ? "（包括所有课题/子课题）" : string.Empty)}？", "确认提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                        {
                            /* 领取当前任务和直属上级任务 */
                            object piid = dgv_WorkLog.Rows[e.RowIndex].Cells["pi_id"].Value;
                            object trcid = SqlHelper.ExecuteOnlyOneQuery($"SELECT trc_id FROM project_info WHERE pi_id=(SELECT pi_obj_id FROM project_info WHERE pi_id='{piid}')");
                            object trpid = SqlHelper.ExecuteOnlyOneQuery($"SELECT trp_id FROM transfer_registraion_cd WHERE trc_id='{trcid}'");
                            //第一个领取人同时领取当前项目/课题直属计划
                            SqlHelper.ExecuteNonQuery($"UPDATE transfer_registration_pc SET trp_complete_status={(int)WorkStatus.WorkSuccess},trp_complete_user='{UserHelper.GetInstance().User.UserKey}' WHERE trp_id='{trpid}'");
                            SqlHelper.ExecuteNonQuery($"UPDATE transfer_registraion_cd SET trc_complete_status={(int)WorkStatus.WorkSuccess},trc_complete_user='{UserHelper.GetInstance().User.UserKey}' WHERE trc_id='{trcid}'");
                            //领取当前选定任务
                            object primaryKey = Guid.NewGuid().ToString();
                            string insertSql = $"INSERT INTO work_registration VALUES('{primaryKey}',{(int)WorkStatus.WorkSuccess},'{trpid}',{(int)WorkType.ProjectWork}," +
                                $"'{DateTime.Now}',null,'{piid}',{(int)ObjectSubmitStatus.NonSubmit},{(int)ReceiveStatus.NonReceive},'{UserHelper.GetInstance().User.UserKey}',0)";
                            SqlHelper.ExecuteNonQuery(insertSql);
                            string updateSql = $"UPDATE project_info SET pi_work_status={(int)WorkStatus.WorkSuccess}, pi_worker_id='{UserHelper.GetInstance().User.UserKey}' WHERE pi_id='{piid}'";
                            SqlHelper.ExecuteNonQuery(updateSql);
                            /* 如果当前项目下的子课题数<=10，则同时领取其下全部任务 */
                            if(totalAmount <= 10)
                            {
                                //领取子级【课题/子课题】
                                List<object[]> _obj2 = SqlHelper.ExecuteColumnsQuery($"SELECT si_id FROM subject_info WHERE pi_id='{piid}' AND si_work_status={(int)WorkStatus.NonWork}", 1);
                                for(int j = 0; j < _obj2.Count; j++)
                                {
                                    SqlHelper.ExecuteOnlyOneQuery($"UPDATE subject_info SET si_work_status={(int)WorkStatus.WorkSuccess}, si_worker_id='{UserHelper.GetInstance().User.UserKey}' WHERE si_id='{_obj2[j][0]}'");
                                    //【子课题】
                                    List<object[]> _obj3 = SqlHelper.ExecuteColumnsQuery($"SELECT si_id FROM subject_info WHERE pi_id='{_obj2[j][0]}' AND si_work_status={(int)WorkStatus.NonWork}", 1);
                                    for(int k = 0; k < _obj3.Count; k++)
                                        SqlHelper.ExecuteOnlyOneQuery($"UPDATE subject_info SET si_work_status={(int)WorkStatus.WorkSuccess}, si_worker_id='{UserHelper.GetInstance().User.UserKey}' WHERE si_id='{_obj3[k][0]}'");
                                }
                                //跳转到我的加工页面
                                LoadWorkList(null, WorkStatus.WorkSuccess);
                            }
                        }
                    }
                    else
                        MessageBox.Show("请先完成当前已领取且尚未完成的加工项。", "领取失败");
                }
                //课题/子课题 - 加工
                else if("si_control".Equals(columnName))
                {
                    if(CheckCanReceive())
                    {
                        if(MessageBox.Show("是否确认加工当前选中课题/子课题？", "确认提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                        {
                            object siid = dgv_WorkLog.Rows[e.RowIndex].Cells["si_id"].Value;
                            object trpid = SqlHelper.ExecuteOnlyOneQuery($"SELECT trp.trp_id FROM transfer_registration_pc trp, transfer_registraion_cd trc, project_info pi, subject_info si WHERE trp.trp_id = trc.trp_id AND pi.trc_id = trc.trc_id AND si.pi_id = pi.pi_id AND si.si_id = '{siid}'");

                            object primaryKey = Guid.NewGuid().ToString();
                            string insertSql = $"INSERT INTO work_registration VALUES('{primaryKey}',{(int)WorkStatus.WorkSuccess},'{trpid}',{(int)WorkType.SubjectWork},'{DateTime.Now}'" +
                                $",null,'{siid}',{(int)ObjectSubmitStatus.NonSubmit},{(int)ReceiveStatus.NonReceive},'{UserHelper.GetInstance().User.UserKey}',0)";
                            SqlHelper.ExecuteNonQuery(insertSql);

                            string updateSql = $"UPDATE subject_info SET si_work_status={(int)WorkStatus.WorkSuccess},si_worker_id='{UserHelper.GetInstance().User.UserKey}' WHERE si_id='{siid}'";
                            SqlHelper.ExecuteNonQuery(updateSql);
                            //【子课题】
                            List<object[]> _obj3 = SqlHelper.ExecuteColumnsQuery($"SELECT si_id FROM subject_info WHERE pi_id='{siid}' AND si_work_status={(int)WorkStatus.NonWork}", 1);
                            for(int k = 0; k < _obj3.Count; k++)
                                SqlHelper.ExecuteOnlyOneQuery($"UPDATE subject_info SET si_work_status={(int)WorkStatus.WorkSuccess}, si_worker_id='{UserHelper.GetInstance().User.UserKey}' WHERE si_id='{_obj3[k][0]}'");
                           
                            //跳转到我的加工页面
                            LoadWorkList(null, WorkStatus.WorkSuccess);
                        }
                    }
                    else
                        MessageBox.Show("请先完成当前已领取且尚未完成的加工项。", "领取失败");
                }
                //编辑 - 开始加工
                else if("edit".Equals(columnName))
                {
                    object objId = dgv_WorkLog.Rows[e.RowIndex].Cells["id"].Value;
                    string typeValue = GetValue(dgv_WorkLog.Rows[e.RowIndex].Cells["type"].Value);
                    if(typeValue.Contains("光盘"))
                    {
                        object planId = SqlHelper.ExecuteOnlyOneQuery($"SELECT pi_id FROM project_info WHERE trc_id='{objId}'");
                        if(planId != null)//项目/课题
                            new Frm_MyWork(WorkType.CDWork, planId, null, ControlType.Default,false).ShowDialog();
                        else
                        {
                            planId = SqlHelper.ExecuteOnlyOneQuery($"SELECT pi_id FROM project_info WHERE pi_obj_id='{objId}'");
                            if(planId == null)
                            {
                                planId = SqlHelper.ExecuteOnlyOneQuery($"SELECT imp_id FROM imp_info WHERE imp_obj_id='{objId}'");
                                if(planId == null)
                                {
                                    Frm_ProTypeSelect frm = new Frm_ProTypeSelect(WorkType.CDWork, objId);
                                    frm.unitCode = dgv_WorkLog.Rows[e.RowIndex].Cells["dd_name"].Tag;
                                    frm.ShowDialog();
                                }
                                else
                                {
                                    Frm_MyWork frm = new Frm_MyWork(WorkType.Default, planId, objId, ControlType.Imp, false);
                                    frm.planCode = GetValue(SqlHelper.ExecuteOnlyOneQuery($"SELECT pi_code FROM project_info WHERE pi_id='{planId}'"));
                                    frm.unitCode = dgv_WorkLog.Rows[e.RowIndex].Cells["dd_name"].Tag;
                                    frm.ShowDialog();
                                }
                            }
                            else
                            {
                                Frm_MyWork frm = new Frm_MyWork(WorkType.CDWork, planId, objId, ControlType.Plan,false);
                                frm.planCode = GetValue(SqlHelper.ExecuteOnlyOneQuery($"SELECT pi_code FROM project_info WHERE pi_id='{planId}'"));
                                frm.unitCode = dgv_WorkLog.Rows[e.RowIndex].Cells["dd_name"].Tag;
                                frm.ShowDialog();
                            }
                        }
                    }
                    else if(typeValue.Contains("纸本"))
                    {
                        //普通计划
                        object planId = SqlHelper.ExecuteOnlyOneQuery($"SELECT pi_id FROM project_info WHERE trc_id='{objId}'");
                        if(planId != null)
                            new Frm_MyWork(WorkType.PaperWork, planId, null, ControlType.Default,false).ShowDialog();
                        //重点研发计划
                        else
                            planId = SqlHelper.ExecuteOnlyOneQuery($"SELECT imp_id FROM imp_info WHERE imp_obj_id='{objId}'");
                        if(planId != null)
                        {
                            Frm_MyWork frm = new Frm_MyWork(WorkType.Default, planId, objId, ControlType.Imp,false);
                            frm.ShowDialog();
                        }
                        else
                        {
                            planId = SqlHelper.ExecuteOnlyOneQuery($"SELECT pi_id FROM project_info WHERE pi_obj_id='{objId}'");
                            if(planId == null)
                            {
                                Frm_ProTypeSelect frm = new Frm_ProTypeSelect(WorkType.PaperWork, objId);
                                frm.unitCode = dgv_WorkLog.Rows[e.RowIndex].Cells["dd_name"].Tag;
                                frm.ShowDialog();
                            }
                            else
                            {
                                Frm_MyWork frm = new Frm_MyWork(WorkType.PaperWork, planId, objId, ControlType.Plan,false);
                                frm.planCode = GetValue(SqlHelper.ExecuteOnlyOneQuery($"SELECT pi_code FROM project_info WHERE pi_id='{planId}'"));
                                frm.unitCode = dgv_WorkLog.Rows[e.RowIndex].Cells["dd_name"].Tag;
                                frm.ShowDialog();
                            }
                        }
                    }
                    else
                    {
                        //根据当前id获取根节点ID
                        if(typeValue.Contains("项目/课题"))
                        {
                            object rootId = GetRootId(objId, WorkType.ProjectWork);
                            if(string.IsNullOrEmpty(GetValue(rootId)))
                            {
                                MessageBox.Show("无法找到当前项目/课题所属计划。", "操作失败");
                            }
                            else
                            {
                                Frm_MyWork frm = new Frm_MyWork(WorkType.ProjectWork, rootId, objId, ControlType.Default,false);
                                frm.SetUnitSourceId(dgv_WorkLog.Rows[e.RowIndex].Cells["dd_name"].Tag);
                                frm.ShowDialog();
                            }
                        }
                        else if(typeValue.Contains("课题/子课题"))
                        {
                            object rootId = GetRootId(objId, WorkType.SubjectWork);
                            if(string.IsNullOrEmpty(GetValue(rootId)))
                            {
                                MessageBox.Show("无法找到当前课题/子课题所属计划。", "操作失败");
                            }
                            else
                            {
                                Frm_MyWork myWork = new Frm_MyWork(WorkType.SubjectWork, rootId, objId, ControlType.Default,false);
                                myWork.ShowDialog();
                            }
                        }

                    }
                }
                //提交质检
                else if("submit".Equals(columnName))
                {
                    object objId = dgv_WorkLog.Rows[e.RowIndex].Cells["id"].Tag;
                    WorkType workType = (WorkType)dgv_WorkLog.Rows[e.RowIndex].Cells["type"].Tag;
                    if(workType == WorkType.SubjectWork)
                        MessageBox.Show("此操作不被允许！", "提交失败", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    else if(workType == WorkType.PaperWork)
                    {
                        WorkType _type = WorkType.Default;
                        //重点研发
                        object completeUser = SqlHelper.ExecuteOnlyOneQuery("SELECT imp_source_id FROM imp_info WHERE imp_obj_id=" +
                            $"(SELECT trp_id FROM work_registration WHERE wr_id='{objId}')");
                        //普通
                        if(string.IsNullOrEmpty(GetValue(completeUser)))
                        {
                            completeUser = SqlHelper.ExecuteOnlyOneQuery("SELECT trp_complete_user FROM transfer_registration_pc WHERE trp_id=" +
                            $"(SELECT trp_id FROM work_registration WHERE wr_id='{objId}')");
                            _type = WorkType.PaperWork;
                        }
                        if(UserHelper.GetInstance().User.UserKey.Equals(completeUser))//仅【管理员】可以提交纸本加工
                        {
                            if(CanSubmitToQT(objId, _type))
                            {
                                if(MessageBox.Show("确定要将当前批次提交到质检吗？", "提交确认", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                                {
                                    //将批次状态置为已完成
                                    object objid = dgv_WorkLog.Rows[e.RowIndex].Cells["id"].Value;
                                    //将专项添加到待质检
                                    object impid = SqlHelper.ExecuteOnlyOneQuery($"SELECT imp_id FROM imp_info WHERE imp_obj_id='{objid}'");
                                    if(impid != null)
                                        SqlHelper.ExecuteNonQuery($"INSERT INTO work_myreg(wm_id, wr_id, wm_status, wm_user, wm_type, wm_obj_id, wm_ticker) VALUES " +
                                            $"('{Guid.NewGuid().ToString()}', '{objId}', '{(int)QualityStatus.NonQuality}', '{UserHelper.GetInstance().User.UserKey}', 0, '{impid}', 0)");
                                    else
                                    {
                                        impid = SqlHelper.ExecuteOnlyOneQuery($"SELECT pi_id FROM project_info WHERE pi_obj_id='{objid}'");
                                        if(impid != null)
                                            SqlHelper.ExecuteNonQuery($"INSERT INTO work_myreg(wm_id, wr_id, wm_status, wm_user, wm_type, wm_obj_id, wm_ticker) VALUES " +
                                                $"('{Guid.NewGuid().ToString()}', '{objId}', '{(int)QualityStatus.NonQuality}', '{UserHelper.GetInstance().User.UserKey}', -1, '{impid}', 0)");
                                    }
                                    //专项信息
                                    object devid = SqlHelper.ExecuteOnlyOneQuery($"SELECT imp_id FROM imp_dev_info WHERE imp_obj_id='{impid}'");
                                    if(devid != null)
                                        SqlHelper.ExecuteNonQuery($"INSERT INTO work_myreg(wm_id, wr_id, wm_status, wm_user, wm_type, wm_obj_id, wm_ticker) VALUES " +
                                            $"('{Guid.NewGuid().ToString()}', '{objId}', '{(int)QualityStatus.NonQuality}', '{UserHelper.GetInstance().User.UserKey}', 1, '{devid}', 0)");
                                    else
                                        devid = impid;
                                    //项目/课题
                                    List<object[]> list = SqlHelper.ExecuteColumnsQuery($"SELECT pi_id FROM project_info WHERE pi_obj_id='{devid}'", 1);
                                    for(int i = 0; i < list.Count; i++)
                                        SqlHelper.ExecuteNonQuery($"INSERT INTO work_myreg(wm_id, wr_id, wm_status, wm_user, wm_type, wm_obj_id, wm_ticker) VALUES " +
                                            $"('{Guid.NewGuid().ToString()}', '{objId}', '{(int)QualityStatus.NonQuality}', '{UserHelper.GetInstance().User.UserKey}', 2, '{list[i][0]}', 0)");
                                    SqlHelper.ExecuteNonQuery($"UPDATE work_registration SET wr_submit_status ={(int)ObjectSubmitStatus.SubmitSuccess},wr_submit_date='{DateTime.Now}' WHERE wr_id='{objId}'");

                                    MessageBox.Show("提交成功。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                                    LoadWorkList(null, WorkStatus.NonWork);
                                }
                            }
                            else
                            {
                                MessageBox.Show("当前项目/课题下尚有未提交的数据。", "提交失败", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                            }
                        }
                        else
                            MessageBox.Show("此操作不被允许！", "提交失败", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                    else if(workType == WorkType.CDWork)
                    {
                        if(CanSubmitToQT(objId, WorkType.CDWork))
                        {
                            if(MessageBox.Show("确定要将当前行数据提交到质检吗？", "提交确认", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                            {
                                object objid = dgv_WorkLog.Rows[e.RowIndex].Cells["id"].Value;
                                //将专项添加到待质检
                                object impid = SqlHelper.ExecuteOnlyOneQuery($"SELECT imp_id FROM imp_info WHERE imp_obj_id='{objid}'");
                                if(impid != null)
                                    SqlHelper.ExecuteNonQuery($"INSERT INTO work_myreg(wm_id, wr_id, wm_status, wm_user, wm_type, wm_obj_id) VALUES " +
                                        $"('{Guid.NewGuid().ToString()}', '{objId}', '{(int)QualityStatus.NonQuality}', '{UserHelper.GetInstance().User.UserKey}', 0, '{impid}')");
                                else
                                {
                                    impid = SqlHelper.ExecuteOnlyOneQuery($"SELECT pi_id FROM project_info WHERE pi_obj_id='{objid}'");
                                    if(impid != null)
                                        SqlHelper.ExecuteNonQuery($"INSERT INTO work_myreg(wm_id, wr_id, wm_status, wm_user, wm_type, wm_obj_id) VALUES " +
                                            $"('{Guid.NewGuid().ToString()}', '{objId}', '{(int)QualityStatus.NonQuality}', '{UserHelper.GetInstance().User.UserKey}', -1, '{impid}')");
                                }
                                //专项信息
                                object devid = SqlHelper.ExecuteOnlyOneQuery($"SELECT imp_id FROM imp_dev_info WHERE imp_obj_id='{impid}'");
                                if(devid != null)
                                    SqlHelper.ExecuteNonQuery($"INSERT INTO work_myreg(wm_id, wr_id, wm_status, wm_user, wm_type, wm_obj_id) VALUES " +
                                        $"('{Guid.NewGuid().ToString()}', '{objId}', '{(int)QualityStatus.NonQuality}', '{UserHelper.GetInstance().User.UserKey}', 1, '{devid}')");
                                //项目/课题
                                List<object[]> list = SqlHelper.ExecuteColumnsQuery($"SELECT pi_id FROM project_info WHERE pi_obj_id='{devid}'", 1);
                                for(int i = 0; i < list.Count; i++)
                                    SqlHelper.ExecuteNonQuery($"INSERT INTO work_myreg(wm_id, wr_id, wm_status, wm_user, wm_type, wm_obj_id) VALUES " +
                                        $"('{Guid.NewGuid().ToString()}', '{objId}', '{(int)QualityStatus.NonQuality}', '{UserHelper.GetInstance().User.UserKey}', 2, '{list[i][0]}')");
                                
                                SqlHelper.ExecuteNonQuery($"UPDATE work_registration SET wr_submit_status ={(int)ObjectSubmitStatus.SubmitSuccess},wr_submit_date='{DateTime.Now}' WHERE wr_id='{objId}'");

                                LoadWorkList(null, WorkStatus.NonWork);
                            }
                        }
                        else
                            MessageBox.Show("当前数据尚未加工完成。", "提交失败", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                }
                //返工 - 编辑
                else if("bk_edit".Equals(columnName))
                {
                    int type = (int)dgv_WorkLog.Rows[e.RowIndex].Tag;
                    if(type == 0)
                    {
                        object impId = dgv_WorkLog.Rows[e.RowIndex].Cells["bk_id"].Value;
                        object trpId = dgv_WorkLog.Rows[e.RowIndex].Cells["bk_id"].Tag;
                        Frm_MyWork frm = new Frm_MyWork(WorkType.Default, impId, trpId, ControlType.Imp, true);
                        frm.ShowDialog();
                    }
                    else if(type == -1)
                    {
                        object piId = dgv_WorkLog.Rows[e.RowIndex].Cells["bk_id"].Value;
                        object trpId = dgv_WorkLog.Rows[e.RowIndex].Cells["bk_id"].Tag;
                        Frm_MyWork frm = new Frm_MyWork(WorkType.Default, piId, trpId, ControlType.Imp_Normal, true);
                        frm.ShowDialog();
                    }
                    else if(type == 1)
                    {
                        object impId = dgv_WorkLog.Rows[e.RowIndex].Cells["bk_id"].Value;
                        object trpId = dgv_WorkLog.Rows[e.RowIndex].Cells["bk_id"].Tag;
                        Frm_MyWork frm = new Frm_MyWork(WorkType.Default, impId, trpId, ControlType.Imp_Sub, true);
                        frm.ShowDialog();
                    }
                    else if(type == 2)
                    {
                        object piId = dgv_WorkLog.Rows[e.RowIndex].Cells["bk_id"].Value;
                        string querySql = "SELECT trc.trc_id FROM transfer_registraion_cd trc " +
                            "LEFT JOIN imp_info ii ON ii.imp_obj_id = trc.trc_id " +
                            "LEFT JOIN imp_dev_info idi ON idi.imp_obj_id = ii.imp_id " +
                            "LEFT JOIN project_info pi ON pi.pi_obj_id = idi.imp_id " +
                            $"WHERE pi.pi_id = '{piId}'";
                        object trcId = SqlHelper.ExecuteOnlyOneQuery(querySql);
                        object trpId = dgv_WorkLog.Rows[e.RowIndex].Cells["bk_id"].Tag;
                        Frm_MyWork frm = new Frm_MyWork(WorkType.Default, piId, trcId, ControlType.Plan_Project, true);
                        frm.ShowDialog();
                    }
                }
                //返工 - 提交
                else if("bk_submit".Equals(columnName))
                {
                    int type = (int)dgv_WorkLog.Rows[e.RowIndex].Tag;
                    if(type == 0)
                    {
                        object impId = dgv_WorkLog.Rows[e.RowIndex].Cells["bk_id"].Value;
                        object status = SqlHelper.ExecuteOnlyOneQuery($"SELECT imp_submit_status FROM imp_info WHERE imp_id='{impId}'");
                        if(status.Equals(2))
                        {
                            if(MessageBox.Show("确定要将当前数据提交至质检吗？", "确认提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk) == DialogResult.OK)
                            {
                                object wmid = dgv_WorkLog.Rows[e.RowIndex].Cells["bk_code"].Tag;//返工表主键
                                SqlHelper.ExecuteNonQuery($"UPDATE work_myreg SET wm_status='{(int)QualityStatus.NonQuality}', wm_user='{UserHelper.GetInstance().User.UserKey}' WHERE wm_id='{wmid}'");
                                InitialLeftMenu();
                                LoadWorkBackList();
                            }
                        }
                        else
                            MessageBox.Show("当前计划尚未加工完成！", "提交失败", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                    else if(type == -1)
                    {
                        object piId = dgv_WorkLog.Rows[e.RowIndex].Cells["bk_id"].Value;
                        object status = SqlHelper.ExecuteOnlyOneQuery($"SELECT pi_submit_status FROM project_info WHERE pi_id='{piId}'");
                        if(status.Equals(2))
                        {
                            if(MessageBox.Show("确定要将当前数据提交至质检吗？", "确认提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk) == DialogResult.OK)
                            {
                                object wmid = dgv_WorkLog.Rows[e.RowIndex].Cells["bk_code"].Tag;//返工表主键
                                SqlHelper.ExecuteNonQuery($"UPDATE work_myreg SET wm_status='{(int)QualityStatus.NonQuality}', wm_user='{UserHelper.GetInstance().User.UserKey}' WHERE wm_id='{wmid}'");

                                InitialLeftMenu();
                                LoadWorkBackList();
                            }
                        }
                        else
                            MessageBox.Show("当前计划尚未加工完成！", "提交失败", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                    else if(type == 1)
                    {
                        object impId = dgv_WorkLog.Rows[e.RowIndex].Cells["bk_id"].Value;
                        object status = SqlHelper.ExecuteOnlyOneQuery($"SELECT imp_submit_status FROM imp_dev_info WHERE imp_id='{impId}'");
                        if(status.Equals(2))
                        {
                            if(MessageBox.Show("确定要将当前数据提交至质检吗？", "确认提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk) == DialogResult.OK)
                            {
                                object wmid = dgv_WorkLog.Rows[e.RowIndex].Cells["bk_code"].Tag;//返工表主键
                                SqlHelper.ExecuteNonQuery($"UPDATE work_myreg SET wm_status='{(int)QualityStatus.NonQuality}', wm_user='{UserHelper.GetInstance().User.UserKey}' WHERE wm_id='{wmid}'");
                                InitialLeftMenu();
                                LoadWorkBackList();
                            }
                        }
                        else
                            MessageBox.Show("当前计划尚未加工完成！", "提交失败", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                    else if(type == 2)
                    {
                        object piId = dgv_WorkLog.Rows[e.RowIndex].Cells["bk_id"].Value;
                        int count = SqlHelper.ExecuteCountQuery($"SELECT COUNT(pi.pi_id) FROM project_info pi LEFT JOIN subject_info si ON pi.pi_id = si.pi_id LEFT JOIN subject_info si2 ON si.si_id = si2.pi_id " +
                            $"WHERE pi.pi_id = '{piId}' AND (pi_submit_status = 1 OR si.si_submit_status = 1 OR si2.si_submit_status = 1)");
                        if(count == 0)
                        {
                            if(MessageBox.Show("确定要将当前数据提交至质检吗？", "确认提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk) == DialogResult.OK)
                            {
                                object wmid = dgv_WorkLog.Rows[e.RowIndex].Cells["bk_code"].Tag;//返工表主键
                                SqlHelper.ExecuteNonQuery($"UPDATE work_myreg SET wm_status='{(int)QualityStatus.NonQuality}', wm_user='{UserHelper.GetInstance().User.UserKey}' WHERE wm_id='{wmid}'");
                                InitialLeftMenu();
                                LoadWorkBackList();
                            }
                        }
                        else
                            MessageBox.Show("当前计划尚未加工完成！", "提交失败", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                }
            }
        }
     
        /// <summary>
        /// 获取返工质检次数
        /// </summary>
        /// <param name="wrid">返工表主键</param>
        private int GetBkCountById(ControlType type, object wrid)
        {
            string querySql = "SELECT wr_qtcount FROM work_registration wrn " +
                "LEFT JOIN work_myreg wm ON wm.wr_id = wrn.wr_id " +
                "LEFT JOIN work_rework wrk ON wrk.wm_id = wm.wm_id " +
                $"WHERE wrk.wr_id='{wrid}'";
            return Convert.ToInt32(SqlHelper.ExecuteOnlyOneQuery(querySql));
        }
       
        /// <summary>
        /// 根据指定ID查看是否其所属项目/课题是否全部提交
        /// </summary>
        /// <param name="objId">Work_Reg登记表主键</param>
        private bool CanSubmitToQT(object objId, WorkType workType)
        {
            if(workType == WorkType.Default)
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
                object[] imp = SqlHelper.ExecuteRowsQuery($"SELECT imp_id, imp_submit_status FROM imp_info WHERE imp_obj_id=(SELECT wr_obj_id FROM work_registration WHERE wr_id='{objId}')");
                if(imp == null || imp.Length == 0)
                    imp = SqlHelper.ExecuteRowsQuery($"SELECT pi_id, pi_submit_status FROM project_info WHERE pi_obj_id=(SELECT wr_obj_id FROM work_registration WHERE wr_id='{objId}')");
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
                        if(rootStatu == null || (Convert.ToInt32(rootStatu) == (int)ObjectSubmitStatus.NonSubmit))
                            return false;
                        _obj2 = SqlHelper.ExecuteColumnsQuery($"SELECT pi_id, pi_submit_status FROM project_info WHERE pi_obj_id='{rootId}'", 2);
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
                    else
                    {
                        if(type == WorkType.SubjectWork)
                            _obj2 = SqlHelper.ExecuteColumnsQuery($"SELECT pi_id, pi_submit_status FROM project_info WHERE pi_obj_id='{rootId}'", 2);
                        else
                            _obj2 = SqlHelper.ExecuteColumnsQuery($"SELECT pi_id, pi_submit_status FROM project_info WHERE pi_obj_id='{rootId}' AND pi_worker_id='{UserHelper.GetInstance().User.UserKey}'", 2);
                        for(int i = 0; i < _obj2.Count; i++)
                        {
                            if(Convert.ToInt32(_obj2[i][1]) == (int)ObjectSubmitStatus.NonSubmit)
                                return false;
                            List<object[]> _obj3 = SqlHelper.ExecuteColumnsQuery($"SELECT si_id,si_submit_status FROM subject_info WHERE pi_id='{_obj2[i][0]}' AND si_worker_id='{UserHelper.GetInstance().User.UserKey}'", 2);
                            for(int j = 0; j < _obj3.Count; j++)
                            {
                                if(Convert.ToInt32(_obj3[j][1]) == (int)ObjectSubmitStatus.NonSubmit)
                                    return false;
                                List<object[]> _obj4 = SqlHelper.ExecuteColumnsQuery($"SELECT si_id,si_submit_status FROM subject_info WHERE pi_id='{_obj3[j][0]}' AND si_worker_id='{UserHelper.GetInstance().User.UserKey}'", 2);
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
                return SqlHelper.ExecuteOnlyOneQuery($"SELECT pi_id FROM project_info WHERE pi_obj_id='{objId}'");
            }
            if(type == WorkType.ProjectWork)
            {
                return SqlHelper.ExecuteOnlyOneQuery($"SELECT pi_obj_id FROM project_info WHERE pi_id='{objId}'");
            }
            else if(type == WorkType.SubjectWork)
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
            int amount = Convert.ToInt32(SqlHelper.ExecuteOnlyOneQuery($"SELECT COUNT(wr_id) FROM work_registration WHERE wr_status={(int)WorkStatus.WorkSuccess} AND wr_submit_status ={(int)ObjectSubmitStatus.NonSubmit} AND wr_source_id='{UserHelper.GetInstance().User.UserKey}'"));
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
        private void LoadSubjectList(object pid)
        {
            DataGridViewStyleHelper.ResetDataGridView(dgv_WorkLog);

            dgv_WorkLog.Columns.Add("si_id", "主键");
            dgv_WorkLog.Columns.Add("si_code", "课题/子课题编号");
            dgv_WorkLog.Columns.Add("si_name", "课题/子课题名称");
            dgv_WorkLog.Columns.Add("si_file_amount", "文件数");
            dgv_WorkLog.Columns.Add("si_control", "操作");

            DataTable table = null;
            StringBuilder querySql = new StringBuilder("SELECT si_id,si_code,si_name FROM subject_info si");
            querySql.Append($" WHERE pi_id='{pid}' AND si_work_status={(int)WorkStatus.NonWork} ORDER BY si_code");
            table = SqlHelper.ExecuteQuery(querySql.ToString());
            foreach (DataRow row in table.Rows)
            {
                int _index = dgv_WorkLog.Rows.Add();
                dgv_WorkLog.Rows[_index].Cells["si_id"].Value = row["si_id"];
                dgv_WorkLog.Rows[_index].Cells["si_code"].Value = row["si_code"];
                dgv_WorkLog.Rows[_index].Cells["si_name"].Value = row["si_name"];
                dgv_WorkLog.Rows[_index].Cells["si_file_amount"].Value = 0;
                dgv_WorkLog.Rows[_index].Cells["si_control"].Value = "加工";
            }
            if (dgv_WorkLog.Columns.Count > 0)
                dgv_WorkLog.Columns[0].Visible = false;

            List<KeyValuePair<string, int>> list = new List<KeyValuePair<string, int>>();
            list.AddRange(new KeyValuePair<string, int>[]
            {
                new KeyValuePair<string, int>("si_file_amount",90),
                new KeyValuePair<string, int>("si_control",100),
            });
            DataGridViewStyleHelper.SetWidth(dgv_WorkLog, list);
            DataGridViewStyleHelper.SetLinkStyle(dgv_WorkLog, new string[] {"si_control" }, true);
            DataGridViewStyleHelper.SetAlignWithCenter(dgv_WorkLog, new string[] { "si_file_amount" });

            dgv_WorkLog.Columns["si_id"].Visible = false;

            LastIdLog = new string[] { LastIdLog[1], $"Subject_{pid}" };
        }
        
        /// <summary>
        /// 光盘列表
        /// </summary>
        /// <param name="trpId">批次主键</param>
        private void LoadCDList(object trpId)
        {
            DataGridViewStyleHelper.ResetDataGridView(dgv_WorkLog);
            dgv_WorkLog.Columns.AddRange(new DataGridViewColumn[]
            {
                new DataGridViewTextBoxColumn(){ Name = "trc_id"},
                new DataGridViewTextBoxColumn(){ Name = "dd_name", FillWeight = 15 },
                new DataGridViewTextBoxColumn(){ Name = "trc_code", FillWeight = 20 },
                new DataGridViewTextBoxColumn(){ Name = "trc_name", FillWeight = 20 },
                new DataGridViewTextBoxColumn(){ Name = "trc_total_amount", FillWeight = 10 },
                new DataGridViewTextBoxColumn(){ Name = "trc_receive_amount", FillWeight = 10 },
                new DataGridViewTextBoxColumn(){ Name = "trc_file_amount", FillWeight = 10 },
                new DataGridViewButtonColumn(){ Name = "trc_control", FillWeight = 7, Text = "加工", UseColumnTextForButtonValue = true },
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
            DataGridViewStyleHelper.SetTreeViewHeader(dgv_WorkLog, tv);

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
                int _index = dgv_WorkLog.Rows.Add();
                dgv_WorkLog.Rows[_index].Cells["trc_id"].Value = row["trc_id"];
                dgv_WorkLog.Rows[_index].Cells["dd_name"].Value = row["dd_name"];
                dgv_WorkLog.Rows[_index].Cells["trc_code"].Value = row["trc_code"];
                dgv_WorkLog.Rows[_index].Cells["trc_name"].Value = row["trc_name"];
                dgv_WorkLog.Rows[_index].Cells["trc_total_amount"].Value = totalAmount;
                dgv_WorkLog.Rows[_index].Cells["trc_receive_amount"].Value = receiveAmount;
                dgv_WorkLog.Rows[_index].Cells["trc_file_amount"].Value = GetFileAmount(row["trc_id"]);
            }

            DataGridViewStyleHelper.SetLinkStyle(dgv_WorkLog, new string[] { "trc_total_amount"}, true);
            DataGridViewStyleHelper.SetAlignWithCenter(dgv_WorkLog, new string[] { "trc_receive_amount", "trc_file_amount", "trc_total_amount" });

            dgv_WorkLog.Columns["trc_id"].Visible = false;
            LastIdLog = new string[] { LastIdLog[1], $"CD_{trpId}" };
        }
        
        /// <summary>
        /// 根据光盘ID获取文件数
        /// </summary>
        /// <param name="cdid">光盘ID</param>
        private object GetFileAmount(object cdid) => SqlHelper.ExecuteOnlyOneQuery($"SELECT COUNT(bfi_id) FROM backup_files_info WHERE bfi_type=0 AND bfi_trcid='{cdid}'");
        
        /// <summary>
        /// 根据光盘ID获取已领取项目总数
        /// </summary>
        private int GetReceiveAmount(object trcId)
        {
            int proAmount = 0;
            if (trcId != null)
            {
                proAmount = Convert.ToInt32(SqlHelper.ExecuteOnlyOneQuery($"SELECT COUNT(*) FROM project_info WHERE pi_obj_id=" +
                    $"(SELECT pi_id FROM project_info WHERE trc_id='{trcId}') " +
                    $"AND pi_work_status={(int)WorkStatus.WorkSuccess}"));
            }
            return proAmount;
        }
        /// <summary>
        /// 根据光盘ID获取项目总数
        /// </summary>
        private int GetProjectAmount(object trcId)
        {
            int proAmount = 0;
            if (trcId != null)
            {
                proAmount = Convert.ToInt32(SqlHelper.ExecuteOnlyOneQuery($"SELECT COUNT(*) FROM project_info WHERE pi_obj_id=" +
                    $"(SELECT pi_id FROM project_info WHERE trc_id='{trcId}')"));
            }
            return proAmount;
        }
        /// <summary>
        /// 加载光盘下的项目/课题列表
        /// </summary>
        /// <param name="trcId">光盘ID</param>
        private void LoadProjectList(object trcId)
        {
            DataGridViewStyleHelper.ResetDataGridView(dgv_WorkLog);

            dgv_WorkLog.Columns.Add("pi_id", "主键");
            dgv_WorkLog.Columns.Add("pi_code", "项目/课题编号");
            dgv_WorkLog.Columns.Add("pi_name", "项目/课题名称");
            dgv_WorkLog.Columns.Add("pi_company", "承担单位");
            dgv_WorkLog.Columns.Add("pi_total_amount", "总数");
            dgv_WorkLog.Columns.Add("pi_receive_amount", "已领取数");
            dgv_WorkLog.Columns.Add("pi_file_amount", "文件数");
            dgv_WorkLog.Columns.Add("pi_control", "操作");

            TreeView tree = new TreeView();
            tree.Nodes.AddRange(new TreeNode[] {
                new TreeNode("主键"),
                new TreeNode("项目/课题编号"),
                new TreeNode( "项目/课题名称"),
                new TreeNode("承担单位"),
                new TreeNode("课题/子课题"),
                new TreeNode("文件数"),
            });
            tree.Nodes[4].Nodes.AddRange(new TreeNode[]
            {
                new TreeNode("总数"),
                new TreeNode("已领取数"),
            });
            DataGridViewStyleHelper.SetTreeViewHeader(dgv_WorkLog, tree);

            DataTable table = null;
            StringBuilder querySql = new StringBuilder("SELECT pi_id,pi_code,pi_name,pi_company_id,pi_work_status FROM project_info pi");
            querySql.Append($" WHERE pi_obj_id=(SELECT pi_id FROM project_info WHERE trc_id='{trcId}') ORDER BY pi_code");
            table = SqlHelper.ExecuteQuery(querySql.ToString());
            foreach (DataRow row in table.Rows)
            {
                int totalAmount = GetSubjectAmount(row["pi_id"]);
                int receiveAmount = GetSubjectReceiveAmount(row["pi_id"]);
                if(totalAmount == 0 || totalAmount != receiveAmount)
                {
                    int _index = dgv_WorkLog.Rows.Add();
                    dgv_WorkLog.Rows[_index].Cells["pi_id"].Value = row["pi_id"];
                    dgv_WorkLog.Rows[_index].Cells["pi_code"].Value = row["pi_code"];
                    dgv_WorkLog.Rows[_index].Cells["pi_name"].Value = row["pi_name"];
                    dgv_WorkLog.Rows[_index].Cells["pi_company"].Value = SqlHelper.GetCompanysNameById(row["pi_company_id"]);
                    dgv_WorkLog.Rows[_index].Cells["pi_total_amount"].Value = totalAmount;
                    dgv_WorkLog.Rows[_index].Cells["pi_receive_amount"].Value = receiveAmount;
                    dgv_WorkLog.Rows[_index].Cells["pi_file_amount"].Value = 0;//文件数待处理
                    dgv_WorkLog.Rows[_index].Cells["pi_control"].Value = GetWorkValue(row["pi_work_status"]);
                }
            }
            if (dgv_WorkLog.Columns.Count > 0)
                dgv_WorkLog.Columns[0].Visible = false;

            List<KeyValuePair<string, int>> list = new List<KeyValuePair<string, int>>();
            list.Add(new KeyValuePair<string, int>("pi_code", 100));
            list.Add(new KeyValuePair<string, int>("pi_name", 250));
            list.Add(new KeyValuePair<string, int>("pi_company", 200));

            list.Add(new KeyValuePair<string, int>("pi_total_amount", 90));
            list.Add(new KeyValuePair<string, int>("pi_receive_amount", 90));
            list.Add(new KeyValuePair<string, int>("pi_file_amount", 90));
            list.Add(new KeyValuePair<string, int>("pi_control", 100));
            DataGridViewStyleHelper.SetWidth(dgv_WorkLog, list);

            DataGridViewStyleHelper.SetLinkStyle(dgv_WorkLog, new string[] { "pi_total_amount", "pi_control" }, true);
            DataGridViewStyleHelper.SetAlignWithCenter(dgv_WorkLog, new string[] { "pi_receive_amount", "pi_file_amount" });

            dgv_WorkLog.Columns["pi_id"].Visible = false;

            LastIdLog = new string[] { LastIdLog[1], $"Project_{trcId}" };
        }
        /// <summary>
        /// 获取加工结果
        /// </summary>
        private object GetWorkValue(object index) => (WorkStatus)Convert.ToInt32(index) == WorkStatus.NonWork ? "加工" : "已加工";
        /// <summary>
        ///  根据父级ID获取子级已领取列表
        /// </summary>
        private int GetSubjectReceiveAmount(object pid)
        {
            int subAmount = 0;
            if (pid != null)
            {
                subAmount = Convert.ToInt32(SqlHelper.ExecuteOnlyOneQuery($"SELECT COUNT(*) FROM subject_info WHERE pi_id='{pid}' AND si_work_status='{(int)WorkStatus.WorkSuccess}'"));
            }
            return subAmount;
        }
        /// <summary>
        /// 根据项目id获取课题数
        /// </summary>
        private int GetSubjectAmount(object pid)
        {
            int subAmount = 0;
            if (pid != null)
            {
                subAmount = Convert.ToInt32(SqlHelper.ExecuteOnlyOneQuery($"SELECT COUNT(*) FROM subject_info WHERE pi_id='{pid}'"));
            }
            return subAmount;
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
                if ("WORK_LIST".Equals(dgv_WorkLog.Tag))
                    LoadWorkList(null, WorkStatus.WorkSuccess);
                else
                    LoadPCList(null, null);
            }
            else
            {
                if ("WORK_LIST".Equals(dgv_WorkLog.Tag))
                    LoadWorkList(csid, WorkStatus.WorkSuccess);
                else
                    LoadPCList(null, csid);
            }
        }
    }
}
