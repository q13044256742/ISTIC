﻿namespace 科技计划项目档案数据采集管理系统.TransferOfRegistration
{
    partial class Frm_ToR
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
            this.dgv_GPDJ = new System.Windows.Forms.DataGridView();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.dgv_SWDJ = new System.Windows.Forms.DataGridView();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tc_ToR = new System.Windows.Forms.TabControl();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.pal_YJDJ = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.pal_XTSY = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.pal_LeftMenu = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txt_Search = new System.Windows.Forms.TextBox();
            this.btn_Back = new System.Windows.Forms.Button();
            this.btn_Delete = new System.Windows.Forms.Button();
            this.btn_Search = new System.Windows.Forms.Button();
            this.btn_Add = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.txt_CD_Search = new System.Windows.Forms.TextBox();
            this.btn_CD_Search = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_GPDJ)).BeginInit();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_SWDJ)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.tc_ToR.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.pal_YJDJ.SuspendLayout();
            this.pal_XTSY.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgv_GPDJ
            // 
            this.dgv_GPDJ.AllowUserToDeleteRows = false;
            this.dgv_GPDJ.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgv_GPDJ.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv_GPDJ.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgv_GPDJ.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_GPDJ.Location = new System.Drawing.Point(2, 44);
            this.dgv_GPDJ.Name = "dgv_GPDJ";
            this.dgv_GPDJ.ReadOnly = true;
            this.dgv_GPDJ.RowTemplate.Height = 23;
            this.dgv_GPDJ.Size = new System.Drawing.Size(759, 415);
            this.dgv_GPDJ.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.groupBox2);
            this.tabPage1.Controls.Add(this.dgv_SWDJ);
            this.tabPage1.Location = new System.Drawing.Point(4, 30);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(764, 458);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "实物登记";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // dgv_SWDJ
            // 
            this.dgv_SWDJ.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgv_SWDJ.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv_SWDJ.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgv_SWDJ.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_SWDJ.Location = new System.Drawing.Point(2, 44);
            this.dgv_SWDJ.Name = "dgv_SWDJ";
            this.dgv_SWDJ.ReadOnly = true;
            this.dgv_SWDJ.RowTemplate.Height = 23;
            this.dgv_SWDJ.Size = new System.Drawing.Size(757, 414);
            this.dgv_SWDJ.TabIndex = 0;
            this.dgv_SWDJ.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_SWDJ_CellClick);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.groupBox3);
            this.tabPage2.Controls.Add(this.dgv_GPDJ);
            this.tabPage2.Location = new System.Drawing.Point(4, 30);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(764, 458);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "光盘登记";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tc_ToR
            // 
            this.tc_ToR.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tc_ToR.Controls.Add(this.tabPage1);
            this.tc_ToR.Controls.Add(this.tabPage2);
            this.tc_ToR.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.tc_ToR.Location = new System.Drawing.Point(1, 10);
            this.tc_ToR.Name = "tc_ToR";
            this.tc_ToR.SelectedIndex = 0;
            this.tc_ToR.Size = new System.Drawing.Size(772, 492);
            this.tc_ToR.TabIndex = 8;
            this.tc_ToR.SelectedIndexChanged += new System.EventHandler(this.tc_ToR_SelectedIndexChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.tc_ToR);
            this.groupBox1.Location = new System.Drawing.Point(230, -9);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(771, 502);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            // 
            // pal_YJDJ
            // 
            this.pal_YJDJ.BackColor = System.Drawing.Color.Transparent;
            this.pal_YJDJ.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pal_YJDJ.Controls.Add(this.label5);
            this.pal_YJDJ.Dock = System.Windows.Forms.DockStyle.Top;
            this.pal_YJDJ.Location = new System.Drawing.Point(0, 53);
            this.pal_YJDJ.Name = "pal_YJDJ";
            this.pal_YJDJ.Size = new System.Drawing.Size(228, 53);
            this.pal_YJDJ.TabIndex = 3;
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Tahoma", 11F);
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Image = global::科技计划项目档案数据采集管理系统.Properties.Resources.pic2;
            this.label5.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label5.Location = new System.Drawing.Point(33, 8);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(114, 36);
            this.label5.TabIndex = 0;
            this.label5.Text = "移交登记";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pal_XTSY
            // 
            this.pal_XTSY.BackColor = System.Drawing.Color.Transparent;
            this.pal_XTSY.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pal_XTSY.Controls.Add(this.label4);
            this.pal_XTSY.Dock = System.Windows.Forms.DockStyle.Top;
            this.pal_XTSY.Location = new System.Drawing.Point(0, 0);
            this.pal_XTSY.Name = "pal_XTSY";
            this.pal_XTSY.Size = new System.Drawing.Size(228, 53);
            this.pal_XTSY.TabIndex = 2;
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Tahoma", 11F);
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Image = global::科技计划项目档案数据采集管理系统.Properties.Resources.pic1;
            this.label4.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label4.Location = new System.Drawing.Point(33, 8);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(114, 36);
            this.label4.TabIndex = 0;
            this.label4.Text = "系统首页";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pal_LeftMenu
            // 
            this.pal_LeftMenu.AutoScroll = true;
            this.pal_LeftMenu.Dock = System.Windows.Forms.DockStyle.Left;
            this.pal_LeftMenu.Location = new System.Drawing.Point(0, 0);
            this.pal_LeftMenu.Name = "pal_LeftMenu";
            this.pal_LeftMenu.Size = new System.Drawing.Size(229, 491);
            this.pal_LeftMenu.TabIndex = 12;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.txt_Search);
            this.groupBox2.Controls.Add(this.btn_Back);
            this.groupBox2.Controls.Add(this.btn_Delete);
            this.groupBox2.Controls.Add(this.btn_Search);
            this.groupBox2.Controls.Add(this.btn_Add);
            this.groupBox2.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.groupBox2.Location = new System.Drawing.Point(2, -5);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(757, 49);
            this.groupBox2.TabIndex = 12;
            this.groupBox2.TabStop = false;
            // 
            // txt_Search
            // 
            this.txt_Search.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txt_Search.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_Search.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txt_Search.Location = new System.Drawing.Point(207, 15);
            this.txt_Search.Name = "txt_Search";
            this.txt_Search.Size = new System.Drawing.Size(251, 26);
            this.txt_Search.TabIndex = 4;
            // 
            // btn_Back
            // 
            this.btn_Back.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Back.AutoSize = true;
            this.btn_Back.Location = new System.Drawing.Point(688, 13);
            this.btn_Back.Name = "btn_Back";
            this.btn_Back.Size = new System.Drawing.Size(66, 30);
            this.btn_Back.TabIndex = 3;
            this.btn_Back.Text = "返回(&B)";
            this.btn_Back.UseVisualStyleBackColor = true;
            this.btn_Back.Click += new System.EventHandler(this.btn_Back_Click);
            // 
            // btn_Delete
            // 
            this.btn_Delete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Delete.AutoSize = true;
            this.btn_Delete.Location = new System.Drawing.Point(612, 13);
            this.btn_Delete.Name = "btn_Delete";
            this.btn_Delete.Size = new System.Drawing.Size(68, 30);
            this.btn_Delete.TabIndex = 2;
            this.btn_Delete.Text = "删除(&D)";
            this.btn_Delete.UseVisualStyleBackColor = true;
            this.btn_Delete.Click += new System.EventHandler(this.btn_Delete_Click);
            // 
            // btn_Search
            // 
            this.btn_Search.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Search.AutoSize = true;
            this.btn_Search.Location = new System.Drawing.Point(464, 13);
            this.btn_Search.Name = "btn_Search";
            this.btn_Search.Size = new System.Drawing.Size(65, 30);
            this.btn_Search.TabIndex = 1;
            this.btn_Search.Text = "查询(&F)";
            this.btn_Search.UseVisualStyleBackColor = true;
            this.btn_Search.Click += new System.EventHandler(this.btn_Search_Click);
            // 
            // btn_Add
            // 
            this.btn_Add.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Add.AutoSize = true;
            this.btn_Add.Location = new System.Drawing.Point(537, 13);
            this.btn_Add.Name = "btn_Add";
            this.btn_Add.Size = new System.Drawing.Size(67, 30);
            this.btn_Add.TabIndex = 0;
            this.btn_Add.Text = "添加(&A)";
            this.btn_Add.UseVisualStyleBackColor = true;
            this.btn_Add.Click += new System.EventHandler(this.btn_Add_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.txt_CD_Search);
            this.groupBox3.Controls.Add(this.btn_CD_Search);
            this.groupBox3.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.groupBox3.Location = new System.Drawing.Point(2, -5);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(758, 49);
            this.groupBox3.TabIndex = 13;
            this.groupBox3.TabStop = false;
            // 
            // txt_CD_Search
            // 
            this.txt_CD_Search.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txt_CD_Search.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_CD_Search.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txt_CD_Search.Location = new System.Drawing.Point(429, 16);
            this.txt_CD_Search.Name = "txt_CD_Search";
            this.txt_CD_Search.Size = new System.Drawing.Size(251, 26);
            this.txt_CD_Search.TabIndex = 4;
            // 
            // btn_CD_Search
            // 
            this.btn_CD_Search.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_CD_Search.AutoSize = true;
            this.btn_CD_Search.Location = new System.Drawing.Point(686, 13);
            this.btn_CD_Search.Name = "btn_CD_Search";
            this.btn_CD_Search.Size = new System.Drawing.Size(65, 30);
            this.btn_CD_Search.TabIndex = 1;
            this.btn_CD_Search.Text = "查询(&F)";
            this.btn_CD_Search.UseVisualStyleBackColor = true;
            this.btn_CD_Search.Click += new System.EventHandler(this.btn_CD_Search_Click);
            // 
            // Frm_ToR
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1001, 491);
            this.Controls.Add(this.pal_LeftMenu);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Frm_ToR";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Frm_ToR_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_GPDJ)).EndInit();
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_SWDJ)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tc_ToR.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.pal_YJDJ.ResumeLayout(false);
            this.pal_XTSY.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pal_YJDJ;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel pal_XTSY;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridView dgv_GPDJ;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.DataGridView dgv_SWDJ;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabControl tc_ToR;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Panel pal_LeftMenu;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txt_Search;
        private System.Windows.Forms.Button btn_Back;
        private System.Windows.Forms.Button btn_Delete;
        private System.Windows.Forms.Button btn_Search;
        private System.Windows.Forms.Button btn_Add;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox txt_CD_Search;
        private System.Windows.Forms.Button btn_CD_Search;
    }
}