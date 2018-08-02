using System;
using System.Data;
using System.Windows.Forms;

namespace 科技计划项目档案数据采集管理系统.QueryAndCount
{
    public partial class Frm_QueryBorrowing : DevExpress.XtraEditors.XtraForm
    {
        /// <summary>
        /// 每页默认显示条数
        /// </summary>
        int pageSize = 30;
        /// <summary>
        /// 总页数
        /// </summary>
        int maxPage = 0;
        public Frm_QueryBorrowing()
        {
            InitializeComponent();
        }

        private void Frm_QueryBorrowing_Load(object sender, EventArgs e)
        {
            navigationPane1.SelectedPage = navigationPage1;
            string planKey = "dic_key_plan";
            string querySql = $"SELECT dd_id, dd_name FROM data_dictionary WHERE dd_pid=(SELECT top(1) dd_id FROM data_dictionary WHERE dd_code = '{planKey}') ORDER BY dd_sort";
            DataTable table = SqlHelper.ExecuteQuery(querySql);
            cbo_PlanTypeList.DataSource = table;
            cbo_PlanTypeList.DisplayMember = "dd_name";
            cbo_PlanTypeList.ValueMember = "dd_id";
            cbo_PlanTypeList.ResetText();
            LoadList(1);
            view1.Tag = false;
            GetTotalSize();
            view1.ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle()
            {
                Padding = new Padding(0, 3, 0, 3),
                Font = new System.Drawing.Font("微软雅黑", 13f, System.Drawing.FontStyle.Regular),
                Alignment = DataGridViewContentAlignment.MiddleCenter
            };
        }

        private void GetTotalSize()
        {
            string querySQL = $"SELECT COUNT(pi_id) FROM (" +
                           "SELECT pi_id, pi_worker_date FROM project_info WHERE pi_categor = 2 " +
                           "UNION ALL " +
                           "SELECT ti_id, ti_worker_date FROM topic_info) TB1";
            int size = SqlHelper.ExecuteCountQuery(querySQL);
            maxPage = size % pageSize == 0 ? size / pageSize : size / pageSize + 1;
            label1.Text = $"共 {size} 条记录，每页共 {pageSize} 条，共 {maxPage} 页";
        }

        /// <summary>
        /// 加载默认页数据
        /// </summary>
        /// <param name="page">当前页码</param>
        private void LoadList(int page)
        {
            string querySQL = $"SELECT TOP({pageSize}) * FROM (" +
               "SELECT pi_id, pi_code, pi_name, pi_start_datetime, pi_uniter, pi_year, pi_funds, pi_worker_date, pi_worker_id FROM project_info WHERE pi_categor = 2 " +
               "UNION ALL " +
               "SELECT ti_id, ti_code, ti_name, ti_start_datetime, ti_uniter, ti_year, ti_funds, ti_worker_date, ti_worker_id FROM topic_info) TB1 " +
               "WHERE pi_id NOT IN(" +
               $"SELECT TOP({pageSize * (page - 1)}) TB2.pi_id FROM (" +
               "SELECT pi_id, pi_code, pi_worker_date FROM project_info WHERE pi_categor = 2 " +
               "UNION ALL " +
               "SELECT ti_id, ti_code, ti_worker_date FROM topic_info) TB2 ORDER BY TB2.pi_worker_date DESC, TB2.pi_code) " +
               "ORDER BY TB1.pi_worker_date DESC, TB1.pi_code";
            CreateDataList(page, querySQL);
        }

