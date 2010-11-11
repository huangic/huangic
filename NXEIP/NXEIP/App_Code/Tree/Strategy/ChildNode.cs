using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NXEIP.JsTree;



namespace NXEIP.Tree
{
    /// <summary>
    /// ChildNode 的摘要描述
    /// </summary>
    public abstract class ChildNode
    {


        public abstract ICollection<JsTreeJson> GetData(int parentID);


        public abstract KeyValuePair<String, String>? GetKeyValuePair(int id);
    }
}