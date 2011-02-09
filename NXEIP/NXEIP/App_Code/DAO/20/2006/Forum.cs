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
        ///  使用者的權限(00000:管理者,寫入,讀取內容,讀取標題,看的到討論區) 
        /// </summary>
            public String Permission { get; set; }

            /// <summary>
            /// 最後更新日
            /// </summary>
            public DateTime? LastModifyDate { get; set; }

            /// <summary>
            /// 最後發表人
            /// </summary>
            public people LastModifyPeouid { get; set; }

        /// <summary>
        /// 通知的FLAG，
        /// </summary>
            public String NotifyFlag { get; set; }

        /// <summary>
        /// 這個使用者有沒有訂閱此討論區
        /// </summary>
            public bool Subscribe
            {
                get;
                set;
            }


            public String ManagerName { get {
                String str = String.Join(",", this.Manager.Select(x => x.peo_name).ToList());
                
                return String.IsNullOrEmpty(str)?"從缺中":str;
            } }

        /// <summary>
        /// 是否為版主
        /// </summary>
            public bool IsManager {

                get{
                    return this.Permission.Substring(0, 1) == "1";
                }
            }

        /// <summary>
        /// 是否總管理者
        /// </summary>
            public bool IsRoot { get; set; }


            /// <summary>
            /// 寄訊息給版主
            /// </summary>
            /// <returns></returns>
            public void SendNotify(int peo_uid) {


                List<people> admin = this.Manager;
                
                
                //TODO: 個人訊息 (寫錯!!!這個是討論區內才要用的)
                //E公務訊息
                if (NotifyFlag.Substring(2, 1) == "1")
                {
                    //E 公務
                }

                bool sendMsg = false, sendEmail = false;
                //個人訊息
                if (NotifyFlag.Substring(1, 1) == "1")
                {
                    sendEmail = true;
                }

                if (NotifyFlag.Substring(0, 1) == "1")
                {
                    sendEmail = true;
                }


                if (sendEmail || sendMsg)
                {
                    PersonalMessageUtil msgUtil = new PersonalMessageUtil();

                    foreach (var p in admin)
                    {
                        msgUtil.SendMessage("申請討論區", "申請討論區", "", p.peo_uid, peo_uid, sendMsg, sendEmail, false);
                    }

                }
            
            }


            [Flags]
            public enum ForumPermission
            {
                /// <summary>
                /// 沒有權限
                /// </summary>
                NoPermission=0,
                /// <summary>
                /// 讀取討論區
                /// </summary>
                ReadForum = 1,
                /// <summary>
                /// 可以看主題
                /// </summary>
                ReadTopic=2,
                /// <summary>
                /// 可以看內容
                /// </summary>
                ReadContent=4,
                /// <summary>
                /// 可以新增
                /// </summary>
                Write=8,
                /// <summary>
                /// 有管理權限
                /// </summary>
                Manager=16

            }
        /// <summary>
        /// 取權限值
        /// </summary>
        /// <param name="f"></param>
        /// <returns></returns>
            public bool GetPermission(ForumPermission f)
            {
                ForumPermission fp=(ForumPermission)System.Convert.ToInt32(this.Permission,2);        
        
                //做FLAG的判斷
                return (fp & f)==f;
            }
        /// <summary>
            /// 取權限值
        /// </summary>
        /// <param name="permission">權限FLAG(2進位基數)</param>
        /// <param name="f"></param>
        /// <returns></returns>
            public static bool GetPermission(int permission, ForumPermission f) {
                ForumPermission fp = (ForumPermission)permission;

                //做FLAG的判斷
                return (fp & f) == f;
            }
        /// <summary>
            /// 取權限值
        /// </summary>
            /// <param name="permission">權限FLAG("0001")</param>
        /// <param name="f"></param>
        /// <returns></returns>
            public static bool GetPermission(String permission, ForumPermission f)
            {
                ForumPermission fp = (ForumPermission)System.Convert.ToInt32(permission, 2);

                //做FLAG的判斷
                return (fp & f) == f;
            }
    }
}