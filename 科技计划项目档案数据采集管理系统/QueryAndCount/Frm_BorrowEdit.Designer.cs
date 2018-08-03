namespace 科技计划项目档案数据采集管理系统
{
    partial class Frm_BorrowEdit
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
            if(disposing && (components != null))
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_BorrowEdit));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lbl_FIleName = new System.Windows.Forms.Label();
            this.lbl_FileCode = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txt_Borrow_Term = new DevExpress.XtraEditors.TextEdit();
            this.txt_Phone = new DevExpress.XtraEditors.TextEdit();
            this.txt_User = new DevExpress.XtraEditors.TextEdit();
            this.txt_Unit = new DevExpress.XtraEditors.TextEdit();
            this.cbo_FileType = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txt_Real_Return_Date = new DevExpress.XtraEditors.TextEdit();
            this.txt_Should_Return_Date = new DevExpress.XtraEditors.TextEdit();
            this.txt_Borrow_Date = new DevExpress.XtraEditors.TextEdit();
            this.lbl_LogUser = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.btn_Sure = new DevExpress.XtraEditors.SimpleButton();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txt_Borrow_Term.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_Phone.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_User.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_Unit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_Real_Return_Date.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_Should_Return_Date.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_Borrow_Date.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(47, 64);
            this.label1.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 22);
            this.label1.TabIndex = 0;
            this.label1.Text = "文件名称：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(31, 29);
            this.label2.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(106, 22);
            this.label2.TabIndex = 2;
            this.label2.Text = "文件类别号：";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.lbl_FIleName);
            this.groupBox1.Controls.Add(this.lbl_FileCode);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(8, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(579, 112);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "文件信息";
            // 
            // lbl_FIleName
            // 
            this.lbl_FIleName.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.lbl_FIleName.ImageAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.lbl_FIleName.Location = new System.Drawing.Point(143, 66);
            this.lbl_FIleName.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lbl_FIleName.Name = "lbl_FIleName";
            this.lbl_FIleName.Size = new System.Drawing.Size(432, 41);
            this.lbl_FIleName.TabIndex = 4;
            // 
            // lbl_FileCode
            // 
            this.lbl_FileCode.AutoSize = true;
            this.lbl_FileCode.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.lbl_FileCode.Location = new System.Drawing.Point(143, 30);
            this.lbl_FileCode.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lbl_FileCode.Name = "lbl_FileCode";
            this.lbl_FileCode.Size = new System.Drawing.Size(38, 21);
            this.lbl_FileCode.TabIndex = 3;
            this.lbl_FileCode.Text = "null";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.txt_Borrow_Term);
            this.groupBox2.Controls.Add(this.txt_Phone);
            this.groupBox2.Controls.Add(this.txt_User);
            this.groupBox2.Controls.Add(this.txt_Unit);
            this.groupBox2.Controls.Add(this.cbo_FileType);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.txt_Real_Return_Date);
            this.groupBox2.Controls.Add(this.txt_Should_Return_Date);
            this.groupBox2.Controls.Add(this.txt_Borrow_Date);
            this.groupBox2.Controls.Add(this.lbl_LogUser);
            this.groupBox2.Controls.Add(this.label13);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Location = new System.Drawing.Point(8, 117);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(579, 433);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "借阅人员信息";
            // 
            // txt_Borrow_Term
            // 
            this.txt_Borrow_Term.Location = new System.Drawing.Point(152, 206);
            this.txt_Borrow_Term.Name = "txt_Borrow_Term";
            this.txt_Borrow_Term.Properties.Appearance.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.txt_Borrow_Term.Properties.Appearance.Options.UseFont = true;
            this.txt_Borrow_Term.Size = new System.Drawing.Size(297, 28);
            this.txt_Borrow_Term.TabIndex = 4;
            // 
            // txt_Phone
            // 
            this.txt_Phone.Location = new System.Drawing.Point(152, 126);
            this.txt_Phone.Name = "txt_Phone";
            this.txt_Phone.Properties.Appearance.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.txt_Phone.Properties.Appearance.Options.UseFont = true;
            this.txt_Phone.Size = new System.Drawing.Size(297, 28);
            this.txt_Phone.TabIndex = 2;
            // 
            // txt_User
            // 
            this.txt_User.Location = new System.Drawing.Point(152, 86);
            this.txt_User.Name = "txt_User";
            this.txt_User.Properties.Appearance.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.txt_User.Properties.Appearance.Options.UseFont = true;
            this.txt_User.Size = new System.Drawing.Size(297, 28);
            this.txt_User.TabIndex = 1;
            // 
            // txt_Unit
            // 
            this.txt_Unit.Location = new System.Drawing.Point(152, 46);
            this.txt_Unit.Name = "txt_Unit";
            this.txt_Unit.Properties.Appearance.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.txt_Unit.Properties.Appearance.Options.UseFont = true;
            this.txt_Unit.Size = new System.Drawing.Size(297, 28);
            this.txt_Unit.TabIndex = 0;
            // 
            // cbo_FileType
            // 
            this.cbo_FileType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo_FileType.FormattingEnabled = true;
            this.cbo_FileType.Location = new System.Drawing.Point(152, 246);
            this.cbo_FileType.Name = "cbo_FileType";
            this.cbo_FileType.Size = new System.Drawing.Size(188, 29);
            this.cbo_FileType.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(48, 249);
            this.label3.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(90, 22);
            this.label3.TabIndex = 19;
            this.label3.Text = "文件形态：";
            // 
            // txt_Real_Return_Date
            // 
            this.txt_Real_Return_Date.Location = new System.Drawing.Point(152, 326);
            this.txt_Real_Return_Date.Name = "txt_Real_Return_Date";
            this.txt_Real_Return_Date.Properties.Appearance.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.txt_Real_Return_Date.Properties.Appearance.Options.UseFont = true;
            this.txt_Real_Return_Date.Properties.Mask.BeepOnError = true;
            this.txt_Real_Return_Date.Properties.Mask.EditMask = "D";
            this.txt_Real_Return_Date.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTime;
            this.txt_Real_Return_Date.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.txt_Real_Return_Date.Size = new System.Drawing.Size(297, 28);
            this.txt_Real_Return_Date.TabIndex = 7;
            // 
            // txt_Should_Return_Date
            // 
            this.txt_Should_Return_Date.Location = new System.Drawing.Point(152, 286);
            this.txt_Should_Return_Date.Name = "txt_Should_Return_Date";
            this.txt_Should_Return_Date.Properties.Appearance.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.txt_Should_Return_Date.Properties.Appearance.Options.UseFont = true;
            this.txt_Should_Return_Date.Properties.Mask.BeepOnError = true;
            this.txt_Should_Return_Date.Properties.Mask.EditMask = "D";
            this.txt_Should_Return_Date.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTime;
            this.txt_Should_Return_Date.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.txt_Should_Return_Date.Size = new System.Drawing.Size(297, 28);
            this.txt_Should_Return_Date.TabIndex = 6;
            // 
            // txt_Borrow_Date
            // 
            this.txt_Borrow_Date.Location = new System.Drawing.Point(152, 166);
            this.txt_Borrow_Date.Name = "txt_Borrow_Date";
            this.txt_Borrow_Date.Properties.Appearance.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.txt_Borrow_Date.Properties.Appearance.Options.UseFont = true;
            this.txt_Borrow_Date.Properties.Mask.EditMask = "D";
            this.txt_Borrow_Date.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTime;
            this.txt_Borrow_Date.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.txt_Borrow_Date.Size = new System.Drawing.Size(297, 28);
            this.txt_Borrow_Date.TabIndex = 3;
            // 
            // lbl_LogUser
            // 
            this.lbl_LogUser.AutoSize = true;
            this.lbl_LogUser.Location = new System.Drawing.Point(148, 370);
            this.lbl_LogUser.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lbl_LogUser.Name = "lbl_LogUser";
            this.lbl_LogUser.Size = new System.Drawing.Size(38, 21);
            this.lbl_LogUser.TabIndex = 18;
            this.lbl_LogUser.Text = "null";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label13.Location = new System.Drawing.Point(48, 369);
            this.label13.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(90, 22);
            this.label13.TabIndex = 17;
            this.label13.Text = "登记人员：";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label11.Location = new System.Drawing.Point(16, 329);
            this.label11.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(122, 22);
            this.label11.TabIndex = 13;
            this.label11.Text = "实际归还日期：";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label10.Location = new System.Drawing.Point(32, 289);
            this.label10.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(106, 22);
            this.label10.TabIndex = 11;
            this.label10.Text = "应归还日期：";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label9.Location = new System.Drawing.Point(48, 209);
            this.label9.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(90, 22);
            this.label9.TabIndex = 10;
            this.label9.Text = "借阅期限：";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label8.Location = new System.Drawing.Point(48, 169);
            this.label8.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(90, 22);
            this.label8.TabIndex = 8;
            this.label8.Text = "借阅日期：";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.Location = new System.Drawing.Point(48, 129);
            this.label7.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(90, 22);
            this.label7.TabIndex = 6;
            this.label7.Text = "联系方式：";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(80, 89);
            this.label6.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(58, 22);
            this.label6.TabIndex = 4;
            this.label6.Text = "姓名：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(80, 49);
            this.label5.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(58, 22);
            this.label5.TabIndex = 3;
            this.label5.Text = "单位：";
            // 
            // btn_Sure
            // 
            this.btn_Sure.Appearance.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.btn_Sure.Appearance.Options.UseFont = true;
            this.btn_Sure.Image = ((System.Drawing.Image)(resources.GetObject("btn_Sure.Image")));
            this.btn_Sure.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btn_Sure.Location = new System.Drawing.Point(255, 562);
            this.btn_Sure.Name = "btn_Sure";
            this.btn_Sure.Size = new System.Drawing.Size(87, 32);
            this.btn_Sure.TabIndex = 8;
            this.btn_Sure.Text = "确认借阅";
            this.btn_Sure.Click += new System.EventHandler(this.Btn_Sure_Click);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // Frm_BorrowEdit
            // 
            this.Appearance.Options.UseFont = true;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(596, 602);
            this.Controls.Add(this.btn_Sure);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Frm_BorrowEdit";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "文件借阅登记";
            this.Load += new System.EventHandler(this.Frm_BorrowEdit_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txt_Borrow_Term.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_Phone.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_User.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_Unit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_Real_Return_Date.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_Should_Return_Date.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_Borrow_Date.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
        public System.Windows.Forms.Label lbl_FIleName;
        private System.Windows.Forms.Label lbl_FileCode;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label lbl_LogUser;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        public DevExpress.XtraEditors.SimpleButton btn_Sure;
        public DevExpress.XtraEditors.TextEdit txt_Should_Return_Date;
        public DevExpress.XtraEditors.TextEdit txt_Borrow_Date;
        public DevExpress.XtraEditors.TextEdit txt_Real_Return_Date;
        public System.Windows.Forms.ComboBox cbo_FileType;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        public DevExpress.XtraEditors.TextEdit txt_Unit;
        public DevExpress.XtraEditors.TextEdit txt_Borrow_Term;
        public DevExpress.XtraEditors.TextEdit txt_Phone;
        public DevExpress.XtraEditors.TextEdit txt_User;
    }
}