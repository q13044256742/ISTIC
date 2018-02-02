namespace 科技计划项目档案数据采集管理系统.Manager
{
    partial class Frm_UserGroupSelect
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
            this.List_all = new System.Windows.Forms.ListView();
            this.button1 = new System.Windows.Forms.Button();
            this.list_select = new System.Windows.Forms.ListView();
            this.button2 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // List_all
            // 
            this.List_all.FullRowSelect = true;
            this.List_all.GridLines = true;
            this.List_all.Location = new System.Drawing.Point(31, 67);
            this.List_all.Name = "List_all";
            this.List_all.Size = new System.Drawing.Size(211, 257);
            this.List_all.TabIndex = 0;
            this.List_all.UseCompatibleStateImageBehavior = false;
            this.List_all.View = System.Windows.Forms.View.Details;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(258, 165);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = ">>>";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Btn_rightClick);
            // 
            // list_select
            // 
            this.list_select.FullRowSelect = true;
            this.list_select.GridLines = true;
            this.list_select.Location = new System.Drawing.Point(350, 67);
            this.list_select.Name = "list_select";
            this.list_select.Size = new System.Drawing.Size(211, 257);
            this.list_select.TabIndex = 3;
            this.list_select.UseCompatibleStateImageBehavior = false;
            this.list_select.View = System.Windows.Forms.View.Details;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(258, 201);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 4;
            this.button2.Text = "<<<";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.Btn_leftClick);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(391, 376);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(79, 37);
            this.button5.TabIndex = 7;
            this.button5.Text = "保存";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.Ug_seclect_btnSave);
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(487, 376);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(79, 37);
            this.button6.TabIndex = 8;
            this.button6.Text = "取消";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.Ug_select_btnClose);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 11F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(87, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(99, 19);
            this.label1.TabIndex = 26;
            this.label1.Text = "待选用户组：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 11F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(404, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(99, 19);
            this.label2.TabIndex = 27;
            this.label2.Text = "已选用户组：";
            // 
            // Frm_UserGroupSelect
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(606, 454);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.list_select);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.List_all);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Frm_UserGroupSelect";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "用户组选择";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView List_all;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ListView list_select;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}