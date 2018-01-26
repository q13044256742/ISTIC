﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace 科技计划项目档案数据采集管理系统
{
    public partial class Frm_userInfo : Form
    {
        public Frm_userInfo(string name)
        {
            InitializeComponent();
            InitialForm();
        }

        //初始化表单内容
        private void InitialForm()
        {
            string querySql = $"SELECT u.ul_id, u.login_name as 登录名,u.real_name as 真实姓名,r.r_name as 角色,u.telephone as 联系电话,u.belong_unit as 所属单位 from user_list u left join role r on u.role_id = r.r_id ";
            DataTable dataTable = SqlHelper.ExecuteQuery(querySql);
            u_DataList.DataSource = dataTable;
            u_DataList.Columns["ul_id"].Visible = false;

            //string ul_id = u_DataList.Columns["ul_id"].ToString();
            //u_DataList.Tag = ul_id;
        }

        //查询
        private void U_btnSearch(object sender, EventArgs e)
        {
            int index = u_SearchType.SelectedIndex;
            string searchKey = u_SearchKey.Text;
            string queryKey = string.Empty;/*查询条件*/
            if (index == 0)
            {
                queryKey = "login_name";
            }
            else if (index == 1)
            {
                queryKey = "real_name";
            }
            else if (index == 2)
            {
                queryKey = "telephone";
            }
            else if (index == 3)
            {
                queryKey = "belong_unit";
            }


            if (searchKey != null)
            {
                string querySql = $"select u.login_name as 登录名,u.real_name as 真实姓名,r.r_name as 角色,u.telephone as 联系电话,u.belong_unit as 所属单位 from user_list u left join role r on u.role_id = r.r_id " +
               $" where {queryKey} like '%" + searchKey + "%'";
                u_DataList.DataSource = SqlHelper.ExecuteQuery(querySql);
            }
            else
            {
                string querySql = $"select u.login_name as 登录名,u.real_name as 真实姓名,r.r_name as 角色,u.telephone as 联系电话,u.belong_unit as 所属单位 from user_list u left join role r on u.role_id = r.r_id ";            
                u_DataList.DataSource = SqlHelper.ExecuteQuery(querySql);
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
            string sql = $"select u.login_name as 登录名,u.real_name as 真实姓名,r.r_name as 角色,u.telephone as 联系电话,u.belong_unit as 所属单位 from user_list u left join role r on u.role_id = r.r_id ";
            u_DataList.DataSource = SqlHelper.ExecuteQuery(sql);
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
    }
}
