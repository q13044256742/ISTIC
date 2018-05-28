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
        List<TabPage> tabList = new List<TabPage>();
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
                    lbl_PlanIntroducation.Text = GetValue(_obj[2]);
                }
            }
            else
            {
                dgv_JH_FileList.Tag = GetValue(_obj[0]);
                lbl_JH_Name.Text = GetValue(_obj[1]);
                LoadFileList(dgv_JH_FileList, string.Empty, GetValue(_obj[0]));

                if(!string.IsNullOrEmpty(GetValue(_obj[3])))
                {
                    ObjectSubmitStatus status = (ObjectSubmitStatus)_obj[3];
                    EnableControls(ControlType.Plan_Project, status != ObjectSubmitStatus.SubmitSuccess);
                }
            }
            dgv_JH_FileList.ColumnHeadersDefaultCellStyle = DataGridViewStyleHelper.GetHeaderStyle();
            dgv_JH_FileList.DefaultCellStyle = DataGridViewStyleHelper.GetCellStyle();
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
                dataGridView.Rows[index].Cells[key + "name"].Value = dataTable.Rows[i]["pfl_filename"];
                dataGridView.Rows[index].Cells[key + "user"].Value = dataTable.Rows[i]["pfl_user"];
                dataGridView.Rows[index].Cells[key + "type"].Value = dataTable.Rows[i]["pfl_type"];
                dataGridView.Rows[index].Cells[key + "secret"].Value = dataTable.Rows[i]["pfl_scert"];
                dataGridView.Rows[index].Cells[key + "page"].Value = dataTable.Rows[i]["pfl_page_amount"];
                dataGridView.Rows[index].Cells[key + "amount"].Value = dataTable.Rows[i]["pfl_amount"];
                object _date = dataTable.Rows[i]["pfl_complete_date"];
                if(_date != null)
                    dataGridView.Rows[index].Cells[key + "date"].Value = Convert.ToDateTime(_date).ToString("yyyyMMdd");
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
        }
        /// <summary>
        /// 渲染表格样式，初始化表单字段
        /// </summary>
        private void Frm_MyWork_Load(object sender, EventArgs e)
        {
            //不同加工种类特殊处理
            if(workType == WorkType.PaperWork)
            {
                dgv_JH_FileList.Columns["link"].Visible = false;
                dgv_JH_XM_FileList.Columns["jh_xm_link"].Visible = false;
                dgv_JH_KT_FileList.Columns["jh_kt_link"].Visible = false;
                dgv_JH_XM_KT_ZKT_FileList.Columns["jh_xm_kt_zkt_link"].Visible = false;
                dgv_Imp_FileList.Columns["imp_link"].Visible = false;
                dgv_Imp_Dev_FileList.Columns["imp_dev_link"].Visible = false;
            }
            
            //阶段
            InitialStageList(dgv_JH_FileList.Columns["stage"]);
            InitialStageList(dgv_JH_XM_FileList.Columns["jh_xm_stage"]);
            InitialStageList(dgv_JH_KT_FileList.Columns["jh_kt_stage"]);
            InitialStageList(dgv_JH_XM_KT_ZKT_FileList.Columns["jh_xm_kt_zkt_stage"]);
            InitialStageList(dgv_Imp_FileList.Columns["imp_stage"]);
            InitialStageList(dgv_Imp_Dev_FileList.Columns["imp_dev_stage"]);

            //文件类别
            InitialCategorList(dgv_JH_FileList, string.Empty);
            InitialCategorList(dgv_JH_XM_FileList, "jh_xm_");
            InitialCategorList(dgv_JH_KT_FileList, "jh_kt_");
            InitialCategorList(dgv_JH_XM_KT_ZKT_FileList, "jh_xm_kt_zkt_");
            InitialCategorList(dgv_Imp_FileList, "imp_");
            InitialCategorList(dgv_Imp_Dev_FileList, "imp_dev_");

            //文件类型
            InitialTypeList(dgv_JH_FileList, string.Empty);
            InitialTypeList(dgv_JH_XM_FileList, "jh_xm_");
            InitialTypeList(dgv_JH_KT_FileList, "jh_kt_");
            InitialTypeList(dgv_JH_XM_KT_ZKT_FileList, "jh_xm_kt_zkt_");
            InitialTypeList(dgv_Imp_FileList, "imp_");
            InitialTypeList(dgv_Imp_Dev_FileList, "imp_dev_");

            //载体
            InitialCarrierList(dgv_JH_FileList, string.Empty);
            InitialCarrierList(dgv_JH_XM_FileList, "jh_xm_");
            InitialCarrierList(dgv_JH_KT_FileList, "jh_kt_");
            InitialCarrierList(dgv_JH_XM_KT_ZKT_FileList, "jh_xm_kt_zkt_");
            InitialCarrierList(dgv_Imp_FileList, "imp_");
            InitialCarrierList(dgv_Imp_Dev_FileList, "imp_dev_");

            //文件核查原因列表
            InitialLostReasonList(dgv_JH_FileValid, "dgv_jh_");
            InitialLostReasonList(dgv_JH_XM_FileValid, "dgv_jh_xm_");
            InitialLostReasonList(dgv_JH_KT_FileValid, "dgv_jh_kt_");
            InitialLostReasonList(dgv_JH_XM_KT_ZKT_FileValid, "dgv_jh_xm_kt_zkt_");
            InitialLostReasonList(dgv_Imp_FileValid, "dgv_imp_");
            InitialLostReasonList(dgv_Imp_Dev_FileValid, "dgv_imp_dev_");

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
            if("dgv_JH_FileList".Equals(dataGridView.Name))
            {
                string columnName = dgv_JH_FileList.CurrentCell.OwningColumn.Name;
                Control con = e.Control;
                con.Tag = ControlType.Plan_Project;
                if("stage".Equals(columnName))
                    (con as ComboBox).SelectionChangeCommitted += new EventHandler(StageComboBox_SelectionChangeCommitted);
                else if("categor".Equals(columnName))
                    (con as ComboBox).SelectionChangeCommitted += new EventHandler(CategorComboBox_SelectionChangeCommitted);
            }
            else if("dgv_JH_XM_FileList".Equals(dataGridView.Name))
            {
                string columnName = dgv_JH_XM_FileList.CurrentCell.OwningColumn.Name;
                Control con = e.Control;
                con.Tag = ControlType.Plan_Project;
                if("jh_xm_stage".Equals(columnName))
                    (con as ComboBox).SelectionChangeCommitted += new EventHandler(StageComboBox_SelectionChangeCommitted);
                else if("jh_xm_categor".Equals(columnName))
                    (con as ComboBox).SelectionChangeCommitted += new EventHandler(CategorComboBox_SelectionChangeCommitted);
            }
            else if("dgv_JH_KT_FileList".Equals(dataGridView.Name))
            {
                string columnName = dgv_JH_KT_FileList.CurrentCell.OwningColumn.Name;
                Control con = e.Control;
                con.Tag = ControlType.Plan_Topic;
                if("jh_kt_stage".Equals(columnName))
                    (con as ComboBox).SelectionChangeCommitted += new EventHandler(StageComboBox_SelectionChangeCommitted);
                else if("jh_kt_categor".Equals(columnName))
                    (con as ComboBox).SelectionChangeCommitted += new EventHandler(CategorComboBox_SelectionChangeCommitted);
            }
            else if("dgv_JH_XM_KT_ZKT_FileList".Equals(dataGridView.Name))
            {
                string columnName = dgv_JH_XM_KT_ZKT_FileList.CurrentCell.OwningColumn.Name;
                Control con = e.Control;
                con.Tag = ControlType.Plan_Project_Topic_Subtopic;
                if("jh_xm_kt_zkt_stage".Equals(columnName))
                    (con as ComboBox).SelectionChangeCommitted += new EventHandler(StageComboBox_SelectionChangeCommitted);
                else if("jh_xm_kt_zkt_categor".Equals(columnName))
                    (con as ComboBox).SelectionChangeCommitted += new EventHandler(CategorComboBox_SelectionChangeCommitted);
            }
            else if("dgv_Imp_FileList".Equals(dataGridView.Name))
            {
                string columnName = dgv_Imp_FileList.CurrentCell.OwningColumn.Name;
                Control con = e.Control;
                con.Tag = ControlType.Imp;
                if("imp_stage".Equals(columnName))
                    (con as ComboBox).SelectionChangeCommitted += new EventHandler(StageComboBox_SelectionChangeCommitted);
                else if("imp_categor".Equals(columnName))
                    (con as ComboBox).SelectionChangeCommitted += new EventHandler(CategorComboBox_SelectionChangeCommitted);
            }
            else if("dgv_Imp_Dev_FileList".Equals(dataGridView.Name))
            {
                string columnName = dgv_Imp_Dev_FileList.CurrentCell.OwningColumn.Name;
                Control con = e.Control;
                con.Tag = ControlType.Imp_Dev;
                if("imp_dev_stage".Equals(columnName))
                    (con as ComboBox).SelectionChangeCommitted += new EventHandler(StageComboBox_SelectionChangeCommitted);
                else if("imp_dev_categor".Equals(columnName))
                    (con as ComboBox).SelectionChangeCommitted += new EventHandler(CategorComboBox_SelectionChangeCommitted);
            }
        }
        /// <summary>
        /// 文件阶段 下拉事件
        /// </summary>
        private void StageComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;
            if((ControlType)comboBox.Tag == ControlType.Plan_Project)
                SetCategorByStage(comboBox.SelectedValue, dgv_JH_FileList.CurrentRow, string.Empty);
            else if((ControlType)comboBox.Tag == ControlType.Plan_Project)
                SetCategorByStage(comboBox.SelectedValue, dgv_JH_XM_FileList.CurrentRow, "jh_xm_");
            else if((ControlType)comboBox.Tag == ControlType.Plan_Topic)
                SetCategorByStage(comboBox.SelectedValue, dgv_JH_KT_FileList.CurrentRow, "jh_kt_");
            else if((ControlType)comboBox.Tag == ControlType.Plan_Project_Topic_Subtopic)
                SetCategorByStage(comboBox.SelectedValue, dgv_JH_XM_KT_ZKT_FileList.CurrentRow, "jh_xm_kt_zkt_");
            else if((ControlType)comboBox.Tag == ControlType.Imp)
                SetCategorByStage(comboBox.SelectedValue, dgv_Imp_FileList.CurrentRow, "imp_");
            else if((ControlType)comboBox.Tag == ControlType.Imp_Dev)
                SetCategorByStage(comboBox.SelectedValue, dgv_Imp_Dev_FileList.CurrentRow, "imp_dev_");
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
            if((ControlType)comboBox.Tag == ControlType.Plan_Project)
                SetNameByCategor(comboBox.SelectedValue, dgv_JH_FileList.CurrentRow, string.Empty);
            else if((ControlType)comboBox.Tag == ControlType.Plan_Project)
                SetNameByCategor(comboBox.SelectedValue, dgv_JH_XM_FileList.CurrentRow, "jh_xm_");
            else if((ControlType)comboBox.Tag == ControlType.Plan_Topic)
                SetNameByCategor(comboBox.SelectedValue, dgv_JH_KT_FileList.CurrentRow, "jh_kt_");
            else if((ControlType)comboBox.Tag == ControlType.Plan_Project_Topic_Subtopic)
                SetNameByCategor(comboBox.SelectedValue, dgv_JH_XM_KT_ZKT_FileList.CurrentRow, "jh_xm_kt_zkt_");
            else if((ControlType)comboBox.Tag == ControlType.Imp)
                SetNameByCategor(comboBox.SelectedValue, dgv_Imp_FileList.CurrentRow, "imp_");
            else if((ControlType)comboBox.Tag == ControlType.Imp_Dev)
                SetNameByCategor(comboBox.SelectedValue, dgv_Imp_Dev_FileList.CurrentRow, "imp_dev_");
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

        private void Dgv_File_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
           
        }

        private void Cbo_JH_Next_SelectionChangeCommitted(object sender, EventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;
            if(comboBox.Name.Contains("Imp_Dev"))
            {
                object id = dgv_Imp_Dev_FileList.Tag;
                if(id == null)
                {
                    MessageBox.Show("尚未保存当前项目，无法添加新数据。", "温馨提示");
                    cbo_Imp_Dev_HasNext.SelectedIndex = 0;
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
                        pal_JH_XM.Tag = dgv_Imp_Dev_FileList.Tag;
                        ResetControls(ControlType.Plan_Project);
                    }
                    else if(index == 2)//父级 - 课题
                    {
                        ShowTab("plan_topic", _index + 1);
                        pal_JH_KT.Tag = dgv_Imp_Dev_FileList.Tag;
                        ResetControls(ControlType.Plan_Topic);
                    }
                }
            }
            else
            {
                object id = dgv_JH_FileList.Tag;
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
                        ResetControls(ControlType.Plan_Project);
                        pal_JH_XM.Tag = dgv_JH_FileList.Tag;
                        txt_Project_Code.Text = DateTime.Now.Year + GetValue(planCode);
                    }
                    else if(index == 2)//父级 - 课题
                    {
                        ShowTab("plan_topic", _index + 1);
                        ResetControls(ControlType.Plan_Topic);
                        pal_JH_KT.Tag = dgv_JH_FileList.Tag;
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
                if(dgv_JH_KT_FileList.Tag == null)
                {
                    MessageBox.Show("尚未保存当前课题信息，无法添加新数据。", "温馨提示");
                    cbo_Topic_HasNext.SelectedIndex = 0;
                }
                else
                {
                    ShowTab("plan_topic_subtopic", _index + 1);
                    ResetControls(ControlType.Plan_Topic_Subtopic);
                }
            }
        }
      
        /// <summary>
        /// 保存事件
        /// </summary>
        private void Btn_Save_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;
            DataGridView view = null;
            //计划
            if("btn_JH_Save".Equals(button.Name))
            {
                view = dgv_JH_FileList;
                int fileIndex = tab_JH_FileInfo.SelectedTabPageIndex;
                if(fileIndex == 0)//文件
                {
                    object objId = view.Tag;
                    if(objId == null)
                        objId = view.Tag = AddProjectBasicInfo(this.OBJECT_ID, ControlType.Plan_Project);
                    else
                        UpdateProjectBasicInfo(objId, ControlType.Plan_Project);
                    MessageBox.Show("基础信息保存成功。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    if(CheckFileListComplete(view, string.Empty))
                    {
                        SqlHelper.ExecuteNonQuery($"DELETE FROM processing_file_list WHERE pfl_obj_id='{objId}'");
                        int maxLength = view.Rows.Count - 1;
                        for(int i = 0; i < maxLength; i++)
                        {
                            DataGridViewRow row = view.Rows[i];
                            row.Cells["id"].Tag = AddFileInfo(string.Empty, row, objId, i);
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
                    ModifyFileValid(dgv_JH_FileValid, dgv_JH_FileList.Tag, "dgv_jh_");
                    MessageBox.Show("文件核查信息保存成功！");
                }
                else if(fileIndex == 2)
                {
                    object objId = dgv_JH_FileList.Tag;
                    if(objId != null) {
                        object aid = txt_JH_AJ_Code.Tag;
                        string code = txt_JH_AJ_Code.Text;
                        string name = txt_JH_AJ_Name.Text;
                        string term = txt_JH_AJ_Term.Text;
                        string secret = txt_JH_AJ_Secret.Text;
                        string user = txt_JH_AJ_User.Text;
                        string unit = txt_JH_AJ_Unit.Text;
                        if(aid == null)
                        {
                            aid = Guid.NewGuid().ToString();
                            string insertSql = $"INSERT INTO processing_tag VALUES ('{aid}','{code}','{name}','{term}','{secret}','{user}','{unit}','{objId}')";
                            SqlHelper.ExecuteNonQuery(insertSql);
                            txt_JH_AJ_Code.Tag = aid;
                        }
                        else
                        {
                            string updateSql = $"UPDATE processing_tag SET pt_code='{code}',pt_name='{name}',pt_term='{term}',pt_secret='{secret}',pt_user='{user}',pt_unit='{unit}' WHERE pt_id='{aid}'";
                            SqlHelper.ExecuteNonQuery(updateSql);
                        }
                        MessageBox.Show($"案卷信息保存成功！");
                    }
                }
            }
            //计划-项目
            else if("btn_JH_XM_Save".Equals(button.Name))
            {
                view = dgv_JH_XM_FileList;
                int fileIndex = tab_JH_XM_FileInfo.SelectedTabPageIndex;
                if(fileIndex == 0)
                {
                    string code = txt_Project_Code.Text;
                    if(string.IsNullOrEmpty(code))
                        MessageBox.Show("项目编号不能为空。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    else
                    {
                        object objId = view.Tag;
                        if(objId != null)//更新
                            UpdateProjectBasicInfo(view.Tag, ControlType.Plan_Project);
                        else//新增
                            objId = view.Tag = AddProjectBasicInfo(pal_JH_XM.Tag, ControlType.Plan_Project);
                        MessageBox.Show("基础信息保存成功。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        if(CheckFileListComplete(view,"jh_xm_"))
                        {
                            SqlHelper.ExecuteNonQuery($"DELETE FROM processing_file_list WHERE pfl_obj_id='{objId}'");
                            int maxLength = view.Rows.Count - 1;
                            for(int i = 0; i < maxLength; i++)
                            {
                                DataGridViewRow row = view.Rows[i];
                                row.Cells["jh_xm_id"].Tag = AddFileInfo("jh_xm_", row, objId, i);
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
                    ModifyFileValid(dgv_JH_XM_FileValid, dgv_JH_XM_FileList.Tag, "dgv_jh_xm_");
                    MessageBox.Show("文件核查信息保存成功！");
                }
                else if(fileIndex == 2)
                {
                    object objId = dgv_JH_XM_FileList.Tag;
                    if(objId != null)
                    {
                        object aid = txt_JH_XM_AJ_Code.Tag;
                        string code = txt_JH_XM_AJ_Code.Text;
                        string name = txt_JH_XM_AJ_Name.Text;
                        string term = txt_JH_XM_AJ_Term.Text;
                        string secret = txt_JH_XM_AJ_Secret.Text;
                        string user = txt_JH_XM_AJ_User.Text;
                        string unit = txt_JH_XM_AJ_Unit.Text;
                        if(aid == null)
                        {
                            aid = Guid.NewGuid().ToString();
                            string insertSql = $"INSERT INTO processing_tag VALUES ('{aid}','{code}','{name}','{term}','{secret}','{user}','{unit}','{objId}')";
                            SqlHelper.ExecuteNonQuery(insertSql);
                            txt_JH_XM_AJ_Code.Tag = aid;
                        }
                        else
                        {
                            string updateSql = $"UPDATE processing_tag SET pt_code='{code}',pt_name='{name}',pt_term='{term}',pt_secret='{secret}',pt_user='{user}',pt_unit='{unit}' WHERE pt_id='{aid}'";
                            SqlHelper.ExecuteNonQuery(updateSql);
                        }
                        MessageBox.Show($"案卷信息保存成功！");
                    }
                }
            }
            //计划-课题
            else if("btn_JH_KT_Save".Equals(button.Name))
            {
                view = dgv_JH_KT_FileList;
                int fileIndex = tab_JH_KT_FileInfo.SelectedTabPageIndex;
                if(fileIndex == 0)
                {
                    string code = txt_Topic_Code.Text;
                    if(string.IsNullOrEmpty(code))
                        MessageBox.Show("课题编号不能为空。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    else
                    {
                        object objId = view.Tag;
                        if(objId != null)//更新
                            UpdateProjectBasicInfo(objId, ControlType.Plan_Topic);
                        else//新增
                            objId = view.Tag = AddProjectBasicInfo(pal_JH_KT.Tag, ControlType.Plan_Topic);
                        MessageBox.Show("基础信息保存成功。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        if(CheckFileListComplete(view, "jh_kt_"))
                        {
                            SqlHelper.ExecuteNonQuery($"DELETE FROM processing_file_list WHERE pfl_obj_id='{objId}'");
                            int maxLength = view.Rows.Count - 1;
                            for(int i = 0; i < maxLength; i++)
                            {
                                DataGridViewRow row = view.Rows[i];
                                row.Cells["jh_kt_id"].Tag = AddFileInfo("jh_kt_", row, objId, i);
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
                    ModifyFileValid(dgv_JH_KT_FileValid, dgv_JH_KT_FileList.Tag, "dgv_jh_kt_");
                    MessageBox.Show("文件核查信息保存成功！");
                }
                else if(fileIndex == 2)
                {
                    object objId = dgv_JH_KT_FileList.Tag;
                    if(objId != null)
                    {
                        object aid = txt_JH_KT_AJ_Code.Tag;
                        string code = txt_JH_KT_AJ_Code.Text;
                        string name = txt_JH_KT_AJ_Name.Text;
                        string term = txt_JH_KT_AJ_Term.Text;
                        string secret = txt_JH_KT_AJ_Secret.Text;
                        string user = txt_JH_KT_AJ_User.Text;
                        string unit = txt_JH_KT_AJ_Unit.Text;
                        if(aid == null)
                        {
                            aid = Guid.NewGuid().ToString();
                            string insertSql = $"INSERT INTO processing_tag VALUES ('{aid}','{code}','{name}','{term}','{secret}','{user}','{unit}','{objId}')";
                            SqlHelper.ExecuteNonQuery(insertSql);
                            txt_JH_KT_AJ_Code.Tag = aid;
                        }
                        else
                        {
                            string updateSql = $"UPDATE processing_tag SET pt_code='{code}',pt_name='{name}',pt_term='{term}',pt_secret='{secret}',pt_user='{user}',pt_unit='{unit}' WHERE pt_id='{aid}'";
                            SqlHelper.ExecuteNonQuery(updateSql);
                        }
                        MessageBox.Show($"案卷信息保存成功！");
                    }
                }
            }
            //计划-项目-课题-子课题
            else if("btn_JH_XM_KT_ZKT_Save".Equals(button.Name))
            {
                view = dgv_JH_XM_KT_ZKT_FileList;
                int fileIndex = tab_JH_XM_KT_ZKT_FileInfo.SelectedTabPageIndex;
                if(fileIndex == 0)
                {
                    string code = txt_Subject_Code.Text;
                    if(string.IsNullOrEmpty(code))
                        MessageBox.Show("课题编号不能为空。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    else
                    {
                        object objId = view.Tag;
                        if(objId != null)//更新
                            UpdateProjectBasicInfo(objId, ControlType.Plan_Project_Topic_Subtopic);
                        else//新增
                            objId = view.Tag = AddProjectBasicInfo(pal_JH_XM_KT_ZKT.Tag, ControlType.Plan_Project_Topic_Subtopic);
                        MessageBox.Show("基础信息保存成功。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        if(CheckFileListComplete(view, "jh_xm_kt_zkt_"))
                        {
                            SqlHelper.ExecuteNonQuery($"DELETE FROM processing_file_list WHERE pfl_obj_id='{objId}'");
                            int maxLength = view.Rows.Count - 1;
                            for(int i = 0; i < maxLength; i++)
                            {
                                DataGridViewRow row = view.Rows[i];
                                row.Cells["jh_xm_kt_zkt_id"].Tag = AddFileInfo("jh_xm_kt_zkt_", row, objId, i);
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
                    ModifyFileValid(dgv_JH_XM_KT_ZKT_FileValid, dgv_JH_XM_KT_ZKT_FileList.Tag, "dgv_jh_xm_kt_zkt_");
                    MessageBox.Show("文件核查信息保存成功！");
                }
                else if(fileIndex == 2)
                {
                    object objId = dgv_JH_XM_KT_ZKT_FileList.Tag;
                    if(objId != null)
                    {
                        object aid = txt_JH_XM_KT_ZKT_AJ_Code.Tag;
                        string code = txt_JH_XM_KT_ZKT_AJ_Code.Text;
                        string name = txt_JH_XM_KT_ZKT_AJ_Name.Text;
                        string term = txt_JH_XM_KT_ZKT_AJ_Term.Text;
                        string secret = txt_JH_XM_KT_ZKT_AJ_Secret.Text;
                        string user = txt_JH_XM_KT_ZKT_AJ_User.Text;
                        string unit = txt_JH_XM_KT_ZKT_AJ_Unit.Text;
                        if(aid == null)
                        {
                            aid = Guid.NewGuid().ToString();
                            string insertSql = $"INSERT INTO processing_tag VALUES ('{aid}','{code}','{name}','{term}','{secret}','{user}','{unit}','{objId}')";
                            SqlHelper.ExecuteNonQuery(insertSql);
                            txt_JH_XM_KT_ZKT_AJ_Code.Tag = aid;
                        }
                        else
                        {
                            string updateSql = $"UPDATE processing_tag SET pt_code='{code}',pt_name='{name}',pt_term='{term}',pt_secret='{secret}',pt_user='{user}',pt_unit='{unit}' WHERE pt_id='{aid}'";
                            SqlHelper.ExecuteNonQuery(updateSql);
                        }
                        MessageBox.Show($"案卷信息保存成功！");
                    }
                }
            }
            //重大专项/研发
            else if("btn_Imp_Save".Equals(button.Name))
            {
                view = dgv_Imp_FileList;
                int fileIndex = tab_Imp_FileInfo.SelectedTabPageIndex;
                if(fileIndex == 0)
                {
                    object impId = view.Tag;
                    if(impId == null)
                        impId = view.Tag = AddProjectBasicInfo(OBJECT_ID, ControlType.Imp);
                    else
                        UpdateProjectBasicInfo(impId, ControlType.Imp);
                    MessageBox.Show("基础信息保存成功。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    if(CheckFileListComplete(view, "imp_"))
                    {
                        SqlHelper.ExecuteNonQuery($"DELETE FROM processing_file_list WHERE pfl_obj_id='{impId}'");
                        int maxLength = view.Rows.Count - 1;
                        for(int i = 0; i < maxLength; i++)
                        {
                            DataGridViewRow row = view.Rows[i];
                            row.Cells["imp_id"].Value = AddFileInfo("imp_", row, impId, i);
                        }
                        SqlHelper.ExecuteNonQuery($"UPDATE processing_tag SET pt_secret='{GetMaxSecretById(impId)}' WHERE pt_obj_id='{impId}'");
                        MessageBox.Show("文件信息保存成功。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

                        GoToTreeList();
                    }
                    else
                        MessageBox.Show("存在文件名重复的文件。", "保存失败", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
                else if(fileIndex == 1)//文件核查
                {
                    ModifyFileValid(dgv_Imp_FileValid, dgv_Imp_FileList.Tag, "dgv_imp_");
                    MessageBox.Show("文件核查信息保存成功！");
                }
                else if(fileIndex == 2)
                {
                    object objId = dgv_Imp_FileList.Tag;
                    if(objId != null)
                    {
                        object aid = txt_Imp_AJ_Code.Tag;
                        string code = txt_Imp_AJ_Code.Text;
                        string name = txt_Imp_AJ_Name.Text;
                        string term = txt_Imp_AJ_Term.Text;
                        string secret = txt_Imp_AJ_Secret.Text;
                        string user = txt_Imp_AJ_User.Text;
                        string unit = txt_Imp_AJ_Unit.Text;
                        if(aid == null)
                        {
                            aid = Guid.NewGuid().ToString();
                            string insertSql = $"INSERT INTO processing_tag VALUES ('{aid}','{code}','{name}','{term}','{secret}','{user}','{unit}','{objId}')";
                            SqlHelper.ExecuteNonQuery(insertSql);
                            txt_Imp_AJ_Code.Tag = aid;
                        }
                        else
                        {
                            string updateSql = $"UPDATE processing_tag SET pt_code='{code}',pt_name='{name}',pt_term='{term}',pt_secret='{secret}',pt_user='{user}',pt_unit='{unit}' WHERE pt_id='{aid}'";
                            SqlHelper.ExecuteNonQuery(updateSql);
                        }
                        MessageBox.Show($"案卷信息保存成功！");
                    }
                }
            }
            //重大专项/研发-信息
            else if("btn_Imp_Sub_Save".Equals(button.Name))
            {
                view = dgv_Imp_Dev_FileList;
                int fileIndex = tab_Imp_Dev_FileInfo.SelectedTabPageIndex;
                if(fileIndex == 0)
                {
                    string code = txt_Imp_Dev_Code.Text;
                    if(string.IsNullOrEmpty(code))
                        MessageBox.Show("编号不能为空。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    else
                    {
                        object objId = view.Tag;
                        if(objId != null)//更新
                            UpdateProjectBasicInfo(objId, ControlType.Imp_Dev);
                        else//新增
                            objId = view.Tag = AddProjectBasicInfo(pal_Imp_Dev.Tag, ControlType.Imp_Dev);
                        MessageBox.Show("基础信息保存成功。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        if(CheckFileListComplete(view, "imp_dev_"))
                        {
                            SqlHelper.ExecuteNonQuery($"DELETE FROM processing_file_list WHERE pfl_obj_id='{objId}'");
                            int maxLength = view.Rows.Count - 1;
                            for(int i = 0; i < maxLength; i++)
                            {
                                DataGridViewRow row = view.Rows[i];
                                row.Cells["imp_dev_id"].Tag = AddFileInfo("imp_dev_", row, objId, i);
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
                    ModifyFileValid(dgv_Imp_Dev_FileValid, dgv_Imp_Dev_FileList.Tag, "dgv_imp_dev_");
                    MessageBox.Show("文件核查信息保存成功！");
                }
                else if(fileIndex == 2)
                {
                    object objId = dgv_Imp_Dev_FileList.Tag;
                    if(objId != null)
                    {
                        object aid = txt_Imp_Dev_AJ_Code.Tag;
                        string code = txt_Imp_Dev_AJ_Code.Text;
                        string name = txt_Imp_Dev_AJ_Name.Text;
                        string term = txt_Imp_Dev_AJ_Term.Text;
                        string secret = txt_Imp_Dev_AJ_Secret.Text;
                        string user = txt_Imp_Dev_AJ_User.Text;
                        string unit = txt_Imp_Dev_AJ_Unit.Text;
                        if(aid == null)
                        {
                            aid = Guid.NewGuid().ToString();
                            string insertSql = $"INSERT INTO processing_tag VALUES ('{aid}','{code}','{name}','{term}','{secret}','{user}','{unit}','{objId}')";
                            SqlHelper.ExecuteNonQuery(insertSql);
                            txt_Imp_Dev_AJ_Code.Tag = aid;
                        }
                        else
                        {
                            string updateSql = $"UPDATE processing_tag SET pt_code='{code}',pt_name='{name}',pt_term='{term}',pt_secret='{secret}',pt_user='{user}',pt_unit='{unit}' WHERE pt_id='{aid}'";
                            SqlHelper.ExecuteNonQuery(updateSql);
                        }
                        MessageBox.Show($"案卷信息保存成功！");
                    }
                }
            }
        }

        private void GoToTreeList()
        {
            if(workType == WorkType.Default)
            {
                if(controlType == ControlType.Plan_Project)
                    LoadTreeList(PLAN_ID, ControlType.Plan_Project);
                else
                    LoadTreeList(lbl_Imp_Name.Tag, ControlType.Imp_Sub);
            }
            else
                LoadTreeList(dgv_JH_FileList.Tag, ControlType.Default);
        }

        /// <summary>
        /// 检验文件列表是否可以保存
        /// </summary>
        /// <param name="dataGridView">待检验的表格</param>
        private bool CheckFileListComplete(DataGridView dataGridView, string key)
        {
            for(int i = 0; i < dataGridView.Rows.Count - 1; i++)
            {
                DataGridViewCell name1 = dataGridView.Rows[i].Cells[key + "name"];
                if(name1.Value != null)
                {
                    for(int j = i + 1; j < dataGridView.Rows.Count - 1; j++)
                    {
                        DataGridViewCell name2 = dataGridView.Rows[j].Cells[key + "name"];
                        if(name1.Value.Equals(name2.Value))
                        {
                            name1.ErrorText = $"温馨提示：与{j + 1}行文件名重复";
                            name2.ErrorText = $"温馨提示：与{i + 1}行文件名重复";
                            return false;
                        }
                    }
                }
            }
            return true;
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
            if(controlType == ControlType.Plan_Project)
            {

            }
            else if(controlType == ControlType.Plan_Project)
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
            else if(controlType == ControlType.Plan_Topic)
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
            else if(controlType == ControlType.Plan_Project_Topic_Subtopic)
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
            else if(controlType == ControlType.Imp_Dev)
            {
                string code = txt_Imp_Dev_Code.Text;
                string name = txt_Imp_Dev_Name.Text;
                string unit = txt_Imp_Dev_Unit.Text;

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
            if(type == ControlType.Plan_Project)
            {
                object name = lbl_JH_Name.Text;
                SqlHelper.ExecuteNonQuery($"INSERT INTO project_info(pi_id,pi_code,pi_name,pi_obj_id,pi_categor,pi_submit_status,pi_worker_id)" +
                            $" VALUES('{primaryKey}','{planCode}','{name}','{parentId}','{(int)ControlType.Plan_Project}','{(int)ObjectSubmitStatus.NonSubmit}','{UserHelper.GetInstance().User.UserKey}')");
            }
            else if(type == ControlType.Plan_Project)
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
            else if(type == ControlType.Plan_Topic)
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
            else if(type == ControlType.Plan_Project_Topic_Subtopic)
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
                string insertSql = "INSERT INTO imp_info(imp_id, imp_code, imp_name, pi_categor, imp_submit_status, imp_obj_id, imp_source_id, imp_type) " +
                    $"VALUES ('{primaryKey}', '{planCode}', '{name}', '{(int)type}', '{(int)ObjectSubmitStatus.NonSubmit}', '{parentId}', '{UserHelper.GetInstance().User.UserKey}', {DEV_TYPE})";
                SqlHelper.ExecuteNonQuery(insertSql);
            }
            else if(type == ControlType.Imp_Dev)
            {
                string code = txt_Imp_Dev_Code.Text;
                string name = txt_Imp_Dev_Name.Text;
                string unit = txt_Imp_Dev_Unit.Text;

                string insertSql = "INSERT INTO imp_dev_info VALUES " +
                    $"('{primaryKey}'" +
                    $",'{code}'" +
                    $",'{name}'" +
                    $",'{unit}'" +
                    $",null" +
                    $",'{(int)ControlType.Imp_Dev}'" +
                    $",'{(int)SubmitStatus.NonSubmit}'" +
                    $",'{parentId}'" +
                    $",'{UserHelper.GetInstance().User.UserKey}')";
                SqlHelper.ExecuteNonQuery(insertSql);
            }
            return primaryKey;
        }
        /// <summary>
        /// 更新文件信息
        /// </summary>
        private void UpdateFileInfo(string key, DataGridViewRow row)
        {
            object pflid = row.Cells[key + "id"].Tag;
            object stage = row.Cells[key + "stage"].Value;
            object categor = row.Cells[key + "categor"].Value;
            object name = row.Cells[key + "name"].Value;
            object user = row.Cells[key + "user"].Value;
            object type = row.Cells[key + "type"].Value;
            object secret = row.Cells[key + "secret"].Value;
            object page = row.Cells[key + "page"].Value;
            object amount = row.Cells[key + "amount"].Value;
            DateTime date = DateTime.Now;
            string _date = GetValue(row.Cells[key + "date"].Value);
            if(!string.IsNullOrEmpty(_date))
            {
                if(_date.Length == 6)
                    _date = _date.Substring(0, 4) + "-" + _date.Substring(4, 2) + "-01";
                if(_date.Length == 8)
                    _date = _date.Substring(0, 4) + "-" + _date.Substring(4, 2) + "-" + _date.Substring(6, 2);
                DateTime.TryParse(_date, out date);
            }
            object unit = row.Cells[key + "unit"].Value;
            object carrier = row.Cells[key + "carrier"].Value;
            object format = row.Cells[key + "format"].Value;
            object form = row.Cells[key + "form"].Value;
            object link = row.Cells[key + "link"].Value;
            object remark = row.Cells[key + "remark"].Value;

            string updateSql = "UPDATE processing_file_list SET " +
                $"pfl_stage = '{stage}'" +
                $",pfl_categor = '{categor}'" +
                $",pfl_filename = '{name}'" +
                $",pfl_user = '{user}'" +
                $",pfl_type = '{type}'" +
                $",pfl_scert = '{secret}'" +
                $",pfl_page_amount = '{page}'" +
                $",pfl_amount = '{amount}'" +
                $",pfl_complete_date = '{date}'" +
                $",pfl_save_location = '{unit}'" +
                $",pfl_carrier = '{carrier}'" +
                $",pfl_file_format = '{format}'" +
                $",pfl_file_form = '{form}'" +
                $",pfl_file_link = '{link}'" +
                $",pfl_remark = '{remark}'" +
                $",pfl_modify_user = '{UserHelper.GetInstance().User.UserKey}'" +
                $",pfl_handle_time = '{DateTime.Now}'" +
                $" WHERE pfl_id = '{pflid}'";
            SqlHelper.ExecuteNonQuery(updateSql);
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
            object pflid = Guid.NewGuid().ToString();
            object stage = row.Cells[key + "stage"].Value;
            object categor = row.Cells[key + "categor"].Value;
            object name = row.Cells[key + "name"].Value;
            object user = row.Cells[key + "user"].Value;
            object type = row.Cells[key + "type"].Value;
            object secret = row.Cells[key + "secret"].Value;
            object page = row.Cells[key + "page"].Value;
            object amount = row.Cells[key + "amount"].Value;

            DateTime date = DateTime.Now;
            string _date = GetValue(row.Cells[key + "date"].Value);
            if(!string.IsNullOrEmpty(_date))
            {
                if(_date.Length == 4)
                    _date = _date + "-" + date.Month + "-" + date.Day;
                else if(_date.Length == 6)
                    _date = _date.Substring(0, 4) + "-" + _date.Substring(4, 2) + "-01";
                else if(_date.Length == 8)
                    _date = _date.Substring(0, 4) + "-" + _date.Substring(4, 2) + "-" + _date.Substring(6, 2);
                DateTime.TryParse(_date, out date);
            }
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
                $",'{link}' ,'{remark}' ,{(int)status} ,'{UserHelper.GetInstance().User.UserKey}','{DateTime.Now}', '{sort}')";

            SqlHelper.ExecuteNonQuery(insertSql);
            return pflid;
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
                    else if(type == ControlType.Imp_Normal)
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
                    else if(type == ControlType.Imp_Sub)
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
                            Tag = ControlType.Imp_Sub
                        };
                        treeNode.Nodes.Add(treeNode2);
                    }
                    //项目/课题
                    else if(type == ControlType.Plan_Project)
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
                                Tag = ControlType.Imp_Sub,
                                ForeColor = DisEnbleColor
                            };
                            treeNode.Nodes.Add(treeNode2);
                            TreeNode currentNode = new TreeNode()
                            {
                                Name = GetValue(currentRow["pi_id"]),
                                Text = GetValue(currentRow["pi_code"]),
                                Tag = ControlType.Plan_Project,
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
                                    Tag = ControlType.Plan_Project_Topic,
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
                                        Tag = ControlType.Plan_Project_Topic_Subtopic,
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
                                    Tag = ControlType.Imp_Normal,
                                    ForeColor = DisEnbleColor
                                };
                                TreeNode currentNode = new TreeNode()
                                {
                                    Name = GetValue(currentRow["pi_id"]),
                                    Text = GetValue(currentRow["pi_code"]),
                                    Tag = ControlType.Plan_Project,
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
                                        Tag = ControlType.Plan_Project_Topic,
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
                                            Tag = ControlType.Plan_Project_Topic_Subtopic,
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
                            Tag = ControlType.Imp_Sub
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
                        if(type == ControlType.Imp || type == ControlType.Imp_Sub)
                        {
                            ShowTab("imp", 0);
                            LoadImpPage(node.Name, node.ForeColor);
                        }
                        else if(type == ControlType.Imp_Normal)
                        {
                            ShowTab("plan", 0);
                            LoadPlanPage(node.Name, Color.Black);
                        }
                        else if(type == ControlType.Plan_Project)
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
                        Tag = ControlType.Plan_Project,
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
                        Tag = ControlType.Plan_Project
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
                        Tag = ControlType.Plan_Project,
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
                        Tag = ControlType.Plan_Project
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
            if(type == ControlType.Plan_Project)
            {
                ShowTab("plan", 0);
                LoadPlanPage(e.Node.Name, e.Node.ForeColor);
            }
            else if(type == ControlType.Plan_Project)
            {
                tab_MenuList.TabPages.Clear();
                if(workType == WorkType.Default)
                {
                    int count = SqlHelper.ExecuteCountQuery($"SELECT COUNT(pi_id) FROM project_info WHERE pi_id='{e.Node.Parent.Name}'");
                    if(count == 0)
                    {
                        ShowTab("imp", 0);
                        LoadImpPage(e.Node.Parent.Parent.Name, e.Node.Parent.Parent.ForeColor);

                        ShowTab("imp_dev", 1);
                        LoadPageBasicInfo(e.Node.Parent.Name, ControlType.Imp_Sub, e.Node.Parent.ForeColor);

                        ShowTab("plan_project", 2);
                        LoadPageBasicInfo(e.Node.Name, type, e.Node.ForeColor);
                    }
                    else
                    {
                        ShowTab("plan", 0);
                        LoadPlanPage(e.Node.Parent.Name, e.Node.Parent.ForeColor);

                        ShowTab("plan_project", 1);
                        LoadPageBasicInfo(e.Node.Name, ControlType.Plan_Project, e.Node.ForeColor);
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
            else if(type == ControlType.Plan_Topic)
            {
                tab_MenuList.TabPages.Clear();
                if(workType == WorkType.Default)
                {
                    ShowTab("imp", 0);
                    LoadImpPage(e.Node.Parent.Parent.Name, e.Node.Parent.Parent.ForeColor);

                    ShowTab("imp_dev", 1);
                    LoadPageBasicInfo(e.Node.Parent.Name, ControlType.Imp_Sub, e.Node.Parent.ForeColor);

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
            else if(type == ControlType.Plan_Project_Topic)
            {
                tab_MenuList.TabPages.Clear();
                if(workType == WorkType.Default)
                {
                    int count = SqlHelper.ExecuteCountQuery($"SELECT COUNT(pi_id) FROM project_info WHERE pi_id='{e.Node.Parent.Parent.Name}'");
                    if(count == 0)
                    {
                        ShowTab("imp", 0);
                        LoadImpPage(e.Node.Parent.Parent.Parent.Name, e.Node.Parent.Parent.Parent.ForeColor);

                        ShowTab("imp_dev", 1);
                        LoadPageBasicInfo(e.Node.Parent.Parent.Name, ControlType.Imp_Sub, e.Node.Parent.Parent.ForeColor);

                        ShowTab("plan_project", 2);
                        LoadPageBasicInfo(e.Node.Parent.Name, ControlType.Plan_Project, e.Node.Parent.ForeColor);

                        ShowTab("plan_project_topic", 3);
                        LoadPageBasicInfo(e.Node.Name, ControlType.Plan_Project_Topic, e.Node.ForeColor);
                    }
                    else
                    {
                        ShowTab("plan", 0);
                        LoadPlanPage(e.Node.Parent.Parent.Name, e.Node.Parent.Parent.ForeColor);

                        ShowTab("plan_project", 1);
                        LoadPageBasicInfo(e.Node.Parent.Name, ControlType.Plan_Project, e.Node.Parent.ForeColor);

                        ShowTab("plan_project_topic", 2);
                        LoadPageBasicInfo(e.Node.Name, type, e.Node.ForeColor);
                    }
                }
                else if(workType == WorkType.CDWork || workType == WorkType.PaperWork)
                {
                    ShowTab("plan", 0);
                    LoadPlanPage(e.Node.Parent.Parent.Name, e.Node.Parent.Parent.ForeColor);

                    ShowTab("plan_project", 1);
                    LoadPageBasicInfo(e.Node.Parent.Name, ControlType.Plan_Project, e.Node.Parent.ForeColor);

                    ShowTab("plan_project_topic", 2);
                    LoadPageBasicInfo(e.Node.Name, ControlType.Plan_Project_Topic, e.Node.ForeColor);
                }
                else if(workType == WorkType.ProjectWork || workType == WorkType.SubjectWork)
                {
                    ShowTab("plan", 0);
                    LoadPlanPage(e.Node.Parent.Parent.Name, e.Node.Parent.Parent.ForeColor);

                    ShowTab("plan_project", 1);
                    LoadPageBasicInfo(e.Node.Parent.Name, ControlType.Plan_Project, e.Node.Parent.ForeColor);

                    ShowTab("plan_project_topic", 2);
                    LoadPageBasicInfo(e.Node.Name, ControlType.Plan_Project_Topic, e.Node.ForeColor);
                }
            }
            else if(type == ControlType.Plan_Topic_Subtopic)
            {
                tab_MenuList.TabPages.Clear();
                if(workType == WorkType.Default)
                {
                    ShowTab("imp", 0);
                    LoadImpPage(e.Node.Parent.Parent.Parent.Name, e.Node.Parent.Parent.Parent.ForeColor);

                    ShowTab("imp_dev", 1);
                    LoadPageBasicInfo(e.Node.Parent.Parent.Name, ControlType.Imp_Sub, e.Node.Parent.Parent.ForeColor);

                    ShowTab("plan_topic", 2);
                    LoadPageBasicInfo(e.Node.Parent.Name, ControlType.Plan_Topic, e.Node.Parent.ForeColor);

                    ShowTab("plan_topic_subtopic", 3);
                    LoadPageBasicInfo(e.Node.Name, ControlType.Plan_Topic_Subtopic, e.Node.ForeColor);
                }
                else if(workType == WorkType.CDWork || workType == WorkType.PaperWork)
                {

                    ShowTab("plan", 0);
                    LoadPlanPage(e.Node.Parent.Parent.Name, e.Node.Parent.Parent.ForeColor);

                    ShowTab("plan_topic", 1);
                    LoadPageBasicInfo(e.Node.Parent.Name, ControlType.Plan_Topic, e.Node.Parent.ForeColor);

                    ShowTab("plan_topic_subtopic", 2);
                    LoadPageBasicInfo(e.Node.Name, type, e.Node.ForeColor);
                }
            }
            else if(type == ControlType.Plan_Project_Topic_Subtopic)
            {
                if(workType == WorkType.Default)
                {
                    int count = SqlHelper.ExecuteCountQuery($"SELECT COUNT(pi_id) FROM project_info WHERE pi_id='{e.Node.Parent.Parent.Parent.Name}'");
                    if(count == 0)
                    {
                        ShowTab("imp", 0);
                        LoadImpPage(e.Node.Parent.Parent.Parent.Parent.Name, e.Node.Parent.Parent.Parent.Parent.ForeColor);

                        ShowTab("imp_dev", 1);
                        LoadPageBasicInfo(e.Node.Parent.Parent.Parent.Name, ControlType.Imp_Sub, e.Node.Parent.Parent.Parent.ForeColor);

                        ShowTab("plan_project", 2);
                        LoadPageBasicInfo(e.Node.Parent.Parent.Name, ControlType.Plan_Project, e.Node.Parent.Parent.ForeColor);

                        ShowTab("plan_project_topic", 3);
                        LoadPageBasicInfo(e.Node.Parent.Name, ControlType.Plan_Project_Topic, e.Node.Parent.ForeColor);

                        ShowTab("plan_project_topic_subtopic", 4);
                        LoadPageBasicInfo(e.Node.Name, ControlType.Plan_Project_Topic_Subtopic, e.Node.ForeColor);
                    }
                    else
                    {
                        ShowTab("plan", 0);
                        LoadPlanPage(e.Node.Parent.Parent.Parent.Name, e.Node.Parent.Parent.Parent.ForeColor);

                        ShowTab("plan_project", 1);
                        LoadPageBasicInfo(e.Node.Parent.Parent.Name, ControlType.Plan_Project, e.Node.Parent.Parent.ForeColor);

                        ShowTab("plan_project_topic", 2);
                        LoadPageBasicInfo(e.Node.Parent.Name, ControlType.Plan_Project_Topic, e.Node.Parent.ForeColor);

                        ShowTab("plan_project_topic_subtopic", 3);
                        LoadPageBasicInfo(e.Node.Name, ControlType.Plan_Project_Topic_Subtopic, e.Node.ForeColor);
                    }
                }
                else
                {
                    tab_MenuList.TabPages.Clear();

                    ShowTab("plan", 0);
                    LoadPlanPage(e.Node.Parent.Parent.Parent.Name, e.Node.Parent.Parent.Parent.ForeColor);

                    ShowTab("plan_project", 1);
                    LoadPageBasicInfo(e.Node.Parent.Parent.Name, ControlType.Plan_Project,e.Node.Parent.Parent.ForeColor);

                    ShowTab("plan_project_topic", 2);
                    LoadPageBasicInfo(e.Node.Parent.Name, ControlType.Plan_Project_Topic,e.Node.Parent.ForeColor);

                    ShowTab("plan_project_topic_subtopic", 3);
                    LoadPageBasicInfo(e.Node.Name, type, e.Node.ForeColor);
                }
            }
            else if(type == ControlType.Imp || type == ControlType.Imp_Dev)
            {
                tab_MenuList.TabPages.Clear();

                ShowTab("imp", 0);
                LoadImpPage(e.Node.Name, e.Node.ForeColor);

            }
            else if(type == ControlType.Imp_Sub)
            {
                tab_MenuList.TabPages.Clear();

                ShowTab("imp", 0);
                LoadImpPage(e.Node.Parent.Name, e.Node.Parent.ForeColor);

                ShowTab("imp_dev", 1);
                LoadPageBasicInfo(e.Node.Name, type, e.Node.ForeColor);
            }
            else if(type == ControlType.Imp_Normal)
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
                dgv_Imp_FileList.Tag = GetValue(_obj[0]);
                LoadFileList(dgv_Imp_FileList, "imp_", GetValue(_obj[0]));
            }
            if(_obj != null)
            {
                lbl_Imp_Name.Tag = GetValue(_obj[0]);
                lbl_Imp_Name.Text = GetValue(_obj[1]);
                lbl_Imp_Intro.Text = GetValue(_obj[2]);
            }
            //加载下拉列表
            if(cbo_Imp_HasNext.DataSource == null)
            {
                object key = objId;
                if(DEV_TYPE == -1)
                    key = SqlHelper.ExecuteOnlyOneQuery($"SELECT dd_code FROM data_dictionary WHERE dd_id='{key}'");
                if(DEV_TYPE == 0)//重点研发
                    key = "dic_plan_imp";
                else if(DEV_TYPE == 1 || "dic_imp_dev".Equals(key))
                {
                    key = "dic_imp_dev";
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
                object objid = dgv_JH_FileList.Tag;
                btn_JH_AddFile.Visible =  false;
                if(index == 0)
                    btn_JH_AddFile.Visible =  true;
                else if(index == 1)//文件核查
                {
                    if(objid != null)
                    {
                        LoadFileValidList(dgv_JH_FileValid, objid, "dgv_jh_");
                    }
                }
                else if(index == 2)//案卷信息
                {
                    if(objid != null)
                    {
                        DataTable dataTable = SqlHelper.ExecuteQuery($"SELECT * FROM processing_tag WHERE pt_obj_id='{objid}'");
                        if(dataTable.Rows.Count > 0)
                        {
                            DataRow row = dataTable.Rows[0];
                            txt_JH_AJ_Code.Tag = GetValue(row["pt_id"]);
                            txt_JH_AJ_Code.Text = GetValue(row["pt_code"]);
                            txt_JH_AJ_Name.Text = GetValue(row["pt_name"]);
                            txt_JH_AJ_Term.Text = GetValue(row["pt_term"]);
                            txt_JH_AJ_Secret.Text = GetValue(row["pt_secret"]);
                            txt_JH_AJ_User.Text = GetValue(row["pt_user"]);
                            txt_JH_AJ_Unit.Text = GetValue(row["pt_unit"]);
                        }
                        else
                        {
                            txt_JH_AJ_Code.Text = $"{planCode}-{DateTime.Now.Year}-{GetAJAmount(planCode)}";
                            txt_JH_AJ_Name.Text = lbl_JH_Name.Text;
                            txt_JH_AJ_Secret.Text = GetMaxSecretById(objid);
                            txt_JH_AJ_User.Text = UserHelper.GetInstance().User.RealName;
                            txt_JH_AJ_Unit.Text = UserHelper.GetInstance().User.Company;
                        }
                    }
                }
                else if(index == 3)
                {
                    if(objid != null)
                    {
                        object code = SqlHelper.ExecuteOnlyOneQuery($"SELECT pt_code FROM processing_tag WHERE pt_obj_id='{objid}'") ?? string.Empty;
                        object name = SqlHelper.ExecuteOnlyOneQuery($"SELECT pt_name FROM processing_tag WHERE pt_obj_id='{objid}'") ?? string.Empty;
                        lbl_JH_AJ_Code.Text = GetValue(code);
                        lbl_JH_AJ_Name.Text = GetValue(name);
                    }
                    LoadBoxList(dgv_JH_FileList.Tag, ControlType.Plan_Project);
                    LoadFileBoxTable(cbo_JH_Box.SelectedValue, dgv_JH_FileList.Tag, ControlType.Plan_Project);
                }
            }
            else if("tab_JH_XM_FileInfo".Equals(tab.Name))
            {
                object objid = dgv_JH_XM_FileList.Tag;
                btn_JH_XM_AddFile.Visible = false;
                if(index == 0)
                    btn_JH_XM_AddFile.Visible = true;
                else if(index == 1)//文件核查
                {
                    if(objid != null)
                    {
                        LoadFileValidList(dgv_JH_XM_FileValid, objid, "dgv_jh_xm_");
                    }
                }
                else if(index == 2)//案卷信息
                {
                    if(objid != null)
                    {
                        DataTable dataTable = SqlHelper.ExecuteQuery($"SELECT * FROM processing_tag WHERE pt_obj_id='{objid}'");
                        if(dataTable.Rows.Count > 0)
                        {
                            DataRow row = dataTable.Rows[0];
                            txt_JH_XM_AJ_Code.Tag = GetValue(row["pt_id"]);
                            txt_JH_XM_AJ_Code.Text = GetValue(row["pt_code"]);
                            txt_JH_XM_AJ_Name.Text = GetValue(row["pt_name"]);
                            txt_JH_XM_AJ_Term.Text = GetValue(row["pt_term"]);
                            txt_JH_XM_AJ_Secret.Text = GetValue(row["pt_secret"]);
                            txt_JH_XM_AJ_User.Text = GetValue(row["pt_user"]);
                            txt_JH_XM_AJ_Unit.Text = GetValue(row["pt_unit"]);
                        }
                        else
                        {
                            txt_JH_XM_AJ_Code.Text = $"{planCode}-{DateTime.Now.Year}-{GetAJAmount(planCode)}";
                            txt_JH_XM_AJ_Name.Text = txt_Project_Name.Text;
                            txt_JH_XM_AJ_Secret.Text = GetMaxSecretById(objid);
                            txt_JH_XM_AJ_User.Text = UserHelper.GetInstance().User.RealName;
                            txt_JH_XM_AJ_Unit.Text = UserHelper.GetInstance().User.Company;
                        }
                    }
                }
                else if(index == 3)
                {
                    if(objid != null)
                    {
                        object code = SqlHelper.ExecuteOnlyOneQuery($"SELECT pt_code FROM processing_tag WHERE pt_obj_id='{objid}'") ?? string.Empty;
                        object name = SqlHelper.ExecuteOnlyOneQuery($"SELECT pt_name FROM processing_tag WHERE pt_obj_id='{objid}'") ?? string.Empty;
                        lbl_JH_XM_AJ_Code.Text = GetValue(code);
                        lbl_JH_XM_AJ_Name.Text = GetValue(name);
                    }
                    LoadBoxList(dgv_JH_XM_FileList.Tag, ControlType.Plan_Project);
                    LoadFileBoxTable(cbo_JH_XM_Box.SelectedValue, dgv_JH_XM_FileList.Tag, ControlType.Plan_Project);
                }
            }
            else if("tab_JH_KT_FileInfo".Equals(tab.Name))
            {
                object objid = dgv_JH_KT_FileList.Tag;
                btn_JH_KT_AddFile.Visible = false;
                if(index == 0)
                    btn_JH_KT_AddFile.Visible = true;
                else if(index == 1)//文件核查
                {
                    if(objid != null)
                    {
                        LoadFileValidList(dgv_JH_KT_FileValid, objid, "dgv_jh_kt_");
                    }
                }
                else if(index == 2)//案卷信息
                {
                    if(objid != null)
                    {
                        DataTable dataTable = SqlHelper.ExecuteQuery($"SELECT * FROM processing_tag WHERE pt_obj_id='{objid}'");
                        if(dataTable.Rows.Count > 0)
                        {
                            DataRow row = dataTable.Rows[0];
                            txt_JH_KT_AJ_Code.Tag = GetValue(row["pt_id"]);
                            txt_JH_KT_AJ_Code.Text = GetValue(row["pt_code"]);
                            txt_JH_KT_AJ_Name.Text = GetValue(row["pt_name"]);
                            txt_JH_KT_AJ_Term.Text = GetValue(row["pt_term"]);
                            txt_JH_KT_AJ_Secret.Text = GetValue(row["pt_secret"]);
                            txt_JH_KT_AJ_User.Text = GetValue(row["pt_user"]);
                            txt_JH_KT_AJ_Unit.Text = GetValue(row["pt_unit"]);
                        }
                        else
                        {
                            txt_JH_KT_AJ_Code.Text = $"{planCode}-{DateTime.Now.Year}-{GetAJAmount(planCode)}";
                            txt_JH_KT_AJ_Name.Text = txt_Topic_Name.Text;
                            txt_JH_KT_AJ_Secret.Text = GetMaxSecretById(objid);
                            txt_JH_KT_AJ_User.Text = UserHelper.GetInstance().User.RealName;
                            txt_JH_KT_AJ_Unit.Text = UserHelper.GetInstance().User.Company;
                        }
                    }
                }
                else if(index == 3)
                {
                    if(objid != null)
                    {
                        object code = SqlHelper.ExecuteOnlyOneQuery($"SELECT pt_code FROM processing_tag WHERE pt_obj_id='{objid}'") ?? string.Empty;
                        object name = SqlHelper.ExecuteOnlyOneQuery($"SELECT pt_name FROM processing_tag WHERE pt_obj_id='{objid}'") ?? string.Empty;
                        lbl_JH_KT_AJ_Code.Text = GetValue(code);
                        lbl_JH_KT_AJ_Name.Text = GetValue(name);
                    }
                    LoadBoxList(dgv_JH_KT_FileList.Tag, ControlType.Plan_Topic);
                    LoadFileBoxTable(cbo_JH_KT_Box.SelectedValue, dgv_JH_KT_FileList.Tag, ControlType.Plan_Topic);
                }
            }
            else if("tab_JH_XM_KT_ZKT_FileInfo".Equals(tab.Name))
            {
                object objid = dgv_JH_XM_KT_ZKT_FileList.Tag;
                btn_JH_XM_KT_ZKT_AddFile.Visible = false;
                if(index == 0)
                    btn_JH_XM_KT_ZKT_AddFile.Visible = true;
                else if(index == 1)//文件核查
                {
                    if(objid != null)
                    {
                        LoadFileValidList(dgv_JH_XM_KT_ZKT_FileValid, objid, "dgv_jh_xm_kt_zkt_");
                    }
                }
                else if(index == 2)//案卷信息
                {
                    if(objid != null)
                    {
                        DataTable dataTable = SqlHelper.ExecuteQuery($"SELECT * FROM processing_tag WHERE pt_obj_id='{objid}'");
                        if(dataTable.Rows.Count > 0)
                        {
                            DataRow row = dataTable.Rows[0];
                            txt_JH_XM_KT_ZKT_AJ_Code.Tag = GetValue(row["pt_id"]);
                            txt_JH_XM_KT_ZKT_AJ_Code.Text = GetValue(row["pt_code"]);
                            txt_JH_XM_KT_ZKT_AJ_Name.Text = GetValue(row["pt_name"]);
                            txt_JH_XM_KT_ZKT_AJ_Term.Text = GetValue(row["pt_term"]);
                            txt_JH_XM_KT_ZKT_AJ_Secret.Text = GetValue(row["pt_secret"]);
                            txt_JH_XM_KT_ZKT_AJ_User.Text = GetValue(row["pt_user"]);
                            txt_JH_XM_KT_ZKT_AJ_Unit.Text = GetValue(row["pt_unit"]);
                        }
                        else
                        {
                            txt_JH_XM_KT_ZKT_AJ_Code.Text = $"{planCode}-{DateTime.Now.Year}-{GetAJAmount(planCode)}";
                            txt_JH_XM_KT_ZKT_AJ_Name.Text = txt_Subject_Name.Text;
                            txt_JH_XM_KT_ZKT_AJ_Secret.Text = GetMaxSecretById(objid);
                            txt_JH_XM_KT_ZKT_AJ_User.Text = UserHelper.GetInstance().User.RealName;
                            txt_JH_XM_KT_ZKT_AJ_Unit.Text = UserHelper.GetInstance().User.Company;
                        }
                    }
                }
                else if(index == 3)
                {
                    if(objid != null)
                    {
                        object code = SqlHelper.ExecuteOnlyOneQuery($"SELECT pt_code FROM processing_tag WHERE pt_obj_id='{objid}'") ?? string.Empty;
                        object name = SqlHelper.ExecuteOnlyOneQuery($"SELECT pt_name FROM processing_tag WHERE pt_obj_id='{objid}'") ?? string.Empty;
                        lbl_JH_XM_KT_ZKT_AJ_Code.Text = GetValue(code);
                        lbl_JH_XM_KT_ZKT_AJ_Name.Text = GetValue(name);
                    }
                    LoadBoxList(dgv_JH_XM_KT_ZKT_FileList.Tag, ControlType.Plan_Project_Topic_Subtopic);
                    LoadFileBoxTable(cbo_JH_XM_KT_ZKT_Box.SelectedValue, dgv_JH_XM_KT_ZKT_FileList.Tag, ControlType.Plan_Project_Topic_Subtopic);
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
                        LoadFileValidList(dgv_Imp_FileValid, objid, "dgv_imp_");
                    }
                }
                else if(index == 2)//案卷信息
                {
                    if(objid != null)
                    {
                        DataTable dataTable = SqlHelper.ExecuteQuery($"SELECT * FROM processing_tag WHERE pt_obj_id='{objid}'");
                        if(dataTable.Rows.Count > 0)
                        {
                            DataRow row = dataTable.Rows[0];
                            txt_Imp_AJ_Code.Tag = GetValue(row["pt_id"]);
                            txt_Imp_AJ_Code.Text = GetValue(row["pt_code"]);
                            txt_Imp_AJ_Name.Text = GetValue(row["pt_name"]);
                            txt_Imp_AJ_Term.Text = GetValue(row["pt_term"]);
                            txt_Imp_AJ_Secret.Text = GetValue(row["pt_secret"]);
                            txt_Imp_AJ_User.Text = GetValue(row["pt_user"]);
                            txt_Imp_AJ_Unit.Text = GetValue(row["pt_unit"]);
                        }
                        else
                        {
                            txt_Imp_AJ_Code.Text = $"{planCode}-{DateTime.Now.Year}-{GetAJAmount(planCode)}";
                            txt_Imp_AJ_Name.Text = lbl_Imp_Name.Text;
                            txt_Imp_AJ_Secret.Text = GetMaxSecretById(objid);
                            txt_Imp_AJ_User.Text = UserHelper.GetInstance().User.RealName;
                            txt_Imp_AJ_Unit.Text = UserHelper.GetInstance().User.Company;
                        }
                    }
                }
                else if(index == 3)
                {
                    if(objid != null)
                    {
                        object code = SqlHelper.ExecuteOnlyOneQuery($"SELECT pt_code FROM processing_tag WHERE pt_obj_id='{objid}'") ?? string.Empty;
                        object name = SqlHelper.ExecuteOnlyOneQuery($"SELECT pt_name FROM processing_tag WHERE pt_obj_id='{objid}'") ?? string.Empty;
                        lbl_Imp_AJ_Code.Text = GetValue(code);
                        lbl_Imp_AJ_Name.Text = GetValue(name);
                    }
                    LoadBoxList(dgv_Imp_FileList.Tag, ControlType.Imp);
                    LoadFileBoxTable(cbo_Imp_Box.SelectedValue, dgv_Imp_FileList.Tag, ControlType.Imp);
                }
            }
            else if("tab_Imp_Dev_FileInfo".Equals(tab.Name))
            {
                object objid = dgv_Imp_Dev_FileList.Tag;
                btn_Imp_Dev_AddFile.Visible = false;
                if(index == 0)
                    btn_Imp_Dev_AddFile.Visible = true;
                else if(index == 1)//文件核查
                {
                    if(objid != null)
                    {
                        LoadFileValidList(dgv_Imp_Dev_FileValid, objid, "dgv_imp_dev_");
                    }
                }
                else if(index == 2)//案卷信息
                {
                    if(objid != null)
                    {
                        DataTable dataTable = SqlHelper.ExecuteQuery($"SELECT * FROM processing_tag WHERE pt_obj_id='{objid}'");
                        if(dataTable.Rows.Count > 0)
                        {
                            DataRow row = dataTable.Rows[0];
                            txt_Imp_Dev_AJ_Code.Tag = GetValue(row["pt_id"]);
                            txt_Imp_Dev_AJ_Code.Text = GetValue(row["pt_code"]);
                            txt_Imp_Dev_AJ_Name.Text = GetValue(row["pt_name"]);
                            txt_Imp_Dev_AJ_Term.Text = GetValue(row["pt_term"]);
                            txt_Imp_Dev_AJ_Secret.Text = GetValue(row["pt_secret"]);
                            txt_Imp_Dev_AJ_User.Text = GetValue(row["pt_user"]);
                            txt_Imp_Dev_AJ_Unit.Text = GetValue(row["pt_unit"]);
                        }
                        else
                        {
                            txt_Imp_Dev_AJ_Code.Text = $"{planCode}-{DateTime.Now.Year}-{GetAJAmount(planCode)}";
                            txt_Imp_Dev_AJ_Name.Text = txt_Imp_Dev_Name.Text;
                            txt_Imp_Dev_AJ_Secret.Text = GetMaxSecretById(objid);
                            txt_Imp_Dev_AJ_User.Text = UserHelper.GetInstance().User.RealName;
                            txt_Imp_Dev_AJ_Unit.Text = UserHelper.GetInstance().User.Company;
                        }
                    }
                }
                else if(index == 3)
                {
                    if(objid != null)
                    {
                        object code = SqlHelper.ExecuteOnlyOneQuery($"SELECT pt_code FROM processing_tag WHERE pt_obj_id='{objid}'") ?? string.Empty;
                        object name = SqlHelper.ExecuteOnlyOneQuery($"SELECT pt_name FROM processing_tag WHERE pt_obj_id='{objid}'") ?? string.Empty;
                        lbl_Imp_Dev_AJ_Code.Text = GetValue(code);
                        lbl_Imp_Dev_AJ_Name.Text = GetValue(name);
                    }
                    LoadBoxList(dgv_Imp_Dev_FileList.Tag, ControlType.Imp_Dev);
                    LoadFileBoxTable(cbo_Imp_Dev_Box.SelectedValue, dgv_Imp_Dev_FileList.Tag, ControlType.Imp_Dev);
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
            if(type == ControlType.Plan_Project)
            {
                txt_JH_Box_GCID.Text = GCID;
                LoadFileBoxTableInstance(lsv_JH_File1,lsv_JH_File2, "jh", pbId, objId);
            }
            else if(type == ControlType.Plan_Project)
            {
                txt_JH_XM_Box_GCID.Text = GCID;
                LoadFileBoxTableInstance(lsv_JH_XM_File1, lsv_JH_XM_File2, "jh_xm", pbId, objId);
            }
            else if(type == ControlType.Plan_Topic)
            {
                txt_JH_KT_Box_GCID.Text = GCID;
                LoadFileBoxTableInstance(lsv_JH_KT_File1, lsv_JH_KT_File2, "jh_kt", pbId, objId);
            }
            else if(type == ControlType.Plan_Project_Topic_Subtopic)
            {
                txt_JH_XM_KT_ZKT_Box_GCID.Text = GCID;
                LoadFileBoxTableInstance(lsv_JH_XM_KT_ZKT_File1, lsv_JH_XM_KT_ZKT_File2, "jh_xm_kt_zkt", pbId, objId);
            }
            else if(type == ControlType.Imp)
            {
                txt_Imp_Box_GCID.Text = GCID;
                LoadFileBoxTableInstance(lsv_Imp_File1, lsv_Imp_File2, "imp", pbId, objId);
            }
            else if(type == ControlType.Imp_Dev)
            {
                txt_Imp_Dev_Box_GCID.Text = GCID;
                LoadFileBoxTableInstance(lsv_Imp_Dev_File1, lsv_Imp_Dev_File2, "imp_dev", pbId, objId);
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
                    LoadFileBoxTable(value, dgv_JH_FileList.Tag, ControlType.Plan_Project);
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
                    LoadFileBoxTable(value, dgv_JH_XM_FileList.Tag, ControlType.Plan_Project);
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
                    LoadFileBoxTable(value, dgv_JH_KT_FileList.Tag, ControlType.Plan_Topic);
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
                    LoadFileBoxTable(value, dgv_JH_XM_KT_ZKT_FileList.Tag, ControlType.Plan_Project_Topic_Subtopic);
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
                    LoadFileBoxTable(value, dgv_Imp_Dev_FileList.Tag, ControlType.Imp_Dev);
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
                object objId = dgv_JH_FileList.Tag;
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
                    LoadBoxList(objId, ControlType.Plan_Project);
                    LoadFileBoxTable(cbo_JH_Box.SelectedValue, objId, ControlType.Plan_Project);
                }
            }
            //计划-项目
            if(label.Name.Contains("lbl_JH_XM_Box"))
            {
                object objId = dgv_JH_XM_FileList.Tag;
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
                    LoadBoxList(objId, ControlType.Plan_Project);
                    LoadFileBoxTable(cbo_JH_XM_Box.SelectedValue, objId, ControlType.Plan_Project);
                }
            }
            //计划-项目-课题-子课题
            if(label.Name.Contains("lbl_JH_XM_KT_ZKT_Box"))
            {
                object objId = dgv_JH_XM_KT_ZKT_FileList.Tag;
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
                    LoadBoxList(objId, ControlType.Plan_Project_Topic_Subtopic);
                    LoadFileBoxTable(cbo_JH_XM_KT_ZKT_Box.SelectedValue, objId, ControlType.Plan_Project_Topic_Subtopic);
                }
            }
            //计划-课题
            if(label.Name.Contains("lbl_JH_KT_Box"))
            {
                object objId = dgv_JH_KT_FileList.Tag;
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
                    LoadBoxList(objId, ControlType.Plan_Topic);
                    LoadFileBoxTable(cbo_JH_KT_Box.SelectedValue, objId, ControlType.Plan_Topic);
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
                object objId = dgv_Imp_Dev_FileList.Tag;
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
                    LoadBoxList(objId, ControlType.Imp_Dev);
                    LoadFileBoxTable(cbo_Imp_Dev_Box.SelectedValue, objId, ControlType.Imp_Dev);
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
            if(type == ControlType.Plan_Project)
            {
                cbo_JH_Box.DataSource = table;
                cbo_JH_Box.DisplayMember = "pb_box_number";
                cbo_JH_Box.ValueMember = "pb_id";
                if(table.Rows.Count > 0)
                    cbo_JH_Box.SelectedIndex = 0;
            }
            else if(type == ControlType.Plan_Project)
            {
                cbo_JH_XM_Box.DataSource = table;
                cbo_JH_XM_Box.DisplayMember = "pb_box_number";
                cbo_JH_XM_Box.ValueMember = "pb_id";
                if(table.Rows.Count > 0)
                    cbo_JH_XM_Box.SelectedIndex = 0;
            }
            else if(type == ControlType.Plan_Topic)
            {
                cbo_JH_KT_Box.DataSource = table;
                cbo_JH_KT_Box.DisplayMember = "pb_box_number";
                cbo_JH_KT_Box.ValueMember = "pb_id";
                if(table.Rows.Count > 0)
                    cbo_JH_KT_Box.SelectedIndex = 0;
            }
            else if(type == ControlType.Plan_Project_Topic_Subtopic)
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
            else if(type == ControlType.Imp_Dev)
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
                LoadFileBoxTable(pbId, dgv_JH_FileList.Tag, ControlType.Plan_Project);
            }
            else if("cbo_JH_XM_Box".Equals(comboBox.Name))
            {
                object pbId = comboBox.SelectedValue;
                LoadFileBoxTable(pbId, dgv_JH_XM_FileList.Tag, ControlType.Plan_Project);
            }
            else if("cbo_JH_KT_Box".Equals(comboBox.Name))
            {
                object pbId = comboBox.SelectedValue;
                LoadFileBoxTable(pbId, dgv_JH_KT_FileList.Tag, ControlType.Plan_Topic);
            }
            else if("cbo_JH_XM_KT_ZKT_Box".Equals(comboBox.Name))
            {
                object pbId = comboBox.SelectedValue;
                LoadFileBoxTable(pbId, dgv_JH_XM_KT_ZKT_FileList.Tag, ControlType.Plan_Project_Topic_Subtopic);
            }
            else if("cbo_Imp_Box".Equals(comboBox.Name))
            {
                object pbId = comboBox.SelectedValue;
                LoadFileBoxTable(pbId, dgv_Imp_FileList.Tag, ControlType.Imp);
            }
            else if("cbo_Imp_Dev_Box".Equals(comboBox.Name))
            {
                object pbId = comboBox.SelectedValue;
                LoadFileBoxTable(pbId, dgv_Imp_Dev_FileList.Tag, ControlType.Imp_Dev);
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
                        dgv_JH_XM_FileList.ColumnHeadersDefaultCellStyle = DataGridViewStyleHelper.GetHeaderStyle();
                        dgv_JH_XM_FileList.DefaultCellStyle = DataGridViewStyleHelper.GetCellStyle();
                        dgv_JH_XM_FileValid.ColumnHeadersDefaultCellStyle = DataGridViewStyleHelper.GetHeaderStyle();
                        dgv_JH_XM_FileValid.DefaultCellStyle = DataGridViewStyleHelper.GetCellStyle();
                    }
                }
                else if("plan_project_topic_subtopic".Equals(currentPageName))
                {
                    if(string.IsNullOrEmpty(txt_Subject_Name.Text))
                    {
                        dgv_JH_XM_KT_ZKT_FileList.ColumnHeadersDefaultCellStyle = DataGridViewStyleHelper.GetHeaderStyle();
                        dgv_JH_XM_KT_ZKT_FileList.DefaultCellStyle = DataGridViewStyleHelper.GetCellStyle();
                        dgv_JH_XM_KT_ZKT_FileValid.ColumnHeadersDefaultCellStyle = DataGridViewStyleHelper.GetHeaderStyle();
                        dgv_JH_XM_KT_ZKT_FileValid.DefaultCellStyle = DataGridViewStyleHelper.GetCellStyle();
                    }
                }
                else if("plan_topic".Equals(currentPageName))
                {
                    if(string.IsNullOrEmpty(txt_Topic_Name.Text))
                    {
                        dgv_JH_KT_FileList.ColumnHeadersDefaultCellStyle = DataGridViewStyleHelper.GetHeaderStyle();
                        dgv_JH_KT_FileList.DefaultCellStyle = DataGridViewStyleHelper.GetCellStyle();
                        dgv_JH_KT_FileValid.ColumnHeadersDefaultCellStyle = DataGridViewStyleHelper.GetHeaderStyle();
                        dgv_JH_KT_FileValid.DefaultCellStyle = DataGridViewStyleHelper.GetCellStyle();
                    }
                }
                else if("imp".Equals(currentPageName))
                {

                }
                else if("imp_dev".Equals(currentPageName))
                {
                    if(string.IsNullOrEmpty(txt_Imp_Dev_Name.Text))
                    {
                        dgv_Imp_Dev_FileList.ColumnHeadersDefaultCellStyle = DataGridViewStyleHelper.GetHeaderStyle();
                        dgv_Imp_Dev_FileList.DefaultCellStyle = DataGridViewStyleHelper.GetCellStyle();
                        dgv_Imp_Dev_FileValid.ColumnHeadersDefaultCellStyle = DataGridViewStyleHelper.GetHeaderStyle();
                        dgv_Imp_Dev_FileValid.DefaultCellStyle = DataGridViewStyleHelper.GetCellStyle();
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
            if(type == ControlType.Plan_Project)
            {
                DataTable table = SqlHelper.ExecuteQuery($"SELECT * FROM project_info WHERE pi_id='{projectId}'");
                if(table.Rows.Count > 0)
                {
                    DataRow row = table.Rows[0];
                    dgv_JH_XM_FileList.Tag = row["pi_id"];
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
                LoadFileList(dgv_JH_XM_FileList, "jh_xm_", projectId);

                dgv_JH_XM_FileList.ColumnHeadersDefaultCellStyle = DataGridViewStyleHelper.GetHeaderStyle();
                dgv_JH_XM_FileList.DefaultCellStyle = DataGridViewStyleHelper.GetCellStyle();
                dgv_JH_XM_FileValid.ColumnHeadersDefaultCellStyle = DataGridViewStyleHelper.GetHeaderStyle();
                dgv_JH_XM_FileValid.DefaultCellStyle = DataGridViewStyleHelper.GetCellStyle();

                pal_JH_XM_BtnGroup.Enabled = !(color == DisEnbleColor);
                tab_MenuList.TabPages["plan_project"].Tag = projectId;

                if(isBacked)
                {
                    btn_JH_XM_QTReason.Text = $"质检意见({GetAdvincesAmount(dgv_JH_XM_FileList.Tag)})";
                }
            }
            else if(type == ControlType.Plan_Topic)
            {
                DataTable table = SqlHelper.ExecuteQuery($"SELECT * FROM project_info WHERE pi_id='{projectId}'");
                if(table.Rows.Count > 0)
                {
                    DataRow row = table.Rows[0];
                    dgv_JH_KT_FileList.Tag = row["pi_id"];
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
                LoadFileList(dgv_JH_KT_FileList, "jh_kt_", projectId);

                dgv_JH_KT_FileList.ColumnHeadersDefaultCellStyle = DataGridViewStyleHelper.GetHeaderStyle();
                dgv_JH_KT_FileList.DefaultCellStyle = DataGridViewStyleHelper.GetCellStyle();
                dgv_JH_KT_FileValid.ColumnHeadersDefaultCellStyle = DataGridViewStyleHelper.GetHeaderStyle();
                dgv_JH_KT_FileValid.DefaultCellStyle = DataGridViewStyleHelper.GetCellStyle();

                pal_JH_KT_BtnGroup.Enabled = !(color == DisEnbleColor);
                tab_MenuList.TabPages["plan_topic"].Tag = projectId;

                if(isBacked)
                {
                    btn_JH_KT_QTReason.Text = $"质检意见({GetAdvincesAmount(dgv_JH_KT_FileList.Tag)})";
                }
            }
            else if(type == ControlType.Plan_Project_Topic_Subtopic)
            {
                DataTable table = SqlHelper.ExecuteQuery($"SELECT * FROM subject_info WHERE si_id='{projectId}'");
                if(table.Rows.Count > 0)
                {
                    DataRow row = table.Rows[0];
                    dgv_JH_XM_KT_ZKT_FileList.Tag = row["si_id"];
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
                LoadFileList(dgv_JH_XM_KT_ZKT_FileList, "jh_xm_kt_zkt_", projectId);

                dgv_JH_XM_KT_ZKT_FileList.ColumnHeadersDefaultCellStyle = DataGridViewStyleHelper.GetHeaderStyle();
                dgv_JH_XM_KT_ZKT_FileList.DefaultCellStyle = DataGridViewStyleHelper.GetCellStyle();
                dgv_JH_XM_KT_ZKT_FileValid.ColumnHeadersDefaultCellStyle = DataGridViewStyleHelper.GetHeaderStyle();
                dgv_JH_XM_KT_ZKT_FileValid.DefaultCellStyle = DataGridViewStyleHelper.GetCellStyle();

                pal_JH_XM_KT_ZKT_BtnGroup.Enabled = !(color == DisEnbleColor);
                tab_MenuList.TabPages["plan_project_topic_subtopic"].Tag = projectId;
                if(isBacked)
                {
                    btn_JH_XM_KT_ZKT_QTReason.Text = $"质检意见({GetAdvincesAmount(dgv_JH_XM_KT_ZKT_FileList.Tag)})";
                }
            }
            else if(type == ControlType.Imp_Sub)
            {
                object[] _obj = SqlHelper.ExecuteRowsQuery($"SELECT imp_id,imp_code,imp_name,imp_unit,imp_intro,pi_categor,imp_submit_status FROM imp_dev_info WHERE imp_id='{projectId}'");
                if(_obj != null)
                {
                    txt_Imp_Dev_Code.Text = GetValue(_obj[1]);
                    txt_Imp_Dev_Name.Text = GetValue(_obj[2]);
                    txt_Imp_Dev_Unit.Text = GetValue(_obj[3]);
                    dgv_Imp_Dev_FileList.Tag = GetValue(_obj[0]);
                    LoadFileList(dgv_Imp_Dev_FileList, "imp_dev_", GetValue(_obj[0]));
                    EnableControls(ControlType.Imp_Dev, (ObjectSubmitStatus)_obj[6] != ObjectSubmitStatus.SubmitSuccess);
                }
                else
                {
                    object[] obj = SqlHelper.ExecuteRowsQuery($"SELECT dd_id,dd_code,dd_name FROM data_dictionary WHERE dd_id='{projectId}'");
                    if(obj != null)
                    {
                        txt_Imp_Dev_Name.Text = GetValue(obj[2]);
                    }
                }
                if(DEV_TYPE == 1)
                    tab_MenuList.TabPages["imp_dev"].Text = "研发信息";
                cbo_Imp_Dev_HasNext.SelectedIndex = 0;
                dgv_Imp_Dev_FileList.ColumnHeadersDefaultCellStyle = DataGridViewStyleHelper.GetHeaderStyle();
                dgv_Imp_Dev_FileList.DefaultCellStyle = DataGridViewStyleHelper.GetCellStyle();
                dgv_Imp_Dev_FileValid.ColumnHeadersDefaultCellStyle = DataGridViewStyleHelper.GetHeaderStyle();
                dgv_Imp_Dev_FileValid.DefaultCellStyle = DataGridViewStyleHelper.GetCellStyle();

                pal_Imp_Dev_BtnGroup.Enabled = !(color == DisEnbleColor);
                tab_MenuList.TabPages["imp_dev"].Tag = projectId;

                if(isBacked)
                {
                    btn_Imp_Dev_QTReason.Text = $"质检意见({GetAdvincesAmount(dgv_Imp_Dev_FileList.Tag)})";
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
                ResetControls(ControlType.Plan_Project);
            else if("btn_JH_KT_Add".Equals(button.Name))
                ResetControls(ControlType.Plan_Topic);
            else if("btn_JH_KT_ZKT_Add".Equals(button.Name))
                ResetControls(ControlType.Plan_Topic_Subtopic);
            else if("btn_JH_XM_KT_Add".Equals(button.Name))
                ResetControls(ControlType.Plan_Project_Topic);
            else if("btn_JH_XM_KT_ZKT_Add".Equals(button.Name))
                ResetControls(ControlType.Plan_Project_Topic_Subtopic);
        }
        /// <summary>
        /// 重置控件为默认状态
        /// </summary>
        /// <param name="type">对象类型</param>
        void ResetControls(ControlType type)
        {
            if(type == ControlType.Plan_Project)
            {
                dgv_JH_XM_FileList.Tag = null;
                DataGridViewStyleHelper.ResetDataGridView(dgv_JH_XM_FileList, false);
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
            else if(type == ControlType.Plan_Topic)
            {
                dgv_JH_KT_FileList.Tag = null;
                DataGridViewStyleHelper.ResetDataGridView(dgv_JH_KT_FileList, false);
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
            else if(type == ControlType.Plan_Project_Topic_Subtopic)
            {
                dgv_JH_XM_KT_ZKT_FileList.Tag = null;
                DataGridViewStyleHelper.ResetDataGridView(dgv_JH_XM_KT_ZKT_FileList, false);
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
            else if(type == ControlType.Imp_Dev)
            {
                DataGridViewStyleHelper.ResetDataGridView(dgv_Imp_Dev_FileList, false);
                DataGridViewStyleHelper.ResetDataGridView(dgv_Imp_Dev_FileValid, false);
                txt_Imp_Dev_Code.Clear();
                txt_Imp_Dev_Name.Clear();
                txt_Imp_Dev_Unit.Clear();
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
            if(type == ControlType.Plan_Project)
            {
                //tab_JH_FileInfo.Enabled = pal_JH_BasicInfo.Enabled = enable;
                foreach(Control item in pal_JH_BtnGroup.Controls)
                {
                    item.Enabled = enable;
                    if(item.Name.Contains("Submit"))
                        item.Text = enable ? "提交" : "已提交";
                }
            }
            else if(type == ControlType.Plan_Project)
            {
                //tab_JH_XM_FileInfo.Enabled = pal_JH_XM.Enabled = enable;
                foreach(Control item in pal_JH_XM_BtnGroup.Controls)
                {
                    item.Enabled = enable;
                    if(item.Name.Contains("Submit"))
                        item.Text = enable ? "提交" : "已提交";
                }
            }
            else if(type == ControlType.Plan_Project_Topic_Subtopic)
            {
                //tab_JH_XM_KT_ZKT_FileInfo.Enabled = pal_JH_XM_KT_ZKT.Enabled = enable;
                foreach(Control item in pal_JH_XM_KT_ZKT_BtnGroup.Controls)
                {
                    item.Enabled = enable;
                    if(item.Name.Contains("Submit"))
                        item.Text = enable ? "提交" : "已提交";
                }
            }
            else if(type == ControlType.Plan_Topic)
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
            else if(type == ControlType.Imp_Dev)
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
        /// 删除事件
        /// </summary>
        private void Btn_Delete_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;
            if("btn_JH_XM_Delete".Equals(button.Name))
            {
                object objId = "";
                if(MessageBox.Show("此操作会删除其下所关联的所有数据，是否确定删除？", "删除确认", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    
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
                    objId = dgv_JH_FileList.Tag;
                    if(objId != null)
                    {
                        SqlHelper.ExecuteNonQuery($"UPDATE project_info SET pi_submit_status='{(int)SubmitStatus.SubmitSuccess}' WHERE pi_id='{objId}'");
                        EnableControls(ControlType.Plan_Project, false);
                    }
                    else
                        MessageBox.Show("请先保存数据.");
                }
                else if("btn_JH_XM_Submit".Equals(button.Name))
                {
                    objId = dgv_JH_XM_FileList.Tag;
                    if(objId != null)
                    {
                        SqlHelper.ExecuteNonQuery($"UPDATE project_info SET pi_submit_status={(int)SubmitStatus.SubmitSuccess} WHERE pi_id='{objId}'");
                        EnableControls(ControlType.Plan_Project, false);
                    }
                    else
                        MessageBox.Show("请先保存数据.");
                }
                else if("btn_JH_KT_Submit".Equals(button.Name))
                {
                    objId = dgv_JH_KT_FileList.Tag;
                    if(objId != null)
                    {
                        SqlHelper.ExecuteNonQuery($"UPDATE project_info SET pi_submit_status='{(int)SubmitStatus.SubmitSuccess}' WHERE pi_id='{objId}'");
                        EnableControls(ControlType.Plan_Topic, false);
                    }
                    else
                        MessageBox.Show("请先保存数据.");
                }
                else if("btn_JH_XM_KT_ZKT_Submit".Equals(button.Name))
                {
                    objId = dgv_JH_XM_KT_ZKT_FileList.Tag;
                    if(objId != null)
                    {
                        SqlHelper.ExecuteNonQuery($"UPDATE subject_info SET si_submit_status='{(int)SubmitStatus.SubmitSuccess}' WHERE si_id='{objId}'");
                        EnableControls(ControlType.Plan_Project_Topic_Subtopic, false);
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
                    objId = dgv_Imp_Dev_FileList.Tag;
                    if(objId != null)
                    {
                        SqlHelper.ExecuteNonQuery($"UPDATE imp_dev_info SET imp_submit_status='{(int)ObjectSubmitStatus.SubmitSuccess}' WHERE imp_id='{objId}'");
                        EnableControls(ControlType.Imp_Dev, false);
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
                int _index = tab_MenuList.SelectedIndex;
                ShowTab("imp_dev", _index + 1);
                ResetControls(ControlType.Imp_Dev);

                object value = cbo_Imp_HasNext.SelectedValue;
                object[] _obj = SqlHelper.ExecuteRowsQuery($"SELECT dd_code, dd_name FROM data_dictionary WHERE dd_id='{value}'");
                if(_obj.Length > 0)
                {
                    txt_Imp_Dev_Code.Text = GetValue(_obj[0]);
                    txt_Imp_Dev_Name.Text = GetValue(_obj[1]);
                }
                pal_Imp_Dev.Tag = id;
                dgv_Imp_Dev_FileList.Tag = null;

                dgv_Imp_Dev_FileList.ColumnHeadersDefaultCellStyle = DataGridViewStyleHelper.GetHeaderStyle();
                dgv_Imp_Dev_FileList.DefaultCellStyle = DataGridViewStyleHelper.GetCellStyle();
                dgv_Imp_Dev_FileValid.ColumnHeadersDefaultCellStyle = DataGridViewStyleHelper.GetHeaderStyle();
                dgv_Imp_Dev_FileValid.DefaultCellStyle = DataGridViewStyleHelper.GetCellStyle();

                if(DEV_TYPE == 1)
                    tab_MenuList.TabPages["imp_dev"].Text = "研发信息";
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
            DataGridView view = sender as DataGridView;
            string columnName = view.CurrentCell.OwningColumn.Name;
            if(columnName.Contains("link"))
            {
                string path = GetValue(view.CurrentCell.Value);
                if(!string.IsNullOrEmpty(path))
                {
                    if(File.Exists(path))
                    {
                        if(MessageBox.Show("是否打开此文件？", "温馨提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            System.Diagnostics.Process.Start("Explorer.exe", path);
                    }
                    else
                    {
                        MessageBox.Show("文件不存在。");
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
            DataGridView view = null;
            string key = null;
            object name = (sender as KyoButton).Name;
            if("btn_JH_AddFile".Equals(name))
            {
                view = dgv_JH_FileList;
                if(view.Tag != null)
                    key = string.Empty;
            }
            else if("btn_JH_XM_AddFile".Equals(name))
            {
                view = dgv_JH_XM_FileList;
                if(view.Tag != null)
                    key = "jh_xm_";
            }
            else if("btn_JH_XM_KT_ZKT_AddFile".Equals(name))
            {
                view = dgv_JH_XM_KT_ZKT_FileList;
                if(view.Tag != null)
                    key = "jh_xm_kt_zkt_";
            }
            else if("btn_JH_KT_AddFile".Equals(name))
            {
                view = dgv_JH_KT_FileList;
                if(view.Tag != null)
                    key = "jh_kt_";
            }
            else if("btn_Imp_AddFile".Equals(name))
            {
                view = dgv_Imp_FileList;
                if(view.Tag != null)
                    key = "imp_";
            }
            else if("btn_Imp_Dev_AddFile".Equals(name))
            {
                view = dgv_Imp_Dev_FileList;
                if(view.Tag != null)
                    key = "imp_dev_";
            }

            if(key != null)
            {
                int selectedRowCount = view.SelectedRows.Count;
                if(selectedRowCount == 1)
                    frm = new Frm_AddFile(view.SelectedRows[0], key) { parentId = view.Tag, trcId = OBJECT_ID, workType = workType };
                else
                    frm = new Frm_AddFile(view.Rows[view.Rows.Add()], key) { parentId = view.Tag, trcId = OBJECT_ID, workType = workType };
                frm.ShowDialog();
            }
            else
                MessageBox.Show("请先保存基础信息。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
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
                objId = dgv_JH_FileList.Tag;
                objName = lbl_JH_Name.Text;
            }
            else if("btn_Imp_Dev_QTReason".Equals(name))
            {
                objId = dgv_Imp_Dev_FileList.Tag;
                objName = txt_Imp_Dev_Name.Text;
            }
            else if("btn_JH_XM_QTReason".Equals(name))
            {
                objId = dgv_JH_XM_FileList.Tag;
                objName = txt_Project_Name.Text;
            }
            else if("btn_JH_XM_KT_ZKT_QTReason".Equals(name))
            {
                objId = dgv_JH_XM_KT_ZKT_FileList.Tag;
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
            object rootId = SqlHelper.ExecuteOnlyOneQuery($"SELECT bfi_id FROM backup_files_info WHERE bfi_trcid='{OBJECT_ID}' AND bfi_sort=-1");
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

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            DataGridView view = (DataGridView)(sender as ToolStripItem).GetCurrentParent().Tag;
            view.Rows.Insert(view.CurrentCell.RowIndex, 1);
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            DataGridView view = (DataGridView)(sender as ToolStripItem).GetCurrentParent().Tag;
            int index = view.CurrentCell.RowIndex;
            if(index != view.RowCount - 1)
                view.Rows.RemoveAt(index);
        }

        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            DataGridView view = (DataGridView)(sender as ToolStripItem).GetCurrentParent().Tag;
            string name = view.Parent.Parent.Name;
            string key = null;
            if("tab_JH_FileInfo".Equals(name))
                key = string.Empty;
            else if("tab_JH_XM_FileInfo".Equals(name))
                key = "jh_xm_";
            else if("tab_JH_KT_FileInfo".Equals(name))
                key = "jh_kt_";
            else if("tab_JH_XM_KT_FileInfo".Equals(name))
                key = "jh_xm_kt_";
            else if("tab_JH_KT_ZKT_FileInfo".Equals(name))
                key = "jh_kt_zkt_";
            else if("tab_JH_XM_KT_ZKT_FileInfo".Equals(name))
                key = "jh_xm_kt_zkt_";
            else if("tab_Imp_FileInfo".Equals(name))
                key = "imp_";
            else if("tab_Imp_Dev_FileInfo".Equals(name))
                key = "imp_dev_";
            if(key != null)
                LoadFileList(view, key, view.Tag);
        }

        private void Tab_FileInfo_SelectedIndexChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {

        }
    }
}
