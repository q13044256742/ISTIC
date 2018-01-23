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
            dgv_WorkingLog.ColumnHeadersDefaultCellStyle = DataGridViewStyleHelper.GetHeaderStyle();
            
            //默认来源单位
            LoadCompanyList();
        }

        /// <summary>
        /// 加载来源单位ComboBox下拉表
        /// </summary>
        private void LoadCompanyList()
        {
            string querySql = "SELECT cs_id,cs_name FROM company_source ORDER BY sorting ASC";
            DataTable table = SqlHelper.ExecuteQuery(querySql);
            DataRow dataRow = table.NewRow();
            dataRow["cs_id"] = "all";
            dataRow["cs_name"] = "全部来源单位";
            table.Rows.InsertAt(dataRow, 0);
            cbo_CompanyList.DataSource = table;
            cbo_CompanyList.DisplayMember = "cs_name";
            cbo_CompanyList.ValueMember = "cs_id";
            cbo_CompanyList.SelectedIndex = 0;
        }

        /// <summary>
        /// 加载批次列表
        /// </summary>
        /// <param name="querySql">指定的查询语句</param>
        /// <param name="csid">来源单位ID</param>
        private void LoadPCList(StringBuilder querySql, object csid)
        {
            DataGridViewStyleHelper.ResetDataGridView(dgv_WorkingLog);

            DataTable dataTable = null;
            if (querySql == null)
            {
                //加载实物登记数据【默认加载状态为2（已提交）的数据】
                querySql = new StringBuilder("SELECT pc.trp_id, cs_name, trp_name, trp_code, trp_cd_amount");
                querySql.Append(" FROM transfer_registration_pc pc LEFT JOIN company_source cs ON pc.com_id = cs.cs_id");
                querySql.Append($" WHERE trp_submit_status={(int)SubmitStatus.SubmitSuccess} AND trp_work_status={(int)WorkStatus.NonWork}");
                if (csid != null)
                    querySql.Append($" AND cs.cs_id='{csid}'");
            }
            dataTable = SqlHelper.ExecuteQuery(querySql.ToString());
            //将数据源转化成DataGridView表数据
            dgv_WorkingLog.Columns.Add("trp_id", "主键");
            dgv_WorkingLog.Columns.Add("cs_name", "来源单位");
            dgv_WorkingLog.Columns.Add("trp_name", "批次名称");
            dgv_WorkingLog.Columns.Add("trp_code", "批次编号");
            dgv_WorkingLog.Columns.Add("trp_finishtime", "完成时间");
            dgv_WorkingLog.Columns.Add("trp_cd_amount", "光盘数");
            dgv_WorkingLog.Columns.Add("trp_control", "操作");
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                DataRow row = dataTable.Rows[i];
                int index = dgv_WorkingLog.Rows.Add();
                dgv_WorkingLog.Rows[index].Cells["trp_id"].Value = row["trp_id"];
                dgv_WorkingLog.Rows[index].Cells["cs_name"].Value = row["cs_name"];
                dgv_WorkingLog.Rows[index].Cells["trp_name"].Value = row["trp_name"];
                dgv_WorkingLog.Rows[index].Cells["trp_code"].Value = row["trp_code"];
                dgv_WorkingLog.Rows[index].Cells["trp_finishtime"].Value = null;//完成时间字段待定
                dgv_WorkingLog.Rows[index].Cells["trp_cd_amount"].Value = row["trp_cd_amount"];
                dgv_WorkingLog.Rows[index].Cells["trp_control"].Value = "加工";
            }
            //设置最小列宽
            dgv_WorkingLog.Columns["cs_name"].MinimumWidth = 200;
            dgv_WorkingLog.Columns["trp_name"].MinimumWidth = 250;
            dgv_WorkingLog.Columns["trp_cd_amount"].MinimumWidth = 90;
            dgv_WorkingLog.Columns["trp_control"].MinimumWidth = 100;
            //设置链接按钮样式
            DataGridViewStyleHelper.SetAlignWithCenter(dgv_WorkingLog, new string[] { "trp_cd_amount", "trp_control" });
            DataGridViewStyleHelper.SetLinkStyle(dgv_WorkingLog, new string[] { "trp_cd_amount", "trp_control" }, true);

            dgv_WorkingLog.Columns["trp_id"].Visible = false;
            LastIdLog = new string[] { string.Empty, $"PC_{csid}" };

            dgv_WorkingLog.Tag = "PC_LIST";
        }

        /// <summary>
        /// 加载左侧菜单
        /// </summary>
        private void InitialLeftMenu()
        {
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
                    Text="已返工",
                    HasNext = false
                }
            });
            Panel parentPanel = pal_LeftMenu.Controls.Find("CG", false)[0] as Panel;
            CreateKyoPanel.SetSubPanel(parentPanel, list, Sub_Menu_Click);
        }

        /// <summary>
        /// 二级菜单点击事件（加工登记/加工中/已返工）
        /// </summary>
        private void Sub_Menu_Click(object sender, EventArgs e)
        {
            Panel panel = null;
            if (sender is Panel) panel = sender as Panel;
            else if (sender is Label) panel = (sender as Label).Parent as Panel;
            if ("CG_LOGIN".Equals(panel.Name))//加工登记
            {
                LoadPCList(null, null);
                cbo_CompanyList.SelectedIndex = 0;
            }else if ("CG_WORK_ING".Equals(panel.Name))//加工中
            {
                LoadWorkList(null, WorkStatus.WorkSuccess);
            }
            else if ("CG_WORK_ED".Equals(panel.Name))//已返工
            {

            }
        }

        /// <summary>
        /// 加载-加工中列表
        /// </summary>
        /// <param name="unitId">来源单位（默认全部）</param>
        /// <param name="nonWork">加工状态</param>
        private void LoadWorkList(object unitId, WorkStatus workStatus)
        {
            DataGridViewStyleHelper.ResetDataGridView(dgv_WorkingLog);
            string querySql = $" SELECT wr_type,wr_obj_id FROM work_registration wr LEFT JOIN(" +
                $"SELECT trp_id, cs_id FROM transfer_registration_pc LEFT JOIN company_source ON com_id = cs_id) tb " +
                $"ON wr.trp_id = tb.trp_id WHERE wr_status = {(int)workStatus}";
            if (unitId != null)
                querySql += $" AND cs_id='{unitId}'";
            List<object[]> list = SqlHelper.ExecuteColumnsQuery(querySql, 2);
            List<object[]> resultList = new List<object[]>();
            for (int i = 0; i < list.Count; i++)
            {
                WorkType type = (WorkType)list[i][0];
                object id = list[i][1];
                string _querySql = null;
                switch (type)
                {
                    case WorkType.PaperWork:
                        _querySql = $"SELECT trp_id,trp_code,trp_name,cs_name,'纸本加工' FROM transfer_registration_pc LEFT JOIN " +
                            $"company_source ON com_id = cs_id WHERE trp_id='{id}'";
                        break;
                    case WorkType.CDWork:
                        _querySql = $"SELECT trc_id,trc_code,trc_name,cs_name,'光盘加工' FROM transfer_registraion_cd trc LEFT JOIN(" +
                            $"SELECT trp_id, cs_name FROM transfer_registration_pc LEFT JOIN company_source ON com_id = cs_id ) tb1 " +
                            $"ON tb1.trp_id = trc.trp_id WHERE trc_id='{id}'";
                        break;
                    case WorkType.ProjectWork:
                        _querySql = $"SELECT pi_id,pi_code,pi_name,cs_name,'项目/课题加工' FROM project_info pi " +
                            $"LEFT JOIN(SELECT trc_id, cs_name FROM transfer_registraion_cd trc " +
                            $"LEFT JOIN(SELECT trp_id, cs_name FROM transfer_registration_pc trp " +
                            $"LEFT JOIN company_source ON cs_id = trp.com_id)tb1 ON trc.trp_id = tb1.trp_id) tb2 ON tb2.trc_id = pi.trc_id " +
                            $"WHERE pi_id='{id}'";
                        break;
                    case WorkType.SubjectWork:
                        _querySql = $"SELECT si_id,si_code,si_name,cs_name,'课题/子课题加工' FROM subject_info si LEFT JOIN(" +
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
                object[] _obj = SqlHelper.ExecuteRowsQuery(_querySql);
                if (_obj != null)
                    resultList.Add(_obj);
            }

            dgv_WorkingLog.Columns.Add("id", "主键");
            dgv_WorkingLog.Columns.Add("code", "编号");
            dgv_WorkingLog.Columns.Add("name", "名称");
            dgv_WorkingLog.Columns.Add("cs_name", "来源单位");
            dgv_WorkingLog.Columns.Add("type", "加工类型");
            dgv_WorkingLog.Columns.Add("edit", "操作");
            dgv_WorkingLog.Columns.Add("submit", "提交");
            for (int i = 0; i < resultList.Count; i++)
            {
                int index = dgv_WorkingLog.Rows.Add();
                dgv_WorkingLog.Rows[index].Cells["id"].Value = resultList[i][0];
                dgv_WorkingLog.Rows[index].Cells["code"].Value = resultList[i][1];
                dgv_WorkingLog.Rows[index].Cells["name"].Value = resultList[i][2];
                dgv_WorkingLog.Rows[index].Cells["cs_name"].Value = resultList[i][3];
                dgv_WorkingLog.Rows[index].Cells["type"].Value = resultList[i][4];
                dgv_WorkingLog.Rows[index].Cells["edit"].Value = "编辑";
                dgv_WorkingLog.Rows[index].Cells["submit"].Value = "提交质检";
            }
            dgv_WorkingLog.Columns["id"].Visible = false;

            DataGridViewStyleHelper.SetLinkStyle(dgv_WorkingLog, new string[] { "edit", "submit" }, false);
            DataGridViewStyleHelper.SetAlignWithCenter(dgv_WorkingLog, new string[] { "type" });

            List<KeyValuePair<string, int>> keyValue = new List<KeyValuePair<string, int>>();
            keyValue.Add(new KeyValuePair<string, int>("name", 200));
            keyValue.Add(new KeyValuePair<string, int>("edit", 100));
            keyValue.Add(new KeyValuePair<string, int>("submit", 100));
            DataGridViewStyleHelper.SetWidth(dgv_WorkingLog, keyValue);

            dgv_WorkingLog.Tag = "WORK_LIST";
        }

        /// <summary>
        /// 单元格点击事件
        /// </summary>
        private void Dgv_WorkingLog_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex!=-1 && e.ColumnIndex != -1)
            {
                object columnName = dgv_WorkingLog.Columns[e.ColumnIndex].Name;
                //光盘数
                if ("trp_cd_amount".Equals(columnName))
                {
                    object trpid = dgv_WorkingLog.Rows[e.RowIndex].Cells["trp_id"].Value;
                    LoadCDList(trpid);
                }
                //光盘页 - 总数
                else if ("trc_total_amount".Equals(columnName))
                {
                    if (0 != Convert.ToInt32(dgv_WorkingLog.Rows[e.RowIndex].Cells[e.ColumnIndex].Value))
                    {
                        object trcid = dgv_WorkingLog.Rows[e.RowIndex].Cells["trc_id"].Value;
                        LoadProjectList(trcid);
                    }
                }
                //项目/课题 - 总数
                else if ("pi_total_amount".Equals(columnName))
                {
                    if (0 != Convert.ToInt32(dgv_WorkingLog.Rows[e.RowIndex].Cells[e.ColumnIndex].Value))
                    {
                        object pid = dgv_WorkingLog.Rows[e.RowIndex].Cells["pi_id"].Value;
                        LoadSubjectList(pid);
                    }
                }
                //光盘 - 加工
                else if ("trc_control".Equals(columnName))
                {
                    object trcid = dgv_WorkingLog.Rows[e.RowIndex].Cells["trc_id"].Value;
                    string msg = $"本张光盘包括{GetProjectAmount(trcid)}个项目，{GetSubjectAmountWithProject(trcid)}个课题；是否开始加工？";
                    if (MessageBox.Show(msg, "确认提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                    {
                        object trpId = SqlHelper.ExecuteOnlyOneQuery($"SELECT trp_id FROM transfer_registraion_cd WHERE trc_id='{trcid}'");
                        WorkRegistration wr = new WorkRegistration
                        {
                            WrId = Guid.NewGuid().ToString(),
                            WrStauts = WorkStatus.WorkSuccess,
                            WrStartDate = DateTime.Now,
                            WrType = WorkType.CDWork,
                            WrTrpId = trpId,
                            WrObjId = trcid
                        };
                        string insertSql = $"INSERT INTO work_registration VALUES('{wr.WrId}',{(int)wr.WrStauts},'{wr.WrTrpId}',{(int)wr.WrType},'{wr.WrStartDate}',null,'{wr.WrObjId}')";
                        SqlHelper.ExecuteNonQuery(insertSql);

                        string updateSql = $"UPDATE transfer_registraion_cd SET trc_status={(int)WorkStatus.WorkSuccess} WHERE trc_id='{wr.WrObjId}'";
                        SqlHelper.ExecuteNonQuery(updateSql);

                        //跳转到我的加工页面
                        LoadWorkList(null,WorkStatus.WorkSuccess);
                    }
                }
                //批次 - 加工
                else if ("trp_control".Equals(columnName))
                {
                    if (MessageBox.Show("是否确认加工当前选中批次？", "确认提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                    {
                       object trpid = dgv_WorkingLog.Rows[e.RowIndex].Cells["trp_id"].Value;
                        WorkRegistration wr = new WorkRegistration
                        {
                            WrId = Guid.NewGuid().ToString(),
                            WrStauts = WorkStatus.WorkSuccess,
                            WrStartDate = DateTime.Now,
                            WrType = WorkType.PaperWork,
                            WrTrpId = trpid,
                            WrObjId = trpid
                        };
                        string insertSql = $"INSERT INTO work_registration VALUES('{wr.WrId}',{(int)wr.WrStauts},'{wr.WrTrpId}',{(int)wr.WrType},'{wr.WrStartDate}',null,'{wr.WrObjId}')";
                        SqlHelper.ExecuteNonQuery(insertSql);

                        string updateSql = $"UPDATE transfer_registration_pc SET trp_work_status={(int)WorkStatus.WorkSuccess} WHERE trc_id='{wr.WrTrpId}'";
                        SqlHelper.ExecuteNonQuery(updateSql);

                        //跳转到我的加工页面
                        LoadWorkList(null, WorkStatus.WorkSuccess);
                    }
                }
                //项目/课题 - 加工
                else if ("pi_control".Equals(columnName))
                {
                    if (MessageBox.Show("是否确认加工当前选中项目/课题？", "确认提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                    {
                        object piid = dgv_WorkingLog.Rows[e.RowIndex].Cells["pi_id"].Value;
                        object trpid = SqlHelper.ExecuteOnlyOneQuery($"SELECT trp.trp_id FROM transfer_registration_pc trp, transfer_registraion_cd trc, project_info pi WHERE trp.trp_id = trc.trp_id AND pi.trc_id = trc.trc_id AND pi.pi_id='{piid}'");
                        WorkRegistration wr = new WorkRegistration
                        {
                            WrId = Guid.NewGuid().ToString(),
                            WrStauts = WorkStatus.WorkSuccess,
                            WrStartDate = DateTime.Now,
                            WrType = WorkType.ProjectWork,
                            WrObjId = piid,
                            WrTrpId = trpid
                        };
                        string insertSql = $"INSERT INTO work_registration VALUES('{wr.WrId}',{(int)wr.WrStauts},'{wr.WrTrpId}',{(int)wr.WrType},'{wr.WrStartDate}',null,'{wr.WrObjId}')";
                        SqlHelper.ExecuteNonQuery(insertSql);

                        string updateSql = $"UPDATE project_info SET pi_work_status={(int)WorkStatus.WorkSuccess} WHERE pi_id='{wr.WrObjId}'";
                        SqlHelper.ExecuteNonQuery(updateSql);

                        //跳转到我的加工页面
                        LoadWorkList(null, WorkStatus.WorkSuccess);
                    }
                }
                //课题/子课题 - 加工
                else if ("si_control".Equals(columnName))
                {
                    if (MessageBox.Show("是否确认加工当前选中课题/子课题？", "确认提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                    {
                        object siid = dgv_WorkingLog.Rows[e.RowIndex].Cells["si_id"].Value;
                        object trpid = SqlHelper.ExecuteOnlyOneQuery($"SELECT trp.trp_id FROM transfer_registration_pc trp, transfer_registraion_cd trc, project_info pi, subject_info si WHERE trp.trp_id = trc.trp_id AND pi.trc_id = trc.trc_id AND si.pi_id = pi.pi_id AND si.si_id = '{siid}'");
                        WorkRegistration wr = new WorkRegistration
                        {
                            WrId = Guid.NewGuid().ToString(),
                            WrStauts = WorkStatus.WorkSuccess,
                            WrStartDate = DateTime.Now,
                            WrType = WorkType.SubjectWork,
                            WrObjId = siid,
                            WrTrpId = trpid
                        };
                        string insertSql = $"INSERT INTO work_registration VALUES('{wr.WrId}',{(int)wr.WrStauts},'{wr.WrTrpId}',{(int)wr.WrType},'{wr.WrStartDate}',null,'{wr.WrObjId}')";
                        SqlHelper.ExecuteNonQuery(insertSql);

                        string updateSql = $"UPDATE subject_info SET si_work_status={(int)WorkStatus.WorkSuccess} WHERE si_id='{wr.WrObjId}'";
                        SqlHelper.ExecuteNonQuery(updateSql);

                        //跳转到我的加工页面
                        LoadWorkList(null, WorkStatus.WorkSuccess);
                    }
                }
            }
        }

        /// <summary>
        /// 根据光盘编号获取对应的项目下的课题总数
        /// </summary>
        /// <param name="trcid">光盘ID</param>
        private int GetSubjectAmountWithProject(object trcid)
        {
            int totalAmount = 0;
            List<object[]> list = SqlHelper.ExecuteColumnsQuery($"SELECT pi_id FROM project_info WHERE trc_id='{trcid}'", 1);
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
            DataGridViewStyleHelper.ResetDataGridView(dgv_WorkingLog);

            dgv_WorkingLog.Columns.Add("si_id", "主键");
            dgv_WorkingLog.Columns.Add("si_code", "课题/子课题编号");
            dgv_WorkingLog.Columns.Add("si_name", "课题/子课题名称");
            dgv_WorkingLog.Columns.Add("si_file_amount", "文件数");
            dgv_WorkingLog.Columns.Add("si_control", "操作");

            DataTable table = null;
            StringBuilder querySql = new StringBuilder("SELECT si_id,si_code,si_name FROM subject_info si");
            querySql.Append($" WHERE pi_id='{pid}' AND si_work_status={(int)WorkStatus.NonWork}");
            table = SqlHelper.ExecuteQuery(querySql.ToString());
            foreach (DataRow row in table.Rows)
            {
                int _index = dgv_WorkingLog.Rows.Add();
                dgv_WorkingLog.Rows[_index].Cells["si_id"].Value = row["si_id"];
                dgv_WorkingLog.Rows[_index].Cells["si_code"].Value = row["si_code"];
                dgv_WorkingLog.Rows[_index].Cells["si_name"].Value = row["si_name"];
                dgv_WorkingLog.Rows[_index].Cells["si_file_amount"].Value = 0;
                dgv_WorkingLog.Rows[_index].Cells["si_control"].Value = "加工";
            }
            if (dgv_WorkingLog.Columns.Count > 0)
                dgv_WorkingLog.Columns[0].Visible = false;

            List<KeyValuePair<string, int>> list = new List<KeyValuePair<string, int>>();
            list.AddRange(new KeyValuePair<string, int>[]
            {
                new KeyValuePair<string, int>("si_file_amount",90),
                new KeyValuePair<string, int>("si_control",100),
            });
            DataGridViewStyleHelper.SetWidth(dgv_WorkingLog, list);
            DataGridViewStyleHelper.SetLinkStyle(dgv_WorkingLog, new string[] {"si_control" }, true);
            DataGridViewStyleHelper.SetAlignWithCenter(dgv_WorkingLog, new string[] { "si_file_amount" });

            dgv_WorkingLog.Columns["si_id"].Visible = false;

            LastIdLog = new string[] { LastIdLog[1], $"Subject_{pid}" };
        }

        /// <summary>
        /// 加载指定批次下且未领取的光盘列表
        /// </summary>
        /// <param name="trpId">批次主键</param>
        private void LoadCDList(object trpId)
        {
            DataGridViewStyleHelper.ResetDataGridView(dgv_WorkingLog);

            dgv_WorkingLog.Columns.Add("trc_id", "主键");
            dgv_WorkingLog.Columns.Add("cs_name", "来源单位");
            dgv_WorkingLog.Columns.Add("trc_code", "光盘编号");
            dgv_WorkingLog.Columns.Add("trc_name", "光盘名称");
            dgv_WorkingLog.Columns.Add("trc_total_amount", "总数");
            dgv_WorkingLog.Columns.Add("trc_receive_amount", "已领取数");
            dgv_WorkingLog.Columns.Add("trc_file_amount", "文件数");
            dgv_WorkingLog.Columns.Add("trc_control", "操作");

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
            DataGridViewStyleHelper.SetTreeViewHeader(dgv_WorkingLog, tv);

            DataTable table = null;
            StringBuilder querySql = new StringBuilder("SELECT trc_id,cs_name,trc_code,trc_name,trc_status");
            querySql.Append(" FROM transfer_registraion_cd trc");
            querySql.Append(" LEFT JOIN(");
            querySql.Append(" SELECT trp.trp_id, cs_name,sorting FROM transfer_registration_pc trp, company_source cs WHERE trp.com_id = cs.cs_id ) tb");
            querySql.Append(" ON trc.trp_id = tb.trp_id");
            querySql.Append($" WHERE trc.trp_id='{trpId}' AND trc.trc_status={(int)WorkStatus.NonWork}");
            querySql.Append(" ORDER BY sorting ASC, trc_code ASC");
            table = SqlHelper.ExecuteQuery(querySql.ToString());
            foreach (DataRow row in table.Rows)
            {
                int _index = dgv_WorkingLog.Rows.Add();
                dgv_WorkingLog.Rows[_index].Cells["trc_id"].Value = row["trc_id"];
                dgv_WorkingLog.Rows[_index].Cells["cs_name"].Value = row["cs_name"];
                dgv_WorkingLog.Rows[_index].Cells["trc_code"].Value = row["trc_code"];
                dgv_WorkingLog.Rows[_index].Cells["trc_name"].Value = row["trc_name"];
                dgv_WorkingLog.Rows[_index].Cells["trc_total_amount"].Value = GetProjectAmount(row["trc_id"]);
                dgv_WorkingLog.Rows[_index].Cells["trc_receive_amount"].Value = GetTotalReceiveAmount(row["trc_id"]);
                dgv_WorkingLog.Rows[_index].Cells["trc_file_amount"].Value = 0;//文件数待处理
                dgv_WorkingLog.Rows[_index].Cells["trc_control"].Value = "加工";
            }

            List<KeyValuePair<string, int>> list = new List<KeyValuePair<string, int>>();
            list.Add(new KeyValuePair<string, int>("trc_total_amount", 90));
            list.Add(new KeyValuePair<string, int>("trc_receive_amount", 90));
            list.Add(new KeyValuePair<string, int>("trc_file_amount", 90));
            list.Add(new KeyValuePair<string, int>("trc_control", 100));
            DataGridViewStyleHelper.SetWidth(dgv_WorkingLog, list);

            DataGridViewStyleHelper.SetLinkStyle(dgv_WorkingLog, new string[] { "trc_total_amount", "trc_control" }, true);
            DataGridViewStyleHelper.SetAlignWithCenter(dgv_WorkingLog, new string[] { "trc_receive_amount", "trc_file_amount" });

            dgv_WorkingLog.Columns["trc_id"].Visible = false;
            LastIdLog = new string[] { LastIdLog[1], $"CD_{trpId}" };
        }

        /// <summary>
        /// 根据光盘ID获取项目+课题中已领取数总和
        /// </summary>
        private object GetTotalReceiveAmount(object trcId)
        {
            int proAmount = 0;
            int subAmount = 0;
            if (trcId != null)
            {
                proAmount = Convert.ToInt32(SqlHelper.ExecuteOnlyOneQuery($"SELECT COUNT(*) FROM project_info WHERE trc_id='{trcId}' AND pi_work_status='{(int)ReceiveStatus.ReceiveSuccess}'"));
                if (proAmount >= 0)
                    subAmount = Convert.ToInt32(SqlHelper.ExecuteOnlyOneQuery($"SELECT COUNT(*) FROM subject_info WHERE pi_id IN(SELECT pi_id FROM project_info WHERE trc_id = '{trcId}') AND si_work_status='{(int)ReceiveStatus.ReceiveSuccess}'"));
            }
            return proAmount + subAmount;
        }

        /// <summary>
        /// 根据光盘ID获取项目数
        /// </summary>
        private int GetProjectAmount(object trcId)
        {
            int proAmount = 0;
            if (trcId != null)
            {
                proAmount = Convert.ToInt32(SqlHelper.ExecuteOnlyOneQuery($"SELECT COUNT(*) FROM project_info WHERE trc_id='{trcId}'"));
            }
            return proAmount;
        }

        /// <summary>
        /// 加载光盘下的项目/课题列表
        /// </summary>
        /// <param name="trcId">光盘ID</param>
        private void LoadProjectList(object trcId)
        {
            DataGridViewStyleHelper.ResetDataGridView(dgv_WorkingLog);

            dgv_WorkingLog.Columns.Add("pi_id", "主键");
            dgv_WorkingLog.Columns.Add("pi_code", "项目/课题编号");
            dgv_WorkingLog.Columns.Add("pi_name", "项目/课题名称");
            dgv_WorkingLog.Columns.Add("pi_company", "承担单位");
            dgv_WorkingLog.Columns.Add("pi_total_amount", "总数");
            dgv_WorkingLog.Columns.Add("pi_receive_amount", "已领取数");
            dgv_WorkingLog.Columns.Add("pi_file_amount", "文件数");
            dgv_WorkingLog.Columns.Add("pi_control", "操作");

            TreeView tree = new TreeView();
            tree.Nodes.AddRange(new TreeNode[] {
                new TreeNode("主键"),
                new TreeNode("项目/课题编号"),
                new TreeNode( "项目/课题名称"),
                new TreeNode("承担单位"),
                new TreeNode("课题/子课题"),
                new TreeNode("操作"),
            });
            tree.Nodes[4].Nodes.AddRange(new TreeNode[]
            {
                new TreeNode("总数"),
                new TreeNode("已领取数"),
            });
            DataGridViewStyleHelper.SetTreeViewHeader(dgv_WorkingLog, tree);

            DataTable table = null;
            StringBuilder querySql = new StringBuilder("SELECT pi_id,pi_code,pi_name,pi_company,pi_work_status FROM project_info pi");
            querySql.Append($" WHERE trc_id='{trcId}' AND pi_work_status={(int)WorkStatus.NonWork}");
            table = SqlHelper.ExecuteQuery(querySql.ToString());
            foreach (DataRow row in table.Rows)
            {
                int _index = dgv_WorkingLog.Rows.Add();
                dgv_WorkingLog.Rows[_index].Cells["pi_id"].Value = row["pi_id"];
                dgv_WorkingLog.Rows[_index].Cells["pi_code"].Value = row["pi_code"];
                dgv_WorkingLog.Rows[_index].Cells["pi_name"].Value = row["pi_name"];
                dgv_WorkingLog.Rows[_index].Cells["pi_company"].Value = row["pi_company"];
                dgv_WorkingLog.Rows[_index].Cells["pi_total_amount"].Value = GetSubjectAmount(row["pi_id"]);
                dgv_WorkingLog.Rows[_index].Cells["pi_receive_amount"].Value = GetSubjectReceiveAmount(row["pi_id"]);
                dgv_WorkingLog.Rows[_index].Cells["pi_file_amount"].Value = 0;//文件数待处理
                dgv_WorkingLog.Rows[_index].Cells["pi_control"].Value = "加工";
            }
            if (dgv_WorkingLog.Columns.Count > 0)
                dgv_WorkingLog.Columns[0].Visible = false;

            List<KeyValuePair<string, int>> list = new List<KeyValuePair<string, int>>();
            list.Add(new KeyValuePair<string, int>("pi_code", 100));
            list.Add(new KeyValuePair<string, int>("pi_name", 250));
            list.Add(new KeyValuePair<string, int>("pi_company", 200));

            list.Add(new KeyValuePair<string, int>("pi_total_amount", 90));
            list.Add(new KeyValuePair<string, int>("pi_receive_amount", 90));
            list.Add(new KeyValuePair<string, int>("pi_file_amount", 90));
            list.Add(new KeyValuePair<string, int>("pi_control", 100));
            DataGridViewStyleHelper.SetWidth(dgv_WorkingLog, list);

            DataGridViewStyleHelper.SetLinkStyle(dgv_WorkingLog, new string[] { "pi_total_amount", "pi_control" }, true);
            DataGridViewStyleHelper.SetAlignWithCenter(dgv_WorkingLog, new string[] { "pi_receive_amount", "pi_file_amount" });

            dgv_WorkingLog.Columns["pi_id"].Visible = false;

            LastIdLog = new string[] { LastIdLog[1], $"Project_{trcId}" };
        }

        /// <summary>
        ///  根据父级ID获取子级已领取列表
        /// </summary>
        private object GetSubjectReceiveAmount(object pid)
        {
            int subAmount = 0;
            if (pid != null)
            {
                subAmount = Convert.ToInt32(SqlHelper.ExecuteOnlyOneQuery($"SELECT COUNT(*) FROM subject_info WHERE pi_id='{pid}' AND si_work_status='{(int)ReceiveStatus.ReceiveSuccess}'"));
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
                if ("WORK_LIST".Equals(dgv_WorkingLog.Tag))
                    LoadWorkList(null, WorkStatus.WorkSuccess);
                else
                    LoadPCList(null, null);
            }
            else
            {
                if ("WORK_LIST".Equals(dgv_WorkingLog.Tag))
                    LoadWorkList(csid, WorkStatus.WorkSuccess);
                else
                    LoadPCList(null, csid);
            }
        }
    }
}
