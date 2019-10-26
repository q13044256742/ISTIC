using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using 科技计划项目档案数据采集管理系统.KyoControl;

namespace 科技计划项目档案数据采集管理系统
{
    public partial class Frm_ProDetails : XtraForm
    {
        private object objectID;
        /// <summary>
        /// 对象所属来源单位编号
        /// </summary>
        private object UnitCode;
        public Frm_ProDetails(object objectID)
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
            this.objectID = objectID;
            InitialForm();

        }

        private List<TabPage> pages = new List<TabPage>();
        private void InitialForm()
        {
            for (int i = 0; i < tab_MenuList.TabCount; i++)
            {
                pages.Add(tab_MenuList.TabPages[i]);
            }
            tab_MenuList.TabPages.Clear();

            tab_Project_Info.Height = 360;
            tab_Project_Info.Top = 310;
            tab_Topic_Info.Height = 360;
            tab_Topic_Info.Top = 310;
            tab_Subject_Info.Height = 360;
            tab_Subject_Info.Top = 310;

            dgv_Project_FileList.ColumnHeadersDefaultCellStyle = DataGridViewStyleHelper.GetHeaderStyle();
            dgv_Project_FileValid.DefaultCellStyle = DataGridViewStyleHelper.GetCellStyle();

            dgv_Topic_FileList.ColumnHeadersDefaultCellStyle = DataGridViewStyleHelper.GetHeaderStyle();
            dgv_Topic_FileValid.DefaultCellStyle = DataGridViewStyleHelper.GetCellStyle();

            dgv_Subject_FileList.ColumnHeadersDefaultCellStyle = DataGridViewStyleHelper.GetHeaderStyle();
            dgv_Subject_FileValid.DefaultCellStyle = DataGridViewStyleHelper.GetCellStyle();

            //阶段
            InitialStageList(dgv_Project_FileList.Columns["project_fl_stage"]);
            InitialStageList(dgv_Topic_FileList.Columns["topic_fl_stage"]);
            InitialStageList(dgv_Subject_FileList.Columns["subject_fl_stage"]);

            //载体
            InitialCarrierList(dgv_Project_FileList, "project_fl_");
            InitialCarrierList(dgv_Topic_FileList, "topic_fl_");
            InitialCarrierList(dgv_Subject_FileList, "subject_fl_");

            //类型
            InitialTypeList(dgv_Project_FileList, "project_fl_");
            InitialTypeList(dgv_Topic_FileList, "topic_fl_");
            InitialTypeList(dgv_Subject_FileList, "subject_fl_");

            //文件核查原因列表
            InitialLostReasonList(dgv_Project_FileValid, "project_fc_");
            InitialLostReasonList(dgv_Topic_FileValid, "topic_fc_");
            InitialLostReasonList(dgv_Subject_FileValid, "subject_fc_");

            //加载省市
            InitialProvinceList(cbo_Project_Province);
            InitialProvinceList(cbo_Topic_Province);
            InitialProvinceList(cbo_Subject_Province);

            dgv_Project_FileList.AutoGenerateColumns = false;
            dgv_Topic_FileList.AutoGenerateColumns = false;
            dgv_Subject_FileList.AutoGenerateColumns = false;

        }

        private void InitialTypeList(DataGridView dataGridView, string key)
        {
            DataGridViewComboBoxColumn filetypeColumn = dataGridView.Columns[key + "type"] as DataGridViewComboBoxColumn;
            filetypeColumn.DataSource = DictionaryHelper.GetTableByCode("dic_file_type");
            filetypeColumn.DisplayMember = "dd_name";
            filetypeColumn.ValueMember = "dd_id";
        }

        private void InitialProvinceList(System.Windows.Forms.ComboBox comboBox)
        {
            DataTable table = SqlHelper.GetProvinceList();
            comboBox.DataSource = table;
            comboBox.DisplayMember = "dd_name";
            comboBox.ValueMember = "dd_id";
        }

        private void InitialLostReasonList(DataGridView view, string key)
        {
            string code = "dic_file_lostreason";
            DataTable table = SqlHelper.ExecuteQuery($"SELECT * FROM data_dictionary WHERE dd_pId = (SELECT dd_id FROM data_dictionary WHERE dd_code='{code}')");
            DataGridViewComboBoxColumn comboBoxColumn = view.Columns[key + "reason"] as DataGridViewComboBoxColumn;
            comboBoxColumn.DataSource = table;
            comboBoxColumn.DisplayMember = "dd_name";
            comboBoxColumn.ValueMember = "dd_id";
        }

        private void InitialCarrierList(DataGridView dataGridView, string key)
        {
            DataGridViewComboBoxColumn carrierColumn = dataGridView.Columns[key + "carrier"] as DataGridViewComboBoxColumn;
            carrierColumn.DataSource = DictionaryHelper.GetTableByCode("dic_file_zt");
            carrierColumn.DisplayMember = "dd_name";
            carrierColumn.ValueMember = "dd_id";
        }

        private void InitialStageList(DataGridViewColumn dataGridViewColumn)
        {
            DataGridViewComboBoxColumn comboBoxColumn = dataGridViewColumn as DataGridViewComboBoxColumn;
            comboBoxColumn.DataSource = DictionaryHelper.GetTableByCode("dic_file_jd");
            comboBoxColumn.DisplayMember = "dd_name";
            comboBoxColumn.ValueMember = "dd_id";
        }

        private void Frm_ProDetails_Load(object sender, EventArgs e)
        {
            if (UserHelper.GetUserRole() == UserRole.DocManager)
            {
                pal_Project_BtnGroup.Enabled = true;
                pal_Project_MoveBtnGroup.Enabled = true;
                dgv_Project_FileList.AllowUserToAddRows = true;

                pal_Topic_BtnGroup.Enabled = true;
                pal_Topic_MoveBtnGroup.Enabled = true;
                dgv_Topic_FileList.AllowUserToAddRows = true;

                pal_Subject_BtnGroup.Enabled = true;
                pal_Subject_MoveBtnGroup.Enabled = true;
                dgv_Subject_FileList.AllowUserToAddRows = true;
            }
            else
            {
                dgv_Project_FileList.RowHeaderMouseDoubleClick -= FileList_RowHeaderMouseDoubleClick;
                dgv_Topic_FileList.RowHeaderMouseDoubleClick -= FileList_RowHeaderMouseDoubleClick;
                dgv_Subject_FileList.RowHeaderMouseDoubleClick -= FileList_RowHeaderMouseDoubleClick;
            }
            string querySQL = $"SELECT * FROM(" +
                $"SELECT pi_id, pi_code, pi_categor FROM project_info UNION ALL " +
                $"SELECT ti_id, ti_code, ti_categor FROM topic_info UNION ALL " +
                $"SELECT si_id, si_code, si_categor FROM subject_info) A WHERE A.pi_id='{objectID}' ";
            DataRow proRow = SqlHelper.ExecuteSingleRowQuery(querySQL);
            if (proRow != null)
            {
                int type = ToolHelper.GetIntValue(proRow["pi_categor"], 2);
                TreeNode treeNode = new TreeNode()
                {
                    Name = ToolHelper.GetValue(proRow["pi_id"]),
                    Text = ToolHelper.GetValue(proRow["pi_code"]),
                    Tag = type == 2 ? ControlType.Project : type == 3 || type == -3 ? ControlType.Topic : ControlType.Subject
                };
                string topQuerySQL = $"SELECT * FROM (" +
                    $"SELECT ti_id, ti_code, ti_obj_id, ti_categor FROM topic_info UNION ALL " +
                    $"SELECT si_id, si_code, si_obj_id, si_categor FROM subject_info) A WHERE A.ti_obj_id='{proRow["pi_id"]}' " +
                    $"ORDER BY A.ti_code ";
                DataTable topTable = SqlHelper.ExecuteQuery(topQuerySQL);
                foreach (DataRow topRow in topTable.Rows)
                {
                    int type2 = ToolHelper.GetIntValue(topRow["ti_categor"], 3);
                    TreeNode topNode = new TreeNode()
                    {
                        Name = ToolHelper.GetValue(topRow["ti_id"]),
                        Text = ToolHelper.GetValue(topRow["ti_code"]),
                        Tag = type2 == 3 ? ControlType.Topic : ControlType.Subject
                    };
                    treeNode.Nodes.Add(topNode);

                    string subQuerySQL = $"SELECT * FROM subject_info A WHERE A.si_obj_id='{topRow["ti_id"]}' ORDER BY si_code";
                    DataTable subTable = SqlHelper.ExecuteQuery(subQuerySQL);
                    foreach (DataRow subRow in subTable.Rows)
                    {
                        TreeNode subNode = new TreeNode()
                        {
                            Name = ToolHelper.GetValue(subRow["si_id"]),
                            Text = ToolHelper.GetValue(subRow["si_code"]),
                            Tag = ControlType.Subject
                        };
                        topNode.Nodes.Add(subNode);

                    }
                }

                treeView.Nodes.Add(treeNode);
                treeView.ExpandAll();

                ControlType firstType = (ControlType)treeNode.Tag;
                if (firstType == ControlType.Project)
                {
                    ShowTab("Project", 1);
                    LoadPageBasicInfo(treeView.Nodes[0], firstType);
                }
                else if (firstType == ControlType.Topic)
                {
                    ShowTab("Topic", 1);
                    LoadPageBasicInfo(treeView.Nodes[0], firstType);
                }
                else if (firstType == ControlType.Subject)
                {
                    ShowTab("Subject", 1);
                    LoadPageBasicInfo(treeView.Nodes[0], firstType);
                }

                if (treeView.Nodes[0].Nodes.Count == 0)
                {
                    Controls.Remove(Pal_LeftBar);
                }
            }
        }

