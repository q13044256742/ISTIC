using DevExpress.XtraEditors;

namespace 科技计划项目档案数据采集管理系统.KyoControl
{
    class KyoButton : SimpleButton
    {
        public KyoButton()
        {
            LookAndFeel.UseDefaultLookAndFeel = false;
            LookAndFeel.SkinName = "McSkin";
            AutoSize = false;
        }
    }
}
