using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Windows.Forms;
using 科技计划项目档案数据采集管理系统.Tools;

namespace 科技计划项目档案数据采集管理系统
{
    public partial class Frm_QT : Form
    {
        /// <summary>
        ///  每页页数
        /// </summary>
        private int PAGE_SIZE = 30;
        /// <summary>
        /// 当前页码（默认0）
        /// </summary>
        private int CURRENT_PAGE = 0;
        /// <summary>
        /// 最大页码
        /// </summary>
        private int MAX_PAGE = 0;
        public Frm_QT()
        {
            InitializeComponent();
            InitialForm();
        }
        /// <summary>
        /// 初始化我的质检列表
        /// </summary>
        private void InitialForm()
        {
            //dgv_MyReg -- 位于Designer中。
            dgv_MyReg.AllowUserToAddRows = false;
            dgv_MyReg.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
        }

        private void Frm_QT_Load(object sender, EventArgs e)
        {
            dgv_Imp.ColumnHeadersDefaultCellStyle = DataGridViewStyleHelper.GetHeaderStyle();
            dgv_Imp_Dev.ColumnHeadersDefaultCellStyle = DataGridViewStyleHelper.GetHeaderStyle();
            dgv_Project.ColumnHeadersDefaultCellStyle = DataGridViewStyleHelper.GetHeaderStyle();

            LoadImpList();
            ace_LeftMenu.SelectedElement = ace_Login;

            searchControl.Visible = false;
        }

        /// <summary>
        /// 二级菜单点击事件
        /// </summary>
        private void Sub_Click(object sender, EventArgs e)
        {
            string name = (sender as DevExpress.XtraBars.Navigation.AccordionControlElement).Name;
            if("ace_Login".Equals(name))
            {
                dgv_MyReg.Visible = false;
                tab_Menulist.Visible = true;
                tab_Menulist.SelectedTabPageIndex = 0;
                if(tab_Menulist.SelectedTabPageIndex == 2)
                {
                    searchControl.Visible = true;
                    pal_Page.Visible = true;
                    btn_Search.Visible = true;
                }
            }
            else if("ace_MyLog".Equals(name))
            {
                tab_Menulist.Visible = false;
                dgv_MyReg.Visible = true;
                dgv_MyReg.DefaultCellStyle = dgv_Project.DefaultCellStyle;
                dgv_MyReg.ColumnHeadersDefaultCellStyle = DataGridViewStyleHelper.GetHeaderStyle();
                LoadMyRegList();
                pal_Page.Visible = false;
                searchControl.Visible = false;
                searchControl.ResetText();
                btn_Search.Visible = false;
            }
            else if("ace_MyQT".Equals(name))
            {
                tab_Menulist.Visible = false;
                dgv_MyReg.Visible = true;
                dgv_MyReg.DefaultCellStyle = dgv_Project.DefaultCellStyle;
                dgv_MyReg.ColumnHeadersDefaultCellStyle = DataGridViewStyleHelper.GetHeaderStyle();
                LoadMyRegedList();
                searchControl.Visible = false;
                pal_Page.Visible = false;
                searchControl.ResetText();
                btn_Search.Visible = false;
            }
        }

