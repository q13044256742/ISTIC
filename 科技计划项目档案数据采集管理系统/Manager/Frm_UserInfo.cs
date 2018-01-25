using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace 科技计划项目档案数据采集管理系统.Manager
{
    public partial class Frm_UserInfo : Form
    {
        private GroupBox 列表;
        private DataGridView u_DataList;
        private GroupBox groupBox1;
        private Button button1;
        private Button button4;
        private Button button3;
        private Button btn_Add;
        private Button btn_Search;
        private Label label1;
        private ComboBox u_SearchType;
        private Label label2;
        private TextBox u_SearchKey;

        public Frm_UserInfo()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.列表 = new System.Windows.Forms.GroupBox();
            this.u_DataList = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.btn_Add = new System.Windows.Forms.Button();
            this.btn_Search = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.u_SearchType = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.u_SearchKey = new System.Windows.Forms.TextBox();
            this.列表.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.u_DataList)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // 列表
            // 
            this.列表.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.列表.Controls.Add(this.u_DataList);
            this.列表.Location = new System.Drawing.Point(12, 81);
            this.列表.Name = "列表";
            this.列表.Size = new System.Drawing.Size(977, 402);
            this.列表.TabIndex = 22;
            this.列表.TabStop = false;
            this.列表.Text = "字段列表";
            // 
            // u_DataList
            // 
            this.u_DataList.AllowUserToAddRows = false;
            this.u_DataList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.u_DataList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.u_DataList.BackgroundColor = System.Drawing.SystemColors.ButtonFace;
            this.u_DataList.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.u_DataList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.u_DataList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.u_DataList.Location = new System.Drawing.Point(6, 15);
            this.u_DataList.Name = "u_DataList";
            this.u_DataList.ReadOnly = true;
            this.u_DataList.RowTemplate.Height = 23;
            this.u_DataList.Size = new System.Drawing.Size(965, 381);
            this.u_DataList.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.button4);
            this.groupBox1.Controls.Add(this.button3);
            this.groupBox1.Controls.Add(this.btn_Add);
            this.groupBox1.Controls.Add(this.btn_Search);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.u_SearchType);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.u_SearchKey);
            this.groupBox1.Location = new System.Drawing.Point(12, 7);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(974, 55);
            this.groupBox1.TabIndex = 21;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "快速检索";
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.AutoSize = true;
            this.button1.Enabled = false;
            this.button1.Location = new System.Drawing.Point(904, 24);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(68, 27);
            this.button1.TabIndex = 13;
            this.button1.Text = "返回";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            this.button4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button4.AutoSize = true;
            this.button4.Location = new System.Drawing.Point(836, 24);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(68, 27);
            this.button4.TabIndex = 12;
            this.button4.Text = "删除";
            this.button4.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button3.AutoSize = true;
            this.button3.Location = new System.Drawing.Point(768, 24);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(68, 27);
            this.button3.TabIndex = 11;
            this.button3.Text = "修改";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // btn_Add
            // 
            this.btn_Add.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Add.AutoSize = true;
            this.btn_Add.Location = new System.Drawing.Point(700, 24);
            this.btn_Add.Name = "btn_Add";
            this.btn_Add.Size = new System.Drawing.Size(68, 27);
            this.btn_Add.TabIndex = 10;
            this.btn_Add.Text = "新增";
            this.btn_Add.UseVisualStyleBackColor = true;
            // 
            // btn_Search
            // 
            this.btn_Search.AutoSize = true;
            this.btn_Search.Location = new System.Drawing.Point(366, 23);
            this.btn_Search.Name = "btn_Search";
            this.btn_Search.Size = new System.Drawing.Size(68, 27);
            this.btn_Search.TabIndex = 9;
            this.btn_Search.Text = "查询";
            this.btn_Search.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(31, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(17, 12);
            this.label1.TabIndex = 5;
            this.label1.Text = "按";
            // 
            // u_SearchType
            // 
            this.u_SearchType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.u_SearchType.FormattingEnabled = true;
            this.u_SearchType.Items.AddRange(new object[] {
            "所属单位",
            "所属部门",
            "所属用户组",
            "真实姓名",
            "联系电话",
            "邮箱"});
            this.u_SearchType.Location = new System.Drawing.Point(54, 28);
            this.u_SearchType.Name = "u_SearchType";
            this.u_SearchType.Size = new System.Drawing.Size(97, 20);
            this.u_SearchType.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(159, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 7;
            this.label2.Text = "检索";
            // 
            // u_SearchKey
            // 
            this.u_SearchKey.Location = new System.Drawing.Point(194, 27);
            this.u_SearchKey.Name = "u_SearchKey";
            this.u_SearchKey.Size = new System.Drawing.Size(163, 21);
            this.u_SearchKey.TabIndex = 8;
            // 
            // Frm_UserInfo
            // 
            this.ClientSize = new System.Drawing.Size(1001, 491);
            this.Controls.Add(this.列表);
            this.Controls.Add(this.groupBox1);
            this.Name = "Frm_UserInfo";
            this.Text = "用户信息管理";
            this.列表.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.u_DataList)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }
    }
}
