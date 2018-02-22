using System;
using System.Windows.Forms;

namespace 科技计划项目档案数据采集管理系统
{
    public partial class Frm_AddFile : Form
    {
        private DataGridView view;
        private object key;
        public Frm_AddFile(DataGridView view, object key)
        {
            InitializeComponent();
            this.view = view;
            this.key = key;
        }

        private void Frm_AddFile_Load(object sender, System.EventArgs e)
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
        /// 打开文件
        /// </summary>
        private void Btn_OpenFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            { Multiselect = false };
            if(openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string path = openFileDialog.FileName;
                txt_link.Text = path;
                if(MessageBox.Show("是否要打开选定文件?", "温馨提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    System.Diagnostics.Process.Start("Explorer.exe", path);
            }
        }
        /// <summary>
        /// 新增
        /// </summary>
        private void Btn_Sure_Click(object sender, EventArgs e)
        {
            string nameValue = txt_fileName.Text.Trim();
            if(string.IsNullOrEmpty(nameValue))
                MessageBox.Show("文件名不可为空。", "保存失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                SaveFileInfo();
                Close();
            }
        }
        /// <summary>
        /// 添加信息到指定表格
        /// </summary>
        private void SaveFileInfo()
        {
            int index = view.Rows.Add();
            view.Rows[index].Cells[key + "stage"].Value = cbo_stage.SelectedValue;
            SetCategorByStage(cbo_stage.SelectedValue, view.Rows[index], key);
            view.Rows[index].Cells[key + "categor"].Value = cbo_categor.SelectedValue;
            view.Rows[index].Cells[key + "name"].Value = txt_fileName.Text;
            view.Rows[index].Cells[key + "user"].Value = txt_user.Text;
            view.Rows[index].Cells[key + "type"].Value = cbo_type.SelectedValue;
            view.Rows[index].Cells[key + "secret"].Value = cbo_secret.SelectedValue;
            view.Rows[index].Cells[key + "page"].Value = num_page.Value;
            view.Rows[index].Cells[key + "amount"].Value = num_amount.Value;
            view.Rows[index].Cells[key + "date"].Value = dtp_date.Value.ToString("yyyyMMdd");
            view.Rows[index].Cells[key + "unit"].Value = txt_unit.Text;
            view.Rows[index].Cells[key + "carrier"].Value = cbo_carrier.SelectedValue;
            view.Rows[index].Cells[key + "format"].Value = cbo_format.SelectedValue;
            view.Rows[index].Cells[key + "form"].Value = cbo_form.SelectedValue;
            view.Rows[index].Cells[key + "link"].Value = txt_link.Text;
            view.Rows[index].Cells[key + "remark"].Value = txt_remark.Text;
        }
        /// <summary>
        /// 保存-新增
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Save_Add_Click(object sender, EventArgs e)
        {
            string nameValue = txt_fileName.Text.Trim();
            if(string.IsNullOrEmpty(nameValue))
                MessageBox.Show("文件名不可为空。", "保存失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                SaveFileInfo();
                ResetControl();
                cbo_stage.Focus();
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
            foreach(Control item in gro_BasicInfo.Controls)
            {
                if(!(item is Label))
                {
                    if(item is TextBox || item is DateTimePicker)
                        item.ResetText();
                    else if(item is NumericUpDown)
                        (item as NumericUpDown).Value = 0;
                    else if(item is ComboBox)
                        (item as ComboBox).SelectedIndex = 0;
                }
            }
        }

        private void Frm_AddFile_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Control && e.KeyCode == Keys.Enter)
            {
                Btn_Save_Add_Click(null, null);
            }
        }
    }
}
