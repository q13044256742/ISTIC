using System;
using System.Collections.Generic;
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

            if (planId != null)
            {
                ShowTab("plan", 0);
                object[] _obj = SqlHelper.ExecuteRowsQuery($"SELECT dd_id,dd_name,dd_note FROM data_dictionary WHERE dd_id='{planId}'");
                lbl_JH_Name.Text = _obj[1].ToString();
                lbl_JH_Name.Tag = _obj[0];
                lbl_PlanIntroducation.Text = _obj[2].ToString();
                AddTreeNode(new TreeNode() { Name = Chinese2Spell.GetSpellStringLower(_obj[1].ToString()), Text = _obj[1].ToString() }, null);
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
            dgv_JH_FileValid.ColumnHeadersDefaultCellStyle = DataGridViewStyleHelper.GetHeaderStyle();

            dgv_XM_FileList.ColumnHeadersDefaultCellStyle = DataGridViewStyleHelper.GetHeaderStyle();
            dgv_XM_FileValid.ColumnHeadersDefaultCellStyle = DataGridViewStyleHelper.GetHeaderStyle();

            //阶段
            DataGridViewComboBoxColumn satgeColumn = dgv_JH_FileList.Columns["stage"] as DataGridViewComboBoxColumn;
            satgeColumn.Items.AddRange(FileStage.规划阶段.ToString(), FileStage.申报立项阶段.ToString(), FileStage.过程管理阶段.ToString(), FileStage.验收阶段.ToString());
            satgeColumn.DefaultCellStyle = new DataGridViewCellStyle() { Font = new System.Drawing.Font("宋体", 10.5f), NullValue = satgeColumn.Items[0] };
            //文件类别
            SetCategorByStage(FileStage.规划阶段, dgv_JH_FileList.Rows[0]);
            //文件类型
            DataGridViewComboBoxColumn filetypeColumn = dgv_JH_FileList.Columns["filetype"] as DataGridViewComboBoxColumn;
            filetypeColumn.Items.AddRange("财务", "管理", "评审", "技术", "汇编");
            filetypeColumn.DefaultCellStyle = new DataGridViewCellStyle() { Font = new System.Drawing.Font("宋体", 10.5f), NullValue = filetypeColumn.Items[0] };
            //密级
            DataGridViewComboBoxColumn secretColumn = dgv_JH_FileList.Columns["secret"] as DataGridViewComboBoxColumn;
            secretColumn.Items.AddRange("公开", "国内", "秘密", "机密", "绝密");
            secretColumn.DefaultCellStyle = new DataGridViewCellStyle() { Font = new System.Drawing.Font("宋体", 10.5f), NullValue = secretColumn.Items[0] };
            //载体
            DataGridViewComboBoxColumn carrierColumn = dgv_JH_FileList.Columns["carrier"] as DataGridViewComboBoxColumn;
            carrierColumn.Items.AddRange("纸质", "电子", "纸质+电子");
            carrierColumn.DefaultCellStyle = new DataGridViewCellStyle() { Font = new System.Drawing.Font("宋体", 10.5f), NullValue = carrierColumn.Items[0] };
            //文件格式
            DataGridViewComboBoxColumn formatColumn = dgv_JH_FileList.Columns["format"] as DataGridViewComboBoxColumn;
            formatColumn.Items.AddRange("PDF", "DOC", "EXCEL");
            formatColumn.DefaultCellStyle = new DataGridViewCellStyle() { Font = new System.Drawing.Font("宋体", 10.5f), NullValue = formatColumn.Items[0] };
            //文件形态
            DataGridViewComboBoxColumn formColumn = dgv_JH_FileList.Columns["form"] as DataGridViewComboBoxColumn;
            formColumn.Items.AddRange("原件", "复印本");
            formColumn.DefaultCellStyle = new DataGridViewCellStyle() { Font = new System.Drawing.Font("宋体", 10.5f), NullValue = formColumn.Items[0] };

            cbo_JH_Next.SelectedIndex = 0;
            cbo_JH_XM_HasNext.SelectedIndex = 0;
            cbo_JH_XM_KT_HasNext.SelectedIndex = 0;
        }

        /// <summary>
        /// 根据阶段设置相应的文件类别
        /// </summary>
        /// <param name="fileStage">阶段</param>
        private void SetCategorByStage(FileStage fileStage, DataGridViewRow dataGridViewRow)
        {
            //文件类别
            DataGridViewComboBoxCell categorCell = dataGridViewRow.Cells["categor"] as DataGridViewComboBoxCell;
            categorCell.Items.Clear();
            switch (fileStage)
            {
                case FileStage.规划阶段:
                    for (int i = 1; i <= 5; i++)
                        categorCell.Items.Add("A"+ i.ToString().PadLeft(2, '0'));
                    break;
                case FileStage.申报立项阶段:
                    for (int i = 1; i <= 8; i++)
                        categorCell.Items.Add("B" + i.ToString().PadLeft(2, '0'));
                    break;
                case FileStage.过程管理阶段:
                    for (int i = 1; i <= 11; i++)
                        categorCell.Items.Add("C" + i.ToString().PadLeft(2, '0'));
                    break;
                case FileStage.验收阶段:
                    for (int i = 1; i <= 29; i++)
                        categorCell.Items.Add("D" + i.ToString().PadLeft(2, '0'));
                    break;
            }
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
            FileStage fileStage = (FileStage)comboBox.SelectedIndex;
            SetCategorByStage(fileStage, dgv_JH_FileList.CurrentRow);

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
            SetNameByCategor(comboBox.SelectedItem.ToString(), dgv_JH_FileList.CurrentRow);

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
        private void SetNameByCategor(string catogerCode, DataGridViewRow currentRow)
        {
            string value = string.Empty;
            if (catogerCode.Contains("A"))
                value = "A--";
            else if (catogerCode.Contains("B"))
                value = "B--";
            else if (catogerCode.Contains("C"))
                value = "C--";
            else if (catogerCode.Contains("D"))
                value = "D--";
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
                for (int i = 0; i < maxLength - 2; i++)
                {
                    object stage = dgv_JH_FileList.Rows[i].Cells["stage"].Value;
                    object categor = dgv_JH_FileList.Rows[i].Cells["categor"].Value;
                    object name = dgv_JH_FileList.Rows[i].Cells["name"].Value;
                    object user = dgv_JH_FileList.Rows[i].Cells["user"].Value;
                    object filetype = dgv_JH_FileList.Rows[i].Cells["filetype"].Value;
                    object secret = dgv_JH_FileList.Rows[i].Cells["secret"].Value;
                    object page = dgv_JH_FileList.Rows[i].Cells["page"].Value;
                    object amount = dgv_JH_FileList.Rows[i].Cells["amount"].Value;
                    object date = dgv_JH_FileList.Rows[i].Cells["date"].Value;
                    object unit = dgv_JH_FileList.Rows[i].Cells["unit"].Value;
                    object carrier = dgv_JH_FileList.Rows[i].Cells["carrier"].Value;
                    object format = dgv_JH_FileList.Rows[i].Cells["format"].Value;
                    object form = dgv_JH_FileList.Rows[i].Cells["form"].Value;
                    object link = dgv_JH_FileList.Rows[i].Cells["link"].Value;
                    object remark = dgv_JH_FileList.Rows[i].Cells["remark"].Value;
                    object objId = lbl_JH_Name.Tag;
                    GuiDangStatus status = GuiDangStatus.NonGuiDang;

                    string insertSql = $"INSERT INTO [dbo].[processing_file_list] VALUES ('{Guid.NewGuid().ToString()}' " +
                        $",{stage},{categor} ,'{name}' ,'{user}' " +
                        $",{filetype} ,{secret} ,{page} ,{amount} ,'{date}' " +
                        $",'{unit}' ,'{carrier}',{format} ,{form},'{objId}'" +
                        $",'{link}' ,'{remark}' ,{(int)status} ,null,'{DateTime.Now}')";

                    SqlHelper.ExecuteNonQuery(insertSql);
                }
            }
        }

    }
}
