using System;

namespace 科技计划项目档案数据采集管理系统
{
    /// <summary>
    /// 对象提交状态
    /// </summary>
    enum ObjectSubmitStatus
    {
        /// <summary>
        /// 默认
        /// </summary>
        Default = 0,
        /// <summary>
        /// 未提交/未完成
        /// </summary>
        NonSubmit = 1,
        /// <summary>
        /// 已提交/已完成
        /// </summary>
        SubmitSuccess = 2,
        /// <summary>
        /// 已返工
        /// </summary>
        Back = 3
    }
    /// <summary>
    /// 当前操作对象类型
    /// </summary>
    public enum ControlType
    {
        /// <summary>
        /// 默认
        /// </summary>
        Default = 0,
        /// <summary>
        /// 计划
        /// </summary>
        Plan = 1,
        /// <summary>
        /// 计划-项目
        /// </summary>
        Plan_Project = 2,
        /// <summary>
        /// 计划-课题
        /// </summary>
        Plan_Topic = 3,
        /// <summary>
        /// 计划-项目-课题
        /// </summary>
        Plan_Project_Topic = 4,
        /// <summary>
        /// 计划-课题-子课题
        /// </summary>
        Plan_Topic_Subtopic = 5,
        /// <summary>
        /// 计划-项目-课题-子课题
        /// </summary>
        Plan_Project_Topic_Subtopic = 6,
        /// <summary>
        /// 重大专项
        /// </summary>
        Imp = 7,
        /// <summary>
        /// 重大专项信息
        /// </summary>
        Imp_Sub = 8,
        /// <summary>
        /// 重点研发
        /// </summary>
        Imp_Dev = 9,
        /// <summary>
        /// 重点研发信息
        /// </summary>
        Imp_Dev_Sub = 10
    }
    /// <summary>
    /// 文件归档状态
    /// </summary>
    enum GuiDangStatus
    {
        /// <summary>
        /// 默认
        /// </summary>
        Default = 0,
        /// <summary>
        /// 未归档
        /// </summary>
        NonGuiDang = 1,
        /// <summary>
        /// 已归档
        /// </summary>
        GuiDangSuccess = 2
    }
    /// <summary>
    /// 加工状态
    /// </summary>
    enum WorkStatus
    {
        /// <summary>
        /// 默认
        /// </summary>
        Default = 0,
        /// <summary>
        /// 未加工
        /// </summary>
        NonWork = 1,
        /// <summary>
        /// 已加工
        /// </summary>
        WorkSuccess = 2,
        /// <summary>
        /// 已返工
        /// </summary>
        BackWork = 3,
        /// <summary>
        /// [返工]已加工
        /// </summary>
        WorkSuccessFromBack = 4
    }
    /// <summary>
    /// 加工类型
    /// </summary>
    public enum WorkType
    {
        /// <summary>
        /// 默认
        /// </summary>
        Default = 0,
        /// <summary>
        /// 纸本加工
        /// </summary>
        PaperWork = 1,
        /// <summary>
        /// 光盘加工
        /// </summary>
        CDWork = 2,
        /// <summary>
        /// 父级【项目/课题】加工
        /// </summary>
        ProjectWork = 3,
        /// <summary>
        /// 子级【课题/子课题】加工
        /// </summary>
        SubjectWork = 4
    }
    /// <summary>
    /// 加工登记 - 实体对象
    /// </summary>
    class WorkRegistration
    {
        private object wrId;
        private WorkStatus wrStauts;
        private object wrTrpId;
        private WorkType wrType;
        private DateTime wrStartDate;
        private DateTime wrSubmitDate;
        private object wrObjId;
        private ObjectSubmitStatus submitStatus;
        private ReceiveStatus receiveStatus;
        private string sourceId;

        public object WrId { get => wrId; set => wrId = value; }
        public DateTime WrStartDate { get => wrStartDate; set => wrStartDate = value; }
        public DateTime WrSubmitDate { get => wrSubmitDate; set => wrSubmitDate = value; }
        public object WrObjId { get => wrObjId; set => wrObjId = value; }
        public object WrTrpId { get => wrTrpId; set => wrTrpId = value; }
        public WorkStatus WrStauts { get => wrStauts; set => wrStauts = value; }
        public WorkType WrType { get => wrType; set => wrType = value; }
        public string SourceId { get => sourceId; set => sourceId = value; }
        public ObjectSubmitStatus SubmitStatus { get => submitStatus; set => submitStatus = value; }
        public ReceiveStatus ReceiveStatus { get => receiveStatus; set => receiveStatus = value; }
    }
}
