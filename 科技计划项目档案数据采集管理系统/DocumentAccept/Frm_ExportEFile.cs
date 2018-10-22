using DevExpress.XtraEditors;
using System;
using System.Data;
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
                        Text = ToolHelper.GetValue(impRow["imp_code"]) + $"({ToolHelper.GetValue(impRow["imp_name"])})"
                    };
                    rootNode.Nodes.Add(speNode);

                    DataTable proTable = SqlHelper.ExecuteQuery($"SELECT * FROM (" +
                         "SELECT pi_id, pi_code, pi_name, pi_obj_id FROM project_info WHERE pi_categor=2 UNION ALL " +
                        $"SELECT ti_id, ti_code, ti_name, ti_obj_id FROM topic_info WHERE ti_categor=-3) A WHERE A.pi_obj_id='{speRow["imp_id"]}'");
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
                            $"SELECT si_id, si_code, si_name, si_obj_id FROM subject_info) A WHERE A.ti_obj_id='{proRow["pi_id"]}'");

                        foreach(DataRow topRow in topTable.Rows)
                        {
                            TreeNode topNode = new TreeNode()
                            {
                                Name = ToolHelper.GetValue(topRow["ti_id"]),
                                Text = ToolHelper.GetValue(topRow["ti_code"]),
                            };
                            proNode.Nodes.Add(topNode);

                            DataTable subTable = SqlHelper.ExecuteQuery($"SELECT si_id, si_code, si_name FROM subject_info WHERE si_obj_id='{topRow["ti_id"]}'");
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
                        $"SELECT ti_id, ti_code, ti_name, ti_obj_id FROM topic_info WHERE ti_categor=-3) A WHERE A.pi_obj_id='{planRow["pi_id"]}'");

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
                            $"SELECT si_id, si_code, si_name, si_obj_id FROM subject_info) A WHERE A.ti_obj_id='{proRow["pi_id"]}'");

                        foreach(DataRow topRow in topTable.Rows)
                        {
                            TreeNode topNode = new TreeNode()
                            {
                                Name = ToolHelper.GetValue(topRow["ti_id"]),
                                Text = ToolHelper.GetValue(topRow["ti_code"]),
                            };
                            proNode.Nodes.Add(topNode);

                            DataTable subTable = SqlHelper.ExecuteQuery($"SELECT si_id, si_code, si_name FROM subject_info WHERE si_obj_id='{topRow["ti_id"]}'");
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
            string savePath = txt_ExportEFilePath.Text;
            if(!string.IsNullOrEmpty(savePath))
            {
                if(treeView1.Nodes.Count > 0)
                {
                    ExportEFile(treeView1.Nodes[0], savePath);
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

        private void ExportEFile(TreeNode rootNode, string savePath)
        {
            if(rootNode.Checked)
            {
                string filePath = savePath + @"\" + rootNode.FullPath;
                if(!Directory.Exists(filePath))
                    Directory.CreateDirectory(filePath);
                object objectId = rootNode.Name;
                object[] fileLinkObjectArray = SqlHelper.ExecuteSingleColumnQuery($"SELECT pfl_link FROM processing_file_list WHERE pfl_obj_id='{objectId}'");
                foreach(object fileLinkObject in fileLinkObjectArray)
                {
                    if(!string.IsNullOrEmpty(ToolHelper.GetValue(fileLinkObject).Trim()))
                    {
                        string[] fileLinkArray = ToolHelper.GetValue(fileLinkObject).Split('；');
                        foreach(string fileLink in fileLinkArray)
                        {
                            if(File.Exists(fileLink))
                            {
                                filePath += @"\" + Path.GetFileName(fileLink);
                                File.Copy(fileLink, savePath, true);
                            }
                        }
                    }
                }
            }
            foreach(TreeNode node in rootNode.Nodes)
            {
                ExportEFile(node, savePath);
            }
        }
    }
}
