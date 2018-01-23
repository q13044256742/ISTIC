using KyoFileEntity;
using System;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace 科技计划项目档案数据采集管理系统
{
    class FilesBackupsHelper
    {
        private static FilesBackupsHelper _filesBackupsHelper;
        private FilesBackupsHelper() { }
        public static FilesBackupsHelper GetInstance()
        {
            if (_filesBackupsHelper == null)
                _filesBackupsHelper = new FilesBackupsHelper();
            return _filesBackupsHelper;
        }

        /// <summary>
        /// 获取服务器连接套接字
        /// </summary>
        /// <param name="serverIP">服务器IP地址</param>
        /// <param name="serverPort">服务器端口号</param>
        public Socket GetServerConnect()
        {
            try
            {
                IPAddress serverIP = IPAddress.Parse(OperateIniFile.GetInstance().ReadIniData("Socket", "IPAddress", string.Empty));
                int serverPort = Convert.ToInt32(OperateIniFile.GetInstance().ReadIniData("Socket", "port", string.Empty));
                Socket _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                _socket.Connect(new IPEndPoint(serverIP, serverPort));
                return _socket;
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.Message, "服务器连接失败", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return null;
            }
        }

        public FilesBackupsHelper DeleteFile(object rootPath)
        {
            string deleteSql = $"DELETE FROM files WHERE fpath LIKE '%{rootPath}%'";
            SqlHelper.ExecuteNonQuery(deleteSql);
            return GetInstance();
        }

        /// <summary>
        /// 备份指定文件夹下文件到数据库
        /// </summary>
        /// <param name="socket">服务器套接字</param>
        /// <param name="saveDirPath">源文件夹路径</param>
        public void UploadFile(object saveDirPath, Action<string> SetTipMsg)
        {
            string[] files = Directory.GetFiles(saveDirPath.ToString());
            foreach (string file in files)
            {
                FileEntity fileEntity = new FileEntity
                {
                    KfeId = Guid.NewGuid().ToString(),
                    KfeName = Path.GetFileName(file),
                    KfeFilePath = file,
                    KfeFormat = Path.GetExtension(file),
                    KfeFileSize = (int)(new FileInfo(file).Length / 1024),
                    KfeByte = GetFileByte(file)
                };
                SaveFile(fileEntity);
                SetTipMsg($">> {fileEntity.KfeName} 已成功添加。");
            }

            string[] directorys = Directory.GetDirectories(saveDirPath.ToString());
            foreach (string directory in directorys)
            {
                UploadFile(directory, SetTipMsg);
            }
        }

        /// <summary>
        /// 发送解析指令
        /// </summary>
        public void AnalyticalFile(object rootPath)
        {
            Socket socket = GetServerConnect();
            if (socket != null)
            {
                byte[] _byte = System.Text.Encoding.UTF8.GetBytes($"PARSE_FILE#{rootPath}");
                socket.Send(_byte);
                socket.Close();
            }
        }

        /// <summary>
        /// 上传至服务器
        /// </summary>
        /// <param name="fileEntity">文件实例</param>
        private void SaveFile(FileEntity fileEntity)
        {
            string insertSql = $"INSERT INTO files VALUES('{Guid.NewGuid().ToString()}','{fileEntity.KfeName}','{fileEntity.KfeFilePath}','{fileEntity.KfeFormat}','{fileEntity.KfeFileSize}',@fileByte)";
            SqlHelper.ExecuteNonQueryWithParam(insertSql, new string[] { "@fileByte" }, new System.Data.SqlDbType[] { System.Data.SqlDbType.VarBinary }, new object[] { fileEntity.KfeByte });
        }

        /// <summary>
        /// 将指定文件转换成字节码
        /// </summary>
        /// <param name="file">源文件路径</param>
        private byte[] GetFileByte(string filePath)
        {
            FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            byte[] _fileByte = new byte[fileStream.Length];
            fileStream.Read(_fileByte, 0, _fileByte.Length);
            fileStream.Close();
            return _fileByte;
        }

    }
}
