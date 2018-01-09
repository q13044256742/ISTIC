using System;

namespace 科技计划项目档案数据采集管理系统
{
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
        /// <summary>
        /// 批次状态
        /// 1：未提交
        /// 2：已提交
        /// </summary>
        private int trpStatus;
        /// <summary>
        /// 批次下的光盘数
        /// </summary>
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
        public int TrpStatus { get => trpStatus; set => trpStatus = value; }
    }
}
