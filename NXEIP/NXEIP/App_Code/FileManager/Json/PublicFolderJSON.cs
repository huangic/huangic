using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NXEIP.FileManager;
using Entity;
using NXEIP.JsTree;


namespace NXEIP.FileManager.Json 
{
    /// <summary>
    /// EntityFolderJSON 的摘要描述
    /// 資料表Doc01所對應的JSON欄位
    /// </summary>
    /// 
    public class PublicFolderJSON : JsTreeJson
    {
        public PublicFolderJSON()
        {

        }

        public PublicFolderJSON(people people) {
            this.data = people.peo_name;

            this.state = "closed";

            PublicFolderAttrJson attr = new PublicFolderAttrJson();
            attr.peo_id = people.peo_uid.ToString();
            //attr.id=
            
        
        }



        public PublicFolderJSON(doc01 doc)
        {
            this.data = doc.d01_name;
            if (doc.d01_son == 1)
            {

                this.state = "closed";
            }


            FolderAttrJson attr = new FolderAttrJson();


            attr.id = doc.d01_no.ToString();
            attr.depid = doc.dep_no.ToString();
            attr.folderType = doc.d01_type.ToString();
            this.attr = attr;

        }
    }
}