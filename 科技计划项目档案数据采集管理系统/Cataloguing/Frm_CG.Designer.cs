namespace 科技计划项目档案数据采集管理系统
{
    partial class Frm_CG
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_CG));
            this.view = new 科技计划项目档案数据采集管理系统.KyoControl.HeaderUnitView(this.components);
            this.txt_Search = new DevExpress.XtraEditors.SearchControl();
            this.cbo_CompanyList = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_Back = new 科技计划项目档案数据采集管理系统.KyoControl.KyoButton();
            this.ace_LeftMenu = new 科技计划项目档案数据采集管理系统.KyoControl.KyoAccordion();
            this.acg_Worked = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.ac_Login = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.ac_Working = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.ac_Worked = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.ac_MyWork = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pal_UnitList = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.txt_SearchData_F = new DevExpress.XtraEditors.TextEdit();
            this.txt_SearchDate_S = new DevExpress.XtraEditors.TextEdit();
            this.btn_MyWorkQuery = new 科技计划项目档案数据采集管理系统.KyoControl.KyoButton();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.view)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_Search.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ace_LeftMenu)).BeginInit();
            this.panel1.SuspendLayout();
            this.pal_UnitList.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txt_SearchData_F.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_SearchDate_S.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // view
            // 
            this.view.AllowUserToAddRows = false;
            this.view.AllowUserToDeleteRows = false;
            this.view.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.view.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.view.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.view.CellHeight = 17;
            this.view.ColumnDeep = 1;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("微软雅黑", 12F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.view.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.view.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.view.ColumnTreeView = null;
            this.view.Dock = System.Windows.Forms.DockStyle.Fill;
            this.view.Location = new System.Drawing.Point(223, 104);
            this.view.MergeColumnNames = ((System.Collections.Generic.List<string>)(resources.GetObject("view.MergeColumnNames")));
            this.view.Name = "view";
            this.view.ReadOnly = true;
            this.view.RefreshAtHscroll = false;
            this.view.RowHeadersVisible = false;
            this.view.RowTemplate.Height = 23;
            this.view.Size = new System.Drawing.Size(1023, 600);
            this.view.TabIndex = 15;
            this.view.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Dgv_WorkLog_CellClick);
            // 
            // txt_Search
            // 
            this.txt_Search.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txt_Search.Location = new System.Drawing.Point(713, 13);
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
            this.txt_Search.TabIndex = 8;
            this.txt_Search.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Txt_Search_KeyDown);
            // 
            // cbo_CompanyList
            // 
            this.cbo_CompanyList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo_CompanyList.FormattingEnabled = true;
            this.cbo_CompanyList.Location = new System.Drawing.Point(98, 5);
            this.cbo_CompanyList.Name = "cbo_CompanyList";
            this.cbo_CompanyList.Size = new System.Drawing.Size(229, 29);
            this.cbo_CompanyList.TabIndex = 7;
            this.cbo_CompanyList.SelectionChangeCommitted += new System.EventHandler(this.Cbo_CompanyList_SelectionChangeCommitted);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(13, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 19);
            this.label1.TabIndex = 6;
            this.label1.Text = "来源单位：";
            // 
            // btn_Back
            // 
            this.btn_Back.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Back.Appearance.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Back.Appearance.Options.UseFont = true;
            this.btn_Back.Image = ((System.Drawing.Image)(resources.GetObject("btn_Back.Image")));
            this.btn_Back.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btn_Back.ImageToTextIndent = 5;
            this.btn_Back.Location = new System.Drawing.Point(949, 12);
            this.btn_Back.Name = "btn_Back";
            this.btn_Back.Size = new System.Drawing.Size(67, 30);
            this.btn_Back.TabIndex = 5;
            this.btn_Back.Text = "返回";
            this.btn_Back.Click += new System.EventHandler(this.Btn_Back_Click);
            // 
            // ace_LeftMenu
            // 
            this.ace_LeftMenu.AllowItemSelection = true;
            this.ace_LeftMenu.Appearance.Group.Hovered.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.ace_LeftMenu.Appearance.Group.Hovered.Options.UseFont = true;
            this.ace_LeftMenu.Appearance.Group.Normal.Font = new System.Drawing.Font("微软雅黑", 11F, System.Drawing.FontStyle.Bold);
            this.ace_LeftMenu.Appearance.Group.Normal.Options.UseFont = true;
            this.ace_LeftMenu.Appearance.Group.Pressed.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.ace_LeftMenu.Appearance.Group.Pressed.Options.UseFont = true;
            this.ace_LeftMenu.Appearance.Hint.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.ace_LeftMenu.Appearance.Hint.Options.UseFont = true;
            this.ace_LeftMenu.Appearance.Item.Hovered.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.ace_LeftMenu.Appearance.Item.Hovered.Options.UseFont = true;
            this.ace_LeftMenu.Appearance.Item.Normal.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.ace_LeftMenu.Appearance.Item.Normal.Options.UseFont = true;
            this.ace_LeftMenu.Appearance.Item.Pressed.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Bold);
            this.ace_LeftMenu.Appearance.Item.Pressed.Options.UseFont = true;
            this.ace_LeftMenu.Dock = System.Windows.Forms.DockStyle.Left;
            this.ace_LeftMenu.Elements.AddRange(new DevExpress.XtraBars.Navigation.AccordionControlElement[] {
            this.acg_Worked});
            this.ace_LeftMenu.Location = new System.Drawing.Point(0, 0);
            this.ace_LeftMenu.LookAndFeel.SkinName = "McSkin";
            this.ace_LeftMenu.LookAndFeel.UseDefaultLookAndFeel = false;
            this.ace_LeftMenu.Name = "ace_LeftMenu";
            this.ace_LeftMenu.ScrollBarMode = DevExpress.XtraBars.Navigation.ScrollBarMode.Hidden;
            this.ace_LeftMenu.Size = new System.Drawing.Size(223, 704);
            this.ace_LeftMenu.TabIndex = 16;
            this.ace_LeftMenu.SelectedElementChanged += new DevExpress.XtraBars.Navigation.SelectedElementChangedEventHandler(this.Ace_LeftMenu_SelectedElementChanged);
            // 
            // acg_Worked
            // 
            this.acg_Worked.Elements.AddRange(new DevExpress.XtraBars.Navigation.AccordionControlElement[] {
            this.ac_Login,
            this.ac_Working,
            this.ac_Worked,
            this.ac_MyWork});
            this.acg_Worked.Expanded = true;
            this.acg_Worked.Height = 50;
            this.acg_Worked.Image = ((System.Drawing.Image)(resources.GetObject("acg_Worked.Image")));
            this.acg_Worked.Name = "acg_Worked";
            this.acg_Worked.Text = "著录加工";
            this.acg_Worked.TextToImageDistance = 10;
            // 
            // ac_Login
            // 
            this.ac_Login.Appearance.Hovered.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.ac_Login.Appearance.Hovered.Options.UseFont = true;
            this.ac_Login.Appearance.Normal.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.ac_Login.Appearance.Normal.Options.UseFont = true;
            this.ac_Login.Appearance.Pressed.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.ac_Login.Appearance.Pressed.Options.UseFont = true;
            this.ac_Login.Height = 35;
            this.ac_Login.Image = ((System.Drawing.Image)(resources.GetObject("ac_Login.Image")));
            this.ac_Login.ImageLayoutMode = DevExpress.XtraBars.Navigation.ImageLayoutMode.Squeeze;
            this.ac_Login.Name = "ac_Login";
            this.ac_Login.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.ac_Login.Text = "加工登记";
            this.ac_Login.TextToImageDistance = 15;
            this.ac_Login.Click += new System.EventHandler(this.Sub_Menu_Click);
            // 
            // ac_Working
            // 
            this.ac_Working.Appearance.Normal.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.ac_Working.Appearance.Normal.Options.UseFont = true;
            this.ac_Working.Height = 35;
            this.ac_Working.Image = ((System.Drawing.Image)(resources.GetObject("ac_Working.Image")));
            this.ac_Working.ImageLayoutMode = DevExpress.XtraBars.Navigation.ImageLayoutMode.Squeeze;
            this.ac_Working.Name = "ac_Working";
            this.ac_Working.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.ac_Working.Text = "加工中";
            this.ac_Working.TextToImageDistance = 15;
            this.ac_Working.Click += new System.EventHandler(this.Sub_Menu_Click);
            // 
            // ac_Worked
            // 
            this.ac_Worked.Appearance.Normal.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.ac_Worked.Appearance.Normal.Options.UseFont = true;
            this.ac_Worked.Height = 35;
            this.ac_Worked.Image = ((System.Drawing.Image)(resources.GetObject("ac_Worked.Image")));
            this.ac_Worked.ImageLayoutMode = DevExpress.XtraBars.Navigation.ImageLayoutMode.Squeeze;
            this.ac_Worked.Name = "ac_Worked";
            this.ac_Worked.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.ac_Worked.Text = "已返工(0)";
            this.ac_Worked.TextToImageDistance = 15;
            this.ac_Worked.Click += new System.EventHandler(this.Sub_Menu_Click);
            // 
            // ac_MyWork
            // 
            this.ac_MyWork.Appearance.Normal.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.ac_MyWork.Appearance.Normal.Options.UseFont = true;
            this.ac_MyWork.Height = 35;
            this.ac_MyWork.Image = ((System.Drawing.Image)(resources.GetObject("ac_MyWork.Image")));
            this.ac_MyWork.ImageLayoutMode = DevExpress.XtraBars.Navigation.ImageLayoutMode.Squeeze;
            this.ac_MyWork.Name = "ac_MyWork";
            this.ac_MyWork.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.ac_MyWork.Text = "我的加工";
            this.ac_MyWork.TextToImageDistance = 15;
            this.ac_MyWork.Click += new System.EventHandler(this.Sub_Menu_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.pal_UnitList);
            this.panel1.Controls.Add(this.txt_Search);
            this.panel1.Controls.Add(this.btn_Back);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(223, 52);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1023, 52);
            this.panel1.TabIndex = 17;
            // 
            // pal_UnitList
            // 
            this.pal_UnitList.Controls.Add(this.label1);
            this.pal_UnitList.Controls.Add(this.cbo_CompanyList);
            this.pal_UnitList.Location = new System.Drawing.Point(6, 7);
            this.pal_UnitList.Name = "pal_UnitList";
            this.pal_UnitList.Size = new System.Drawing.Size(345, 39);
            this.pal_UnitList.TabIndex = 18;
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
            this.panel2.Location = new System.Drawing.Point(223, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1023, 52);
            this.panel2.TabIndex = 19;
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
            this.comboBox1.Size = new System.Drawing.Size(142, 29);
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
            this.label3.Size = new System.Drawing.Size(22, 21);
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
            // Frm_CG
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1246, 704);
            this.Controls.Add(this.view);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.ace_LeftMenu);
            this.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Frm_CG";
            this.ShowInTaskbar = false;
            this.Text = "著录加工";
            this.Load += new System.EventHandler(this.Frm_CG_Load);
            ((System.ComponentModel.ISupportInitialize)(this.view)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_Search.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ace_LeftMenu)).EndInit();
            this.panel1.ResumeLayout(false);
            this.pal_UnitList.ResumeLayout(false);
            this.pal_UnitList.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txt_SearchData_F.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_SearchDate_S.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private KyoControl.KyoButton btn_Back;
        private System.Windows.Forms.ComboBox cbo_CompanyList;
        private System.Windows.Forms.Label label1;
        private KyoControl.HeaderUnitView view;
        private KyoControl.KyoAccordion ace_LeftMenu;
        private DevExpress.XtraBars.Navigation.AccordionControlElement acg_Worked;
        private DevExpress.XtraBars.Navigation.AccordionControlElement ac_Login;
        private DevExpress.XtraBars.Navigation.AccordionControlElement ac_Working;
        private DevExpress.XtraBars.Navigation.AccordionControlElement ac_Worked;
        private DevExpress.XtraEditors.SearchControl txt_Search;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel pal_UnitList;
        private DevExpress.XtraBars.Navigation.AccordionControlElement ac_MyWork;
        private System.Windows.Forms.Panel panel2;
        private KyoControl.KyoButton btn_MyWorkQuery;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private DevExpress.XtraEditors.TextEdit txt_SearchData_F;
        private DevExpress.XtraEditors.TextEdit txt_SearchDate_S;
        private System.Windows.Forms.ComboBox comboBox1;
    }
}