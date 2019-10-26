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
        private const string SERVER_TYPE = "SQLServer_Server";
        private static readonly string IPAddress = OperateIniFile.GetInstance().ReadIniData(SERVER_TYPE, "IPAddress", null);
        private static readonly string Username = OperateIniFile.GetInstance().ReadIniData(SERVER_TYPE, "Username", null);
        private static readonly string Password = OperateIniFile.GetInstance().ReadIniData(SERVER_TYPE, "Password", null);
        private static readonly string CatalogName = OperateIniFile.GetInstance().ReadIniData(SERVER_TYPE, "CatalogName", null);
        /// <summary>
        /// 数据连接字符串
        /// </summary>
        private static readonly string SQL_CONNECT = $"Data Source={IPAddress};Initial Catalog={CatalogName};Persist Security Info=True;MultipleActiveResultSets=true;User ID={Username};Password={Password}";

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
        /// 关闭全局数据库连接
        /// </summary>
        public static void CloseConnect()
        {
            if (sqlConnection != null && sqlConnection.State == ConnectionState.Open)
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
            return table;
        }

        /// <summary>
        /// 查询唯一结果的SQL(无结果返回null)
        /// </summary>
        public static object ExecuteOnlyOneQuery(string querySql)
        {
            SqlCommand sqlCommand = new SqlCommand(querySql, GetConnect());
            object result = sqlCommand.ExecuteScalar();
            if (result != null)
                if (string.IsNullOrEmpty(ToolHelper.GetValue(result)))
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
            if (!string.IsNullOrEmpty(nonQuerySql))
            {
                SqlConnection con = GetConnect();
                SqlCommand sqlCommand = new SqlCommand(nonQuerySql, con);
                SqlTransaction sqlTransaction = con.BeginTransaction();
                sqlCommand.Transaction = sqlTransaction;
                try
                {
                    sqlCommand.ExecuteNonQuery();
                    sqlTransaction.Commit();
                }
                catch (Exception e)
                {
                    sqlTransaction.Rollback();
                    DevExpress.XtraEditors.XtraMessageBox.Show(e.Message, "数据出错(详情查看错误日志)");
                    LogsHelper.AddErrorLogs("执行SQL语句失败", $"SQL语句为 >> {nonQuerySql}");
                }
            }
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
            return _obj;
        }

        /// <summary>
        /// 获取来源单位列表
        /// </summary>
        public static DataTable GetCompanyList()
        {
            string key = "dic_key_company_source";
            string querySql = $"SELECT dd_id, dd_code, dd_name FROM data_dictionary WHERE dd_pId = (SELECT dd_id FROM data_dictionary WHERE dd_code='{key}') ORDER BY dd_sort";
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
            var type = (WorkType)Convert.ToInt32(workType);
            string querySql = null;
            switch (type)
            {
                case WorkType.PaperWork:
                {
                    object comId = ExecuteOnlyOneQuery($"SELECT com_id FROM transfer_registration_pc WHERE trp_id='{id}'");
                    querySql = $"SELECT dd_code, dd_name FROM data_dictionary WHERE dd_id='{comId}'";
                    break;
                }
                case WorkType.CDWork:
                {
                    object trpId = ExecuteOnlyOneQuery($"SELECT trp_id FROM transfer_registraion_cd WHERE trc_id='{id}'");
                    object comId = ExecuteOnlyOneQuery($"SELECT com_id FROM transfer_registration_pc WHERE trp_id='{trpId}'");
                    querySql = $"SELECT dd_code, dd_name FROM data_dictionary WHERE dd_id='{comId}'";
                    break;
                }
                case WorkType.ProjectWork:
                {
                    object trcid = ExecuteOnlyOneQuery($"SELECT trc_id FROM project_info WHERE pi_id= '{id}'") ?? ExecuteOnlyOneQuery($"SELECT trc_id FROM topic_info WHERE ti_id='{id}'");
                    object trpid = ExecuteOnlyOneQuery($"SELECT trp_id FROM transfer_registraion_cd WHERE trc_id='{trcid}'");
                    object comid = ExecuteOnlyOneQuery($"SELECT com_id FROM transfer_registration_pc WHERE trp_id='{trpid}'");
                    querySql = $"SELECT dd_code, dd_name FROM data_dictionary WHERE dd_id='{comid}'";
                    break;
                }
                case WorkType.TopicWork:
                {
                    object trcid = ExecuteOnlyOneQuery($"SELECT trc_id FROM topic_info WHERE ti_id='{id}'") ?? ExecuteOnlyOneQuery($"SELECT trc_id FROM topic_info WHERE ti_id=(SELECT si_obj_id FROM subject_info WHERE si_id='{id}')");
                    object trpid = ExecuteOnlyOneQuery($"SELECT trp_id FROM transfer_registraion_cd WHERE trc_id='{trcid}'");
                    object comid = ExecuteOnlyOneQuery($"SELECT com_id FROM transfer_registration_pc WHERE trp_id='{trpid}'");
                    querySql = $"SELECT dd_code, dd_name FROM data_dictionary WHERE dd_id='{comid}'";
                    break;
                }
            }
            return ExecuteRowsQuery(querySql);
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
            object value = ExecuteOnlyOneQuery(querySql);
            return ToolHelper.GetIntValue(value, 0);
        }

        /// <summary>
        /// 根据字典表的主键获取值(默认返回空字符串)
        /// </summary>
        /// <param name="objectId">字典表主键</param>
        /// <param name="fieldName">待显示的字段名</param>
        public static string GetValueByKey(object objectId, object fieldName)
        {
            object value = ExecuteOnlyOneQuery($"SELECT {fieldName} FROM data_dictionary WHERE dd_id='{objectId}'");
            return ToolHelper.GetValue(value);
        }

        /// <summary>
        /// 获取单列数据(无结果length=0)
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
            return result;
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

        /// <summary>
        /// 获取所有必交类型的文件的编号
        /// </summary>
        public static List<object> GetIsMustCategor()
        {
            string querySql = "SELECT d2.dd_name FROM data_dictionary d1 " +
                "LEFT JOIN data_dictionary d2 ON d1.dd_id = d2.dd_pId " +
                "WHERE d1.dd_pId = '3f5c727a-1fb7-4197-81d7-3c2fef295aa0' AND d2.extend_2 = 1";
            object[] result = ExecuteSingleColumnQuery(querySql);
            return new List<object>(result);
        }

        /// <summary>
        /// 根据指定批次ID获取其补录的其他批次根节点ID
        /// </summary>
        /// <param name="batchId">待查找批次ID</param>
        /// <param name="type">查找类型<para>0：计划</para><para>1：专项</para></param>
        public static object[] GetOtherBatchRootIds(object batchId, int type)
        {
            object otherBatchIds = ExecuteOnlyOneQuery($"SELECT br_auxiliary_id FROM batch_relevance WHERE br_main_id='{batchId}'");
            string otherBatchIdString = ToolHelper.GetFullStringBySplit(ToolHelper.GetValue(otherBatchIds), ',', ",", "'");
            if (otherBatchIdString.Length > 0)
            {
                if (type == 0)
                {
                    string querySql = $"SELECT pi_id FROM project_info WHERE pi_categor = 1 AND pi_obj_id IN ({otherBatchIdString})";
                    return ExecuteSingleColumnQuery(querySql);
                }
                else
                {
                    string querySql = "SELECT idi.imp_id FROM imp_info ii " +
                        "INNER JOIN imp_dev_info idi ON idi.imp_obj_id = ii.imp_id " +
                       $"WHERE ii.imp_obj_id IN ({otherBatchIdString})";
                    return ExecuteSingleColumnQuery(querySql);
                }
            }
            return null;
        }
    }
}
