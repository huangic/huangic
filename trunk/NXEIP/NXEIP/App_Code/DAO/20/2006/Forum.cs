using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Entity;




namespace NXEIP.DAO
{
    /// <summary>
    /// Forum 討論區列表使用
    /// </summary>
    public class Forum
    {
        public Forum()
        {
            //
            // TODO: 在此加入建構函式的程式碼
            //
        }


        

        

        /// <summary>
        /// 討論區編號
        /// </summary>
            public int Id { get; set; }


        /// <summary>
        /// 討論區名稱
        /// </summary>
            public String Name { get; set; }
        /// <summary>
        /// 英文名稱
        /// </summary>
            public String EngName { get; set; }

        /// <summary>
            /// 討論區說明
        /// </summary>
            public String Desc { get; set; }

        /// <summary>
        /// 版主編號
        /// </summary>
            public List<people> Manager { get; set; }


        /// <summary>
        /// 點擊統計
        /// </summary>
            public int ClickCount { get; set; }

        /// <summary>
        /// 回文統計
        /// </summary>
            public int RelayCount { get; set; }

        /// <summary>
        /// 版型
        /// </summary>
            public String Layout { get; set; }

        /// <summary>
        ///  使用者的權限(00000:管理,寫入,讀取內容,讀取標題,看的到討論區) 
        /// </summary>
            public String Permission { get; set; }


        /// <summary>
        /// 這個使用者有沒有訂閱此討論區
        /// </summary>
            public String Subscribe
            {
                get;
                set;
            }
    }
}