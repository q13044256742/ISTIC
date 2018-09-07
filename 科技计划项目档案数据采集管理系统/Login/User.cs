namespace 科技计划项目档案数据采集管理系统
{
    public class User
    {
        private string userKey;
        private string loginUserName;
        private string loginPassword;
        private string realName;
        private object remark;
        private string role;
        private string unitName;
        private string unitCode;
        private object[] group;
        private string loginKey;

        public User() { }
        public User(string loginUserName, string loginPassword)
        {
            this.loginUserName = loginUserName;
            this.loginPassword = loginPassword;
        }
        /// <summary>
        /// 登录名
        /// </summary>
        public string LoginUserName { get => loginUserName; set => loginUserName = value; }
        /// <summary>
        /// 登录密码
        /// </summary>
        public string LoginPassword { get => loginPassword; set => loginPassword = value; }
        /// <summary>
        /// 真实姓名
        /// </summary>
        public string RealName { get => realName; set => realName = value; }
        /// <summary>
        /// 备注
        /// </summary>
        public object Remark { get => remark; set => remark = value; }
        /// <summary>
        /// 用户主键
        /// </summary>
        public string UserKey
        {
            get => userKey;
            set => userKey = value;
        }
        /// <summary>
        /// 用户所属组
        /// </summary>
        public object[] Group { get => group; set => group = value; }
        /// <summary>
        /// 所属角色主键
        /// </summary>
        public string Role { get => role; set => role = value; }
        /// <summary>
        /// 单位名称
        /// </summary>
        public string UnitName { get => unitName; set => unitName = value; }
        /// <summary>
        /// 单位编码
        /// </summary>
        public string UnitCode { get => unitCode; set => unitCode = value; }
        /// <summary>
        /// 本次登录key（用于记录登录日志）
        /// </summary>
        public string LoginKey { get => loginKey; set => loginKey = value; }
    }
}
