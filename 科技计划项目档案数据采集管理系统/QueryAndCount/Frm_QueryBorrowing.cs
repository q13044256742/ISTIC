using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Columns;
using DevExpress.XtraTreeList.Nodes;
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
            navigationPane1.SelectedPage = ngp_Query;

            DataTable planTable = SqlHelper.ExecuteQuery("SELECT F_ID, F_Title FROM T_Plan ORDER BY F_ID");
            DataRow allRow = planTable.NewRow();
            allRow[0] = "all"; allRow[1] = "全部计划";
            planTable.Rows.InsertAt(allRow, 0);
            cbo_PlanTypeList.DataSource = planTable;
            cbo_PlanTypeList.DisplayMember = "F_Title";
            cbo_PlanTypeList.ValueMember = "F_ID";

            DataTable orgTable = SqlHelper.GetCompanyList(); // SqlHelper.ExecuteQuery("SELECT F_ID, F_Title FROM T_SourceOrg ORDER BY F_ID");
            DataRow orgRow = orgTable.NewRow();
            orgRow[3] = "all"; orgRow[1] = "全部来源单位";
            orgTable.Rows.InsertAt(orgRow, 0);
            cbo_SourceOrg.DataSource = orgTable;
            cbo_SourceOrg.DisplayMember = "dd_name";
            cbo_SourceOrg.ValueMember = "dd_code";

            LoadDataListByPage(1, null);
            view2.DefaultCellStyle = DataGridViewStyleHelper.GetCellStyle();
            view_Log.DefaultCellStyle = DataGridViewStyleHelper.GetCellStyle();
            view2.ColumnHeadersDefaultCellStyle = DataGridViewStyleHelper.GetHeaderStyle();
            view_Log.ColumnHeadersDefaultCellStyle = DataGridViewStyleHelper.GetHeaderStyle();

            panel3.Bounds = new System.Drawing.Rectangle(0, panel3.Top, ngp_Query.Width, ngp_Query.Height - panel3.Top);
            panel1.Bounds = new System.Drawing.Rectangle(0, panel1.Top, ngp_Borrow.Width, ngp_Borrow.Height - panel1.Top);
            lbl_TotalFileAmount.Anchor = AnchorStyles.Right | AnchorStyles.Top;
        }

        /// <summary>
        /// 填充数据列表
        /// </summary>
        /// <param name="page">当前页码</param>
        /// <param name="querySQL">查询sql语句</param>
        private void CreateDataList(int page, string querySQL)
        {
            treeList1.Columns.Clear();
            treeList1.ClearNodes();
            treeList1.Columns.AddRange(new TreeListColumn[]
            {
                new TreeListColumn(){ Name = "id", Caption = "序号", Width = 10, Visible = true },
                new TreeListColumn(){ Name = "orgCode", Caption = "来源单位", Width = 100, Visible = true },
                new TreeListColumn(){ Name = "code", Caption = "项目/课题编号", Width = 100, Visible = true },
                new TreeListColumn(){ Name = "name", Caption = "项目/课题名称",  Width = 280, Visible = true },
                new TreeListColumn(){ Name = "sdate", Caption = "开始时间", Width = 60, Visible = true },
                new TreeListColumn(){ Name = "tcount", Caption = "课题/子课题数", Width = 20, Visible = true },
                new TreeListColumn(){ Name = "bcount", Caption = "盒数",  Width = 10 , Visible = true},
                new TreeListColumn(){ Name = "fcount", Caption = "文件数", Width = 10, Visible = true },
            });
            DataTable table = SqlHelper.ExecuteQuery(querySQL);
            int i = 0;
            foreach(DataRow row in table.Rows)
            {
                int tcount = ToolHelper.GetIntValue(GetTopicCount(row["pi_id"]));
                TreeListNode node = treeList1.Nodes.Add(new object[] { ++i, row["dd_name"], row["pi_code"], row["pi_name"], row["pi_start_datetime"], tcount, row["bcount"], GetFileCount(row["pi_id"]) });
                node.Tag = row["pi_id"];

                if(tcount > 0)
                {
                    string querySQL_Topic = "SELECT ti_id, ti_code, ti_name, ti_start_datetime, ti_funds, ti_source_id, ti_orga_id, COUNT(pb_id) bcount FROM (" +
                         "SELECT ti_id, ti_code, ti_name, ti_start_datetime, ti_funds, ti_source_id, ti_orga_id, pb_id " +
                         "FROM topic_info AS ti LEFT OUTER JOIN processing_box ON processing_box.pb_obj_id = ti.ti_id " +
                        $"WHERE ti_obj_id ='{row["pi_id"]}') A " +
                         "GROUP BY ti_id, ti_code, ti_name, ti_start_datetime, ti_funds, ti_source_id, ti_orga_id " +
                         "ORDER BY ti_code";
                    DataTable topTable = SqlHelper.ExecuteQuery(querySQL_Topic);
                    int j = 0;
                    foreach(DataRow topRow in topTable.Rows)
                    {
                        TreeListNode topNode = node.Nodes.Add(new object[] { i + "-" + ++j, topRow["ti_orga_id"], topRow["ti_code"], topRow["ti_name"], topRow["ti_start_datetime"], GetTopicCount(topRow["ti_id"]), topRow["bcount"], GetFileCount(topRow["ti_id"]) });
                    }
                }
            }
            txt_page.Text = ToolHelper.GetValue(page);
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
                    LoadDataListByPage(page, null);
                }
            }
            //上一页
            else if("btn_lpage".Equals(name))
            {
                int page = Convert.ToInt32(txt_page.Text) - 1;
                if(page > 0)
                {
                    LoadDataListByPage(page, null);
                }
            }
            //首页
            else if("btn_fpage".Equals(name))
            {
                int page = Convert.ToInt32(txt_page.Text);
                if(page > 1)
                {
                    LoadDataListByPage(1, null);
                }
            }
            //末页
            if("btn_epage".Equals(name))
            {
                int page = Convert.ToInt32(txt_page.Text);
                if(page < maxPage)
                {
                    LoadDataListByPage(maxPage, null);
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
                    LoadDataListByPage(value, null);
                }
            }
        }

        /// <summary>
        /// 加载指定页码的数据
        /// </summary>
        /// <param name="pageValue">指定页码</param>
        private void LoadDataListByPage(object pageValue, EventArgs e)
        {
            int page = ToolHelper.GetIntValue(pageValue, 1);
            object planType = cbo_PlanTypeList.SelectedValue;
            object orgType = cbo_SourceOrg.SelectedValue;
            string batchName = txt_BatchName.Text;
            string proCode = txt_ProjectCode.Text;
            string proName = txt_ProjectName.Text;
            string sDate = chk_allDate.Checked ? null : dtp_sDate.Text;
            string eDate = chk_allDate.Checked ? null : dtp_eDate.Text;

            LoadDataList(page, planType, batchName, proCode, proName, sDate, eDate, orgType);
        }

        /// <summary>
        /// 加载查询数据
        /// </summary>
        /// <param name="page">当前页码</param>
        private void LoadDataList(int page, object planType, string batchName, string proCode, string proName, string sDate, string eDate, object orgType)
        {
            string querySQL = $"SELECT TOP({pageSize}) A.* FROM( " +
                "SELECT pi_id, pi_code, pi_name, pi_start_datetime, pi_funds, pi_source_id, pi_orga_id FROM project_info WHERE pi_categor = 2 " +
                "UNION ALL SELECT ti_id, ti_code, ti_name, ti_start_datetime, ti_funds, ti_source_id, ti_orga_id FROM topic_info WHERE ti_categor = -3" +
                ") A " +
                "WHERE 1 = 1";
            if(!string.IsNullOrEmpty(proCode))
                querySQL += $"AND A.pi_code LIKE '%{proCode}%' ";
            if(!string.IsNullOrEmpty(proName))
                querySQL += $"AND A.pi_name LIKE '%{proName}%' ";
            if(!"all".Equals(planType))
                querySQL += $"AND A.pi_source_id = '{planType}' ";
            if(!"all".Equals(orgType))
                querySQL += $"AND A.pi_orga_id = '{orgType}' ";
            if(!string.IsNullOrEmpty(sDate))
                querySQL += $"AND A.pi_start_datetime >= '{sDate}' ";
            if(!string.IsNullOrEmpty(eDate))
                querySQL += $"AND A.pi_start_datetime <= '{eDate}' ";
            string totalQuerySQL = $"SELECT TOP({pageSize * (page - 1)}) B.pi_id FROM( " +
                "SELECT pi_id, pi_name, pi_code, pi_obj_id, pi_start_datetime, pi_source_id, pi_orga_id FROM project_info WHERE pi_categor = 2 " +
                "UNION ALL SELECT ti_id, ti_code, ti_name, ti_start_datetime, ti_funds, ti_source_id, ti_orga_id FROM topic_info WHERE ti_categor = -3" +
                ") B " +
                "WHERE 1 = 1 ";
            if(!string.IsNullOrEmpty(proCode))
                totalQuerySQL += $"AND B.pi_code LIKE '%{proCode}%' ";
            if(!string.IsNullOrEmpty(proName))
                totalQuerySQL += $"AND B.pi_name LIKE '%{proName}%' ";
            if(!"all".Equals(planType))
                totalQuerySQL += $"AND B.pi_source_id = '{planType}' ";
            if(!"all".Equals(orgType))
                totalQuerySQL += $"AND B.pi_orga_id = '{orgType}' ";
            if(!string.IsNullOrEmpty(sDate))
                totalQuerySQL += $"AND B.pi_start_datetime >= '{sDate}' ";
            if(!string.IsNullOrEmpty(eDate))
                totalQuerySQL += $"AND B.pi_start_datetime <= '{eDate}' ";
            //totalQuerySQL += "ORDER BY pi_code ";
            //querySQL += $"AND A.pi_id NOT IN ({totalQuerySQL}) ORDER BY pi_code ";
            querySQL += $"AND A.pi_id NOT IN ({totalQuerySQL}) ";

            //关联盒数
            querySQL = $"SELECT pi_id, pi_code, pi_name, pi_start_datetime, pi_funds, pi_source_id, dd_name, COUNT(pb_id) bcount FROM({querySQL}) C " +
                "LEFT JOIN processing_box ON C.pi_id=pb_obj_id " +
                "LEFT JOIN data_dictionary ON C.pi_orga_id=dd_code " +
                "GROUP BY C.pi_orga_id, pi_id, pi_code, pi_name, pi_start_datetime, pi_funds, pi_source_id, dd_name ";
                //"ORDER BY pi_code";
            CreateDataList(page, querySQL);

            if(page == 1)
            {
                string countQuerySQL = $"SELECT COUNT(A.pi_id) FROM( " +
                    "SELECT pi_id, pi_name, pi_code, pi_obj_id, pi_start_datetime, pi_source_id, pi_orga_id FROM project_info WHERE pi_categor = 2 " +
                    "UNION ALL SELECT ti_id, ti_code, ti_name, ti_start_datetime, ti_funds, ti_source_id, ti_orga_id FROM topic_info WHERE ti_categor = -3" +
                    ") A " +
                    //"UNION ALL SELECT ti_id, ti_name, ti_code, ti_obj_id, ti_start_datetime, ti_source_id FROM topic_info) A " +
                    "WHERE 1 = 1 ";
                if(!string.IsNullOrEmpty(proCode))
                    countQuerySQL += $"AND A.pi_code LIKE '%{proCode}%' ";
                if(!string.IsNullOrEmpty(proName))
                    countQuerySQL += $"AND A.pi_name LIKE '%{proName}%' ";
                if(!"all".Equals(planType))
                    countQuerySQL += $"AND A.pi_source_id = '{planType}' ";
                if(!"all".Equals(orgType))
                    countQuerySQL += $"AND A.pi_orga_id = '{orgType}' ";
                if(!string.IsNullOrEmpty(sDate))
                    countQuerySQL += $"AND A.pi_start_datetime >= '{sDate}' ";
                if(!string.IsNullOrEmpty(eDate))
                    countQuerySQL += $"AND A.pi_start_datetime <= '{eDate}' ";

                int totalSize = SqlHelper.ExecuteCountQuery(countQuerySQL);
                maxPage = totalSize % pageSize == 0 ? totalSize / pageSize : totalSize / pageSize + 1;
                label1.Text = $"共 {totalSize} 条记录，每页共 {pageSize} 条，共 {maxPage} 页";
            }

        }

        private void Btn_Reset_Click(object sender, EventArgs e)
        {
            cbo_PlanTypeList.SelectedIndex = 0;
            cbo_SourceOrg.SelectedIndex = 0;
            txt_BatchName.ResetText();
            txt_ProjectCode.ResetText();
            txt_ProjectName.ResetText();
            dtp_sDate.ResetText();
            dtp_eDate.ResetText();
            chk_allDate.Checked = true;
            LoadDataListByPage(1, null);
        }

        private void chk_allDate_CheckedChanged(object sender, EventArgs e)
        {
            dtp_sDate.Enabled = dtp_eDate.Enabled = !chk_allDate.Checked;
        }

        /// <summary>
        /// 加载指定项目/课题下的文件列表
        /// </summary>
        /// <param name="id"></param>
        private void LoadFileList(object id, string fname, string fcategor, string pcode, string pname)
        {
            view2.Rows.Clear();
            string querySQL = "SELECT TOP(1000) bl.bl_id, bl.bl_borrow_state, bl.bl_return_state, pfl.pfl_id, pfl.pfl_code, pfl.pfl_name, pb.pb_box_number " +
              "FROM processing_file_list pfl " +
              "LEFT JOIN (SELECT pi_id, pi_code, pi_name, pi_obj_id FROM project_info " +
              "UNION ALL SELECT ti_id, ti_code, ti_name, ti_obj_id FROM topic_info " +
              "UNION ALL SELECT si_id, si_code, si_name, si_obj_id FROM subject_info ) A ON A.pi_id = pfl.pfl_obj_id " +
              "LEFT JOIN processing_box pb ON pb.pb_id=pfl.pfl_box_id " +
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
                querySQL += $"AND A.pi_code LIKE '%{pcode}%' ";
            if(!string.IsNullOrEmpty(pname))
                querySQL += $"AND A.pi_name LIKE '%{pname}%' ";
            if(rdo_Out.Checked)
                querySQL += $"AND bl.bl_borrow_state = 1 ";
            else if(rdo_In.Checked)
                querySQL += $"AND (bl.bl_borrow_state = 0 OR bl.bl_borrow_state IS NULL) ";

            querySQL += "ORDER BY pfl.pfl_sort DESC, pfl.pfl_code";
            DataTable dataTable = SqlHelper.ExecuteQuery(querySQL);
            DataGridViewStyleHelper.ResetDataGridView(view2, true);
            view2.Columns.AddRange(new DataGridViewColumn[]
            {
                new DataGridViewTextBoxColumn(){ Name = "fid", HeaderText = "序号", FillWeight = 20, SortMode = DataGridViewColumnSortMode.NotSortable },
                new DataGridViewTextBoxColumn(){ Name = "fcode", HeaderText = "文件编号", FillWeight = 50, SortMode = DataGridViewColumnSortMode.NotSortable },
                new DataGridViewTextBoxColumn(){ Name = "fname", HeaderText = "文件名称", FillWeight = 120, SortMode = DataGridViewColumnSortMode.NotSortable },
                new DataGridViewTextBoxColumn(){ Name = "fbox", HeaderText = "盒号", FillWeight = 20, SortMode = DataGridViewColumnSortMode.NotSortable },
                new DataGridViewButtonColumn(){ Name = "fbstate", HeaderText = "借阅状态", FillWeight = 30, SortMode = DataGridViewColumnSortMode.NotSortable },
                new DataGridViewButtonColumn(){ Name = "frstate", HeaderText = "借阅状态", FillWeight = 30, SortMode = DataGridViewColumnSortMode.NotSortable },
            });
            foreach(DataRow row in dataTable.Rows)
            {
                int i = view2.Rows.Add();
                view2.Rows[i].Tag = row["pfl_id"];
                view2.Rows[i].Cells["fid"].Value = i + 1;
                view2.Rows[i].Cells["fcode"].Value = row["pfl_code"];
                view2.Rows[i].Cells["fname"].Value = row["pfl_name"];
                view2.Rows[i].Cells["fbox"].Value = row["pb_box_number"];
                view2.Rows[i].Cells["fbstate"].Tag = row["bl_id"];
                view2.Rows[i].Cells["fbstate"].Value = GetBorrowState(row["bl_borrow_state"]);
                view2.Rows[i].Cells["frstate"].Value = GetReturnState(row["bl_return_state"]);
            }
            view2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            view2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
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
                object fid = view2.Rows[e.RowIndex].Tag;    
                if("在库".Equals(view2.Rows[e.RowIndex].Cells[e.ColumnIndex].Value))
                {
                    Frm_BorrowEdit frm = new Frm_BorrowEdit(fid);
                    frm.txt_Real_Return_Date.Enabled = false;
                    if(frm.ShowDialog() == DialogResult.OK)
                    {
                        view2.Rows[e.RowIndex].Cells[e.ColumnIndex].Tag = frm.Tag;
                        view2.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = "借出";
                        view2.Rows[e.RowIndex].Cells["frstate"].Value = "未归还";
                    }
                }
            }
            //借阅状态(box)
            else if("borrow_state".Equals(columnName))
            {
                object boxId = view2.Rows[e.RowIndex].Tag;
                if("在库".Equals(view2.Rows[e.RowIndex].Cells[e.ColumnIndex].Value))
                {
                    Frm_BorrowEditBox frm = new Frm_BorrowEditBox(boxId);
                    frm.txt_Real_Return_Date.Enabled = false;
                    if(frm.ShowDialog() == DialogResult.OK)
                    {
                        view2.Rows[e.RowIndex].Cells[e.ColumnIndex].Tag = frm.Tag;
                        view2.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = "借出";
                        view2.Rows[e.RowIndex].Cells["return_state"].Value = "未归还";
                    }
                }
            }
            //归还状态
            else if("frstate".Equals(columnName))
            {
                object fid = view2.Rows[e.RowIndex].Tag;
                if("未归还".Equals(view2.Rows[e.RowIndex].Cells[e.ColumnIndex].Value))
                {
                    object id = view2.Rows[e.RowIndex].Cells["fbstate"].Tag;
                    DataRow row = SqlHelper.ExecuteSingleRowQuery($"SELECT * FROM borrow_log WHERE bl_id='{id}'");
                    if(row != null)
                    {
                        Frm_BorrowEdit frm = new Frm_BorrowEdit(fid);
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
            //归还状态（box）
            else if("return_state".Equals(columnName))
            {
                object boxId = view2.Rows[e.RowIndex].Tag;
                if("未归还".Equals(view2.Rows[e.RowIndex].Cells[e.ColumnIndex].Value))
                {
                    object id = view2.Rows[e.RowIndex].Cells["return_state"].Tag;
                    DataRow row = SqlHelper.ExecuteSingleRowQuery($"SELECT * FROM borrow_log WHERE bl_id='{id}'");
                    if(row != null)
                    {
                        Frm_BorrowEditBox frm = new Frm_BorrowEditBox(boxId);
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
                        frm.lbl_Code.Tag = id;
                        frm.btn_Sure.Text = "确认归还";
                        frm.txt_Real_Return_Date.Focus();
                        if(frm.ShowDialog() == DialogResult.OK)
                        {
                            view2.Rows[e.RowIndex].Cells[e.ColumnIndex].Tag = frm.Tag;
                            view2.Rows[e.RowIndex].Cells["return_state"].Value = "在库";
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
            if(frm_FirstPage != null && !frm_FirstPage.IsDisposed)
                frm_FirstPage.Show();
        }

        private void treeList1_NodeCellStyle(object sender, GetCustomNodeCellStyleEventArgs e)
        {
            if(e.Node.Level > 0)
            {
                e.Appearance.BackColor = System.Drawing.Color.AliceBlue;
            }
        }

        private void treeList1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            TreeList tree = sender as TreeList;
            string columnName = tree.FocusedColumn.Name;
            //文件数
            if("fcount".Equals(columnName))
            {
                txt_FileName.Tag = tree.FocusedNode.Tag;
                txt_Pcode.Text = tree.FocusedNode.GetDisplayText(2);
                rdo_type_file.Checked = true;
                Btn_FileQuery_Click(null, null);
                navigationPane1.SelectedPage = ngp_Borrow;
            }
            //盒数
            if("bcount".Equals(columnName))
            {
                txt_FileName.Tag = tree.FocusedNode.Tag;
                txt_Pcode.Text = tree.FocusedNode.GetDisplayText(2);
                rdo_type_box.Checked = true;
                LoadBoxListById(tree.FocusedNode.Tag);
                navigationPane1.SelectedPage = ngp_Borrow;
            }
            //名称
            else if("name".Equals(columnName))
            {
                object id = tree.FocusedNode.Tag;
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
        /// 按盒加载数据
        /// </summary>
        /// <param name="id">项目/课题ID</param>
        private void LoadBoxListById(object id)
        {
            DataGridViewStyleHelper.ResetDataGridView(view2, true);
            view2.Columns.AddRange(new DataGridViewColumn[]
            {
                new DataGridViewTextBoxColumn(){ Name = "id", HeaderText = "序号", FillWeight = 20, SortMode = DataGridViewColumnSortMode.NotSortable },
                new DataGridViewTextBoxColumn(){ Name = "code", HeaderText = "项目/课题编号", FillWeight = 80, SortMode = DataGridViewColumnSortMode.NotSortable },
                new DataGridViewTextBoxColumn(){ Name = "gc", HeaderText = "馆藏号", FillWeight = 50, SortMode = DataGridViewColumnSortMode.NotSortable },
                new DataGridViewTextBoxColumn(){ Name = "box", HeaderText = "盒号", FillWeight = 50, SortMode = DataGridViewColumnSortMode.NotSortable },
                new DataGridViewTextBoxColumn(){ Name = "files", HeaderText = "文件数", FillWeight = 30, SortMode = DataGridViewColumnSortMode.NotSortable },
                new DataGridViewButtonColumn(){ Name = "borrow_state", HeaderText = "借阅状态", FillWeight = 30, SortMode = DataGridViewColumnSortMode.NotSortable },
                new DataGridViewButtonColumn(){ Name = "return_state", HeaderText = "归还状态", FillWeight = 30, SortMode = DataGridViewColumnSortMode.NotSortable },
            });
            string querySQL = $"SELECT A.pi_code, pb_id, pb_gc_id, pb_box_number, COUNT(pfl_id) num, bl_borrow_state, bl_return_state, bl_id FROM( " +
                 "SELECT pi_id, pi_code FROM project_info " +
                 "UNION ALL SELECT ti_id, ti_code FROM topic_info " +
                 "UNION ALL SELECT si_id, si_code FROM subject_info) A " +
                 "LEFT JOIN processing_box ON pb_obj_id = A.pi_id " +
                 "LEFT JOIN processing_file_list ON pfl_box_id = pb_id " +
                 "LEFT JOIN (SELECT * FROM (SELECT rowid = ROW_NUMBER() OVER (PARTITION BY bl_file_id ORDER BY bl_date DESC), * FROM borrow_log) A WHERE rowid = 1) bl ON bl.bl_file_id=pb_id " +
                $"WHERE A.pi_id = '{id}' AND pb_id IS NOT NULL " +
                 "GROUP BY A.pi_code, pb_id, pb_gc_id, pb_box_number, bl_borrow_state, bl_return_state, bl_id ";
            DataTable table = SqlHelper.ExecuteQuery(querySQL);
            foreach(DataRow row in table.Rows)
            {
                int i = view2.Rows.Add();
                view2.Rows[i].Cells["id"].Value = i + 1;
                view2.Rows[i].Tag = row["pb_id"];
                view2.Rows[i].Cells["code"].Value = row["pi_code"];
                view2.Rows[i].Cells["gc"].Value = row["pb_gc_id"];
                view2.Rows[i].Cells["box"].Value = row["pb_box_number"];
                view2.Rows[i].Cells["files"].Value = row["num"];
                view2.Rows[i].Cells["borrow_state"].Value = GetBorrowState(row["bl_borrow_state"]);
                view2.Rows[i].Cells["return_state"].Tag = row["bl_id"];
                view2.Rows[i].Cells["return_state"].Value = GetReturnState(row["bl_return_state"]);
            }
            view2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            view2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void navigationPane1_SelectedPageChanged(object sender, DevExpress.XtraBars.Navigation.SelectedPageChangedEventArgs e)
        {
            if(e.Page == ngp_Log)
            {
                LoadBorrowLog(null);
            }
        }

        /// <summary>
        /// 借阅历史记录
        /// </summary>
        /// <param name="page">页码</param>
        private void LoadBorrowLog(string key)
        {
            view_Log.Rows.Clear();
            if(!string.IsNullOrEmpty(key))
                key = $"WHERE bl_code LIKE '%{key}%'";
            string querySQL = $"SELECT TOP(1000) * FROM borrow_log {key} ORDER BY bl_code DESC ";
            DataTable table = SqlHelper.ExecuteQuery(querySQL);
            foreach(DataRow row in table.Rows)
            {
                int i = view_Log.Rows.Add();
                view_Log.Rows[i].Cells["id"].Value = i + 1;
                view_Log.Rows[i].Cells["code"].Value = row["bl_code"];
                view_Log.Rows[i].Cells["date"].Value = ToolHelper.GetDateValue(row["bl_date"], "yyyy-MM-dd HH:mm");
                view_Log.Rows[i].Cells["unit"].Value = row["bl_user_unit"];
                view_Log.Rows[i].Cells["user"].Value = row["bl_user"];
                view_Log.Rows[i].Cells["state"].Value = GetReturnState(row["bl_return_state"]);
            }
        }

        private void btn_LogQuery_Click(object sender, EventArgs e)
        {
            string searchCode = log_SearchCode.Text.Trim();
            LoadBorrowLog(searchCode);
        }
    }
}
