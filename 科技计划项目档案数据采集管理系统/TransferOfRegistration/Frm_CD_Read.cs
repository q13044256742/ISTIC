using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace 科技计划项目档案数据采集管理系统.TransferOfRegistration
{
    public partial class Frm_CD_Read : Form
    {
        public Frm_CD_Read()
        {
            InitializeComponent();
        }

        private void btn_CD_Choose_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            if(dialog.ShowDialog() == DialogResult.OK)
            {
                txt_CD_Path.Text = dialog.SelectedPath;
            }
        }

        private void btn_DS_Choose_Click(object sender, EventArgs e)
        {
            FileDialog dialog = new OpenFileDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                txt_DS_Path.Text = dialog.FileName;
            }
        }
    }
}
