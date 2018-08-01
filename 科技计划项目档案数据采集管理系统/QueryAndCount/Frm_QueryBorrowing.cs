using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace 科技计划项目档案数据采集管理系统.QueryAndCount
{
    public partial class Frm_QueryBorrowing : DevExpress.XtraEditors.XtraForm
    {
        public Frm_QueryBorrowing()
        {
            InitializeComponent();
        }

        private void Frm_QueryBorrowing_Load(object sender, EventArgs e)
        {


        }

        private void navigationPane1_StateChanged(object sender, DevExpress.XtraBars.Navigation.StateChangedEventArgs e)
        {
            DevExpress.XtraBars.Navigation.NavigationPane panel = (sender as DevExpress.XtraBars.Navigation.NavigationPane);
            if(e.State == DevExpress.XtraBars.Navigation.NavigationPaneState.Collapsed)
            {
                panel.State = DevExpress.XtraBars.Navigation.NavigationPaneState.Default;
            }
        }
    }
}
