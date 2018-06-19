using DevExpress.XtraEditors;
using System;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace 科技计划项目档案数据采集管理系统
{
    public partial class Frm_AddPC : DevExpress.XtraEditors.XtraForm
    {
        /// <summary>
        /// 新增：机构编码
        /// 编辑：批次主键
        /// </summary>
        private bool isAdd;
        /// <summary>
        ///  新增：机构编码
        ///  编辑：批次主键
        /// </summary>
        private string unitCode;

        public Frm_AddPC(bool isAdd, string unitCode)
        {
            this.isAdd = isAdd;
            InitializeComponent();
            this.unitCode = unitCode;
            if (isAdd)
                LoadCompanySource();
            else
                LoadData(unitCode);
        }

        /// <summary>
        /// 编辑状态下，根据ID读取数据
        /// </summary>
        /// <param name="unitCode">批次主键</param>
        private void LoadData(string unitCode)
        {
            //加载批次信息
            StringBuilder querySql = new StringBuilder("SELECT * FROM transfer_registration_pc pc,data_dictionary dd");
            querySql.Append(" WHERE pc.trp_id = '" + unitCode + "'");
            querySql.Append(" AND pc.com_id = dd.dd_id");
            DataTable dataTable = SqlHelper.ExecuteQuery(querySql.ToString());
            LoadCompanySource();
            if (dataTable.Rows.Count > 0)
            {
                txt_BatchCode.Text = GetString(dataTable.Rows[0]["trp_code"]);
                txt_BatchName.Text = GetString(dataTable.Rows[0]["trp_name"]);
                cbo_SourceUnit.SelectedValue = GetString(dataTable.Rows[0]["com_id"]);
                dtp_TransferTime.Value = Convert.ToDateTime(dataTable.Rows[0]["trp_log_data"]);
                txt_Receiver.Text = GetString(dataTable.Rows[0]["trp_receiver"]);
                txt_giver.Text = GetString(dataTable.Rows[0]["trp_giver"]);
                txt_Remark.Text = GetString(dataTable.Rows[0]["trp_remark"]);
                txt_UploadFile.Text = GetString(dataTable.Rows[0]["trp_attachment_id"]);
            }
            //加载批次下光盘信息

            querySql = new StringBuilder("SELECT trc_id,trc_code,trc_name,trc_remark FROM transfer_registraion_cd WHERE trp_id='" + unitCode + "' ORDER BY trc_sort");
            DataTable table = SqlHelper.ExecuteQuery(querySql.ToString());
            foreach (DataRow item in table.Rows)
            {
                int index = dgv_CDlist.Rows.Add();
                dgv_CDlist.Rows[index].Cells["gpmc"].Value = item["trc_name"];
                dgv_CDlist.Rows[index].Cells["gpbh"].Value = item["trc_code"];
                dgv_CDlist.Rows[index].Cells["bz"].Value = item["trc_remark"];
            }

        }

        private void Btn_UploadFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            DialogResult result = dialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                txt_UploadFile.Text = dialog.FileName;
            }
        }

        private void Frm_AddPC_Load(object sender, EventArgs e)
        {
            //表头设置
            dgv_CDlist.ColumnHeadersDefaultCellStyle = DataGridViewStyleHelper.GetHeaderStyle();

            //列设置
            if (string.IsNullOrEmpty(txt_BatchCode.Text) && !"PC".Equals(unitCode))
                CreateBatchCode(unitCode);

            //隐藏光盘列表主键
            dgv_CDlist.Columns[0].Visible = false;

            Text = (isAdd ? "新增" : "修改") + "批次信息";

            txt_BatchName.Focus();
        }

        /// <summary>
        /// 加载来源单位至ComboBox下拉框
        /// </summary>
        private void LoadCompanySource()
        {
            DataTable table = SqlHelper.GetCompanyList();
            cbo_SourceUnit.DataSource = table;
            cbo_SourceUnit.DisplayMember = "dd_name";
            cbo_SourceUnit.ValueMember = "dd_id";
        }

        /// <summary>
        /// 自动生成批次编号
        /// </summary>
        private void CreateBatchCode(string unitCode)
        {
            string querySql = null;
            //自动生成批次编号
            object csid = SqlHelper.ExecuteOnlyOneQuery("SELECT dd_id FROM data_dictionary WHERE dd_code = '" + unitCode + "'");
            if (csid == null)
                cbo_SourceUnit.SelectedIndex = 0;
            else
                cbo_SourceUnit.SelectedValue = csid;

            querySql = "SELECT COUNT(*) FROM transfer_registration_pc WHERE com_id=('" + csid + "')";
            string amountStr = (Convert.ToInt32(SqlHelper.ExecuteOnlyOneQuery(querySql)) + 1).ToString();
            txt_BatchCode.Text = unitCode + DateTime.Now.Year + amountStr.PadLeft(3, '0');
        }

        private void btn_Save_MouseEnter(object sender, EventArgs e)
        {
            Cursor = Cursors.Hand;
        }

        private void btn_Save_MouseLeave(object sender, EventArgs e)
        {
            Cursor = Cursors.Default;
        }

        /// <summary>
        /// 保存/更新
        /// </summary>
        private void Btn_Save_Click(object sender, EventArgs e)
        {
            if(!ValidData())
            {
                XtraMessageBox.Show("请先将表单信息补充完整!");
                return;
            }
            if (XtraMessageBox.Show("确定要保存当前数据吗?", "确认提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk) == DialogResult.OK)
            {
                //保存批次基本信息
                string batchName = txt_BatchName.Text.Trim();
                string batchCode = txt_BatchCode.Text.Trim();
                string sourceUnit = cbo_SourceUnit.SelectedValue.ToString().Trim();
                DateTime transferTime = dtp_TransferTime.Value;
                string receiver = txt_Receiver.Text.Trim();
                string giver = txt_giver.Text.Trim();
                string remark = txt_Remark.Text.Trim();
                string fileUpload = txt_UploadFile.Text.Trim();
                object trpId = Guid.NewGuid().ToString();
                int cdCount = dgv_CDlist.RowCount - 1;
                //新增信息
                if (isAdd)
                {
                    StringBuilder basicInfo_QuerySql = new StringBuilder("INSERT INTO transfer_registration_pc ");
                    basicInfo_QuerySql.Append("(trp_id,com_id,trp_name,trp_code,trp_log_data,trp_receiver,trp_giver,trp_remark,trp_cd_amount,trp_attachment_id,trp_submit_status,trp_work_status,trp_people,trp_handle_time) VALUES(");
                    basicInfo_QuerySql.Append("'" + trpId + "',");
                    basicInfo_QuerySql.Append("'" + sourceUnit + "',");
                    basicInfo_QuerySql.Append("'" + batchName + "',");
                    basicInfo_QuerySql.Append("'" + batchCode + "',");
                    basicInfo_QuerySql.Append("'" + transferTime + "',");
                    basicInfo_QuerySql.Append("'" + receiver + "',");
                    basicInfo_QuerySql.Append("'" + giver + "',");
                    basicInfo_QuerySql.Append("'" + remark + "',");
                    basicInfo_QuerySql.Append("'" + cdCount + "',");
                    basicInfo_QuerySql.Append("'" + fileUpload + "',");
                    basicInfo_QuerySql.Append("'" + (int)SubmitStatus.NonSubmit + "',");
                    basicInfo_QuerySql.Append("'" + (int)WorkStatus.NonWork + "',");
                    basicInfo_QuerySql.Append("'" + UserHelper.GetInstance().User.UserKey + "',");
                    basicInfo_QuerySql.Append("'" + DateTime.Now + "')");
                    SqlHelper.ExecuteNonQuery(basicInfo_QuerySql.ToString());

                    //保存光盘基本信息
                    SaveCDList(trpId);
                }
                //更新信息
                else
                {
                    StringBuilder basicInfo_QuerySql = new StringBuilder("UPDATE transfer_registration_pc SET ");
                    basicInfo_QuerySql.Append("com_id='" + sourceUnit + "',");
                    basicInfo_QuerySql.Append("trp_name='" + batchName + "',");
                    basicInfo_QuerySql.Append("trp_code='" + batchCode + "',");
                    basicInfo_QuerySql.Append("trp_log_data='" + transferTime + "',");
                    basicInfo_QuerySql.Append("trp_receiver='" + receiver + "',");
                    basicInfo_QuerySql.Append("trp_giver='" + giver + "',");
                    basicInfo_QuerySql.Append("trp_remark='" + remark + "',");
                    basicInfo_QuerySql.Append("trp_cd_amount='" + cdCount + "',");
                    basicInfo_QuerySql.Append("trp_attachment_id='" + fileUpload + "',");
                    basicInfo_QuerySql.Append("trp_submit_status=1, trp_work_status=1,");
                    basicInfo_QuerySql.Append("trp_people='" + UserHelper.GetInstance().User.UserKey + "',");
                    basicInfo_QuerySql.Append("trp_handle_time='" + DateTime.Now + "'");
                    basicInfo_QuerySql.Append(" WHERE trp_id='" + unitCode + "'");
                    SqlHelper.ExecuteNonQuery(basicInfo_QuerySql.ToString());

                    //保存光盘基本信息【先删除当前批次下的所有光盘，再执行新增】
                    SqlHelper.ExecuteNonQuery("DELETE FROM transfer_registraion_cd WHERE trp_id = '" + unitCode + "'");
                    SaveCDList(unitCode);
                }
                if (XtraMessageBox.Show((isAdd ? "添加" : "更新") + "成功，是否返回列表页", "恭喜", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk) == DialogResult.Yes)
                {
                    DialogResult = DialogResult.OK;
                    Close();
                }
            }
        }

        /// <summary>
        /// 保存光盘列表信息
        /// </summary>
        /// <param name="trpId">批次主键</param>
        private void SaveCDList(object trpId)
        {
            for(int i = 0; i < dgv_CDlist.RowCount - 1; i++)
            {
                object cdName = dgv_CDlist.Rows[i].Cells["gpmc"].Value;
                object cdCode = dgv_CDlist.Rows[i].Cells["gpbh"].Value;
                object cdRemark = GetString(dgv_CDlist.Rows[i].Cells["bz"].Value);
                StringBuilder cdInfo_querySql = new StringBuilder("INSERT INTO transfer_registraion_cd ");
                cdInfo_querySql.Append("(trc_id,trc_name,trc_code,trp_id,trc_remark,trc_status,trc_complete_status,trc_people,trc_handle_time, trc_sort)");
                cdInfo_querySql.Append(" VALUES(");
                cdInfo_querySql.Append("'" + Guid.NewGuid().ToString() + "',");
                cdInfo_querySql.Append("'" + cdName + "',");
                cdInfo_querySql.Append("'" + cdCode + "',");
                cdInfo_querySql.Append("'" + trpId + "',");
                cdInfo_querySql.Append("'" + cdRemark + "',");
                cdInfo_querySql.Append("'" + (int)ReadStatus.NonRead + "',");
                cdInfo_querySql.Append("'" + (int)WorkStatus.NonWork + "',");
                cdInfo_querySql.Append("'" + UserHelper.GetInstance().User.UserKey + "',");
                cdInfo_querySql.Append("'" + DateTime.Now + "', '" + i + "')");
                SqlHelper.ExecuteNonQuery(cdInfo_querySql.ToString());
            }
        }

        /// <summary>
        /// 检验数据的完整性（如果存在光盘，则名称和编号为必填）
        /// </summary>
        private bool ValidData()
        {
            for (int i = 0; i < dgv_CDlist.RowCount - 1; i++)
                if (dgv_CDlist.Rows[i].Cells["gpmc"].Value == null || dgv_CDlist.Rows[i].Cells["gpbh"].Value == null)
                    return false;
            if (string.IsNullOrEmpty(txt_BatchName.Text.Trim()) || string.IsNullOrEmpty(txt_BatchCode.Text.Trim()))
                return false;
            return true;
        }

        /// <summary>
        /// Tab键触发自动生成光盘编号事件
        /// </summary>
        private void dgv_CDlist_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1 && e.ColumnIndex != -1)
            {
                if ("gpbh".Equals(dgv_CDlist.Columns[e.ColumnIndex].Name))
                {
                    if(dgv_CDlist.Rows[e.RowIndex].Cells["gpmc"].Value != null)
                    {
                        int index = dgv_CDlist.RowCount - 1;
                        string pcCode = txt_BatchCode.Text + "-" + index.ToString().PadLeft(3, '0');
                        dgv_CDlist.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = pcCode;
                    }
                }
            }
        }
        /// <summary>
        /// 来源单位变化时，批次编号同步变化
        /// </summary>
        private void cbo_SourceUnit_SelectionChangeCommitted(object sender, EventArgs e)
        {
            string unitId = cbo_SourceUnit.SelectedValue == null ? string.Empty : cbo_SourceUnit.SelectedValue.ToString();
            if (!string.IsNullOrEmpty(unitId))
            {
                string unitCode = GetValue(SqlHelper.ExecuteOnlyOneQuery("SELECT dd_code FROM data_dictionary WHERE dd_id='" + unitId + "'"));
                CreateBatchCode(unitCode);
            }
        }
        private string GetValue(object v)
        {
            return v == null ? string.Empty : v.ToString();
        }
        private void dgv_CDlist_RowLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1 && e.ColumnIndex != -1)
            {
                //暂时搁置
            }
        }
        /// <summary>
        /// 将Object对象转换成String形式
        /// </summary>
        private string GetString(object _obj)
        {
            return _obj == null ? string.Empty : _obj.ToString();
        }
    }
}
