namespace 科技计划项目档案数据采集管理系统
{
    partial class Frm_FileSelect
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_FileSelect));
            this.tv_file = new System.Windows.Forms.TreeView();
            this.btn_sure = new 科技计划项目档案数据采集管理系统.KyoControl.KyoButton();
            this.label1 = new System.Windows.Forms.Label();
            this.chk_ShowAll = new System.Windows.Forms.CheckBox();
            this.lsv_Selected = new System.Windows.Forms.ListView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.移动MToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.删除DToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tv_file
            // 
            this.tv_file.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tv_file.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.tv_file.LineColor = System.Drawing.Color.DimGray;
            this.tv_file.Location = new System.Drawing.Point(3, 132);
            this.tv_file.Name = "tv_file";
            this.tv_file.Size = new System.Drawing.Size(806, 560);
            this.tv_file.TabIndex = 0;
            this.tv_file.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.Tv_file_NodeMouseClick);
            this.tv_file.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.Tv_file_NodeMouseDoubleClick);
            // 
            // btn_sure
            // 
            this.btn_sure.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btn_sure.Appearance.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_sure.Appearance.Options.UseFont = true;
            this.btn_sure.Image = ((System.Drawing.Image)(resources.GetObject("btn_sure.Image")));
            this.btn_sure.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btn_sure.Location = new System.Drawing.Point(373, 703);
            this.btn_sure.Name = "btn_sure";
            this.btn_sure.Size = new System.Drawing.Size(79, 30);
            this.btn_sure.TabIndex = 1;
            this.btn_sure.Text = "确定(&S)";
            this.btn_sure.Click += new System.EventHandler(this.Btn_Sure_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(2, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(176, 22);
            this.label1.TabIndex = 2;
            this.label1.Text = "当前已选择文件数(0)：";
            // 
            // chk_ShowAll
            // 
            this.chk_ShowAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chk_ShowAll.AutoSize = true;
            this.chk_ShowAll.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.chk_ShowAll.Location = new System.Drawing.Point(707, 11);
            this.chk_ShowAll.Name = "chk_ShowAll";
            this.chk_ShowAll.Size = new System.Drawing.Size(93, 25);
            this.chk_ShowAll.TabIndex = 4;
            this.chk_ShowAll.Text = "全部显示";
            this.chk_ShowAll.UseVisualStyleBackColor = true;
            this.chk_ShowAll.CheckedChanged += new System.EventHandler(this.Chk_ShowAll_CheckedChanged);
            // 
            // lsv_Selected
            // 
            this.lsv_Selected.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lsv_Selected.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lsv_Selected.Location = new System.Drawing.Point(3, 41);
            this.lsv_Selected.Name = "lsv_Selected";
            this.lsv_Selected.Size = new System.Drawing.Size(806, 89);
            this.lsv_Selected.TabIndex = 5;
            this.lsv_Selected.UseCompatibleStateImageBehavior = false;
            this.lsv_Selected.View = System.Windows.Forms.View.List;
            this.lsv_Selected.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Lb_Selected_KeyDown);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.移动MToolStripMenuItem,
            this.删除DToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(121, 48);
            // 
            // 移动MToolStripMenuItem
            // 
            this.移动MToolStripMenuItem.Image = global::科技计划项目档案数据采集管理系统.Properties.Resources._3;
            this.移动MToolStripMenuItem.Name = "移动MToolStripMenuItem";
            this.移动MToolStripMenuItem.Size = new System.Drawing.Size(120, 22);
            this.移动MToolStripMenuItem.Text = "移动(&M)";
            this.移动MToolStripMenuItem.Click += new System.EventHandler(this.移动MToolStripMenuItem_Click);
            // 
            // 删除DToolStripMenuItem
            // 
            this.删除DToolStripMenuItem.Image = global::科技计划项目档案数据采集管理系统.Properties.Resources._2;
            this.删除DToolStripMenuItem.Name = "删除DToolStripMenuItem";
            this.删除DToolStripMenuItem.Size = new System.Drawing.Size(120, 22);
            this.删除DToolStripMenuItem.Text = "删除(&D)";
            this.删除DToolStripMenuItem.Click += new System.EventHandler(this.删除DToolStripMenuItem_Click);
            // 
            // Frm_FileSelect
            // 
            this.AcceptButton = this.btn_sure;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(812, 741);
            this.Controls.Add(this.lsv_Selected);
            this.Controls.Add(this.chk_ShowAll);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btn_sure);
            this.Controls.Add(this.tv_file);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Frm_FileSelect";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "文件选择(按住Ctrl键可多选)";
            this.Load += new System.EventHandler(this.Frm_AddFile_FileSelect_Load);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView tv_file;
        private KyoControl.KyoButton btn_sure;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chk_ShowAll;
        private System.Windows.Forms.ListView lsv_Selected;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 移动MToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 删除DToolStripMenuItem;
    }
}