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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_CG));
            this.dgv_WorkLog = new 科技计划项目档案数据采集管理系统.KyoControl.HeaderUnitView(this.components);
            this.txt_Search = new DevExpress.XtraEditors.SearchControl();
            this.cbo_CompanyList = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_Back = new 科技计划项目档案数据采集管理系统.KyoControl.KyoButton();
            this.ace_LeftMenu = new 科技计划项目档案数据采集管理系统.KyoControl.KyoAccordion();
            this.acg_Worked = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.ac_Login = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.ac_Working = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.ac_Worked = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_WorkLog)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_Search.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ace_LeftMenu)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgv_WorkLog
            // 
            this.dgv_WorkLog.AllowUserToAddRows = false;
            this.dgv_WorkLog.AllowUserToDeleteRows = false;
            this.dgv_WorkLog.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv_WorkLog.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgv_WorkLog.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgv_WorkLog.CellHeight = 17;
            this.dgv_WorkLog.ColumnDeep = 1;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("微软雅黑", 12F);
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgv_WorkLog.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dgv_WorkLog.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_WorkLog.ColumnTreeView = null;
            this.dgv_WorkLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_WorkLog.Location = new System.Drawing.Point(223, 52);
            this.dgv_WorkLog.MergeColumnNames = ((System.Collections.Generic.List<string>)(resources.GetObject("dgv_WorkLog.MergeColumnNames")));
            this.dgv_WorkLog.Name = "dgv_WorkLog";
            this.dgv_WorkLog.ReadOnly = true;
            this.dgv_WorkLog.RefreshAtHscroll = false;
            this.dgv_WorkLog.RowHeadersVisible = false;
            this.dgv_WorkLog.RowTemplate.Height = 23;
            this.dgv_WorkLog.Size = new System.Drawing.Size(778, 439);
            this.dgv_WorkLog.TabIndex = 15;
            this.dgv_WorkLog.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Dgv_WorkLog_CellClick);
            // 
            // txt_Search
            // 
            this.txt_Search.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txt_Search.Location = new System.Drawing.Point(468, 13);
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
            // 
            // cbo_CompanyList
            // 
            this.cbo_CompanyList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo_CompanyList.FormattingEnabled = true;
            this.cbo_CompanyList.Location = new System.Drawing.Point(88, 13);
            this.cbo_CompanyList.Name = "cbo_CompanyList";
            this.cbo_CompanyList.Size = new System.Drawing.Size(229, 29);
            this.cbo_CompanyList.TabIndex = 7;
            this.cbo_CompanyList.SelectionChangeCommitted += new System.EventHandler(this.Cbo_CompanyList_SelectionChangeCommitted);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(3, 18);
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
            this.btn_Back.Location = new System.Drawing.Point(704, 12);
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
            this.ace_LeftMenu.Size = new System.Drawing.Size(223, 491);
            this.ace_LeftMenu.TabIndex = 16;
            // 
            // acg_Worked
            // 
            this.acg_Worked.Elements.AddRange(new DevExpress.XtraBars.Navigation.AccordionControlElement[] {
            this.ac_Login,
            this.ac_Working,
            this.ac_Worked});
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
            // panel1
            // 
            this.panel1.Controls.Add(this.txt_Search);
            this.panel1.Controls.Add(this.btn_Back);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.cbo_CompanyList);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(223, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(778, 52);
            this.panel1.TabIndex = 17;
            // 
            // Frm_CG
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1001, 491);
            this.Controls.Add(this.dgv_WorkLog);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.ace_LeftMenu);
            this.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Frm_CG";
            this.ShowInTaskbar = false;
            this.Text = "著录加工";
            this.Load += new System.EventHandler(this.Frm_CG_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_WorkLog)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_Search.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ace_LeftMenu)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private KyoControl.KyoButton btn_Back;
        private System.Windows.Forms.ComboBox cbo_CompanyList;
        private System.Windows.Forms.Label label1;
        private KyoControl.HeaderUnitView dgv_WorkLog;
        private KyoControl.KyoAccordion ace_LeftMenu;
        private DevExpress.XtraBars.Navigation.AccordionControlElement acg_Worked;
        private DevExpress.XtraBars.Navigation.AccordionControlElement ac_Login;
        private DevExpress.XtraBars.Navigation.AccordionControlElement ac_Working;
        private DevExpress.XtraBars.Navigation.AccordionControlElement ac_Worked;
        private DevExpress.XtraEditors.SearchControl txt_Search;
        private System.Windows.Forms.Panel panel1;
    }
}