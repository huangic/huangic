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
    public class PeopleChildNode : ChildNode
    {



        public int status { get; set; }

        public bool showTitle { get; set; }
        public bool showExt { get; set; }
        public bool showWorkid { get; set; }


        public PeopleChildNode()
        {
            //
            // TODO: 在此加入建構函式的程式碼
            //
        }

        public override ICollection<JsTree.JsTreeJson> GetData(int pid)
        {
            ICollection<JsTreeJson> json = new LinkedList<JsTreeJson>();



            using (NXEIPEntities model = new NXEIPEntities())
            {

                //所屬人員
                var peopleResult = from p in model.people
                                   from acc in model.accounts
                                   where p.departments.dep_no == pid && p.peo_uid == acc.people.peo_uid && acc.acc_status == "1"
                                   select p;

                if (status != 0) {
                    peopleResult = peopleResult.Where(x => x.peo_jobtype == status).Select(x=>x);
                }


                foreach (var peo in peopleResult)
                {

                    //處理一下編號
                    DepartTreeJson node = new DepartTreeJson(peo);





                    json.Add(node);
                }

                


            }
            return json;
        }

        public override KeyValuePair<string, string> GetKeyValuePair(int id)
        {
            using (NXEIPEntities model = new NXEIPEntities())
            {
                var peo = (from d in model.people where d.peo_uid == id select d).First();



                KeyValuePair<String, String> value = new KeyValuePair<string, string>(id.ToString(), peo.peo_name);

                return value;



            }
        }
    }
}