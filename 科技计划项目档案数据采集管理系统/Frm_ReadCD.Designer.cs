namespace 科技计划项目档案数据采集管理系统
{
    partial class Frm_ReadCD
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
            this.label2 = new System.Windows.Forms.Label();
            this.txt_CdPath = new System.Windows.Forms.TextBox();
            this.txt_YsjPath = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btn_ReadCD = new System.Windows.Forms.Button();
            this.btn_ReadYSJ = new System.Windows.Forms.Button();
            this.pgb_GP = new System.Windows.Forms.ProgressBar();
            this.pgb_YSJ = new System.Windows.Forms.ProgressBar();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btn_Back = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(11, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(125, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "光盘数据存放路径:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(27, 104);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(111, 17);
            this.label2.TabIndex = 1;
            this.label2.Text = "元数据存放路径:";
            // 
            // txt_CdPath
            // 
            this.txt_CdPath.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_CdPath.ForeColor = System.Drawing.SystemColors.GrayText;
            this.txt_CdPath.Location = new System.Drawing.Point(151, 20);
            this.txt_CdPath.Name = "txt_CdPath";
            this.txt_CdPath.Size = new System.Drawing.Size(211, 24);
            this.txt_CdPath.TabIndex = 2;
            this.txt_CdPath.Text = "点击选取光盘存放路径";
            // 
            // txt_YsjPath
            // 
            this.txt_YsjPath.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_YsjPath.ForeColor = System.Drawing.SystemColors.GrayText;
            this.txt_YsjPath.Location = new System.Drawing.Point(151, 101);
            this.txt_YsjPath.Name = "txt_YsjPath";
            this.txt_YsjPath.Size = new System.Drawing.Size(211, 24);
            this.txt_YsjPath.TabIndex = 3;
            this.txt_YsjPath.Text = "点击选取元数据文件存放路径";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(43, 63);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(97, 17);
            this.label3.TabIndex = 4;
            this.label3.Text = "光盘读写进度:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(27, 145);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(111, 17);
            this.label4.TabIndex = 5;
            this.label4.Text = "元数据解析进度:";
            // 
            // btn_ReadCD
            // 
            this.btn_ReadCD.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_ReadCD.Location = new System.Drawing.Point(373, 17);
            this.btn_ReadCD.Name = "btn_ReadCD";
            this.btn_ReadCD.Size = new System.Drawing.Size(75, 28);
            this.btn_ReadCD.TabIndex = 6;
            this.btn_ReadCD.Text = "读取(&R)";
            this.btn_ReadCD.UseVisualStyleBackColor = true;
            this.btn_ReadCD.Click += new System.EventHandler(this.btn_ReadCD_Click);
            // 
            // btn_ReadYSJ
            // 
            this.btn_ReadYSJ.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_ReadYSJ.Location = new System.Drawing.Point(373, 100);
            this.btn_ReadYSJ.Name = "btn_ReadYSJ";
            this.btn_ReadYSJ.Size = new System.Drawing.Size(75, 28);
            this.btn_ReadYSJ.TabIndex = 7;
            this.btn_ReadYSJ.Text = "读取(&R)";
            this.btn_ReadYSJ.UseVisualStyleBackColor = true;
            this.btn_ReadYSJ.Click += new System.EventHandler(this.btn_ReadYSJ_Click);
            // 
            // pgb_GP
            // 
            this.pgb_GP.Location = new System.Drawing.Point(151, 65);
            this.pgb_GP.Name = "pgb_GP";
            this.pgb_GP.Size = new System.Drawing.Size(297, 15);
            this.pgb_GP.TabIndex = 8;
            // 
            // pgb_YSJ
            // 
            this.pgb_YSJ.Location = new System.Drawing.Point(151, 147);
            this.pgb_YSJ.Name = "pgb_YSJ";
            this.pgb_YSJ.Size = new System.Drawing.Size(297, 15);
            this.pgb_YSJ.TabIndex = 9;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.pgb_YSJ);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.pgb_GP);
            this.groupBox1.Controls.Add(this.txt_CdPath);
            this.groupBox1.Controls.Add(this.btn_ReadYSJ);
            this.groupBox1.Controls.Add(this.txt_YsjPath);
            this.groupBox1.Controls.Add(this.btn_ReadCD);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Location = new System.Drawing.Point(5, -6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(462, 174);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btn_Back);
            this.groupBox2.Location = new System.Drawing.Point(5, 162);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(462, 60);
            this.groupBox2.TabIndex = 11;
            this.groupBox2.TabStop = false;
            // 
            // btn_Back
            // 
            this.btn_Back.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Back.BackColor = System.Drawing.Color.Transparent;
            this.btn_Back.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.btn_Back.Image = global::科技计划项目档案数据采集管理系统.Properties.Resources.save;
            this.btn_Back.Location = new System.Drawing.Point(378, 21);
            this.btn_Back.Name = "btn_Back";
            this.btn_Back.Size = new System.Drawing.Size(68, 29);
            this.btn_Back.TabIndex = 5;
            this.btn_Back.Click += new System.EventHandler(this.btn_Back_Click);
            // 
            // Frm_ReadCD
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(474, 228);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Frm_ReadCD";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "读取光盘";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txt_CdPath;
        private System.Windows.Forms.TextBox txt_YsjPath;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btn_ReadCD;
        private System.Windows.Forms.Button btn_ReadYSJ;
        private System.Windows.Forms.ProgressBar pgb_GP;
        private System.Windows.Forms.ProgressBar pgb_YSJ;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label btn_Back;
    }
}