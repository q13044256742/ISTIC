using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using 科技计划项目档案数据采集管理系统.KyoControl;

namespace 科技计划项目档案数据采集管理系统
{
    public partial class Frm_ProDetails : XtraForm
    {
        private object objectID;
        public Frm_ProDetails(object objectID)
        {
            InitializeComponent();
            this.objectID = objectID;
            InitialForm();

        }

        List<TabPage> pages = new List<TabPage>();
        private void InitialForm()
        {
            for(int i = 0; i < tab_MenuList.TabCount; i++)
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

            //文件类型
            InitialTypeList(dgv_Project_FileList, "project_fl_");
            InitialTypeList(dgv_Topic_FileList, "topic_fl_");
            InitialTypeList(dgv_Subject_FileList, "subject_fl_");

            //载体
            InitialCarrierList(dgv_Project_FileList, "project_fl_");
            InitialCarrierList(dgv_Topic_FileList, "topic_fl_");
            InitialCarrierList(dgv_Subject_FileList, "subject_fl_");

            //文件核查原因列表
            InitialLostReasonList(dgv_Project_FileValid, "project_fc_");
            InitialLostReasonList(dgv_Topic_FileValid, "topic_fc_");
            InitialLostReasonList(dgv_Subject_FileValid, "subject_fc_");

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

        private void InitialTypeList(DataGridView dataGridView, string key)
        {
            DataGridViewComboBoxColumn filetypeColumn = dataGridView.Columns[key + "type"] as DataGridViewComboBoxColumn;
            filetypeColumn.DataSource = DictionaryHelper.GetTableByCode("dic_file_type");
            filetypeColumn.DisplayMember = "dd_name";
            filetypeColumn.ValueMember = "dd_id";
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
            if(UserHelper.GetUserRole() == UserRole.DocManager)
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
            if(proRow != null)
            {
                int type = ToolHelper.GetIntValue(proRow["pi_categor"], 2);
                TreeNode treeNode = new TreeNode()
                {
                    Name = ToolHelper.GetValue(proRow["pi_id"]),
                    Text = ToolHelper.GetValue(proRow["pi_code"]),
                    Tag = type == 2 ? ControlType.Project : type == 3 || type == -3 ? ControlType.Topic : ControlType.Subject
                };
                string topQuerySQL = $"SELECT * FROM (SELECT * FROM topic_info UNION ALL SELECT * FROM subject_info) A WHERE A.ti_obj_id='{proRow["pi_id"]}'";
                DataTable topTable = SqlHelper.ExecuteQuery(topQuerySQL);
                foreach(DataRow topRow in topTable.Rows)
                {
                    int type2 = ToolHelper.GetIntValue(proRow["ti_categor"], 3);
                    TreeNode topNode = new TreeNode()
                    {
                        Name = ToolHelper.GetValue(topRow["ti_id"]),
                        Text = ToolHelper.GetValue(topRow["ti_code"]),
                        Tag = type2 == 3 ? ControlType.Topic : ControlType.Subject
                    };
                    treeNode.Nodes.Add(topNode);

                    string subQuerySQL = $"SELECT * FROM subject_info A WHERE A.si_obj_id='{topRow["ti_id"]}'";
                    DataTable subTable = SqlHelper.ExecuteQuery(subQuerySQL);
                    foreach(DataRow subRow in subTable.Rows)
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
                ControlType firstType = (ControlType)treeNode.Tag;
                if(firstType == ControlType.Project)
                {
                    ShowTab("Project", 1);
                    LoadPageBasicInfo(treeView.Nodes[0], firstType);
                }
                else if(firstType == ControlType.Topic)
                {
                    ShowTab("Topic", 1);
                    LoadPageBasicInfo(treeView.Nodes[0], firstType);
                }
                else if(firstType == ControlType.Subject)
                {
                    ShowTab("Subject", 1);
                    LoadPageBasicInfo(treeView.Nodes[0], firstType);
                }

                if(treeView.Nodes[0].Nodes.Count == 0)
                {
                    Controls.Remove(Pal_LeftBar);
                }
            }
        }

        private void LoadPageBasicInfo(TreeNode node, ControlType type)
        {
            if(type == ControlType.Project)
            {
                DataRow row = SqlHelper.ExecuteSingleRowQuery($"SELECT * FROM project_info WHERE pi_id='{node.Name}'");
                if(row != null)
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
                    txt_Project_Province.Text = ToolHelper.GetValue(row["pi_province"]);
                    txt_Project_UnitUser.Text = ToolHelper.GetValue(row["pi_uniter"]);
                    txt_Project_ProUser.Text = ToolHelper.GetValue(row["pi_prouser"]);
                    txt_Project_Intro.Text = ToolHelper.GetValue(row["pi_intro"]);
                    Topic.Tag = row["pi_obj_id"];
                }

                LoadFileList(dgv_Project_FileList, node.Name);
                LoadFileValidList(dgv_Project_FileValid, node.Name, "project_fc_");
                LoadDocList(node.Name, ControlType.Project);
            }
            else if(type == ControlType.Topic)
            {
                DataRow row = SqlHelper.ExecuteSingleRowQuery($"SELECT * FROM topic_info WHERE ti_id='{node.Name}'");
                if(row != null)
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
                    txt_Topic_Province.Text = ToolHelper.GetValue(row["ti_province"]);
                    txt_Topic_UnitUser.Text = ToolHelper.GetValue(row["ti_uniter"]);
                    txt_Topic_ProUser.Text = ToolHelper.GetValue(row["ti_prouser"]);
                    txt_Topic_Intro.Text = ToolHelper.GetValue(row["ti_intro"]);
                    Topic.Tag = row["ti_obj_id"];
                }

                LoadFileList(dgv_Topic_FileList, node.Name);
                LoadFileValidList(dgv_Topic_FileValid, node.Name, "topic_fc_");
                LoadDocList(node.Name, ControlType.Topic);
            }
            else if(type == ControlType.Subject)
            {
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
                    txt_Subject_Funds.Text = ToolHelper.GetValue(row["si_funds"]);

                    txt_Subject_StartTime.Text = ToolHelper.GetValue(row["si_start_datetime"]);
                    txt_Subject_EndTime.Text = ToolHelper.GetValue(row["si_end_datetime"]);

                    txt_Subject_Year.Text = ToolHelper.GetValue(row["si_year"]);
                    txt_Subject_Unit.Text = ToolHelper.GetValue(row["si_unit"]);
                    txt_Subject_Province.Text = ToolHelper.GetValue(row["si_province"]);
                    txt_Subject_Unituser.Text = ToolHelper.GetValue(row["si_uniter"]);
                    txt_Subject_ProUser.Text = ToolHelper.GetValue(row["si_prouser"]);
                    txt_Subject_Intro.Text = ToolHelper.GetValue(row["si_intro"]);
                    Subject.Tag = row["si_obj_id"];
                }
                LoadFileList(dgv_Subject_FileList, node.Name);
                LoadFileValidList(dgv_Subject_FileValid, node.Name, "subject_fc_");
                LoadDocList(node.Name, ControlType.Subject);
            }
        }

