using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace 科技计划项目档案数据采集管理系统
{
    /// <summary>
    /// 操作ini格式的配置文件
    /// </summary>
    class OperateIniFile
    {
        private static OperateIniFile _operateIniFile;
        private OperateIniFile() { }
        public static OperateIniFile GetInstance()
        {
            if (_operateIniFile == null)
                _operateIniFile = new OperateIniFile();
            return _operateIniFile;
        }

        /// <summary>
        /// 默认配置文件路径
        /// </summary>
        private static string IniFilePath = Application.StartupPath + "\\Datas\\DataSources.ini";

        [DllImport("kernel32")]//返回0表示失败，非0为成功
        private static extern long WritePrivateProfileString(string section, string key,
           string val, string filePath);

        [DllImport("kernel32")]//返回取得字符串缓冲区的长度
        private static extern long GetPrivateProfileString(string section, string key,
            string def, StringBuilder retVal, int size, string filePath);

        /// <summary>
        /// 读取INI配置文件,根据指定的关键字获取对应的值
        /// </summary>
        /// <param name="Section">文本所在区域</param>
        /// <param name="Key">关键字</param>
        /// <param name="NoText">默认值</param>
        /// <param name="iniFilePath">配置文件路径</param>
        /// <returns>关键字对应的值</returns>
        public string ReadIniData(string Section, string Key, string NoText)
        {
            if (File.Exists(IniFilePath))
            {
                StringBuilder temp = new StringBuilder(1024);
                GetPrivateProfileString(Section, Key, NoText, temp, 1024, IniFilePath);
                return temp.ToString();
            }
            else
            {
                return String.Empty;
            }
        }

        public bool WriteIniData(string Section, string Key, string Value)
        {
            if (File.Exists(IniFilePath))
            {
                long OpStation = WritePrivateProfileString(Section, Key, Value, IniFilePath);
                if (OpStation == 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return false;
            }
        }
    }
}
