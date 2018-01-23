using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using 科技计划项目档案数据采集管理系统.Properties;

namespace 科技计划项目档案数据采集管理系统.Manager
{
    public partial class Frm_Manager : Form
    {
        public Frm_Manager()
        {
            InitializeComponent();
        }

        private void Frm_Manager_load(object sender, EventArgs e)
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

            //string querySql = "SELECT dm_name,dm_code from dictionary_manage";
            string querySql = "SELECT dd_id,dd_name,dd_code from data_dictionary where level = '1'";

            DataTable dataTable = SqlHelper.ExecuteQuery(querySql);
            CreateKyoPanel.KyoPanel[] kyoPanels = new CreateKyoPanel.KyoPanel[dataTable.Rows.Count];
            for (int i = 0; i < kyoPanels.Length; i++)
            {
                kyoPanels[i] = new CreateKyoPanel.KyoPanel
                {
                    Name = dataTable.Rows[i]["dd_id"].ToString(),
                    Text = dataTable.Rows[i]["dd_name"].ToString(),
                   
                    //Name = dataTable.Rows[i]["dm_code"].ToString(),
                    //Text = dataTable.Rows[i]["dm_name"].ToString(),
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
                string pId = control.Name;               
                string querySql = $"SELECT dd_id, dd_name as 名称,dd_code as 编码,dd_note as 描述,dd_sort as 排序 from  data_dictionary where dd_pId='{pId}'";
                DataTable dataTable = SqlHelper.ExecuteQuery(querySql);
                dgv_DataList.DataSource = dataTable;

                dgv_DataList.Columns["dd_id"].Visible = false;          
                //当前列表的pid
               dgv_DataList.Tag = pId;
            }
        }

        private void btn_Search_Click(object sender, EventArgs e)
        {
            int index = cbo_SearchType.SelectedIndex;
            string searchKey = txt_SearchKey.Text;
            string queryKey = string.Empty;/*查询条件*/
            if (index == 0) {
                queryKey = "dd_name";
            }else if (index == 1) {
                queryKey = "dd_code";
            }          
            string querySql = $"select dd_name as 名称,dd_code as 编码,dd_note as 描述,dd_sort as 排序 from data_dictionary where {queryKey} = '{searchKey}' and dd_pId='{dgv_DataList.Tag}'";          
            dgv_DataList.DataSource = SqlHelper.ExecuteQuery(querySql);
        }

        private void btn_Add_Click(object sender, EventArgs e)
        {
            //获取列表的pId
            string pId = dgv_DataList.Tag == null ? string.Empty : dgv_DataList.Tag.ToString();       
            Frm_Add frm = new Frm_Add(true, pId);        
            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadZDDataScoure(pId);
            }           
        }

        //加载添加或者更改后的列表】
        private void LoadZDDataScoure(string pId)
        {
            //DataGridViewStyleHelper.
            
            if (!string.IsNullOrEmpty(pId))
            {
                string querySql = $"SELECT dd_id, dd_name as 名称,dd_code as 编码,dd_note as 描述,dd_sort as 排序 from  data_dictionary where dd_pId='{pId}'";
                dgv_DataList.DataSource = SqlHelper.ExecuteQuery(querySql);
            }           
        }






        private void btn_update(object sender, EventArgs e)
        {
            Frm_Update frm_Update = new Frm_Update();
            frm_Update.ShowDialog();
        }

        private void btn_del(object sender, EventArgs e)
        {
            MessageBox.Show("请先至少选择一条要删除的数据!", "尚未选择数据", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            //int amount = dgv_SWDJ.SelectedRows.Count;
            //if (amount > 0)
            //{
            //    if (MessageBox.Show("确定要删除选中的数据吗?", "确认提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk) == DialogResult.OK)
            //    {
            //        int deleteAmount = 0;
            //        if ("PC".Equals(dgv_SWDJ.Tag))
            //        {
            //            foreach (DataGridViewRow row in dgv_SWDJ.SelectedRows)
            //            {
            //                string pid = row.Cells["trp_id"].Value.ToString();
            //                string deleteSql = $"DELETE FROM transfer_registration_pc WHERE trp_id = '{pid}'";
            //                SqlHelper.ExecuteNonQuery(deleteSql);
            //                deleteAmount++;
            //            }
            //            LoadPCDataScoure(null);
            //        }
            //        else if ("CD".Equals(dgv_SWDJ.Tag))
            //        {
            //            string pid = null;
            //            foreach (DataGridViewRow row in dgv_SWDJ.SelectedRows)
            //            {
            //                string cid = row.Cells["trc_id"].Value.ToString();
            //                pid = SqlHelper.ExecuteOnlyOneQuery($"SELECT trp_id FROM transfer_registraion_cd WHERE trc_id='{cid}'").ToString();

            //                string deleteSql = $"DELETE FROM transfer_registraion_cd WHERE trc_id = '{cid}'";
            //                SqlHelper.ExecuteNonQuery(deleteSql);

            //                string updateSql = $"UPDATE transfer_registration_pc SET trp_cd_amount=(SELECT COUNT(*) FROM transfer_registraion_cd WHERE trp_id = '{pid}') WHERE trp_id = '{pid}'";
            //                SqlHelper.ExecuteNonQuery(updateSql);

            //                deleteAmount++;
            //            }
            //            LoadCDDataScoure(pid);
            //        }
            //        MessageBox.Show(deleteAmount + "条数据已被删除!", "操作成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    }
            //}
            //else
            //{
            //    MessageBox.Show("请先至少选择一条要删除的数据!", "尚未选择数据", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            //}
        }


        //二级单元格点击事件
        private void dgv_DataList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1 && e.ColumnIndex != -1)
            {
                if ("名称".Equals(dgv_DataList.Columns[e.ColumnIndex].Name))
                {
                    object pid = dgv_DataList.Rows[e.RowIndex].Cells["dd_id"].Value;

                    string querySql = $"SELECT dd_id, dd_name as 名称,dd_code as 编码,dd_note as 描述,dd_sort as 排序 from  data_dictionary where dd_pId='{pid}'";
                    DataTable dataTable = SqlHelper.ExecuteQuery(querySql);
                    dgv_DataList.DataSource = dataTable;

                    dgv_DataList.Columns["dd_id"].Visible = false;

                    //当前列表的pid
                    dgv_DataList.Tag = pid;
                }
            }
        }
    }
}