        /// <summary>
        /// 填充数据列表
        /// </summary>
        /// <param name="page">当前页码</param>
        /// <param name="querySQL">查询sql语句</param>
        private void CreateDataList(int page, string querySQL)
        {
            view1.Columns.Clear();
            view1.Rows.Clear();
            view1.Columns.AddRange(new DataGridViewColumn[]
            {
                new DataGridViewTextBoxColumn(){ Name = "id", HeaderText = "序号", FillWeight = 20, SortMode = DataGridViewColumnSortMode.NotSortable, DefaultCellStyle = DataGridViewStyleHelper.GetCellStyle() },
                new DataGridViewTextBoxColumn(){ Name = "code", HeaderText = "项目/课题编号", SortMode = DataGridViewColumnSortMode.NotSortable, FillWeight = 80, DefaultCellStyle = DataGridViewStyleHelper.GetCellStyle() },
                new DataGridViewTextBoxColumn(){ Name = "name", HeaderText = "项目/课题名称", SortMode = DataGridViewColumnSortMode.NotSortable, FillWeight = 150, DefaultCellStyle = DataGridViewStyleHelper.GetCellStyle()},
                new DataGridViewTextBoxColumn(){ Name = "sdate", HeaderText = "开始时间", SortMode = DataGridViewColumnSortMode.NotSortable, FillWeight = 50, DefaultCellStyle = DataGridViewStyleHelper.GetCellStyle() },
                new DataGridViewTextBoxColumn(){ Name = "user", HeaderText = "负责人", SortMode = DataGridViewColumnSortMode.NotSortable, FillWeight = 40, DefaultCellStyle = DataGridViewStyleHelper.GetCellStyle() },
                new DataGridViewTextBoxColumn(){ Name = "tcount", HeaderText = "子课题数", SortMode = DataGridViewColumnSortMode.NotSortable, FillWeight = 30, DefaultCellStyle = DataGridViewStyleHelper.GetCellStyle() },
                new DataGridViewLinkColumn(){ Name = "fcount", HeaderText = "文件数", SortMode = DataGridViewColumnSortMode.NotSortable, FillWeight = 30, DefaultCellStyle = DataGridViewStyleHelper.GetCellStyle() },
            });
            DataTable table = SqlHelper.ExecuteQuery(querySQL);
            foreach(DataRow row in table.Rows)
            {
                int i = view1.Rows.Add();
                view1.Rows[i].Cells["id"].Value = (i + 1).ToString();
                view1.Rows[i].Cells["code"].Value = row["pi_code"];
                view1.Rows[i].Cells["name"].Value = row["pi_name"];
                view1.Rows[i].Cells["sdate"].Value = row["pi_start_datetime"];
                view1.Rows[i].Cells["user"].Value = row["pi_uniter"];
                view1.Rows[i].Cells["tcount"].Value = GetTopicCount(row["pi_id"]);
                view1.Rows[i].Cells["fcount"].Value = GetFileCount(row["pi_id"]);
            }
            view1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            view1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            txt_page.Text = page.ToString();
        }

        /// <summary>
        /// 获取指定项目/课题的文件数
        /// </summary>
        private int GetFileCount(object id)
        {
            string querySql = $"SELECT COUNT(pfl_id) FROM processing_file_list WHERE pfl_obj_id = '{id}'";
            return SqlHelper.ExecuteCountQuery(querySql);
        }

        /// <summary>
        /// 获取指定项目/课题下的子课题数
        /// </summary>
        private object GetTopicCount(object id)
        {
            string querySQL = "SELECT COUNT(ti_id) FROM (" +
                $"SELECT ti_id FROM topic_info WHERE ti_obj_id='{id}' UNION ALL " +
                $"SELECT si_id FROM subject_info WHERE si_obj_id='{id}' " +
                ") tb1";
            return SqlHelper.ExecuteCountQuery(querySQL);
        }

        private void navigationPane1_StateChanged(object sender, DevExpress.XtraBars.Navigation.StateChangedEventArgs e)
        {
            DevExpress.XtraBars.Navigation.NavigationPane panel = (sender as DevExpress.XtraBars.Navigation.NavigationPane);
            if(e.State == DevExpress.XtraBars.Navigation.NavigationPaneState.Collapsed)
            {
                panel.State = DevExpress.XtraBars.Navigation.NavigationPaneState.Default;
            }
        }

        private void Btn_Page_Click(object sender, EventArgs e)
        {
            string name = (sender as Control).Name;
            //下一页
            if("btn_npage".Equals(name))
            {
                int page = Convert.ToInt32(txt_page.Text) + 1;
                if(page <= maxPage)
                {
                    bool flag = (bool)view1.Tag;
                    if(flag)
                        Btn_Query_Click(page, null);
                    else
                        LoadList(page);
                }
            }
            //上一页
            else if("btn_lpage".Equals(name))
            {
                int page = Convert.ToInt32(txt_page.Text) - 1;
                if(page > 0)
                {
                    bool flag = (bool)view1.Tag;
                    if(flag)
                        Btn_Query_Click(page, null);
                    else
                        LoadList(page);
                }
            }
            //首页
            else if("btn_fpage".Equals(name))
            {
                int page = Convert.ToInt32(txt_page.Text);
                if(page > 1)
                {
                    bool flag = (bool)view1.Tag;
                    if(flag)
                        Btn_Query_Click(1, null);
                    else
                        LoadList(1);
                }
            }
            //末页
            if("btn_epage".Equals(name))
            {
                int page = Convert.ToInt32(txt_page.Text);
                if(page < maxPage)
                {
                    bool flag = (bool)view1.Tag;
                    if(flag)
                        Btn_Query_Click(maxPage, null);
                    else
                        LoadList(maxPage);
                }
            }
        }

