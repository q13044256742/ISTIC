using System;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace 科技计划项目档案数据采集管理系统
{
    public partial class Frm_BoxList : DevExpress.XtraEditors.XtraForm
    {
        public string proCode;
        public string name;
        public string code;
        public string gcCode;
        public DataTable dataTable;
        public Frm_BoxList()
        {
            InitializeComponent();
        }

        private void Frm_BoxList_Load(object sender, System.EventArgs e)
        {
            lbl_proCode.Text = proCode;
            lbl_Name.Text = name;
            lbl_Code.Text = code;
            lbl_GC.Text = gcCode;

            dgv_DataList.Rows.Clear();
            for(int i = 0; i < dataTable.Rows.Count; i++)
            {
                int index = dgv_DataList.Rows.Add();
                dgv_DataList.Rows[index].Cells["fb_id"].Value = i + 1;
                dgv_DataList.Rows[index].Cells["fb_code"].Value = dataTable.Rows[i]["pfl_code"];
                dgv_DataList.Rows[index].Cells["fb_name"].Value = dataTable.Rows[i]["pfl_name"];
                dgv_DataList.Rows[index].Cells["fb_page"].Value = dataTable.Rows[i]["pfl_pages"];
                dgv_DataList.Rows[index].Cells["fb_count"].Value = dataTable.Rows[i]["pfl_amount"];
                dgv_DataList.Rows[index].Cells["fb_remark"].Value = dataTable.Rows[i]["pfl_remark"];
            }
        }
        private string GetValue(object value) => value == null ? string.Empty : value.ToString();

        private void btn_PrintSetup_Click(object sender, EventArgs e)
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

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Bitmap bitmap = new Bitmap(pal_Show.Width, pal_Show.Height, PixelFormat.Format24bppRgb);
            Graphics graphics = Graphics.FromImage(bitmap);
            graphics.Clear(Color.White);
            pal_Show.DrawToBitmap(bitmap, new Rectangle(new Point(0, 0), bitmap.Size));

            e.Graphics.DrawImage(bitmap, 0f, 0f);
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

        private void btn_Font_Click(object sender, EventArgs e)
        {
            fontDialog1.ShowColor = false;
            fontDialog1.ShowEffects = false;
            if(fontDialog1.ShowDialog() == DialogResult.OK)
            {
                lbl_Code.Font = fontDialog1.Font;
                lbl_GC.Font = fontDialog1.Font;
                lbl_Name.Font = fontDialog1.Font;
                lbl_proCode.Font = fontDialog1.Font;
            }
        }
    }
}
