using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace 科技计划项目档案数据采集管理系统
{
    public partial class BatchRecInfo : DevExpress.XtraEditors.XtraForm
    {
        /// <summary>
        /// 批次ID
        /// </summary>
        private object TRP_ID;
        public BatchRecInfo(object trpID)
        {
            TRP_ID = trpID;
            InitializeComponent();
            view.RowHeadersDefaultCellStyle = DataGridViewStyleHelper.GetHeaderStyle();
        }

        private void BatchRecInfo_Load(object sender, EventArgs e)
        {
            string querySQL= "SELECT C.trp_code, B.real_name, A.wr_source_id, A.wr_date FROM work_registration A " +
                 "LEFT JOIN user_list B ON B.ul_id = A.wr_source_id " +
                 "LEFT JOIN transfer_registration_pc C on A.trp_id = C.trp_id " +
                $"WHERE A.trp_id = '{TRP_ID}' ORDER BY A.wr_date ";
            DataTable table = SqlHelper.ExecuteQuery(querySQL);
            foreach (DataRow row in table.Rows)
            {
                int i = view.Rows.Add();
                view.Rows[i].Cells[0].Value = row["trp_code"];
                view.Rows[i].Cells[1].Value = ToolHelper.GetDateValue(row["wr_date"], "yyyy-MM-dd HH:mm");
                view.Rows[i].Cells[2].Value = row["real_name"];
                view.Rows[i].Cells[3].Value = GetWorkAmount(TRP_ID, row["wr_source_id"]);
            }
        }

        /// <summary>
        /// 获取指定批次下制定人员的加工量
        /// </summary>
        /// <param name="trpID"></param>
        /// <param name="userId">用户ID</param>
        private int GetWorkAmount(object trpID, object userId)
        {
            int amount = 0;
            object impId = SqlHelper.ExecuteOnlyOneQuery($"SELECT imp_id FROM imp_info WHERE imp_obj_id='{trpID}'");
            //重大专项
            if (impId != null)
            {
                object specialID = SqlHelper.ExecuteOnlyOneQuery($"SELECT imp_id FROM imp_dev_info WHERE imp_obj_id='{impId}'");
                if (specialID != null)
                {
                    //专项下的项目/课题数
                    object[] proIds = SqlHelper.ExecuteSingleColumnQuery("SELECT pi_id FROM(" +
                         "SELECT pi_id, pi_worker_id, pi_obj_id FROM project_info WHERE pi_categor = 2 UNION ALL " +
                         "SELECT ti_id, ti_worker_id, ti_obj_id FROM topic_info WHERE ti_categor = -3)A " +
                        $"WHERE pi_worker_id = '{userId}' AND pi_obj_id='{specialID}'");
                    amount += proIds.Length;
                    foreach (object proId in proIds)
                    {
                        object[] topIds = SqlHelper.ExecuteSingleColumnQuery("SELECT si_id FROM(" +
                             "SELECT si_id, si_worker_id, si_obj_id FROM subject_info UNION ALL " +
                             "SELECT ti_id, ti_worker_id, ti_obj_id FROM topic_info WHERE ti_categor = 3)A " +
                            $"WHERE si_worker_id = '{userId}' AND si_obj_id='{proId}'");
                        amount += topIds.Length;
                        foreach (object topId in topIds)
                        {
                            object[] subIds = SqlHelper.ExecuteSingleColumnQuery("SELECT si_id FROM subject_info " +
                               $"WHERE si_worker_id = '{userId}' AND si_obj_id='{topId}'");
                            amount += subIds.Length;
                        }
                    }

                }
            }
            else
            {
                impId = SqlHelper.ExecuteCountQuery($"SELECT pi_id FROM project_info WHERE pi_categor=1 AND trc_id='{trpID}'");
                //计划
                if (impId != null)
                {
                    //专项下的项目/课题数
                    object[] proIds = SqlHelper.ExecuteSingleColumnQuery("SELECT pi_id FROM(" +
                         "SELECT pi_id, pi_worker_id, pi_obj_id FROM project_info WHERE pi_categor = 2 UNION ALL " +
                         "SELECT ti_id, ti_worker_id, ti_obj_id FROM topic_info WHERE ti_categor = -3)A " +
                        $"WHERE pi_worker_id = '{userId}' AND pi_obj_id='{impId}'");
                    amount += proIds.Length;
                    foreach (object proId in proIds)
                    {
                        object[] topIds = SqlHelper.ExecuteSingleColumnQuery("SELECT si_id FROM(" +
                             "SELECT si_id, si_worker_id, si_obj_id FROM subject_info UNION ALL " +
                             "SELECT ti_id, ti_worker_id, ti_obj_id FROM topic_info WHERE ti_categor = 3)A " +
                            $"WHERE si_worker_id = '{userId}' AND si_obj_id='{proId}'");
                        amount += topIds.Length;
                        foreach (object topId in topIds)
                        {
                            object[] subIds = SqlHelper.ExecuteSingleColumnQuery("SELECT si_id FROM subject_info " +
                               $"WHERE si_worker_id = '{userId}' AND si_obj_id='{topId}'");
                            amount += subIds.Length;
                        }
                    }
                }
            }
            return amount;
        }
    }
}
