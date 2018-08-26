namespace 科技计划项目档案数据采集管理系统
{
    partial class Frm_UploadFile
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_UploadFile));
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.txt_fileCode = new DevExpress.XtraEditors.TextEdit();
            this.txt_fileVersion = new DevExpress.XtraEditors.TextEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.txt_filePath = new DevExpress.XtraEditors.TextEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.btn_Upload = new DevExpress.XtraEditors.SimpleButton();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.label1 = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.txt_fileCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_fileVersion.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_filePath.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelControl1.Location = new System.Drawing.Point(56, 70);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(80, 22);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "文件编号：";
            // 
            // txt_fileCode
            // 
            this.txt_fileCode.Location = new System.Drawing.Point(151, 67);
            this.txt_fileCode.Name = "txt_fileCode";
            this.txt_fileCode.Properties.Appearance.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_fileCode.Properties.Appearance.Options.UseFont = true;
            this.txt_fileCode.Size = new System.Drawing.Size(282, 28);
            this.txt_fileCode.TabIndex = 1;
            // 
            // txt_fileVersion
            // 
            this.txt_fileVersion.Location = new System.Drawing.Point(151, 109);
            this.txt_fileVersion.Name = "txt_fileVersion";
            this.txt_fileVersion.Properties.Appearance.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_fileVersion.Properties.Appearance.Options.UseFont = true;
            this.txt_fileVersion.Properties.Mask.EditMask = "\\d{1,2}\\.\\d{1,2}";
            this.txt_fileVersion.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.txt_fileVersion.Properties.Mask.ShowPlaceHolders = false;
            this.txt_fileVersion.Size = new System.Drawing.Size(190, 28);
            this.txt_fileVersion.TabIndex = 3;
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelControl2.Location = new System.Drawing.Point(72, 112);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(64, 22);
            this.labelControl2.TabIndex = 2;
            this.labelControl2.Text = "版本号：";
            // 
            // txt_filePath
            // 
            this.txt_filePath.Location = new System.Drawing.Point(151, 24);
            this.txt_filePath.Name = "txt_filePath";
            this.txt_filePath.Properties.Appearance.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_filePath.Properties.Appearance.Options.UseFont = true;
            this.txt_filePath.Size = new System.Drawing.Size(378, 28);
            this.txt_filePath.TabIndex = 5;
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelControl3.Location = new System.Drawing.Point(24, 28);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(112, 22);
            this.labelControl3.TabIndex = 4;
            this.labelControl3.Text = "本地文件路径：";
            // 
            // btn_Upload
            // 
            this.btn_Upload.Appearance.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Upload.Appearance.Options.UseFont = true;
            this.btn_Upload.Image = ((System.Drawing.Image)(resources.GetObject("btn_Upload.Image")));
            this.btn_Upload.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btn_Upload.ImageToTextIndent = 0;
            this.btn_Upload.Location = new System.Drawing.Point(249, 181);
            this.btn_Upload.Name = "btn_Upload";
            this.btn_Upload.Size = new System.Drawing.Size(75, 31);
            this.btn_Upload.TabIndex = 6;
            this.btn_Upload.Text = "上传";
            this.btn_Upload.Click += new System.EventHandler(this.btn_Upload_Click);
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(531, 28);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(22, 21);
            this.linkLabel1.TabIndex = 7;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "...";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(50, 134);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 20);
            this.label1.TabIndex = 8;
            this.label1.Text = "(示例:01.01)";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // Frm_UploadFile
            // 
            this.Appearance.Options.UseFont = true;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(581, 230);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.btn_Upload);
            this.Controls.Add(this.txt_filePath);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.txt_fileVersion);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.txt_fileCode);
            this.Controls.Add(this.labelControl1);
            this.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Frm_UploadFile";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "文件上传";
            ((System.ComponentModel.ISupportInitialize)(this.txt_fileCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_fileVersion.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_filePath.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl1;
        public DevExpress.XtraEditors.TextEdit txt_fileCode;
        public DevExpress.XtraEditors.TextEdit txt_fileVersion;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        public DevExpress.XtraEditors.TextEdit txt_filePath;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.SimpleButton btn_Upload;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
    }
}