namespace 科技计划项目档案数据采集管理系统
{
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
        private int GetUserRole()
        {
            object obj = SqlHelper.ExecuteOnlyOneQuery($"SELECT dd_code FROM data_dictionary WHERE dd_id='{GetInstance().User.Role}'");
            if("dic_key_role_ordinary".Equals(obj))
                return 0;
            else if("dic_key_role_manager".Equals(obj))
                return 1;
            return -1;
        }
    }
}
