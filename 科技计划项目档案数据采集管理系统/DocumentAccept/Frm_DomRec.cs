using System;
using System.Data;

namespace 科技计划项目档案数据采集管理系统
{
    public partial class Frm_DomRec : DevExpress.XtraEditors.XtraForm
    {
        private object trpId;
        public Frm_DomRec(object trpId)
        {
            InitializeComponent();
            this.trpId = trpId;
        }

        private void Frm_DomRec_Load(object sender, EventArgs e)
        {
            //来源单位
            DataRow row = SqlHelper.ExecuteSingleRowQuery("SELECT dd_name, trp_log_data FROM transfer_registration_pc " +
               $"LEFT JOIN data_dictionary ON dd_id = com_id WHERE trp_id = '{trpId}';");
            if(row != null)
            {
                lbl_Body.Text = $"  {row["dd_name"]}：\n";
                lbl_Body.Text += $"  今中国科学技术信息研究所于{ToolHelper.GetDateValue(row["trp_log_data"], "yyyy年MM月")}收到贵单位提交的    捆纸质档案， 电子档案    件，" +
                    $"并于{ToolHelper.GetDateValue(DateTime.Now, "yyyy年MM月")}完成确认，" +
                    $"共计纸质档案    卷，电子档案    件，并附案卷列表（附件一）。 此据。";
            }
            txt_Head.Focus();
        }
    }
}
