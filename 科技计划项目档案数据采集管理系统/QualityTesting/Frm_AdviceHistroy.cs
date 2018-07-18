using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace 科技计划项目档案数据采集管理系统
{
    public partial class Frm_AdviceHistroy : DevExpress.XtraEditors.XtraForm
    {
        /// <summary>
        /// 当前对象所属ID
        /// </summary>
        private object objId;
        public Frm_AdviceHistroy(object objId, object objName)
        {
            this.objId = objId;
            InitializeComponent();
            lbl_ObjName.Text = GetValue(objName);
        }
        /// <summary>
        /// 加载历史意见
        /// </summary>
        private void Frm_Advice_Load(object sender, EventArgs e)
        {
            dgv_BW.Rows.Clear();
            string querySql = $"SELECT qa_id, qa_type, qa_advice, qa_time FROM quality_advices WHERE qa_obj_id='{objId}' ORDER BY qa_type, qa_time";
            DataTable table = SqlHelper.ExecuteQuery(querySql);
            foreach(DataRow row in table.Rows)
            {
                int index = dgv_BW.Rows.Add();
                dgv_BW.Rows[index].Tag = row["qa_id"];
                dgv_BW.Rows[index].Cells[0].Value = GetTypeValue(row["qa_type"]);
                dgv_BW.Rows[index].Cells[1].Value = row["qa_advice"];
                dgv_BW.Rows[index].Cells[2].Value = row["qa_time"];
            }

            dgv_BW.ColumnHeadersDefaultCellStyle = DataGridViewStyleHelper.GetHeaderStyle();
            dgv_BW.DefaultCellStyle = DataGridViewStyleHelper.GetCellStyle();
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

        private void Btn_Delete_Click(object sender, EventArgs e)
        {
            int count = dgv_BW.SelectedRows.Count;
            if(count > 0)
            {
                DialogResult dialogResult = XtraMessageBox.Show("确定要删除选中行吗？", "确认提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);
                if(dialogResult == DialogResult.OK)
                {
                    string ids = string.Empty;
                    foreach(DataGridViewRow row in dgv_BW.SelectedRows)
                        ids += $"'{row.Tag}',";
                    ids = ids.Substring(0, ids.Length - 1);
                    SqlHelper.ExecuteNonQuery($"DELETE FROM quality_advices WHERE qa_id IN ({ids})");
                    Frm_Advice_Load(null, null);
                    XtraMessageBox.Show("删除成功.");
                }
            }
        }
    }
}
