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
            this.lbl_OtherInfo = new System.Windows.Forms.Label();
            this.txt_RealName = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pal_Top = new System.Windows.Forms.Panel();
            this.pal_Top_Info = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.pal_Top.SuspendLayout();
            this.pal_Top_Info.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbl_OtherInfo
            // 
            this.lbl_OtherInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_OtherInfo.AutoSize = true;
            this.lbl_OtherInfo.BackColor = System.Drawing.Color.Transparent;
            this.lbl_OtherInfo.Location = new System.Drawing.Point(1067, 11);
            this.lbl_OtherInfo.Name = "lbl_OtherInfo";
            this.lbl_OtherInfo.Size = new System.Drawing.Size(192, 14);
            this.lbl_OtherInfo.TabIndex = 2;
            this.lbl_OtherInfo.Text = "当前时间：2017年4月21日 星期三";
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
            this.linkLabel1.Location = new System.Drawing.Point(1118, 30);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(101, 21);
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
            this.pictureBox1.Size = new System.Drawing.Size(560, 76);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // pal_Top
            // 
            this.pal_Top.BackColor = System.Drawing.Color.Gray;
            this.pal_Top.BackgroundImage = global::科技计划项目档案数据采集管理系统.Properties.Resources.top;
            this.pal_Top.Controls.Add(this.pal_Top_Info);
            this.pal_Top.Controls.Add(this.pictureBox1);
            this.pal_Top.Controls.Add(this.linkLabel1);
            this.pal_Top.Dock = System.Windows.Forms.DockStyle.Top;
            this.pal_Top.Location = new System.Drawing.Point(0, 0);
            this.pal_Top.Name = "pal_Top";
            this.pal_Top.Size = new System.Drawing.Size(1271, 115);
            this.pal_Top.TabIndex = 1;
            // 
            // pal_Top_Info
            // 
            this.pal_Top_Info.BackgroundImage = global::科技计划项目档案数据采集管理系统.Properties.Resources.top_bg;
            this.pal_Top_Info.Controls.Add(this.label1);
            this.pal_Top_Info.Controls.Add(this.txt_RealName);
            this.pal_Top_Info.Controls.Add(this.lbl_OtherInfo);
            this.pal_Top_Info.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pal_Top_Info.Location = new System.Drawing.Point(0, 79);
            this.pal_Top_Info.Name = "pal_Top_Info";
            this.pal_Top_Info.Size = new System.Drawing.Size(1271, 36);
            this.pal_Top_Info.TabIndex = 1;
            // 
            // Frm_MainFrame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1271, 741);
            this.Controls.Add(this.pal_Top);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.IsMdiContainer = true;
            this.Name = "Frm_MainFrame";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "科技计划项目档案数据采集管理系统";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Frm_MainFrame_FormClosing);
            this.Load += new System.EventHandler(this.Frm_MainFrame_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.pal_Top.ResumeLayout(false);
            this.pal_Top_Info.ResumeLayout(false);
            this.pal_Top_Info.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.Label lbl_OtherInfo;
        private System.Windows.Forms.Label txt_RealName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel pal_Top;
        private System.Windows.Forms.Panel pal_Top_Info;
    }
}