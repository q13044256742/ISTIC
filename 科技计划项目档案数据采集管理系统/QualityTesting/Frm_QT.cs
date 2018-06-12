using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace 科技计划项目档案数据采集管理系统
{
    public partial class Frm_QT : Form
    {
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
            }
            else if("ace_MyLog".Equals(name))
            {
                tab_Menulist.Visible = false;
                dgv_MyReg.Visible = true;
                dgv_MyReg.DefaultCellStyle = dgv_Project.DefaultCellStyle;
                dgv_MyReg.ColumnHeadersDefaultCellStyle = DataGridViewStyleHelper.GetHeaderStyle();
                LoadMyRegList();
            }
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
        /// ----------------------------过期-------------------------------
        /// 【计划/专项】单元格点击事件
        /// </summary>
        private void Dgv_Plan_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex != -1 && e.ColumnIndex != -1)
            {
                object columnName = dgv_Imp.Columns[e.ColumnIndex].Name;
                //待质检 - 【计划/专项】质检
                if("plan_edit".Equals(columnName))
                {
                    object objid = dgv_Imp.Rows[e.RowIndex].Cells["plan_id"].Value;
                    SqlHelper.ExecuteNonQuery($"UPDATE work_registration SET wr_receive_status={(int)ReceiveStatus.ReceiveSuccess} WHERE wr_id='{objid}'");
                    SqlHelper.ExecuteNonQuery($"INSERT INTO work_myreg(wm_id,wr_id,wm_status,wm_user)" +
                        $" VALUES('{Guid.NewGuid().ToString()}','{objid}','{(int)QualityStatus.NonQuality}','{UserHelper.GetInstance().User.UserKey}')");
                    LoadMyRegList();
                }
                //我的质检 -  编辑
                else if("mr_edit".Equals(columnName))
                {
                    object objid = dgv_Imp.Rows[e.RowIndex].Cells["mr_id"].Value;
                    object wrid = dgv_Imp.Rows[e.RowIndex].Cells["mr_code"].Tag;
                    WorkType type = (WorkType)Convert.ToInt32(SqlHelper.ExecuteOnlyOneQuery($"SELECT wr_type FROM work_registration WHERE wr_id='{wrid}'"));
                    object planId = GetRootId(objid, type);
                    if(planId != null)
                        new Frm_MyWorkQT(type, objid, null, ControlType.Default).ShowDialog();
                    else
                    {
                        planId = GetRootId(objid, WorkType.TopicWork);
                        if(planId != null)
                            new Frm_MyWorkQT(WorkType.TopicWork, planId, null, ControlType.Default).ShowDialog();
                        else
                            MessageBox.Show("未找到此项目/课题所属计划。");
                    }
                }
                //我的质检 - 提交（返工）
                else if("mr_submit".Equals(columnName))
                {
                    object wmid = dgv_Imp.Rows[e.RowIndex].Cells["mr_id"].Tag;
                    if(wmid != null)
                    {
                        if(MessageBox.Show("确定要将选中的数据返工吗?", "确认提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            SqlHelper.ExecuteNonQuery($"UPDATE work_myreg SET wm_status={(int)ObjectSubmitStatus.Back} WHERE wm_id='{wmid}'");
                            LoadMyRegList();
                        }
                    }
                }
            }
        }
        /// <summary>
        /// 获取指定项目/课题获取其所属计划ID
        /// </summary>
        private object GetRootId(object objId, WorkType type)
        {
            if(type == WorkType.PaperWork)
                return SqlHelper.ExecuteOnlyOneQuery($"SELECT pi_id FROM project_info WHERE pi_obj_id='{objId}' AND pi_source_id='{UserHelper.GetInstance().User.UserKey}'");
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
        /// 我的质检列表
        /// </summary>
        private void LoadMyRegList()
        {
            DataGridViewStyleHelper.ResetDataGridView(dgv_MyReg, true);
            dgv_MyReg.Columns.AddRange(new DataGridViewColumn[]
            {
                new DataGridViewTextBoxColumn(){ Name = "mr_id"},
                new DataGridViewTextBoxColumn(){ Name = "mr_unit", HeaderText = "来源单位", FillWeight = 15},
                new DataGridViewTextBoxColumn(){ Name = "mr_code", HeaderText = "编号", FillWeight = 15},
                new DataGridViewTextBoxColumn(){ Name = "mr_name", HeaderText = "名称", FillWeight = 20},
                new DataGridViewTextBoxColumn(){ Name = "mr_fileamount", HeaderText = "文件数", FillWeight = 5},
                new DataGridViewButtonColumn(){ Name = "mr_edit", HeaderText = "操作", FillWeight = 8, Text = "编辑", UseColumnTextForButtonValue = true},
                new DataGridViewButtonColumn(){ Name = "mr_submit", HeaderText = "完成", FillWeight = 8, Text = "提交", UseColumnTextForButtonValue = true}
            });

            List<object[]> _obj = SqlHelper.ExecuteColumnsQuery($"SELECT wm_id, wm_type, wm_obj_id FROM work_myreg WHERE wm_accepter='{UserHelper.GetInstance().User.UserKey}' AND wm_status=2", 3);
            for(int i = 0; i < _obj.Count; i++)
            {
                int index = (int)_obj[i][1];
                object objId = _obj[i][2];
                if(index == 0)
                {
                    string querySql = "SELECT dd_name, imp_code, imp_name, imp_id FROM imp_info ii " +
                        "LEFT JOIN work_myreg wm ON wm.wm_obj_id = ii.imp_id " +
                        "LEFT JOIN work_registration wr ON wr.wr_id = wm.wr_id " +
                        "LEFT JOIN transfer_registration_pc trp ON wr.trp_id = trp.trp_id " +
                        "LEFT JOIN data_dictionary dd ON dd.dd_id = trp.com_id " +
                       $"WHERE imp_id = '{objId}'";

                    DataRow row = SqlHelper.ExecuteSingleRowQuery(querySql);
                    if(row != null)
                    {
                        int rowIndex = dgv_MyReg.Rows.Add();
                        dgv_MyReg.Rows[rowIndex].Tag = _obj[i][0];
                        dgv_MyReg.Rows[rowIndex].Cells["mr_id"].Tag = index;
                        dgv_MyReg.Rows[rowIndex].Cells["mr_id"].Value = row["imp_id"];
                        dgv_MyReg.Rows[rowIndex].Cells["mr_name"].Value = row["imp_name"];
                        dgv_MyReg.Rows[rowIndex].Cells["mr_code"].Value = row["imp_code"];
                        dgv_MyReg.Rows[rowIndex].Cells["mr_unit"].Value = row["dd_name"];
                        dgv_MyReg.Rows[rowIndex].Cells["mr_fileamount"].Value = GetFileAmountById(row["imp_id"], ControlType.Imp);
                    }
                    querySql = "SELECT dd_name, pi_code, pi_name, pi_id FROM project_info pi " +
                        "LEFT JOIN work_registration wr ON pi.pi_obj_id = wr.wr_obj_id " +
                        "LEFT JOIN work_myreg wm ON wm.wr_id = wr.wr_id " +
                        "LEFT JOIN transfer_registration_pc trp ON wr.trp_id = trp.trp_id " +
                        "LEFT JOIN data_dictionary dd ON dd.dd_id = trp.com_id " +
                        $"WHERE wm.wm_id='{_obj[i][0]}'";
                    row = SqlHelper.ExecuteSingleRowQuery(querySql);
                    if(row != null)
                    {
                        int rowIndex = dgv_MyReg.Rows.Add();
                        dgv_MyReg.Rows[rowIndex].Tag = _obj[i][0];//wmid
                        dgv_MyReg.Rows[rowIndex].Cells["mr_id"].Tag = index;
                        dgv_MyReg.Rows[rowIndex].Cells["mr_id"].Value = row["pi_id"];
                        dgv_MyReg.Rows[rowIndex].Cells["mr_name"].Value = row["pi_name"];
                        dgv_MyReg.Rows[rowIndex].Cells["mr_code"].Value = row["pi_code"];
                        dgv_MyReg.Rows[rowIndex].Cells["mr_unit"].Value = row["dd_name"];
                        dgv_MyReg.Rows[rowIndex].Cells["mr_fileamount"].Value = GetFileAmountById(row["pi_id"], ControlType.Imp);
                    }
                }
                else if(index == 1)
                {
                    string querySql = "SELECT dd_name, idi.imp_code, idi.imp_name, idi.imp_id FROM imp_dev_info idi " +
                        "LEFT JOIN work_myreg wm ON wm.wm_obj_id = idi.imp_id " +
                        "LEFT JOIN work_registration wr ON wr.wr_id = wm.wr_id " +
                        "LEFT JOIN transfer_registration_pc trp ON wr.trp_id = trp.trp_id " +
                        "LEFT JOIN data_dictionary dd ON dd.dd_id = trp.com_id  " +
                       $"WHERE idi.imp_id='{objId}'";
                    DataRow row = SqlHelper.ExecuteSingleRowQuery(querySql);
                    int rowIndex = dgv_MyReg.Rows.Add();
                    dgv_MyReg.Rows[rowIndex].Tag = _obj[i][0];
                    dgv_MyReg.Rows[rowIndex].Cells["mr_id"].Tag = index;
                    dgv_MyReg.Rows[rowIndex].Cells["mr_id"].Value = row["imp_id"];
                    dgv_MyReg.Rows[rowIndex].Cells["mr_name"].Value = row["imp_name"];
                    dgv_MyReg.Rows[rowIndex].Cells["mr_code"].Value = row["imp_code"];
                    dgv_MyReg.Rows[rowIndex].Cells["mr_unit"].Value = row["dd_name"];
                    dgv_MyReg.Rows[rowIndex].Cells["mr_fileamount"].Value = GetFileAmountById(row["imp_id"], ControlType.Default);
                }
                else if(index == 2 || index == -1 || index == 3)
                {
                    string querySql = "SELECT dd.dd_name, wm.wm_id, pi.pi_id, pi.pi_code, pi.pi_name FROM project_info pi " +
                        "LEFT JOIN work_myreg wm ON wm.wm_obj_id = pi.pi_id " +
                        "LEFT JOIN work_registration wr ON wr.wr_id = wm.wr_id " +
                        "LEFT JOIN transfer_registration_pc trp ON wr.trp_id = trp.trp_id " +
                        "LEFT JOIN data_dictionary dd ON dd.dd_id = trp.com_id " +
                        $"WHERE wm.wm_obj_id = '{objId}' " +
                        "UNION ALL " +
                        "SELECT dd.dd_name, wm.wm_id, ti.ti_id, ti.ti_code, ti.ti_name FROM topic_info ti " +
                        "LEFT JOIN work_myreg wm ON wm.wm_obj_id = ti.ti_id " +
                        "LEFT JOIN work_registration wr ON wr.wr_id = wm.wr_id " +
                        "LEFT JOIN transfer_registration_pc trp ON wr.trp_id = trp.trp_id " +
                        "LEFT JOIN data_dictionary dd ON dd.dd_id = trp.com_id " +
                        $"WHERE wm.wm_obj_id = '{objId}'";
                    DataRow row = SqlHelper.ExecuteSingleRowQuery(querySql);
                    if(row != null)
                    {
                        int rowIndex = dgv_MyReg.Rows.Add();
                        dgv_MyReg.Rows[rowIndex].Tag = _obj[i][0];
                        dgv_MyReg.Rows[rowIndex].Cells["mr_id"].Tag = index;
                        dgv_MyReg.Rows[rowIndex].Cells["mr_id"].Value = row["pi_id"];
                        dgv_MyReg.Rows[rowIndex].Cells["mr_name"].Value = row["pi_name"];
                        dgv_MyReg.Rows[rowIndex].Cells["mr_code"].Value = row["pi_code"];
                        dgv_MyReg.Rows[rowIndex].Cells["mr_unit"].Value = row["dd_name"];
                        dgv_MyReg.Rows[rowIndex].Cells["mr_fileamount"].Value = GetFileAmountById(row["pi_id"], ControlType.Default);
                    }
                }
            }

            DataGridViewStyleHelper.SetAlignWithCenter(dgv_MyReg, new string[] { "mr_fileamount" });
            dgv_MyReg.Columns["mr_id"].Visible = false;
        }
        
        /// <summary>
        /// 根据计划ID获取其下所有文件总数
        /// </summary>
        private object GetFileAmountById(object pid, ControlType type)
        {
            int totalAmount = 0;
            if(type == ControlType.Default)
            {
                totalAmount = Convert.ToInt32(SqlHelper.ExecuteOnlyOneQuery($"SELECT COUNT(pfl_id) FROM processing_file_list WHERE pfl_obj_id = '{pid}'"));
            }
            else
            {
                object objid = SqlHelper.ExecuteOnlyOneQuery($"SELECT pi_id FROM project_info WHERE trc_id='{pid}'");
                if(objid == null)
                    objid = SqlHelper.ExecuteOnlyOneQuery($"SELECT pi_id FROM project_info WHERE pi_obj_id='{pid}'");
                totalAmount = Convert.ToInt32(SqlHelper.ExecuteOnlyOneQuery($"SELECT COUNT(pfl_id) FROM processing_file_list WHERE pfl_obj_id='{objid}'"));
                List<object[]> _obj1 = SqlHelper.ExecuteColumnsQuery($"SELECT pi_id FROM project_info WHERE pi_obj_id='{objid}'", 1);
                for(int i = 0; i < _obj1.Count; i++)
                {
                    totalAmount += Convert.ToInt32(SqlHelper.ExecuteOnlyOneQuery($"SELECT COUNT(pfl_id) FROM processing_file_list WHERE pfl_obj_id='{_obj1[i][0]}'"));
                    List<object[]> _obj2 = SqlHelper.ExecuteColumnsQuery($"SELECT si_id FROM subject_info WHERE pi_id='{_obj1[i][0]}'", 1);
                    for(int j = 0; j < _obj2.Count; j++)
                    {
                        totalAmount += Convert.ToInt32(SqlHelper.ExecuteOnlyOneQuery($"SELECT COUNT(pfl_id) FROM processing_file_list WHERE pfl_obj_id='{_obj2[j][0]}'"));
                        List<object[]> _obj3 = SqlHelper.ExecuteColumnsQuery($"SELECT si_id FROM subject_info WHERE pi_id='{_obj2[j][0]}'", 1);
                        for(int k = 0; k < _obj3.Count; k++)
                            totalAmount += Convert.ToInt32(SqlHelper.ExecuteOnlyOneQuery($"SELECT COUNT(pfl_id) FROM processing_file_list WHERE pfl_obj_id='{_obj3[k][0]}'"));
                    }
                }
            }
            return totalAmount;
        }
     
        /// <summary>
        /// 领取当前及下属所有课题信息
        /// </summary>
        private void GetAllProjectByPid(object objid)
        {
            SqlHelper.ExecuteNonQuery($"UPDATE project_info SET pi_checker_id='{UserHelper.GetInstance().User.UserKey}' WHERE pi_id='{objid}'");
            List<object[]> _obj = SqlHelper.ExecuteColumnsQuery($"SELECT si_id FROM subject_info WHERE pi_id='{objid}'", 1);
            for(int i = 0; i < _obj.Count; i++)
            {
                SqlHelper.ExecuteNonQuery($"UPDATE subject_info SET si_checker_id='{UserHelper.GetInstance().User.UserKey}' WHERE si_id='{_obj[i][0]}'");
                List<object[]> _obj2 = SqlHelper.ExecuteColumnsQuery($"SELECT si_id FROM subject_info WHERE pi_id='{_obj[i][0]}'", 1);
                for(int j = 0; j < _obj2.Count; j++)
                    SqlHelper.ExecuteNonQuery($"UPDATE subject_info SET si_checker_id='{UserHelper.GetInstance().User.UserKey}' WHERE si_id='{_obj2[j][0]}'");
            }
        }
        
        /// <summary>
        /// 待质检 - 种类切换事件
        /// </summary>
        private void Tab_Menulist_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = tab_Menulist.SelectedTabPageIndex;
            if(index == 0)//计划
            {
                LoadImpList();
            }
            else if(index == 1)
            {
                LoadImpDevList();
            }
            else if(index == 2)
            {
                LoadProjectList();
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
                new DataGridViewTextBoxColumn(){ Name = "imp_unit", HeaderText = "来源单位", FillWeight = 12},
                new DataGridViewTextBoxColumn(){ Name = "imp_code", HeaderText = "计划编号", FillWeight = 10 },
                new DataGridViewTextBoxColumn(){ Name = "imp_name", HeaderText = "计划名称", FillWeight = 15 },
                new DataGridViewTextBoxColumn(){ Name = "imp_fileAmount", HeaderText = "文件数", FillWeight = 5 },
                new DataGridViewTextBoxColumn(){ Name = "imp_qtAmount", HeaderText = "质检次数", FillWeight = 5 },
                new DataGridViewButtonColumn(){ Name = "imp_control", HeaderText = "操作", FillWeight = 5 },
                new DataGridViewTextBoxColumn(){ Name = "imp_via", HeaderText = "数据途径", FillWeight = 6 },
            });
            //查询已提交至质检的计划
            /* ---------------------重点计划------------------ */
            string querySql = "SELECT imp_id, dd_id, dd_name, imp_code, imp_name, trp.trp_id, wm.wm_id, wm.wm_ticker FROM imp_info ii " +
                "LEFT JOIN work_myreg wm ON wm.wm_obj_id = ii.imp_id " +
                "LEFT JOIN work_registration wr ON wm.wr_id = wr.wr_id " +
                "LEFT JOIN transfer_registration_pc trp ON trp.trp_id = wr.trp_id " +
                "LEFT JOIN data_dictionary dd ON trp.com_id = dd.dd_id " +
                "WHERE wm.wm_type = 0 AND wm.wm_status = 1";
            LoadDataGridViewData(SqlHelper.ExecuteQuery(querySql));
            
            /* ---------------------普通计划------------------ */
            querySql = "SELECT pi_id as imp_id, dd_id, dd_name, pi_code as imp_code, pi_name as imp_name, trp.trp_id, wm.wm_id, wm.wm_ticker FROM work_myreg wm " +
                "LEFT JOIN project_info pi ON pi.pi_id = wm.wm_obj_id " +
                "LEFT JOIN work_registration wr ON wm.wr_id = wr.wr_id " +
                "LEFT JOIN transfer_registration_pc trp ON trp.trp_id = wr.trp_id " +
                "LEFT JOIN data_dictionary dd ON dd.dd_id = trp.com_id " +
                "WHERE wm.wm_type = -1 AND wm.wm_status = 1";
            LoadDataGridViewData(SqlHelper.ExecuteQuery(querySql));

            DataGridViewStyleHelper.SetAlignWithCenter(dgv_Imp, new string[] { "imp_fileAmount", "imp_qtAmount" });
            DataGridViewStyleHelper.SetLinkStyle(dgv_Imp, new string[] { "imp_control", }, true);
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
                dgv_Imp.Rows[_index].Cells["imp_code"].Value = row["imp_code"];
                dgv_Imp.Rows[_index].Cells["imp_name"].Value = row["imp_name"];
                dgv_Imp.Rows[_index].Cells["imp_fileAmount"].Value = GetFileAmountById(row["imp_id"], ControlType.Default);
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
                new DataGridViewTextBoxColumn(){Name = "imp_dev_unit", HeaderText = "来源单位", FillWeight = 12},
                new DataGridViewTextBoxColumn(){Name = "imp_dev_code", HeaderText = "专项编号", FillWeight = 10},
                new DataGridViewTextBoxColumn(){Name = "imp_dev_name", HeaderText = "专项名称", FillWeight = 15},
                new DataGridViewTextBoxColumn(){Name = "imp_dev_fileAmount", HeaderText = "文件数", FillWeight = 5},
                new DataGridViewTextBoxColumn(){Name = "imp_dev_qtAmount", HeaderText = "质检次数", FillWeight = 5 },
                new DataGridViewButtonColumn(){Name = "imp_dev_control", HeaderText = "操作", FillWeight = 5, Text = "质检", UseColumnTextForButtonValue = true},
                new DataGridViewTextBoxColumn(){Name = "imp_dev_via", HeaderText = "数据途径", FillWeight = 6},
            });

            string querySql = "SELECT wm.wm_id, idi.imp_id, dd_id, dd_name, trp.trp_id, idi.imp_code, idi.imp_name, wm.wm_ticker FROM imp_dev_info idi " +
                "LEFT JOIN work_myreg wm ON wm.wm_obj_id = idi.imp_id " +
                "LEFT JOIN work_registration wr ON wm.wr_id = wr.wr_id " +
                "LEFT JOIN transfer_registration_pc trp ON trp.trp_id = wr.trp_id " +
                "LEFT JOIN data_dictionary dd ON trp.com_id = dd.dd_id " +
                "WHERE wm.wm_type = 1 AND wm.wm_status = 1";
            DataTable table = SqlHelper.ExecuteQuery(querySql);
            for(int i = 0; i < table.Rows.Count; i++)
            {
                DataRow row = table.Rows[i];
                int _index = dgv_Imp_Dev.Rows.Add();
                dgv_Imp_Dev.Rows[_index].Cells["imp_dev_id"].Tag = row["wm_id"];
                dgv_Imp_Dev.Rows[_index].Cells["imp_dev_id"].Value = row["imp_id"];
                dgv_Imp_Dev.Rows[_index].Cells["imp_dev_unit"].Tag = row["dd_id"];
                dgv_Imp_Dev.Rows[_index].Cells["imp_dev_unit"].Value = row["dd_name"];
                dgv_Imp_Dev.Rows[_index].Cells["imp_dev_code"].Value = row["imp_code"];
                dgv_Imp_Dev.Rows[_index].Cells["imp_dev_name"].Value = row["imp_name"];
                dgv_Imp_Dev.Rows[_index].Cells["imp_dev_fileAmount"].Value = GetFileAmountById(row["imp_id"], ControlType.Default);
                dgv_Imp_Dev.Rows[_index].Cells["imp_dev_qtAmount"].Value = row["wm_ticker"];
            }

            DataGridViewStyleHelper.SetAlignWithCenter(dgv_Imp_Dev, new string[] { "imp_dev_fileAmount", "imp_dev_qtAmount" });
            dgv_Imp_Dev.Columns["imp_dev_id"].Visible = false;
        }

        /// <summary>
        /// 待质检 - 项目/课题列表
        /// </summary>
        private void LoadProjectList()
        {
            DataGridViewStyleHelper.ResetDataGridView(dgv_Project, true);
            dgv_Project.Columns.AddRange(new DataGridViewColumn[]
            {
                new DataGridViewTextBoxColumn(){Name = "pro_id"},
                new DataGridViewTextBoxColumn(){Name = "pro_unit", HeaderText = "来源单位", FillWeight = 12},
                new DataGridViewTextBoxColumn(){Name = "pro_code", HeaderText = "项目/课题编号", FillWeight = 10},
                new DataGridViewTextBoxColumn(){Name = "pro_name", HeaderText = "项目/课题名称", FillWeight = 15},
                new DataGridViewTextBoxColumn(){Name = "pro_subAmount", HeaderText = "课题/子课题数", FillWeight = 8},
                new DataGridViewTextBoxColumn(){Name = "pro_fileAmount", HeaderText = "文件数", FillWeight = 5},
                new DataGridViewTextBoxColumn(){Name = "pro_qtAmount", HeaderText = "质检次数", FillWeight = 5 },
                new DataGridViewButtonColumn(){Name = "pro_control", HeaderText = "操作", FillWeight = 5, Text = "质检", UseColumnTextForButtonValue = true},
                new DataGridViewTextBoxColumn(){Name = "pro_via", HeaderText = "数据途径", FillWeight = 6},
            });

            DataTable table = new DataTable();
            table.Columns.AddRange(new DataColumn[]
            {
                new DataColumn("dd_name"),
                new DataColumn("wm_id"),
                new DataColumn("pi_id"),
                new DataColumn("pi_code"),
                new DataColumn("pi_name"),
                new DataColumn("wm_ticker")
            });
            string querySql = "SELECT dd.dd_name, wm.wm_id, pi.pi_id, pi.pi_code, pi.pi_name, wm.wm_ticker FROM project_info pi " +
                "LEFT JOIN work_myreg wm ON wm.wm_obj_id = pi.pi_id " +
                "LEFT JOIN work_registration wr ON wr.wr_id = wm.wr_id " +
                "LEFT JOIN transfer_registration_pc trp ON wr.trp_id = trp.trp_id " +
                "LEFT JOIN data_dictionary dd ON dd.dd_id = trp.com_id " +
                "WHERE wm.wm_type = 2 AND wm.wm_status = 1 " +
                "UNION ALL " +
                "SELECT dd.dd_name, wm.wm_id, ti.ti_id, ti.ti_code, ti.ti_name, wm.wm_ticker FROM topic_info ti " +
                "LEFT JOIN work_myreg wm ON wm.wm_obj_id = ti.ti_id " +
                "LEFT JOIN work_registration wr ON wr.wr_id = wm.wr_id " +
                "LEFT JOIN transfer_registration_pc trp ON wr.trp_id = trp.trp_id " +
                "LEFT JOIN data_dictionary dd ON dd.dd_id = trp.com_id " +
                "WHERE wm.wm_type = 3 AND wm.wm_status = 1 AND ti.trc_id IS NOT NULL;";
            DataTable _table = SqlHelper.ExecuteQuery(querySql);
            foreach(DataRow row in _table.Rows)
                table.ImportRow(row);

            foreach(DataRow row in table.Rows)
            {
                int _index = dgv_Project.Rows.Add();
                dgv_Project.Rows[_index].Cells["pro_id"].Tag = row["wm_id"];
                dgv_Project.Rows[_index].Cells["pro_id"].Value = row["pi_id"];
                dgv_Project.Rows[_index].Cells["pro_unit"].Value = row["dd_name"]; 
                dgv_Project.Rows[_index].Cells["pro_code"].Value = row["pi_code"];
                dgv_Project.Rows[_index].Cells["pro_name"].Value = row["pi_name"];
                dgv_Project.Rows[_index].Cells["pro_subAmount"].Value = GetSubAmount(row["pi_id"]);
                dgv_Project.Rows[_index].Cells["pro_fileAmount"].Value = GetFileAmountById(row["pi_id"], ControlType.Default);
                dgv_Project.Rows[_index].Cells["pro_qtAmount"].Value = row["wm_ticker"];
            }

            DataGridViewStyleHelper.SetAlignWithCenter(dgv_Project, new string[] { "pro_fileAmount", "pro_qtAmount", "pro_subAmount" });
            dgv_Project.Columns["pro_id"].Visible = false;
        }

        private object GetSubAmount(object pId) => SqlHelper.ExecuteCountQuery($"SELECT COUNT(si_id)+(SELECT COUNT(ti_id) FROM topic_info WHERE ti_obj_id='{pId}') FROM subject_info WHERE si_obj_id='{pId}'");

        private object GetValue(object obj) => (obj == null || string.IsNullOrEmpty(obj.ToString())) ? null : obj.ToString();
        
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
                    if(MessageBox.Show("确定要质检当前选中数据吗？", "领取确认", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                    {
                        object wmid = dgv_Imp.Rows[e.RowIndex].Cells["imp_id"].Tag;
                        SqlHelper.ExecuteNonQuery($"UPDATE work_myreg SET wm_status='{(int)QualityStatus.QualitySuccess}', wm_accepter='{UserHelper.GetInstance().User.UserKey}', wm_ticker+=1 WHERE wm_id='{wmid}'");
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
                    if(MessageBox.Show("确定要质检当前选中数据吗？", "领取确认", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                    {
                        object wmid = dgv_Imp_Dev.Rows[e.RowIndex].Cells["imp_dev_id"].Tag;
                        SqlHelper.ExecuteNonQuery($"UPDATE work_myreg SET wm_status='{(int)QualityStatus.QualitySuccess}', wm_accepter='{UserHelper.GetInstance().User.UserKey}', wm_ticker+=1 WHERE wm_id='{wmid}'");
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
                    if(MessageBox.Show("确定要质检当前选中数据吗？", "领取确认", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                    {
                        object wmid = dgv_Project.Rows[e.RowIndex].Cells["pro_id"].Tag;
                        SqlHelper.ExecuteNonQuery($"UPDATE work_myreg SET wm_status='{(int)QualityStatus.QualitySuccess}', wm_accepter='{UserHelper.GetInstance().User.UserKey}', wm_ticker+=1 WHERE wm_id='{wmid}'");
                        dgv_Project.Rows.RemoveAt(e.RowIndex);
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
                new DataGridViewTextBoxColumn(){ Name = "subUnitName", HeaderText = "来源单位", FillWeight = 15},
                new DataGridViewTextBoxColumn(){ Name = "subCode", HeaderText = "课题/子课题编号", FillWeight = 20},
                new DataGridViewTextBoxColumn(){ Name = "subName", HeaderText = "课题/子课题名称", FillWeight = 25},
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
        /// 我的质检 - 单元格 点击事件
        /// </summary>
        private void Dgv_MyReg_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex != -1 && e.ColumnIndex != -1)
            {
                object wmid = dgv_MyReg.Rows[e.RowIndex].Tag;
                object impid = dgv_MyReg.Rows[e.RowIndex].Cells["mr_id"].Value;
                object columnName = dgv_MyReg.Columns[e.ColumnIndex].Name;
                //编辑
                if("mr_edit".Equals(columnName))
                {
                    int index = (int)dgv_MyReg.Rows[e.RowIndex].Cells["mr_id"].Tag;
                    if(index == 0)
                    {
                        
                        
                        Frm_MyWorkQT frm = new Frm_MyWorkQT(WorkType.PaperWork_Imp, impid, wmid, ControlType.Imp);

                        if(frm.ShowDialog() == DialogResult.OK)
                        {
                            int _index = SqlHelper.ExecuteCountQuery($"SELECT COUNT(*) FROM imp_info WHERE imp_id='{impid}' AND imp_submit_status=1");
                            if(_index > 0)
                            {
                                if(MessageBox.Show("此数据包含返工信息，是否执行返工操作？", "温馨提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk) == DialogResult.OK)
                                {
                                    SqlHelper.ExecuteNonQuery($"UPDATE work_myreg SET wm_status={(int)QualityStatus.QualityBack} WHERE wm_id='{wmid}'");
                                    LoadMyRegList();
                                }
                            }
                            LoadMyRegList();
                        }
                    }
                    else if(index == 1)
                    {
                        Frm_MyWorkQT frm = new Frm_MyWorkQT(WorkType.PaperWork, impid, wmid, ControlType.Special);
                        if(frm.ShowDialog() == DialogResult.OK)
                        {
                            int _index = SqlHelper.ExecuteCountQuery($"SELECT COUNT(*) FROM imp_dev_info WHERE imp_id='{impid}' AND imp_submit_status=1");
                            if(_index > 0)
                            {
                                if(MessageBox.Show("此数据包含返工信息，是否执行返工操作？", "温馨提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk) == DialogResult.OK)
                                {
                                    SqlHelper.ExecuteNonQuery($"UPDATE work_myreg SET wm_status={(int)QualityStatus.QualityBack} WHERE wm_id='{wmid}'");
                                    LoadMyRegList();
                                }
                            }
                            LoadMyRegList();
                        }
                    }
                    else if(index == 2 || index ==3)
                    {
                        object piid = dgv_MyReg.Rows[e.RowIndex].Cells["mr_id"].Value;
                        Frm_MyWorkQT frm = new Frm_MyWorkQT(WorkType.ProjectWork, piid, null, ControlType.Project);
                        if(frm.ShowDialog() == DialogResult.OK)
                        {
                            if(HaveBacked(piid))
                            {
                                if(MessageBox.Show("此数据有返工记录，是否执行返工操作？", "温馨提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk) == DialogResult.OK)
                                {
                                    SqlHelper.ExecuteNonQuery($"UPDATE work_myreg SET wm_status={(int)QualityStatus.QualityBack} WHERE wm_id='{wmid}'");
                                    LoadMyRegList();
                                }
                            }
                        }
                    }
                    else if(index == -1)
                    {
                        object piid = dgv_MyReg.Rows[e.RowIndex].Cells["mr_id"].Value;
                        Frm_MyWorkQT frm = new Frm_MyWorkQT(WorkType.PaperWork, piid, null, ControlType.Plan);
                        if(frm.ShowDialog() == DialogResult.OK)
                        {
                            if(HaveBacked(piid))
                            {
                                if(MessageBox.Show("此数据有返工记录，是否执行返工操作？", "温馨提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk) == DialogResult.OK)
                                {
                                    SqlHelper.ExecuteNonQuery($"UPDATE work_myreg SET wm_status={(int)QualityStatus.QualityBack} WHERE wm_id='{wmid}'");
                                    LoadMyRegList();
                                }
                            }
                        }
                    }
                }
                //完成质检
                else if("mr_submit".Equals(columnName))
                {
                    if(MessageBox.Show("确定要完成对当前数据的质检吗？", "确认提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk) == DialogResult.OK)
                    {
                        SqlHelper.ExecuteNonQuery($"UPDATE work_myreg SET wm_status='{(int)QualityStatus.QualityFinish}' WHERE wm_id='{wmid}'");
                    }
                    LoadMyRegList();
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
    }
}
