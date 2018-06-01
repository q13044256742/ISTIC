using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SQLite;

namespace 科技计划项目档案数据采集管理系统
{
    /// <summary>  
    /// SqlServer数据访问帮助类  
    /// </summary>  
    public class SQLiteBackupHelper
    {
        /// <summary>
        /// 数据连接字符串
        /// </summary>
        private string SQL_CONNECT;

        private static SQLiteConnection SQLiteConnection;

        public SQLiteBackupHelper(string SQL_CONNECT) => this.SQL_CONNECT = SQL_CONNECT;

        /// <summary>
        /// 获取SQLiteConnection连接
        /// </summary>
        private SQLiteConnection GetConnect()
        {
            if(SQLiteConnection == null)
                SQLiteConnection = new SQLiteConnection("Data Source=" + SQL_CONNECT);
            OpenConnect();
            return SQLiteConnection;
        }
        /// <summary>
        /// 打开数据库连接
        /// </summary> 
        public void OpenConnect()
        {
            if (SQLiteConnection != null && SQLiteConnection.State == ConnectionState.Closed)
            {
                SQLiteConnection.Open();
            }
        }
        
        /// <summary>
        /// 关闭数据库连接
        /// </summary>
        public void CloseConnect()
        {
            if(SQLiteConnection!=null && SQLiteConnection.State == ConnectionState.Open)
            {
                SQLiteConnection.Close();
            }
        }

        /// <summary>
        /// 根据指定的sql获取数据源
        /// </summary>
        /// <param name="querySql">sql语句</param>
        /// <returns>数据源的表</returns>
        public DataTable ExecuteQuery(string querySql)
        {
            SQLiteDataAdapter adapter = new SQLiteDataAdapter(querySql, GetConnect());
            DataTable table = new DataTable();
            adapter.Fill(table);
            CloseConnect();
            return table;
        }

        /// <summary>
        /// 查询唯一结果的SQL(count)
        /// </summary>
        public object ExecuteOnlyOneQuery(string querySql)
        {
            SQLiteCommand sqlCommand = new SQLiteCommand(querySql, GetConnect());
            object result = sqlCommand.ExecuteScalar();
            CloseConnect();
            return GetValue(result);
        }

        internal int ExecuteCountQuery(string queryString)
        {
            object obj = ExecuteOnlyOneQuery(queryString);
            if(obj != null && !string.IsNullOrEmpty(obj.ToString()))
                return Convert.ToInt32(obj);
            else
                return 0;
        }

        private object GetValue(object result)
        {
            if(result == null)
                return null;
            else
            {
                if(string.IsNullOrEmpty(result.ToString()))
                    return null;
                return result;
            }
        }

        /// <summary>
        /// 获取指定表的Reader
        /// </summary>
        public SQLiteDataReader ExecuteQueryWithReader(string querySql)
        {
            SQLiteCommand sqlCommand = new SQLiteCommand(querySql, GetConnect());
            SQLiteDataReader reader = sqlCommand.ExecuteReader();
            return reader;
        }

        /// <summary>
        /// 执行非查询操作（增删改）
        /// </summary>
        /// <param name="nonQuerySql">SQL语句</param>
        public void ExecuteNonQuery(string nonQuerySql)
        {
            SQLiteCommand sqlCommand = new SQLiteCommand(nonQuerySql, GetConnect());
            sqlCommand.ExecuteNonQuery();
            CloseConnect();
        }

        /// <summary>
        /// 查询带参数的
        /// </summary>
        /// <param name="insertSql">原始SQL语句</param>
        /// <param name="paramName">参数名称</param>
        /// <param name="paramType">参数类型</param>
        /// <param name="paramValue">参数值</param>
        internal void ExecuteNonQueryWithParam(string insertSql,string[] paramName, SqlDbType[] paramType, object[] paramValue)
        {
            SQLiteCommand sqlCommand = new SQLiteCommand(insertSql, GetConnect());
            for (int i = 0; i < paramName.Length; i++)
            {
                SqlParameter sqlParameter = new SqlParameter(paramName[i], paramType[i]);
                sqlParameter.Value = paramValue[i];
                sqlCommand.Parameters.Add(sqlParameter);
                sqlCommand.ExecuteNonQuery();
            }
            CloseConnect();
        }

        /// <summary>
        /// 执行指定列数返回结果的SQL语句
        /// </summary>
        /// <param name="querySql">SQL语句</param>
        /// <param name="columnSize">读取列数</param>
        public List<object[]> ExecuteColumnsQuery(string querySql, int columnSize)
        {
            List<object[]> list = new List<object[]>();
            SQLiteDataReader sqlDataReader = ExecuteQueryWithReader(querySql);
            while (sqlDataReader.Read())
            {
                object[] _obj = new object[columnSize];
                for (int i = 0; i < columnSize; i++)
                    _obj[i] = sqlDataReader.GetValue(i);
                list.Add(_obj);
            }
            sqlDataReader.Close();
            CloseConnect();
            return list;
        }

        /// <summary>
        /// 获取单行数据
        /// </summary>
        /// <param name="querySql">SQL语句</param>
        public object[] ExecuteRowsQuery(string querySql)
        {
            object[] _obj = null;
            SQLiteDataReader sqlDataReader = ExecuteQueryWithReader(querySql);
            if (sqlDataReader.Read())
            {
                _obj = new object[sqlDataReader.FieldCount];
                sqlDataReader.GetValues(_obj);
            }
            sqlDataReader.Close();
            CloseConnect();
            return _obj;
        }

        /// <summary>
        /// 获取单行数据
        /// </summary>
        public DataRow ExecuteSingleRowQuery(string querySql)
        {
            DataTable table = ExecuteQuery(querySql);
            return table.Rows.Count > 0 ? table.Rows[0] : null;
        }
    }
}
