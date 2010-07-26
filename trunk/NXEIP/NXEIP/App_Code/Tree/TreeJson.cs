using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;

/// <summary>
/// TreeJson 的摘要描述
/// </summary>
public class TreeJson
{
    public TreeJson()
    {
        //
        // TODO: 在此加入建構函式的程式碼
        //
    }

   public String text{ get;set;}
   public String id { get; set; }
   public bool hasChildren { get; set; }
   public bool expanded { get; set; }
   public String classes { get; set; }
   public ArrayList children { get; set; }
}
