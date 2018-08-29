using System;
using System.Collections.Generic;
using System.Drawing.Printing;

namespace 科技计划项目档案数据采集管理系统
{
    class LocalPrinter
    {
        private static PrintDocument fPrintDocument = new PrintDocument();
        //获取本机默认打印机名称
        public static String DefaultPrinter()
        {
            return fPrintDocument.PrinterSettings.PrinterName;
        }
        public static List<String> GetLocalPrinters()
        {
            List<String> fPrinters = new List<String>();
            fPrinters.Add(DefaultPrinter()); //默认打印机始终出现在列表的第一项
            foreach(String fPrinterName in PrinterSettings.InstalledPrinters)
            {
                if(!fPrinters.Contains(fPrinterName))
                {
                    fPrinters.Add(fPrinterName);
                }
            }
            return fPrinters;
        }
    }
    class Externs
    {
        [System.Runtime.InteropServices.DllImport("winspool.drv")]
        public static extern bool SetDefaultPrinter(String Name); //调用win api将指定名称的打印机设置为默认打印机
    }
}