        /// <summary>
        /// 我的质检
        /// </summary>
        private void LoadMyRegedList()
        {
            DataGridViewStyleHelper.ResetDataGridView(dgv_MyReg, true);
            dgv_MyReg.Columns.AddRange(new DataGridViewColumn[]
            {
                new DataGridViewTextBoxColumn(){ Name = "mrl_id", Visible = false},
                new DataGridViewTextBoxColumn(){ Name = "mrl_unit", HeaderText = "来源单位", FillWeight = 15, SortMode = DataGridViewColumnSortMode.NotSortable},
                new DataGridViewTextBoxColumn(){ Name = "mrl_pcode", HeaderText = "批次号", FillWeight = 15, SortMode = DataGridViewColumnSortMode.NotSortable},
                new DataGridViewTextBoxColumn(){ Name = "mrl_code", HeaderText = "编号", FillWeight = 15, SortMode = DataGridViewColumnSortMode.NotSortable},
                new DataGridViewTextBoxColumn(){ Name = "mrl_name", HeaderText = "名称", FillWeight = 20, SortMode = DataGridViewColumnSortMode.NotSortable},
                new DataGridViewTextBoxColumn(){ Name = "mrl_fileamount", HeaderText = "文件数", FillWeight = 5, SortMode = DataGridViewColumnSortMode.NotSortable},
                new DataGridViewTextBoxColumn(){ Name = "mrl_state", HeaderText = "状态", FillWeight = 8, SortMode = DataGridViewColumnSortMode.NotSortable},
                new DataGridViewButtonColumn(){ Name = "mrl_edit", HeaderText = "操作", FillWeight = 5, Text = "查看", UseColumnTextForButtonValue = true, SortMode = DataGridViewColumnSortMode.NotSortable},
            });
            DataTable table = SqlHelper.ExecuteQuery($"SELECT wm_id, wm_obj_id FROM work_myreg WHERE wm_accepter='{UserHelper.GetUser().UserKey}' AND wm_type=3 AND wm_status<>1");
            foreach(DataRow row in table.Rows)
            {
                object objId = row["wm_obj_id"];
                string querySql = "SELECT   TOP (1)  dd_name, dd.dd_code, wm.wm_id, A.pi_id, A.pi_code, A.pi_name, wr.wr_obj_id, trp.trp_code, trc.trc_id, wm.wm_status " +
                    "FROM(SELECT * FROM project_info UNION ALL SELECT * FROM topic_info) A " +
                    "LEFT OUTER JOIN work_myreg AS wm ON wm.wm_obj_id = A.pi_id " +
                    "LEFT OUTER JOIN work_registration AS wr ON wr.wr_id = wm.wr_id " +
                    "LEFT OUTER JOIN transfer_registration_pc AS trp ON wr.trp_id = trp.trp_id " +
                    "LEFT OUTER JOIN transfer_registraion_cd AS trc ON trp.trp_id = trc.trp_id " +
                    "LEFT OUTER JOIN data_dictionary AS dd ON dd.dd_id = trp.com_id " +
                   $"WHERE(wm.wm_obj_id = '{objId}') AND wm_status<>{(int)QualityStatus.Qualitting}";
                DataRow dataRow = SqlHelper.ExecuteSingleRowQuery(querySql);
                if(dataRow != null)
                {
                    int rowIndex = dgv_MyReg.Rows.Add();
                    dgv_MyReg.Rows[rowIndex].Tag = row["wm_id"];
                    dgv_MyReg.Rows[rowIndex].Cells["mrl_id"].Value = dataRow["pi_id"];
                    dgv_MyReg.Rows[rowIndex].Cells["mrl_name"].Value = dataRow["pi_name"];
                    dgv_MyReg.Rows[rowIndex].Cells["mrl_pcode"].Value = dataRow["trp_code"];
                    dgv_MyReg.Rows[rowIndex].Cells["mrl_pcode"].Tag = dataRow["trc_id"];
                    dgv_MyReg.Rows[rowIndex].Cells["mrl_code"].Value = dataRow["pi_code"];
                    dgv_MyReg.Rows[rowIndex].Cells["mrl_code"].Tag = dataRow["wr_obj_id"];
                    dgv_MyReg.Rows[rowIndex].Cells["mrl_unit"].Value = dataRow["dd_name"];
                    dgv_MyReg.Rows[rowIndex].Cells["mrl_unit"].Tag = dataRow["dd_code"];
                    dgv_MyReg.Rows[rowIndex].Cells["mrl_state"].Value = GetMyWorkState(dataRow["wm_status"]);
                    dgv_MyReg.Rows[rowIndex].Cells["mrl_fileamount"].Value = GetFileAmountById(dataRow["pi_id"]);
                }
            }
            dgv_MyReg.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            dgv_MyReg.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private string GetMyWorkState(object state)
        {
            if(state != null)
            {
                QualityStatus status = (QualityStatus)(int)state;
                if(status == QualityStatus.Qualitting)
                    return "质检中";
                else if(status == QualityStatus.QualityFinish)
                    return "质检通过";
                else if(status == QualityStatus.QualityBack)
                    return "返工中";
            }
            return string.Empty;
        }

        /// <summary>
        /// 加载待质检列表
        /// </summary>
        private void LoadWaitQTList()
        {
            DataGridViewStyleHelper.ResetDataGridView(dgv_Imp, true);
            DataGridViewStyleHelper.ResetDataGridView(dgv_Imp_Dev, true);

            dgv_Imp.Columns.Add("plan_id", "主键");
            dgv_Imp.Columns.Add("plan_code", "编号");
            dgv_Imp.Columns.Add("plan_name", "名称");
            dgv_Imp.Columns.Add("plan_unit", "来源单位");
            dgv_Imp.Columns.Add("plan_qtcount", "质检次数");
            dgv_Imp.Columns.Add("plan_edit", "操作");

            dgv_Imp_Dev.Columns.Add("project_id", "主键");
            dgv_Imp_Dev.Columns.Add("project_unit", "来源单位");
            dgv_Imp_Dev.Columns.Add("project_code", "项目/课题编号");
            dgv_Imp_Dev.Columns.Add("project_name", "项目/课题名称");
            dgv_Imp_Dev.Columns.Add("project_subamount", "课题/子课题数");
            dgv_Imp_Dev.Columns.Add("project_fileamount", "文件数");
            dgv_Imp_Dev.Columns.Add("project_edit", "操作");

            List<DataTable> resultList = GetObjectListById(null);
            for(int i = 0; i < resultList.Count; i++)
            {
                if(resultList[i].Rows.Count > 0)
                {
                    DataRow row = resultList[i].Rows[0];
                    WorkType type = (WorkType)Convert.ToInt32(row[1]);
                    if(type == WorkType.PaperWork || type == WorkType.CDWork)
                    {
                        int index = dgv_Imp.Rows.Add();
                        dgv_Imp.Rows[index].Cells["plan_id"].Value = row[0];
                        dgv_Imp.Rows[index].Cells["plan_qtcount"].Value = SqlHelper.ExecuteOnlyOneQuery($"SELECT wr_qtcount FROM work_registration WHERE wr_id='{row[0]}'");
                        dgv_Imp.Rows[index].Cells["plan_id"].Tag = row[2];
                        dgv_Imp.Rows[index].Cells["plan_code"].Value = row[3];
                        dgv_Imp.Rows[index].Cells["plan_name"].Value = row[4];
                        dgv_Imp.Rows[index].Cells["plan_unit"].Value = row[5];
                        dgv_Imp.Rows[index].Cells["plan_edit"].Value = "质检";
                    }
                    else if(type == WorkType.ProjectWork)
                    {
                        int index = dgv_Imp_Dev.Rows.Add();
                        dgv_Imp_Dev.Rows[index].Cells["project_id"].Value = row[0];
                        dgv_Imp_Dev.Rows[index].Cells["project_id"].Tag = row[2];
                        dgv_Imp_Dev.Rows[index].Cells["project_code"].Value = row[3];
                        dgv_Imp_Dev.Rows[index].Cells["project_name"].Value = row[4];
                        dgv_Imp_Dev.Rows[index].Cells["project_unit"].Value = row[5];
                        dgv_Imp_Dev.Rows[index].Cells["project_subamount"].Value = GetSubjectAmountByProjectId(row[0]);
                        dgv_Imp_Dev.Rows[index].Cells["project_fileamount"].Value = GetFileAmountByProjectId(row[0]);
                        dgv_Imp_Dev.Rows[index].Cells["project_edit"].Value = "质检";
                    }
                    else if(type == WorkType.TopicWork)
                    {
                        int index = dgv_Imp_Dev.Rows.Add();
                        dgv_Imp_Dev.Rows[index].Cells["project_id"].Value = row[0];
                        dgv_Imp_Dev.Rows[index].Cells["project_id"].Tag = row[2];
                        dgv_Imp_Dev.Rows[index].Cells["project_code"].Value = row[3];
                        dgv_Imp_Dev.Rows[index].Cells["project_name"].Value = row[4];
                        dgv_Imp_Dev.Rows[index].Cells["project_unit"].Value = row[5];
                        dgv_Imp_Dev.Rows[index].Cells["project_subamount"].Value = 0;
                        dgv_Imp_Dev.Rows[index].Cells["project_fileamount"].Value = 0;
                        dgv_Imp_Dev.Rows[index].Cells["project_edit"].Value = "质检";
                    }
                }
            }
            dgv_Imp.Columns["plan_id"].Visible = false;
            dgv_Imp_Dev.Columns["project_id"].Visible = false;

            DataGridViewStyleHelper.SetLinkStyle(dgv_Imp, new string[] { "plan_edit" }, false);
            DataGridViewStyleHelper.SetLinkStyle(dgv_Imp_Dev, new string[] { "project_edit" }, false);

            List<KeyValuePair<string, int>> keyValue = new List<KeyValuePair<string, int>>
            {
                new KeyValuePair<string, int>("plan_qtcount", 100),
                new KeyValuePair<string, int>("plan_name", 200),
                new KeyValuePair<string, int>("plan_edit", 100)
            };
            DataGridViewStyleHelper.SetWidth(dgv_Imp, keyValue);
            List<KeyValuePair<string, int>> _keyValue = new List<KeyValuePair<string, int>>
            {
                new KeyValuePair<string, int>("project_name", 200),
                new KeyValuePair<string, int>("project_edit", 100)
            };
            DataGridViewStyleHelper.SetWidth(dgv_Imp_Dev, _keyValue);

            DataGridViewStyleHelper.SetAlignWithCenter(dgv_Imp, new string[] { "plan_qtcount" });
        }
   
        /// <summary>
        /// 获取【项目|课题】下的文件总数
        /// </summary>
        private object GetFileAmountByProjectId(object proId)
        {
            int amount = 0;
            amount += Convert.ToInt32(SqlHelper.ExecuteOnlyOneQuery($"SELECT COUNT(pfl_id) FROM processing_file_list WHERE pfl_obj_id='{proId}'"));
            List<object[]> list = SqlHelper.ExecuteColumnsQuery($"SELECT si_id FROM subject_info WHERE pi_id='{proId}'", 1);
            for(int i = 0; i < list.Count; i++)
            {
                amount += Convert.ToInt32(SqlHelper.ExecuteOnlyOneQuery($"SELECT COUNT(pfl_id) FROM processing_file_list WHERE pfl_obj_id='{list[i][0]}'"));
                List<object[]> list2 = SqlHelper.ExecuteColumnsQuery($"SELECT si_id FROM subject_info WHERE pi_id='{list[i][0]}'", 1);
                for(int j = 0; j < list2.Count; j++)
                    amount += Convert.ToInt32(SqlHelper.ExecuteOnlyOneQuery($"SELECT COUNT(pfl_id) FROM processing_file_list WHERE pfl_obj_id='{list2[j][0]}'"));
            }
            return amount;
        }
    
        /// <summary>
        /// 获取【项目|课题】下的子课题数
        /// </summary>
        private object GetSubjectAmountByProjectId(object proId)
        {
            int amount = 0;
            List<object[]> list = SqlHelper.ExecuteColumnsQuery($"SELECT si_id FROM subject_info WHERE pi_id='{proId}'", 1);
            amount += list.Count;
            for(int i = 0; i < list.Count; i++)
                amount += Convert.ToInt32($"SELECT COUNT(si_id) FROM subject_info WHERE pi_id='{list[i][0]}'");
            return amount;
        }
    
        /// <summary>
        /// 根据加工登记主键获取对应项目/课题信息
        /// 0:wr_id 1:wr_type 2:wr_obj_id 3:code 4:name 5:dd_name
        /// </summary>
        private static List<DataTable> GetObjectListById(object objid)
        {
            string querySql = $" SELECT wr_id, wr_type,wr_obj_id FROM work_registration wr LEFT JOIN(" +
                            $"SELECT trp_id, dd_id FROM transfer_registration_pc LEFT JOIN data_dictionary ON com_id = dd_id) tb " +
                            $"ON wr.trp_id = tb.trp_id WHERE wr_submit_status={(int)ObjectSubmitStatus.SubmitSuccess}";
            if(objid != null)
                querySql += $" AND wr_id='{objid}'";
            else
                querySql += $" AND wr_receive_status={(int)ReceiveStatus.NonReceive}";
            List<object[]> list = SqlHelper.ExecuteColumnsQuery(querySql, 3);
            List<DataTable> resultList = new List<DataTable>();
            for(int i = 0; i < list.Count; i++)
            {
                WorkType type = (WorkType)list[i][1];
                string _querySql = null;
                switch(type)
                {
                    case WorkType.PaperWork:
                        _querySql = $"SELECT '{list[i][0]}','{list[i][1]}','{list[i][2]}',trp_code,trp_name,dd_name FROM transfer_registration_pc LEFT JOIN " +
                            $"data_dictionary ON com_id = dd_id WHERE trp_id='{list[i][2]}'";
                        break;
                    case WorkType.CDWork:
                        _querySql = $"SELECT '{list[i][0]}','{list[i][1]}','{list[i][2]}',trc_code,trc_name,dd_name FROM transfer_registraion_cd trc LEFT JOIN(" +
                            $"SELECT trp_id, dd_name FROM transfer_registration_pc LEFT JOIN data_dictionary ON com_id = dd_id ) tb1 " +
                            $"ON tb1.trp_id = trc.trp_id WHERE trc_id='{list[i][2]}'";
                        break;
                    case WorkType.ProjectWork:
                        _querySql = $"SELECT '{list[i][0]}','{list[i][1]}','{list[i][2]}',pi_code,pi_name,dd_name FROM project_info pi " +
                            $"LEFT JOIN(SELECT trc_id, dd_name FROM transfer_registraion_cd trc " +
                            $"LEFT JOIN(SELECT trp_id, dd_name FROM transfer_registration_pc trp " +
                            $"LEFT JOIN data_dictionary ON dd_id = trp.com_id)tb1 ON trc.trp_id = tb1.trp_id) tb2 ON tb2.trc_id = pi.trc_id " +
                            $"WHERE pi_id='{list[i][2]}'";
                        break;
                    case WorkType.TopicWork:
                        _querySql = $"SELECT '{list[i][0]}','{list[i][1]}','{list[i][2]}',si_code,si_name,dd_name FROM subject_info si LEFT JOIN(" +
                           $"SELECT pi_id,dd_name FROM project_info pi " +
                           $"LEFT JOIN(SELECT trc_id, dd_name FROM transfer_registraion_cd trc " +
                           $"LEFT JOIN(SELECT trp_id, dd_name FROM transfer_registration_pc trp " +
                           $"LEFT JOIN data_dictionary ON dd_id = trp.com_id)tb1 ON trc.trp_id = tb1.trp_id) tb2 ON tb2.trc_id = pi.trc_id " +
                           $") tb3 ON tb3.pi_id = si.pi_id WHERE si.si_id='{list[i][2]}'";
                        break;
                    default:
                        _querySql = string.Empty;
                        break;
                }
                DataTable table = SqlHelper.ExecuteQuery(_querySql);
                resultList.Add(table);
            }
            return resultList;
        }
       
        /// <summary>
        /// 获取指定项目/课题获取其所属计划ID
        /// </summary>
        private object GetRootId(object objId, WorkType type)
        {
            if(type == WorkType.PaperWork)
                return SqlHelper.ExecuteOnlyOneQuery($"SELECT pi_id FROM project_info WHERE pi_obj_id='{objId}' AND pi_source_id='{UserHelper.GetUser().UserKey}'");
            else if(type == WorkType.CDWork)
                return SqlHelper.ExecuteOnlyOneQuery($"SELECT pi_id FROM project_info WHERE trc_id='{objId}'") ?? SqlHelper.ExecuteOnlyOneQuery($"SELECT pi_id FROM project_info WHERE pi_obj_id='{objId}'");
            if(type == WorkType.ProjectWork)
                return SqlHelper.ExecuteOnlyOneQuery($"SELECT pi_obj_id FROM project_info WHERE pi_id='{objId}'");
            else if(type == WorkType.TopicWork)
            {
                object pid = SqlHelper.ExecuteOnlyOneQuery($"SELECT pi_id FROM subject_info WHERE si_id='{objId}'");
                return GetRootId(pid, WorkType.ProjectWork);
            }
            return null;
        }
        
        /// <summary>
        /// 质检中
        /// </summary>
        private void LoadMyRegList()
        {
            DataGridViewStyleHelper.ResetDataGridView(dgv_MyReg, true);
            dgv_MyReg.Columns.AddRange(new DataGridViewColumn[]
            {
                new DataGridViewTextBoxColumn(){ Name = "mr_id"},
                new DataGridViewTextBoxColumn(){ Name = "mr_unit", HeaderText = "来源单位", FillWeight = 15, SortMode = DataGridViewColumnSortMode.NotSortable},
                new DataGridViewTextBoxColumn(){ Name = "mr_pcode", HeaderText = "批次号", FillWeight = 15, SortMode = DataGridViewColumnSortMode.NotSortable},
                new DataGridViewTextBoxColumn(){ Name = "mr_code", HeaderText = "编号", FillWeight = 15, SortMode = DataGridViewColumnSortMode.NotSortable},
                new DataGridViewTextBoxColumn(){ Name = "mr_name", HeaderText = "名称", FillWeight = 20, SortMode = DataGridViewColumnSortMode.NotSortable},
                new DataGridViewTextBoxColumn(){ Name = "mr_fileamount", HeaderText = "文件数", FillWeight = 5, SortMode = DataGridViewColumnSortMode.NotSortable},
                new DataGridViewButtonColumn(){ Name = "mr_edit", HeaderText = "操作", FillWeight = 8, Text = "编辑", UseColumnTextForButtonValue = true, SortMode = DataGridViewColumnSortMode.NotSortable},
                new DataGridViewButtonColumn(){ Name = "mr_submit", HeaderText = "完成", FillWeight = 8, Text = "提交", UseColumnTextForButtonValue = true, SortMode = DataGridViewColumnSortMode.NotSortable}
            });

            DataTable table = SqlHelper.ExecuteQuery($"SELECT wm_id, wm_type, wm_obj_id FROM work_myreg WHERE wm_accepter='{UserHelper.GetUser().UserKey}' AND wm_status=2");
            foreach(DataRow  row in table.Rows)
            {
                WorkType type = (WorkType)Convert.ToInt32(row["wm_type"]);
                object objId = row["wm_obj_id"];
                if(type == WorkType.PaperWork_Plan || type == WorkType.CDWork_Plan)
                {
                    string querySql = "SELECT dd_name, dd_code, pi_code, pi_name, pi_id, trp.trp_code, trc.trc_id FROM work_myreg wm " +
                        "LEFT JOIN project_info pi ON pi.pi_id = wm.wm_obj_id " +
                        "LEFT JOIN work_registration wr ON wr.wr_id = wm.wr_id " +
                        "LEFT JOIN transfer_registration_pc trp ON wr.trp_id = trp.trp_id " +
                        "LEFT JOIN transfer_registraion_cd trc ON pi.trc_id = trc.trc_id " +
                        "LEFT JOIN data_dictionary dd ON dd.dd_id = trp.com_id " +
                        $"WHERE wm.wm_obj_id = '{objId}' AND pi_id IS NOT NULL";

                    DataRow planRow = SqlHelper.ExecuteSingleRowQuery(querySql);
                    if(planRow != null)
                    {
                        int rowIndex = dgv_MyReg.Rows.Add();
                        dgv_MyReg.Rows[rowIndex].Tag = row["wm_id"];
                        dgv_MyReg.Rows[rowIndex].Cells["mr_id"].Tag = type;
                        dgv_MyReg.Rows[rowIndex].Cells["mr_id"].Value = planRow["pi_id"];
                        dgv_MyReg.Rows[rowIndex].Cells["mr_name"].Value = planRow["pi_name"];
                        dgv_MyReg.Rows[rowIndex].Cells["mr_pcode"].Value = planRow["trp_code"];
                        dgv_MyReg.Rows[rowIndex].Cells["mr_code"].Value = planRow["pi_code"];
                        dgv_MyReg.Rows[rowIndex].Cells["mr_code"].Tag = planRow["trc_id"];
                        dgv_MyReg.Rows[rowIndex].Cells["mr_unit"].Value = planRow["dd_name"];
                        dgv_MyReg.Rows[rowIndex].Cells["mr_unit"].Tag = planRow["dd_code"];
                        dgv_MyReg.Rows[rowIndex].Cells["mr_fileamount"].Value = GetFileAmountById(planRow["pi_id"]);
                    }
                }
                else if(type == WorkType.PaperWork_Imp || type == WorkType.CDWork_Imp)
                {
                    string querySql = "SELECT dd_name, dd_code, imp_code, imp_name, imp_id, trp.trp_code FROM imp_info ii " +
                        "LEFT JOIN work_myreg wm ON wm.wm_obj_id = ii.imp_id " +
                        "LEFT JOIN work_registration wr ON wr.wr_id = wm.wr_id " +
                        "LEFT JOIN transfer_registration_pc trp ON wr.trp_id = trp.trp_id " +
                        "LEFT JOIN data_dictionary dd ON dd.dd_id = trp.com_id " +
                       $"WHERE imp_id = '{objId}'";

                    DataRow impRow = SqlHelper.ExecuteSingleRowQuery(querySql);
                    if(impRow != null)
                    {
                        int rowIndex = dgv_MyReg.Rows.Add();
                        dgv_MyReg.Rows[rowIndex].Tag = row["wm_id"];
                        dgv_MyReg.Rows[rowIndex].Cells["mr_id"].Tag = type;
                        dgv_MyReg.Rows[rowIndex].Cells["mr_id"].Value = impRow["imp_id"];
                        dgv_MyReg.Rows[rowIndex].Cells["mr_name"].Value = impRow["imp_name"];
                        dgv_MyReg.Rows[rowIndex].Cells["mr_pcode"].Value = impRow["trp_code"];
                        dgv_MyReg.Rows[rowIndex].Cells["mr_code"].Value = impRow["imp_code"];
                        dgv_MyReg.Rows[rowIndex].Cells["mr_unit"].Value = impRow["dd_name"];
                        dgv_MyReg.Rows[rowIndex].Cells["mr_unit"].Tag = impRow["dd_code"];
                        dgv_MyReg.Rows[rowIndex].Cells["mr_fileamount"].Value = GetFileAmountById(impRow["imp_id"]);
                    }
                }
                else if(type == WorkType.PaperWork_Special || type == WorkType.CDWork_Special)
                {
                    string querySql = "SELECT dd_name, dd_code, idi.imp_code, idi.imp_name, idi.imp_id, trp.trp_code FROM imp_dev_info idi " +
                        "LEFT JOIN work_myreg wm ON wm.wm_obj_id = idi.imp_id " +
                        "LEFT JOIN work_registration wr ON wr.wr_id = wm.wr_id " +
                        "LEFT JOIN transfer_registration_pc trp ON wr.trp_id = trp.trp_id " +
                        "LEFT JOIN data_dictionary dd ON dd.dd_id = trp.com_id  " +
                       $"WHERE idi.imp_id='{objId}'";
                    DataRow speRow = SqlHelper.ExecuteSingleRowQuery(querySql);
                    if(speRow != null)
                    {
                        int rowIndex = dgv_MyReg.Rows.Add();
                        dgv_MyReg.Rows[rowIndex].Tag = row["wm_id"];
                        dgv_MyReg.Rows[rowIndex].Cells["mr_id"].Tag = type;
                        dgv_MyReg.Rows[rowIndex].Cells["mr_id"].Value = speRow["imp_id"];
                        dgv_MyReg.Rows[rowIndex].Cells["mr_name"].Value = speRow["imp_name"];
                        dgv_MyReg.Rows[rowIndex].Cells["mr_pcode"].Value = speRow["trp_code"];
                        dgv_MyReg.Rows[rowIndex].Cells["mr_code"].Value = speRow["imp_code"];
                        dgv_MyReg.Rows[rowIndex].Cells["mr_unit"].Value = speRow["dd_name"];
                        dgv_MyReg.Rows[rowIndex].Cells["mr_unit"].Tag = speRow["dd_code"];
                        dgv_MyReg.Rows[rowIndex].Cells["mr_fileamount"].Value = GetFileAmountById(speRow["imp_id"]);
                    }
                }
                else if(type == WorkType.ProjectWork)
                {
                    string querySql = "SELECT dd.dd_name, dd_code, wm.wm_id, pi.pi_id, pi.pi_code, pi.pi_name, wr.wr_obj_id, trp.trp_code, trc.trc_id " +
                        "FROM (SELECT * FROM project_info UNION ALL SELECT * FROM topic_info) pi " +
                        "LEFT JOIN work_myreg wm ON wm.wm_obj_id = pi.pi_id " +
                        "LEFT JOIN work_registration wr ON wr.wr_id = wm.wr_id " +
                        "LEFT JOIN transfer_registration_pc trp ON wr.trp_id = trp.trp_id " +
                        "LEFT JOIN transfer_registraion_cd trc ON trp.trp_id = trc.trp_id " +
                        "LEFT JOIN data_dictionary dd ON dd.dd_id = trp.com_id " +
                        $"WHERE wm.wm_obj_id = '{objId}' ";
                    DataRow proRow = SqlHelper.ExecuteSingleRowQuery(querySql);
                    if(proRow != null)
                    {
                        int rowIndex = dgv_MyReg.Rows.Add();
                        dgv_MyReg.Rows[rowIndex].Tag = row["wm_id"];
                        dgv_MyReg.Rows[rowIndex].Cells["mr_id"].Tag = type;
                        dgv_MyReg.Rows[rowIndex].Cells["mr_id"].Value = proRow["pi_id"];
                        dgv_MyReg.Rows[rowIndex].Cells["mr_name"].Value = proRow["pi_name"];
                        dgv_MyReg.Rows[rowIndex].Cells["mr_pcode"].Value = proRow["trp_code"];
                        dgv_MyReg.Rows[rowIndex].Cells["mr_pcode"].Tag = proRow["trc_id"];
                        dgv_MyReg.Rows[rowIndex].Cells["mr_code"].Value = proRow["pi_code"];
                        dgv_MyReg.Rows[rowIndex].Cells["mr_code"].Tag = proRow["wr_obj_id"];
                        dgv_MyReg.Rows[rowIndex].Cells["mr_unit"].Value = proRow["dd_name"];
                        dgv_MyReg.Rows[rowIndex].Cells["mr_unit"].Tag = proRow["dd_code"];
                        dgv_MyReg.Rows[rowIndex].Cells["mr_fileamount"].Value = GetFileAmountById(proRow["pi_id"]);
                    }
                }
            }

            DataGridViewStyleHelper.SetAlignWithCenter(dgv_MyReg, new string[] { "mr_fileamount" });
            dgv_MyReg.Columns["mr_id"].Visible = false;
        }

        /// <summary>
        /// 根据计划ID获取其下所有文件总数
        /// </summary>
        private object GetFileAmountById(object fileId) => SqlHelper.ExecuteCountQuery($"SELECT COUNT(pfl_id) FROM processing_file_list WHERE pfl_obj_id='{fileId}'");

        /// <summary>
        /// 领取当前及下属所有课题信息
        /// </summary>
        private void GetAllProjectByPid(object objid)
        {
            SqlHelper.ExecuteNonQuery($"UPDATE project_info SET pi_checker_id='{UserHelper.GetUser().UserKey}' WHERE pi_id='{objid}'");
            List<object[]> _obj = SqlHelper.ExecuteColumnsQuery($"SELECT si_id FROM subject_info WHERE pi_id='{objid}'", 1);
            for(int i = 0; i < _obj.Count; i++)
            {
                SqlHelper.ExecuteNonQuery($"UPDATE subject_info SET si_checker_id='{UserHelper.GetUser().UserKey}' WHERE si_id='{_obj[i][0]}'");
                List<object[]> _obj2 = SqlHelper.ExecuteColumnsQuery($"SELECT si_id FROM subject_info WHERE pi_id='{_obj[i][0]}'", 1);
                for(int j = 0; j < _obj2.Count; j++)
                    SqlHelper.ExecuteNonQuery($"UPDATE subject_info SET si_checker_id='{UserHelper.GetUser().UserKey}' WHERE si_id='{_obj2[j][0]}'");
            }
        }
        
        /// <summary>
        /// 待质检 - 种类切换事件
        /// </summary>
        private void Tab_Menulist_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = tab_Menulist.SelectedTabPageIndex;
            searchControl.Visible = false;
            btn_Search.Visible = false;
            if(index == 0)//计划
            {
                LoadImpList();
                pal_Page.Visible = false;
            }
            else if(index == 1)
            {
                LoadImpDevList();
                pal_Page.Visible = false;
            }
            else if(index == 2)
            {
                LoadProjectList(0, null);
                searchControl.Visible = true;
                pal_Page.Visible = true;
                btn_Search.Visible = true;
            }
        }
        
