namespace 科技计划项目档案数据采集管理系统
{
    partial class Frm_SpecialSymbol
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_SpecialSymbol));
            this.lsv_Top = new System.Windows.Forms.ListView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lbl_Temp = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lsv_Normal = new System.Windows.Forms.ListView();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.lsv_Bottom = new System.Windows.Forms.ListView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btn_Sure = new DevExpress.XtraEditors.SimpleButton();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lsv_Top
            // 
            this.lsv_Top.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lsv_Top.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lsv_Top.Location = new System.Drawing.Point(3, 18);
            this.lsv_Top.MultiSelect = false;
            this.lsv_Top.Name = "lsv_Top";
            this.lsv_Top.Size = new System.Drawing.Size(699, 141);
            this.lsv_Top.TabIndex = 0;
            this.lsv_Top.UseCompatibleStateImageBehavior = false;
            this.lsv_Top.View = System.Windows.Forms.View.SmallIcon;
            this.lsv_Top.SelectedIndexChanged += new System.EventHandler(this.LSV_SelectedIndexChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lsv_Top);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 135);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(705, 162);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "上标符号";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 15F);
            this.label1.Location = new System.Drawing.Point(302, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 27);
            this.label1.TabIndex = 2;
            this.label1.Text = "示例：";
            // 
            // lbl_Temp
            // 
            this.lbl_Temp.AutoSize = true;
            this.lbl_Temp.Font = new System.Drawing.Font("微软雅黑", 15F);
            this.lbl_Temp.Location = new System.Drawing.Point(377, 4);
            this.lbl_Temp.Name = "lbl_Temp";
            this.lbl_Temp.Size = new System.Drawing.Size(26, 27);
            this.lbl_Temp.TabIndex = 3;
            this.lbl_Temp.Text = "A";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lsv_Normal);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox2.Location = new System.Drawing.Point(0, 35);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(705, 100);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "常用符号";
            // 
            // lsv_Normal
            // 
            this.lsv_Normal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lsv_Normal.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lsv_Normal.Location = new System.Drawing.Point(3, 18);
            this.lsv_Normal.MultiSelect = false;
            this.lsv_Normal.Name = "lsv_Normal";
            this.lsv_Normal.Size = new System.Drawing.Size(699, 79);
            this.lsv_Normal.TabIndex = 0;
            this.lsv_Normal.UseCompatibleStateImageBehavior = false;
            this.lsv_Normal.View = System.Windows.Forms.View.SmallIcon;
            this.lsv_Normal.SelectedIndexChanged += new System.EventHandler(this.LSV_SelectedIndexChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.lsv_Bottom);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox3.Location = new System.Drawing.Point(0, 297);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(705, 138);
            this.groupBox3.TabIndex = 5;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "下标符号";
            // 
            // lsv_Bottom
            // 
            this.lsv_Bottom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lsv_Bottom.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lsv_Bottom.Location = new System.Drawing.Point(3, 18);
            this.lsv_Bottom.MultiSelect = false;
            this.lsv_Bottom.Name = "lsv_Bottom";
            this.lsv_Bottom.Size = new System.Drawing.Size(699, 117);
            this.lsv_Bottom.TabIndex = 0;
            this.lsv_Bottom.UseCompatibleStateImageBehavior = false;
            this.lsv_Bottom.View = System.Windows.Forms.View.SmallIcon;
            this.lsv_Bottom.SelectedIndexChanged += new System.EventHandler(this.LSV_SelectedIndexChanged);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.lbl_Temp);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(705, 35);
            this.panel1.TabIndex = 6;
            // 
            // btn_Sure
            // 
            this.btn_Sure.Image = ((System.Drawing.Image)(resources.GetObject("btn_Sure.Image")));
            this.btn_Sure.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btn_Sure.Location = new System.Drawing.Point(308, 438);
            this.btn_Sure.Name = "btn_Sure";
            this.btn_Sure.Size = new System.Drawing.Size(88, 29);
            this.btn_Sure.TabIndex = 7;
            this.btn_Sure.Text = "确定(&S)";
            this.btn_Sure.Click += new System.EventHandler(this.btn_Sure_Click);
            // 
            // Frm_SpecialSymbol
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(705, 472);
            this.Controls.Add(this.btn_Sure);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Frm_SpecialSymbol";
            this.ShowIcon = false;
            this.Text = "特殊符号选择";
            this.Load += new System.EventHandler(this.Frm_SpecialSymbol_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lsv_Top;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbl_Temp;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ListView lsv_Normal;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ListView lsv_Bottom;
        private System.Windows.Forms.Panel panel1;
        private DevExpress.XtraEditors.SimpleButton btn_Sure;
    }
}