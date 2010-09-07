using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FileManager;
using Entity;

/// <summary>
/// EntityFolderJSON 的摘要描述
/// 資料表Doc01所對應的JSON欄位
/// </summary>
public class EntityFolderJSON:FolderJSON
{
    public EntityFolderJSON()
    {
        
    }

    public EntityFolderJSON(doc01 doc){
        this.data = doc.d01_name;
        if (doc.d01_son == 1)
        {

            this.state = "closed";
        }
        this.attr.id = doc.d01_no.ToString();

    }
}
