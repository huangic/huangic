using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Entity;
using System.ComponentModel;



namespace NXEIP.DAO
{
    /// <summary>
    /// _100105DAO 的摘要描述
    /// </summary>
    [DataObject]
    public class _100105DAO
    {

        private NXEIPEntities model = new NXEIPEntities();

        public _100105DAO()
        {
            //
            // TODO: 在此加入建構函式的程式碼
            //
        }

        public IQueryable<doc01> GetPublicFile(String network, String pwd) {
            var folders = 
                from s in model.doc14
                from f in model.doc01 
                where 
                s.d14_network==network && s.d14_passwd==pwd
                && f.d01_parentid == s.d01_no 
                && f.d01_type == "1" 
                && String.IsNullOrEmpty(f.d01_name) select f;


            return folders;
        }

    }
}