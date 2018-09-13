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
        Stopwatch stopwatch = new Stopwatch();
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
            string querySql = "SELECT * FROM T_SourceOrg ORDER BY F_ID";
            DataTable table = SqlHelper.ExecuteQuery(querySql);
            for(int i = 0; i < table.Rows.Count; i++)
            {
                AccordionControlElement element = new AccordionControlElement()
                {
                    Style = ElementStyle.Item,
                    Name = ToolHelper.GetValue(table.Rows[i]["F_ID"]),
                    Text = ToolHelper.GetValue(table.Rows[i]["F_Title"]),
                };
                element.Click += Item_Click;
                ac_LeftMenu.Elements.Add(element);
            }

            querySql = "SELECT * FROM T_Plan ORDER BY F_ID";
            table = SqlHelper.ExecuteQuery(querySql);
            table.Rows.Add("国家科技重大专项", "ZX");

            for(int i = 0; i < table.Rows.Count; i++)
            {
                DataRow row = table.Rows[i];
                AccordionControlElement element = new AccordionControlElement()
                {
                    Name = ToolHelper.GetValue(row["F_ID"]),
                    Text = ToolHelper.GetValue(row["F_Title"]),
                };
                bc_LeftMenu.Elements.Add(element);
                //国家科技重大专项 -- 特殊处理
                if("ZX".Equals(row["F_ID"]))
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
                }
                else
                {
                    element.Style = ElementStyle.Item;
                    element.Click += Bc_Element_Click;
                }
            }
        }

        private void Frm_Statistics_Load(object sender, EventArgs e)
        {
            DataTable table = SqlHelper.ExecuteQuery($"SELECT ul_id, real_name FROM user_list");
            DataRow row = table.NewRow();
            row[0] = "all";row[1] = "全部用户";
            table.Rows.InsertAt(row, 0);

            cbo_UserList.DataSource = table;
            cbo_UserList.ValueMember = "ul_id";
            cbo_UserList.DisplayMember = "real_name";

            tabPane1.SelectedPage = tabNavigationPage1;
            chart1.Series.Clear();
            chart2.Series.Clear();
            chart3.Series.Clear();
            ////////////////
            chart2.Width = chart3.Width = chart1.Width = datachart.Width;
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
            object userId = cbo_UserList.SelectedValue;
            bool allUser = "all".Equals(userId);
            DateTime startDate = dtp_StartDate.Value;
            DateTime endDate = dtp_EndDate.Value;
            DataTable table = new DataTable();
            table.Columns.AddRange(new DataColumn[]
            {
                new DataColumn("date", typeof(DateTime)),
                new DataColumn("pcount"),
                new DataColumn("tcount"),
                new DataColumn("fcount"),
                new DataColumn("bcount"),
                new DataColumn("pgcount"),
            });
            bool flag = chk_AllDate.Checked;
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
                    "SELECT pi_id, pi_worker_date, pi_worker_id FROM project_info WHERE pi_categor = 2 " +
                    $"UNION ALL SELECT ti_id, ti_worker_date, ti_worker_id FROM topic_info WHERE ti_categor = -3) AS TB1 WHERE 1=1 {queryCondition} {userConditon}" +
                    "GROUP BY pi_worker_date";
                List<object[]> list = SqlHelper.ExecuteColumnsQuery(querySQL, 2);
                object userCode = "all".Equals(userId) ? null : userId;
                for(int i = 0; i < list.Count; i++)
                {
                    object date = GetDateValue(list[i][0], "yyyy-MM-dd");
                    object pcount = list[i][1];
                    object tcount = GetTopicAmount(date, userCode, 1);
                    object fcount = GetFileAmount(date, userCode, 1);
                    object bcount = GetBackAmount(date, userCode, 1);
                    object pgcount = GetPageAmount(date, userCode, 1);
                    table.Rows.Add(date, pcount, tcount, fcount, bcount, pgcount);
                }

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

                if(!flag)
                {
                    if(startDate.Date == endDate.Date)
                        queryCondition = $"AND pfl_worker_date =  CONVERT(DATE, '{startDate}')";
                    else
                        queryCondition = $"AND pfl_worker_date >=  CONVERT(DATE, '{startDate}') AND pfl_worker_date <=  CONVERT(DATE, '{endDate}')";
                }
                //单独统计文件加工工作量
                querySQL = "SELECT pfl_worker_date, COUNT(pfl_id) FROM processing_file_list " +
                    $"WHERE pfl_worker_id = '{userId}' " +
                    $"{queryCondition} AND pfl_worker_id NOT IN( " +
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
                    object pgcount = GetFilePageByFid($"SELECT pfl_id FROM processing_file_list LEFT JOIN project_info ON pi_id = pfl_obj_id " +
                        $"WHERE pfl_worker_id = '{userId}' AND pi_worker_date <> pfl_worker_date");
                    table.Rows.Add(date, pcount, tcount, fcount, bcount, pgcount);
                }
                LoadViewList(table);
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
                    $"SELECT ti_id, ti_checker_id, ti_checker_date FROM topic_info WHERE ti_categor = -3) AS TB1 WHERE 1=1 {queryCondition} {userConditon} " +
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

                LoadViewList(table);
            }
        }

        private int GetFilePageByFid(string querySql)
        {
            string querySQL = $"SELECT SUM(pfl_pages) FROM processing_file_list WHERE pfl_id IN ({querySql})";
            return SqlHelper.ExecuteCountQuery(querySQL);
        }

        /// <summary>
        /// 获取指定日期下指定用户的总加工页数
        /// </summary>
        private int GetPageAmount(object date, object userId, int type)
        {
            string key = type == 1 ? "worker" : "checker";
            string userCondition = userId == null ? string.Empty : $"AND pfl_{ key}_id='{userId}'";
            string querySQL = $"SELECT SUM(pfl_pages) FROM processing_file_list WHERE pfl_{key}_date='{date}' {userCondition} ";
            return SqlHelper.ExecuteCountQuery(querySQL);
        }

        /// <summary>
        /// 获取指定日期下指定用户的返工数
        /// </summary>
        private int GetBackAmount(object date, object userId, int type)
        {
            if(type == 1)
            {
                string userCondition = userId == null ? string.Empty : $"AND wm_user='{userId}'";
                string querySQL = $"SELECT SUM(wm_ticker) FROM work_myreg WHERE CONVERT(DATE, wm_accepter_date)='{date}' {userCondition} ";
                return SqlHelper.ExecuteCountQuery(querySQL);
            }
            else if(type == 2)
            {
                string userCondition = userId == null ? string.Empty : $"AND rl_user_id='{userId}'";
                string querySQL = $"SELECT COUNT(rl_id) FROM remake_log WHERE rl_date='{date}' {userCondition} ";
                return SqlHelper.ExecuteCountQuery(querySQL);
            }
            return -1;
        }

        /// <summary>
        /// 获取指定日期下指定用户加工的文件数
        /// </summary>
        private int GetFileAmount(object date, object userId, int type)
        {
            string key = type == 1 ? "worker" : "checker";
            string userCondition = userId == null ? string.Empty : $"AND pfl_{ key}_id='{userId}'";
            string querySQL = $"SELECT COUNT(pfl_id) FROM processing_file_list WHERE pfl_{key}_date='{date}' {userCondition} ";
            return SqlHelper.ExecuteCountQuery(querySQL);
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
            return -1;
        }

        private void LoadViewList(DataTable table)
        {
            countView.Rows.Clear();
            table.DefaultView.Sort = "date ASC";
            table = table.DefaultView.ToTable();
            foreach(DataRow row in table.Rows)
            {
                int i = countView.Rows.Add();
                countView.Rows[i].Cells["date"].Value = GetDateValue(row["date"], "yyyy-MM-dd");
                countView.Rows[i].Cells["pcount"].Value = row["pcount"];
                countView.Rows[i].Cells["tcount"].Value = row["tcount"];
                countView.Rows[i].Cells["fcount"].Value = row["fcount"];
                countView.Rows[i].Cells["bcount"].Value = row["bcount"];
                countView.Rows[i].Cells["pgcount"].Value = row["pgcount"];
            }
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

        private void Rdo_ZJ_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton box = sender as RadioButton;
            if(box.Checked)
                countView.Columns["bcount"].HeaderText = "返工数";
            else
                countView.Columns["bcount"].HeaderText = "被返工数";

            countView.Rows.Clear();
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
            string querySQL = "SELECT p.F_ID,F_Title, pCount, bCount, fCount FROM( SELECT F_Title, F_ID, pCount " +
                "FROM(SELECT p.F_Title, p.F_ID, COUNT(A.pi_id) AS pCount FROM T_Plan AS p LEFT OUTER JOIN (" +
                "SELECT pi_id, pi_source_id, pi_orga_id, pi_year, pi_start_datetime FROM project_info WHERE pi_categor = 2 UNION ALL " +
               $"SELECT ti_id, ti_source_id, ti_orga_id, ti_year, ti_start_datetime FROM topic_info) AS A ON A.pi_source_id = p.F_ID {value} {minYearCondition} {maxYearCondition} " +
                "GROUP BY F_Title, p.F_ID) AS B WHERE pCount > 0) p LEFT JOIN( SELECT   F_ID, bCount FROM(SELECT p.F_ID, COUNT(pb.pb_id) AS bCount FROM T_Plan AS p LEFT OUTER JOIN (" +
                "SELECT pi_id, pi_source_id, pi_orga_id, pi_year, pi_start_datetime FROM project_info WHERE(pi_categor = 2) UNION ALL " +
               $"SELECT ti_id, ti_source_id, ti_orga_id, ti_year, ti_start_datetime FROM topic_info) AS A ON A.pi_source_id = p.F_ID {value} {minYearCondition} {maxYearCondition} " +
                "LEFT JOIN processing_box pb ON pb.pb_obj_id = A.pi_id GROUP BY p.F_ID) AS B ) b ON p.F_ID = b.F_ID LEFT JOIN( " +
                "SELECT F_ID, fCount FROM (SELECT p.F_ID, COUNT(pb.pfl_id) AS fCount FROM T_Plan AS p LEFT OUTER JOIN (" +
                "SELECT pi_id, pi_source_id, pi_orga_id, pi_year, pi_start_datetime FROM project_info WHERE pi_categor = 2 UNION ALL " +
               $"SELECT ti_id, ti_source_id, ti_orga_id, ti_year, ti_start_datetime FROM topic_info) AS A ON A.pi_source_id = p.F_ID {value} {minYearCondition} {maxYearCondition} " +
                "LEFT JOIN processing_file_list pb ON pb.pfl_obj_id = A.pi_id GROUP BY p.F_ID) AS B ) f ON f.F_ID = b.F_ID ";
            DataTable table = SqlHelper.ExecuteQuery(querySQL);
            DataGridViewStyleHelper.ResetDataGridView(view, true);
            DataTable tableEntity = new DataTable();
            tableEntity.Columns.AddRange(new DataColumn[]
            {
                    new DataColumn("计划类别编号"),
                    new DataColumn("计划类别名称"),
                    new DataColumn("项目/课题数"),
                    new DataColumn("盒数"),
                    new DataColumn("文件数"),
            });
            int totalPcount = 0, totalFcount = 0, totalBcount = 0;
            DataRowCollection rowCollection = table.Rows;
            for(int i = 0; i < rowCollection.Count; i++)
            {
                DataRow row = rowCollection[i];
                int _pcount = ToolHelper.GetIntValue(row["pCount"], 0);
                int _bcount = ToolHelper.GetIntValue(row["bCount"], 0);
                int _fcount = ToolHelper.GetIntValue(row["fCount"], 0);
                tableEntity.Rows.Add(row["F_ID"], row["F_Title"], _pcount, _bcount, _fcount);

                totalPcount += _pcount;
                totalBcount += _bcount;
                totalFcount += _fcount;
            }
            tableEntity.Rows.Add(string.Empty, "合计", totalPcount, totalBcount, totalFcount);
            view.DataSource = tableEntity;
            view.Columns[0].Visible = false;
            tabPane2.SelectedPageIndex = 0;
        }

        /// <summary>
        /// 计划类别点击事件
        /// </summary>
        private void Bc_Element_Click(object sender, EventArgs e)
        {
            string value = (sender as AccordionControlElement).Name;
            if("all_ptype".Equals(value))
                value = string.Empty;
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
            string querySQL = "SELECT p.F_ID,F_Title, pCount, bCount, fCount FROM( " +
                "SELECT F_Title, F_ID, pCount FROM(SELECT p.F_Title, p.F_ID, COUNT(A.pi_id) AS pCount FROM T_SourceOrg AS p LEFT OUTER JOIN (" +
                "SELECT pi_id, pi_source_id, pi_orga_id, pi_year, pi_start_datetime FROM project_info WHERE(pi_categor = 2) UNION ALL " +
               $"SELECT ti_id, ti_source_id, ti_orga_id, ti_year, ti_start_datetime FROM topic_info) AS A ON A.pi_orga_id = p.F_ID {value} {minYearCondition} {maxYearCondition} " +
                "GROUP BY F_Title, p.F_ID) AS B WHERE pCount>0) p LEFT JOIN(SELECT   F_ID, bCount FROM(" +
                "SELECT p.F_ID, COUNT(pb.pb_id) AS bCount FROM T_SourceOrg AS p LEFT OUTER JOIN(" +
                "SELECT pi_id, pi_source_id, pi_orga_id, pi_year, pi_start_datetime FROM project_info WHERE pi_categor = 2 UNION ALL " +
               $"SELECT ti_id, ti_source_id, ti_orga_id, ti_year, ti_start_datetime FROM topic_info) AS A ON A.pi_orga_id = p.F_ID {value} {minYearCondition} {maxYearCondition} " +
                "LEFT JOIN processing_box pb ON pb.pb_obj_id = A.pi_id GROUP BY p.F_ID) AS B) b ON p.F_ID = b.F_ID LEFT JOIN( " +
                "SELECT F_ID, fCount FROM      (SELECT p.F_ID, COUNT(pb.pfl_id) AS fCount FROM      T_SourceOrg AS p LEFT OUTER JOIN (" +
                "SELECT pi_id, pi_source_id, pi_orga_id, pi_year, pi_start_datetime FROM project_info WHERE   (pi_categor = 2) UNION ALL " +
               $"SELECT ti_id, ti_source_id, ti_orga_id, ti_year, ti_start_datetime FROM topic_info) AS A ON A.pi_orga_id = p.F_ID {value} {minYearCondition} {maxYearCondition} " +
                "LEFT JOIN processing_file_list pb ON pb.pfl_obj_id = A.pi_id GROUP BY p.F_ID) AS B ) f ON f.F_ID = b.F_ID; ";

            DataTable table = SqlHelper.ExecuteQuery(querySQL);
            int totalPcount = 0, totalFcount = 0, totalBcount = 0;
            DataGridViewStyleHelper.ResetDataGridView(view, true);
            DataTable tableEntity = new DataTable();
            tableEntity.Columns.AddRange(new DataColumn[]
            {
                    new DataColumn("来源单位编号"),
                    new DataColumn("来源单位名称"),
                    new DataColumn("项目/课题数"),
                    new DataColumn("盒数"),
                    new DataColumn("文件数"),
            });
            DataRowCollection rowCollection = table.Rows;
            for(int i = 0; i < rowCollection.Count; i++)
            {
                DataRow row = rowCollection[i];
                int _pcount = ToolHelper.GetIntValue(row["pCount"], 0);
                int _bcount = ToolHelper.GetIntValue(row["bCount"], 0);
                int _fcount = ToolHelper.GetIntValue(row["fCount"], 0);
                tableEntity.Rows.Add(row["F_ID"], row["F_Title"], _pcount, _bcount, _fcount);

                totalPcount += _pcount;
                totalBcount += _bcount;
                totalFcount += _fcount;
            }

            tableEntity.Rows.Add(string.Empty, "合计", totalPcount, totalBcount, totalFcount);
            view.DataSource = tableEntity;
            view.Columns[0].Visible = false;
            tabPane2.SelectedPageIndex = 0;
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(tabControl1.SelectedIndex == 0)
            {
                ac_LeftMenu.SelectedElement = ace_all;
                Item_Click(new AccordionControlElement() { Name = "ace_all" }, null);
            }
            else
            {
                bc_LeftMenu.SelectedElement = all_ptype;
                Bc_Element_Click(new AccordionControlElement() { Name = "all_ptype" }, null);
            }
        }

        private void tabPane1_SelectedPageIndexChanged(object sender, EventArgs e)
        {
            if(tabPane1.SelectedPageIndex == 1)
            {
                tabPane2.SelectedPageIndex = 0;
                tabControl1_SelectedIndexChanged(null, null);
            }
        }

        private void tabPane2_SelectedPageIndexChanged(object sender, EventArgs e)
        {
            chart1.Series.Clear();
            chart2.Series.Clear();
            chart3.Series.Clear();
            if(tabPane2.SelectedPageIndex == 1)
            {
                int tpye1 = tabControl1.SelectedIndex;
                //来源单位0
                if(tpye1 == 0)
                {
                    Series amount = new Series("项目/课题数")
                    {
                        IsValueShownAsLabel = true,
                        ShadowOffset = 5,
                        ChartType = SeriesChartType.Column
                    };
                    for(int i = 0; i < view.RowCount - 1; i++)
                    {
                        amount.Points.AddXY(view.Rows[i].Cells[1].Value, view.Rows[i].Cells[2].Value);
                    }

                    Series amount2 = new Series("文件数")
                    {
                        IsValueShownAsLabel = true,
                        ShadowOffset = 5,
                        ChartType = SeriesChartType.Column
                    };
                    for(int i = 0; i < view.RowCount - 1; i++)
                    {
                        amount2.Points.AddXY(view.Rows[i].Cells[1].Value, view.Rows[i].Cells[4].Value);
                    }

                    Series amount3 = new Series("盒数")
                    {
                        IsValueShownAsLabel = true,
                        ShadowOffset = 5,
                        ChartType = SeriesChartType.Column
                    };
                    for(int i = 0; i < view.RowCount - 1; i++)
                    {
                        amount3.Points.AddXY(view.Rows[i].Cells[1].Value, view.Rows[i].Cells[3].Value);
                    }

                    chart1.Series.Add(amount);
                    chart2.Series.Add(amount2);
                    chart1.Series.Add(amount3);

                    //年度统计
                    string orgName = ac_LeftMenu.SelectedElement.Name;
                    object[] years = btn_Year.Text.Split('-');
                    if(years.Length != 2)
                    {
                        string querySQL_Year = "SELECT MIN(myear) maxyear, MAX(myear) minyear FROM( " +
                            "SELECT ISNULL(pi_year, TRY_CAST(SUBSTRING(pi_start_datetime, 1, 4) AS INT)) AS myear, pi_orga_id FROM " +
                            "(SELECT pi_year, pi_start_datetime, pi_orga_id FROM project_info WHERE pi_categor = 2 " +
                            "UNION ALL SELECT ti_year, ti_start_datetime, ti_orga_id FROM topic_info WHERE ti_categor = -3 )a)b " +
                            "WHERE myear IS NOT NULL AND myear> '0' AND myear<= YEAR(SYSDATETIME()) ";
                        if(!"ace_all".Equals(orgName))
                            querySQL_Year += $"AND pi_orga_id='{orgName}' ";
                        years = SqlHelper.ExecuteRowsQuery(querySQL_Year);
                    }
                    if(!string.IsNullOrEmpty(ToolHelper.GetValue(years[0])) &&
                        !string.IsNullOrEmpty(ToolHelper.GetValue(years[1])))
                    {
                        int minYear = ToolHelper.GetIntValue(years[0]);
                        int maxYear = ToolHelper.GetIntValue(years[1]);
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
                //计划类别1
                else
                {
                    Series amount = new Series("项目/课题数")
                    {
                        IsValueShownAsLabel = true,
                        ShadowOffset = 5,
                        ChartType = SeriesChartType.Column
                    };
                    for(int i = 0; i < view.RowCount - 1; i++)
                    {
                        amount.Points.AddXY(view.Rows[i].Cells[1].Value, view.Rows[i].Cells[2].Value);
                    }

                    Series amount2 = new Series("文件数")
                    {
                        IsValueShownAsLabel = true,
                        ShadowOffset = 5,
                        ChartType = SeriesChartType.Column
                    };
                    for(int i = 0; i < view.RowCount - 1; i++)
                    {
                        amount2.Points.AddXY(view.Rows[i].Cells[1].Value, view.Rows[i].Cells[4].Value);
                    }

                    Series amount3 = new Series("盒数")
                    {
                        IsValueShownAsLabel = true,
                        ShadowOffset = 5,
                        ChartType = SeriesChartType.Column
                    };
                    for(int i = 0; i < view.RowCount - 1; i++)
                    {
                        amount3.Points.AddXY(view.Rows[i].Cells[1].Value, view.Rows[i].Cells[3].Value);
                    }

                    chart1.Series.Add(amount);
                    chart2.Series.Add(amount2);
                    chart1.Series.Add(amount3);

                    //年度统计
                    string orgName = bc_LeftMenu.SelectedElement.Name;
                    object[] years = btn_Year.Text.Split('-');
                    if(years.Length != 2)
                    {
                        string querySQL_Year = "SELECT MIN(myear) maxyear, MAX(myear) minyear FROM( " +
                        "SELECT ISNULL(pi_year, TRY_CAST(SUBSTRING(pi_start_datetime, 1, 4) AS INT)) AS myear, pi_source_id FROM " +
                        "(SELECT pi_year, pi_start_datetime, pi_source_id FROM project_info WHERE pi_categor = 2 " +
                        "UNION ALL SELECT ti_year, ti_start_datetime, ti_source_id FROM topic_info WHERE ti_categor = -3 )a)b " +
                        "WHERE myear IS NOT NULL AND myear> '0' AND myear<= YEAR(SYSDATETIME()) ";
                        if(!"all_ptype".Equals(orgName))
                            querySQL_Year += $"AND pi_source_id='{orgName}' ";
                        years = SqlHelper.ExecuteRowsQuery(querySQL_Year);
                    }
                    if(!string.IsNullOrEmpty(ToolHelper.GetValue(years[0])) &&
                        !string.IsNullOrEmpty(ToolHelper.GetValue(years[1])))
                    {
                        int minYear = ToolHelper.GetIntValue(years[0]);
                        int maxYear = ToolHelper.GetIntValue(years[1]);
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
                    object boxAmount = chart.Series[1].Points[i].YValues[0];
                    //分别显示x轴和y轴的数值                   
                    lbl_TipAmount.Text = $"项目/课题数：{proAmount}\r\n盒数：{boxAmount}";
                }
                else if("chart2".Equals(chartName))
                {
                    lbl_TipAmount.Text = $"文件数：{dp.YValues[0]}";
                }
                else if("chart3".Equals(chartName))
                {
                    lbl_TipName.Text = $"{dp.XValue} {e.HitTestResult.Series.Name}";
                    lbl_TipAmount.Text = $"项目/课题数：{dp.YValues[0]}";
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
                        bool flag = MicrosoftWordHelper.ExportToExcel(table, saveFileDialog.FileName);
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

        private void Btn_Year_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if(e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Clear)
                btn_Year.ResetText();
            else if(e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Search)
            {
                string[] years = btn_Year.Text.Split('-');
                if(years.Length == 2)
                {
                    btn_Year.ErrorText = null;
                    string minYear = years[0].Trim(), maxYear = years[1].Trim();
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
                else
                {
                    btn_Year.ErrorText = "请输入立项年度范围。";
                    btn_Year.Focus();
                }
            }
        }
    }
}
