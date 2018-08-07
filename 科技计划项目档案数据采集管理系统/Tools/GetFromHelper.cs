using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using 科技计划项目档案数据采集管理系统.Cataloguing;
using 科技计划项目档案数据采集管理系统.KyoControl;
using 科技计划项目档案数据采集管理系统.Tools;

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

        private static Frm_Query query;
        /// <summary>
        /// 工作量统计
        /// </summary>
        /// <param name="form">父窗体</param>
        public static Frm_Query GetQueryFrom()
        {
            if(query == null || query.IsDisposed)
                query = new Frm_Query();
            return query;
        }
    }
}
