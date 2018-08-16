using System;
using System.Data;
using System.Windows.Forms;
using 科技计划项目档案数据采集管理系统.FirstPage;

namespace 科技计划项目档案数据采集管理系统
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
        private Frm_FirstPage frm_FirstPage;

        public Frm_QueryBorrowing()
        {
            InitializeComponent();
        }

        public Frm_QueryBorrowing(Frm_FirstPage frm_FirstPage)
        {
            InitializeComponent();
            this.frm_FirstPage = frm_FirstPage;
        }

        private void Frm_QueryBorrowing_Load(object sender, EventArgs e)
        {
            navigationPane1.SelectedPage = navigationPage1;
            string querySql = "SELECT dd_name FROM data_dictionary WHERE (dd_pId IN (SELECT dd_id " +
                "FROM data_dictionary WHERE(dd_code = 'dic_key_plan') OR (dd_code = 'dic_key_project'))) " +
                "AND dd_code<>'ZX' AND dd_code<>'YF'" +
                "ORDER BY dd_pId, dd_sort";
            object[] list = SqlHelper.ExecuteSingleColumnQuery(querySql);
            cbo_PlanTypeList.Items.AddRange(list);
            cbo_PlanTypeList.ResetText();
            LoadList(1);
            view1.Tag = false;
            GetTotalSize();
            view2.ColumnHeadersDefaultCellStyle = view1.ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle()
            {
                Padding = new Padding(0, 3, 0, 3),
                Font = new System.Drawing.Font("微软雅黑", 13f, System.Drawing.FontStyle.Regular),
                Alignment = DataGridViewContentAlignment.MiddleCenter
            };
            view2.DefaultCellStyle = DataGridViewStyleHelper.GetCellStyle();
            panel3.Bounds = new System.Drawing.Rectangle(0, panel3.Top, navigationPage1.Width, navigationPage1.Height - panel3.Top);
            panel1.Bounds = new System.Drawing.Rectangle(0, panel1.Top, navigationPage2.Width, navigationPage2.Height - panel1.Top);
            lbl_TotalFileAmount.Anchor = AnchorStyles.Right | AnchorStyles.Top;
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
                view1.Rows[i].Tag = row["pi_id"];
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
            string planType = cbo_PlanTypeList.Text;
            string batchName = txt_BatchName.Text;
            string proCode = txt_ProjectCode.Text;
            string proName = txt_ProjectName.Text;
            string sDate = chk_allDate.Checked ? null : dtp_sDate.Text;
            string eDate = chk_allDate.Checked ? null : dtp_eDate.Text;
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
        private void LoadDataList(int page, string planType, string batchName, string proCode, string proName, string sDate, string eDate)
        {
            string querySQL = $"SELECT TOP({pageSize}) A.* FROM( " +
                "SELECT pi_id, pi_code, pi_name, pi_start_datetime, pi_uniter, pi_year, pi_funds, pi_worker_date, pi_worker_id, pi_obj_id, pi_source_id FROM project_info WHERE pi_categor = 2 UNION ALL " +
                "SELECT ti_id, ti_code, ti_name, ti_start_datetime, ti_uniter, ti_year, ti_funds, ti_worker_date, ti_worker_id, ti_obj_id, ti_source_id FROM topic_info) A " +
                "LEFT JOIN project_info pi ON (A.pi_obj_id = pi.pi_id AND pi.pi_categor=1) " +
                "LEFT JOIN imp_dev_info idi ON A.pi_obj_id = idi.imp_id " +
                "LEFT JOIN T_Plan ON F_ID=A.pi_source_id " +
                "WHERE 1 = 1 ";
            if(!string.IsNullOrEmpty(proCode))
                querySQL += $"AND A.pi_code LIKE '%{proCode}%' ";
            if(!string.IsNullOrEmpty(proName))
                querySQL += $"AND A.pi_name LIKE '%{proName}%' ";
            if(!string.IsNullOrEmpty(planType))
                querySQL += $"AND (((pi.pi_id IS NOT NULL AND pi.pi_name LIKE '%{planType}%') OR " +
                    $"(idi.imp_id IS NOT NULL AND idi.imp_name LIKE '%{planType}%')) OR (F_Title LIKE '%{planType}%'))";
            if(!string.IsNullOrEmpty(sDate))
                querySQL += $"AND A.pi_start_datetime >= '{sDate}' ";
            if(!string.IsNullOrEmpty(eDate))
                querySQL += $"AND A.pi_start_datetime <= '{eDate}' ";

            string totalQuerySQL = $"SELECT TOP({pageSize * (page - 1)}) B.pi_id FROM( " +
                "SELECT pi_id, pi_name, pi_code, pi_obj_id, pi_start_datetime, pi_source_id FROM project_info WHERE pi_categor = 2 UNION ALL " +
                "SELECT ti_id, ti_name, ti_code, ti_obj_id, ti_start_datetime, ti_source_id FROM topic_info) B " +
                "LEFT JOIN project_info pi ON (B.pi_obj_id = pi.pi_id AND pi.pi_categor = 1) " +
                "LEFT JOIN imp_dev_info idi ON (B.pi_obj_id = idi.imp_id) " +
                "LEFT JOIN T_Plan ON F_ID=B.pi_source_id " +
                "WHERE 1 = 1 ";
            if(!string.IsNullOrEmpty(proCode))
                totalQuerySQL += $"AND B.pi_code LIKE '%{proCode}%' ";
            if(!string.IsNullOrEmpty(proName))
                totalQuerySQL += $"AND B.pi_name LIKE '%{proName}%' ";
            if(!string.IsNullOrEmpty(planType))
                totalQuerySQL += $"AND (((pi.pi_id IS NOT NULL AND pi.pi_name LIKE '%{planType}%') OR " +
                    $"(idi.imp_id IS NOT NULL AND idi.imp_name LIKE '%{planType}%')) OR (F_Title LIKE '%{planType}%'))";
            if(!string.IsNullOrEmpty(sDate))
                totalQuerySQL += $"AND B.pi_start_datetime >= '{sDate}' ";
            if(!string.IsNullOrEmpty(eDate))
                totalQuerySQL += $"AND B.pi_start_datetime <= '{eDate}' ";

            string countQuerySQL = $"SELECT COUNT(A.pi_id) FROM( " +
                "SELECT pi_id, pi_name, pi_code, pi_obj_id, pi_start_datetime, pi_source_id FROM project_info WHERE pi_categor = 2 UNION ALL " +
                "SELECT ti_id, ti_name, ti_code, ti_obj_id, ti_start_datetime, ti_source_id FROM topic_info) A " +
                "LEFT JOIN project_info pi ON (A.pi_obj_id = pi.pi_id AND pi.pi_categor = 1) " +
                "LEFT JOIN imp_dev_info idi ON (A.pi_obj_id = idi.imp_id) " +
                "LEFT JOIN T_Plan ON F_ID=A.pi_source_id " +
                "WHERE 1 = 1 ";
            if(!string.IsNullOrEmpty(proCode))
                countQuerySQL += $"AND A.pi_code LIKE '%{proCode}%' ";
            if(!string.IsNullOrEmpty(proName))
                countQuerySQL += $"AND A.pi_name LIKE '%{proName}%' ";
            if(!string.IsNullOrEmpty(planType))
                countQuerySQL += $"AND (((pi.pi_id IS NOT NULL AND pi.pi_name LIKE '%{planType}%') OR " +
                    $"(idi.imp_id IS NOT NULL AND idi.imp_name LIKE '%{planType}%')) OR (F_Title LIKE '%{planType}%'))";
            if(!string.IsNullOrEmpty(sDate))
                countQuerySQL += $"AND A.pi_start_datetime >= '{sDate}' ";
            if(!string.IsNullOrEmpty(eDate))
                countQuerySQL += $"AND A.pi_start_datetime <= '{eDate}' ";

            int totalSize = SqlHelper.ExecuteCountQuery(countQuerySQL);
            maxPage = totalSize % pageSize == 0 ? totalSize / pageSize : totalSize / pageSize + 1;
            label1.Text = $"共 {totalSize} 条记录，每页共 {pageSize} 条，共 {maxPage} 页";

            querySQL += $"AND A.pi_id NOT IN ({totalQuerySQL})";
            
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
            chk_allDate.Checked = true;
            GetTotalSize();
            LoadList(1);
        }

        private void chk_allDate_CheckedChanged(object sender, EventArgs e)
        {
            dtp_sDate.Enabled = dtp_eDate.Enabled = !chk_allDate.Checked;
        }

        private void View1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.ColumnIndex == -1 || e.RowIndex == -1)
                return;
            string columnName = view1.Columns[e.ColumnIndex].Name;
            //文件数
            if("fcount".Equals(columnName))
            {
                object id = view1.Rows[e.RowIndex].Tag;
                string pcode = ToolHelper.GetValue(view1.Rows[e.RowIndex].Cells["code"].Value);
                txt_FileName.Tag = id;
                txt_Pcode.Text = pcode;

                Btn_FileQuery_Click(null, null);
                navigationPane1.SelectedPage = navigationPage2;
            }
            else if("name".Equals(columnName))
            {
                object id = view1.Rows[e.RowIndex].Tag;
                DataRow data = SqlHelper.ExecuteSingleRowQuery($"SELECT * FROM project_info WHERE pi_id='{id}' UNION ALL " +
                    $"SELECT * FROM topic_info WHERE ti_id='{id}'");
                if(data != null)
                {
                    Frm_QueryDetail detail = new Frm_QueryDetail(data);
                    detail.ShowDialog();
                }
            }
        }

        /// <summary>
        /// 加载指定项目/课题下的文件列表
        /// </summary>
        /// <param name="id"></param>
        private void LoadFileList(object id, string fname, string fcategor, string pcode, string pname)
        {
            view2.Rows.Clear();
            string querySQL = "SELECT TOP(1000) bl.bl_id, bl.bl_borrow_state, bl.bl_return_state, pfl.pfl_id, pi.pi_code, pi.pi_name, ti.ti_code, ti.ti_name, si.si_code, si.si_name, pfl.pfl_name, dd.dd_name + ' ' + dd.extend_3 as categor " +
              "FROM processing_file_list pfl " +
              "LEFT JOIN project_info pi ON pi.pi_id = pfl.pfl_obj_id " +
              "LEFT JOIN topic_info ti ON ti.ti_id = pfl.pfl_obj_id " +
              "LEFT JOIN subject_info si ON si.si_id = pfl.pfl_obj_id " +
              "LEFT JOIN data_dictionary dd ON dd.dd_id = pfl.pfl_categor " +
              "LEFT JOIN borrow_log bl ON (bl.bl_file_id = pfl.pfl_id AND bl.bl_borrow_state=1) " +
              "WHERE 1=1 ";
            if(id != null)
                querySQL += $"AND pfl.pfl_obj_id='{id}' ";
            if(!string.IsNullOrEmpty(fname))
                querySQL += $"AND pfl.pfl_name LIKE '%{fname}%' ";
            if(!string.IsNullOrEmpty(fcategor))
                querySQL += $"AND dd.dd_name LIKE '%{fcategor}%' ";
            if(!string.IsNullOrEmpty(pcode))
                querySQL += $"AND (pi.pi_code LIKE '%{pcode}%' OR ti.ti_code LIKE '%{pcode}%' OR si.si_code LIKE '%{pcode}%') ";
            if(!string.IsNullOrEmpty(pname))
                querySQL += $"AND (pi.pi_name LIKE '%{pname}%' OR ti.ti_name LIKE '%{pname}%' OR si.si_name LIKE '%{pname}%') ";
            if(rdo_Out.Checked)
                querySQL += $"AND bl.bl_borrow_state = 1 ";
            else if(rdo_In.Checked)
                querySQL += $"AND (bl.bl_borrow_state = 0 OR bl.bl_borrow_state IS NULL) ";

            querySQL += "ORDER BY pfl.pfl_worker_date, pfl.pfl_sort DESC";
            DataTable dataTable = SqlHelper.ExecuteQuery(querySQL);
            foreach(DataRow row in dataTable.Rows)
            {
                int i = view2.Rows.Add();
                view2.Rows[i].Tag = row["pfl_id"];
                view2.Rows[i].Cells["fid"].Value = (i + 1).ToString();
                view2.Rows[i].Cells["fpcode"].Value = IsNull(row["pi_code"]) ?? IsNull(row["ti_code"]) ?? IsNull(row["si_code"]);
                view2.Rows[i].Cells["fpname"].Value = IsNull(row["pi_name"]) ?? IsNull(row["ti_name"]) ?? IsNull(row["si_name"]);
                view2.Rows[i].Cells["fname"].Value = row["pfl_name"];
                view2.Rows[i].Cells["fcategor"].Value = row["categor"];
                view2.Rows[i].Cells["fbstate"].Tag = row["bl_id"];
                view2.Rows[i].Cells["fbstate"].Value = GetBorrowState(row["bl_borrow_state"]);
                view2.Rows[i].Cells["frstate"].Value = GetReturnState(row["bl_return_state"]);
            }
            lbl_TotalFileAmount.Text = $"共计文件数：{view2.RowCount}";
        }

        private object GetReturnState(object value)
        {
            int i = ToolHelper.GetIntValue(value);
            if(i == 0)
                return "未归还";
            else if(i == 1)
                return "已归还";
            else
                return "-";
        }

        private string GetBorrowState(object value)
        {
            int i = ToolHelper.GetIntValue(value);
            if(i == 1)
                return "借出";
            else
                return "在库";
        }

        private object IsNull(object value)
        {
            if(value == null)
                return null;
            else
            {
                string result = value.ToString();
                if(string.IsNullOrEmpty(result))
                    return null;
                else
                    return result;
            }
        }

        private void View2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string columnName = view2.Columns[e.ColumnIndex].Name;
            //借阅状态
            if("fbstate".Equals(columnName))
            {
                if("在库".Equals(view2.Rows[e.RowIndex].Cells[e.ColumnIndex].Value))
                {
                    object fid = view2.Rows[e.RowIndex].Tag;
                    object fcode = view2.Rows[e.RowIndex].Cells["fcategor"].Value;
                    object fname = view2.Rows[e.RowIndex].Cells["fname"].Value;
                    Frm_BorrowEdit frm = new Frm_BorrowEdit(ToolHelper.GetValue(fcode), ToolHelper.GetValue(fname));
                    frm.FILE_ID = fid;
                    frm.txt_Real_Return_Date.Enabled = false;
                    if(frm.ShowDialog() == DialogResult.OK)
                    {
                        view2.Rows[e.RowIndex].Cells[e.ColumnIndex].Tag = frm.Tag;
                        view2.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = "借出";
                        view2.Rows[e.RowIndex].Cells["frstate"].Value = "未归还";
                    }
                }
            }
            //归还状态
            else if("frstate".Equals(columnName))
            {
                if("未归还".Equals(view2.Rows[e.RowIndex].Cells[e.ColumnIndex].Value))
                {
                    object fcode = view2.Rows[e.RowIndex].Cells["fcategor"].Value;
                    object fname = view2.Rows[e.RowIndex].Cells["fname"].Value;
                    object id = view2.Rows[e.RowIndex].Cells["fbstate"].Tag;
                    DataRow row = SqlHelper.ExecuteSingleRowQuery($"SELECT * FROM borrow_log WHERE bl_id='{id}'");
                    if(row != null)
                    {
                        Frm_BorrowEdit frm = new Frm_BorrowEdit(ToolHelper.GetValue(fcode), ToolHelper.GetValue(fname));
                        frm.txt_Unit.Text = ToolHelper.GetValue(row["bl_user_unit"]);
                        frm.txt_Unit.ReadOnly = true;
                        frm.txt_User.Text = ToolHelper.GetValue(row["bl_user"]);
                        frm.txt_User.ReadOnly = true;
                        frm.txt_Phone.Text = ToolHelper.GetValue(row["bl_user_phone"]);
                        frm.txt_Phone.ReadOnly = true;
                        frm.txt_Borrow_Date.Text = ToolHelper.GetValue(row["bl_date"]);
                        frm.txt_Borrow_Date.ReadOnly = true;
                        frm.txt_Borrow_Term.Text = ToolHelper.GetValue(row["bl_term"]);
                        frm.txt_Borrow_Term.ReadOnly = true;
                        frm.cbo_FileType.SelectedIndex = ToolHelper.GetIntValue(row["bl_form"]);
                        frm.cbo_FileType.Enabled = false;
                        frm.txt_Should_Return_Date.Text = ToolHelper.GetValue(row["bl_should_return_term"]);
                        frm.txt_Should_Return_Date.ReadOnly = true;
                        frm.txt_Real_Return_Date.Text = DateTime.Now.ToString();
                        frm.lbl_FIleName.Tag = id;
                        frm.FILE_ID = fid;
                        frm.btn_Sure.Text = "确认归还";
                        frm.txt_Real_Return_Date.Focus();
                        if(frm.ShowDialog() == DialogResult.OK)
                        {
                            view2.Rows[e.RowIndex].Cells[e.ColumnIndex].Tag = frm.Tag;
                            view2.Rows[e.RowIndex].Cells["fbstate"].Value = "在库";
                            view2.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = "归还";
                        }
                    }
                }
            }
        }

        private void Btn_FileQuery_Click(object sender, EventArgs e)
        {
            object fid = txt_FileName.Tag;
            string fname = txt_FileName.Text;
            string fcategor = txt_FileCategor.Text;
            string pcode = txt_Pcode.Text;
            string pname = txt_Pname.Text;
            if(fid != null || !string.IsNullOrEmpty(fname) || !string.IsNullOrEmpty(fcategor) || !string.IsNullOrEmpty(pcode) || !string.IsNullOrEmpty(pname)
                || rdo_Out.Checked)
            {
                LoadFileList(fid, fname, fcategor, pcode, pname);
            }
            else
                Btn_FileReset_Click(null, null);
        }

        private void Btn_FileReset_Click(object sender, EventArgs e)
        {
            txt_FileName.Tag = null;
            txt_FileName.ResetText();
            txt_FileCategor.ResetText();
            txt_Pcode.ResetText();
            txt_Pname.ResetText();
            rdo_All.Checked = true;
            view2.Rows.Clear();
            lbl_TotalFileAmount.Text = "共计文件数：0";
        }

        private void Frm_QueryBorrowing_FormClosing(object sender, FormClosingEventArgs e)
        {
            frm_FirstPage.Show();
        }
    }
}
