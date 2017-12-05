using System;
using System.Threading;
using System.Windows.Forms;

namespace 科技计划项目档案数据采集管理系统
{
    public partial class Frm_ReadCD : Form
    {
        public Frm_ReadCD()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
        }

        private void btn_ReadCD_Click(object sender, System.EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if(openFileDialog.ShowDialog()== DialogResult.OK)
            {
                txt_CdPath.Text = openFileDialog.FileName;
                new Thread(delegate ()
                {
                    while (pgb_GP.Value < pgb_GP.Maximum)
                    {
                        pgb_GP.PerformStep();
                        Thread.Sleep(250);
                    }
                    Thread.CurrentThread.Abort();
                }).Start();
            }
        }

        private void btn_ReadYSJ_Click(object sender, System.EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                txt_YsjPath.Text = openFileDialog.FileName;
                new Thread(delegate ()
                {
                    while (pgb_YSJ.Value < pgb_YSJ.Maximum)
                    {
                        pgb_YSJ.PerformStep();
                        if (pgb_YSJ.Value >= pgb_YSJ.Maximum / 2)
                        {
                            TriggerError();
                            Thread.CurrentThread.Abort();
                        }
                        Thread.Sleep(250);
                    }
                }).Start();
            }

        }

        //触发错误
        private void TriggerError()
        {
            Height += 55;
        }

        private void btn_Back_Click(object sender, System.EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
