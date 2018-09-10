namespace 科技计划项目档案数据采集管理系统
{
    partial class Frm_Download
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_Download));
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject5 = new DevExpress.Utils.SerializableAppearanceObject();
            this.gridControl = new DevExpress.XtraGrid.GridControl();
            this.view = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.id = new DevExpress.XtraGrid.Columns.GridColumn();
            this.name = new DevExpress.XtraGrid.Columns.GridColumn();
            this.size = new DevExpress.XtraGrid.Columns.GridColumn();
            this.date = new DevExpress.XtraGrid.Columns.GridColumn();
            this.download = new DevExpress.XtraGrid.Columns.GridColumn();
            this.downloadButton = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.btn_Upload = new DevExpress.XtraEditors.SimpleButton();
            this.btn_Delete = new DevExpress.XtraEditors.SimpleButton();
            this.progressBar = new DevExpress.XtraEditors.MarqueeProgressBarControl();
            this.btn_Refresh = new DevExpress.XtraEditors.SimpleButton();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.panel1 = new System.Windows.Forms.Panel();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.txt_search = new DevExpress.XtraEditors.SearchControl();
            this.panel3 = new System.Windows.Forms.Panel();
            this.treeList = new DevExpress.XtraTreeList.TreeList();
            this.treeListColumn1 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.repositoryItemHyperLinkEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.view)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.downloadButton)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.progressBar.Properties)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txt_search.Properties)).BeginInit();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treeList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemHyperLinkEdit1)).BeginInit();
            this.SuspendLayout();
            // 
            // gridControl
            // 
            this.gridControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridControl.Location = new System.Drawing.Point(3, 45);
            this.gridControl.MainView = this.view;
            this.gridControl.Name = "gridControl";
            this.gridControl.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.downloadButton});
            this.gridControl.Size = new System.Drawing.Size(1018, 640);
            this.gridControl.TabIndex = 0;
            this.gridControl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.view});
            // 
            // view
            // 
            this.view.Appearance.HeaderPanel.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.view.Appearance.HeaderPanel.Options.UseFont = true;
            this.view.Appearance.Row.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.view.Appearance.Row.Options.UseFont = true;
            this.view.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.id,
            this.name,
            this.size,
            this.date,
            this.download});
            this.view.GridControl = this.gridControl;
            this.view.Name = "view";
            this.view.OptionsView.ShowGroupPanel = false;
            // 
            // id
            // 
            this.id.AppearanceCell.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.id.AppearanceCell.Options.UseFont = true;
            this.id.Caption = "序号";
            this.id.FieldName = "at_id";
            this.id.Name = "id";
            this.id.Width = 118;
            // 
            // name
            // 
            this.name.AppearanceCell.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.name.AppearanceCell.Options.UseFont = true;
            this.name.Caption = "文件名";
            this.name.FieldName = "at_name";
            this.name.Name = "name";
            this.name.OptionsColumn.AllowEdit = false;
            this.name.Visible = true;
            this.name.VisibleIndex = 0;
            this.name.Width = 650;
            // 
            // size
            // 
            this.size.AppearanceCell.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.size.AppearanceCell.Options.UseFont = true;
            this.size.Caption = "大小(KB)";
            this.size.FieldName = "at_size";
            this.size.Name = "size";
            this.size.OptionsColumn.AllowEdit = false;
            this.size.Visible = true;
            this.size.VisibleIndex = 1;
            this.size.Width = 124;
            // 
            // date
            // 
            this.date.AppearanceCell.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.date.AppearanceCell.Options.UseFont = true;
            this.date.Caption = "上传时间";
            this.date.FieldName = "at_date";
            this.date.Name = "date";
            this.date.OptionsColumn.AllowEdit = false;
            this.date.Visible = true;
            this.date.VisibleIndex = 2;
            this.date.Width = 170;
            // 
            // download
            // 
            this.download.AppearanceCell.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.download.AppearanceCell.Options.UseFont = true;
            this.download.AppearanceCell.Options.UseTextOptions = true;
            this.download.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.download.Caption = "操作";
            this.download.ColumnEdit = this.downloadButton;
            this.download.Name = "download";
            this.download.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.download.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways;
            this.download.Visible = true;
            this.download.VisibleIndex = 3;
            this.download.Width = 177;
            // 
            // downloadButton
            // 
            this.downloadButton.AutoHeight = false;
            serializableAppearanceObject5.Font = new System.Drawing.Font("华文中宋", 12F);
            serializableAppearanceObject5.Options.UseFont = true;
            serializableAppearanceObject5.Options.UseTextOptions = true;
            serializableAppearanceObject5.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            serializableAppearanceObject5.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.downloadButton.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "下载", -1, true, true, true, DevExpress.XtraEditors.ImageLocation.MiddleLeft, ((System.Drawing.Image)(resources.GetObject("downloadButton.Buttons"))), new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject5, "", null, null, true)});
            this.downloadButton.Name = "downloadButton";
            this.downloadButton.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.downloadButton.MouseDown += new System.Windows.Forms.MouseEventHandler(this.DownloadButton_MouseDown);
            // 
            // btn_Upload
            // 
            this.btn_Upload.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Upload.Appearance.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.btn_Upload.Appearance.Options.UseFont = true;
            this.btn_Upload.Image = ((System.Drawing.Image)(resources.GetObject("btn_Upload.Image")));
            this.btn_Upload.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btn_Upload.ImageToTextIndent = 0;
            this.btn_Upload.Location = new System.Drawing.Point(795, 8);
            this.btn_Upload.Name = "btn_Upload";
            this.btn_Upload.Size = new System.Drawing.Size(66, 31);
            this.btn_Upload.TabIndex = 1;
            this.btn_Upload.Text = "上传";
            this.btn_Upload.Click += new System.EventHandler(this.Btn_Upload_Click);
            // 
            // btn_Delete
            // 
            this.btn_Delete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Delete.Appearance.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.btn_Delete.Appearance.Options.UseFont = true;
            this.btn_Delete.Image = ((System.Drawing.Image)(resources.GetObject("btn_Delete.Image")));
            this.btn_Delete.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btn_Delete.ImageToTextIndent = 0;
            this.btn_Delete.Location = new System.Drawing.Point(867, 8);
            this.btn_Delete.Name = "btn_Delete";
            this.btn_Delete.Size = new System.Drawing.Size(66, 31);
            this.btn_Delete.TabIndex = 2;
            this.btn_Delete.Text = "删除";
            this.btn_Delete.Click += new System.EventHandler(this.Btn_Delete_Click);
            // 
            // progressBar
            // 
            this.progressBar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.progressBar.EditValue = "";
            this.progressBar.Location = new System.Drawing.Point(1, 673);
            this.progressBar.Name = "progressBar";
            this.progressBar.Properties.Appearance.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.progressBar.Properties.MarqueeWidth = 10;
            this.progressBar.Properties.ShowTitle = true;
            this.progressBar.Properties.Stopped = true;
            this.progressBar.Size = new System.Drawing.Size(234, 14);
            this.progressBar.TabIndex = 5;
            this.progressBar.Visible = false;
            // 
            // btn_Refresh
            // 
            this.btn_Refresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Refresh.Appearance.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.btn_Refresh.Appearance.Options.UseFont = true;
            this.btn_Refresh.Image = ((System.Drawing.Image)(resources.GetObject("btn_Refresh.Image")));
            this.btn_Refresh.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btn_Refresh.ImageToTextIndent = 0;
            this.btn_Refresh.Location = new System.Drawing.Point(939, 8);
            this.btn_Refresh.Name = "btn_Refresh";
            this.btn_Refresh.Size = new System.Drawing.Size(66, 31);
            this.btn_Refresh.TabIndex = 6;
            this.btn_Refresh.Text = "刷新";
            this.btn_Refresh.Click += new System.EventHandler(this.Frm_Download_Load);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.simpleButton1);
            this.panel1.Controls.Add(this.labelControl1);
            this.panel1.Controls.Add(this.txt_search);
            this.panel1.Controls.Add(this.btn_Refresh);
            this.panel1.Controls.Add(this.gridControl);
            this.panel1.Controls.Add(this.btn_Upload);
            this.panel1.Controls.Add(this.btn_Delete);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(234, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1023, 688);
            this.panel1.TabIndex = 7;
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelControl1.Location = new System.Drawing.Point(9, 13);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(80, 22);
            this.labelControl1.TabIndex = 8;
            this.labelControl1.Text = "信息检索：";
            // 
            // txt_search
            // 
            this.txt_search.Location = new System.Drawing.Point(95, 8);
            this.txt_search.Name = "txt_search";
            this.txt_search.Properties.Appearance.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.txt_search.Properties.Appearance.Options.UseFont = true;
            this.txt_search.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
            this.txt_search.Properties.NullValuePrompt = "输入关键字快速检索";
            this.txt_search.Properties.ShowClearButton = false;
            this.txt_search.Properties.ShowSearchButton = false;
            this.txt_search.Size = new System.Drawing.Size(247, 30);
            this.txt_search.TabIndex = 7;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.progressBar);
            this.panel3.Controls.Add(this.treeList);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(234, 688);
            this.panel3.TabIndex = 9;
            // 
            // treeList
            // 
            this.treeList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.treeList.Appearance.Caption.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.treeList.Appearance.Caption.Options.UseFont = true;
            this.treeList.Appearance.HeaderPanel.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.treeList.Appearance.HeaderPanel.Options.UseFont = true;
            this.treeList.Appearance.Row.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.treeList.Appearance.Row.Options.UseFont = true;
            this.treeList.Caption = "信息公开目录";
            this.treeList.CausesValidation = false;
            this.treeList.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.treeListColumn1});
            this.treeList.Location = new System.Drawing.Point(0, 0);
            this.treeList.Name = "treeList";
            this.treeList.BeginUnboundLoad();
            this.treeList.AppendNode(new object[] {
            "法规制度"}, -1);
            this.treeList.AppendNode(new object[] {
            "部门规章"}, -1);
            this.treeList.AppendNode(new object[] {
            "标准规范"}, -1);
            this.treeList.AppendNode(new object[] {
            "档案清单"}, -1);
            this.treeList.AppendNode(new object[] {
            "工作文件"}, -1);
            this.treeList.AppendNode(new object[] {
            "其他"}, -1);
            this.treeList.EndUnboundLoad();
            this.treeList.OptionsBehavior.Editable = false;
            this.treeList.OptionsBehavior.ReadOnly = true;
            this.treeList.OptionsView.ColumnHeaderAutoHeight = DevExpress.Utils.DefaultBoolean.True;
            this.treeList.OptionsView.ShowCaption = true;
            this.treeList.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemHyperLinkEdit1});
            this.treeList.Size = new System.Drawing.Size(234, 673);
            this.treeList.TabIndex = 0;
            this.treeList.FocusedNodeChanged += new DevExpress.XtraTreeList.FocusedNodeChangedEventHandler(this.treeList_FocusedNodeChanged);
            // 
            // treeListColumn1
            // 
            this.treeListColumn1.Caption = "文件类别";
            this.treeListColumn1.FieldName = "文件类别";
            this.treeListColumn1.MinWidth = 34;
            this.treeListColumn1.Name = "treeListColumn1";
            this.treeListColumn1.Visible = true;
            this.treeListColumn1.VisibleIndex = 0;
            // 
            // repositoryItemHyperLinkEdit1
            // 
            this.repositoryItemHyperLinkEdit1.AutoHeight = false;
            this.repositoryItemHyperLinkEdit1.Name = "repositoryItemHyperLinkEdit1";
            // 
            // simpleButton1
            // 
            this.simpleButton1.Appearance.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.simpleButton1.Appearance.Options.UseFont = true;
            this.simpleButton1.Image = ((System.Drawing.Image)(resources.GetObject("simpleButton1.Image")));
            this.simpleButton1.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.simpleButton1.Location = new System.Drawing.Point(348, 10);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(73, 27);
            this.simpleButton1.TabIndex = 9;
            this.simpleButton1.Text = "查询";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // Frm_Download
            // 
            this.Appearance.Options.UseFont = true;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1257, 688);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel3);
            this.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Name = "Frm_Download";
            this.Text = "相关下载";
            this.Load += new System.EventHandler(this.Frm_Download_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.view)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.downloadButton)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.progressBar.Properties)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txt_search.Properties)).EndInit();
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.treeList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemHyperLinkEdit1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridControl;
        private DevExpress.XtraGrid.Views.Grid.GridView view;
        private DevExpress.XtraGrid.Columns.GridColumn name;
        private DevExpress.XtraGrid.Columns.GridColumn size;
        private DevExpress.XtraGrid.Columns.GridColumn date;
        private DevExpress.XtraGrid.Columns.GridColumn download;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit downloadButton;
        private DevExpress.XtraGrid.Columns.GridColumn id;
        private DevExpress.XtraEditors.SimpleButton btn_Upload;
        private DevExpress.XtraEditors.SimpleButton btn_Delete;
        private DevExpress.XtraEditors.MarqueeProgressBarControl progressBar;
        private DevExpress.XtraEditors.SimpleButton btn_Refresh;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel3;
        private DevExpress.XtraTreeList.TreeList treeList;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn1;
        private DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit repositoryItemHyperLinkEdit1;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.SearchControl txt_search;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
    }
}