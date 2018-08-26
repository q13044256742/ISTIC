using System;

namespace 科技计划项目档案数据采集管理系统
{
    /// <summary>
    /// 用户角色
    /// </summary>
    public enum UserRole
    {
        /// <summary>
        /// 未知用户
        /// </summary>
        Error = 0,
        /// <summary>
        /// 著录加工
        /// </summary>
        Worker = 1,
        /// <summary>
        /// 档案质检
        /// </summary>
        Qualityer = 2,
        /// <summary>
        /// 加工质检管理员
        /// </summary>
        W_Q_Manager = 3,
        /// <summary>
        /// 档案管理员
        /// </summary>
        DocManager = 4,
        /// <summary>
        /// 普通用户
        /// </summary>
        Ordinary = 5,
    }
    public class UserHelper
    {
        private static User user;
        public static User GetUser()
        {
            if(user == null)
               throw new Exception("未检测到登录用户。");
            return user;
        }

        /// <summary>
        /// 获取当前登录用户的身份
        /// </summary>
        public static UserRole GetUserRole()
        {
            object value = SqlHelper.ExecuteOnlyOneQuery($"SELECT dd_code FROM data_dictionary WHERE dd_id='{GetUser().Role}'");
            if("dic_key_role_worker".Equals(value))
                return UserRole.Worker;
            else if("dic_key_role_valider".Equals(value))
                return UserRole.Qualityer;
            else if("dic_key_role_manager".Equals(value))
                return UserRole.W_Q_Manager;
            else if("dic_key_role_administrator".Equals(value))
                return UserRole.DocManager;
            else if("dic_key_role_ordinary".Equals(value))
                return UserRole.Ordinary;
            return UserRole.Error;
        }

        /// <summary>
        /// 记录登录/退出日志
        /// </summary>
        /// <param name="isLogin">登录或退出</param>
        public static void SetLogin(bool isLogin)
        {
            if(isLogin)
            {
                string insertSQL = "INSERT INTO sys_login_log VALUES" +
                    $"('{GetUser().LoginKey}', '{GetUser().UserKey}', '{DateTime.Now}', '', '{GetIPAddress()}')";
                SqlHelper.ExecuteNonQuery(insertSQL);
            }
            else
            {
                string updateSQL = $"UPDATE sys_login_log SET sll_offline_date='{DateTime.Now}' WHERE sll_id='{GetUser().LoginKey}';";
                SqlHelper.ExecuteNonQuery(updateSQL);
            }
        }

        /// <summary>
        /// 获取当前主机的IPv4地址
        /// </summary>
        /// <returns></returns>
        private static string GetIPAddress()
        {
            try
            {
                string HostName = System.Net.Dns.GetHostName(); //得到主机名
                System.Net.IPHostEntry IpEntry = System.Net.Dns.GetHostEntry(HostName);
                for(int i = 0; i < IpEntry.AddressList.Length; i++)
                {
                    //从IP地址列表中筛选出IPv4类型的IP地址
                    //AddressFamily.InterNetwork表示此IP为IPv4,
                    //AddressFamily.InterNetworkV6表示此地址为IPv6类型
                    if(IpEntry.AddressList[i].AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                    {
                        return IpEntry.AddressList[i].ToString();
                    }
                }
                return string.Empty;
            }
            catch(Exception ex)
            {
                return ex.Message;
            }

        }

        public static void SetUser(User _user)
        {
            user = new User
            {
                LoginUserName = _user.LoginUserName,
                LoginPassword = _user.LoginPassword,
                RealName = _user.RealName,
                UserKey = _user.UserKey,
                UnitName = _user.UnitName,
                Role = _user.Role,
                Group = _user.Group,
                LoginKey = Guid.NewGuid().ToString()
            };
        }

        /// <summary>
        /// 获取当前登录用户的身份
        /// </summary>
        public static string GetUserRoleName()
        {
            if(UserHelper.GetUserRole() == UserRole.Worker)
                return "加工人员";
            else if(UserHelper.GetUserRole() == UserRole.Qualityer)
                return "质检人员";
            else if(UserHelper.GetUserRole() == UserRole.W_Q_Manager)
                return "管理员(线上)";
            else if(UserHelper.GetUserRole() == UserRole.DocManager)
                return "档案管理员";
            else if(UserHelper.GetUserRole() == UserRole.Ordinary)
                return "普通用户";
            return "未知身份";
        }

        /// <summary>
        /// 根据用户ID获取用户姓名
        /// </summary>
        public static string GetUserNameById(object userId)
        {
            object value = SqlHelper.ExecuteOnlyOneQuery($"SELECT real_name FROM user_list WHERE ul_id='{userId}'");
            return value == null ? string.Empty : value.ToString();
        }
    }
}
