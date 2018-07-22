using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Windows.Forms;

namespace 科技计划项目档案数据采集管理系统
{
    public partial class Frm_Query : DevExpress.XtraEditors.XtraForm
    {
        private Form form;
        public Frm_Query(Form form)
        {
            InitializeComponent();
            this.form = form;
        }

        private void Frm_Query_Load(object sender, EventArgs e)
        {
            cbo_UserList.DataSource = SqlHelper.ExecuteQuery($"SELECT ul_id, real_name FROM user_list");
            cbo_UserList.ValueMember = "ul_id";
            cbo_UserList.DisplayMember = "real_name";
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
                    object date = list[i][0];
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
                    object date = list2[i][0];
                    object pcount = 0;
                    object tcount = list2[i][1];
                    object fcount = GetFileAmount(date, userId, 1);
                    object bcount = GetBackAmount(date, userId, 1);
                    object pgcount = GetPageAmount(date, userId, 1);
                    table.Rows.Add(date, pcount, tcount, fcount, bcount, pgcount);
                }

                //单独统计文件加工工作量
                querySQL = "SELECT pfl_worker_date, COUNT(pfl_id) FROM processing_file_list LEFT JOIN project_info ON pi_id = pfl_obj_id " +
                    $"WHERE pfl_worker_id = '{userId}' AND pi_worker_date<>pfl_worker_date " +
                    $"GROUP BY pfl_worker_date";
                List<object[]> list3 = SqlHelper.ExecuteColumnsQuery(querySQL, 2);
                for(int i = 0; i < list3.Count; i++)
                {
                    object date = list3[i][0];
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
                    object date = list[i][0];
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
                string querySQL = $"SELECT wm_ticker FROM work_myreg WHERE wm_user='{userId}' AND wm_accepter_date='{date}'";
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
            view.Rows.Clear();
            table.DefaultView.Sort = "date ASC";
            table = table.DefaultView.ToTable();
            foreach(DataRow row in table.Rows)
            {
                int i = view.Rows.Add();
                view.Rows[i].Cells["date"].Value = GetDateValue(row["date"]);
                view.Rows[i].Cells["pcount"].Value = row["pcount"];
                view.Rows[i].Cells["tcount"].Value = row["tcount"];
                view.Rows[i].Cells["fcount"].Value = row["fcount"];
                view.Rows[i].Cells["bcount"].Value = row["bcount"];
                view.Rows[i].Cells["pgcount"].Value = row["pgcount"];
            }
        }

        private object GetDateValue(object value)
        {
            if(value == null)
                return null;
            else
            {
                if(DateTime.TryParse(value.ToString(), out DateTime result))
                    return result.ToString("yyyy-MM-dd");
                else
                    return null;
            }
        }

        private void view_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            lbl_Tip.Text = $"合计 {view.RowCount} 条数据";
        }

        private void Btn_Exprot_Click(object sender, EventArgs e)
        {
            if(view.RowCount == 0)
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
                    for(int i = 0; i < view.ColumnCount; i++)
                    {
                        if(i > 0)
                        {
                            str += "\t";
                        }
                        str += view.Columns[i].HeaderText;
                    }
                    sw.WriteLine(str);
                    for(int j = 0; j < view.Rows.Count; j++)
                    {
                        string tempStr = "";
                        for(int k = 0; k < view.Columns.Count; k++)
                        {
                            if(k > 0)
                            {
                                tempStr += "\t";
                            }
                            tempStr += view.Rows[j].Cells[k].Value.ToString();
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

                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    sw.Close();
                    myStream.Close();
                }
            }
        }

        private void Frm_Query_FormClosing(object sender, FormClosingEventArgs e)
        {
            form.Show();
        }

        private void rdo_ZJ_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton box = sender as RadioButton;
            if(box.Checked)
                view.Columns["bcount"].HeaderText = "返工数";
            else
                view.Columns["bcount"].HeaderText = "被返工数";
        }
    }
}
