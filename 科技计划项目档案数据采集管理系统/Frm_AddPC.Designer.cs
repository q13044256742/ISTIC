namespace 科技计划项目档案数据采集管理系统
{
    partial class Frm_AddPC
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
            this.label1 = new System.Windows.Forms.Label();
            this.txt_BatchName = new System.Windows.Forms.TextBox();
            this.txt_BatchCode = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cbo_SourceUnit = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txt_giver = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txt_Receiver = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txt_Remark = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txt_UploadFile = new System.Windows.Forms.TextBox();
            this.btn_UploadFile = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dtp_TransferTime = new System.Windows.Forms.DateTimePicker();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dgv_CDlist = new System.Windows.Forms.DataGridView();
            this.gpmc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gpbh = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bz = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btn_Save = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_CDlist)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(36, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "批次名称";
            // 
            // txt_BatchName
            // 
            this.txt_BatchName.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txt_BatchName.Location = new System.Drawing.Point(110, 29);
            this.txt_BatchName.Name = "txt_BatchName";
            this.txt_BatchName.Size = new System.Drawing.Size(269, 23);
            this.txt_BatchName.TabIndex = 1;
            // 
            // txt_BatchCode
            // 
            this.txt_BatchCode.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txt_BatchCode.Location = new System.Drawing.Point(477, 29);
            this.txt_BatchCode.Name = "txt_BatchCode";
            this.txt_BatchCode.Size = new System.Drawing.Size(239, 23);
            this.txt_BatchCode.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(406, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 17);
            this.label2.TabIndex = 2;
            this.label2.Text = "批次编号";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(36, 68);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 17);
            this.label3.TabIndex = 4;
            this.label3.Text = "来源单位";
            // 
            // cbo_SourceUnit
            // 
            this.cbo_SourceUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo_SourceUnit.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbo_SourceUnit.FormattingEnabled = true;
            this.cbo_SourceUnit.Location = new System.Drawing.Point(110, 65);
            this.cbo_SourceUnit.Name = "cbo_SourceUnit";
            this.cbo_SourceUnit.Size = new System.Drawing.Size(269, 25);
            this.cbo_SourceUnit.TabIndex = 6;
            this.cbo_SourceUnit.SelectionChangeCommitted += new System.EventHandler(this.cbo_SourceUnit_SelectionChangeCommitted);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(406, 68);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(56, 17);
            this.label5.TabIndex = 8;
            this.label5.Text = "交接时间";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.Location = new System.Drawing.Point(418, 106);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(44, 17);
            this.label7.TabIndex = 11;
            this.label7.Text = "移交人";
            // 
            // txt_giver
            // 
            this.txt_giver.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txt_giver.Location = new System.Drawing.Point(477, 103);
            this.txt_giver.Name = "txt_giver";
            this.txt_giver.Size = new System.Drawing.Size(180, 23);
            this.txt_giver.TabIndex = 10;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label9.Location = new System.Drawing.Point(48, 106);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(44, 17);
            this.label9.TabIndex = 15;
            this.label9.Text = "接受人";
            // 
            // txt_Receiver
            // 
            this.txt_Receiver.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txt_Receiver.Location = new System.Drawing.Point(110, 103);
            this.txt_Receiver.Name = "txt_Receiver";
            this.txt_Receiver.Size = new System.Drawing.Size(180, 23);
            this.txt_Receiver.TabIndex = 14;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label11.Location = new System.Drawing.Point(60, 145);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(32, 17);
            this.label11.TabIndex = 21;
            this.label11.Text = "备注";
            // 
            // txt_Remark
            // 
            this.txt_Remark.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txt_Remark.Location = new System.Drawing.Point(110, 139);
            this.txt_Remark.Multiline = true;
            this.txt_Remark.Name = "txt_Remark";
            this.txt_Remark.Size = new System.Drawing.Size(584, 60);
            this.txt_Remark.TabIndex = 20;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label12.Location = new System.Drawing.Point(60, 215);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(32, 17);
            this.label12.TabIndex = 22;
            this.label12.Text = "附件";
            // 
            // txt_UploadFile
            // 
            this.txt_UploadFile.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txt_UploadFile.Location = new System.Drawing.Point(110, 212);
            this.txt_UploadFile.Name = "txt_UploadFile";
            this.txt_UploadFile.Size = new System.Drawing.Size(466, 23);
            this.txt_UploadFile.TabIndex = 23;
            // 
            // btn_UploadFile
            // 
            this.btn_UploadFile.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_UploadFile.Location = new System.Drawing.Point(582, 210);
            this.btn_UploadFile.Name = "btn_UploadFile";
            this.btn_UploadFile.Size = new System.Drawing.Size(75, 26);
            this.btn_UploadFile.TabIndex = 25;
            this.btn_UploadFile.Text = "上传";
            this.btn_UploadFile.UseVisualStyleBackColor = true;
            this.btn_UploadFile.Click += new System.EventHandler(this.btn_UploadFile_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dtp_TransferTime);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.btn_UploadFile);
            this.groupBox1.Controls.Add(this.txt_BatchName);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txt_UploadFile);
            this.groupBox1.Controls.Add(this.txt_BatchCode);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.txt_Remark);
            this.groupBox1.Controls.Add(this.cbo_SourceUnit);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.txt_giver);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.txt_Receiver);
            this.groupBox1.Font = new System.Drawing.Font("宋体", 10F);
            this.groupBox1.Location = new System.Drawing.Point(6, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(739, 252);
            this.groupBox1.TabIndex = 26;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "批次基本信息";
            // 
            // dtp_TransferTime
            // 
            this.dtp_TransferTime.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dtp_TransferTime.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtp_TransferTime.Location = new System.Drawing.Point(477, 66);
            this.dtp_TransferTime.Name = "dtp_TransferTime";
            this.dtp_TransferTime.Size = new System.Drawing.Size(180, 23);
            this.dtp_TransferTime.TabIndex = 29;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dgv_CDlist);
            this.groupBox2.Font = new System.Drawing.Font("宋体", 10F);
            this.groupBox2.Location = new System.Drawing.Point(6, 268);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(739, 269);
            this.groupBox2.TabIndex = 27;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "光盘列表";
            // 
            // dgv_CDlist
            // 
            this.dgv_CDlist.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv_CDlist.BackgroundColor = System.Drawing.Color.White;
            this.dgv_CDlist.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgv_CDlist.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_CDlist.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.gpmc,
            this.gpbh,
            this.bz});
            this.dgv_CDlist.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_CDlist.EnableHeadersVisualStyles = false;
            this.dgv_CDlist.Location = new System.Drawing.Point(3, 19);
            this.dgv_CDlist.Name = "dgv_CDlist";
            this.dgv_CDlist.RowTemplate.Height = 23;
            this.dgv_CDlist.Size = new System.Drawing.Size(733, 247);
            this.dgv_CDlist.TabIndex = 0;
            this.dgv_CDlist.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_CDlist_CellEnter);
            this.dgv_CDlist.RowLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_CDlist_RowLeave);
            // 
            // gpmc
            // 
            this.gpmc.FillWeight = 200F;
            this.gpmc.HeaderText = "光盘名称";
            this.gpmc.Name = "gpmc";
            // 
            // gpbh
            // 
            this.gpbh.FillWeight = 150F;
            this.gpbh.HeaderText = "光盘编号";
            this.gpbh.Name = "gpbh";
            // 
            // bz
            // 
            this.bz.FillWeight = 250F;
            this.bz.HeaderText = "备注";
            this.bz.Name = "bz";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btn_Save);
            this.groupBox3.Location = new System.Drawing.Point(6, 530);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(739, 48);
            this.groupBox3.TabIndex = 28;
            this.groupBox3.TabStop = false;
            // 
            // btn_Save
            // 
            this.btn_Save.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Save.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.btn_Save.Location = new System.Drawing.Point(658, 13);
            this.btn_Save.Name = "btn_Save";
            this.btn_Save.Size = new System.Drawing.Size(70, 30);
            this.btn_Save.TabIndex = 0;
            this.btn_Save.Text = "保存(&S)";
            this.btn_Save.UseVisualStyleBackColor = true;
            this.btn_Save.Click += new System.EventHandler(this.btn_Save_Click);
            // 
            // Frm_AddPC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(751, 580);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Frm_AddPC";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "新增批次信息";
            this.Load += new System.EventHandler(this.Frm_AddPC_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_CDlist)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_BatchName;
        private System.Windows.Forms.TextBox txt_BatchCode;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbo_SourceUnit;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txt_giver;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txt_Receiver;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txt_Remark;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txt_UploadFile;
        private System.Windows.Forms.Button btn_UploadFile;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView dgv_CDlist;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.DateTimePicker dtp_TransferTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn gpmc;
        private System.Windows.Forms.DataGridViewTextBoxColumn gpbh;
        private System.Windows.Forms.DataGridViewTextBoxColumn bz;
        private System.Windows.Forms.Button btn_Save;
    }
}