        private void Txt_page_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                if(int.TryParse(txt_page.Text, out int value))
                {
                    if(value > maxPage)
                        value = maxPage;
                    else if(value < 1)
                        value = 1;
                    bool flag = (bool)view1.Tag;
                    if(flag)
                        Btn_Query_Click(value, null);
                    else
                        LoadList(value);
                }
            }
        }

        private void Btn_Query_Click(object sender, EventArgs e)
        {
            int page = 1;
            page = int.TryParse(sender.ToString(), out page) ? page : 1;
            object planType = cbo_PlanTypeList.SelectedValue;
            string batchName = txt_BatchName.Text;
            string proCode = txt_ProjectCode.Text;
            string proName = txt_ProjectName.Text;
            string sDate = dtp_sDate.Text;
            string eDate = dtp_eDate.Text;
            //如果查询条件全部为空，则默认显示首页查询
            if(string.IsNullOrEmpty(cbo_PlanTypeList.Text) && string.IsNullOrEmpty(batchName) && string.IsNullOrEmpty(proCode)
                && string.IsNullOrEmpty(proName) && string.IsNullOrEmpty(sDate) && string.IsNullOrEmpty(eDate))
            {
                LoadList(page);
                view1.Tag = false;
            }
            else
            {
                LoadDataList(page, planType, batchName, proCode, proName, sDate, eDate);
                view1.Tag = true;
            }
        }

        /// <summary>
        /// 加载查询数据
        /// </summary>
        /// <param name="page">当前页码</param>
        private void LoadDataList(int page, object planType, string batchName, string proCode, string proName, object sDate, object eDate)
        {
            string querySQL = $"SELECT TOP({pageSize}) * FROM( " +
                "SELECT pi_id, pi_code, pi_name, pi_start_datetime, pi_uniter, pi_year, pi_funds, pi_worker_date, pi_worker_id " +
                "FROM project_info WHERE(pi_categor = 2) UNION ALL " +
                "SELECT ti_id, ti_code, ti_name, ti_start_datetime, ti_uniter, ti_year, ti_funds, ti_worker_date, ti_worker_id FROM topic_info) A " +
                "WHERE 1 = 1 ";
            if(!string.IsNullOrEmpty(proCode))
                querySQL += $"AND pi_code LIKE '%{proCode}%' ";
            if(!string.IsNullOrEmpty(proName))
                querySQL += $"AND pi_name LIKE '%{proName}%' ";

            string totalQuerySQL = $"SELECT TOP({pageSize * (page - 1)}) pi_id FROM( " +
                "SELECT pi_id, pi_code FROM project_info WHERE(pi_categor = 2) UNION ALL " +
                "SELECT ti_id, ti_code FROM topic_info) A1 " +
                "WHERE 1 = 1 ";
            if(!string.IsNullOrEmpty(proCode))
                totalQuerySQL += $"AND pi_code LIKE '%{proCode}%' ";
            if(!string.IsNullOrEmpty(proName))
                totalQuerySQL += $"AND pi_name LIKE '%{proName}%' ";

            string countQuerySQL = $"SELECT COUNT(pi_id) FROM( " +
                "SELECT pi_id, pi_code FROM project_info WHERE(pi_categor = 2) UNION ALL " +
                "SELECT ti_id, ti_code FROM topic_info) A1 " +
                "WHERE 1 = 1 ";
            if(!string.IsNullOrEmpty(proCode))
                countQuerySQL += $"AND pi_code LIKE '%{proCode}%' ";
            if(!string.IsNullOrEmpty(proName))
                countQuerySQL += $"AND pi_name LIKE '%{proName}%' ";

            int totalSize = SqlHelper.ExecuteCountQuery(countQuerySQL);
            maxPage = totalSize % pageSize == 0 ? totalSize / pageSize : totalSize / pageSize + 1;
            label1.Text = $"共 {totalSize} 条记录，每页共 {pageSize} 条，共 {maxPage} 页";

            querySQL += $" AND pi_id NOT IN ({totalQuerySQL})";
            
            CreateDataList(page, querySQL);
        }

        private void Btn_Reset_Click(object sender, EventArgs e)
        {
            cbo_PlanTypeList.ResetText();
            txt_BatchName.ResetText();
            txt_ProjectCode.ResetText();
            txt_ProjectName.ResetText();
            dtp_sDate.ResetText();
            dtp_eDate.ResetText();
            GetTotalSize();
            LoadList(1);
        }

    }
}
