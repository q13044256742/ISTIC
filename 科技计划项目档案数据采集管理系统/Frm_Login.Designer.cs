using DevExpress.XtraEditors;

namespace 科技计划项目档案数据采集管理系统
{
    partial class Frm_Login
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
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
        private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_Login));
            this.label3 = new System.Windows.Forms.Label();
            this.pal_Login = new System.Windows.Forms.Panel();
            this.txt_loginName = new System.Windows.Forms.TextBox();
            this.txt_loginPassword = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btn_Login = new 科技计划项目档案数据采集管理系统.KyoControl.KyoButton();
            this.pal_Login.SuspendLayout();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 9.5F);
            this.label3.ForeColor = System.Drawing.Color.DimGray;
            this.label3.Location = new System.Drawing.Point(225, 433);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(308, 38);
            this.label3.TabIndex = 7;
            this.label3.Text = "版权：Copyright © 2017 中国科学技术信息研究所\r\n技术支持：中科软科技股份有限公司";
            // 
            // pal_Login
            // 
            this.pal_Login.BackgroundImage = global::科技计划项目档案数据采集管理系统.Properties.Resources.banner;
            this.pal_Login.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pal_Login.Controls.Add(this.txt_loginName);
            this.pal_Login.Controls.Add(this.txt_loginPassword);
            this.pal_Login.Controls.Add(this.label1);
            this.pal_Login.Controls.Add(this.label2);
            this.pal_Login.Controls.Add(this.btn_Login);
            this.pal_Login.Location = new System.Drawing.Point(43, 67);
            this.pal_Login.Name = "pal_Login";
            this.pal_Login.Size = new System.Drawing.Size(834, 509);
            this.pal_Login.TabIndex = 0;
            // 
            // txt_loginName
            // 
            this.txt_loginName.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.txt_loginName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_loginName.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.txt_loginName.Location = new System.Drawing.Point(307, 196);
            this.txt_loginName.Name = "txt_loginName";
            this.txt_loginName.Size = new System.Drawing.Size(168, 29);
            this.txt_loginName.TabIndex = 2;
            this.txt_loginName.Text = "admin";
            // 
            // txt_loginPassword
            // 
            this.txt_loginPassword.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.txt_loginPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_loginPassword.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.txt_loginPassword.Location = new System.Drawing.Point(307, 243);
            this.txt_loginPassword.Name = "txt_loginPassword";
            this.txt_loginPassword.PasswordChar = '*';
            this.txt_loginPassword.Size = new System.Drawing.Size(168, 29);
            this.txt_loginPassword.TabIndex = 3;
            this.txt_loginPassword.Text = "admin";
            this.txt_loginPassword.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Txt_loginPassword_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label1.Location = new System.Drawing.Point(218, 199);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 21);
            this.label1.TabIndex = 0;
            this.label1.Text = "用户名：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label2.Location = new System.Drawing.Point(234, 246);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 21);
            this.label2.TabIndex = 1;
            this.label2.Text = "密码：";
            // 
            // btn_Login
            // 
            this.btn_Login.Appearance.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.btn_Login.Appearance.Options.UseFont = true;
            this.btn_Login.Image = ((System.Drawing.Image)(resources.GetObject("btn_Login.Image")));
            this.btn_Login.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.btn_Login.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btn_Login.ImageToTextIndent = 5;
            this.btn_Login.Location = new System.Drawing.Point(375, 330);
            this.btn_Login.LookAndFeel.SkinName = "McSkin";
            this.btn_Login.LookAndFeel.UseDefaultLookAndFeel = false;
            this.btn_Login.Name = "btn_Login";
            this.btn_Login.Size = new System.Drawing.Size(84, 35);
            this.btn_Login.TabIndex = 4;
            this.btn_Login.Text = "登录";
            this.btn_Login.Click += new System.EventHandler(this.btn_Login_Click);
            // 
            // Frm_Login
            // 
            this.AcceptButton = this.btn_Login;
            this.Appearance.BackColor = System.Drawing.SystemColors.Control;
            this.Appearance.Options.UseBackColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(921, 529);
            this.Controls.Add(this.pal_Login);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.LookAndFeel.SkinName = "Summer 2008";
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Frm_Login";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "登录系统";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Frm_Login_FormClosing);
            this.pal_Login.ResumeLayout(false);
            this.pal_Login.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txt_loginName;
        private System.Windows.Forms.TextBox txt_loginPassword;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel pal_Login;
        private KyoControl.KyoButton btn_Login;
    }
}

