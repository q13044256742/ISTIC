using DevExpress.XtraEditors;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Columns;
using DevExpress.XtraTreeList.Nodes;
using System;
using System.Data;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using 科技计划项目档案数据采集管理系统.FirstPage;

namespace 科技计划项目档案数据采集管理系统
{
    public partial class Frm_QueryBorrowing : XtraForm
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

            view2.DefaultCellStyle = DataGridViewStyleHelper.GetCellStyle();
            view_Log.DefaultCellStyle = DataGridViewStyleHelper.GetCellStyle();
            view2.ColumnHeadersDefaultCellStyle = DataGridViewStyleHelper.GetHeaderStyle();
            view_Log.ColumnHeadersDefaultCellStyle = DataGridViewStyleHelper.GetHeaderStyle();

            panel3.Bounds = new System.Drawing.Rectangle(0, panel3.Top, ngp_Query.Width, ngp_Query.Height - panel3.Top);
            panel1.Bounds = new System.Drawing.Rectangle(0, panel1.Top, ngp_Borrow.Width, ngp_Borrow.Height - panel1.Top);
            lbl_TotalFileAmount.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            btn_Refresh.Anchor = AnchorStyles.Right | AnchorStyles.Top;

            //全部计划（重大专项）
            DataTable planTable = SqlHelper.ExecuteQuery("SELECT dd_code, dd_name FROM data_dictionary WHERE dd_pId= " +
                "(SELECT dd_id FROM data_dictionary WHERE dd_code = 'dic_key_plan') " +
                "ORDER BY dd_sort");
            //全部--
            DataRow allRow = planTable.NewRow();
            allRow[0] = "all"; allRow[1] = "全部计划";
            planTable.Rows.InsertAt(allRow, 0);
            //10个专项
            DataTable speTable = SqlHelper.ExecuteQuery($"SELECT dd_code, dd_name FROM data_dictionary WHERE dd_pId=(SELECT dd_id FROM data_dictionary WHERE dd_code='dic_key_project') ORDER BY dd_sort");
            foreach(DataRow dataRow in speTable.Rows)
                planTable.ImportRow(dataRow);

            cbo_PlanTypeList.DataSource = planTable;
            cbo_PlanTypeList.DisplayMember = "dd_name";
            cbo_PlanTypeList.ValueMember = "dd_code";

