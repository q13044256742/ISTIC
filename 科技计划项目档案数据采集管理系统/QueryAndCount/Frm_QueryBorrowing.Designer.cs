namespace 科技计划项目档案数据采集管理系统
{
    partial class Frm_QueryBorrowing
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_QueryBorrowing));
            this.navigationPane1 = new DevExpress.XtraBars.Navigation.NavigationPane();
            this.navigationPage1 = new DevExpress.XtraBars.Navigation.NavigationPage();
            this.chk_allDate = new System.Windows.Forms.CheckBox();
            this.dtp_eDate = new System.Windows.Forms.DateTimePicker();
            this.dtp_sDate = new System.Windows.Forms.DateTimePicker();
            this.cbo_PlanTypeList = new System.Windows.Forms.ComboBox();
            this.labelControl8 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.txt_ProjectName = new DevExpress.XtraEditors.TextEdit();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.txt_ProjectCode = new DevExpress.XtraEditors.TextEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.txt_BatchName = new DevExpress.XtraEditors.TextEdit();
            this.panel3 = new System.Windows.Forms.Panel();
            this.treeList1 = new DevExpress.XtraTreeList.TreeList();
            this.pal1 = new System.Windows.Forms.Panel();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.panel4 = new System.Windows.Forms.Panel();
            this.txt_page = new DevExpress.XtraEditors.TextEdit();
            this.btn_lpage = new DevExpress.XtraEditors.SimpleButton();
            this.btn_npage = new DevExpress.XtraEditors.SimpleButton();
            this.btn_epage = new DevExpress.XtraEditors.SimpleButton();
            this.btn_fpage = new DevExpress.XtraEditors.SimpleButton();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_Reset = new 科技计划项目档案数据采集管理系统.KyoControl.KyoButton();
            this.btn_Query = new 科技计划项目档案数据采集管理系统.KyoControl.KyoButton();
            this.navigationPage2 = new DevExpress.XtraBars.Navigation.NavigationPage();
            this.rdo_All = new System.Windows.Forms.RadioButton();
            this.rdo_In = new System.Windows.Forms.RadioButton();
            this.rdo_Out = new System.Windows.Forms.RadioButton();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.txt_FileName = new DevExpress.XtraEditors.TextEdit();
            this.labelControl10 = new DevExpress.XtraEditors.LabelControl();
            this.txt_Pname = new DevExpress.XtraEditors.TextEdit();
            this.labelControl11 = new DevExpress.XtraEditors.LabelControl();
            this.txt_Pcode = new DevExpress.XtraEditors.TextEdit();
            this.labelControl12 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl13 = new DevExpress.XtraEditors.LabelControl();
            this.txt_FileCategor = new DevExpress.XtraEditors.TextEdit();
            this.panel1 = new System.Windows.Forms.Panel();
            this.view2 = new System.Windows.Forms.DataGridView();
            this.fid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fcode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fname = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fbox = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fbstate = new System.Windows.Forms.DataGridViewButtonColumn();
            this.frstate = new System.Windows.Forms.DataGridViewButtonColumn();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lbl_TotalFileAmount = new DevExpress.XtraEditors.LabelControl();
            this.labelControl14 = new DevExpress.XtraEditors.LabelControl();
            this.btn_FileReset = new 科技计划项目档案数据采集管理系统.KyoControl.KyoButton();
            this.btn_FileQuery = new 科技计划项目档案数据采集管理系统.KyoControl.KyoButton();
            this.navigationPane1.SuspendLayout();
            this.navigationPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txt_ProjectName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_ProjectCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_BatchName.Properties)).BeginInit();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treeList1)).BeginInit();
            this.pal1.SuspendLayout();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txt_page.Properties)).BeginInit();
            this.navigationPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txt_FileName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_Pname.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_Pcode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_FileCategor.Properties)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.view2)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // navigationPane1
            // 
            this.navigationPane1.AllowTransitionAnimation = DevExpress.Utils.DefaultBoolean.False;
            this.navigationPane1.AppearanceButton.Hovered.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.navigationPane1.AppearanceButton.Hovered.Options.UseFont = true;
            this.navigationPane1.AppearanceButton.Normal.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.navigationPane1.AppearanceButton.Normal.Options.UseFont = true;
            this.navigationPane1.AppearanceButton.Pressed.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.navigationPane1.AppearanceButton.Pressed.Options.UseFont = true;
            this.navigationPane1.Controls.Add(this.navigationPage1);
            this.navigationPane1.Controls.Add(this.navigationPage2);
            this.navigationPane1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.navigationPane1.Location = new System.Drawing.Point(0, 0);
            this.navigationPane1.Margin = new System.Windows.Forms.Padding(5);
            this.navigationPane1.Name = "navigationPane1";
            this.navigationPane1.Padding = new System.Windows.Forms.Padding(5);
            this.navigationPane1.Pages.AddRange(new DevExpress.XtraBars.Navigation.NavigationPageBase[] {
            this.navigationPage1,
            this.navigationPage2});
            this.navigationPane1.RegularSize = new System.Drawing.Size(1228, 749);
            this.navigationPane1.SelectedPage = this.navigationPage1;
            this.navigationPane1.SelectedPageIndex = 1;
            this.navigationPane1.Size = new System.Drawing.Size(1228, 749);
            this.navigationPane1.TabIndex = 2;
            this.navigationPane1.StateChanged += new DevExpress.XtraBars.Navigation.StateChangedEventHandler(this.navigationPane1_StateChanged);
            // 
            // navigationPage1
            // 
            this.navigationPage1.AlwaysScrollActiveControlIntoView = false;
            this.navigationPage1.Appearance.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.navigationPage1.Appearance.Options.UseFont = true;
            this.navigationPage1.BackgroundPadding = new System.Windows.Forms.Padding(0);
            this.navigationPage1.Caption = "高级检索";
            this.navigationPage1.Controls.Add(this.chk_allDate);
            this.navigationPage1.Controls.Add(this.dtp_eDate);
            this.navigationPage1.Controls.Add(this.dtp_sDate);
            this.navigationPage1.Controls.Add(this.cbo_PlanTypeList);
            this.navigationPage1.Controls.Add(this.labelControl8);
            this.navigationPage1.Controls.Add(this.labelControl7);
            this.navigationPage1.Controls.Add(this.labelControl6);
            this.navigationPage1.Controls.Add(this.txt_ProjectName);
            this.navigationPage1.Controls.Add(this.labelControl5);
            this.navigationPage1.Controls.Add(this.txt_ProjectCode);
            this.navigationPage1.Controls.Add(this.labelControl4);
            this.navigationPage1.Controls.Add(this.labelControl3);
            this.navigationPage1.Controls.Add(this.txt_BatchName);
            this.navigationPage1.Controls.Add(this.panel3);
            this.navigationPage1.Controls.Add(this.btn_Reset);
            this.navigationPage1.Controls.Add(this.btn_Query);
            this.navigationPage1.CustomHeaderButtons.AddRange(new DevExpress.XtraBars.Docking2010.IButton[] {
            new DevExpress.XtraBars.Docking.CustomHeaderButton("", ((System.Drawing.Image)(resources.GetObject("navigationPage1.CustomHeaderButtons"))), -1, DevExpress.XtraBars.Docking2010.ButtonStyle.CheckButton, -1)});
            this.navigationPage1.FireScrollEventOnMouseWheel = true;
            this.navigationPage1.Image = ((System.Drawing.Image)(resources.GetObject("navigationPage1.Image")));
            this.navigationPage1.Name = "navigationPage1";
            this.navigationPage1.PageText = "数据查询";
            this.navigationPage1.Properties.AppearanceCaption.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.navigationPage1.Properties.AppearanceCaption.Image = ((System.Drawing.Image)(resources.GetObject("navigationPage1.Properties.AppearanceCaption.Image")));
            this.navigationPage1.Properties.AppearanceCaption.Options.UseFont = true;
            this.navigationPage1.Properties.AppearanceCaption.Options.UseImage = true;
            this.navigationPage1.Properties.ShowCollapseButton = DevExpress.Utils.DefaultBoolean.False;
            this.navigationPage1.Properties.ShowExpandButton = DevExpress.Utils.DefaultBoolean.False;
            this.navigationPage1.Properties.ShowMode = DevExpress.XtraBars.Navigation.ItemShowMode.ImageAndText;
            this.navigationPage1.Size = new System.Drawing.Size(1112, 708);
            // 
            // chk_allDate
            // 
            this.chk_allDate.AutoSize = true;
            this.chk_allDate.Checked = true;
            this.chk_allDate.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chk_allDate.Location = new System.Drawing.Point(466, 121);
            this.chk_allDate.Name = "chk_allDate";
            this.chk_allDate.Size = new System.Drawing.Size(93, 25);
            this.chk_allDate.TabIndex = 23;
            this.chk_allDate.Text = "全部时间";
            this.chk_allDate.UseVisualStyleBackColor = true;
            this.chk_allDate.CheckedChanged += new System.EventHandler(this.chk_allDate_CheckedChanged);
            // 
            // dtp_eDate
            // 
            this.dtp_eDate.CustomFormat = "yyyy-MM-dd";
            this.dtp_eDate.Enabled = false;
            this.dtp_eDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtp_eDate.Location = new System.Drawing.Point(287, 119);
            this.dtp_eDate.Name = "dtp_eDate";
            this.dtp_eDate.Size = new System.Drawing.Size(114, 29);
            this.dtp_eDate.TabIndex = 22;
            // 
            // dtp_sDate
            // 
            this.dtp_sDate.CustomFormat = "yyyy-MM-dd";
            this.dtp_sDate.Enabled = false;
            this.dtp_sDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtp_sDate.Location = new System.Drawing.Point(143, 119);
            this.dtp_sDate.Name = "dtp_sDate";
            this.dtp_sDate.Size = new System.Drawing.Size(114, 29);
            this.dtp_sDate.TabIndex = 21;
            // 
            // cbo_PlanTypeList
            // 
            this.cbo_PlanTypeList.FormattingEnabled = true;
            this.cbo_PlanTypeList.Location = new System.Drawing.Point(143, 15);
            this.cbo_PlanTypeList.Name = "cbo_PlanTypeList";
            this.cbo_PlanTypeList.Size = new System.Drawing.Size(292, 29);
            this.cbo_PlanTypeList.TabIndex = 18;
            // 
            // labelControl8
            // 
            this.labelControl8.Appearance.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl8.Location = new System.Drawing.Point(266, 123);
            this.labelControl8.Name = "labelControl8";
            this.labelControl8.Size = new System.Drawing.Size(12, 21);
            this.labelControl8.TabIndex = 13;
            this.labelControl8.Text = "~";
            // 
            // labelControl7
            // 
            this.labelControl7.Appearance.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl7.Location = new System.Drawing.Point(50, 123);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(80, 21);
            this.labelControl7.TabIndex = 10;
            this.labelControl7.Text = "时间范围：";
            // 
            // labelControl6
            // 
            this.labelControl6.Appearance.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl6.Location = new System.Drawing.Point(466, 71);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(119, 21);
            this.labelControl6.TabIndex = 9;
            this.labelControl6.Text = "项目/课题名称：";
            // 
            // txt_ProjectName
            // 
            this.txt_ProjectName.EditValue = "";
            this.txt_ProjectName.Location = new System.Drawing.Point(595, 67);
            this.txt_ProjectName.Name = "txt_ProjectName";
            this.txt_ProjectName.Properties.Appearance.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_ProjectName.Properties.Appearance.Options.UseFont = true;
            this.txt_ProjectName.Size = new System.Drawing.Size(292, 28);
            this.txt_ProjectName.TabIndex = 8;
            // 
            // labelControl5
            // 
            this.labelControl5.Appearance.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl5.Location = new System.Drawing.Point(11, 71);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(119, 21);
            this.labelControl5.TabIndex = 7;
            this.labelControl5.Text = "项目/课题编号：";
            // 
            // txt_ProjectCode
            // 
            this.txt_ProjectCode.EditValue = "";
            this.txt_ProjectCode.Location = new System.Drawing.Point(143, 67);
            this.txt_ProjectCode.Name = "txt_ProjectCode";
            this.txt_ProjectCode.Properties.Appearance.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_ProjectCode.Properties.Appearance.Options.UseFont = true;
            this.txt_ProjectCode.Size = new System.Drawing.Size(292, 28);
            this.txt_ProjectCode.TabIndex = 6;
            // 
            // labelControl4
            // 
            this.labelControl4.Appearance.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl4.Location = new System.Drawing.Point(505, 19);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(80, 21);
            this.labelControl4.TabIndex = 5;
            this.labelControl4.Text = "批次名称：";
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl3.Location = new System.Drawing.Point(50, 19);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(80, 21);
            this.labelControl3.TabIndex = 3;
            this.labelControl3.Text = "计划类别：";
            // 
            // txt_BatchName
            // 
            this.txt_BatchName.EditValue = "";
            this.txt_BatchName.Location = new System.Drawing.Point(595, 15);
            this.txt_BatchName.Name = "txt_BatchName";
            this.txt_BatchName.Properties.Appearance.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_BatchName.Properties.Appearance.Options.UseFont = true;
            this.txt_BatchName.Size = new System.Drawing.Size(290, 28);
            this.txt_BatchName.TabIndex = 2;
            // 
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel3.Controls.Add(this.treeList1);
            this.panel3.Controls.Add(this.pal1);
            this.panel3.Controls.Add(this.panel4);
            this.panel3.Location = new System.Drawing.Point(0, 205);
            this.panel3.Margin = new System.Windows.Forms.Padding(0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1336, 582);
            this.panel3.TabIndex = 1;
            // 
            // treeList1
            // 
            this.treeList1.Appearance.HeaderPanel.Font = new System.Drawing.Font("华文中宋", 12F, System.Drawing.FontStyle.Bold);
            this.treeList1.Appearance.HeaderPanel.Options.UseFont = true;
            this.treeList1.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.treeList1.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.treeList1.Appearance.Row.Font = new System.Drawing.Font("华文中宋", 14F);
            this.treeList1.Appearance.Row.Options.UseFont = true;
            this.treeList1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeList1.Location = new System.Drawing.Point(0, 30);
            this.treeList1.Name = "treeList1";
            this.treeList1.OptionsBehavior.Editable = false;
            this.treeList1.OptionsCustomization.AllowBandMoving = false;
            this.treeList1.OptionsCustomization.AllowColumnMoving = false;
            this.treeList1.Size = new System.Drawing.Size(1336, 519);
            this.treeList1.TabIndex = 4;
            this.treeList1.NodeCellStyle += new DevExpress.XtraTreeList.GetCustomNodeCellStyleEventHandler(this.treeList1_NodeCellStyle);
            this.treeList1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.treeList1_MouseDoubleClick);
            // 
            // pal1
            // 
            this.pal1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.pal1.Controls.Add(this.labelControl2);
            this.pal1.Dock = System.Windows.Forms.DockStyle.Top;
            this.pal1.Location = new System.Drawing.Point(0, 0);
            this.pal1.Name = "pal1";
            this.pal1.Size = new System.Drawing.Size(1336, 30);
            this.pal1.TabIndex = 0;
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl2.Appearance.Image = ((System.Drawing.Image)(resources.GetObject("labelControl2.Appearance.Image")));
            this.labelControl2.Appearance.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.labelControl2.ImageAlignToText = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.labelControl2.Location = new System.Drawing.Point(4, 5);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(85, 21);
            this.labelControl2.TabIndex = 0;
            this.labelControl2.Text = "项目列表";
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.txt_page);
            this.panel4.Controls.Add(this.btn_lpage);
            this.panel4.Controls.Add(this.btn_npage);
            this.panel4.Controls.Add(this.btn_epage);
            this.panel4.Controls.Add(this.btn_fpage);
            this.panel4.Controls.Add(this.label1);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel4.Location = new System.Drawing.Point(0, 549);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(1336, 33);
            this.panel4.TabIndex = 3;
            // 
            // txt_page
            // 
            this.txt_page.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txt_page.Location = new System.Drawing.Point(1127, 3);
            this.txt_page.Name = "txt_page";
            this.txt_page.Properties.Appearance.Font = new System.Drawing.Font("微软雅黑", 10.5F);
            this.txt_page.Properties.Appearance.Options.UseFont = true;
            this.txt_page.Properties.Appearance.Options.UseTextOptions = true;
            this.txt_page.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.txt_page.Properties.Mask.EditMask = "d";
            this.txt_page.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.txt_page.Size = new System.Drawing.Size(33, 26);
            this.txt_page.TabIndex = 7;
            this.txt_page.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Txt_page_KeyDown);
            // 
            // btn_lpage
            // 
            this.btn_lpage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_lpage.Appearance.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_lpage.Appearance.Options.UseFont = true;
            this.btn_lpage.Image = ((System.Drawing.Image)(resources.GetObject("btn_lpage.Image")));
            this.btn_lpage.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btn_lpage.Location = new System.Drawing.Point(1081, 5);
            this.btn_lpage.Name = "btn_lpage";
            this.btn_lpage.Size = new System.Drawing.Size(41, 23);
            this.btn_lpage.TabIndex = 6;
            this.btn_lpage.Click += new System.EventHandler(this.Btn_Page_Click);
            // 
            // btn_npage
            // 
            this.btn_npage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_npage.Appearance.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_npage.Appearance.Options.UseFont = true;
            this.btn_npage.Image = ((System.Drawing.Image)(resources.GetObject("btn_npage.Image")));
            this.btn_npage.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btn_npage.Location = new System.Drawing.Point(1165, 5);
            this.btn_npage.Name = "btn_npage";
            this.btn_npage.Size = new System.Drawing.Size(41, 23);
            this.btn_npage.TabIndex = 5;
            this.btn_npage.Click += new System.EventHandler(this.Btn_Page_Click);
            // 
            // btn_epage
            // 
            this.btn_epage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_epage.Appearance.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_epage.Appearance.Options.UseFont = true;
            this.btn_epage.Image = ((System.Drawing.Image)(resources.GetObject("btn_epage.Image")));
            this.btn_epage.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btn_epage.Location = new System.Drawing.Point(1208, 5);
            this.btn_epage.Name = "btn_epage";
            this.btn_epage.Size = new System.Drawing.Size(41, 23);
            this.btn_epage.TabIndex = 4;
            this.btn_epage.Click += new System.EventHandler(this.Btn_Page_Click);
            // 
            // btn_fpage
            // 
            this.btn_fpage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_fpage.Appearance.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_fpage.Appearance.Options.UseFont = true;
            this.btn_fpage.Image = ((System.Drawing.Image)(resources.GetObject("btn_fpage.Image")));
            this.btn_fpage.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btn_fpage.Location = new System.Drawing.Point(1038, 5);
            this.btn_fpage.Name = "btn_fpage";
            this.btn_fpage.Size = new System.Drawing.Size(41, 23);
            this.btn_fpage.TabIndex = 3;
            this.btn_fpage.Click += new System.EventHandler(this.Btn_Page_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(275, 21);
            this.label1.TabIndex = 2;
            this.label1.Text = "总计 0 条记录，每页共 0 条，共 0 页";
            // 
            // btn_Reset
            // 
            this.btn_Reset.Appearance.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Reset.Appearance.Options.UseFont = true;
            this.btn_Reset.Image = ((System.Drawing.Image)(resources.GetObject("btn_Reset.Image")));
            this.btn_Reset.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btn_Reset.Location = new System.Drawing.Point(809, 153);
            this.btn_Reset.Name = "btn_Reset";
            this.btn_Reset.Size = new System.Drawing.Size(67, 32);
            this.btn_Reset.TabIndex = 17;
            this.btn_Reset.Text = "重置";
            this.btn_Reset.Click += new System.EventHandler(this.Btn_Reset_Click);
            // 
            // btn_Query
            // 
            this.btn_Query.Appearance.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.btn_Query.Appearance.Options.UseFont = true;
            this.btn_Query.Image = ((System.Drawing.Image)(resources.GetObject("btn_Query.Image")));
            this.btn_Query.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btn_Query.Location = new System.Drawing.Point(704, 153);
            this.btn_Query.Name = "btn_Query";
            this.btn_Query.Size = new System.Drawing.Size(99, 32);
            this.btn_Query.TabIndex = 16;
            this.btn_Query.Text = "立即查询";
            this.btn_Query.Click += new System.EventHandler(this.LoadDataListByPage);
            // 
            // navigationPage2
            // 
            this.navigationPage2.Appearance.Font = new System.Drawing.Font("微软雅黑", 10.5F);
            this.navigationPage2.Appearance.Options.UseFont = true;
            this.navigationPage2.AutoSize = true;
            this.navigationPage2.BackgroundPadding = new System.Windows.Forms.Padding(0);
            this.navigationPage2.Caption = "高级检索";
            this.navigationPage2.Controls.Add(this.rdo_All);
            this.navigationPage2.Controls.Add(this.rdo_In);
            this.navigationPage2.Controls.Add(this.rdo_Out);
            this.navigationPage2.Controls.Add(this.labelControl1);
            this.navigationPage2.Controls.Add(this.txt_FileName);
            this.navigationPage2.Controls.Add(this.labelControl10);
            this.navigationPage2.Controls.Add(this.txt_Pname);
            this.navigationPage2.Controls.Add(this.labelControl11);
            this.navigationPage2.Controls.Add(this.txt_Pcode);
            this.navigationPage2.Controls.Add(this.labelControl12);
            this.navigationPage2.Controls.Add(this.labelControl13);
            this.navigationPage2.Controls.Add(this.txt_FileCategor);
            this.navigationPage2.Controls.Add(this.panel1);
            this.navigationPage2.Controls.Add(this.btn_FileReset);
            this.navigationPage2.Controls.Add(this.btn_FileQuery);
            this.navigationPage2.CustomHeaderButtons.AddRange(new DevExpress.XtraBars.Docking2010.IButton[] {
            new DevExpress.XtraBars.Docking.CustomHeaderButton("", ((System.Drawing.Image)(resources.GetObject("navigationPage2.CustomHeaderButtons"))), -1, DevExpress.XtraBars.Docking2010.ButtonStyle.CheckButton, -1)});
            this.navigationPage2.Image = ((System.Drawing.Image)(resources.GetObject("navigationPage2.Image")));
            this.navigationPage2.Margin = new System.Windows.Forms.Padding(5);
            this.navigationPage2.Name = "navigationPage2";
            this.navigationPage2.Padding = new System.Windows.Forms.Padding(5);
            this.navigationPage2.PageText = "档案借阅";
            this.navigationPage2.Properties.AppearanceCaption.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.navigationPage2.Properties.AppearanceCaption.Image = ((System.Drawing.Image)(resources.GetObject("navigationPage2.Properties.AppearanceCaption.Image")));
            this.navigationPage2.Properties.AppearanceCaption.Options.UseFont = true;
            this.navigationPage2.Properties.AppearanceCaption.Options.UseImage = true;
            this.navigationPage2.Properties.ShowCollapseButton = DevExpress.Utils.DefaultBoolean.False;
            this.navigationPage2.Properties.ShowExpandButton = DevExpress.Utils.DefaultBoolean.False;
            this.navigationPage2.Properties.ShowMode = DevExpress.XtraBars.Navigation.ItemShowMode.ImageAndText;
            this.navigationPage2.Size = new System.Drawing.Size(2512, 708);
            // 
            // rdo_All
            // 
            this.rdo_All.AutoSize = true;
            this.rdo_All.Checked = true;
            this.rdo_All.Location = new System.Drawing.Point(138, 111);
            this.rdo_All.Name = "rdo_All";
            this.rdo_All.Size = new System.Drawing.Size(60, 25);
            this.rdo_All.TabIndex = 38;
            this.rdo_All.TabStop = true;
            this.rdo_All.Text = "全部";
            this.rdo_All.UseVisualStyleBackColor = true;
            // 
            // rdo_In
            // 
            this.rdo_In.AutoSize = true;
            this.rdo_In.Location = new System.Drawing.Point(270, 111);
            this.rdo_In.Name = "rdo_In";
            this.rdo_In.Size = new System.Drawing.Size(60, 25);
            this.rdo_In.TabIndex = 37;
            this.rdo_In.Text = "在库";
            this.rdo_In.UseVisualStyleBackColor = true;
            // 
            // rdo_Out
            // 
            this.rdo_Out.AutoSize = true;
            this.rdo_Out.Location = new System.Drawing.Point(204, 111);
            this.rdo_Out.Name = "rdo_Out";
            this.rdo_Out.Size = new System.Drawing.Size(60, 25);
            this.rdo_Out.TabIndex = 36;
            this.rdo_Out.Text = "借出";
            this.rdo_Out.UseVisualStyleBackColor = true;
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Location = new System.Drawing.Point(48, 113);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(80, 21);
            this.labelControl1.TabIndex = 35;
            this.labelControl1.Text = "借阅状态：";
            // 
            // txt_FileName
            // 
            this.txt_FileName.EditValue = "";
            this.txt_FileName.Location = new System.Drawing.Point(138, 15);
            this.txt_FileName.Name = "txt_FileName";
            this.txt_FileName.Properties.Appearance.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_FileName.Properties.Appearance.Options.UseFont = true;
            this.txt_FileName.Size = new System.Drawing.Size(292, 28);
            this.txt_FileName.TabIndex = 33;
            // 
            // labelControl10
            // 
            this.labelControl10.Appearance.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl10.Location = new System.Drawing.Point(494, 66);
            this.labelControl10.Name = "labelControl10";
            this.labelControl10.Size = new System.Drawing.Size(119, 21);
            this.labelControl10.TabIndex = 26;
            this.labelControl10.Text = "项目/课题名称：";
            // 
            // txt_Pname
            // 
            this.txt_Pname.EditValue = "";
            this.txt_Pname.Location = new System.Drawing.Point(623, 62);
            this.txt_Pname.Name = "txt_Pname";
            this.txt_Pname.Properties.Appearance.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_Pname.Properties.Appearance.Options.UseFont = true;
            this.txt_Pname.Size = new System.Drawing.Size(292, 28);
            this.txt_Pname.TabIndex = 25;
            // 
            // labelControl11
            // 
            this.labelControl11.Appearance.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl11.Location = new System.Drawing.Point(9, 66);
            this.labelControl11.Name = "labelControl11";
            this.labelControl11.Size = new System.Drawing.Size(119, 21);
            this.labelControl11.TabIndex = 24;
            this.labelControl11.Text = "项目/课题编号：";
            // 
            // txt_Pcode
            // 
            this.txt_Pcode.EditValue = "";
            this.txt_Pcode.Location = new System.Drawing.Point(138, 62);
            this.txt_Pcode.Name = "txt_Pcode";
            this.txt_Pcode.Properties.Appearance.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_Pcode.Properties.Appearance.Options.UseFont = true;
            this.txt_Pcode.Size = new System.Drawing.Size(292, 28);
            this.txt_Pcode.TabIndex = 23;
            // 
            // labelControl12
            // 
            this.labelControl12.Appearance.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl12.Location = new System.Drawing.Point(533, 19);
            this.labelControl12.Name = "labelControl12";
            this.labelControl12.Size = new System.Drawing.Size(80, 21);
            this.labelControl12.TabIndex = 22;
            this.labelControl12.Text = "文件类别：";
            // 
            // labelControl13
            // 
            this.labelControl13.Appearance.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl13.Location = new System.Drawing.Point(48, 19);
            this.labelControl13.Name = "labelControl13";
            this.labelControl13.Size = new System.Drawing.Size(80, 21);
            this.labelControl13.TabIndex = 20;
            this.labelControl13.Text = "文件名称：";
            // 
            // txt_FileCategor
            // 
            this.txt_FileCategor.EditValue = "";
            this.txt_FileCategor.Location = new System.Drawing.Point(623, 15);
            this.txt_FileCategor.Name = "txt_FileCategor";
            this.txt_FileCategor.Properties.Appearance.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_FileCategor.Properties.Appearance.Options.UseFont = true;
            this.txt_FileCategor.Size = new System.Drawing.Size(292, 28);
            this.txt_FileCategor.TabIndex = 19;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.view2);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Location = new System.Drawing.Point(5, 196);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(5009, 1190);
            this.panel1.TabIndex = 18;
            // 
            // view2
            // 
            this.view2.AllowUserToAddRows = false;
            this.view2.AllowUserToDeleteRows = false;
            this.view2.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.view2.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.view2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.view2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.view2.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.fid,
            this.fcode,
            this.fname,
            this.fbox,
            this.fbstate,
            this.frstate});
            this.view2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.view2.Location = new System.Drawing.Point(0, 30);
            this.view2.Name = "view2";
            this.view2.ReadOnly = true;
            this.view2.RowTemplate.Height = 23;
            this.view2.Size = new System.Drawing.Size(5009, 1160);
            this.view2.TabIndex = 1;
            this.view2.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.View2_CellContentClick);
            // 
            // fid
            // 
            this.fid.FillWeight = 30F;
            this.fid.HeaderText = "序号";
            this.fid.Name = "fid";
            this.fid.ReadOnly = true;
            this.fid.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // fcode
            // 
            this.fcode.FillWeight = 60F;
            this.fcode.HeaderText = "文件编号";
            this.fcode.Name = "fcode";
            this.fcode.ReadOnly = true;
            this.fcode.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // fname
            // 
            this.fname.HeaderText = "文件名称";
            this.fname.Name = "fname";
            this.fname.ReadOnly = true;
            this.fname.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // fbox
            // 
            this.fbox.FillWeight = 50F;
            this.fbox.HeaderText = "盒号";
            this.fbox.Name = "fbox";
            this.fbox.ReadOnly = true;
            this.fbox.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // fbstate
            // 
            this.fbstate.FillWeight = 40F;
            this.fbstate.HeaderText = "借阅状态";
            this.fbstate.Name = "fbstate";
            this.fbstate.ReadOnly = true;
            this.fbstate.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.fbstate.Text = "";
            // 
            // frstate
            // 
            this.frstate.FillWeight = 40F;
            this.frstate.HeaderText = "归还状态";
            this.frstate.Name = "frstate";
            this.frstate.ReadOnly = true;
            this.frstate.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.frstate.Text = "";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panel2.Controls.Add(this.lbl_TotalFileAmount);
            this.panel2.Controls.Add(this.labelControl14);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(5009, 30);
            this.panel2.TabIndex = 0;
            // 
            // lbl_TotalFileAmount
            // 
            this.lbl_TotalFileAmount.Appearance.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.lbl_TotalFileAmount.Location = new System.Drawing.Point(961, 5);
            this.lbl_TotalFileAmount.Name = "lbl_TotalFileAmount";
            this.lbl_TotalFileAmount.Size = new System.Drawing.Size(105, 21);
            this.lbl_TotalFileAmount.TabIndex = 1;
            this.lbl_TotalFileAmount.Text = "共计文件数：0";
            // 
            // labelControl14
            // 
            this.labelControl14.Appearance.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl14.Appearance.Image = ((System.Drawing.Image)(resources.GetObject("labelControl14.Appearance.Image")));
            this.labelControl14.Appearance.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.labelControl14.ImageAlignToText = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.labelControl14.Location = new System.Drawing.Point(4, 5);
            this.labelControl14.Name = "labelControl14";
            this.labelControl14.Size = new System.Drawing.Size(85, 21);
            this.labelControl14.TabIndex = 0;
            this.labelControl14.Text = "文件列表";
            // 
            // btn_FileReset
            // 
            this.btn_FileReset.Appearance.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_FileReset.Appearance.Options.UseFont = true;
            this.btn_FileReset.Image = ((System.Drawing.Image)(resources.GetObject("btn_FileReset.Image")));
            this.btn_FileReset.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btn_FileReset.Location = new System.Drawing.Point(839, 154);
            this.btn_FileReset.Name = "btn_FileReset";
            this.btn_FileReset.Size = new System.Drawing.Size(67, 28);
            this.btn_FileReset.TabIndex = 32;
            this.btn_FileReset.Text = "清空";
            this.btn_FileReset.Click += new System.EventHandler(this.Btn_FileReset_Click);
            // 
            // btn_FileQuery
            // 
            this.btn_FileQuery.Appearance.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.btn_FileQuery.Appearance.Options.UseFont = true;
            this.btn_FileQuery.Image = ((System.Drawing.Image)(resources.GetObject("btn_FileQuery.Image")));
            this.btn_FileQuery.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btn_FileQuery.Location = new System.Drawing.Point(734, 154);
            this.btn_FileQuery.Name = "btn_FileQuery";
            this.btn_FileQuery.Size = new System.Drawing.Size(99, 28);
            this.btn_FileQuery.TabIndex = 31;
            this.btn_FileQuery.Text = "立即查询";
            this.btn_FileQuery.Click += new System.EventHandler(this.Btn_FileQuery_Click);
            // 
            // Frm_QueryBorrowing
            // 
            this.Appearance.Options.UseFont = true;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1228, 749);
            this.Controls.Add(this.navigationPane1);
            this.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "Frm_QueryBorrowing";
            this.Text = "查询借阅";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Frm_QueryBorrowing_FormClosing);
            this.Load += new System.EventHandler(this.Frm_QueryBorrowing_Load);
            this.navigationPane1.ResumeLayout(false);
            this.navigationPane1.PerformLayout();
            this.navigationPage1.ResumeLayout(false);
            this.navigationPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txt_ProjectName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_ProjectCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_BatchName.Properties)).EndInit();
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.treeList1)).EndInit();
            this.pal1.ResumeLayout(false);
            this.pal1.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txt_page.Properties)).EndInit();
            this.navigationPage2.ResumeLayout(false);
            this.navigationPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txt_FileName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_Pname.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_Pcode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_FileCategor.Properties)).EndInit();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.view2)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraBars.Navigation.NavigationPane navigationPane1;
        private DevExpress.XtraBars.Navigation.NavigationPage navigationPage1;
        private DevExpress.XtraBars.Navigation.NavigationPage navigationPage2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel pal1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.TextEdit txt_BatchName;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.TextEdit txt_ProjectCode;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl labelControl8;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.TextEdit txt_ProjectName;
        private KyoControl.KyoButton btn_Query;
        private KyoControl.KyoButton btn_Reset;
        private KyoControl.KyoButton btn_FileReset;
        private KyoControl.KyoButton btn_FileQuery;
        private DevExpress.XtraEditors.LabelControl labelControl10;
        private DevExpress.XtraEditors.TextEdit txt_Pname;
        private DevExpress.XtraEditors.LabelControl labelControl11;
        private DevExpress.XtraEditors.TextEdit txt_Pcode;
        private DevExpress.XtraEditors.LabelControl labelControl12;
        private DevExpress.XtraEditors.LabelControl labelControl13;
        private DevExpress.XtraEditors.TextEdit txt_FileCategor;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView view2;
        private System.Windows.Forms.Panel panel2;
        private DevExpress.XtraEditors.LabelControl labelControl14;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TextEdit txt_FileName;
        private System.Windows.Forms.RadioButton rdo_All;
        private System.Windows.Forms.RadioButton rdo_In;
        private System.Windows.Forms.RadioButton rdo_Out;
        private System.Windows.Forms.ComboBox cbo_PlanTypeList;
        private System.Windows.Forms.Panel panel4;
        private DevExpress.XtraEditors.SimpleButton btn_lpage;
        private DevExpress.XtraEditors.SimpleButton btn_npage;
        private DevExpress.XtraEditors.SimpleButton btn_epage;
        private DevExpress.XtraEditors.SimpleButton btn_fpage;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraEditors.TextEdit txt_page;
        private System.Windows.Forms.CheckBox chk_allDate;
        private System.Windows.Forms.DateTimePicker dtp_eDate;
        private System.Windows.Forms.DateTimePicker dtp_sDate;
        private DevExpress.XtraEditors.LabelControl lbl_TotalFileAmount;
        private System.Windows.Forms.DataGridViewTextBoxColumn fid;
        private System.Windows.Forms.DataGridViewTextBoxColumn fcode;
        private System.Windows.Forms.DataGridViewTextBoxColumn fname;
        private System.Windows.Forms.DataGridViewTextBoxColumn fbox;
        private System.Windows.Forms.DataGridViewButtonColumn fbstate;
        private System.Windows.Forms.DataGridViewButtonColumn frstate;
        private DevExpress.XtraTreeList.TreeList treeList1;
    }
}