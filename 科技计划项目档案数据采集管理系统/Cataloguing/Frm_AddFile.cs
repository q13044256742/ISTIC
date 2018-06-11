using System;
using System.Data;
using System.IO;
using System.Windows.Forms;

namespace 科技计划项目档案数据采集管理系统
{
    public partial class Frm_AddFile : DevExpress.XtraEditors.XtraForm
    {
        private DataGridView view;
        private object key;
        private object fileId;
      
        /// <summary>
        /// 所属对象主键
        /// </summary>
        public object parentId;
      
        /// <summary>
        /// 光盘ID
        /// </summary>
        public object trcId;

        public Frm_AddFile(DataGridView view, object key, object fileId)
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
            this.view = view;
            this.key = key;
            if(fileId != null)
            {
                Text = "编辑文件";
                this.fileId = fileId;
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
                dtp_date.Value = Convert.ToDateTime(row["pfl_date"]);
                num_Pages.Value = Convert.ToInt32(row["pfl_pages"]);
                SetRadioValue(row["pfl_type"], pal_type);
                SetRadioValue(row["pfl_carrier"], pal_carrier);
                num_Amount.Value = Convert.ToInt32(row["pfl_amount"]);
                txt_Unit.Text = GetValue(row["pfl_unit"]);
                txt_Link.Text = GetValue(row["pfl_link"]);
                txt_Remark.Text = GetValue(row["pfl_remark"]);
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
            string querySql = $"SELECT dd_id, dd_name+' '+extend_3 AS dd_name FROM data_dictionary WHERE dd_pId='{stageValue}' ORDER BY dd_sort";
            cbo_categor.DataSource = SqlHelper.ExecuteQuery(querySql);
            cbo_categor.DisplayMember = "dd_name";
            cbo_categor.ValueMember = "dd_id";
        }
      
        /// <summary>
        /// 根据类别加载文件名称
        /// </summary>
        private void LoadFileNameByCategor(ComboBox comboBox)
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

            object[] fileName = SqlHelper.ExecuteSingleColumnQuery($"SELECT pfl_name FROM processing_file_list WHERE pfl_categor='{value}' AND pfl_obj_id='{parentId}'");
            txt_fileName.Items.Clear();
            txt_fileName.Items.AddRange(fileName);
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
        /// 添加信息到指定表格
        /// </summary>
        private object SaveFileInfo(DataGridViewRow row, bool isAdd)
        {
            bool isOtherType = cbo_categor.SelectedIndex == -1;

