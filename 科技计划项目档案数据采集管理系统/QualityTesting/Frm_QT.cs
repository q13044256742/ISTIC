using System;
using System.Collections.Generic;
using System.Windows.Forms;
using 科技计划项目档案数据采集管理系统.Properties;

namespace 科技计划项目档案数据采集管理系统
{
    public partial class Frm_QT : Form
    {
        public Frm_QT()
        {
            InitializeComponent();
        }

        private void Frm_QT_Load(object sender, EventArgs e)
        {
            List<CreateKyoPanel.KyoPanel> list = new List<CreateKyoPanel.KyoPanel>();
            list.Add(new CreateKyoPanel.KyoPanel()
            {
                Name = "QT_ZLJG",
                Text = "著录加工",
                Image = Resources.pic6,
                HasNext = true
            });
            CreateKyoPanel.SetPanel(pal_LeftMenu, list);

            list.Clear();
            list.AddRange(new CreateKyoPanel.KyoPanel[] {
                new CreateKyoPanel.KyoPanel()
                {
                     Name = "QT_Login",
                     Text = "质检登记",
                     HasNext = false
                },new CreateKyoPanel.KyoPanel()
                {
                     Name = "QT_Myqt",
                     Text = "我的质检",
                     HasNext = false
                }
            });
            CreateKyoPanel.SetSubPanel(pal_LeftMenu.Controls.Find("QT_ZLJG", false)[0] as Panel, list, Sub_Click);

            LoadPlanList();
        }

        private void Sub_Click(object sender, EventArgs e)
        {
            Panel panel = null;
            if(sender is Panel)
                panel = sender as Panel;
            else
                panel = (sender as Control).Parent as Panel;

            if("QT_Login".Equals(panel.Name))
            {
                MessageBox.Show("1");
            }
            else if("QT_Myqt".Equals(panel.Name))
            {
                MessageBox.Show("2");
            }
        }

        private void tab_MenuList_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = tab_MenuList.SelectedIndex;
            if(index == 0)//plan
            {
                LoadPlanList();
            }
            else if(index == 1)//project
            {

            }
            else if(index == 2)//subject
            {

            }
        }
        /// <summary>
        /// 加载计划列表
        /// </summary>
        private void LoadPlanList()
        {
            dgv_Plan.Columns.Clear();
            dgv_Plan.Rows.Clear();

            dgv_Plan.ColumnHeadersDefaultCellStyle = DataGridViewStyleHelper.GetHeaderStyle();
            dgv_Plan.Columns.Add("unit", "来源单位");
            dgv_Plan.Columns.Add("code", "计划编号");
            dgv_Plan.Columns.Add("size", "文件数");
            dgv_Plan.Columns.Add("control", "操作");
            dgv_Plan.Columns.Add("data_source", "数据途径");

        }
    }
}
