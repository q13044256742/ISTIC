namespace 科技计划项目档案数据采集管理系统
{
    partial class Frm_DomAccept
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_DomAccept));
            this.dgv_DataShow = new System.Windows.Forms.DataGridView();
            this.pal_LeftMenu = new System.Windows.Forms.Panel();
            this.ac_LeftMenu = new 科技计划项目档案数据采集管理系统.KyoControl.KyoAccordion();
            this.acg_Register = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.ace_all = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_DataShow)).BeginInit();
            this.pal_LeftMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ac_LeftMenu)).BeginInit();
            this.SuspendLayout();
            // 
            // dgv_DataShow
            // 
            this.dgv_DataShow.AllowUserToAddRows = false;
            this.dgv_DataShow.AllowUserToDeleteRows = false;
            this.dgv_DataShow.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv_DataShow.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgv_DataShow.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgv_DataShow.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgv_DataShow.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_DataShow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_DataShow.Location = new System.Drawing.Point(273, 0);
            this.dgv_DataShow.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dgv_DataShow.Name = "dgv_DataShow";
            this.dgv_DataShow.ReadOnly = true;
            this.dgv_DataShow.RowTemplate.Height = 23;
            this.dgv_DataShow.Size = new System.Drawing.Size(640, 484);
            this.dgv_DataShow.TabIndex = 0;
            this.dgv_DataShow.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_DataShow_CellContentClick);
            // 
            // pal_LeftMenu
            // 
            this.pal_LeftMenu.AutoScroll = true;
            this.pal_LeftMenu.Controls.Add(this.ac_LeftMenu);
            this.pal_LeftMenu.Dock = System.Windows.Forms.DockStyle.Left;
            this.pal_LeftMenu.Location = new System.Drawing.Point(0, 0);
            this.pal_LeftMenu.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pal_LeftMenu.Name = "pal_LeftMenu";
            this.pal_LeftMenu.Size = new System.Drawing.Size(273, 484);
            this.pal_LeftMenu.TabIndex = 13;
            // 
            // ac_LeftMenu
            // 
            this.ac_LeftMenu.AllowItemSelection = true;
            this.ac_LeftMenu.Appearance.Group.Hovered.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.ac_LeftMenu.Appearance.Group.Hovered.Options.UseFont = true;
            this.ac_LeftMenu.Appearance.Group.Normal.Font = new System.Drawing.Font("微软雅黑", 11F, System.Drawing.FontStyle.Bold);
            this.ac_LeftMenu.Appearance.Group.Normal.Options.UseFont = true;
            this.ac_LeftMenu.Appearance.Group.Pressed.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.ac_LeftMenu.Appearance.Group.Pressed.Options.UseFont = true;
            this.ac_LeftMenu.Appearance.Item.Hovered.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.ac_LeftMenu.Appearance.Item.Hovered.Options.UseFont = true;
            this.ac_LeftMenu.Appearance.Item.Normal.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.ac_LeftMenu.Appearance.Item.Normal.Options.UseFont = true;
            this.ac_LeftMenu.Appearance.Item.Pressed.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.ac_LeftMenu.Appearance.Item.Pressed.Options.UseFont = true;
            this.ac_LeftMenu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ac_LeftMenu.Elements.AddRange(new DevExpress.XtraBars.Navigation.AccordionControlElement[] {
            this.acg_Register});
            this.ac_LeftMenu.Location = new System.Drawing.Point(0, 0);
            this.ac_LeftMenu.LookAndFeel.SkinName = "McSkin";
            this.ac_LeftMenu.LookAndFeel.UseDefaultLookAndFeel = false;
            this.ac_LeftMenu.Name = "ac_LeftMenu";
            this.ac_LeftMenu.ScrollBarMode = DevExpress.XtraBars.Navigation.ScrollBarMode.Auto;
            this.ac_LeftMenu.ShowToolTips = false;
            this.ac_LeftMenu.Size = new System.Drawing.Size(273, 484);
            this.ac_LeftMenu.TabIndex = 12;
            // 
            // acg_Register
            // 
            this.acg_Register.Elements.AddRange(new DevExpress.XtraBars.Navigation.AccordionControlElement[] {
            this.ace_all});
            this.acg_Register.Expanded = true;
            this.acg_Register.Image = ((System.Drawing.Image)(resources.GetObject("acg_Register.Image")));
            this.acg_Register.Name = "acg_Register";
            this.acg_Register.Text = "移交登记";
            this.acg_Register.TextToImageDistance = 10;
            // 
            // ace_all
            // 
            this.ace_all.Name = "ace_all";
            this.ace_all.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.ace_all.Text = "全部来源单位";
            // 
            // Frm_DomAccept
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(913, 484);
            this.Controls.Add(this.dgv_DataShow);
            this.Controls.Add(this.pal_LeftMenu);
            this.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Frm_DomAccept";
            this.Text = "档案接收";
            this.Load += new System.EventHandler(this.Frm_DomAccept_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_DataShow)).EndInit();
            this.pal_LeftMenu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ac_LeftMenu)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgv_DataShow;
        private System.Windows.Forms.Panel pal_LeftMenu;
        private KyoControl.KyoAccordion ac_LeftMenu;
        private DevExpress.XtraBars.Navigation.AccordionControlElement acg_Register;
        private DevExpress.XtraBars.Navigation.AccordionControlElement ace_all;
    }
}