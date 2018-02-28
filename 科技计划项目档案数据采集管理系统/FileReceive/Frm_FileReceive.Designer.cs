namespace 科技计划项目档案数据采集管理系统.FileReceive
{
    partial class Frm_FileReceive
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_FileReceive));
            this.pal_LeftMenu = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txt_Search = new System.Windows.Forms.TextBox();
            this.btn_Search = new System.Windows.Forms.Button();
            this.dgv_PCList = new 科技计划项目档案数据采集管理系统.HeaderUnitView(this.components);
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_PCList)).BeginInit();
            this.SuspendLayout();
            // 
            // pal_LeftMenu
            // 
            this.pal_LeftMenu.AutoScroll = true;
            this.pal_LeftMenu.Dock = System.Windows.Forms.DockStyle.Left;
            this.pal_LeftMenu.Location = new System.Drawing.Point(0, 0);
            this.pal_LeftMenu.Name = "pal_LeftMenu";
            this.pal_LeftMenu.Size = new System.Drawing.Size(229, 491);
            this.pal_LeftMenu.TabIndex = 18;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.dgv_PCList);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox1.Location = new System.Drawing.Point(230, -6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(771, 502);
            this.groupBox1.TabIndex = 17;
            this.groupBox1.TabStop = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.txt_Search);
            this.groupBox2.Controls.Add(this.btn_Search);
            this.groupBox2.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.groupBox2.Location = new System.Drawing.Point(3, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(766, 49);
            this.groupBox2.TabIndex = 14;
            this.groupBox2.TabStop = false;
            // 
            // txt_Search
            // 
            this.txt_Search.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txt_Search.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_Search.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txt_Search.Location = new System.Drawing.Point(434, 19);
            this.txt_Search.Name = "txt_Search";
            this.txt_Search.Size = new System.Drawing.Size(251, 26);
            this.txt_Search.TabIndex = 4;
            // 
            // btn_Search
            // 
            this.btn_Search.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Search.AutoSize = true;
            this.btn_Search.Location = new System.Drawing.Point(693, 16);
            this.btn_Search.Name = "btn_Search";
            this.btn_Search.Size = new System.Drawing.Size(66, 28);
            this.btn_Search.TabIndex = 1;
            this.btn_Search.Text = "查询(&F)";
            this.btn_Search.UseVisualStyleBackColor = true;
            // 
            // dgv_PCList
            // 
            this.dgv_PCList.AllowUserToDeleteRows = false;
            this.dgv_PCList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgv_PCList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv_PCList.CellHeight = 17;
            this.dgv_PCList.ColumnDeep = 1;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgv_PCList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgv_PCList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_PCList.ColumnTreeView = null;
            this.dgv_PCList.Location = new System.Drawing.Point(3, 51);
            this.dgv_PCList.MergeColumnNames = ((System.Collections.Generic.List<string>)(resources.GetObject("dgv_PCList.MergeColumnNames")));
            this.dgv_PCList.Name = "dgv_PCList";
            this.dgv_PCList.ReadOnly = true;
            this.dgv_PCList.RefreshAtHscroll = false;
            this.dgv_PCList.RowTemplate.Height = 23;
            this.dgv_PCList.Size = new System.Drawing.Size(766, 445);
            this.dgv_PCList.TabIndex = 15;
            // 
            // Frm_FileReceive
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1001, 491);
            this.Controls.Add(this.pal_LeftMenu);
            this.Controls.Add(this.groupBox1);
            this.Name = "Frm_FileReceive";
            this.Text = "档案接收";
            this.Load += new System.EventHandler(this.Frm_FileReceive_load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_PCList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private HeaderUnitView dgv_PCList;
        private System.Windows.Forms.Panel pal_LeftMenu;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txt_Search;
        private System.Windows.Forms.Button btn_Search;
    }
}