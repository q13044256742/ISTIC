using DevExpress.XtraEditors;
using System;
using System.Text;
using System.Windows.Forms;

namespace 科技计划项目档案数据采集管理系统.TransferOfRegistration
{
    public partial class Frm_AddCD : DevExpress.XtraEditors.XtraForm
    {
        /// <summary>
        /// 批次ID
        /// </summary>
        private string TRP_ID;
        public Frm_AddCD(string pid)
        {
            this.TRP_ID = pid;
            InitializeComponent();
        }

        private void Btn_Save_Click(object sender, System.EventArgs e)
        {
            string name = txt_CDName.Text.Trim();
            string code = txt_CDCode.Text.Trim();
            string remark = txt_CDRemark.Text.Trim();
            object sort = SqlHelper.ExecuteOnlyOneQuery($"SELECT MAX(trc_sort) FROM transfer_registraion_cd WHERE trp_id='{TRP_ID}'") ?? -1;
            int _sort = ToolHelper.GetIntValue(sort) + 1;

            if(string.IsNullOrEmpty(name) || string.IsNullOrEmpty(code))
                XtraMessageBox.Show("请先将表单补充完整!");
            else
            {
                CDEntity cd = new CDEntity()
                {
                    TrcId = Guid.NewGuid().ToString(),
                    TrcName = name,
                    TrcCode = code,
                    TrpId = TRP_ID,
                    TrcRemark = remark
                };
                StringBuilder cdInfo_querySql = new StringBuilder("INSERT INTO transfer_registraion_cd ");
                cdInfo_querySql.Append("(trc_id, trc_name, trc_code, trp_id, trc_remark, trc_status, trc_complete_status, trc_people, trc_handle_time, trc_sort)");
                cdInfo_querySql.Append(" VALUES(");
                cdInfo_querySql.Append("'" + cd.TrcId + "',");
                cdInfo_querySql.Append("'" + cd.TrcName + "',");
                cdInfo_querySql.Append("'" + cd.TrcCode + "',");
                cdInfo_querySql.Append("'" + cd.TrpId + "',");
                cdInfo_querySql.Append("'" + cd.TrcRemark + "',");
                cdInfo_querySql.Append("'" + (int)ReadStatus.NonRead + "',");
                cdInfo_querySql.Append("'" + (int)WorkStatus.NonWork + "',");
                cdInfo_querySql.Append("'" + string.Empty + "',");
                cdInfo_querySql.Append("'" + DateTime.Now + "', '" + _sort + "')");
                SqlHelper.ExecuteNonQuery(cdInfo_querySql.ToString());

                //刷新批次下的光盘数
                string refreshSql = "UPDATE transfer_registration_pc SET trp_cd_amount= " +
                    "(SELECT COUNT(trc_id) FROM transfer_registraion_cd WHERE trp_id = '" + TRP_ID + "') WHERE trp_id = '" + TRP_ID + "'";
                SqlHelper.ExecuteNonQuery(refreshSql);

                if(XtraMessageBox.Show("保存成功，是否返回列表页?","操作成功", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk) == DialogResult.Yes)
                {
                    DialogResult = DialogResult.OK;
                    Close();
                }
            }
        }

        private void Frm_AddCD_Load(object sender, EventArgs e)
        {
            string querySql = $"SELECT trp_name FROM transfer_registration_pc WHERE trp_id='{TRP_ID}'";
            string name = ToolHelper.GetValue(SqlHelper.ExecuteOnlyOneQuery(querySql));
            lbl_PCName.Text = name;
            Txt_CDCode_Enter(null, null);
        }

        private void Txt_CDCode_Enter(object sender, EventArgs e)
        {
            object[] vals = SqlHelper.ExecuteRowsQuery($"SELECT trp_code, trp_cd_amount+1 FROM transfer_registration_pc WHERE trp_id='{TRP_ID}'");
            if (vals != null && vals.Length == 2)
            {
                string Pcode = ToolHelper.GetValue(vals[0]);
                string Pamount = ToolHelper.GetValue(vals[1]);
                txt_CDCode.Text = Pcode + "-" + Pamount.PadLeft(3, '0');
            }
        }

        private string GetValue(object value) => value == null ? string.Empty : value.ToString();

        /// <summary>
        /// 自动生成批次编号
        /// </summary>
        private string CreateBatchCode(object unitCode, int index)
        {
            string querySql = null;
            //自动生成批次编号
            object csid = SqlHelper.ExecuteOnlyOneQuery("SELECT dd_id FROM data_dictionary WHERE dd_code = '" + unitCode + "'");
            querySql = "SELECT COUNT(*) FROM transfer_registration_pc WHERE com_id=('" + csid + "')";
            object amountStr = SqlHelper.ExecuteOnlyOneQuery(querySql);
            return unitCode.ToString() + DateTime.Now.Year + amountStr.ToString().PadLeft(3, '0') + "-" + index.ToString().PadLeft(3, '0');
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
