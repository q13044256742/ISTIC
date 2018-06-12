using DevExpress.XtraEditors.Controls;

namespace 科技计划项目档案数据采集管理系统.Tools
{
    class LocalizationCHS: Localizer
    {
        public override string GetLocalizedString(StringId id)
        {
            switch(id)
            {
                case StringId.XtraMessageBoxCancelButtonText:
                    return "取消";
                case StringId.XtraMessageBoxOkButtonText:
                    return "确定";
                case StringId.XtraMessageBoxYesButtonText:
                    return "是";
                case StringId.XtraMessageBoxNoButtonText:
                    return "否";
                case StringId.XtraMessageBoxIgnoreButtonText:
                    return "忽略";
                case StringId.XtraMessageBoxAbortButtonText:
                    return "中止";
                case StringId.XtraMessageBoxRetryButtonText:
                    return "重试";
                default:
                    return base.GetLocalizedString(id);
            }
        }
    }
}
