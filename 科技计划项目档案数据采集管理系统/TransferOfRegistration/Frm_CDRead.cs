using System;
using System.Data;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using 科技计划项目档案数据采集管理系统.Tools;

namespace 科技计划项目档案数据采集管理系统.TransferOfRegistration
{
    public partial class Frm_CDRead : DevExpress.XtraEditors.XtraForm
    {
        private object trcId;
        private object trpName;
        public Frm_CDRead(object trcId, object trpName)
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
            this.trcId = trcId;
            this.trpName = trpName;
        }

        private void Btn_Sure_Click(object sender, EventArgs e)
        {
            if(!string.IsNullOrEmpty(txt_CD_Path.Text) && string.IsNullOrEmpty(txt_SavePath.Text))
                return;
            if(string.IsNullOrEmpty(txt_CD_Path.Text) && string.IsNullOrEmpty(txt_DS_Path.Text))
                return;
            string targetPath = txt_SavePath.Text;
            if(!Directory.Exists(targetPath))
                Directory.CreateDirectory(targetPath);
            SetButtonState();
            string sourPath = txt_CD_Path.Text;
            /* -------------------- 光盘读写【非结构化数据】 -----------------------------*/
            if(!string.IsNullOrEmpty(sourPath))
            {
                new Thread(delegate ()
                {
                    DirectoryInfo directoryInfo = new DirectoryInfo(sourPath);
                    pgb_CD.Tag = false;
                    int totalFileAmount = GetFilesCount(directoryInfo);
                    SetDocProcessTip(0, totalFileAmount);
                    pgb_CD.Value = pgb_CD.Minimum;
                    pgb_CD.Maximum = totalFileAmount;

                    object localKey = SqlHelper.ExecuteOnlyOneQuery($"SELECT bfi_id FROM backup_files_info WHERE bfi_name='{trpName}' AND bfi_trcid='{trcId}'");
                    if(localKey != null)
                        SqlHelper.ExecuteNonQuery($"UPDATE backup_files_info SET bfi_date='{DateTime.Now}', bfi_userid='{UserHelper.GetUser().UserKey}', bfi_trcid='{trcId}' WHERE bfi_id='{localKey}'");
                    else
                    {
                        localKey = Guid.NewGuid().ToString();
                        SqlHelper.ExecuteNonQuery($"INSERT INTO backup_files_info(bfi_id, bfi_name, bfi_date, bfi_userid, bfi_trcid, bfi_type) VALUES " +
                            $"('{localKey}', '{trpName}', '{DateTime.Now}', '{UserHelper.GetUser().UserKey}', '{trcId}', -1)");
                    }
                    CopyFile(directoryInfo, targetPath, localKey, totalFileAmount);
                    pgb_CD.Tag = true;
                    SetButtonState();
                    DevExpress.XtraEditors.XtraMessageBox.Show("文件备份完成。");
                }).Start();
            }
            else
                pgb_CD.Tag = true;
           
            /* -------------------- 源数据读写【结构化数据】 -----------------------------*/
            string dPath = txt_DS_Path.Text;
            if(!string.IsNullOrEmpty(dPath))
            {
                pgb_DS.Tag = false;
                string queryString = "SELECT COUNT(pi_id)+ " +
                    "(SELECT COUNT(spi_id) FROM special_info) +" +
                    "(SELECT COUNT(ti_id) FROM topic_info) +" +
                    "(SELECT COUNT(si_id) FROM subject_info) +" +
                    "(SELECT COUNT(fi_id) FROM files_info) +" +
                    "(SELECT COUNT(pfo_id) FROM files_lost_info) +" +
                    "(SELECT COUNT(pb_id) FROM files_box_info) " +
                    "FROM project_info";
                int totalAmount = new SQLiteBackupHelper(dPath).ExecuteCountQuery(queryString);
                pgb_DS.Value = pgb_DS.Minimum;
                pgb_DS.Maximum = totalAmount;

                CopyDataTableInstince(dPath, targetPath);
                pgb_DS.Tag = true;
                SetButtonState();
                DevExpress.XtraEditors.XtraMessageBox.Show("源数据读写完成。");
            }
            else
                pgb_DS.Tag = true;
        }

        private void SetDocProcessTip(params object[] value)
        {
            lbl_DocProcess.Text = $"文档读写进度（{value[0]}/{value[1]}）";
        }

