using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using 科技计划项目档案数据采集管理系统.Properties;

namespace 科技计划项目档案数据采集管理系统
{
    public partial class Frm_QT : Form
    {
        public Frm_QT()
        {
            InitializeComponent();
            InitialForm();
        }

        private void InitialForm()
        {
        }

        private void Frm_QT_Load(object sender, EventArgs e)
        {
            LoadLeftMenu();
            LoadWaitQTList();
            dgv_Plan.ColumnHeadersDefaultCellStyle = DataGridViewStyleHelper.GetHeaderStyle();
        }
        /// <summary>
        /// 加载左侧菜单栏
        /// </summary>
        private void LoadLeftMenu()
        {
            List<CreateKyoPanel.KyoPanel> list = new List<CreateKyoPanel.KyoPanel>();
            list.Add(new CreateKyoPanel.KyoPanel()
            {
                Name = "QT_ZLJG",
                Text = "档案质检",
                Image = Resources.pic1,
                HasNext = true
            });
            CreateKyoPanel.SetPanel(pal_LeftMenu, list);

            list.Clear();
            list.AddRange(new CreateKyoPanel.KyoPanel[] {
                new CreateKyoPanel.KyoPanel()
                {
                     Name = "QT_Login",
                     Text = "质检登记",
                     HasNext = false
                },new CreateKyoPanel.KyoPanel()
                {
                     Name = "QT_Myqt",
                     Text = "我的质检",
                     HasNext = false
                }
            });
            CreateKyoPanel.SetSubPanel(pal_LeftMenu.Controls.Find("QT_ZLJG", false)[0] as Panel, list, Sub_Click);
        }
        /// <summary>
        /// 二级菜单点击事件
        /// </summary>
        private void Sub_Click(object sender, EventArgs e)
        {
            Panel panel = null;
            if(sender is Panel)
                panel = sender as Panel;
            else
                panel = (sender as Control).Parent as Panel;

            if("QT_Login".Equals(panel.Name))
            {
                LoadWaitQTList();
            }
            else if("QT_Myqt".Equals(panel.Name))
            {
                LoadMyRegList();
            }
        }
        /// <summary>
        /// 加载待质检列表
        /// </summary>
        private void LoadWaitQTList()
        {
            DataGridViewStyleHelper.ResetDataGridView(dgv_Plan);
            DataGridViewStyleHelper.ResetDataGridView(dgv_Project);

            dgv_Plan.Columns.Add("plan_id", "主键");
            dgv_Plan.Columns.Add("plan_code", "编号");
            dgv_Plan.Columns.Add("plan_name", "名称");
            dgv_Plan.Columns.Add("plan_unit", "来源单位");
            dgv_Plan.Columns.Add("plan_edit", "操作");

            dgv_Project.Columns.Add("project_id", "主键");
            dgv_Project.Columns.Add("project_unit", "来源单位");
            dgv_Project.Columns.Add("project_code", "项目/课题编号");
            dgv_Project.Columns.Add("project_name", "项目/课题名称");
            dgv_Project.Columns.Add("project_subamount", "课题/子课题数");
            dgv_Project.Columns.Add("project_fileamount", "文件数");
            dgv_Project.Columns.Add("project_edit", "操作");

            List<DataTable> resultList = GetObjectListById(null);
            for(int i = 0; i < resultList.Count; i++)
            {
                if(resultList[i].Rows.Count > 0)
                {
                    DataRow row = resultList[i].Rows[0];
                    WorkType type = (WorkType)Convert.ToInt32(row[1]);
                    if(type == WorkType.PaperWork || type == WorkType.CDWork)
                    {
                        int index = dgv_Plan.Rows.Add();
                        dgv_Plan.Rows[index].Cells["plan_id"].Value = row[0];
                        dgv_Plan.Rows[index].Cells["plan_id"].Tag = row[2];
                        dgv_Plan.Rows[index].Cells["plan_code"].Value = row[3];
                        dgv_Plan.Rows[index].Cells["plan_name"].Value = row[4];
                        dgv_Plan.Rows[index].Cells["plan_unit"].Value = row[5];
                        dgv_Plan.Rows[index].Cells["plan_edit"].Value = "质检";
                    }
                    else if(type == WorkType.ProjectWork)
                    {
                        int index = dgv_Project.Rows.Add();
                        dgv_Project.Rows[index].Cells["project_id"].Value = row[0];
                        dgv_Project.Rows[index].Cells["project_id"].Tag = row[2];
                        dgv_Project.Rows[index].Cells["project_code"].Value = row[3];
                        dgv_Project.Rows[index].Cells["project_name"].Value = row[4];
                        dgv_Project.Rows[index].Cells["project_unit"].Value = row[5];
                        dgv_Project.Rows[index].Cells["project_subamount"].Value = GetSubjectAmountByProjectId(row[0]);
                        dgv_Project.Rows[index].Cells["project_fileamount"].Value = GetFileAmountByProjectId(row[0]);
                        dgv_Project.Rows[index].Cells["project_edit"].Value = "质检";
                    }
                    else if(type == WorkType.SubjectWork)
                    {
                        int index = dgv_Project.Rows.Add();
                        dgv_Project.Rows[index].Cells["project_id"].Value = row[0];
                        dgv_Project.Rows[index].Cells["project_id"].Tag = row[2];
                        dgv_Project.Rows[index].Cells["project_code"].Value = row[3];
                        dgv_Project.Rows[index].Cells["project_name"].Value = row[4];
                        dgv_Project.Rows[index].Cells["project_unit"].Value = row[5];
                        dgv_Project.Rows[index].Cells["project_subamount"].Value = 0;
                        dgv_Project.Rows[index].Cells["project_fileamount"].Value = 0;
                        dgv_Project.Rows[index].Cells["project_edit"].Value = "质检";
                    }
                }
            }
            dgv_Plan.Columns["plan_id"].Visible = false;
            dgv_Project.Columns["project_id"].Visible = false;

            DataGridViewStyleHelper.SetLinkStyle(dgv_Plan, new string[] { "plan_edit" }, false);
            DataGridViewStyleHelper.SetLinkStyle(dgv_Project, new string[] { "project_edit" }, false);

            List<KeyValuePair<string, int>> keyValue = new List<KeyValuePair<string, int>>
            {
                new KeyValuePair<string, int>("plan_name", 200),
                new KeyValuePair<string, int>("plan_edit", 100)
            };
            DataGridViewStyleHelper.SetWidth(dgv_Plan, keyValue);
            List<KeyValuePair<string, int>> _keyValue = new List<KeyValuePair<string, int>>
            {
                new KeyValuePair<string, int>("project_name", 200),
                new KeyValuePair<string, int>("project_edit", 100)
            };
            DataGridViewStyleHelper.SetWidth(dgv_Project, _keyValue);
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
                    case WorkType.SubjectWork:
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
        /// 【计划/专项】单元格点击事件
        /// </summary>
        private void Dgv_Plan_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex != -1 && e.ColumnIndex != -1)
            {
                object columnName = dgv_Plan.Columns[e.ColumnIndex].Name;
                //待质检 - 【计划/专项】质检
                if("plan_edit".Equals(columnName))
                {
                    object objid = dgv_Plan.Rows[e.RowIndex].Cells["plan_id"].Value;
                    SqlHelper.ExecuteNonQuery($"UPDATE work_registration SET wr_receive_status={(int)ReceiveStatus.ReceiveSuccess} WHERE wr_id='{objid}'");
                    SqlHelper.ExecuteNonQuery($"INSERT INTO work_myreg(wm_id,wr_id,wm_status,wm_user)" +
                        $" VALUES('{Guid.NewGuid().ToString()}','{objid}','{(int)QualityStatus.NonQuality}','{UserHelper.GetInstance().User.UserKey}')");
                    LoadMyRegList();
                }
                //我的质检 -  编辑
                else if("mr_edit".Equals(columnName))
                {
                    object objid = dgv_Plan.Rows[e.RowIndex].Cells["mr_id"].Value;
                    object wrid = dgv_Plan.Rows[e.RowIndex].Cells["mr_code"].Tag;
                    WorkType type = (WorkType)Convert.ToInt32(SqlHelper.ExecuteOnlyOneQuery($"SELECT wr_type FROM work_registration WHERE wr_id='{wrid}'"));
                    object planId = GetRootId(objid, type);
                    if(planId != null)
                        new Frm_MyWorkQT(type, objid, ControlType.Default, false).ShowDialog();
                    else
                    {
                        planId = GetRootId(objid, WorkType.SubjectWork);
                        if(planId != null)
                            new Frm_MyWorkQT(WorkType.SubjectWork, planId, ControlType.Default, false).ShowDialog();
                        else
                            MessageBox.Show("未找到此项目/课题所属计划。");
                    }
                }
                //我的质检 - 提交（返工）
                else if("mr_submit".Equals(columnName))
                {
                    object wmid = dgv_Plan.Rows[e.RowIndex].Cells["mr_id"].Tag;
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
        /// 【项目/课题】单元格点击事件
        /// </summary>
        private void Dgv_Project_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex!=-1 && e.ColumnIndex != -1)
            {
                object columnName = dgv_Project.Columns[e.ColumnIndex].Name;
                //领取此条质检
                if("project_edit".Equals(columnName))
                {
                    //同时将当前项目|课题下所有子课题领取
                    object objid = dgv_Project.Rows[e.RowIndex].Cells["project_id"].Value;
                    GetAllProjectByPid(objid);
                    //更新加工记录表状态
                    SqlHelper.ExecuteNonQuery($"UPDATE work_registration SET wr_receive_status={(int)ReceiveStatus.ReceiveSuccess} WHERE wr_id='{objid}'");
                    //添加我的质检记录
                    SqlHelper.ExecuteNonQuery($"INSERT INTO work_myreg(wm_id,wr_id,wm_status,wm_user)" +
                        $" VALUES('{Guid.NewGuid().ToString()}','{objid}','{(int)QualityStatus.NonQuality}','{UserHelper.GetInstance().User.UserKey}')");

                    LoadMyRegList();
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
            else if(type == WorkType.SubjectWork)
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
            DataGridViewStyleHelper.ResetDataGridView(dgv_Plan);

            dgv_Plan.Columns.Add("mr_id", string.Empty);
            dgv_Plan.Columns.Add("mr_unit", "来源单位");
            dgv_Plan.Columns.Add("mr_code", "项目/课题编号");
            dgv_Plan.Columns.Add("mr_name", "项目/课题名称");
            dgv_Plan.Columns.Add("mr_fileamount", "文件数");
            dgv_Plan.Columns.Add("mr_edit", "编辑");
            dgv_Plan.Columns.Add("mr_submit", "提交");

            List<object[]> wmIds = SqlHelper.ExecuteColumnsQuery($"SELECT wm_id, wr_id FROM work_myreg WHERE wm_user='{UserHelper.GetInstance().User.UserKey}' " +
                $"AND wm_status='{(int)QualityStatus.NonQuality}'", 2);
            for(int i = 0; i < wmIds.Count; i++)
            {
                List<DataTable> list = GetObjectListById(wmIds[i][1]);
                for(int j = 0; j < list.Count; j++)
                {
                    if(list[j].Rows.Count > 0)
                    {
                        DataRow row = list[j].Rows[0];
                        int index = dgv_Plan.Rows.Add();
                        dgv_Plan.Rows[index].Cells["mr_id"].Tag = wmIds[i][0];
                        dgv_Plan.Rows[index].Cells["mr_code"].Tag = row[0];
                        dgv_Plan.Rows[index].Cells["mr_id"].Value = row[2];
                        dgv_Plan.Rows[index].Cells["mr_code"].Value = row[3];
                        dgv_Plan.Rows[index].Cells["mr_name"].Value = row[4];
                        dgv_Plan.Rows[index].Cells["mr_unit"].Value = row[5];
                        dgv_Plan.Rows[index].Cells["mr_fileamount"].Value = GetFileAmountByPID(row[2]);
                        dgv_Plan.Rows[index].Cells["mr_edit"].Value = "编辑";
                        dgv_Plan.Rows[index].Cells["mr_submit"].Value = "返工/提交";
                    }
                }
            }

            dgv_Plan.Columns["mr_id"].Visible = false;

            DataGridViewStyleHelper.SetAlignWithCenter(dgv_Plan, new string[] { "mr_fileamount" });
            List<KeyValuePair<string, int>> keyValueList = new List<KeyValuePair<string, int>>();
            keyValueList.Add(new KeyValuePair<string, int>("mr_name", 250));
            keyValueList.Add(new KeyValuePair<string, int>("mr_fileamount", 80));
            keyValueList.Add(new KeyValuePair<string, int>("mr_edit", 100));
            keyValueList.Add(new KeyValuePair<string, int>("mr_submit", 100));
            DataGridViewStyleHelper.SetWidth(dgv_Plan, keyValueList);

            DataGridViewStyleHelper.SetLinkStyle(dgv_Plan, new string[] { "mr_edit", "mr_submit" }, false);
        }
        /// <summary>
        /// 根据计划ID获取其下所有文件总数
        /// </summary>
        private object GetFileAmountByPID(object pid)
        {
            object objid = SqlHelper.ExecuteOnlyOneQuery($"SELECT pi_id FROM project_info WHERE trc_id='{pid}'");
            if(objid == null)
                objid = SqlHelper.ExecuteOnlyOneQuery($"SELECT pi_id FROM project_info WHERE pi_obj_id='{pid}'");
            int totalAmount = Convert.ToInt32(SqlHelper.ExecuteOnlyOneQuery($"SELECT COUNT(pfl_id) FROM processing_file_list WHERE pfl_obj_id='{objid}'"));
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
    }
}
