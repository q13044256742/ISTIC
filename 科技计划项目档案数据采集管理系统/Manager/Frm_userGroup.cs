using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace 科技计划项目档案数据采集管理系统
{
    public partial class Frm_userGroup : Form
    {
        public Frm_userGroup(string name)
        {
            InitializeComponent();
            LoadUserGroupDataScoure();
        }

        //加载实时数据
        private void LoadUserGroupDataScoure()
        {
            string sql = $"select ug_id,ug_name as 用户组名称,ug_code as 编码, ug_note as 说明,ug_sort as 排序 from user_group";
            userGroup_DataList.DataSource = SqlHelper.ExecuteQuery(sql);
            userGroup_DataList.Columns["ug_id"].Visible = false;
            userGroup_SearchKey.Text = null;
        }

        //查询
        private void UG_btnSearch(object sender, EventArgs e)
        {
            int index = userGroup_SearchType.SelectedIndex;
            string searchKey = userGroup_SearchKey.Text;
            string queryKey = string.Empty;/*查询条件*/
            if (index == 0)
            {
                queryKey = "ug_name";
            }
            else if (index == 1)
            {
                queryKey = "ug_code";
            }
            else if (index == 2)
            {
                queryKey = "ug_note";
            }

            if (!string.IsNullOrEmpty(queryKey))
            {
                if (!string.IsNullOrEmpty(searchKey))
                {
                    string querySql = $"select ug_id,ug_name as 用户组名称,ug_code as 编码, ug_note as 说明,ug_sort as 排序 from user_group" +
                   $" where {queryKey} like '%" + searchKey + "%'";
                    userGroup_DataList.DataSource = SqlHelper.ExecuteQuery(querySql);
                    userGroup_DataList.Columns["ug_id"].Visible = false;
                }
                else
                {
                    string querySql = $"select ug_id,ug_name as 用户组名称,ug_code as 编码, ug_note as 说明,ug_sort as 排序 from user_group";
                    userGroup_DataList.DataSource = SqlHelper.ExecuteQuery(querySql);
                    userGroup_DataList.Columns["ug_id"].Visible = false;
                }
            }

        }

        //添加
        private void UG_btnAdd(object sender, EventArgs e)
        {

        }

        //更新
        private void UG_btnUpdate(object sender, EventArgs e)
        {

        }

        //删除
        private void UG_btnDel(object sender, EventArgs e)
        {

        }
    }
}
