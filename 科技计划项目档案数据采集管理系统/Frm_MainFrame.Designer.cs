namespace 科技计划项目档案数据采集管理系统
{
    partial class Frm_MainFrame
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
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
        private void InitializeComponent() {
            this.panel1 = new 科技计划项目档案数据采集管理系统.Tools.MyPanel();
            this.pal_LoginInfo = new 科技计划项目档案数据采集管理系统.Tools.MyPanel();
            this.lbl_OtherInfo = new System.Windows.Forms.Label();
            this.txt_RealName = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel1.SuspendLayout();
            this.pal_LoginInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackgroundImage = global::科技计划项目档案数据采集管理系统.Properties.Resources.top;
            this.panel1.Controls.Add(this.pal_LoginInfo);
            this.panel1.Controls.Add(this.linkLabel1);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1271, 142);
            this.panel1.TabIndex = 4;
            // 
            // pal_LoginInfo
            // 
            this.pal_LoginInfo.BackgroundImage = global::科技计划项目档案数据采集管理系统.Properties.Resources.top_bg;
            this.pal_LoginInfo.Controls.Add(this.lbl_OtherInfo);
            this.pal_LoginInfo.Controls.Add(this.txt_RealName);
            this.pal_LoginInfo.Controls.Add(this.label1);
            this.pal_LoginInfo.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pal_LoginInfo.Location = new System.Drawing.Point(0, 105);
            this.pal_LoginInfo.Name = "pal_LoginInfo";
            this.pal_LoginInfo.Size = new System.Drawing.Size(1271, 37);
            this.pal_LoginInfo.TabIndex = 2;
            // 
            // lbl_OtherInfo
            // 
            this.lbl_OtherInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_OtherInfo.AutoSize = true;
            this.lbl_OtherInfo.BackColor = System.Drawing.Color.Transparent;
            this.lbl_OtherInfo.Location = new System.Drawing.Point(976, 12);
            this.lbl_OtherInfo.Name = "lbl_OtherInfo";
            this.lbl_OtherInfo.Size = new System.Drawing.Size(280, 14);
            this.lbl_OtherInfo.TabIndex = 2;
            this.lbl_OtherInfo.Text = "角色：移交登记 当前时间：2017年4月21日 星期三";
            // 
            // txt_RealName
            // 
            this.txt_RealName.AutoSize = true;
            this.txt_RealName.BackColor = System.Drawing.Color.Transparent;
            this.txt_RealName.Location = new System.Drawing.Point(75, 11);
            this.txt_RealName.Name = "txt_RealName";
            this.txt_RealName.Size = new System.Drawing.Size(0, 14);
            this.txt_RealName.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(14, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 14);
            this.label1.TabIndex = 0;
            this.label1.Text = "欢迎你：";
            // 
            // linkLabel1
            // 
            this.linkLabel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.linkLabel1.BackColor = System.Drawing.Color.Transparent;
            this.linkLabel1.Font = new System.Drawing.Font("黑体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.linkLabel1.Image = global::科技计划项目档案数据采集管理系统.Properties.Resources.tuichu;
            this.linkLabel1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.linkLabel1.Location = new System.Drawing.Point(1118, 48);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(101, 32);
            this.linkLabel1.TabIndex = 1;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "退出系统";
            this.linkLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Image = global::科技计划项目档案数据采集管理系统.Properties.Resources.logo;
            this.pictureBox1.Location = new System.Drawing.Point(3, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(615, 101);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // Frm_MainFrame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1271, 741);
            this.Controls.Add(this.panel1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.IsMdiContainer = true;
            this.Name = "Frm_MainFrame";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "科技计划项目档案数据采集管理系统";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Frm_MainFrame_FormClosing);
            this.panel1.ResumeLayout(false);
            this.pal_LoginInfo.ResumeLayout(false);
            this.pal_LoginInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private Tools.MyPanel panel1;
        private Tools.MyPanel pal_LoginInfo;
        private System.Windows.Forms.Label lbl_OtherInfo;
        private System.Windows.Forms.Label txt_RealName;
        private System.Windows.Forms.Label label1;
    }
}