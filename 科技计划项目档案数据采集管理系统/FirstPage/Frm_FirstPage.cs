using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace 科技计划项目档案数据采集管理系统.FirstPage
{
    public partial class Frm_FirstPage : DevExpress.XtraEditors.XtraForm
    {
        public Frm_FirstPage()
        {
            InitializeComponent();
            InitialForm();
        }

        private void InitialForm()
        {
            view.ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle()
            {
                Font = new Font("微软雅黑", 12f, FontStyle.Bold),
                Alignment = DataGridViewContentAlignment.MiddleCenter,
                Padding = new Padding(3),
            };

            string[] keys = new string[0];
            if(UserHelper.GetUserRole() == UserRole.Worker)
            {
                //keys = new string[] { "tbar_FirstPage", "tbar_ZLJG", "tbar_Download" };
            }
            else if(UserHelper.GetUserRole() == UserRole.Qualityer)
            {
                keys = new string[] { "tbar_FirstPage", "tbar_DAZJ", "tbar_Download" };
            }
            else if(UserHelper.GetUserRole() == UserRole.W_Q_Manager)
            {
                keys = new string[] { "tbar_FirstPage", "tbar_ZLJG", "tbar_Count", "tbar_DAZJ", "tbar_Download" };
            }
            if(keys.Length > 0)
                foreach(TileGroup group in tileBar1.Groups)
                {
                    bool flag = true;
                    foreach(string key in keys)
                    {
                        if(group.Items[0].Name.Equals(key))
                        {
                            flag = false;
                            break;
                        }
                    }
                    if(flag)
                        group.Items[0].Visible = false;
                }


            tip_User.Text = $"当前用户：{UserHelper.GetUserRoleName()}（{UserHelper.GetUser().RealName}） {ToolHelper.GetDateValue(DateTime.Now, "yyyy年MM月dd日")} {System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.GetDayName(DateTime.Now.DayOfWeek)} " +
                $"农历{ToolHelper.GetChineseDateTime(DateTime.Now)}";
        }


        private void Frm_FirstPage_Load(object sender, EventArgs e)
        {
            LoadLastData();
            view.ClearSelection();
        }

        private void LoadLastData()
        {
            search.Properties.Items.Clear();
            string querySQL = "SELECT TOP(25) * FROM (" +
                "SELECT pi_id, pi_code, pi_name, pi_start_datetime, pi_year, pi_funds, pi_worker_date, pi_worker_id FROM project_info WHERE pi_categor = 2 " +
                "UNION ALL " +
                "SELECT ti_id, ti_code, ti_name, ti_start_datetime, ti_year, ti_funds, ti_worker_date, ti_worker_id FROM topic_info " +
                "UNION ALL " +
                "SELECT si_id, si_code, si_name, si_start_datetime, si_year, si_funds, si_worker_date, si_worker_id FROM subject_info) TB1 " +
                "ORDER BY TB1.pi_worker_date DESC";
            DataTable table = SqlHelper.ExecuteQuery(querySQL);
            int num = 1;
            foreach(DataRow row in table.Rows)
            {
                int i = view.Rows.Add();
                view.Rows[i].Cells["id"].Value = num++.ToString();
                view.Rows[i].Cells["code"].Value = row["pi_code"];
                view.Rows[i].Cells["name"].Value = row["pi_name"];
                view.Rows[i].Cells["sdate"].Value = ToolHelper.GetDateValue(row["pi_start_datetime"], "yyyy-MM-dd");
                view.Rows[i].Cells["year"].Value = row["pi_year"];
                view.Rows[i].Cells["fund"].Value = row["pi_funds"];
                view.Rows[i].Cells["idate"].Value = ToolHelper.GetDateValue(row["pi_worker_date"], "yyyy-MM-dd");
                view.Rows[i].Cells["user"].Value = UserHelper.GetUserNameById(row["pi_worker_id"]);
                search.Properties.Items.AddRange(new object[] { row["pi_code"], row["pi_name"] });
            }
        }
        private int count = -1;

        /// <summary>
        /// 检索索引
        /// </summary>
        public int Count { get => count; set => count = value; }

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
                Frm_MainFrame frm = new Frm_MainFrame(this, new Frm_CG());
                Hide();
                frm.Show();
            }
            else if("tbar_YJDJ".Equals(itemName))//移交登记
            {
                Frm_MainFrame frm = new Frm_MainFrame(this, new Frm_ToR());
                Hide();
                frm.Show();
            }
            else if("tbar_DAJS".Equals(itemName))//档案接收
            {
                Frm_MainFrame frm = new Frm_MainFrame(this, new Frm_DomAccept());
                Hide();
                frm.Show();
            }
            else if("tbar_DAZJ".Equals(itemName))//档案质检
            {
                Frm_MainFrame frm = new Frm_MainFrame(this, new Frm_QT());
                Hide();
                frm.Show();
            }
            
            else if("tbar_Manage".Equals(itemName))//后台管理
            {
                Frm_MainFrameManager frm = new Frm_MainFrameManager(UserHelper.GetUser());
                Hide();
                frm.ShowDialog();
            }
            else if("tbar_Count".Equals(itemName))//统计分析
            {
                Frm_Query frm = new Frm_Query(this);
                frm.ShowDialog();
            }
        }
    }
}
