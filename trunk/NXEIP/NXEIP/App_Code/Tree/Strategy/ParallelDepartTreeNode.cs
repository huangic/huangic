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
    public class ParallelDepartTreeNode:DepartTreeNode
    {
        public ParallelDepartTreeNode()
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
            using (NXEIPEntities model = new NXEIPEntities())
            {

                var dep = (from d in model.departments where d.dep_no == CurrentDepId select d).First();
                
                //level 1
                if (dep.dep_level == 1)
                {
                    jsons.Add(new DepartTreeJson(dep));
                }
                else { 
                    //取自己父帶

                    var parentDep = (from d in model.departments where d.dep_no == dep.dep_parentid select d).First();


                    jsons.Add(new DepartTreeJson(parentDep));
                }



            }



            return jsons;
        }
    }
}