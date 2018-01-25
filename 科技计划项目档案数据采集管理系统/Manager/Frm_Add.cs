using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace 科技计划项目档案数据采集管理系统.Manager
{
    public partial class Frm_Add : Form
    {
        private bool isAdd;
        private string pId;
        private string id;


        public Frm_Add(bool isAdd, string pId,string id)
        {
            this.isAdd = isAdd;
            InitializeComponent();
            this.pId = pId;
            this.id = id;
            if (isAdd)
                Load_name(pId);
            else
                LoadData(pId,id);
        }


        private void Load_name(string pId)
        {
            string querySql = $"SELECT dd_name FROM data_dictionary where dd_id = '{pId}'";
            String dd_name = (String)SqlHelper.ExecuteOnlyOneQuery(querySql);

            //给文本框赋值
            txt_Search.Text = dd_name;
            txt_Search.Tag = pId;
        }

        private void LoadData(string pId,string id)
        {
            string querySql = $"SELECT dd_name FROM data_dictionary where dd_id = '{pId}'";
            string dataSql = $"SELECT dd_name,dd_code,dd_sort,dd_note FROM data_dictionary where dd_id = '{id}'";
            String dd_name = (String)SqlHelper.ExecuteOnlyOneQuery(querySql);
            object[] _obj = SqlHelper.ExecuteRowsQuery(dataSql);

            //给文本框赋值
            txt_Search.Text = dd_name;
            textBox1.Text = _obj[0].ToString();
            textBox2.Text = _obj[1].ToString();
            textBox3.Text = _obj[2].ToString();
            textBox4.Text = _obj[3].ToString();          
            txt_Search.Tag = pId;
            textBox1.Tag = id;
        }

        private void btn_save(object sender, EventArgs e)
        {
            if (!ValidData())
            {
                MessageBox.Show("请先将表单信息补充完整!");
                return;
            }
            if (MessageBox.Show("确定要保存当前数据吗?", "确认提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk) == DialogResult.OK)
            {
                //保存基本信息                     
                string dd_name = textBox1.Text.Trim();
                string dd_code = textBox2.Text.Trim();
                int dd_sort = (int)textBox3.Value;
                string dd_node = textBox4.Text.Trim();
                string dd_pId = (string)txt_Search.Tag;

                //新增信息
                if (isAdd)
                {
                    string dd_id = Guid.NewGuid().ToString();
                    string querySql = $"insert into data_dictionary  (dd_id, dd_name,dd_pId,dd_code,dd_note,dd_sort)values('{dd_id}','{dd_name}','{dd_pId}','{dd_code}','{dd_node}','{dd_sort}')";
                    SqlHelper.ExecuteQuery(querySql);
                }
                //更新信息
                else
                {
                    string dd_id = (string)textBox1.Tag;
                    string querySql = $"update data_dictionary set dd_name='{dd_name}',dd_code='{dd_code}',dd_sort='{dd_sort}',dd_note='{dd_node}' where dd_id='{dd_id}'";
                    SqlHelper.ExecuteQuery(querySql);
                }
                if (MessageBox.Show((isAdd ? "添加" : "更新") + "成功，是否返回列表页", "恭喜", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk) == DialogResult.Yes)
                {
                    DialogResult = DialogResult.OK;
                    Close();
                }
            }
        }

        /// <summary>
        /// 检验数据的完整性（名称和编码必填）
        /// </summary>
        private bool ValidData()
        {          
            if (string.IsNullOrEmpty(textBox1.Text.Trim()) || string.IsNullOrEmpty(textBox2.Text.Trim()))
                return false;
            return true;
        }

        private void Btn_close(object sender, EventArgs e)
        {
            Close();
        }
    }  
}
