﻿namespace 科技计划项目档案数据采集管理系统
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_MainFrame));
            this.lbl_OtherInfo = new System.Windows.Forms.Label();
            this.txt_RealName = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
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
            this.lbl_OtherInfo.Location = new System.Drawing.Point(1008, 8);
            this.lbl_OtherInfo.Name = "lbl_OtherInfo";
            this.lbl_OtherInfo.Size = new System.Drawing.Size(254, 21);
            this.lbl_OtherInfo.TabIndex = 2;
            this.lbl_OtherInfo.Text = "当前时间：2017年4月21日 星期三";
            // 
            // txt_RealName
            // 
            this.txt_RealName.AutoSize = true;
            this.txt_RealName.BackColor = System.Drawing.Color.Transparent;
            this.txt_RealName.Location = new System.Drawing.Point(102, 8);
            this.txt_RealName.Name = "txt_RealName";
            this.txt_RealName.Size = new System.Drawing.Size(0, 21);
            this.txt_RealName.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(14, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 21);
            this.label1.TabIndex = 0;
            this.label1.Text = "欢迎你：";
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
            this.pal_Top_Info.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pal_Top_Info.Location = new System.Drawing.Point(0, 79);
            this.pal_Top_Info.Name = "pal_Top_Info";
            this.pal_Top_Info.Size = new System.Drawing.Size(1271, 36);
            this.pal_Top_Info.TabIndex = 1;
            // 
            // Frm_MainFrame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1271, 733);
            this.Controls.Add(this.pal_Top);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.Name = "Frm_MainFrame";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "科技计划项目档案数据采集管理系统";
            this.Load += new System.EventHandler(this.Frm_MainFrame_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.pal_Top.ResumeLayout(false);
            this.pal_Top_Info.ResumeLayout(false);
            this.pal_Top_Info.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lbl_OtherInfo;
        private System.Windows.Forms.Label txt_RealName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel pal_Top;
        private System.Windows.Forms.Panel pal_Top_Info;
    }
}