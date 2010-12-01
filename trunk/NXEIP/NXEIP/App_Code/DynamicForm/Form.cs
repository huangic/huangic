using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NXEIP.DynamicForm
{
    /// <summary>
    /// 動態FORM表單
    /// </summary>
    /// 
    [Serializable]
    public class Form
    {
        public Form()
        {
            //
            // TODO: 在此加入建構函式的程式碼
            //
        }

        /// <summary>
        /// 表單編號
        /// </summary>
        public String Id { get; set; }


        /// <summary>
        /// 表單名稱
        /// </summary>
        public String Name { get; set; }

        /// <summary>
        /// 表單狀態
        /// </summary>
        public String Status { get; set; }



        /// <summary>
        /// 描述
        /// </summary>
        public String Description { get; set; }

        /// <summary>
        /// 建立時間
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 建立人
        /// </summary>
        public string CreateUser { get; set; }

        public String CreareUserNO { get; set; }

        /// <summary>
        /// 承辦人
        /// </summary>
        public String HandleUser { get; set; }

        public String HandleUserNO { get; set; }

        /// <summary>
        /// 上架時間
        /// </summary>
        public DateTime? PublicStartTime { get; set; }
        /// <summary>
        /// 下架時間
        /// </summary>
        public DateTime? PublicEndTime { get; set; }


        /// <summary>
        /// 開始時間
        /// </summary>
        public DateTime? PostStartTime { get; set; }
        /// <summary>
        /// 結束時間
        /// </summary>
        public DateTime? PostEndTime { get; set; }


        /// <summary>
        /// 表單欄位
        /// </summary>
        public List<Column> Columns { get; set; }



        public Column GetColumn(String uid){
            return this.Columns.Find(x => x.UID==uid);
        }

        public void AddColumn(Column c) {
            this.Columns.Add(c);
        }

        public List<String> Footer { get; set; }

       
    }
}