using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using 科技计划项目档案数据采集管理系统.Properties;

namespace 科技计划项目档案数据采集管理系统
{
    public partial class Frm_PrintBox : DevExpress.XtraEditors.XtraForm
    {
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
        /// 密级
        /// </summary>
        public string secret;
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
                view.Rows[index].Cells["amount"].Value = GetFilePageCount(boxTable.Rows[i]["pb_id"], 1);
                view.Rows[index].Cells["id"].Value = boxTable.Rows[i]["pb_box_number"];
                view.Rows[index].Cells["id"].Tag = boxTable.Rows[i]["pb_gc_id"];
                view.Rows[index].Cells["fmbj"].Value = "20mm";
            }
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
                            int boxNumber = (int)row.Cells["id"].Value;
                            PrintBKB(id, boxNumber);
                        }
                        bool printFm = GetBooleanValue(row.Cells["fm"].Value);
                        if(printFm)
                        {
                            object bj = row.Cells["fmbj"].Value;
                            object GCNumber = row.Cells["id"].Tag;
                            PrintFM(id, bj, GCNumber, row.Index);
                        }
                        bool printJnml = GetBooleanValue(row.Cells["jnml"].Value);
                        if(printJnml)
                        {
                            object GCNumber = row.Cells["id"].Tag;
                            PrintJNML(id, GCNumber);
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
        private void PrintJNML(object boxId, object GCNumber)
        {
            string jnmlString = Resources.jnml;
            jnmlString = jnmlString.Replace("id=\"ajbh\">", $"id=\"ajbh\">{objectCode}");
            jnmlString = jnmlString.Replace("id=\"gch\">", $"id=\"gch\">{GCNumber}");

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
                        $"<td>{GetDateValue(dataTable.Rows[i]["pfl_date"], "yyyy-MM-dd")}&nbsp;</td>" +
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

        private object GetDateValue(object date, string format)
        {
            if(date != null)
            {
                if(DateTime.TryParse(GetValue(date), out DateTime result))
                {
                    if(result.Date != new DateTime(1900, 01, 01))
                        return result.ToString(format);
                }
                else
                    return date;
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
        /// 获取文件数/页数
        /// </summary>
        /// <param name="boxId">盒ID</param>
        /// <param name="type">获取类型
        /// <para>1：文件数</para>
        /// <para>2：页数</para>
        /// </param>
        private int GetFilePageCount(object boxId, int type)
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
                    if(type == 2)
                    {
                        object _page = SqlHelper.ExecuteOnlyOneQuery($"SELECT pfl_pages FROM processing_file_list WHERE pfl_id='{_files[i]}'");
                        if(!string.IsNullOrEmpty(GetValue(_page)))
                            filePages += Convert.ToInt32(_page);
                    }
                }
            }
            return type == 1 ? fileAmount : filePages;
        }

        /// <summary>
        /// 打印封面
        /// </summary>
        private void PrintFM(object boxId, object bj, object GCNumber, int rowIndex)
        {
            string fmString = Resources.fm;
            object fontObject = view.Rows[rowIndex].Cells["font"].Tag;
            if(font != null)
            {
                Font font = (Font)fontObject;
                fmString = fmString.Replace("font-family:;", $"font-family:{font.FontFamily};");
                fmString = fmString.Replace("font-size:;", $"font-size:{font.Size};");
            }
            fmString = GetCoverHtmlString(boxId, fmString, bj, GCNumber);

            new WebBrowser() { DocumentText = fmString, ScriptErrorsSuppressed = false }.DocumentCompleted += Web_DocumentCompleted;
        }

        /// <summary>
        /// 打印卷内备考表
        /// </summary>
        private void PrintBKB(object boxId, int boxNumber)
        {
            string bkbString = Resources.bkb;
            string fa = MicrosoftWordHelper.GetZN(GetFilePageCount(boxId, 1));
            string fp = MicrosoftWordHelper.GetZN(GetFilePageCount(boxId, 2));
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
            bkbString = bkbString.Replace("id=\"ljrq\">", $"id=\"dh\">{GetDateValue(ljDate, "yyyy-MM-dd")}");
            bkbString = bkbString.Replace("id=\"jcr\">", $"id=\"jcr\">{jcPeople}");
            bkbString = bkbString.Replace("id=\"jcrq\">", $"id=\"jcrq\">{GetDateValue(jcDate, "yyyy-MM-dd")}");

            new WebBrowser() { DocumentText = bkbString, ScriptErrorsSuppressed = false }.DocumentCompleted += Web_DocumentCompleted;
        }

        /// <summary>
        /// 打印文档
        /// </summary>
        private void Web_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            (sender as WebBrowser).Print();
            (sender as WebBrowser).Dispose();
        }

        /// <summary>
        /// 字体设置|预览
        /// </summary>
        private void View_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string columnName = view.Columns[e.ColumnIndex].Name;
            if("font".Equals(columnName))
            {
                object font = view.Rows[e.RowIndex].Cells[e.ColumnIndex].Tag;
                if(font != null)
                    fontDialog.Font = (Font)font;
                if(fontDialog.ShowDialog() == DialogResult.OK)
                {
                    view.Rows[e.RowIndex].Cells[e.ColumnIndex].Tag = fontDialog.Font;
                }
            }
            else if("preview".Equals(columnName))
            {
                string fmString = Resources.fm;
                object fontObject = view.Rows[e.RowIndex].Cells["font"].Tag;
                if(font != null)
                {
                    Font font = (Font)fontObject;
                    fmString = fmString.Replace("font-family:;", $"font-family:{font.FontFamily};");
                    fmString = fmString.Replace("font-size:;", $"font-size:{font.Size};");
                }
                object bj = view.Rows[e.RowIndex].Cells["fmbj"].Value;
                object boxId = view.Rows[e.RowIndex].Cells["print"].Tag;
                object GCNumber = view.Rows[e.RowIndex].Cells["id"].Tag;
                fmString = GetCoverHtmlString(boxId, fmString, bj, GCNumber);

                new WebBrowser() { DocumentText = fmString, ScriptErrorsSuppressed = false }.DocumentCompleted += Preview_DocumentCompleted;
            }
            else if("print".Equals(columnName))
            {
                
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
        private string GetCoverHtmlString(object boxId, string fmString, object bj, object GCNumber)
        {
            fmString = fmString.Replace("20mm", $"{bj}");
            if(string.IsNullOrEmpty(GetValue(parentObjectName)))
                fmString = fmString.Replace("id=\"ajmc\">", $"id=\"ajmc\">{objectName}");
            else
            {
                fmString = fmString.Replace("id=\"ajmc\">", $"id=\"ajmc\">{parentObjectName}");
                fmString = fmString.Replace("id=\"ktmc\">", $"id=\"ktmc\">{objectName}");
            }
            fmString = fmString.Replace("id=\"bzdw\">", $"id=\"bzdw\">{unitName}");
            fmString = fmString.Replace("id=\"bzrq\">", $"id=\"bzrq\">{GetBzDate(boxId)}");
            fmString = fmString.Replace("id=\"bgrq\">", $"id=\"bgrq\">永久");
            fmString = fmString.Replace("id=\"mj\">", $"id=\"mj\">{secret}");
            fmString = fmString.Replace("id=\"gch\">", $"id=\"dh\">{GCNumber}");
            return fmString;
        }

        /// <summary>
        /// 获取当前盒的编制日期（当前盒内文件的最早至最晚形成日期）
        /// </summary>
        private string GetBzDate(object boxId)
        {
            object fileIds = SqlHelper.ExecuteOnlyOneQuery($"SELECT pb_files_id FROM processing_box WHERE pb_id='{boxId}'");
            if(!string.IsNullOrEmpty(GetValue(fileIds)))
            {
                string[] ids = GetValue(fileIds).Split(',');
                string idsString = string.Empty;
                foreach(string id in ids)
                    if(!string.IsNullOrEmpty(id))
                        idsString += $"'{id}',";
                if(!string.IsNullOrEmpty(idsString))
                {
                    idsString = idsString.Substring(0, idsString.Length - 1);
                    object minDate = SqlHelper.ExecuteOnlyOneQuery($"SELECT MIN(pfl_date) FROM processing_file_list where pfl_id IN ({idsString}) AND CONVERT(DATE, pfl_date) <> '1900-01-01';");
                    object maxDate = SqlHelper.ExecuteOnlyOneQuery($"SELECT MAX(pfl_date) FROM processing_file_list where pfl_id IN ({idsString}) AND CONVERT(DATE, pfl_date) <> '1900-01-01';");
                    if(minDate != null && maxDate != null)
                        return $"{((DateTime)minDate).ToString("yyyy-MM-dd")} ~ {((DateTime)maxDate).ToString("yyyy-MM-dd")}";
                }
            }
            return null;
        }

    }
}
