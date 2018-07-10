using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;
using System.Windows.Forms;

namespace 科技计划项目档案数据采集管理系统
{
    public partial class Frm_FileBackUpTable : DevExpress.XtraEditors.XtraForm
    {
        public Frm_FileBackUpTable()
        {
            InitializeComponent();
        }
        public int fileAmount;
        public int filePages;
        public object docNumber;
        public object user;
        private void Frm_FileBackUpTable_Load(object sender, EventArgs e)
        {
            lbl_Amount.Text = GetZN(fileAmount);
            lbl_Count.Text = GetZN(filePages);

            lbl_DocNumber.Text = GetValue(docNumber);

            lbl_LiJuanRen.Text = UserHelper.GetInstance().User.RealName;
            lbl_date1.Text = DateTime.Now.ToString("yyyy 年 MM 月 dd 日");

            //pal_Show.Height = 800;
        }

        private string GetValue(object value) => value == null ? string.Empty : value.ToString();

        private string GetZN(int param)
        {
            string[] number = { "零", "壹", "贰", "叁", "肆", "伍", "陆", "柒", "捌", "玖" };
            string[] dom = { "", "拾", "佰", "仟", "万", "拾万", "佰万", "仟万" };
            string index = GetValue(param);
            StringBuilder sb = new StringBuilder();
            for(int i = 0; i < index.Length; i++)
            {
                sb.Append(number[index[i] - '0']);
                sb.Append(dom[index.Length - 1 - i]);
            }
            return sb.ToString();
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Bitmap bitmap = new Bitmap(pal_Show.Width, pal_Show.Height, PixelFormat.Format24bppRgb);
            Graphics graphics = Graphics.FromImage(bitmap);
            graphics.Clear(Color.White);
            pal_Show.DrawToBitmap(bitmap, new Rectangle(new Point(0, 0), bitmap.Size));
            int left = (e.PageBounds.Width - bitmap.Width) / 2 - 15;
            e.Graphics.DrawImage(bitmap, left, 0);
        }

        private void Btn_PrintSetup_Click(object sender, EventArgs e)
        {
            try
            {
                pageSetupDialog1.ShowDialog();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btn_Print_Click(object sender, EventArgs e)
        {
            try
            {
                if(printPreviewDialog1.ShowDialog() == DialogResult.OK)
                {
                    printDocument1.Print();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