            object primaryKey = Guid.NewGuid().ToString();
            row.Cells[key + "id"].Value = row.Index + 1;
            row.Cells[key + "stage"].Value = cbo_stage.SelectedValue;
            SetCategorByStage(cbo_stage.SelectedValue, row, key);
            row.Cells[key + "categor"].Value = cbo_categor.SelectedValue ?? cbo_categor.Tag;
            object categorName = isOtherType ? cbo_categor.Text.Split('-')[1].Trim() : null;
            row.Cells[key + "name"].Value = txt_fileName.Text;
            row.Cells[key + "code"].Value = txt_fileCode.Text;
            row.Cells[key + "user"].Value = txt_User.Text;
            row.Cells[key + "type"].Value = GetRadioValue(pal_type);
            row.Cells[key + "pages"].Value = num_Pages.Value;
            row.Cells[key + "amount"].Value = num_Amount.Value;
            row.Cells[key + "date"].Value = dtp_date.Value.ToString("yyyyMMdd");
            row.Cells[key + "unit"].Value = txt_Unit.Text;
            row.Cells[key + "carrier"].Value = GetCheckBoxValue(pal_carrier);
            object format = Path.GetExtension(txt_Link.Text).Replace(".", string.Empty);
            row.Cells[key + "link"].Value = txt_Link.Text;
            if(isAdd)
            {
                object stage = row.Cells[key + "stage"].Value;
                object categor = row.Cells[key + "categor"].Value;
                object code = row.Cells[key + "code"].Value;
                object name = row.Cells[key + "name"].Value;
                object user = row.Cells[key + "user"].Value;
                object type = row.Cells[key + "type"].Value;
                object pages = row.Cells[key + "pages"].Value;
                object count = row.Cells[key + "amount"].Value;
                DateTime date = DateTime.Now;
                string _date = GetValue(row.Cells[key + "date"].Value);
                if(!string.IsNullOrEmpty(_date))
                {
                    if(_date.Length == 4)
                        _date = _date + "-" + date.Month + "-" + date.Day;
                    else if(_date.Length == 6)
                        _date = _date.Substring(0, 4) + "-" + _date.Substring(4, 2) + "-" + date.Day;
                    else if(_date.Length == 8)
                        _date = _date.Substring(0, 4) + "-" + _date.Substring(4, 2) + "-" + _date.Substring(6, 2);
                    DateTime.TryParse(_date, out date);
                }
                object unit = row.Cells[key + "unit"].Value;
                object carrier = row.Cells[key + "carrier"].Value;
                object link = row.Cells[key + "link"].Value;
                object fileId = txt_Link.Tag;
                object remark = txt_Remark.Text;

                if(isOtherType)
                {
                    categor = Guid.NewGuid().ToString();
                    object pid = cbo_stage.SelectedValue;
                    string value = txt_fileCode.Text.Split('-')[0];
                    int sort = cbo_categor.Items.Count - 1;

                    string _insertSql = "INSERT INTO data_dictionary (dd_id, dd_name, dd_pId, dd_sort, extend_3, extend_4) " +
                        $"VALUES('{categor}', '{value}', '{pid}', '{sort}', '{categorName}', '{1}');";
                    SqlHelper.ExecuteNonQuery(_insertSql);
                }

                string insertSql = "INSERT INTO processing_file_list (" +
                "pfl_id, pfl_stage, pfl_categor, pfl_code, pfl_name, pfl_user, pfl_type, pfl_pages, pfl_amount, pfl_date, pfl_unit, pfl_carrier, pfl_format, pfl_link, pfl_remark, pfl_file_id, pfl_obj_id, pfl_sort) " +
                $"VALUES( '{primaryKey}', '{stage}', '{categor}', '{code}', '{name}', '{user}', '{type}', '{pages}', '{count}', '{date}', '{unit}', '{carrier}', '{format}', '{link}', '{remark}', '{fileId}', '{parentId}', '{row.Index}');";
                if(fileId != null)
                {
                    int value = link == null ? 0 : 1;
                    insertSql += $"UPDATE backup_files_info SET bfi_state={value} WHERE bfi_id='{fileId}';";
                }
                SqlHelper.ExecuteNonQuery(insertSql);

                row.Cells[key + "id"].Tag = primaryKey;
            }
            else
            {
                primaryKey = row.Cells[key + "id"].Tag;
                object stage = row.Cells[key + "stage"].Value;
                object categor = row.Cells[key + "categor"].Value;
                object code = row.Cells[key + "code"].Value;
                object name = row.Cells[key + "name"].Value;
                object user = row.Cells[key + "user"].Value;
                object type = row.Cells[key + "type"].Value;
                object pages = row.Cells[key + "pages"].Value;
                object count = row.Cells[key + "amount"].Value;
                DateTime date = DateTime.Now;
                string _date = GetValue(row.Cells[key + "date"].Value);
                if(!string.IsNullOrEmpty(_date))
                {
                    if(_date.Length == 4)
                        _date = _date + "-" + date.Month + "-" + date.Day;
                    else if(_date.Length == 6)
                        _date = _date.Substring(0, 4) + "-" + _date.Substring(4, 2) + "-" + date.Day;
                    else if(_date.Length == 8)
                        _date = _date.Substring(0, 4) + "-" + _date.Substring(4, 2) + "-" + _date.Substring(6, 2);
                    DateTime.TryParse(_date, out date);
                }
                object unit = row.Cells[key + "unit"].Value;
                object carrier = row.Cells[key + "carrier"].Value;
                object link = row.Cells[key + "link"].Value;
                object fileId = txt_Link.Tag;
                object remark = txt_Remark.Text;
                string updateSql = "UPDATE processing_file_list SET " +
                        $"[pfl_stage] = '{stage}'" +
                        $",[pfl_categor] = '{categor}'" +
                        $",[pfl_code] = '{code}'" +
                        $",[pfl_name] = '{name}'" +
                        $",[pfl_user] = '{user}'" +
                        $",[pfl_type] = '{type}'" +
                        $",[pfl_pages] = '{pages}'" +
                        $",[pfl_amount] = '{count}'" +
                        $",[pfl_date] = '{date}'" +
                        $",[pfl_unit] = '{unit}'" +
                        $",[pfl_carrier] = '{carrier}'" +
                        $",[pfl_format] = '{format}'" +
                        $",[pfl_link] = '{link}'" +
                        $",[pfl_remark] = '{remark}'" +
                        $",[pfl_sort] = '{row.Index}'" +
                        $",[pfl_file_id] = '{fileId}'" +
                        $" WHERE pfl_id= '{primaryKey}';";
                if(fileId != null)
                {
                    int value = link == null ? 0 : 1;
                    updateSql += $"UPDATE backup_files_info SET bfi_state={value} WHERE bfi_id='{fileId}';";
                }
                SqlHelper.ExecuteNonQuery(updateSql);
                MessageBox.Show("数据已保存。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            return primaryKey;
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
                    SaveFileInfo(view.Rows[view.Rows.Add()], true);
                    ResetControl();
                }
                else if(Text.Contains("更新"))
                    UpdateFileInfo();
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
                if(fileId.Equals(row.Cells[key + "id"].Tag))
                {
                    SaveFileInfo(row, false);
                    break;
                }
            }
        }

