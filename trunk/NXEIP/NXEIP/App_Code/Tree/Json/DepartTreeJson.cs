using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NXEIP.JsTree;
using Entity;


namespace NXEIP.Tree.Json
{

    /// <summary>
    /// PermissionTreeJson 的摘要描述
    /// </summary>
    public class DepartTreeJson : JsTreeJson
    {
        public DepartTreeJson()
        {
           
        }

        //部門的節點
        public DepartTreeJson(departments department)
        {
            this.data = department.dep_name;

            DepartTreeAttr attr = new DepartTreeAttr();

            this.state = "closed";

            attr.id = department.dep_no.ToString();
            //attr.nType = "depart";
            attr.rel = "depart";
            attr.nName = this.data;
            this.attr = attr;

        }
        //人員節點
        public DepartTreeJson(people peo)
        {
            this.data = peo.peo_name;

            DepartTreeAttr attr = new DepartTreeAttr();

            attr.id = peo.peo_uid.ToString();
            //attr.nType = "people";
            attr.rel = "people";
            attr.nName = this.data;
            this.attr = attr;
        }

    }
}