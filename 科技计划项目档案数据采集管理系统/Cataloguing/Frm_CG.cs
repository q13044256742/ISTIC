using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using 科技计划项目档案数据采集管理系统.Properties;

namespace 科技计划项目档案数据采集管理系统
{
    public partial class Frm_CG : Form
    {
        public Frm_CG()
        {
            InitializeComponent();
        }

        private void Frm_CG_Load(object sender, EventArgs e)
        {
            Image[] imgs = new Image[] { Resources.pic1, Resources.pic2, Resources.pic3, Resources.pic4, Resources.pic5, Resources.pic6, Resources.pic7, Resources.pic8 };
            List<CreateKyoPanel.KyoPanel> list = new List<CreateKyoPanel.KyoPanel>();
            list.Add(new CreateKyoPanel.KyoPanel
            {
                Name = "CG",
                Text = "著录加工",
                Image = imgs[0],
                HasNext = true
            });
            CreateKyoPanel.SetPanel(pal_LeftMenu, list);

            list.Clear();
            list.AddRange(new CreateKyoPanel.KyoPanel[]
            {
                new CreateKyoPanel.KyoPanel
                {
                    Name ="CG_LOGIN",
                    Text="加工登记",
                    HasNext = false
                },
                new CreateKyoPanel.KyoPanel
                {
                    Name="CG_WORK",
                    Text="我的加工",
                    HasNext = true
                }
            });
            Panel parentPanel = pal_LeftMenu.Controls.Find("CG", false)[0] as Panel;
            CreateKyoPanel.SetSubPanel(parentPanel, list, Sub_Menu_Click);
            
        }

        /// <summary>
        /// 二级菜单点击事件（加工登记/我的加工）
        /// </summary>
        private void Sub_Menu_Click(object sender, EventArgs e)
        {
            Panel panel = null;
            if (sender is Panel) panel = sender as Panel;
            else if (sender is Label) panel = (sender as Label).Parent as Panel;
            if ("CG_LOGIN".Equals(panel.Name))
            {

            }
            else if ("CG_WORK".Equals(panel.Name))
            {
                if (!true.Equals(panel.Tag))
                {
                    List<CreateKyoPanel.KyoPanel> list = new List<CreateKyoPanel.KyoPanel>();
                    list.AddRange(new CreateKyoPanel.KyoPanel[]
                    {
                        new CreateKyoPanel.KyoPanel
                        {
                            Name ="CG_WORK_ING",
                            Text="加工中",
                            HasNext = false
                        },
                        new CreateKyoPanel.KyoPanel
                        {
                            Name="CG_WORK_ED",
                            Text="已返工",
                            HasNext = false
                        }
                    });
                    Panel _parentPanel = pal_LeftMenu.Controls.Find("CG_WORK", true)[0] as Panel;
                    CreateKyoPanel.SetThreePanel(_parentPanel, list, Three_Menu_Click);
                    panel.Tag = true;
                }
            }
        }

        private void Three_Menu_Click(object sender, EventArgs e)
        {

        }
    }
}
