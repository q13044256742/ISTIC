namespace 科技计划项目档案数据采集管理系统.DocumentAccept
{
    partial class Frm_ExportEFile
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_ExportEFile));
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.txt_ExportEFilePath = new DevExpress.XtraEditors.TextEdit();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btn_ExportOk = new DevExpress.XtraEditors.SimpleButton();
            this.lbl_ExportLink = new System.Windows.Forms.LinkLabel();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.panel2 = new System.Windows.Forms.Panel();
            this.chk_All = new System.Windows.Forms.CheckBox();
            this.chk_Data = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.txt_ExportEFilePath.Properties)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // treeView1
            // 
            this.treeView1.CheckBoxes = true;
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView1.Location = new System.Drawing.Point(0, 43);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(749, 504);
            this.treeView1.TabIndex = 0;
            this.treeView1.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterCheck);
            // 
            // txt_ExportEFilePath
            // 
            this.txt_ExportEFilePath.Location = new System.Drawing.Point(99, 9);
            this.txt_ExportEFilePath.Name = "txt_ExportEFilePath";
            this.txt_ExportEFilePath.Properties.Appearance.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.txt_ExportEFilePath.Properties.Appearance.Options.UseFont = true;
            this.txt_ExportEFilePath.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
            this.txt_ExportEFilePath.Size = new System.Drawing.Size(465, 30);
            this.txt_ExportEFilePath.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(6, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 22);
            this.label1.TabIndex = 2;
            this.label1.Text = "导出路径：";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btn_ExportOk);
            this.panel1.Controls.Add(this.lbl_ExportLink);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.txt_ExportEFilePath);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 547);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(749, 48);
            this.panel1.TabIndex = 3;
            // 
            // btn_ExportOk
            // 
            this.btn_ExportOk.Appearance.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.btn_ExportOk.Appearance.Options.UseFont = true;
            this.btn_ExportOk.Image = ((System.Drawing.Image)(resources.GetObject("btn_ExportOk.Image")));
            this.btn_ExportOk.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btn_ExportOk.Location = new System.Drawing.Point(613, 11);
            this.btn_ExportOk.Name = "btn_ExportOk";
            this.btn_ExportOk.Size = new System.Drawing.Size(78, 28);
            this.btn_ExportOk.TabIndex = 4;
            this.btn_ExportOk.Text = "确定";
            this.btn_ExportOk.Click += new System.EventHandler(this.btn_ExportOk_Click);
            // 
            // lbl_ExportLink
            // 
            this.lbl_ExportLink.AutoSize = true;
            this.lbl_ExportLink.Location = new System.Drawing.Point(567, 14);
            this.lbl_ExportLink.Name = "lbl_ExportLink";
            this.lbl_ExportLink.Size = new System.Drawing.Size(22, 21);
            this.lbl_ExportLink.TabIndex = 3;
            this.lbl_ExportLink.TabStop = true;
            this.lbl_ExportLink.Text = "...";
            this.lbl_ExportLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lbl_ExportLink_LinkClicked);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.chk_Data);
            this.panel2.Controls.Add(this.chk_All);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(749, 43);
            this.panel2.TabIndex = 4;
            // 
            // chk_All
            // 
            this.chk_All.AutoSize = true;
            this.chk_All.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.chk_All.Location = new System.Drawing.Point(12, 9);
            this.chk_All.Name = "chk_All";
            this.chk_All.Size = new System.Drawing.Size(58, 24);
            this.chk_All.TabIndex = 0;
            this.chk_All.Text = "全选";
            this.chk_All.UseVisualStyleBackColor = true;
            this.chk_All.CheckedChanged += new System.EventHandler(this.chk_All_CheckedChanged);
            // 
            // chk_Data
            // 
            this.chk_Data.AutoSize = true;
            this.chk_Data.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.chk_Data.Location = new System.Drawing.Point(79, 9);
            this.chk_Data.Name = "chk_Data";
            this.chk_Data.Size = new System.Drawing.Size(118, 24);
            this.chk_Data.TabIndex = 1;
            this.chk_Data.Text = "同时导出数据";
            this.chk_Data.UseVisualStyleBackColor = true;
            // 
            // Frm_ExportEFile
            // 
            this.Appearance.Options.UseFont = true;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(749, 595);
            this.Controls.Add(this.treeView1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "Frm_ExportEFile";
            this.Text = "导出电子文件";
            this.Load += new System.EventHandler(this.Frm_ExportEFile_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txt_ExportEFilePath.Properties)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView treeView1;
        private DevExpress.XtraEditors.TextEdit txt_ExportEFilePath;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private DevExpress.XtraEditors.SimpleButton btn_ExportOk;
        private System.Windows.Forms.LinkLabel lbl_ExportLink;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.CheckBox chk_Data;
        private System.Windows.Forms.CheckBox chk_All;
    }
}