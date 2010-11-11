using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NXEIP.JsTree;


namespace NXEIP.Tree.Json
{
    /// <summary>
    /// DepartTreeAttr 的摘要描述
    /// </summary>
    public class DepartTreeAttr : JsTreeAttr
    {
        public DepartTreeAttr()
        {
            //
            // TODO: 在此加入建構函式的程式碼
            //
        }

        public String rel { get; set; }
        //public String nType { get; set; }
        public String nName { get; set; }

    }
}