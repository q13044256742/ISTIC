using System;
using System.Data;

namespace 科技计划项目档案数据采集管理系统
{
    public partial class Frm_DomNeed : DevExpress.XtraEditors.XtraForm
    {
        private object trpId;
        public Frm_DomNeed(object trpId)
        {
            InitializeComponent();
            this.trpId = trpId;
        }

        private void Frm_DomRec_Load(object sender, System.EventArgs e)
        {
            DataRow dataRow = SqlHelper.ExecuteSingleRowQuery($"SELECT * FROM transfer_registration_pc WHERE trp_id='{trpId}'");
            DateTime date = Convert.ToDateTime(dataRow["trp_log_data"]);

            lbl_Body.Text = "  重大专项管理办公室：\n";
            lbl_Body.Text += $"  今中国科学技术信息研究所于 {date.Year} 年 {date.Month} 月收到贵单位提交的 {0} 捆质档案，电子档案 {0} 件，并于 {DateTime.Now.Year} 年 {DateTime.Now.Month} 月完成确认，共计纸质档案 {0} 卷，电子档案 {0} 件。" +
                $"按照《归档范围表》，缺失文件（必备） {0} 件（附件一），请尽快提交缺失的文件，若确实没有，请返回缺失文件清单并注明缺失原因。";

            lbl_Code.Text += "APTX-4869";
            lbl_Recever.Text += UserHelper.GetInstance().User.RealName;
            lbl_ReceDate.Text += DateTime.Now.ToString("yyyy-MM-dd");

            txt_Head.Focus();
        }
    }
}
