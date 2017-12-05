using System;
using System.Windows.Forms;

namespace 科技计划项目档案数据采集管理系统
{
    public partial class Frm_MyWork : Form
    {
        public Frm_MyWork(string planName)
        {
            InitializeComponent();
            InitialForm(planName);
        }

        private void InitialForm(string planName)
        {
            showTab(0);
            lbl_PlanName.Text = planName;
            tv_DataList.Nodes[0].Text = planName;
        }

        /// <summary>
        /// 展示指定文本的tab页面（隐藏其他页面）
        /// </summary>
        /// <param name="v">要展示页面的Text</param>
        private void showTab(int index)
        {
            tab_MenuList.TabPages[index].Select();
            //foreach (TabPage item in tab_MenuList.TabPages)
            //{
            //    if (!tabName.Equals(item.Name))
            //    {
            //        tab_MenuList.TabPages.Remove(tab_MenuList.TabPages[item.Name]);
            //    }
            //    else
            //    {
            //        //item.Parent = tab_MenuList;
            //        item.Select();
            //    }
            //}
        }

        private void cbo_JH_Next_SelectedIndexChanged(object sender, EventArgs e)
        {
            tv_DataList.Nodes[0].Nodes.Clear();
            tv_DataList.Nodes[0].ExpandAll();
            if(cbo_JH_Next.SelectedIndex == 0)
            {
                TreeNode treeNode = new TreeNode("项目");
                treeNode.Name = "xm";
                tv_DataList.Nodes[0].Nodes.Add(treeNode);
                tv_DataList.SelectedNode = treeNode;
            }else if(cbo_JH_Next.SelectedIndex == 1)
            {
                TreeNode treeNode = new TreeNode("课题");
                treeNode.Name = "kt";
                tv_DataList.Nodes[0].Nodes.Add(treeNode);
                tv_DataList.SelectedNode = treeNode;
            }
        }

        private void tv_DataList_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if ("项目".Equals(tv_DataList.SelectedNode.Text))
            {
                showTab(1);
            }
        }
    }
}
