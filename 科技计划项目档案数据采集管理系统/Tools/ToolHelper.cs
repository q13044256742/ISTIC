using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Windows.Forms;

namespace 科技计划项目档案数据采集管理系统
{
    /// <summary>
    /// 工具类
    /// </summary>
    public class ToolHelper
    {
        /// <summary>
        /// 将对象转换成字符串形式，null作空字符串处理
        /// </summary>
        public static string GetValue(object value)
        {
            if(value == null)
                return string.Empty;
            else
                return value.ToString();
        }

        /// <summary>
        /// 将对象转换成字符串形式，null则返回指定默认字符串
        /// </summary>
        public static string GetValue(object value, string defaultValue)
        {
            if(value == null)
                return defaultValue;
            else
                return value.ToString();
        }

        /// <summary>
        /// 获取指定时间对象的指定格式化字符对象
        /// </summary>
        /// <param name="dateObject">时间对象</param>
        /// <param name="format">格式化类型</param>
        public static string GetDateValue(object dateObject, string format)
        {
            if(dateObject == null)
                return string.Empty;
            else
            {
                if(DateTime.TryParse(GetValue(dateObject), out DateTime result))
                    return result.ToString(format);
                return string.Empty;
            }
        }

        ///<summary>
        /// 实例化一个 ChineseLunisolarCalendar
        ///</summary>
        private static ChineseLunisolarCalendar ChineseCalendar = new ChineseLunisolarCalendar();

        /// <summary>
        /// 根据指定角色获取模块名称
        /// </summary>
        public static object[] GetModelByRole()
        {
            string querySQL =  "SELECT m_name FROM module " +
                 "LEFT JOIN data_dictionary ON dd_id = m_code " +
                $"WHERE dd_id = '{UserHelper.GetUser().Role}'";
            return SqlHelper.ExecuteSingleColumnQuery(querySQL);
        }

        ///<summary>
        /// 十天干
        ///</summary>
        private static string[] tg = { "甲", "乙", "丙", "丁", "戊", "己", "庚", "辛", "壬", "癸" };
        ///<summary>
        /// 十二地支
        ///</summary>
        private static string[] dz = { "子", "丑", "寅", "卯", "辰", "巳", "午", "未", "申", "酉", "戌", "亥" };
        ///<summary>
        /// 十二生肖
        ///</summary>
        private static string[] sx = { "鼠", "牛", "虎", "免", "龙", "蛇", "马", "羊", "猴", "鸡", "狗", "猪" };

        ///<summary>
        /// 返回农历天干地支年
        ///</summary>
        ///<param name="year">农历年</param>
        public static string GetLunisolarYear(int year)
        {
            if(year > 3)
            {
                int tgIndex = (year - 4) % 10;
                int dzIndex = (year - 4) % 12;

                return string.Concat(tg[tgIndex], dz[dzIndex], "[", sx[dzIndex], "]");
            }

            throw new ArgumentOutOfRangeException("无效的年份!");
        }

        ///<summary>
        /// 农历月
        ///</summary>
        private static string[] months = { "正", "二", "三", "四", "五", "六", "七", "八", "九", "十", "十一", "十二(腊)" };
        ///<summary>
        /// 农历日
        ///</summary>
        private static string[] days1 = { "初", "十", "廿", "三" };
        ///<summary>
        /// 农历日
        ///</summary>
        private static string[] days = { "一", "二", "三", "四", "五", "六", "七", "八", "九", "十" };

        ///<summary>
        /// 返回农历月
        ///</summary>
        ///<param name="month">月份</param>
        ///<return s></return s>
        public static string GetLunisolarMonth(int month)
        {
            if(month < 13 && month > 0)
            {
                return months[month - 1];
            }
            throw new ArgumentOutOfRangeException("无效的月份!");
        }

        ///<summary>
        /// 返回农历日
        ///</summary>
        ///<param name="day">天</param>
        ///<return s></return s>
        public static string GetLunisolarDay(int day)
        {
            if(day > 0 && day < 32)
            {
                if(day != 20 && day != 30)
                {
                    return string.Concat(days1[(day - 1) / 10], days[(day - 1) % 10]);
                }
                else
                {
                    return string.Concat(days[(day - 1) / 10], days1[1]);
                }
            }
            throw new ArgumentOutOfRangeException("无效的日!");
        }

