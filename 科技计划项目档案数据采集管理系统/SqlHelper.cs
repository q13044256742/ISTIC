using System; 
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
        /// 数据库类型
        /// <para>SQLServer_Local：本地</para>
        /// <para>SQLServer_Server：服务端</para>
        /// </summary>
        static readonly string SERVER_TYPE = "SQLServer_Local";
        static readonly string IPAddress = OperateIniFile.GetInstance().ReadIniData(SERVER_TYPE, "IPAddress", null);
        static readonly string Username = OperateIniFile.GetInstance().ReadIniData(SERVER_TYPE, "Username", null);
        static readonly string Password = OperateIniFile.GetInstance().ReadIniData(SERVER_TYPE, "Password", null);
        static readonly string CatalogName = OperateIniFile.GetInstance().ReadIniData(SERVER_TYPE, "CatalogName", null);
        /// <summary>
        /// 数据连接字符串
        /// </summary>
        private static string SQL_CONNECT = $"Data Source={IPAddress};Initial Catalog={CatalogName};Persist Security Info=True;MultipleActiveResultSets=true;User ID={Username};Password={Password}";

        private static SqlConnection sqlConnection; 


        /// <summary>
        /// 获取已经打开的SqlConnection连接
        /// </summary>
        public static SqlConnection GetConnect()
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
            SqlDataAdapter adapter = new SqlDataAdapter(querySql, GetConnect());
            DataTable table = new DataTable();
            adapter.Fill(table);
            CloseConnect();
            return table;
        }

        public static DataTable ExecuteQuery(string querySql, string tableName)
        {
            DataTable dataTable = ExecuteQuery(querySql);
            dataTable.TableName = tableName;
            return dataTable;
        }

        /// <summary>
        /// 查询唯一结果的SQL(count)
        /// </summary>
        public static object ExecuteOnlyOneQuery(string querySql)
        {
            SqlCommand sqlCommand = new SqlCommand(querySql, GetConnect());
            object result = sqlCommand.ExecuteScalar();
            CloseConnect();
            if(result != null)
                if(string.IsNullOrEmpty(result.ToString()))
                    result = null;
            return result;
        }

        public static DataTable GetProvinceList(string fieldName)
        {
            string key = "dic_xzqy_province";
            string querySQL = $"SELECT {fieldName} FROM data_dictionary WHERE dd_pId=" +
               $"(SELECT dd_id FROM data_dictionary WHERE dd_code = '{key}') " +
                "ORDER BY dd_sort";
            return ExecuteQuery(querySQL);
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
            if(!string.IsNullOrEmpty(nonQuerySql))
            {
                using(SqlCommand sqlCommand = new SqlCommand(nonQuerySql, GetConnect()))
                {
                    SqlTransaction sqlTransaction = GetConnect().BeginTransaction();
                    sqlCommand.Transaction = sqlTransaction;
                    try
                    {
                        sqlCommand.ExecuteNonQuery();
                        sqlTransaction.Commit();
                    }
                    catch(Exception e)
                    {
                        sqlTransaction.Rollback();
                        DevExpress.XtraEditors.XtraMessageBox.Show(e.Message, "数据出错(详情查看错误日志)");
                        LogsHelper.AddErrorLogs("执行SQL语句失败", $"Sql语句为>> {nonQuerySql}");
                    }
                    finally
                    {
                        CloseConnect();
                    }
                }
            }
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
            SqlCommand sqlCommand = new SqlCommand(insertSql, GetConnect( ));
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

        /// <summary>
        /// 根据单位ID获取单位名称
        /// </summary>
        /// <param name="companyId">来源单位ID</param>
        /// <returns></returns>
        public static string GetCompanysNameById(object companyId)
        {
            object obj = SqlHelper.ExecuteOnlyOneQuery($"SELECT dd_name FROM data_dictionary WHERE dd_id='{companyId}'");
            return obj == null ? string.Empty : obj.ToString();
        }
        /// <summary>
        /// 获取来源单位列表
        /// </summary>
        public static DataTable GetCompanyList()
        {
            string key = "dic_key_company_source";
            string querySql = $"SELECT * FROM data_dictionary WHERE dd_pId = (SELECT dd_id FROM data_dictionary WHERE dd_code='{key}') ORDER BY dd_sort";
            return ExecuteQuery(querySql);
        }
        /// <summary>
        /// 通过类型获取对应来源单位CODE和NAME
        /// </summary>
        /// <param name="id">给定ID</param>
        /// <param name="workType">给定类型</param>
        /// <returns>1：单位Code；2：单位名称</returns>
        public static object[] GetCompanyByParam(object id, object workType)
        {
            WorkType type = (WorkType)Convert.ToInt32(workType);
            string querySql = null;
            if(type == WorkType.PaperWork)
            {
                object comid = SqlHelper.ExecuteOnlyOneQuery($"SELECT com_id FROM transfer_registration_pc WHERE trp_id='{id}'");
                querySql = $"SELECT dd_code, dd_name FROM data_dictionary WHERE dd_id='{comid}'";
            }
            else if(type== WorkType.CDWork)
            {
                object trpid = SqlHelper.ExecuteOnlyOneQuery($"SELECT trp_id FROM transfer_registraion_cd WHERE trc_id='{id}'");
                object comid = SqlHelper.ExecuteOnlyOneQuery($"SELECT com_id FROM transfer_registration_pc WHERE trp_id='{trpid}'");
                querySql = $"SELECT dd_code, dd_name FROM data_dictionary WHERE dd_id='{comid}'";

            }
            else if(type == WorkType.ProjectWork)
            {
                object trcid = SqlHelper.ExecuteOnlyOneQuery($"SELECT trc_id FROM project_info WHERE pi_id= '{id}'") ?? SqlHelper.ExecuteOnlyOneQuery($"SELECT trc_id FROM topic_info WHERE ti_id='{id}'");
                object trpid = SqlHelper.ExecuteOnlyOneQuery($"SELECT trp_id FROM transfer_registraion_cd WHERE trc_id='{trcid}'");
                object comid = SqlHelper.ExecuteOnlyOneQuery($"SELECT com_id FROM transfer_registration_pc WHERE trp_id='{trpid}'");
                querySql = $"SELECT dd_code, dd_name FROM data_dictionary WHERE dd_id='{comid}'";
            }
            else if(type == WorkType.TopicWork)
            {
                object trcid = SqlHelper.ExecuteOnlyOneQuery($"SELECT trc_id FROM topic_info WHERE ti_id='{id}'") ?? SqlHelper.ExecuteOnlyOneQuery($"SELECT trc_id FROM topic_info WHERE ti_id=(SELECT si_obj_id FROM subject_info WHERE si_id='{id}')");
                object trpid = SqlHelper.ExecuteOnlyOneQuery($"SELECT trp_id FROM transfer_registraion_cd WHERE trc_id='{trcid}'");
                object comid = SqlHelper.ExecuteOnlyOneQuery($"SELECT com_id FROM transfer_registration_pc WHERE trp_id='{trpid}'");
                querySql = $"SELECT dd_code, dd_name FROM data_dictionary WHERE dd_id='{comid}'";
            }
            return SqlHelper.ExecuteRowsQuery(querySql);
        }
       
        /// <summary>
        /// 获取单行数据
        /// </summary>
        public static DataRow ExecuteSingleRowQuery(string querySql)
        {
            DataTable table = ExecuteQuery(querySql);
            return table.Rows.Count > 0 ? table.Rows[0] : null;
        }
       
        /// <summary>
        /// 获取统计数(若结果为null或非数字，则默认返回0)
        /// </summary>
        public static int ExecuteCountQuery(string querySql)
        {
            object value = SqlHelper.ExecuteOnlyOneQuery(querySql);
            return ToolHelper.GetIntValue(value, 0);
        }

        public static string GetValueByKey(object companyId)
        {
            object obj = ExecuteOnlyOneQuery($"SELECT dd_name FROM data_dictionary WHERE dd_id='{companyId}'");
            return obj == null ? string.Empty : obj.ToString();
        }

        /// <summary>
        /// 获取单列数据
        /// </summary>
        public static object[] ExecuteSingleColumnQuery(string querySql)
        {
            List<object> list = new List<object>();
            SqlDataReader sqlDataReader = ExecuteQueryWithReader(querySql);
            while(sqlDataReader.Read())
            {
                if(sqlDataReader.FieldCount > 0)
                    list.Add(sqlDataReader.GetValue(0));
            }
            sqlDataReader.Close();
            CloseConnect();
            return list.ToArray();
        }

        public static Dictionary<object, int> GetKeyValuePair(string querySQL)
        {
            Dictionary<object, int> result = new Dictionary<object, int>();
            SqlDataReader sqlDataReader = ExecuteQueryWithReader(querySQL);
            while(sqlDataReader.Read())
            {
                if(sqlDataReader.FieldCount >= 2)
                    result.Add(sqlDataReader.GetValue(0), sqlDataReader.GetInt32(1));
            }
            sqlDataReader.Close();
            CloseConnect();
            return result;
        }

        /// <summary>
        /// 批量插入/更新数据
        /// </summary>
        /// <param name="table">数据源</param>
        public static void ExecuteUpdateQuery(DataTable table)
        {
            SqlConnection con = GetConnect();
            SqlCommand com = new SqlCommand($"SELECT TOP(0) FROM {table.TableName}", con);
            SqlDataAdapter adapter = new SqlDataAdapter(com);
            try
            {
                adapter.UpdateBatchSize = 5000;
                adapter.SelectCommand.Transaction = con.BeginTransaction();
                SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
                adapter.Update(table.GetChanges());
                adapter.SelectCommand.Transaction.Commit();
                table.AcceptChanges();
            }
            catch(Exception e)
            {
                LogsHelper.AddErrorLogs("批量插入失败", e.Message);
                adapter.SelectCommand.Transaction.Rollback();
            }
            finally
            {
                CloseConnect();
            }
        }

        /// <summary>
        /// 获取地区（省市）表
        /// </summary>
        /// <returns></returns>
        public static DataTable GetProvinceList()
        {
            string key = "dic_xzqy_province";
            string querySQL = "SELECT * FROM data_dictionary WHERE dd_pId=" +
               $"(SELECT dd_id FROM data_dictionary WHERE dd_code = '{key}') " +
                "ORDER BY dd_sort";
            return ExecuteQuery(querySQL);
        }
    }
}
