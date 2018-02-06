using System;
using System.IO;
using System.Windows.Forms;

namespace 科技计划项目档案数据采集管理系统
{
    /// <summary>
    /// 文件夹操作类
    /// </summary>
    public class FolderHelper
    {
        private static FolderHelper _folderHelper;
        private ProgressBar progressBar;
        public FolderHelper(ProgressBar progressBar)
        {
            this.progressBar = progressBar;
        }

        /// <summary>
        /// 实例化一个新的文件操作类
        /// </summary>
        /// <param name="progressBar">进度条</param>
        public static FolderHelper GetInstance(ProgressBar progressBar)
        {
            if (_folderHelper == null)
                _folderHelper = new FolderHelper(progressBar);
            return _folderHelper;
        }

        /// <summary>
        /// 复制文件夹
        /// </summary>
        /// <param name="sourceDirPath">源文件夹目录</param>
        /// <param name="saveDirPath">目标文件夹目录</param>
        /// <param name="initialBar">是否初始化进度条</param>
        /// <param name="SetTipMsg">设置提示语句</param>
        public void CopyDirectory(string sourceDirPath, string saveDirPath, bool initialBar, Action<string> SetTipMsg)
        {
            if (initialBar)
            {
                progressBar.Value = progressBar.Minimum = 0;
                progressBar.Maximum = GetFilesCount(new DirectoryInfo(sourceDirPath));
                progressBar.Tag = saveDirPath;
                if (Directory.Exists(saveDirPath))
                {
                    Directory.Delete(saveDirPath, true);
                }
            }
            string[] files = Directory.GetFiles(sourceDirPath);
            foreach (string file in files)
            {
                string pFilePath = saveDirPath + "\\" + Path.GetFileName(file);

                if (!Directory.Exists(saveDirPath)) Directory.CreateDirectory(saveDirPath);
                File.Copy(file, pFilePath, true);
                progressBar.Value += 1;
                if (progressBar.Value == progressBar.Maximum)
                {
                    MessageBox.Show(text: $"本次共读取文件{progressBar.Value}个", caption: "读写完毕", buttons: MessageBoxButtons.OK, icon: MessageBoxIcon.Asterisk);

                    //new System.Threading.Thread(delegate ()
                    //{
                    //    FilesBackupsHelper.GetInstance()
                    //        .DeleteFile(progressBar.Tag)
                    //        .UploadFile(progressBar.Tag, SetTipMsg);
                    //FilesBackupsHelper.GetInstance().AnalyticalFile(progressBar.Tag);
                    //    SetTipMsg(">>数据库备份完毕，请等待服务器解析文件。");
                    //}).Start();
                    //MessageBox.Show("已将文件信息备份至数据库！");
                }
            }
            string[] dirs = Directory.GetDirectories(sourceDirPath);
            foreach (string dir in dirs)
            {
                CopyDirectory(dir, saveDirPath + "\\" + Path.GetFileName(dir), false, SetTipMsg);
            }
        }

        /// <summary>
        /// 统计指定文件下的文件数量（包括子目录）
        /// </summary>
        /// <param name="dirInfo">源文件夹路径</param>
        private int GetFilesCount(DirectoryInfo dirInfo)
        {
            int totalFile = 0;
            totalFile += dirInfo.GetFiles().Length;
            foreach (DirectoryInfo subdir in dirInfo.GetDirectories())
            {
                totalFile += GetFilesCount(subdir);
            }
            return totalFile;
        }

    }
}
