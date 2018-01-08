namespace 科技计划项目档案数据采集管理系统.TransferOfRegistration
{
    partial class Frm_ToR
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
            this.pal_LeftMenu = new 科技计划项目档案数据采集管理系统.Tools.MyPanel();
            this.pal_YJDJ = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.pal_XTSY = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.pal_LeftMenu.SuspendLayout();
            this.pal_YJDJ.SuspendLayout();
            this.pal_XTSY.SuspendLayout();
            this.SuspendLayout();
            // 
            // pal_LeftMenu
            // 
            this.pal_LeftMenu.BackgroundImage = global::科技计划项目档案数据采集管理系统.Properties.Resources.top;
            this.pal_LeftMenu.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pal_LeftMenu.Controls.Add(this.pal_YJDJ);
            this.pal_LeftMenu.Controls.Add(this.pal_XTSY);
            this.pal_LeftMenu.Dock = System.Windows.Forms.DockStyle.Left;
            this.pal_LeftMenu.Location = new System.Drawing.Point(0, 0);
            this.pal_LeftMenu.Name = "pal_LeftMenu";
            this.pal_LeftMenu.Size = new System.Drawing.Size(254, 491);
            this.pal_LeftMenu.TabIndex = 6;
            // 
            // pal_YJDJ
            // 
            this.pal_YJDJ.BackColor = System.Drawing.Color.Transparent;
            this.pal_YJDJ.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pal_YJDJ.Controls.Add(this.label5);
            this.pal_YJDJ.Dock = System.Windows.Forms.DockStyle.Top;
            this.pal_YJDJ.Location = new System.Drawing.Point(0, 53);
            this.pal_YJDJ.Name = "pal_YJDJ";
            this.pal_YJDJ.Size = new System.Drawing.Size(254, 53);
            this.pal_YJDJ.TabIndex = 3;
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Tahoma", 11F);
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Image = global::科技计划项目档案数据采集管理系统.Properties.Resources.pic2;
            this.label5.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label5.Location = new System.Drawing.Point(62, 8);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(107, 36);
            this.label5.TabIndex = 0;
            this.label5.Text = "移交登记";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pal_XTSY
            // 
            this.pal_XTSY.BackColor = System.Drawing.Color.Transparent;
            this.pal_XTSY.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pal_XTSY.Controls.Add(this.label4);
            this.pal_XTSY.Dock = System.Windows.Forms.DockStyle.Top;
            this.pal_XTSY.Location = new System.Drawing.Point(0, 0);
            this.pal_XTSY.Name = "pal_XTSY";
            this.pal_XTSY.Size = new System.Drawing.Size(254, 53);
            this.pal_XTSY.TabIndex = 2;
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Tahoma", 11F);
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Image = global::科技计划项目档案数据采集管理系统.Properties.Resources.pic1;
            this.label4.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label4.Location = new System.Drawing.Point(62, 8);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(107, 36);
            this.label4.TabIndex = 0;
            this.label4.Text = "系统首页";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // Frm_ToR
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(954, 491);
            this.Controls.Add(this.pal_LeftMenu);
            this.Name = "Frm_ToR";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.pal_LeftMenu.ResumeLayout(false);
            this.pal_YJDJ.ResumeLayout(false);
            this.pal_XTSY.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Tools.MyPanel pal_LeftMenu;
        private System.Windows.Forms.Panel pal_YJDJ;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel pal_XTSY;
        private System.Windows.Forms.Label label4;
    }
}