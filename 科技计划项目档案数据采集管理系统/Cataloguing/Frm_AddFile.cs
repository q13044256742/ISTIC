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
        /// <summary>
        /// 加工类型
        /// </summary>
        public WorkType workType;

        public Frm_AddFile(DataGridView view, object key, object fileId)
        {
            InitializeComponent();
            this.view = view;
            this.key = key;
            if(fileId != null)
            {
                Text = "编辑文件";
                this.fileId = fileId;
            }
            else
            {
                Text = "新增文件";
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
            //类型
            cbo_type.DataSource = DictionaryHelper.GetTableByCode("dic_file_type");
            cbo_type.DisplayMember = "dd_name";
            cbo_type.ValueMember = "dd_id";
            //密级
            cbo_secret.DataSource = DictionaryHelper.GetTableByCode("dic_file_mj");
            cbo_secret.DisplayMember = "dd_name";
            cbo_secret.ValueMember = "dd_id";
            //载体
            cbo_carrier.DataSource = DictionaryHelper.GetTableByCode("dic_file_zt");
            cbo_carrier.DisplayMember = "dd_name";
            cbo_carrier.ValueMember = "dd_id";
            //格式
            cbo_format.DataSource = DictionaryHelper.GetTableByCode("dic_file_format");
            cbo_format.DisplayMember = "dd_name";
            cbo_format.ValueMember = "dd_id";
            //形态
            cbo_form.DataSource = DictionaryHelper.GetTableByCode("dic_file_state");
            cbo_form.DisplayMember = "dd_name";
            cbo_form.ValueMember = "dd_id";
            //默认焦点
            cbo_stage.Focus();
            //编辑状态加载信息
            LoadFileInfo(fileId);

            if(workType == WorkType.PaperWork)
            {
                lbl_OpenFile.Enabled = false;
            }
        }

        private void LoadFileInfo(object fileId)
        {
            DataRow row = SqlHelper.ExecuteSingleRowQuery($"SELECT * FROM processing_file_list WHERE pfl_id='{fileId}'");
            if(row != null)
            {
                cbo_stage.SelectedValue = row["pfl_stage"];
                cbo_categor.SelectedValue = row["pfl_categor"];
                txt_fileName.Text = GetValue(row["pfl_filename"]);
                txt_user.Text = GetValue(row["pfl_user"]);
                cbo_type.SelectedValue = row["pfl_type"];
                cbo_secret.SelectedValue = row["pfl_scert"];
                num_page.Value = Convert.ToInt32(row["pfl_page_amount"]);
                num_amount.Value = Convert.ToInt32(row["pfl_amount"]);
                dtp_date.Value = Convert.ToDateTime(row["pfl_complete_date"]);
                txt_unit.Text = GetValue(row["pfl_save_location"]);
                cbo_carrier.SelectedValue = row["pfl_carrier"];
                cbo_format.SelectedValue = row["pfl_format"];
                cbo_form.SelectedValue = row["pfl_form"];
                txt_link.Text = GetValue(row["pfl_file_link"]);
                txt_remark.Text = GetValue(row["pfl_remark"]);
            }
        }

        /// <summary>
        /// 根据阶段加载类别
        /// </summary>
        private void LoadCategorByStage(object stageValue)
        {
            string querySql = $"SELECT dd_id, dd_name FROM data_dictionary WHERE dd_pId='{stageValue}' ORDER BY dd_sort";
            cbo_categor.DataSource = SqlHelper.ExecuteQuery(querySql);
            cbo_categor.DisplayMember = "dd_name";
            cbo_categor.ValueMember = "dd_id";
        }
        /// <summary>
        /// 根据类别加载文件名称
        /// </summary>
        private void LoadFileNameByCategor(object categorValue)
        {
            string value = GetValue(SqlHelper.ExecuteOnlyOneQuery($"SELECT dd_note FROM data_dictionary WHERE dd_id='{categorValue}'"));
            txt_fileName.Text = value;
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
                LoadFileNameByCategor(cbo_categor.SelectedValue);
        }

        /// <summary>
        /// 添加信息到指定表格
        /// </summary>
        private object SaveFileInfo(DataGridViewRow row, bool isAdd)
        {
            object primaryKey = Guid.NewGuid().ToString();
            row.Cells[key + "id"].Value = 0;
            row.Cells[key + "stage"].Value = cbo_stage.SelectedValue;
            SetCategorByStage(cbo_stage.SelectedValue, row, key);
            row.Cells[key + "categor"].Value = cbo_categor.SelectedValue;
            row.Cells[key + "name"].Value = txt_fileName.Text;
            row.Cells[key + "user"].Value = txt_user.Text;
            row.Cells[key + "type"].Value = cbo_type.SelectedValue;
            row.Cells[key + "secret"].Value = cbo_secret.SelectedValue;
            row.Cells[key + "page"].Value = num_page.Value;
            row.Cells[key + "amount"].Value = num_amount.Value;
            row.Cells[key + "date"].Value = dtp_date.Value.ToString("yyyyMMdd");
            row.Cells[key + "unit"].Value = txt_unit.Text;
            row.Cells[key + "carrier"].Value = cbo_carrier.SelectedValue;
            row.Cells[key + "format"].Value = cbo_format.SelectedValue;
            row.Cells[key + "form"].Value = cbo_form.SelectedValue;
            row.Cells[key + "link"].Value = txt_link.Text;
            row.Cells[key + "remark"].Value = txt_remark.Text;

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

            string sqlString = string.Empty;
            if(isAdd)
            {
                sqlString = $"INSERT INTO processing_file_list VALUES ('{primaryKey}' " +
                    $",'{stage}','{categor}' ,'{name}' ,'{user}' " +
                    $",'{type}' ,'{secret}' ,'{page}' ,'{amount}' ,'{date}' " +
                    $",'{unit}' ,'{carrier}','{format}' ,'{form}','{parentId}'" +
                    $",'{link}' ,'{remark}' ,{(int)GuiDangStatus.NonGuiDang} ,'{UserHelper.GetInstance().User.UserKey}','{DateTime.Now}')";
                row.Cells[key + "id"].Tag = primaryKey;
            }
            else
            {
                primaryKey = row.Cells[key + "id"].Tag;
                sqlString = "UPDATE processing_file_list SET " +
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
                $" WHERE pfl_id = '{primaryKey}'";
            }
            SqlHelper.ExecuteNonQuery(sqlString);

            return primaryKey;
        }
        
        private void Btn_Save_Click(object sender, EventArgs e)
        {
            string nameValue = txt_fileName.Text.Trim();
            if(string.IsNullOrEmpty(nameValue))
                MessageBox.Show("文件名不可为空。", "保存失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                if(Text.Contains("新增"))
                {
                    fileId = SaveFileInfo(view.Rows[view.Rows.Add()], true);
                    ResetControl();
                }
                else if(Text.Contains("编辑"))
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
            }
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
                if(!(item is Label))
                {
                    if(item is TextBox || item is DateTimePicker)
                        item.ResetText();
                    else if(item is NumericUpDown)
                        (item as NumericUpDown).Value = 0;
                    else if(item is ComboBox)
                        if(!item.Name.Equals("cbo_stage"))
                            (item as ComboBox).SelectedIndex = 0;
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

        private void lbl_OpenFile_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            object rootId = SqlHelper.ExecuteOnlyOneQuery($"SELECT bfi_id FROM backup_files_info WHERE bfi_trcid='{trcId}' AND bfi_sort=-1");
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
                    txt_link.Text = fullPath;
                    if(MessageBox.Show("已从服务器拷贝文件到本地，是否现在打开？", "操作确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        System.Diagnostics.Process.Start("EXPLORER.EXE", filePath);
                }
                else
                    MessageBox.Show("服务器不存在此文件。", "打开失败", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }
    }
}
