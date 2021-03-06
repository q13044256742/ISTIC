﻿using System;

namespace 科技计划项目档案数据采集管理系统
{
    partial class Frm_Advice
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_Advice));
            this.panel1 = new System.Windows.Forms.Panel();
            this.btn_HistroyOpinion = new 科技计划项目档案数据采集管理系统.KyoControl.KyoButton();
            this.cbo_AdviceType = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btn_Delete = new 科技计划项目档案数据采集管理系统.KyoControl.KyoButton();
            this.btn_Sure = new 科技计划项目档案数据采集管理系统.KyoControl.KyoButton();
            this.label3 = new System.Windows.Forms.Label();
            this.lbl_ObjName = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txt_Advice = new System.Windows.Forms.RichTextBox();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.txt_Advice);
            this.panel1.Controls.Add(this.btn_HistroyOpinion);
            this.panel1.Controls.Add(this.cbo_AdviceType);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.btn_Delete);
            this.panel1.Controls.Add(this.btn_Sure);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.lbl_ObjName);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(827, 511);
            this.panel1.TabIndex = 0;
            // 
            // btn_HistroyOpinion
            // 
            this.btn_HistroyOpinion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_HistroyOpinion.Appearance.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.btn_HistroyOpinion.Appearance.Options.UseFont = true;
            this.btn_HistroyOpinion.Image = ((System.Drawing.Image)(resources.GetObject("btn_HistroyOpinion.Image")));
            this.btn_HistroyOpinion.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btn_HistroyOpinion.Location = new System.Drawing.Point(715, 11);
            this.btn_HistroyOpinion.Name = "btn_HistroyOpinion";
            this.btn_HistroyOpinion.Size = new System.Drawing.Size(97, 36);
            this.btn_HistroyOpinion.TabIndex = 12;
            this.btn_HistroyOpinion.Text = "历史意见";
            this.btn_HistroyOpinion.Click += new System.EventHandler(this.Btn_HistroyOpinion_Click);
            // 
            // cbo_AdviceType
            // 
            this.cbo_AdviceType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo_AdviceType.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.cbo_AdviceType.FormattingEnabled = true;
            this.cbo_AdviceType.Items.AddRange(new object[] {
            "基本信息",
            "文件信息",
            "核查信息",
            "案盒信息"});
            this.cbo_AdviceType.Location = new System.Drawing.Point(137, 50);
            this.cbo_AdviceType.Name = "cbo_AdviceType";
            this.cbo_AdviceType.Size = new System.Drawing.Size(137, 29);
            this.cbo_AdviceType.TabIndex = 11;
            this.cbo_AdviceType.SelectionChangeCommitted += new System.EventHandler(this.Cbo_AdviceType_SelectionChangeCommitted);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(33, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(90, 22);
            this.label2.TabIndex = 10;
            this.label2.Text = "意见类别：";
            // 
            // btn_Delete
            // 
            this.btn_Delete.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btn_Delete.Appearance.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.btn_Delete.Appearance.Options.UseFont = true;
            this.btn_Delete.Image = ((System.Drawing.Image)(resources.GetObject("btn_Delete.Image")));
            this.btn_Delete.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btn_Delete.Location = new System.Drawing.Point(416, 473);
            this.btn_Delete.Name = "btn_Delete";
            this.btn_Delete.Size = new System.Drawing.Size(82, 33);
            this.btn_Delete.TabIndex = 9;
            this.btn_Delete.Text = "删除";
            this.btn_Delete.Click += new System.EventHandler(this.Btn_Delete_Click);
            // 
            // btn_Sure
            // 
            this.btn_Sure.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btn_Sure.Appearance.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.btn_Sure.Appearance.Options.UseFont = true;
            this.btn_Sure.Image = ((System.Drawing.Image)(resources.GetObject("btn_Sure.Image")));
            this.btn_Sure.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btn_Sure.Location = new System.Drawing.Point(328, 473);
            this.btn_Sure.Name = "btn_Sure";
            this.btn_Sure.Size = new System.Drawing.Size(82, 33);
            this.btn_Sure.TabIndex = 8;
            this.btn_Sure.Text = "确定";
            this.btn_Sure.Click += new System.EventHandler(this.Btn_Sure_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold);
            this.label3.Location = new System.Drawing.Point(33, 94);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(90, 22);
            this.label3.TabIndex = 7;
            this.label3.Text = "填写意见：";
            // 
            // lbl_ObjName
            // 
            this.lbl_ObjName.AutoSize = true;
            this.lbl_ObjName.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.lbl_ObjName.Location = new System.Drawing.Point(137, 19);
            this.lbl_ObjName.Name = "lbl_ObjName";
            this.lbl_ObjName.Size = new System.Drawing.Size(40, 21);
            this.lbl_ObjName.TabIndex = 6;
            this.lbl_ObjName.Text = "XXX";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(5, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(122, 22);
            this.label1.TabIndex = 5;
            this.label1.Text = "当前质检对象：";
            // 
            // txt_Advice
            // 
            this.txt_Advice.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txt_Advice.Location = new System.Drawing.Point(137, 94);
            this.txt_Advice.MaxLength = 500;
            this.txt_Advice.Name = "txt_Advice";
            this.txt_Advice.Size = new System.Drawing.Size(678, 373);
            this.txt_Advice.TabIndex = 13;
            this.txt_Advice.Text = "";
            this.txt_Advice.WordWrap = false;
            // 
            // Frm_Advice
            // 
            this.Appearance.Options.UseFont = true;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(827, 511);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Name = "Frm_Advice";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "质检意见";
            this.Load += new System.EventHandler(this.Frm_Advice_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private KyoControl.KyoButton btn_Sure;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lbl_ObjName;
        private System.Windows.Forms.Label label1;
        private KyoControl.KyoButton btn_Delete;
        private System.Windows.Forms.ComboBox cbo_AdviceType;
        private System.Windows.Forms.Label label2;
        private KyoControl.KyoButton btn_HistroyOpinion;
        private System.Windows.Forms.RichTextBox txt_Advice;
    }
}