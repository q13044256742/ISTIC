using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using 科技计划项目档案数据采集管理系统.Cataloguing;
using 科技计划项目档案数据采集管理系统.KyoControl;

namespace 科技计划项目档案数据采集管理系统
{
    public partial class Frm_MyWorkQT : DevExpress.XtraEditors.XtraForm
    {
        /// <summary>
        /// 待删除文件ID
        /// </summary>
        List<object> removeIdList = new List<object>();
    
        /// <summary>
        /// 当前加工类型
        /// </summary>
        private WorkType workType;
    
        private object wmid;
        private Frm_Advice adviceFrom;
        /// <summary>
        /// 我的质检列表【只读】
        /// </summary>
        private readonly bool isReadOnly;
        /// <summary>
        /// 所属对象主键
        /// </summary>
        private object objId;
        public object planCode;
        public int DEV_TYPE = -1;
        public object unitCode;
        public object trcId;
        /// <summary>
        /// 加工类型【返工】
        /// </summary>
        private ControlType controlType;
    
        /// <summary>
        /// 禁用背景色
        /// </summary>
        private Color DisEnbleColor = Color.Gray;

        List<TabPage> tabList = new List<TabPage>();

        /// <summary>
        /// 开始加工指定的对象
        /// </summary>
        /// <param name="workType">对象类型</param>
        /// <param name="planId">计划主键（仅针对光盘/批次加工）</param>
        public Frm_MyWorkQT(WorkType workType, object objId, object wmid, ControlType controlType)
        {
            InitializeComponent();
            this.wmid = wmid;
            this.objId = objId;
            this.workType = workType;
            this.controlType = controlType;
            if(workType == WorkType.PaperWork_Imp && DEV_TYPE == -1)
            {
                object _type = SqlHelper.ExecuteOnlyOneQuery($"SELECT imp_type FROM imp_info WHERE imp_id='{objId}'");
                if(!string.IsNullOrEmpty(ToolHelper.GetValue(_type)))
                    DEV_TYPE = Convert.ToInt32(_type);
            }
            string querySql = "SELECT pi_code FROM project_info WHERE pi_id=(SELECT pi_obj_id FROM(" +
                "SELECT pi_id, pi_obj_id FROM project_info WHERE pi_categor = 2 UNION ALL " +
                "SELECT ti_id, ti_obj_id FROM topic_info WHERE ti_categor = -3) A " +
               $"WHERE pi_id = '{objId}')";
            object value = SqlHelper.ExecuteOnlyOneQuery(querySql);
            if(value != null) Tag = value;
        }

        public Frm_MyWorkQT(WorkType workType, object objId, object wmid, ControlType controlType, bool isReadOnly) : this(workType, objId, wmid, controlType)
        {
            if(isReadOnly)
            {
                pal_Imp_BtnGroup.Enabled = false;
                pal_Plan_BtnGroup.Enabled = false;
                pal_Project_BtnGroup.Enabled = false;
                pal_Topic_BtnGroup.Enabled = false;
                pal_Subject_BtnGroup.Enabled = false;
                pal_Special_BtnGroup.Enabled = false;
                Text += "[我的质检]";
            }
            this.isReadOnly = isReadOnly;
        }

        /// <summary>
        /// 初始化选项卡
        /// </summary>
        /// <param name="planId">计划ID（仅针对纸本/光盘加工）</param>
        private void InitialForm(object planId, ControlType type)
        {
            foreach (TabPage tab in tab_MenuList.TabPages)
            {
                tabList.Add(tab);
                tab_MenuList.TabPages.Remove(tab);
            }
            LoadTreeList(planId, type);
        }

        /// <summary>
        /// 加载计划页面
        /// </summary>
        private void LoadPlanPage(TreeNode node)
        {
            if((ControlType)node.Tag == ControlType.Plan)
            {
                DataRow row = SqlHelper.ExecuteSingleRowQuery($"SELECT pi_name, pi_intro, pi_code, pi_submit_status FROM project_info WHERE pi_id='{node.Name}'");
                if(row != null)
                {
                    lbl_Plan_Name.Tag = ToolHelper.GetValue(row["pi_code"]);
                    lbl_Plan_Name.Text = ToolHelper.GetValue(row["pi_name"]);
                    lbl_Plan_Intro.Text = ToolHelper.GetValue(row["pi_intro"]);
                    tab_Plan_Info.Tag = node.Name;
                    LoadFileList(dgv_Plan_FileList, "plan_fl_", node.Name);
                    EnableControls(ControlType.Plan, Convert.ToInt32(row["pi_submit_status"]) != 1);
                }
            }
            else
            {
                DataRow row = SqlHelper.ExecuteSingleRowQuery($"SELECT dd_name, dd_note, dd_code FROM data_dictionary WHERE dd_id='{node.Name}'");
                if(row != null)
                {
                    lbl_Plan_Name.Tag = ToolHelper.GetValue(row["dd_code"]);
                    lbl_Plan_Name.Text = ToolHelper.GetValue(row["dd_name"]);
                    lbl_Plan_Intro.Text = ToolHelper.GetValue(row["dd_note"]);
                }
            }
            plan.Tag = node.Name;
            if(node.ForeColor == DisEnbleColor)
            {
                pal_Plan_BtnGroup.Enabled = false;
                cbo_Plan_HasNext.Enabled = false;
            }
        }

        /// <summary>
        /// 加载文件列表
        /// </summary>
        /// <param name="dataGridView">表格控件</param>
        /// <param name="key">列名关键字</param>
        /// <param name="parentId">所属对象ID</param>
        private void LoadFileList(DataGridView dataGridView, string key, object parentId)
        {
            dataGridView.Rows.Clear();
            string querySql = $"SELECT * FROM processing_file_list WHERE pfl_obj_id='{parentId}' ORDER BY pfl_sort";
            DataTable dataTable = SqlHelper.ExecuteQuery(querySql);
            for(int i = 0; i < dataTable.Rows.Count; i++)
            {
                int index = dataGridView.Rows.Add();
                dataGridView.Rows[index].Cells[key + "id"].Value = i + 1;
                dataGridView.Rows[index].Cells[key + "id"].Tag = dataTable.Rows[i]["pfl_id"];
                dataGridView.Rows[index].Cells[key + "stage"].Value = dataTable.Rows[i]["pfl_stage"];
                SetCategorByStage(dataTable.Rows[i]["pfl_stage"], dataGridView.Rows[index], key);
                dataGridView.Rows[index].Cells[key + "categor"].Value = dataTable.Rows[i]["pfl_categor"];
                dataGridView.Rows[index].Cells[key + "code"].Value = dataTable.Rows[i]["pfl_code"];
                dataGridView.Rows[index].Cells[key + "name"].Value = dataTable.Rows[i]["pfl_name"];
                dataGridView.Rows[index].Cells[key + "user"].Value = dataTable.Rows[i]["pfl_user"];
                dataGridView.Rows[index].Cells[key + "type"].Value = dataTable.Rows[i]["pfl_type"];
                dataGridView.Rows[index].Cells[key + "pages"].Value = dataTable.Rows[i]["pfl_pages"];
                dataGridView.Rows[index].Cells[key + "count"].Value = dataTable.Rows[i]["pfl_count"];
                dataGridView.Rows[index].Cells[key + "amount"].Value = dataTable.Rows[i]["pfl_amount"];
                dataGridView.Rows[index].Cells[key + "date"].Value = GetDateValue(dataTable.Rows[i]["pfl_date"], "yyyy-MM-dd");
                dataGridView.Rows[index].Cells[key + "unit"].Value = dataTable.Rows[i]["pfl_unit"];
                dataGridView.Rows[index].Cells[key + "carrier"].Value = dataTable.Rows[i]["pfl_carrier"];
                dataGridView.Rows[index].Cells[key + "link"].Value = dataTable.Rows[i]["pfl_link"];
                dataGridView.Rows[index].Cells[key + "link"].Tag = dataTable.Rows[i]["pfl_file_id"];
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
        private void ShowTab(string name, int index)
        {
            if (index == 0)
                tab_MenuList.TabPages.Clear();
            else
            {
                int amount = tab_MenuList.TabPages.Count;
                List<TabPage> removeList = new List<TabPage>();
                for (int i = 0; i < amount; i++)
                    if (i >= index)
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
            //tab_MenuList.SelectedIndex = index;
        }
        
        /// <summary>
        /// 渲染表格样式，初始化表单字段
        /// </summary>
        private void Frm_MyWork_Load(object sender, EventArgs e)
        {
            tab_Plan_Info.Height = 345;
            tab_Plan_Info.Top = 310;
            tab_Project_Info.Height = 345;
            tab_Project_Info.Top = 310;
            tab_Topic_Info.Height = 345;
            tab_Topic_Info.Top = 310;
            tab_Subject_Info.Height = 345;
            tab_Subject_Info.Top = 310;
            tab_Imp_Info.Height = 345;
            tab_Imp_Info.Top = 310;
            tab_Special_Info.Height = 345;
            tab_Special_Info.Top = 310;

            //不同加工种类特殊处理
            if(workType == WorkType.PaperWork)
            {
                dgv_Plan_FileList.Columns["plan_fl_link"].Visible = false;
                dgv_Project_FileList.Columns["project_fl_link"].Visible = false;
                dgv_Topic_FileList.Columns["topic_fl_link"].Visible = false;
                dgv_Subject_FileList.Columns["subject_fl_link"].Visible = false;
                dgv_Imp_FileList.Columns["imp_fl_link"].Visible = false;
                dgv_Special_FileList.Columns["special_fl_link"].Visible = false;
            }

            InitialForm(objId, controlType);

            //阶段
            InitialStageList(dgv_Plan_FileList.Columns["plan_fl_stage"]);
            InitialStageList(dgv_Project_FileList.Columns["project_fl_stage"]);
            InitialStageList(dgv_Topic_FileList.Columns["topic_fl_stage"]);
            InitialStageList(dgv_Subject_FileList.Columns["subject_fl_stage"]);
            InitialStageList(dgv_Imp_FileList.Columns["imp_fl_stage"]);
            InitialStageList(dgv_Special_FileList.Columns["special_fl_stage"]);

            //文件类别
            InitialCategorList(dgv_Plan_FileList, "plan_fl_");
            InitialCategorList(dgv_Project_FileList, "project_fl_");
            InitialCategorList(dgv_Topic_FileList, "topic_fl_");
            InitialCategorList(dgv_Subject_FileList, "subject_fl_");
            InitialCategorList(dgv_Imp_FileList, "imp_fl_");
            InitialCategorList(dgv_Special_FileList, "special_fl_");

            //文件类型
            InitialTypeList(dgv_Plan_FileList, "plan_fl_");
            InitialTypeList(dgv_Project_FileList, "project_fl_");
            InitialTypeList(dgv_Topic_FileList, "topic_fl_");
            InitialTypeList(dgv_Subject_FileList, "subject_fl_");
            InitialTypeList(dgv_Imp_FileList, "imp_fl_");
            InitialTypeList(dgv_Special_FileList, "special_fl_");

            //载体
            InitialCarrierList(dgv_Plan_FileList, "plan_fl_");
            InitialCarrierList(dgv_Project_FileList, "project_fl_");
            InitialCarrierList(dgv_Topic_FileList, "topic_fl_");
            InitialCarrierList(dgv_Subject_FileList, "subject_fl_");
            InitialCarrierList(dgv_Imp_FileList, "imp_fl_");
            InitialCarrierList(dgv_Special_FileList, "special_fl_");

            //文件核查原因列表
            InitialLostReasonList(dgv_Plan_FileValid, "plan_fc_");
            InitialLostReasonList(dgv_Project_FileValid, "project_fc_");
            InitialLostReasonList(dgv_Topic_FileValid, "topic_fc_");
            InitialLostReasonList(dgv_Subject_FileValid, "subject_fc_");
            InitialLostReasonList(dgv_Imp_FileValid, "imp_fc_");
            InitialLostReasonList(dgv_Special_FileValid, "special_fc_");

            cbo_Plan_HasNext.SelectedIndex = 0;
            cbo_Project_HasNext.SelectedIndex = 0;
            cbo_Topic_HasNext.SelectedIndex = 0;

            dgv_Plan_FileList.ColumnHeadersDefaultCellStyle = DataGridViewStyleHelper.GetHeaderStyle();
            dgv_Project_FileList.ColumnHeadersDefaultCellStyle = DataGridViewStyleHelper.GetHeaderStyle();
            dgv_Topic_FileList.ColumnHeadersDefaultCellStyle = DataGridViewStyleHelper.GetHeaderStyle();
            dgv_Subject_FileList.ColumnHeadersDefaultCellStyle = DataGridViewStyleHelper.GetHeaderStyle();
            dgv_Imp_FileList.ColumnHeadersDefaultCellStyle = DataGridViewStyleHelper.GetHeaderStyle();
            dgv_Special_FileList.ColumnHeadersDefaultCellStyle = DataGridViewStyleHelper.GetHeaderStyle();

            dgv_Plan_FileList.DefaultCellStyle = DataGridViewStyleHelper.GetCellStyle();
            dgv_Project_FileList.DefaultCellStyle = DataGridViewStyleHelper.GetCellStyle();
            dgv_Topic_FileList.DefaultCellStyle = DataGridViewStyleHelper.GetCellStyle();
            dgv_Subject_FileList.DefaultCellStyle = DataGridViewStyleHelper.GetCellStyle();
            dgv_Imp_FileList.DefaultCellStyle = DataGridViewStyleHelper.GetCellStyle();
            dgv_Special_FileList.DefaultCellStyle = DataGridViewStyleHelper.GetCellStyle();

            dgv_Plan_FileValid.ColumnHeadersDefaultCellStyle = DataGridViewStyleHelper.GetHeaderStyle();
            dgv_Project_FileValid.ColumnHeadersDefaultCellStyle = DataGridViewStyleHelper.GetHeaderStyle();
            dgv_Topic_FileValid.ColumnHeadersDefaultCellStyle = DataGridViewStyleHelper.GetHeaderStyle();
            dgv_Subject_FileValid.ColumnHeadersDefaultCellStyle = DataGridViewStyleHelper.GetHeaderStyle();
            dgv_Imp_FileValid.ColumnHeadersDefaultCellStyle = DataGridViewStyleHelper.GetHeaderStyle();
            dgv_Special_FileValid.ColumnHeadersDefaultCellStyle = DataGridViewStyleHelper.GetHeaderStyle();

            dgv_Plan_FileValid.DefaultCellStyle = DataGridViewStyleHelper.GetCellStyle();
            dgv_Project_FileValid.DefaultCellStyle = DataGridViewStyleHelper.GetCellStyle();
            dgv_Topic_FileValid.DefaultCellStyle = DataGridViewStyleHelper.GetCellStyle();
            dgv_Subject_FileValid.DefaultCellStyle = DataGridViewStyleHelper.GetCellStyle();
            dgv_Imp_FileValid.DefaultCellStyle = DataGridViewStyleHelper.GetCellStyle();
            dgv_Special_FileValid.DefaultCellStyle = DataGridViewStyleHelper.GetCellStyle();

        }
 
        /// <summary>
        /// 初始化文件核查原因
        /// </summary>
        private void InitialLostReasonList(DataGridView view, string key)
        {
            string code = "dic_file_lostreason";
            DataTable table = SqlHelper.ExecuteQuery($"SELECT * FROM data_dictionary WHERE dd_pId = (SELECT dd_id FROM data_dictionary WHERE dd_code='{code}')");
            DataGridViewComboBoxColumn comboBoxColumn = view.Columns[key + "reason"] as DataGridViewComboBoxColumn;
            comboBoxColumn.DataSource = table;
            comboBoxColumn.DisplayMember = "dd_name";
            comboBoxColumn.ValueMember = "dd_id";
        }

        private void InitialFormList(DataGridView dataGridView, string key)
        {
            DataGridViewComboBoxColumn formColumn = dataGridView.Columns[key + "form"] as DataGridViewComboBoxColumn;
            formColumn.DataSource = DictionaryHelper.GetTableByCode("dic_file_state");
            formColumn.DisplayMember = "dd_name";
            formColumn.ValueMember = "dd_id";
            formColumn.DefaultCellStyle = new DataGridViewCellStyle() { Font = new System.Drawing.Font("宋体", 10.5f) };
        }

        private void InitialFormatList(DataGridView dataGridView, string key)
        {
            DataGridViewComboBoxColumn formatColumn = dataGridView.Columns[key + "format"] as DataGridViewComboBoxColumn;
            formatColumn.DataSource = DictionaryHelper.GetTableByCode("dic_file_format");
            formatColumn.DisplayMember = "dd_name";
            formatColumn.ValueMember = "dd_id";
            formatColumn.DefaultCellStyle = new DataGridViewCellStyle() { Font = new System.Drawing.Font("宋体", 10.5f) };
        }

        private void InitialCarrierList(DataGridView dataGridView, string key)
        {
            DataGridViewComboBoxColumn carrierColumn = dataGridView.Columns[key + "carrier"] as DataGridViewComboBoxColumn;
            carrierColumn.DataSource = DictionaryHelper.GetTableByCode("dic_file_zt");
            carrierColumn.DisplayMember = "dd_name";
            carrierColumn.ValueMember = "dd_id";
        }

        private void InitialTypeList(DataGridView dataGridView, string key)
        {
            DataGridViewComboBoxColumn filetypeColumn = dataGridView.Columns[key + "type"] as DataGridViewComboBoxColumn;
            filetypeColumn.DataSource = DictionaryHelper.GetTableByCode("dic_file_type");
            filetypeColumn.DisplayMember = "dd_name";
            filetypeColumn.ValueMember = "dd_id";
        }
   
        /// <summary>
        /// 根据阶段初始化文件类别
        /// </summary>
        /// <param name="dataGridView">表格</param>
        /// <param name="key">关键字</param>
        private void InitialCategorList(DataGridView dataGridView, string key)
        {
            for(int i = 0; i < dataGridView.Rows.Count; i++)
            //if(dataGridView.Rows[i].Cells[key + "id"].Value != null)
            {
                DataGridViewComboBoxCell satgeCell = (DataGridViewComboBoxCell)dataGridView.Rows[i].Cells[key + "stage"];
                object stageId = satgeCell.Value;
                if(stageId != null)
                    SetCategorByStage(stageId, dataGridView.Rows[i], key);
            }
        }
   
        /// <summary>
        /// 初始化阶段下拉字段
        /// </summary>
        /// <param name="dataGridViewComboBoxColumn">指定列</param>
        private void InitialStageList(DataGridViewColumn dataGridViewColumn)
        {
            DataGridViewComboBoxColumn comboBoxColumn = dataGridViewColumn as DataGridViewComboBoxColumn;
            comboBoxColumn.DataSource = DictionaryHelper.GetTableByCode("dic_file_jd");
            comboBoxColumn.DisplayMember = "dd_name";
            comboBoxColumn.ValueMember = "dd_id";
        }
   
        /// <summary>
        /// 根据阶段设置相应的文件类别
        /// </summary>
        /// <param name="jdId">阶段ID</param>
        public void SetCategorByStage(object jdId, DataGridViewRow dataGridViewRow, string key)
        {
            //文件类别
            DataGridViewComboBoxCell categorCell = dataGridViewRow.Cells[key + "categor"] as DataGridViewComboBoxCell;

            string querySql = $"SELECT dd_id, dd_name+' '+extend_3 as dd_name FROM data_dictionary WHERE dd_pId='{jdId}' ORDER BY dd_name";
            categorCell.DataSource = SqlHelper.ExecuteQuery(querySql);
            categorCell.DisplayMember = "dd_name";
            categorCell.ValueMember = "dd_id";
            if(categorCell.Items.Count > 0)
                categorCell.Style.NullValue = categorCell.Items[0];
        }
   
        /// <summary>
        /// 单元格事件绑定
        /// </summary>
        private void Dgv_File_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            DataGridView dataGridView = sender as DataGridView;
            if("dgv_Plan_FileList".Equals(dataGridView.Name))
            {
                string columnName = dgv_Plan_FileList.CurrentCell.OwningColumn.Name;
                Control con = e.Control;
                con.Tag = ControlType.Plan;
                if("plan_fl_stage".Equals(columnName))
                    (con as System.Windows.Forms.ComboBox).SelectionChangeCommitted += new EventHandler(StageComboBox_SelectionChangeCommitted);
                else if("plan_fl_categor".Equals(columnName))
                    (con as System.Windows.Forms.ComboBox).SelectionChangeCommitted += new EventHandler(CategorComboBox_SelectionChangeCommitted);
            }
            else if("dgv_Project_FileList".Equals(dataGridView.Name))
            {
                string columnName = dgv_Project_FileList.CurrentCell.OwningColumn.Name;
                Control con = e.Control;
                con.Tag = ControlType.Project;
                if("project_fl_stage".Equals(columnName))
                    (con as System.Windows.Forms.ComboBox).SelectionChangeCommitted += new EventHandler(StageComboBox_SelectionChangeCommitted);
                else if("project_fl_categor".Equals(columnName))
                    (con as System.Windows.Forms.ComboBox).SelectionChangeCommitted += new EventHandler(CategorComboBox_SelectionChangeCommitted);
            }
            else if("dgv_Topic_FileList".Equals(dataGridView.Name))
            {
                string columnName = dgv_Topic_FileList.CurrentCell.OwningColumn.Name;
                Control con = e.Control;
                con.Tag = ControlType.Topic;
                if("topic_fl_stage".Equals(columnName))
                    (con as System.Windows.Forms.ComboBox).SelectionChangeCommitted += new EventHandler(StageComboBox_SelectionChangeCommitted);
                else if("topic_fl_categor".Equals(columnName))
                    (con as System.Windows.Forms.ComboBox).SelectionChangeCommitted += new EventHandler(CategorComboBox_SelectionChangeCommitted);
            }
            else if("dgv_Subject_FileList".Equals(dataGridView.Name))
            {
                string columnName = dgv_Subject_FileList.CurrentCell.OwningColumn.Name;
                Control con = e.Control;
                con.Tag = ControlType.Subject;
                if("subject_fl_stage".Equals(columnName))
                    (con as System.Windows.Forms.ComboBox).SelectionChangeCommitted += new EventHandler(StageComboBox_SelectionChangeCommitted);
                else if("subject_fl_categor".Equals(columnName))
                    (con as System.Windows.Forms.ComboBox).SelectionChangeCommitted += new EventHandler(CategorComboBox_SelectionChangeCommitted);
            }
            else if("dgv_Imp_FileList".Equals(dataGridView.Name))
            {
                string columnName = dgv_Imp_FileList.CurrentCell.OwningColumn.Name;
                Control con = e.Control;
                con.Tag = ControlType.Imp;
                if("imp_fl_stage".Equals(columnName))
                    (con as System.Windows.Forms.ComboBox).SelectionChangeCommitted += new EventHandler(StageComboBox_SelectionChangeCommitted);
                else if("imp_fl_categor".Equals(columnName))
                    (con as System.Windows.Forms.ComboBox).SelectionChangeCommitted += new EventHandler(CategorComboBox_SelectionChangeCommitted);
            }
            else if("dgv_Special_FileList".Equals(dataGridView.Name))
            {
                string columnName = dgv_Special_FileList.CurrentCell.OwningColumn.Name;
                Control con = e.Control;
                con.Tag = ControlType.Special;
                if("special_fl_stage".Equals(columnName))
                    (con as System.Windows.Forms.ComboBox).SelectionChangeCommitted += new EventHandler(StageComboBox_SelectionChangeCommitted);
                else if("special_fl_categor".Equals(columnName))
                    (con as System.Windows.Forms.ComboBox).SelectionChangeCommitted += new EventHandler(CategorComboBox_SelectionChangeCommitted);
            }
            if(e.Control is System.Windows.Forms.ComboBox)
            {
                System.Windows.Forms.ComboBox box = e.Control as System.Windows.Forms.ComboBox;
                if(box.Items.Count > 0)
                    box.SelectedValue = box.Items[0];
            }
        }
  
        /// <summary>
        /// 文件阶段 下拉事件
        /// </summary>
        private void StageComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            System.Windows.Forms.ComboBox comboBox = sender as System.Windows.Forms.ComboBox;
            if((ControlType)comboBox.Tag == ControlType.Plan)
                SetCategorByStage(comboBox.SelectedValue, dgv_Plan_FileList.CurrentRow, "plan_fl_");
            else if((ControlType)comboBox.Tag == ControlType.Project)
                SetCategorByStage(comboBox.SelectedValue, dgv_Project_FileList.CurrentRow, "project_fl_");
            else if((ControlType)comboBox.Tag == ControlType.Topic)
                SetCategorByStage(comboBox.SelectedValue, dgv_Topic_FileList.CurrentRow, "topic_fl_");
            else if((ControlType)comboBox.Tag == ControlType.Subject)
                SetCategorByStage(comboBox.SelectedValue, dgv_Subject_FileList.CurrentRow, "subject_fl_");
            else if((ControlType)comboBox.Tag == ControlType.Imp)
                SetCategorByStage(comboBox.SelectedValue, dgv_Imp_FileList.CurrentRow, "imp_fl_");
            else if((ControlType)comboBox.Tag == ControlType.Special)
                SetCategorByStage(comboBox.SelectedValue, dgv_Special_FileList.CurrentRow, "special_fl_");
            comboBox.Leave += new EventHandler(delegate (object obj, EventArgs eve)
            {
                System.Windows.Forms.ComboBox _comboBox = obj as System.Windows.Forms.ComboBox;
                _comboBox.SelectionChangeCommitted -= new EventHandler(StageComboBox_SelectionChangeCommitted);
            });
        }
    
        /// <summary>
        /// 文件类别 下拉事件
        /// </summary>
        private void CategorComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            System.Windows.Forms.ComboBox comboBox = sender as System.Windows.Forms.ComboBox;
            if((ControlType)comboBox.Tag == ControlType.Plan)
                SetNameByCategor(comboBox, dgv_Plan_FileList.CurrentRow, "plan_fl_", tab_Plan_Info.Tag);
            else if((ControlType)comboBox.Tag == ControlType.Project)
                SetNameByCategor(comboBox, dgv_Project_FileList.CurrentRow, "project_fl_", tab_Project_Info.Tag);
            else if((ControlType)comboBox.Tag == ControlType.Topic)
                SetNameByCategor(comboBox, dgv_Topic_FileList.CurrentRow, "topic_fl_", tab_Topic_Info.Tag);
            else if((ControlType)comboBox.Tag == ControlType.Subject)
                SetNameByCategor(comboBox, dgv_Subject_FileList.CurrentRow, "subject_fl_", tab_Subject_Info.Tag);
            else if((ControlType)comboBox.Tag == ControlType.Imp)
                SetNameByCategor(comboBox, dgv_Imp_FileList.CurrentRow, "imp_fl_", tab_Imp_Info.Tag);
            else if((ControlType)comboBox.Tag == ControlType.Special)
                SetNameByCategor(comboBox, dgv_Special_FileList.CurrentRow, "special_fl_", tab_Special_Info.Tag);
            comboBox.Leave += new EventHandler(delegate (object obj, EventArgs eve)
            {
                System.Windows.Forms.ComboBox _comboBox = obj as System.Windows.Forms.ComboBox;
                _comboBox.SelectionChangeCommitted -= new EventHandler(CategorComboBox_SelectionChangeCommitted);
            });
        }
 
