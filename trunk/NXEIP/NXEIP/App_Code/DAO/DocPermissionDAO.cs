using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NXEIP;
using Entity;
using NLog;
using NXEIP.FileManager;
using System.ComponentModel;
using System.Data.Objects.SqlClient;
using System.Data.Objects;


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


        private NXEIPEntities model = new NXEIPEntities();
            

        public DocPermissionDAO()
        {
            //
            // TODO: 在此加入建構函式的程式碼
            //
        }

        public IQueryable<PermissionObj> GetFilePermission(int doc_no)
        {

            //doc_no 轉陣列;

            //string[] docArray = docNoString.Split(',');
            //int[] doc_no = Array.ConvertAll(docArray, new Converter<string, int>(StringToInt));

                
                //找檔案對應的權限設定
            var permisssion = (from d in model.doc03 where d.d01_no == doc_no select d);

            //沒有權限檔就見一個
            if (permisssion.Count() == 0) {

                doc03 newPermission = new doc03();
                newPermission.d01_no = doc_no;
                newPermission.d03_authority = "1000";
                newPermission.d03_type = "001";

                model.doc03.AddObject(newPermission);
                model.SaveChanges();

                return null;
            }


           
                //檔案權限的物件

                //部門授權
                var groupA =
                    (
                    from c in model.departments
                    from b in model.doc04
                    from a in model.doc03
                    where doc_no==a.d01_no && a.d03_type.Substring(1, 1) == "1" && a.d03_no == b.d03_no
                    && c.dep_no == b.d04_depno
                    select new PermissionObj { id = b.d04_no, type="D", value = c.dep_name });

                

                //個人授權
                var groupB =
                    (
                      from c in model.people
                      from b in model.doc05
                      from a in model.doc03
                      where doc_no==a.d01_no && a.d03_type.Substring(2, 1) == "1" && a.d03_no == b.d03_no && c.peo_uid == b.d05_peouid
                      select new PermissionObj { id = b.d05_no, type = "P", value = c.peo_name });

                var group = groupA.Union(groupB);



                logger.Debug((group as ObjectQuery).ToTraceString());

                return group;
            
        }



       
    }
}