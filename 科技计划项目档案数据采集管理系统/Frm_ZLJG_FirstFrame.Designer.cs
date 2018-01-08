namespace 科技计划项目档案数据采集管理系统
{
    partial class Frm_ZLJG_FirstFrame
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dgv_JGDJ = new System.Windows.Forms.DataGridView();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btn_Back = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.btn_Find = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_JGDJ)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dgv_JGDJ);
            this.groupBox1.Location = new System.Drawing.Point(0, 31);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(818, 352);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            // 
            // dgv_JGDJ
            // 
            this.dgv_JGDJ.AllowUserToAddRows = false;
            this.dgv_JGDJ.AllowUserToDeleteRows = false;
            this.dgv_JGDJ.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv_JGDJ.BackgroundColor = System.Drawing.Color.White;
            this.dgv_JGDJ.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_JGDJ.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_JGDJ.Location = new System.Drawing.Point(3, 19);
            this.dgv_JGDJ.Name = "dgv_JGDJ";
            this.dgv_JGDJ.ReadOnly = true;
            this.dgv_JGDJ.RowTemplate.Height = 23;
            this.dgv_JGDJ.Size = new System.Drawing.Size(812, 330);
            this.dgv_JGDJ.TabIndex = 0;
            this.dgv_JGDJ.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_JGDJ_CellClick);
            // 
            // groupBox2
            // 
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox2.Location = new System.Drawing.Point(0, 381);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(818, 41);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.BackgroundImage = global::科技计划项目档案数据采集管理系统.Properties.Resources.dhbg;
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel1.Controls.Add(this.btn_Back);
            this.panel1.Controls.Add(this.textBox1);
            this.panel1.Controls.Add(this.btn_Find);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(818, 44);
            this.panel1.TabIndex = 1;
            // 
            // btn_Back
            // 
            this.btn_Back.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Back.BackColor = System.Drawing.Color.Transparent;
            this.btn_Back.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.btn_Back.Image = global::科技计划项目档案数据采集管理系统.Properties.Resources.back;
            this.btn_Back.Location = new System.Drawing.Point(741, 8);
            this.btn_Back.Name = "btn_Back";
            this.btn_Back.Size = new System.Drawing.Size(70, 29);
            this.btn_Back.TabIndex = 4;
            this.btn_Back.Click += new System.EventHandler(this.btn_Back_Click);
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.Location = new System.Drawing.Point(502, 10);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(158, 27);
            this.textBox1.TabIndex = 3;
            // 
            // btn_Find
            // 
            this.btn_Find.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Find.BackColor = System.Drawing.Color.Transparent;
            this.btn_Find.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.btn_Find.Image = global::科技计划项目档案数据采集管理系统.Properties.Resources.chaxun;
            this.btn_Find.Location = new System.Drawing.Point(666, 8);
            this.btn_Find.Name = "btn_Find";
            this.btn_Find.Size = new System.Drawing.Size(70, 29);
            this.btn_Find.TabIndex = 2;
            // 
            // Frm_ZLJG_FirstFrame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(818, 422);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Frm_ZLJG_FirstFrame";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "加工登记";
            this.Load += new System.EventHandler(this.Frm_ZLJG_FirstFrame_Load);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_JGDJ)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label btn_Find;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView dgv_JGDJ;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label btn_Back;
    }
}