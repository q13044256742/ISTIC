namespace 科技计划项目档案数据采集管理系统
{
    partial class Frm_ProTypeSelect
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_ProTypeSelect));
            this.label1 = new System.Windows.Forms.Label();
            this.cbo_TypeSelect = new System.Windows.Forms.ComboBox();
            this.btn_Sure = new 科技计划项目档案数据采集管理系统.KyoControl.KyoButton();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(25, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 19);
            this.label1.TabIndex = 0;
            this.label1.Text = "计划类别:";
            // 
            // cbo_TypeSelect
            // 
            this.cbo_TypeSelect.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo_TypeSelect.FormattingEnabled = true;
            this.cbo_TypeSelect.Location = new System.Drawing.Point(106, 21);
            this.cbo_TypeSelect.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cbo_TypeSelect.Name = "cbo_TypeSelect";
            this.cbo_TypeSelect.Size = new System.Drawing.Size(234, 27);
            this.cbo_TypeSelect.TabIndex = 1;
            // 
            // btn_Sure
            // 
            this.btn_Sure.Appearance.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.btn_Sure.Appearance.Options.UseFont = true;
            this.btn_Sure.Image = ((System.Drawing.Image)(resources.GetObject("btn_Sure.Image")));
            this.btn_Sure.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btn_Sure.ImageToTextIndent = 5;
            this.btn_Sure.Location = new System.Drawing.Point(151, 80);
            this.btn_Sure.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btn_Sure.Name = "btn_Sure";
            this.btn_Sure.Size = new System.Drawing.Size(67, 30);
            this.btn_Sure.TabIndex = 2;
            this.btn_Sure.Text = "确定";
            this.btn_Sure.Click += new System.EventHandler(this.Btn_Sure_Click);
            // 
            // Frm_ProTypeSelect
            // 
            this.Appearance.Options.UseFont = true;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(369, 126);
            this.Controls.Add(this.btn_Sure);
            this.Controls.Add(this.cbo_TypeSelect);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Frm_ProTypeSelect";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "计划类型选择";
            this.Load += new System.EventHandler(this.Frm_ProTypeSelect_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbo_TypeSelect;
        private KyoControl.KyoButton btn_Sure;
    }
}