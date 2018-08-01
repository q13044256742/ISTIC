namespace 科技计划项目档案数据采集管理系统
{
    partial class Frm_DomNeed
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
            this.txt_Head = new System.Windows.Forms.Label();
            this.lbl_Code = new System.Windows.Forms.Label();
            this.lbl_Recever = new System.Windows.Forms.Label();
            this.lbl_ReceDate = new System.Windows.Forms.Label();
            this.lbl_Body = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txt_Head
            // 
            this.txt_Head.AutoSize = true;
            this.txt_Head.Font = new System.Drawing.Font("微软雅黑", 18F, System.Drawing.FontStyle.Bold);
            this.txt_Head.Location = new System.Drawing.Point(364, 22);
            this.txt_Head.Name = "txt_Head";
            this.txt_Head.Size = new System.Drawing.Size(134, 31);
            this.txt_Head.TabIndex = 0;
            this.txt_Head.Text = "档案催报单";
            // 
            // lbl_Code
            // 
            this.lbl_Code.AutoSize = true;
            this.lbl_Code.Font = new System.Drawing.Font("华文中宋", 13F);
            this.lbl_Code.Location = new System.Drawing.Point(674, 81);
            this.lbl_Code.Name = "lbl_Code";
            this.lbl_Code.Size = new System.Drawing.Size(63, 20);
            this.lbl_Code.TabIndex = 1;
            this.lbl_Code.Text = "编号：";
            // 
            // lbl_Recever
            // 
            this.lbl_Recever.AutoSize = true;
            this.lbl_Recever.Font = new System.Drawing.Font("华文中宋", 13F);
            this.lbl_Recever.Location = new System.Drawing.Point(656, 352);
            this.lbl_Recever.Name = "lbl_Recever";
            this.lbl_Recever.Size = new System.Drawing.Size(81, 20);
            this.lbl_Recever.TabIndex = 3;
            this.lbl_Recever.Text = "接收人：";
            // 
            // lbl_ReceDate
            // 
            this.lbl_ReceDate.AutoSize = true;
            this.lbl_ReceDate.Font = new System.Drawing.Font("华文中宋", 13F);
            this.lbl_ReceDate.Location = new System.Drawing.Point(638, 392);
            this.lbl_ReceDate.Name = "lbl_ReceDate";
            this.lbl_ReceDate.Size = new System.Drawing.Size(99, 20);
            this.lbl_ReceDate.TabIndex = 4;
            this.lbl_ReceDate.Text = "确认日期：";
            // 
            // lbl_Body
            // 
            this.lbl_Body.Font = new System.Drawing.Font("华文中宋", 13F);
            this.lbl_Body.Location = new System.Drawing.Point(28, 116);
            this.lbl_Body.Name = "lbl_Body";
            this.lbl_Body.Size = new System.Drawing.Size(806, 204);
            this.lbl_Body.TabIndex = 5;
            // 
            // Frm_DomNeed
            // 
            this.Appearance.Options.UseFont = true;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(862, 438);
            this.Controls.Add(this.lbl_Body);
            this.Controls.Add(this.lbl_ReceDate);
            this.Controls.Add(this.lbl_Recever);
            this.Controls.Add(this.lbl_Code);
            this.Controls.Add(this.txt_Head);
            this.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Frm_DomNeed";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.Frm_DomRec_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label txt_Head;
        private System.Windows.Forms.Label lbl_Code;
        private System.Windows.Forms.Label lbl_Recever;
        private System.Windows.Forms.Label lbl_ReceDate;
        private System.Windows.Forms.Label lbl_Body;
    }
}