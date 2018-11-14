using System;
using System.Windows.Forms;

namespace 科技计划项目档案数据采集管理系统
{
    public partial class Frm_SpecialSymbol : DevExpress.XtraEditors.XtraForm
    {
        /// <summary>
        /// 选择的字符
        /// </summary>
        public string VALUE;
        private TextBox control;
        public Frm_SpecialSymbol(TextBox control)
        {
            InitializeComponent();
            this.control = control;
        }

        private void Frm_SpecialSymbol_Load(object sender, EventArgs e)
        {
            LoadNormal();
        }

        private void LoadNormal()
        {
            lsv_Normal.Items.AddRange(new ListViewItem[]
            {
                new ListViewItem("\x3C"),
                new ListViewItem("\x3E"),
                new ListViewItem("\x2264"),
                new ListViewItem("\x2265"),
                new ListViewItem("\xB1"),
                new ListViewItem("\x2260"),
                new ListViewItem("\xF7"),
                new ListViewItem("\x00D7"),
                new ListViewItem("\x2212"),
                new ListViewItem("\x221A"),
                new ListViewItem("\x221E"),
            });
            lsv_Top.Items.AddRange(new ListViewItem[]
            {
                new ListViewItem("\x2070"),
                new ListViewItem("\x00B9"),
                new ListViewItem("\x00B2"),
                new ListViewItem("\x00B3"),
                new ListViewItem("\x2074"),
                new ListViewItem("\x2075"),
                new ListViewItem("\x2076"),
                new ListViewItem("\x2077"),
                new ListViewItem("\x2078"),
                new ListViewItem("\x2079"),
                new ListViewItem("\x207A"),
                new ListViewItem("\x207B"),
                new ListViewItem("\x207C"),
                new ListViewItem("\x207D"),
                new ListViewItem("\x207E"),
                new ListViewItem("\x2071"),
                new ListViewItem("\x207F"),
            });
            lsv_Bottom.Items.AddRange(new ListViewItem[]
            {
                new ListViewItem("\x2080"),
                new ListViewItem("\x2081"),
                new ListViewItem("\x2082"),
                new ListViewItem("\x2083"),
                new ListViewItem("\x2084"),
                new ListViewItem("\x2085"),
                new ListViewItem("\x2086"),
                new ListViewItem("\x2087"),
                new ListViewItem("\x2088"),
                new ListViewItem("\x2089"),
                new ListViewItem("\x208A"),
                new ListViewItem("\x208B"),
                new ListViewItem("\x208C"),
                new ListViewItem("\x208D"),
                new ListViewItem("\x208E"),
            });
        }

        private void LSV_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListView view = sender as ListView;
            int count = view.SelectedItems.Count;
            if(count > 0)
            {
                string _str = view.SelectedItems[0].Text;
                lbl_Temp.Text = $"A{_str}";
                VALUE = _str;
            }
        }

        private void btn_Sure_Click(object sender, EventArgs e)
        {
            if(control != null && VALUE != null)
            {
                string value = control.Text;
                int index = control.SelectionStart;
                value = value.Insert(index, VALUE);
                control.Text = value;
                control.SelectionStart = index;
            }
            Close();
        }
    }
}