        /// <summary>
        /// 获取指定文件夹下的所有文件总数
        /// </summary>
        /// <param name="dirInfo">指定文件夹</param>
        private int GetFilesCount(DirectoryInfo dirInfo)
        {
            int totalFile = 0;
            if(!IsSystemHidden(dirInfo))
            {
                totalFile += dirInfo.GetFiles().Length;
                foreach(DirectoryInfo subdir in dirInfo.GetDirectories())
                    totalFile += GetFilesCount(subdir);
            }
            return totalFile;
        }

        /// <summary>
        /// 判断指定文件夹是否是系统文件夹
        /// </summary>
        private bool IsSystemHidden(DirectoryInfo dirInfo)
        {
            if(dirInfo.Parent == null)
            {
                return false;
            }
            string attributes = dirInfo.Attributes.ToString();
            if(attributes.IndexOf("Hidden") > -1 || attributes.IndexOf("System") > -1)
            {
                return true;
            }
            return false;
        }

        private void SetButtonState()
        {
            if(true.Equals(pgb_CD.Tag) && true.Equals(pgb_DS.Tag))
            {
                btn_Sure.Enabled = true;
                Text += "[读取成功]";
            }
            else
                btn_Sure.Enabled = false;
        }

        /// <summary>
        /// 将光盘中的文档复制到本地（服务器）
        /// </summary>
        /// <param name="sPath">源文件夹目录</param>
        /// <param name="tPath">目标文件夹基路径</param>
        private void CopyFile(DirectoryInfo sPath, string tPath, object pid, int totalFileAmount)
        {
            FileInfo[] file = sPath.GetFiles();
            for(int i = 0; i < file.Length; i++)
            {
                string fileName = file[i].Name;
                string primaryKey = Guid.NewGuid().ToString();
                SqlHelper.ExecuteNonQuery($"INSERT INTO backup_files_info(bfi_id, bfi_name, bfi_path, bfi_date, bfi_pid, bfi_userid, bfi_trcid, bfi_type) VALUES " +
                    $"('{primaryKey}', '{fileName}', '{tPath}', '{DateTime.Now}', '{pid}', '{UserHelper.GetUser().UserKey}', '{trcId}', 0)");

                UploadFile(file[i].FullName, tPath, fileName);
                pgb_CD.Value++;
                SetDocProcessTip(pgb_CD.Value, totalFileAmount);
            }
            DirectoryInfo[] infos = sPath.GetDirectories();
            for(int i = 0; i < infos.Length; i++)
            {
                if(!IsSystemHidden(infos[i]))
                {
                    string primaryKey = Guid.NewGuid().ToString();
                    SqlHelper.ExecuteNonQuery($"INSERT INTO backup_files_info(bfi_id, bfi_name, bfi_path, bfi_date, bfi_pid, bfi_userid, bfi_trcid, bfi_type) VALUES " +
                       $"('{primaryKey}', '{infos[i].Name}', '{tPath}', '{DateTime.Now}', '{pid}', '{UserHelper.GetUser().UserKey}', '{trcId}', 1)");
                    CopyFile(infos[i], tPath + "\\" + infos[i].Name + @"\", primaryKey, totalFileAmount);
                }
            }
        }

        /// <summary>  
        /// 将本地文件上传到远程服务器共享目录  
        /// </summary>  
        /// <param name="src">本地文件的绝对路径，包含扩展名</param>  
        /// <param name="dst">远程服务器共享文件路径，不包含文件扩展名</param>  
        /// <param name="fileName">上传到远程服务器后的文件扩展名</param>  
        public static void UploadFile(string src, string dst, string fileName)
        {
            try
            {
                FileStream inFileStream = new FileStream(src, FileMode.Open, FileAccess.Read);    //此处假定本地文件存在，不然程序会报错     
                if(!Directory.Exists(dst))        //判断上传到的远程服务器路径是否存在  
                    Directory.CreateDirectory(dst);
                dst = dst + "\\" + fileName;            //上传到远程服务器共享文件夹后文件的绝对路径  
                FileStream outFileStream = new FileStream(dst, FileMode.OpenOrCreate);
                byte[] buf = new byte[inFileStream.Length];
                int byteCount;
                while((byteCount = inFileStream.Read(buf, 0, buf.Length)) > 0)
                    outFileStream.Write(buf, 0, byteCount);
                inFileStream.Flush();
                inFileStream.Close();
                outFileStream.Flush();
                outFileStream.Close();
            }
            catch(Exception e)
            {
                LogsHelper.AddErrorLogs("UploadFile", e.Message);
            }
        }

        private void Lbl_CdPath_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            if(dialog.ShowDialog() == DialogResult.OK)
            {
                txt_CD_Path.Text = dialog.SelectedPath;
            }
        }

