using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Entity;
using System.ComponentModel;

namespace NXEIP.DAO
{
    /// <summary>
    /// RssDAO 的摘要描述
    /// </summary>
    [DataObject(true)]
    public class RssDAO
    {
        private NXEIPEntities model = new NXEIPEntities();

        public RssDAO()
        {
        }

        public IQueryable<rss> Get_RssData(int peo_uid)
        {
            return (from d in model.rss
                    where d.peo_uid == peo_uid && d.rss_status == "1"
                    orderby d.rss_order, d.rss_createtime descending
                    select d);
        }

        public IQueryable<rss> Get_RssData(int peo_uid, int startRowIndex, int maximumRows)
        {
            return Get_RssData(peo_uid).Skip(startRowIndex).Take(maximumRows);
        }

        public int Get_RssDataCount(int peo_uid)
        {
            return Get_RssData(peo_uid).Count();
        }

        public rss Get_Rss(int peo_uid, int rss_no)
        {
            return (from d in model.rss
                    where d.rss_no == rss_no && d.peo_uid == peo_uid
                    select d).FirstOrDefault();
        }

        public int Get_MAXRssNO(int peo_uid)
        {
            var data = (from d in model.rss
                        where d.peo_uid == peo_uid
                        select d.rss_no);

            if (data.Count() > 0)
            {
                return data.Max();
            }
            else
            {
                return 0;
            }
        }

        public void AddToRss(rss d)
        {
            model.rss.AddObject(d);
        }

        public void DeleteRss(rss d)
        {
            model.rss.DeleteObject(d);
        }

        public void Update()
        {
            model.SaveChanges();
        }
        
    }

}
