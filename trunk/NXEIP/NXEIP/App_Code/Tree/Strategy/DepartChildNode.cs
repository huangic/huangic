using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NXEIP.JsTree;
using Entity;
using NXEIP.FileManager.Json;
using NXEIP.Tree.Json;


namespace NXEIP.Tree
{
    /// <summary>
    /// DepartChildNode 的摘要描述
    /// </summary>
    public class DepartChildNode : ChildNode
    {

        public bool checkChildPeople { get; set; }

        public DepartChildNode()
        {
            //
            // TODO: 在此加入建構函式的程式碼
            //
        }

        public override ICollection<JsTree.JsTreeJson> GetData(int parentID)
        {
            ICollection<JsTreeJson> json = new LinkedList<JsTreeJson>();



            using (NXEIPEntities model = new NXEIPEntities())
            {
                int pid = parentID;
                //子部門
                var result = (from d in model.departments
                              where d.dep_parentid == pid && d.dep_status == "1"
                              orderby d.dep_order
                              select d);
                
                foreach (var depart in result)
                {

                    JsTreeJson node = new DepartTreeJson(depart);
                    json.Add(node);

                    int peopleCount = 0;

                    if (checkChildPeople)
                    {

                        var childPeopleResult = from p in model.people
                                                from acc in model.accounts
                                                where p.departments.dep_no == depart.dep_no && p.peo_uid == acc.people.peo_uid && acc.acc_status == "1"
                                                select p;
                        peopleCount = childPeopleResult.Count();
                    }


                    if (depart.dep_son != "1" && peopleCount == 0)
                    {
                        //node.PopulateOnDemand = true;
                        node.state = null;

                    }


                }


            }
            return json;
        }

        public override KeyValuePair<string, string> GetKeyValuePair(int id)
        {
            



            using (NXEIPEntities model = new NXEIPEntities())
            {
                departments dep = (from d in model.departments where d.dep_no == id select d).First();



                KeyValuePair<String, String> value = new KeyValuePair<string, string>(id.ToString(), dep.dep_name);

                return value;
               

               
            }
        }
    }
}