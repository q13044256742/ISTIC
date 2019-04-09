namespace 科技计划项目档案数据采集管理系统
{
    partial class Frm_ProTypeSelect
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_ProTypeSelect));
            this.label1 = new System.Windows.Forms.Label();
            this.cbo_TypeSelect = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.pal_Special = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.cbo_SpecialType = new System.Windows.Forms.ComboBox();
            this.pal_BatchList = new System.Windows.Forms.GroupBox();
            this.listbox = new DevExpress.XtraEditors.ListBoxControl();
            this.btn_Sure = new 科技计划项目档案数据采集管理系统.KyoControl.KyoButton();
            this.groupBox1.SuspendLayout();
            this.pal_Special.SuspendLayout();
            this.pal_BatchList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.listbox)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(27, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 19);
            this.label1.TabIndex = 0;
            this.label1.Text = "计划类别:";
            // 
            // cbo_TypeSelect
            // 
            this.cbo_TypeSelect.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo_TypeSelect.FormattingEnabled = true;
            this.cbo_TypeSelect.Location = new System.Drawing.Point(108, 26);
            this.cbo_TypeSelect.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cbo_TypeSelect.Name = "cbo_TypeSelect";
            this.cbo_TypeSelect.Size = new System.Drawing.Size(234, 27);
            this.cbo_TypeSelect.TabIndex = 1;
            this.cbo_TypeSelect.SelectionChangeCommitted += new System.EventHandler(this.TypeSelect_SelectionChangeCommitted);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.pal_Special);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.cbo_TypeSelect);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(369, 123);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "待选择计划类别";
            // 
            // pal_Special
            // 
            this.pal_Special.Controls.Add(this.label2);
            this.pal_Special.Controls.Add(this.cbo_SpecialType);
            this.pal_Special.Enabled = false;
            this.pal_Special.Location = new System.Drawing.Point(12, 60);
            this.pal_Special.Name = "pal_Special";
            this.pal_Special.Size = new System.Drawing.Size(345, 47);
            this.pal_Special.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(15, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 19);
            this.label2.TabIndex = 2;
            this.label2.Text = "专项类别:";
            // 
            // cbo_SpecialType
            // 
            this.cbo_SpecialType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo_SpecialType.FormattingEnabled = true;
            this.cbo_SpecialType.Location = new System.Drawing.Point(96, 10);
            this.cbo_SpecialType.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cbo_SpecialType.Name = "cbo_SpecialType";
            this.cbo_SpecialType.Size = new System.Drawing.Size(234, 27);
            this.cbo_SpecialType.TabIndex = 3;
            this.cbo_SpecialType.SelectionChangeCommitted += new System.EventHandler(this.SpecialType_SelectionChangeCommitted);
            // 
            // pal_BatchList
            // 
            this.pal_BatchList.Controls.Add(this.listbox);
            this.pal_BatchList.Enabled = false;
            this.pal_BatchList.Location = new System.Drawing.Point(0, 131);
            this.pal_BatchList.Name = "pal_BatchList";
            this.pal_BatchList.Size = new System.Drawing.Size(369, 117);
            this.pal_BatchList.TabIndex = 4;
            this.pal_BatchList.TabStop = false;
            this.pal_BatchList.Text = "可选择继承的批次(ctrl键可多选)";
            // 
            // listbox
            // 
            this.listbox.Appearance.BackColor = System.Drawing.SystemColors.Control;
            this.listbox.Appearance.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.listbox.Appearance.Options.UseBackColor = true;
            this.listbox.Appearance.Options.UseFont = true;
            this.listbox.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.listbox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listbox.HorizontalScrollbar = true;
            this.listbox.Location = new System.Drawing.Point(3, 21);
            this.listbox.Name = "listbox";
            this.listbox.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.listbox.Size = new System.Drawing.Size(363, 93);
            this.listbox.TabIndex = 0;
            this.listbox.SelectedIndexChanged += new System.EventHandler(this.listbox_SelectedIndexChanged);
            // 
            // btn_Sure
            // 
            this.btn_Sure.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Sure.Appearance.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.btn_Sure.Appearance.Options.UseFont = true;
            this.btn_Sure.Image = ((System.Drawing.Image)(resources.GetObject("btn_Sure.Image")));
            this.btn_Sure.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btn_Sure.ImageToTextIndent = 5;
            this.btn_Sure.Location = new System.Drawing.Point(151, 263);
            this.btn_Sure.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btn_Sure.Name = "btn_Sure";
            this.btn_Sure.Size = new System.Drawing.Size(67, 30);
            this.btn_Sure.TabIndex = 288;
            this.btn_Sure.Text = "确定";
            this.btn_Sure.Click += new System.EventHandler(this.Btn_Sure_Click);
            // 
            // Frm_ProTypeSelect
            // 
            this.Appearance.Options.UseFont = true;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(369, 303);
            this.Controls.Add(this.btn_Sure);
            this.Controls.Add(this.pal_BatchList);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Frm_ProTypeSelect";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "计划类别";
            this.Load += new System.EventHandler(this.Frm_ProTypeSelect_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.pal_Special.ResumeLayout(false);
            this.pal_Special.PerformLayout();
            this.pal_BatchList.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.listbox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbo_TypeSelect;
        private KyoControl.KyoButton btn_Sure;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox pal_BatchList;
        private DevExpress.XtraEditors.ListBoxControl listbox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbo_SpecialType;
        private System.Windows.Forms.Panel pal_Special;
    }
}