using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// PermissionObj 的摘要描述
/// </summary>
/// 

namespace NXEIP.FileManager
{
    public class PermissionObj
    {
        public PermissionObj()
        {
            //
            // TODO: 在此加入建構函式的程式碼
            //
        }



        public int id { get; set; }
        public string type { get; set; }
        public string value { get; set; }
        public int d03_no { get; set; }
    }
}