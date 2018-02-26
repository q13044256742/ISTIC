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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pal_LeftMenu = new System.Windows.Forms.Panel();
            this.dgv_Plan = new System.Windows.Forms.DataGridView();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.plan = new System.Windows.Forms.TabPage();
            this.project = new System.Windows.Forms.TabPage();
            this.dgv_Project = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Plan)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.plan.SuspendLayout();
            this.project.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Project)).BeginInit();
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
            // dgv_Plan
            // 
            this.dgv_Plan.AllowUserToDeleteRows = false;
            this.dgv_Plan.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv_Plan.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgv_Plan.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgv_Plan.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_Plan.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_Plan.Location = new System.Drawing.Point(3, 3);
            this.dgv_Plan.Name = "dgv_Plan";
            this.dgv_Plan.ReadOnly = true;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_Plan.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgv_Plan.RowTemplate.Height = 23;
            this.dgv_Plan.Size = new System.Drawing.Size(744, 447);
            this.dgv_Plan.TabIndex = 2;
            this.dgv_Plan.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Dgv_Plan_CellClick);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.plan);
            this.tabControl1.Controls.Add(this.project);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tabControl1.Location = new System.Drawing.Point(237, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(758, 487);
            this.tabControl1.TabIndex = 3;
            // 
            // plan
            // 
            this.plan.Controls.Add(this.dgv_Plan);
            this.plan.Location = new System.Drawing.Point(4, 30);
            this.plan.Name = "plan";
            this.plan.Padding = new System.Windows.Forms.Padding(3);
            this.plan.Size = new System.Drawing.Size(750, 453);
            this.plan.TabIndex = 0;
            this.plan.Text = "计划/专项";
            this.plan.UseVisualStyleBackColor = true;
            // 
            // project
            // 
            this.project.Controls.Add(this.dgv_Project);
            this.project.Location = new System.Drawing.Point(4, 30);
            this.project.Name = "project";
            this.project.Padding = new System.Windows.Forms.Padding(3);
            this.project.Size = new System.Drawing.Size(750, 453);
            this.project.TabIndex = 1;
            this.project.Text = "项目/课题";
            this.project.UseVisualStyleBackColor = true;
            // 
            // dgv_Project
            // 
            this.dgv_Project.AllowUserToAddRows = false;
            this.dgv_Project.AllowUserToDeleteRows = false;
            this.dgv_Project.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv_Project.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgv_Project.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgv_Project.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_Project.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_Project.Location = new System.Drawing.Point(3, 3);
            this.dgv_Project.Name = "dgv_Project";
            this.dgv_Project.ReadOnly = true;
            this.dgv_Project.RowTemplate.Height = 23;
            this.dgv_Project.Size = new System.Drawing.Size(744, 447);
            this.dgv_Project.TabIndex = 0;
            this.dgv_Project.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Dgv_Project_CellClick);
            // 
            // Frm_QT
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(995, 487);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.pal_LeftMenu);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Frm_QT";
            this.Text = "档案质检";
            this.Load += new System.EventHandler(this.Frm_QT_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Plan)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.plan.ResumeLayout(false);
            this.project.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Project)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pal_LeftMenu;
        private System.Windows.Forms.DataGridView dgv_Plan;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage plan;
        private System.Windows.Forms.TabPage project;
        private System.Windows.Forms.DataGridView dgv_Project;
    }
}