﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using Entity;

/// <summary>
/// _200601_2DAO 的摘要描述
/// </summary>
/// 
namespace NXEIP.DAO
{
    [DataObject(true)]
    public class _200601_2DAO
    {
        public _200601_2DAO()
        {
            //
            // TODO: 在此加入建構函式的程式碼
            //
        }

        NXEIPEntities model = new NXEIPEntities();

        /// <summary>
        /// 取得主題
        /// </summary>
        /// <param name="tao_no">討論區編號</param>
        /// <param name="Featured">是否為精華區</param>
        /// <returns></returns>
        private IQueryable<Topic> GetTao01(int tao_no, bool Featured,string keyword,string option,DateTime? sdate,DateTime? edate)
        {
                  
            
            
            IQueryable<Topic> taos = from p in model.people
                                     from d in model.tao01
                                     where d.tao_no == tao_no
                                     && d.t01_peouid==p.peo_uid
                                     && d.t01_parent == 0
                                     && d.t01_status=="1"
                                     orderby d.t01_order
                                     orderby d.t01_date descending
                                     select new Topic { 
                                        ForumId=d.tao_no,
                                        Id=d.t01_no,
                                        Name=d.t01_subject,
                                        Content=d.t01_content,
                                        AuthorId=d.t01_peouid.Value,
                                        Author=p.peo_name,
                                        PublishDate=d.t01_date.Value,
                                        FileName=d.t01_file,
                                        FileFormat=d.t01_type,
                                        FilePath=d.t01_path,
                                        Order=d.t01_order.Value,
                                        LastRelayDate=d.t01_date.Value,
                                        LastRelayAuthor=p.peo_name,
                                        ParentId=d.t01_parent.Value
                                     };


            ///需要多取精華區條件
            if (Featured) {
                IQueryable<int> featured = from d in model.tao02 where d.tao_no == tao_no select d.t01_no;

                taos = taos.Where(x => featured.Contains(x.Id));

            }

            if (!String.IsNullOrEmpty(keyword))
            {

                //取關鍵字條件
                if (option == "1")
                {
                    taos = taos.Where(x => x.Name.Contains(keyword));
                }
                if (option == "2")
                {
                    taos = taos.Where(x => x.Content.Contains(keyword));
                }
                if (option == "3")
                {
                    taos = taos.Where(x => x.FileName.Contains(keyword));
                }
            }

            //取日期
            if (sdate.HasValue && edate.HasValue) {

                taos = taos.Where(x => x.PublishDate > sdate && x.PublishDate < edate);
            }
            return taos;
        }

        private IQueryable<Topic> GetTao01(int tao_no,bool Featured,string keyword,string option,DateTime? sdate,DateTime? edate, int startRowIndex, int maximumRows)
        {
            return GetTao01(tao_no,Featured,keyword,option,sdate,edate).Skip(startRowIndex).Take(maximumRows);
        }

        public IEnumerable<Topic> GetTopicList(int tao_no, int peo_uid, bool Featured,string keyword,string option,DateTime? sdate,DateTime? edate, int startRowIndex, int maximumRows)
        {
            List<Topic> topics = GetTao01(tao_no, Featured,keyword,option,sdate,edate,startRowIndex, maximumRows).ToList();


            foreach (Topic t in topics) {
                t.RelayCount = this.ComputeRelay(t.Id);
                ValidPermission(t, peo_uid);
            }


            return topics;
        }

        public IEnumerable<Topic> GetTopicList(int tao_no, int peo_uid, bool Featured,string keyword,string option,DateTime? sdate,DateTime? edate)
        {
            List<Topic> topics = GetTao01(tao_no,Featured,keyword,option,sdate,edate).ToList();

            foreach (Topic t in topics)
            {
                t.RelayCount = this.ComputeRelay(t.Id);
                ValidPermission(t, peo_uid);
            }

            return topics;
        }

        public int GetTopicListCount(int tao_no, int peo_uid, bool Featured, string keyword, string option, DateTime? sdate, DateTime? edate)
        {
            return GetTao01(tao_no,Featured,keyword,option,sdate,edate).Count();
        }


        private int ComputeRelay(int tao_01) {
            return (from d in model.tao01 where d.t01_parent == tao_01 && d.t01_status == "1" select d).Count();
        }


        private void ValidPermission(Topic t,int peo_uid) {

            int permission = 0;

            //驗證權限

            //總管理者 版主 發布者會有全縣
            if (t.AuthorId == peo_uid) {
                permission = permission | 1;
            }

            //驗證版主
            int count = (from d in model.tao04 where d.tao_no == t.ForumId && d.peo_uid == peo_uid select d).Count();
            if (count > 0) {
                permission = permission | 2;
            }

            //驗證總管理者
            int rootCount = (from d in model.manager where d.peo_uid==peo_uid && d.man_type=="2" select d).Count();
            if (rootCount > 0)
            {
                permission = permission | 4;
            }

            t.Permission = System.Convert.ToString(permission, 2);

        }

    }
}