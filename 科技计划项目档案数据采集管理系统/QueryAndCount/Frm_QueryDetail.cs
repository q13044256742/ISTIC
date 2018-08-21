using System.Data;

namespace 科技计划项目档案数据采集管理系统
{
    public partial class Frm_QueryDetail : DevExpress.XtraEditors.XtraForm
    {
        private DataRow row;
        public Frm_QueryDetail(DataRow row)
        {
            InitializeComponent();
            this.row = row;
        }

        private void Frm_QueryDetail_Load(object sender, System.EventArgs e)
        {
            txt_Project_Code.Text = ToolHelper.GetValue(row["pi_code"]);
            txt_Project_Name.Text = ToolHelper.GetValue(row["pi_name"]);
            txt_Project_Field.Text = ToolHelper.GetValue(row["pi_field"]);
            txt_Project_Theme.Text = ToolHelper.GetValue(row["pb_theme"]);
            txt_Project_StartTime.Text = ToolHelper.GetValue(row["pi_start_datetime"]);
            txt_Project_EndTime.Text = ToolHelper.GetValue(row["pi_end_datetime"]);
            txt_Project_Unit.Text = ToolHelper.GetValue(row["pi_unit"]);
            txt_Project_Province.Text = ToolHelper.GetValue(row["pi_province"]);
            txt_Project_ProUser.Text = ToolHelper.GetValue(row["pi_prouser"]);
            txt_Project_Funds.Text = ToolHelper.GetValue(row["pi_funds"]);
            txt_Project_Year.Text = ToolHelper.GetValue(row["pi_year"]);
            txt_Project_UnitUser.Text = ToolHelper.GetValue(row["pi_uniter"]);
            txt_Project_Intro.Text = ToolHelper.GetValue(row["pi_intro"]);

            txt_Project_Intro.SelectionStart = 0;
            Btn_Close.Focus();
        }

        private void Btn_Close_Click(object sender, System.EventArgs e)
        {
            Close();
        }
    }
}
