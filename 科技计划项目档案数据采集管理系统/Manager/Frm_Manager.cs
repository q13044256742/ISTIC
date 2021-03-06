﻿using DevExpress.XtraEditors;
using System;
using System.Data;
using System.Windows.Forms;

namespace 科技计划项目档案数据采集管理系统.Manager
{
    public partial class Frm_Manager : XtraForm
    {
        public Frm_Manager(object id)
        {
            InitializeComponent();
            InitialForm(id);
        }

        private void InitialForm(object id)
        {
            LoadTableInfo(id);
            //当前列表的pid
            dgv_DataList.Tag = id;
        }

        //查询
        private void btn_Search_Click(object sender, EventArgs e)
        {
            string key = txt_SearchKey.Text;
            if (!string.IsNullOrEmpty(key))
            {
                dgv_DataList.ClearSelection();
                foreach (DataGridViewRow row in dgv_DataList.Rows)
                {
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        if (ToolHelper.GetValue(cell.Value).Contains(key))
                        {
                            cell.Selected = true;
                            return;
                        }
                    }
                }
            }
        }

        //添加
        private void Btn_Add_Click(object sender, EventArgs e)
        {
            txt_SearchKey.Text = null;
            //获取当前列表的pId
            string pId = GetValue(dgv_DataList.Tag);
            Frm_Add frm = new Frm_Add(true, pId, null, dgv_DataList.RowCount + 1);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadZDDataScoure(pId);
            }
        }

        private string GetValue(object value) => value == null ? string.Empty : value.ToString();

        //加载添加或者更改后的列表
        private void LoadZDDataScoure(string pId)
        {
            if (!string.IsNullOrEmpty(pId))
            {
                LoadTableInfo(pId);
            }

            btn_Back.Enabled = true;       
        }
      
        //删除
        private void Btn_Delete(object sender, EventArgs e)
        {
            int amount = dgv_DataList.SelectedRows.Count;
            if (amount > 0)
            {
               
                if (XtraMessageBox.Show("确定要删除选中的数据吗?", "确认提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk) == DialogResult.OK)
                {
                    int deleteAmount = 0;
                    string ids = string.Empty;
                    foreach(DataGridViewRow row in dgv_DataList.SelectedRows)
                    {
                        object id = row.Cells["id"].Value;
                        int count = SqlHelper.ExecuteCountQuery($"SELECT COUNT(dd_id) FROM data_dictionary WHERE dd_pId ='{id}'");
                        if(count == 0)
                        {
                            ids += $"'{id}',";
                            deleteAmount++;
                        }
                        else
                            XtraMessageBox.Show($"编号【{row.Cells["code"]}】下存在子节点，无法删除。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                    if(!string.IsNullOrEmpty(ids))
                    {
                        ids = ids.Substring(0, ids.Length - 1);
                        SqlHelper.ExecuteNonQuery($"DELETE FROM data_dictionary WHERE dd_id IN ({ids})");
                    }
                    //获取当前列表的pId
                    string pId = GetValue(dgv_DataList.Tag);
                    XtraMessageBox.Show(deleteAmount + "条数据已被删除!", "操作成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txt_SearchKey.Text = null;
                    LoadZDDataScoure(pId);
                }
            }
            else
            {
                XtraMessageBox.Show("请先至少选择一条要删除的数据!", "尚未选择数据", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }

        //修改
        private void Btn_updateClick(object sender, EventArgs e)
        {
            int amount = dgv_DataList.SelectedRows.Count;
            if(amount == 1)
            {
                //获取你所选行的id
                object id = dgv_DataList.SelectedRows[0].Cells["id"].Value;
                //获取当前列表的pId
                string pId = GetValue(dgv_DataList.Tag);
                Frm_Add frm = new Frm_Add(false, pId, id, dgv_DataList.RowCount + 1);
                if(frm.ShowDialog() == DialogResult.OK)
                {
                    LoadZDDataScoure(pId);
                    txt_SearchKey.Text = null;
                }
            }
            else
            {
                XtraMessageBox.Show("请先选择一条要修改的数据!", "尚未选择数据", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }

        //返回
        private void Btn_backClick(object sender, EventArgs e)
        {
            //获取上级列表的id == 当前列表的pId 
            string id = dgv_DataList.Tag == null ? string.Empty : dgv_DataList.Tag.ToString();
            //根据id找到上级列表的pId            
            string querySql = $"SELECT dd_pId FROM data_dictionary WHERE dd_id = '{id}'";
            string pId = (SqlHelper.ExecuteOnlyOneQuery(querySql)).ToString();

            LoadZDDataScoure(pId);
            dgv_DataList.Tag = pId;

            string sql = $"SELECT level FROM data_dictionary WHERE dd_id = '{pId}'";
            string b = GetValue(SqlHelper.ExecuteOnlyOneQuery(sql));
            if(b == "1")
            {
                btn_Back.Enabled = false;
            }
            txt_SearchKey.Text = null;
        }

        private void Dgv_DataList_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if(e.RowIndex != -1 && e.ColumnIndex != -1)
            {
                object pid = dgv_DataList.Rows[e.RowIndex].Cells["id"].Value;
                LoadTableInfo(pid);

                txt_SearchKey.Text = null;
                btn_Back.Enabled = true;
                dgv_DataList.Tag = pid;
            }
        }

        private void LoadTableInfo(object parentId)
        {
            string code = ToolHelper.GetValue(SqlHelper.ExecuteOnlyOneQuery($"SELECT dd_code FROM data_dictionary WHERE dd_id='{parentId}'"));
            if(!string.IsNullOrEmpty(code))
            {
                if(code.EndsWith("_JD"))
                {
                    dgv_DataList.CellMouseDown += Dgv_DataList_CellMouseDown;
                    cms_move.DropDownItems.Clear();

                    object[] names = SqlHelper.ExecuteSingleColumnQuery($"SELECT dd_name FROM data_dictionary WHERE dd_pId='{parentId}' " +
                        $"AND extend_4 IS NULL AND dd_name <> '其他' ORDER BY dd_name");
                    foreach(object item in names)
                        cms_move.DropDownItems.Add(ToolHelper.GetValue(item)).Click += MoveItem_Click;
                }
                else
                    dgv_DataList.CellMouseDown -= Dgv_DataList_CellMouseDown;
            }

            dgv_DataList.Rows.Clear();
            DataTable table = SqlHelper.ExecuteQuery($"SELECT dd_id, dd_name, dd_code, dd_note, extend_3, dd_sort FROM  data_dictionary WHERE dd_pId='{parentId}' ORDER BY dd_sort");
            foreach(DataRow row in table.Rows)
            {
                int index = dgv_DataList.Rows.Add();
                dgv_DataList.Rows[index].Tag = parentId;
                dgv_DataList.Rows[index].Cells["id"].Value = row["dd_id"];
                dgv_DataList.Rows[index].Cells["name"].Value = row["dd_name"];
                dgv_DataList.Rows[index].Cells["code"].Value = row["dd_code"];
                dgv_DataList.Rows[index].Cells["note"].Value = row["dd_note"];
                dgv_DataList.Rows[index].Cells["extend_3"].Value = row["extend_3"];
                dgv_DataList.Rows[index].Cells["sort"].Value = row["dd_sort"];
            }
        }

        private void MoveItem_Click(object sender, EventArgs e)
        {
            if(dgv_DataList.SelectedRows.Count <= 0) return;
            DataGridViewRow row = dgv_DataList.SelectedRows[0];
            object scode = row.Cells["name"].Value;
            object dcode = (sender as ToolStripItem).Text;
            if(!scode.Equals(dcode))
            {
                if(XtraMessageBox.Show($"是否确认将{scode}的所有文件合并至{dcode}？", "确认提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    object pid = row.Tag;
                    object id = row.Cells["id"].Value;
                    string updateSQL = $"UPDATE processing_file_list SET pfl_categor=" +
                        $"(SELECT dd_id FROM data_dictionary WHERE dd_pId='{pid}' AND dd_name='{dcode}') WHERE pfl_categor='{id}';";

                    SqlHelper.ExecuteNonQuery(updateSQL);

                    XtraMessageBox.Show("操作成功");
                }
            }
        }

        private void Dgv_DataList_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if(e.Button == MouseButtons.Right)
            {
                dgv_DataList.ClearSelection();
                dgv_DataList.Rows[e.RowIndex].Selected = true;
                contextMenuStrip1.Show(MousePosition);
            }
        }

        private void Dgv_DataList_ColumnHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            Btn_updateClick(sender, e);
        }
    }
    
}
