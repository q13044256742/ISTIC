﻿namespace 科技计划项目档案数据采集管理系统
{
    partial class Frm_CG
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_CG));
            this.pal_LeftMenu = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cbo_CompanyList = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_Back = new System.Windows.Forms.Button();
            this.txt_Search = new System.Windows.Forms.TextBox();
            this.btn_Search = new System.Windows.Forms.Button();
            this.dgv_WorkLog = new 科技计划项目档案数据采集管理系统.HeaderUnitView(this.components);
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_WorkLog)).BeginInit();
            this.SuspendLayout();
            // 
            // pal_LeftMenu
            // 
            this.pal_LeftMenu.AutoScroll = true;
            this.pal_LeftMenu.Dock = System.Windows.Forms.DockStyle.Left;
            this.pal_LeftMenu.Location = new System.Drawing.Point(0, 0);
            this.pal_LeftMenu.Name = "pal_LeftMenu";
            this.pal_LeftMenu.Size = new System.Drawing.Size(229, 491);
            this.pal_LeftMenu.TabIndex = 16;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.dgv_WorkLog);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox1.Location = new System.Drawing.Point(230, -6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(771, 502);
            this.groupBox1.TabIndex = 15;
            this.groupBox1.TabStop = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.cbo_CompanyList);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.btn_Back);
            this.groupBox2.Controls.Add(this.txt_Search);
            this.groupBox2.Controls.Add(this.btn_Search);
            this.groupBox2.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.groupBox2.Location = new System.Drawing.Point(3, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(766, 49);
            this.groupBox2.TabIndex = 14;
            this.groupBox2.TabStop = false;
            // 
            // cbo_CompanyList
            // 
            this.cbo_CompanyList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo_CompanyList.FormattingEnabled = true;
            this.cbo_CompanyList.Location = new System.Drawing.Point(76, 16);
            this.cbo_CompanyList.Name = "cbo_CompanyList";
            this.cbo_CompanyList.Size = new System.Drawing.Size(185, 25);
            this.cbo_CompanyList.TabIndex = 7;
            this.cbo_CompanyList.SelectionChangeCommitted += new System.EventHandler(this.Cbo_CompanyList_SelectionChangeCommitted);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(6, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 17);
            this.label1.TabIndex = 6;
            this.label1.Text = "来源单位：";
            // 
            // btn_Back
            // 
            this.btn_Back.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Back.Location = new System.Drawing.Point(697, 14);
            this.btn_Back.Name = "btn_Back";
            this.btn_Back.Size = new System.Drawing.Size(66, 28);
            this.btn_Back.TabIndex = 5;
            this.btn_Back.Text = "返回(&B)";
            this.btn_Back.UseVisualStyleBackColor = true;
            this.btn_Back.Click += new System.EventHandler(this.Btn_Back_Click);
            // 
            // txt_Search
            // 
            this.txt_Search.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txt_Search.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_Search.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txt_Search.Location = new System.Drawing.Point(372, 17);
            this.txt_Search.Name = "txt_Search";
            this.txt_Search.Size = new System.Drawing.Size(251, 26);
            this.txt_Search.TabIndex = 4;
            // 
            // btn_Search
            // 
            this.btn_Search.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Search.AutoSize = true;
            this.btn_Search.Location = new System.Drawing.Point(629, 14);
            this.btn_Search.Name = "btn_Search";
            this.btn_Search.Size = new System.Drawing.Size(66, 28);
            this.btn_Search.TabIndex = 1;
            this.btn_Search.Text = "查询(&F)";
            this.btn_Search.UseVisualStyleBackColor = true;
            // 
            // dgv_WorkLog
            // 
            this.dgv_WorkLog.AllowUserToAddRows = false;
            this.dgv_WorkLog.AllowUserToDeleteRows = false;
            this.dgv_WorkLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgv_WorkLog.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv_WorkLog.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgv_WorkLog.CellHeight = 17;
            this.dgv_WorkLog.ColumnDeep = 1;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgv_WorkLog.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgv_WorkLog.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_WorkLog.ColumnTreeView = null;
            this.dgv_WorkLog.Location = new System.Drawing.Point(3, 51);
            this.dgv_WorkLog.MergeColumnNames = ((System.Collections.Generic.List<string>)(resources.GetObject("dgv_WorkLog.MergeColumnNames")));
            this.dgv_WorkLog.Name = "dgv_WorkLog";
            this.dgv_WorkLog.ReadOnly = true;
            this.dgv_WorkLog.RefreshAtHscroll = false;
            this.dgv_WorkLog.RowTemplate.Height = 23;
            this.dgv_WorkLog.Size = new System.Drawing.Size(766, 445);
            this.dgv_WorkLog.TabIndex = 15;
            this.dgv_WorkLog.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Dgv_WorkLog_CellClick);
            // 
            // Frm_CG
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1001, 491);
            this.Controls.Add(this.pal_LeftMenu);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Frm_CG";
            this.ShowInTaskbar = false;
            this.Text = "著录加工";
            this.Load += new System.EventHandler(this.Frm_CG_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_WorkLog)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel pal_LeftMenu;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txt_Search;
        private System.Windows.Forms.Button btn_Search;
        private System.Windows.Forms.Button btn_Back;
        private System.Windows.Forms.ComboBox cbo_CompanyList;
        private System.Windows.Forms.Label label1;
        private HeaderUnitView dgv_WorkLog;
    }
}