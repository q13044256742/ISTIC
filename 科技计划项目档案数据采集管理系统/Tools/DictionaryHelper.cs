using System.Collections.Generic;
using System.Data;

namespace 科技计划项目档案数据采集管理系统
{
    /// <summary>
    /// 数据库字典帮助类
    /// </summary>
    class DictionaryHelper
    {
        /// <summary>
        /// 根据编码获取其下的数据的id和name属性
        /// </summary>
        /// <param name="parentCode">父节点Code</param>
        public static List<object[]> GetValuesByCode(object parentCode)
        {
            object parentId = SqlHelper.ExecuteOnlyOneQuery($"SELECT dd_id FROM data_dictionary WHERE dd_code = '{parentCode}'");
            return SqlHelper.ExecuteColumnsQuery($"SELECT dd_id, dd_name FROM data_dictionary WHERE dd_pId='{parentId}' ORDER BY dd_sort", 2);
        }

        /// <summary>
        /// 根据编码获取其下的数据表格
        /// </summary>
        /// <param name="parentCode"></param>
        /// <returns></returns>
        public static DataTable GetTableByCode(object parentCode)
        {
            object parentId = SqlHelper.ExecuteOnlyOneQuery($"SELECT dd_id FROM data_dictionary WHERE dd_code = '{parentCode}'");
            return SqlHelper.ExecuteQuery($"SELECT * FROM data_dictionary WHERE dd_pId='{parentId}' ORDER BY dd_sort");
        }
    }
}
