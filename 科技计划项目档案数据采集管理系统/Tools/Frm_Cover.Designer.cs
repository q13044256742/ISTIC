﻿namespace 科技计划项目档案数据采集管理系统
{
    partial class Frm_Cover
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_Cover));
            this.panel1 = new System.Windows.Forms.Panel();
            this.label7 = new System.Windows.Forms.Label();
            this.trackBar = new DevExpress.XtraEditors.TrackBarControl();
            this.pal_Show = new System.Windows.Forms.Panel();
            this.panel7 = new System.Windows.Forms.Panel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lbl_Name = new System.Windows.Forms.Label();
            this.lbl_Secret = new System.Windows.Forms.Label();
            this.lbl_BGD = new System.Windows.Forms.Label();
            this.lbl_BZD = new System.Windows.Forms.Label();
            this.lbl_Unit = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel8 = new System.Windows.Forms.Panel();
            this.lbl_GCH = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.pageSetupDialog1 = new System.Windows.Forms.PageSetupDialog();
            this.printDocument1 = new System.Drawing.Printing.PrintDocument();
            this.printDialog1 = new System.Windows.Forms.PrintDialog();
            this.printPreviewDialog1 = new System.Windows.Forms.PrintPreviewDialog();
            this.fontDialog1 = new System.Windows.Forms.FontDialog();
            this.btn_Font = new 科技计划项目档案数据采集管理系统.KyoControl.KyoButton();
            this.btn_PrintSetup = new 科技计划项目档案数据采集管理系统.KyoControl.KyoButton();
            this.btn_Print = new 科技计划项目档案数据采集管理系统.KyoControl.KyoButton();
            this.dialog = new System.Windows.Forms.FolderBrowserDialog();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar.Properties)).BeginInit();
            this.pal_Show.SuspendLayout();
            this.panel7.SuspendLayout();
            this.panel8.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.AutoScroll = true;
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.trackBar);
            this.panel1.Controls.Add(this.pal_Show);
            this.panel1.Location = new System.Drawing.Point(1, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(816, 548);
            this.panel1.TabIndex = 0;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("微软雅黑", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(24, 504);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(75, 28);
            this.label7.TabIndex = 2;
            this.label7.Text = "边距：";
            // 
            // trackBar
            // 
            this.trackBar.EditValue = null;
            this.trackBar.Location = new System.Drawing.Point(105, 496);
            this.trackBar.Name = "trackBar";
            this.trackBar.Properties.LabelAppearance.Options.UseTextOptions = true;
            this.trackBar.Properties.LabelAppearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.trackBar.Size = new System.Drawing.Size(142, 45);
            this.trackBar.TabIndex = 1;
            this.trackBar.EditValueChanged += new System.EventHandler(this.trackBar_EditValueChanged);
            // 
            // pal_Show
            // 
            this.pal_Show.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pal_Show.Controls.Add(this.panel7);
            this.pal_Show.Controls.Add(this.panel8);
            this.pal_Show.Font = new System.Drawing.Font("宋体", 12F);
            this.pal_Show.Location = new System.Drawing.Point(18, 10);
            this.pal_Show.Name = "pal_Show";
            this.pal_Show.Size = new System.Drawing.Size(776, 480);
            this.pal_Show.TabIndex = 0;
            // 
            // panel7
            // 
            this.panel7.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel7.Controls.Add(this.panel6);
            this.panel7.Controls.Add(this.panel5);
            this.panel7.Controls.Add(this.panel4);
            this.panel7.Controls.Add(this.panel3);
            this.panel7.Controls.Add(this.panel2);
            this.panel7.Controls.Add(this.lbl_Name);
            this.panel7.Controls.Add(this.lbl_Secret);
            this.panel7.Controls.Add(this.lbl_BGD);
            this.panel7.Controls.Add(this.lbl_BZD);
            this.panel7.Controls.Add(this.lbl_Unit);
            this.panel7.Controls.Add(this.label5);
            this.panel7.Controls.Add(this.label4);
            this.panel7.Controls.Add(this.label3);
            this.panel7.Controls.Add(this.label2);
            this.panel7.Controls.Add(this.label1);
            this.panel7.Location = new System.Drawing.Point(25, 5);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(726, 324);
            this.panel7.TabIndex = 25;
            // 
            // panel6
            // 
            this.panel6.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel6.Location = new System.Drawing.Point(141, 295);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(560, 1);
            this.panel6.TabIndex = 22;
            // 
            // panel5
            // 
            this.panel5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel5.Location = new System.Drawing.Point(141, 234);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(560, 1);
            this.panel5.TabIndex = 21;
            // 
            // panel4
            // 
            this.panel4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Location = new System.Drawing.Point(141, 173);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(560, 1);
            this.panel4.TabIndex = 20;
            // 
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Location = new System.Drawing.Point(141, 112);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(560, 1);
            this.panel3.TabIndex = 19;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Location = new System.Drawing.Point(141, 53);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(560, 1);
            this.panel2.TabIndex = 18;
            // 
            // lbl_Name
            // 
            this.lbl_Name.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_Name.Font = new System.Drawing.Font("华文中宋", 15F);
            this.lbl_Name.Location = new System.Drawing.Point(146, 3);
            this.lbl_Name.Name = "lbl_Name";
            this.lbl_Name.Size = new System.Drawing.Size(539, 47);
            this.lbl_Name.TabIndex = 12;
            this.lbl_Name.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // lbl_Secret
            // 
            this.lbl_Secret.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_Secret.Font = new System.Drawing.Font("华文中宋", 15F);
            this.lbl_Secret.Location = new System.Drawing.Point(146, 245);
            this.lbl_Secret.Name = "lbl_Secret";
            this.lbl_Secret.Size = new System.Drawing.Size(539, 47);
            this.lbl_Secret.TabIndex = 16;
            this.lbl_Secret.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // lbl_BGD
            // 
            this.lbl_BGD.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_BGD.Font = new System.Drawing.Font("华文中宋", 15F);
            this.lbl_BGD.Location = new System.Drawing.Point(146, 184);
            this.lbl_BGD.Name = "lbl_BGD";
            this.lbl_BGD.Size = new System.Drawing.Size(539, 47);
            this.lbl_BGD.TabIndex = 15;
            this.lbl_BGD.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // lbl_BZD
            // 
            this.lbl_BZD.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_BZD.Font = new System.Drawing.Font("华文中宋", 15F);
            this.lbl_BZD.Location = new System.Drawing.Point(146, 123);
            this.lbl_BZD.Name = "lbl_BZD";
            this.lbl_BZD.Size = new System.Drawing.Size(539, 47);
            this.lbl_BZD.TabIndex = 14;
            this.lbl_BZD.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // lbl_Unit
            // 
            this.lbl_Unit.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_Unit.Font = new System.Drawing.Font("华文中宋", 15F);
            this.lbl_Unit.Location = new System.Drawing.Point(146, 62);
            this.lbl_Unit.Name = "lbl_Unit";
            this.lbl_Unit.Size = new System.Drawing.Size(539, 47);
            this.lbl_Unit.TabIndex = 13;
            this.lbl_Unit.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("华文宋体", 18F, System.Drawing.FontStyle.Bold);
            this.label5.Location = new System.Drawing.Point(63, 268);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(62, 27);
            this.label5.TabIndex = 4;
            this.label5.Text = "密级";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("华文宋体", 18F, System.Drawing.FontStyle.Bold);
            this.label4.Location = new System.Drawing.Point(13, 207);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(112, 27);
            this.label4.TabIndex = 3;
            this.label4.Text = "保管日期";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("华文宋体", 18F, System.Drawing.FontStyle.Bold);
            this.label3.Location = new System.Drawing.Point(13, 146);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(112, 27);
            this.label3.TabIndex = 2;
            this.label3.Text = "编制日期";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("华文宋体", 18F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(13, 85);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(112, 27);
            this.label2.TabIndex = 1;
            this.label2.Text = "编制单位";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("华文宋体", 18F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(13, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(112, 27);
            this.label1.TabIndex = 0;
            this.label1.Text = "案卷名称";
            // 
            // panel8
            // 
            this.panel8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel8.Controls.Add(this.lbl_GCH);
            this.panel8.Controls.Add(this.label6);
            this.panel8.Location = new System.Drawing.Point(9, 335);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(934, 135);
            this.panel8.TabIndex = 24;
            // 
            // lbl_GCH
            // 
            this.lbl_GCH.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lbl_GCH.AutoSize = true;
            this.lbl_GCH.Font = new System.Drawing.Font("华文宋体", 30F, System.Drawing.FontStyle.Bold);
            this.lbl_GCH.Location = new System.Drawing.Point(218, 48);
            this.lbl_GCH.Name = "lbl_GCH";
            this.lbl_GCH.Size = new System.Drawing.Size(165, 44);
            this.lbl_GCH.TabIndex = 17;
            this.lbl_GCH.Text = "XXXXX";
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("华文宋体", 30F, System.Drawing.FontStyle.Bold);
            this.label6.Location = new System.Drawing.Point(25, 48);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(184, 44);
            this.label6.TabIndex = 5;
            this.label6.Text = "馆藏号：";
            // 
            // pageSetupDialog1
            // 
            this.pageSetupDialog1.Document = this.printDocument1;
            // 
            // printDocument1
            // 
            this.printDocument1.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.printDocument1_PrintPage);
            // 
            // printDialog1
            // 
            this.printDialog1.Document = this.printDocument1;
            this.printDialog1.UseEXDialog = true;
            // 
            // printPreviewDialog1
            // 
            this.printPreviewDialog1.AutoScrollMargin = new System.Drawing.Size(0, 0);
            this.printPreviewDialog1.AutoScrollMinSize = new System.Drawing.Size(0, 0);
            this.printPreviewDialog1.ClientSize = new System.Drawing.Size(400, 300);
            this.printPreviewDialog1.Document = this.printDocument1;
            this.printPreviewDialog1.Enabled = true;
            this.printPreviewDialog1.Icon = ((System.Drawing.Icon)(resources.GetObject("printPreviewDialog1.Icon")));
            this.printPreviewDialog1.Name = "printPreviewDialog1";
            this.printPreviewDialog1.Visible = false;
            // 
            // btn_Font
            // 
            this.btn_Font.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btn_Font.Image = ((System.Drawing.Image)(resources.GetObject("btn_Font.Image")));
            this.btn_Font.Location = new System.Drawing.Point(293, 554);
            this.btn_Font.Name = "btn_Font";
            this.btn_Font.Size = new System.Drawing.Size(83, 31);
            this.btn_Font.TabIndex = 6;
            this.btn_Font.Text = "字体设置";
            this.btn_Font.Click += new System.EventHandler(this.btn_Font_Click);
            // 
            // btn_PrintSetup
            // 
            this.btn_PrintSetup.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btn_PrintSetup.Image = ((System.Drawing.Image)(resources.GetObject("btn_PrintSetup.Image")));
            this.btn_PrintSetup.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btn_PrintSetup.Location = new System.Drawing.Point(378, 554);
            this.btn_PrintSetup.Name = "btn_PrintSetup";
            this.btn_PrintSetup.Size = new System.Drawing.Size(83, 31);
            this.btn_PrintSetup.TabIndex = 3;
            this.btn_PrintSetup.Text = "打印设置";
            this.btn_PrintSetup.Click += new System.EventHandler(this.btn_PrintSetup_Click);
            // 
            // btn_Print
            // 
            this.btn_Print.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btn_Print.Image = ((System.Drawing.Image)(resources.GetObject("btn_Print.Image")));
            this.btn_Print.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btn_Print.Location = new System.Drawing.Point(463, 554);
            this.btn_Print.Name = "btn_Print";
            this.btn_Print.Size = new System.Drawing.Size(63, 31);
            this.btn_Print.TabIndex = 1;
            this.btn_Print.Text = "打印";
            this.btn_Print.Click += new System.EventHandler(this.Btn_Print_Click);
            // 
            // Frm_Cover
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(818, 591);
            this.Controls.Add(this.btn_Font);
            this.Controls.Add(this.btn_PrintSetup);
            this.Controls.Add(this.btn_Print);
            this.Controls.Add(this.panel1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Frm_Cover";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.Frm_Cover_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar)).EndInit();
            this.pal_Show.ResumeLayout(false);
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            this.panel8.ResumeLayout(false);
            this.panel8.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private KyoControl.KyoButton btn_Print;
        private System.Windows.Forms.Panel pal_Show;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbl_GCH;
        private System.Windows.Forms.Label lbl_Secret;
        private System.Windows.Forms.Label lbl_BGD;
        private System.Windows.Forms.Label lbl_BZD;
        private System.Windows.Forms.Label lbl_Unit;
        private System.Windows.Forms.Label lbl_Name;
        private System.Windows.Forms.PageSetupDialog pageSetupDialog1;
        private System.Drawing.Printing.PrintDocument printDocument1;
        private System.Windows.Forms.PrintDialog printDialog1;
        private System.Windows.Forms.PrintPreviewDialog printPreviewDialog1;
        private KyoControl.KyoButton btn_PrintSetup;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.Panel panel7;
        private KyoControl.KyoButton btn_Font;
        private System.Windows.Forms.FontDialog fontDialog1;
        private System.Windows.Forms.Label label7;
        private DevExpress.XtraEditors.TrackBarControl trackBar;
        private System.Windows.Forms.FolderBrowserDialog dialog;
    }
}