namespace 科技计划项目档案数据采集管理系统.TransferOfRegistration
{
    partial class Frm_CDRead
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_CDRead));
            this.label1 = new System.Windows.Forms.Label();
            this.txt_CD_Path = new System.Windows.Forms.TextBox();
            this.txt_DS_Path = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lbl_DocProcess = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lbl_CdPath = new System.Windows.Forms.LinkLabel();
            this.lbl_DataPath = new System.Windows.Forms.LinkLabel();
            this.btn_Cancel = new 科技计划项目档案数据采集管理系统.KyoControl.KyoButton();
            this.btn_Sure = new 科技计划项目档案数据采集管理系统.KyoControl.KyoButton();
            this.pgb_DS = new 科技计划项目档案数据采集管理系统.KyoControl.KyoProgressBar();
            this.pgb_CD = new 科技计划项目档案数据采集管理系统.KyoControl.KyoProgressBar();
            this.lbl_SavePath = new System.Windows.Forms.LinkLabel();
            this.txt_SavePath = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(23, 33);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 19);
            this.label1.TabIndex = 0;
            this.label1.Text = "文档路径：";
            // 
            // txt_CD_Path
            // 
            this.txt_CD_Path.Font = new System.Drawing.Font("Tahoma", 10.5F);
            this.txt_CD_Path.Location = new System.Drawing.Point(23, 55);
            this.txt_CD_Path.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txt_CD_Path.Name = "txt_CD_Path";
            this.txt_CD_Path.Size = new System.Drawing.Size(465, 24);
            this.txt_CD_Path.TabIndex = 1;
            // 
            // txt_DS_Path
            // 
            this.txt_DS_Path.Font = new System.Drawing.Font("Tahoma", 10.5F);
            this.txt_DS_Path.Location = new System.Drawing.Point(23, 64);
            this.txt_DS_Path.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txt_DS_Path.Name = "txt_DS_Path";
            this.txt_DS_Path.ReadOnly = true;
            this.txt_DS_Path.Size = new System.Drawing.Size(466, 24);
            this.txt_DS_Path.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(23, 41);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(93, 19);
            this.label2.TabIndex = 3;
            this.label2.Text = "源数据路径：";
            // 
            // lbl_DocProcess
            // 
            this.lbl_DocProcess.AutoSize = true;
            this.lbl_DocProcess.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbl_DocProcess.Location = new System.Drawing.Point(23, 156);
            this.lbl_DocProcess.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_DocProcess.Name = "lbl_DocProcess";
            this.lbl_DocProcess.Size = new System.Drawing.Size(146, 19);
            this.lbl_DocProcess.TabIndex = 6;
            this.lbl_DocProcess.Text = "文档读写进度（0/0）";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(23, 104);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(121, 19);
            this.label4.TabIndex = 8;
            this.label4.Text = "源数据读取进度：";
            // 
            // lbl_CdPath
            // 
            this.lbl_CdPath.AutoSize = true;
            this.lbl_CdPath.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.lbl_CdPath.Location = new System.Drawing.Point(491, 54);
            this.lbl_CdPath.Name = "lbl_CdPath";
            this.lbl_CdPath.Size = new System.Drawing.Size(22, 21);
            this.lbl_CdPath.TabIndex = 11;
            this.lbl_CdPath.TabStop = true;
            this.lbl_CdPath.Text = "...";
            this.lbl_CdPath.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.Lbl_CdPath_LinkClicked);
            // 
            // lbl_DataPath
            // 
            this.lbl_DataPath.AutoSize = true;
            this.lbl_DataPath.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.lbl_DataPath.Location = new System.Drawing.Point(496, 64);
            this.lbl_DataPath.Name = "lbl_DataPath";
            this.lbl_DataPath.Size = new System.Drawing.Size(22, 21);
            this.lbl_DataPath.TabIndex = 12;
            this.lbl_DataPath.TabStop = true;
            this.lbl_DataPath.Text = "...";
            this.lbl_DataPath.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.Lbl_DataPath_LinkClicked);
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.Appearance.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.btn_Cancel.Appearance.Options.UseFont = true;
            this.btn_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_Cancel.Image = ((System.Drawing.Image)(resources.GetObject("btn_Cancel.Image")));
            this.btn_Cancel.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btn_Cancel.ImageToTextIndent = 5;
            this.btn_Cancel.Location = new System.Drawing.Point(273, 404);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(70, 30);
            this.btn_Cancel.TabIndex = 13;
            this.btn_Cancel.Text = "关闭";
            this.btn_Cancel.Click += new System.EventHandler(this.btn_Cancel_Click);
            // 
            // btn_Sure
            // 
            this.btn_Sure.Appearance.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Sure.Appearance.Options.UseFont = true;
            this.btn_Sure.Image = ((System.Drawing.Image)(resources.GetObject("btn_Sure.Image")));
            this.btn_Sure.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btn_Sure.ImageToTextIndent = 5;
            this.btn_Sure.Location = new System.Drawing.Point(200, 404);
            this.btn_Sure.Name = "btn_Sure";
            this.btn_Sure.Size = new System.Drawing.Size(67, 30);
            this.btn_Sure.TabIndex = 10;
            this.btn_Sure.Text = "开始";
            this.btn_Sure.Click += new System.EventHandler(this.Btn_Sure_Click);
            // 
            // pgb_DS
            // 
            this.pgb_DS.Location = new System.Drawing.Point(23, 127);
            this.pgb_DS.Name = "pgb_DS";
            this.pgb_DS.Size = new System.Drawing.Size(490, 23);
            this.pgb_DS.TabIndex = 9;
            this.pgb_DS.TextColor = System.Drawing.Color.Black;
            this.pgb_DS.TextFont = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            // 
            // pgb_CD
            // 
            this.pgb_CD.Location = new System.Drawing.Point(23, 178);
            this.pgb_CD.Name = "pgb_CD";
            this.pgb_CD.Size = new System.Drawing.Size(490, 23);
            this.pgb_CD.TabIndex = 7;
            this.pgb_CD.TextColor = System.Drawing.Color.Black;
            this.pgb_CD.TextFont = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            // 
            // lbl_SavePath
            // 
            this.lbl_SavePath.AutoSize = true;
            this.lbl_SavePath.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.lbl_SavePath.Location = new System.Drawing.Point(491, 113);
            this.lbl_SavePath.Name = "lbl_SavePath";
            this.lbl_SavePath.Size = new System.Drawing.Size(22, 21);
            this.lbl_SavePath.TabIndex = 16;
            this.lbl_SavePath.TabStop = true;
            this.lbl_SavePath.Text = "...";
            this.lbl_SavePath.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.Lbl_SavePath_LinkClicked);
            // 
            // txt_SavePath
            // 
            this.txt_SavePath.Font = new System.Drawing.Font("Tahoma", 10.5F);
            this.txt_SavePath.Location = new System.Drawing.Point(23, 114);
            this.txt_SavePath.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txt_SavePath.Name = "txt_SavePath";
            this.txt_SavePath.Size = new System.Drawing.Size(465, 24);
            this.txt_SavePath.TabIndex = 15;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(23, 92);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(107, 19);
            this.label5.TabIndex = 14;
            this.label5.Text = "文档存放路径：";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.lbl_SavePath);
            this.groupBox1.Controls.Add(this.txt_CD_Path);
            this.groupBox1.Controls.Add(this.txt_SavePath);
            this.groupBox1.Controls.Add(this.pgb_CD);
            this.groupBox1.Controls.Add(this.lbl_CdPath);
            this.groupBox1.Controls.Add(this.lbl_DocProcess);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Italic);
            this.groupBox1.Location = new System.Drawing.Point(6, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(529, 222);
            this.groupBox1.TabIndex = 17;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "文档读写";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txt_DS_Path);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.lbl_DataPath);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.pgb_DS);
            this.groupBox2.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Italic);
            this.groupBox2.Location = new System.Drawing.Point(6, 223);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(529, 168);
            this.groupBox2.TabIndex = 18;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "结构化数据导入";
            // 
            // Frm_CDRead
            // 
            this.Appearance.Options.UseFont = true;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.CancelButton = this.btn_Cancel;
            this.ClientSize = new System.Drawing.Size(542, 446);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btn_Cancel);
            this.Controls.Add(this.btn_Sure);
            this.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Frm_CDRead";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "光盘读写";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_CD_Path;
        private System.Windows.Forms.TextBox txt_DS_Path;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lbl_DocProcess;
        private System.Windows.Forms.Label label4;
        private KyoControl.KyoButton btn_Sure;
        private KyoControl.KyoProgressBar pgb_CD;
        private KyoControl.KyoProgressBar pgb_DS;
        private System.Windows.Forms.LinkLabel lbl_CdPath;
        private System.Windows.Forms.LinkLabel lbl_DataPath;
        private KyoControl.KyoButton btn_Cancel;
        private System.Windows.Forms.LinkLabel lbl_SavePath;
        private System.Windows.Forms.TextBox txt_SavePath;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
    }
}