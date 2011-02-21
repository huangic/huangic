﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using Entity;


namespace NXEIP.DAO
{
    /// <summary>
    /// _300901DAO 的摘要描述
    /// </summary>
    [DataObject(true)]
    public class _300901DAO
    {


        public _300901DAO()
        {
            //
            // TODO: 在此加入建構函式的程式碼
            //
        }

        NXEIPEntities model = new NXEIPEntities();

        #region 用表單取提交表單
        /// <summary>
        /// 用表單編號取取提交表單
        /// </summary>
        /// <param name="f01_no"></param>
        /// <returns></returns>
        public IQueryable<FormDetailVO> GetSubmitByFormNo(int f01_no,DateTime? sDate,DateTime? eDate,String peo_name,int? dep_no)
        {
            var forms = from f1 in model.form01
                        from f2 in model.form02
                        where
                        f1.f01_no == f2.f01_no
                        && f1.f01_no == f01_no
                        orderby f2.f02_createtime
                        select new FormDetailVO { Form = f1, Submit = f2 };

            //依照條件查詢
            if (sDate.HasValue) {

                sDate = DateUtil.ConvertToZeroHour(sDate.Value);

                forms=forms.Where(x => x.Submit.f02_createtime > sDate.Value);
            
            }
            
            if (eDate.HasValue)
            {

                eDate = DateUtil.ConvertToMaxHout(eDate.Value);
                
                forms=forms.Where(x => x.Submit.f02_createtime <= eDate.Value);

            }

            if (!String.IsNullOrEmpty(peo_name))
            {
               forms= forms.Where(x => x.Submit.people.peo_name.Contains(peo_name));

            }

            if (dep_no.HasValue)
            {
                forms=forms.Where(x => x.Submit.people.dep_no == dep_no.Value);

            }


            return forms;
        }

        public int GetSubmitByFormNoCount(int f01_no, DateTime? sDate, DateTime? eDate, String peo_name, int? dep_no)
        {
            return GetSubmitByFormNo(f01_no, sDate, eDate, peo_name, dep_no).Count();
        }

        public IQueryable<FormDetailVO> GetSubmitByFormNo(int f01_no,DateTime? sDate,DateTime? eDate,String peo_name,int? dep_no, int startRowIndex, int maximumRows)
        {
            return GetSubmitByFormNo(f01_no, sDate, eDate, peo_name, dep_no).Skip(startRowIndex).Take(maximumRows);
        }

        #endregion


        #region 用提交人取提交表單
        /// <summary>
        /// 用表單編號取取提交表單
        /// </summary>
        /// <param name="f01_no"></param>
        /// <returns></returns>
        public IQueryable<FormDetailVO> GetSubmitByPeo(int peouid,String keyword)
        {
            var forms = from f1 in model.form01
                        from f2 in model.form02
                        where
                        f1.f01_no == f2.f01_no
                        && f2.peo_uid == peouid
                        orderby f2.f02_createtime
                        select new FormDetailVO { Form = f1, Submit = f2 };

            if (!String.IsNullOrEmpty(keyword)) {
                forms = forms.Where(x => x.Form.f01_name.Contains(keyword));
            }

            return forms;
        }

        public int GetSubmitByPeouidCount(int peouid, String keyword)
        {
            return GetSubmitByPeo(peouid, keyword).Count();
        }

        public IQueryable<FormDetailVO> GetSubmitByPeo(int peouid, String keyword, int startRowIndex, int maximumRows)
        {
            return GetSubmitByPeo(peouid, keyword).Skip(startRowIndex).Take(maximumRows);
        }

        #endregion


    }
}