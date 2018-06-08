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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_AddFile));
            this.lbl_OpenFile = new System.Windows.Forms.LinkLabel();
            this.txt_Remark = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
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
            this.pal_carrier = new System.Windows.Forms.Panel();
            this.chk_carrier_2 = new System.Windows.Forms.CheckBox();
            this.chk_carrier_1 = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.txt_fileName = new System.Windows.Forms.ComboBox();
            this.btn_Reset = new 科技计划项目档案数据采集管理系统.KyoControl.KyoButton();
            this.btn_Exit = new 科技计划项目档案数据采集管理系统.KyoControl.KyoButton();
            this.btn_Save_Add = new 科技计划项目档案数据采集管理系统.KyoControl.KyoButton();
            this.txt_Link = new System.Windows.Forms.ListView();
            ((System.ComponentModel.ISupportInitialize)(this.num_Amount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_Pages)).BeginInit();
            this.pal_type.SuspendLayout();
            this.pal_carrier.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbl_OpenFile
            // 
            this.lbl_OpenFile.AutoSize = true;
            this.lbl_OpenFile.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbl_OpenFile.Location = new System.Drawing.Point(702, 379);
            this.lbl_OpenFile.Name = "lbl_OpenFile";
            this.lbl_OpenFile.Size = new System.Drawing.Size(35, 14);
            this.lbl_OpenFile.TabIndex = 63;
            this.lbl_OpenFile.TabStop = true;
            this.lbl_OpenFile.Text = "添加";
            this.lbl_OpenFile.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.OpenFile_LinkClicked);
            // 
            // txt_Remark
            // 
            this.txt_Remark.Font = new System.Drawing.Font("宋体", 12F);
            this.txt_Remark.Location = new System.Drawing.Point(130, 459);
            this.txt_Remark.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.txt_Remark.Multiline = true;
            this.txt_Remark.Name = "txt_Remark";
            this.txt_Remark.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txt_Remark.Size = new System.Drawing.Size(600, 123);
            this.txt_Remark.TabIndex = 53;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("微软雅黑", 11.5F, System.Drawing.FontStyle.Bold);
            this.label15.Location = new System.Drawing.Point(78, 459);
            this.label15.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(42, 22);
            this.label15.TabIndex = 62;
            this.label15.Text = "备注";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("微软雅黑", 11.5F, System.Drawing.FontStyle.Bold);
            this.label14.Location = new System.Drawing.Point(46, 338);
            this.label14.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(74, 22);
            this.label14.TabIndex = 61;
            this.label14.Text = "文件链接";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("微软雅黑", 11.5F, System.Drawing.FontStyle.Bold);
            this.label11.Location = new System.Drawing.Point(77, 244);
            this.label11.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(42, 22);
            this.label11.TabIndex = 58;
            this.label11.Text = "载体";
            // 
            // txt_Unit
            // 
            this.txt_Unit.Font = new System.Drawing.Font("宋体", 12F);
            this.txt_Unit.Location = new System.Drawing.Point(130, 291);
            this.txt_Unit.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.txt_Unit.Name = "txt_Unit";
            this.txt_Unit.Size = new System.Drawing.Size(600, 26);
            this.txt_Unit.TabIndex = 45;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("微软雅黑", 11.5F, System.Drawing.FontStyle.Bold);
            this.label10.Location = new System.Drawing.Point(46, 293);
            this.label10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(74, 22);
            this.label10.TabIndex = 57;
            this.label10.Text = "存放单位";
            // 
            // dtp_date
            // 
            this.dtp_date.Font = new System.Drawing.Font("宋体", 12F);
            this.dtp_date.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtp_date.Location = new System.Drawing.Point(130, 141);
            this.dtp_date.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.dtp_date.Name = "dtp_date";
            this.dtp_date.Size = new System.Drawing.Size(227, 26);
            this.dtp_date.TabIndex = 44;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("微软雅黑", 11.5F, System.Drawing.FontStyle.Bold);
            this.label9.Location = new System.Drawing.Point(46, 143);
            this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(74, 22);
            this.label9.TabIndex = 55;
            this.label9.Text = "形成日期";
            // 
            // num_Amount
            // 
            this.num_Amount.Font = new System.Drawing.Font("宋体", 12F);
            this.num_Amount.Location = new System.Drawing.Point(503, 243);
            this.num_Amount.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.num_Amount.Name = "num_Amount";
            this.num_Amount.Size = new System.Drawing.Size(124, 26);
            this.num_Amount.TabIndex = 43;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("微软雅黑", 11.5F, System.Drawing.FontStyle.Bold);
            this.label8.Location = new System.Drawing.Point(372, 245);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(118, 22);
            this.label8.TabIndex = 52;
            this.label8.Text = "份数(用于移交)";
            // 
            // num_Pages
            // 
            this.num_Pages.Font = new System.Drawing.Font("宋体", 12F);
            this.num_Pages.Location = new System.Drawing.Point(503, 141);
            this.num_Pages.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.num_Pages.Name = "num_Pages";
            this.num_Pages.Size = new System.Drawing.Size(124, 26);
            this.num_Pages.TabIndex = 41;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("微软雅黑", 11.5F, System.Drawing.FontStyle.Bold);
            this.label7.Location = new System.Drawing.Point(448, 143);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(42, 22);
            this.label7.TabIndex = 49;
            this.label7.Text = "页数";
            // 
            // txt_User
            // 
            this.txt_User.Font = new System.Drawing.Font("宋体", 12F);
            this.txt_User.Location = new System.Drawing.Point(503, 55);
            this.txt_User.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.txt_User.Name = "txt_User";
            this.txt_User.Size = new System.Drawing.Size(227, 26);
            this.txt_User.TabIndex = 36;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("微软雅黑", 11.5F, System.Drawing.FontStyle.Bold);
            this.label5.Location = new System.Drawing.Point(46, 194);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(74, 22);
            this.label5.TabIndex = 42;
            this.label5.Text = "文件类型";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("微软雅黑", 11.5F, System.Drawing.FontStyle.Bold);
            this.label4.Location = new System.Drawing.Point(400, 57);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(90, 22);
            this.label4.TabIndex = 40;
            this.label4.Text = "文件责任者";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 11.5F, System.Drawing.FontStyle.Bold);
            this.label3.Location = new System.Drawing.Point(46, 100);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(74, 22);
            this.label3.TabIndex = 37;
            this.label3.Text = "文件名称";
            // 
            // cbo_categor
            // 
            this.cbo_categor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo_categor.Font = new System.Drawing.Font("宋体", 12F);
            this.cbo_categor.FormattingEnabled = true;
            this.cbo_categor.Location = new System.Drawing.Point(503, 16);
            this.cbo_categor.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.cbo_categor.Name = "cbo_categor";
            this.cbo_categor.Size = new System.Drawing.Size(227, 24);
            this.cbo_categor.TabIndex = 33;
            this.cbo_categor.SelectionChangeCommitted += new System.EventHandler(this.Cbo_categor_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 11.5F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(416, 17);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 22);
            this.label2.TabIndex = 34;
            this.label2.Text = "文件类别";
            // 
            // cbo_stage
            // 
            this.cbo_stage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo_stage.Font = new System.Drawing.Font("宋体", 12F);
            this.cbo_stage.FormattingEnabled = true;
            this.cbo_stage.Location = new System.Drawing.Point(130, 17);
            this.cbo_stage.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.cbo_stage.Name = "cbo_stage";
            this.cbo_stage.Size = new System.Drawing.Size(227, 24);
            this.cbo_stage.TabIndex = 32;
            this.cbo_stage.SelectedIndexChanged += new System.EventHandler(this.Cbo_stage_SelectionChangeCommitted);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 11.5F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(78, 18);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 22);
            this.label1.TabIndex = 31;
            this.label1.Text = "阶段";
            // 
            // txt_fileCode
            // 
            this.txt_fileCode.Font = new System.Drawing.Font("宋体", 12F);
            this.txt_fileCode.Location = new System.Drawing.Point(130, 56);
            this.txt_fileCode.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.txt_fileCode.Name = "txt_fileCode";
            this.txt_fileCode.ReadOnly = true;
            this.txt_fileCode.Size = new System.Drawing.Size(227, 26);
            this.txt_fileCode.TabIndex = 66;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("微软雅黑", 11.5F, System.Drawing.FontStyle.Bold);
            this.label16.Location = new System.Drawing.Point(46, 58);
            this.label16.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(74, 22);
            this.label16.TabIndex = 67;
            this.label16.Text = "文件编号";
            // 
            // pal_type
            // 
            this.pal_type.Controls.Add(this.rdo_type_2);
            this.pal_type.Controls.Add(this.rdo_type_3);
            this.pal_type.Controls.Add(this.rdo_type_4);
            this.pal_type.Controls.Add(this.rdo_type_1);
            this.pal_type.Location = new System.Drawing.Point(130, 191);
            this.pal_type.Name = "pal_type";
            this.pal_type.Size = new System.Drawing.Size(259, 29);
            this.pal_type.TabIndex = 68;
            // 
            // rdo_type_2
            // 
            this.rdo_type_2.AutoSize = true;
            this.rdo_type_2.Location = new System.Drawing.Point(67, 4);
            this.rdo_type_2.Name = "rdo_type_2";
            this.rdo_type_2.Size = new System.Drawing.Size(58, 20);
            this.rdo_type_2.TabIndex = 3;
            this.rdo_type_2.TabStop = true;
            this.rdo_type_2.Tag = "1731a1cf-781d-438b-bbde-ac48d4d07914";
            this.rdo_type_2.Text = "财务";
            this.rdo_type_2.UseVisualStyleBackColor = true;
            // 
            // rdo_type_3
            // 
            this.rdo_type_3.AutoSize = true;
            this.rdo_type_3.Location = new System.Drawing.Point(133, 4);
            this.rdo_type_3.Name = "rdo_type_3";
            this.rdo_type_3.Size = new System.Drawing.Size(58, 20);
            this.rdo_type_3.TabIndex = 2;
            this.rdo_type_3.TabStop = true;
            this.rdo_type_3.Tag = "d61492d9-d981-459a-8481-a555becd6178";
            this.rdo_type_3.Text = "管理";
            this.rdo_type_3.UseVisualStyleBackColor = true;
            // 
            // rdo_type_4
            // 
            this.rdo_type_4.AutoSize = true;
            this.rdo_type_4.Location = new System.Drawing.Point(198, 4);
            this.rdo_type_4.Name = "rdo_type_4";
            this.rdo_type_4.Size = new System.Drawing.Size(58, 20);
            this.rdo_type_4.TabIndex = 1;
            this.rdo_type_4.TabStop = true;
            this.rdo_type_4.Tag = "430e0b65-0476-431f-9254-a57d83ee2095";
            this.rdo_type_4.Text = "文书";
            this.rdo_type_4.UseVisualStyleBackColor = true;
            // 
            // rdo_type_1
            // 
            this.rdo_type_1.AutoSize = true;
            this.rdo_type_1.Location = new System.Drawing.Point(2, 4);
            this.rdo_type_1.Name = "rdo_type_1";
            this.rdo_type_1.Size = new System.Drawing.Size(58, 20);
            this.rdo_type_1.TabIndex = 0;
            this.rdo_type_1.TabStop = true;
            this.rdo_type_1.Tag = "8c132762-1150-437b-8048-25a703ec1583";
            this.rdo_type_1.Text = "技术";
            this.rdo_type_1.UseVisualStyleBackColor = true;
            // 
            // pal_carrier
            // 
            this.pal_carrier.Controls.Add(this.chk_carrier_2);
            this.pal_carrier.Controls.Add(this.chk_carrier_1);
            this.pal_carrier.Location = new System.Drawing.Point(130, 241);
            this.pal_carrier.Name = "pal_carrier";
            this.pal_carrier.Size = new System.Drawing.Size(130, 29);
            this.pal_carrier.TabIndex = 69;
            // 
            // chk_carrier_2
            // 
            this.chk_carrier_2.AutoSize = true;
            this.chk_carrier_2.Location = new System.Drawing.Point(67, 4);
            this.chk_carrier_2.Name = "chk_carrier_2";
            this.chk_carrier_2.Size = new System.Drawing.Size(59, 20);
            this.chk_carrier_2.TabIndex = 1;
            this.chk_carrier_2.Tag = "6ffdf849-31fa-4401-a640-c371cd994daf";
            this.chk_carrier_2.Text = "电子";
            this.chk_carrier_2.UseVisualStyleBackColor = true;
            // 
            // chk_carrier_1
            // 
            this.chk_carrier_1.AutoSize = true;
            this.chk_carrier_1.Location = new System.Drawing.Point(2, 4);
            this.chk_carrier_1.Name = "chk_carrier_1";
            this.chk_carrier_1.Size = new System.Drawing.Size(59, 20);
            this.chk_carrier_1.TabIndex = 0;
            this.chk_carrier_1.Tag = "e7bce5d4-38b7-4d74-8aa2-c580b880aaba";
            this.chk_carrier_1.Text = "纸质";
            this.chk_carrier_1.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.Color.Red;
            this.label6.Location = new System.Drawing.Point(402, 20);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(16, 16);
            this.label6.TabIndex = 70;
            this.label6.Text = "*";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.ForeColor = System.Drawing.Color.Red;
            this.label12.Location = new System.Drawing.Point(36, 61);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(16, 16);
            this.label12.TabIndex = 71;
            this.label12.Text = "*";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.ForeColor = System.Drawing.Color.Red;
            this.label13.Location = new System.Drawing.Point(36, 103);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(16, 16);
            this.label13.TabIndex = 72;
            this.label13.Text = "*";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.ForeColor = System.Drawing.Color.Red;
            this.label17.Location = new System.Drawing.Point(36, 197);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(16, 16);
            this.label17.TabIndex = 73;
            this.label17.Text = "*";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.ForeColor = System.Drawing.Color.Red;
            this.label18.Location = new System.Drawing.Point(57, 247);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(16, 16);
            this.label18.TabIndex = 74;
            this.label18.Text = "*";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.ForeColor = System.Drawing.Color.Red;
            this.label19.Location = new System.Drawing.Point(36, 296);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(16, 16);
            this.label19.TabIndex = 75;
            this.label19.Text = "*";
            // 
            // txt_fileName
            // 
            this.txt_fileName.Font = new System.Drawing.Font("宋体", 12F);
            this.txt_fileName.FormattingEnabled = true;
            this.txt_fileName.Location = new System.Drawing.Point(130, 99);
            this.txt_fileName.Name = "txt_fileName";
            this.txt_fileName.Size = new System.Drawing.Size(600, 24);
            this.txt_fileName.TabIndex = 76;
            this.txt_fileName.SelectionChangeCommitted += new System.EventHandler(this.FileName_SelectionChangeCommitted);
            // 
            // btn_Reset
            // 
            this.btn_Reset.Appearance.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Reset.Appearance.Options.UseFont = true;
            this.btn_Reset.Image = ((System.Drawing.Image)(resources.GetObject("btn_Reset.Image")));
            this.btn_Reset.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btn_Reset.ImageToTextIndent = 5;
            this.btn_Reset.Location = new System.Drawing.Point(271, 597);
            this.btn_Reset.Name = "btn_Reset";
            this.btn_Reset.Size = new System.Drawing.Size(67, 30);
            this.btn_Reset.TabIndex = 64;
            this.btn_Reset.Text = "重置";
            this.btn_Reset.Click += new System.EventHandler(this.button1_Click);
            // 
            // btn_Exit
            // 
            this.btn_Exit.Appearance.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Exit.Appearance.Options.UseFont = true;
            this.btn_Exit.Image = ((System.Drawing.Image)(resources.GetObject("btn_Exit.Image")));
            this.btn_Exit.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btn_Exit.ImageToTextIndent = 5;
            this.btn_Exit.Location = new System.Drawing.Point(413, 597);
            this.btn_Exit.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btn_Exit.Name = "btn_Exit";
            this.btn_Exit.Size = new System.Drawing.Size(67, 30);
            this.btn_Exit.TabIndex = 56;
            this.btn_Exit.Text = "退出";
            this.btn_Exit.Click += new System.EventHandler(this.btn_Exit_Click);
            // 
            // btn_Save_Add
            // 
            this.btn_Save_Add.Appearance.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Save_Add.Appearance.Options.UseFont = true;
            this.btn_Save_Add.Image = ((System.Drawing.Image)(resources.GetObject("btn_Save_Add.Image")));
            this.btn_Save_Add.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btn_Save_Add.ImageToTextIndent = 5;
            this.btn_Save_Add.Location = new System.Drawing.Point(342, 597);
            this.btn_Save_Add.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btn_Save_Add.Name = "btn_Save_Add";
            this.btn_Save_Add.Size = new System.Drawing.Size(67, 30);
            this.btn_Save_Add.TabIndex = 54;
            this.btn_Save_Add.Text = "保存";
            this.btn_Save_Add.Click += new System.EventHandler(this.Btn_Save_Click);
            // 
            // txt_Link
            // 
            this.txt_Link.GridLines = true;
            this.txt_Link.Location = new System.Drawing.Point(130, 338);
            this.txt_Link.Name = "txt_Link";
            this.txt_Link.Size = new System.Drawing.Size(566, 103);
            this.txt_Link.TabIndex = 77;
            this.txt_Link.UseCompatibleStateImageBehavior = false;
            this.txt_Link.View = System.Windows.Forms.View.Details;
            // 
            // Frm_AddFile
            // 
            this.Appearance.Options.UseFont = true;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(751, 642);
            this.Controls.Add(this.txt_Link);
            this.Controls.Add(this.txt_fileName);
            this.Controls.Add(this.label19);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.pal_carrier);
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
            this.Controls.Add(this.label11);
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
            this.Font = new System.Drawing.Font("宋体", 11.5F);
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
            this.pal_carrier.ResumeLayout(false);
            this.pal_carrier.PerformLayout();
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
        private System.Windows.Forms.Label label11;
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
        private System.Windows.Forms.Panel pal_carrier;
        private System.Windows.Forms.CheckBox chk_carrier_2;
        private System.Windows.Forms.CheckBox chk_carrier_1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.ComboBox txt_fileName;
        private System.Windows.Forms.ListView txt_Link;
    }
}