using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace NXEIP.DynamicForm
{

    /// <summary>
    /// 要填入的欄位的摘要描述
    /// </summary>
    /// 

    [Serializable]
    public class Column
    {
        public Column()
        {
            //
            // TODO: 在此加入建構函式的程式碼
            //
        }
        /// <summary>
        /// 唯一編號
        /// </summary>
        public String UID { get; set; }
        /// <summary>
        /// 欄位名稱
        /// </summary>
        public String Name { get; set; }
        /// <summary>
        /// 欄位說明
        /// </summary>
        public String Description { get; set; }
        /// <summary>
        /// 是否必填
        /// </summary>
        public bool Required { get; set; }
        /// <summary>
        /// 最小長度
        /// </summary>
        public int MinLength { get; set; }
        /// <summary>
        /// 最大長度
        /// </summary>
        public int MaxLength { get; set; }

        public String Condition { get; set; }
        /// <summary>
        /// 欄位類別
        /// </summary>
        public ColumnType ColumnType { get; set; }
        /// <summary>
        /// 候選詞
        /// </summary>
        public List<String> Items { get; set; }
        /// <summary>
        /// 順序
        /// </summary>
        public int Order { get; set; }

    }

    public enum ColumnType { 
            InputBox,
            TextArea,
            Checkbox,
            RadioButton,
            DropDownList,
            ListBox,
            DateSelector,
            Department
    }

}