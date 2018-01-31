using System;
using System.Windows.Forms;

namespace 科技计划项目档案数据采集管理系统.TransferOfRegistration
{
    public partial class Frm_CDRead : Form
    {
        public Frm_CDRead()
        {
            InitializeComponent();
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
            if (pgb_CD.Value == pgb_CD.Maximum)
            {
                if (MessageBox.Show("此操作会覆盖当前已有文件，是否重新读取？", "温馨提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    return;
            }
            //光盘读写【备份】
            string sPath = txt_CD_Path.Text;
            if (!string.IsNullOrEmpty(sPath))
            {
                string[] spSplit = sPath.Split('\\');
                string tPath = Application.StartupPath + "\\BackupFile\\" + spSplit[spSplit.Length - 1];
                btn_Sure.Enabled = false;
                FolderHelper.GetInstance(pgb_CD).CopyDirectory(sPath, tPath, true, SetTipMsg);
                btn_Sure.Enabled = true;
            }
            string dPath = txt_DS_Path.Text;
            if(!string.IsNullOrEmpty(dPath))
            {
                
            }
        }

        private void SetTipMsg(string msg)
        {
            tip.Text = msg;
        }
    }
}
