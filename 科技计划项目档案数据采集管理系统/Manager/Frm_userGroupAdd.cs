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
               // Load_sq();
            }
            else
            {
               // Load_sq();
                LoadData(id);
            }
        }

        //加载授权模块
        //private void Load_sq()
        //{
        //    TreeNode rootNode = new TreeNode("平台菜单");
        //    string m_sql = $"select DISTINCT m_name from module order by m_sort";
        //    List<object[]> _obj = SqlHelper.ExecuteColumnsQuery(m_sql, 2);

        //    for (int i = 0; i < _obj.Count; i++)
        //    {
        //        TreeNode node = new TreeNode()
        //        {
        //           // Name = GetValue(_obj[i][0]),
        //            Text = GetValue(_obj[i][1])
        //        };
                
        //        TreeNode node2 = new TreeNode("查看");
        //        node2.Name = "view";
        //        TreeNode node3 = new TreeNode("修改");
        //        node3.Name = "edit";

        //        node.Nodes.Add(node2);
        //        node.Nodes.Add(node3);

        //        rootNode.Nodes.Add(node);
        //    }
        //    treeView1.Nodes.Add(rootNode);


        //}

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

               
                //string m_ids =_obj[4].ToString();
                //string[] mId_list = m_ids.Split(',');
                //StringBuilder mId_sb = new StringBuilder();
                //for (int i = 0; i < mId_list.Length; i++)
                //{
                //    mId_sb.Append($"'{mId_list[i]}'{(i == mId_list.Length - 1 ? string.Empty : ",")}");
                //}
                
                //TreeNode[] ts =  treeView1.Nodes.Find("id",true);
                //if (ts.Length > 0)
                //{
                //    TreeNode treeNode = ts[0];
                //    treeNode.Checked = true;
                //}


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

                //StringBuilder mId_sb = new StringBuilder();
                //StringBuilder oId_sb = new StringBuilder();
                //int index = 0;
                //int count = 0;
                //for (int i = 0; i < treeView1.Nodes.Count; i++)
                //{
                //    TreeNode root = treeView1.Nodes[i];
                //    //if (root.Checked)
                //    //{
                //    //    mId_sb.Append(root.Name + ",");
                //    //}

                //    for (int j = 0;j < root.Nodes.Count; j++)
                //    {
                //        TreeNode node = root.Nodes[j];
                //        if (node.Checked)
                //        {                          
                //           // mId_sb.Append($"{node.Name}{(index == root.Nodes.Count - 1 ? string.Empty : ",")}");
                                                                                   
                //            for (int k = 0; k < node.Nodes.Count; k++)
                //            {
                //                TreeNode node2 = node.Nodes[k];
                //                if (node2.Checked)
                //                {
                //                    count++;
                //                    oId_sb.Append($"{node2.Name}{(count == node.Nodes.Count - 1? string.Empty : ",")}");
                //                }
                //            }
                //        }                      
                //    }
                //}

               // Console.WriteLine("@ check m_id : "+mId_sb.ToString()+" -- o_id : "+oId_sb.ToString());

                //新增信息
                if (isAdd)
                {
                    string _ug_Id = Guid.NewGuid().ToString();
                    string querySql = $"insert into user_group " +
                        $"(ug_id,ug_name,ug_code,ug_note,ug_sort)" +
                        $"values" +
                        $"('{_ug_Id}','{_ug_name}','{_ug_code}','{_ug_note}','{_ug_sort}')";
                    SqlHelper.ExecuteQuery(querySql);

                    //if (string.IsNullOrEmpty(mId_sb.ToString()))
                    //{                      
                    //    string[] mId_list = mId_sb.ToString().Split(',');

                    //    StringBuilder m_id_sb = new StringBuilder();
                    //    for (int i = 0; i < mId_list.Length; i++)
                    //    {
                    //        m_id_sb.Append($"'{mId_list[i]}'{(i == mId_list.Length - 1 ? string.Empty : ",")}");
                    //    }




                    //}

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
            //MessageBox.Show(e.Node.Name);

            if (e.Node.IsSelected)
            {
                if (e.Node.Level == 2)
                {
                    e.Node.Checked = true;
                }

            }
        }
    }
}
