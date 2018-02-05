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
                Text = "著录加工",
                Image = Resources.pic6,
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
            List<DataTable> resultList = GetObjectListById(null);

            dgv_Plan.Columns.Add("wt_id", "主键");
            dgv_Plan.Columns.Add("wt_code", "编号");
            dgv_Plan.Columns.Add("wt_name", "名称");
            dgv_Plan.Columns.Add("wt_unit", "来源单位");
            dgv_Plan.Columns.Add("wt_edit", "操作");
            for(int i = 0; i < resultList.Count; i++)
            {
                DataRow row = resultList[i].Rows[0];
                int index = dgv_Plan.Rows.Add();
                dgv_Plan.Rows[index].Cells["wt_id"].Value = row[0];
                dgv_Plan.Rows[index].Cells["wt_code"].Value = row[2];
                dgv_Plan.Rows[index].Cells["wt_name"].Value = row[3];
                dgv_Plan.Rows[index].Cells["wt_unit"].Value = row[4];
                dgv_Plan.Rows[index].Cells["wt_edit"].Value = "质检";
            }
            dgv_Plan.Columns["wt_id"].Visible = false;

            DataGridViewStyleHelper.SetLinkStyle(dgv_Plan, new string[] { "wt_edit" }, false);

            List<KeyValuePair<string, int>> keyValue = new List<KeyValuePair<string, int>>();
            keyValue.Add(new KeyValuePair<string, int>("wt_name", 200));
            keyValue.Add(new KeyValuePair<string, int>("wt_edit", 100));
            DataGridViewStyleHelper.SetWidth(dgv_Plan, keyValue);
        }
        /// <summary>
        /// 根据加工登记主键获取对应项目/课题信息
        /// </summary>
        /// <param name="objid"></param>
        /// <returns></returns>
        private static List<DataTable> GetObjectListById(object objid)
        {
            string querySql = $" SELECT wr_id, wr_type,wr_obj_id FROM work_registration wr LEFT JOIN(" +
                            $"SELECT trp_id, cs_id FROM transfer_registration_pc LEFT JOIN company_source ON com_id = cs_id) tb " +
                            $"ON wr.trp_id = tb.trp_id WHERE wr_submit_status={(int)ObjectSubmitStatus.SubmitSuccess}";
            if(objid != null)
                querySql += $" AND wr_id='{objid}'";
            else
                querySql += $" AND wr_receive_status={(int)ReceiveStatus.ReceiveSuccess} AND wr_receive_status={(int)ReceiveStatus.NonReceive}";
            List<object[]> list = SqlHelper.ExecuteColumnsQuery(querySql, 3);
            List<DataTable> resultList = new List<DataTable>();
            for(int i = 0; i < list.Count; i++)
            {
                WorkType type = (WorkType)list[i][1];
                object id = list[i][2];
                string _querySql = null;
                switch(type)
                {
                    case WorkType.PaperWork:
                        _querySql = $"SELECT '{list[i][0]}','{id}',trp_code,trp_name,cs_name FROM transfer_registration_pc LEFT JOIN " +
                            $"company_source ON com_id = cs_id WHERE trp_id='{id}'";
                        break;
                    case WorkType.CDWork:
                        _querySql = $"SELECT '{list[i][0]}','{id}',trc_code,trc_name,cs_name FROM transfer_registraion_cd trc LEFT JOIN(" +
                            $"SELECT trp_id, cs_name FROM transfer_registration_pc LEFT JOIN company_source ON com_id = cs_id ) tb1 " +
                            $"ON tb1.trp_id = trc.trp_id WHERE trc_id='{id}'";
                        break;
                    case WorkType.ProjectWork:
                        _querySql = $"SELECT '{list[i][0]}','{id}',pi_code,pi_name,cs_name FROM project_info pi " +
                            $"LEFT JOIN(SELECT trc_id, cs_name FROM transfer_registraion_cd trc " +
                            $"LEFT JOIN(SELECT trp_id, cs_name FROM transfer_registration_pc trp " +
                            $"LEFT JOIN company_source ON cs_id = trp.com_id)tb1 ON trc.trp_id = tb1.trp_id) tb2 ON tb2.trc_id = pi.trc_id " +
                            $"WHERE pi_id='{id}'";
                        break;
                    case WorkType.SubjectWork:
                        _querySql = $"SELECT '{list[i][0]}','{id}',si_code,si_name,cs_name FROM subject_info si LEFT JOIN(" +
                           $"SELECT pi_id,cs_name FROM project_info pi " +
                           $"LEFT JOIN(SELECT trc_id, cs_name FROM transfer_registraion_cd trc " +
                           $"LEFT JOIN(SELECT trp_id, cs_name FROM transfer_registration_pc trp " +
                           $"LEFT JOIN company_source ON cs_id = trp.com_id)tb1 ON trc.trp_id = tb1.trp_id) tb2 ON tb2.trc_id = pi.trc_id " +
                           $") tb3 ON tb3.pi_id = si.pi_id WHERE si.si_id='{id}'";
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
        /// 单元格点击事件
        /// </summary>
        private void Dgv_Plan_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex != -1 && e.ColumnIndex != -1)
            {
                object columnName = dgv_Plan.Columns[e.ColumnIndex].Name;
                //待质检 - 质检
                if("wt_edit".Equals(columnName))
                {
                    object objid = dgv_Plan.Rows[e.RowIndex].Cells["wt_id"].Value;
                    string updateSql = $"UPDATE work_registration SET wr_receive_status={(int)ReceiveStatus.ReceiveSuccess} WHERE wr_id='{objid}'";
                    SqlHelper.ExecuteNonQuery(updateSql);

                    string primaryKey = Guid.NewGuid().ToString();
                    string insertSql = $"INSERT INTO work_myreg(wm_id,wr_id,wm_status,wm_user)" +
                        $" VALUES('{primaryKey}','{objid}','{(int)QualityStatus.NonQuality}','{UserHelper.GetInstance().User.UserKey}')";
                    SqlHelper.ExecuteNonQuery(insertSql);

                    LoadMyRegList();
                }
                //我的质检 -  编辑
                else if("mr_edit".Equals(columnName))
                {
                    object objid = dgv_Plan.Rows[e.RowIndex].Cells["mr_id"].Value;
                    object planId = GetRootId(objid, WorkType.ProjectWork);
                    if(planId != null)
                        new Frm_MyWorkQT(WorkType.ProjectWork, planId, ControlType.Default, false).ShowDialog();
                    else
                    {
                        planId = GetRootId(objid, WorkType.SubjectWork);
                        if(planId != null)
                            new Frm_MyWorkQT(WorkType.SubjectWork, planId, ControlType.Default, false).ShowDialog();
                        else
                            MessageBox.Show("未找到此项目/课题所属计划。");
                    }
                }
                //质检 - 提交（返工）
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
        /// 获取指定项目/课题获取其所属计划ID
        /// </summary>
        private object GetRootId(object objId, WorkType type)
        {
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

            List<object[]> wmIds = SqlHelper.ExecuteColumnsQuery($"SELECT wm_id, wr_id FROM work_myreg WHERE wm_user='{UserHelper.GetInstance().User.UserKey}' AND wm_status='{(int)QualityStatus.NonQuality}'", 2);
            for(int i = 0; i < wmIds.Count; i++)
            {
                List<DataTable> list = GetObjectListById(wmIds[i][1]);
                for(int j = 0; j < list.Count; j++)
                {
                    DataRow row = list[j].Rows[0];
                    int index = dgv_Plan.Rows.Add();
                    dgv_Plan.Rows[index].Cells["mr_id"].Tag = wmIds[i][0];
                    dgv_Plan.Rows[index].Cells["mr_id"].Value = row[1];
                    dgv_Plan.Rows[index].Cells["mr_unit"].Value = row[4];
                    dgv_Plan.Rows[index].Cells["mr_code"].Value = row[2];
                    dgv_Plan.Rows[index].Cells["mr_name"].Value = row[3];
                    dgv_Plan.Rows[index].Cells["mr_fileamount"].Value = 0;

                    dgv_Plan.Rows[index].Cells["mr_edit"].Value = "编辑";
                    dgv_Plan.Rows[index].Cells["mr_submit"].Value = "返工/提交";
                }
            }

            dgv_Plan.Columns["mr_id"].Visible = false;

            List<KeyValuePair<string, int>> keyValueList = new List<KeyValuePair<string, int>>();
            keyValueList.Add(new KeyValuePair<string, int>("mr_name", 250));
            DataGridViewStyleHelper.SetWidth(dgv_Plan, keyValueList);

            DataGridViewStyleHelper.SetLinkStyle(dgv_Plan, new string[] { "mr_edit", "mr_submit" }, false);
        }
    }
}