        private void ShowTab(string name, int index)
        {
            if(index == 0)
                tab_MenuList.TabPages.Clear();
            else
            {
                int amount = tab_MenuList.TabPages.Count;
                List<TabPage> removeList = new List<TabPage>();
                for(int i = 0; i < amount; i++)
                    if(i >= index)
                        removeList.Add(tab_MenuList.TabPages[i]);
                if(removeList.Count > 0)
                    for(int i = 0; i < removeList.Count; i++)
                        tab_MenuList.TabPages.Remove(removeList[i]);
            }
            //根据指定name添加选项卡
            for(int i = 0; i < pages.Count; i++)
                if(pages[i].Name.Equals(name))
                {
                    tab_MenuList.TabPages.Add(pages[i]);
                    break;
                }
        }

        private void LoadFileList(DataGridView dataGridView, object parentId)
        {
            string querySql = "SELECT pfl_id, ROW_NUMBER() OVER (ORDER BY pfl_sort) rownum, pfl_stage, pfl_categor, pfl_code, pfl_name, pfl_amount, pfl_user, pfl_type, " +
               $"pfl_pages, pfl_count, TRY_CAST(TRY_PARSE(pfl_date as date) AS VARCHAR) pfl_date, pfl_unit, pfl_carrier, pfl_link FROM processing_file_list WHERE pfl_obj_id='{parentId}'";
            DataTable dataTable = SqlHelper.ExecuteQuery(querySql);
            dataGridView.DataSource = dataTable;

            dataGridView.ColumnHeadersDefaultCellStyle = DataGridViewStyleHelper.GetHeaderStyle();
            dataGridView.DefaultCellStyle = DataGridViewStyleHelper.GetCellStyle();
        }

