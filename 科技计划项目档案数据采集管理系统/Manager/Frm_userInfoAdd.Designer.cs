namespace 科技计划项目档案数据采集管理系统.Manager
{
    partial class Frm_userInfoAdd
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.btn_Save = new System.Windows.Forms.Button();
            this.note = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.belong_bm = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.phone = new System.Windows.Forms.TextBox();
            this.role_box = new System.Windows.Forms.ComboBox();
            this.login_name = new System.Windows.Forms.TextBox();
            this.password = new System.Windows.Forms.TextBox();
            this.real_password = new System.Windows.Forms.TextBox();
            this.belong_unit = new System.Windows.Forms.TextBox();
            this.mobile = new System.Windows.Forms.TextBox();
            this.mail = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.real_name = new System.Windows.Forms.TextBox();
            this.ip_4 = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.ip_2 = new System.Windows.Forms.TextBox();
            this.ip_3 = new System.Windows.Forms.TextBox();
            this.ip_1 = new System.Windows.Forms.TextBox();
            this.ip_5 = new System.Windows.Forms.TextBox();
            this.ip_7 = new System.Windows.Forms.TextBox();
            this.ip_6 = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.ip_8 = new System.Windows.Forms.TextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.btn_Select = new System.Windows.Forms.Button();
            this.belong_userGroup = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(719, 486);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(76, 29);
            this.button1.TabIndex = 35;
            this.button1.Text = "取消(&C)";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.U_btnClose);
            // 
            // btn_Save
            // 
            this.btn_Save.Location = new System.Drawing.Point(637, 486);
            this.btn_Save.Name = "btn_Save";
            this.btn_Save.Size = new System.Drawing.Size(76, 29);
            this.btn_Save.TabIndex = 34;
            this.btn_Save.Text = "保存(&S)";
            this.btn_Save.UseVisualStyleBackColor = true;
            this.btn_Save.Click += new System.EventHandler(this.U_btnSave);
            // 
            // note
            // 
            this.note.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.note.Location = new System.Drawing.Point(136, 390);
            this.note.Multiline = true;
            this.note.Name = "note";
            this.note.Size = new System.Drawing.Size(659, 60);
            this.note.TabIndex = 33;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 11F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(63, 52);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 19);
            this.label1.TabIndex = 25;
            this.label1.Text = "登录名：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 11F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(466, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 19);
            this.label2.TabIndex = 37;
            this.label2.Text = "密码：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 11F, System.Drawing.FontStyle.Bold);
            this.label3.Location = new System.Drawing.Point(49, 95);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(84, 19);
            this.label3.TabIndex = 38;
            this.label3.Text = "确认密码：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("微软雅黑", 11F, System.Drawing.FontStyle.Bold);
            this.label4.Location = new System.Drawing.Point(49, 139);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(84, 19);
            this.label4.TabIndex = 39;
            this.label4.Text = "所属单位：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("微软雅黑", 11F, System.Drawing.FontStyle.Bold);
            this.label5.Location = new System.Drawing.Point(438, 140);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(84, 19);
            this.label5.TabIndex = 40;
            this.label5.Text = "所属部门：";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("微软雅黑", 11F, System.Drawing.FontStyle.Bold);
            this.label6.Location = new System.Drawing.Point(79, 185);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(54, 19);
            this.label6.TabIndex = 41;
            this.label6.Text = "手机：";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("微软雅黑", 11F, System.Drawing.FontStyle.Bold);
            this.label7.Location = new System.Drawing.Point(79, 234);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(54, 19);
            this.label7.TabIndex = 42;
            this.label7.Text = "邮箱：";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("微软雅黑", 11F, System.Drawing.FontStyle.Bold);
            this.label8.Location = new System.Drawing.Point(64, 286);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(69, 19);
            this.label8.TabIndex = 43;
            this.label8.Text = "IP地址：";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("微软雅黑", 11F, System.Drawing.FontStyle.Bold);
            this.label9.Location = new System.Drawing.Point(35, 338);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(99, 19);
            this.label9.TabIndex = 44;
            this.label9.Text = "所属用户组：";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("微软雅黑", 11F, System.Drawing.FontStyle.Bold);
            this.label10.Location = new System.Drawing.Point(78, 403);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(54, 19);
            this.label10.TabIndex = 45;
            this.label10.Text = "备注：";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("微软雅黑", 11F, System.Drawing.FontStyle.Bold);
            this.label13.Location = new System.Drawing.Point(466, 94);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(54, 19);
            this.label13.TabIndex = 49;
            this.label13.Text = "角色：";
            // 
            // belong_bm
            // 
            this.belong_bm.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.belong_bm.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.belong_bm.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.belong_bm.Location = new System.Drawing.Point(525, 136);
            this.belong_bm.Name = "belong_bm";
            this.belong_bm.Size = new System.Drawing.Size(271, 26);
            this.belong_bm.TabIndex = 51;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("微软雅黑", 11F, System.Drawing.FontStyle.Bold);
            this.label14.Location = new System.Drawing.Point(467, 186);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(54, 19);
            this.label14.TabIndex = 54;
            this.label14.Text = "电话：";
            // 
            // phone
            // 
            this.phone.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.phone.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.phone.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.phone.Location = new System.Drawing.Point(524, 182);
            this.phone.MaxLength = 11;
            this.phone.Name = "phone";
            this.phone.Size = new System.Drawing.Size(271, 26);
            this.phone.TabIndex = 55;
            this.phone.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Phone_keyPress);
            // 
            // role_box
            // 
            this.role_box.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.role_box.Font = new System.Drawing.Font("黑体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.role_box.FormattingEnabled = true;
            this.role_box.Location = new System.Drawing.Point(523, 92);
            this.role_box.Name = "role_box";
            this.role_box.Size = new System.Drawing.Size(271, 24);
            this.role_box.TabIndex = 59;
            // 
            // login_name
            // 
            this.login_name.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.login_name.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.login_name.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.login_name.Location = new System.Drawing.Point(135, 49);
            this.login_name.Name = "login_name";
            this.login_name.Size = new System.Drawing.Size(271, 26);
            this.login_name.TabIndex = 60;
            // 
            // password
            // 
            this.password.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.password.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.password.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.password.Location = new System.Drawing.Point(523, 48);
            this.password.Name = "password";
            this.password.PasswordChar = '*';
            this.password.Size = new System.Drawing.Size(271, 26);
            this.password.TabIndex = 61;
            // 
            // real_password
            // 
            this.real_password.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.real_password.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.real_password.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.real_password.Location = new System.Drawing.Point(136, 92);
            this.real_password.Name = "real_password";
            this.real_password.PasswordChar = '*';
            this.real_password.Size = new System.Drawing.Size(271, 26);
            this.real_password.TabIndex = 62;
            // 
            // belong_unit
            // 
            this.belong_unit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.belong_unit.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.belong_unit.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.belong_unit.Location = new System.Drawing.Point(136, 136);
            this.belong_unit.Name = "belong_unit";
            this.belong_unit.Size = new System.Drawing.Size(271, 26);
            this.belong_unit.TabIndex = 63;
            // 
            // mobile
            // 
            this.mobile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.mobile.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.mobile.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.mobile.Location = new System.Drawing.Point(136, 182);
            this.mobile.MaxLength = 11;
            this.mobile.Name = "mobile";
            this.mobile.Size = new System.Drawing.Size(271, 26);
            this.mobile.TabIndex = 64;
            this.mobile.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Phone_keyPress);
            // 
            // mail
            // 
            this.mail.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.mail.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.mail.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.mail.Location = new System.Drawing.Point(136, 230);
            this.mail.Name = "mail";
            this.mail.Size = new System.Drawing.Size(271, 26);
            this.mail.TabIndex = 65;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("微软雅黑", 11F, System.Drawing.FontStyle.Bold);
            this.label11.Location = new System.Drawing.Point(437, 234);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(84, 19);
            this.label11.TabIndex = 66;
            this.label11.Text = "真实姓名：";
            // 
            // real_name
            // 
            this.real_name.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.real_name.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.real_name.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.real_name.Location = new System.Drawing.Point(524, 230);
            this.real_name.Name = "real_name";
            this.real_name.Size = new System.Drawing.Size(271, 26);
            this.real_name.TabIndex = 67;
            // 
            // ip_4
            // 
            this.ip_4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ip_4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ip_4.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ip_4.Location = new System.Drawing.Point(352, 283);
            this.ip_4.MaxLength = 3;
            this.ip_4.Name = "ip_4";
            this.ip_4.Size = new System.Drawing.Size(55, 26);
            this.ip_4.TabIndex = 71;
            this.ip_4.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Phone_keyPress);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("微软雅黑", 11F, System.Drawing.FontStyle.Bold);
            this.label12.Location = new System.Drawing.Point(192, 282);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(13, 19);
            this.label12.TabIndex = 72;
            this.label12.Text = ".";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("微软雅黑", 11F, System.Drawing.FontStyle.Bold);
            this.label15.Location = new System.Drawing.Point(336, 282);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(13, 19);
            this.label15.TabIndex = 73;
            this.label15.Text = ".";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("微软雅黑", 11F, System.Drawing.FontStyle.Bold);
            this.label16.Location = new System.Drawing.Point(264, 282);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(13, 19);
            this.label16.TabIndex = 74;
            this.label16.Text = ".";
            // 
            // ip_2
            // 
            this.ip_2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ip_2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ip_2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ip_2.Location = new System.Drawing.Point(208, 283);
            this.ip_2.MaxLength = 3;
            this.ip_2.Name = "ip_2";
            this.ip_2.Size = new System.Drawing.Size(53, 26);
            this.ip_2.TabIndex = 75;
            this.ip_2.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Phone_keyPress);
            // 
            // ip_3
            // 
            this.ip_3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ip_3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ip_3.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ip_3.Location = new System.Drawing.Point(280, 283);
            this.ip_3.MaxLength = 3;
            this.ip_3.Name = "ip_3";
            this.ip_3.Size = new System.Drawing.Size(53, 26);
            this.ip_3.TabIndex = 76;
            this.ip_3.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Phone_keyPress);
            // 
            // ip_1
            // 
            this.ip_1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ip_1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ip_1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ip_1.Location = new System.Drawing.Point(136, 283);
            this.ip_1.MaxLength = 3;
            this.ip_1.Name = "ip_1";
            this.ip_1.Size = new System.Drawing.Size(53, 26);
            this.ip_1.TabIndex = 77;
            this.ip_1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Phone_keyPress);
            // 
            // ip_5
            // 
            this.ip_5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ip_5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ip_5.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ip_5.Location = new System.Drawing.Point(525, 283);
            this.ip_5.MaxLength = 3;
            this.ip_5.Name = "ip_5";
            this.ip_5.Size = new System.Drawing.Size(53, 26);
            this.ip_5.TabIndex = 84;
            // 
            // ip_7
            // 
            this.ip_7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ip_7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ip_7.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ip_7.Location = new System.Drawing.Point(669, 283);
            this.ip_7.MaxLength = 3;
            this.ip_7.Name = "ip_7";
            this.ip_7.Size = new System.Drawing.Size(53, 26);
            this.ip_7.TabIndex = 83;
            // 
            // ip_6
            // 
            this.ip_6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ip_6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ip_6.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ip_6.Location = new System.Drawing.Point(597, 283);
            this.ip_6.MaxLength = 3;
            this.ip_6.Name = "ip_6";
            this.ip_6.Size = new System.Drawing.Size(53, 26);
            this.ip_6.TabIndex = 82;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("微软雅黑", 11F, System.Drawing.FontStyle.Bold);
            this.label17.Location = new System.Drawing.Point(653, 282);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(13, 19);
            this.label17.TabIndex = 81;
            this.label17.Text = ".";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("微软雅黑", 11F, System.Drawing.FontStyle.Bold);
            this.label18.Location = new System.Drawing.Point(725, 282);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(13, 19);
            this.label18.TabIndex = 80;
            this.label18.Text = ".";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("微软雅黑", 11F, System.Drawing.FontStyle.Bold);
            this.label19.Location = new System.Drawing.Point(581, 282);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(13, 19);
            this.label19.TabIndex = 79;
            this.label19.Text = ".";
            // 
            // ip_8
            // 
            this.ip_8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ip_8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ip_8.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ip_8.Location = new System.Drawing.Point(741, 283);
            this.ip_8.MaxLength = 3;
            this.ip_8.Name = "ip_8";
            this.ip_8.Size = new System.Drawing.Size(53, 26);
            this.ip_8.TabIndex = 78;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("微软雅黑", 11F, System.Drawing.FontStyle.Bold);
            this.label20.Location = new System.Drawing.Point(457, 287);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(24, 19);
            this.label20.TabIndex = 85;
            this.label20.Text = "至";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Font = new System.Drawing.Font("微软雅黑", 11F, System.Drawing.FontStyle.Bold);
            this.label21.ForeColor = System.Drawing.Color.Red;
            this.label21.Location = new System.Drawing.Point(409, 53);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(16, 19);
            this.label21.TabIndex = 110;
            this.label21.Text = "*";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Font = new System.Drawing.Font("微软雅黑", 11F, System.Drawing.FontStyle.Bold);
            this.label22.ForeColor = System.Drawing.Color.Red;
            this.label22.Location = new System.Drawing.Point(797, 52);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(16, 19);
            this.label22.TabIndex = 111;
            this.label22.Text = "*";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Font = new System.Drawing.Font("微软雅黑", 11F, System.Drawing.FontStyle.Bold);
            this.label23.ForeColor = System.Drawing.Color.Red;
            this.label23.Location = new System.Drawing.Point(410, 96);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(16, 19);
            this.label23.TabIndex = 112;
            this.label23.Text = "*";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Font = new System.Drawing.Font("微软雅黑", 11F, System.Drawing.FontStyle.Bold);
            this.label24.ForeColor = System.Drawing.Color.Red;
            this.label24.Location = new System.Drawing.Point(797, 95);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(16, 19);
            this.label24.TabIndex = 113;
            this.label24.Text = "*";
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Font = new System.Drawing.Font("微软雅黑", 11F, System.Drawing.FontStyle.Bold);
            this.label25.ForeColor = System.Drawing.Color.Red;
            this.label25.Location = new System.Drawing.Point(798, 234);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(16, 19);
            this.label25.TabIndex = 114;
            this.label25.Text = "*";
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Font = new System.Drawing.Font("微软雅黑", 11F, System.Drawing.FontStyle.Bold);
            this.label26.ForeColor = System.Drawing.Color.Red;
            this.label26.Location = new System.Drawing.Point(797, 287);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(16, 19);
            this.label26.TabIndex = 115;
            this.label26.Text = "*";
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Font = new System.Drawing.Font("微软雅黑", 11F, System.Drawing.FontStyle.Bold);
            this.label27.ForeColor = System.Drawing.Color.Red;
            this.label27.Location = new System.Drawing.Point(796, 339);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(16, 19);
            this.label27.TabIndex = 116;
            this.label27.Text = "*";
            // 
            // btn_Select
            // 
            this.btn_Select.Location = new System.Drawing.Point(719, 334);
            this.btn_Select.Name = "btn_Select";
            this.btn_Select.Size = new System.Drawing.Size(75, 28);
            this.btn_Select.TabIndex = 117;
            this.btn_Select.Text = "选择";
            this.btn_Select.UseVisualStyleBackColor = true;
            this.btn_Select.Click += new System.EventHandler(this.btn_Select_Click);
            // 
            // belong_userGroup
            // 
            this.belong_userGroup.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.belong_userGroup.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.belong_userGroup.Enabled = false;
            this.belong_userGroup.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.belong_userGroup.Location = new System.Drawing.Point(137, 335);
            this.belong_userGroup.Name = "belong_userGroup";
            this.belong_userGroup.Size = new System.Drawing.Size(577, 26);
            this.belong_userGroup.TabIndex = 118;
            // 
            // Frm_userInfoAdd
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(857, 563);
            this.Controls.Add(this.belong_userGroup);
            this.Controls.Add(this.btn_Select);
            this.Controls.Add(this.label27);
            this.Controls.Add(this.label26);
            this.Controls.Add(this.label25);
            this.Controls.Add(this.label24);
            this.Controls.Add(this.label23);
            this.Controls.Add(this.label22);
            this.Controls.Add(this.label21);
            this.Controls.Add(this.label20);
            this.Controls.Add(this.ip_5);
            this.Controls.Add(this.ip_7);
            this.Controls.Add(this.ip_6);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.label19);
            this.Controls.Add(this.ip_8);
            this.Controls.Add(this.ip_1);
            this.Controls.Add(this.ip_3);
            this.Controls.Add(this.ip_2);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.ip_4);
            this.Controls.Add(this.real_name);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.mail);
            this.Controls.Add(this.mobile);
            this.Controls.Add(this.belong_unit);
            this.Controls.Add(this.real_password);
            this.Controls.Add(this.password);
            this.Controls.Add(this.login_name);
            this.Controls.Add(this.role_box);
            this.Controls.Add(this.phone);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.belong_bm);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btn_Save);
            this.Controls.Add(this.note);
            this.Controls.Add(this.label1);
            this.Name = "Frm_userInfoAdd";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "用户信息";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btn_Save;
        private System.Windows.Forms.TextBox note;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox belong_bm;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox phone;
        private System.Windows.Forms.ComboBox role_box;
        private System.Windows.Forms.TextBox login_name;
        private System.Windows.Forms.TextBox password;
        private System.Windows.Forms.TextBox real_password;
        private System.Windows.Forms.TextBox belong_unit;
        private System.Windows.Forms.TextBox mobile;
        private System.Windows.Forms.TextBox mail;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox real_name;
        private System.Windows.Forms.TextBox ip_4;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox ip_2;
        private System.Windows.Forms.TextBox ip_3;
        private System.Windows.Forms.TextBox ip_1;
        private System.Windows.Forms.TextBox ip_5;
        private System.Windows.Forms.TextBox ip_7;
        private System.Windows.Forms.TextBox ip_6;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.TextBox ip_8;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Button btn_Select;
        private System.Windows.Forms.TextBox belong_userGroup;
    }
}