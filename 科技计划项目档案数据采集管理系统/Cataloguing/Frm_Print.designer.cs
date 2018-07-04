namespace 科技计划项目档案数据采集管理系统.Cataloguing
{
    partial class Frm_Print
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
            this.lbl_BKB = new System.Windows.Forms.LinkLabel();
            this.lbl_FM = new System.Windows.Forms.LinkLabel();
            this.lbl_WJ = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();
            // 
            // lbl_BKB
            // 
            this.lbl_BKB.AutoSize = true;
            this.lbl_BKB.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbl_BKB.Location = new System.Drawing.Point(60, 25);
            this.lbl_BKB.Name = "lbl_BKB";
            this.lbl_BKB.Size = new System.Drawing.Size(98, 14);
            this.lbl_BKB.TabIndex = 6;
            this.lbl_BKB.TabStop = true;
            this.lbl_BKB.Text = "1、卷内备考表";
            this.lbl_BKB.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lbl_BKB_LinkClicked);
            // 
            // lbl_FM
            // 
            this.lbl_FM.AutoSize = true;
            this.lbl_FM.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbl_FM.Location = new System.Drawing.Point(60, 67);
            this.lbl_FM.Name = "lbl_FM";
            this.lbl_FM.Size = new System.Drawing.Size(91, 14);
            this.lbl_FM.TabIndex = 7;
            this.lbl_FM.TabStop = true;
            this.lbl_FM.Text = "2、封面&&脊背";
            this.lbl_FM.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lbl_FM_LinkClicked);
            // 
            // lbl_WJ
            // 
            this.lbl_WJ.AutoSize = true;
            this.lbl_WJ.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbl_WJ.Location = new System.Drawing.Point(60, 111);
            this.lbl_WJ.Name = "lbl_WJ";
            this.lbl_WJ.Size = new System.Drawing.Size(112, 14);
            this.lbl_WJ.TabIndex = 8;
            this.lbl_WJ.TabStop = true;
            this.lbl_WJ.Text = "3、卷内文件目录";
            this.lbl_WJ.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lbl_WJ_LinkClicked);
            // 
            // Frm_Print
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(233, 158);
            this.Controls.Add(this.lbl_WJ);
            this.Controls.Add(this.lbl_FM);
            this.Controls.Add(this.lbl_BKB);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Frm_Print";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "打印类型";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.LinkLabel lbl_BKB;
        private System.Windows.Forms.LinkLabel lbl_FM;
        private System.Windows.Forms.LinkLabel lbl_WJ;
    }
}