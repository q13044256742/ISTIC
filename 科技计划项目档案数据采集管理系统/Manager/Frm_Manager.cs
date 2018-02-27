using System;
using System.Data;
using System.Windows.Forms;

namespace 科技计划项目档案数据采集管理系统.Manager
{
    public partial class Frm_Manager : Form
    {
        public Frm_Manager(string name)
        {
            InitializeComponent();
            InitialForm(name);
        }

        private void InitialForm(string name)
        {
            string querySql = $"SELECT dd_id, dd_name as 名称,dd_note as 描述,dd_sort as 排序 from  data_dictionary where dd_pId='{name}' order by dd_sort";
            DataTable dataTable = SqlHelper.ExecuteQuery(querySql);
            dgv_DataList.DataSource = dataTable;

            dgv_DataList.Columns["dd_id"].Visible = false;
            //当前列表的pid
            dgv_DataList.Tag = name;
        }

        //查询
        private void btn_Search_Click(object sender, EventArgs e)
        {
            int index = cbo_SearchType.SelectedIndex;
            string searchKey = txt_SearchKey.Text;
            string queryKey = string.Empty;/*查询条件*/
            if (index == 0) {
                queryKey = "dd_name";
            }
            string tag = (dgv_DataList.Tag).ToString();

            if (!string.IsNullOrEmpty(queryKey)) {
                if (!string.IsNullOrEmpty(searchKey))
                {
                    string querySql = $"select dd_id,dd_name as 名称,dd_note as 描述,dd_sort as 排序 from data_dictionary" +
                   $" where {queryKey} like '%" + searchKey + "%' and dd_pId='" + tag + "' order by dd_sort";
                    dgv_DataList.DataSource = SqlHelper.ExecuteQuery(querySql);
                    dgv_DataList.Columns["dd_id"].Visible = false;
                }
                else {
                    string querySql = $"select dd_id,dd_name as 名称,dd_note as 描述,dd_sort as 排序 from data_dictionary" +
                   $" where dd_pId='" + tag + "' order by dd_sort";
                    dgv_DataList.DataSource = SqlHelper.ExecuteQuery(querySql);
                    dgv_DataList.Columns["dd_id"].Visible = false;
                }
            }
           
            dgv_DataList.Tag = tag;
        }

        //添加
        private void Btn_Add_Click(object sender, EventArgs e)
        {
            txt_SearchKey.Text = null;
            //获取当前列表的pId
            string pId = dgv_DataList.Tag == null ? string.Empty : dgv_DataList.Tag.ToString();
            Frm_Add frm = new Frm_Add(true, pId, null);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadZDDataScoure(pId);
            }
        }

        //加载添加或者更改后的列表】
        private void LoadZDDataScoure(string pId)
        {
            if (!string.IsNullOrEmpty(pId))
            {
                string querySql = $"SELECT dd_id, dd_name as 名称,dd_note as 描述,dd_sort as 排序 from  data_dictionary where dd_pId='{pId}' order by dd_sort ";
                dgv_DataList.DataSource = SqlHelper.ExecuteQuery(querySql);
                dgv_DataList.Columns["dd_id"].Visible = false;
            }
            button1.Enabled = true;       
        }
      
        //删除
        private void btn_del(object sender, EventArgs e)
        {
            int amount = dgv_DataList.SelectedRows.Count;
            if (amount > 0)
            {
               
                if (MessageBox.Show("确定要删除选中的数据吗?", "确认提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk) == DialogResult.OK)
                {
                    int deleteAmount = 0;                  
                    foreach (DataGridViewRow row in dgv_DataList.SelectedRows)
                    {
                        string id = row.Cells["dd_id"].Value.ToString();
                        string sql = $"select count(*) from data_dictionary where dd_pId ='{id}'";
                        object[] _obj = SqlHelper.ExecuteRowsQuery(sql);
                        if (_obj.Length == 0)
                        {
                            string deleteSql = $"DELETE FROM data_dictionary WHERE dd_id = '{id}'";
                            SqlHelper.ExecuteNonQuery(deleteSql);
                        }
                        else {
                            string pid_delSql = $"DELETE FROM data_dictionary WHERE dd_pId = '{id}'";
                            SqlHelper.ExecuteNonQuery(pid_delSql);
                            string deleteSql = $"DELETE FROM data_dictionary WHERE dd_id = '{id}'";                                                      
                            SqlHelper.ExecuteNonQuery(deleteSql);
                        }                      
                        deleteAmount++;                      
                    }
                    //获取当前列表的pId
                    string pId = dgv_DataList.Tag == null ? string.Empty : dgv_DataList.Tag.ToString();
                    LoadZDDataScoure(pId);
                    MessageBox.Show(deleteAmount + "条数据已被删除!", "操作成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txt_SearchKey.Text = null;
                }
            }
            else
            {
                MessageBox.Show("请先至少选择一条要删除的数据!", "尚未选择数据", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }

        //列表名称点击事件
        private void dgv_DataList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1 && e.ColumnIndex != -1)
            {
                if ("名称".Equals(dgv_DataList.Columns[e.ColumnIndex].Name))
                {
                    txt_SearchKey.Text = null;
                    string pid = dgv_DataList.Rows[e.RowIndex].Cells["dd_id"].Value.ToString();
                    string querySql = $"SELECT dd_id, dd_name as 名称,dd_note as 描述,dd_sort as 排序 from  data_dictionary where dd_pId='{pid}' order by dd_sort";
                    DataTable dataTable = SqlHelper.ExecuteQuery(querySql);                
                    dgv_DataList.DataSource = dataTable;                                        
                    dgv_DataList.Columns["dd_id"].Visible = false;
                    //按钮是否显示
                    button1.Enabled = true;
                    //当前列表的pid
                    dgv_DataList.Tag = pid;
                }
            }
        }

        //修改
        private void Btn_updateClick(object sender, EventArgs e)
        {          
            int amount = dgv_DataList.SelectedRows.Count;
            if (amount == 1)
            {                               
                //获取你所选行的id
                string id = (dgv_DataList.SelectedRows[0]).Cells["dd_id"].Value.ToString();
                //获取当前列表的pId
                string pId = dgv_DataList.Tag == null ? string.Empty : dgv_DataList.Tag.ToString();              
                Frm_Add frm = new Frm_Add(false, pId, id);
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    LoadZDDataScoure(pId);
                    txt_SearchKey.Text = null;
                }
            }
            else
                {
                    MessageBox.Show("请先选择一条要修改的数据!", "尚未选择数据", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
            }

        //返回
        private void Btn_backClick(object sender, EventArgs e)
        {
            //获取上级列表的id == 当前列表的pId 
            string id = dgv_DataList.Tag == null ? string.Empty : dgv_DataList.Tag.ToString();
            //根据id找到上级列表的pId            
            string querySql = $"SELECT dd_pId FROM data_dictionary where dd_id = '{id}'";
            string pId = (SqlHelper.ExecuteOnlyOneQuery(querySql)).ToString();

            LoadZDDataScoure(pId);
            dgv_DataList.Tag = pId;

            string sql = $"SELECT level FROM data_dictionary where dd_id = '{pId}'";
            string b = (SqlHelper.ExecuteOnlyOneQuery(sql)).ToString();
               
            if ( b == "1") {
                button1.Enabled = false;
            }

            txt_SearchKey.Text = null;
        }
    }
    
}
