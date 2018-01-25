using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace 科技计划项目档案数据采集管理系统
{
    public partial class Frm_MyWork : Form
    {
        /// <summary>
        /// 开始加工指定的对象
        /// </summary>
        /// <param name="workType">对象类型</param>
        /// <param name="objId">对象主键</param>
        /// <param name="planId">计划主键（仅针对光盘/批次加工）</param>
        public Frm_MyWork(WorkType workType, object objId, object planId)
        {
            InitializeComponent();
            InitialForm(planId);
        }

        List<TabPage> tabList = new List<TabPage>();
        /// <summary>
        /// 初始化选项卡
        /// </summary>
        /// <param name="planId">计划ID（仅针对纸本/光盘加工）</param>
        private void InitialForm(object planId)
        {
            foreach (TabPage tab in tab_MenuList.TabPages)
            {
                tabList.Add(tab);
                tab_MenuList.TabPages.Remove(tab);
            }
            LoadTreeList(planId);
        }

        /// <summary>
        /// 加载计划页面
        /// </summary>
        /// <param name="planId">计划主键</param>
        private void LoadPlanPage(object planId)
        {
            object[] _obj = SqlHelper.ExecuteRowsQuery($"SELECT dd_id,dd_name,dd_note FROM data_dictionary WHERE dd_id='{planId}'");
            lbl_JH_Name.Text = _obj[1].ToString();
            lbl_JH_Name.Tag = _obj[0];
            lbl_PlanIntroducation.Text = _obj[2].ToString();

            LoadFileList(dgv_JH_FileList, string.Empty, planId);
        }

        /// <summary>
        /// 加载指定表格的文件列表
        /// </summary>
        /// <param name="dataGridView">表格控件</param>
        /// <param name="key">列名关键字</param>
        /// <param name="parentId">所属对象ID</param>
        private void LoadFileList(DataGridView dataGridView, string key, object parentId)
        {
            dataGridView.Rows.Clear();
            string querySql = $"SELECT * FROM processing_file_list WHERE pfl_obj_id='{parentId}'";
            DataTable dataTable = SqlHelper.ExecuteQuery(querySql);
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                int index = dgv_JH_FileList.Rows.Add();
                dataGridView.Rows[index].Cells[key + "id"].Value = i + 1;
                dataGridView.Rows[index].Cells[key + "id"].Tag = dataTable.Rows[i]["pfl_id"];
                dataGridView.Rows[index].Cells[key + "stage"].Value = dataTable.Rows[i]["pfl_stage"];
                dataGridView.Rows[index].Cells[key + "categor"].Value = dataTable.Rows[i]["pfl_categor"];
                dataGridView.Rows[index].Cells[key + "name"].Value = dataTable.Rows[i]["pfl_filename"];
                dataGridView.Rows[index].Cells[key + "user"].Value = dataTable.Rows[i]["pfl_user"];
                dataGridView.Rows[index].Cells[key + "type"].Value = dataTable.Rows[i]["pfl_type"];
                dataGridView.Rows[index].Cells[key + "secret"].Value = dataTable.Rows[i]["pfl_scert"];
                dataGridView.Rows[index].Cells[key + "page"].Value = dataTable.Rows[i]["pfl_page_amount"];
                dataGridView.Rows[index].Cells[key + "amount"].Value = dataTable.Rows[i]["pfl_amount"];
                dataGridView.Rows[index].Cells[key + "date"].Value = dataTable.Rows[i]["pfl_complete_date"];
                dataGridView.Rows[index].Cells[key + "unit"].Value = dataTable.Rows[i]["pfl_save_location"];
                dataGridView.Rows[index].Cells[key + "carrier"].Value = dataTable.Rows[i]["pfl_carrier"];
                dataGridView.Rows[index].Cells[key + "format"].Value = dataTable.Rows[i]["pfl_file_format"];
                dataGridView.Rows[index].Cells[key + "form"].Value = dataTable.Rows[i]["pfl_file_form"];
                dataGridView.Rows[index].Cells[key + "link"].Value = dataTable.Rows[i]["pfl_file_link"];
                dataGridView.Rows[index].Cells[key + "remark"].Value = dataTable.Rows[i]["pfl_remark"];
            }
        }

        /// <summary>
        /// 添加树节点
        /// </summary>
        /// <param name="nodeName">节点</param>
        /// <param name="parentNodeName">父节点名称</param>
        private void AddTreeNode(TreeNode treeNode, string parentNodeName)
        {
            if(string.IsNullOrEmpty(parentNodeName))
            {
                treeView.Nodes.Add(treeNode);
            }
            else
            {
                TreeNode[] ts = treeView.Nodes.Find(parentNodeName, false);
                if (ts.Length > 0)
                {
                    ts[0].Nodes.Add(treeNode);
                }
            }
        }

        /// <summary>
        /// 展示指定文本的tab页面（隐藏其他页面）
        /// </summary>
        /// <param name="name">要展示页面的Name</param>
        /// <param name="index">所属索引序列(删除大于指定索引的TabPages)</param>
        /// <param name="id">要展示页面的id</param>
        private void ShowTab(string name, int index, object id)
        {
            if (index == 0)
                tab_MenuList.TabPages.Clear();
            else
            {
                int amount = tab_MenuList.TabPages.Count;
                List<TabPage> removeList = new List<TabPage>();
                for (int i = 0; i < amount; i++)
                    if (i > index)
                        removeList.Add(tab_MenuList.TabPages[i]);
                if (removeList.Count > 0)
                    for (int i = 0; i < removeList.Count; i++)
                        tab_MenuList.TabPages.Remove(removeList[i]);
            }
            //根据指定name添加选项卡
            for (int i = 0; i < tabList.Count; i++)
                if (tabList[i].Name.Equals(name))
                {
                    tab_MenuList.TabPages.Add(tabList[i]);
                    break;
                }
        }

        /// <summary>
        /// 窗体初始化
        /// </summary>
        private void Frm_MyWork_Load(object sender, EventArgs e)
        {
            //设置表格样式
            dgv_JH_FileList.ColumnHeadersDefaultCellStyle = DataGridViewStyleHelper.GetHeaderStyle();
            dgv_JH_FileList.DefaultCellStyle = DataGridViewStyleHelper.GetCellStyle();
            dgv_JH_FileValid.ColumnHeadersDefaultCellStyle = DataGridViewStyleHelper.GetHeaderStyle();

            dgv_JH_XM_FileList.ColumnHeadersDefaultCellStyle = DataGridViewStyleHelper.GetHeaderStyle();
            dgv_JH_XM_FileList.DefaultCellStyle = DataGridViewStyleHelper.GetCellStyle();
            dgv_JH_XM_FileValid.ColumnHeadersDefaultCellStyle = DataGridViewStyleHelper.GetHeaderStyle();

            //阶段
            DataGridViewComboBoxColumn satgeColumn1 = dgv_JH_FileList.Columns["stage"] as DataGridViewComboBoxColumn;
            satgeColumn1.DataSource = DictionaryHelper.GetTableByCode("dic_file_jd");
            satgeColumn1.DisplayMember = "dd_name";
            satgeColumn1.ValueMember = "dd_id";
            satgeColumn1.DefaultCellStyle = new DataGridViewCellStyle() { Font = new System.Drawing.Font("宋体", 10.5f) };

            DataGridViewComboBoxColumn satgeColumn2 = dgv_JH_XM_FileList.Columns["jh_xm_stage"] as DataGridViewComboBoxColumn;
            satgeColumn2.DataSource = DictionaryHelper.GetTableByCode("dic_file_jd");
            satgeColumn2.DisplayMember = "dd_name";
            satgeColumn2.ValueMember = "dd_id";
            satgeColumn2.DefaultCellStyle = new DataGridViewCellStyle() { Font = new System.Drawing.Font("宋体", 10.5f) };



            //文件类别
            for (int i = 0; i < dgv_JH_FileList.Rows.Count; i++)
                if (dgv_JH_FileList.Rows[i].Cells["id"].Value != null)
                {
                    DataGridViewComboBoxCell satgeCell = (DataGridViewComboBoxCell)dgv_JH_FileList.Rows[i].Cells["stage"];
                    object stageId = satgeCell.Value;
                    if (stageId != null)
                        SetCategorByStage(stageId, dgv_JH_FileList.Rows[i],string.Empty);
                }
            for (int i = 0; i < dgv_JH_XM_FileList.Rows.Count; i++)
                if (dgv_JH_XM_FileList.Rows[i].Cells["jh_xm_id"].Value != null)
                {
                    DataGridViewComboBoxCell satgeCell = (DataGridViewComboBoxCell)dgv_JH_XM_FileList.Rows[i].Cells["jh_xm_stage"];
                    object stageId = satgeCell.Value;
                    if (stageId != null)
                        SetCategorByStage(stageId, dgv_JH_FileList.Rows[i], "jh_xm_");
                }

            //文件类型
            DataGridViewComboBoxColumn filetypeColumn = dgv_JH_FileList.Columns["type"] as DataGridViewComboBoxColumn;
            filetypeColumn.DataSource = DictionaryHelper.GetTableByCode("dic_file_type");
            filetypeColumn.DisplayMember = "dd_name";
            filetypeColumn.ValueMember = "dd_id";
            filetypeColumn.DefaultCellStyle = new DataGridViewCellStyle() { Font = new System.Drawing.Font("宋体", 10.5f) };

            DataGridViewComboBoxColumn filetypeColumn2 = dgv_JH_XM_FileList.Columns["jh_xm_type"] as DataGridViewComboBoxColumn;
            filetypeColumn2.DataSource = DictionaryHelper.GetTableByCode("dic_file_type");
            filetypeColumn2.DisplayMember = "dd_name";
            filetypeColumn2.ValueMember = "dd_id";
            filetypeColumn2.DefaultCellStyle = new DataGridViewCellStyle() { Font = new System.Drawing.Font("宋体", 10.5f) };

            //密级
            DataGridViewComboBoxColumn secretColumn = dgv_JH_FileList.Columns["secret"] as DataGridViewComboBoxColumn;
            secretColumn.DataSource = DictionaryHelper.GetTableByCode("dic_file_mj");
            secretColumn.DisplayMember = "dd_name";
            secretColumn.ValueMember = "dd_id";
            secretColumn.DefaultCellStyle = new DataGridViewCellStyle() { Font = new System.Drawing.Font("宋体", 10.5f) };

            DataGridViewComboBoxColumn secretColumn2 = dgv_JH_XM_FileList.Columns["jh_xm_secret"] as DataGridViewComboBoxColumn;
            secretColumn2.DataSource = DictionaryHelper.GetTableByCode("dic_file_mj");
            secretColumn2.DisplayMember = "dd_name";
            secretColumn2.ValueMember = "dd_id";
            secretColumn2.DefaultCellStyle = new DataGridViewCellStyle() { Font = new System.Drawing.Font("宋体", 10.5f) };

            //载体
            DataGridViewComboBoxColumn carrierColumn = dgv_JH_FileList.Columns["carrier"] as DataGridViewComboBoxColumn;
            carrierColumn.DataSource = DictionaryHelper.GetTableByCode("dic_file_zt");
            carrierColumn.DisplayMember = "dd_name";
            carrierColumn.ValueMember = "dd_id";
            carrierColumn.DefaultCellStyle = new DataGridViewCellStyle() { Font = new System.Drawing.Font("宋体", 10.5f) };

            DataGridViewComboBoxColumn carrierColumn2 = dgv_JH_XM_FileList.Columns["jh_xm_carrier"] as DataGridViewComboBoxColumn;
            carrierColumn2.DataSource = DictionaryHelper.GetTableByCode("dic_file_zt");
            carrierColumn2.DisplayMember = "dd_name";
            carrierColumn2.ValueMember = "dd_id";
            carrierColumn2.DefaultCellStyle = new DataGridViewCellStyle() { Font = new System.Drawing.Font("宋体", 10.5f) };

            //文件格式
            DataGridViewComboBoxColumn formatColumn = dgv_JH_FileList.Columns["format"] as DataGridViewComboBoxColumn;
            formatColumn.DataSource = DictionaryHelper.GetTableByCode("dic_file_format");
            formatColumn.DisplayMember = "dd_name";
            formatColumn.ValueMember = "dd_id";
            formatColumn.DefaultCellStyle = new DataGridViewCellStyle() { Font = new System.Drawing.Font("宋体", 10.5f) };

            DataGridViewComboBoxColumn formatColumn2 = dgv_JH_XM_FileList.Columns["jh_xm_format"] as DataGridViewComboBoxColumn;
            formatColumn2.DataSource = DictionaryHelper.GetTableByCode("dic_file_format");
            formatColumn2.DisplayMember = "dd_name";
            formatColumn2.ValueMember = "dd_id";
            formatColumn2.DefaultCellStyle = new DataGridViewCellStyle() { Font = new System.Drawing.Font("宋体", 10.5f) };

            //文件形态
            DataGridViewComboBoxColumn formColumn = dgv_JH_FileList.Columns["form"] as DataGridViewComboBoxColumn;
            formColumn.DataSource = DictionaryHelper.GetTableByCode("dic_file_state");
            formColumn.DisplayMember = "dd_name";
            formColumn.ValueMember = "dd_id";
            formColumn.DefaultCellStyle = new DataGridViewCellStyle() { Font = new System.Drawing.Font("宋体", 10.5f) };

            DataGridViewComboBoxColumn formColumn2 = dgv_JH_XM_FileList.Columns["jh_xm_form"] as DataGridViewComboBoxColumn;
            formColumn2.DataSource = DictionaryHelper.GetTableByCode("dic_file_state");
            formColumn2.DisplayMember = "dd_name";
            formColumn2.ValueMember = "dd_id";
            formColumn2.DefaultCellStyle = new DataGridViewCellStyle() { Font = new System.Drawing.Font("宋体", 10.5f) };

            cbo_JH_Next.SelectedIndex = 0;
            cbo_JH_XM_HasNext.SelectedIndex = 0;
            cbo_JH_XM_KT_HasNext.SelectedIndex = 0;
        }

        /// <summary>
        /// 根据阶段设置相应的文件类别
        /// </summary>
        /// <param name="jdId">阶段ID</param>
        private void SetCategorByStage(object jdId, DataGridViewRow dataGridViewRow, string key)
        {
            //文件类别
            DataGridViewComboBoxCell categorCell = dataGridViewRow.Cells[key + "categor"] as DataGridViewComboBoxCell;

            string querySql = $"SELECT dd_id, dd_name FROM data_dictionary WHERE dd_pId='{jdId}'";
            categorCell.DataSource = SqlHelper.ExecuteQuery(querySql);
            categorCell.DisplayMember = "dd_name";
            categorCell.ValueMember = "dd_id";
            categorCell.Style = new DataGridViewCellStyle() { Font = new System.Drawing.Font("宋体", 10.5f), NullValue = categorCell.Items[0] };
        }

        //单元格事件绑定
        private void Dgv_File_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            DataGridView dataGridView = sender as DataGridView;
            if ("dgv_JH_FileList".Equals(dataGridView.Name))
            {
                string columnName = dgv_JH_FileList.CurrentCell.OwningColumn.Name;
                Control con = e.Control;
                con.Tag = "jh";
                if ("stage".Equals(columnName))
                    (con as ComboBox).SelectionChangeCommitted += new EventHandler(StageComboBox_SelectionChangeCommitted);
                else if ("categor".Equals(columnName))
                    (con as ComboBox).SelectionChangeCommitted += new EventHandler(CategorComboBox_SelectionChangeCommitted);
            }
            else if ("dgv_JH_XM_FileList".Equals(dataGridView.Name))
            {
                string columnName = dgv_JH_XM_FileList.CurrentCell.OwningColumn.Name;
                Control con = e.Control;
                con.Tag = "jh_xm";
                if ("jh_xm_stage".Equals(columnName))
                    (con as ComboBox).SelectionChangeCommitted += new EventHandler(StageComboBox_SelectionChangeCommitted);
                else if ("jh_xm_categor".Equals(columnName))
                    (con as ComboBox).SelectionChangeCommitted += new EventHandler(CategorComboBox_SelectionChangeCommitted);
            }
        }

        /// <summary>
        /// 文件阶段 下拉事件
        /// </summary>
        private void StageComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;
            if ("jh".Equals(comboBox.Tag))
                SetCategorByStage(comboBox.SelectedValue, dgv_JH_FileList.CurrentRow,string.Empty);
            else if ("jh_xm".Equals(comboBox.Tag))
                SetCategorByStage(comboBox.SelectedValue, dgv_JH_XM_FileList.CurrentRow, "jh_xm_");
            comboBox.Leave += new EventHandler(delegate (object obj, EventArgs eve)
            {
                ComboBox _comboBox = obj as ComboBox;
                _comboBox.SelectionChangeCommitted -= new EventHandler(StageComboBox_SelectionChangeCommitted);
            });
        }

        /// <summary>
        /// 文件类别 下拉事件
        /// </summary>
        private void CategorComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;
            if ("jh".Equals(comboBox.Tag))
                SetNameByCategor(comboBox.SelectedValue, dgv_JH_FileList.CurrentRow, string.Empty);
            else if ("jh_xm".Equals(comboBox.Tag))
                SetNameByCategor(comboBox.SelectedValue, dgv_JH_XM_FileList.CurrentRow, "jh_xm_");
            comboBox.Leave += new EventHandler(delegate (object obj, EventArgs eve)
            {
                ComboBox _comboBox = obj as ComboBox;
                _comboBox.SelectionChangeCommitted -= new EventHandler(CategorComboBox_SelectionChangeCommitted);
            });
        }

        /// <summary>
        /// 根据文件类别设置文件名称
        /// </summary>
        /// <param name="catogerCode">文件类别编号</param>
        /// <param name="currentRow">当前行</param>
        private void SetNameByCategor(object catogerId, DataGridViewRow currentRow, string key)
        {
            string value = GetValue(SqlHelper.ExecuteOnlyOneQuery($"SELECT dd_note FROM data_dictionary WHERE dd_id='{catogerId}'"));
            currentRow.Cells[key + "name"].Value = value;
        }


        //表单错误处理
        private void Dgv_JH_File_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
           
        }

        private void Btn_OpenFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = false;
            if(openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string path = openFileDialog.FileName;
                System.Diagnostics.Process.Start("Explorer.exe", path);
            }
        }

        //计划 - 下一级Combox切换
        private void Cbo_JH_Next_SelectionChangeCommitted(object sender, EventArgs e)
        {
            int index = cbo_JH_Next.SelectedIndex;
            if(index == 0)//无
            {
                ShowTab(null, 0, lbl_JH_Name.Tag);
                pal_JH_XM.Tag = null;
            }
            else if(index == 1)//父级 - 项目
            {
                //将当前计划的ID赋值给下一级
                pal_JH_XM.Tag = lbl_JH_Name.Tag;
                ShowTab("plan_project", 0, null);
            }
            else if (index == 2)//父级 - 课题
            {
                ShowTab("plan_topic", 0, null);
            }
        }

        private void Cbo_JH_XM_HasNext_SelectionChangeCommitted(object sender, EventArgs e)
        {
            int index = cbo_JH_XM_HasNext.SelectedIndex;
            if (index == 0)//无
            {
                ShowTab(null, 1, dgv_JH_XM_FileList.Tag);
            }
            else if (index == 1)//子级 - 课题
            {
                ShowTab("plan_project_topic", 1, null);
            }
        }
       
        private void Cbo_JH_XM_KT_HasNext_SelectionChangeCommitted(object sender, EventArgs e)
        {
            int index = cbo_JH_XM_KT_HasNext.SelectedIndex;
            if (index == 0)//无
            {
                ShowTab(null, 2, null);
            }
            else if (index == 1)//子级 - 子课题
            {
                ShowTab("plan_project_topic_subtopic", 2,null);
            }
        }
        //
        private void Cbo_JH_KT_HasNext_SelectionChangeCommitted(object sender, EventArgs e)
        {
            int index = cbo_JH_KT_HasNext.SelectedIndex;
            if (index == 0)//无
            {
                ShowTab(null, 1, null);
            }
            else if (index == 1)//子级 - 子课题
            {
                ShowTab("plan_topic_subtopic", 1, null);
            }
        }

        /// <summary>
        /// 保存事件
        /// </summary>
        private void Btn_Save_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;
            //计划
            if ("btn_JH_Save".Equals(button.Name))
            {
                int maxLength = dgv_JH_FileList.Rows.Count;
                if (maxLength > 1)
                {
                    for (int i = 0; i < maxLength - 1; i++)
                    {
                        DataGridViewRow row = dgv_JH_FileList.Rows[i];
                        object id = row.Cells["id"].Value;
                        if (id == null)//新增
                        {
                            object pflid = AddFileInfo(string.Empty, row, lbl_JH_Name.Tag);
                            row.Cells["id"].Value = row.Index + 1;
                            row.Cells["id"].Tag = pflid;
                        }
                        else//更新
                        {
                            object pflid = row.Cells["id"].Tag;
                            UpdateFileInfo(row);
                        }
                    }
                    MessageBox.Show("保存成功！");
                }
            }
            //计划-项目
            else if ("btn_JH_XM_Save".Equals(button.Name))
            {
                //根据主键是否存在判断是更新还是新增
                if (dgv_JH_XM_FileList.Tag != null)//更新
                    UpdateProjectBasicInfo(dgv_JH_XM_FileList.Tag);
                else//新增
                {
                    //保存基本信息
                    object pid = AddProjectBasicInfo(pal_JH_XM.Tag);
                    dgv_JH_XM_FileList.Tag = pid;
                }
                //保存文件列表
                int maxLength = dgv_JH_XM_FileList.Rows.Count;
                if (maxLength > 1)
                    for (int i = 0; i < maxLength - 1; i++)
                    {
                        DataGridViewRow row = dgv_JH_XM_FileList.Rows[i];
                        object id = row.Cells["jh_xm_id"].Value;
                        if (id == null)//新增
                        {
                            object pflid = AddFileInfo("jh_xm_", row, dgv_JH_XM_FileList.Tag);
                            row.Cells["jh_xm_id"].Value = row.Index + 1;
                            row.Cells["jh_xm_id"].Tag = pflid;
                        }
                        else//更新
                        {
                            object pflid = row.Cells["jh_xm_id"].Tag;
                            UpdateFileInfo(row);
                        }
                    }
                MessageBox.Show("操作成功！");
            }
            LoadTreeList(lbl_JH_Name.Tag);
        }

        /// <summary>
        /// 更新 项目/课题基本信息
        /// </summary>
        /// <param name="tag">主键</param>
        private void UpdateProjectBasicInfo(object tag)
        {
            string pid = Guid.NewGuid().ToString();
            string code = txt_JH_XM_Code.Text;
            string name = txt_JH_XM_Name.Text;
            string type = txt_JH_XM_JHType.Text;
            string ly = txt_JH_XM_LY.Text;
            string zt = txt_JH_XM_ZT.Text;
            string jf = txt_JH_XM_JF.Text;
            DateTime starttime = dtp_JH_XM_StartTime.Value;
            DateTime endtime = dtp_JH_XM_EndTime.Value;
            string year = txt_JH_XM_LXND.Text;
            string unit = txt_JH_XM_Unit.Text;
            string province = txt_JH_XM_Province.Text;
            string unituser = txt_JH_XM_UnitUser.Text;
            string objuser = txt_JH_XM_ObjUser.Text;
            string intro = txt_JH_XM_ObjIntroduct.Text;

            string updateSql = "UPDATE project_info SET " +
                $"pi_code = '{code}'" +
                $",pi_name = '{name}' " +
                $",pi_type = '{type}' " +
                $",pb_belong = '{ly}'" +
                $",pb_belong_type = '{zt}'" +
                $",pi_money = '{jf}'" +
                $",pi_start_datetime = '{starttime}'" +
                $",pi_end_datetime = '{endtime}'" +
                $",pi_year = '{year}'" +
                $",pi_company_id = '{unit}'" +
                $",pi_company_user = '{unituser}'" +
                $",pi_province = '{province}'" +
                $",pi_project_user = '{objuser}'" +
                $",pi_introduction = '{intro}'" +
                $" WHERE pi_id='{tag}'";

            SqlHelper.ExecuteNonQuery(updateSql);
        }

        /// <summary>
        /// 保存 计划-项目 基本信息
        /// </summary>
        /// <returns>主键</returns>
        private object AddProjectBasicInfo(object parentId)
        {
            string pid = Guid.NewGuid().ToString();
            string code = txt_JH_XM_Code.Text;
            string name = txt_JH_XM_Name.Text;
            string planType = txt_JH_XM_JHType.Text;
            string ly = txt_JH_XM_LY.Text;
            string zt = txt_JH_XM_ZT.Text;
            string jf = txt_JH_XM_JF.Text;
            DateTime starttime = dtp_JH_XM_StartTime.Value;
            DateTime endtime = dtp_JH_XM_EndTime.Value;
            string year = txt_JH_XM_LXND.Text;
            string unit = txt_JH_XM_Unit.Text;
            string province = txt_JH_XM_Province.Text;
            string unituser = txt_JH_XM_UnitUser.Text;
            string objuser = txt_JH_XM_ObjUser.Text;
            string intro = txt_JH_XM_ObjIntroduct.Text;

            string insertSql = "INSERT INTO [dbo].[project_info]([pi_id] ,[trc_id],[pi_code],[pi_name],[pi_type],[pb_belong]" +
                ",[pb_belong_type],[pi_money],[pi_start_datetime],[pi_end_datetime],[pi_year],[pi_company_id],[pi_company_user]" +
                ",[pi_province],[pi_project_user],[pi_introduction],[pi_work_status],[pi_obj_id])" +
                "VALUES" +
                $"('{pid}',null,'{code}','{name}','{planType}','{ly}','{zt}','{jf}','{starttime}'" +
                $",'{endtime}','{year}','{unit}','{unituser}'" +
                $",'{province}','{objuser}','{intro}','{(int)WorkStatus.Default}','{parentId}')";

            SqlHelper.ExecuteNonQuery(insertSql);
            return pid;
        }

        /// <summary>
        /// 更新文件信息
        /// </summary>
        private void UpdateFileInfo(DataGridViewRow row)
        {
            
        }

        /// <summary>
        /// 新增文件信息
        /// </summary>
        /// <param name="key">当前表格列名前缀</param>
        /// <param name="row">当前待保存的行</param>
        /// <param name="parentId">父对象ID</param>
        /// <returns>新增信息主键</returns>
        private object AddFileInfo(string key, DataGridViewRow row, object parentId)
        {
            object pflid = Guid.NewGuid().ToString();
            object stage = row.Cells[key + "stage"].Value;
            object categor = row.Cells[key + "categor"].Value;
            object name = row.Cells[key + "name"].Value;
            object user = row.Cells[key + "user"].Value;
            object type = row.Cells[key + "type"].Value;
            object secret = row.Cells[key + "secret"].Value;
            object page = row.Cells[key + "page"].Value;
            object amount = row.Cells[key + "amount"].Value;
            object date = row.Cells[key + "date"].Value;
            object unit = row.Cells[key + "unit"].Value;
            object carrier = row.Cells[key + "carrier"].Value;
            object format = row.Cells[key + "format"].Value;
            object form = row.Cells[key + "form"].Value;
            object link = row.Cells[key + "link"].Value;
            object remark = row.Cells[key + "remark"].Value;
            object objId = parentId;
            GuiDangStatus status = GuiDangStatus.NonGuiDang;

            string insertSql = $"INSERT INTO processing_file_list VALUES ('{pflid}' " +
                $",'{stage}','{categor}' ,'{name}' ,'{user}' " +
                $",'{type}' ,'{secret}' ,'{page}' ,'{amount}' ,'{date}' " +
                $",'{unit}' ,'{carrier}','{format}' ,'{form}','{objId}'" +
                $",'{link}' ,'{remark}' ,{(int)status} ,'kyo','{DateTime.Now}')";

            SqlHelper.ExecuteNonQuery(insertSql);
            return pflid;
        }

        /// <summary>
        /// 加载树
        /// </summary>
        /// <param name="planId">计划ID</param>
        private void LoadTreeList(object planId)
        {
            treeView.Nodes.Clear();

            object[] _obj = SqlHelper.ExecuteRowsQuery($"SELECT dd_id,dd_name FROM data_dictionary WHERE dd_id='{planId}'");
            TreeNode treeNode = new TreeNode()
            {
                Tag = "root",
                Name = _obj[0].ToString(),
                Text = _obj[1].ToString()
            };
            //根据【计划】查询【项目/课题】集
            List<object[]> list = SqlHelper.ExecuteColumnsQuery($"SELECT pi_id,pi_name FROM project_info WHERE pi_obj_id='{treeNode.Name}'", 2);
            for (int i = 0; i < list.Count; i++)
            {
                TreeNode treeNode2 = new TreeNode()
                {
                    Tag = "parent",
                    Name = list[i][0].ToString(),
                    Text = list[i][1].ToString()
                };
                treeNode.Nodes.Add(treeNode2);
                //根据【项目/课题】查询【课题/子课题】集
                List<object[]> list2 = SqlHelper.ExecuteColumnsQuery($"SELECT si_id,si_name FROM subject_info WHERE pi_id='{treeNode2.Name}'", 2);
                for (int j = 0; j < list2.Count; j++)
                {
                    TreeNode treeNode3 = new TreeNode()
                    {
                        Tag = "son",
                        Name = list[i][0].ToString(),
                        Text = list[i][1].ToString()
                    };
                    treeNode2.Nodes.Add(treeNode3);
                }
            }
            
            treeView.Nodes.Add(treeNode);
            treeView.ExpandAll();
            treeView.AfterSelect += TreeView_AfterSelect;
        }

        private void TreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            object index = e.Node.Tag;
            if ("root".Equals(index))
            {
                ShowTab("plan", 0, e.Node.Name);
                LoadPlanPage(e.Node.Name);
            }
            else if ("parent".Equals(index))
            {
                tab_MenuList.TabPages.Clear();
                ShowTab("plan", 0, e.Node.Parent);
                LoadPlanPage(e.Node.Parent.Name);

                ShowTab("plan_project", 1, e.Node.Name);
                LoadProjectPage(e.Node.Name);
            }
            tab_MenuList.SelectedIndex = tab_MenuList.TabPages.Count - 1;
        }

        /// <summary>
        /// 计划 - 选项卡切换事件
        /// </summary>
        private void Tab_JH_FileInfo_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = tab_JH_FileInfo.SelectedIndex;
            if (index == 3)//分盒
            {
                LoadJHBoxList(lbl_JH_Name.Tag);
                LoadJHFileBoxTable(cbo_JH_Box.SelectedValue, lbl_JH_Name.Tag);
            }
        }

        /// <summary>
        /// 加载计划-案卷盒归档表
        /// </summary>
        /// <param name="pbId">案卷盒ID</param>
        /// <param name="objId">所属对象ID</param>
        private void LoadJHFileBoxTable(object pbId, object objId)
        {
            lsv_JH_File1.Items.Clear();
            lsv_JH_File1.Columns.Clear();
            lsv_JH_File2.Items.Clear();
            lsv_JH_File2.Columns.Clear();

            lsv_JH_File1.Columns.AddRange(new ColumnHeader[]
            {
                    new ColumnHeader{ Name = "jh_file1_id", Text = "主键" },
                    new ColumnHeader{ Name = "jh_file1_type", Text = "文件类别" },
                    new ColumnHeader{ Name = "jh_file1_name", Text = "文件名称" }
            });
            lsv_JH_File2.Columns.AddRange(new ColumnHeader[]
            {
                    new ColumnHeader{ Name = "jh_file2_id", Text = "主键" },
                    new ColumnHeader{ Name = "jh_file2_type", Text = "文件类别" },
                    new ColumnHeader{ Name = "jh_file2_name", Text = "文件名称" }
            });

            //未归档
            string querySql = $"SELECT pfl_id,dd_name,pfl_filename FROM processing_file_list LEFT JOIN data_dictionary " +
                $"ON pfl_categor=dd_id WHERE pfl_obj_id = '{objId}' AND pfl_status={(int)GuiDangStatus.NonGuiDang}";
            DataTable dataTable = SqlHelper.ExecuteQuery(querySql);
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                ListViewItem item = lsv_JH_File1.Items.Add(GetValue(dataTable.Rows[i]["pfl_id"]));
                item.SubItems.Add(GetValue(dataTable.Rows[i]["dd_name"]));
                item.SubItems.Add(GetValue(dataTable.Rows[i]["pfl_filename"]));

            }
            //已归档
            object id = SqlHelper.ExecuteOnlyOneQuery($"SELECT pb_files_id FROM processing_box WHERE pb_id = '{pbId}' AND pb_obj_id = '{objId}'");
            if (id != null)
            {
                querySql = $"SELECT pfl_id,dd_name,pfl_filename FROM processing_file_list LEFT JOIN data_dictionary ON pfl_categor=dd_id WHERE pfl_id IN(";
                string[] ids = GetValue(id).Split(',');
                for (int i = 0; i < ids.Length; i++)
                    querySql += "'" + ids[i] + "'" + (i == ids.Length - 1 ? ")" : ",");
                DataTable _dataTable = SqlHelper.ExecuteQuery(querySql);
                for (int i = 0; i < _dataTable.Rows.Count; i++)
                {
                    ListViewItem item = lsv_JH_File2.Items.Add(GetValue(_dataTable.Rows[i]["pfl_id"]));
                    item.SubItems.Add(GetValue(_dataTable.Rows[i]["dd_name"]));
                    item.SubItems.Add(GetValue(_dataTable.Rows[i]["pfl_filename"]));

                }
                lsv_JH_File2.Columns["jh_file2_id"].Width = lsv_JH_File1.Columns["jh_file1_id"].Width = 0;
                lsv_JH_File2.Columns["jh_file2_type"].Width = lsv_JH_File1.Columns["jh_file1_type"].Width = 100;
                lsv_JH_File2.Columns["jh_file2_name"].Width = lsv_JH_File1.Columns["jh_file1_name"].Width = 200;
            }
        }

        /// <summary>
        /// 将object对象转换成string
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        private string GetValue(object obj)
        {
            return obj == null ? string.Empty : obj.ToString();
        }

        /// <summary>
        /// 案卷归档事件
        /// </summary>
        private void Btn_Box_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;
            //计划
            if (button.Name.Contains("btn_JH_Box"))
            {
                if ("btn_JH_Box_Right".Equals(button.Name))
                {
                    if (lsv_JH_File1.SelectedItems.Count > 0)
                    {
                        int count = lsv_JH_File1.SelectedItems.Count;
                        if (count > 0)
                        {
                            object[] _obj = new object[count];
                            for (int i = 0; i < count; i++)
                                _obj[i] = lsv_JH_File1.SelectedItems[i].SubItems[0].Text;
                            SetFileState(_obj, cbo_JH_Box.SelectedValue, true);
                        }
                    }
                }
                else if ("btn_JH_Box_RightAll".Equals(button.Name))
                {
                    int count = lsv_JH_File1.Items.Count;
                    if (count > 0)
                    {
                        object[] _obj = new object[count];
                        for (int i = 0; i < count; i++)
                            _obj[i] = lsv_JH_File1.Items[i].SubItems[0].Text;
                        SetFileState(_obj, cbo_JH_Box.SelectedValue, true);
                    }
                }
                else if ("btn_JH_Box_Left".Equals(button.Name))
                {
                    int count = lsv_JH_File2.SelectedItems.Count;
                    if (count > 0)
                    {
                        object[] _obj = new object[count];
                        for (int i = 0; i < count; i++)
                            _obj[i] = lsv_JH_File2.SelectedItems[i].SubItems[0].Text;
                        SetFileState(_obj, cbo_JH_Box.SelectedValue, false);
                    }
                }
                else if ("btn_JH_Box_LeftAll".Equals(button.Name))
                {
                    int count = lsv_JH_File2.Items.Count;
                    if (count > 0)
                    {
                        object[] _obj = new object[count];
                        for (int i = 0; i < count; i++)
                            _obj[i] = lsv_JH_File2.Items[i].SubItems[0].Text;
                        SetFileState(_obj, cbo_JH_Box.SelectedValue, false);
                    }
                }
                LoadJHFileBoxTable(cbo_JH_Box.SelectedValue, lbl_JH_Name.Tag);
            }
        }

        /// <summary>
        /// 文件归档
        /// </summary>
        /// <param name="_obj">待处理文件IDS</param>
        /// <param name="pbid">案卷盒ID</param>
        /// <param name="pbid">ture:归档;false:不归档</param>
        private void SetFileState(object[] _obj, object pbid, bool isGD)
        {
            string filesIds = GetValue(SqlHelper.ExecuteOnlyOneQuery($"SELECT pb_files_id FROM processing_box WHERE pb_id='{pbid}'"));
            if (isGD)
            {
                //将文件状态置为已归档
                StringBuilder updateSql = new StringBuilder($"UPDATE processing_file_list SET pfl_status={(int)GuiDangStatus.GuiDangSuccess} WHERE pfl_id IN (");
                for (int i = 0; i < _obj.Length; i++)
                    updateSql.Append($"'{_obj[i]}'{(i == _obj.Length - 1 ? ")" : ",")}");
                SqlHelper.ExecuteNonQuery(updateSql.ToString());

                //当前案卷盒中加入已归档文件ID
                string newfilesIds = filesIds.EndsWith(",") ? filesIds : filesIds + ",";
                for (int i = 0; i < _obj.Length; i++)
                    newfilesIds += $"{_obj[i]}{(i == _obj.Length - 1 ? string.Empty : ",")}";
                updateSql = new StringBuilder($"UPDATE processing_box SET pb_files_id='{newfilesIds}' WHERE pb_id='{pbid}'");
                SqlHelper.ExecuteNonQuery(updateSql.ToString());
            }
            else
            {
                //将文件状态置为未归档
                StringBuilder updateSql = new StringBuilder($"UPDATE processing_file_list SET pfl_status={(int)GuiDangStatus.NonGuiDang} WHERE pfl_id IN (");
                for (int i = 0; i < _obj.Length; i++)
                    updateSql.Append($"'{_obj[i]}'{(i == _obj.Length - 1 ? ")" : ",")}");
                SqlHelper.ExecuteNonQuery(updateSql.ToString());

                //当前案卷盒中移除已归档文件ID【先查询，接着修改，最后更新】
                if (!string.IsNullOrEmpty(filesIds))
                {
                    string newfilesIds = filesIds;
                    for (int i = 0; i < _obj.Length; i++)
                        if (filesIds.Contains(GetValue(_obj[i])))
                            newfilesIds = newfilesIds.Replace(_obj[i] + ",", string.Empty).Replace(GetValue(_obj[i]), string.Empty);
                    SqlHelper.ExecuteNonQuery($"UPDATE processing_box SET pb_files_id='{newfilesIds}' WHERE pb_id='{pbid}'");
                }
            }
        }

        /// <summary>
        /// 计划 - 增加/删除案卷盒
        /// </summary>
        private void Lbl_Box_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Label label = sender as Label;
            //计划
            if (label.Name.Contains("lbl_JH_Box"))
            {
                if ("lbl_JH_Box_Add".Equals(label.Name))//新增
                {
                    //当前已有盒号数量
                    int amount = Convert.ToInt32(SqlHelper.ExecuteOnlyOneQuery($"SELECT COUNT(pb_box_number) FROM processing_box WHERE pb_obj_id='{lbl_JH_Name.Tag}'"));
                    string gch = txt_JH_Box_GCID.Text;
                    string insertSql = $"INSERT INTO processing_box VALUES('{Guid.NewGuid().ToString()}','{amount + 1}','{gch}',null,'{lbl_JH_Name.Tag}')";
                    SqlHelper.ExecuteNonQuery(insertSql);
                }
                else if ("lbl_JH_Box_Remove".Equals(label.Name))//删除
                {
                    if (MessageBox.Show("删除当前案卷盒会清空盒下已归档的文件，是否继续？", "删除确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        object value = cbo_JH_Box.SelectedValue;
                        if (value != null)
                        {
                            //将当前盒中文件状态致为未归档
                            object ids = SqlHelper.ExecuteOnlyOneQuery($"SELECT pb_files_id FROM processing_box WHERE pb_obj_id='{lbl_JH_Name.Tag}' AND pb_id='{value}'");
                            string[] _ids = ids.ToString().Split(',');
                            StringBuilder sb = new StringBuilder($"UPDATE processing_file_list SET pfl_status={(int)GuiDangStatus.NonGuiDang} WHERE pfl_id IN(");
                            for (int i = 0; i < _ids.Length; i++)
                                sb.Append($"'{_ids[i]}'{(_ids.Length - 1 != i ? "," : ")")}");
                            SqlHelper.ExecuteNonQuery(sb.ToString());

                            //删除当前盒信息
                            SqlHelper.ExecuteNonQuery($"DELETE FROM processing_box WHERE pb_id='{value}'");
                        }
                    }
                }
                LoadJHBoxList(lbl_JH_Name.Tag);
                LoadJHFileBoxTable(cbo_JH_Box.SelectedValue, lbl_JH_Name.Tag);
            }
        }

        /// <summary>
        /// 计划 - 加载案卷盒列表
        /// </summary>
        /// <param name="objId">案卷盒所属对象ID</param>
        private void LoadJHBoxList(object objId)
        {
            DataTable table = SqlHelper.ExecuteQuery($"SELECT pb_id,pb_box_number FROM processing_box WHERE pb_obj_id='{objId}'");
            cbo_JH_Box.DataSource = table;
            cbo_JH_Box.DisplayMember = "pb_box_number";
            cbo_JH_Box.ValueMember = "pb_id";
            cbo_JH_Box.SelectedIndex = 0;
        }

        /// <summary>
        /// 案卷盒切换事件
        /// </summary>
        private void Cbo_Box_SelectionChangeCommitted(object sender, EventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;
            if ("cbo_JH_Box".Equals(comboBox.Name))
            {
                object pbId = comboBox.SelectedValue;
                //加载归档表
                LoadJHFileBoxTable(pbId, lbl_JH_Name.Tag);
                //加载馆藏号
                object gcId = SqlHelper.ExecuteOnlyOneQuery($"SELECT pb_gc_id FROM processing_box WHERE pb_id='{pbId}'");
                txt_JH_Box_GCID.Text = GetValue(gcId);

            }
        }

        /// <summary>
        /// 根目录切换事件
        /// </summary>
        private void tab_MenuList_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = tab_MenuList.SelectedIndex;
            if (index != -1) {
                string currentPageName = tab_MenuList.TabPages[index].Name;
                if ("plan_project".Equals(currentPageName))
                {

                }
            }
        }

        /// <summary>
        /// 根据ID加载【项目/课题】基本信息和文件列表
        /// </summary>
        /// <param name="planId">【项目/课题】ID</param>
        private void LoadProjectPage(object projectId)
        {
            DataTable table = SqlHelper.ExecuteQuery($"SELECT * FROM project_info WHERE pi_id='{projectId}'");
            if (table.Rows.Count > 0)
            {
                DataRow row = table.Rows[0];
                dgv_JH_XM_FileList.Tag = row["pi_id"];
                txt_JH_XM_Code.Text = GetValue(row["pi_code"]);
                txt_JH_XM_Name.Text = GetValue(row["pi_name"]);
                txt_JH_XM_JHType.Text = GetValue(row["pi_type"]);
                txt_JH_XM_LY.Text = GetValue(row["pb_belong"]);
                txt_JH_XM_ZT.Text = GetValue(row["pb_belong_type"]);
                txt_JH_XM_JF.Text = GetValue(row["pi_money"]);
                dtp_JH_XM_StartTime.Value = DateTime.Parse(GetValue(row["pi_start_datetime"]));
                dtp_JH_XM_EndTime.Value = DateTime.Parse(GetValue(row["pi_end_datetime"]));
                txt_JH_XM_LXND.Text = GetValue(row["pi_year"]);
                txt_JH_XM_Unit.Text = GetValue(row["pi_company_id"]);
                txt_JH_XM_Province.Text = GetValue(row["pi_province"]);
                txt_JH_XM_UnitUser.Text = GetValue(row["pi_company_user"]);
                txt_JH_XM_ObjUser.Text = GetValue(row["pi_project_user"]);
                txt_JH_XM_ObjIntroduct.Text = GetValue(row["pi_introduction"]);
            }
            LoadFileList(dgv_JH_XM_FileList, "jh_xm_", projectId);
        }
    }
}
