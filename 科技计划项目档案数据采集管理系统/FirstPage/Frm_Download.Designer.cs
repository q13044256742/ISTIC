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
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
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
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.progressBar = new DevExpress.XtraEditors.MarqueeProgressBarControl();
            this.btn_Refresh = new DevExpress.XtraEditors.SimpleButton();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.view)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.downloadButton)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.progressBar.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // gridControl
            // 
            this.gridControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridControl.Location = new System.Drawing.Point(0, 49);
            this.gridControl.MainView = this.view;
            this.gridControl.Name = "gridControl";
            this.gridControl.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.downloadButton});
            this.gridControl.Size = new System.Drawing.Size(1257, 622);
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
            serializableAppearanceObject1.Font = new System.Drawing.Font("华文中宋", 12F);
            serializableAppearanceObject1.Options.UseFont = true;
            serializableAppearanceObject1.Options.UseTextOptions = true;
            serializableAppearanceObject1.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            serializableAppearanceObject1.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.downloadButton.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "下载", -1, true, true, true, DevExpress.XtraEditors.ImageLocation.MiddleLeft, ((System.Drawing.Image)(resources.GetObject("downloadButton.Buttons"))), new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, "", null, null, true)});
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
            this.btn_Upload.Location = new System.Drawing.Point(1037, 11);
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
            this.btn_Delete.Location = new System.Drawing.Point(1109, 11);
            this.btn_Delete.Name = "btn_Delete";
            this.btn_Delete.Size = new System.Drawing.Size(66, 31);
            this.btn_Delete.TabIndex = 2;
            this.btn_Delete.Text = "删除";
            this.btn_Delete.Click += new System.EventHandler(this.Btn_Delete_Click);
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Appearance.Image = ((System.Drawing.Image)(resources.GetObject("labelControl1.Appearance.Image")));
            this.labelControl1.ImageAlignToText = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.labelControl1.Location = new System.Drawing.Point(25, 6);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(157, 36);
            this.labelControl1.TabIndex = 3;
            this.labelControl1.Text = "附件下载列表";
            // 
            // progressBar
            // 
            this.progressBar.EditValue = "";
            this.progressBar.Location = new System.Drawing.Point(0, 672);
            this.progressBar.Name = "progressBar";
            this.progressBar.Properties.Appearance.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.progressBar.Properties.MarqueeWidth = 10;
            this.progressBar.Properties.ShowTitle = true;
            this.progressBar.Properties.Stopped = true;
            this.progressBar.Size = new System.Drawing.Size(195, 15);
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
            this.btn_Refresh.Location = new System.Drawing.Point(1181, 11);
            this.btn_Refresh.Name = "btn_Refresh";
            this.btn_Refresh.Size = new System.Drawing.Size(66, 31);
            this.btn_Refresh.TabIndex = 6;
            this.btn_Refresh.Text = "刷新";
            this.btn_Refresh.Click += new System.EventHandler(this.Frm_Download_Load);
            // 
            // Frm_Download
            // 
            this.Appearance.Options.UseFont = true;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1257, 688);
            this.Controls.Add(this.btn_Refresh);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.btn_Delete);
            this.Controls.Add(this.btn_Upload);
            this.Controls.Add(this.gridControl);
            this.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Name = "Frm_Download";
            this.Text = "相关下载";
            this.Load += new System.EventHandler(this.Frm_Download_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.view)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.downloadButton)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.progressBar.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

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
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private DevExpress.XtraEditors.MarqueeProgressBarControl progressBar;
        private DevExpress.XtraEditors.SimpleButton btn_Refresh;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
    }
}