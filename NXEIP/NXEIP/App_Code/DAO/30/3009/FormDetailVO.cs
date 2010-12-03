using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Entity;


namespace NXEIP.DAO
{
    /// <summary>
    /// FormDetail 的摘要描述
    /// </summary>
    public class FormDetailVO
    {
        public FormDetailVO()
        {
            //
            // TODO: 在此加入建構函式的程式碼
            //
        }
        /// <summary>
        /// 主表單
        /// </summary>
        public form01 Form { get; set; }
        /// <summary>
        /// 提交表單
        /// </summary>
        public form02 Submit { get; set; }
    }
}