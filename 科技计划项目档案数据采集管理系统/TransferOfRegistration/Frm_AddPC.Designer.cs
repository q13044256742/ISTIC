﻿namespace 科技计划项目档案数据采集管理系统
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_AddPC));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
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
            this.btn_UploadFile = new 科技计划项目档案数据采集管理系统.KyoControl.KyoButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dtp_TransferTime = new System.Windows.Forms.DateTimePicker();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dgv_CDlist = new System.Windows.Forms.DataGridView();
            this.btn_Save = new 科技计划项目档案数据采集管理系统.KyoControl.KyoButton();
            this.btn_Cancel = new 科技计划项目档案数据采集管理系统.KyoControl.KyoButton();
            this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gpmc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gpbh = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bz = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_CDlist)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(19, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 19);
            this.label1.TabIndex = 0;
            this.label1.Text = "批次名称";
            // 
            // txt_BatchName
            // 
            this.txt_BatchName.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.txt_BatchName.Location = new System.Drawing.Point(96, 24);
            this.txt_BatchName.Name = "txt_BatchName";
            this.txt_BatchName.Size = new System.Drawing.Size(269, 27);
            this.txt_BatchName.TabIndex = 1;
            // 
            // txt_BatchCode
            // 
            this.txt_BatchCode.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.txt_BatchCode.Location = new System.Drawing.Point(96, 64);
            this.txt_BatchCode.Name = "txt_BatchCode";
            this.txt_BatchCode.Size = new System.Drawing.Size(269, 27);
            this.txt_BatchCode.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(19, 68);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 19);
            this.label2.TabIndex = 2;
            this.label2.Text = "批次编号";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold);
            this.label3.Location = new System.Drawing.Point(388, 28);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 19);
            this.label3.TabIndex = 4;
            this.label3.Text = "来源单位";
            // 
            // cbo_SourceUnit
            // 
            this.cbo_SourceUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo_SourceUnit.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.cbo_SourceUnit.FormattingEnabled = true;
            this.cbo_SourceUnit.Location = new System.Drawing.Point(465, 23);
            this.cbo_SourceUnit.Name = "cbo_SourceUnit";
            this.cbo_SourceUnit.Size = new System.Drawing.Size(248, 28);
            this.cbo_SourceUnit.TabIndex = 2;
            this.cbo_SourceUnit.SelectionChangeCommitted += new System.EventHandler(this.cbo_SourceUnit_SelectionChangeCommitted);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold);
            this.label5.Location = new System.Drawing.Point(388, 68);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 19);
            this.label5.TabIndex = 8;
            this.label5.Text = "交接时间";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold);
            this.label7.Location = new System.Drawing.Point(402, 106);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(51, 19);
            this.label7.TabIndex = 11;
            this.label7.Text = "移交人";
            // 
            // txt_giver
            // 
            this.txt_giver.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.txt_giver.Location = new System.Drawing.Point(465, 102);
            this.txt_giver.Name = "txt_giver";
            this.txt_giver.Size = new System.Drawing.Size(248, 27);
            this.txt_giver.TabIndex = 6;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold);
            this.label9.Location = new System.Drawing.Point(33, 106);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(51, 19);
            this.label9.TabIndex = 15;
            this.label9.Text = "接收人";
            // 
            // txt_Receiver
            // 
            this.txt_Receiver.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.txt_Receiver.Location = new System.Drawing.Point(96, 100);
            this.txt_Receiver.Name = "txt_Receiver";
            this.txt_Receiver.Size = new System.Drawing.Size(269, 27);
            this.txt_Receiver.TabIndex = 5;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold);
            this.label11.Location = new System.Drawing.Point(47, 162);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(37, 19);
            this.label11.TabIndex = 21;
            this.label11.Text = "备注";
            // 
            // txt_Remark
            // 
            this.txt_Remark.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.txt_Remark.Location = new System.Drawing.Point(96, 138);
            this.txt_Remark.Multiline = true;
            this.txt_Remark.Name = "txt_Remark";
            this.txt_Remark.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txt_Remark.Size = new System.Drawing.Size(617, 67);
            this.txt_Remark.TabIndex = 7;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold);
            this.label12.Location = new System.Drawing.Point(47, 219);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(37, 19);
            this.label12.TabIndex = 22;
            this.label12.Text = "附件";
            // 
            // txt_UploadFile
            // 
            this.txt_UploadFile.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.txt_UploadFile.Location = new System.Drawing.Point(96, 215);
            this.txt_UploadFile.Name = "txt_UploadFile";
            this.txt_UploadFile.Size = new System.Drawing.Size(536, 27);
            this.txt_UploadFile.TabIndex = 9;
            // 
            // btn_UploadFile
            // 
            this.btn_UploadFile.Appearance.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.btn_UploadFile.Appearance.Options.UseFont = true;
            this.btn_UploadFile.Image = ((System.Drawing.Image)(resources.GetObject("btn_UploadFile.Image")));
            this.btn_UploadFile.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btn_UploadFile.ImageToTextIndent = 5;
            this.btn_UploadFile.Location = new System.Drawing.Point(646, 213);
            this.btn_UploadFile.Name = "btn_UploadFile";
            this.btn_UploadFile.Size = new System.Drawing.Size(67, 30);
            this.btn_UploadFile.TabIndex = 8;
            this.btn_UploadFile.Text = "添加";
            this.btn_UploadFile.Click += new System.EventHandler(this.Btn_UploadFile_Click);
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
            this.groupBox1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox1.Location = new System.Drawing.Point(6, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(739, 252);
            this.groupBox1.TabIndex = 26;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "批次基本信息";
            // 
            // dtp_TransferTime
            // 
            this.dtp_TransferTime.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.dtp_TransferTime.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtp_TransferTime.Location = new System.Drawing.Point(465, 64);
            this.dtp_TransferTime.Name = "dtp_TransferTime";
            this.dtp_TransferTime.Size = new System.Drawing.Size(248, 27);
            this.dtp_TransferTime.TabIndex = 4;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dgv_CDlist);
            this.groupBox2.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
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
            this.dgv_CDlist.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgv_CDlist.BackgroundColor = System.Drawing.Color.White;
            this.dgv_CDlist.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_CDlist.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgv_CDlist.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_CDlist.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.id,
            this.gpmc,
            this.gpbh,
            this.bz});
            this.dgv_CDlist.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_CDlist.EnableHeadersVisualStyles = false;
            this.dgv_CDlist.Location = new System.Drawing.Point(3, 25);
            this.dgv_CDlist.Name = "dgv_CDlist";
            this.dgv_CDlist.RowTemplate.Height = 23;
            this.dgv_CDlist.Size = new System.Drawing.Size(733, 241);
            this.dgv_CDlist.TabIndex = 10;
            this.dgv_CDlist.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.Dgv_CDlist_CellEnter);
            this.dgv_CDlist.RowLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_CDlist_RowLeave);
            // 
            // btn_Save
            // 
            this.btn_Save.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Save.Appearance.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.btn_Save.Appearance.Options.UseFont = true;
            this.btn_Save.Image = ((System.Drawing.Image)(resources.GetObject("btn_Save.Image")));
            this.btn_Save.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btn_Save.ImageToTextIndent = 5;
            this.btn_Save.Location = new System.Drawing.Point(305, 543);
            this.btn_Save.Name = "btn_Save";
            this.btn_Save.Size = new System.Drawing.Size(67, 30);
            this.btn_Save.TabIndex = 11;
            this.btn_Save.Text = "保存";
            this.btn_Save.Click += new System.EventHandler(this.Btn_Save_Click);
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Cancel.Appearance.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.btn_Cancel.Appearance.Options.UseFont = true;
            this.btn_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_Cancel.Image = ((System.Drawing.Image)(resources.GetObject("btn_Cancel.Image")));
            this.btn_Cancel.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btn_Cancel.ImageToTextIndent = 5;
            this.btn_Cancel.Location = new System.Drawing.Point(378, 543);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(67, 30);
            this.btn_Cancel.TabIndex = 28;
            this.btn_Cancel.Text = "关闭";
            this.btn_Cancel.Click += new System.EventHandler(this.btn_Cancel_Click);
            // 
            // id
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.id.DefaultCellStyle = dataGridViewCellStyle2;
            this.id.HeaderText = "编号";
            this.id.Name = "id";
            this.id.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // gpmc
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.gpmc.DefaultCellStyle = dataGridViewCellStyle3;
            this.gpmc.FillWeight = 200F;
            this.gpmc.HeaderText = "光盘名称";
            this.gpmc.Name = "gpmc";
            this.gpmc.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // gpbh
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.gpbh.DefaultCellStyle = dataGridViewCellStyle4;
            this.gpbh.FillWeight = 150F;
            this.gpbh.HeaderText = "光盘编号";
            this.gpbh.Name = "gpbh";
            this.gpbh.ReadOnly = true;
            this.gpbh.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // bz
            // 
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.bz.DefaultCellStyle = dataGridViewCellStyle5;
            this.bz.FillWeight = 250F;
            this.bz.HeaderText = "备注";
            this.bz.Name = "bz";
            this.bz.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Frm_AddPC
            // 
            this.Appearance.Options.UseFont = true;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.CancelButton = this.btn_Cancel;
            this.ClientSize = new System.Drawing.Size(751, 580);
            this.Controls.Add(this.btn_Cancel);
            this.Controls.Add(this.btn_Save);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Frm_AddPC";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.Frm_AddPC_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_CDlist)).EndInit();
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
        private KyoControl.KyoButton btn_UploadFile;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView dgv_CDlist;
        private System.Windows.Forms.DateTimePicker dtp_TransferTime;
        private KyoControl.KyoButton btn_Save;
        private KyoControl.KyoButton btn_Cancel;
        private System.Windows.Forms.DataGridViewTextBoxColumn id;
        private System.Windows.Forms.DataGridViewTextBoxColumn gpmc;
        private System.Windows.Forms.DataGridViewTextBoxColumn gpbh;
        private System.Windows.Forms.DataGridViewTextBoxColumn bz;
    }
}