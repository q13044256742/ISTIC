using System;
using System.Data;
using System.Windows.Forms;

namespace 科技计划项目档案数据采集管理系统.Manager
{
    public partial class Frm_Add : DevExpress.XtraEditors.XtraForm
    {
        private bool isAdd;
        private string pId;
        private object id;

        public Frm_Add(bool isAdd, string pId, object id, int sort)
        {
            this.isAdd = isAdd;
            InitializeComponent();
            this.pId = pId;
            this.id = id;
            if(isAdd)
                Load_BasicInfo(pId, sort);
            else
                LoadData(pId, id);
        }


        private void Load_BasicInfo(string pId, int sort)
        {
            string querySql = $"SELECT dd_name FROM data_dictionary where dd_id = '{pId}'";
            string dd_name = GetValue(SqlHelper.ExecuteOnlyOneQuery(querySql));

            //给文本框赋值
            txt_ParentName.Text = dd_name;
            txt_ParentName.Tag = pId;
            num_Sort.Value = sort;
        }

        private string GetValue(object value) => value == null ? string.Empty : value.ToString();

        private void LoadData(object pId, object id)
        {
            object pName = SqlHelper.ExecuteOnlyOneQuery($"SELECT dd_name FROM data_dictionary WHERE dd_id='{pId}'");
            if(pName != null)
            {
                DataRow row = SqlHelper.ExecuteSingleRowQuery($"SELECT dd_name, dd_code, dd_sort, dd_note FROM data_dictionary where dd_id = '{id}'");
                //给文本框赋值
                txt_ParentName.Text = GetValue(pName);
                txt_ParentName.Tag = pId;

                txt_Name.Text = GetValue(row["dd_name"]);
                txt_Code.Text = GetValue(row["dd_code"]);
                num_Sort.Text = GetValue(row["dd_sort"]);
                txt_Intro.Text = GetValue(row["dd_note"]);
                txt_Name.Tag = GetValue(id);
            }
        }

        private void Btn_Save(object sender, EventArgs e)
        {
            if(ValidData())
            {
                if(MessageBox.Show("确定要保存当前数据吗?", "确认提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk) == DialogResult.OK)
                {
                    //保存基本信息                     
                    string name = txt_Name.Text.Trim();
                    string code = txt_Code.Text;
                    int _sort = (int)num_Sort.Value;
                    string note = txt_Intro.Text;
                    object pid = txt_ParentName.Tag;

                    //新增信息
                    if(isAdd)
                    {
                        string dd_id = Guid.NewGuid().ToString();
                        string querySql = $"INSERT INTO data_dictionary (dd_id, dd_name, dd_pId, dd_code, dd_note, dd_sort) VALUES " +
                            $"('{dd_id}', '{name}', '{pid}', '{code}', '{note}', '{_sort}')";
                        SqlHelper.ExecuteQuery(querySql);
                    }
                    //更新信息
                    else
                    {
                        string dd_id = (string)txt_Name.Tag;
                        string querySql = $"UPDATE data_dictionary SET dd_name='{name}', dd_code='{code}', dd_sort='{_sort}',dd_note='{note}' WHERE dd_id='{dd_id}'";
                        SqlHelper.ExecuteQuery(querySql);
                    }
                    if(MessageBox.Show((isAdd ? "添加" : "更新") + "成功，是否返回列表页", "温馨提示", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk) == DialogResult.Yes)
                    {
                        DialogResult = DialogResult.OK;
                        Close();
                    }
                }
            }
        }

        private bool ValidData()
        {
            bool flag = true;
            TextBox pNameBox = txt_ParentName;
            if(string.IsNullOrEmpty(pNameBox.Text.Trim()))
            {
                errorProvider1.SetError(pNameBox, "提示：父节点名称不能为空。");
                flag = false;
            }
            else
                errorProvider1.SetError(pNameBox, null);

            TextBox nameBox = txt_Name;
            if(string.IsNullOrEmpty(nameBox.Text.Trim()))
            {
                errorProvider1.SetError(nameBox, "提示：节点名称不能为空。");
                flag = false;
            }
            else
                errorProvider1.SetError(nameBox, null);
            return flag;
        }

        private void Close_Form(object sender, EventArgs e)
        {
            Close();
        }

    }  
}