        /// <summary>
        /// 待质检 - 计划列表
        /// </summary>
        private void LoadImpList()
        {
            DataGridViewStyleHelper.ResetDataGridView(dgv_Imp, true);
            dgv_Imp.Columns.AddRange(new DataGridViewColumn[]
            {
                new DataGridViewTextBoxColumn(){ Name = "imp_id"},
                new DataGridViewTextBoxColumn(){ Name = "imp_unit", HeaderText = "来源单位", FillWeight = 12, SortMode = DataGridViewColumnSortMode.NotSortable},
                new DataGridViewTextBoxColumn(){ Name = "imp_pcode", HeaderText = "批次号", FillWeight = 10 , SortMode = DataGridViewColumnSortMode.NotSortable},
                new DataGridViewTextBoxColumn(){ Name = "imp_code", HeaderText = "计划编号", FillWeight = 10 , SortMode = DataGridViewColumnSortMode.NotSortable},
                new DataGridViewTextBoxColumn(){ Name = "imp_name", HeaderText = "计划名称", FillWeight = 15, SortMode = DataGridViewColumnSortMode.NotSortable },
                new DataGridViewTextBoxColumn(){ Name = "imp_fileAmount", HeaderText = "文件数", FillWeight = 5, SortMode = DataGridViewColumnSortMode.NotSortable },
                new DataGridViewTextBoxColumn(){ Name = "imp_qtAmount", HeaderText = "质检次数", FillWeight = 5, SortMode = DataGridViewColumnSortMode.NotSortable },
                new DataGridViewButtonColumn(){ Name = "imp_control", HeaderText = "操作", FillWeight = 5 , SortMode = DataGridViewColumnSortMode.NotSortable},
                new DataGridViewTextBoxColumn(){ Name = "imp_via", HeaderText = "数据途径", FillWeight = 6 , SortMode = DataGridViewColumnSortMode.NotSortable},
            });
            //查询已提交至质检的计划
            /* ---------------------重点计划------------------ */
            string querySql = "SELECT imp_id, dd_id, dd_name, imp_code, imp_name, trp.trp_id, wm.wm_id, wm.wm_ticker, trp.trp_code FROM work_myreg wm " +
                "LEFT JOIN imp_info ii ON wm.wm_obj_id = ii.imp_id " +
                "LEFT JOIN work_registration wr ON wm.wr_id = wr.wr_id " +
                "LEFT JOIN transfer_registration_pc trp ON trp.trp_id = wr.trp_id " +
                "LEFT JOIN data_dictionary dd ON trp.com_id = dd.dd_id " +
               $"WHERE (wm.wm_type='{(int)WorkType.PaperWork_Imp}' OR wm.wm_type='{(int)WorkType.CDWork_Imp}') AND wm.wm_status = 1";
            LoadDataGridViewData(SqlHelper.ExecuteQuery(querySql));
            
            /* ---------------------普通计划------------------ */
            querySql = "SELECT pi_id as imp_id, dd_id, dd_name, pi_code as imp_code, pi_name as imp_name, trp.trp_id, wm.wm_id, wm.wm_ticker, trp.trp_code FROM work_myreg wm " +
                "LEFT JOIN project_info pi ON pi.pi_id = wm.wm_obj_id " +
                "LEFT JOIN work_registration wr ON wm.wr_id = wr.wr_id " +
                "LEFT JOIN transfer_registration_pc trp ON trp.trp_id = wr.trp_id " +
                "LEFT JOIN data_dictionary dd ON dd.dd_id = trp.com_id " +
                $"WHERE (wm.wm_type = '{(int)WorkType.PaperWork_Plan}' OR wm.wm_type = '{(int)WorkType.CDWork_Plan}') AND wm.wm_status = 1";
            LoadDataGridViewData(SqlHelper.ExecuteQuery(querySql));

            dgv_Imp.Columns["imp_id"].Visible = false;
        }

