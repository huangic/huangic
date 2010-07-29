using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// FileJSON 的摘要描述
/// </summary>
public class FolderJSON
{
    public FolderJSON() {
        this.attr = new AttrJson();
    }

     public String data{get;set;}
     public String state { get; set; }
     public AttrJson attr { get; set; }

     public ICollection<FolderJSON> children { get; set; }
        
}

public class AttrJson{
    public String id{get;set;}
}
