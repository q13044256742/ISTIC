using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using 科技计划项目档案数据采集管理系统.Properties;

namespace 科技计划项目档案数据采集管理系统.DocumentAccept
{
    public partial class Frm_Print : XtraForm
    {
        /// <summary>
        /// 打印类型
        /// <para>1：齐备 </para>
        /// <para>-：不齐备</para>
        /// </summary>
        private int TYPE;
        private object trpId;
        private Form parentForm;
        public Frm_Print(Form pForm, int type, object trpId)
        {
            InitializeComponent();
            parentForm = pForm;
            TYPE = type;
            this.trpId = trpId;
            if(type == 1)//齐备
            {
                chk1.Text = "档案接收确认函";
                chk2.Text = "文件列表清单";
                chk3.Visible = false;
            }
            else
            {
                chk1.Text = "档案催报单";
                chk2.Text = "缺失必备文件清单";
                chk3.Text = "文件列表清单";
            }
        }

        private void Frm_Print_Load(object sender, System.EventArgs e)
        {

        }

        private void Btn_Print_Click(object sender, System.EventArgs e)
        {
            if(TYPE == 1)
            {
                //档案接收确认函
                if(chk1.Checked)
                {
                    new WebBrowser() { DocumentText = GetDomRecHTML() }.DocumentCompleted += Frm_Print_DocumentCompleted;
                }
                //文件列表清单
                if(chk2.Checked)
                {
                    CreateFileList();
                }
            }
            else
            {
                //档案接收确认函
                if(chk1.Checked)
                {
                    new WebBrowser() { DocumentText = GetDomRecHTML() }.DocumentCompleted += Frm_Print_DocumentCompleted;
                }
                //缺失文件清单
                if(chk2.Checked)
                {
                    CreateLostFileList();
                }
                //文件列表清单
                if(chk3.Checked)
                {
                    CreateFileList();
                }
            }
        }

