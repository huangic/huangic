using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;




namespace NXEIP.JsTree
{
    /// <summary>
    /// JsTreeJson 的摘要描述
    /// </summary>
    /// 
    public class JsTreeJson
    {
        public JsTreeJson()
        {
            //
            // TODO: 在此加入建構函式的程式碼
            //
        }

        /// <summary>
        /// 資料
        /// </summary>
        public String data { get; set; }
        
        /// <summary>
        /// 目錄狀態open close
        /// </summary>
        public String state { get; set; }

        /// <summary>
        /// 節點屬性
        /// </summary>
        public JsTreeAttr attr { get; set; }

        /// <summary>
        /// 子項目
        /// </summary>
        public ICollection<JsTreeJson> children { get; set; }
    }
}