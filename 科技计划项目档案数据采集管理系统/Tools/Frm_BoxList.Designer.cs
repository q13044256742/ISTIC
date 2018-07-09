namespace 科技计划项目档案数据采集管理系统
{
    partial class Frm_BoxList
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_BoxList));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pal_Show = new System.Windows.Forms.Panel();
            this.dgv_DataList = new System.Windows.Forms.DataGridView();
            this.lbl_GC = new System.Windows.Forms.Label();
            this.lbl_Code = new System.Windows.Forms.Label();
            this.lbl_Name = new System.Windows.Forms.Label();
            this.lbl_proCode = new System.Windows.Forms.Label();
            this.panel6 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.pageSetupDialog1 = new System.Windows.Forms.PageSetupDialog();
            this.printDocument1 = new System.Drawing.Printing.PrintDocument();
            this.printPreviewDialog1 = new System.Windows.Forms.PrintPreviewDialog();
            this.fontDialog1 = new System.Windows.Forms.FontDialog();
            this.btn_Font = new 科技计划项目档案数据采集管理系统.KyoControl.KyoButton();
            this.btn_PrintSetup = new 科技计划项目档案数据采集管理系统.KyoControl.KyoButton();
            this.btn_Print = new 科技计划项目档案数据采集管理系统.KyoControl.KyoButton();
            this.fb_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fb_code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fb_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fb_page = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fb_count = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fb_remark = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1.SuspendLayout();
            this.pal_Show.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_DataList)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.pal_Show);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Font = new System.Drawing.Font("宋体", 12F);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(763, 729);
            this.panel1.TabIndex = 0;
            // 
            // pal_Show
            // 
            this.pal_Show.BackColor = System.Drawing.Color.White;
            this.pal_Show.Controls.Add(this.dgv_DataList);
            this.pal_Show.Controls.Add(this.lbl_GC);
            this.pal_Show.Controls.Add(this.lbl_Code);
            this.pal_Show.Controls.Add(this.lbl_Name);
            this.pal_Show.Controls.Add(this.lbl_proCode);
            this.pal_Show.Controls.Add(this.panel6);
            this.pal_Show.Controls.Add(this.panel5);
            this.pal_Show.Controls.Add(this.panel4);
            this.pal_Show.Controls.Add(this.panel3);
            this.pal_Show.Controls.Add(this.label6);
            this.pal_Show.Controls.Add(this.label5);
            this.pal_Show.Controls.Add(this.label4);
            this.pal_Show.Controls.Add(this.label3);
            this.pal_Show.Controls.Add(this.label2);
            this.pal_Show.Font = new System.Drawing.Font("宋体", 12F);
            this.pal_Show.Location = new System.Drawing.Point(21, 10);
            this.pal_Show.Name = "pal_Show";
            this.pal_Show.Size = new System.Drawing.Size(717, 675);
            this.pal_Show.TabIndex = 1;
            // 
            // dgv_DataList
            // 
            this.dgv_DataList.AllowUserToAddRows = false;
            this.dgv_DataList.AllowUserToDeleteRows = false;
            this.dgv_DataList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv_DataList.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders;
            this.dgv_DataList.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 12F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.Padding = new System.Windows.Forms.Padding(5);
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgv_DataList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgv_DataList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_DataList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.fb_id,
            this.fb_code,
            this.fb_name,
            this.fb_page,
            this.fb_count,
            this.fb_remark});
            this.dgv_DataList.Location = new System.Drawing.Point(3, 271);
            this.dgv_DataList.Name = "dgv_DataList";
            this.dgv_DataList.ReadOnly = true;
            this.dgv_DataList.RowHeadersVisible = false;
            this.dgv_DataList.RowHeadersWidth = 48;
            this.dgv_DataList.RowTemplate.Height = 23;
            this.dgv_DataList.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.dgv_DataList.Size = new System.Drawing.Size(711, 277);
            this.dgv_DataList.TabIndex = 13;
            // 
            // lbl_GC
            // 
            this.lbl_GC.Location = new System.Drawing.Point(105, 202);
            this.lbl_GC.Name = "lbl_GC";
            this.lbl_GC.Size = new System.Drawing.Size(569, 34);
            this.lbl_GC.TabIndex = 12;
            this.lbl_GC.Text = "null";
            this.lbl_GC.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // lbl_Code
            // 
            this.lbl_Code.Location = new System.Drawing.Point(105, 161);
            this.lbl_Code.Name = "lbl_Code";
            this.lbl_Code.Size = new System.Drawing.Size(569, 34);
            this.lbl_Code.TabIndex = 11;
            this.lbl_Code.Text = "null";
            this.lbl_Code.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // lbl_Name
            // 
            this.lbl_Name.Location = new System.Drawing.Point(105, 121);
            this.lbl_Name.Name = "lbl_Name";
            this.lbl_Name.Size = new System.Drawing.Size(569, 34);
            this.lbl_Name.TabIndex = 10;
            this.lbl_Name.Text = "null";
            this.lbl_Name.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // lbl_proCode
            // 
            this.lbl_proCode.Location = new System.Drawing.Point(105, 79);
            this.lbl_proCode.Name = "lbl_proCode";
            this.lbl_proCode.Size = new System.Drawing.Size(569, 34);
            this.lbl_proCode.TabIndex = 9;
            this.lbl_proCode.Text = "null";
            this.lbl_proCode.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // panel6
            // 
            this.panel6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel6.Location = new System.Drawing.Point(96, 239);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(583, 1);
            this.panel6.TabIndex = 8;
            // 
            // panel5
            // 
            this.panel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel5.Location = new System.Drawing.Point(96, 198);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(583, 1);
            this.panel5.TabIndex = 7;
            // 
            // panel4
            // 
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Location = new System.Drawing.Point(96, 158);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(583, 1);
            this.panel4.TabIndex = 6;
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Location = new System.Drawing.Point(96, 116);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(583, 1);
            this.panel3.TabIndex = 5;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("新宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(25, 218);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(76, 16);
            this.label6.TabIndex = 4;
            this.label6.Text = "馆藏号：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("新宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(9, 177);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(93, 16);
            this.label5.TabIndex = 3;
            this.label5.Text = "案卷编号：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("新宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(9, 136);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(93, 16);
            this.label4.TabIndex = 2;
            this.label4.Text = "案卷名称：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("新宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(9, 95);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(93, 16);
            this.label3.TabIndex = 1;
            this.label3.Text = "项目编号：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("等线", 22F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(261, 34);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(194, 31);
            this.label2.TabIndex = 0;
            this.label2.Text = "卷内文件目录";
            // 
            // pageSetupDialog1
            // 
            this.pageSetupDialog1.Document = this.printDocument1;
            // 
            // printDocument1
            // 
            this.printDocument1.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.PrintDocument1_PrintPage);
            // 
            // printPreviewDialog1
            // 
            this.printPreviewDialog1.AutoScrollMargin = new System.Drawing.Size(0, 0);
            this.printPreviewDialog1.AutoScrollMinSize = new System.Drawing.Size(0, 0);
            this.printPreviewDialog1.ClientSize = new System.Drawing.Size(400, 300);
            this.printPreviewDialog1.Document = this.printDocument1;
            this.printPreviewDialog1.Enabled = true;
            this.printPreviewDialog1.Icon = ((System.Drawing.Icon)(resources.GetObject("printPreviewDialog1.Icon")));
            this.printPreviewDialog1.Name = "printPreviewDialog1";
            this.printPreviewDialog1.Visible = false;
            // 
            // btn_Font
            // 
            this.btn_Font.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btn_Font.Image = ((System.Drawing.Image)(resources.GetObject("btn_Font.Image")));
            this.btn_Font.Location = new System.Drawing.Point(254, 693);
            this.btn_Font.Name = "btn_Font";
            this.btn_Font.Size = new System.Drawing.Size(83, 31);
            this.btn_Font.TabIndex = 7;
            this.btn_Font.Text = "字体设置";
            this.btn_Font.Click += new System.EventHandler(this.btn_Font_Click);
            // 
            // btn_PrintSetup
            // 
            this.btn_PrintSetup.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btn_PrintSetup.Image = ((System.Drawing.Image)(resources.GetObject("btn_PrintSetup.Image")));
            this.btn_PrintSetup.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btn_PrintSetup.Location = new System.Drawing.Point(340, 693);
            this.btn_PrintSetup.Name = "btn_PrintSetup";
            this.btn_PrintSetup.Size = new System.Drawing.Size(83, 31);
            this.btn_PrintSetup.TabIndex = 4;
            this.btn_PrintSetup.Text = "打印设置";
            this.btn_PrintSetup.Click += new System.EventHandler(this.btn_PrintSetup_Click);
            // 
            // btn_Print
            // 
            this.btn_Print.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btn_Print.Image = ((System.Drawing.Image)(resources.GetObject("btn_Print.Image")));
            this.btn_Print.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btn_Print.Location = new System.Drawing.Point(426, 693);
            this.btn_Print.Name = "btn_Print";
            this.btn_Print.Size = new System.Drawing.Size(83, 31);
            this.btn_Print.TabIndex = 3;
            this.btn_Print.Text = "打印";
            this.btn_Print.Click += new System.EventHandler(this.Btn_Print_Click);
            // 
            // fb_id
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.fb_id.DefaultCellStyle = dataGridViewCellStyle2;
            this.fb_id.FillWeight = 50F;
            this.fb_id.HeaderText = "序号";
            this.fb_id.Name = "fb_id";
            this.fb_id.ReadOnly = true;
            this.fb_id.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // fb_code
            // 
            this.fb_code.FillWeight = 120F;
            this.fb_code.HeaderText = "文件编号";
            this.fb_code.Name = "fb_code";
            this.fb_code.ReadOnly = true;
            this.fb_code.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // fb_name
            // 
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.fb_name.DefaultCellStyle = dataGridViewCellStyle3;
            this.fb_name.FillWeight = 200F;
            this.fb_name.HeaderText = "文件名称";
            this.fb_name.Name = "fb_name";
            this.fb_name.ReadOnly = true;
            this.fb_name.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // fb_page
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.fb_page.DefaultCellStyle = dataGridViewCellStyle4;
            this.fb_page.FillWeight = 50F;
            this.fb_page.HeaderText = "页数";
            this.fb_page.Name = "fb_page";
            this.fb_page.ReadOnly = true;
            this.fb_page.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // fb_count
            // 
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.fb_count.DefaultCellStyle = dataGridViewCellStyle5;
            this.fb_count.FillWeight = 50F;
            this.fb_count.HeaderText = "份数";
            this.fb_count.Name = "fb_count";
            this.fb_count.ReadOnly = true;
            this.fb_count.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // fb_remark
            // 
            this.fb_remark.FillWeight = 60F;
            this.fb_remark.HeaderText = "备注";
            this.fb_remark.Name = "fb_remark";
            this.fb_remark.ReadOnly = true;
            this.fb_remark.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Frm_BoxList
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(763, 729);
            this.Controls.Add(this.btn_Font);
            this.Controls.Add(this.btn_PrintSetup);
            this.Controls.Add(this.btn_Print);
            this.Controls.Add(this.panel1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Frm_BoxList";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.Frm_BoxList_Load);
            this.panel1.ResumeLayout(false);
            this.pal_Show.ResumeLayout(false);
            this.pal_Show.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_DataList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private KyoControl.KyoButton btn_Print;
        private System.Windows.Forms.Panel pal_Show;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label lbl_GC;
        private System.Windows.Forms.Label lbl_Code;
        private System.Windows.Forms.Label lbl_Name;
        private System.Windows.Forms.Label lbl_proCode;
        private KyoControl.KyoButton btn_PrintSetup;
        private System.Windows.Forms.PageSetupDialog pageSetupDialog1;
        private System.Drawing.Printing.PrintDocument printDocument1;
        private System.Windows.Forms.PrintPreviewDialog printPreviewDialog1;
        private System.Windows.Forms.DataGridView dgv_DataList;
        private KyoControl.KyoButton btn_Font;
        private System.Windows.Forms.FontDialog fontDialog1;
        private System.Windows.Forms.DataGridViewTextBoxColumn fb_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn fb_code;
        private System.Windows.Forms.DataGridViewTextBoxColumn fb_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn fb_page;
        private System.Windows.Forms.DataGridViewTextBoxColumn fb_count;
        private System.Windows.Forms.DataGridViewTextBoxColumn fb_remark;
    }
}