        /// <summary>
        /// 加载表格
        /// </summary>
        private void LoadDataGridViewData(DataTable table)
        {
            for(int i = 0; i < table.Rows.Count; i++)
            {
                DataRow row = table.Rows[i];
                int _index = dgv_Imp.Rows.Add();
                dgv_Imp.Rows[_index].Cells["imp_id"].Tag = row["wm_id"];
                dgv_Imp.Rows[_index].Cells["imp_id"].Value = row["imp_id"];
                dgv_Imp.Rows[_index].Cells["imp_unit"].Tag = row["dd_id"];
                dgv_Imp.Rows[_index].Cells["imp_unit"].Value = row["dd_name"];
                dgv_Imp.Rows[_index].Cells["imp_pcode"].Value = row["trp_code"];
                dgv_Imp.Rows[_index].Cells["imp_code"].Value = row["imp_code"];
                dgv_Imp.Rows[_index].Cells["imp_name"].Value = row["imp_name"];
                dgv_Imp.Rows[_index].Cells["imp_fileAmount"].Value = GetFileAmountById(row["imp_id"]);
                dgv_Imp.Rows[_index].Cells["imp_qtAmount"].Value = row["wm_ticker"];
                dgv_Imp.Rows[_index].Cells["imp_control"].Value = "质检";
                dgv_Imp.Rows[_index].Cells["imp_via"].Value = null;
            }
        }

