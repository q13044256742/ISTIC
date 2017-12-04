namespace 科技计划项目档案数据采集管理系统
{
    partial class Frm_YJDJ_FirstFrame
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.dgv_SWDJ = new System.Windows.Forms.DataGridView();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lbl_Submit = new System.Windows.Forms.Label();
            this.cbo_TypeSelect = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dgv_GPDJ = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btn_Back = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.btn_Find = new System.Windows.Forms.Label();
            this.btn_Delete = new System.Windows.Forms.Label();
            this.btn_Add = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.cbo_Company = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_SWDJ)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_GPDJ)).BeginInit();
            this.panel1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tabControl1.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl1.Location = new System.Drawing.Point(0, 42);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1015, 372);
            this.tabControl1.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.groupBox3);
            this.tabPage1.Controls.Add(this.groupBox4);
            this.tabPage1.Location = new System.Drawing.Point(4, 28);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1007, 340);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "实物登记";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // dgv_SWDJ
            // 
            this.dgv_SWDJ.AllowUserToAddRows = false;
            this.dgv_SWDJ.AllowUserToDeleteRows = false;
            this.dgv_SWDJ.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv_SWDJ.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dgv_SWDJ.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgv_SWDJ.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_SWDJ.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_SWDJ.EnableHeadersVisualStyles = false;
            this.dgv_SWDJ.Location = new System.Drawing.Point(3, 23);
            this.dgv_SWDJ.Name = "dgv_SWDJ";
            this.dgv_SWDJ.ReadOnly = true;
            this.dgv_SWDJ.RowHeadersVisible = false;
            this.dgv_SWDJ.RowTemplate.Height = 23;
            this.dgv_SWDJ.Size = new System.Drawing.Size(987, 270);
            this.dgv_SWDJ.TabIndex = 0;
            this.dgv_SWDJ.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Dgv_SWDJ_CellContentClick);
            this.dgv_SWDJ.CellMouseEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.Dgv_SWDJ_CellMouseEnter);
            this.dgv_SWDJ.CellMouseLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.Dgv_SWDJ_CellMouseLeave);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.groupBox1);
            this.tabPage2.Controls.Add(this.groupBox2);
            this.tabPage2.Location = new System.Drawing.Point(4, 28);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1007, 340);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "光盘登记";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lbl_Submit);
            this.groupBox1.Controls.Add(this.cbo_TypeSelect);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(6, -4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(993, 47);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            // 
            // lbl_Submit
            // 
            this.lbl_Submit.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_Submit.BackColor = System.Drawing.Color.Transparent;
            this.lbl_Submit.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_Submit.Image = global::科技计划项目档案数据采集管理系统.Properties.Resources.tijiao;
            this.lbl_Submit.Location = new System.Drawing.Point(917, 14);
            this.lbl_Submit.Name = "lbl_Submit";
            this.lbl_Submit.Size = new System.Drawing.Size(67, 28);
            this.lbl_Submit.TabIndex = 3;
            this.lbl_Submit.Click += new System.EventHandler(this.lbl_Submit_Click);
            this.lbl_Submit.MouseEnter += new System.EventHandler(this.btn_Find_MouseEnter);
            this.lbl_Submit.MouseLeave += new System.EventHandler(this.btn_Find_MouseLeave);
            // 
            // cbo_TypeSelect
            // 
            this.cbo_TypeSelect.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo_TypeSelect.FormattingEnabled = true;
            this.cbo_TypeSelect.Items.AddRange(new object[] {
            "尚未读写",
            "读写成功",
            "解析异常",
            "读写异常",
            "所有"});
            this.cbo_TypeSelect.Location = new System.Drawing.Point(100, 15);
            this.cbo_TypeSelect.Name = "cbo_TypeSelect";
            this.cbo_TypeSelect.Size = new System.Drawing.Size(171, 27);
            this.cbo_TypeSelect.TabIndex = 1;
            this.cbo_TypeSelect.SelectedIndexChanged += new System.EventHandler(this.cbo_TypeSelect_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 19);
            this.label1.TabIndex = 0;
            this.label1.Text = "类型筛选:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dgv_GPDJ);
            this.groupBox2.Location = new System.Drawing.Point(6, 37);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(993, 297);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            // 
            // dgv_GPDJ
            // 
            this.dgv_GPDJ.AllowUserToAddRows = false;
            this.dgv_GPDJ.AllowUserToDeleteRows = false;
            this.dgv_GPDJ.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv_GPDJ.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dgv_GPDJ.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgv_GPDJ.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_GPDJ.EnableHeadersVisualStyles = false;
            this.dgv_GPDJ.Location = new System.Drawing.Point(3, 23);
            this.dgv_GPDJ.Name = "dgv_GPDJ";
            this.dgv_GPDJ.ReadOnly = true;
            this.dgv_GPDJ.RowTemplate.Height = 23;
            this.dgv_GPDJ.Size = new System.Drawing.Size(987, 271);
            this.dgv_GPDJ.TabIndex = 1;
            this.dgv_GPDJ.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_GPDJ_CellContentClick);
            // 
            // panel1
            // 
            this.panel1.BackgroundImage = global::科技计划项目档案数据采集管理系统.Properties.Resources.dhbg;
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel1.Controls.Add(this.btn_Back);
            this.panel1.Controls.Add(this.textBox1);
            this.panel1.Controls.Add(this.btn_Find);
            this.panel1.Controls.Add(this.btn_Delete);
            this.panel1.Controls.Add(this.btn_Add);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1015, 42);
            this.panel1.TabIndex = 0;
            // 
            // btn_Back
            // 
            this.btn_Back.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Back.BackColor = System.Drawing.Color.Transparent;
            this.btn_Back.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.btn_Back.Image = global::科技计划项目档案数据采集管理系统.Properties.Resources.back;
            this.btn_Back.Location = new System.Drawing.Point(938, 5);
            this.btn_Back.Name = "btn_Back";
            this.btn_Back.Size = new System.Drawing.Size(70, 33);
            this.btn_Back.TabIndex = 4;
            this.btn_Back.Click += new System.EventHandler(this.btn_Back_Click);
            this.btn_Back.MouseEnter += new System.EventHandler(this.btn_Find_MouseEnter);
            this.btn_Back.MouseLeave += new System.EventHandler(this.btn_Find_MouseLeave);
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.Location = new System.Drawing.Point(560, 8);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(136, 27);
            this.textBox1.TabIndex = 3;
            // 
            // btn_Find
            // 
            this.btn_Find.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Find.BackColor = System.Drawing.Color.Transparent;
            this.btn_Find.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.btn_Find.Image = global::科技计划项目档案数据采集管理系统.Properties.Resources.chaxun;
            this.btn_Find.Location = new System.Drawing.Point(704, 5);
            this.btn_Find.Name = "btn_Find";
            this.btn_Find.Size = new System.Drawing.Size(70, 33);
            this.btn_Find.TabIndex = 2;
            this.btn_Find.MouseEnter += new System.EventHandler(this.btn_Find_MouseEnter);
            this.btn_Find.MouseLeave += new System.EventHandler(this.btn_Find_MouseLeave);
            // 
            // btn_Delete
            // 
            this.btn_Delete.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Delete.BackColor = System.Drawing.Color.Transparent;
            this.btn_Delete.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.btn_Delete.Image = global::科技计划项目档案数据采集管理系统.Properties.Resources.shanchu;
            this.btn_Delete.Location = new System.Drawing.Point(860, 5);
            this.btn_Delete.Name = "btn_Delete";
            this.btn_Delete.Size = new System.Drawing.Size(70, 33);
            this.btn_Delete.TabIndex = 1;
            this.btn_Delete.MouseEnter += new System.EventHandler(this.btn_Find_MouseEnter);
            this.btn_Delete.MouseLeave += new System.EventHandler(this.btn_Find_MouseLeave);
            // 
            // btn_Add
            // 
            this.btn_Add.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Add.BackColor = System.Drawing.Color.Transparent;
            this.btn_Add.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.btn_Add.Image = global::科技计划项目档案数据采集管理系统.Properties.Resources.tianjia;
            this.btn_Add.Location = new System.Drawing.Point(782, 5);
            this.btn_Add.Name = "btn_Add";
            this.btn_Add.Size = new System.Drawing.Size(70, 33);
            this.btn_Add.TabIndex = 0;
            this.btn_Add.Click += new System.EventHandler(this.btn_Add_Click);
            this.btn_Add.MouseEnter += new System.EventHandler(this.btn_Find_MouseEnter);
            this.btn_Add.MouseLeave += new System.EventHandler(this.btn_Find_MouseLeave);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.cbo_Company);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Location = new System.Drawing.Point(6, -4);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(993, 47);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            // 
            // cbo_Company
            // 
            this.cbo_Company.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo_Company.FormattingEnabled = true;
            this.cbo_Company.Items.AddRange(new object[] {
            "重大专项管理办公室",
            "核高基（01）专项办公室",
            "集成电路（02）专项办公室",
            "宽带移动（03）专项办公室",
            "数控机床（04）专项办公室",
            "数控机床（04）专项办公室",
            "核电（06）专项办公室",
            "转基因（08）专项办公室",
            "新药创制（09）专项办公室",
            "传染病（10）专项办公室",
            "中国农村技术开发中心",
            "科学技术部火炬高技术产业开发中心",
            "科学技术部火炬高技术产业开发中心",
            "中国科学技术交流中心（中日技术合作事务中心）",
            "中国生物技术发展中心",
            "科学技术部高技术研究发展中心",
            "中国21世纪议程管理中心",
            "科技部科技评估中心",
            "国家科技风险开发事业中心",
            "中国生物技术发展中心"});
            this.cbo_Company.Location = new System.Drawing.Point(122, 15);
            this.cbo_Company.Name = "cbo_Company";
            this.cbo_Company.Size = new System.Drawing.Size(367, 27);
            this.cbo_Company.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 18);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(111, 19);
            this.label3.TabIndex = 0;
            this.label3.Text = "来源单位筛选:";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.dgv_SWDJ);
            this.groupBox4.Location = new System.Drawing.Point(6, 37);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(993, 296);
            this.groupBox4.TabIndex = 4;
            this.groupBox4.TabStop = false;
            // 
            // Frm_YJDJ_FirstFrame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1015, 488);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Frm_YJDJ_FirstFrame";
            this.Text = "移交登记";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_SWDJ)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_GPDJ)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label btn_Add;
        private System.Windows.Forms.Label btn_Find;
        private System.Windows.Forms.Label btn_Delete;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.DataGridView dgv_SWDJ;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.DataGridView dgv_GPDJ;
        private System.Windows.Forms.Label btn_Back;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cbo_TypeSelect;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label lbl_Submit;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ComboBox cbo_Company;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox4;
    }
}