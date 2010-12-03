using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using Entity;


namespace NXEIP.DAO
{
    /// <summary>
    /// _200108DAO 的摘要描述
    /// </summary>
    [DataObject(true)]
    public class _200108DAO
    {


        public _200108DAO()
        {
            //
            // TODO: 在此加入建構函式的程式碼
            //
        }

        NXEIPEntities model = new NXEIPEntities();

        /// <summary>
        /// 用表單編號取取提交表單
        /// </summary>
        /// <param name="f01_no"></param>
        /// <returns></returns>
        public IQueryable<FormDetailVO> GetSubmitByPeoUid(int peo_Uid) {
            var forms = from f1 in model.form01
                        from f2 in model.form02
                        where
                        f1.f01_no == f2.f01_no
                        && f2.peo_uid == peo_Uid
                        orderby f2.f02_createtime
                        select new FormDetailVO { Form = f1, Submit = f2 };

            return forms;
        }

        public int GetSubmitByPeoUidCount(int peo_Uid)
        {
            return GetSubmitByPeoUid(peo_Uid).Count();
        }

        public IQueryable<FormDetailVO> GetSubmitByFormNo(int peo_Uid, int startRowIndex, int maximumRows)
        {
            return GetSubmitByPeoUid(peo_Uid).Skip(startRowIndex).Take(maximumRows);
        }

    }
}