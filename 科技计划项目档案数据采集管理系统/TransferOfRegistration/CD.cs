using System;

namespace 科技计划项目档案数据采集管理系统
{
    class CD
    {
        private string trcId;
        private string trcName;
        private string trcCode;
        private string trpId;
        private string trcComSource;
        private int trcProjecAmount;
        private int trcSubjectAmount;
        private DateTime trcCreateDate;
        private int trcPaperAmount;
        private int trcElectronicAmount;
        private DateTime trcFinishDate;
        private string trcPeople;
        private DateTime trpHandleTime;
        private string trcRemark;

        public string TrcId { get => trcId; set => trcId = value; }
        public string TrcName { get => trcName; set => trcName = value; }
        public string TrcCode { get => trcCode; set => trcCode = value; }
        public string TrcComSource { get => trcComSource; set => trcComSource = value; }
        public int TrcProjecAmount { get => trcProjecAmount; set => trcProjecAmount = value; }
        public int TrcSubjectAmount { get => trcSubjectAmount; set => trcSubjectAmount = value; }
        public DateTime TrcCreateDate { get => trcCreateDate; set => trcCreateDate = value; }
        public int TrcPaperAmount { get => trcPaperAmount; set => trcPaperAmount = value; }
        public int TrcElectronicAmount { get => trcElectronicAmount; set => trcElectronicAmount = value; }
        public DateTime TrcFinishDate { get => trcFinishDate; set => trcFinishDate = value; }
        public string TrcPeople { get => trcPeople; set => trcPeople = value; }
        public DateTime TrpHandleTime { get => trpHandleTime; set => trpHandleTime = value; }
        public string TrcRemark { get => trcRemark; set => trcRemark = value; }
        public string TrpId { get => trpId; set => trpId = value; }
    }
}