        /// <summary>
        /// 导出缺失文件清单
        /// </summary>
        private void CreateLostFileList()
        {
            string querySQL = "SELECT A.pi_code '项目/课题编号', A.pi_name '项目/课题名称', A.pi_unit '承担单位', A.pi_prouser '项目负责人', A.pi_start_datetime '项目开始时间', A.pi_end_datetime '项目结束时间', B.dd_name '缺失文件类别', B.dd_note '缺失文件名称' " +
                "FROM transfer_registration_pc trp " +
                "LEFT JOIN imp_info ii ON ii.imp_obj_id=trp.trp_id " +
                "LEFT JOIN imp_dev_info idi ON idi.imp_obj_id=ii.imp_id " +
                "LEFT JOIN " +
                "(SELECT pi_id, pi_code, pi_name, pi_unit, pi_prouser, pi_start_datetime, pi_end_datetime, pi_obj_id FROM project_info WHERE pi_categor=2 UNION ALL " +
                " SELECT ti_id, ti_code, ti_name, ti_unit, ti_prouser, ti_start_datetime, ti_end_datetime, ti_obj_id FROM topic_info WHERE ti_categor=-3 " +
                ")A ON A.pi_obj_id=idi.imp_id " +
                "LEFT JOIN processing_file_list pfl ON pfl.pfl_obj_id = A.pi_id " +
                "LEFT JOIN (" +
                "    SELECT dd_id, dd_name, dd_note FROM data_dictionary WHERE dd_pId IN " +
                "   (SELECT dd_id FROM data_dictionary WHERE dd_pId = " +
                "    (SELECT dd_id FROM data_dictionary WHERE dd_code = 'dic_file_jd')) AND extend_2=1 )B ON pfl.pfl_categor NOT IN (B.dd_id) " +
               $"WHERE trp.trp_id='{trpId}' AND idi.imp_id IS NOT NULL ORDER BY A.pi_code, dd_name";
            DataTable table = SqlHelper.ExecuteQuery(querySQL);
            if(table.Rows.Count > 0)
            {
                saveFileDialog1.Title = "请选择导出位置";
                saveFileDialog1.Filter = "CSV文件|*.csv";
                if(saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    string savePath = saveFileDialog1.FileName;
                    bool flag = MicrosoftWordHelper.GetCsvFromDataTable(table, savePath);
                    {
                        if(XtraMessageBox.Show("导出缺失文件清单成功，是否立即打开？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk) == DialogResult.Yes)
                        {
                            WinFormOpenHelper.OpenWinForm(0, "open", savePath, null, null, ShowWindowCommands.SW_NORMAL);
                        }
                    }
                }
            }
            else
            {
                string querySQL2 = "SELECT A.pi_code '项目/课题编号', A.pi_name '项目/课题名称', A.pi_unit '承担单位', A.pi_prouser '项目负责人', A.pi_start_datetime '项目开始时间', A.pi_end_datetime '项目结束时间', B.dd_name '缺失文件类别', B.dd_note '缺失文件名称' " +
                    "FROM transfer_registration_pc trp " +
                    "LEFT JOIN project_info pi ON pi.pi_categor=1 AND pi.trc_id=trp.trp_id " +
                    "LEFT JOIN " +
                    "(SELECT pi_id, pi_code, pi_name, pi_unit, pi_prouser, pi_start_datetime, pi_end_datetime, pi_obj_id FROM project_info WHERE pi_categor=2 UNION ALL " +
                    " SELECT ti_id, ti_code, ti_name, ti_unit, ti_prouser, ti_start_datetime, ti_end_datetime, ti_obj_id FROM topic_info WHERE ti_categor=-3 " +
                    ")A ON A.pi_obj_id=pi.pi_id " +
                    "LEFT JOIN processing_file_list pfl ON pfl.pfl_obj_id = A.pi_id " +
                    "LEFT JOIN (" +
                    "    SELECT dd_id, dd_name, dd_note FROM data_dictionary WHERE dd_pId IN " +
                    "   (SELECT dd_id FROM data_dictionary WHERE dd_pId = " +
                    "    (SELECT dd_id FROM data_dictionary WHERE dd_code = 'dic_file_jd')) AND extend_2=1 )B ON pfl.pfl_categor NOT IN (B.dd_id) " +
                   $"WHERE trp.trp_id='{trpId}' AND pi.pi_id IS NOT NULL ORDER BY A.pi_code, dd_name";
                DataTable table2 = SqlHelper.ExecuteQuery(querySQL2);
                if(table2.Rows.Count > 0)
                {
                    saveFileDialog1.Title = "请选择导出位置";
                    saveFileDialog1.Filter = "CSV文件|*.csv";
                    if(saveFileDialog1.ShowDialog() == DialogResult.OK)
                    {
                        string savePath = saveFileDialog1.FileName;
                        bool flag = MicrosoftWordHelper.GetCsvFromDataTable(table2, savePath);
                        {
                            if(XtraMessageBox.Show("导出缺失文件清单成功，是否立即打开？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk) == DialogResult.Yes)
                            {
                                WinFormOpenHelper.OpenWinForm(0, "open", savePath, null, null, ShowWindowCommands.SW_NORMAL);
                            }
                        }
                    }
                }
                else
                {
                    XtraMessageBox.Show("当前批次下尚无项目/课题。", "导出缺失文件清单失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// 导出文件列表清单
        /// </summary>
        private void CreateFileList()
        {
            List<DataTable> list = new List<DataTable>();
            //普通计划
            string querySQL = $"SELECT pi.pi_name '计划名称', pt.pt_code '案卷编号/档号', pt.pt_name '案卷题名', " +
                $"A.pi_code '项目编号', A.pi_name '项目名称',	A.pi_unit '承担单位', A.pi_prouser '项目负责人', A.pi_start_datetime '项目开始时间', A.pi_end_datetime '项目结束时间', " +
                $"pb.pb_gc_id '馆藏号', pb.pb_box_number '盒号', B.fcount '文件数量', " +
                $"pfl.pfl_code '文件编号', pfl.pfl_box_sort '文件盒内序号', pfl.pfl_name '文件题名', pfl.pfl_amount '文件移交份数', pfl_pages '文件页数' " +
                $"FROM transfer_registration_pc trp LEFT JOIN project_info pi ON pi.trc_id = trp.trp_id AND pi.pi_categor = 1 LEFT JOIN ( " +
                $"SELECT pi_id, pi_code, pi_name, pi_unit, pi_prouser, pi_start_datetime, pi_end_datetime, pi_obj_id FROM project_info WHERE pi_categor= 2 UNION ALL " +
                $"SELECT ti_id, ti_code, ti_name, ti_unit, ti_prouser, ti_start_datetime, ti_end_datetime, ti_obj_id FROM topic_info WHERE ti_categor= -3)A ON A.pi_obj_id = pi.pi_id " +
                $"LEFT JOIN processing_tag pt ON pt.pt_obj_id = A.pi_id LEFT JOIN processing_box pb ON pb.pb_obj_id = A.pi_id LEFT JOIN ( " +
                $"SELECT pfl_box_id, COUNT(pfl_id) fcount FROM processing_file_list GROUP BY pfl_box_id )B ON B.pfl_box_id = pb.pb_id " +
                $"LEFT JOIN processing_file_list pfl ON pfl.pfl_box_id = pb.pb_id " +
                $"WHERE trp.trp_id = '{trpId}' AND pi.pi_id IS NOT NULL ";
            DataTable table = SqlHelper.ExecuteQuery(querySQL);
            if(table.Rows.Count > 0)
            {
                saveFileDialog1.Title = "请选择导出位置";
                saveFileDialog1.Filter = "CSV文件|*.csv";
                if(saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    string savePath = saveFileDialog1.FileName;
                    bool flag = MicrosoftWordHelper.GetCsvFromDataTable(table, savePath);
                    {
                        if(XtraMessageBox.Show("导出文件列表清单成功，是否立即打开？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk) == DialogResult.Yes)
                        {
                            WinFormOpenHelper.OpenWinForm(0, "open", savePath, null, null, ShowWindowCommands.SW_NORMAL);
                        }
                    }
                }
            }
            else
            {
                querySQL = $"SELECT idi.imp_name '计划名称', pt.pt_code '案卷编号/档号', pt.pt_name '案卷题名', " +
                   $"A.pi_code '项目编号', A.pi_name '项目名称',	A.pi_unit '承担单位', A.pi_prouser '项目负责人', A.pi_start_datetime '项目开始时间', A.pi_end_datetime '项目结束时间', " +
                   $"pb.pb_gc_id '馆藏号', pb.pb_box_number '盒号', B.fcount '文件数量', " +
                   $"pfl.pfl_code '文件编号', pfl.pfl_box_sort '文件盒内序号', pfl.pfl_name '文件题名', pfl.pfl_amount '文件移交份数', pfl_pages '文件页数' " +
                   $"FROM transfer_registration_pc trp LEFT JOIN imp_info ii ON ii.imp_obj_id=trp.trp_id " +
                   $"LEFT JOIN imp_dev_info idi ON idi.imp_obj_id = ii.imp_id LEFT JOIN ( " +
                   $"SELECT pi_id, pi_code, pi_name, pi_unit, pi_prouser, pi_start_datetime, pi_end_datetime, pi_obj_id FROM project_info WHERE pi_categor= 2 UNION ALL " +
                   $"SELECT ti_id, ti_code, ti_name, ti_unit, ti_prouser, ti_start_datetime, ti_end_datetime, ti_obj_id FROM topic_info WHERE ti_categor= -3)A ON A.pi_obj_id = idi.imp_id " +
                   $"LEFT JOIN processing_tag pt ON pt.pt_obj_id = A.pi_id LEFT JOIN processing_box pb ON pb.pb_obj_id = A.pi_id LEFT JOIN ( " +
                   $"SELECT pfl_box_id, COUNT(pfl_id) fcount FROM processing_file_list GROUP BY pfl_box_id )B ON B.pfl_box_id = pb.pb_id " +
                   $"LEFT JOIN processing_file_list pfl ON pfl.pfl_box_id = pb.pb_id " +
                   $"WHERE trp.trp_id = '{trpId}' AND idi.imp_id IS NOT NULL ";
                DataTable speTable = SqlHelper.ExecuteQuery(querySQL);
                if(speTable.Rows.Count > 0)
                {
                    saveFileDialog1.Title = "请选择导出位置";
                    saveFileDialog1.Filter = "CSV文件|*.csv";
                    if(saveFileDialog1.ShowDialog() == DialogResult.OK)
                    {
                        string savePath = saveFileDialog1.FileName;
                        bool flag = MicrosoftWordHelper.GetCsvFromDataTable(speTable, savePath);
                        {
                            if(XtraMessageBox.Show("导出文件列表清单成功，是否立即打开？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk) == DialogResult.Yes)
                            {
                                WinFormOpenHelper.OpenWinForm(0, "open", savePath, null, null, ShowWindowCommands.SW_NORMAL);
                            }
                        }
                    }
                }
                else
                {
                    XtraMessageBox.Show("当前批次下尚无项目/课题。", "导出文件列表清单失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void Frm_Print_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            (sender as WebBrowser).ShowPrintDialog();
            //System.Threading.Thread.Sleep(3000);
            (sender as WebBrowser).Dispose();
        }

        /// <summary>
        /// 档案接收确认函预览
        /// </summary>
        private void lbl1_Click(object sender, System.EventArgs e)
        {
            string domRec = GetDomRecHTML();

            new WebBrowser() { DocumentText = domRec, Parent = parentForm }.DocumentCompleted += Browser_DocumentCompleted;
        }

        private string GetDomRecHTML()
        {
            string domRec = Resources.domrec;
            string querySQL = "SELECT dd_name, trp_log_data, trp_code, COUNT(trc.trc_id) cCount FROM transfer_registration_pc trp " +
                "LEFT JOIN data_dictionary ON dd_id = trp.com_id " +
                "LEFT JOIN transfer_registraion_cd trc ON trc.trp_id=trp.trp_id " +
               $"WHERE trp.trp_id = '{trpId}' " +
                "GROUP BY dd_name, trp_log_data, trp_code ";
            int totalBoxCount = SqlHelper.ExecuteCountQuery("SELECT COUNT(pb.pb_id) FROM transfer_registration_pc trp " +
                "LEFT JOIN imp_info ii ON ii.imp_obj_id = trp_id " +
                "LEFT JOIN imp_dev_info idi ON idi.imp_obj_id = ii.imp_id " +
                "LEFT JOIN( " +
                "SELECT pi_id, pi_obj_id FROM project_info WHERE pi_categor = 2 UNION ALL " +
                "SELECT ti_id, ti_obj_id FROM topic_info)A ON A.pi_obj_id = idi.imp_id " +
                "LEFT JOIN processing_box pb ON pb.pb_obj_id = A.pi_id " +
               $"WHERE trp.trp_id = '{trpId}' AND pb.pb_id IS NOT NULL");
            if(totalBoxCount == 0)
            {
                totalBoxCount = SqlHelper.ExecuteCountQuery("SELECT COUNT(pb.pb_id) FROM transfer_registration_pc trp " +
                    "LEFT JOIN project_info p ON(p.trc_id = trp.trp_id AND p.pi_categor = 1) " +
                    "LEFT JOIN (" +
                    "SELECT pi_id, pi_obj_id FROM project_info WHERE pi_categor = 2 UNION ALL " +
                    "SELECT ti_id, ti_obj_id FROM topic_info )A ON A.pi_obj_id = p.pi_id " +
                    "LEFT JOIN processing_box pb ON pb.pb_obj_id = A.pi_id " +
                   $"WHERE trp.trp_id = '{trpId}' AND pb.pb_id IS NOT NULL");
            }
            int totalEFileCount = SqlHelper.ExecuteCountQuery("SELECT COUNT(pb.pfl_id) FROM transfer_registration_pc trp " +
                "LEFT JOIN imp_info ii ON ii.imp_obj_id = trp_id " +
                "LEFT JOIN imp_dev_info idi ON idi.imp_obj_id = ii.imp_id " +
                "LEFT JOIN( " +
                "SELECT pi_id, pi_obj_id FROM project_info WHERE pi_categor = 2 UNION ALL " +
                "SELECT ti_id, ti_obj_id FROM topic_info)A ON A.pi_obj_id = idi.imp_id " +
                "LEFT JOIN processing_file_list pb ON (pb.pfl_obj_id = A.pi_id AND DATALENGTH(pfl_link)>0) " +
               $"WHERE trp.trp_id = '{trpId}' AND pb.pfl_id IS NOT NULL");
            if(totalEFileCount == 0)
            {
                totalEFileCount = SqlHelper.ExecuteCountQuery("SELECT COUNT(pb.pfl_id) FROM transfer_registration_pc trp " +
                    "LEFT JOIN project_info p ON(p.trc_id = trp.trp_id AND p.pi_categor = 1) " +
                    "LEFT JOIN (" +
                    "SELECT pi_id, pi_obj_id FROM project_info WHERE pi_categor = 2 UNION ALL " +
                    "SELECT ti_id, ti_obj_id FROM topic_info )A ON A.pi_obj_id = p.pi_id " +
                    "LEFT JOIN processing_file_list pb ON (pb.pfl_obj_id = A.pi_id AND DATALENGTH(pfl_link)>0) " +
                   $"WHERE trp.trp_id = '{trpId}' AND pb.pfl_id IS NOT NULL");
            }
            DataRow row = SqlHelper.ExecuteSingleRowQuery(querySQL);
            if(row != null)
            {
                SetTagValueById(ref domRec, "trpid", $"{ToolHelper.GetValue(row["trp_code"])}");
                SetTagValueById(ref domRec, "orgName", $"&emsp;&emsp;{row["dd_name"]}：");
                SetTagValueById(ref domRec, "param1", $"{ToolHelper.GetDateValue(row["trp_log_data"], "yyyy年MM月")}");
                SetTagValueById(ref domRec, "cdcount", $"{row["cCount"]}");

                SetTagValueById(ref domRec, "boxcount", $"{totalBoxCount}");
                SetTagValueById(ref domRec, "ecount", $"{totalEFileCount}");
            }

            return domRec;
        }

        /// <summary>
        /// 设置指定ID元素的文本值
        /// </summary>
        /// <param name="domRec">html文档</param>
        /// <param name="id">元素ID</param>
        /// <param name="value">文本值</param>
        private void SetTagValueById(ref string domRec, string id, string value)
        {
            string pattern = $"<\\w+ id=\"{id}\">(&emsp;){{2}}</\\w+>";
            Match match = Regex.Match(domRec, pattern);
            if(match.Success)
            {
                string newValue = match.Value.Replace("&emsp;&emsp;", $"{value}");
                domRec = domRec.Replace(match.Value, newValue);
            }
        }

        private void Browser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            (sender as WebBrowser).ShowPrintPreviewDialog();
            (sender as WebBrowser).Dispose();
        }
    }
}
