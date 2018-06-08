namespace 科技计划项目档案数据采集管理系统.TransferOfRegistration
{
    partial class Frm_AddCD
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_AddCD));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txt_CDName = new System.Windows.Forms.TextBox();
            this.txt_CDCode = new System.Windows.Forms.TextBox();
            this.txt_CDRemark = new System.Windows.Forms.TextBox();
            this.btn_Save = new 科技计划项目档案数据采集管理系统.KyoControl.KyoButton();
            this.label4 = new System.Windows.Forms.Label();
            this.lbl_PCName = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(13, 57);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 19);
            this.label1.TabIndex = 0;
            this.label1.Text = "光盘名称";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(13, 96);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 19);
            this.label2.TabIndex = 1;
            this.label2.Text = "光盘编号";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold);
            this.label3.Location = new System.Drawing.Point(37, 136);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 19);
            this.label3.TabIndex = 2;
            this.label3.Text = "备注";
            // 
            // txt_CDName
            // 
            this.txt_CDName.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.txt_CDName.Location = new System.Drawing.Point(91, 53);
            this.txt_CDName.Margin = new System.Windows.Forms.Padding(4);
            this.txt_CDName.Name = "txt_CDName";
            this.txt_CDName.Size = new System.Drawing.Size(361, 27);
            this.txt_CDName.TabIndex = 3;
            // 
            // txt_CDCode
            // 
            this.txt_CDCode.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.txt_CDCode.Location = new System.Drawing.Point(91, 92);
            this.txt_CDCode.Margin = new System.Windows.Forms.Padding(4);
            this.txt_CDCode.Name = "txt_CDCode";
            this.txt_CDCode.Size = new System.Drawing.Size(361, 27);
            this.txt_CDCode.TabIndex = 4;
            this.txt_CDCode.Enter += new System.EventHandler(this.Txt_CDCode_Enter);
            // 
            // txt_CDRemark
            // 
            this.txt_CDRemark.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.txt_CDRemark.Location = new System.Drawing.Point(91, 136);
            this.txt_CDRemark.Margin = new System.Windows.Forms.Padding(4);
            this.txt_CDRemark.Multiline = true;
            this.txt_CDRemark.Name = "txt_CDRemark";
            this.txt_CDRemark.Size = new System.Drawing.Size(361, 111);
            this.txt_CDRemark.TabIndex = 5;
            // 
            // btn_Save
            // 
            this.btn_Save.Appearance.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.btn_Save.Appearance.Options.UseFont = true;
            this.btn_Save.Image = ((System.Drawing.Image)(resources.GetObject("btn_Save.Image")));
            this.btn_Save.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btn_Save.ImageToTextIndent = 5;
            this.btn_Save.Location = new System.Drawing.Point(205, 277);
            this.btn_Save.Name = "btn_Save";
            this.btn_Save.Size = new System.Drawing.Size(70, 30);
            this.btn_Save.TabIndex = 6;
            this.btn_Save.Text = "保存";
            this.btn_Save.Click += new System.EventHandler(this.Btn_Save_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold);
            this.label4.Location = new System.Drawing.Point(13, 19);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 19);
            this.label4.TabIndex = 7;
            this.label4.Text = "批次名称";
            // 
            // lbl_PCName
            // 
            this.lbl_PCName.AutoSize = true;
            this.lbl_PCName.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.lbl_PCName.Location = new System.Drawing.Point(88, 18);
            this.lbl_PCName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_PCName.Name = "lbl_PCName";
            this.lbl_PCName.Size = new System.Drawing.Size(39, 20);
            this.lbl_PCName.TabIndex = 8;
            this.lbl_PCName.Text = "XXX";
            // 
            // Frm_AddCD
            // 
            this.Appearance.Options.UseFont = true;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(480, 325);
            this.Controls.Add(this.lbl_PCName);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btn_Save);
            this.Controls.Add(this.txt_CDRemark);
            this.Controls.Add(this.txt_CDCode);
            this.Controls.Add(this.txt_CDName);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Frm_AddCD";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "新增光盘信息";
            this.Load += new System.EventHandler(this.Frm_AddCD_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txt_CDName;
        private System.Windows.Forms.TextBox txt_CDCode;
        private System.Windows.Forms.TextBox txt_CDRemark;
        private KyoControl.KyoButton btn_Save;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lbl_PCName;
    }
}