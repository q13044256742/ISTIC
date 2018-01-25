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

            ShowTab("plan", 0);
            object[] _obj = SqlHelper.ExecuteRowsQuery($"SELECT dd_id,dd_name,dd_note FROM data_dictionary WHERE dd_id='{planId}'");
            lbl_JH_Name.Text = _obj[1].ToString();
            lbl_JH_Name.Tag = _obj[0];
            lbl_PlanIntroducation.Text = _obj[2].ToString();

            LoadTreeList(lbl_JH_Name.Tag);

            LoadFileList(lbl_JH_Name.Tag);
        }

        /// <summary>
        /// 加载当前文件列表
        /// </summary>
        /// <param name="planId">计划主键</param>
        private void LoadFileList(object planId)
        {
            string querySql = $"SELECT * FROM processing_file_list WHERE pfl_obj_id='{planId}'";
            DataTable dataTable = SqlHelper.ExecuteQuery(querySql);
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                int index = dgv_JH_FileList.Rows.Add();
                dgv_JH_FileList.Rows[index].Cells["id"].Value = i + 1;
                dgv_JH_FileList.Rows[index].Cells["id"].Tag = dataTable.Rows[i]["pfl_id"];
                dgv_JH_FileList.Rows[index].Cells["stage"].Value = dataTable.Rows[i]["pfl_stage"];
                dgv_JH_FileList.Rows[index].Cells["categor"].Value = dataTable.Rows[i]["pfl_categor"];
                dgv_JH_FileList.Rows[index].Cells["name"].Value = dataTable.Rows[i]["pfl_filename"];
                dgv_JH_FileList.Rows[index].Cells["user"].Value = dataTable.Rows[i]["pfl_user"];
                dgv_JH_FileList.Rows[index].Cells["filetype"].Value = dataTable.Rows[i]["pfl_type"];
                dgv_JH_FileList.Rows[index].Cells["secret"].Value = dataTable.Rows[i]["pfl_scert"];
                dgv_JH_FileList.Rows[index].Cells["page"].Value = dataTable.Rows[i]["pfl_page_amount"];
                dgv_JH_FileList.Rows[index].Cells["amount"].Value = dataTable.Rows[i]["pfl_amount"];
                dgv_JH_FileList.Rows[index].Cells["date"].Value = dataTable.Rows[i]["pfl_complete_date"];
                dgv_JH_FileList.Rows[index].Cells["unit"].Value = dataTable.Rows[i]["pfl_save_location"];
                dgv_JH_FileList.Rows[index].Cells["carrier"].Value = dataTable.Rows[i]["pfl_carrier"];
                dgv_JH_FileList.Rows[index].Cells["format"].Value = dataTable.Rows[i]["pfl_file_format"];
                dgv_JH_FileList.Rows[index].Cells["form"].Value = dataTable.Rows[i]["pfl_file_form"];
                dgv_JH_FileList.Rows[index].Cells["link"].Value = dataTable.Rows[i]["pfl_file_link"];
                dgv_JH_FileList.Rows[index].Cells["remark"].Value = dataTable.Rows[i]["pfl_remark"];
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
        /// <param name="index">所属索引序列(删除指定索引及以后的TabPages)</param>
        private void ShowTab(string name, int index)
        {
            int amount = tab_MenuList.TabPages.Count;
            if (index > 0)//删除大于等于指定索引的选项卡
            {
                List<TabPage> removeList = new List<TabPage>();
                for (int i = 0; i < amount; i++)
                    if (i >= index)
                        removeList.Add(tab_MenuList.TabPages[i]);
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

            dgv_XM_FileList.ColumnHeadersDefaultCellStyle = DataGridViewStyleHelper.GetHeaderStyle();
            dgv_XM_FileList.DefaultCellStyle = DataGridViewStyleHelper.GetCellStyle();
            dgv_XM_FileValid.ColumnHeadersDefaultCellStyle = DataGridViewStyleHelper.GetHeaderStyle();

            //阶段
            DataGridViewComboBoxColumn satgeColumn = dgv_JH_FileList.Columns["stage"] as DataGridViewComboBoxColumn;
            satgeColumn.DataSource = DictionaryHelper.GetTableByCode("dic_file_jd");
            satgeColumn.DisplayMember = "dd_name";
            satgeColumn.ValueMember = "dd_id";
            satgeColumn.DefaultCellStyle = new DataGridViewCellStyle() { Font = new System.Drawing.Font("宋体", 10.5f) };

            //文件类别
            for (int i = 0; i < dgv_JH_FileList.Rows.Count; i++)
                if (dgv_JH_FileList.Rows[i].Cells["id"].Value != null)
                {
                    DataGridViewComboBoxCell satgeCell = (DataGridViewComboBoxCell)dgv_JH_FileList.Rows[i].Cells["stage"];
                    object stageId = satgeCell.Value;
                    if (stageId != null)
                        SetCategorByStage(stageId, dgv_JH_FileList.Rows[i]);
                }

            //文件类型
            DataGridViewComboBoxColumn filetypeColumn = dgv_JH_FileList.Columns["filetype"] as DataGridViewComboBoxColumn;
            filetypeColumn.DataSource = DictionaryHelper.GetTableByCode("dic_file_type");
            filetypeColumn.DisplayMember = "dd_name";
            filetypeColumn.ValueMember = "dd_id";
            filetypeColumn.DefaultCellStyle = new DataGridViewCellStyle() { Font = new System.Drawing.Font("宋体", 10.5f) };

            //密级
            DataGridViewComboBoxColumn secretColumn = dgv_JH_FileList.Columns["secret"] as DataGridViewComboBoxColumn;
            secretColumn.DataSource = DictionaryHelper.GetTableByCode("dic_file_mj");
            secretColumn.DisplayMember = "dd_name";
            secretColumn.ValueMember = "dd_id";
            secretColumn.DefaultCellStyle = new DataGridViewCellStyle() { Font = new System.Drawing.Font("宋体", 10.5f) };
            //载体
            DataGridViewComboBoxColumn carrierColumn = dgv_JH_FileList.Columns["carrier"] as DataGridViewComboBoxColumn;
            carrierColumn.DataSource = DictionaryHelper.GetTableByCode("dic_file_zt");
            carrierColumn.DisplayMember = "dd_name";
            carrierColumn.ValueMember = "dd_id";
            carrierColumn.DefaultCellStyle = new DataGridViewCellStyle() { Font = new System.Drawing.Font("宋体", 10.5f) };
            //文件格式
            DataGridViewComboBoxColumn formatColumn = dgv_JH_FileList.Columns["format"] as DataGridViewComboBoxColumn;
            formatColumn.DataSource = DictionaryHelper.GetTableByCode("dic_file_format");
            formatColumn.DisplayMember = "dd_name";
            formatColumn.ValueMember = "dd_id";
            formatColumn.DefaultCellStyle = new DataGridViewCellStyle() { Font = new System.Drawing.Font("宋体", 10.5f) };
            //文件形态
            DataGridViewComboBoxColumn formColumn = dgv_JH_FileList.Columns["form"] as DataGridViewComboBoxColumn;
            formColumn.DataSource = DictionaryHelper.GetTableByCode("dic_file_state");
            formColumn.DisplayMember = "dd_name";
            formColumn.ValueMember = "dd_id";
            formColumn.DefaultCellStyle = new DataGridViewCellStyle() { Font = new System.Drawing.Font("宋体", 10.5f) };

            cbo_JH_Next.SelectedIndex = 0;
            cbo_JH_XM_HasNext.SelectedIndex = 0;
            cbo_JH_XM_KT_HasNext.SelectedIndex = 0;

        }

        /// <summary>
        /// 根据阶段设置相应的文件类别
        /// </summary>
        /// <param name="jdId">阶段ID</param>
        private void SetCategorByStage(object jdId, DataGridViewRow dataGridViewRow)
        {
            //文件类别
            DataGridViewComboBoxCell categorCell = dataGridViewRow.Cells["categor"] as DataGridViewComboBoxCell;

            string querySql = $"SELECT dd_id, dd_name FROM data_dictionary WHERE dd_pId='{jdId}'";
            categorCell.DataSource = SqlHelper.ExecuteQuery(querySql);
            categorCell.DisplayMember = "dd_name";
            categorCell.ValueMember = "dd_id";
            categorCell.Style = new DataGridViewCellStyle() { Font = new System.Drawing.Font("宋体", 10.5f), NullValue = categorCell.Items[0] };
        }

        //单元格事件绑定
        private void dgv_JH_File_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            string columnName = dgv_JH_FileList.CurrentCell.OwningColumn.Name;

            //阶段
            if ("stage".Equals(columnName))
            {
                (e.Control as ComboBox).SelectionChangeCommitted += new EventHandler(StageComboBox_SelectionChangeCommitted);
            }
            else if ("categor".Equals(columnName))
            {
                (e.Control as ComboBox).SelectionChangeCommitted += new EventHandler(CategorComboBox_SelectionChangeCommitted);
            }
        }

        /// <summary>
        /// 文件阶段 下拉事件
        /// </summary>
        private void StageComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;
            SetCategorByStage(comboBox.SelectedValue, dgv_JH_FileList.CurrentRow);

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
            SetNameByCategor(comboBox.SelectedValue, dgv_JH_FileList.CurrentRow);

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
        private void SetNameByCategor(object catogerId, DataGridViewRow currentRow)
        {
            string value = GetValue(SqlHelper.ExecuteOnlyOneQuery($"SELECT dd_note FROM data_dictionary WHERE dd_id='{catogerId}'"));
            dgv_JH_FileList.CurrentRow.Cells["name"].Value = value;
        }


        //表单错误处理
        private void Dgv_JH_File_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
           
        }

        private void btn_OpenFile_Click(object sender, EventArgs e)
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
                ShowTab(null, 1);
            }
            else if(index == 1)//父级 - 项目
            {
                ShowTab("plan_project", 1);
            }
            else if (index == 2)//父级 - 课题
            {
                ShowTab("plan_topic", 1);
            }
        }

        private void cbo_JH_XM_HasNext_SelectionChangeCommitted(object sender, EventArgs e)
        {
            int index = cbo_JH_XM_HasNext.SelectedIndex;
            if (index == 0)//无
            {
                ShowTab(null, 2);
            }
            else if (index == 1)//子级 - 课题
            {
                ShowTab("plan_project_topic", 2);
            }
        }
        //cbo_JH_XM_KT_HasNext
        private void cbo_JH_XM_KT_HasNext_SelectionChangeCommitted(object sender, EventArgs e)
        {
            int index = cbo_JH_XM_KT_HasNext.SelectedIndex;
            if (index == 0)//无
            {
                ShowTab(null, 3);
            }
            else if (index == 1)//子级 - 子课题
            {
                ShowTab("plan_project_topic_subtopic", 3);
            }
        }
        //
        private void cbo_JH_KT_HasNext_SelectionChangeCommitted(object sender, EventArgs e)
        {
            int index = cbo_JH_KT_HasNext.SelectedIndex;
            if (index == 0)//无
            {
                ShowTab(null, 2);
            }
            else if (index == 1)//子级 - 子课题
            {
                ShowTab("plan_topic_subtopic", 2);
            }
        }


        /// <summary>
        /// 计划- 保存
        /// </summary>
        private void Btn_JH_Save_Click(object sender, EventArgs e)
        {
            int maxLength = dgv_JH_FileList.Rows.Count;
            if(maxLength > 1)
            {
                for (int i = 0; i < maxLength - 1; i++)
                {
                    DataGridViewRow row = dgv_JH_FileList.Rows[i];
                    object id = row.Cells["id"].Value;
                    if (id == null)//新增
                    {
                        object pflid = AddFileInfo(row);
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
                LoadTreeList(lbl_JH_Name.Tag);
            }
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
        private object AddFileInfo(DataGridViewRow row)
        {
            object pflid = Guid.NewGuid().ToString();
            object stage = row.Cells["stage"].Value;
            object categor = row.Cells["categor"].Value;
            object name = row.Cells["name"].Value;
            object user = row.Cells["user"].Value;
            object filetype = row.Cells["filetype"].Value;
            object secret = row.Cells["secret"].Value;
            object page = row.Cells["page"].Value;
            object amount = row.Cells["amount"].Value;
            object date = row.Cells["date"].Value;
            object unit = row.Cells["unit"].Value;
            object carrier = row.Cells["carrier"].Value;
            object format = row.Cells["format"].Value;
            object form = row.Cells["form"].Value;
            object link = row.Cells["link"].Value;
            object remark = row.Cells["remark"].Value;
            object objId = lbl_JH_Name.Tag;
            GuiDangStatus status = GuiDangStatus.NonGuiDang;

            string insertSql = $"INSERT INTO processing_file_list VALUES ('{pflid}' " +
                $",'{stage}','{categor}' ,'{name}' ,'{user}' " +
                $",'{filetype}' ,'{secret}' ,'{page}' ,'{amount}' ,'{date}' " +
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
                Name = _obj[0].ToString(),
                Text = _obj[1].ToString()
            };
            //根据【计划】查询【项目/课题】集
            List<object[]> list = SqlHelper.ExecuteColumnsQuery($"SELECT pi_id,pi_name FROM project_info WHERE pi_obj_id='{treeNode.Name}'", 2);
            for (int i = 0; i < list.Count; i++)
            {
                TreeNode treeNode2 = new TreeNode()
                {
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
                        Name = list[i][0].ToString(),
                        Text = list[i][1].ToString()
                    };
                    treeNode2.Nodes.Add(treeNode3);
                }
            }
            
            treeView.Nodes.Add(treeNode);
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
                LoadJHFileBoxTable(cbo_JH_Box.Items[0], lbl_JH_Name.Tag);
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
            querySql = $"SELECT pfl_id,dd_name,pfl_filename FROM processing_file_list LEFT JOIN data_dictionary ON pfl_categor=dd_id WHERE pfl_id IN(" +
                $"SELECT pb_files_id FROM processing_box WHERE pb_id = '{pbId}' AND pb_obj_id = '{objId}')";
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
        private void Btn_JH_Box_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;
            //计划
            if (button.Name.Contains("btn_JH_Box"))
            {
                if ("btn_JH_Box_Right".Equals(button.Name))
                {
                    if (lsv_JH_File1.SelectedItems.Count > 0)
                    {
                        if (MessageBox.Show("是否确认将选中行数据进行归档操作?", "确认提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                        {
                            int count = lsv_JH_File1.SelectedItems.Count;
                            object[] _obj = new object[count];
                            for (int i = 0; i < count; i++)
                                _obj[i] = lsv_JH_File1.SelectedItems[i].SubItems[0].Text;
                            SetFileState(_obj, cbo_JH_Box.SelectedValue);

                            MessageBox.Show("操作成功！");
                        }
                    }
                }
                else if ("btn_JH_Box_RightAll".Equals(button.Name))
                {

                }
                else if ("btn_JH_Box_Left".Equals(button.Name))
                {

                }
                else if ("btn_JH_Box_LeftAll".Equals(button.Name))
                {

                }
                LoadJHFileBoxTable(cbo_JH_Box.SelectedValue, lbl_JH_Name.Tag);
            }
        }

        /// <summary>
        /// 文件归档
        /// </summary>
        /// <param name="_obj">待归档文件IDS</param>
        /// <param name="pbid">案卷盒ID</param>
        private void SetFileState(object[] _obj, object pbid)
        {
            //将文件状态置为已归档
            StringBuilder updateSql = new StringBuilder($"UPDATE processing_file_list SET pfl_status={(int)GuiDangStatus.GuiDangSuccess} WHERE pfl_id IN (");
            for (int i = 0; i < _obj.Length; i++)
                updateSql.Append($"'{_obj[i]}'{(i == _obj.Length - 1 ? ")" : ",")}");
            SqlHelper.ExecuteNonQuery(updateSql.ToString());

            //当前案卷盒中加入已归档文件ID
            updateSql = new StringBuilder($"UPDATE processing_box SET pb_files_id='");
            for (int i = 0; i < _obj.Length; i++)
                updateSql.Append($"{_obj[i]}{(i == _obj.Length - 1 ? "'" : ",")}");
            updateSql.Append($" WHERE pb_id='{pbid}'");
            SqlHelper.ExecuteNonQuery(updateSql.ToString());
        }

        /// <summary>
        /// 计划 - 增加/删除案卷盒
        /// </summary>
        private void Lbl_JH_Box_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
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
                            object ids = SqlHelper.ExecuteOnlyOneQuery($"SELECT pb_files_id FROM processing_box WHERE pb_obj_id='{lbl_JH_Name.Tag}'");
                            string[] _ids = ids.ToString().Split(',');
                            StringBuilder sb = new StringBuilder($"UPDATE processing_file_list SET pfl_status={(int)GuiDangStatus.NonGuiDang} WHERE pfl_id IN(");
                            for (int i = 0; i < _ids.Length; i++)
                                sb.Append(_ids + "" + (_ids.Length - 1 != i ? "," : ")"));
                            SqlHelper.ExecuteNonQuery(sb.ToString());

                            //删除当前盒信息
                            SqlHelper.ExecuteNonQuery($"DELETE FROM processing_box WHERE pb_id='{value}'");
                        }
                    }
                }
                LoadJHBoxList(lbl_JH_Name.Tag);
                //默认选中最后一盒
                if (cbo_JH_Box.Items.Count > 0)
                    cbo_JH_Box.SelectedIndex = cbo_JH_Box.Items.Count - 1;
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
        }

        /// <summary>
        /// 案卷盒切换事件
        /// </summary>
        private void Cbo_JH_Box_SelectionChangeCommitted(object sender, EventArgs e)
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
    }
}
