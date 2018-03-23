namespace 科技计划项目档案数据采集管理系统
{
    partial class Frm_AdviceBW
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dgv_BW = new System.Windows.Forms.DataGridView();
            this.bw_type = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bw_text = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lbl_ObjName = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_BW)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.dgv_BW);
            this.panel1.Controls.Add(this.lbl_ObjName);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(750, 406);
            this.panel1.TabIndex = 0;
            // 
            // dgv_BW
            // 
            this.dgv_BW.AllowUserToAddRows = false;
            this.dgv_BW.AllowUserToDeleteRows = false;
            this.dgv_BW.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv_BW.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgv_BW.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgv_BW.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_BW.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.bw_type,
            this.bw_text});
            this.dgv_BW.Location = new System.Drawing.Point(13, 48);
            this.dgv_BW.Name = "dgv_BW";
            this.dgv_BW.ReadOnly = true;
            this.dgv_BW.RowTemplate.Height = 23;
            this.dgv_BW.Size = new System.Drawing.Size(725, 346);
            this.dgv_BW.TabIndex = 7;
            // 
            // bw_type
            // 
            this.bw_type.FillWeight = 50F;
            this.bw_type.HeaderText = "意见类型";
            this.bw_type.Name = "bw_type";
            this.bw_type.ReadOnly = true;
            // 
            // bw_text
            // 
            this.bw_text.FillWeight = 150F;
            this.bw_text.HeaderText = "意见";
            this.bw_text.Name = "bw_text";
            this.bw_text.ReadOnly = true;
            // 
            // lbl_ObjName
            // 
            this.lbl_ObjName.AutoSize = true;
            this.lbl_ObjName.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbl_ObjName.Location = new System.Drawing.Point(124, 18);
            this.lbl_ObjName.Name = "lbl_ObjName";
            this.lbl_ObjName.Size = new System.Drawing.Size(0, 17);
            this.lbl_ObjName.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(11, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(107, 19);
            this.label1.TabIndex = 5;
            this.label1.Text = "当前质检对象：";
            // 
            // Frm_AdviceBW
            // 
            this.Appearance.Options.UseFont = true;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(750, 406);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Frm_AdviceBW";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "质检意见";
            this.Load += new System.EventHandler(this.Frm_Advice_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_BW)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lbl_ObjName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgv_BW;
        private System.Windows.Forms.DataGridViewTextBoxColumn bw_type;
        private System.Windows.Forms.DataGridViewTextBoxColumn bw_text;
    }
}