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
            this.temp_richTextBox = new System.Windows.Forms.RichTextBox();
            this.列表.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // 列表
            // 
            this.列表.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.列表.Controls.Add(this.temp_richTextBox);
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
            // temp_richTextBox
            // 
            this.temp_richTextBox.Location = new System.Drawing.Point(30, 38);
            this.temp_richTextBox.Name = "temp_richTextBox";
            this.temp_richTextBox.Size = new System.Drawing.Size(903, 307);
            this.temp_richTextBox.TabIndex = 0;
            this.temp_richTextBox.Text = "";
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
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox 列表;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button Btn_save_temp;
        private System.Windows.Forms.Button Btn_edit_temp;
        private System.Windows.Forms.RichTextBox temp_richTextBox;
    }
}