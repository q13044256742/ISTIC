using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace 科技计划项目档案数据采集管理系统
{
    public partial class Frm_PrintChoose : DevExpress.XtraEditors.XtraForm
    {
        public Frm_PrintChoose()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            InitprinterComboBox(); //初始化打印机下拉列表选项
        }

        private void InitprinterComboBox()
        {
            List<String> list = LocalPrinter.GetLocalPrinters(); //获得系统中的打印机列表
            foreach(String s in list)
            {
                printerComboBox.Items.Add(s); //将打印机名称添加到下拉框中
            }
            if(printerComboBox.Items.Count > 0)
                printerComboBox.SelectedIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(printerComboBox.SelectedItem != null) //判断是否有选中值
            {
                if(Externs.SetDefaultPrinter(printerComboBox.SelectedItem.ToString())) //设置默认打印机
                {
                    MessageBox.Show(printerComboBox.SelectedItem.ToString() + "设置为默认打印机成功！");
                    DialogResult = DialogResult.OK;
                }
                else
                {
                    MessageBox.Show(printerComboBox.SelectedItem.ToString() + "设置为默认打印机失败！");
                    DialogResult = DialogResult.No;
                }
            }
        }
    }
}
