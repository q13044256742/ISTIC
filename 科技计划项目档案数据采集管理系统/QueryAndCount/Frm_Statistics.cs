using DevExpress.XtraBars.Navigation;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace 科技计划项目档案数据采集管理系统
{
    public partial class Frm_Statistics : DevExpress.XtraEditors.XtraForm
    {
        public Frm_Statistics()
        {
            InitializeComponent();
            view.ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle()
            {
                Font = new Font("微软雅黑", 12f, FontStyle.Bold),
                Padding = new Padding(0, 3, 0, 3),
                Alignment = DataGridViewContentAlignment.MiddleCenter,
            };
            view.DefaultCellStyle = DataGridViewStyleHelper.GetCellStyle();
            LoadCompanySource();
        }

        /// <summary>
        /// 加载来源单位列表
        /// </summary>
        private void LoadCompanySource()
        {
            string querySql = "SELECT dd_id, dd_name FROM data_dictionary WHERE dd_pId=" +
                "(SELECT dd_id FROM data_dictionary WHERE dd_code = 'dic_key_company_source') ORDER BY dd_sort";
            DataTable table = SqlHelper.ExecuteQuery(querySql);
            for(int i = 0; i < table.Rows.Count; i++)
            {
                AccordionControlElement element = new AccordionControlElement()
                {
                    Style = ElementStyle.Item,
                    Name = ToolHelper.GetValue(table.Rows[i]["dd_id"]),
                    Text = ToolHelper.GetValue(table.Rows[i]["dd_name"]),
                };
                acg_Register.Elements.Add(element);
            }
        }

        private void Frm_Statistics_Load(object sender, EventArgs e)
        {
            LoadDataList();
        }

        private void LoadDataList()
        {
            DataGridViewStyleHelper.ResetDataGridView(view, true);
            view.Columns.AddRange(new DataGridViewColumn[]
            {
                new DataGridViewTextBoxColumn(){ Name = "pName", HeaderText = "计划类别名称", FillWeight = 40, SortMode = DataGridViewColumnSortMode.NotSortable },
                new DataGridViewTextBoxColumn(){ Name = "pAmount", HeaderText = "项目/课题数", FillWeight = 20, SortMode = DataGridViewColumnSortMode.NotSortable },
                new DataGridViewTextBoxColumn(){ Name = "fAmount", HeaderText = "文件数", FillWeight = 20, SortMode = DataGridViewColumnSortMode.NotSortable },
                new DataGridViewTextBoxColumn(){ Name = "bAmount", HeaderText = "盒数", FillWeight = 20, SortMode = DataGridViewColumnSortMode.NotSortable },
            });
            string querySQL = $"SELECT dd_code, dd_name, ISNULL(amount, 0) amount FROM (" +
                $"SELECT dd_code, dd_name FROM data_dictionary WHERE dd_pId = " +
                $"(SELECT TOP(1) dd_id FROM data_dictionary WHERE dd_code = 'dic_key_plan')) B " +
                $"LEFT JOIN( " +
                $"SELECT pi.pi_code, pi.pi_name, COUNT(A.pi_obj_id) amount " +
                $"FROM project_info pi " +
                $"LEFT JOIN ( " +
                $"SELECT pi_obj_id FROM project_info WHERE pi_categor = 2 " +
                $"UNION ALL SELECT ti_obj_id FROM topic_info ti) A ON A.pi_obj_id = pi.pi_id " +
                $"WHERE pi.pi_categor = 1 GROUP BY pi.pi_name, pi.pi_code) C ON B.dd_code = C.pi_code " +
                $"WHERE(dd_code <> 'ZX' AND dd_code <> 'YF') " +
                $"UNION ALL " +
                $"SELECT dd_code, dd_name, COUNT(A.pi_id) amount FROM data_dictionary " +
                $"LEFT JOIN imp_dev_info ON imp_code = dd_code LEFT JOIN( " +
                $"SELECT pi_id, pi_obj_id FROM project_info WHERE pi_categor= 2 " +
                $"UNION ALL SELECT ti_id, ti_obj_id FROM topic_info) A ON A.pi_obj_id = imp_id " +
                $"WHERE dd_pId = (SELECT TOP(1) dd_id FROM data_dictionary WHERE dd_code = 'dic_key_project') " +
                $"GROUP BY dd_code, dd_name " +
                $"ORDER BY dd_code ";

            DataTable table = SqlHelper.ExecuteQuery(querySQL);
            foreach(DataRow row in table.Rows)
            {
                int rowIndex = view.Rows.Add();
                view.Rows[rowIndex].Cells["pName"].Value = row["dd_name"];
                view.Rows[rowIndex].Cells["pAmount"].Value = row["amount"];
                view.Rows[rowIndex].Cells["fAmount"].Value = GetFileAmount(row["dd_code"]);
                view.Rows[rowIndex].Cells["bAmount"].Value = GetBoxAmount(row["dd_code"]);
            }
            view.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            view.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        /// <summary>
        /// 获取指定计划类别下的盒数
        /// </summary>
        /// <param name="planTypeCode">计划类别编号</param>
        private object GetBoxAmount(object planTypeCode)
        {
            string querySQL = string.Empty;
            if(!ToolHelper.GetValue(planTypeCode).Contains("ZX"))
            {
                querySQL = "SELECT COUNT(pb.pb_id) FROM project_info pi " +
                  "LEFT JOIN( SELECT pi_id, pi_obj_id FROM project_info WHERE pi_categor= 2 " +
                  "UNION ALL SELECT ti_id, ti_obj_id FROM topic_info) A ON A.pi_obj_id = pi.pi_id " +
                  "LEFT JOIN processing_box pb ON pb.pb_obj_id = A.pi_id " +
                  $"WHERE pi.pi_code = '{planTypeCode}' GROUP BY pi.pi_code";
            }
            else
            {
                querySQL = "SELECT COUNT(pb.pb_id) FROM imp_dev_info idi " +
                    "LEFT JOIN(SELECT pi_id, pi_obj_id FROM project_info WHERE pi_categor = 2 " +
                    "UNION ALL SELECT ti_id, ti_obj_id FROM topic_info) A ON A.pi_obj_id = idi.imp_id " +
                    "LEFT JOIN processing_box pb ON pb.pb_obj_id = A.pi_id " +
                    $"WHERE idi.imp_code = '{planTypeCode}' GROUP BY imp_code";
            }
            return SqlHelper.ExecuteCountQuery(querySQL);
        }

        /// <summary>
        /// 获取指定计划类别下的文件数
        /// </summary>
        /// <param name="planTypeCode">计划类别编号</param>
        private int GetFileAmount(object planTypeCode)
        {
            string querySQL = string.Empty;
            if(!ToolHelper.GetValue(planTypeCode).Contains("ZX"))
            {
                querySQL = "SELECT COUNT(pfl.pfl_id) FROM project_info pi " +
                  "LEFT JOIN( SELECT pi_id, pi_obj_id FROM project_info WHERE pi_categor= 2 " +
                  "UNION ALL SELECT ti_id, ti_obj_id FROM topic_info) A ON A.pi_obj_id = pi.pi_id " +
                  "LEFT JOIN processing_file_list pfl ON pfl.pfl_obj_id = A.pi_id " +
                  $"WHERE pi.pi_code = '{planTypeCode}' GROUP BY pi.pi_code";
            }
            else
            {
                querySQL = "SELECT COUNT(pfl.pfl_id) FROM imp_dev_info idi " +
                    "LEFT JOIN(SELECT pi_id, pi_obj_id FROM project_info WHERE pi_categor = 2 " +
                    "UNION ALL SELECT ti_id, ti_obj_id FROM topic_info) A ON A.pi_obj_id = idi.imp_id " +
                    "LEFT JOIN processing_file_list pfl ON pfl.pfl_obj_id = A.pi_id " +
                    $"WHERE idi.imp_code = '{planTypeCode}' GROUP BY imp_code";
            }
            return SqlHelper.ExecuteCountQuery(querySQL);
        }
    }
}
