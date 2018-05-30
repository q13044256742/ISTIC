
using System;
using System.Data;

namespace 科技计划项目档案数据采集管理系统
{
    class UserLogin
    {
        /// <summary>
        /// 查询指定的用户是否存在
        /// </summary>
        /// <param name="user">待查询的用户（包含用户名和密码）</param>
        /// <returns></returns>
        public bool IsExist(User user)
        {
            string querySql = $"SELECT COUNT(*) FROM user_list WHERE login_name='{user.LoginUserName}' AND login_password='{user.LoginPassword}'";
            int i = Convert.ToInt32(SqlHelper.ExecuteOnlyOneQuery(querySql));
            return i == 0 ? false : true;
        }

        /// <summary>
        /// 根据用户名和密码获取指定的User对象
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="passWord">密码</param>
        /// <returns>User对象</returns>
        public User GetUser(string userName, string passWord)
        {
            User user = new User();
            string querySql = $"SELECT * FROM user_list WHERE login_name='{userName}' AND login_password='{passWord}'";
            DataRow row = SqlHelper.ExecuteSingleRowQuery(querySql);
            if(row != null)
            {
                user.LoginUserName = GetValue(row["login_name"]);
                user.LoginPassword = GetValue(row["login_password"]);
                user.RealName = GetValue(row["real_name"]);
                user.UserKey = GetValue(row["ul_id"]);
                user.UnitName = GetValue(row["belong_unit"]);
                user.Role = GetValue(row["role_id"]);
                user.Group = GetGroupId(row["belong_user_group_id"]);
            }
            return user;
        }

        private string GetValue(object value) => value == null ? string.Empty : value.ToString();

        private string[] GetGroupId(object ids) => ids == null ? null : ids.ToString().Split(',');
    }
}
