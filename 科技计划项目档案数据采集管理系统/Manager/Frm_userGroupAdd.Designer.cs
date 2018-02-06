namespace 科技计划项目档案数据采集管理系统.Manager
{
    partial class Frm_userGroupAdd
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
            this.ug_code = new System.Windows.Forms.TextBox();
            this.ug_name = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.btn_Save = new System.Windows.Forms.Button();
            this.ug_note = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.ug_sort = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.ug_sort)).BeginInit();
            this.SuspendLayout();
            // 
            // ug_code
            // 
            this.ug_code.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ug_code.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ug_code.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ug_code.Location = new System.Drawing.Point(179, 123);
            this.ug_code.Name = "ug_code";
            this.ug_code.Size = new System.Drawing.Size(422, 26);
            this.ug_code.TabIndex = 106;
            // 
            // ug_name
            // 
            this.ug_name.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ug_name.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ug_name.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ug_name.Location = new System.Drawing.Point(179, 66);
            this.ug_name.Name = "ug_name";
            this.ug_name.Size = new System.Drawing.Size(422, 26);
            this.ug_name.TabIndex = 105;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("微软雅黑", 11F, System.Drawing.FontStyle.Bold);
            this.label10.Location = new System.Drawing.Point(122, 265);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(54, 19);
            this.label10.TabIndex = 98;
            this.label10.Text = "说明：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 11F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(121, 125);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 19);
            this.label2.TabIndex = 90;
            this.label2.Text = "编码：";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(525, 393);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(76, 29);
            this.button1.TabIndex = 89;
            this.button1.Text = "取消(&C)";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.UserGroup_btnClose);
            // 
            // btn_Save
            // 
            this.btn_Save.Location = new System.Drawing.Point(443, 393);
            this.btn_Save.Name = "btn_Save";
            this.btn_Save.Size = new System.Drawing.Size(76, 29);
            this.btn_Save.TabIndex = 88;
            this.btn_Save.Text = "保存(&S)";
            this.btn_Save.UseVisualStyleBackColor = true;
            this.btn_Save.Click += new System.EventHandler(this.UserGroup_btnSave);
            // 
            // ug_note
            // 
            this.ug_note.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ug_note.Location = new System.Drawing.Point(179, 249);
            this.ug_note.Multiline = true;
            this.ug_note.Name = "ug_note";
            this.ug_note.Size = new System.Drawing.Size(422, 70);
            this.ug_note.TabIndex = 87;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 11F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(76, 68);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(99, 19);
            this.label1.TabIndex = 86;
            this.label1.Text = "用户组名称：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 11F, System.Drawing.FontStyle.Bold);
            this.label3.Location = new System.Drawing.Point(122, 184);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(54, 19);
            this.label3.TabIndex = 107;
            this.label3.Text = "排序：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("微软雅黑", 11F, System.Drawing.FontStyle.Bold);
            this.label4.ForeColor = System.Drawing.Color.Red;
            this.label4.Location = new System.Drawing.Point(606, 73);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(16, 19);
            this.label4.TabIndex = 109;
            this.label4.Text = "*";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("微软雅黑", 11F, System.Drawing.FontStyle.Bold);
            this.label5.ForeColor = System.Drawing.Color.Red;
            this.label5.Location = new System.Drawing.Point(606, 128);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(16, 19);
            this.label5.TabIndex = 110;
            this.label5.Text = "*";
            // 
            // ug_sort
            // 
            this.ug_sort.Location = new System.Drawing.Point(179, 184);
            this.ug_sort.Name = "ug_sort";
            this.ug_sort.Size = new System.Drawing.Size(422, 21);
            this.ug_sort.TabIndex = 111;
            // 
            // Frm_userGroupAdd
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(709, 472);
            this.Controls.Add(this.ug_sort);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.ug_code);
            this.Controls.Add(this.ug_name);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btn_Save);
            this.Controls.Add(this.ug_note);
            this.Controls.Add(this.label1);
            this.MinimizeBox = false;
            this.Name = "Frm_userGroupAdd";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "用户组";
            ((System.ComponentModel.ISupportInitialize)(this.ug_sort)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox ug_code;
        private System.Windows.Forms.TextBox ug_name;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btn_Save;
        private System.Windows.Forms.TextBox ug_note;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown ug_sort;
    }
}