using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace 科技计划项目档案数据采集管理系统
{
    class CategorHelper
    {
        /// <summary>
        /// 存储阶段和类别的字典集
        /// </summary>
        private Dictionary<object, DataTable> CategorDictionary;
        private static CategorHelper _categorHelper;
        private CategorHelper()
        {
            CategorDictionary = new Dictionary<object, DataTable>();
            DataTable table = DictionaryHelper.GetTableByCode("dic_file_jd");
            foreach(DataRow row in table.Rows)
            {
                object pCode = row["dd_code"];
                DataTable _table = GetTableByCode(pCode);
                CategorDictionary.Add(row["dd_id"], _table);
            }
        }

        private DataTable GetTableByCode(object pCode)
        {
            return SqlHelper.ExecuteQuery(
                $"SELECT dd_id, dd_name + ' ' + extend_3 dd_name FROM data_dictionary WHERE dd_pId=(" +
                $"SELECT TOP(1) dd_id FROM data_dictionary WHERE dd_code = '{pCode}') " +
                $"ORDER BY dd_sort");
        }

        /// <summary>
        /// 获取类别帮助类单例实例
        /// </summary>
        public static CategorHelper GetInstance()
        {
            if(_categorHelper == null)
            {
                _categorHelper = new CategorHelper();
            }
            return _categorHelper;
        }

        /// <summary>
        /// 根据阶段ID获取指定类别集
        /// </summary>
        public DataTable GetCategorTableByStage(object stageID)
        {
            if(CategorDictionary.Count > 0)
            {
                bool flag = CategorDictionary.TryGetValue(stageID, out DataTable table);
                if(flag)
                {
                    return table;
                }
            }
            return null;
        }

        /// <summary>
        /// 获取默认文件类别（第一阶段）
        /// </summary>
        /// <returns></returns>
        public DataTable GetDefaultTable()
        {
            if(CategorDictionary.Count > 0)
            {
                return CategorDictionary.Values.ToArray()[0];
            }
            return null;
        }
    }
}
