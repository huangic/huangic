using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Entity;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;
using System.Globalization;

namespace NXEIP.DAO
{
    /// <summary>
    /// 功能名稱：borrows
    /// 功能描述：取得、設定設備借用申請單
    /// 撰寫者：Lina
    /// 撰寫時間：2011/01/21
    /// </summary>
    [DataObject(true)]
    public class M03DAO
    {
        public M03DAO()
        {
        }

        private NXEIPEntities model = new NXEIPEntities();
        ChangeObject changeobj = new ChangeObject();

        #region 抓出來的結構
        public class NewM03
        {
            public int m03_no { get; set; }
            public int m03_type { get; set; }
            public string typename { get; set; }
            public int m02_no { get; set; }
            public string m02_number { get; set; }
            public int m03_depno { get; set; }
            public int m03_peouid { get; set; }
            public DateTime m03_sdate { get; set; }
            public DateTime m03_edate { get; set; }
            public string m03_reason { get; set; }
            public string m03_verify { get; set; }
            public string stet
            {
                get
                {
                    if (m03_sdate != null && m03_edate != null)
                    {
                        string st = new ChangeObject().ADDTtoROCDT(m03_sdate.ToString("yyyy-MM-dd HH:mm")) + "~" + m03_edate.ToString("HH:mm");
                        return st;
                    }
                    else
                        return "";
                }
                set
                {
                    stet = value;
                }
            }

        }
        #endregion

