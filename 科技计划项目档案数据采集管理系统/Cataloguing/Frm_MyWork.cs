using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace 科技计划项目档案数据采集管理系统
{
    public partial class Frm_MyWork : Form
    {
        /// <summary>
        /// 开始加工指定的对象
        /// </summary>
        /// <param name="workType">对象类型</param>
        /// <param name="objId">对象主键</param>
        public Frm_MyWork(WorkType workType, object objId)
        {
            InitializeComponent();
            InitialForm("DEMO");
        }

        /// <summary>
        /// 开始加工指定的对象
        /// </summary>
        /// <param name="workType">对象类型</param>
        /// <param name="objId">对象主键</param>
        /// <param name="planId">计划主键（仅针对光盘/批次加工）</param>
        public Frm_MyWork(WorkType workType, object objId, object planId)
        {
            InitializeComponent();
            InitialForm(planId);
        }

        List<TabPage> tabList = new List<TabPage>();
        /// <summary>
        /// 初始化选项卡
        /// </summary>
        /// <param name="planId">计划ID（仅针对纸本/光盘加工）</param>
        private void InitialForm(object planId)
        {
            foreach (TabPage tab in tab_MenuList.TabPages)
            {
                tabList.Add(tab);
                tab_MenuList.TabPages.Remove(tab);
            }

            if (planId != null)
            {
                ShowTab(false, "plan");
                string planName = SqlHelper.ExecuteOnlyOneQuery($"SELECT dd_name FROM data_dictionary WHERE dd_id='{planId}'").ToString();
                lbl_PlanName.Text = planName;
                tv_DataList.Nodes[0].Text = planName;
            }
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
