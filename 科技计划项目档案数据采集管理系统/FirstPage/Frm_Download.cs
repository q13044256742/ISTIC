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
            gridControl.DataSource = null;
            DataTable table = SqlHelper.ExecuteQuery($"SELECT at_id, at_name, at_size, at_date, at_uploader, at_loadticker FROM Attachment");
            gridControl.DataSource = table;

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
                string fileCode = uploadFile.txt_fileCode.Text;
                string fileVersion = uploadFile.txt_fileVersion.Text;

                progressBar.Visible = true;
                progressBar.Properties.Stopped = false;
                progressBar.Text = "正在上传文件...";
                new Thread(delegate ()
                {
                    string insertSQL = string.Empty;
                    SqlConnection con = SqlHelper.GetConnect();
                    FileInfo info = new FileInfo(filePath);
                    string primaryKey = Guid.NewGuid().ToString();
                    insertSQL += "INSERT INTO Attachment(at_id, at_name, at_size, at_date, at_uploader, at_entity, at_version, at_code) " +
                        $"VALUES ('{primaryKey}' ,'{info.Name}' ,'{(float)(info.Length / 1024f)}' ,'{DateTime.Now}' ,'{UserHelper.GetUser().UserKey}', @fileByte, '{fileVersion}', '{fileCode}');";
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
                    folderBrowserDialog1.Description = "选择文件保存路径";
                    if(folderBrowserDialog1.ShowDialog() == DialogResult.OK)
                    {
                        string savePath = folderBrowserDialog1.SelectedPath;
                        object id = ((DataRowView)viewRow).Row.ItemArray[0];
                        object name = ((DataRowView)viewRow).Row.ItemArray[1];
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
        }
    }
}
