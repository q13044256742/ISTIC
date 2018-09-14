using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace 科技计划项目档案数据采集管理系统
{
    public partial class Frm_userGroup : DevExpress.XtraEditors.XtraForm
    {
        public Frm_userGroup()
        {
            InitializeComponent();
           
        }

        //加载实时数据
        private void LoadUserGroupDataScoure()
        {
            string key = "dic_key_role";
            string querySql = $"SELECT * FROM data_dictionary WHERE dd_pId=(SELECT TOP(1) dd_id FROM data_dictionary " +
                $"WHERE dd_code='{key}') ORDER BY dd_sort";
            DataTable table = SqlHelper.ExecuteQuery(querySql);
            view.Rows.Clear();
            foreach(DataRow row in table.Rows)
            {
                int i = view.Rows.Add();
                view.Rows[i].Tag = row["dd_id"];
                view.Rows[i].Cells["id"].Value = i + 1;
                view.Rows[i].Cells["name"].Value = row["dd_name"];
                view.Rows[i].Cells["code"].Value = row["dd_code"];
                view.Rows[i].Cells["note"].Value = row["dd_note"];
                view.Rows[i].Cells["sort"].Value = row["dd_sort"];
            }
        }

        //查询
        private void UG_btnSearch(object sender, EventArgs e)
        {

        }

        //删除
        private void UG_btnDel(object sender, EventArgs e)
        {
            int amount = view.SelectedRows.Count;
            if (amount > 0)
            {

                if (MessageBox.Show("确定要删除选中的数据吗?", "确认提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk) == DialogResult.OK)
                {
                    int deleteAmount = 0;
                    foreach (DataGridViewRow row in view.SelectedRows)
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
            int amount = view.SelectedRows.Count;
            if (amount == 1)
            {
                //获取你所选行的id
                object RoleId = view.SelectedRows[0].Tag;
                object RoleName = view.SelectedRows[0].Cells["name"].Value;

                Manager.Frm_Authoriz frm = new Manager.Frm_Authoriz(RoleId, RoleName);
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

        private void Frm_userGroup_Load(object sender, EventArgs e)
        {
            LoadUserGroupDataScoure();
        }
    }
}
