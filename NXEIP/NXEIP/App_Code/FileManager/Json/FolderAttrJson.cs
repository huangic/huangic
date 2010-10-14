using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NXEIP.JsTree;


/// <summary>
/// 樹狀的目錄JSON
/// </summary>
/// 


namespace NXEIP.FileManager.Json
{
   
    public class FolderAttrJson : JsTreeAttr
    {
        
        public String depid { get; set; }
        public String folderType { get; set; }
    }
}