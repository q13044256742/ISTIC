using System;
using System.Windows.Forms;

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
            this.dgv_Imp = new System.Windows.Forms.DataGridView();
            this.tab_Menulist = new System.Windows.Forms.TabControl();
            this.imp = new System.Windows.Forms.TabPage();
            this.imp_dev = new System.Windows.Forms.TabPage();
            this.dgv_Imp_Dev = new System.Windows.Forms.DataGridView();
            this.project = new System.Windows.Forms.TabPage();
            this.dgv_Project = new System.Windows.Forms.DataGridView();
            this.dgv_MyReg = new System.Windows.Forms.DataGridView();
            this.ace_LeftMenu = new 科技计划项目档案数据采集管理系统.KyoControl.KyoAccordion();
            this.acg_Worked = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.ace_Login = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.ace_MyLog = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.pal_LeftMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Imp)).BeginInit();
            this.tab_Menulist.SuspendLayout();
            this.imp.SuspendLayout();
            this.imp_dev.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Imp_Dev)).BeginInit();
            this.project.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Project)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_MyReg)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ace_LeftMenu)).BeginInit();
            this.SuspendLayout();
            // 
            // pal_LeftMenu
            // 
            this.pal_LeftMenu.Controls.Add(this.ace_LeftMenu);
            this.pal_LeftMenu.Dock = System.Windows.Forms.DockStyle.Left;
            this.pal_LeftMenu.Location = new System.Drawing.Point(0, 0);
            this.pal_LeftMenu.Name = "pal_LeftMenu";
            this.pal_LeftMenu.Size = new System.Drawing.Size(237, 487);
            this.pal_LeftMenu.TabIndex = 0;
            // 
            // dgv_Imp
            // 
            this.dgv_Imp.AllowUserToAddRows = false;
            this.dgv_Imp.AllowUserToDeleteRows = false;
            this.dgv_Imp.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv_Imp.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgv_Imp.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_Imp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_Imp.Location = new System.Drawing.Point(3, 3);
            this.dgv_Imp.Name = "dgv_Imp";
            this.dgv_Imp.ReadOnly = true;
            this.dgv_Imp.RowTemplate.Height = 23;
            this.dgv_Imp.Size = new System.Drawing.Size(744, 447);
            this.dgv_Imp.TabIndex = 2;
            this.dgv_Imp.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Dgv_Imp_CellClick);
            // 
            // tab_Menulist
            // 
            this.tab_Menulist.Controls.Add(this.imp);
            this.tab_Menulist.Controls.Add(this.imp_dev);
            this.tab_Menulist.Controls.Add(this.project);
            this.tab_Menulist.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tab_Menulist.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tab_Menulist.Location = new System.Drawing.Point(237, 0);
            this.tab_Menulist.Name = "tab_Menulist";
            this.tab_Menulist.SelectedIndex = 0;
            this.tab_Menulist.Size = new System.Drawing.Size(758, 487);
            this.tab_Menulist.TabIndex = 3;
            this.tab_Menulist.SelectedIndexChanged += new System.EventHandler(this.Tab_Menulist_SelectedIndexChanged);
            // 
            // imp
            // 
            this.imp.Controls.Add(this.dgv_Imp);
            this.imp.Location = new System.Drawing.Point(4, 30);
            this.imp.Name = "imp";
            this.imp.Padding = new System.Windows.Forms.Padding(3);
            this.imp.Size = new System.Drawing.Size(750, 453);
            this.imp.TabIndex = 0;
            this.imp.Text = "计划";
            this.imp.UseVisualStyleBackColor = true;
            // 
            // imp_dev
            // 
            this.imp_dev.Controls.Add(this.dgv_Imp_Dev);
            this.imp_dev.Location = new System.Drawing.Point(4, 30);
            this.imp_dev.Name = "imp_dev";
            this.imp_dev.Padding = new System.Windows.Forms.Padding(3);
            this.imp_dev.Size = new System.Drawing.Size(750, 453);
            this.imp_dev.TabIndex = 1;
            this.imp_dev.Text = "专项";
            this.imp_dev.UseVisualStyleBackColor = true;
            // 
            // dgv_Imp_Dev
            // 
            this.dgv_Imp_Dev.AllowUserToAddRows = false;
            this.dgv_Imp_Dev.AllowUserToDeleteRows = false;
            this.dgv_Imp_Dev.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv_Imp_Dev.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgv_Imp_Dev.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_Imp_Dev.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_Imp_Dev.Location = new System.Drawing.Point(3, 3);
            this.dgv_Imp_Dev.Name = "dgv_Imp_Dev";
            this.dgv_Imp_Dev.ReadOnly = true;
            this.dgv_Imp_Dev.RowTemplate.Height = 23;
            this.dgv_Imp_Dev.Size = new System.Drawing.Size(744, 447);
            this.dgv_Imp_Dev.TabIndex = 0;
            this.dgv_Imp_Dev.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Dgv_Imp_Dev_CellClick);
            // 
            // project
            // 
            this.project.Controls.Add(this.dgv_Project);
            this.project.Location = new System.Drawing.Point(4, 30);
            this.project.Name = "project";
            this.project.Padding = new System.Windows.Forms.Padding(3);
            this.project.Size = new System.Drawing.Size(750, 453);
            this.project.TabIndex = 2;
            this.project.Text = "项目/课题";
            this.project.UseVisualStyleBackColor = true;
            // 
            // dgv_Project
            // 
            this.dgv_Project.AllowUserToAddRows = false;
            this.dgv_Project.AllowUserToDeleteRows = false;
            this.dgv_Project.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv_Project.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgv_Project.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_Project.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_Project.Location = new System.Drawing.Point(3, 3);
            this.dgv_Project.Name = "dgv_Project";
            this.dgv_Project.ReadOnly = true;
            this.dgv_Project.RowTemplate.Height = 23;
            this.dgv_Project.Size = new System.Drawing.Size(744, 447);
            this.dgv_Project.TabIndex = 0;
            this.dgv_Project.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Dgv_Project_CellClick);
            // 
            // dgv_MyReg
            // 
            this.dgv_MyReg.AllowUserToDeleteRows = false;
            this.dgv_MyReg.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv_MyReg.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_MyReg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_MyReg.Location = new System.Drawing.Point(237, 0);
            this.dgv_MyReg.Name = "dgv_MyReg";
            this.dgv_MyReg.ReadOnly = true;
            this.dgv_MyReg.RowTemplate.Height = 23;
            this.dgv_MyReg.Size = new System.Drawing.Size(758, 487);
            this.dgv_MyReg.TabIndex = 2;
            this.dgv_MyReg.Visible = false;
            this.dgv_MyReg.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Dgv_MyReg_CellClick);
            // 
            // ace_LeftMenu
            // 
            this.ace_LeftMenu.Dock = System.Windows.Forms.DockStyle.Left;
            this.ace_LeftMenu.Elements.AddRange(new DevExpress.XtraBars.Navigation.AccordionControlElement[] {
            this.acg_Worked});
            this.ace_LeftMenu.Location = new System.Drawing.Point(0, 0);
            this.ace_LeftMenu.Name = "ace_LeftMenu";
            this.ace_LeftMenu.ScrollBarMode = DevExpress.XtraBars.Navigation.ScrollBarMode.Hidden;
            this.ace_LeftMenu.Size = new System.Drawing.Size(227, 487);
            this.ace_LeftMenu.TabIndex = 17;
            // 
            // acg_Worked
            // 
            this.acg_Worked.Elements.AddRange(new DevExpress.XtraBars.Navigation.AccordionControlElement[] {
            this.ace_Login,
            this.ace_MyLog});
            this.acg_Worked.Expanded = true;
            this.acg_Worked.Height = 50;
            this.acg_Worked.Image = global::科技计划项目档案数据采集管理系统.Properties.Resources.pic2;
            this.acg_Worked.Name = "acg_Worked";
            this.acg_Worked.Text = "档案质检";
            this.acg_Worked.TextToImageDistance = 10;
            // 
            // ace_Login
            // 
            this.ace_Login.Height = 35;
            this.ace_Login.Image = global::科技计划项目档案数据采集管理系统.Properties.Resources.pic8;
            this.ace_Login.ImageLayoutMode = DevExpress.XtraBars.Navigation.ImageLayoutMode.Squeeze;
            this.ace_Login.Name = "ace_Login";
            this.ace_Login.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.ace_Login.Text = "质检登记";
            this.ace_Login.TextToImageDistance = 15;
            this.ace_Login.Click += new System.EventHandler(this.Sub_Click);
            // 
            // ace_MyLog
            // 
            this.ace_MyLog.Height = 35;
            this.ace_MyLog.Image = global::科技计划项目档案数据采集管理系统.Properties.Resources.pic7;
            this.ace_MyLog.ImageLayoutMode = DevExpress.XtraBars.Navigation.ImageLayoutMode.Squeeze;
            this.ace_MyLog.Name = "ace_MyLog";
            this.ace_MyLog.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.ace_MyLog.Text = "我的质检";
            this.ace_MyLog.TextToImageDistance = 15;
            this.ace_MyLog.Click += new System.EventHandler(this.Sub_Click);
            // 
            // Frm_QT
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(995, 487);
            this.Controls.Add(this.tab_Menulist);
            this.Controls.Add(this.dgv_MyReg);
            this.Controls.Add(this.pal_LeftMenu);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Frm_QT";
            this.Text = "档案质检";
            this.Load += new System.EventHandler(this.Frm_QT_Load);
            this.pal_LeftMenu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Imp)).EndInit();
            this.tab_Menulist.ResumeLayout(false);
            this.imp.ResumeLayout(false);
            this.imp_dev.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Imp_Dev)).EndInit();
            this.project.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Project)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_MyReg)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ace_LeftMenu)).EndInit();
            this.ResumeLayout(false);

        }
        #endregion

        private System.Windows.Forms.Panel pal_LeftMenu;
        private System.Windows.Forms.DataGridView dgv_Imp;
        private System.Windows.Forms.TabControl tab_Menulist;
        private System.Windows.Forms.DataGridView dgv_MyReg;
        private System.Windows.Forms.TabPage imp;
        private System.Windows.Forms.TabPage imp_dev;
        private System.Windows.Forms.DataGridView dgv_Imp_Dev;
        private System.Windows.Forms.TabPage project;
        private System.Windows.Forms.DataGridView dgv_Project;
        private KyoControl.KyoAccordion ace_LeftMenu;
        private DevExpress.XtraBars.Navigation.AccordionControlElement acg_Worked;
        private DevExpress.XtraBars.Navigation.AccordionControlElement ace_Login;
        private DevExpress.XtraBars.Navigation.AccordionControlElement ace_MyLog;
    }
}