using System;
using System.Data;

namespace 科技计划项目档案数据采集管理系统
{
    public partial class Frm_ProTypeSelect : DevExpress.XtraEditors.XtraForm
    {
        private WorkType workType;
        private object objId;
        public object unitCode;
        public Frm_ProTypeSelect(WorkType workType,object objId)
        {
            

            InitializeComponent();
            this.workType = workType;
            this.objId = objId;
        }

        private void Frm_ProTypeSelect_Load(object sender, EventArgs e)
        {
            string planKey = "dic_key_plan";
            string querySql = $"SELECT dd_id,dd_name FROM data_dictionary WHERE dd_pid=(SELECT top(1) dd_id FROM data_dictionary WHERE dd_code = '{planKey}') ORDER BY dd_sort";
            DataTable table = SqlHelper.ExecuteQuery(querySql);
            cbo_TypeSelect.DataSource = table;
            cbo_TypeSelect.DisplayMember = "dd_name";
            cbo_TypeSelect.ValueMember = "dd_id";
            if (cbo_TypeSelect.Items.Count > 0)
                cbo_TypeSelect.SelectedIndex = 0;
        }

        private void Btn_Sure_Click(object sender, EventArgs e)
        {
            Hide();
            object obj = SqlHelper.ExecuteOnlyOneQuery($"SELECT dd_code FROM data_dictionary WHERE dd_id='{cbo_TypeSelect.SelectedValue}'");
            if("dic_plan_imp".Equals(obj))
            {
                Frm_MyWork frm = new Frm_MyWork(WorkType.Default, cbo_TypeSelect.SelectedValue, objId, ControlType.Imp, false);
                frm.planCode = obj;
                frm.unitCode = unitCode;
                frm.DEV_TYPE = 0;
                frm.ShowDialog();
            }
            else if("dic_imp_dev".Equals(obj))
            {
                Frm_MyWork frm = new Frm_MyWork(WorkType.Default, cbo_TypeSelect.SelectedValue, objId, ControlType.Special, false);
                frm.planCode = obj;
                frm.unitCode = unitCode;
                frm.DEV_TYPE = 1;
                frm.ShowDialog();
            }
            else
            {
                Frm_MyWork frm = new Frm_MyWork(workType, cbo_TypeSelect.SelectedValue, objId, ControlType.Default, false);
                frm.planCode = obj;
                frm.unitCode = unitCode;
                frm.ShowDialog();
            }
        }
    }
}
