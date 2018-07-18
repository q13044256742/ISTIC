using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using 科技计划项目档案数据采集管理系统.Properties;

namespace 科技计划项目档案数据采集管理系统
{
    public partial class Frm_PrintBox : DevExpress.XtraEditors.XtraForm
    {
        /// <summary>
        /// 文件总份数
        /// </summary>
        public int fileAmount;
        /// <summary>
        /// 文件总页数
        /// </summary>
        public int filePages;
        /// <summary>
        /// 案卷名称
        /// </summary>
        public string objectName;
        /// <summary>
        /// 案卷编号
        /// </summary>
        public object objectCode;
        /// <summary>
        /// 编制单位
        /// </summary>
        public string unitName;
        /// <summary>
        /// 编制日期
        /// </summary>
        public string bzDate;
        /// <summary>
        /// 保管日期
        /// </summary>
        public string bgDate;
        /// <summary>
        /// 密级
        /// </summary>
        public string secret;
        /// <summary>
        /// 馆藏号
        /// </summary>
        public string gcCode;
        /// <summary>
        /// 项目编号
        /// </summary>
        public string proCode;
        /// <summary>
        /// 项目名称
        /// </summary>
        public string proName;
        /// <summary>
        /// 盒列表
        /// </summary>
        public DataTable boxTable;
        /// <summary>
        /// 所属对象父级名称
        /// </summary>
        public object parentObjectName;
        /// <summary>
        /// 盒号
        /// </summary>
        public int boxNumber;
        /// <summary>
        /// 其他密切相关文档
        /// </summary>
        public DataTable otherDoc;
        public string ljPeople;
        public string ljDate;
        public string jcPeople;
        public string jcDate;
        public Frm_PrintBox()
        {
            InitializeComponent();
            InitialFrom();
        }

        private void InitialFrom()
        {
            view.ColumnHeadersDefaultCellStyle.Font = new Font("宋体", 12, FontStyle.Bold);
            cbo_BJ.SelectedIndex = 0;
        }

        private void Frm_PrintBox_Load(object sender, EventArgs e)
        {
            for(int i = 0; i < boxTable.Rows.Count; i++)
            {
                int index = view.Rows.Add();
                view.Rows[index].Cells["print"].Tag = boxTable.Rows[i]["pb_id"];
                view.Rows[index].Cells["amount"].Value = GetFileAmount(boxTable.Rows[i]["pb_id"]);
                view.Rows[index].Cells["id"].Value = boxTable.Rows[i]["pb_box_number"];
                view.Rows[index].Cells["fmbj"].Value = "20mm";
            }
        }

        private int GetFileAmount(object id)
        {
            int i = 0;
            object idsObject = SqlHelper.ExecuteOnlyOneQuery($"SELECT pb_files_id FROM processing_box WHERE pb_id='{id}'");
            if(idsObject != null)
            {
                string ids = GetValue(idsObject);
                string[] idsArray = ids.Split(',');
                foreach(string item in idsArray)
                    if(!string.IsNullOrEmpty(item))
                        i++;
            }
            return i;
        }

        private void Chk_PrintAll_CheckedChanged(object sender, EventArgs e)
        {
            bool flag = chk_PrintAll.Checked;
            foreach(DataGridViewRow row in view.Rows)
            {
                row.Cells["print"].Value = flag;
            }
        }

        private void Chk_BKB_CheckedChanged(object sender, EventArgs e)
        {
            bool flag = chk_BKB.Checked;
            foreach(DataGridViewRow row in view.Rows)
            {
                row.Cells["bkb"].Value = flag;
            }
        }

        private void Chk_JNML_CheckedChanged(object sender, EventArgs e)
        {
            bool flag = chk_JNML.Checked;
            foreach(DataGridViewRow row in view.Rows)
            {
                row.Cells["jnml"].Value = flag;
            }
        }

        private void Chk_FMBJ_CheckedChanged(object sender, EventArgs e)
        {
            bool flag = chk_FMBJ.Checked;
            foreach(DataGridViewRow row in view.Rows)
            {
                row.Cells["fm"].Value = flag;
                row.Cells["fmbj"].Value = flag ? cbo_BJ.SelectedItem : "20mm";
            }
        }

        private void Cbo_BJ_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool flag = chk_FMBJ.Checked;
            if(flag)
            {
                foreach(DataGridViewRow row in view.Rows)
                {
                    row.Cells["fmbj"].Value = cbo_BJ.SelectedItem;
                }
            }
        }

