using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace 科技计划项目档案数据采集管理系统
{
    public partial class Frm_userGroup : DevExpress.XtraEditors.XtraForm
    {
        public Frm_userGroup()
        {
            InitializeComponent();
            LoadUserGroupDataScoure();
        }

        //加载实时数据
        private void LoadUserGroupDataScoure()
        {
            string sql = $"select ug_id,ug_name as 用户组名称, ug_note as 说明,ug_sort as 排序 from user_group order by ug_sort";
            userGroup_DataList.DataSource = SqlHelper.ExecuteQuery(sql);
            userGroup_DataList.Columns["ug_id"].Visible = false;
            userGroup_SearchKey.Text = null;
        }

        //查询
        private void UG_btnSearch(object sender, EventArgs e)
        {

        }

        //添加
        private void UG_btnAdd(object sender, EventArgs e)
        {       
            Manager.Frm_userGroupAdd frm = new Manager.Frm_userGroupAdd(true,null);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadUserGroupDataScoure();
            }
        }

        //更新
        private void UG_btnUpdate(object sender, EventArgs e)
        {
            int amount = userGroup_DataList.SelectedRows.Count;
            if (amount == 1)
            {
                //获取你所选行的id
                string id = userGroup_DataList.SelectedRows[0].Cells["ug_id"].Value.ToString();
               
                Manager.Frm_userGroupAdd frm = new Manager.Frm_userGroupAdd(false, id);
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    LoadUserGroupDataScoure();
                }
            }
            else
            {
                MessageBox.Show("请先选择一条要修改的数据!", "尚未选择数据", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }

        //删除
        private void UG_btnDel(object sender, EventArgs e)
        {
            int amount = userGroup_DataList.SelectedRows.Count;
            if (amount > 0)
            {

                if (MessageBox.Show("确定要删除选中的数据吗?", "确认提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk) == DialogResult.OK)
                {
                    int deleteAmount = 0;
                    foreach (DataGridViewRow row in userGroup_DataList.SelectedRows)
                    {
                        //删除用户组（user_group）表
                        string id = row.Cells["ug_id"].Value.ToString();
                        string deleteSql = $"DELETE FROM user_group WHERE ug_id = '{id}'";
                        SqlHelper.ExecuteNonQuery(deleteSql);

                        //删除模块（module）表
                        string m_sql = $"select m_id from module where userGroup_id ='{id}'";
                        List<object[]> m_ids = SqlHelper.ExecuteColumnsQuery(m_sql,1);
                        if (m_ids != null)
                        {
                            for (int i = 0;i < m_ids.Count;i++)
                            {
                                string m_id = GetValue(m_ids[i][0]);
                                string del_m_sql = $"delete from module where m_id = '{m_id}'";
                                SqlHelper.ExecuteQuery(del_m_sql);

                                //删除权限（operation）表
                                string op_sql = $"select o_id from operation where module_id = '{m_id}'";
                                List<object[]> o_ids = SqlHelper.ExecuteColumnsQuery(op_sql, 1);
                                if (o_ids != null)
                                {
                                    for (int j = 0;j < o_ids.Count;j++)
                                    {
                                        string o_id = GetValue(o_ids[j][0]);
                                        string del_o_sql = $"delete from operation where o_id = '{o_id}'";
                                        SqlHelper.ExecuteQuery(del_o_sql);
                                    }
                                }
                            }                                                    
                        }

                        deleteAmount++;
                    }
                    LoadUserGroupDataScoure();
                    MessageBox.Show(deleteAmount + "条数据已被删除!", "操作成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("请先至少选择一条要删除的数据!", "尚未选择数据", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }

        //授权
        private void Btn_authorization(object sender, EventArgs e)
        {
            int amount = userGroup_DataList.SelectedRows.Count;
            if (amount == 1)
            {
                //获取你所选行的id
                string id = userGroup_DataList.SelectedRows[0].Cells["ug_id"].Value.ToString();

                Manager.Frm_authorization frm = new Manager.Frm_authorization(id);
                frm.ShowDialog();

                //加载实时数据
                LoadUserGroupDataScoure();
            }
            else
            {
                MessageBox.Show("请先选择一条数据!", "尚未选择数据", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }


        //把object对象转换为string
        private string GetValue(object obj)
        {
            return obj == null ? string.Empty : obj.ToString();
        }
    }
}
