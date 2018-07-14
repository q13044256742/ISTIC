namespace 科技计划项目档案数据采集管理系统
{
    partial class Frm_PrintBox
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_PrintBox));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.view = new System.Windows.Forms.DataGridView();
            this.chk_PrintAll = new System.Windows.Forms.CheckBox();
            this.chk_BKB = new System.Windows.Forms.CheckBox();
            this.chk_FMBJ = new System.Windows.Forms.CheckBox();
            this.chk_JNML = new System.Windows.Forms.CheckBox();
            this.cbo_BJ = new System.Windows.Forms.ComboBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tip = new System.Windows.Forms.ToolStripStatusLabel();
            this.btn_StartPrint = new 科技计划项目档案数据采集管理系统.KyoControl.KyoButton();
            this.print = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.amount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bkb = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.fm = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.fmbj = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.jnml = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.view)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
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
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.Padding = new System.Windows.Forms.Padding(5);
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.view.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.view.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.view.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.print,
            this.id,
            this.amount,
            this.bkb,
            this.fm,
            this.fmbj,
            this.jnml});
            this.view.Dock = System.Windows.Forms.DockStyle.Top;
            this.view.Location = new System.Drawing.Point(0, 0);
            this.view.Name = "view";
            this.view.RowHeadersVisible = false;
            this.view.RowTemplate.Height = 23;
            this.view.Size = new System.Drawing.Size(824, 422);
            this.view.TabIndex = 2;
            // 
            // chk_PrintAll
            // 
            this.chk_PrintAll.AutoSize = true;
            this.chk_PrintAll.Location = new System.Drawing.Point(12, 454);
            this.chk_PrintAll.Name = "chk_PrintAll";
            this.chk_PrintAll.Size = new System.Drawing.Size(91, 20);
            this.chk_PrintAll.TabIndex = 3;
            this.chk_PrintAll.Text = "全部打印";
            this.chk_PrintAll.UseVisualStyleBackColor = true;
            this.chk_PrintAll.CheckedChanged += new System.EventHandler(this.Chk_PrintAll_CheckedChanged);
            // 
            // chk_BKB
            // 
            this.chk_BKB.AutoSize = true;
            this.chk_BKB.Location = new System.Drawing.Point(150, 454);
            this.chk_BKB.Name = "chk_BKB";
            this.chk_BKB.Size = new System.Drawing.Size(107, 20);
            this.chk_BKB.TabIndex = 4;
            this.chk_BKB.Text = "卷内备考表";
            this.chk_BKB.UseVisualStyleBackColor = true;
            this.chk_BKB.CheckedChanged += new System.EventHandler(this.Chk_BKB_CheckedChanged);
            // 
            // chk_FMBJ
            // 
            this.chk_FMBJ.AutoSize = true;
            this.chk_FMBJ.Location = new System.Drawing.Point(12, 500);
            this.chk_FMBJ.Name = "chk_FMBJ";
            this.chk_FMBJ.Size = new System.Drawing.Size(99, 20);
            this.chk_FMBJ.TabIndex = 5;
            this.chk_FMBJ.Text = "封面&&脊背";
            this.chk_FMBJ.UseVisualStyleBackColor = true;
            this.chk_FMBJ.CheckedChanged += new System.EventHandler(this.Chk_FMBJ_CheckedChanged);
            // 
            // chk_JNML
            // 
            this.chk_JNML.AutoSize = true;
            this.chk_JNML.Location = new System.Drawing.Point(298, 454);
            this.chk_JNML.Name = "chk_JNML";
            this.chk_JNML.Size = new System.Drawing.Size(123, 20);
            this.chk_JNML.TabIndex = 6;
            this.chk_JNML.Text = "卷内文件目录";
            this.chk_JNML.UseVisualStyleBackColor = true;
            this.chk_JNML.CheckedChanged += new System.EventHandler(this.Chk_JNML_CheckedChanged);
            // 
            // cbo_BJ
            // 
            this.cbo_BJ.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo_BJ.FormattingEnabled = true;
            this.cbo_BJ.Items.AddRange(new object[] {
            "20mm",
            "40mm",
            "60mm",
            "80mm"});
            this.cbo_BJ.Location = new System.Drawing.Point(150, 498);
            this.cbo_BJ.Name = "cbo_BJ";
            this.cbo_BJ.Size = new System.Drawing.Size(121, 24);
            this.cbo_BJ.TabIndex = 7;
            this.cbo_BJ.SelectedIndexChanged += new System.EventHandler(this.Cbo_BJ_SelectedIndexChanged);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tip});
            this.statusStrip1.Location = new System.Drawing.Point(0, 557);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(824, 22);
            this.statusStrip1.SizingGrip = false;
            this.statusStrip1.TabIndex = 8;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tip
            // 
            this.tip.Name = "tip";
            this.tip.Size = new System.Drawing.Size(0, 17);
            // 
            // btn_StartPrint
            // 
            this.btn_StartPrint.Image = ((System.Drawing.Image)(resources.GetObject("btn_StartPrint.Image")));
            this.btn_StartPrint.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btn_StartPrint.Location = new System.Drawing.Point(729, 495);
            this.btn_StartPrint.Name = "btn_StartPrint";
            this.btn_StartPrint.Size = new System.Drawing.Size(83, 30);
            this.btn_StartPrint.TabIndex = 1;
            this.btn_StartPrint.Text = "开始打印";
            this.btn_StartPrint.Click += new System.EventHandler(this.Btn_StartPrint_Click);
            // 
            // print
            // 
            this.print.FillWeight = 10F;
            this.print.HeaderText = "打印";
            this.print.Name = "print";
            this.print.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // id
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.id.DefaultCellStyle = dataGridViewCellStyle2;
            this.id.FillWeight = 10F;
            this.id.HeaderText = "盒号";
            this.id.Name = "id";
            this.id.ReadOnly = true;
            this.id.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // amount
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.amount.DefaultCellStyle = dataGridViewCellStyle3;
            this.amount.FillWeight = 13F;
            this.amount.HeaderText = "文件数";
            this.amount.Name = "amount";
            this.amount.ReadOnly = true;
            // 
            // bkb
            // 
            this.bkb.FillWeight = 25F;
            this.bkb.HeaderText = "卷内备考表";
            this.bkb.Name = "bkb";
            this.bkb.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // fm
            // 
            this.fm.FillWeight = 25F;
            this.fm.HeaderText = "封面&脊背";
            this.fm.Name = "fm";
            // 
            // fmbj
            // 
            this.fmbj.FillWeight = 20F;
            this.fmbj.HeaderText = "边距";
            this.fmbj.Items.AddRange(new object[] {
            "20mm",
            "40mm",
            "60mm",
            "80mm"});
            this.fmbj.Name = "fmbj";
            // 
            // jnml
            // 
            this.jnml.FillWeight = 25F;
            this.jnml.HeaderText = "卷内文件目录";
            this.jnml.Name = "jnml";
            this.jnml.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // Frm_PrintBox
            // 
            this.Appearance.Options.UseFont = true;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(824, 579);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.cbo_BJ);
            this.Controls.Add(this.chk_JNML);
            this.Controls.Add(this.chk_FMBJ);
            this.Controls.Add(this.chk_BKB);
            this.Controls.Add(this.chk_PrintAll);
            this.Controls.Add(this.view);
            this.Controls.Add(this.btn_StartPrint);
            this.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Frm_PrintBox";
            this.Text = "案卷盒打印";
            this.Load += new System.EventHandler(this.Frm_PrintBox_Load);
            ((System.ComponentModel.ISupportInitialize)(this.view)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private KyoControl.KyoButton btn_StartPrint;
        private System.Windows.Forms.DataGridView view;
        private System.Windows.Forms.CheckBox chk_PrintAll;
        private System.Windows.Forms.CheckBox chk_BKB;
        private System.Windows.Forms.CheckBox chk_FMBJ;
        private System.Windows.Forms.CheckBox chk_JNML;
        private System.Windows.Forms.ComboBox cbo_BJ;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel tip;
        private System.Windows.Forms.DataGridViewCheckBoxColumn print;
        private System.Windows.Forms.DataGridViewTextBoxColumn id;
        private System.Windows.Forms.DataGridViewTextBoxColumn amount;
        private System.Windows.Forms.DataGridViewCheckBoxColumn bkb;
        private System.Windows.Forms.DataGridViewCheckBoxColumn fm;
        private System.Windows.Forms.DataGridViewComboBoxColumn fmbj;
        private System.Windows.Forms.DataGridViewCheckBoxColumn jnml;
    }
}