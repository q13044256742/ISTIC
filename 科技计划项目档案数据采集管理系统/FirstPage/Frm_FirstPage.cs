﻿using DevExpress.XtraEditors;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace 科技计划项目档案数据采集管理系统.FirstPage
{
    public partial class Frm_FirstPage : XtraForm
    {
        Form loginFrom;
        public Frm_FirstPage(Form loginFrom)
        {
            InitializeComponent();
            InitialForm();
            this.loginFrom = loginFrom;
        }

        private void InitialForm()
        {
            view.ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle()
            {
                Font = new Font("微软雅黑", 12f, FontStyle.Bold),
                Alignment = DataGridViewContentAlignment.MiddleCenter,
                Padding = new Padding(3),
            };
            view2.ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle()
            {
                Font = new Font("微软雅黑", 12f, FontStyle.Bold),
                Alignment = DataGridViewContentAlignment.MiddleCenter,
                Padding = new Padding(3),
            };
            tabPane1.SelectedPage = tab_Work;
            object[] model = ToolHelper.GetModelByRole();

            foreach(TileGroup group in tileBar1.Groups)
            {
                bool flag = true;
                foreach(object key in model)
                {
                    if(group.Items[0].Name.Equals(key))
                    {
                        flag = false;
                        break;
                    }
                }
                if(flag)
                {
                    //首页默认全部加载
                    if(!"tbar_FirstPage".Equals(group.Items[0].Name))
                        group.Items[0].Visible = false;
                    //管理员默认加载后台管理
                    if(UserHelper.GetUserRole() == UserRole.DocManager &&
                        "tbar_Manage".Equals(group.Items[0].Name))
                        group.Items[0].Visible = true;
                }
            }

            tip_User.Text = $"当前用户：{UserHelper.GetUserRoleName()}（{UserHelper.GetUser().RealName}） {ToolHelper.GetDateValue(DateTime.Now, "yyyy年MM月dd日")} {System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.GetDayName(DateTime.Now.DayOfWeek)} " +
                $"农历{ToolHelper.GetChineseDateTime(DateTime.Now)}";
        }


        private void Frm_FirstPage_Load(object sender, EventArgs e)
        {
            LoadLastData(null);
            view.ClearSelection();

            string filePath = Application.ExecutablePath;

            //检查版本更新
            object lastVersion = SqlHelper.ExecuteOnlyOneQuery($"SELECT TOP(1) at_version FROM Attachment WHERE at_code='ISTIC' ORDER BY at_date DESC");
            if(lastVersion != null && Version.TryParse(ToolHelper.GetValue(lastVersion), out Version result))
            {
                Version currentVersion = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
                if(currentVersion < result)
                {
                    XtraMessageBox.Show("当前程序有新版本，请尽快更新。", "更新提醒", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        private void LoadLastData(string querySql)
        {
            search.Properties.Items.Clear();
            if(string.IsNullOrEmpty(querySql))
            {
                querySql = "SELECT TOP(100) * FROM (" +
                     "SELECT pi_id, pi_code, pi_name, pi_year, pi_worker_date, pi_worker_id, pi_checker_date, pi_checker_id FROM project_info WHERE pi_categor = 2 " +
                     "UNION ALL " +
                     "SELECT ti_id, ti_code, ti_name, ti_year, ti_worker_date, ti_worker_id, ti_checker_date, ti_checker_id FROM topic_info " +
                     "UNION ALL " +
                     "SELECT si_id, si_code, si_name, si_year, si_worker_date, si_worker_id, si_checker_date, si_checker_id FROM subject_info) TB1 ";
            }
            if(UserHelper.GetUserRole() == UserRole.Worker || 
                UserHelper.GetUserRole() == UserRole.DocManager || 
                UserHelper.GetUserRole() == UserRole.W_Q_Manager)
            {
                string querySql1 = querySql + "WHERE pi_worker_date IS NOT NULL ORDER BY TB1.pi_worker_date DESC";
                DataTable table = SqlHelper.ExecuteQuery(querySql1);
                view.Rows.Clear();
                foreach(DataRow row in table.Rows)
                {
                    int i = view.Rows.Add();
                    view.Rows[i].Tag = row["pi_id"];
                    view.Rows[i].Cells["id"].Value = i + 1;
                    view.Rows[i].Cells["code"].Value = row["pi_code"];
                    view.Rows[i].Cells["name"].Value = row["pi_name"];
                    view.Rows[i].Cells["year"].Value = row["pi_year"];
                    view.Rows[i].Cells["idate"].Value = ToolHelper.GetDateValue(row["pi_worker_date"], "yyyy-MM-dd");
                    view.Rows[i].Cells["user"].Value = UserHelper.GetUserNameById(row["pi_worker_id"]);
                    search.Properties.Items.AddRange(new object[] { row["pi_code"], row["pi_name"] });
                }
            }
            if(UserHelper.GetUserRole() == UserRole.Qualityer || 
                UserHelper.GetUserRole() == UserRole.DocManager || 
                UserHelper.GetUserRole() == UserRole.W_Q_Manager)
            {
                string querySql1 = querySql + "WHERE pi_checker_date IS NOT NULL ORDER BY TB1.pi_checker_date DESC";
                DataTable table = SqlHelper.ExecuteQuery(querySql1);
                view2.Rows.Clear();
                foreach(DataRow row in table.Rows)
                {
                    int i = view2.Rows.Add();
                    view2.Rows[i].Tag = row["pi_id"];
                    view2.Rows[i].Cells["cid"].Value = i + 1;
                    view2.Rows[i].Cells["ccode"].Value = row["pi_code"];
                    view2.Rows[i].Cells["cname"].Value = row["pi_name"];
                    view2.Rows[i].Cells["cyear"].Value = row["pi_year"];
                    view2.Rows[i].Cells["cdate"].Value = ToolHelper.GetDateValue(row["pi_checker_date"], "yyyy-MM-dd");
                    view2.Rows[i].Cells["cuser"].Value = UserHelper.GetUserNameById(row["pi_checker_id"]);
                }
            }
            if(UserHelper.GetUserRole() == UserRole.Worker)
                tabPane1.Pages.Remove(tab_Check);
            else if(UserHelper.GetUserRole() == UserRole.Qualityer)
                tabPane1.Pages.Remove(tab_Work);
        }

        /// <summary>
        /// 检索索引
        /// </summary>
        public int Count { get; set; } = -1;

        private void Search_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                string key = search.Text;
                if(!string.IsNullOrEmpty(key))
                {
                    view.ClearSelection();
                    foreach(DataGridViewRow row in view.Rows)
                    {
                        if(row.Index > Count)
                        {
                            string codeValue = ToolHelper.GetValue(row.Cells["code"].Value);
                            string nameValue = ToolHelper.GetValue(row.Cells["name"].Value);
                            if(codeValue.Contains(key) || nameValue.Contains(key))
                            {
                                row.Selected = true;
                                view.FirstDisplayedScrollingRowIndex = row.Index;
                                Count = row.Index;
                                return;
                            }
                        }
                    }
                    Count = -1;
                }
            }
        }

        private void Search_TextChanged(object sender, EventArgs e) => Count = -1;

        private void Tbar_ItemClick(object sender, DevExpress.XtraEditors.TileItemEventArgs e)
        {
            string itemName = e.Item.Name;
            if("tbar_ZLJG".Equals(itemName))//著录加工
            {
                Frm_MainFrame frm = GetFormHelper.GetMainFrame(new Frm_CG());
                frm.Show();
                frm.Activate();
            }
            else if("tbar_YJDJ".Equals(itemName))//移交登记
            {
                Frm_MainFrame frm = GetFormHelper.GetMainFrame(new Frm_ToR());
                frm.Show();
                frm.Activate();
            }
            else if("tbar_DAJS".Equals(itemName))//档案接收
            {
                Frm_MainFrame frm = GetFormHelper.GetMainFrame(new Frm_DomAccept());
                frm.Show();
                frm.Activate();
            }
            else if("tbar_DAZJ".Equals(itemName))//档案质检
            {
                Frm_MainFrame frm = GetFormHelper.GetMainFrame(new Frm_QT());
                frm.Show();
                frm.Activate();
            }
            
            else if("tbar_Manage".Equals(itemName))//后台管理
            {
                Frm_MainFrameManager frm = new Frm_MainFrameManager(this, UserHelper.GetUser());
                frm.ShowDialog();
            }
            else if("tbar_Count".Equals(itemName))//统计分析
            {
                Frm_Statistics frm = GetFormHelper.GetStatistic();
                frm.Show();
                frm.Activate();
            }
            else if("tbar_Query".Equals(itemName))//查询借阅
            {
                Frm_QueryBorrowing frm = GetFormHelper.GetQueryBorrow(this);
                frm.Show();
                frm.Activate();
            }
            else if("tbar_Download".Equals(itemName))//下载
            {
                Frm_Download frm = GetFormHelper.GetFileDownload();
                frm.Show();
                frm.Activate();
            }
        }

        private void btn_QuitUser_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            UserHelper.SetLogin(false);
            loginFrom.Show();
            loginFrom.Activate();
            Hide();
        }

        private void Frm_FirstPage_FormClosing(object sender, FormClosingEventArgs e)
        {
            UserHelper.SetLogin(false);
            Environment.Exit(0);
        }

        private void Btn_ExitSystem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Frm_FirstPage_FormClosing(null, null);
        }

        private void btn_Query_Click(object sender, EventArgs e)
        {
            string key1 = search.Text;
            string key2 = txt_DateSearch.Text;
            if(!string.IsNullOrEmpty(key1) || !string.IsNullOrEmpty(key2))
            {
                string querySQL = $"SELECT * FROM (SELECT pi_id, pi_code, pi_name, pi_start_datetime, pi_year, pi_funds, pi_worker_date, pi_worker_id FROM project_info " +
                    $"UNION ALL SELECT ti_id, ti_code, ti_name, ti_start_datetime, ti_year, ti_funds, ti_worker_date, ti_worker_id FROM topic_info " +
                    $"UNION ALL SELECT si_id, si_code, si_name, si_start_datetime, si_year, si_funds, si_worker_date, si_worker_id FROM subject_info ) A " +
                    $"WHERE 1=1 ";
                if(!string.IsNullOrEmpty(key1))
                    querySQL += $"AND (A.pi_code LIKE '%{key1}%' OR A.pi_name LIKE '%{key1}%') ";
                if(!string.IsNullOrEmpty(key2))
                    querySQL += $"AND A.pi_worker_date='{key2}' ";
                LoadLastData(querySQL);
            }
        }

        private void view_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if("code".Equals(view.Columns[e.ColumnIndex].Name) ||
                "ccode".Equals(view.Columns[e.ColumnIndex].Name))
            {
                object id = view.Rows[e.RowIndex].Tag;
                Frm_ProDetails details = new Frm_ProDetails(id);
                if(details.ShowDialog() == DialogResult.OK)
                {
                    LoadLastData(null);
                }
            }
        }
    }
}
