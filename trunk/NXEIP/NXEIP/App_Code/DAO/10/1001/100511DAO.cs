using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Entity;
using System.ComponentModel;
using System.IO;



namespace NXEIP.DAO
{

    public class LayoutObj
    {
        public string Name { get; set; }
    }
    
    
    /// <summary>
    /// _100103DAO 的摘要描述
    /// </summary>
    [DataObject]
    public class _100511DAO
    {

        private NXEIPEntities model = new NXEIPEntities();

        public _100511DAO()
        {
            //
            // TODO: 在此加入建構函式的程式碼
            //
        }

        //取版型目錄
        public List<LayoutObj> GetLayoutDir()
        {
            DirectoryInfo dirinfo = new DirectoryInfo(HttpContext.Current.Server.MapPath("~/style/"));
            DirectoryInfo[] sortList = dirinfo.GetDirectories();


            return (from d in sortList where (d.Attributes & FileAttributes.Hidden) != FileAttributes.Hidden select new LayoutObj { Name = d.Name }).ToList();

        }

        

    }
}