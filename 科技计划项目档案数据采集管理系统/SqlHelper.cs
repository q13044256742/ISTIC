﻿using System;
using System.Collections.Generic;
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
        static string IPAddress = "172.28.28.2";//OperateIniFile.GetInstance().ReadIniData("SQLServer", "IPAddress", "172.24.139.2");
        static string Username = "sa";// OperateIniFile.GetInstance().ReadIniData("SQLServer", "Username", "sa");
        static string Password = "123456";// OperateIniFile.GetInstance().ReadIniData("SQLServer", "Password", "123456");
        private static string SQL_CONNECT = $"Data Source={IPAddress};Initial Catalog=ISTIC;Persist Security Info=True;User ID={Username};Password={Password}";
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
        /// 查询唯一结果的SQL(count)
        /// </summary>
        public static object ExecuteOnlyOneQuery(string querySql)
        {
            SqlCommand sqlCommand = new SqlCommand(querySql, GetConnect());
            object result = sqlCommand.ExecuteScalar();
            CloseConnect();
            return result;
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

        /// <summary>
        /// 查询带参数的
        /// </summary>
        /// <param name="insertSql">原始SQL语句</param>
        /// <param name="paramName">参数名称</param>
        /// <param name="paramType">参数类型</param>
        /// <param name="paramValue">参数值</param>
        internal static void ExecuteNonQueryWithParam(string insertSql,string[] paramName, SqlDbType[] paramType, object[] paramValue)
        {
            SqlCommand sqlCommand = new SqlCommand(insertSql, GetConnect());
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
        public static List<object[]> ExecuteColumnsQuery(string querySql, int columnSize)
        {
            List<object[]> list = new List<object[]>();
            SqlDataReader sqlDataReader = ExecuteQueryWithReader(querySql);
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
        public static object[] ExecuteRowsQuery(string querySql)
        {
            object[] _obj = null;
            SqlDataReader sqlDataReader = ExecuteQueryWithReader(querySql);
            if (sqlDataReader.Read())
            {
                _obj = new object[sqlDataReader.FieldCount];
                sqlDataReader.GetValues(_obj);
            }
            sqlDataReader.Close();
            CloseConnect();
            return _obj;
        }
    }
}
