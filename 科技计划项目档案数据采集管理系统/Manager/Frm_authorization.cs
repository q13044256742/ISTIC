using DevExpress.XtraEditors;
using System;
using System.Windows.Forms;

namespace 科技计划项目档案数据采集管理系统.Manager
{
    public partial class Frm_Authoriz : XtraForm
    {
        private object RoldId;
        public Frm_Authoriz(object RoldId, object RoleName)
        {
            InitializeComponent();
            this.RoldId = RoldId;
            lbl_Role.Text = ToolHelper.GetValue(RoleName);
        }
        //保存
        private void Btn_Save(object sender, EventArgs e)
        {
            string insertSQL = $"DELETE FROM module WHERE m_code='{RoldId}';";
            foreach(CheckEdit item in group_Model.Controls)
            {
                if(item.Checked)
                {
                    insertSQL += $"INSERT INTO module(m_id, m_name, m_code) VALUES('{Guid.NewGuid().ToString()}','{item.Tag}','{RoldId}');";
                }
            }
            SqlHelper.ExecuteNonQuery(insertSQL);
            XtraMessageBox.Show("授权成功!", "操作提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            Close();               
        }

        private void Frm_authorization_Load(object sender, EventArgs e)
        {
            object[] roles = SqlHelper.ExecuteSingleColumnQuery($"SELECT m_name FROM module WHERE m_code='{RoldId}'");
            foreach(object role in roles)
            {
                foreach(CheckEdit item in group_Model.Controls)
                {
                    if(item.Tag.Equals(role))
                    {
                        item.Checked = true;
                        break;
                    }
                }
            }
        }
    }
}
