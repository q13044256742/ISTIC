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
    public partial class Frm_UserGroupSelect : Form
    {
        public Frm_UserGroupSelect()
        {
            InitializeComponent();
            LoadUserGroup();
        }

        private void LoadUserGroup()
        {
            string sql = $"select ug_id,ug_name from user_group order by ug_sort";
            //DataTable table = SqlHelper.ExecuteQuery(sql);


            List_all.Items.Add(SqlHelper.ExecuteQuery(sql).ToString());  

            //List_all.DisplayMember = "ug_name";
            //List_all.ValueMember = "ug_id";

        }

        private void Ug_seclect_btnSave(object sender, EventArgs e)
        {

        }

        private void Ug_select_btnClose(object sender, EventArgs e)
        {
            Close();
        }
    }
}
