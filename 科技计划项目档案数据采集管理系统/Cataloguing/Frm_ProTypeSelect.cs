using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace 科技计划项目档案数据采集管理系统
{
    public partial class Frm_ProTypeSelect : XtraForm
    {
        /// <summary>
        /// 来源类型
        /// </summary>
        private WorkType workType;
        private object objId;
        public object unitCode;
        /// <summary>
        /// 批次ID
        /// </summary>
        public object batchId;
        public Frm_ProTypeSelect(WorkType workType, object objId)
        {
            InitializeComponent();
            this.workType = workType;
            this.objId = objId;
        }

        private void Frm_ProTypeSelect_Load(object sender, EventArgs e)
        {
            string planKey = "dic_key_plan";
            string querySql = $"SELECT dd_id, dd_name FROM data_dictionary WHERE dd_pid=(SELECT top(1) dd_id FROM data_dictionary WHERE dd_code = '{planKey}') ORDER BY dd_sort";
            DataTable table = SqlHelper.ExecuteQuery(querySql);
            cbo_TypeSelect.DataSource = table;
            cbo_TypeSelect.DisplayMember = "dd_name";
            cbo_TypeSelect.ValueMember = "dd_id";

            string key = "dic_key_project";
            DataTable speTable = SqlHelper.ExecuteQuery($"SELECT dd_id, dd_name FROM data_dictionary WHERE dd_pId=(SELECT dd_id FROM data_dictionary WHERE dd_code='{key}') ORDER BY dd_sort");
            cbo_SpecialType.DataSource = speTable;
            cbo_SpecialType.DisplayMember = "dd_name";
            cbo_SpecialType.ValueMember = "dd_id";

            listbox.DrawItem += Listbox_DrawItem;
        }

        private void Listbox_DrawItem(object sender, ListBoxDrawItemEventArgs e)
        {
            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
            {
                e.Appearance.ForeColor = Color.DarkRed;
            }
        }

        private void Btn_Sure_Click(object sender, EventArgs e)
        {
            object planCode = SqlHelper.ExecuteOnlyOneQuery($"SELECT dd_code FROM data_dictionary WHERE dd_id='{cbo_TypeSelect.SelectedValue}'");
            object planName = cbo_TypeSelect.Text;
            bool isSpecial = pal_Special.Enabled;//是否是专项
            if (listbox.SelectedItems.Count > 0)
            {
                string queryString = $"是否确定要补录选中的{listbox.SelectedItems.Count}个批次？";
                if (XtraMessageBox.Show(queryString, "确认提示", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk) == DialogResult.Yes)
                {
                    StringBuilder sb = new StringBuilder($"DELETE FROM batch_relevance WHERE br_main_id='{batchId}';");
                    List<object> batchIds = new List<object>();
                    foreach (object item in listbox.SelectedItems)
                    {
                        SelectEntity entity = (SelectEntity)item;
                        batchIds.Add(entity.batchId);
                    }
                    sb.Append($"INSERT INTO batch_relevance(br_id, br_main_id, br_auxiliary_id, br_type) " +
                        $"VALUES('{Guid.NewGuid().ToString()}', '{batchId}', '{string.Join(",", batchIds.ToArray())}', {(isSpecial ? 2 : 1)});");
                    if (isSpecial)
                    {
                        string primaryKey = Guid.NewGuid().ToString();
                        string subPriKey = Guid.NewGuid().ToString();
                        object speCode = SqlHelper.GetValueByKey(cbo_SpecialType.SelectedValue, "dd_code");
                        object speName = SqlHelper.GetValueByKey(cbo_SpecialType.SelectedValue, "dd_name");
                        //重大专项
                        sb.Append("INSERT INTO imp_info(imp_id, imp_code, imp_name, pi_categor, imp_submit_status, imp_obj_id, imp_source_id, imp_type) " +
                            $"VALUES ('{primaryKey}', 'ZX', '国家重大专项', 5, 1, '{batchId}', '{UserHelper.GetUser().UserKey}', 5);");
                        //专项
                        sb.Append("INSERT INTO imp_dev_info(imp_id, imp_code, imp_name, pi_categor, imp_submit_status, imp_obj_id, imp_source_id) " +
                            $"VALUES ('{subPriKey}', '{speCode}', '{speName}', 6, 1, '{primaryKey}', '{UserHelper.GetUser().UserKey}');");
                    }
                    else
                    {
                        string primaryKey = Guid.NewGuid().ToString();
                        sb.Append("INSERT INTO project_info (pi_id, pi_code, pi_name, pi_obj_id, pi_categor, pi_submit_status, pi_source_id, pi_orga_id, pi_worker_id, pi_worker_date) VALUES" +
                            $"('{primaryKey}', '{planCode}', '{planName}', '{batchId}', 1, 1, '{planCode}', '{unitCode}', '{UserHelper.GetUser().UserKey}', '{DateTime.Now}');");
                    }
                    SetStateToUnsubmit(isSpecial ? 2 : 1, batchIds.ToArray());
                    SqlHelper.ExecuteNonQuery(sb.ToString());
                    XtraMessageBox.Show("操作成功，请重新进入。", "温馨提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    Close();
                }
                return;
            }
            Hide();

            WorkType _type = WorkType.Default;
            ControlType _ctype = ControlType.Default;
            if ("ZX".Equals(planCode))
            {
                if (workType == WorkType.PaperWork)
                    _type = WorkType.PaperWork_Imp;
                else if (workType == WorkType.CDWork)
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
                if (workType == WorkType.PaperWork)
                    _type = WorkType.PaperWork_Plan;
                else if (workType == WorkType.CDWork)
                    _type = WorkType.CDWork_Plan;
                _ctype = ControlType.Plan;
            }
            if (_type != WorkType.Default)
            {
                Frm_MyWork frm = new Frm_MyWork(_type, cbo_TypeSelect.SelectedValue, objId, _ctype, false);
                frm.planCode = planCode;
                frm.unitCode = unitCode;
                frm.trcId = batchId;
                frm.Show();
            }
        }
        
        private class SelectEntity
        {
            public object batchId;
            public object batchCode;
            public object batchName;
            public override string ToString()
            {
                return $"[{batchCode}]{batchName}";
            }
        }
        private void TypeSelect_SelectionChangeCommitted(object sender, EventArgs e)
        {
            object value = cbo_TypeSelect.SelectedValue;
            int index = cbo_TypeSelect.SelectedIndex, maxLength = cbo_TypeSelect.Items.Count;
            bool boo = index == maxLength - 1;
            pal_Special.Enabled = boo;
            //重大专项
            if (!boo)
            {
                string querySQL = $"SELECT dd_code FROM data_dictionary WHERE dd_id='{value}'";
                querySQL = $"SELECT pi_obj_id FROM project_info WHERE pi_categor=1 AND pi_source_id=({querySQL}) AND pi_orga_id='{unitCode}'";
                querySQL = $"SELECT trp_id, trp_code, trp_name FROM transfer_registration_pc WHERE trp_id IN({querySQL})";
                DataTable table = SqlHelper.ExecuteQuery(querySQL);
                boo = table.Rows.Count > 0;
                pal_BatchList.Enabled = boo;
                listbox.Items.Clear();
                if (boo)
                {
                    //如果所选计划有补录，则列出批次供选择
                    foreach (DataRow row in table.Rows)
                        listbox.Items.Add(new SelectEntity() { batchId = row["trp_id"], batchCode = row["trp_code"], batchName = row["trp_name"] });

                }
            }
        }

        /// <summary>
        /// 重置指定批次下所有数据状态为 未提交
        /// </summary>
        /// <param name="type">类型<para>1：计划</para><para>2：专项</para></param>
        /// <param name="batchIds">批次ID</param>
        private void SetStateToUnsubmit(int type, object[] batchIds)
        {
            string updateSQL = string.Empty;
            List<object> projectIds = new List<object>();
            if (type == 1)
            {
                object[] planIds = SqlHelper.ExecuteSingleColumnQuery($"SELECT pi_id FROM project_info WHERE pi_categor=1 AND pi_obj_id IN ({ToolHelper.GetStringBySplit(batchIds, ",", "'")})");
                string proQuerySql = "SELECT A.pi_id FROM project_info p LEFT JOIN ( " +
                    "SELECT pi_id, pi_obj_id FROM project_info WHERE pi_categor=2 UNION ALL " +
                    "SELECT ti_id, ti_obj_id FROM topic_info WHERE ti_categor=-3)A ON A.pi_obj_id=p.pi_id " +
                   $"WHERE p.pi_id IN ({ToolHelper.GetStringBySplit(planIds, ",", "'")})";
                object[] proIds = SqlHelper.ExecuteSingleColumnQuery(proQuerySql);
                projectIds.AddRange(proIds);
            }
            else if (type == 2)
            {
                string querySQL = "SELECT idi.imp_id FROM imp_dev_info idi INNER JOIN imp_info ii ON idi.imp_obj_id = ii.imp_id INNER JOIN transfer_registration_pc trp ON ii.imp_obj_id = trp.trp_id " +
                    $"WHERE trp.trp_id IN({ToolHelper.GetStringBySplit(batchIds, ",", "'")})";
                object[] specialIds = SqlHelper.ExecuteSingleColumnQuery(querySQL);
                string proQuerySql = "SELECT A.pi_id FROM imp_dev_info idi LEFT JOIN ( " +
                    "SELECT pi_id, pi_obj_id FROM project_info WHERE pi_categor=2 UNION ALL " +
                    "SELECT ti_id, ti_obj_id FROM topic_info WHERE ti_categor=-3)A ON A.pi_obj_id=idi.imp_id " +
                   $"WHERE idi.imp_id IN ({ToolHelper.GetStringBySplit(specialIds, ",", "'")})";
                object[] proIds = SqlHelper.ExecuteSingleColumnQuery(proQuerySql);
                projectIds.AddRange(proIds);
            }
            if (projectIds.Count > 0)
            {
                string pids = ToolHelper.GetStringBySplit(projectIds.ToArray(), ",", "'");
                updateSQL +=
                    $"UPDATE project_info SET pi_submit_status=1, pi_worker_id=null, pi_checker_id=null WHERE pi_id IN ({pids});" +
                    $"UPDATE topic_info SET ti_submit_status=1, ti_worker_id=null, ti_checker_id=null WHERE ti_id IN ({pids});";

                string topQuerySql = "SELECT A.ti_id FROM( " +
                    "SELECT ti_id, ti_obj_id FROM topic_info WHERE ti_categor = 3 UNION ALL " +
                    "SELECT si_id, si_obj_id FROM subject_info )A " +
                   $"WHERE A.ti_obj_id IN({pids})";
                object[] topIds = SqlHelper.ExecuteSingleColumnQuery(topQuerySql);
                if (topIds.Length > 0)
                {
                    string tids = ToolHelper.GetStringBySplit(topIds, ",", "'");
                    updateSQL +=
                       $"UPDATE subject_info SET si_submit_status=1, si_worker_id=null, si_checker_id=null WHERE si_id IN ({tids});" +
                       $"UPDATE topic_info SET ti_submit_status=1, ti_worker_id=null, ti_checker_id=null WHERE ti_id IN ({tids});" +
                       $"UPDATE subject_info SET si_submit_status=1, si_worker_id=null, si_checker_id=null WHERE si_obj_id IN ({tids});";
                }
            }
            SqlHelper.ExecuteNonQuery(updateSQL);
        }

        private void listbox_SelectedIndexChanged(object sender, EventArgs e)
        {
            int count = listbox.SelectedItems.Count;
            pal_BatchList.Text = $"可选择继承的批次(ctrl键可多选)[{count}]";
        }

        /// <summary>
        /// 专项选择是判断是否存在可补录的批次
        /// </summary>
        private void SpecialType_SelectionChangeCommitted(object sender, EventArgs e)
        {
            object value = cbo_SpecialType.SelectedValue;
            DataRow row = SqlHelper.ExecuteSingleRowQuery($"SELECT dd_code, dd_name, dd_note FROM data_dictionary WHERE dd_id='{value}'");
            if (row != null)
            {
                object specialCode = row["dd_code"];
                string querySQL = "SELECT trp.trp_id, trp.trp_code, trp.trp_name FROM transfer_registration_pc trp " +
                    "INNER JOIN imp_info ii ON ii.imp_obj_id = trp.trp_id " +
                    "INNER JOIN imp_dev_info idi ON idi.imp_obj_id = ii.imp_id " +
                    "INNER JOIN data_dictionary dd ON dd.dd_id = trp.com_id " +
                   $"WHERE idi.imp_code = '{specialCode}' AND dd.dd_code = '{unitCode}' ";
                DataTable table = SqlHelper.ExecuteQuery(querySQL);
                bool boo = table.Rows.Count > 0;
                pal_BatchList.Enabled = boo;
                listbox.Items.Clear();
                if (boo)
                {
                    foreach (DataRow dataRow in table.Rows)
                        listbox.Items.Add(new SelectEntity() { batchId = dataRow["trp_id"], batchCode = dataRow["trp_code"], batchName = dataRow["trp_name"] });
                }
            }
        }
    }
}