using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Entity;
using System.ComponentModel;

namespace NXEIP.DAO
{
    /// <summary>
    /// 功能名稱：c02
    /// 功能描述：取得、設定場地資料rooms資料表的內容
    /// 撰寫者：Lina
    /// 撰寫時間：2010/10/25
    /// </summary>
    [DataObject(true)]
    public class C02DAO
    {
        public C02DAO()
        {
           
        }
        private NXEIPEntities model = new NXEIPEntities();

        #region 抓出來的結構
        public class NewC02
        {
            public int peo_uid { get; set; }
            public int c02_no { get; set; }
            public string peo_name { get; set; }
            public DateTime c02_sdate { get; set; }
            public DateTime c02_edate { get; set; }
            public string c02_title { get; set; }
            public string c02_check { get; set; }
            public string c02_reason { get; set; }
            public string stet
            {
                get
                {
                    if (c02_sdate != null && c02_edate != null)
                    {
                        string st = new ChangeObject().ADDTtoROCDT(c02_sdate.ToString("yyyy-MM-dd HH:mm")) + "~" + c02_edate.ToString("HH:mm");
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
        public IQueryable<NewC02> GetAll(string sdate, string edate, string status,int loginuser)
        {
            if (sdate != null && edate != null && status != null)
            {
                DateTime sd = Convert.ToDateTime(sdate + " 00:00:00");
                DateTime ed = Convert.ToDateTime(edate + " 23:59:59");
                if (status.Equals("-1"))
                {
                    #region 當申請狀態選全部時
                    var itemColl = from tbc02 in model.c02
                                   join tbpeo in model.people on tbc02.peo_uid equals tbpeo.peo_uid
                                   where tbc02.c02_sdate >= sd && tbc02.c02_edate <= ed && tbc02.c02_appointmen == "1" && tbc02.c02_setuid == loginuser && tbc02.peo_uid != loginuser
                                   orderby tbc02.c02_sdate ascending, tbc02.c02_edate ascending
                                   select new NewC02
                                   {
                                       peo_uid = tbc02.peo_uid,
                                       c02_no = tbc02.c02_no,
                                       peo_name = tbpeo.peo_name,
                                       c02_sdate = tbc02.c02_sdate,
                                       c02_edate = tbc02.c02_edate,
                                       c02_title = tbc02.c02_title,
                                       c02_check = tbc02.c02_check,
                                       c02_reason = tbc02.c02_reason
                                   };
                    return itemColl;
                    #endregion
                }
                else
                {
                    #region 當申請狀態選非全部時
                    var itemColl = from tbc02 in model.c02
                                   join tbpeo in model.people on tbc02.peo_uid equals tbpeo.peo_uid
                                   where tbc02.c02_sdate >= sd && tbc02.c02_edate <= ed && tbc02.c02_appointmen == "1" && tbc02.c02_check == status && tbc02.c02_setuid == loginuser && tbc02.peo_uid != loginuser
                                   orderby tbc02.c02_sdate ascending, tbc02.c02_edate ascending
                                   select new NewC02
                                   {
                                       peo_uid = tbc02.peo_uid,
                                       c02_no = tbc02.c02_no,
                                       peo_name = tbpeo.peo_name,
                                       c02_sdate = tbc02.c02_sdate,
                                       c02_edate = tbc02.c02_edate,
                                       c02_title = tbc02.c02_title,
                                       c02_check = tbc02.c02_check,
                                       c02_reason = tbc02.c02_reason
                                   };
                    return itemColl;
                    #endregion
                }
            }
            else
            {
                #region 查出全部
                var itemColl = from tbc02 in model.c02
                               join tbpeo in model.people on tbc02.peo_uid equals tbpeo.peo_uid
                               where tbc02.c02_appointmen == "1" && tbc02.c02_setuid == loginuser && tbc02.peo_uid != loginuser
                               orderby tbc02.c02_sdate ascending, tbc02.c02_edate ascending
                               select new NewC02
                               {
                                   peo_uid = tbc02.peo_uid,
                                   c02_no = tbc02.c02_no,
                                   peo_name = tbpeo.peo_name,
                                   c02_sdate = tbc02.c02_sdate,
                                   c02_edate = tbc02.c02_edate,
                                   c02_title = tbc02.c02_title,
                                   c02_check = tbc02.c02_check,
                                   c02_reason = tbc02.c02_reason
                               };
                return itemColl;
                #endregion
            }
        }
        public IQueryable<NewC02> GetAll(string sdate, string edate, string status, int loginuser, int startRowIndex, int maximumRows)
        {
            return GetAll(sdate, edate, status, loginuser).Skip(startRowIndex).Take(maximumRows);
        }

        public int GetAllCount(string sdate, string edate, string status, int loginuser)
        {
            return GetAll(sdate, edate, status, loginuser).Count();
        }
        #endregion

        #region 新增&修改
        public void AddC02(c02 tb)
        {
            model.AddToc02(tb);
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
        public c02 GetByC02No(int peo_uid, int c02_no)
        {
            return (from tb in model.c02 where tb.peo_uid == peo_uid && tb.c02_no == c02_no select tb).FirstOrDefault();
        }
        #endregion
        
        /// <summary>
        /// 由人員編號(peo_uid)取得最大c02_no
        /// </summary>
        /// <param name="peo_uid">人員編號</param>
        /// <returns>c02_no</returns>
        public int GetMaxNoByPeoUid(int peo_uid)
        {
            return (from tb in model.c02 where tb.peo_uid == peo_uid select tb.c02_no).DefaultIfEmpty().Max();
        }
    }
}