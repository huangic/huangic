using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NXEIP.JsTree;
using NXEIP.Tree.Json;



namespace NXEIP.Tree
{
    /// <summary>
    /// ChildDepartTreeNode 的摘要描述
    /// </summary>
    public class SelfDepartTreeNode:DepartTreeNode
    {
        public SelfDepartTreeNode()
        {
            //
            // TODO: 在此加入建構函式的程式碼
            //
        }

        public int CurrentDepId;


        public override ICollection<JsTreeJson> GetLevelOneNode()
        {
            //取自己部門
                
            


            return this.GetChildNode(1);
        }
    }
}