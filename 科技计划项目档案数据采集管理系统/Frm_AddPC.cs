using System;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace 科技计划项目档案数据采集管理系统
{
    public partial class Frm_AddPC : Form
    {
        public Frm_AddPC()
        {
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

            cbo_SourceUnit.SelectedIndex = 0;
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
            if (MessageBox.Show("确定要保存当前数据吗?", "确认提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk) == DialogResult.OK)
            {
                string batchName = txt_BatchName.Text.Trim();
                string batchCode = txt_BatchCode.Text.Trim();
                string sourceUnit = cbo_SourceUnit.SelectedItem.ToString();
                DateTime transferTime = dtp_TransferTime.Value;
                string receiver = txt_Receiver.Text.Trim();
                string giver = txt_giver.Text.Trim();
                string remark = txt_Remark.Text.Trim();
                string fileUpload = txt_UploadFile.Text.Trim();

                TransferOfRegist registration = new TransferOfRegist()
                {
                    BatchName = batchName,
                    BatchCode = batchCode,
                    SourceUnit = sourceUnit,
                    TransferTime = transferTime,
                    Receive = receiver,
                    Giver = giver,
                    Remark = remark,
                    FileUpload = fileUpload
                };

                StringBuilder nonQuerySql = new StringBuilder("INSERT INTO transfer_registration_pc ");
                nonQuerySql.Append("(trp_id,com_id,trp_name,trp_code,trp_log_data,trp_receiver,trp_giver,trp_remark,trp_attachment_id,trp_people,trp_handle_time) VALUES(");
                nonQuerySql.Append("'" + Guid.NewGuid().ToString() + "',");
                nonQuerySql.Append("'" + sourceUnit + "',");
                nonQuerySql.Append("'" + batchName + "',");
                nonQuerySql.Append("'" + batchCode + "',");
                nonQuerySql.Append("'" + transferTime + "',");
                nonQuerySql.Append("'" + receiver + "',");
                nonQuerySql.Append("'" + giver + "',");
                nonQuerySql.Append("'" + remark + "',");
                nonQuerySql.Append("'" + fileUpload + "',");
                nonQuerySql.Append("'" + string.Empty + "',");
                nonQuerySql.Append("'" + DateTime.Now + "')");
                SqlHelper.ExecuteNonQuery(nonQuerySql.ToString());
                if (MessageBox.Show("保存成功，是否返回列表页", "恭喜", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk) == DialogResult.Yes)
                {
                    DialogResult = DialogResult.OK;
                    Close();
                }
            }
        }
    }
}
