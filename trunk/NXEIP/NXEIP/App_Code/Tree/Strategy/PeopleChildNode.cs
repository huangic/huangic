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

              

        public DepartTreeEnum setting { get; set; }  



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

                int status = (int)setting.TreePeopleStatus;


                if (status != 0) {
                    peopleResult = peopleResult.Where(x => x.peo_jobtype == status);
                }

                int ptype=(int)setting.TreePeopleType;
                
                if (ptype != 0) { 
                    if(ptype==1){
                        peopleResult=peopleResult.Where(x=>x.peo_ptype==1);
                    }else{
                        peopleResult = peopleResult.Where(x => x.peo_ptype != 1);
                    }
                
                }



                foreach (var peo in peopleResult)
                {
                   


                    //處理一下編號
                    DepartTreeJson node = new DepartTreeJson(peo);

                    String addition = "";

                    if( (setting.TreePeopleColumn & DepartTreeEnum.PeopleColumn.Title) ==DepartTreeEnum.PeopleColumn.Title){
                        string type_code=peo.peo_pfofess.HasValue?peo.peo_pfofess.Value.ToString():"";
                        if(!string.IsNullOrEmpty(type_code)){
                        //取職稱
                            string title = (from d in model.types where d.typ_code == "profess" && d.typ_number == type_code select d.typ_cname).FirstOrDefault();

                            if (!String.IsNullOrEmpty(title)) {
                                addition += title;
                                
                            }   
                        
                        }
                    }

                    //取員工編號

                    if ((setting.TreePeopleColumn & DepartTreeEnum.PeopleColumn.WorkId) == DepartTreeEnum.PeopleColumn.WorkId)
                    {
                        string wid = peo.peo_workid??"";


                        if (!String.IsNullOrEmpty(wid))
                            {
                                addition += " "+wid;

                            }

                        
                    }


                    if (!String.IsNullOrEmpty(addition)) {
                        node.data = node.data + "(" + addition + ")";
                    }


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