        private void LoadPageBasicInfo(TreeNode node, ControlType type)
        {
            if (type == ControlType.Project)
            {
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
                    txt_Project_StartTime.Text = ToolHelper.GetValue(row["pi_start_datetime"]);
                    txt_Project_EndTime.Text = ToolHelper.GetValue(row["pi_end_datetime"]);
                    txt_Project_Year.Text = ToolHelper.GetValue(row["pi_year"]);
                    txt_Project_Unit.Text = ToolHelper.GetValue(row["pi_unit"]);
                    cbo_Project_Province.SelectedValue = ToolHelper.GetValue(row["pi_province"]);
                    txt_Project_UnitUser.Text = ToolHelper.GetValue(row["pi_uniter"]);
                    txt_Project_ProUser.Text = ToolHelper.GetValue(row["pi_prouser"]);
                    txt_Project_Intro.Text = ToolHelper.GetValue(row["pi_intro"]);
                    lbl_Project_Tip.Text = $"著录人：{UserHelper.GetUserNameById(row["pi_worker_id"])}          质检人：{UserHelper.GetUserNameById(row["pi_checker_id"])}";
                    Topic.Tag = row["pi_obj_id"];
                    int otherDocsCount = ToolHelper.GetIntValue(row["oCount"], 0);
                    if (otherDocsCount != 0)
                        btn_Project_OtherDoc.Text = $"其它载体档案({otherDocsCount})";
                    string orgCode = ToolHelper.GetValue(row["pi_orga_id"]);
                    txt_Project_Unit.Tag = orgCode;
                    string sourCode = ToolHelper.GetValue(row["pi_source_id"]);
                    if (sourCode.StartsWith("ZX"))
                        UnitCode = $"{orgCode}{sourCode}";
                    else
                        UnitCode = $"{orgCode}";
                }
                tab_Project_Info.SelectedTabPageIndex = 0;
                LoadFileList(dgv_Project_FileList, node.Name, -1);
            }
            else if (type == ControlType.Topic)
            {
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
                    txt_Topic_StartTime.Text = ToolHelper.GetValue(row["ti_start_datetime"]);
                    txt_Topic_EndTime.Text = ToolHelper.GetValue(row["ti_end_datetime"]);
                    txt_Topic_Year.Text = ToolHelper.GetValue(row["ti_year"]);
                    txt_Topic_Unit.Text = ToolHelper.GetValue(row["ti_unit"]);
                    cbo_Topic_Province.SelectedValue = ToolHelper.GetValue(row["ti_province"]);
                    txt_Topic_UnitUser.Text = ToolHelper.GetValue(row["ti_uniter"]);
                    txt_Topic_ProUser.Text = ToolHelper.GetValue(row["ti_prouser"]);
                    txt_Topic_Intro.Text = ToolHelper.GetValue(row["ti_intro"]);
                    lbl_Topic_Tip.Text = $"著录人：{UserHelper.GetUserNameById(row["ti_worker_id"])}          质检人：{UserHelper.GetUserNameById(row["ti_checker_id"])}";
                    Topic.Tag = row["ti_obj_id"];
                    int otherDocsCount = ToolHelper.GetIntValue(row["oCount"], 0);
                    if (otherDocsCount != 0)
                        btn_Topic_OtherDoc.Text = $"其它载体档案({otherDocsCount})";
                    string orgCode = ToolHelper.GetValue(row["ti_orga_id"]);
                    txt_Topic_Unit.Tag = orgCode;
                    string sourCode = ToolHelper.GetValue(row["ti_source_id"]);
                    if (sourCode.StartsWith("ZX"))
                        UnitCode = $"{orgCode}{sourCode}";
                    else
                        UnitCode = $"{orgCode}";
                }
                tab_Topic_Info.SelectedTabPageIndex = 0;
                LoadFileList(dgv_Topic_FileList, node.Name, -1);
            }
            else if (type == ControlType.Subject)
            {
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

                    txt_Subject_StartTime.Text = ToolHelper.GetValue(row["si_start_datetime"]);
                    txt_Subject_EndTime.Text = ToolHelper.GetValue(row["si_end_datetime"]);

                    txt_Subject_Year.Text = ToolHelper.GetValue(row["si_year"]);
                    txt_Subject_Unit.Text = ToolHelper.GetValue(row["si_unit"]);
                    cbo_Subject_Province.SelectedValue = ToolHelper.GetValue(row["si_province"]);
                    txt_Subject_Unituser.Text = ToolHelper.GetValue(row["si_uniter"]);
                    txt_Subject_ProUser.Text = ToolHelper.GetValue(row["si_prouser"]);
                    txt_Subject_Intro.Text = ToolHelper.GetValue(row["si_intro"]);
                    lbl_Subject_Tip.Text = $"著录人：{UserHelper.GetUserNameById(row["si_worker_id"])}          质检人：{UserHelper.GetUserNameById(row["si_checker_id"])}";
                    Subject.Tag = row["si_obj_id"];
                    int otherDocsCount = ToolHelper.GetIntValue(row["oCount"], 0);
                    if (otherDocsCount != 0)
                        btn_Subject_OtherDoc.Text = $"其它载体档案({otherDocsCount})";
                    string orgCode = ToolHelper.GetValue(row["si_orga_id"]);
                    txt_Subject_Unit.Tag = orgCode;
                    string sourCode = ToolHelper.GetValue(row["si_source_id"]);
                    if (sourCode.StartsWith("ZX"))
                        UnitCode = $"{orgCode}{sourCode}";
                    else
                        UnitCode = $"{orgCode}";
                }
                tab_Subject_Info.SelectedTabPageIndex = 0;
                LoadFileList(dgv_Subject_FileList, node.Name, -1);
            }
        }

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
            for (int i = 0; i < pages.Count; i++)
                if (pages[i].Name.Equals(name))
                {
                    tab_MenuList.TabPages.Add(pages[i]);
                    break;
                }
        }

        private void LoadFileList(DataGridView dataGridView, object parentId, int selectedRowIndex)
        {
            string querySql = "SELECT pfl_id, ROW_NUMBER() OVER (ORDER BY pfl_sort) rownum, pfl_stage, pfl_categor, pfl_code, pfl_name, pfl_amount, pfl_user, pfl_type, " +
               $"pfl_pages, pfl_count, TRY_CAST(TRY_PARSE(pfl_date as date) AS VARCHAR) pfl_date, pfl_unit, pfl_carrier, pfl_link FROM processing_file_list WHERE pfl_obj_id='{parentId}'";
            DataTable dataTable = SqlHelper.ExecuteQuery(querySql);
            dataGridView.DataSource = dataTable;

            dataGridView.ColumnHeadersDefaultCellStyle = DataGridViewStyleHelper.GetHeaderStyle();
            dataGridView.DefaultCellStyle = DataGridViewStyleHelper.GetCellStyle();
            if (selectedRowIndex != -1)
            {
                dataGridView.ClearSelection();
                dataGridView.Rows[selectedRowIndex].Selected = true;
                dataGridView.FirstDisplayedScrollingRowIndex = selectedRowIndex;
            }
        }

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
                        dataGridView.Rows[indexRow].Cells[key + "name"].Style.ForeColor = System.Drawing.Color.Red;
                    }
                }
            }

            dataGridView.ColumnHeadersDefaultCellStyle = DataGridViewStyleHelper.GetHeaderStyle();
        }

        private void LoadDocList(object objid, ControlType type)
        {
            LoadBoxList(objid, type);
        }

        private void LoadBoxList(object objId, ControlType type)
        {
            DataTable table = SqlHelper.ExecuteQuery($"SELECT pb_id, pb_box_number FROM processing_box WHERE pb_obj_id='{objId}' ORDER BY pb_box_number ASC");
            if (type == ControlType.Project)
            {
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
        }

        private void Cbo_Box_SelectionChangeCommitted(object sender, EventArgs e)
        {
            errorProvider.Clear();
            System.Windows.Forms.ComboBox comboBox = sender as System.Windows.Forms.ComboBox;
            if (comboBox.Name.Contains("Project"))
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
        }

        private void LoadFileBoxTable(object pbId, object objId, ControlType type)
        {
            string GCID = ToolHelper.GetValue(SqlHelper.ExecuteOnlyOneQuery($"SELECT pb_gc_id FROM processing_box WHERE pb_id='{pbId}'"));
            if (type == ControlType.Project)
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
        }

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
                $"WHERE pfl_obj_id = '{objId}' AND (pfl_box_id IS NULL OR pfl_box_id='') AND pfl_amount > 0 ORDER BY pfl_code, pfl_date";
            DataTable dataTable = SqlHelper.ExecuteQuery(querySql);
            foreach (DataRow row in dataTable.Rows)
            {
                ListViewItem item = leftView.Items.Add(ToolHelper.GetValue(row["pfl_id"]));
                item.SubItems.AddRange(new ListViewItem.ListViewSubItem[]
                {
                    new ListViewItem.ListViewSubItem(){ Text = ToolHelper.GetValue(row["pfl_code"]) },
                    new ListViewItem.ListViewSubItem(){ Text = ToolHelper.GetValue(row["pfl_name"]) },
                    new ListViewItem.ListViewSubItem(){ Text = ToolHelper.GetDateValue(row["pfl_date"], "yyyy-MM-dd") },
                });
            }
            //已归档[已存在盒]
            if (!string.IsNullOrEmpty(ToolHelper.GetValue(pbId)))
            {
                querySql = $"SELECT pfl_id, pfl_code, pfl_name, pfl_date FROM processing_file_list " +
                    $"WHERE pfl_box_id ='{pbId}' ORDER BY pfl_box_sort, pfl_code";
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
                        new ListViewItem.ListViewSubItem(){ Text = ToolHelper.GetDateValue(row["pfl_date"], "yyyy-MM-dd") },
                    });
                }
            }
        }

        public void SetCategorByStage(object stageId, DataGridView view, int rowIndex, object key)
        {
            //文件类别
            DataGridViewComboBoxCell categorCell = view.Rows[rowIndex].Cells[key + "categor"] as DataGridViewComboBoxCell;
            view.Rows[rowIndex].Cells[key + "categorname"].Tag = stageId;
            DataTable table = CategorHelper.GetInstance().GetCategorTableByStage(stageId);
            if (table != null && table.Rows.Count > 0)
            {
                categorCell.DisplayMember = "dd_name";
                categorCell.ValueMember = "dd_id";
                categorCell.DataSource = table;
            }
        }

        private void DataError(object sender, DataGridViewDataErrorEventArgs e) { }

        private void FileList_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridView view = sender as DataGridView;
            if (view.Name.Contains("Project"))
                Btn_AddFile_Click(btn_Project_AddFile, e);
            else if (view.Name.Contains("Topic"))
                Btn_AddFile_Click(btn_Topic_AddFile, e);
            else if (view.Name.Contains("Subject"))
                Btn_AddFile_Click(btn_Subject_AddFile, e);
        }

        private void Btn_AddFile_Click(object sender, EventArgs e)
        {
            Frm_AddFile frm = null;
            object name = (sender as KyoButton).Name;
            string key = string.Empty;
            if ("btn_Project_AddFile".Equals(name))
            {
                key = "project_fl_";
                object objId = tab_Project_Info.Tag;
                if (objId != null)
                {
                    if (dgv_Project_FileList.SelectedRows.Count == 1 && dgv_Project_FileList.RowCount != 1)
                        frm = new Frm_AddFile(dgv_Project_FileList, key, dgv_Project_FileList.SelectedRows[0].Cells[key + "id"].Value, null, null);
                    else
                        frm = new Frm_AddFile(dgv_Project_FileList, key, null, null, null);
                    frm.UpdateDataSource = LoadFileList;
                    frm.txt_Unit.Text = UserHelper.GetUser().UnitName;
                    frm.parentId = objId;
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
                        frm = new Frm_AddFile(dgv_Topic_FileList, key, dgv_Topic_FileList.SelectedRows[0].Cells[key + "id"].Value, null, null);
                    else
                        frm = new Frm_AddFile(dgv_Topic_FileList, key, null, null, null);
                    frm.UpdateDataSource = LoadFileList;
                    frm.txt_Unit.Text = UserHelper.GetUser().UnitName;
                    frm.parentId = objId;
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
                        frm = new Frm_AddFile(dgv_Subject_FileList, key, dgv_Subject_FileList.SelectedRows[0].Cells[key + "id"].Value, null, null);
                    else
                        frm = new Frm_AddFile(dgv_Subject_FileList, key, null, null, null);
                    frm.UpdateDataSource = LoadFileList;
                    frm.txt_Unit.Text = UserHelper.GetUser().UnitName;
                    frm.parentId = objId;
                    frm.Show();
                }
                else
                    XtraMessageBox.Show("请先保存基本信息。", "温馨提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }

        private bool CheckMustEnter(string name, object pid)
        {
            bool result = true;
            errorProvider.Clear();
            if (name.Contains("Project"))
            {
                string proCode = txt_Project_Code.Text;
                if (string.IsNullOrEmpty(proCode.Trim()))
                {
                    errorProvider.SetError(txt_Project_Code, "提示：项目编号不能为空");
                    result = false;
                }
                else if (tab_Project_Info.Tag == null)
                {
                    int count = SqlHelper.ExecuteCountQuery("SELECT COUNT(pi_id) FROM project_info A WHERE EXISTS(" +
                        "SELECT pi_id FROM project_info B WHERE B.pi_categor = 1 AND B.pi_source_id = A.pi_source_id AND B.pi_orga_id = A.pi_orga_id AND A.pi_obj_id = B.pi_id) " +
                       $"AND A.pi_categor=2 AND A.pi_code = '{proCode}'");
                    if (count > 0)
                    {
                        errorProvider.SetError(txt_Project_Code, "提示：此项目/课题编号已存在");
                        result = false;
                    }
                }

                string funds = txt_Project_Funds.Text;
                if (!string.IsNullOrEmpty(funds))
                {
                    if (!Regex.IsMatch(funds, "^[0-9]+(.[0-9]{1,2})?$"))
                    {
                        errorProvider.SetError(txt_Project_Funds, "提示：请输入合法经费");
                        result = false;
                    }
                }
                string year = txt_Project_Year.Text;
                if (string.IsNullOrEmpty(year) || !Regex.IsMatch(year, "^\\d{4}$"))
                {
                    errorProvider.SetError(txt_Project_Year, "提示：请输入有效的立项年度");
                    result = false;
                }

                string startDate = txt_Project_StartTime.Text;
                if (!string.IsNullOrEmpty(startDate))
                {

                    if (!Regex.IsMatch(startDate, "^\\d{4}-\\d{2}-\\d{2}$") || !DateTime.TryParse(startDate, out DateTime time))
                    {
                        errorProvider.SetError(txt_Project_StartTime, "提示：请输入yyyy-MM-dd格式的有效日期");
                        result = false;
                    }
                }
                string endDate = txt_Project_EndTime.Text;
                if (!string.IsNullOrEmpty(endDate))
                {
                    if (!Regex.IsMatch(endDate, "^\\d{4}-\\d{2}-\\d{2}$") || !DateTime.TryParse(endDate, out DateTime time))
                    {
                        errorProvider.SetError(txt_Project_EndTime, "提示：请输入yyyy-MM-dd格式的有效日期");
                        result = false;
                    }
                }

            }
            else if (name.Contains("Topic"))

            {
                string topCode = txt_Topic_Code.Text;
                if (string.IsNullOrEmpty(topCode.Trim()))
                {
                    errorProvider.SetError(txt_Topic_Code, "提示：课题编号不能为空");
                    result = false;
                }
                else if (tab_Topic_Info.Tag == null)
                {
                    int count = SqlHelper.ExecuteCountQuery("SELECT COUNT(ti_id) FROM topic_info A WHERE EXISTS(" +
                        "SELECT pi_id FROM project_info B WHERE B.pi_categor = 1 AND B.pi_source_id = A.ti_source_id AND B.pi_orga_id = A.ti_orga_id AND A.ti_obj_id = B.pi_id) " +
                       $"AND A.ti_code = '{topCode}'");
                    if (count > 0)
                    {
                        errorProvider.SetError(txt_Topic_Code, "提示：此项目/课题编号已存在");
                        result = false;
                    }
                }

                string funds = txt_Topic_Funds.Text;
                if (!string.IsNullOrEmpty(funds))
                {
                    if (!Regex.IsMatch(funds, "^[0-9]+(.[0-9]{1,2})?$"))
                    {
                        errorProvider.SetError(txt_Topic_Funds, "提示：请输入合法经费");
                        result = false;
                    }
                }

                string year = txt_Topic_Year.Text;
                if (string.IsNullOrEmpty(year) || !Regex.IsMatch(year, "^\\d{4}$"))
                {
                    errorProvider.SetError(txt_Topic_Year, "提示：请输入有效的立项年度");
                    result = false;
                }

                if (string.IsNullOrEmpty(txt_Topic_Unit.Text))
                {
                    errorProvider.SetError(txt_Topic_Unit, "提示：承担单位不能为空");
                    result = false;
                }
                if (string.IsNullOrEmpty(txt_Topic_ProUser.Text))
                {
                    errorProvider.SetError(txt_Topic_ProUser, "提示：负责人不能为空");
                    result = false;
                }

                string startDate = txt_Topic_StartTime.Text;
                if (!string.IsNullOrEmpty(startDate))
                {
                    if (!Regex.IsMatch(startDate, "^\\d{4}-\\d{2}-\\d{2}$") || !DateTime.TryParse(startDate, out DateTime time))
                    {
                        errorProvider.SetError(txt_Topic_StartTime, "提示：请输入yyyy-MM-dd格式的有效日期");
                        result = false;
                    }
                }
                string endDate = txt_Topic_EndTime.Text;
                if (!string.IsNullOrEmpty(endDate))
                {
                    if (!Regex.IsMatch(endDate, "^\\d{4}-\\d{2}-\\d{2}$") || !DateTime.TryParse(endDate, out DateTime time))
                    {
                        errorProvider.SetError(txt_Topic_EndTime, "提示：请输入yyyy-MM-dd格式的有效日期");
                        result = false;
                    }
                }

            }
            else if (name.Contains("Subject"))
            {
                string subCode = txt_Subject_Code.Text.Trim();
                if (string.IsNullOrEmpty(subCode.Trim()))
                {
                    errorProvider.SetError(txt_Subject_Code, "提示：课题编号不能为空");
                    result = false;
                }
                if (tab_Subject_Info.Tag == null)
                {
                    int count = SqlHelper.ExecuteCountQuery("SELECT COUNT(si_id) FROM subject_info A WHERE EXISTS(SELECT si_id FROM subject_info B WHERE A.si_obj_id = B.si_obj_id) " +
                       $"AND A.si_code = '{subCode}'");
                    if (count > 0)
                    {
                        errorProvider.SetError(txt_Subject_Code, "提示：子课题编号已存在");
                        result = false;
                    }
                }

                string funds = txt_Subject_Funds.Text;
                if (!string.IsNullOrEmpty(funds))
                {
                    if (!Regex.IsMatch(funds, "^[0-9]+(.[0-9]{1,2})?$"))
                    {
                        errorProvider.SetError(txt_Subject_Funds, "提示：请输入合法经费");
                        result = false;
                    }
                }

                string year = txt_Subject_Year.Text;
                if (string.IsNullOrEmpty(year) || !Regex.IsMatch(year, "^\\d{4}$"))
                {
                    errorProvider.SetError(txt_Subject_Year, "提示：请输入有效的立项年度");
                    result = false;
                }
                if (string.IsNullOrEmpty(txt_Subject_Unit.Text))
                {
                    errorProvider.SetError(txt_Subject_Unit, "提示：承担单位不能为空");
                    result = false;
                }
                if (string.IsNullOrEmpty(txt_Subject_ProUser.Text))
                {
                    errorProvider.SetError(txt_Subject_ProUser, "提示：负责人不能为空");
                    result = false;
                }

                string startDate = txt_Subject_StartTime.Text;
                if (!string.IsNullOrEmpty(startDate))
                {
                    if (!Regex.IsMatch(startDate, "^\\d{4}-\\d{2}-\\d{2}$") || !DateTime.TryParse(startDate, out DateTime time))
                    {
                        errorProvider.SetError(txt_Subject_StartTime, "提示：请输入yyyy-MM-dd格式的有效日期");
                        result = false;
                    }
                }
                string endDate = txt_Subject_EndTime.Text;
                if (!string.IsNullOrEmpty(endDate))
                {
                    if (!Regex.IsMatch(endDate, "^\\d{4}-\\d{2}-\\d{2}$") || !DateTime.TryParse(endDate, out DateTime time))
                    {
                        errorProvider.SetError(txt_Subject_EndTime, "提示：请输入yyyy-MM-dd格式的有效日期");
                        result = false;
                    }
                }

            }
            return result;
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

        private void Btn_Save_Click(object sender, EventArgs e)
        {
            KyoButton button = sender as KyoButton;
            DataGridView view = null;
            string key = string.Empty;
            if ("btn_Project_Save".Equals(button.Name))
            {
                if (CheckCodeHasExist(txt_Project_Code)) return;
                object objId = tab_Project_Info.Tag;
                view = dgv_Project_FileList;
                key = "project_fl_";
                int index = tab_Project_Info.SelectedTabPageIndex;
                if (index == 0 && CheckMustEnter(button.Name, objId))
                {
                    if (objId != null)
                        UpdateBasicInfo(objId, ControlType.Project);

                    if (!CheckFileList(dgv_Project_FileList.Rows, key))
                    {
                        XtraMessageBox.Show("文件信息存在错误数据，请先更正。", "保存失败", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        return;
                    }

                    int maxLength = dgv_Project_FileList.Rows.Count - 1;
                    for (int i = 0; i < maxLength; i++)
                    {
                        DataGridViewRow row = dgv_Project_FileList.Rows[i];
                        row.Cells[$"{key}id"].Value = AddFileInfo(key, row, objId, row.Index);
                    }
                    //自动更新缺失文件表
                    UpdateLostFileList(objId);
                    RemoveFileList();
                    LoadFileList(view, objId, -1);
                    XtraMessageBox.Show("信息保存成功。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
                else if (objId != null)
                {
                    if (index == 1)//文件核查
                    {
                        ModifyFileValid(dgv_Project_FileValid, objId, "project_fc_");
                        XtraMessageBox.Show("文件核查信息保存成功。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
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
                if (CheckCodeHasExist(txt_Topic_Code)) return;
                object objId = tab_Topic_Info.Tag;
                view = dgv_Topic_FileList;
                key = "topic_fl_";
                int index = tab_Topic_Info.SelectedTabPageIndex;
                if (index == 0 && CheckMustEnter(button.Name, objId))
                {
                    if (objId != null)//更新
                        UpdateBasicInfo(objId, ControlType.Topic);

                    if (!CheckFileList(dgv_Topic_FileList.Rows, key))
                    {
                        XtraMessageBox.Show("文件信息存在错误数据，请先更正。", "保存失败", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        return;
                    }

                    int maxLength = dgv_Topic_FileList.Rows.Count - 1;
                    for (int i = 0; i < maxLength; i++)
                    {
                        DataGridViewRow row = dgv_Topic_FileList.Rows[i];
                        row.Cells[$"{key}id"].Value = AddFileInfo(key, row, objId, row.Index);
                    }
                    //自动更新缺失文件表
                    UpdateLostFileList(objId);
                    RemoveFileList();
                    LoadFileList(view, objId, -1);
                    XtraMessageBox.Show("信息保存成功。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
                else if (objId != null)
                {
                    if (index == 1)//文件核查
                    {
                        ModifyFileValid(dgv_Topic_FileValid, objId, "topic_fc_");
                        XtraMessageBox.Show("文件核查信息保存成功。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
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
                if (CheckCodeHasExist(txt_Subject_Code)) return;
                object objId = tab_Subject_Info.Tag;
                view = dgv_Subject_FileList;
                key = "subject_fl_";
                int index = tab_Subject_Info.SelectedTabPageIndex;
                if (index == 0 && CheckMustEnter(button.Name, objId))
                {
                    if (objId != null)
                        UpdateBasicInfo(objId, ControlType.Subject);

                    if (!CheckFileList(dgv_Subject_FileList.Rows, key))
                    {
                        XtraMessageBox.Show("文件信息存在错误数据，请先更正。", "保存失败", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        return;
                    }

                    int maxLength = dgv_Subject_FileList.Rows.Count - 1;
                    for (int i = 0; i < maxLength; i++)
                    {
                        DataGridViewRow row = dgv_Subject_FileList.Rows[i];
                        row.Cells[$"{key}id"].Value = AddFileInfo(key, row, objId, row.Index);
                    }
                    //自动更新缺失文件表
                    UpdateLostFileList(objId);
                    RemoveFileList();
                    LoadFileList(view, objId, -1);
                    XtraMessageBox.Show("信息保存成功。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
                else if (objId != null)
                {
                    if (index == 1)//文件核查
                    {
                        ModifyFileValid(dgv_Subject_FileValid, objId, "subject_fc_");
                        XtraMessageBox.Show("文件核查信息保存成功。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
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
        }

        private bool CheckValueIsNotNull(ControlType type)
        {
            bool result = true;
            errorProvider.Clear();
            if (type == ControlType.Project)
            {
                string value1 = txt_Project_AJ_Code.Text;
                if (string.IsNullOrEmpty(value1))
                {
                    errorProvider.SetError(txt_Project_AJ_Code, "提示：档号不能为空。");
                    result = false;
                }
                string value2 = txt_Project_AJ_Name.Text;
                if (string.IsNullOrEmpty(value2))
                {
                    errorProvider.SetError(txt_Project_AJ_Name, "提示：案卷名称不能为空。");
                    result = false;
                }
                string value3 = txt_Project_GCID.Text;
                if (string.IsNullOrEmpty(value3))
                {
                    errorProvider.SetError(txt_Project_GCID, "提示：馆藏号不能为空。");
                    result = false;
                }
            }
            else if (type == ControlType.Topic)
            {
                string value1 = txt_Topic_AJ_Code.Text;
                if (string.IsNullOrEmpty(value1))
                {
                    errorProvider.SetError(txt_Topic_AJ_Code, "提示：档号不能为空。");
                    result = false;
                }
                string value2 = txt_Topic_AJ_Name.Text;
                if (string.IsNullOrEmpty(value2))
                {
                    errorProvider.SetError(txt_Topic_AJ_Name, "提示：案卷名称不能为空。");
                    result = false;
                }
                string value3 = txt_Topic_GCID.Text;
                if (string.IsNullOrEmpty(value3))
                {
                    errorProvider.SetError(txt_Topic_GCID, "提示：馆藏号不能为空。");
                    result = false;
                }
            }
            else if (type == ControlType.Subject)
            {
                string value1 = txt_Subject_AJ_Code.Text;
                if (string.IsNullOrEmpty(value1))
                {
                    errorProvider.SetError(txt_Subject_AJ_Code, "提示：档号不能为空。");
                    result = false;
                }
                string value2 = txt_Subject_AJ_Name.Text;
                if (string.IsNullOrEmpty(value2))
                {
                    errorProvider.SetError(txt_Subject_AJ_Name, "提示：案卷名称不能为空。");
                    result = false;
                }
                string value3 = txt_Subject_GCID.Text;
                if (string.IsNullOrEmpty(value3))
                {
                    errorProvider.SetError(txt_Subject_GCID, "提示：馆藏号不能为空。");
                    result = false;
                }
            }
            return result;
        }

        /// <summary>
        /// 更新指定对象下的文件缺失列表
        /// </summary>
        /// <param name="objID">项目/课题ID</param>
        private void UpdateLostFileList(object objID)
        {
            string querySql = "SELECT d1.dd_name, d1.extend_2 FROM data_dictionary d1 " +
                "INNER JOIN data_dictionary d2 ON d1.dd_pId = d2.dd_id " +
                "INNER JOIN data_dictionary d3 ON d2.dd_pId = d3.dd_id " +
                "WHERE d3.dd_code='dic_file_jd' AND d1.dd_name<>'其他' " +
                "AND d1.dd_name NOT IN ( " +
                "SELECT dd_name FROM processing_file_list AS fi " +
                "INNER JOIN data_dictionary AS dd ON fi.pfl_categor = dd.dd_id " +
               $"WHERE (fi.pfl_obj_id = '{objID}') GROUP BY dd_name ) " +
                "ORDER BY dd_name";
            DataTable table = SqlHelper.ExecuteQuery(querySql);
            StringBuilder sqlString = new StringBuilder($"DELETE FROM processing_file_lost WHERE pfo_obj_id='{objID}';");
            for (int i = 0; i < table.Rows.Count; i++)
            {
                object categor = table.Rows[i]["dd_name"];
                int ismust = ToolHelper.GetIntValue(table.Rows[i]["extend_2"], 0);
                sqlString.Append("INSERT INTO processing_file_lost (pfo_id, pfo_categor, pfo_obj_id, pfo_ismust) " +
                    $"VALUES('{Guid.NewGuid().ToString()}', '{categor}', '{objID}', '{ismust}');");
            }
            SqlHelper.ExecuteNonQuery(sqlString.ToString());
        }

        private DataTable GetUpdateTable(DataTable dataSource)
        {
            foreach (DataRow row in dataSource.Rows)
            {
                //row[""]
                if (row.RowState == DataRowState.Added)
                {
                    row["pfl_id"] = Guid.NewGuid().ToString();
                }
            }
            return dataSource;
        }

        private void UpdateBasicInfo(object objid, ControlType controlType)
        {
            if (controlType == ControlType.Project)
            {
                string code = txt_Project_Code.Text;
                string name = txt_Project_Name.Text;
                string type = string.Empty;
                string filed = txt_Project_Field.Text;
                string theme = txt_Project_Theme.Text;
                string funds = ToolHelper.GetFloatValue(txt_Project_Funds.Text, 2);
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
                    $" WHERE pi_id='{objid}'";
                SqlHelper.ExecuteNonQuery(updateSql);
            }
            else if (controlType == ControlType.Topic)
            {
                string code = txt_Topic_Code.Text;
                string name = txt_Topic_Name.Text;
                string field = txt_Topic_Field.Text;
                string theme = txt_Topic_Theme.Text;
                string funds = ToolHelper.GetFloatValue(txt_Topic_Funds.Text, 2);
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
                    $" WHERE ti_id='{objid}'";
                SqlHelper.ExecuteNonQuery(updateSql);
            }
            else if (controlType == ControlType.Subject)
            {
                string code = txt_Subject_Code.Text;
                string name = txt_Subject_Name.Text;
                string field = txt_Subject_Field.Text;
                string theme = txt_Subject_Theme.Text;
                string fund = ToolHelper.GetFloatValue(txt_Subject_Funds.Text, 2);
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
                    $" WHERE si_id='{objid}'";
                SqlHelper.ExecuteNonQuery(updateSql);
            }
        }

        private void ModifyFileValid(DataGridView dataGridView, object objid, string key)
        {
            List<object> isMustCategorList = SqlHelper.GetIsMustCategor();
            int rowCount = dataGridView.Rows.Count;
            StringBuilder sqlString = new StringBuilder($"DELETE FROM processing_file_lost WHERE pfo_obj_id='{objid}';");
            for (int i = 0; i < rowCount; i++)
            {
                DataGridViewRow row = dataGridView.Rows[i];
                object name = row.Cells[key + "name"].Value;
                if (name != null)
                {
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
                    sqlString.Append("INSERT INTO processing_file_lost(pfo_id, pfo_categor, pfo_name, pfo_reason, pfo_remark, pfo_obj_id, pfo_ismust) " +
                        $"VALUES('{rid}','{_categor}','{name}','{reason}','{remark}','{objid}', {(isMustCategorList.Contains(_categor) ? 1 : 0)});");
                    dataGridView.Rows[i].Cells[key + "id"].Tag = rid;
                }
            }
            SqlHelper.ExecuteNonQuery(sqlString.ToString());
        }

        private object AddFileInfo(string key, DataGridViewRow row, object parentId, int sort)
        {
            string nonQuerySql = string.Empty;
            string _fileId = ToolHelper.GetValue(row.Cells[key + "id"].Value);
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
            object date = row.Cells[key + "date"].Value;
            object unit = row.Cells[key + "unit"].Value;
            object carrier = row.Cells[key + "carrier"].Value;
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
                    nonQuerySql += $"DELETE FROM data_dictionary WHERE dd_name='{value}' AND dd_pId='{stage}';";
                }
                nonQuerySql += "INSERT INTO data_dictionary (dd_id, dd_name, dd_pId, dd_sort, extend_3, extend_4) " +
                    $"VALUES('{categor}', '{value}', '{stage}', '{_sort}', '{categorName}', '{1}');";
            }
            //更新
            if (!string.IsNullOrEmpty(_fileId))
            {
                nonQuerySql += $"UPDATE processing_file_list SET pfl_stage='{stage}', pfl_categor='{categor}', pfl_code='{code}', pfl_name=N'{name}', pfl_user='{user}', pfl_type='{type}', pfl_pages='{pages}'," +
                    $"pfl_count='{count}', pfl_amount='{amount}', pfl_date='{date}', pfl_unit='{unit}', pfl_carrier='{carrier}', pfl_sort='{sort}' WHERE pfl_id='{_fileId}';";
            }
            //新增
            else
            {
                _fileId = Guid.NewGuid().ToString();
                nonQuerySql += "INSERT INTO processing_file_list (pfl_id, pfl_code, pfl_stage, pfl_categor, pfl_name, pfl_user, pfl_type, pfl_pages, pfl_count, pfl_amount, pfl_date, pfl_unit, pfl_carrier, pfl_obj_id, pfl_sort, pfl_worker_id, pfl_worker_date) " +
                    $"VALUES( '{_fileId}', '{code}', '{stage}', '{categor}', N'{name}', '{user}', '{type}', '{pages}', '{count}', '{amount}', '{date}', '{unit}', '{carrier}', '{parentId}', '{sort}', '{UserHelper.GetUser().UserKey}', '{DateTime.Now}');";
            }
            SqlHelper.ExecuteNonQuery(nonQuerySql);
            return _fileId;
        }

        /// <summary>
        /// 待删除文件ID集合
        /// </summary>
        private List<object> removeIdList = new List<object>();

        /// <summary>
        /// 根据待删除文件集合中的ID删除指定文件
        /// </summary>
        private void RemoveFileList()
        {
            if (removeIdList.Count > 0)
            {
                StringBuilder stringBuilder = new StringBuilder();
                string rids = ToolHelper.GetStringBySplit(removeIdList.ToArray(), ",", "'");
                //删除当前文件
                stringBuilder.Append($"DELETE FROM processing_file_list WHERE pfl_id IN ({rids});");

                object bfis = SqlHelper.ExecuteOnlyOneQuery($"SELECT pfl_file_id FROM processing_file_list WHERE pfl_id IN ({rids})");
                string _bfid = ToolHelper.GetFullStringBySplit(ToolHelper.GetValue(bfis), ',', ",", "'");
                // 重置文件备份表中的状态为可选择（0）
                if (!string.IsNullOrEmpty(_bfid))
                    stringBuilder.Append($"UPDATE backup_files_info SET bfi_state=0 WHERE bfi_id IN ({_bfid});");

                SqlHelper.ExecuteNonQuery(stringBuilder.ToString());
                removeIdList.Clear();
            }
        }

        /// <summary>
        /// 按Del键删除行时记录文件ID
        /// </summary>
        private void FileList_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            DataGridView view = sender as DataGridView;
            removeIdList.Add(e.Row.Cells[view.Tag + "id"].Value);
        }

        private void Btn_QTReason_Click(object sender, EventArgs e)
        {
            string name = (sender as Control).Name;
            object objId = null, objName = null;
            if (name.Contains("Project"))
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
                Frm_AdviceBW frm = GetFormHelper.GetAdviceBW(objId, objName);
                frm.Show();
                frm.Activate();
            }
        }

        private void Btn_OtherDoc_Click(object sender, EventArgs e)
        {
            string name = (sender as KyoButton).Name;
            object objid = null;
            if (name.Contains("Project"))
                objid = tab_Project_Info.Tag;
            else if (name.Contains("Topic"))
                objid = tab_Topic_Info.Tag;
            else if (name.Contains("Subject"))
                objid = tab_Subject_Info.Tag;
            if (objid != null)
            {
                Frm_OtherDoc frm = GetFormHelper.GetOtherDoc(objid);
                if (UserHelper.GetUserRole() != UserRole.DocManager)
                    frm.ReadOnly = true;
                frm.Show();
                frm.Activate();
            }
            else
                XtraMessageBox.Show("请先保存基本信息。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
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

        private string GetWorker(object objId, int type)
        {
            string key = type == 1 ? "worker_id" : type == 2 ? "worker_date" : type == 3 ? "checker_id" : "checker_date";
            object result = SqlHelper.ExecuteOnlyOneQuery($"SELECT pi_{key} FROM project_info WHERE pi_id='{objId}'") ??
                SqlHelper.ExecuteOnlyOneQuery($"SELECT ti_{key} FROM topic_info WHERE ti_id='{objId}'") ??
                SqlHelper.ExecuteOnlyOneQuery($"SELECT si_{key} FROM subject_info WHERE si_id='{objId}'");
            return ToolHelper.GetValue(result);
        }

        private void Btn_Box_Click(object sender, EventArgs e)
        {
            string name = (sender as KyoButton).Name;
            if (name.Contains("btn_Project_Box"))
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
        }

        private void SetFileState(object[] fileIds, object boxId, bool isGD)
        {
            if (isGD)
            {
                string updateSQL = string.Empty;
                for (int i = 0; i < fileIds.Length; i++)
                {
                    updateSQL += $"UPDATE processing_file_list SET pfl_box_id='{boxId}', pfl_box_sort='{i}' WHERE pfl_id='{fileIds[i]}';";
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

        private void Tab_SelectedIndexChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            string name = (sender as Control).Name;
            if (name.Contains("Project"))
            {
                int index = tab_Project_Info.SelectedTabPageIndex;
                object objid = tab_Project_Info.Tag;
                if (objid != null)
                {
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
                if (objid != null)
                {
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
                if (objId != null)
                {
                    if (index == 1)
                        LoadFileValidList(dgv_Subject_FileValid, objId, "subject_fc_");
                    else if (index == 2)
                        LoadDocList(objId, ControlType.Subject);
                }
            }
        }

        private void FileList_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            DataGridView dataGridView = sender as DataGridView;
            if ("dgv_Project_FileList".Equals(dataGridView.Name))
            {
                string columnName = dgv_Project_FileList.CurrentCell.OwningColumn.Name;
                if ("project_fl_stage".Equals(columnName))
                {
                    System.Windows.Forms.ComboBox con = (System.Windows.Forms.ComboBox)e.Control;
                    con.Tag = ControlType.Project;
                    con.SelectionChangeCommitted += new EventHandler(StageComboBox_SelectionChangeCommitted);
                }
                else if ("project_fl_categor".Equals(columnName))
                {
                    System.Windows.Forms.ComboBox con = (System.Windows.Forms.ComboBox)e.Control;
                    con.Tag = ControlType.Project;
                    con.SelectionChangeCommitted += new EventHandler(CategorComboBox_SelectionChangeCommitted);
                }
            }
            else if ("dgv_Topic_FileList".Equals(dataGridView.Name))
            {
                string columnName = dgv_Topic_FileList.CurrentCell.OwningColumn.Name;
                if ("topic_fl_stage".Equals(columnName))
                {
                    System.Windows.Forms.ComboBox con = (System.Windows.Forms.ComboBox)e.Control;
                    con.Tag = ControlType.Topic;
                    con.SelectionChangeCommitted += new EventHandler(StageComboBox_SelectionChangeCommitted);
                }
                else if ("topic_fl_categor".Equals(columnName))
                {
                    System.Windows.Forms.ComboBox con = (System.Windows.Forms.ComboBox)e.Control;
                    con.Tag = ControlType.Topic;
                    con.SelectionChangeCommitted += new EventHandler(CategorComboBox_SelectionChangeCommitted);
                }
            }
            else if ("dgv_Subject_FileList".Equals(dataGridView.Name))
            {
                string columnName = dgv_Subject_FileList.CurrentCell.OwningColumn.Name;
                if ("subject_fl_stage".Equals(columnName))
                {
                    System.Windows.Forms.ComboBox con = (System.Windows.Forms.ComboBox)e.Control;
                    con.Tag = ControlType.Subject;
                    con.SelectionChangeCommitted += new EventHandler(StageComboBox_SelectionChangeCommitted);
                }
                else if ("subject_fl_categor".Equals(columnName))
                {
                    System.Windows.Forms.ComboBox con = (System.Windows.Forms.ComboBox)e.Control;
                    con.Tag = ControlType.Subject;
                    con.SelectionChangeCommitted += new EventHandler(CategorComboBox_SelectionChangeCommitted);
                }
            }
        }

        /// <summary>
        /// 文件阶段 下拉事件
        /// </summary>
        private void StageComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            System.Windows.Forms.ComboBox comboBox = sender as System.Windows.Forms.ComboBox;
            if ((ControlType)comboBox.Tag == ControlType.Project)
                SetCategorByStage(comboBox.SelectedValue, dgv_Project_FileList, dgv_Project_FileList.CurrentRow.Index, "project_fl_");
            else if ((ControlType)comboBox.Tag == ControlType.Topic)
                SetCategorByStage(comboBox.SelectedValue, dgv_Topic_FileList, dgv_Topic_FileList.CurrentRow.Index, "topic_fl_");
            else if ((ControlType)comboBox.Tag == ControlType.Subject)
                SetCategorByStage(comboBox.SelectedValue, dgv_Subject_FileList, dgv_Subject_FileList.CurrentRow.Index, "subject_fl_");
            comboBox.Leave += new EventHandler(delegate (object obj, EventArgs eve)
            {
                System.Windows.Forms.ComboBox _comboBox = obj as System.Windows.Forms.ComboBox;
                _comboBox.SelectionChangeCommitted -= new EventHandler(StageComboBox_SelectionChangeCommitted);
            });
        }

        private void CategorComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            System.Windows.Forms.ComboBox comboBox = sender as System.Windows.Forms.ComboBox;
            comboBox.MaxDropDownItems = 10;
            if ((ControlType)comboBox.Tag == ControlType.Project)
                SetNameByCategor(comboBox, dgv_Project_FileList.CurrentRow, "project_fl_", tab_Project_Info.Tag);
            else if ((ControlType)comboBox.Tag == ControlType.Topic)
                SetNameByCategor(comboBox, dgv_Topic_FileList.CurrentRow, "topic_fl_", tab_Topic_Info.Tag);
            else if ((ControlType)comboBox.Tag == ControlType.Subject)
                SetNameByCategor(comboBox, dgv_Subject_FileList.CurrentRow, "subject_fl_", tab_Subject_Info.Tag);
            comboBox.Leave += new EventHandler(delegate (object obj, EventArgs eve)
            {
                System.Windows.Forms.ComboBox _comboBox = obj as System.Windows.Forms.ComboBox;
                _comboBox.SelectionChangeCommitted -= new EventHandler(CategorComboBox_SelectionChangeCommitted);
            });
        }

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
                if (System.Text.RegularExpressions.Regex.IsMatch(tempKey, "^[A-D]"))
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

        private void FileList_Sort(object sender, EventArgs e)
        {
            FileList_DataSourceChanged(sender, e);
        }

        private void FileList_DataSourceChanged(object sender, EventArgs e)
        {
            DataGridView view = sender as DataGridView;
            view.Update();
            object key = view.Tag;
            for (int i = 0; i < view.Rows.Count; i++)
            {
                DataGridViewRow row = view.Rows[i];
                object stageId = row.Cells[key + "stage"].Value;
                if (stageId != null)
                {
                    SetCategorByStage(stageId, view, i, key);
                }
            }
        }

        private void Cbo_Project_Box_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            System.Windows.Forms.ComboBox comboBox = sender as System.Windows.Forms.ComboBox;
            if (comboBox.Name.Contains("Project"))
            {
                object pbId = comboBox.SelectedValue;
                LoadFileBoxTable(pbId, tab_Project_Info.Tag, ControlType.Project);
                object gcid = SqlHelper.ExecuteOnlyOneQuery($"SELECT pb_gc_id FROM processing_box WHERE pb_id='{pbId}'");
                txt_Project_GCID.Text = ToolHelper.GetValue(gcid, string.Empty);
            }
            else if (comboBox.Name.Contains("Topic"))
            {
                object pbId = comboBox.SelectedValue;
                LoadFileBoxTable(pbId, tab_Topic_Info.Tag, ControlType.Topic);
                object gcid = SqlHelper.ExecuteOnlyOneQuery($"SELECT pb_gc_id FROM processing_box WHERE pb_id='{pbId}'");
                txt_Topic_GCID.Text = ToolHelper.GetValue(gcid, string.Empty);
            }
            else if (comboBox.Name.Contains("Subject"))
            {
                object pbId = comboBox.SelectedValue;
                LoadFileBoxTable(pbId, tab_Subject_Info.Tag, ControlType.Subject);
                object gcid = SqlHelper.ExecuteOnlyOneQuery($"SELECT pb_gc_id FROM processing_box WHERE pb_id='{pbId}'");
                txt_Subject_GCID.Text = ToolHelper.GetValue(gcid, string.Empty);
            }
        }

        private void treeView_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            tab_MenuList.Update();
            ControlType type = (ControlType)e.Node.Tag;
            if (type == ControlType.Project)
            {
                ShowTab("Project", 0);
                LoadPageBasicInfo(e.Node, ControlType.Project);
            }
            else if (type == ControlType.Topic)
            {
                int index = e.Node.Level;
                if (index == 0)
                {
                    ShowTab("Topic", 0);
                    LoadPageBasicInfo(e.Node, ControlType.Topic);
                }
                else if (index == 1)
                {
                    ShowTab("Project", 0);
                    LoadPageBasicInfo(e.Node.Parent, ControlType.Project);

                    ShowTab("Topic", 1);
                    LoadPageBasicInfo(e.Node, ControlType.Topic);
                }

            }
            else if (type == ControlType.Subject)
            {
                int level = e.Node.Level;
                if (level == 0)
                {
                    ShowTab("Subject", 0);
                    LoadPageBasicInfo(e.Node, ControlType.Subject);
                }
                else if (level == 1)
                {
                    ShowTab("Topic", 0);
                    LoadPageBasicInfo(e.Node.Parent, ControlType.Topic);

                    ShowTab("Subject", 1);
                    LoadPageBasicInfo(e.Node, ControlType.Subject);
                }
                else if (level == 2)
                {
                    ShowTab("Project", 0);
                    LoadPageBasicInfo(e.Node.Parent.Parent, ControlType.Project);

                    ShowTab("Topic", 1);
                    LoadPageBasicInfo(e.Node.Parent, ControlType.Topic);

                    ShowTab("Subject", 2);
                    LoadPageBasicInfo(e.Node, ControlType.Subject);
                }
            }
            if (tab_MenuList.TabCount > 0)
                tab_MenuList.SelectedIndex = tab_MenuList.TabCount - 1;
        }

        /// <summary>
        /// 计划 - 增加/删除案卷盒
        /// </summary>
        private void Lbl_Box_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Label label = sender as Label;
            if (label.Name.Contains("lbl_Project_Box"))
            {
                object objId = tab_Project_Info.Tag;
                if (objId != null)
                {
                    if ("lbl_Project_Box_Add".Equals(label.Name))//新增
                    {
                        //当前已有盒号数量
                        int Amount = SqlHelper.ExecuteCountQuery($"SELECT COUNT(pb_box_number) FROM processing_box WHERE pb_obj_id='{objId}'");
                        object GcNumber = GetBoxWaterNumber(6);
                        //默认档号和案卷名称为当前项目
                        string primaryKey = Guid.NewGuid().ToString();
                        string __code = txt_Project_Code.Text;
                        string _name = txt_Project_Name.Text;
                        string insertSql = $"INSERT INTO processing_tag(pt_id, pt_code, pt_name, pt_obj_id) VALUES('{primaryKey}', '{__code}', '{_name}', '{objId}');";
                        insertSql += $"INSERT INTO processing_box(pb_id, pb_box_number, pb_gc_fix, pb_gc_id, pb_gc_number, pb_obj_id, pb_create_id, pb_create_date, pb_create_type, pb_unit_id, pt_id) " +
                            $"VALUES('{Guid.NewGuid().ToString()}', '{Amount + 1}', '{UnitCode}', '{UnitCode + "" + GcNumber}', '{GcNumber}', '{objId}', '{UserHelper.GetUser().UserKey}', '{DateTime.Now.Date}', 1, '{UnitCode}', '{primaryKey}')";
                        SqlHelper.ExecuteNonQuery(insertSql);
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
                        object GcNumber = GetBoxWaterNumber(6);
                        //默认档号和案卷名称为当前项目
                        string primaryKey = Guid.NewGuid().ToString();
                        string __code = txt_Subject_Code.Text;
                        string _name = txt_Subject_Name.Text;
                        string insertSql = $"INSERT INTO processing_tag(pt_id, pt_code, pt_name, pt_obj_id) VALUES('{primaryKey}', '{__code}', '{_name}', '{objId}');";
                        insertSql += $"INSERT INTO processing_box(pb_id, pb_box_number, pb_gc_fix, pb_gc_id, pb_gc_number, pb_obj_id, pb_create_id, pb_create_date, pb_create_type, pb_unit_id, pt_id) " +
                            $"VALUES('{Guid.NewGuid().ToString()}', '{amount + 1}', '{UnitCode}', '{UnitCode + "" + GcNumber}', '{GcNumber}', '{objId}', '{UserHelper.GetUser().UserKey}', '{DateTime.Now.Date}', 1, '{UnitCode}', '{primaryKey}')";
                        SqlHelper.ExecuteNonQuery(insertSql);
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
                        object GcNumber = GetBoxWaterNumber(6);
                        //默认档号和案卷名称为当前项目
                        string primaryKey = Guid.NewGuid().ToString();
                        string __code = txt_Topic_Code.Text;
                        string _name = txt_Topic_Name.Text;
                        string insertSql = $"INSERT INTO processing_tag(pt_id, pt_code, pt_name, pt_obj_id) VALUES('{primaryKey}', '{__code}', '{_name}', '{objId}');";
                        insertSql += $"INSERT INTO processing_box(pb_id, pb_box_number, pb_gc_fix, pb_gc_id, pb_gc_number, pb_obj_id, pb_create_id, pb_create_date, pb_create_type, pb_unit_id, pt_id) " +
                            $"VALUES('{Guid.NewGuid().ToString()}', '{amount + 1}', '{UnitCode}', '{UnitCode + "" + GcNumber}', '{GcNumber}', '{objId}', '{UserHelper.GetUser().UserKey}', '{DateTime.Now.Date}', 1, '{UnitCode}', '{primaryKey}')";
                        SqlHelper.ExecuteNonQuery(insertSql);
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
        }

        /// <summary>
        /// 获取馆藏号流水号
        /// （优先获取已删除的）
        /// </summary>
        private object GetBoxWaterNumber(int length)
        {
            int result = 1;
            string querySql = "SELECT MIN(num) FROM( SELECT ROW_NUMBER() OVER(ORDER BY pb_gc_number) num, pb_gc_number FROM( " +
                $"SELECT DISTINCT(pb_gc_number) FROM processing_box a where a.pb_unit_id = '{UnitCode}') A) A WHERE num<> pb_gc_number";
            object value = SqlHelper.ExecuteOnlyOneQuery(querySql);
            result = value == null
                ? ToolHelper.GetIntValue(SqlHelper.ExecuteOnlyOneQuery($"SELECT MAX(pb_gc_number) + 1 FROM processing_box WHERE pb_unit_id='{UnitCode}'"), 1)
                : ToolHelper.GetIntValue(value);
            return result.ToString().PadLeft(length, '0');
        }

        private void Frm_ProDetails_KeyDown(object sender, KeyEventArgs e)
        {
            if (ActiveControl != null && ActiveControl is TextBox)
            {
                if (e.Control && e.KeyCode == Keys.Q)
                {
                    Frm_SpecialSymbol specialSymbol = new Frm_SpecialSymbol(ActiveControl as TextBox);
                    specialSymbol.ShowDialog();
                }
            }
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
                    string key = ToolHelper.GetValue(view.Tag);
                    //当前行不能是修改（只能新增）
                    if (string.IsNullOrEmpty(ToolHelper.GetValue(row.Cells[$"{key}id"].Value)))
                    {
                        object pId = null;
                        if (view.Name.Contains("Project"))
                        { pId = tab_Project_Info.Tag; }
                        else if (view.Name.Contains("Topic"))
                        { pId = tab_Topic_Info.Tag; }
                        else if (view.Name.Contains("Subject"))
                        { pId = tab_Subject_Info.Tag; }
                        if (pId != null && CheckFileName(row, key))
                        {
                            row.Cells[$"{key}id"].Value = AddFileInfo(key, row, pId, row.Index);
                        }
                    }
                }
                Thread.CurrentThread.Abort();
            }).Start();
        }

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

        private void Code_Leave(object sender, EventArgs e)
        {
            CheckCodeHasExist(sender);
        }

        /// <summary>
        /// 判断编号是否重复
        /// </summary>
        private bool CheckCodeHasExist(object sender)
        {
            bool flag = false;
            TextEdit codeText = sender as TextEdit;
            string value = codeText.Text.Trim();
            if (!string.IsNullOrEmpty(value))
            {
                object objectID = GetObjectValue(codeText.Name, 1);
                object orgaCode = GetObjectValue(codeText.Name, 2);
                int count = SqlHelper.ExecuteCountQuery("SELECT COUNT(pi_id) FROM (" +
                    "SELECT pi_id, pi_code, pi_orga_id FROM project_info UNION ALL " +
                    "SELECT ti_id, ti_code, ti_orga_id FROM topic_info UNION ALL " +
                   $"SELECT si_id, si_code, si_orga_id FROM subject_info) A WHERE A.pi_code='{value}' AND A.pi_id<>'{objectID}' AND A.pi_orga_id='{orgaCode}';");
                if (count > 0)
                {
                    errorProvider.SetError(codeText, "提示：此编号已存在。");
                    codeText.Focus();
                    flag = true;
                }
                else
                    errorProvider.SetError(codeText, null);
            }
            return flag;
        }

        private object GetObjectValue(string name, int type)
        {
            if (name.Contains("Project"))
                return type == 1 ? tab_Project_Info.Tag : txt_Project_Unit.Tag;
            else if (name.Contains("Topic"))
                return type == 1 ? tab_Topic_Info.Tag : txt_Topic_Unit.Tag;
            else if (name.Contains("Subject"))
                return type == 1 ? tab_Subject_Info.Tag : txt_Subject_Unit.Tag;
            return null;
        }

        private void FileList_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridView view = sender as DataGridView;
            if (e.Button == MouseButtons.Right && e.RowIndex != -1 && e.ColumnIndex != -1)
            {
                view.ClearSelection();
                view.CurrentCell = view.Rows[e.RowIndex].Cells[e.ColumnIndex];
                contextMenuStrip1.Tag = view;
                contextMenuStrip1.Show(MousePosition);
            }
        }

        private void Refresh_Click(object sender, EventArgs e)
        {
            DataGridView view = (DataGridView)(sender as ToolStripItem).GetCurrentParent().Tag;
            object objId = view.Parent.Parent.Tag;
            string name = view.Parent.Parent.Name;
            object key = view.Tag;
            LoadFileList(view, objId, -1);

            removeIdList.Clear();
        }

        private void InsertRow_Click(object sender, EventArgs e)
        {
            DataGridView view = (DataGridView)(sender as ToolStripItem).GetCurrentParent().Tag;
            int rowIndex = view.CurrentRow.Index;
            DataTable table = (DataTable)view.DataSource;
            table.Rows.InsertAt(table.NewRow(), rowIndex);
        }

        private void DeleteRow_Click(object sender, EventArgs e)
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

    }
}
