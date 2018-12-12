using DevExpress.XtraEditors;
using System;
using System.Data;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace 科技计划项目档案数据采集管理系统
{
    public partial class Frm_AddFile : XtraForm
    {
        private DataGridView view;
        private object key;
        /// <summary>
        /// 文件主键
        /// </summary>
        private object fileId;
      
        /// <summary>
        /// 所属对象主键
        /// </summary>
        public object parentId;
      
        /// <summary>
        /// 光盘ID
        /// </summary>
        public object trcId;
        public Action<DataGridView, object, int> UpdateDataSource;

        public Frm_AddFile(DataGridView view, object key, object fileId, object trcId)
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
            this.view = view;
            this.key = key;
            this.trcId = trcId;
            if(fileId != null)
            {
                Text = "编辑文件";
                this.fileId = fileId;
            }
            else
            {
                btn_Last.Visible = false;
                btn_Next.Visible = false;
            }
        }

        private void Frm_AddFile_Load(object sender, EventArgs e)
        {
            //阶段
            cbo_stage.DataSource = DictionaryHelper.GetTableByCode("dic_file_jd");
            cbo_stage.DisplayMember = "dd_name";
            cbo_stage.ValueMember = "dd_id";
            //类别
            LoadCategorByStage(cbo_stage.SelectedValue);
            //默认焦点
            cbo_stage.Focus();
            //编辑状态加载信息
            LoadFileInfo(fileId);
        }

        private void LoadFileInfo(object fileId)
        {
            DataRow row = SqlHelper.ExecuteSingleRowQuery($"SELECT * FROM processing_file_list WHERE pfl_id='{fileId}'");
            if(row != null)
            {
                cbo_stage.SelectedValue = row["pfl_stage"];
                cbo_categor.SelectedValue = row["pfl_categor"];
                txt_fileCode.Text = GetValue(row["pfl_code"]);
                txt_User.Text = GetValue(row["pfl_user"]);
                txt_fileName.Text = GetValue(row["pfl_name"]);
                txt_date.Text = ToolHelper.GetDateValue(row["pfl_date"], "yyyy-MM-dd");
                num_Pages.Value = ToolHelper.GetIntValue(row["pfl_pages"], 0);
                num_Count.Value = ToolHelper.GetIntValue(row["pfl_count"], 0);
                num_Amount.Value = ToolHelper.GetIntValue(row["pfl_amount"], 0);
                SetRadioValue(row["pfl_type"], pal_type);
                txt_Unit.Text = GetValue(row["pfl_unit"]);
                LoadFileLinkList(GetValue(row["pfl_file_id"]));
                txt_Remark.Text = GetValue(row["pfl_remark"]);
            }
        }

        private void LoadFileLinkList(string ids)
        {
            if(!string.IsNullOrEmpty(ids))
            {
                string[] _ids = ids.Split(',');
                for(int i = 0; i < _ids.Length; i++)
                {
                    if(!string.IsNullOrEmpty(_ids[i]))
                    {
                        object filePath = SqlHelper.ExecuteOnlyOneQuery($"SELECT bfi_path + '\' + bfi_name FROM backup_files_info WHERE bfi_id='{_ids[i]}'");
                        AddFileToList(GetValue(filePath), _ids[i]);
                    }
                }
            }
            else
            {
                lsv_LinkList.Items.Clear();
            }
        }

        private void SetRadioValue(object value, Panel panel)
        {
            foreach(Control item in panel.Controls)
            {
                if(value.Equals(item.Tag))
                {
                    if(item is RadioButton)
                    {
                        (item as RadioButton).Checked = true;
                        break;
                    }
                    else if(item is CheckBox)
                    {
                        (item as CheckBox).Checked = true;
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// 根据阶段加载类别
        /// </summary>
        private void LoadCategorByStage(object stageValue)
        {
            string querySql = $"SELECT dd_id, dd_name+' '+extend_3 AS dd_name FROM data_dictionary WHERE dd_pId='{stageValue}' ORDER BY dd_name";
            cbo_categor.DataSource = SqlHelper.ExecuteQuery(querySql);
            cbo_categor.DisplayMember = "dd_name";
            cbo_categor.ValueMember = "dd_id";
        }
      
        /// <summary>
        /// 根据类别加载文件名称
        /// </summary>
        private void LoadFileNameByCategor(System.Windows.Forms.ComboBox comboBox)
        {
            string _tempKey = comboBox.Text.Split(' ')[0];
            if(string.IsNullOrEmpty(_tempKey))
            {
                string _tempKeyObj = GetValue(((DataRowView)comboBox.Items[comboBox.SelectedIndex]).Row.ItemArray[1]);
                if(!string.IsNullOrEmpty(_tempKeyObj))
                    _tempKey = _tempKeyObj.Split(' ')[0];
            }
            object key = _tempKey;
            object value = comboBox.SelectedValue;

            txt_fileName.Text = GetValue(SqlHelper.ExecuteOnlyOneQuery($"SELECT dd_note FROM data_dictionary WHERE dd_id='{value}'"));

            int amount = SqlHelper.ExecuteCountQuery($"SELECT COUNT(pfl_id) FROM processing_file_list WHERE pfl_categor='{value}' AND pfl_obj_id='{parentId}'");

            int _amount = comboBox.Items.Count;
            if(comboBox.SelectedIndex == _amount - 1)
            {
                string tempKey = ((DataRowView)comboBox.Items[0]).Row.ItemArray[1].ToString();
                string _key = GetValue(tempKey).Substring(0, 1) + _amount.ToString().PadLeft(2, '0');
                txt_fileCode.Text = _key + "-" + (amount + 1).ToString().PadLeft(2, '0');
            }
            else
                txt_fileCode.Text = key + "-" + (amount + 1).ToString().PadLeft(2, '0');
        }
   
        /// <summary>
        /// 阶段下拉切换事件
        /// </summary>
        private void Cbo_stage_SelectionChangeCommitted(object sender, System.EventArgs e)
        {
            LoadCategorByStage(cbo_stage.SelectedValue);
            Cbo_categor_SelectedIndexChanged(sender, e);
        }

        private string GetValue(object obj)
        {
            return obj == null ? string.Empty : obj.ToString();
        }
    
        /// <summary>
        /// 文件类别下拉事件
        /// </summary>
        private void Cbo_categor_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cbo_categor.SelectedIndex != -1)
            {
                int index = cbo_categor.SelectedIndex;
                int maxIndex = cbo_categor.Items.Count;
                LoadFileNameByCategor(cbo_categor);

                if(index == maxIndex - 1)//其他
                {
                    cbo_categor.Tag = cbo_categor.SelectedValue;
                    cbo_categor.DropDownStyle = ComboBoxStyle.DropDown;

                    string value = txt_fileCode.Text.Split('-')[0] + "-";
                    cbo_categor.Text = value;
                    cbo_categor.SelectionStart = value.Length;
                }
                else
                {
                    cbo_categor.DropDownStyle = ComboBoxStyle.DropDownList;
                }
            }
        }

        /// <summary>
        /// 将用,切割后的字符串用指定间隔符和引号重新组合
        /// </summary>
        public string GetFullStringBySplit(string _str, string flag, string param)
        {
            string result = string.Empty;
            string[] strs = _str.Split(',');
            for(int i = 0; i < strs.Length; i++)
            {
                result += $"{param}{strs[i]}{param}{flag}";
            }
            return result.Length > 0 ? result.Substring(0, result.Length - 1) : string.Empty;
        }

        /// <summary>
        /// 将用指定分隔符切割后的字符串用指定间隔符和引号重新组合
        /// </summary>
        /// <param name="_str">原字符串</param>
        /// <param name="flag">间隔符</param>
        /// <param name="param">包围符</param>
        /// <param name="splitTag">切割符</param>
        public string GetFullStringBySplit(string _str, string flag, string param, char splitTag)
        {
            string result = string.Empty;
            string[] strs = _str.Split(splitTag);
            for(int i = 0; i < strs.Length; i++)
            {
                if(!string.IsNullOrEmpty(strs[i]))
                    result += $"{param}{strs[i]}{param}{flag}";
            }
            return result.Length > 0 ? result.Substring(0, result.Length - 1) : string.Empty;
        }

        /// <summary>
        /// 将字符串数组转换成指定分隔符组合成的字符串
        /// </summary>
        /// <param name="_str">字符串数组</param>
        /// <param name="flag">分隔符</param>
        /// <param name="param">引号类型</param>
        private string GetFullStringBySplit(string[] _str, string flag, string param)
        {
            string str = string.Empty;
            for(int i = 0; i < _str.Length; i++)
                str += $"{param}{_str[i]}{param}{flag}";
            return string.IsNullOrEmpty(str) ? string.Empty : str.Substring(0, str.Length - 1);
        }



        /// <summary>
        /// 获取文件链接主键
        /// </summary>
        /// <param name="type"><para>1：ID</para><para>2：链接地址</para></param>
        private string[] GetLinkList(int type)
        {
            string[] result = new string[lsv_LinkList.Items.Count];
            for(int i = 0; i < result.Length; i++)
            {
                if(type == 1)
                    result[i] = GetValue(lsv_LinkList.Items[i].Tag);
                else if(type == 2)
                    result[i] = GetValue(lsv_LinkList.Items[i].SubItems[1].Text);
            }
            return result;
        }

        /// <summary>
        /// 添加信息到指定表格
        /// </summary>
        private void SaveFileInfo(bool isAdd)
        {
            bool isOtherType = cbo_categor.SelectedIndex == -1;

            object primaryKey = Guid.NewGuid().ToString();
            object stage = cbo_stage.SelectedValue;
            object categor = cbo_categor.SelectedValue ?? cbo_categor.Tag;
            object categorName = isOtherType ? cbo_categor.Text.Split('-')[1].Trim() : null;
            object code = txt_fileCode.Text;
            object name = txt_fileName.Text.Replace("'", "''");
            object user = txt_User.Text;
            object type = GetRadioValue(pal_type);
            object pages = num_Pages.Value;
            object count = num_Count.Value;
            object amount = num_Amount.Value;
            object date = txt_date.Text;
            object unit = txt_Unit.Text;
            object carrier = GetCarrierValue();
            object link = GetFullStringBySplit(GetLinkList(2), "；", string.Empty);
            string _fileId = GetFullStringBySplit(GetLinkList(1), ",", "'");
            object remark = txt_Remark.Text;

            if(isAdd)
            {
                if(isOtherType)
                {
                    categor = Guid.NewGuid().ToString();
                    object pid = cbo_stage.SelectedValue;
                    string value = txt_fileCode.Text.Split('-')[0];
                    int sort = cbo_categor.Items.Count - 1;
                    object dicId = SqlHelper.ExecuteOnlyOneQuery($"SELECT dd_id FROM data_dictionary WHERE dd_name='{value}' AND dd_pId='{pid}'");
                    string sqlString = string.Empty;
                    if(dicId != null)
                    {
                        categor = dicId;
                        sqlString += $"DELETE FROM data_dictionary WHERE dd_name='{value}' AND dd_pId='{stage}';";
                    }
                    sqlString += "INSERT INTO data_dictionary (dd_id, dd_name, dd_note, dd_pId, dd_sort, extend_3, extend_4) " +
                        $"VALUES('{categor}', '{value}', '{name}', '{pid}', '{sort}', '{categorName}', '{1}');";
                    SqlHelper.ExecuteNonQuery(sqlString);
                }

                string insertSql = "INSERT INTO processing_file_list (" +
                "pfl_id, pfl_stage, pfl_categor, pfl_code, pfl_name, pfl_user, pfl_type, pfl_pages, pfl_count, pfl_amount, pfl_date, pfl_unit, pfl_carrier, pfl_link, pfl_file_id, pfl_obj_id, pfl_status, pfl_sort, pfl_remark, pfl_worker_id, pfl_worker_date) " +
                $"VALUES( '{primaryKey}', '{stage}', '{categor}', '{code}', '{name}', '{user}', '{type}', '{pages}', '{count}', '{amount}', '{date}', '{unit}', '{carrier}', '{link}', '{GetFullStringBySplit(GetLinkList(1), ",", string.Empty)}', '{parentId}', -1, '{view.RowCount}', '{remark}', '{UserHelper.GetUser().UserKey}', '{DateTime.Now}');";
                //将备份表中的文件标记为已选取
                if(!string.IsNullOrEmpty(_fileId))
                    insertSql += $"UPDATE backup_files_info SET bfi_state=1 WHERE bfi_id IN ({_fileId});";
                SqlHelper.ExecuteNonQuery(insertSql);
            }
            else
            {
                string oldFileId = GetValue(SqlHelper.ExecuteOnlyOneQuery($"SELECT pfl_file_id FROM processing_file_list WHERE pfl_id='{primaryKey}';"));
                string updateSql = $"UPDATE backup_files_info SET bfi_state=0 WHERE bfi_id IN ({GetFullStringBySplit(oldFileId, ",", "'")});";
                       updateSql += "UPDATE processing_file_list SET " +
                        $"[pfl_stage] = '{stage}'" +
                        $",[pfl_categor] = '{categor}'" +
                        $",[pfl_code] = '{code}'" +
                        $",[pfl_name] = '{name}'" +
                        $",[pfl_user] = '{user}'" +
                        $",[pfl_type] = '{type}'" +
                        $",[pfl_pages] = '{pages}'" +
                        $",[pfl_count] = '{count}'" +
                        $",[pfl_amount] = '{amount}'" +
                        $",[pfl_date] = '{date}'" +
                        $",[pfl_unit] = '{unit}'" +
                        $",[pfl_carrier] = '{carrier}'" +
                        $",[pfl_link] = '{link}'" +
                        $",[pfl_remark] = '{remark}'" +
                        $",[pfl_file_id] = '{GetFullStringBySplit(GetLinkList(1), ",", string.Empty)}'" +
                        $" WHERE pfl_id= '{fileId}';";
                if(!string.IsNullOrEmpty(_fileId))
                    updateSql += $"UPDATE backup_files_info SET bfi_state=1 WHERE bfi_id IN ({_fileId});";
                SqlHelper.ExecuteNonQuery(updateSql);

                XtraMessageBox.Show("数据已保存。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }

            UpdateDataSource?.Invoke(view, parentId, -1);
        }

        private object GetCarrierValue()
        {
            bool isPaper = num_Amount.Value != 0;
            bool isElect = lsv_LinkList.Items.Count != 0;
            if(isPaper && isElect)
                return "e7bce5d4-38b7-4d74-8aa2-c580b880aabb";
            else if(isPaper && !isElect)
                return "e7bce5d4-38b7-4d74-8aa2-c580b880aaba";
            else if(!isPaper && isElect)
                return "6ffdf849-31fa-4401-a640-c371cd994daf";
            return null;
        }

        private object GetCheckBoxValue(Panel panel)
        {
            int index = 0;
            object result = null;
            foreach(CheckBox item in panel.Controls)
                if(item.Checked)
                {
                    result = item.Tag;
                    index++;
                }
            return index > 1 ? panel.Tag : result;
        }

        private object GetRadioValue(Panel panel)
        {
            foreach(Control item in panel.Controls)
            {
                if(item is RadioButton)
                {
                    if((item as RadioButton).Checked)
                    {
                        return item.Tag;
                    }
                }
            }
            return null;
        }

        private void Btn_Save_Click(object sender, EventArgs e)
        {
            if(CheckDatas())
            {
                if(Text.Contains("新增"))
                {
                    SaveFileInfo(true);
                    ResetControl();
                }
                else if(Text.Contains("编辑"))
                {
                    object CurrentRow = view.CurrentRow;
                    int CurrentRowIndex = -1;
                    if(CurrentRow != null)
                        CurrentRowIndex = ((DataGridViewRow)CurrentRow).Index;
                    UpdateFileInfo();
                    if(CurrentRowIndex != -1)
                    {
                        DataGridViewRow row = view.Rows[CurrentRowIndex];
                        view.ClearSelection();
                        row.Selected = true;
                        view.CurrentCell = row.Cells[1];
                    }
                }
            }
            else
            {
                MessageBox.Show("检查数据是否完整。", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }

        private void UpdateFileInfo()
        {
            foreach(DataGridViewRow row in view.Rows)
            {
                if(fileId.Equals(row.Cells[key + "num"].Value))
                {
                    SaveFileInfo(false);
                    break;
                }
            }
        }

        private bool CheckDatas()
        {
            bool result = true;
            errorProvider1.Clear();
            //文件类别
            if(cbo_categor.SelectedIndex == -1 || cbo_categor.SelectedIndex == cbo_categor.Items.Count - 1)
            {
                string value = cbo_categor.Text.Trim();
                if(string.IsNullOrEmpty(value) || value.StartsWith("-") || value.EndsWith("-") || !value.Contains("-"))
                {
                    errorProvider1.SetError(cbo_categor, "提示：请输入文件类别名称。");
                    result = false;
                }
                else
                {
                    //文件类别是否已存在
                    string codeParam = value.Split('-')[0];
                    int index = SqlHelper.ExecuteCountQuery($"SELECT COUNT(dd_id) FROM data_dictionary WHERE dd_name = '{codeParam}'");
                    if(index > 0)
                    {
                        errorProvider1.SetError(cbo_categor, "提示：文件类别已存在。");
                        result = false;
                    }
                }
            }
            //页数
            //NumericUpDown pagesCell = num_Pages;
            //if(pagesCell.Value == 0)
            //{
            //    errorProvider1.SetError(pagesCell, "提示：页数不能为0。");
            //    result = false;
            //}
            //文件名
            string nameValue = txt_fileName.Text.Trim();
            if(string.IsNullOrEmpty(nameValue))
            {
                errorProvider1.SetError(txt_fileName, "提示：文件名不能为空。");
                result = false;
            }
            else if(Text.Contains("新增"))
            {
                int _count = SqlHelper.ExecuteCountQuery($"SELECT COUNT(pfl_id) FROM processing_file_list WHERE pfl_name='{nameValue}' AND pfl_obj_id='{parentId}'");
                if(_count > 0)
                {
                    errorProvider1.SetError(txt_fileName, "提示：文件名已存在，请重新输入。");
                    result = false;
                }
            }
            //编号
            if(string.IsNullOrEmpty(txt_fileCode.Text.Trim()))
            {
                errorProvider1.SetError(txt_fileCode, "提示：编号不能为空。");
                result = false;
            }
            //文件类型
            int count = 0;
            foreach(RadioButton item in pal_type.Controls)
                if(item.Checked)
                { count++; break; }
            if(count == 0)
            {
                errorProvider1.SetError(pal_type, "提示：文件类型不能为空。");
                result = false;
            }
            //存放单位
            if(string.IsNullOrEmpty(txt_Unit.Text.Trim()))
            {
                errorProvider1.SetError(txt_Unit, "提示：存放单位不能为空。");
                result = false;
            }

            string dateString = txt_date.Text;
            if(!string.IsNullOrEmpty(dateString))
            {
                if(!Regex.IsMatch(dateString, "^\\d{4}-\\d{2}-\\d{2}$") || !DateTime.TryParse(dateString, out DateTime date))
                {
                    errorProvider1.SetError(txt_date, "提示：请输入格式为 yyyy-MM-dd 的有效日期。");
                    result = false;
                }
            }

            return result;
        }

        /// <summary>
        /// 根据阶段设置相应的文件类别
        /// </summary>
        /// <param name="jdId">阶段ID</param>
        public void SetCategorByStage(object jdId, DataGridViewRow dataGridViewRow, object key)
        {
            DataGridViewComboBoxCell categorCell = dataGridViewRow.Cells[key + "categor"] as DataGridViewComboBoxCell;
            string querySql = $"SELECT dd_id, dd_name+' '+extend_3 as dd_name FROM data_dictionary WHERE dd_pId='{jdId}' ORDER BY dd_sort";
            categorCell.DataSource = SqlHelper.ExecuteQuery(querySql);
            categorCell.DisplayMember = "dd_name";
            categorCell.ValueMember = "dd_id";
        }
        
        /// <summary>
        /// 重置控件
        /// </summary>
        private void ResetControl()
        {
            foreach(Control item in Controls)
            {
                if(item is TextBox || item is DateTimePicker)
                {
                    if(!"txt_Unit".Equals(item.Name))
                        item.ResetText();
                }
                else if(item is NumericUpDown)
                {
                    (item as NumericUpDown).Value = ToolHelper.GetIntValue(item.Tag, 0);
                }
                else if(item is System.Windows.Forms.ComboBox)
                {
                    if("txt_fileName".Equals(item.Name))
                    {
                        System.Windows.Forms.ComboBox comboBox = item as System.Windows.Forms.ComboBox;
                        comboBox.Items.Clear();
                        comboBox.Text = null;
                    }
                    else if("cbo_categor".Equals(item.Name))
                    {
                        (item as System.Windows.Forms.ComboBox).SelectedIndex = 0;
                    }
                }
                else if(item is Panel)
                {
                    foreach(Control _item in item.Controls)
                    {
                        if(_item is RadioButton)
                            (_item as RadioButton).Checked = false;
                        else if(_item is CheckBox)
                            (_item as CheckBox).Checked = false;
                    }
                }
                else if(item is ListView)
                {
                    (item as ListView).Items.Clear();
                }
            }
        }

        private void Frm_AddFile_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.PageUp)
            {
                Btn_Last_Click(null, null);
            }
            else if(e.KeyCode == Keys.PageDown)
            {
                Btn_Next_Click(null, null);
            }
        }

        private void Btn_Exit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            ResetControl();
        }

        private void OpenFile_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string ids = GetFullStringBySplit(GetValue(trcId), ",", "'", ';');
            if(string.IsNullOrEmpty(ids))
            {
                XtraMessageBox.Show("尚未导入文件。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            object[] rootId = SqlHelper.ExecuteSingleColumnQuery($"SELECT bfi_id FROM backup_files_info WHERE bfi_trcid IN ({ids}) AND bfi_type=-1");
            if(rootId.Length == 0)
            {
                XtraMessageBox.Show("生成文件树失败。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            Frm_AddFile_FileSelect frm = new Frm_AddFile_FileSelect(rootId);
            if(frm.ShowDialog() == DialogResult.OK)
            {
                string[] fullPath = frm.SelectedFileName;
                for(int i = 0; i < fullPath.Length; i++)
                {
                    if(File.Exists(fullPath[i]))
                    {
                        if(NotExist(fullPath[i]))
                            AddFileToList(fullPath[i], frm.SelectedFileId[i]);
                        else
                            XtraMessageBox.Show($"{Path.GetFileName(fullPath[i])}文件已存在，不可重复添加。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                    else
                        XtraMessageBox.Show($"服务器不存在文件{Path.GetFileName(fullPath[i])}。", "打开失败", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
            }
        }

        private bool NotExist(string fullPath)
        {
            foreach(ListViewItem item in lsv_LinkList.Items)
                if(fullPath.Equals(item.SubItems[1].Text))
                    return false;
            return true;
        }

        private void AddFileToList(string tPath, string fid)
        {
            string index = (lsv_LinkList.Items.Count + 1).ToString();
            ListViewItem item = lsv_LinkList.Items.Add(index);
            item.SubItems.AddRange(new ListViewItem.ListViewSubItem[]
            {
                new ListViewItem.ListViewSubItem(){ Text = tPath },
            });
            item.Tag = fid;
        }

        private void Lsv_FileList_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Delete)
            {
                int count = lsv_LinkList.SelectedItems.Count;
                if(count > 0)
                {
                    if(MessageBox.Show("是否删除选中项？", "确认提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        for(int i = 0; i < count; i++)
                            lsv_LinkList.SelectedItems[i].Remove();
                }
            }
        }

        private void Lsv_LinkList_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int index = lsv_LinkList.SelectedItems.Count;
            if(index > 0 && MessageBox.Show("是否打开文件？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                ListViewItem item = lsv_LinkList.SelectedItems[0];
                string filePath = item.SubItems[1].Text;
                if(!string.IsNullOrEmpty(filePath) && File.Exists(filePath))
                {
                    WinFormOpenHelper.OpenWinForm(Handle.ToInt32(), "open", filePath, null, null, ShowWindowCommands.SW_NORMAL);
                }
            }
        }

        private void num_Pages_Enter(object sender, EventArgs e)
        {
            NumericUpDown numeric = sender as NumericUpDown;
            int length = numeric.Value.ToString().Length;
            numeric.Select(0, length);
        }

        private void Btn_Last_Click(object sender, EventArgs e)
        {
            object currentRow = view.CurrentRow;
            if(currentRow != null)
            {
                int currentRowIndex = ((DataGridViewRow)currentRow).Index;
                if(currentRowIndex > 0)
                {
                    fileId = view.Rows[currentRowIndex - 1].Cells[0].Value;
                    Frm_AddFile_Load(null, null);
                    view.ClearSelection();
                    view.Rows[currentRowIndex - 1].Selected = true;
                    view.CurrentCell = view.Rows[currentRowIndex - 1].Cells[1];
                }
            }
        }

        private void Btn_Next_Click(object sender, EventArgs e)
        {
            object currentRow = view.CurrentRow;
            if(currentRow != null)
            {
                int currentRowIndex = ((DataGridViewRow)currentRow).Index;
                bool flag = view.AllowUserToAddRows;
                int maxRowIndex = flag ? view.RowCount - 1 : view.RowCount;
                if(currentRowIndex < maxRowIndex - 1)
                {
                    fileId = view.Rows[currentRowIndex + 1].Cells[0].Value;
                    Frm_AddFile_Load(null, null);
                    view.ClearSelection();
                    view.Rows[currentRowIndex + 1].Selected = true;
                    view.CurrentCell = view.Rows[currentRowIndex + 1].Cells[1];
                }
            }
        }
    }
}
