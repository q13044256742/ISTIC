﻿namespace 科技计划项目档案数据采集管理系统.Manager
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_userInfoAdd));
            this.btn_Cancel = new 科技计划项目档案数据采集管理系统.KyoControl.KyoButton();
            this.btn_Save = new 科技计划项目档案数据采集管理系统.KyoControl.KyoButton();
            this.note = new DevExpress.XtraEditors.MemoEdit();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.belong_bm = new DevExpress.XtraEditors.TextEdit();
            this.login_name = new DevExpress.XtraEditors.TextEdit();
            this.password = new DevExpress.XtraEditors.TextEdit();
            this.real_password = new DevExpress.XtraEditors.TextEdit();
            this.belong_unit = new DevExpress.XtraEditors.TextEdit();
            this.mail = new DevExpress.XtraEditors.TextEdit();
            this.label11 = new System.Windows.Forms.Label();
            this.real_name = new DevExpress.XtraEditors.TextEdit();
            this.label21 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.ip_input = new DevExpress.XtraEditors.TextEdit();
            this.label12 = new System.Windows.Forms.Label();
            this.phone = new DevExpress.XtraEditors.TextEdit();
            this.label14 = new System.Windows.Forms.Label();
            this.mobile = new DevExpress.XtraEditors.TextEdit();
            this.label6 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.role_select = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.note.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.belong_bm.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.login_name.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.password.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.real_password.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.belong_unit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mail.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.real_name.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ip_input.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.phone.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mobile.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.Image = ((System.Drawing.Image)(resources.GetObject("btn_Cancel.Image")));
            this.btn_Cancel.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btn_Cancel.Location = new System.Drawing.Point(687, 414);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(76, 29);
            this.btn_Cancel.TabIndex = 35;
            this.btn_Cancel.Text = "取消";
            this.btn_Cancel.Click += new System.EventHandler(this.U_btnClose);
            // 
            // btn_Save
            // 
            this.btn_Save.Image = ((System.Drawing.Image)(resources.GetObject("btn_Save.Image")));
            this.btn_Save.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btn_Save.Location = new System.Drawing.Point(605, 414);
            this.btn_Save.Name = "btn_Save";
            this.btn_Save.Size = new System.Drawing.Size(76, 29);
            this.btn_Save.TabIndex = 34;
            this.btn_Save.Text = "保存";
            this.btn_Save.Click += new System.EventHandler(this.U_btnSave);
            // 
            // note
            // 
            this.note.Location = new System.Drawing.Point(105, 325);
            this.note.Name = "note";
            this.note.Properties.Appearance.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.note.Properties.Appearance.Options.UseFont = true;
            this.note.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
            this.note.Size = new System.Drawing.Size(659, 75);
            this.note.TabIndex = 33;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 11F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(33, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 19);
            this.label1.TabIndex = 25;
            this.label1.Text = "登录名：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 11F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(48, 72);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 19);
            this.label2.TabIndex = 37;
            this.label2.Text = "密码：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 11F, System.Drawing.FontStyle.Bold);
            this.label3.Location = new System.Drawing.Point(407, 72);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(84, 19);
            this.label3.TabIndex = 38;
            this.label3.Text = "确认密码：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("微软雅黑", 11F, System.Drawing.FontStyle.Bold);
            this.label4.Location = new System.Drawing.Point(19, 115);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(84, 19);
            this.label4.TabIndex = 39;
            this.label4.Text = "所属单位：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("微软雅黑", 11F, System.Drawing.FontStyle.Bold);
            this.label5.Location = new System.Drawing.Point(407, 116);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(84, 19);
            this.label5.TabIndex = 40;
            this.label5.Text = "所属部门：";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("微软雅黑", 11F, System.Drawing.FontStyle.Bold);
            this.label7.Location = new System.Drawing.Point(49, 210);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(54, 19);
            this.label7.TabIndex = 42;
            this.label7.Text = "邮箱：";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("微软雅黑", 11F, System.Drawing.FontStyle.Bold);
            this.label8.Location = new System.Drawing.Point(34, 262);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(69, 19);
            this.label8.TabIndex = 43;
            this.label8.Text = "IP地址：";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("微软雅黑", 11F, System.Drawing.FontStyle.Bold);
            this.label10.Location = new System.Drawing.Point(49, 326);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(54, 19);
            this.label10.TabIndex = 45;
            this.label10.Text = "备注：";
            // 
            // belong_bm
            // 
            this.belong_bm.Location = new System.Drawing.Point(494, 113);
            this.belong_bm.Name = "belong_bm";
            this.belong_bm.Properties.Appearance.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.belong_bm.Properties.Appearance.Options.UseFont = true;
            this.belong_bm.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
            this.belong_bm.Size = new System.Drawing.Size(271, 30);
            this.belong_bm.TabIndex = 51;
            // 
            // login_name
            // 
            this.login_name.Location = new System.Drawing.Point(105, 22);
            this.login_name.Name = "login_name";
            this.login_name.Properties.Appearance.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.login_name.Properties.Appearance.Options.UseFont = true;
            this.login_name.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
            this.login_name.Size = new System.Drawing.Size(271, 30);
            this.login_name.TabIndex = 60;
            // 
            // password
            // 
            this.password.Location = new System.Drawing.Point(106, 66);
            this.password.Name = "password";
            this.password.Properties.Appearance.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.password.Properties.Appearance.Options.UseFont = true;
            this.password.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
            this.password.Properties.PasswordChar = '*';
            this.password.Size = new System.Drawing.Size(271, 30);
            this.password.TabIndex = 61;
            // 
            // real_password
            // 
            this.real_password.Location = new System.Drawing.Point(494, 66);
            this.real_password.Name = "real_password";
            this.real_password.Properties.Appearance.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.real_password.Properties.Appearance.Options.UseFont = true;
            this.real_password.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
            this.real_password.Properties.PasswordChar = '*';
            this.real_password.Size = new System.Drawing.Size(271, 30);
            this.real_password.TabIndex = 62;
            // 
            // belong_unit
            // 
            this.belong_unit.Location = new System.Drawing.Point(106, 112);
            this.belong_unit.Name = "belong_unit";
            this.belong_unit.Properties.Appearance.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.belong_unit.Properties.Appearance.Options.UseFont = true;
            this.belong_unit.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
            this.belong_unit.Size = new System.Drawing.Size(271, 30);
            this.belong_unit.TabIndex = 63;
            // 
            // mail
            // 
            this.mail.Location = new System.Drawing.Point(106, 206);
            this.mail.Name = "mail";
            this.mail.Properties.Appearance.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.mail.Properties.Appearance.Options.UseFont = true;
            this.mail.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
            this.mail.Size = new System.Drawing.Size(271, 30);
            this.mail.TabIndex = 65;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("微软雅黑", 11F, System.Drawing.FontStyle.Bold);
            this.label11.Location = new System.Drawing.Point(407, 28);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(84, 19);
            this.label11.TabIndex = 66;
            this.label11.Text = "真实姓名：";
            // 
            // real_name
            // 
            this.real_name.Location = new System.Drawing.Point(494, 22);
            this.real_name.Name = "real_name";
            this.real_name.Properties.Appearance.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.real_name.Properties.Appearance.Options.UseFont = true;
            this.real_name.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
            this.real_name.Size = new System.Drawing.Size(271, 30);
            this.real_name.TabIndex = 67;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Font = new System.Drawing.Font("微软雅黑", 11F, System.Drawing.FontStyle.Bold);
            this.label21.ForeColor = System.Drawing.Color.Red;
            this.label21.Location = new System.Drawing.Point(379, 28);
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
            this.label22.Location = new System.Drawing.Point(380, 72);
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
            this.label23.Location = new System.Drawing.Point(768, 72);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(16, 19);
            this.label23.TabIndex = 112;
            this.label23.Text = "*";
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Font = new System.Drawing.Font("微软雅黑", 11F, System.Drawing.FontStyle.Bold);
            this.label25.ForeColor = System.Drawing.Color.Red;
            this.label25.Location = new System.Drawing.Point(768, 28);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(16, 19);
            this.label25.TabIndex = 114;
            this.label25.Text = "*";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("微软雅黑", 11F, System.Drawing.FontStyle.Bold);
            this.label9.ForeColor = System.Drawing.Color.Red;
            this.label9.Location = new System.Drawing.Point(380, 117);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(16, 19);
            this.label9.TabIndex = 116;
            this.label9.Text = "*";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("微软雅黑", 11F, System.Drawing.FontStyle.Bold);
            this.label13.ForeColor = System.Drawing.Color.Red;
            this.label13.Location = new System.Drawing.Point(768, 117);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(16, 19);
            this.label13.TabIndex = 117;
            this.label13.Text = "*";
            // 
            // ip_input
            // 
            this.ip_input.Location = new System.Drawing.Point(106, 262);
            this.ip_input.Name = "ip_input";
            this.ip_input.Properties.Appearance.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.ip_input.Properties.Appearance.Options.UseFont = true;
            this.ip_input.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
            this.ip_input.Size = new System.Drawing.Size(658, 30);
            this.ip_input.TabIndex = 118;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("微软雅黑", 11F, System.Drawing.FontStyle.Bold);
            this.label12.Location = new System.Drawing.Point(48, 165);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(54, 19);
            this.label12.TabIndex = 123;
            this.label12.Text = "角色：";
            // 
            // phone
            // 
            this.phone.Location = new System.Drawing.Point(494, 210);
            this.phone.Name = "phone";
            this.phone.Properties.Appearance.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.phone.Properties.Appearance.Options.UseFont = true;
            this.phone.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
            this.phone.Size = new System.Drawing.Size(271, 30);
            this.phone.TabIndex = 126;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("微软雅黑", 11F, System.Drawing.FontStyle.Bold);
            this.label14.Location = new System.Drawing.Point(437, 214);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(54, 19);
            this.label14.TabIndex = 125;
            this.label14.Text = "电话：";
            // 
            // mobile
            // 
            this.mobile.Location = new System.Drawing.Point(494, 162);
            this.mobile.Name = "mobile";
            this.mobile.Properties.Appearance.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.mobile.Properties.Appearance.Options.UseFont = true;
            this.mobile.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
            this.mobile.Size = new System.Drawing.Size(271, 30);
            this.mobile.TabIndex = 128;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("微软雅黑", 11F, System.Drawing.FontStyle.Bold);
            this.label6.Location = new System.Drawing.Point(437, 165);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(54, 19);
            this.label6.TabIndex = 127;
            this.label6.Text = "手机：";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("微软雅黑", 11F, System.Drawing.FontStyle.Bold);
            this.label15.ForeColor = System.Drawing.Color.Red;
            this.label15.Location = new System.Drawing.Point(380, 165);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(16, 19);
            this.label15.TabIndex = 129;
            this.label15.Text = "*";
            // 
            // role_select
            // 
            this.role_select.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.role_select.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.role_select.Location = new System.Drawing.Point(106, 159);
            this.role_select.Name = "role_select";
            this.role_select.Size = new System.Drawing.Size(271, 29);
            this.role_select.TabIndex = 132;
            // 
            // Frm_userInfoAdd
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(808, 455);
            this.Controls.Add(this.role_select);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.mobile);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.phone);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.ip_input);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label25);
            this.Controls.Add(this.label23);
            this.Controls.Add(this.label22);
            this.Controls.Add(this.label21);
            this.Controls.Add(this.real_name);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.mail);
            this.Controls.Add(this.belong_unit);
            this.Controls.Add(this.real_password);
            this.Controls.Add(this.password);
            this.Controls.Add(this.login_name);
            this.Controls.Add(this.belong_bm);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btn_Cancel);
            this.Controls.Add(this.btn_Save);
            this.Controls.Add(this.note);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Frm_userInfoAdd";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "用户信息";
            ((System.ComponentModel.ISupportInitialize)(this.note.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.belong_bm.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.login_name.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.password.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.real_password.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.belong_unit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mail.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.real_name.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ip_input.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.phone.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mobile.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private KyoControl.KyoButton btn_Cancel;
        private KyoControl.KyoButton btn_Save;
        private DevExpress.XtraEditors.MemoEdit note;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label10;
        private DevExpress.XtraEditors.TextEdit belong_bm;
        private DevExpress.XtraEditors.TextEdit login_name;
        private DevExpress.XtraEditors.TextEdit password;
        private DevExpress.XtraEditors.TextEdit real_password;
        private DevExpress.XtraEditors.TextEdit belong_unit;
        private DevExpress.XtraEditors.TextEdit mail;
        private System.Windows.Forms.Label label11;
        private DevExpress.XtraEditors.TextEdit real_name;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label13;
        private DevExpress.XtraEditors.TextEdit ip_input;
        private System.Windows.Forms.Label label12;
        private DevExpress.XtraEditors.TextEdit phone;
        private System.Windows.Forms.Label label14;
        private DevExpress.XtraEditors.TextEdit mobile;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.ComboBox role_select;
    }
}