using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


/// <summary>
/// 樹狀的目錄JSON
/// </summary>
/// 


namespace FileManager
{
    public class FolderJSON
    {
        public FolderJSON()
        {
            this.attr = new AttrJson();
        }

       

        public String data { get; set; }
        public String state { get; set; }
        public AttrJson attr { get; set; }

        public ICollection<FolderJSON> children { get; set; }

    }

    public class AttrJson
    {
        public String id { get; set; }
        public String depid { get; set; }
        public String folderType { get; set; }
    }
}