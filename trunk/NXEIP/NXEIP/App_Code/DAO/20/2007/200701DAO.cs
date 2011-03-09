using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Entity;
using System.ComponentModel;

namespace NXEIP.DAO
{
    /// <summary>
    /// _200701DAO 的摘要描述
    /// </summary>
    [DataObject(true)]
    public class _200701DAO
    {
        private NXEIPEntities model = new NXEIPEntities();

        public _200701DAO()
        {
        }

        

        //維修類別
        public IQueryable<rep05> Get_rep05()
        {
            return (from d in model.rep05 where d.r05_status == "1" select d);
        }

        //業務類別
        public IQueryable<sysfuction> Get_SysfucType()
        {
            int[] sfu_no = { 200501, 200502, 200503, 200504, 200505, 200506 };
            return (from d in model.sysfuction where sfu_no.Contains(d.sfu_no) select d);
        }

        public qatype Search_qatype(string name, int? sfu_no, int? r05_no)
        {
            qatype d = null;

            if (!string.IsNullOrEmpty(name))
            {
                d = (from p in model.qatype where p.qat_name == name && p.qat_status == "1" select p).FirstOrDefault();
            }

            if (sfu_no.HasValue)
            {
                d = (from p in model.qatype where p.qat_s06no.Value == sfu_no.Value && p.qat_status == "1" select p).FirstOrDefault();
            }

            if (r05_no.HasValue)
            {
                d = (from p in model.qatype where p.qat_r05no.Value == r05_no.Value && p.qat_status == "1" select p).FirstOrDefault();
            }

            return d;
        }

        public qatype Get_qatype(int qat_no)
        {
            return (from d in model.qatype where d.qat_no == qat_no select d).FirstOrDefault();
        }

        public IQueryable<qatype> Get_QAtypeData()
        {
            return (from d in model.qatype
                    where d.qat_status == "1"
                    orderby d.qat_self, d.qat_name, d.qat_s06no, d.qat_r05no
                    select d);
        }

        public IQueryable<qatype> Get_QAtypeData(int startRowIndex, int maximumRows)
        {
            return Get_QAtypeData().Skip(startRowIndex).Take(maximumRows);
        }

        public int Get_QAtypeDataCount()
        {
            return Get_QAtypeData().Count();
        }

        public IQueryable<qamanager> Get_QAManager(int qat_no)
        {
            return (from d in model.qamanager where d.qat_no == qat_no orderby d.qam_peouid select d);
        }

        public IQueryable<qamanager> Get_QAManager(int qat_no, int startRowIndex, int maximumRows)
        {
            return Get_QAManager(qat_no).Skip(startRowIndex).Take(maximumRows);
        }

        public int Get_QAManagerCount(int qat_no)
        {
            return Get_QAManager(qat_no).Count();
        }

        public qamanager Get_QAManagerData(int qat_no, int peo_uid)
        {
            return (from d in model.qamanager where d.qam_peouid==peo_uid && d.qat_no == qat_no select d).FirstOrDefault();
        }

        public int Get_QAManagerMax(int qat_no)
        {
            var data = (from d in model.qamanager where d.qat_no == qat_no select d.qam_no);
            if (data.Count() > 0)
            {
                return data.Max();
            }
            else
            {
                return 0;
            }
        }

        public void AddToQAManager(qamanager d)
        {
            model.qamanager.AddObject(d);
        }

        public void DelToQAManager(qamanager d)
        {
            model.qamanager.DeleteObject(d);
        }

        public void AddToQAtype(qatype d)
        {
            model.qatype.AddObject(d);
        }

        public void Update()
        {
            model.SaveChanges();
        }
    }
}