        /// <summary>
        /// 根据文件类别设置文件名称
        /// </summary>
        /// <param name="catogerCode">文件类别编号</param>
        /// <param name="currentRow">当前行</param>
        private void SetNameByCategor(System.Windows.Forms.ComboBox comboBox, DataGridViewRow currentRow, string key, object objId)
        {
            if(comboBox.Items.Count <= 4) return;
            string value = ToolHelper.GetValue(SqlHelper.ExecuteOnlyOneQuery($"SELECT dd_note FROM data_dictionary WHERE dd_id='{comboBox.SelectedValue}'"));
            currentRow.Cells[key + "name"].Value = value;

            int amount = SqlHelper.ExecuteCountQuery($"SELECT COUNT(pfl_id) FROM processing_file_list WHERE pfl_categor='{comboBox.SelectedValue}' AND pfl_obj_id='{objId}'");

            currentRow.Cells[key + "categorname"].Value = null;
            if(comboBox.SelectedIndex == comboBox.Items.Count - 1)
            {
                currentRow.DataGridView.Columns[key + "categorname"].Visible = true;

                int _amount = comboBox.Items.Count;
                string tempKey = ((DataRowView)comboBox.Items[0]).Row.ItemArray[1].ToString();
                if(Regex.IsMatch(tempKey, "^[A-D]"))
                {
                    string _key = ToolHelper.GetValue(tempKey).Substring(0, 1) + _amount.ToString().PadLeft(2, '0');
                    currentRow.Cells[key + "code"].Value = _key + "-" + (amount + 1).ToString().PadLeft(2, '0');
                }
            }
            else
            {
                string _key = comboBox.Text.Split(' ')[0];
                if(Regex.IsMatch(_key, "^[A-D]"))
                    currentRow.Cells[key + "code"].Value = _key + "-" + (amount + 1).ToString().PadLeft(2, '0');
            }
        }

        private void Dgv_File_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
           
        }

        private void Cbo_JH_Next_SelectionChangeCommitted(object sender, EventArgs e)
        {
            System.Windows.Forms.ComboBox comboBox = sender as System.Windows.Forms.ComboBox;
            if(comboBox.Name.Contains("Special"))
            {
                object id = dgv_Special_FileList.Tag;
                if(id == null)
                {
                    XtraMessageBox.Show("尚未保存当前项目，无法添加新数据。", "温馨提示");
                    cbo_Special_HasNext.SelectedIndex = 0;
                }
                else
                {
                    int _index = tab_MenuList.SelectedIndex;
                    int index = comboBox.SelectedIndex;
                    if(index == 0)//无
                    {
                        ShowTab(null, _index + 1);

                        pal_Project.Tag = null;
                    }
                    else if(index == 1)//父级 - 项目
                    {
                        ShowTab("project", _index + 1);
                        pal_Project.Tag = dgv_Special_FileList.Tag;
                        ResetControls(ControlType.Plan);
                    }
                    else if(index == 2)//父级 - 课题
                    {
                        ShowTab("plan_topic", _index + 1);
                        pal_Topic.Tag = dgv_Special_FileList.Tag;
                        ResetControls(ControlType.Topic);
                    }
                }
            }
            else
            {
                object id = dgv_Plan_FileList.Tag;
                if(id == null)
                {
                    XtraMessageBox.Show("尚未保存当前项目，无法添加新数据。", "温馨提示");
                    cbo_Plan_HasNext.SelectedIndex = 0;
                }
                else
                {
                    int _index = tab_MenuList.SelectedIndex;
                    int index = comboBox.SelectedIndex;
                    if(index == 0)//无
                    {
                        ShowTab(null, _index + 1);

                        pal_Project.Tag = null;
                    }
                    else if(index == 1)//父级 - 项目
                    {
                        ShowTab("project", _index + 1);
                        ResetControls(ControlType.Plan);
                        pal_Project.Tag = dgv_Plan_FileList.Tag;
                        txt_Project_Code.Text = DateTime.Now.Year + ToolHelper.GetValue(planCode);
                    }
                    else if(index == 2)//父级 - 课题
                    {
                        ShowTab("plan_topic", _index + 1);
                        ResetControls(ControlType.Topic);
                        pal_Topic.Tag = dgv_Plan_FileList.Tag;
                        txt_Project_Code.Text = DateTime.Now.Year + ToolHelper.GetValue(planCode);
                    }
                }
            }
        }

        private void Cbo_JH_KT_HasNext_SelectionChangeCommitted(object sender, EventArgs e)
        {
            int _index = tab_MenuList.SelectedIndex;
            int index = cbo_Topic_HasNext.SelectedIndex;
            if(index == 0)//无
            {
                ShowTab(null, _index + 1);
            }
            else if(index == 1)//子级 - 子课题
            {
                if(dgv_Topic_FileList.Tag == null)
                {
                    XtraMessageBox.Show("尚未保存当前课题信息，无法添加新数据。", "温馨提示");
                    cbo_Topic_HasNext.SelectedIndex = 0;
                }
                else
                {
                    ShowTab("plan_topic_subtopic", _index + 1);
                    ResetControls(ControlType.Subject);
                }
            }
        }

