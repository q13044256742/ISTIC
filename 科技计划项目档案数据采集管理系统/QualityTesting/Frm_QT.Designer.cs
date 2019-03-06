using System;
using System.Windows.Forms;

namespace 科技计划项目档案数据采集管理系统
{
    partial class Frm_QT
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_QT));
            this.pal_LeftMenu = new System.Windows.Forms.Panel();
            this.ace_LeftMenu = new 科技计划项目档案数据采集管理系统.KyoControl.KyoAccordion();
            this.acg_Worked = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.ace_Login = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.ace_MyLog = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.ace_MyQT = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.dgv_Imp = new System.Windows.Forms.DataGridView();
            this.tab_Menulist = new 科技计划项目档案数据采集管理系统.KyoControl.KyoTabControl();
            this.imp = new DevExpress.XtraTab.XtraTabPage();
            this.imp_dev = new DevExpress.XtraTab.XtraTabPage();
            this.dgv_Imp_Dev = new System.Windows.Forms.DataGridView();
            this.project = new DevExpress.XtraTab.XtraTabPage();
            this.dgv_Project = new System.Windows.Forms.DataGridView();
            this.dgv_MyReg = new System.Windows.Forms.DataGridView();
            this.searchControl = new DevExpress.XtraEditors.SearchControl();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pal_Page = new System.Windows.Forms.Panel();
            this.txt_page = new DevExpress.XtraEditors.TextEdit();
            this.btn_lpage = new DevExpress.XtraEditors.SimpleButton();
            this.btn_npage = new DevExpress.XtraEditors.SimpleButton();
            this.btn_epage = new DevExpress.XtraEditors.SimpleButton();
            this.btn_fpage = new DevExpress.XtraEditors.SimpleButton();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_Search = new DevExpress.XtraEditors.SimpleButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.txt_SearchData_F = new DevExpress.XtraEditors.TextEdit();
            this.txt_SearchDate_S = new DevExpress.XtraEditors.TextEdit();
            this.btn_MyWorkQuery = new 科技计划项目档案数据采集管理系统.KyoControl.KyoButton();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.提交SToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.全部提交AToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pal_LeftMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ace_LeftMenu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Imp)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tab_Menulist)).BeginInit();
            this.tab_Menulist.SuspendLayout();
            this.imp.SuspendLayout();
            this.imp_dev.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Imp_Dev)).BeginInit();
            this.project.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Project)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_MyReg)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchControl.Properties)).BeginInit();
            this.panel1.SuspendLayout();
            this.pal_Page.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txt_page.Properties)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txt_SearchData_F.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_SearchDate_S.Properties)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pal_LeftMenu
            // 
            this.pal_LeftMenu.Controls.Add(this.ace_LeftMenu);
            this.pal_LeftMenu.Dock = System.Windows.Forms.DockStyle.Left;
            this.pal_LeftMenu.Location = new System.Drawing.Point(0, 0);
            this.pal_LeftMenu.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pal_LeftMenu.Name = "pal_LeftMenu";
            this.pal_LeftMenu.Size = new System.Drawing.Size(214, 673);
            this.pal_LeftMenu.TabIndex = 0;
            // 
            // ace_LeftMenu
            // 
            this.ace_LeftMenu.AllowItemSelection = true;
            this.ace_LeftMenu.AnimationType = DevExpress.XtraBars.Navigation.AnimationType.Simple;
            this.ace_LeftMenu.Appearance.Group.Normal.Font = new System.Drawing.Font("微软雅黑", 11.5F);
            this.ace_LeftMenu.Appearance.Group.Normal.Options.UseFont = true;
            this.ace_LeftMenu.Appearance.Item.Hovered.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.ace_LeftMenu.Appearance.Item.Hovered.Options.UseFont = true;
            this.ace_LeftMenu.Appearance.Item.Normal.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.ace_LeftMenu.Appearance.Item.Normal.Options.UseFont = true;
            this.ace_LeftMenu.Appearance.Item.Pressed.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Bold);
            this.ace_LeftMenu.Appearance.Item.Pressed.Options.UseFont = true;
            this.ace_LeftMenu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ace_LeftMenu.Elements.AddRange(new DevExpress.XtraBars.Navigation.AccordionControlElement[] {
            this.acg_Worked});
            this.ace_LeftMenu.Location = new System.Drawing.Point(0, 0);
            this.ace_LeftMenu.LookAndFeel.SkinName = "McSkin";
            this.ace_LeftMenu.LookAndFeel.UseDefaultLookAndFeel = false;
            this.ace_LeftMenu.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ace_LeftMenu.Name = "ace_LeftMenu";
            this.ace_LeftMenu.ScrollBarMode = DevExpress.XtraBars.Navigation.ScrollBarMode.Hidden;
            this.ace_LeftMenu.Size = new System.Drawing.Size(214, 673);
            this.ace_LeftMenu.TabIndex = 17;
            // 
            // acg_Worked
            // 
            this.acg_Worked.Appearance.Hovered.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.acg_Worked.Appearance.Hovered.ForeColor = System.Drawing.Color.Black;
            this.acg_Worked.Appearance.Hovered.Options.UseFont = true;
            this.acg_Worked.Appearance.Hovered.Options.UseForeColor = true;
            this.acg_Worked.Appearance.Normal.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.acg_Worked.Appearance.Normal.ForeColor = System.Drawing.Color.Black;
            this.acg_Worked.Appearance.Normal.Options.UseFont = true;
            this.acg_Worked.Appearance.Normal.Options.UseForeColor = true;
            this.acg_Worked.Appearance.Normal.Options.UseTextOptions = true;
            this.acg_Worked.Appearance.Normal.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.acg_Worked.Appearance.Pressed.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.acg_Worked.Appearance.Pressed.Options.UseFont = true;
            this.acg_Worked.Elements.AddRange(new DevExpress.XtraBars.Navigation.AccordionControlElement[] {
            this.ace_Login,
            this.ace_MyLog,
            this.ace_MyQT});
            this.acg_Worked.Expanded = true;
            this.acg_Worked.Height = 50;
            this.acg_Worked.Image = ((System.Drawing.Image)(resources.GetObject("acg_Worked.Image")));
            this.acg_Worked.Name = "acg_Worked";
            this.acg_Worked.Text = "档案质检";
            this.acg_Worked.TextToImageDistance = 10;
            // 
            // ace_Login
            // 
            this.ace_Login.Appearance.Normal.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.ace_Login.Appearance.Normal.Options.UseFont = true;
            this.ace_Login.Appearance.Pressed.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.ace_Login.Appearance.Pressed.Options.UseFont = true;
            this.ace_Login.Height = 35;
            this.ace_Login.Image = ((System.Drawing.Image)(resources.GetObject("ace_Login.Image")));
            this.ace_Login.ImageLayoutMode = DevExpress.XtraBars.Navigation.ImageLayoutMode.Stretch;
            this.ace_Login.Name = "ace_Login";
            this.ace_Login.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.ace_Login.Text = "质检登记";
            this.ace_Login.TextToImageDistance = 15;
            this.ace_Login.Click += new System.EventHandler(this.Sub_Click);
            // 
            // ace_MyLog
            // 
            this.ace_MyLog.Appearance.Normal.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.ace_MyLog.Appearance.Normal.Options.UseFont = true;
            this.ace_MyLog.Appearance.Pressed.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.ace_MyLog.Appearance.Pressed.Options.UseFont = true;
            this.ace_MyLog.Height = 35;
            this.ace_MyLog.Image = ((System.Drawing.Image)(resources.GetObject("ace_MyLog.Image")));
            this.ace_MyLog.ImageLayoutMode = DevExpress.XtraBars.Navigation.ImageLayoutMode.Squeeze;
            this.ace_MyLog.Name = "ace_MyLog";
            this.ace_MyLog.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.ace_MyLog.Text = "质检中";
            this.ace_MyLog.TextToImageDistance = 15;
            this.ace_MyLog.Click += new System.EventHandler(this.Sub_Click);
            // 
            // ace_MyQT
            // 
            this.ace_MyQT.Appearance.Normal.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.ace_MyQT.Appearance.Normal.Options.UseFont = true;
            this.ace_MyQT.Appearance.Pressed.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.ace_MyQT.Appearance.Pressed.Options.UseFont = true;
            this.ace_MyQT.Height = 35;
            this.ace_MyQT.Image = ((System.Drawing.Image)(resources.GetObject("ace_MyQT.Image")));
            this.ace_MyQT.ImageLayoutMode = DevExpress.XtraBars.Navigation.ImageLayoutMode.Squeeze;
            this.ace_MyQT.Name = "ace_MyQT";
            this.ace_MyQT.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.ace_MyQT.Text = "我的质检";
            this.ace_MyQT.TextToImageDistance = 15;
            this.ace_MyQT.Click += new System.EventHandler(this.Sub_Click);
            // 
            // dgv_Imp
            // 
            this.dgv_Imp.AllowUserToAddRows = false;
            this.dgv_Imp.AllowUserToDeleteRows = false;
            this.dgv_Imp.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv_Imp.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgv_Imp.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgv_Imp.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_Imp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_Imp.Location = new System.Drawing.Point(0, 0);
            this.dgv_Imp.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dgv_Imp.Name = "dgv_Imp";
            this.dgv_Imp.ReadOnly = true;
            this.dgv_Imp.RowTemplate.Height = 23;
            this.dgv_Imp.Size = new System.Drawing.Size(1003, 543);
            this.dgv_Imp.TabIndex = 2;
            this.dgv_Imp.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Dgv_Imp_CellClick);
            // 
            // tab_Menulist
            // 
            this.tab_Menulist.Appearance.Font = new System.Drawing.Font("微软雅黑", 15F);
            this.tab_Menulist.Appearance.Options.UseFont = true;
            this.tab_Menulist.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tab_Menulist.Location = new System.Drawing.Point(0, 0);
            this.tab_Menulist.Margin = new System.Windows.Forms.Padding(0);
            this.tab_Menulist.Name = "tab_Menulist";
            this.tab_Menulist.SelectedTabPage = this.imp;
            this.tab_Menulist.Size = new System.Drawing.Size(1009, 583);
            this.tab_Menulist.TabIndex = 3;
            this.tab_Menulist.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.imp,
            this.imp_dev,
            this.project});
            this.tab_Menulist.SelectedPageChanged += new DevExpress.XtraTab.TabPageChangedEventHandler(this.Tab_Menulist_SelectedIndexChanged);
            // 
            // imp
            // 
            this.imp.Appearance.Header.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.imp.Appearance.Header.Options.UseFont = true;
            this.imp.Appearance.PageClient.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.imp.Appearance.PageClient.Options.UseFont = true;
            this.imp.Controls.Add(this.dgv_Imp);
            this.imp.Margin = new System.Windows.Forms.Padding(0);
            this.imp.Name = "imp";
            this.imp.Size = new System.Drawing.Size(1003, 543);
            this.imp.Text = "计划";
            // 
            // imp_dev
            // 
            this.imp_dev.Appearance.Header.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.imp_dev.Appearance.Header.Options.UseFont = true;
            this.imp_dev.Appearance.PageClient.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.imp_dev.Appearance.PageClient.Options.UseFont = true;
            this.imp_dev.Controls.Add(this.dgv_Imp_Dev);
            this.imp_dev.Margin = new System.Windows.Forms.Padding(0);
            this.imp_dev.Name = "imp_dev";
            this.imp_dev.Size = new System.Drawing.Size(1003, 543);
            this.imp_dev.Text = "专项";
            // 
            // dgv_Imp_Dev
            // 
            this.dgv_Imp_Dev.AllowUserToAddRows = false;
            this.dgv_Imp_Dev.AllowUserToDeleteRows = false;
            this.dgv_Imp_Dev.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv_Imp_Dev.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgv_Imp_Dev.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgv_Imp_Dev.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_Imp_Dev.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_Imp_Dev.Location = new System.Drawing.Point(0, 0);
            this.dgv_Imp_Dev.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dgv_Imp_Dev.Name = "dgv_Imp_Dev";
            this.dgv_Imp_Dev.ReadOnly = true;
            this.dgv_Imp_Dev.RowTemplate.Height = 23;
            this.dgv_Imp_Dev.Size = new System.Drawing.Size(1003, 543);
            this.dgv_Imp_Dev.TabIndex = 0;
            this.dgv_Imp_Dev.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Dgv_Imp_Dev_CellClick);
            // 
            // project
            // 
            this.project.Appearance.Header.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.project.Appearance.Header.Options.UseFont = true;
            this.project.Appearance.PageClient.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.project.Appearance.PageClient.Options.UseFont = true;
            this.project.Controls.Add(this.dgv_Project);
            this.project.Margin = new System.Windows.Forms.Padding(0);
            this.project.Name = "project";
            this.project.Size = new System.Drawing.Size(1003, 543);
            this.project.Text = "项目/课题";
            // 
            // dgv_Project
            // 
            this.dgv_Project.AllowUserToAddRows = false;
            this.dgv_Project.AllowUserToDeleteRows = false;
            this.dgv_Project.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv_Project.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgv_Project.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgv_Project.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_Project.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_Project.Location = new System.Drawing.Point(0, 0);
            this.dgv_Project.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dgv_Project.Name = "dgv_Project";
            this.dgv_Project.ReadOnly = true;
            this.dgv_Project.RowTemplate.Height = 23;
            this.dgv_Project.Size = new System.Drawing.Size(1003, 543);
            this.dgv_Project.TabIndex = 0;
            this.dgv_Project.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Dgv_Project_CellClick);
            // 
            // dgv_MyReg
            // 
            this.dgv_MyReg.AllowUserToDeleteRows = false;
            this.dgv_MyReg.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv_MyReg.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgv_MyReg.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_MyReg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_MyReg.Location = new System.Drawing.Point(0, 0);
            this.dgv_MyReg.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dgv_MyReg.Name = "dgv_MyReg";
            this.dgv_MyReg.ReadOnly = true;
            this.dgv_MyReg.RowTemplate.Height = 23;
            this.dgv_MyReg.Size = new System.Drawing.Size(1009, 583);
            this.dgv_MyReg.TabIndex = 2;
            this.dgv_MyReg.Visible = false;
            this.dgv_MyReg.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Dgv_MyReg_CellClick);
            // 
            // searchControl
            // 
            this.searchControl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.searchControl.Location = new System.Drawing.Point(953, 2);
            this.searchControl.Name = "searchControl";
            this.searchControl.Properties.Appearance.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.searchControl.Properties.Appearance.Options.UseFont = true;
            this.searchControl.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
            this.searchControl.Properties.NullValuePrompt = "输入关键字模糊查询";
            this.searchControl.Properties.ShowClearButton = false;
            this.searchControl.Properties.ShowSearchButton = false;
            this.searchControl.Size = new System.Drawing.Size(197, 30);
            this.searchControl.TabIndex = 3;
            this.searchControl.KeyDown += new System.Windows.Forms.KeyEventHandler(this.SearchControl_KeyDown);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tab_Menulist);
            this.panel1.Controls.Add(this.dgv_MyReg);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(214, 52);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1009, 583);
            this.panel1.TabIndex = 4;
            // 
            // pal_Page
            // 
            this.pal_Page.Controls.Add(this.txt_page);
            this.pal_Page.Controls.Add(this.btn_lpage);
            this.pal_Page.Controls.Add(this.btn_npage);
            this.pal_Page.Controls.Add(this.btn_epage);
            this.pal_Page.Controls.Add(this.btn_fpage);
            this.pal_Page.Controls.Add(this.label1);
            this.pal_Page.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pal_Page.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.pal_Page.Location = new System.Drawing.Point(214, 635);
            this.pal_Page.Name = "pal_Page";
            this.pal_Page.Size = new System.Drawing.Size(1009, 38);
            this.pal_Page.TabIndex = 4;
            this.pal_Page.Visible = false;
            // 
            // txt_page
            // 
            this.txt_page.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txt_page.EditValue = "";
            this.txt_page.Location = new System.Drawing.Point(876, 7);
            this.txt_page.Name = "txt_page";
            this.txt_page.Properties.Appearance.Font = new System.Drawing.Font("微软雅黑", 10.5F);
            this.txt_page.Properties.Appearance.Options.UseFont = true;
            this.txt_page.Properties.Appearance.Options.UseTextOptions = true;
            this.txt_page.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.txt_page.Properties.Mask.EditMask = "d";
            this.txt_page.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.txt_page.Size = new System.Drawing.Size(41, 26);
            this.txt_page.TabIndex = 7;
            // 
            // btn_lpage
            // 
            this.btn_lpage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_lpage.Appearance.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_lpage.Appearance.Options.UseFont = true;
            this.btn_lpage.Image = ((System.Drawing.Image)(resources.GetObject("btn_lpage.Image")));
            this.btn_lpage.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btn_lpage.Location = new System.Drawing.Point(834, 8);
            this.btn_lpage.Name = "btn_lpage";
            this.btn_lpage.Size = new System.Drawing.Size(41, 23);
            this.btn_lpage.TabIndex = 6;
            this.btn_lpage.Click += new System.EventHandler(this.Page_Click);
            // 
            // btn_npage
            // 
            this.btn_npage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_npage.Appearance.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_npage.Appearance.Options.UseFont = true;
            this.btn_npage.Image = ((System.Drawing.Image)(resources.GetObject("btn_npage.Image")));
            this.btn_npage.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btn_npage.Location = new System.Drawing.Point(918, 8);
            this.btn_npage.Name = "btn_npage";
            this.btn_npage.Size = new System.Drawing.Size(41, 23);
            this.btn_npage.TabIndex = 5;
            this.btn_npage.Click += new System.EventHandler(this.Page_Click);
            // 
            // btn_epage
            // 
            this.btn_epage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_epage.Appearance.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_epage.Appearance.Options.UseFont = true;
            this.btn_epage.Image = ((System.Drawing.Image)(resources.GetObject("btn_epage.Image")));
            this.btn_epage.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btn_epage.Location = new System.Drawing.Point(961, 8);
            this.btn_epage.Name = "btn_epage";
            this.btn_epage.Size = new System.Drawing.Size(41, 23);
            this.btn_epage.TabIndex = 4;
            this.btn_epage.Click += new System.EventHandler(this.Page_Click);
            // 
            // btn_fpage
            // 
            this.btn_fpage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_fpage.Appearance.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_fpage.Appearance.Options.UseFont = true;
            this.btn_fpage.Image = ((System.Drawing.Image)(resources.GetObject("btn_fpage.Image")));
            this.btn_fpage.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btn_fpage.Location = new System.Drawing.Point(791, 8);
            this.btn_fpage.Name = "btn_fpage";
            this.btn_fpage.Size = new System.Drawing.Size(41, 23);
            this.btn_fpage.TabIndex = 3;
            this.btn_fpage.Click += new System.EventHandler(this.Page_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(255, 20);
            this.label1.TabIndex = 2;
            this.label1.Text = "总计 0 条记录，每页共 0 条，共 0 页";
            // 
            // btn_Search
            // 
            this.btn_Search.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Search.Appearance.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.btn_Search.Appearance.Options.UseFont = true;
            this.btn_Search.Image = ((System.Drawing.Image)(resources.GetObject("btn_Search.Image")));
            this.btn_Search.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btn_Search.Location = new System.Drawing.Point(1153, 3);
            this.btn_Search.Name = "btn_Search";
            this.btn_Search.Size = new System.Drawing.Size(66, 29);
            this.btn_Search.TabIndex = 3;
            this.btn_Search.Text = "查询";
            this.btn_Search.Visible = false;
            this.btn_Search.Click += new System.EventHandler(this.btn_Search_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.comboBox1);
            this.panel2.Controls.Add(this.txt_SearchData_F);
            this.panel2.Controls.Add(this.txt_SearchDate_S);
            this.panel2.Controls.Add(this.btn_MyWorkQuery);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(214, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1009, 52);
            this.panel2.TabIndex = 20;
            this.panel2.Visible = false;
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "选择时间段",
            "最近三天",
            "最近一周",
            "最近一个月"});
            this.comboBox1.Location = new System.Drawing.Point(436, 12);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(142, 28);
            this.comboBox1.TabIndex = 16;
            this.comboBox1.SelectionChangeCommitted += new System.EventHandler(this.comboBox1_SelectionChangeCommitted);
            // 
            // txt_SearchData_F
            // 
            this.txt_SearchData_F.EditValue = "";
            this.txt_SearchData_F.Location = new System.Drawing.Point(300, 10);
            this.txt_SearchData_F.Name = "txt_SearchData_F";
            this.txt_SearchData_F.Properties.Appearance.Font = new System.Drawing.Font("微软雅黑", 13F);
            this.txt_SearchData_F.Properties.Appearance.Options.UseFont = true;
            this.txt_SearchData_F.Properties.Appearance.Options.UseTextOptions = true;
            this.txt_SearchData_F.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.txt_SearchData_F.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
            this.txt_SearchData_F.Properties.DisplayFormat.FormatString = "yyyy-MM-dd";
            this.txt_SearchData_F.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.txt_SearchData_F.Properties.Mask.BeepOnError = true;
            this.txt_SearchData_F.Properties.Mask.EditMask = "yyyy-MM-dd";
            this.txt_SearchData_F.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTime;
            this.txt_SearchData_F.Size = new System.Drawing.Size(130, 32);
            this.txt_SearchData_F.TabIndex = 15;
            // 
            // txt_SearchDate_S
            // 
            this.txt_SearchDate_S.EditValue = "";
            this.txt_SearchDate_S.Location = new System.Drawing.Point(125, 10);
            this.txt_SearchDate_S.Name = "txt_SearchDate_S";
            this.txt_SearchDate_S.Properties.Appearance.Font = new System.Drawing.Font("微软雅黑", 13F);
            this.txt_SearchDate_S.Properties.Appearance.Options.UseFont = true;
            this.txt_SearchDate_S.Properties.Appearance.Options.UseTextOptions = true;
            this.txt_SearchDate_S.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.txt_SearchDate_S.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
            this.txt_SearchDate_S.Properties.DisplayFormat.FormatString = "yyyy-MM-dd";
            this.txt_SearchDate_S.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.txt_SearchDate_S.Properties.Mask.BeepOnError = true;
            this.txt_SearchDate_S.Properties.Mask.EditMask = "yyyy-MM-dd";
            this.txt_SearchDate_S.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTime;
            this.txt_SearchDate_S.Size = new System.Drawing.Size(141, 32);
            this.txt_SearchDate_S.TabIndex = 14;
            // 
            // btn_MyWorkQuery
            // 
            this.btn_MyWorkQuery.Appearance.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.btn_MyWorkQuery.Appearance.Options.UseFont = true;
            this.btn_MyWorkQuery.Image = ((System.Drawing.Image)(resources.GetObject("btn_MyWorkQuery.Image")));
            this.btn_MyWorkQuery.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btn_MyWorkQuery.Location = new System.Drawing.Point(584, 11);
            this.btn_MyWorkQuery.Name = "btn_MyWorkQuery";
            this.btn_MyWorkQuery.Size = new System.Drawing.Size(76, 31);
            this.btn_MyWorkQuery.TabIndex = 13;
            this.btn_MyWorkQuery.Text = "查看";
            this.btn_MyWorkQuery.Click += new System.EventHandler(this.btn_MyWorkQuery_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(272, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(19, 20);
            this.label3.TabIndex = 11;
            this.label3.Text = "~";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 13F);
            this.label2.Location = new System.Drawing.Point(19, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 24);
            this.label2.TabIndex = 10;
            this.label2.Text = "日期范围：";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.提交SToolStripMenuItem,
            this.全部提交AToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(152, 48);
            // 
            // 提交SToolStripMenuItem
            // 
            this.提交SToolStripMenuItem.Name = "提交SToolStripMenuItem";
            this.提交SToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
            this.提交SToolStripMenuItem.Text = "提交选中项(&S)";
            this.提交SToolStripMenuItem.Click += new System.EventHandler(this.提交SToolStripMenuItem_Click);
            // 
            // 全部提交AToolStripMenuItem
            // 
            this.全部提交AToolStripMenuItem.Name = "全部提交AToolStripMenuItem";
            this.全部提交AToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
            this.全部提交AToolStripMenuItem.Text = "全部提交(&A)";
            this.全部提交AToolStripMenuItem.Click += new System.EventHandler(this.全部提交AToolStripMenuItem_Click);
            // 
            // Frm_QT
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1223, 673);
            this.Controls.Add(this.searchControl);
            this.Controls.Add(this.btn_Search);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.pal_Page);
            this.Controls.Add(this.pal_LeftMenu);
            this.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Frm_QT";
            this.Text = "档案质检";
            this.Load += new System.EventHandler(this.Frm_QT_Load);
            this.pal_LeftMenu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ace_LeftMenu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Imp)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tab_Menulist)).EndInit();
            this.tab_Menulist.ResumeLayout(false);
            this.imp.ResumeLayout(false);
            this.imp_dev.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Imp_Dev)).EndInit();
            this.project.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Project)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_MyReg)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchControl.Properties)).EndInit();
            this.panel1.ResumeLayout(false);
            this.pal_Page.ResumeLayout(false);
            this.pal_Page.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txt_page.Properties)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txt_SearchData_F.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_SearchDate_S.Properties)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }
        #endregion

        private System.Windows.Forms.Panel pal_LeftMenu;
        private System.Windows.Forms.DataGridView dgv_Imp;
        private KyoControl.KyoTabControl tab_Menulist;
        private System.Windows.Forms.DataGridView dgv_MyReg;
        private DevExpress.XtraTab.XtraTabPage imp;
        private DevExpress.XtraTab.XtraTabPage imp_dev;
        private DevExpress.XtraTab.XtraTabPage project;
        private System.Windows.Forms.DataGridView dgv_Imp_Dev;
        private System.Windows.Forms.DataGridView dgv_Project;
        private KyoControl.KyoAccordion ace_LeftMenu;
        private DevExpress.XtraBars.Navigation.AccordionControlElement acg_Worked;
        private DevExpress.XtraBars.Navigation.AccordionControlElement ace_Login;
        private DevExpress.XtraBars.Navigation.AccordionControlElement ace_MyLog;
        private DevExpress.XtraEditors.SearchControl searchControl;
        private DevExpress.XtraBars.Navigation.AccordionControlElement ace_MyQT;
        private Panel panel1;
        private Panel pal_Page;
        private DevExpress.XtraEditors.TextEdit txt_page;
        private DevExpress.XtraEditors.SimpleButton btn_lpage;
        private DevExpress.XtraEditors.SimpleButton btn_npage;
        private DevExpress.XtraEditors.SimpleButton btn_epage;
        private DevExpress.XtraEditors.SimpleButton btn_fpage;
        private Label label1;
        private DevExpress.XtraEditors.SimpleButton btn_Search;
        private Panel panel2;
        private ComboBox comboBox1;
        private DevExpress.XtraEditors.TextEdit txt_SearchData_F;
        private DevExpress.XtraEditors.TextEdit txt_SearchDate_S;
        private KyoControl.KyoButton btn_MyWorkQuery;
        private Label label3;
        private Label label2;
        private ContextMenuStrip contextMenuStrip1;
        private ToolStripMenuItem 提交SToolStripMenuItem;
        private ToolStripMenuItem 全部提交AToolStripMenuItem;
    }
}