using DevExpress.XtraEditors;
using System;
using System.Data;
using System.Linq;
using System.IO;
using System.Windows.Forms;

namespace 科技计划项目档案数据采集管理系统.DocumentAccept
{
    public partial class Frm_ExportEFile : XtraForm
    {
        private object trpId;
        public Frm_ExportEFile(object trpId)
        {
            this.trpId = trpId;
            InitializeComponent();
        }

        private void Frm_ExportEFile_Load(object sender, EventArgs e)
        {
            LoadBatchTree();
        }

        private void LoadBatchTree()
        {
            TreeNode rootNode = null;
            DataRow impRow = SqlHelper.ExecuteSingleRowQuery($"SELECT * FROM imp_info WHERE imp_obj_id='{trpId}'");
            if(impRow != null)//专项
            {
                rootNode = new TreeNode()
                {
                    Name = ToolHelper.GetValue(impRow["imp_id"]),
                    Text = ToolHelper.GetValue(impRow["imp_code"])
                };
                DataRow speRow = SqlHelper.ExecuteSingleRowQuery($"SELECT * FROM imp_dev_info WHERE imp_obj_id='{impRow["imp_id"]}'");
                if(speRow != null)
                {
                    TreeNode speNode = new TreeNode()
                    {
                        Name = ToolHelper.GetValue(speRow["imp_id"]),
                        Text = ToolHelper.GetValue(speRow["imp_code"])
                    };
                    rootNode.Nodes.Add(speNode);

                    DataTable proTable = SqlHelper.ExecuteQuery($"SELECT * FROM (" +
                         "SELECT pi_id, pi_code, pi_name, pi_obj_id FROM project_info WHERE pi_categor=2 UNION ALL " +
                         "SELECT ti_id, ti_code, ti_name, ti_obj_id FROM topic_info WHERE ti_categor=-3) A " +
                        $"WHERE A.pi_obj_id='{speRow["imp_id"]}' ORDER BY A.pi_code");
                    foreach(DataRow proRow in proTable.Rows)
                    {
                        TreeNode proNode = new TreeNode()
                        {
                            Name = ToolHelper.GetValue(proRow["pi_id"]),
                            Text = ToolHelper.GetValue(proRow["pi_code"]),
                        };
                        speNode.Nodes.Add(proNode);

                        DataTable topTable = SqlHelper.ExecuteQuery($"SELECT * FROM (" +
                             "SELECT ti_id, ti_code, ti_name, ti_obj_id FROM topic_info WHERE ti_categor=3 UNION ALL " +
                             "SELECT si_id, si_code, si_name, si_obj_id FROM subject_info) A " +
                            $"WHERE A.ti_obj_id='{proRow["pi_id"]}' ORDER BY A.ti_code");

                        foreach(DataRow topRow in topTable.Rows)
                        {
                            TreeNode topNode = new TreeNode()
                            {
                                Name = ToolHelper.GetValue(topRow["ti_id"]),
                                Text = ToolHelper.GetValue(topRow["ti_code"]),
                            };
                            proNode.Nodes.Add(topNode);

                            DataTable subTable = SqlHelper.ExecuteQuery(
                                $"SELECT si_id, si_code, si_name FROM subject_info WHERE si_obj_id='{topRow["ti_id"]}' ORDER BY si_code");
                            foreach(DataRow subRow in subTable.Rows)
                            {
                                TreeNode subNode = new TreeNode()
                                {
                                    Name = ToolHelper.GetValue(subRow["si_id"]),
                                    Text = ToolHelper.GetValue(subRow["si_code"])
                                };
                                topNode.Nodes.Add(subNode);
                            }
                        }
                    }
                }
            }
            else//计划
            {
                DataRow planRow = SqlHelper.ExecuteSingleRowQuery($"SELECT * FROM project_info WHERE pi_categor=1 AND trc_id='{trpId}'");
                if(planRow != null)
                {
                    rootNode = new TreeNode()
                    {
                        Name = ToolHelper.GetValue(planRow["pi_id"]),
                        Text = ToolHelper.GetValue(planRow["pi_code"])
                    };
                    DataTable proTable = SqlHelper.ExecuteQuery($"SELECT * FROM (" +
                         "SELECT pi_id, pi_code, pi_name, pi_obj_id FROM project_info WHERE pi_categor=2 UNION ALL " +
                         "SELECT ti_id, ti_code, ti_name, ti_obj_id FROM topic_info WHERE ti_categor=-3) A " +
                        $"WHERE A.pi_obj_id='{planRow["pi_id"]}' ORDER BY A.pi_code");

                    foreach(DataRow proRow in proTable.Rows)
                    {
                        TreeNode proNode = new TreeNode()
                        {
                            Name = ToolHelper.GetValue(proRow["pi_id"]),
                            Text = ToolHelper.GetValue(proRow["pi_code"]),
                        };
                        rootNode.Nodes.Add(proNode);

                        DataTable topTable = SqlHelper.ExecuteQuery($"SELECT * FROM (" +
                             "SELECT ti_id, ti_code, ti_name, ti_obj_id FROM topic_info WHERE ti_categor=3 UNION ALL " +
                             "SELECT si_id, si_code, si_name, si_obj_id FROM subject_info) A " +
                            $"WHERE A.ti_obj_id='{proRow["pi_id"]}' ORDER BY A.ti_code");

                        foreach(DataRow topRow in topTable.Rows)
                        {
                            TreeNode topNode = new TreeNode()
                            {
                                Name = ToolHelper.GetValue(topRow["ti_id"]),
                                Text = ToolHelper.GetValue(topRow["ti_code"]),
                            };
                            proNode.Nodes.Add(topNode);

                            DataTable subTable = SqlHelper.ExecuteQuery(
                                $"SELECT si_id, si_code, si_name FROM subject_info WHERE si_obj_id='{topRow["ti_id"]}' ORDER BY si_code");
                            foreach(DataRow subRow in subTable.Rows)
                            {
                                TreeNode subNode = new TreeNode()
                                {
                                    Name = ToolHelper.GetValue(subRow["si_id"]),
                                    Text = ToolHelper.GetValue(subRow["si_code"])
                                };
                                topNode.Nodes.Add(subNode);
                            }
                        }
                    }
                }
            }

            if(rootNode != null)
            {
                treeView1.Nodes.Add(rootNode);
                rootNode.ExpandAll();
            }
        }

