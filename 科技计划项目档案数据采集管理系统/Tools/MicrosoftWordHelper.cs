using Microsoft.Office.Interop.Word;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace 科技计划项目档案数据采集管理系统
{
    class MicrosoftWordHelper
    {
        /// <summary>
        /// 向指定Word中写入指定文本
        /// </summary>
        /// <param name="filePath">Word 所在路径</param>
        /// <param name="list">所需写入的内容</param>
        public static void WriteDocument(ref string filePath, List<DataRow> list)
        {
            Microsoft.Office.Interop.Word.Application app = null;
            Document doc = null;
            try
            {
                //构造数据
                List<EntityObject> datas = new List<EntityObject>();
                for(int i = 0; i < list.Count; i++)
                {
                    string code = SqlHelper.GetValueByKey(list[i]["pfl_categor"]);
                    string name = GetValue(list[i]["pfl_filename"]);
                    string user = GetValue(list[i]["pfl_user"]);
                    string carrier = SqlHelper.GetValueByKey(list[i]["pfl_carrier"]);
                    int pages = Convert.ToInt32(list[i]["pfl_page_amount"]);
                    int number = Convert.ToInt32(list[i]["pfl_amount"]);
                    DateTime date = Convert.ToDateTime(list[i]["pfl_complete_date"]);
                    datas.Add(new EntityObject { Code = code, Name = name, User = user, Type = carrier, PageSize = pages, FileAmount = number, Date = date });
                }

                int rows = datas.Count() + 1;//表格行数加1是为了标题栏
                int cols = 7;//表格列数
                object oMissing = Missing.Value;
                app = new Microsoft.Office.Interop.Word.Application();//创建word应用程序
                doc = app.Documents.Add();//添加一个word文档

                app.Selection.PageSetup.LeftMargin = 50f;
                app.Selection.PageSetup.RightMargin = 50f;
                app.Selection.PageSetup.PageWidth = 800f;  //页面宽度

                //标题
                app.Selection.Font.Bold = 700;
                app.Selection.Font.Size = 18;
                app.Selection.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
                app.Selection.Text = "项目（课题）档案交接清单";

                //换行添加表格
                object line = WdUnits.wdLine;
                app.Selection.MoveDown(ref line, oMissing, oMissing);
                app.Selection.TypeParagraph();//换行
                app.Selection.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                Range range = app.Selection.Range;
                Table table = app.Selection.Tables.Add(range, rows, cols, ref oMissing, ref oMissing);
                //设置表格的字体大小粗细
                table.Range.Font.Size = 10;
                table.Range.Font.Bold = 0;
                table.Borders.OutsideLineStyle = WdLineStyle.wdLineStyleSingle;
                table.Borders.InsideLineStyle = WdLineStyle.wdLineStyleSingle;

                //设置表格标题
                int rowIndex = 1;
                table.Rows[rowIndex].Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
                table.Rows[rowIndex].Range.Font.Bold = 100;
                table.Rows[rowIndex].Height = 30f;
                table.Cell(rowIndex, 1).Range.Text = "项目（课题）档案材料名称";
                table.Cell(rowIndex, 2).Range.Text = "责任者";
                table.Cell(rowIndex, 3).Range.Text = "载体类型";
                table.Cell(rowIndex, 4).Range.Text = "页数";
                table.Cell(rowIndex, 5).Range.Text = "文件数";
                table.Cell(rowIndex, 6).Range.Text = "日期";
                table.Cell(rowIndex, 7).Range.Text = "备注";
                table.Columns[1].Width = 200f;
                table.Columns[2].Width = table.Columns[4].Width = table.Columns[5].Width = 50f;
                //循环数据创建数据行
                foreach (EntityObject eo in datas)
                {
                    rowIndex++;
                    table.Cell(rowIndex, 1).Range.Text = eo.Name;
                    table.Cell(rowIndex, 2).Range.Text = eo.User;
                    table.Cell(rowIndex, 3).Range.Text = eo.Type;
                    table.Cell(rowIndex, 4).Range.Text = eo.PageSize.ToString();
                    table.Cell(rowIndex, 5).Range.Text = eo.FileAmount.ToString();
                    table.Cell(rowIndex, 6).Range.Text = eo.Date.ToString("yyyy-MM-dd");
                    table.Cell(rowIndex, 7).Range.Text = eo.Remark;
                }
                app.Selection.EndKey(WdUnits.wdStory, oMissing); //将光标移动到文档末尾
                app.Selection.Font.Bold = 0;
                app.Selection.Font.Size = 11;
                
                //底部署名
                doc.Content.InsertAfter("\n移交单位（盖章）：                                        接收单位（盖章）：\n");
                doc.Content.InsertAfter("移交人：                                                 接收人：\n");
                doc.Content.InsertAfter("交接时间：    年  月  日");

                //导出到文件
                filePath += DateTime.Now.ToString("yyyyMMddHHmm") + ".doc";
                doc.SaveAs(filePath,
                    oMissing, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing,
                    oMissing, oMissing, oMissing, oMissing, oMissing, oMissing);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (doc != null)
                    doc.Close();//关闭文档
                if (app != null)
                    app.Quit();//退出应用程序
            }
        }

        private static string GetValue(object v) => v == null ? string.Empty : v.ToString();
    }
    class EntityObject
    {
        private string code;
        private string name;
        private string user;
        private string type;
        private int pageSize;
        private int fileAmount;
        private DateTime date;
        private string remark;

        public string Code { get => code; set => code = value; }
        public string Name { get => name; set => name = value; }
        public string User { get => user; set => user = value; }
        public string Type { get => type; set => type = value; }
        public int PageSize { get => pageSize; set => pageSize = value; }
        public int FileAmount { get => fileAmount; set => fileAmount = value; }
        public DateTime Date { get => date; set => date = value; }
        public string Remark { get => remark; set => remark = value; }
    }
}
