using CefSharp;
using CefSharp.WinForms;
using DevExpress.XtraBars.Navigation;
using DevExpress.XtraSplashScreen;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace 科技计划项目档案数据采集管理系统
{
    public partial class Frm_Statistics : DevExpress.XtraEditors.XtraForm
    {
        public Frm_Statistics()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
            view.ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle()
            {
                Font = new Font("微软雅黑", 12f, FontStyle.Bold),
                Padding = new Padding(0, 3, 0, 3),
                Alignment = DataGridViewContentAlignment.MiddleCenter,
            };
            view.DefaultCellStyle = DataGridViewStyleHelper.GetCellStyle();
            LoadCompanySource();

        }

        /// <summary>
        /// 加载来源单位列表
        /// </summary>
        private void LoadCompanySource()
        {
            string querySql = "SELECT dd_code, dd_name FROM data_dictionary WHERE dd_pId=(SELECT dd_id FROM data_dictionary WHERE dd_code='dic_key_company_source') ORDER BY dd_sort";
            DataTable sTable = SqlHelper.ExecuteQuery(querySql);
            DataRow sRow = sTable.NewRow();
            sRow.SetField("dd_code", "ace_all");
            sRow.SetField("dd_name", "全部来源单位");
            sTable.Rows.InsertAt(sRow, 0);
            ac_LeftMenu.BeginUpdate();
            for(int i = 0; i < sTable.Rows.Count; i++)
            {
                AccordionControlElement element = new AccordionControlElement()
                {
                    Style = ElementStyle.Item,
                    Name = ToolHelper.GetValue(sTable.Rows[i]["dd_code"]),
                    Text = ToolHelper.GetValue(sTable.Rows[i]["dd_name"]),
                };
                element.Click += Item_Click;
                ac_LeftMenu.Elements.Add(element);
            }
            ac_LeftMenu.EndUpdate();

            cbo_SourceList.DataSource = sTable;
            cbo_SourceList.DisplayMember = "dd_name";
            cbo_SourceList.ValueMember = "dd_code";

            querySql = "SELECT dd_code, dd_name FROM data_dictionary WHERE dd_pId=(SELECT dd_id FROM data_dictionary WHERE dd_code='dic_key_plan') ORDER BY dd_sort";
            DataTable pTable = SqlHelper.ExecuteQuery(querySql);
            DataRow pRow = pTable.NewRow();
            pRow.SetField("dd_code", "all_ptype");
            pRow.SetField("dd_name", "全部计划类别");
            pTable.Rows.InsertAt(pRow, 0);
            bc_LeftMenu.BeginUpdate();
            for(int i = 0; i < pTable.Rows.Count; i++)
            {
                DataRow row = pTable.Rows[i];
                AccordionControlElement element = new AccordionControlElement()
                {
                    Name = ToolHelper.GetValue(row["dd_code"]),
                    Text = ToolHelper.GetValue(row["dd_name"]),
                };
                bc_LeftMenu.Elements.Add(element);
                //国家科技重大专项 -- 特殊处理
                if("ZX".Equals(row["dd_code"]))
                {
                    element.Style = ElementStyle.Group;
                    string speQuerySql = "SELECT dd_code, dd_name FROM data_dictionary WHERE dd_pId= " +
                        "(SELECT dd_id FROM data_dictionary WHERE dd_code = 'dic_key_project') " +
                        "ORDER BY dd_sort";
                    DataTable speTable = SqlHelper.ExecuteQuery(speQuerySql);
                    foreach(DataRow dataRow in speTable.Rows)
                    {
                        AccordionControlElement element2 = new AccordionControlElement()
                        {
                            Style = ElementStyle.Item,
                            Name = ToolHelper.GetValue(dataRow["dd_code"]),
                            Text = ToolHelper.GetValue(dataRow["dd_name"]),
                        };
                        element2.Click += Bc_Element_Click;
                        element.Elements.Add(element2);
                    }
                    element.Click += Bc_Element_Click;
                }
                else
                {
                    element.Style = ElementStyle.Item;
                    element.Click += Bc_Element_Click;
                }
            }
            bc_LeftMenu.EndUpdate();

            DataTable _pTable = pTable.Copy();
            string _speQuerySql = "SELECT dd_code, dd_name FROM data_dictionary WHERE dd_pId=(SELECT dd_id FROM data_dictionary WHERE dd_code = 'dic_key_project') ORDER BY dd_sort";
            DataTable _speTable = SqlHelper.ExecuteQuery(_speQuerySql);
            foreach(DataRow dataRow in _speTable.Rows)
                _pTable.ImportRow(dataRow);
            cbo_PlanList.DataSource = _pTable;
            cbo_PlanList.DisplayMember = "dd_name";
            cbo_PlanList.ValueMember = "dd_code";

            querySql = "SELECT dd_id, dd_name FROM data_dictionary WHERE dd_pId= " +
                "(SELECT dd_id FROM data_dictionary WHERE dd_code = 'dic_xzqy_province') " +
                "ORDER BY dd_sort";
            DataTable lTable = SqlHelper.ExecuteQuery(querySql);
            DataRow lRow = lTable.NewRow();
            lRow.SetField("dd_id", "all_ltype");
            lRow.SetField("dd_name", "全部地区");
            lTable.Rows.InsertAt(lRow, 0);
            cc_LeftMenu.BeginUpdate();
            for(int i = 0; i < lTable.Rows.Count; i++)
            {
                AccordionControlElement element = new AccordionControlElement()
                {
                    Style = ElementStyle.Item,
                    Name = ToolHelper.GetValue(lTable.Rows[i]["dd_id"]),
                    Text = ToolHelper.GetValue(lTable.Rows[i]["dd_name"]),
                };
                element.Click += LocalElement_Click;
                cc_LeftMenu.Elements.Add(element);
            }
            cc_LeftMenu.EndUpdate();
        }

        /// <summary>
        /// 按地区 - 点击事件
        /// </summary>
        private void LocalElement_Click(object sender, EventArgs e)
        {
            SplashScreenManager.ShowDefaultWaitForm(this, false, false);
            string name = (sender as AccordionControlElement).Name;
            if("all_ltype".Equals(name))
                name = string.Empty;
            else
                name = $"AND M.dd_id='{name}'";
            object _sourCode = cbo_SourceList.SelectedValue;
            object _planCode = cbo_PlanList.SelectedValue;
            string sourCode = "ace_all".Equals(_sourCode) ? "AND LEN(pi_orga_id)>0" : $"AND pi_orga_id='{_sourCode}'";
            string planCode = "AND LEN(pi_source_id)>0";
            if("ZX".Equals(_planCode))
                planCode = $"AND pi_source_id LIKE '%{_planCode}%'";
            else if(!"all_ptype".Equals(_planCode))
                planCode = $"AND pi_source_id='{_planCode}'";
            LoadDataListByProvince(name, null, null, sourCode, planCode);
            SplashScreenManager.CloseDefaultWaitForm();
        }

        /// <summary>
        /// 按地区加载数据
        /// </summary>
        /// <param name="provinceId">地区条件SQL(AND M.dd_id='')</param>
        /// <param name="minYear">最小年份</param>
        /// <param name="maxYear">最大年份</param>
        /// <param name="sourCode">来源单位条件SQL（AND LEN(pi_source_id)>0）</param>
        /// <param name="planCode">计划类别SQL（LEN(pi_orga_id)>0）</param>
        private void LoadDataListByProvince(string provinceId, object minYear, object maxYear, object sourCode, object planCode)
        {
            string querySQL = "SELECT M.dd_id, M.dd_name, M.pCount, N.bCount, X.fCount, Y.lCount FROM(" +
                "SELECT dd_id, dd_name, COUNT(B.pi_id) pCount, dd_sort FROM data_dictionary LEFT JOIN (SELECT A.* FROM( " +
                "SELECT pi_id, pi_province, pi_source_id, pi_orga_id, pi_worker_id FROM project_info WHERE pi_categor=2 UNION ALL " +
                "SELECT ti_id, ti_province, ti_source_id, ti_orga_id, ti_worker_id FROM topic_info) A " +
               $"LEFT JOIN work_myreg ON wm_obj_id=A.pi_id WHERE (wm_status=3 OR (wm_status IS NULL AND pi_worker_id IS NULL)) {sourCode} {planCode} AND LEN(pi_province)<>0) B ON dd_id=pi_province " +
                "WHERE dd_pId=(SELECT dd_id FROM data_dictionary WHERE dd_code='dic_xzqy_province') GROUP BY dd_id, dd_name, dd_sort) M LEFT JOIN( " +
                "SELECT dd_id, dd_name, COUNT(pb.pb_id) bCount FROM data_dictionary LEFT JOIN (SELECT A.* FROM( " +
                "SELECT pi_id, pi_province, pi_source_id, pi_orga_id, pi_worker_id FROM project_info WHERE pi_categor=2 UNION ALL " +
                "SELECT ti_id, ti_province, ti_source_id, ti_orga_id, ti_worker_id FROM topic_info) A " +
               $"LEFT JOIN work_myreg ON wm_obj_id=A.pi_id WHERE (wm_status=3 OR (wm_status IS NULL AND pi_worker_id IS NULL)) {sourCode} {planCode} AND LEN(pi_province)<>0) B ON dd_id=pi_province " +
                "LEFT JOIN processing_box pb ON B.pi_id = pb.pb_obj_id WHERE dd_pId=(SELECT dd_id FROM data_dictionary WHERE dd_code='dic_xzqy_province') " +
                "GROUP BY dd_id, dd_name ) N ON M.dd_id=N.dd_id LEFT JOIN( " +
                "SELECT dd_id, dd_name, COUNT(pfl.pfl_id) fCount FROM data_dictionary LEFT JOIN (SELECT A.* FROM( " +
                "SELECT pi_id, pi_province, pi_source_id, pi_orga_id, pi_worker_id FROM project_info WHERE pi_categor=2 UNION ALL " +
                "SELECT ti_id, ti_province, ti_source_id, ti_orga_id, ti_worker_id FROM topic_info) A " +
               $"LEFT JOIN work_myreg ON wm_obj_id=A.pi_id WHERE (wm_status=3 OR (wm_status IS NULL AND pi_worker_id IS NULL)) {sourCode} {planCode} AND LEN(pi_province)<>0) B ON dd_id=pi_province " +
                "LEFT JOIN processing_file_list pfl ON B.pi_id = pfl.pfl_obj_id WHERE dd_pId=(SELECT dd_id FROM data_dictionary WHERE dd_code='dic_xzqy_province') " +
                "GROUP BY dd_id, dd_name) X ON N.dd_id=X.dd_id " +
                "LEFT JOIN(SELECT dd_id, dd_name, SUM(lCount) lCount FROM (SELECT dd_id, dd_name, B.pi_id, CASE COUNT(pfo.pfo_id) WHEN 0 THEN 0 ELSE 1 END lCount FROM data_dictionary LEFT JOIN (SELECT A.* FROM( " +
                "SELECT pi_id, pi_province, pi_source_id, pi_orga_id, pi_worker_id FROM project_info WHERE pi_categor=2 UNION ALL " +
                "SELECT ti_id, ti_province, ti_source_id, ti_orga_id, ti_worker_id FROM topic_info) A " +
               $"LEFT JOIN work_myreg ON wm_obj_id=A.pi_id WHERE (wm_status=3 OR (wm_status IS NULL AND pi_worker_id IS NULL)) {sourCode} {planCode} AND LEN(pi_province)<>0) B ON dd_id=pi_province " +
                "LEFT JOIN processing_file_lost pfo ON B.pi_id = pfo.pfo_obj_id WHERE dd_pId=(SELECT dd_id FROM data_dictionary WHERE dd_code='dic_xzqy_province') " +
                "GROUP BY dd_id, dd_name, B.pi_id )A GROUP BY dd_id, dd_name) Y ON X.dd_id=Y.dd_id " +
               $"WHERE M.pCount>0 {provinceId} ORDER BY dd_sort";
            DataTable table = SqlHelper.ExecuteQuery(querySQL);
            DataGridViewStyleHelper.ResetDataGridView(view, true);
            DataTable tableEntity = new DataTable();
            tableEntity.Columns.AddRange(new DataColumn[]
            {
                new DataColumn("地区编号"),
                new DataColumn("地域名称"),
                new DataColumn("项目/课题数", typeof(int)),
                new DataColumn("盒数", typeof(int)),
                new DataColumn("文件数", typeof(int)),
                new DataColumn("必备文件缺失项目数", typeof(int))
            });
            int totalPcount = 0, totalFcount = 0, totalBcount = 0, totalFBcount = 0;
            DataRowCollection rowCollection = table.Rows;
            for(int i = 0; i < rowCollection.Count; i++)
            {
                DataRow row = rowCollection[i];
                int _pcount = ToolHelper.GetIntValue(row["pCount"], 0);
                int _bcount = ToolHelper.GetIntValue(row["bCount"], 0);
                int _fcount = ToolHelper.GetIntValue(row["fCount"], 0);
                int _fbcount = ToolHelper.GetIntValue(row["lCount"], 0);
                tableEntity.Rows.Add(row["dd_id"], row["dd_name"], _pcount, _bcount, _fcount, _fbcount);

                totalPcount += _pcount;
                totalBcount += _bcount;
                totalFcount += _fcount;
                totalFBcount += _fbcount;
            }
            tableEntity.Rows.Add(string.Empty, "合计", totalPcount, totalBcount, totalFcount, totalFBcount);
            view.DataSource = tableEntity;
            view.Columns[0].Visible = false;
            tabPane2.SelectedPageIndex = 0;
        }

        private void Frm_Statistics_Load(object sender, EventArgs e)
        {
            InitialUserList(string.Empty);

            tabPane2.SelectedPageIndex = 0;
            tabPane1.SelectedPage = tabNavigationPage1;
            int _pwidth = tabNavigationPage4.Width;
            panel3.Location = new Point(_pwidth - panel3.Width, 3);
            chart1.Width = chart2.Width = chart3.Width = datachart.Width;
            tabPane3.SelectedPageIndex = 0;
            chart4.Width = chart5.Width = _pwidth - panel3.Width;
            chart4.Left = chart5.Left = 0;
            panel1.Left = (datachart.Width - panel1.Width) / 2;

            object[] orgList = SqlHelper.ExecuteSingleColumnQuery("SELECT belong_unit FROM user_list GROUP BY belong_unit");
            cbo_UnitList.Items.Add("全部所属单位");
            if (orgList.Length > 0)
            {
                cbo_UnitList.Items.AddRange(orgList);
                cbo_UnitList.SelectedIndex = 0;
            }

            object value = SqlHelper.ExecuteOnlyOneQuery("select MIN(pi_worker_date) from project_info");
            DateTime MinDate = ToolHelper.GetDateValue(value);
            dtp_StartDate.MinDate = MinDate;
            dtp_StartDate.Value = MinDate;
            dtp_StartDate.MaxDate = DateTime.Now;

            dtp_EndDate.MinDate = MinDate;
            dtp_EndDate.MaxDate = DateTime.Now;

        }

        private void InitialUserList(string unitName)
        {
            string querySQL = $"SELECT ul_id, real_name FROM user_list ";
            if (!string.IsNullOrEmpty(unitName))
                querySQL += "WHERE belong_unit= '" + unitName + "'";
            DataTable table = SqlHelper.ExecuteQuery(querySQL);
            DataRow row = table.NewRow();
            row[0] = "all"; row[1] = "全部用户";
            table.Rows.InsertAt(row, 0);

            cbo_UserList.DataSource = table;
            cbo_UserList.ValueMember = "ul_id";
            cbo_UserList.DisplayMember = "real_name";
        }

        /// <summary>
        /// 获取指定计划类别下的盒数
        /// </summary>
        /// <param name="planTypeCode">计划类别编号</param>
        private object GetBoxsAmount(object planTypeCode, string unitCode, int type)
        {
            if(type == 0)
            {
                string querySQL = "SELECT COUNT(pb.pb_id) FROM T_Plan p " +
                    "LEFT JOIN (SELECT pi_id, pi_source_id, pi_orga_id FROM project_info WHERE pi_categor= 2 " +
                   $"UNION ALL SELECT ti_id, ti_source_id, ti_orga_id FROM topic_info) A ON A.pi_source_id = p.F_ID {unitCode}" +
                    "LEFT JOIN processing_box pb ON pb.pb_obj_id = A.pi_id " +
                   $"WHERE p.F_ID = '{planTypeCode}' GROUP BY F_ID";
                return SqlHelper.ExecuteCountQuery(querySQL);
            }
            else
            {
                string querySQL = "SELECT COUNT(pb.pb_id) FROM T_SourceOrg AS s " +
                   "LEFT JOIN (SELECT pi_id, pi_source_id, pi_orga_id FROM project_info WHERE pi_categor = 2 " +
                  $"UNION ALL SELECT ti_id, ti_source_id, ti_orga_id FROM topic_info) A ON s.F_ID = A.pi_orga_id {unitCode} " +
                   "LEFT JOIN processing_box pb ON pb.pb_obj_id = A.pi_id " +
                  $"WHERE F_ID = '{planTypeCode}' GROUP BY F_ID";
                return SqlHelper.ExecuteCountQuery(querySQL);
            }
        }

        /// <summary>
        /// 获取指定计划类别下的文件数
        /// </summary>
        /// <param name="planTypeCode">来源单位编号</param>
        /// <param name="unitCode">计划类别编号</param>
        private int GetFilesAmount(object planTypeCode, string unitCode, int type)
        {
            if(type == 0)
            {
                string querySQL = "SELECT COUNT(pfl.pfl_id) FROM T_Plan p " +
                    "LEFT JOIN (SELECT pi_id, pi_source_id, pi_orga_id FROM project_info WHERE pi_categor= 2 " +
                   $"UNION ALL SELECT ti_id, ti_source_id, ti_orga_id FROM topic_info) A ON A.pi_source_id = p.F_ID {unitCode} " +
                    "LEFT JOIN processing_file_list pfl ON pfl.pfl_obj_id = A.pi_id " +
                   $"WHERE p.F_ID = '{planTypeCode}' GROUP BY F_ID";
                return SqlHelper.ExecuteCountQuery(querySQL);
            }
            else
            {
                string querySQL = "SELECT COUNT(pfl.pfl_id) FROM T_SourceOrg AS s " +
                    "LEFT JOIN (SELECT pi_id, pi_source_id, pi_orga_id FROM project_info WHERE pi_categor = 2 " +
                   $"UNION ALL SELECT ti_id, ti_source_id, ti_orga_id FROM topic_info) A ON s.F_ID = A.pi_orga_id {unitCode} " +
                    "LEFT JOIN processing_file_list pfl ON pfl.pfl_obj_id = A.pi_id " +
                   $"WHERE F_ID = '{planTypeCode}' GROUP BY F_ID";
                return SqlHelper.ExecuteCountQuery(querySQL);
            }
        }

        private void chk_AllDate_CheckedChanged(object sender, EventArgs e)
        {
            dtp_StartDate.Enabled = dtp_EndDate.Enabled = !chk_AllDate.Checked;
        }

        private void Btn_StartCount_Click(object sender, EventArgs e)
        {
            SplashScreenManager.ShowDefaultWaitForm(this, false, false);
            tabPane3.SelectedPageIndex = 0;
            object userId = cbo_UserList.SelectedValue;
            bool allUser = "all".Equals(userId);
            //所属单位
            string unitName = cbo_UnitList.SelectedIndex == 0 ? string.Empty : cbo_UnitList.SelectedItem.ToString();
            DateTime startDate = dtp_StartDate.Value;
            DateTime endDate = dtp_EndDate.Value;
            DataTable table = new DataTable();
            bool flag = chk_AllDate.Checked;
            //按时间统计
            if(rdo_sort_date.Checked)
            {
                table.Columns.AddRange(new DataColumn[]
                {
                    new DataColumn("日期"),
                    new DataColumn("项目/课题数", typeof(int)),
                    new DataColumn("课题/子课题数", typeof(int)),
                    new DataColumn("文件数", typeof(int)),
                    new DataColumn("电子文件数", typeof(int)),
                    new DataColumn("盒数", typeof(int)),
                    new DataColumn((rdo_JG.Checked?"被":string.Empty) + "返工数", typeof(int)),
                    new DataColumn("页数", typeof(int)),
                });
                //加工人员工作量统计
                if(rdo_JG.Checked)
                {
                    string userConditon = !allUser ? $" AND pi_worker_id='{userId}'" : string.Empty;
                    string queryCondition = string.Empty;
                    string unitCondition = string.Empty;
                    if(!flag)//全部时间
                    {
                        if(startDate.Date == endDate.Date)
                            queryCondition = $"AND pi_worker_date =  CONVERT(DATE, '{startDate}')";
                        else
                            queryCondition = $"AND pi_worker_date >=  CONVERT(DATE, '{startDate}') AND pi_worker_date <=  CONVERT(DATE, '{endDate}')";
                    }
                    if (!string.IsNullOrEmpty(unitName))
                        unitCondition = "INNER JOIN user_list ON pi_worker_id = ul_id AND belong_unit='" + unitName + "'";
                    else
                        unitCondition = string.Empty;
                    string querySQL = "SELECT pi_worker_date, COUNT(pi_id) FROM(" +
                        "SELECT pi_id, pi_worker_date, pi_worker_id FROM project_info WHERE pi_categor = 2 UNION ALL " +
                       $"SELECT ti_id, ti_worker_date, ti_worker_id FROM topic_info WHERE ti_categor = -3) AS TB1 {unitCondition} " +
                       $"WHERE pi_worker_id IS NOT NULL {queryCondition} {userConditon}" +
                        "GROUP BY pi_worker_date";
                    List<object[]> list = SqlHelper.ExecuteColumnsQuery(querySQL, 2);
                    object userCode = "all".Equals(userId) ? null : userId;
                    for(int i = 0; i < list.Count; i++)
                    {
                        object date = GetDateValue(list[i][0], "yyyy-MM-dd");
                        if(date != null)
                        {
                            object pcount = list[i][1];
                            object tcount = GetTopicAmount(date, userCode, 1);
                            object fcount = GetFileAmount(date, userCode, 1);
                            object efcount = GetEFileAmount(date, userCode, 1);
                            object boxcount = GetBoxAmount(date, userCode, 1);
                            object bcount = GetBackAmount(date, userCode, 1);
                            object pgcount = GetPageAmount(date, userCode, 1);
                            table.Rows.Add(date, pcount, tcount, fcount, efcount, boxcount, bcount, pgcount);
                        }
                    }
                    
                    //单独统计课题/子课题工作量
                    if(!flag)
                    {
                        if(startDate.Date == endDate.Date)
                            queryCondition = $"AND ti_worker_date =  CONVERT(DATE, '{startDate}')";
                        else
                            queryCondition = $"AND ti_worker_date >=  CONVERT(DATE, '{startDate}') AND ti_worker_date <=  CONVERT(DATE, '{endDate}')";
                    }
                    if (!string.IsNullOrEmpty(unitName))
                        unitCondition = "INNER JOIN user_list ON ti_worker_id = ul_id AND belong_unit='" + unitName + "'";
                    else
                        unitCondition = string.Empty;
                    querySQL = "SELECT ti_worker_date, COUNT(ti_id) FROM topic_info " +
                         $"LEFT JOIN project_info ON ti_obj_id = pi_id {unitCondition} " +
                         $"WHERE ti_categor = 3 AND ti_worker_id='{userId}' AND ti_worker_date <> pi_worker_date {queryCondition} " +
                         "GROUP BY ti_worker_date;";
                    List<object[]> list2 = SqlHelper.ExecuteColumnsQuery(querySQL, 2);
                    for(int i = 0; i < list2.Count; i++)
                    {
                        object date = GetDateValue(list2[i][0], "yyyy-MM-dd");
                        object pcount = 0;
                        object tcount = list2[i][1];
                        object fcount = GetFileAmount(date, userId, 1);
                        object efcount = GetEFileAmount(date, userCode, 1);
                        object boxcount = GetBoxAmount(date, userCode, 1);
                        object bcount = GetBackAmount(date, userId, 1);
                        object pgcount = GetPageAmount(date, userId, 1);
                        table.Rows.Add(date, pcount, tcount, fcount, efcount, boxcount, bcount, pgcount);
                    }

                    //单独统计文件加工工作量
                    if(!flag)
                    {
                        if(startDate.Date == endDate.Date)
                            queryCondition = $"AND pfl_worker_date =  CONVERT(DATE, '{startDate}')";
                        else
                            queryCondition = $"AND pfl_worker_date >=  CONVERT(DATE, '{startDate}') AND pfl_worker_date <=  CONVERT(DATE, '{endDate}')";
                    }
                    if (!string.IsNullOrEmpty(unitName))
                        unitCondition = "INNER JOIN user_list ON pfl_worker_id = ul_id AND belong_unit='" + unitName + "'";
                    else
                        unitCondition = string.Empty;
                    if (allUser)
                    {
                        querySQL = $"SELECT pfl_worker_date, COUNT(pfl_id) FROM processing_file_list {unitCondition} " +
                            $"WHERE 1=1 {queryCondition} AND pfl_worker_id NOT IN( " +
                            $"SELECT pi_worker_id FROM project_info WHERE pi_worker_date = pfl_worker_date UNION ALL " +
                            $"SELECT ti_worker_id FROM topic_info WHERE ti_worker_date = pfl_worker_date) " +
                            $"GROUP BY pfl_worker_date; ";
                    }
                    else
                    {
                        querySQL = $"SELECT pfl_worker_date, COUNT(pfl_id) FROM processing_file_list {unitCondition} " +
                               $"WHERE pfl_worker_id='{userId}' {queryCondition} AND pfl_worker_id NOT IN( " +
                               $"SELECT pi_worker_id FROM project_info WHERE pi_worker_id='{userId}' AND pi_worker_date = pfl_worker_date UNION ALL " +
                               $"SELECT ti_worker_id FROM topic_info WHERE ti_worker_id='{userId}' AND ti_worker_date = pfl_worker_date) " +
                                "GROUP BY pfl_worker_date; ";
                    }
                    string fUserConditon = !allUser ? $" " : string.Empty;
                    
                    List<object[]> list3 = SqlHelper.ExecuteColumnsQuery(querySQL, 2);
                    for(int i = 0; i < list3.Count; i++)
                    {
                        object date = GetDateValue(list3[i][0], "yyyy-MM-dd");
                        object pcount = 0;
                        object tcount = 0;
                        object fcount = Convert.ToInt32(list3[i][1]);
                        object efcount = GetEFileAmount(date, userCode, 1);
                        object boxcount = GetBoxAmount(date, userCode, 1);
                        object bcount = GetBackAmount(date, userId, 1);
                        object pgcount = GetFilePageByFid(date, userId);
                        table.Rows.Add(date, pcount, tcount, fcount, efcount, boxcount, bcount, pgcount);
                    }
                    countView.DataSource = DistinctSomeColumn(table, "日期");
                }
                //质检人员工作量统计
                else
                {
                    string userConditon = !allUser ? $"AND pi_checker_id='{userId}'" : string.Empty;
                    string queryCondition = string.Empty;
                    string unitCondition = string.Empty;
                    if (!flag)//全部时间
                    {
                        if(startDate.Date == endDate.Date)
                            queryCondition = $"AND pi_checker_date =  CONVERT(DATE, '{startDate}')";
                        else
                            queryCondition = $"AND pi_checker_date >=  CONVERT(DATE, '{startDate}') AND pi_checker_date <=  CONVERT(DATE, '{endDate}')";
                    }
                    if (!string.IsNullOrEmpty(unitName))
                        unitCondition = "INNER JOIN user_list ON pi_checker_id = ul_id AND belong_unit='" + unitName + "'";
                    else
                        unitCondition = string.Empty;
                    string querySQL = "SELECT pi_checker_date, COUNT(pi_id) FROM(" +
                         "SELECT pi_id, pi_checker_id, pi_checker_date FROM project_info WHERE pi_categor = 2 UNION ALL " +
                        $"SELECT ti_id, ti_checker_id, ti_checker_date FROM topic_info WHERE ti_categor = -3) AS TB1 {unitCondition} " +
                        $"WHERE pi_checker_id IS NOT NULL {queryCondition} {userConditon} " +
                        $"GROUP BY pi_checker_date";
                    List<object[]> list = SqlHelper.ExecuteColumnsQuery(querySQL, 2);
                    object userCode = "all".Equals(userId) ? null : userId;
                    for(int i = 0; i < list.Count; i++)
                    {
                        object date = GetDateValue(list[i][0], "yyyy-MM-dd");
                        object pcount = list[i][1];
                        object tcount = GetTopicAmount(date, userCode, 2);
                        object fcount = GetFileAmount(date, userCode, 2);
                        object efcount = GetEFileAmount(date, userCode, 2);
                        object boxcount = GetBoxAmount(date, userCode, 2);
                        object bcount = GetBackAmount(date, userCode, 2);
                        object pgcount = GetPageAmount(date, userCode, 2);
                        table.Rows.Add(date, pcount, tcount, fcount, efcount, boxcount, bcount, pgcount);
                    }

                    countView.DataSource = DistinctSomeColumn(table, "日期"); ;
                }
            }
            //按人员统计
            else
            {
                table.Columns.AddRange(new DataColumn[]
                {
                    new DataColumn("姓名"),
                    new DataColumn("项目/课题数", typeof(int)),
                    new DataColumn("课题/子课题数", typeof(int)),
                    new DataColumn("文件数", typeof(int)),
                    new DataColumn("电子文件数", typeof(int)),
                    new DataColumn("盒数", typeof(int)),
                    new DataColumn((rdo_JG.Checked?"被":string.Empty) + "返工数", typeof(int)),
                    new DataColumn("页数", typeof(int)),
                });
                //加工人员工作量统计
                if(rdo_JG.Checked)
                {
                    string userConditon = !allUser ? $" AND pi_worker_id='{userId}'" : string.Empty;
                    string dateCondition = string.Empty;
                    string unitCondition = string.Empty;
                    string _startDate = dtp_StartDate.Value.ToString("yyyy-MM-dd");
                    string _endDate = dtp_EndDate.Value.ToString("yyyy-MM-dd");
                    if(!chk_AllDate.Checked)//全部时间
                    {
                        if(startDate.Equals(endDate))
                            dateCondition = $"AND pi_worker_date = '{_startDate}'";
                        else
                            dateCondition = $"AND pi_worker_date >= '{_startDate}' AND pi_worker_date <= '{_endDate}'";
                    }
                    if (!string.IsNullOrEmpty(unitName))
                        unitCondition = "INNER JOIN user_list ON pi_worker_id = ul_id AND belong_unit='" + unitName + "'";
                    else
                        unitCondition = string.Empty;
                    string querySQL = "SELECT pi_worker_id, COUNT(pi_id) FROM(" +
                        "SELECT pi_id, pi_worker_date, pi_worker_id FROM project_info WHERE pi_categor = 2 UNION ALL " +
                       $"SELECT ti_id, ti_worker_date, ti_worker_id FROM topic_info WHERE ti_categor = -3) AS TB1 {unitCondition} " +
                       $"WHERE pi_worker_id IS NOT NULL {dateCondition} {userConditon} " +
                        "GROUP BY pi_worker_id";
                    List<object[]> list = SqlHelper.ExecuteColumnsQuery(querySQL, 2);
                    object userCode = "all".Equals(userId) ? null : userId;
                    for(int i = 0; i < list.Count; i++)
                    {
                        object user = UserHelper.GetUserNameById(list[i][0]);
                        object pcount = list[i][1];
                        object tcount = GetTopicAmount(null, list[i][0], 3);
                        object fcount = GetFileAmount(null, list[i][0], 3);
                        object efcount = GetEFileAmount(null, list[i][0], 3);
                        object boxcount = GetBoxAmount(null, list[i][0], 3);
                        object bcount = GetBackAmount(null, list[i][0], 3);
                        object pgcount = GetPageAmount(null, list[i][0], 3);
                        table.Rows.Add(user, pcount, tcount, fcount, efcount, boxcount, bcount, pgcount);
                    }
                    countView.DataSource = DistinctSomeColumn(table, "姓名"); ;
                }
                //质检人员工作量统计
                else
                {
                    string userConditon = !allUser ? $"AND pi_checker_id='{userId}'" : string.Empty;
                    string dateCondition = string.Empty;
                    string unitCondition = string.Empty;
                    string _startDate = dtp_StartDate.Value.ToString("yyyy-MM-dd");
                    string _endDate = dtp_EndDate.Value.ToString("yyyy-MM-dd");
                    if(!chk_AllDate.Checked)//全部时间
                    {
                        if(startDate.Equals(endDate))
                            dateCondition = $"AND pi_checker_date = '{_startDate}'";
                        else
                            dateCondition = $"AND pi_checker_date >= '{_startDate}' AND pi_checker_date <= '{_endDate}'";
                    }
                    if (!string.IsNullOrEmpty(unitName))
                        unitCondition = "INNER JOIN user_list ON pi_checker_id = ul_id AND belong_unit='" + unitName + "'";
                    else
                        unitCondition = string.Empty;
                    string querySQL = "SELECT pi_checker_id, COUNT(pi_id) FROM(" +
                         "SELECT pi_id, pi_checker_id, pi_checker_date FROM project_info WHERE pi_categor = 2 UNION ALL " +
                        $"SELECT ti_id, ti_checker_id, ti_checker_date FROM topic_info WHERE ti_categor = -3) AS TB1 {unitCondition} " +
                        $"WHERE pi_checker_id IS NOT NULL {dateCondition} {userConditon} " +
                         "GROUP BY pi_checker_id";
                    List<object[]> list = SqlHelper.ExecuteColumnsQuery(querySQL, 2);
                    for(int i = 0; i < list.Count; i++)
                    {
                        object user = UserHelper.GetUserNameById(list[i][0]);
                        object pcount = list[i][1];
                        object tcount = GetTopicAmount(null, list[i][0], 4);
                        object fcount = GetFileAmount(null, list[i][0], 4);
                        object efcount = GetEFileAmount(null, list[i][0], 4);
                        object boxcount = GetBoxAmount(null, list[i][0], 4);
                        object bcount = GetBackAmount(null, list[i][0], 4);
                        object pgcount = GetPageAmount(null, list[i][0], 4);
                        table.Rows.Add(user, pcount, tcount, fcount, efcount, boxcount, bcount, pgcount);
                    }

                    countView.DataSource = DistinctSomeColumn(table, "姓名"); ;
                }
            }
            SplashScreenManager.CloseDefaultWaitForm();
        }

        /// <summary>
        /// 获取指定用户在指定日期【加工/质检】的盒数
        /// </summary>
        private object GetBoxAmount(object date, object userId, int type)
        {
            if(type == 1 || type == 2)
            {
                string userCondition = userId == null ? string.Empty : $"AND pb_create_id='{userId}'";
                return SqlHelper.ExecuteCountQuery($"SELECT COUNT(pb_id) FROM processing_box WHERE pb_create_type={type} AND pb_create_date='{date}' {userCondition} ");
            }
            else
            {
                string dateCondition = string.Empty;
                string startDate = dtp_StartDate.Value.ToString("yyyy-MM-dd");
                string endDate = dtp_EndDate.Value.ToString("yyyy-MM-dd");
                int key = type == 3 ? 1 : 2;
                if(!chk_AllDate.Checked)//全部时间
                {
                    if(startDate.Equals(endDate))
                        dateCondition = $"AND pb_create_date = '{startDate}'";
                    else
                        dateCondition = $"AND pb_create_date >= '{startDate}' AND pb_create_date <= '{endDate}'";
                }
                if(type == 3 || type == 4)
                {
                    return SqlHelper.ExecuteCountQuery($"SELECT COUNT(pb_id) FROM processing_box WHERE pb_create_type={key} AND pb_create_id='{userId}' {dateCondition} ");
                }
            }
            return 0;
        }

        /// <summary>
        /// 获取挂接的电子文件数
        /// </summary>
        private int GetEFileAmount(object date, object userId, int type)
        {
            if(type == 1 || type == 2)
            {
                string key = type == 1 ? "worker" : "checker";
                string userCondition = userId == null ? string.Empty : $"AND pfl_{ key}_id='{userId}'";
                object[] linkArray = SqlHelper.ExecuteSingleColumnQuery($"SELECT pfl_link FROM processing_file_list WHERE LEN(TRY_CAST(pfl_link AS VARCHAR))>0 AND pfl_{key}_date='{date}' {userCondition} ");
                int count = 0;
                count = linkArray.Sum(x => x.ToString().Count(y => y == '；') + 1);
                //for(int i = 0; i < linkArray.Length; i++)
                //{
                //    string value = ToolHelper.GetValue(linkArray[i]);
                //    count += value.Split('；').Length;
                //}
                return count;
            }
            else
            {
                string dateCondition = string.Empty;
                string startDate = dtp_StartDate.Value.ToString("yyyy-MM-dd");
                string endDate = dtp_EndDate.Value.ToString("yyyy-MM-dd");
                string key = type == 3 ? "worker" : "checker";
                if(!chk_AllDate.Checked)//全部时间
                {
                    if(startDate.Equals(endDate))
                        dateCondition = $"AND pfl_{key}_date = '{startDate}'";
                    else
                        dateCondition = $"AND pfl_{key}_date >= '{startDate}' AND pfl_{key}_date <= '{endDate}'";
                }
                if(type == 3 || type == 4)
                {
                    object[] linkArray = SqlHelper.ExecuteSingleColumnQuery($"SELECT pfl_link FROM processing_file_list WHERE LEN(TRY_CAST(pfl_link AS VARCHAR))>0 AND pfl_{key}_id='{userId}' {dateCondition} ");
                    int count = 0;
                    count = linkArray.Sum(x => x.ToString().Count(y => y == '；') + 1);
                    //for(int i = 0; i < linkArray.Length; i++)
                    //{
                    //    string value = ToolHelper.GetValue(linkArray[i]);
                    //    count += value.Split('；').Length;
                    //}
                    return count;
                }
            }
            return 0;
        }

        /// <summary>
        /// 按照fieldName从sourceTable中选择出不重复的行，   
        /// 并且返回sourceTable中所有的列。   
        /// </summary>
        /// <param name="sourceTable">数据源</param>
        /// <param name="fieldName">查重字段</param>
        public static DataTable DistinctSomeColumn(DataTable sourceTable, string fieldName)
        {
            if(sourceTable.Rows.Count == 0 || fieldName == null || fieldName.Length == 0) return sourceTable;
            DataTable dataTable = sourceTable.AsEnumerable().Distinct(new ColumnEquals(new string[] { fieldName })).CopyToDataTable();
            DataView dataView = dataTable.AsDataView();
            dataView.Sort = $"{fieldName} ASC";
            return dataView.ToTable();
        }

        public class ColumnEquals : IEqualityComparer<DataRow>
        {
            public ColumnEquals(string[] sArr)
            {
                _sArr = sArr;
            }

            private string[] _sArr;
            public bool Equals(DataRow x, DataRow y)
            {
                return !_sArr.Any(p => !x[p].Equals(y[p]));
            }

            public int GetHashCode(DataRow obj)
            {
                return obj.ToString().GetHashCode();
            }
        }

        private int GetFilePageByFid(object date, object userId)
        {
            string querySQL = $"SELECT SUM(pfl_pages) FROM processing_file_list WHERE pfl_worker_id = '{userId}' AND pfl_worker_date='{date}'";
            return SqlHelper.ExecuteCountQuery(querySQL);
        }

        /// <summary>
        /// 获取指定日期下指定用户的总加工页数
        /// </summary>
        private int GetPageAmount(object date, object userId, int type)
        {
            if(type == 1 || type == 2)
            {
                string key = type == 1 ? "worker" : "checker";
                string userCondition = userId == null ? string.Empty : $"AND pfl_{ key}_id='{userId}'";
                string querySQL = $"SELECT SUM(pfl_pages) FROM processing_file_list WHERE pfl_{key}_date='{date}' {userCondition} ";
                return SqlHelper.ExecuteCountQuery(querySQL);
            }
            else
            {
                string dateCondition = string.Empty;
                string startDate = dtp_StartDate.Value.ToString("yyyy-MM-dd");
                string endDate = dtp_EndDate.Value.ToString("yyyy-MM-dd");
                string key = type == 3 ? "worker" : "checker";
                if(!chk_AllDate.Checked)//全部时间
                {
                    if(startDate.Equals(endDate))
                        dateCondition = $"AND pfl_{key}_date = '{startDate}'";
                    else
                        dateCondition = $"AND pfl_{key}_date >= '{startDate}' AND pfl_{key}_date <= '{endDate}'";
                }
                if(type == 3 || type == 4)
                {
                    // pfl_{key}_date='{date}'
                    string querySQL = $"SELECT SUM(pfl_pages) FROM processing_file_list WHERE  pfl_{ key}_id='{userId}' {dateCondition} ";
                    return SqlHelper.ExecuteCountQuery(querySQL);
                }
            }
            return 0;
        }

        /// <summary>
        /// 获取指定日期下指定用户的返工数
        /// </summary>
        private int GetBackAmount(object date, object userId, int type)
        {
            if(type == 1)
            {
                string userCondition = userId == null ? string.Empty : $"AND wm_user='{userId}'";
                string querySQL = $"SELECT SUM(wm_ticker) FROM work_myreg WHERE TRY_CAST(wm_accepter_date AS DATE)='{date}' {userCondition} ";
                return SqlHelper.ExecuteCountQuery(querySQL);
            }
            else if(type == 2)
            {
                string userCondition = userId == null ? string.Empty : $"AND rl_user_id='{userId}'";
                string querySQL = $"SELECT COUNT(rl_id) FROM remake_log WHERE rl_date='{date}' {userCondition} ";
                return SqlHelper.ExecuteCountQuery(querySQL);
            }
            else
            {
                string dateCondition = string.Empty;
                string startDate = dtp_StartDate.Value.ToString("yyyy-MM-dd");
                string endDate = dtp_EndDate.Value.ToString("yyyy-MM-dd");
                if(type == 3)
                {
                    if(!chk_AllDate.Checked)
                    {
                        if(startDate.Equals(endDate))
                            dateCondition = $"AND TRY_CAST(wm_accepter_date AS DATE) = '{startDate}'";
                        else
                            dateCondition = $"AND TRY_CAST(wm_accepter_date AS DATE) >= '{startDate}' AND TRY_CAST(wm_accepter_date AS DATE) <= '{endDate}'";
                    }
                    //CONVERT(DATE, wm_accepter_date)='{date}'
                    string querySQL = $"SELECT SUM(wm_ticker) FROM work_myreg WHERE wm_user='{userId}' {dateCondition} ";
                    return SqlHelper.ExecuteCountQuery(querySQL);
                }
                else if(type == 4)
                {
                    if(!chk_AllDate.Checked)
                    {
                        if(startDate.Equals(endDate))
                            dateCondition = $"AND TRY_CAST(rl_date AS DATE) = '{startDate}'";
                        else
                            dateCondition = $"AND TRY_CAST(rl_date AS DATE) >= '{startDate}' AND TRY_CAST(rl_date AS DATE) <= '{endDate}'";
                    }
                    // rl_date='{date}'
                    string querySQL = $"SELECT COUNT(rl_id) FROM remake_log WHERE rl_user_id='{userId}' {dateCondition}";
                    return SqlHelper.ExecuteCountQuery(querySQL);
                }
            }
            return 0;
        }

        /// <summary>
        /// 获取指定日期下指定用户加工的文件数
        /// </summary>
        private int GetFileAmount(object date, object userId, int type)
        {
            if(type == 1 || type == 2)
            {
                string key = type == 1 ? "worker" : "checker";
                string userCondition = userId == null ? string.Empty : $"AND pfl_{ key}_id='{userId}'";
                return SqlHelper.ExecuteCountQuery($"SELECT COUNT(pfl_id) FROM processing_file_list WHERE pfl_{key}_date='{date}' {userCondition} ");
            }
            else
            {
                string dateCondition = string.Empty;
                string startDate = dtp_StartDate.Value.ToString("yyyy-MM-dd");
                string endDate = dtp_EndDate.Value.ToString("yyyy-MM-dd");
                string key = type == 3 ? "worker" : "checker";
                if(!chk_AllDate.Checked)//全部时间
                {
                    if(startDate.Equals(endDate))
                        dateCondition = $"AND pfl_{key}_date = '{startDate}'";
                    else
                        dateCondition = $"AND pfl_{key}_date >= '{startDate}' AND pfl_{key}_date <= '{endDate}'";
                }
                if(type == 3 || type == 4)
                {
                    return SqlHelper.ExecuteCountQuery($"SELECT COUNT(pfl_id) FROM processing_file_list WHERE pfl_{key}_id='{userId}' {dateCondition} ");
                }
            }
            return 0;
        }

        /// <summary>
        /// 获取指定日期下指定用户加工的课题/子课题数
        /// </summary>
        private int GetTopicAmount(object date, object userId, int type)
        {
            bool flag = userId == null;
            if(type == 1)
            {
                string userConditon = !flag ? $"AND ti_worker_id='{userId}'" : string.Empty;
                string querySQL = "SELECT COUNT(ti_id) FROM(" +
                     "SELECT ti_id, ti_worker_date, ti_worker_id FROM topic_info WHERE ti_categor = 3 UNION ALL " +
                     "SELECT si_id, si_worker_date, si_worker_id FROM subject_info) " +
                    $"AS TB1 WHERE ti_worker_date = '{date}' {userConditon} ";
                return SqlHelper.ExecuteCountQuery(querySQL);
            }
            else if(type == 2)
            {
                string userConditon = !flag ? $"AND ti_checker_id='{userId}'" : string.Empty;
                string querySQL = "SELECT COUNT(ti_id) FROM(" +
                     "SELECT ti_id, ti_checker_date, ti_checker_id FROM topic_info WHERE ti_categor = 3 UNION ALL " +
                     "SELECT si_id, si_checker_date, si_checker_id FROM subject_info) " +
                    $"AS TB1 WHERE ti_checker_date = '{date}' {userConditon} ";
                return SqlHelper.ExecuteCountQuery(querySQL);
            }
            else
            {
                string startDate = dtp_StartDate.Value.ToString("yyyy-MM-dd");
                string endDate = dtp_EndDate.Value.ToString("yyyy-MM-dd");
                string dateCondition = string.Empty;
                if(type == 3)
                {
                    if(!chk_AllDate.Checked)//全部时间
                    {
                        if(startDate.Equals(endDate))
                            dateCondition = $"AND ti_worker_date = '{startDate}'";
                        else
                            dateCondition = $"AND ti_worker_date >= '{startDate}' AND ti_worker_date <= '{endDate}'";
                    }
                    string querySQL = "SELECT COUNT(ti_id) FROM(" +
                         "SELECT ti_id, ti_worker_date, ti_worker_id FROM topic_info WHERE ti_categor = 3 UNION ALL " +
                         "SELECT si_id, si_worker_date, si_worker_id FROM subject_info) " +
                        $"AS TB1 WHERE 1=1 AND ti_worker_id='{userId}' {dateCondition}";
                    return SqlHelper.ExecuteCountQuery(querySQL);
                }
                else if(type == 4)
                {
                    if(!chk_AllDate.Checked)//全部时间
                    {
                        if(startDate.Equals(endDate))
                            dateCondition = $"AND ti_checker_date = '{startDate}'";
                        else
                            dateCondition = $"AND ti_checker_date >= '{startDate}' AND ti_checker_date <= '{endDate}'";
                    }
                    string querySQL = "SELECT COUNT(ti_id) FROM(" +
                         "SELECT ti_id, ti_checker_date, ti_checker_id FROM topic_info WHERE ti_categor = 3 UNION ALL " +
                         "SELECT si_id, si_checker_date, si_checker_id FROM subject_info) " +
                        $"AS TB1 WHERE 1=1 AND ti_checker_id='{userId}' {dateCondition}";
                    return SqlHelper.ExecuteCountQuery(querySQL);
                }
            }
            return -1;
        }

        private object GetDateValue(object value, string format)
        {
            if(value == null)
                return null;
            else
            {
                if(DateTime.TryParse(value.ToString(), out DateTime result))
                    return result.ToString(format);
                else
                    return null;
            }
        }

        private void Btn_Exprot_Click(object sender, EventArgs e)
        {
            if(countView.RowCount == 0)
                return;
            saveFileDialog.Filter = "Execl 表单 (*.xls)|*.xls";
            saveFileDialog.Title = "选择导出文件夹...";
            if(saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                Stream myStream = saveFileDialog.OpenFile();
                StreamWriter sw = new StreamWriter(myStream, System.Text.Encoding.GetEncoding(-0));

                string str = "";
                try
                {
                    for(int i = 0; i < countView.ColumnCount; i++)
                    {
                        if(i > 0)
                        {
                            str += "\t";
                        }
                        str += countView.Columns[i].HeaderText;
                    }
                    sw.WriteLine(str);
                    for(int j = 0; j < countView.Rows.Count; j++)
                    {
                        string tempStr = "";
                        for(int k = 0; k < countView.Columns.Count; k++)
                        {
                            if(k > 0)
                            {
                                tempStr += "\t";
                            }
                            tempStr += ToolHelper.GetValue(countView.Rows[j].Cells[k].Value);
                        }
                        sw.WriteLine(tempStr);
                    }
                    sw.Close();
                    myStream.Close();
                    DialogResult dialogResult = MessageBox.Show("导出完毕, 是否打开文件所在地址？", "温馨提示", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk);
                    if(dialogResult == DialogResult.Yes)
                    {
                        string filePath = saveFileDialog.FileName;
                        WinFormOpenHelper.OpenWinForm(0, "open", null, null, Path.GetDirectoryName(filePath), ShowWindowCommands.SW_NORMAL);
                    }
                }

                //catch(Exception ex)
                //{
                //    MessageBox.Show(ex.Message);
                //}
                finally
                {
                    sw.Close();
                    myStream.Close();
                }
            }
        }

        /// <summary>
        /// /来源单位点击事件
        /// </summary>
        private void Item_Click(object sender, EventArgs e)
        {
            SplashScreenManager.ShowDefaultWaitForm(this, false, false);
            string value = (sender as AccordionControlElement).Name;
            if("ace_all".Equals(value))
                value = string.Empty;
            else
                value = $"AND A.pi_orga_id = '{value}'";

            SetTableByOrga(value, null, null);
            SplashScreenManager.CloseDefaultWaitForm();
        }

        /// <summary>
        /// 加载计划类别表
        /// </summary>
        /// <param name="value">来源单位编码</param>
        /// <param name="minYear">最小年份</param>
        /// <param name="maxYear">最大年份</param>
        private void SetTableByOrga(string value, string minYear, string maxYear)
        {
            DataGridViewStyleHelper.ResetDataGridView(view, true);
            string minYearCondition = string.Empty;
            string maxYearCondition = string.Empty;
            if(!string.IsNullOrEmpty(minYear))
                minYearCondition = $"AND ISNULL(pi_year, TRY_CAST(SUBSTRING(pi_start_datetime, 1, 4) AS INT)) >= {minYear}";
            if(!string.IsNullOrEmpty(maxYear))
                maxYearCondition = $"AND ISNULL(pi_year, TRY_CAST(SUBSTRING(pi_start_datetime, 1, 4) AS INT)) <= {maxYear}";
            DataTable tableEntity = new DataTable();
            tableEntity.Columns.AddRange(new DataColumn[]
            {
                new DataColumn("F_ID"),
                new DataColumn("F_Title"),
                new DataColumn("pCount", typeof(int)),
                new DataColumn("bCount", typeof(int)),
                new DataColumn("fCount", typeof(int)),
                new DataColumn("fbCount", typeof(int))
            });
            tableEntity.PrimaryKey = new DataColumn[] { tableEntity.Columns[0] };
            view.Columns.AddRange(new DataGridViewColumn[]
            {
                new DataGridViewTextBoxColumn() { DataPropertyName = "F_ID" },
                new DataGridViewTextBoxColumn() { DataPropertyName = "F_Title", HeaderText = "计划类别名称" },
                new DataGridViewTextBoxColumn() { DataPropertyName = "pCount", HeaderText = "项目/课题数" },
                new DataGridViewTextBoxColumn() { DataPropertyName = "bCount", HeaderText = "盒数" },
                new DataGridViewTextBoxColumn() { DataPropertyName = "fCount", HeaderText = "文件数" },
                new DataGridViewTextBoxColumn() { DataPropertyName = "fbCount", HeaderText = "缺失必备文件项目数" },
            });

            //项目|课题数
            string querySql_pCount = "SELECT A.F_ID, A.F_Title, pCount+tCount+sCount pCount FROM ( " +
               "    SELECT p.F_Title, p.F_ID, COUNT(DISTINCT(A.pi_id)) AS pCount FROM T_Plan AS p " +
               "    LEFT JOIN ( " +
               "        SELECT pi_id, pi_source_id, pi_orga_id, pi_year, pi_start_datetime, pi_worker_id FROM project_info WHERE pi_categor=2 UNION ALL " +
               "        SELECT ti_id, ti_source_id, ti_orga_id, ti_year, ti_start_datetime, ti_worker_id FROM topic_info WHERE ti_categor=-3 " +
               "    ) A ON A.pi_source_id = p.F_ID " +
               "    LEFT JOIN work_myreg wm ON wm.wm_obj_id = A.pi_id " +
              $"    WHERE (wm.wm_status=3 OR (wm.wm_status IS NULL AND A.pi_worker_id IS NULL)) AND LEN(A.pi_orga_id) > 0 {value} {minYearCondition} {maxYearCondition} " +
               "    GROUP BY p.F_Title, p.F_ID " +
               " ) AS A  " +
               " LEFT JOIN " +
               " ( " +
               "    SELECT p.F_ID, COUNT(DISTINCT(B.ti_id)) AS tCount FROM T_Plan AS p " +
               "    LEFT JOIN ( " +
               "        SELECT pi_id, pi_source_id, pi_orga_id, pi_year, pi_start_datetime, pi_worker_id FROM project_info WHERE pi_categor=2 UNION ALL " +
               "        SELECT ti_id, ti_source_id, ti_orga_id, ti_year, ti_start_datetime, ti_worker_id FROM topic_info WHERE ti_categor=-3 " +
               "    ) A ON A.pi_source_id = p.F_ID " +
               "    LEFT JOIN work_myreg wm ON wm.wm_obj_id = A.pi_id " +
               "    LEFT JOIN ( " +
               "        SELECT ti_id, ti_obj_id FROM topic_info WHERE ti_categor=3 UNION ALL " +
               "        SELECT si_id, si_obj_id FROM subject_info " +
               "    ) B ON A.pi_id = B.ti_obj_id " +
              $"    WHERE (wm.wm_status=3 OR (wm.wm_status IS NULL AND A.pi_worker_id IS NULL)) AND LEN(A.pi_orga_id) > 0 {value} {minYearCondition} {maxYearCondition} " +
               "    GROUP BY p.F_ID " +
               ") AS B ON A.F_ID = B.F_ID " +
               "LEFT JOIN " +
               "( " +
               "    SELECT p.F_ID, COUNT(DISTINCT(C.si_id)) AS sCount FROM T_Plan AS p  " +
               "    LEFT JOIN ( " +
               "        SELECT pi_id, pi_source_id, pi_orga_id, pi_year, pi_start_datetime, pi_worker_id FROM project_info WHERE pi_categor=2 UNION ALL  " +
               "        SELECT ti_id, ti_source_id, ti_orga_id, ti_year, ti_start_datetime, ti_worker_id FROM topic_info WHERE ti_categor=-3 " +
               "    ) A ON A.pi_source_id = p.F_ID " +
               "    LEFT JOIN work_myreg wm ON wm.wm_obj_id = A.pi_id " +
               "    LEFT JOIN ( " +
               "        SELECT ti_id, ti_obj_id FROM topic_info WHERE ti_categor=3 UNION ALL " +
               "        SELECT si_id, si_obj_id FROM subject_info " +
               "    ) B ON A.pi_id = B.ti_obj_id " +
               "    LEFT JOIN subject_info C ON B.ti_id = C.si_obj_id " +
              $"    WHERE (wm.wm_status=3 OR (wm.wm_status IS NULL AND A.pi_worker_id IS NULL)) AND LEN(A.pi_orga_id) > 0 {value} {minYearCondition} {maxYearCondition} " +
               "    GROUP BY p.F_ID " +
               ") AS C ON A.F_ID = C.F_ID ORDER BY A.F_ID";
            DataTable pCountTable = SqlHelper.ExecuteQuery(querySql_pCount);
            foreach(DataRow row in pCountTable.Rows)
            {
                tableEntity.Rows.Add(row["F_ID"], row["F_Title"], row["pCount"]);
            }

            //盒数
            string querySql_bCount = "SELECT A.F_ID, A.F_Title, pCount+tCount+sCount bCount FROM ( " +
               "    SELECT p.F_Title, p.F_ID, COUNT(pb.pb_id) AS pCount FROM T_Plan AS p " +
               "    LEFT JOIN ( " +
               "        SELECT pi_id, pi_source_id, pi_orga_id, pi_year, pi_start_datetime, pi_worker_id FROM project_info WHERE pi_categor=2 UNION ALL " +
               "        SELECT ti_id, ti_source_id, ti_orga_id, ti_year, ti_start_datetime, ti_worker_id FROM topic_info WHERE ti_categor=-3 " +
               "    ) A ON A.pi_source_id = p.F_ID " +
               "    LEFT JOIN work_myreg wm ON wm.wm_obj_id=A.pi_id " +
               "    LEFT JOIN processing_box pb ON pb.pb_obj_id=A.pi_id " +
              $"    WHERE (wm.wm_status=3 OR (wm.wm_status IS NULL AND A.pi_worker_id IS NULL)) AND LEN(A.pi_orga_id) > 0 {value} {minYearCondition} {maxYearCondition}" +
               "    GROUP BY p.F_Title, p.F_ID " +
               " ) AS A  " +
               " LEFT JOIN " +
               " ( " +
               "    SELECT p.F_ID, COUNT(pb.pb_id) AS tCount FROM T_Plan AS p " +
               "    LEFT JOIN ( " +
               "        SELECT pi_id, pi_source_id, pi_orga_id, pi_year, pi_start_datetime, pi_worker_id FROM project_info WHERE pi_categor=2 UNION ALL " +
               "        SELECT ti_id, ti_source_id, ti_orga_id, ti_year, ti_start_datetime, ti_worker_id FROM topic_info WHERE ti_categor=-3 " +
               "    ) A ON A.pi_source_id = p.F_ID " +
               "    LEFT JOIN work_myreg wm ON wm.wm_obj_id = A.pi_id " +
               "    LEFT JOIN ( " +
               "        SELECT ti_id, ti_obj_id FROM topic_info WHERE ti_categor=3 UNION ALL " +
               "        SELECT si_id, si_obj_id FROM subject_info " +
               "    ) B ON A.pi_id = B.ti_obj_id " +
               "    LEFT JOIN processing_box pb ON pb.pb_obj_id=B.ti_id " +
              $"    WHERE (wm.wm_status=3 OR (wm.wm_status IS NULL AND A.pi_worker_id IS NULL)) AND LEN(A.pi_orga_id) > 0 {value} {minYearCondition} {maxYearCondition}" +
               "    GROUP BY p.F_ID " +
               ") AS B ON A.F_ID = B.F_ID " +
               "LEFT JOIN " +
               "( " +
               "    SELECT p.F_ID, COUNT(pb.pb_id) AS sCount FROM T_Plan AS p  " +
               "    LEFT JOIN ( " +
               "        SELECT pi_id, pi_source_id, pi_orga_id, pi_year, pi_start_datetime, pi_worker_id FROM project_info WHERE pi_categor=2 UNION ALL  " +
               "        SELECT ti_id, ti_source_id, ti_orga_id, ti_year, ti_start_datetime, ti_worker_id FROM topic_info WHERE ti_categor=-3 " +
               "    ) A ON A.pi_source_id = p.F_ID " +
               "    LEFT JOIN work_myreg wm ON wm.wm_obj_id = A.pi_id " +
               "    LEFT JOIN ( " +
               "        SELECT ti_id, ti_obj_id FROM topic_info WHERE ti_categor=3 UNION ALL " +
               "        SELECT si_id, si_obj_id FROM subject_info " +
               "    ) B ON A.pi_id = B.ti_obj_id " +
               "    LEFT JOIN subject_info C ON B.ti_id = C.si_obj_id " +
               "    LEFT JOIN processing_box pb ON pb.pb_obj_id=C.si_id " +
              $"    WHERE (wm.wm_status=3 OR (wm.wm_status IS NULL AND A.pi_worker_id IS NULL)) AND LEN(A.pi_orga_id) > 0 {value} {minYearCondition} {maxYearCondition}" +
               "    GROUP BY p.F_ID " +
               ") AS C ON A.F_ID = C.F_ID ORDER BY A.F_ID";
            DataTable bCountTable = SqlHelper.ExecuteQuery(querySql_bCount);
            for(int i = 0; i < bCountTable.Rows.Count; i++)
            {
                DataRow dataRow = bCountTable.Rows[i];
                DataRow _data = tableEntity.Rows.Find(dataRow["F_ID"]);
                if (_data != null)
                {
                    _data["bCount"] = dataRow["bCount"];
                }
                //tableEntity.Rows[i]["bCount"] = bCountTable.Rows[i]["bCount"];
            }

            //文件数
            string querySql_fCount = "SELECT A.F_ID, A.F_Title, pCount+tCount+sCount fCount FROM ( " +
               "    SELECT p.F_Title, p.F_ID, COUNT(pfl.pfl_id) AS pCount FROM T_Plan AS p " +
               "    LEFT JOIN ( " +
               "        SELECT pi_id, pi_source_id, pi_orga_id, pi_year, pi_start_datetime, pi_worker_id FROM project_info WHERE pi_categor=2 UNION ALL " +
               "        SELECT ti_id, ti_source_id, ti_orga_id, ti_year, ti_start_datetime, ti_worker_id FROM topic_info WHERE ti_categor=-3 " +
               "    ) A ON A.pi_source_id = p.F_ID " +
               "    LEFT JOIN work_myreg wm ON wm.wm_obj_id = A.pi_id " +
               "    LEFT JOIN processing_file_list pfl ON pfl.pfl_obj_id=A.pi_id " +
              $"    WHERE (wm.wm_status=3 OR (wm.wm_status IS NULL AND A.pi_worker_id IS NULL)) AND LEN(A.pi_orga_id) > 0 {value} {minYearCondition} {maxYearCondition}" +
               "    GROUP BY p.F_Title, p.F_ID " +
               " ) AS A  " +
               " LEFT JOIN " +
               " ( " +
               "    SELECT p.F_ID, COUNT(pfl.pfl_id) AS tCount FROM T_Plan AS p " +
               "    LEFT JOIN ( " +
               "        SELECT pi_id, pi_source_id, pi_orga_id, pi_year, pi_start_datetime, pi_worker_id FROM project_info WHERE pi_categor=2 UNION ALL " +
               "        SELECT ti_id, ti_source_id, ti_orga_id, ti_year, ti_start_datetime, ti_worker_id FROM topic_info WHERE ti_categor=-3 " +
               "    ) A ON A.pi_source_id = p.F_ID " +
               "    LEFT JOIN work_myreg wm ON wm.wm_obj_id = A.pi_id " +
               "    LEFT JOIN ( " +
               "        SELECT ti_id, ti_obj_id FROM topic_info WHERE ti_categor=3 UNION ALL " +
               "        SELECT si_id, si_obj_id FROM subject_info " +
               "    ) B ON A.pi_id = B.ti_obj_id " +
               "    LEFT JOIN processing_file_list pfl ON pfl.pfl_obj_id=B.ti_id " +
              $"    WHERE (wm.wm_status=3 OR (wm.wm_status IS NULL AND A.pi_worker_id IS NULL)) AND LEN(A.pi_orga_id) > 0 {value} {minYearCondition} {maxYearCondition}" +
               "    GROUP BY p.F_ID " +
               ") AS B ON A.F_ID = B.F_ID " +
               "LEFT JOIN " +
               "( " +
               "    SELECT p.F_ID, COUNT(pfl.pfl_id) AS sCount FROM T_Plan AS p  " +
               "    LEFT JOIN ( " +
               "        SELECT pi_id, pi_source_id, pi_orga_id, pi_year, pi_start_datetime, pi_worker_id FROM project_info WHERE pi_categor=2 UNION ALL  " +
               "        SELECT ti_id, ti_source_id, ti_orga_id, ti_year, ti_start_datetime, ti_worker_id FROM topic_info WHERE ti_categor=-3 " +
               "    ) A ON A.pi_source_id = p.F_ID " +
               "    LEFT JOIN work_myreg wm ON wm.wm_obj_id = A.pi_id " +
               "    LEFT JOIN ( " +
               "        SELECT ti_id, ti_obj_id FROM topic_info WHERE ti_categor=3 UNION ALL " +
               "        SELECT si_id, si_obj_id FROM subject_info " +
               "    ) B ON A.pi_id = B.ti_obj_id " +
               "    LEFT JOIN subject_info C ON B.ti_id = C.si_obj_id " +
               "    LEFT JOIN processing_file_list pfl ON pfl.pfl_obj_id=C.si_id " +
              $"    WHERE (wm.wm_status=3 OR (wm.wm_status IS NULL AND A.pi_worker_id IS NULL)) AND LEN(A.pi_orga_id) > 0 {value} {minYearCondition} {maxYearCondition}" +
               "    GROUP BY p.F_ID " +
               ") AS C ON A.F_ID = C.F_ID ORDER BY A.F_ID";
            DataTable fCountTable = SqlHelper.ExecuteQuery(querySql_fCount);
            for(int i = 0; i < fCountTable.Rows.Count; i++)
            {
                DataRow dataRow = fCountTable.Rows[i];
                DataRow _data = tableEntity.Rows.Find(dataRow["F_ID"]);
                if (_data != null)
                {
                    _data["fCount"] = dataRow["fCount"];
                }
                //tableEntity.Rows[i]["fCount"] = fCountTable.Rows[i]["fCount"];
            }

            //必备文件缺失项目数
            string querySql_fbCount = "SELECT F_ID, fbCount FROM(SELECT p.F_ID, COUNT(A.pi_id) AS fbCount FROM T_Plan AS p " +
                "LEFT JOIN (SELECT * FROM(SELECT pi_id, pi_source_id, pi_orga_id, pi_year, pi_start_datetime, CASE COUNT(pfo.pfo_id) WHEN 0 THEN 0 ELSE 1 END oCount FROM(" +
                "   SELECT pi_id, pi_source_id, pi_orga_id, pi_year, pi_start_datetime, pi_worker_id FROM project_info WHERE pi_categor = 2 UNION ALL " +
                "   SELECT ti_id, ti_source_id, ti_orga_id, ti_year, ti_start_datetime, ti_worker_id FROM topic_info) A " +
                "LEFT JOIN work_myreg wm ON wm.wm_obj_id=A.pi_id AND (wm.wm_status=3 OR wm.wm_status IS NULL) " +
                "LEFT JOIN processing_file_lost pfo ON A.pi_id = pfo.pfo_obj_id AND pfo.pfo_ismust = 1 " +
               $"GROUP BY pi_id, pi_source_id, pi_orga_id, pi_year, pi_start_datetime) B WHERE B.oCount>0) AS A ON A.pi_source_id = p.F_ID " +
               $"WHERE 1=1 AND LEN(A.pi_orga_id)>0 {value} {minYearCondition} {maxYearCondition} " +
                "GROUP BY p.F_ID) AS B ORDER BY F_ID ";
            DataTable fbCountTable = SqlHelper.ExecuteQuery(querySql_fbCount);
            for(int i = 0; i < fbCountTable.Rows.Count; i++)
            {
                DataRow dataRow = fbCountTable.Rows[i];
                DataRow _data = tableEntity.Rows.Find(dataRow["F_ID"]);
                if (_data != null)
                {
                    _data["fbCount"] = dataRow["fbCount"];
                }
                //tableEntity.Rows[i]["fbCount"] = fbCountTable.Rows[i]["fbCount"];
            }

            tableEntity = HandleZX(tableEntity);
            object totalPcount = 0, totalFcount = 0, totalBcount = 0, totalFBcount = 0;
            totalPcount = tableEntity.Compute("SUM(pCount)", null);
            totalFcount = tableEntity.Compute("SUM(fCount)", null);
            totalBcount = tableEntity.Compute("SUM(bCount)", null);
            totalFBcount = tableEntity.Compute("SUM(fbCount)", null);
            tableEntity.Rows.Add(string.Empty, "合计", totalPcount, totalBcount, totalFcount, totalFBcount);
            view.DataSource = tableEntity;
            view.Columns[0].Visible = false;
            tabPane2.SelectedPageIndex = 0;
        }

        /// <summary>
        /// 汇总各专项为重大专项
        /// </summary>
        private DataTable HandleZX(DataTable table)
        {
            table.AcceptChanges();
            int pCount = 0, bCount = 0, fCount = 0, fbCount = 0;
            for(int i = 0; i < table.Rows.Count; i++)
            {
                DataRow row = table.Rows[i];
                int _pCount = ToolHelper.GetIntValue(row["pCount"], 0);
                if(_pCount == 0)
                {
                    table.Rows[i].Delete();
                }
                else
                {
                    if(ToolHelper.GetValue(row[0]).StartsWith("ZX"))
                    {
                        pCount += _pCount;
                        bCount += ToolHelper.GetIntValue(row["bCount"], 0);
                        fCount += ToolHelper.GetIntValue(row["fCount"], 0);
                        fbCount += ToolHelper.GetIntValue(row["fbCount"], 0);
                        table.Rows[i].Delete();
                    }
                }
            }
            if(pCount > 0)
                table.Rows.Add(new object[] { "ZX", "国家重大专项", pCount, bCount, fCount, fbCount });
            table.AcceptChanges();
            return table;
        }

        /// <summary>
        /// 计划类别点击事件
        /// </summary>
        private void Bc_Element_Click(object sender, EventArgs e)
        {
            SplashScreenManager.ShowDefaultWaitForm(this, false, false);
            string value = (sender as AccordionControlElement).Name;
            bc_LeftMenu.Tag = value;
            if ("all_ptype".Equals(value))
            {
                value = string.Empty;
            }
            else if ("ZX".Equals(value))
            {
                value = $"AND A.pi_source_id LIKE 'ZX%'";
                bc_LeftMenu.Tag = "ZX";
            }
            else
                value = $"AND A.pi_source_id = '{value}'";
            SetTableBySource(value, null, null);
            SplashScreenManager.CloseDefaultWaitForm();
        }

        private void SetTableBySource(string value, string minYear, string maxYear)
        {
            DataGridViewStyleHelper.ResetDataGridView(view, true);
            string minYearCondition = string.Empty;
            string maxYearCondition = string.Empty;
            if(!string.IsNullOrEmpty(minYear))
                minYearCondition = $"AND ISNULL(pi_year, TRY_CAST(SUBSTRING(pi_start_datetime, 1, 4) AS INT)) >= {minYear}";
            if(!string.IsNullOrEmpty(maxYear))
                maxYearCondition = $"AND ISNULL(pi_year, TRY_CAST(SUBSTRING(pi_start_datetime, 1, 4) AS INT)) <= {maxYear}";
            DataTable tableEntity = new DataTable();
            tableEntity.Columns.AddRange(new DataColumn[]
            {
                new DataColumn("F_ID", typeof(string)),
                new DataColumn("F_Title", typeof(string)),
                new DataColumn("pCount", typeof(int)),
                new DataColumn("bCount", typeof(int)),
                new DataColumn("fCount", typeof(int)),
                new DataColumn("fbCount", typeof(int))
            });
            tableEntity.PrimaryKey = new DataColumn[] { tableEntity.Columns[0] };
            view.Columns.AddRange(new DataGridViewColumn[]
            {
                new DataGridViewTextBoxColumn(){ DataPropertyName= "F_ID" },
                new DataGridViewTextBoxColumn(){ DataPropertyName= "F_Title", HeaderText= "来源单位名称" },
                new DataGridViewTextBoxColumn(){ DataPropertyName= "pCount", HeaderText= "项目/课题数" },
                new DataGridViewTextBoxColumn(){ DataPropertyName= "bCount", HeaderText= "盒数" },
                new DataGridViewTextBoxColumn(){ DataPropertyName= "fCount", HeaderText= "文件数" },
                new DataGridViewTextBoxColumn(){ DataPropertyName= "fbCount", HeaderText= "必备文件缺失项目数" },
            });

            //项目|课题数
            string querySql_pCount = "SELECT A.F_ID, A.F_Title, pCount+tCount+sCount pCount FROM ( " +
               "    SELECT p.F_Title, p.F_ID, COUNT(DISTINCT(A.pi_id)) AS pCount FROM T_SourceOrg AS p " +
               "    LEFT JOIN ( " +
               "        SELECT pi_id, pi_source_id, pi_orga_id, pi_year, pi_start_datetime, pi_worker_id FROM project_info WHERE pi_categor=2 UNION ALL " +
               "        SELECT ti_id, ti_source_id, ti_orga_id, ti_year, ti_start_datetime, ti_worker_id FROM topic_info WHERE ti_categor=-3 " +
               "    ) A ON A.pi_orga_id = p.F_ID " +
               "    LEFT JOIN work_myreg wm ON wm.wm_obj_id = A.pi_id " +
              $"    WHERE (wm.wm_status=3 OR (wm.wm_status IS NULL AND A.pi_worker_id IS NULL)) AND LEN(A.pi_source_id) > 0 {value} {minYearCondition} {maxYearCondition} " +
               "    GROUP BY p.F_Title, p.F_ID " +
               " ) AS A  " +
               " LEFT JOIN " +
               " ( " +
               "    SELECT p.F_ID, COUNT(DISTINCT(B.ti_id)) AS tCount FROM T_SourceOrg AS p " +
               "    LEFT JOIN ( " +
               "        SELECT pi_id, pi_source_id, pi_orga_id, pi_year, pi_start_datetime, pi_worker_id FROM project_info WHERE pi_categor=2 UNION ALL " +
               "        SELECT ti_id, ti_source_id, ti_orga_id, ti_year, ti_start_datetime, ti_worker_id FROM topic_info WHERE ti_categor=-3 " +
               "    ) A ON A.pi_orga_id = p.F_ID " +
               "    LEFT JOIN work_myreg wm ON wm.wm_obj_id = A.pi_id " +
               "    LEFT JOIN ( " +
               "        SELECT ti_id, ti_obj_id FROM topic_info WHERE ti_categor=3 UNION ALL " +
               "        SELECT si_id, si_obj_id FROM subject_info " +
               "    ) B ON A.pi_id = B.ti_obj_id " +
              $"    WHERE (wm.wm_status=3 OR (wm.wm_status IS NULL AND A.pi_worker_id IS NULL)) AND LEN(A.pi_source_id) > 0 {value} {minYearCondition} {maxYearCondition} " +
               "    GROUP BY p.F_ID " +
               ") AS B ON A.F_ID = B.F_ID " +
               "LEFT JOIN " +
               "( " +
               "    SELECT p.F_ID, COUNT(DISTINCT(C.si_id)) AS sCount FROM T_SourceOrg AS p  " +
               "    LEFT JOIN ( " +
               "        SELECT pi_id, pi_source_id, pi_orga_id, pi_year, pi_start_datetime, pi_worker_id FROM project_info WHERE pi_categor=2 UNION ALL  " +
               "        SELECT ti_id, ti_source_id, ti_orga_id, ti_year, ti_start_datetime, ti_worker_id FROM topic_info WHERE ti_categor=-3 " +
               "    ) A ON A.pi_orga_id = p.F_ID " +
               "    LEFT JOIN work_myreg wm ON wm.wm_obj_id = A.pi_id " +
               "    LEFT JOIN ( " +
               "        SELECT ti_id, ti_obj_id FROM topic_info WHERE ti_categor=3 UNION ALL " +
               "        SELECT si_id, si_obj_id FROM subject_info " +
               "    ) B ON A.pi_id = B.ti_obj_id " +
               "    LEFT JOIN subject_info C ON B.ti_id = C.si_obj_id " +
              $"    WHERE (wm.wm_status=3 OR (wm.wm_status IS NULL AND A.pi_worker_id IS NULL)) AND LEN(A.pi_source_id) > 0 {value} {minYearCondition} {maxYearCondition} " +
               "    GROUP BY p.F_ID " +
               ") AS C ON A.F_ID = C.F_ID ORDER BY A.F_ID";
            DataTable pCountTable = SqlHelper.ExecuteQuery(querySql_pCount);
            foreach (DataRow row in pCountTable.Rows)
            {
                tableEntity.Rows.Add(row["F_ID"], row["F_Title"], row["pCount"]);
            }

            //盒数
            string querySql_bCount = "SELECT A.F_ID, A.F_Title, pCount+tCount+sCount bCount FROM ( " +
               "    SELECT p.F_Title, p.F_ID, COUNT(pb.pb_id) AS pCount FROM T_SourceOrg AS p " +
               "    LEFT JOIN ( " +
               "        SELECT pi_id, pi_source_id, pi_orga_id, pi_year, pi_start_datetime, pi_worker_id FROM project_info WHERE pi_categor=2 UNION ALL " +
               "        SELECT ti_id, ti_source_id, ti_orga_id, ti_year, ti_start_datetime, ti_worker_id FROM topic_info WHERE ti_categor=-3 " +
               "    ) A ON A.pi_orga_id = p.F_ID " +
               "    LEFT JOIN work_myreg wm ON wm.wm_obj_id=A.pi_id " +
               "    LEFT JOIN processing_box pb ON pb.pb_obj_id=A.pi_id " +
              $"    WHERE (wm.wm_status=3 OR (wm.wm_status IS NULL AND A.pi_worker_id IS NULL)) AND LEN(A.pi_source_id) > 0 {value} {minYearCondition} {maxYearCondition} " +
               "    GROUP BY p.F_Title, p.F_ID " +
               " ) AS A  " +
               " LEFT JOIN " +
               " ( " +
               "    SELECT p.F_ID, COUNT(pb.pb_id) AS tCount FROM T_SourceOrg AS p " +
               "    LEFT JOIN ( " +
               "        SELECT pi_id, pi_source_id, pi_orga_id, pi_year, pi_start_datetime, pi_worker_id FROM project_info WHERE pi_categor=2 UNION ALL " +
               "        SELECT ti_id, ti_source_id, ti_orga_id, ti_year, ti_start_datetime, ti_worker_id FROM topic_info WHERE ti_categor=-3 " +
               "    ) A ON A.pi_orga_id = p.F_ID " +
               "    LEFT JOIN work_myreg wm ON wm.wm_obj_id = A.pi_id " +
               "    LEFT JOIN ( " +
               "        SELECT ti_id, ti_obj_id FROM topic_info WHERE ti_categor=3 UNION ALL " +
               "        SELECT si_id, si_obj_id FROM subject_info " +
               "    ) B ON A.pi_id = B.ti_obj_id " +
               "    LEFT JOIN processing_box pb ON pb.pb_obj_id=B.ti_id " +
              $"    WHERE (wm.wm_status=3 OR (wm.wm_status IS NULL AND A.pi_worker_id IS NULL)) AND LEN(A.pi_source_id) > 0 {value} {minYearCondition} {maxYearCondition} " +
               "    GROUP BY p.F_ID " +
               ") AS B ON A.F_ID = B.F_ID " +
               "LEFT JOIN " +
               "( " +
               "    SELECT p.F_ID, COUNT(pb.pb_id) AS sCount FROM T_SourceOrg AS p  " +
               "    LEFT JOIN ( " +
               "        SELECT pi_id, pi_source_id, pi_orga_id, pi_year, pi_start_datetime, pi_worker_id FROM project_info WHERE pi_categor=2 UNION ALL  " +
               "        SELECT ti_id, ti_source_id, ti_orga_id, ti_year, ti_start_datetime, ti_worker_id FROM topic_info WHERE ti_categor=-3 " +
               "    ) A ON A.pi_orga_id = p.F_ID " +
               "    LEFT JOIN work_myreg wm ON wm.wm_obj_id = A.pi_id " +
               "    LEFT JOIN ( " +
               "        SELECT ti_id, ti_obj_id FROM topic_info WHERE ti_categor=3 UNION ALL " +
               "        SELECT si_id, si_obj_id FROM subject_info " +
               "    ) B ON A.pi_id = B.ti_obj_id " +
               "    LEFT JOIN subject_info C ON B.ti_id = C.si_obj_id " +
               "    LEFT JOIN processing_box pb ON pb.pb_obj_id=C.si_id " +
              $"    WHERE (wm.wm_status=3 OR (wm.wm_status IS NULL AND A.pi_worker_id IS NULL)) AND LEN(A.pi_source_id) > 0 {value} {minYearCondition} {maxYearCondition} " +
               "    GROUP BY p.F_ID " +
               ") AS C ON A.F_ID = C.F_ID ORDER BY A.F_ID";
            DataTable bCountTable = SqlHelper.ExecuteQuery(querySql_bCount);
            for (int i = 0; i < bCountTable.Rows.Count; i++)
            {
                DataRow dataRow = bCountTable.Rows[i];
                DataRow _data = tableEntity.Rows.Find(dataRow["F_ID"]);
                if (_data != null)
                {
                    _data["bCount"] = dataRow["bCount"];
                }
            }

            //文件数
            string querySql_fCount = "SELECT A.F_ID, A.F_Title, pCount+tCount+sCount fCount FROM ( " +
               "    SELECT p.F_Title, p.F_ID, COUNT(pfl.pfl_id) AS pCount FROM T_SourceOrg AS p " +
               "    LEFT JOIN ( " +
               "        SELECT pi_id, pi_source_id, pi_orga_id, pi_year, pi_start_datetime, pi_worker_id FROM project_info WHERE pi_categor=2 UNION ALL " +
               "        SELECT ti_id, ti_source_id, ti_orga_id, ti_year, ti_start_datetime, ti_worker_id FROM topic_info WHERE ti_categor=-3 " +
               "    ) A ON A.pi_orga_id = p.F_ID " +
               "    LEFT JOIN work_myreg wm ON wm.wm_obj_id=A.pi_id " +
               "    LEFT JOIN processing_file_list pfl ON pfl.pfl_obj_id=A.pi_id " +
              $"    WHERE (wm.wm_status=3 OR (wm.wm_status IS NULL AND A.pi_worker_id IS NULL)) AND LEN(A.pi_source_id) > 0 {value} {minYearCondition} {maxYearCondition} " +
               "    GROUP BY p.F_Title, p.F_ID " +
               " ) AS A  " +
               " LEFT JOIN " +
               " ( " +
               "    SELECT p.F_ID, COUNT(pfl.pfl_id) AS tCount FROM T_SourceOrg AS p " +
               "    LEFT JOIN ( " +
               "        SELECT pi_id, pi_source_id, pi_orga_id, pi_year, pi_start_datetime, pi_worker_id FROM project_info WHERE pi_categor=2 UNION ALL " +
               "        SELECT ti_id, ti_source_id, ti_orga_id, ti_year, ti_start_datetime, ti_worker_id FROM topic_info WHERE ti_categor=-3 " +
               "    ) A ON A.pi_orga_id = p.F_ID " +
               "    LEFT JOIN work_myreg wm ON wm.wm_obj_id = A.pi_id " +
               "    LEFT JOIN ( " +
               "        SELECT ti_id, ti_obj_id FROM topic_info WHERE ti_categor=3 UNION ALL " +
               "        SELECT si_id, si_obj_id FROM subject_info " +
               "    ) B ON A.pi_id = B.ti_obj_id " +
               "    LEFT JOIN processing_file_list pfl ON pfl.pfl_obj_id=B.ti_id " +
              $"    WHERE (wm.wm_status=3 OR (wm.wm_status IS NULL AND A.pi_worker_id IS NULL)) AND LEN(A.pi_source_id) > 0 {value} {minYearCondition} {maxYearCondition} " +
               "    GROUP BY p.F_ID " +
               ") AS B ON A.F_ID = B.F_ID " +
               "LEFT JOIN " +
               "( " +
               "    SELECT p.F_ID, COUNT(pfl.pfl_id) AS sCount FROM T_SourceOrg AS p  " +
               "    LEFT JOIN ( " +
               "        SELECT pi_id, pi_source_id, pi_orga_id, pi_year, pi_start_datetime, pi_worker_id FROM project_info WHERE pi_categor=2 UNION ALL  " +
               "        SELECT ti_id, ti_source_id, ti_orga_id, ti_year, ti_start_datetime, ti_worker_id FROM topic_info WHERE ti_categor=-3 " +
               "    ) A ON A.pi_orga_id = p.F_ID " +
               "    LEFT JOIN work_myreg wm ON wm.wm_obj_id = A.pi_id " +
               "    LEFT JOIN ( " +
               "        SELECT ti_id, ti_obj_id FROM topic_info WHERE ti_categor=3 UNION ALL " +
               "        SELECT si_id, si_obj_id FROM subject_info " +
               "    ) B ON A.pi_id = B.ti_obj_id " +
               "    LEFT JOIN subject_info C ON B.ti_id = C.si_obj_id " +
               "    LEFT JOIN processing_file_list pfl ON pfl.pfl_obj_id=C.si_id " +
              $"    WHERE (wm.wm_status=3 OR (wm.wm_status IS NULL AND A.pi_worker_id IS NULL)) AND LEN(A.pi_source_id) > 0 {value} {minYearCondition} {maxYearCondition} " +
               "    GROUP BY p.F_ID " +
               ") AS C ON A.F_ID = C.F_ID ORDER BY A.F_ID";
            DataTable fCountTable = SqlHelper.ExecuteQuery(querySql_fCount);
            for (int i = 0; i < fCountTable.Rows.Count; i++)
            {
                DataRow dataRow = fCountTable.Rows[i];
                DataRow _data = tableEntity.Rows.Find(dataRow["F_ID"]);
                if (_data != null)
                {
                    _data["fCount"] = dataRow["fCount"];
                }
                //tableEntity.Rows[i]["fCount"] = fCountTable.Rows[i]["fCount"];
            }

            //必备文件缺失项目数
            string querySql_fbCount = "SELECT F_ID, fbCount FROM(SELECT p.F_ID, COUNT(A.pi_id) AS fbCount FROM T_SourceOrg AS p " +
                "LEFT JOIN (SELECT * FROM(SELECT pi_id, pi_source_id, pi_orga_id, pi_year, pi_start_datetime, CASE COUNT(pfo.pfo_id) WHEN 0 THEN 0 ELSE 1 END oCount FROM(" +
                "   SELECT pi_id, pi_source_id, pi_orga_id, pi_year, pi_start_datetime FROM project_info WHERE pi_categor = 2 UNION ALL " +
                "   SELECT ti_id, ti_source_id, ti_orga_id, ti_year, ti_start_datetime FROM topic_info) A " +
                "LEFT JOIN work_myreg wm ON wm.wm_obj_id=A.pi_id AND (wm.wm_status=3 OR wm.wm_status IS NULL) " +
                "LEFT JOIN processing_file_lost pfo ON A.pi_id = pfo.pfo_obj_id AND pfo.pfo_ismust = 1 " +
               $"GROUP BY pi_id, pi_source_id, pi_orga_id, pi_year, pi_start_datetime) B WHERE B.oCount>0) AS A ON A.pi_orga_id = p.F_ID " +
               $"WHERE 1=1 AND LEN(A.pi_source_id)>0 {value} {minYearCondition} {maxYearCondition} " +
                "GROUP BY p.F_ID) AS B ORDER BY F_ID ";
            DataTable fbCountTable = SqlHelper.ExecuteQuery(querySql_fbCount);
            for (int i = 0; i < fbCountTable.Rows.Count; i++)
            {
                DataRow dataRow = fbCountTable.Rows[i];
                DataRow _data = tableEntity.Rows.Find(dataRow["F_ID"]);
                if (_data != null)
                {
                    _data["fbCount"] = dataRow["fbCount"];
                }
                //tableEntity.Rows[i]["fbCount"] = fbCountTable.Rows[i]["fbCount"];
            }

            tableEntity = HandleZX(tableEntity);
            object totalPcount = 0, totalFcount = 0, totalBcount = 0, totalFBcount = 0;
            totalPcount = tableEntity.Compute("SUM(pCount)", null);
            totalFcount = tableEntity.Compute("SUM(fCount)", null);
            totalBcount = tableEntity.Compute("SUM(bCount)", null);
            totalFBcount = tableEntity.Compute("SUM(fbCount)", null);
            tableEntity.Rows.Add(string.Empty, "合计", totalPcount, totalBcount, totalFcount, totalFBcount);
            view.DataSource = tableEntity;

            view.Columns[0].Visible = false;
            tabPane2.SelectedPageIndex = 0;
        }

        private void TabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            pal_Local.Visible = false;
            if(tabControl1.SelectedIndex == 0)
            {
                ac_LeftMenu.SelectedElement = ac_LeftMenu.Elements[0];
                Item_Click(ac_LeftMenu.Elements[0], null);
            }
            else if(tabControl1.SelectedIndex == 1)
            {
                bc_LeftMenu.SelectedElement = bc_LeftMenu.Elements[0];
                Bc_Element_Click(bc_LeftMenu.Elements[0], null);
            }
            else if(tabControl1.SelectedIndex == 2)
            {
                cbo_PlanList.SelectedIndex = 0;
                cbo_SourceList.SelectedIndex = 0;
                pal_Local.Visible = true;
                cc_LeftMenu.SelectedElement = cc_LeftMenu.Elements[0];
                LocalElement_Click(cc_LeftMenu.Elements[0], null);
            }
        }

        /// <summary>
        /// 加载中国地图信息
        /// </summary>
        /// <param name="mapPanel">地图框架</param>
        /// <param name="mapFile">地图文件路径</param>
        private void LoadMapDataInstince(Panel mapPanel, string mapFile)
        {
            mapPanel.Controls.Clear();
            if (!Cef.IsInitialized)
            {
                CefSettings settings = new CefSettings();
                Cef.Initialize(settings);
            }
            mapPanel.Controls.Add(
                new ChromiumWebBrowser(mapFile)
                {
                    Dock = DockStyle.Fill
                }
             );
        }

        private void tabPane1_SelectedPageIndexChanged(object sender, EventArgs e)
        {
            if(tabPane1.SelectedPageIndex == 1)
            {
                tabNavigationPage2.Update();
                TabControl1_SelectedIndexChanged(null, null);
            }
        }

        /// <summary>
        /// 图形加载事件
        /// </summary>
        private void tabPane2_SelectedPageIndexChanged(object sender, EventArgs e)
        {
            chart1.Controls.Clear();
            chart2.Series.Clear();
            chart3.Series.Clear();
            if(tabPane2.SelectedPageIndex == 1)
            {
                tabPane2.Update();
                LoadImageInfo();
                if (tabControl1.SelectedIndex == 2)
                {
                    //地域单独加载中国地图
                    LoadMapData();
                }
                else
                {
                    mapPanel.Visible = false;
                    chart2.Visible = true;
                    chart3.Top = chart2.Top + chart2.Height + 5;
                }
            }
        }

        /// <summary>
        /// 加载地图
        /// </summary>
        private void LoadMapData()
        {
            string mapFile = Application.StartupPath + @"\Datas\chinamap.html";
            if (File.Exists(mapFile))
            {
                mapPanel.Visible = true;
                chart2.Visible = false;
                chart3.Top = mapPanel.Top + mapPanel.Height + 5;

                mapPanel.Location = chart2.Location;
                mapPanel.Width = chart2.Width;
                string mapString = File.ReadAllText(mapFile, Encoding.UTF8);
                int type = rdo_ProCount.Checked ? 2 : rdo_BoxCount.Checked ? 3 : rdo_FileCount.Checked ? 4 : rdo_FileBCount.Checked ? 5 : 0;
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < view.RowCount - 1; i++)
                {
                    DataGridViewRow row = view.Rows[i];
                    builder.Append("{name: '" + row.Cells[1].Value + "', value: '" + row.Cells[type].Value + "' },");
                }
                mapString = mapString.Replace("var dataMap = []", $"var dataMap = [{builder.ToString()}]");
                mapString = mapString.Replace("当前数据", view.Columns[type].HeaderText);
                string tempFile = Path.GetDirectoryName(mapFile) + "\\temp\\" + DateTime.Now.Millisecond + ".html";
                if (!Directory.Exists(Path.GetDirectoryName(tempFile)))
                    Directory.CreateDirectory(Path.GetDirectoryName(tempFile));
                File.WriteAllText(tempFile, mapString, Encoding.UTF8);
                LoadMapDataInstince(mapPanel, tempFile);
            }
        }

        /// <summary>
        /// 加载图形实例
        /// </summary>
        private void LoadImageInfo()
        {
            string name = rdo_ProCount.Checked ? rdo_ProCount.Text : rdo_BoxCount.Checked ? rdo_BoxCount.Text : rdo_FileCount.Checked ? rdo_FileCount.Text : rdo_FileBCount.Text;
            chart1.Controls.Clear(); 
            chart2.Series.Clear(); chart2.BeginInit();
            chart3.Series.Clear(); chart3.BeginInit();
            int tabType = tabControl1.SelectedIndex;

            //项目/课题2，盒3，文件4
            int typeIndex = rdo_ProCount.Checked ? 2 : rdo_BoxCount.Checked ? 3 : rdo_FileCount.Checked ? 4 : 5;
            Series amount2 = new Series()
            {
                IsValueShownAsLabel = true,
                ShadowOffset = 5,
                Palette = ChartColorPalette.BrightPastel,
                ChartType = SeriesChartType.Column
            };
            double maxNum = 0;
            StringBuilder keyData = new StringBuilder();
            StringBuilder valueData = new StringBuilder();
            for(int i = 0; i < view.RowCount - 1; i++)
            {
                object objectName = view.Rows[i].Cells[1].Value;
                int value = ToolHelper.GetIntValue(view.Rows[i].Cells[typeIndex].Value, 0);
                maxNum = value > maxNum ? value : maxNum;
                keyData.Append($"'{objectName}',");
                valueData.AppendLine("{ value: " + value + ", name: '" + objectName + "' },");
                amount2.Points.AddXY(objectName, value);
            }
            amount2.ToolTip = "#VALX(#PERCENT{P2})\n" + name + "：#VALY";
            //加载饼图
            string typeName = view.Columns[typeIndex].HeaderText;
            LoadPie(chart1, keyData, valueData, typeName);
            //地域选项卡不用加载柱形图（由地图代替）
            if (tabType != 2)
            {
                chart2.Series.Add(amount2);
                chart2.ChartAreas[0].AxisY.Maximum = maxNum;
            }
            //来源单位0
            if(tabType == 0)
            {
                //年度统计
                if(typeIndex == 2)
                {
                    string orgName = ac_LeftMenu.SelectedElement == null ? "ace_all" : ac_LeftMenu.SelectedElement.Name;
                    string _minYear = txt_QuerySyear.Text, _maxYear = txt_QueryEyear.Text;
                    if(string.IsNullOrEmpty(_minYear) && string.IsNullOrEmpty(_maxYear))
                    {
                        string querySQL_Year = "SELECT MIN(myear) maxyear, MAX(myear) minyear FROM( " +
                            "SELECT ISNULL(pi_year, TRY_CAST(SUBSTRING(pi_start_datetime, 1, 4) AS INT)) AS myear, pi_orga_id FROM " +
                            "(SELECT pi_year, pi_start_datetime, pi_orga_id FROM project_info WHERE pi_categor = 2 " +
                            "UNION ALL SELECT ti_year, ti_start_datetime, ti_orga_id FROM topic_info WHERE ti_categor = -3 )a)b " +
                            "WHERE myear IS NOT NULL AND myear> '0' AND myear<= YEAR(SYSDATETIME()) ";
                        if(!"ace_all".Equals(orgName))
                            querySQL_Year += $"AND pi_orga_id='{orgName}' ";
                        object[] years = SqlHelper.ExecuteRowsQuery(querySQL_Year);
                        _minYear = ToolHelper.GetValue(years[0]);
                        _maxYear = ToolHelper.GetValue(years[1]);
                    }
                    if(!string.IsNullOrEmpty(_minYear) && !string.IsNullOrEmpty(_maxYear))
                    {
                        int minYear = ToolHelper.GetIntValue(_minYear);
                        int maxYear = ToolHelper.GetIntValue(_maxYear);
                        //逐行添加
                        for(int j = 0; j < view.RowCount - 1; j++)
                        {
                            //来源单位编号
                            object orgCode = view.Rows[j].Cells[0].Value;
                            string sorName = "ace_all".Equals(orgName) ? string.Empty : $"AND pi_orga_id='{orgName}'";
                            Series series = new Series($"{view.Rows[j].Cells[1].Value}")
                            {
                                BorderWidth = 2,
                                IsXValueIndexed = true,
                                ChartType = SeriesChartType.Spline
                            };
                            string querySQL = "SELECT nian, COUNT(pi_id) num FROM( " +
                                "SELECT ISNULL(pi_year, TRY_CAST(SUBSTRING(pi_start_datetime, 1, 4) AS INT)) as nian, pi_source_id, pi_orga_id, pi_id FROM(" +
                                "SELECT pi_id, pi_start_datetime, pi_year, pi_source_id, pi_orga_id FROM project_info WHERE(pi_categor = 2) UNION ALL " +
                                "SELECT ti_id, ti_start_datetime, ti_year, ti_source_id, ti_orga_id FROM topic_info WHERE(ti_categor = -3)) AS A ) A " +
                               $"WHERE(A.nian BETWEEN {minYear} AND {maxYear}) AND(pi_source_id = '{orgCode}') {sorName} GROUP BY nian ";
                            Dictionary<object, int> pair = SqlHelper.GetKeyValuePair(querySQL);
                            for(int i = minYear; i <= maxYear; i++)
                            {
                                pair.TryGetValue(i, out int value);
                                series.Points.AddXY(i, value);
                            }
                            series.ToolTip = "#VALX年度（" + view.Rows[j].Cells[1].Value + "）\n项目/课题数：#VALY";
                            chart3.Series.Add(series);
                        }
                    }
                }
            }
            //计划类别1
            else if(tabType == 1)
            {
                //年度统计
                if(typeIndex == 2)
                {
                    object orgName = bc_LeftMenu.Tag ?? "all_ptype";
                    string _minYear = txt_QuerySyear.Text, _maxYear = txt_QueryEyear.Text;
                    if(string.IsNullOrEmpty(_minYear) && string.IsNullOrEmpty(_maxYear))
                    {
                        string querySQL_Year = "SELECT MIN(myear) maxyear, MAX(myear) minyear FROM( " +
                        "SELECT ISNULL(pi_year, TRY_CAST(SUBSTRING(pi_start_datetime, 1, 4) AS INT)) AS myear, pi_source_id FROM " +
                        "(SELECT pi_year, pi_start_datetime, pi_source_id FROM project_info WHERE pi_categor = 2 " +
                        "UNION ALL SELECT ti_year, ti_start_datetime, ti_source_id FROM topic_info WHERE ti_categor = -3 )a)b " +
                        "WHERE myear IS NOT NULL AND myear> '0' AND myear<= YEAR(SYSDATETIME()) ";
                        if(!"all_ptype".Equals(orgName))
                            querySQL_Year += $"AND pi_source_id LIKE '%{orgName}%' ";
                        object[] years = SqlHelper.ExecuteRowsQuery(querySQL_Year);
                        _minYear = ToolHelper.GetValue(years[0]);
                        _maxYear = ToolHelper.GetValue(years[1]);
                    }
                    if(!string.IsNullOrEmpty(_minYear) && !string.IsNullOrEmpty(_maxYear))
                    {
                        int minYear = ToolHelper.GetIntValue(_minYear);
                        int maxYear = ToolHelper.GetIntValue(_maxYear);
                        //逐行添加
                        for(int j = 0; j < view.RowCount - 1; j++)
                        {
                            object orgCode = view.Rows[j].Cells[0].Value;
                            string sorName = "all_ptype".Equals(orgName) ? string.Empty : $"AND pi_source_id LIKE '%{orgName}%'";
                            Series series = new Series($"{view.Rows[j].Cells[1].Value}")
                            {
                                BorderWidth = 2,
                                IsXValueIndexed = true,
                                ChartType = SeriesChartType.Spline
                            };
                            string querySQL = "SELECT nian, COUNT(pi_id) FROM( " +
                                "SELECT ISNULL(pi_year, TRY_CAST(SUBSTRING(pi_start_datetime, 1, 4) AS INT)) as nian, pi_source_id, pi_orga_id, pi_id FROM(" +
                                "SELECT pi_id, pi_year, pi_start_datetime, pi_orga_id, pi_source_id FROM project_info WHERE pi_categor = 2 UNION ALL " +
                                "SELECT ti_id, ti_year, ti_start_datetime, ti_orga_id, ti_source_id FROM topic_info WHERE ti_categor = -3) A ) A " +
                               $"WHERE A.nian BETWEEN {minYear} AND {maxYear} AND pi_orga_id = '{orgCode}' {sorName} GROUP BY nian ";
                            Dictionary<object, int> pair = SqlHelper.GetKeyValuePair(querySQL);
                            for(int i = minYear; i <= maxYear; i++)
                            {
                                pair.TryGetValue(i, out int value);
                                series.Points.AddXY(i, value);
                            }
                            series.ToolTip = "#VALX年度（" + view.Rows[j].Cells[1].Value + "）\n项目/课题数：#VALY";
                            chart3.Series.Add(series);
                        }
                    }
                }
            }
            //地域
            else if(tabType == 2)
            {
                //年度统计
                if(typeIndex == 2)
                {
                    string proName = cc_LeftMenu.SelectedElement == null ? "all_ltype" : cc_LeftMenu.SelectedElement.Name;
                    string _minYear = txt_QuerySyear.Text, _maxYear = txt_QueryEyear.Text;
                    //如果时间查询条件为空，则获取默认最大最小时间
                    if(string.IsNullOrEmpty(_minYear) && string.IsNullOrEmpty(_maxYear))
                    {
                        string querySQL_Local = "SELECT MIN(myear) maxyear, MAX(myear) minyear FROM( " +
                            "SELECT ISNULL(pi_year, TRY_CAST(SUBSTRING(pi_start_datetime, 1, 4) AS INT)) AS myear, pi_province FROM (" +
                            "SELECT pi_year, pi_start_datetime, pi_province FROM project_info WHERE pi_categor = 2 UNION ALL " +
                            "SELECT ti_year, ti_start_datetime, ti_province FROM topic_info WHERE ti_categor = -3 )A)B " +
                            "WHERE myear IS NOT NULL AND myear> '0' AND myear<= YEAR(SYSDATETIME()) ";
                        if(!"all_ltype".Equals(proName))
                            querySQL_Local += $"AND pi_province = '{proName}' ";
                        object[] years = SqlHelper.ExecuteRowsQuery(querySQL_Local);
                        _minYear = ToolHelper.GetValue(years[0]);
                        _maxYear = ToolHelper.GetValue(years[1]);
                    }
                    if(!string.IsNullOrEmpty(_minYear) && !string.IsNullOrEmpty(_maxYear))
                    {
                        int minYear = ToolHelper.GetIntValue(_minYear);
                        int maxYear = ToolHelper.GetIntValue(_maxYear);
                        //逐行添加
                        for(int j = 0; j < view.RowCount - 1; j++)
                        {
                            //if("all_ltype".Equals(proName)) continue;
                            object localID = view.Rows[j].Cells[0].Value;
                            Series series = new Series($"{view.Rows[j].Cells[1].Value}")
                            {
                                BorderWidth = 2,
                                IsXValueIndexed = true,
                                ChartType = SeriesChartType.Spline
                            };
                            string querySQL = "SELECT nian, COUNT(pi_id) num FROM( " +
                                "SELECT ISNULL(pi_year, TRY_CAST(SUBSTRING(pi_start_datetime, 1, 4) AS INT)) as nian, pi_province, pi_id FROM(" +
                                "SELECT pi_id, pi_start_datetime, pi_year, pi_province FROM project_info WHERE pi_categor = 2 UNION ALL " +
                                "SELECT ti_id, ti_start_datetime, ti_year, ti_province FROM topic_info WHERE ti_categor = -3) AS A ) A " +
                               $"WHERE A.nian BETWEEN {minYear} AND {maxYear} AND pi_province = '{localID}' GROUP BY nian ";
                            Dictionary<object, int> pair = SqlHelper.GetKeyValuePair(querySQL);
                            for(int i = minYear; i <= maxYear; i++)
                            {
                                pair.TryGetValue(i, out int value);
                                series.Points.AddXY(i, value);
                            }
                            series.ToolTip = "#VALX年度（" + view.Rows[j].Cells[1].Value + "）\n项目/课题数：#VALY";
                            chart3.Series.Add(series);
                        }
                    }
                }
            }
            chart2.EndInit();
            chart3.EndInit();
        }

        /// <summary>
        /// 加载饼图
        /// </summary>
        private void LoadPie(Panel piePanel, object key, object value, object typeName)
        {
            string filePath = Application.StartupPath + "\\Datas\\pie.html";
            if (File.Exists(filePath))
            {
                string pieString = File.ReadAllText(filePath, Encoding.UTF8);
                pieString = pieString.Replace("data: [],", $"data: [{value.ToString()}],");
                pieString = pieString.Replace("data: []", $"data: [{key.ToString()}]");
                pieString = pieString.Replace("访问来源", $"{typeName}");
                string tempFile = Path.GetDirectoryName(filePath) + "\\temp\\pie.html";
                if (!Directory.Exists(Path.GetDirectoryName(tempFile)))
                    Directory.CreateDirectory(Path.GetDirectoryName(tempFile));
                File.WriteAllText(tempFile, pieString, Encoding.UTF8);

                piePanel.Controls.Clear();
                if (!Cef.IsInitialized)
                {
                    CefSettings settings = new CefSettings();
                    Cef.Initialize(settings);
                }
                piePanel.Controls.Add(
                    new ChromiumWebBrowser(tempFile)
                    {
                        Dock = DockStyle.Fill
                    }
                 );
            }
        }
        private void Btn_Export_Click(object sender, EventArgs e)
        {
            object dataSource = view.DataSource;
            if(dataSource != null)
            {
                saveFileDialog.Filter = "Execl 表单(*.CSV)|*.CSV";
                saveFileDialog.Title = "选择导出文件夹...";
                if(saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    DataTable table = (DataTable)dataSource;
                    if(table.Rows.Count > 1)
                    {
                        bool flag = MicrosoftWordHelper.GetCsvFromDataTable(table, saveFileDialog.FileName);
                        if(flag)
                        {
                            DialogResult dialogResult = DevExpress.XtraEditors.XtraMessageBox.Show("导出成功，是否立即打开文件?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk);
                            if(dialogResult == DialogResult.Yes)
                            {
                                WinFormOpenHelper.OpenWinForm(0, "open", saveFileDialog.FileName, null, null, ShowWindowCommands.SW_NORMAL);
                            }
                        }
                    }
                }
            }
        }

        private void Btn_Year_Properties_ButtonClick(object sender, EventArgs e)
        {
            txt_QuerySyear.ErrorText = null;
            txt_QueryEyear.ErrorText = null;
            string minYear = txt_QuerySyear.Text, maxYear = txt_QueryEyear.Text;
            if(string.IsNullOrEmpty(minYear) && string.IsNullOrEmpty(maxYear))
                return;
            int tabType = tabControl1.SelectedIndex;
            if(tabType == 0)
            {
                string value = ac_LeftMenu.SelectedElement.Name;
                if("ace_all".Equals(value))
                    value = string.Empty;
                else
                    value = $"AND A.pi_orga_id = '{value}'";

                SetTableByOrga(value, minYear, maxYear);
            }
            else if(tabType == 1)
            {
                string value = bc_LeftMenu.SelectedElement.Name;
                if("all_ptype".Equals(value))
                    value = string.Empty;
                else
                    value = $"AND A.pi_source_id = '{value}'";

                SetTableBySource(value, minYear, maxYear);
            }
            else if(tabType == 2)
            {
                AccordionControlElement element = cc_LeftMenu.SelectedElement;
                if(element != null)
                    LocalElement_Click(element, null);
            }
            tabPane2.SelectedPageIndex = 0;

        }

        private void ProCount_MouseClick(object sender, MouseEventArgs e)
        {
            LoadImageInfo();
            if (tabControl1.SelectedIndex == 2)
            {
                //地域单独加载中国地图
                LoadMapData();
            }
            else
            {
                mapPanel.Visible = false;
                chart2.Visible = true;
                chart3.Top = chart2.Top + chart2.Height + 5;
            }
        }

        private void tabPane3_SelectedPageChanged(object sender, SelectedPageChangedEventArgs e)
        {
            LoadChartInfo();
        }

        private void LoadChartInfo()
        {
            if(tabPane3.SelectedPageIndex == 0) return;
            chart4.Series.Clear(); chart5.Series.Clear();
            int a = rdo1.Checked ? 1 : rdo2.Checked ? 2 : rdo3.Checked ? 3 : rdo4.Checked ? 6 : 7;
            double maxNum = 0;

            Series amount = new Series()
            {
                IsValueShownAsLabel = true,
                IsXValueIndexed = false,
                ShadowOffset = 5,
                ChartType = SeriesChartType.Column,
            };
            Series amount1 = new Series()
            {
                IsValueShownAsLabel = true,
                IsXValueIndexed = false,
                BorderWidth = 2,
            };
            if(rdo_sort_date.Checked)//按日期
            {
                amount1.ChartType = SeriesChartType.Spline;
            }
            else//按人员
            {
                chart4.ChartAreas[0].AxisX.LabelStyle.Angle = 0;
                amount1["PieLabelStyle"] = "Outside";//将文字移到外侧 
                amount1["PieLineColor"] = "Black";//绘制黑色的连线。 
                amount1.ChartType = SeriesChartType.Pie;
                amount1.Label = "#VALX:#PERCENT{P2}";
            }
            int rowCount = 0;
            for(int i = 0; i < countView.RowCount; i++)
            {
                int value = ToolHelper.GetIntValue(countView.Rows[i].Cells[a].Value, 0);
                if(value > 0)
                {
                    rowCount++;
                    maxNum = value > maxNum ? value : maxNum;
                    amount.Points.AddXY(countView.Rows[i].Cells[0].Value, value);
                    amount1.Points.AddXY(countView.Rows[i].Cells[0].Value, value);
                }
            }

            chart4.ChartAreas[0] = GetInitialChartArea(rowCount);
            chart5.ChartAreas[0] = GetInitialChartArea(rowCount);

            chart4.Series.Add(amount);
            chart5.Series.Add(amount1);

        }

        private ChartArea GetInitialChartArea(int MaxAxisXSize)
        {
            ChartArea chartArea = new ChartArea();
            chartArea.AxisX.Interval = 1D;
            chartArea.AxisX.IsLabelAutoFit = false;
            chartArea.AxisX.LabelStyle.Angle = -45;
            chartArea.AxisX.MajorGrid.LineColor = Color.LightGray;
            chartArea.AxisY.IsStartedFromZero = false;
            chartArea.AxisY.LabelStyle.IntervalOffset = 0D;
            chartArea.AxisY.LabelStyle.IntervalOffsetType = DateTimeIntervalType.Auto;
            chartArea.AxisY.LabelStyle.IntervalType = DateTimeIntervalType.Auto;
            chartArea.AxisY.MajorGrid.LineColor = Color.LightGray;
            chartArea.AxisY.ScrollBar.ButtonStyle = ScrollBarButtonStyles.SmallScroll;
            chartArea.BackColor = Color.FromArgb(235, 236, 239);

            chartArea.AxisX.ScrollBar.IsPositionedInside = true;
            chartArea.AxisX.ScrollBar.ButtonStyle = ScrollBarButtonStyles.SmallScroll;
            chartArea.AxisX.ScrollBar.Size = 15;
            chartArea.AxisX.ScaleView.Size = MaxAxisXSize < 20 ? MaxAxisXSize : 20;
            chartArea.AxisX.Minimum = 0;
            chartArea.AxisX.Maximum = MaxAxisXSize + 1;
            return chartArea;
        }

        private void CheckedChanged(object sender, EventArgs e)
        {
            LoadChartInfo();
        }

        object[] lastRow = null;
        int colindex = 0;

        private void view_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if(e.RowIndex == -1 && e.ColumnIndex != -1 && view.Rows.Count > 0)
            {
                int index = view.Rows.Count - 1;
                if(lastRow == null)
                {
                    colindex = e.ColumnIndex;
                    lastRow = ((DataTable)view.DataSource).Rows[index].ItemArray;
                }
                view.Rows.Remove(view.Rows[index]);
            }
        }

        private void view_Sorted(object sender, EventArgs e)
        {
            if(lastRow != null)
            {
                DataTable dt = ((DataTable)view.DataSource);
                DataView dv = dt.DefaultView;
                dv.Sort = dt.Columns[colindex].ColumnName;
                dt = dv.ToTable();
                dt.Rows.Add(lastRow);
                view.DataSource = dt;
                lastRow = null;
            }
        }

        private void View_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex!=-1 && e.ColumnIndex != -1)
            {
                int queryType = tabControl1.SelectedIndex;
                object code = view.Rows[e.RowIndex].Cells[0].Value;
                if(!string.IsNullOrEmpty(ToolHelper.GetValue(code)))
                {
                    Frm_QueryBorrowing queryBorrowing = new Frm_QueryBorrowing();
                    queryBorrowing.Update();
                    if(queryType == 0)//来源单位
                        queryBorrowing.cbo_PlanTypeList.SelectedValue = code;
                    else if(queryType == 1)//计划类别
                        queryBorrowing.cbo_SourceOrg.SelectedValue = code;
                    else if(queryType == 2)//地区
                        queryBorrowing.txt_Province.SelectedValue = code;
                    //queryBorrowing.LoadDataListByPage(null, null);
                    queryBorrowing.Show();
                }
            }
        }

        /// <summary>
        /// 地区页【类别下拉框事件】
        /// </summary>
        private void Cbo_SelectionChangeCommitted(object sender, EventArgs e)
        {
            AccordionControlElement element = cc_LeftMenu.SelectedElement;
            if(element != null)
                LocalElement_Click(element, null);
        }

        private void cbo_UnitList_SelectionChangeCommitted(object sender, EventArgs e)
        {
            string unitName = string.Empty;
            if (cbo_UnitList.SelectedIndex != 0)
                unitName = ToolHelper.GetValue(cbo_UnitList.SelectedItem);
            InitialUserList(unitName);
        }
    }
}
