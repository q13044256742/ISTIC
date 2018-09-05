using System;
using System.Collections.Generic;
using System.Data;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using 科技计划项目档案数据采集管理系统.Properties;

namespace 科技计划项目档案数据采集管理系统.DocumentAccept
{
    public partial class Frm_Print : DevExpress.XtraEditors.XtraForm
    {
        /// <summary>
        /// 打印类型
        /// <para>1：齐备 </para>
        /// <para>-：不齐备</para>
        /// </summary>
        private int TYPE;
        private object trpId;
        private Form parentForm;
        public Frm_Print(Form pForm, int type, object trpId)
        {
            InitializeComponent();
            parentForm = pForm;
            TYPE = type;
            this.trpId = trpId;
            if(type == 1)//齐备
            {
                chk1.Text = "档案接收确认函";
                chk2.Text = "文件列表清单";
                chk3.Visible = false;
            }
            else
            {
                chk1.Text = "档案催报单";
                chk2.Text = "缺失文件清单";
                chk3.Text = "文件列表清单";
            }
        }

        private void Frm_Print_Load(object sender, System.EventArgs e)
        {

        }

        private void btn_Print_Click(object sender, System.EventArgs e)
        {
            if(TYPE == 1)
            {
                //档案接收确认函
                if(chk1.Checked)
                {
                    new WebBrowser() { DocumentText = GetDomRecHTML() }.DocumentCompleted += Frm_Print_DocumentCompleted;
                }
                //文件列表清单
                if(chk2.Checked)
                {
                    CreateFileList();
                }
            }
            else
            {
                //档案接收确认函
                if(chk1.Checked)
                {
                    new WebBrowser() { DocumentText = GetDomRecHTML() }.DocumentCompleted += Frm_Print_DocumentCompleted;
                }
                //缺失文件清单
                if(chk2.Checked)
                {

                }
                //文件列表清单
                if(chk3.Checked)
                {

                }
            }
        }

        /// <summary>
        /// 文件列表清单
        /// </summary>
        private void CreateFileList()
        {
            List<DataTable> list = new List<DataTable>();
            //普通计划
            string querySQL = $"SELECT pi_id, pi_code, pi_name, pfl.* FROM project_info  " +
                $"LEFT JOIN processing_file_list pfl ON pfl_obj_id = pi_id " +
                $"WHERE pi_categor = 1 and trc_id = '{trpId}'; ";
            DataTable speTable = SqlHelper.ExecuteQuery(querySQL);
            if(speTable.Rows.Count > 0)
            {
                list.Add(speTable);
                string querySQL2 = $"SELECT pi_id, pi_code, pi_name, pfl.* FROM project_info  " +
                    $"LEFT JOIN processing_file_list pfl ON pfl_obj_id = pi_id " +
                    $"WHERE pi_categor = 2 and pi_obj_id = '{speTable.Rows[0]["pi_id"]}'; ";
                
            }
        }

        private void Frm_Print_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            (sender as WebBrowser).ShowPrintDialog();
            //System.Threading.Thread.Sleep(3000);
            (sender as WebBrowser).Dispose();
        }

        /// <summary>
        /// 档案接收确认函预览
        /// </summary>
        private void lbl1_Click(object sender, System.EventArgs e)
        {
            string domRec = GetDomRecHTML();

            new WebBrowser() { DocumentText = domRec, Parent = parentForm }.DocumentCompleted += Browser_DocumentCompleted;
        }

        private string GetDomRecHTML()
        {
            string domRec = Resources.domrec;
            string querySQL = "SELECT dd_name, trp_log_data FROM transfer_registration_pc " +
               $"LEFT JOIN data_dictionary ON dd_id = com_id WHERE trp_id = '{trpId}'";
            DataRow row = SqlHelper.ExecuteSingleRowQuery(querySQL);
            if(row != null)
            {
                SetTagValueById(ref domRec, "orgName", $"&emsp;&emsp;{row["dd_name"]}：");
                SetTagValueById(ref domRec, "param1", $"{ToolHelper.GetDateValue(row["trp_log_data"], "yyyy年MM月")}");
            }

            return domRec;
        }

        /// <summary>
        /// 设置指定ID元素的文本值
        /// </summary>
        /// <param name="domRec">html文档</param>
        /// <param name="id">元素ID</param>
        /// <param name="value">文本值</param>
        private void SetTagValueById(ref string domRec, string id, string value)
        {
            string pattern = $"<\\w+ id=\"{id}\">(&emsp;){{2}}</\\w+>";
            Match match = Regex.Match(domRec, pattern);
            if(match.Success)
            {
                string newValue = match.Value.Replace("&emsp;&emsp;", $"{value}");
                domRec = domRec.Replace(match.Value, newValue);
            }
        }

        private void Browser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            (sender as WebBrowser).ShowPrintPreviewDialog();
            (sender as WebBrowser).Dispose();
        }
    }
}
