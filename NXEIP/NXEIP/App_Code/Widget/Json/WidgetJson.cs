using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;





namespace NXEIP.Widget.Json
{
    /// <summary>
    /// WidgetJson 的摘要描述
    /// </summary>
    public class WidgetJson
    {
        //此頁面使用的SESSION;
        public String SessionName { get; set; }
        //此頁面使用的編修用SESSION
        public String SessionTmpName { get;set; } 
        
        //UID
        public String Uid { get; set; }
        //頁面種類
        public String PageType { get; set; }
    }
}