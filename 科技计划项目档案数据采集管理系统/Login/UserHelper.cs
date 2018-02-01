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

    }
}
