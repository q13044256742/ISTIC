namespace 科技计划项目档案数据采集管理系统
{
    partial class Frm_QT
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
            this.pal_LeftMenu = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dgv_Plan = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Plan)).BeginInit();
            this.SuspendLayout();
            // 
            // pal_LeftMenu
            // 
            this.pal_LeftMenu.Dock = System.Windows.Forms.DockStyle.Left;
            this.pal_LeftMenu.Location = new System.Drawing.Point(0, 0);
            this.pal_LeftMenu.Name = "pal_LeftMenu";
            this.pal_LeftMenu.Size = new System.Drawing.Size(237, 487);
            this.pal_LeftMenu.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(237, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(758, 54);
            this.panel1.TabIndex = 1;
            // 
            // dgv_Plan
            // 
            this.dgv_Plan.AllowUserToDeleteRows = false;
            this.dgv_Plan.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv_Plan.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_Plan.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_Plan.Location = new System.Drawing.Point(237, 54);
            this.dgv_Plan.Name = "dgv_Plan";
            this.dgv_Plan.ReadOnly = true;
            this.dgv_Plan.RowTemplate.Height = 23;
            this.dgv_Plan.Size = new System.Drawing.Size(758, 433);
            this.dgv_Plan.TabIndex = 2;
            this.dgv_Plan.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Dgv_Plan_CellClick);
            // 
            // Frm_QT
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(995, 487);
            this.Controls.Add(this.dgv_Plan);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pal_LeftMenu);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Frm_QT";
            this.Text = "档案质检";
            this.Load += new System.EventHandler(this.Frm_QT_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Plan)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pal_LeftMenu;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView dgv_Plan;
    }
}