        private void Lbl_DataPath_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FileDialog dialog = new OpenFileDialog();
            dialog.Filter = "db|*.db";
            dialog.InitialDirectory = txt_CD_Path.Text;
            if(dialog.ShowDialog() == DialogResult.OK)
            {
                txt_DS_Path.Text = dialog.FileName;
            }
        }
        
        /// <summary>
        /// 拷贝数据库
        /// </summary>
        /// <param name="rootFolder">课题组数据库文件路径</param>
        private void CopyDataTableInstince(string dataBasePath, string rootFolder)
        {
            SQLiteBackupHelper helper = new SQLiteBackupHelper(dataBasePath);

            StringBuilder sqlString = new StringBuilder();
            DataTable specialTable = helper.ExecuteQuery($"SELECT * FROM special_info");
            int length = specialTable.Rows.Count;
            for(int i = 0; i < length; i++)
            {
                DataRow row = specialTable.Rows[i];
                sqlString.Append($"DELETE FROM project_info WHERE pi_id='{row["spi_id"]}';");
                sqlString.Append("INSERT INTO project_info(pi_id, pi_code, pi_name, pi_unit, pi_obj_id, pi_categor, pi_submit_status, pi_source_id) " +
                    $"VALUES ('{row["spi_id"]}', '{row["spi_code"]}', '{row["spi_name"]}', '{row["spi_unit"]}', '{trcId}', '{(int)ControlType.Plan}', 1, '{UserHelper.GetUser().UserKey}');");
                pgb_DS.Value += 1;
            }
            SqlHelper.ExecuteNonQuery(sqlString.ToString());

            DataTable projectTable = helper.ExecuteQuery($"SELECT * FROM project_info");
            length = projectTable.Rows.Count;
            sqlString = new StringBuilder();
            for(int i = 0; i < length; i++)
            {
                //SetTip($"正在导入项目数据({i + 1}\\{length})");
                DataRow row = projectTable.Rows[i];
                sqlString.Append($"DELETE FROM project_info WHERE pi_id='{row["pi_id"]}';");
                sqlString.Append("INSERT INTO project_info([pi_id], [trc_id], [pi_code], [pi_name], [pi_field], [pb_theme], [pi_funds], [pi_start_datetime] " +
                    ",[pi_end_datetime], [pi_year] ,[pi_unit] ,[pi_province] ,[pi_uniter],[pi_prouser] ,[pi_intro] ,[pi_work_status] ,[pi_obj_id] " +
                    ",[pi_categor] ,[pi_submit_status] ,[pi_source_id]) " +
                    $" VALUES('{row["pi_id"]}', '{trcId}', '{row["pi_code"]}', '{row["pi_name"]}', '{row["pi_field"]}', '{row["pi_theme"]}', '{row["pi_funds"]}', '{row["pi_startdate"]}', '{row["pi_finishdate"]}'" +
                    $",'{row["pi_year"]}', '{row["pi_unit"]}', '{row["pi_province"]}', '{row["pi_unit_user"]}', '{row["pi_project_user"]}', '{row["pi_introduction"]}', {(int)WorkStatus.NonWork}, '{row["pi_obj_id"]}'" +
                    $",'{(int)ControlType.Project}', '{(int)ObjectSubmitStatus.NonSubmit}' ,'{UserHelper.GetUser().UserKey}');");
                pgb_DS.Value += 1;
            }
            SqlHelper.ExecuteNonQuery(sqlString.ToString());

            sqlString = new StringBuilder();
            DataTable topicTable = helper.ExecuteQuery($"SELECT * FROM topic_info");
            length = topicTable.Rows.Count;
            object _trcId = helper.ExecuteOnlyOneQuery($"SELECT pi_obj_id FROM project_info");
            for(int i = 0; i < length; i++)
            {
                //SetTip($"正在导入课题数据({i + 1}\\{length})");
                DataRow row = topicTable.Rows[i];
                object tid = row["ti_obj_id"];
                
                sqlString.Append($"DELETE FROM topic_info WHERE ti_id='{row["ti_id"]}';");
                sqlString.Append("INSERT INTO topic_info ([ti_id], [trc_id],[ti_code] ,[ti_name],[ti_field],[tb_theme],[ti_funds],[ti_start_datetime],[ti_end_datetime],[ti_year]" +
                    ",[ti_unit],[ti_province],[ti_uniter],[ti_prouser],[ti_work_status],[ti_intro],[ti_obj_id],[ti_categor],[ti_submit_status],[ti_source_id]) " +
                    $"VALUES('{row["ti_id"]}','{(_trcId.Equals(tid) ? trcId : null)}', '{row["ti_code"]}', '{row["ti_name"]}', '{row["ti_field"]}', '{row["ti_theme"]}', '{row["ti_funds"]}', '{row["ti_startdate"]}', '{row["ti_finishdate"]}'" +
                    $",'{row["ti_year"]}', '{row["ti_unit"]}', '{row["ti_province"]}', '{row["ti_unit_user"]}', '{row["ti_project_user"]}', '{(int)WorkStatus.NonWork}', '{ row["ti_introduction"]}', '{tid}'" +
                    $",'{(int)ControlType.Topic}', '{(int)ObjectSubmitStatus.NonSubmit}' ,'{UserHelper.GetUser().UserKey}');");
                pgb_DS.Value += 1;
            }
            SqlHelper.ExecuteNonQuery(sqlString.ToString());

            sqlString = new StringBuilder();
            DataTable subjectTable = helper.ExecuteQuery($"SELECT * FROM subject_info");
            length = subjectTable.Rows.Count;
            for(int i = 0; i < length; i++)
            {
                //SetTip($"正在导入子课题数据({i + 1}\\{length})");
                DataRow row = subjectTable.Rows[i];
                sqlString.Append($"DELETE FROM subject_info WHERE si_id='{row["si_id"]}';");
                sqlString.Append("INSERT INTO subject_info ([si_id], [si_code], [si_name], [si_field], [si_theme], [si_funds], [si_start_datetime], [si_end_datetime]" +
                    ",[si_year],[si_unit],[si_province],[si_uniter],[si_prouser],[si_intro],[si_obj_id],[si_work_status],[si_categor],[si_submit_status],[si_source_id])" +
                    "VALUES(" +
                    $"'{row["si_id"]}', '{row["si_code"]}', '{row["si_name"]}', '{row["si_field"]}', '{row["si_theme"]}', '{row["si_funds"]}', '{row["si_startdate"]}', '{row["si_finishdate"]}'," +
                    $"'{row["si_year"]}', '{row["si_unit"]}', '{row["si_province"]}', '{row["si_unit_user"]}', '{row["si_project_user"]}', '{row["si_introduction"]}', '{row["si_obj_id"]}', '{(int)WorkStatus.NonWork}'" +
                    $",'{(int)ControlType.Subject}', '{(int)ObjectSubmitStatus.NonSubmit}' ,'{UserHelper.GetUser().UserKey}');");
                pgb_DS.Value += 1;
            }
            SqlHelper.ExecuteNonQuery(sqlString.ToString());

            sqlString = new StringBuilder();
            DataTable fileTable = helper.ExecuteQuery($"SELECT * FROM files_info");
            length = fileTable.Rows.Count;
            for(int i = 0; i < length; i++)
            {
                //SetTip($"正在导入文件基础数据({i + 1}\\{length})");
                DataRow row = fileTable.Rows[i];
                string link = GetValue(row["fi_link"]).Trim();
                object fileId = row["fi_file_id"];
                //尝试转换文件的link路径-转换为当前服务器链接
                if(!string.IsNullOrEmpty(link) && Directory.Exists(rootFolder))
                {
                    string fileName = Path.GetFileName(link);
                    string[] files = Directory.GetFiles(rootFolder, "*" + fileName, SearchOption.AllDirectories);
                    if(files.Length == 1)
                        link = files[0];
                    else
                        for(int j = 0; j < files.Length; j++)
                        {
                            string parent = Directory.GetParent(files[j]).Name;
                            string real = GetRealParentName(row["fi_obj_id"]).Trim();
                            if(string.IsNullOrEmpty(real))
                                link = real;
                            else if(parent.Equals(real))
                            {
                                link = files[j];
                                break;
                            }
                        }
                    string filePath = Path.GetDirectoryName(link);
                    string _fileName = Path.GetFileName(link);
                    sqlString.Append($"UPDATE backup_files_info SET bfi_state=1 WHERE bfi_path='{filePath}\\' AND bfi_name='{_fileName}';");
                }

                //更新文件备份表状态
                string _filePath = Path.GetDirectoryName(link);
                string __fileName = Path.GetFileName(link);
                fileId = SqlHelper.ExecuteOnlyOneQuery($"SELECT bfi_id FROM backup_files_info WHERE bfi_path='{_filePath}\\' AND bfi_name='{__fileName}';") ?? fileId;

                sqlString.Append($"DELETE FROM processing_file_list WHERE pfl_id='{row["fi_id"]}';");
                sqlString.Append("INSERT INTO processing_file_list(pfl_id, pfl_stage, pfl_categor, pfl_code, pfl_name, pfl_user, pfl_type, pfl_pages, pfl_amount, pfl_date, pfl_unit, pfl_carrier, pfl_format, pfl_link, pfl_file_id, pfl_status, pfl_obj_id, pfl_sort, pfl_remark) VALUES(" +
                    $"'{row["fi_id"]}', '{row["fi_stage"]}', '{row["fi_categor"]}', '{row["fi_code"]}', '{row["fi_name"]}', '{row["fi_user"]}', '{row["fi_type"]}', '{row["fi_pages"]}', '{row["fi_count"]}', " +
                    $"'{row["fi_create_date"]}', '{row["fi_unit"]}', '{row["fi_carrier"]}', '{row["fi_format"]}', '{link}', '{fileId}', '{row["fi_status"]}', '{row["fi_obj_id"]}', '{row["fi_sort"]}', '{row["fi_remark"]}');");
                pgb_DS.Value += 1;
            }
            SqlHelper.ExecuteNonQuery(sqlString.ToString());

            sqlString = new StringBuilder();
            DataTable lostTable = helper.ExecuteQuery($"SELECT * FROM files_lost_info");
            length = lostTable.Rows.Count;
            for(int i = 0; i < length; i++)
            {
                //SetTip($"正在导入缺失文件数据({i + 1}\\{length})");
                DataRow row = lostTable.Rows[i];
                sqlString.Append($"DELETE FROM processing_file_lost WHERE pfo_id='{row["pfo_id"]}';");
                sqlString.Append($"INSERT INTO processing_file_lost([pfo_id],[pfo_categor],[pfo_name],[pfo_reason],[pfo_remark],[pfo_obj_id]) " +
                    $"VALUES('{row["pfo_id"]}', '{row["pfo_categor"]}', '{row["pfo_name"]}', '{row["pfo_reason"]}', '{row["pfo_remark"]}', '{row["pfo_obj_id"]}');");
                pgb_DS.Value += 1;
            }
            SqlHelper.ExecuteNonQuery(sqlString.ToString());


            sqlString = new StringBuilder();
            DataTable boxTable = helper.ExecuteQuery($"SELECT * FROM files_box_info");
            length = boxTable.Rows.Count;
            for(int i = 0; i < length; i++)
            {
                //SetTip($"正在导入卷盒信息数据({i + 1}\\{length})");
                DataRow row = boxTable.Rows[i];
                sqlString.Append($"DELETE FROM processing_box WHERE pb_id='{row["pb_id"]}';");
                sqlString.Append($"INSERT INTO processing_box(pb_id, pb_box_number, pb_gc_id, pb_files_id, pb_obj_id, pb_unit_id) " +
                    $"VALUES('{row["pb_id"]}', '{row["pb_box_number"]}', '{row["pb_gc_id"]}', '{row["pb_files_id"]}', '{row["pb_obj_id"]}', '{row["pb_special_id"]}');");
                pgb_DS.Value += 1;
            }
            SqlHelper.ExecuteNonQuery(sqlString.ToString());
        }

        private string GetRealParentName(object id)
        {
            object name = SqlHelper.ExecuteOnlyOneQuery($"SELECT pi_name FROM project_info WHERE pi_id='{id}'");
            if(name == null)
                name = SqlHelper.ExecuteOnlyOneQuery($"SELECT ti_name FROM topic_info WHERE ti_id='{id}'");
            if(name == null)
                name = SqlHelper.ExecuteOnlyOneQuery($"SELECT si_name FROM subject_info WHERE si_id='{id}'");
            return GetValue(name);
        }

        private string GetValue(object value) => value == null ? string.Empty : value.ToString();

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Lbl_SavePath_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if(folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                txt_SavePath.Text = folderBrowserDialog1.SelectedPath;
            }
        }
    }
}
