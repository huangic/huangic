using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NXEIP.JsTree;
using NXEIP.Tree.Json;



namespace NXEIP.Tree
{
    /// <summary>
    /// AllDepartTreeNode 的摘要描述
    /// </summary>
    public class AllDepartTreeNode:DepartTreeNode
    {
        public AllDepartTreeNode()
        {
            //
            // TODO: 在此加入建構函式的程式碼
            //
        }

        public override ICollection<JsTreeJson> GetLevelOneNode()
        {



            return this.GetChildNode(1);
        }
    }
}