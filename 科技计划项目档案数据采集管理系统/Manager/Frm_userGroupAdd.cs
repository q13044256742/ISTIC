using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace 科技计划项目档案数据采集管理系统.Manager
{
    public partial class Frm_userGroupAdd : Form
    {
        private bool isAdd;
        private string id;

        public Frm_userGroupAdd(bool isAdd, string id)
        {
            InitializeComponent();
            this.isAdd = isAdd;
            this.id = id;

            if (!isAdd)
            {
                LoadData(id);
            }         
        }

        //把object对象转换为string
        private string GetValue(object obj)
        {
            return obj == null ? string.Empty : obj.ToString();
        }

        //加载更新表单
        private void LoadData(string id)
        {
            string sql = $"select ug_name,ug_note,ug_sort" +
              $" from user_group where ug_id = '{id}'";
            object[] _obj = SqlHelper.ExecuteRowsQuery(sql);

            if (_obj != null)
            {
                ug_name.Text = _obj[0].ToString();             
                ug_note.Text = _obj[1].ToString();
                ug_sort.Text = _obj[2].ToString();

                ug_name.Tag = id;
            }      
        }

        //保存
        private void UserGroup_btnSave(object sender, EventArgs e)
        {
            if (!ValidData())
            {
                return;
            }
            if (MessageBox.Show("确定要保存当前数据吗?", "确认提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk) == DialogResult.OK)
            {
                //保存基本信息
                string _ug_name = ug_name.Text.Trim();
               // string _ug_code = ug_code.Text.Trim();            
                string _ug_note = ug_note.Text.Trim();
                string _ug_sort = ug_sort.Text.Trim();

                //新增信息
                if (isAdd)
                {
                    string _ug_Id = Guid.NewGuid().ToString();
                    string _ug_code = Guid.NewGuid().ToString();
                    string querySql = $"insert into user_group " +
                        $"(ug_id,ug_name,ug_code,ug_note,ug_sort)" +
                        $"values" +
                        $"('{_ug_Id}','{_ug_name}','{_ug_code}','{_ug_note}','{_ug_sort}')";
                    SqlHelper.ExecuteQuery(querySql);
                }
                //更新信息
                else
                {
                    string _ug_Id = ug_name.Tag.ToString();
                    string querySql = $"update user_group set ug_name='{_ug_name}',ug_note='{_ug_note}',ug_sort='{_ug_sort}'" +
                        $" where ug_id='{_ug_Id}'";
                    SqlHelper.ExecuteQuery(querySql);
                }
                if (MessageBox.Show((isAdd ? "添加" : "更新") + "成功，是否返回列表页", "恭喜", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk) == DialogResult.Yes)
                {
                    DialogResult = DialogResult.OK;
                    Close();
                }
            }
        }

        //取消
        private void UserGroup_btnClose(object sender, EventArgs e)
        {
            Close();
        }

        // 检验数据的完整性
        private bool ValidData()
        {
            if (string.IsNullOrEmpty(ug_name.Text.Trim()))
            {
                MessageBox.Show("请输入用户组名称", "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
              
            return true;
        }     
    }
}
