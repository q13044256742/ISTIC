using System;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace 科技计划项目档案数据采集管理系统.Manager
{
    public partial class Frm_UserGroupSelect : Form
    {
        private object userId;
        public Frm_UserGroupSelect(string userId)
        {
            this.userId = userId;
            InitializeComponent();
            LoadUserGroup();
            LoadUserGroup(userId);
        }

        private void LoadUserGroup()
        {
            string sql = $"select ug_id,ug_name from user_group order by ug_sort";
            DataTable dataTable = SqlHelper.ExecuteQuery(sql);

            List_all.Columns.AddRange(new ColumnHeader[]
            {
                new ColumnHeader{ Name = "id", Text = "主键" },
                new ColumnHeader{ Name = "type", Text = "文件类别" }
            });
            for(int i = 0; i < dataTable.Rows.Count; i++)
            {
                ListViewItem item = List_all.Items.Add(GetValue(dataTable.Rows[i]["ug_id"]));
                item.SubItems.Add(GetValue(dataTable.Rows[i]["ug_name"]));
            }
            List_all.Columns["id"].Width = 0;

        }
        private void LoadUserGroup(object userId)
        {
            

        }
        private void Ug_seclect_btnSave(object sender, EventArgs e)
        {

        }

        private void Ug_select_btnClose(object sender, EventArgs e)
        {
            Close();
        }

        private string GetValue(object obj)
        {
            return obj == null ? string.Empty : obj.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            int size = List_all.SelectedItems.Count;
            for(int i = 0; i < size; i++)
            {
                object groupId = List_all.SelectedItems[i].SubItems["id"].Text;
                sb.Append($"{groupId}{(i == size - 1 ? string.Empty : ",")}");
            }
            string updateSql = $"UPDATE .. SET usergoupid='{sb.ToString()}' where userid='{userId}'";
            SqlHelper.ExecuteNonQuery(updateSql);

            LoadUserGroup(userId);
        }
    }
}
