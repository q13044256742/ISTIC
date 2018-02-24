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
    public partial class Frm_template : Form
    {
        private string bm_id;

        public Frm_template(string name)
        {
            InitializeComponent();
            this.bm_id = name;
            LoadDataScoure(bm_id);
        }
      
        //初始化数据
        private void LoadDataScoure(string bm_id)
        {
            string sql = $"select t_title,t_content from temp where mb_manager_id = '{bm_id}'";
            object[] _obj = SqlHelper.ExecuteRowsQuery(sql);

            if (!string.IsNullOrEmpty(GetValue(_obj)))
            {
                temp_title.Text = GetValue(_obj[0]);
                temp_content.Text = GetValue(_obj[1]);
            }
        }
     
        //保存
        private void Btn_saveClick_temp(object sender, EventArgs e)
        {
            if (!ValidData())
            {
                return;
            }
            if (MessageBox.Show("确定要保存当前数据吗?", "确认提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk) == DialogResult.OK)
            {
                //删除old数据
                string querySql_del = $"delete from temp where mb_manager_id = '{bm_id}'";                  
                SqlHelper.ExecuteQuery(querySql_del);

                //保存基本信息
                string t_Id = Guid.NewGuid().ToString();
                string title = temp_title.Text.Trim();                         
                string content = temp_content.Text.Trim();  
          
                //新增信息                                        
                string querySql = $"insert into temp " +
                    $"(t_id,t_title,t_content,mb_manager_id)" +
                    $"values" +
                    $"('{t_Id}','{title}','{content}','{bm_id}')";
                SqlHelper.ExecuteQuery(querySql);
 
                MessageBox.Show("保存成功!");
            }
        }

        //校验
        private bool ValidData()
        {
            if (string.IsNullOrEmpty(temp_title.Text.Trim()))
            {
                MessageBox.Show("请输入标题", "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            return true;
        }

        //把object对象转换为string
        private string GetValue(object obj)
        {
            return obj == null ? string.Empty : obj.ToString();
        }      
    }
}
