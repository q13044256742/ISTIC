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
            this.ac_LeftMenu = new 科技计划项目档案数据采集管理系统.KyoControl.KyoAccordion();
            this.acg_WorkCount = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.acg_Register = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.ace_all = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.panel2 = new System.Windows.Forms.Panel();
            this.view = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.searchControl = new DevExpress.XtraEditors.SearchControl();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.ac_LeftMenu)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.view)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.searchControl.Properties)).BeginInit();
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
            this.acg_WorkCount,
            this.acg_Register});
            this.ac_LeftMenu.Location = new System.Drawing.Point(0, 0);
            this.ac_LeftMenu.LookAndFeel.SkinName = "McSkin";
            this.ac_LeftMenu.LookAndFeel.UseDefaultLookAndFeel = false;
            this.ac_LeftMenu.Name = "ac_LeftMenu";
            this.ac_LeftMenu.ScrollBarMode = DevExpress.XtraBars.Navigation.ScrollBarMode.Auto;
            this.ac_LeftMenu.ShowToolTips = false;
            this.ac_LeftMenu.Size = new System.Drawing.Size(275, 641);
            this.ac_LeftMenu.TabIndex = 13;
            // 
            // acg_WorkCount
            // 
            this.acg_WorkCount.Appearance.Hovered.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold);
            this.acg_WorkCount.Appearance.Hovered.ForeColor = System.Drawing.Color.Navy;
            this.acg_WorkCount.Appearance.Hovered.Options.UseFont = true;
            this.acg_WorkCount.Appearance.Hovered.Options.UseForeColor = true;
            this.acg_WorkCount.Appearance.Normal.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.acg_WorkCount.Appearance.Normal.Options.UseFont = true;
            this.acg_WorkCount.Appearance.Pressed.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold);
            this.acg_WorkCount.Appearance.Pressed.ForeColor = System.Drawing.Color.Navy;
            this.acg_WorkCount.Appearance.Pressed.Options.UseFont = true;
            this.acg_WorkCount.Appearance.Pressed.Options.UseForeColor = true;
            this.acg_WorkCount.Image = ((System.Drawing.Image)(resources.GetObject("acg_WorkCount.Image")));
            this.acg_WorkCount.Name = "acg_WorkCount";
            this.acg_WorkCount.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.acg_WorkCount.Text = "工作量统计";
            this.acg_WorkCount.TextToImageDistance = 10;
            this.acg_WorkCount.Click += new System.EventHandler(this.acg_WorkCount_Click);
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
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.view);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(275, 69);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(989, 572);
            this.panel2.TabIndex = 15;
            // 
            // view
            // 
            this.view.AllowUserToAddRows = false;
            this.view.AllowUserToDeleteRows = false;
            this.view.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.view.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.view.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.view.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.view.Dock = System.Windows.Forms.DockStyle.Fill;
            this.view.Location = new System.Drawing.Point(0, 0);
            this.view.Name = "view";
            this.view.ReadOnly = true;
            this.view.RowTemplate.Height = 23;
            this.view.Size = new System.Drawing.Size(989, 572);
            this.view.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.searchControl);
            this.panel1.Controls.Add(this.simpleButton1);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel1.Location = new System.Drawing.Point(275, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(989, 69);
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
            // simpleButton1
            // 
            this.simpleButton1.Appearance.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.simpleButton1.Appearance.Options.UseFont = true;
            this.simpleButton1.Location = new System.Drawing.Point(723, 18);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(79, 32);
            this.simpleButton1.TabIndex = 2;
            this.simpleButton1.Text = "查询(&Q)";
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
            // Frm_Statistics
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1264, 641);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.ac_LeftMenu);
            this.Name = "Frm_Statistics";
            this.Text = "统计分析";
            this.Load += new System.EventHandler(this.Frm_Statistics_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ac_LeftMenu)).EndInit();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.view)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.searchControl.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private KyoControl.KyoAccordion ac_LeftMenu;
        private DevExpress.XtraBars.Navigation.AccordionControlElement acg_Register;
        private DevExpress.XtraBars.Navigation.AccordionControlElement ace_all;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.DataGridView view;
        private System.Windows.Forms.Panel panel1;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraEditors.SearchControl searchControl;
        private DevExpress.XtraBars.Navigation.AccordionControlElement acg_WorkCount;
    }
}