        /// <summary>
        /// 待质检 - 专项信息
        /// </summary>
        private void LoadImpDevList()
        {
            DataGridViewStyleHelper.ResetDataGridView(dgv_Imp_Dev, true);
            dgv_Imp_Dev.Columns.AddRange(new DataGridViewColumn[]
            {
                new DataGridViewTextBoxColumn(){Name = "imp_dev_id"},
                new DataGridViewTextBoxColumn(){Name = "imp_dev_unit", HeaderText = "来源单位", FillWeight = 12, SortMode = DataGridViewColumnSortMode.NotSortable},
                new DataGridViewTextBoxColumn(){Name = "imp_dev_pcode", HeaderText = "批次号", FillWeight = 10, SortMode = DataGridViewColumnSortMode.NotSortable},
                new DataGridViewTextBoxColumn(){Name = "imp_dev_code", HeaderText = "专项编号", FillWeight = 10, SortMode = DataGridViewColumnSortMode.NotSortable},
                new DataGridViewTextBoxColumn(){Name = "imp_dev_name", HeaderText = "专项名称", FillWeight = 15, SortMode = DataGridViewColumnSortMode.NotSortable},
                new DataGridViewTextBoxColumn(){Name = "imp_dev_fileAmount", HeaderText = "文件数", FillWeight = 5, SortMode = DataGridViewColumnSortMode.NotSortable},
                new DataGridViewTextBoxColumn(){Name = "imp_dev_qtAmount", HeaderText = "质检次数", FillWeight = 5, SortMode = DataGridViewColumnSortMode.NotSortable },
                new DataGridViewButtonColumn(){Name = "imp_dev_control", HeaderText = "操作", FillWeight = 5, Text = "质检", UseColumnTextForButtonValue = true, SortMode = DataGridViewColumnSortMode.NotSortable},
                new DataGridViewTextBoxColumn(){Name = "imp_dev_via", HeaderText = "数据途径", FillWeight = 6, SortMode = DataGridViewColumnSortMode.NotSortable},
            });

            string querySql = "SELECT wm.wm_id, idi.imp_id, dd_id, dd_name, trp.trp_id, idi.imp_code, idi.imp_name, wm.wm_ticker, trp.trp_code FROM imp_dev_info idi " +
                "LEFT JOIN work_myreg wm ON wm.wm_obj_id = idi.imp_id " +
                "LEFT JOIN work_registration wr ON wm.wr_id = wr.wr_id " +
                "LEFT JOIN transfer_registration_pc trp ON trp.trp_id = wr.trp_id " +
                "LEFT JOIN data_dictionary dd ON trp.com_id = dd.dd_id " +
               $"WHERE (wm.wm_type = '{(int)WorkType.PaperWork_Special}' OR wm.wm_type = '{(int)WorkType.CDWork_Special}') AND wm.wm_status = 1";
            DataTable table = SqlHelper.ExecuteQuery(querySql);
            for(int i = 0; i < table.Rows.Count; i++)
            {
                DataRow row = table.Rows[i];
                int _index = dgv_Imp_Dev.Rows.Add();
                dgv_Imp_Dev.Rows[_index].Cells["imp_dev_id"].Tag = row["wm_id"];
                dgv_Imp_Dev.Rows[_index].Cells["imp_dev_id"].Value = row["imp_id"];
                dgv_Imp_Dev.Rows[_index].Cells["imp_dev_unit"].Tag = row["dd_id"];
                dgv_Imp_Dev.Rows[_index].Cells["imp_dev_unit"].Value = row["dd_name"];
                dgv_Imp_Dev.Rows[_index].Cells["imp_dev_pcode"].Value = row["trp_code"];
                dgv_Imp_Dev.Rows[_index].Cells["imp_dev_code"].Value = row["imp_code"];
                dgv_Imp_Dev.Rows[_index].Cells["imp_dev_name"].Value = row["imp_name"];
                dgv_Imp_Dev.Rows[_index].Cells["imp_dev_fileAmount"].Value = GetFileAmountById(row["imp_id"]);
                dgv_Imp_Dev.Rows[_index].Cells["imp_dev_qtAmount"].Value = row["wm_ticker"];
            }

            DataGridViewStyleHelper.SetAlignWithCenter(dgv_Imp_Dev, new string[] { "imp_dev_fileAmount", "imp_dev_qtAmount" });
            dgv_Imp_Dev.Columns["imp_dev_id"].Visible = false;
        }

