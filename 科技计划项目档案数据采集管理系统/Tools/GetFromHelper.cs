using System;
using System.Windows.Forms;
using 科技计划项目档案数据采集管理系统.Cataloguing;
using 科技计划项目档案数据采集管理系统.FirstPage;

namespace 科技计划项目档案数据采集管理系统
{
    /// <summary>
    /// 通过单例模式获取窗体
    /// </summary>
    public class GetFormHelper
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

        private static Frm_FirstPage firstPage;
        public static Frm_FirstPage GetFirstPage(Form form)
        {
            if(firstPage == null || firstPage.IsDisposed)
                firstPage = new Frm_FirstPage(form);
            return firstPage;
        }

        private static Frm_MainFrame mainFrame;
        public static Frm_MainFrame GetMainFrame( Form f2)
        {
            if(mainFrame == null || mainFrame.IsDisposed)
                mainFrame = new Frm_MainFrame(f2);
            return mainFrame;
        }

        private static Frm_Statistics statistics;
        public static Frm_Statistics GetStatistic()
        {
            if(statistics == null || statistics.IsDisposed)
                statistics = new Frm_Statistics();
            return statistics;
        }

        private static Frm_QueryBorrowing borrowing;
        public static Frm_QueryBorrowing GetQueryBorrow(Frm_FirstPage form)
        {
            if(borrowing == null || borrowing.IsDisposed)
                borrowing = new Frm_QueryBorrowing(form);
            return borrowing;
        }

        private static Frm_Download download;
        public static Frm_Download GetFileDownload()
        {
            if(download == null || download.IsDisposed)
                download = new Frm_Download();
            return download;
        }

        private static Frm_ProTypeSelect proTypeSelect;
        public static Frm_ProTypeSelect GetProTypeSelecter(WorkType type, object objId)
        {
            if(proTypeSelect == null || proTypeSelect.IsDisposed)
                proTypeSelect = new Frm_ProTypeSelect(type, objId);
            return proTypeSelect;
        }

        private static Frm_FileList fileList;
        public static Frm_FileList GetFileList(string[] linkString)
        {
            if(fileList == null || fileList.IsDisposed)
                fileList = new Frm_FileList(linkString);
            return fileList;
        }

        private static Frm_AdviceBW AdviceBW;
        internal static Frm_AdviceBW GetAdviceBW(object objId, object objName)
        {
            if(AdviceBW == null || AdviceBW.IsDisposed)
                AdviceBW = new Frm_AdviceBW(objId, objName);
            return AdviceBW;
        }

        private static Frm_ProDetails details;
        internal static Frm_ProDetails GetProDetails(object id)
        {
            if(details == null || details.IsDisposed)
                details = new Frm_ProDetails(id);
            return details;
        }
    }
}
