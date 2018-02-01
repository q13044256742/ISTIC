namespace 科技计划项目档案数据采集管理系统
{
    public class User
    {
        private string userKey;
        private string loginUserName;
        private string loginPassword;
        private string realName;
        private object remark;

        public User() { }
        public User(string loginUserName, string loginPassword)
        {
            this.loginUserName = loginUserName;
            this.loginPassword = loginPassword;
        }

        public string LoginUserName { get => loginUserName; set => loginUserName = value; }
        public string LoginPassword { get => loginPassword; set => loginPassword = value; }
        public string RealName { get => realName; set => realName = value; }
        public object Remark { get => remark; set => remark = value; }
        public string UserKey {
            get => userKey;
            set => userKey = value;
        }
    }
}
