using DevExpress.XtraBars.Navigation;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using 科技计划项目档案数据采集管理系统.Properties;

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

            string querySql = $"SELECT * FROM transfer_registration_pc LEFT JOIN data_dictionary ON dd_id = com_id";
            if(!string.IsNullOrEmpty(unitId)) querySql += $" WHERE dd_id='{unitId}'";
            DataTable table = SqlHelper.ExecuteQuery(querySql);
            for(int i = 0; i < table.Rows.Count; i++)
            {
                DataGridViewRow newRow = dgv_DataShow.Rows[dgv_DataShow.Rows.Add()];
                newRow.Cells["id"].Value = table.Rows[i]["trp_id"];
                newRow.Cells["unit"].Value = table.Rows[i]["dd_name"];
                newRow.Cells["code"].Value = table.Rows[i]["trp_code"];
                newRow.Cells["name"].Value = table.Rows[i]["trp_name"];
                newRow.Cells["file"].Value = GetFileResult(table.Rows[i]["trp_id"]);
            }
        }

        private object GetFileResult(object trpId)
        {
            string result = "齐备";
            string querySql = "SELECT wm.* FROM work_myreg wm " +
                "LEFT JOIN work_registration wr ON wr.wr_id = wm.wr_id " +
                "LEFT JOIN transfer_registration_pc trp ON trp.trp_id = wr.trp_id " +
                "WHERE trp.trp_id = '" + trpId + "'";
            DataTable table = SqlHelper.ExecuteQuery(querySql);
            for(int i = 0; i < table.Rows.Count; i++)
            {
                object objId = table.Rows[i]["wm_obj_id"];
                int type = (int)table.Rows[i]["wm_type"];
                if(type == 0 || type == 1)
                {
                    if(HaveLostFile(objId))
                        result = "不齐备";
                }
                else if(type == 2)
                {
                    if(HaveLostFile(objId))
                        result = "不齐备";
                    else
                    {
                        List<object[]> list = SqlHelper.ExecuteColumnsQuery($"SELECT ti_id FROM topic_info WHERE ti_obj_id = '{objId}'", 1);
                        for(int j = 0; j < list.Count; j++)
                        {
                            if(HaveLostFile(list[j][0]))
                            {
                                result = "不齐备";
                                break;
                            }
                            List<object[]> list2 = SqlHelper.ExecuteColumnsQuery($"SELECT si_id FROM subject_info WHERE si_obj_id = '{list[j][0]}'", 1);
                            for(int k = 0; k < list2.Count; k++)
                            {
                                if(HaveLostFile(list2[k][0]))
                                {
                                    result = "不齐备";
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// 是否缺失文件
        /// </summary>
        private bool HaveLostFile(object id) => SqlHelper.ExecuteCountQuery($"SELECT COUNT(*) FROM processing_file_lost WHERE pfo_obj_id='{id}'") == 0 ? false : true;

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
                    Name = GetValue(table.Rows[i]["dd_id"]),
                    Text = GetValue(table.Rows[i]["dd_name"]),
                };
                element.Click += new EventHandler(Element_Click);
                acg_Register.Elements.Add(element);
            }
        }

        /// <summary>
        /// 来源单位点击事件
        /// </summary>
        private void Element_Click(object sender, EventArgs e)
        {

        }

        private string GetValue(object v) => v == null ? string.Empty : v.ToString();

        private void dgv_DataShow_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex != -1 && e.ColumnIndex != -1)
            {
                string columnName = dgv_DataShow.Columns[e.ColumnIndex].Name;
                if("control".Equals(columnName))
                {
                    if(MessageBox.Show("本批次完结，请线下发送确认函和文件清单。", "温馨提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk) == DialogResult.OK)
                    {
                        object trpId = dgv_DataShow.Rows[e.RowIndex].Cells["id"].Value;
                        SqlHelper.ExecuteNonQuery($"UPDATE transfer_registration_pc SET trp_complete_status='{(int)SubmitStatus.Completed}', trp_complete_user='{UserHelper.GetInstance().User.UserKey}' WHERE trp_id='{trpId}'");
                        LoadDataGridView(string.Empty);
                    }
                }
                else if("file".Equals(columnName))
                {
                    object value = dgv_DataShow.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                    object trpId = dgv_DataShow.Rows[e.RowIndex].Cells["id"].Value;
                    if("齐备".Equals(value))
                    {
                        Frm_Print frm = new Frm_Print(1, trpId);
                        frm.ShowDialog();
                    }
                    else
                    {
                        Frm_Print frm = new Frm_Print(-1, trpId);
                        frm.ShowDialog();
                    }
                }
            }
        }
    }
}
