using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace 科技计划项目档案数据采集管理系统.KyoControl
{
    class KyoComboBox: DevExpress.XtraEditors.ComboBoxEdit
    {
        public KyoComboBox()
        {
            Properties.LookAndFeel.UseDefaultLookAndFeel = false;
            Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            Properties.DropDownItemHeight = 25;
            Properties.LookAndFeel.SkinName = "McSkin";
        }
    }
}
