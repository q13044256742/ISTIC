using System;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace 科技计划项目档案数据采集管理系统.Manager
{
    public partial class Frm_UserGroupSelect : Form
    {
        private string userId;
        public Frm_UserGroupSelect(string userId)
        {
            this.userId = userId;
            InitializeComponent();
            LoadUserGroup_left(userId);
            LoadUserGroup(userId);
        }


        //左边
        private void LoadUserGroup_left(string userId)
        {
           
            string sql = $"select belong_user_group_id from user_list where ul_id = '{userId}'";
            string userGroupId = SqlHelper.ExecuteOnlyOneQuery(sql).ToString();

            DataTable dataTable = null;
            string[] userGroupId_list = userGroupId.Split(',');
            for (int i = 0; i < userGroupId_list.Length; i++)
            {
                string _sql = $"select ug_id,ug_name from user_group where ug_id != '{userGroupId_list[i]}'";
                dataTable = SqlHelper.ExecuteQuery(_sql);
            }


            List_all.Columns.AddRange(new ColumnHeader[]
            {
                new ColumnHeader{ Name = "id", Text = "主键" },
                new ColumnHeader{ Name = "type", Text = "用户组名称" }
            });
            for(int i = 0; i < dataTable.Rows.Count; i++)
            {
                ListViewItem item = List_all.Items.Add(GetValue(dataTable.Rows[i]["ug_id"]));
                item.SubItems.Add(GetValue(dataTable.Rows[i]["ug_name"]));
            }
            List_all.Columns["id"].Width = 0;
        }

        //右边
        private void LoadUserGroup(string userId)
        {
            string sql = $"select belong_user_group_id from user_list where ul_id = '{userId}'";
            string str_userGroupId = SqlHelper.ExecuteOnlyOneQuery(sql).ToString();

            DataTable dataTable = null;
            string[] userGroupId_list = str_userGroupId.Split(',');
            for (int i= 0;i<userGroupId_list.Length;i++)
            {
                string _sql = $"select ug_id,ug_name from user_group where ug_id = '{userGroupId_list[i]}'";
                dataTable = SqlHelper.ExecuteQuery(_sql);
            }          

            //导航栏
            list_select.Columns.AddRange(new ColumnHeader[]
           {
                new ColumnHeader{ Name = "id", Text = "主键" },
                new ColumnHeader{ Name = "type", Text = "用户组名称" }
           });

            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                ListViewItem item = list_select.Items.Add(GetValue(dataTable.Rows[i]["ug_id"]));
                item.SubItems.Add(GetValue(dataTable.Rows[i]["ug_name"]));
            }

            //隐藏主键
            list_select.Columns["id"].Width = 0;
        }
        private void Ug_seclect_btnSave(object sender, EventArgs e)
        {
            Close();
        }

        private void Ug_select_btnClose(object sender, EventArgs e)
        {
            Close();
        }

        private string GetValue(object obj)
        {
            return obj == null ? string.Empty : obj.ToString();
        }

        //按钮》》》
        private void button1_Click(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            int size = List_all.SelectedItems.Count;
            for(int i = 0; i < size; i++)
            {
                object groupId = List_all.SelectedItems[i].SubItems["id"].Text;
                sb.Append($"{groupId}{(i == size - 1 ? string.Empty : ",")}");
            }
            string updateSql = $"UPDATE user_list SET belong_user_goup_id='{sb.ToString()}' where ul_id='{userId}'";
            SqlHelper.ExecuteNonQuery(updateSql);

            LoadUserGroup(userId);
        }
    }
}
