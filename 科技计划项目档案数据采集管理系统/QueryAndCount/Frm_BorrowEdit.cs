using System;
using System.Data;
using System.Windows.Forms;

namespace 科技计划项目档案数据采集管理系统
{
    public partial class Frm_BorrowEdit : DevExpress.XtraEditors.XtraForm
    {
        private object FILE_ID;
        /// <param name="fileId">文件主键</param>
        public Frm_BorrowEdit(object fileId, object borrowId, bool isLog)
        {
            string queryCon = string.Empty;
            if(isLog)
                queryCon = $"AND bl.bl_id='{borrowId}'";
            FILE_ID = fileId;
            InitializeComponent();
            cbo_FileType.Items.AddRange(new object[] { "原件", "复印件", "电子" });
            string querySql = $"SELECT * FROM processing_file_list pfl " +
                $"LEFT JOIN (SELECT pi_id, pi_code, pi_name FROM project_info UNION ALL SELECT ti_id, ti_code, ti_name FROM topic_info UNION ALL SELECT si_id, si_code, si_name FROM subject_info) a ON pfl_obj_id = a.pi_id " +
                $"LEFT JOIN processing_box pb ON pb.pb_id = pfl.pfl_box_id " +
                $"LEFT JOIN borrow_log bl ON bl.bl_file_id = pfl.pfl_id {queryCon} " +
                $"WHERE pfl_id = '{fileId}'";
            DataRow row = SqlHelper.ExecuteSingleRowQuery(querySql);
            if(row != null)
            {
                lbl_pCode.Text = ToolHelper.GetValue(row["pi_code"]);
                lbl_pName.Text = ToolHelper.GetValue(row["pi_name"]);
                lbl_pGC.Text = ToolHelper.GetValue(row["pb_gc_id"]);
                lbl_pBoxId.Text = ToolHelper.GetValue(row["pb_box_number"]);
                lbl_FileCode.Text = ToolHelper.GetValue(row["pfl_code"]);
                lbl_FIleName.Text = ToolHelper.GetValue(row["pfl_name"]);
                if(isLog)
                {
                    txt_Unit.Text = ToolHelper.GetValue(row["bl_user_unit"]);
                    txt_User.Text = ToolHelper.GetValue(row["bl_user"]);
                    txt_Phone.Text = ToolHelper.GetValue(row["bl_user_phone"]);
                    txt_Borrow_Date.Text = ToolHelper.GetDateValue(row["bl_date"], "yyyy-MM-dd HH:mm:dd");
                    txt_Borrow_Term.Text = ToolHelper.GetValue(row["bl_term"]);
                    cbo_FileType.SelectedIndex = ToolHelper.GetIntValue(row["bl_form"], -1);
                    txt_Should_Return_Date.Text = ToolHelper.GetValue(row["bl_should_return_term"]);
                    txt_Real_Return_Date.Text = ToolHelper.GetValue(row["bl_real_return_term"]);
                    lbl_Code.Text = ToolHelper.GetValue(row["bl_code"]);
                    lbl_LogUser.Text = ToolHelper.GetValue(row["bl_log_user"]);
                    txt_Remark.Text = ToolHelper.GetValue(row["bl_remark"]);

                    string value = ToolHelper.GetValue(row["bl_id"]);
                    if(!string.IsNullOrEmpty(value))
                    {
                        lbl_FIleName.Tag = value;
                        int bstate = ToolHelper.GetIntValue(row["bl_return_state"], 0);
                        if(bstate != 0)
                            btn_Sure.Enabled = false;
                        else
                            btn_Sure.Text = "确认归还";
                    }
                }
            }
        }

        private void Frm_BorrowEdit_Load(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(txt_Real_Return_Date.Text))
                txt_Real_Return_Date.Text = ToolHelper.GetDateValue(DateTime.Now, "yyyy-MM-dd HH:mm");
            if(string.IsNullOrEmpty(lbl_Code.Text))
                lbl_Code.Text = GetCode();
            if(string.IsNullOrEmpty(lbl_LogUser.Text))
                lbl_LogUser.Text = UserHelper.GetUser().RealName;
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
                    string code = lbl_Code.Text;
                    string unit = txt_Unit.Text;
                    string user = txt_User.Text;
                    string phone = txt_Phone.Text;
                    int ftype = cbo_FileType.SelectedIndex;
                    string bdate = ToolHelper.GetDateValue(DateTime.Now, "yyyy-MM-dd HH:mm");
                    string bterm = txt_Borrow_Term.Text;
                    string sbdate = ToolHelper.GetDateValue(txt_Should_Return_Date.EditValue, "yyyy-MM-dd");
                    string loguser = lbl_LogUser.Text;
                    string remark = txt_Remark.Text;
                    string insertSQL = "INSERT INTO borrow_log(bl_id, bl_code, bl_file_id, bl_borrow_state, bl_return_state, bl_form, bl_user, bl_user_unit, bl_user_phone, bl_date, bl_term, bl_should_return_term, bl_log_user, bl_remark) " +
                        $"VALUES ('{primaryKey}', '{code}', '{FILE_ID}', '{1}', '{0}', '{ftype}', '{user}', '{unit}', '{phone}', '{bdate}', '{bterm}', '{sbdate}', '{loguser}', '{remark}')";

                    SqlHelper.ExecuteNonQuery(insertSQL);
                }
                else
                {
                    primaryKey = lbl_FIleName.Tag;
                    string rbdate = ToolHelper.GetDateValue(txt_Real_Return_Date.EditValue, "yyyy-MM-dd HH:mm:dd");
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
