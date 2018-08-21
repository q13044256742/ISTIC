using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace 科技计划项目档案数据采集管理系统
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            treeList1.Nodes.Clear();
            for(int i = 0; i < 5; i++)
            {
                treeList1.Nodes.Add(new object[] { i + "!", i + "!", i + "!", i + "!", i + "!" });
                for(int j = 0; j < 3; j++)
                {
                    treeList1.Nodes[i].Nodes.Add(new object[] { j, j, j, j, j });
                }
            }
        }
    }
}
