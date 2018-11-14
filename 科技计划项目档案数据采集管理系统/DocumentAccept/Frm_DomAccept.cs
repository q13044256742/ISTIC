using DevExpress.XtraBars.Navigation;
using System;
using System.Data;
using System.Windows.Forms;
using 科技计划项目档案数据采集管理系统.DocumentAccept;

namespace 科技计划项目档案数据采集管理系统
{
    public partial class Frm_DomAccept : Form
    {
        public Frm_DomAccept()
        {
            InitializeComponent();
        }

        private void Frm_DomAccept_Load(object sender, EventArgs e)
        {
            LoadCompanySource();
            LoadDataGridView(string.Empty);
            dgv_DataShow.ColumnHeadersDefaultCellStyle = DataGridViewStyleHelper.GetHeaderStyle();
            dgv_DataShow.DefaultCellStyle = DataGridViewStyleHelper.GetCellStyle();
        }

        private void LoadDataGridView(string unitId)
        {
            DataGridViewStyleHelper.ResetDataGridView(dgv_DataShow, true);
            dgv_DataShow.Columns.AddRange(new DataGridViewColumn[]
            {
                new DataGridViewTextBoxColumn(){ Name = "id"},
                new DataGridViewTextBoxColumn(){ Name = "unit", HeaderText = "来源单位", FillWeight = 15 },
                new DataGridViewTextBoxColumn(){ Name = "code", HeaderText = "批次编号", FillWeight = 20 },
                new DataGridViewTextBoxColumn(){ Name = "name", HeaderText = "批次名称", FillWeight = 20 },
                new DataGridViewButtonColumn(){ Name = "file", HeaderText = "必备文件", FillWeight = 7 },
                new DataGridViewButtonColumn(){ Name = "control", HeaderText = "操作", FillWeight = 7, Text = "结束", UseColumnTextForButtonValue = true },
            });
            dgv_DataShow.Columns["id"].Visible = false;

            string querySql = $"SELECT * FROM transfer_registration_pc LEFT JOIN data_dictionary ON dd_id = com_id " +
                $"WHERE trp_work_status=2";
            if(!string.IsNullOrEmpty(unitId)) querySql += $" AND dd_id='{unitId}'";

            DataTable table = SqlHelper.ExecuteQuery(querySql);
            for(int i = 0; i < table.Rows.Count; i++)
            {
                DataGridViewRow newRow = dgv_DataShow.Rows[dgv_DataShow.Rows.Add()];
                newRow.Cells["id"].Value = table.Rows[i]["trp_id"];
                newRow.Cells["unit"].Value = table.Rows[i]["dd_name"];
                newRow.Cells["code"].Value = table.Rows[i]["trp_code"];
                newRow.Cells["name"].Value = table.Rows[i]["trp_name"];
                newRow.Cells["file"].Value = GetFileCompleteResult(table.Rows[i]["trp_id"]);
            }
        }

        /// <summary>
        /// 获取文件缺失情况
        /// </summary>
        /// <param name="trpId">批次ID</param>
        private string GetFileCompleteResult(object trpId)
        {
            string querySql = "SELECT wm.wm_obj_id FROM work_myreg wm " +
                "LEFT JOIN work_registration wr ON wr.wr_id = wm.wr_id " +
                "LEFT JOIN transfer_registration_pc trp ON trp.trp_id = wr.trp_id " +
                "WHERE wm.wm_type=3 AND trp.trp_id = '" + trpId + "'";
            object[] objIDs = SqlHelper.ExecuteSingleColumnQuery(querySql);

            foreach(object objID in objIDs)
            {
                if(IsLostFile(objID))
                    return "不齐备";

                //如果项目不缺失文件，则判断其下属课题是否有缺失文件
                object[] topicIdList = SqlHelper.ExecuteSingleColumnQuery(
                    $"SELECT ti_id FROM topic_info WHERE ti_obj_id = '{objID}' UNION ALL " +
                    $"SELECT si_id FROM subject_info WHERE si_obj_id = '{objID}'");
                for(int j = 0; j < topicIdList.Length; j++)
                {
                    if(IsLostFile(topicIdList[j]))
                        return "不齐备";

                    object[] subjectIdList = SqlHelper.ExecuteSingleColumnQuery($"SELECT si_id FROM subject_info WHERE si_obj_id = '{topicIdList[j]}'");
                    for(int k = 0; k < subjectIdList.Length; k++)
                    {
                        if(IsLostFile(subjectIdList[k]))
                            return "不齐备";
                    }
                }
            }
            return "齐备";
        }

        /// <summary>
        /// 判断指定项目/课题是否缺失文件
        /// </summary>
        private bool IsLostFile(object objID)
        {
            string querySQL = $"SELECT COUNT(pfo_id) FROM processing_file_lost WHERE pfo_obj_id='{objID}' AND pfo_ismust=1";
            int result = SqlHelper.ExecuteCountQuery(querySQL);
            //大于0则有缺失记录，返回true；反之返回false
            return result > 0;
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

        private void Dgv_DataShow_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex != -1 && e.ColumnIndex != -1)
            {
                string columnName = dgv_DataShow.Columns[e.ColumnIndex].Name;
                if("control".Equals(columnName))
                {
                    if(MessageBox.Show("本批次完结，请线下发送确认函和文件清单。", "温馨提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk) == DialogResult.OK)
                    {
                        object trpId = dgv_DataShow.Rows[e.RowIndex].Cells["id"].Value;
                        SqlHelper.ExecuteNonQuery($"UPDATE transfer_registration_pc SET trp_complete_status='{(int)SubmitStatus.Completed}', trp_complete_user='{UserHelper.GetUser().UserKey}' WHERE trp_id='{trpId}'");
                        LoadDataGridView(string.Empty);
                    }
                }
                else if("file".Equals(columnName))
                {
                    object value = dgv_DataShow.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                    object trpId = dgv_DataShow.Rows[e.RowIndex].Cells["id"].Value;
                    if("齐备".Equals(value))
                    {
                        Frm_Print frm = GetFormHelper.GetPrintDoc(this, 1, trpId);
                        frm.Show();
                        frm.Activate();
                    }
                    else
                    {
                        Frm_Print frm = GetFormHelper.GetPrintDoc(this, 2, trpId);
                        frm.Show();
                        frm.Activate();
                    }
                }
            }
        }

        private void Ac_LeftMenu_ElementClick(object sender, ElementClickEventArgs e)
        {
            string name = e.Element.Name;
            if(!string.IsNullOrEmpty(name))
            {
                if("ace_all".Equals(name))
                    LoadDataGridView(null);
                else
                    LoadDataGridView(name);
            }
        }

        private void btn_ExportEFile_Click(object sender, EventArgs e)
        {
            int count = dgv_DataShow.SelectedRows.Count;
            if(count == 1)
            {
                object trpId = dgv_DataShow.SelectedRows[0].Cells["id"].Value;
                Frm_ExportEFile exportEFile = GetFormHelper.GetExportEFile(trpId);
                exportEFile.Show();
                exportEFile.Activate();
            }
            else
                DevExpress.XtraEditors.XtraMessageBox.Show("请选择一个批次进行导出。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }
    }
}
