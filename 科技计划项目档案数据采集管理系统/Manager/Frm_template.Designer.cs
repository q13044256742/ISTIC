namespace 科技计划项目档案数据采集管理系统.Manager
{
    partial class Frm_template
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
            this.列表 = new System.Windows.Forms.GroupBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.Btn_save_temp = new System.Windows.Forms.Button();
            this.Btn_edit_temp = new System.Windows.Forms.Button();
            this.temp_content = new System.Windows.Forms.RichTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.temp_title = new System.Windows.Forms.RichTextBox();
            this.列表.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // 列表
            // 
            this.列表.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.列表.Controls.Add(this.temp_title);
            this.列表.Controls.Add(this.label6);
            this.列表.Controls.Add(this.label5);
            this.列表.Controls.Add(this.label4);
            this.列表.Controls.Add(this.label3);
            this.列表.Controls.Add(this.label1);
            this.列表.Controls.Add(this.label2);
            this.列表.Controls.Add(this.temp_content);
            this.列表.Location = new System.Drawing.Point(12, 69);
            this.列表.Name = "列表";
            this.列表.Size = new System.Drawing.Size(977, 414);
            this.列表.TabIndex = 26;
            this.列表.TabStop = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.Btn_save_temp);
            this.groupBox1.Controls.Add(this.Btn_edit_temp);
            this.groupBox1.Location = new System.Drawing.Point(12, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(974, 45);
            this.groupBox1.TabIndex = 25;
            this.groupBox1.TabStop = false;
            // 
            // Btn_save_temp
            // 
            this.Btn_save_temp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Btn_save_temp.AutoSize = true;
            this.Btn_save_temp.Location = new System.Drawing.Point(886, 15);
            this.Btn_save_temp.Name = "Btn_save_temp";
            this.Btn_save_temp.Size = new System.Drawing.Size(68, 27);
            this.Btn_save_temp.TabIndex = 13;
            this.Btn_save_temp.Text = "保存";
            this.Btn_save_temp.UseVisualStyleBackColor = true;
            // 
            // Btn_edit_temp
            // 
            this.Btn_edit_temp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Btn_edit_temp.AutoSize = true;
            this.Btn_edit_temp.Location = new System.Drawing.Point(811, 15);
            this.Btn_edit_temp.Name = "Btn_edit_temp";
            this.Btn_edit_temp.Size = new System.Drawing.Size(68, 27);
            this.Btn_edit_temp.TabIndex = 12;
            this.Btn_edit_temp.Text = "编辑";
            this.Btn_edit_temp.UseVisualStyleBackColor = true;
            // 
            // temp_content
            // 
            this.temp_content.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.temp_content.Location = new System.Drawing.Point(18, 94);
            this.temp_content.Name = "temp_content";
            this.temp_content.Size = new System.Drawing.Size(883, 209);
            this.temp_content.TabIndex = 0;
            this.temp_content.Text = "";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(665, 328);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 19);
            this.label2.TabIndex = 8;
            this.label2.Text = "接收人：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(645, 368);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(109, 19);
            this.label1.TabIndex = 9;
            this.label1.Text = "确认日期：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(680, 69);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(69, 19);
            this.label3.TabIndex = 10;
            this.label3.Text = "编号：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(743, 328);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(185, 19);
            this.label4.TabIndex = 11;
            this.label4.Text = "________________";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(745, 368);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(185, 19);
            this.label5.TabIndex = 12;
            this.label5.Text = "________________";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(747, 69);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(185, 19);
            this.label6.TabIndex = 13;
            this.label6.Text = "________________";
            // 
            // temp_title
            // 
            this.temp_title.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.temp_title.Location = new System.Drawing.Point(318, 20);
            this.temp_title.Name = "temp_title";
            this.temp_title.Size = new System.Drawing.Size(263, 39);
            this.temp_title.TabIndex = 14;
            this.temp_title.Text = "";
            // 
            // Frm_template
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1001, 491);
            this.Controls.Add(this.列表);
            this.Controls.Add(this.groupBox1);
            this.Name = "Frm_template";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "模板管理";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.列表.ResumeLayout(false);
            this.列表.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox 列表;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button Btn_save_temp;
        private System.Windows.Forms.Button Btn_edit_temp;
        private System.Windows.Forms.RichTextBox temp_content;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.RichTextBox temp_title;
    }
}