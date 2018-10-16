using DevExpress.XtraBars.Navigation;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
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
            DataTable table = SqlHelper.ExecuteQuery(querySql);
            ac_LeftMenu.BeginUpdate();
            for(int i = 0; i < table.Rows.Count; i++)
            {
                AccordionControlElement element = new AccordionControlElement()
                {
                    Style = ElementStyle.Item,
                    Name = ToolHelper.GetValue(table.Rows[i]["dd_code"]),
                    Text = ToolHelper.GetValue(table.Rows[i]["dd_name"]),
                };
                element.Click += Item_Click;
                ac_LeftMenu.Elements.Add(element);
            }
            ac_LeftMenu.EndUpdate();

            querySql = "SELECT dd_code, dd_name FROM data_dictionary WHERE dd_pId=(SELECT dd_id FROM data_dictionary WHERE dd_code='dic_key_plan') ORDER BY dd_sort";
            table = SqlHelper.ExecuteQuery(querySql);
            bc_LeftMenu.BeginUpdate();
            for(int i = 0; i < table.Rows.Count; i++)
            {
                DataRow row = table.Rows[i];
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

            querySql = "SELECT dd_id, dd_name FROM data_dictionary WHERE dd_pId= " +
                "(SELECT dd_id FROM data_dictionary WHERE dd_code = 'dic_xzqy_province') " +
                "ORDER BY dd_sort";
            table = SqlHelper.ExecuteQuery(querySql);
            cc_LeftMenu.BeginUpdate();
            for(int i = 0; i < table.Rows.Count; i++)
            {
                AccordionControlElement element = new AccordionControlElement()
                {
                    Style = ElementStyle.Item,
                    Name = ToolHelper.GetValue(table.Rows[i]["dd_id"]),
                    Text = ToolHelper.GetValue(table.Rows[i]["dd_name"]),
                };
                element.Click += Element_Click;
                cc_LeftMenu.Elements.Add(element);
            }
            cc_LeftMenu.EndUpdate();
        }

        /// <summary>
        /// 按地区 - 点击事件
        /// </summary>
        private void Element_Click(object sender, EventArgs e)
        {
            string name = (sender as AccordionControlElement).Name;
            if("all_ltype".Equals(name))
                name = string.Empty;
            else
                name = $"AND M.dd_id='{name}'";
            LoadDataListByProvince(name, null, null);
        }

        /// <summary>
        /// 按地区加载数据
        /// </summary>
        /// <param name="provinceId">地区条件SQL(AND M.dd_id='')</param>
        /// <param name="minYear">最小年份</param>
        /// <param name="maxYear">最大年份</param>
        private void LoadDataListByProvince(string provinceId, object minYear, object maxYear)
        {
            string querySQL = "SELECT M.dd_id, M.dd_name, M.pCount, N.bCount, X.fCount, Y.lCount FROM(" +
                "SELECT dd_id, dd_name, COUNT(B.pi_id) pCount FROM data_dictionary LEFT JOIN (SELECT A.* FROM( " +
                "SELECT pi_id, pi_province FROM project_info WHERE pi_categor=2 UNION ALL " +
                "SELECT ti_id, ti_province FROM topic_info) A WHERE pi_province IS NOT NULL AND LEN(pi_province)<>0) B ON dd_id=pi_province " +
                "WHERE dd_pId=(SELECT dd_id FROM data_dictionary WHERE dd_code='dic_xzqy_province') GROUP BY dd_id, dd_name) M LEFT JOIN( " +
                "SELECT dd_id, dd_name, COUNT(pb.pb_id) bCount FROM data_dictionary LEFT JOIN (SELECT A.* FROM( " +
                "SELECT pi_id, pi_province FROM project_info WHERE pi_categor=2 UNION ALL " +
                "SELECT ti_id, ti_province FROM topic_info) A WHERE pi_province IS NOT NULL AND LEN(pi_province)<>0) B ON dd_id=pi_province " +
                "LEFT JOIN processing_box pb ON B.pi_id = pb.pb_obj_id WHERE dd_pId=(SELECT dd_id FROM data_dictionary WHERE dd_code='dic_xzqy_province') " +
                "GROUP BY dd_id, dd_name ) N ON M.dd_id=N.dd_id LEFT JOIN( " +
                "SELECT dd_id, dd_name, COUNT(pfl.pfl_id) fCount FROM data_dictionary LEFT JOIN (SELECT A.* FROM( " +
                "SELECT pi_id, pi_province FROM project_info WHERE pi_categor=2 UNION ALL " +
                "SELECT ti_id, ti_province FROM topic_info) A WHERE pi_province IS NOT NULL AND LEN(pi_province)<>0) B ON dd_id=pi_province " +
                "LEFT JOIN processing_file_list pfl ON B.pi_id = pfl.pfl_obj_id WHERE dd_pId=(SELECT dd_id FROM data_dictionary WHERE dd_code='dic_xzqy_province') " +
                "GROUP BY dd_id, dd_name) X ON N.dd_id=X.dd_id LEFT JOIN( SELECT dd_id, dd_name, CASE COUNT(pfo.pfo_id) WHEN 0 THEN 0 ELSE 1 END lCount FROM data_dictionary LEFT JOIN (SELECT A.* FROM( " +
                "SELECT pi_id, pi_province FROM project_info WHERE pi_categor=2 UNION ALL SELECT ti_id, ti_province FROM topic_info) A WHERE pi_province IS NOT NULL AND LEN(pi_province)<>0) B ON dd_id=pi_province " +
                "LEFT JOIN processing_file_lost pfo ON B.pi_id = pfo.pfo_obj_id WHERE dd_pId=(SELECT dd_id FROM data_dictionary WHERE dd_code='dic_xzqy_province') GROUP BY dd_id, dd_name) Y ON X.dd_id=Y.dd_id " +
               $"WHERE M.pCount>0 {provinceId} ";
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
            DataTable table = SqlHelper.ExecuteQuery($"SELECT ul_id, real_name FROM user_list");
            DataRow row = table.NewRow();
            row[0] = "all"; row[1] = "全部用户";
            table.Rows.InsertAt(row, 0);

            cbo_UserList.DataSource = table;
            cbo_UserList.ValueMember = "ul_id";
            cbo_UserList.DisplayMember = "real_name";
            tabPane2.SelectedPageIndex = 0;
            tabPane1.SelectedPage = tabNavigationPage1;
            panel3.Location = new Point(1066, 3);
            chart1.Width = chart2.Width = chart3.Width = datachart.Width;
            tabPane3.SelectedPageIndex = 0;
            chart4.Width = chart5.Width = 1051;
            panel1.Left = (datachart.Width - panel1.Width) / 2;
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
            tabPane3.SelectedPageIndex = 0;
            object userId = cbo_UserList.SelectedValue;
            bool allUser = "all".Equals(userId);
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
                    new DataColumn((rdo_JG.Checked?"被":string.Empty) + "返工数", typeof(int)),
                    new DataColumn("页数", typeof(int)),
                });
                //加工人员工作量统计
                if(rdo_JG.Checked)
                {
                    string userConditon = !allUser ? $" AND pi_worker_id='{userId}'" : string.Empty;
                    string queryCondition = string.Empty;
                    if(!flag)//全部时间
                    {
                        if(startDate.Date == endDate.Date)
                            queryCondition = $"AND pi_worker_date =  CONVERT(DATE, '{startDate}')";
                        else
                            queryCondition = $"AND pi_worker_date >=  CONVERT(DATE, '{startDate}') AND pi_worker_date <=  CONVERT(DATE, '{endDate}')";
                    }
                    string querySQL = "SELECT pi_worker_date, COUNT(pi_id) FROM(" +
                        "SELECT pi_id, pi_worker_date, pi_worker_id FROM project_info WHERE pi_categor = 2 UNION ALL " +
                        "SELECT ti_id, ti_worker_date, ti_worker_id FROM topic_info WHERE ti_categor = -3) AS TB1 " +
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
                            object bcount = GetBackAmount(date, userCode, 1);
                            object pgcount = GetPageAmount(date, userCode, 1);
                            table.Rows.Add(date, pcount, tcount, fcount, bcount, pgcount);
                        }
                    }
                    //如果当天无项目，则统计是否有课题
                    if(list.Count == 0)
                    {
                        if(!flag)
                        {
                            if(startDate.Date == endDate.Date)
                                queryCondition = $"AND ti_worker_date =  CONVERT(DATE, '{startDate}')";
                            else
                                queryCondition = $"AND ti_worker_date >=  CONVERT(DATE, '{startDate}') AND ti_worker_date <=  CONVERT(DATE, '{endDate}')";
                        }
                        querySQL = "SELECT ti_worker_date, COUNT(ti_id) FROM topic_info " +
                             $"LEFT JOIN project_info ON ti_obj_id = pi_id WHERE ti_categor = 3 AND ti_worker_id='{userId}' " +
                             $"AND ti_worker_date <> pi_worker_date {queryCondition} " +
                             "GROUP BY ti_worker_date;";
                        List<object[]> list2 = SqlHelper.ExecuteColumnsQuery(querySQL, 2);
                        for(int i = 0; i < list2.Count; i++)
                        {
                            object date = GetDateValue(list2[i][0], "yyyy-MM-dd");
                            object pcount = 0;
                            object tcount = list2[i][1];
                            object fcount = GetFileAmount(date, userId, 1);
                            object bcount = GetBackAmount(date, userId, 1);
                            object pgcount = GetPageAmount(date, userId, 1);
                            table.Rows.Add(date, pcount, tcount, fcount, bcount, pgcount);
                        }

                        //如果没有课题/子课题，则单独统计文件数
                        if(list2.Count == 0)
                        {
                            if(!flag)
                            {
                                if(startDate.Date == endDate.Date)
                                    queryCondition = $"AND pfl_worker_date =  CONVERT(DATE, '{startDate}')";
                                else
                                    queryCondition = $"AND pfl_worker_date >=  CONVERT(DATE, '{startDate}') AND pfl_worker_date <=  CONVERT(DATE, '{endDate}')";
                            }
                            //单独统计文件加工工作量
                            querySQL = "SELECT pfl_worker_date, COUNT(pfl_id) FROM processing_file_list " +
                                $"WHERE pfl_worker_id = '{userId}' {queryCondition} AND pfl_worker_id NOT IN( " +
                                $"SELECT pi_worker_id FROM project_info WHERE pi_worker_id = '{userId}' AND pi_worker_date = pfl_worker_date UNION ALL " +
                                $"SELECT ti_worker_id FROM topic_info WHERE ti_worker_id = '{userId}' AND ti_worker_date = pfl_worker_date) " +
                                $"GROUP BY pfl_worker_date; ";
                            List<object[]> list3 = SqlHelper.ExecuteColumnsQuery(querySQL, 2);
                            for(int i = 0; i < list3.Count; i++)
                            {
                                object date = GetDateValue(list3[i][0], "yyyy-MM-dd");
                                object pcount = 0;
                                object tcount = 0;
                                object fcount = Convert.ToInt32(list3[i][1]);
                                object bcount = 0;
                                object pgcount = GetFilePageByFid(userId);
                                table.Rows.Add(date, pcount, tcount, fcount, bcount, pgcount);
                            }
                        }
                    }
                    countView.DataSource = table;
                }
                //质检人员工作量统计
                else
                {
                    string userConditon = !allUser ? $"AND pi_checker_id='{userId}'" : string.Empty;
                    string queryCondition = string.Empty;
                    if(!flag)//全部时间
                    {
                        if(startDate.Date == endDate.Date)
                            queryCondition = $"AND pi_checker_date =  CONVERT(DATE, '{startDate}')";
                        else
                            queryCondition = $"AND pi_checker_date >=  CONVERT(DATE, '{startDate}') AND pi_checker_date <=  CONVERT(DATE, '{endDate}')";
                    }
                    string querySQL = "SELECT pi_checker_date, COUNT(pi_id) FROM(" +
                        $"SELECT pi_id, pi_checker_id, pi_checker_date FROM project_info WHERE pi_categor = 2 UNION ALL " +
                        $"SELECT ti_id, ti_checker_id, ti_checker_date FROM topic_info WHERE ti_categor = -3) AS TB1 WHERE pi_checker_id IS NOT NULL {queryCondition} {userConditon} " +
                        $"GROUP BY pi_checker_date";
                    List<object[]> list = SqlHelper.ExecuteColumnsQuery(querySQL, 2);
                    object userCode = "all".Equals(userId) ? null : userId;
                    for(int i = 0; i < list.Count; i++)
                    {
                        object date = GetDateValue(list[i][0], "yyyy-MM-dd");
                        object pcount = list[i][1];
                        object tcount = GetTopicAmount(date, userCode, 2);
                        object fcount = GetFileAmount(date, userCode, 2);
                        object bcount = GetBackAmount(date, userCode, 2);
                        object pgcount = GetPageAmount(date, userCode, 2);
                        table.Rows.Add(date, pcount, tcount, fcount, bcount, pgcount);
                    }

                    countView.DataSource = table;
                }
            }
            //按人员统计
            else
            {
                table.Columns.AddRange(new DataColumn[]
                {
                    new DataColumn("用户名"),
                    new DataColumn("项目/课题数", typeof(int)),
                    new DataColumn("课题/子课题数", typeof(int)),
                    new DataColumn("文件数", typeof(int)),
                    new DataColumn("返工数", typeof(int)),
                    new DataColumn("页数", typeof(int)),
                });
                //加工人员工作量统计
                if(rdo_JG.Checked)
                {
                    string userConditon = !allUser ? $" AND pi_worker_id='{userId}'" : string.Empty;
                    string dateCondition = string.Empty;
                    string _startDate = dtp_StartDate.Value.ToString("yyyy-MM-dd");
                    string _endDate = dtp_EndDate.Value.ToString("yyyy-MM-dd");
                    if(!chk_AllDate.Checked)//全部时间
                    {
                        if(startDate.Equals(endDate))
                            dateCondition = $"AND pi_worker_date = '{_startDate}'";
                        else
                            dateCondition = $"AND pi_worker_date >= '{_startDate}' AND pi_worker_date <= '{_endDate}'";
                    }
                    string querySQL = "SELECT pi_worker_id, COUNT(pi_id) FROM(" +
                        "SELECT pi_id, pi_worker_date, pi_worker_id FROM project_info WHERE pi_categor = 2 UNION ALL " +
                       $"SELECT ti_id, ti_worker_date, ti_worker_id FROM topic_info WHERE ti_categor = -3) AS TB1 WHERE pi_worker_id IS NOT NULL {dateCondition} {userConditon}" +
                        "GROUP BY pi_worker_id";
                    List<object[]> list = SqlHelper.ExecuteColumnsQuery(querySQL, 2);
                    object userCode = "all".Equals(userId) ? null : userId;
                    for(int i = 0; i < list.Count; i++)
                    {
                        object user = UserHelper.GetUserNameById(list[i][0]);
                        object pcount = list[i][1];
                        object tcount = GetTopicAmount(null, list[i][0], 3);
                        object fcount = GetFileAmount(null, list[i][0], 3);
                        object bcount = GetBackAmount(null, list[i][0], 3);
                        object pgcount = GetPageAmount(null, list[i][0], 3);
                        table.Rows.Add(user, pcount, tcount, fcount, bcount, pgcount);
                    }
                    countView.DataSource = table;
                }
                //质检人员工作量统计
                else
                {
                    string userConditon = !allUser ? $"AND pi_checker_id='{userId}'" : string.Empty;
                    string dateCondition = string.Empty;
                    string _startDate = dtp_StartDate.Value.ToString("yyyy-MM-dd");
                    string _endDate = dtp_EndDate.Value.ToString("yyyy-MM-dd");
                    if(!chk_AllDate.Checked)//全部时间
                    {
                        if(startDate.Equals(endDate))
                            dateCondition = $"AND pi_checker_date = '{_startDate}'";
                        else
                            dateCondition = $"AND pi_checker_date >= '{_startDate}' AND pi_checker_date <= '{_endDate}'";
                    }
                    string querySQL = "SELECT pi_checker_id, COUNT(pi_id) FROM(" +
                        $"SELECT pi_id, pi_checker_id, pi_checker_date FROM project_info WHERE pi_categor = 2 UNION ALL " +
                        $"SELECT ti_id, ti_checker_id, ti_checker_date FROM topic_info WHERE ti_categor = -3) AS TB1 WHERE pi_checker_id IS NOT NULL {dateCondition} {userConditon} " +
                        $"GROUP BY pi_checker_id";
                    List<object[]> list = SqlHelper.ExecuteColumnsQuery(querySQL, 2);
                    for(int i = 0; i < list.Count; i++)
                    {
                        object user = UserHelper.GetUserNameById(list[i][0]);
                        object pcount = list[i][1];
                        object tcount = GetTopicAmount(null, list[i][0], 4);
                        object fcount = GetFileAmount(null, list[i][0], 4);
                        object bcount = GetBackAmount(null, list[i][0], 4);
                        object pgcount = GetPageAmount(null, list[i][0], 4);
                        table.Rows.Add(user, pcount, tcount, fcount, bcount, pgcount);
                    }

                    countView.DataSource = table;
                }
            }
        }

        private int GetFilePageByFid(object userId)
        {
            string _querySQL = $"SELECT pfl_id FROM processing_file_list LEFT JOIN project_info ON pi_id = pfl_obj_id WHERE pfl_worker_id = '{userId}' AND pi_worker_date <> pfl_worker_date";
            string querySQL = $"SELECT SUM(pfl_pages) FROM processing_file_list WHERE pfl_id IN ({_querySQL})";
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
            string value = (sender as AccordionControlElement).Name;
            if("ace_all".Equals(value))
                value = string.Empty;
            else
                value = $"AND A.pi_orga_id = '{value}'";

            SetTableByOrga(value, null, null);
        }

        /// <summary>
        /// 加载计划类别表
        /// </summary>
        /// <param name="value">来源单位编码</param>
        /// <param name="minYear">最小年份</param>
        /// <param name="maxYear">最大年份</param>
        private void SetTableByOrga(string value, string minYear, string maxYear)
        {
            string minYearCondition = string.Empty;
            string maxYearCondition = string.Empty;
            if(!string.IsNullOrEmpty(minYear))
                minYearCondition = $"AND ISNULL(pi_year, TRY_CAST(SUBSTRING(pi_start_datetime, 1, 4) AS INT)) >= {minYear}";
            if(!string.IsNullOrEmpty(maxYear))
                maxYearCondition = $"AND ISNULL(pi_year, TRY_CAST(SUBSTRING(pi_start_datetime, 1, 4) AS INT)) <= {maxYear}";
            string querySQL = "SELECT p.F_ID,F_Title, pCount, bCount, fCount, fb.fbCount FROM( SELECT F_Title, F_ID, pCount " +
                "FROM(SELECT p.F_Title, p.F_ID, COUNT(A.pi_id) AS pCount FROM T_Plan AS p LEFT OUTER JOIN (" +
                "SELECT pi_id, pi_source_id, pi_orga_id, pi_year, pi_start_datetime FROM project_info WHERE pi_categor = 2 UNION ALL " +
               $"SELECT ti_id, ti_source_id, ti_orga_id, ti_year, ti_start_datetime FROM topic_info) AS A ON A.pi_source_id = p.F_ID " +
               $"AND A.pi_orga_id IS NOT NULL AND A.pi_orga_id<>'' {value} {minYearCondition} {maxYearCondition} " +
                "GROUP BY F_Title, p.F_ID) AS B WHERE pCount > 0) p " +
                "LEFT JOIN( SELECT F_ID, bCount FROM(SELECT p.F_ID, COUNT(pb.pb_id) AS bCount FROM T_Plan AS p LEFT OUTER JOIN (" +
                "SELECT pi_id, pi_source_id, pi_orga_id, pi_year, pi_start_datetime FROM project_info WHERE(pi_categor = 2) UNION ALL " +
               $"SELECT ti_id, ti_source_id, ti_orga_id, ti_year, ti_start_datetime FROM topic_info) AS A ON A.pi_source_id = p.F_ID " +
               $"AND A.pi_orga_id IS NOT NULL AND A.pi_orga_id<>'' {value} {minYearCondition} {maxYearCondition} " +
                "LEFT JOIN processing_box pb ON pb.pb_obj_id = A.pi_id GROUP BY p.F_ID) AS B ) b ON p.F_ID = b.F_ID LEFT JOIN( " +
                "SELECT F_ID, fCount FROM (SELECT p.F_ID, COUNT(pb.pfl_id) AS fCount FROM T_Plan AS p LEFT OUTER JOIN (" +
                "SELECT pi_id, pi_source_id, pi_orga_id, pi_year, pi_start_datetime FROM project_info WHERE pi_categor = 2 UNION ALL " +
               $"SELECT ti_id, ti_source_id, ti_orga_id, ti_year, ti_start_datetime FROM topic_info) AS A ON A.pi_source_id = p.F_ID " +
               $"AND A.pi_orga_id IS NOT NULL AND A.pi_orga_id<>'' {value} {minYearCondition} {maxYearCondition} " +
                "LEFT JOIN processing_file_list pb ON pb.pfl_obj_id = A.pi_id GROUP BY p.F_ID) AS B ) f ON f.F_ID = b.F_ID " +
                "LEFT OUTER JOIN (SELECT F_ID, fbCount FROM(SELECT p.F_ID, COUNT(A.pi_id) AS fbCount FROM T_Plan AS p LEFT OUTER JOIN (" +
                "SELECT * FROM(SELECT pi_id, pi_source_id, pi_orga_id, pi_year, pi_start_datetime, CASE COUNT(pfo.pfo_id) WHEN 0 THEN 0 ELSE 1 END oCount FROM(" +
                "SELECT   pi_id, pi_source_id, pi_orga_id, pi_year, pi_start_datetime FROM project_info WHERE pi_categor = 2 UNION ALL " +
                "SELECT   ti_id, ti_source_id, ti_orga_id, ti_year, ti_start_datetime FROM topic_info) A LEFT JOIN processing_file_lost pfo ON A.pi_id = pfo.pfo_obj_id AND pfo.pfo_ismust = 1 " +
               $"GROUP BY pi_id, pi_source_id, pi_orga_id, pi_year, pi_start_datetime) B WHERE B.oCount>0) AS A ON A.pi_source_id = p.F_ID " +
               $"AND A.pi_orga_id IS NOT NULL AND A.pi_orga_id<>'' {value} {minYearCondition} {maxYearCondition} " +
                "GROUP BY p.F_ID) AS B) AS fb ON fb.F_ID = b.F_ID";
            DataTable table = SqlHelper.ExecuteQuery(querySQL);
            table = HandleZX(table);
            DataGridViewStyleHelper.ResetDataGridView(view, true);
            DataTable tableEntity = new DataTable();
            tableEntity.Columns.AddRange(new DataColumn[]
            {
                new DataColumn("计划类别编号"),
                new DataColumn("计划类别名称"),
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
                int _fbcount = ToolHelper.GetIntValue(row["fbCount"], 0);
                tableEntity.Rows.Add(row["F_ID"], row["F_Title"], _pcount, _bcount, _fcount, _fbcount);

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

        /// <summary>
        /// 汇总各专项为重大专项
        /// </summary>
        private DataTable HandleZX(DataTable table)
        {
            List<DataRow> rowList = new List<DataRow>();
            int pCount = 0, bCount = 0, fCount = 0, fbCount = 0;
            for(int i = 0; i < table.Rows.Count; i++)
            {
                DataRow row = table.Rows[i];
                object fid = row["F_ID"];
                if(ToolHelper.GetValue(fid).StartsWith("ZX"))
                {
                    pCount += ToolHelper.GetIntValue(row["pCount"], 0);
                    bCount += ToolHelper.GetIntValue(row["bCount"], 0);
                    fCount += ToolHelper.GetIntValue(row["fCount"], 0);
                    fbCount += ToolHelper.GetIntValue(row["fbCount"], 0);
                    rowList.Add(row);
                }
            }
            if(pCount > 0)
            {
                foreach(DataRow item in rowList)
                    table.Rows.Remove(item);
                object[] zxList = new object[] { "ZX", "国家重大专项", pCount, bCount, fCount, fbCount };
                table.Rows.Add(zxList);
            }
            return table;
        }

        /// <summary>
        /// 计划类别点击事件
        /// </summary>
        private void Bc_Element_Click(object sender, EventArgs e)
        {
            string value = (sender as AccordionControlElement).Name;
            if("all_ptype".Equals(value))
                value = string.Empty;
            else if("ZX".Equals(value))
                value = $"AND A.pi_source_id LIKE 'ZX%'";
            else
                value = $"AND A.pi_source_id = '{value}'";
            SetTableBySource(value, null, null);
        }

        private void SetTableBySource(string value, string minYear, string maxYear)
        {
            string minYearCondition = string.Empty;
            string maxYearCondition = string.Empty;
            if(!string.IsNullOrEmpty(minYear))
                minYearCondition = $"AND ISNULL(pi_year, TRY_CAST(SUBSTRING(pi_start_datetime, 1, 4) AS INT)) >= {minYear}";
            if(!string.IsNullOrEmpty(maxYear))
                maxYearCondition = $"AND ISNULL(pi_year, TRY_CAST(SUBSTRING(pi_start_datetime, 1, 4) AS INT)) <= {maxYear}";
            string querySQL = "SELECT p.F_ID,F_Title, pCount, bCount, fCount, fbCount FROM( " +
                "SELECT F_Title, F_ID, pCount FROM(SELECT p.F_Title, p.F_ID, COUNT(A.pi_id) AS pCount FROM T_SourceOrg AS p LEFT OUTER JOIN (" +
                "SELECT pi_id, pi_source_id, pi_orga_id, pi_year, pi_start_datetime FROM project_info WHERE(pi_categor = 2) UNION ALL " +
               $"SELECT ti_id, ti_source_id, ti_orga_id, ti_year, ti_start_datetime FROM topic_info) AS A ON A.pi_orga_id = p.F_ID " +
               $"AND A.pi_source_id IS NOT NULL AND A.pi_source_id<>'' {value} {minYearCondition} {maxYearCondition} " +
                "GROUP BY F_Title, p.F_ID) AS B WHERE pCount>0) p LEFT JOIN(SELECT   F_ID, bCount FROM(" +
                "SELECT p.F_ID, COUNT(pb.pb_id) AS bCount FROM T_SourceOrg AS p LEFT OUTER JOIN(" +
                "SELECT pi_id, pi_source_id, pi_orga_id, pi_year, pi_start_datetime FROM project_info WHERE pi_categor = 2 UNION ALL " +
               $"SELECT ti_id, ti_source_id, ti_orga_id, ti_year, ti_start_datetime FROM topic_info) AS A ON A.pi_orga_id = p.F_ID " +
               $"AND A.pi_source_id IS NOT NULL AND A.pi_source_id<>'' {value} {minYearCondition} {maxYearCondition} " +
                "LEFT JOIN processing_box pb ON pb.pb_obj_id = A.pi_id GROUP BY p.F_ID) AS B) b ON p.F_ID = b.F_ID LEFT JOIN( " +
                "SELECT F_ID, fCount FROM (SELECT p.F_ID, COUNT(pb.pfl_id) AS fCount FROM T_SourceOrg AS p LEFT OUTER JOIN (" +
                "SELECT pi_id, pi_source_id, pi_orga_id, pi_year, pi_start_datetime FROM project_info WHERE pi_categor = 2 UNION ALL " +
               $"SELECT ti_id, ti_source_id, ti_orga_id, ti_year, ti_start_datetime FROM topic_info) AS A ON A.pi_orga_id = p.F_ID " +
               $"AND A.pi_source_id IS NOT NULL AND A.pi_source_id<>'' {value} {minYearCondition} {maxYearCondition} " +
                "LEFT JOIN processing_file_list pb ON pb.pfl_obj_id = A.pi_id GROUP BY p.F_ID) AS B ) f ON f.F_ID = b.F_ID " +
                "LEFT OUTER JOIN (SELECT F_ID, fbCount FROM (SELECT p.F_ID, COUNT(A.pi_id) AS fbCount FROM T_SourceOrg AS p LEFT OUTER JOIN (" +
                "SELECT * FROM(SELECT pi_id, pi_source_id, pi_orga_id, pi_year, pi_start_datetime, CASE COUNT(pfo.pfo_id) WHEN 0 THEN 0 ELSE 1 END oCount FROM(" +
                "SELECT pi_id, pi_source_id, pi_orga_id, pi_year, pi_start_datetime FROM project_info WHERE pi_categor = 2 UNION ALL " +
                "SELECT ti_id, ti_source_id, ti_orga_id, ti_year, ti_start_datetime FROM topic_info) A LEFT JOIN processing_file_lost pfo ON A.pi_id = pfo.pfo_obj_id AND pfo.pfo_ismust = 1 " +
               $"GROUP BY pi_id, pi_source_id, pi_orga_id, pi_year, pi_start_datetime) B WHERE B.oCount>0) A ON A.pi_orga_id = p.F_ID " +
               $"AND A.pi_source_id IS NOT NULL AND A.pi_source_id<>'' {value} {minYearCondition} {maxYearCondition} " +
                "GROUP BY p.F_ID) AS B) AS fb ON fb.F_ID = b.F_ID";
            DataTable table = SqlHelper.ExecuteQuery(querySQL);
            int totalPcount = 0, totalFcount = 0, totalBcount = 0, totalFBcount = 0;
            DataGridViewStyleHelper.ResetDataGridView(view, true);
            DataTable tableEntity = new DataTable();
            tableEntity.Columns.AddRange(new DataColumn[]
            {
                    new DataColumn("来源单位编号"),
                    new DataColumn("来源单位名称"),
                    new DataColumn("项目/课题数", typeof(int)),
                    new DataColumn("盒数", typeof(int)),
                    new DataColumn("文件数", typeof(int)),
                    new DataColumn("必备文件缺失项目数", typeof(int)),
            });
            DataRowCollection rowCollection = table.Rows;
            for(int i = 0; i < rowCollection.Count; i++)
            {
                DataRow row = rowCollection[i];
                int _pcount = ToolHelper.GetIntValue(row["pCount"], 0);
                int _bcount = ToolHelper.GetIntValue(row["bCount"], 0);
                int _fcount = ToolHelper.GetIntValue(row["fCount"], 0);
                int _fbcount = ToolHelper.GetIntValue(row["fbCount"], 0);
                tableEntity.Rows.Add(row["F_ID"], row["F_Title"], _pcount, _bcount, _fcount, _fbcount);

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

        private void TabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(tabControl1.SelectedIndex == 0)
            {
                ac_LeftMenu.SelectedElement = ace_all;
                Item_Click(ace_all, null);
            }
            else if(tabControl1.SelectedIndex == 1)
            {
                bc_LeftMenu.SelectedElement = all_ptype;
                Bc_Element_Click(all_ptype, null);
            }
            else if(tabControl1.SelectedIndex == 2)
            {
                cc_LeftMenu.SelectedElement = all_ltype;
                Element_Click(all_ltype, null);
            }
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
            chart1.Series.Clear();
            chart2.Series.Clear();
            chart3.Series.Clear();
            if(tabPane2.SelectedPageIndex == 1)
            {
                tabPane2.Update();
                LoadImageInfo();
            }
        }

        /// <summary>
        /// 加载图形实例
        /// </summary>
        private void LoadImageInfo()
        {
            chart1.Series.Clear();
            chart2.Series.Clear();
            chart3.Series.Clear();
            int tpye1 = tabControl1.SelectedIndex;

            //项目/课题2，盒3，文件4
            int typeIndex = rdo_ProCount.Checked ? 2 : rdo_BoxCount.Checked ? 3 : rdo_FileCount.Checked ? 4 : 5;
            Series amount = new Series()
            {
                IsValueShownAsLabel = true,
                Palette = ChartColorPalette.Excel,
                ChartType = SeriesChartType.Pie
            };
            amount["PieLabelStyle"] = "Outside";//将文字移到外侧 
            amount["PieLineColor"] = "Black";//绘制黑色的连线。 
            amount.Label = "#PERCENT{P2}";
            amount.LegendText = "#VALX";
            Series amount2 = new Series()
            {
                IsValueShownAsLabel = true,
                ShadowOffset = 5,
                Palette = ChartColorPalette.Excel,
                ChartType = SeriesChartType.Column
            };
            double maxNum = 0;
            for(int i = 0; i < view.RowCount - 1; i++)
            {
                int value = ToolHelper.GetIntValue(view.Rows[i].Cells[typeIndex].Value, 0);
                maxNum = value > maxNum ? value : maxNum;
                amount.Points.AddXY(view.Rows[i].Cells[1].Value, value);
                amount2.Points.AddXY(view.Rows[i].Cells[1].Value, value);
            }

            chart1.Series.Add(amount);
            chart2.Series.Add(amount2);
            chart1.ChartAreas[0].AxisY.Maximum = maxNum;
            chart2.ChartAreas[0].AxisY.Maximum = maxNum;
            //来源单位0
            if(tpye1 == 0)
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
                            chart3.Series.Add(series);
                        }
                    }
                }
            }
            //计划类别1
            else
            {

                //年度统计
                if(typeIndex == 2)
                {
                    string orgName = bc_LeftMenu.SelectedElement == null ? "all_ptype" : bc_LeftMenu.SelectedElement.Name;
                    string _minYear = txt_QuerySyear.Text, _maxYear = txt_QueryEyear.Text;
                    if(string.IsNullOrEmpty(_minYear) && string.IsNullOrEmpty(_maxYear))
                    {
                        string querySQL_Year = "SELECT MIN(myear) maxyear, MAX(myear) minyear FROM( " +
                        "SELECT ISNULL(pi_year, TRY_CAST(SUBSTRING(pi_start_datetime, 1, 4) AS INT)) AS myear, pi_source_id FROM " +
                        "(SELECT pi_year, pi_start_datetime, pi_source_id FROM project_info WHERE pi_categor = 2 " +
                        "UNION ALL SELECT ti_year, ti_start_datetime, ti_source_id FROM topic_info WHERE ti_categor = -3 )a)b " +
                        "WHERE myear IS NOT NULL AND myear> '0' AND myear<= YEAR(SYSDATETIME()) ";
                        if(!"all_ptype".Equals(orgName))
                            querySQL_Year += $"AND pi_source_id='{orgName}' ";
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
                            string sorName = "all_ptype".Equals(orgName) ? string.Empty : $"AND pi_source_id='{orgName}'";
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
                                int value = 0;
                                pair.TryGetValue(i, out value);
                                series.Points.AddXY(i, value);
                            }
                            chart3.Series.Add(series);
                        }
                    }
                }
            }
        }

        private void chart1_GetToolTipText(object sender, ToolTipEventArgs e)
        {
            string name = rdo_ProCount.Checked ? rdo_ProCount.Text : rdo_BoxCount.Checked ? rdo_BoxCount.Text : rdo_FileCount.Checked ? rdo_FileCount.Text : rdo_FileBCount.Text;
            Chart chart = sender as Chart;
            string chartName = chart.Name;
            //判断鼠标是否移动到数据标记点，是则显示提示信息
            if(e.HitTestResult.ChartElementType == ChartElementType.DataPoint)
            {
                int i = e.HitTestResult.PointIndex;
                DataPoint dp = e.HitTestResult.Series.Points[i];
                lbl_TipName.Text = dp.AxisLabel;
                if("chart1".Equals(chartName))
                {
                    object proAmount = chart.Series[0].Points[i].YValues[0];
                    //分别显示x轴和y轴的数值                   
                    lbl_TipAmount.Text = $"{name}：{proAmount}";
                }
                else if("chart2".Equals(chartName))
                {
                    lbl_TipAmount.Text = $"{name}：{dp.YValues[0]}";
                }
                else if("chart3".Equals(chartName))
                {
                    lbl_TipName.Text = $"{dp.XValue} {e.HitTestResult.Series.Name}";
                    lbl_TipAmount.Text = $"{name}：{dp.YValues[0]}";
                }
                //显示提示信息
                tip_Panel.Visible = true;
                Point point = datachart.PointToClient(MousePosition);
                tip_Panel.Location = new Point(point.X + 30, point.Y);
            }

            //鼠标离开数据标记点，则隐藏提示信息
            else
            {
                tip_Panel.Visible = false;
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
            int tpye1 = tabControl1.SelectedIndex;
            if(tpye1 == 0)
            {
                string value = ac_LeftMenu.SelectedElement.Name;
                if("ace_all".Equals(value))
                    value = string.Empty;
                else
                    value = $"AND A.pi_orga_id = '{value}'";

                SetTableByOrga(value, minYear, maxYear);
            }
            else if(tpye1 == 1)
            {
                string value = bc_LeftMenu.SelectedElement.Name;
                if("all_ptype".Equals(value))
                    value = string.Empty;
                else
                    value = $"AND A.pi_source_id = '{value}'";

                SetTableBySource(value, minYear, maxYear);
            }
            tabPane2.SelectedPageIndex = 0;

        }

        private void ProCount_MouseClick(object sender, MouseEventArgs e)
        {
            LoadImageInfo();
        }

        private void tabPane3_SelectedPageChanged(object sender, SelectedPageChangedEventArgs e)
        {
            LoadChartInfo();
        }

        private void LoadChartInfo()
        {
            if(tabPane3.SelectedPageIndex == 0) return;
            chart4.Series.Clear(); chart5.Series.Clear();
            int a = rdo1.Checked ? 1 : rdo2.Checked ? 2 : rdo3.Checked ? 3 : rdo4.Checked ? 4 : 5;
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
                chart4.ChartAreas[0].AxisX.LabelStyle.Angle = -45;
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
            for(int i = 0; i < countView.RowCount; i++)
            {
                int value = ToolHelper.GetIntValue(countView.Rows[i].Cells[a].Value, 0);
                if(value > 0)
                {
                    maxNum = value > maxNum ? value : maxNum;
                    amount.Points.AddXY(countView.Rows[i].Cells[0].Value, value);
                    amount1.Points.AddXY(countView.Rows[i].Cells[0].Value, value);
                }
            }

            chart4.Series.Add(amount);
            chart5.Series.Add(amount1);
            if(maxNum >= 0)
            {
                chart4.ChartAreas[0].AxisY.Maximum = maxNum;
                chart5.ChartAreas[0].AxisY.Maximum = maxNum;
            }
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
                Frm_QueryBorrowing queryBorrowing = GetFormHelper.GetQueryBorrow(null);
                queryBorrowing.Update();
                if(queryType == 0)//来源单位
                    queryBorrowing.cbo_PlanTypeList.SelectedValue = code;
                else if(queryType == 1)//计划类别
                    queryBorrowing.cbo_SourceOrg.SelectedValue = code;
                queryBorrowing.LoadDataListByPage(null, null);

                queryBorrowing.Show();
                queryBorrowing.Activate();
            }
        }
    }
}
