namespace 科技计划项目档案数据采集管理系统.KyoControl
{
    class KyoComboBox: DevExpress.XtraEditors.LookUpEdit
    {
        private string DisplayMemberValue;
        private string ValueMemberValue;

        public KyoComboBox()
        {
            Properties.LookAndFeel.SkinName = "McSkin";
            Properties.ShowHeader = false;
        }

        public string DisplayMember
        {
            get => DisplayMember;
            set
            {
                DisplayMemberValue = value;
                Properties.DisplayMember = value;
                Properties.Columns.Add(
                   new DevExpress.XtraEditors.Controls.LookUpColumnInfo(value)
                );
            }
        }

        public string ValueMember
        {
            get => ValueMemberValue;
            set
            {
                ValueMemberValue = value;
                Properties.ValueMember = value;
            }
        }
    }
}
