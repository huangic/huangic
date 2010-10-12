using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NXEIP;
using Entity;
using NLog;
using NXEIP.FileManager;
using System.ComponentModel;


namespace NXEIP.DAO
{
    /// <summary>
    /// DocPermission 的摘要描述
    /// </summary>
    /// 
    /// 
    [DataObject(true)]
    public class DocPermissionDAO
    {

        private static Logger logger = LogManager.GetCurrentClassLogger();

        public DocPermissionDAO()
        {
            //
            // TODO: 在此加入建構函式的程式碼
            //
        }

        public IQueryable GetAll(int doc_no)
        {

            int value = Convert.ToInt32("110", 2);
            logger.Debug(value);



            using (NXEIPEntities model = new NXEIPEntities())
            {

                //檔案權限的物件

                //部門授權
                var groupA =
                    (
                    from c in model.departments
                    from b in model.doc04
                    from a in model.doc03
                    where a.d01_no == doc_no && Convert.ToInt32(a.d03_type, 2) >= 2 && a.d01_no == b.d03_no
                    && c.dep_no == b.d04_depno
                    select new PermissionObj { id = a.d03_no, value = c.dep_name });
                //個人授權
                var groupB =
                    (
                      from c in model.people
                      from b in model.doc05
                      from a in model.doc03
                      where a.d01_no == doc_no && Convert.ToInt32(a.d03_type, 2) >= 1 && a.d01_no == b.d03_no && c.peo_uid == b.d05_peouid
                      select new PermissionObj { id = a.d03_no, value = c.peo_name });

                var group = groupA.Union(groupB);


                return group;
            }
        }



      
    }
}