        /// <summary>
        /// 保存事件
        /// </summary>
        private void Btn_Save_Click(object sender, EventArgs e)
        {
            KyoButton button = sender as KyoButton;
            DataGridView view = null;
            string key = string.Empty;
            if("btn_Plan_Save".Equals(button.Name))
            {
                object objId = tab_Plan_Info.Tag;
                view = dgv_Plan_FileList;
                key = "plan_fl_";
                int index = tab_Plan_Info.SelectedTabPageIndex;
                if(index == 0)//文件
                {
                    if(objId == null)
                        objId = view.Tag = AddProjectBasicInfo(this.objId, ControlType.Plan);
                    else
                        UpdateBasicInfo(objId, ControlType.Plan);
                    if(CheckFileList(view.Rows, key))
                    {
                        int maxLength = view.Rows.Count - 1;
                        for(int i = 0; i < maxLength; i++)
                        {
                            DataGridViewRow row = view.Rows[i];
                            row.Cells["{key}id"].Tag = AddFileInfo(key, row, objId, row.Index);
                        }
                        RemoveFileList(objId);

                        XtraMessageBox.Show("文件信息保存成功。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        GoToTreeList();
                    }
                    else
                        XtraMessageBox.Show("文件信息存在错误数据，请先更正。", "保存失败", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
                else if(objId != null)
                {
                    if(index == 1)//文件核查
                    {
                        if(CheckValidMustEnter(dgv_Plan_FileValid, "plan_fc_"))
                        {
                            ModifyFileValid(dgv_Plan_FileValid, objId, "plan_fc_");
                            XtraMessageBox.Show("文件核查信息保存成功。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        }
                        else
                            XtraMessageBox.Show("请填写完整信息。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                    else if(index == 2)
                    {

                    }
                }
            }
            else if("btn_Project_Save".Equals(button.Name))
            {
                object objId = tab_Project_Info.Tag;
                view = dgv_Project_FileList;
                key = "project_fl_";
                int index = tab_Project_Info.SelectedTabPageIndex;
                if(index == 0)
                {
                    if(CheckMustEnter(button.Name, objId))
                    {
                        if(objId == null)
                            objId = view.Tag = AddProjectBasicInfo(project.Tag, ControlType.Project);
                        else
                            UpdateBasicInfo(objId, ControlType.Project);

                        if(CheckFileList(dgv_Project_FileList.Rows, key))
                        {
                            int maxLength = dgv_Project_FileList.Rows.Count - 1;
                            for(int i = 0; i < maxLength; i++)
                            {
                                object fileName = dgv_Project_FileList.Rows[i].Cells[$"{key}name"].Value;
                                if(fileName != null)
                                {
                                    DataGridViewRow row = dgv_Project_FileList.Rows[i];
                                    object fileId = AddFileInfo(key, row, objId, row.Index);
                                    row.Cells[$"{key}id"].Tag = fileId;
                                }
                            }
                            RemoveFileList(objId);

                            XtraMessageBox.Show("信息保存成功。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                            GoToTreeList();
                        }
                        else
                            XtraMessageBox.Show("文件信息存在错误数据，请先更正。", "保存失败", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                }
                else if(objId != null)
                {
                    if(index == 1)//文件核查
                    {
                        if(CheckValidMustEnter(dgv_Project_FileValid, "project_fl_"))
                        {
                            ModifyFileValid(dgv_Project_FileValid, objId, "project_fl_");
                            XtraMessageBox.Show("文件核查信息保存成功。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        }
                        else
                            XtraMessageBox.Show("请填写完整信息。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                    else if(index == 2)
                    {

                    }
                }
            }
            else if("btn_Topic_Save".Equals(button.Name))
            {
                object objId = tab_Topic_Info.Tag;
                view = dgv_Topic_FileList;
                key = "topic_fl_";
                int index = tab_Topic_Info.SelectedTabPageIndex;
                if(index == 0)
                {
                    if(CheckMustEnter(button.Name, objId))
                    {
                        if(objId != null)//更新
                            UpdateBasicInfo(objId, ControlType.Topic);
                        else//新增
                            objId = view.Tag = AddProjectBasicInfo(pal_Topic.Tag, ControlType.Topic);

                        if(CheckFileList(view.Rows, key))
                        {
                            int maxLength = dgv_Topic_FileList.Rows.Count - 1;
                            for(int i = 0; i < maxLength; i++)
                            {
                                object fileName = dgv_Topic_FileList.Rows[i].Cells[$"{key}name"].Value;
                                if(fileName != null)
                                {
                                    DataGridViewRow row = dgv_Topic_FileList.Rows[i];
                                    object fileId = AddFileInfo(key, row, objId, row.Index);
                                    row.Cells[$"{key}id"].Tag = fileId;
                                }
                            }
                            RemoveFileList(objId);

                            XtraMessageBox.Show("信息保存成功。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                            GoToTreeList();
                        }
                        else
                            XtraMessageBox.Show("文件信息存在错误数据，请先更正。", "保存失败", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                }
                else if(objId != null)
                {
                    if(index == 1)//文件核查
                    {
                        if(CheckValidMustEnter(dgv_Topic_FileValid, "topic_fc_"))
                        {
                            ModifyFileValid(dgv_Topic_FileValid, objId, "topic_fc_");
                            XtraMessageBox.Show("文件核查信息保存成功。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        }
                        else
                            XtraMessageBox.Show("请填写完整信息。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                    else if(index == 2)
                    {

                    }
                }
            }
            else if("btn_Subject_Save".Equals(button.Name))
            {
                object objId = tab_Subject_Info.Tag;
                view = dgv_Subject_FileList;
                key = "subject_fl_";
                int index = tab_Subject_Info.SelectedTabPageIndex;
                if(index == 0)
                {
                    if(CheckMustEnter(button.Name, objId))
                    {
                        if(objId != null)
                            UpdateBasicInfo(objId, ControlType.Subject);
                        else
                            objId = view.Tag = AddProjectBasicInfo(pal_Subject.Tag, ControlType.Subject);
                        if(CheckFileList(view.Rows, key))
                        {
                            int maxLength = dgv_Subject_FileList.Rows.Count - 1;
                            for(int i = 0; i < maxLength; i++)
                            {
                                object fileName = dgv_Subject_FileList.Rows[i].Cells[$"{key}name"].Value;
                                if(fileName != null)
                                {
                                    DataGridViewRow row = dgv_Subject_FileList.Rows[i];
                                    object fileId = AddFileInfo(key, row, objId, row.Index);
                                    row.Cells[$"{key}id"].Tag = fileId;
                                }
                            }
                            RemoveFileList(objId);

                            XtraMessageBox.Show("信息保存成功。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                            GoToTreeList();
                        }
                        else
                            XtraMessageBox.Show("文件信息存在错误数据，请先更正。", "保存失败", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                }
                else if(objId != null)
                {
                    if(index == 1)//文件核查
                    {
                        if(CheckValidMustEnter(dgv_Subject_FileValid, "subject_fc_"))
                        {
                            ModifyFileValid(dgv_Subject_FileValid, objId, "subject_fc_");
                            XtraMessageBox.Show("文件核查信息保存成功。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        }
                        else
                            XtraMessageBox.Show("请填写完整信息。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                    else if(index == 2)
                    {

                    }
                }
            }
            else if("btn_Imp_Save".Equals(button.Name))
            {
                object objId = tab_Imp_Info.Tag;
                view = dgv_Imp_FileList;
                key = "imp_fl_";
                int index = tab_Imp_Info.SelectedTabPageIndex;
                if(index == 0)
                {
                    if(objId == null)
                    {
                        objId = view.Tag = AddProjectBasicInfo(objId, ControlType.Imp);
                        XtraMessageBox.Show("基础信息保存成功。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                    if(CheckFileList(view.Rows, key))
                    {
                        int maxLength = dgv_Imp_FileList.Rows.Count - 1;
                        for(int i = 0; i < maxLength; i++)
                        {
                            object fileName = dgv_Imp_FileList.Rows[i].Cells[$"{key}name"].Value;
                            if(fileName != null)
                            {
                                DataGridViewRow row = dgv_Imp_FileList.Rows[i];
                                object fileId = AddFileInfo(key, row, objId, row.Index);
                                row.Cells[$"{key}id"].Tag = fileId;
                            }
                        }
                        RemoveFileList(objId);

                        XtraMessageBox.Show("信息保存成功。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        GoToTreeList();
                    }
                    else
                        XtraMessageBox.Show("文件信息存在错误数据，请先更正。", "保存失败", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
                else if(objId != null)
                {
                    if(index == 1)//文件核查
                    {
                        if(CheckValidMustEnter(dgv_Imp_FileValid, "imp_fc_"))
                        {
                            ModifyFileValid(dgv_Imp_FileValid, objId, "imp_fc_");
                            XtraMessageBox.Show("文件核查信息保存成功。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        }
                        else
                            XtraMessageBox.Show("请填写完整信息。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                    else if(index == 2)
                    {

                    }
                }
            }
            else if("btn_Special_Save".Equals(button.Name))
            {
                object objId = tab_Special_Info.Tag;
                view = dgv_Special_FileList;
                key = "special_fl_";
                int index = tab_Special_Info.SelectedTabPageIndex;
                if(index == 0)
                {
                    if(objId == null)
                        objId = view.Tag = AddProjectBasicInfo(pal_Special.Tag, ControlType.Special);
                    if(CheckFileList(view.Rows, key))
                    {
                        int maxLength = dgv_Special_FileList.Rows.Count - 1;
                        for(int i = 0; i < maxLength; i++)
                        {
                            object fileName = dgv_Special_FileList.Rows[i].Cells[$"{key}name"].Value;
                            if(fileName != null)
                            {
                                DataGridViewRow row = dgv_Special_FileList.Rows[i];
                                object fileId = AddFileInfo(key, row, objId, row.Index);
                                row.Cells[$"{key}id"].Tag = fileId;
                            }
                        }
                        RemoveFileList(objId);

                        XtraMessageBox.Show("信息保存成功。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        GoToTreeList();
                    }
                    else
                        XtraMessageBox.Show("文件信息存在错误数据，请先更正。", "保存失败", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
                else if(objId != null)
                {
                    if(index == 1)
                    {
                        if(CheckValidMustEnter(dgv_Special_FileValid, "special_fc_"))
                        {
                            ModifyFileValid(dgv_Special_FileValid, objId, "special_fc_");
                            XtraMessageBox.Show("文件核查信息保存成功。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        }
                        else
                            XtraMessageBox.Show("请填写完整信息。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                    else if(index == 2)
                    {

                    }
                }
            }
        }

        private bool CheckMustEnter(string name, object pid)
        {
            errorProvider1.Clear();
            bool result = true;
            if(name.Contains("Project"))
            {
                string proCode = txt_Project_Code.Text.Trim();
                if(string.IsNullOrEmpty(proCode))
                {
                    errorProvider1.SetError(txt_Project_Code, "提示：项目编号不能为空");
                    result = false;
                }
                else if(tab_Project_Info.Tag == null)
                {
                    int count = SqlHelper.ExecuteCountQuery($"SELECT COUNT(pi_id) FROM project_info WHERE pi_code='{proCode}' AND pi_obj_id='{pid}';");
                    if(count > 0)
                    {
                        errorProvider1.SetError(txt_Project_Code, "提示：此项目编号已存在");
                        result = false;
                    }
                }

                string funds = txt_Project_Funds.Text;
                if(!string.IsNullOrEmpty(funds))
                {
                    if(!Regex.IsMatch(funds, "^[0-9]+(.[0-9]{2})?$"))
                    {
                        errorProvider1.SetError(txt_Project_Funds, "提示：请输入合法经费");
                        result = false;
                    }
                }

                string startDate = txt_Project_StartTime.Text;
                if(!string.IsNullOrEmpty(startDate))
                {
                    if(!Regex.IsMatch(startDate, "\\d{4}-\\d{2}-\\d{2}"))
                    {
                        errorProvider1.SetError(dtp_Project_StartTime, "提示：请输入yyyy-MM-dd格式的日期");
                        result = false;
                    }
                    else
                    {
                        if(!DateTime.TryParse(startDate, out DateTime time))
                        {
                            errorProvider1.SetError(dtp_Project_StartTime, "提示：请输入保留两位小数的合法经费");
                            result = false;
                        }
                    }
                }
                string endDate = txt_Project_EndTime.Text;
                if(!string.IsNullOrEmpty(endDate))
                {
                    if(!Regex.IsMatch(endDate, "\\d{4}-\\d{2}-\\d{2}"))
                    {
                        errorProvider1.SetError(dtp_Project_EndTime, "提示：请输入yyyy-MM-dd格式的日期");
                        result = false;
                    }
                    else
                    {
                        if(!DateTime.TryParse(endDate, out DateTime time))
                        {
                            errorProvider1.SetError(dtp_Project_EndTime, "提示：请输入合法的日期");
                            result = false;
                        }
                    }
                }
            }
            else if(name.Contains("Topic"))
            {
                string topCode = txt_Topic_Code.Text;
                if(string.IsNullOrEmpty(topCode))
                {
                    errorProvider1.SetError(txt_Topic_Code, "提示：课题编号不能为空");
                    result = false;
                }
                else if(tab_Topic_Info.Tag == null)
                {
                    int count = SqlHelper.ExecuteCountQuery($"SELECT COUNT(ti_id) FROM topic_info WHERE ti_code='{topCode}' AND ti_obj_id='{pid}';");
                    if(count > 0)
                    {
                        errorProvider1.SetError(txt_Topic_Code, "提示：此课题编号已存在");
                        result = false;
                    }
                }
                if(string.IsNullOrEmpty(txt_Topic_Year.Text))
                {
                    errorProvider1.SetError(txt_Topic_Year, "提示：立项年度不能为空");
                    result = false;
                }
                if(string.IsNullOrEmpty(txt_Topic_Unit.Text))
                {
                    errorProvider1.SetError(txt_Topic_Unit, "提示：承担单位不能为空");
                    result = false;
                }
                if(string.IsNullOrEmpty(txt_Topic_ProUser.Text))
                {
                    errorProvider1.SetError(txt_Topic_ProUser, "提示：负责人不能为空");
                    result = false;
                }

                string funds = txt_Topic_Fund.Text;
                if(!string.IsNullOrEmpty(funds))
                {
                    if(!Regex.IsMatch(funds, "^[0-9]+(.[0-9]{2})?$"))
                    {
                        errorProvider1.SetError(txt_Topic_Fund, "提示：请输入合法经费");
                        result = false;
                    }
                }

                string startDate = txt_Topic_StartTime.Text;
                if(!string.IsNullOrEmpty(startDate))
                {
                    if(!Regex.IsMatch(startDate, "\\d{4}-\\d{2}-\\d{2}"))
                    {
                        errorProvider1.SetError(dtp_Topic_StartTime, "提示：请输入yyyy-MM-dd格式的日期");
                        result = false;
                    }
                    else
                    {
                        if(!DateTime.TryParse(startDate, out DateTime time))
                        {
                            errorProvider1.SetError(dtp_Topic_StartTime, "提示：请输入合法的日期");
                            result = false;
                        }
                    }
                }
                string endDate = txt_Topic_EndTime.Text;
                if(!string.IsNullOrEmpty(endDate))
                {
                    if(!Regex.IsMatch(endDate, "\\d{4}-\\d{2}-\\d{2}"))
                    {
                        errorProvider1.SetError(dtp_Topic_EndTime, "提示：请输入yyyy-MM-dd格式的日期");
                        result = false;
                    }
                    else
                    {
                        if(!DateTime.TryParse(endDate, out DateTime time))
                        {
                            errorProvider1.SetError(dtp_Topic_EndTime, "提示：请输入合法的日期");
                            result = false;
                        }
                    }
                }
            }
            else if(name.Contains("Subject"))
            {
                string subCode = txt_Subject_Code.Text.Trim();
                if(string.IsNullOrEmpty(subCode))
                {
                    errorProvider1.SetError(txt_Subject_Code, "提示：课题编号不能为空");
                    result = false;
                }
                if(tab_Subject_Info.Tag == null)
                {
                    int count = SqlHelper.ExecuteCountQuery($"SELECT COUNT(si_id) FROM subject_info WHERE si_code='{subCode}' WHERE si_obj_id='{pid}';");
                    if(count > 0)
                    {
                        errorProvider1.SetError(txt_Subject_Code, "提示：子课题编号已存在");
                        result = false;
                    }
                }
                if(string.IsNullOrEmpty(txt_Subject_Year.Text))
                {
                    errorProvider1.SetError(txt_Subject_Year, "提示：立项年度不能为空");
                    result = false;
                }
                if(string.IsNullOrEmpty(txt_Subject_Unit.Text))
                {
                    errorProvider1.SetError(txt_Subject_Unit, "提示：承担单位不能为空");
                    result = false;
                }
                if(string.IsNullOrEmpty(txt_Subject_ProUser.Text))
                {
                    errorProvider1.SetError(txt_Subject_ProUser, "提示：负责人不能为空");
                    result = false;
                }

                string funds = txt_Subject_Fund.Text;
                if(!string.IsNullOrEmpty(funds))
                {
                    if(!Regex.IsMatch(funds, "^[0-9]+(.[0-9]{2})?$"))
                    {
                        errorProvider1.SetError(txt_Subject_Fund, "提示：请输入合法经费");
                        result = false;
                    }
                }

                string startDate = txt_Subject_StartTime.Text;
                if(!string.IsNullOrEmpty(startDate))
                {
                    if(!Regex.IsMatch(startDate, "\\d{4}-\\d{2}-\\d{2}"))
                    {
                        errorProvider1.SetError(dtp_Subject_StartTime, "提示：请输入yyyy-MM-dd格式的日期");
                        result = false;
                    }
                    else
                    {
                        if(!DateTime.TryParse(startDate, out DateTime time))
                        {
                            errorProvider1.SetError(dtp_Subject_StartTime, "提示：请输入合法的日期");
                            result = false;
                        }
                    }
                }
                string endDate = txt_Subject_EndTime.Text;
                if(!string.IsNullOrEmpty(endDate))
                {
                    if(!Regex.IsMatch(endDate, "\\d{4}-\\d{2}-\\d{2}"))
                    {
                        errorProvider1.SetError(dtp_Subject_EndTime, "提示：请输入yyyy-MM-dd格式的日期");
                        result = false;
                    }
                    else
                    {
                        if(!DateTime.TryParse(endDate, out DateTime time))
                        {
                            errorProvider1.SetError(dtp_Subject_EndTime, "提示：请输入合法的日期");
                            result = false;
                        }
                    }
                }
            }
            else if(name.Contains("Special"))
            {
                if(string.IsNullOrEmpty(txt_Special_Unit.Text))
                {
                    errorProvider1.SetError(txt_Special_Unit, "提示：牵头组织单位不能为空");
                    result = false;
                }
            }
            return result;
        }

        private bool CheckValidMustEnter(DataGridView view, string key)
        {
            bool result = true;
            foreach(DataGridViewRow row in view.Rows)
            {
                object reason = row.Cells[key + "reason"].Value;
                object remark = row.Cells[key + "remark"].Value;
                object flag = row.Tag;
                if(flag != null && (reason == null || remark == null))
                {
                    row.Cells[key + "reason"].ErrorText = "提示：此类型为必存文件，请说明缺失原因。";
                    result = false;
                }
                else
                    row.Cells[key + "reason"].ErrorText = string.Empty;
            }
            return result;
        }

        private void RemoveFileList(object objId)
        {
            string fileString = string.Empty;
            for(int i = 0; i < removeIdList.Count; i++)
            {
                if(removeIdList[i] != null)
                {
                    //收集文件号（供重新选取）
                    object fileId = SqlHelper.ExecuteOnlyOneQuery($"SELECT pfl_file_id FROM processing_file_list WHERE pfl_id='{removeIdList[i]}';");
                    if(fileId != null)
                        fileString += $"'{fileId}',";

                    //删除当前文件
                    SqlHelper.ExecuteNonQuery($"DELETE FROM processing_file_list WHERE pfl_id='{removeIdList[i]}';");
                }
            }

            //重置文件备份表中的状态为0
            if(!string.IsNullOrEmpty(fileString))
            {
                fileString = fileString.Substring(0, fileString.Length - 1);
                SqlHelper.ExecuteNonQuery($"UPDATE backup_files_info SET bfi_state=0 WHERE bfi_id IN ({fileString});");
            }
            removeIdList.Clear();
        }

        private void GoToTreeList()
        {
            if(workType == WorkType.PaperWork)
            {
                if(controlType == ControlType.Plan)
                    LoadTreeList(objId, ControlType.Plan);
                else
                    LoadTreeList(lbl_Imp_Name.Tag, ControlType.Special);
            }
            else
                LoadTreeList(dgv_Plan_FileList.Tag, ControlType.Default);
        }

        /// <summary>
        /// 检验文件列表是否可以保存
        /// </summary>
        private bool CheckFileList(DataGridViewRowCollection rows, string key)
        {
            bool result = true;
            for(int i = 0; i < rows.Count - 1; i++)
            {
                DataGridViewCell cellName = rows[i].Cells[key + "name"];
                if(cellName.Value == null || string.IsNullOrEmpty(ToolHelper.GetValue(cellName.Value).Trim()))
                {
                    cellName.ErrorText = "温馨提示：文件名不能为空。";
                    result = false;
                }
                else
                {
                    cellName.ErrorText = null;
                    for(int j = i + 1; j < rows.Count - 1; j++)
                    {
                        DataGridViewCell cell2 = rows[j].Cells[key + "name"];
                        if(cellName.Value.Equals(cell2.Value))
                        {
                            cellName.ErrorText = $"温馨提示：与{j + 1}行的文件名重复。";
                            result = false;
                        }
                        else
                        {
                            cellName.ErrorText = null;
                        }
                    }
                }

                //检测文件编号重复
                DataGridViewCell cellCode = rows[i].Cells[key + "code"];
                if(string.IsNullOrEmpty(ToolHelper.GetValue(cellCode.Value)))
                {
                    cellCode.ErrorText = "温馨提示：文件编号不能为空。";
                    result = false;
                }
                else
                {
                    cellCode.ErrorText = null;
                    for(int j = i + 1; j < rows.Count - 1; j++)
                    {
                        DataGridViewCell cell2 = rows[j].Cells[key + "code"];
                        if(cellCode.Value.Equals(cell2.Value))
                        {
                            cellCode.ErrorText = $"温馨提示：与{j + 1}行的文件编号重复。";
                            result = false;
                            break;
                        }
                        else
                            cellCode.ErrorText = null;
                    }
                }

                DataGridViewCell pagesCell = rows[i].Cells[key + "pages"];
                if(pagesCell.Value == null || string.IsNullOrEmpty(ToolHelper.GetValue(pagesCell.Value)) || Convert.ToInt32(pagesCell.Value) == 0)
                {
                    pagesCell.ErrorText = "温馨提示：页数不能为0或空。";
                    result = false;
                }
                else
                {
                    bool flag = int.TryParse(ToolHelper.GetValue(pagesCell.Value), out int page);
                    if(!flag)
                    {
                        pagesCell.ErrorText = "温馨提示：页数不能为0。";
                        result = false;
                    }
                    else
                    {
                        if(page > 9999)
                        {
                            pagesCell.ErrorText = "温馨提示：页数不能超过4位数。";
                            result = false;
                        }
                        else
                            pagesCell.ErrorText = null;
                    }
                }

                //份数
                DataGridViewCell countCell = rows[i].Cells[key + "count"];
                if(!string.IsNullOrEmpty(ToolHelper.GetValue(countCell.Value)))
                {
                    bool flag = int.TryParse(ToolHelper.GetValue(countCell.Value), out int page);
                    if(!flag)
                    {
                        countCell.ErrorText = "温馨提示：请输入有效数字。";
                        result = false;
                    }
                    else
                        countCell.ErrorText = null;
                }

                //份数移交
                DataGridViewCell amountCell = rows[i].Cells[key + "amount"];
                if(!string.IsNullOrEmpty(ToolHelper.GetValue(amountCell.Value)))
                {
                    bool flag = int.TryParse(ToolHelper.GetValue(amountCell.Value), out int page);
                    if(!flag)
                    {
                        amountCell.ErrorText = "温馨提示：请输入有效数字。";
                        result = false;
                    }
                    else
                        amountCell.ErrorText = null;
                }

                bool isOtherType = "其他".Equals(ToolHelper.GetValue(rows[i].Cells[key + "categor"].FormattedValue).Trim());
                DataGridViewCell cellCategor = rows[i].Cells[key + "categorname"];
                if(isOtherType)
                {
                    if(cellCategor.Value == null || string.IsNullOrEmpty(ToolHelper.GetValue(cellCategor.Value).Trim()))
                    {
                        cellCategor.ErrorText = "温馨提示：类型名称不能为空。";
                        result = false;
                    }
                    else
                        cellCategor.ErrorText = null;
                }
                else
                    cellCategor.ErrorText = null;

                DataGridViewCell dateCell = rows[i].Cells[key + "date"];
                if(!string.IsNullOrEmpty(ToolHelper.GetValue(dateCell.Value)))
                {
                    if(!Regex.IsMatch(ToolHelper.GetValue(dateCell.Value), "\\d{4}-\\d{2}-\\d{2}"))
                    {
                        dateCell.ErrorText = "提示：请输入格式为 yyyy-MM-dd 的有效日期。";
                        result = false;
                    }
                    else
                    {
                        bool flag = DateTime.TryParse(ToolHelper.GetValue(dateCell.Value), out DateTime date);
                        if(!flag)
                        {
                            dateCell.ErrorText = "提示：请输入格式为 yyyy-MM-dd 的有效日期。";
                            result = false;
                        }
                        else
                            dateCell.ErrorText = null;
                    }
                }
            }
            return result;
        }
      
        /// <summary>
        /// 修改或保存指定文件校验列表
        /// </summary>
        /// <param name="dataGridView">指定的表格</param>
        /// <param name="objid">主键</param>
        /// <param name="key">关键字</param>
        private void ModifyFileValid(DataGridView dataGridView, object objid, string key)
        {
            int rowCount = dataGridView.Rows.Count;
            StringBuilder sqlString = new StringBuilder();
            sqlString.Append($"DELETE FROM processing_file_lost WHERE pfo_obj_id='{objid}';");
            for(int i = 0; i < rowCount; i++)
            {
                DataGridViewRow row = dataGridView.Rows[i];
                object name = row.Cells[key + "name"].Value;
                if(name != null)
                {
                    object reason = row.Cells[key + "reason"].Value;
                    object remark = row.Cells[key + "remark"].Value;
                    object categor = row.Cells[key + "categor"].Value;
                    string _categor = ToolHelper.GetValue(categor);
                    if(!string.IsNullOrEmpty(_categor))
                    {
                        string[] _temp = _categor.Split(' ');
                        if(_temp.Length > 0 && !string.IsNullOrEmpty(_temp[0].Trim()))
                            _categor = _temp[0];
                    }
                    object rid = dataGridView.Rows[i].Cells[key + "id"].Tag;
                    if(rid != null)
                        sqlString.Append($"DELETE FROM processing_file_lost WHERE pfo_id='{rid}';");
                    rid = Guid.NewGuid().ToString();
                    sqlString.Append($"INSERT INTO processing_file_lost VALUES('{rid}','{_categor}','{name}','{reason}','{remark}','{objid}');");
                    dataGridView.Rows[i].Cells[key + "id"].Tag = rid;
                }
            }
            SqlHelper.ExecuteNonQuery(sqlString.ToString());
        }
  
        /// <summary>
        /// 更新 项目/课题基本信息
        /// </summary>
        /// <param name="objid">主键</param>
        /// <param name="controlType">操作对象类型</param>
        private void UpdateBasicInfo(object objid, ControlType controlType)
        {
           if(controlType == ControlType.Project)
            {
                string code = txt_Project_Code.Text;
                string name = txt_Project_Name.Text;
                string type = string.Empty;
                string filed = txt_Project_Field.Text;
                string theme = txt_Project_Theme.Text;
                string funds = txt_Project_Funds.Text;
                string starttime = txt_Project_StartTime.Text;
                string endtime = txt_Project_EndTime.Text;
                string year = txt_Project_Year.Text;
                object unit = txt_Project_Unit.Text;
                object province = txt_Project_Province.Text;
                string unituser = txt_Project_UnitUser.Text;
                string objuser = txt_Project_ProUser.Text;
                string intro = txt_Project_Intro.Text;

                string updateSql = "UPDATE project_info SET " +
                    $"pi_code = '{code}'" +
                    $",pi_name = '{name}' " +
                    $",pi_field = '{filed}'" +
                    $",pb_theme = '{theme}'" +
                    $",pi_funds = '{funds}'" +
                    $",pi_start_datetime = '{starttime}'" +
                    $",pi_end_datetime = '{endtime}'" +
                    $",pi_year = '{year}'" +
                    $",pi_unit = '{unit}'" +
                    $",pi_province = '{province}'" +
                    $",pi_uniter = '{unituser}'" +
                    $",pi_prouser = '{objuser}'" +
                    $",pi_intro = '{intro}'" +
                    $" WHERE pi_id='{objid}'";
                SqlHelper.ExecuteNonQuery(updateSql);
            }
            else if(controlType == ControlType.Topic)
            {
                string code = txt_Topic_Code.Text;
                string name = txt_Topic_Name.Text;
                string field = txt_Topic_Field.Text;
                string theme = txt_Topic_Theme.Text;
                string funds = txt_Topic_Fund.Text;
                string starttime = txt_Topic_StartTime.Text;
                string endtime = txt_Topic_EndTime.Text;
                string year = txt_Topic_Year.Text;
                object unit = txt_Topic_Unit.Text;
                object province = txt_Topic_Province.Text;
                string unituser = txt_Topic_UnitUser.Text;
                string objuser = txt_Topic_ProUser.Text;
                string intro = txt_Topic_Intro.Text;

                string updateSql = "UPDATE topic_info SET " +
                    $"ti_code = '{code}'" +
                    $",ti_name = '{name}' " +
                    $",ti_field = '{field}' " +
                    $",tb_theme = '{theme}'" +
                    $",ti_funds = '{funds}'" +
                    $",ti_start_datetime = '{starttime}'" +
                    $",ti_end_datetime = '{endtime}'" +
                    $",ti_year = '{year}'" +
                    $",ti_unit = '{unit}'" +
                    $",ti_province = '{province}'" +
                    $",ti_uniter = '{unituser}'" +
                    $",ti_prouser = '{objuser}'" +
                    $",ti_intro = '{intro}'" +
                    $" WHERE ti_id='{objid}'";
                SqlHelper.ExecuteNonQuery(updateSql);
            }
            else if(controlType == ControlType.Subject)
            {
                string code = txt_Subject_Code.Text;
                string name = txt_Subject_Name.Text;
                string field = txt_Subject_Field.Text;
                string theme = txt_Subject_Theme.Text;
                string fund = txt_Subject_Fund.Text;
                string starttime = txt_Subject_StartTime.Text;
                string endtime = txt_Subject_EndTime.Text;
                string year = txt_Subject_Year.Text;
                object unit = txt_Subject_Unit.Text;
                string unituser = txt_Subject_Unituser.Text;
                string objuser = txt_Subject_ProUser.Text;
                object province = txt_Subject_Province.Text;
                string intro = txt_Subject_Intro.Text;

                string updateSql = "UPDATE subject_info SET " +
                    $"[si_code] = '{code}'" +
                    $",[si_name] = '{name}'" +
                    $",[si_field] = '{field}'" +
                    $",[si_theme] = '{theme}'" +
                    $",[si_funds] = '{fund}'" +
                    $",[si_start_datetime] = '{starttime}'" +
                    $",[si_end_datetime] = '{endtime}'" +
                    $",[si_year] = '{year}'" +
                    $",[si_unit] = '{unit}'" +
                    $",[si_province] = '{province}'" +
                    $",[si_uniter] = '{unituser}'" +
                    $",[si_prouser] = '{objuser}'" +
                    $",[si_intro] = '{intro}'" +
                    $" WHERE si_id='{objid}'";
                SqlHelper.ExecuteNonQuery(updateSql);
            }
            else if(controlType == ControlType.Special)
            {
                string code = txt_Special_Code.Text;
                string name = txt_Special_Name.Text;
                string unit = txt_Special_Unit.Text;

                string updateSql = "UPDATE imp_dev_info SET " +
                    $"imp_code = '{code}'" +
                    $",imp_name = '{name}'" +
                    $",imp_unit = '{unit}'" +
                    $" WHERE imp_id = '{objid}'";
                SqlHelper.ExecuteNonQuery(updateSql);
            }
        }

        private string GetFloatValue(string text, int length)
        {
            if(!string.IsNullOrEmpty(text))
            {
                if(float.TryParse(text, out float result))
                {
                    string format = $"0.{"0".PadLeft(length, '0')}";
                    return result.ToString(format);
                }
                else
                    return string.Empty;
            }
            return string.Empty;
        }

        /// <summary>
        /// 保存 计划-项目 基本信息
        /// </summary>
        /// <param name="parentId">父对象ID</param>
        /// <param name="type">对象类型</param>
        /// <returns>对象生成的主键</returns>
        private object AddProjectBasicInfo(object parentId, ControlType type)
        {
            string primaryKey = Guid.NewGuid().ToString();
            if(type == ControlType.Project)
            {
                string code = txt_Project_Code.Text;
                string name = txt_Project_Name.Text.Replace("'", "''");
                string planType = string.Empty;
                string ly = txt_Project_Field.Text;
                string zt = txt_Project_Theme.Text;
                string funds = GetFloatValue(txt_Project_Funds.Text, 2);
                DateTime starttime = dtp_Project_StartTime.Value;
                DateTime endtime = dtp_Project_EndTime.Value;
                string year = txt_Project_Year.Text;
                object unit = txt_Project_Unit.Text;
                object province = txt_Project_Province.Text;
                string unituser = txt_Project_UnitUser.Text;
                string objuser = txt_Project_ProUser.Text;
                string intro = txt_Project_Intro.Text.Replace("'", "''");

                string insertSql = "INSERT INTO project_info(pi_id ,trc_id,pi_code,pi_name,pi_type,pb_belong" +
                    ",pb_belong_type,pi_money,pi_start_datetime,pi_end_datetime,pi_year,pi_company_id,pi_company_user" +
                    ",pi_province, pi_project_user, pi_introduction, pi_work_status, pi_obj_id, pi_categor, pi_submit_status, pi_worker_id)" +
                    "VALUES" +
                    $"('{primaryKey}',null,'{code}','{name}','{planType}','{ly}','{zt}','{funds}','{starttime}'" +
                    $",'{endtime}','{year}','{unit}','{unituser}'" +
                    $",'{province}','{objuser}','{intro}','{(int)WorkStatus.Default}','{parentId}',{(int)type}, 1,'{UserHelper.GetUser().UserKey}')";

                SqlHelper.ExecuteNonQuery(insertSql);
            }
            else if(type == ControlType.Topic)
            {
                string code = txt_Topic_Code.Text;
                string name = txt_Topic_Name.Text.Replace("'", "''");
                string planType = string.Empty;
                string ly = txt_Topic_Field.Text;
                string zt = txt_Topic_Theme.Text;
                string funds = GetFloatValue(txt_Topic_Fund.Text, 2);
                DateTime starttime = dtp_Topic_StartTime.Value;
                DateTime endtime = dtp_Topic_EndTime.Value;
                string year = txt_Topic_Year.Text;
                object unit = txt_Topic_Unit.Text;
                object province = txt_Topic_Province.Text;
                string unituser = txt_Topic_UnitUser.Text;
                string objuser = txt_Topic_ProUser.Text;
                string intro = txt_Topic_Intro.Text;
                //判断是直属课题【-3】还是项目下的课题【3】
                int categorType = SqlHelper.ExecuteCountQuery($"SELECT COUNT(pi_id) FROM project_info WHERE pi_categor=2 AND pi_id='{parentId}'");
                int categor = categorType > 0 ? (int)type : -(int)type;
                string insertSql = "INSERT INTO topic_info(ti_id, trc_id, ti_code, ti_name, ti_type, tb_belong, tb_belong_type, ti_money, ti_start_datetime, ti_end_datetime, ti_year, ti_company_id, ti_company_user" +
                    ",ti_province, ti_project_user, ti_introduction, ti_work_status, ti_obj_id, ti_categor, ti_submit_status, ti_worker_id)" +
                    "VALUES" +
                    $"('{primaryKey}',null,'{code}','{name}','{planType}','{ly}','{zt}','{funds}','{starttime}'" +
                    $",'{endtime}','{year}','{unit}','{unituser}'" +
                    $",'{province}', '{objuser}', '{intro}', '{0}', '{parentId}', '{categor}', '1', '{UserHelper.GetUser().UserKey}')";

                SqlHelper.ExecuteNonQuery(insertSql);
            }
            else if(type == ControlType.Subject)
            {
                string code = txt_Subject_Code.Text;
                string name = txt_Subject_Name.Text.Replace("'", "''");
                string planType = string.Empty;
                string ly = txt_Subject_Field.Text;
                string zt = txt_Subject_Theme.Text;
                string funds = GetFloatValue(txt_Subject_Fund.Text, 2);
                DateTime starttime = dtp_Subject_StartTime.Value;
                DateTime endtime = dtp_Subject_EndTime.Value;
                string year = txt_Subject_Year.Text;
                object unit = txt_Subject_Unit.Text;
                string unituser = txt_Subject_Unituser.Text;
                string objuser = txt_Subject_ProUser.Text;
                object province = txt_Subject_Province.Text;
                string intro = txt_Subject_Intro.Text.Replace("'", "''");

                string insertSql = "INSERT INTO subject_info(si_id, pi_id, si_code, si_name, si_type, si_field, si_belong, si_money, si_start_datetime," +
                   "si_end_datetime, si_year, si_company, si_company_user, si_province, si_project_user, si_introduction, si_work_status, si_categor, si_submit_status," +
                   "si_worker_id) VALUES " +
                   $"('{primaryKey}','{parentId}','{code}','{name}','{planType}','{ly}','{zt}','{funds}'" +
                   $",'{starttime}','{endtime}','{year}','{unit}','{unituser}','{province}','{objuser}'" +
                   $",'{intro}','{(int)WorkStatus.NonWork}','{(int)type}',{(int)ObjectSubmitStatus.NonSubmit},'{UserHelper.GetUser().UserKey}')";
                SqlHelper.ExecuteNonQuery(insertSql);
            }
            else if(type == ControlType.Imp)
            {
                string name = lbl_Imp_Name.Text;
                object intro = txt_Imp_Intro.Text;
                string insertSql = "INSERT INTO imp_info(imp_id, imp_code, imp_name, imp_intro, pi_categor, imp_submit_status, imp_obj_id, imp_source_id, imp_type) " +
                    $"VALUES ('{primaryKey}', '{planCode}', '{name}', '{intro}', '{(int)type}', '{(int)ObjectSubmitStatus.NonSubmit}', '{parentId}', '{UserHelper.GetUser().UserKey}', {DEV_TYPE})";
                SqlHelper.ExecuteNonQuery(insertSql);
            }
            else if(type == ControlType.Special)
            {
                string code = txt_Special_Code.Text;
                string name = txt_Special_Name.Text;
                string unit = txt_Special_Unit.Text;

                string insertSql = "INSERT INTO imp_dev_info VALUES " +
                    $"('{primaryKey}'" +
                    $",'{code}'" +
                    $",'{name}'" +
                    $",'{unit}'" +
                    $",null" +
                    $",'{(int)ControlType.Special}'" +
                    $",'{(int)SubmitStatus.NonSubmit}'" +
                    $",'{parentId}'" +
                    $",'{UserHelper.GetUser().UserKey}')";
                SqlHelper.ExecuteNonQuery(insertSql);
            }
            return primaryKey;
        }
        
        /// <summary>
        /// 新增文件信息
        /// </summary>
        /// <param name="key">当前表格列名前缀</param>
        /// <param name="row">当前待保存的行</param>
        /// <param name="parentId">父对象ID</param>
        /// <returns>新增信息主键</returns>
        private object AddFileInfo(string key, DataGridViewRow row, object parentId, int sort)
        {
            string sqlString = string.Empty;
            object _fileId = row.Cells[key + "id"].Tag;
            object boxId = null, fileId = null, link = null, remark = null, boxSort = null, workDate = DateTime.Now;
            if(_fileId != null)
            {
                DataRow param = SqlHelper.ExecuteSingleRowQuery($"SELECT pfl_box_id, pfl_link, pfl_file_id, pfl_remark, pfl_box_sort, pfl_worker_date FROM processing_file_list WHERE pfl_id='{_fileId}'");
                if(param != null)
                {
                    boxId = param["pfl_box_id"];
                    link = param["pfl_link"];
                    fileId = param["pfl_file_id"];
                    remark = param["pfl_remark"];
                    boxSort = param["pfl_box_sort"];
                    workDate = param["pfl_worker_date"];
                }
                sqlString += $"DELETE FROM processing_file_list WHERE pfl_id='{_fileId}';";
            }
            else
                _fileId = Guid.NewGuid().ToString();
            object stage = row.Cells[key + "stage"].Value;
            object categor = row.Cells[key + "categor"].Value;
            object categorName = row.Cells[key + "categorname"].Value;
            object name = ToolHelper.GetValue(row.Cells[key + "name"].Value).Replace("'", "''");
            object user = row.Cells[key + "user"].Value;
            object type = row.Cells[key + "type"].Value;
            object pages = row.Cells[key + "pages"].Value;
            object count = row.Cells[key + "count"].Value;
            object amount = row.Cells[key + "amount"].Value;
            object code = row.Cells[key + "code"].Value;
            DateTime now = DateTime.MinValue;
            string _date = ToolHelper.GetValue(row.Cells[key + "date"].Value);
            if(!string.IsNullOrEmpty(_date))
            {
                if(_date.Length == 4)
                    _date = _date + "-" + now.Month + "-" + now.Day;
                else if(_date.Length == 6)
                    _date = _date.Substring(0, 4) + "-" + _date.Substring(4, 2) + "-" + now.Day;
                else if(_date.Length == 8)
                    _date = _date.Substring(0, 4) + "-" + _date.Substring(4, 2) + "-" + _date.Substring(6, 2);
                DateTime.TryParse(_date, out now);
            }
            object date = now == DateTime.MinValue ? null : now.ToString();
            object unit = row.Cells[key + "unit"].Value;
            object carrier = row.Cells[key + "carrier"].Value;

            bool isOtherType = "其他".Equals(ToolHelper.GetValue(row.Cells[key + "categor"].FormattedValue).Trim());
            if(isOtherType)
            {
                categor = Guid.NewGuid().ToString();
                string value = ToolHelper.GetValue(code).Split('-')[0];
                int _sort = ((DataGridViewComboBoxCell)row.Cells[key + "categor"]).Items.Count - 1;

                sqlString += "INSERT INTO data_dictionary (dd_id, dd_name, dd_pId, dd_sort, extend_3, extend_4) " +
                    $"VALUES('{categor}', '{value}', '{stage}', '{_sort}', '{categorName}', '{1}');";
            }
            sqlString += "INSERT INTO processing_file_list (pfl_id, pfl_code, pfl_stage, pfl_categor, pfl_name, pfl_user, pfl_type, pfl_pages, pfl_count, pfl_amount, pfl_date, pfl_unit, pfl_carrier, pfl_link, pfl_file_id, pfl_remark, pfl_obj_id, pfl_box_id, pfl_box_sort, pfl_sort, pfl_worker_id, pfl_worker_date) " +
                $"VALUES( '{_fileId}', '{code}', '{stage}', '{categor}', '{name}', '{user}', '{type}', '{pages}', '{count}', '{amount}', '{date}', '{unit}', '{carrier}', '{link}', '{fileId}', '{remark}', '{parentId}', '{boxId}', '{boxSort}', '{sort}', '{UserHelper.GetUser().UserKey}', '{workDate}');";
            if(fileId != null)
            {
                int value = link == null ? 0 : 1;
                sqlString += $"UPDATE backup_files_info SET bfi_state={value} WHERE bfi_id='{fileId}';";
            }
            SqlHelper.ExecuteNonQuery(sqlString);
            return _fileId;
        }

        /// <summary>
        /// 根据状态获取对应背景色
        /// </summary>
        private Color GetForeColorByState(object state)
        {
            string _str = ToolHelper.GetValue(state);
            if(string.IsNullOrEmpty(_str))
                return DisEnbleColor;
            else
            {
                int index = Convert.ToInt32(_str);
                if(index == 2)
                    return Color.Black;
                else return DisEnbleColor;
            }
        }

        /// <summary>
        /// 加载树
        /// </summary>
        /// <param name="planId">计划ID</param>
        private void LoadTreeList(object planId, ControlType type)
        {
            treeView.BeginUpdate();
            treeView.Nodes.Clear();
            treeView.SelectedNode = null;
            TreeNode treeNode = null;
            //光盘加工
            if(workType == WorkType.CDWork)
            {
                object[] _obj = SqlHelper.ExecuteRowsQuery($"SELECT pi_id,pi_name,pi_categor FROM project_info WHERE pi_id='{planId}' AND pi_worker_id='{UserHelper.GetUser().UserKey}'");
                if(_obj == null)
                    _obj = SqlHelper.ExecuteRowsQuery($"SELECT pi_id,pi_name,pi_categor FROM project_info WHERE pi_obj_id='{objId}'");
                if(_obj == null)
                    _obj = SqlHelper.ExecuteRowsQuery($"SELECT dd_id,dd_name FROM data_dictionary WHERE dd_id='{planId}'");
                treeNode = new TreeNode()
                {
                    Name = _obj[0].ToString(),
                    Text = _obj[1].ToString(),
                    Tag = ControlType.Plan,
                };
                //根据【计划】查询【项目/课题】集
                List<object[]> list = SqlHelper.ExecuteColumnsQuery($"SELECT pi_id,pi_code,pi_categor FROM project_info WHERE pi_obj_id='{treeNode.Name}' AND pi_worker_id='{UserHelper.GetUser().UserKey}' " +
                    $"ORDER BY pi_code", 3);
                for(int i = 0; i < list.Count; i++)
                {
                    TreeNode treeNode2 = new TreeNode()
                    {
                        Name = list[i][0].ToString(),
                        Text = list[i][1].ToString(),
                        Tag = (ControlType)list[i][2]
                    };
                    treeNode.Nodes.Add(treeNode2);
                    //根据【项目/课题】查询【课题/子课题】集
                    List<object[]> list2 = SqlHelper.ExecuteColumnsQuery($"SELECT si_id,si_code,si_categor FROM subject_info WHERE pi_id='{treeNode2.Name}' AND si_worker_id='{UserHelper.GetUser().UserKey}' " +
                        $"ORDER BY si_code", 3);
                    for(int j = 0; j < list2.Count; j++)
                    {
                        TreeNode treeNode3 = new TreeNode()
                        {
                            Name = list2[j][0].ToString(),
                            Text = list2[j][1].ToString(),
                            Tag = (ControlType)list2[j][2]
                        };
                        treeNode2.Nodes.Add(treeNode3);

                        List<object[]> list3 = SqlHelper.ExecuteColumnsQuery($"SELECT si_id,si_code,si_categor FROM subject_info WHERE pi_id='{treeNode3.Name}' AND si_worker_id='{UserHelper.GetUser().UserKey}' " +
                            $"ORDER BY si_id", 3);
                        for(int k = 0; k < list3.Count; k++)
                        {
                            TreeNode treeNode4 = new TreeNode()
                            {
                                Name = list3[k][0].ToString(),
                                Text = list3[k][1].ToString(),
                                Tag = (ControlType)list3[k][2]
                            };
                            treeNode3.Nodes.Add(treeNode4);
                        }
                    }
                }
            }
            //纸本加工
            else if(workType == WorkType.PaperWork)
            {
                DataRow planRow = SqlHelper.ExecuteSingleRowQuery($"SELECT pi_id, pi_name, pi_worker_id, pi_submit_status FROM project_info WHERE pi_id='{planId}'");
                if(planRow != null)
                {
                    treeNode = new TreeNode()
                    {
                        Name = ToolHelper.GetValue(planRow["pi_id"]),
                        Text = ToolHelper.GetValue(planRow["pi_name"]),
                        Tag = ControlType.Plan,
                        ForeColor = GetForeColorByState(planRow["pi_submit_status"]),
                    };
                    if(type != ControlType.Plan)
                    {
                        //根据【计划】查询【项目/课题】集
                        DataTable proTable = SqlHelper.ExecuteQuery($"SELECT pi_id, pi_code, pi_worker_id, pi_submit_status FROM project_info WHERE pi_obj_id='{treeNode.Name}'");
                        foreach(DataRow proRow in proTable.Rows)
                        {
                            TreeNode treeNode2 = new TreeNode()
                            {
                                Name = ToolHelper.GetValue(proRow["pi_id"]),
                                Text = ToolHelper.GetValue(proRow["pi_code"]),
                                Tag = ControlType.Project,
                                ForeColor = GetForeColorByState(proRow["pi_submit_status"]),
                            };
                            treeNode.Nodes.Add(treeNode2);
                            //根据【项目/课题】查询【课题/子课题】集
                            DataTable topTable = SqlHelper.ExecuteQuery($"SELECT ti_id, ti_code, ti_submit_status FROM topic_info WHERE ti_obj_id='{treeNode2.Name}'");
                            foreach(DataRow topRow in topTable.Rows)
                            {
                                TreeNode treeNode3 = new TreeNode()
                                {
                                    Name = ToolHelper.GetValue(topRow["ti_id"]),
                                    Text = ToolHelper.GetValue(topRow["ti_code"]),
                                    Tag = ControlType.Topic,
                                    ForeColor = GetForeColorByState(topRow["ti_submit_status"]),
                                };
                                treeNode2.Nodes.Add(treeNode3);

                                DataTable subTable = SqlHelper.ExecuteQuery($"SELECT si_id, si_code, si_submit_status FROM subject_info WHERE si_obj_id='{treeNode3.Name}'");
                                foreach(DataRow subRow in subTable.Rows)
                                {
                                    TreeNode treeNode4 = new TreeNode()
                                    {
                                        Name = ToolHelper.GetValue(subRow["si_id"]),
                                        Text = ToolHelper.GetValue(subRow["si_code"]),
                                        Tag = ControlType.Subject,
                                        ForeColor = GetForeColorByState(subRow["si_submit_status"]),
                                    };
                                    treeNode3.Nodes.Add(treeNode4);
                                }
                            }
                        }
                    }
                }
            }
            //父级（项目/课题）
            else if(workType == WorkType.ProjectWork)
            {
                object _planId = SqlHelper.ExecuteOnlyOneQuery($"SELECT pi_obj_id FROM project_info WHERE pi_id='{objId}';") ?? SqlHelper.ExecuteOnlyOneQuery($"SELECT ti_obj_id FROM topic_info WHERE ti_id='{objId}';");
                if(_planId != null)
                {
                    DataRow planRow = SqlHelper.ExecuteSingleRowQuery($"SELECT pi_id, pi_name FROM project_info WHERE pi_id='{_planId}' UNION ALL " +
                        $"SELECT dd_id, dd_name FROM data_dictionary WHERE dd_id='{_planId}'");
                    //普通计划
                    if(planRow != null)
                    {
                        treeNode = new TreeNode()
                        {
                            Name = ToolHelper.GetValue(planRow["pi_id"]),
                            Text = ToolHelper.GetValue(planRow["pi_name"]),
                            Tag = ControlType.Plan,
                            ForeColor = DisEnbleColor
                        };
                        //根据【计划】查询【项目/课题】集
                        DataRow row = SqlHelper.ExecuteSingleRowQuery($"SELECT pi_id, pi_code, pi_categor, pi_submit_status, pi_worker_id FROM project_info WHERE pi_id='{objId}';");
                        if(row != null)
                        {
                            TreeNode treeNode2 = new TreeNode()
                            {
                                Name = ToolHelper.GetValue(row["pi_id"]),
                                Text = ToolHelper.GetValue(row["pi_code"]),
                                Tag = ControlType.Project,
                                ForeColor = "1".Equals(ToolHelper.GetValue(row["pi_submit_status"])) ? DisEnbleColor : Color.Black
                            };
                            treeNode.Nodes.Add(treeNode2);
                            //根据【项目/课题】查询【课题/子课题】集
                            List<object[]> list2 = SqlHelper.ExecuteColumnsQuery($"SELECT ti_id, ti_code, ti_submit_status FROM topic_info WHERE ti_obj_id='{treeNode2.Name}' AND ti_worker_id='{row["pi_worker_id"]}' ORDER BY ti_code", 3);
                            for(int j = 0; j < list2.Count; j++)
                            {
                                TreeNode treeNode3 = new TreeNode()
                                {
                                    Name = ToolHelper.GetValue(list2[j][0]),
                                    Text = ToolHelper.GetValue(list2[j][1]),
                                    Tag = ControlType.Topic,
                                    ForeColor = "1".Equals(ToolHelper.GetValue(list2[j][2])) ? DisEnbleColor : Color.Black
                                };
                                treeNode2.Nodes.Add(treeNode3);

                                List<object[]> list3 = SqlHelper.ExecuteColumnsQuery($"SELECT si_id, si_code, si_submit_status FROM subject_info WHERE si_obj_id='{treeNode3.Name}' AND si_worker_id='{row["pi_worker_id"]}' ORDER BY si_code", 3);
                                for(int k = 0; k < list3.Count; k++)
                                {
                                    TreeNode treeNode4 = new TreeNode()
                                    {
                                        Name = ToolHelper.GetValue(list3[k][0]),
                                        Text = ToolHelper.GetValue(list3[k][1]),
                                        Tag = ControlType.Subject,
                                        ForeColor = "1".Equals(ToolHelper.GetValue(list3[k][2])) ? DisEnbleColor : Color.Black
                                    };
                                    treeNode3.Nodes.Add(treeNode4);
                                }
                            }
                        }
                        else
                        {
                            //根据【项目/课题】查询【课题/子课题】集
                            DataRow _row = SqlHelper.ExecuteSingleRowQuery($"SELECT ti_id, ti_code, ti_submit_status, ti_worker_id FROM topic_info WHERE ti_id='{objId}';");
                            if(_row != null)
                            {
                                TreeNode treeNode3 = new TreeNode()
                                {
                                    Name = ToolHelper.GetValue(_row["ti_id"]),
                                    Text = ToolHelper.GetValue(_row["ti_code"]),
                                    Tag = ControlType.Topic,
                                    ForeColor = "1".Equals(ToolHelper.GetValue(_row["ti_submit_status"])) ? DisEnbleColor : Color.Black
                                };
                                treeNode.Nodes.Add(treeNode3);

                                List<object[]> list3 = SqlHelper.ExecuteColumnsQuery($"SELECT si_id, si_code, si_submit_status FROM subject_info WHERE si_obj_id='{treeNode3.Name}' AND si_worker_id='{_row["ti_worker_id"]}' ORDER BY si_code", 3);
                                for(int k = 0; k < list3.Count; k++)
                                {
                                    TreeNode treeNode4 = new TreeNode()
                                    {
                                        Name = ToolHelper.GetValue(list3[k][0]),
                                        Text = ToolHelper.GetValue(list3[k][1]),
                                        Tag = ControlType.Subject,
                                        ForeColor = "1".Equals(ToolHelper.GetValue(list3[k][2])) ? DisEnbleColor : Color.Black
                                    };
                                    treeNode3.Nodes.Add(treeNode4);
                                }
                            }
                        }
                    }
                    //重大专项
                    else
                    {
                        DataRow speRow = SqlHelper.ExecuteSingleRowQuery($"SELECT imp_id, imp_name, imp_obj_id FROM imp_dev_info WHERE imp_id='{_planId}'");
                        if(speRow != null)
                        {
                            DataRow impRow = SqlHelper.ExecuteSingleRowQuery($"SELECT imp_id, imp_name FROM imp_info WHERE imp_id='{speRow["imp_obj_id"]}'");
                            treeNode = new TreeNode()
                            {
                                Name = ToolHelper.GetValue(impRow["imp_id"]),
                                Text = ToolHelper.GetValue(impRow["imp_name"]),
                                Tag = ControlType.Imp,
                                ForeColor = DisEnbleColor
                            };
                            TreeNode speNode = new TreeNode()
                            {
                                Name = ToolHelper.GetValue(speRow["imp_id"]),
                                Text = ToolHelper.GetValue(speRow["imp_name"]),
                                Tag = ControlType.Special,
                                ForeColor = DisEnbleColor
                            };
                            treeNode.Nodes.Add(speNode);

                            //根据【计划】查询【项目/课题】集
                            DataRow row = SqlHelper.ExecuteSingleRowQuery($"SELECT pi_id, pi_code, pi_categor, pi_submit_status, pi_worker_id FROM project_info WHERE pi_id='{objId}';");
                            if(row != null)
                            {
                                TreeNode treeNode2 = new TreeNode()
                                {
                                    Name = ToolHelper.GetValue(row["pi_id"]),
                                    Text = ToolHelper.GetValue(row["pi_code"]),
                                    Tag = ControlType.Project,
                                    ForeColor = GetForeColorByState(row["pi_submit_status"])
                                };
                                speNode.Nodes.Add(treeNode2);
                                //根据【项目/课题】查询【课题/子课题】集
                                List<object[]> list2 = SqlHelper.ExecuteColumnsQuery($"SELECT ti_id, ti_code, ti_submit_status FROM topic_info WHERE ti_obj_id='{treeNode2.Name}' AND ti_worker_id='{row["pi_worker_id"]}' ORDER BY ti_code", 3);
                                for(int j = 0; j < list2.Count; j++)
                                {
                                    TreeNode treeNode3 = new TreeNode()
                                    {
                                        Name = ToolHelper.GetValue(list2[j][0]),
                                        Text = ToolHelper.GetValue(list2[j][1]),
                                        Tag = ControlType.Topic,
                                        ForeColor = GetForeColorByState(list2[j][2])
                                    };
                                    treeNode2.Nodes.Add(treeNode3);

                                    List<object[]> list3 = SqlHelper.ExecuteColumnsQuery($"SELECT si_id, si_code, si_submit_status FROM subject_info WHERE si_obj_id='{treeNode3.Name}' AND si_worker_id='{row["pi_worker_id"]}' ORDER BY si_code", 3);
                                    for(int k = 0; k < list3.Count; k++)
                                    {
                                        TreeNode treeNode4 = new TreeNode()
                                        {
                                            Name = ToolHelper.GetValue(list3[k][0]),
                                            Text = ToolHelper.GetValue(list3[k][1]),
                                            Tag = ControlType.Subject,
                                            ForeColor = GetForeColorByState(list3[k][2])
                                        };
                                        treeNode3.Nodes.Add(treeNode4);
                                    }
                                }
                            }
                            else
                            {
                                //根据【项目/课题】查询【课题/子课题】集
                                DataRow _row = SqlHelper.ExecuteSingleRowQuery($"SELECT ti_id, ti_code, ti_submit_status, ti_worker_id FROM topic_info WHERE ti_id='{objId}';");
                                if(_row != null)
                                {
                                    TreeNode treeNode3 = new TreeNode()
                                    {
                                        Name = ToolHelper.GetValue(_row["ti_id"]),
                                        Text = ToolHelper.GetValue(_row["ti_code"]),
                                        Tag = ControlType.Topic,
                                        ForeColor = GetForeColorByState(_row["ti_submit_status"])
                                    };
                                    speNode.Nodes.Add(treeNode3);

                                    List<object[]> list3 = SqlHelper.ExecuteColumnsQuery($"SELECT si_id, si_code, si_submit_status FROM subject_info WHERE si_obj_id='{treeNode3.Name}' AND si_worker_id='{_row["ti_worker_id"]}' ORDER BY si_code", 3);
                                    for(int k = 0; k < list3.Count; k++)
                                    {
                                        TreeNode treeNode4 = new TreeNode()
                                        {
                                            Name = ToolHelper.GetValue(list3[k][0]),
                                            Text = ToolHelper.GetValue(list3[k][1]),
                                            Tag = ControlType.Subject,
                                            ForeColor = GetForeColorByState(list3[k][2])
                                        };
                                        treeNode3.Nodes.Add(treeNode4);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            //子级（课题/子课题）
            else if(workType == WorkType.TopicWork)
            {
                object[] _obj = SqlHelper.ExecuteRowsQuery($"SELECT pi_id, pi_name, pi_worker_id FROM project_info WHERE pi_id='{planId}'");
                if(_obj == null)
                    _obj = SqlHelper.ExecuteRowsQuery($"SELECT dd_id, dd_name,'{UserHelper.GetUser().UserKey}' FROM data_dictionary WHERE dd_id='{planId}'");
                treeNode = new TreeNode()
                {
                    Name = ToolHelper.GetValue(_obj[0]),
                    Text = ToolHelper.GetValue(_obj[1]),
                    Tag = ControlType.Plan
                };
                //如果当前任务并非登录人加工，则无法编辑【文字置灰】
                if(!UserHelper.GetUser().UserKey.Equals(_obj[2]))
                    treeNode.ForeColor = DisEnbleColor;
                //根据【计划】查询【项目/课题】集
                List<object[]> list = SqlHelper.ExecuteColumnsQuery($"SELECT pi_id, pi_code, pi_categor, pi_worker_id FROM project_info WHERE pi_obj_id='{treeNode.Name}'", 4);
                for(int i = 0; i < list.Count; i++)
                {
                    TreeNode treeNode2 = new TreeNode()
                    {
                        Name = ToolHelper.GetValue(list[i][0]),
                        Text = ToolHelper.GetValue(list[i][1]),
                        Tag = (ControlType)list[i][2]
                    };
                    //如果当前任务并非登录人加工，则无法编辑【文字置灰】
                    if(!UserHelper.GetUser().UserKey.Equals(list[i][3]))
                        treeNode2.ForeColor = DisEnbleColor;
                    //根据【项目/课题】查询【课题/子课题】集
                    List<object[]> list2 = SqlHelper.ExecuteColumnsQuery($"SELECT si_id, si_code, si_categor, si_worker_id FROM subject_info WHERE pi_id='{treeNode2.Name}' AND si_worker_id='{UserHelper.GetUser().UserKey}'", 4);
                    for(int j = 0; j < list2.Count; j++)
                    {
                        TreeNode treeNode3 = new TreeNode()
                        {
                            Name = ToolHelper.GetValue(list2[j][0]),
                            Text = ToolHelper.GetValue(list2[j][1]),
                            Tag = (ControlType)list2[j][2]
                        };
                        //如果当前任务并非登录人加工，则无法编辑【文字置灰】
                        if(!UserHelper.GetUser().UserKey.Equals(list2[j][3]))
                            treeNode3.ForeColor = DisEnbleColor;
                        //【当前定位为课题子课题，则只取一条即可】
                        if(treeNode3.Name.Equals(objId))
                        {
                            treeNode.Nodes.Add(treeNode2);
                            treeNode2.Nodes.Add(treeNode3);
                            List<object[]> list3 = SqlHelper.ExecuteColumnsQuery($"SELECT si_id,si_code,si_categor FROM subject_info WHERE pi_id='{treeNode3.Name}' AND si_worker_id='{UserHelper.GetUser().UserKey}'", 3);
                            for(int k = 0; k < list3.Count; k++)
                            {
                                TreeNode treeNode4 = new TreeNode()
                                {
                                    Name = list3[k][0].ToString(),
                                    Text = list3[k][1].ToString(),
                                    Tag = (ControlType)list3[k][2]
                                };
                                treeNode3.Nodes.Add(treeNode4);
                            }
                            break;
                        }
                    }
                }
            }
            //纸本加工-普通计划（Plan）
            else if(workType == WorkType.PaperWork_Plan)
            {
                DataRow planRow = SqlHelper.ExecuteSingleRowQuery($"SELECT pi_id, pi_name, pi_worker_id, pi_submit_status FROM project_info WHERE pi_id='{planId}'");
                if(planRow != null)
                {
                    treeNode = new TreeNode()
                    {
                        Name = ToolHelper.GetValue(planRow["pi_id"]),
                        Text = ToolHelper.GetValue(planRow["pi_name"]),
                        Tag = ControlType.Plan,
                        ForeColor = GetForeColorByState(planRow["pi_submit_status"]),
                    };
                }
            }
            //纸本加工-重点研发（Imp）
            else if(workType == WorkType.PaperWork_Imp)
            {
                DataRow impRow = SqlHelper.ExecuteSingleRowQuery($"SELECT imp_id, imp_name, imp_submit_status FROM imp_info WHERE imp_id='{planId}'");
                if(impRow != null)
                {
                    treeNode = new TreeNode()
                    {
                        Name = ToolHelper.GetValue(impRow["imp_id"]),
                        Text = ToolHelper.GetValue(impRow["imp_name"]),
                        Tag = ControlType.Imp,
                        ForeColor = GetForeColorByState(impRow["imp_submit_status"]),
                    };
                }
            }
            //纸本加工-专项（Special）
            else if(workType == WorkType.PaperWork_Special)
            {
                DataRow speRow = SqlHelper.ExecuteSingleRowQuery($"SELECT imp_id, imp_name, imp_obj_id, imp_submit_status FROM imp_dev_info WHERE imp_id='{planId}'");
                if(speRow != null)
                {
                    DataRow impRow = SqlHelper.ExecuteSingleRowQuery($"SELECT imp_id, imp_name FROM imp_info WHERE imp_id='{speRow["imp_obj_id"]}'");
                    treeNode = new TreeNode()
                    {
                        Name = ToolHelper.GetValue(impRow["imp_id"]),
                        Text = ToolHelper.GetValue(impRow["imp_name"]),
                        Tag = ControlType.Imp,
                        ForeColor = DisEnbleColor
                    };
                    treeNode.Nodes.Add(new TreeNode()
                    {
                        Name = ToolHelper.GetValue(speRow["imp_id"]),
                        Text = ToolHelper.GetValue(speRow["imp_name"]),
                        Tag = ControlType.Special,
                        ForeColor = GetForeColorByState(speRow["imp_submit_status"]),
                    });
                }
            }
            else if(workType == WorkType.CDWork_Imp)
            {
                DataRow impRow = SqlHelper.ExecuteSingleRowQuery($"SELECT imp_id, imp_name, imp_submit_status FROM imp_info WHERE imp_id='{planId}'");
                if(impRow != null)
                {
                    treeNode = new TreeNode()
                    {
                        Name = ToolHelper.GetValue(impRow["imp_id"]),
                        Text = ToolHelper.GetValue(impRow["imp_name"]),
                        Tag = ControlType.Imp,
                        ForeColor = GetForeColorByState(impRow["imp_submit_status"]),
                    };
                }
            }
            //光盘 - 专项
            else if(workType == WorkType.CDWork_Special)
            {
                DataRow speRow = SqlHelper.ExecuteSingleRowQuery($"SELECT imp_id, imp_name, imp_obj_id FROM imp_dev_info WHERE imp_id='{planId}'");
                if(speRow != null)
                {
                    DataRow impRow = SqlHelper.ExecuteSingleRowQuery($"SELECT imp_id, imp_name, imp_obj_id FROM imp_info WHERE imp_id='{speRow["imp_obj_id"]}'");
                    treeNode = new TreeNode()
                    {
                        Name = ToolHelper.GetValue(impRow["imp_id"]),
                        Text = ToolHelper.GetValue(impRow["imp_name"]),
                        Tag = ControlType.Imp,
                        ForeColor = DisEnbleColor
                    };
                    treeNode.Nodes.Add(new TreeNode()
                    {
                        Name = ToolHelper.GetValue(speRow["imp_id"]),
                        Text = ToolHelper.GetValue(speRow["imp_name"]),
                        Tag = ControlType.Special
                    });
                }
            }

            treeView.EndUpdate();
            if(treeNode != null)
            {
                treeView.Nodes.Add(treeNode);
                //默认加载计划页面
                if(treeView.Nodes.Count > 0 && tab_MenuList.TabPages.Count == 0)
                {
                    TreeNode node = treeView.Nodes[0];
                    ControlType _type = (ControlType)node.Tag;
                    if(_type == ControlType.Plan)
                    {
                        ShowTab("plan", 0);
                        LoadPlanPage(node);
                    }
                    else if(_type == ControlType.Imp)
                    {
                        ShowTab("imp", 0);
                        LoadImpPage(node);
                    }
                }
                treeView.ExpandAll();
                
            }
        }
        
        /// <summary>
        /// 目录树点击事件
        /// </summary>
        private void TreeView_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if(e.Button == MouseButtons.Left)
            {
                ControlType type = (ControlType)e.Node.Tag;
                if(type == ControlType.Plan)
                {
                    ShowTab("plan", 0);
                    LoadPlanPage(e.Node);
                }
                else if(type == ControlType.Project)
                {
                    tab_MenuList.TabPages.Clear();
                    if(workType == WorkType.PaperWork)
                    {
                        int count = SqlHelper.ExecuteCountQuery($"SELECT COUNT(pi_id) FROM project_info WHERE pi_id='{e.Node.Parent.Name}'");
                        if(count == 0)
                        {
                            ShowTab("imp", 0);
                            LoadImpPage(e.Node.Parent.Parent);

                            ShowTab("Special", 1);
                            LoadPageBasicInfo(e.Node.Parent, ControlType.Special);

                            ShowTab("project", 2);
                            LoadPageBasicInfo(e.Node, type);
                        }
                        else
                        {
                            ShowTab("plan", 0);
                            LoadPlanPage(e.Node.Parent);

                            ShowTab("project", 1);
                            LoadPageBasicInfo(e.Node, ControlType.Project);
                        }
                    }
                    else if(workType == WorkType.CDWork || workType == WorkType.PaperWork)
                    {
                        ShowTab("plan", 0);
                        LoadPlanPage(e.Node.Parent);

                        ShowTab("project", 1);
                        LoadPageBasicInfo(e.Node, type);
                    }
                    else if(workType == WorkType.ProjectWork)
                    {
                        int index = SqlHelper.ExecuteCountQuery($"SELECT COUNT(imp_id) FROM imp_dev_info WHERE imp_id=(SELECT pi_obj_id FROM project_info WHERE pi_id='{e.Node.Name}')");
                        if(index > 0)
                        {
                            ShowTab("imp", 0);
                            LoadImpPage(e.Node.Parent.Parent);

                            ShowTab("special", 1);
                            LoadPageBasicInfo(e.Node.Parent, ControlType.Special);

                            ShowTab("project", 2);
                            LoadPageBasicInfo(e.Node, ControlType.Project);
                        }
                        else
                        {
                            ShowTab("plan", 0);
                            LoadPlanPage(e.Node.Parent);

                            ShowTab("project", 1);
                            LoadPageBasicInfo(e.Node, ControlType.Project);
                        }
                    }
                    else if(workType == WorkType.TopicWork)
                    {

                        ShowTab("plan", 0);
                        LoadPlanPage(e.Node.Parent);

                        ShowTab("project", 1);
                        LoadPageBasicInfo(e.Node, type);
                    }
                }
                else if(type == ControlType.Topic)
                {
                    tab_MenuList.TabPages.Clear();
                    if(workType == WorkType.PaperWork)
                    {
                        int count = SqlHelper.ExecuteCountQuery($"SELECT COUNT(pi_id) FROM project_info WHERE pi_id='{e.Node.Parent.Parent.Name}'");
                        if(count == 0)
                        {
                            ShowTab("imp", 0);
                            LoadImpPage(e.Node.Parent.Parent.Parent);

                            ShowTab("Special", 1);
                            LoadPageBasicInfo(e.Node.Parent.Parent, ControlType.Special);

                            ShowTab("project", 2);
                            LoadPageBasicInfo(e.Node.Parent, ControlType.Plan);

                            ShowTab("Topic", 3);
                            LoadPageBasicInfo(e.Node, ControlType.Topic);
                        }
                        else
                        {
                            ShowTab("plan", 0);
                            LoadPlanPage(e.Node.Parent.Parent);

                            ShowTab("project", 1);
                            LoadPageBasicInfo(e.Node.Parent, ControlType.Plan);

                            ShowTab("Topic", 2);
                            LoadPageBasicInfo(e.Node, type);
                        }
                    }
                    else if(workType == WorkType.CDWork || workType == WorkType.PaperWork)
                    {
                        ShowTab("plan", 0);
                        LoadPlanPage(e.Node.Parent.Parent);

                        ShowTab("project", 1);
                        LoadPageBasicInfo(e.Node.Parent, ControlType.Plan);

                        ShowTab("Topic", 2);
                        LoadPageBasicInfo(e.Node, ControlType.Topic);
                    }
                    else if(workType == WorkType.ProjectWork)
                    {
                        object _tempParam = SqlHelper.ExecuteOnlyOneQuery($"SELECT pi_obj_id FROM project_info WHERE pi_id=(SELECT ti_obj_id FROM topic_info WHERE ti_id='{e.Node.Name}') AND pi_categor=2");
                        if(_tempParam != null)
                        {
                            int speParam = SqlHelper.ExecuteCountQuery($"SELECT COUNT(imp_id) FROM imp_dev_info WHERE imp_id='{_tempParam}'");
                            if(speParam > 0)
                            {
                                ShowTab("imp", 0);
                                LoadImpPage(e.Node.Parent.Parent.Parent);

                                ShowTab("special", 1);
                                LoadPageBasicInfo(e.Node.Parent.Parent, ControlType.Special);

                                ShowTab("project", 2);
                                LoadPageBasicInfo(e.Node.Parent, ControlType.Project);

                                ShowTab("topic", 3);
                                LoadPageBasicInfo(e.Node, ControlType.Topic);
                            }
                            else
                            {
                                ShowTab("plan", 0);
                                LoadPlanPage(e.Node.Parent.Parent);

                                ShowTab("project", 1);
                                LoadPageBasicInfo(e.Node.Parent, ControlType.Project);

                                ShowTab("topic", 2);
                                LoadPageBasicInfo(e.Node, ControlType.Topic);
                            }
                        }
                        else
                        {
                            int speParam = SqlHelper.ExecuteCountQuery($"SELECT COUNT(imp_id) FROM imp_dev_info WHERE imp_id=(SELECT ti_obj_id FROM topic_info WHERE ti_id='{e.Node.Name}')");
                            if(speParam > 0)
                            {
                                ShowTab("imp", 0);
                                LoadImpPage(e.Node.Parent.Parent);

                                ShowTab("special", 1);
                                LoadPageBasicInfo(e.Node.Parent, ControlType.Special);

                                ShowTab("topic", 2);
                                LoadPageBasicInfo(e.Node, ControlType.Topic);
                            }
                            else
                            {

                                ShowTab("plan", 0);
                                LoadPlanPage(e.Node.Parent);

                                ShowTab("topic", 1);
                                LoadPageBasicInfo(e.Node, ControlType.Topic);
                            }
                        }
                    }
                }
                else if(type == ControlType.Subject)
                {
                    if(workType == WorkType.PaperWork)
                    {
                        int count = SqlHelper.ExecuteCountQuery($"SELECT COUNT(pi_id) FROM project_info WHERE pi_id='{e.Node.Parent.Parent.Parent.Name}'");
                        if(count == 0)
                        {
                            ShowTab("imp", 0);
                            LoadImpPage(e.Node.Parent.Parent.Parent.Parent);

                            ShowTab("special", 1);
                            LoadPageBasicInfo(e.Node.Parent.Parent.Parent, ControlType.Special);

                            ShowTab("project", 2);
                            LoadPageBasicInfo(e.Node.Parent.Parent, ControlType.Plan);

                            ShowTab("topic", 3);
                            LoadPageBasicInfo(e.Node.Parent, ControlType.Topic);

                            ShowTab("subject", 4);
                            LoadPageBasicInfo(e.Node, ControlType.Subject);
                        }
                        else
                        {
                            ShowTab("plan", 0);
                            LoadPlanPage(e.Node.Parent.Parent.Parent);

                            ShowTab("project", 1);
                            LoadPageBasicInfo(e.Node.Parent.Parent, ControlType.Plan);

                            ShowTab("topic", 2);
                            LoadPageBasicInfo(e.Node.Parent, ControlType.Topic);

                            ShowTab("subject", 3);
                            LoadPageBasicInfo(e.Node, ControlType.Subject);
                        }
                    }
                    else if(workType == WorkType.ProjectWork)
                    {
                        tab_MenuList.TabPages.Clear();
                        object topId = SqlHelper.ExecuteOnlyOneQuery($"SELECT ti_obj_id FROM topic_info WHERE ti_id=(SELECT si_obj_id FROM subject_info WHERE si_id='{e.Node.Name}')");
                        object _tempParam = SqlHelper.ExecuteOnlyOneQuery($"SELECT pi_obj_id FROM project_info WHERE pi_id='{topId}' AND pi_categor=2");
                        if(_tempParam != null)
                        {
                            int speParam = SqlHelper.ExecuteCountQuery($"SELECT COUNT(imp_id) FROM imp_dev_info WHERE imp_id='{_tempParam}'");
                            if(speParam > 0)
                            {
                                ShowTab("imp", 0);
                                LoadImpPage(e.Node.Parent.Parent.Parent.Parent);

                                ShowTab("special", 1);
                                LoadPageBasicInfo(e.Node.Parent.Parent.Parent, ControlType.Special);

                                ShowTab("project", 2);
                                LoadPageBasicInfo(e.Node.Parent.Parent, ControlType.Project);

                                ShowTab("topic", 3);
                                LoadPageBasicInfo(e.Node.Parent, ControlType.Topic);

                                ShowTab("subject", 4);
                                LoadPageBasicInfo(e.Node, ControlType.Subject);
                            }
                            else
                            {
                                ShowTab("plan", 0);
                                LoadPlanPage(e.Node.Parent.Parent.Parent);

                                ShowTab("project", 1);
                                LoadPageBasicInfo(e.Node.Parent.Parent, ControlType.Project);

                                ShowTab("topic", 2);
                                LoadPageBasicInfo(e.Node.Parent, ControlType.Topic);

                                ShowTab("subject", 3);
                                LoadPageBasicInfo(e.Node, ControlType.Subject);
                            }
                        }
                        else
                        {
                            int speParam = SqlHelper.ExecuteCountQuery($"SELECT COUNT(imp_id) FROM imp_dev_info WHERE imp_id='{topId}'");
                            if(speParam > 0)
                            {
                                ShowTab("imp", 0);
                                LoadImpPage(e.Node.Parent.Parent.Parent);

                                ShowTab("special", 1);
                                LoadPageBasicInfo(e.Node.Parent.Parent, ControlType.Special);

                                ShowTab("topic", 2);
                                LoadPageBasicInfo(e.Node.Parent, ControlType.Topic);

                                ShowTab("subject", 3);
                                LoadPageBasicInfo(e.Node, ControlType.Subject);
                            }
                            else
                            {
                                ShowTab("plan", 0);
                                LoadPlanPage(e.Node.Parent.Parent);

                                ShowTab("topic", 1);
                                LoadPageBasicInfo(e.Node.Parent, ControlType.Topic);

                                ShowTab("subject", 2);
                                LoadPageBasicInfo(e.Node, ControlType.Subject);
                            }
                        }
                    }
                }
                else if(type == ControlType.Imp)
                {
                    tab_MenuList.TabPages.Clear();

                    ShowTab("imp", 0);
                    LoadImpPage(e.Node);

                }
                else if(type == ControlType.Special)
                {
                    tab_MenuList.TabPages.Clear();

                    ShowTab("imp", 0);
                    LoadImpPage(e.Node.Parent);

                    ShowTab("special", 1);
                    LoadPageBasicInfo(e.Node, type);
                }
            }
        }
      
        /// <summary>
        /// 加载Imp/Dev基本信息
        /// </summary>
        private void LoadImpPage(TreeNode node)
        {
            DataRow row = SqlHelper.ExecuteSingleRowQuery($"SELECT imp_id, imp_name, imp_intro, imp_submit_status FROM imp_info WHERE imp_id='{node.Name}'");
            if(row != null)
            {
                tab_Imp_Info.Tag = ToolHelper.GetValue(row["imp_id"]);
                LoadFileList(dgv_Imp_FileList, "imp_fl_", ToolHelper.GetValue(row["imp_id"]));
                lbl_Imp_Name.Text = ToolHelper.GetValue(row["imp_name"]);
                txt_Imp_Intro.Text = ToolHelper.GetValue(row["imp_intro"]);
                EnableControls(ControlType.Imp, Convert.ToInt32(row["imp_submit_status"]) != 1);
            }

            //加载下拉列表
            if(cbo_Imp_HasNext.DataSource == null)
            {
                object key = objId; 
                if(DEV_TYPE == -1)
                    key = SqlHelper.ExecuteOnlyOneQuery($"SELECT dd_code FROM data_dictionary WHERE dd_id='{key}'");
                if(DEV_TYPE == 0)//重点研发
                    key = "dic_key_project";
                else if(DEV_TYPE == 1 || "dic_imp_dev".Equals(key))
                {
                    key = "dic_key_project";
                    tab_MenuList.TabPages["imp"].Text = "国家重点研发计划";
                }
                DataTable table = SqlHelper.ExecuteQuery($"SELECT dd_id, dd_name FROM data_dictionary WHERE dd_pId=(SELECT dd_id FROM data_dictionary WHERE dd_code='{key}') ORDER BY dd_sort");
                cbo_Imp_HasNext.DataSource = table;
                cbo_Imp_HasNext.DisplayMember = "dd_name";
                cbo_Imp_HasNext.ValueMember = "dd_id";
            }
            special.Tag = node.Name;
            if(node.ForeColor == DisEnbleColor)
                pal_Imp_BtnGroup.Enabled = false;
        }

        private int GetAdvincesAmount(object objId)
        {
            return SqlHelper.ExecuteCountQuery($"SELECT COUNT(qa_id) FROM quality_advices a " +
                $"WHERE qa_time = (SELECT MAX(qa_time) FROM quality_advices WHERE qa_obj_id = a.qa_obj_id AND qa_type = a.qa_type) " +
                $"AND qa_obj_id='{objId}'");
        }
   
        /// <summary>
        /// 获取当前计划下案卷总数
        /// </summary>
        /// <param name="planCode">计划编号</param>
        private object GetAJAmount(object planCode)
        {
            int amount = Convert.ToInt32(SqlHelper.ExecuteOnlyOneQuery($"SELECT COUNT(pt_id) FROM processing_tag WHERE pt_code LIKE '{planCode}%'"));
            return (amount + 1).ToString().PadLeft(6, '0');
        }
    
        /// <summary>
        /// 获取最高密级
        /// </summary>
        private string GetMaxSecretById(object objid) => ToolHelper.GetValue(SqlHelper.ExecuteOnlyOneQuery($"SELECT TOP(1) dd_name FROM processing_file_list LEFT JOIN data_dictionary ON pfl_scert = dd_id WHERE pfl_obj_id = '{objid}' ORDER BY dd_sort DESC"));
  
        /// <summary>
        /// 加载文件缺失校验列表
        /// </summary>
        /// <param name="dataGridView">待校验表格</param>
        /// <param name="objid">主键</param>
        private void LoadFileValidList(DataGridView dataGridView, object objid, string key)
        {
            dataGridView.Rows.Clear();

            string querySql = "SELECT dd_name [name], dd_name+' '+extend_3 dd_name, dd_note, extend_2 FROM data_dictionary dd WHERE dd_pId in(" +
               "SELECT dd_id FROM data_dictionary WHERE dd_pId = (SELECT dd_id FROM data_dictionary  WHERE dd_code = 'dic_file_jd')) " +
               $"AND dd.dd_name NOT IN(SELECT dd.dd_name FROM processing_file_list fi LEFT JOIN data_dictionary dd ON fi.pfl_categor = dd.dd_id where fi.pfl_obj_id='{objid}')" +
               $" ORDER BY dd_name";
            DataTable table = SqlHelper.ExecuteQuery(querySql);
            for (int i = 0; i < table.Rows.Count; i++)
            {
                string typeName = ToolHelper.GetValue(table.Rows[i]["name"]).Trim();
                if(!"其他".Equals(typeName))
                {
                    int indexRow = dataGridView.Rows.Add();
                    dataGridView.Rows[indexRow].Cells[key + "id"].Value = i + 1;
                    dataGridView.Rows[indexRow].Cells[key + "categor"].Value = ToolHelper.GetValue(table.Rows[i]["dd_name"]);
                    dataGridView.Rows[indexRow].Cells[key + "name"].Value = ToolHelper.GetValue(table.Rows[i]["dd_note"]);

                    string queryReasonSql = $"SELECT pfo_id, pfo_reason, pfo_remark FROM processing_file_lost WHERE pfo_obj_id='{objid}' AND pfo_categor LIKE '{typeName}%'";
                    object[] _obj = SqlHelper.ExecuteRowsQuery(queryReasonSql);
                    if(_obj != null)
                    {
                        dataGridView.Rows[indexRow].Cells[key + "id"].Tag = ToolHelper.GetValue(_obj[0]);
                        dataGridView.Rows[indexRow].Cells[key + "reason"].Value = ToolHelper.GetValue(_obj[1]);
                        dataGridView.Rows[indexRow].Cells[key + "remark"].Value = ToolHelper.GetValue(_obj[2]);
                    }
                    if(!key.Contains("special") && !key.Contains("plan"))
                    {
                        string musted = ToolHelper.GetValue(table.Rows[i]["extend_2"]);
                        if(!string.IsNullOrEmpty(musted))
                        {
                            dataGridView.Rows[indexRow].Tag = musted;
                            dataGridView.Rows[indexRow].Cells[key + "name"].Style.ForeColor = Color.Red;
                        }
                    }
                }
            }
        }
    
        /// <summary>
        /// 加载计划-案卷盒归档表
        /// </summary>
        /// <param name="pbId">案卷盒ID</param>
        /// <param name="objId">所属对象ID</param>
        /// <param name="type">对象类型</param>
        private void LoadFileBoxTable(object pbId, object objId, ControlType type)
        {
            string GCID = ToolHelper.GetValue(SqlHelper.ExecuteOnlyOneQuery($"SELECT pb_gc_id FROM processing_box WHERE pb_id='{pbId}'"));
            if(type == ControlType.Plan)
            {
                txt_Plan_Box_GCID.Text = GCID;
                LoadFileBoxTableInstance(lsv_JH_File1,lsv_JH_File2, "jh", pbId, objId);
            }
            else if(type == ControlType.Project)
            {
                txt_Project_Box_GCID.Text = GCID;
                LoadFileBoxTableInstance(lsv_JH_XM_File1, lsv_JH_XM_File2, "jh_xm", pbId, objId);
            }
            else if(type == ControlType.Topic)
            {
                txt_Topic_Box_GCID.Text = GCID;
                LoadFileBoxTableInstance(lsv_JH_KT_File1, lsv_JH_KT_File2, "jh_kt", pbId, objId);
            }
            else if(type == ControlType.Subject)
            {
                txt_Subject_Box_GCID.Text = GCID;
                LoadFileBoxTableInstance(lsv_JH_XM_KT_ZKT_File1, lsv_JH_XM_KT_ZKT_File2, "jh_xm_kt_zkt", pbId, objId);
            }
            else if(type == ControlType.Imp)
            {
                txt_Imp_Box_GCID.Text = GCID;
                LoadFileBoxTableInstance(lsv_Imp_File1, lsv_Imp_File2, "imp", pbId, objId);
            }
            else if(type == ControlType.Special)
            {
                txt_Special_Box_GCID.Text = GCID;
                LoadFileBoxTableInstance(lsv_Imp_Dev_File1, lsv_Imp_Dev_File2, "Special", pbId, objId);
            }

        }
    
        /// <summary>
        /// 加载案卷盒归档表
        /// </summary>
        /// <param name="leftView">待归档列表</param>
        /// <param name="rightView">已归档列表</param>
        /// <param name="key">关键字</param>
        /// <param name="pbId">盒ID</param>
        /// <param name="objId">所属对象ID</param>
        private void LoadFileBoxTableInstance(ListView leftView, ListView rightView, string key, object pbId, object objId)
        {
            leftView.Items.Clear();
            leftView.Columns.Clear();
            rightView.Items.Clear();
            rightView.Columns.Clear();

            leftView.Columns.AddRange(new ColumnHeader[]
            {
                new ColumnHeader{ Name = $"{key}_file1_id", Text = "主键", Width = 0},
                new ColumnHeader{ Name = $"{key}_file1_type", Text = "文件编号", TextAlign = HorizontalAlignment.Center ,Width = 85},
                new ColumnHeader{ Name = $"{key}_file1_name", Text = "文件名称", Width = 250},
                new ColumnHeader{ Name = $"{key}_file1_date", Text = "形成日期", Width = 100}
            });
            rightView.Columns.AddRange(new ColumnHeader[]
            {
                new ColumnHeader{ Name = $"{key}_file2_id", Text = "主键", Width = 0},
                new ColumnHeader{ Name = $"{key}_file2_number", Text = "序号", Width = 50},
                new ColumnHeader{ Name = $"{key}_file2_type", Text = "文件编号", TextAlign = HorizontalAlignment.Center ,Width = 85},
                new ColumnHeader{ Name = $"{key}_file2_name", Text = "文件名称", Width = 250},
                new ColumnHeader{ Name = $"{key}_file2_date", Text = "形成日期", Width = 100}
            });
            //未归档
            string querySql = $"SELECT pfl_id, pfl_code, pfl_name, pfl_date FROM processing_file_list " +
                $"WHERE pfl_obj_id = '{objId}' AND (pfl_box_id IS NULL OR pfl_box_id='') AND pfl_amount > 0 ORDER BY pfl_code, pfl_date";
            DataTable dataTable = SqlHelper.ExecuteQuery(querySql);
            for(int i = 0; i < dataTable.Rows.Count; i++)
            {
                ListViewItem item = leftView.Items.Add(ToolHelper.GetValue(dataTable.Rows[i]["pfl_id"]));
                item.SubItems.AddRange(new ListViewItem.ListViewSubItem[]
                {
                    new ListViewItem.ListViewSubItem(){ Text = ToolHelper.GetValue(dataTable.Rows[i]["pfl_code"]) },
                    new ListViewItem.ListViewSubItem(){ Text = ToolHelper.GetValue(dataTable.Rows[i]["pfl_name"]) },
                    new ListViewItem.ListViewSubItem(){ Text = GetDateValue(dataTable.Rows[i]["pfl_date"], "yyyy-MM-dd") },
                });
            }
            //已归档[已存在盒]
            if(!string.IsNullOrEmpty(ToolHelper.GetValue(pbId)))
            {
                querySql = $"SELECT pfl_id, pfl_code, pfl_name, pfl_date FROM processing_file_list " +
                    $"WHERE pfl_box_id ='{pbId}' ORDER BY pfl_box_sort";
                DataTable table = SqlHelper.ExecuteQuery(querySql);
                int j = 0;
                foreach(DataRow row in table.Rows)
                {
                    ListViewItem item = rightView.Items.Add(ToolHelper.GetValue(row["pfl_id"]));
                    item.SubItems.AddRange(new ListViewItem.ListViewSubItem[]
                    {
                        new ListViewItem.ListViewSubItem(){ Text = ToolHelper.GetValue(++j).PadLeft(2, '0') },
                        new ListViewItem.ListViewSubItem(){ Text = ToolHelper.GetValue(row["pfl_code"]) },
                        new ListViewItem.ListViewSubItem(){ Text = ToolHelper.GetValue(row["pfl_name"]) },
                        new ListViewItem.ListViewSubItem(){ Text = GetDateValue(row["pfl_date"], "yyyy-MM-dd") },
                    });
                }
            }
        }
    
        /// <summary>
        /// 将指定文本转换成指定日期格式
        /// </summary>
        /// <param name="date">待转换的日期对象</param>
        /// <param name="format">转换格式</param>
        private string GetDateValue(object date, string format)
        {
            string _formatDate = null, value = ToolHelper.GetValue(date);
            if(!string.IsNullOrEmpty(value))
            {
                if(DateTime.TryParse(value, out DateTime result))
                {
                    if(result != DefaultValue.DefaultMinDate)
                        _formatDate = result.ToString(format);
                }
            }
            return _formatDate;
        }
    
        /// <summary>
        /// 案卷归档事件
        /// </summary>
        private void Btn_Box_Click(object sender, EventArgs e)
        {
            string name = (sender as KyoButton).Name;
            //计划
            if(name.Contains("Plan"))
            {
                object value = cbo_Plan_Box.SelectedValue;
                if(value != null)
                {
                    if("btn_Plan_Box_Right".Equals(name))
                    {
                        if(lsv_JH_File1.SelectedItems.Count > 0)
                        {
                            int count = lsv_JH_File1.SelectedItems.Count;
                            if(count > 0)
                            {
                                object[] _obj = new object[count];
                                for(int i = 0; i < count; i++)
                                    _obj[i] = lsv_JH_File1.SelectedItems[i].SubItems[0].Text;
                                SetFileState(_obj, value, true);
                            }
                        }
                    }
                    else if("btn_Plan_Box_RightAll".Equals(name))
                    {
                        int count = lsv_JH_File1.Items.Count;
                        if(count > 0)
                        {
                            object[] _obj = new object[count];
                            for(int i = 0; i < count; i++)
                                _obj[i] = lsv_JH_File1.Items[i].SubItems[0].Text;
                            SetFileState(_obj, value, true);
                        }
                    }
                    else if("btn_Plan_Box_Left".Equals(name))
                    {
                        int count = lsv_JH_File2.SelectedItems.Count;
                        if(count > 0)
                        {
                            object[] _obj = new object[count];
                            for(int i = 0; i < count; i++)
                                _obj[i] = lsv_JH_File2.SelectedItems[i].SubItems[0].Text;
                            SetFileState(_obj, value, false);
                        }
                    }
                    else if("btn_Plan_Box_LeftAll".Equals(name))
                    {
                        int count = lsv_JH_File2.Items.Count;
                        if(count > 0)
                        {
                            object[] _obj = new object[count];
                            for(int i = 0; i < count; i++)
                                _obj[i] = lsv_JH_File2.Items[i].SubItems[0].Text;
                            SetFileState(_obj, value, false);
                        }
                    }
                    LoadFileBoxTable(value, tab_Plan_Info.Tag, ControlType.Plan);
                }
                else
                    XtraMessageBox.Show("请先添加案卷盒。", "保存失败");
            }
            //计划-项目
            else if(name.Contains("btn_Project_Box"))
            {
                object value = cbo_Project_Box.SelectedValue;
                if(value != null)
                {
                    if("btn_Project_Box_Right".Equals(name))
                    {
                        if(lsv_JH_XM_File1.SelectedItems.Count > 0)
                        {
                            int count = lsv_JH_XM_File1.SelectedItems.Count;
                            if(count > 0)
                            {
                                object[] _obj = new object[count];
                                for(int i = 0; i < count; i++)
                                    _obj[i] = lsv_JH_XM_File1.SelectedItems[i].SubItems[0].Text;
                                SetFileState(_obj, value, true);
                            }
                        }
                    }
                    else if("btn_Project_Box_RightAll".Equals(name))
                    {
                        int count = lsv_JH_XM_File1.Items.Count;
                        if(count > 0)
                        {
                            object[] _obj = new object[count];
                            for(int i = 0; i < count; i++)
                                _obj[i] = lsv_JH_XM_File1.Items[i].SubItems[0].Text;
                            SetFileState(_obj, value, true);
                        }
                    }
                    else if("btn_Project_Box_Left".Equals(name))
                    {
                        int count = lsv_JH_XM_File2.SelectedItems.Count;
                        if(count > 0)
                        {
                            object[] _obj = new object[count];
                            for(int i = 0; i < count; i++)
                                _obj[i] = lsv_JH_XM_File2.SelectedItems[i].SubItems[0].Text;
                            SetFileState(_obj, value, false);
                        }
                    }
                    else if("btn_Project_Box_LeftAll".Equals(name))
                    {
                        int count = lsv_JH_XM_File2.Items.Count;
                        if(count > 0)
                        {
                            object[] _obj = new object[count];
                            for(int i = 0; i < count; i++)
                                _obj[i] = lsv_JH_XM_File2.Items[i].SubItems[0].Text;
                            SetFileState(_obj, value, false);
                        }
                    }
                    LoadFileBoxTable(value, tab_Project_Info.Tag, ControlType.Project);
                }
                else
                    XtraMessageBox.Show("请先添加案卷盒。", "保存失败");
            }
            //计划-课题
            else if(name.Contains("btn_Topic_Box"))
            {
                object value = cbo_Topic_Box.SelectedValue;
                if(value != null)
                {
                    if("btn_Topic_Box_Right".Equals(name))
                    {
                        if(lsv_JH_KT_File1.SelectedItems.Count > 0)
                        {
                            int count = lsv_JH_KT_File1.SelectedItems.Count;
                            if(count > 0)
                            {
                                object[] _obj = new object[count];
                                for(int i = 0; i < count; i++)
                                    _obj[i] = lsv_JH_KT_File1.SelectedItems[i].SubItems[0].Text;
                                SetFileState(_obj, value, true);
                            }
                        }
                    }
                    else if("btn_Topic_Box_RightAll".Equals(name))
                    {
                        int count = lsv_JH_KT_File1.Items.Count;
                        if(count > 0)
                        {
                            object[] _obj = new object[count];
                            for(int i = 0; i < count; i++)
                                _obj[i] = lsv_JH_KT_File1.Items[i].SubItems[0].Text;
                            SetFileState(_obj, value, true);
                        }
                    }
                    else if("btn_Topic_Box_Left".Equals(name))
                    {
                        int count = lsv_JH_KT_File2.SelectedItems.Count;
                        if(count > 0)
                        {
                            object[] _obj = new object[count];
                            for(int i = 0; i < count; i++)
                                _obj[i] = lsv_JH_KT_File2.SelectedItems[i].SubItems[0].Text;
                            SetFileState(_obj, value, false);
                        }
                    }
                    else if("btn_Topic_Box_LeftAll".Equals(name))
                    {
                        int count = lsv_JH_KT_File2.Items.Count;
                        if(count > 0)
                        {
                            object[] _obj = new object[count];
                            for(int i = 0; i < count; i++)
                                _obj[i] = lsv_JH_KT_File2.Items[i].SubItems[0].Text;
                            SetFileState(_obj, value, false);
                        }
                    }
                    LoadFileBoxTable(value, tab_Topic_Info.Tag, ControlType.Topic);
                }
                else
                    XtraMessageBox.Show("请先添加案卷盒。", "保存失败");
            }
            //计划-项目-课题-子课题
            else if(name.Contains("btn_Subject_Box"))
            {
                object value = cbo_Subject_Box.SelectedValue;
                if(value != null)
                {
                    if("btn_Subject_Box_Right".Equals(name))
                    {
                        if(lsv_JH_XM_KT_ZKT_File1.SelectedItems.Count > 0)
                        {
                            int count = lsv_JH_XM_KT_ZKT_File1.SelectedItems.Count;
                            if(count > 0)
                            {
                                object[] _obj = new object[count];
                                for(int i = 0; i < count; i++)
                                    _obj[i] = lsv_JH_XM_KT_ZKT_File1.SelectedItems[i].SubItems[0].Text;
                                SetFileState(_obj, value, true);
                            }
                        }
                    }
                    else if("btn_Subject_Box_RightAll".Equals(name))
                    {
                        int count = lsv_JH_XM_KT_ZKT_File1.Items.Count;
                        if(count > 0)
                        {
                            object[] _obj = new object[count];
                            for(int i = 0; i < count; i++)
                                _obj[i] = lsv_JH_XM_KT_ZKT_File1.Items[i].SubItems[0].Text;
                            SetFileState(_obj, value, true);
                        }
                    }
                    else if("btn_Subject_Box_Left".Equals(name))
                    {
                        int count = lsv_JH_XM_KT_ZKT_File2.SelectedItems.Count;
                        if(count > 0)
                        {
                            object[] _obj = new object[count];
                            for(int i = 0; i < count; i++)
                                _obj[i] = lsv_JH_XM_KT_ZKT_File2.SelectedItems[i].SubItems[0].Text;
                            SetFileState(_obj, value, false);
                        }
                    }
                    else if("btn_Subject_Box_LeftAll".Equals(name))
                    {
                        int count = lsv_JH_XM_KT_ZKT_File2.Items.Count;
                        if(count > 0)
                        {
                            object[] _obj = new object[count];
                            for(int i = 0; i < count; i++)
                                _obj[i] = lsv_JH_XM_KT_ZKT_File2.Items[i].SubItems[0].Text;
                            SetFileState(_obj, value, false);
                        }
                    }
                    LoadFileBoxTable(value, tab_Subject_Info.Tag, ControlType.Subject);
                }
                else
                    XtraMessageBox.Show("请先添加案卷盒。", "保存失败");
            }
            //重大专项/研发
            else if(name.Contains("btn_Imp_Box"))
            {
                object value = cbo_Imp_Box.SelectedValue;
                if(value != null)
                {
                    if("btn_Imp_Box_Right".Equals(name))
                    {
                        if(lsv_Imp_File1.SelectedItems.Count > 0)
                        {
                            int count = lsv_Imp_File1.SelectedItems.Count;
                            if(count > 0)
                            {
                                object[] _obj = new object[count];
                                for(int i = 0; i < count; i++)
                                    _obj[i] = lsv_Imp_File1.SelectedItems[i].SubItems[0].Text;
                                SetFileState(_obj, value, true);
                            }
                        }
                    }
                    else if("btn_Imp_Box_RightAll".Equals(name))
                    {
                        int count = lsv_Imp_File1.Items.Count;
                        if(count > 0)
                        {
                            object[] _obj = new object[count];
                            for(int i = 0; i < count; i++)
                                _obj[i] = lsv_Imp_File1.Items[i].SubItems[0].Text;
                            SetFileState(_obj, value, true);
                        }
                    }
                    else if("btn_Imp_Box_Left".Equals(name))
                    {
                        int count = lsv_Imp_File2.SelectedItems.Count;
                        if(count > 0)
                        {
                            object[] _obj = new object[count];
                            for(int i = 0; i < count; i++)
                                _obj[i] = lsv_Imp_File2.SelectedItems[i].SubItems[0].Text;
                            SetFileState(_obj, value, false);
                        }
                    }
                    else if("btn_Imp_Box_LeftAll".Equals(name))
                    {
                        int count = lsv_Imp_File2.Items.Count;
                        if(count > 0)
                        {
                            object[] _obj = new object[count];
                            for(int i = 0; i < count; i++)
                                _obj[i] = lsv_Imp_File2.Items[i].SubItems[0].Text;
                            SetFileState(_obj, value, false);
                        }
                    }
                    LoadFileBoxTable(value, tab_Imp_Info.Tag, ControlType.Imp);
                }
                else
                    XtraMessageBox.Show("请先添加案卷盒。", "保存失败");
            }
            //重大专项/研发 - 信息
            else if(name.Contains("btn_Special_Box"))
            {
                object value = cbo_Special_Box.SelectedValue;
                if(value != null)
                {
                    if("btn_Special_Box_Right".Equals(name))
                    {
                        if(lsv_Imp_Dev_File1.SelectedItems.Count > 0)
                        {
                            int count = lsv_Imp_Dev_File1.SelectedItems.Count;
                            if(count > 0)
                            {
                                object[] _obj = new object[count];
                                for(int i = 0; i < count; i++)
                                    _obj[i] = lsv_Imp_Dev_File1.SelectedItems[i].SubItems[0].Text;
                                SetFileState(_obj, value, true);
                            }
                        }
                    }
                    else if("btn_Special_Box_RightAll".Equals(name))
                    {
                        int count = lsv_Imp_Dev_File1.Items.Count;
                        if(count > 0)
                        {
                            object[] _obj = new object[count];
                            for(int i = 0; i < count; i++)
                                _obj[i] = lsv_Imp_Dev_File1.Items[i].SubItems[0].Text;
                            SetFileState(_obj, value, true);
                        }
                    }
                    else if("btn_Special_Box_Left".Equals(name))
                    {
                        int count = lsv_Imp_Dev_File2.SelectedItems.Count;
                        if(count > 0)
                        {
                            object[] _obj = new object[count];
                            for(int i = 0; i < count; i++)
                                _obj[i] = lsv_Imp_Dev_File2.SelectedItems[i].SubItems[0].Text;
                            SetFileState(_obj, value, false);
                        }
                    }
                    else if("btn_Special_Box_LeftAll".Equals(name))
                    {
                        int count = lsv_Imp_Dev_File2.Items.Count;
                        if(count > 0)
                        {
                            object[] _obj = new object[count];
                            for(int i = 0; i < count; i++)
                                _obj[i] = lsv_Imp_Dev_File2.Items[i].SubItems[0].Text;
                            SetFileState(_obj, value, false);
                        }
                    }
                    LoadFileBoxTable(value, tab_Special_Info.Tag, ControlType.Special);
                }
                else
                    XtraMessageBox.Show("请先添加案卷盒。", "保存失败");
            }
        }
     
        /// <summary>
        /// 文件归档
        /// </summary>
        /// <param name="_obj">待处理文件IDS</param>
        /// <param name="pbid">案卷盒ID</param>
        /// <param name="pbid">ture:归档;false:不归档</param>
        private void SetFileState(object[] fileIds, object boxId, bool isGD)
        {
            if (isGD)
            {
                string updateSQL = string.Empty;
                for(int i = 0; i < fileIds.Length; i++)
                {
                    updateSQL += $"UPDATE processing_file_list SET pfl_box_id='{boxId}', pfl_box_sort='{i}' WHERE pfl_id='{fileIds[i]}';";
                }
                SqlHelper.ExecuteNonQuery(updateSQL);
            }
            else
            {
                //将文件状态置为未归档
                StringBuilder updateSql = new StringBuilder($"UPDATE processing_file_list SET pfl_box_id=NULL WHERE pfl_id IN (");
                for(int i = 0; i < fileIds.Length; i++)
                    updateSql.Append($"'{fileIds[i]}'{(i == fileIds.Length - 1 ? ");" : ",")}");
                SqlHelper.ExecuteNonQuery(updateSql.ToString());
            }
        }
    
        /// <summary>
        /// 计划 - 增加/删除案卷盒
        /// </summary>
        private void Lbl_Box_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Label label = sender as Label;
            //计划
            if(label.Name.Contains("lbl_Plan_Box"))
            {
                object objId = tab_Plan_Info.Tag;
                if(objId != null)
                {
                    if("lbl_Plan_Box_Add".Equals(label.Name))//新增
                    {
                        //当前已有盒号数量
                        int amount = Convert.ToInt32(SqlHelper.ExecuteOnlyOneQuery($"SELECT COUNT(pb_box_number) FROM processing_box WHERE pb_obj_id='{objId}'"));
                        string[] gch = GetBoxCode(objId, null, 1, DateTime.Now.Year.ToString(), null, ToolHelper.GetValue(unitCode));
                        string insertSql = $"INSERT INTO processing_box(pb_id, pb_box_number, pb_gc_fix, pb_gc_id, pb_gc_number, pb_obj_id, pb_unit_id) " +
                            $"VALUES('{Guid.NewGuid().ToString()}', '{amount + 1}', '{gch[0]}', '{gch[0] + gch[1]}', '{gch[1]}', '{objId}', '{unitCode}{Tag}')";
                        SqlHelper.ExecuteNonQuery(insertSql);
                    }
                    else if("lbl_Plan_Box_Remove".Equals(label.Name))//删除
                    {
                        object _temp = SqlHelper.ExecuteOnlyOneQuery($"SELECT MAX(pb_box_number) FROM processing_box WHERE pb_obj_id='{objId}'");
                        if(_temp != null)
                        {
                            int currentBoxId = Convert.ToInt32(SqlHelper.ExecuteOnlyOneQuery($"SELECT pb_box_number FROM processing_box WHERE pb_id='{cbo_Plan_Box.SelectedValue}'"));
                            if(Convert.ToInt32(_temp) > currentBoxId)
                                XtraMessageBox.Show("请先删除较大盒号。", "删除失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            else if(XtraMessageBox.Show("删除当前案卷盒会清空盒下已归档的文件，是否继续？", "删除确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                object value = cbo_Plan_Box.SelectedValue;
                                if(value != null)
                                {
                                    SqlHelper.ExecuteNonQuery(
                                        $"UPDATE processing_file_list SET pfl_box_id=NULL WHERE pfl_box_id='{value}';" +
                                        $"DELETE FROM processing_box WHERE pb_id='{value}'");
                                }
                            }
                        }
                    }
                    LoadBoxList(objId, ControlType.Plan);
                    LoadFileBoxTable(cbo_Plan_Box.SelectedValue, objId, ControlType.Plan);
                }
            }
            //计划-项目
            if(label.Name.Contains("lbl_Project_Box"))
            {
                object objId = tab_Project_Info.Tag;
                if(objId != null)
                {
                    if("lbl_Project_Box_Add".Equals(label.Name))//新增
                    {
                        //当前已有盒号数量
                        int amount = Convert.ToInt32(SqlHelper.ExecuteOnlyOneQuery($"SELECT COUNT(pb_box_number) FROM processing_box WHERE pb_obj_id='{objId}'"));
                        string[] gch = GetBoxCode(objId, txt_Project_Code.Text, 1, txt_Project_Year.Text, txt_Special_Code.Text, ToolHelper.GetValue(unitCode));
                        string insertSql = $"INSERT INTO processing_box(pb_id, pb_box_number, pb_gc_fix, pb_gc_id, pb_gc_number, pb_obj_id, pb_unit_id) " +
                            $"VALUES('{Guid.NewGuid().ToString()}', '{amount + 1}', '{gch[0]}', '{gch[0] + gch[1]}', '{gch[1]}', '{objId}', '{unitCode}{Tag}')";
                        SqlHelper.ExecuteNonQuery(insertSql);
                    }
                    else if("lbl_Project_Box_Remove".Equals(label.Name))//删除
                    {
                        object _temp = SqlHelper.ExecuteOnlyOneQuery($"SELECT MAX(pb_box_number) FROM processing_box WHERE pb_obj_id='{objId}'");
                        if(_temp != null)
                        {
                            int currentBoxId = Convert.ToInt32(SqlHelper.ExecuteOnlyOneQuery($"SELECT pb_box_number FROM processing_box WHERE pb_id='{cbo_Project_Box.SelectedValue}'"));
                            if(Convert.ToInt32(_temp) > currentBoxId)
                                XtraMessageBox.Show("请先删除较大盒号。", "删除失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            else if(XtraMessageBox.Show("删除当前案卷盒会清空盒下已归档的文件，是否继续？", "删除确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                object value = cbo_Project_Box.SelectedValue;
                                if(value != null)
                                {
                                    SqlHelper.ExecuteNonQuery(
                                         $"UPDATE processing_file_list SET pfl_box_id=NULL WHERE pfl_box_id='{value}';" +
                                         $"DELETE FROM processing_box WHERE pb_id='{value}'");
                                }
                            }
                        }
                    }
                    LoadBoxList(objId, ControlType.Project);
                    LoadFileBoxTable(cbo_Project_Box.SelectedValue, objId, ControlType.Plan);
                }
            }
            //计划-项目-课题-子课题
            if(label.Name.Contains("lbl_Subject_Box"))
            {
                object objId = tab_Subject_Info.Tag;
                if(objId != null)
                {
                    if("lbl_Subject_Box_Add".Equals(label.Name))//新增
                    {
                        //当前已有盒号数量
                        int amount = Convert.ToInt32(SqlHelper.ExecuteOnlyOneQuery($"SELECT COUNT(pb_box_number) FROM processing_box WHERE pb_obj_id='{objId}'"));
                        string[] gch = GetBoxCode(objId, txt_Subject_Code.Text, 1, txt_Subject_Year.Text, txt_Special_Code.Text, ToolHelper.GetValue(unitCode));
                        string insertSql = $"INSERT INTO processing_box(pb_id, pb_box_number, pb_gc_fix, pb_gc_id, pb_gc_number, pb_obj_id, pb_unit_id) " +
                            $"VALUES('{Guid.NewGuid().ToString()}', '{amount + 1}', '{gch[0]}', '{gch[0] + gch[1]}', '{gch[1]}', '{objId}', '{unitCode}{Tag}')";
                        SqlHelper.ExecuteNonQuery(insertSql);
                    }
                    else if("lbl_Subject_Box_Remove".Equals(label.Name))//删除
                    {
                        object _temp = SqlHelper.ExecuteOnlyOneQuery($"SELECT MAX(pb_box_number) FROM processing_box WHERE pb_obj_id='{objId}'");
                        if(_temp != null)
                        {
                            object value = cbo_Subject_Box.SelectedValue;
                            int currentBoxId = Convert.ToInt32(SqlHelper.ExecuteOnlyOneQuery($"SELECT pb_box_number FROM processing_box WHERE pb_id='{value}'"));
                            if(Convert.ToInt32(_temp) > currentBoxId)
                                XtraMessageBox.Show("请先删除较大盒号。", "删除失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            else if(XtraMessageBox.Show("删除当前案卷盒会清空盒下已归档的文件，是否继续？", "删除确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                SqlHelper.ExecuteNonQuery(
                                         $"UPDATE processing_file_list SET pfl_box_id=NULL WHERE pfl_box_id='{value}';" +
                                         $"DELETE FROM processing_box WHERE pb_id='{value}'");
                            }
                        }
                    }
                    LoadBoxList(objId, ControlType.Subject);
                    LoadFileBoxTable(cbo_Subject_Box.SelectedValue, objId, ControlType.Subject);
                }
            }
            //计划-课题
            if(label.Name.Contains("lbl_Topic_Box"))
            {
                object objId = tab_Topic_Info.Tag;
                if(objId != null)
                {
                    if("lbl_Topic_Box_Add".Equals(label.Name))//新增
                    {
                        //当前已有盒号数量
                        int amount = Convert.ToInt32(SqlHelper.ExecuteOnlyOneQuery($"SELECT COUNT(pb_box_number) FROM processing_box WHERE pb_obj_id='{objId}'"));
                        string[] gch = GetBoxCode(objId, txt_Topic_Code.Text, 1, txt_Topic_Year.Text, txt_Special_Code.Text, ToolHelper.GetValue(unitCode));
                        string insertSql = $"INSERT INTO processing_box(pb_id, pb_box_number, pb_gc_fix, pb_gc_id, pb_gc_number, pb_obj_id, pb_unit_id) " +
                            $"VALUES('{Guid.NewGuid().ToString()}', '{amount + 1}', '{gch[0]}', '{gch[0] + gch[1]}', '{gch[1]}', '{objId}', '{unitCode}{Tag}')";
                        SqlHelper.ExecuteNonQuery(insertSql);
                    }
                    else if("lbl_Topic_Box_Remove".Equals(label.Name))//删除
                    {
                        object _temp = SqlHelper.ExecuteOnlyOneQuery($"SELECT MAX(pb_box_number) FROM processing_box WHERE pb_obj_id='{objId}'");
                        if(_temp != null)
                        {
                            object value = cbo_Topic_Box.SelectedValue;
                            int currentBoxId = Convert.ToInt32(SqlHelper.ExecuteOnlyOneQuery($"SELECT pb_box_number FROM processing_box WHERE pb_id='{value}'"));
                            if(Convert.ToInt32(_temp) > currentBoxId)
                                XtraMessageBox.Show("请先删除较大盒号。", "删除失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            else if(XtraMessageBox.Show("删除当前案卷盒会清空盒下已归档的文件，是否继续？", "删除确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                SqlHelper.ExecuteNonQuery(
                                         $"UPDATE processing_file_list SET pfl_box_id=NULL WHERE pfl_box_id='{value}';" +
                                         $"DELETE FROM processing_box WHERE pb_id='{value}'");
                            }
                        }
                    }
                    LoadBoxList(objId, ControlType.Topic);
                    LoadFileBoxTable(cbo_Topic_Box.SelectedValue, objId, ControlType.Topic);
                }
            }
            //重大专项/研发
            if(label.Name.Contains("lbl_Imp_Box"))
            {
                object objId = tab_Imp_Info.Tag;
                if(objId != null)
                {
                    if("lbl_Imp_Box_Add".Equals(label.Name))//新增
                    {
                        //当前已有盒号数量
                        int amount = Convert.ToInt32(SqlHelper.ExecuteOnlyOneQuery($"SELECT COUNT(pb_box_number) FROM processing_box WHERE pb_obj_id='{objId}'"));
                        string[] gch = GetBoxCode(objId, null, 1, DateTime.Now.Year.ToString(), txt_Special_Code.Text, ToolHelper.GetValue(unitCode));
                        string insertSql = $"INSERT INTO processing_box(pb_id, pb_box_number, pb_gc_fix, pb_gc_id, pb_gc_number, pb_obj_id, pb_unit_id) " +
                            $"VALUES('{Guid.NewGuid().ToString()}', '{amount + 1}', '{gch[0]}', '{gch[0] + gch[1]}', '{gch[1]}', '{objId}', '{unitCode}{Tag}')";
                        SqlHelper.ExecuteNonQuery(insertSql);
                    }
                    else if("lbl_Imp_Box_Remove".Equals(label.Name))//删除
                    {
                        object _temp = SqlHelper.ExecuteOnlyOneQuery($"SELECT MAX(pb_box_number) FROM processing_box WHERE pb_obj_id='{objId}'");
                        if(_temp != null)
                        {
                            object value = cbo_Imp_Box.SelectedValue;
                            int currentBoxId = Convert.ToInt32(SqlHelper.ExecuteOnlyOneQuery($"SELECT pb_box_number FROM processing_box WHERE pb_id='{value}'"));
                            if(Convert.ToInt32(_temp) > currentBoxId)
                                XtraMessageBox.Show("请先删除较大盒号。", "删除失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            else if(XtraMessageBox.Show("删除当前案卷盒会清空盒下已归档的文件，是否继续？", "删除确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                SqlHelper.ExecuteNonQuery(
                                          $"UPDATE processing_file_list SET pfl_box_id=NULL WHERE pfl_box_id='{value}';" +
                                          $"DELETE FROM processing_box WHERE pb_id='{value}'");
                            }
                        }
                    }
                    LoadBoxList(objId, ControlType.Imp);
                    LoadFileBoxTable(cbo_Imp_Box.SelectedValue, objId, ControlType.Imp);
                }
            }
            //重大专项/研发 - 信息
            if(label.Name.Contains("lbl_Special_Box"))
            {
                object objId = tab_Special_Info.Tag;
                if(objId != null)
                {
                    if("lbl_Special_Box_Add".Equals(label.Name))//新增
                    {
                        //当前已有盒号数量
                        int amount = Convert.ToInt32(SqlHelper.ExecuteOnlyOneQuery($"SELECT COUNT(pb_box_number) FROM processing_box WHERE pb_obj_id='{objId}'"));
                        string[] gch = GetBoxCode(objId, null, 1, DateTime.Now.Year.ToString(), txt_Special_Code.Text, ToolHelper.GetValue(unitCode));
                        string insertSql = $"INSERT INTO processing_box(pb_id, pb_box_number, pb_gc_fix, pb_gc_id, pb_gc_number, pb_obj_id, pb_unit_id) " +
                            $"VALUES('{Guid.NewGuid().ToString()}', '{amount + 1}', '{gch[0]}', '{gch[0] + gch[1]}', '{gch[1]}', '{objId}', '{unitCode}{Tag}')";
                        SqlHelper.ExecuteNonQuery(insertSql);
                    }
                    else if("lbl_Special_Box_Remove".Equals(label.Name))//删除
                    {
                        object _temp = SqlHelper.ExecuteOnlyOneQuery($"SELECT MAX(pb_box_number) FROM processing_box WHERE pb_obj_id='{objId}'");
                        if(_temp != null)
                        {
                            object value = cbo_Special_Box.SelectedValue;
                            int currentBoxId = Convert.ToInt32(SqlHelper.ExecuteOnlyOneQuery($"SELECT pb_box_number FROM processing_box WHERE pb_id='{value}'"));
                            if(Convert.ToInt32(_temp) > currentBoxId)
                                XtraMessageBox.Show("请先删除较大盒号。", "删除失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            else if(XtraMessageBox.Show("删除当前案卷盒会清空盒下已归档的文件，是否继续？", "删除确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                SqlHelper.ExecuteNonQuery(
                                        $"UPDATE processing_file_list SET pfl_box_id=NULL WHERE pfl_box_id='{value}';" +
                                        $"DELETE FROM processing_box WHERE pb_id='{value}'");
                            }
                        }
                    }
                    LoadBoxList(objId, ControlType.Special);
                    LoadFileBoxTable(cbo_Special_Box.SelectedValue, objId, ControlType.Special);
                }
            }
        }

        /// <summary>
        /// 根据预设规则获取编码
        /// </summary>
        /// <param name="type">0：案卷 1：馆藏号</param>
        private string[] GetBoxCode(object objId, object objCode, int type, string year, string zxCode, string unitCode)
        {
            string[] code = new string[2] { string.Empty, string.Empty };
            DataRow row = SqlHelper.ExecuteSingleRowQuery($"SELECT * FROM code_rule WHERE cr_type='{type}';");
            if(row != null)
            {
                string fix = ToolHelper.GetValue(row["cr_fixed"]);
                string symbol = ToolHelper.GetValue(row["cr_split_symbol"]);
                if(!string.IsNullOrEmpty(fix))
                    code[0] += $"{fix + symbol}";
                string template = ToolHelper.GetValue(row["cr_template"]);
                string[] strs = GetGroupCode(template, symbol);
                for(int i = 0; i < strs.Length; i++)
                {
                    if("AAAA".Equals(strs[i]))//专项编号
                    {
                        if(!string.IsNullOrEmpty(zxCode))
                            code[0] += zxCode;
                        else
                            continue;
                    }
                    else if("BBBB".Equals(strs[i]))//项目/课题编号
                        code[0] += objCode;
                    else if("CCCC".Equals(strs[i]))//来源单位
                    {
                        if(!string.IsNullOrEmpty(unitCode))
                            code[0] += unitCode;
                        else
                            continue;
                    }
                    else if("YYYY".Equals(strs[i]))
                        code[0] += year;
                    else
                    {
                        int length = strs[i].Length;
                        int amount = 0;
                        if(type == 0)
                        {
                            //   amount = SqlHelper.ExecuteCountQuery($"SELECT COUNT(pt_id) FROM files_tag_info WHERE pt_special_id='{sourceUnitId}'") + 1;
                        }
                        else if(type == 1)
                            amount = GetBoxWaterNumber(length, unitCode + Tag);
                        code[1] += amount.ToString().PadLeft(length, '0');
                    }
                    code[0] += symbol;
                }

                if(!string.IsNullOrEmpty(symbol) && code[0].EndsWith(symbol))
                    code[0] = code[0].Substring(0, code.Length - 1);
            }
            return code;
        }

        /// <summary>
        /// 获取馆藏号流水号
        /// （优先获取已删除的）
        /// </summary>
        private int GetBoxWaterNumber(int length, string specialId)
        {
            string querySql = $"SELECT COUNT(pb_id) FROM processing_box WHERE pb_unit_id='{specialId}'";
            int max = SqlHelper.ExecuteCountQuery(querySql);
            for(int i = 1; i <= max; i++)
            {
                string _str = i.ToString().PadLeft(length, '0');
                int temp = SqlHelper.ExecuteCountQuery(querySql + $" AND pb_gc_id LIKE '%{_str}'");
                if(temp == 0)
                    return i;
            }
            return max + 1;
        }

        private string[] GetGroupCode(string value, string symbol)
        {
            List<string> list = new List<string>();
            int i = 0;
            int length = 4 + symbol.Length;
            while(value.Length >= length)
            {
                if(int.TryParse(value, out int result))
                {
                    list.Add(value);
                    break;
                }
                else
                {
                    string code = value.Substring(i, length - symbol.Length);
                    list.Add(code);
                    value = value.Remove(i, length);
                }
            }
            if(value.Length < length)
                list.Add(value);
            return list.ToArray();
        }

        /// <summary>
        /// 计划 - 加载案卷盒列表
        /// </summary>
        /// <param name="objId">案卷盒所属对象ID</param>
        /// <param name="type">对象类型</param>
        private void LoadBoxList(object objId, ControlType type)
        {
            DataTable table = SqlHelper.ExecuteQuery($"SELECT * FROM processing_box WHERE pb_obj_id='{objId}' ORDER BY pb_box_number ASC");
            if(type == ControlType.Plan)
            {
                cbo_Plan_Box.DataSource = table;
                cbo_Plan_Box.DisplayMember = "pb_box_number";
                cbo_Plan_Box.ValueMember = "pb_id";
                if(table.Rows.Count > 0)
                {
                    int maxAmount = cbo_Plan_Box.Items.Count;
                    if(maxAmount > 0)
                        cbo_Plan_Box.SelectedIndex = maxAmount - 1;
                }
                Cbo_Box_SelectionChangeCommitted(cbo_Plan_Box, null);
            }
            else if(type == ControlType.Project)
            {
                cbo_Project_Box.DataSource = table;
                cbo_Project_Box.DisplayMember = "pb_box_number";
                cbo_Project_Box.ValueMember = "pb_id";
                if(table.Rows.Count > 0)
                {
                    int maxAmount = cbo_Project_Box.Items.Count;
                    if(maxAmount > 0)
                        cbo_Project_Box.SelectedIndex = maxAmount - 1;
                }
                Cbo_Box_SelectionChangeCommitted(cbo_Project_Box, null);
            }
            else if(type == ControlType.Topic)
            {
                cbo_Topic_Box.DataSource = table;
                cbo_Topic_Box.DisplayMember = "pb_box_number";
                cbo_Topic_Box.ValueMember = "pb_id";
                if(table.Rows.Count > 0)
                {
                    int maxAmount = cbo_Topic_Box.Items.Count;
                    if(maxAmount > 0)
                        cbo_Topic_Box.SelectedIndex = maxAmount - 1;
                }
                Cbo_Box_SelectionChangeCommitted(cbo_Topic_Box, null);
            }
            else if(type == ControlType.Subject)
            {
                cbo_Subject_Box.DataSource = table;
                cbo_Subject_Box.DisplayMember = "pb_box_number";
                cbo_Subject_Box.ValueMember = "pb_id";
                if(table.Rows.Count > 0)
                {
                    int maxAmount = cbo_Subject_Box.Items.Count;
                    if(maxAmount > 0)
                        cbo_Subject_Box.SelectedIndex = maxAmount - 1;
                }
                Cbo_Box_SelectionChangeCommitted(cbo_Subject_Box, null);
            }
            else if(type == ControlType.Imp)
            {
                cbo_Imp_Box.DataSource = table;
                cbo_Imp_Box.DisplayMember = "pb_box_number";
                cbo_Imp_Box.ValueMember = "pb_id";
                if(table.Rows.Count > 0)
                {
                    int maxAmount = cbo_Imp_Box.Items.Count;
                    if(maxAmount > 0)
                        cbo_Imp_Box.SelectedIndex = maxAmount - 1;
                }
                Cbo_Box_SelectionChangeCommitted(cbo_Imp_Box, null);
            }
            else if(type == ControlType.Special)
            {
                cbo_Special_Box.DataSource = table;
                cbo_Special_Box.DisplayMember = "pb_box_number";
                cbo_Special_Box.ValueMember = "pb_id";
                if(table.Rows.Count > 0)
                {
                    int maxAmount = cbo_Special_Box.Items.Count;
                    if(maxAmount > 0)
                        cbo_Special_Box.SelectedIndex = maxAmount - 1;
                }
                Cbo_Box_SelectionChangeCommitted(cbo_Special_Box, null);
            }
        }

        /// <summary>
        /// 案卷盒切换事件
        /// </summary>
        private void Cbo_Box_SelectionChangeCommitted(object sender, EventArgs e)
        {
            System.Windows.Forms.ComboBox comboBox = sender as System.Windows.Forms.ComboBox;
            if (comboBox.Name.Contains("Plan"))
            {
                object pbId = comboBox.SelectedValue;
                LoadFileBoxTable(pbId, tab_Plan_Info.Tag, ControlType.Plan);
            }
            else if(comboBox.Name.Contains("Project"))
            {
                object pbId = comboBox.SelectedValue;
                LoadFileBoxTable(pbId, tab_Project_Info.Tag, ControlType.Project);
            }
            else if(comboBox.Name.Contains("Topic"))
            {
                object pbId = comboBox.SelectedValue;
                LoadFileBoxTable(pbId, tab_Topic_Info.Tag, ControlType.Topic);
            }
            else if(comboBox.Name.Contains("Subject"))
            {
                object pbId = comboBox.SelectedValue;
                LoadFileBoxTable(pbId, tab_Subject_Info.Tag, ControlType.Subject);
            }
            else if(comboBox.Name.Contains("Imp"))
            {
                object pbId = comboBox.SelectedValue;
                LoadFileBoxTable(pbId, tab_Imp_Info.Tag, ControlType.Imp);
            }
            else if(comboBox.Name.Contains("Special"))
            {
                object pbId = comboBox.SelectedValue;
                LoadFileBoxTable(pbId, tab_Special_Info.Tag, ControlType.Special);
            }
        }
     
        /// <summary>
        /// 根目录切换事件
        /// </summary>
        private void Tab_MenuList_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = tab_MenuList.SelectedIndex;
            if(index != -1)
            {
                string currentPageName = tab_MenuList.TabPages[index].Name;
                if("plan".Equals(currentPageName))
                {
                }
                else if("project".Equals(currentPageName))
                {
                    if(string.IsNullOrEmpty(txt_Project_Name.Text))
                    {
                        dgv_Project_FileList.ColumnHeadersDefaultCellStyle = DataGridViewStyleHelper.GetHeaderStyle();
                        dgv_Project_FileList.DefaultCellStyle = DataGridViewStyleHelper.GetCellStyle();
                        dgv_Project_FileValid.ColumnHeadersDefaultCellStyle = DataGridViewStyleHelper.GetHeaderStyle();
                        dgv_Project_FileValid.DefaultCellStyle = DataGridViewStyleHelper.GetCellStyle();
                    }
                }
                else if("Subject".Equals(currentPageName))
                {
                    if(string.IsNullOrEmpty(txt_Subject_Name.Text))
                    {
                        dgv_Subject_FileList.ColumnHeadersDefaultCellStyle = DataGridViewStyleHelper.GetHeaderStyle();
                        dgv_Subject_FileList.DefaultCellStyle = DataGridViewStyleHelper.GetCellStyle();
                        dgv_Subject_FileValid.ColumnHeadersDefaultCellStyle = DataGridViewStyleHelper.GetHeaderStyle();
                        dgv_Subject_FileValid.DefaultCellStyle = DataGridViewStyleHelper.GetCellStyle();
                    }
                }
                else if("plan_topic".Equals(currentPageName))
                {
                    if(string.IsNullOrEmpty(txt_Topic_Name.Text))
                    {
                        dgv_Topic_FileList.ColumnHeadersDefaultCellStyle = DataGridViewStyleHelper.GetHeaderStyle();
                        dgv_Topic_FileList.DefaultCellStyle = DataGridViewStyleHelper.GetCellStyle();
                        dgv_Topic_FileValid.ColumnHeadersDefaultCellStyle = DataGridViewStyleHelper.GetHeaderStyle();
                        dgv_Topic_FileValid.DefaultCellStyle = DataGridViewStyleHelper.GetCellStyle();
                    }
                }
                else if("imp".Equals(currentPageName))
                {

                }
                else if("Special".Equals(currentPageName))
                {
                    if(string.IsNullOrEmpty(txt_Special_Name.Text))
                    {
                        dgv_Special_FileList.ColumnHeadersDefaultCellStyle = DataGridViewStyleHelper.GetHeaderStyle();
                        dgv_Special_FileList.DefaultCellStyle = DataGridViewStyleHelper.GetCellStyle();
                        dgv_Special_FileValid.ColumnHeadersDefaultCellStyle = DataGridViewStyleHelper.GetHeaderStyle();
                        dgv_Special_FileValid.DefaultCellStyle = DataGridViewStyleHelper.GetCellStyle();
                    }
                }
            }
        }
        
        /// <summary>
        /// 加载基本信息和文件列表
        /// </summary>
        /// <param name="planId">【项目/课题】ID</param>
        /// <param name="type">对象类型</param>
        private void LoadPageBasicInfo(TreeNode node, ControlType type)
        {
            errorProvider1.Clear();
            if(type == ControlType.Project)
            {
                pal_Project_BtnGroup.Enabled = !(node.ForeColor == DisEnbleColor);
                DataRow row = SqlHelper.ExecuteSingleRowQuery($"SELECT * FROM project_info WHERE pi_id='{node.Name}'");
                if(row != null)
                {
                    tab_Project_Info.Tag = row["pi_id"];
                    txt_Project_Code.Text = ToolHelper.GetValue(row["pi_code"]);
                    txt_Project_Name.Text = ToolHelper.GetValue(row["pi_name"]);
                    txt_Project_Field.Text = ToolHelper.GetValue(row["pi_field"]);
                    txt_Project_Theme.Text = ToolHelper.GetValue(row["pb_theme"]);
                    txt_Project_Funds.Text = ToolHelper.GetValue(row["pi_funds"]);

                    string startTime = ToolHelper.GetValue(row["pi_start_datetime"]);
                    DateTime _startTime = new DateTime();
                    if(DateTime.TryParse(startTime, out _startTime))
                        dtp_Project_StartTime.Value = _startTime;

                    string endTime = ToolHelper.GetValue(row["pi_end_datetime"]);
                    DateTime _endTime = new DateTime();
                    if(DateTime.TryParse(endTime, out _endTime))
                        dtp_Project_EndTime.Value = _endTime;

                    txt_Project_Year.Text = ToolHelper.GetValue(row["pi_year"]);
                    txt_Project_Unit.Text = ToolHelper.GetValue(row["pi_unit"]);
                    txt_Project_Province.Text = ToolHelper.GetValue(row["pi_province"]);
                    txt_Project_UnitUser.Text = ToolHelper.GetValue(row["pi_uniter"]);
                    txt_Project_ProUser.Text = ToolHelper.GetValue(row["pi_prouser"]);
                    txt_Project_Intro.Text = ToolHelper.GetValue(row["pi_intro"]);
                    EnableControls(type, Convert.ToInt32(row["pi_submit_status"]) != 1);
                    project.Tag = row["pi_obj_id"];
                }

                LoadFileList(dgv_Project_FileList, "project_fl_", node.Name);
                LoadFileValidList(dgv_Project_FileValid, node.Name, "project_fc_");
                LoadDocList(node.Name, ControlType.Project);
            }
            else if(type == ControlType.Topic)
            {
                pal_Topic_BtnGroup.Enabled = !(node.ForeColor == DisEnbleColor);
                DataRow row = SqlHelper.ExecuteSingleRowQuery($"SELECT * FROM topic_info WHERE ti_id='{node.Name}'");
                if(row != null)
                {
                    tab_Topic_Info.Tag = row["ti_id"];
                    txt_Topic_Code.Text = ToolHelper.GetValue(row["ti_code"]);
                    txt_Topic_Name.Text = ToolHelper.GetValue(row["ti_name"]);
                    txt_Topic_Field.Text = ToolHelper.GetValue(row["ti_field"]);
                    txt_Topic_Theme.Text = ToolHelper.GetValue(row["tb_theme"]);
                    txt_Topic_Fund.Text = ToolHelper.GetValue(row["ti_funds"]);

                    string startTime = ToolHelper.GetValue(row["ti_start_datetime"]);
                    DateTime _startTime = new DateTime();
                    if(DateTime.TryParse(startTime, out _startTime))
                        dtp_Topic_StartTime.Value = _startTime;
                    string endTime = ToolHelper.GetValue(row["ti_end_datetime"]);
                    DateTime _endTime = new DateTime();
                    if(DateTime.TryParse(endTime, out _endTime))
                        dtp_Topic_EndTime.Value = _endTime;

                    txt_Topic_Year.Text = ToolHelper.GetValue(row["ti_year"]);
                    txt_Topic_Unit.Text = ToolHelper.GetValue(row["ti_unit"]);
                    txt_Topic_Province.Text = ToolHelper.GetValue(row["ti_province"]);
                    txt_Topic_UnitUser.Text = ToolHelper.GetValue(row["ti_uniter"]);
                    txt_Topic_ProUser.Text = ToolHelper.GetValue(row["ti_prouser"]);
                    txt_Topic_Intro.Text = ToolHelper.GetValue(row["ti_intro"]);
                    EnableControls(type, Convert.ToInt32(row["ti_submit_status"]) != 1);
                    topic.Tag = row["ti_obj_id"];
                }

                LoadFileList(dgv_Topic_FileList, "topic_fl_", node.Name);
                LoadFileValidList(dgv_Topic_FileValid, node.Name, "topic_fc_");
                LoadDocList(node.Name, ControlType.Topic);
            }
            else if(type == ControlType.Subject)
            {
                pal_Subject_BtnGroup.Enabled = !(node.ForeColor == DisEnbleColor);
                DataTable table = SqlHelper.ExecuteQuery($"SELECT * FROM subject_info WHERE si_id='{node.Name}'");
                if(table.Rows.Count > 0)
                {
                    DataRow row = table.Rows[0];
                    tab_Subject_Info.Tag = row["si_id"];
                    pal_Subject.Tag = row["si_obj_id"];
                    txt_Subject_Code.Text = ToolHelper.GetValue(row["si_code"]);
                    txt_Subject_Name.Text = ToolHelper.GetValue(row["si_name"]);
                    txt_Subject_Field.Text = ToolHelper.GetValue(row["si_field"]);
                    txt_Subject_Theme.Text = ToolHelper.GetValue(row["si_theme"]);
                    txt_Subject_Fund.Text = ToolHelper.GetValue(row["si_funds"]);

                    string startTime = ToolHelper.GetValue(row["si_start_datetime"]);
                    DateTime _startTime = new DateTime();
                    if(DateTime.TryParse(startTime, out _startTime))
                        dtp_Subject_StartTime.Value = _startTime;

                    string endTime = ToolHelper.GetValue(row["si_end_datetime"]);
                    DateTime _endTime = new DateTime();
                    if(DateTime.TryParse(endTime, out _endTime))
                        dtp_Subject_EndTime.Value = _endTime;

                    txt_Subject_Year.Text = ToolHelper.GetValue(row["si_year"]);
                    txt_Subject_Unit.Text = ToolHelper.GetValue(row["si_unit"]);
                    txt_Subject_Province.Text = ToolHelper.GetValue(row["si_province"]);
                    txt_Subject_Unituser.Text = ToolHelper.GetValue(row["si_uniter"]);
                    txt_Subject_ProUser.Text = ToolHelper.GetValue(row["si_prouser"]);
                    txt_Subject_Intro.Text = ToolHelper.GetValue(row["si_intro"]);
                    EnableControls(type, Convert.ToInt32(row["si_submit_status"]) != 1);
                    subject.Tag = row["si_obj_id"];

                }
               
                LoadFileList(dgv_Subject_FileList, "subject_fl_", node.Name);
                LoadFileValidList(dgv_Subject_FileValid, node.Name, "subject_fc_");
                LoadDocList(node.Name, ControlType.Subject);
            }
            else if(type == ControlType.Special)
            {
                DataRow row = SqlHelper.ExecuteSingleRowQuery($"SELECT * FROM imp_dev_info WHERE imp_id='{node.Name}'");
                if(row != null)
                {
                    txt_Special_Code.Text = ToolHelper.GetValue(row["imp_code"]);
                    txt_Special_Name.Text = ToolHelper.GetValue(row["imp_name"]);
                    txt_Special_Unit.Text = ToolHelper.GetValue(row["imp_unit"]);
                    tab_Special_Info.Tag = ToolHelper.GetValue(row["imp_id"]);
                    EnableControls(ControlType.Special, Convert.ToInt32(row["imp_submit_status"]) != 1);
                    special.Tag = row["imp_obj_id"];
                }
                if(workType == WorkType.PaperWork_Special)
                    special.Text = "研发信息";
                cbo_Special_HasNext.SelectedIndex = 0;

                pal_Special_BtnGroup.Enabled = !(node.ForeColor == DisEnbleColor);

                LoadFileList(dgv_Special_FileList, "special_fl_", node.Name);
                LoadFileValidList(dgv_Special_FileValid, node.Name, "special_fc_");
                LoadDocList(node.Name, ControlType.Special);
            }
        }

        /// <summary>
        /// 重置控件为默认状态
        /// </summary>
        /// <param name="type">对象类型</param>
        void ResetControls(ControlType type)
        {
            if(type == ControlType.Project)
            {
                dgv_Project_FileList.Tag = null;
                DataGridViewStyleHelper.ResetDataGridView(dgv_Project_FileList, false);
                DataGridViewStyleHelper.ResetDataGridView(dgv_Project_FileValid, false);
                txt_Project_Code.Clear();
                txt_Project_Name.Clear();
                txt_Project_Field.Clear();
                txt_Project_Theme.Clear();
                txt_Project_Funds.ResetText();
                dtp_Project_StartTime.ResetText();
                dtp_Project_EndTime.ResetText();
                txt_Project_Year.Clear();
                txt_Project_UnitUser.Clear();
                txt_Project_ProUser.Clear();
                txt_Project_Intro.Clear();
            }
            else if(type == ControlType.Topic)
            {
                dgv_Topic_FileList.Tag = null;
                DataGridViewStyleHelper.ResetDataGridView(dgv_Topic_FileList, false);
                DataGridViewStyleHelper.ResetDataGridView(dgv_Topic_FileValid, false);
                txt_Topic_Code.Clear();
                txt_Topic_Name.Clear();
                txt_Topic_Field.Clear();
                txt_Topic_Theme.Clear();
                txt_Topic_Fund.ResetText();
                dtp_Topic_StartTime.ResetText();
                dtp_Topic_EndTime.ResetText();
                txt_Topic_Year.Clear();
                txt_Topic_UnitUser.Clear();
                txt_Topic_ProUser.Clear();
                txt_Topic_Intro.Clear();
            }
            else if(type == ControlType.Subject)
            {
                dgv_Subject_FileList.Tag = null;
                DataGridViewStyleHelper.ResetDataGridView(dgv_Subject_FileList, false);
                DataGridViewStyleHelper.ResetDataGridView(dgv_Subject_FileValid, false);
                txt_Subject_Code.Clear();
                txt_Subject_Name.Clear();
                txt_Subject_Field.Clear();
                txt_Subject_Theme.Clear();
                txt_Subject_Fund.ResetText();
                dtp_Subject_StartTime.ResetText();
                dtp_Subject_EndTime.ResetText();
                txt_Subject_Year.Clear();
                txt_Subject_Unituser.Clear();
                txt_Subject_ProUser.Clear();
                txt_Subject_Intro.Clear();
            }
        }
        
        /// <summary>
        /// 控制控件的可用性
        /// </summary>
        /// <param name="type">对象类型</param>
        /// <param name="enable">是否可用</param>
        /// <param name="nextEnable">下一级是否可用</param>
        void EnableControls(ControlType type, bool enable)
        {
            if(type == ControlType.Plan)
            {
                foreach(Control item in pal_Plan_BtnGroup.Controls)
                {
                    item.Enabled = enable;
                    if(item.Name.Contains("Submit"))
                        item.Text = enable ? "返工" : "已返工";
                }
            }
            else if(type == ControlType.Project)
            {
                foreach(Control item in pal_Project_BtnGroup.Controls)
                {
                    item.Enabled = enable;
                    if(item.Name.Contains("Submit"))
                        item.Text = enable ? "返工" : "已返工";
                }
            }
            else if(type == ControlType.Subject)
            {
                foreach(Control item in pal_Subject_BtnGroup.Controls)
                {
                    item.Enabled = enable;
                    if(item.Name.Contains("Submit"))
                        item.Text = enable ? "返工" : "已返工";
                }
            }
            else if(type == ControlType.Topic)
            {
                foreach(Control item in pal_Topic_BtnGroup.Controls)
                {
                    item.Enabled = enable;
                    if(item.Name.Contains("Submit"))
                        item.Text = enable ? "返工" : "已返工";
                }
            }
            else if(type == ControlType.Imp)
            {
                foreach(Control item in pal_Imp_BtnGroup.Controls)
                {
                    item.Enabled = enable;
                    if(item.Name.Contains("Submit"))
                        item.Text = enable ? "返工" : "已返工";
                }
            }
            else if(type == ControlType.Special)
            {
                foreach(Control item in pal_Special_BtnGroup.Controls)
                {
                    item.Enabled = enable;
                    if(item.Name.Contains("Submit"))
                        item.Text = enable ? "返工" : "已返工";
                }
            }
        }
     
        /// <summary>
        /// 提交事件
        /// </summary>
        private void Btn_Submit_Click(object sender, EventArgs e)
        {
            object objId = null;
            string updateSql = null;
            ControlType type = ControlType.Default;
            if(XtraMessageBox.Show("请注意填写返工意见，确认将数据返工吗?", "提交确认", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                string name = (sender as KyoButton).Name;
                if(name.Contains("Plan"))
                {
                    type = ControlType.Plan;
                    objId = tab_Plan_Info.Tag;
                    updateSql = $"UPDATE project_info SET pi_submit_status={(int)SubmitStatus.NonSubmit} WHERE pi_id='{objId}';";
                }
                else if(name.Contains("Project"))
                {
                    type = ControlType.Project;
                    objId = tab_Project_Info.Tag;
                    updateSql = $"UPDATE project_info SET pi_submit_status={(int)SubmitStatus.NonSubmit} WHERE pi_id='{objId}';";
                }
                else if(name.Contains("Topic"))
                {
                    type = ControlType.Topic;
                    objId = tab_Topic_Info.Tag;
                    updateSql = $"UPDATE topic_info SET ti_submit_status='{(int)SubmitStatus.NonSubmit}' WHERE ti_id='{objId}';";
                }
                else if(name.Contains("Subject"))
                {
                    type = ControlType.Subject;
                    objId = tab_Subject_Info.Tag;
                    updateSql = $"UPDATE subject_info SET si_submit_status='{(int)SubmitStatus.NonSubmit}' WHERE si_id='{objId}';";
                }
                else if(name.Contains("Imp"))
                {
                    type = ControlType.Imp;
                    objId = tab_Imp_Info.Tag;
                    updateSql = $"UPDATE imp_info SET imp_submit_status='{(int)ObjectSubmitStatus.NonSubmit}' WHERE imp_id='{objId}';";
                }
                else if(name.Contains("Special"))
                {
                    type = ControlType.Special;
                    objId = tab_Special_Info.Tag;
                    updateSql = $"UPDATE imp_dev_info SET imp_submit_status='{(int)ObjectSubmitStatus.NonSubmit}' WHERE imp_id='{objId}';";
                }

                if(objId != null)
                {
                    updateSql += "INSERT INTO remake_log(rl_id, rl_date, rl_user_id, rl_obj_id, rl_type) VALUES (" +
                        $"'{Guid.NewGuid().ToString()}', '{DateTime.Now}', '{UserHelper.GetUser().UserKey}', '{objId}', '{(int)type}');";
                    SqlHelper.ExecuteNonQuery(updateSql);
                    EnableControls(type, false);
                    XtraMessageBox.Show("操作成功.");
                }
                else
                    XtraMessageBox.Show("操作失败.");
            }
        }

        /// <summary>
        /// 下拉框切换事件
        /// </summary>
        private void Cbo_Imp_HasNext_SelectionChangeCommitted(object sender, EventArgs e)
        {
            object id = dgv_Imp_FileList.Tag;
            if(id != null)
            {
                ShowTab("special", tab_MenuList.SelectedIndex + 1);
                ResetControls(ControlType.Special);

                object value = cbo_Imp_HasNext.SelectedValue;
                object[] _obj = SqlHelper.ExecuteRowsQuery($"SELECT dd_code, dd_name, dd_note FROM data_dictionary WHERE dd_id='{value}'");
                if(_obj.Length > 0)
                {
                    txt_Special_Code.Text = ToolHelper.GetValue(_obj[0]);
                    txt_Special_Name.Text = ToolHelper.GetValue(_obj[1]);
                    txt_Special_Intro.Text = ToolHelper.GetValue(_obj[2]);
                }
                pal_Special.Tag = id;

                if(DEV_TYPE == 1)
                    tab_MenuList.TabPages["Special"].Text = "研发信息";
            }
            else
            {
                XtraMessageBox.Show("请先保存当前信息！", "操作失败", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                if(cbo_Imp_HasNext.Items.Count > 0)
                    cbo_Imp_HasNext.SelectedIndex = 0;
            }
        }
        /// <summary>
        /// 文件链接点击事件
        /// </summary>
        private void Dgv_FileList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex != -1 && e.ColumnIndex != -1)
            {
                DataGridView dataGridView = sender as DataGridView;
                if(dataGridView.Columns[e.ColumnIndex].Name.Contains("link"))
                {
                    string path = ToolHelper.GetValue(dataGridView.CurrentCell.Value);
                    if(!string.IsNullOrEmpty(path))
                    {
                        if(path.Contains("；"))
                        {
                            string[] linkString = path.Split('；');
                            Frm_FileList fileList = GetFormHelper.GetFileList(linkString);
                            fileList.Show();
                            fileList.Activate();
                        }
                        else if(File.Exists(path))
                        {
                            if(XtraMessageBox.Show("是否打开文件?", "确认提示", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk) == DialogResult.Yes)
                                WinFormOpenHelper.OpenWinForm(0, "open", path, null, null, ShowWindowCommands.SW_NORMAL);
                        }
                        else
                            XtraMessageBox.Show("文件不存在。", "打开失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
    
        /// <summary>
        /// 新增文件
        /// </summary>
        private void Btn_AddFile_Click(object sender, EventArgs e)
        {
            Frm_AddFile frm = null;
            object name = (sender as KyoButton).Name;
            string key = string.Empty;
            if("btn_Plan_AddFile".Equals(name))
            {
                object objId = tab_Plan_Info.Tag;
                if(objId != null)
                {
                    if(dgv_Plan_FileList.SelectedRows.Count == 1)
                        frm = new Frm_AddFile(dgv_Plan_FileList, "plan_fl_", dgv_Plan_FileList.CurrentRow.Cells[0].Tag, trcId);
                    else
                        frm = new Frm_AddFile(dgv_Plan_FileList, "plan_fl_", null, trcId);
                    frm.txt_Unit.Text = UserHelper.GetUser().UnitName;
                    frm.parentId = objId;
                    frm.Show();
                }
                else
                    XtraMessageBox.Show("请先保存基本信息。", "温馨提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            else if("btn_Project_AddFile".Equals(name))
            {
                object objId = tab_Project_Info.Tag;
                if(objId != null)
                {
                    if(dgv_Project_FileList.SelectedRows.Count == 1)
                        frm = new Frm_AddFile(dgv_Project_FileList, "project_fl_", dgv_Project_FileList.CurrentRow.Cells[0].Tag, trcId);
                    else
                        frm = new Frm_AddFile(dgv_Project_FileList, "project_fl_", null, trcId);
                    frm.txt_Unit.Text = UserHelper.GetUser().UnitName;
                    frm.parentId = objId;
                    frm.Show();
                }
                else
                    XtraMessageBox.Show("请先保存基本信息。", "温馨提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            else if("btn_Topic_AddFile".Equals(name))
            {
                object objId = tab_Topic_Info.Tag;
                if(objId != null)
                {
                    if(dgv_Topic_FileList.SelectedRows.Count == 1)
                        frm = new Frm_AddFile(dgv_Topic_FileList, "topic_fl_", dgv_Topic_FileList.CurrentRow.Cells[0].Tag, trcId);
                    else
                        frm = new Frm_AddFile(dgv_Topic_FileList, "topic_fl_", null, trcId);
                    frm.txt_Unit.Text = UserHelper.GetUser().UnitName;
                    frm.parentId = objId;
                    frm.Show();
                }
                else
                    XtraMessageBox.Show("请先保存基本信息。", "温馨提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            else if("btn_Subject_AddFile".Equals(name))
            {
                object objId = tab_Subject_Info.Tag;
                if(objId != null)
                {
                    if(dgv_Subject_FileList.SelectedRows.Count == 1)
                        frm = new Frm_AddFile(dgv_Subject_FileList, "subject_fl_", dgv_Subject_FileList.CurrentRow.Cells[0].Tag, trcId);
                    else
                        frm = new Frm_AddFile(dgv_Subject_FileList, "subject_fl_", null, trcId);
                    frm.txt_Unit.Text = UserHelper.GetUser().UnitName;
                    frm.parentId = objId;
                    frm.Show();
                }
                else
                    XtraMessageBox.Show("请先保存基本信息。", "温馨提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            else if("btn_Imp_AddFile".Equals(name))
            {
                object objId = tab_Imp_Info.Tag;
                if(objId != null)
                {
                    if(dgv_Imp_FileList.SelectedRows.Count == 1)
                        frm = new Frm_AddFile(dgv_Imp_FileList, "imp_fl_", dgv_Imp_FileList.CurrentRow.Cells[0].Tag, trcId);
                    else
                        frm = new Frm_AddFile(dgv_Imp_FileList, "imp_fl_", null, trcId);
                    frm.parentId = objId;
                    frm.txt_Unit.Text = UserHelper.GetUser().UnitName;
                    frm.Show();
                }
                else
                    XtraMessageBox.Show("请先保存基本信息。", "温馨提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            else if("btn_Special_AddFile".Equals(name))
            {
                object objId = tab_Special_Info.Tag;
                if(objId != null)
                {
                    if(dgv_Special_FileList.SelectedRows.Count == 1)
                        frm = new Frm_AddFile(dgv_Special_FileList, "special_fl_", dgv_Special_FileList.CurrentRow.Cells[0].Tag, trcId);
                    else
                        frm = new Frm_AddFile(dgv_Special_FileList, "special_fl_", null, trcId);
                    frm.txt_Unit.Text = UserHelper.GetUser().UnitName;
                    frm.parentId = objId;
                    frm.Show();
                }
                else
                    XtraMessageBox.Show("请先保存基本信息。", "温馨提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }
    
        /// <summary>
        /// 上下移动
        /// </summary>
        private void Btn_TB_Click(object sender, EventArgs e)
        {
            string nameValue = (sender as KyoButton).Name;
            //计划
            //计划
            if(nameValue.Contains("Plan"))
            {
                if(nameValue.Equals("btn_Plan_Box_Top"))
                    MoveListViewItem(lsv_JH_File2, true);
                else if(nameValue.Equals("btn_Plan_Box_Bottom"))
                    MoveListViewItem(lsv_JH_File2, false);
                SaveListSort(lsv_JH_File2, cbo_Plan_Box.SelectedValue);
            }
            //计划-项目
            if(nameValue.Contains("Project"))
            {
                if(nameValue.Equals("btn_Project_Box_Top"))
                    MoveListViewItem(lsv_JH_XM_File2, true);
                else if(nameValue.Equals("btn_Project_Box_Bottom"))
                    MoveListViewItem(lsv_JH_XM_File2, false);
                SaveListSort(lsv_JH_XM_File2, cbo_Project_Box.SelectedValue);
            }
            //计划-项目-课题-子课题
            if(nameValue.Contains("Subject"))
            {
                if(nameValue.Equals("btn_Subject_Box_Top"))
                    MoveListViewItem(lsv_JH_XM_KT_ZKT_File2, true);
                else if(nameValue.Equals("btn_Subject_Box_Bottom"))
                    MoveListViewItem(lsv_JH_XM_KT_ZKT_File2, false);
                SaveListSort(lsv_JH_XM_KT_ZKT_File2, cbo_Subject_Box.SelectedValue);
            }
            //计划-课题
            if(nameValue.Contains("Topic"))
            {
                if(nameValue.Equals("btn_Topic_Box_Top"))
                    MoveListViewItem(lsv_JH_KT_File2, true);
                else if(nameValue.Equals("btn_Topic_Box_Bottom"))
                    MoveListViewItem(lsv_JH_KT_File2, false);
                SaveListSort(lsv_JH_KT_File2, cbo_Topic_Box.SelectedValue);
            }
            //重大专项
            if(nameValue.Contains("Imp"))
            {
                if(nameValue.Equals("btn_Imp_Box_Top"))
                    MoveListViewItem(lsv_Imp_File2, true);
                else if(nameValue.Equals("btn_Imp_Box_Bottom"))
                    MoveListViewItem(lsv_Imp_File2, false);
                SaveListSort(lsv_Imp_File2, cbo_Imp_Box.SelectedValue);
            }
            //重大专项-信息
            if(nameValue.Contains("Special"))
            {
                if(nameValue.Equals("btn_Special_Box_Top"))
                    MoveListViewItem(lsv_Imp_Dev_File2, true);
                else if(nameValue.Equals("btn_Special_Box_Bottom"))
                    MoveListViewItem(lsv_Imp_Dev_File2, false);
                SaveListSort(lsv_Imp_Dev_File2, cbo_Special_Box.SelectedValue);
            }
        }
     
        /// <summary>
        /// 保存新的顺序
        /// </summary>
        private void SaveListSort(ListView listView, object pbid)
        {
            StringBuilder builder = new StringBuilder();
            for(int i = 0; i < listView.Items.Count; i++)
            {
                string fileId = listView.Items[i].SubItems[0].Text;
                builder.Append($"UPDATE processing_file_list SET pfl_box_sort='{i}' WHERE pfl_id='{fileId}';");
            }
            if(builder.Length > 0)
                SqlHelper.ExecuteNonQuery(builder.ToString());
        }
    
        /// <summary>
        /// 上下移动已归档列表中的项
        /// </summary>
        /// <param name="view">表单控件</param>
        /// <param name="isTop">方向:true上;false下</param>
        private void MoveListViewItem(ListView view, bool isTop)
        {
            view.BeginUpdate();
            if(isTop)
            {
                foreach(ListViewItem item in view.SelectedItems)
                {
                    int index = item.Index;
                    if(index > 0)
                    {
                        view.Items.RemoveAt(index);
                        view.Items.Insert(index - 1, item);
                    }
                }
            }
            else
            {
                int size = view.Items.Count - 1;
                for(int i = size; i >= 0; i--)
                {
                    ListViewItem item = view.Items[i];
                    if(item.Selected)
                    {
                        int index = item.Index;
                        if(index < size)
                        {
                            view.Items.RemoveAt(index);
                            view.Items.Insert(index + 1, item);
                        }
                    }
                }
            }
            view.EndUpdate();
        }
    
        /// <summary>
        /// 设置来源单位Code
        /// </summary>
        public void SetUnitSourceId(object Code)
        {
            if(Code != null)
                treeView.Tag = Code;
        }
   
        /// <summary>
        /// 质检意见
        /// </summary>
        private void Btn_QTReason_Click(object sender, EventArgs e)
        {
            string name = (sender as Control).Name;
            object objId = null;
            string objName = null;
            int type = -1;
            if(name.Contains("Imp"))
            {
                objId = tab_Imp_Info.Tag;
                objName = lbl_Imp_Name.Text;
                type = tab_Imp_Info.SelectedTabPageIndex;
            }
            else if(name.Contains("Plan"))
            {
                objId = tab_Plan_Info.Tag;
                objName = lbl_Plan_Name.Text;
                type = tab_Plan_Info.SelectedTabPageIndex;
            }
            else if(name.Contains("Special"))
            {
                objId = tab_Special_Info.Tag;
                objName = txt_Special_Name.Text;
                type = tab_Special_Info.SelectedTabPageIndex;
            }
            else if(name.Contains("Project"))
            {
                objId = tab_Project_Info.Tag;
                objName = txt_Project_Name.Text;
                type = tab_Project_Info.SelectedTabPageIndex;
            }
            else if(name.Contains("Topic"))
            {
                objId = tab_Topic_Info.Tag;
                objName = txt_Topic_Name.Text;
                type = tab_Topic_Info.SelectedTabPageIndex;
            }
            else if(name.Contains("Subject"))
            {
                objId = tab_Subject_Info.Tag;
                objName = txt_Subject_Name.Text;
                type = tab_Subject_Info.SelectedTabPageIndex;
            }
            if(objId != null && objName != null)
            {
                Form form = GetAdviceFrom(objId, objName, type, false);
                form.Show();
                form.Activate();
            }
        }

        private Form GetAdviceFrom(object objId, string objName, int type, bool isBackWork)
        {
            if(adviceFrom == null || adviceFrom.IsDisposed)
                adviceFrom = new Frm_Advice(objId, objName, type, isBackWork);
            return adviceFrom;
        }

        private void Dgv_FileList_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridView view = sender as DataGridView;
            if(e.Button == MouseButtons.Right && e.RowIndex != -1 && e.ColumnIndex != -1)
            {
                view.ClearSelection();
                view.CurrentCell = view.Rows[e.RowIndex].Cells[e.ColumnIndex];
                contextMenuStrip1.Tag = view;
                contextMenuStrip1.Show(MousePosition);
            }
            else if(e.Button == MouseButtons.Left)
            {
                if(view.CurrentRow != null && view.RowCount > 1 && e.RowIndex != view.RowCount - 1)
                {
                    string name = view.Name;
                    if(name.Contains("Plan"))
                    {
                        lbl_Plan_Tip.Text = $"共{view.RowCount - 1}份文件，当前选中{e.RowIndex + 1}行";
                    }
                    else if(name.Contains("Project"))
                    {
                        lbl_Project_Tip.Text = $"共{view.RowCount - 1}份文件，当前选中{e.RowIndex + 1}行";
                    }
                    else if(name.Contains("Subject"))
                    {
                        lbl_Subject_Tip.Text = $"共{view.RowCount - 1}份文件，当前选中{e.RowIndex + 1}行";
                    }
                    else if(name.Contains("Topic"))
                    {
                        lbl_Topic_Tip.Text = $"共{view.RowCount - 1}份文件，当前选中{e.RowIndex + 1}行";
                    }
                    else if(name.Contains("Imp"))
                    {
                        lbl_Imp_Tip.Text = $"共{view.RowCount - 1}份文件，当前选中{e.RowIndex + 1}行";
                    }
                    else if(name.Contains("Special"))
                    {
                        lbl_Special_Tip.Text = $"共{view.RowCount - 1}份文件，当前选中{e.RowIndex + 1}行";
                    }
                }
            }
        }

        private void Tsm_InsertRow_Click(object sender, EventArgs e)
        {
            DataGridView view = (DataGridView)(sender as ToolStripItem).GetCurrentParent().Tag;
            view.Rows.Insert(view.CurrentCell.RowIndex, 1);
        }

        private void Tsm_DeleteRow(object sender, EventArgs e)
        {
            DataGridView view = (DataGridView)(sender as ToolStripItem).GetCurrentParent().Tag;
            int index = view.CurrentCell.RowIndex;
            if(index != view.RowCount - 1)
            {
                removeIdList.Add(view.Rows[index].Cells[0].Tag);
                view.Rows.RemoveAt(index);
            }
        }

        private void Tsm_Refresh(object sender, EventArgs e)
        {
            DataGridView view = (DataGridView)(sender as ToolStripItem).GetCurrentParent().Tag;
            object objId = view.Parent.Parent.Tag;
            string name = view.Parent.Parent.Name;
            string key = null;
            if(name.Contains("Plan"))
                key = "plan_fl_";
            else if(name.Contains("Project"))
                key = "project_fl_";
            else if(name.Contains("Topic"))
                key = "topic_fl_";
            else if(name.Contains("Subject"))
                key = "subject_fl_";
            else if(name.Contains("Imp"))
                key = "imp_fl_";
            else if(name.Contains("Special"))
                key = "special_fl_";
            if(key != null)
                LoadFileList(view, key, objId);

            removeIdList.Clear();
        }

        private void Tab_FileInfo_SelectedIndexChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            string name = (sender as Control).Name;
            if(name.Contains("Plan"))
            {
                int index = tab_Plan_Info.SelectedTabPageIndex;
                object objid = tab_Plan_Info.Tag;
                lbl_Plan_Tip.Visible = false;
                if(objid != null)
                {
                    if(index == 0)
                        lbl_Plan_Tip.Visible = true;
                    else if(index == 1)
                        LoadFileValidList(dgv_Plan_FileValid, objid, "plan_fc_");
                    else if(index == 2)
                        LoadDocList(objid, ControlType.Project);
                }
            }
            else if(name.Contains("Imp"))
            {
                int index = tab_Imp_Info.SelectedTabPageIndex;
                object objid = tab_Imp_Info.Tag;
                lbl_Imp_Tip.Visible = false;
                if(objid != null)
                {
                    if(index == 0)
                        lbl_Imp_Tip.Visible = true;
                    if(index == 1)
                        LoadFileValidList(dgv_Imp_FileValid, objid, "imp_fc_");
                    else if(index == 2)
                        LoadDocList(objid, ControlType.Imp);
                }
            }
            else if(name.Contains("Special"))
            {
                int index = tab_Special_Info.SelectedTabPageIndex;
                object objid = tab_Special_Info.Tag;
                lbl_Special_Tip.Visible = false;
                if(objid != null)
                {
                    if(index == 0)
                        lbl_Special_Tip.Visible = true;
                    if(index == 1)
                        LoadFileValidList(dgv_Special_FileValid, objid, "special_fc_");
                    else if(index == 2)
                        LoadDocList(objid, ControlType.Special);
                }
            }
            else if(name.Contains("Project"))
            {
                int index = tab_Project_Info.SelectedTabPageIndex;
                object objid = tab_Project_Info.Tag;
                lbl_Project_Tip.Visible = false;
                if(objid != null)
                {
                    if(index == 0)
                        lbl_Project_Tip.Visible = true;
                    if(index == 1)
                        LoadFileValidList(dgv_Project_FileValid, objid, "project_fc_");
                    else if(index == 2)
                        LoadDocList(objid, ControlType.Project);
                }
            }
            else if(name.Contains("Topic"))
            {
                int index = tab_Topic_Info.SelectedTabPageIndex;
                object objid = tab_Topic_Info.Tag;
                lbl_Topic_Tip.Visible = false;
                if(objid != null)
                {
                    if(index == 0)
                        lbl_Topic_Tip.Visible = true;
                    if(index == 1)
                        LoadFileValidList(dgv_Topic_FileValid, objid, "topic_fc_");
                    else if(index == 2)
                        LoadDocList(objid, ControlType.Topic);
                }
            }
            else if(name.Contains("Subject"))
            {
                int index = tab_Subject_Info.SelectedTabPageIndex;
                object objId = tab_Subject_Info.Tag;
                lbl_Subject_Tip.Visible = false;
                if(objId != null)
                {
                    if(index == 0)
                        lbl_Subject_Tip.Visible = true;
                    if(index == 1)
                        LoadFileValidList(dgv_Subject_FileValid, objId, "subject_fc_");
                    else if(index == 2)
                        LoadDocList(objId, ControlType.Subject);
                }
            }
        }

        private void LoadDocList(object objid, ControlType type)
        {
            if(type == ControlType.Plan)
            {
                //txt_Plan_AJ_Code.Text = lbl_JH_Name.Text;
                txt_Plan_AJ_Name.Text = lbl_Plan_Name.Text;
            }
            else if(type == ControlType.Imp)
            {
                //txt_Imp_AJ_Code.Text = txt_Imp_Code.Text;
                txt_Imp_AJ_Name.Text = lbl_Imp_Name.Text;
            }
            else if(type == ControlType.Special)
            {
                txt_Special_AJ_Code.Text = txt_Special_Code.Text;
                txt_Special_AJ_Name.Text = txt_Special_Name.Text;
            }
            else if(type == ControlType.Project)
            {
                txt_Project_AJ_Code.Text = txt_Project_Code.Text;
                txt_Project_AJ_Name.Text = txt_Project_Name.Text;
            }
            else if(type == ControlType.Topic)
            {
                txt_Topic_AJ_Code.Text = txt_Topic_Code.Text;
                txt_Topic_AJ_Name.Text = txt_Topic_Name.Text;
            }
            else if(type == ControlType.Subject)
            {
                txt_Subject_AJ_Code.Text = txt_Subject_Code.Text;
                txt_Subject_AJ_Name.Text = txt_Subject_Name.Text;
            }
            LoadBoxList(objid, type);
        }

        private void FileList_UserDeletedRow(object sender, DataGridViewRowEventArgs e) => removeIdList.Add(e.Row.Cells[0].Tag);

        private void FileList_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridView view = sender as DataGridView;
            object id = view.Rows[e.RowIndex].Cells[0].Tag;
            if(view.Name.Contains("Plan"))
                Btn_AddFile_Click(btn_Plan_AddFile, e);
            else if(view.Name.Contains("Project"))
                Btn_AddFile_Click(btn_Project_AddFile, e);
            else if(view.Name.Contains("Topic"))
                Btn_AddFile_Click(btn_Topic_AddFile, e);
            else if(view.Name.Contains("Subject"))
                Btn_AddFile_Click(btn_Subject_AddFile, e);
            else if(view.Name.Contains("Imp"))
                Btn_AddFile_Click(btn_Imp_AddFile, e);
            else if(view.Name.Contains("Special"))
                Btn_AddFile_Click(btn_Special_AddFile, e);
        }

        private void StartTime_ValueChanged(object sender, EventArgs e)
        {
            DateTimePicker picker = sender as DateTimePicker;
            if(picker.Name.Contains("Project"))
                txt_Project_StartTime.Text = picker.Value.ToString("yyyy-MM-dd");
            else if(picker.Name.Contains("Topic"))
                txt_Topic_StartTime.Text = picker.Value.ToString("yyyy-MM-dd");
            else if(picker.Name.Contains("Subject"))
                txt_Subject_StartTime.Text = picker.Value.ToString("yyyy-MM-dd");
        }

        private void EndTime_ValueChanged(object sender, EventArgs e)
        {
            DateTimePicker picker = sender as DateTimePicker;
            if(picker.Name.Contains("Project"))
                txt_Project_EndTime.Text = picker.Value.ToString("yyyy-MM-dd");
            else if(picker.Name.Contains("Topic"))
                txt_Topic_EndTime.Text = picker.Value.ToString("yyyy-MM-dd");
            else if(picker.Name.Contains("Subject"))
                txt_Subject_EndTime.Text = picker.Value.ToString("yyyy-MM-dd");
        }

        private void BtnGroup_EnabledChanged(object sender, EventArgs e)
        {
            Panel panel = sender as Panel;
            if(!isReadOnly)
            {
                if(panel.Name.Contains("Plan"))
                {
                    dgv_Plan_FileList.AllowUserToAddRows = panel.Enabled;
                    lbl_Plan_Box_Add.Enabled = panel.Enabled;
                    lbl_Plan_Box_Remove.Enabled = panel.Enabled;
                    foreach(Control item in tp_Plan_Box.Controls)
                        if(item is KyoButton || item is LinkLabel)
                            if(!item.Name.Contains("Print"))
                                item.Enabled = panel.Enabled;
                    if(!panel.Enabled)
                        dgv_Plan_FileList.RowHeaderMouseDoubleClick -= FileList_RowHeaderMouseDoubleClick;
                    else
                        dgv_Plan_FileList.RowHeaderMouseDoubleClick += FileList_RowHeaderMouseDoubleClick;
                }
                else if(panel.Name.Contains("Project"))
                {
                    dgv_Project_FileList.AllowUserToAddRows = panel.Enabled;
                    lbl_Project_Box_Add.Enabled = panel.Enabled;
                    lbl_Project_Box_Remove.Enabled = panel.Enabled;
                    btn_Project_OtherDoc.Enabled = panel.Enabled;
                    foreach(Control item in tp_Project_Box.Controls)
                        if(item is KyoButton || item is LinkLabel)
                            if(!item.Name.Contains("Print"))
                                item.Enabled = panel.Enabled;
                    //可以继承别人做的项目
                    cbo_Project_HasNext.Enabled = panel.Enabled;
                    if(!panel.Enabled)
                        dgv_Project_FileList.RowHeaderMouseDoubleClick -= FileList_RowHeaderMouseDoubleClick;
                    else
                        dgv_Project_FileList.RowHeaderMouseDoubleClick += FileList_RowHeaderMouseDoubleClick;
                }
                else if(panel.Name.Contains("Topic"))
                {
                    dgv_Topic_FileList.AllowUserToAddRows = panel.Enabled;
                    lbl_Topic_Box_Add.Enabled = panel.Enabled;
                    lbl_Topic_Box_Remove.Enabled = panel.Enabled;
                    btn_Topic_OtherDoc.Enabled = panel.Enabled;
                    foreach(Control item in tp_Topic_Box.Controls)
                        if(item is KyoButton || item is LinkLabel)
                            if(!item.Name.Contains("Print"))
                                item.Enabled = panel.Enabled;
                    cbo_Topic_HasNext.Enabled = panel.Enabled;
                    if(!panel.Enabled)
                        dgv_Topic_FileList.RowHeaderMouseDoubleClick -= FileList_RowHeaderMouseDoubleClick;
                    else
                        dgv_Topic_FileList.RowHeaderMouseDoubleClick += FileList_RowHeaderMouseDoubleClick;
                }
                else if(panel.Name.Contains("Subject"))
                {
                    dgv_Subject_FileList.AllowUserToAddRows = panel.Enabled;
                    lbl_Subject_Box_Add.Enabled = panel.Enabled;
                    lbl_Subject_Box_Remove.Enabled = panel.Enabled;
                    btn_Subject_OtherDoc.Enabled = panel.Enabled;
                    foreach(Control item in tp_Subject_Box.Controls)
                        if(item is KyoButton || item is LinkLabel)
                            if(!item.Name.Contains("Print"))
                                item.Enabled = panel.Enabled;
                    if(!panel.Enabled)
                        dgv_Subject_FileList.RowHeaderMouseDoubleClick -= FileList_RowHeaderMouseDoubleClick;
                    else
                        dgv_Subject_FileList.RowHeaderMouseDoubleClick += FileList_RowHeaderMouseDoubleClick;
                }
                else if(panel.Name.Contains("Imp"))
                {
                    dgv_Imp_FileList.AllowUserToAddRows = panel.Enabled;
                    lbl_Imp_Box_Add.Enabled = panel.Enabled;
                    lbl_Imp_Box_Remove.Enabled = panel.Enabled;
                    foreach(Control item in tp_Imp_Box.Controls)
                        if(item is KyoButton || item is LinkLabel)
                            if(!item.Name.Contains("Print"))
                                item.Enabled = panel.Enabled;
                    cbo_Imp_HasNext.Enabled = panel.Enabled;
                    if(!panel.Enabled)
                        dgv_Imp_FileList.RowHeaderMouseDoubleClick -= FileList_RowHeaderMouseDoubleClick;
                    else
                        dgv_Imp_FileList.RowHeaderMouseDoubleClick += FileList_RowHeaderMouseDoubleClick;
                }
                else if(panel.Name.Contains("Special"))
                {
                    dgv_Special_FileList.AllowUserToAddRows = panel.Enabled;
                    lbl_Special_Box_Add.Enabled = panel.Enabled;
                    lbl_Special_Box_Remove.Enabled = panel.Enabled;
                    btn_Special_OtherDoc.Enabled = panel.Enabled;
                    foreach(Control item in tp_Special_Box.Controls)
                        if(item is KyoButton || item is LinkLabel)
                            if(!item.Name.Contains("Print"))
                                item.Enabled = panel.Enabled;
                    if(!panel.Enabled)
                        dgv_Special_FileList.RowHeaderMouseDoubleClick -= FileList_RowHeaderMouseDoubleClick;
                    else
                        dgv_Special_FileList.RowHeaderMouseDoubleClick += FileList_RowHeaderMouseDoubleClick;
                }
            }
            else
                panel.Enabled = false;
        }
        private void Btn_Print_Click(object sender, EventArgs e)
        {
            string controlName = (sender as KyoButton).Name;
            object objId = null, boxId = null, docNumber = null;
            string objName = null, gcCode = null;
            DataTable boxTable = null;
            string proName = null, proCode = null;
            object parentObjectName = null;
            int boxNumber = 0;
            if(controlName.Contains("Project"))
            {
                objId = tab_Project_Info.Tag;
                boxId = cbo_Project_Box.SelectedValue;
                docNumber = txt_Project_AJ_Code.Text;
                objName = txt_Project_AJ_Name.Text;
                gcCode = txt_Project_Box_GCID.Text;
                proName = txt_Project_Name.Text;
                proCode = txt_Project_Code.Text;
                boxTable = (DataTable)cbo_Project_Box.DataSource;
                boxNumber = cbo_Project_Box.SelectedIndex + 1;
            }
            else if(controlName.Contains("Topic"))
            {
                objId = tab_Topic_Info.Tag;
                boxId = cbo_Topic_Box.SelectedValue;
                docNumber = txt_Topic_AJ_Code.Text;
                objName = txt_Topic_AJ_Name.Text;
                gcCode = txt_Topic_Box_GCID.Text;
                proName = txt_Topic_Name.Text;
                proCode = txt_Topic_Code.Text;
                boxTable = (DataTable)cbo_Topic_Box.DataSource;
                parentObjectName = SqlHelper.ExecuteOnlyOneQuery($"SELECT pi_name FROM project_info WHERE pi_id=(SELECT ti_obj_id FROM topic_info WHERE ti_id='{objId}')");
                boxNumber = cbo_Topic_Box.SelectedIndex + 1;
            }
            else if(controlName.Contains("Subject"))
            {
                objId = tab_Subject_Info.Tag;
                boxId = cbo_Subject_Box.SelectedValue;
                docNumber = txt_Subject_AJ_Code.Text;
                objName = txt_Subject_AJ_Name.Text;
                gcCode = txt_Subject_Box_GCID.Text;
                proName = txt_Subject_Name.Text;
                proCode = txt_Subject_Code.Text;
                boxTable = (DataTable)cbo_Subject_Box.DataSource;
                parentObjectName = SqlHelper.ExecuteOnlyOneQuery($"SELECT ti_name FROM topic_info WHERE ti_id=(SELECT si_obj_id FROM subject_info WHERE si_id='{objId}')");
                boxNumber = cbo_Subject_Box.SelectedIndex + 1;
            }
            else if(controlName.Contains("Special"))
            {
                objId = tab_Special_Info.Tag;
                boxId = cbo_Special_Box.SelectedValue;
                docNumber = txt_Special_AJ_Code.Text;
                objName = txt_Special_AJ_Name.Text;
                gcCode = txt_Special_Box_GCID.Text;
                proName = txt_Special_Name.Text;
                proCode = txt_Special_Code.Text;
                boxTable = (DataTable)cbo_Special_Box.DataSource;
                boxNumber = cbo_Special_Box.SelectedIndex + 1;
            }
            else if(controlName.Contains("Imp"))
            {
                objId = tab_Imp_Info.Tag;
                boxId = cbo_Imp_Box.SelectedValue;
                docNumber = txt_Imp_AJ_Code.Text;
                objName = txt_Imp_AJ_Name.Text;
                gcCode = txt_Imp_Box_GCID.Text;
                proName = lbl_Imp_Name.Text;
                proCode = ToolHelper.GetValue(docNumber);
                boxTable = (DataTable)cbo_Imp_Box.DataSource;
                boxNumber = cbo_Imp_Box.SelectedIndex + 1;
            }
            else if(controlName.Contains("Plan"))
            {
                objId = tab_Plan_Info.Tag;
                boxId = cbo_Plan_Box.SelectedValue;
                docNumber = txt_Plan_AJ_Code.Text;
                objName = txt_Plan_AJ_Name.Text;
                gcCode = txt_Plan_Box_GCID.Text;
                proName = lbl_Plan_Name.Text;
                proCode = ToolHelper.GetValue(docNumber);
                boxTable = (DataTable)cbo_Plan_Box.DataSource;
                boxNumber = cbo_Plan_Box.SelectedIndex + 1;
            }
            object _fileAmount = SqlHelper.ExecuteOnlyOneQuery($"SELECT pb_files_id FROM processing_box WHERE pb_id='{boxId}'");
            string[] _files = ToolHelper.GetValue(_fileAmount).Split(',');
            int fileAmount = 0;
            int filePages = 0;
            for(int i = 0; i < _files.Length; i++)
            {
                if(!string.IsNullOrEmpty(_files[i]))
                {
                    fileAmount++;
                    object _page = SqlHelper.ExecuteOnlyOneQuery($"SELECT pfl_pages FROM processing_file_list WHERE pfl_id='{_files[i]}'");
                    if(!string.IsNullOrEmpty(ToolHelper.GetValue(_page)))
                        filePages += Convert.ToInt32(_page);
                }
            }

            Frm_PrintBox frm = new Frm_PrintBox(this)
            {
                boxTable = boxTable,
                objectCode = docNumber,
                objectName = objName,
                unitName = UserHelper.GetUser().UnitName,
                proCode = proCode,
                proName = proName,
                parentObjectName = parentObjectName,
                ljPeople = UserHelper.GetUserNameById(GetWorker(objId, 1)),
                ljDate = GetWorker(objId, 2),
                jcPeople = UserHelper.GetUserNameById(GetWorker(objId, 3)),
                jcDate = GetWorker(objId, 4),
                otherDoc = SqlHelper.ExecuteQuery($"SELECT * FROM other_doc WHERE od_obj_id='{objId}' ORDER BY od_code"),
            };
            frm.ShowDialog();
        }

        /// <summary>
        /// 获取当前盒的编制日期（当前盒内文件的最早至最晚形成日期）
        /// </summary>
        private string GetBzDate(object boxId)
        {
            object fileIds = SqlHelper.ExecuteOnlyOneQuery($"SELECT pb_files_id FROM processing_box WHERE pb_id='{boxId}'");
            if(!string.IsNullOrEmpty(ToolHelper.GetValue(fileIds)))
            {
                string[] ids = ToolHelper.GetValue(fileIds).Split(',');
                string idsString = string.Empty;
                foreach(string id in ids)
                    if(!string.IsNullOrEmpty(id))
                        idsString += $"'{id}',";
                if(!string.IsNullOrEmpty(idsString))
                {
                    idsString = idsString.Substring(0, idsString.Length - 1);
                    object minDateObject = SqlHelper.ExecuteOnlyOneQuery($"SELECT MIN(pfl_date) FROM processing_file_list where pfl_id IN ({idsString}) AND CONVERT(DATE, pfl_date) <> '1900-01-01' AND CONVERT(DATE, pfl_date) <> '0001-01-01';");
                    object maxDateObject = SqlHelper.ExecuteOnlyOneQuery($"SELECT MAX(pfl_date) FROM processing_file_list where pfl_id IN ({idsString}) AND CONVERT(DATE, pfl_date) <> '1900-01-01' AND CONVERT(DATE, pfl_date) <> '0001-01-01';");
                    if(minDateObject != null && maxDateObject != null)
                    {
                        DateTime minDate = Convert.ToDateTime(minDateObject);
                        DateTime maxDate = Convert.ToDateTime(maxDateObject);
                        return $"{minDate.ToString("yyyy-MM-dd")} ~ {maxDate.ToString("yyyy-MM-dd")}";
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// 根据指定ID获取对应类型的数据
        /// </summary>
        /// <param name="objId">指定对象的ID</param>
        /// <param name="type">
        /// <para>1:著录人</para>
        /// <para>2:著录日期</para>
        /// <para>3:检查人</para>
        /// <para>4:检查日期</para>
        /// </param>
        private string GetWorker(object objId, int type)
        {
            string key = type == 1 ? "worker_id" : type == 2 ? "worker_date" : type == 3 ? "checker_id" : "checker_date";
            object result = SqlHelper.ExecuteOnlyOneQuery($"SELECT pi_{key} FROM project_info WHERE pi_id='{objId}'") ??
                SqlHelper.ExecuteOnlyOneQuery($"SELECT ti_{key} FROM topic_info WHERE ti_id='{objId}'") ??
                SqlHelper.ExecuteOnlyOneQuery($"SELECT si_{key} FROM subject_info WHERE si_id='{objId}'");
            return ToolHelper.GetValue(result);
        }

        private void Btn_OtherDoc_Click(object sender, EventArgs e)
        {
            string name = (sender as KyoButton).Name;
            object objid = null;
            if(name.Contains("Project"))
                objid = tab_Project_Info.Tag;
            else if(name.Contains("Topic"))
                objid = tab_Topic_Info.Tag;
            else if(name.Contains("Subject"))
                objid = tab_Subject_Info.Tag;
            else if(name.Contains("Special"))
                objid = tab_Special_Info.Tag;
            if(objid != null)
            {
                Frm_OtherDoc frm = new Frm_OtherDoc(objid);
                frm.ShowDialog();
            }
            else
                XtraMessageBox.Show("请先保存基本信息。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }

        private void toggleSwitch1_Toggled(object sender, EventArgs e)
        {
            treeView.Width = toggleSwitch1.IsOn ? 250 : 0;
        }

        public Action<WorkType, object, object, object> BackCallMethod;

        private void Frm_MyWorkQT_FormClosed(object sender, FormClosedEventArgs e)
        {
            BackCallMethod?.Invoke(workType, objId, wmid, objId);
        }
    }

}
