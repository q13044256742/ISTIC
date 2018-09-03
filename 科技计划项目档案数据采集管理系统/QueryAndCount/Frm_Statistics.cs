using DevExpress.XtraBars.Navigation;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Threading;
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
            for(int i = 0; i < table.Rows.Count; i++)
            {
                AccordionControlElement element = new AccordionControlElement()
                {
                    Style = ElementStyle.Item,
                    Name = ToolHelper.GetValue(table.Rows[i]["F_ID"]),
                    Text = ToolHelper.GetValue(table.Rows[i]["F_Title"]),
                };
                element.Click += Bc_Element_Click;
                bc_LeftMenu.Elements.Add(element);
            }
        }

        private void Frm_Statistics_Load(object sender, EventArgs e)
        {
            cbo_UserList.DataSource = SqlHelper.ExecuteQuery($"SELECT ul_id, real_name FROM user_list");
            cbo_UserList.ValueMember = "ul_id";
            cbo_UserList.DisplayMember = "real_name";

            tabPane1.SelectedPage = tabNavigationPage1;
            chart1.Series.Clear();
            chart2.Series.Clear();
            chart3.Series.Clear();
            ////////////////
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
            bool allUser = chk_AllUser.Checked;
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
                for(int i = 0; i < list.Count; i++)
                {
                    object date = GetDateValue(list[i][0], "yyyy-MM-dd");
                    object pcount = list[i][1];
                    object tcount = GetTopicAmount(date, userId, 1);
                    object fcount = GetFileAmount(date, userId, 1);
                    object bcount = GetBackAmount(date, userId, 1);
                    object pgcount = GetPageAmount(date, userId, 1);
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
                string queryCondition = string.Empty;
                if(!flag)//全部时间
                {
                    if(startDate.Date == endDate.Date)
                        queryCondition = $"WHERE pi_checker_date =  CONVERT(DATE, '{startDate}')";
                    else
                        queryCondition = $"WHERE pi_checker_date >=  CONVERT(DATE, '{startDate}') AND pi_checker_date <=  CONVERT(DATE, '{endDate}')";
                }
                string querySQL = "SELECT pi_checker_date, COUNT(pi_id) FROM(" +
                    $"SELECT pi_id, pi_checker_date FROM project_info WHERE pi_categor = 2 AND pi_checker_id='{userId}' " +
                    "UNION ALL " +
                    $"SELECT ti_id, ti_checker_date FROM topic_info WHERE ti_categor = -3 AND ti_checker_id='{userId}') AS TB1 {queryCondition} " +
                    $"GROUP BY pi_checker_date";
                List<object[]> list = SqlHelper.ExecuteColumnsQuery(querySQL, 2);
                for(int i = 0; i < list.Count; i++)
                {
                    object date = GetDateValue(list[i][0], "yyyy-MM-dd");
                    object pcount = list[i][1];
                    object tcount = GetTopicAmount(date, userId, 2);
                    object fcount = GetFileAmount(date, userId, 2);
                    object bcount = GetBackAmount(date, userId, 2);
                    object pgcount = GetPageAmount(date, userId, 2);
                    table.Rows.Add(date, pcount, tcount, fcount, bcount, pgcount);
                }

                LoadViewList(table);
            }
        }

        private int GetFilePageByFid(string querySql)
        {
            int count = 0;
            string querySQL = $"SELECT pfl_pages FROM processing_file_list WHERE pfl_id IN ({querySql})";
            object[] tics = SqlHelper.ExecuteSingleColumnQuery(querySQL);
            foreach(object item in tics)
                count += (int)item;
            return count;
        }

        /// <summary>
        /// 获取指定日期下指定用户的总加工页数
        /// </summary>
        private int GetPageAmount(object date, object userId, int type)
        {
            string key = type == 1 ? "worker" : "checker";
            int count = 0;
            string querySQL = $"SELECT pfl_pages FROM processing_file_list WHERE pfl_{key}_id='{userId}' AND pfl_{key}_date='{date}'";
            object[] tics = SqlHelper.ExecuteSingleColumnQuery(querySQL);
            foreach(object item in tics)
                count += (int)item;
            return count;
        }

        /// <summary>
        /// 获取指定日期下指定用户的返工数
        /// </summary>
        private int GetBackAmount(object date, object userId, int type)
        {
            if(type == 1)
            {
                int count = 0;
                string querySQL = $"SELECT wm_ticker FROM work_myreg WHERE wm_user='{userId}' AND CONVERT(DATE, wm_accepter_date)='{date}'";
                object[] tics = SqlHelper.ExecuteSingleColumnQuery(querySQL);
                foreach(object item in tics)
                    count += (int)item;
                return count;
            }
            else if(type == 2)
            {
                string querySQL = $"SELECT COUNT(rl_id) FROM remake_log WHERE rl_user_id='{userId}' AND rl_date='{date}'";
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
            string querySQL = $"SELECT COUNT(pfl_id) FROM processing_file_list WHERE pfl_{key}_id='{userId}' AND pfl_{key}_date='{date}'";
            return SqlHelper.ExecuteCountQuery(querySQL);
        }

        /// <summary>
        /// 获取指定日期下指定用户加工的课题/子课题数
        /// </summary>
        private int GetTopicAmount(object date, object userId, int type)
        {
            string userConditon = !chk_AllUser.Checked ? $" AND pi_worker_id='{userId}'" : string.Empty;
            if(type == 1)
            {
                string querySQL = "SELECT COUNT(ti_id) FROM(" +
                    $"SELECT ti_id, ti_worker_date FROM topic_info WHERE ti_categor = 3 AND ti_worker_id='{userId}' " +
                    "UNION ALL " +
                    $"SELECT si_id, si_worker_date FROM subject_info WHERE si_worker_id='{userId}') " +
                    $"AS TB1 WHERE ti_worker_date = '{date}'";
                return SqlHelper.ExecuteCountQuery(querySQL);
            }
            else if(type == 2)
            {
                string querySQL = "SELECT COUNT(ti_id) FROM(" +
                    $"SELECT ti_id, ti_checker_date FROM topic_info WHERE ti_categor = 3 AND ti_checker_id='{userId}' " +
                    "UNION ALL " +
                    $"SELECT si_id, si_checker_date FROM subject_info WHERE si_checker_id='{userId}') " +
                    $"AS TB1 WHERE ti_checker_date = '{date}'";
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
            view.Columns["pName"].HeaderText = "计划类别名称";
            string value = (sender as AccordionControlElement).Name;
            if("ace_all".Equals(value))
                value = string.Empty;
            else
                value = $"AND A.pi_orga_id = '{value}'";

            string querySQL = "SELECT F_ID, F_Title, pCount FROM (SELECT p.F_ID, p.F_Title, COUNT(A.pi_id) AS pCount FROM T_Plan AS p " +
                "LEFT OUTER JOIN (SELECT pi_id, pi_source_id, pi_orga_id FROM project_info WHERE(pi_categor = 2) " +
               $"UNION ALL SELECT ti_id, ti_source_id, ti_orga_id FROM topic_info) AS A ON (A.pi_source_id = p.F_ID {value}) " +
                "GROUP BY p.F_ID, p.F_Title) AS B WHERE (pCount <> 0)";

            DataTable table = SqlHelper.ExecuteQuery(querySQL);
            view.Rows.Clear();
            int totalPcount = 0, totalFcount = 0, totalBcount = 0;
            foreach(DataRow row in table.Rows)
            {
                int _pcount = ToolHelper.GetIntValue(row["pCount"], 0);
                int _fcount = ToolHelper.GetIntValue(GetFilesAmount(row["F_ID"], value, 0), 0);
                int _bcount = ToolHelper.GetIntValue(GetBoxsAmount(row["F_ID"], value, 0), 0);
                int rowIndex = view.Rows.Add();
                view.Rows[rowIndex].Tag = row["F_ID"];
                view.Rows[rowIndex].Cells["pName"].Value = row["F_Title"];
                view.Rows[rowIndex].Cells["pAmount"].Value = _pcount;
                view.Rows[rowIndex].Cells["fAmount"].Value = _fcount;
                view.Rows[rowIndex].Cells["bAmount"].Value = _bcount;
                totalPcount += _pcount;
                totalFcount += _fcount;
                totalBcount += _bcount;
            }

            int addRowIndex = view.Rows.Add();
            view.Rows[addRowIndex].Cells["pName"].Value = "合计";
            view.Rows[addRowIndex].Cells["pAmount"].Value = totalPcount;
            view.Rows[addRowIndex].Cells["fAmount"].Value = totalFcount;
            view.Rows[addRowIndex].Cells["bAmount"].Value = totalBcount;

            //如果当前是图形选项卡，则更新图形
            if(tabPane2.SelectedPageIndex == 1)
            {
                tabPane2_SelectedPageIndexChanged(null, null);
            }
        }
        
        /// <summary>
        /// 计划类别点击事件
        /// </summary>
        private void Bc_Element_Click(object sender, EventArgs e)
        {
            view.Columns["pName"].HeaderText = "来源单位名称";
            string value = (sender as AccordionControlElement).Name;
            if("all_ptype".Equals(value))
                value = string.Empty;
            else
                value = $"AND A.pi_source_id = '{value}'";
            
            string querySQL = "SELECT * FROM(SELECT s.F_ID, s.F_Title, COUNT(A.pi_id) AS pCount FROM T_SourceOrg AS s " +
                "LEFT OUTER JOIN (SELECT pi_id, pi_source_id, pi_orga_id FROM project_info WHERE(pi_categor = 2) UNION ALL SELECT ti_id, ti_source_id, ti_orga_id " +
               $"FROM topic_info) AS A ON (s.F_ID = A.pi_orga_id {value}) GROUP BY s.F_ID, s.F_Title) B WHERE pCount <> 0";

            DataTable table = SqlHelper.ExecuteQuery(querySQL);
            view.Rows.Clear();
            int totalPcount = 0, totalFcount = 0, totalBcount = 0;
            foreach(DataRow row in table.Rows)
            {
                int _pcount = ToolHelper.GetIntValue(row["pCount"], 0);
                int _fcount = ToolHelper.GetIntValue(GetFilesAmount(row["F_ID"], value, 1), 0);
                int _bcount = ToolHelper.GetIntValue(GetBoxsAmount(row["F_ID"], value, 1), 0);
                int rowIndex = view.Rows.Add();
                view.Rows[rowIndex].Tag = row["F_ID"];
                view.Rows[rowIndex].Cells["pName"].Value = row["F_Title"];
                view.Rows[rowIndex].Cells["pAmount"].Value = _pcount;
                view.Rows[rowIndex].Cells["fAmount"].Value = _fcount;
                view.Rows[rowIndex].Cells["bAmount"].Value = _bcount;
                totalPcount += _pcount;
                totalFcount += _fcount;
                totalBcount += _bcount;
            }

            int addRowIndex = view.Rows.Add();
            view.Rows[addRowIndex].Cells["pName"].Value = "合计";
            view.Rows[addRowIndex].Cells["pAmount"].Value = totalPcount;
            view.Rows[addRowIndex].Cells["fAmount"].Value = totalFcount;
            view.Rows[addRowIndex].Cells["bAmount"].Value = totalBcount;

            //如果当前是图形选项卡，则更新图形
            if(tabPane2.SelectedPageIndex == 1)
            {
                tabPane2_SelectedPageIndexChanged(null, null);
            }
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
            tabPane2.SelectedPageIndex = 0;
            tabControl1_SelectedIndexChanged(null, null);
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
                    Series amount = new Series("项目/课题数");
                    amount.IsValueShownAsLabel = true; amount.IsXValueIndexed = true;
                    amount.ChartType = SeriesChartType.Column;
                    for(int i = 0; i < view.RowCount - 1; i++)
                    {
                        amount.Points.AddXY(view.Rows[i].Cells["pName"].Value, view.Rows[i].Cells["pAmount"].Value);
                    }

                    Series amount2 = new Series("文件数");
                    amount2.IsValueShownAsLabel = true; amount2.IsXValueIndexed = true;
                    amount2.ChartType = SeriesChartType.Column;
                    for(int i = 0; i < view.RowCount - 1; i++)
                    {
                        amount2.Points.AddXY(view.Rows[i].Cells["pName"].Value, view.Rows[i].Cells["fAmount"].Value);
                    }

                    Series amount3 = new Series("盒数");
                    amount3.IsValueShownAsLabel = true; amount3.IsXValueIndexed = true;
                    amount3.ChartType = SeriesChartType.Column;
                    for(int i = 0; i < view.RowCount - 1; i++)
                    {
                        amount3.Points.AddXY(view.Rows[i].Cells["pName"].Value, view.Rows[i].Cells["bAmount"].Value);
                    }

                    chart1.Series.Add(amount);
                    chart2.Series.Add(amount2);
                    chart3.Series.Add(amount3);
                }
                //计划类别1
                else
                {
                    Series amount = new Series("项目/课题数");
                    amount.IsValueShownAsLabel = true;
                    amount.ChartType = SeriesChartType.Column;
                    for(int i = 0; i < view.RowCount - 1; i++)
                    {
                        amount.Points.AddXY(view.Rows[i].Cells["pName"].Value, view.Rows[i].Cells["pAmount"].Value);
                    }

                    Series amount2 = new Series("文件数");
                    amount2.IsValueShownAsLabel = true;
                    amount2.ChartType = SeriesChartType.Column;
                    for(int i = 0; i < view.RowCount - 1; i++)
                    {
                        amount2.Points.AddXY(view.Rows[i].Cells["pName"].Value, view.Rows[i].Cells["fAmount"].Value);
                    }

                    Series amount3 = new Series("盒数");
                    amount3.IsValueShownAsLabel = true;
                    amount3.ChartType = SeriesChartType.Column;
                    for(int i = 0; i < view.RowCount - 1; i++)
                    {
                        amount3.Points.AddXY(view.Rows[i].Cells["pName"].Value, view.Rows[i].Cells["bAmount"].Value);
                    }

                    chart1.Series.Add(amount);
                    chart2.Series.Add(amount2);
                    chart3.Series.Add(amount3);
                }
            }
        }

        private void chart1_GetToolTipText(object sender, ToolTipEventArgs e)
        {
            Chart chart = sender as Chart;
            //判断鼠标是否移动到数据标记点，是则显示提示信息
            if(e.HitTestResult.ChartElementType == ChartElementType.DataPoint)
            {
                int i = e.HitTestResult.PointIndex;
                DataPoint dp = e.HitTestResult.Series.Points[i];
                //分别显示x轴和y轴的数值                   
                lbl_TipName.Text = dp.AxisLabel;
                lbl_TipAmount.Text = "数量：" + dp.YValues[0];

                //鼠标相对于窗体左上角的坐标
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
    }
}
