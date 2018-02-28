using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using 科技计划项目档案数据采集管理系统.Properties;

namespace 科技计划项目档案数据采集管理系统.FileReceive
{
    public partial class Frm_FileReceive : Form
    {
        private object currentUnit;
        public Frm_FileReceive()
        {
            InitializeComponent();
        }

        private void Frm_FileReceive_load(object sender, EventArgs e)
        {
            //默认加载批次列表
            LoadPCDataScoure(null);

            //加载来源单位列表
            LoadCompanySource();
           
            dgv_PCList.ColumnHeadersDefaultCellStyle = DataGridViewStyleHelper.GetHeaderStyle();
            dgv_PCList.ColumnHeadersDefaultCellStyle = DataGridViewStyleHelper.GetHeaderStyle();
         
        }

        //加载来源单位
        private void LoadCompanySource()
        {
           Image[] imgs = new Image[] { Resources.pic1, Resources.pic2, Resources.pic3, Resources.pic4, Resources.pic5, Resources.pic6, Resources.pic7, Resources.pic8 };
            //加载一级菜单
            List<CreateKyoPanel.KyoPanel> list = new List<CreateKyoPanel.KyoPanel>();
            list.Add(new CreateKyoPanel.KyoPanel
            {
                Name = "FileReceive",
                Text = "档案接收",
                Image = imgs[0],
                HasNext = true
            });
            CreateKyoPanel.SetPanel(pal_LeftMenu, list);
            //加载二级菜单
            list.Clear();
            list.Add(new CreateKyoPanel.KyoPanel
            {
                Name = "ace_all",
                Text = "全部来源单位",
                Image = Resources.pic1,
                HasNext = false
            });

            string querySql = "SELECT dd_id, dd_name FROM data_dictionary WHERE dd_pId=" +
                "(SELECT dd_id FROM data_dictionary WHERE dd_code = 'dic_key_company_source') ORDER BY dd_sort";
            DataTable table = SqlHelper.ExecuteQuery(querySql);
            for (int i = 0; i < table.Rows.Count; i++)
            {
                list.Add(new CreateKyoPanel.KyoPanel
                {
                    Name = GetValue(table.Rows[i]["dd_id"]),
                    Text = GetValue(table.Rows[i]["dd_name"]),
                    HasNext = false
                });
            }
            Panel basicPanel = CreateKyoPanel.SetSubPanel(pal_LeftMenu.Controls.Find("FileReceive", false)[0] as Panel, list, Element_Click);
        }


        /// <summary>
        /// 来源单位点击事件
        /// </summary>
        private void Element_Click(object sender, EventArgs e)
        {
            Panel panel = null;
            if (sender is Panel)
                panel = sender as Panel;
            else if (sender is Label)
                panel = (sender as Label).Parent as Panel;
            if ("ace_all".Equals(panel.Name))
            {
                MessageBox.Show("@check  qqq : " + panel.Name);
                LoadPCDataScoure(null);
            }
            else
            {
                MessageBox.Show("@check   : "+ panel.Name);
                //StringBuilder querySql = new StringBuilder("SELECT ");
                //querySql.Append("pc.trp_id, dd_name, trp_name, trp_code, trp_submit_status, trp_cd_amount");
                //querySql.Append(" FROM transfer_registration_pc pc,data_dictionary dd");
                //querySql.Append(" WHERE com_id='" + panel.Name + "'");
                //querySql.Append(" AND pc.com_id = dd.dd_id AND trp_submit_status=1");
                //LoadPCDataScoure(querySql.ToString());
                //dgv_PCList.Tag = SqlHelper.ExecuteOnlyOneQuery("SELECT dd_code FROM data_dictionary WHERE dd_id ='" + panel.Name + "'");               
               
                //currentUnit = panel.Name;
            }
            //当前Panel为选中状态
            foreach (Panel item in panel.Parent.Controls)
            {
                item.BackColor = Color.Transparent;
                item.Tag = false;
            }
            panel.BackColor = Color.Purple;
            panel.Tag = true;
        }







        /// <summary>
        /// 加载批次数据
        /// </summary>
        /// <param name="querySql">待加载的数据SQL</param>
        private void LoadPCDataScoure(string _querySql)
        {
           
        }


        private string GetValue(object v)
        {
            return v == null ? string.Empty : v.ToString();
        }
    }
}
