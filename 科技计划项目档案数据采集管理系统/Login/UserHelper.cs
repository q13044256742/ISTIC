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
        /// 普通用户
        /// </summary>
        Normal = 1,
        /// <summary>
        /// 加工用户
        /// </summary>
        Worker = 2
    }
    public class UserHelper
    {
        private static User user;
        private static UserHelper userHelper;


        private UserHelper() { }
        public static UserHelper GetInstance()
        {
            if(userHelper == null)
                userHelper = new UserHelper();
            return userHelper;
        }

        public User User { get => user; set => user = value; }
        /// <summary>
        /// 获取当前登录用户的身份
        /// -1：未知
        /// 0：普通用户
        /// 1：加工管理
        /// </summary>
        public UserRole GetUserRole()
        {
            object obj = SqlHelper.ExecuteOnlyOneQuery($"SELECT dd_code FROM data_dictionary WHERE dd_id='{GetInstance().User.Role}'");
            if("dic_key_role_ordinary".Equals(obj))
                return UserRole.Normal;
            else if("dic_key_role_manager".Equals(obj))
                return UserRole.Worker;
            return UserRole.Error;
        }

        /// <summary>
        /// 根据用户ID获取用户姓名
        /// </summary>
        public string GetUserNameById(object userId)
        {
            object value = SqlHelper.ExecuteOnlyOneQuery($"SELECT real_name FROM user_list WHERE ul_id='{userId}'");
            return value == null ? string.Empty : value.ToString();
        }
    }
}
