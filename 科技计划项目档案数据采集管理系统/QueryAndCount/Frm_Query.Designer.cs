namespace 科技计划项目档案数据采集管理系统
{
    partial class Frm_Query
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle19 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle20 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle26 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle27 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle21 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle22 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle23 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle24 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle25 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_Query));
            this.label1 = new System.Windows.Forms.Label();
            this.cbo_UserList = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.dtp_StartDate = new System.Windows.Forms.DateTimePicker();
            this.dtp_EndDate = new System.Windows.Forms.DateTimePicker();
            this.chk_AllDate = new System.Windows.Forms.CheckBox();
            this.view = new System.Windows.Forms.DataGridView();
            this.date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pcount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tcount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fcount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bcount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pgcount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lbl_Tip = new System.Windows.Forms.Label();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.btn_StartCount = new 科技计划项目档案数据采集管理系统.KyoControl.KyoButton();
            this.btn_Exprot = new 科技计划项目档案数据采集管理系统.KyoControl.KyoButton();
            this.rdo_JG = new System.Windows.Forms.RadioButton();
            this.rdo_ZJ = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.view)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(43, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 22);
            this.label1.TabIndex = 0;
            this.label1.Text = "用户选择：";
            // 
            // cbo_UserList
            // 
            this.cbo_UserList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo_UserList.FormattingEnabled = true;
            this.cbo_UserList.Location = new System.Drawing.Point(139, 25);
            this.cbo_UserList.Name = "cbo_UserList";
            this.cbo_UserList.Size = new System.Drawing.Size(136, 29);
            this.cbo_UserList.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(43, 88);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(90, 22);
            this.label2.TabIndex = 2;
            this.label2.Text = "加工时间：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(291, 88);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(22, 22);
            this.label3.TabIndex = 3;
            this.label3.Text = "~";
            // 
            // dtp_StartDate
            // 
            this.dtp_StartDate.CustomFormat = "yyyy-MM-dd";
            this.dtp_StartDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtp_StartDate.Location = new System.Drawing.Point(139, 85);
            this.dtp_StartDate.Name = "dtp_StartDate";
            this.dtp_StartDate.Size = new System.Drawing.Size(136, 29);
            this.dtp_StartDate.TabIndex = 4;
            // 
            // dtp_EndDate
            // 
            this.dtp_EndDate.CustomFormat = "yyyy-MM-dd";
            this.dtp_EndDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtp_EndDate.Location = new System.Drawing.Point(329, 85);
            this.dtp_EndDate.Name = "dtp_EndDate";
            this.dtp_EndDate.Size = new System.Drawing.Size(136, 29);
            this.dtp_EndDate.TabIndex = 5;
            // 
            // chk_AllDate
            // 
            this.chk_AllDate.AutoSize = true;
            this.chk_AllDate.Location = new System.Drawing.Point(590, 87);
            this.chk_AllDate.Name = "chk_AllDate";
            this.chk_AllDate.Size = new System.Drawing.Size(93, 25);
            this.chk_AllDate.TabIndex = 6;
            this.chk_AllDate.Text = "全部时间";
            this.chk_AllDate.UseVisualStyleBackColor = true;
            this.chk_AllDate.CheckedChanged += new System.EventHandler(this.chk_AllDate_CheckedChanged);
            // 
            // view
            // 
            this.view.AllowUserToAddRows = false;
            this.view.AllowUserToDeleteRows = false;
            dataGridViewCellStyle19.BackColor = System.Drawing.Color.White;
            this.view.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle19;
            this.view.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.view.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.view.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            dataGridViewCellStyle20.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle20.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle20.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle20.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle20.Padding = new System.Windows.Forms.Padding(3);
            dataGridViewCellStyle20.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle20.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle20.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.view.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle20;
            this.view.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.view.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.date,
            this.pcount,
            this.tcount,
            this.fcount,
            this.bcount,
            this.pgcount});
            dataGridViewCellStyle26.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle26.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle26.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle26.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(31)))), ((int)(((byte)(53)))));
            dataGridViewCellStyle26.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle26.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle26.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.view.DefaultCellStyle = dataGridViewCellStyle26;
            this.view.Location = new System.Drawing.Point(12, 156);
            this.view.Name = "view";
            this.view.ReadOnly = true;
            dataGridViewCellStyle27.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle27.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle27.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle27.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle27.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle27.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle27.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.view.RowHeadersDefaultCellStyle = dataGridViewCellStyle27;
            this.view.RowTemplate.Height = 23;
            this.view.Size = new System.Drawing.Size(1018, 462);
            this.view.TabIndex = 7;
            this.view.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.view_RowsAdded);
            // 
            // date
            // 
            this.date.HeaderText = "工作时间";
            this.date.Name = "date";
            this.date.ReadOnly = true;
            // 
            // pcount
            // 
            dataGridViewCellStyle21.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.pcount.DefaultCellStyle = dataGridViewCellStyle21;
            this.pcount.HeaderText = "项目/课题数";
            this.pcount.Name = "pcount";
            this.pcount.ReadOnly = true;
            // 
            // tcount
            // 
            dataGridViewCellStyle22.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.tcount.DefaultCellStyle = dataGridViewCellStyle22;
            this.tcount.HeaderText = "课题/子课题数";
            this.tcount.Name = "tcount";
            this.tcount.ReadOnly = true;
            // 
            // fcount
            // 
            dataGridViewCellStyle23.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.fcount.DefaultCellStyle = dataGridViewCellStyle23;
            this.fcount.HeaderText = "文件数";
            this.fcount.Name = "fcount";
            this.fcount.ReadOnly = true;
            // 
            // bcount
            // 
            dataGridViewCellStyle24.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.bcount.DefaultCellStyle = dataGridViewCellStyle24;
            this.bcount.HeaderText = "被返工数";
            this.bcount.Name = "bcount";
            this.bcount.ReadOnly = true;
            // 
            // pgcount
            // 
            dataGridViewCellStyle25.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.pgcount.DefaultCellStyle = dataGridViewCellStyle25;
            this.pgcount.FillWeight = 40F;
            this.pgcount.HeaderText = "页数";
            this.pgcount.Name = "pgcount";
            this.pgcount.ReadOnly = true;
            // 
            // lbl_Tip
            // 
            this.lbl_Tip.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_Tip.AutoSize = true;
            this.lbl_Tip.Location = new System.Drawing.Point(921, 621);
            this.lbl_Tip.Name = "lbl_Tip";
            this.lbl_Tip.Size = new System.Drawing.Size(109, 21);
            this.lbl_Tip.TabIndex = 10;
            this.lbl_Tip.Text = "合计 0 条数据";
            // 
            // btn_StartCount
            // 
            this.btn_StartCount.Appearance.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.btn_StartCount.Appearance.Options.UseFont = true;
            this.btn_StartCount.Image = ((System.Drawing.Image)(resources.GetObject("btn_StartCount.Image")));
            this.btn_StartCount.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btn_StartCount.Location = new System.Drawing.Point(944, 102);
            this.btn_StartCount.Name = "btn_StartCount";
            this.btn_StartCount.Size = new System.Drawing.Size(83, 31);
            this.btn_StartCount.TabIndex = 9;
            this.btn_StartCount.Text = "开始统计";
            this.btn_StartCount.Click += new System.EventHandler(this.Btn_StartCount_Click);
            // 
            // btn_Exprot
            // 
            this.btn_Exprot.Appearance.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.btn_Exprot.Appearance.Options.UseFont = true;
            this.btn_Exprot.Image = ((System.Drawing.Image)(resources.GetObject("btn_Exprot.Image")));
            this.btn_Exprot.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btn_Exprot.ImageToTextIndent = 5;
            this.btn_Exprot.Location = new System.Drawing.Point(12, 685);
            this.btn_Exprot.Name = "btn_Exprot";
            this.btn_Exprot.Size = new System.Drawing.Size(86, 34);
            this.btn_Exprot.TabIndex = 8;
            this.btn_Exprot.Text = "导出Excel";
            this.btn_Exprot.Click += new System.EventHandler(this.Btn_Exprot_Click);
            // 
            // rdo_JG
            // 
            this.rdo_JG.AutoSize = true;
            this.rdo_JG.Checked = true;
            this.rdo_JG.Location = new System.Drawing.Point(329, 27);
            this.rdo_JG.Name = "rdo_JG";
            this.rdo_JG.Size = new System.Drawing.Size(60, 25);
            this.rdo_JG.TabIndex = 11;
            this.rdo_JG.TabStop = true;
            this.rdo_JG.Text = "加工";
            this.rdo_JG.UseVisualStyleBackColor = true;
            // 
            // rdo_ZJ
            // 
            this.rdo_ZJ.AutoSize = true;
            this.rdo_ZJ.Location = new System.Drawing.Point(395, 27);
            this.rdo_ZJ.Name = "rdo_ZJ";
            this.rdo_ZJ.Size = new System.Drawing.Size(60, 25);
            this.rdo_ZJ.TabIndex = 12;
            this.rdo_ZJ.Text = "质检";
            this.rdo_ZJ.UseVisualStyleBackColor = true;
            this.rdo_ZJ.CheckedChanged += new System.EventHandler(this.rdo_ZJ_CheckedChanged);
            // 
            // Frm_Query
            // 
            this.Appearance.Options.UseFont = true;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1044, 734);
            this.Controls.Add(this.rdo_ZJ);
            this.Controls.Add(this.rdo_JG);
            this.Controls.Add(this.lbl_Tip);
            this.Controls.Add(this.btn_StartCount);
            this.Controls.Add(this.btn_Exprot);
            this.Controls.Add(this.view);
            this.Controls.Add(this.chk_AllDate);
            this.Controls.Add(this.dtp_EndDate);
            this.Controls.Add(this.dtp_StartDate);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cbo_UserList);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Frm_Query";
            this.Text = "工作量统计";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Frm_Query_FormClosing);
            this.Load += new System.EventHandler(this.Frm_Query_Load);
            ((System.ComponentModel.ISupportInitialize)(this.view)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbo_UserList;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dtp_StartDate;
        private System.Windows.Forms.DateTimePicker dtp_EndDate;
        private System.Windows.Forms.CheckBox chk_AllDate;
        private System.Windows.Forms.DataGridView view;
        private KyoControl.KyoButton btn_Exprot;
        private KyoControl.KyoButton btn_StartCount;
        private System.Windows.Forms.Label lbl_Tip;
        private System.Windows.Forms.DataGridViewTextBoxColumn date;
        private System.Windows.Forms.DataGridViewTextBoxColumn pcount;
        private System.Windows.Forms.DataGridViewTextBoxColumn tcount;
        private System.Windows.Forms.DataGridViewTextBoxColumn fcount;
        private System.Windows.Forms.DataGridViewTextBoxColumn bcount;
        private System.Windows.Forms.DataGridViewTextBoxColumn pgcount;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.RadioButton rdo_JG;
        private System.Windows.Forms.RadioButton rdo_ZJ;
    }
}