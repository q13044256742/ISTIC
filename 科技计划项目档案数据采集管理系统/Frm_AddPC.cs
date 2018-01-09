using System;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace 科技计划项目档案数据采集管理系统
{
    public partial class Frm_AddPC : Form
    {
        private string unitCode;
        public Frm_AddPC(string unitCode)
        {
            this.unitCode = unitCode;
            InitializeComponent();
        }

        private void btn_UploadFile_Click(object sender, EventArgs e)
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
            dgv_CDlist.ColumnHeadersDefaultCellStyle.Font = new Font("微软雅黑", 10f, FontStyle.Bold);
            dgv_CDlist.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv_CDlist.ColumnHeadersDefaultCellStyle.BackColor = Color.AliceBlue;

            //列设置
            dgv_CDlist.DefaultCellStyle.Font = new Font("微软雅黑", 9f);

            //加载来源单位
            string querySql = "SELECT cs_id,cs_name FROM company_source ORDER BY sorting ASC";
            DataTable table = SqlHelper.ExecuteQuery(querySql);
            cbo_SourceUnit.DataSource = table;
            cbo_SourceUnit.DisplayMember = "cs_name";
            cbo_SourceUnit.ValueMember = "cs_id";

            CreateBatchCode(unitCode);
        }

        /// <summary>
        /// 自动生成批次编号
        /// </summary>
        /// <returns></returns>
        private void CreateBatchCode(string unitCode)
        {
            string querySql;
            //自动生成批次编号
            object csid = SqlHelper.ExecuteOnlyOneQuery("SELECT CS_ID FROM company_source WHERE cs_code = '" + unitCode + "'");
            if (csid == null)
                cbo_SourceUnit.SelectedIndex = 0;
            else
                cbo_SourceUnit.SelectedValue = csid;

            querySql = "SELECT COUNT(*) FROM transfer_registration_pc WHERE com_id=('" + csid + "')";
            string amountStr = (Convert.ToInt32(SqlHelper.ExecuteOnlyOneQuery(querySql)) + 1).ToString();
            amountStr = amountStr.Length == 3 ? amountStr : (amountStr.Length == 2 ? "0" + amountStr : "00" + amountStr);

            txt_BatchCode.Text = unitCode + DateTime.Now.Year + amountStr;
        }

        private void btn_Save_MouseEnter(object sender, EventArgs e)
        {
            Cursor = Cursors.Hand;
        }

        private void btn_Save_MouseLeave(object sender, EventArgs e)
        {
            Cursor = Cursors.Default;
        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            if(!ValidData())
            {
                MessageBox.Show("请先将表单信息补充完整!");
                return;
            }
            if (MessageBox.Show("确定要保存当前数据吗?", "确认提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk) == DialogResult.OK)
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

                TransferOfRegist registration = new TransferOfRegist()
                {
                    Id = Guid.NewGuid().ToString(),
                    BatchName = batchName,
                    BatchCode = batchCode,
                    SourceUnit = sourceUnit,
                    TransferTime = transferTime,
                    Receive = receiver,
                    Giver = giver,
                    Remark = remark,
                    TrpCdAmount = dgv_CDlist.RowCount - 1,
                    TrpStatus = 1,
                    FileUpload = fileUpload
                };

                StringBuilder basicInfo_QuerySql = new StringBuilder("INSERT INTO transfer_registration_pc ");
                basicInfo_QuerySql.Append("(trp_id,com_id,trp_name,trp_code,trp_log_data,trp_receiver,trp_giver,trp_remark,trp_cd_amount,trp_attachment_id,trp_status,trp_people,trp_handle_time) VALUES(");
                basicInfo_QuerySql.Append("'" + registration.Id + "',");
                basicInfo_QuerySql.Append("'" + registration.SourceUnit + "',");
                basicInfo_QuerySql.Append("'" + registration.BatchName + "',");
                basicInfo_QuerySql.Append("'" + registration.BatchCode + "',");
                basicInfo_QuerySql.Append("'" + registration.TransferTime + "',");
                basicInfo_QuerySql.Append("'" + registration.Receive + "',");
                basicInfo_QuerySql.Append("'" + registration.Giver + "',");
                basicInfo_QuerySql.Append("'" + registration.Remark + "',");
                basicInfo_QuerySql.Append("'" + registration.TrpCdAmount + "',");
                basicInfo_QuerySql.Append("'" + registration.FileUpload + "',");
                basicInfo_QuerySql.Append("'" + registration.TrpStatus + "',");
                basicInfo_QuerySql.Append("'" + string.Empty + "',");
                basicInfo_QuerySql.Append("'" + DateTime.Now + "')");
                SqlHelper.ExecuteNonQuery(basicInfo_QuerySql.ToString());

                //保存光盘基本信息
                for (int i = 0; i < dgv_CDlist.RowCount - 1; i++)
                {
                    string cdName = dgv_CDlist.Rows[i].Cells[0].Value.ToString();
                    string cdCode = dgv_CDlist.Rows[i].Cells[1].Value.ToString();
                    string cdRemark = dgv_CDlist.Rows[i].Cells[2].Value != null ? dgv_CDlist.Rows[i].Cells[2].Value.ToString() : string.Empty;
                    CD cd = new CD()
                    {
                        TrcId = Guid.NewGuid().ToString(),
                        TrcName = cdName,
                        TrcCode = cdCode,
                        TrpId = registration.Id,//关联批次的主键
                        TrcRemark = cdRemark
                    };
                    StringBuilder cdInfo_querySql = new StringBuilder("INSERT INTO transfer_registraion_cd ");
                    cdInfo_querySql.Append("(trc_id,trc_name,trc_code,trp_id,trc_remark,trc_people,trc_handle_time)");
                    cdInfo_querySql.Append(" VALUES(");
                    cdInfo_querySql.Append("'" + cd.TrcId + "',");
                    cdInfo_querySql.Append("'" + cd.TrcName + "',");
                    cdInfo_querySql.Append("'" + cd.TrcCode + "',");
                    cdInfo_querySql.Append("'" + cd.TrpId + "',");
                    cdInfo_querySql.Append("'" + cd.TrcRemark + "',");
                    cdInfo_querySql.Append("'" + string.Empty + "',");
                    cdInfo_querySql.Append("'" + DateTime.Now + "')");
                    SqlHelper.ExecuteNonQuery(cdInfo_querySql.ToString());
                }

                if (MessageBox.Show("保存成功，是否返回列表页", "恭喜", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk) == DialogResult.Yes)
                {
                    DialogResult = DialogResult.OK;
                    Close();
                }
            }
        }

        /// <summary>
        /// 检验数据的完整性（如果存在光盘，则名称和编号为必填）
        /// </summary>
        private bool ValidData()
        {
            for (int i = 0; i < dgv_CDlist.RowCount - 1; i++)
                if (dgv_CDlist.Rows[i].Cells[0].Value == null || dgv_CDlist.Rows[i].Cells[1].Value == null)
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
                if (e.ColumnIndex == 1)
                {
                    string number = dgv_CDlist.RowCount - 1 < 10 ? "0" + (dgv_CDlist.RowCount - 1) : (dgv_CDlist.RowCount - 1).ToString();
                    string pcCode = txt_BatchCode.Text + "-" + number;
                    dgv_CDlist.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = pcCode;
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
                string unitCode = SqlHelper.ExecuteOnlyOneQuery("SELECT cs_code FROM company_source WHERE cs_id='" + unitId + "'").ToString();
                CreateBatchCode(unitCode);
            }
        }

        private void dgv_CDlist_RowLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1 && e.ColumnIndex != -1)
            {
                //暂时搁置
            }
        }
    }
}
