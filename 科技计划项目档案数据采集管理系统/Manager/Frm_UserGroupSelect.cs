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
            List_all.Columns.Clear();
            List_all.Items.Clear();

            string sql = $"select belong_user_group_id from user_list where ul_id = '{userId}'";
            string userGroupId = GetValue(SqlHelper.ExecuteOnlyOneQuery(sql));
            DataTable dataTable = null;

            if(string.IsNullOrEmpty(userGroupId))
            {
                string _sql = $"select ug_id,ug_name from user_group";
                dataTable = SqlHelper.ExecuteQuery(_sql);
            }
            else
            {
                string[] userGroupId_list = userGroupId.Split(',');
                StringBuilder sb = new StringBuilder();
                for(int i = 0; i < userGroupId_list.Length; i++)
                {
                    sb.Append($"'{userGroupId_list[i]}'{(i == userGroupId_list.Length - 1 ? string.Empty : ",")}");
                }
                string _sql = $"select ug_id,ug_name from user_group where ug_id not in ({sb.ToString()})";
                dataTable = SqlHelper.ExecuteQuery(_sql);
            }

            List_all.Columns.AddRange(new ColumnHeader[]
            {
                new ColumnHeader{ Name = "id", Text = "主键" },
                new ColumnHeader{ Name = "type", Text = "用户组名称"}
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
            list_select.Columns.Clear();
            list_select.Items.Clear();

            string sql = $"select belong_user_group_id from user_list where ul_id = '{userId}'";
            string userGroupId =GetValue(SqlHelper.ExecuteOnlyOneQuery(sql));

            DataTable dataTable = null;
          
            string[] userGroupId_list = userGroupId.Split(',');
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < userGroupId_list.Length; i++)
            {
                sb.Append($"'{userGroupId_list[i]}'{(i == userGroupId_list.Length - 1 ? string.Empty : ",")}");
            }
            string _sql = $"select ug_id,ug_name from user_group where ug_id in ({sb.ToString()})";
            dataTable = SqlHelper.ExecuteQuery(_sql);

            //导航栏
            list_select.Columns.AddRange(new ColumnHeader[]
           {
                new ColumnHeader{ Name = "id", Text = "主键" },
                new ColumnHeader{ Name = "type", Text = "用户组名称"}
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

        //判空
        private string GetValue(object obj)
        {
            return obj == null ? string.Empty : obj.ToString();
        }

        //按钮右移>>>
        private void Btn_rightClick(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            string[] userGroupId_list = new string[0];
            //查询已有用户组
            string sql = $"select belong_user_group_id from user_list where ul_id = '{userId}'";
            string userGroupId = GetValue(SqlHelper.ExecuteOnlyOneQuery(sql));
                                
            if (!string.IsNullOrEmpty(userGroupId))
                userGroupId_list = userGroupId.Split(',');      

            for (int i = 0; i < userGroupId_list.Length; i++)
                sb.Append($"{userGroupId_list[i]}{(i == userGroupId_list.Length - 1 ? string.Empty : ",")}");

            //新选的用户组            
            int size = List_all.SelectedItems.Count;
            for(int i = 0; i < size; i++)
            {
                object groupId = List_all.SelectedItems[i].SubItems[0].Text;
                sb.Append($"{(userGroupId_list.Length == 0 ? string.Empty : ",")}{groupId}{(i == size - 1 ? string.Empty : ",")}");
            }
            string updateSql = $"UPDATE user_list SET belong_user_group_id='{sb.ToString()}' where ul_id='{userId}'";
            SqlHelper.ExecuteNonQuery(updateSql);
           
            LoadUserGroup_left(userId);
            LoadUserGroup(userId);
        }

        //按钮左移<<<
        private void Btn_leftClick(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
                      
            //移除选的belong_user_group_id
            int size = list_select.SelectedItems.Count;
            for(int i = 0; i < size; i++)
            {
                list_select.Items.Remove(list_select.SelectedItems[i]);
            }

            //获取剩下的belong_user_group_id
            int amount = list_select.Items.Count;
            for(int i = 0; i < amount; i++)
            {
                object id = list_select.Items[i].SubItems[0].Text;
                sb.Append($"{id}{(i == amount - 1 ? string.Empty : ",")}");
            }

            string updateSql = $"UPDATE user_list SET belong_user_group_id='{sb.ToString()}' where ul_id='{userId}'";
            SqlHelper.ExecuteNonQuery(updateSql);

            LoadUserGroup_left(userId);
            LoadUserGroup(userId);
        }
    }
}
