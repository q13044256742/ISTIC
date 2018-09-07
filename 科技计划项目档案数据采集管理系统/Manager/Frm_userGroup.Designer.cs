namespace 科技计划项目档案数据采集管理系统
{
    partial class Frm_userGroup
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button1 = new 科技计划项目档案数据采集管理系统.KyoControl.KyoButton();
            this.button4 = new 科技计划项目档案数据采集管理系统.KyoControl.KyoButton();
            this.button3 = new 科技计划项目档案数据采集管理系统.KyoControl.KyoButton();
            this.btn_Add = new 科技计划项目档案数据采集管理系统.KyoControl.KyoButton();
            this.btn_Search = new 科技计划项目档案数据采集管理系统.KyoControl.KyoButton();
            this.userGroup_SearchKey = new System.Windows.Forms.TextBox();
            this.view = new System.Windows.Forms.DataGridView();
            this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.note = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.extend_3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sort = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.view)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.button4);
            this.groupBox1.Controls.Add(this.button3);
            this.groupBox1.Controls.Add(this.btn_Add);
            this.groupBox1.Controls.Add(this.btn_Search);
            this.groupBox1.Controls.Add(this.userGroup_SearchKey);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1001, 55);
            this.groupBox1.TabIndex = 23;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "快速检索";
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(927, 20);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(68, 27);
            this.button1.TabIndex = 13;
            this.button1.Text = "授权";
            this.button1.Click += new System.EventHandler(this.Btn_authorization);
            // 
            // button4
            // 
            this.button4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button4.Location = new System.Drawing.Point(851, 20);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(68, 27);
            this.button4.TabIndex = 12;
            this.button4.Text = "删除";
            this.button4.Click += new System.EventHandler(this.UG_btnDel);
            // 
            // button3
            // 
            this.button3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button3.Location = new System.Drawing.Point(775, 20);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(68, 27);
            this.button3.TabIndex = 11;
            this.button3.Text = "修改";
            this.button3.Click += new System.EventHandler(this.UG_btnUpdate);
            // 
            // btn_Add
            // 
            this.btn_Add.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Add.Location = new System.Drawing.Point(699, 20);
            this.btn_Add.Name = "btn_Add";
            this.btn_Add.Size = new System.Drawing.Size(68, 27);
            this.btn_Add.TabIndex = 10;
            this.btn_Add.Text = "新增";
            this.btn_Add.Click += new System.EventHandler(this.UG_btnAdd);
            // 
            // btn_Search
            // 
            this.btn_Search.AutoSize = true;
            this.btn_Search.Location = new System.Drawing.Point(342, 21);
            this.btn_Search.Name = "btn_Search";
            this.btn_Search.Size = new System.Drawing.Size(68, 27);
            this.btn_Search.TabIndex = 9;
            this.btn_Search.Text = "查询";
            this.btn_Search.Click += new System.EventHandler(this.UG_btnSearch);
            // 
            // userGroup_SearchKey
            // 
            this.userGroup_SearchKey.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.userGroup_SearchKey.Location = new System.Drawing.Point(6, 21);
            this.userGroup_SearchKey.Name = "userGroup_SearchKey";
            this.userGroup_SearchKey.Size = new System.Drawing.Size(330, 26);
            this.userGroup_SearchKey.TabIndex = 8;
            // 
            // view
            // 
            this.view.AllowUserToAddRows = false;
            this.view.AllowUserToDeleteRows = false;
            this.view.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.view.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.view.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Tahoma", 9F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.view.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.view.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.view.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.id,
            this.name,
            this.code,
            this.note,
            this.extend_3,
            this.sort});
            this.view.Dock = System.Windows.Forms.DockStyle.Fill;
            this.view.Location = new System.Drawing.Point(0, 55);
            this.view.Name = "view";
            this.view.ReadOnly = true;
            this.view.RowTemplate.Height = 23;
            this.view.Size = new System.Drawing.Size(1001, 436);
            this.view.TabIndex = 24;
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
            // Frm_userGroup
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1001, 491);
            this.Controls.Add(this.view);
            this.Controls.Add(this.groupBox1);
            this.Name = "Frm_userGroup";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "角色授权";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Frm_userGroup_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.view)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBox1;
        private KyoControl.KyoButton button4;
        private KyoControl.KyoButton button3;
        private KyoControl.KyoButton btn_Add;
        private KyoControl.KyoButton btn_Search;
        private System.Windows.Forms.TextBox userGroup_SearchKey;
        private KyoControl.KyoButton button1;
        private System.Windows.Forms.DataGridView view;
        private System.Windows.Forms.DataGridViewTextBoxColumn id;
        private System.Windows.Forms.DataGridViewTextBoxColumn name;
        private System.Windows.Forms.DataGridViewTextBoxColumn code;
        private System.Windows.Forms.DataGridViewTextBoxColumn note;
        private System.Windows.Forms.DataGridViewTextBoxColumn extend_3;
        private System.Windows.Forms.DataGridViewTextBoxColumn sort;
    }
}