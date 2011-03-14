using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Entity;
using System.ComponentModel;



namespace NXEIP.DAO
{
    /// <summary>
    /// FloorsDAO 的摘要描述
    /// </summary>
    [DataObject(true)]
    public class FloorsDAO
    {
        public FloorsDAO()
        {
            //
            // TODO: 在此加入建構函式的程式碼
            //
        }

        NXEIPEntities model = new NXEIPEntities();

        public IQueryable<floors> GetDataBySpotNo(int spot_no){
                return from d in model.floors 
                       where d.spo_no==spot_no && d.flo_status=="1" 
                       orderby d.flo_level,d.flo_order
                       select d;
        }
    
    
        public IQueryable<floors> GetAll(){
            return from d in model.floors where d.flo_status=="1" orderby d.spo_no,d.flo_level,d.flo_order select d;
        }

        public int GetAllCount() {
            return GetAll().Count();
        }

        public IQueryable<floors> GetAll(int startRowIndex, int maximumRows) {
            return GetAll().Skip(startRowIndex).Take(maximumRows);
        }
    
    }
}