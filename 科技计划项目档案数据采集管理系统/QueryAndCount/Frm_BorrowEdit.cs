using System;
using System.Windows.Forms;

namespace 科技计划项目档案数据采集管理系统
{
    public partial class Frm_BorrowEdit : DevExpress.XtraEditors.XtraForm
    {
        /// <summary>
        /// 文件ID
        /// </summary>
        public object FILE_ID;
        public Frm_BorrowEdit(string fileCode, string fileName)
        {
            InitializeComponent();
            cbo_FileType.Items.AddRange(new object[] { "原件", "复印件", "电子" });
            lbl_FileCode.Text = fileCode;
            lbl_FIleName.Text = fileName;
        }

        private void Frm_BorrowEdit_Load(object sender, EventArgs e)
        {
            txt_Borrow_Date.Text = ToolHelper.GetDateValue(DateTime.Now, "yyyy-MM-dd");
            lbl_LogUser.Text = UserHelper.GetUser().RealName;
        }

        private void Btn_Sure_Click(object sender, EventArgs e)
        {
            if(CheckDatas())
            {
                object primaryKey = Guid.NewGuid().ToString();
                if(lbl_FIleName.Tag == null)
                {
                    string unit = txt_Unit.Text;
                    string user = txt_User.Text;
                    string phone = txt_Phone.Text;
                    int ftype = cbo_FileType.SelectedIndex;
                    string bdate = ToolHelper.GetDateValue(txt_Borrow_Date.EditValue, "yyyy-MM-dd");
                    string bterm = txt_Borrow_Term.Text;
                    string sbdate = ToolHelper.GetDateValue(txt_Should_Return_Date.EditValue, "yyyy-MM-dd");
                    string loguser = lbl_LogUser.Text;
                    string insertSQL = $"INSERT INTO [borrow_log]" +
                        $"([bl_id], [bl_file_id], [bl_borrow_state], [bl_return_state], [bl_form], [bl_user], [bl_user_unit], [bl_user_phone], [bl_date], [bl_term], [bl_should_return_term], [bl_log_user]) " +
                        $"VALUES ('{primaryKey}', '{FILE_ID}', '{1}', '{0}', '{ftype}', '{user}', '{unit}', '{phone}', '{bdate}', '{bterm}', '{sbdate}', '{loguser}')";

                    SqlHelper.ExecuteNonQuery(insertSQL);
                }
                else
                {
                    primaryKey = lbl_FIleName.Tag;
                    string rbdate = ToolHelper.GetDateValue(txt_Real_Return_Date.EditValue, "yyyy-MM-dd");
                    string updateSQL = $"UPDATE borrow_log SET bl_real_return_term='{rbdate}', bl_borrow_state=0, bl_return_state=1 WHERE bl_id='{primaryKey}'";
                    SqlHelper.ExecuteNonQuery(updateSQL);
                }
                DevExpress.XtraEditors.XtraMessageBox.Show("操作成功。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                DialogResult = DialogResult.OK;
                Tag = primaryKey;
                Close();
            }
        }

        private bool CheckDatas()
        {
            bool flag = true;

            return flag;
        }
    }
}
