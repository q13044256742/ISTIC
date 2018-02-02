using System;
using System.Windows.Forms;

namespace 科技计划项目档案数据采集管理系统.TransferOfRegistration
{
    public partial class Frm_CDRead : Form
    {
        private object trcId;
        public Frm_CDRead(object trcId)
        {
            InitializeComponent();
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
            //if (pgb_CD.Value == pgb_CD.Maximum)
            //{
            //    if (MessageBox.Show("此操作会覆盖当前已有文件，是否重新读取？", "温馨提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            //        return;
            //}
            ////光盘读写【备份】
            //string sPath = txt_CD_Path.Text;
            //if (!string.IsNullOrEmpty(sPath))
            //{
            //    string[] spSplit = sPath.Split('\\');
            //    string tPath = Application.StartupPath + "\\BackupFile\\" + spSplit[spSplit.Length - 1];
            //    btn_Sure.Enabled = false;
            //    FolderHelper.GetInstance(pgb_CD).CopyDirectory(sPath, tPath, true, SetTipMsg);
            //    btn_Sure.Enabled = true;
            //}
            //string dPath = txt_DS_Path.Text;
            //if(!string.IsNullOrEmpty(dPath))
            //{
            //    //XML文件读写
            //}

            /* 测试注释 */
            //当前登录用户主键
            string adminId = UserHelper.GetInstance().User.UserKey;
            //1条【计划】信息
            string mainKey = Guid.NewGuid().ToString();
            string mainSql = $"INSERT INTO project_info(pi_id,trc_id,pi_code,pi_name,pi_source_id) VALUES('{mainKey}','{trcId}','APTX-4869','アポトキシン4869','{adminId}')";
            SqlHelper.ExecuteNonQuery(mainSql);
            //5条【项目/课题】信息
            for(int i = 0; i < 5; i++)
            {
                string index = i.ToString().PadLeft(3, '0');
                string primaryKey = Guid.NewGuid().ToString();
                string insertSql = "INSERT INTO project_info(pi_id,trc_id,pi_code,pi_name,pi_work_status,pi_obj_id,pi_categor,pi_submit_status,pi_source_id) " +
                    $"VALUES('{primaryKey}','{trcId}','Z0120180201-{index}','我是测试数据{index}号','{(int)WorkStatus.NonWork}','{mainKey}','{(int)ControlType.Plan_Project}','{(int)ObjectSubmitStatus.NonSubmit}','{adminId}')";
                SqlHelper.ExecuteNonQuery(insertSql);
                //5条【课题/子课题】信息
                for(int j = 0; j < 5; j++)
                {
                    string _index = j.ToString().PadLeft(3, '0');
                    string _primaryKey = Guid.NewGuid().ToString();
                    string _insertSql = "INSERT INTO subject_info(si_id,pi_id,si_code,si_name,si_work_status,si_categor,si_submit_status,si_source_id)" +
                        $"VALUES('{_primaryKey}','{primaryKey}','Z0120180201-{index}-{_index}','我是测试数据{index}_{_index}号','{(int)WorkStatus.NonWork}','{(int)ControlType.Plan_Project_Topic}','{(int)ObjectSubmitStatus.NonSubmit}','{adminId}')";
                    SqlHelper.ExecuteNonQuery(_insertSql);
                }
            }
            //更新光盘信息
            string updateSql = $"UPDATE transfer_registraion_cd SET trc_status='{(int)ReadStatus.ReadSuccess}' WHERE trc_id='{trcId}'";
            SqlHelper.ExecuteNonQuery(updateSql);

            MessageBox.Show("测试数据添加成功");


            DialogResult = DialogResult.OK;
            Close();
        }

        private void SetTipMsg(string msg)
        {
            tip.Text = msg;
        }
    }
}
