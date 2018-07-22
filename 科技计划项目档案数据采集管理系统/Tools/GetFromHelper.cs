using System;

namespace 科技计划项目档案数据采集管理系统.Tools
{
    /// <summary>
    /// 通过单例模式获取窗体
    /// </summary>
    class GetFromHelper
    {
        private static Frm_MyWorkQT myWorkQT;
        public static Frm_MyWorkQT GetMyWorkQT(WorkType workType, object objId, object wmid, ControlType controlType)
        {
            if(myWorkQT == null || myWorkQT.IsDisposed)
                myWorkQT = new Frm_MyWorkQT(workType, objId, wmid, controlType);
            return myWorkQT;
        }

        private static Frm_OtherDoc otherDoc;
        public static Frm_OtherDoc GetOtherDoc(object objid)
        {
            if(otherDoc == null || otherDoc.IsDisposed)
                otherDoc = new Frm_OtherDoc(objid);
            return otherDoc;
        }
    }
}
