using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Entity;
using System.ComponentModel;



namespace NXEIP.DAO
{
    /// <summary>
    /// _100103DAO 的摘要描述
    /// </summary>
    [DataObject]
    public class _100103DAO
    {

        private NXEIPEntities model = new NXEIPEntities();

        public _100103DAO()
        {
            //
            // TODO: 在此加入建構函式的程式碼
            //
        }

        /// <summary>
        /// 取人員的相簿
        /// </summary>
        /// <returns></returns>
        public IQueryable<Photoalbum> GetPeopleAlbum(int people, int dep_no, string alb_public)
        {
            //取開放程度
            //取所有個人的資料
            if (alb_public == "1")
            {
                var albums = (from d in model.album
                              join p in model.photo on d.alb_cover equals p.pho_no into k
                              from p2 in k.DefaultIfEmpty()
                              where d.peo_uid == people && d.alb_status != "2" && d.alb_status != "4"
                              select new Photoalbum { Album=d,Cover=p2,Count=(from p3 in model.photo where p3.alb_no==d.alb_no select d).Count()});
                return albums;
            }

            if (alb_public == "2")
            {
                var albums = (from d in model.album
                              join p in model.photo on d.alb_cover equals p.alb_no into k
                              from p2 in k.DefaultIfEmpty()
                              where d.alb_dep == dep_no && d.alb_public == "2" && d.alb_status == "1"
                              select new Photoalbum { Album = d, Cover = p2, Count = (from p3 in model.photo where p3.alb_no == d.alb_no select d).Count() });
                return albums;
            }

            if (alb_public == "3")
            {
                var albums = (from d in model.album
                              join p in model.photo on d.alb_cover equals p.alb_no into k
                              from p2 in k.DefaultIfEmpty()
                              where d.alb_dep == dep_no && d.alb_public == "3" && d.alb_status == "1"
                              select new Photoalbum { Album = d, Cover = p2, Count = (from p3 in model.photo where p3.alb_no == d.alb_no select d).Count() });
                return albums;
            }

            return default(IQueryable<Photoalbum>);
        }


        public int GetPeopleAlbumCount(int people, int dep_no, string alb_public)
        {
            return GetPeopleAlbum(people, dep_no, alb_public).Count();
        }


        public IQueryable<photo> GetUploadPhoto(int album_no,String fileNo) {
            try
            {
                String[] files = fileNo.Split(',');
                List<int> fileNos = (from d in files select int.Parse(d)).ToList();


                var ps = from d in model.photo where d.alb_no == album_no && fileNos.Contains(d.pho_no) select d;

                return ps;
            }
            catch {
                return default(IQueryable<photo>);
            }
        }

        public IQueryable<photo> GetAlbumPhoto(int album_no)
        {
            
                var ps = from d in model.photo where d.alb_no == album_no select d;

                return ps;
           
        }

    }
}