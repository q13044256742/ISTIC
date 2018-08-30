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
        /// <summary>
        /// 父窗体
        /// </summary>
        private Form parentForm;
        public Frm_PrintBox(Form parentForm)
        {
            this.parentForm = parentForm;
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
            chk_BKB.Checked = chk_FMBJ.Checked = chk_JNML.Checked = flag;
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
            string jnmlString = GetFileList(boxId, GCNumber);

            new WebBrowser() { DocumentText = jnmlString }.DocumentCompleted += Print_DocumentCompleted;
        }

        /// <summary>
        /// 卷内目录
        /// </summary>
        private string GetFileList(object boxId, object GCNumber)
        {
            string jnmlString = Resources.jnml;
            jnmlString = jnmlString.Replace("id=\"ajbh\">", $"id=\"ajbh\">{objectCode}");
            jnmlString = jnmlString.Replace("id=\"gch\">", $"id=\"gch\">{GCNumber}");

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
            DataTable table = SqlHelper.ExecuteQuery($"SELECT pfl_code, pfl_user, pfl_name, pfl_pages, pfl_date, pfl_remark FROM processing_file_list WHERE pfl_box_id='{boxId}' ORDER BY pfl_box_sort");
            foreach(DataRow row in table.Rows)
                dataTable.ImportRow(row);
            int fileCount = dataTable.Rows.Count, pageCount = 0;
            int i = 0;
            foreach(DataRow dataRow in dataTable.Rows)
            {
                string newRr = "<tr>" +
                    $"<td>{++i}</td>" +
                    $"<td>{dataRow["pfl_code"]}&nbsp;</td>" +
                    $"<td>{dataRow["pfl_user"]}&nbsp;</td>" +
                    $"<td style='text-align: left;'>{dataRow["pfl_name"]}&nbsp;</td>" +
                    $"<td>{ToolHelper.GetDateValue(dataRow["pfl_date"], "yyyy-MM-dd")}&nbsp;</td>" +
                    $"<td>{dataRow["pfl_pages"]}&nbsp;</td>" +
                    $"<td>{dataRow["pfl_remark"]}&nbsp;</td>" +
                    $"</tr>";
                jnmlString = jnmlString.Replace("</tbody>", $"{newRr}</tbody>");
                pageCount += ToolHelper.GetIntValue(dataRow["pfl_pages"]);
            }
            jnmlString = jnmlString.Replace("id=\"fileCount\">", $"id=\"fileCount\">{fileCount}");
            jnmlString = jnmlString.Replace("id=\"pageCount\">", $"id=\"pageCount\">{pageCount}");
            return jnmlString;
        }

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
            int fileAmount = 0, filePages = 0;
            object[] pages = SqlHelper.ExecuteSingleColumnQuery($"SELECT pfl_pages FROM processing_file_list WHERE pfl_box_id='{boxId}'");
            for(int i = 0; i < pages.Length; i++)
            {
                fileAmount++;
                if(type == 2)
                    filePages += ToolHelper.GetIntValue(pages[i], 0);
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
            if(fontObject != null)
            {
                Font font = (Font)fontObject;
                fmString = fmString.Replace("id=\"ajmc\"", $"style=\"font-family:{font.FontFamily.Name}; \" id=\"ajmc\"");
                fmString = fmString.Replace($"style=\"font-family:{font.FontFamily.Name}; \" id=\"ajmc\"", $"style=\"font-family:{font.FontFamily.Name}; font-size:{font.Size}pt; \" id=\"ajmc\"");
            }
            object fontObject2 = view.Rows[rowIndex].Cells["fmbj"].Tag;
            if(fontObject2 != null)
            {
                Font font = (Font)fontObject2;
                fmString = fmString.Replace("id=\"ktmc\"", $"style=\"font-family:{font.FontFamily.Name}; \" id=\"ktmc\"");
                fmString = fmString.Replace($"style=\"font-family:{font.FontFamily.Name}; \" id=\"ktmc\"", $"style=\"font-family:{font.FontFamily.Name}; font-size:{font.Size}pt; \" id=\"ktmc\"");
            }
            fmString = GetCoverHtmlString(boxId, fmString, bj, GCNumber);
            new WebBrowser() { DocumentText = fmString }.DocumentCompleted += Print_DocumentCompleted;
        }

        /// <summary>
        /// 打印卷内备考表
        /// </summary>
        private void PrintBKB(object boxId, int boxNumber)
        {
            string bkbString = GetBackupTable(boxId, boxNumber);

            new WebBrowser() { DocumentText = bkbString }.DocumentCompleted += Print_DocumentCompleted;
        }

        /// <summary>
        /// 备考表
        /// </summary>
        private string GetBackupTable(object boxId, int boxNumber)
        {
            string bkbString = Resources.bkb;
            string fa = MicrosoftWordHelper.GetZN(GetFilePageCount(boxId, 1));
            string fp = MicrosoftWordHelper.GetZN(GetFilePageCount(boxId, 2));
            string hh = MicrosoftWordHelper.GetZN(boxNumber);

            bkbString = bkbString.Replace("name=\"count\"", $"name=\"count\" value=\"{fa}\"");
            bkbString = bkbString.Replace("name=\"pages\"", $"name=\"pages\" value=\"{fp}\"");
            bkbString = bkbString.Replace("name=\"number\"", $"name=\"number\" value=\"{hh}\"");
            string newTr = string.Empty;
            if(otherDoc.Rows.Count > 0)
                foreach(DataRow row in otherDoc.Rows)
                    newTr += $"<tr><td>{row["od_name"]}</td>" +
                        $"<td>{row["od_code"]}</td>" +
                        $"<td>{row["od_carrier"]}</td>" +
                        $"<td>{row["od_intro"]}</td></tr>";
            else
                newTr = "<tr><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td></tr>" +
                    "<tr><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td></tr>";
            bkbString = bkbString.Replace("</tbody>", $"{newTr}</tbody>");

            bkbString = bkbString.Replace("id=\"dh\">", $"id=\"dh\">{objectCode}");
            bkbString = bkbString.Replace("id=\"ljr\">", $"id=\"dh\">{ljPeople}");
            bkbString = bkbString.Replace("id=\"ljrq\">", $"id=\"dh\">{ToolHelper.GetDateValue(ljDate, "yyyy-MM-dd")}");
            bkbString = bkbString.Replace("id=\"jcr\">", $"id=\"jcr\">{jcPeople}");
            bkbString = bkbString.Replace("id=\"jcrq\">", $"id=\"jcrq\">{ToolHelper.GetDateValue(jcDate, "yyyy-MM-dd")}");
            return bkbString;
        }

        /// <summary>
        /// 打印文档
        /// </summary>
        private void Print_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            WebBrowser browser = sender as WebBrowser;
            if(browser.ReadyState == WebBrowserReadyState.Complete)
            {
                browser.Print();
                System.Threading.Thread.Sleep(5000);
                browser.Dispose();
            }
        }

        /// <summary>
        /// 打印预览
        /// </summary>
        private void Preview_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            WebBrowser browser = sender as WebBrowser;
            browser.Parent = parentForm;
            if(browser.ReadyState == WebBrowserReadyState.Complete)
            {
                browser.ShowPrintPreviewDialog();
                browser.Dispose();
            }
        }

        /// <summary>
        /// 字体设置
        /// </summary>
        private void View_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string columnName = view.Columns[e.ColumnIndex].Name;
            if("font".Equals(columnName))
            {
                object fontObject = view.Rows[e.RowIndex].Cells[e.ColumnIndex].Tag;
                if(fontObject != null)
                {
                    Font font = (Font)fontObject;
                    fontDialog.Font = (Font)font;
                }
                if(fontDialog.ShowDialog() == DialogResult.OK)
                {
                    view.Rows[e.RowIndex].Cells[e.ColumnIndex].Tag = fontDialog.Font;
                }
            }
            //选中当前行
            else if("print".Equals(columnName))
            {
                bool state = (bool)view.Rows[e.RowIndex].Cells[e.ColumnIndex].EditedFormattedValue;
                foreach(DataGridViewCell cell in view.Rows[e.RowIndex].Cells)
                {
                    if(cell is DataGridViewCheckBoxCell)
                    {
                        (cell as DataGridViewCheckBoxCell).Value = state;
                    }
                }
            }
        }

        /// <summary>
        /// 获取完整的封面HTML模板页
        /// </summary>
        /// <param name="bj">边距mm数</param>
        private string GetCoverHtmlString(object boxId, string fmString, object bj, object GCNumber)
        {
            fmString = fmString.Replace("20mm", $"{bj}");
            if(string.IsNullOrEmpty(ToolHelper.GetValue(parentObjectName)))
                fmString = fmString.Replace("id=\"ajmc\">", $"id=\"ajmc\">{objectName}");
            else
            {
                fmString = fmString.Replace("id=\"ajmc\">", $"id=\"ajmc\">{parentObjectName}");
                fmString = fmString.Replace("id=\"ktmc\">", $"id=\"ktmc\">{objectName}");
            }
            fmString = fmString.Replace("id=\"bzdw\">", $"id=\"bzdw\">{unitName}");
            fmString = fmString.Replace("id=\"bzrq\">", $"id=\"bzrq\">{ToolHelper.GetDateValue(DateTime.Now, "yyyy-MM-dd")}");
            fmString = fmString.Replace("id=\"bgqx\">", $"id=\"bgqx\">永久");
            fmString = fmString.Replace("id=\"gch\">", $"id=\"gch\">{GCNumber}");
            return fmString;
        }

        private void btn_PrinterSet_Click(object sender, EventArgs e)
        {
            Frm_PrintChoose printChoose = new Frm_PrintChoose();
            printChoose.ShowDialog();
        }

        private void view_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if(e.Button == MouseButtons.Right && e.RowIndex != -1 && e.ColumnIndex != -1)
            {
                字体设置SToolStripMenuItem.Visible = false;
                string name = view.Columns[e.ColumnIndex].Name;
                if("fm".Equals(name) || "bkb".Equals(name) || "jnml".Equals(name))
                {
                    view.ClearSelection();
                    view.CurrentCell = view.Rows[e.RowIndex].Cells[e.ColumnIndex];
                    contextMenuStrip1.Tag = view;
                    contextMenuStrip1.Show(MousePosition);
                }
                if("fm".Equals(name))
                    字体设置SToolStripMenuItem.Visible = true;
            }
        }

        private void 打印预览PToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataGridViewCell cell = view.CurrentCell;
            object boxId = cell.OwningRow.Cells["print"].Tag;
            object GCNumber = cell.OwningRow.Cells["id"].Tag;
            string HTML_STRING = string.Empty;
            if("fm".Equals(cell.OwningColumn.Name))
            {
                HTML_STRING = Resources.fm;
                object fontObject = cell.OwningRow.Cells["font"].Tag;
                if(fontObject != null)
                {
                    Font font = (Font)fontObject;
                    HTML_STRING = HTML_STRING.Replace("id=\"ajmc\"", $"style=\"font-family:{font.FontFamily.Name}; \" id=\"ajmc\"");
                    HTML_STRING = HTML_STRING.Replace($"style=\"font-family:{font.FontFamily.Name}; \" id=\"ajmc\"", $"style=\"font-family:{font.FontFamily.Name}; font-size:{font.Size}pt; \" id=\"ajmc\"");
                }
                object fontObject2 = cell.OwningRow.Cells["fmbj"].Tag;
                if(fontObject2 != null)
                {
                    Font font = (Font)fontObject2;
                    HTML_STRING = HTML_STRING.Replace("id=\"ktmc\"", $"style=\"font-family:{font.FontFamily.Name}; \" id=\"ktmc\"");
                    HTML_STRING = HTML_STRING.Replace($"style=\"font-family:{font.FontFamily.Name}; \" id=\"ktmc\"", $"style=\"font-family:{font.FontFamily.Name}; font-size:{font.Size}pt; \" id=\"ktmc\"");
                }
                object bj = cell.OwningRow.Cells["fmbj"].Value;
                HTML_STRING = GetCoverHtmlString(boxId, HTML_STRING, bj, GCNumber);
            }
            else if("bkb".Equals(cell.OwningColumn.Name))
            {
                object boxNumber = cell.OwningRow.Cells["id"].Value;
                HTML_STRING = GetBackupTable(boxId, ToolHelper.GetIntValue(boxNumber, 1));
            }
            else if("jnml".Equals(cell.OwningColumn.Name))
            {
                HTML_STRING = GetFileList(boxId, GCNumber);
            }
            new WebBrowser() { DocumentText = HTML_STRING, Size = new Size(500, 500) }.DocumentCompleted += Preview_DocumentCompleted;
        }

        private void 案卷名称ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataGridViewCell cell = view.CurrentCell;

            object fontObject = cell.OwningRow.Cells["font"].Tag;
            if(fontObject != null)
            {
                Font font = (Font)fontObject;
                fontDialog.Font = (Font)font;
            }
            if(fontDialog.ShowDialog() == DialogResult.OK)
            {
                cell.OwningRow.Cells["font"].Tag = fontDialog.Font;
            }
        }

        private void 课题名称ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataGridViewCell cell = view.CurrentCell;

            object fontObject = cell.OwningRow.Cells["fmbj"].Tag;
            if(fontObject != null)
            {
                Font font = (Font)fontObject;
                fontDialog.Font = (Font)font;
            }
            if(fontDialog.ShowDialog() == DialogResult.OK)
            {
                cell.OwningRow.Cells["fmbj"].Tag = fontDialog.Font;
            }
        }
    }
}
