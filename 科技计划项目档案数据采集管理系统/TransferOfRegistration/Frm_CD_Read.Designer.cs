namespace 科技计划项目档案数据采集管理系统.TransferOfRegistration
{
    partial class Frm_CD_Read
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
            this.label1 = new System.Windows.Forms.Label();
            this.txt_CD_Path = new System.Windows.Forms.TextBox();
            this.btn_CD_Choose = new System.Windows.Forms.Button();
            this.btn_DS_Choose = new System.Windows.Forms.Button();
            this.txt_DS_Path = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btn_Sure = new System.Windows.Forms.Button();
            this.pgb_DS = new 科技计划项目档案数据采集管理系统.KyoProgressBar();
            this.pgb_CD = new 科技计划项目档案数据采集管理系统.KyoProgressBar();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(39, 28);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(121, 19);
            this.label1.TabIndex = 0;
            this.label1.Text = "光盘数据存放路径";
            // 
            // txt_CD_Path
            // 
            this.txt_CD_Path.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txt_CD_Path.Location = new System.Drawing.Point(39, 53);
            this.txt_CD_Path.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txt_CD_Path.Name = "txt_CD_Path";
            this.txt_CD_Path.ReadOnly = true;
            this.txt_CD_Path.Size = new System.Drawing.Size(317, 23);
            this.txt_CD_Path.TabIndex = 1;
            // 
            // btn_CD_Choose
            // 
            this.btn_CD_Choose.Location = new System.Drawing.Point(362, 50);
            this.btn_CD_Choose.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btn_CD_Choose.Name = "btn_CD_Choose";
            this.btn_CD_Choose.Size = new System.Drawing.Size(80, 29);
            this.btn_CD_Choose.TabIndex = 2;
            this.btn_CD_Choose.Text = "选择";
            this.btn_CD_Choose.UseVisualStyleBackColor = true;
            this.btn_CD_Choose.Click += new System.EventHandler(this.btn_CD_Choose_Click);
            // 
            // btn_DS_Choose
            // 
            this.btn_DS_Choose.Location = new System.Drawing.Point(362, 116);
            this.btn_DS_Choose.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btn_DS_Choose.Name = "btn_DS_Choose";
            this.btn_DS_Choose.Size = new System.Drawing.Size(80, 29);
            this.btn_DS_Choose.TabIndex = 5;
            this.btn_DS_Choose.Text = "选择";
            this.btn_DS_Choose.UseVisualStyleBackColor = true;
            this.btn_DS_Choose.Click += new System.EventHandler(this.btn_DS_Choose_Click);
            // 
            // txt_DS_Path
            // 
            this.txt_DS_Path.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txt_DS_Path.Location = new System.Drawing.Point(39, 119);
            this.txt_DS_Path.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txt_DS_Path.Name = "txt_DS_Path";
            this.txt_DS_Path.ReadOnly = true;
            this.txt_DS_Path.Size = new System.Drawing.Size(317, 23);
            this.txt_DS_Path.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(39, 94);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(107, 19);
            this.label2.TabIndex = 3;
            this.label2.Text = "元数据存放路径";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(39, 162);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(93, 19);
            this.label3.TabIndex = 6;
            this.label3.Text = "光盘读写进度";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(39, 224);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(107, 19);
            this.label4.TabIndex = 8;
            this.label4.Text = "元数据解析进度";
            // 
            // btn_Sure
            // 
            this.btn_Sure.Location = new System.Drawing.Point(331, 307);
            this.btn_Sure.Name = "btn_Sure";
            this.btn_Sure.Size = new System.Drawing.Size(111, 31);
            this.btn_Sure.TabIndex = 10;
            this.btn_Sure.Text = "开始读写";
            this.btn_Sure.UseVisualStyleBackColor = true;
            this.btn_Sure.Click += new System.EventHandler(this.Btn_Sure_Click);
            // 
            // pgb_DS
            // 
            this.pgb_DS.Location = new System.Drawing.Point(39, 249);
            this.pgb_DS.Name = "pgb_DS";
            this.pgb_DS.Size = new System.Drawing.Size(403, 23);
            this.pgb_DS.TabIndex = 9;
            this.pgb_DS.TextColor = System.Drawing.Color.Black;
            this.pgb_DS.TextFont = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            // 
            // pgb_CD
            // 
            this.pgb_CD.Location = new System.Drawing.Point(39, 187);
            this.pgb_CD.Name = "pgb_CD";
            this.pgb_CD.Size = new System.Drawing.Size(403, 23);
            this.pgb_CD.TabIndex = 7;
            this.pgb_CD.TextColor = System.Drawing.Color.Black;
            this.pgb_CD.TextFont = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            // 
            // Frm_CD_Read
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(485, 359);
            this.Controls.Add(this.btn_Sure);
            this.Controls.Add(this.pgb_DS);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.pgb_CD);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btn_DS_Choose);
            this.Controls.Add(this.txt_DS_Path);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btn_CD_Choose);
            this.Controls.Add(this.txt_CD_Path);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Frm_CD_Read";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "光盘读写";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_CD_Path;
        private System.Windows.Forms.Button btn_CD_Choose;
        private System.Windows.Forms.Button btn_DS_Choose;
        private System.Windows.Forms.TextBox txt_DS_Path;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btn_Sure;
        private KyoProgressBar pgb_CD;
        private KyoProgressBar pgb_DS;
    }
}