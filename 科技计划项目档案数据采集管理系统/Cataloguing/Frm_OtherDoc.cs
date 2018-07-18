using System;
using System.Data;
using System.Windows.Forms;

namespace 科技计划项目档案数据采集管理系统
{
    public partial class Frm_OtherDoc : DevExpress.XtraEditors.XtraForm
    {
        private object objId;
        public Frm_OtherDoc(object objId)
        {
            InitializeComponent();
            this.objId = objId;
        }

        private void Frm_OtherDoc_Load(object sender, EventArgs e)
        {
            //CREATE TABLE other_doc(
            //od_id varchar(100) primary key not null,
            //od_name varchar(500) not null,
            //od_code varchar(100) not null,
            //od_carrier varchar(100) not null,
            //od_intro varchar(1000) not null,
            //od_obj_id varchar(100) not null
            //)


            DataTable table = SqlHelper.ExecuteQuery($"SELECT * FROM other_doc WHERE od_obj_id='{objId}'");
            foreach(DataRow row in table.Rows)
            {
                int i = view.Rows.Add();
                view.Rows[i].Tag = row["od_id"];
                view.Rows[i].Cells["name"].Value = row["od_name"];
                view.Rows[i].Cells["code"].Value = row["od_code"];
                view.Rows[i].Cells["carrier"].Value = row["od_carrier"];
                view.Rows[i].Cells["intro"].Value = row["od_intro"];
            }
        }

        private void Btn_Sure_Click(object sender, EventArgs e)
        {
            if(DevExpress.XtraEditors.XtraMessageBox.Show("确定保存当前录入信息吗？", "确认提示", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk) == DialogResult.Yes)
            {
                string name = txt_Name.Text;
                string code = txt_Code.Text;
                string carrier = txt_Carrier.Text;
                string intro = txt_Intro.Text;
                if(!string.IsNullOrEmpty(name))
                {
                    string primaryKey = Guid.NewGuid().ToString();
                    string insertSQL = "INSERT INTO other_doc(od_id, od_name, od_code, od_carrier, od_intro, od_obj_id) " +
                        $"VALUES ('{primaryKey}','{name}','{code}','{carrier}','{intro}','{objId}')";
                    SqlHelper.ExecuteNonQuery(insertSQL);
                    Frm_OtherDoc_Load(null, null);
                }
            }
        }

        private void View_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            if(DevExpress.XtraEditors.XtraMessageBox.Show("确定要删除当前选中行吗？", "确认提示", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk) == DialogResult.Yes)
            {
                object id = e.Row.Tag;
                SqlHelper.ExecuteNonQuery($"DELETE FROM other_doc WHERE od_id='{id}'");
                Frm_OtherDoc_Load(null, null);
            }
            else
                e.Cancel = true;
        }
    }
}
