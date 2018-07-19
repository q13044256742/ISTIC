using DevExpress.XtraEditors;
using System;
using System.Windows.Forms;

namespace 科技计划项目档案数据采集管理系统
{
    public partial class Frm_Advice : XtraForm
    {
        /// <summary>
        /// 当前对象所属ID
        /// </summary>
        private object objId;

        public Frm_Advice(object objId, string objName, int type, bool isBackWork)
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
            cbo_AdviceType.SelectedIndex = type + 1;
            lbl_ObjName.Text = objName;
            object[] _obj = GetAdvice(objId, type);
            if(_obj != null)
            {
                lbl_ObjName.Tag = _obj[0];
                txt_Advice.Text = GetValue(_obj[1]);
            }
            if(isBackWork)
            {
                btn_Sure.Visible = btn_Delete.Visible = false;
                txt_Advice.ReadOnly = true;
            }
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
                if(XtraMessageBox.Show("确定要删除当前质检意见吗?", "删除确认", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk) == DialogResult.Yes)
                {
                    SqlHelper.ExecuteNonQuery($"DELETE FROM quality_advices WHERE qa_id='{qaid}'");
                    XtraMessageBox.Show("删除成功。");
                    Close();
                }
            }
        }

        private void Btn_Sure_Click(object sender, System.EventArgs e)
        {
            int index = cbo_AdviceType.SelectedIndex;
            if(index != -1)
            {
                object advice = txt_Advice.Text;
                string primaryKey = Guid.NewGuid().ToString();
                string insertSql = $"INSERT INTO quality_advices VALUES('{primaryKey}','{UserHelper.GetInstance().User.UserKey}','{DateTime.Now}','{objId}','{advice}',{index})";
                SqlHelper.ExecuteNonQuery(insertSql);
                lbl_ObjName.Tag = primaryKey;
                btn_Delete.Enabled = true;
                XtraMessageBox.Show("保存成功。");
                Close();
            }
        }

        private void Frm_Advice_Load(object sender, EventArgs e)
        {
            Cbo_AdviceType_SelectionChangeCommitted(null, null);
        }

        private string GetValue(object obj)
        {
            return obj == null ? string.Empty : obj.ToString();
        }

        private void Cbo_AdviceType_SelectionChangeCommitted(object sender, EventArgs e)
        {
            int index = cbo_AdviceType.SelectedIndex;
            if(index != -1)
            {
                txt_Advice.Clear();
                lbl_ObjName.Tag = null;
                object[] _obj = GetAdvice(objId, index);
                if(_obj != null)
                {
                    lbl_ObjName.Tag = _obj[0];
                    txt_Advice.Text = GetValue(_obj[1]);
                }
            }
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
            object[] _obj = SqlHelper.ExecuteRowsQuery($"SELECT qa_id, qa_advice FROM quality_advices WHERE qa_obj_id='{id}' AND qa_user='{UserHelper.GetInstance().User.UserKey}' AND qa_type={type} ORDER BY qa_time DESC");
            return _obj ?? null;
        }
        /// <summary>
        /// 历史意见
        /// </summary>
        private void Btn_HistroyOpinion_Click(object sender, EventArgs e)
        {
            Form form = GetHistoryFrom(objId, lbl_ObjName.Text);
            form.Show();
            form.Activate();
        }

        private Frm_AdviceHistroy frm;
        private Form GetHistoryFrom(object param1, object param2)
        {
            if(frm == null || frm.IsDisposed)
                frm = new Frm_AdviceHistroy(param1, param2);
            return frm;
        }
    }
}
