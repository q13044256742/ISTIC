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
            this.tab_MenuList = new System.Windows.Forms.TabControl();
            this.plan = new System.Windows.Forms.TabPage();
            this.project = new System.Windows.Forms.TabPage();
            this.subject = new System.Windows.Forms.TabPage();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dgv_Plan = new System.Windows.Forms.DataGridView();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.button2 = new System.Windows.Forms.Button();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.dataGridView3 = new System.Windows.Forms.DataGridView();
            this.panel3 = new System.Windows.Forms.Panel();
            this.button3 = new System.Windows.Forms.Button();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.tab_MenuList.SuspendLayout();
            this.plan.SuspendLayout();
            this.project.SuspendLayout();
            this.subject.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Plan)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView3)).BeginInit();
            this.panel3.SuspendLayout();
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
            // tab_MenuList
            // 
            this.tab_MenuList.Controls.Add(this.plan);
            this.tab_MenuList.Controls.Add(this.project);
            this.tab_MenuList.Controls.Add(this.subject);
            this.tab_MenuList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tab_MenuList.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tab_MenuList.Location = new System.Drawing.Point(237, 0);
            this.tab_MenuList.Name = "tab_MenuList";
            this.tab_MenuList.SelectedIndex = 0;
            this.tab_MenuList.Size = new System.Drawing.Size(758, 487);
            this.tab_MenuList.TabIndex = 1;
            this.tab_MenuList.SelectedIndexChanged += new System.EventHandler(this.tab_MenuList_SelectedIndexChanged);
            // 
            // plan
            // 
            this.plan.Controls.Add(this.dgv_Plan);
            this.plan.Controls.Add(this.panel1);
            this.plan.Location = new System.Drawing.Point(4, 26);
            this.plan.Name = "plan";
            this.plan.Padding = new System.Windows.Forms.Padding(3);
            this.plan.Size = new System.Drawing.Size(750, 457);
            this.plan.TabIndex = 0;
            this.plan.Text = "计划";
            this.plan.UseVisualStyleBackColor = true;
            // 
            // project
            // 
            this.project.Controls.Add(this.dataGridView2);
            this.project.Controls.Add(this.panel2);
            this.project.Location = new System.Drawing.Point(4, 26);
            this.project.Name = "project";
            this.project.Padding = new System.Windows.Forms.Padding(3);
            this.project.Size = new System.Drawing.Size(750, 457);
            this.project.TabIndex = 1;
            this.project.Text = "专项";
            this.project.UseVisualStyleBackColor = true;
            // 
            // subject
            // 
            this.subject.Controls.Add(this.dataGridView3);
            this.subject.Controls.Add(this.panel3);
            this.subject.Location = new System.Drawing.Point(4, 26);
            this.subject.Name = "subject";
            this.subject.Size = new System.Drawing.Size(750, 457);
            this.subject.TabIndex = 2;
            this.subject.Text = "项目/课题";
            this.subject.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.textBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(744, 40);
            this.panel1.TabIndex = 0;
            // 
            // dgv_Plan
            // 
            this.dgv_Plan.AllowUserToAddRows = false;
            this.dgv_Plan.AllowUserToDeleteRows = false;
            this.dgv_Plan.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv_Plan.BackgroundColor = System.Drawing.SystemColors.ButtonFace;
            this.dgv_Plan.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgv_Plan.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_Plan.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_Plan.Location = new System.Drawing.Point(3, 43);
            this.dgv_Plan.Name = "dgv_Plan";
            this.dgv_Plan.ReadOnly = true;
            this.dgv_Plan.RowTemplate.Height = 23;
            this.dgv_Plan.Size = new System.Drawing.Size(744, 411);
            this.dgv_Plan.TabIndex = 1;
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.Location = new System.Drawing.Point(439, 8);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(193, 26);
            this.textBox1.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(647, 8);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(78, 26);
            this.button1.TabIndex = 1;
            this.button1.Text = "查询(&F)";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // dataGridView2
            // 
            this.dataGridView2.BackgroundColor = System.Drawing.SystemColors.ButtonFace;
            this.dataGridView2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView2.Location = new System.Drawing.Point(3, 43);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.RowTemplate.Height = 23;
            this.dataGridView2.Size = new System.Drawing.Size(744, 411);
            this.dataGridView2.TabIndex = 3;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.button2);
            this.panel2.Controls.Add(this.textBox2);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(3, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(744, 40);
            this.panel2.TabIndex = 2;
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.Location = new System.Drawing.Point(647, 8);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(78, 26);
            this.button2.TabIndex = 1;
            this.button2.Text = "查询(&F)";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // textBox2
            // 
            this.textBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox2.Location = new System.Drawing.Point(439, 8);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(193, 26);
            this.textBox2.TabIndex = 0;
            // 
            // dataGridView3
            // 
            this.dataGridView3.BackgroundColor = System.Drawing.SystemColors.ButtonFace;
            this.dataGridView3.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridView3.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView3.Location = new System.Drawing.Point(0, 40);
            this.dataGridView3.Name = "dataGridView3";
            this.dataGridView3.RowTemplate.Height = 23;
            this.dataGridView3.Size = new System.Drawing.Size(750, 417);
            this.dataGridView3.TabIndex = 3;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.button3);
            this.panel3.Controls.Add(this.textBox3);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(750, 40);
            this.panel3.TabIndex = 2;
            // 
            // button3
            // 
            this.button3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button3.Location = new System.Drawing.Point(653, 8);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(78, 26);
            this.button3.TabIndex = 1;
            this.button3.Text = "查询(&F)";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // textBox3
            // 
            this.textBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox3.Location = new System.Drawing.Point(445, 8);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(193, 26);
            this.textBox3.TabIndex = 0;
            // 
            // Frm_QT
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(995, 487);
            this.Controls.Add(this.tab_MenuList);
            this.Controls.Add(this.pal_LeftMenu);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Frm_QT";
            this.Text = "档案质检";
            this.Load += new System.EventHandler(this.Frm_QT_Load);
            this.tab_MenuList.ResumeLayout(false);
            this.plan.ResumeLayout(false);
            this.project.ResumeLayout(false);
            this.subject.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Plan)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView3)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pal_LeftMenu;
        private System.Windows.Forms.TabControl tab_MenuList;
        private System.Windows.Forms.TabPage plan;
        private System.Windows.Forms.TabPage project;
        private System.Windows.Forms.TabPage subject;
        private System.Windows.Forms.DataGridView dgv_Plan;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.DataGridView dataGridView3;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.TextBox textBox3;
    }
}