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
                    select new PermissionObj { id = b.d04_no, type="D", value = c.dep_name, d03_no=a.d03_no});

                

                //個人授權
                var groupB =
                    (
                      from c in model.people
                      from b in model.doc05
                      from a in model.doc03
                      where doc_no==a.d01_no && a.d03_type.Substring(2, 1) == "1" && a.d03_no == b.d03_no && c.peo_uid == b.d05_peouid
                      select new PermissionObj { id = b.d05_no, type = "P", value = c.peo_name, d03_no = a.d03_no });

                var group = groupA.Union(groupB);



                logger.Debug((group as ObjectQuery).ToTraceString());

                return group;
            
        }

        /// <summary>
        /// 用DOC01 NO 取DOC03
        /// </summary>
        /// <param name="doc01_no"></param>
        /// <returns></returns>
        public int? GetDoc03NoFromDoc01NO(int doc01_no) {
            try {
                return (from d in model.doc03 where d.d01_no == doc01_no select d.d03_no).FirstOrDefault();
            }catch{
                logger.Debug("無符合資料");
                return null; 
            }
            
          
            
            
            
        }

        private int GetDocDepartmentMax(int doc03_no)
        {
            int max = 1;
             try
            {
            int nowValue = (from d in model.doc04 where d.d03_no == doc03_no select d.d04_no).Max();
            max = nowValue+1;
             }
             catch
             {

             }
            return max;
        }


        private int GetDocPeopleMax(int doc03_no)
        {
            int max = 1;
            try
            {
                int nowValue = (from d in model.doc05 where d.d03_no == doc03_no select d.d05_no).Max();
                max = nowValue+1;
            }
            catch { 
            
            }
            return max;
        }

        /// <summary>
        /// 新增物件並加上PK流水號(因為是用計算的)
        /// </summary>
        /// <param name="doc"></param>
        public void AddDocPeopleAndSetPK(doc05 doc) {
            doc.d05_no = GetDocPeopleMax(doc.d03_no);
            
            model.doc05.AddObject(doc);
            model.SaveChanges();
        }

        public void AddDocDepartmentAndSetPK(doc04 doc)
        {
            doc.d04_no = GetDocDepartmentMax(doc.d03_no);

            model.doc04.AddObject(doc);
            model.SaveChanges();
        }

       
    }
}