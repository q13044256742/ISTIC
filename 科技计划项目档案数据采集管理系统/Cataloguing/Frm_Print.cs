using System;
using System.Data;
using System.Windows.Forms;

namespace 科技计划项目档案数据采集管理系统.Cataloguing
{
    public partial class Frm_Print : DevExpress.XtraEditors.XtraForm
    {
        /// <summary>
        /// 项目/课题ID
        /// </summary>
        private object objId;
        /// <summary>
        /// 项目/课题名称
        /// </summary>
        private string objName;
        /// <summary>
        /// 盒号ID
        /// </summary>
        private object boxId;
        /// <summary>
        /// 档号
        /// </summary>
        private object docNumber;
        /// <summary>
        /// 馆藏号
        /// </summary>
        private string gcCode;

        public Frm_Print(object objId, object boxId, object docNumber, string objName, string gcCode)
        {
            InitializeComponent();
            this.objId = objId;
            this.boxId = boxId;
            this.docNumber = docNumber;
            this.objName = objName;
            this.gcCode = gcCode;
        }

        private string GetValue(object value) => value == null ? string.Empty : value.ToString();

        private void lbl_BKB_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            object _fileAmount = SqlHelper.ExecuteOnlyOneQuery($"SELECT pb_files_id FROM processing_box WHERE pb_id='{boxId}'");
            string[] _files = GetValue(_fileAmount).Split(',');
            int fileAmount = 0;
            int filePages = 0;
            for(int i = 0; i < _files.Length; i++)
            {
                if(!string.IsNullOrEmpty(_files[i]))
                {
                    fileAmount++;
                    object _page = SqlHelper.ExecuteOnlyOneQuery($"SELECT pfl_pages FROM processing_file_list WHERE pfl_id='{_files[i]}'");
                    if(!string.IsNullOrEmpty(GetValue(_page)))
                        filePages += Convert.ToInt32(_page);
                }
            }
            Frm_FileBackUpTable table = new Frm_FileBackUpTable()
            {
                fileAmount = fileAmount,
                filePages = filePages,
                docNumber = docNumber,
                user = UserHelper.GetInstance().User.RealName
            };
            table.Show();
        }

        private void lbl_FM_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Frm_Cover cover = new Frm_Cover()
            {
                objectName = objName,
                unitName = UserHelper.GetInstance().User.UnitName,
                bzDate = DateTime.Now.ToString("yyyy-MM-dd"),
                bgDate = DateTime.Now.ToString("yyyy-MM-dd"),
                gcCode = gcCode
            };
            cover.Show();
        }

        private void lbl_WJ_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string files = GetValue(SqlHelper.ExecuteOnlyOneQuery($"SELECT pb_files_id FROM processing_box WHERE pb_id='{boxId}'"));
            string[] fids = files.Split(',');
            DataTable table = new DataTable();
            table.Columns.AddRange(new DataColumn[]
            {
                new DataColumn("pfl_code"),
                new DataColumn("pfl_name"),
                new DataColumn("pfl_pages"),
                new DataColumn("pfl_amount"),
                new DataColumn("pfl_remark"),
            });
            for(int i = 0; i < fids.Length; i++)
            {
                if(!string.IsNullOrEmpty(fids[i]))
                {
                    DataRow row = SqlHelper.ExecuteSingleRowQuery($"SELECT pfl_code, pfl_name, pfl_pages, pfl_amount, pfl_remark FROM processing_file_list WHERE pfl_id='{fids[i]}'");
                    if(row != null)
                        table.ImportRow(row);
                }
            }
            Frm_BoxList boxList = new Frm_BoxList()
            {
                proCode = GetValue(docNumber),
                code = GetValue(docNumber),
                name = objName,
                gcCode = gcCode,
                dataTable = table
            };
            boxList.Show();
        }
    }
}
