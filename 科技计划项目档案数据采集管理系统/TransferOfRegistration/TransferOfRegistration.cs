using System;

namespace 科技计划项目档案数据采集管理系统
{
    /// <summary>
    /// 批次提交状态
    /// </summary>
    enum SubmitStatus
    {
        /// <summary>
        /// 默认
        /// </summary>
        Default = 0,
        /// <summary>
        /// 未提交
        /// </summary>
        NonSubmit = 1,
        /// <summary>
        /// 已提交
        /// </summary>
        SubmitSuccess = 2
    }
    /// <summary>
    /// 批次/项目/课题/子课题 领取状态
    /// </summary>
    enum ReceiveStatus
    {
        /// <summary>
        /// 默认
        /// </summary>
        Default = 0,
        /// <summary>
        /// 未领取
        /// </summary>
        NonReceive = 1,
        /// <summary>
        /// 已领取
        /// </summary>
        ReceiveSuccess = 2,
    }
    class TransferOfRegist
    {
        private string id;
        private string batchName;
        private string batchCode;
        private string sourceUnit;
        private DateTime transferTime;
        private string receive;
        private string giver;
        private string remark;
        private string fileUpload;
        private SubmitStatus submitStrtus;
        private WorkStatus workStatus;
        private int trpCdAmount;

        public string BatchName { get => batchName; set => batchName = value; }
        public string BatchCode { get => batchCode; set => batchCode = value; }
        public string SourceUnit { get => sourceUnit; set => sourceUnit = value; }
        public DateTime TransferTime { get => transferTime; set => transferTime = value; }
        public string Receive { get => receive; set => receive = value; }
        public string Giver { get => giver; set => giver = value; }
        public string Remark { get => remark; set => remark = value; }
        public string FileUpload { get => fileUpload; set => fileUpload = value; }
        public string Id { get => id; set => id = value; }
        public int TrpCdAmount { get => trpCdAmount; set => trpCdAmount = value; }
        internal SubmitStatus SubmitStrtus { get => submitStrtus; set => submitStrtus = value; }
        internal WorkStatus WorkStatus { get => workStatus; set => workStatus = value; }
    }
}
