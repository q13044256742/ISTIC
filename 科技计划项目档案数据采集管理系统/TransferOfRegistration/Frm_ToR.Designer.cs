namespace 科技计划项目档案数据采集管理系统
{
    partial class Frm_ToR
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_ToR));
            this.dgv_GPDJ = new System.Windows.Forms.DataGridView();
            this.tabPage1 = new DevExpress.XtraTab.XtraTabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txt_Search = new DevExpress.XtraEditors.SearchControl();
            this.btn_Back = new 科技计划项目档案数据采集管理系统.KyoControl.KyoButton();
            this.btn_Delete = new 科技计划项目档案数据采集管理系统.KyoControl.KyoButton();
            this.btn_Add = new 科技计划项目档案数据采集管理系统.KyoControl.KyoButton();
            this.dgv_SWDJ = new System.Windows.Forms.DataGridView();
            this.tabPage2 = new DevExpress.XtraTab.XtraTabPage();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.txt_CDSearch = new DevExpress.XtraEditors.SearchControl();
            this.btn_CD_Delete = new 科技计划项目档案数据采集管理系统.KyoControl.KyoButton();
            this.cbo_Status = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tc_ToR = new 科技计划项目档案数据采集管理系统.KyoControl.KyoTabControl();
            this.pal_YJDJ = new System.Windows.Forms.Panel();
            this.pal_XTSY = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.ac_LeftMenu = new 科技计划项目档案数据采集管理系统.KyoControl.KyoAccordion();
            this.acg_Register = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.ace_all = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_GPDJ)).BeginInit();
            this.tabPage1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txt_Search.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_SWDJ)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txt_CDSearch.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tc_ToR)).BeginInit();
            this.tc_ToR.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ac_LeftMenu)).BeginInit();
            this.SuspendLayout();
            // 
            // dgv_GPDJ
            // 
            this.dgv_GPDJ.AllowUserToAddRows = false;
            this.dgv_GPDJ.AllowUserToDeleteRows = false;
            this.dgv_GPDJ.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgv_GPDJ.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv_GPDJ.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgv_GPDJ.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgv_GPDJ.Location = new System.Drawing.Point(2, 44);
            this.dgv_GPDJ.Name = "dgv_GPDJ";
            this.dgv_GPDJ.ReadOnly = true;
            this.dgv_GPDJ.RowTemplate.Height = 23;
            this.dgv_GPDJ.Size = new System.Drawing.Size(729, 408);
            this.dgv_GPDJ.TabIndex = 1;
            this.dgv_GPDJ.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Dgv_GPDJ_CellClick);
            // 
            // tabPage1
            // 
            this.tabPage1.Appearance.Header.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.tabPage1.Appearance.Header.Options.UseFont = true;
            this.tabPage1.Appearance.PageClient.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.tabPage1.Appearance.PageClient.Options.UseFont = true;
            this.tabPage1.Controls.Add(this.groupBox2);
            this.tabPage1.Controls.Add(this.dgv_SWDJ);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(736, 456);
            this.tabPage1.Text = "实物登记";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.txt_Search);
            this.groupBox2.Controls.Add(this.btn_Back);
            this.groupBox2.Controls.Add(this.btn_Delete);
            this.groupBox2.Controls.Add(this.btn_Add);
            this.groupBox2.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.groupBox2.Location = new System.Drawing.Point(2, -6);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(727, 49);
            this.groupBox2.TabIndex = 12;
            this.groupBox2.TabStop = false;
            // 
            // txt_Search
            // 
            this.txt_Search.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txt_Search.Location = new System.Drawing.Point(267, 14);
            this.txt_Search.Name = "txt_Search";
            this.txt_Search.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.txt_Search.Properties.Appearance.Options.UseFont = true;
            this.txt_Search.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
            this.txt_Search.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Repository.ClearButton(),
            new DevExpress.XtraEditors.Repository.SearchButton(),
            new DevExpress.XtraEditors.Repository.MRUButton()});
            this.txt_Search.Properties.NullValuePrompt = "输入关键字进行查询";
            this.txt_Search.Properties.ShowDefaultButtonsMode = DevExpress.XtraEditors.Repository.ShowDefaultButtonsMode.AutoShowClear;
            this.txt_Search.Properties.ShowMRUButton = true;
            this.txt_Search.Properties.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.Txt_Search_Properties_ButtonClick);
            this.txt_Search.Size = new System.Drawing.Size(232, 28);
            this.txt_Search.TabIndex = 5;
            this.txt_Search.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_Search_KeyDown);
            // 
            // btn_Back
            // 
            this.btn_Back.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Back.Appearance.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Back.Appearance.Options.UseFont = true;
            this.btn_Back.Image = ((System.Drawing.Image)(resources.GetObject("btn_Back.Image")));
            this.btn_Back.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btn_Back.ImageToTextIndent = 5;
            this.btn_Back.Location = new System.Drawing.Point(658, 13);
            this.btn_Back.Name = "btn_Back";
            this.btn_Back.Size = new System.Drawing.Size(66, 30);
            this.btn_Back.TabIndex = 3;
            this.btn_Back.Text = "返回";
            this.btn_Back.Click += new System.EventHandler(this.Btn_Back_Click);
            // 
            // btn_Delete
            // 
            this.btn_Delete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Delete.Appearance.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_Delete.Appearance.Options.UseFont = true;
            this.btn_Delete.Image = ((System.Drawing.Image)(resources.GetObject("btn_Delete.Image")));
            this.btn_Delete.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btn_Delete.ImageToTextIndent = 5;
            this.btn_Delete.Location = new System.Drawing.Point(582, 13);
            this.btn_Delete.Name = "btn_Delete";
            this.btn_Delete.Size = new System.Drawing.Size(68, 30);
            this.btn_Delete.TabIndex = 2;
            this.btn_Delete.Text = "删除";
            this.btn_Delete.Click += new System.EventHandler(this.Btn_Delete_Click);
            // 
            // btn_Add
            // 
            this.btn_Add.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Add.Appearance.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Add.Appearance.Options.UseFont = true;
            this.btn_Add.Image = ((System.Drawing.Image)(resources.GetObject("btn_Add.Image")));
            this.btn_Add.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btn_Add.ImageToTextIndent = 5;
            this.btn_Add.Location = new System.Drawing.Point(507, 13);
            this.btn_Add.Name = "btn_Add";
            this.btn_Add.Size = new System.Drawing.Size(67, 30);
            this.btn_Add.TabIndex = 0;
            this.btn_Add.Text = "添加";
            this.btn_Add.Click += new System.EventHandler(this.Btn_Add_Click);
            // 
            // dgv_SWDJ
            // 
            this.dgv_SWDJ.AllowUserToAddRows = false;
            this.dgv_SWDJ.AllowUserToDeleteRows = false;
            this.dgv_SWDJ.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgv_SWDJ.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv_SWDJ.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgv_SWDJ.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgv_SWDJ.Location = new System.Drawing.Point(2, 44);
            this.dgv_SWDJ.Name = "dgv_SWDJ";
            this.dgv_SWDJ.ReadOnly = true;
            this.dgv_SWDJ.RowTemplate.Height = 23;
            this.dgv_SWDJ.Size = new System.Drawing.Size(727, 407);
            this.dgv_SWDJ.TabIndex = 0;
            this.dgv_SWDJ.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Dgv_SWDJ_CellClick);
            // 
            // tabPage2
            // 
            this.tabPage2.Appearance.Header.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.tabPage2.Appearance.Header.Options.UseFont = true;
            this.tabPage2.Appearance.PageClient.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.tabPage2.Appearance.PageClient.Options.UseFont = true;
            this.tabPage2.Controls.Add(this.groupBox3);
            this.tabPage2.Controls.Add(this.dgv_GPDJ);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(736, 456);
            this.tabPage2.Text = "光盘登记";
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.txt_CDSearch);
            this.groupBox3.Controls.Add(this.btn_CD_Delete);
            this.groupBox3.Controls.Add(this.cbo_Status);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.groupBox3.Location = new System.Drawing.Point(2, -6);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(728, 49);
            this.groupBox3.TabIndex = 13;
            this.groupBox3.TabStop = false;
            // 
            // txt_CDSearch
            // 
            this.txt_CDSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txt_CDSearch.Location = new System.Drawing.Point(416, 14);
            this.txt_CDSearch.Name = "txt_CDSearch";
            this.txt_CDSearch.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.txt_CDSearch.Properties.Appearance.Options.UseFont = true;
            this.txt_CDSearch.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
            this.txt_CDSearch.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Repository.ClearButton(),
            new DevExpress.XtraEditors.Repository.SearchButton(),
            new DevExpress.XtraEditors.Repository.MRUButton()});
            this.txt_CDSearch.Properties.NullValuePrompt = "输入关键字进行查询";
            this.txt_CDSearch.Properties.ShowDefaultButtonsMode = DevExpress.XtraEditors.Repository.ShowDefaultButtonsMode.AutoShowClear;
            this.txt_CDSearch.Properties.ShowMRUButton = true;
            this.txt_CDSearch.Properties.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.SearchControl1_Properties_ButtonClick);
            this.txt_CDSearch.Size = new System.Drawing.Size(232, 28);
            this.txt_CDSearch.TabIndex = 8;
            this.txt_CDSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_CDSearch_KeyDown);
            // 
            // btn_CD_Delete
            // 
            this.btn_CD_Delete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_CD_Delete.Appearance.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_CD_Delete.Appearance.Options.UseFont = true;
            this.btn_CD_Delete.Image = ((System.Drawing.Image)(resources.GetObject("btn_CD_Delete.Image")));
            this.btn_CD_Delete.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btn_CD_Delete.ImageToTextIndent = 5;
            this.btn_CD_Delete.Location = new System.Drawing.Point(654, 13);
            this.btn_CD_Delete.Name = "btn_CD_Delete";
            this.btn_CD_Delete.Size = new System.Drawing.Size(67, 30);
            this.btn_CD_Delete.TabIndex = 7;
            this.btn_CD_Delete.Text = "删除";
            this.btn_CD_Delete.Click += new System.EventHandler(this.Btn_CD_Delete_Click);
            // 
            // cbo_Status
            // 
            this.cbo_Status.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo_Status.FormattingEnabled = true;
            this.cbo_Status.Items.AddRange(new object[] {
            "全部",
            "尚未读写",
            "读写成功",
            "解析异常"});
            this.cbo_Status.Location = new System.Drawing.Point(95, 16);
            this.cbo_Status.Name = "cbo_Status";
            this.cbo_Status.Size = new System.Drawing.Size(143, 25);
            this.cbo_Status.TabIndex = 6;
            this.cbo_Status.SelectionChangeCommitted += new System.EventHandler(this.Cbo_Status_SelectionChangeCommitted);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.label1.Location = new System.Drawing.Point(10, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 20);
            this.label1.TabIndex = 5;
            this.label1.Text = "读写状态：";
            // 
            // tc_ToR
            // 
            this.tc_ToR.Appearance.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.tc_ToR.Appearance.Options.UseFont = true;
            this.tc_ToR.AppearancePage.Header.Font = new System.Drawing.Font("Tahoma", 11F);
            this.tc_ToR.AppearancePage.Header.Options.UseFont = true;
            this.tc_ToR.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tc_ToR.Location = new System.Drawing.Point(259, 0);
            this.tc_ToR.Name = "tc_ToR";
            this.tc_ToR.SelectedTabPage = this.tabPage1;
            this.tc_ToR.Size = new System.Drawing.Size(742, 491);
            this.tc_ToR.TabIndex = 8;
            this.tc_ToR.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.tabPage1,
            this.tabPage2});
            this.tc_ToR.SelectedPageChanged += new DevExpress.XtraTab.TabPageChangedEventHandler(this.Tc_ToR_SelectedIndexChanged);
            // 
            // pal_YJDJ
            // 
            this.pal_YJDJ.Location = new System.Drawing.Point(0, 0);
            this.pal_YJDJ.Name = "pal_YJDJ";
            this.pal_YJDJ.Size = new System.Drawing.Size(200, 100);
            this.pal_YJDJ.TabIndex = 0;
            // 
            // pal_XTSY
            // 
            this.pal_XTSY.Location = new System.Drawing.Point(0, 0);
            this.pal_XTSY.Name = "pal_XTSY";
            this.pal_XTSY.Size = new System.Drawing.Size(200, 100);
            this.pal_XTSY.TabIndex = 0;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(0, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(100, 23);
            this.label5.TabIndex = 0;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(0, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(100, 23);
            this.label4.TabIndex = 0;
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
            this.ac_LeftMenu.Size = new System.Drawing.Size(259, 491);
            this.ac_LeftMenu.TabIndex = 11;
            // 
            // acg_Register
            // 
            this.acg_Register.Elements.AddRange(new DevExpress.XtraBars.Navigation.AccordionControlElement[] {
            this.ace_all});
            this.acg_Register.Expanded = true;
            this.acg_Register.Image = ((System.Drawing.Image)(resources.GetObject("acg_Register.Image")));
            this.acg_Register.Name = "acg_Register";
            this.acg_Register.Text = "移交登记";
            this.acg_Register.TextToImageDistance = 10;
            // 
            // ace_all
            // 
            this.ace_all.Name = "ace_all";
            this.ace_all.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.ace_all.Text = "全部来源单位";
            this.ace_all.Click += new System.EventHandler(this.Element_Click);
            // 
            // Frm_ToR
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1001, 491);
            this.Controls.Add(this.tc_ToR);
            this.Controls.Add(this.ac_LeftMenu);
            this.DoubleBuffered = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Frm_ToR";
            this.Text = "移交登记";
            this.Load += new System.EventHandler(this.Frm_ToR_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_GPDJ)).EndInit();
            this.tabPage1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txt_Search.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_SWDJ)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txt_CDSearch.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tc_ToR)).EndInit();
            this.tc_ToR.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ac_LeftMenu)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pal_YJDJ;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel pal_XTSY;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridView dgv_GPDJ;
        private DevExpress.XtraTab.XtraTabPage tabPage1;
        private System.Windows.Forms.DataGridView dgv_SWDJ;
        private DevExpress.XtraTab.XtraTabPage tabPage2;
        private KyoControl.KyoTabControl tc_ToR;
        private System.Windows.Forms.GroupBox groupBox2;
        private KyoControl.KyoButton btn_Back;
        private KyoControl.KyoButton btn_Delete;
        private KyoControl.KyoButton btn_Add;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ComboBox cbo_Status;
        private System.Windows.Forms.Label label1;
        private KyoControl.KyoButton btn_CD_Delete;
        private KyoControl.KyoAccordion ac_LeftMenu;
        private DevExpress.XtraBars.Navigation.AccordionControlElement acg_Register;
        private DevExpress.XtraBars.Navigation.AccordionControlElement ace_all;
        private DevExpress.XtraEditors.SearchControl txt_Search;
        private DevExpress.XtraEditors.SearchControl txt_CDSearch;
    }
}