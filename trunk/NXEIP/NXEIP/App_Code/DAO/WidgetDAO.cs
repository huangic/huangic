﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Entity;


namespace NXEIP.DAO
{
    /// <summary>
    /// WidgetDAO 的摘要描述
    /// </summary>
    public class WidgetDAO
    {
        public WidgetDAO()
        {
            //
            // TODO: 在此加入建構函式的程式碼
            //
        }

        /// <summary>
        /// 取DB 的PAGENO
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="page_type"></param>
        /// <returns></returns>
        public int? GetPageNo(int? uid, String page_type)
        {
            NXEIPEntities model = new NXEIPEntities();
            try
            {
                return (from p in model.page where p.pag_type == page_type && p.pag_uid == uid select p).FirstOrDefault().pag_no;
            }
            catch
            {
                return null;
            }
        }



        #region DB 讀取ZONE的WIDGET
        /// <summary>
        /// DB 讀取ZONE的WIDGET
        /// </summary>
        /// <param name="page_no"></param>
        /// <param name="zone"></param>
        /// <returns></returns>
        public IQueryable<widget> GetZoneWidget(int page_no, String zone)
        {
            NXEIPEntities model = new NXEIPEntities();


            return (from w in model.block where w.blo_layout == zone && w.page.pag_no == page_no orderby w.blo_order select w.widget);
        }
        #endregion
    }
}