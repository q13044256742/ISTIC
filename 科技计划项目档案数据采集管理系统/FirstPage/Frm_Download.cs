using DevExpress.XtraEditors;
using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace 科技计划项目档案数据采集管理系统
{
    public partial class Frm_Download : XtraForm
    {
        public Frm_Download()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
        }

        private void Frm_Download_Load(object sender, EventArgs e)
        {
            LoadFileListByType(-1);

        }

        private void LoadFileListByType(int type)
        {
            gridControl.DataSource = null;

            string querySQL = $"SELECT CASE at_type " +
                "WHEN 0 THEN '法规制度' " +
                "WHEN 1 THEN '部门规章' " +
                "WHEN 2 THEN '标准规范' " +
                "WHEN 3 THEN '项目/课题清单' " +
                "WHEN 4 THEN '工作文件' " +
                "ELSE '其他' END  " +
                "_at_type, at_id, at_name, at_size, at_date, at_uploader, at_loadticker FROM Attachment WHERE 1=1 ";
            if(type != -1)
            {
                querySQL += $"AND at_type = {type} ";
            }
            querySQL += "ORDER BY at_type";
            DataTable table = SqlHelper.ExecuteQuery(querySQL);
            gridControl.DataSource = table;
            //项目/课题清单不能下载
        }

        private void Btn_Upload_Click(object sender, EventArgs e)
        {
            if(UserHelper.GetUserRole() != UserRole.DocManager)
            {
                XtraMessageBox.Show("仅档案管理员可以上传文件。", "提示");
                return;
            }
            Frm_UploadFile uploadFile = new Frm_UploadFile();
            if(uploadFile.ShowDialog() == DialogResult.OK)
            {
                string filePath = uploadFile.txt_filePath.Text;
                string fileName = uploadFile.txt_fileName.Text;
                string fileCode = uploadFile.txt_fileCode.Text;
                string fileVersion = uploadFile.txt_fileVersion.Text;
                int fileType = uploadFile.cbo_fileType.SelectedIndex;

                progressBar.Visible = true;
                progressBar.Properties.Stopped = false;
                progressBar.Text = "正在上传文件...";
                new Thread(delegate ()
                {
                    string insertSQL = string.Empty;
                    SqlConnection con = SqlHelper.GetConnect();
                    FileInfo info = new FileInfo(filePath);
                    string primaryKey = Guid.NewGuid().ToString();
                    insertSQL += "INSERT INTO Attachment(at_id, at_name, at_size, at_date, at_uploader, at_entity, at_version, at_type, at_code) " +
                        $"VALUES ('{primaryKey}' ,'{fileName}' ,'{(float)(info.Length / 1024f)}' ,'{DateTime.Now}' ,'{UserHelper.GetUser().UserKey}', @fileByte, '{fileVersion}', {fileType}, '{fileCode}');";
                    byte[] fileByte = GetByteFromFile(info);
                    SqlCommand com = new SqlCommand(insertSQL, con);
                    com.Parameters.Add("@fileByte", SqlDbType.Image, fileByte.Length);
                    com.Parameters["@fileByte"].Value = fileByte;
                    com.ExecuteNonQuery();
                    SqlHelper.CloseConnect();
                    progressBar.Properties.Stopped = true;
                    progressBar.Text = "文件上传成功。";
                }).Start();
            }
        }

        private byte[] GetByteFromFile(FileInfo info)
        {
            byte[] result = new byte[info.Length];
            FileStream fs = info.OpenRead();
            fs.Read(result, 0, Convert.ToInt32(fs.Length));
            fs.Close();
            return result;
        }

        private void DownloadButton_MouseDown(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Left)
            {
                object viewRow = view.GetFocusedRow();
                if(viewRow != null)
                {
                    object type = ((DataRowView)viewRow).Row.ItemArray[0];
                    if("项目/课题清单".Equals(type))
                    {
                        XtraMessageBox.Show("此类别文件不可下载", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        return;
                    }
                    folderBrowserDialog1.Description = "选择文件保存路径";
                    if(folderBrowserDialog1.ShowDialog() == DialogResult.OK)
                    {
                        string savePath = folderBrowserDialog1.SelectedPath;
                        object id = ((DataRowView)viewRow).Row.ItemArray[1];
                        object name = ((DataRowView)viewRow).Row.ItemArray[2];
                        SqlConnection con = SqlHelper.GetConnect();
                        string querySQL = $"SELECT at_entity FROM Attachment WHERE at_id='{id}'";
                        SqlDataAdapter dataAdapter = new SqlDataAdapter(querySQL, con);
                        DataSet dataSet = new DataSet();
                        dataAdapter.Fill(dataSet);
                        SqlHelper.CloseConnect();

                        Byte[] fileByte = (Byte[])dataSet.Tables[0].Rows[0]["at_entity"];
                        BinaryWriter bw = new BinaryWriter(File.Open(savePath + "\\" + name, FileMode.OpenOrCreate, FileAccess.Write));
                        bw.Write(fileByte);
                        bw.Close();

                        if(XtraMessageBox.Show("文件下载成功。\r\n是否打开文件所在地址", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk) == DialogResult.Yes)
                        {
                            WinFormOpenHelper.OpenWinForm(0, "open", null, null, savePath, ShowWindowCommands.SW_NORMAL);
                        }
                    }
                }
            }
        }

        private void Btn_Delete_Click(object sender, EventArgs e)
        {
            if(UserHelper.GetUserRole() != UserRole.DocManager)
            {
                XtraMessageBox.Show("仅档案管理员可以删除文件。", "提示");
                return;
            }
            int[] rowCount = view.GetSelectedRows();
            if(rowCount.Length > 0)
            {
                if(XtraMessageBox.Show($"确认删除选中的{rowCount.Length}条数据吗？", "确认提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk) == DialogResult.OK)
                {
                    string ids = string.Empty;
                    foreach(int rowIndex in rowCount)
                        ids += $"'{view.GetRowCellValue(rowIndex, "at_id")}',";
                    if(!string.IsNullOrEmpty(ids))
                        ids = ids.Substring(0, ids.Length - 1);

                    string deleteSQL = $"DELETE FROM Attachment WHERE at_id IN({ids});";
                    SqlHelper.ExecuteNonQuery(deleteSQL);
                    XtraMessageBox.Show("删除成功！");
                    Frm_Download_Load(null, null);
                }
            }
        }

        private void treeList_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            int id = e.Node.Id;
            LoadFileListByType(id);


        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            string key = txt_search.Text;
            if(!string.IsNullOrEmpty(key))
            {
                gridControl.DataSource = null;

                string querySQL = $"SELECT at_id, at_name, at_size, at_date, at_uploader, at_loadticker FROM Attachment WHERE 1=1 " +
                    $"AND at_name LIKE '%{key}%' OR at_code LIKE '%{key}%' ";

                DataTable table = SqlHelper.ExecuteQuery(querySQL);
                gridControl.DataSource = table;
            }
        }
    }
}
