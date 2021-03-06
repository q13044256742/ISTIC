﻿using DevExpress.XtraEditors;
using DevExpress.XtraSplashScreen;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using 科技计划项目档案数据采集管理系统.Cataloguing;
using 科技计划项目档案数据采集管理系统.KyoControl;

namespace 科技计划项目档案数据采集管理系统
{
    public partial class Frm_MyWork : XtraForm
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
        /// 批次主键
        /// </summary>
        private object OBJECT_ID;
        private object PLAN_ID;
        public object planCode;
        public object unitCode;
        public object trcId;
        private string SearchKeyIndex = string.Empty;
        /// <summary>
        /// 当前批次关联的补录批次ID
        /// </summary>
        internal object[] additRecords;
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
        /// 我的加工 - 只查看
        /// </summary>
        private bool isReadOnly;

        /// <summary>
        /// 开始加工指定的对象
        /// </summary>
        /// <param name="workType">对象类型</param>
        /// <param name="planId">计划主键（仅针对光盘/批次加工）</param>
        public Frm_MyWork(WorkType workType, object PLAN_ID, object OBJECT_ID, ControlType controlType, bool isBacked)
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
            this.isBacked = isBacked;
            this.OBJECT_ID = OBJECT_ID;
            this.PLAN_ID = PLAN_ID;
            this.workType = workType;
            this.controlType = controlType;
            togle.Tag = SqlHelper.ExecuteOnlyOneQuery($"SELECT dd_code FROM data_dictionary WHERE dd_id=(SELECT com_id FROM transfer_registration_pc WHERE trp_id = '{OBJECT_ID}')");
        }

        public Frm_MyWork(WorkType workType, object planId, object objId, ControlType controlType, bool isBacked, bool isReadOnly) : this(workType, planId, objId, controlType, isBacked)
        {
            if (isReadOnly)
            {
                pal_Imp_BtnGroup.Enabled = false;
                pal_Project_BtnGroup.Enabled = false;
                pal_Topic_BtnGroup.Enabled = false;
                pal_Subject_BtnGroup.Enabled = false;
                pal_Plan_BtnGroup.Enabled = false;
                pal_Special_BtnGroup.Enabled = false;
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
            if (node == null) return;
            if ((ControlType)node.Tag == ControlType.Plan)
            {
                DataRow row = SqlHelper.ExecuteSingleRowQuery($"SELECT * FROM project_info WHERE pi_id='{node.Name}'");
                if (row != null)
                {
                    lbl_Plan_Name.Tag = ToolHelper.GetValue(row["pi_code"]);
                    lbl_Plan_Name.Text = ToolHelper.GetValue(row["pi_name"]);
                    txt_Plan_Intro.Text = ToolHelper.GetValue(row["pi_intro"]);
                    lbl_Plan_Tip.Text = $"著录人：{UserHelper.GetUserNameById(row["pi_worker_id"])}          质检人：{UserHelper.GetUserNameById(row["pi_checker_id"])}";
                    tab_Plan_Info.Tag = node.Name;

                    EnableControls(ControlType.Plan, ToolHelper.GetIntValue(row["pi_submit_status"], -1) != 2);
                    tab_Plan_Info.SelectedTabPageIndex = 0;
                    LoadDocumentList(node.Name, ControlType.Plan);

                    if (isBacked)
                        btn_Plan_QTReason.Text = $"质检意见({GetAdvincesAmount(node.Name)})";
                }
            }
            else
            {
                DataRow row = SqlHelper.ExecuteSingleRowQuery($"SELECT dd_name, dd_note, dd_code FROM data_dictionary WHERE dd_id='{node.Name}'");
                if (row != null)
                {
                    lbl_Plan_Name.Tag = ToolHelper.GetValue(row["dd_code"]);
                    lbl_Plan_Name.Text = ToolHelper.GetValue(row["dd_name"]);
                    txt_Plan_Intro.Text = ToolHelper.GetValue(row["dd_note"]);
                }
            }
            Tag = lbl_Plan_Name.Tag;
            plan.Tag = node.Name;
            if (node.ForeColor == DisEnbleColor)
            {
                pal_Plan_BtnGroup.Enabled = false;
            }
            if (isBacked)
            {
                cbo_Plan_HasNext.Enabled = !isBacked;
            }
        }

        /// <summary>
        /// 加载计划/专项下的档号列表
        /// </summary>
        /// <param name="parentID">父ID</param>
        /// <param name="type">列表类型</param>
        private void LoadDocumentList(object parentID, ControlType type)
        {
            string queryDocList = $"SELECT pt_id, pt_code FROM processing_tag WHERE pt_obj_id='{parentID}'";
            DataTable dataTable = SqlHelper.ExecuteQuery(queryDocList);
            if (type == ControlType.Plan)
            {
                if (dataTable.Rows.Count > 0)
                {
                    cbo_Plan_AJ_Code.DataSource = dataTable;
                    cbo_Plan_AJ_Code.DisplayMember = "pt_code";
                    cbo_Plan_AJ_Code.ValueMember = "pt_id";

                    Cbo_Code_SelectionChangeCommitted(cbo_Plan_AJ_Code, null);
                }
                else
                    cbo_Plan_AJ_Code.DataSource = null;
            }
            else if (type == ControlType.Special)
            {
                if (dataTable.Rows.Count > 0)
                {
                    cbo_Special_AJ_Code.DataSource = dataTable;
                    cbo_Special_AJ_Code.DisplayMember = "pt_code";
                    cbo_Special_AJ_Code.ValueMember = "pt_id";

                    Cbo_Code_SelectedIndexChanged(cbo_Special_AJ_Code, null);
                }
                else
                    cbo_Special_AJ_Code.DataSource = null;
            }
        }

        /// <summary>
        /// 加载文件列表
        /// </summary>
        /// <param name="dataGridView">表格控件</param>
        /// <param name="selectedRowIndex">默认选中行，-1表示不选中</param>
        /// <param name="parentId">所属对象ID</param>
        private void LoadFileList(DataGridView dataGridView, object parentId, int selectedRowIndex)
        {
            string querySql = "SELECT pfl_id, ROW_NUMBER() OVER (ORDER BY pfl_sort) rownum, pfl_stage, pfl_categor, pfl_code, pfl_name, pfl_amount, pfl_user, pfl_type, " +
                $"pfl_pages, pfl_count, TRY_CAST(TRY_PARSE(pfl_date as date) AS VARCHAR) pfl_date, pfl_unit, pfl_carrier, pfl_link FROM processing_file_list WHERE pfl_obj_id='{parentId}'";
            DataTable dataTable = SqlHelper.ExecuteQuery(querySql);
            dataGridView.DataSource = dataTable;
            dataGridView.ColumnHeadersDefaultCellStyle = DataGridViewStyleHelper.GetHeaderStyle();
            dataGridView.DefaultCellStyle = DataGridViewStyleHelper.GetCellStyle();

            if (selectedRowIndex != -1 && selectedRowIndex < dataGridView.RowCount)
            {
                dataGridView.ClearSelection();
                dataGridView.Rows[selectedRowIndex].Selected = true;
                dataGridView.FirstDisplayedScrollingRowIndex = selectedRowIndex;
            }
        }

        /// <summary>
        /// 添加树节点
        /// </summary>
        /// <param name="nodeName">节点</param>
        /// <param name="parentNodeName">父节点名称</param>
        private void AddTreeNode(TreeNode treeNode, string parentNodeName)
        {
            if (string.IsNullOrEmpty(parentNodeName))
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
            tab_MenuList.Update();
            //tab_MenuList.SelectedIndex = index;
        }

        /// <summary>
        /// 渲染表格样式，初始化表单字段
        /// </summary>
        private void Frm_MyWork_Load(object sender, EventArgs e)
        {
            InitialForm(PLAN_ID, controlType);

            SearchText.Properties.DropDownRows = 10;
            tab_Plan_Info.Height = 358;
            tab_Plan_Info.Top = 310;
            tab_Project_Info.Height = 358;
            tab_Project_Info.Top = 310;
            tab_Topic_Info.Height = 358;
            tab_Topic_Info.Top = 310;
            tab_Subject_Info.Height = 358;
            tab_Subject_Info.Top = 310;
            tab_Imp_Info.Height = 358;
            tab_Imp_Info.Top = 310;
            tab_Special_Info.Height = 358;
            tab_Special_Info.Top = 310;

            MinimumSize = new Size(Width, Height);

            //不同加工种类特殊处理
            if (workType == WorkType.PaperWork)
            {
                dgv_Plan_FileList.Columns["plan_fl_link"].Visible = false;
                dgv_Project_FileList.Columns["project_fl_link"].Visible = false;
                dgv_Topic_FileList.Columns["topic_fl_link"].Visible = false;
                dgv_Subject_FileList.Columns["subject_fl_link"].Visible = false;
                dgv_Imp_FileList.Columns["imp_fl_link"].Visible = false;
                dgv_Special_FileList.Columns["special_fl_link"].Visible = false;
            }

            if (isBacked && !isReadOnly)
            {
                Text += "[返工]";
                btn_Plan_QTReason.Visible = isBacked;
                btn_Imp_QTReason.Visible = isBacked;
                btn_Special_QTReason.Visible = isBacked;
                btn_Project_QTReason.Visible = isBacked;
                btn_Topic_QTReason.Visible = isBacked;
                btn_Subject_QTReason.Visible = isBacked;
            }
            else if (isReadOnly)
            {
                Text += "[查看]";
                btn_SubmitAll.Visible = false;
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
            InitialLostReasonList(dgv_Plan_FileValid, "plan_fc_");
            InitialLostReasonList(dgv_Project_FileValid, "project_fc_");
            InitialLostReasonList(dgv_Topic_FileValid, "topic_fc_");
            InitialLostReasonList(dgv_Subject_FileValid, "subject_fc_");
            InitialLostReasonList(dgv_Imp_FileValid, "imp_fc_");
            InitialLostReasonList(dgv_Special_FileValid, "special_fc_");

            //加载省市
            InitialProvinceList(cbo_Project_Province);
            InitialProvinceList(cbo_Topic_Province);
            InitialProvinceList(cbo_Subject_Province);

            cbo_Plan_HasNext.SelectedIndex = 0;
            cbo_Project_HasNext.SelectedIndex = 0;
            cbo_Topic_HasNext.SelectedIndex = 0;

            dgv_Plan_FileList.DefaultCellStyle = DataGridViewStyleHelper.GetCellStyle();
            dgv_Project_FileList.DefaultCellStyle = DataGridViewStyleHelper.GetCellStyle();
            dgv_Topic_FileList.DefaultCellStyle = DataGridViewStyleHelper.GetCellStyle();
            dgv_Subject_FileList.DefaultCellStyle = DataGridViewStyleHelper.GetCellStyle();
            dgv_Imp_FileList.DefaultCellStyle = DataGridViewStyleHelper.GetCellStyle();
            dgv_Special_FileList.DefaultCellStyle = DataGridViewStyleHelper.GetCellStyle();
            dgv_Subject_FileList.ColumnHeadersDefaultCellStyle = DataGridViewStyleHelper.GetHeaderStyle();
            dgv_Subject_FileValid.ColumnHeadersDefaultCellStyle = DataGridViewStyleHelper.GetHeaderStyle();

            dgv_Plan_FileValid.DefaultCellStyle = DataGridViewStyleHelper.GetCellStyle();
            dgv_Project_FileValid.DefaultCellStyle = DataGridViewStyleHelper.GetCellStyle();
            dgv_Topic_FileValid.DefaultCellStyle = DataGridViewStyleHelper.GetCellStyle();
            dgv_Subject_FileValid.DefaultCellStyle = DataGridViewStyleHelper.GetCellStyle();
            dgv_Imp_FileValid.DefaultCellStyle = DataGridViewStyleHelper.GetCellStyle();
            dgv_Special_FileValid.DefaultCellStyle = DataGridViewStyleHelper.GetCellStyle();

            dgv_Plan_FileList.AutoGenerateColumns = false;
            dgv_Project_FileList.AutoGenerateColumns = false;
            dgv_Topic_FileList.AutoGenerateColumns = false;
            dgv_Subject_FileList.AutoGenerateColumns = false;
            dgv_Imp_FileList.AutoGenerateColumns = false;
            dgv_Special_FileList.AutoGenerateColumns = false;
        }

        private void InitialProvinceList(System.Windows.Forms.ComboBox comboBox)
        {
            DataTable table = SqlHelper.GetProvinceList();
            comboBox.DataSource = table;
            comboBox.DisplayMember = "dd_name";
            comboBox.ValueMember = "dd_id";
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
        }

        private void InitialFormatList(DataGridView dataGridView, string key)
        {
            DataGridViewComboBoxColumn formatColumn = dataGridView.Columns[key + "format"] as DataGridViewComboBoxColumn;
            formatColumn.DataSource = DictionaryHelper.GetTableByCode("dic_file_format");
            formatColumn.DisplayMember = "dd_name";
            formatColumn.ValueMember = "dd_id";
        }

        private void InitialCarrierList(DataGridView dataGridView, string key)
        {
            DataGridViewComboBoxColumn carrierColumn = dataGridView.Columns[key + "carrier"] as DataGridViewComboBoxColumn;
            carrierColumn.DataSource = DictionaryHelper.GetTableByCode("dic_file_zt");
            carrierColumn.DisplayMember = "dd_name";
            carrierColumn.ValueMember = "dd_id";
        }

        private void InitialSecretList(DataGridView dataGridView, string key)
        {
            DataGridViewComboBoxColumn secretColumn = dataGridView.Columns[key + "secret"] as DataGridViewComboBoxColumn;
            secretColumn.DataSource = DictionaryHelper.GetTableByCode("dic_file_mj");
            secretColumn.DisplayMember = "dd_name";
            secretColumn.ValueMember = "dd_id";
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
            for (int i = 0; i < dataGridView.Rows.Count; i++)
            {
                DataGridViewComboBoxCell satgeCell = (DataGridViewComboBoxCell)dataGridView.Rows[i].Cells[key + "stage"];
                object stageId = satgeCell.Value;
                if (stageId != null)
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
        /// <param name="stageId">阶段ID</param>
        public void SetCategorByStage(object stageId, DataGridViewRow dataGridViewRow, object key)
        {
            //文件类别
            DataGridViewComboBoxCell categorCell = dataGridViewRow.Cells[key + "categor"] as DataGridViewComboBoxCell;
            dataGridViewRow.Cells[key + "categorname"].Tag = stageId;
            DataTable table = CategorHelper.GetInstance().GetCategorTableByStage(stageId);
            if (table != null && table.Rows.Count > 0)
            {
                categorCell.DataSource = table;
                categorCell.DisplayMember = "dd_name";
                categorCell.ValueMember = "dd_id";
            }
        }

        /// <summary>
        /// 单元格事件绑定
        /// </summary>
        private void Dgv_File_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            DataGridView dataGridView = sender as DataGridView;
            if ("dgv_Plan_FileList".Equals(dataGridView.Name))
            {
                string columnName = dgv_Plan_FileList.CurrentCell.OwningColumn.Name;
                Control con = e.Control;
                con.Tag = ControlType.Plan;
                if ("plan_fl_stage".Equals(columnName))
                    (con as System.Windows.Forms.ComboBox).SelectionChangeCommitted += new EventHandler(StageComboBox_SelectionChangeCommitted);
                else if ("plan_fl_categor".Equals(columnName))
                    (con as System.Windows.Forms.ComboBox).SelectionChangeCommitted += new EventHandler(CategorComboBox_SelectionChangeCommitted);
            }
            else if ("dgv_Project_FileList".Equals(dataGridView.Name))
            {
                string columnName = dgv_Project_FileList.CurrentCell.OwningColumn.Name;
                Control con = e.Control;
                con.Tag = ControlType.Project;
                if ("project_fl_stage".Equals(columnName))
                    (con as System.Windows.Forms.ComboBox).SelectionChangeCommitted += new EventHandler(StageComboBox_SelectionChangeCommitted);
                else if ("project_fl_categor".Equals(columnName))
                    (con as System.Windows.Forms.ComboBox).SelectionChangeCommitted += new EventHandler(CategorComboBox_SelectionChangeCommitted);
            }
            else if ("dgv_Topic_FileList".Equals(dataGridView.Name))
            {
                string columnName = dgv_Topic_FileList.CurrentCell.OwningColumn.Name;
                Control con = e.Control;
                con.Tag = ControlType.Topic;
                if ("topic_fl_stage".Equals(columnName))
                    (con as System.Windows.Forms.ComboBox).SelectionChangeCommitted += new EventHandler(StageComboBox_SelectionChangeCommitted);
                else if ("topic_fl_categor".Equals(columnName))
                    (con as System.Windows.Forms.ComboBox).SelectionChangeCommitted += new EventHandler(CategorComboBox_SelectionChangeCommitted);
            }
            else if ("dgv_Subject_FileList".Equals(dataGridView.Name))
            {
                string columnName = dgv_Subject_FileList.CurrentCell.OwningColumn.Name;
                Control con = e.Control;
                con.Tag = ControlType.Subject;
                if ("subject_fl_stage".Equals(columnName))
                    (con as System.Windows.Forms.ComboBox).SelectionChangeCommitted += new EventHandler(StageComboBox_SelectionChangeCommitted);
                else if ("subject_fl_categor".Equals(columnName))
                    (con as System.Windows.Forms.ComboBox).SelectionChangeCommitted += new EventHandler(CategorComboBox_SelectionChangeCommitted);
            }
            else if ("dgv_Imp_FileList".Equals(dataGridView.Name))
            {
                string columnName = dgv_Imp_FileList.CurrentCell.OwningColumn.Name;
                Control con = e.Control;
                con.Tag = ControlType.Imp;
                if ("imp_fl_stage".Equals(columnName))
                    (con as System.Windows.Forms.ComboBox).SelectionChangeCommitted += new EventHandler(StageComboBox_SelectionChangeCommitted);
                else if ("imp_fl_categor".Equals(columnName))
                    (con as System.Windows.Forms.ComboBox).SelectionChangeCommitted += new EventHandler(CategorComboBox_SelectionChangeCommitted);
            }
            else if ("dgv_Special_FileList".Equals(dataGridView.Name))
            {
                string columnName = dgv_Special_FileList.CurrentCell.OwningColumn.Name;
                Control con = e.Control;
                con.Tag = ControlType.Special;
                if ("special_fl_stage".Equals(columnName))
                    (con as System.Windows.Forms.ComboBox).SelectionChangeCommitted += new EventHandler(StageComboBox_SelectionChangeCommitted);
                else if ("special_fl_categor".Equals(columnName))
                    (con as System.Windows.Forms.ComboBox).SelectionChangeCommitted += new EventHandler(CategorComboBox_SelectionChangeCommitted);
            }
            if (e.Control is System.Windows.Forms.ComboBox)
            {
                System.Windows.Forms.ComboBox box = e.Control as System.Windows.Forms.ComboBox;
                if (box.Items.Count > 0)
                {
                    box.SelectedValue = box.Items[0];
                }
            }
        }

        /// <summary>
        /// 文件阶段 下拉事件
        /// </summary>
        private void StageComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            System.Windows.Forms.ComboBox comboBox = sender as System.Windows.Forms.ComboBox;
            if ((ControlType)comboBox.Tag == ControlType.Plan)
                SetCategorByStage(comboBox.SelectedValue, dgv_Plan_FileList.CurrentRow, "plan_fl_");
            else if ((ControlType)comboBox.Tag == ControlType.Project)
                SetCategorByStage(comboBox.SelectedValue, dgv_Project_FileList.CurrentRow, "project_fl_");
            else if ((ControlType)comboBox.Tag == ControlType.Topic)
                SetCategorByStage(comboBox.SelectedValue, dgv_Topic_FileList.CurrentRow, "topic_fl_");
            else if ((ControlType)comboBox.Tag == ControlType.Subject)
                SetCategorByStage(comboBox.SelectedValue, dgv_Subject_FileList.CurrentRow, "subject_fl_");
            else if ((ControlType)comboBox.Tag == ControlType.Imp)
                SetCategorByStage(comboBox.SelectedValue, dgv_Imp_FileList.CurrentRow, "imp_fl_");
            else if ((ControlType)comboBox.Tag == ControlType.Special)
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
            comboBox.MaxDropDownItems = 10;
            if ((ControlType)comboBox.Tag == ControlType.Plan)
                SetNameByCategor(comboBox, dgv_Plan_FileList.CurrentRow, "plan_fl_", cbo_Plan_AJ_Code.SelectedValue);
            else if ((ControlType)comboBox.Tag == ControlType.Project)
                SetNameByCategor(comboBox, dgv_Project_FileList.CurrentRow, "project_fl_", tab_Project_Info.Tag);
            else if ((ControlType)comboBox.Tag == ControlType.Topic)
                SetNameByCategor(comboBox, dgv_Topic_FileList.CurrentRow, "topic_fl_", tab_Topic_Info.Tag);
            else if ((ControlType)comboBox.Tag == ControlType.Subject)
                SetNameByCategor(comboBox, dgv_Subject_FileList.CurrentRow, "subject_fl_", tab_Subject_Info.Tag);
            else if ((ControlType)comboBox.Tag == ControlType.Imp)
                SetNameByCategor(comboBox, dgv_Imp_FileList.CurrentRow, "imp_fl_", tab_Imp_Info.Tag);
            else if ((ControlType)comboBox.Tag == ControlType.Special)
                SetNameByCategor(comboBox, dgv_Special_FileList.CurrentRow, "special_fl_", cbo_Special_AJ_Code.SelectedValue);
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
            if (comboBox.Items.Count <= 4) return;
            string value = ToolHelper.GetValue(SqlHelper.ExecuteOnlyOneQuery($"SELECT dd_note FROM data_dictionary WHERE dd_id='{comboBox.SelectedValue}'"));
            currentRow.Cells[key + "name"].Value = value;

            int amount = SqlHelper.ExecuteCountQuery($"SELECT COUNT(pfl_id) FROM processing_file_list WHERE pfl_categor='{comboBox.SelectedValue}' AND pfl_obj_id='{objId}'");

            currentRow.Cells[key + "categorname"].Value = null;
            if (comboBox.SelectedIndex == comboBox.Items.Count - 1)
            {
                currentRow.DataGridView.Columns[key + "categorname"].Visible = true;
                object id = currentRow.Cells[key + "categorname"].Tag;
                int _amount = SqlHelper.ExecuteCountQuery($"SELECT COUNT(dd_id) FROM data_dictionary WHERE dd_pId='{id}'");
                string tempKey = ((DataRowView)comboBox.Items[0]).Row.ItemArray[1].ToString();
                if (Regex.IsMatch(tempKey, "^[A-D]"))
                {
                    string _key = ToolHelper.GetValue(tempKey).Substring(0, 1) + _amount.ToString().PadLeft(2, '0');
                    currentRow.Cells[key + "code"].Value = _key + "-" + (amount + 1).ToString().PadLeft(2, '0');
                }
            }
            else
            {
                string _key = comboBox.Text.Split(' ')[0];
                if (Regex.IsMatch(_key, "^[A-D]"))
                    currentRow.Cells[key + "code"].Value = _key + "-" + (amount + 1).ToString().PadLeft(2, '0');
            }
        }

        private void Dgv_File_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

        }

        private void Cbo_JH_Next_SelectionChangeCommitted(object sender, EventArgs e)
        {
            System.Windows.Forms.ComboBox comboBox = sender as System.Windows.Forms.ComboBox;
            if (comboBox.Name.Contains("Special"))
            {
                object id = tab_Special_Info.Tag;
                if (id == null)
                {
                    XtraMessageBox.Show("尚未保存当前项目，无法添加新数据。", "温馨提示");
                    cbo_Special_HasNext.SelectedIndex = 0;
                }
                else
                {
                    int _index = tab_MenuList.SelectedIndex + 1;
                    int index = comboBox.SelectedIndex;
                    project.Tag = null;
                    topic.Tag = null;
                    if (index == 0)//无
                        ShowTab(null, _index);
                    else if (index == 1)//父级 - 项目
                    {
                        ShowTab("project", _index);
                        ResetControls(ControlType.Project);
                        project.Tag = id;
                        EnableControls(ControlType.Project, true);
                        Tag = txt_Special_Code.Text;
                    }
                    else if (index == 2)//父级 - 课题
                    {
                        ShowTab("topic", _index);
                        ResetControls(ControlType.Topic);
                        topic.Tag = id;
                        EnableControls(ControlType.Topic, true);
                        Tag = txt_Special_Code.Text;
                    }
                    tab_MenuList.SelectedIndex = _index;
                }
            }
            else if (comboBox.Name.Contains("Plan"))
            {
                object id = tab_Plan_Info.Tag;
                if (id == null)
                {
                    XtraMessageBox.Show("尚未保存当前项目，无法添加新数据。", "温馨提示");
                    cbo_Plan_HasNext.SelectedIndex = 0;
                }
                else
                {
                    int _index = tab_MenuList.SelectedIndex;
                    int index = comboBox.SelectedIndex;
                    if (index == 0)//无
                    {
                        ShowTab(null, _index + 1);
                        project.Tag = null;
                    }
                    else if (index == 1)//父级 - 项目
                    {
                        ShowTab("project", _index + 1);
                        ResetControls(ControlType.Project);
                        project.Tag = id;
                        EnableControls(ControlType.Project, true);
                    }
                    else if (index == 2)//父级 - 课题
                    {
                        ShowTab("topic", _index + 1);
                        ResetControls(ControlType.Topic);
                        topic.Tag = id;
                        EnableControls(ControlType.Topic, true);
                    }
                }
            }
        }

        private void Cbo_JH_KT_HasNext_SelectionChangeCommitted(object sender, EventArgs e)
        {
            System.Windows.Forms.ComboBox comboBox = sender as System.Windows.Forms.ComboBox;
            int index = comboBox.SelectedIndex;
            int _index = tab_MenuList.SelectedIndex + 1;
            if (index == 0)//无
            {
                ShowTab(null, _index);
            }
            else if (index == 1)//子级 - 子课题
            {
                object id = tab_Topic_Info.Tag;
                if (id == null)
                {
                    XtraMessageBox.Show("尚未保存当前课题信息，无法添加新数据。", "温馨提示");
                    cbo_Topic_HasNext.SelectedIndex = 0;
                    subject.Tag = null;
                }
                else
                {
                    ShowTab("subject", _index);
                    ResetControls(ControlType.Subject);
                    subject.Tag = id;
                }
                tab_MenuList.SelectedIndex = _index;
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
            if ("btn_Plan_Save".Equals(button.Name))
            {
                object parentID = cbo_Plan_AJ_Code.SelectedValue;
                object objId = tab_Plan_Info.Tag;
                view = dgv_Plan_FileList;
                key = "plan_fl_";
                int index = tab_Plan_Info.SelectedTabPageIndex;
                if (index == 0)//文件
                {
                    if (objId == null)
                    {
                        objId = tab_Plan_Info.Tag = AddBasicInfo(plan.Tag, ControlType.Plan);
                        XtraMessageBox.Show("保存成功。");
                    }
                    else
                    {
                        UpdateBasicInfo(objId, ControlType.Plan);
                    }
                    if (CheckFileList(view.Rows, key))
                    {
                        if (parentID != null)
                        {
                            int selectedRow = GetSelectedRowIndex(view);
                            int maxLength = view.Rows.Count - 1;
                            for (int i = 0; i < maxLength; i++)
                            {
                                DataGridViewRow row = view.Rows[i];
                                row.Cells[$"{key}id"].Value = AddFileInfo(key, row, parentID, row.Index);
                            }
                            //自动更新缺失文件表
                            UpdateLostFileList(parentID);
                            //移除文件列表
                            RemoveFileList(parentID);
                            LoadFileList(view, parentID, selectedRow);
                        }
                        else
                        {
                            XtraMessageBox.Show("请先添加档号信息。", "保存失败", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        }
                    }
                    else
                        XtraMessageBox.Show("文件信息存在错误数据，请先更正。", "保存失败", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
                else if (objId != null)
                {
                    if (index == 1)//文件核查
                    {
                        if (CheckValidMustEnter(dgv_Plan_FileValid, "plan_fc_"))
                        {
                            ModifyFileValid(dgv_Plan_FileValid, parentID, "plan_fc_");
                            XtraMessageBox.Show("文件核查信息保存成功。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        }
                        else
                            XtraMessageBox.Show("请填写完整信息。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                    else if (index == 2)
                    {
                        if (CheckValueIsNotNull(ControlType.Plan))
                        {
                            string insertSQL = $"UPDATE processing_box SET pb_gc_id='{txt_Plan_GCID.Text}' WHERE pb_id='{cbo_Plan_Box.SelectedValue}';";
                            SqlHelper.ExecuteNonQuery(insertSQL);
                            XtraMessageBox.Show("案卷保存成功。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        }
                    }
                }
            }
            else if ("btn_Project_Save".Equals(button.Name))
            {
                object objId = tab_Project_Info.Tag;
                view = dgv_Project_FileList;
                key = "project_fl_";
                int index = tab_Project_Info.SelectedTabPageIndex;
                if (index == 0)
                {
                    bool isAdd = false;
                    if (CheckMustEnter(button.Name, objId))
                    {
                        if (objId == null)
                        {
                            objId = tab_Project_Info.Tag = AddBasicInfo(project.Tag, ControlType.Project);
                            SetFileLinkByCode(txt_Project_Code.Text, objId);
                            isAdd = true;
                        }
                        else
                            UpdateBasicInfo(objId, ControlType.Project);

                        if (CheckFileList(view.Rows, key))
                        {
                            int selectedRow = GetSelectedRowIndex(view);
                            int maxLength = view.Rows.Count - 1;
                            for (int i = 0; i < maxLength; i++)
                            {
                                object fileName = view.Rows[i].Cells[$"{key}name"].Value;
                                if (fileName != null)
                                {
                                    DataGridViewRow row = view.Rows[i];
                                    row.Cells[$"{key}id"].Value = AddFileInfo(key, row, objId, row.Index);
                                }
                            }
                            //自动更新缺失文件表
                            UpdateLostFileList(objId);
                            //移除文件列表
                            RemoveFileList(objId);
                            LoadFileList(view, objId, selectedRow);
                            XtraMessageBox.Show("信息保存成功。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        }
                        else
                            XtraMessageBox.Show("文件信息存在错误数据，请先更正。", "保存失败", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        if (!isBacked && isAdd)
                        {
                            TreeNode[] pNode = treeView.Nodes.Find(ToolHelper.GetValue(project.Tag), true);
                            if (pNode.Length == 1)
                            {
                                TreeNode node = new TreeNode()
                                {
                                    Name = ToolHelper.GetValue(objId),
                                    Text = txt_Project_Code.Text,
                                    Tag = ControlType.Project
                                };
                                pNode[0].Nodes.Add(node);
                                treeView.SelectedNode = node;
                            }
                        }
                    }
                }
                else if (objId != null)
                {
                    if (index == 1)//文件核查
                    {
                        if (CheckValidMustEnter(dgv_Project_FileValid, "project_fc_"))
                        {
                            ModifyFileValid(dgv_Project_FileValid, objId, "project_fc_");
                            XtraMessageBox.Show("文件核查信息保存成功。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        }
                        else
                            XtraMessageBox.Show("请填写完整信息。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                    else if (index == 2)
                    {
                        if (CheckValueIsNotNull(ControlType.Project))
                        {
                            string docId = txt_Project_AJ_Code.Text;
                            string docName = txt_Project_AJ_Name.Text;
                            string primaryKey = Guid.NewGuid().ToString();
                            string insertSQL =
                               $"DELETE FROM processing_tag WHERE pt_id=(SELECT pt_id FROM processing_box WHERE pb_id='{cbo_Project_Box.SelectedValue}');" +
                               $"INSERT INTO processing_tag(pt_id, pt_code, pt_name, pt_obj_id) VALUES('{primaryKey}','{docId}','{docName}','{objId}');";
                            insertSQL += $"UPDATE processing_box SET pb_gc_id='{txt_Project_GCID.Text}', pt_id='{primaryKey}' WHERE pb_id='{cbo_Project_Box.SelectedValue}';";
                            SqlHelper.ExecuteNonQuery(insertSQL);
                            XtraMessageBox.Show("案卷保存成功。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        }
                    }
                }
            }
            else if ("btn_Topic_Save".Equals(button.Name))
            {
                object objId = tab_Topic_Info.Tag;
                view = dgv_Topic_FileList;
                key = "topic_fl_";
                int index = tab_Topic_Info.SelectedTabPageIndex;
                if (index == 0)
                {
                    bool isAdd = false;
                    if (CheckMustEnter(button.Name, objId))
                    {
                        if (objId != null)//更新
                            UpdateBasicInfo(objId, ControlType.Topic);
                        else//新增
                        {
                            objId = tab_Topic_Info.Tag = AddBasicInfo(topic.Tag, ControlType.Topic);
                            SetFileLinkByCode(txt_Topic_Code.Text, objId);
                            isAdd = true;
                        }
                        if (CheckFileList(view.Rows, key))
                        {
                            int selectedRow = GetSelectedRowIndex(view);
                            int maxLength = view.Rows.Count - 1;
                            for (int i = 0; i < maxLength; i++)
                            {
                                object fileName = view.Rows[i].Cells[$"{key}name"].Value;
                                if (fileName != null)
                                {
                                    DataGridViewRow row = view.Rows[i];
                                    row.Cells[$"{key}id"].Value = AddFileInfo(key, row, objId, row.Index);
                                }
                            }
                            //自动更新缺失文件表
                            UpdateLostFileList(objId);
                            //移除文件列表
                            RemoveFileList(objId);
                            LoadFileList(view, objId, selectedRow);
                            XtraMessageBox.Show("信息保存成功。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        }
                        else
                            XtraMessageBox.Show("文件信息存在错误数据，请先更正。", "保存失败", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        if (!isBacked && isAdd)
                        {
                            TreeNode[] pNode = treeView.Nodes.Find(ToolHelper.GetValue(topic.Tag), true);
                            if (pNode.Length == 1)
                            {
                                ControlType _type = ControlType.Project;
                                if ((ControlType)pNode[0].Tag == ControlType.Project)
                                    _type = ControlType.Topic;
                                TreeNode node = new TreeNode()
                                {
                                    Name = ToolHelper.GetValue(objId),
                                    Text = txt_Topic_Code.Text,
                                    Tag = _type
                                };
                                pNode[0].Nodes.Add(node);
                                treeView.SelectedNode = node;
                            }
                            //GoToTreeList();
                        }
                    }
                }
                else if (objId != null)
                {
                    if (index == 1)//文件核查
                    {
                        if (CheckValidMustEnter(dgv_Topic_FileValid, "topic_fc_"))
                        {
                            ModifyFileValid(dgv_Topic_FileValid, objId, "topic_fc_");
                            XtraMessageBox.Show("文件核查信息保存成功。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        }
                        else
                            XtraMessageBox.Show("请填写完整信息。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                    else if (index == 2)
                    {
                        if (CheckValueIsNotNull(ControlType.Topic))
                        {
                            string docId = txt_Topic_AJ_Code.Text;
                            string docName = txt_Topic_AJ_Name.Text;
                            string primaryKey = Guid.NewGuid().ToString();
                            string insertSQL =
                               $"DELETE FROM processing_tag WHERE pt_id=(SELECT pt_id FROM processing_box WHERE pb_id='{cbo_Topic_Box.SelectedValue}');" +
                               $"INSERT INTO processing_tag(pt_id, pt_code, pt_name, pt_obj_id) VALUES('{primaryKey}','{docId}','{docName}','{objId}');";
                            insertSQL += $"UPDATE processing_box SET pb_gc_id='{txt_Topic_GCID.Text}', pt_id='{primaryKey}' WHERE pb_id='{cbo_Topic_Box.SelectedValue}';";
                            SqlHelper.ExecuteNonQuery(insertSQL);
                            XtraMessageBox.Show("案卷保存成功。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        }
                    }
                }
            }
            else if ("btn_Subject_Save".Equals(button.Name))
            {
                object objId = tab_Subject_Info.Tag;
                view = dgv_Subject_FileList;
                key = "subject_fl_";
                int index = tab_Subject_Info.SelectedTabPageIndex;
                if (index == 0)
                {
                    bool isAdd = false;
                    if (CheckMustEnter(button.Name, objId))
                    {
                        if (objId != null)
                            UpdateBasicInfo(objId, ControlType.Subject);
                        else
                        {
                            objId = tab_Subject_Info.Tag = AddBasicInfo(subject.Tag, ControlType.Subject);
                            SetFileLinkByCode(txt_Subject_Code.Text, objId);
                            isAdd = true;
                        }
                        if (CheckFileList(view.Rows, key))
                        {
                            int selectedRow = GetSelectedRowIndex(view);
                            int maxLength = view.Rows.Count - 1;
                            for (int i = 0; i < maxLength; i++)
                            {
                                object fileName = view.Rows[i].Cells[$"{key}name"].Value;
                                if (fileName != null)
                                {
                                    DataGridViewRow row = view.Rows[i];
                                    row.Cells[$"{key}id"].Value = AddFileInfo(key, row, objId, row.Index);
                                }
                            }
                            //自动更新缺失文件表
                            UpdateLostFileList(objId);
                            //移除文件列表
                            RemoveFileList(objId);
                            LoadFileList(view, objId, selectedRow);
                            XtraMessageBox.Show("信息保存成功。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        }
                        else
                            XtraMessageBox.Show("文件信息存在错误数据，请先更正。", "保存失败", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        if (!isBacked && isAdd)
                        {
                            TreeNode[] pNode = treeView.Nodes.Find(ToolHelper.GetValue(subject.Tag), true);
                            if (pNode.Length == 1)
                            {
                                TreeNode node = new TreeNode()
                                {
                                    Name = ToolHelper.GetValue(objId),
                                    Text = txt_Subject_Code.Text,
                                    Tag = ControlType.Subject
                                };
                                pNode[0].Nodes.Add(node);
                                treeView.SelectedNode = node;
                            }
                            //GoToTreeList();
                        }
                    }
                }
                else if (objId != null)
                {
                    if (index == 1)//文件核查
                    {
                        if (CheckValidMustEnter(dgv_Subject_FileValid, "subject_fc_"))
                        {
                            ModifyFileValid(dgv_Subject_FileValid, objId, "subject_fc_");
                            XtraMessageBox.Show("文件核查信息保存成功。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        }
                        else
                            XtraMessageBox.Show("请填写完整信息。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                    else if (index == 2)
                    {
                        if (CheckValueIsNotNull(ControlType.Subject))
                        {
                            string docId = txt_Subject_AJ_Code.Text;
                            string docName = txt_Subject_AJ_Name.Text;
                            string primaryKey = Guid.NewGuid().ToString();
                            string insertSQL =
                               $"DELETE FROM processing_tag WHERE pt_id=(SELECT pt_id FROM processing_box WHERE pb_id='{cbo_Subject_Box.SelectedValue}');" +
                               $"INSERT INTO processing_tag(pt_id, pt_code, pt_name, pt_obj_id) VALUES('{primaryKey}','{docId}','{docName}','{objId}');";
                            insertSQL += $"UPDATE processing_box SET pb_gc_id='{txt_Subject_GCID.Text}', pt_id='{primaryKey}' WHERE pb_id='{cbo_Subject_Box.SelectedValue}';";
                            SqlHelper.ExecuteNonQuery(insertSQL);
                            XtraMessageBox.Show("案卷保存成功。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        }
                    }
                }
            }
            else if ("btn_Imp_Save".Equals(button.Name))
            {
                object objId = tab_Imp_Info.Tag;
                view = dgv_Imp_FileList;
                key = "imp_fl_";
                int index = tab_Imp_Info.SelectedTabPageIndex;
                if (index == 0)
                {
                    if (objId == null)
                        objId = tab_Imp_Info.Tag = AddBasicInfo(OBJECT_ID, ControlType.Imp);
                    if (CheckFileList(view.Rows, key))
                    {
                        int maxLength = dgv_Imp_FileList.Rows.Count - 1;
                        for (int i = 0; i < maxLength; i++)
                        {
                            object fileName = dgv_Imp_FileList.Rows[i].Cells[$"{key}name"].Value;
                            if (fileName != null)
                            {
                                DataGridViewRow row = dgv_Imp_FileList.Rows[i];
                                row.Cells[$"{key}id"].Value = AddFileInfo(key, row, objId, row.Index);
                            }
                        }
                        //自动更新缺失文件表
                        UpdateLostFileList(objId);
                        //移除文件列表
                        RemoveFileList(objId);
                        LoadFileList(view, objId, -1);
                        XtraMessageBox.Show("信息保存成功。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                    else
                        XtraMessageBox.Show("文件信息存在错误数据，请先更正。", "保存失败", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
                else if (objId != null)
                {
                    if (index == 1)//文件核查
                    {
                        if (CheckValidMustEnter(dgv_Imp_FileValid, "imp_fc_"))
                        {
                            ModifyFileValid(dgv_Imp_FileValid, objId, "imp_fc_");
                            XtraMessageBox.Show("文件核查信息保存成功。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        }
                        else
                            XtraMessageBox.Show("请填写完整信息。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                    else if (index == 2)
                    {
                        if (CheckValueIsNotNull(ControlType.Imp))
                        {
                            string docId = txt_Imp_AJ_Code.Text;
                            string docName = txt_Imp_AJ_Name.Text;
                            string primaryKey = Guid.NewGuid().ToString();
                            string insertSQL =
                               $"DELETE FROM processing_tag WHERE pt_id=(SELECT pt_id FROM processing_box WHERE pb_id='{cbo_Imp_Box.SelectedValue}');" +
                               $"INSERT INTO processing_tag(pt_id, pt_code, pt_name, pt_obj_id) VALUES('{primaryKey}','{docId}','{docName}','{objId}');";
                            insertSQL += $"UPDATE processing_box SET pb_gc_id='{txt_Imp_GCID.Text}', pt_id='{primaryKey}' WHERE pb_id='{cbo_Imp_Box.SelectedValue}';";
                            SqlHelper.ExecuteNonQuery(insertSQL);
                            XtraMessageBox.Show("案卷保存成功。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        }
                    }
                }
            }
            else if ("btn_Special_Save".Equals(button.Name))
            {
                object parentID = cbo_Special_AJ_Code.SelectedValue;
                object objId = tab_Special_Info.Tag;
                view = dgv_Special_FileList;
                key = "special_fl_";
                int index = tab_Special_Info.SelectedTabPageIndex;
                if (index == 0)
                {
                    if (CheckMustEnter(button.Name, objId))
                    {
                        if (objId == null)
                        {
                            objId = tab_Special_Info.Tag = AddBasicInfo(special.Tag, ControlType.Special);
                            XtraMessageBox.Show("保存成功。");
                            treeView.Nodes[0].Nodes.Add(new TreeNode()
                            {
                                Name = ToolHelper.GetValue(objId),
                                Text = txt_Special_Code.Text,
                                Tag = ControlType.Special
                            });
                            treeView.SelectedNode = treeView.Nodes[0].FirstNode;
                        }
                        else UpdateBasicInfo(objId, ControlType.Special);
                        Tag = txt_Special_Code.Text;
                        if (CheckFileList(view.Rows, key))
                        {
                            if (parentID != null)
                            {
                                int selectedRow = GetSelectedRowIndex(view);
                                int maxLength = dgv_Special_FileList.Rows.Count - 1;
                                for (int i = 0; i < maxLength; i++)
                                {
                                    object fileName = dgv_Special_FileList.Rows[i].Cells[$"{key}name"].Value;
                                    if (fileName != null)
                                    {
                                        DataGridViewRow row = dgv_Special_FileList.Rows[i];
                                        row.Cells[$"{key}id"].Value = AddFileInfo(key, row, parentID, row.Index);
                                    }
                                }
                                //自动更新缺失文件表
                                UpdateLostFileList(parentID);
                                //移除文件列表
                                RemoveFileList(parentID);
                                LoadFileList(view, parentID, selectedRow);
                                XtraMessageBox.Show("保存成功。");
                            }
                        }
                        else
                            XtraMessageBox.Show("文件信息存在错误数据，请先更正。", "保存失败", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                }
                else if (objId != null)
                {
                    if (index == 1)
                    {
                        if (CheckValidMustEnter(dgv_Special_FileValid, "special_fc_"))
                        {
                            ModifyFileValid(dgv_Special_FileValid, parentID, "special_fc_");
                            XtraMessageBox.Show("文件核查信息保存成功。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        }
                        else
                            XtraMessageBox.Show("请填写完整信息。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                    else if (index == 2)
                    {
                        if (CheckValueIsNotNull(ControlType.Special))
                        {
                            string insertSQL = $"UPDATE processing_box SET pb_gc_id='{txt_Special_GCID.Text}' WHERE pb_id='{cbo_Special_Box.SelectedValue}';";
                            SqlHelper.ExecuteNonQuery(insertSQL);
                            XtraMessageBox.Show("案卷保存成功。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 判断是否有电子文件相关链接
        /// </summary>
        /// <param name="code">对象编号</param>
        private void SetFileLinkByCode(string code, object objectID)
        {
            if (string.IsNullOrEmpty(code.Trim()) || objectID == null)
                return;
            string lostReasonTypeID = "caaac45d-70b0-457d-a919-156d3342f63b";//汇编包含
            DataRow row = SqlHelper.ExecuteSingleRowQuery($"SELECT id, code, fid FROM file_link WHERE code='{code}'");
            if (row != null)
            {
                string querySQL = $"SELECT dd_name, dd_note, ISNULL(extend_2,0) ismust, A.* FROM processing_file_list " +
                    $"LEFT JOIN data_dictionary ON pfl_categor=dd_id " +
                    $"LEFT JOIN (" +
                    $"SELECT pi_id, pi_code, pi_categor FROM project_info WHERE pi_categor=2 UNION ALL " +
                    $"SELECT ti_id, ti_code, ti_categor FROM topic_info UNION ALL " +
                    $"SELECT si_id, si_code, si_categor FROM subject_info)A ON pi_id=pfl_obj_id " +
                    $"WHERE pfl_id='{row["fid"]}' ";
                object[] result = SqlHelper.ExecuteRowsQuery(querySQL);
                int objectType = ToolHelper.GetIntValue(result[5]);
                string lostRemark = $"此文件已与{(objectType == 2 ? "项目" : (objectType == 3 || objectType == -3) ? "课题" : "子课题")}编号为[{result[4]}({result[0]})]电子文件汇编；";
                string insertSQL = $"INSERT INTO processing_file_lost(pfo_id, pfo_categor, pfo_name, pfo_reason, pfo_remark, pfo_obj_id, pfo_ismust) VALUES " +
                    $"('{Guid.NewGuid().ToString()}', '{result[0]}', '{result[1]}', '{lostReasonTypeID}', '{lostRemark}', '{objectID}', '{result[2]}')";

                SqlHelper.ExecuteNonQuery(insertSQL);
            }
        }

        private bool CheckValueIsNotNull(ControlType type)
        {
            bool result = true;
            errorProvider1.Clear();
            if (type == ControlType.Plan)
            {
                string value1 = txt_Plan_AJ_Code_R.Text;
                if (string.IsNullOrEmpty(value1))
                {
                    errorProvider1.SetError(txt_Plan_AJ_Code_R, "提示：档号不能为空。");
                    result = false;
                }
                string value2 = txt_Plan_AJ_Name_R.Text;
                if (string.IsNullOrEmpty(value2))
                {
                    errorProvider1.SetError(txt_Plan_AJ_Name_R, "提示：案卷名称不能为空。");
                    result = false;
                }
                string value3 = txt_Plan_GCID.Text;
                if (string.IsNullOrEmpty(value3))
                {
                    errorProvider1.SetError(txt_Plan_GCID, "提示：馆藏号不能为空。");
                    result = false;
                }
            }
            else if (type == ControlType.Project)
            {
                string value1 = txt_Project_AJ_Code.Text;
                if (string.IsNullOrEmpty(value1))
                {
                    errorProvider1.SetError(txt_Project_AJ_Code, "提示：档号不能为空。");
                    result = false;
                }
                string value2 = txt_Project_AJ_Name.Text;
                if (string.IsNullOrEmpty(value2))
                {
                    errorProvider1.SetError(txt_Project_AJ_Name, "提示：案卷名称不能为空。");
                    result = false;
                }
                string value3 = txt_Project_GCID.Text;
                if (string.IsNullOrEmpty(value3))
                {
                    errorProvider1.SetError(txt_Project_GCID, "提示：馆藏号不能为空。");
                    result = false;
                }
            }
            else if (type == ControlType.Topic)
            {
                string value1 = txt_Topic_AJ_Code.Text;
                if (string.IsNullOrEmpty(value1))
                {
                    errorProvider1.SetError(txt_Topic_AJ_Code, "提示：档号不能为空。");
                    result = false;
                }
                string value2 = txt_Topic_AJ_Name.Text;
                if (string.IsNullOrEmpty(value2))
                {
                    errorProvider1.SetError(txt_Topic_AJ_Name, "提示：案卷名称不能为空。");
                    result = false;
                }
                string value3 = txt_Topic_GCID.Text;
                if (string.IsNullOrEmpty(value3))
                {
                    errorProvider1.SetError(txt_Topic_GCID, "提示：馆藏号不能为空。");
                    result = false;
                }
            }
            else if (type == ControlType.Subject)
            {
                string value1 = txt_Subject_AJ_Code.Text;
                if (string.IsNullOrEmpty(value1))
                {
                    errorProvider1.SetError(txt_Subject_AJ_Code, "提示：档号不能为空。");
                    result = false;
                }
                string value2 = txt_Subject_AJ_Name.Text;
                if (string.IsNullOrEmpty(value2))
                {
                    errorProvider1.SetError(txt_Subject_AJ_Name, "提示：案卷名称不能为空。");
                    result = false;
                }
                string value3 = txt_Subject_GCID.Text;
                if (string.IsNullOrEmpty(value3))
                {
                    errorProvider1.SetError(txt_Subject_GCID, "提示：馆藏号不能为空。");
                    result = false;
                }
            }
            else if (type == ControlType.Imp)
            {
                string value1 = txt_Imp_AJ_Code.Text;
                if (string.IsNullOrEmpty(value1))
                {
                    errorProvider1.SetError(txt_Imp_AJ_Code, "提示：档号不能为空。");
                    result = false;
                }
                string value2 = txt_Imp_AJ_Name.Text;
                if (string.IsNullOrEmpty(value2))
                {
                    errorProvider1.SetError(txt_Imp_AJ_Name, "提示：案卷名称不能为空。");
                    result = false;
                }
                string value3 = txt_Imp_GCID.Text;
                if (string.IsNullOrEmpty(value3))
                {
                    errorProvider1.SetError(txt_Imp_GCID, "提示：馆藏号不能为空。");
                    result = false;
                }
            }
            else if (type == ControlType.Special)
            {
                string value1 = txt_Special_AJ_Code_R.Text;
                if (string.IsNullOrEmpty(value1))
                {
                    errorProvider1.SetError(txt_Special_AJ_Code_R, "提示：档号不能为空。");
                    result = false;
                }
                string value2 = txt_Special_AJ_Name_R.Text;
                if (string.IsNullOrEmpty(value2))
                {
                    errorProvider1.SetError(txt_Special_AJ_Name_R, "提示：案卷名称不能为空。");
                    result = false;
                }
                string value3 = txt_Special_GCID.Text;
                if (string.IsNullOrEmpty(value3))
                {
                    errorProvider1.SetError(txt_Special_GCID, "提示：馆藏号不能为空。");
                    result = false;
                }
            }
            return result;
        }

        private int GetSelectedRowIndex(DataGridView view) => view.CurrentRow == null ? 0 : view.CurrentRow.Index;

        /// <summary>
        /// 更新指定对象下的文件缺失列表
        /// </summary>
        /// <param name="objID">项目/课题ID</param>
        private void UpdateLostFileList(object objID)
        {
            StringBuilder sqlString = new StringBuilder();

            int logCount = SqlHelper.ExecuteCountQuery($"SELECT COUNT(pfo_id) FROM processing_file_lost WHERE pfo_obj_id='{objID}'");
            //如果是首次保存缺失表，则直接统计缺失情况
            if (logCount == 0)
            {
                string querySQL = "SELECT d1.dd_name, d1.extend_2 FROM data_dictionary d1 " +
                    "INNER JOIN data_dictionary d2 ON d1.dd_pId = d2.dd_id " +
                    "INNER JOIN data_dictionary d3 ON d2.dd_pId = d3.dd_id " +
                    "WHERE d3.dd_code='dic_file_jd' AND d1.dd_name<>'其他' " +
                    "AND d1.dd_name NOT IN ( " +
                    "SELECT dd_name FROM processing_file_list AS fi " +
                    "INNER JOIN data_dictionary AS dd ON fi.pfl_categor = dd.dd_id " +
                    $"WHERE (fi.pfl_obj_id = '{objID}') GROUP BY dd_name ) " +
                    "ORDER BY dd_name";
                DataTable table = SqlHelper.ExecuteQuery(querySQL);
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    object categor = table.Rows[i]["dd_name"];
                    int ismust = ToolHelper.GetIntValue(table.Rows[i]["extend_2"], 0);
                    sqlString.Append("INSERT INTO processing_file_lost (pfo_id, pfo_categor, pfo_obj_id, pfo_ismust) " +
                        $"VALUES('{Guid.NewGuid().ToString()}', '{categor}', '{objID}', '{ismust}');");
                }
            }
            //如果非首次保存，则删除缺失表中已新增的文件类型
            else
            {
                string querySQL = "SELECT dd_name FROM processing_file_list " +
                    "LEFT JOIN data_dictionary ON pfl_categor = dd_id " +
                    "WHERE dd_name IN(SELECT pfo_categor FROM processing_file_lost WHERE pfo_obj_id = pfl_obj_id) " +
                   $"AND pfl_obj_id = '{objID}'";
                object[] categorList = SqlHelper.ExecuteSingleColumnQuery(querySQL);
                if (categorList != null && categorList.Length > 0)
                {
                    string categorString = ToolHelper.GetStringBySplit(categorList, ",", "'");
                    sqlString.Append($"DELETE FROM processing_file_lost WHERE pfo_obj_id='{objID}' AND pfo_categor IN ({categorString});");
                }
            }
            SqlHelper.ExecuteNonQuery(sqlString.ToString());
        }

        private bool CheckMustEnter(string name, object pid)
        {
            bool result = true;
            errorProvider1.Clear();
            if (name.Contains("Project"))
            {
                string proCode = txt_Project_Code.Text;
                if (string.IsNullOrEmpty(proCode))
                {
                    errorProvider1.SetError(txt_Project_Code, "提示：项目编号不能为空");
                    result = false;
                }
                else if (proCode.Contains(" "))
                {
                    errorProvider1.SetError(txt_Project_Code, "提示：项目编号不能含有空格");
                    result = false;
                }
                else
                {
                    object objectID = tab_Project_Info.Tag;
                    int count = SqlHelper.ExecuteCountQuery("SELECT COUNT(pi_id) FROM (" +
                        "SELECT pi_id, pi_code, pi_orga_id FROM project_info UNION ALL " +
                        "SELECT ti_id, ti_code, ti_orga_id FROM topic_info UNION ALL " +
                        "SELECT si_id, si_code, si_orga_id FROM subject_info) A " +
                       $"WHERE A.pi_code='{proCode}' AND A.pi_orga_id='{togle.Tag}' AND A.pi_id<>'{objectID}';");
                    if (count > 0)
                    {
                        errorProvider1.SetError(txt_Project_Code, "提示：此项目/课题编号已存在");
                        result = false;
                    }
                }

                string funds = txt_Project_Funds.Text;
                if (!string.IsNullOrEmpty(funds))
                {
                    if (!Regex.IsMatch(funds, "^[0-9]+(.[0-9]{1,2})?$"))
                    {
                        errorProvider1.SetError(txt_Project_Funds, "提示：请输入合法经费");
                        result = false;
                    }
                }
                string year = txt_Project_Year.Text;
                if (string.IsNullOrEmpty(year) || !Regex.IsMatch(year, "^\\d{4}$"))
                {
                    errorProvider1.SetError(txt_Project_Year, "提示：请输入有效的立项年度");
                    result = false;
                }

                string startDate = txt_Project_StartTime.Text;
                if (!string.IsNullOrEmpty(startDate))
                {

                    if (!Regex.IsMatch(startDate, "^\\d{4}-\\d{2}-\\d{2}$") || !DateTime.TryParse(startDate, out DateTime time))
                    {
                        errorProvider1.SetError(dtp_Project_StartTime, "提示：请输入yyyy-MM-dd格式的有效日期");
                        result = false;
                    }
                }
                string endDate = txt_Project_EndTime.Text;
                if (!string.IsNullOrEmpty(endDate))
                {
                    if (!Regex.IsMatch(endDate, "^\\d{4}-\\d{2}-\\d{2}$") || !DateTime.TryParse(endDate, out DateTime time))
                    {
                        errorProvider1.SetError(dtp_Project_EndTime, "提示：请输入yyyy-MM-dd格式的有效日期");
                        result = false;
                    }
                }

            }
            else if (name.Contains("Topic"))
            {
                string topCode = txt_Topic_Code.Text;
                if (string.IsNullOrEmpty(topCode))
                {
                    errorProvider1.SetError(txt_Topic_Code, "提示：课题编号不能为空");
                    result = false;
                }
                else if (topCode.Contains(" "))
                {
                    errorProvider1.SetError(txt_Topic_Code, "提示：课题编号不能含有空格");
                    result = false;
                }
                else
                {
                    object objectID = tab_Topic_Info.Tag;
                    int count = SqlHelper.ExecuteCountQuery("SELECT COUNT(pi_id) FROM " +
                        $"(SELECT pi_id, pi_code, pi_orga_id FROM project_info " +
                        $"UNION ALL SELECT ti_id, ti_code, ti_orga_id FROM topic_info " +
                        $"UNION ALL SELECT si_id, si_code, si_orga_id FROM subject_info) A " +
                        $"WHERE A.pi_code='{topCode}' AND A.pi_orga_id='{togle.Tag}' AND A.pi_id<>'{objectID}';");
                    if (count > 0)
                    {
                        errorProvider1.SetError(txt_Topic_Code, "提示：此项目/课题编号已存在");
                        result = false;
                    }
                }

                string funds = txt_Topic_Funds.Text;
                if (!string.IsNullOrEmpty(funds))
                {
                    if (!Regex.IsMatch(funds, "^[0-9]+(.[0-9]{1,2})?$"))
                    {
                        errorProvider1.SetError(txt_Topic_Funds, "提示：请输入合法经费");
                        result = false;
                    }
                }

                string year = txt_Topic_Year.Text;
                if (string.IsNullOrEmpty(year) || !Regex.IsMatch(year, "^\\d{4}$"))
                {
                    errorProvider1.SetError(txt_Topic_Year, "提示：请输入有效的立项年度");
                    result = false;
                }

                if (string.IsNullOrEmpty(txt_Topic_Unit.Text))
                {
                    errorProvider1.SetError(txt_Topic_Unit, "提示：承担单位不能为空");
                    result = false;
                }
                if (string.IsNullOrEmpty(txt_Topic_ProUser.Text))
                {
                    errorProvider1.SetError(txt_Topic_ProUser, "提示：负责人不能为空");
                    result = false;
                }

                string startDate = txt_Topic_StartTime.Text;
                if (!string.IsNullOrEmpty(startDate))
                {
                    if (!Regex.IsMatch(startDate, "^\\d{4}-\\d{2}-\\d{2}$") || !DateTime.TryParse(startDate, out DateTime time))
                    {
                        errorProvider1.SetError(dtp_Topic_StartTime, "提示：请输入yyyy-MM-dd格式的有效日期");
                        result = false;
                    }
                }
                string endDate = txt_Topic_EndTime.Text;
                if (!string.IsNullOrEmpty(endDate))
                {
                    if (!Regex.IsMatch(endDate, "^\\d{4}-\\d{2}-\\d{2}$") || !DateTime.TryParse(endDate, out DateTime time))
                    {
                        errorProvider1.SetError(dtp_Topic_EndTime, "提示：请输入yyyy-MM-dd格式的有效日期");
                        result = false;
                    }
                }

            }
            else if (name.Contains("Subject"))
            {
                string subCode = txt_Subject_Code.Text.Trim();
                if (string.IsNullOrEmpty(subCode))
                {
                    errorProvider1.SetError(txt_Subject_Code, "提示：课题编号不能为空");
                    result = false;
                }
                else if (subCode.Contains(" "))
                {
                    errorProvider1.SetError(txt_Subject_Code, "提示：子课题编号不能含有空格");
                    result = false;
                }
                else
                {
                    object objectID = tab_Subject_Info.Tag;
                    int count = SqlHelper.ExecuteCountQuery("SELECT COUNT(pi_id) FROM (" +
                        "SELECT pi_id, pi_code, pi_orga_id FROM project_info UNION ALL " +
                        "SELECT ti_id, ti_code, ti_orga_id FROM topic_info UNION ALL " +
                        "SELECT si_id, si_code, si_orga_id FROM subject_info) A " +
                        $"WHERE A.pi_code='{subCode}' AND A.pi_orga_id='{togle.Tag}' AND A.pi_id<>'{objectID}';");
                    if (count > 0)
                    {
                        errorProvider1.SetError(txt_Subject_Code, "提示：子课题编号已存在");
                        result = false;
                    }
                }

                string funds = txt_Subject_Funds.Text;
                if (!string.IsNullOrEmpty(funds))
                {
                    if (!Regex.IsMatch(funds, "^[0-9]+(.[0-9]{1,2})?$"))
                    {
                        errorProvider1.SetError(txt_Subject_Funds, "提示：请输入合法经费");
                        result = false;
                    }
                }

                string year = txt_Subject_Year.Text;
                if (string.IsNullOrEmpty(year) || !Regex.IsMatch(year, "^\\d{4}$"))
                {
                    errorProvider1.SetError(txt_Subject_Year, "提示：请输入有效的立项年度");
                    result = false;
                }
                if (string.IsNullOrEmpty(txt_Subject_Unit.Text))
                {
                    errorProvider1.SetError(txt_Subject_Unit, "提示：承担单位不能为空");
                    result = false;
                }
                if (string.IsNullOrEmpty(txt_Subject_ProUser.Text))
                {
                    errorProvider1.SetError(txt_Subject_ProUser, "提示：负责人不能为空");
                    result = false;
                }

                string startDate = txt_Subject_StartTime.Text;
                if (!string.IsNullOrEmpty(startDate))
                {
                    if (!Regex.IsMatch(startDate, "^\\d{4}-\\d{2}-\\d{2}$") || !DateTime.TryParse(startDate, out DateTime time))
                    {
                        errorProvider1.SetError(dtp_Subject_StartTime, "提示：请输入yyyy-MM-dd格式的有效日期");
                        result = false;
                    }
                }
                string endDate = txt_Subject_EndTime.Text;
                if (!string.IsNullOrEmpty(endDate))
                {
                    if (!Regex.IsMatch(endDate, "^\\d{4}-\\d{2}-\\d{2}$") || !DateTime.TryParse(endDate, out DateTime time))
                    {
                        errorProvider1.SetError(dtp_Subject_EndTime, "提示：请输入yyyy-MM-dd格式的有效日期");
                        result = false;
                    }
                }

            }
            else if (name.Contains("Special"))
            {
                if (string.IsNullOrEmpty(txt_Special_Unit.Text))
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
            foreach (DataGridViewRow row in view.Rows)
            {
                object reason = row.Cells[key + "reason"].Value;
                object remark = row.Cells[key + "remark"].Value;
                object flag = row.Tag;
                if (flag != null && (reason == null || remark == null))
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
            for (int i = 0; i < removeIdList.Count; i++)
            {
                if (removeIdList[i] != null)
                {
                    //收集文件号（供重新选取）
                    object fileId = SqlHelper.ExecuteOnlyOneQuery($"SELECT pfl_file_id FROM processing_file_list WHERE pfl_id='{removeIdList[i]}';");
                    if (fileId != null)
                        fileString += $"'{fileId}',";

                    //删除当前文件
                    SqlHelper.ExecuteNonQuery($"DELETE FROM processing_file_list WHERE pfl_id='{removeIdList[i]}';");
                }
            }

            //重置文件备份表中的状态为0
            if (!string.IsNullOrEmpty(fileString))
            {
                fileString = fileString.Substring(0, fileString.Length - 1);
                SqlHelper.ExecuteNonQuery($"UPDATE backup_files_info SET bfi_state=0 WHERE bfi_id IN ({fileString});");
            }
            removeIdList.Clear();
        }

        private void GoToTreeList()
        {
            if (workType == WorkType.PaperWork)
            {
                if (controlType == ControlType.Plan)
                    LoadTreeList(PLAN_ID, ControlType.Plan);
                else
                    LoadTreeList(lbl_Imp_Name.Tag, ControlType.Special);
            }
            else
                LoadTreeList(tab_Plan_Info.Tag, ControlType.Default);
        }

        /// <summary>
        /// 检验文件列表是否可以保存
        /// </summary>
        private bool CheckFileList(DataGridViewRowCollection rows, string key)
        {
            bool result = true;
            for (int i = 0; i < rows.Count - 1; i++)
            {
                //文件名称
                DataGridViewCell cellName = rows[i].Cells[key + "name"];
                if (string.IsNullOrEmpty(ToolHelper.GetValue(cellName.Value)))
                {
                    cellName.ErrorText = "温馨提示：文件名不能为空。";
                    result = false;
                }
                else
                {
                    cellName.ErrorText = null;
                    for (int j = i + 1; j < rows.Count - 1; j++)
                    {
                        DataGridViewCell cell2 = rows[j].Cells[key + "name"];
                        if (cellName.Value.Equals(cell2.Value))
                        {
                            cellName.ErrorText = $"温馨提示：与{j + 1}行的文件名重复。";
                            result = false;
                            break;
                        }
                        else
                            cellName.ErrorText = null;
                    }
                }

                //检测文件编号重复
                DataGridViewCell cellCode = rows[i].Cells[key + "code"];
                if (string.IsNullOrEmpty(ToolHelper.GetValue(cellCode.Value)))
                {
                    cellCode.ErrorText = "温馨提示：文件编号不能为空。";
                    result = false;
                }
                else
                {
                    cellCode.ErrorText = null;
                    for (int j = i + 1; j < rows.Count - 1; j++)
                    {
                        DataGridViewCell cell2 = rows[j].Cells[key + "code"];
                        if (cellCode.Value.Equals(cell2.Value))
                        {
                            cellCode.ErrorText = $"温馨提示：与{j + 1}行的文件编号重复。";
                            result = false;
                            break;
                        }
                        else
                            cellCode.ErrorText = null;
                    }
                }

                //页数
                //DataGridViewCell pagesCell = rows[i].Cells[key + "pages"];
                //if (pagesCell.Value == null)
                //{
                //    pagesCell.ErrorText = "温馨提示：页数不能为0或空。";
                //    result = false;
                //}
                //else
                //{
                //    if (!Regex.IsMatch(ToolHelper.GetValue(pagesCell.Value), "^[0-9]{1,4}$"))
                //    {
                //        pagesCell.ErrorText = "温馨提示：请输入小于4位数的合法数字。";
                //        result = false;
                //    }
                //    else
                //        pagesCell.ErrorText = null;
                //}

                //份数
                DataGridViewCell countCell = rows[i].Cells[key + "count"];
                if (!string.IsNullOrEmpty(ToolHelper.GetValue(countCell.Value)))
                {
                    bool flag = int.TryParse(ToolHelper.GetValue(countCell.Value), out int page);
                    if (!flag)
                    {
                        countCell.ErrorText = "温馨提示：请输入有效数字。";
                        result = false;
                    }
                    else
                        countCell.ErrorText = null;
                }

                //份数移交
                DataGridViewCell amountCell = rows[i].Cells[key + "amount"];
                if (!string.IsNullOrEmpty(ToolHelper.GetValue(amountCell.Value)))
                {
                    bool flag = int.TryParse(ToolHelper.GetValue(amountCell.Value), out int page);
                    if (!flag)
                    {
                        amountCell.ErrorText = "温馨提示：请输入有效数字。";
                        result = false;
                    }
                    else
                        amountCell.ErrorText = null;
                }

                bool isOtherType = "其他".Equals(ToolHelper.GetValue(rows[i].Cells[key + "categor"].FormattedValue).Trim());
                DataGridViewCell categorCode = rows[i].Cells[key + "categorname"];
                if (isOtherType)
                {
                    if (categorCode.Value == null || string.IsNullOrEmpty(ToolHelper.GetValue(categorCode.Value).Trim()))
                    {
                        categorCode.ErrorText = "提示：类型名称不能为空。";
                        result = false;
                        break;
                    }
                    else
                        categorCode.ErrorText = null;

                    //文件类别是否已存在
                    bool flag = rows[i].Cells[key + "id"].Tag == null;//只有新增行才判断是否重复
                    if (flag)
                    {
                        string codeParam = ToolHelper.GetValue(cellCode.Value);
                        if (string.IsNullOrEmpty(cellCode.ErrorText) && !string.IsNullOrEmpty(codeParam))
                        {
                            codeParam = codeParam.Split('-')[0];
                            int index = SqlHelper.ExecuteCountQuery($"SELECT COUNT(dd_id) FROM data_dictionary WHERE dd_name = '{codeParam}'");
                            if (index > 0)
                            {
                                cellCode.ErrorText = "提示：文件类别已存在。";
                                result = false;
                            }
                            else
                                cellCode.ErrorText = null;
                        }
                    }
                }
                else
                    categorCode.ErrorText = null;

                DataGridViewCell dateCell = rows[i].Cells[key + "date"];
                if (!string.IsNullOrEmpty(ToolHelper.GetValue(dateCell.Value)))
                {

                    if (!Regex.IsMatch(ToolHelper.GetValue(dateCell.Value), "\\d{4}-\\d{2}-\\d{2}"))
                    {
                        dateCell.ErrorText = "提示：请输入格式为 yyyy-MM-dd 的有效日期。";
                        result = false;
                    }
                    else
                    {
                        if (!DateTime.TryParse(ToolHelper.GetValue(dateCell.Value), out DateTime date))
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
            for (int i = 0; i < rowCount; i++)
            {
                DataGridViewRow row = dataGridView.Rows[i];
                object name = row.Cells[key + "name"].Value;
                if (name != null)
                {
                    object ismust = row.Tag;
                    object reason = row.Cells[key + "reason"].Value;
                    object remark = row.Cells[key + "remark"].Value;
                    object categor = row.Cells[key + "categor"].Value;
                    string _categor = ToolHelper.GetValue(categor);
                    if (!string.IsNullOrEmpty(_categor))
                    {
                        string[] _temp = _categor.Split(' ');
                        if (_temp.Length > 0 && !string.IsNullOrEmpty(_temp[0].Trim()))
                            _categor = _temp[0];
                    }
                    object rid = Guid.NewGuid().ToString();
                    sqlString.Append($"INSERT INTO processing_file_lost (pfo_id, pfo_categor, pfo_name, pfo_reason, pfo_remark, pfo_obj_id, pfo_ismust) " +
                        $"VALUES('{rid}', '{_categor}', '{name}', '{reason}', '{remark}', '{objid}', '{ismust}');");
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
            if (controlType == ControlType.Project)
            {
                string code = txt_Project_Code.Text;
                string name = txt_Project_Name.Text;
                string type = string.Empty;
                string filed = txt_Project_Field.Text;
                string theme = txt_Project_Theme.Text;
                string funds = GetFloatValue(txt_Project_Funds.Text, 2);
                string starttime = txt_Project_StartTime.Text;
                string endtime = txt_Project_EndTime.Text;
                string year = txt_Project_Year.Text;
                object unit = txt_Project_Unit.Text;
                object province = cbo_Project_Province.SelectedValue;
                string unituser = txt_Project_UnitUser.Text;
                string objuser = txt_Project_ProUser.Text;
                string intro = txt_Project_Intro.Text;

                string updateSql = "UPDATE project_info SET " +
                    $"pi_code = '{code}'" +
                    $",pi_name = N'{name}' " +
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
                    $",pi_intro = N'{intro}'" +
                    $",pi_source_id = '{Tag}'" +
                    $",pi_orga_id = '{togle.Tag}'" +
                    $" WHERE pi_id='{objid}'";
                SqlHelper.ExecuteNonQuery(updateSql);
            }
            else if (controlType == ControlType.Topic)
            {
                string code = txt_Topic_Code.Text;
                string name = txt_Topic_Name.Text;
                string field = txt_Topic_Field.Text;
                string theme = txt_Topic_Theme.Text;
                string funds = GetFloatValue(txt_Topic_Funds.Text, 2);
                string starttime = txt_Topic_StartTime.Text;
                string endtime = txt_Topic_EndTime.Text;
                string year = txt_Topic_Year.Text;
                object unit = txt_Topic_Unit.Text;
                object province = cbo_Topic_Province.SelectedValue;
                string unituser = txt_Topic_UnitUser.Text;
                string objuser = txt_Topic_ProUser.Text;
                string intro = txt_Topic_Intro.Text;

                string updateSql = "UPDATE topic_info SET " +
                    $"ti_code = '{code}'" +
                    $",ti_name = N'{name}' " +
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
                    $",ti_intro = N'{intro}'" +
                    $",ti_source_id = '{Tag}'" +
                    $",ti_orga_id = '{togle.Tag}'" +
                    $" WHERE ti_id='{objid}'";
                SqlHelper.ExecuteNonQuery(updateSql);
            }
            else if (controlType == ControlType.Subject)
            {
                string code = txt_Subject_Code.Text;
                string name = txt_Subject_Name.Text;
                string field = txt_Subject_Field.Text;
                string theme = txt_Subject_Theme.Text;
                string fund = GetFloatValue(txt_Subject_Funds.Text, 2);
                string starttime = txt_Subject_StartTime.Text;
                string endtime = txt_Subject_EndTime.Text;
                string year = txt_Subject_Year.Text;
                object unit = txt_Subject_Unit.Text;
                string unituser = txt_Subject_Unituser.Text;
                string objuser = txt_Subject_ProUser.Text;
                object province = cbo_Subject_Province.SelectedValue;
                string intro = txt_Subject_Intro.Text;

                string updateSql = "UPDATE subject_info SET " +
                    $"[si_code] = '{code}'" +
                    $",[si_name] = N'{name}'" +
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
                    $",[si_intro] = N'{intro}'" +
                    $",si_source_id = '{Tag}'" +
                    $",si_orga_id = '{togle.Tag}'" +
                    $" WHERE si_id='{objid}'";
                SqlHelper.ExecuteNonQuery(updateSql);
            }
            else if (controlType == ControlType.Special)
            {
                string code = txt_Special_Code.Text;
                string name = txt_Special_Name.Text;
                string unit = txt_Special_Unit.Text;

                string updateSql = "UPDATE imp_dev_info SET " +
                    $"imp_code = '{code}'" +
                    $",imp_name = N'{name}'" +
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
        private object AddBasicInfo(object parentId, ControlType type)
        {
            string primaryKey = Guid.NewGuid().ToString();
            if (type == ControlType.Plan)
            {
                object code = lbl_Plan_Name.Tag;
                string name = lbl_Plan_Name.Text.Replace("'", "''");
                string intro = txt_Plan_Intro.Text;
                string diskIds = GetDiskIdsByPid(OBJECT_ID);
                string insertSql = "INSERT INTO project_info(pi_id, trc_id, pi_code, pi_name, pi_intro, pi_obj_id, pi_categor, pi_submit_status, pi_worker_id, pi_source_id, pi_orga_id) VALUES" +
                    $"('{primaryKey}', '{diskIds}', '{code}', N'{name}', N'{intro}', '{OBJECT_ID}', '{(int)type}', '{1}', '{UserHelper.GetUser().UserKey}', '{Tag}', '{togle.Tag}')";
                SqlHelper.ExecuteNonQuery(insertSql);
            }
            else if (type == ControlType.Project)
            {
                string code = txt_Project_Code.Text;
                string name = txt_Project_Name.Text.Replace("'", "''");
                string filed = txt_Project_Field.Text;
                string theme = txt_Project_Theme.Text;
                string funds = GetFloatValue(txt_Project_Funds.Text, 2);
                string starttime = txt_Project_StartTime.Text;
                string endtime = txt_Project_EndTime.Text;
                string year = txt_Project_Year.Text;
                object unit = txt_Project_Unit.Text;
                object province = cbo_Project_Province.SelectedValue;
                string unituser = txt_Project_UnitUser.Text;
                string objuser = txt_Project_ProUser.Text;
                string intro = txt_Project_Intro.Text.Replace("'", "''");

                string insertSql = "INSERT INTO project_info(pi_id, pi_code, pi_name, pi_field, pb_theme, pi_funds, pi_start_datetime, pi_end_datetime, pi_year, pi_unit, pi_uniter" +
                    ",pi_province, pi_prouser, pi_intro, pi_work_status, pi_obj_id, pi_categor, pi_submit_status, pi_worker_id, pi_worker_date, pi_source_id, pi_orga_id)" +
                    "VALUES" +
                    $"('{primaryKey}', '{code}', N'{name}', '{filed}', '{theme}', '{funds}', '{starttime}', '{endtime}', '{year}', '{unit}', '{unituser}'" +
                    $",'{province}','{objuser}', N'{intro}', '{(int)WorkStatus.Default}', '{parentId}',{(int)type}, {1}, '{UserHelper.GetUser().UserKey}', '{DateTime.Now}', '{Tag}', '{togle.Tag}')";

                SqlHelper.ExecuteNonQuery(insertSql);
                LogsHelper.AddWorkLog(WorkLogType.Project_Topic, 1, OBJECT_ID, 1, primaryKey);
            }
            else if (type == ControlType.Topic)
            {
                string code = txt_Topic_Code.Text;
                string name = txt_Topic_Name.Text.Replace("'", "''");
                string field = txt_Topic_Field.Text;
                string theme = txt_Topic_Theme.Text;
                string funds = GetFloatValue(txt_Topic_Funds.Text, 2);
                string starttime = txt_Topic_StartTime.Text;
                string endtime = txt_Topic_EndTime.Text;
                string year = txt_Topic_Year.Text;
                object unit = txt_Topic_Unit.Text;
                object province = cbo_Topic_Province.SelectedValue;
                string unituser = txt_Topic_UnitUser.Text;
                string objuser = txt_Topic_ProUser.Text;
                string intro = txt_Topic_Intro.Text.Replace("'", "''");
                //判断是直属课题【-3】还是项目下的课题【3】
                int categorType = SqlHelper.ExecuteCountQuery($"SELECT COUNT(pi_id) FROM project_info WHERE pi_categor=2 AND pi_id='{parentId}'");
                int categor = categorType > 0 ? (int)type : -(int)type;
                string insertSql = "INSERT INTO topic_info(ti_id, ti_code, ti_name, ti_field, tb_theme, ti_funds, ti_start_datetime, ti_end_datetime, ti_year, ti_unit, ti_uniter" +
                    ",ti_province, ti_prouser, ti_intro, ti_work_status, ti_obj_id, ti_categor, ti_submit_status, ti_worker_id, ti_worker_date, ti_source_id, ti_orga_id)" +
                    "VALUES" +
                    $"('{primaryKey}', '{code}', N'{name}',  '{field}', '{theme}', '{funds}', '{starttime}', '{endtime}', '{year}', '{unit}', '{unituser}'" +
                    $",'{province}','{objuser}', N'{intro}', 0, '{parentId}', '{categor}', 1, '{UserHelper.GetUser().UserKey}', '{DateTime.Now}', '{Tag}', '{togle.Tag}')";

                SqlHelper.ExecuteNonQuery(insertSql);
                LogsHelper.AddWorkLog(categor == 3 ? WorkLogType.Topic_Subject : WorkLogType.Project_Topic, 1, OBJECT_ID, 1, primaryKey);
            }
            else if (type == ControlType.Subject)
            {
                string code = txt_Subject_Code.Text;
                string name = txt_Subject_Name.Text.Replace("'", "''");
                string planType = string.Empty;
                string field = txt_Subject_Field.Text;
                string theme = txt_Subject_Theme.Text;
                string funds = GetFloatValue(txt_Subject_Funds.Text, 2);
                string starttime = txt_Subject_StartTime.Text;
                string endtime = txt_Subject_EndTime.Text;
                string year = txt_Subject_Year.Text;
                object unit = txt_Subject_Unit.Text;
                string unituser = txt_Subject_Unituser.Text;
                string objuser = txt_Subject_ProUser.Text;
                object province = cbo_Subject_Province.SelectedValue;
                string intro = txt_Subject_Intro.Text.Replace("'", "''");

                string insertSql = "INSERT INTO subject_info(si_id, si_code, si_name, si_field, si_theme, si_funds, si_start_datetime, si_end_datetime, si_year, si_unit, si_uniter" +
                    ", si_province, si_prouser, si_intro, si_obj_id, si_work_status, si_categor, si_submit_status, si_worker_id, si_worker_date, si_source_id, si_orga_id)" +
                   $" VALUES ('{primaryKey}', '{code}', N'{name}', '{field}', '{theme}', '{funds}', '{starttime}', '{endtime}', '{year}', '{unit}', '{unituser}', '{province}', '{objuser}'" +
                   $", N'{intro}', '{parentId}', 1, '{(int)type}', 1, '{UserHelper.GetUser().UserKey}', '{DateTime.Now}', '{Tag}', '{togle.Tag}')";
                SqlHelper.ExecuteNonQuery(insertSql);
                LogsHelper.AddWorkLog(WorkLogType.Topic_Subject, 1, OBJECT_ID, 1, primaryKey);
            }
            else if (type == ControlType.Imp)
            {
                string name = lbl_Imp_Name.Text;
                object intro = txt_Imp_Intro.Text;
                string insertSql = "INSERT INTO imp_info(imp_id, imp_code, imp_name, imp_intro, pi_categor, imp_submit_status, imp_obj_id, imp_source_id, imp_type) " +
                    $"VALUES ('{primaryKey}', '{planCode}', N'{name}', N'{intro}', '{(int)type}', '{(int)ObjectSubmitStatus.NonSubmit}', '{parentId}', '{UserHelper.GetUser().UserKey}', {(int)type})";
                SqlHelper.ExecuteNonQuery(insertSql);
            }
            else if (type == ControlType.Special)
            {
                string code = txt_Special_Code.Text;
                string name = txt_Special_Name.Text;
                string unit = txt_Special_Unit.Text;
                string intro = txt_Special_Intro.Text;

                string insertSql = "INSERT INTO imp_dev_info ([imp_id], [imp_code], [imp_name], [imp_unit], [imp_intro], [pi_categor], [imp_submit_status], [imp_obj_id], [imp_source_id]) " +
                    $"VALUES ('{primaryKey}', '{code}', N'{name}', N'{unit}', '{intro}', 6, 1, '{parentId}', '{UserHelper.GetUser().UserKey}')";
                SqlHelper.ExecuteNonQuery(insertSql);
            }
            return primaryKey;
        }

        /// <summary>
        /// 根据批次ID获取其下的光盘ID列表
        /// </summary>
        /// <param name="batchID">批次ID</param>
        private string GetDiskIdsByPid(object batchID)
        {
            string querySQL = $"SELECT trc_id+';' FROM transfer_registraion_cd WHERE trp_id='{batchID}' FOR XML PATH('')";
            object value = SqlHelper.ExecuteOnlyOneQuery(querySQL);
            return ToolHelper.GetValue(value);
        }

        private string GetFloatValue(string text, int length)
        {
            if (!string.IsNullOrEmpty(text))
            {
                if (float.TryParse(text, out float result))
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
        /// 新增文件信息
        /// </summary>
        /// <param name="key">当前表格列名前缀</param>
        /// <param name="row">当前待保存的行</param>
        /// <param name="parentId">父对象ID</param>
        /// <returns>新增信息主键</returns>
        private object AddFileInfo(string key, DataGridViewRow row, object parentId, int sort)
        {
            StringBuilder nonQuerySql = new StringBuilder();
            string _fileId = ToolHelper.GetValue(row.Cells[key + "id"].Value);
            object stage = row.Cells[key + "stage"].Value;
            object categor = row.Cells[key + "categor"].Value;
            object categorName = row.Cells[key + "categorname"].Value;
            object name = ToolHelper.GetValue(row.Cells[key + "name"].Value).Replace("'", "''");
            object user = ToolHelper.GetValue(row.Cells[key + "user"].Value).Replace("'", "''");
            object type = row.Cells[key + "type"].Value;
            int pages = ToolHelper.GetIntValue(row.Cells[key + "pages"].Value, 0);
            object count = row.Cells[key + "count"].Value;
            object amount = row.Cells[key + "amount"].Value;
            object code = row.Cells[key + "code"].Value;
            object date = row.Cells[key + "date"].Value;
            object unit = row.Cells[key + "unit"].Value;
            string carrier = ToolHelper.GetValue(row.Cells[key + "carrier"].Value);
            bool isOtherType = "其他".Equals(row.Cells[key + "categor"].FormattedValue);
            if (isOtherType)
            {
                categor = Guid.NewGuid().ToString();
                string value = ToolHelper.GetValue(code).Split('-')[0];
                int _sort = ((DataGridViewComboBoxCell)row.Cells[key + "categor"]).Items.Count - 1;

                object dicId = SqlHelper.ExecuteOnlyOneQuery($"SELECT dd_id FROM data_dictionary WHERE dd_name='{value}' AND dd_pId='{stage}'");
                if (dicId != null)
                {
                    categor = dicId;
                    nonQuerySql.Append($"DELETE FROM data_dictionary WHERE dd_name='{value}' AND dd_pId='{stage}';");
                }
                nonQuerySql.Append("INSERT INTO data_dictionary (dd_id, dd_name, dd_pId, dd_sort, extend_3, extend_4) " +
                    $"VALUES('{categor}', '{value}', '{stage}', '{_sort}', '{categorName}', '{1}');");
            }
            //更新
            if (!string.IsNullOrEmpty(_fileId))
            {
                nonQuerySql.Append($"UPDATE processing_file_list SET pfl_stage='{stage}', pfl_categor='{categor}', pfl_code='{code}', pfl_name=N'{name}', pfl_user='{user}', pfl_type='{type}', pfl_pages='{pages}'," +
                    $"pfl_count='{count}', pfl_amount='{amount}', pfl_date='{date}', pfl_unit='{unit}', pfl_carrier='{carrier}', pfl_sort='{sort}' WHERE pfl_id='{_fileId}';");
            }
            //新增
            else
            {
                _fileId = Guid.NewGuid().ToString();
                nonQuerySql.Append("INSERT INTO processing_file_list (pfl_id, pfl_code, pfl_stage, pfl_categor, pfl_name, pfl_user, pfl_type, pfl_pages, pfl_count, pfl_amount, pfl_date, pfl_unit, pfl_carrier, pfl_obj_id, pfl_sort, pfl_worker_id, pfl_worker_date) " +
                    $"VALUES( '{_fileId}', '{code}', '{stage}', '{categor}', N'{name}', '{user}', '{type}', '{pages}', '{count}', '{amount}', '{date}', '{unit}', '{carrier}', '{parentId}', '{sort}', '{UserHelper.GetUser().UserKey}', '{DateTime.Now}');");
                if (!string.IsNullOrEmpty(carrier))
                {
                    string carCode = SqlHelper.GetValueByKey(carrier, "dd_code");
                    if ("ZT_ZZ".Equals(carCode))
                        LogsHelper.AddWorkLog(WorkLogType.File, 1, OBJECT_ID, 1, null);
                    else
                        LogsHelper.AddWorkLog(WorkLogType.File_Electronic, 1, OBJECT_ID, 1, null);
                }
                else
                    LogsHelper.AddWorkLog(WorkLogType.File, 1, OBJECT_ID, 1, null);
                if (pages > 0)
                    LogsHelper.AddWorkLog(WorkLogType.Pages, pages, OBJECT_ID, 1, null);
            }
            SqlHelper.ExecuteNonQuery(nonQuerySql.ToString());
            return _fileId;
        }

        /// <summary>
        /// 加载树
        /// </summary>
        /// <param name="planId">计划ID</param>
        private void LoadTreeList(object planId, ControlType type)
        {
            SearchText.Properties.Items.Clear();
            treeView.BeginUpdate();
            treeView.Nodes.Clear();
            treeView.SelectedNode = null;
            TreeNode treeNode = null;
            //纸本加工 - 普通计划
            if (workType == WorkType.PaperWork_Plan)
            {
                if (isBacked)
                {
                    DataRow row = SqlHelper.ExecuteSingleRowQuery($"SELECT pi_id, pi_name, pi_worker_id, pi_submit_status FROM project_info WHERE pi_id='{planId}'");
                    if (row != null)
                    {
                        treeNode = new TreeNode()
                        {
                            Name = ToolHelper.GetValue(row["pi_id"]),
                            Text = ToolHelper.GetValue(row["pi_name"]),
                            Tag = ControlType.Plan,
                            ForeColor = GetForeColorByState(row["pi_submit_status"]),
                        };
                    }
                }
                else
                {
                    DataRow row = SqlHelper.ExecuteSingleRowQuery($"SELECT pi_id, pi_name, pi_worker_id, pi_submit_status FROM project_info WHERE pi_id='{planId}'");
                    if (row != null)
                    {
                        treeNode = new TreeNode()
                        {
                            Name = ToolHelper.GetValue(row["pi_id"]),
                            Text = ToolHelper.GetValue(row["pi_name"]),
                            Tag = ControlType.Plan,
                            ForeColor = GetForeColorByState(row["pi_submit_status"]),
                        };
                        if (!UserHelper.GetUser().UserKey.Equals(row["pi_worker_id"]) && !(row["pi_worker_id"] is DBNull))
                            treeNode.ForeColor = DisEnbleColor;
                        //根据【计划】查询【项目/课题】集
                        DataTable proTable = SqlHelper.ExecuteQuery("SELECT * FROM (" +
                             "SELECT pi_id, pi_code, pi_categor, pi_worker_id, pi_submit_status, pi_obj_id FROM project_info UNION ALL " +
                            $"SELECT ti_id, ti_code, ti_categor, ti_worker_id, ti_submit_status, ti_obj_id FROM topic_info) A WHERE pi_obj_id='{treeNode.Name}' ORDER BY pi_code");
                        //根据【计划】查询其他补录批次集
                        if (additRecords != null && additRecords.Length > 0)
                        {
                            string querySql = $"SELECT pi_id FROM project_info WHERE pi_categor=1 AND pi_obj_id IN({ToolHelper.GetStringBySplit(additRecords, ",", "'")})";
                            object[] planIds = SqlHelper.ExecuteSingleColumnQuery(querySql);
                            if (planIds.Length > 0)
                            {
                                DataTable addTable = SqlHelper.ExecuteQuery("SELECT * FROM (" +
                                     "SELECT pi_id, pi_code, pi_categor, pi_worker_id, pi_submit_status, pi_obj_id FROM project_info WHERE pi_categor=2 UNION ALL " +
                                     "SELECT ti_id, ti_code, ti_categor, ti_worker_id, ti_submit_status, ti_obj_id FROM topic_info WHERE ti_categor=-3) A " +
                                    $"WHERE pi_obj_id IN ({ToolHelper.GetStringBySplit(planIds, ",", "'")}) ORDER BY pi_code");
                                foreach (DataRow dataRow in addTable.Rows)
                                {
                                    proTable.ImportRow(dataRow);
                                }
                            }
                        }
                        foreach (DataRow proRow in proTable.Rows)
                        {
                            TreeNode treeNode2 = new TreeNode()
                            {
                                Name = ToolHelper.GetValue(proRow["pi_id"]),
                                Text = ToolHelper.GetValue(proRow["pi_code"]),
                                Tag = ControlType.Project,
                                ForeColor = GetForeColorByState(proRow["pi_submit_status"]),
                            };
                            if (!UserHelper.GetUser().UserKey.Equals(proRow["pi_worker_id"]) && !(proRow["pi_worker_id"] is DBNull))
                                treeNode2.ForeColor = DisEnbleColor;
                            treeNode.Nodes.Add(treeNode2);

                            DataTable topTable = SqlHelper.ExecuteQuery("SELECT * FROM (" +
                                "SELECT ti_id, ti_code, ti_categor, ti_worker_id, ti_submit_status, ti_obj_id FROM topic_info UNION ALL " +
                               $"SELECT si_id, si_code, si_categor, si_worker_id, si_submit_status, si_obj_id FROM subject_info) A WHERE ti_obj_id='{treeNode2.Name}' ORDER BY ti_code");
                            foreach (DataRow topRow in topTable.Rows)
                            {
                                TreeNode treeNode3 = new TreeNode()
                                {
                                    Name = ToolHelper.GetValue(topRow["ti_id"]),
                                    Text = ToolHelper.GetValue(topRow["ti_code"]),
                                    Tag = ControlType.Topic,
                                    ForeColor = GetForeColorByState(topRow["ti_submit_status"]),
                                };
                                if (!UserHelper.GetUser().UserKey.Equals(topRow["ti_worker_id"]) && !(topRow["ti_worker_id"] is DBNull))
                                    treeNode3.ForeColor = DisEnbleColor;
                                treeNode2.Nodes.Add(treeNode3);

                                DataTable subTable = SqlHelper.ExecuteQuery($"SELECT si_id, si_code, si_categor, si_worker_id, si_submit_status FROM subject_info WHERE si_obj_id='{treeNode3.Name}' ORDER BY si_code");
                                foreach (DataRow subRow in subTable.Rows)
                                {
                                    TreeNode treeNode4 = new TreeNode()
                                    {
                                        Name = ToolHelper.GetValue(subRow["si_id"]),
                                        Text = ToolHelper.GetValue(subRow["si_code"]),
                                        Tag = ControlType.Subject,
                                        ForeColor = GetForeColorByState(subRow["si_submit_status"]),
                                    };
                                    if (!UserHelper.GetUser().UserKey.Equals(subRow["si_worker_id"]) && !(subRow["si_worker_id"] is DBNull))
                                        treeNode4.ForeColor = DisEnbleColor;
                                    treeNode3.Nodes.Add(treeNode4);
                                }
                            }
                        }
                    }
                    else
                    {
                        row = SqlHelper.ExecuteSingleRowQuery($"SELECT dd_id, dd_name FROM data_dictionary WHERE dd_id='{planId}'");
                        if (row != null)
                        {
                            treeNode = new TreeNode()
                            {
                                Name = ToolHelper.GetValue(row["dd_id"]),
                                Text = ToolHelper.GetValue(row["dd_name"]),
                                Tag = ControlType.Plan_Default
                            };
                        }
                    }
                }
            }
            //纸本加工 - 重大专项、重点研发
            else if (workType == WorkType.PaperWork_Imp || workType == WorkType.PaperWork_Special)
            {
                if (isBacked)
                {
                    //重点计划
                    if (type == ControlType.Imp)
                    {
                        object[] _obj = SqlHelper.ExecuteRowsQuery($"SELECT imp_id, imp_name, imp_source_id FROM imp_info WHERE imp_id='{planId}'");
                        treeNode = new TreeNode()
                        {
                            Name = ToolHelper.GetValue(_obj[0]),
                            Text = ToolHelper.GetValue(_obj[1]),
                            Tag = type
                        };
                    }
                    //重点计划 - 专项信息
                    else if (type == ControlType.Special)
                    {
                        DataRow row = SqlHelper.ExecuteSingleRowQuery($"SELECT * FROM imp_dev_info WHERE imp_id='{planId}'");
                        object[] _obj = SqlHelper.ExecuteRowsQuery($"SELECT imp_id, imp_name, imp_source_id FROM imp_info WHERE imp_id='{row["imp_obj_id"]}'");
                        treeNode = new TreeNode()
                        {
                            Name = ToolHelper.GetValue(_obj[0]),
                            Text = ToolHelper.GetValue(_obj[1]),
                            Tag = ControlType.Imp,
                            ForeColor = DisEnbleColor
                        };
                        TreeNode treeNode2 = new TreeNode()
                        {
                            Name = ToolHelper.GetValue(row["imp_id"]),
                            Text = ToolHelper.GetValue(row["imp_code"]),
                            Tag = ControlType.Special
                        };
                        treeNode.Nodes.Add(treeNode2);
                    }
                    //项目/课题
                    else if (type == ControlType.Plan)
                    {
                        DataRow planRow = SqlHelper.ExecuteSingleRowQuery($"SELECT pi_id, pi_name, pi_submit_status FROM project_info WHERE pi_id='{planId}'");
                        if (planRow != null)
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
                    //普通计划
                    else if (type == ControlType.Project)
                    {
                        DataRow proRow = SqlHelper.ExecuteSingleRowQuery("SELECT * FROM (" +
                            "SELECT pi_id, pi_name, pi_code, pi_obj_id, pi_submit_status FROM project_info UNION ALL " +
                           $"SELECT ti_id, ti_name, ti_code, ti_obj_id, ti_submit_status FROM topic_info) A WHERE pi_id='{planId}' ORDER BY pi_code");
                        if (proRow != null)
                        {
                            DataRow planRow = SqlHelper.ExecuteSingleRowQuery($"SELECT pi_id, pi_name FROM project_info WHERE pi_id='{proRow["pi_obj_id"]}' UNION ALL " +
                                $"SELECT dd_id, dd_name FROM data_dictionary WHERE dd_id='{proRow["pi_obj_id"]}'");
                            treeNode = new TreeNode()
                            {
                                Name = ToolHelper.GetValue(planRow["pi_id"]),
                                Text = ToolHelper.GetValue(planRow["pi_name"]),
                                Tag = ControlType.Plan,
                                ForeColor = DisEnbleColor
                            };
                            TreeNode proNode = new TreeNode()
                            {
                                Name = ToolHelper.GetValue(proRow["pi_id"]),
                                Text = ToolHelper.GetValue(proRow["pi_code"]),
                                Tag = ControlType.Project,
                                ForeColor = Convert.ToInt32(proRow["pi_submit_status"]) == 1 ? Color.Black : DisEnbleColor
                            };
                            treeNode.Nodes.Add(proNode);
                            DataTable topTable = SqlHelper.ExecuteQuery($"SELECT * FROM (" +
                                 "SELECT ti_id, ti_name, ti_code, ti_submit_status, ti_obj_id FROM topic_info UNION ALL " +
                                $"SELECT si_id, si_name, si_code, si_submit_status, si_obj_id FROM subject_info) A WHERE ti_obj_id='{proRow["pi_id"]}' AND ti_worker_id='{UserHelper.GetUser().UserKey}' ORDER BY ti_code");
                            foreach (DataRow _row in topTable.Rows)
                            {
                                TreeNode topNode = new TreeNode()
                                {
                                    Name = ToolHelper.GetValue(_row["ti_id"]),
                                    Text = ToolHelper.GetValue(_row["ti_code"]),
                                    Tag = ControlType.Topic,
                                    ForeColor = Convert.ToInt32(_row["ti_submit_status"]) == 1 ? Color.Black : DisEnbleColor
                                };
                                proNode.Nodes.Add(topNode);

                                DataTable subTable = SqlHelper.ExecuteQuery($"SELECT si_id, si_name, si_code, si_submit_status FROM subject_info WHERE si_obj_id='{_row["ti_id"]}' AND si_worker_id='{UserHelper.GetUser().UserKey}' ORDER BY si_code");
                                foreach (DataRow subRow in subTable.Rows)
                                {
                                    TreeNode subNode = new TreeNode()
                                    {
                                        Name = ToolHelper.GetValue(subRow["si_id"]),
                                        Text = ToolHelper.GetValue(subRow["si_code"]),
                                        Tag = ControlType.Subject,
                                        ForeColor = Convert.ToInt32(subRow["si_submit_status"]) == 1 ? Color.Black : DisEnbleColor
                                    };
                                    topNode.Nodes.Add(subNode);
                                }
                            }
                        }
                    }
                }
                else
                {
                    DataRow impRow = SqlHelper.ExecuteSingleRowQuery($"SELECT imp_id, imp_name, imp_submit_status, imp_source_id FROM imp_info WHERE imp_obj_id='{OBJECT_ID}'");
                    if (impRow != null)
                    {
                        treeNode = new TreeNode()
                        {
                            Name = ToolHelper.GetValue(impRow["imp_id"]),
                            Text = ToolHelper.GetValue(impRow["imp_name"]),
                            Tag = ControlType.Imp,
                            ForeColor = GetForeColorByState(impRow["imp_submit_status"]),
                        };
                        if (!impRow["imp_source_id"].Equals(UserHelper.GetUser().UserKey) && !(impRow["imp_source_id"] is DBNull))
                            treeNode.ForeColor = DisEnbleColor;
                        //根据重大专项查询具体专项信息
                        DataRow row = SqlHelper.ExecuteSingleRowQuery($"SELECT imp_id, imp_code, imp_submit_status,imp_source_id FROM imp_dev_info WHERE imp_obj_id='{treeNode.Name}'");
                        if (row != null)
                        {
                            TreeNode treeNode2 = new TreeNode()
                            {
                                Name = ToolHelper.GetValue(row["imp_id"]),
                                Text = ToolHelper.GetValue(row["imp_code"]),
                                Tag = ControlType.Special,
                                ForeColor = GetForeColorByState(row["imp_submit_status"]),
                            };
                            treeNode.Nodes.Add(treeNode2);
                            if (!row["imp_source_id"].Equals(UserHelper.GetUser().UserKey) && !(row["imp_source_id"] is DBNull))
                                treeNode2.ForeColor = DisEnbleColor;
                            //根据【专项】查询【项目/课题】集
                            DataTable proTable = SqlHelper.ExecuteQuery($"SELECT * FROM (" +
                                 "SELECT pi_id, pi_code, pi_submit_status, pi_worker_id, pi_obj_id FROM project_info UNION ALL " +
                                $"SELECT ti_id, ti_code, ti_submit_status, ti_worker_id, ti_obj_id FROM topic_info) A WHERE pi_obj_id='{treeNode2.Name}' ORDER BY pi_code");
                            //根据【专项】查询其他补录批次集
                            if (additRecords != null && additRecords.Length > 0)
                            {
                                string querySql = "SELECT idi.imp_id FROM imp_dev_info idi " +
                                    "INNER JOIN imp_info ii ON idi.imp_obj_id = ii.imp_id " +
                                    "INNER JOIN transfer_registration_pc trp ON ii.imp_obj_id = trp.trp_id " +
                                   $"WHERE trp.trp_id IN({ ToolHelper.GetStringBySplit(additRecords, ",", "'")})";

                                object[] speIds = SqlHelper.ExecuteSingleColumnQuery(querySql);
                                 if (speIds.Length > 0)
                                {
                                    DataTable addTable = SqlHelper.ExecuteQuery("SELECT * FROM (" +
                                         "SELECT pi_id, pi_code, pi_categor, pi_worker_id, pi_submit_status, pi_obj_id FROM project_info WHERE pi_categor=2 UNION ALL " +
                                         "SELECT ti_id, ti_code, ti_categor, ti_worker_id, ti_submit_status, ti_obj_id FROM topic_info WHERE ti_categor=-3) A " +
                                        $"WHERE pi_obj_id IN ({ToolHelper.GetStringBySplit(speIds, ",", "'")}) ORDER BY pi_code");
                                    foreach (DataRow dataRow in addTable.Rows)
                                    {
                                        proTable.ImportRow(dataRow);
                                    }
                                }
                            }
                            foreach (DataRow proRow in proTable.Rows)
                            {
                                TreeNode treeNode3 = new TreeNode()
                                {
                                    Name = ToolHelper.GetValue(proRow["pi_id"]),
                                    Text = ToolHelper.GetValue(proRow["pi_code"]),
                                    Tag = ControlType.Project,
                                    ForeColor = GetForeColorByState(proRow["pi_submit_status"]),
                                };
                                treeNode2.Nodes.Add(treeNode3);
                                if (!proRow["pi_worker_id"].Equals(UserHelper.GetUser().UserKey) && !(proRow["pi_worker_id"] is DBNull))
                                    treeNode3.ForeColor = DisEnbleColor;
                                //根据【项目/课题】查询【课题/子课题】集
                                DataTable list2 = SqlHelper.ExecuteQuery("SELECT * FROM (" +
                                    "SELECT ti_id, ti_code, ti_submit_status, ti_worker_id, ti_obj_id FROM topic_info UNION ALL " +
                                   $"SELECT si_id, si_code, si_submit_status, si_worker_id, si_obj_id FROM subject_info) A WHERE ti_obj_id='{treeNode3.Name}' ORDER BY ti_code");
                                foreach (DataRow topRow in list2.Rows)
                                {
                                    TreeNode treeNode4 = new TreeNode()
                                    {
                                        Name = ToolHelper.GetValue(topRow["ti_id"]),
                                        Text = ToolHelper.GetValue(topRow["ti_code"]),
                                        Tag = ControlType.Topic,
                                        ForeColor = GetForeColorByState(topRow["ti_submit_status"]),
                                    };
                                    treeNode3.Nodes.Add(treeNode4);
                                    if (!topRow["ti_worker_id"].Equals(UserHelper.GetUser().UserKey) && !(topRow["ti_worker_id"] is DBNull))
                                        treeNode4.ForeColor = DisEnbleColor;
                                    DataTable list3 = SqlHelper.ExecuteQuery($"SELECT si_id, si_code, si_submit_status, si_worker_id FROM subject_info WHERE si_obj_id='{treeNode4.Name}' ORDER BY si_code");
                                    foreach (DataRow subRow in list3.Rows)
                                    {
                                        TreeNode treeNode5 = new TreeNode()
                                        {
                                            Name = ToolHelper.GetValue(subRow["si_id"]),
                                            Text = ToolHelper.GetValue(subRow["si_code"]),
                                            Tag = ControlType.Subject,
                                            ForeColor = GetForeColorByState(subRow["si_submit_status"]),
                                        };
                                        treeNode4.Nodes.Add(treeNode5);
                                        if (!subRow["si_worker_id"].Equals(UserHelper.GetUser().UserKey) && !(subRow["si_worker_id"] is DBNull))
                                            treeNode5.ForeColor = DisEnbleColor;
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        impRow = SqlHelper.ExecuteSingleRowQuery($"SELECT dd_id, dd_name, '{UserHelper.GetUser().UserKey}' FROM data_dictionary WHERE dd_id='{planId}'");
                        treeNode = new TreeNode()
                        {
                            Name = ToolHelper.GetValue(impRow["dd_id"]),
                            Text = ToolHelper.GetValue(impRow["dd_name"]),
                            Tag = ControlType.Imp
                        };
                    }
                }
            }
            //项目|课题
            else if (workType == WorkType.ProjectWork)
            {
                if (isBacked)
                {
                    object _planId = SqlHelper.ExecuteOnlyOneQuery($"SELECT pi_obj_id FROM project_info WHERE pi_id='{planId}'");
                    if (_planId != null)
                    {
                        DataRow planRow = SqlHelper.ExecuteSingleRowQuery($"SELECT pi_id, pi_name FROM project_info WHERE pi_id='{_planId}'");
                        //计划>>项目
                        if (planRow != null)
                        {
                            treeNode = new TreeNode()
                            {
                                Name = ToolHelper.GetValue(planRow["pi_id"]),
                                Text = ToolHelper.GetValue(planRow["pi_name"]),
                                Tag = ControlType.Plan,
                                ForeColor = DisEnbleColor
                            };
                            //根据【计划】查询【项目/课题】集
                            DataRow row = SqlHelper.ExecuteSingleRowQuery($"SELECT pi_id, pi_code, pi_submit_status FROM project_info WHERE pi_id='{planId}';");
                            if (row != null)
                            {
                                TreeNode treeNode2 = new TreeNode()
                                {
                                    Name = ToolHelper.GetValue(row["pi_id"]),
                                    Text = ToolHelper.GetValue(row["pi_code"]),
                                    Tag = ControlType.Project,
                                    ForeColor = GetForeColorByState(row["pi_submit_status"]),
                                };
                                treeNode.Nodes.Add(treeNode2);
                                //根据【项目/课题】查询【课题/子课题】集
                                DataTable list2 = SqlHelper.ExecuteQuery($"SELECT ti_id, ti_code, ti_submit_status FROM topic_info WHERE ti_obj_id='{treeNode2.Name}' AND ti_worker_id='{UserHelper.GetUser().UserKey}' ORDER BY ti_code");
                                foreach (DataRow topRow in list2.Rows)
                                {
                                    TreeNode treeNode3 = new TreeNode()
                                    {
                                        Name = ToolHelper.GetValue(topRow["ti_id"]),
                                        Text = ToolHelper.GetValue(topRow["ti_code"]),
                                        Tag = ControlType.Topic,
                                        ForeColor = GetForeColorByState(topRow["ti_submit_status"]),
                                    };
                                    treeNode2.Nodes.Add(treeNode3);

                                    DataTable list3 = SqlHelper.ExecuteQuery($"SELECT si_id, si_code, si_submit_status FROM subject_info WHERE si_obj_id='{treeNode3.Name}' AND si_worker_id='{UserHelper.GetUser().UserKey}' ORDER BY si_code");
                                    foreach (DataRow subRow in list3.Rows)
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
                        //重大专项>>专项>>项目
                        else
                        {
                            DataRow speRow = SqlHelper.ExecuteSingleRowQuery($"SELECT imp_id, imp_name, imp_obj_id FROM imp_dev_info WHERE imp_id='{_planId}'");
                            if (speRow != null)
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
                                DataRow row = SqlHelper.ExecuteSingleRowQuery($"SELECT pi_id, pi_code, pi_categor, pi_submit_status, pi_worker_id FROM project_info WHERE pi_id='{planId}';");
                                if (row != null)
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
                                    for (int j = 0; j < list2.Count; j++)
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
                                        for (int k = 0; k < list3.Count; k++)
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
                    else
                    {
                        _planId = SqlHelper.ExecuteOnlyOneQuery($"SELECT ti_obj_id FROM topic_info WHERE ti_id='{planId}'");
                        if (_planId != null)
                        {
                            DataRow planRow = SqlHelper.ExecuteSingleRowQuery($"SELECT pi_id, pi_name FROM project_info WHERE pi_id='{_planId}'");
                            //计划>>课题
                            if (planRow != null)
                            {
                                treeNode = new TreeNode()
                                {
                                    Name = ToolHelper.GetValue(planRow["pi_id"]),
                                    Text = ToolHelper.GetValue(planRow["pi_name"]),
                                    Tag = ControlType.Plan,
                                    ForeColor = DisEnbleColor
                                };
                                DataRow topRow = SqlHelper.ExecuteSingleRowQuery($"SELECT ti_id, ti_code, ti_categor, ti_submit_status FROM topic_info WHERE ti_id='{planId}';");
                                if (topRow != null)
                                {
                                    TreeNode treeNode2 = new TreeNode()
                                    {
                                        Name = ToolHelper.GetValue(topRow["ti_id"]),
                                        Text = ToolHelper.GetValue(topRow["ti_code"]),
                                        Tag = ControlType.Topic,
                                        ForeColor = GetForeColorByState(topRow["ti_submit_status"]),
                                    };
                                    treeNode.Nodes.Add(treeNode2);

                                    DataTable list3 = SqlHelper.ExecuteQuery($"SELECT si_id, si_code, si_categor, si_submit_status FROM subject_info WHERE si_obj_id='{treeNode2.Name}' AND si_worker_id='{UserHelper.GetUser().UserKey}' ORDER BY si_code");
                                    foreach (DataRow subRow in list3.Rows)
                                    {
                                        TreeNode treeNode3 = new TreeNode()
                                        {
                                            Name = ToolHelper.GetValue(subRow["si_id"]),
                                            Text = ToolHelper.GetValue(subRow["si_code"]),
                                            Tag = ControlType.Subject,
                                            ForeColor = GetForeColorByState(subRow["si_submit_status"]),
                                        };
                                        treeNode2.Nodes.Add(treeNode3);
                                    }
                                }
                            }
                            //重大专项>>专项>>课题
                            else
                            {
                                DataRow speRow = SqlHelper.ExecuteSingleRowQuery($"SELECT imp_id, imp_name, imp_obj_id FROM imp_dev_info WHERE imp_id='{_planId}'");
                                if (speRow != null)
                                {
                                    DataRow impRow = SqlHelper.ExecuteSingleRowQuery($"SELECT imp_id, imp_name FROM imp_info WHERE imp_id='{speRow["imp_obj_id"]}'");
                                    if (impRow != null)
                                    {
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
                                        //根据【项目/课题】查询【课题/子课题】集
                                        DataRow _row = SqlHelper.ExecuteSingleRowQuery($"SELECT ti_id, ti_code, ti_submit_status, ti_worker_id FROM topic_info WHERE ti_id='{planId}';");
                                        if (_row != null)
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
                                            for (int k = 0; k < list3.Count; k++)
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
                }
                else
                {
                    object _planId = SqlHelper.ExecuteOnlyOneQuery($"SELECT pi_obj_id FROM project_info WHERE pi_id='{OBJECT_ID}';") ?? SqlHelper.ExecuteOnlyOneQuery($"SELECT ti_obj_id FROM topic_info WHERE ti_id='{OBJECT_ID}';");
                    if (_planId != null)
                    {
                        DataRow planRow = SqlHelper.ExecuteSingleRowQuery($"SELECT pi_id, pi_name FROM project_info WHERE pi_id='{_planId}'");
                        if (planRow != null)
                        {
                            treeNode = new TreeNode()
                            {
                                Name = ToolHelper.GetValue(planRow["pi_id"]),
                                Text = ToolHelper.GetValue(planRow["pi_name"]),
                                Tag = ControlType.Plan,
                                ForeColor = DisEnbleColor
                            };
                        }
                        else
                        {
                            DataRow _planRow = SqlHelper.ExecuteSingleRowQuery($"SELECT dd_id, dd_name FROM data_dictionary WHERE dd_id='{_planId}'");
                            treeNode = new TreeNode()
                            {
                                Name = ToolHelper.GetValue(planRow["dd_id"]),
                                Text = ToolHelper.GetValue(planRow["dd_name"]),
                                Tag = ControlType.Plan_Default,
                                ForeColor = DisEnbleColor
                            };
                        }
                        //根据【计划】查询【项目/课题】集
                        DataRow row = SqlHelper.ExecuteSingleRowQuery($"SELECT pi_id, pi_code, pi_submit_status FROM project_info WHERE pi_id='{OBJECT_ID}';");
                        if (row != null)
                        {
                            TreeNode treeNode2 = new TreeNode()
                            {
                                Name = ToolHelper.GetValue(row["pi_id"]),
                                Text = ToolHelper.GetValue(row["pi_code"]),
                                Tag = ControlType.Project,
                                ForeColor = GetForeColorByState(row["pi_submit_status"]),
                            };
                            treeNode.Nodes.Add(treeNode2);
                            //根据【项目/课题】查询【课题/子课题】集
                            DataTable topTable = SqlHelper.ExecuteQuery($"SELECT ti_id, ti_code, ti_submit_status FROM topic_info WHERE ti_obj_id='{treeNode2.Name}' AND ti_worker_id='{UserHelper.GetUser().UserKey}' ORDER BY ti_code");
                            foreach (DataRow topRow in topTable.Rows)
                            {
                                TreeNode treeNode3 = new TreeNode()
                                {
                                    Name = ToolHelper.GetValue(topRow["ti_id"]),
                                    Text = ToolHelper.GetValue(topRow["ti_code"]),
                                    Tag = ControlType.Topic,
                                    ForeColor = GetForeColorByState(topRow["ti_submit_status"]),
                                };
                                treeNode2.Nodes.Add(treeNode3);

                                DataTable subTable = SqlHelper.ExecuteQuery($"SELECT si_id, si_code, si_submit_status FROM subject_info WHERE si_obj_id='{treeNode3.Name}' AND si_worker_id='{UserHelper.GetUser().UserKey}' ORDER BY si_code");
                                foreach (DataRow subRow in subTable.Rows)
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
                        else
                        {
                            //根据【项目/课题】查询【课题/子课题】集
                            DataRow _row = SqlHelper.ExecuteSingleRowQuery($"SELECT ti_id, ti_code, ti_submit_status FROM topic_info WHERE ti_id='{OBJECT_ID}';");
                            if (_row != null)
                            {
                                TreeNode treeNode3 = new TreeNode()
                                {
                                    Name = ToolHelper.GetValue(_row["ti_id"]),
                                    Text = ToolHelper.GetValue(_row["ti_code"]),
                                    Tag = ControlType.Topic,
                                    ForeColor = GetForeColorByState(_row["ti_submit_status"]),
                                };
                                treeNode.Nodes.Add(treeNode3);

                                DataTable subTable = SqlHelper.ExecuteQuery($"SELECT si_id, si_code, si_submit_status FROM subject_info WHERE si_obj_id='{treeNode3.Name}' AND si_worker_id='{UserHelper.GetUser().UserKey}' ORDER BY si_code");
                                foreach (DataRow subRow in subTable.Rows)
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
            //光盘加工 - 普通计划
            else if (workType == WorkType.CDWork_Plan)
            {
                if (isBacked)
                {

                }
                else
                {
                    DataRow row = SqlHelper.ExecuteSingleRowQuery($"SELECT pi_id, pi_name, pi_worker_id, pi_submit_status FROM project_info WHERE pi_id='{planId}'");
                    if (row != null)
                    {
                        treeNode = new TreeNode()
                        {
                            Name = ToolHelper.GetValue(row["pi_id"]),
                            Text = ToolHelper.GetValue(row["pi_name"]),
                            Tag = ControlType.Plan,
                            ForeColor = GetForeColorByState(row["pi_submit_status"]),
                        };
                        //根据【计划】查询【项目/课题】集
                        DataTable proTable = SqlHelper.ExecuteQuery($"SELECT pi_id, pi_code, pi_categor, pi_worker_id, pi_submit_status FROM project_info WHERE pi_obj_id='{treeNode.Name}' UNION ALL " +
                        $"SELECT ti_id, ti_code, ti_categor, ti_worker_id, ti_submit_status FROM topic_info WHERE ti_obj_id='{treeNode.Name}'");
                        foreach (DataRow proRow in proTable.Rows)
                        {
                            TreeNode treeNode2 = new TreeNode()
                            {
                                Name = ToolHelper.GetValue(proRow["pi_id"]),
                                Text = ToolHelper.GetValue(proRow["pi_code"]),
                                Tag = ControlType.Project,
                                ForeColor = GetForeColorByState(proRow["pi_submit_status"]),
                            };
                            if (!UserHelper.GetUser().UserKey.Equals(proRow["pi_worker_id"]) && !(proRow["pi_worker_id"] is DBNull))
                                treeNode2.ForeColor = DisEnbleColor;
                            treeNode.Nodes.Add(treeNode2);

                            DataTable topTable = SqlHelper.ExecuteQuery($"SELECT ti_id, ti_code, ti_categor, ti_worker_id, ti_submit_status FROM topic_info WHERE ti_obj_id='{treeNode2.Name}' UNION ALL " +
                            $"SELECT si_id, si_code, si_categor, si_worker_id, si_submit_status FROM subject_info WHERE si_obj_id='{treeNode2.Name}'");
                            foreach (DataRow topRow in topTable.Rows)
                            {
                                TreeNode treeNode3 = new TreeNode()
                                {
                                    Name = ToolHelper.GetValue(topRow["ti_id"]),
                                    Text = ToolHelper.GetValue(topRow["ti_code"]),
                                    Tag = ControlType.Topic,
                                    ForeColor = GetForeColorByState(topRow["ti_submit_status"]),
                                };
                                if (!UserHelper.GetUser().UserKey.Equals(topRow["ti_worker_id"]) && !(topRow["ti_worker_id"] is DBNull))
                                    treeNode3.ForeColor = DisEnbleColor;
                                treeNode2.Nodes.Add(treeNode3);

                                DataTable subTable = SqlHelper.ExecuteQuery($"SELECT si_id, si_code, si_categor, si_worker_id, si_submit_status FROM subject_info WHERE si_obj_id='{treeNode3.Name}'");
                                foreach (DataRow subRow in subTable.Rows)
                                {
                                    TreeNode treeNode4 = new TreeNode()
                                    {
                                        Name = ToolHelper.GetValue(subRow["si_id"]),
                                        Text = ToolHelper.GetValue(subRow["si_code"]),
                                        Tag = ControlType.Subject,
                                        ForeColor = GetForeColorByState(subRow["si_submit_status"]),
                                    };
                                    if (!UserHelper.GetUser().UserKey.Equals(subRow["si_worker_id"]) && !(subRow["si_worker_id"] is DBNull))
                                        treeNode4.ForeColor = DisEnbleColor;
                                    treeNode3.Nodes.Add(treeNode4);
                                }
                            }
                        }
                    }
                    else
                    {
                        row = SqlHelper.ExecuteSingleRowQuery($"SELECT dd_id, dd_name FROM data_dictionary WHERE dd_id='{planId}'");
                        if (row != null)
                        {
                            treeNode = new TreeNode()
                            {
                                Name = ToolHelper.GetValue(row["dd_id"]),
                                Text = ToolHelper.GetValue(row["dd_name"]),
                                Tag = ControlType.Plan_Default
                            };
                        }
                    }
                }
            }
            //光盘加工 - 重大专项、重点研发
            else if (workType == WorkType.CDWork_Imp || workType == WorkType.CDWork_Special)
            {
                if (isBacked)
                {
                    //重点计划
                    if (type == ControlType.Imp)
                    {
                        object[] _obj = SqlHelper.ExecuteRowsQuery($"SELECT imp_id, imp_name, imp_source_id FROM imp_info WHERE imp_id='{planId}'");
                        treeNode = new TreeNode()
                        {
                            Name = ToolHelper.GetValue(_obj[0]),
                            Text = ToolHelper.GetValue(_obj[1]),
                            Tag = type
                        };
                    }
                    //重点计划 - 专项信息
                    else if (type == ControlType.Special)
                    {
                        DataRow row = SqlHelper.ExecuteSingleRowQuery($"SELECT * FROM imp_dev_info WHERE imp_id='{planId}'");
                        object[] _obj = SqlHelper.ExecuteRowsQuery($"SELECT imp_id, imp_name, imp_source_id FROM imp_info WHERE imp_id='{row["imp_obj_id"]}'");
                        treeNode = new TreeNode()
                        {
                            Name = ToolHelper.GetValue(_obj[0]),
                            Text = ToolHelper.GetValue(_obj[1]),
                            Tag = ControlType.Imp,
                            ForeColor = DisEnbleColor
                        };
                        TreeNode treeNode2 = new TreeNode()
                        {
                            Name = ToolHelper.GetValue(row["imp_id"]),
                            Text = ToolHelper.GetValue(row["imp_code"]),
                            Tag = ControlType.Special
                        };
                        treeNode.Nodes.Add(treeNode2);
                    }
                    //项目/课题
                    else if (type == ControlType.Plan)
                    {
                        DataRow planRow = SqlHelper.ExecuteSingleRowQuery($"SELECT pi_id, pi_name, pi_submit_status FROM project_info WHERE pi_id='{planId}'");
                        if (planRow != null)
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
                    //普通计划
                    else if (type == ControlType.Project)
                    {
                        DataRow proRow = SqlHelper.ExecuteSingleRowQuery($"SELECT pi_id, pi_name, pi_code, pi_obj_id, pi_submit_status FROM project_info WHERE pi_id='{planId}' UNION ALL " +
                        $"SELECT ti_id, ti_name, ti_code, ti_obj_id, ti_submit_status FROM topic_info WHERE ti_id='{planId}'");
                        if (proRow != null)
                        {
                            DataRow planRow = SqlHelper.ExecuteSingleRowQuery($"SELECT pi_id, pi_name FROM project_info WHERE pi_id='{proRow["pi_obj_id"]}' UNION ALL" +
                            $" SELECT dd_id, dd_name FROM data_dictionary WHERE dd_id='{proRow["pi_obj_id"]}'");
                            treeNode = new TreeNode()
                            {
                                Name = ToolHelper.GetValue(planRow["pi_id"]),
                                Text = ToolHelper.GetValue(planRow["pi_name"]),
                                Tag = ControlType.Plan,
                                ForeColor = DisEnbleColor
                            };
                            TreeNode proNode = new TreeNode()
                            {
                                Name = ToolHelper.GetValue(proRow["pi_id"]),
                                Text = ToolHelper.GetValue(proRow["pi_code"]),
                                Tag = ControlType.Project,
                                ForeColor = Convert.ToInt32(proRow["pi_submit_status"]) == 1 ? Color.Black : DisEnbleColor
                            };
                            treeNode.Nodes.Add(proNode);
                            DataTable topTable = SqlHelper.ExecuteQuery($"SELECT ti_id, ti_name, ti_code, ti_submit_status FROM topic_info WHERE ti_obj_id='{proRow["pi_id"]}' AND ti_worker_id='{UserHelper.GetUser().UserKey}' UNION ALL " +
                            $"SELECT si_id, si_name, si_code, si_submit_status FROM subject_info WHERE si_obj_id='{proRow["pi_id"]}' AND si_worker_id='{UserHelper.GetUser().UserKey}';");
                            foreach (DataRow _row in topTable.Rows)
                            {
                                TreeNode topNode = new TreeNode()
                                {
                                    Name = ToolHelper.GetValue(_row["ti_id"]),
                                    Text = ToolHelper.GetValue(_row["ti_code"]),
                                    Tag = ControlType.Topic,
                                    ForeColor = Convert.ToInt32(_row["ti_submit_status"]) == 1 ? Color.Black : DisEnbleColor
                                };
                                proNode.Nodes.Add(topNode);

                                DataTable subTable = SqlHelper.ExecuteQuery($"SELECT si_id, si_name, si_code, si_submit_status FROM subject_info WHERE si_obj_id='{_row["ti_id"]}' AND si_worker_id='{UserHelper.GetUser().UserKey}';");
                                foreach (DataRow subRow in subTable.Rows)
                                {
                                    TreeNode subNode = new TreeNode()
                                    {
                                        Name = ToolHelper.GetValue(subRow["si_id"]),
                                        Text = ToolHelper.GetValue(subRow["si_code"]),
                                        Tag = ControlType.Subject,
                                        ForeColor = Convert.ToInt32(subRow["si_submit_status"]) == 1 ? Color.Black : DisEnbleColor
                                    };
                                    topNode.Nodes.Add(subNode);
                                }
                            }
                        }
                    }
                }
                else
                {
                    DataRow impRow = SqlHelper.ExecuteSingleRowQuery($"SELECT imp_id, imp_name, imp_submit_status FROM imp_info WHERE imp_obj_id='{OBJECT_ID}'");
                    if (impRow != null)
                    {
                        treeNode = new TreeNode()
                        {
                            Name = ToolHelper.GetValue(impRow["imp_id"]),
                            Text = ToolHelper.GetValue(impRow["imp_name"]),
                            Tag = ControlType.Imp,
                            ForeColor = GetForeColorByState(impRow["imp_submit_status"]),
                        };

                        //根据重大专项查询具体专项信息
                        DataRow row = SqlHelper.ExecuteSingleRowQuery($"SELECT imp_id, imp_code, imp_submit_status FROM imp_dev_info WHERE imp_obj_id='{treeNode.Name}'");
                        if (row != null)
                        {
                            TreeNode treeNode2 = new TreeNode()
                            {
                                Name = ToolHelper.GetValue(row["imp_id"]),
                                Text = ToolHelper.GetValue(row["imp_code"]),
                                Tag = ControlType.Special,
                                ForeColor = GetForeColorByState(row["imp_submit_status"]),
                            };
                            treeNode.Nodes.Add(treeNode2);

                            //根据【专项信息】查询【项目/课题】集
                            DataTable list = SqlHelper.ExecuteQuery($"SELECT pi_id, pi_code, pi_submit_status FROM project_info WHERE pi_obj_id='{treeNode2.Name}' UNION ALL " +
                            $"SELECT ti_id, ti_code, ti_submit_status FROM topic_info WHERE ti_obj_id='{treeNode2.Name}'");
                            foreach (DataRow proRow in list.Rows)
                            {
                                TreeNode treeNode3 = new TreeNode()
                                {
                                    Name = ToolHelper.GetValue(proRow["pi_id"]),
                                    Text = ToolHelper.GetValue(proRow["pi_code"]),
                                    Tag = ControlType.Project,
                                    ForeColor = GetForeColorByState(proRow["pi_submit_status"]),
                                };
                                treeNode2.Nodes.Add(treeNode3);

                                //根据【项目/课题】查询【课题/子课题】集
                                DataTable list2 = SqlHelper.ExecuteQuery($"SELECT ti_id, ti_code, ti_submit_status FROM topic_info WHERE ti_obj_id='{treeNode3.Name}' UNION ALL " +
                                $"SELECT si_id, si_code, si_submit_status FROM subject_info WHERE si_obj_id='{treeNode3.Name}'");
                                foreach (DataRow topRow in list2.Rows)
                                {
                                    TreeNode treeNode4 = new TreeNode()
                                    {
                                        Name = ToolHelper.GetValue(topRow["ti_id"]),
                                        Text = ToolHelper.GetValue(topRow["ti_code"]),
                                        Tag = ControlType.Topic,
                                        ForeColor = GetForeColorByState(topRow["ti_submit_status"]),
                                    };
                                    treeNode3.Nodes.Add(treeNode4);

                                    DataTable list3 = SqlHelper.ExecuteQuery($"SELECT si_id, si_code, si_submit_status FROM subject_info WHERE si_obj_id='{treeNode4.Name}'");
                                    foreach (DataRow subRow in list3.Rows)
                                    {
                                        TreeNode treeNode5 = new TreeNode()
                                        {
                                            Name = ToolHelper.GetValue(subRow["si_id"]),
                                            Text = ToolHelper.GetValue(subRow["si_code"]),
                                            Tag = ControlType.Subject,
                                            ForeColor = GetForeColorByState(subRow["si_submit_status"]),
                                        };
                                        treeNode4.Nodes.Add(treeNode5);
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        impRow = SqlHelper.ExecuteSingleRowQuery($"SELECT dd_id, dd_name, '{UserHelper.GetUser().UserKey}' FROM data_dictionary WHERE dd_id='{planId}'");
                        treeNode = new TreeNode()
                        {
                            Name = ToolHelper.GetValue(impRow["dd_id"]),
                            Text = ToolHelper.GetValue(impRow["dd_name"]),
                            Tag = ControlType.Imp
                        };
                    }
                }
            }
            treeView.EndUpdate();

            if (treeNode != null)
            {
                treeView.Nodes.Add(treeNode);
                List<string> list = new List<string>();
                InitialSearchDropDownList(treeNode, list);
                SearchText.Properties.Items.AddRange(list.Distinct().ToArray());

                if (treeView.Nodes.Count > 0 && tab_MenuList.TabPages.Count == 0)
                {
                    TreeNode node = treeView.Nodes[0];
                    ControlType _type = (ControlType)node.Tag;
                    if (_type == ControlType.Plan || _type == ControlType.Plan_Default)
                    {
                        ShowTab("plan", 0);
                        LoadPlanPage(node);
                    }
                    else if (_type == ControlType.Imp || _type == ControlType.Imp_Default)
                    {
                        ShowTab("imp", 0);
                        LoadImpPage(node);
                    }
                }

                treeView.ExpandAll();
            }
        }

        /// <summary>
        /// 根据ID存入编号和名称记录
        /// </summary>
        private void InitialSearchDropDownList(TreeNode treeNode, List<string> list)
        {
            foreach (TreeNode item in treeNode.Nodes)
            {
                if (item.Text != null)
                    list.Add(item.Text);
                InitialSearchDropDownList(item, list);
            }
        }

        /// <summary>
        /// 根据状态获取对应背景色
        /// </summary>
        private Color GetForeColorByState(object state)
        {
            string _str = ToolHelper.GetValue(state);
            if (string.IsNullOrEmpty(_str))
                return DisEnbleColor;
            else
            {
                int index = Convert.ToInt32(_str);
                if (index == 1)
                    return Color.Black;
                else return DisEnbleColor;
            }
        }

        /// <summary>
        /// 目录树点击事件
        /// </summary>
        private void TreeView_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (treeView.SelectedNode != null)
                treeView.SelectedNode.BackColor = Color.Transparent;
            if (e.Button == MouseButtons.Left)
            {
                ControlType type = (ControlType)e.Node.Tag;
                if (type == ControlType.Plan || type == ControlType.Plan_Default)
                {
                    ShowTab("plan", 0);
                    LoadPlanPage(e.Node);
                }
                else if (type == ControlType.Project)
                {
                    if (workType == WorkType.ProjectWork)
                    {
                        object proParam = SqlHelper.ExecuteOnlyOneQuery($"SELECT pi_obj_id FROM project_info WHERE pi_id='{e.Node.Name}' AND pi_categor=2");
                        if (proParam != null)
                        {
                            int index = SqlHelper.ExecuteCountQuery($"SELECT COUNT(imp_id) FROM imp_dev_info WHERE imp_id='{proParam}'");
                            if (index > 0)
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
                        else
                        {
                            int index = SqlHelper.ExecuteCountQuery($"SELECT COUNT(imp_id) FROM imp_dev_info WHERE imp_id=(SELECT ti_obj_id FROM topic_info WHERE ti_id='{e.Node.Name}')");
                            if (index > 0)
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
                    else if (workType == WorkType.PaperWork_Plan)
                    {
                        ShowTab("plan", 0);
                        LoadPlanPage(e.Node.Parent);

                        int index = SqlHelper.ExecuteCountQuery($"SELECT COUNT(pi_id) FROM project_info WHERE pi_id='{e.Node.Name}'");
                        if (index > 0)
                        {
                            ShowTab("project", 1);
                            LoadPageBasicInfo(e.Node, ControlType.Project);
                        }
                        else
                        {
                            ShowTab("topic", 1);
                            LoadPageBasicInfo(e.Node, ControlType.Topic);
                        }
                    }
                    else if (workType == WorkType.PaperWork_Imp)
                    {
                        ShowTab("imp", 0);
                        LoadImpPage(e.Node.Parent.Parent);

                        ShowTab("special", 1);
                        LoadPageBasicInfo(e.Node.Parent, ControlType.Special);

                        int index = SqlHelper.ExecuteCountQuery($"SELECT COUNT(pi_id) FROM project_info WHERE pi_id='{e.Node.Name}'");
                        if (index > 0)
                        {
                            ShowTab("project", 2);
                            LoadPageBasicInfo(e.Node, ControlType.Project);
                        }
                        else
                        {
                            ShowTab("topic", 2);
                            LoadPageBasicInfo(e.Node, ControlType.Topic);
                        }
                    }
                    else if (workType == WorkType.CDWork_Plan)
                    {
                        ShowTab("plan", 0);
                        LoadPlanPage(e.Node.Parent);

                        int index = SqlHelper.ExecuteCountQuery($"SELECT COUNT(pi_id) FROM project_info WHERE pi_id='{e.Node.Name}'");
                        if (index > 0)
                        {
                            ShowTab("project", 1);
                            LoadPageBasicInfo(e.Node, ControlType.Project);
                        }
                        else
                        {
                            ShowTab("topic", 1);
                            LoadPageBasicInfo(e.Node, ControlType.Topic);
                        }
                    }
                    else if (workType == WorkType.CDWork_Imp)
                    {
                        ShowTab("imp", 0);
                        LoadImpPage(e.Node.Parent.Parent);

                        ShowTab("special", 1);
                        LoadPageBasicInfo(e.Node.Parent, ControlType.Special);

                        int index = SqlHelper.ExecuteCountQuery($"SELECT COUNT(pi_id) FROM project_info WHERE pi_id='{e.Node.Name}'");
                        if (index > 0)
                        {
                            ShowTab("project", 2);
                            LoadPageBasicInfo(e.Node, ControlType.Project);
                        }
                        else
                        {
                            ShowTab("topic", 2);
                            LoadPageBasicInfo(e.Node, ControlType.Topic);
                        }
                    }
                }
                else if (type == ControlType.Topic)
                {
                    tab_MenuList.TabPages.Clear();
                    if (workType == WorkType.PaperWork_Plan)
                    {
                        ShowTab("plan", 0);
                        LoadPlanPage(e.Node.Parent.Parent);

                        int index = SqlHelper.ExecuteCountQuery($"SELECT COUNT(pi_id) FROM project_info WHERE pi_id=(SELECT ti_obj_id FROM topic_info WHERE ti_id='{e.Node.Name}')");
                        if (index > 0)
                        {
                            ShowTab("project", 1);
                            LoadPageBasicInfo(e.Node.Parent, ControlType.Project);

                            ShowTab("topic", 2);
                            LoadPageBasicInfo(e.Node, type);
                        }
                        else
                        {
                            ShowTab("topic", 1);
                            LoadPageBasicInfo(e.Node.Parent, ControlType.Topic);

                            ShowTab("subject", 2);
                            LoadPageBasicInfo(e.Node, ControlType.Subject);
                        }
                    }
                    else if (workType == WorkType.PaperWork_Imp)
                    {
                        ShowTab("imp", 0);
                        LoadImpPage(e.Node.Parent.Parent.Parent);

                        ShowTab("special", 1);
                        LoadPageBasicInfo(e.Node.Parent.Parent, ControlType.Special);

                        int index = SqlHelper.ExecuteCountQuery($"SELECT COUNT(pi_id) FROM project_info WHERE pi_id='{e.Node.Parent.Name}'");
                        if (index > 0)
                        {

                            ShowTab("project", 2);
                            LoadPageBasicInfo(e.Node.Parent, ControlType.Project);

                            ShowTab("topic", 3);
                            LoadPageBasicInfo(e.Node, ControlType.Topic);
                        }
                        else
                        {

                            ShowTab("topic", 2);
                            LoadPageBasicInfo(e.Node.Parent, ControlType.Topic);

                            ShowTab("subject", 3);
                            LoadPageBasicInfo(e.Node, ControlType.Subject);
                        }
                    }
                    else if (workType == WorkType.CDWork_Plan)
                    {
                        ShowTab("plan", 0);
                        LoadPlanPage(e.Node.Parent.Parent);

                        int index = SqlHelper.ExecuteCountQuery($"SELECT COUNT(pi_id) FROM project_info WHERE pi_id=(SELECT ti_obj_id FROM topic_info WHERE ti_id='{e.Node.Name}')");
                        if (index > 0)
                        {
                            ShowTab("project", 1);
                            LoadPageBasicInfo(e.Node.Parent, ControlType.Project);

                            ShowTab("topic", 2);
                            LoadPageBasicInfo(e.Node, type);
                        }
                        else
                        {
                            ShowTab("topic", 1);
                            LoadPageBasicInfo(e.Node.Parent, ControlType.Topic);

                            ShowTab("subject", 2);
                            LoadPageBasicInfo(e.Node, ControlType.Subject);
                        }
                    }
                    else if (workType == WorkType.ProjectWork)
                    {
                        object _tempParam = SqlHelper.ExecuteOnlyOneQuery($"SELECT pi_obj_id FROM project_info WHERE pi_id=(SELECT ti_obj_id FROM topic_info WHERE ti_id='{e.Node.Name}') AND pi_categor=2");
                        if (_tempParam != null)
                        {
                            int speParam = SqlHelper.ExecuteCountQuery($"SELECT COUNT(imp_id) FROM imp_dev_info WHERE imp_id='{_tempParam}'");
                            if (speParam > 0)
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
                            if (speParam > 0)
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
                    else if (workType == WorkType.CDWork_Imp)
                    {
                        ShowTab("imp", 0);
                        LoadImpPage(e.Node.Parent.Parent.Parent);

                        ShowTab("special", 1);
                        LoadPageBasicInfo(e.Node.Parent.Parent, ControlType.Special);

                        int index = SqlHelper.ExecuteCountQuery($"SELECT COUNT(pi_id) FROM project_info WHERE pi_id='{e.Node.Parent.Name}'");
                        if (index > 0)
                        {

                            ShowTab("project", 2);
                            LoadPageBasicInfo(e.Node.Parent, ControlType.Project);

                            ShowTab("topic", 3);
                            LoadPageBasicInfo(e.Node, ControlType.Topic);
                        }
                        else
                        {

                            ShowTab("topic", 2);
                            LoadPageBasicInfo(e.Node.Parent, ControlType.Topic);

                            ShowTab("subject", 3);
                            LoadPageBasicInfo(e.Node, ControlType.Subject);
                        }
                    }
                }
                else if (type == ControlType.Subject)
                {
                    if (workType == WorkType.ProjectWork)
                    {
                        object proId = SqlHelper.ExecuteOnlyOneQuery($"SELECT ti_obj_id FROM topic_info WHERE ti_id=(SELECT si_obj_id FROM subject_info WHERE si_id='{e.Node.Name}')");
                        object _tempParam = SqlHelper.ExecuteOnlyOneQuery($"SELECT pi_obj_id FROM project_info WHERE pi_id='{proId}' AND pi_categor=2");
                        if (_tempParam != null)
                        {
                            int speParam = SqlHelper.ExecuteCountQuery($"SELECT COUNT(imp_id) FROM imp_dev_info WHERE imp_id='{_tempParam}'");
                            if (speParam > 0)
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
                            int speParam = SqlHelper.ExecuteCountQuery($"SELECT COUNT(imp_id) FROM imp_dev_info WHERE imp_id='{proId}'");
                            if (speParam > 0)
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
                    else if (workType == WorkType.PaperWork_Imp)
                    {
                        int index = SqlHelper.ExecuteCountQuery($"SELECT COUNT(pi_id) FROM project_info WHERE pi_id='{e.Node.Parent.Parent.Name}'");
                        if (index > 0)
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
                            ShowTab("imp", 0);
                            LoadImpPage(e.Node.Parent.Parent.Parent);

                            ShowTab("special", 1);
                            LoadPageBasicInfo(e.Node.Parent.Parent, ControlType.Special);

                            ShowTab("topic", 2);
                            LoadPageBasicInfo(e.Node.Parent, ControlType.Topic);

                            ShowTab("subject", 3);
                            LoadPageBasicInfo(e.Node, ControlType.Subject);
                        }
                    }
                    else if (workType == WorkType.PaperWork_Plan)
                    {
                        tab_MenuList.TabPages.Clear();
                        object tempId = SqlHelper.ExecuteOnlyOneQuery($"SELECT ti_obj_id FROM topic_info WHERE ti_id=(SELECT si_obj_id FROM subject_info WHERE si_id='{e.Node.Name}')");
                        object _tempParam = SqlHelper.ExecuteOnlyOneQuery($"SELECT pi_id FROM project_info WHERE pi_id='{tempId}' AND pi_categor=2");
                        if (_tempParam != null)
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
                    else if (workType == WorkType.CDWork_Plan)
                    {
                        object tempId = SqlHelper.ExecuteOnlyOneQuery($"SELECT ti_obj_id FROM topic_info WHERE ti_id=(SELECT si_obj_id FROM subject_info WHERE si_id='{e.Node.Name}')");
                        object _tempParam = SqlHelper.ExecuteOnlyOneQuery($"SELECT pi_id FROM project_info WHERE pi_id='{tempId}'");
                        if (_tempParam != null)
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
                    else if (workType == WorkType.CDWork_Imp)
                    {
                        int index = SqlHelper.ExecuteCountQuery($"SELECT COUNT(pi_id) FROM project_info WHERE pi_id='{e.Node.Parent.Parent.Name}'");
                        if (index > 0)
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
                            ShowTab("imp", 0);
                            LoadImpPage(e.Node.Parent.Parent.Parent);

                            ShowTab("special", 1);
                            LoadPageBasicInfo(e.Node.Parent.Parent, ControlType.Special);

                            ShowTab("topic", 2);
                            LoadPageBasicInfo(e.Node.Parent, ControlType.Topic);

                            ShowTab("subject", 3);
                            LoadPageBasicInfo(e.Node, ControlType.Subject);
                        }
                    }
                }
                else if (type == ControlType.Imp || type == ControlType.Imp_Default)
                {
                    ShowTab("imp", 0);
                    LoadImpPage(e.Node);
                }
                else if (type == ControlType.Special)
                {
                    ShowTab("imp", 0);
                    LoadImpPage(e.Node.Parent);

                    ShowTab("special", 1);
                    LoadPageBasicInfo(e.Node, type);
                }

                if (tab_MenuList.TabCount > 1)
                {
                    tab_MenuList.SelectedIndex = tab_MenuList.TabCount - 1;
                }
            }
            else if (e.Button == MouseButtons.Right)
            {
                treeView.SelectedNode = e.Node;
                if (e.Node.ForeColor == Color.Black)
                    contextMenuStrip2.Show(MousePosition);
            }
        }

        /// <summary>
        /// 加载IMP基本信息
        /// </summary>
        private void LoadImpPage(TreeNode node)
        {
            DataRow impRow = SqlHelper.ExecuteSingleRowQuery($"SELECT imp_id, imp_name, imp_intro, imp_submit_status, imp_source_id FROM imp_info WHERE imp_id='{node.Name}'");
            if (impRow != null)
            {
                if ((ObjectSubmitStatus)impRow["imp_submit_status"] == ObjectSubmitStatus.SubmitSuccess)
                    EnableControls(ControlType.Imp, false);
                tab_Imp_Info.Tag = ToolHelper.GetValue(impRow["imp_id"]);
                lbl_Imp_Name.Text = ToolHelper.GetValue(impRow["imp_name"]);
                txt_Imp_Intro.Text = ToolHelper.GetValue(impRow["imp_intro"]);

                tab_Imp_Info.SelectedTabPageIndex = 0;
                LoadFileList(dgv_Imp_FileList, ToolHelper.GetValue(impRow["imp_id"]), -1);
                //如果非被人创建则不允许修改
                if (!impRow["imp_source_id"].Equals(UserHelper.GetUser().UserKey) && !(impRow["imp_source_id"] is DBNull))
                {
                    cbo_Imp_HasNext.Enabled = false;
                    pal_Imp_BtnGroup.Enabled = false;
                }
                DataRow speRow = SqlHelper.ExecuteSingleRowQuery("SELECT imp_dev_info.* FROM imp_info LEFT JOIN imp_dev_info ON imp_info.imp_id=imp_dev_info.imp_obj_id " +
                    $"WHERE imp_info.imp_id='{impRow["imp_id"]}' AND imp_dev_info.imp_id IS NOT NULL");
                if (speRow != null)
                {
                    Tag = speRow["imp_code"];
                    cbo_Imp_HasNext.Enabled = false;
                }
            }
            else
            {
                impRow = SqlHelper.ExecuteSingleRowQuery($"SELECT dd_id, dd_name, dd_note FROM data_dictionary WHERE dd_id='{node.Name}'");
                lbl_Imp_Name.Tag = ToolHelper.GetValue(impRow["dd_id"]);
                lbl_Imp_Name.Text = ToolHelper.GetValue(impRow["dd_name"]);
                txt_Imp_Intro.Text = ToolHelper.GetValue(impRow["dd_note"]);
            }
            //加载下拉列表
            if (cbo_Imp_HasNext.DataSource == null)
            {
                string key = "dic_key_project";
                DataTable table = SqlHelper.ExecuteQuery($"SELECT dd_id, dd_name FROM data_dictionary WHERE dd_pId=(SELECT dd_id FROM data_dictionary WHERE dd_code='{key}') ORDER BY dd_sort");
                cbo_Imp_HasNext.DataSource = table;
                cbo_Imp_HasNext.DisplayMember = "dd_name";
                cbo_Imp_HasNext.ValueMember = "dd_id";
            }

            //如果是质检返工则加载意见数
            if (isBacked)
            {
                btn_Imp_QTReason.Text = $"质检意见({GetAdvincesAmount(tab_Imp_Info.Tag)})";
                cbo_Imp_HasNext.Enabled = false;
            }
            if (node.ForeColor == DisEnbleColor)
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
        /// 获取最高密级
        /// </summary>
        private string GetMaxSecretById(object objid) => ToolHelper.GetValue(SqlHelper.ExecuteOnlyOneQuery($"SELECT TOP(1) dd_name FROM processing_file_list LEFT JOIN data_dictionary ON pfl_scert = dd_id WHERE pfl_obj_id = '{objid}' ORDER BY dd_sort DESC"));

        /// <summary>
        /// 加载文件缺失校验列表
        /// </summary>
        /// <param name="dataGridView">待校验表格</param>
        /// <param name="objID">主键</param>
        private void LoadFileValidList(DataGridView dataGridView, object objID, string key)
        {
            dataGridView.Rows.Clear();

            string querySql = "SELECT d1.dd_name name, d1.dd_name + ' ' + d1.extend_3 AS dd_name, d1.dd_note, d1.extend_2 FROM data_dictionary d1 " +
                "INNER JOIN data_dictionary d2 ON d1.dd_pId = d2.dd_id " +
                "INNER JOIN data_dictionary d3 ON d2.dd_pId = d3.dd_id " +
                "WHERE d3.dd_code = 'dic_file_jd' AND d1.dd_name <> '其他' AND d1.dd_name NOT IN ( " +
                "SELECT dd_name FROM processing_file_list AS fi " +
                "INNER JOIN data_dictionary AS dd ON fi.pfl_categor = dd.dd_id " +
               $"WHERE fi.pfl_obj_id = '{objID}' GROUP BY dd_name) ORDER BY name";
            DataTable table = SqlHelper.ExecuteQuery(querySql);
            for (int i = 0; i < table.Rows.Count; i++)
            {
                string typeName = ToolHelper.GetValue(table.Rows[i]["name"]).Trim();
                int indexRow = dataGridView.Rows.Add();
                dataGridView.Rows[indexRow].Cells[key + "id"].Value = i + 1;
                dataGridView.Rows[indexRow].Cells[key + "categor"].Value = ToolHelper.GetValue(table.Rows[i]["dd_name"]);
                dataGridView.Rows[indexRow].Cells[key + "name"].Value = ToolHelper.GetValue(table.Rows[i]["dd_note"]);

                string queryReasonSql = $"SELECT pfo_id, pfo_reason, pfo_remark FROM processing_file_lost WHERE pfo_obj_id='{objID}' AND pfo_categor LIKE '{typeName}%'";
                object[] _obj = SqlHelper.ExecuteRowsQuery(queryReasonSql);
                if (_obj != null)
                {
                    dataGridView.Rows[indexRow].Cells[key + "id"].Tag = ToolHelper.GetValue(_obj[0]);
                    dataGridView.Rows[indexRow].Cells[key + "reason"].Value = ToolHelper.GetValue(_obj[1]);
                    dataGridView.Rows[indexRow].Cells[key + "remark"].Value = ToolHelper.GetValue(_obj[2]);
                }
                if (!key.Contains("special") && !key.Contains("plan"))
                {
                    string musted = ToolHelper.GetValue(table.Rows[i]["extend_2"]);
                    if (!string.IsNullOrEmpty(musted))
                    {
                        dataGridView.Rows[indexRow].Tag = musted;
                        dataGridView.Rows[indexRow].Cells[key + "name"].Style.ForeColor = Color.Red;
                    }
                }
            }

            dataGridView.ColumnHeadersDefaultCellStyle = DataGridViewStyleHelper.GetHeaderStyle();
        }

        /// <summary>
        /// 加载计划-案卷盒归档表
        /// </summary>
        /// <param name="pbId">案卷盒ID</param>
        /// <param name="objId">所属对象ID</param>
        /// <param name="type">对象类型</param>
        private void LoadFileBoxTable(object pbId, object objId, ControlType type)
        {
            if (pbId == null || objId == null) return;
            string GCID = ToolHelper.GetValue(SqlHelper.ExecuteOnlyOneQuery($"SELECT pb_gc_id FROM processing_box WHERE pb_id='{pbId}'"));
            if (type == ControlType.Plan)
            {
                txt_Plan_GCID.Text = GCID;
                LoadFileBoxTableInstance(lsv_JH_File1, lsv_JH_File2, "jh", pbId, objId);
            }
            else if (type == ControlType.Project)
            {
                txt_Project_GCID.Text = GCID;
                LoadFileBoxTableInstance(lsv_JH_XM_File1, lsv_JH_XM_File2, "jh_xm", pbId, objId);
            }
            else if (type == ControlType.Topic)
            {
                txt_Topic_GCID.Text = GCID;
                LoadFileBoxTableInstance(lsv_JH_KT_File1, lsv_JH_KT_File2, "jh_kt", pbId, objId);
            }
            else if (type == ControlType.Subject)
            {
                txt_Subject_GCID.Text = GCID;
                LoadFileBoxTableInstance(lsv_JH_XM_KT_ZKT_File1, lsv_JH_XM_KT_ZKT_File2, "jh_xm_kt_zkt", pbId, objId);
            }
            else if (type == ControlType.Imp)
            {
                txt_Imp_GCID.Text = GCID;
                LoadFileBoxTableInstance(lsv_Imp_File1, lsv_Imp_File2, "imp", pbId, objId);
            }
            else if (type == ControlType.Special)
            {
                txt_Special_GCID.Text = GCID;
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
                    new ColumnHeader{ Name = $"{key}_file1_name", Text = "文件名称", Width = 350},
                    new ColumnHeader{ Name = $"{key}_file1_date", Text = "形成日期", Width = 100}
            });
            rightView.Columns.AddRange(new ColumnHeader[]
            {
                    new ColumnHeader{ Name = $"{key}_file2_id", Text = "主键", Width = 0},
                    new ColumnHeader{ Name = $"{key}_file2_number", Text = "序号", Width = 50},
                    new ColumnHeader{ Name = $"{key}_file2_type", Text = "文件编号", TextAlign = HorizontalAlignment.Center ,Width = 85},
                    new ColumnHeader{ Name = $"{key}_file2_name", Text = "文件名称", Width = 350},
                    new ColumnHeader{ Name = $"{key}_file2_date", Text = "形成日期", Width = 100}
            });
            //未归档
            string querySql = $"SELECT pfl_id, pfl_code, pfl_name, pfl_date FROM processing_file_list " +
                $"WHERE pfl_obj_id = '{objId}' AND (pfl_box_id IS NULL OR pfl_box_id='') AND pfl_amount > 0 ORDER BY pfl_sort, pfl_code";
            DataTable dataTable = SqlHelper.ExecuteQuery(querySql);
            foreach (DataRow row in dataTable.Rows)
            {
                ListViewItem item = leftView.Items.Add(ToolHelper.GetValue(row["pfl_id"]));
                item.SubItems.AddRange(new ListViewItem.ListViewSubItem[]
                {
                    new ListViewItem.ListViewSubItem(){ Text = ToolHelper.GetValue(row["pfl_code"]) },
                    new ListViewItem.ListViewSubItem(){ Text = ToolHelper.GetValue(row["pfl_name"]) },
                    new ListViewItem.ListViewSubItem(){ Text = GetDateValue(row["pfl_date"], "yyyy-MM-dd") },
                });
            }
            //已归档[已存在盒]
            if (!string.IsNullOrEmpty(ToolHelper.GetValue(pbId)))
            {
                querySql = $"SELECT pfl_id, pfl_code, pfl_name, pfl_date FROM processing_file_list " +
                    $"WHERE pfl_box_id ='{pbId}' ORDER BY pfl_box_sort";
                DataTable table = SqlHelper.ExecuteQuery(querySql);
                int j = 0;
                foreach (DataRow row in table.Rows)
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
            if (!string.IsNullOrEmpty(value))
            {
                if (DateTime.TryParse(value, out DateTime result))
                {
                    if (result != DefaultValue.DefaultMinDate)
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
            if (name.Contains("Plan"))
            {
                object value = cbo_Plan_Box.SelectedValue;
                if (value != null)
                {
                    if ("btn_Plan_Box_Right".Equals(name))
                    {
                        if (lsv_JH_File1.SelectedItems.Count > 0)
                        {
                            int count = lsv_JH_File1.SelectedItems.Count;
                            if (count > 0)
                            {
                                object[] _obj = new object[count];
                                for (int i = 0; i < count; i++)
                                    _obj[i] = lsv_JH_File1.SelectedItems[i].SubItems[0].Text;
                                SetFileState(_obj, value, true);
                            }
                        }
                    }
                    else if ("btn_Plan_Box_RightAll".Equals(name))
                    {
                        int count = lsv_JH_File1.Items.Count;
                        if (count > 0)
                        {
                            object[] _obj = new object[count];
                            for (int i = 0; i < count; i++)
                                _obj[i] = lsv_JH_File1.Items[i].SubItems[0].Text;
                            SetFileState(_obj, value, true);
                        }
                    }
                    else if ("btn_Plan_Box_Left".Equals(name))
                    {
                        int count = lsv_JH_File2.SelectedItems.Count;
                        if (count > 0)
                        {
                            object[] _obj = new object[count];
                            for (int i = 0; i < count; i++)
                                _obj[i] = lsv_JH_File2.SelectedItems[i].SubItems[0].Text;
                            SetFileState(_obj, value, false);
                        }
                    }
                    else if ("btn_Plan_Box_LeftAll".Equals(name))
                    {
                        int count = lsv_JH_File2.Items.Count;
                        if (count > 0)
                        {
                            object[] _obj = new object[count];
                            for (int i = 0; i < count; i++)
                                _obj[i] = lsv_JH_File2.Items[i].SubItems[0].Text;
                            SetFileState(_obj, value, false);
                        }
                    }
                    LoadFileBoxTable(value, cbo_Plan_AJ_Code.SelectedValue, ControlType.Plan);
                }
                else
                    XtraMessageBox.Show("请先添加案卷盒。", "保存失败");
            }
            //计划-项目
            else if (name.Contains("btn_Project_Box"))
            {
                object value = cbo_Project_Box.SelectedValue;
                if (value != null)
                {
                    if ("btn_Project_Box_Right".Equals(name))
                    {
                        if (lsv_JH_XM_File1.SelectedItems.Count > 0)
                        {
                            int count = lsv_JH_XM_File1.SelectedItems.Count;
                            if (count > 0)
                            {
                                object[] _obj = new object[count];
                                for (int i = 0; i < count; i++)
                                    _obj[i] = lsv_JH_XM_File1.SelectedItems[i].SubItems[0].Text;
                                SetFileState(_obj, value, true);
                            }
                        }
                    }
                    else if ("btn_Project_Box_RightAll".Equals(name))
                    {
                        int count = lsv_JH_XM_File1.Items.Count;
                        if (count > 0)
                        {
                            object[] _obj = new object[count];
                            for (int i = 0; i < count; i++)
                                _obj[i] = lsv_JH_XM_File1.Items[i].SubItems[0].Text;
                            SetFileState(_obj, value, true);
                        }
                    }
                    else if ("btn_Project_Box_Left".Equals(name))
                    {
                        int count = lsv_JH_XM_File2.SelectedItems.Count;
                        if (count > 0)
                        {
                            object[] _obj = new object[count];
                            for (int i = 0; i < count; i++)
                                _obj[i] = lsv_JH_XM_File2.SelectedItems[i].SubItems[0].Text;
                            SetFileState(_obj, value, false);
                        }
                    }
                    else if ("btn_Project_Box_LeftAll".Equals(name))
                    {
                        int count = lsv_JH_XM_File2.Items.Count;
                        if (count > 0)
                        {
                            object[] _obj = new object[count];
                            for (int i = 0; i < count; i++)
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
            else if (name.Contains("btn_Topic_Box"))
            {
                object value = cbo_Topic_Box.SelectedValue;
                if (value != null)
                {
                    if ("btn_Topic_Box_Right".Equals(name))
                    {
                        if (lsv_JH_KT_File1.SelectedItems.Count > 0)
                        {
                            int count = lsv_JH_KT_File1.SelectedItems.Count;
                            if (count > 0)
                            {
                                object[] _obj = new object[count];
                                for (int i = 0; i < count; i++)
                                    _obj[i] = lsv_JH_KT_File1.SelectedItems[i].SubItems[0].Text;
                                SetFileState(_obj, value, true);
                            }
                        }
                    }
                    else if ("btn_Topic_Box_RightAll".Equals(name))
                    {
                        int count = lsv_JH_KT_File1.Items.Count;
                        if (count > 0)
                        {
                            object[] _obj = new object[count];
                            for (int i = 0; i < count; i++)
                                _obj[i] = lsv_JH_KT_File1.Items[i].SubItems[0].Text;
                            SetFileState(_obj, value, true);
                        }
                    }
                    else if ("btn_Topic_Box_Left".Equals(name))
                    {
                        int count = lsv_JH_KT_File2.SelectedItems.Count;
                        if (count > 0)
                        {
                            object[] _obj = new object[count];
                            for (int i = 0; i < count; i++)
                                _obj[i] = lsv_JH_KT_File2.SelectedItems[i].SubItems[0].Text;
                            SetFileState(_obj, value, false);
                        }
                    }
                    else if ("btn_Topic_Box_LeftAll".Equals(name))
                    {
                        int count = lsv_JH_KT_File2.Items.Count;
                        if (count > 0)
                        {
                            object[] _obj = new object[count];
                            for (int i = 0; i < count; i++)
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
            else if (name.Contains("btn_Subject_Box"))
            {
                object value = cbo_Subject_Box.SelectedValue;
                if (value != null)
                {
                    if ("btn_Subject_Box_Right".Equals(name))
                    {
                        if (lsv_JH_XM_KT_ZKT_File1.SelectedItems.Count > 0)
                        {
                            int count = lsv_JH_XM_KT_ZKT_File1.SelectedItems.Count;
                            if (count > 0)
                            {
                                object[] _obj = new object[count];
                                for (int i = 0; i < count; i++)
                                    _obj[i] = lsv_JH_XM_KT_ZKT_File1.SelectedItems[i].SubItems[0].Text;
                                SetFileState(_obj, value, true);
                            }
                        }
                    }
                    else if ("btn_Subject_Box_RightAll".Equals(name))
                    {
                        int count = lsv_JH_XM_KT_ZKT_File1.Items.Count;
                        if (count > 0)
                        {
                            object[] _obj = new object[count];
                            for (int i = 0; i < count; i++)
                                _obj[i] = lsv_JH_XM_KT_ZKT_File1.Items[i].SubItems[0].Text;
                            SetFileState(_obj, value, true);
                        }
                    }
                    else if ("btn_Subject_Box_Left".Equals(name))
                    {
                        int count = lsv_JH_XM_KT_ZKT_File2.SelectedItems.Count;
                        if (count > 0)
                        {
                            object[] _obj = new object[count];
                            for (int i = 0; i < count; i++)
                                _obj[i] = lsv_JH_XM_KT_ZKT_File2.SelectedItems[i].SubItems[0].Text;
                            SetFileState(_obj, value, false);
                        }
                    }
                    else if ("btn_Subject_Box_LeftAll".Equals(name))
                    {
                        int count = lsv_JH_XM_KT_ZKT_File2.Items.Count;
                        if (count > 0)
                        {
                            object[] _obj = new object[count];
                            for (int i = 0; i < count; i++)
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
            else if (name.Contains("btn_Imp_Box"))
            {
                object value = cbo_Imp_Box.SelectedValue;
                if (value != null)
                {
                    if ("btn_Imp_Box_Right".Equals(name))
                    {
                        if (lsv_Imp_File1.SelectedItems.Count > 0)
                        {
                            int count = lsv_Imp_File1.SelectedItems.Count;
                            if (count > 0)
                            {
                                object[] _obj = new object[count];
                                for (int i = 0; i < count; i++)
                                    _obj[i] = lsv_Imp_File1.SelectedItems[i].SubItems[0].Text;
                                SetFileState(_obj, value, true);
                            }
                        }
                    }
                    else if ("btn_Imp_Box_RightAll".Equals(name))
                    {
                        int count = lsv_Imp_File1.Items.Count;
                        if (count > 0)
                        {
                            object[] _obj = new object[count];
                            for (int i = 0; i < count; i++)
                                _obj[i] = lsv_Imp_File1.Items[i].SubItems[0].Text;
                            SetFileState(_obj, value, true);
                        }
                    }
                    else if ("btn_Imp_Box_Left".Equals(name))
                    {
                        int count = lsv_Imp_File2.SelectedItems.Count;
                        if (count > 0)
                        {
                            object[] _obj = new object[count];
                            for (int i = 0; i < count; i++)
                                _obj[i] = lsv_Imp_File2.SelectedItems[i].SubItems[0].Text;
                            SetFileState(_obj, value, false);
                        }
                    }
                    else if ("btn_Imp_Box_LeftAll".Equals(name))
                    {
                        int count = lsv_Imp_File2.Items.Count;
                        if (count > 0)
                        {
                            object[] _obj = new object[count];
                            for (int i = 0; i < count; i++)
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
            else if (name.Contains("btn_Special_Box"))
            {
                object value = cbo_Special_Box.SelectedValue;
                if (value != null)
                {
                    if ("btn_Special_Box_Right".Equals(name))
                    {
                        if (lsv_Imp_Dev_File1.SelectedItems.Count > 0)
                        {
                            int count = lsv_Imp_Dev_File1.SelectedItems.Count;
                            if (count > 0)
                            {
                                object[] _obj = new object[count];
                                for (int i = 0; i < count; i++)
                                    _obj[i] = lsv_Imp_Dev_File1.SelectedItems[i].SubItems[0].Text;
                                SetFileState(_obj, value, true);
                            }
                        }
                    }
                    else if ("btn_Special_Box_RightAll".Equals(name))
                    {
                        int count = lsv_Imp_Dev_File1.Items.Count;
                        if (count > 0)
                        {
                            object[] _obj = new object[count];
                            for (int i = 0; i < count; i++)
                                _obj[i] = lsv_Imp_Dev_File1.Items[i].SubItems[0].Text;
                            SetFileState(_obj, value, true);
                        }
                    }
                    else if ("btn_Special_Box_Left".Equals(name))
                    {
                        int count = lsv_Imp_Dev_File2.SelectedItems.Count;
                        if (count > 0)
                        {
                            object[] _obj = new object[count];
                            for (int i = 0; i < count; i++)
                                _obj[i] = lsv_Imp_Dev_File2.SelectedItems[i].SubItems[0].Text;
                            SetFileState(_obj, value, false);
                        }
                    }
                    else if ("btn_Special_Box_LeftAll".Equals(name))
                    {
                        int count = lsv_Imp_Dev_File2.Items.Count;
                        if (count > 0)
                        {
                            object[] _obj = new object[count];
                            for (int i = 0; i < count; i++)
                                _obj[i] = lsv_Imp_Dev_File2.Items[i].SubItems[0].Text;
                            SetFileState(_obj, value, false);
                        }
                    }
                    LoadFileBoxTable(value, cbo_Special_AJ_Code.SelectedValue, ControlType.Special);
                }
                else
                    XtraMessageBox.Show("请先添加案卷盒。", "保存失败");
            }
        }

        /// <summary>
        /// 文件归档
        /// </summary>
        /// <param name="fileIds">待处理文件IDS</param>
        /// <param name="boxId">案卷盒ID</param>
        /// <param name="isGD">ture:归档;false:不归档</param>
        private void SetFileState(object[] fileIds, object boxId, bool isGD)
        {
            if (isGD)
            {
                string updateSQL = string.Empty;
                for (int i = 0; i < fileIds.Length; i++)
                {
                    updateSQL += $"UPDATE processing_file_list SET pfl_box_id='{boxId}', pfl_box_sort=(SELECT COUNT(pfl_id) FROM processing_file_list WHERE pfl_box_id='{boxId}') WHERE pfl_id='{fileIds[i]}';";
                }
                SqlHelper.ExecuteNonQuery(updateSQL);
            }
            else
            {
                //将文件状态置为未归档
                StringBuilder updateSql = new StringBuilder($"UPDATE processing_file_list SET pfl_box_id=NULL WHERE pfl_id IN (");
                for (int i = 0; i < fileIds.Length; i++)
                    updateSql.Append($"'{fileIds[i]}'{(i == fileIds.Length - 1 ? ");" : ",")}");
                SqlHelper.ExecuteNonQuery(updateSql.ToString());
            }
        }

        /// <summary>
        /// 根据预设规则获取编码
        /// </summary>
        /// <param name="type">0：案卷 1：馆藏号</param>
        private string[] GetAJCode(object objId, object objCode, int type, string year, string zxCode, string unitCode)
        {
            string[] code = new string[2] { string.Empty, string.Empty };
            DataRow row = SqlHelper.ExecuteSingleRowQuery($"SELECT * FROM code_rule WHERE cr_type='{type}';");
            if (row != null)
            {
                string fix = ToolHelper.GetValue(row["cr_fixed"]);
                string symbol = ToolHelper.GetValue(row["cr_split_symbol"]);
                if (!string.IsNullOrEmpty(fix))
                    code[0] += $"{fix + symbol}";
                string template = ToolHelper.GetValue(row["cr_template"]);
                string[] strs = GetGroupCode(template, symbol);
                for (int i = 0; i < strs.Length; i++)
                {
                    if ("AAAA".Equals(strs[i]))//专项编号
                    {
                        if (!string.IsNullOrEmpty(zxCode))
                        {
                            //如果同时存在来源单位，则去ZX字母
                            if (strs.Contains("CCCC"))
                            {
                                code[0] += zxCode.Replace("ZX", string.Empty);
                            }
                            else
                            {
                                code[0] += zxCode;
                            }
                        }
                        else
                            continue;
                    }
                    else if ("BBBB".Equals(strs[i]))//项目/课题编号
                        code[0] += objCode;
                    else if ("CCCC".Equals(strs[i]))//来源单位
                    {
                        if (!string.IsNullOrEmpty(unitCode))
                            code[0] += unitCode;
                        else
                            continue;
                    }
                    else if ("YYYY".Equals(strs[i]))
                        code[0] += year;
                    else
                    {
                        int length = strs[i].Length;
                        int amount = 0;
                        if (type == 0)
                        {
                            //   amount = SqlHelper.ExecuteCountQuery($"SELECT COUNT(pt_id) FROM files_tag_info WHERE pt_special_id='{sourceUnitId}'") + 1;
                        }
                        else if (type == 1)
                        {
                            //专项单独流水，其他以来源单位为基准
                            object _code = ToolHelper.GetValue(Tag).StartsWith("ZX") ? Tag : string.Empty;
                            amount = GetBoxWaterNumber(length, unitCode + _code);
                        }
                        code[1] = amount.ToString().PadLeft(length, '0');
                    }
                    code[0] += symbol;
                }

                if (!string.IsNullOrEmpty(symbol) && code[0].EndsWith(symbol))
                    code[0] = code[0].Substring(0, code.Length - 1);
            }
            return code;
        }

        private string[] GetGroupCode(string value, string symbol)
        {
            List<string> list = new List<string>();
            int i = 0;
            int length = 4 + symbol.Length;
            while (value.Length >= length)
            {
                if (int.TryParse(value, out int result))
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
            if (value.Length < length)
                list.Add(value);
            return list.ToArray();
        }

        /// <summary>
        /// 获取馆藏号流水号
        /// （优先获取已删除的）
        /// </summary>
        private int GetBoxWaterNumber(int length, string unitCode)
        {
            int result = 1;
            string querySql = "SELECT MIN(num) FROM( SELECT ROW_NUMBER() OVER(ORDER BY pb_gc_number) num, pb_gc_number FROM( " +
               $"SELECT DISTINCT(pb_gc_number) FROM processing_box a where a.pb_unit_id = '{unitCode}') A) A WHERE num<> pb_gc_number";
            object value = SqlHelper.ExecuteOnlyOneQuery(querySql);
            result = value == null
                ? ToolHelper.GetIntValue(SqlHelper.ExecuteOnlyOneQuery($"SELECT MAX(pb_gc_number) + 1 FROM processing_box WHERE pb_unit_id='{unitCode}'"), 1)
                : ToolHelper.GetIntValue(value);
            return result;
        }

        /// <summary>
        /// 计划 - 增加/删除案卷盒
        /// </summary>
        private void Lbl_Box_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Label label = sender as Label;
            if (label.Name.Contains("lbl_Plan_Box"))
            {
                object objId = cbo_Plan_AJ_Code.SelectedValue;
                if (objId != null)
                {
                    if ("lbl_Plan_Box_Add".Equals(label.Name))//新增
                    {
                        //当前已有盒号数量
                        int amount = SqlHelper.ExecuteCountQuery($"SELECT COUNT(pb_box_number) FROM processing_box WHERE pb_obj_id='{objId}'");
                        string[] gch = GetAJCode(objId, null, 1, DateTime.Now.Year.ToString(), null, ToolHelper.GetValue(unitCode));
                        object _code = ToolHelper.GetValue(Tag).StartsWith("ZX") ? Tag : string.Empty;
                        string primaryKey = Guid.NewGuid().ToString();
                        string insertSql = $"INSERT INTO processing_box(pb_id, pb_box_number, pb_gc_fix, pb_gc_id, pb_gc_number, pb_obj_id, pb_create_id, pb_create_date, pb_create_type, pb_unit_id, pt_id) " +
                            $"VALUES('{primaryKey}', '{amount + 1}', '{gch[0]}', '{gch[0] + gch[1]}', '{gch[1]}', '{objId}', '{UserHelper.GetUser().UserKey}', '{DateTime.Now.Date}', 1, '{unitCode}{_code}', '{objId}');";
                        SqlHelper.ExecuteNonQuery(insertSql);
                        LogsHelper.AddWorkLog(WorkLogType.Box, 1, OBJECT_ID, 1, primaryKey);
                    }
                    else if ("lbl_Plan_Box_Remove".Equals(label.Name))//删除
                    {
                        object _temp = SqlHelper.ExecuteOnlyOneQuery($"SELECT MAX(pb_box_number) FROM processing_box WHERE pb_obj_id='{objId}'");
                        if (_temp != null)
                        {
                            int currentBoxId = Convert.ToInt32(SqlHelper.ExecuteOnlyOneQuery($"SELECT pb_box_number FROM processing_box WHERE pb_id='{cbo_Plan_Box.SelectedValue}'"));
                            if (Convert.ToInt32(_temp) > currentBoxId)
                                XtraMessageBox.Show("请先删除较大盒号。", "删除失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            else if (XtraMessageBox.Show("删除当前案卷盒会清空盒下已归档的文件，是否继续？", "删除确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                object value = cbo_Plan_Box.SelectedValue;
                                if (value != null)
                                {
                                    SqlHelper.ExecuteNonQuery(
                                        $"UPDATE processing_file_list SET pfl_box_id=NULL WHERE pfl_box_id='{value}';" +
                                        $"DELETE FROM processing_box WHERE pb_id='{value}';");
                                }
                            }
                        }
                    }
                    LoadBoxList(objId, ControlType.Plan);
                }
            }
            else if (label.Name.Contains("lbl_Project_Box"))
            {
                object objId = tab_Project_Info.Tag;
                if (objId != null)
                {
                    if ("lbl_Project_Box_Add".Equals(label.Name))//新增
                    {
                        //当前已有盒号数量
                        int amount = SqlHelper.ExecuteCountQuery($"SELECT COUNT(pb_box_number) FROM processing_box WHERE pb_obj_id='{objId}'");
                        string[] gch = GetAJCode(objId, txt_Project_Code.Text, 1, txt_Project_Year.Text, txt_Special_Code.Text, ToolHelper.GetValue(unitCode));
                        object _code = ToolHelper.GetValue(Tag).StartsWith("ZX") ? Tag : string.Empty;
                        //默认档号和案卷名称为当前项目
                        string primaryKey = Guid.NewGuid().ToString();
                        string __code = txt_Project_Code.Text;
                        string _name = txt_Project_Name.Text;
                        string insertSql = $"INSERT INTO processing_tag(pt_id, pt_code, pt_name, pt_obj_id) VALUES('{primaryKey}', '{__code}', '{_name}', '{objId}');";
                        string pk = Guid.NewGuid().ToString();
                        insertSql += $"INSERT INTO processing_box(pb_id, pb_box_number, pb_gc_fix, pb_gc_id, pb_gc_number, pb_obj_id, pb_create_id, pb_create_date, pb_create_type, pb_unit_id, pt_id) " +
                           $"VALUES('{pk}', '{amount + 1}', '{gch[0]}', '{gch[0] + gch[1]}', '{gch[1]}', '{objId}', '{UserHelper.GetUser().UserKey}', '{DateTime.Now.Date}', 1, '{unitCode}{_code}', '{primaryKey}')";
                        SqlHelper.ExecuteNonQuery(insertSql);
                        LogsHelper.AddWorkLog(WorkLogType.Box, 1, OBJECT_ID, 1, pk);
                    }
                    else if ("lbl_Project_Box_Remove".Equals(label.Name))//删除
                    {
                        object _temp = SqlHelper.ExecuteOnlyOneQuery($"SELECT MAX(pb_box_number) FROM processing_box WHERE pb_obj_id='{objId}'");
                        if (_temp != null)
                        {
                            int currentBoxId = Convert.ToInt32(SqlHelper.ExecuteOnlyOneQuery($"SELECT pb_box_number FROM processing_box WHERE pb_id='{cbo_Project_Box.SelectedValue}'"));
                            if (Convert.ToInt32(_temp) > currentBoxId)
                                XtraMessageBox.Show("请先删除较大盒号。", "删除失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            else if (XtraMessageBox.Show("删除当前案卷盒会清空盒下已归档的文件，是否继续？", "删除确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                object value = cbo_Project_Box.SelectedValue;
                                if (value != null)
                                {
                                    SqlHelper.ExecuteNonQuery(
                                        $"UPDATE processing_file_list SET pfl_box_id=NULL WHERE pfl_box_id='{value}';" +
                                        $"DELETE FROM processing_tag WHERE pt_id=(SELECT pt_id FROM processing_box WHERE pb_id='{value}');" +
                                        $"DELETE FROM processing_box WHERE pb_id='{value}';");
                                }
                            }
                        }
                    }
                    LoadBoxList(objId, ControlType.Project);
                }
            }
            else if (label.Name.Contains("lbl_Subject_Box"))
            {
                object objId = tab_Subject_Info.Tag;
                if (objId != null)
                {
                    if ("lbl_Subject_Box_Add".Equals(label.Name))//新增
                    {
                        //当前已有盒号数量
                        int amount = SqlHelper.ExecuteCountQuery($"SELECT COUNT(pb_box_number) FROM processing_box WHERE pb_obj_id='{objId}'");
                        string[] gch = GetAJCode(objId, txt_Subject_Code.Text, 1, txt_Subject_Year.Text, txt_Special_Code.Text, ToolHelper.GetValue(unitCode));
                        object _code = ToolHelper.GetValue(Tag).StartsWith("ZX") ? Tag : string.Empty;
                        //默认档号和案卷名称为当前项目
                        string primaryKey = Guid.NewGuid().ToString();
                        string __code = txt_Subject_Code.Text;
                        string _name = txt_Subject_Name.Text;
                        string pk = Guid.NewGuid().ToString();
                        string insertSql = $"INSERT INTO processing_tag(pt_id, pt_code, pt_name, pt_obj_id) VALUES('{primaryKey}', '{__code}', '{_name}', '{objId}');";
                        insertSql += $"INSERT INTO processing_box(pb_id, pb_box_number, pb_gc_fix, pb_gc_id, pb_gc_number, pb_obj_id, pb_create_id, pb_create_date, pb_create_type, pb_unit_id, pt_id) " +
                            $"VALUES('{pk}', '{amount + 1}', '{gch[0]}', '{gch[0] + gch[1]}', '{gch[1]}', '{objId}', '{UserHelper.GetUser().UserKey}', '{DateTime.Now.Date}', 1, '{unitCode}{_code}', '{primaryKey}')";
                        SqlHelper.ExecuteNonQuery(insertSql);
                        LogsHelper.AddWorkLog(WorkLogType.Box, 1, OBJECT_ID, 1, pk);
                    }
                    else if ("lbl_Subject_Box_Remove".Equals(label.Name))//删除
                    {
                        object _temp = SqlHelper.ExecuteOnlyOneQuery($"SELECT MAX(pb_box_number) FROM processing_box WHERE pb_obj_id='{objId}'");
                        if (_temp != null)
                        {
                            object value = cbo_Subject_Box.SelectedValue;
                            int currentBoxId = Convert.ToInt32(SqlHelper.ExecuteOnlyOneQuery($"SELECT pb_box_number FROM processing_box WHERE pb_id='{value}'"));
                            if (Convert.ToInt32(_temp) > currentBoxId)
                                XtraMessageBox.Show("请先删除较大盒号。", "删除失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            else if (XtraMessageBox.Show("删除当前案卷盒会清空盒下已归档的文件，是否继续？", "删除确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                SqlHelper.ExecuteNonQuery(
                                        $"UPDATE processing_file_list SET pfl_box_id=NULL WHERE pfl_box_id='{value}';" +
                                        $"DELETE FROM processing_tag WHERE pt_id=(SELECT pt_id FROM processing_box WHERE pb_id='{value}');" +
                                        $"DELETE FROM processing_box WHERE pb_id='{value}';");
                            }
                        }
                    }
                    LoadBoxList(objId, ControlType.Subject);
                }
            }
            else if (label.Name.Contains("lbl_Topic_Box"))
            {
                object objId = tab_Topic_Info.Tag;
                if (objId != null)
                {
                    if ("lbl_Topic_Box_Add".Equals(label.Name))//新增
                    {
                        //当前已有盒号数量
                        int amount = SqlHelper.ExecuteCountQuery($"SELECT COUNT(pb_box_number) FROM processing_box WHERE pb_obj_id='{objId}'");
                        string[] gch = GetAJCode(objId, txt_Topic_Code.Text, 1, txt_Topic_Year.Text, txt_Special_Code.Text, ToolHelper.GetValue(unitCode));
                        object _code = ToolHelper.GetValue(Tag).StartsWith("ZX") ? Tag : string.Empty;
                        //默认档号和案卷名称为当前项目
                        string primaryKey = Guid.NewGuid().ToString();
                        string __code = txt_Topic_Code.Text;
                        string _name = txt_Topic_Name.Text;
                        string pk = Guid.NewGuid().ToString();
                        string insertSql = $"INSERT INTO processing_tag(pt_id, pt_code, pt_name, pt_obj_id) VALUES('{primaryKey}', '{__code}', '{_name}', '{objId}');";
                        insertSql += $"INSERT INTO processing_box(pb_id, pb_box_number, pb_gc_fix, pb_gc_id, pb_gc_number, pb_obj_id, pb_create_id, pb_create_date, pb_create_type, pb_unit_id, pt_id) " +
                            $"VALUES('{pk}', '{amount + 1}', '{gch[0]}', '{gch[0] + gch[1]}', '{gch[1]}', '{objId}', '{UserHelper.GetUser().UserKey}', '{DateTime.Now.Date}', 1, '{unitCode}{_code}', '{primaryKey}')";
                        SqlHelper.ExecuteNonQuery(insertSql);
                        LogsHelper.AddWorkLog(WorkLogType.Box, 1, OBJECT_ID, 1, pk);
                    }
                    else if ("lbl_Topic_Box_Remove".Equals(label.Name))//删除
                    {
                        object _temp = SqlHelper.ExecuteOnlyOneQuery($"SELECT MAX(pb_box_number) FROM processing_box WHERE pb_obj_id='{objId}'");
                        if (_temp != null)
                        {
                            object value = cbo_Topic_Box.SelectedValue;
                            int currentBoxId = Convert.ToInt32(SqlHelper.ExecuteOnlyOneQuery($"SELECT pb_box_number FROM processing_box WHERE pb_id='{value}'"));
                            if (Convert.ToInt32(_temp) > currentBoxId)
                                XtraMessageBox.Show("请先删除较大盒号。", "删除失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            else if (XtraMessageBox.Show("删除当前案卷盒会清空盒下已归档的文件，是否继续？", "删除确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                SqlHelper.ExecuteNonQuery(
                                        $"UPDATE processing_file_list SET pfl_box_id=NULL WHERE pfl_box_id='{value}';" +
                                        $"DELETE FROM processing_tag WHERE pt_id=(SELECT pt_id FROM processing_box WHERE pb_id='{value}');" +
                                        $"DELETE FROM processing_box WHERE pb_id='{value}';");
                            }
                        }
                    }
                    LoadBoxList(objId, ControlType.Topic);
                }
            }
            else if (label.Name.Contains("lbl_Imp_Box"))
            {
                object objId = tab_Imp_Info.Tag;
                if (objId != null)
                {
                    if ("lbl_Imp_Box_Add".Equals(label.Name))//新增
                    {
                        //当前已有盒号数量
                        int amount = SqlHelper.ExecuteCountQuery($"SELECT COUNT(pb_box_number) FROM processing_box WHERE pb_obj_id='{objId}'");
                        string[] gch = GetAJCode(objId, null, 1, DateTime.Now.Year.ToString(), txt_Special_Code.Text, ToolHelper.GetValue(unitCode));
                        object _code = ToolHelper.GetValue(Tag).StartsWith("ZX") ? Tag : string.Empty;
                        string primaryKey = Guid.NewGuid().ToString();
                        string insertSql = $"INSERT INTO processing_box(pb_id, pb_box_number, pb_gc_fix, pb_gc_id, pb_gc_number, pb_obj_id, pb_create_id, pb_create_date, pb_create_type, pb_unit_id) " +
                            $"VALUES('{primaryKey}', '{amount + 1}', '{gch[0]}', '{gch[0] + gch[1]}', '{gch[1]}', '{objId}', '{UserHelper.GetUser().UserKey}', '{DateTime.Now.Date}', 1, '{unitCode}{_code}')";
                        SqlHelper.ExecuteNonQuery(insertSql);
                        LogsHelper.AddWorkLog(WorkLogType.Box, 1, OBJECT_ID, 1, primaryKey);
                    }
                    else if ("lbl_Imp_Box_Remove".Equals(label.Name))//删除
                    {
                        object _temp = SqlHelper.ExecuteOnlyOneQuery($"SELECT MAX(pb_box_number) FROM processing_box WHERE pb_obj_id='{objId}'");
                        if (_temp != null)
                        {
                            object value = cbo_Imp_Box.SelectedValue;
                            int currentBoxId = Convert.ToInt32(SqlHelper.ExecuteOnlyOneQuery($"SELECT pb_box_number FROM processing_box WHERE pb_id='{value}'"));
                            if (Convert.ToInt32(_temp) > currentBoxId)
                                XtraMessageBox.Show("请先删除较大盒号。", "删除失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            else if (XtraMessageBox.Show("删除当前案卷盒会清空盒下已归档的文件，是否继续？", "删除确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                SqlHelper.ExecuteNonQuery(
                                        $"UPDATE processing_file_list SET pfl_box_id=NULL WHERE pfl_box_id='{value}';" +
                                        $"DELETE FROM processing_tag WHERE pt_id=(SELECT pt_id FROM processing_box WHERE pb_id='{value}');" +
                                        $"DELETE FROM processing_box WHERE pb_id='{value}';");
                            }
                        }
                    }
                    LoadBoxList(objId, ControlType.Imp);
                }
            }
            else if (label.Name.Contains("lbl_Special_Box"))
            {
                object objId = cbo_Special_AJ_Code.SelectedValue;
                if (objId != null)
                {
                    if ("lbl_Special_Box_Add".Equals(label.Name))//新增
                    {
                        //当前已有盒号数量
                        int amount = SqlHelper.ExecuteCountQuery($"SELECT COUNT(pb_box_number) FROM processing_box WHERE pb_obj_id='{objId}'");
                        string[] gch = GetAJCode(objId, null, 1, DateTime.Now.Year.ToString(), txt_Special_Code.Text, ToolHelper.GetValue(unitCode));
                        object _code = ToolHelper.GetValue(Tag).StartsWith("ZX") ? Tag : string.Empty;
                        string primaryKey = Guid.NewGuid().ToString();
                        string insertSql = $"INSERT INTO processing_box(pb_id, pb_box_number, pb_gc_fix, pb_gc_id, pb_gc_number, pb_obj_id, pb_create_id, pb_create_date, pb_create_type, pb_unit_id, pt_id) " +
                            $"VALUES('{primaryKey}', '{amount + 1}', '{gch[0]}', '{gch[0] + gch[1]}', '{gch[1]}', '{objId}', '{UserHelper.GetUser().UserKey}', '{DateTime.Now.Date}', 1, '{unitCode}{_code}', '{objId}')";
                        SqlHelper.ExecuteNonQuery(insertSql);
                        LogsHelper.AddWorkLog(WorkLogType.Box, 1, OBJECT_ID, 1, primaryKey);
                    }
                    else if ("lbl_Special_Box_Remove".Equals(label.Name))//删除
                    {
                        object _temp = SqlHelper.ExecuteOnlyOneQuery($"SELECT MAX(pb_box_number) FROM processing_box WHERE pb_obj_id='{objId}'");
                        if (_temp != null)
                        {
                            object value = cbo_Special_Box.SelectedValue;
                            int currentBoxId = Convert.ToInt32(SqlHelper.ExecuteOnlyOneQuery($"SELECT pb_box_number FROM processing_box WHERE pb_id='{value}'"));
                            if (Convert.ToInt32(_temp) > currentBoxId)
                                XtraMessageBox.Show("请先删除较大盒号。", "删除失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            else if (XtraMessageBox.Show("删除当前案卷盒会清空盒下已归档的文件，是否继续？", "删除确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                SqlHelper.ExecuteNonQuery(
                                        $"UPDATE processing_file_list SET pfl_box_id=NULL WHERE pfl_box_id='{value}';" +
                                        $"DELETE FROM processing_box WHERE pb_id='{value}';");
                            }
                        }
                    }
                    LoadBoxList(objId, ControlType.Special);
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
            DataTable table = SqlHelper.ExecuteQuery($"SELECT * FROM processing_box WHERE pb_obj_id='{objId}' ORDER BY pb_box_number ASC");
            if (type == ControlType.Plan)
            {
                cbo_Plan_Box.DataSource = table;
                cbo_Plan_Box.DisplayMember = "pb_box_number";
                cbo_Plan_Box.ValueMember = "pb_id";
                if (table.Rows.Count > 0)
                {
                    int maxAmount = cbo_Plan_Box.Items.Count;
                    if (maxAmount > 0)
                        cbo_Plan_Box.SelectedIndex = maxAmount - 1;
                }
                Cbo_Box_SelectionChangeCommitted(cbo_Plan_Box, null);
            }
            else if (type == ControlType.Project)
            {
                txt_Project_AJ_Code.ResetText();
                txt_Project_AJ_Name.ResetText();
                cbo_Project_Box.DataSource = table;
                cbo_Project_Box.DisplayMember = "pb_box_number";
                cbo_Project_Box.ValueMember = "pb_id";
                if (table.Rows.Count > 0)
                {
                    int maxAmount = cbo_Project_Box.Items.Count;
                    if (maxAmount > 0)
                        cbo_Project_Box.SelectedIndex = maxAmount - 1;
                }
                Cbo_Box_SelectionChangeCommitted(cbo_Project_Box, null);
            }
            else if (type == ControlType.Topic)
            {
                txt_Topic_AJ_Code.ResetText();
                txt_Topic_AJ_Name.ResetText();
                cbo_Topic_Box.DataSource = table;
                cbo_Topic_Box.DisplayMember = "pb_box_number";
                cbo_Topic_Box.ValueMember = "pb_id";
                if (table.Rows.Count > 0)
                {
                    int maxAmount = cbo_Topic_Box.Items.Count;
                    if (maxAmount > 0)
                        cbo_Topic_Box.SelectedIndex = maxAmount - 1;
                }
                Cbo_Box_SelectionChangeCommitted(cbo_Topic_Box, null);
            }
            else if (type == ControlType.Subject)
            {
                txt_Subject_AJ_Code.ResetText();
                txt_Subject_AJ_Name.ResetText();
                cbo_Subject_Box.DataSource = table;
                cbo_Subject_Box.DisplayMember = "pb_box_number";
                cbo_Subject_Box.ValueMember = "pb_id";
                if (table.Rows.Count > 0)
                {
                    int maxAmount = cbo_Subject_Box.Items.Count;
                    if (maxAmount > 0)
                        cbo_Subject_Box.SelectedIndex = maxAmount - 1;
                }
                Cbo_Box_SelectionChangeCommitted(cbo_Subject_Box, null);
            }
            else if (type == ControlType.Imp)
            {
                txt_Imp_AJ_Code.ResetText();
                txt_Imp_AJ_Name.ResetText();
                cbo_Imp_Box.DataSource = table;
                cbo_Imp_Box.DisplayMember = "pb_box_number";
                cbo_Imp_Box.ValueMember = "pb_id";
                if (table.Rows.Count > 0)
                {
                    int maxAmount = cbo_Imp_Box.Items.Count;
                    if (maxAmount > 0)
                        cbo_Imp_Box.SelectedIndex = maxAmount - 1;
                }
                Cbo_Box_SelectionChangeCommitted(cbo_Imp_Box, null);
            }
            else if (type == ControlType.Special)
            {
                cbo_Special_Box.DataSource = table;
                cbo_Special_Box.DisplayMember = "pb_box_number";
                cbo_Special_Box.ValueMember = "pb_id";
                if (table.Rows.Count > 0)
                {
                    int maxAmount = cbo_Special_Box.Items.Count;
                    if (maxAmount > 0)
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
            errorProvider1.Clear();
            System.Windows.Forms.ComboBox comboBox = sender as System.Windows.Forms.ComboBox;
            if (comboBox.Name.Contains("Plan"))
            {
                object pbId = comboBox.SelectedValue;
                LoadFileBoxTable(pbId, cbo_Plan_AJ_Code.SelectedValue, ControlType.Plan);
                object value = SqlHelper.ExecuteOnlyOneQuery($"SELECT a.pb_gc_id FROM processing_box a WHERE a.pb_id='{pbId}'");
                txt_Plan_GCID.Text = ToolHelper.GetValue(value);
            }
            else if (comboBox.Name.Contains("Project"))
            {
                object pbId = comboBox.SelectedValue;
                LoadFileBoxTable(pbId, tab_Project_Info.Tag, ControlType.Project);
                DataRow dataRow = SqlHelper.ExecuteSingleRowQuery("SELECT b.pt_code, b.pt_name, a.pb_gc_id FROM processing_box a " +
                    $"LEFT JOIN processing_tag b ON a.pt_id = b.pt_id WHERE a.pb_id='{pbId}'");
                if (dataRow != null)
                {
                    txt_Project_AJ_Code.Text = ToolHelper.GetValue(dataRow["pt_code"]);
                    txt_Project_AJ_Name.Text = ToolHelper.GetValue(dataRow["pt_name"]);
                    txt_Project_GCID.Text = ToolHelper.GetValue(dataRow["pb_gc_id"]);
                }
            }
            else if (comboBox.Name.Contains("Topic"))
            {
                object pbId = comboBox.SelectedValue;
                LoadFileBoxTable(pbId, tab_Topic_Info.Tag, ControlType.Topic);
                DataRow dataRow = SqlHelper.ExecuteSingleRowQuery("SELECT b.pt_code, b.pt_name, a.pb_gc_id FROM processing_box a " +
                    $"LEFT JOIN processing_tag b ON a.pt_id = b.pt_id WHERE a.pb_id='{pbId}'");
                if (dataRow != null)
                {
                    txt_Topic_AJ_Code.Text = ToolHelper.GetValue(dataRow["pt_code"]);
                    txt_Topic_AJ_Name.Text = ToolHelper.GetValue(dataRow["pt_name"]);
                    txt_Topic_GCID.Text = ToolHelper.GetValue(dataRow["pb_gc_id"]);
                }
            }
            else if (comboBox.Name.Contains("Subject"))
            {
                object pbId = comboBox.SelectedValue;
                LoadFileBoxTable(pbId, tab_Subject_Info.Tag, ControlType.Subject);
                DataRow dataRow = SqlHelper.ExecuteSingleRowQuery("SELECT b.pt_code, b.pt_name, a.pb_gc_id FROM processing_box a " +
                    $"LEFT JOIN processing_tag b ON a.pt_id = b.pt_id WHERE a.pb_id='{pbId}'");
                if (dataRow != null)
                {
                    txt_Subject_AJ_Code.Text = ToolHelper.GetValue(dataRow["pt_code"]);
                    txt_Subject_AJ_Name.Text = ToolHelper.GetValue(dataRow["pt_name"]);
                    txt_Subject_GCID.Text = ToolHelper.GetValue(dataRow["pb_gc_id"]);
                }
            }
            else if (comboBox.Name.Contains("Imp"))
            {
                object pbId = comboBox.SelectedValue;
                LoadFileBoxTable(pbId, tab_Imp_Info.Tag, ControlType.Imp);
                DataRow dataRow = SqlHelper.ExecuteSingleRowQuery("SELECT b.pt_code, b.pt_name, a.pb_gc_id FROM processing_box a " +
                    $"LEFT JOIN processing_tag b ON a.pt_id = b.pt_id WHERE a.pb_id='{pbId}'");
                if (dataRow != null)
                {
                    txt_Imp_AJ_Code.Text = ToolHelper.GetValue(dataRow["pt_code"]);
                    txt_Imp_AJ_Name.Text = ToolHelper.GetValue(dataRow["pt_name"]);
                    txt_Imp_GCID.Text = ToolHelper.GetValue(dataRow["pb_gc_id"]);
                }
            }
            else if (comboBox.Name.Contains("Special"))
            {
                object pbId = comboBox.SelectedValue;
                LoadFileBoxTable(pbId, cbo_Special_AJ_Code.SelectedValue, ControlType.Special);
                object value = SqlHelper.ExecuteOnlyOneQuery($"SELECT a.pb_gc_id FROM processing_box a WHERE a.pb_id='{pbId}'");
                txt_Special_GCID.Text = ToolHelper.GetValue(value);
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
            if (type == ControlType.Project)
            {
                pal_Project_BtnGroup.Enabled = !(node.ForeColor == DisEnbleColor);
                DataRow row = SqlHelper.ExecuteSingleRowQuery("SELECT pi.*, od.oCount FROM project_info pi " +
                    $"LEFT JOIN(SELECT od_obj_id, COUNT(od_id) oCount FROM other_doc GROUP BY od_obj_id) od ON od.od_obj_id = pi.pi_id WHERE pi.pi_id='{node.Name}'");
                if (row != null)
                {
                    tab_Project_Info.Tag = row["pi_id"];
                    txt_Project_Code.Text = ToolHelper.GetValue(row["pi_code"]);
                    txt_Project_Name.Text = ToolHelper.GetValue(row["pi_name"]);
                    txt_Project_Field.Text = ToolHelper.GetValue(row["pi_field"]);
                    txt_Project_Theme.Text = ToolHelper.GetValue(row["pb_theme"]);
                    txt_Project_Funds.Text = ToolHelper.GetValue(row["pi_funds"]);
                    lbl_Project_Tip.Text = $"著录人：{UserHelper.GetUserNameById(row["pi_worker_id"])}          质检人：{UserHelper.GetUserNameById(row["pi_checker_id"])}";
                    txt_Project_StartTime.Text = ToolHelper.GetDateValue(row["pi_start_datetime"], "yyyy-MM-dd");
                    txt_Project_EndTime.Text = ToolHelper.GetDateValue(row["pi_end_datetime"], "yyyy-MM-dd");
                    txt_Project_Year.Text = ToolHelper.GetValue(row["pi_year"]);
                    txt_Project_Unit.Text = ToolHelper.GetValue(row["pi_unit"]);
                    cbo_Project_Province.SelectedValue = ToolHelper.GetValue(row["pi_province"]);
                    txt_Project_UnitUser.Text = ToolHelper.GetValue(row["pi_uniter"]);
                    txt_Project_ProUser.Text = ToolHelper.GetValue(row["pi_prouser"]);
                    txt_Project_Intro.Text = ToolHelper.GetValue(row["pi_intro"]);
                    bool flag = Convert.ToInt32(row["pi_submit_status"]) != 2;
                    EnableControls(type, flag);
                    project.Tag = row["pi_obj_id"];
                    int otherDocsCount = ToolHelper.GetIntValue(row["oCount"], 0);
                    if (otherDocsCount != 0)
                        btn_Project_OtherDoc.Text = $"其它载体档案({otherDocsCount})";

                    if (isBacked)
                    {
                        btn_Project_QTReason.Text = $"质检意见({GetAdvincesAmount(node.Name)})";
                        cbo_Project_HasNext.Enabled = flag;
                    }
                    //灰色背景优先级高于加工人优先级
                    if (pal_Project_BtnGroup.Enabled)
                    {
                        bool result = row["pi_worker_id"].Equals(UserHelper.GetUser().UserKey) || row["pi_worker_id"] is DBNull;
                        pal_Project_BtnGroup.Enabled = result;
                    }
                }

                tab_Project_Info.SelectedTabPageIndex = 0;
                LoadFileList(dgv_Project_FileList, node.Name, -1);
            }
            else if (type == ControlType.Topic)
            {
                pal_Topic_BtnGroup.Enabled = !(node.ForeColor == DisEnbleColor);
                DataRow row = SqlHelper.ExecuteSingleRowQuery($"SELECT pi.*, od.oCount FROM topic_info pi " +
                    $"LEFT JOIN(SELECT od_obj_id, COUNT(od_id) oCount FROM other_doc GROUP BY od_obj_id) od ON od.od_obj_id = pi.ti_id WHERE pi.ti_id='{node.Name}'");
                if (row != null)
                {
                    tab_Topic_Info.Tag = row["ti_id"];
                    txt_Topic_Code.Text = ToolHelper.GetValue(row["ti_code"]);
                    txt_Topic_Name.Text = ToolHelper.GetValue(row["ti_name"]);
                    txt_Topic_Field.Text = ToolHelper.GetValue(row["ti_field"]);
                    txt_Topic_Theme.Text = ToolHelper.GetValue(row["tb_theme"]);
                    txt_Topic_Funds.Text = ToolHelper.GetValue(row["ti_funds"]);
                    lbl_Topic_Tip.Text = $"著录人：{UserHelper.GetUserNameById(row["ti_worker_id"])}          质检人：{UserHelper.GetUserNameById(row["ti_checker_id"])}";
                    txt_Topic_StartTime.Text = ToolHelper.GetDateValue(row["ti_start_datetime"], "yyyy-MM-dd");
                    txt_Topic_EndTime.Text = ToolHelper.GetDateValue(row["ti_end_datetime"], "yyyy-MM-dd");
                    txt_Topic_Year.Text = ToolHelper.GetValue(row["ti_year"]);
                    txt_Topic_Unit.Text = ToolHelper.GetValue(row["ti_unit"]);
                    cbo_Topic_Province.SelectedValue = ToolHelper.GetValue(row["ti_province"]);
                    txt_Topic_UnitUser.Text = ToolHelper.GetValue(row["ti_uniter"]);
                    txt_Topic_ProUser.Text = ToolHelper.GetValue(row["ti_prouser"]);
                    txt_Topic_Intro.Text = ToolHelper.GetValue(row["ti_intro"]);
                    bool flag = Convert.ToInt32(row["ti_submit_status"]) != 2;
                    EnableControls(type, flag);
                    topic.Tag = row["ti_obj_id"];

                    int otherDocsCount = ToolHelper.GetIntValue(row["oCount"], 0);
                    if (otherDocsCount != 0)
                        btn_Topic_OtherDoc.Text = $"其它载体档案({otherDocsCount})";

                    if (pal_Topic_BtnGroup.Enabled)
                    {
                        bool result = row["ti_worker_id"].Equals(UserHelper.GetUser().UserKey) || row["ti_worker_id"] is DBNull;
                        pal_Topic_BtnGroup.Enabled = result;
                    }
                    if (isBacked)
                    {
                        btn_Topic_QTReason.Text = $"质检意见({GetAdvincesAmount(node.Name)})";
                        cbo_Topic_HasNext.Enabled = flag;
                    }
                }

                tab_Topic_Info.SelectedTabPageIndex = 0;
                LoadFileList(dgv_Topic_FileList, node.Name, -1);
            }
            else if (type == ControlType.Subject)
            {
                pal_Subject_BtnGroup.Enabled = !(node.ForeColor == DisEnbleColor);
                DataTable table = SqlHelper.ExecuteQuery($"SELECT pi.*, od.oCount FROM subject_info pi " +
                    $"LEFT JOIN(SELECT od_obj_id, COUNT(od_id) oCount FROM other_doc GROUP BY od_obj_id) od ON od.od_obj_id = pi.si_id WHERE pi.si_id='{node.Name}'");
                if (table.Rows.Count > 0)
                {
                    DataRow row = table.Rows[0];
                    tab_Subject_Info.Tag = row["si_id"];
                    pal_Subject.Tag = row["si_obj_id"];
                    txt_Subject_Code.Text = ToolHelper.GetValue(row["si_code"]);
                    txt_Subject_Name.Text = ToolHelper.GetValue(row["si_name"]);
                    txt_Subject_Field.Text = ToolHelper.GetValue(row["si_field"]);
                    txt_Subject_Theme.Text = ToolHelper.GetValue(row["si_theme"]);
                    txt_Subject_Funds.Text = ToolHelper.GetValue(row["si_funds"]);
                    lbl_Subject_Tip.Text = $"著录人：{UserHelper.GetUserNameById(row["si_worker_id"])}          质检人：{UserHelper.GetUserNameById(row["si_checker_id"])}";
                    txt_Subject_StartTime.Text = ToolHelper.GetDateValue(row["si_start_datetime"], "yyyy-MM-dd");
                    txt_Subject_EndTime.Text = ToolHelper.GetDateValue(row["si_end_datetime"], "yyyy-MM-dd");
                    txt_Subject_Year.Text = ToolHelper.GetValue(row["si_year"]);
                    txt_Subject_Unit.Text = ToolHelper.GetValue(row["si_unit"]);
                    cbo_Subject_Province.SelectedValue = ToolHelper.GetValue(row["si_province"]);
                    txt_Subject_Unituser.Text = ToolHelper.GetValue(row["si_uniter"]);
                    txt_Subject_ProUser.Text = ToolHelper.GetValue(row["si_prouser"]);
                    txt_Subject_Intro.Text = ToolHelper.GetValue(row["si_intro"]);
                    bool flag = Convert.ToInt32(row["si_submit_status"]) != 2;
                    EnableControls(type, flag);
                    subject.Tag = row["si_obj_id"];

                    int otherDocsCount = ToolHelper.GetIntValue(row["oCount"], 0);
                    if (otherDocsCount != 0)
                        btn_Subject_OtherDoc.Text = $"其它载体档案({otherDocsCount})";

                    if (pal_Subject_BtnGroup.Enabled)
                    {
                        bool result = row["si_worker_id"].Equals(UserHelper.GetUser().UserKey) || row["si_worker_id"] is DBNull;
                        pal_Subject_BtnGroup.Enabled = result;
                    }
                    if (isBacked)
                    {
                        btn_Subject_QTReason.Text = $"质检意见({GetAdvincesAmount(node.Name)})";
                    }

                }

                tab_Subject_Info.SelectedTabPageIndex = 0;
                LoadFileList(dgv_Subject_FileList, node.Name, -1);
            }
            else if (type == ControlType.Special)
            {
                DataRow row = SqlHelper.ExecuteSingleRowQuery($"SELECT * FROM imp_dev_info WHERE imp_id='{node.Name}'");
                if (row != null)
                {
                    txt_Special_Code.Text = ToolHelper.GetValue(row["imp_code"]);
                    txt_Special_Name.Text = ToolHelper.GetValue(row["imp_name"]);
                    txt_Special_Unit.Text = ToolHelper.GetValue(row["imp_unit"]);
                    lbl_Special_Tip.Text = $"著录人：{UserHelper.GetUserNameById(row["imp_source_id"])}";
                    txt_Special_Intro.Text = ToolHelper.GetValue(row["imp_intro"]);
                    tab_Special_Info.Tag = ToolHelper.GetValue(row["imp_id"]);
                    bool flag = Convert.ToInt32(row["imp_submit_status"]) != 2;
                    EnableControls(ControlType.Special, flag);
                    special.Tag = row["imp_obj_id"];
                    if (isBacked)
                    {
                        btn_Special_QTReason.Text = $"质检意见({GetAdvincesAmount(node.Name)})";
                        cbo_Special_HasNext.Enabled = flag;
                    }

                    LoadDocumentList(node.Name, ControlType.Special);
                }
                cbo_Special_HasNext.SelectedIndex = 0;

                pal_Special_BtnGroup.Enabled = !(node.ForeColor == DisEnbleColor);
                if (pal_Special_BtnGroup.Enabled)
                {
                    bool result = row["imp_source_id"].Equals(UserHelper.GetUser().UserKey) || row["imp_source_id"] is DBNull;
                    pal_Special_BtnGroup.Enabled = result;
                }

                tab_Special_Info.SelectedTabPageIndex = 0;
            }
        }

        /// <summary>
        /// 新增对象事件
        /// </summary>
        private void Btn_Add_Click(object sender, EventArgs e)
        {
            KyoButton button = sender as KyoButton;
            if (button.Name.Contains("Plan"))
            {
                ResetControls(ControlType.Plan);
            }
            else if (button.Name.Contains("Imp"))
            {
                ResetControls(ControlType.Imp);
            }
            else if (button.Name.Contains("Special"))
            {
                ResetControls(ControlType.Special);
            }
            else if (button.Name.Contains("Project"))
            {
                ResetControls(ControlType.Project);
            }
            else if (button.Name.Contains("Topic"))
            {
                ResetControls(ControlType.Topic);
            }
            else if (button.Name.Contains("Subject"))
            {
                ResetControls(ControlType.Subject);
            }
        }

        /// <summary>
        /// 重置控件为默认状态
        /// </summary>
        /// <param name="type">对象类型</param>
        private void ResetControls(ControlType type)
        {
            if (type == ControlType.Plan)
            {
                tab_Plan_Info.Tag = null;
            }
            else if (type == ControlType.Imp)
            {
                tab_Imp_Info.Tag = null;
            }
            else if (type == ControlType.Special)
            {
                tab_Special_Info.Tag = null;
            }
            else if (type == ControlType.Project)
            {
                tab_Project_Info.Tag = null;
                DataGridViewStyleHelper.ResetDataGridView(dgv_Project_FileList, false);
                DataGridViewStyleHelper.ResetDataGridView(dgv_Project_FileValid, false);
                foreach (Control item in pal_Project.Controls)
                {
                    if (!(item is Label) && !(item is KyoButton) && !(item is DateTimePicker))
                        item.ResetText();
                }
                EnableControls(type, true);
            }
            else if (type == ControlType.Topic)
            {
                tab_Topic_Info.Tag = null;
                DataGridViewStyleHelper.ResetDataGridView(dgv_Topic_FileList, false);
                DataGridViewStyleHelper.ResetDataGridView(dgv_Topic_FileValid, false);
                foreach (Control item in pal_Topic.Controls)
                {
                    if (!(item is Label) && !(item is KyoButton) && !(item is DateTimePicker))
                        item.ResetText();
                }
                EnableControls(type, true);
            }
            else if (type == ControlType.Subject)
            {
                tab_Subject_Info.Tag = null;
                DataGridViewStyleHelper.ResetDataGridView(dgv_Subject_FileList, false);
                DataGridViewStyleHelper.ResetDataGridView(dgv_Subject_FileValid, false);
                foreach (Control item in pal_Subject.Controls)
                {
                    if (!(item is Label) && !(item is KyoButton) && !(item is DateTimePicker))
                        item.ResetText();
                }
                EnableControls(type, true);
            }
            LoadDocList(null, type);
        }

        /// <summary>
        /// 控制控件的可用性
        /// </summary>
        /// <param name="type">对象类型</param>
        /// <param name="enable">是否可用</param>
        /// <param name="nextEnable">下一级是否可用</param>
        void EnableControls(ControlType type, bool enable)
        {
            if (type == ControlType.Plan)
            {
                pal_Plan_BtnGroup.Enabled = enable;
                foreach (Control item in pal_Plan_BtnGroup.Controls)
                {
                    item.Enabled = enable;
                    if (item.Name.Contains("Submit"))
                        item.Text = enable ? "提交" : "已提交";
                }
            }
            else if (type == ControlType.Project)
            {
                pal_Project_BtnGroup.Enabled = enable;
                foreach (Control item in pal_Project_BtnGroup.Controls)
                {
                    item.Enabled = enable;
                    if (item.Name.Contains("Submit"))
                        item.Text = enable ? "提交" : "已提交";
                }
            }
            else if (type == ControlType.Subject)
            {
                pal_Subject_BtnGroup.Enabled = enable;
                foreach (Control item in pal_Subject_BtnGroup.Controls)
                {
                    item.Enabled = enable;
                    if (item.Name.Contains("Submit"))
                        item.Text = enable ? "提交" : "已提交";
                }
            }
            else if (type == ControlType.Topic)
            {
                pal_Topic_BtnGroup.Enabled = enable;
                foreach (Control item in pal_Topic_BtnGroup.Controls)
                {
                    item.Enabled = enable;
                    if (item.Name.Contains("Submit"))
                        item.Text = enable ? "提交" : "已提交";
                }
            }
            else if (type == ControlType.Imp)
            {
                pal_Imp_BtnGroup.Enabled = enable;
                foreach (Control item in pal_Imp_BtnGroup.Controls)
                {
                    item.Enabled = enable;
                    if (item.Name.Contains("Submit"))
                        item.Text = enable ? "提交" : "已提交";
                }
            }
            else if (type == ControlType.Special)
            {
                pal_Special_BtnGroup.Enabled = enable;
                foreach (Control item in pal_Special_BtnGroup.Controls)
                {
                    item.Enabled = enable;
                    if (item.Name.Contains("Submit"))
                        item.Text = enable ? "提交" : "已提交";
                }
            }
        }

        /// <summary>
        /// 提交事件
        /// </summary>
        private void Btn_Submit_Click(object sender, EventArgs e)
        {
            if (XtraMessageBox.Show("提交前请先确保所有数据已保存，确认要提交吗?", "提交确认", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                object userKey = UserHelper.GetUser().UserKey;
                string name = (sender as KyoButton).Name;
                object objId = null;
                if (name.Contains("Plan"))
                {
                    objId = tab_Plan_Info.Tag;
                    if (objId != null)
                    {
                        SqlHelper.ExecuteNonQuery($"UPDATE project_info SET pi_submit_status={(int)SubmitStatus.SubmitSuccess}, pi_worker_id='{userKey}' WHERE pi_id='{objId}'");
                        EnableControls(ControlType.Plan, false);
                    }
                    else
                        XtraMessageBox.Show("请先保存数据。");
                }
                else if (name.Contains("Project"))
                {
                    objId = tab_Project_Info.Tag;
                    if (objId != null)
                    {
                        SqlHelper.ExecuteNonQuery($"UPDATE project_info SET pi_submit_status={(int)SubmitStatus.SubmitSuccess}, pi_worker_id='{userKey}' WHERE pi_id='{objId}'");
                        EnableControls(ControlType.Project, false);
                    }
                    else
                        XtraMessageBox.Show("请先保存数据。");
                }
                else if (name.Contains("Topic"))
                {
                    objId = tab_Topic_Info.Tag;
                    if (objId != null)
                    {
                        SqlHelper.ExecuteNonQuery($"UPDATE topic_info SET ti_submit_status='{(int)SubmitStatus.SubmitSuccess}', ti_worker_id='{userKey}' WHERE ti_id='{objId}'");
                        EnableControls(ControlType.Topic, false);
                    }
                    else
                        XtraMessageBox.Show("请先保存数据.");
                }
                else if (name.Contains("Subject"))
                {
                    objId = tab_Subject_Info.Tag;
                    if (objId != null)
                    {
                        SqlHelper.ExecuteNonQuery($"UPDATE subject_info SET si_submit_status='{(int)SubmitStatus.SubmitSuccess}', si_worker_id='{userKey}' WHERE si_id='{objId}'");
                        EnableControls(ControlType.Subject, false);
                    }
                    else
                        XtraMessageBox.Show("请先保存数据.");
                }
                else if (name.Contains("Imp"))
                {
                    objId = tab_Imp_Info.Tag;
                    if (objId != null)
                    {
                        SqlHelper.ExecuteNonQuery($"UPDATE imp_info SET imp_submit_status='{(int)ObjectSubmitStatus.SubmitSuccess}', imp_source_id='{userKey}' WHERE imp_id='{objId}'");
                        EnableControls(ControlType.Imp, false);
                    }
                    else
                        XtraMessageBox.Show("请先保存数据。");
                }
                else if (name.Contains("Special"))
                {
                    objId = tab_Special_Info.Tag;
                    if (objId != null)
                    {
                        SqlHelper.ExecuteNonQuery($"UPDATE imp_dev_info SET imp_submit_status='{(int)ObjectSubmitStatus.SubmitSuccess}', imp_source_id='{userKey}' WHERE imp_id='{objId}'");
                        EnableControls(ControlType.Special, false);
                    }
                    else
                        XtraMessageBox.Show("请先保存数据。");
                }
                if (objId != null && treeView.Nodes.Count > 0)
                {
                    TreeNode treeNode = treeView.SelectedNode;
                    if (treeNode != null)
                    {
                        treeNode.ForeColor = DisEnbleColor;
                    }
                }
            }
        }

        /// <summary>
        /// 下拉框切换事件
        /// </summary>
        private void Cbo_Imp_HasNext_SelectionChangeCommitted(object sender, EventArgs e)
        {
            object id = tab_Imp_Info.Tag;
            if (id != null)
            {
                int index = tab_MenuList.SelectedIndex + 1;
                ShowTab("special", index);
                ResetControls(ControlType.Special);

                object value = cbo_Imp_HasNext.SelectedValue;
                DataRow row = SqlHelper.ExecuteSingleRowQuery($"SELECT dd_code, dd_name, dd_note FROM data_dictionary WHERE dd_id='{value}'");
                if (row != null)
                {
                    txt_Special_Code.Text = ToolHelper.GetValue(row["dd_code"]);
                    txt_Special_Name.Text = ToolHelper.GetValue(row["dd_name"]);
                    txt_Special_Intro.Text = ToolHelper.GetValue(row["dd_note"]);
                }
                special.Tag = id;
                cbo_Special_HasNext.SelectedIndex = 0;

                if (workType == WorkType.PaperWork_Special)
                    special.Text = "研发信息";

                tab_MenuList.SelectedIndex = index;
            }
            else
            {
                XtraMessageBox.Show("请先保存当前信息。", "操作失败", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                if (cbo_Imp_HasNext.Items.Count > 0)
                    cbo_Imp_HasNext.SelectedIndex = 0;
            }
        }

        /// <summary>
        /// 新重大专项继承旧数据
        /// </summary>
        private void InheritImpInfo(object oldImpId, object oldSpeId, object newImpId)
        {
            string updateSQL =
                $"UPDATE processing_file_list SET pfl_obj_id='{newImpId}' WHERE pfl_obj_id='{oldImpId}'; " +
                $"UPDATE processing_tag SET pt_obj_id='{newImpId}' WHERE pt_obj_id='{oldImpId}';" +
                $"UPDATE processing_box SET pb_obj_id='{newImpId}' WHERE pb_obj_id='{oldImpId}';" +
                $"UPDATE imp_dev_info SET imp_obj_id='{newImpId}', imp_submit_status=1, imp_source_id=null WHERE imp_id='{oldSpeId}';";
            SqlHelper.ExecuteNonQuery(updateSQL);
        }

        /// <summary>
        /// 重置指定专项下所有项目课题状态为 未提交
        /// </summary>
        /// <param name="specialId">专项ID</param>
        private void ResetProjectState(object specialId)
        {
            string updateSQL = string.Empty;
            string proQuerySql = "SELECT A.pi_id FROM imp_dev_info p INNER JOIN ( " +
                "SELECT pi_id, pi_obj_id FROM project_info WHERE pi_categor=2 UNION ALL " +
                "SELECT ti_id, ti_obj_id FROM topic_info WHERE ti_categor=-3)A ON A.pi_obj_id = p.imp_id " +
               $"WHERE p.imp_id='{specialId}'";
            object[] proIds = SqlHelper.ExecuteSingleColumnQuery(proQuerySql);
            if (proIds.Length > 0)
            {
                string pids = ToolHelper.GetStringBySplit(proIds, ",", "'");
                updateSQL +=
                    $"UPDATE project_info SET pi_submit_status=1, pi_worker_id=null, pi_checker_id=null WHERE pi_id IN ({pids});" +
                    $"UPDATE topic_info SET ti_submit_status=1, ti_worker_id=null, ti_checker_id=null WHERE ti_id IN ({pids});";
                string topQuerySql = "SELECT A.ti_id FROM( " +
                    "SELECT ti_id, ti_obj_id FROM topic_info WHERE ti_categor = 3 UNION ALL " +
                    "SELECT si_id, si_obj_id FROM subject_info )A " +
                   $"WHERE A.ti_obj_id IN({pids})";
                object[] topIds = SqlHelper.ExecuteSingleColumnQuery(topQuerySql);
                if (topIds.Length > 0)
                {
                    string tids = ToolHelper.GetStringBySplit(topIds, ",", "'");
                    updateSQL +=
                       $"UPDATE topic_info SET ti_submit_status=1, ti_worker_id=null, ti_checker_id=null WHERE ti_id IN ({tids});" +
                       $"UPDATE subject_info SET si_submit_status=1, si_worker_id=null, si_checker_id=null WHERE si_id IN ({tids});" +
                       $"UPDATE subject_info SET si_submit_status=1, si_worker_id=null, si_checker_id=null WHERE si_obj_id IN ({tids});";
                }
            }
            SqlHelper.ExecuteNonQuery(updateSQL);
        }

        /// <summary>
        /// 文件链接点击事件
        /// </summary>
        private void Dgv_FileList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1 && e.ColumnIndex != -1)
            {
                DataGridView dataGridView = sender as DataGridView;
                if (dataGridView.Columns[e.ColumnIndex].Name.Contains("link"))
                {
                    string path = ToolHelper.GetValue(dataGridView.CurrentCell.Value);
                    if (!string.IsNullOrEmpty(path))
                    {
                        if (path.Contains("；"))
                        {
                            string[] linkString = path.Split('；');
                            Frm_FileList fileList = GetFormHelper.GetFileList(linkString);
                            fileList.Show();
                            fileList.Activate();
                        }
                        else if (File.Exists(path))
                        {
                            if (XtraMessageBox.Show("是否打开文件?", "确认提示", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk) == DialogResult.Yes)
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
            if ("btn_Plan_AddFile".Equals(name))
            {
                key = "plan_fl_";
                object objId = cbo_Plan_AJ_Code.SelectedValue;
                if (objId != null)
                {
                    if (dgv_Plan_FileList.SelectedRows.Count == 1 && dgv_Plan_FileList.RowCount != 1)
                        frm = new Frm_AddFile(dgv_Plan_FileList, key, dgv_Plan_FileList.SelectedRows[0].Cells[key + "id"].Value, trcId, OBJECT_ID);
                    else
                        frm = new Frm_AddFile(dgv_Plan_FileList, key, null, trcId, OBJECT_ID);
                    frm.txt_Unit.Text = UserHelper.GetUser().UnitName;
                    frm.parentId = objId;
                    frm.UpdateDataSource = LoadFileList;
                    frm.Show();
                }
                else
                    XtraMessageBox.Show("请先添加档号信息。", "温馨提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            else if ("btn_Project_AddFile".Equals(name))
            {
                key = "project_fl_";
                object objId = tab_Project_Info.Tag;
                if (objId != null)
                {
                    if (dgv_Project_FileList.SelectedRows.Count == 1 && dgv_Project_FileList.RowCount != 1)
                        frm = new Frm_AddFile(dgv_Project_FileList, key, dgv_Project_FileList.SelectedRows[0].Cells[key + "id"].Value, trcId, OBJECT_ID);
                    else
                        frm = new Frm_AddFile(dgv_Project_FileList, key, null, trcId, OBJECT_ID);
                    frm.txt_Unit.Text = UserHelper.GetUser().UnitName;
                    frm.parentId = objId;
                    frm.UpdateDataSource = LoadFileList;
                    frm.objectCode = txt_Project_Code.Text;
                    frm.Show();
                }
                else
                    XtraMessageBox.Show("请先保存基本信息。", "温馨提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            else if ("btn_Topic_AddFile".Equals(name))
            {
                key = "topic_fl_";
                object objId = tab_Topic_Info.Tag;
                if (objId != null)
                {
                    if (dgv_Topic_FileList.SelectedRows.Count == 1 && dgv_Topic_FileList.RowCount != 1)
                        frm = new Frm_AddFile(dgv_Topic_FileList, key, dgv_Topic_FileList.SelectedRows[0].Cells[key + "id"].Value, trcId, OBJECT_ID);
                    else
                        frm = new Frm_AddFile(dgv_Topic_FileList, key, null, trcId, OBJECT_ID);
                    frm.txt_Unit.Text = UserHelper.GetUser().UnitName;
                    frm.parentId = objId;
                    frm.UpdateDataSource = LoadFileList;
                    frm.objectCode = txt_Topic_Code.Text;
                    frm.Show();
                }
                else
                    XtraMessageBox.Show("请先保存基本信息。", "温馨提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            else if ("btn_Subject_AddFile".Equals(name))
            {
                key = "subject_fl_";
                object objId = tab_Subject_Info.Tag;
                if (objId != null)
                {
                    if (dgv_Subject_FileList.SelectedRows.Count == 1 && dgv_Subject_FileList.RowCount != 1)
                        frm = new Frm_AddFile(dgv_Subject_FileList, key, dgv_Subject_FileList.SelectedRows[0].Cells[key + "id"].Value, trcId, OBJECT_ID);
                    else
                        frm = new Frm_AddFile(dgv_Subject_FileList, key, null, trcId, OBJECT_ID);
                    frm.txt_Unit.Text = UserHelper.GetUser().UnitName;
                    frm.parentId = objId;
                    frm.UpdateDataSource = LoadFileList;
                    frm.objectCode = txt_Subject_Code.Text;
                    frm.Show();
                }
                else
                    XtraMessageBox.Show("请先保存基本信息。", "温馨提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            else if ("btn_Imp_AddFile".Equals(name))
            {
                key = "imp_fl_";
                object objId = tab_Imp_Info.Tag;
                if (objId != null)
                {
                    if (dgv_Imp_FileList.SelectedRows.Count == 1 && dgv_Imp_FileList.RowCount != 1)
                        frm = new Frm_AddFile(dgv_Imp_FileList, key, dgv_Imp_FileList.SelectedRows[0].Cells[key + "id"].Value, trcId, OBJECT_ID);
                    else
                        frm = new Frm_AddFile(dgv_Imp_FileList, key, null, trcId, OBJECT_ID);
                    frm.parentId = objId;
                    frm.txt_Unit.Text = UserHelper.GetUser().UnitName;
                    frm.UpdateDataSource = LoadFileList;
                    frm.Show();
                }
                else
                    XtraMessageBox.Show("请先保存基本信息。", "温馨提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            else if ("btn_Special_AddFile".Equals(name))
            {
                key = "special_fl_";
                //object objId = tab_Special_Info.Tag;
                object objId = cbo_Special_AJ_Code.SelectedValue;
                if (objId != null)
                {
                    if (dgv_Special_FileList.SelectedRows.Count == 1 && dgv_Special_FileList.RowCount != 1)
                        frm = new Frm_AddFile(dgv_Special_FileList, key, dgv_Special_FileList.SelectedRows[0].Cells[key + "id"].Value, trcId, OBJECT_ID);
                    else
                        frm = new Frm_AddFile(dgv_Special_FileList, key, null, trcId, OBJECT_ID);
                    frm.txt_Unit.Text = UserHelper.GetUser().UnitName;
                    frm.parentId = objId;
                    frm.UpdateDataSource = LoadFileList;
                    frm.Show();
                }
                else
                    XtraMessageBox.Show("请先添加档号信息。", "温馨提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }

        /// <summary>
        /// 上下移动
        /// </summary>
        private void Btn_TB_Click(object sender, EventArgs e)
        {
            string nameValue = (sender as KyoButton).Name;
            //计划
            if (nameValue.Contains("Plan"))
            {
                if (nameValue.Equals("btn_Plan_Box_Top"))
                    MoveListViewItem(lsv_JH_File2, true);
                else if (nameValue.Equals("btn_Plan_Box_Bottom"))
                    MoveListViewItem(lsv_JH_File2, false);
                SaveListSort(lsv_JH_File2, cbo_Plan_Box.SelectedValue);
            }
            //计划-项目
            if (nameValue.Contains("Project"))
            {
                if (nameValue.Equals("btn_Project_Box_Top"))
                    MoveListViewItem(lsv_JH_XM_File2, true);
                else if (nameValue.Equals("btn_Project_Box_Bottom"))
                    MoveListViewItem(lsv_JH_XM_File2, false);
                SaveListSort(lsv_JH_XM_File2, cbo_Project_Box.SelectedValue);
            }
            //计划-项目-课题-子课题
            if (nameValue.Contains("Subject"))
            {
                if (nameValue.Equals("btn_Subject_Box_Top"))
                    MoveListViewItem(lsv_JH_XM_KT_ZKT_File2, true);
                else if (nameValue.Equals("btn_Subject_Box_Bottom"))
                    MoveListViewItem(lsv_JH_XM_KT_ZKT_File2, false);
                SaveListSort(lsv_JH_XM_KT_ZKT_File2, cbo_Subject_Box.SelectedValue);
            }
            //计划-课题
            if (nameValue.Contains("Topic"))
            {
                if (nameValue.Equals("btn_Topic_Box_Top"))
                    MoveListViewItem(lsv_JH_KT_File2, true);
                else if (nameValue.Equals("btn_Topic_Box_Bottom"))
                    MoveListViewItem(lsv_JH_KT_File2, false);
                SaveListSort(lsv_JH_KT_File2, cbo_Topic_Box.SelectedValue);
            }
            //重大专项
            if (nameValue.Contains("Imp"))
            {
                if (nameValue.Equals("btn_Imp_Box_Top"))
                    MoveListViewItem(lsv_Imp_File2, true);
                else if (nameValue.Equals("btn_Imp_Box_Bottom"))
                    MoveListViewItem(lsv_Imp_File2, false);
                SaveListSort(lsv_Imp_File2, cbo_Imp_Box.SelectedValue);
            }
            //重大专项-信息
            if (nameValue.Contains("Special"))
            {
                if (nameValue.Equals("btn_Special_Box_Top"))
                    MoveListViewItem(lsv_Imp_Dev_File2, true);
                else if (nameValue.Equals("btn_Special_Box_Bottom"))
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
            for (int i = 0; i < listView.Items.Count; i++)
            {
                string fileId = listView.Items[i].SubItems[0].Text;
                builder.Append($"UPDATE processing_file_list SET pfl_box_sort='{i}' WHERE pfl_id='{fileId}';");
            }
            if (builder.Length > 0)
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
            if (isTop)
            {
                foreach (ListViewItem item in view.SelectedItems)
                {
                    int index = item.Index;
                    if (index > 0)
                    {
                        view.Items.RemoveAt(index);
                        view.Items.Insert(index - 1, item);
                    }
                }
            }
            else
            {
                int size = view.Items.Count - 1;
                for (int i = size; i >= 0; i--)
                {
                    ListViewItem item = view.Items[i];
                    if (item.Selected)
                    {
                        int index = item.Index;
                        if (index < size)
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
            if (Code != null)
                treeView.Tag = Code;
        }

        /// <summary>
        /// 质检意见
        /// </summary>
        private void Btn_QTReason_Click(object sender, EventArgs e)
        {
            string name = (sender as Control).Name;
            object objId = null, objName = null;
            if (name.Contains("Imp"))
            {
                objId = tab_Imp_Info.Tag;
                objName = lbl_Imp_Name.Text;
            }
            else if (name.Contains("Plan"))
            {
                objId = tab_Plan_Info.Tag;
                objName = lbl_Plan_Name.Text;
            }
            else if (name.Contains("Special"))
            {
                objId = tab_Special_Info.Tag;
                objName = txt_Special_Name.Text;
            }
            else if (name.Contains("Project"))
            {
                objId = tab_Project_Info.Tag;
                objName = txt_Project_Name.Text;
            }
            else if (name.Contains("Topic"))
            {
                objId = tab_Topic_Info.Tag;
                objName = txt_Topic_Name.Text;
            }
            else if (name.Contains("Subject"))
            {
                objId = tab_Subject_Info.Tag;
                objName = txt_Subject_Name.Text;
            }
            if (objId != null && objName != null)
            {
                Frm_AdviceBW frm = new Frm_AdviceBW(objId, objName);
                frm.Show();
            }
        }

        /// <summary>
        /// 历史存档单位
        /// </summary>
        object localPositon = null;

        private void DGV_FileList_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (isReadOnly) return;
            DataGridView view = sender as DataGridView;
            if (e.Button == MouseButtons.Right && e.RowIndex != -1 && e.ColumnIndex != -1)
            {
                view.ClearSelection();
                view.CurrentCell = view.Rows[e.RowIndex].Cells[e.ColumnIndex];
                contextMenuStrip1.Tag = view;
                bool vis = view.Equals(dgv_Plan_FileList) || view.Equals(dgv_Special_FileList);
                tsm_Up.Visible = !vis;
                tsm_Down.Visible = !vis;
                tsm_AutoSort.Visible = !vis;
                contextMenuStrip1.Show(MousePosition);
            }
            else if (e.Button == MouseButtons.Left)
            {
                if (e.RowIndex == -1 || e.ColumnIndex == -1 ||
                    !view.AllowUserToAddRows) return;
                object value = view.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                ///存放单位下拉自动填充
                if (view.Columns[e.ColumnIndex].Name.Contains("fl_unit")
                    && value != null)
                {
                    localPositon = value;
                }
                else
                    localPositon = null;
            }
        }

        private void Tsm_DeleteRow(object sender, EventArgs e)
        {
            DataGridView view = (DataGridView)(sender as ToolStripItem).GetCurrentParent().Tag;
            int index = view.CurrentCell.RowIndex;
            if (index != view.RowCount - 1)
            {
                object fileId = view.Rows[index].Cells[view.Tag + "id"].Value;
                removeIdList.Add(fileId);
                view.Rows.RemoveAt(index);
            }
        }

        private void Tsm_Refresh(object sender, EventArgs e)
        {
            DataGridView view = (DataGridView)(sender as ToolStripItem).GetCurrentParent().Tag;
            object objId = view.Parent.Parent.Tag;
            string name = view.Parent.Parent.Name;
            string key = null;
            if (name.Contains("Plan"))
            {
                key = "plan_fl_";
                objId = cbo_Plan_AJ_Code.SelectedValue;
            }
            else if (name.Contains("Project"))
                key = "project_fl_";
            else if (name.Contains("Topic"))
                key = "topic_fl_";
            else if (name.Contains("Subject"))
                key = "subject_fl_";
            else if (name.Contains("Imp"))
                key = "imp_fl_";
            else if (name.Contains("Special"))
            {
                key = "special_fl_";
                objId = cbo_Special_AJ_Code.SelectedValue;
            }
            if (key != null && objId != null)
                LoadFileList(view, objId, -1);

            removeIdList.Clear();
        }

        private void Tab_FileInfo_SelectedIndexChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            string name = (sender as Control).Name;
            if (name.Contains("Plan"))
            {
                int index = tab_Plan_Info.SelectedTabPageIndex;
                object objid = tab_Plan_Info.Tag;
                lbl_Plan_Tip.Visible = false;
                if (objid != null)
                {
                    objid = cbo_Plan_AJ_Code.SelectedValue;
                    if (objid != null)
                    {
                        if (index == 0)
                            lbl_Plan_Tip.Visible = true;
                        else if (index == 1)
                            LoadFileValidList(dgv_Plan_FileValid, objid, "plan_fc_");
                        else if (index == 2)
                        {
                            txt_Plan_AJ_Code_R.Text = cbo_Plan_AJ_Code.Text;
                            txt_Plan_AJ_Name_R.Text = txt_Plan_AJ_Name.Text;
                            LoadDocList(objid, ControlType.Plan);
                        }
                    }
                }
            }
            else if (name.Contains("Imp"))
            {
                int index = tab_Imp_Info.SelectedTabPageIndex;
                object objid = tab_Imp_Info.Tag;
                lbl_Imp_Tip.Visible = false;
                if (objid != null)
                {
                    if (index == 0)
                        lbl_Imp_Tip.Visible = true;
                    else if (index == 1)
                        LoadFileValidList(dgv_Imp_FileValid, objid, "imp_fc_");
                    else if (index == 2)
                        LoadDocList(objid, ControlType.Imp);
                }
            }
            else if (name.Contains("Special"))
            {
                int index = tab_Special_Info.SelectedTabPageIndex;
                object objid = tab_Special_Info.Tag;
                lbl_Special_Tip.Visible = false;
                if (objid != null)
                {
                    object parentID = cbo_Special_AJ_Code.SelectedValue;
                    if (index == 0)
                        lbl_Special_Tip.Visible = true;
                    if (index == 1)
                        LoadFileValidList(dgv_Special_FileValid, parentID, "special_fc_");
                    else if (index == 2)
                    {
                        txt_Special_AJ_Code_R.Text = cbo_Special_AJ_Code.Text;
                        txt_Special_AJ_Name_R.Text = txt_Special_AJ_Name.Text;
                        LoadDocList(parentID, ControlType.Special);
                    }
                }
            }
            else if (name.Contains("Project"))
            {
                int index = tab_Project_Info.SelectedTabPageIndex;
                object objid = tab_Project_Info.Tag;
                lbl_Project_Tip.Visible = false;
                if (objid != null)
                {
                    if (index == 0)
                        lbl_Project_Tip.Visible = true;
                    if (index == 1)
                        LoadFileValidList(dgv_Project_FileValid, objid, "project_fc_");
                    else if (index == 2)
                        LoadDocList(objid, ControlType.Project);
                }
            }
            else if (name.Contains("Topic"))
            {
                int index = tab_Topic_Info.SelectedTabPageIndex;
                object objid = tab_Topic_Info.Tag;
                lbl_Topic_Tip.Visible = false;
                if (objid != null)
                {
                    if (index == 0)
                        lbl_Topic_Tip.Visible = true;
                    if (index == 1)
                        LoadFileValidList(dgv_Topic_FileValid, objid, "topic_fc_");
                    else if (index == 2)
                        LoadDocList(objid, ControlType.Topic);
                }
            }
            else if (name.Contains("Subject"))
            {
                int index = tab_Subject_Info.SelectedTabPageIndex;
                object objId = tab_Subject_Info.Tag;
                lbl_Subject_Tip.Visible = false;
                if (objId != null)
                {
                    if (index == 0)
                        lbl_Subject_Tip.Visible = true;
                    if (index == 1)
                        LoadFileValidList(dgv_Subject_FileValid, objId, "subject_fc_");
                    else if (index == 2)
                        LoadDocList(objId, ControlType.Subject);
                }
            }
        }

        private void LoadDocList(object objid, ControlType type)
        {
            LoadBoxList(objid, type);
        }

        private void FileList_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            string name = (sender as DataGridView).Name;
            if (name.Contains("Plan"))
                Btn_AddFile_Click(btn_Plan_AddFile, e);
            else if (name.Contains("Project"))
                Btn_AddFile_Click(btn_Project_AddFile, e);
            else if (name.Contains("Topic"))
                Btn_AddFile_Click(btn_Topic_AddFile, e);
            else if (name.Contains("Subject"))
                Btn_AddFile_Click(btn_Subject_AddFile, e);
            else if (name.Contains("Imp"))
                Btn_AddFile_Click(btn_Imp_AddFile, e);
            else if (name.Contains("Special"))
                Btn_AddFile_Click(btn_Special_AddFile, e);
        }

        private void cbo_Project_HasNext_SelectionChangeCommitted(object sender, EventArgs e)
        {
            System.Windows.Forms.ComboBox comboBox = sender as System.Windows.Forms.ComboBox;
            object id = tab_Project_Info.Tag;
            if (id == null)
            {
                XtraMessageBox.Show("尚未保存当前项目，无法添加新数据。", "温馨提示");
                comboBox.SelectedIndex = 0;
            }
            else
            {
                int _index = tab_MenuList.SelectedIndex + 1;
                int index = comboBox.SelectedIndex;
                if (index == 0)//无
                {
                    ShowTab(null, _index);
                    topic.Tag = null;
                }
                else if (index == 1)
                {
                    ShowTab("topic", _index);
                    ResetControls(ControlType.Topic);
                    topic.Tag = id;
                }
                tab_MenuList.SelectedIndex = _index;
            }
        }

        private void Btn_Print_Click(object sender, EventArgs e)
        {
            string controlName = (sender as KyoButton).Name;
            object objId = null;
            string proName = null, proCode = null;
            object parentObjectName = null;
            if (controlName.Contains("Project"))
            {
                objId = tab_Project_Info.Tag;
                proName = txt_Project_Name.Text;
                proCode = txt_Project_Code.Text;
            }
            else if (controlName.Contains("Topic"))
            {
                objId = tab_Topic_Info.Tag;
                proName = txt_Topic_Name.Text;
                proCode = txt_Topic_Code.Text;
                parentObjectName = SqlHelper.ExecuteOnlyOneQuery($"SELECT pi_name FROM project_info WHERE pi_id=(SELECT ti_obj_id FROM topic_info WHERE ti_id='{objId}') AND pi_categor=2");
            }
            else if (controlName.Contains("Subject"))
            {
                objId = tab_Subject_Info.Tag;
                proName = txt_Subject_Name.Text;
                proCode = txt_Subject_Code.Text;
                parentObjectName = SqlHelper.ExecuteOnlyOneQuery($"SELECT ti_name FROM topic_info WHERE ti_id=(SELECT si_obj_id FROM subject_info WHERE si_id='{objId}')");
            }
            else if (controlName.Contains("Special"))
            {
                objId = cbo_Special_AJ_Code.SelectedValue;
                proName = txt_Special_Name.Text;
                proCode = txt_Special_Code.Text;
            }
            else if (controlName.Contains("Imp"))
            {
                objId = tab_Imp_Info.Tag;
                proName = lbl_Imp_Name.Text;
            }
            else if (controlName.Contains("Plan"))
            {
                objId = cbo_Plan_AJ_Code.SelectedValue;
                proName = lbl_Plan_Name.Text;
            }

            Frm_PrintBox frm = new Frm_PrintBox(objId, this)
            {
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

        private void Code_Leave(object sender, EventArgs e)
        {
            CheckCodeHasExist(sender);
        }

        private bool CheckCodeHasExist(object sender)
        {
            bool result = false;
            TextEdit codeText = sender as TextEdit;
            string value = codeText.Text.Trim();
            if (!string.IsNullOrEmpty(value) && GetSaveState(codeText.Name))
            {
                object pId = codeText.Parent.Parent.Tag;
                int count = SqlHelper.ExecuteCountQuery("SELECT COUNT(pi_id) FROM " +
                        $"(SELECT pi_id, pi_code, pi_orga_id FROM project_info " +
                        $"UNION ALL SELECT ti_id, ti_code, ti_orga_id FROM topic_info " +
                        $"UNION ALL SELECT si_id, si_code, si_orga_id FROM subject_info) A " +
                        $"WHERE A.pi_code='{value}' AND A.pi_orga_id='{togle.Tag}';");
                if (count > 0)
                {
                    errorProvider1.SetError(codeText, "提示：此编号已存在。");
                    codeText.Focus();
                    result = true;
                }
                else
                    errorProvider1.SetError(codeText, null);
            }
            return result;
        }

        private bool GetSaveState(string name)
        {
            bool flag = true;
            if (name.Contains("Project"))
            {
                if (tab_Project_Info.Tag != null)
                    flag = false;
            }
            else if (name.Contains("Topic"))
            {
                if (tab_Topic_Info.Tag != null)
                    flag = false;
            }
            else if (name.Contains("Subject"))
            {
                if (tab_Subject_Info.Tag != null)
                    flag = false;
            }
            return flag;
        }

        private void Dtp_Project_StartTime(object sender, EventArgs e)
        {
            DateTimePicker picker = sender as DateTimePicker;
            if (picker.Name.Contains("Project"))
                txt_Project_StartTime.Text = picker.Value.ToString("yyyy-MM-dd");
            else if (picker.Name.Contains("Topic"))
                txt_Topic_StartTime.Text = picker.Value.ToString("yyyy-MM-dd");
            else if (picker.Name.Contains("Subject"))
                txt_Subject_StartTime.Text = picker.Value.ToString("yyyy-MM-dd");
        }

        private void Dtp_Project_EndTime(object sender, EventArgs e)
        {
            DateTimePicker picker = sender as DateTimePicker;
            if (picker.Name.Contains("Project"))
                txt_Project_EndTime.Text = picker.Value.ToString("yyyy-MM-dd");
            else if (picker.Name.Contains("Topic"))
                txt_Topic_EndTime.Text = picker.Value.ToString("yyyy-MM-dd");
            else if (picker.Name.Contains("Subject"))
                txt_Subject_EndTime.Text = picker.Value.ToString("yyyy-MM-dd");
        }

        private void Togle_Toggled(object sender, EventArgs e)
        {
            Pal_LeftBar.Visible = togle.IsOn ? true : false;
        }

        /// <summary>
        /// 检测是否存在重复的文件名
        /// </summary>
        private bool CheckFileName(DataGridViewRow row, string key)
        {
            bool result = true;
            DataGridViewCell cellName = row.Cells[key + "name"];
            if (cellName.Value == null || string.IsNullOrEmpty(ToolHelper.GetValue(cellName.Value).Trim()))
            {
                cellName.ErrorText = "温馨提示：文件名不能为空。";
                result = false;
            }
            else
            {
                cellName.ErrorText = null;
                for (int j = 0; j < row.Index; j++)
                {
                    DataGridViewCell cell2 = row.DataGridView.Rows[j].Cells[key + "name"];
                    if (cellName.Value.Equals(cell2.Value))
                    {
                        cellName.ErrorText = $"温馨提示：与行{j + 1}的文件名重复。";
                        result = false;
                    }
                    else
                        cellName.ErrorText = null;
                }
            }

            //检测文件编号重复
            DataGridViewCell cellCode = row.Cells[key + "code"];
            if (cellCode.Value == null || string.IsNullOrEmpty(ToolHelper.GetValue(cellCode.Value).Trim()))
            {
                cellCode.ErrorText = "温馨提示：文件编号不能为空。";
                result = false;
            }
            else
            {
                cellCode.ErrorText = null;
                for (int j = 0; j < row.Index; j++)
                {
                    DataGridViewCell cell2 = row.DataGridView.Rows[j].Cells[key + "code"];
                    if (cellCode.Value.Equals(cell2.Value))
                    {
                        cellCode.ErrorText = $"温馨提示：与行{j + 1}的文件编号重复。";
                        result = false;
                    }
                    else
                        cellCode.ErrorText = null;
                }
            }

            //DataGridViewCell pagesCell = row.Cells[key + "pages"];
            //if (pagesCell.Value == null)
            //{
            //    pagesCell.ErrorText = "温馨提示：页数不能为0或空。";
            //    result = false;
            //}
            //else
            //{
            //    if (!Regex.IsMatch(ToolHelper.GetValue(pagesCell.Value), "^[0-9]{1,4}$"))
            //    {
            //        pagesCell.ErrorText = "温馨提示：请输入小于4位数的合法数字。";
            //        result = false;
            //    }
            //    else
            //        pagesCell.ErrorText = null;
            //}

            bool isOtherType = "其他".Equals(ToolHelper.GetValue(row.Cells[key + "categor"].FormattedValue).Trim());
            DataGridViewCell cellCategor = row.Cells[key + "categorname"];
            if (isOtherType)
            {
                if (cellCategor.Value == null || string.IsNullOrEmpty(ToolHelper.GetValue(cellCategor.Value).Trim()))
                {
                    cellCategor.ErrorText = "温馨提示：类型名称不能为空。";
                    result = false;
                }
                else
                    cellCategor.ErrorText = null;

                //文件类别是否已存在
                string codeParam = ToolHelper.GetValue(cellCode.Value);
                if (string.IsNullOrEmpty(cellCode.ErrorText) && !string.IsNullOrEmpty(codeParam))
                {
                    codeParam = codeParam.Split('-')[0];
                    int index = SqlHelper.ExecuteCountQuery($"SELECT COUNT(dd_id) FROM data_dictionary WHERE dd_name = '{codeParam}'");
                    if (index > 0)
                    {
                        cellCode.ErrorText = "提示：文件类别已存在。";
                        result = false;
                    }
                    else
                        cellCode.ErrorText = null;
                }
            }
            else
                cellCategor.ErrorText = null;

            DataGridViewCell dateCell = row.Cells[key + "date"];
            if (!string.IsNullOrEmpty(ToolHelper.GetValue(dateCell.Value)))
            {
                if (!Regex.IsMatch(ToolHelper.GetValue(dateCell.Value), "\\d{4}-\\d{2}-\\d{2}"))
                {
                    dateCell.ErrorText = "提示：请输入格式为 yyyy-MM-dd 的有效日期。";
                    result = false;
                }
                else
                {
                    bool flag = DateTime.TryParse(ToolHelper.GetValue(dateCell.Value), out DateTime date);
                    if (!flag)
                    {
                        dateCell.ErrorText = "提示：请输入格式为 yyyy-MM-dd 的有效日期。";
                        result = false;
                    }
                    else
                        dateCell.ErrorText = null;
                }
            }

            return result;
        }

        private void FileList_UserAddedRow(object sender, DataGridViewRowEventArgs e)
        {
            DataGridView view = sender as DataGridView;
            view.Rows[e.Row.Index - 1].Cells[view.Tag + "amount"].Value = 1;
            new Thread(delegate ()
            {
                int lastRowIndex = e.Row.Index - 1;
                if (lastRowIndex > 0)//当前行不能是第一行
                {
                    DataGridViewRow row = view.Rows[lastRowIndex - 1];
                    if (row.Cells[view.Tag + "id"].Tag == null)//当前行不能使修改（只能新增）
                    {
                        string key = null;
                        object pId = null;
                        if (view.Name.Contains("Plan"))
                        { key = "plan_fl_"; pId = cbo_Plan_AJ_Code.SelectedValue; }
                        else if (view.Name.Contains("Project"))
                        { key = "project_fl_"; pId = tab_Project_Info.Tag; }
                        else if (view.Name.Contains("Topic"))
                        { key = "topic_fl_"; pId = tab_Topic_Info.Tag; }
                        else if (view.Name.Contains("Subject"))
                        { key = "subject_FL_"; pId = tab_Subject_Info.Tag; }
                        else if (view.Name.Contains("Imp"))
                        { key = "imp_fl_"; pId = tab_Imp_Info.Tag; }
                        else if (view.Name.Contains("Special"))
                        { key = "special_fl_"; pId = cbo_Special_AJ_Code.SelectedValue; }

                        if (pId != null && CheckFileName(row, key) && pId != null)
                        {
                            row.Cells[$"{key}id"].Value = AddFileInfo(key, row, pId, row.Index);
                        }
                    }
                }
                Thread.CurrentThread.Abort();
            }).Start();
        }

        private void BtnGroup_EnabledChanged(object sender, EventArgs e)
        {
            Panel panel = sender as Panel;
            if (!isReadOnly)
            {
                if (panel.Name.Contains("Plan"))
                {
                    dgv_Plan_FileList.AllowUserToAddRows = panel.Enabled;
                    lbl_Plan_Box_Add.Enabled = panel.Enabled;
                    lbl_Plan_Box_Remove.Enabled = panel.Enabled;
                    pal_Plan_MoveBtnGroup.Enabled = panel.Enabled;
                    btn_Plan_SaveDoc.Enabled = panel.Enabled;
                    if (!panel.Enabled)
                        dgv_Plan_FileList.RowHeaderMouseDoubleClick -= FileList_RowHeaderMouseDoubleClick;
                    else
                        dgv_Plan_FileList.RowHeaderMouseDoubleClick += FileList_RowHeaderMouseDoubleClick;
                }
                else if (panel.Name.Contains("Project"))
                {
                    dgv_Project_FileList.AllowUserToAddRows = panel.Enabled;
                    lbl_Project_Box_Add.Enabled = panel.Enabled;
                    lbl_Project_Box_Remove.Enabled = panel.Enabled;
                    pal_Project_MoveBtnGroup.Enabled = panel.Enabled;
                    btn_Project_OtherDoc.Enabled = panel.Enabled;
                    //可以继承别人做的项目
                    cbo_Project_HasNext.Enabled = panel.Enabled;
                    if (!panel.Enabled)
                        dgv_Project_FileList.RowHeaderMouseDoubleClick -= FileList_RowHeaderMouseDoubleClick;
                    else
                        dgv_Project_FileList.RowHeaderMouseDoubleClick += FileList_RowHeaderMouseDoubleClick;
                }
                else if (panel.Name.Contains("Topic"))
                {
                    dgv_Topic_FileList.AllowUserToAddRows = panel.Enabled;
                    lbl_Topic_Box_Add.Enabled = panel.Enabled;
                    lbl_Topic_Box_Remove.Enabled = panel.Enabled;
                    pal_Topic_MoveBtnGroup.Enabled = panel.Enabled;
                    cbo_Topic_HasNext.Enabled = panel.Enabled;
                    btn_Topic_OtherDoc.Enabled = panel.Enabled;
                    if (!panel.Enabled)
                        dgv_Topic_FileList.RowHeaderMouseDoubleClick -= FileList_RowHeaderMouseDoubleClick;
                    else
                        dgv_Topic_FileList.RowHeaderMouseDoubleClick += FileList_RowHeaderMouseDoubleClick;
                }
                else if (panel.Name.Contains("Subject"))
                {
                    dgv_Subject_FileList.AllowUserToAddRows = panel.Enabled;
                    lbl_Subject_Box_Add.Enabled = panel.Enabled;
                    lbl_Subject_Box_Remove.Enabled = panel.Enabled;
                    pal_Subject_MoveBtnGroup.Enabled = panel.Enabled;
                    btn_Subject_OtherDoc.Enabled = panel.Enabled;
                    if (!panel.Enabled)
                        dgv_Subject_FileList.RowHeaderMouseDoubleClick -= FileList_RowHeaderMouseDoubleClick;
                    else
                        dgv_Subject_FileList.RowHeaderMouseDoubleClick += FileList_RowHeaderMouseDoubleClick;
                }
                else if (panel.Name.Contains("Imp"))
                {
                    dgv_Imp_FileList.AllowUserToAddRows = panel.Enabled;
                    lbl_Imp_Box_Add.Enabled = panel.Enabled;
                    lbl_Imp_Box_Remove.Enabled = panel.Enabled;
                    pal_Imp_MoveBtnGroup.Enabled = panel.Enabled;
                    cbo_Imp_HasNext.Enabled = panel.Enabled;
                    if (!panel.Enabled)
                        dgv_Imp_FileList.RowHeaderMouseDoubleClick -= FileList_RowHeaderMouseDoubleClick;
                    else
                        dgv_Imp_FileList.RowHeaderMouseDoubleClick += FileList_RowHeaderMouseDoubleClick;
                }
                else if (panel.Name.Contains("Special"))
                {
                    dgv_Special_FileList.AllowUserToAddRows = panel.Enabled;
                    lbl_Special_Box_Add.Enabled = panel.Enabled;
                    lbl_Special_Box_Remove.Enabled = panel.Enabled;
                    pal_Special_MoveBtnGroup.Enabled = panel.Enabled;
                    btn_Special_OtherDoc.Enabled = panel.Enabled;
                    btn_Speical_SaveDoc.Enabled = panel.Enabled;
                    if (!panel.Enabled)
                        dgv_Special_FileList.RowHeaderMouseDoubleClick -= FileList_RowHeaderMouseDoubleClick;
                    else
                        dgv_Special_FileList.RowHeaderMouseDoubleClick += FileList_RowHeaderMouseDoubleClick;
                }
            }
            else
            {
                panel.EnabledChanged -= BtnGroup_EnabledChanged;
                panel.Enabled = false;
                panel.EnabledChanged += BtnGroup_EnabledChanged;
            }
        }

        private void SearchText_KeyDown(object sender, KeyEventArgs e)
        {
            if (treeView.Nodes.Count > 0 && e.KeyCode == Keys.Enter)
            {
                string key = SearchText.Text;
                if (!string.IsNullOrEmpty(key))
                {
                    TreeNode node = GetTreeNodeByKey(treeView.Nodes[0], key);
                    if (node == null)
                    {
                        MessageBox.Show("找不到相关数据。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        SearchKeyIndex = string.Empty;
                    }
                    else
                    {
                        if (treeView.SelectedNode != null)
                            treeView.SelectedNode.BackColor = Color.Transparent;
                        node.Parent.Expand();
                        treeView.SelectedNode = node;
                        node.EnsureVisible();
                        treeView.SelectedNode.BackColor = Color.DarkGray;
                    }
                }
            }
            else
                SearchKeyIndex = string.Empty;
        }

        private TreeNode GetTreeNodeByKey(TreeNode node, string key)
        {
            foreach (TreeNode item in node.Nodes)
            {
                if (item.Text.Contains(key))
                {
                    if (string.Compare(item.FullPath, SearchKeyIndex) > 0)
                    {
                        SearchKeyIndex = item.FullPath;
                        return item;
                    }
                }
                TreeNode treeNode = GetTreeNodeByKey(item, key);
                if (treeNode != null)
                    return treeNode;
            }
            return null;
        }

        private void Btn_OtherDoc_Click(object sender, EventArgs e)
        {
            string name = (sender as KyoButton).Name;
            object objid = null;
            if (name.Contains("Plan"))
                objid = tab_Plan_Info.Tag;
            else if (name.Contains("Project"))
                objid = tab_Project_Info.Tag;
            else if (name.Contains("Topic"))
                objid = tab_Topic_Info.Tag;
            else if (name.Contains("Subject"))
                objid = tab_Subject_Info.Tag;
            else if (name.Contains("Imp"))
                objid = tab_Imp_Info.Tag;
            else if (name.Contains("Special"))
                objid = tab_Special_Info.Tag;
            if (objid != null)
            {
                Frm_OtherDoc frm = GetFormHelper.GetOtherDoc(objid);
                frm.Show();
                frm.Activate();
            }
            else
                XtraMessageBox.Show("请先保存基本信息。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }

        private void Frm_MyWork_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Close();
            }
            else if (e.Control && e.KeyCode == Keys.Q)
            {
                if (ActiveControl != null && ActiveControl is TextBox)
                {
                    Frm_SpecialSymbol specialSymbol = new Frm_SpecialSymbol(ActiveControl as TextBox);
                    specialSymbol.ShowDialog();
                }
            }
        }

        private void cbo_Imp_HasNext_EnabledChanged(object sender, EventArgs e)
        {
            cbo_Imp_HasNext.Visible = cbo_Imp_HasNext.Enabled;
            lbl_SpeName.Visible = cbo_Imp_HasNext.Enabled;
        }

        private void FileList_DataSourceChanged(object sender, EventArgs e)
        {
            DataGridView view = sender as DataGridView;
            object key = view.Tag;
            foreach (DataGridViewRow row in view.Rows)
            {
                object id = row.Cells[key + "stage"].Value;
                if (id != null)
                {
                    SetCategorByStage(id, row, key);
                }
            }
        }

        private void Tsm_Click(object sender, EventArgs e)
        {
            DataGridView view = (DataGridView)(sender as ToolStripItem).GetCurrentParent().Tag;
            string name = (sender as ToolStripMenuItem).Name;
            int index = view.CurrentRow.Index;
            object currentRowId = view.Rows[index].Cells[view.Tag + "id"].Value;
            if (currentRowId != null)
            {
                if (name.Contains("Up"))
                {
                    if (index != 0)
                    {
                        object lastRowId = view.Rows[index - 1].Cells[view.Tag + "id"].Value;
                        ChangeFileSort(currentRowId, index - 1, lastRowId, index);
                    }
                    LoadFileList(view, view.Parent.Parent.Tag, index - 1);
                }
                else if (name.Contains("Down"))
                {
                    if (index != view.RowCount - 1)
                    {
                        object nextRowId = view.Rows[index + 1].Cells[view.Tag + "id"].Value;
                        ChangeFileSort(currentRowId, index + 1, nextRowId, index);
                    }
                    LoadFileList(view, view.Parent.Parent.Tag, index + 1);
                }
            }
        }

        /// <summary>
        /// 交换文件顺序
        /// </summary>
        private void ChangeFileSort(object currentRowId, int i, object lastRowId, int j)
        {
            string updateSQL =
                $"UPDATE processing_file_list SET pfl_sort='{i}' WHERE pfl_id='{currentRowId}';" +
                $"UPDATE processing_file_list SET pfl_sort='{j}' WHERE pfl_id='{lastRowId}';";
            SqlHelper.ExecuteNonQuery(updateSQL);
        }

        private void FileList_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            DataGridView view = (sender as DataGridView);
            removeIdList.Add(e.Row.Cells[view.Tag + "id"].Value);
        }

        private void Tsm_InsertRow(object sender, EventArgs e)
        {
            DataGridView view = (DataGridView)(sender as ToolStripItem).GetCurrentParent().Tag;
            int rowIndex = view.CurrentRow.Index;
            DataTable table = (DataTable)view.DataSource;
            table.Rows.InsertAt(table.NewRow(), rowIndex);
        }

        private void btn_SubmitAll_Click(object sender, EventArgs e)
        {
            if (treeView.Nodes.Count > 0)
            {
                List<object> objectID = new List<object>();
                GetObjectIdOfNonSubmit(treeView.Nodes[0], objectID);
                if (objectID.Count == 0)
                {
                    XtraMessageBox.Show("当前尚无可提交项目/课题。", "提示", MessageBoxButtons.OK);
                }
                else
                {
                    string queryString = $"当前共计{objectID.Count}条待提交的数据，是否全部提交？";
                    if (XtraMessageBox.Show(queryString, "确认提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk) == DialogResult.OK)
                    {
                        string ids = ToolHelper.GetStringBySplit(objectID.ToArray(), ",", "'");
                        if (UserHelper.GetUserRole() == UserRole.DocManager)
                        {
                            string updateSql =
                               $"UPDATE imp_info SET imp_submit_status=2 WHERE imp_id IN({ids});" +
                               $"UPDATE imp_dev_info SET imp_submit_status=2 WHERE imp_id IN({ids});" +
                               $"UPDATE project_info SET pi_submit_status=2 WHERE pi_id IN({ids});" +
                               $"UPDATE topic_info SET ti_submit_status=2 WHERE ti_id IN({ids});" +
                               $"UPDATE subject_info SET si_submit_status=2 WHERE si_id IN({ids});";
                            SqlHelper.ExecuteNonQuery(updateSql);
                        }
                        else
                        {
                            string updateSql =
                                $"UPDATE imp_info SET imp_submit_status=2 WHERE imp_id IN({ids}) AND imp_source_id='{UserHelper.GetUser().UserKey}';" +
                                $"UPDATE imp_dev_info SET imp_submit_status=2 WHERE imp_id IN({ids}) AND imp_source_id='{UserHelper.GetUser().UserKey}';" +
                                $"UPDATE project_info SET pi_submit_status=2 WHERE pi_id IN({ids}) AND pi_worker_id='{UserHelper.GetUser().UserKey}';" +
                                $"UPDATE topic_info SET ti_submit_status=2 WHERE ti_id IN({ids}) AND ti_worker_id='{UserHelper.GetUser().UserKey}';" +
                                $"UPDATE subject_info SET si_submit_status=2 WHERE si_id IN({ids}) AND si_worker_id='{UserHelper.GetUser().UserKey}';";
                            SqlHelper.ExecuteNonQuery(updateSql);
                        }
                        XtraMessageBox.Show("全部提交完毕入。", "提示", MessageBoxButtons.OK);
                        Close();
                    }
                }
            }
        }

        /// <summary>
        /// 获取当前角色的未提交数据ID
        /// </summary>
        private void GetObjectIdOfNonSubmit(TreeNode treeNode, List<object> objectID)
        {
            //前景色为灰色的直接跳过
            if (treeNode.ForeColor != DisEnbleColor)
            {
                if (UserHelper.GetUserRole() == UserRole.DocManager)
                    objectID.Add(treeNode.Name);
                else
                {
                    object userKey = UserHelper.GetUser().UserKey;
                    ControlType type = (ControlType)treeNode.Tag;
                    int i = 0;
                    if (type == ControlType.Project || type == ControlType.Plan)
                    {
                        i = SqlHelper.ExecuteCountQuery($"SELECT COUNT(pi_id) FROM project_info WHERE pi_worker_id='{userKey}' AND pi_id='{treeNode.Name}'");
                        if (i == 0)
                            i = SqlHelper.ExecuteCountQuery($"SELECT COUNT(ti_id) FROM topic_info WHERE ti_worker_id='{userKey}' AND ti_id='{treeNode.Name}'");
                    }
                    else if (type == ControlType.Topic)
                    {
                        i = SqlHelper.ExecuteCountQuery($"SELECT COUNT(ti_id) FROM topic_info WHERE ti_worker_id='{userKey}' AND ti_id='{treeNode.Name}'");
                        if (i == 0)
                            i = SqlHelper.ExecuteCountQuery($"SELECT COUNT(si_id) FROM subject_info WHERE si_worker_id='{userKey}' AND si_id='{treeNode.Name}'");
                    }
                    else if (type == ControlType.Subject)
                        i = SqlHelper.ExecuteCountQuery($"SELECT COUNT(si_id) FROM subject_info WHERE si_worker_id='{userKey}' AND si_id='{treeNode.Name}'");
                    else if (type == ControlType.Imp)
                        i = SqlHelper.ExecuteCountQuery($"SELECT COUNT(imp_id) FROM imp_info WHERE imp_source_id='{userKey}' AND imp_id='{treeNode.Name}'");
                    else if (type == ControlType.Special)
                        i = SqlHelper.ExecuteCountQuery($"SELECT COUNT(imp_id) FROM imp_dev_info WHERE imp_source_id='{userKey}' AND imp_id='{treeNode.Name}'");
                    if (i > 0)
                        objectID.Add(treeNode.Name);
                }
            }
            foreach (TreeNode node in treeNode.Nodes)
            {
                GetObjectIdOfNonSubmit(node, objectID);
            }
        }

        private void 自动排序AToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataGridView view = (DataGridView)(sender as ToolStripItem).GetCurrentParent().Tag;
            object parentID = view.Parent.Parent.Tag;
            if (parentID != null)
            {
                try
                {
                    SplashScreenManager.ShowDefaultWaitForm(this, false, false);
                    StringBuilder stringBuilder = new StringBuilder();
                    string queryTypeCode = $"SELECT SUBSTRING(pfl_code, 0,CHARINDEX('-',pfl_code)) FROM processing_file_list WHERE pfl_obj_id = '{parentID}' " +
                        "GROUP BY SUBSTRING(pfl_code, 0, CHARINDEX('-', pfl_code))";
                    object[] typeCodeList = SqlHelper.ExecuteSingleColumnQuery(queryTypeCode);
                    foreach (object typeCode in typeCodeList)
                    {
                        string orderTable = "SELECT ROW_NUMBER() OVER(ORDER BY CASE WHEN LEN(pfl_date)=0 THEN 1 ELSE 0 END ASC, pfl_date, pfl_id) ID, pfl_id FROM processing_file_list " +
                           $"WHERE pfl_obj_id = '{parentID}' AND SUBSTRING(pfl_code, 0,CHARINDEX('-', pfl_code) )= '{typeCode}'";
                        DataTable newTable = SqlHelper.ExecuteQuery(orderTable);
                        foreach (DataRow row in newTable.Rows)
                        {
                            string codeNumber = ToolHelper.GetValue(row["ID"]);
                            string newCode = typeCode + "-" + codeNumber.PadLeft(2, '0');
                            stringBuilder.Append($"UPDATE processing_file_list SET pfl_code='{newCode}' WHERE pfl_id='{row["pfl_id"]}';");
                        }
                    }

                    SqlHelper.ExecuteNonQuery(stringBuilder.ToString());
                    bool result = AutoSortFileByPID(parentID);
                    if (result)
                        LoadFileList(view, parentID, -1);
                }
                catch (Exception ex)
                {
                    LogsHelper.AddErrorLogs("文件列表自动排序失败", "失败原因：" + ex.Message);
                }
                finally
                {
                    SplashScreenManager.CloseDefaultWaitForm();
                }
            }
        }

        private void Cbo_Code_SelectionChangeCommitted(object sender, EventArgs e)
        {
            System.Windows.Forms.ComboBox view = sender as System.Windows.Forms.ComboBox;
            object docID = view.SelectedValue;
            if (docID != null)
            {
                object ptName = SqlHelper.ExecuteOnlyOneQuery($"SELECT pt_name FROM processing_tag WHERE pt_id='{docID}'");
                txt_Plan_AJ_Name.Text = ToolHelper.GetValue(ptName);
                LoadFileList(dgv_Plan_FileList, docID, -1);
                if (tab_Plan_Info.SelectedTabPageIndex == 2)
                    Tab_FileInfo_SelectedIndexChanged(tab_Plan_Info, null);
            }
        }

        private void Btn_Plan_SaveDoc_Click(object sender, EventArgs e)
        {
            object planID = tab_Plan_Info.Tag;
            if (planID != null)
            {
                if (XtraMessageBox.Show("确定要保存吗？", "确认提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string primaryKey = Guid.NewGuid().ToString();
                    string docCode = cbo_Plan_AJ_Code.Text;
                    string docName = txt_Plan_AJ_Name.Text;
                    string insertSQL = "INSERT INTO processing_tag(pt_id, pt_code, pt_name, pt_obj_id) VALUES " +
                        $"('{primaryKey}', '{docCode}', '{docName}', '{planID}');";
                    SqlHelper.ExecuteNonQuery(insertSQL);
                    LoadDocumentList(planID, ControlType.Plan);
                }
            }
            else
            {
                XtraMessageBox.Show("请先保存计划基础信息");
            }
        }

        private void Cbo_Code_SelectedIndexChanged(object sender, EventArgs e)
        {
            System.Windows.Forms.ComboBox view = sender as System.Windows.Forms.ComboBox;
            object docID = view.SelectedValue;
            if (docID != null)
            {
                object ptName = SqlHelper.ExecuteOnlyOneQuery($"SELECT pt_name FROM processing_tag WHERE pt_id='{docID}'");
                txt_Special_AJ_Name.Text = ToolHelper.GetValue(ptName);
                LoadFileList(dgv_Special_FileList, docID, -1);
                if (tab_Special_Info.SelectedTabPageIndex == 2)
                    Tab_FileInfo_SelectedIndexChanged(tab_Special_Info, null);
            }
        }

        private void Btn_Speical_SaveDoc_Click(object sender, EventArgs e)
        {
            object planID = tab_Special_Info.Tag;
            if (planID != null)
            {
                if (XtraMessageBox.Show("确定要保存吗？", "确认提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string primaryKey = Guid.NewGuid().ToString();
                    string docCode = cbo_Special_AJ_Code.Text;
                    string docName = txt_Special_AJ_Name.Text;
                    string insertSQL = "INSERT INTO processing_tag(pt_id, pt_code, pt_name, pt_obj_id) VALUES " +
                        $"('{primaryKey}', '{docCode}', '{docName}', '{planID}');";
                    SqlHelper.ExecuteNonQuery(insertSQL);
                    LoadDocumentList(planID, ControlType.Special);
                }
            }
            else
            {
                XtraMessageBox.Show("请先保存计划基础信息");
            }
        }

        /// <summary>
        /// 文件编号列头点击排序事件
        /// </summary>
        private void FileList_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridView view = sender as DataGridView;
            DataTable table = (DataTable)view.DataSource;
            string columnName = view.Columns[e.ColumnIndex].Name;
            if (columnName.Contains("fl_code"))
            {
                SplashScreenManager.ShowDefaultWaitForm(this, false, false);
                object parentID = view.Parent.Parent.Tag;
                if (view.Name.Contains("Plan"))
                    parentID = cbo_Plan_AJ_Code.SelectedValue;
                else if (view.Name.Contains("Special"))
                    parentID = cbo_Special_AJ_Code.SelectedValue;
                if (parentID != null)
                {
                    bool result = AutoSortFileByPID(parentID);
                    if (result)
                        LoadFileList(view, parentID, -1);
                }
                SplashScreenManager.CloseDefaultWaitForm();
            }
        }

        private bool AutoSortFileByPID(object parentID)
        {
            try
            {
                string querySql = $"SELECT pfl_id FROM processing_file_list WHERE pfl_obj_id = '{parentID}' " +
                    "ORDER BY SUBSTRING(pfl_code, 0, CHARINDEX('-', pfl_code)), TRY_PARSE(SUBSTRING(pfl_code, CHARINDEX('-', pfl_code) + 1, LEN(pfl_code)) AS int)";
                DataTable newTable = SqlHelper.ExecuteQuery(querySql);
                if (newTable.Rows.Count > 0)
                {
                    StringBuilder updateSQL = new StringBuilder();
                    for (int i = 0; i < newTable.Rows.Count; i++)
                    {
                        updateSQL.Append($"UPDATE processing_file_list SET pfl_sort={i} WHERE pfl_id='{newTable.Rows[i][0]}';");
                    }
                    SqlHelper.ExecuteNonQuery(updateSQL.ToString());
                }
                return true;
            }
            catch (Exception ex)
            {
                LogsHelper.AddErrorLogs("文件按编号排序失败", "失败原因：" + ex.Message);
            }
            return false;
        }

        private void DGV_FileList_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (string.IsNullOrEmpty(ToolHelper.GetValue(localPositon)) ||
                e.ColumnIndex == -1) return;
            DataGridView view = sender as DataGridView;
            if (view.Columns[e.ColumnIndex].Name.Contains("fl_unit"))
            {
                for (int i = 0; i < view.RowCount - 1; i++)
                {
                    DataGridViewCell cell = view.Rows[i].Cells[e.ColumnIndex];
                    if (cell.Selected)
                    {
                        cell.Value = localPositon;
                    }
                }
            }
        }

        private void 删除DToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TreeNode node = treeView.SelectedNode;
            if (node != null)
            {
                string tipMsg = $"将要删除{node.Text}，此操作将删除当前及包含的子数据，且不可恢复；\r\n是否确认删除？";
                if (XtraMessageBox.Show(tipMsg, "确认提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    ControlType type = (ControlType)node.Tag;
                    StringBuilder stringBuilder = new StringBuilder();
                    if (type == ControlType.Project)
                    {
                        stringBuilder.Append($"DELETE FROM project_info WHERE pi_id='{node.Name}';");
                        stringBuilder.Append($"DELETE FROM processing_file_list WHERE pfl_obj_id='{node.Name}';");
                        stringBuilder.Append($"DELETE FROM topic_info WHERE ti_id='{node.Name}';");
                        stringBuilder.Append($"DELETE FROM topic_info WHERE ti_obj_id='{node.Name}';");
                        stringBuilder.Append($"DELETE FROM subject_info WHERE si_obj_id='{node.Name}';");
                        string querySQL = $"SELECT ti_id FROM topic_info WHERE ti_obj_id='{node.Name}';";
                        object[] topicIDs = SqlHelper.ExecuteSingleColumnQuery(querySQL);
                        if (topicIDs.Length > 0)
                        {
                            string ids = ToolHelper.GetStringBySplit(topicIDs, ",", "'");
                            stringBuilder.Append($"DELETE FROM subject_info WHERE si_obj_id IN ({ids});");
                        }
                    }
                    else if (type == ControlType.Topic)
                    {
                        stringBuilder.Append($"DELETE FROM topic_info WHERE ti_id='{node.Name}';");
                        stringBuilder.Append($"DELETE FROM subject_info WHERE si_obj_id='{node.Name}';");
                    }
                    else if (type == ControlType.Subject)
                    {
                        stringBuilder.Append($"DELETE FROM subject_info WHERE si_id='{node.Name}';");
                    }
                    if (stringBuilder.Length > 0)
                    {
                        SqlHelper.ExecuteNonQuery(stringBuilder.ToString());
                        node.Remove();
                        XtraMessageBox.Show("删除成功。");
                    }
                }
            }
        }
    }
}
