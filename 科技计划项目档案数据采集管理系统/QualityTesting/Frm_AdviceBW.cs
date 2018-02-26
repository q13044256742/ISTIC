using System;
using System.Collections.Generic;

namespace 科技计划项目档案数据采集管理系统
{
    public partial class Frm_AdviceBW : DevExpress.XtraEditors.XtraForm
    {
        /// <summary>
        /// 当前对象所属ID
        /// </summary>
        private object objId;
        public Frm_AdviceBW(object objId, object objName)
        {
            this.objId = objId;
            InitializeComponent();
            lbl_ObjName.Text = GetValue(objName);
        }
        /// <summary>
        /// 加载当前意见
        /// </summary>
        private void Frm_Advice_Load(object sender, EventArgs e)
        {
            string querySql = $"SELECT a.qa_type, a.qa_advice FROM quality_advices a WHERE qa_time = " +
                $"(SELECT MAX(qa_time) FROM quality_advices WHERE qa_type = a.qa_type) " +
                $"AND qa_obj_id='{objId}' ORDER BY a.qa_type";
            List<object[]> list = SqlHelper.ExecuteColumnsQuery(querySql, 2);
            for(int i = 0; i < list.Count; i++)
            {
                int index = dgv_BW.Rows.Add();
                dgv_BW.Rows[index].Cells[0].Value = GetTypeValue(list[i][0]);
                dgv_BW.Rows[index].Cells[1].Value = list[i][1];
            }
        }
        /// <summary>
        /// 获取错误类型
        /// </summary>
        private object GetTypeValue(object index)
        {
            int i = Convert.ToInt32(index);
            if(i == 0) return "基本信息";
            else if(i == 1) return "文件信息";
            else if(i == 2) return "文件核查";
            else if(i == 3) return "案卷信息";
            else if(i == 4) return "案卷盒";
            else return string.Empty;
        }

        private string GetValue(object obj)
        {
            return obj == null ? string.Empty : obj.ToString();
        }

        
        /// <summary>
        /// 根据指定的对象ID和类型获取意见
        /// </summary>
        /// <param name="id">主键</param>
        /// <param name="type">类型</param>
        /// <returns>
        /// 对象数组：[0]主键[1]名称
        /// </returns>
        private object[] GetAdvice(object id, int type)
        {
            object[] _obj = SqlHelper.ExecuteRowsQuery($"SELECT qa_id, qa_advice FROM quality_advices WHERE qa_obj_id='{id}' AND qa_user='{UserHelper.GetInstance().User.UserKey}' AND qa_type={type}");
            return _obj ?? null;
        }
    }
}
