using DevExpress.XtraBars.Navigation;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;

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
            string querySql = "SELECT dd_id, dd_name FROM data_dictionary WHERE dd_pId=" +
                "(SELECT dd_id FROM data_dictionary WHERE dd_code = 'dic_key_company_source') ORDER BY dd_sort";
            DataTable table = SqlHelper.ExecuteQuery(querySql);
            for(int i = 0; i < table.Rows.Count; i++)
            {
                AccordionControlElement element = new AccordionControlElement()
                {
                    Style = ElementStyle.Item,
                    Name = ToolHelper.GetValue(table.Rows[i]["dd_id"]),
                    Text = ToolHelper.GetValue(table.Rows[i]["dd_name"]),
                };
                element.Click += Item_Click;
                acg_Register.Elements.Add(element);
            }
        }

        private void Frm_Statistics_Load(object sender, EventArgs e)
        {
            LoadDataList(null);
            cbo_UserList.DataSource = SqlHelper.ExecuteQuery($"SELECT ul_id, real_name FROM user_list");
            cbo_UserList.ValueMember = "ul_id";
            cbo_UserList.DisplayMember = "real_name";

            tabPane1.SelectedPage = tabNavigationPage1;
        }

        /// <summary>
        /// 加载统计列表
        /// </summary>
        /// <param name="value">来源单位ID</param>
        private void LoadDataList(string value)
        {
            view.Rows.Clear();
            string querySQL = $"SELECT dd_code, dd_name FROM (" +
                $"SELECT dd_code, dd_name FROM data_dictionary WHERE dd_pId = " +
                $"(SELECT TOP(1) dd_id FROM data_dictionary WHERE dd_code = 'dic_key_plan')) B " +
                $"WHERE(dd_code <> 'ZX' AND dd_code <> 'YF') " +
                $"UNION ALL " +
                $"SELECT dd_code, dd_name FROM data_dictionary " +
                $"WHERE dd_pId = (SELECT TOP(1) dd_id FROM data_dictionary WHERE dd_code = 'dic_key_project') " +
                $"ORDER BY dd_code ";

            DataTable table = SqlHelper.ExecuteQuery(querySQL);
            searchControl.Properties.Items.Clear();
            foreach(DataRow row in table.Rows)
            {
                int rowIndex = view.Rows.Add();
                view.Rows[rowIndex].Cells["pName"].Value = row["dd_name"];
                view.Rows[rowIndex].Cells["pAmount"].Value = GetProjectAmount(row["dd_code"], value);
                view.Rows[rowIndex].Cells["fAmount"].Value = GetFileAmount(row["dd_code"], value);
                view.Rows[rowIndex].Cells["bAmount"].Value = GetBoxAmount(row["dd_code"], value);
                searchControl.Properties.Items.Add(row["dd_name"]);
            }
        }

        /// <summary>
        /// 获取指定计划类别下的项目/课题数
        /// </summary>
        /// <param name="planTypeCode">计划类别编号</param>
        /// <param name="unitID">来源单位ID</param>
        /// <returns></returns>
        private int GetProjectAmount(object planTypeCode, string unitID)
        {
            string querySQL = string.Empty;
            if(string.IsNullOrEmpty(unitID))
            {
                if(ToolHelper.GetValue(planTypeCode).Contains("ZX"))
                {
                    querySQL = "SELECT COUNT(idi.imp_id) AS count FROM imp_dev_info idi " +
                        "LEFT JOIN (SELECT pi_obj_id FROM project_info WHERE pi_categor = 2 UNION ALL " +
                        "SELECT ti_obj_id FROM topic_info WHERE ti_categor = -3) A ON idi.imp_id = A.pi_obj_id " +
                       $"WHERE idi.imp_code = '{planTypeCode}' " +
                        "GROUP BY idi.imp_code";
                }
                else
                {
                    querySQL = "SELECT COUNT(pi.pi_id) AS count FROM project_info pi " +
                        "LEFT JOIN(SELECT pi_obj_id FROM project_info WHERE pi_categor = 2 UNION ALL " +
                        "SELECT ti_obj_id FROM topic_info WHERE ti_categor = -3) A ON pi.pi_id = A.pi_obj_id " +
                        $"WHERE pi.pi_categor = 1 AND pi_code='{planTypeCode}' " +
                        "GROUP BY pi.pi_code";
                }
            }
            else
            {
                if(ToolHelper.GetValue(planTypeCode).Contains("ZX"))
                {
                    querySQL = " SELECT COUNT(A.pi_obj_id) FROM transfer_registration_pc trp " +
                        "LEFT JOIN imp_info ii ON trp.trp_id = ii.imp_obj_id " +
                       $"LEFT JOIN imp_dev_info idi ON idi.imp_obj_id = ii.imp_id AND idi.imp_code = '{planTypeCode}' " +
                        "LEFT JOIN(SELECT pi_obj_id FROM project_info WHERE pi_categor = 2 UNION ALL " +
                        "SELECT ti_obj_id FROM topic_info WHERE ti_categor = -3) A ON A.pi_obj_id = idi.imp_id " +
                       $"WHERE trp.com_id = '{unitID}' " +
                       $"GROUP BY com_id";
                }
                else
                {
                    querySQL = "SELECT COUNT(A.pi_obj_id) FROM transfer_registration_pc trp " +
                       $"LEFT JOIN project_info pi ON(trp.trp_id = pi.trc_id AND pi.pi_categor = 1 AND pi.pi_code = '{planTypeCode}') " +
                        "LEFT JOIN (SELECT pi_obj_id FROM project_info WHERE pi_categor = 2 UNION ALL SELECT ti_obj_id FROM topic_info WHERE ti_categor = -3) A " +
                       $"ON A.pi_obj_id = pi.pi_id WHERE trp.com_id = '{unitID}' " +
                        "GROUP BY com_id";
                }
            }
            return SqlHelper.ExecuteCountQuery(querySQL);
        }

        /// <summary>
        /// 获取指定计划类别下的盒数
        /// </summary>
        /// <param name="planTypeCode">计划类别编号</param>
        private object GetBoxAmount(object planTypeCode, string unitID)
        {
            string querySQL = string.Empty;
            if(string.IsNullOrEmpty(unitID))
            {
                if(!ToolHelper.GetValue(planTypeCode).Contains("ZX"))
                {
                    querySQL = "SELECT COUNT(pb.pb_id) FROM project_info pi " +
                      "LEFT JOIN( SELECT pi_id, pi_obj_id FROM project_info WHERE pi_categor= 2 " +
                      "UNION ALL SELECT ti_id, ti_obj_id FROM topic_info) A ON A.pi_obj_id = pi.pi_id " +
                      "LEFT JOIN processing_box pb ON pb.pb_obj_id = A.pi_id " +
                      $"WHERE pi.pi_code = '{planTypeCode}' GROUP BY pi.pi_code";
                }
                else
                {
                    querySQL = "SELECT COUNT(pb.pb_id) FROM imp_dev_info idi " +
                        "LEFT JOIN(SELECT pi_id, pi_obj_id FROM project_info WHERE pi_categor = 2 " +
                        "UNION ALL SELECT ti_id, ti_obj_id FROM topic_info) A ON A.pi_obj_id = idi.imp_id " +
                        "LEFT JOIN processing_box pb ON pb.pb_obj_id = A.pi_id " +
                        $"WHERE idi.imp_code = '{planTypeCode}' GROUP BY imp_code";
                }
            }
            else
            {
                if(ToolHelper.GetValue(planTypeCode).Contains("ZX"))
                {
                    querySQL = " SELECT COUNT(pfl.pb_id) FROM transfer_registration_pc trp " +
                        "LEFT JOIN imp_info ii ON trp.trp_id = ii.imp_obj_id " +
                       $"LEFT JOIN imp_dev_info idi ON idi.imp_obj_id = ii.imp_id AND idi.imp_code = '{planTypeCode}' " +
                        "LEFT JOIN(SELECT pi_id, pi_obj_id FROM project_info WHERE pi_categor = 2 UNION ALL " +
                        "SELECT ti_id, ti_obj_id FROM topic_info WHERE ti_categor = -3) A ON A.pi_obj_id = idi.imp_id " +
                        "LEFT JOIN processing_box pfl ON pfl.pb_obj_id = A.pi_id " +
                       $"WHERE trp.com_id = '{unitID}' " +
                       $"GROUP BY com_id";
                }
                else
                {
                    querySQL = "SELECT COUNT(pfl.pb_id) FROM transfer_registration_pc trp " +
                      $"LEFT JOIN project_info pi ON(trp.trp_id = pi.trc_id AND pi.pi_categor = 1 AND pi.pi_code = '{planTypeCode}') " +
                       "LEFT JOIN (SELECT pi_id, pi_obj_id FROM project_info WHERE pi_categor = 2 UNION ALL " +
                       "SELECT ti_id, ti_obj_id FROM topic_info WHERE ti_categor = -3) A ON A.pi_obj_id = pi.pi_id " +
                       "LEFT JOIN processing_box pfl ON pfl.pb_obj_id = A.pi_id " +
                      $"WHERE trp.com_id = '{unitID}' " +
                       "GROUP BY com_id";
                }
            }
            return SqlHelper.ExecuteCountQuery(querySQL);
        }

        /// <summary>
        /// 获取指定计划类别下的文件数
        /// </summary>
        /// <param name="planTypeCode">计划类别编号</param>
        private int GetFileAmount(object planTypeCode, string unitID)
        {
            string querySQL = string.Empty;
            if(string.IsNullOrEmpty(unitID))
            {
                if(ToolHelper.GetValue(planTypeCode).Contains("ZX"))
                {
                    querySQL = "SELECT COUNT(pfl.pfl_id) FROM imp_dev_info idi " +
                        "LEFT JOIN(SELECT pi_id, pi_obj_id FROM project_info WHERE pi_categor = 2 " +
                        "UNION ALL SELECT ti_id, ti_obj_id FROM topic_info) A ON A.pi_obj_id = idi.imp_id " +
                        "LEFT JOIN processing_file_list pfl ON pfl.pfl_obj_id = A.pi_id " +
                        $"WHERE idi.imp_code = '{planTypeCode}' GROUP BY imp_code";
                }
                else
                {
                    querySQL = "SELECT COUNT(pfl.pfl_id) FROM project_info pi " +
                      "LEFT JOIN( SELECT pi_id, pi_obj_id FROM project_info WHERE pi_categor= 2 " +
                      "UNION ALL SELECT ti_id, ti_obj_id FROM topic_info) A ON A.pi_obj_id = pi.pi_id " +
                      "LEFT JOIN processing_file_list pfl ON pfl.pfl_obj_id = A.pi_id " +
                      $"WHERE pi.pi_code = '{planTypeCode}' GROUP BY pi.pi_code";
                }
            }
            else
            {
                if(ToolHelper.GetValue(planTypeCode).Contains("ZX"))
                {
                    querySQL = " SELECT COUNT(pfl.pfl_id) FROM transfer_registration_pc trp " +
                        "LEFT JOIN imp_info ii ON trp.trp_id = ii.imp_obj_id " +
                       $"LEFT JOIN imp_dev_info idi ON idi.imp_obj_id = ii.imp_id AND idi.imp_code = '{planTypeCode}' " +
                        "LEFT JOIN(SELECT pi_id, pi_obj_id FROM project_info WHERE pi_categor = 2 UNION ALL " +
                        "SELECT ti_id, ti_obj_id FROM topic_info WHERE ti_categor = -3) A ON A.pi_obj_id = idi.imp_id " +
                        "LEFT JOIN processing_file_list pfl ON pfl.pfl_obj_id=A.pi_id " +
                       $"WHERE trp.com_id = '{unitID}' " +
                       $"GROUP BY com_id";
                }
                else
                {
                    querySQL = "SELECT COUNT(pfl.pfl_id) FROM transfer_registration_pc trp " +
                      $"LEFT JOIN project_info pi ON(trp.trp_id = pi.trc_id AND pi.pi_categor = 1 AND pi.pi_code = '{planTypeCode}') " +
                       "LEFT JOIN (SELECT pi_id, pi_obj_id FROM project_info WHERE pi_categor = 2 UNION ALL " +
                       "SELECT ti_id, ti_obj_id FROM topic_info WHERE ti_categor = -3) A ON A.pi_obj_id = pi.pi_id " +
                       "LEFT JOIN processing_file_list pfl ON pfl.pfl_obj_id = A.pi_id " +
                      $"WHERE trp.com_id = '{unitID}' " +
                       "GROUP BY com_id";
                }
            }
            return SqlHelper.ExecuteCountQuery(querySQL);
        }

        private void chk_AllDate_CheckedChanged(object sender, EventArgs e)
        {
            dtp_StartDate.Enabled = dtp_EndDate.Enabled = !chk_AllDate.Checked;
        }

        private void Btn_StartCount_Click(object sender, EventArgs e)
        {
            object userId = cbo_UserList.SelectedValue;
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
                string queryCondition = string.Empty;
                if(!flag)//全部时间
                {
                    if(startDate.Date == endDate.Date)
                        queryCondition = $"WHERE pi_worker_date =  CONVERT(DATE, '{startDate}')";
                    else
                        queryCondition = $"WHERE pi_worker_date >=  CONVERT(DATE, '{startDate}') AND pi_worker_date <=  CONVERT(DATE, '{endDate}')";
                }
                string querySQL = "SELECT pi_worker_date, COUNT(pi_id) FROM(" +
                    $"SELECT pi_id, pi_worker_date FROM project_info WHERE pi_categor = 2 AND pi_worker_id='{userId}' " +
                    "UNION ALL " +
                    $"SELECT ti_id, ti_worker_date FROM topic_info WHERE ti_categor = -3 AND ti_worker_id='{userId}') AS TB1 {queryCondition} " +
                    $"GROUP BY pi_worker_date";
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
                     $"LEFT JOIN project_info ON ti_obj_id = pi_id WHERE ti_categor = -3 AND ti_worker_id='{userId}' " +
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

        private void Item_Click(object sender, EventArgs e)
        {
            string value = (sender as AccordionControlElement).Name;
            if("ace_all".Equals(value))
                value = string.Empty;
            LoadDataList(value);

        }
    }
}