            DataTable orgTable = SqlHelper.GetCompanyList(); // SqlHelper.ExecuteQuery("SELECT F_ID, F_Title FROM T_SourceOrg ORDER BY F_ID");
            DataRow orgRow = orgTable.NewRow();
            orgRow[3] = "all"; orgRow[1] = "全部来源单位";
            orgTable.Rows.InsertAt(orgRow, 0);
            cbo_SourceOrg.DataSource = orgTable;
            cbo_SourceOrg.DisplayMember = "dd_name";
            cbo_SourceOrg.ValueMember = "dd_code";
        }

        public Frm_QueryBorrowing(Frm_FirstPage frm_FirstPage) : this()
        {
            this.frm_FirstPage = frm_FirstPage;
        }

        private void Frm_QueryBorrowing_Load(object sender, EventArgs e)
        {
            navigationPane1.SelectedPage = ngp_Query;
            ngp_Query.Select();
            LoadDataListByPage(1, null);
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
                new TreeListColumn(){ Name = "batchCode", Caption = "批次号", Width = 60, Visible = true },
                new TreeListColumn(){ Name = "tcount", Caption = "课题/子课题数", Width = 20, Visible = true },
                new TreeListColumn(){ Name = "bcount", Caption = "盒数",  Width = 10 , Visible = true},
                new TreeListColumn(){ Name = "fcount", Caption = "文件数", Width = 10, Visible = true },
            });
            DataTable table = SqlHelper.ExecuteQuery(querySQL);
            int i = 0;
            foreach(DataRow row in table.Rows)
            {
                int tcount = ToolHelper.GetIntValue(GetTopicCount(row["pi_id"]));
                object batchCode = GetBatchCode(row["pi_id"]);
                TreeListNode node = treeList1.Nodes.Add(new object[] { ++i, row["dd_name"], row["pi_code"], row["pi_name"], batchCode, tcount, row["bcount"], GetFileCount(row["pi_id"]) });
                node.Tag = row["pi_id"];

                if(tcount > 0)
                {
                    string querySQL_Topic = "SELECT ti_id, ti_code, ti_name, ISNULL(subCount,0) subCount, ISNULL(boxCount,0) boxCount, ISNULL(fileCount,0) fileCount FROM (" +
                        "SELECT ti_id, ti_code, ti_name, ti_obj_id, COUNT(pb_id) boxCount FROM ( SELECT * FROM ( " +
                        "SELECT ti_id, ti_code, ti_name, ti_obj_id, ti_categor FROM topic_info UNION ALL " +
                        "SELECT si_id, si_code, si_name, si_obj_id, si_categor FROM subject_info) A LEFT OUTER JOIN processing_box pb ON pb.pb_obj_id = ti_id) AS A " +
                        "GROUP BY ti_id, ti_code, ti_name, ti_obj_id) M LEFT JOIN ( " +
                        "SELECT si_obj_id, COUNT(si_id) subCount FROM subject_info GROUP BY si_obj_id) N ON M.ti_id = N.si_obj_id " +
                        "LEFT JOIN (SELECT pfl_obj_id, COUNT(pfl_id) fileCount FROM processing_file_list GROUP BY pfl_obj_id) X ON M.ti_id = X.pfl_obj_id " +
                       $"WHERE ti_obj_id='{row["pi_id"]}' ORDER BY ti_code";
                    DataTable topTable = SqlHelper.ExecuteQuery(querySQL_Topic);
                    int j = 0;
                    foreach(DataRow topRow in topTable.Rows)
                    {
                        TreeListNode topNode = node.Nodes.Add(new object[] { i + "-" + ++j, row["dd_name"], topRow["ti_code"], topRow["ti_name"], batchCode, topRow["subCount"], topRow["boxCount"], topRow["fileCount"] });
                        topNode.Tag = topRow["ti_id"];
                    }
                }
            }
            txt_page.Text = ToolHelper.GetValue(page);
        }

        /// <summary>
        /// 根据指定对象获取批次号
        /// </summary>
        private object GetBatchCode(object objId)
        {
            object result = null;
            string querySql = "SELECT trp.trp_code FROM (" +
                "SELECT pi_id, pi_obj_id FROM project_info WHERE pi_categor=2 UNION ALL " +
                "SELECT ti_id, ti_obj_id FROM topic_info WHERE ti_categor=-3) p1 " +
                "LEFT JOIN project_info p2 ON p2.pi_categor = 1 AND p1.pi_obj_id = p2.pi_id " +
                "LEFT JOIN transfer_registration_pc trp ON p2.trc_id = trp.trp_id " +
               $"WHERE p1.pi_id='{objId}'";
            result = SqlHelper.ExecuteOnlyOneQuery(querySql);
            if(result == null)
            {
                 querySql = "SELECT trp.trp_code FROM (" +
                    "SELECT pi_id, pi_obj_id FROM project_info WHERE pi_categor=2 UNION ALL " +
                    "SELECT ti_id, ti_obj_id FROM topic_info WHERE ti_categor=-3) p1 " +
                    "LEFT JOIN imp_dev_info idi ON idi.imp_id = p1.pi_obj_id " +
                    "LEFT JOIN imp_info ii ON idi.imp_obj_id = ii.imp_id " +
                    "LEFT JOIN transfer_registration_pc trp ON ii.imp_obj_id = trp.trp_id " +
                   $"WHERE p1.pi_id='{objId}'";
                result = SqlHelper.ExecuteOnlyOneQuery(querySql);
            }
            return result;
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
        public void LoadDataListByPage(object pageValue, EventArgs e)
        {
            int page = ToolHelper.GetIntValue(pageValue, 1);
            object planType = cbo_PlanTypeList.SelectedValue;
            object orgType = cbo_SourceOrg.SelectedValue;
            string proCode = txt_ProjectCode.Text;
            string proName = txt_ProjectName.Text;
            string sDate = chk_allDate.Checked ? null : dtp_sDate.Text;
            string eDate = chk_allDate.Checked ? null : dtp_eDate.Text;

            string batchCode = txt_BatchCode.Text;
            if(string.IsNullOrEmpty(batchCode))
                LoadDataList(page, ToolHelper.GetValue(planType), proCode, proName, sDate, eDate, orgType);
            else
                LoadDataListByBatchCode(page, ToolHelper.GetValue(planType), batchCode, proCode, proName, sDate, eDate, orgType);
        }

        /// <summary>
        /// 加载查询数据
        /// </summary>
        /// <param name="page">当前页码</param>
        private void LoadDataList(int page, string planType, string proCode, string proName, string sDate, string eDate, object orgType)
        {
            string querySQL = $"SELECT ROW_NUMBER() OVER(ORDER BY pi_orga_id DESC, pi_code) ID, A.* FROM( " +
                "SELECT pi_id, pi_code, pi_name, pi_start_datetime, pi_source_id, pi_orga_id FROM project_info WHERE pi_categor = 2 UNION ALL " +
                "SELECT ti_id, ti_code, ti_name, ti_start_datetime, ti_source_id, ti_orga_id FROM topic_info WHERE ti_categor = -3" +
               $") A WHERE LEN(A.pi_orga_id)>0 AND LEN(A.pi_source_id)>0 ";
            if(!string.IsNullOrEmpty(proCode))
                querySQL += $"AND pi_code LIKE '%{proCode}%' ";
            if(!string.IsNullOrEmpty(proName))
                querySQL += $"AND pi_name LIKE '%{proName}%' ";
            if("ZX".Equals(planType))//全部重大专项
                querySQL += $"AND pi_source_id LIKE 'ZX__' ";
            else if(!"all".Equals(planType))//普通计划
                querySQL += $"AND pi_source_id = '{planType}' ";
            if(!"all".Equals(orgType))
                querySQL += $"AND pi_orga_id = '{orgType}' ";
            if(!string.IsNullOrEmpty(sDate))
                querySQL += $"AND pi_start_datetime >= '{sDate}' ";
            if(!string.IsNullOrEmpty(eDate))
                querySQL += $"AND pi_start_datetime <= '{eDate}' ";
            //分页
            querySQL = $"SELECT * FROM ({querySQL}) B WHERE ID BETWEEN {(page - 1) * pageSize + 1} AND {(page - 1) * pageSize + pageSize}";
            //关联盒数
            querySQL = $"SELECT pi_id, pi_code, pi_name, pi_start_datetime, pi_source_id, dd_name, COUNT(pb_id) bcount FROM({querySQL}) C " +
                "LEFT JOIN processing_box ON C.pi_id=pb_obj_id " +
                "LEFT JOIN data_dictionary ON C.pi_orga_id=dd_code " +
                "GROUP BY ID, C.pi_orga_id, pi_id, pi_code, pi_name, pi_start_datetime, pi_source_id, dd_name " +
                "ORDER BY ID ";
            CreateDataList(page, querySQL);

            if(page == 1)
            {
                string countQuerySQL = "SELECT COUNT(A.pi_id) FROM( " +
                    "SELECT pi_id, pi_name, pi_code, pi_obj_id, pi_start_datetime, pi_source_id, pi_orga_id FROM project_info WHERE pi_categor = 2 UNION ALL " +
                    "SELECT ti_id, ti_name, ti_code, ti_obj_id, ti_start_datetime, ti_source_id, ti_orga_id FROM topic_info WHERE ti_categor = -3" +
                    ") A WHERE LEN(A.pi_source_id)>0 AND LEN(A.pi_orga_id)>0 ";
                string countQuerySQL2 = "SELECT COUNT(ti_id) FROM topic_info WHERE ti_categor=3 AND LEN(ti_source_id)>0 AND LEN(ti_orga_id)>0 ";
                if(!string.IsNullOrEmpty(proCode))
                {
                    countQuerySQL += $"AND A.pi_code LIKE '%{proCode}%' ";
                    countQuerySQL2 += $"AND ti_code LIKE '%{proCode}%' ";
                }
                if(!string.IsNullOrEmpty(proName))
                {
                    countQuerySQL += $"AND A.pi_name LIKE '%{proName}%' ";
                    countQuerySQL2 += $"AND ti_name LIKE '%{proName}%' ";
                }
                if("ZX".Equals(planType))//全部重大专项
                {
                    countQuerySQL += $"AND pi_source_id LIKE 'ZX__' ";
                    countQuerySQL2 += $"AND ti_source_id LIKE 'ZX__' ";
                }
                else if(!"all".Equals(planType))//普通计划
                {
                    countQuerySQL += $"AND A.pi_source_id = '{planType}' ";
                    countQuerySQL2 += $"AND ti_source_id = '{planType}' ";
                }
                if(!"all".Equals(orgType))
                {
                    countQuerySQL += $"AND A.pi_orga_id = '{orgType}' ";
                    countQuerySQL2 += $"AND ti_orga_id = '{orgType}' ";
                }
                if(!string.IsNullOrEmpty(sDate))
                {
                    countQuerySQL += $"AND A.pi_start_datetime >= '{sDate}' ";
                    countQuerySQL2 += $"AND ti_start_datetime >= '{sDate}' ";
                }
                if(!string.IsNullOrEmpty(eDate))
                {
                    countQuerySQL += $"AND A.pi_start_datetime <= '{eDate}' ";
                    countQuerySQL2 += $"AND ti_start_datetime <= '{eDate}' ";
                }
                int totalProjectSize = SqlHelper.ExecuteCountQuery(countQuerySQL);
                int totalTopicSize = SqlHelper.ExecuteCountQuery(countQuerySQL2);
                maxPage = totalProjectSize % pageSize == 0 ? totalProjectSize / pageSize : totalProjectSize / pageSize + 1;
                label1.Text = $"合计{totalProjectSize + totalTopicSize}条记录, 其中{totalProjectSize}个项目/课题, {totalTopicSize}个课题/子课题  每页共 {pageSize} 条，共 {maxPage} 页";
            }

        }

        private void LoadDataListByBatchCode(int page, string planType, string batchCode, string proCode, string proName, string sDate, string eDate, object orgType)
        {
            string querySQL = "SELECT ROW_NUMBER() OVER(ORDER BY A.pi_orga_id DESC, A.pi_code) ID, * FROM( " +
                 "SELECT trp_code, A.* FROM transfer_registration_pc trp " +
                 "LEFT JOIN project_info p1 ON p1.trc_id=trp.trp_id " +
                 "LEFT JOIN ( " +
                 "SELECT pi_id, pi_code, pi_name, pi_start_datetime, pi_source_id, pi_orga_id, pi_obj_id FROM project_info WHERE pi_categor = 2 UNION ALL " +
                 "SELECT ti_id, ti_code, ti_name, ti_start_datetime, ti_source_id, ti_orga_id, ti_obj_id FROM topic_info WHERE ti_categor = -3) A ON p1.pi_id=A.pi_obj_id " +
                 "UNION ALL " +
                 "SELECT trp_code, A.* FROM transfer_registration_pc trp " +
                 "LEFT JOIN imp_info ii ON ii.imp_obj_id = trp.trp_id " +
                 "LEFT JOIN imp_dev_info idi ON idi.imp_obj_id = ii.imp_id " +
                 "LEFT JOIN ( " +
                 "SELECT pi_id, pi_code, pi_name, pi_start_datetime, pi_source_id, pi_orga_id, pi_obj_id FROM project_info WHERE pi_categor = 2 UNION ALL " +
                 "SELECT ti_id, ti_code, ti_name, ti_start_datetime, ti_source_id, ti_orga_id, ti_obj_id FROM topic_info WHERE ti_categor = -3 ) A ON idi.imp_id =A.pi_obj_id " +
                $") A WHERE trp_code LIKE '%{batchCode}%' AND LEN(A.pi_orga_id)>0 AND LEN(A.pi_source_id)>0 ";
            if(!string.IsNullOrEmpty(proCode))
                querySQL += $"AND pi_code LIKE '%{proCode}%' ";
            if(!string.IsNullOrEmpty(proName))
                querySQL += $"AND pi_name LIKE '%{proName}%' ";
            if("ZX".Equals(planType))//全部重大专项
                querySQL += $"AND pi_source_id LIKE 'ZX__' ";
            else if(!"all".Equals(planType))//普通计划
                querySQL += $"AND pi_source_id = '{planType}' ";
            if(!"all".Equals(orgType))
                querySQL += $"AND pi_orga_id = '{orgType}' ";
            if(!string.IsNullOrEmpty(sDate))
                querySQL += $"AND pi_start_datetime >= '{sDate}' ";
            if(!string.IsNullOrEmpty(eDate))
                querySQL += $"AND pi_start_datetime <= '{eDate}' ";
            string querySqlByPage = querySQL;
            //分页
            querySQL = $"SELECT * FROM ({querySQL}) B WHERE ID BETWEEN {(page - 1) * pageSize + 1} AND {(page - 1) * pageSize + pageSize}";
            //关联盒数
            querySQL = $"SELECT pi_id, pi_code, pi_name, pi_start_datetime, pi_source_id, dd_name, COUNT(pb_id) bcount FROM({querySQL}) C " +
                "LEFT JOIN processing_box ON C.pi_id=pb_obj_id " +
                "LEFT JOIN data_dictionary ON C.pi_orga_id=dd_code " +
                "GROUP BY ID, C.pi_orga_id, pi_id, pi_code, pi_name, pi_start_datetime, pi_source_id, dd_name " +
                "ORDER BY ID ";
            CreateDataList(page, querySQL);

            if(page == 1)
            {
                string countQuerySQL = "SELECT COUNT(pi_id) FROM( " +
                    "SELECT trp_code, A.* FROM transfer_registration_pc trp " +
                    "LEFT JOIN project_info p1 ON p1.trc_id=trp.trp_id " +
                    "LEFT JOIN ( " +
                    "SELECT pi_id, pi_code, pi_name, pi_start_datetime, pi_source_id, pi_orga_id, pi_obj_id FROM project_info WHERE pi_categor = 2 UNION ALL " +
                    "SELECT ti_id, ti_code, ti_name, ti_start_datetime, ti_source_id, ti_orga_id, ti_obj_id FROM topic_info WHERE ti_categor = -3) A ON p1.pi_id=A.pi_obj_id " +
                    "UNION ALL " +
                    "SELECT trp_code, A.* FROM transfer_registration_pc trp " +
                    "LEFT JOIN imp_info ii ON ii.imp_obj_id = trp.trp_id " +
                    "LEFT JOIN imp_dev_info idi ON idi.imp_obj_id = ii.imp_id " +
                    "LEFT JOIN ( " +
                    "SELECT pi_id, pi_code, pi_name, pi_start_datetime, pi_source_id, pi_orga_id, pi_obj_id FROM project_info WHERE pi_categor = 2 UNION ALL " +
                    "SELECT ti_id, ti_code, ti_name, ti_start_datetime, ti_source_id, ti_orga_id, ti_obj_id FROM topic_info WHERE ti_categor = -3 ) A ON idi.imp_id =A.pi_obj_id) A " +
                   $"WHERE trp_code LIKE '%{batchCode}%' AND LEN(A.pi_orga_id)>0 AND LEN(A.pi_source_id)>0 ";
                string countQuerySQL2 = $"SELECT COUNT(ti_id) FROM ({querySqlByPage}) A LEFT JOIN (" +
                     "SELECT ti_id, ti_code, ti_name, ti_start_datetime, ti_obj_id, ti_source_id, ti_orga_id FROM topic_info UNION ALL " +
                     "SELECT si_id, si_code, si_name, si_start_datetime, si_obj_id, si_source_id, si_orga_id FROM subject_info) B ON A.pi_id = B.ti_obj_id " +
                     "WHERE B.ti_id IS NOT NULL AND LEN(B.ti_source_id)>0 AND LEN(B.ti_orga_id)>0 ";
                if(!string.IsNullOrEmpty(proCode))
                {
                    countQuerySQL += $"AND A.pi_code LIKE '%{proCode}%' ";
                    countQuerySQL2 += $"AND ti_code LIKE '%{proCode}%' ";
                }
                if(!string.IsNullOrEmpty(proName))
                {
                    countQuerySQL += $"AND A.pi_name LIKE '%{proName}%' ";
                    countQuerySQL2 += $"AND ti_name LIKE '%{proName}%' ";
                }
                if("ZX".Equals(planType))//全部重大专项
                {
                    countQuerySQL += $"AND pi_source_id LIKE 'ZX__' ";
                    countQuerySQL2 += $"AND ti_source_id LIKE 'ZX__' ";
                }
                else if(!"all".Equals(planType))//普通计划
                {
                    countQuerySQL += $"AND A.pi_source_id = '{planType}' ";
                    countQuerySQL2 += $"AND ti_source_id = '{planType}' ";
                }
                if(!"all".Equals(orgType))
                {
                    countQuerySQL += $"AND A.pi_orga_id = '{orgType}' ";
                    countQuerySQL2 += $"AND ti_orga_id = '{orgType}' ";
                }
                if(!string.IsNullOrEmpty(sDate))
                {
                    countQuerySQL += $"AND A.pi_start_datetime >= '{sDate}' ";
                    countQuerySQL2 += $"AND ti_start_datetime >= '{sDate}' ";
                }
                if(!string.IsNullOrEmpty(eDate))
                {
                    countQuerySQL += $"AND A.pi_start_datetime <= '{eDate}' ";
                    countQuerySQL2 += $"AND ti_start_datetime <= '{eDate}' ";
                }
                int totalProjectSize = SqlHelper.ExecuteCountQuery(countQuerySQL);
                int totalTopicSize = SqlHelper.ExecuteCountQuery(countQuerySQL2);
                maxPage = totalProjectSize % pageSize == 0 ? totalProjectSize / pageSize : totalProjectSize / pageSize + 1;
                label1.Text = $"合计{totalProjectSize + totalTopicSize}条记录, 其中{totalProjectSize}个项目/课题, {totalTopicSize}个课题/子课题  每页共 {pageSize} 条，共 {maxPage} 页";
            }

        }

        private void Btn_Reset_Click(object sender, EventArgs e)
        {
            cbo_PlanTypeList.SelectedIndex = 0;
            cbo_SourceOrg.SelectedIndex = 0;
            txt_BatchCode.ResetText();
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
            string querySQL = "SELECT TOP(1000) A.pi_code, pb.pb_gc_id, bl.bl_id, bl.bl_borrow_state, pfl.pfl_id, pfl.pfl_code, pfl.pfl_name, pb.pb_box_number " +
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
                querySQL += $"AND (bl.bl_borrow_state <> 1) ";

            querySQL += "ORDER BY A.pi_code, pb.pb_box_number, TRY_CAST(pfl_code AS int), pfl_code";
            DataTable dataTable = SqlHelper.ExecuteQuery(querySQL);
            DataGridViewStyleHelper.ResetDataGridView(view2, true);
            view2.Columns.AddRange(new DataGridViewColumn[]
            {
                new DataGridViewTextBoxColumn(){ Name = "fid", HeaderText = "序号", FillWeight = 20, SortMode = DataGridViewColumnSortMode.NotSortable },
                new DataGridViewTextBoxColumn(){ Name = "fpcode", HeaderText = "项目/课题编号", FillWeight = 50 },
                new DataGridViewTextBoxColumn(){ Name = "fcode", HeaderText = "文件编号", FillWeight = 30 },
                new DataGridViewTextBoxColumn(){ Name = "fname", HeaderText = "文件名称", FillWeight = 120, SortMode = DataGridViewColumnSortMode.NotSortable },
                new DataGridViewTextBoxColumn(){ Name = "fgc", HeaderText = "馆藏号", FillWeight = 40, SortMode = DataGridViewColumnSortMode.NotSortable },
                new DataGridViewTextBoxColumn(){ Name = "fbox", HeaderText = "盒号", FillWeight = 20, ValueType= typeof(int) },
                new DataGridViewButtonColumn(){ Name = "fbstate", HeaderText = "借阅状态", FillWeight = 30, SortMode = DataGridViewColumnSortMode.NotSortable }
            });
            foreach(DataRow row in dataTable.Rows)
            {
                int i = view2.Rows.Add();
                view2.Rows[i].Tag = row["pfl_id"];
                view2.Rows[i].Cells["fpcode"].Value = row["pi_code"];
                view2.Rows[i].Cells["fid"].Value = i + 1;
                view2.Rows[i].Cells["fcode"].Value = row["pfl_code"];
                view2.Rows[i].Cells["fname"].Value = row["pfl_name"];
                view2.Rows[i].Cells["fgc"].Value = row["pb_gc_id"];
                view2.Rows[i].Cells["fbox"].Value = row["pb_box_number"];
                view2.Rows[i].Cells["fbstate"].Tag = row["bl_id"];
                view2.Rows[i].Cells["fbstate"].Value = GetBorrowState(row["bl_borrow_state"]);
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
                    Frm_BorrowEdit frm = new Frm_BorrowEdit(fid, null, false);
                    frm.txt_Real_Return_Date.Enabled = false;
                    if(frm.ShowDialog() == DialogResult.OK)
                    {
                        view2.Rows[e.RowIndex].Cells[e.ColumnIndex].Tag = frm.Tag;
                        view2.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = "借出";
                    }
                }
                else if("借出".Equals(view2.Rows[e.RowIndex].Cells[e.ColumnIndex].Value))
                {
                    object id = view2.Rows[e.RowIndex].Cells["fbstate"].Tag;
                    DataRow row = SqlHelper.ExecuteSingleRowQuery($"SELECT * FROM borrow_log WHERE bl_id='{id}'");
                    if(row != null)
                    {
                        Frm_BorrowEdit frm = new Frm_BorrowEdit(fid, null, false);
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
                            view2.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = "在库";
                        }
                    }
                }
            }
            //借阅状态(box)
            else if("borrow_state".Equals(columnName))
            {
                object boxId = view2.Rows[e.RowIndex].Tag;
                if("在库".Equals(view2.Rows[e.RowIndex].Cells[e.ColumnIndex].Value))
                {
                    Frm_BorrowEditBox frm = new Frm_BorrowEditBox(boxId, null, false);
                    frm.txt_Real_Return_Date.Enabled = false;
                    if(frm.ShowDialog() == DialogResult.OK)
                    {
                        view2.Rows[e.RowIndex].Cells[e.ColumnIndex].Tag = frm.Tag;
                        view2.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = "借出";
                    }
                }
                else if("借出".Equals(view2.Rows[e.RowIndex].Cells[e.ColumnIndex].Value))
                {
                    object id = view2.Rows[e.RowIndex].Cells["return_state"].Tag;
                    DataRow row = SqlHelper.ExecuteSingleRowQuery($"SELECT * FROM borrow_log WHERE bl_id='{id}'");
                    if(row != null)
                    {
                        Frm_BorrowEditBox frm = new Frm_BorrowEditBox(boxId, null, false);
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
                            view2.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = "在库";
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

                Frm_ProDetails details = GetFormHelper.GetProDetails(id);
                details.Show();
                details.Activate();
                //DataRow data = SqlHelper.ExecuteSingleRowQuery($"SELECT * FROM project_info WHERE pi_id='{id}' UNION ALL " +
                //    $"SELECT * FROM topic_info WHERE ti_id='{id}'");
                //if(data != null)
                //{
                //    Frm_QueryDetail detail = new Frm_QueryDetail(data);
                //    detail.ShowDialog();
                //}
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
            });
            string querySQL = $"SELECT A.pi_code, pb_id, pb_gc_id, pb_box_number, COUNT(pfl_id) num, bl_borrow_state, bl_return_state, bl_id FROM( " +
                 "SELECT pi_id, pi_code FROM project_info UNION ALL " +
                 "SELECT ti_id, ti_code FROM topic_info UNION ALL " +
                 "SELECT si_id, si_code FROM subject_info) A " +
                 "LEFT JOIN processing_box ON pb_obj_id = A.pi_id " +
                 "LEFT JOIN processing_file_list ON pfl_box_id = pb_id " +
                 "LEFT JOIN (SELECT * FROM (" +
                 "SELECT rowid = ROW_NUMBER() OVER (PARTITION BY bl_file_id ORDER BY bl_date DESC), * FROM borrow_log) A WHERE rowid = 1) bl ON bl.bl_file_id=pb_id " +
                $"WHERE A.pi_id = '{id}' AND pb_id IS NOT NULL " +
                 "GROUP BY A.pi_code, pb_id, pb_gc_id, pb_box_number, bl_borrow_state, bl_return_state, bl_id " +
                 "ORDER BY pb_box_number";
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
                view_Log.Rows[i].Tag = row["bl_file_id"];
                view_Log.Rows[i].Cells["id"].Value = i + 1;
                view_Log.Rows[i].Cells["code"].Value = row["bl_code"];
                view_Log.Rows[i].Cells["code"].Tag = row["bl_id"];
                view_Log.Rows[i].Cells["date"].Value = ToolHelper.GetDateValue(row["bl_date"], "yyyy-MM-dd HH:mm");
                view_Log.Rows[i].Cells["unit"].Value = row["bl_user_unit"];
                view_Log.Rows[i].Cells["user"].Value = row["bl_user"];
                view_Log.Rows[i].Cells["state"].Value = GetReturnState(row["bl_return_state"]);
                view_Log.Rows[i].Cells["bdate"].Value = ToolHelper.GetDateValue(row["bl_real_return_term"], "yyyy-MM-dd HH:mm");
            }
        }

        private void btn_LogQuery_Click(object sender, EventArgs e)
        {
            string searchCode = log_SearchCode.Text.Trim();
            LoadBorrowLog(searchCode);
        }

        private void btn_Export_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter= "表单文件(*.CSV)|*.csv";
            saveFileDialog1.Title = "选择文件导出位置";
            if(saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string filePath = saveFileDialog1.FileName;

                object planType = cbo_PlanTypeList.SelectedValue;
                object orgType = cbo_SourceOrg.SelectedValue;
                string batchName = txt_BatchCode.Text;
                string proCode = txt_ProjectCode.Text;
                string proName = txt_ProjectName.Text;
                string sDate = chk_allDate.Checked ? null : dtp_sDate.Text;
                string eDate = chk_allDate.Checked ? null : dtp_eDate.Text;

                string querySQL = $"SELECT ROW_NUMBER() OVER(ORDER BY pi_orga_id DESC, pi_code) ID, A.* FROM( " +
                    "SELECT * FROM project_info WHERE pi_categor = 2 UNION ALL " +
                    "SELECT * FROM topic_info) A WHERE pi_orga_id IS NOT NULL ";
                if(!string.IsNullOrEmpty(proCode))
                    querySQL += $"AND pi_code LIKE '%{proCode}%' ";
                if(!string.IsNullOrEmpty(proName))
                    querySQL += $"AND pi_name LIKE '%{proName}%' ";
                if("ZX".Equals(planType))//全部重大专项
                    querySQL += $"AND pi_source_id LIKE 'ZX__' ";
                else if(!"all".Equals(planType))//普通计划
                    querySQL += $"AND pi_source_id = '{planType}' ";
                if(!"all".Equals(orgType))
                    querySQL += $"AND pi_orga_id = '{orgType}' ";
                if(!string.IsNullOrEmpty(sDate))
                    querySQL += $"AND pi_start_datetime >= '{sDate}' ";
                if(!string.IsNullOrEmpty(eDate))
                    querySQL += $"AND pi_start_datetime <= '{eDate}' ";

                //关联盒数
                string querySqlString = "SELECT dd_name '来源单位', pi_code '编号', pi_name '名称', pi_unit '承担单位', pi_prouser '负责人', " +
                    $"pi_start_datetime '开始时间', pi_end_datetime '结束时间', pi_funds '经费', pb_gc_id '馆藏号', pb_box_number '盒号', D.fCount '文件数', pi_checker_date '完成时间' FROM({querySQL}) C " +
                     "LEFT JOIN processing_box ON C.pi_id = pb_obj_id " +
                     "LEFT JOIN data_dictionary ON C.pi_orga_id = dd_code " +
                     "LEFT JOIN( SELECT pfl_box_id, COUNT(pfl_id) fCount FROM processing_file_list GROUP BY pfl_box_id) D ON pb_id = D.pfl_box_id " +
                     "ORDER BY dd_sort, pi_code; ";
                DataTable table = SqlHelper.ExecuteQuery(querySqlString);

                string querySql_Subject = $"SELECT B.* FROM ({querySQL}) A LEFT JOIN ( " +
                     "SELECT ti_id, ti_code, ti_name, ti_unit, ti_prouser, ti_start_datetime, ti_end_datetime, ti_funds, ti_obj_id, ti_checker_date, ti_orga_id FROM topic_info UNION ALL " +
                     "SELECT si_id, si_code, si_name, si_unit, si_prouser, si_start_datetime, si_end_datetime, si_funds, si_obj_id, si_checker_date, si_orga_id FROM subject_info) B ON A.pi_id = B.ti_obj_id " +
                     "WHERE B.ti_id IS NOT NULL";
                //关联盒数
                string querySqlString_Subject = "SELECT dd_name, ti_code, ti_name, ti_unit, ti_prouser, " +
                    $"ti_start_datetime, ti_end_datetime, ti_funds, pb_gc_id, pb_box_number, D.fCount, ti_checker_date FROM({querySql_Subject}) C " +
                     "LEFT JOIN processing_box ON C.ti_id = pb_obj_id " +
                     "LEFT JOIN data_dictionary ON C.ti_orga_id = dd_code " +
                     "LEFT JOIN( SELECT pfl_box_id, COUNT(pfl_id) fCount FROM processing_file_list GROUP BY pfl_box_id) D ON pb_id = D.pfl_box_id " +
                     "ORDER BY dd_sort, ti_code; ";
                DataTable table_Subject = SqlHelper.ExecuteQuery(querySqlString_Subject);
                foreach(DataRow dataRow in table_Subject.Rows)
                {
                    table.Rows.Add(dataRow["dd_name"], dataRow["ti_code"], dataRow["ti_name"], dataRow["ti_unit"], dataRow["ti_prouser"], dataRow["ti_start_datetime"],
                        dataRow["ti_end_datetime"], dataRow["ti_funds"], dataRow["pb_gc_id"], dataRow["pb_box_number"], dataRow["fCount"], dataRow["ti_checker_date"]);
                }

                bool result = MicrosoftWordHelper.GetCsvFromDataTable(table, filePath);
                if(result && XtraMessageBox.Show("导出完毕，是否立即打开文件?", "确认提示", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk) == DialogResult.Yes)
                {
                    WinFormOpenHelper.OpenWinForm(0, "open", filePath, null, null, ShowWindowCommands.SW_NORMAL);
                }
            }
        }

        private void view_Log_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex != -1)
            {
                string value = view_Log.Columns[e.ColumnIndex].Name;
                if("code".Equals(value))
                {
                    object fileId = view_Log.Rows[e.RowIndex].Tag;
                    object borrowId = view_Log.Rows[e.RowIndex].Cells["code"].Tag;
                    int type = SqlHelper.ExecuteCountQuery($"select COUNT(pb_id) from processing_box where pb_id='{fileId}'");
                    if(type == 0)
                    {
                        Frm_BorrowEdit borrowEdit = new Frm_BorrowEdit(fileId, borrowId, true);
                        if(borrowEdit.ShowDialog()== DialogResult.OK)
                        {
                            btn_LogQuery_Click(null, null);
                        }
                    }
                    else
                    {
                        Frm_BorrowEditBox borrowEditBox = new Frm_BorrowEditBox(fileId, borrowId, true);
                        if(borrowEditBox.ShowDialog()== DialogResult.OK)
                        {
                            btn_LogQuery_Click(null, null);
                        }
                    }
                }
            }
        }

        private void btn_Refresh_Click(object sender, EventArgs e)
        {
            int rowCount = view2.RowCount;
            for(int i = 0; i < rowCount; i++)
            {
                DataGridViewCell cell = view2.Rows[i].Cells["fbstate"];
                if(cell != null && cell.Tag != null)
                {
                    object bstate = SqlHelper.ExecuteOnlyOneQuery($"SELECT bl_borrow_state FROM borrow_log WHERE bl_id='{cell.Tag}'");
                    cell.Value = GetBorrowState(bstate);
                }
            }
        }

        private void treeList1_EndSorting(object sender, EventArgs e)
        {
            for(int i = 0; i < treeList1.Nodes.Count; i++)
            {
                TreeListNode node = treeList1.Nodes[i];
                node.SetValue(0, i + 1);
            }
            if(treeList1.Nodes.Count > 0)
            {
                treeList1.SetFocusedNode(treeList1.Nodes[0]);
            }
        }
    }
}
