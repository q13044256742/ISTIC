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
            return UserRole.Error;
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
                Group = _user.Group
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
