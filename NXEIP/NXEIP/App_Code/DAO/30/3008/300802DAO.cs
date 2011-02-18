using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Entity;
using System.ComponentModel;



namespace NXEIP.DAO
{
    /// <summary>
    /// _300802DAO 的摘要描述
    /// </summary>
    [DataObject]
    public class _300802DAO
    {

        private NXEIPEntities model = new NXEIPEntities();

        public _300802DAO()
        {
            //
            // TODO: 在此加入建構函式的程式碼
            //
        }

      /// <summary>
        ///  取該單位的要審核相簿
      /// </summary>
        /// <param name="peo_uid">管理人員</param>
      /// <returns></returns>
        public IQueryable<album> GetPeopleAlbum(int peo_uid)
        {
            //取管理的部門
                var manager_dep=(from peo in model.manager where peo.peo_uid==peo_uid && peo.man_type=="1" select peo.dep_no);

            //取符合的相簿
                var albums = (from d in model.album
                              where  manager_dep.Contains(d.alb_dep)  && d.alb_public == "3" && d.alb_status == "3"
                              select d);
                return albums;
           
        }


        public int GetPeopleAlbumCount(int dep_no)
        {
            return GetPeopleAlbum(dep_no).Count();
        }


    }
}