        /// <summary>
        /// 待质检 - 项目/课题列表
        /// </summary>
        /// <param name="page">页码索引（从0开始计算）</param>
        /// <param name="pageSize">每页页数</param>
        private void LoadProjectList(int page, string key)
        {
            DataGridViewStyleHelper.ResetDataGridView(dgv_Project, true);
            dgv_Project.Columns.AddRange(new DataGridViewColumn[]
            {
                new DataGridViewTextBoxColumn(){Name = "pro_id"},
                new DataGridViewTextBoxColumn(){Name = "pro_unit", HeaderText = "来源单位", FillWeight = 12, SortMode = DataGridViewColumnSortMode.NotSortable},
                new DataGridViewTextBoxColumn(){Name = "pro_pcode", HeaderText = "批次号", FillWeight = 8, SortMode = DataGridViewColumnSortMode.NotSortable},
                new DataGridViewTextBoxColumn(){Name = "pro_code", HeaderText = "项目/课题编号", FillWeight = 10, SortMode = DataGridViewColumnSortMode.NotSortable},
                new DataGridViewTextBoxColumn(){Name = "pro_name", HeaderText = "项目/课题名称", FillWeight = 15, SortMode = DataGridViewColumnSortMode.NotSortable},
                new DataGridViewTextBoxColumn(){Name = "pro_subAmount", HeaderText = "课题/子课题数", FillWeight = 8, SortMode = DataGridViewColumnSortMode.NotSortable},
                new DataGridViewTextBoxColumn(){Name = "pro_fileAmount", HeaderText = "文件数", FillWeight = 5, SortMode = DataGridViewColumnSortMode.NotSortable},
                new DataGridViewTextBoxColumn(){Name = "pro_qtAmount", HeaderText = "质检次数", FillWeight = 5 , SortMode = DataGridViewColumnSortMode.NotSortable},
                new DataGridViewButtonColumn(){Name = "pro_control", HeaderText = "操作", FillWeight = 5, Text = "质检", UseColumnTextForButtonValue = true, SortMode = DataGridViewColumnSortMode.NotSortable},
                new DataGridViewTextBoxColumn(){Name = "pro_via", HeaderText = "数据途径", FillWeight = 6, SortMode = DataGridViewColumnSortMode.NotSortable},
            });

            DataTable table = new DataTable();
            table.Columns.AddRange(new DataColumn[]
            {
                new DataColumn("dd_name"),
                new DataColumn("wm_id"),
                new DataColumn("pi_id"),
                new DataColumn("pi_code"),
                new DataColumn("pi_name"),
                new DataColumn("wm_ticker"),
                new DataColumn("trp_code")
            });
            string queryCondition = string.Empty;
            if(!string.IsNullOrEmpty(key))
                queryCondition = $"AND (pi.pi_code LIKE '%{key}%' OR pi.pi_name LIKE '%{key}%')";
            string querySql = "SELECT * FROM(SELECT ROW_NUMBER() OVER(ORDER BY dd.dd_name) num, dd.dd_name, wm.wm_id, pi.pi_id, pi.pi_code, pi.pi_name, wm.wm_ticker, trp.trp_code " +
                "FROM (SELECT * FROM project_info UNION ALL SELECT * FROM topic_info) pi, work_myreg wm " +
                "LEFT JOIN work_registration wr ON wr.wr_id = wm.wr_id " +
                "LEFT JOIN transfer_registration_pc trp ON wr.trp_id = trp.trp_id " +
                "LEFT JOIN data_dictionary dd ON dd.dd_id = trp.com_id " +
               $"WHERE wm.wm_obj_id = pi.pi_id AND wm.wm_type = '{(int)WorkType.ProjectWork}' AND wm.wm_status = 1 {queryCondition} " +
               $") A WHERE A.num BETWEEN {page * PAGE_SIZE + 1} AND {(page + 1) * PAGE_SIZE}";
            DataTable _table = SqlHelper.ExecuteQuery(querySql);
            foreach(DataRow row in _table.Rows)
                table.ImportRow(row);

            searchControl.Properties.Items.Clear();
            dgv_Project.SuspendLayout();
            foreach(DataRow row in table.Rows)
            {
                int _index = dgv_Project.Rows.Add();
                dgv_Project.Rows[_index].Cells["pro_id"].Tag = row["wm_id"];
                dgv_Project.Rows[_index].Cells["pro_id"].Value = row["pi_id"];
                dgv_Project.Rows[_index].Cells["pro_unit"].Value = row["dd_name"]; 
                dgv_Project.Rows[_index].Cells["pro_code"].Value = row["pi_code"];
                dgv_Project.Rows[_index].Cells["pro_pcode"].Value = row["trp_code"];
                dgv_Project.Rows[_index].Cells["pro_name"].Value = row["pi_name"];
                dgv_Project.Rows[_index].Cells["pro_subAmount"].Value = GetSubAmount(row["pi_id"]);
                dgv_Project.Rows[_index].Cells["pro_fileAmount"].Value = GetFileAmountById(row["pi_id"]);
                dgv_Project.Rows[_index].Cells["pro_qtAmount"].Value = row["wm_ticker"];
                searchControl.Properties.Items.AddRange(new object[] { row["pi_code"], row["pi_name"] });
            }
            dgv_Project.ResumeLayout();
            DataGridViewStyleHelper.SetAlignWithCenter(dgv_Project, new string[] { "pro_fileAmount", "pro_qtAmount", "pro_subAmount" });
            dgv_Project.Columns["pro_id"].Visible = false;

            CURRENT_PAGE = page;
            txt_page.Text = $"{CURRENT_PAGE + 1}";
            if(page == 0)
            {
                int maxCount = SqlHelper.ExecuteCountQuery("SELECT COUNT(pi_id) FROM(SELECT * FROM project_info UNION ALL SELECT * FROM topic_info) AS pi " +
                     "INNER JOIN work_myreg AS wm LEFT OUTER JOIN work_registration AS wr ON wr.wr_id = wm.wr_id " +
                     "LEFT OUTER JOIN transfer_registration_pc AS trp ON wr.trp_id = trp.trp_id " +
                     "LEFT OUTER JOIN data_dictionary AS dd ON dd.dd_id = trp.com_id ON pi.pi_id = wm.wm_obj_id " +
                    $"WHERE(wm.wm_type = '3') AND(wm.wm_status = 1) {queryCondition} ");
                MAX_PAGE = maxCount % PAGE_SIZE == 0 ? maxCount / PAGE_SIZE : maxCount / PAGE_SIZE + 1;
                label1.Text = $"总计 {maxCount} 条记录，每页共 {PAGE_SIZE} 条，共 {MAX_PAGE} 页";
            }
        }