        ///<summary>
        /// 根据公历获取农历日期
        ///</summary>
        ///<param name="datetime">公历日期</param>
        public static string GetChineseDateTime(DateTime datetime)
        {
            int year = ChineseCalendar.GetYear(datetime);
            int month = ChineseCalendar.GetMonth(datetime);
            int day = ChineseCalendar.GetDayOfMonth(datetime);
            //获取闰月， 0 则表示没有闰月
            int leapMonth = ChineseCalendar.GetLeapMonth(year);

            bool isleap = false;

            if(leapMonth > 0)
            {
                if(leapMonth == month)
                {
                    //闰月
                    isleap = true;
                    month--;
                }
                else if(month > leapMonth)
                {
                    month--;
                }
            }

            return string.Concat(GetLunisolarYear(year), "年", isleap ? "闰" : string.Empty, GetLunisolarMonth(month), "月", GetLunisolarDay(day));
        }

        public static int GetIntValue(object value)
        {
            string str = GetValue(value);
            if(!string.IsNullOrEmpty(str))
            {
                if(int.TryParse(str, out int result))
                    return result;
                else
                    return -1;
            }
            return -1;
        }

        /// <summary>
        /// 将指定数组对象按指定的字符分隔
        /// </summary>
        /// <param name="values">数组对象</param>
        /// <param name="v1">分隔符</param>
        /// <param name="v2">包围符号</param>
        internal static string GetFullStringBySplit(object[] values, string v1, string v2)
        {
            string result = string.Empty;
            if(values.Length == 0) return result;
            foreach(object value in values)
                result += $"{v2}{value}{v2}{v1}";
            return result.Substring(0, result.Length - 1);
        }

        /// <summary>
        /// 将对象转换成其整型
        /// </summary>
        /// <param name="value">object对象</param>
        /// <param name="defaultValue">转换失败时的默认值</param>
        public static int GetIntValue(object value, int defaultValue)
        {
            string str = GetValue(value);
            if(!string.IsNullOrEmpty(str))
            {
                if(int.TryParse(str, out int result))
                    return result;
                else
                    return defaultValue;
            }
            return defaultValue;
        }

        public static string GetFloatValue(string text, int length)
        {
            if(!string.IsNullOrEmpty(text))
            {
                if(float.TryParse(text, out float result))
                {
                    string format = $"0.{"0".PadLeft(length, '0')}";
                    return result.ToString(format);
                }
                else
                    return string.Empty;
            }
            return string.Empty;
        }

        public static string GetFullStringBySplit(string _str, string flag, string param)
        {
            string result = string.Empty;
            string[] strs = _str.Split(',');
            for(int i = 0; i < strs.Length; i++)
            {
                result += $"{param}{strs[i]}{param}{flag}";
            }
            return result.Length > 0 ? result.Substring(0, result.Length - 1) : string.Empty;
        }

        /// <summary>
        /// 在指定树下获取指定节点
        /// </summary>
        /// <param name="treeNode">树</param>
        /// <param name="nodeName">节点名称（name）</param>
        public static TreeNode GetTreeNodeByName(TreeNode treeNode, object nodeName)
        {
            if(treeNode.Name.Equals(nodeName))
                return treeNode;
            foreach(TreeNode node in treeNode.Nodes)
            {
                return GetTreeNodeByName(node, nodeName);
            }
            return null;
        }

        /// <summary>
        /// 获取时间对象的DATE格式对象，若转换失败则返回当前时间
        /// </summary>
        internal static DateTime GetDateValue(object value)
        {
            string dateStr = ToolHelper.GetValue(value);
            if (DateTime.TryParse(dateStr, out DateTime date))
                return date;
            return new DateTime();
        }

        /// <summary>
        /// 将用指定分隔符切割后的字符串用指定间隔符和包围符重新组合
        /// </summary>
        /// <param name="oldString">原字符串</param>
        /// <param name="newFlag">间隔符</param>
        /// <param name="newParam">包围符</param>
        /// <param name="oldSplitTag">切割符</param>
        internal static string GetFullStringBySplit(string oldString, char oldSplitTag, string newFlag, string newParam)
        {
            string result = string.Empty;
            string[] strs = oldString.Split(oldSplitTag);
            for (int i = 0; i < strs.Length; i++)
            {
                if (!string.IsNullOrEmpty(strs[i]))
                    result += $"{newParam}{strs[i]}{newParam}{newFlag}";
            }
            return result.Length > 0 ? result.Substring(0, result.Length - 1) : string.Empty;
        }
    }

}
