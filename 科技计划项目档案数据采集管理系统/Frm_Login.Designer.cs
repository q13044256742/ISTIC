﻿using DevExpress.XtraEditors;

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
            this.txt_loginPassword = new DevExpress.XtraEditors.TextEdit();
            this.txt_loginName = new DevExpress.XtraEditors.TextEdit();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btn_Login = new 科技计划项目档案数据采集管理系统.KyoControl.KyoButton();
            this.pal_Login.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txt_loginPassword.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_loginName.Properties)).BeginInit();
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
            this.pal_Login.Controls.Add(this.txt_loginPassword);
            this.pal_Login.Controls.Add(this.txt_loginName);
            this.pal_Login.Controls.Add(this.label1);
            this.pal_Login.Controls.Add(this.label2);
            this.pal_Login.Controls.Add(this.btn_Login);
            this.pal_Login.Location = new System.Drawing.Point(43, 67);
            this.pal_Login.Name = "pal_Login";
            this.pal_Login.Size = new System.Drawing.Size(834, 509);
            this.pal_Login.TabIndex = 0;
            // 
            // txt_loginPassword
            // 
            this.txt_loginPassword.EditValue = "admin";
            this.txt_loginPassword.Location = new System.Drawing.Point(307, 242);
            this.txt_loginPassword.Name = "txt_loginPassword";
            this.txt_loginPassword.Properties.Appearance.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.txt_loginPassword.Properties.Appearance.Options.UseFont = true;
            this.txt_loginPassword.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
            this.txt_loginPassword.Properties.PasswordChar = '*';
            this.txt_loginPassword.Size = new System.Drawing.Size(168, 30);
            this.txt_loginPassword.TabIndex = 6;
            // 
            // txt_loginName
            // 
            this.txt_loginName.EditValue = "admin";
            this.txt_loginName.Location = new System.Drawing.Point(307, 195);
            this.txt_loginName.Name = "txt_loginName";
            this.txt_loginName.Properties.Appearance.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.txt_loginName.Properties.Appearance.Options.UseFont = true;
            this.txt_loginName.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
            this.txt_loginName.Size = new System.Drawing.Size(168, 30);
            this.txt_loginName.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label1.Location = new System.Drawing.Point(218, 199);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 22);
            this.label1.TabIndex = 0;
            this.label1.Text = "用户名：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label2.Location = new System.Drawing.Point(234, 246);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 22);
            this.label2.TabIndex = 1;
            this.label2.Text = "密码：";
            // 
            // btn_Login
            // 
            this.btn_Login.Appearance.Font = new System.Drawing.Font("微软雅黑", 10.5F);
            this.btn_Login.Appearance.Options.UseFont = true;
            this.btn_Login.Image = ((System.Drawing.Image)(resources.GetObject("btn_Login.Image")));
            this.btn_Login.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.btn_Login.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btn_Login.ImageToTextIndent = 2;
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
            this.Load += new System.EventHandler(this.Frm_Login_Load);
            this.pal_Login.ResumeLayout(false);
            this.pal_Login.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txt_loginPassword.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_loginName.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel pal_Login;
        private KyoControl.KyoButton btn_Login;
        private TextEdit txt_loginPassword;
        private TextEdit txt_loginName;
    }
}

