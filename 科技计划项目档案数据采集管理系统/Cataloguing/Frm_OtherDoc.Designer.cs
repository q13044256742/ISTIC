namespace 科技计划项目档案数据采集管理系统
{
    partial class Frm_OtherDoc
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_OtherDoc));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txt_Intro = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txt_Carrier = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txt_Code = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_Name = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_Sure = new 科技计划项目档案数据采集管理系统.KyoControl.KyoButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.view = new System.Windows.Forms.DataGridView();
            this.name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.carrier = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.intro = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btn_Edit = new 科技计划项目档案数据采集管理系统.KyoControl.KyoButton();
            this.btn_Delete = new 科技计划项目档案数据采集管理系统.KyoControl.KyoButton();
            this.btn_Add = new 科技计划项目档案数据采集管理系统.KyoControl.KyoButton();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.view)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.txt_Intro);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txt_Carrier);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txt_Code);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txt_Name);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 1);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(759, 233);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "信息录入";
            // 
            // txt_Intro
            // 
            this.txt_Intro.Location = new System.Drawing.Point(122, 136);
            this.txt_Intro.Multiline = true;
            this.txt_Intro.Name = "txt_Intro";
            this.txt_Intro.Size = new System.Drawing.Size(592, 84);
            this.txt_Intro.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(42, 139);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(58, 22);
            this.label4.TabIndex = 6;
            this.label4.Text = "说明：";
            // 
            // txt_Carrier
            // 
            this.txt_Carrier.Location = new System.Drawing.Point(122, 101);
            this.txt_Carrier.Name = "txt_Carrier";
            this.txt_Carrier.Size = new System.Drawing.Size(592, 29);
            this.txt_Carrier.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(42, 104);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 22);
            this.label3.TabIndex = 4;
            this.label3.Text = "载体：";
            // 
            // txt_Code
            // 
            this.txt_Code.Location = new System.Drawing.Point(122, 66);
            this.txt_Code.Name = "txt_Code";
            this.txt_Code.Size = new System.Drawing.Size(592, 29);
            this.txt_Code.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(42, 69);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 22);
            this.label2.TabIndex = 2;
            this.label2.Text = "编号：";
            // 
            // txt_Name
            // 
            this.txt_Name.Location = new System.Drawing.Point(122, 31);
            this.txt_Name.Name = "txt_Name";
            this.txt_Name.Size = new System.Drawing.Size(592, 29);
            this.txt_Name.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(42, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 22);
            this.label1.TabIndex = 0;
            this.label1.Text = "名称：";
            // 
            // btn_Sure
            // 
            this.btn_Sure.Appearance.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_Sure.Appearance.Options.UseFont = true;
            this.btn_Sure.Image = ((System.Drawing.Image)(resources.GetObject("btn_Sure.Image")));
            this.btn_Sure.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btn_Sure.ImageToTextIndent = 0;
            this.btn_Sure.Location = new System.Drawing.Point(672, 243);
            this.btn_Sure.Name = "btn_Sure";
            this.btn_Sure.Size = new System.Drawing.Size(69, 31);
            this.btn_Sure.TabIndex = 5;
            this.btn_Sure.Text = "保存";
            this.btn_Sure.Click += new System.EventHandler(this.Btn_Sure_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.view);
            this.groupBox2.Location = new System.Drawing.Point(12, 279);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(762, 392);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "档案列表";
            // 
            // view
            // 
            this.view.AllowUserToAddRows = false;
            this.view.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.view.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.view.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.view.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.view.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.view.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.name,
            this.code,
            this.carrier,
            this.intro});
            this.view.Dock = System.Windows.Forms.DockStyle.Fill;
            this.view.GridColor = System.Drawing.Color.Black;
            this.view.Location = new System.Drawing.Point(3, 25);
            this.view.Name = "view";
            this.view.ReadOnly = true;
            this.view.RowTemplate.Height = 23;
            this.view.Size = new System.Drawing.Size(756, 364);
            this.view.TabIndex = 0;
            this.view.RowHeaderMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.view_RowHeaderMouseDoubleClick);
            this.view.UserDeletingRow += new System.Windows.Forms.DataGridViewRowCancelEventHandler(this.View_UserDeletingRow);
            // 
            // name
            // 
            this.name.HeaderText = "名称";
            this.name.Name = "name";
            this.name.ReadOnly = true;
            this.name.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.name.Width = 204;
            // 
            // code
            // 
            this.code.HeaderText = "馆藏号";
            this.code.Name = "code";
            this.code.ReadOnly = true;
            this.code.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.code.Width = 203;
            // 
            // carrier
            // 
            this.carrier.FillWeight = 50F;
            this.carrier.HeaderText = "载体";
            this.carrier.Name = "carrier";
            this.carrier.ReadOnly = true;
            this.carrier.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.carrier.Width = 102;
            // 
            // intro
            // 
            this.intro.HeaderText = "说明";
            this.intro.Name = "intro";
            this.intro.ReadOnly = true;
            this.intro.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.intro.Width = 204;
            // 
            // btn_Edit
            // 
            this.btn_Edit.Appearance.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_Edit.Appearance.Options.UseFont = true;
            this.btn_Edit.Image = ((System.Drawing.Image)(resources.GetObject("btn_Edit.Image")));
            this.btn_Edit.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btn_Edit.ImageToTextIndent = 0;
            this.btn_Edit.Location = new System.Drawing.Point(597, 243);
            this.btn_Edit.Name = "btn_Edit";
            this.btn_Edit.Size = new System.Drawing.Size(69, 31);
            this.btn_Edit.TabIndex = 6;
            this.btn_Edit.Text = "编辑";
            this.btn_Edit.Click += new System.EventHandler(this.kyoButton1_Click);
            // 
            // btn_Delete
            // 
            this.btn_Delete.Appearance.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_Delete.Appearance.Options.UseFont = true;
            this.btn_Delete.Image = ((System.Drawing.Image)(resources.GetObject("btn_Delete.Image")));
            this.btn_Delete.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btn_Delete.ImageToTextIndent = 0;
            this.btn_Delete.Location = new System.Drawing.Point(522, 243);
            this.btn_Delete.Name = "btn_Delete";
            this.btn_Delete.Size = new System.Drawing.Size(69, 31);
            this.btn_Delete.TabIndex = 7;
            this.btn_Delete.Text = "删除";
            this.btn_Delete.Click += new System.EventHandler(this.btn_Delete_Click);
            // 
            // btn_Add
            // 
            this.btn_Add.Appearance.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_Add.Appearance.Options.UseFont = true;
            this.btn_Add.Image = ((System.Drawing.Image)(resources.GetObject("btn_Add.Image")));
            this.btn_Add.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btn_Add.ImageToTextIndent = 0;
            this.btn_Add.Location = new System.Drawing.Point(447, 243);
            this.btn_Add.Name = "btn_Add";
            this.btn_Add.Size = new System.Drawing.Size(69, 31);
            this.btn_Add.TabIndex = 8;
            this.btn_Add.Text = "新增";
            this.btn_Add.Click += new System.EventHandler(this.btn_Add_Click);
            // 
            // Frm_OtherDoc
            // 
            this.Appearance.Options.UseFont = true;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(786, 679);
            this.Controls.Add(this.btn_Add);
            this.Controls.Add(this.btn_Delete);
            this.Controls.Add(this.btn_Edit);
            this.Controls.Add(this.btn_Sure);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Frm_OtherDoc";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "其它载体档案录入";
            this.Load += new System.EventHandler(this.Frm_OtherDoc_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.view)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txt_Name;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_Intro;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txt_Carrier;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txt_Code;
        private System.Windows.Forms.Label label2;
        private KyoControl.KyoButton btn_Sure;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView view;
        private System.Windows.Forms.DataGridViewTextBoxColumn name;
        private System.Windows.Forms.DataGridViewTextBoxColumn code;
        private System.Windows.Forms.DataGridViewTextBoxColumn carrier;
        private System.Windows.Forms.DataGridViewTextBoxColumn intro;
        private KyoControl.KyoButton btn_Edit;
        private KyoControl.KyoButton btn_Delete;
        private KyoControl.KyoButton btn_Add;
    }
}