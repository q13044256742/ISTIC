namespace 科技计划项目档案数据采集管理系统
{
    /// <summary>
    /// 质检状态
    /// </summary>
    public enum QualityStatus
    {
        /// <summary>
        /// 默认
        /// </summary>
        Default = 0,
        /// <summary>
        /// 待质检
        /// </summary>
        NonQuality = 1,
        /// <summary>
        /// 已质检
        /// </summary>
        QualitySuccess = 2,
        /// <summary>
        /// 质检通过
        /// </summary>
        QualityFinish = 3
    }
    class QualityTesting
    {
    }
}
