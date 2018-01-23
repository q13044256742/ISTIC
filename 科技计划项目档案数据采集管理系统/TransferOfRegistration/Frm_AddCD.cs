using System;
using System.Text;
using System.Windows.Forms;

namespace 科技计划项目档案数据采集管理系统.TransferOfRegistration
{
    public partial class Frm_AddCD : Form
    {
        private string pid;
        public Frm_AddCD(string pid)
        {
            this.pid = pid;
            InitializeComponent();
        }

        private void btn_Save_Click(object sender, System.EventArgs e)
        {
            string name = txt_CDName.Text.Trim();
            string code = txt_CDCode.Text.Trim();
            string remark = txt_CDRemark.Text.Trim();
            if(string.IsNullOrEmpty(name) || string.IsNullOrEmpty(code))
                MessageBox.Show("请先将表单补充完整!");
            else
            {
                CDEntity cd = new CDEntity()
                {
                    TrcId = Guid.NewGuid().ToString(),
                    TrcName = name,
                    TrcCode = code,
                    TrpId = pid,
                    TrcReadStatus = ReadStatus.NonRead,
                    TrcRemark = remark
                };
                StringBuilder cdInfo_querySql = new StringBuilder("INSERT INTO transfer_registraion_cd ");
                cdInfo_querySql.Append("(trc_id,trc_name,trc_code,trp_id,trc_remark,trc_status,trc_people,trc_handle_time)");
                cdInfo_querySql.Append(" VALUES(");
                cdInfo_querySql.Append("'" + cd.TrcId + "',");
                cdInfo_querySql.Append("'" + cd.TrcName + "',");
                cdInfo_querySql.Append("'" + cd.TrcCode + "',");
                cdInfo_querySql.Append("'" + cd.TrpId + "',");
                cdInfo_querySql.Append("'" + cd.TrcRemark + "',");
                cdInfo_querySql.Append("'" + (int)cd.TrcReadStatus + "',");
                cdInfo_querySql.Append("'" + string.Empty + "',");
                cdInfo_querySql.Append("'" + DateTime.Now + "')");
                SqlHelper.ExecuteNonQuery(cdInfo_querySql.ToString());

                //刷新批次下的光盘数
                string refreshSql = "UPDATE transfer_registration_pc SET trp_cd_amount= " +
                    "(SELECT COUNT(*) FROM transfer_registraion_cd WHERE trp_id = '" + pid + "') " +
                    "WHERE trp_id = '" + pid + "'";
                SqlHelper.ExecuteNonQuery(refreshSql);

                if(MessageBox.Show("保存成功，是否返回列表页?","操作成功", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk) == DialogResult.Yes)
                {
                    DialogResult = DialogResult.OK;
                    Close();
                }
            }
        }

        private void Frm_AddCD_Load(object sender, EventArgs e)
        {
            string querySql = $"SELECT trp_name FROM transfer_registration_pc WHERE trp_id='{pid}'";
            string name = Convert.ToString(SqlHelper.ExecuteOnlyOneQuery(querySql));
            lbl_PCName.Text = name;
        }
    }
}
