namespace 科技计划项目档案数据采集管理系统
{
    partial class Frm_AddFile
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
            this.btn_Reset = new System.Windows.Forms.Button();
            this.lbl_OpenFile = new System.Windows.Forms.LinkLabel();
            this.btn_Exit = new System.Windows.Forms.Button();
            this.txt_remark = new System.Windows.Forms.TextBox();
            this.btn_Save_Add = new System.Windows.Forms.Button();
            this.label15 = new System.Windows.Forms.Label();
            this.txt_link = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.cbo_form = new System.Windows.Forms.ComboBox();
            this.label13 = new System.Windows.Forms.Label();
            this.cbo_format = new System.Windows.Forms.ComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.cbo_carrier = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txt_unit = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.dtp_date = new System.Windows.Forms.DateTimePicker();
            this.label9 = new System.Windows.Forms.Label();
            this.num_amount = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.num_page = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.cbo_secret = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txt_user = new System.Windows.Forms.TextBox();
            this.cbo_type = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txt_fileName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cbo_categor = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cbo_stage = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.num_amount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_page)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_Reset
            // 
            this.btn_Reset.Location = new System.Drawing.Point(171, 525);
            this.btn_Reset.Name = "btn_Reset";
            this.btn_Reset.Size = new System.Drawing.Size(68, 29);
            this.btn_Reset.TabIndex = 64;
            this.btn_Reset.Text = "重置";
            this.btn_Reset.UseVisualStyleBackColor = true;
            this.btn_Reset.Click += new System.EventHandler(this.button1_Click);
            // 
            // lbl_OpenFile
            // 
            this.lbl_OpenFile.AutoSize = true;
            this.lbl_OpenFile.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbl_OpenFile.Location = new System.Drawing.Point(506, 353);
            this.lbl_OpenFile.Name = "lbl_OpenFile";
            this.lbl_OpenFile.Size = new System.Drawing.Size(28, 14);
            this.lbl_OpenFile.TabIndex = 63;
            this.lbl_OpenFile.TabStop = true;
            this.lbl_OpenFile.Text = "...";
            this.lbl_OpenFile.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lbl_OpenFile_LinkClicked);
            // 
            // btn_Exit
            // 
            this.btn_Exit.Location = new System.Drawing.Point(321, 525);
            this.btn_Exit.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btn_Exit.Name = "btn_Exit";
            this.btn_Exit.Size = new System.Drawing.Size(68, 29);
            this.btn_Exit.TabIndex = 56;
            this.btn_Exit.Text = "退出";
            this.btn_Exit.UseVisualStyleBackColor = true;
            this.btn_Exit.Click += new System.EventHandler(this.btn_Exit_Click);
            // 
            // txt_remark
            // 
            this.txt_remark.Location = new System.Drawing.Point(101, 393);
            this.txt_remark.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.txt_remark.Multiline = true;
            this.txt_remark.Name = "txt_remark";
            this.txt_remark.Size = new System.Drawing.Size(431, 82);
            this.txt_remark.TabIndex = 53;
            // 
            // btn_Save_Add
            // 
            this.btn_Save_Add.Location = new System.Drawing.Point(246, 525);
            this.btn_Save_Add.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btn_Save_Add.Name = "btn_Save_Add";
            this.btn_Save_Add.Size = new System.Drawing.Size(68, 29);
            this.btn_Save_Add.TabIndex = 54;
            this.btn_Save_Add.Text = "保存";
            this.btn_Save_Add.UseVisualStyleBackColor = true;
            this.btn_Save_Add.Click += new System.EventHandler(this.Btn_Save_Click);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label15.Location = new System.Drawing.Point(60, 393);
            this.label15.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(37, 19);
            this.label15.TabIndex = 62;
            this.label15.Text = "备注";
            // 
            // txt_link
            // 
            this.txt_link.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txt_link.Location = new System.Drawing.Point(101, 350);
            this.txt_link.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.txt_link.Name = "txt_link";
            this.txt_link.ReadOnly = true;
            this.txt_link.Size = new System.Drawing.Size(400, 21);
            this.txt_link.TabIndex = 51;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label14.Location = new System.Drawing.Point(32, 351);
            this.label14.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(65, 19);
            this.label14.TabIndex = 61;
            this.label14.Text = "文件链接";
            // 
            // cbo_form
            // 
            this.cbo_form.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo_form.FormattingEnabled = true;
            this.cbo_form.Location = new System.Drawing.Point(101, 307);
            this.cbo_form.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.cbo_form.Name = "cbo_form";
            this.cbo_form.Size = new System.Drawing.Size(173, 21);
            this.cbo_form.TabIndex = 50;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label13.Location = new System.Drawing.Point(32, 309);
            this.label13.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(65, 19);
            this.label13.TabIndex = 60;
            this.label13.Text = "文件形态";
            // 
            // cbo_format
            // 
            this.cbo_format.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo_format.FormattingEnabled = true;
            this.cbo_format.Location = new System.Drawing.Point(363, 265);
            this.cbo_format.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.cbo_format.Name = "cbo_format";
            this.cbo_format.Size = new System.Drawing.Size(173, 21);
            this.cbo_format.TabIndex = 48;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label12.Location = new System.Drawing.Point(293, 267);
            this.label12.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(65, 19);
            this.label12.TabIndex = 59;
            this.label12.Text = "文件格式";
            // 
            // cbo_carrier
            // 
            this.cbo_carrier.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo_carrier.FormattingEnabled = true;
            this.cbo_carrier.Location = new System.Drawing.Point(101, 265);
            this.cbo_carrier.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.cbo_carrier.Name = "cbo_carrier";
            this.cbo_carrier.Size = new System.Drawing.Size(173, 21);
            this.cbo_carrier.TabIndex = 46;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label11.Location = new System.Drawing.Point(60, 267);
            this.label11.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(37, 19);
            this.label11.TabIndex = 58;
            this.label11.Text = "载体";
            // 
            // txt_unit
            // 
            this.txt_unit.Location = new System.Drawing.Point(101, 223);
            this.txt_unit.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.txt_unit.Name = "txt_unit";
            this.txt_unit.Size = new System.Drawing.Size(433, 23);
            this.txt_unit.TabIndex = 45;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label10.Location = new System.Drawing.Point(32, 225);
            this.label10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(65, 19);
            this.label10.TabIndex = 57;
            this.label10.Text = "存放单位";
            // 
            // dtp_date
            // 
            this.dtp_date.Location = new System.Drawing.Point(363, 181);
            this.dtp_date.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.dtp_date.Name = "dtp_date";
            this.dtp_date.Size = new System.Drawing.Size(173, 23);
            this.dtp_date.TabIndex = 44;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label9.Location = new System.Drawing.Point(293, 183);
            this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(65, 19);
            this.label9.TabIndex = 55;
            this.label9.Text = "形成日期";
            // 
            // num_amount
            // 
            this.num_amount.Location = new System.Drawing.Point(101, 181);
            this.num_amount.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.num_amount.Name = "num_amount";
            this.num_amount.Size = new System.Drawing.Size(114, 23);
            this.num_amount.TabIndex = 43;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label8.Location = new System.Drawing.Point(60, 183);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(37, 19);
            this.label8.TabIndex = 52;
            this.label8.Text = "份数";
            // 
            // num_page
            // 
            this.num_page.Location = new System.Drawing.Point(363, 139);
            this.num_page.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.num_page.Name = "num_page";
            this.num_page.Size = new System.Drawing.Size(112, 23);
            this.num_page.TabIndex = 41;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.Location = new System.Drawing.Point(317, 141);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(37, 19);
            this.label7.TabIndex = 49;
            this.label7.Text = "页数";
            // 
            // cbo_secret
            // 
            this.cbo_secret.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo_secret.FormattingEnabled = true;
            this.cbo_secret.Location = new System.Drawing.Point(101, 139);
            this.cbo_secret.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.cbo_secret.Name = "cbo_secret";
            this.cbo_secret.Size = new System.Drawing.Size(173, 21);
            this.cbo_secret.TabIndex = 39;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(60, 141);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(37, 19);
            this.label6.TabIndex = 47;
            this.label6.Text = "密级";
            // 
            // txt_user
            // 
            this.txt_user.Location = new System.Drawing.Point(101, 97);
            this.txt_user.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.txt_user.Name = "txt_user";
            this.txt_user.Size = new System.Drawing.Size(173, 23);
            this.txt_user.TabIndex = 36;
            // 
            // cbo_type
            // 
            this.cbo_type.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo_type.FormattingEnabled = true;
            this.cbo_type.Location = new System.Drawing.Point(363, 97);
            this.cbo_type.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.cbo_type.Name = "cbo_type";
            this.cbo_type.Size = new System.Drawing.Size(173, 21);
            this.cbo_type.TabIndex = 38;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(293, 99);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 19);
            this.label5.TabIndex = 42;
            this.label5.Text = "文件类型";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(18, 99);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(79, 19);
            this.label4.TabIndex = 40;
            this.label4.Text = "文件责任者";
            // 
            // txt_fileName
            // 
            this.txt_fileName.Location = new System.Drawing.Point(101, 55);
            this.txt_fileName.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.txt_fileName.Name = "txt_fileName";
            this.txt_fileName.Size = new System.Drawing.Size(433, 23);
            this.txt_fileName.TabIndex = 35;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(32, 57);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 19);
            this.label3.TabIndex = 37;
            this.label3.Text = "文件名称";
            // 
            // cbo_categor
            // 
            this.cbo_categor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo_categor.FormattingEnabled = true;
            this.cbo_categor.Location = new System.Drawing.Point(363, 13);
            this.cbo_categor.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.cbo_categor.Name = "cbo_categor";
            this.cbo_categor.Size = new System.Drawing.Size(173, 21);
            this.cbo_categor.TabIndex = 33;
            this.cbo_categor.SelectedIndexChanged += new System.EventHandler(this.Cbo_categor_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(293, 15);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 19);
            this.label2.TabIndex = 34;
            this.label2.Text = "文件类别";
            // 
            // cbo_stage
            // 
            this.cbo_stage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo_stage.FormattingEnabled = true;
            this.cbo_stage.Location = new System.Drawing.Point(101, 13);
            this.cbo_stage.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.cbo_stage.Name = "cbo_stage";
            this.cbo_stage.Size = new System.Drawing.Size(173, 21);
            this.cbo_stage.TabIndex = 32;
            this.cbo_stage.SelectedIndexChanged += new System.EventHandler(this.Cbo_stage_SelectionChangeCommitted);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(60, 15);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 19);
            this.label1.TabIndex = 31;
            this.label1.Text = "阶段";
            // 
            // Frm_AddFile
            // 
            this.Appearance.Options.UseFont = true;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(560, 572);
            this.Controls.Add(this.btn_Reset);
            this.Controls.Add(this.lbl_OpenFile);
            this.Controls.Add(this.btn_Exit);
            this.Controls.Add(this.txt_remark);
            this.Controls.Add(this.btn_Save_Add);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.txt_link);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.cbo_form);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.cbo_format);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.cbo_carrier);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.txt_unit);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.dtp_date);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.num_amount);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.num_page);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.cbo_secret);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txt_user);
            this.Controls.Add(this.cbo_type);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txt_fileName);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cbo_categor);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cbo_stage);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("宋体", 10F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Frm_AddFile";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "新增文件";
            this.Load += new System.EventHandler(this.Frm_AddFile_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Frm_AddFile_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.num_amount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_page)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_Reset;
        private System.Windows.Forms.LinkLabel lbl_OpenFile;
        private System.Windows.Forms.Button btn_Exit;
        private System.Windows.Forms.TextBox txt_remark;
        private System.Windows.Forms.Button btn_Save_Add;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox txt_link;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.ComboBox cbo_form;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.ComboBox cbo_format;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.ComboBox cbo_carrier;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txt_unit;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.DateTimePicker dtp_date;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.NumericUpDown num_amount;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.NumericUpDown num_page;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cbo_secret;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txt_user;
        private System.Windows.Forms.ComboBox cbo_type;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txt_fileName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbo_categor;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbo_stage;
        private System.Windows.Forms.Label label1;
    }
}