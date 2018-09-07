using System;
using System.Data;
using System.Windows.Forms;

namespace 科技计划项目档案数据采集管理系统
{
    public partial class Frm_OtherDoc : DevExpress.XtraEditors.XtraForm
    {
        public bool ReadOnly;
        private object objId;
        public Frm_OtherDoc(object objId)
        {
            InitializeComponent();
            this.objId = objId;
            ReadOnly = false;
        }

        private void Frm_OtherDoc_Load(object sender, EventArgs e)
        {
            LoadDocList();
            if(ReadOnly == true)
            {
                btn_Add.Enabled = btn_Delete.Enabled = btn_Edit.Enabled = btn_Sure.Enabled = !ReadOnly;
            }
        }

        private void LoadDocList()
        {
            view.Rows.Clear();
            DataTable table = SqlHelper.ExecuteQuery($"SELECT * FROM other_doc WHERE od_obj_id='{objId}' ORDER BY od_code");
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
                object id = txt_Name.Tag;
                string name = txt_Name.Text;
                string code = txt_Code.Text;
                string carrier = txt_Carrier.Text;
                string intro = txt_Intro.Text;
                if(id == null)
                {
                    id = Guid.NewGuid().ToString();
                    if(!string.IsNullOrEmpty(name))
                    {
                        string insertSQL = "INSERT INTO other_doc(od_id, od_name, od_code, od_carrier, od_intro, od_obj_id) " +
                            $"VALUES ('{id}','{name}','{code}','{carrier}','{intro}','{objId}');";
                        SqlHelper.ExecuteNonQuery(insertSQL);
                    }
                }
                else
                {
                    string updateSQL = "UPDATE other_doc SET " +
                        $"od_name='{name}'," +
                        $"od_code='{code}'," +
                        $"od_carrier='{carrier}'," +
                        $"od_intro='{intro}' " +
                        $"WHERE od_id='{id}';";
                    SqlHelper.ExecuteNonQuery(updateSQL);
                }
                btn_Add_Click(null, null);
                LoadDocList();
            }
        }

        private void View_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            e.Cancel = true;
            btn_Delete_Click(null, null);
        }

        /// <summary>
        /// 编辑
        /// </summary>
        private void kyoButton1_Click(object sender, EventArgs e)
        {
            DataGridViewRow row = view.CurrentRow;
            if(row != null)
            {
                DataRow dataRow = SqlHelper.ExecuteSingleRowQuery($"SELECT * FROM other_doc WHERE od_id='{row.Tag}'");
                if(dataRow != null)
                {
                    txt_Name.Tag = dataRow["od_id"];
                    txt_Name.Text = GetValue(dataRow["od_name"]);
                    txt_Code.Text = GetValue(dataRow["od_code"]);
                    txt_Carrier.Text = GetValue(dataRow["od_carrier"]);
                    txt_Intro.Text = GetValue(dataRow["od_intro"]);
                }
            }
            else
                DevExpress.XtraEditors.XtraMessageBox.Show("请选择待编辑行。", "提示");
        }

        private string GetValue(object value) => value == null ? string.Empty : value.ToString();

        private void btn_Add_Click(object sender, EventArgs e)
        {
            foreach(Control item in groupBox1.Controls)
                if(item is TextBox)
                {
                    item.Tag = null;
                    item.ResetText();
                }
        }

        private void btn_Delete_Click(object sender, EventArgs e)
        {
            DataGridViewRow row = view.CurrentRow;
            if(row != null)
            {
                SqlHelper.ExecuteNonQuery($"DELETE FROM other_doc WHERE od_id='{row.Tag}'");
                LoadDocList();
            }
            else
                DevExpress.XtraEditors.XtraMessageBox.Show("请选择待删除行。", "提示");
        }

        private void view_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            kyoButton1_Click(null, null);
        }
    }
}
