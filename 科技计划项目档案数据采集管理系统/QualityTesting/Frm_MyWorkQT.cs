using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace 科技计划项目档案数据采集管理系统
{
    public partial class Frm_MyWorkQT : Form
    {
        /// <summary>
        /// 当前加工类型
        /// </summary>
        private WorkType workType;
        private ControlType controlType;
        /// <summary>
        /// 我的质检记录表主键
        /// </summary>
        public object WMID;
        /// <summary>
        /// 是否是质检返工
        /// </summary>
        private bool isBackWork;
        /// <summary>
        /// 节点禁用背景色
        /// </summary>
        private System.Drawing.Color DisEnableColor;
        /// <summary>
        /// 开始加工指定的对象
        /// </summary>
        /// <param name="workType">对象类型</param>
        /// <param name="planId">计划主键</param>
        /// <param name="isBackWork">是否是质检返工</param>
        public Frm_MyWorkQT(WorkType workType, object planId, ControlType controlType, bool isBackWork)
        {
            InitializeComponent();
            this.isBackWork = isBackWork;
            this.workType = workType;
            this.controlType = controlType;
            this.DisEnableColor = System.Drawing.Color.Gray;
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
        private void LoadPlanPage(object planId, System.Drawing.Color color)
        {
            object[] _obj = SqlHelper.ExecuteRowsQuery($"SELECT pi_id, pi_name, pi_obj_id FROM project_info WHERE pi_id = '{planId}'");
            if(_obj == null)
            {
                _obj = SqlHelper.ExecuteRowsQuery($"SELECT dd_id,dd_name,dd_note FROM data_dictionary WHERE dd_id='{planId}'");
                lbl_JH_Name.Tag = GetValue(_obj[0]);
                lbl_JH_Name.Text = GetValue(_obj[1]);
                lbl_PlanIntroducation.Text = GetValue(_obj[2]);
            }
            else
            {
                dgv_JH_FileList.Tag = GetValue(_obj[0]);
                lbl_JH_Name.Text = GetValue(_obj[1]);
                lbl_JH_Name.Tag = GetValue(_obj[2]);
                LoadFileList(dgv_JH_FileList, string.Empty, GetValue(_obj[0]));
            }
            dgv_JH_FileList.ColumnHeadersDefaultCellStyle = DataGridViewStyleHelper.GetHeaderStyle();
            dgv_JH_FileList.DefaultCellStyle = DataGridViewStyleHelper.GetCellStyle();
            dgv_JH_FileValid.ColumnHeadersDefaultCellStyle = DataGridViewStyleHelper.GetHeaderStyle();
            dgv_JH_FileValid.DefaultCellStyle = DataGridViewStyleHelper.GetCellStyle();

            if(controlType == ControlType.Imp_Normal)
            {
                cbo_JH_HasNext.Enabled = false;
            }
            if(color == DisEnableColor) {
                pal_JH_BtnGroup.Enabled = false;
                cbo_JH_HasNext.Enabled = false;
            }
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
                btn_JH_OpenFile.Visible = false;

                dgv_JH_XM_FileList.Columns["jh_xm_link"].Visible = false;
                btn_JH_XM_OpenFile.Visible = false;

                dgv_JH_KT_FileList.Columns["jh_kt_link"].Visible = false;
                btn_JH_KT_OpenFile.Visible = false;

                dgv_JH_XM_KT_FileList.Columns["jh_xm_kt_link"].Visible = false;
                btn_JH_XM_KT_OpenFile.Visible = false;

                dgv_JH_XM_KT_ZKT_FileList.Columns["jh_xm_kt_zkt_link"].Visible = false;
                btn_JH_XM_KT_ZKT_OpenFile.Visible = false;

                dgv_JH_KT_ZKT_FileList.Columns["jh_kt_zkt_link"].Visible = false;
                btn_JH_KT_ZKT_OpenFile.Visible = false;

                dgv_Imp_FileList.Columns["imp_link"].Visible = false;
                btn_Imp_OpenFile.Visible = false;

                dgv_Imp_Dev_FileList.Columns["imp_dev_link"].Visible = false;
                btn_Imp_Dev_OpenFile.Visible = false;
            }
            
            //阶段
            InitialStageList(dgv_JH_FileList.Columns["stage"]);
            InitialStageList(dgv_JH_XM_FileList.Columns["jh_xm_stage"]);
            InitialStageList(dgv_JH_KT_FileList.Columns["jh_kt_stage"]);
            InitialStageList(dgv_JH_XM_KT_FileList.Columns["jh_xm_kt_stage"]);
            InitialStageList(dgv_JH_XM_KT_ZKT_FileList.Columns["jh_xm_kt_zkt_stage"]);
            InitialStageList(dgv_JH_KT_ZKT_FileList.Columns["jh_kt_zkt_stage"]);
            InitialStageList(dgv_Imp_FileList.Columns["imp_stage"]);
            InitialStageList(dgv_Imp_Dev_FileList.Columns["imp_dev_stage"]);

            //文件类别
            InitialCategorList(dgv_JH_FileList, string.Empty);
            InitialCategorList(dgv_JH_XM_FileList, "jh_xm_");
            InitialCategorList(dgv_JH_KT_FileList, "jh_kt_");
            InitialCategorList(dgv_JH_XM_KT_FileList, "jh_xm_kt_");
            InitialCategorList(dgv_JH_XM_KT_ZKT_FileList, "jh_xm_kt_zkt_");
            InitialCategorList(dgv_JH_KT_ZKT_FileList, "jh_kt_zkt_");
            InitialCategorList(dgv_Imp_FileList, "imp_");
            InitialCategorList(dgv_Imp_Dev_FileList, "imp_dev_");

            //文件类型
            InitialTypeList(dgv_JH_FileList, string.Empty);
            InitialTypeList(dgv_JH_XM_FileList, "jh_xm_");
            InitialTypeList(dgv_JH_KT_FileList, "jh_kt_");
            InitialTypeList(dgv_JH_XM_KT_FileList, "jh_xm_kt_");
            InitialTypeList(dgv_JH_XM_KT_ZKT_FileList, "jh_xm_kt_zkt_");
            InitialTypeList(dgv_JH_KT_ZKT_FileList, "jh_kt_zkt_");
            InitialTypeList(dgv_Imp_FileList, "imp_");
            InitialTypeList(dgv_Imp_Dev_FileList, "imp_dev_");

            //密级
            InitialSecretList(dgv_JH_FileList, string.Empty);
            InitialSecretList(dgv_JH_XM_FileList, "jh_xm_");
            InitialSecretList(dgv_JH_KT_FileList, "jh_kt_");
            InitialSecretList(dgv_JH_XM_KT_FileList, "jh_xm_kt_");
            InitialSecretList(dgv_JH_XM_KT_ZKT_FileList, "jh_xm_kt_zkt_");
            InitialSecretList(dgv_JH_KT_ZKT_FileList, "jh_kt_zkt_");
            InitialSecretList(dgv_Imp_FileList, "imp_");
            InitialSecretList(dgv_Imp_Dev_FileList, "imp_dev_");

            //载体
            InitialCarrierList(dgv_JH_FileList, string.Empty);
            InitialCarrierList(dgv_JH_XM_FileList, "jh_xm_");
            InitialCarrierList(dgv_JH_KT_FileList, "jh_kt_");
            InitialCarrierList(dgv_JH_XM_KT_FileList, "jh_xm_kt_");
            InitialCarrierList(dgv_JH_XM_KT_ZKT_FileList, "jh_xm_kt_zkt_");
            InitialCarrierList(dgv_JH_KT_ZKT_FileList, "jh_kt_zkt_");
            InitialCarrierList(dgv_Imp_FileList, "imp_");
            InitialCarrierList(dgv_Imp_Dev_FileList, "imp_dev_");

            //文件格式
            InitialFormatList(dgv_JH_FileList, string.Empty);
            InitialFormatList(dgv_JH_XM_FileList, "jh_xm_");
            InitialFormatList(dgv_JH_KT_FileList, "jh_kt_");
            InitialFormatList(dgv_JH_XM_KT_FileList, "jh_xm_kt_");
            InitialFormatList(dgv_JH_XM_KT_ZKT_FileList, "jh_xm_kt_zkt_");
            InitialFormatList(dgv_JH_KT_ZKT_FileList, "jh_kt_zkt_");
            InitialFormatList(dgv_Imp_FileList, "imp_");
            InitialFormatList(dgv_Imp_Dev_FileList, "imp_dev_");

            //文件形态
            InitialFormList(dgv_JH_FileList, string.Empty);
            InitialFormList(dgv_JH_XM_FileList, "jh_xm_");
            InitialFormList(dgv_JH_KT_FileList, "jh_kt_");
            InitialFormList(dgv_JH_XM_KT_FileList, "jh_xm_kt_");
            InitialFormList(dgv_JH_XM_KT_ZKT_FileList, "jh_xm_kt_zkt_");
            InitialFormList(dgv_JH_KT_ZKT_FileList, "jh_kt_zkt_");
            InitialFormList(dgv_Imp_FileList, "imp_");
            InitialFormList(dgv_Imp_Dev_FileList, "imp_dev_");

            //来源单位/省份 下拉列表
            //InitialDrowDownList(ControlType.Plan_Project);
            //InitialDrowDownList(ControlType.Plan_Project_Topic);
            //InitialDrowDownList(ControlType.Plan_Project_Topic_Subtopic);
            //InitialDrowDownList(ControlType.Plan_Topic);
            //InitialDrowDownList(ControlType.Plan_Topic_Subtopic);
            
            //文件核查原因列表
            InitialLostReasonList(dgv_JH_FileValid, "dgv_jh_");
            InitialLostReasonList(dgv_JH_XM_FileValid, "dgv_jh_xm_");
            InitialLostReasonList(dgv_JH_KT_FileValid, "dgv_jh_kt_");
            InitialLostReasonList(dgv_JH_XM_KT_FileValid, "dgv_jh_xm_kt_");
            InitialLostReasonList(dgv_JH_XM_KT_ZKT_FileValid, "dgv_jh_xm_kt_zkt_");
            InitialLostReasonList(dgv_JH_KT_ZKT_FileValid, "dgv_jh_kt_zkt_");
            InitialLostReasonList(dgv_Imp_FileValid, "dgv_imp_");
            InitialLostReasonList(dgv_Imp_Dev_FileValid, "dgv_imp_dev_");

            cbo_JH_HasNext.SelectedIndex = 0;
            cbo_JH_XM_HasNext.SelectedIndex = 0;
            cbo_JH_XM_KT_HasNext.SelectedIndex = 0;
            cbo_JH_KT_HasNext.SelectedIndex = 0;
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
        /// <summary>
        /// 初始化下拉框数据（承担单位/省份）
        /// </summary>
        private void InitialDrowDownList(ControlType type)
        {
            DataTable dataTable = SqlHelper.ExecuteQuery("SELECT dd_id,dd_name FROM data_dictionary WHERE dd_pId=(" +
                "SELECT dd_id FROM data_dictionary where dd_code = 'dic_key_company_work')");
            DataTable _dataTable = SqlHelper.ExecuteQuery("SELECT dd_id,dd_name FROM data_dictionary WHERE dd_pId=(" +
                "SELECT dd_id FROM data_dictionary where dd_code = 'dic_xzqy_province')");
            if(type == ControlType.Plan_Project)
            {
                cbo_JH_XM_Unit.DataSource = dataTable;
                cbo_JH_XM_Unit.DisplayMember = "dd_name";
                cbo_JH_XM_Unit.ValueMember = "dd_id";

                cbo_JH_XM_Province.DataSource = _dataTable;
                cbo_JH_XM_Province.DisplayMember = "dd_name";
                cbo_JH_XM_Province.ValueMember = "dd_id";
            }
            else if(type == ControlType.Plan_Topic)
            {
                cbo_JH_KT_Unit.DataSource = dataTable;
                cbo_JH_KT_Unit.DisplayMember = "dd_name";
                cbo_JH_KT_Unit.ValueMember = "dd_id";

                cbo_JH_KT_Province.DataSource = _dataTable;
                cbo_JH_KT_Province.DisplayMember = "dd_name";
                cbo_JH_KT_Province.ValueMember = "dd_id";
            }
            else if(type == ControlType.Plan_Project_Topic)
            {
                cbo_JH_XM_KT_Unit.DataSource = dataTable;
                cbo_JH_XM_KT_Unit.DisplayMember = "dd_name";
                cbo_JH_XM_KT_Unit.ValueMember = "dd_id";

                cbo_JH_XM_KT_Province.DataSource = _dataTable;
                cbo_JH_XM_KT_Province.DisplayMember = "dd_name";
                cbo_JH_XM_KT_Province.ValueMember = "dd_id";
            }
            else if(type == ControlType.Plan_Project_Topic_Subtopic)
            {
                cbo_JH_XM_KT_ZKT_Unit.DataSource = dataTable;
                cbo_JH_XM_KT_ZKT_Unit.DisplayMember = "dd_name";
                cbo_JH_XM_KT_ZKT_Unit.ValueMember = "dd_id";

                cbo_JH_XM_KT_ZKT_Province.DataSource = _dataTable;
                cbo_JH_XM_KT_ZKT_Province.DisplayMember = "dd_name";
                cbo_JH_XM_KT_ZKT_Province.ValueMember = "dd_id";
            }
            else if(type == ControlType.Plan_Topic_Subtopic)
            {
                cbo_JH_KT_ZKT_Unit.DataSource = dataTable;
                cbo_JH_KT_ZKT_Unit.DisplayMember = "dd_name";
                cbo_JH_KT_ZKT_Unit.ValueMember = "dd_id";

                cbo_JH_KT_ZKT_Province.DataSource = _dataTable;
                cbo_JH_KT_ZKT_Province.DisplayMember = "dd_name";
                cbo_JH_KT_ZKT_Province.ValueMember = "dd_id";
            }
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
        private void SetCategorByStage(object jdId, DataGridViewRow dataGridViewRow, string key)
        {
            //文件类别
            DataGridViewComboBoxCell categorCell = dataGridViewRow.Cells[key + "categor"] as DataGridViewComboBoxCell;

            string querySql = $"SELECT dd_id, dd_name FROM data_dictionary WHERE dd_pId='{jdId}' ORDER BY dd_sort";
            categorCell.DataSource = SqlHelper.ExecuteQuery(querySql);
            categorCell.DisplayMember = "dd_name";
            categorCell.ValueMember = "dd_id";
            categorCell.Style = new DataGridViewCellStyle() { Font = new System.Drawing.Font("宋体", 10.5f), NullValue = categorCell.Items[0] };
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
                con.Tag = ControlType.Plan;
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
            else if("dgv_JH_KT_ZKT_FileList".Equals(dataGridView.Name))
            {
                string columnName = dgv_JH_KT_ZKT_FileList.CurrentCell.OwningColumn.Name;
                Control con = e.Control;
                con.Tag = ControlType.Plan_Topic_Subtopic;
                if("jh_kt_zkt_stage".Equals(columnName))
                    (con as ComboBox).SelectionChangeCommitted += new EventHandler(StageComboBox_SelectionChangeCommitted);
                else if("jh_kt_zkt_categor".Equals(columnName))
                    (con as ComboBox).SelectionChangeCommitted += new EventHandler(CategorComboBox_SelectionChangeCommitted);
            }
            else if("dgv_JH_XM_KT_FileList".Equals(dataGridView.Name))
            {
                string columnName = dgv_JH_XM_KT_FileList.CurrentCell.OwningColumn.Name;
                Control con = e.Control;
                con.Tag = ControlType.Plan_Project_Topic;
                if("jh_xm_kt_stage".Equals(columnName))
                    (con as ComboBox).SelectionChangeCommitted += new EventHandler(StageComboBox_SelectionChangeCommitted);
                else if("jh_xm_kt_categor".Equals(columnName))
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
            if((ControlType)comboBox.Tag == ControlType.Plan)
                SetCategorByStage(comboBox.SelectedValue, dgv_JH_FileList.CurrentRow, string.Empty);
            else if((ControlType)comboBox.Tag == ControlType.Plan_Project)
                SetCategorByStage(comboBox.SelectedValue, dgv_JH_XM_FileList.CurrentRow, "jh_xm_");
            else if((ControlType)comboBox.Tag == ControlType.Plan_Topic)
                SetCategorByStage(comboBox.SelectedValue, dgv_JH_KT_FileList.CurrentRow, "jh_kt_");
            else if((ControlType)comboBox.Tag == ControlType.Plan_Project_Topic)
                SetCategorByStage(comboBox.SelectedValue, dgv_JH_XM_KT_FileList.CurrentRow, "jh_xm_kt_");
            else if((ControlType)comboBox.Tag == ControlType.Plan_Topic_Subtopic)
                SetCategorByStage(comboBox.SelectedValue, dgv_JH_KT_ZKT_FileList.CurrentRow, "jh_kt_zkt_");
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
            if((ControlType)comboBox.Tag == ControlType.Plan)
                SetNameByCategor(comboBox.SelectedValue, dgv_JH_FileList.CurrentRow, string.Empty);
            else if((ControlType)comboBox.Tag == ControlType.Plan_Project)
                SetNameByCategor(comboBox.SelectedValue, dgv_JH_XM_FileList.CurrentRow, "jh_xm_");
            else if((ControlType)comboBox.Tag == ControlType.Plan_Topic)
                SetNameByCategor(comboBox.SelectedValue, dgv_JH_KT_FileList.CurrentRow, "jh_kt_");
            else if((ControlType)comboBox.Tag == ControlType.Plan_Project_Topic)
                SetNameByCategor(comboBox.SelectedValue, dgv_JH_XM_KT_FileList.CurrentRow, "jh_xm_kt_");
            else if((ControlType)comboBox.Tag == ControlType.Plan_Topic_Subtopic)
                SetNameByCategor(comboBox.SelectedValue, dgv_JH_KT_ZKT_FileList.CurrentRow, "jh_kt_zkt_");
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
        /// <summary>
        /// 质检意见
        /// </summary>
        private void Btn_OpenFile_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;
            if("btn_JH_OpenFile".Equals(button.Name))
            {
                object id = dgv_JH_FileList.Tag;
                string name = lbl_JH_Name.Text;
                int index = tab_JH_FileInfo.SelectedIndex;
                if(isBackWork)
                    new Frm_AdviceBW(id, name).Show();
                else
                    new Frm_Advice(id, name, index == 0 ? index : index + 1, isBackWork).Show();
            }
            else if("btn_JH_XM_OpenFile".Equals(button.Name))
            {
                object id = dgv_JH_XM_FileList.Tag;
                string name = txt_JH_XM_Name.Text;
                int index = tab_JH_XM_FileInfo.SelectedIndex;
                if(isBackWork)
                    new Frm_AdviceBW(id, name).Show();
                else
                    new Frm_Advice(id, name, index == 0 ? index : index + 1, isBackWork).Show();
            }
            else if("btn_JH_XM_KT_OpenFile".Equals(button.Name))
            {
                object id = dgv_JH_XM_KT_FileList.Tag;
                string name = txt_JH_XM_KT_Name.Text;
                int index = tab_JH_XM_KT_FileInfo.SelectedIndex;
                if(isBackWork)
                    new Frm_AdviceBW(id, name).Show();
                else
                    new Frm_Advice(id, name, index == 0 ? index : index + 1, isBackWork).Show();
            }
            else if("btn_JH_XM_KT_ZKT_OpenFile".Equals(button.Name))
            {
                object id = dgv_JH_XM_KT_ZKT_FileList.Tag;
                string name = txt_JH_XM_KT_ZKT_Name.Text;
                int index = tab_JH_XM_KT_ZKT_FileInfo.SelectedIndex;
                if(isBackWork)
                    new Frm_AdviceBW(id, name).Show();
                else
                    new Frm_Advice(id, name, index == 0 ? index : index + 1, isBackWork).Show();
            }
            else if("btn_JH_KT_OpenFile".Equals(button.Name))
            {
                object id = dgv_JH_KT_FileList.Tag;
                string name = txt_JH_KT_Name.Text;
                int index = tab_JH_KT_FileInfo.SelectedIndex;
                if(isBackWork)
                    new Frm_AdviceBW(id, name).Show();
                else
                    new Frm_Advice(id, name, index == 0 ? index : index + 1, isBackWork).Show();
            }
            else if("btn_JH_KT_ZKT_OpenFile".Equals(button.Name))
            {
                object id = dgv_JH_KT_ZKT_FileList.Tag;
                string name = txt_JH_KT_ZKT_Name.Text;
                int index = tab_JH_KT_ZKT_FileInfo.SelectedIndex;
                if(isBackWork)
                    new Frm_AdviceBW(id, name).Show();
                else
                    new Frm_Advice(id, name, index == 0 ? index : index + 1, isBackWork).Show();
            }
            else if("btn_Imp_OpenFile".Equals(button.Name))
            {
                object id = dgv_Imp_FileList.Tag;
                string name = lbl_Imp_Name.Text;
                int index = tab_Imp_FileInfo.SelectedIndex;
                if(isBackWork)
                    new Frm_AdviceBW(id, name).Show();
                else
                    new Frm_Advice(id, name, index == 0 ? index : index + 1, isBackWork).Show();
            }
            else if("btn_Imp_Dev_OpenFile".Equals(button.Name))
            {
                object id = dgv_Imp_Dev_FileList.Tag;
                string name = txt_Imp_Dev_Name.Text;
                int index = tab_Imp_Dev_FileInfo.SelectedIndex;
                if(isBackWork)
                    new Frm_AdviceBW(id, name).Show();
                else
                    new Frm_Advice(id, name, index == 0 ? index : index + 1, isBackWork).Show();
            }
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
                    return;
                }
                int _index = tab_MenuList.SelectedIndex;
                int index = comboBox.SelectedIndex;
                if(index == 0)//无
                {
                    ShowTab(null, _index+1);

                    pal_JH_XM.Tag = null;
                }
                else if(index == 1)//父级 - 项目
                {
                    ShowTab("plan_project", _index + 1);
                    pal_JH_XM.Tag = dgv_Imp_Dev_FileList.Tag;
                    ResetControls(ControlType.Plan_Project);
                    InitialDrowDownList(ControlType.Plan_Project);
                }
                else if(index == 2)//父级 - 课题
                {
                    ShowTab("plan_topic", _index + 1);
                    pal_JH_KT.Tag = dgv_Imp_Dev_FileList.Tag;
                    ResetControls(ControlType.Plan_Topic);
                    InitialDrowDownList(ControlType.Plan_Topic);
                }
            }
            else
            {
                object id = dgv_JH_FileList.Tag;
                if(id == null)
                {
                    MessageBox.Show("尚未保存当前项目，无法添加新数据。", "温馨提示");
                    cbo_JH_HasNext.SelectedIndex = 0;
                    return;
                }
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
                    pal_JH_XM.Tag = dgv_JH_FileList.Tag;
                    ResetControls(ControlType.Plan_Project);
                    InitialDrowDownList(ControlType.Plan_Project);
                }
                else if(index == 2)//父级 - 课题
                {
                    ShowTab("plan_topic", _index + 1);
                    pal_JH_KT.Tag = dgv_JH_FileList.Tag;
                    ResetControls(ControlType.Plan_Topic);
                    InitialDrowDownList(ControlType.Plan_Topic);
                }
            }
        }

        private void Cbo_JH_XM_HasNext_SelectionChangeCommitted(object sender, EventArgs e)
        {
            int _index = tab_MenuList.SelectedIndex;
            int index = cbo_JH_XM_HasNext.SelectedIndex;
            if(index == 0)//无
            {
                ShowTab(null, _index + 1);
            }
            else if(index == 1)//子级 - 课题
            {
                if(dgv_JH_XM_FileList.Tag == null)
                {
                    MessageBox.Show("尚未保存当前项目，无法添加新数据。", "温馨提示");
                    cbo_JH_XM_HasNext.SelectedIndex = 0;
                }
                else
                {
                    ShowTab("plan_project_topic", _index + 1);
                    pal_JH_XM_KT.Tag = dgv_JH_XM_FileList.Tag;
                    ResetControls(ControlType.Plan_Project_Topic);
                    InitialDrowDownList(ControlType.Plan_Project_Topic);
                }
            }
        }
       
        private void Cbo_JH_XM_KT_HasNext_SelectionChangeCommitted(object sender, EventArgs e)
        {
            int _index = tab_MenuList.SelectedIndex;
            int index = cbo_JH_XM_KT_HasNext.SelectedIndex;
            if(index == 0)//无
            {
                ShowTab(null, _index + 1);
            }
            else if(index == 1)//子级 - 子课题
            {
                if(dgv_JH_XM_KT_FileList.Tag == null)
                {
                    MessageBox.Show("尚未保存当前课题信息，无法添加新数据。", "温馨提示");
                    cbo_JH_XM_KT_HasNext.SelectedIndex = 0;
                }
                else
                {
                    ShowTab("plan_project_topic_subtopic", _index + 1);
                    pal_JH_XM_KT_ZKT.Tag = dgv_JH_XM_KT_FileList.Tag;
                    ResetControls(ControlType.Plan_Project_Topic_Subtopic);
                    InitialDrowDownList(ControlType.Plan_Project_Topic_Subtopic);
                }
            }
        }
        
        private void Cbo_JH_KT_HasNext_SelectionChangeCommitted(object sender, EventArgs e)
        {
            int _index = tab_MenuList.SelectedIndex;
            int index = cbo_JH_KT_HasNext.SelectedIndex;
            if(index == 0)//无
            {
                ShowTab(null, _index + 1);
            }
            else if(index == 1)//子级 - 子课题
            {
                if(dgv_JH_KT_FileList.Tag == null)
                {
                    MessageBox.Show("尚未保存当前课题信息，无法添加新数据。", "温馨提示");
                    cbo_JH_KT_HasNext.SelectedIndex = 0;
                }
                else
                {
                    ShowTab("plan_topic_subtopic", _index + 1);
                    pal_JH_KT_ZKT.Tag = dgv_JH_KT_FileList.Tag;
                    ResetControls(ControlType.Plan_Topic_Subtopic);
                    InitialDrowDownList(ControlType.Plan_Topic_Subtopic);
                }
            }
        }
        /// <summary>
        /// 保存事件
        /// </summary>
        private void Btn_Save_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;
            //计划
            if("btn_JH_Save".Equals(button.Name))
            {
                //判断当前保存类型【根据Tab页索引】
                int fileIndex = tab_JH_FileInfo.SelectedIndex;
                if(fileIndex == 0)//文件
                {
                    //保存计划基本信息
                    object objId = dgv_JH_FileList.Tag;
                    if(objId == null)
                    {
                        objId = AddProjectBasicInfo(lbl_JH_Name.Tag, ControlType.Plan);
                        dgv_JH_FileList.Tag = objId;
                    }
                    else
                    {
                        UpdateProjectBasicInfo(objId, ControlType.Plan);
                    }
                    if(CheckFileListComplete(dgv_JH_FileList, string.Empty))
                    {
                        int maxLength = dgv_JH_FileList.Rows.Count;
                        if(maxLength > 1)
                        {
                            for(int i = 0; i < maxLength - 1; i++)
                            {
                                DataGridViewRow row = dgv_JH_FileList.Rows[i];
                                object id = row.Cells["id"].Value;
                                if(id == null)//新增
                                {
                                    object pflid = AddFileInfo(string.Empty, row, objId);
                                    row.Cells["id"].Value = row.Index + 1;
                                    row.Cells["id"].Tag = pflid;
                                }
                                else//更新
                                {
                                    object pflid = row.Cells["id"].Tag;
                                    UpdateFileInfo(string.Empty, row);
                                }
                            }
                            MessageBox.Show("文件列表信息保存成功！");
                        }
                        if(workType == WorkType.Default)
                            LoadTreeList(lbl_Imp_Name.Tag, ControlType.Imp_Sub);
                        else
                            LoadTreeList(dgv_JH_FileList.Tag, ControlType.Default);
                    }
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
                int fileIndex = tab_JH_XM_FileInfo.SelectedIndex;
                if(fileIndex == 0)
                {
                    string code = txt_JH_XM_Code.Text;
                    if(string.IsNullOrEmpty(code))
                        MessageBox.Show("项目编号不能为空");
                    else
                    {
                        //根据主键是否存在判断是更新还是新增
                        if(dgv_JH_XM_FileList.Tag != null)//更新
                            UpdateProjectBasicInfo(dgv_JH_XM_FileList.Tag, ControlType.Plan_Project);
                        else//新增
                        {
                            //保存基本信息
                            object pid = AddProjectBasicInfo(pal_JH_XM.Tag, ControlType.Plan_Project);
                            dgv_JH_XM_FileList.Tag = pid;
                        }
                        //保存文件列表
                        if(CheckFileListComplete(dgv_JH_XM_FileList,"jh_xm_"))
                        {
                            int maxLength = dgv_JH_XM_FileList.Rows.Count;
                            if(maxLength > 1)
                                for(int i = 0; i < maxLength - 1; i++)
                                {
                                    DataGridViewRow row = dgv_JH_XM_FileList.Rows[i];
                                    object id = row.Cells["jh_xm_id"].Value;
                                    if(id == null)//新增
                                    {
                                        object pflid = AddFileInfo("jh_xm_", row, dgv_JH_XM_FileList.Tag);
                                        row.Cells["jh_xm_id"].Value = row.Index + 1;
                                        row.Cells["jh_xm_id"].Tag = pflid;
                                    }
                                    else//更新
                                    {
                                        object pflid = row.Cells["jh_xm_id"].Tag;
                                        UpdateFileInfo("jh_xm_", row);
                                    }
                                }
                            MessageBox.Show("操作成功！");
                        }
                        if(workType == WorkType.Default)
                            LoadTreeList(lbl_Imp_Name.Tag, ControlType.Imp_Sub);
                        else
                            LoadTreeList(dgv_JH_FileList.Tag, ControlType.Default);
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
                int fileIndex = tab_JH_KT_FileInfo.SelectedIndex;
                if(fileIndex == 0)
                {
                    string code = txt_JH_KT_Code.Text;
                    if(string.IsNullOrEmpty(code))
                        MessageBox.Show("课题编号不能为空");
                    else
                    {
                        //根据主键是否存在判断是更新还是新增
                        if(dgv_JH_KT_FileList.Tag != null)//更新
                            UpdateProjectBasicInfo(dgv_JH_KT_FileList.Tag, ControlType.Plan_Topic);
                        else//新增
                        {
                            //保存基本信息
                            object pid = AddProjectBasicInfo(pal_JH_KT.Tag, ControlType.Plan_Topic);
                            dgv_JH_KT_FileList.Tag = pid;
                        }
                        //保存文件列表
                        if(CheckFileListComplete(dgv_JH_KT_FileList, "jh_kt_"))
                        {
                            int maxLength = dgv_JH_KT_FileList.Rows.Count;
                            if(maxLength > 1)
                                for(int i = 0; i < maxLength - 1; i++)
                                {
                                    DataGridViewRow row = dgv_JH_KT_FileList.Rows[i];
                                    object id = row.Cells["jh_kt_id"].Value;
                                    if(id == null)//新增
                                    {
                                        object pflid = AddFileInfo("jh_kt_", row, dgv_JH_KT_FileList.Tag);
                                        row.Cells["jh_kt_id"].Value = row.Index + 1;
                                        row.Cells["jh_kt_id"].Tag = pflid;
                                    }
                                    else//更新
                                    {
                                        object pflid = row.Cells["jh_kt_id"].Tag;
                                        UpdateFileInfo("jh_kt_", row);
                                    }
                                }
                            MessageBox.Show("操作成功！");
                        }
                        if(workType == WorkType.Default)
                            LoadTreeList(lbl_Imp_Name.Tag, ControlType.Imp_Sub);
                        else
                            LoadTreeList(dgv_JH_FileList.Tag, ControlType.Default);
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
            //计划-项目-课题
            else if("btn_JH_XM_KT_Save".Equals(button.Name))
            {
                int fileIndex = tab_JH_XM_KT_FileInfo.SelectedIndex;
                if(fileIndex == 0)
                {
                    string code = txt_JH_XM_KT_Code.Text;
                    if(string.IsNullOrEmpty(code))
                        MessageBox.Show("课题编号不能为空");
                    else
                    {
                        //根据主键是否存在判断是更新还是新增
                        if(dgv_JH_XM_KT_FileList.Tag != null)//更新
                            UpdateProjectBasicInfo(dgv_JH_XM_KT_FileList.Tag, ControlType.Plan_Project_Topic);
                        else//新增
                        {
                            //保存基本信息
                            object pid = AddProjectBasicInfo(pal_JH_XM_KT.Tag, ControlType.Plan_Project_Topic);
                            dgv_JH_XM_KT_FileList.Tag = pid;
                        }
                        //保存文件列表
                        if(CheckFileListComplete(dgv_JH_XM_KT_FileList, "jh_xm_kt_"))
                        {
                            int maxLength = dgv_JH_XM_KT_FileList.Rows.Count;
                            if(maxLength > 1)
                                for(int i = 0; i < maxLength - 1; i++)
                                {
                                    DataGridViewRow row = dgv_JH_XM_KT_FileList.Rows[i];
                                    object id = row.Cells["jh_xm_kt_id"].Value;
                                    if(id == null)//新增
                                    {
                                        object pflid = AddFileInfo("jh_xm_kt_", row, dgv_JH_XM_KT_FileList.Tag);
                                        row.Cells["jh_xm_kt_id"].Value = row.Index + 1;
                                        row.Cells["jh_xm_kt_id"].Tag = pflid;
                                    }
                                    else//更新
                                    {
                                        object pflid = row.Cells["jh_xm_kt_id"].Tag;
                                        UpdateFileInfo("jh_xm_kt_", row);
                                    }
                                }
                            MessageBox.Show("操作成功！");
                        }
                        if(workType == WorkType.Default)
                            LoadTreeList(lbl_Imp_Name.Tag, ControlType.Imp_Sub);
                        else
                            LoadTreeList(dgv_JH_FileList.Tag, ControlType.Default);
                    }
                }
                else if(fileIndex == 1)//文件核查
                {
                    ModifyFileValid(dgv_JH_XM_KT_FileValid, dgv_JH_XM_KT_FileList.Tag, "dgv_jh_xm_kt_");
                    MessageBox.Show("文件核查信息保存成功！");
                }
                else if(fileIndex == 2)
                {
                    object objId = dgv_JH_XM_KT_FileList.Tag;
                    if(objId != null)
                    {
                        object aid = txt_JH_XM_KT_AJ_Code.Tag;
                        string code = txt_JH_XM_KT_AJ_Code.Text;
                        string name = txt_JH_XM_KT_AJ_Name.Text;
                        string term = txt_JH_XM_KT_AJ_Term.Text;
                        string secret = txt_JH_XM_KT_AJ_Secret.Text;
                        string user = txt_JH_XM_KT_AJ_User.Text;
                        string unit = txt_JH_XM_KT_AJ_Unit.Text;
                        if(aid == null)
                        {
                            aid = Guid.NewGuid().ToString();
                            string insertSql = $"INSERT INTO processing_tag VALUES ('{aid}','{code}','{name}','{term}','{secret}','{user}','{unit}','{objId}')";
                            SqlHelper.ExecuteNonQuery(insertSql);
                            txt_JH_XM_KT_AJ_Code.Tag = aid;
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
            //计划-课题-子课题
            else if("btn_JH_KT_ZKT_Save".Equals(button.Name))
            {
                int fileIndex = tab_JH_KT_ZKT_FileInfo.SelectedIndex;
                if(fileIndex == 0)
                {
                    string code = txt_JH_KT_ZKT_Code.Text;
                    if(string.IsNullOrEmpty(code))
                        MessageBox.Show("课题编号不能为空");
                    else
                    {
                        //根据主键是否存在判断是更新还是新增
                        if(dgv_JH_KT_ZKT_FileList.Tag != null)//更新
                            UpdateProjectBasicInfo(dgv_JH_KT_ZKT_FileList.Tag, ControlType.Plan_Topic_Subtopic);
                        else//新增
                        {
                            //保存基本信息
                            object pid = AddProjectBasicInfo(pal_JH_KT_ZKT.Tag, ControlType.Plan_Topic_Subtopic);
                            dgv_JH_KT_ZKT_FileList.Tag = pid;
                        }
                        //保存文件列表
                        if(CheckFileListComplete(dgv_JH_KT_ZKT_FileList, "jh_kt_zkt_"))
                        {
                            int maxLength = dgv_JH_KT_ZKT_FileList.Rows.Count;
                            if(maxLength > 1)
                                for(int i = 0; i < maxLength - 1; i++)
                                {
                                    DataGridViewRow row = dgv_JH_KT_ZKT_FileList.Rows[i];
                                    object id = row.Cells["jh_kt_zkt_id"].Value;
                                    if(id == null)//新增
                                    {
                                        object pflid = AddFileInfo("jh_kt_zkt_", row, dgv_JH_KT_ZKT_FileList.Tag);
                                        row.Cells["jh_kt_zkt_id"].Value = row.Index + 1;
                                        row.Cells["jh_kt_zkt_id"].Tag = pflid;
                                    }
                                    else//更新
                                    {
                                        object pflid = row.Cells["jh_kt_zkt_id"].Tag;
                                        UpdateFileInfo("jh_kt_zkt_", row);
                                    }
                                }
                            MessageBox.Show("操作成功！");
                        }
                        if(workType == WorkType.Default)
                            LoadTreeList(lbl_Imp_Name.Tag, ControlType.Imp_Sub);
                        else
                            LoadTreeList(dgv_JH_FileList.Tag, ControlType.Default);
                    }
                }
                else if(fileIndex == 1)//文件核查
                {
                    ModifyFileValid(dgv_JH_KT_ZKT_FileValid, dgv_JH_KT_ZKT_FileList.Tag, "dgv_jh_kt_zkt_");
                    MessageBox.Show("文件核查信息保存成功！");
                }
                else if(fileIndex == 2)
                {
                    object objId = dgv_JH_KT_ZKT_FileList.Tag;
                    if(objId != null)
                    {
                        object aid = txt_JH_KT_ZKT_AJ_Code.Tag;
                        string code = txt_JH_KT_ZKT_AJ_Code.Text;
                        string name = txt_JH_KT_ZKT_AJ_Name.Text;
                        string term = txt_JH_KT_ZKT_AJ_Term.Text;
                        string secret = txt_JH_KT_ZKT_AJ_Secret.Text;
                        string user = txt_JH_KT_ZKT_AJ_User.Text;
                        string unit = txt_JH_KT_ZKT_AJ_Unit.Text;
                        if(aid == null)
                        {
                            aid = Guid.NewGuid().ToString();
                            string insertSql = $"INSERT INTO processing_tag VALUES ('{aid}','{code}','{name}','{term}','{secret}','{user}','{unit}','{objId}')";
                            SqlHelper.ExecuteNonQuery(insertSql);
                            txt_JH_KT_ZKT_AJ_Code.Tag = aid;
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
                int fileIndex = tab_JH_XM_KT_ZKT_FileInfo.SelectedIndex;
                if(fileIndex == 0)
                {
                    string code = txt_JH_XM_KT_ZKT_Code.Text;
                    if(string.IsNullOrEmpty(code))
                        MessageBox.Show("课题编号不能为空");
                    else
                    {
                        //根据主键是否存在判断是更新还是新增
                        if(dgv_JH_XM_KT_ZKT_FileList.Tag != null)//更新
                            UpdateProjectBasicInfo(dgv_JH_XM_KT_ZKT_FileList.Tag, ControlType.Plan_Project_Topic_Subtopic);
                        else//新增
                        {
                            object pid = AddProjectBasicInfo(pal_JH_XM_KT_ZKT.Tag, ControlType.Plan_Project_Topic_Subtopic);
                            dgv_JH_XM_KT_ZKT_FileList.Tag = pid;
                        }
                        //保存文件列表
                        if(CheckFileListComplete(dgv_JH_XM_KT_ZKT_FileList, "jh_xm_kt_zkt_"))
                        {
                            int maxLength = dgv_JH_XM_KT_ZKT_FileList.Rows.Count;
                            if(maxLength > 1)
                                for(int i = 0; i < maxLength - 1; i++)
                                {
                                    DataGridViewRow row = dgv_JH_XM_KT_ZKT_FileList.Rows[i];
                                    object id = row.Cells["jh_xm_kt_zkt_id"].Value;
                                    if(id == null)//新增
                                    {
                                        object pflid = AddFileInfo("jh_xm_kt_zkt_", row, dgv_JH_KT_ZKT_FileList.Tag);
                                        row.Cells["jh_xm_kt_zkt_id"].Value = row.Index + 1;
                                        row.Cells["jh_xm_kt_zkt_id"].Tag = pflid;
                                    }
                                    else//更新
                                    {
                                        object pflid = row.Cells["jh_xm_kt_zkt_id"].Tag;
                                        UpdateFileInfo("jh_xm_kt_zkt_", row);
                                    }
                                }
                            MessageBox.Show("操作成功！");
                        }
                        if(workType == WorkType.Default)
                            LoadTreeList(lbl_Imp_Name.Tag, ControlType.Imp_Sub);
                        else
                            LoadTreeList(dgv_JH_FileList.Tag, ControlType.Default);
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

                int fileIndex = tab_Imp_FileInfo.SelectedIndex;
                if(fileIndex == 0)
                {
                    //保存文件列表
                    if(CheckFileListComplete(dgv_Imp_FileList, "imp_"))
                    {
                        int maxLength = dgv_Imp_FileList.Rows.Count;
                        if(maxLength > 1)
                            for(int i = 0; i < maxLength - 1; i++)
                            {
                                DataGridViewRow row = dgv_Imp_FileList.Rows[i];
                                object id = row.Cells["imp_id"].Value;
                                if(id == null)//新增
                                {
                                    object pflid = AddFileInfo("imp_", row, dgv_Imp_FileList.Tag);
                                    row.Cells["imp_id"].Value = row.Index + 1;
                                    row.Cells["imp_id"].Tag = pflid;
                                }
                                else//更新
                                {
                                    object pflid = row.Cells["imp_id"].Tag;
                                    UpdateFileInfo("imp_", row);
                                }
                            }
                        MessageBox.Show("操作成功！");
                        if(workType == WorkType.Default)
                            LoadTreeList(lbl_Imp_Name.Tag, ControlType.Imp_Sub);
                        else
                            LoadTreeList(dgv_JH_FileList.Tag, ControlType.Default);
                    }
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
                int fileIndex = tab_Imp_Dev_FileInfo.SelectedIndex;
                if(fileIndex == 0)
                {
                    string code = txt_Imp_Dev_Code.Text;
                    if(string.IsNullOrEmpty(code))
                        MessageBox.Show("编号不能为空");
                    else
                    {
                        //根据主键是否存在判断是更新还是新增
                        if(dgv_Imp_Dev_FileList.Tag != null)//更新
                            UpdateProjectBasicInfo(dgv_Imp_Dev_FileList.Tag, ControlType.Imp_Dev);
                        else//新增
                        {
                            object pid = AddProjectBasicInfo(pal_Imp_Dev.Tag, ControlType.Imp_Dev);
                            dgv_Imp_Dev_FileList.Tag = pid;
                        }
                        //保存文件列表
                        if(CheckFileListComplete(dgv_Imp_Dev_FileList, "imp_dev_"))
                        {
                            int maxLength = dgv_Imp_Dev_FileList.Rows.Count;
                            if(maxLength > 1)
                                for(int i = 0; i < maxLength - 1; i++)
                                {
                                    DataGridViewRow row = dgv_Imp_Dev_FileList.Rows[i];
                                    object id = row.Cells["imp_dev_id"].Value;
                                    if(id == null)//新增
                                    {
                                        object pflid = AddFileInfo("imp_dev_", row, dgv_Imp_Dev_FileList.Tag);
                                        row.Cells["imp_dev_id"].Value = row.Index + 1;
                                        row.Cells["imp_dev_id"].Tag = pflid;
                                    }
                                    else//更新
                                    {
                                        object pflid = row.Cells["imp_dev_id"].Tag;
                                        UpdateFileInfo("imp_dev_", row);
                                    }
                                }
                            MessageBox.Show("操作成功！");
                        }
                        if(workType == WorkType.Default)
                            LoadTreeList(lbl_Imp_Name.Tag, ControlType.Imp_Sub);
                        else
                            LoadTreeList(dgv_JH_FileList.Tag, ControlType.Default);
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
        /// <summary>
        /// 检验文件列表是否可以保存
        /// </summary>
        /// <param name="dataGridView">待检验的表格</param>
        private bool CheckFileListComplete(DataGridView dataGridView, string key)
        {
            for(int i = 0; i < dataGridView.Rows.Count; i++)
            {
                object link = dataGridView.Rows[i].Cells[key + "link"].Value;
                object name = dataGridView.Rows[i].Cells[key + "name"].Value;
                if(link != null && name == null)
                {
                    MessageBox.Show("请先将文件信息补充完整");
                    return false;
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
            if(controlType == ControlType.Plan)
            {

            }
            else if(controlType == ControlType.Plan_Project)
            {
                string code = txt_JH_XM_Code.Text;
                string name = txt_JH_XM_Name.Text;
                string type = txt_JH_XM_JHType.Text;
                string ly = txt_JH_XM_LY.Text;
                string zt = txt_JH_XM_ZT.Text;
                string jf = txt_JH_XM_JF.Text;
                DateTime starttime = dtp_JH_XM_StartTime.Value;
                DateTime endtime = dtp_JH_XM_EndTime.Value;
                string year = txt_JH_XM_LXND.Text;
                object unit = cbo_JH_XM_Unit.SelectedValue;
                object province = cbo_JH_XM_Province.SelectedValue;
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
                    $" WHERE pi_id='{objid}'";
                SqlHelper.ExecuteNonQuery(updateSql);
            }
            else if(controlType == ControlType.Plan_Topic)
            {
                string code = txt_JH_KT_Code.Text;
                string name = txt_JH_KT_Name.Text;
                string type = txt_JH_KT_Type.Text;
                string ly = txt_JH_KT_LY.Text;
                string zt = txt_JH_KT_ZT.Text;
                string jf = txt_JH_KT_JF.Text;
                DateTime starttime = dtp_JH_KT_StartTime.Value;
                DateTime endtime = dtp_JH_KT_EndTime.Value;
                string year = txt_JH_KT_Year.Text;
                object unit = cbo_JH_KT_Unit.SelectedValue;
                object province = cbo_JH_KT_Province.SelectedValue;
                string unituser = txt_JH_KT_UnitUser.Text;
                string objuser = txt_JH_KT_ProUser.Text;
                string intro = txt_JH_KT_Intro.Text;

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
            else if(controlType == ControlType.Plan_Project_Topic)
            {
                string code = txt_JH_XM_KT_Code.Text;
                string name = txt_JH_XM_KT_Name.Text;
                string type = txt_JH_XM_KT_Type.Text;
                string ly = txt_JH_XM_KT_LY.Text;
                string zt = txt_JH_XM_KT_ZT.Text;
                string jf = txt_JH_XM_KT_JF.Text;
                DateTime starttime = dtp_JH_XM_KT_StartTime.Value;
                DateTime endtime = dtp_JH_XM_KT_EndTime.Value;
                string year = txt_JH_XM_KT_Year.Text;
                object unit = cbo_JH_XM_KT_Unit.SelectedValue;
                object province = cbo_JH_XM_KT_Province.SelectedValue;
                string unituser = txt_JH_XM_KT_UnitUser.Text;
                string objuser = txt_JH_XM_KT_ProUser.Text;
                string intro = txt_JH_XM_KT_Intro.Text;

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
            else if(controlType == ControlType.Plan_Topic_Subtopic)
            {
                string code = txt_JH_KT_ZKT_Code.Text;
                string name = txt_JH_KT_ZKT_Name.Text;
                string type = txt_JH_KT_ZKT_Type.Text;
                string ly = txt_JH_KT_ZKT_LY.Text;
                string zt = txt_JH_KT_ZKT_ZT.Text;
                string jf = txt_JH_KT_ZKT_JF.Text;
                DateTime starttime = dtp_JH_KT_ZKT_StartTime.Value;
                DateTime endtime = dtp_JH_KT_ZKT_EndTime.Value;
                string year = txt_JH_KT_ZKT_Year.Text;
                object unit = cbo_JH_KT_ZKT_Unit.SelectedValue;
                object province = cbo_JH_KT_ZKT_Province.SelectedValue;
                string unituser = txt_JH_KT_ZKT_Unituser.Text;
                string objuser = txt_JH_KT_ZKT_ProUser.Text;
                string intro = txt_JH_KT_ZKT_Intro.Text;

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
            else if(controlType == ControlType.Plan_Project_Topic_Subtopic)
            {
                string code = txt_JH_XM_KT_ZKT_Code.Text;
                string name = txt_JH_XM_KT_ZKT_Name.Text;
                string type = txt_JH_XM_KT_ZKT_Type.Text;
                string ly = txt_JH_XM_KT_ZKT_LY.Text;
                string zt = txt_JH_XM_KT_ZKT_ZT.Text;
                string jf = txt_JH_XM_KT_ZKT_JF.Text;
                DateTime starttime = dtp_JH_XM_KT_ZKT_StartTime.Value;
                DateTime endtime = dtp_JH_XM_KT_ZKT_EndTime.Value;
                string year = txt_JH_XM_KT_ZKT_Year.Text;
                object unit = cbo_JH_XM_KT_ZKT_Unit.SelectedValue;
                string unituser = txt_JH_XM_KT_ZKT_Unituser.Text;
                string objuser = txt_JH_XM_KT_ZKT_Prouser.Text;
                object province = cbo_JH_XM_KT_ZKT_Province.SelectedValue;
                string intro = txt_JH_XM_KT_ZKT_Intro.Text;

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
            if(type == ControlType.Plan)
            {
                object code = lbl_JH_Name.Text;
                object name = lbl_JH_Name.Text;
                SqlHelper.ExecuteNonQuery($"INSERT INTO project_info(pi_id,pi_code,pi_name,pi_obj_id,pi_categor,pi_submit_status,pi_source_id)" +
                            $" VALUES('{primaryKey}','{code}','{name}','{parentId}','{(int)ControlType.Plan}','{(int)ObjectSubmitStatus.NonSubmit}','{UserHelper.GetInstance().User.UserKey}')");
            }
            else if(type == ControlType.Plan_Project)
            {
                string code = txt_JH_XM_Code.Text;
                string name = txt_JH_XM_Name.Text;
                string planType = txt_JH_XM_JHType.Text;
                string ly = txt_JH_XM_LY.Text;
                string zt = txt_JH_XM_ZT.Text;
                string jf = txt_JH_XM_JF.Text;
                DateTime starttime = dtp_JH_XM_StartTime.Value;
                DateTime endtime = dtp_JH_XM_EndTime.Value;
                string year = txt_JH_XM_LXND.Text;
                object unit = cbo_JH_XM_Unit.SelectedValue;
                object province = cbo_JH_XM_Province.SelectedValue;
                string unituser = txt_JH_XM_UnitUser.Text;
                string objuser = txt_JH_XM_ObjUser.Text;
                string intro = txt_JH_XM_ObjIntroduct.Text;

                string insertSql = "INSERT INTO project_info(pi_id ,trc_id,pi_code,pi_name,pi_type,pb_belong" +
                    ",pb_belong_type,pi_money,pi_start_datetime,pi_end_datetime,pi_year,pi_company_id,pi_company_user" +
                    ",pi_province,pi_project_user,pi_introduction,pi_work_status,pi_obj_id,pi_categor,pi_submit_status,pi_source_id)" +
                    "VALUES" +
                    $"('{primaryKey}',null,'{code}','{name}','{planType}','{ly}','{zt}','{jf}','{starttime}'" +
                    $",'{endtime}','{year}','{unit}','{unituser}'" +
                    $",'{province}','{objuser}','{intro}','{(int)WorkStatus.Default}','{parentId}',{(int)type},{(int)ObjectSubmitStatus.NonSubmit},'{UserHelper.GetInstance().User.UserKey}')";

                SqlHelper.ExecuteNonQuery(insertSql);
            }
            else if(type == ControlType.Plan_Topic)
            {
                string code = txt_JH_KT_Code.Text;
                string name = txt_JH_KT_Name.Text;
                string planType = txt_JH_KT_Type.Text;
                string ly = txt_JH_KT_LY.Text;
                string zt = txt_JH_KT_ZT.Text;
                string jf = txt_JH_KT_JF.Text;
                DateTime starttime = dtp_JH_KT_StartTime.Value;
                DateTime endtime = dtp_JH_KT_EndTime.Value;
                string year = txt_JH_KT_Year.Text;
                object unit = cbo_JH_KT_Unit.SelectedValue;
                object province = cbo_JH_KT_Province.SelectedValue;
                string unituser = txt_JH_KT_UnitUser.Text;
                string objuser = txt_JH_KT_ProUser.Text;
                string intro = txt_JH_KT_Intro.Text;

                string insertSql = "INSERT INTO project_info(pi_id ,trc_id,pi_code,pi_name,pi_type,pb_belong" +
                    ",pb_belong_type,pi_money,pi_start_datetime,pi_end_datetime,pi_year,pi_company_id,pi_company_user" +
                    ",pi_province,pi_project_user,pi_introduction,pi_work_status,pi_obj_id,pi_categor,pi_submit_status,pi_source_id)" +
                    "VALUES" +
                    $"('{primaryKey}',null,'{code}','{name}','{planType}','{ly}','{zt}','{jf}','{starttime}'" +
                    $",'{endtime}','{year}','{unit}','{unituser}'" +
                    $",'{province}','{objuser}','{intro}','{(int)WorkStatus.Default}','{parentId}','{(int)type}','{(int)ObjectSubmitStatus.NonSubmit}','{UserHelper.GetInstance().User.UserKey}')";

                SqlHelper.ExecuteNonQuery(insertSql);
            }
            else if(type == ControlType.Plan_Project_Topic)
            {
                string code = txt_JH_XM_KT_Code.Text;
                string name = txt_JH_XM_KT_Name.Text;
                string planType = txt_JH_XM_KT_Type.Text;
                string ly = txt_JH_XM_KT_LY.Text;
                string zt = txt_JH_XM_KT_ZT.Text;
                string jf = txt_JH_XM_KT_JF.Text;
                DateTime starttime = dtp_JH_XM_KT_StartTime.Value;
                DateTime endtime = dtp_JH_XM_KT_EndTime.Value;
                string year = txt_JH_XM_KT_Year.Text;
                object unit = cbo_JH_XM_KT_Unit.SelectedValue;
                object province = cbo_JH_XM_KT_Province.SelectedValue;
                string unituser = txt_JH_XM_KT_UnitUser.Text;
                string objuser = txt_JH_XM_KT_ProUser.Text;
                string intro = txt_JH_XM_KT_Intro.Text;

                string insertSql = "INSERT INTO subject_info(si_id, pi_id, si_code, si_name, si_type, si_field, si_belong, si_money, si_start_datetime," +
                    "si_end_datetime, si_year, si_company, si_company_user, si_province, si_project_user, si_introduction, si_work_status, si_categor, si_submit_status," +
                    "si_worker_id) VALUES " +
                    $"('{primaryKey}','{parentId}','{code}','{name}','{planType}','{ly}','{zt}','{jf}'" +
                    $",'{starttime}','{endtime}','{year}','{unit}','{unituser}','{province}','{objuser}'" +
                    $",'{intro}','{(int)WorkStatus.NonWork}','{(int)type}',{(int)ObjectSubmitStatus.NonSubmit},'{UserHelper.GetInstance().User.UserKey}')";
                SqlHelper.ExecuteNonQuery(insertSql);
            }
            else if(type == ControlType.Plan_Topic_Subtopic)
            {
                string code = txt_JH_KT_ZKT_Code.Text;
                string name = txt_JH_KT_ZKT_Name.Text;
                string planType = txt_JH_KT_ZKT_Type.Text;
                string ly = txt_JH_KT_ZKT_LY.Text;
                string zt = txt_JH_KT_ZKT_ZT.Text;
                string jf = txt_JH_KT_ZKT_JF.Text;
                DateTime starttime = dtp_JH_KT_ZKT_StartTime.Value;
                DateTime endtime = dtp_JH_KT_ZKT_EndTime.Value;
                string year = txt_JH_KT_ZKT_Year.Text;
                object unit = cbo_JH_KT_ZKT_Unit.SelectedValue;
                object province = cbo_JH_KT_ZKT_Province.SelectedValue;
                string unituser = txt_JH_KT_ZKT_Unituser.Text;
                string objuser = txt_JH_KT_ZKT_ProUser.Text;
                string intro = txt_JH_KT_ZKT_Intro.Text;

                string insertSql = "INSERT INTO subject_info(si_id, pi_id, si_code, si_name, si_type, si_field, si_belong, si_money, si_start_datetime," +
                    "si_end_datetime, si_year, si_company, si_company_user, si_province, si_project_user, si_introduction, si_work_status, si_categor, si_submit_status," +
                    "si_worker_id) VALUES " +
                    $"('{primaryKey}','{parentId}','{code}','{name}','{planType}','{ly}','{zt}','{jf}'" +
                    $",'{starttime}','{endtime}','{year}','{unit}','{unituser}','{province}','{objuser}'" +
                    $",'{intro}','{(int)WorkStatus.NonWork}','{(int)type}',{(int)ObjectSubmitStatus.NonSubmit},'{UserHelper.GetInstance().User.UserKey}')";
                SqlHelper.ExecuteNonQuery(insertSql);
            }
            else if(type == ControlType.Plan_Project_Topic_Subtopic)
            {
                string code = txt_JH_XM_KT_ZKT_Code.Text;
                string name = txt_JH_XM_KT_ZKT_Name.Text;
                string planType = txt_JH_XM_KT_ZKT_Type.Text;
                string ly = txt_JH_XM_KT_ZKT_LY.Text;
                string zt = txt_JH_XM_KT_ZKT_ZT.Text;
                string jf = txt_JH_XM_KT_ZKT_JF.Text;
                DateTime starttime = dtp_JH_XM_KT_ZKT_StartTime.Value;
                DateTime endtime = dtp_JH_XM_KT_ZKT_EndTime.Value;
                string year = txt_JH_XM_KT_ZKT_Year.Text;
                object unit = cbo_JH_XM_KT_ZKT_Unit.SelectedValue;
                string unituser = txt_JH_XM_KT_ZKT_Unituser.Text;
                string objuser = txt_JH_XM_KT_ZKT_Prouser.Text;
                object province = cbo_JH_XM_KT_ZKT_Province.SelectedValue;
                string intro = txt_JH_XM_KT_ZKT_Intro.Text;

                string insertSql = "INSERT INTO subject_info(si_id, pi_id, si_code, si_name, si_type, si_field, si_belong, si_money, si_start_datetime," +
                    "si_end_datetime, si_year, si_company, si_company_user, si_province, si_project_user, si_introduction, si_work_status, si_categor, si_submit_status," +
                    "si_worker_id) VALUES " +
                    $"('{primaryKey}','{parentId}','{code}','{name}','{planType}','{ly}','{zt}','{jf}'" +
                    $",'{starttime}','{endtime}','{year}','{unit}','{unituser}','{province}','{objuser}'" +
                    $",'{intro}','{(int)WorkStatus.NonWork}','{(int)type}',{(int)ObjectSubmitStatus.NonSubmit},'{UserHelper.GetInstance().User.UserKey}')";
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
        private void LoadTreeList(object planId, ControlType type)
        {
            treeView.Nodes.Clear();
            TreeNode treeNode = null;
            //重大专项/重点研发
            if(workType == WorkType.Default)
            {
                //重大专项
                if(type == ControlType.Imp)
                {
                    object[] _obj = SqlHelper.ExecuteRowsQuery($"SELECT imp_id, imp_name FROM imp_info WHERE imp_id='{planId}'");
                    treeNode = new TreeNode()
                    {
                        Name = GetValue(_obj[0]),
                        Text = GetValue(_obj[1]),
                        Tag = type
                    };
                }
                //普通专项
                else if(type == ControlType.Imp_Normal)
                {
                    object[] _obj = SqlHelper.ExecuteRowsQuery($"SELECT pi_id, pi_name FROM project_info WHERE pi_id='{planId}'");
                    treeNode = new TreeNode()
                    {
                        Name = GetValue(_obj[0]),
                        Text = GetValue(_obj[1]),
                        Tag = type
                    };
                }
                //重大专项 - 专项信息
                else if(type == ControlType.Imp_Sub)
                {
                    DataRow row = SqlHelper.ExecuteSingleRowQuery($"SELECT * FROM imp_dev_info WHERE imp_id = '{planId}'");
                    object[] _obj = SqlHelper.ExecuteRowsQuery($"SELECT imp_id, imp_name FROM imp_info WHERE imp_id = '{row["imp_obj_id"]}'");
                    treeNode = new TreeNode()
                    {
                        Name = GetValue(_obj[0]),
                        Text = GetValue(_obj[1]),
                        Tag = ControlType.Imp,
                        ForeColor = DisEnableColor
                    };
                    TreeNode treeNode2 = new TreeNode()
                    {
                        Name = GetValue(row["imp_id"]),
                        Text = GetValue(row["imp_code"]),
                        Tag = ControlType.Imp_Sub,
                        ForeColor = 2.Equals(row["imp_submit_status"]) ? Color.Black : DisEnableColor
                    };
                    treeNode.Nodes.Add(treeNode2);
                }
                //项目/课题
                else if(type == ControlType.Plan_Project)
                {
                    DataRow row = SqlHelper.ExecuteSingleRowQuery($"SELECT * FROM project_info WHERE pi_id='{planId}'");
                    object[] obj = SqlHelper.ExecuteRowsQuery($"SELECT imp_id, imp_code, imp_name, imp_obj_id FROM imp_dev_info WHERE imp_id='{row["pi_obj_id"]}'");
                    if(obj != null)
                    {
                        TreeNode treeNode2 = new TreeNode()
                        {
                            Name = GetValue(obj[0]),
                            Text = GetValue(obj[1]),
                            Tag = ControlType.Imp_Sub,
                            ForeColor = DisEnableColor
                        };
                        object[] obj2 = SqlHelper.ExecuteRowsQuery($"SELECT imp_id, imp_code, imp_name FROM imp_info WHERE imp_id='{obj[3]}'");
                        treeNode = new TreeNode()
                        {
                            Name = GetValue(obj2[0]),
                            Text = GetValue(obj2[1]),
                            Tag = ControlType.Imp,
                            ForeColor = DisEnableColor
                        };
                        treeNode.Nodes.Add(treeNode2);
                        TreeNode currentNode = new TreeNode()
                        {
                            Name = GetValue(row["pi_id"]),
                            Text = GetValue(row["pi_code"]),
                            Tag = ControlType.Plan_Project,
                            ForeColor = GetValue(row["pi_submit_status"]).Equals(4) ? Color.Red : Color.Black
                        };
                        treeNode2.Nodes.Add(currentNode);
                        List<object[]> list = SqlHelper.ExecuteColumnsQuery($"SELECT si_id, si_name, si_code, si_submit_status FROM subject_info WHERE pi_id='{currentNode.Name}'", 4);
                        for(int i = 0; i < list.Count; i++)
                        {
                            TreeNode treeNode3 = new TreeNode()
                            {
                                Name = GetValue(list[i][0]),
                                Text = GetValue(list[i][2]),
                                Tag = ControlType.Plan_Project_Topic,
                                ForeColor = GetValue(list[i][3]).Equals(4) ? Color.Red : Color.Black
                            };
                            currentNode.Nodes.Add(treeNode3);
                            List<object[]> list2 = SqlHelper.ExecuteColumnsQuery($"SELECT si_id, si_name, si_code, si_submit_status FROM subject_info WHERE pi_id='{treeNode3.Name}'", 4);
                            for(int j = 0; j < list2.Count; j++)
                            {
                                TreeNode treeNode4 = new TreeNode()
                                {
                                    Name = GetValue(list2[j][0]),
                                    Text = GetValue(list2[j][2]),
                                    Tag = ControlType.Plan_Project_Topic_Subtopic,
                                    ForeColor = GetValue(list2[j][3]).Equals(4) ? Color.Red : Color.Black
                                };
                                treeNode3.Nodes.Add(treeNode4);
                            }
                        }
                    }
                }
            }
            //计划
            else if(workType == WorkType.CDWork || workType == WorkType.PaperWork)
            {
                object[] _obj = null;
                if(workType == WorkType.CDWork)
                    _obj = SqlHelper.ExecuteRowsQuery($"SELECT pi_id,pi_name,pi_categor FROM project_info WHERE pi_id='{planId}'") ?? SqlHelper.ExecuteRowsQuery($"SELECT pi_id,pi_name,pi_categor FROM project_info WHERE trc_id='{planId}'") ?? SqlHelper.ExecuteRowsQuery($"SELECT pi_id,pi_name FROM project_info WHERE pi_obj_id='{planId}'");
                if(_obj == null)
                    _obj = SqlHelper.ExecuteRowsQuery($"SELECT dd_id,dd_name FROM data_dictionary WHERE dd_id='{planId}'");
                treeNode = new TreeNode()
                {
                    Name = GetValue(_obj[0]),
                    Text = GetValue(_obj[1]),
                    Tag = ControlType.Plan
                };
                //根据【计划】查询【项目/课题】集
                List<object[]> list = SqlHelper.ExecuteColumnsQuery($"SELECT pi_id,pi_code,pi_categor FROM project_info WHERE pi_obj_id='{treeNode.Name}' AND pi_worker_id='{UserHelper.GetInstance().User.UserKey}'", 3);
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
                    List<object[]> list2 = SqlHelper.ExecuteColumnsQuery($"SELECT si_id,si_code,si_categor FROM subject_info WHERE pi_id='{treeNode2.Name}' AND si_worker_id='{UserHelper.GetInstance().User.UserKey}'", 3);
                    for(int j = 0; j < list2.Count; j++)
                    {
                        TreeNode treeNode3 = new TreeNode()
                        {
                            Name = list2[j][0].ToString(),
                            Text = list2[j][1].ToString(),
                            Tag = (ControlType)list2[j][2]
                        };
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
                    }
                }
            }
            //父级（项目/课题）
            else if(workType == WorkType.ProjectWork)
            {
                object[] _obj = SqlHelper.ExecuteRowsQuery($"SELECT pi_id,pi_name,pi_categor FROM project_info WHERE pi_id='{planId}'");
                if(_obj == null)
                    _obj = SqlHelper.ExecuteRowsQuery($"SELECT dd_id,dd_name FROM data_dictionary WHERE dd_id='{planId}'");
                treeNode = new TreeNode()
                {
                    Name = _obj[0].ToString(),
                    Text = _obj[1].ToString(),
                    Tag = ControlType.Plan,
                };
                //根据【计划】查询【项目/课题】集
                List<object[]> list = SqlHelper.ExecuteColumnsQuery($"SELECT pi_id,pi_code,pi_categor FROM project_info WHERE pi_obj_id='{treeNode.Name}' AND pi_worker_id='{UserHelper.GetInstance().User.UserKey}'", 3);
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
                    List<object[]> list2 = SqlHelper.ExecuteColumnsQuery($"SELECT si_id,si_code,si_categor FROM subject_info WHERE pi_id='{treeNode2.Name}'", 3);
                    for(int j = 0; j < list2.Count; j++)
                    {
                        TreeNode treeNode3 = new TreeNode()
                        {
                            Name = list2[j][0].ToString(),
                            Text = list2[j][1].ToString(),
                            Tag = (ControlType)list2[j][2]
                        };
                        treeNode2.Nodes.Add(treeNode3);

                        List<object[]> list3 = SqlHelper.ExecuteColumnsQuery($"SELECT si_id,si_code,si_categor FROM subject_info WHERE pi_id='{treeNode3.Name}'", 3);
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
            //子级（课题/子课题）
            else if(workType == WorkType.SubjectWork)
            {
                object[] _obj = SqlHelper.ExecuteRowsQuery($"SELECT pi_id,pi_name,pi_categor FROM project_info WHERE pi_id='{planId}'");
                if(_obj == null)
                    _obj = SqlHelper.ExecuteRowsQuery($"SELECT dd_id,dd_name FROM data_dictionary WHERE dd_id='{planId}'");
                treeNode = new TreeNode()
                {
                    Name = _obj[0].ToString(),
                    Text = _obj[1].ToString(),
                    Tag = ControlType.Plan,
                };
                //根据【计划】查询【项目/课题】集
                List<object[]> list = SqlHelper.ExecuteColumnsQuery($"SELECT pi_id,pi_code,pi_categor FROM project_info WHERE pi_obj_id='{treeNode.Name}'", 3);
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
                    List<object[]> list2 = SqlHelper.ExecuteColumnsQuery($"SELECT si_id,si_code,si_categor FROM subject_info WHERE pi_id='{treeNode2.Name}'", 3);
                    for(int j = 0; j < list2.Count; j++)
                    {
                        TreeNode treeNode3 = new TreeNode()
                        {
                            Name = list2[j][0].ToString(),
                            Text = list2[j][1].ToString(),
                            Tag = (ControlType)list2[j][2]
                        };
                        treeNode2.Nodes.Add(treeNode3);

                        List<object[]> list3 = SqlHelper.ExecuteColumnsQuery($"SELECT si_id,si_code,si_categor FROM subject_info WHERE pi_id='{treeNode3.Name}'", 3);
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
            treeView.Nodes.Add(treeNode);
            treeView.ExpandAll();
            treeView.NodeMouseClick += TreeView_NodeMouseClick;

            if(treeView.Nodes.Count > 0)
            {
                TreeNode node = treeView.Nodes[0];
                if(type == ControlType.Imp || type == ControlType.Imp_Sub)
                {
                    ShowTab("imp", 0);
                    LoadImpPage(node.Name, node.ForeColor);
                }
                else if(type == ControlType.Imp_Normal)
                {
                    ShowTab("plan", 0);
                    LoadPlanPage(node.Name, System.Drawing.Color.Black);
                }
                else if(type == ControlType.Plan_Project)
                {
                    if(workType == WorkType.Default)
                    {
                        ShowTab("imp", 0);
                        LoadImpPage(node.Name, node.ForeColor);
                    }
                    else
                    {
                        ShowTab("plan", 0);
                        LoadPlanPage(node.Name, DisEnableColor);
                    }
                }
            }
        }
        /// <summary>
        /// 目录树点击事件
        /// </summary>
        private void TreeView_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            ControlType type = (ControlType)e.Node.Tag;
            if(type == ControlType.Plan)
            {
                if(workType == WorkType.Default)
                {
                    ShowTab("plan", 0);
                    LoadPlanPage(e.Node.Name, DisEnableColor);
                }
                else
                {
                    ShowTab("plan", 0);
                    LoadPlanPage(e.Node.Name, System.Drawing.Color.Black);
                }
            }
            else if(type == ControlType.Plan_Project)
            {
                tab_MenuList.TabPages.Clear();
                if(workType == WorkType.Default)
                {
                    ShowTab("imp", 0);
                    LoadImpPage(e.Node.Parent.Parent.Name, e.Node.Parent.Parent.ForeColor);
                    EnabledBtnGroup(ControlType.Imp, GetIsBacked(e.Node.Parent.Parent.Name, ControlType.Imp));

                    ShowTab("imp_dev", 1);
                    LoadPageBasicInfo(e.Node.Parent.Name, ControlType.Imp_Sub, e.Node.Parent.ForeColor);
                    EnabledBtnGroup(ControlType.Imp_Sub, GetIsBacked(e.Node.Parent.Name, ControlType.Imp_Sub));

                    ShowTab("plan_project", 2);
                    LoadPageBasicInfo(e.Node.Name, type, e.Node.ForeColor);
                    EnabledBtnGroup(ControlType.Plan_Project, GetIsBacked(e.Node.Name, ControlType.Plan_Project));
                }
                else if(workType == WorkType.CDWork || workType == WorkType.PaperWork)
                {
                    ShowTab("plan", 0);
                    LoadPlanPage(e.Node.Parent.Name, System.Drawing.Color.Black);

                    ShowTab("plan_project", 1);
                    LoadPageBasicInfo(e.Node.Name, type);
                }
                else if(workType == WorkType.ProjectWork)
                {
                    ShowTab("plan", 0);
                    LoadPlanPage(e.Node.Parent.Name, System.Drawing.Color.Black);

                    ShowTab("plan_project", 1);
                    LoadPageBasicInfo(e.Node.Name, type);
                }
                else if(workType == WorkType.SubjectWork)
                {
                    ShowTab("plan", 0);
                    LoadPlanPage(e.Node.Parent.Name, System.Drawing.Color.Black);

                    ShowTab("plan_project", 1);
                    LoadPageBasicInfo(e.Node.Name, type);
                }
            }
            else if(type == ControlType.Plan_Topic)
            {
                tab_MenuList.TabPages.Clear();
                if(workType == WorkType.Default)
                {
                    ShowTab("imp", 0);
                    LoadImpPage(e.Node.Parent.Parent.Name, e.Node.Parent.Parent.ForeColor);

                    ShowTab("imp_sub", 1);
                    LoadPageBasicInfo(e.Node.Parent.Name, ControlType.Imp_Sub);

                    ShowTab("plan_project", 2);
                    LoadPageBasicInfo(e.Node.Name, type);
                }
                else
                {
                    ShowTab("plan", 0);
                    LoadPlanPage(e.Node.Parent.Name, System.Drawing.Color.Black);

                    ShowTab("plan_topic", 1);
                    LoadPageBasicInfo(e.Node.Name, type);
                }
            }
            else if(type == ControlType.Plan_Project_Topic)
            {
                tab_MenuList.TabPages.Clear();
                if(workType == WorkType.Default)
                {
                    ShowTab("plan", 0);
                    LoadPlanPage(e.Node.Parent.Parent.Name, e.Node.Parent.Parent.ForeColor);
                    EnabledBtnGroup(ControlType.Plan, GetIsBacked(e.Node.Parent.Parent.Name, ControlType.Plan));

                    ShowTab("plan_project", 1);
                    LoadPageBasicInfo(e.Node.Parent.Name, ControlType.Plan_Project, e.Node.Parent.ForeColor);
                    EnabledBtnGroup(ControlType.Plan_Project, GetIsBacked(e.Node.Parent.Name, ControlType.Plan_Project));

                    ShowTab("plan_project_topic", 2);
                    LoadPageBasicInfo(e.Node.Name, ControlType.Plan_Project_Topic, e.Node.ForeColor);
                    EnabledBtnGroup(ControlType.Plan_Project_Topic, GetIsBacked(e.Node.Name, ControlType.Plan_Project_Topic));
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
                    LoadPageBasicInfo(e.Node.Parent.Parent.Name, ControlType.Imp_Sub);

                    ShowTab("plan_topic", 2);
                    LoadPageBasicInfo(e.Node.Parent.Name, ControlType.Plan_Topic);

                    ShowTab("plan_topic_subtopic", 3);
                    LoadPageBasicInfo(e.Node.Name, ControlType.Plan_Topic_Subtopic);
                }
                else
                {

                    ShowTab("plan", 0);
                    LoadPlanPage(e.Node.Parent.Parent.Name, System.Drawing.Color.Black);

                    ShowTab("plan_topic", 1);
                    LoadPageBasicInfo(e.Node.Parent.Name, ControlType.Plan_Topic);

                    ShowTab("plan_topic_subtopic", 2);
                    LoadPageBasicInfo(e.Node.Name, type);
                }
            }
            else if(type == ControlType.Plan_Project_Topic_Subtopic)
            {
                if(workType == WorkType.Default)
                {
                    ShowTab("imp", 0);
                    LoadImpPage(e.Node.Parent.Parent.Parent.Name, e.Node.Parent.Parent.Parent.ForeColor);

                    ShowTab("imp_dev", 1);
                    LoadPageBasicInfo(e.Node.Parent.Parent.Name, ControlType.Imp_Sub);

                    ShowTab("plan_project", 2);
                    LoadPageBasicInfo(e.Node.Parent.Name, ControlType.Plan_Project);

                    ShowTab("plan_project_topic", 3);
                    LoadPageBasicInfo(e.Node.Name, ControlType.Plan_Project_Topic);

                    ShowTab("plan_project_topic_subtopic", 4);
                    LoadPageBasicInfo(e.Node.Name, ControlType.Plan_Project_Topic_Subtopic);
                }
                else
                {
                    tab_MenuList.TabPages.Clear();

                    ShowTab("plan", 0);
                    LoadPlanPage(e.Node.Parent.Parent.Parent.Name, System.Drawing.Color.Black);

                    ShowTab("plan_project", 1);
                    LoadPageBasicInfo(e.Node.Parent.Parent.Name, ControlType.Plan_Project);

                    ShowTab("plan_project_topic", 2);
                    LoadPageBasicInfo(e.Node.Parent.Name, ControlType.Plan_Project_Topic);

                    ShowTab("plan_project_topic_subtopic", 3);
                    LoadPageBasicInfo(e.Node.Name, type);
                }
            }
            else if(type == ControlType.Imp)
            {
                tab_MenuList.TabPages.Clear();

                ShowTab("imp", 0);
                LoadImpPage(e.Node.Name, e.Node.ForeColor);
                EnabledBtnGroup(ControlType.Imp, GetIsBacked(e.Node.Name, ControlType.Imp));
            }
            else if(type == ControlType.Imp_Normal)
            {
                tab_MenuList.TabPages.Clear();

                ShowTab("plan", 0);
                LoadPlanPage(e.Node.Name, System.Drawing.Color.Black);
            }
            else if(type == ControlType.Imp_Dev || type == ControlType.Imp_Sub)
            {
                tab_MenuList.TabPages.Clear();

                ShowTab("imp", 0);
                LoadImpPage(e.Node.Parent.Name, e.Node.Parent.ForeColor);
                EnabledBtnGroup(ControlType.Imp, GetIsBacked(e.Node.Parent.Name, ControlType.Imp));

                ShowTab("imp_dev", 1);
                LoadPageBasicInfo(e.Node.Name, ControlType.Imp_Sub, e.Node.ForeColor);
                EnabledBtnGroup(ControlType.Imp_Sub, GetIsBacked(e.Node.Name, ControlType.Imp_Sub));
            }
            //if(tab_MenuList.TabPages.Count > 0)
            //    tab_MenuList.SelectedIndex = tab_MenuList.TabPages.Count - 1;
        }
        /// <summary>
        /// 加载基本信息【返工专用】
        /// </summary>
        private void LoadPageBasicInfo(string name, ControlType type, Color color)
        {
            LoadPageBasicInfo(name, type);
            if(color == DisEnableColor)
            {
                if(type == ControlType.Plan_Project)
                    pal_JH_XM_BtnGroup.Enabled = false;
                else if(type == ControlType.Plan_Project_Topic)
                    pal_JH_XM_KT_BtnGroup.Enabled = false;
                else if(type == ControlType.Imp_Sub)
                    pal_Imp_Dev_BtnGroup.Enabled = false;
            }
        }
       
        /// <summary>
        /// 获取指定ID是否已返工
        /// </summary>
        private bool GetIsBacked(string id, ControlType type)
        {
            if(type == ControlType.Imp)
            {
                object obj = SqlHelper.ExecuteOnlyOneQuery($"SELECT imp_submit_status FROM imp_info WHERE imp_id='{id}'");
                if(string.IsNullOrEmpty(GetValue(obj)))
                    return false;
                else
                    return (int)obj == 1 ? true : false;
            }
            else if(type == ControlType.Imp_Sub)
            {
                object obj = SqlHelper.ExecuteOnlyOneQuery($"SELECT imp_submit_status FROM imp_dev_info WHERE imp_id='{id}'");
                if(string.IsNullOrEmpty(GetValue(obj)))
                    return false;
                else
                    return (int)obj == 1 ? true : false;
            }
            else if(type == ControlType.Plan || type == ControlType.Plan_Project)
            {
                object obj = SqlHelper.ExecuteOnlyOneQuery($"SELECT pi_submit_status FROM project_info WHERE pi_id='{id}'");
                if(string.IsNullOrEmpty(GetValue(obj)))
                    return false;
                else
                    return (int)obj == 1 ? true : false;
            }
            else if(type == ControlType.Plan_Project_Topic)
            {
                object obj = SqlHelper.ExecuteOnlyOneQuery($"SELECT si_submit_status FROM subject_info WHERE si_id='{id}'");
                if(string.IsNullOrEmpty(GetValue(obj)))
                    return false;
                else
                {
                    if((ObjectSubmitStatus)obj == ObjectSubmitStatus.Back)
                        return true;
                    else
                        return false;
                }
            }
            return false;
        }
        
        /// <summary>
        /// 加载Imp基本信息
        /// </summary>
        private void LoadImpPage(object objId, System.Drawing.Color color)
        {
            object[] _obj = SqlHelper.ExecuteRowsQuery($"SELECT imp_id, imp_name, imp_intro, imp_type FROM imp_info WHERE imp_id='{objId}'");
            lbl_Imp_Name.Tag = GetValue(_obj[0]);
            lbl_Imp_Name.Text = GetValue(_obj[1]);
            lbl_Imp_Intro.Text = GetValue(_obj[2]);

            dgv_Imp_FileList.Tag = GetValue(_obj[0]);
            LoadFileList(dgv_Imp_FileList, "imp_", GetValue(_obj[0]));

            //加载下拉列表
            if(cbo_Imp_HasNext.DataSource == null)
            {
                int type = Convert.ToInt32(_obj[3]);
                string key = null;
                if(type == 0)//重点研发
                    key = "dic_plan_imp";
                else if(type == 1)
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

            pal_Imp_BtnGroup.Enabled = !(color == DisEnableColor);

        }
        /// <summary>
        /// 文件信息选项卡切换事件
        /// </summary>
        private void Tab_FileInfo_SelectedIndexChanged(object sender, EventArgs e)
        {
            TabControl tab = sender as TabControl;
            int index = tab.SelectedIndex;
            if("tab_JH_FileInfo".Equals(tab.Name))
            {
                object objid = dgv_JH_FileList.Tag;
                if(index == 1)//文件核查
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
                    }
                }
                else if(index == 3)
                {
                    LoadBoxList(dgv_JH_FileList.Tag, ControlType.Plan);
                    LoadFileBoxTable(cbo_JH_Box.SelectedValue, dgv_JH_FileList.Tag, ControlType.Plan);
                }
            }
            else if("tab_JH_XM_FileInfo".Equals(tab.Name))
            {
                object objid = dgv_JH_XM_FileList.Tag;
                if(index == 1)//文件核查
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
                    }
                }
                else if(index == 3)
                {
                    LoadBoxList(dgv_JH_XM_FileList.Tag, ControlType.Plan_Project);
                    LoadFileBoxTable(cbo_JH_XM_Box.SelectedValue, dgv_JH_XM_FileList.Tag, ControlType.Plan_Project);
                }
            }
            else if("tab_JH_KT_FileInfo".Equals(tab.Name))
            {
                object objid = dgv_JH_KT_FileList.Tag;
                if(index == 1)//文件核查
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
                    }
                }
                else if(index == 3)
                {
                    LoadBoxList(dgv_JH_KT_FileList.Tag, ControlType.Plan_Topic);
                    LoadFileBoxTable(cbo_JH_KT_Box.SelectedValue, dgv_JH_KT_FileList.Tag, ControlType.Plan_Topic);
                }
            }
            else if("tab_JH_XM_KT_FileInfo".Equals(tab.Name))
            {
                object objid = dgv_JH_XM_KT_FileList.Tag;
                if(index == 1)//文件核查
                {
                    if(objid != null)
                    {
                        LoadFileValidList(dgv_JH_XM_KT_FileValid, objid, "dgv_jh_xm_kt_");
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
                            txt_JH_XM_KT_AJ_Code.Tag = GetValue(row["pt_id"]);
                            txt_JH_XM_KT_AJ_Code.Text = GetValue(row["pt_code"]);
                            txt_JH_XM_KT_AJ_Name.Text = GetValue(row["pt_name"]);
                            txt_JH_XM_KT_AJ_Term.Text = GetValue(row["pt_term"]);
                            txt_JH_XM_KT_AJ_Secret.Text = GetValue(row["pt_secret"]);
                            txt_JH_XM_KT_AJ_User.Text = GetValue(row["pt_user"]);
                            txt_JH_XM_KT_AJ_Unit.Text = GetValue(row["pt_unit"]);
                        }
                    }
                }
                else if(index == 3)
                {
                    LoadBoxList(dgv_JH_XM_KT_FileList.Tag, ControlType.Plan_Project_Topic);
                    LoadFileBoxTable(cbo_JH_XM_KT_Box.SelectedValue, dgv_JH_XM_KT_FileList.Tag, ControlType.Plan_Project_Topic);
                }
            }
            else if("tab_JH_XM_KT_ZKT_FileInfo".Equals(tab.Name))
            {
                object objid = dgv_JH_XM_KT_ZKT_FileList.Tag;
                if(index == 1)//文件核查
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
                    }
                }
                else if(index == 3)
                {
                    LoadBoxList(dgv_JH_XM_KT_ZKT_FileList.Tag, ControlType.Plan_Project_Topic_Subtopic);
                    LoadFileBoxTable(cbo_JH_XM_KT_ZKT_Box.SelectedValue, dgv_JH_XM_KT_ZKT_FileList.Tag, ControlType.Plan_Project_Topic_Subtopic);
                }
            }
            else if("tab_JH_KT_ZKT_FileInfo".Equals(tab.Name))
            {
                object objid = dgv_JH_KT_ZKT_FileList.Tag;
                if(index == 1)//文件核查
                {
                    if(objid != null)
                    {
                        LoadFileValidList(dgv_JH_KT_ZKT_FileValid, objid, "dgv_jh_kt_zkt_");
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
                            txt_JH_KT_ZKT_AJ_Code.Tag = GetValue(row["pt_id"]);
                            txt_JH_KT_ZKT_AJ_Code.Text = GetValue(row["pt_code"]);
                            txt_JH_KT_ZKT_AJ_Name.Text = GetValue(row["pt_name"]);
                            txt_JH_KT_ZKT_AJ_Term.Text = GetValue(row["pt_term"]);
                            txt_JH_KT_ZKT_AJ_Secret.Text = GetValue(row["pt_secret"]);
                            txt_JH_KT_ZKT_AJ_User.Text = GetValue(row["pt_user"]);
                            txt_JH_KT_ZKT_AJ_Unit.Text = GetValue(row["pt_unit"]);
                        }
                    }
                }
                else if(index == 3)
                {
                    LoadBoxList(dgv_JH_KT_ZKT_FileList.Tag, ControlType.Plan_Topic_Subtopic);
                    LoadFileBoxTable(cbo_JH_KT_ZKT_Box.SelectedValue, dgv_JH_KT_ZKT_FileList.Tag, ControlType.Plan_Topic_Subtopic);
                }
            }
            else if("tab_Imp_FileInfo".Equals(tab.Name))
            {
                object objid = dgv_Imp_FileList.Tag;
                if(index == 1)//文件核查
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
                    }
                }
                else if(index == 3)
                {
                    LoadBoxList(dgv_Imp_FileList.Tag, ControlType.Imp);
                    LoadFileBoxTable(cbo_Imp_Box.SelectedValue, dgv_Imp_FileList.Tag, ControlType.Imp);
                }
            }
            else if("tab_Imp_Dev_FileInfo".Equals(tab.Name))
            {
                object objid = dgv_Imp_Dev_FileList.Tag;
                if(index == 1)//文件核查
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
                    }
                }
                else if(index == 3)
                {
                    LoadBoxList(dgv_Imp_Dev_FileList.Tag, ControlType.Imp_Dev);
                    LoadFileBoxTable(cbo_Imp_Dev_Box.SelectedValue, dgv_Imp_Dev_FileList.Tag, ControlType.Imp_Dev);
                }
            }
        }
        /// <summary>
        /// 加载文件缺失校验列表
        /// </summary>
        /// <param name="dataGridView">待校验表格</param>
        /// <param name="objid">主键</param>
        private void LoadFileValidList(DataGridView dataGridView, object objid, string key)
        {
            dataGridView.Rows.Clear();

            DataTable table = SqlHelper.ExecuteQuery($"SELECT * FROM processing_file_lost WHERE pfo_obj_id='{objid}' ORDER BY pfo_categor");
            for (int i = 0; i < table.Rows.Count; i++)
            {
                int indexRow = dataGridView.Rows.Add();
                dataGridView.Rows[indexRow].Cells[key + "id"].Value = i + 1;
                dataGridView.Rows[indexRow].Cells[key + "categor"].Value = table.Rows[i]["pfo_categor"];
                dataGridView.Rows[indexRow].Cells[key + "name"].Value = table.Rows[i]["pfo_name"];
                dataGridView.Rows[indexRow].Cells[key + "id"].Tag = table.Rows[i]["pfo_id"];
                dataGridView.Rows[indexRow].Cells[key + "reason"].Value = table.Rows[i]["pfo_reason"];
                dataGridView.Rows[indexRow].Cells[key + "remark"].Value = table.Rows[i]["pfo_remark"];
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
                LoadFileBoxTableInstance(lsv_JH_File1, lsv_JH_File2, "jh", pbId, objId);
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
            else if(type == ControlType.Plan_Project_Topic)
            {
                txt_JH_XM_KT_Box_GCID.Text = GCID;
                LoadFileBoxTableInstance(lsv_JH_XM_KT_File1, lsv_JH_XM_KT_File2, "jh_xm_kt", pbId, objId);
            }
            else if(type == ControlType.Plan_Topic_Subtopic)
            {
                txt_JH_KT_ZKT_Box_GCID.Text = GCID;
                LoadFileBoxTableInstance(lsv_JH_KT_ZKT_File1, lsv_JH_KT_ZKT_File2, "jh_kt_zkt", pbId, objId);
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
        /// 将object对象转换成string
        /// </summary>
        private string GetValue(object obj) => obj == null ? string.Empty : obj.ToString();
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
        /// 案卷归档事件
        /// </summary>
        private void Btn_Box_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;
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
                    LoadFileBoxTable(value, dgv_JH_FileList.Tag, ControlType.Plan);
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
                    LoadFileBoxTable(value, dgv_JH_KT_FileList.Tag, ControlType.Plan_Project);
                }
                else
                    MessageBox.Show("请先添加案卷盒。", "保存失败");
            }
            //计划-课题-子课题
            else if(button.Name.Contains("btn_JH_KT_ZKT_Box"))
            {
                object value = cbo_JH_KT_ZKT_Box.SelectedValue;
                if(value != null)
                {
                    if("btn_JH_KT_ZKT_Box_Right".Equals(button.Name))
                    {
                        if(lsv_JH_KT_ZKT_File1.SelectedItems.Count > 0)
                        {
                            int count = lsv_JH_KT_ZKT_File1.SelectedItems.Count;
                            if(count > 0)
                            {
                                object[] _obj = new object[count];
                                for(int i = 0; i < count; i++)
                                    _obj[i] = lsv_JH_KT_ZKT_File1.SelectedItems[i].SubItems[0].Text;
                                SetFileState(_obj, value, true);
                            }
                        }
                    }
                    else if("btn_JH_KT_ZKT_Box_RightAll".Equals(button.Name))
                    {
                        int count = lsv_JH_KT_ZKT_File1.Items.Count;
                        if(count > 0)
                        {
                            object[] _obj = new object[count];
                            for(int i = 0; i < count; i++)
                                _obj[i] = lsv_JH_KT_ZKT_File1.Items[i].SubItems[0].Text;
                            SetFileState(_obj, value, true);
                        }
                    }
                    else if("btn_JH_KT_ZKT_Box_Left".Equals(button.Name))
                    {
                        int count = lsv_JH_KT_ZKT_File2.SelectedItems.Count;
                        if(count > 0)
                        {
                            object[] _obj = new object[count];
                            for(int i = 0; i < count; i++)
                                _obj[i] = lsv_JH_KT_ZKT_File2.SelectedItems[i].SubItems[0].Text;
                            SetFileState(_obj, value, false);
                        }
                    }
                    else if("btn_JH_KT_ZKT_Box_LeftAll".Equals(button.Name))
                    {
                        int count = lsv_JH_KT_ZKT_File2.Items.Count;
                        if(count > 0)
                        {
                            object[] _obj = new object[count];
                            for(int i = 0; i < count; i++)
                                _obj[i] = lsv_JH_KT_ZKT_File2.Items[i].SubItems[0].Text;
                            SetFileState(_obj, value, false);
                        }
                    }
                    LoadFileBoxTable(value, dgv_JH_KT_ZKT_FileList.Tag, ControlType.Plan_Topic_Subtopic);
                }
                else
                    MessageBox.Show("请先添加案卷盒。", "保存失败");
            }
            //计划-项目-课题
            else if(button.Name.Contains("btn_JH_XM_KT_Box"))
            {
                object value = cbo_JH_XM_KT_Box.SelectedValue;
                if(value != null)
                {
                    if("btn_JH_XM_KT_Box_Right".Equals(button.Name))
                    {
                        if(lsv_JH_XM_KT_File1.SelectedItems.Count > 0)
                        {
                            int count = lsv_JH_XM_KT_File1.SelectedItems.Count;
                            if(count > 0)
                            {
                                object[] _obj = new object[count];
                                for(int i = 0; i < count; i++)
                                    _obj[i] = lsv_JH_XM_KT_File1.SelectedItems[i].SubItems[0].Text;
                                SetFileState(_obj, value, true);
                            }
                        }
                    }
                    else if("btn_JH_XM_KT_Box_RightAll".Equals(button.Name))
                    {
                        int count = lsv_JH_XM_KT_File1.Items.Count;
                        if(count > 0)
                        {
                            object[] _obj = new object[count];
                            for(int i = 0; i < count; i++)
                                _obj[i] = lsv_JH_XM_KT_File1.Items[i].SubItems[0].Text;
                            SetFileState(_obj, value, true);
                        }
                    }
                    else if("btn_JH_XM_KT_Box_Left".Equals(button.Name))
                    {
                        int count = lsv_JH_XM_KT_File2.SelectedItems.Count;
                        if(count > 0)
                        {
                            object[] _obj = new object[count];
                            for(int i = 0; i < count; i++)
                                _obj[i] = lsv_JH_XM_KT_File2.SelectedItems[i].SubItems[0].Text;
                            SetFileState(_obj, value, false);
                        }
                    }
                    else if("btn_JH_XM_KT_Box_LeftAll".Equals(button.Name))
                    {
                        int count = lsv_JH_XM_KT_File2.Items.Count;
                        if(count > 0)
                        {
                            object[] _obj = new object[count];
                            for(int i = 0; i < count; i++)
                                _obj[i] = lsv_JH_XM_KT_File2.Items[i].SubItems[0].Text;
                            SetFileState(_obj, value, false);
                        }
                    }
                    LoadFileBoxTable(value, dgv_JH_XM_KT_FileList.Tag, ControlType.Plan_Project_Topic);
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
                        string gch = txt_JH_Box_GCID.Text;
                        string insertSql = $"INSERT INTO processing_box VALUES('{Guid.NewGuid().ToString()}','{amount + 1}','{gch}',null,'{objId}')";
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
                object objId = dgv_JH_XM_FileList.Tag;
                if(objId != null)
                {
                    if("lbl_JH_XM_Box_Add".Equals(label.Name))//新增
                    {
                        //当前已有盒号数量
                        int amount = Convert.ToInt32(SqlHelper.ExecuteOnlyOneQuery($"SELECT COUNT(pb_box_number) FROM processing_box WHERE pb_obj_id='{objId}'"));
                        string gch = txt_JH_XM_Box_GCID.Text;
                        string insertSql = $"INSERT INTO processing_box VALUES('{Guid.NewGuid().ToString()}','{amount + 1}','{gch}',null,'{objId}')";
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
            //计划-项目-课题
            if(label.Name.Contains("lbl_JH_XM_KT_Box"))
            {
                object objId = dgv_JH_XM_KT_FileList.Tag;
                if(objId != null)
                {
                    if("lbl_JH_XM_KT_Box_Add".Equals(label.Name))//新增
                    {
                        //当前已有盒号数量
                        int amount = Convert.ToInt32(SqlHelper.ExecuteOnlyOneQuery($"SELECT COUNT(pb_box_number) FROM processing_box WHERE pb_obj_id='{objId}'"));
                        string gch = txt_JH_XM_KT_Box_GCID.Text;
                        string insertSql = $"INSERT INTO processing_box VALUES('{Guid.NewGuid().ToString()}','{amount + 1}','{gch}',null,'{objId}')";
                        SqlHelper.ExecuteNonQuery(insertSql);
                    }
                    else if("lbl_JH_XM_KT_Box_Remove".Equals(label.Name))//删除
                    {
                        object _temp = SqlHelper.ExecuteOnlyOneQuery($"SELECT MAX(pb_box_number) FROM processing_box WHERE pb_obj_id='{objId}'");
                        if(_temp != null)
                        {
                            object value = cbo_JH_XM_KT_Box.SelectedValue;
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
                    LoadBoxList(objId, ControlType.Plan_Project_Topic);
                    LoadFileBoxTable(cbo_JH_XM_KT_Box.SelectedValue, objId, ControlType.Plan_Project_Topic);
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
                        string gch = txt_JH_XM_KT_ZKT_Box_GCID.Text;
                        string insertSql = $"INSERT INTO processing_box VALUES('{Guid.NewGuid().ToString()}','{amount + 1}','{gch}',null,'{objId}')";
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
                        string gch = txt_JH_KT_Box_GCID.Text;
                        string insertSql = $"INSERT INTO processing_box VALUES('{Guid.NewGuid().ToString()}','{amount + 1}','{gch}',null,'{objId}')";
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
            //计划-课题-子课题
            if(label.Name.Contains("lbl_JH_KT_ZKT_Box"))
            {
                object objId = dgv_JH_KT_ZKT_FileList.Tag;
                if(objId != null)
                {
                    if("lbl_JH_KT_ZKT_Box_Add".Equals(label.Name))//新增
                    {
                        //当前已有盒号数量
                        int amount = Convert.ToInt32(SqlHelper.ExecuteOnlyOneQuery($"SELECT COUNT(pb_box_number) FROM processing_box WHERE pb_obj_id='{objId}'"));
                        string gch = txt_JH_KT_ZKT_Box_GCID.Text;
                        string insertSql = $"INSERT INTO processing_box VALUES('{Guid.NewGuid().ToString()}','{amount + 1}','{gch}',null,'{objId}')";
                        SqlHelper.ExecuteNonQuery(insertSql);
                    }
                    else if("lbl_JH_KT_ZKT_Box_Remove".Equals(label.Name))//删除
                    {
                        object _temp = SqlHelper.ExecuteOnlyOneQuery($"SELECT MAX(pb_box_number) FROM processing_box WHERE pb_obj_id='{objId}'");
                        if(_temp != null)
                        {
                            object value = cbo_JH_KT_ZKT_Box.SelectedValue;
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
                    LoadBoxList(objId, ControlType.Plan_Topic_Subtopic);
                    LoadFileBoxTable(cbo_JH_KT_ZKT_Box.SelectedValue, objId, ControlType.Plan_Topic_Subtopic);
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
                        string gch = txt_Imp_Box_GCID.Text;
                        string insertSql = $"INSERT INTO processing_box VALUES('{Guid.NewGuid().ToString()}','{amount + 1}','{gch}',null,'{objId}')";
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
                        string gch = txt_Imp_Dev_Box_GCID.Text;
                        string insertSql = $"INSERT INTO processing_box VALUES('{Guid.NewGuid().ToString()}','{amount + 1}','{gch}',null,'{objId}')";
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
            if(type == ControlType.Plan)
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
            else if(type == ControlType.Plan_Project_Topic)
            {
                cbo_JH_XM_KT_Box.DataSource = table;
                cbo_JH_XM_KT_Box.DisplayMember = "pb_box_number";
                cbo_JH_XM_KT_Box.ValueMember = "pb_id";
                if(table.Rows.Count > 0)
                    cbo_JH_XM_KT_Box.SelectedIndex = 0;
            }
            else if(type == ControlType.Plan_Project_Topic_Subtopic)
            {
                cbo_JH_XM_KT_ZKT_Box.DataSource = table;
                cbo_JH_XM_KT_ZKT_Box.DisplayMember = "pb_box_number";
                cbo_JH_XM_KT_ZKT_Box.ValueMember = "pb_id";
                if(table.Rows.Count > 0)
                    cbo_JH_XM_KT_ZKT_Box.SelectedIndex = 0;
            }
            else if(type == ControlType.Plan_Topic_Subtopic)
            {
                cbo_JH_KT_ZKT_Box.DataSource = table;
                cbo_JH_KT_ZKT_Box.DisplayMember = "pb_box_number";
                cbo_JH_KT_ZKT_Box.ValueMember = "pb_id";
                if(table.Rows.Count > 0)
                    cbo_JH_KT_ZKT_Box.SelectedIndex = 0;
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
        /// <summary>
        /// 案卷盒切换事件
        /// </summary>
        private void Cbo_Box_SelectionChangeCommitted(object sender, EventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;
            if ("cbo_JH_Box".Equals(comboBox.Name))
            {
                object pbId = comboBox.SelectedValue;
                LoadFileBoxTable(pbId, dgv_JH_FileList.Tag, ControlType.Plan);
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
            else if("cbo_JH_KT_ZKT_Box".Equals(comboBox.Name))
            {
                object pbId = comboBox.SelectedValue;
                LoadFileBoxTable(pbId, dgv_JH_KT_ZKT_FileList.Tag, ControlType.Plan_Topic_Subtopic);
            }
            else if("cbo_JH_XM_KT_Box".Equals(comboBox.Name))
            {
                object pbId = comboBox.SelectedValue;
                LoadFileBoxTable(pbId, dgv_JH_XM_KT_FileList.Tag, ControlType.Plan_Project_Topic);
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
                    if(string.IsNullOrEmpty(txt_JH_XM_Code.Text))
                    {
                        dgv_JH_XM_FileList.ColumnHeadersDefaultCellStyle = DataGridViewStyleHelper.GetHeaderStyle();
                        dgv_JH_XM_FileList.DefaultCellStyle = DataGridViewStyleHelper.GetCellStyle();
                        dgv_JH_XM_FileValid.ColumnHeadersDefaultCellStyle = DataGridViewStyleHelper.GetHeaderStyle();
                        dgv_JH_XM_FileValid.DefaultCellStyle = DataGridViewStyleHelper.GetCellStyle();
                    }
                }
                else if("plan_project_topic".Equals(currentPageName))
                {
                    if(string.IsNullOrEmpty(txt_JH_XM_KT_Code.Text))
                    {
                        dgv_JH_XM_KT_FileList.ColumnHeadersDefaultCellStyle = DataGridViewStyleHelper.GetHeaderStyle();
                        dgv_JH_XM_KT_FileList.DefaultCellStyle = DataGridViewStyleHelper.GetCellStyle();
                        dgv_JH_XM_KT_FileValid.ColumnHeadersDefaultCellStyle = DataGridViewStyleHelper.GetHeaderStyle();
                        dgv_JH_XM_KT_FileValid.DefaultCellStyle = DataGridViewStyleHelper.GetCellStyle();
                    }
                }
                else if("plan_project_topic_subtopic".Equals(currentPageName))
                {
                    if(string.IsNullOrEmpty(txt_JH_XM_KT_ZKT_Code.Text))
                    {
                        dgv_JH_XM_KT_ZKT_FileList.ColumnHeadersDefaultCellStyle = DataGridViewStyleHelper.GetHeaderStyle();
                        dgv_JH_XM_KT_ZKT_FileList.DefaultCellStyle = DataGridViewStyleHelper.GetCellStyle();
                        dgv_JH_XM_KT_ZKT_FileValid.ColumnHeadersDefaultCellStyle = DataGridViewStyleHelper.GetHeaderStyle();
                        dgv_JH_XM_KT_ZKT_FileValid.DefaultCellStyle = DataGridViewStyleHelper.GetCellStyle();
                    }
                }
                else if("plan_topic_subtopic".Equals(currentPageName))
                {
                    if(string.IsNullOrEmpty(txt_JH_KT_ZKT_Code.Text))
                    {
                        dgv_JH_KT_ZKT_FileList.ColumnHeadersDefaultCellStyle = DataGridViewStyleHelper.GetHeaderStyle();
                        dgv_JH_KT_ZKT_FileList.DefaultCellStyle = DataGridViewStyleHelper.GetCellStyle();
                        dgv_JH_KT_ZKT_FileValid.ColumnHeadersDefaultCellStyle = DataGridViewStyleHelper.GetHeaderStyle();
                        dgv_JH_KT_ZKT_FileValid.DefaultCellStyle = DataGridViewStyleHelper.GetCellStyle();
                    }
                }
                else if("plan_topic".Equals(currentPageName))
                {
                    if(string.IsNullOrEmpty(txt_JH_KT_Code.Text))
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
                    if(string.IsNullOrEmpty(txt_Imp_Dev_Code.Text))
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
        /// 根据ID加载指定对象的基本信息和文件列表
        /// </summary>
        /// <param name="planId">【项目/课题】ID</param>
        /// <param name="type">对象类型</param>
        private void LoadPageBasicInfo(object projectId, ControlType type)
        {
            InitialDrowDownList(type);
            if(type == ControlType.Plan_Project)
            {
                DataTable table = SqlHelper.ExecuteQuery($"SELECT * FROM project_info WHERE pi_id='{projectId}'");
                if(table.Rows.Count > 0)
                {
                    DataRow row = table.Rows[0];
                    dgv_JH_XM_FileList.Tag = row["pi_id"];
                    txt_JH_XM_Code.Text = GetValue(row["pi_code"]);
                    txt_JH_XM_Name.Text = GetValue(row["pi_name"]);
                    txt_JH_XM_JHType.Text = GetValue(row["pi_type"]);
                    txt_JH_XM_LY.Text = GetValue(row["pb_belong"]);
                    txt_JH_XM_ZT.Text = GetValue(row["pb_belong_type"]);
                    txt_JH_XM_JF.Text = GetValue(row["pi_money"]);

                    string startTime = GetValue(row["pi_start_datetime"]);
                    DateTime _startTime = new DateTime();
                    if(DateTime.TryParse(startTime, out _startTime))
                        dtp_JH_XM_StartTime.Value = _startTime;

                    string endTime = GetValue(row["pi_end_datetime"]);
                    DateTime _endTime = new DateTime();
                    if(DateTime.TryParse(endTime, out _endTime))
                        dtp_JH_XM_EndTime.Value = _endTime;

                    txt_JH_XM_LXND.Text = GetValue(row["pi_year"]);
                    cbo_JH_XM_Unit.SelectedValue = GetValue(row["pi_company_id"]);
                    cbo_JH_XM_Province.SelectedValue = GetValue(row["pi_province"]);
                    txt_JH_XM_UnitUser.Text = GetValue(row["pi_company_user"]);
                    txt_JH_XM_ObjUser.Text = GetValue(row["pi_project_user"]);
                    txt_JH_XM_ObjIntroduct.Text = GetValue(row["pi_introduction"]);
                }
                LoadFileList(dgv_JH_XM_FileList, "jh_xm_", projectId);

                dgv_JH_XM_FileList.ColumnHeadersDefaultCellStyle = DataGridViewStyleHelper.GetHeaderStyle();
                dgv_JH_XM_FileList.DefaultCellStyle = DataGridViewStyleHelper.GetCellStyle();
                dgv_JH_XM_FileValid.ColumnHeadersDefaultCellStyle = DataGridViewStyleHelper.GetHeaderStyle();
                dgv_JH_XM_FileValid.DefaultCellStyle = DataGridViewStyleHelper.GetCellStyle();
            }
            else if(type == ControlType.Plan_Topic)
            {
                DataTable table = SqlHelper.ExecuteQuery($"SELECT * FROM project_info WHERE pi_id='{projectId}'");
                if(table.Rows.Count > 0)
                {
                    DataRow row = table.Rows[0];
                    dgv_JH_KT_FileList.Tag = row["pi_id"];
                    txt_JH_KT_Code.Text = GetValue(row["pi_code"]);
                    txt_JH_KT_Name.Text = GetValue(row["pi_name"]);
                    txt_JH_KT_Type.Text = GetValue(row["pi_type"]);
                    txt_JH_KT_LY.Text = GetValue(row["pb_belong"]);
                    txt_JH_KT_ZT.Text = GetValue(row["pb_belong_type"]);
                    txt_JH_KT_JF.Text = GetValue(row["pi_money"]);

                    string startTime = GetValue(row["pi_start_datetime"]);
                    DateTime _startTime = new DateTime();
                    if(DateTime.TryParse(startTime, out _startTime))
                        dtp_JH_KT_StartTime.Value = _startTime;

                    string endTime = GetValue(row["pi_end_datetime"]);
                    DateTime _endTime = new DateTime();
                    if(DateTime.TryParse(endTime, out _endTime))
                        dtp_JH_KT_EndTime.Value = _endTime;

                    txt_JH_KT_Year.Text = GetValue(row["pi_year"]);
                    cbo_JH_KT_Unit.SelectedValue = GetValue(row["pi_company_id"]);
                    cbo_JH_KT_Province.SelectedValue = GetValue(row["pi_province"]);
                    txt_JH_KT_UnitUser.Text = GetValue(row["pi_company_user"]);
                    txt_JH_KT_ProUser.Text = GetValue(row["pi_project_user"]);
                    txt_JH_KT_Intro.Text = GetValue(row["pi_introduction"]);
                }
                LoadFileList(dgv_JH_KT_FileList, "jh_kt_", projectId);

                dgv_JH_KT_FileList.ColumnHeadersDefaultCellStyle = DataGridViewStyleHelper.GetHeaderStyle();
                dgv_JH_KT_FileList.DefaultCellStyle = DataGridViewStyleHelper.GetCellStyle();
                dgv_JH_KT_FileValid.ColumnHeadersDefaultCellStyle = DataGridViewStyleHelper.GetHeaderStyle();
                dgv_JH_KT_FileValid.DefaultCellStyle = DataGridViewStyleHelper.GetCellStyle();
            }
            else if(type == ControlType.Plan_Project_Topic)
            {
                DataTable table = SqlHelper.ExecuteQuery($"SELECT * FROM subject_info WHERE si_id='{projectId}'");
                if(table.Rows.Count > 0)
                {
                    DataRow row = table.Rows[0];
                    dgv_JH_XM_KT_FileList.Tag = row["si_id"];
                    txt_JH_XM_KT_Code.Text = GetValue(row["si_code"]);
                    txt_JH_XM_KT_Name.Text = GetValue(row["si_name"]);
                    txt_JH_XM_KT_Type.Text = GetValue(row["si_type"]);
                    txt_JH_XM_KT_LY.Text = GetValue(row["si_field"]);
                    txt_JH_XM_KT_ZT.Text = GetValue(row["si_belong"]);
                    txt_JH_XM_KT_JF.Text = GetValue(row["si_money"]);

                    string startTime = GetValue(row["si_start_datetime"]);
                    DateTime _startTime = new DateTime();
                    if(DateTime.TryParse(startTime, out _startTime))
                        dtp_JH_XM_KT_StartTime.Value = _startTime;

                    string endTime = GetValue(row["si_end_datetime"]);
                    DateTime _endTime = new DateTime();
                    if(DateTime.TryParse(endTime, out _endTime))
                        dtp_JH_XM_KT_EndTime.Value = _endTime;

                    txt_JH_XM_KT_Year.Text = GetValue(row["si_year"]);
                    cbo_JH_XM_KT_Unit.SelectedValue = GetValue(row["si_company"]);
                    cbo_JH_XM_KT_Province.SelectedValue = GetValue(row["si_province"]);
                    txt_JH_XM_KT_UnitUser.Text = GetValue(row["si_company_user"]);
                    txt_JH_XM_KT_ProUser.Text = GetValue(row["si_project_user"]);
                    txt_JH_XM_KT_Intro.Text = GetValue(row["si_introduction"]);
                }
                LoadFileList(dgv_JH_XM_KT_FileList, "jh_xm_kt_", projectId);

                dgv_JH_XM_KT_FileList.ColumnHeadersDefaultCellStyle = DataGridViewStyleHelper.GetHeaderStyle();
                dgv_JH_XM_KT_FileList.DefaultCellStyle = DataGridViewStyleHelper.GetCellStyle();
                dgv_JH_XM_KT_FileValid.ColumnHeadersDefaultCellStyle = DataGridViewStyleHelper.GetHeaderStyle();
                dgv_JH_XM_KT_FileValid.DefaultCellStyle = DataGridViewStyleHelper.GetCellStyle();
            }
            else if(type == ControlType.Plan_Topic_Subtopic)
            {
                DataTable table = SqlHelper.ExecuteQuery($"SELECT * FROM subject_info WHERE si_id='{projectId}'");
                if(table.Rows.Count > 0)
                {
                    DataRow row = table.Rows[0];
                    dgv_JH_KT_ZKT_FileList.Tag = row["si_id"];
                    txt_JH_KT_ZKT_Code.Text = GetValue(row["si_code"]);
                    txt_JH_KT_ZKT_Name.Text = GetValue(row["si_name"]);
                    txt_JH_KT_ZKT_Type.Text = GetValue(row["si_type"]);
                    txt_JH_KT_ZKT_LY.Text = GetValue(row["si_field"]);
                    txt_JH_KT_ZKT_ZT.Text = GetValue(row["si_belong"]);
                    txt_JH_KT_ZKT_JF.Text = GetValue(row["si_money"]);

                    string startTime = GetValue(row["si_start_datetime"]);
                    DateTime _startTime = new DateTime();
                    if(DateTime.TryParse(startTime, out _startTime))
                        dtp_JH_KT_ZKT_StartTime.Value = _startTime;

                    string endTime = GetValue(row["si_end_datetime"]);
                    DateTime _endTime = new DateTime();
                    if(DateTime.TryParse(endTime, out _endTime))
                        dtp_JH_KT_ZKT_EndTime.Value = _endTime;

                    txt_JH_KT_ZKT_Year.Text = GetValue(row["si_year"]);
                    cbo_JH_KT_ZKT_Unit.SelectedValue = GetValue(row["si_company"]);
                    cbo_JH_KT_ZKT_Province.SelectedValue = GetValue(row["si_province"]);
                    txt_JH_KT_ZKT_Unituser.Text = GetValue(row["si_company_user"]);
                    txt_JH_KT_ZKT_ProUser.Text = GetValue(row["si_project_user"]);
                    txt_JH_KT_ZKT_Intro.Text = GetValue(row["si_introduction"]);
                }
                LoadFileList(dgv_JH_KT_ZKT_FileList, "jh_kt_zkt_", projectId);

                dgv_JH_KT_ZKT_FileList.ColumnHeadersDefaultCellStyle = DataGridViewStyleHelper.GetHeaderStyle();
                dgv_JH_KT_ZKT_FileList.DefaultCellStyle = DataGridViewStyleHelper.GetCellStyle();
                dgv_JH_KT_ZKT_FileValid.ColumnHeadersDefaultCellStyle = DataGridViewStyleHelper.GetHeaderStyle();
                dgv_JH_KT_ZKT_FileValid.DefaultCellStyle = DataGridViewStyleHelper.GetCellStyle();
            }
            else if(type == ControlType.Plan_Project_Topic_Subtopic)
            {
                DataTable table = SqlHelper.ExecuteQuery($"SELECT * FROM subject_info WHERE si_id='{projectId}'");
                if(table.Rows.Count > 0)
                {
                    DataRow row = table.Rows[0];
                    dgv_JH_XM_KT_ZKT_FileList.Tag = row["si_id"];
                    txt_JH_XM_KT_ZKT_Code.Text = GetValue(row["si_code"]);
                    txt_JH_XM_KT_ZKT_Name.Text = GetValue(row["si_name"]);
                    txt_JH_XM_KT_ZKT_Type.Text = GetValue(row["si_type"]);
                    txt_JH_XM_KT_ZKT_LY.Text = GetValue(row["si_field"]);
                    txt_JH_XM_KT_ZKT_ZT.Text = GetValue(row["si_belong"]);
                    txt_JH_XM_KT_ZKT_JF.Text = GetValue(row["si_money"]);

                    string startTime = GetValue(row["si_start_datetime"]);
                    DateTime _startTime = new DateTime();
                    if(DateTime.TryParse(startTime, out _startTime))
                        dtp_JH_XM_KT_ZKT_StartTime.Value = _startTime;

                    string endTime = GetValue(row["si_end_datetime"]);
                    DateTime _endTime = new DateTime();
                    if(DateTime.TryParse(endTime, out _endTime))
                        dtp_JH_XM_KT_ZKT_EndTime.Value = _endTime;

                    txt_JH_XM_KT_ZKT_Year.Text = GetValue(row["si_year"]);
                    cbo_JH_XM_KT_ZKT_Unit.SelectedValue = GetValue(row["si_company"]);
                    cbo_JH_XM_KT_ZKT_Province.SelectedValue = GetValue(row["si_province"]);
                    txt_JH_XM_KT_ZKT_Unituser.Text = GetValue(row["si_company_user"]);
                    txt_JH_XM_KT_ZKT_Prouser.Text = GetValue(row["si_project_user"]);
                    txt_JH_XM_KT_ZKT_Intro.Text = GetValue(row["si_introduction"]);
                }
                LoadFileList(dgv_JH_XM_KT_ZKT_FileList, "jh_xm_kt_zkt_", projectId);

                dgv_JH_XM_KT_ZKT_FileList.ColumnHeadersDefaultCellStyle = DataGridViewStyleHelper.GetHeaderStyle();
                dgv_JH_XM_KT_ZKT_FileList.DefaultCellStyle = DataGridViewStyleHelper.GetCellStyle();
                dgv_JH_XM_KT_ZKT_FileValid.ColumnHeadersDefaultCellStyle = DataGridViewStyleHelper.GetHeaderStyle();
                dgv_JH_XM_KT_ZKT_FileValid.DefaultCellStyle = DataGridViewStyleHelper.GetCellStyle();
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
                }
                else
                {
                    object[] obj = SqlHelper.ExecuteRowsQuery($"SELECT dd_id,dd_code,dd_name FROM data_dictionary WHERE dd_id='{projectId}'");
                    if(obj != null)
                    {
                        txt_Imp_Dev_Name.Text = GetValue(obj[2]);
                    }
                }
                cbo_Imp_Dev_HasNext.SelectedIndex = 0;

                dgv_Imp_Dev_FileList.ColumnHeadersDefaultCellStyle = DataGridViewStyleHelper.GetHeaderStyle();
                dgv_Imp_Dev_FileList.DefaultCellStyle = DataGridViewStyleHelper.GetCellStyle();
                dgv_Imp_Dev_FileValid.ColumnHeadersDefaultCellStyle = DataGridViewStyleHelper.GetHeaderStyle();
                dgv_Imp_Dev_FileValid.DefaultCellStyle = DataGridViewStyleHelper.GetCellStyle();
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
                txt_JH_XM_Code.Clear();
                txt_JH_XM_Name.Clear();
                txt_JH_XM_JHType.Clear();
                txt_JH_XM_LY.Clear();
                txt_JH_XM_ZT.Clear();
                txt_JH_XM_JF.ResetText();
                dtp_JH_XM_StartTime.ResetText();
                dtp_JH_XM_EndTime.ResetText();
                txt_JH_XM_LXND.Clear();
                txt_JH_XM_UnitUser.Clear();
                txt_JH_XM_ObjUser.Clear();
                txt_JH_XM_ObjIntroduct.Clear();
            }
            else if(type == ControlType.Plan_Topic)
            {
                dgv_JH_KT_FileList.Tag = null;
                txt_JH_KT_Code.Clear();
                txt_JH_KT_Name.Clear();
                txt_JH_KT_Type.Clear();
                txt_JH_KT_LY.Clear();
                txt_JH_KT_ZT.Clear();
                txt_JH_KT_JF.ResetText();
                dtp_JH_KT_StartTime.ResetText();
                dtp_JH_KT_EndTime.ResetText();
                txt_JH_KT_Year.Clear();
                txt_JH_KT_UnitUser.Clear();
                txt_JH_KT_ProUser.Clear();
                txt_JH_KT_Intro.Clear();
            }
            else if(type == ControlType.Plan_Project_Topic)
            {
                dgv_JH_XM_KT_FileList.Tag = null;
                txt_JH_XM_KT_Code.Clear();
                txt_JH_XM_KT_Name.Clear();
                txt_JH_XM_KT_Type.Clear();
                txt_JH_XM_KT_LY.Clear();
                txt_JH_XM_KT_ZT.Clear();
                txt_JH_XM_KT_JF.ResetText();
                dtp_JH_XM_KT_StartTime.ResetText();
                dtp_JH_XM_KT_EndTime.ResetText();
                txt_JH_XM_KT_Year.Clear();
                txt_JH_XM_KT_UnitUser.Clear();
                txt_JH_XM_KT_ProUser.Clear();
                txt_JH_XM_KT_Intro.Clear();
            }
            else if(type == ControlType.Plan_Topic_Subtopic)
            {
                dgv_JH_KT_ZKT_FileList.Tag = null;
                txt_JH_KT_ZKT_Code.Clear();
                txt_JH_KT_ZKT_Name.Clear();
                txt_JH_KT_ZKT_Type.Clear();
                txt_JH_KT_ZKT_LY.Clear();
                txt_JH_KT_ZKT_ZT.Clear();
                txt_JH_KT_ZKT_JF.ResetText();
                dtp_JH_KT_ZKT_StartTime.ResetText();
                dtp_JH_KT_ZKT_EndTime.ResetText();
                txt_JH_KT_ZKT_Year.Clear();
                txt_JH_KT_ZKT_Unituser.Clear();
                txt_JH_KT_ZKT_ProUser.Clear();
                txt_JH_KT_ZKT_Intro.Clear();
            }
            else if(type == ControlType.Plan_Project_Topic_Subtopic)
            {
                dgv_JH_XM_KT_ZKT_FileList.Tag = null;
                txt_JH_XM_KT_ZKT_Code.Clear();
                txt_JH_XM_KT_ZKT_Name.Clear();
                txt_JH_XM_KT_ZKT_Type.Clear();
                txt_JH_XM_KT_ZKT_LY.Clear();
                txt_JH_XM_KT_ZKT_ZT.Clear();
                txt_JH_XM_KT_ZKT_JF.ResetText();
                dtp_JH_XM_KT_ZKT_StartTime.ResetText();
                dtp_JH_XM_KT_ZKT_EndTime.ResetText();
                txt_JH_XM_KT_ZKT_Year.Clear();
                txt_JH_XM_KT_ZKT_Unituser.Clear();
                txt_JH_XM_KT_ZKT_Prouser.Clear();
                txt_JH_XM_KT_ZKT_Intro.Clear();
            }
            else if(type == ControlType.Imp_Dev)
            {
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
        //void EnableControls(ControlType type, bool enable)
        //{
        //    if(type == ControlType.Plan)
        //    {
        //        //tab_JH_FileInfo.Enabled = pal_JH_BasicInfo.Enabled = enable;
        //        foreach(Control item in pal_JH_BtnGroup.Controls)
        //        {
        //            item.Enabled = enable;
        //            if(item.Name.Contains("Submit"))
        //                item.Text = enable ? "提交" : "已提交";
        //        }
        //    }
        //    else if(type == ControlType.Plan_Project)
        //    {
        //        //tab_JH_XM_FileInfo.Enabled = pal_JH_XM.Enabled = enable;
        //        foreach(Control item in pal_JH_XM_BtnGroup.Controls)
        //        {
        //            item.Enabled = enable;
        //            if(item.Name.Contains("Submit"))
        //                item.Text = enable ? "提交" : "已提交";
        //        }
        //    }
        //    else if(type == ControlType.Plan_Project_Topic)
        //    {
        //        //tab_JH_XM_KT_FileInfo.Enabled = pal_JH_XM_KT.Enabled = enable;
        //        foreach(Control item in pal_JH_XM_KT_BtnGroup.Controls)
        //        {
        //            item.Enabled = enable;
        //            if(item.Name.Contains("Submit"))
        //                item.Text = enable ? "提交" : "已提交";
        //        }
        //    }
        //    else if(type == ControlType.Plan_Project_Topic_Subtopic)
        //    {
        //        //tab_JH_XM_KT_ZKT_FileInfo.Enabled = pal_JH_XM_KT_ZKT.Enabled = enable;
        //        foreach(Control item in pal_JH_XM_KT_ZKT_BtnGroup.Controls)
        //        {
        //            item.Enabled = enable;
        //            if(item.Name.Contains("Submit"))
        //                item.Text = enable ? "提交" : "已提交";
        //        }
        //    }
        //    else if(type == ControlType.Plan_Topic)
        //    {
        //        //tab_JH_KT_FileInfo.Enabled = pal_JH_KT.Enabled = enable;
        //        foreach(Control item in pal_JH_KT_BtnGroup.Controls)
        //        {
        //            item.Enabled = enable;
        //            if(item.Name.Contains("Submit"))
        //                item.Text = enable ? "提交" : "已提交";
        //        }
        //    }
        //    else if(type == ControlType.Plan_Topic_Subtopic)
        //    {
        //        //tab_JH_KT_ZKT_FileInfo.Enabled = pal_JH_KT_ZKT.Enabled = enable;
        //        foreach(Control item in pal_JH_KT_ZKT_BtnGroup.Controls)
        //        {
        //            item.Enabled = enable;
        //            if(item.Name.Contains("Submit"))
        //                item.Text = enable ? "提交" : "已提交";
        //        }
        //    }
        //    else if(type == ControlType.Imp)
        //    {
        //        //tab_Imp_FileInfo.Enabled = pal_Imp.Enabled = enable;
        //        foreach(Control item in pal_Imp_BtnGroup.Controls)
        //        {
        //            item.Enabled = enable;
        //            if(item.Name.Contains("Submit"))
        //                item.Text = enable ? "提交" : "已提交";
        //        }
        //    }
        //    else if(type == ControlType.Imp_Dev)
        //    {
        //        //tab_Imp_Dev_FileInfo.Enabled = pal_Imp_Dev.Enabled = enable;
        //        foreach(Control item in pal_Imp_Dev_BtnGroup.Controls)
        //        {
        //            item.Enabled = enable;
        //            if(item.Name.Contains("Submit"))
        //                item.Text = enable ? "提交" : "已提交";
        //        }
        //    }
        //}
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
            if(MessageBox.Show("提交后不可再修改，确认要提交当前对象信息吗?", "提交确认", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                Button button = sender as Button;
                object objId = null;
                if("btn_JH_Submit".Equals(button.Name))
                {
                    objId = dgv_JH_FileList.Tag;
                }
                else if("btn_JH_XM_Submit".Equals(button.Name))
                {
                    objId = dgv_JH_XM_FileList.Tag;
                    if(objId != null)
                    {
                        SqlHelper.ExecuteNonQuery($"UPDATE project_info SET pi_submit_status='{(int)ObjectSubmitStatus.SubmitSuccess}' WHERE pi_id='{objId}'");
                    }
                    else
                        MessageBox.Show("请先保存数据.");
                }
                else if("btn_JH_KT_Submit".Equals(button.Name))
                {
                    objId = dgv_JH_KT_FileList.Tag;
                    if(objId != null)
                    {
                        SqlHelper.ExecuteNonQuery($"UPDATE project_info SET pi_submit_status='{(int)ObjectSubmitStatus.SubmitSuccess}' WHERE pi_id='{objId}'");
                    }
                    else
                        MessageBox.Show("请先保存数据.");
                }
                else if("btn_JH_XM_KT_Submit".Equals(button.Name))
                {
                    objId = dgv_JH_XM_KT_FileList.Tag;
                    if(objId != null)
                    {
                        SqlHelper.ExecuteNonQuery($"UPDATE subject_info SET si_submit_status='{(int)ObjectSubmitStatus.SubmitSuccess}' WHERE si_id='{objId}'");
                    }
                    else
                        MessageBox.Show("请先保存数据.");
                }
                else if("btn_JH_XM_KT_ZKT_Submit".Equals(button.Name))
                {
                    objId = dgv_JH_XM_KT_ZKT_FileList.Tag;
                    if(objId != null)
                    {
                        SqlHelper.ExecuteNonQuery($"UPDATE subject_info SET si_submit_status='{(int)ObjectSubmitStatus.SubmitSuccess}' WHERE si_id='{objId}'");
                    }
                    else
                        MessageBox.Show("请先保存数据.");
                }
                else if("btn_JH_KT_ZKT_Submit".Equals(button.Name))
                {
                    objId = dgv_JH_KT_ZKT_FileList.Tag;
                    if(objId != null)
                    {
                        SqlHelper.ExecuteNonQuery($"UPDATE subject_info SET si_submit_status='{(int)ObjectSubmitStatus.SubmitSuccess}' WHERE si_id='{objId}'");
                    }
                    else
                        MessageBox.Show("请先保存数据.");
                }
                else if("btn_Imp_Dev_Submit".Equals(button.Name))
                {
                    objId = dgv_Imp_Dev_FileList.Tag;
                    if(objId != null)
                    {
                        SqlHelper.ExecuteNonQuery($"UPDATE imp_dev_info SET imp_submit_status='{(int)ObjectSubmitStatus.SubmitSuccess}' WHERE imp_id='{objId}'");
                    }
                }
            }
        }

        private void Cbo_Imp_HasNext_SelectionChangeCommitted(object sender, EventArgs e)
        {
            int _index = tab_MenuList.SelectedIndex;
            object value = cbo_Imp_HasNext.SelectedValue;
            ShowTab("imp_dev", _index + 1);
            LoadPageBasicInfo(value, ControlType.Imp_Sub);
            pal_Imp_Dev.Tag = lbl_Imp_Name.Tag;
        }
        /// <summary>
        /// 新增文件
        /// </summary>
        private void Btn_JH_AddFile_Click(object sender, EventArgs e)
        {
            object name = (sender as Button).Name;
            if("btn_JH_AddFile".Equals(name))
                new Frm_AddFile(dgv_JH_FileList, string.Empty, null).ShowDialog();
            else if("btn_JH_XM_AddFile".Equals(name))
                new Frm_AddFile(dgv_JH_XM_FileList, "jh_xm_", null).ShowDialog();
            else if("btn_JH_XM_KT_AddFile".Equals(name))
                new Frm_AddFile(dgv_JH_XM_KT_FileList, "jh_xm_kt_", null).ShowDialog();
            else if("btn_JH_XM_KT_ZKT_AddFile".Equals(name))
                new Frm_AddFile(dgv_JH_XM_KT_ZKT_FileList, "jh_xm_kt_zkt_", null).ShowDialog();
            else if("btn_JH_KT_AddFile".Equals(name))
                new Frm_AddFile(dgv_JH_KT_FileList, "jh_kt_", null).ShowDialog();
            else if("btn_JH_KT_ZKT_AddFile".Equals(name))
                new Frm_AddFile(dgv_JH_KT_ZKT_FileList, "jh_kt_zkt_", null).ShowDialog();
            else if("btn_Imp_AddFile".Equals(name))
                new Frm_AddFile(dgv_Imp_FileList, "imp_", null).ShowDialog();
            else if("btn_Imp_Dev_AddFile".Equals(name))
                new Frm_AddFile(dgv_Imp_Dev_FileList, "imp_dev_", null).ShowDialog();
        }
        /// <summary>
        /// 返工 点击事件
        /// </summary>
        private void Btn_BackWork_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("确定将当前数据返工吗？", "确认提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                string name = (sender as Control).Name;
                if("btn_Imp_BackWork".Equals(name))
                {
                    object impId = dgv_Imp_FileList.Tag;
                    SqlHelper.ExecuteNonQuery($"UPDATE imp_info SET imp_submit_status={(int)ObjectSubmitStatus.NonSubmit} WHERE imp_id='{impId}'");
                    EnabledBtnGroup(ControlType.Imp, true);
                }
                else if("btn_JH_BackWork".Equals(name))
                {
                    object piId = dgv_JH_FileList.Tag;
                    SqlHelper.ExecuteNonQuery($"UPDATE project_info SET pi_submit_status={(int)ObjectSubmitStatus.NonSubmit} WHERE pi_id='{piId}'");
                    EnabledBtnGroup(ControlType.Imp_Normal, true);
                }
                else if("btn_Imp_Dev_BackWork".Equals(name))
                {
                    object impDevId = dgv_Imp_Dev_FileList.Tag;
                    //将当前对象状态置为未提交
                    SqlHelper.ExecuteNonQuery($"UPDATE imp_dev_info SET imp_submit_status={(int)ObjectSubmitStatus.NonSubmit} WHERE imp_id='{impDevId}'");
                    EnabledBtnGroup(ControlType.Imp_Sub, true);
                }
                else if("btn_JH_XM_BackWork".Equals(name))
                {
                    object piId = dgv_JH_XM_FileList.Tag;
                    SqlHelper.ExecuteNonQuery($"UPDATE project_info SET pi_submit_status={(int)ObjectSubmitStatus.NonSubmit} WHERE pi_id='{piId}'");
                    EnabledBtnGroup(ControlType.Plan_Project, true);
                }
                else if("btn_JH_XM_KT_BackWork".Equals(name))
                {
                    object siId = dgv_JH_XM_KT_FileList.Tag;
                    SqlHelper.ExecuteNonQuery($"UPDATE subject_info SET si_submit_status={(int)ObjectSubmitStatus.NonSubmit} WHERE si_id='{siId}'");
                    EnabledBtnGroup(ControlType.Plan_Project_Topic, true);
                }
            }
        }

        /// <summary>
        /// 已返工 禁用按钮
        /// </summary>
        /// <param name="controlType">页面类型</param>
        /// <param name="disEnable">是否禁用</param>
        private void EnabledBtnGroup(ControlType controlType, bool disEnable)
        {
            if(controlType == ControlType.Imp)
            {
                pal_Imp_BtnGroup.Enabled = !disEnable;
                btn_Imp_BackWork.Text = disEnable ? "已返工" : "返工";
            }
            else if(controlType == ControlType.Imp_Normal)
            {
                pal_JH_BtnGroup.Enabled = !disEnable;
                btn_JH_BackWork.Text = disEnable ? "已返工" : "返工";
            }
            else if(controlType == ControlType.Imp_Sub)
            {
                pal_Imp_Dev_BtnGroup.Enabled = !disEnable;
                btn_Imp_Dev_BackWork.Text = disEnable ? "已返工" : "返工";
            }
            else if(controlType == ControlType.Plan_Project)
            {
                pal_JH_XM_BtnGroup.Enabled = !disEnable;
                btn_JH_XM_BackWork.Text = disEnable ? "已返工" : "返工";
            }
            else if(controlType == ControlType.Plan_Project_Topic)
            {
                pal_JH_XM_KT_BtnGroup.Enabled = !disEnable;
                btn_JH_XM_KT_BackWork.Text = disEnable ? "已返工" : "返工";
            }
        }

        private void Frm_MyWorkQT_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(MessageBox.Show("请确保所有数据已保存,是否继续确认退出?", "确认提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk) == DialogResult.OK)
            {
                DialogResult = DialogResult.OK;
            }
            else
                e.Cancel = true;
        }
    }
}
