﻿namespace 科技计划项目档案数据采集管理系统.DocumentAccept
{
    partial class Frm_Print
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_Print));
            this.chk1 = new DevExpress.XtraEditors.CheckEdit();
            this.chk2 = new DevExpress.XtraEditors.CheckEdit();
            this.chk3 = new DevExpress.XtraEditors.CheckEdit();
            this.lbl1 = new DevExpress.XtraEditors.HyperlinkLabelControl();
            this.btn_Print = new 科技计划项目档案数据采集管理系统.KyoControl.KyoButton();
            this.printDocument1 = new System.Drawing.Printing.PrintDocument();
            this.printPreviewDialog1 = new System.Windows.Forms.PrintPreviewDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.lbl2 = new DevExpress.XtraEditors.HyperlinkLabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.chk1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chk2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chk3.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // chk1
            // 
            this.chk1.Location = new System.Drawing.Point(41, 34);
            this.chk1.Name = "chk1";
            this.chk1.Properties.Appearance.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chk1.Properties.Appearance.Options.UseFont = true;
            this.chk1.Properties.Caption = "null";
            this.chk1.Size = new System.Drawing.Size(249, 25);
            this.chk1.TabIndex = 8;
            // 
            // chk2
            // 
            this.chk2.Location = new System.Drawing.Point(41, 81);
            this.chk2.Name = "chk2";
            this.chk2.Properties.Appearance.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chk2.Properties.Appearance.Options.UseFont = true;
            this.chk2.Properties.Caption = "null";
            this.chk2.Size = new System.Drawing.Size(249, 25);
            this.chk2.TabIndex = 9;
            // 
            // chk3
            // 
            this.chk3.Location = new System.Drawing.Point(41, 128);
            this.chk3.Name = "chk3";
            this.chk3.Properties.Appearance.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chk3.Properties.Appearance.Options.UseFont = true;
            this.chk3.Properties.Caption = "null";
            this.chk3.Size = new System.Drawing.Size(249, 25);
            this.chk3.TabIndex = 10;
            // 
            // lbl1
            // 
            this.lbl1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lbl1.Location = new System.Drawing.Point(281, 39);
            this.lbl1.Name = "lbl1";
            this.lbl1.Size = new System.Drawing.Size(34, 14);
            this.lbl1.TabIndex = 11;
            this.lbl1.Text = "[预览]";
            this.lbl1.Click += new System.EventHandler(this.lbl1_Click);
            // 
            // btn_Print
            // 
            this.btn_Print.Appearance.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.btn_Print.Appearance.Options.UseFont = true;
            this.btn_Print.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_Print.Image = ((System.Drawing.Image)(resources.GetObject("btn_Print.Image")));
            this.btn_Print.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btn_Print.Location = new System.Drawing.Point(165, 193);
            this.btn_Print.Name = "btn_Print";
            this.btn_Print.Size = new System.Drawing.Size(66, 30);
            this.btn_Print.TabIndex = 4;
            this.btn_Print.Text = "导出";
            this.btn_Print.Click += new System.EventHandler(this.Btn_Print_Click);
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
            // lbl2
            // 
            this.lbl2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lbl2.Location = new System.Drawing.Point(321, 39);
            this.lbl2.Name = "lbl2";
            this.lbl2.Size = new System.Drawing.Size(34, 14);
            this.lbl2.TabIndex = 12;
            this.lbl2.Text = "[打印]";
            this.lbl2.Click += new System.EventHandler(this.lbl2_Click);
            // 
            // Frm_Print
            // 
            this.Appearance.Options.UseFont = true;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.CancelButton = this.btn_Print;
            this.ClientSize = new System.Drawing.Size(396, 249);
            this.Controls.Add(this.lbl2);
            this.Controls.Add(this.lbl1);
            this.Controls.Add(this.chk3);
            this.Controls.Add(this.chk2);
            this.Controls.Add(this.chk1);
            this.Controls.Add(this.btn_Print);
            this.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Frm_Print";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "打印类型设置";
            this.Load += new System.EventHandler(this.Frm_Print_Load);
            ((System.ComponentModel.ISupportInitialize)(this.chk1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chk2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chk3.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private KyoControl.KyoButton btn_Print;
        private DevExpress.XtraEditors.CheckEdit chk1;
        private DevExpress.XtraEditors.CheckEdit chk2;
        private DevExpress.XtraEditors.CheckEdit chk3;
        private DevExpress.XtraEditors.HyperlinkLabelControl lbl1;
        private System.Drawing.Printing.PrintDocument printDocument1;
        private System.Windows.Forms.PrintPreviewDialog printPreviewDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private DevExpress.XtraEditors.HyperlinkLabelControl lbl2;
    }
}