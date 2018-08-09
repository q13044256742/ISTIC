namespace 科技计划项目档案数据采集管理系统
{
    partial class Frm_LogList
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_LogList));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.view = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btn_Query = new DevExpress.XtraEditors.SimpleButton();
            this.searchControl = new DevExpress.XtraEditors.SearchControl();
            this.panel2 = new System.Windows.Forms.Panel();
            this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.username = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ipaddress = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.login = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.logout = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.view)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.searchControl.Properties)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // view
            // 
            this.view.AllowUserToAddRows = false;
            this.view.AllowUserToDeleteRows = false;
            this.view.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.view.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.view.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.Padding = new System.Windows.Forms.Padding(0, 3, 0, 3);
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.view.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.view.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.view.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.id,
            this.username,
            this.ipaddress,
            this.login,
            this.logout});
            this.view.Dock = System.Windows.Forms.DockStyle.Fill;
            this.view.Location = new System.Drawing.Point(0, 0);
            this.view.Margin = new System.Windows.Forms.Padding(5);
            this.view.Name = "view";
            this.view.ReadOnly = true;
            this.view.RowTemplate.Height = 23;
            this.view.Size = new System.Drawing.Size(892, 477);
            this.view.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btn_Query);
            this.panel1.Controls.Add(this.searchControl);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(5);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(892, 55);
            this.panel1.TabIndex = 1;
            // 
            // btn_Query
            // 
            this.btn_Query.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Query.Appearance.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_Query.Appearance.Options.UseFont = true;
            this.btn_Query.Image = ((System.Drawing.Image)(resources.GetObject("btn_Query.Image")));
            this.btn_Query.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btn_Query.Location = new System.Drawing.Point(805, 12);
            this.btn_Query.Name = "btn_Query";
            this.btn_Query.Size = new System.Drawing.Size(75, 31);
            this.btn_Query.TabIndex = 1;
            this.btn_Query.Text = "查询";
            this.btn_Query.Click += new System.EventHandler(this.btn_Query_Click);
            // 
            // searchControl
            // 
            this.searchControl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.searchControl.Location = new System.Drawing.Point(535, 13);
            this.searchControl.Name = "searchControl";
            this.searchControl.Properties.Appearance.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.searchControl.Properties.Appearance.Options.UseFont = true;
            this.searchControl.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Repository.ClearButton(),
            new DevExpress.XtraEditors.Repository.SearchButton()});
            this.searchControl.Size = new System.Drawing.Size(262, 28);
            this.searchControl.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.view);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 55);
            this.panel2.Margin = new System.Windows.Forms.Padding(5);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(892, 477);
            this.panel2.TabIndex = 2;
            // 
            // id
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.id.DefaultCellStyle = dataGridViewCellStyle2;
            this.id.FillWeight = 50F;
            this.id.HeaderText = "序号";
            this.id.Name = "id";
            this.id.ReadOnly = true;
            this.id.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // username
            // 
            this.username.HeaderText = "用户姓名";
            this.username.Name = "username";
            this.username.ReadOnly = true;
            this.username.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // ipaddress
            // 
            this.ipaddress.HeaderText = "IP地址";
            this.ipaddress.Name = "ipaddress";
            this.ipaddress.ReadOnly = true;
            this.ipaddress.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // login
            // 
            this.login.HeaderText = "登录时间";
            this.login.Name = "login";
            this.login.ReadOnly = true;
            this.login.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // logout
            // 
            this.logout.HeaderText = "退出时间";
            this.logout.Name = "logout";
            this.logout.ReadOnly = true;
            this.logout.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Frm_LogList
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(892, 532);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "Frm_LogList";
            this.ShowIcon = false;
            this.Text = "安全监控";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Frm_LogList_Load);
            ((System.ComponentModel.ISupportInitialize)(this.view)).EndInit();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.searchControl.Properties)).EndInit();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView view;
        private System.Windows.Forms.Panel panel1;
        private DevExpress.XtraEditors.SimpleButton btn_Query;
        private DevExpress.XtraEditors.SearchControl searchControl;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.DataGridViewTextBoxColumn id;
        private System.Windows.Forms.DataGridViewTextBoxColumn username;
        private System.Windows.Forms.DataGridViewTextBoxColumn ipaddress;
        private System.Windows.Forms.DataGridViewTextBoxColumn login;
        private System.Windows.Forms.DataGridViewTextBoxColumn logout;
    }
}