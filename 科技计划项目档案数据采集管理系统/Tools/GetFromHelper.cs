namespace 科技计划项目档案数据采集管理系统
{
    /// <summary>
    /// 通过单例模式获取窗体
    /// </summary>
    public class GetFromHelper
    {
        private static Frm_MyWorkQT myWorkQT;
        public static Frm_MyWorkQT GetMyWorkQT(WorkType workType, object objId, object wmid, ControlType controlType, bool isReadOnly)
        {
            if(myWorkQT == null || myWorkQT.IsDisposed)
                myWorkQT = new Frm_MyWorkQT(workType, objId, wmid, controlType, isReadOnly);
            return myWorkQT;
        }

        private static Frm_OtherDoc otherDoc;
        public static Frm_OtherDoc GetOtherDoc(object objid)
        {
            if(otherDoc == null || otherDoc.IsDisposed)
                otherDoc = new Frm_OtherDoc(objid);
            return otherDoc;
        }

        private static Frm_MyWork myWork;
        public static Frm_MyWork GetMyWork(WorkType workType, object planId, object objId, ControlType controlType, bool isBacked, bool isReadOnly)
        {
            if(myWork == null || myWork.IsDisposed)
                myWork = new Frm_MyWork(workType, planId, objId, controlType, isBacked, isReadOnly);
            return myWork;
        }

        public static Frm_MyWorkQT GetMyWorkQT(WorkType type, object objid, object wmid, ControlType imp)
        {
            if(myWorkQT == null || myWorkQT.IsDisposed)
                myWorkQT = new Frm_MyWorkQT(type, objid, wmid, imp);
            return myWorkQT;
        }

        private static Frm_Login loginForm;
        public static Frm_Login GetLoginForm()
        {
            if(loginForm == null || loginForm.IsDisposed)
                loginForm = new Frm_Login();
            return loginForm;
        }

    }
}
