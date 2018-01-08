namespace 科技计划项目档案数据采集管理系统
{
    public class User
    {
        private string loginUserName;
        private string loginPassword;
        private string realName;
        private string remark;

        public User() { }
        public User(string loginUserName, string loginPassword)
        {
            this.loginUserName = loginUserName;
            this.loginPassword = loginPassword;
        }

        public string LoginUserName { get => loginUserName; set => loginUserName = value; }
        public string LoginPassword { get => loginPassword; set => loginPassword = value; }
        public string RealName { get => realName; set => realName = value; }
        public string Remark { get => remark; set => remark = value; }
    }
}