        private void Btn_StartPrint_Click(object sender, EventArgs e)
        {
            List<object> boxIds = new List<object>();
            foreach(DataGridViewRow row in view.Rows)
            {
                if(true.Equals(row.Cells["print"].Value))
                {
                    boxIds.Add(row.Cells["print"].Tag);
                }
            }
            if(boxIds.Count > 0)
            {
                PrintDocument(boxIds.ToArray());
            }
            else
                MessageBox.Show("请先至少选择一盒进行打印。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void PrintDocument(object[] ids)
        {
            foreach(object id in ids)
            {
                foreach(DataGridViewRow row in view.Rows)
                {
                    if(id.Equals(row.Cells["print"].Tag))
                    {
                        bool printBkb = GetBooleanValue(row.Cells["bkb"].Value);
                        if(printBkb)
                        {
                            SetCurrentState(row.Cells["id"].Value, "备考表");
                            PrintBKB(id);
                        }
                        bool printFm = GetBooleanValue(row.Cells["fm"].Value);
                        if(printFm)
                        {
                            SetCurrentState(row.Cells["id"].Value, "封面&脊背");
                            object bj = row.Cells["fmbj"].Value;
                            PrintFM(id, bj);
                        }
                        bool printJnml = GetBooleanValue(row.Cells["jnml"].Value);
                        if(printJnml)
                        {
                            SetCurrentState(row.Cells["id"].Value, "卷内文件目录");
                            PrintJNML(id);
                        }
                    }
                }
                tip.Text = "提示：正在执行打印操作，请等待打印完毕。。。";
            }
        }

        private bool GetBooleanValue(object value) => value == null ? false : string.IsNullOrEmpty(value.ToString()) ? false : (bool)value;

        private void SetCurrentState(object value, string type) => tip.Text = $"提示：正在打印盒{value}{type}";

        /// <summary>
        /// 打印卷内文件目录
        /// </summary>
        private void PrintJNML(object boxId)
        {
            string jnmlString = Resources.jnml;
            jnmlString = jnmlString.Replace("id=\"ajbh\">", $"id=\"ajbh\">{objectCode}");

            string files = GetValue(SqlHelper.ExecuteOnlyOneQuery($"SELECT pb_files_id FROM processing_box WHERE pb_id='{boxId}'"));
            string[] fids = files.Split(',');
            DataTable dataTable = new DataTable();
            dataTable.Columns.AddRange(new DataColumn[]
            {
                new DataColumn("pfl_code"),
                new DataColumn("pfl_user"),
                new DataColumn("pfl_name"),
                new DataColumn("pfl_pages"),
                new DataColumn("pfl_date"),
                new DataColumn("pfl_remark"),
            });
            for(int i = 0; i < fids.Length; i++)
            {
                if(!string.IsNullOrEmpty(fids[i]))
                {
                    DataRow row = SqlHelper.ExecuteSingleRowQuery($"SELECT pfl_code, pfl_user, pfl_name, pfl_pages, pfl_date, pfl_remark FROM processing_file_list WHERE pfl_id='{fids[i]}'");
                    if(row != null)
                        dataTable.ImportRow(row);
                }
            }
            int fileCount = 0, pageCount = 0;
            if(dataTable != null)
            {
                fileCount = dataTable.Rows.Count;
                for(int i = 0; i < dataTable.Rows.Count; i++)
                {
                    string newRr = "<tr>" +
                        $"<td>{i + 1}</td>" +
                        $"<td>{dataTable.Rows[i]["pfl_code"]}&nbsp;</td>" +
                        $"<td>{dataTable.Rows[i]["pfl_user"]}&nbsp;</td>" +
                        $"<td>{dataTable.Rows[i]["pfl_name"]}&nbsp;</td>" +
                        $"<td>{GetDateValue(dataTable.Rows[i]["pfl_date"])}&nbsp;</td>" +
                        $"<td>{dataTable.Rows[i]["pfl_pages"]}&nbsp;</td>" +
                        $"<td>{dataTable.Rows[i]["pfl_remark"]}&nbsp;</td>" +
                        $"</tr>";
                    jnmlString = jnmlString.Replace("</tbody>", $"{newRr}</tbody>");
                    pageCount += GetIntValue(dataTable.Rows[i]["pfl_pages"]);
                }
            }
            jnmlString = jnmlString.Replace("id=\"fileCount\">", $"id=\"fileCount\">{fileCount}");
            jnmlString = jnmlString.Replace("id=\"pageCount\">", $"id=\"pageCount\">{pageCount}");
            new WebBrowser() { DocumentText = jnmlString, ScriptErrorsSuppressed = false }.DocumentCompleted += Web_DocumentCompleted;
        }

        private object GetDateValue(object date)
        {
            if(date != null)
            {
                if(DateTime.TryParse(GetValue(date), out DateTime result))
                {
                    if(result.Date != new DateTime(1900, 01, 01))
                        return result.ToString("yyyy-MM-dd");
                }
            }
            return null;
        }

        private int GetIntValue(object value)
        {
            if(value == null)
                return 0;
            else
            {
                if(int.TryParse(GetValue(value), out int result))
                    return result;
                else
                    return 0;
            }
        }

        private string GetValue(object value) => value == null ? string.Empty : value.ToString();

        /// <summary>
        /// 打印封面
        /// </summary>
        private void PrintFM(object id, object bj)
        {
            string fmString = Resources.fm;
            fmString = GetCoverHtmlString(fmString, bj);

            new WebBrowser() { DocumentText = fmString, ScriptErrorsSuppressed = false }.DocumentCompleted += Web_DocumentCompleted;
        }

        /// <summary>
        /// 打印卷内备考表
        /// </summary>
        private void PrintBKB(object id)
        {
            string bkbString = Resources.bkb;
            string fa = MicrosoftWordHelper.GetZN(fileAmount);
            string fp = MicrosoftWordHelper.GetZN(filePages);
            string hh = MicrosoftWordHelper.GetZN(boxNumber);

            bkbString = bkbString.Replace("name=\"count\"", $"name=\"count\" value=\"{fa}\"");
            bkbString = bkbString.Replace("name=\"pages\"", $"name=\"pages\" value=\"{fp}\"");
            bkbString = bkbString.Replace("name=\"number\"", $"name=\"number\" value=\"{hh}\"");

            foreach(DataRow row in otherDoc.Rows)
            {
                string newTr = $"<tr>" +
                    $"<td>{row["od_name"]}</td>" +
                    $"<td>{row["od_code"]}</td>" +
                    $"<td>{row["od_carrier"]}</td>" +
                    $"<td>{row["od_intro"]}</td>" +
                    $"</tr>";
                bkbString = bkbString.Replace("</tbody>", $"{newTr}</tbody>");
            }

            bkbString = bkbString.Replace("id=\"dh\">", $"id=\"dh\">{objectCode}");
            bkbString = bkbString.Replace("id=\"ljr\">", $"id=\"dh\">{ljPeople}");
            bkbString = bkbString.Replace("id=\"ljrq\">", $"id=\"dh\">{ljDate}");
            bkbString = bkbString.Replace("id=\"jcr\">", $"id=\"jcr\">{jcPeople}");
            bkbString = bkbString.Replace("id=\"jcrq\">", $"id=\"jcrq\">{jcDate}");

            new WebBrowser() { DocumentText = bkbString, ScriptErrorsSuppressed = false }.DocumentCompleted += Web_DocumentCompleted;
        }

        /// <summary>
        /// 打印文档
        /// </summary>
        private void Web_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            (sender as WebBrowser).ShowPrintPreviewDialog();
            (sender as WebBrowser).Dispose();
        }

        private void View_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string columnName = view.Columns[e.ColumnIndex].Name;
            if("font".Equals(columnName))
            {
                if(fontDialog.ShowDialog() == DialogResult.OK)
                {
                    view.Rows[e.RowIndex].Cells[e.ColumnIndex].Tag = fontDialog.Font;
                }
            }
            else if("preview".Equals(columnName))
            {
                string fmString = Resources.fm;
                Font font = (Font)view.Rows[e.RowIndex].Cells["font"].Tag;
                if(font != null)
                {
                    fmString = fmString.Replace("font-family:;", $"font-family:{font.FontFamily};");
                    fmString = fmString.Replace("font-size:;", $"font-size:{font.Size};");
                }
                object bj = view.Rows[e.RowIndex].Cells["fmbj"].Value;
                fmString = GetCoverHtmlString(fmString, bj);

                new WebBrowser() { DocumentText = fmString, ScriptErrorsSuppressed = false }.DocumentCompleted += Preview_DocumentCompleted;
            }
        }

        /// <summary>
        /// 打印预览
        /// </summary>
        private void Preview_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            (sender as WebBrowser).ShowPrintPreviewDialog();
            (sender as WebBrowser).Dispose();
        }

        /// <summary>
        /// 获取完整的封面HTML模板页
        /// </summary>
        /// <param name="bj">边距mm数</param>
        private string GetCoverHtmlString(string fmString, object bj)
        {
            fmString = fmString.Replace("20mm", $"{bj}");
            fmString = fmString.Replace("id=\"ajmc\">", $"id=\"ajmc\">{parentObjectName}");
            fmString = fmString.Replace("id=\"ktmc\">", $"id=\"ktmc\">{objectName}");
            fmString = fmString.Replace("id=\"bzdw\">", $"id=\"bzdw\">{unitName}");
            fmString = fmString.Replace("id=\"bzrq\">", $"id=\"bzrq\">{bzDate}");
            fmString = fmString.Replace("id=\"bgrq\">", $"id=\"bgrq\">{bgDate}");
            fmString = fmString.Replace("id=\"mj\">", $"id=\"mj\">{secret}");
            fmString = fmString.Replace("id=\"gch\">", $"id=\"dh\">{gcCode}");
            return fmString;
        }
    }
}
