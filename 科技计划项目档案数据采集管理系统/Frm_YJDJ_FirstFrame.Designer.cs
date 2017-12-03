namespace 科技计划项目档案数据采集管理系统
{
    partial class Frm_YJDJ_FirstFrame
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.dgv_SWDJ = new System.Windows.Forms.DataGridView();
            this.来源单位 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewButtonColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewButtonColumn();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.panel1 = new System.Windows.Forms.Panel();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.btn_Find = new System.Windows.Forms.Label();
            this.btn_Delete = new System.Windows.Forms.Label();
            this.btn_Add = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_SWDJ)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tabControl1.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl1.Location = new System.Drawing.Point(0, 42);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1015, 372);
            this.tabControl1.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.dgv_SWDJ);
            this.tabPage1.Location = new System.Drawing.Point(4, 28);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1007, 340);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "实物登记";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // dgv_SWDJ
            // 
            this.dgv_SWDJ.AllowUserToAddRows = false;
            this.dgv_SWDJ.AllowUserToDeleteRows = false;
            this.dgv_SWDJ.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dgv_SWDJ.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgv_SWDJ.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_SWDJ.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.来源单位,
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column5,
            this.Column6,
            this.Column7});
            this.dgv_SWDJ.Dock = System.Windows.Forms.DockStyle.Top;
            this.dgv_SWDJ.EnableHeadersVisualStyles = false;
            this.dgv_SWDJ.Location = new System.Drawing.Point(3, 3);
            this.dgv_SWDJ.Name = "dgv_SWDJ";
            this.dgv_SWDJ.ReadOnly = true;
            this.dgv_SWDJ.RowHeadersVisible = false;
            this.dgv_SWDJ.RowTemplate.Height = 23;
            this.dgv_SWDJ.Size = new System.Drawing.Size(1001, 285);
            this.dgv_SWDJ.TabIndex = 0;
            this.dgv_SWDJ.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Dgv_SWDJ_CellContentClick);
            this.dgv_SWDJ.CellMouseEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.Dgv_SWDJ_CellMouseEnter);
            this.dgv_SWDJ.CellMouseLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.Dgv_SWDJ_CellMouseLeave);
            // 
            // 来源单位
            // 
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.来源单位.DefaultCellStyle = dataGridViewCellStyle3;
            this.来源单位.HeaderText = "来源单位";
            this.来源单位.Name = "来源单位";
            this.来源单位.ReadOnly = true;
            this.来源单位.Width = 150;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "批次名称";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Width = 200;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "批次号";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Width = 105;
            // 
            // Column3
            // 
            dataGridViewCellStyle4.Format = "d";
            dataGridViewCellStyle4.NullValue = null;
            this.Column3.DefaultCellStyle = dataGridViewCellStyle4;
            this.Column3.HeaderText = "加工完成时间";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.Width = 150;
            // 
            // Column4
            // 
            this.Column4.HeaderText = "纸本数";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            // 
            // Column5
            // 
            this.Column5.HeaderText = "光盘数";
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            // 
            // Column6
            // 
            this.Column6.HeaderText = "添加光盘";
            this.Column6.Name = "Column6";
            this.Column6.ReadOnly = true;
            this.Column6.Width = 90;
            // 
            // Column7
            // 
            this.Column7.HeaderText = "操作";
            this.Column7.Name = "Column7";
            this.Column7.ReadOnly = true;
            this.Column7.Width = 90;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 28);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1000, 340);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "光盘登记";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.BackgroundImage = global::科技计划项目档案数据采集管理系统.Properties.Resources.dhbg;
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel1.Controls.Add(this.textBox1);
            this.panel1.Controls.Add(this.btn_Find);
            this.panel1.Controls.Add(this.btn_Delete);
            this.panel1.Controls.Add(this.btn_Add);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1015, 42);
            this.panel1.TabIndex = 0;
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.Location = new System.Drawing.Point(635, 8);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(136, 27);
            this.textBox1.TabIndex = 3;
            // 
            // btn_Find
            // 
            this.btn_Find.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Find.BackColor = System.Drawing.Color.Transparent;
            this.btn_Find.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.btn_Find.Image = global::科技计划项目档案数据采集管理系统.Properties.Resources.chaxun;
            this.btn_Find.Location = new System.Drawing.Point(777, 5);
            this.btn_Find.Name = "btn_Find";
            this.btn_Find.Size = new System.Drawing.Size(70, 33);
            this.btn_Find.TabIndex = 2;
            this.btn_Find.MouseEnter += new System.EventHandler(this.btn_Find_MouseEnter);
            this.btn_Find.MouseLeave += new System.EventHandler(this.btn_Find_MouseLeave);
            // 
            // btn_Delete
            // 
            this.btn_Delete.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Delete.BackColor = System.Drawing.Color.Transparent;
            this.btn_Delete.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.btn_Delete.Image = global::科技计划项目档案数据采集管理系统.Properties.Resources.shanchu;
            this.btn_Delete.Location = new System.Drawing.Point(933, 5);
            this.btn_Delete.Name = "btn_Delete";
            this.btn_Delete.Size = new System.Drawing.Size(70, 33);
            this.btn_Delete.TabIndex = 1;
            this.btn_Delete.MouseEnter += new System.EventHandler(this.btn_Find_MouseEnter);
            this.btn_Delete.MouseLeave += new System.EventHandler(this.btn_Find_MouseLeave);
            // 
            // btn_Add
            // 
            this.btn_Add.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Add.BackColor = System.Drawing.Color.Transparent;
            this.btn_Add.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.btn_Add.Image = global::科技计划项目档案数据采集管理系统.Properties.Resources.tianjia;
            this.btn_Add.Location = new System.Drawing.Point(855, 5);
            this.btn_Add.Name = "btn_Add";
            this.btn_Add.Size = new System.Drawing.Size(70, 33);
            this.btn_Add.TabIndex = 0;
            this.btn_Add.Click += new System.EventHandler(this.btn_Add_Click);
            this.btn_Add.MouseEnter += new System.EventHandler(this.btn_Find_MouseEnter);
            this.btn_Add.MouseLeave += new System.EventHandler(this.btn_Find_MouseLeave);
            // 
            // Frm_YJDJ_FirstFrame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1015, 488);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Frm_YJDJ_FirstFrame";
            this.Text = "Frm_YJDJ_FirstFrame";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_SWDJ)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label btn_Add;
        private System.Windows.Forms.Label btn_Find;
        private System.Windows.Forms.Label btn_Delete;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.DataGridView dgv_SWDJ;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.DataGridViewTextBoxColumn 来源单位;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewButtonColumn Column6;
        private System.Windows.Forms.DataGridViewButtonColumn Column7;
    }
}