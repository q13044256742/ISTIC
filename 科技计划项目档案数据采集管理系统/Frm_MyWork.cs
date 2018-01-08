using System;
using System.Collections.Generic;
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
        List<TabPage> tabList = new List<TabPage>();
        private void InitialForm(string planName)
        {
            foreach (TabPage tab in tab_MenuList.TabPages)
            {
                tabList.Add(tab);
                tab_MenuList.TabPages.Remove(tab);
            }
            ShowTab(false, "plan");
            lbl_PlanName.Text = planName;
            tv_DataList.Nodes[0].Text = planName;
        }

        /// <summary>
        /// 展示指定文本的tab页面（隐藏其他页面）
        /// </summary>
        /// <param name="v">要展示页面的Name</param>
        private void ShowTab(bool clear, string name)
        {
            if (clear)
                tab_MenuList.TabPages.Clear();
            for (int i = 0; i < tabList.Count; i++)
            {
                if (tabList[i].Name.Equals(name))
                {
                    tab_MenuList.TabPages.Add(tabList[i]);
                    break;
                }
            }
        }

        private void cbo_JH_Next_SelectedIndexChanged(object sender, EventArgs e)
        {
            tv_DataList.Nodes[0].Nodes.Clear();
            tv_DataList.Nodes[0].ExpandAll();
            if (cbo_JH_Next.SelectedIndex == 1)
            {
                ShowTab(false, "project");
            }
            else if (cbo_JH_Next.SelectedIndex == 2)
            {
            }
        }

        private void tv_DataList_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if ("项目".Equals(tv_DataList.SelectedNode.Text))
            {
               
            }
        }

        private void Frm_MyWork_Load(object sender, EventArgs e)
        {
            cbo_JH_Next.SelectedIndex = 0;
        }
    }
}
