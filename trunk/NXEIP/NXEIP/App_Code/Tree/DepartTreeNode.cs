using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NXEIP.JsTree;



namespace NXEIP.Tree
{
    /// <summary>
    /// IDepartTreeNode 的摘要描述
    /// </summary>
    public abstract class DepartTreeNode
    {

        List<ChildNode> childs = new List<ChildNode>();


        public abstract ICollection<JsTreeJson> GetLevelOneNode();

        public ICollection<JsTreeJson> GetChildNode(int parentID)
        {
            ICollection<JsTreeJson> jsons = new LinkedList<JsTreeJson>();
              foreach(ChildNode c in childs){
                  jsons = (ICollection<JsTreeJson>)(jsons.Concat(c.GetData(parentID))).ToList();
              }

            return jsons;
        }

        public void AddChildNodeStrategy(ChildNode node){
            this.childs.Add(node);
        }
    }
}