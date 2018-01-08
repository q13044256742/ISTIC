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
    public partial class Frm_ProTypeSelect : Form
    {
        public Frm_ProTypeSelect()
        {
            InitializeComponent();
        }

        private void Frm_ProTypeSelect_Load(object sender, EventArgs e)
        {
            cbo_TypeSelect.SelectedIndex = 0;
        }

        private void btn_Sure_Click(object sender, EventArgs e)
        {
            Hide();
            Frm_MyWork frm = new Frm_MyWork(cbo_TypeSelect.SelectedItem.ToString());
            frm.ShowDialog();
        }
    }
}
