namespace 科技计划项目档案数据采集管理系统
{
    partial class Frm_Statistics
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_Statistics));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle22 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle23 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle24 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle25 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle26 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle27 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle28 = new System.Windows.Forms.DataGridViewCellStyle();
            this.ac_LeftMenu = new DevExpress.XtraBars.Navigation.AccordionControl();
            this.acg_Register = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.ace_all = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.view = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.searchControl = new DevExpress.XtraEditors.SearchControl();
            this.btn_Query = new DevExpress.XtraEditors.SimpleButton();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPane1 = new DevExpress.XtraBars.Navigation.TabPane();
            this.tabNavigationPage1 = new DevExpress.XtraBars.Navigation.TabNavigationPage();
            this.rdo_ZJ = new System.Windows.Forms.RadioButton();
            this.btn_StartCount = new 科技计划项目档案数据采集管理系统.KyoControl.KyoButton();
            this.btn_Exprot = new 科技计划项目档案数据采集管理系统.KyoControl.KyoButton();
            this.rdo_JG = new System.Windows.Forms.RadioButton();
            this.countView = new System.Windows.Forms.DataGridView();
            this.date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pcount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tcount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fcount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bcount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pgcount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.chk_AllDate = new System.Windows.Forms.CheckBox();
            this.dtp_EndDate = new System.Windows.Forms.DateTimePicker();
            this.dtp_StartDate = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cbo_UserList = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tabNavigationPage2 = new DevExpress.XtraBars.Navigation.TabNavigationPage();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.pName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.ac_LeftMenu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.view)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.searchControl.Properties)).BeginInit();
            this.tabPane1.SuspendLayout();
            this.tabNavigationPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.countView)).BeginInit();
            this.tabNavigationPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // ac_LeftMenu
            // 
            this.ac_LeftMenu.AllowItemSelection = true;
            this.ac_LeftMenu.Appearance.Group.Hovered.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.ac_LeftMenu.Appearance.Group.Hovered.Options.UseFont = true;
            this.ac_LeftMenu.Appearance.Group.Normal.Font = new System.Drawing.Font("微软雅黑", 11F, System.Drawing.FontStyle.Bold);
            this.ac_LeftMenu.Appearance.Group.Normal.Options.UseFont = true;
            this.ac_LeftMenu.Appearance.Group.Pressed.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.ac_LeftMenu.Appearance.Group.Pressed.Options.UseFont = true;
            this.ac_LeftMenu.Appearance.Item.Hovered.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.ac_LeftMenu.Appearance.Item.Hovered.Options.UseFont = true;
            this.ac_LeftMenu.Appearance.Item.Normal.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.ac_LeftMenu.Appearance.Item.Normal.Options.UseFont = true;
            this.ac_LeftMenu.Appearance.Item.Pressed.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.ac_LeftMenu.Appearance.Item.Pressed.Options.UseFont = true;
            this.ac_LeftMenu.Dock = System.Windows.Forms.DockStyle.Left;
            this.ac_LeftMenu.Elements.AddRange(new DevExpress.XtraBars.Navigation.AccordionControlElement[] {
            this.acg_Register});
            this.ac_LeftMenu.Location = new System.Drawing.Point(0, 0);
            this.ac_LeftMenu.LookAndFeel.SkinName = "McSkin";
            this.ac_LeftMenu.LookAndFeel.UseDefaultLookAndFeel = false;
            this.ac_LeftMenu.Name = "ac_LeftMenu";
            this.ac_LeftMenu.ScrollBarMode = DevExpress.XtraBars.Navigation.ScrollBarMode.Auto;
            this.ac_LeftMenu.ShowToolTips = false;
            this.ac_LeftMenu.Size = new System.Drawing.Size(275, 673);
            this.ac_LeftMenu.TabIndex = 13;
            // 
            // acg_Register
            // 
            this.acg_Register.Appearance.Hovered.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold);
            this.acg_Register.Appearance.Hovered.ForeColor = System.Drawing.Color.Navy;
            this.acg_Register.Appearance.Hovered.Options.UseFont = true;
            this.acg_Register.Appearance.Hovered.Options.UseForeColor = true;
            this.acg_Register.Appearance.Normal.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold);
            this.acg_Register.Appearance.Normal.Options.UseFont = true;
            this.acg_Register.Appearance.Pressed.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold);
            this.acg_Register.Appearance.Pressed.ForeColor = System.Drawing.Color.Navy;
            this.acg_Register.Appearance.Pressed.Options.UseFont = true;
            this.acg_Register.Appearance.Pressed.Options.UseForeColor = true;
            this.acg_Register.Elements.AddRange(new DevExpress.XtraBars.Navigation.AccordionControlElement[] {
            this.ace_all});
            this.acg_Register.Expanded = true;
            this.acg_Register.Image = ((System.Drawing.Image)(resources.GetObject("acg_Register.Image")));
            this.acg_Register.Name = "acg_Register";
            this.acg_Register.Text = "数据统计";
            this.acg_Register.TextToImageDistance = 10;
            // 
            // ace_all
            // 
            this.ace_all.Name = "ace_all";
            this.ace_all.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.ace_all.Text = "全部来源单位";
            this.ace_all.Click += new System.EventHandler(this.Item_Click);
            // 
            // view
            // 
            this.view.AllowUserToAddRows = false;
            this.view.AllowUserToDeleteRows = false;
            this.view.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.view.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.view.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.view.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.view.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.pName,
            this.pAmount,
            this.fAmount,
            this.bAmount});
            this.view.Dock = System.Windows.Forms.DockStyle.Fill;
            this.view.Location = new System.Drawing.Point(275, 69);
            this.view.Name = "view";
            this.view.ReadOnly = true;
            this.view.RowTemplate.Height = 23;
            this.view.Size = new System.Drawing.Size(971, 604);
            this.view.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.searchControl);
            this.panel1.Controls.Add(this.btn_Query);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel1.Location = new System.Drawing.Point(275, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(971, 69);
            this.panel1.TabIndex = 16;
            // 
            // searchControl
            // 
            this.searchControl.Location = new System.Drawing.Point(120, 20);
            this.searchControl.Name = "searchControl";
            this.searchControl.Properties.Appearance.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.searchControl.Properties.Appearance.Options.UseFont = true;
            this.searchControl.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Repository.ClearButton(),
            new DevExpress.XtraEditors.Repository.SearchButton()});
            this.searchControl.Properties.NullValuePrompt = " ";
            this.searchControl.Properties.NullValuePromptShowForEmptyValue = false;
            this.searchControl.Size = new System.Drawing.Size(263, 28);
            this.searchControl.TabIndex = 3;
            // 
            // btn_Query
            // 
            this.btn_Query.Appearance.Font = new System.Drawing.Font("微软雅黑", 10.5F);
            this.btn_Query.Appearance.Options.UseFont = true;
            this.btn_Query.Location = new System.Drawing.Point(389, 20);
            this.btn_Query.Name = "btn_Query";
            this.btn_Query.Size = new System.Drawing.Size(71, 29);
            this.btn_Query.TabIndex = 2;
            this.btn_Query.Text = "查询(&Q)";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 21);
            this.label1.TabIndex = 0;
            this.label1.Text = "快速查询：";
            // 
            // tabPane1
            // 
            this.tabPane1.Controls.Add(this.tabNavigationPage1);
            this.tabPane1.Controls.Add(this.tabNavigationPage2);
            this.tabPane1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabPane1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabPane1.Location = new System.Drawing.Point(0, 0);
            this.tabPane1.Name = "tabPane1";
            this.tabPane1.PageProperties.ShowMode = DevExpress.XtraBars.Navigation.ItemShowMode.ImageAndText;
            this.tabPane1.Pages.AddRange(new DevExpress.XtraBars.Navigation.NavigationPageBase[] {
            this.tabNavigationPage1,
            this.tabNavigationPage2});
            this.tabPane1.RegularSize = new System.Drawing.Size(1264, 721);
            this.tabPane1.SelectedPage = this.tabNavigationPage1;
            this.tabPane1.SelectedPageIndex = 1;
            this.tabPane1.Size = new System.Drawing.Size(1264, 721);
            this.tabPane1.TabIndex = 17;
            this.tabPane1.Text = "tabPane1";
            // 
            // tabNavigationPage1
            // 
            this.tabNavigationPage1.Appearance.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.tabNavigationPage1.Appearance.Options.UseFont = true;
            this.tabNavigationPage1.Caption = "tabNavigationPage1";
            this.tabNavigationPage1.Controls.Add(this.rdo_ZJ);
            this.tabNavigationPage1.Controls.Add(this.btn_StartCount);
            this.tabNavigationPage1.Controls.Add(this.btn_Exprot);
            this.tabNavigationPage1.Controls.Add(this.rdo_JG);
            this.tabNavigationPage1.Controls.Add(this.countView);
            this.tabNavigationPage1.Controls.Add(this.chk_AllDate);
            this.tabNavigationPage1.Controls.Add(this.dtp_EndDate);
            this.tabNavigationPage1.Controls.Add(this.dtp_StartDate);
            this.tabNavigationPage1.Controls.Add(this.label3);
            this.tabNavigationPage1.Controls.Add(this.label2);
            this.tabNavigationPage1.Controls.Add(this.cbo_UserList);
            this.tabNavigationPage1.Controls.Add(this.label4);
            this.tabNavigationPage1.Image = ((System.Drawing.Image)(resources.GetObject("tabNavigationPage1.Image")));
            this.tabNavigationPage1.ItemShowMode = DevExpress.XtraBars.Navigation.ItemShowMode.ImageAndText;
            this.tabNavigationPage1.Name = "tabNavigationPage1";
            this.tabNavigationPage1.PageText = "工作量统计";
            this.tabNavigationPage1.Properties.ShowMode = DevExpress.XtraBars.Navigation.ItemShowMode.ImageAndText;
            this.tabNavigationPage1.Size = new System.Drawing.Size(1246, 673);
            // 
            // rdo_ZJ
            // 
            this.rdo_ZJ.AutoSize = true;
            this.rdo_ZJ.Location = new System.Drawing.Point(398, 22);
            this.rdo_ZJ.Name = "rdo_ZJ";
            this.rdo_ZJ.Size = new System.Drawing.Size(60, 25);
            this.rdo_ZJ.TabIndex = 25;
            this.rdo_ZJ.Text = "质检";
            this.rdo_ZJ.UseVisualStyleBackColor = true;
            this.rdo_ZJ.CheckedChanged += new System.EventHandler(this.Rdo_ZJ_CheckedChanged);
            // 
            // btn_StartCount
            // 
            this.btn_StartCount.Appearance.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.btn_StartCount.Appearance.Options.UseFont = true;
            this.btn_StartCount.Image = ((System.Drawing.Image)(resources.GetObject("btn_StartCount.Image")));
            this.btn_StartCount.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btn_StartCount.Location = new System.Drawing.Point(944, 69);
            this.btn_StartCount.Name = "btn_StartCount";
            this.btn_StartCount.Size = new System.Drawing.Size(83, 31);
            this.btn_StartCount.TabIndex = 22;
            this.btn_StartCount.Text = "开始统计";
            this.btn_StartCount.Click += new System.EventHandler(this.Btn_StartCount_Click);
            // 
            // btn_Exprot
            // 
            this.btn_Exprot.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btn_Exprot.Appearance.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.btn_Exprot.Appearance.Options.UseFont = true;
            this.btn_Exprot.Image = ((System.Drawing.Image)(resources.GetObject("btn_Exprot.Image")));
            this.btn_Exprot.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btn_Exprot.ImageToTextIndent = 5;
            this.btn_Exprot.Location = new System.Drawing.Point(15, 625);
            this.btn_Exprot.Name = "btn_Exprot";
            this.btn_Exprot.Size = new System.Drawing.Size(86, 34);
            this.btn_Exprot.TabIndex = 21;
            this.btn_Exprot.Text = "导出Excel";
            this.btn_Exprot.Click += new System.EventHandler(this.Btn_Exprot_Click);
            // 
            // rdo_JG
            // 
            this.rdo_JG.AutoSize = true;
            this.rdo_JG.Checked = true;
            this.rdo_JG.Location = new System.Drawing.Point(332, 22);
            this.rdo_JG.Name = "rdo_JG";
            this.rdo_JG.Size = new System.Drawing.Size(60, 25);
            this.rdo_JG.TabIndex = 24;
            this.rdo_JG.TabStop = true;
            this.rdo_JG.Text = "加工";
            this.rdo_JG.UseVisualStyleBackColor = true;
            // 
            // countView
            // 
            this.countView.AllowUserToAddRows = false;
            this.countView.AllowUserToDeleteRows = false;
            dataGridViewCellStyle22.BackColor = System.Drawing.Color.White;
            this.countView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle22;
            this.countView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.countView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.countView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            dataGridViewCellStyle23.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle23.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle23.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle23.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle23.Padding = new System.Windows.Forms.Padding(3);
            dataGridViewCellStyle23.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle23.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle23.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.countView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle23;
            this.countView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.countView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.date,
            this.pcount,
            this.tcount,
            this.fcount,
            this.bcount,
            this.pgcount});
            this.countView.Location = new System.Drawing.Point(9, 133);
            this.countView.Name = "countView";
            this.countView.ReadOnly = true;
            this.countView.RowTemplate.Height = 23;
            this.countView.Size = new System.Drawing.Size(1228, 477);
            this.countView.TabIndex = 20;
            // 
            // date
            // 
            this.date.HeaderText = "工作时间";
            this.date.Name = "date";
            this.date.ReadOnly = true;
            this.date.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // pcount
            // 
            dataGridViewCellStyle24.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.pcount.DefaultCellStyle = dataGridViewCellStyle24;
            this.pcount.HeaderText = "项目/课题数";
            this.pcount.Name = "pcount";
            this.pcount.ReadOnly = true;
            this.pcount.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // tcount
            // 
            dataGridViewCellStyle25.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.tcount.DefaultCellStyle = dataGridViewCellStyle25;
            this.tcount.HeaderText = "课题/子课题数";
            this.tcount.Name = "tcount";
            this.tcount.ReadOnly = true;
            this.tcount.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // fcount
            // 
            dataGridViewCellStyle26.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.fcount.DefaultCellStyle = dataGridViewCellStyle26;
            this.fcount.HeaderText = "文件数";
            this.fcount.Name = "fcount";
            this.fcount.ReadOnly = true;
            this.fcount.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // bcount
            // 
            dataGridViewCellStyle27.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.bcount.DefaultCellStyle = dataGridViewCellStyle27;
            this.bcount.HeaderText = "被返工数";
            this.bcount.Name = "bcount";
            this.bcount.ReadOnly = true;
            this.bcount.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // pgcount
            // 
            dataGridViewCellStyle28.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.pgcount.DefaultCellStyle = dataGridViewCellStyle28;
            this.pgcount.FillWeight = 40F;
            this.pgcount.HeaderText = "页数";
            this.pgcount.Name = "pgcount";
            this.pgcount.ReadOnly = true;
            this.pgcount.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // chk_AllDate
            // 
            this.chk_AllDate.AutoSize = true;
            this.chk_AllDate.Location = new System.Drawing.Point(593, 72);
            this.chk_AllDate.Name = "chk_AllDate";
            this.chk_AllDate.Size = new System.Drawing.Size(93, 25);
            this.chk_AllDate.TabIndex = 19;
            this.chk_AllDate.Text = "全部时间";
            this.chk_AllDate.UseVisualStyleBackColor = true;
            this.chk_AllDate.CheckedChanged += new System.EventHandler(this.chk_AllDate_CheckedChanged);
            // 
            // dtp_EndDate
            // 
            this.dtp_EndDate.CustomFormat = "yyyy-MM-dd";
            this.dtp_EndDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtp_EndDate.Location = new System.Drawing.Point(332, 70);
            this.dtp_EndDate.Name = "dtp_EndDate";
            this.dtp_EndDate.Size = new System.Drawing.Size(136, 29);
            this.dtp_EndDate.TabIndex = 18;
            // 
            // dtp_StartDate
            // 
            this.dtp_StartDate.CustomFormat = "yyyy-MM-dd";
            this.dtp_StartDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtp_StartDate.Location = new System.Drawing.Point(142, 70);
            this.dtp_StartDate.Name = "dtp_StartDate";
            this.dtp_StartDate.Size = new System.Drawing.Size(136, 29);
            this.dtp_StartDate.TabIndex = 17;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(294, 73);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(22, 22);
            this.label3.TabIndex = 16;
            this.label3.Text = "~";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(46, 73);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(90, 22);
            this.label2.TabIndex = 15;
            this.label2.Text = "加工时间：";
            // 
            // cbo_UserList
            // 
            this.cbo_UserList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo_UserList.FormattingEnabled = true;
            this.cbo_UserList.Location = new System.Drawing.Point(142, 20);
            this.cbo_UserList.Name = "cbo_UserList";
            this.cbo_UserList.Size = new System.Drawing.Size(136, 29);
            this.cbo_UserList.TabIndex = 14;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(46, 23);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(90, 22);
            this.label4.TabIndex = 13;
            this.label4.Text = "用户选择：";
            // 
            // tabNavigationPage2
            // 
            this.tabNavigationPage2.Appearance.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.tabNavigationPage2.Appearance.Options.UseFont = true;
            this.tabNavigationPage2.Caption = "tabNavigationPage2";
            this.tabNavigationPage2.Controls.Add(this.view);
            this.tabNavigationPage2.Controls.Add(this.panel1);
            this.tabNavigationPage2.Controls.Add(this.ac_LeftMenu);
            this.tabNavigationPage2.Image = ((System.Drawing.Image)(resources.GetObject("tabNavigationPage2.Image")));
            this.tabNavigationPage2.ItemShowMode = DevExpress.XtraBars.Navigation.ItemShowMode.ImageAndText;
            this.tabNavigationPage2.Name = "tabNavigationPage2";
            this.tabNavigationPage2.PageText = "档案统计";
            this.tabNavigationPage2.Properties.ShowMode = DevExpress.XtraBars.Navigation.ItemShowMode.ImageAndText;
            this.tabNavigationPage2.Size = new System.Drawing.Size(1246, 673);
            // 
            // pName
            // 
            this.pName.FillWeight = 40F;
            this.pName.HeaderText = "计划类别名称";
            this.pName.Name = "pName";
            this.pName.ReadOnly = true;
            this.pName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // pAmount
            // 
            this.pAmount.FillWeight = 20F;
            this.pAmount.HeaderText = "项目/课题名称";
            this.pAmount.Name = "pAmount";
            this.pAmount.ReadOnly = true;
            this.pAmount.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // fAmount
            // 
            this.fAmount.FillWeight = 20F;
            this.fAmount.HeaderText = "文件数";
            this.fAmount.Name = "fAmount";
            this.fAmount.ReadOnly = true;
            this.fAmount.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // bAmount
            // 
            this.bAmount.FillWeight = 20F;
            this.bAmount.HeaderText = "盒数";
            this.bAmount.Name = "bAmount";
            this.bAmount.ReadOnly = true;
            this.bAmount.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Frm_Statistics
            // 
            this.Appearance.Options.UseFont = true;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1264, 721);
            this.Controls.Add(this.tabPane1);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "Frm_Statistics";
            this.Text = "统计分析";
            this.Load += new System.EventHandler(this.Frm_Statistics_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ac_LeftMenu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.view)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.searchControl.Properties)).EndInit();
            this.tabPane1.ResumeLayout(false);
            this.tabNavigationPage1.ResumeLayout(false);
            this.tabNavigationPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.countView)).EndInit();
            this.tabNavigationPage2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraBars.Navigation.AccordionControl ac_LeftMenu;
        private DevExpress.XtraBars.Navigation.AccordionControlElement acg_Register;
        private DevExpress.XtraBars.Navigation.AccordionControlElement ace_all;
        private System.Windows.Forms.DataGridView view;
        private System.Windows.Forms.Panel panel1;
        private DevExpress.XtraEditors.SimpleButton btn_Query;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraEditors.SearchControl searchControl;
        private DevExpress.XtraBars.Navigation.TabPane tabPane1;
        private DevExpress.XtraBars.Navigation.TabNavigationPage tabNavigationPage1;
        private DevExpress.XtraBars.Navigation.TabNavigationPage tabNavigationPage2;
        private System.Windows.Forms.RadioButton rdo_ZJ;
        private KyoControl.KyoButton btn_StartCount;
        private KyoControl.KyoButton btn_Exprot;
        private System.Windows.Forms.RadioButton rdo_JG;
        private System.Windows.Forms.DataGridView countView;
        private System.Windows.Forms.CheckBox chk_AllDate;
        private System.Windows.Forms.DateTimePicker dtp_EndDate;
        private System.Windows.Forms.DateTimePicker dtp_StartDate;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbo_UserList;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.DataGridViewTextBoxColumn date;
        private System.Windows.Forms.DataGridViewTextBoxColumn pcount;
        private System.Windows.Forms.DataGridViewTextBoxColumn tcount;
        private System.Windows.Forms.DataGridViewTextBoxColumn fcount;
        private System.Windows.Forms.DataGridViewTextBoxColumn bcount;
        private System.Windows.Forms.DataGridViewTextBoxColumn pgcount;
        private System.Windows.Forms.DataGridViewTextBoxColumn pName;
        private System.Windows.Forms.DataGridViewTextBoxColumn pAmount;
        private System.Windows.Forms.DataGridViewTextBoxColumn fAmount;
        private System.Windows.Forms.DataGridViewTextBoxColumn bAmount;
    }
}