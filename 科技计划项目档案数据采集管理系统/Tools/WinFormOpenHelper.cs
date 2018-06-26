using System;

namespace 科技计划项目档案数据采集管理系统
{
    /// <summary>
    /// 此参数指定程序窗口的初始显示方式
    /// </summary>
    public enum ShowWindowCommands : int
    {
        /// <summary>
        /// 隐藏窗口，活动状态给令一个窗口
        /// </summary>
        SW_HIDE = 0,
        /// <summary>
        /// 用原来的大小和位置显示一个窗口，同时令其进入活动状态(与SW_RESTORE相同)
        /// </summary>
        SW_SHOWNORMAL = 1,
        /// <summary>
        /// 用原来的大小和位置显示一个窗口，同时令其进入活动状态(与SW_RESTORE相同)
        /// </summary>
        SW_NORMAL = 1,
        /// <summary>
        /// 最小化窗口，并将其激活
        /// </summary>
        SW_SHOWMINIMIZED = 2,
        /// <summary>
        /// 最大化窗口，并将其激活
        /// </summary>
        SW_SHOWMAXIMIZED = 3,
        /// <summary>
        /// 最大化窗口，并将其激活
        /// </summary>
        SW_MAXIMIZE = 3,
        /// <summary>
        ///  用最近的大小和位置显示一个窗口，同时不改变活动窗口
        /// </summary>
        SW_SHOWNOACTIVATE = 4,
        /// <summary>
        /// 用当前的大小和位置显示一个窗口，同时令其进入活动状态
        /// </summary>
        SW_SHOW = 5,
        /// <summary>
        /// 最小化窗口，活动状态给令一个窗口
        /// </summary>
        SW_MINIMIZE = 6,
        /// <summary>
        /// 最小化一个窗口，同时不改变活动窗口
        /// </summary>
        SW_SHOWMINNOACTIVE = 7,
        /// <summary>
        /// 用当前的大小和位置显示一个窗口，不改变活动窗口
        /// </summary>
        SW_SHOWNA = 8,
        /// <summary>
        /// 用原来的大小和位置显示一个窗口，同时令其进入活动状态
        /// </summary>
        SW_RESTORE = 9,
        SW_SHOWDEFAULT = 10,
        SW_MAX = 10
    }
    class WinFormOpenHelper
    {
        /// <summary>
        /// 打开文件或链接
        /// </summary>
        /// <param name="hwnd">用于指定父窗口句柄。当函数调用过程出现错误时，它将作为Windows消息窗口的父窗口。</param>
        /// <param name="lpOperation">用于指定要进行的操作
        /// <para>“open” 执行由lpFile参数指定的程序，或打开由lpFile参数指定的文件或文件夹</para>
        /// <para>“print” 打印由lpFile参数指定的文件</para>
        /// <para>“explore” 浏览由lpFile参数指定的文件夹</para>
        /// <para>当参数设为NULL时，表示执行默认操作“open”</para></param>
        /// <param name="lpFile">用于指定要打开的文件名、要执行的程序文件名或要浏览的文件夹名。</param>
        /// <param name="lpParameters">若lpFile参数是一个可执行程序，则此参数指定命令行参数，否则此参数应为NULL.</param>
        /// <param name="lpDirectory">用于指定默认目录.</param>
        /// <param name="nShowCmd">若lpFile参数是一个可执行程序，则此参数指定程序窗口的初始显示方式，否则此参数应设置为0。</param>
        /// <returns>执行成功会返回应用程序句柄,
        /// 返回的HINSTANCE可以将它转换为一个整数(%d)，并比较它的值大于还是小于32或比较它的错误代码 
        /// 返回值大于32表示执行成功 返回值小于32表示执行错误
        /// 返回值可能的错误有: = 0 {内存不足}
        /// ERROR_FILE_NOT_FOUND = 2; {文件名错误 
        /// ERROR_PATH_NOT_FOUND = 3; {路径名错误
        /// ERROR_BAD_FORMAT = 11; {EXE 文件无效}
        /// SE_ERR_SHARE = 26; {发生共享错误}
        /// SE_ERR_ASSOCINCOMPLETE = 27; {文件名不完全或无效}
        /// SE_ERR_DDETIMEOUT = 28; {超时}
        /// SE_ERR_DDEFAIL = 29; {DDE 事务失败}
        /// SE_ERR_DDEBUSY = 30; {正在处理其他 DDE 事务而不能完成该 DDE 事务}
        /// SE_ERR_NOASSOC = 31; {没有相关联的应用程序}
        /// </returns>
        public static IntPtr OpenWinForm(int hwnd, string lpOperation, string lpFile, string lpParameters, string lpDirectory, ShowWindowCommands nShowCmd)
        {
            return ShellExecute(hwnd, lpOperation, lpFile, lpParameters, lpDirectory, (int)nShowCmd);
        }

        [System.Runtime.InteropServices.DllImport("shell32.dll")]
        public static extern IntPtr ShellExecute(int hwnd, string lpOperation, string lpFile, string lpParameters, string lpDirectory, int nShowCmd);
    }
}
