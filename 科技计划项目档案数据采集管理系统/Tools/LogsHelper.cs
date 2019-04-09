using System;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace 科技计划项目档案数据采集管理系统
{
    /// <summary>
    /// 加工记录实体对象
    /// </summary>
    public class Work_Log
    {
        /// <summary>
        /// 主键
        /// </summary>
        public string id;
        /// <summary>
        /// 加工类型
        /// <para>1:项目/课题</para>
        /// <para>2:课题/子课题</para>
        /// <para>3:文件</para>
        /// <para>4:卷盒</para>
        /// <para>5:返工</para>
        /// <para>6:被返工</para>
        /// </summary>
        public int type;
        /// <summary>
        /// 批次编号
        /// </summary>
        public string batchCode;
        /// <summary>
        /// 数量
        /// </summary>
        public int amount;
        /// <summary>
        /// 记录日期
        /// </summary>
        public DateTime date;
        /// <summary>
        /// 用户ID
        /// </summary>
        public string userid;
    }

    /// <summary>
    /// 加工记录枚举列表
    /// </summary>
    public enum WorkLogType{
        Default,
        /// <summary>
        /// 项目/课题
        /// </summary>
        Project_Topic,
        /// <summary>
        /// 课题/子课题
        /// </summary>
        Topic_Subject,
        /// <summary>
        /// 文件
        /// </summary>
        File,
        /// <summary>
        /// 卷盒
        /// </summary>
        Box,
        /// <summary>
        /// 返工
        /// </summary>
        BackWork,
        /// <summary>
        /// 被返工
        /// </summary>
        BeBackWork
    }
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

        /// <summary>
        /// 添加工作记录
        /// </summary>
        /// <param name="logType">记录类别</param>
        /// <param name="amount">数量</param>
        /// <param name="batchCode">批次编号</param>
        /// <param name="segment">环节<para>1：加工</para><para>2：质检</para></param>
        public static void AddWorkLog(WorkLogType logType, int amount, object batchCode, int segment, object objectId)
        {
            if (string.IsNullOrEmpty(ToolHelper.GetValue(batchCode))) return;
            string date = ToolHelper.GetDateValue(DateTime.Now, "yyyy-MM-dd");
            string userid = UserHelper.GetUser().UserKey;
            int type = (int)logType;

            string existQuery = $"SELECT wl_id FROM work_log WHERE wl_user_id='{userid}' AND wl_datetime='{date}' AND wl_batch_code='{batchCode}' AND wl_type='{type}' AND wl_segment={segment}";
            object result = SqlHelper.ExecuteOnlyOneQuery(existQuery);
            if (result != null)
            {
                string updateSQL = $"UPDATE work_log SET wl_amount += {amount} WHERE wl_id='{result}'";
                SqlHelper.ExecuteNonQuery(updateSQL);
            }
            else
            {
                //新增记录
                string insertSQL = "INSERT INTO work_log(wl_id, wl_type, wl_batch_code, wl_amount, wl_datetime, wl_user_id, wl_segment, wl_object_id) VALUES" +
                    $"('{Guid.NewGuid().ToString()}', '{type}', '{batchCode}', '{amount}', '{date}', '{userid}', {segment}, '{objectId}')";
                SqlHelper.ExecuteNonQuery(insertSQL);
            }
        }

        public static void AddWorkLog(WorkLogType logType, int amount, object batchCode, int segment, object objectId, object userid)
        {
            if (string.IsNullOrEmpty(ToolHelper.GetValue(batchCode))) return;
            string date = ToolHelper.GetDateValue(DateTime.Now, "yyyy-MM-dd");
            int type = (int)logType;

            string existQuery = $"SELECT wl_id FROM work_log WHERE wl_user_id='{userid}' AND wl_datetime='{date}' AND wl_batch_code='{batchCode}' AND wl_type='{type}' AND wl_segment={segment}";
            object result = SqlHelper.ExecuteOnlyOneQuery(existQuery);
            if (result != null)
            {
                string updateSQL = $"UPDATE work_log SET wl_amount += {amount} WHERE wl_id='{result}'";
                SqlHelper.ExecuteNonQuery(updateSQL);
            }
            else
            {
                //新增记录
                string insertSQL = "INSERT INTO work_log(wl_id, wl_type, wl_batch_code, wl_amount, wl_datetime, wl_user_id, wl_segment, wl_object_id) VALUES" +
                    $"('{Guid.NewGuid().ToString()}', '{type}', '{batchCode}', '{amount}', '{date}', '{userid}', {segment}, '{objectId}')";
                SqlHelper.ExecuteNonQuery(insertSQL);
            }
        }
    }
}
