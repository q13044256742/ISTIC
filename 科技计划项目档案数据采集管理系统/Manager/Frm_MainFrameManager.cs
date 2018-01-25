using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using 科技计划项目档案数据采集管理系统.Properties;
using 科技计划项目档案数据采集管理系统.TransferOfRegistration;

namespace 科技计划项目档案数据采集管理系统
{
    public partial class Frm_MainFrameManager : DevExpress.XtraEditors.XtraForm
    {
        public User user;
        public Frm_MainFrameManager(User user)
        {
            this.user = user;
            InitializeComponent();
            InitalForm();
        }

        private void InitalForm()
        {
            //当前登录人信息
            txt_RealName.Text = user.RealName;
            if("1".Equals(user.Remark))
            {
                Frm_ToR frm = new Frm_ToR();
                frm.MdiParent = this;
                frm.Show();
            }
            else if ("2".Equals(user.Remark))
            {
                Frm_CG frm = new Frm_CG();
                frm.MdiParent = this;
                frm.Show();
            }
        }

        private void Frm_MainFrame_FormClosing(object sender, System.Windows.Forms.FormClosingEventArgs e)
        {
            Application.Exit();
        }


        private void Frm_MainFrame_Load(object sender, EventArgs e)
        {
            lbl_OtherInfo.Text = $"当前时间：{DateTime.Now.Year}年{DateTime.Now.Month}月{DateTime.Now.Day}日 {System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.GetDayName(DateTime.Now.DayOfWeek)}";
            LoadLeftMenu();
        }

        private void LoadLeftMenu()
        {
            Image[] imgs = new Image[] { Resources.pic1, Resources.pic2, Resources.pic3, Resources.pic4, Resources.pic5, Resources.pic6, Resources.pic7, Resources.pic8 };
            List<CreateKyoPanel.KyoPanel> list = new List<CreateKyoPanel.KyoPanel>();
            list.Add(new CreateKyoPanel.KyoPanel
            {
                Name = "ZD_MANAGER",
                Text = "字典管理",
                Image = imgs[0],
                HasNext = true
            });
            CreateKyoPanel.SetPanel(pal_LeftMenu, list);
            list.Clear();
            string querySql = "SELECT dd_id,dd_name,dd_code from data_dictionary where level = '1' order by dd_sort";

            DataTable dataTable = SqlHelper.ExecuteQuery(querySql);
            CreateKyoPanel.KyoPanel[] kyoPanels = new CreateKyoPanel.KyoPanel[dataTable.Rows.Count];
            for (int i = 0; i < kyoPanels.Length; i++)
            {
                kyoPanels[i] = new CreateKyoPanel.KyoPanel
                {
                    Name = dataTable.Rows[i]["dd_id"].ToString(),
                    Text = dataTable.Rows[i]["dd_name"].ToString(),
                    HasNext = false
                };
            }
            list.AddRange(kyoPanels);

            Panel parentPanel = pal_LeftMenu.Controls.Find("ZD_MANAGER", false)[0] as Panel;
            CreateKyoPanel.SetSubPanel(parentPanel, list, Sub_Menu_Click);
        }

        /// <summary>
        /// 二级菜单点击事件（计划/项目/单位...字典）
        /// </summary>
        private void Sub_Menu_Click(object sender, EventArgs e)
        {
            Control control = null;
            if (sender is Panel)
                control = sender as Control;
            else
                control = (sender as Control).Parent;

            if (!string.IsNullOrEmpty(control.Name))
            {
                Manager.Frm_Manager frm = new Manager.Frm_Manager(control.Name);
                frm.MdiParent = this;
                frm.Show();
            }
        }

        private void lbl_ExitSystem_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Frm_Login frm_Login = new Frm_Login();
            frm_Login.Show();
            Hide();
        }
    }
}