        private bool CheckDatas()
        {
            ErrorProvider errorProvider1 = new ErrorProvider();
            bool result = true;
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
                    errorProvider1.SetError(cbo_categor, null);
            }
            //页数
            NumericUpDown pagesCell = num_Pages;
            if(pagesCell.Value == 0)
            {
                errorProvider1.SetError(pagesCell, "提示：页数不能为0。");
                result = false;
            }
            else
                errorProvider1.SetError(pagesCell, null);
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
                else
                    errorProvider1.SetError(txt_fileName, null);
            }
            //编号
            if(string.IsNullOrEmpty(txt_fileCode.Text.Trim()))
            {
                errorProvider1.SetError(txt_fileCode, "提示：编号不能为空。");
                result = false;
            }
            else
                errorProvider1.SetError(txt_fileCode, null);
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
            else
                errorProvider1.SetError(pal_type, null);
            //载体
            count = 0;
            foreach(CheckBox item in pal_carrier.Controls)
                if(item.Checked)
                { count++; break; }
            if(count == 0)
            {
                errorProvider1.SetError(pal_carrier, "提示：载体不能为空。");
                result = false;
            }
            else
                errorProvider1.SetError(pal_carrier, null);

            //存放单位
            if(string.IsNullOrEmpty(txt_Unit.Text.Trim()))
            {
                errorProvider1.SetError(txt_Unit, "提示：存放单位不能为空。");
                result = false;
            }
            else
                errorProvider1.SetError(txt_Unit, null);
            return result;
        }

        /// <summary>
        /// 根据阶段设置相应的文件类别
        /// </summary>
        /// <param name="jdId">阶段ID</param>
        public void SetCategorByStage(object jdId, DataGridViewRow dataGridViewRow, object key)
        {
            DataGridViewComboBoxCell categorCell = dataGridViewRow.Cells[key + "categor"] as DataGridViewComboBoxCell;
            string querySql = $"SELECT dd_id, dd_name FROM data_dictionary WHERE dd_pId='{jdId}' ORDER BY dd_sort";
            categorCell.DataSource = SqlHelper.ExecuteQuery(querySql);
            categorCell.DisplayMember = "dd_name";
            categorCell.ValueMember = "dd_id";
            categorCell.Style = new DataGridViewCellStyle() { Font = new System.Drawing.Font("宋体", 10.5f), NullValue = categorCell.Items[0] };
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
                    (item as NumericUpDown).Value = 0;
                else if(item is ComboBox)
                {
                    if("txt_fileName".Equals(item.Name))
                    {
                        ComboBox comboBox = item as ComboBox;
                        comboBox.Items.Clear();
                        comboBox.Text = null;
                    }
                    else if("cbo_categor".Equals(item.Name))
                    {
                        (item as ComboBox).SelectedIndex = 0;
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
            }
        }

        private void Frm_AddFile_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Control && e.KeyCode == Keys.Enter)
            {
                Btn_Save_Click(null, null);
            }
        }

        private void btn_Exit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ResetControl();
        }

        private void OpenFile_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            object[] rootId = SqlHelper.ExecuteSingleColumnQuery($"SELECT bfi_id FROM backup_files_info WHERE bfi_trcid='{trcId}' AND bfi_sort=-1");
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
                    txt_Link.Text = fullPath;
                    if(MessageBox.Show("已从服务器拷贝文件到本地，是否现在打开？", "操作确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        System.Diagnostics.Process.Start("EXPLORER.EXE", filePath);
                }
                else
                    MessageBox.Show("服务器不存在此文件。", "打开失败", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }

        private void Frm_AddFile_FormClosing(object sender, FormClosingEventArgs e)
        {
        }

        private void FileName_SelectionChangeCommitted(object sender, EventArgs e)
        {
            object fileName = txt_fileName.SelectedItem;
            if(fileName != null)
            {
                Hide();
                DataRow row = SqlHelper.ExecuteSingleRowQuery($"SELECT pfl_id FROM processing_file_list WHERE pfl_name='{fileName}' AND pfl_obj_id='{parentId}'");
                new Frm_AddFile(view, key, row["fi_id"]).ShowDialog();
            }
        }
    }
}
