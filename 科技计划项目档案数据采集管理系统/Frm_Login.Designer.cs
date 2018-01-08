namespace 科技计划项目档案数据采集管理系统
{
    partial class frm_Login
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
            this.myPanel1 = new 科技计划项目档案数据采集管理系统.Tools.MyPanel();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cbo_Identity = new System.Windows.Forms.ComboBox();
            this.txt_loginPassword = new System.Windows.Forms.TextBox();
            this.btn_Login = new System.Windows.Forms.Button();
            this.txt_loginName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.myPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // myPanel1
            // 
            this.myPanel1.BackgroundImage = global::科技计划项目档案数据采集管理系统.Properties.Resources.banner;
            this.myPanel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.myPanel1.Controls.Add(this.label3);
            this.myPanel1.Controls.Add(this.label2);
            this.myPanel1.Controls.Add(this.cbo_Identity);
            this.myPanel1.Controls.Add(this.txt_loginPassword);
            this.myPanel1.Controls.Add(this.btn_Login);
            this.myPanel1.Controls.Add(this.txt_loginName);
            this.myPanel1.Controls.Add(this.label1);
            this.myPanel1.Location = new System.Drawing.Point(145, 91);
            this.myPanel1.Name = "myPanel1";
            this.myPanel1.Size = new System.Drawing.Size(817, 515);
            this.myPanel1.TabIndex = 7;
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
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("黑体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(226, 232);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 16);
            this.label2.TabIndex = 1;
            this.label2.Text = "密码：";
            // 
            // cbo_Identity
            // 
            this.cbo_Identity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo_Identity.Font = new System.Drawing.Font("黑体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbo_Identity.FormattingEnabled = true;
            this.cbo_Identity.Items.AddRange(new object[] {
            "档案管理",
            "移交登记",
            "任务分配",
            "著录加工",
            "档案质检",
            "工作统计",
            "档案接收",
            "加工管理"});
            this.cbo_Identity.Location = new System.Drawing.Point(321, 270);
            this.cbo_Identity.Name = "cbo_Identity";
            this.cbo_Identity.Size = new System.Drawing.Size(103, 24);
            this.cbo_Identity.TabIndex = 6;
            // 
            // txt_loginPassword
            // 
            this.txt_loginPassword.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.txt_loginPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_loginPassword.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.txt_loginPassword.Location = new System.Drawing.Point(309, 228);
            this.txt_loginPassword.Name = "txt_loginPassword";
            this.txt_loginPassword.PasswordChar = '*';
            this.txt_loginPassword.Size = new System.Drawing.Size(168, 27);
            this.txt_loginPassword.TabIndex = 3;
            this.txt_loginPassword.Text = "admin";
            // 
            // btn_Login
            // 
            this.btn_Login.BackgroundImage = global::科技计划项目档案数据采集管理系统.Properties.Resources.login2;
            this.btn_Login.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_Login.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btn_Login.ForeColor = System.Drawing.Color.White;
            this.btn_Login.Location = new System.Drawing.Point(355, 337);
            this.btn_Login.Name = "btn_Login";
            this.btn_Login.Size = new System.Drawing.Size(95, 31);
            this.btn_Login.TabIndex = 4;
            this.btn_Login.Text = "登录(&L)";
            this.btn_Login.UseVisualStyleBackColor = true;
            this.btn_Login.Click += new System.EventHandler(this.btn_Login_Click);
            // 
            // txt_loginName
            // 
            this.txt_loginName.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.txt_loginName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_loginName.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.txt_loginName.Location = new System.Drawing.Point(309, 181);
            this.txt_loginName.Name = "txt_loginName";
            this.txt_loginName.Size = new System.Drawing.Size(168, 27);
            this.txt_loginName.TabIndex = 2;
            this.txt_loginName.Text = "admin";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("黑体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(210, 185);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "用户名：";
            // 
            // frm_Login
            // 
            this.Appearance.BackColor = System.Drawing.SystemColors.Control;
            this.Appearance.Options.UseBackColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1069, 630);
            this.Controls.Add(this.myPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_Login";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "登录系统";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frm_Login_FormClosing);
            this.myPanel1.ResumeLayout(false);
            this.myPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txt_loginName;
        private System.Windows.Forms.TextBox txt_loginPassword;
        private System.Windows.Forms.Button btn_Login;
        private System.Windows.Forms.ComboBox cbo_Identity;
        private Tools.MyPanel myPanel1;
        private System.Windows.Forms.Label label3;
    }
}

