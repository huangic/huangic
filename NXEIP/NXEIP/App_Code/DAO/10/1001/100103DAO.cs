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
                              join p in model.photo on new { Id = d.alb_no, photo=d.alb_cover.Value } equals new { Id = p.alb_no, photo=p.pho_no } into k
                              from p2 in k.DefaultIfEmpty()

                              where

                              d.peo_uid == people && d.alb_status != "2" && d.alb_status != "4"
                              select new Photoalbum { Album = d, Cover = p2, Count = (from p3 in model.photo where p3.alb_no == d.alb_no select d).Count() });
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


        public IQueryable<photo> GetTopUploadPhoto(int num,int peo_uid) {
            var ps = (from d in model.photo where d.pho_createuid == peo_uid orderby d.pho_createtime select d).Take(num);

            return ps;
        }


        public IQueryable<photo> GetTopUploadDepartPhoto(int num, int dep_no)
        {
            var ps = (from a in model.album from d in model.photo 
                      where a.alb_no==d.alb_no 
                      && a.alb_dep == dep_no
                      && a.alb_public=="2"
                      orderby d.pho_createtime select d).Take(num);

            return ps;
        }

        public IQueryable<photo> GetTopUploadUnitPhoto(int num)
        {
            var ps = (from a in model.album
                      from d in model.photo
                      where a.alb_no == d.alb_no
                      && a.alb_public == "3"
                      && a.alb_status=="1"
                      orderby d.pho_createtime
                      select d).Take(num);

            return ps;
        }

    }
}