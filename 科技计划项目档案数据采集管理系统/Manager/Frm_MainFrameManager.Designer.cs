namespace 科技计划项目档案数据采集管理系统
{
    partial class Frm_MainFrameManager
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
            this.lbl_OtherInfo = new System.Windows.Forms.Label();
            this.txt_RealName = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lbl_ExitSystem = new System.Windows.Forms.LinkLabel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pal_Top = new System.Windows.Forms.Panel();
            this.pal_Top_Info = new System.Windows.Forms.Panel();
            this.pal_LeftMenu = new System.Windows.Forms.Panel();
            this.accordionControl1 = new 科技计划项目档案数据采集管理系统.KyoControl.KyoAccordion();
            this.accordionControlElement1 = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.ace_UserInfo = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.ace_UserGroup = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.accordionControlElement2 = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.ace_DicPlan = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.ace_DicFiles = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.ace_DicUnit = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.ace_DicStandard = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.accordionControlElement3 = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.ace_LoginLog = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.ace_WorkLog = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.accordionControlElement4 = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.ace_ReciveDemo = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.ace_Giveup = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.pal_Top.SuspendLayout();
            this.pal_Top_Info.SuspendLayout();
            this.pal_LeftMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.accordionControl1)).BeginInit();
            this.SuspendLayout();
            // 
            // lbl_OtherInfo
            // 
            this.lbl_OtherInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_OtherInfo.AutoSize = true;
            this.lbl_OtherInfo.BackColor = System.Drawing.Color.Transparent;
            this.lbl_OtherInfo.Location = new System.Drawing.Point(1067, 11);
            this.lbl_OtherInfo.Name = "lbl_OtherInfo";
            this.lbl_OtherInfo.Size = new System.Drawing.Size(192, 14);
            this.lbl_OtherInfo.TabIndex = 2;
            this.lbl_OtherInfo.Text = "当前时间：2017年4月21日 星期三";
            // 
            // txt_RealName
            // 
            this.txt_RealName.AutoSize = true;
            this.txt_RealName.BackColor = System.Drawing.Color.Transparent;
            this.txt_RealName.Location = new System.Drawing.Point(75, 11);
            this.txt_RealName.Name = "txt_RealName";
            this.txt_RealName.Size = new System.Drawing.Size(0, 14);
            this.txt_RealName.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(14, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 14);
            this.label1.TabIndex = 0;
            this.label1.Text = "欢迎你：";
            // 
            // lbl_ExitSystem
            // 
            this.lbl_ExitSystem.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_ExitSystem.BackColor = System.Drawing.Color.Transparent;
            this.lbl_ExitSystem.Font = new System.Drawing.Font("黑体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbl_ExitSystem.Image = global::科技计划项目档案数据采集管理系统.Properties.Resources.tuichu;
            this.lbl_ExitSystem.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lbl_ExitSystem.Location = new System.Drawing.Point(1118, 30);
            this.lbl_ExitSystem.Name = "lbl_ExitSystem";
            this.lbl_ExitSystem.Size = new System.Drawing.Size(101, 21);
            this.lbl_ExitSystem.TabIndex = 1;
            this.lbl_ExitSystem.TabStop = true;
            this.lbl_ExitSystem.Text = "退出系统";
            this.lbl_ExitSystem.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lbl_ExitSystem.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.ExitSystem_LinkClicked);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Image = global::科技计划项目档案数据采集管理系统.Properties.Resources.logo;
            this.pictureBox1.Location = new System.Drawing.Point(3, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(560, 76);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // pal_Top
            // 
            this.pal_Top.BackColor = System.Drawing.Color.Gray;
            this.pal_Top.BackgroundImage = global::科技计划项目档案数据采集管理系统.Properties.Resources.top;
            this.pal_Top.Controls.Add(this.pal_Top_Info);
            this.pal_Top.Controls.Add(this.pictureBox1);
            this.pal_Top.Controls.Add(this.lbl_ExitSystem);
            this.pal_Top.Dock = System.Windows.Forms.DockStyle.Top;
            this.pal_Top.Location = new System.Drawing.Point(0, 0);
            this.pal_Top.Name = "pal_Top";
            this.pal_Top.Size = new System.Drawing.Size(1271, 115);
            this.pal_Top.TabIndex = 1;
            // 
            // pal_Top_Info
            // 
            this.pal_Top_Info.BackgroundImage = global::科技计划项目档案数据采集管理系统.Properties.Resources.top_bg;
            this.pal_Top_Info.Controls.Add(this.label1);
            this.pal_Top_Info.Controls.Add(this.txt_RealName);
            this.pal_Top_Info.Controls.Add(this.lbl_OtherInfo);
            this.pal_Top_Info.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pal_Top_Info.Location = new System.Drawing.Point(0, 79);
            this.pal_Top_Info.Name = "pal_Top_Info";
            this.pal_Top_Info.Size = new System.Drawing.Size(1271, 36);
            this.pal_Top_Info.TabIndex = 1;
            // 
            // pal_LeftMenu
            // 
            this.pal_LeftMenu.AutoScroll = true;
            this.pal_LeftMenu.Controls.Add(this.accordionControl1);
            this.pal_LeftMenu.Dock = System.Windows.Forms.DockStyle.Left;
            this.pal_LeftMenu.Location = new System.Drawing.Point(0, 115);
            this.pal_LeftMenu.Name = "pal_LeftMenu";
            this.pal_LeftMenu.Size = new System.Drawing.Size(260, 626);
            this.pal_LeftMenu.TabIndex = 19;
            // 
            // accordionControl1
            // 
            this.accordionControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.accordionControl1.Elements.AddRange(new DevExpress.XtraBars.Navigation.AccordionControlElement[] {
            this.accordionControlElement1,
            this.accordionControlElement2,
            this.accordionControlElement3,
            this.accordionControlElement4});
            this.accordionControl1.Location = new System.Drawing.Point(0, 0);
            this.accordionControl1.Name = "accordionControl1";
            this.accordionControl1.ScrollBarMode = DevExpress.XtraBars.Navigation.ScrollBarMode.Hidden;
            this.accordionControl1.Size = new System.Drawing.Size(260, 626);
            this.accordionControl1.TabIndex = 0;
            // 
            // accordionControlElement1
            // 
            this.accordionControlElement1.Appearance.Normal.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.accordionControlElement1.Appearance.Normal.ForeColor = System.Drawing.Color.Black;
            this.accordionControlElement1.Appearance.Normal.Options.UseFont = true;
            this.accordionControlElement1.Appearance.Normal.Options.UseForeColor = true;
            this.accordionControlElement1.Elements.AddRange(new DevExpress.XtraBars.Navigation.AccordionControlElement[] {
            this.ace_UserInfo,
            this.ace_UserGroup});
            this.accordionControlElement1.Expanded = true;
            this.accordionControlElement1.Name = "accordionControlElement1";
            this.accordionControlElement1.Text = "用户管理";
            // 
            // ace_UserInfo
            // 
            this.ace_UserInfo.Appearance.Normal.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ace_UserInfo.Appearance.Normal.ForeColor = System.Drawing.Color.Blue;
            this.ace_UserInfo.Appearance.Normal.Options.UseFont = true;
            this.ace_UserInfo.Appearance.Normal.Options.UseForeColor = true;
            this.ace_UserInfo.Name = "ace_UserInfo";
            this.ace_UserInfo.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.ace_UserInfo.Text = "用户信息管理";
            this.ace_UserInfo.TextToImageDistance = 10;
            this.ace_UserInfo.Click += new System.EventHandler(this.UserInfo_Click);
            // 
            // ace_UserGroup
            // 
            this.ace_UserGroup.Appearance.Normal.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ace_UserGroup.Appearance.Normal.ForeColor = System.Drawing.Color.Blue;
            this.ace_UserGroup.Appearance.Normal.Options.UseFont = true;
            this.ace_UserGroup.Appearance.Normal.Options.UseForeColor = true;
            this.ace_UserGroup.Name = "ace_UserGroup";
            this.ace_UserGroup.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.ace_UserGroup.Text = "用户组管理";
            this.ace_UserGroup.Click += new System.EventHandler(this.UserGroup_Click);
            // 
            // accordionControlElement2
            // 
            this.accordionControlElement2.Appearance.Normal.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.accordionControlElement2.Appearance.Normal.ForeColor = System.Drawing.Color.Black;
            this.accordionControlElement2.Appearance.Normal.Options.UseFont = true;
            this.accordionControlElement2.Appearance.Normal.Options.UseForeColor = true;
            this.accordionControlElement2.Elements.AddRange(new DevExpress.XtraBars.Navigation.AccordionControlElement[] {
            this.ace_DicPlan,
            this.ace_DicFiles,
            this.ace_DicUnit,
            this.ace_DicStandard});
            this.accordionControlElement2.Expanded = true;
            this.accordionControlElement2.Name = "accordionControlElement2";
            this.accordionControlElement2.Text = "字典管理";
            // 
            // ace_DicPlan
            // 
            this.ace_DicPlan.Appearance.Normal.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ace_DicPlan.Appearance.Normal.ForeColor = System.Drawing.Color.Blue;
            this.ace_DicPlan.Appearance.Normal.Options.UseFont = true;
            this.ace_DicPlan.Appearance.Normal.Options.UseForeColor = true;
            this.ace_DicPlan.Name = "ace_DicPlan";
            this.ace_DicPlan.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.ace_DicPlan.Tag = "D752F90E-A5BC-4C4F-91FD-C4EA250B61DA";
            this.ace_DicPlan.Text = "计划字典";
            this.ace_DicPlan.Click += new System.EventHandler(this.Dictionary_Click);
            // 
            // ace_DicFiles
            // 
            this.ace_DicFiles.Appearance.Normal.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ace_DicFiles.Appearance.Normal.ForeColor = System.Drawing.Color.Blue;
            this.ace_DicFiles.Appearance.Normal.Options.UseFont = true;
            this.ace_DicFiles.Appearance.Normal.Options.UseForeColor = true;
            this.ace_DicFiles.Name = "ace_DicFiles";
            this.ace_DicFiles.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.ace_DicFiles.Tag = "08C7AD07-F7D0-4EAC-AA03-1155351C9B3D";
            this.ace_DicFiles.Text = "文件字典";
            this.ace_DicFiles.Click += new System.EventHandler(this.Dictionary_Click);
            // 
            // ace_DicUnit
            // 
            this.ace_DicUnit.Appearance.Normal.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ace_DicUnit.Appearance.Normal.ForeColor = System.Drawing.Color.Blue;
            this.ace_DicUnit.Appearance.Normal.Options.UseFont = true;
            this.ace_DicUnit.Appearance.Normal.Options.UseForeColor = true;
            this.ace_DicUnit.Name = "ace_DicUnit";
            this.ace_DicUnit.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.ace_DicUnit.Tag = "421E381F-237C-4395-A08B-0E20435AE91B";
            this.ace_DicUnit.Text = "单位字典";
            this.ace_DicUnit.Click += new System.EventHandler(this.Dictionary_Click);
            // 
            // ace_DicStandard
            // 
            this.ace_DicStandard.Appearance.Normal.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ace_DicStandard.Appearance.Normal.ForeColor = System.Drawing.Color.Blue;
            this.ace_DicStandard.Appearance.Normal.Options.UseFont = true;
            this.ace_DicStandard.Appearance.Normal.Options.UseForeColor = true;
            this.ace_DicStandard.Name = "ace_DicStandard";
            this.ace_DicStandard.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.ace_DicStandard.Tag = "19B6FF50-3C10-4B19-9C34-7EE25FA0996B";
            this.ace_DicStandard.Text = "标准字典";
            this.ace_DicStandard.Click += new System.EventHandler(this.Dictionary_Click);
            // 
            // accordionControlElement3
            // 
            this.accordionControlElement3.Appearance.Normal.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.accordionControlElement3.Appearance.Normal.ForeColor = System.Drawing.Color.Black;
            this.accordionControlElement3.Appearance.Normal.Options.UseFont = true;
            this.accordionControlElement3.Appearance.Normal.Options.UseForeColor = true;
            this.accordionControlElement3.Elements.AddRange(new DevExpress.XtraBars.Navigation.AccordionControlElement[] {
            this.ace_LoginLog,
            this.ace_WorkLog});
            this.accordionControlElement3.Expanded = true;
            this.accordionControlElement3.Name = "accordionControlElement3";
            this.accordionControlElement3.Text = "安全监控";
            // 
            // ace_LoginLog
            // 
            this.ace_LoginLog.Appearance.Normal.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ace_LoginLog.Appearance.Normal.ForeColor = System.Drawing.Color.Blue;
            this.ace_LoginLog.Appearance.Normal.Options.UseFont = true;
            this.ace_LoginLog.Appearance.Normal.Options.UseForeColor = true;
            this.ace_LoginLog.Name = "ace_LoginLog";
            this.ace_LoginLog.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.ace_LoginLog.Text = "登录日志";
            // 
            // ace_WorkLog
            // 
            this.ace_WorkLog.Appearance.Normal.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ace_WorkLog.Appearance.Normal.ForeColor = System.Drawing.Color.Blue;
            this.ace_WorkLog.Appearance.Normal.Options.UseFont = true;
            this.ace_WorkLog.Appearance.Normal.Options.UseForeColor = true;
            this.ace_WorkLog.Name = "ace_WorkLog";
            this.ace_WorkLog.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.ace_WorkLog.Text = "业务日志";
            // 
            // accordionControlElement4
            // 
            this.accordionControlElement4.Appearance.Normal.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.accordionControlElement4.Appearance.Normal.ForeColor = System.Drawing.Color.Black;
            this.accordionControlElement4.Appearance.Normal.Options.UseFont = true;
            this.accordionControlElement4.Appearance.Normal.Options.UseForeColor = true;
            this.accordionControlElement4.Elements.AddRange(new DevExpress.XtraBars.Navigation.AccordionControlElement[] {
            this.ace_ReciveDemo,
            this.ace_Giveup});
            this.accordionControlElement4.Expanded = true;
            this.accordionControlElement4.Name = "accordionControlElement4";
            this.accordionControlElement4.Text = "模板管理";
            // 
            // ace_ReciveDemo
            // 
            this.ace_ReciveDemo.Appearance.Normal.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ace_ReciveDemo.Appearance.Normal.ForeColor = System.Drawing.Color.Blue;
            this.ace_ReciveDemo.Appearance.Normal.Options.UseFont = true;
            this.ace_ReciveDemo.Appearance.Normal.Options.UseForeColor = true;
            this.ace_ReciveDemo.Name = "ace_ReciveDemo";
            this.ace_ReciveDemo.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.ace_ReciveDemo.Text = "接收确认函";
            this.ace_ReciveDemo.Click += new System.EventHandler(this.Demo_Click);
            // 
            // ace_Giveup
            // 
            this.ace_Giveup.Appearance.Normal.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ace_Giveup.Appearance.Normal.ForeColor = System.Drawing.Color.Blue;
            this.ace_Giveup.Appearance.Normal.Options.UseFont = true;
            this.ace_Giveup.Appearance.Normal.Options.UseForeColor = true;
            this.ace_Giveup.Name = "ace_Giveup";
            this.ace_Giveup.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.ace_Giveup.Text = "催报单";
            this.ace_Giveup.Click += new System.EventHandler(this.Demo_Click);
            // 
            // Frm_MainFrameManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1271, 741);
            this.Controls.Add(this.pal_LeftMenu);
            this.Controls.Add(this.pal_Top);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.IsMdiContainer = true;
            this.Name = "Frm_MainFrameManager";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "科技计划项目档案数据采集管理系统";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Frm_MainFrame_FormClosing);
            this.Load += new System.EventHandler(this.Frm_MainFrame_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.pal_Top.ResumeLayout(false);
            this.pal_Top_Info.ResumeLayout(false);
            this.pal_Top_Info.PerformLayout();
            this.pal_LeftMenu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.accordionControl1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.LinkLabel lbl_ExitSystem;
        private System.Windows.Forms.Label lbl_OtherInfo;
        private System.Windows.Forms.Label txt_RealName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel pal_Top;
        private System.Windows.Forms.Panel pal_Top_Info;
        private System.Windows.Forms.Panel pal_LeftMenu;
        private KyoControl.KyoAccordion accordionControl1;
        private DevExpress.XtraBars.Navigation.AccordionControlElement accordionControlElement1;
        private DevExpress.XtraBars.Navigation.AccordionControlElement accordionControlElement2;
        private DevExpress.XtraBars.Navigation.AccordionControlElement accordionControlElement3;
        private DevExpress.XtraBars.Navigation.AccordionControlElement accordionControlElement4;
        private DevExpress.XtraBars.Navigation.AccordionControlElement ace_UserInfo;
        private DevExpress.XtraBars.Navigation.AccordionControlElement ace_UserGroup;
        private DevExpress.XtraBars.Navigation.AccordionControlElement ace_DicPlan;
        private DevExpress.XtraBars.Navigation.AccordionControlElement ace_DicFiles;
        private DevExpress.XtraBars.Navigation.AccordionControlElement ace_DicUnit;
        private DevExpress.XtraBars.Navigation.AccordionControlElement ace_DicStandard;
        private DevExpress.XtraBars.Navigation.AccordionControlElement ace_LoginLog;
        private DevExpress.XtraBars.Navigation.AccordionControlElement ace_WorkLog;
        private DevExpress.XtraBars.Navigation.AccordionControlElement ace_ReciveDemo;
        private DevExpress.XtraBars.Navigation.AccordionControlElement ace_Giveup;
    }
}