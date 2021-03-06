﻿using System;
using System.Windows.Forms;

namespace 科技计划项目档案数据采集管理系统
{
    public partial class Frm_userInfo : DevExpress.XtraEditors.XtraForm
    {
        public Frm_userInfo()
        {
            InitializeComponent();
        }

        //查询
        private void U_btnSearch(object sender, EventArgs e)
        {
            string key = u_SearchKey.Text;
            if (!string.IsNullOrEmpty(key))
            {
                u_DataList.ClearSelection();
                foreach (DataGridViewRow row in u_DataList.Rows)
                {
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        if (ToolHelper.GetValue(cell.Value).Contains(key))
                        {
                            cell.Selected = true;
                            return;
                        }
                    }
                }
            }
        }

        //新增
        private void U_btnAdd(object sender, EventArgs e)
        {
            Manager.Frm_userInfoAdd frm = new Manager.Frm_userInfoAdd(true,null);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadUserDataScoure();
            }
        }

        //加载实时数据
        private void LoadUserDataScoure()
        {
            string sql = $"select u.ul_id,u.login_name as 登录名,u.real_name as 真实姓名,d.dd_name as 角色,u.telephone as 联系电话,u.belong_unit as 所属单位 from user_list u left join data_dictionary d on u.role_id = d.dd_id";
            u_DataList.DataSource = SqlHelper.ExecuteQuery(sql);
            u_DataList.Columns["ul_id"].Visible = false;
            u_SearchKey.Text = null;
        }

        //删除
        private void U_btnDel(object sender, EventArgs e)
        {
            int amount = u_DataList.SelectedRows.Count;
            if (amount > 0)
            {

                if (MessageBox.Show("确定要删除选中的数据吗?", "确认提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk) == DialogResult.OK)
                {
                    int deleteAmount = 0;
                    foreach (DataGridViewRow row in u_DataList.SelectedRows)
                    {
                        string id = row.Cells["ul_id"].Value.ToString();                                            
                        string deleteSql = $"DELETE FROM user_list WHERE ul_id = '{id}'";
                        SqlHelper.ExecuteNonQuery(deleteSql);
                      
                        deleteAmount++;
                    }                  
                    LoadUserDataScoure();
                    MessageBox.Show(deleteAmount + "条数据已被删除!", "操作成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("请先至少选择一条要删除的数据!", "尚未选择数据", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }

        //更新
        private void U_btnUpdate(object sender, EventArgs e)
        {           
            int amount = u_DataList.SelectedRows.Count;
            if (amount == 1)
            {
                //获取你所选行的id
                string id = u_DataList.SelectedRows[0].Cells["ul_id"].Value.ToString();
                Manager.Frm_userInfoAdd frm = new Manager.Frm_userInfoAdd(false, id);
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    LoadUserDataScoure();
                }
            }
            else
            {
                MessageBox.Show("请先选择一条要修改的数据!", "尚未选择数据", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }

        private void Frm_userInfo_Load(object sender, EventArgs e)
        {
            LoadUserDataScoure();
        }
    }
}
