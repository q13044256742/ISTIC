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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_AddFile));
            this.lbl_OpenFile = new System.Windows.Forms.LinkLabel();
            this.txt_Remark = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.txt_Unit = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.dtp_date = new System.Windows.Forms.DateTimePicker();
            this.label9 = new System.Windows.Forms.Label();
            this.num_Amount = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.num_Pages = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.txt_User = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cbo_categor = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cbo_stage = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txt_fileCode = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.pal_type = new System.Windows.Forms.Panel();
            this.rdo_type_2 = new System.Windows.Forms.RadioButton();
            this.rdo_type_3 = new System.Windows.Forms.RadioButton();
            this.rdo_type_4 = new System.Windows.Forms.RadioButton();
            this.rdo_type_1 = new System.Windows.Forms.RadioButton();
            this.lsv_LinkList = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.txt_date = new System.Windows.Forms.TextBox();
            this.num_Count = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.btn_Reset = new 科技计划项目档案数据采集管理系统.KyoControl.KyoButton();
            this.btn_Exit = new 科技计划项目档案数据采集管理系统.KyoControl.KyoButton();
            this.btn_Save_Add = new 科技计划项目档案数据采集管理系统.KyoControl.KyoButton();
            this.txt_fileName = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.num_Amount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_Pages)).BeginInit();
            this.pal_type.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_Count)).BeginInit();
            this.SuspendLayout();
            // 
            // lbl_OpenFile
            // 
            this.lbl_OpenFile.AutoSize = true;
            this.lbl_OpenFile.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.lbl_OpenFile.Location = new System.Drawing.Point(689, 302);
            this.lbl_OpenFile.Name = "lbl_OpenFile";
            this.lbl_OpenFile.Size = new System.Drawing.Size(42, 21);
            this.lbl_OpenFile.TabIndex = 63;
            this.lbl_OpenFile.TabStop = true;
            this.lbl_OpenFile.Text = "添加";
            this.lbl_OpenFile.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.OpenFile_LinkClicked);
            // 
            // txt_Remark
            // 
            this.txt_Remark.Location = new System.Drawing.Point(114, 459);
            this.txt_Remark.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.txt_Remark.Multiline = true;
            this.txt_Remark.Name = "txt_Remark";
            this.txt_Remark.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txt_Remark.Size = new System.Drawing.Size(572, 99);
            this.txt_Remark.TabIndex = 53;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("微软雅黑", 13F, System.Drawing.FontStyle.Bold);
            this.label15.Location = new System.Drawing.Point(55, 459);
            this.label15.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(48, 25);
            this.label15.TabIndex = 62;
            this.label15.Text = "备注";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("微软雅黑", 13F, System.Drawing.FontStyle.Bold);
            this.label14.Location = new System.Drawing.Point(19, 302);
            this.label14.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(84, 25);
            this.label14.TabIndex = 61;
            this.label14.Text = "文件链接";
            // 
            // txt_Unit
            // 
            this.txt_Unit.Location = new System.Drawing.Point(114, 253);
            this.txt_Unit.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.txt_Unit.Name = "txt_Unit";
            this.txt_Unit.Size = new System.Drawing.Size(407, 30);
            this.txt_Unit.TabIndex = 45;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("微软雅黑", 13F, System.Drawing.FontStyle.Bold);
            this.label10.Location = new System.Drawing.Point(19, 256);
            this.label10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(84, 25);
            this.label10.TabIndex = 57;
            this.label10.Text = "存放单位";
            // 
            // dtp_date
            // 
            this.dtp_date.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtp_date.Location = new System.Drawing.Point(325, 161);
            this.dtp_date.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.dtp_date.Name = "dtp_date";
            this.dtp_date.Size = new System.Drawing.Size(16, 30);
            this.dtp_date.TabIndex = 44;
            this.dtp_date.ValueChanged += new System.EventHandler(this.dtp_date_ValueChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("微软雅黑", 13F, System.Drawing.FontStyle.Bold);
            this.label9.Location = new System.Drawing.Point(19, 164);
            this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(84, 25);
            this.label9.TabIndex = 55;
            this.label9.Text = "形成日期";
            // 
            // num_Amount
            // 
            this.num_Amount.Location = new System.Drawing.Point(489, 207);
            this.num_Amount.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.num_Amount.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.num_Amount.Name = "num_Amount";
            this.num_Amount.Size = new System.Drawing.Size(227, 30);
            this.num_Amount.TabIndex = 43;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("微软雅黑", 13F, System.Drawing.FontStyle.Bold);
            this.label8.Location = new System.Drawing.Point(392, 210);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(84, 25);
            this.label8.TabIndex = 52;
            this.label8.Text = "移交份数";
            // 
            // num_Pages
            // 
            this.num_Pages.Location = new System.Drawing.Point(489, 161);
            this.num_Pages.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.num_Pages.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.num_Pages.Name = "num_Pages";
            this.num_Pages.Size = new System.Drawing.Size(227, 30);
            this.num_Pages.TabIndex = 41;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("微软雅黑", 13F, System.Drawing.FontStyle.Bold);
            this.label7.Location = new System.Drawing.Point(428, 164);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(48, 25);
            this.label7.TabIndex = 49;
            this.label7.Text = "页数";
            // 
            // txt_User
            // 
            this.txt_User.Location = new System.Drawing.Point(489, 57);
            this.txt_User.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.txt_User.Name = "txt_User";
            this.txt_User.Size = new System.Drawing.Size(227, 30);
            this.txt_User.TabIndex = 36;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("微软雅黑", 13F, System.Drawing.FontStyle.Bold);
            this.label5.Location = new System.Drawing.Point(19, 210);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(84, 25);
            this.label5.TabIndex = 42;
            this.label5.Text = "文件类型";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("微软雅黑", 13F, System.Drawing.FontStyle.Bold);
            this.label4.Location = new System.Drawing.Point(374, 60);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(102, 25);
            this.label4.TabIndex = 40;
            this.label4.Text = "文件责任者";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 13F, System.Drawing.FontStyle.Bold);
            this.label3.Location = new System.Drawing.Point(19, 103);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(84, 25);
            this.label3.TabIndex = 37;
            this.label3.Text = "文件名称";
            // 
            // cbo_categor
            // 
            this.cbo_categor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo_categor.FormattingEnabled = true;
            this.cbo_categor.Location = new System.Drawing.Point(489, 12);
            this.cbo_categor.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.cbo_categor.Name = "cbo_categor";
            this.cbo_categor.Size = new System.Drawing.Size(227, 28);
            this.cbo_categor.TabIndex = 33;
            this.cbo_categor.SelectionChangeCommitted += new System.EventHandler(this.Cbo_categor_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 13F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(389, 14);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(84, 25);
            this.label2.TabIndex = 34;
            this.label2.Text = "文件类别";
            // 
            // cbo_stage
            // 
            this.cbo_stage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo_stage.FormattingEnabled = true;
            this.cbo_stage.Location = new System.Drawing.Point(114, 12);
            this.cbo_stage.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.cbo_stage.Name = "cbo_stage";
            this.cbo_stage.Size = new System.Drawing.Size(227, 28);
            this.cbo_stage.TabIndex = 32;
            this.cbo_stage.SelectedIndexChanged += new System.EventHandler(this.Cbo_stage_SelectionChangeCommitted);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 13F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(55, 14);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 25);
            this.label1.TabIndex = 31;
            this.label1.Text = "阶段";
            // 
            // txt_fileCode
            // 
            this.txt_fileCode.Location = new System.Drawing.Point(114, 57);
            this.txt_fileCode.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.txt_fileCode.Name = "txt_fileCode";
            this.txt_fileCode.ReadOnly = true;
            this.txt_fileCode.Size = new System.Drawing.Size(227, 30);
            this.txt_fileCode.TabIndex = 66;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("微软雅黑", 13F, System.Drawing.FontStyle.Bold);
            this.label16.Location = new System.Drawing.Point(19, 60);
            this.label16.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(84, 25);
            this.label16.TabIndex = 67;
            this.label16.Text = "文件编号";
            // 
            // pal_type
            // 
            this.pal_type.Controls.Add(this.rdo_type_2);
            this.pal_type.Controls.Add(this.rdo_type_3);
            this.pal_type.Controls.Add(this.rdo_type_4);
            this.pal_type.Controls.Add(this.rdo_type_1);
            this.pal_type.Location = new System.Drawing.Point(114, 208);
            this.pal_type.Name = "pal_type";
            this.pal_type.Size = new System.Drawing.Size(265, 29);
            this.pal_type.TabIndex = 68;
            // 
            // rdo_type_2
            // 
            this.rdo_type_2.AutoSize = true;
            this.rdo_type_2.Font = new System.Drawing.Font("宋体", 13F);
            this.rdo_type_2.Location = new System.Drawing.Point(67, 3);
            this.rdo_type_2.Name = "rdo_type_2";
            this.rdo_type_2.Size = new System.Drawing.Size(62, 22);
            this.rdo_type_2.TabIndex = 3;
            this.rdo_type_2.TabStop = true;
            this.rdo_type_2.Tag = "1731a1cf-781d-438b-bbde-ac48d4d07914";
            this.rdo_type_2.Text = "财务";
            this.rdo_type_2.UseVisualStyleBackColor = true;
            // 
            // rdo_type_3
            // 
            this.rdo_type_3.AutoSize = true;
            this.rdo_type_3.Font = new System.Drawing.Font("宋体", 13F);
            this.rdo_type_3.Location = new System.Drawing.Point(133, 3);
            this.rdo_type_3.Name = "rdo_type_3";
            this.rdo_type_3.Size = new System.Drawing.Size(62, 22);
            this.rdo_type_3.TabIndex = 2;
            this.rdo_type_3.TabStop = true;
            this.rdo_type_3.Tag = "d61492d9-d981-459a-8481-a555becd6178";
            this.rdo_type_3.Text = "管理";
            this.rdo_type_3.UseVisualStyleBackColor = true;
            // 
            // rdo_type_4
            // 
            this.rdo_type_4.AutoSize = true;
            this.rdo_type_4.Font = new System.Drawing.Font("宋体", 13F);
            this.rdo_type_4.Location = new System.Drawing.Point(200, 3);
            this.rdo_type_4.Name = "rdo_type_4";
            this.rdo_type_4.Size = new System.Drawing.Size(62, 22);
            this.rdo_type_4.TabIndex = 1;
            this.rdo_type_4.TabStop = true;
            this.rdo_type_4.Tag = "430e0b65-0476-431f-9254-a57d83ee2095";
            this.rdo_type_4.Text = "文书";
            this.rdo_type_4.UseVisualStyleBackColor = true;
            // 
            // rdo_type_1
            // 
            this.rdo_type_1.AutoSize = true;
            this.rdo_type_1.Font = new System.Drawing.Font("宋体", 13F);
            this.rdo_type_1.Location = new System.Drawing.Point(2, 3);
            this.rdo_type_1.Name = "rdo_type_1";
            this.rdo_type_1.Size = new System.Drawing.Size(62, 22);
            this.rdo_type_1.TabIndex = 0;
            this.rdo_type_1.TabStop = true;
            this.rdo_type_1.Tag = "8c132762-1150-437b-8048-25a703ec1583";
            this.rdo_type_1.Text = "技术";
            this.rdo_type_1.UseVisualStyleBackColor = true;
            // 
            // lsv_LinkList
            // 
            this.lsv_LinkList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.lsv_LinkList.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lsv_LinkList.FullRowSelect = true;
            this.lsv_LinkList.GridLines = true;
            this.lsv_LinkList.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lsv_LinkList.Location = new System.Drawing.Point(114, 302);
            this.lsv_LinkList.MultiSelect = false;
            this.lsv_LinkList.Name = "lsv_LinkList";
            this.lsv_LinkList.Size = new System.Drawing.Size(572, 141);
            this.lsv_LinkList.TabIndex = 77;
            this.lsv_LinkList.UseCompatibleStateImageBehavior = false;
            this.lsv_LinkList.View = System.Windows.Forms.View.Details;
            this.lsv_LinkList.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Lsv_FileList_KeyDown);
            this.lsv_LinkList.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.Lsv_LinkList_MouseDoubleClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "序号";
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "名称";
            this.columnHeader2.Width = 500;
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // txt_date
            // 
            this.txt_date.Location = new System.Drawing.Point(114, 161);
            this.txt_date.Name = "txt_date";
            this.txt_date.Size = new System.Drawing.Size(209, 30);
            this.txt_date.TabIndex = 78;
            // 
            // num_Count
            // 
            this.num_Count.Font = new System.Drawing.Font("宋体", 15F);
            this.num_Count.Location = new System.Drawing.Point(601, 253);
            this.num_Count.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.num_Count.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.num_Count.Name = "num_Count";
            this.num_Count.Size = new System.Drawing.Size(115, 30);
            this.num_Count.TabIndex = 79;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("微软雅黑", 13F, System.Drawing.FontStyle.Bold);
            this.label6.Location = new System.Drawing.Point(540, 256);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(48, 25);
            this.label6.TabIndex = 80;
            this.label6.Text = "份数";
            // 
            // btn_Reset
            // 
            this.btn_Reset.Appearance.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Reset.Appearance.Options.UseFont = true;
            this.btn_Reset.Image = ((System.Drawing.Image)(resources.GetObject("btn_Reset.Image")));
            this.btn_Reset.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btn_Reset.ImageToTextIndent = 5;
            this.btn_Reset.Location = new System.Drawing.Point(267, 573);
            this.btn_Reset.Name = "btn_Reset";
            this.btn_Reset.Size = new System.Drawing.Size(67, 30);
            this.btn_Reset.TabIndex = 64;
            this.btn_Reset.Text = "重置";
            this.btn_Reset.Click += new System.EventHandler(this.Button1_Click);
            // 
            // btn_Exit
            // 
            this.btn_Exit.Appearance.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Exit.Appearance.Options.UseFont = true;
            this.btn_Exit.Image = ((System.Drawing.Image)(resources.GetObject("btn_Exit.Image")));
            this.btn_Exit.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btn_Exit.ImageToTextIndent = 5;
            this.btn_Exit.Location = new System.Drawing.Point(409, 573);
            this.btn_Exit.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btn_Exit.Name = "btn_Exit";
            this.btn_Exit.Size = new System.Drawing.Size(67, 30);
            this.btn_Exit.TabIndex = 56;
            this.btn_Exit.Text = "退出";
            this.btn_Exit.Click += new System.EventHandler(this.Btn_Exit_Click);
            // 
            // btn_Save_Add
            // 
            this.btn_Save_Add.Appearance.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Save_Add.Appearance.Options.UseFont = true;
            this.btn_Save_Add.Image = ((System.Drawing.Image)(resources.GetObject("btn_Save_Add.Image")));
            this.btn_Save_Add.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btn_Save_Add.ImageToTextIndent = 5;
            this.btn_Save_Add.Location = new System.Drawing.Point(338, 573);
            this.btn_Save_Add.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btn_Save_Add.Name = "btn_Save_Add";
            this.btn_Save_Add.Size = new System.Drawing.Size(67, 30);
            this.btn_Save_Add.TabIndex = 54;
            this.btn_Save_Add.Text = "保存";
            this.btn_Save_Add.Click += new System.EventHandler(this.Btn_Save_Click);
            // 
            // txt_fileName
            // 
            this.txt_fileName.Location = new System.Drawing.Point(114, 103);
            this.txt_fileName.Multiline = true;
            this.txt_fileName.Name = "txt_fileName";
            this.txt_fileName.Size = new System.Drawing.Size(602, 46);
            this.txt_fileName.TabIndex = 81;
            // 
            // Frm_AddFile
            // 
            this.Appearance.Options.UseFont = true;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(743, 612);
            this.Controls.Add(this.txt_fileName);
            this.Controls.Add(this.num_Count);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txt_date);
            this.Controls.Add(this.lsv_LinkList);
            this.Controls.Add(this.pal_type);
            this.Controls.Add(this.txt_fileCode);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.btn_Reset);
            this.Controls.Add(this.lbl_OpenFile);
            this.Controls.Add(this.btn_Exit);
            this.Controls.Add(this.txt_Remark);
            this.Controls.Add(this.btn_Save_Add);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.txt_Unit);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.dtp_date);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.num_Amount);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.num_Pages);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txt_User);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cbo_categor);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cbo_stage);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
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
            ((System.ComponentModel.ISupportInitialize)(this.num_Amount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_Pages)).EndInit();
            this.pal_type.ResumeLayout(false);
            this.pal_type.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_Count)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private KyoControl.KyoButton btn_Reset;
        private System.Windows.Forms.LinkLabel lbl_OpenFile;
        private KyoControl.KyoButton btn_Exit;
        private System.Windows.Forms.TextBox txt_Remark;
        private KyoControl.KyoButton btn_Save_Add;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label14;
        public System.Windows.Forms.TextBox txt_Unit;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.DateTimePicker dtp_date;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.NumericUpDown num_Amount;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.NumericUpDown num_Pages;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txt_User;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbo_categor;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbo_stage;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_fileCode;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Panel pal_type;
        private System.Windows.Forms.RadioButton rdo_type_2;
        private System.Windows.Forms.RadioButton rdo_type_3;
        private System.Windows.Forms.RadioButton rdo_type_4;
        private System.Windows.Forms.RadioButton rdo_type_1;
        private System.Windows.Forms.ListView lsv_LinkList;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.TextBox txt_date;
        private System.Windows.Forms.NumericUpDown num_Count;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txt_fileName;
    }
}