        #region 分頁列表使用
        public IQueryable<NewM03> GetAll(string sdate, string edate, string status, int chekuan, int car, int loginuser)
        {
            if (sdate != null && edate != null && status != null)
            {
                DateTime sd = Convert.ToDateTime(sdate + " 00:00:00");
                DateTime ed = Convert.ToDateTime(edate + " 23:59:59");
                if (status.Equals("0"))
                {
                    #region 當申請狀態選全部時
                    if (car > 0)
                    {
                        #region 查某個場地
                        var itemColl = from m03tb in model.m03
                                       join m02tb in model.m02 on m03tb.m03_m02no equals m02tb.m02_no
                                       join m01tb in model.m01 on m03tb.m03_type equals m01tb.m01_no
                                       where m03tb.m03_sdate >= sd && m03tb.m03_edate <= ed && (m03tb.m03_verify == "1" || m03tb.m03_verify == "2") && m03tb.m03_m02no == car && m02tb.m02_peouid == loginuser
                                       orderby m03tb.m03_sdate descending, m03tb.m03_edate descending
                                       select new NewM03
                                       {
                                           m03_no = m03tb.m03_no,
                                           m03_type = m03tb.m03_type.Value,
                                           typename = m01tb.m01_name,
                                           m02_no = m03tb.m03_m02no.Value,
                                           m02_number = m02tb.m02_number,
                                           m03_depno = m03tb.m03_depno.Value,
                                           m03_peouid = m03tb.m03_peouid.Value,
                                           m03_sdate = m03tb.m03_sdate.Value,
                                           m03_edate = m03tb.m03_edate.Value,
                                           m03_reason = m03tb.m03_reason,
                                           m03_verify = m03tb.m03_verify
                                       };
                        return itemColl;
                        #endregion
                    }
                    else if (chekuan > 0)
                    {
                        #region 查某個所在地
                        var itemColl = from m03tb in model.m03
                                       join m02tb in model.m02 on m03tb.m03_m02no equals m02tb.m02_no
                                       join m01tb in model.m01 on m03tb.m03_type equals m01tb.m01_no
                                       where m03tb.m03_sdate >= sd && m03tb.m03_edate <= ed && (m03tb.m03_verify == "1" || m03tb.m03_verify == "2") && m03tb.m03_type == chekuan && m02tb.m02_peouid == loginuser
                                       orderby m03tb.m03_sdate descending, m03tb.m03_edate descending
                                       select new NewM03
                                       {
                                           m03_no = m03tb.m03_no,
                                           m03_type = m03tb.m03_type.Value,
                                           typename = m01tb.m01_name,
                                           m02_no = m03tb.m03_m02no.Value,
                                           m02_number = m02tb.m02_number,
                                           m03_depno = m03tb.m03_depno.Value,
                                           m03_peouid = m03tb.m03_peouid.Value,
                                           m03_sdate = m03tb.m03_sdate.Value,
                                           m03_edate = m03tb.m03_edate.Value,
                                           m03_reason = m03tb.m03_reason,
                                           m03_verify = m03tb.m03_verify
                                       };
                        return itemColl;
                        #endregion
                    }
                    else
                    {
                        #region 查全部
                        var itemColl = from m03tb in model.m03
                                       join m02tb in model.m02 on m03tb.m03_m02no equals m02tb.m02_no
                                       join m01tb in model.m01 on m03tb.m03_type equals m01tb.m01_no
                                       where m03tb.m03_sdate >= sd && m03tb.m03_edate <= ed && (m03tb.m03_verify == "1" || m03tb.m03_verify == "2") && m02tb.m02_peouid == loginuser
                                       orderby m03tb.m03_sdate descending, m03tb.m03_edate descending
                                       select new NewM03
                                       {
                                           m03_no = m03tb.m03_no,
                                           m03_type = m03tb.m03_type.Value,
                                           typename = m01tb.m01_name,
                                           m02_no = m03tb.m03_m02no.Value,
                                           m02_number = m02tb.m02_number,
                                           m03_depno = m03tb.m03_depno.Value,
                                           m03_peouid = m03tb.m03_peouid.Value,
                                           m03_sdate = m03tb.m03_sdate.Value,
                                           m03_edate = m03tb.m03_edate.Value,
                                           m03_reason = m03tb.m03_reason,
                                           m03_verify = m03tb.m03_verify
                                       };
                        return itemColl;
                        #endregion
                    }
                    #endregion
                }
                else
                {
                    #region 當申請狀態選非全部時
                    if (car > 0)
                    {
                        #region 查某個場地
                        var itemColl = from m03tb in model.m03
                                       join m02tb in model.m02 on m03tb.m03_m02no equals m02tb.m02_no
                                       join m01tb in model.m01 on m03tb.m03_type equals m01tb.m01_no
                                       where m03tb.m03_sdate >= sd && m03tb.m03_edate <= ed && m03tb.m03_verify == status && m03tb.m03_m02no == car && m02tb.m02_peouid == loginuser
                                       orderby m03tb.m03_sdate descending, m03tb.m03_edate descending
                                       select new NewM03
                                       {
                                           m03_no = m03tb.m03_no,
                                           m03_type = m03tb.m03_type.Value,
                                           typename = m01tb.m01_name,
                                           m02_no = m03tb.m03_m02no.Value,
                                           m02_number = m02tb.m02_number,
                                           m03_depno = m03tb.m03_depno.Value,
                                           m03_peouid = m03tb.m03_peouid.Value,
                                           m03_sdate = m03tb.m03_sdate.Value,
                                           m03_edate = m03tb.m03_edate.Value,
                                           m03_reason = m03tb.m03_reason,
                                           m03_verify = m03tb.m03_verify
                                       };
                        return itemColl;
                        #endregion
                    }
                    else if (chekuan > 0)
                    {
                        #region 查某個所在地
                        var itemColl = from m03tb in model.m03
                                       join m02tb in model.m02 on m03tb.m03_m02no equals m02tb.m02_no
                                       join m01tb in model.m01 on m03tb.m03_type equals m01tb.m01_no
                                       where m03tb.m03_sdate >= sd && m03tb.m03_edate <= ed && m03tb.m03_verify == status && m03tb.m03_type == chekuan && m02tb.m02_peouid == loginuser
                                       orderby m03tb.m03_sdate descending, m03tb.m03_edate descending
                                       select new NewM03
                                       {
                                           m03_no = m03tb.m03_no,
                                           m03_type = m03tb.m03_type.Value,
                                           typename = m01tb.m01_name,
                                           m02_no = m03tb.m03_m02no.Value,
                                           m02_number = m02tb.m02_number,
                                           m03_depno = m03tb.m03_depno.Value,
                                           m03_peouid = m03tb.m03_peouid.Value,
                                           m03_sdate = m03tb.m03_sdate.Value,
                                           m03_edate = m03tb.m03_edate.Value,
                                           m03_reason = m03tb.m03_reason,
                                           m03_verify = m03tb.m03_verify
                                       };
                        return itemColl;
                        #endregion
                    }
                    else
                    {
                        #region 查全部
                        var itemColl = from m03tb in model.m03
                                       join m02tb in model.m02 on m03tb.m03_m02no equals m02tb.m02_no
                                       join m01tb in model.m01 on m03tb.m03_type equals m01tb.m01_no
                                       where m03tb.m03_sdate >= sd && m03tb.m03_edate <= ed && m03tb.m03_verify == status && m02tb.m02_peouid == loginuser
                                       orderby m03tb.m03_sdate descending, m03tb.m03_edate descending
                                       select new NewM03
                                       {
                                           m03_no = m03tb.m03_no,
                                           m03_type = m03tb.m03_type.Value,
                                           typename = m01tb.m01_name,
                                           m02_no = m03tb.m03_m02no.Value,
                                           m02_number = m02tb.m02_number,
                                           m03_depno = m03tb.m03_depno.Value,
                                           m03_peouid = m03tb.m03_peouid.Value,
                                           m03_sdate = m03tb.m03_sdate.Value,
                                           m03_edate = m03tb.m03_edate.Value,
                                           m03_reason = m03tb.m03_reason,
                                           m03_verify = m03tb.m03_verify
                                       };
                        return itemColl;
                        #endregion
                    }
                    #endregion
                }
            }
            else
            {
                #region 查出全部
                var itemColl = from m03tb in model.m03
                               join m02tb in model.m02 on m03tb.m03_m02no equals m02tb.m02_no
                               join m01tb in model.m01 on m03tb.m03_type equals m01tb.m01_no
                               where m02tb.m02_peouid == loginuser
                               orderby m03tb.m03_sdate descending, m03tb.m03_edate descending
                               select new NewM03
                               {
                                   m03_no = m03tb.m03_no,
                                   m03_type = m03tb.m03_type.Value,
                                   typename = m01tb.m01_name,
                                   m02_no = m03tb.m03_m02no.Value,
                                   m02_number = m02tb.m02_number,
                                   m03_depno = m03tb.m03_depno.Value,
                                   m03_peouid = m03tb.m03_peouid.Value,
                                   m03_sdate = m03tb.m03_sdate.Value,
                                   m03_edate = m03tb.m03_edate.Value,
                                   m03_reason = m03tb.m03_reason,
                                   m03_verify = m03tb.m03_verify
                               };
                return itemColl;
                #endregion
            }
        }
        public IQueryable<NewM03> GetAll(string sdate, string edate, string status, int chekuan, int car, int loginuser, int startRowIndex, int maximumRows)
        {
            return GetAll(sdate, edate, status, chekuan, car, loginuser).Skip(startRowIndex).Take(maximumRows);
        }

        public int GetAllCount(string sdate, string edate, string status, int chekuan, int car, int loginuser)
        {
            return GetAll(sdate, edate, status, chekuan, car, loginuser).Count();
        }
        #endregion

        #region 新增&修改
        public void AddM03(m03 rowdata)
        {
            model.AddTom03(rowdata);
        }

        public int Update()
        {
            return model.SaveChanges();
        }
        #endregion

        #region 由[編號]取得[資料]
        /// <summary>
        /// 由[編號]取得[資料]
        /// </summary>
        /// <param name="no">編號</param>
        /// <returns>整筆資料</returns>
        public m03 GetByNo(int no)
        {
            return (from tb in model.m03 where tb.m03_no == no select tb).FirstOrDefault();
        }
        #endregion
    }
}