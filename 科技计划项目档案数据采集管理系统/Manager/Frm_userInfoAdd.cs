using System;
using System.Data;
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
                LoadRole();
            }               
            else {
                LoadRole();
                LoadData(id);
            }             
        }

        //加载角色
        private void LoadRole()
        {
            string key = "dic_key_role";
            string querySql = $"SELECT * FROM data_dictionary WHERE dd_pId = (SELECT dd_id FROM data_dictionary WHERE dd_code='{key}')";
            DataTable table = SqlHelper.ExecuteQuery(querySql);
            role_select.DataSource = table;
            role_select.DisplayMember = "dd_name";
            role_select.ValueMember = "dd_id";
        }
      
        //加载更新表单
        private void LoadData(string id)
        {            
            string sql = $"select u.login_name,u.login_password,u.belong_unit,u.belong_department,u.real_name,u.email,u.telephone,u.cellphone,u.ip_address,u.remark,role_id" +
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
            ip_input.Text = _obj[8].ToString();                            
            note.Text = _obj[9].ToString();
            role_select.SelectedValue = _obj[10].ToString();
            login_name.Tag = id;
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
                string _belong_unit = belong_unit.Text.Trim();
                string _belong_bm = belong_bm.Text.Trim();
                string _mobile = mobile.Text.Trim();
                string _phone = phone.Text.Trim();
                string _mail = mail.Text.Trim();              
                string _role_id = role_select.SelectedValue.ToString();
                string _ip = ip_input.Text.Trim();
                string _note = note.Text.Trim();            
                string _real_name = real_name.Text.Trim();
            
                //新增信息
                if (isAdd)
                {
                    string _uId = Guid.NewGuid().ToString();
                    string querySql = $"insert into user_list " +
                        $"(ul_id,login_name,login_password,belong_unit,belong_department,real_name,role_id,email,telephone,cellphone,ip_address,remark)" +
                        $"values" +
                        $"('{_uId}','{_login_name}','{_password}','{_belong_unit}','{_belong_bm}','{_real_name}','{_role_id}','{_mail}','{_mobile}','{_phone}','{_ip}','{_note}')";
                    SqlHelper.ExecuteQuery(querySql);
                }
                //更新信息
                else
                {
                    string ul_id = login_name.Tag.ToString();
                    string querySql = $"update user_list set login_name='{_login_name}',login_password='{_password}',belong_unit='{_belong_unit}',belong_department='{_belong_bm}'," +
                        $" real_name='{_real_name}',role_id='{_role_id}',email='{_mail}',telephone='{_mobile}',cellphone='{_phone}',ip_address='{_ip}',remark='{_note}' where ul_id='{ul_id}'";
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
            else if (string.IsNullOrEmpty(belong_unit.Text.Trim()))
            {
                MessageBox.Show("请输入所属单位", "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            else if (string.IsNullOrEmpty(belong_bm.Text.Trim()))
            {
                MessageBox.Show("请输入所属部门", "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            else if (string.IsNullOrEmpty(role_select.SelectedValue.ToString()))
            {
                MessageBox.Show("请选择角色", "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        //ip校验
        bool IsIp()
        {                     
            if (!string.IsNullOrEmpty(ip_input.Text.Trim()))
            {               
                string _ip = ip_input.Text.Trim();                                          
                return Regex.IsMatch(_ip, "^(1\\d{2}|2[0-4]\\d|25[0-5]|[1-9]\\d|[1-9])\\." + "(1\\d{2}|2[0-4]\\d|25[0-5]|[1-9]\\d|\\d)\\." + "(1\\d{2}|2[0-4]\\d|25[0-5]|[1-9]\\d|\\d)\\." + "(1\\d{2}|2[0-4]\\d|25[0-5]|[1-9]\\d|\\d)" +"-"+ "(1\\d{2}|2[0-4]\\d|25[0-5]|[1-9]\\d|[1-9])\\."+ "(1\\d{2}|2[0-4]\\d|25[0-5]|[1-9]\\d|\\d)\\." + "(1\\d{2}|2[0-4]\\d|25[0-5]|[1-9]\\d|\\d)\\." + "(1\\d{2}|2[0-4]\\d|25[0-5]|[1-9]\\d|\\d)$") ||
                       Regex.IsMatch(_ip, "^(1\\d{2}|2[0-4]\\d|25[0-5]|[1-9]\\d|[1-9])\\." + "(1\\d{2}|2[0-4]\\d|25[0-5]|[1-9]\\d|\\d)\\." + "(1\\d{2}|2[0-4]\\d|25[0-5]|[1-9]\\d|\\d)\\." + "(1\\d{2}|2[0-4]\\d|25[0-5]|[1-9]\\d|\\d)" + "-" + "(1\\d{2}|2[0-4]\\d|25[0-5]|[1-9]\\d|[1-9])\\." + "(1\\d{2}|2[0-4]\\d|25[0-5]|[1-9]\\d|\\d)\\." + "(1\\d{2}|2[0-4]\\d|25[0-5]|[1-9]\\d|\\d)\\." + "(1\\d{2}|2[0-4]\\d|25[0-5]|[1-9]\\d|\\d)\\," + "(1\\d{2}|2[0-4]\\d|25[0-5]|[1-9]\\d|\\d)\\."+ "(1\\d{2}|2[0-4]\\d|25[0-5]|[1-9]\\d|\\d)\\."+ "(1\\d{2}|2[0-4]\\d|25[0-5]|[1-9]\\d|\\d)\\."+"(1\\d{2}|2[0-4]\\d|25[0-5]|[1-9]\\d|\\d)" + "-" + "(1\\d{2}|2[0-4]\\d|25[0-5]|[1-9]\\d|\\d)\\."+ "(1\\d{2}|2[0-4]\\d|25[0-5]|[1-9]\\d|\\d)\\."+ "(1\\d{2}|2[0-4]\\d|25[0-5]|[1-9]\\d|\\d)\\."+ "(1\\d{2}|2[0-4]\\d|25[0-5]|[1-9]\\d|\\d)$") ||
                       Regex.IsMatch(_ip, "^(1\\d{2}|2[0-4]\\d|25[0-5]|[1-9]\\d|[1-9])\\." + "(1\\d{2}|2[0-4]\\d|25[0-5]|[1-9]\\d|\\d)\\." + "(1\\d{2}|2[0-4]\\d|25[0-5]|[1-9]\\d|\\d)\\." + "(1\\d{2}|2[0-4]\\d|25[0-5]|[1-9]\\d|\\d)" + "-" + "(1\\d{2}|2[0-4]\\d|25[0-5]|[1-9]\\d|[1-9])\\." + "(1\\d{2}|2[0-4]\\d|25[0-5]|[1-9]\\d|\\d)\\." + "(1\\d{2}|2[0-4]\\d|25[0-5]|[1-9]\\d|\\d)\\." + "(1\\d{2}|2[0-4]\\d|25[0-5]|[1-9]\\d|\\d)\\," + "(1\\d{2}|2[0-4]\\d|25[0-5]|[1-9]\\d|\\d)\\." + "(1\\d{2}|2[0-4]\\d|25[0-5]|[1-9]\\d|\\d)\\." + "(1\\d{2}|2[0-4]\\d|25[0-5]|[1-9]\\d|\\d)\\." + "(1\\d{2}|2[0-4]\\d|25[0-5]|[1-9]\\d|\\d)" + "-" + "(1\\d{2}|2[0-4]\\d|25[0-5]|[1-9]\\d|\\d)\\." + "(1\\d{2}|2[0-4]\\d|25[0-5]|[1-9]\\d|\\d)\\." + "(1\\d{2}|2[0-4]\\d|25[0-5]|[1-9]\\d|\\d)\\." + "(1\\d{2}|2[0-4]\\d|25[0-5]|[1-9]\\d|\\d)\\," + "(1\\d{2}|2[0-4]\\d|25[0-5]|[1-9]\\d|\\d)\\." + "(1\\d{2}|2[0-4]\\d|25[0-5]|[1-9]\\d|\\d)\\." + "(1\\d{2}|2[0-4]\\d|25[0-5]|[1-9]\\d|\\d)\\." + "(1\\d{2}|2[0-4]\\d|25[0-5]|[1-9]\\d|\\d)" + "-" + "(1\\d{2}|2[0-4]\\d|25[0-5]|[1-9]\\d|\\d)\\." + "(1\\d{2}|2[0-4]\\d|25[0-5]|[1-9]\\d|\\d)\\." + "(1\\d{2}|2[0-4]\\d|25[0-5]|[1-9]\\d|\\d)\\."+"(1\\d{2}|2[0-4]\\d|25[0-5]|[1-9]\\d|\\d)$");
            }
            return true;
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
    }
}
