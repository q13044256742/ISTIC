
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
            string querySql = "SELECT COUNT(*) FROM user_list WHERE login_name='" + user.LoginUserName + "' AND login_password='" + user.LoginPassword + "'";
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
            string querySql = "SELECT * FROM user_list  WHERE login_name='" + userName + "' AND login_password='" + passWord + "'";
            System.Data.SqlClient.SqlDataReader reader = SqlHelper.ExecuteQueryWithReader(querySql);
            while (reader.Read())
            {
                user.LoginUserName = reader["login_name"].ToString();
                user.LoginPassword = reader["login_password"].ToString();
                user.RealName = reader["real_name"].ToString();
            }
            reader.Close();
            SqlHelper.CloseConnect();
            return user;
        }
    }
}
