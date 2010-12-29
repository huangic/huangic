using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using Entity;


namespace NXEIP.DAO
{
    /// <summary>
    /// AlbumDAO 的摘要描述
    /// </summary>
    [DataObject]
    public class AlbumDAO
    {

        private NXEIPEntities model = new NXEIPEntities();
        
        
        public AlbumDAO()
        {
            //
            // TODO: 在此加入建構函式的程式碼
            //
        }

        /// <summary>
        /// 取人員的相簿
        /// </summary>
        /// <returns></returns>
        public IQueryable<album> GetPeopleAlbum(int people,int dep_no,string alb_public){
                //取開放程度
            //取所有個人的資料
            if (alb_public == "1") {
                var albums = (from d in model.album where d.peo_uid == people && d.alb_status != "2" select d);
                return albums;
            }

            if (alb_public == "2")
            {
                var albums = (from d in model.album where d.alb_dep==dep_no &&d.alb_public=="2" && d.alb_status == "1" select d);
                return albums;
            }

            if (alb_public == "3")
            {
                var albums = (from d in model.album where d.alb_dep == dep_no && d.alb_public=="3" && d.alb_status == "1" select d);
                return albums;
            }

            return default(IQueryable<album>);
        }


        public int GetPeopleAlbumCount(int people, int dep_no, string alb_public) {
            return GetPeopleAlbum(people, dep_no, alb_public).Count();
        }

    }
}