        private void treeView1_AfterCheck(object sender, TreeViewEventArgs e)
        {
            TreeNode node = e.Node;
            SetCheckedState(node, node.Checked);
        }

        private void SetCheckedState(TreeNode rootNode, bool isChecked)
        {
            foreach(TreeNode node in rootNode.Nodes)
            {
                node.Checked = isChecked;
                SetCheckedState(node, isChecked);
            }
        }

        private void lbl_ExportLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            DialogResult dialogResult = folderBrowserDialog1.ShowDialog();
            if(dialogResult == DialogResult.OK)
            {
                string savePath = folderBrowserDialog1.SelectedPath;
                txt_ExportEFilePath.Text = savePath;
            }
        }

        private void btn_ExportOk_Click(object sender, EventArgs e)
        {
            docDataSet = new DataSet("Documents");
            fileDataSet = new DataSet("Files");
            boxDataSet = new DataSet("Box_Tag");
            string savePath = txt_ExportEFilePath.Text;
            if(!string.IsNullOrEmpty(savePath))
            {
                if(treeView1.Nodes.Count > 0)
                {
                    bool isData = chk_Data.Checked;
                    ExportEFile(treeView1.Nodes[0], savePath, isData);
                    if(isData)
                    {
                        docDataSet.WriteXml(savePath + @"\Documents.xml");
                        fileDataSet.WriteXml(savePath + @"\Files.xml");
                        boxDataSet.WriteXml(savePath + @"\Box_Tag.xml");
                    }
                    DialogResult dialogResult = XtraMessageBox.Show("导出完毕，是否立即打开文件夹？", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    if(dialogResult == DialogResult.OK)
                    {
                        WinFormOpenHelper.OpenWinForm(0, "open", null, null, savePath, ShowWindowCommands.SW_NORMAL);
                    }
                }
                else
                    XtraMessageBox.Show("尚无文件可导出。");
            }
            else
                XtraMessageBox.Show("请先选择导出路径。");
        }
        /// <summary>
        /// 项目/课题/子课题
        /// </summary>
        private DataSet docDataSet;
        /// <summary>
        /// 文件
        /// </summary>
        private DataSet fileDataSet;
        /// <summary>
        /// 盒|案卷
        /// </summary>
        private DataSet boxDataSet;
        /// <summary>
        /// 导出选中项目/课题下的所有电子文件到指定文件夹
        /// </summary>
        /// <param name="rootNode">选择节点</param>
        /// <param name="savePath">保存基路径</param>
        /// <param name="isData">是否导出当前选中数据（XML）</param>
        private void ExportEFile(TreeNode rootNode, string savePath, bool isData)
        {
            if(rootNode.Checked)
            {
                string filePath = savePath + @"\" + rootNode.FullPath;
                if(!Directory.Exists(filePath))
                    Directory.CreateDirectory(filePath);
                object objectId = rootNode.Name;
                if(isData)
                {
                    string querySQL = "SELECT pi_id id, pi_code code, pi_name name, pi_field field, pb_theme theme, pi_funds funds, pi_start_datetime startDate, pi_end_datetime endDate, pi_year year, pi_unit unit, pi_province province, pi_uniter uinter, pi_prouser prouser, pi_intro intro, pi_categor categor, pi_source_id sourceCode, pi_orga_id orgaCode, pi_obj_id objId FROM(" +
                        "SELECT pi_id, pi_code, pi_name, pi_field, pb_theme, pi_funds, pi_start_datetime, pi_end_datetime, pi_year, pi_unit, pi_province, pi_uniter, pi_prouser, pi_intro, pi_categor, pi_source_id, pi_orga_id, pi_obj_id FROM project_info UNION ALL " +
                        "SELECT ti_id, ti_code, ti_name, ti_field, tb_theme, ti_funds, ti_start_datetime, ti_end_datetime, ti_year, ti_unit, ti_province, ti_uniter, ti_prouser, ti_intro, ti_categor, ti_source_id, ti_orga_id, ti_obj_id FROM topic_info UNION ALL " +
                        "SELECT si_id, si_code, si_name, si_field, si_theme, si_funds, si_start_datetime, si_end_datetime, si_year, si_unit, si_province, si_uniter, si_prouser, si_intro, si_categor, si_source_id, si_orga_id, si_obj_id FROM subject_info) A " +
                       $"WHERE A.pi_id='{objectId}'";
                    DataTable table = SqlHelper.ExecuteQuery(querySQL);
                    if(table.Rows.Count > 0)
                    {
                        int categor = ToolHelper.GetIntValue(table.Rows[0]["categor"]);
                        if(categor == 1 || categor == 2)
                        {
                            DataTable _table = docDataSet.Tables["Project"];
                            if(_table != null)
                                _table.ImportRow(table.Rows[0]);
                            else
                            {
                                table.TableName = "Project";
                                docDataSet.Tables.Add(table);
                            }
                        }
                        else if(categor == 3 || categor == -3)
                        {
                            DataTable _table = docDataSet.Tables["Topic"];
                            if(_table != null)
                                _table.ImportRow(table.Rows[0]);
                            else
                            {
                                table.TableName = "Topic";
                                docDataSet.Tables.Add(table);
                            }
                        }
                        else if(categor == 4)
                        {
                            DataTable _table = docDataSet.Tables["Subject"];
                            if(_table != null)
                                _table.ImportRow(table.Rows[0]);
                            else
                            {
                                table.TableName = "Subject";
                                docDataSet.Tables.Add(table);
                            }
                        }
                    }

                    string boxQuerySQL = $"SELECT pb_id, pb_box_number, pb_gc_id, pb_gc_fix, pb_gc_number, pb_obj_id, pb_unit_id, pt_id FROM processing_box WHERE pb_obj_id='{objectId}';";
                    DataTable boxTable = SqlHelper.ExecuteQuery(boxQuerySQL);
                    if(boxTable.Rows.Count > 0)
                    {
                        DataTable _table = boxDataSet.Tables["Box"];
                        if(_table != null)
                        {
                            foreach(DataRow row in boxTable.Rows)
                                _table.ImportRow(row);
                        }
                        else
                        {
                            boxTable.TableName = "Box";
                            boxDataSet.Tables.Add(boxTable);
                        }
                    }

                    string tagQuerySQL = $"SELECT pt_id, pt_code, pt_name, pt_obj_id FROM processing_tag WHERE pt_obj_id='{objectId}';";
                    DataTable tagTable = SqlHelper.ExecuteQuery(tagQuerySQL);
                    if(tagTable.Rows.Count > 0)
                    {
                        DataTable _table = boxDataSet.Tables["Tag"];
                        if(_table != null)
                        {
                            foreach(DataRow row in tagTable.Rows)
                                _table.ImportRow(row);
                        }
                        else
                        {
                            tagTable.TableName = "Tag";
                            boxDataSet.Tables.Add(tagTable);
                        }
                    }
                }
                string fileQuerySql = $"SELECT pfl_id, pfl_stage, pfl_categor, pfl_code, pfl_name, pfl_user, pfl_type, pfl_scert, pfl_pages, pfl_count, pfl_amount, pfl_date, pfl_unit, pfl_carrier, pfl_format, pfl_link, pfl_remark, pfl_obj_id, pfl_sort, pfl_box_id, pfl_box_sort FROM processing_file_list WHERE pfl_obj_id='{objectId}';";
                DataTable fileTable = SqlHelper.ExecuteQuery(fileQuerySql);
                if(fileTable.Rows.Count > 0)
                {
                    if(isData)//导出数据库至XML
                    {
                        DataTable _table = fileDataSet.Tables["File"];
                        if(_table != null)
                        {
                            foreach(DataRow row in fileTable.Rows)
                                _table.ImportRow(row);
                        }
                        else
                        {
                            fileTable.TableName = "File";
                            fileDataSet.Tables.Add(fileTable);
                        }
                    }
                    foreach(DataRow fileRow in fileTable.Rows)
                    {
                        string _fileLink = ToolHelper.GetValue(fileRow["pfl_link"]);
                        string[] fileLinkArray = _fileLink.Split('；');
                        foreach(string fileLink in fileLinkArray)
                        {
                            if(File.Exists(fileLink))
                            {
                                string _filePath = filePath + @"\" + Path.GetFileName(fileLink);
                                if(!Directory.Exists(filePath))
                                    Directory.CreateDirectory(filePath);
                                File.Copy(fileLink, _filePath, true);
                            }
                        }
                    }
                }
            }
            foreach(TreeNode node in rootNode.Nodes)
            {
                ExportEFile(node, savePath, isData);
            }
        }

        private void chk_All_CheckedChanged(object sender, EventArgs e)
        {
            if(treeView1.Nodes.Count > 0)
            {
                treeView1.Nodes[0].Checked = chk_All.Checked;
            }
        }
    }
}
