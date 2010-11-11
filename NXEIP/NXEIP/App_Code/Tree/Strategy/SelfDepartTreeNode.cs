using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NXEIP.JsTree;
using NXEIP.Tree.Json;
using Entity;



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

            ICollection<JsTreeJson> jsons = new LinkedList<JsTreeJson>();
            
            //取自己部門
            using(NXEIPEntities model=new NXEIPEntities()){

                var dep=(from d in model.departments where d.dep_no==CurrentDepId select d).First();
                jsons.Add(new DepartTreeJson(dep));
            }





            return jsons;
        }
    }
}