        private object GetSubAmount(object pId) => SqlHelper.ExecuteCountQuery($"SELECT COUNT(si_id)+(SELECT COUNT(ti_id) FROM topic_info WHERE ti_obj_id='{pId}') FROM subject_info WHERE si_obj_id='{pId}'");

        private string GetValue(object value) => value == null ? string.Empty : value.ToString();
        
        /// <summary>
        /// 计划 - 单元格 点击事件
        /// </summary>
        private void Dgv_Imp_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex!=-1 && e.ColumnIndex != -1)
            {
                object columnName = dgv_Imp.Columns[e.ColumnIndex].Name;
                if("imp_control".Equals(columnName))
                {
                    if(XtraMessageBox.Show("确定要质检当前选中数据吗？", "领取确认", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                    {
                        object wmid = dgv_Imp.Rows[e.RowIndex].Cells["imp_id"].Tag;
                        SqlHelper.ExecuteNonQuery($"UPDATE work_myreg SET wm_status='{(int)QualityStatus.Qualitting}', wm_accepter='{UserHelper.GetUser().UserKey}', wm_ticker+=1 WHERE wm_id='{wmid}'");
                        dgv_Imp.Rows.RemoveAt(e.RowIndex);
                    }
                }
            }
        }
        
        /// <summary>
        /// 专项 - 单元格 点击事件
        /// </summary>
        private void Dgv_Imp_Dev_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex != -1 && e.ColumnIndex != -1)
            {
                object columnName = dgv_Imp_Dev.Columns[e.ColumnIndex].Name;
                //领取此条质检
                if("imp_dev_control".Equals(columnName))
                {
                    if(XtraMessageBox.Show("确定要质检当前选中数据吗？", "领取确认", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                    {
                        object wmid = dgv_Imp_Dev.Rows[e.RowIndex].Cells["imp_dev_id"].Tag;
                        SqlHelper.ExecuteNonQuery($"UPDATE work_myreg SET wm_status='{(int)QualityStatus.Qualitting}', wm_accepter='{UserHelper.GetUser().UserKey}', wm_ticker+=1 WHERE wm_id='{wmid}'");
                        dgv_Imp_Dev.Rows.RemoveAt(e.RowIndex);
                    }
                }
            }
        }
        
        /// <summary>
        /// 项目/课题 - 单元格 点击事件
        /// </summary>
        private void Dgv_Project_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex != -1 && e.ColumnIndex != -1)
            {
                object columnName = dgv_Project.Columns[e.ColumnIndex].Name;
                if("pro_control".Equals(columnName))
                {
                    if(XtraMessageBox.Show("确定要质检当前选中数据吗？", "领取确认", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                    {
                        object wmid = dgv_Project.Rows[e.RowIndex].Cells["pro_id"].Tag;
                        int index = SqlHelper.ExecuteCountQuery($"SELECT COUNT(wm_id) FROM work_myreg WHERE wm_status=1 AND wm_id='{wmid}'");
                        if(index == 0)
                        {
                            XtraMessageBox.Show("此条数据已被领取。", "领取失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            dgv_Project.Rows.RemoveAt(e.RowIndex);
                            return;
                        }
                        object objId = SqlHelper.ExecuteOnlyOneQuery($"SELECT wm_obj_id FROM work_myreg WHERE wm_id='{wmid}'");

                        StringBuilder updateSQL = new StringBuilder();
                        updateSQL.Append($"UPDATE project_info SET pi_checker_id='{UserHelper.GetUser().UserKey}', pi_checker_date='{DateTime.Now}' WHERE pi_id='{objId}';");
                        updateSQL.Append($"UPDATE topic_info SET ti_checker_id='{UserHelper.GetUser().UserKey}', ti_checker_date='{DateTime.Now}' WHERE ti_id='{objId}';");
                        updateSQL.Append($"UPDATE processing_file_list SET pfl_checker_id='{UserHelper.GetUser().UserKey}', pfl_checker_date='{DateTime.Now}' WHERE pfl_obj_id='{objId}';");

                        DataTable topicTable = SqlHelper.ExecuteQuery($"SELECT ti_id FROM topic_info WHERE ti_obj_id='{objId}' UNION ALL SELECT si_id FROM subject_info WHERE si_obj_id='{objId}'");
                        foreach(DataRow row in topicTable.Rows)
                        {
                            updateSQL.Append($"UPDATE topic_info SET ti_checker_id='{UserHelper.GetUser().UserKey}', ti_checker_date='{DateTime.Now}' WHERE ti_id='{row["ti_id"]}';");
                            updateSQL.Append($"UPDATE subject_info SET si_checker_id='{UserHelper.GetUser().UserKey}', si_checker_date='{DateTime.Now}' WHERE si_id='{row["ti_id"]}';");
                            updateSQL.Append($"UPDATE processing_file_list SET pfl_checker_id='{UserHelper.GetUser().UserKey}', pfl_checker_date='{DateTime.Now}' WHERE pfl_obj_id='{row["ti_id"]}';");

                            DataTable subjectTable = SqlHelper.ExecuteQuery($"SELECT si_id FROM subject_info WHERE si_obj_id='{row["ti_id"]}'");
                            foreach(DataRow subRow in subjectTable.Rows)
                            {
                                updateSQL.Append($"UPDATE subject_info SET si_checker_id='{UserHelper.GetUser().UserKey}', si_checker_date='{DateTime.Now}' WHERE si_id='{subRow["si_id"]}';");
                                updateSQL.Append($"UPDATE processing_file_list SET pfl_checker_id='{UserHelper.GetUser().UserKey}', pfl_checker_date='{DateTime.Now}' WHERE pfl_obj_id='{subRow["si_id"]}';");
                            }
                        }
                        updateSQL.Append($"UPDATE work_myreg SET wm_status='{(int)QualityStatus.Qualitting}', wm_accepter='{UserHelper.GetUser().UserKey}', wm_ticker+=1 WHERE wm_id='{wmid}';");

                        SqlHelper.ExecuteNonQuery(updateSQL.ToString());

                        dgv_Project.Rows.RemoveAt(e.RowIndex);
                        XtraMessageBox.Show("操作成功。");
                    }
                }
                //else if("pro_subAmount".Equals(columnName))
                //{
                //    object unitName = dgv_Project.Rows[e.RowIndex].Cells["pro_unit"].Value;
                //    object pId = dgv_Project.Rows[e.RowIndex].Cells["pro_id"].Value;
                //    LoadSubProjectList(pId, unitName);
                //}
            }
        }

        private void LoadSubProjectList(object pId, object unitName)
        {
            DataGridViewStyleHelper.ResetDataGridView(dgv_Project, true);
            dgv_Project.Columns.AddRange(new DataGridViewColumn[]
            {
                new DataGridViewTextBoxColumn(){ Name = "subId" },
                new DataGridViewTextBoxColumn(){ Name = "subUnitName", HeaderText = "来源单位", FillWeight = 15, SortMode = DataGridViewColumnSortMode.NotSortable},
                new DataGridViewTextBoxColumn(){ Name = "subCode", HeaderText = "课题/子课题编号", FillWeight = 20, SortMode = DataGridViewColumnSortMode.NotSortable},
                new DataGridViewTextBoxColumn(){ Name = "subName", HeaderText = "课题/子课题名称", FillWeight = 25, SortMode = DataGridViewColumnSortMode.NotSortable},
                //new DataGridViewButtonColumn(){ Name = "subControl", HeaderText = "操作", Text="质检", FillWeight = 5}
            });
            dgv_Project.Columns["subId"].Visible = false;

            DataTable table = SqlHelper.ExecuteQuery($"SELECT * FROM subject_info WHERE pi_id='{pId}'");
            for(int i = 0; i < table.Rows.Count; i++)
            {
                int index = dgv_Project.Rows.Add();
                dgv_Project.Rows[index].Cells["subId"].Value = table.Rows[i]["si_id"];
                dgv_Project.Rows[index].Cells["subUnitName"].Value = unitName;
                dgv_Project.Rows[index].Cells["subCode"].Value = table.Rows[i]["si_code"];
                dgv_Project.Rows[index].Cells["subName"].Value = table.Rows[i]["si_name"];
                //dgv_Project.Rows[index].Cells["subControl"].Value = "质检";
            }
        }

        /// <summary>
        /// 质检中 - 单元格 点击事件
        /// </summary>
        private void Dgv_MyReg_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex != -1 && e.ColumnIndex != -1)
            {
                object wmid = dgv_MyReg.Rows[e.RowIndex].Tag;
                object columnName = dgv_MyReg.Columns[e.ColumnIndex].Name;
                //编辑
                if("mr_edit".Equals(columnName))
                {
                    object objid = dgv_MyReg.Rows[e.RowIndex].Cells["mr_id"].Value;
                    object unitCode = dgv_MyReg.Rows[e.RowIndex].Cells["mr_unit"].Tag;
                    WorkType type = (WorkType)dgv_MyReg.Rows[e.RowIndex].Cells["mr_id"].Tag;
                    if(type == WorkType.PaperWork_Imp || type == WorkType.CDWork_Imp)
                    {
                        Frm_MyWorkQT frm = GetFormHelper.GetMyWorkQT(type, objid, wmid, ControlType.Imp);
                        frm.unitCode = unitCode;
                        frm.BackCallMethod = CheckExistBackLog;
                        frm.Show();
                        frm.Activate();
                    }
                    else if(type == WorkType.PaperWork_Special || type == WorkType.CDWork_Special)
                    {
                        Frm_MyWorkQT frm = GetFormHelper.GetMyWorkQT(type, objid, wmid, ControlType.Special);
                        frm.unitCode = unitCode;
                        frm.BackCallMethod = CheckExistBackLog;
                        frm.Show();
                        frm.Activate();
                    }
                    else if(type == WorkType.ProjectWork)
                    {
                        object piid = dgv_MyReg.Rows[e.RowIndex].Cells["mr_id"].Value;
                        Frm_MyWorkQT frm = GetFormHelper.GetMyWorkQT(WorkType.ProjectWork, piid, wmid, ControlType.Project);
                        frm.trcId = dgv_MyReg.Rows[e.RowIndex].Cells["mr_pcode"].Tag;
                        frm.unitCode = unitCode;
                        frm.BackCallMethod = CheckExistBackLog;
                        frm.Show();
                        frm.Activate();
                    }
                    else if(type == WorkType.PaperWork_Plan || type == WorkType.CDWork_Plan)
                    {
                        object piid = dgv_MyReg.Rows[e.RowIndex].Cells["mr_id"].Value;
                        Frm_MyWorkQT frm = GetFormHelper.GetMyWorkQT(WorkType.PaperWork_Plan, piid, objid, ControlType.Plan);
                        frm.unitCode = unitCode;
                        frm.BackCallMethod = CheckExistBackLog;
                        frm.trcId = dgv_MyReg.Rows[e.RowIndex].Cells["mr_code"].Tag;
                        frm.Show();
                        frm.Activate();
                    }
                }
                //完成质检
                else if("mr_submit".Equals(columnName))
                {
                    if(XtraMessageBox.Show("确定要完成对当前数据的质检吗？", "确认提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk) == DialogResult.OK)
                    {
                        SqlHelper.ExecuteNonQuery($"UPDATE work_myreg SET wm_status='{(int)QualityStatus.QualityFinish}' WHERE wm_id='{wmid}'");
                    }
                    LoadMyRegList();
                }
                //我的质检
                else if("mrl_edit".Equals(columnName))
                {
                    object piid = dgv_MyReg.Rows[e.RowIndex].Cells["mrl_id"].Value;
                    Frm_MyWorkQT frm = GetFormHelper.GetMyWorkQT(WorkType.ProjectWork, piid, wmid, ControlType.Project, true);
                    frm.Show();
                    frm.Activate();
                }
            }
        }

        private void CheckExistBackLog(WorkType type, object objid, object wmid, object piid)
        {
            int index = 0;
            if(type == WorkType.PaperWork_Imp || type == WorkType.CDWork_Imp)
            {
                index = SqlHelper.ExecuteCountQuery($"SELECT COUNT(*) FROM imp_info WHERE imp_id='{objid}' AND imp_submit_status=1");
            }
            else if(type == WorkType.PaperWork_Special || type == WorkType.CDWork_Special)
            {
                index = SqlHelper.ExecuteCountQuery($"SELECT COUNT(*) FROM imp_dev_info WHERE imp_id='{objid}' AND imp_submit_status=1");
            }
            else if(type == WorkType.ProjectWork || type == WorkType.PaperWork_Plan)
            {
                if(HaveBacked(piid))
                {
                    if(XtraMessageBox.Show("此数据有返工记录，是否执行返工操作？", "温馨提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk) == DialogResult.OK)
                    {
                        SqlHelper.ExecuteNonQuery($"UPDATE work_myreg SET wm_accepter_date='{DateTime.Now}', wm_status={(int)QualityStatus.QualityBack} WHERE wm_id='{wmid}'");
                        LoadMyRegList();
                        XtraMessageBox.Show("操作成功。");
                    }
                }
                return;
            }
            if(index > 0)
            {
                if(XtraMessageBox.Show("此数据有返工记录，是否执行返工操作？", "温馨提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk) == DialogResult.OK)
                {
                    SqlHelper.ExecuteNonQuery($"UPDATE work_myreg SET wm_accepter_date='{DateTime.Now}', wm_status={(int)QualityStatus.QualityBack} WHERE wm_id='{wmid}'");
                    LoadMyRegList();
                    XtraMessageBox.Show("操作成功。");
                }
            }
        }

        /// <summary>
        /// 根据ID判断是否存在返工数据
        /// </summary>
        private bool HaveBacked(object piid)
        {
            bool result = false;
            DataRow row = SqlHelper.ExecuteSingleRowQuery($"SELECT pi_id, pi_submit_status FROM project_info WHERE pi_id='{piid}' UNION ALL " +
                $"SELECT ti_id, ti_submit_status FROM topic_info WHERE ti_id='{piid}'");
            if(row != null)
            {
                if((int)row["pi_submit_status"] == 1)
                    result = true;
                else
                {
                    DataTable table = SqlHelper.ExecuteQuery($"SELECT ti_id, ti_submit_status FROM topic_info WHERE ti_obj_id='{row["pi_id"]}' UNION ALL " +
                        $"SELECT si_id, si_submit_status FROM subject_info WHERE si_obj_id='{row["pi_id"]}'");
                    for(int i = 0; i < table.Rows.Count; i++)
                    {
                        if((int)table.Rows[i]["ti_submit_status"] == 1)
                        {
                            result = true;
                            break;
                        }
                        DataTable table2 = SqlHelper.ExecuteQuery($"SELECT si_id, si_submit_status FROM subject_info WHERE si_obj_id='{table.Rows[i]["ti_id"]}'");
                        for(int j = 0; j < table2.Rows.Count; j++)
                        {
                            if((int)table2.Rows[j]["si_submit_status"] == 1)
                            {
                                result = true;
                                break;
                            }
                        }
                    }
                }
            }
            return result;
        }

        int index = -1;
        private void SearchControl_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                string key = searchControl.Text;
                if(!string.IsNullOrEmpty(key))
                {
                    dgv_Project.ClearSelection();
                    foreach(DataGridViewRow row in dgv_Project.Rows)
                    {
                        if(row.Index > index)
                        {
                            string code = ToolHelper.GetValue(row.Cells["pro_code"].Value);
                            string name = GetValue(row.Cells["pro_name"].Value);
                            if(code.Contains(key) || name.Contains(key))
                            {
                                dgv_Project.FirstDisplayedScrollingRowIndex = row.Index;
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

        private void Page_Click(object sender, EventArgs e)
        {
            string key = searchControl.Text.Trim();
            string name = (sender as SimpleButton).Name;
            if(name.Contains("fpage"))
            {
                LoadProjectList(0, null);
            }
            else if(name.Contains("lpage"))
            {
                if(CURRENT_PAGE > 0)
                    LoadProjectList(CURRENT_PAGE - 1, key);
            }
            else if(name.Contains("npage"))
            {
                if(CURRENT_PAGE < MAX_PAGE - 1)
                    LoadProjectList(CURRENT_PAGE + 1, key);
            }
            else if(name.Contains("epage"))
            {
                LoadProjectList(MAX_PAGE - 1, key);
            }
        }

        private void btn_Search_Click(object sender, EventArgs e)
        {
            string key = searchControl.Text;
            LoadProjectList(0, key);
        }
    }
}
