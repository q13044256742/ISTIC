using System.Data;
using System.Windows.Forms;

namespace 科技计划项目档案数据采集管理系统
{
    public partial class Frm_LogList : Form
    {
        public Frm_LogList()
        {
            InitializeComponent();
        }

        private void Frm_LogList_Load(object sender, System.EventArgs e)
        {
            LoadLoginLog(null);
        }

        private void LoadLoginLog(string key)
        {
            view.Rows.Clear();
            searchControl.Properties.Items.Clear();
            string querySQL = $"SELECT TOP(1000) sll.*, ul.real_name FROM sys_login_log sll " +
                $"LEFT JOIN user_list ul ON sll_user_id = ul_id WHERE 1=1 ";
            if(key != null)
            {
                querySQL += $" AND ul.real_name LIKE '%{key}%' ";
            }
            querySQL += "ORDER BY CONVERT(DATETIME, sll_online_date) DESC";
            DataTable table = SqlHelper.ExecuteQuery(querySQL);
            foreach(DataRow row in table.Rows)
            {
                int i = view.Rows.Add();
                view.Rows[i].Cells["id"].Value = i + 1;
                view.Rows[i].Cells["username"].Value = row["real_name"];
                view.Rows[i].Cells["ipaddress"].Value = row["sll_ipaddress"];
                view.Rows[i].Cells["login"].Value = ToolHelper.GetDateValue(row["sll_online_date"], "yyyy-MM-dd HH:mm:dd");
                view.Rows[i].Cells["logout"].Value = ToolHelper.GetDateValue(row["sll_offline_date"], "yyyy-MM-dd HH:mm:dd");

                searchControl.Properties.Items.Add(row["real_name"]);
            }
        }

        private void btn_Query_Click(object sender, System.EventArgs e)
        {
            string key = searchControl.Text;
            if(!string.IsNullOrEmpty(key))
            {
                LoadLoginLog(key);
            }
        }
    }
}
