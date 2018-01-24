using System;
using System.Data;

namespace 科技计划项目档案数据采集管理系统
{
    public partial class Frm_ProTypeSelect : DevExpress.XtraEditors.XtraForm
    {
        private WorkType workType;
        private object objId;
        public Frm_ProTypeSelect(WorkType workType,object objId)
        {
            InitializeComponent();
            this.workType = workType;
            this.objId = objId;
        }

        private void Frm_ProTypeSelect_Load(object sender, EventArgs e)
        {
            string planKey = "dic_key_plan";
            string querySql = $"SELECT dd_id,dd_name FROM data_dictionary WHERE dd_pid=(SELECT dd_id FROM data_dictionary WHERE dd_code = '{planKey}') ORDER BY dd_sort";
            DataTable table = SqlHelper.ExecuteQuery(querySql);
            cbo_TypeSelect.DataSource = table;
            cbo_TypeSelect.DisplayMember = "dd_name";
            cbo_TypeSelect.ValueMember = "dd_id";
            if (cbo_TypeSelect.Items.Count > 0)
                cbo_TypeSelect.SelectedIndex = 0;
        }

        private void btn_Sure_Click(object sender, EventArgs e)
        {
            Frm_MyWork frm = new Frm_MyWork(workType, objId, cbo_TypeSelect.SelectedValue);
            Hide();
            frm.ShowDialog();
        }
    }
}
