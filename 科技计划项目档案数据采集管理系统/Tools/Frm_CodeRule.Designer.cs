namespace 科技计划项目档案数据采集管理系统
{
    partial class Frm_CodeRule
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_CodeRule));
            this.label1 = new System.Windows.Forms.Label();
            this.cbo_Type = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.chk_ZX_Code = new System.Windows.Forms.CheckBox();
            this.chk_Unit = new System.Windows.Forms.CheckBox();
            this.chk_Water = new System.Windows.Forms.CheckBox();
            this.num_Water = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lbl_Template = new System.Windows.Forms.Label();
            this.btn_Save = new 科技计划项目档案数据采集管理系统.KyoControl.KyoButton();
            this.btn_Reset = new 科技计划项目档案数据采集管理系统.KyoControl.KyoButton();
            this.label5 = new System.Windows.Forms.Label();
            this.txt_Mdi = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.chk_Year = new System.Windows.Forms.CheckBox();
            this.label9 = new System.Windows.Forms.Label();
            this.chk_KT_Code = new System.Windows.Forms.CheckBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txt_Fixed = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.num_Water)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(41, 29);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 19);
            this.label1.TabIndex = 0;
            this.label1.Text = "所属对象：";
            // 
            // cbo_Type
            // 
            this.cbo_Type.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo_Type.FormattingEnabled = true;
            this.cbo_Type.Location = new System.Drawing.Point(151, 25);
            this.cbo_Type.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cbo_Type.Name = "cbo_Type";
            this.cbo_Type.Size = new System.Drawing.Size(160, 28);
            this.cbo_Type.TabIndex = 1;
            this.cbo_Type.SelectedIndexChanged += new System.EventHandler(this.Cbo_Type_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(41, 162);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 19);
            this.label2.TabIndex = 2;
            this.label2.Text = "指定组合：";
            // 
            // chk_ZX_Code
            // 
            this.chk_ZX_Code.AutoSize = true;
            this.chk_ZX_Code.Location = new System.Drawing.Point(151, 162);
            this.chk_ZX_Code.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.chk_ZX_Code.Name = "chk_ZX_Code";
            this.chk_ZX_Code.Size = new System.Drawing.Size(84, 24);
            this.chk_ZX_Code.TabIndex = 3;
            this.chk_ZX_Code.Text = "专项编号";
            this.chk_ZX_Code.UseVisualStyleBackColor = true;
            this.chk_ZX_Code.CheckedChanged += new System.EventHandler(this.chk_Code_CheckedChanged);
            // 
            // chk_Unit
            // 
            this.chk_Unit.AutoSize = true;
            this.chk_Unit.Location = new System.Drawing.Point(470, 162);
            this.chk_Unit.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.chk_Unit.Name = "chk_Unit";
            this.chk_Unit.Size = new System.Drawing.Size(112, 24);
            this.chk_Unit.TabIndex = 4;
            this.chk_Unit.Text = "来源单位编号";
            this.chk_Unit.UseVisualStyleBackColor = true;
            this.chk_Unit.CheckedChanged += new System.EventHandler(this.chk_Unit_CheckedChanged);
            // 
            // chk_Water
            // 
            this.chk_Water.AutoSize = true;
            this.chk_Water.Location = new System.Drawing.Point(288, 217);
            this.chk_Water.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.chk_Water.Name = "chk_Water";
            this.chk_Water.Size = new System.Drawing.Size(70, 24);
            this.chk_Water.TabIndex = 5;
            this.chk_Water.Text = "流水号";
            this.chk_Water.UseVisualStyleBackColor = true;
            this.chk_Water.CheckedChanged += new System.EventHandler(this.chk_Water_CheckedChanged);
            // 
            // num_Water
            // 
            this.num_Water.Enabled = false;
            this.num_Water.Location = new System.Drawing.Point(358, 216);
            this.num_Water.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.num_Water.Name = "num_Water";
            this.num_Water.Size = new System.Drawing.Size(47, 26);
            this.num_Water.TabIndex = 6;
            this.num_Water.ValueChanged += new System.EventHandler(this.num_Water_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(405, 219);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(23, 20);
            this.label3.TabIndex = 7;
            this.label3.Text = "位";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(41, 346);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(79, 19);
            this.label4.TabIndex = 8;
            this.label4.Text = "当前示例：";
            // 
            // lbl_Template
            // 
            this.lbl_Template.AutoSize = true;
            this.lbl_Template.Location = new System.Drawing.Point(151, 346);
            this.lbl_Template.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_Template.Name = "lbl_Template";
            this.lbl_Template.Size = new System.Drawing.Size(35, 20);
            this.lbl_Template.TabIndex = 9;
            this.lbl_Template.Text = "null";
            // 
            // btn_Save
            // 
            this.btn_Save.Image = ((System.Drawing.Image)(resources.GetObject("btn_Save.Image")));
            this.btn_Save.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btn_Save.Location = new System.Drawing.Point(325, 420);
            this.btn_Save.Name = "btn_Save";
            this.btn_Save.Size = new System.Drawing.Size(68, 30);
            this.btn_Save.TabIndex = 10;
            this.btn_Save.Text = "保存";
            this.btn_Save.Click += new System.EventHandler(this.Btn_Save_Click);
            // 
            // btn_Reset
            // 
            this.btn_Reset.Image = ((System.Drawing.Image)(resources.GetObject("btn_Reset.Image")));
            this.btn_Reset.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btn_Reset.Location = new System.Drawing.Point(244, 420);
            this.btn_Reset.Name = "btn_Reset";
            this.btn_Reset.Size = new System.Drawing.Size(68, 30);
            this.btn_Reset.TabIndex = 11;
            this.btn_Reset.Text = "重置";
            this.btn_Reset.Click += new System.EventHandler(this.btn_Reset_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(55, 283);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 19);
            this.label5.TabIndex = 12;
            this.label5.Text = "间隔符：";
            // 
            // txt_Mdi
            // 
            this.txt_Mdi.Location = new System.Drawing.Point(155, 280);
            this.txt_Mdi.Name = "txt_Mdi";
            this.txt_Mdi.ReadOnly = true;
            this.txt_Mdi.Size = new System.Drawing.Size(37, 26);
            this.txt_Mdi.TabIndex = 13;
            this.txt_Mdi.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txt_Mdi.TextChanged += new System.EventHandler(this.txt_Mdi_TextChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(227, 165);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(48, 17);
            this.label6.TabIndex = 14;
            this.label6.Text = "(AAAA)";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.Location = new System.Drawing.Point(573, 165);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(48, 17);
            this.label7.TabIndex = 15;
            this.label7.Text = "(CCCC)";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label8.Location = new System.Drawing.Point(200, 221);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(44, 17);
            this.label8.TabIndex = 17;
            this.label8.Text = "(YYYY)";
            // 
            // chk_Year
            // 
            this.chk_Year.AutoSize = true;
            this.chk_Year.Location = new System.Drawing.Point(151, 217);
            this.chk_Year.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.chk_Year.Name = "chk_Year";
            this.chk_Year.Size = new System.Drawing.Size(56, 24);
            this.chk_Year.TabIndex = 16;
            this.chk_Year.Text = "年度";
            this.chk_Year.UseVisualStyleBackColor = true;
            this.chk_Year.CheckedChanged += new System.EventHandler(this.chk_Year_CheckedChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label9.Location = new System.Drawing.Point(398, 166);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(48, 17);
            this.label9.TabIndex = 19;
            this.label9.Text = "(BBBB)";
            // 
            // chk_KT_Code
            // 
            this.chk_KT_Code.AutoSize = true;
            this.chk_KT_Code.Location = new System.Drawing.Point(288, 162);
            this.chk_KT_Code.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.chk_KT_Code.Name = "chk_KT_Code";
            this.chk_KT_Code.Size = new System.Drawing.Size(118, 24);
            this.chk_KT_Code.TabIndex = 18;
            this.chk_KT_Code.Text = "项目/课题编号";
            this.chk_KT_Code.UseVisualStyleBackColor = true;
            this.chk_KT_Code.CheckedChanged += new System.EventHandler(this.chk_KT_Code_CheckedChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label10.Location = new System.Drawing.Point(41, 95);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(79, 19);
            this.label10.TabIndex = 20;
            this.label10.Text = "固定部分：";
            // 
            // txt_Fixed
            // 
            this.txt_Fixed.Location = new System.Drawing.Point(151, 91);
            this.txt_Fixed.Name = "txt_Fixed";
            this.txt_Fixed.Size = new System.Drawing.Size(160, 26);
            this.txt_Fixed.TabIndex = 21;
            // 
            // Frm_CodeRule
            // 
            this.Appearance.Options.UseFont = true;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(637, 473);
            this.Controls.Add(this.txt_Fixed);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.chk_KT_Code);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.chk_Year);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txt_Mdi);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.btn_Reset);
            this.Controls.Add(this.btn_Save);
            this.Controls.Add(this.lbl_Template);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.num_Water);
            this.Controls.Add(this.chk_Water);
            this.Controls.Add(this.chk_Unit);
            this.Controls.Add(this.chk_ZX_Code);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cbo_Type);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Frm_CodeRule";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Load += new System.EventHandler(this.Frm_CodeRule_Load);
            ((System.ComponentModel.ISupportInitialize)(this.num_Water)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbo_Type;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox chk_ZX_Code;
        private System.Windows.Forms.CheckBox chk_Unit;
        private System.Windows.Forms.CheckBox chk_Water;
        private System.Windows.Forms.NumericUpDown num_Water;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lbl_Template;
        private KyoControl.KyoButton btn_Save;
        private KyoControl.KyoButton btn_Reset;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txt_Mdi;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.CheckBox chk_Year;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.CheckBox chk_KT_Code;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txt_Fixed;
    }
}