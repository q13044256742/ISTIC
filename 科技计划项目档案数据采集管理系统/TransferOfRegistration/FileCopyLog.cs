namespace 科技计划项目档案数据采集管理系统
{
    class FileCopyLog
    {
        private string errFileName;
        private string errReason;

        public FileCopyLog(string errFileName, string errReason)
        {
            this.errFileName = errFileName;
            this.errReason = errReason;
        }

        public string ErrFileName { get => errFileName; set => errFileName = value; }
        public string ErrReason { get => errReason; set => errReason = value; }
    }
}
