using System;
using System.Data;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace 科技计划项目档案数据采集管理系统.TransferOfRegistration
{
    public partial class Frm_CDRead : DevExpress.XtraEditors.XtraForm
    {
        /// <summary>
        /// 导入成功数
        /// </summary>
        private int okCount = 0;
        /// <summary>
        /// 导入失败数
        /// </summary>
        private int noCount = 0;
        private object trcId;
        public Frm_CDRead(object trcId)
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
            this.trcId = trcId;
        }

        private void btn_CD_Choose_Click(object sender, EventArgs e)
        {
            
        }

        private void btn_DS_Choose_Click(object sender, EventArgs e)
        {
            
        }

        private void Btn_Sure_Click(object sender, EventArgs e)
        {
            string targetDirec = Application.StartupPath + "\\BackupFolder\\";
            if(!Directory.Exists(targetDirec))
                Directory.CreateDirectory(targetDirec);

            string sourPath = txt_CD_Path.Text;
            //光盘读写【非结构化数据】
            if(!string.IsNullOrEmpty(sourPath))
            {

                new Thread(delegate ()
                {
                    int totalFiles = Directory.GetFiles(sourPath, "*", SearchOption.AllDirectories).Length;
                    pgb_CD.Maximum = totalFiles;
                    CopyFile(sourPath, targetDirec, pgb_CD);
                }).Start();
            }
            /* -------------------- 源数据读写【结构化数据】 -----------------------------*/
            string dPath = txt_DS_Path.Text;
            if(!string.IsNullOrEmpty(dPath))
            {
                new Thread(delegate ()
                {
                    string queryString = "SELECT COUNT(pi_id)+ " +
                    "(SELECT COUNT(ti_id) FROM topic_info) +" +
                    "(SELECT COUNT(si_id) FROM subject_info) +" +
                    "(SELECT COUNT(fi_id) FROM files_info)+" +
                    "(SELECT COUNT(pfo_id) FROM files_lost_info)+" +
                    "(SELECT COUNT(pb_id) FROM files_box_info) " +
                    "FROM project_info";
                    int totalAmount = new SQLiteBackupHelper(dPath).ExecuteCountQuery(queryString);
                    pgb_DS.Maximum = totalAmount;
                    CopyDataTableInstince(dPath, targetDirec, pgb_DS);

                }).Start();

                /* --
                //当前登录用户主键
                string adminId = UserHelper.GetInstance().User.UserKey;
                //1条【计划】信息
                string mainKey = Guid.NewGuid().ToString();
                string mainSql = $"INSERT INTO project_info(pi_id,trc_id,pi_code,pi_name,pi_source_id) VALUES('{mainKey}','{trcId}','Z022017001','02专项办2017年第一次移交项目档案','{adminId}')";
                SqlHelper.ExecuteNonQuery(mainSql);
                //5条【项目/课题】信息
                for(int i = 0; i < 5; i++)
                {
                    string index = i.ToString().PadLeft(3, '0');
                    string primaryKey = Guid.NewGuid().ToString();
                    string insertSql = "INSERT INTO project_info(pi_id,pi_code,pi_name,pi_work_status,pi_obj_id,pi_categor,pi_submit_status,pi_source_id) " +
                        $"VALUES('{primaryKey}','Z0120180201-{index}','测试数据{index}号','{(int)WorkStatus.NonWork}','{mainKey}','{(int)ControlType.Plan_Project}','{(int)ObjectSubmitStatus.NonSubmit}','{adminId}')";
                    SqlHelper.ExecuteNonQuery(insertSql);
                    //5条【课题/子课题】信息
                    for(int j = 0; j < (i == 2 ? 15 : 5); j++)
                    {
                        string _index = j.ToString().PadLeft(3, '0');
                        string _primaryKey = Guid.NewGuid().ToString();
                        string _insertSql = "INSERT INTO subject_info(si_id,pi_id,si_code,si_name,si_work_status,si_categor,si_submit_status,si_source_id)" +
                            $"VALUES('{_primaryKey}','{primaryKey}','Z0120180201{index}{_index}','测试数据{index}{_index}号','{(int)WorkStatus.NonWork}','{(int)ControlType.Plan_Project_Topic}','{(int)ObjectSubmitStatus.NonSubmit}','{adminId}')";
                        SqlHelper.ExecuteNonQuery(_insertSql);
                    }
                }
                --*/
            }
        }
        /// <summary>
        /// 将光盘中的文档复制到本地（服务器）
        /// </summary>
        /// <param name="sPath">源文件夹路径</param>
        /// <param name="tPath">目标文件夹基路径</param>
        private void CopyFile(string sPath, string tPath, ProgressBar progressBar)
        {
            DirectoryInfo info = new DirectoryInfo(sPath);
            FileInfo[] file = info.GetFiles();
            for(int i = 0; i < file.Length; i++)
            {
                try
                {
                    if(!Directory.Exists(tPath))
                        Directory.CreateDirectory(tPath);
                    string _filePath = tPath + file[i].Name;
                    if(!File.Exists(_filePath))
                        File.Create(_filePath).Close();
                    File.Copy(file[i].FullName, _filePath, true);
                    okCount++;
                }
                catch(Exception e)
                {
                    noCount++;
                }
                progressBar.Value++;
            }
            DirectoryInfo[] infos = info.GetDirectories();
            for(int i = 0; i < infos.Length; i++)
            {
                CopyFile(infos[i].FullName, tPath + infos[i].Name + "\\", progressBar);
            }
        }

        private void Frm_CDRead_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(pgb_CD.Value == pgb_CD.Maximum)
            {
                DialogResult = DialogResult.OK;
            }
            else
                DialogResult = DialogResult.No;
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
            if(dialog.ShowDialog() == DialogResult.OK)
            {
                txt_DS_Path.Text = dialog.FileName;
            }
        }
        
        /// <summary>
        /// 拷贝数据库
        /// </summary>
        /// <param name="rootFolder">课题组数据库文件路径</param>
        private void CopyDataTableInstince(string dataBasePath, string rootFolder, ProgressBar progressBar)
        {
            SQLiteBackupHelper helper = new SQLiteBackupHelper(dataBasePath);

            DataTable projectTable = helper.ExecuteQuery($"SELECT * FROM project_info");
            int length = projectTable.Rows.Count;
            StringBuilder sqlString = new StringBuilder();
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
                    $",'{(int)ControlType.Project}', '{(int)ObjectSubmitStatus.NonSubmit}' ,'{UserHelper.GetInstance().User.UserKey}');");
                progressBar.Value += 1;
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
                    $",'{(int)ControlType.Topic}', '{(int)ObjectSubmitStatus.NonSubmit}' ,'{UserHelper.GetInstance().User.UserKey}');");
                progressBar.Value += 1;
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
                    $",'{(int)ControlType.Subject}', '{(int)ObjectSubmitStatus.NonSubmit}' ,'{UserHelper.GetInstance().User.UserKey}');");
                progressBar.Value += 1;
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
                progressBar.Value += 1;
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
                progressBar.Value += 1;
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
                progressBar.Value += 1;
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
    }
}
