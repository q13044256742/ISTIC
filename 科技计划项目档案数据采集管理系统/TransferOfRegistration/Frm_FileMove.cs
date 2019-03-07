using System;
using System.Data;
using System.IO;
using System.Windows.Forms;
using 科技计划项目档案数据采集管理系统.Properties;

namespace 科技计划项目档案数据采集管理系统
{
    public partial class Frm_FileMove : DevExpress.XtraEditors.XtraForm
    {
        /// <summary>
        /// 光盘编号
        /// </summary>
        private object trcCode;
        /// <summary>
        /// 源文件名
        /// </summary>
        public object objectName;
        /// <summary>
        /// 源ID
        /// </summary>
        public object objectId;
        public Frm_FileMove(object trpID, object trcCode)
        {
            InitializeComponent();
            this.trcCode = trcCode;
            LoadDiskList(trpID, trcCode);
        }

        private void LoadDiskList(object trpID, object trcCode)
        {
            object batchCode = SqlHelper.ExecuteOnlyOneQuery($"SELECT trp_name+'['+ trp_code+']' FROM transfer_registration_pc WHERE trp_id='{trpID}';");
            TreeNode batchNode = new TreeNode(ToolHelper.GetValue(batchCode)) { ImageIndex = 0 };
            treeView1.Nodes.Add(batchNode);
            string querySQL = $"SELECT A.bfi_id, B.trc_code, B.trc_name FROM backup_files_info A LEFT JOIN transfer_registraion_cd B ON A.bfi_trcid = B.trc_id " +
                   $"WHERE B.trp_id ='{trpID}' AND A.bfi_type = -1 ORDER BY b.trc_code";
            DataTable table = SqlHelper.ExecuteQuery(querySQL);
            foreach (DataRow row in table.Rows)
            {
                TreeNode diskNode = new TreeNode()
                {
                    Name = ToolHelper.GetValue(row["bfi_id"]),
                    Text = $"{row["trc_name"]}[{row["trc_code"]}]",
                    ImageIndex = 1,
                    Tag = row["trc_code"]
                };
                batchNode.Nodes.Add(diskNode);

                string _querySQL = $"SELECT bfi_id, bfi_name FROM backup_files_info WHERE bfi_pid ='{row["bfi_id"]}' AND bfi_type = 1 ORDER BY bfi_path";
                DataTable _table = SqlHelper.ExecuteQuery(_querySQL);
                foreach (DataRow _row in _table.Rows)
                {
                    TreeNode folderNode = new TreeNode()
                    {
                        Name = ToolHelper.GetValue(_row["bfi_id"]),
                        Text = $"{_row["bfi_name"]}",
                        ImageIndex = 1,
                        Tag = row["trc_code"]
                    };
                    diskNode.Nodes.Add(folderNode);
                }
            }

            if (treeView1.Nodes.Count > 0)
                treeView1.ExpandAll();
        }

        private ImageList imageList;
        private void Frm_FileMove_Load(object sender, System.EventArgs e)
        {
            imageList = new ImageList();
            imageList.Images.AddRange(new System.Drawing.Image[] {
                Resources.file2, Resources.file
            });
            treeView1.ImageList = imageList;
        }

        private void simpleButton1_Click(object sender, System.EventArgs e)
        {
            object sourceFile = objectName;
            object destDiskCode = treeView1.SelectedNode.Tag;
            object destFolder = treeView1.SelectedNode.Text;
            string queryString = $"确定将光盘({trcCode})下的文件[{sourceFile}]移动到光盘({destDiskCode})的文件夹[{destFolder}]下吗？";
            DialogResult dialogResult = DevExpress.XtraEditors.XtraMessageBox.Show(queryString, "确认提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if(dialogResult == DialogResult.OK)
            {
                string sourceFilePath = ToolHelper.GetValue(SqlHelper.ExecuteOnlyOneQuery($"SELECT bfi_path+'\\'+bfi_name FROM backup_files_info WHERE bfi_id='{objectId}';"));
                string destFolderId = treeView1.SelectedNode.Name;
                string destFolderPath = ToolHelper.GetValue(SqlHelper.ExecuteOnlyOneQuery($"SELECT bfi_path+'\\'+bfi_name FROM backup_files_info WHERE bfi_id='{destFolderId}';"));

                if (File.Exists(sourceFilePath))
                {
                    string updateSQL = $"UPDATE backup_files_info SET bfi_path='{destFolderPath}', bfi_pid='{destFolderId}', bfi_date='{DateTime.Now}', bfi_userid='{UserHelper.GetUser().UserKey}' WHERE bfi_id='{objectId}'";
                    SqlHelper.ExecuteNonQuery(updateSQL);

                    FileInfo file = new FileInfo(sourceFilePath);
                    if (!Directory.Exists(destFolderPath))
                        Directory.CreateDirectory(destFolderPath);
                    string newDestFilePath = destFolderPath + "\\" + Path.GetFileName(sourceFilePath);
                    file.CopyTo(newDestFilePath, true);
                }
                DevExpress.XtraEditors.XtraMessageBox.Show("操作成功");
                DialogResult = DialogResult.OK;
                Close();
            }
        }
    }
}
