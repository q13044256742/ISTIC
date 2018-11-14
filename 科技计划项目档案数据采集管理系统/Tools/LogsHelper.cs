using System;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace 科技计划项目档案数据采集管理系统
{
    class LogsHelper
    {
        /// <summary>
        /// 添加错误日志到日期记录本
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="message">内容</param>
        public static void AddErrorLogs(string title, string message)
        {
            string errorLogs = Application.StartupPath + "\\errorLogs.txt";
            string context = $"{title}\t" + DateTime.Now.ToString("yyyy-MM-dd HH:mm") + "\r\n" + message + "\r\n\n";
            File.AppendAllText(errorLogs, context, Encoding.UTF8);
        }
    }
}
