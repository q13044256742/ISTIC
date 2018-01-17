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
            dgv_WorkLog.DataSource = null;
            dgv_WorkLog.Columns.Clear();
            dgv_WorkLog.Rows.Clear();

            DataTable dataTable = null;
            if (querySql == null)
            {
                //加载实物登记数据【默认加载状态为2（已提交）的数据】
                querySql = new StringBuilder("SELECT ");
                querySql.Append("pc.trp_id,");
                querySql.Append("cs_name,");
                querySql.Append("trp_name,");
                querySql.Append("trp_code,");
                querySql.Append("trp_cd_amount");
                querySql.Append(" FROM transfer_registration_pc pc LEFT JOIN company_source cs ON pc.com_id = cs.cs_id");
                querySql.Append(" WHERE trp_status=2");
                if (csid != null)
                    querySql.Append($" AND cs.cs_id='{csid}'");
            }
            dataTable = SqlHelper.ExecuteQuery(querySql.ToString());
            //将数据源转化成DataGridView表数据
            dgv_WorkLog.Columns.Add("trp_id", "主键");
            dgv_WorkLog.Columns.Add("cs_name", "来源单位");
            dgv_WorkLog.Columns.Add("trp_name", "批次名称");
            dgv_WorkLog.Columns.Add("trp_code", "批次编号");
            dgv_WorkLog.Columns.Add("trp_finishtime", "完成时间");
            dgv_WorkLog.Columns.Add("trp_cd_amount", "光盘数");
            dgv_WorkLog.Columns.Add("trp_control", "操作");
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                DataRow row = dataTable.Rows[i];
                int index = dgv_WorkLog.Rows.Add();
                dgv_WorkLog.Rows[index].Cells["trp_id"].Value = row["trp_id"];
                dgv_WorkLog.Rows[index].Cells["cs_name"].Value = row["cs_name"];
                dgv_WorkLog.Rows[index].Cells["trp_name"].Value = row["trp_name"];
                dgv_WorkLog.Rows[index].Cells["trp_code"].Value = row["trp_code"];
                dgv_WorkLog.Rows[index].Cells["trp_finishtime"].Value = null;//完成时间字段待定
                dgv_WorkLog.Rows[index].Cells["trp_cd_amount"].Value = row["trp_cd_amount"];
                dgv_WorkLog.Rows[index].Cells["trp_control"].Value = "加工";
            }
            //设置最小列宽
            dgv_WorkLog.Columns["cs_name"].MinimumWidth = 200;
            dgv_WorkLog.Columns["trp_name"].MinimumWidth = 250;
            dgv_WorkLog.Columns["trp_cd_amount"].MinimumWidth = 90;
            dgv_WorkLog.Columns["trp_control"].MinimumWidth = 100;
            //设置链接按钮样式
            DataGridViewStyleHelper.SetAlignWithCenter(dgv_WorkLog, new string[] { "trp_cd_amount", "trp_control" });
            DataGridViewStyleHelper.SetLinkStyle(dgv_WorkLog, new string[] { "trp_cd_amount", "trp_control" }, true);

            dgv_WorkLog.Columns["trp_id"].Visible = false;
            LastIdLog = new string[] { string.Empty, $"PC_{csid}" };
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
            }
        }

        /// <summary>
        /// 单元格点击事件
        /// </summary>
        private void dgv_WorkLog_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex!=-1 && e.ColumnIndex != -1)
            {
                //光盘数
                if ("trp_cd_amount".Equals(dgv_WorkLog.Columns[e.ColumnIndex].Name))
                {
                    object trpid = dgv_WorkLog.Rows[e.RowIndex].Cells["trp_id"].Value;
                    LoadCDList(trpid);
                }
                //光盘页 - 总数
                else if ("trc_total_amount".Equals(dgv_WorkLog.Columns[e.ColumnIndex].Name))
                {
                    if (0 != Convert.ToInt32(dgv_WorkLog.Rows[e.RowIndex].Cells[e.ColumnIndex].Value))
                    {
                        object trcid = dgv_WorkLog.Rows[e.RowIndex].Cells["trc_id"].Value;
                        LoadProjectList(trcid);
                    }
                }
                //项目/课题 - 总数
                else if ("pi_total_amount".Equals(dgv_WorkLog.Columns[e.ColumnIndex].Name))
                {
                    if (0 != Convert.ToInt32(dgv_WorkLog.Rows[e.RowIndex].Cells[e.ColumnIndex].Value))
                    {
                        object pid = dgv_WorkLog.Rows[e.RowIndex].Cells["pi_id"].Value;
                        LoadSubjectList(pid);
                    }
                }
            }
        }

        /// <summary>
        /// 加载课题/子课题列表
        /// </summary>
        /// <param name="pid">项目/课题ID</param>
        private void LoadSubjectList(object pid)
        {
            dgv_WorkLog.Rows.Clear();
            dgv_WorkLog.Columns.Clear();

            dgv_WorkLog.Columns.Add("si_id", "主键");
            dgv_WorkLog.Columns.Add("si_code", "课题/子课题编号");
            dgv_WorkLog.Columns.Add("si_name", "课题/子课题名称");
            dgv_WorkLog.Columns.Add("si_file_amount", "文件数");
            dgv_WorkLog.Columns.Add("si_control", "操作");

            DataTable table = null;
            StringBuilder querySql = new StringBuilder("SELECT si_id,si_code,si_name");
            querySql.Append(" FROM subject_info si");
            querySql.Append($" WHERE pi_id='{pid}'");
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
        /// 加载光盘列表
        /// </summary>
        /// <param name="trpId">批次主键</param>
        private void LoadCDList(object trpId)
        {
            dgv_WorkLog.Rows.Clear();
            dgv_WorkLog.Columns.Clear();

            dgv_WorkLog.Columns.Add("trc_id", "主键");
            dgv_WorkLog.Columns.Add("cs_name", "来源单位");
            dgv_WorkLog.Columns.Add("trc_code", "光盘编号");
            dgv_WorkLog.Columns.Add("trc_name", "光盘名称");
            dgv_WorkLog.Columns.Add("trc_total_amount", "总数");
            dgv_WorkLog.Columns.Add("trc_receive_amount", "已领取数");
            dgv_WorkLog.Columns.Add("trc_file_amount", "文件数");
            dgv_WorkLog.Columns.Add("trc_control", "操作");

            DataTable table = null;
            StringBuilder querySql = new StringBuilder("SELECT trc_id,cs_name,trc_code,trc_name,trc_status");
            querySql.Append(" FROM transfer_registraion_cd trc");
            querySql.Append(" LEFT JOIN(");
            querySql.Append(" SELECT trp.trp_id, cs_name,sorting FROM transfer_registration_pc trp, company_source cs WHERE trp.com_id = cs.cs_id ) tb");
            querySql.Append(" ON trc.trp_id = tb.trp_id");
            querySql.Append($" WHERE trc.trp_id='{trpId}'");
            querySql.Append(" ORDER BY sorting ASC, trc_code ASC");
            table = SqlHelper.ExecuteQuery(querySql.ToString());
            foreach (DataRow row in table.Rows)
            {
                int _index = dgv_WorkLog.Rows.Add();
                dgv_WorkLog.Rows[_index].Cells["trc_id"].Value = row["trc_id"];
                dgv_WorkLog.Rows[_index].Cells["cs_name"].Value = row["cs_name"];
                dgv_WorkLog.Rows[_index].Cells["trc_code"].Value = row["trc_code"];
                dgv_WorkLog.Rows[_index].Cells["trc_name"].Value = row["trc_name"];
                dgv_WorkLog.Rows[_index].Cells["trc_total_amount"].Value = GetTotalAmount(row["trc_id"]);
                dgv_WorkLog.Rows[_index].Cells["trc_receive_amount"].Value = GetTotalReceiveAmount(row["trc_id"]);
                dgv_WorkLog.Rows[_index].Cells["trc_file_amount"].Value = 0;//文件数待处理
                dgv_WorkLog.Rows[_index].Cells["trc_control"].Value = "加工";
            }

            List<KeyValuePair<string, int>> list = new List<KeyValuePair<string, int>>();
            list.Add(new KeyValuePair<string, int>("trc_total_amount", 90));
            list.Add(new KeyValuePair<string, int>("trc_receive_amount", 90));
            list.Add(new KeyValuePair<string, int>("trc_file_amount", 90));
            list.Add(new KeyValuePair<string, int>("trc_control", 100));
            DataGridViewStyleHelper.SetWidth(dgv_WorkLog, list);

            DataGridViewStyleHelper.SetLinkStyle(dgv_WorkLog, new string[] { "trc_total_amount", "trc_control" }, true);
            DataGridViewStyleHelper.SetAlignWithCenter(dgv_WorkLog, new string[] { "trc_receive_amount", "trc_file_amount" });

            dgv_WorkLog.Columns["trc_id"].Visible = false;
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
        /// 根据光盘ID获取项目数+课题数总和
        /// </summary>
        private int GetTotalAmount(object trcId)
        {
            if (trcId != null)
            {
                int proAmount = Convert.ToInt32(SqlHelper.ExecuteOnlyOneQuery($"SELECT COUNT(*) FROM project_info WHERE trc_id='{trcId}'"));
                int subAmount = Convert.ToInt32(SqlHelper.ExecuteOnlyOneQuery($"SELECT COUNT(*) FROM subject_info WHERE pi_id IN(SELECT pi_id FROM project_info WHERE trc_id = '{trcId}')"));
                return proAmount + subAmount;
            }
            return 0;
        }

        /// <summary>
        /// 加载光盘下的项目/课题列表
        /// </summary>
        /// <param name="trcId">光盘ID</param>
        private void LoadProjectList(object trcId)
        {
            dgv_WorkLog.Rows.Clear();
            dgv_WorkLog.Columns.Clear();

            dgv_WorkLog.Columns.Add("pi_id", "主键");
            dgv_WorkLog.Columns.Add("pi_code", "项目/课题编号");
            dgv_WorkLog.Columns.Add("pi_name", "项目/课题名称");
            dgv_WorkLog.Columns.Add("pi_company", "承担单位");
            dgv_WorkLog.Columns.Add("pi_total_amount", "总数");
            dgv_WorkLog.Columns.Add("pi_receive_amount", "已领取数");
            dgv_WorkLog.Columns.Add("pi_file_amount", "文件数");
            dgv_WorkLog.Columns.Add("pi_control", "操作");

            DataTable table = null;
            StringBuilder querySql = new StringBuilder("SELECT pi_id,pi_code,pi_name,pi_company,pi_work_status");
            querySql.Append(" FROM project_info pi");
            querySql.Append($" WHERE trc_id='{trcId}'");
            table = SqlHelper.ExecuteQuery(querySql.ToString());
            foreach (DataRow row in table.Rows)
            {
                int _index = dgv_WorkLog.Rows.Add();
                dgv_WorkLog.Rows[_index].Cells["pi_id"].Value = row["pi_id"];
                dgv_WorkLog.Rows[_index].Cells["pi_code"].Value = row["pi_code"];
                dgv_WorkLog.Rows[_index].Cells["pi_name"].Value = row["pi_name"];
                dgv_WorkLog.Rows[_index].Cells["pi_company"].Value = row["pi_company"];
                dgv_WorkLog.Rows[_index].Cells["pi_total_amount"].Value = GetSubjectAmount(row["pi_id"]);
                dgv_WorkLog.Rows[_index].Cells["pi_receive_amount"].Value = GetSubjectReceiveAmount(row["pi_id"]);
                dgv_WorkLog.Rows[_index].Cells["pi_file_amount"].Value = 0;//文件数待处理
                dgv_WorkLog.Rows[_index].Cells["pi_control"].Value = "加工";
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
        /// 根据父级ID获取子级列表
        /// </summary>
        private object GetSubjectAmount(object pid)
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
                LoadPCList(null, null);
            }
            else
            {
                LoadPCList(null, csid);
            }
        }
    }
}
