using System;
using System.Windows.Forms;

namespace 科技计划项目档案数据采集管理系统
{
    public partial class Frm_AdviceBW : Form
    {
        /// <summary>
        /// 当前对象所属ID
        /// </summary>
        private object objId;

        public Frm_AdviceBW(object objId, string objName, int type, bool isBackWork)
        {
            this.objId = objId;
            InitializeComponent();
            InitialForm(objName, objId, type, isBackWork);
        }
        /// <summary>
        /// 初始化窗体
        /// </summary>
        /// <param name="objName">对象名</param>
        /// <param name="objId">对象ID</param>
        /// <param name="type">意见类型</param>
        private void InitialForm(string objName, object objId, int type, bool isBackWork)
        {
            
        }
        /// <summary>
        /// 删除指定类型的意见
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Delete_Click(object sender, System.EventArgs e)
        {
            object qaid = lbl_ObjName.Tag;
            if(qaid != null)
            {
                if(MessageBox.Show("确定要删除当前质检意见吗?", "删除确认", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk) == DialogResult.Yes)
                {
                    SqlHelper.ExecuteNonQuery($"DELETE FROM quality_advices WHERE qa_id='{qaid}'");
                    MessageBox.Show("删除成功。");
                    Close();
                }
            }
        }

        

        private void Frm_Advice_Load(object sender, EventArgs e)
        {
            
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
