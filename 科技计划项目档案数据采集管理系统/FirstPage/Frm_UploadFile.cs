using System.Windows.Forms;

namespace 科技计划项目档案数据采集管理系统
{
    public partial class Frm_UploadFile : DevExpress.XtraEditors.XtraForm
    {
        public Frm_UploadFile()
        {
            InitializeComponent();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            openFileDialog1.Title = "选择待上传的文件";
            openFileDialog1.Multiselect = false;
            if(openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                txt_filePath.Text = openFileDialog1.FileName;
            }
        }

        private void btn_Upload_Click(object sender, System.EventArgs e)
        {
            if(!string.IsNullOrEmpty(txt_filePath.Text))
            {
                DialogResult = DialogResult.OK;
            }
            Close();
        }
    }
}
