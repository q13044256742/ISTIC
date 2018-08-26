using System;
using System.Data;
using System.Windows.Forms;

namespace 科技计划项目档案数据采集管理系统
{
    public partial class Frm_BorrowEdit : DevExpress.XtraEditors.XtraForm
    {
        private object FILE_ID;
        /// <param name="fileId">文件主键</param>
        public Frm_BorrowEdit(object fileId)
        {
            FILE_ID = fileId;
            InitializeComponent();
            cbo_FileType.Items.AddRange(new object[] { "原件", "复印件", "电子" });
            DataRow row = SqlHelper.ExecuteSingleRowQuery($"SELECT a.pi_code, a.pi_name, pb_gc_id, pb_box_number, pfl_code, pfl_name FROM processing_file_list " +
                $"LEFT JOIN (SELECT pi_id, pi_code, pi_name FROM project_info UNION ALL SELECT ti_id, ti_code, ti_name FROM topic_info UNION ALL SELECT si_id, si_code, si_name FROM subject_info) a ON pfl_obj_id = a.pi_id " +
                $"LEFT JOIN processing_box ON pb_obj_id = pfl_obj_id " +
                $"WHERE pfl_id = '{fileId}'");
            if(row != null)
            {
                lbl_pCode.Text = ToolHelper.GetValue(row["pi_code"]);
                lbl_pName.Text = ToolHelper.GetValue(row["pi_name"]);
                lbl_pGC.Text = ToolHelper.GetValue(row["pb_gc_id"]);
                lbl_pBoxId.Text = ToolHelper.GetValue(row["pb_box_number"]);
                lbl_FileCode.Text = ToolHelper.GetValue(row["pfl_code"]);
                lbl_FIleName.Text = ToolHelper.GetValue(row["pfl_name"]);
            }

        }

        private void Frm_BorrowEdit_Load(object sender, EventArgs e)
        {
            txt_Borrow_Date.Text = ToolHelper.GetDateValue(DateTime.Now, "yyyy-MM-dd");
            //lbl_LogUser.Text = UserHelper.GetUser().RealName;

            lbl_Code.Text = GetCode();
        }

        /// <summary>
        /// 自动生成借阅单编号
        /// </summary>
        private string GetCode()
        {
            //年度(4位)+年度内流水号（4位）
            int year = DateTime.Now.Year;
            int number = SqlHelper.ExecuteCountQuery($"SELECT COUNT(bl_id) FROM borrow_log WHERE bl_code LIKE '{year}%';");
            return $"{year}{(number + 1).ToString().PadLeft(4, '0')}";
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
