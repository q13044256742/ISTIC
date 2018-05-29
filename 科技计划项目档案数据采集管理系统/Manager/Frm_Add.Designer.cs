namespace 科技计划项目档案数据采集管理系统.Manager
{
    partial class Frm_Add
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
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txt_ParentName = new System.Windows.Forms.TextBox();
            this.txt_Name = new System.Windows.Forms.TextBox();
            this.txt_Intro = new System.Windows.Forms.TextBox();
            this.btn_Save = new 科技计划项目档案数据采集管理系统.KyoControl.KyoButton();
            this.num_Sort = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.txt_Code = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.num_Sort)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(48, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(106, 22);
            this.label1.TabIndex = 7;
            this.label1.Text = "父节点名称：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(64, 81);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(90, 22);
            this.label2.TabIndex = 8;
            this.label2.Text = "节点名称：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold);
            this.label4.Location = new System.Drawing.Point(96, 181);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(58, 22);
            this.label4.TabIndex = 10;
            this.label4.Text = "排序：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold);
            this.label5.Location = new System.Drawing.Point(96, 231);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(58, 22);
            this.label5.TabIndex = 11;
            this.label5.Text = "描述：";
            // 
            // txt_ParentName
            // 
            this.txt_ParentName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txt_ParentName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_ParentName.Enabled = false;
            this.txt_ParentName.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txt_ParentName.Location = new System.Drawing.Point(160, 29);
            this.txt_ParentName.Name = "txt_ParentName";
            this.txt_ParentName.ReadOnly = true;
            this.txt_ParentName.Size = new System.Drawing.Size(377, 26);
            this.txt_ParentName.TabIndex = 12;
            // 
            // txt_Name
            // 
            this.txt_Name.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txt_Name.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_Name.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txt_Name.Location = new System.Drawing.Point(160, 79);
            this.txt_Name.Name = "txt_Name";
            this.txt_Name.Size = new System.Drawing.Size(377, 26);
            this.txt_Name.TabIndex = 0;
            // 
            // txt_Intro
            // 
            this.txt_Intro.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txt_Intro.Location = new System.Drawing.Point(160, 231);
            this.txt_Intro.Multiline = true;
            this.txt_Intro.Name = "txt_Intro";
            this.txt_Intro.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txt_Intro.Size = new System.Drawing.Size(377, 106);
            this.txt_Intro.TabIndex = 3;
            // 
            // btn_Save
            // 
            this.btn_Save.Location = new System.Drawing.Point(277, 376);
            this.btn_Save.Name = "btn_Save";
            this.btn_Save.Size = new System.Drawing.Size(80, 33);
            this.btn_Save.TabIndex = 4;
            this.btn_Save.Text = "保存(&S)";
            this.btn_Save.Click += new System.EventHandler(this.Btn_Save);
            // 
            // num_Sort
            // 
            this.num_Sort.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.num_Sort.Location = new System.Drawing.Point(160, 182);
            this.num_Sort.Name = "num_Sort";
            this.num_Sort.Size = new System.Drawing.Size(195, 26);
            this.num_Sort.TabIndex = 2;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("微软雅黑", 11F, System.Drawing.FontStyle.Bold);
            this.label7.ForeColor = System.Drawing.Color.Red;
            this.label7.Location = new System.Drawing.Point(543, 83);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(16, 19);
            this.label7.TabIndex = 111;
            this.label7.Text = "*";
            // 
            // txt_Code
            // 
            this.txt_Code.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txt_Code.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_Code.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txt_Code.Location = new System.Drawing.Point(160, 129);
            this.txt_Code.Name = "txt_Code";
            this.txt_Code.Size = new System.Drawing.Size(268, 26);
            this.txt_Code.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold);
            this.label3.Location = new System.Drawing.Point(96, 131);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 22);
            this.label3.TabIndex = 112;
            this.label3.Text = "编码：";
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // Frm_Add
            // 
            this.AcceptButton = this.btn_Save;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(634, 429);
            this.Controls.Add(this.txt_Code);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.num_Sort);
            this.Controls.Add(this.btn_Save);
            this.Controls.Add(this.txt_Intro);
            this.Controls.Add(this.txt_Name);
            this.Controls.Add(this.txt_ParentName);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Frm_Add";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "字典信息";
            ((System.ComponentModel.ISupportInitialize)(this.num_Sort)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txt_ParentName;
        private System.Windows.Forms.TextBox txt_Name;
        private System.Windows.Forms.TextBox txt_Intro;
        private KyoControl.KyoButton btn_Save;
        private System.Windows.Forms.NumericUpDown num_Sort;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txt_Code;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ErrorProvider errorProvider1;
    }
}