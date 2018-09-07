namespace 科技计划项目档案数据采集管理系统.Manager
{
    partial class Frm_Manager
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_Manager));
            this.dgv_DataList = new System.Windows.Forms.DataGridView();
            this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.note = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.extend_3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sort = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btn_Back = new 科技计划项目档案数据采集管理系统.KyoControl.KyoButton();
            this.button4 = new 科技计划项目档案数据采集管理系统.KyoControl.KyoButton();
            this.button3 = new 科技计划项目档案数据采集管理系统.KyoControl.KyoButton();
            this.btn_Add = new 科技计划项目档案数据采集管理系统.KyoControl.KyoButton();
            this.btn_Search = new 科技计划项目档案数据采集管理系统.KyoControl.KyoButton();
            this.txt_SearchKey = new System.Windows.Forms.TextBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cms_move = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_DataList)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgv_DataList
            // 
            this.dgv_DataList.AllowUserToAddRows = false;
            this.dgv_DataList.AllowUserToDeleteRows = false;
            this.dgv_DataList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv_DataList.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgv_DataList.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Tahoma", 9F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgv_DataList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgv_DataList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_DataList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.id,
            this.name,
            this.code,
            this.note,
            this.extend_3,
            this.sort});
            this.dgv_DataList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_DataList.Location = new System.Drawing.Point(0, 64);
            this.dgv_DataList.Name = "dgv_DataList";
            this.dgv_DataList.ReadOnly = true;
            this.dgv_DataList.RowTemplate.Height = 23;
            this.dgv_DataList.Size = new System.Drawing.Size(923, 452);
            this.dgv_DataList.TabIndex = 1;
            this.dgv_DataList.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.Dgv_DataList_CellMouseDoubleClick);
            this.dgv_DataList.RowHeaderMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.Dgv_DataList_ColumnHeaderMouseDoubleClick);
            // 
            // id
            // 
            this.id.HeaderText = "id";
            this.id.Name = "id";
            this.id.ReadOnly = true;
            this.id.Visible = false;
            // 
            // name
            // 
            this.name.FillWeight = 80F;
            this.name.HeaderText = "名称";
            this.name.Name = "name";
            this.name.ReadOnly = true;
            // 
            // code
            // 
            this.code.FillWeight = 60F;
            this.code.HeaderText = "编码";
            this.code.Name = "code";
            this.code.ReadOnly = true;
            // 
            // note
            // 
            this.note.FillWeight = 250F;
            this.note.HeaderText = "描述";
            this.note.Name = "note";
            this.note.ReadOnly = true;
            // 
            // extend_3
            // 
            this.extend_3.FillWeight = 50F;
            this.extend_3.HeaderText = "扩展";
            this.extend_3.Name = "extend_3";
            this.extend_3.ReadOnly = true;
            // 
            // sort
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.sort.DefaultCellStyle = dataGridViewCellStyle2;
            this.sort.FillWeight = 50F;
            this.sort.HeaderText = "排序";
            this.sort.Name = "sort";
            this.sort.ReadOnly = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btn_Back);
            this.groupBox1.Controls.Add(this.button4);
            this.groupBox1.Controls.Add(this.button3);
            this.groupBox1.Controls.Add(this.btn_Add);
            this.groupBox1.Controls.Add(this.btn_Search);
            this.groupBox1.Controls.Add(this.txt_SearchKey);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(923, 64);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "快速检索";
            // 
            // btn_Back
            // 
            this.btn_Back.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Back.Enabled = false;
            this.btn_Back.Image = ((System.Drawing.Image)(resources.GetObject("btn_Back.Image")));
            this.btn_Back.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btn_Back.Location = new System.Drawing.Point(844, 26);
            this.btn_Back.Name = "btn_Back";
            this.btn_Back.Size = new System.Drawing.Size(66, 28);
            this.btn_Back.TabIndex = 13;
            this.btn_Back.Text = "返回";
            this.btn_Back.Click += new System.EventHandler(this.Btn_backClick);
            // 
            // button4
            // 
            this.button4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button4.Image = ((System.Drawing.Image)(resources.GetObject("button4.Image")));
            this.button4.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.button4.Location = new System.Drawing.Point(773, 26);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(66, 28);
            this.button4.TabIndex = 12;
            this.button4.Text = "删除";
            this.button4.Click += new System.EventHandler(this.Btn_Delete);
            // 
            // button3
            // 
            this.button3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button3.Image = ((System.Drawing.Image)(resources.GetObject("button3.Image")));
            this.button3.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.button3.Location = new System.Drawing.Point(702, 26);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(66, 28);
            this.button3.TabIndex = 11;
            this.button3.Text = "修改";
            this.button3.Click += new System.EventHandler(this.Btn_updateClick);
            // 
            // btn_Add
            // 
            this.btn_Add.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Add.Image = ((System.Drawing.Image)(resources.GetObject("btn_Add.Image")));
            this.btn_Add.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btn_Add.Location = new System.Drawing.Point(631, 26);
            this.btn_Add.Name = "btn_Add";
            this.btn_Add.Size = new System.Drawing.Size(66, 28);
            this.btn_Add.TabIndex = 10;
            this.btn_Add.Text = "新增";
            this.btn_Add.Click += new System.EventHandler(this.Btn_Add_Click);
            // 
            // btn_Search
            // 
            this.btn_Search.Location = new System.Drawing.Point(263, 26);
            this.btn_Search.Name = "btn_Search";
            this.btn_Search.Size = new System.Drawing.Size(66, 28);
            this.btn_Search.TabIndex = 9;
            this.btn_Search.Text = "查询";
            this.btn_Search.Click += new System.EventHandler(this.btn_Search_Click);
            // 
            // txt_SearchKey
            // 
            this.txt_SearchKey.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_SearchKey.Location = new System.Drawing.Point(13, 27);
            this.txt_SearchKey.Name = "txt_SearchKey";
            this.txt_SearchKey.Size = new System.Drawing.Size(244, 26);
            this.txt_SearchKey.TabIndex = 8;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cms_move});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(113, 26);
            // 
            // cms_move
            // 
            this.cms_move.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem2});
            this.cms_move.Name = "cms_move";
            this.cms_move.Size = new System.Drawing.Size(112, 22);
            this.cms_move.Text = "合并至";
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(97, 22);
            this.toolStripMenuItem2.Text = "123";
            // 
            // Frm_Manager
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(923, 516);
            this.Controls.Add(this.dgv_DataList);
            this.Controls.Add(this.groupBox1);
            this.Name = "Frm_Manager";
            this.Text = "后台管理";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)(this.dgv_DataList)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBox1;
        private KyoControl.KyoButton btn_Search;
        private System.Windows.Forms.TextBox txt_SearchKey;
        private System.Windows.Forms.DataGridView dgv_DataList;
        private KyoControl.KyoButton button4;
        private KyoControl.KyoButton button3;
        private KyoControl.KyoButton btn_Add;
        private KyoControl.KyoButton btn_Back;
        private System.Windows.Forms.DataGridViewTextBoxColumn id;
        private System.Windows.Forms.DataGridViewTextBoxColumn name;
        private System.Windows.Forms.DataGridViewTextBoxColumn code;
        private System.Windows.Forms.DataGridViewTextBoxColumn note;
        private System.Windows.Forms.DataGridViewTextBoxColumn extend_3;
        private System.Windows.Forms.DataGridViewTextBoxColumn sort;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem cms_move;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
    }
}