using System.Collections.Generic;
using System.Data;

namespace 科技计划项目档案数据采集管理系统
{
    public partial class Frm_DiskList : DevExpress.XtraEditors.XtraForm
    {
        /// <summary>
        /// 光盘编号集合
        /// </summary>
        private object objectIDS;
        public object objectCode;
        public Frm_DiskList(object objectIDS)
        {
            this.objectIDS = objectIDS;
            InitializeComponent();
        }

        private void Frm_DiskList_Load(object sender, System.EventArgs e)
        {
            string querySQL = $"SELECT trc_id, trc_name, trc_code FROM transfer_registraion_cd WHERE trc_id IN ({objectIDS}) ORDER BY trc_code";
            DataTable table = SqlHelper.ExecuteQuery(querySQL);
            foreach (DataRow row in table.Rows)
            {
                ObjectEntity entity = new ObjectEntity()
                {
                    ID = row["trc_id"],
                    CODE = row["trc_code"],
                    TEXT = row["trc_name"]
                };
                chkl.Items.Add(entity);
            }
        }

        private void checkBox1_CheckedChanged(object sender, System.EventArgs e)
        {
            bool b = checkBox1.Checked;
            for (int i = 0; i < chkl.Items.Count; i++)
            {
                chkl.SetItemChecked(i, b);
            }
        }

        private void SimpleButton1_Click(object sender, System.EventArgs e)
        {
            List<object> list = new List<object>();
            for (int i = 0; i < chkl.CheckedItems.Count; i++)
            {
                ObjectEntity entity = chkl.CheckedItems[i] as ObjectEntity;
                list.Add(entity.ID);
            }
            if (list.Count > 0)
            {
                objectCode = ToolHelper.GetFullStringBySplit(list.ToArray(), ",", "'");
                DialogResult = System.Windows.Forms.DialogResult.OK;
            }
        }
    }

    public class ObjectEntity
    {
        public object ID;
        public object CODE;
        public object TEXT;

        public override string ToString()
        {
            return $"{TEXT}[{CODE}]";
        }
    }
}
