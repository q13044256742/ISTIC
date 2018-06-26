namespace 科技计划项目档案数据采集管理系统
{
    partial class Frm_AddFile_FileSelect
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_AddFile_FileSelect));
            this.tv_file = new System.Windows.Forms.TreeView();
            this.btn_sure = new 科技计划项目档案数据采集管理系统.KyoControl.KyoButton();
            this.label1 = new System.Windows.Forms.Label();
            this.lbl_filename = new System.Windows.Forms.Label();
            this.chk_ShowAll = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // tv_file
            // 
            this.tv_file.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tv_file.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.tv_file.LineColor = System.Drawing.Color.DimGray;
            this.tv_file.Location = new System.Drawing.Point(2, 55);
            this.tv_file.Name = "tv_file";
            this.tv_file.Size = new System.Drawing.Size(686, 597);
            this.tv_file.TabIndex = 0;
            this.tv_file.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.Tv_file_AfterSelect);
            this.tv_file.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.Tv_file_NodeMouseDoubleClick);
            // 
            // btn_sure
            // 
            this.btn_sure.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btn_sure.Appearance.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_sure.Appearance.Options.UseFont = true;
            this.btn_sure.Image = ((System.Drawing.Image)(resources.GetObject("btn_sure.Image")));
            this.btn_sure.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btn_sure.ImageToTextIndent = 5;
            this.btn_sure.Location = new System.Drawing.Point(313, 666);
            this.btn_sure.Name = "btn_sure";
            this.btn_sure.Size = new System.Drawing.Size(67, 30);
            this.btn_sure.TabIndex = 1;
            this.btn_sure.Text = "确定";
            this.btn_sure.Click += new System.EventHandler(this.Btn_sure_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(2, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "当前已选择：";
            // 
            // lbl_filename
            // 
            this.lbl_filename.AutoSize = true;
            this.lbl_filename.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbl_filename.Location = new System.Drawing.Point(91, 17);
            this.lbl_filename.Name = "lbl_filename";
            this.lbl_filename.Size = new System.Drawing.Size(32, 17);
            this.lbl_filename.TabIndex = 3;
            this.lbl_filename.Text = "null";
            // 
            // chk_ShowAll
            // 
            this.chk_ShowAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chk_ShowAll.AutoSize = true;
            this.chk_ShowAll.Location = new System.Drawing.Point(606, 16);
            this.chk_ShowAll.Name = "chk_ShowAll";
            this.chk_ShowAll.Size = new System.Drawing.Size(74, 18);
            this.chk_ShowAll.TabIndex = 4;
            this.chk_ShowAll.Text = "全部显示";
            this.chk_ShowAll.UseVisualStyleBackColor = true;
            this.chk_ShowAll.CheckedChanged += new System.EventHandler(this.chk_ShowAll_CheckedChanged);
            // 
            // Frm_AddFile_FileSelect
            // 
            this.AcceptButton = this.btn_sure;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(692, 709);
            this.Controls.Add(this.chk_ShowAll);
            this.Controls.Add(this.lbl_filename);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btn_sure);
            this.Controls.Add(this.tv_file);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Frm_AddFile_FileSelect";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "文件选择";
            this.Load += new System.EventHandler(this.Frm_AddFile_FileSelect_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView tv_file;
        private KyoControl.KyoButton btn_sure;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbl_filename;
        private System.Windows.Forms.CheckBox chk_ShowAll;
    }
}