        private void LoadFileValidList(DataGridView dataGridView, object objid, string key)
        {
            dataGridView.Rows.Clear();

            string querySql = "SELECT dd_name [name], dd_name+' '+extend_3 dd_name, dd_note, extend_2 FROM data_dictionary dd WHERE dd_pId in(" +
               "SELECT dd_id FROM data_dictionary WHERE dd_pId = (SELECT dd_id FROM data_dictionary  WHERE dd_code = 'dic_file_jd')) " +
               $"AND dd.dd_name NOT IN(SELECT dd.dd_name FROM processing_file_list fi LEFT JOIN data_dictionary dd ON fi.pfl_categor = dd.dd_id where fi.pfl_obj_id='{objid}')" +
               $" ORDER BY dd_name";
            DataTable table = SqlHelper.ExecuteQuery(querySql);
            for(int i = 0; i < table.Rows.Count; i++)
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
                            dataGridView.Rows[indexRow].Cells[key + "name"].Style.ForeColor = System.Drawing.Color.Red;
                        }
                    }
                }
            }

            dataGridView.ColumnHeadersDefaultCellStyle = DataGridViewStyleHelper.GetHeaderStyle();
        }

        private void LoadDocList(object objid, ControlType type)
        {
            object[] values = SqlHelper.ExecuteRowsQuery($"SELECT pt_code, pt_name FROM processing_tag WHERE pt_obj_id='{objid}'");
            bool flag = values == null;
            if(type == ControlType.Project)
            {
                txt_Project_AJ_Code.Text = flag ? txt_Project_Code.Text : ToolHelper.GetValue(values[0]);
                txt_Project_AJ_Name.Text = flag ? txt_Project_Name.Text : ToolHelper.GetValue(values[1]);
            }
            else if(type == ControlType.Topic)
            {
                txt_Topic_AJ_Code.Text = flag ? txt_Topic_Code.Text : ToolHelper.GetValue(values[0]);
                txt_Topic_AJ_Name.Text = flag ? txt_Topic_Name.Text : ToolHelper.GetValue(values[1]);
            }
            else if(type == ControlType.Subject)
            {
                txt_Subject_AJ_Code.Text = flag ? txt_Subject_Code.Text : ToolHelper.GetValue(values[0]);
                txt_Subject_AJ_Name.Text = flag ? txt_Subject_Name.Text : ToolHelper.GetValue(values[1]);
            }
            LoadBoxList(objid, type);
        }

        private void LoadBoxList(object objId, ControlType type)
        {
            DataTable table = SqlHelper.ExecuteQuery($"SELECT * FROM processing_box WHERE pb_obj_id='{objId}' ORDER BY pb_box_number ASC");
            if(type == ControlType.Project)
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
        }

        private void Cbo_Box_SelectionChangeCommitted(object sender, EventArgs e)
        {
            System.Windows.Forms.ComboBox comboBox = sender as System.Windows.Forms.ComboBox;
            if(comboBox.Name.Contains("Project"))
            {
                object pbId = comboBox.SelectedValue;
                LoadFileBoxTable(pbId, tab_Project_Info.Tag, ControlType.Project);
                object gcid = SqlHelper.ExecuteOnlyOneQuery($"SELECT pb_gc_id FROM processing_box WHERE pb_id='{pbId}'");
                if(gcid != null)
                    txt_Project_GCID.Text = ToolHelper.GetValue(gcid);
                else
                    txt_Project_GCID.Text = null;
            }
            else if(comboBox.Name.Contains("Topic"))
            {
                object pbId = comboBox.SelectedValue;
                LoadFileBoxTable(pbId, tab_Topic_Info.Tag, ControlType.Topic);
                object gcid = SqlHelper.ExecuteOnlyOneQuery($"SELECT pb_gc_id FROM processing_box WHERE pb_id='{pbId}'");
                if(gcid != null)
                    txt_Topic_GCID.Text = ToolHelper.GetValue(gcid);
                else
                    txt_Topic_GCID.Text = null;
            }
            else if(comboBox.Name.Contains("Subject"))
            {
                object pbId = comboBox.SelectedValue;
                LoadFileBoxTable(pbId, tab_Subject_Info.Tag, ControlType.Subject);
                object gcid = SqlHelper.ExecuteOnlyOneQuery($"SELECT pb_gc_id FROM processing_box WHERE pb_id='{pbId}'");
                if(gcid != null)
                    txt_Subject_GCID.Text = ToolHelper.GetValue(gcid);
                else
                    txt_Subject_GCID.Text = null;
            }
        }

        private void LoadFileBoxTable(object pbId, object objId, ControlType type)
        {
            string GCID = ToolHelper.GetValue(SqlHelper.ExecuteOnlyOneQuery($"SELECT pb_gc_id FROM processing_box WHERE pb_id='{pbId}'"));
            if(type == ControlType.Project)
            {
                txt_Project_GCID.Text = GCID;
                LoadFileBoxTableInstance(lsv_JH_XM_File1, lsv_JH_XM_File2, "jh_xm", pbId, objId);
            }
            else if(type == ControlType.Topic)
            {
                txt_Topic_GCID.Text = GCID;
                LoadFileBoxTableInstance(lsv_JH_KT_File1, lsv_JH_KT_File2, "jh_kt", pbId, objId);
            }
            else if(type == ControlType.Subject)
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
            foreach(DataRow row in dataTable.Rows)
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
                        new ListViewItem.ListViewSubItem(){ Text = ToolHelper.GetDateValue(row["pfl_date"], "yyyy-MM-dd") },
                    });
                }
            }
        }

        public void SetCategorByStage(object jdId, DataGridViewRow dataGridViewRow, object key)
        {
            //文件类别
            DataGridViewComboBoxCell categorCell = dataGridViewRow.Cells[key + "categor"] as DataGridViewComboBoxCell;
            dataGridViewRow.Cells[key + "categorname"].Tag = jdId;
            string querySql = $"SELECT dd_id, dd_name+' '+extend_3 as dd_name FROM data_dictionary WHERE dd_pId='{jdId}' ORDER BY dd_name";
            categorCell.DataSource = SqlHelper.ExecuteQuery(querySql);
            categorCell.DisplayMember = "dd_name";
            categorCell.ValueMember = "dd_id";
            if(categorCell.Items.Count > 0)
                categorCell.Style.NullValue = categorCell.Items[0];
        }

        private void DataError(object sender, DataGridViewDataErrorEventArgs e) { }

        private void FileList_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridView view = sender as DataGridView;
            if(view.Name.Contains("Project"))
                Btn_AddFile_Click(btn_Project_AddFile, e);
            else if(view.Name.Contains("Topic"))
                Btn_AddFile_Click(btn_Topic_AddFile, e);
            else if(view.Name.Contains("Subject"))
                Btn_AddFile_Click(btn_Subject_AddFile, e);
        }

        private void Btn_AddFile_Click(object sender, EventArgs e)
        {
            Frm_AddFile frm = null;
            object name = (sender as KyoButton).Name;
            string key = string.Empty;
            if("btn_Project_AddFile".Equals(name))
            {
                key = "project_fl_";
                object objId = tab_Project_Info.Tag;
                if(objId != null)
                {
                    if(dgv_Project_FileList.SelectedRows.Count == 1)
                        frm = new Frm_AddFile(dgv_Project_FileList, key, dgv_Project_FileList.CurrentRow.Cells[key + "num"].Value, null);
                    else
                        frm = new Frm_AddFile(dgv_Project_FileList, key, null, null);
                    frm.UpdateDataSource = LoadFileList;
                    frm.txt_Unit.Text = UserHelper.GetUser().UnitName;
                    frm.parentId = objId;
                    frm.Show();
                }
                else
                    XtraMessageBox.Show("请先保存基本信息。", "温馨提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            else if("btn_Topic_AddFile".Equals(name))
            {
                key = "topic_fl_";
                object objId = tab_Topic_Info.Tag;
                if(objId != null)
                {
                    if(dgv_Topic_FileList.SelectedRows.Count == 1)
                        frm = new Frm_AddFile(dgv_Topic_FileList, key, dgv_Topic_FileList.CurrentRow.Cells[key + "num"].Value, null);
                    else
                        frm = new Frm_AddFile(dgv_Topic_FileList, key, null, null);
                    frm.UpdateDataSource = LoadFileList;
                    frm.txt_Unit.Text = UserHelper.GetUser().UnitName;
                    frm.parentId = objId;
                    frm.Show();
                }
                else
                    XtraMessageBox.Show("请先保存基本信息。", "温馨提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            else if("btn_Subject_AddFile".Equals(name))
            {
                key = "subject_fl_";
                object objId = tab_Subject_Info.Tag;
                if(objId != null)
                {
                    if(dgv_Subject_FileList.SelectedRows.Count == 1)
                        frm = new Frm_AddFile(dgv_Subject_FileList, key, dgv_Subject_FileList.CurrentRow.Cells[key + "num"].Value, null);
                    else
                        frm = new Frm_AddFile(dgv_Subject_FileList, key, null, null);
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
            if(name.Contains("Project"))
            {
                string proCode = txt_Project_Code.Text;
                if(string.IsNullOrEmpty(proCode.Trim()))
                {
                    errorProvider.SetError(txt_Project_Code, "提示：项目编号不能为空");
                    result = false;
                }
                else if(tab_Project_Info.Tag == null)
                {
                    int count = SqlHelper.ExecuteCountQuery("SELECT COUNT(pi_id) FROM project_info A WHERE EXISTS(" +
                        "SELECT pi_id FROM project_info B WHERE B.pi_categor = 1 AND B.pi_source_id = A.pi_source_id AND B.pi_orga_id = A.pi_orga_id AND A.pi_obj_id = B.pi_id) " +
                       $"AND A.pi_categor=2 AND A.pi_code = '{proCode}'");
                    if(count > 0)
                    {
                        errorProvider.SetError(txt_Project_Code, "提示：此项目/课题编号已存在");
                        result = false;
                    }
                }

                string funds = txt_Project_Funds.Text;
                if(!string.IsNullOrEmpty(funds))
                {
                    if(!Regex.IsMatch(funds, "^[0-9]+(.[0-9]{1,2})?$"))
                    {
                        errorProvider.SetError(txt_Project_Funds, "提示：请输入合法经费");
                        result = false;
                    }
                }
                string year = txt_Project_Year.Text;
                if(string.IsNullOrEmpty(year) || !Regex.IsMatch(year, "^\\d{4}$"))
                {
                    errorProvider.SetError(txt_Project_Year, "提示：请输入有效的立项年度");
                    result = false;
                }

                string startDate = txt_Project_StartTime.Text;
                if(!string.IsNullOrEmpty(startDate))
                {

                    if(!Regex.IsMatch(startDate, "^\\d{4}-\\d{2}-\\d{2}$") || !DateTime.TryParse(startDate, out DateTime time))
                    {
                        errorProvider.SetError(dtp_Project_StartTime, "提示：请输入yyyy-MM-dd格式的有效日期");
                        result = false;
                    }
                }
                string endDate = txt_Project_EndTime.Text;
                if(!string.IsNullOrEmpty(endDate))
                {
                    if(!Regex.IsMatch(endDate, "^\\d{4}-\\d{2}-\\d{2}$") || !DateTime.TryParse(endDate, out DateTime time))
                    {
                        errorProvider.SetError(dtp_Project_EndTime, "提示：请输入yyyy-MM-dd格式的有效日期");
                        result = false;
                    }
                }

            }
            else if(name.Contains("Topic"))

            {
                string topCode = txt_Topic_Code.Text;
                if(string.IsNullOrEmpty(topCode.Trim()))
                {
                    errorProvider.SetError(txt_Topic_Code, "提示：课题编号不能为空");
                    result = false;
                }
                else if(tab_Topic_Info.Tag == null)
                {
                    int count = SqlHelper.ExecuteCountQuery("SELECT COUNT(ti_id) FROM topic_info A WHERE EXISTS(" +
                        "SELECT pi_id FROM project_info B WHERE B.pi_categor = 1 AND B.pi_source_id = A.ti_source_id AND B.pi_orga_id = A.ti_orga_id AND A.ti_obj_id = B.pi_id) " +
                       $"AND A.ti_code = '{topCode}'");
                    if(count > 0)
                    {
                        errorProvider.SetError(txt_Topic_Code, "提示：此项目/课题编号已存在");
                        result = false;
                    }
                }

                string funds = txt_Topic_Funds.Text;
                if(!string.IsNullOrEmpty(funds))
                {
                    if(!Regex.IsMatch(funds, "^[0-9]+(.[0-9]{1,2})?$"))
                    {
                        errorProvider.SetError(txt_Topic_Funds, "提示：请输入合法经费");
                        result = false;
                    }
                }

                string year = txt_Topic_Year.Text;
                if(string.IsNullOrEmpty(year) || !Regex.IsMatch(year, "^\\d{4}$"))
                {
                    errorProvider.SetError(txt_Topic_Year, "提示：请输入有效的立项年度");
                    result = false;
                }

                if(string.IsNullOrEmpty(txt_Topic_Unit.Text))
                {
                    errorProvider.SetError(txt_Topic_Unit, "提示：承担单位不能为空");
                    result = false;
                }
                if(string.IsNullOrEmpty(txt_Topic_ProUser.Text))
                {
                    errorProvider.SetError(txt_Topic_ProUser, "提示：负责人不能为空");
                    result = false;
                }

                string startDate = txt_Topic_StartTime.Text;
                if(!string.IsNullOrEmpty(startDate))
                {
                    if(!Regex.IsMatch(startDate, "^\\d{4}-\\d{2}-\\d{2}$") || !DateTime.TryParse(startDate, out DateTime time))
                    {
                        errorProvider.SetError(dtp_Topic_StartTime, "提示：请输入yyyy-MM-dd格式的有效日期");
                        result = false;
                    }
                }
                string endDate = txt_Topic_EndTime.Text;
                if(!string.IsNullOrEmpty(endDate))
                {
                    if(!Regex.IsMatch(endDate, "^\\d{4}-\\d{2}-\\d{2}$") || !DateTime.TryParse(endDate, out DateTime time))
                    {
                        errorProvider.SetError(dtp_Topic_EndTime, "提示：请输入yyyy-MM-dd格式的有效日期");
                        result = false;
                    }
                }

            }
            else if(name.Contains("Subject"))
            {
                string subCode = txt_Subject_Code.Text.Trim();
                if(string.IsNullOrEmpty(subCode.Trim()))
                {
                    errorProvider.SetError(txt_Subject_Code, "提示：课题编号不能为空");
                    result = false;
                }
                if(tab_Subject_Info.Tag == null)
                {
                    int count = SqlHelper.ExecuteCountQuery("SELECT COUNT(si_id) FROM subject_info A WHERE EXISTS(SELECT si_id FROM subject_info B WHERE A.si_obj_id = B.si_obj_id) " +
                       $"AND A.si_code = '{subCode}'");
                    if(count > 0)
                    {
                        errorProvider.SetError(txt_Subject_Code, "提示：子课题编号已存在");
                        result = false;
                    }
                }

                string funds = txt_Subject_Funds.Text;
                if(!string.IsNullOrEmpty(funds))
                {
                    if(!Regex.IsMatch(funds, "^[0-9]+(.[0-9]{1,2})?$"))
                    {
                        errorProvider.SetError(txt_Subject_Funds, "提示：请输入合法经费");
                        result = false;
                    }
                }

                string year = txt_Subject_Year.Text;
                if(string.IsNullOrEmpty(year) || !Regex.IsMatch(year, "^\\d{4}$"))
                {
                    errorProvider.SetError(txt_Subject_Year, "提示：请输入有效的立项年度");
                    result = false;
                }
                if(string.IsNullOrEmpty(txt_Subject_Unit.Text))
                {
                    errorProvider.SetError(txt_Subject_Unit, "提示：承担单位不能为空");
                    result = false;
                }
                if(string.IsNullOrEmpty(txt_Subject_ProUser.Text))
                {
                    errorProvider.SetError(txt_Subject_ProUser, "提示：负责人不能为空");
                    result = false;
                }

                string startDate = txt_Subject_StartTime.Text;
                if(!string.IsNullOrEmpty(startDate))
                {
                    if(!Regex.IsMatch(startDate, "^\\d{4}-\\d{2}-\\d{2}$") || !DateTime.TryParse(startDate, out DateTime time))
                    {
                        errorProvider.SetError(dtp_Subject_StartTime, "提示：请输入yyyy-MM-dd格式的有效日期");
                        result = false;
                    }
                }
                string endDate = txt_Subject_EndTime.Text;
                if(!string.IsNullOrEmpty(endDate))
                {
                    if(!Regex.IsMatch(endDate, "^\\d{4}-\\d{2}-\\d{2}$") || !DateTime.TryParse(endDate, out DateTime time))
                    {
                        errorProvider.SetError(dtp_Subject_EndTime, "提示：请输入yyyy-MM-dd格式的有效日期");
                        result = false;
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
            if("btn_Project_Save".Equals(button.Name))
            {
                object objId = tab_Project_Info.Tag;
                view = dgv_Project_FileList;
                key = "project_fl_";
                int index = tab_Project_Info.SelectedTabPageIndex;
                if(index == 0 && CheckMustEnter(button.Name, objId))
                {
                    if(objId != null)
                        UpdateBasicInfo(objId, ControlType.Project);

                    int maxLength = dgv_Project_FileList.Rows.Count - 1;
                    for(int i = 0; i < maxLength; i++)
                    {
                        DataGridViewRow row = dgv_Project_FileList.Rows[i];
                        row.Cells[$"{key}num"].Value = AddFileInfo(key, row, objId, row.Index);
                    }
                    RemoveFileList(objId);
                    LoadFileList(view, objId);
                    XtraMessageBox.Show("信息保存成功。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
                else if(objId != null)
                {
                    if(index == 1)//文件核查
                    {
                        ModifyFileValid(dgv_Project_FileValid, objId, "project_fc_");
                        XtraMessageBox.Show("文件核查信息保存成功。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                    else if(index == 2)
                    {
                        string docId = txt_Project_AJ_Code.Text;
                        string docName = txt_Project_AJ_Name.Text;
                        string insertSQL =
                            $"DELETE FROM processing_tag WHERE pt_obj_id='{objId}';" +
                            $"INSERT INTO processing_tag(pt_id, pt_code, pt_name, pt_obj_id) VALUES('{Guid.NewGuid().ToString()}','{docId}','{docName}','{objId}');";
                        insertSQL += $"UPDATE processing_box SET pb_gc_id='{txt_Project_GCID.Text}' WHERE pb_id='{cbo_Project_Box.SelectedValue}';";
                        SqlHelper.ExecuteNonQuery(insertSQL);
                    }
                }
            }
            else if("btn_Topic_Save".Equals(button.Name))
            {
                object objId = tab_Topic_Info.Tag;
                view = dgv_Topic_FileList;
                key = "topic_fl_";
                int index = tab_Topic_Info.SelectedTabPageIndex;
                if(index == 0 && CheckMustEnter(button.Name, objId))
                {
                    if(objId != null)//更新
                        UpdateBasicInfo(objId, ControlType.Topic);
                    int maxLength = dgv_Topic_FileList.Rows.Count - 1;
                    for(int i = 0; i < maxLength; i++)
                    {
                        DataGridViewRow row = dgv_Topic_FileList.Rows[i];
                        row.Cells[$"{key}num"].Value = AddFileInfo(key, row, objId, row.Index);
                    }
                    RemoveFileList(objId);
                    LoadFileList(view, objId);
                    XtraMessageBox.Show("信息保存成功。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
                else if(objId != null)
                {
                    if(index == 1)//文件核查
                    {
                        ModifyFileValid(dgv_Topic_FileValid, objId, "topic_fc_");
                        XtraMessageBox.Show("文件核查信息保存成功。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                    else if(index == 2)
                    {
                        string docId = txt_Topic_AJ_Code.Text;
                        string docName = txt_Topic_AJ_Name.Text;
                        string insertSQL =
                           $"DELETE FROM processing_tag WHERE pt_obj_id='{objId}';" +
                           $"INSERT INTO processing_tag(pt_id, pt_code, pt_name, pt_obj_id) VALUES('{Guid.NewGuid().ToString()}','{docId}','{docName}','{objId}');";
                        insertSQL += $"UPDATE processing_box SET pb_gc_id='{txt_Topic_GCID.Text}' WHERE pb_id='{cbo_Topic_Box.SelectedValue}';";
                        SqlHelper.ExecuteNonQuery(insertSQL);
                    }
                }
            }
            else if("btn_Subject_Save".Equals(button.Name))
            {
                object objId = tab_Subject_Info.Tag;
                view = dgv_Subject_FileList;
                key = "subject_fl_";
                int index = tab_Subject_Info.SelectedTabPageIndex;
                if(index == 0 && CheckMustEnter(button.Name, objId))
                {
                    if(objId != null)
                        UpdateBasicInfo(objId, ControlType.Subject);
                    int maxLength = dgv_Subject_FileList.Rows.Count - 1;
                    for(int i = 0; i < maxLength; i++)
                    {
                        DataGridViewRow row = dgv_Subject_FileList.Rows[i];
                        row.Cells[$"{key}num"].Value = AddFileInfo(key, row, objId, row.Index);
                    }
                    RemoveFileList(objId);
                    LoadFileList(view, objId);
                    XtraMessageBox.Show("信息保存成功。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
                else if(objId != null)
                {
                    if(index == 1)//文件核查
                    {
                        ModifyFileValid(dgv_Subject_FileValid, objId, "subject_fc_");
                        XtraMessageBox.Show("文件核查信息保存成功。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                    else if(index == 2)
                    {
                        string docId = txt_Subject_AJ_Code.Text;
                        string docName = txt_Subject_AJ_Name.Text;
                        string insertSQL =
                            $"DELETE FROM processing_tag WHERE pt_obj_id='{objId}';" +
                            $"INSERT INTO processing_tag(pt_id, pt_code, pt_name, pt_obj_id) VALUES('{Guid.NewGuid().ToString()}','{docId}','{docName}','{objId}');";
                        insertSQL += $"UPDATE processing_box SET pb_gc_id='{txt_Subject_GCID.Text}' WHERE pb_id='{cbo_Subject_Box.SelectedValue}';";
                        SqlHelper.ExecuteNonQuery(insertSQL);
                    }
                }
            }
        }

        private void UpdateBasicInfo(object objid, ControlType controlType)
        {
            if(controlType == ControlType.Project)
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
                    $",pi_source_id = '{Tag}'" +
                    $" WHERE pi_id='{objid}'";
                SqlHelper.ExecuteNonQuery(updateSql);
            }
            else if(controlType == ControlType.Topic)
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
                    $",ti_source_id = '{Tag}'" +
                    $" WHERE ti_id='{objid}'";
                SqlHelper.ExecuteNonQuery(updateSql);
            }
            else if(controlType == ControlType.Subject)
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
                    $",si_source_id = '{Tag}'" +
                    $" WHERE si_id='{objid}'";
                SqlHelper.ExecuteNonQuery(updateSql);
            }
        }

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

        private object AddFileInfo(string key, DataGridViewRow row, object parentId, int sort)
        {
            string nonQuerySql = string.Empty;
            string _fileId = ToolHelper.GetValue(row.Cells[key + "num"].Value);
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
            if(isOtherType)
            {
                categor = Guid.NewGuid().ToString();
                string value = ToolHelper.GetValue(code).Split('-')[0];
                int _sort = ((DataGridViewComboBoxCell)row.Cells[key + "categor"]).Items.Count - 1;

                object dicId = SqlHelper.ExecuteOnlyOneQuery($"SELECT dd_id FROM data_dictionary WHERE dd_name='{value}' AND dd_pId='{stage}'");
                if(dicId != null)
                {
                    categor = dicId;
                    nonQuerySql += $"DELETE FROM data_dictionary WHERE dd_name='{value}' AND dd_pId='{stage}';";
                }
                nonQuerySql += "INSERT INTO data_dictionary (dd_id, dd_name, dd_pId, dd_sort, extend_3, extend_4) " +
                    $"VALUES('{categor}', '{value}', '{stage}', '{_sort}', '{categorName}', '{1}');";
            }
            //更新
            if(!string.IsNullOrEmpty(_fileId))
            {
                nonQuerySql += $"UPDATE processing_file_list SET pfl_stage='{stage}', pfl_categor='{categor}', pfl_code='{code}', pfl_name='{name}', pfl_user='{user}', pfl_type='{type}', pfl_pages='{pages}'," +
                    $"pfl_count='{count}', pfl_amount='{amount}', pfl_date='{date}', pfl_unit='{unit}', pfl_carrier='{carrier}' WHERE pfl_id='{_fileId}';";
            }
            //新增
            else
            {
                _fileId = Guid.NewGuid().ToString();
                nonQuerySql += "INSERT INTO processing_file_list (pfl_id, pfl_code, pfl_stage, pfl_categor, pfl_name, pfl_user, pfl_type, pfl_pages, pfl_count, pfl_amount, pfl_date, pfl_unit, pfl_carrier, pfl_obj_id, pfl_sort, pfl_worker_id, pfl_worker_date) " +
                    $"VALUES( '{_fileId}', '{code}', '{stage}', '{categor}', '{name}', '{user}', '{type}', '{pages}', '{count}', '{amount}', '{date}', '{unit}', '{carrier}', '{parentId}', '{sort}', '{UserHelper.GetUser().UserKey}', '{DateTime.Now}');";
            }
            SqlHelper.ExecuteNonQuery(nonQuerySql);
            return _fileId;
        }

        List<object> removeIdList = new List<object>();
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

        private void FileList_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e) => removeIdList.Add(e.Row.Cells[0].Tag);

        private void Btn_QTReason_Click(object sender, EventArgs e)
        {
            string name = (sender as Control).Name;
            object objId = null, objName = null;
            if(name.Contains("Project"))
            {
                objId = tab_Project_Info.Tag;
                objName = txt_Project_Name.Text;
            }
            else if(name.Contains("Topic"))
            {
                objId = tab_Topic_Info.Tag;
                objName = txt_Topic_Name.Text;
            }
            else if(name.Contains("Subject"))
            {
                objId = tab_Subject_Info.Tag;
                objName = txt_Subject_Name.Text;
            }
            if(objId != null && objName != null)
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
            if(name.Contains("Project"))
                objid = tab_Project_Info.Tag;
            else if(name.Contains("Topic"))
                objid = tab_Topic_Info.Tag;
            else if(name.Contains("Subject"))
                objid = tab_Subject_Info.Tag;
            if(objid != null)
            {
                Frm_OtherDoc frm = GetFormHelper.GetOtherDoc(objid);
                if(UserHelper.GetUserRole() != UserRole.DocManager)
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
            object objId = null, boxId = null, docNumber = null;
            string objName = null;
            DataTable boxTable = null;
            string proName = null, proCode = null;
            object parentObjectName = null;
            if(controlName.Contains("Project"))
            {
                objId = tab_Project_Info.Tag;
                boxId = cbo_Project_Box.SelectedValue;
                docNumber = txt_Project_AJ_Code.Text;
                objName = txt_Project_AJ_Name.Text;
                proName = txt_Project_Name.Text;
                proCode = txt_Project_Code.Text;
                boxTable = (DataTable)cbo_Project_Box.DataSource;
            }
            else if(controlName.Contains("Topic"))
            {
                objId = tab_Topic_Info.Tag;
                boxId = cbo_Topic_Box.SelectedValue;
                docNumber = txt_Topic_AJ_Code.Text;
                objName = txt_Topic_AJ_Name.Text;
                proName = txt_Topic_Name.Text;
                proCode = txt_Topic_Code.Text;
                boxTable = (DataTable)cbo_Topic_Box.DataSource;
                parentObjectName = SqlHelper.ExecuteOnlyOneQuery($"SELECT pi_name FROM project_info WHERE pi_id=(SELECT ti_obj_id FROM topic_info WHERE ti_id='{objId}')");
            }
            else if(controlName.Contains("Subject"))
            {
                objId = tab_Subject_Info.Tag;
                boxId = cbo_Subject_Box.SelectedValue;
                docNumber = txt_Subject_AJ_Code.Text;
                objName = txt_Subject_AJ_Name.Text;
                proName = txt_Subject_Name.Text;
                proCode = txt_Subject_Code.Text;
                boxTable = (DataTable)cbo_Subject_Box.DataSource;
                parentObjectName = SqlHelper.ExecuteOnlyOneQuery($"SELECT ti_name FROM topic_info WHERE ti_id=(SELECT si_obj_id FROM subject_info WHERE si_id='{objId}')");
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
            if(name.Contains("btn_Project_Box"))
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
        }

        private void SetFileState(object[] fileIds, object boxId, bool isGD)
        {
            if(isGD)
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

        private void Tab_SelectedIndexChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            string name = (sender as Control).Name;
            if(name.Contains("Project"))
            {
                int index = tab_Project_Info.SelectedTabPageIndex;
                object objid = tab_Project_Info.Tag;
                if(objid != null)
                {
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
                if(objid != null)
                {
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
                if(objId != null)
                {
                    if(index == 1)
                        LoadFileValidList(dgv_Subject_FileValid, objId, "subject_fc_");
                    else if(index == 2)
                        LoadDocList(objId, ControlType.Subject);
                }
            }
        }

        private void FileList_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            DataGridView dataGridView = sender as DataGridView;
            if("dgv_Project_FileList".Equals(dataGridView.Name))
            {
                string columnName = dgv_Project_FileList.CurrentCell.OwningColumn.Name;
                if("project_fl_stage".Equals(columnName))
                {
                    System.Windows.Forms.ComboBox con = (System.Windows.Forms.ComboBox)e.Control;
                    con.Tag = ControlType.Project;
                    con.SelectionChangeCommitted += new EventHandler(StageComboBox_SelectionChangeCommitted);
                }
                else if("project_fl_categor".Equals(columnName))
                {
                    System.Windows.Forms.ComboBox con = (System.Windows.Forms.ComboBox)e.Control;
                    con.Tag = ControlType.Project;
                    con.SelectionChangeCommitted += new EventHandler(CategorComboBox_SelectionChangeCommitted);
                }
            }
            else if("dgv_Topic_FileList".Equals(dataGridView.Name))
            {
                string columnName = dgv_Topic_FileList.CurrentCell.OwningColumn.Name;
                if("topic_fl_stage".Equals(columnName))
                {
                    System.Windows.Forms.ComboBox con = (System.Windows.Forms.ComboBox)e.Control;
                    con.Tag = ControlType.Topic;
                    con.SelectionChangeCommitted += new EventHandler(StageComboBox_SelectionChangeCommitted);
                }
                else if("topic_fl_categor".Equals(columnName))
                {
                    System.Windows.Forms.ComboBox con = (System.Windows.Forms.ComboBox)e.Control;
                    con.Tag = ControlType.Topic;
                    con.SelectionChangeCommitted += new EventHandler(CategorComboBox_SelectionChangeCommitted);
                }
            }
            else if("dgv_Subject_FileList".Equals(dataGridView.Name))
            {
                string columnName = dgv_Subject_FileList.CurrentCell.OwningColumn.Name;
                if("subject_fl_stage".Equals(columnName))
                {
                    System.Windows.Forms.ComboBox con = (System.Windows.Forms.ComboBox)e.Control;
                    con.Tag = ControlType.Subject;
                    con.SelectionChangeCommitted += new EventHandler(StageComboBox_SelectionChangeCommitted);
                }
                else if("subject_fl_categor".Equals(columnName))
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
            if((ControlType)comboBox.Tag == ControlType.Project)
                SetCategorByStage(comboBox.SelectedValue, dgv_Project_FileList.CurrentRow, "project_fl_");
            else if((ControlType)comboBox.Tag == ControlType.Topic)
                SetCategorByStage(comboBox.SelectedValue, dgv_Topic_FileList.CurrentRow, "topic_fl_");
            else if((ControlType)comboBox.Tag == ControlType.Subject)
                SetCategorByStage(comboBox.SelectedValue, dgv_Subject_FileList.CurrentRow, "subject_fl_");
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
            if((ControlType)comboBox.Tag == ControlType.Project)
                SetNameByCategor(comboBox, dgv_Project_FileList.CurrentRow, "project_fl_", tab_Project_Info.Tag);
            else if((ControlType)comboBox.Tag == ControlType.Topic)
                SetNameByCategor(comboBox, dgv_Topic_FileList.CurrentRow, "topic_fl_", tab_Topic_Info.Tag);
            else if((ControlType)comboBox.Tag == ControlType.Subject)
                SetNameByCategor(comboBox, dgv_Subject_FileList.CurrentRow, "subject_fl_", tab_Subject_Info.Tag);
            comboBox.Leave += new EventHandler(delegate (object obj, EventArgs eve)
            {
                System.Windows.Forms.ComboBox _comboBox = obj as System.Windows.Forms.ComboBox;
                _comboBox.SelectionChangeCommitted -= new EventHandler(CategorComboBox_SelectionChangeCommitted);
            });
        }

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
                object id = currentRow.Cells[key + "categorname"].Tag;
                int _amount = SqlHelper.ExecuteCountQuery($"SELECT COUNT(dd_id) FROM data_dictionary WHERE dd_pId='{id}'");
                string tempKey = ((DataRowView)comboBox.Items[0]).Row.ItemArray[1].ToString();
                if(System.Text.RegularExpressions.Regex.IsMatch(tempKey, "^[A-D]"))
                {
                    string _key = ToolHelper.GetValue(tempKey).Substring(0, 1) + _amount.ToString().PadLeft(2, '0');
                    currentRow.Cells[key + "code"].Value = _key + "-" + (amount + 1).ToString().PadLeft(2, '0');
                }
            }
            else
            {
                string _key = comboBox.Text.Split(' ')[0];
                if(System.Text.RegularExpressions.Regex.IsMatch(_key, "^[A-D]"))
                    currentRow.Cells[key + "code"].Value = _key + "-" + (amount + 1).ToString().PadLeft(2, '0');
            }
        }

        private void FileList_DataSourceChanged(object sender, EventArgs e)
        {
            DataGridView view = sender as DataGridView;
            foreach(DataGridViewRow row in view.Rows)
            {
                object id = row.Cells[view.Tag + "stage"].Value;
                SetCategorByStage(id, row, view.Tag);
            }
        }
    }
}
