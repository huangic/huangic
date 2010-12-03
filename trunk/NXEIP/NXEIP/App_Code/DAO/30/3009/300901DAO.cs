using System;
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

        /// <summary>
        /// 用表單編號取取提交表單
        /// </summary>
        /// <param name="f01_no"></param>
        /// <returns></returns>
        public IQueryable<FormDetailVO> GetSubmitByFormNo(int f01_no) {
            var forms = from f1 in model.form01
                        from f2 in model.form02
                        where
                        f1.f01_no == f2.f01_no
                        && f1.f01_no == f01_no
                        orderby f2.f02_createtime
                        select new FormDetailVO { Form = f1, Submit = f2 };

            return forms;
        }

        public int GetSubmitByFormNoCount(int f01_no)
        {
          return GetSubmitByFormNo(f01_no).Count();
        }

        public IQueryable<FormDetailVO> GetSubmitByFormNo(int f01_no, int startRowIndex, int maximumRows) {
            return GetSubmitByFormNo(f01_no).Skip(startRowIndex).Take(maximumRows);
        }

    }
}