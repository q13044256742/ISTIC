using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace 科技计划项目档案数据采集管理系统
{
    class DataGridViewStyleHelper
    {
        /// <summary>
        /// 默认表头字体大小
        /// </summary>
        private static float DefaultHeaderFontSize = 10f;
        /// <summary>
        /// 默认单元格字体大小
        /// </summary>
        private static float DefaultCellFontSize = 9f;

        /// <summary>
        /// 获取DataGridView默认表头样式
        /// </summary>
        /// <returns></returns>
        public static DataGridViewCellStyle GetHeaderStyle()
        {
            DataGridViewCellStyle style = new DataGridViewCellStyle();
            style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            style.Font = new System.Drawing.Font("微软雅黑", DefaultHeaderFontSize, System.Drawing.FontStyle.Bold);
            return style;
        }

        /// <summary>
        /// 设置指定列的宽度
        /// </summary>
        /// <param name="indexs">二维数组，指定列和指定宽度</param>
        public static void SetWidth(DataGridView dataGridView, List<KeyValuePair<int, int>> keyValue)
        {
            for (int i = 0; i < keyValue.Count; i++)
                dataGridView.Columns[keyValue[i].Key].Width = keyValue[i].Value;
        }

        /// <summary>
        /// 设置指定列的宽度
        /// </summary>
        /// <param name="indexs">二维数组，指定列和指定宽度</param>
        public static void SetWidth(DataGridView dataGridView, List<KeyValuePair<string, int>> keyValue)
        {
            for (int i = 0; i < keyValue.Count; i++)
                dataGridView.Columns[keyValue[i].Key].Width = keyValue[i].Value;
        }

        /// <summary>
        /// 设置指定列的值为可点击样式
        /// </summary>
        /// <param name="indexs">指定列的列数（从0开始）</param>
        public static void SetLinkStyle(DataGridView dataGridView, int[] indexs, bool special)
        {
            DataGridViewCellStyle style = new DataGridViewCellStyle();
            style.Font = new System.Drawing.Font("微软雅黑", DefaultCellFontSize);
            style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            if (special)
            {
                for (int i = 0; i < dataGridView.RowCount - 1; i++)
                {
                    for (int j = 0; j < indexs.Length; j++)
                    {
                        int temp = -1;
                        object val = dataGridView.Rows[i].Cells[indexs[j]].Value;
                        if (val == null || (Int32.TryParse(val.ToString(), out temp) && temp == 0))
                            style.ForeColor = System.Drawing.Color.Black;
                        else
                            style.ForeColor = System.Drawing.Color.Blue;
                        dataGridView.Rows[i].Cells[indexs[j]].Style = style;
                    }
                }

            }
            else
            {
                style.ForeColor = System.Drawing.Color.Blue;
                for (int j = 0; j < indexs.Length; j++)
                {
                    dataGridView.Columns[indexs[j]].DefaultCellStyle = style;
                    dataGridView.Columns[indexs[j]].ReadOnly = true;
                }
            }
        }

        /// <summary>
        /// 设置指定列的值为可点击样式
        /// </summary>
        /// <param name="indexs">指定列的列名</param>
        public static void SetLinkStyle(DataGridView dataGridView, string[] columNames, bool special)
        {
            DataGridViewCellStyle normalStyle = new DataGridViewCellStyle();
            normalStyle.Font = new System.Drawing.Font("微软雅黑", DefaultCellFontSize);
            normalStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            normalStyle.ForeColor = System.Drawing.Color.Blue;

            DataGridViewCellStyle specialStyle = new DataGridViewCellStyle();
            specialStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            if (special)
            {
                for (int i = 0; i < dataGridView.RowCount - 1; i++)
                {
                    for (int j = 0; j < columNames.Length; j++)
                    {
                        int temp = -1;
                        object val = dataGridView.Rows[i].Cells[columNames[j]].Value;
                        if (val == null || (Int32.TryParse(val.ToString(), out temp) && temp == 0))
                            dataGridView.Rows[i].Cells[columNames[j]].Style = specialStyle;
                        else
                            dataGridView.Rows[i].Cells[columNames[j]].Style = normalStyle;
                    }
                }

            }
            else
            {
                for (int j = 0; j < columNames.Length; j++)
                {
                    dataGridView.Columns[columNames[j]].DefaultCellStyle = normalStyle;
                    dataGridView.Columns[columNames[j]].ReadOnly = true;
                }
            }
        }

        /// <summary>
        /// 设置单元格文本对齐方式为居中
        /// </summary>
        /// <param name="indexs">指定列的列数（从0开始）</param>
        public static void SetAlignWithCenter(DataGridView dataGridView, int[] indexs)
        {
            DataGridViewCellStyle style = new DataGridViewCellStyle();
            style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            for (int j = 0; j < indexs.Length; j++)
                dataGridView.Columns[indexs[j]].DefaultCellStyle = style;
        }

        /// <summary>
        /// 获取默认单元格样式(不包含表头)
        /// </summary>
        public static DataGridViewCellStyle GetCellStyle()
        {
            return new DataGridViewCellStyle()
            {
                Alignment = DataGridViewContentAlignment.MiddleLeft,
                Font = new System.Drawing.Font("宋体", DefaultCellFontSize, System.Drawing.FontStyle.Regular)
            };
        }

        /// <summary>
        /// 设置单元格文本对齐方式为居中
        /// </summary>
        /// <param name="indexs">指定列的列数（从0开始）</param>
        public static void SetAlignWithCenter(DataGridView dataGridView, string[] indexs)
        {
            DataGridViewCellStyle style = new DataGridViewCellStyle();
            style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            for (int j = 0; j < indexs.Length; j++)
                dataGridView.Columns[indexs[j]].DefaultCellStyle = style;
        }

        /// <summary>
        /// 设置二级标题
        /// </summary>
        /// <param name="dgv_WorkingLog"></param>
        /// <param name="tv"></param>
        public static void SetTreeViewHeader(HeaderUnitView dgv_WorkingLog, TreeView tv)
        {
            dgv_WorkingLog.ColumnTreeView = new TreeView[] { tv };
            dgv_WorkingLog.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgv_WorkingLog.ColumnDeep = 2;
            dgv_WorkingLog.CellHeight = 25;
            dgv_WorkingLog.ColumnHeadersHeight = 50;
            dgv_WorkingLog.RefreshAtHscroll = true;
        }

        /// <summary>
        /// 重置表格
        /// </summary>
        /// <param name="dataGridView">待重置的表格</param>
        public static void ResetDataGridView(HeaderUnitView dataGridView)
        {
            dataGridView.DataSource = null;
            dataGridView.Rows.Clear();
            dataGridView.Columns.Clear();
            dataGridView.ColumnTreeView = null;
            dataGridView.ColumnDeep = 1;
        }

        /// <summary>
        /// 重置表格
        /// </summary>
        /// <param name="dataGridView">待重置的表格</param>
        /// <param name="dataGridView">是否包括表头</param>
        public static void ResetDataGridView(DataGridView dataGridView, bool includeHeader)
        {
            dataGridView.DataSource = null;
            dataGridView.Rows.Clear();
            if(includeHeader)
                dataGridView.Columns.Clear();
        }
    }
}
