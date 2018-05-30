using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using 科技计划项目档案数据采集管理系统.KyoControl;

namespace 科技计划项目档案数据采集管理系统
{
    public partial class Frm_MyWork : DevExpress.XtraEditors.XtraForm
    {
        /// <summary>
        /// 待删除文件ID
        /// </summary>
        List<object> removeIdList = new List<object>();
    
        /// <summary>
        /// 当前加工类型
        /// </summary>
        private WorkType workType;
    
        /// <summary>
        /// 所属对象主键
        /// </summary>
        private object OBJECT_ID;
        private object PLAN_ID;
        public object planCode;
        public int DEV_TYPE = -1;
        public object unitCode;
    
        /// <summary>
        /// 是否是返工后再次编辑
        /// </summary>
        public bool isBacked;
   
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
        public Frm_MyWork(WorkType workType, object planId, object trpId, ControlType controlType, bool isBacked)
        {
            InitializeComponent();
            this.isBacked = isBacked;
            OBJECT_ID = trpId;
            PLAN_ID = planId;
            this.workType = workType;
            this.controlType = controlType;
            if(workType == WorkType.Default && DEV_TYPE == -1)
            {
                object _type = SqlHelper.ExecuteOnlyOneQuery($"SELECT imp_type FROM imp_info WHERE imp_id='{planId}'");
                if(!string.IsNullOrEmpty(GetValue(_type)))
                    DEV_TYPE = Convert.ToInt32(_type);
            }
            if(isBacked)
            {
                Text += "[返工]";
                btn_JH_QTReason.Visible = isBacked;
                btn_Imp_QTReason.Visible = isBacked;
                btn_Imp_Dev_QTReason.Visible = isBacked;
                btn_JH_XM_QTReason.Visible = isBacked;
                btn_JH_XM_KT_ZKT_QTReason.Visible = isBacked;
            }
            InitialForm(planId, controlType);
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
        /// <param name="planId">计划主键</param>
        /// <param name="color">节点背景色</param>
        private void LoadPlanPage(object planId, System.Drawing.Color color)
        {
            object[] _obj = SqlHelper.ExecuteRowsQuery($"SELECT pi_id, pi_name, pi_obj_id, pi_submit_status FROM project_info WHERE pi_id = '{planId}'");
            if(_obj == null)
                _obj = SqlHelper.ExecuteRowsQuery($"SELECT pi_id, pi_name, pi_obj_id, pi_submit_status FROM project_info WHERE pi_obj_id = '{OBJECT_ID}'");
            if(_obj == null)
            {
                _obj = SqlHelper.ExecuteRowsQuery($"SELECT dd_id, dd_name, dd_note FROM data_dictionary WHERE dd_id='{planId}'");
                if(_obj == null)
                    planId = SqlHelper.ExecuteOnlyOneQuery($"SELECT imp_id FROM imp_info WHERE imp_obj_id='{planId}'");
                if(_obj != null)
                {
                    lbl_JH_Name.Tag = GetValue(_obj[0]);
                    lbl_JH_Name.Text = GetValue(_obj[1]);
                    txt_Project_Intro.Text = GetValue(_obj[2]);
                }
            }
            else
            {
                dgv_Plan_FileList.Tag = GetValue(_obj[0]);
                lbl_JH_Name.Text = GetValue(_obj[1]);
                LoadFileList(dgv_Plan_FileList, string.Empty, GetValue(_obj[0]));

                if(!string.IsNullOrEmpty(GetValue(_obj[3])))
                {
                    ObjectSubmitStatus status = (ObjectSubmitStatus)_obj[3];
                    EnableControls(ControlType.Plan, status != ObjectSubmitStatus.SubmitSuccess);
                }
            }
            dgv_Plan_FileList.ColumnHeadersDefaultCellStyle = DataGridViewStyleHelper.GetHeaderStyle();
            dgv_Plan_FileList.DefaultCellStyle = DataGridViewStyleHelper.GetCellStyle();
            dgv_JH_FileValid.ColumnHeadersDefaultCellStyle = DataGridViewStyleHelper.GetHeaderStyle();
            dgv_JH_FileValid.DefaultCellStyle = DataGridViewStyleHelper.GetCellStyle();

            if(color == DisEnbleColor) {
                pal_JH_BtnGroup.Enabled = false;
                cbo_JH_HasNext.Enabled = false;
            }

            tab_MenuList.TabPages["plan"].Tag = planId;
            if(isBacked)
            {
                cbo_JH_HasNext.Enabled = !isBacked;
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
                dataGridView.Rows[index].Cells[key + "page"].Value = dataTable.Rows[i]["pfl_pages"];
                dataGridView.Rows[index].Cells[key + "count"].Value = dataTable.Rows[i]["pfl_amount"];
                object _date = dataTable.Rows[i]["pfl_date"];
                if(_date != null)
                    dataGridView.Rows[index].Cells[key + "date"].Value = Convert.ToDateTime(_date).ToString("yyyyMMdd");
                dataGridView.Rows[index].Cells[key + "unit"].Value = dataTable.Rows[i]["pfl_unit"];
                dataGridView.Rows[index].Cells[key + "carrier"].Value = dataTable.Rows[i]["pfl_carrier"];
                dataGridView.Rows[index].Cells[key + "link"].Value = dataTable.Rows[i]["pfl_link"];
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
            tab_MenuList.SelectedIndex = index;
        }
        
        /// <summary>
        /// 渲染表格样式，初始化表单字段
        /// </summary>
        private void Frm_MyWork_Load(object sender, EventArgs e)
        {
            tab_Plan_Info.Height = 400;
            tab_Project_Info.Height = 400;
            tab_Topic_Info.Height = 400;
            tab_Subject_Info.Height = 400;
            tab_Imp_Info.Height = 400;
            tab_Special_Info.Height = 400;

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
            InitialLostReasonList(dgv_JH_FileValid, "plan_fc_");
            InitialLostReasonList(dgv_JH_XM_FileValid, "project_fc_");
            InitialLostReasonList(dgv_JH_KT_FileValid, "topic_fc_");
            InitialLostReasonList(dgv_JH_XM_KT_ZKT_FileValid, "subject_fc_");
            InitialLostReasonList(dgv_Imp_FileValid, "imp_fc_");
            InitialLostReasonList(dgv_Special_FileValid, "special_fc_");

            cbo_JH_HasNext.SelectedIndex = 0;
            cbo_Project_HasNext.SelectedIndex = 0;
            cbo_Topic_HasNext.SelectedIndex = 0;
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
            carrierColumn.DefaultCellStyle = new DataGridViewCellStyle() { Font = new System.Drawing.Font("宋体", 10.5f) };
        }

        private void InitialSecretList(DataGridView dataGridView, string key)
        {
            DataGridViewComboBoxColumn secretColumn = dataGridView.Columns[key + "secret"] as DataGridViewComboBoxColumn;
            secretColumn.DataSource = DictionaryHelper.GetTableByCode("dic_file_mj");
            secretColumn.DisplayMember = "dd_name";
            secretColumn.ValueMember = "dd_id";
            secretColumn.DefaultCellStyle = new DataGridViewCellStyle() { Font = new System.Drawing.Font("宋体", 10.5f) };
        }

        private void InitialTypeList(DataGridView dataGridView, string key)
        {
            DataGridViewComboBoxColumn filetypeColumn = dataGridView.Columns[key + "type"] as DataGridViewComboBoxColumn;
            filetypeColumn.DataSource = DictionaryHelper.GetTableByCode("dic_file_type");
            filetypeColumn.DisplayMember = "dd_name";
            filetypeColumn.ValueMember = "dd_id";
            filetypeColumn.DefaultCellStyle = new DataGridViewCellStyle() { Font = new System.Drawing.Font("宋体", 10.5f) };
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
            comboBoxColumn.DefaultCellStyle = new DataGridViewCellStyle() { Font = new System.Drawing.Font("宋体", 10.5f) };
        }
   
        /// <summary>
        /// 根据阶段设置相应的文件类别
        /// </summary>
        /// <param name="jdId">阶段ID</param>
        public void SetCategorByStage(object jdId, DataGridViewRow dataGridViewRow, string key)
        {
            //文件类别
            DataGridViewComboBoxCell categorCell = dataGridViewRow.Cells[key + "categor"] as DataGridViewComboBoxCell;

            string querySql = $"SELECT dd_id, dd_name FROM data_dictionary WHERE dd_pId='{jdId}' ORDER BY dd_sort";
            categorCell.DataSource = SqlHelper.ExecuteQuery(querySql);
            categorCell.DisplayMember = "dd_name";
            categorCell.ValueMember = "dd_id";
            categorCell.Style = new DataGridViewCellStyle() { Font = new System.Drawing.Font("宋体", 10.5f) };
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
                    (con as ComboBox).SelectionChangeCommitted += new EventHandler(StageComboBox_SelectionChangeCommitted);
                else if("plan_fl_categor".Equals(columnName))
                    (con as ComboBox).SelectionChangeCommitted += new EventHandler(CategorComboBox_SelectionChangeCommitted);
            }
            else if("dgv_Project_FileList".Equals(dataGridView.Name))
            {
                string columnName = dgv_Project_FileList.CurrentCell.OwningColumn.Name;
                Control con = e.Control;
                con.Tag = ControlType.Plan;
                if("project_fl_stage".Equals(columnName))
                    (con as ComboBox).SelectionChangeCommitted += new EventHandler(StageComboBox_SelectionChangeCommitted);
                else if("project_fl_categor".Equals(columnName))
                    (con as ComboBox).SelectionChangeCommitted += new EventHandler(CategorComboBox_SelectionChangeCommitted);
            }
            else if("dgv_Topic_FileList".Equals(dataGridView.Name))
            {
                string columnName = dgv_Topic_FileList.CurrentCell.OwningColumn.Name;
                Control con = e.Control;
                con.Tag = ControlType.Topic;
                if("topic_fl_stage".Equals(columnName))
                    (con as ComboBox).SelectionChangeCommitted += new EventHandler(StageComboBox_SelectionChangeCommitted);
                else if("topic_fl_categor".Equals(columnName))
                    (con as ComboBox).SelectionChangeCommitted += new EventHandler(CategorComboBox_SelectionChangeCommitted);
            }
            else if("dgv_Subject_FileList".Equals(dataGridView.Name))
            {
                string columnName = dgv_Subject_FileList.CurrentCell.OwningColumn.Name;
                Control con = e.Control;
                con.Tag = ControlType.Subject;
                if("subject_fl_stage".Equals(columnName))
                    (con as ComboBox).SelectionChangeCommitted += new EventHandler(StageComboBox_SelectionChangeCommitted);
                else if("subject_fl_categor".Equals(columnName))
                    (con as ComboBox).SelectionChangeCommitted += new EventHandler(CategorComboBox_SelectionChangeCommitted);
            }
            else if("dgv_Imp_FileList".Equals(dataGridView.Name))
            {
                string columnName = dgv_Imp_FileList.CurrentCell.OwningColumn.Name;
                Control con = e.Control;
                con.Tag = ControlType.Imp;
                if("imp_fl_stage".Equals(columnName))
                    (con as ComboBox).SelectionChangeCommitted += new EventHandler(StageComboBox_SelectionChangeCommitted);
                else if("imp_fl_categor".Equals(columnName))
                    (con as ComboBox).SelectionChangeCommitted += new EventHandler(CategorComboBox_SelectionChangeCommitted);
            }
            else if("dgv_Special_FileList".Equals(dataGridView.Name))
            {
                string columnName = dgv_Special_FileList.CurrentCell.OwningColumn.Name;
                Control con = e.Control;
                con.Tag = ControlType.Special;
                if("special_fl_stage".Equals(columnName))
                    (con as ComboBox).SelectionChangeCommitted += new EventHandler(StageComboBox_SelectionChangeCommitted);
                else if("special_fl_categor".Equals(columnName))
                    (con as ComboBox).SelectionChangeCommitted += new EventHandler(CategorComboBox_SelectionChangeCommitted);
            }
            if(e.Control is ComboBox)
            {
                ComboBox box = e.Control as ComboBox;
                if(box.Items.Count > 0)
                    box.SelectedValue = box.Items[0];
            }
        }
  
        /// <summary>
        /// 文件阶段 下拉事件
        /// </summary>
        private void StageComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;
            if((ControlType)comboBox.Tag == ControlType.Plan)
                SetCategorByStage(comboBox.SelectedValue, dgv_Plan_FileList.CurrentRow, string.Empty);
            else if((ControlType)comboBox.Tag == ControlType.Plan)
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
            if((ControlType)comboBox.Tag == ControlType.Plan)
                SetNameByCategor(comboBox, dgv_Plan_FileList.CurrentRow, "plan_fl_", tab_Plan_Info.Tag);
            else if((ControlType)comboBox.Tag == ControlType.Plan)
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
                ComboBox _comboBox = obj as ComboBox;
                _comboBox.SelectionChangeCommitted -= new EventHandler(CategorComboBox_SelectionChangeCommitted);
            });
        }
 
        /// <summary>
        /// 根据文件类别设置文件名称
        /// </summary>
        /// <param name="catogerCode">文件类别编号</param>
        /// <param name="currentRow">当前行</param>
        private void SetNameByCategor(ComboBox comboBox, DataGridViewRow currentRow, string key, object objId)
        {
            string value = GetValue(SqlHelper.ExecuteOnlyOneQuery($"SELECT dd_note FROM data_dictionary WHERE dd_id='{comboBox.SelectedValue}'"));
            currentRow.Cells[key + "name"].Value = value;

            int amount = SqlHelper.ExecuteCountQuery($"SELECT COUNT(pfl_id) FROM processing_file_list WHERE pfl_categor='{comboBox.SelectedValue}' AND pfl_obj_id='{objId}'");

            currentRow.Cells[key + "categorname"].Value = null;
            if(comboBox.SelectedIndex == comboBox.Items.Count - 1)
            {
                currentRow.DataGridView.Columns[key + "categorname"].Visible = true;

                int _amount = comboBox.Items.Count;
                string tempKey = ((DataRowView)comboBox.Items[0]).Row.ItemArray[1].ToString();
                string _key = GetValue(tempKey).Substring(0, 1) + _amount.ToString().PadLeft(2, '0');
                currentRow.Cells[key + "code"].Value = _key + "-" + (amount + 1).ToString().PadLeft(2, '0');
            }
            else
            {
                string _key = comboBox.Text.Split(' ')[0];
                currentRow.Cells[key + "code"].Value = _key + "-" + (amount + 1).ToString().PadLeft(2, '0');
            }
        }

        private void Dgv_File_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
           
        }

        private void Cbo_JH_Next_SelectionChangeCommitted(object sender, EventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;
            if(comboBox.Name.Contains("Special"))
            {
                object id = dgv_Special_FileList.Tag;
                if(id == null)
                {
                    MessageBox.Show("尚未保存当前项目，无法添加新数据。", "温馨提示");
                    cbo_Special_HasNext.SelectedIndex = 0;
                }
                else
                {
                    int _index = tab_MenuList.SelectedIndex;
                    int index = comboBox.SelectedIndex;
                    if(index == 0)//无
                    {
                        ShowTab(null, _index + 1);

                        pal_JH_XM.Tag = null;
                    }
                    else if(index == 1)//父级 - 项目
                    {
                        ShowTab("plan_project", _index + 1);
                        pal_JH_XM.Tag = dgv_Special_FileList.Tag;
                        ResetControls(ControlType.Plan);
                    }
                    else if(index == 2)//父级 - 课题
                    {
                        ShowTab("plan_topic", _index + 1);
                        pal_JH_KT.Tag = dgv_Special_FileList.Tag;
                        ResetControls(ControlType.Topic);
                    }
                }
            }
            else
            {
                object id = dgv_Plan_FileList.Tag;
                if(id == null)
                {
                    MessageBox.Show("尚未保存当前项目，无法添加新数据。", "温馨提示");
                    cbo_JH_HasNext.SelectedIndex = 0;
                }
                else
                {
                    int _index = tab_MenuList.SelectedIndex;
                    int index = comboBox.SelectedIndex;
                    if(index == 0)//无
                    {
                        ShowTab(null, _index + 1);

                        pal_JH_XM.Tag = null;
                    }
                    else if(index == 1)//父级 - 项目
                    {
                        ShowTab("plan_project", _index + 1);
                        ResetControls(ControlType.Plan);
                        pal_JH_XM.Tag = dgv_Plan_FileList.Tag;
                        txt_Project_Code.Text = DateTime.Now.Year + GetValue(planCode);
                    }
                    else if(index == 2)//父级 - 课题
                    {
                        ShowTab("plan_topic", _index + 1);
                        ResetControls(ControlType.Topic);
                        pal_JH_KT.Tag = dgv_Plan_FileList.Tag;
                        txt_Project_Code.Text = DateTime.Now.Year + GetValue(planCode);
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
                    MessageBox.Show("尚未保存当前课题信息，无法添加新数据。", "温馨提示");
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
            //计划
            if("btn_Plan_Save".Equals(button.Name))
            {
                view = dgv_Plan_FileList;
                key = "plan_fl_";
                int fileIndex = tab_Plan_Info.SelectedTabPageIndex;
                if(fileIndex == 0)//文件
                {
                    object objId = view.Parent.Parent.Tag;
                    if(objId == null)
                        objId = view.Tag = AddProjectBasicInfo(this.OBJECT_ID, ControlType.Plan);
                    else
                        UpdateProjectBasicInfo(objId, ControlType.Plan);
                    MessageBox.Show("基础信息保存成功。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    if(CheckFileList(view.Rows, key))
                    {
                        SqlHelper.ExecuteNonQuery($"DELETE FROM processing_file_list WHERE pfl_obj_id='{objId}'");
                        int maxLength = view.Rows.Count - 1;
                        for(int i = 0; i < maxLength; i++)
                        {
                            DataGridViewRow row = view.Rows[i];
                            row.Cells["id"].Tag = AddFileInfo(key, row, objId, i);
                        }
                        SqlHelper.ExecuteNonQuery($"UPDATE processing_tag SET pt_secret='{GetMaxSecretById(objId)}' WHERE pt_obj_id='{objId}'");
                        MessageBox.Show("文件信息保存成功。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

                        GoToTreeList();
                    }
                    else
                        MessageBox.Show("存在文件名重复的文件。", "保存失败", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
                else if(fileIndex == 1)//文件核查
                {
                    ModifyFileValid(dgv_JH_FileValid, dgv_Plan_FileList.Tag, "plan_fc_");
                    MessageBox.Show("文件核查信息保存成功！");
                }
            }
            //项目
            else if("btn_Project_Save".Equals(button.Name))
            {
                view = dgv_Project_FileList; key = "project_fl_";
                int fileIndex = tab_Project_Info.SelectedTabPageIndex;
                if(fileIndex == 0)
                {
                    string code = txt_Project_Code.Text;
                    if(string.IsNullOrEmpty(code))
                        MessageBox.Show("项目编号不能为空。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    else
                    {
                        object objId = view.Parent.Parent.Tag;
                        if(objId != null)//更新
                            UpdateProjectBasicInfo(view.Tag, ControlType.Plan);
                        else//新增
                            objId = view.Tag = AddProjectBasicInfo(pal_JH_XM.Tag, ControlType.Plan);
                        MessageBox.Show("基础信息保存成功。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        if(CheckFileList(view.Rows, key))
                        {
                            SqlHelper.ExecuteNonQuery($"DELETE FROM processing_file_list WHERE pfl_obj_id='{objId}'");
                            int maxLength = view.Rows.Count - 1;
                            for(int i = 0; i < maxLength; i++)
                            {
                                DataGridViewRow row = view.Rows[i];
                                row.Cells["jh_xm_id"].Tag = AddFileInfo("project_fl_", row, objId, i);
                            }
                            SqlHelper.ExecuteNonQuery($"UPDATE processing_tag SET pt_secret='{GetMaxSecretById(objId)}' WHERE pt_obj_id='{objId}'");
                            MessageBox.Show("文件信息保存成功。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

                            GoToTreeList();
                        }
                        else
                            MessageBox.Show("存在文件名重复的文件。", "保存失败", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                }
                else if(fileIndex == 1)//文件核查
                {
                    ModifyFileValid(dgv_JH_XM_FileValid, dgv_Project_FileList.Tag, "project_fc_");
                    MessageBox.Show("文件核查信息保存成功！");
                }
            }
            //课题
            else if("btn_Topic_Save".Equals(button.Name))
            {
                view = dgv_Topic_FileList; key = "topic_fl_";
                int fileIndex = tab_Topic_Info.SelectedTabPageIndex;
                if(fileIndex == 0)
                {
                    string code = txt_Topic_Code.Text;
                    if(string.IsNullOrEmpty(code))
                        MessageBox.Show("课题编号不能为空。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    else
                    {
                        object objId = view.Parent.Parent.Tag;
                        if(objId != null)//更新
                            UpdateProjectBasicInfo(objId, ControlType.Topic);
                        else//新增
                            objId = view.Tag = AddProjectBasicInfo(pal_JH_KT.Tag, ControlType.Topic);
                        MessageBox.Show("基础信息保存成功。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        if(CheckFileList(view.Rows, key))
                        {
                            SqlHelper.ExecuteNonQuery($"DELETE FROM processing_file_list WHERE pfl_obj_id='{objId}'");
                            int maxLength = view.Rows.Count - 1;
                            for(int i = 0; i < maxLength; i++)
                            {
                                DataGridViewRow row = view.Rows[i];
                                row.Cells["jh_kt_id"].Tag = AddFileInfo("topic_fl_", row, objId, i);
                            }
                            SqlHelper.ExecuteNonQuery($"UPDATE processing_tag SET pt_secret='{GetMaxSecretById(objId)}' WHERE pt_obj_id='{objId}'");
                            MessageBox.Show("文件信息保存成功。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

                            GoToTreeList();
                        }
                        else
                            MessageBox.Show("存在文件名重复的文件。", "保存失败", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                }
                else if(fileIndex == 1)//文件核查
                {
                    ModifyFileValid(dgv_JH_KT_FileValid, dgv_Topic_FileList.Tag, "topic_fc_");
                    MessageBox.Show("文件核查信息保存成功！");
                }
            }
            //子课题
            else if("btn_Subject_Save".Equals(button.Name))
            {
                view = dgv_Subject_FileList; key = "subject_fl_";
                int fileIndex = tab_Subject_Info.SelectedTabPageIndex;
                if(fileIndex == 0)
                {
                    string code = txt_Subject_Code.Text;
                    if(string.IsNullOrEmpty(code))
                        MessageBox.Show("课题编号不能为空。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    else
                    {
                        object objId = view.Parent.Parent.Tag;
                        if(objId != null)//更新
                            UpdateProjectBasicInfo(objId, ControlType.Subject);
                        else//新增
                            objId = view.Tag = AddProjectBasicInfo(pal_JH_XM_KT_ZKT.Tag, ControlType.Subject);
                        MessageBox.Show("基础信息保存成功。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        if(CheckFileList(view.Rows, key))
                        {
                            SqlHelper.ExecuteNonQuery($"DELETE FROM processing_file_list WHERE pfl_obj_id='{objId}'");
                            int maxLength = view.Rows.Count - 1;
                            for(int i = 0; i < maxLength; i++)
                            {
                                DataGridViewRow row = view.Rows[i];
                                row.Cells["jh_xm_kt_zkt_id"].Tag = AddFileInfo("subject_fl_", row, objId, i);
                            }
                            SqlHelper.ExecuteNonQuery($"UPDATE processing_tag SET pt_secret='{GetMaxSecretById(objId)}' WHERE pt_obj_id='{objId}'");
                            MessageBox.Show("文件信息保存成功。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

                            GoToTreeList();
                        }
                        else
                            MessageBox.Show("存在文件名重复的文件。", "保存失败", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                }
                else if(fileIndex == 1)//文件核查
                {
                    ModifyFileValid(dgv_JH_XM_KT_ZKT_FileValid, dgv_Subject_FileList.Tag, "subject_fc_");
                    MessageBox.Show("文件核查信息保存成功！");
                }
            }
            //重大专项/研发
            else if("btn_Imp_Save".Equals(button.Name))
            {
                view = dgv_Imp_FileList;
                key = "imp_fl_";
                int fileIndex = tab_Imp_Info.SelectedTabPageIndex;
                if(fileIndex == 0)
                {
                    object objId = view.Parent.Parent.Tag;
                    if(objId == null)
                    {
                        objId = view.Tag = AddProjectBasicInfo(OBJECT_ID, ControlType.Imp);
                        MessageBox.Show("基础信息保存成功。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                    if(CheckFileList(view.Rows, key))
                    {
                        int maxLength = view.Rows.Count - 1;
                        for(int i = 0; i < maxLength; i++)
                        {
                            DataGridViewRow row = view.Rows[i];
                            object fileId = AddFileInfo(key, row, objId, row.Index);
                            row.Cells[$"{key}id"].Tag = fileId;
                        }
                        RemoveFileList();
                        MessageBox.Show("文件信息存在错误数据，请先更正。", "保存失败", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        
                        GoToTreeList();
                    }
                    else
                        MessageBox.Show("存在文件名重复的文件。", "保存失败", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
                else if(fileIndex == 1)//文件核查
                {
                    ModifyFileValid(dgv_Imp_FileValid, dgv_Imp_FileList.Tag, "imp_fc_");
                    MessageBox.Show("文件核查信息保存成功！");
                }
            }
            //重大专项/研发-信息
            else if("btn_Special_Save".Equals(button.Name))
            {
                view = dgv_Special_FileList; key = "special_fl_";
                int fileIndex = tab_Special_Info.SelectedTabPageIndex;
                if(fileIndex == 0)
                {
                    string code = txt_Special_Code.Text;
                    if(string.IsNullOrEmpty(code))
                        MessageBox.Show("编号不能为空。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    else
                    {
                        object objId = view.Parent.Parent.Tag;
                        if(objId != null)//更新
                            UpdateProjectBasicInfo(objId, ControlType.Special);
                        else//新增
                            objId = view.Tag = AddProjectBasicInfo(pal_Special.Tag, ControlType.Special);
                        MessageBox.Show("基础信息保存成功。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        if(CheckFileList(view.Rows, key))
                        {
                            SqlHelper.ExecuteNonQuery($"DELETE FROM processing_file_list WHERE pfl_obj_id='{objId}'");
                            int maxLength = view.Rows.Count - 1;
                            for(int i = 0; i < maxLength; i++)
                            {
                                DataGridViewRow row = view.Rows[i];
                                row.Cells["imp_dev_id"].Tag = AddFileInfo("special_fl_", row, objId, i);
                            }
                            SqlHelper.ExecuteNonQuery($"UPDATE processing_tag SET pt_secret='{GetMaxSecretById(objId)}' WHERE pt_obj_id='{objId}'");
                            MessageBox.Show("文件信息保存成功。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

                            GoToTreeList();
                        }
                        else
                            MessageBox.Show("存在文件名重复的文件。", "保存失败", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                }
                else if(fileIndex == 1)//文件核查
                {
                    ModifyFileValid(dgv_Special_FileValid, dgv_Special_FileList.Tag, "special_fc_");
                    MessageBox.Show("文件核查信息保存成功！");
                }
            }
        }

        private void RemoveFileList()
        {
            string fileString = string.Empty;
            for(int i = 0; i < removeIdList.Count; i++)
            {
                //将删除后的编号的后续文件编号减1
                object[] code = SqlHelper.ExecuteRowsQuery($"SELECT pfl_code, pfl_obj_id FROM processing_file_list WHERE pfl_id='{removeIdList[i]}'");
                string key = GetValue(code[0]).Split('-')[0], value = GetValue(code[0]).Split('-')[1];
                List<object[]> idsString = SqlHelper.ExecuteColumnsQuery($"SELECT pfl_id, pfl_code FROM processing_file_list WHERE pfl_code LIKE '%{key}%' AND pfl_code>'{code[0]}' AND pfl_obj_id='{code[1]}'", 2);
                string updateSql = string.Empty;
                for(int j = 0; j < idsString.Count; j++)
                {
                    string oldValue = GetValue(idsString[j][1]).Split('-')[1];
                    string newCode = key + "-" + (Convert.ToInt32(oldValue) - 1).ToString().PadLeft(2, '0');
                    updateSql += $"UPDATE processing_file_list SET pfl_code='{newCode}' WHERE pfl_id='{idsString[j][0]}';";
                }
                if(!string.IsNullOrEmpty(updateSql))
                    SqlHelper.ExecuteNonQuery(updateSql);

                //收集文件号（供重新选取）
                object fileId = SqlHelper.ExecuteOnlyOneQuery($"SELECT pfl_file_id FROM processing_file_list WHERE pfl_id='{removeIdList[i]}';");
                if(fileId != null)
                    fileString += $"'{fileId}',";

                //如果文件已装盒，则删除之
                string queryString = $"SELECT pb_id, pb_files_id FROM processing_box WHERE pb_obj_id='{code[1]}'";
                List<object[]> list = SqlHelper.ExecuteColumnsQuery(queryString, 2);
                updateSql = string.Empty;
                for(int j = 0; j < list.Count; j++)
                {
                    string fileIds = GetValue(list[j][1]).Trim();
                    string targetId = GetValue(removeIdList[i]).Trim();
                    if(!string.IsNullOrEmpty(fileIds) && fileIds.Contains(targetId))
                    {
                        string newFileIds = fileIds.Replace(targetId + ",", string.Empty).Replace(targetId, string.Empty);
                        updateSql += $"UPDATE files_box_info SET pb_files_id='{newFileIds}' WHERE pb_id='{list[j][0]}';";
                        break;
                    }
                }
                if(!string.IsNullOrEmpty(updateSql))
                    SqlHelper.ExecuteNonQuery(updateSql);

                //删除当前文件
                SqlHelper.ExecuteNonQuery($"DELETE FROM processing_file_list WHERE pfl_id='{removeIdList[i]}';");
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
            if(workType == WorkType.Default)
            {
                if(controlType == ControlType.Plan)
                    LoadTreeList(PLAN_ID, ControlType.Plan);
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
                if(cellName.Value == null || string.IsNullOrEmpty(GetValue(cellName.Value).Trim()))
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

                DataGridViewCell pagesCell = rows[i].Cells[key + "page"];
                if(pagesCell.Value == null || string.IsNullOrEmpty(GetValue(pagesCell.Value)) || Convert.ToInt32(pagesCell.Value) == 0)
                {
                    pagesCell.ErrorText = "温馨提示：页数不能为0或空。";
                    result = false;
                }
                else
                    pagesCell.ErrorText = null;

                bool isOtherType = "其他".Equals(GetValue(rows[i].Cells[key + "categor"].FormattedValue).Trim());
                DataGridViewCell cellCode = rows[i].Cells[key + "categorname"];
                if(isOtherType)
                {
                    if(cellCode.Value == null || string.IsNullOrEmpty(GetValue(cellCode.Value).Trim()))
                    {
                        cellCode.ErrorText = "温馨提示：类型名称不能为空。";
                        result = false;
                    }
                    else
                        cellCode.ErrorText = null;
                }
                else
                    cellCode.ErrorText = null;
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
            for(int i = 0; i < rowCount; i++)
            {
                DataGridViewRow row = dataGridView.Rows[i];
                object rid = dataGridView.Rows[i].Cells[key + "id"].Tag;
                object pcode = row.Cells[key + "pcode"].Value;
                object pname = row.Cells[key + "pname"].Value;
                object categor = row.Cells[key + "categor"].Value;
                object name = row.Cells[key + "name"].Value;
                object reason = row.Cells[key + "reason"].Value;
                object remark = row.Cells[key + "remark"].Value;
                if(rid == null)
                {
                    rid = Guid.NewGuid().ToString();
                    string insertSql = $"INSERT INTO processing_file_lost VALUES('{rid}','{categor}','{name}','{reason}','{remark}','{objid}')";
                    SqlHelper.ExecuteNonQuery(insertSql);
                    dataGridView.Rows[i].Cells[key + "id"].Tag = rid;
                }
                else
                {
                    string updateSql = $"UPDATE processing_file_lost SET " +
                        $"pfo_categor='{categor}'," +
                        $"pfo_name='{name}'," +
                        $"pfo_reason='{reason}'," +
                        $"pfo_remark='{remark}'" +
                        $" WHERE pfo_id='{rid}'";
                    SqlHelper.ExecuteNonQuery(updateSql);
                }
            }
        }
  
        /// <summary>
        /// 更新 项目/课题基本信息
        /// </summary>
        /// <param name="objid">主键</param>
        /// <param name="controlType">操作对象类型</param>
        private void UpdateProjectBasicInfo(object objid, ControlType controlType)
        {
           if(controlType == ControlType.Plan)
            {
                string code = txt_Project_Code.Text;
                string name = txt_Project_Name.Text;
                string type = string.Empty;
                string ly = txt_Project_LY.Text;
                string zt = txt_Project_ZT.Text;
                string jf = txt_Project_JF.Text;
                DateTime starttime = dtp_Project_StartTime.Value;
                DateTime endtime = dtp_JH_XM_EndTime.Value;
                string year = txt_Project_LXND.Text;
                object unit = txt_Project_Unit.Text;
                object province = txt_Project_Province.Text;
                string unituser = txt_Project_UnitUser.Text;
                string objuser = txt_Project_ObjUser.Text;
                string intro = txt_Project_ObjIntroduct.Text;

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
                    $" WHERE pi_id='{objid}'";
                SqlHelper.ExecuteNonQuery(updateSql);
            }
            else if(controlType == ControlType.Topic)
            {
                string code = txt_Topic_Code.Text;
                string name = txt_Topic_Name.Text;
                string type = string.Empty;
                string ly = txt_Topic_LY.Text;
                string zt = txt_Topic_Theme.Text;
                string jf = txt_Topic_Fund.Text;
                DateTime starttime = dtp_Topic_StartTime.Value;
                DateTime endtime = dtp_Topic_EndTime.Value;
                string year = txt_Topic_Year.Text;
                object unit = txt_Topic_Unit.Text;
                object province = txt_Topic_Province.Text;
                string unituser = txt_Topic_UnitUser.Text;
                string objuser = txt_Topic_ProUser.Text;
                string intro = txt_Topic_Intro.Text;

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
                    $" WHERE pi_id='{objid}'";
                SqlHelper.ExecuteNonQuery(updateSql);
            }
            else if(controlType == ControlType.Subject)
            {
                string code = txt_Subject_Code.Text;
                string name = txt_Subject_Name.Text;
                string type = string.Empty;
                string ly = txt_Subject_LY.Text;
                string zt = txt_Subject_Theme.Text;
                string jf = txt_Subject_Fund.Text;
                DateTime starttime = dtp_Subject_StartTime.Value;
                DateTime endtime = dtp_Subject_EndTime.Value;
                string year = txt_Subject_Year.Text;
                object unit = txt_Subject_Unit.Text;
                string unituser = txt_Subject_Unituser.Text;
                string objuser = txt_Subject_Prouser.Text;
                object province = txt_Subject_Province.Text;
                string intro = txt_Subject_Intro.Text;

                string updateSql = "UPDATE subject_info SET " +
                    $"[si_code] = '{code}'" +
                    $",[si_name] = '{name}'" +
                    $",[si_type] = '{type}'" +
                    $",[si_field] = '{ly}'" +
                    $",[si_belong] = '{zt}'" +
                    $",[si_money] = '{jf}'" +
                    $",[si_start_datetime] = '{starttime}'" +
                    $",[si_end_datetime] = '{endtime}'" +
                    $",[si_year] = '{year}'" +
                    $",[si_company] = '{unit}'" +
                    $",[si_company_user] = '{unituser}'" +
                    $",[si_province] = '{province}'" +
                    $",[si_project_user] = '{objuser}'" +
                    $",[si_introduction] = '{intro}'" +
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

        /// <summary>
        /// 保存 计划-项目 基本信息
        /// </summary>
        /// <param name="parentId">父对象ID</param>
        /// <param name="type">对象类型</param>
        /// <returns>对象生成的主键</returns>
        private object AddProjectBasicInfo(object parentId, ControlType type)
        {
            string primaryKey = Guid.NewGuid().ToString();
           if(type == ControlType.Plan)
            {
                string code = txt_Project_Code.Text;
                string name = txt_Project_Name.Text;
                string planType = string.Empty;
                string ly = txt_Project_LY.Text;
                string zt = txt_Project_ZT.Text;
                string jf = txt_Project_JF.Text;
                DateTime starttime = dtp_Project_StartTime.Value;
                DateTime endtime = dtp_JH_XM_EndTime.Value;
                string year = txt_Project_LXND.Text;
                object unit = txt_Project_Unit.Text;
                object province = txt_Project_Province.Text;
                string unituser = txt_Project_UnitUser.Text;
                string objuser = txt_Project_ObjUser.Text;
                string intro = txt_Project_ObjIntroduct.Text;

                string insertSql = "INSERT INTO project_info(pi_id ,trc_id,pi_code,pi_name,pi_type,pb_belong" +
                    ",pb_belong_type,pi_money,pi_start_datetime,pi_end_datetime,pi_year,pi_company_id,pi_company_user" +
                    ",pi_province,pi_project_user,pi_introduction,pi_work_status,pi_obj_id,pi_categor,pi_submit_status,pi_worker_id)" +
                    "VALUES" +
                    $"('{primaryKey}',null,'{code}','{name}','{planType}','{ly}','{zt}','{jf}','{starttime}'" +
                    $",'{endtime}','{year}','{unit}','{unituser}'" +
                    $",'{province}','{objuser}','{intro}','{(int)WorkStatus.Default}','{parentId}',{(int)type},{(int)ObjectSubmitStatus.NonSubmit},'{UserHelper.GetInstance().User.UserKey}')";

                SqlHelper.ExecuteNonQuery(insertSql);
            }
            else if(type == ControlType.Topic)
            {
                string code = txt_Topic_Code.Text;
                string name = txt_Topic_Name.Text;
                string planType = string.Empty;
                string ly = txt_Topic_LY.Text;
                string zt = txt_Topic_Theme.Text;
                string jf = txt_Topic_Fund.Text;
                DateTime starttime = dtp_Topic_StartTime.Value;
                DateTime endtime = dtp_Topic_EndTime.Value;
                string year = txt_Topic_Year.Text;
                object unit = txt_Topic_Unit.Text;
                object province = txt_Topic_Province.Text;
                string unituser = txt_Topic_UnitUser.Text;
                string objuser = txt_Topic_ProUser.Text;
                string intro = txt_Topic_Intro.Text;

                string insertSql = "INSERT INTO project_info(pi_id ,trc_id,pi_code,pi_name,pi_type,pb_belong" +
                    ",pb_belong_type,pi_money,pi_start_datetime,pi_end_datetime,pi_year,pi_company_id,pi_company_user" +
                    ",pi_province,pi_project_user,pi_introduction,pi_work_status,pi_obj_id,pi_categor,pi_submit_status,pi_worker_id)" +
                    "VALUES" +
                    $"('{primaryKey}',null,'{code}','{name}','{planType}','{ly}','{zt}','{jf}','{starttime}'" +
                    $",'{endtime}','{year}','{unit}','{unituser}'" +
                    $",'{province}','{objuser}','{intro}','{(int)WorkStatus.Default}','{parentId}','{(int)type}','{(int)ObjectSubmitStatus.NonSubmit}','{UserHelper.GetInstance().User.UserKey}')";

                SqlHelper.ExecuteNonQuery(insertSql);
            }
            else if(type == ControlType.Subject)
            {
                string code = txt_Subject_Code.Text;
                string name = txt_Subject_Name.Text;
                string planType = string.Empty;
                string ly = txt_Subject_LY.Text;
                string zt = txt_Subject_Theme.Text;
                string jf = txt_Subject_Fund.Text;
                DateTime starttime = dtp_Subject_StartTime.Value;
                DateTime endtime = dtp_Subject_EndTime.Value;
                string year = txt_Subject_Year.Text;
                object unit = txt_Subject_Unit.Text;
                string unituser = txt_Subject_Unituser.Text;
                string objuser = txt_Subject_Prouser.Text;
                object province = txt_Subject_Province.Text;
                string intro = txt_Subject_Intro.Text;

                string insertSql = "INSERT INTO subject_info(si_id, pi_id, si_code, si_name, si_type, si_field, si_belong, si_money, si_start_datetime," +
                   "si_end_datetime, si_year, si_company, si_company_user, si_province, si_project_user, si_introduction, si_work_status, si_categor, si_submit_status," +
                   "si_worker_id) VALUES " +
                   $"('{primaryKey}','{parentId}','{code}','{name}','{planType}','{ly}','{zt}','{jf}'" +
                   $",'{starttime}','{endtime}','{year}','{unit}','{unituser}','{province}','{objuser}'" +
                   $",'{intro}','{(int)WorkStatus.NonWork}','{(int)type}',{(int)ObjectSubmitStatus.NonSubmit},'{UserHelper.GetInstance().User.UserKey}')";
                SqlHelper.ExecuteNonQuery(insertSql);
            }
            else if(type == ControlType.Imp)
            {
                string name = lbl_Imp_Name.Text;
                object intro = txt_Imp_Intro.Text;
                string insertSql = "INSERT INTO imp_info(imp_id, imp_code, imp_name, imp_intro, pi_categor, imp_submit_status, imp_obj_id, imp_source_id, imp_type) " +
                    $"VALUES ('{primaryKey}', '{planCode}', '{name}', '{intro}', '{(int)type}', '{(int)ObjectSubmitStatus.NonSubmit}', '{parentId}', '{UserHelper.GetInstance().User.UserKey}', {DEV_TYPE})";
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
                    $",'{UserHelper.GetInstance().User.UserKey}')";
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
            if(_fileId != null)
                sqlString += $"DELETE FROM processing_file_list WHERE pfl_id='{_fileId}';";
            else
                _fileId = Guid.NewGuid().ToString();
            object stage = row.Cells[key + "stage"].Value;
            object categor = row.Cells[key + "categor"].Value;
            object categorName = row.Cells[key + "categorname"].Value;
            object name = row.Cells[key + "name"].Value;
            object user = row.Cells[key + "user"].Value;
            object type = row.Cells[key + "type"].Value;
            object pages = row.Cells[key + "page"].Value;
            object count = row.Cells[key + "count"].Value;
            object code = row.Cells[key + "code"].Value;
            DateTime now = DateTime.MinValue;
            string _date = GetValue(row.Cells[key + "date"].Value);
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
            object unit = row.Cells[key + "unit"].Value;
            object carrier = row.Cells[key + "carrier"].Value;
            object link = row.Cells[key + "link"].Value;
            object fileId = row.Cells[key + "link"].Tag;
            object format = link == null ? string.Empty : Path.GetExtension(GetValue(link)).Replace(".", string.Empty);

            bool isOtherType = "其他".Equals(GetValue(row.Cells[key + "categor"].FormattedValue).Trim());
            if(isOtherType)
            {
                categor = Guid.NewGuid().ToString();
                string value = GetValue(code).Split('-')[0];
                int _sort = ((DataGridViewComboBoxCell)row.Cells[key + "categor"]).Items.Count - 1;

                sqlString += "INSERT INTO data_dictionary (dd_id, dd_name, dd_pId, dd_sort, extend_3, extend_4) " +
                    $"VALUES('{categor}', '{value}', '{stage}', '{_sort}', '{categorName}', '{1}');";
            }

            sqlString += "INSERT INTO processing_file_list (" +
            "pfl_id, pfl_code, pfl_stage, pfl_categor, pfl_name, pfl_user, pfl_type, pfl_pages, pfl_amount, pfl_date, pfl_unit, pfl_carrier, pfl_format, pfl_link, pfl_file_id, pfl_obj_id, pfl_sort) " +
            $"VALUES( '{_fileId}', '{code}', '{stage}', '{categor}', '{name}', '{user}', '{type}', '{pages}', '{count}', '{now.ToString("s")}', '{unit}', '{carrier}', '{format}', '{link}', '{fileId}', '{parentId}', '{sort}');";
            if(fileId != null)
            {
                int value = link == null ? 0 : 1;
                sqlString += $"UPDATE backup_files_info SET bfi_state={value} WHERE bfi_id='{fileId}';";
            }
            SqlHelper.ExecuteNonQuery(sqlString);
            return _fileId;
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
            //重大专项/重点研发
            if(workType == WorkType.Default)
            {
                if(isBacked)
                {
                    //重点计划
                    if(type == ControlType.Imp)
                    {
                        object[] _obj = SqlHelper.ExecuteRowsQuery($"SELECT imp_id, imp_name, imp_source_id FROM imp_info WHERE imp_id='{planId}'");
                        treeNode = new TreeNode()
                        {
                            Name = GetValue(_obj[0]),
                            Text = GetValue(_obj[1]),
                            Tag = type
                        };
                    }
                    //普通计划
                    else if(type == ControlType.Special)
                    {
                        object[] _obj = SqlHelper.ExecuteRowsQuery($"SELECT pi_id, pi_name, pi_worker_id FROM project_info WHERE pi_id='{planId}'");
                        treeNode = new TreeNode()
                        {
                            Name = GetValue(_obj[0]),
                            Text = GetValue(_obj[1]),
                            Tag = type
                        };
                    }
                    //重点计划 - 专项信息
                    else if(type == ControlType.Special)
                    {
                        DataRow row = SqlHelper.ExecuteSingleRowQuery($"SELECT * FROM imp_dev_info WHERE imp_id='{planId}'");
                        object[] _obj = SqlHelper.ExecuteRowsQuery($"SELECT imp_id, imp_name, imp_source_id FROM imp_info WHERE imp_id='{row["imp_obj_id"]}'");
                        treeNode = new TreeNode()
                        {
                            Name = GetValue(_obj[0]),
                            Text = GetValue(_obj[1]),
                            Tag = ControlType.Imp,
                            ForeColor = DisEnbleColor
                        };
                        TreeNode treeNode2 = new TreeNode()
                        {
                            Name = GetValue(row["imp_id"]),
                            Text = GetValue(row["imp_code"]),
                            Tag = ControlType.Special
                        };
                        treeNode.Nodes.Add(treeNode2);
                    }
                    //项目/课题
                    else if(type == ControlType.Plan)
                    {
                        DataRow currentRow = SqlHelper.ExecuteSingleRowQuery($"SELECT * FROM project_info WHERE pi_id='{planId}'");
                        DataRow devRow = SqlHelper.ExecuteSingleRowQuery($"SELECT * FROM imp_dev_info WHERE imp_id='{currentRow["pi_obj_id"]}'");
                        if(devRow != null)
                        {
                            DataRow impRow = SqlHelper.ExecuteSingleRowQuery($"SELECT * FROM imp_info WHERE imp_id='{devRow["imp_obj_id"]}'");
                            treeNode = new TreeNode()
                            {
                                Name = GetValue(impRow["imp_id"]),
                                Text = GetValue(impRow["imp_code"]),
                                Tag = ControlType.Imp,
                                ForeColor = DisEnbleColor
                            };
                            TreeNode treeNode2 = new TreeNode()
                            {
                                Name = GetValue(devRow["imp_id"]),
                                Text = GetValue(devRow["imp_code"]),
                                Tag = ControlType.Special,
                                ForeColor = DisEnbleColor
                            };
                            treeNode.Nodes.Add(treeNode2);
                            TreeNode currentNode = new TreeNode()
                            {
                                Name = GetValue(currentRow["pi_id"]),
                                Text = GetValue(currentRow["pi_code"]),
                                Tag = ControlType.Plan,
                                ForeColor = 1.Equals(currentRow["pi_submit_status"]) ? Color.Black : DisEnbleColor
                            };
                            treeNode2.Nodes.Add(currentNode);
                            List<object[]> list = SqlHelper.ExecuteColumnsQuery($"SELECT si_id, si_code, si_worker_id, si_submit_status FROM subject_info WHERE pi_id='{currentNode.Name}'", 4);
                            for(int i = 0; i < list.Count; i++)
                            {
                                TreeNode treeNode3 = new TreeNode()
                                {
                                    Name = GetValue(list[i][0]),
                                    Text = GetValue(list[i][1]),
                                    Tag = ControlType.Topic,
                                    ForeColor = 1.Equals(list[i][3]) ? Color.Black : DisEnbleColor
                                };
                                currentNode.Nodes.Add(treeNode3);
                                List<object[]> list2 = SqlHelper.ExecuteColumnsQuery($"SELECT si_id, si_code, si_worker_id, si_submit_status FROM subject_info WHERE pi_id='{treeNode3.Name}'", 4);
                                for(int j = 0; j < list2.Count; j++)
                                {
                                    TreeNode treeNode4 = new TreeNode()
                                    {
                                        Name = GetValue(list2[j][0]),
                                        Text = GetValue(list2[j][1]),
                                        Tag = ControlType.Subject,
                                        ForeColor = 1.Equals(list2[j][3]) ? Color.Black : DisEnbleColor
                                    };
                                    treeNode3.Nodes.Add(treeNode4);
                                }
                            }
                        }
                        else
                        {
                            devRow = SqlHelper.ExecuteSingleRowQuery($"SELECT * FROM project_info WHERE pi_id='{currentRow["pi_obj_id"]}'");
                            if(devRow != null)
                            {
                                DataRow impRow = SqlHelper.ExecuteSingleRowQuery($"SELECT * FROM imp_info WHERE imp_id='{devRow["pi_obj_id"]}'");
                                treeNode = new TreeNode()
                                {
                                    Name = GetValue(devRow["pi_id"]),
                                    Text = GetValue(devRow["pi_code"]),
                                    Tag = ControlType.Special,
                                    ForeColor = DisEnbleColor
                                };
                                TreeNode currentNode = new TreeNode()
                                {
                                    Name = GetValue(currentRow["pi_id"]),
                                    Text = GetValue(currentRow["pi_code"]),
                                    Tag = ControlType.Plan,
                                    ForeColor = 1.Equals(currentRow["pi_submit_status"]) ? Color.Black : DisEnbleColor
                                };
                                treeNode.Nodes.Add(currentNode);
                                List<object[]> list = SqlHelper.ExecuteColumnsQuery($"SELECT si_id, si_code, si_worker_id, si_submit_status FROM subject_info WHERE pi_id='{currentNode.Name}'", 4);
                                for(int i = 0; i < list.Count; i++)
                                {
                                    TreeNode treeNode3 = new TreeNode()
                                    {
                                        Name = GetValue(list[i][0]),
                                        Text = GetValue(list[i][1]),
                                        Tag = ControlType.Topic,
                                        ForeColor = 1.Equals(list[i][3]) ? Color.Black : DisEnbleColor
                                    };
                                    currentNode.Nodes.Add(treeNode3);
                                    List<object[]> list2 = SqlHelper.ExecuteColumnsQuery($"SELECT si_id, si_code, si_worker_id, si_submit_status FROM subject_info WHERE pi_id='{treeNode3.Name}'", 4);
                                    for(int j = 0; j < list2.Count; j++)
                                    {
                                        TreeNode treeNode4 = new TreeNode()
                                        {
                                            Name = GetValue(list2[j][0]),
                                            Text = GetValue(list2[j][1]),
                                            Tag = ControlType.Subject,
                                            ForeColor = 1.Equals(list2[j][3]) ? Color.Black : DisEnbleColor
                                        };
                                        treeNode3.Nodes.Add(treeNode4);
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    object[] _obj = SqlHelper.ExecuteRowsQuery($"SELECT imp_id, imp_name, imp_source_id FROM imp_info WHERE imp_obj_id='{OBJECT_ID}'");
                    if(_obj == null)
                        _obj = SqlHelper.ExecuteRowsQuery($"SELECT dd_id, dd_name, '{UserHelper.GetInstance().User.UserKey}' FROM data_dictionary WHERE dd_id='{planId}'");
                    treeNode = new TreeNode()
                    {
                        Name = GetValue(_obj[0]),
                        Text = GetValue(_obj[1]),
                        Tag = ControlType.Imp
                    };
                    if(!UserHelper.GetInstance().User.UserKey.Equals(_obj[2]))
                        treeNode.ForeColor = DisEnbleColor;
                    //根据重大专项查询具体专项信息
                    DataTable table = SqlHelper.ExecuteQuery($"SELECT imp_id, imp_code, imp_source_id FROM imp_dev_info WHERE imp_obj_id='{treeNode.Name}'");
                    foreach(DataRow row in table.Rows)
                    {
                        TreeNode treeNode2 = new TreeNode()
                        {
                            Name = GetValue(row["imp_id"]),
                            Text = GetValue(row["imp_code"]),
                            Tag = ControlType.Special
                        };
                        if(!UserHelper.GetInstance().User.UserKey.Equals(row["imp_source_id"]))
                            treeNode2.ForeColor = DisEnbleColor;
                        treeNode.Nodes.Add(treeNode2);
                        object queryCondition = UserHelper.GetInstance().GetUserRole() == UserRole.Worker ? string.Empty : $" AND pi_worker_id='{UserHelper.GetInstance().User.UserKey}'";
                        //根据【专项信息】查询【项目/课题】集
                        List<object[]> list = SqlHelper.ExecuteColumnsQuery($"SELECT pi_id, pi_code, pi_categor, pi_worker_id FROM project_info WHERE pi_obj_id='{treeNode2.Name}'{queryCondition}", 4);
                        for(int i = 0; i < list.Count; i++)
                        {
                            TreeNode treeNode3 = new TreeNode()
                            {
                                Name = GetValue(list[i][0]),
                                Text = GetValue(list[i][1]),
                                Tag = (ControlType)list[i][2]
                            };
                            if(!UserHelper.GetInstance().User.UserKey.Equals(list[i][3]))
                                treeNode3.ForeColor = DisEnbleColor;
                            treeNode2.Nodes.Add(treeNode3);
                            queryCondition = UserHelper.GetInstance().GetUserRole() == UserRole.Worker ? string.Empty : $" AND si_worker_id='{UserHelper.GetInstance().User.UserKey}'";
                            //根据【项目/课题】查询【课题/子课题】集
                            List<object[]> list2 = SqlHelper.ExecuteColumnsQuery($"SELECT si_id, si_code, si_categor, si_worker_id FROM subject_info WHERE pi_id='{treeNode3.Name}'{queryCondition}", 4);
                            for(int j = 0; j < list2.Count; j++)
                            {
                                TreeNode treeNode4 = new TreeNode()
                                {
                                    Name = GetValue(list2[j][0]),
                                    Text = GetValue(list2[j][1]),
                                    Tag = (ControlType)list2[j][2]
                                };
                                if(!UserHelper.GetInstance().User.UserKey.Equals(list2[j][3]))
                                    treeNode4.ForeColor = DisEnbleColor;
                                treeNode3.Nodes.Add(treeNode4);
                                List<object[]> list3 = SqlHelper.ExecuteColumnsQuery($"SELECT si_id, si_code, si_categor, si_worker_id FROM subject_info WHERE pi_id='{treeNode4.Name}'{queryCondition}", 4);
                                for(int k = 0; k < list3.Count; k++)
                                {
                                    TreeNode treeNode5 = new TreeNode()
                                    {
                                        Name = GetValue(list3[k][0]),
                                        Text = GetValue(list3[k][1]),
                                        Tag = (ControlType)list3[k][2]
                                    };
                                    if(!UserHelper.GetInstance().User.UserKey.Equals(list3[k][3]))
                                        treeNode5.ForeColor = DisEnbleColor;
                                    treeNode4.Nodes.Add(treeNode5);
                                }
                            }
                        }
                    }
                }
                treeView.Nodes.Add(treeNode);
                if(treeView.Nodes.Count > 0)
                {
                    TreeNode node = treeView.Nodes[0];
                    if(isBacked)
                    {
                        if(type == ControlType.Imp || type == ControlType.Special)
                        {
                            ShowTab("imp", 0);
                            LoadImpPage(node.Name, node.ForeColor);
                        }
                        else if(type == ControlType.Special)
                        {
                            ShowTab("plan", 0);
                            LoadPlanPage(node.Name, Color.Black);
                        }
                        else if(type == ControlType.Plan)
                        {
                            object _temp = SqlHelper.ExecuteOnlyOneQuery($"SELECT pi_id FROM project_info WHERE pi_id='{node.Name}'");
                            if(_temp == null)//重大专项>>项目/计划
                            {
                                ShowTab("imp", 0);
                                LoadImpPage(node.Name, node.ForeColor);
                            }
                            else//普通专项>>项目/计划
                            {
                                ShowTab("plan", 0);
                                LoadPlanPage(node.Name, Color.Black);
                            }
                        }
                    }
                    else
                    {
                        ShowTab("imp", 0);
                        LoadImpPage(node.Name, node.ForeColor);
                    }
                }
            }
            else
            {
                //光盘加工
                if(workType == WorkType.CDWork)
                {
                    object[] _obj = SqlHelper.ExecuteRowsQuery($"SELECT pi_id,pi_name,pi_categor FROM project_info WHERE pi_id='{planId}' AND pi_worker_id='{UserHelper.GetInstance().User.UserKey}'");
                    if(_obj == null)
                        _obj = SqlHelper.ExecuteRowsQuery($"SELECT pi_id,pi_name,pi_categor FROM project_info WHERE pi_obj_id='{OBJECT_ID}'");
                    if(_obj == null)
                        _obj = SqlHelper.ExecuteRowsQuery($"SELECT dd_id,dd_name FROM data_dictionary WHERE dd_id='{planId}'");
                    treeNode = new TreeNode()
                    {
                        Name = _obj[0].ToString(),
                        Text = _obj[1].ToString(),
                        Tag = ControlType.Plan,
                    };
                    //根据【计划】查询【项目/课题】集
                    List<object[]> list = SqlHelper.ExecuteColumnsQuery($"SELECT pi_id,pi_code,pi_categor FROM project_info WHERE pi_obj_id='{treeNode.Name}' AND pi_worker_id='{UserHelper.GetInstance().User.UserKey}' " +
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
                        List<object[]> list2 = SqlHelper.ExecuteColumnsQuery($"SELECT si_id,si_code,si_categor FROM subject_info WHERE pi_id='{treeNode2.Name}' AND si_worker_id='{UserHelper.GetInstance().User.UserKey}' " +
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

                            List<object[]> list3 = SqlHelper.ExecuteColumnsQuery($"SELECT si_id,si_code,si_categor FROM subject_info WHERE pi_id='{treeNode3.Name}' AND si_worker_id='{UserHelper.GetInstance().User.UserKey}' " +
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
                    object[] _obj = SqlHelper.ExecuteRowsQuery($"SELECT pi_id, pi_name, pi_worker_id FROM project_info WHERE pi_id='{planId}'");
                    if(_obj == null)
                        _obj = SqlHelper.ExecuteRowsQuery($"SELECT dd_id, dd_name, '{UserHelper.GetInstance().User.UserKey}' FROM data_dictionary WHERE dd_id='{planId}'");
                    treeNode = new TreeNode()
                    {
                        Name = GetValue(_obj[0]),
                        Text = GetValue(_obj[1]),
                        Tag = ControlType.Plan
                    };
                    if(!UserHelper.GetInstance().User.UserKey.Equals(_obj[2]))
                        treeNode.ForeColor = DisEnbleColor;
                    //【管理员】查看其他人的任务，【普通用户】只能查看自己的任务
                    object queryCondition = UserHelper.GetInstance().GetUserRole() == UserRole.Worker ? string.Empty : $"AND pi_worker_id='{UserHelper.GetInstance().User.UserKey}'";
                    //根据【计划】查询【项目/课题】集
                    List<object[]> list = SqlHelper.ExecuteColumnsQuery($"SELECT pi_id, pi_code, pi_categor, pi_worker_id FROM project_info WHERE pi_obj_id='{treeNode.Name}'{queryCondition}", 4);
                    for(int i = 0; i < list.Count; i++)
                    {
                        TreeNode treeNode2 = new TreeNode()
                        {
                            Name = GetValue(list[i][0]),
                            Text = GetValue(list[i][1]),
                            Tag = (ControlType)list[i][2]
                        };
                        if(!UserHelper.GetInstance().User.UserKey.Equals(list[i][3]))
                            treeNode2.ForeColor = DisEnbleColor;
                        treeNode.Nodes.Add(treeNode2);
                        queryCondition = UserHelper.GetInstance().GetUserRole() == UserRole.Worker ? string.Empty : $"AND si_worker_id='{UserHelper.GetInstance().User.UserKey}'";
                        //根据【项目/课题】查询【课题/子课题】集
                        List<object[]> list2 = SqlHelper.ExecuteColumnsQuery($"SELECT si_id, si_code, si_categor, si_worker_id FROM subject_info WHERE pi_id='{treeNode2.Name}'{queryCondition}", 4);
                        for(int j = 0; j < list2.Count; j++)
                        {
                            TreeNode treeNode3 = new TreeNode()
                            {
                                Name = GetValue(list2[j][0]),
                                Text = GetValue(list2[j][1]),
                                Tag = (ControlType)list2[j][2]
                            };
                            if(!UserHelper.GetInstance().User.UserKey.Equals(list2[j][3]))
                                treeNode3.ForeColor = DisEnbleColor;
                            treeNode2.Nodes.Add(treeNode3);

                            List<object[]> list3 = SqlHelper.ExecuteColumnsQuery($"SELECT si_id, si_code, si_categor, si_worker_id FROM subject_info WHERE pi_id='{treeNode3.Name}'{queryCondition}", 4);
                            for(int k = 0; k < list3.Count; k++)
                            {
                                TreeNode treeNode4 = new TreeNode()
                                {
                                    Name = GetValue(list3[k][0]),
                                    Text = GetValue(list3[k][1]),
                                    Tag = (ControlType)list3[k][2]
                                };
                                if(!UserHelper.GetInstance().User.UserKey.Equals(list3[k][3]))
                                    treeNode4.ForeColor = DisEnbleColor;
                                treeNode3.Nodes.Add(treeNode4);
                            }
                        }
                    }
                }
                //父级（项目/课题）
                else if(workType == WorkType.ProjectWork)
                {
                    object[] _obj = SqlHelper.ExecuteRowsQuery($"SELECT pi_id, pi_name, pi_categor FROM project_info WHERE pi_id='{planId}'");
                    if(_obj == null)
                        _obj = SqlHelper.ExecuteRowsQuery($"SELECT dd_id,dd_name FROM data_dictionary WHERE dd_id='{planId}'");
                    treeNode = new TreeNode()
                    {
                        Name = _obj[0].ToString(),
                        Text = _obj[1].ToString(),
                        Tag = ControlType.Plan,
                        ForeColor = DisEnbleColor
                    };
                    //根据【计划】查询【项目/课题】集
                    List<object[]> list = SqlHelper.ExecuteColumnsQuery($"SELECT pi_id,pi_code,pi_categor FROM project_info WHERE pi_obj_id='{treeNode.Name}' AND pi_worker_id='{UserHelper.GetInstance().User.UserKey}' " +
                        $"ORDER BY pi_code", 3);
                    for(int i = 0; i < list.Count; i++)
                    {
                        TreeNode treeNode2 = new TreeNode()
                        {
                            Name = list[i][0].ToString(),
                            Text = list[i][1].ToString(),
                            Tag = (ControlType)list[i][2]
                        };
                        if(treeNode2.Name.Equals(OBJECT_ID))
                        {
                            treeNode.Nodes.Add(treeNode2);
                            //根据【项目/课题】查询【课题/子课题】集
                            List<object[]> list2 = SqlHelper.ExecuteColumnsQuery($"SELECT si_id,si_code,si_categor FROM subject_info WHERE pi_id='{treeNode2.Name}' AND si_worker_id='{UserHelper.GetInstance().User.UserKey}' ORDER BY si_code", 3);
                            for(int j = 0; j < list2.Count; j++)
                            {
                                TreeNode treeNode3 = new TreeNode()
                                {
                                    Name = list2[j][0].ToString(),
                                    Text = list2[j][1].ToString(),
                                    Tag = (ControlType)list2[j][2]
                                };
                                treeNode2.Nodes.Add(treeNode3);

                                List<object[]> list3 = SqlHelper.ExecuteColumnsQuery($"SELECT si_id,si_code,si_categor FROM subject_info WHERE pi_id='{treeNode3.Name}' AND si_worker_id='{UserHelper.GetInstance().User.UserKey}' ORDER BY si_code", 3);
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
                            break;
                        }
                    }
                }
                //子级（课题/子课题）
                else if(workType == WorkType.SubjectWork)
                {
                    object[] _obj = SqlHelper.ExecuteRowsQuery($"SELECT pi_id, pi_name, pi_worker_id FROM project_info WHERE pi_id='{planId}'");
                    if(_obj == null)
                        _obj = SqlHelper.ExecuteRowsQuery($"SELECT dd_id, dd_name,'{UserHelper.GetInstance().User.UserKey}' FROM data_dictionary WHERE dd_id='{planId}'");
                    treeNode = new TreeNode()
                    {
                        Name = GetValue(_obj[0]),
                        Text = GetValue(_obj[1]),
                        Tag = ControlType.Plan
                    };
                    //如果当前任务并非登录人加工，则无法编辑【文字置灰】
                    if(!UserHelper.GetInstance().User.UserKey.Equals(_obj[2]))
                        treeNode.ForeColor = DisEnbleColor;
                    //根据【计划】查询【项目/课题】集
                    List<object[]> list = SqlHelper.ExecuteColumnsQuery($"SELECT pi_id, pi_code, pi_categor, pi_worker_id FROM project_info WHERE pi_obj_id='{treeNode.Name}'", 4);
                    for(int i = 0; i < list.Count; i++)
                    {
                        TreeNode treeNode2 = new TreeNode()
                        {
                            Name = GetValue(list[i][0]),
                            Text = GetValue(list[i][1]),
                            Tag = (ControlType)list[i][2]
                        };
                        //如果当前任务并非登录人加工，则无法编辑【文字置灰】
                        if(!UserHelper.GetInstance().User.UserKey.Equals(list[i][3]))
                            treeNode2.ForeColor = DisEnbleColor;
                        //根据【项目/课题】查询【课题/子课题】集
                        List<object[]> list2 = SqlHelper.ExecuteColumnsQuery($"SELECT si_id, si_code, si_categor, si_worker_id FROM subject_info WHERE pi_id='{treeNode2.Name}' AND si_worker_id='{UserHelper.GetInstance().User.UserKey}'", 4);
                        for(int j = 0; j < list2.Count; j++)
                        {
                            TreeNode treeNode3 = new TreeNode()
                            {
                                Name = GetValue(list2[j][0]),
                                Text = GetValue(list2[j][1]),
                                Tag = (ControlType)list2[j][2]
                            };
                            //如果当前任务并非登录人加工，则无法编辑【文字置灰】
                            if(!UserHelper.GetInstance().User.UserKey.Equals(list2[j][3]))
                                treeNode3.ForeColor = DisEnbleColor;
                            //【当前定位为课题子课题，则只取一条即可】
                            if(treeNode3.Name.Equals(OBJECT_ID))
                            {
                                treeNode.Nodes.Add(treeNode2);
                                treeNode2.Nodes.Add(treeNode3);
                                List<object[]> list3 = SqlHelper.ExecuteColumnsQuery($"SELECT si_id,si_code,si_categor FROM subject_info WHERE pi_id='{treeNode3.Name}' AND si_worker_id='{UserHelper.GetInstance().User.UserKey}'", 3);
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
                treeView.Nodes.Add(treeNode);
                //默认加载计划页面
                if(treeView.Nodes.Count > 0)
                {
                    TreeNode node = treeView.Nodes[0];
                    ShowTab("plan", 0);
                    LoadPlanPage(node.Name, node.ForeColor);
                }
            }
            treeView.ExpandAll();
            treeView.NodeMouseClick += TreeView_NodeMouseClick;
            treeView.EndUpdate();
        }
        
        /// <summary>
        /// 目录树点击事件
        /// </summary>
        private void TreeView_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            ControlType type = (ControlType)e.Node.Tag;
            if(type == ControlType.Plan)
            {
                ShowTab("plan", 0);
                LoadPlanPage(e.Node.Name, e.Node.ForeColor);
            }
            else if(type == ControlType.Plan)
            {
                tab_MenuList.TabPages.Clear();
                if(workType == WorkType.Default)
                {
                    int count = SqlHelper.ExecuteCountQuery($"SELECT COUNT(pi_id) FROM project_info WHERE pi_id='{e.Node.Parent.Name}'");
                    if(count == 0)
                    {
                        ShowTab("imp", 0);
                        LoadImpPage(e.Node.Parent.Parent.Name, e.Node.Parent.Parent.ForeColor);

                        ShowTab("Special", 1);
                        LoadPageBasicInfo(e.Node.Parent.Name, ControlType.Special, e.Node.Parent.ForeColor);

                        ShowTab("plan_project", 2);
                        LoadPageBasicInfo(e.Node.Name, type, e.Node.ForeColor);
                    }
                    else
                    {
                        ShowTab("plan", 0);
                        LoadPlanPage(e.Node.Parent.Name, e.Node.Parent.ForeColor);

                        ShowTab("plan_project", 1);
                        LoadPageBasicInfo(e.Node.Name, ControlType.Plan, e.Node.ForeColor);
                    }
                }
                else if(workType == WorkType.CDWork || workType == WorkType.PaperWork)
                {
                    ShowTab("plan", 0);
                    LoadPlanPage(e.Node.Parent.Name, e.Node.Parent.ForeColor);

                    ShowTab("plan_project", 1);
                    LoadPageBasicInfo(e.Node.Name, type, e.Node.ForeColor);
                }
                else if(workType == WorkType.ProjectWork)
                {
                    ShowTab("plan", 0);
                    LoadPlanPage(e.Node.Parent.Name, e.Node.Parent.ForeColor);

                    ShowTab("plan_project", 1);
                    LoadPageBasicInfo(e.Node.Name, type, e.Node.ForeColor);
                }
                else if(workType == WorkType.SubjectWork)
                {

                    ShowTab("plan", 0);
                    LoadPlanPage(e.Node.Parent.Name, e.Node.ForeColor);

                    ShowTab("plan_project", 1);
                    LoadPageBasicInfo(e.Node.Name, type, e.Node.ForeColor);
                }
            }
            else if(type == ControlType.Topic)
            {
                tab_MenuList.TabPages.Clear();
                if(workType == WorkType.Default)
                {
                    ShowTab("imp", 0);
                    LoadImpPage(e.Node.Parent.Parent.Name, e.Node.Parent.Parent.ForeColor);

                    ShowTab("Special", 1);
                    LoadPageBasicInfo(e.Node.Parent.Name, ControlType.Special, e.Node.Parent.ForeColor);

                    ShowTab("plan_topic", 2);
                    LoadPageBasicInfo(e.Node.Name, type, e.Node.ForeColor);
                }
                else if(workType == WorkType.CDWork || workType == WorkType.PaperWork)
                {
                    ShowTab("plan", 0);
                    LoadPlanPage(e.Node.Parent.Name, e.Node.Parent.ForeColor);

                    ShowTab("plan_topic", 1);
                    LoadPageBasicInfo(e.Node.Name, type, e.Node.ForeColor);
                }
            }
            else if(type == ControlType.Topic)
            {
                tab_MenuList.TabPages.Clear();
                if(workType == WorkType.Default)
                {
                    int count = SqlHelper.ExecuteCountQuery($"SELECT COUNT(pi_id) FROM project_info WHERE pi_id='{e.Node.Parent.Parent.Name}'");
                    if(count == 0)
                    {
                        ShowTab("imp", 0);
                        LoadImpPage(e.Node.Parent.Parent.Parent.Name, e.Node.Parent.Parent.Parent.ForeColor);

                        ShowTab("Special", 1);
                        LoadPageBasicInfo(e.Node.Parent.Parent.Name, ControlType.Special, e.Node.Parent.Parent.ForeColor);

                        ShowTab("plan_project", 2);
                        LoadPageBasicInfo(e.Node.Parent.Name, ControlType.Plan, e.Node.Parent.ForeColor);

                        ShowTab("Topic", 3);
                        LoadPageBasicInfo(e.Node.Name, ControlType.Topic, e.Node.ForeColor);
                    }
                    else
                    {
                        ShowTab("plan", 0);
                        LoadPlanPage(e.Node.Parent.Parent.Name, e.Node.Parent.Parent.ForeColor);

                        ShowTab("plan_project", 1);
                        LoadPageBasicInfo(e.Node.Parent.Name, ControlType.Plan, e.Node.Parent.ForeColor);

                        ShowTab("Topic", 2);
                        LoadPageBasicInfo(e.Node.Name, type, e.Node.ForeColor);
                    }
                }
                else if(workType == WorkType.CDWork || workType == WorkType.PaperWork)
                {
                    ShowTab("plan", 0);
                    LoadPlanPage(e.Node.Parent.Parent.Name, e.Node.Parent.Parent.ForeColor);

                    ShowTab("plan_project", 1);
                    LoadPageBasicInfo(e.Node.Parent.Name, ControlType.Plan, e.Node.Parent.ForeColor);

                    ShowTab("Topic", 2);
                    LoadPageBasicInfo(e.Node.Name, ControlType.Topic, e.Node.ForeColor);
                }
                else if(workType == WorkType.ProjectWork || workType == WorkType.SubjectWork)
                {
                    ShowTab("plan", 0);
                    LoadPlanPage(e.Node.Parent.Parent.Name, e.Node.Parent.Parent.ForeColor);

                    ShowTab("plan_project", 1);
                    LoadPageBasicInfo(e.Node.Parent.Name, ControlType.Plan, e.Node.Parent.ForeColor);

                    ShowTab("Topic", 2);
                    LoadPageBasicInfo(e.Node.Name, ControlType.Topic, e.Node.ForeColor);
                }
            }
            else if(type == ControlType.Subject)
            {
                tab_MenuList.TabPages.Clear();
                if(workType == WorkType.Default)
                {
                    ShowTab("imp", 0);
                    LoadImpPage(e.Node.Parent.Parent.Parent.Name, e.Node.Parent.Parent.Parent.ForeColor);

                    ShowTab("Special", 1);
                    LoadPageBasicInfo(e.Node.Parent.Parent.Name, ControlType.Special, e.Node.Parent.Parent.ForeColor);

                    ShowTab("plan_topic", 2);
                    LoadPageBasicInfo(e.Node.Parent.Name, ControlType.Topic, e.Node.Parent.ForeColor);

                    ShowTab("plan_topic_subtopic", 3);
                    LoadPageBasicInfo(e.Node.Name, ControlType.Subject, e.Node.ForeColor);
                }
                else if(workType == WorkType.CDWork || workType == WorkType.PaperWork)
                {

                    ShowTab("plan", 0);
                    LoadPlanPage(e.Node.Parent.Parent.Name, e.Node.Parent.Parent.ForeColor);

                    ShowTab("plan_topic", 1);
                    LoadPageBasicInfo(e.Node.Parent.Name, ControlType.Topic, e.Node.Parent.ForeColor);

                    ShowTab("plan_topic_subtopic", 2);
                    LoadPageBasicInfo(e.Node.Name, type, e.Node.ForeColor);
                }
            }
            else if(type == ControlType.Subject)
            {
                if(workType == WorkType.Default)
                {
                    int count = SqlHelper.ExecuteCountQuery($"SELECT COUNT(pi_id) FROM project_info WHERE pi_id='{e.Node.Parent.Parent.Parent.Name}'");
                    if(count == 0)
                    {
                        ShowTab("imp", 0);
                        LoadImpPage(e.Node.Parent.Parent.Parent.Parent.Name, e.Node.Parent.Parent.Parent.Parent.ForeColor);

                        ShowTab("Special", 1);
                        LoadPageBasicInfo(e.Node.Parent.Parent.Parent.Name, ControlType.Special, e.Node.Parent.Parent.Parent.ForeColor);

                        ShowTab("plan_project", 2);
                        LoadPageBasicInfo(e.Node.Parent.Parent.Name, ControlType.Plan, e.Node.Parent.Parent.ForeColor);

                        ShowTab("Topic", 3);
                        LoadPageBasicInfo(e.Node.Parent.Name, ControlType.Topic, e.Node.Parent.ForeColor);

                        ShowTab("Subject", 4);
                        LoadPageBasicInfo(e.Node.Name, ControlType.Subject, e.Node.ForeColor);
                    }
                    else
                    {
                        ShowTab("plan", 0);
                        LoadPlanPage(e.Node.Parent.Parent.Parent.Name, e.Node.Parent.Parent.Parent.ForeColor);

                        ShowTab("plan_project", 1);
                        LoadPageBasicInfo(e.Node.Parent.Parent.Name, ControlType.Plan, e.Node.Parent.Parent.ForeColor);

                        ShowTab("Topic", 2);
                        LoadPageBasicInfo(e.Node.Parent.Name, ControlType.Topic, e.Node.Parent.ForeColor);

                        ShowTab("Subject", 3);
                        LoadPageBasicInfo(e.Node.Name, ControlType.Subject, e.Node.ForeColor);
                    }
                }
                else
                {
                    tab_MenuList.TabPages.Clear();

                    ShowTab("plan", 0);
                    LoadPlanPage(e.Node.Parent.Parent.Parent.Name, e.Node.Parent.Parent.Parent.ForeColor);

                    ShowTab("plan_project", 1);
                    LoadPageBasicInfo(e.Node.Parent.Parent.Name, ControlType.Plan,e.Node.Parent.Parent.ForeColor);

                    ShowTab("Topic", 2);
                    LoadPageBasicInfo(e.Node.Parent.Name, ControlType.Topic,e.Node.Parent.ForeColor);

                    ShowTab("Subject", 3);
                    LoadPageBasicInfo(e.Node.Name, type, e.Node.ForeColor);
                }
            }
            else if(type == ControlType.Imp || type == ControlType.Special)
            {
                tab_MenuList.TabPages.Clear();

                ShowTab("imp", 0);
                LoadImpPage(e.Node.Name, e.Node.ForeColor);

            }
            else if(type == ControlType.Special)
            {
                tab_MenuList.TabPages.Clear();

                ShowTab("imp", 0);
                LoadImpPage(e.Node.Parent.Name, e.Node.Parent.ForeColor);

                ShowTab("Special", 1);
                LoadPageBasicInfo(e.Node.Name, type, e.Node.ForeColor);
            }
            else if(type == ControlType.Special)
            {
                tab_MenuList.TabPages.Clear();

                ShowTab("plan", 0);
                LoadPlanPage(e.Node.Name, System.Drawing.Color.Black);
            }
        }
      
        /// <summary>
        /// 加载Imp/Dev基本信息
        /// </summary>
        private void LoadImpPage(object objId, Color color)
        {
            object[] _obj = SqlHelper.ExecuteRowsQuery($"SELECT imp_id, imp_name, imp_intro, imp_submit_status FROM imp_info WHERE imp_id='{objId}'");
            if(_obj == null)
                _obj = SqlHelper.ExecuteRowsQuery($"SELECT dd_id, dd_name, dd_note FROM data_dictionary WHERE dd_id='{objId}'");
            else
            {
                if((ObjectSubmitStatus)_obj[3] == ObjectSubmitStatus.SubmitSuccess)
                    EnableControls(ControlType.Imp, false);
                tab_Imp_Info.Tag = GetValue(_obj[0]);
                LoadFileList(dgv_Imp_FileList, "imp_fl_", GetValue(_obj[0]));
            }
            if(_obj != null)
            {
                lbl_Imp_Name.Tag = GetValue(_obj[0]);
                lbl_Imp_Name.Text = GetValue(_obj[1]);
                txt_Imp_Intro.Text = GetValue(_obj[2]);
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
                DataTable table = SqlHelper.ExecuteQuery($"SELECT * FROM data_dictionary WHERE dd_pId=(SELECT dd_id FROM data_dictionary WHERE dd_code='{key}') ORDER BY dd_sort");
                cbo_Imp_HasNext.DataSource = table;
                cbo_Imp_HasNext.DisplayMember = "dd_name";
                cbo_Imp_HasNext.ValueMember = "dd_id";
            }
            dgv_Imp_FileList.ColumnHeadersDefaultCellStyle = DataGridViewStyleHelper.GetHeaderStyle();
            dgv_Imp_FileList.DefaultCellStyle = DataGridViewStyleHelper.GetCellStyle();
            dgv_Imp_FileValid.ColumnHeadersDefaultCellStyle = DataGridViewStyleHelper.GetHeaderStyle();
            dgv_Imp_FileValid.DefaultCellStyle = DataGridViewStyleHelper.GetCellStyle();

            tab_MenuList.TabPages["imp"].Tag = objId;

            //如果是质检返工则加载意见数
            if(isBacked)
            {
                btn_Imp_QTReason.Text = $"质检意见({GetAdvincesAmount(dgv_Imp_FileList.Tag)})";
            }
            if(color == DisEnbleColor)
            {
                pal_Imp_BtnGroup.Enabled = false;
            }
        }

        private int GetAdvincesAmount(object objId)
        {
            return SqlHelper.ExecuteCountQuery($"SELECT COUNT(qa_id) FROM quality_advices a " +
                $"WHERE qa_time = (SELECT MAX(qa_time) FROM quality_advices WHERE qa_obj_id = a.qa_obj_id AND qa_type = a.qa_type) " +
                $"AND qa_obj_id='{objId}'");
        }

        /// <summary>
        /// 文件信息选项卡切换事件
        /// </summary>
        private void Tab_FileInfo_SelectedIndexChanged(object sender, EventArgs e)
        {
            KyoTabControl tab = sender as KyoTabControl;
            int index = tab.SelectedTabPageIndex;
            if("tab_JH_FileInfo".Equals(tab.Name))
            {
                object objid = dgv_Plan_FileList.Tag;
                btn_Plan_AddFile.Visible =  false;
                if(index == 0)
                    btn_Plan_AddFile.Visible =  true;
                else if(index == 1)//文件核查
                {
                    if(objid != null)
                    {
                        LoadFileValidList(dgv_JH_FileValid, objid, "plan_fc_");
                    }
                }
                else if(index == 2)
                {
                    if(objid != null)
                    {
                        object code = SqlHelper.ExecuteOnlyOneQuery($"SELECT pt_code FROM processing_tag WHERE pt_obj_id='{objid}'") ?? string.Empty;
                        object name = SqlHelper.ExecuteOnlyOneQuery($"SELECT pt_name FROM processing_tag WHERE pt_obj_id='{objid}'") ?? string.Empty;
                        txt_Plan_AJ_Code.Text = GetValue(code);
                        txt_Plan_AJ_Name.Text = GetValue(name);
                    }
                    LoadBoxList(dgv_Plan_FileList.Tag, ControlType.Plan);
                    LoadFileBoxTable(cbo_JH_Box.SelectedValue, dgv_Plan_FileList.Tag, ControlType.Plan);
                }
            }
            else if("tab_JH_XM_FileInfo".Equals(tab.Name))
            {
                object objid = dgv_Project_FileList.Tag;
                btn_Project_AddFile.Visible = false;
                if(index == 0)
                    btn_Project_AddFile.Visible = true;
                else if(index == 1)//文件核查
                {
                    if(objid != null)
                    {
                        LoadFileValidList(dgv_JH_XM_FileValid, objid, "project_fc_");
                    }
                }
                else if(index == 2)
                {
                    if(objid != null)
                    {
                        object code = SqlHelper.ExecuteOnlyOneQuery($"SELECT pt_code FROM processing_tag WHERE pt_obj_id='{objid}'") ?? string.Empty;
                        object name = SqlHelper.ExecuteOnlyOneQuery($"SELECT pt_name FROM processing_tag WHERE pt_obj_id='{objid}'") ?? string.Empty;
                        txt_Project_AJ_Code.Text = GetValue(code);
                        txt_Project_AJ_Name.Text = GetValue(name);
                    }
                    LoadBoxList(dgv_Project_FileList.Tag, ControlType.Plan);
                    LoadFileBoxTable(cbo_JH_XM_Box.SelectedValue, dgv_Project_FileList.Tag, ControlType.Plan);
                }
            }
            else if("tab_JH_KT_FileInfo".Equals(tab.Name))
            {
                object objid = dgv_Topic_FileList.Tag;
                btn_Topic_AddFile.Visible = false;
                if(index == 0)
                    btn_Topic_AddFile.Visible = true;
                else if(index == 1)//文件核查
                {
                    if(objid != null)
                    {
                        LoadFileValidList(dgv_JH_KT_FileValid, objid, "topic_fc_");
                    }
                }
                else if(index == 2)
                {
                    if(objid != null)
                    {
                        object code = SqlHelper.ExecuteOnlyOneQuery($"SELECT pt_code FROM processing_tag WHERE pt_obj_id='{objid}'") ?? string.Empty;
                        object name = SqlHelper.ExecuteOnlyOneQuery($"SELECT pt_name FROM processing_tag WHERE pt_obj_id='{objid}'") ?? string.Empty;
                        txt_Topic_AJ_Code.Text = GetValue(code);
                        txt_Topic_AJ_Name.Text = GetValue(name);
                    }
                    LoadBoxList(dgv_Topic_FileList.Tag, ControlType.Topic);
                    LoadFileBoxTable(cbo_JH_KT_Box.SelectedValue, dgv_Topic_FileList.Tag, ControlType.Topic);
                }
            }
            else if("tab_JH_XM_KT_ZKT_FileInfo".Equals(tab.Name))
            {
                object objid = dgv_Subject_FileList.Tag;
                btn_Subject_AddFile.Visible = false;
                if(index == 0)
                    btn_Subject_AddFile.Visible = true;
                else if(index == 1)//文件核查
                {
                    if(objid != null)
                    {
                        LoadFileValidList(dgv_JH_XM_KT_ZKT_FileValid, objid, "subject_fc_");
                    }
                }
                else if(index == 2)
                {
                    if(objid != null)
                    {
                        object code = SqlHelper.ExecuteOnlyOneQuery($"SELECT pt_code FROM processing_tag WHERE pt_obj_id='{objid}'") ?? string.Empty;
                        object name = SqlHelper.ExecuteOnlyOneQuery($"SELECT pt_name FROM processing_tag WHERE pt_obj_id='{objid}'") ?? string.Empty;
                        txt_Subject_AJ_Code.Text = GetValue(code);
                        txt_Subject_AJ_Name.Text = GetValue(name);
                    }
                    LoadBoxList(dgv_Subject_FileList.Tag, ControlType.Subject);
                    LoadFileBoxTable(cbo_JH_XM_KT_ZKT_Box.SelectedValue, dgv_Subject_FileList.Tag, ControlType.Subject);
                }
            }
            else if("tab_Imp_FileInfo".Equals(tab.Name))
            {
                object objid = dgv_Imp_FileList.Tag;
                btn_Imp_AddFile.Visible = false;
                if(index == 0)
                    btn_Imp_AddFile.Visible = true;
                else if(index == 1)//文件核查
                {
                    if(objid != null)
                    {
                        LoadFileValidList(dgv_Imp_FileValid, objid, "imp_fc_");
                    }
                }
                else if(index == 2)
                {
                    if(objid != null)
                    {
                        object code = SqlHelper.ExecuteOnlyOneQuery($"SELECT pt_code FROM processing_tag WHERE pt_obj_id='{objid}'") ?? string.Empty;
                        object name = SqlHelper.ExecuteOnlyOneQuery($"SELECT pt_name FROM processing_tag WHERE pt_obj_id='{objid}'") ?? string.Empty;
                        txt_Imp_AJ_Code.Text = GetValue(code);
                        txt_Imp_AJ_Name.Text = GetValue(name);
                    }
                    LoadBoxList(dgv_Imp_FileList.Tag, ControlType.Imp);
                    LoadFileBoxTable(cbo_Imp_Box.SelectedValue, dgv_Imp_FileList.Tag, ControlType.Imp);
                }
            }
            else if("tab_Imp_Dev_FileInfo".Equals(tab.Name))
            {
                object objid = dgv_Special_FileList.Tag;
                btn_Special_AddFile.Visible = false;
                if(index == 0)
                    btn_Special_AddFile.Visible = true;
                else if(index == 1)//文件核查
                {
                    if(objid != null)
                    {
                        LoadFileValidList(dgv_Special_FileValid, objid, "special_fc_");
                    }
                }
                else if(index == 2)
                {
                    if(objid != null)
                    {
                        object code = SqlHelper.ExecuteOnlyOneQuery($"SELECT pt_code FROM processing_tag WHERE pt_obj_id='{objid}'") ?? string.Empty;
                        object name = SqlHelper.ExecuteOnlyOneQuery($"SELECT pt_name FROM processing_tag WHERE pt_obj_id='{objid}'") ?? string.Empty;
                        txt_Special_AJ_Code.Text = GetValue(code);
                        txt_Special_AJ_Name.Text = GetValue(name);
                    }
                    LoadBoxList(dgv_Special_FileList.Tag, ControlType.Special);
                    LoadFileBoxTable(cbo_Imp_Dev_Box.SelectedValue, dgv_Special_FileList.Tag, ControlType.Special);
                }
            }
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
        private string GetMaxSecretById(object objid) => GetValue(SqlHelper.ExecuteOnlyOneQuery($"SELECT TOP(1) dd_name FROM processing_file_list LEFT JOIN data_dictionary ON pfl_scert = dd_id WHERE pfl_obj_id = '{objid}' ORDER BY dd_sort DESC"));
  
        /// <summary>
        /// 加载文件缺失校验列表
        /// </summary>
        /// <param name="dataGridView">待校验表格</param>
        /// <param name="objid">主键</param>
        private void LoadFileValidList(DataGridView dataGridView, object objid, string key)
        {
            dataGridView.Rows.Clear();

            string querySql = "select dd_name,dd_note from data_dictionary where dd_pId in(" +
                "select dd_id from data_dictionary where dd_pId = (" +
                "select dd_id from data_dictionary  where dd_code = 'dic_file_jd')) and dd_name not in(" +
                $"select dd.dd_name from processing_file_list pfl left join data_dictionary dd on pfl.pfl_categor = dd.dd_id where pfl.pfl_obj_id='{objid}')" +
                $" ORDER BY dd_name";
            DataTable table = SqlHelper.ExecuteQuery(querySql);
            for (int i = 0; i < table.Rows.Count; i++)
            {
                int indexRow = dataGridView.Rows.Add();
                dataGridView.Rows[indexRow].Cells[key + "id"].Value = i + 1;
                dataGridView.Rows[indexRow].Cells[key + "categor"].Value = table.Rows[i]["dd_name"];
                dataGridView.Rows[indexRow].Cells[key + "name"].Value = table.Rows[i]["dd_note"];

                string queryReasonSql = $"SELECT pfo_id, pfo_reason, pfo_remark FROM processing_file_lost WHERE pfo_obj_id='{objid}' AND pfo_categor='{table.Rows[i]["dd_name"]}'";
                object[] _obj = SqlHelper.ExecuteRowsQuery(queryReasonSql);
                if(_obj != null)
                {
                    dataGridView.Rows[indexRow].Cells[key + "id"].Tag = GetValue(_obj[0]);
                    dataGridView.Rows[indexRow].Cells[key + "reason"].Value = GetValue(_obj[1]);
                    dataGridView.Rows[indexRow].Cells[key + "remark"].Value = GetValue(_obj[2]);
                }
            }


            List<KeyValuePair<string, int>> list = new List<KeyValuePair<string, int>>();
            list.Add(new KeyValuePair<string, int>(key + "id", 50));
            list.Add(new KeyValuePair<string, int>(key + "reason", 100));
            list.Add(new KeyValuePair<string, int>(key + "categor", 80));
            list.Add(new KeyValuePair<string, int>(key + "name", 250));
            DataGridViewStyleHelper.SetWidth(dataGridView, list);

            DataGridViewStyleHelper.SetAlignWithCenter(dataGridView, new string[] { key + "id", key + "categor" });
        }
    
        /// <summary>
        /// 加载计划-案卷盒归档表
        /// </summary>
        /// <param name="pbId">案卷盒ID</param>
        /// <param name="objId">所属对象ID</param>
        /// <param name="type">对象类型</param>
        private void LoadFileBoxTable(object pbId, object objId, ControlType type)
        {
            string GCID = GetValue(SqlHelper.ExecuteOnlyOneQuery($"SELECT pb_gc_id FROM processing_box WHERE pb_id='{pbId}'"));
            if(type == ControlType.Plan)
            {
                txt_JH_Box_GCID.Text = GCID;
                LoadFileBoxTableInstance(lsv_JH_File1,lsv_JH_File2, "jh", pbId, objId);
            }
            else if(type == ControlType.Plan)
            {
                txt_JH_XM_Box_GCID.Text = GCID;
                LoadFileBoxTableInstance(lsv_JH_XM_File1, lsv_JH_XM_File2, "jh_xm", pbId, objId);
            }
            else if(type == ControlType.Topic)
            {
                txt_JH_KT_Box_GCID.Text = GCID;
                LoadFileBoxTableInstance(lsv_JH_KT_File1, lsv_JH_KT_File2, "jh_kt", pbId, objId);
            }
            else if(type == ControlType.Subject)
            {
                txt_JH_XM_KT_ZKT_Box_GCID.Text = GCID;
                LoadFileBoxTableInstance(lsv_JH_XM_KT_ZKT_File1, lsv_JH_XM_KT_ZKT_File2, "jh_xm_kt_zkt", pbId, objId);
            }
            else if(type == ControlType.Imp)
            {
                txt_Imp_Box_GCID.Text = GCID;
                LoadFileBoxTableInstance(lsv_Imp_File1, lsv_Imp_File2, "imp", pbId, objId);
            }
            else if(type == ControlType.Special)
            {
                txt_Imp_Dev_Box_GCID.Text = GCID;
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
                    new ColumnHeader{ Name = $"{key}_file1_type", Text = "文件类别", TextAlign = HorizontalAlignment.Center ,Width = 75},
                    new ColumnHeader{ Name = $"{key}_file1_name", Text = "文件名称", Width = 250},
                    new ColumnHeader{ Name = $"{key}_file1_date", Text = "形成日期", Width = 100}
            });
            rightView.Columns.AddRange(new ColumnHeader[]
            {
                    new ColumnHeader{ Name = $"{key}_file2_id", Text = "主键", Width = 0},
                    new ColumnHeader{ Name = $"{key}_file2_type", Text = "文件类别", TextAlign = HorizontalAlignment.Center ,Width = 75},
                    new ColumnHeader{ Name = $"{key}_file2_name", Text = "文件名称", Width = 250},
                    new ColumnHeader{ Name = $"{key}_file2_date", Text = "形成日期", Width = 100}
            });
            //未归档
            string querySql = $"SELECT pfl_id, dd_name, pfl_filename, pfl_complete_date FROM processing_file_list LEFT JOIN data_dictionary " +
                $"ON pfl_categor=dd_id WHERE pfl_obj_id = '{objId}' AND pfl_status={(int)GuiDangStatus.NonGuiDang} ORDER BY pfl_complete_date";
            DataTable dataTable = SqlHelper.ExecuteQuery(querySql);
            for(int i = 0; i < dataTable.Rows.Count; i++)
            {
                ListViewItem item = leftView.Items.Add(GetValue(dataTable.Rows[i]["pfl_id"]));
                item.SubItems.AddRange(new ListViewItem.ListViewSubItem[]
                {
                        new ListViewItem.ListViewSubItem(){ Text = GetValue(dataTable.Rows[i]["dd_name"]) },
                        new ListViewItem.ListViewSubItem(){ Text = GetValue(dataTable.Rows[i]["pfl_filename"]) },
                        new ListViewItem.ListViewSubItem(){ Text = GetDateValue(dataTable.Rows[i]["pfl_complete_date"], "yyyy-MM-dd") },
                });
            }
            //已归档
            object id = SqlHelper.ExecuteOnlyOneQuery($"SELECT pb_files_id FROM processing_box WHERE pb_id = '{pbId}'");
            if(id != null)
            {
                querySql = $"SELECT pfl_id, dd_name, pfl_filename, pfl_complete_date FROM processing_file_list LEFT JOIN data_dictionary ON pfl_categor=dd_id WHERE pfl_id IN(";
                string[] ids = GetValue(id).Split(',');
                string sortString = null;
                for(int i = 0; i < ids.Length; i++)
                {
                    querySql += "'" + ids[i] + "'" + (i == ids.Length - 1 ? ")" : ",");
                    sortString += $"{ids[i]}{(i == ids.Length - 1 ? string.Empty : ",")}";
                }
                querySql += $" ORDER BY CHARINDEX(pfl_id,'{sortString}')";
                DataTable _dataTable = SqlHelper.ExecuteQuery(querySql);
                for(int i = 0; i < _dataTable.Rows.Count; i++)
                {
                    ListViewItem item = rightView.Items.Add(GetValue(_dataTable.Rows[i]["pfl_id"]));
                    item.SubItems.AddRange(new ListViewItem.ListViewSubItem[]
                    {
                        new ListViewItem.ListViewSubItem(){ Text = GetValue(_dataTable.Rows[i]["dd_name"]) },
                        new ListViewItem.ListViewSubItem(){ Text = GetValue(_dataTable.Rows[i]["pfl_filename"]) },
                        new ListViewItem.ListViewSubItem(){ Text = GetDateValue(_dataTable.Rows[i]["pfl_complete_date"], "yyyy-MM-dd") },
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
            string _formatDate = null, value = GetValue(date);
            if(!string.IsNullOrEmpty(value))
                _formatDate = Convert.ToDateTime(value).ToString(format);
            return _formatDate;
        }
    
        /// <summary>
        /// 将object对象转换成string
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        private string GetValue(object obj) => obj == null ? string.Empty : obj.ToString();
    
        /// <summary>
        /// 案卷归档事件
        /// </summary>
        private void Btn_Box_Click(object sender, EventArgs e)
        {
            KyoButton button = sender as KyoButton;
            //计划
            if (button.Name.Contains("btn_JH_Box"))
            {
                object value = cbo_JH_Box.SelectedValue;
                if(value != null)
                {
                    if("btn_JH_Box_Right".Equals(button.Name))
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
                    else if("btn_JH_Box_RightAll".Equals(button.Name))
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
                    else if("btn_JH_Box_Left".Equals(button.Name))
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
                    else if("btn_JH_Box_LeftAll".Equals(button.Name))
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
                    LoadFileBoxTable(value, dgv_Plan_FileList.Tag, ControlType.Plan);
                }
                else
                    MessageBox.Show("请先添加案卷盒。", "保存失败");
            }
            //计划-项目
            else if(button.Name.Contains("btn_JH_XM_Box"))
            {
                object value = cbo_JH_XM_Box.SelectedValue;
                if(value != null)
                {
                    if("btn_JH_XM_Box_Right".Equals(button.Name))
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
                    else if("btn_JH_XM_Box_RightAll".Equals(button.Name))
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
                    else if("btn_JH_XM_Box_Left".Equals(button.Name))
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
                    else if("btn_JH_XM_Box_LeftAll".Equals(button.Name))
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
                    LoadFileBoxTable(value, dgv_Project_FileList.Tag, ControlType.Plan);
                }
                else
                    MessageBox.Show("请先添加案卷盒。", "保存失败");
            }
            //计划-课题
            else if(button.Name.Contains("btn_JH_KT_Box"))
            {
                object value = cbo_JH_KT_Box.SelectedValue;
                if(value != null)
                {
                    if("btn_JH_KT_Box_Right".Equals(button.Name))
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
                    else if("btn_JH_KT_Box_RightAll".Equals(button.Name))
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
                    else if("btn_JH_KT_Box_Left".Equals(button.Name))
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
                    else if("btn_JH_KT_Box_LeftAll".Equals(button.Name))
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
                    LoadFileBoxTable(value, dgv_Topic_FileList.Tag, ControlType.Topic);
                }
                else
                    MessageBox.Show("请先添加案卷盒。", "保存失败");
            }
            //计划-项目-课题-子课题
            else if(button.Name.Contains("btn_JH_XM_KT_ZKT_Box"))
            {
                object value = cbo_JH_XM_KT_ZKT_Box.SelectedValue;
                if(value != null)
                {
                    if("btn_JH_XM_KT_ZKT_Box_Right".Equals(button.Name))
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
                    else if("btn_JH_XM_KT_ZKT_Box_RightAll".Equals(button.Name))
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
                    else if("btn_JH_XM_KT_ZKT_Box_Left".Equals(button.Name))
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
                    else if("btn_JH_XM_KT_ZKT_Box_LeftAll".Equals(button.Name))
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
                    LoadFileBoxTable(value, dgv_Subject_FileList.Tag, ControlType.Subject);
                }
                else
                    MessageBox.Show("请先添加案卷盒。", "保存失败");
            }
            //重大专项/研发
            else if(button.Name.Contains("btn_Imp_Box"))
            {
                object value = cbo_Imp_Box.SelectedValue;
                if(value != null)
                {
                    if("btn_Imp_Box_Right".Equals(button.Name))
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
                    else if("btn_Imp_Box_RightAll".Equals(button.Name))
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
                    else if("btn_Imp_Box_Left".Equals(button.Name))
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
                    else if("btn_Imp_Box_LeftAll".Equals(button.Name))
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
                    LoadFileBoxTable(value, dgv_Imp_FileList.Tag, ControlType.Imp);
                }
                else
                    MessageBox.Show("请先添加案卷盒。", "保存失败");
            }
            //重大专项/研发 - 信息
            else if(button.Name.Contains("btn_Imp_Dev_Box"))
            {
                object value = cbo_Imp_Dev_Box.SelectedValue;
                if(value != null)
                {
                    if("btn_Imp_Dev_Box_Right".Equals(button.Name))
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
                    else if("btn_Imp_Dev_Box_RightAll".Equals(button.Name))
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
                    else if("btn_Imp_Dev_Box_Left".Equals(button.Name))
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
                    else if("btn_Imp_Dev_Box_LeftAll".Equals(button.Name))
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
                    LoadFileBoxTable(value, dgv_Special_FileList.Tag, ControlType.Special);
                }
                else
                    MessageBox.Show("请先添加案卷盒。", "保存失败");
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
            if(label.Name.Contains("lbl_JH_Box"))
            {
                object objId = dgv_Plan_FileList.Tag;
                if(objId != null)
                {
                    if("lbl_JH_Box_Add".Equals(label.Name))//新增
                    {
                        //当前已有盒号数量
                        int amount = Convert.ToInt32(SqlHelper.ExecuteOnlyOneQuery($"SELECT COUNT(pb_box_number) FROM processing_box WHERE pb_obj_id='{objId}'"));
                        string gch = $"{unitCode}{GetGCAmount(unitCode)}";
                        string insertSql = $"INSERT INTO processing_box VALUES('{Guid.NewGuid().ToString()}','{amount + 1}','{gch}',null,'{objId}','{unitCode}')";
                        SqlHelper.ExecuteNonQuery(insertSql);
                    }
                    else if("lbl_JH_Box_Remove".Equals(label.Name))//删除
                    {
                        object _temp = SqlHelper.ExecuteOnlyOneQuery($"SELECT MAX(pb_box_number) FROM processing_box WHERE pb_obj_id='{objId}'");
                        if(_temp != null)
                        {
                            int currentBoxId = Convert.ToInt32(SqlHelper.ExecuteOnlyOneQuery($"SELECT pb_box_number FROM processing_box WHERE pb_id='{cbo_JH_Box.SelectedValue}'"));
                            if(Convert.ToInt32(_temp) > currentBoxId)
                                MessageBox.Show("请先删除较大盒号。", "删除失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            else if(MessageBox.Show("删除当前案卷盒会清空盒下已归档的文件，是否继续？", "删除确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                object value = cbo_JH_Box.SelectedValue;
                                if(value != null)
                                {
                                    //将当前盒中文件状态致为未归档
                                    object ids = SqlHelper.ExecuteOnlyOneQuery($"SELECT pb_files_id FROM processing_box WHERE pb_obj_id='{objId}' AND pb_id='{value}'");
                                    string[] _ids = ids.ToString().Split(',');
                                    StringBuilder sb = new StringBuilder($"UPDATE processing_file_list SET pfl_status={(int)GuiDangStatus.NonGuiDang} WHERE pfl_id IN(");
                                    for(int i = 0; i < _ids.Length; i++)
                                        sb.Append($"'{_ids[i]}'{(_ids.Length - 1 != i ? "," : ")")}");
                                    SqlHelper.ExecuteNonQuery(sb.ToString());

                                    //删除当前盒信息
                                    SqlHelper.ExecuteNonQuery($"DELETE FROM processing_box WHERE pb_id='{value}'");
                                }
                            }
                        }
                    }
                    LoadBoxList(objId, ControlType.Plan);
                    LoadFileBoxTable(cbo_JH_Box.SelectedValue, objId, ControlType.Plan);
                }
            }
            //计划-项目
            if(label.Name.Contains("lbl_JH_XM_Box"))
            {
                object objId = dgv_Project_FileList.Tag;
                if(objId != null)
                {
                    if("lbl_JH_XM_Box_Add".Equals(label.Name))//新增
                    {
                        //当前已有盒号数量
                        int amount = Convert.ToInt32(SqlHelper.ExecuteOnlyOneQuery($"SELECT COUNT(pb_box_number) FROM processing_box WHERE pb_obj_id='{objId}'"));
                        string gch = $"{unitCode}{GetGCAmount(unitCode)}";
                        string insertSql = $"INSERT INTO processing_box VALUES('{Guid.NewGuid().ToString()}','{amount + 1}','{gch}',null,'{objId}','{unitCode}')";
                        SqlHelper.ExecuteNonQuery(insertSql);
                    }
                    else if("lbl_JH_XM_Box_Remove".Equals(label.Name))//删除
                    {
                        object _temp = SqlHelper.ExecuteOnlyOneQuery($"SELECT MAX(pb_box_number) FROM processing_box WHERE pb_obj_id='{objId}'");
                        if(_temp != null)
                        {
                            int currentBoxId = Convert.ToInt32(SqlHelper.ExecuteOnlyOneQuery($"SELECT pb_box_number FROM processing_box WHERE pb_id='{cbo_JH_XM_Box.SelectedValue}'"));
                            if(Convert.ToInt32(_temp) > currentBoxId)
                                MessageBox.Show("请先删除较大盒号。", "删除失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            else if(MessageBox.Show("删除当前案卷盒会清空盒下已归档的文件，是否继续？", "删除确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                object value = cbo_JH_XM_Box.SelectedValue;
                                if(value != null)
                                {
                                    //将当前盒中文件状态致为未归档
                                    object ids = SqlHelper.ExecuteOnlyOneQuery($"SELECT pb_files_id FROM processing_box WHERE pb_obj_id='{objId}' AND pb_id='{value}'");
                                    string[] _ids = ids.ToString().Split(',');
                                    StringBuilder sb = new StringBuilder($"UPDATE processing_file_list SET pfl_status={(int)GuiDangStatus.NonGuiDang} WHERE pfl_id IN(");
                                    for(int i = 0; i < _ids.Length; i++)
                                        sb.Append($"'{_ids[i]}'{(_ids.Length - 1 != i ? "," : ")")}");
                                    SqlHelper.ExecuteNonQuery(sb.ToString());

                                    //删除当前盒信息
                                    SqlHelper.ExecuteNonQuery($"DELETE FROM processing_box WHERE pb_id='{value}'");
                                }
                            }
                        }
                    }
                    LoadBoxList(objId, ControlType.Plan);
                    LoadFileBoxTable(cbo_JH_XM_Box.SelectedValue, objId, ControlType.Plan);
                }
            }
            //计划-项目-课题-子课题
            if(label.Name.Contains("lbl_JH_XM_KT_ZKT_Box"))
            {
                object objId = dgv_Subject_FileList.Tag;
                if(objId != null)
                {
                    if("lbl_JH_XM_KT_ZKT_Box_Add".Equals(label.Name))//新增
                    {
                        //当前已有盒号数量
                        int amount = Convert.ToInt32(SqlHelper.ExecuteOnlyOneQuery($"SELECT COUNT(pb_box_number) FROM processing_box WHERE pb_obj_id='{objId}'"));
                        string gch = $"{unitCode}{GetGCAmount(unitCode)}";
                        string insertSql = $"INSERT INTO processing_box VALUES('{Guid.NewGuid().ToString()}','{amount + 1}','{gch}',null,'{objId}','{unitCode}')";
                        SqlHelper.ExecuteNonQuery(insertSql);
                    }
                    else if("lbl_JH_XM_KT_ZKT_Box_Remove".Equals(label.Name))//删除
                    {
                        object _temp = SqlHelper.ExecuteOnlyOneQuery($"SELECT MAX(pb_box_number) FROM processing_box WHERE pb_obj_id='{objId}'");
                        if(_temp != null)
                        {
                            object value = cbo_JH_XM_KT_ZKT_Box.SelectedValue;
                            int currentBoxId = Convert.ToInt32(SqlHelper.ExecuteOnlyOneQuery($"SELECT pb_box_number FROM processing_box WHERE pb_id='{value}'"));
                            if(Convert.ToInt32(_temp) > currentBoxId)
                                MessageBox.Show("请先删除较大盒号。", "删除失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            else if(MessageBox.Show("删除当前案卷盒会清空盒下已归档的文件，是否继续？", "删除确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                //将当前盒中文件状态致为未归档
                                object ids = SqlHelper.ExecuteOnlyOneQuery($"SELECT pb_files_id FROM processing_box WHERE pb_obj_id='{objId}' AND pb_id='{value}'");
                                string[] _ids = ids.ToString().Split(',');
                                StringBuilder sb = new StringBuilder($"UPDATE processing_file_list SET pfl_status={(int)GuiDangStatus.NonGuiDang} WHERE pfl_id IN(");
                                for(int i = 0; i < _ids.Length; i++)
                                    sb.Append($"'{_ids[i]}'{(_ids.Length - 1 != i ? "," : ")")}");
                                SqlHelper.ExecuteNonQuery(sb.ToString());

                                //删除当前盒信息
                                SqlHelper.ExecuteNonQuery($"DELETE FROM processing_box WHERE pb_id='{value}'");
                            }
                        }
                    }
                    LoadBoxList(objId, ControlType.Subject);
                    LoadFileBoxTable(cbo_JH_XM_KT_ZKT_Box.SelectedValue, objId, ControlType.Subject);
                }
            }
            //计划-课题
            if(label.Name.Contains("lbl_JH_KT_Box"))
            {
                object objId = dgv_Topic_FileList.Tag;
                if(objId != null)
                {
                    if("lbl_JH_KT_Box_Add".Equals(label.Name))//新增
                    {
                        //当前已有盒号数量
                        int amount = Convert.ToInt32(SqlHelper.ExecuteOnlyOneQuery($"SELECT COUNT(pb_box_number) FROM processing_box WHERE pb_obj_id='{objId}'"));
                        string gch = $"{unitCode}{GetGCAmount(unitCode)}";
                        string insertSql = $"INSERT INTO processing_box VALUES('{Guid.NewGuid().ToString()}','{amount + 1}','{gch}',null,'{objId}','{unitCode}')";
                        SqlHelper.ExecuteNonQuery(insertSql);
                    }
                    else if("lbl_JH_KT_Box_Remove".Equals(label.Name))//删除
                    {
                        object _temp = SqlHelper.ExecuteOnlyOneQuery($"SELECT MAX(pb_box_number) FROM processing_box WHERE pb_obj_id='{objId}'");
                        if(_temp != null)
                        {
                            object value = cbo_JH_KT_Box.SelectedValue;
                            int currentBoxId = Convert.ToInt32(SqlHelper.ExecuteOnlyOneQuery($"SELECT pb_box_number FROM processing_box WHERE pb_id='{value}'"));
                            if(Convert.ToInt32(_temp) > currentBoxId)
                                MessageBox.Show("请先删除较大盒号。", "删除失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            else if(MessageBox.Show("删除当前案卷盒会清空盒下已归档的文件，是否继续？", "删除确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                //将当前盒中文件状态致为未归档
                                object ids = SqlHelper.ExecuteOnlyOneQuery($"SELECT pb_files_id FROM processing_box WHERE pb_obj_id='{objId}' AND pb_id='{value}'");
                                string[] _ids = ids.ToString().Split(',');
                                StringBuilder sb = new StringBuilder($"UPDATE processing_file_list SET pfl_status={(int)GuiDangStatus.NonGuiDang} WHERE pfl_id IN(");
                                for(int i = 0; i < _ids.Length; i++)
                                    sb.Append($"'{_ids[i]}'{(_ids.Length - 1 != i ? "," : ")")}");
                                SqlHelper.ExecuteNonQuery(sb.ToString());

                                //删除当前盒信息
                                SqlHelper.ExecuteNonQuery($"DELETE FROM processing_box WHERE pb_id='{value}'");
                            }
                        }
                    }
                    LoadBoxList(objId, ControlType.Topic);
                    LoadFileBoxTable(cbo_JH_KT_Box.SelectedValue, objId, ControlType.Topic);
                }
            }
            //重大专项/研发
            if(label.Name.Contains("lbl_Imp_Box"))
            {
                object objId = dgv_Imp_FileList.Tag;
                if(objId != null)
                {
                    if("lbl_Imp_Box_Add".Equals(label.Name))//新增
                    {
                        //当前已有盒号数量
                        int amount = Convert.ToInt32(SqlHelper.ExecuteOnlyOneQuery($"SELECT COUNT(pb_box_number) FROM processing_box WHERE pb_obj_id='{objId}'"));
                        string gch = $"{unitCode}{GetGCAmount(unitCode)}";
                        string insertSql = $"INSERT INTO processing_box VALUES('{Guid.NewGuid().ToString()}','{amount + 1}','{gch}',null,'{objId}','{unitCode}')";
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
                                MessageBox.Show("请先删除较大盒号。", "删除失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            else if(MessageBox.Show("删除当前案卷盒会清空盒下已归档的文件，是否继续？", "删除确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                //将当前盒中文件状态致为未归档
                                object ids = SqlHelper.ExecuteOnlyOneQuery($"SELECT pb_files_id FROM processing_box WHERE pb_obj_id='{objId}' AND pb_id='{value}'");
                                string[] _ids = ids.ToString().Split(',');
                                StringBuilder sb = new StringBuilder($"UPDATE processing_file_list SET pfl_status={(int)GuiDangStatus.NonGuiDang} WHERE pfl_id IN(");
                                for(int i = 0; i < _ids.Length; i++)
                                    sb.Append($"'{_ids[i]}'{(_ids.Length - 1 != i ? "," : ")")}");
                                SqlHelper.ExecuteNonQuery(sb.ToString());

                                //删除当前盒信息
                                SqlHelper.ExecuteNonQuery($"DELETE FROM processing_box WHERE pb_id='{value}'");
                            }
                        }
                    }
                    LoadBoxList(objId, ControlType.Imp);
                    LoadFileBoxTable(cbo_Imp_Box.SelectedValue, objId, ControlType.Imp);
                }
            }
            //重大专项/研发 - 信息
            if(label.Name.Contains("lbl_Imp_Dev_Box"))
            {
                object objId = dgv_Special_FileList.Tag;
                if(objId != null)
                {
                    if("lbl_Imp_Dev_Box_Add".Equals(label.Name))//新增
                    {
                        //当前已有盒号数量
                        int amount = Convert.ToInt32(SqlHelper.ExecuteOnlyOneQuery($"SELECT COUNT(pb_box_number) FROM processing_box WHERE pb_obj_id='{objId}'"));
                        string gch = $"{unitCode}{GetGCAmount(unitCode)}";
                        string insertSql = $"INSERT INTO processing_box VALUES('{Guid.NewGuid().ToString()}','{amount + 1}','{gch}',null,'{objId}','{unitCode}')";
                        SqlHelper.ExecuteNonQuery(insertSql);
                    }
                    else if("lbl_Imp_Dev_Box_Remove".Equals(label.Name))//删除
                    {
                        object _temp = SqlHelper.ExecuteOnlyOneQuery($"SELECT MAX(pb_box_number) FROM processing_box WHERE pb_obj_id='{objId}'");
                        if(_temp != null)
                        {
                            object value = cbo_Imp_Dev_Box.SelectedValue;
                            int currentBoxId = Convert.ToInt32(SqlHelper.ExecuteOnlyOneQuery($"SELECT pb_box_number FROM processing_box WHERE pb_id='{value}'"));
                            if(Convert.ToInt32(_temp) > currentBoxId)
                                MessageBox.Show("请先删除较大盒号。", "删除失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            else if(MessageBox.Show("删除当前案卷盒会清空盒下已归档的文件，是否继续？", "删除确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                //将当前盒中文件状态致为未归档
                                object ids = SqlHelper.ExecuteOnlyOneQuery($"SELECT pb_files_id FROM processing_box WHERE pb_obj_id='{objId}' AND pb_id='{value}'");
                                string[] _ids = ids.ToString().Split(',');
                                StringBuilder sb = new StringBuilder($"UPDATE processing_file_list SET pfl_status={(int)GuiDangStatus.NonGuiDang} WHERE pfl_id IN(");
                                for(int i = 0; i < _ids.Length; i++)
                                    sb.Append($"'{_ids[i]}'{(_ids.Length - 1 != i ? "," : ")")}");
                                SqlHelper.ExecuteNonQuery(sb.ToString());

                                //删除当前盒信息
                                SqlHelper.ExecuteNonQuery($"DELETE FROM processing_box WHERE pb_id='{value}'");
                            }
                        }
                    }
                    LoadBoxList(objId, ControlType.Special);
                    LoadFileBoxTable(cbo_Imp_Dev_Box.SelectedValue, objId, ControlType.Special);
                }
            }
        }
    
        /// <summary>
        /// 计划 - 加载案卷盒列表
        /// </summary>
        /// <param name="objId">案卷盒所属对象ID</param>
        /// <param name="type">对象类型</param>
        private void LoadBoxList(object objId, ControlType type)
        {
            DataTable table = SqlHelper.ExecuteQuery($"SELECT pb_id,pb_box_number FROM processing_box WHERE pb_obj_id='{objId}' ORDER BY pb_box_number ASC");
            if(type == ControlType.Plan)
            {
                cbo_JH_Box.DataSource = table;
                cbo_JH_Box.DisplayMember = "pb_box_number";
                cbo_JH_Box.ValueMember = "pb_id";
                if(table.Rows.Count > 0)
                    cbo_JH_Box.SelectedIndex = 0;
            }
            else if(type == ControlType.Plan)
            {
                cbo_JH_XM_Box.DataSource = table;
                cbo_JH_XM_Box.DisplayMember = "pb_box_number";
                cbo_JH_XM_Box.ValueMember = "pb_id";
                if(table.Rows.Count > 0)
                    cbo_JH_XM_Box.SelectedIndex = 0;
            }
            else if(type == ControlType.Topic)
            {
                cbo_JH_KT_Box.DataSource = table;
                cbo_JH_KT_Box.DisplayMember = "pb_box_number";
                cbo_JH_KT_Box.ValueMember = "pb_id";
                if(table.Rows.Count > 0)
                    cbo_JH_KT_Box.SelectedIndex = 0;
            }
            else if(type == ControlType.Subject)
            {
                cbo_JH_XM_KT_ZKT_Box.DataSource = table;
                cbo_JH_XM_KT_ZKT_Box.DisplayMember = "pb_box_number";
                cbo_JH_XM_KT_ZKT_Box.ValueMember = "pb_id";
                if(table.Rows.Count > 0)
                    cbo_JH_XM_KT_ZKT_Box.SelectedIndex = 0;
            }
            else if(type == ControlType.Imp)
            {
                cbo_Imp_Box.DataSource = table;
                cbo_Imp_Box.DisplayMember = "pb_box_number";
                cbo_Imp_Box.ValueMember = "pb_id";
                if(table.Rows.Count > 0)
                    cbo_Imp_Box.SelectedIndex = 0;
            }
            else if(type == ControlType.Special)
            {
                cbo_Imp_Dev_Box.DataSource = table;
                cbo_Imp_Dev_Box.DisplayMember = "pb_box_number";
                cbo_Imp_Dev_Box.ValueMember = "pb_id";
                if(table.Rows.Count > 0)
                    cbo_Imp_Dev_Box.SelectedIndex = 0;
            }
        }

        private object GetGCAmount(object unitCode)
        {
            int amount = Convert.ToInt32(SqlHelper.ExecuteOnlyOneQuery($"SELECT COUNT(pb_id) FROM processing_box WHERE pb_unit_id='{unitCode}'"));
            return (amount + 1).ToString().PadLeft(6, '0');
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
                LoadFileBoxTable(pbId, dgv_Plan_FileList.Tag, ControlType.Plan);
            }
            else if("cbo_JH_XM_Box".Equals(comboBox.Name))
            {
                object pbId = comboBox.SelectedValue;
                LoadFileBoxTable(pbId, dgv_Project_FileList.Tag, ControlType.Plan);
            }
            else if("cbo_JH_KT_Box".Equals(comboBox.Name))
            {
                object pbId = comboBox.SelectedValue;
                LoadFileBoxTable(pbId, dgv_Topic_FileList.Tag, ControlType.Topic);
            }
            else if("cbo_JH_XM_KT_ZKT_Box".Equals(comboBox.Name))
            {
                object pbId = comboBox.SelectedValue;
                LoadFileBoxTable(pbId, dgv_Subject_FileList.Tag, ControlType.Subject);
            }
            else if("cbo_Imp_Box".Equals(comboBox.Name))
            {
                object pbId = comboBox.SelectedValue;
                LoadFileBoxTable(pbId, dgv_Imp_FileList.Tag, ControlType.Imp);
            }
            else if("cbo_Imp_Dev_Box".Equals(comboBox.Name))
            {
                object pbId = comboBox.SelectedValue;
                LoadFileBoxTable(pbId, dgv_Special_FileList.Tag, ControlType.Special);
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
                else if("plan_project".Equals(currentPageName))
                {
                    if(string.IsNullOrEmpty(txt_Project_Name.Text))
                    {
                        dgv_Project_FileList.ColumnHeadersDefaultCellStyle = DataGridViewStyleHelper.GetHeaderStyle();
                        dgv_Project_FileList.DefaultCellStyle = DataGridViewStyleHelper.GetCellStyle();
                        dgv_JH_XM_FileValid.ColumnHeadersDefaultCellStyle = DataGridViewStyleHelper.GetHeaderStyle();
                        dgv_JH_XM_FileValid.DefaultCellStyle = DataGridViewStyleHelper.GetCellStyle();
                    }
                }
                else if("Subject".Equals(currentPageName))
                {
                    if(string.IsNullOrEmpty(txt_Subject_Name.Text))
                    {
                        dgv_Subject_FileList.ColumnHeadersDefaultCellStyle = DataGridViewStyleHelper.GetHeaderStyle();
                        dgv_Subject_FileList.DefaultCellStyle = DataGridViewStyleHelper.GetCellStyle();
                        dgv_JH_XM_KT_ZKT_FileValid.ColumnHeadersDefaultCellStyle = DataGridViewStyleHelper.GetHeaderStyle();
                        dgv_JH_XM_KT_ZKT_FileValid.DefaultCellStyle = DataGridViewStyleHelper.GetCellStyle();
                    }
                }
                else if("plan_topic".Equals(currentPageName))
                {
                    if(string.IsNullOrEmpty(txt_Topic_Name.Text))
                    {
                        dgv_Topic_FileList.ColumnHeadersDefaultCellStyle = DataGridViewStyleHelper.GetHeaderStyle();
                        dgv_Topic_FileList.DefaultCellStyle = DataGridViewStyleHelper.GetCellStyle();
                        dgv_JH_KT_FileValid.ColumnHeadersDefaultCellStyle = DataGridViewStyleHelper.GetHeaderStyle();
                        dgv_JH_KT_FileValid.DefaultCellStyle = DataGridViewStyleHelper.GetCellStyle();
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
        private void LoadPageBasicInfo(object projectId, ControlType type, System.Drawing.Color color)
        {
            if(type == ControlType.Plan)
            {
                DataTable table = SqlHelper.ExecuteQuery($"SELECT * FROM project_info WHERE pi_id='{projectId}'");
                if(table.Rows.Count > 0)
                {
                    DataRow row = table.Rows[0];
                    dgv_Project_FileList.Tag = row["pi_id"];
                    pal_JH_XM.Tag = row["pi_obj_id"];
                    txt_Project_Code.Text = GetValue(row["pi_code"]);
                    txt_Project_Name.Text = GetValue(row["pi_name"]);
                    txt_Project_LY.Text = GetValue(row["pb_belong"]);
                    txt_Project_ZT.Text = GetValue(row["pb_belong_type"]);
                    txt_Project_JF.Text = GetValue(row["pi_money"]);

                    string startTime = GetValue(row["pi_start_datetime"]);
                    DateTime _startTime = new DateTime();
                    if(DateTime.TryParse(startTime, out _startTime))
                        dtp_Project_StartTime.Value = _startTime;

                    string endTime = GetValue(row["pi_end_datetime"]);
                    DateTime _endTime = new DateTime();
                    if(DateTime.TryParse(endTime, out _endTime))
                        dtp_JH_XM_EndTime.Value = _endTime;

                    txt_Project_LXND.Text = GetValue(row["pi_year"]);
                    txt_Project_Unit.Text = GetValue(row["pi_company_id"]);
                    txt_Project_Province.Text = GetValue(row["pi_province"]);
                    txt_Project_UnitUser.Text = GetValue(row["pi_company_user"]);
                    txt_Project_ObjUser.Text = GetValue(row["pi_project_user"]);
                    txt_Project_ObjIntroduct.Text = GetValue(row["pi_introduction"]);
                    ObjectSubmitStatus status = (ObjectSubmitStatus)row["pi_submit_status"];
                    EnableControls(type, status != ObjectSubmitStatus.SubmitSuccess);
                }
                LoadFileList(dgv_Project_FileList, "project_fl_", projectId);

                dgv_Project_FileList.ColumnHeadersDefaultCellStyle = DataGridViewStyleHelper.GetHeaderStyle();
                dgv_Project_FileList.DefaultCellStyle = DataGridViewStyleHelper.GetCellStyle();
                dgv_JH_XM_FileValid.ColumnHeadersDefaultCellStyle = DataGridViewStyleHelper.GetHeaderStyle();
                dgv_JH_XM_FileValid.DefaultCellStyle = DataGridViewStyleHelper.GetCellStyle();

                pal_JH_XM_BtnGroup.Enabled = !(color == DisEnbleColor);
                tab_MenuList.TabPages["project"].Tag = projectId;

                if(isBacked)
                {
                    btn_JH_XM_QTReason.Text = $"质检意见({GetAdvincesAmount(dgv_Project_FileList.Tag)})";
                }
            }
            else if(type == ControlType.Topic)
            {
                DataTable table = SqlHelper.ExecuteQuery($"SELECT * FROM project_info WHERE pi_id='{projectId}'");
                if(table.Rows.Count > 0)
                {
                    DataRow row = table.Rows[0];
                    dgv_Topic_FileList.Tag = row["pi_id"];
                    pal_JH_KT.Tag = row["pi_obj_id"];
                    txt_Topic_Code.Text = GetValue(row["pi_code"]);
                    txt_Topic_Name.Text = GetValue(row["pi_name"]);
                    txt_Topic_LY.Text = GetValue(row["pb_belong"]);
                    txt_Topic_Theme.Text = GetValue(row["pb_belong_type"]);
                    txt_Topic_Fund.Text = GetValue(row["pi_money"]);

                    string startTime = GetValue(row["pi_start_datetime"]);
                    DateTime _startTime = new DateTime();
                    if(DateTime.TryParse(startTime, out _startTime))
                        dtp_Topic_StartTime.Value = _startTime;

                    string endTime = GetValue(row["pi_end_datetime"]);
                    DateTime _endTime = new DateTime();
                    if(DateTime.TryParse(endTime, out _endTime))
                        dtp_Topic_EndTime.Value = _endTime;

                    txt_Topic_Year.Text = GetValue(row["pi_year"]);
                    txt_Topic_Unit.Text = GetValue(row["pi_company_id"]);
                    txt_Topic_Province.Text = GetValue(row["pi_province"]);
                    txt_Topic_UnitUser.Text = GetValue(row["pi_company_user"]);
                    txt_Topic_ProUser.Text = GetValue(row["pi_project_user"]);
                    txt_Topic_Intro.Text = GetValue(row["pi_introduction"]);
                    ObjectSubmitStatus status = (ObjectSubmitStatus)row["pi_submit_status"];
                    EnableControls(type, status != ObjectSubmitStatus.SubmitSuccess);
                }
                LoadFileList(dgv_Topic_FileList, "topic_fl_", projectId);

                dgv_Topic_FileList.ColumnHeadersDefaultCellStyle = DataGridViewStyleHelper.GetHeaderStyle();
                dgv_Topic_FileList.DefaultCellStyle = DataGridViewStyleHelper.GetCellStyle();
                dgv_JH_KT_FileValid.ColumnHeadersDefaultCellStyle = DataGridViewStyleHelper.GetHeaderStyle();
                dgv_JH_KT_FileValid.DefaultCellStyle = DataGridViewStyleHelper.GetCellStyle();

                pal_JH_KT_BtnGroup.Enabled = !(color == DisEnbleColor);
                tab_MenuList.TabPages["plan_topic"].Tag = projectId;

                if(isBacked)
                {
                    btn_JH_KT_QTReason.Text = $"质检意见({GetAdvincesAmount(dgv_Topic_FileList.Tag)})";
                }
            }
            else if(type == ControlType.Subject)
            {
                DataTable table = SqlHelper.ExecuteQuery($"SELECT * FROM subject_info WHERE si_id='{projectId}'");
                if(table.Rows.Count > 0)
                {
                    DataRow row = table.Rows[0];
                    dgv_Subject_FileList.Tag = row["si_id"];
                    pal_JH_XM_KT_ZKT.Tag = row["pi_id"];
                    txt_Subject_Code.Text = GetValue(row["si_code"]);
                    txt_Subject_Name.Text = GetValue(row["si_name"]);
                    txt_Subject_LY.Text = GetValue(row["si_field"]);
                    txt_Subject_Theme.Text = GetValue(row["si_belong"]);
                    txt_Subject_Fund.Text = GetValue(row["si_money"]);

                    string startTime = GetValue(row["si_start_datetime"]);
                    DateTime _startTime = new DateTime();
                    if(DateTime.TryParse(startTime, out _startTime))
                        dtp_Subject_StartTime.Value = _startTime;

                    string endTime = GetValue(row["si_end_datetime"]);
                    DateTime _endTime = new DateTime();
                    if(DateTime.TryParse(endTime, out _endTime))
                        dtp_Subject_EndTime.Value = _endTime;

                    txt_Subject_Year.Text = GetValue(row["si_year"]);
                    txt_Subject_Unit.Text = GetValue(row["si_company"]);
                    txt_Subject_Province.Text = GetValue(row["si_province"]);
                    txt_Subject_Unituser.Text = GetValue(row["si_company_user"]);
                    txt_Subject_Prouser.Text = GetValue(row["si_project_user"]);
                    txt_Subject_Intro.Text = GetValue(row["si_introduction"]);
                    ObjectSubmitStatus status = (ObjectSubmitStatus)row["si_submit_status"];
                    EnableControls(type, status != ObjectSubmitStatus.SubmitSuccess);
                }
                LoadFileList(dgv_Subject_FileList, "subject_fl_", projectId);

                dgv_Subject_FileList.ColumnHeadersDefaultCellStyle = DataGridViewStyleHelper.GetHeaderStyle();
                dgv_Subject_FileList.DefaultCellStyle = DataGridViewStyleHelper.GetCellStyle();
                dgv_JH_XM_KT_ZKT_FileValid.ColumnHeadersDefaultCellStyle = DataGridViewStyleHelper.GetHeaderStyle();
                dgv_JH_XM_KT_ZKT_FileValid.DefaultCellStyle = DataGridViewStyleHelper.GetCellStyle();

                pal_JH_XM_KT_ZKT_BtnGroup.Enabled = !(color == DisEnbleColor);
                tab_MenuList.TabPages["Subject"].Tag = projectId;
                if(isBacked)
                {
                    btn_JH_XM_KT_ZKT_QTReason.Text = $"质检意见({GetAdvincesAmount(dgv_Subject_FileList.Tag)})";
                }
            }
            else if(type == ControlType.Special)
            {
                object[] _obj = SqlHelper.ExecuteRowsQuery($"SELECT imp_id,imp_code,imp_name,imp_unit,imp_intro,pi_categor,imp_submit_status FROM imp_dev_info WHERE imp_id='{projectId}'");
                if(_obj != null)
                {
                    txt_Special_Code.Text = GetValue(_obj[1]);
                    txt_Special_Name.Text = GetValue(_obj[2]);
                    txt_Special_Unit.Text = GetValue(_obj[3]);
                    dgv_Special_FileList.Tag = GetValue(_obj[0]);
                    LoadFileList(dgv_Special_FileList, "special_fl_", GetValue(_obj[0]));
                    EnableControls(ControlType.Special, (ObjectSubmitStatus)_obj[6] != ObjectSubmitStatus.SubmitSuccess);
                }
                else
                {
                    object[] obj = SqlHelper.ExecuteRowsQuery($"SELECT dd_id,dd_code,dd_name FROM data_dictionary WHERE dd_id='{projectId}'");
                    if(obj != null)
                    {
                        txt_Special_Name.Text = GetValue(obj[2]);
                    }
                }
                if(DEV_TYPE == 1)
                    tab_MenuList.TabPages["Special"].Text = "研发信息";
                cbo_Special_HasNext.SelectedIndex = 0;
                dgv_Special_FileList.ColumnHeadersDefaultCellStyle = DataGridViewStyleHelper.GetHeaderStyle();
                dgv_Special_FileList.DefaultCellStyle = DataGridViewStyleHelper.GetCellStyle();
                dgv_Special_FileValid.ColumnHeadersDefaultCellStyle = DataGridViewStyleHelper.GetHeaderStyle();
                dgv_Special_FileValid.DefaultCellStyle = DataGridViewStyleHelper.GetCellStyle();

                pal_Imp_Dev_BtnGroup.Enabled = !(color == DisEnbleColor);
                tab_MenuList.TabPages["Special"].Tag = projectId;

                if(isBacked)
                {
                    btn_Imp_Dev_QTReason.Text = $"质检意见({GetAdvincesAmount(dgv_Special_FileList.Tag)})";
                }
            }
        }
        
        /// <summary>
        /// 新增对象事件
        /// </summary>
        private void Btn_Add_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;
            if("btn_JH_XM_Add".Equals(button.Name))
                ResetControls(ControlType.Plan);
            else if("btn_JH_KT_Add".Equals(button.Name))
                ResetControls(ControlType.Topic);
            else if("btn_JH_KT_ZKT_Add".Equals(button.Name))
                ResetControls(ControlType.Subject);
            else if("btn_JH_XM_KT_Add".Equals(button.Name))
                ResetControls(ControlType.Topic);
            else if("btn_JH_XM_KT_ZKT_Add".Equals(button.Name))
                ResetControls(ControlType.Subject);
        }
        
        /// <summary>
        /// 重置控件为默认状态
        /// </summary>
        /// <param name="type">对象类型</param>
        void ResetControls(ControlType type)
        {
            if(type == ControlType.Plan)
            {
                dgv_Project_FileList.Tag = null;
                DataGridViewStyleHelper.ResetDataGridView(dgv_Project_FileList, false);
                DataGridViewStyleHelper.ResetDataGridView(dgv_JH_XM_FileValid, false);
                txt_Project_Code.Clear();
                txt_Project_Name.Clear();
                txt_Project_LY.Clear();
                txt_Project_ZT.Clear();
                txt_Project_JF.ResetText();
                dtp_Project_StartTime.ResetText();
                dtp_JH_XM_EndTime.ResetText();
                txt_Project_LXND.Clear();
                txt_Project_UnitUser.Clear();
                txt_Project_ObjUser.Clear();
                txt_Project_ObjIntroduct.Clear();
            }
            else if(type == ControlType.Topic)
            {
                dgv_Topic_FileList.Tag = null;
                DataGridViewStyleHelper.ResetDataGridView(dgv_Topic_FileList, false);
                DataGridViewStyleHelper.ResetDataGridView(dgv_JH_KT_FileValid, false);
                txt_Topic_Code.Clear();
                txt_Topic_Name.Clear();
                txt_Topic_LY.Clear();
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
                DataGridViewStyleHelper.ResetDataGridView(dgv_JH_XM_KT_ZKT_FileValid, false);
                txt_Subject_Code.Clear();
                txt_Subject_Name.Clear();
                txt_Subject_LY.Clear();
                txt_Subject_Theme.Clear();
                txt_Subject_Fund.ResetText();
                dtp_Subject_StartTime.ResetText();
                dtp_Subject_EndTime.ResetText();
                txt_Subject_Year.Clear();
                txt_Subject_Unituser.Clear();
                txt_Subject_Prouser.Clear();
                txt_Subject_Intro.Clear();
            }
            else if(type == ControlType.Special)
            {
                DataGridViewStyleHelper.ResetDataGridView(dgv_Special_FileList, false);
                DataGridViewStyleHelper.ResetDataGridView(dgv_Special_FileValid, false);
                txt_Special_Code.Clear();
                txt_Special_Name.Clear();
                txt_Special_Unit.Clear();
                txt_Imp_Intro.Clear();
                pal_Special.Tag = null;
                dgv_Special_FileList.Tag = null;
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
                //tab_JH_FileInfo.Enabled = pal_JH_BasicInfo.Enabled = enable;
                foreach(Control item in pal_JH_BtnGroup.Controls)
                {
                    item.Enabled = enable;
                    if(item.Name.Contains("Submit"))
                        item.Text = enable ? "提交" : "已提交";
                }
            }
            else if(type == ControlType.Plan)
            {
                //tab_JH_XM_FileInfo.Enabled = pal_JH_XM.Enabled = enable;
                foreach(Control item in pal_JH_XM_BtnGroup.Controls)
                {
                    item.Enabled = enable;
                    if(item.Name.Contains("Submit"))
                        item.Text = enable ? "提交" : "已提交";
                }
            }
            else if(type == ControlType.Subject)
            {
                //tab_JH_XM_KT_ZKT_FileInfo.Enabled = pal_JH_XM_KT_ZKT.Enabled = enable;
                foreach(Control item in pal_JH_XM_KT_ZKT_BtnGroup.Controls)
                {
                    item.Enabled = enable;
                    if(item.Name.Contains("Submit"))
                        item.Text = enable ? "提交" : "已提交";
                }
            }
            else if(type == ControlType.Topic)
            {
                //tab_JH_KT_FileInfo.Enabled = pal_JH_KT.Enabled = enable;
                foreach(Control item in pal_JH_KT_BtnGroup.Controls)
                {
                    item.Enabled = enable;
                    if(item.Name.Contains("Submit"))
                        item.Text = enable ? "提交" : "已提交";
                }
            }
            else if(type == ControlType.Imp)
            {
                //tab_Imp_FileInfo.Enabled = pal_Imp.Enabled = enable;
                foreach(Control item in pal_Imp_BtnGroup.Controls)
                {
                    item.Enabled = enable;
                    if(item.Name.Contains("Submit"))
                        item.Text = enable ? "提交" : "已提交";
                }
            }
            else if(type == ControlType.Special)
            {
                //tab_Imp_Dev_FileInfo.Enabled = pal_Imp_Dev.Enabled = enable;
                foreach(Control item in pal_Imp_Dev_BtnGroup.Controls)
                {
                    item.Enabled = enable;
                    if(item.Name.Contains("Submit"))
                        item.Text = enable ? "提交" : "已提交";
                }
            }
        }
     
        /// <summary>
        /// 提交事件
        /// </summary>
        private void Btn_Submit_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("提交前请先确保所有数据已保存，确认要提交吗?", "提交确认", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                Button button = sender as Button;
                object objId = null;
                if("btn_JH_Submit".Equals(button.Name))
                {
                    objId = dgv_Plan_FileList.Tag;
                    if(objId != null)
                    {
                        SqlHelper.ExecuteNonQuery($"UPDATE project_info SET pi_submit_status='{(int)SubmitStatus.SubmitSuccess}' WHERE pi_id='{objId}'");
                        EnableControls(ControlType.Plan, false);
                    }
                    else
                        MessageBox.Show("请先保存数据.");
                }
                else if("btn_JH_XM_Submit".Equals(button.Name))
                {
                    objId = dgv_Project_FileList.Tag;
                    if(objId != null)
                    {
                        SqlHelper.ExecuteNonQuery($"UPDATE project_info SET pi_submit_status={(int)SubmitStatus.SubmitSuccess} WHERE pi_id='{objId}'");
                        EnableControls(ControlType.Plan, false);
                    }
                    else
                        MessageBox.Show("请先保存数据.");
                }
                else if("btn_JH_KT_Submit".Equals(button.Name))
                {
                    objId = dgv_Topic_FileList.Tag;
                    if(objId != null)
                    {
                        SqlHelper.ExecuteNonQuery($"UPDATE project_info SET pi_submit_status='{(int)SubmitStatus.SubmitSuccess}' WHERE pi_id='{objId}'");
                        EnableControls(ControlType.Topic, false);
                    }
                    else
                        MessageBox.Show("请先保存数据.");
                }
                else if("btn_JH_XM_KT_ZKT_Submit".Equals(button.Name))
                {
                    objId = dgv_Subject_FileList.Tag;
                    if(objId != null)
                    {
                        SqlHelper.ExecuteNonQuery($"UPDATE subject_info SET si_submit_status='{(int)SubmitStatus.SubmitSuccess}' WHERE si_id='{objId}'");
                        EnableControls(ControlType.Subject, false);
                    }
                    else
                        MessageBox.Show("请先保存数据.");
                }
                else if("btn_Imp_Submit".Equals(button.Name))
                {
                    objId = dgv_Imp_FileList.Tag;
                    if(objId != null)
                    {
                        SqlHelper.ExecuteNonQuery($"UPDATE imp_info SET imp_submit_status='{(int)ObjectSubmitStatus.SubmitSuccess}' WHERE imp_id='{objId}'");
                        EnableControls(ControlType.Imp, false);
                    }
                }
                else if("btn_Imp_Dev_Submit".Equals(button.Name))
                {
                    objId = dgv_Special_FileList.Tag;
                    if(objId != null)
                    {
                        SqlHelper.ExecuteNonQuery($"UPDATE imp_dev_info SET imp_submit_status='{(int)ObjectSubmitStatus.SubmitSuccess}' WHERE imp_id='{objId}'");
                        EnableControls(ControlType.Special, false);
                    }
                }
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
                    txt_Special_Code.Text = GetValue(_obj[0]);
                    txt_Special_Name.Text = GetValue(_obj[1]);
                    txt_Special_Intro.Text = GetValue(_obj[2]);
                }
                pal_Special.Tag = id;

                if(DEV_TYPE == 1)
                    tab_MenuList.TabPages["Special"].Text = "研发信息";
            }
            else
            {
                MessageBox.Show("请先保存当前信息！", "操作失败", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
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
                    string path = GetValue(dataGridView.CurrentCell.Value);
                    if(!string.IsNullOrEmpty(path))
                    {
                        if(MessageBox.Show("是否打开文件?", "确认提示", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk) == DialogResult.Yes)
                        {
                            if(File.Exists(path))
                            {
                                try
                                {
                                    System.Diagnostics.Process.Start("Explorer.exe", path);
                                }
                                catch(Exception ex)
                                {
                                    MessageBox.Show(ex.Message, "打开失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                            else
                                MessageBox.Show("文件不存在。", "打开失败", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        }
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
            if("btn_Plan_AddFile".Equals(name))
            {
                object objId = tab_Plan_Info.Tag;
                if(objId != null)
                {
                    if(dgv_Plan_FileList.SelectedRows.Count == 1)
                        frm = new Frm_AddFile(dgv_Plan_FileList, "plan_fl_", dgv_Plan_FileList.CurrentRow.Cells[0].Tag);
                    else
                        frm = new Frm_AddFile(dgv_Plan_FileList, "plan_fl_", null);
                    frm.txt_Unit.Text = UserHelper.GetInstance().User.UnitName;
                    frm.parentId = objId;
                    frm.Show();
                }
                else
                    MessageBox.Show("请先保存基本信息。", "温馨提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            else if("btn_Project_AddFile".Equals(name))
            {
                object objId = tab_Project_Info.Tag;
                if(objId != null)
                {
                    if(dgv_Project_FileList.SelectedRows.Count == 1)
                        frm = new Frm_AddFile(dgv_Project_FileList, "project_fl_", dgv_Project_FileList.CurrentRow.Cells[0].Tag);
                    else
                        frm = new Frm_AddFile(dgv_Project_FileList, "project_fl_", null);
                    frm.txt_Unit.Text = UserHelper.GetInstance().User.UnitName;
                    frm.parentId = objId;
                    frm.Show();
                }
                else
                    MessageBox.Show("请先保存基本信息。", "温馨提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            else if("btn_Topic_AddFile".Equals(name))
            {
                object objId = tab_Topic_Info.Tag;
                if(objId != null)
                {
                    if(dgv_Topic_FileList.SelectedRows.Count == 1)
                        frm = new Frm_AddFile(dgv_Topic_FileList, "topic_fl_", dgv_Topic_FileList.CurrentRow.Cells[0].Tag);
                    else
                        frm = new Frm_AddFile(dgv_Topic_FileList, "topic_fl_", null);
                    frm.txt_Unit.Text = UserHelper.GetInstance().User.UnitName;
                    frm.parentId = objId;
                    frm.Show();
                }
                else
                    MessageBox.Show("请先保存基本信息。", "温馨提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            else if("btn_Subject_AddFile".Equals(name))
            {
                object objId = tab_Subject_Info.Tag;
                if(objId != null)
                {
                    if(dgv_Subject_FileList.SelectedRows.Count == 1)
                        frm = new Frm_AddFile(dgv_Subject_FileList, "subject_fl_", dgv_Subject_FileList.CurrentRow.Cells[0].Tag);
                    else
                        frm = new Frm_AddFile(dgv_Subject_FileList, "subject_fl_", null);
                    frm.txt_Unit.Text = UserHelper.GetInstance().User.UnitName;
                    frm.parentId = objId;
                    frm.Show();
                }
                else
                    MessageBox.Show("请先保存基本信息。", "温馨提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            else if("btn_Imp_AddFile".Equals(name))
            {
                object objId = tab_Imp_Info.Tag;
                if(objId != null)
                {
                    if(dgv_Imp_FileList.SelectedRows.Count == 1)
                        frm = new Frm_AddFile(dgv_Imp_FileList, "imp_fl_", dgv_Imp_FileList.CurrentRow.Cells[0].Tag);
                    else
                        frm = new Frm_AddFile(dgv_Imp_FileList, "imp_fl_", null);
                    frm.parentId = objId;
                    frm.txt_Unit.Text = UserHelper.GetInstance().User.UnitName;
                    frm.Show();
                }
                else
                    MessageBox.Show("请先保存基本信息。", "温馨提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            else if("btn_Special_AddFile".Equals(name))
            {
                object objId = tab_Special_Info.Tag;
                if(objId != null)
                {
                    if(dgv_Special_FileList.SelectedRows.Count == 1)
                        frm = new Frm_AddFile(dgv_Special_FileList, "special_fl_", dgv_Special_FileList.CurrentRow.Cells[0].Tag);
                    else
                        frm = new Frm_AddFile(dgv_Special_FileList, "special_fl_", null);
                    frm.txt_Unit.Text = UserHelper.GetInstance().User.UnitName;
                    frm.parentId = objId;
                    frm.Show();
                }
                else
                    MessageBox.Show("请先保存基本信息。", "温馨提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }
    
        /// <summary>
        /// 上下移动
        /// </summary>
        private void Btn_TB_Click(object sender, EventArgs e)
        {
            string nameValue = (sender as Button).Name;
            //计划
            if(nameValue.Contains("btn_JH_Box"))
            {
                if(nameValue.Contains("Top"))
                    MoveListViewItem(lsv_JH_File2, true);
                else if(nameValue.Contains("Bottom"))
                    MoveListViewItem(lsv_JH_File2, false);
                SaveListSort(lsv_JH_File2, cbo_JH_Box.SelectedValue);
            }
            //计划-项目
            if(nameValue.Contains("btn_JH_XM_Box"))
            {
                if(nameValue.Contains("Top"))
                    MoveListViewItem(lsv_JH_XM_File2, true);
                else if(nameValue.Contains("Bottom"))
                    MoveListViewItem(lsv_JH_XM_File2, false);
                SaveListSort(lsv_JH_XM_File2, cbo_JH_XM_Box.SelectedValue);
            }
            //计划-项目-课题-子课题
            if(nameValue.Contains("btn_JH_XM_KT_ZKT_Box"))
            {
                if(nameValue.Contains("Top"))
                    MoveListViewItem(lsv_JH_XM_KT_ZKT_File2, true);
                else if(nameValue.Contains("Bottom"))
                    MoveListViewItem(lsv_JH_XM_KT_ZKT_File2, false);
                SaveListSort(lsv_JH_XM_KT_ZKT_File2, cbo_JH_XM_KT_ZKT_Box.SelectedValue);
            }
            //计划-课题
            if(nameValue.Contains("btn_JH_KT_Box"))
            {
                if(nameValue.Contains("Top"))
                    MoveListViewItem(lsv_JH_KT_File2, true);
                else if(nameValue.Contains("Bottom"))
                    MoveListViewItem(lsv_JH_KT_File2, false);
                SaveListSort(lsv_JH_KT_File2, cbo_JH_KT_Box.SelectedValue);
            }
            //重大专项
            if(nameValue.Contains("btn_Imp_Box"))
            {
                if(nameValue.Contains("Top"))
                    MoveListViewItem(lsv_Imp_File2, true);
                else if(nameValue.Contains("Bottom"))
                    MoveListViewItem(lsv_Imp_File2, false);
                SaveListSort(lsv_Imp_File2, cbo_Imp_Box.SelectedValue);
            }
            //重大专项-信息
            if(nameValue.Contains("btn_Imp_Dev_Box"))
            {
                if(nameValue.Contains("Top"))
                    MoveListViewItem(lsv_Imp_Dev_File2, true);
                else if(nameValue.Contains("Bottom"))
                    MoveListViewItem(lsv_Imp_Dev_File2, false);
                SaveListSort(lsv_Imp_Dev_File2, cbo_Imp_Dev_Box.SelectedValue);
            }
        }
     
        /// <summary>
        /// 保存新的顺序
        /// </summary>
        private void SaveListSort(ListView listView, object pbid)
        {
            StringBuilder builder = new StringBuilder();
            for(int i = 0; i < listView.Items.Count; i++)
                builder.Append(listView.Items[i].SubItems[0].Text + ",");
            string ids = builder.Remove(builder.Length - 1, 1).ToString();
            SqlHelper.ExecuteNonQuery($"UPDATE processing_box SET pb_files_id='{ids}' WHERE pb_id='{pbid}'");
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
            object objId = null, objName = null;
            if("btn_Imp_QTReason".Equals(name))
            {
                objId = dgv_Imp_FileList.Tag;
                objName = lbl_Imp_Name.Text;
            }
            else if("btn_JH_QTReason".Equals(name))
            {
                objId = dgv_Plan_FileList.Tag;
                objName = lbl_JH_Name.Text;
            }
            else if("btn_Imp_Dev_QTReason".Equals(name))
            {
                objId = dgv_Special_FileList.Tag;
                objName = txt_Special_Name.Text;
            }
            else if("btn_JH_XM_QTReason".Equals(name))
            {
                objId = dgv_Project_FileList.Tag;
                objName = txt_Project_Name.Text;
            }
            else if("btn_JH_XM_KT_ZKT_QTReason".Equals(name))
            {
                objId = dgv_Subject_FileList.Tag;
                objName = txt_Subject_Name.Text;
            }
            if(objId != null && objName != null)
            {
                Frm_AdviceBW frm = new Frm_AdviceBW(objId, objName);
                frm.Show();
            }
        }

        private void dgv_FileList_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if(e.Button == MouseButtons.Right && e.RowIndex != -1 && e.ColumnIndex != -1)
            {
                DataGridView view = sender as DataGridView;
                if(view.Columns[e.ColumnIndex].Name.Contains("link"))
                {
                    view.ClearSelection();
                    view.CurrentCell = view.Rows[e.RowIndex].Cells[e.ColumnIndex];
                    strip.Tag = view;
                    strip.Show(MousePosition);
                }
                else
                {
                    view.ClearSelection();
                    view.CurrentCell = view.Rows[e.RowIndex].Cells[e.ColumnIndex];
                    contextMenuStrip1.Tag = view;
                    contextMenuStrip1.Show(MousePosition);
                }
            }
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            DataGridView view = (DataGridView)((sender as ToolStripItem).GetCurrentParent() as ContextMenuStrip).Tag;
            object[] rootId = SqlHelper.ExecuteSingleColumnQuery($"SELECT bfi_id FROM backup_files_info WHERE bfi_trcid='{OBJECT_ID}' AND bfi_sort=-1");
            Frm_AddFile_FileSelect frm = new Frm_AddFile_FileSelect(rootId);
            if(frm.ShowDialog() == DialogResult.OK)
            {
                string fullPath = frm.SelectedFileName;
                if(File.Exists(fullPath))
                {
                    string savePath = Application.StartupPath + @"\TempBackupFolder\";
                    if(!Directory.Exists(savePath))
                        Directory.CreateDirectory(savePath);
                    string filePath = savePath + new FileInfo(fullPath).Name;
                    File.Copy(fullPath, filePath, true);
                    view.CurrentCell.Value = fullPath;
                    if(MessageBox.Show("已从服务器拷贝文件到本地，是否现在打开？", "操作确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        System.Diagnostics.Process.Start("EXPLORER.EXE", filePath);
                }
                else
                    MessageBox.Show("服务器不存在此文件。", "打开失败", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            DataGridView view = (DataGridView)((sender as ToolStripItem).GetCurrentParent() as ContextMenuStrip).Tag;
            view.CurrentCell.Value = string.Empty;
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

        }

        private void FileList_UserDeletedRow(object sender, DataGridViewRowEventArgs e) => removeIdList.Add(e.Row.Cells[0].Tag);

        private void FileList_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridView view = sender as DataGridView;
            object id = view.Rows[e.RowIndex].Cells[0].Tag;
            if(view.Name.Contains("Project"))
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
    }
}
