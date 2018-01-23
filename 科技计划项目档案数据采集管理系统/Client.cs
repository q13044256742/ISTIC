using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using KyoFileEntity;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.InteropServices;

namespace 科技计划项目档案数据采集管理系统
{
    public partial class Client : Form
    {
        Socket _socket = null;
        public Client()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            foreach (string item in listBox1.Items)
            {
                string filePath = item;

                FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                byte[] _fileByte = new byte[fileStream.Length];
                fileStream.Read(_fileByte, 0, _fileByte.Length);

                FileEntity fileEntity = new FileEntity
                {
                    KfeId = Guid.NewGuid().ToString("N"),
                    KfeName = Path.GetFileName(filePath),
                    KfeFileSize = Convert.ToInt32((new FileInfo(filePath).Length / 1024)),
                    KfeFormat = Path.GetExtension(filePath),
                    KfeFilePath = filePath,
                    KfeByte = _fileByte
                };

                byte[] _byte = GetByteWithLength(SerializeHelper.SerializeToBinary(fileEntity));
                _socket.Send(_byte);
            }
        }

        /// <summary>
        /// 将指定字节数组长度字节数组追加到数组头部
        /// 【固定占4个长度】
        /// </summary>
        /// <param name="_byte">指定的字节数组</param>
        /// <returns>拼装后的数组</returns>
        byte[] GetByteWithLength(byte[] _byte)
        {
            byte[] lengthByte = BitConverter.GetBytes(_byte.Length);
            byte[] resultByte = new byte[_byte.Length + lengthByte.Length];
            Buffer.BlockCopy(lengthByte, 0, resultByte, 0, lengthByte.Length);
            Buffer.BlockCopy(_byte, 0, resultByte, lengthByte.Length, _byte.Length);
            return resultByte;
        }

        private void ReceiveMessage(Socket socket)
        {
            new Thread(delegate ()
            {
                while (true)
                {
                    try
                    {
                        byte[] _byte = new byte[1024 * 1024];
                        int length = socket.Receive(_byte);
                        string msg = Encoding.UTF8.GetString(_byte, 0, length);
                        ShowReceiveMsg("Server", msg);
                    }catch(Exception e)
                    {
                        ShowReceiveMsg("Server", e.Message);
                        Thread.CurrentThread.Abort();
                    }
                }
            }).Start();
        }

        /// <summary>
        /// 打印接受到的信息
        /// </summary>
        /// <param name="talker">信息发出者</param>
        /// <param name="msg">信息文本</param>
        private void ShowReceiveMsg(string talker, string msg)
        {
            rtxt_ShowMsg.AppendText(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + $"\t{talker}:\t{msg}\r\n");
        }

        private void Client_Load(object sender, EventArgs e)
        {
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string ip = "192.168.4.114";//X

            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            _socket.Connect(new IPEndPoint(IPAddress.Parse(ip), 8080));
            ShowReceiveMsg("Client", "连接服务器成功");

            ReceiveMessage(_socket);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Multiselect = true;
            if(fileDialog.ShowDialog() == DialogResult.OK)
            {
                listBox1.Items.AddRange(fileDialog.FileNames);
            }
        }
    }
}
