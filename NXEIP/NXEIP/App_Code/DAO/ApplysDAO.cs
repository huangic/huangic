using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Entity;
using System.ComponentModel;

namespace NXEIP.DAO
{

    /// <summary>
    /// ApplysDAO 的摘要描述
    /// </summary>
    [DataObject(true)]
    public class ApplysDAO
    {
        public ApplysDAO()
        {
        }

        private NXEIPEntities model = new NXEIPEntities();

        public IQueryable<applys> Get_Data(string type)
        {
            //type 1:未審核{0} 2:已審核{1,2}

            if (type == "1")
            {
                return from d in model.applys where d.app_check == "0" orderby d.app_date select d;
            }
            else
            {
                return from d in model.applys where d.app_check == "1" || d.app_check == "2" orderby d.app_date descending select d;
            }
            
        }

        public IQueryable<applys> Get_Data(string type, int startRowIndex, int maximumRows)
        {
            return Get_Data(type).Skip(startRowIndex).Take(maximumRows);
        }

        public int Get_DataCount(string type)
        {
            return Get_Data(type).Count();
        }

        public applys Get_applys(int app_no)
        {
            return (from d in model.applys where d.app_no == app_no select d).FirstOrDefault();
        }

        public void Update()
        {
            model.SaveChanges();
        }



    }
}