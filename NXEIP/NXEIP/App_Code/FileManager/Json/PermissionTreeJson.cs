using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NXEIP.JsTree;
using Entity;


namespace NXEIP.FileManager.Json
{

    /// <summary>
    /// PermissionTreeJson 的摘要描述
    /// </summary>
    public class PermissionTreeJson : JsTreeJson
    {
        public PermissionTreeJson()
        {
           
        }

        //部門的節點
        public PermissionTreeJson(departments department)
        {
            this.data = department.dep_name;

            PermissionTreeAttr attr = new PermissionTreeAttr();

            this.state = "closed";

            attr.id = department.dep_no.ToString();
            attr.nType = "depart";
            attr.rel = "depart";
            attr.nName = this.data;
            this.attr = attr;

        }
        //人員節點
        public PermissionTreeJson(people peo)
        {
            this.data = peo.peo_name;

            PermissionTreeAttr attr = new PermissionTreeAttr();

            attr.id = peo.peo_uid.ToString();
            attr.nType = "people";
            attr.rel = "people";
            attr.nName = this.data;
            this.attr = attr;
        }

    }
}