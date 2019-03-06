using System;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace 科技计划项目档案数据采集管理系统
{
    public partial class Frm_ProTypeSelect : DevExpress.XtraEditors.XtraForm
    {
        /// <summary>
        /// 来源类型
        /// </summary>
        private WorkType workType;
        private object objId;
        public object unitCode;
        public object trcId;
        public Frm_ProTypeSelect(WorkType workType, object objId)
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
            if(cbo_TypeSelect.Items.Count > 0)
                cbo_TypeSelect.SelectedIndex = 0;
        }

        private void Btn_Sure_Click(object sender, EventArgs e)
        {
            Hide();
            object obj = SqlHelper.ExecuteOnlyOneQuery($"SELECT dd_code FROM data_dictionary WHERE dd_id='{cbo_TypeSelect.SelectedValue}'");
            WorkType _type = WorkType.Default;
            ControlType _ctype = ControlType.Default;
            if("ZX".Equals(obj))
            {
                if(workType == WorkType.PaperWork)
                    _type = WorkType.PaperWork_Imp;
                else if(workType == WorkType.CDWork)
                    _type = WorkType.CDWork_Imp;
                _ctype = ControlType.Imp;
            }
            //else if("YF".Equals(obj))
            //{
            //    if(workType == WorkType.PaperWork)
            //        _type = WorkType.PaperWork_Special;
            //    else if(workType == WorkType.CDWork)
            //        _type = WorkType.CDWork_Special;
            //    _ctype = ControlType.Special;
            //}
            else
            {
                if(workType == WorkType.PaperWork)
                    _type = WorkType.PaperWork_Plan;
                else if(workType == WorkType.CDWork)
                    _type = WorkType.CDWork_Plan;
                _ctype = ControlType.Plan;
            }
            if(_type != WorkType.Default)
            {
                Frm_MyWork frm = new Frm_MyWork(_type, cbo_TypeSelect.SelectedValue, objId, _ctype, false);
                frm.planCode = obj;
                frm.unitCode = unitCode;
                frm.trcId = trcId;
                frm.Show();
            }
        }

        private void cbo_TypeSelect_SelectionChangeCommitted(object sender, EventArgs e)
        {
            string querySQL = $"SELECT dd_code FROM data_dictionary WHERE dd_id='{cbo_TypeSelect.SelectedValue}'";
            object sorCode = SqlHelper.ExecuteOnlyOneQuery(querySQL);

            querySQL = $"SELECT pi_id FROM project_info WHERE pi_categor=1 AND pi_source_id='{sorCode}' AND pi_orga_id='{unitCode}'";
            object id = SqlHelper.ExecuteOnlyOneQuery(querySQL);
            if(id != null)
            {
                DialogResult dialogResult = DevExpress.XtraEditors.XtraMessageBox.Show("所选计划在当前来源单位已录入，是否补录数据？", "确认提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if(dialogResult == DialogResult.Yes)
                {
                    InheritImpInfo(trcId, id);
                    ResetProjectState(id);
                    DevExpress.XtraEditors.XtraMessageBox.Show("操作成功，再次点击即可。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    Close();
                }
            }
        }

        /// <summary>
        /// 将已有计划绑定新批次
        /// </summary>
        private void InheritImpInfo(object trpId, object planId)
        {
            string updateSQL = $"UPDATE project_info SET trc_id='{trpId}' WHERE pi_id='{planId}';";
            SqlHelper.ExecuteNonQuery(updateSQL);
        }

        /// <summary>
        /// 重置指定计划下所有项目课题状态为 未提交
        /// </summary>
        /// <param name="id">计划ID</param>
        private void ResetProjectState(object id)
        {
            string updateSQL = string.Empty;
            string proQuerySql = "SELECT A.pi_id FROM project_info p LEFT JOIN ( " +
                "SELECT pi_id, pi_obj_id FROM project_info WHERE pi_categor=2 UNION ALL " +
                "SELECT ti_id, ti_obj_id FROM topic_info WHERE ti_categor=-3)A ON A.pi_obj_id=p.pi_id " +
               $"WHERE p.pi_id='{id}' AND A.pi_id IS NOT NULL";
            object[] proIds = SqlHelper.ExecuteSingleColumnQuery(proQuerySql);
            string pids = ToolHelper.GetFullStringBySplit(proIds, ",", "'");
            updateSQL += 
                $"UPDATE project_info SET pi_submit_status=1 WHERE pi_id IN ({pids});" +
                $"UPDATE topic_info SET ti_submit_status=1 WHERE ti_id IN ({pids});";

            string topQuerySql = "SELECT A.ti_id FROM( " +
                "SELECT ti_id, ti_obj_id FROM topic_info WHERE ti_categor = 3 UNION ALL " +
                "SELECT si_id, si_obj_id FROM subject_info )A " +
               $"WHERE A.ti_obj_id IN({pids})";
            object[] topIds = SqlHelper.ExecuteSingleColumnQuery(topQuerySql);
            string tids = ToolHelper.GetFullStringBySplit(topIds, ",", "'");
            updateSQL +=
               $"UPDATE subject_info SET si_submit_status=1 WHERE si_id IN ({tids});" +
               $"UPDATE topic_info SET ti_submit_status=1 WHERE ti_id IN ({tids});" +
               $"UPDATE subject_info SET si_submit_status=1 WHERE si_obj_id IN ({tids});";
            SqlHelper.ExecuteNonQuery(updateSQL);
        }
    }
}
