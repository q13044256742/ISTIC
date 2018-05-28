using System;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace 科技计划项目档案数据采集管理系统.TransferOfRegistration
{
    public partial class Frm_CDRead : DevExpress.XtraEditors.XtraForm
    {
        /// <summary>
        /// 共计文件数
        /// </summary>
        private int count = 0;
        /// <summary>
        /// 导入成功数
        /// </summary>
        private int okCount = 0;
        /// <summary>
        /// 导入失败输
        /// </summary>
        private int noCount = -1;
        private object trcId;
        public Frm_CDRead(object trcId)
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
            this.trcId = trcId;
        }

        private void btn_CD_Choose_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            if(dialog.ShowDialog() == DialogResult.OK)
            {
                txt_CD_Path.Text = dialog.SelectedPath;
            }
        }

        private void btn_DS_Choose_Click(object sender, EventArgs e)
        {
            FileDialog dialog = new OpenFileDialog();
            dialog.Filter = "xml|*.xml";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                txt_DS_Path.Text = dialog.FileName;
            }
        }

        private void Btn_Sure_Click(object sender, EventArgs e)
        {
            string sourPath = txt_CD_Path.Text;
            //光盘读写【非结构化数据】
            //if(!string.IsNullOrEmpty(sourPath))
            //{
                //indexCount = -1;
                //object ipAddress = null;
                //备份光盘文件到远程服务器
            //    if(ServerHelper.GetConnectState(ref ipAddress))
            //    {
            //        btn_Sure.Enabled = false;

            //        int totalFileAmount = Directory.GetFiles(sourPath, "*", SearchOption.AllDirectories).Length;
            //        pgb_CD.Maximum = totalFileAmount;
            //        pgb_CD.Value = pgb_CD.Minimum;

            //        string primaryKey = Guid.NewGuid().ToString();
            //        SqlHelper.ExecuteNonQuery($"INSERT INTO backup_files_info(bfi_id, bfi_sort, bfi_name, bfi_date, bfi_userid, bfi_trcid, bfi_type) VALUES " +
            //            $"('{primaryKey}', '{indexCount++}', '{Path.GetFileName(sourPath)}', '{DateTime.Now}', '{UserHelper.GetInstance().User.UserKey}', '{trcId}', '{1}')");

            //        string rootFolder = @"\\" + ipAddress + @"\共享文件夹\" + Path.GetFileName(sourPath) + @"\";
            //        if(!Directory.Exists(rootFolder))
            //            Directory.CreateDirectory(rootFolder);
            //        CopyFile(sourPath, rootFolder, primaryKey);
            //        MessageBox.Show($"备份完毕,共计{count}个文件。", "温馨提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            //    }
            //    else
            //        MessageBox.Show("备份文件到服务器失败。", "操作失败", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            //}
            /* -------------------- 暂时搁置 -----------------------------
            string dPath = txt_DS_Path.Text;
            if(!string.IsNullOrEmpty(dPath))
            {
                //XML文件读写
            }
             ------------------------------------------------------ */
            //光盘读写【结构化数据】
            //else
            //{
            //    MessageBox.Show("光盘路径不能为空。", "读取失败", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            //}
            //else
            //{
            //    /* 测试注释 */
            //    //当前登录用户主键
            //    string adminId = UserHelper.GetInstance().User.UserKey;
            //    //1条【计划】信息
            //    string mainKey = Guid.NewGuid().ToString();
            //    string mainSql = $"INSERT INTO project_info(pi_id,trc_id,pi_code,pi_name,pi_source_id) VALUES('{mainKey}','{trcId}','Z022017001','02专项办2017年第一次移交项目档案','{adminId}')";
            //    SqlHelper.ExecuteNonQuery(mainSql);
            //    //5条【项目/课题】信息
            //    for(int i = 0; i < 5; i++)
            //    {
            //        string index = i.ToString().PadLeft(3, '0');
            //        string primaryKey = Guid.NewGuid().ToString();
            //        string insertSql = "INSERT INTO project_info(pi_id,pi_code,pi_name,pi_work_status,pi_obj_id,pi_categor,pi_submit_status,pi_source_id) " +
            //            $"VALUES('{primaryKey}','Z0120180201-{index}','测试数据{index}号','{(int)WorkStatus.NonWork}','{mainKey}','{(int)ControlType.Plan_Project}','{(int)ObjectSubmitStatus.NonSubmit}','{adminId}')";
            //        SqlHelper.ExecuteNonQuery(insertSql);
            //        //5条【课题/子课题】信息
            //        for(int j = 0; j < (i == 2 ? 15 : 5); j++)
            //        {
            //            string _index = j.ToString().PadLeft(3, '0');
            //            string _primaryKey = Guid.NewGuid().ToString();
            //            string _insertSql = "INSERT INTO subject_info(si_id,pi_id,si_code,si_name,si_work_status,si_categor,si_submit_status,si_source_id)" +
            //                $"VALUES('{_primaryKey}','{primaryKey}','Z0120180201{index}{_index}','测试数据{index}{_index}号','{(int)WorkStatus.NonWork}','{(int)ControlType.Plan_Project_Topic}','{(int)ObjectSubmitStatus.NonSubmit}','{adminId}')";
            //            SqlHelper.ExecuteNonQuery(_insertSql);
            //        }
            //    }
            //}
        }
        int indexCount = 0;
        /// <summary>
        /// 拷贝文件到备份服务器
        /// </summary>
        /// <param name="sPath">源文件夹路径</param>
        /// <param name="rootFolder">目标文件夹基路径</param>
        private void CopyFile(string sPath, string rootFolder, string pid)
        {
            DirectoryInfo info = new DirectoryInfo(sPath);
            FileInfo[] file = info.GetFiles();
            count += file.Length;
            for(int i = 0; i < file.Length; i++)
            {
                string primaryKey = Guid.NewGuid().ToString();
                try
                {
                    SqlHelper.ExecuteNonQuery($"INSERT INTO backup_files_info(bfi_id, bfi_sort, bfi_name, bfi_path, bfi_date, bfi_pid, bfi_userid, bfi_trcid, bfi_type) VALUES " +
                        $"('{primaryKey}', '{indexCount++}', '{file[i].Name}', '{rootFolder}', '{DateTime.Now}', '{pid}', '{UserHelper.GetInstance().User.UserKey}', '{trcId}', '{0}')");
                    //ServerHelper.UploadFile(file[i].FullName, rootFolder, file[i].Name);
                    okCount++;
                }
                catch(Exception)
                {
                    noCount++;
                }
                pgb_CD.Value++;
            }
            DirectoryInfo[] infos = info.GetDirectories();
            for(int i = 0; i < infos.Length; i++)
            {
                string primaryKey = Guid.NewGuid().ToString();
                SqlHelper.ExecuteNonQuery($"INSERT INTO backup_files_info(bfi_id, bfi_sort, bfi_name, bfi_path, bfi_date, bfi_pid, bfi_userid, bfi_trcid, bfi_type) VALUES " +
                        $"('{primaryKey}', '{indexCount++}', '{infos[i].Name}', '{rootFolder}', '{DateTime.Now.ToString("s")}', '{pid}', '{UserHelper.GetInstance().User.UserKey}', '{trcId}', '{1}')");
                CopyFile(infos[i].FullName, rootFolder + infos[i].Name + @"\", primaryKey);
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
    }
}
