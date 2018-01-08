using System;
using System.Data;
using System.Data.SqlClient;

namespace 科技计划项目档案数据采集管理系统
{
    /// <summary>  
    /// SqlServer数据访问帮助类  
    /// </summary>  
    public class SqlHelper
    {
        /// <summary>
        /// 数据连接字符串
        /// </summary>
        private static string SQL_CONNECT = "Data Source=KYO;Initial Catalog=ISTIC;Persist Security Info=True;User ID=sa;Password=123456";

        private static SqlConnection sqlConnection;

        /// <summary>
        /// 获取SqlConnection连接
        /// </summary>
        private static SqlConnection GetConnect()
        {
            if (sqlConnection == null)
                sqlConnection = new SqlConnection(SQL_CONNECT);
            OpenConnect();
            return sqlConnection;
        }
        
        /// <summary>
        /// 打开数据库连接
        /// </summary>
        public static void OpenConnect()
        {
            if (sqlConnection != null && sqlConnection.State == ConnectionState.Closed)
            {
                sqlConnection.Open();
            }
        }
        
        /// <summary>
        /// 关闭数据库连接
        /// </summary>
        public static void CloseConnect()
        {
            if(sqlConnection!=null && sqlConnection.State == ConnectionState.Open)
            {
                sqlConnection.Close();
            }
        }

        /// <summary>
        /// 根据指定的sql获取数据源
        /// </summary>
        /// <param name="querySql">sql语句</param>
        /// <returns>数据源的表</returns>
        public static DataTable ExecuteQuery(string querySql)
        {
            SqlDataAdapter adapter = new SqlDataAdapter(querySql,GetConnect());
            DataTable table = new DataTable();
            adapter.Fill(table);
            CloseConnect();
            return table;
        }

        /// <summary>
        /// 查询统计类型的SQL(count)
        /// </summary>
        public static int ExecuteCountQuery(string querySql)
        {
            SqlCommand sqlCommand = new SqlCommand(querySql, GetConnect());
            string result = sqlCommand.ExecuteScalar().ToString();
            CloseConnect();
            return Convert.ToInt32(result);
        }

        /// <summary>
        /// 获取指定表的Reader
        /// </summary>
        /// <param name="querySql"></param>
        /// <returns></returns>
        public static SqlDataReader ExecuteQueryWithReader(string querySql)
        {
            SqlCommand sqlCommand = new SqlCommand(querySql, GetConnect());
            SqlDataReader reader = sqlCommand.ExecuteReader();
            return reader;
        }

        /// <summary>
        /// 执行非查询操作（增删改）
        /// </summary>
        /// <param name="nonQuerySql">SQL语句</param>
        public static void ExecuteNonQuery(string nonQuerySql)
        {
            SqlCommand sqlCommand = new SqlCommand(nonQuerySql, GetConnect());
            sqlCommand.ExecuteNonQuery();
            CloseConnect();
        }
    }
}
