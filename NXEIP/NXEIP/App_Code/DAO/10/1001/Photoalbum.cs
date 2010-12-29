using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Entity;



namespace NXEIP.DAO
{
    /// <summary>
    /// Photoalbum 的摘要描述
    /// </summary>
    public class Photoalbum
    {
       public album Album{get;set;}
       public photo Cover{get;set;}
       public int Count { get; set; }
        
        public Photoalbum()
        {
            //
            // TODO: 在此加入建構函式的程式碼
            //

           

        }
    }
}