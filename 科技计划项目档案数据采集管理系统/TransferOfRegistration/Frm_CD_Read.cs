using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace 科技计划项目档案数据采集管理系统.TransferOfRegistration
{
    public partial class Frm_CD_Read : Form
    {
        public Frm_CD_Read()
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
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                txt_DS_Path.Text = dialog.FileName;
            }
        }

        private void btn_Sure_Click(object sender, EventArgs e)
        {
            //光盘读写【备份】
            string sPath = txt_CD_Path.Text;
            if (!string.IsNullOrEmpty(sPath))
            {
                string[] spSplit = sPath.Split('\\');
                string tPath = Application.StartupPath + "\\BackupFile\\" + spSplit[spSplit.Length - 1];
                CopyFile(sPath, tPath);
                MessageBox.Show("ok");
            }
        }

        List<FileCopyLog> list = new List<FileCopyLog>();

        /// <summary>
        /// 复制文件
        /// </summary>
        /// <param name="sPath">源文件路径</param>
        /// <param name="tPath">目标文件路径</param>
        private void CopyFile(string sPath, string tPath)
        {
            DirectoryInfo directory = new DirectoryInfo(sPath);
            if (directory.Exists)
            {
                FileInfo[] fis = directory.GetFiles();
                for (int i = 0; i < fis.Length; i++)
                {
                    string destFileName = tPath + "\\" + fis[i].Name;
                    try
                    {
                        if (Directory.Exists(tPath))
                            Directory.Delete(tPath, true);
                        Directory.CreateDirectory(tPath);
                        File.Create(destFileName).Close();
                        fis[i].CopyTo(destFileName, true);
                    }catch( Exception e)
                    {
                        list.Add(new FileCopyLog(destFileName, e.Message));
                    }
                }

                DirectoryInfo[] dis = directory.GetDirectories();
                for (int i = 0; i < dis.Length; i++)
                {
                    sPath = sPath + "\\" + dis[i];
                    tPath = tPath + "\\" + dis[i];
                    CopyFile(sPath, tPath);
                }

            }
        }
    }
}
