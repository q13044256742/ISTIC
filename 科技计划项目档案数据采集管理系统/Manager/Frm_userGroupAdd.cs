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
            if (isAdd)
            {
                Load_sq();
            }
            else
            {
                Load_sq();
                LoadData(id);
            }
        }

        //加载授权模块
        private void Load_sq()
        {
            TreeNode rootNode = new TreeNode("平台菜单");
            string m_sql = $"select m_id,m_name from module order by m_sort";
            object[] _obj = SqlHelper.ExecuteRowsQuery(m_sql);

            if (_obj != null)
            {
                for (int i = 0; i < _obj.Length; i++)
                {
                    TreeNode node = new TreeNode()
                    {
                        Name = GetValue(_obj[0]),
                        Text = GetValue(_obj[1])
                    };
                    string o_sql = $"select o_id,o_name from operation order by o_sort";
                    object[] _obj2 = SqlHelper.ExecuteRowsQuery(o_sql);
                    if (_obj2 != null)
                    {
                        for (int j = 0; j < _obj2.Length; j++)
                        {
                            TreeNode node2 = new TreeNode()
                            {
                                Name = GetValue(_obj2[0]),
                                Text = GetValue(_obj2[1])
                            };
                            node.Nodes.Add(node2);
                        }
                        rootNode.Nodes.Add(node);
                    }
                    rootNode.Nodes.Add(node);
                }
                treeView1.Nodes.Add(rootNode);
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
            string sql = $"select ug_name,ug_code,ug_note,ug_sort" +
              $" from user_group where ug_id = '{id}'";
            object[] _obj = SqlHelper.ExecuteRowsQuery(sql);

            if (_obj != null)
            {
                ug_name.Text = _obj[0].ToString();
                ug_code.Text = _obj[1].ToString();
                ug_note.Text = _obj[2].ToString();
                ug_sort.Text = _obj[3].ToString();

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
                string _ug_code = ug_code.Text.Trim();            
                string _ug_note = ug_note.Text.Trim();
                string _ug_sort = ug_sort.Text.Trim();

                // string _ug_module_id = 
               
                //新增信息
                if (isAdd)
                {
                    string _ug_Id = Guid.NewGuid().ToString();
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
                    string querySql = $"update user_group set ug_name='{_ug_name}',ug_code='{_ug_code}',ug_note='{_ug_code}',ug_sort='{_ug_sort}'" +
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
            else if (string.IsNullOrEmpty(ug_code.Text.Trim()))
            {
                MessageBox.Show("请输入编码", "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }          
            return true;
        }     

        //加载说选的节点
        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            MessageBox.Show(e.Node.Name);

            if ("查看".Equals(e.Node.Name))
            {

            }


        }
    }
}
