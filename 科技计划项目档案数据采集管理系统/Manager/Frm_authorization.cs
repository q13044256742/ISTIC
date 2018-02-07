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
    public partial class Frm_authorization : Form
    {
        private string userId;
        public Frm_authorization(string userId)
        {
            this.userId = userId;
            InitializeComponent();
            Load_sq();
        }

        //加载授权模块
        private void Load_sq()
        {
            TreeNode rootNode = new TreeNode("平台菜单");
            //模块数据（state 为MODULE） 
            string m_sql = $"select m_id,m_name from module where state = 'MODULE' order by m_sort";
            List<object[]> _obj = SqlHelper.ExecuteColumnsQuery(m_sql, 2);

            for (int i = 0; i < _obj.Count; i++)
            {
                TreeNode node = new TreeNode()
                {
                    Name = GetValue(_obj[i][0]),
                    Text = GetValue(_obj[i][1])
                };

                TreeNode node2 = new TreeNode("查看");
                node2.Name = "view";
                TreeNode node3 = new TreeNode("修改");
                node3.Name = "edit";
                TreeNode node4 = new TreeNode("删除");
                node4.Name = "del";

                node.Nodes.Add(node2);
                node.Nodes.Add(node3);
                node.Nodes.Add(node4);

                rootNode.Nodes.Add(node);
            }

            treeView1.Nodes.Add(rootNode);
            treeView1.ExpandAll();

            //初始化状态
            string sql = $"select m_name,m_id from module where userGroup_id = '{userId}'";
            List<object[]> select_module_list = SqlHelper.ExecuteColumnsQuery(sql, 2);

            if (select_module_list.Count != 0)
            {
                TreeNode root = treeView1.Nodes[0];
                root.Checked = true;
                for (int i=0;i < select_module_list.Count;i++)
                {
                    TreeNode node = root.Nodes[i];

                    // if (node.Name.Equals(GetValue(select_module_list[i][1])))

                    TreeNode[] nodes = node.Nodes.Find(GetValue(select_module_list[i][1]),true);
                    for (int a = 0;a < nodes.Length;a++)
                    {
                        nodes[a].Checked = true;
                    }
                   // node.Checked = true;

                   



                        //string module_id = GetValue(select_module_list[i][1]);

                        //string o_sql = $"select o_view,o_edit,o_del from operation where module_id = '{module_id}'";
                        //List<object[]> select_operation_list = SqlHelper.ExecuteColumnsQuery(o_sql, 3);

                        //if (select_operation_list.Count != 0)
                        //{
                        //    for (int j = 0;j < select_operation_list.Count;j++)
                        //    {
                        //        TreeNode node2 = node.Nodes[j];                        
                        //        node2.Checked = true;                                                                                                                                  
                        //    }
                        //}


                   
                }
            }                                   
        }

        //把object对象转换为string
        private string GetValue(object obj)
        {
            return obj == null ? string.Empty : obj.ToString();
        }
     
        //保存
        private void Btn_save_sq(object sender, EventArgs e)
        {
            //删除之前选中状态的表
            //删除模块（module）表
            string m_sql = $"select m_id from module where userGroup_id ='{userId}'";
            List<object[]> m_ids = SqlHelper.ExecuteColumnsQuery(m_sql, 1);
            if (m_ids != null)
            {
                for (int i = 0; i < m_ids.Count; i++)
                {
                    string m_id = GetValue(m_ids[i][0]);
                    string del_m_sql = $"delete from module where m_id = '{m_id}'";
                    SqlHelper.ExecuteQuery(del_m_sql);

                    //删除权限（operation）表
                    string op_sql = $"select o_id from operation where module_id = '{m_id}'";
                    List<object[]> o_ids = SqlHelper.ExecuteColumnsQuery(op_sql, 1);
                    if (o_ids != null)
                    {
                        for (int j = 0; j < o_ids.Count; j++)
                        {
                            string o_id = GetValue(o_ids[j][0]);
                            string del_o_sql = $"delete from operation where o_id = '{o_id}'";
                            SqlHelper.ExecuteQuery(del_o_sql);
                        }
                    }
                }
            }

            //添加新选中的数据
            for (int i = 0; i < treeView1.Nodes.Count; i++)
            {
                TreeNode root = treeView1.Nodes[i];             

                for (int j = 0; j < root.Nodes.Count; j++)
                {
                    TreeNode node = root.Nodes[j];
                    if (node.Checked)
                    {
                        string userGroup_id = userId;
                        string m_id = Guid.NewGuid().ToString();
                        string m_name = node.Text;

                        string code_sql = $"select m_code from module where m_id = '{node.Name}'";
                        string m_code = GetValue(SqlHelper.ExecuteOnlyOneQuery(code_sql)); 

                        //string m_code = node.Name;
                       
                        string node_sql = $"insert into module (m_id,m_name,m_code,userGroup_id)values('{m_id}','{m_name}','{m_code}','{userGroup_id}')";
                        SqlHelper.ExecuteQuery(node_sql);

                        for (int k = 0; k < node.Nodes.Count; k++)
                        {
                            TreeNode node2 = node.Nodes[k];
                            if (node2.Checked)
                            {
                                string o_id = Guid.NewGuid().ToString();
                                if ("view".Equals(node2.Name))
                                {
                                    string sql = $"insert into operation (o_id,o_view,o_edit,module_id,o_del)values('{o_id}','1','0','{m_id}','0')";
                                    SqlHelper.ExecuteQuery(sql);
                                }
                                else if ("edit".Equals(node2.Name))
                                {
                                    string sql = $"insert into operation (o_id,o_view,o_edit,module_id,o_del)values('{o_id}','0','1','{m_id}','0')";
                                    SqlHelper.ExecuteQuery(sql);
                                }
                                else if ("del".Equals(node2.Name))
                                {
                                    string sql = $"insert into operation (o_id,o_view,o_edit,module_id,o_del)values('{o_id}','0','0','{m_id}','1')";
                                    SqlHelper.ExecuteQuery(sql);
                                }
                            }
                        }
                    }
                }
            }

            MessageBox.Show("授权成功!", "操作提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            Close();               
        }

        //取消
        private void Btn_close_sq(object sender, EventArgs e)
        {
            Close();
        }

        //鼠标点击事件
        private void treeView_MouseClick(object sender, MouseEventArgs e)
        {
            TreeNode node = treeView1.GetNodeAt(new Point(e.X, e.Y));
            if (node != null)
            {
                ChangeChild(node, node.Checked);//影响子节点  
                ChangeParent(node);//影响父节点  
            }
        }

        //递归子节点跟随其全选或全不选  
        private void ChangeChild(TreeNode node, bool state)
        {
            node.Checked = state;
            foreach (TreeNode tn in node.Nodes)
            {
                ChangeChild(tn, state);
            }
        }

        //递归父节点跟随其全选或全不选  
        private void ChangeParent(TreeNode node)
        {
            if (node.Parent != null)
            {
                //兄弟节点被选中的个数   
                int brotherNodeCheckedCount = 0;

                //遍历该节点的兄弟节点   
                foreach (TreeNode tn in node.Parent.Nodes)
                {
                    if (tn.Checked == true)
                    {
                        brotherNodeCheckedCount++;
                    }
                }

                //兄弟节点全没选，其父节点也不选   
                if (brotherNodeCheckedCount == 0)
                {
                    node.Parent.Checked = false;
                    ChangeParent(node.Parent);
                }

                //兄弟节点只要有一个被选，其父节点也被选   
                if (brotherNodeCheckedCount >= 1)
                {
                    node.Parent.Checked = true;
                    ChangeParent(node.Parent);
                }
            }
        }
    }
}
