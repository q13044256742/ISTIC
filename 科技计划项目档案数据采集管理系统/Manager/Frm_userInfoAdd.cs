using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace 科技计划项目档案数据采集管理系统.Manager
{
    public partial class Frm_userInfoAdd : Form
    {
        private bool isAdd;
        private string id;
      
        public Frm_userInfoAdd(bool isAdd, string id)
        {
            InitializeComponent();
            this.isAdd = isAdd;
            this.id = id;

            if (isAdd) {
                Load_role();
                LoadUserGroup();
            }               
            else {
                Load_role();
                LoadUserGroup();
                LoadData(id);
            }
                
        }

        //加载用户组
        private void LoadUserGroup()
        {
            string sql = $"select ug_id,ug_name from user_group order by ug_sort";
            DataTable table = SqlHelper.ExecuteQuery(sql);
            //belong_userGroup.DataSource = table;
            //belong_userGroup.DisplayMember = "ug_name";
            //belong_userGroup.ValueMember = "ug_id";
        }

        //加载更新表单
        private void LoadData(string id)
        {            
            string sql = $"select u.login_name,u.login_password,u.belong_unit,u.belong_department,u.real_name,u.email,u.telephone,u.cellphone,u.ip_address,u.remark,u.role_id,u.belong_user_group_id" +
                $" from user_list u where ul_id = '{id}'";
            object[] _obj = SqlHelper.ExecuteRowsQuery(sql);

            login_name.Text = _obj[0].ToString();
            password.Text = _obj[1].ToString();
            real_password.Text = _obj[1].ToString();
            belong_unit.Text = _obj[2].ToString();
            belong_bm.Text = _obj[3].ToString();
            real_name.Text = _obj[4].ToString();
            mail.Text = _obj[5].ToString();
            mobile.Text = _obj[6].ToString();
            phone.Text = _obj[7].ToString();                 
            string ip = _obj[8].ToString();
            string[] array = ip.Split('.');
            if (array.Length != 0) {
                ip_1.Text = array[0];
                ip_2.Text = array[1];
                ip_3.Text = array[2];
                ip_4.Text = array[3];
                ip_5.Text = array[4];
                ip_6.Text = array[5];
                ip_7.Text = array[6];
                ip_8.Text = array[7];
            }
          
            note.Text = _obj[9].ToString();
            role_box.SelectedValue = _obj[10].ToString();
            belong_userGroup.Text = _obj[11].ToString();

            login_name.Tag = id;
        }

        //加载角色
        private void Load_role()
        {
            string sql = $"select r_name,r_id from role";
            DataTable table = SqlHelper.ExecuteQuery(sql);
            role_box.DataSource = table;
            role_box.DisplayMember = "r_name";
            role_box.ValueMember = "r_id";
        }

        //保存
        private void U_btnSave(object sender, EventArgs e)
        {
            if (!ValidData())
            {              
                return;
            }
            if (MessageBox.Show("确定要保存当前数据吗?", "确认提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk) == DialogResult.OK)
            {
                //保存基本信息
                string _login_name = login_name.Text.Trim();
                string _password = password.Text.Trim();
                string r_id = role_box.SelectedValue.ToString().Trim();
                string _belong_unit = belong_unit.Text.Trim();
                string _belong_bm = belong_bm.Text.Trim();
                string _mobile = mobile.Text.Trim();
                string _phone = phone.Text.Trim();
                string _mail = mail.Text.Trim();
                string _ip_1 = ip_1.Text.Trim();
                string _ip_2 = ip_2.Text.Trim();
                string _ip_3 = ip_3.Text.Trim();
                string _ip_4 = ip_4.Text.Trim();
                string _ip_5 = ip_5.Text.Trim();
                string _ip_6 = ip_6.Text.Trim();
                string _ip_7 = ip_7.Text.Trim();
                string _ip_8 = ip_8.Text.Trim();
                string _ip = _ip_1 + '.' + _ip_2 + '.' + _ip_3 + '.' + _ip_4 + '.' + _ip_5 + '.' + _ip_6 + '.' + _ip_7 + '.' + _ip_8;              
                string _note = note.Text.Trim();            
                string _real_name = real_name.Text.Trim();
                string _belong_userGroup_id = belong_userGroup.Text.Trim();

                //新增信息
                if (isAdd)
                {
                    string _uId = Guid.NewGuid().ToString();
                    string querySql = $"insert into user_list " +
                        $"(ul_id,login_name,login_password,belong_unit,belong_department,real_name,email,telephone,cellphone,ip_address,remark,role_id,belong_user_group_id)" +
                        $"values" +
                        $"('{_uId}','{_login_name}','{_password}','{_belong_unit}','{_belong_bm}','{_real_name}','{_mail}','{_mobile}','{_phone}','{_ip}','{_note}','{r_id}','{_belong_userGroup_id}')";
                    SqlHelper.ExecuteQuery(querySql);
                }
                //更新信息
                else
                {
                    string ul_id = login_name.Tag.ToString();
                    string querySql = $"update user_list set login_name='{_login_name}',login_password='{_password}',belong_unit='{_belong_unit}',belong_department='{_belong_bm}'," +
                        $" real_name='{_real_name}',email='{_mail}',telephone='{_mobile}',cellphone='{_phone}',ip_address='{_ip}',remark='{_note}',role_id='{r_id}',belong_user_group_id='{_belong_userGroup_id}' where ul_id='{ul_id}'";
                    SqlHelper.ExecuteQuery(querySql);
                }
                if (MessageBox.Show((isAdd ? "添加" : "更新") + "成功，是否返回列表页", "恭喜", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk) == DialogResult.Yes)
                {
                    DialogResult = DialogResult.OK;
                    Close();
                }
            }
         
        }
      
        // 检验数据的完整性
        private bool ValidData()
        {
            if (string.IsNullOrEmpty(login_name.Text.Trim())) {
                MessageBox.Show("请输入登录名", "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            else if (string.IsNullOrEmpty(password.Text.Trim())) {
                MessageBox.Show("请输入密码", "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            else if (string.IsNullOrEmpty(real_password.Text.Trim()))
            {
                MessageBox.Show("请再次输入密码", "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            else if (string.IsNullOrEmpty(belong_unit.Text.Trim()))
            {
                MessageBox.Show("请输入所属单位", "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            else if (string.IsNullOrEmpty(belong_bm.Text.Trim()))
            {
                MessageBox.Show("请输入部门", "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            else if (password.Text.Trim() != real_password.Text.Trim())
            {
                MessageBox.Show("密码输入不一致", "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            else if (string.IsNullOrEmpty(real_name.Text.Trim()))
            {
                MessageBox.Show("请输入姓名", "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            else if (!IsMobile())
            {
                MessageBox.Show("请输入有效手机号", "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            else if (!IsPhone())
            {
                MessageBox.Show("请输入有效电话号", "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            else if (!IsEmail())
            {
                MessageBox.Show("请输入正确邮箱", "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            else if (!IsIp())
            {
                MessageBox.Show("请输入正确IP", "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            return true;
        }

        //邮件校验
        bool IsEmail()
        {
            if (!string.IsNullOrEmpty(mail.Text.Trim())) {
                return Regex.IsMatch(mail.Text.Trim(), "^[a-zA-Z0-9_-]+@[a-zA-Z0-9_-]+\\.[a-zA-Z0-9_-]+$");
            }
            return true;
        }

        //手机号校验
        bool IsMobile() {
            if (!string.IsNullOrEmpty(mobile.Text.Trim())) {
                if (mobile.Text.Trim().Length == 11)
                {
                    return Regex.IsMatch(mobile.Text.Trim(), @"^[1]+[3,5]+\d{9}");
                }
                else {
                    return false;
                }           
            }
            return true;   
        }

        //电话号校验
        bool IsPhone()
        {
            if (!string.IsNullOrEmpty(phone.Text.Trim()))
            {
                if (phone.Text.Trim().Length == 11)
                {
                    return Regex.IsMatch(phone.Text.Trim(), @"(\d{11})|^((\d{7,8})|(\d{4}|\d{3})(\d{7,8})|(\d{4}|\d{3})(\d{7,8})(\d{4}|\d{3}|\d{2}|\d{1})|(\d{7,8})(\d{4}|\d{3}|\d{2}|\d{1}))$");
                }
                else
                {
                    return false;
                }
            }
            return true;
        }

        bool IsIp()
        {                     
            if (!string.IsNullOrEmpty(ip_1.Text.Trim()) && !string.IsNullOrEmpty(ip_2.Text.Trim()) && !string.IsNullOrEmpty(ip_3.Text.Trim()) && !string.IsNullOrEmpty(ip_4.Text.Trim()) 
                && !string.IsNullOrEmpty(ip_5.Text.Trim()) && !string.IsNullOrEmpty(ip_6.Text.Trim()) && !string.IsNullOrEmpty(ip_7.Text.Trim()) && !string.IsNullOrEmpty(ip_8.Text.Trim()))
            {
                string _ip_1 = ip_1.Text.Trim();
                string _ip_2 = ip_2.Text.Trim();
                string _ip_3 = ip_3.Text.Trim();
                string _ip_4 = ip_4.Text.Trim();
                string _ip_5 = ip_5.Text.Trim();
                string _ip_6 = ip_6.Text.Trim();
                string _ip_7 = ip_7.Text.Trim();
                string _ip_8 = ip_8.Text.Trim();
                string _ip = _ip_1 + '.' + _ip_2 + '.' + _ip_3 + '.' + _ip_4 +'.'+ _ip_5 + '.' + _ip_6 + '.' + _ip_7 + '.' + _ip_8;
                              
                return Regex.IsMatch(_ip, "^(1\\d{2}|2[0-4]\\d|25[0-5]|[1-9]\\d|[1-9])\\." + "(1\\d{2}|2[0-4]\\d|25[0-5]|[1-9]\\d|\\d)\\." + "(1\\d{2}|2[0-4]\\d|25[0-5]|[1-9]\\d|\\d)\\." + "(1\\d{2}|2[0-4]\\d|25[0-5]|[1-9]\\d|\\d)\\." + "(1\\d{2}|2[0-4]\\d|25[0-5]|[1-9]\\d|[1-9])\\."+ "(1\\d{2}|2[0-4]\\d|25[0-5]|[1-9]\\d|\\d)\\." + "(1\\d{2}|2[0-4]\\d|25[0-5]|[1-9]\\d|\\d)\\." + "(1\\d{2}|2[0-4]\\d|25[0-5]|[1-9]\\d|\\d)$");
            }
            return false;
        }

        //关闭
        private void U_btnClose(object sender, EventArgs e)
        {
            Close();
        }

       //判断输入的是否为数字
        private void Phone_keyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar >= '0' && e.KeyChar <= '9')
            {
                e.Handled = false;
            }
            else if (e.KeyChar == '\b')
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void Btn_Select_Click(object sender, EventArgs e)
        {
            Frm_UserGroupSelect frm = new Frm_UserGroupSelect(id);
            frm.ShowDialog();
        }
    }
}
