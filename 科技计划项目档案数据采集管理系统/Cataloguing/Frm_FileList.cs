using DevExpress.XtraEditors;
using System.Drawing;
using System.Windows.Forms;

namespace 科技计划项目档案数据采集管理系统.Cataloguing
{
    public partial class Frm_FileList : XtraForm
    {
        public Frm_FileList(string[] items)
        {
            InitializeComponent();
            foreach(string item in items)
            {
                if(!string.IsNullOrEmpty(item))
                {
                    listBox1.Items.Add(new EntityList()
                    {
                        Name = System.IO.Path.GetFileName(item),
                        Path = item,
                    });
                }
            }
        }

        private void listBox1_MouseDoubleClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            EntityList item = (EntityList)listBox1.SelectedItem;

            if(XtraMessageBox.Show("是否打开文件？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk) == DialogResult.Yes)
            {
                if(System.IO.File.Exists(item.Path))
                {
                    Hide();
                    WinFormOpenHelper.OpenWinForm(0, "open", item.Path, null, null, ShowWindowCommands.SW_NORMAL);
                }
                else
                    XtraMessageBox.Show("文件不存在。");
            }
        }

        private void Frm_FileList_Load(object sender, System.EventArgs e)
        {
            listBox1.DrawMode = DrawMode.OwnerDrawVariable;
        }

        //自绘Item，使其视觉效果更好
        private void ListBoxGroupRange_DrawItem(object sender, DrawItemEventArgs e)
        {
            e.Graphics.FillRectangle(new SolidBrush(e.BackColor), e.Bounds);
            if(e.Index >= 0)
            {
                StringFormat sStringFormat = new StringFormat
                {
                    LineAlignment = StringAlignment.Center
                };
                e.Graphics.DrawString(((ListBox)sender).Items[e.Index].ToString(), e.Font, new SolidBrush(e.ForeColor), e.Bounds, sStringFormat);
            }
            e.DrawFocusRectangle();
        }

        private void ListBoxGroupRange_MeasureItem(object sender, MeasureItemEventArgs e)
        {
            e.ItemHeight = e.ItemHeight + 12;
        }

    }

    class EntityList
    {
        private string name;
        private string path;

        public string Name { get => name; set => name = value; }
        public string Path { get => path; set => path = value; }

        public override string ToString()
        {
            return Name;
        }
    }
}
