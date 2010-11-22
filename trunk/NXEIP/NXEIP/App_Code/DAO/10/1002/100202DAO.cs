using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using Entity;


namespace NXEIP.DAO
{
    /// <summary>
    /// _100202DAO 的摘要描述
    /// </summary>
    [DataObject(true)]
    public class _100202DAO
    {
        public _100202DAO()
        {
            //
            // TODO: 在此加入建構函式的程式碼
            //
        }


        NXEIPEntities model = new NXEIPEntities();

        public IQueryable<TreatDatailVO> GetTreatData(string status,int? tra_peouid,string keyword){

            var data = from t in model.treat
                       from d in model.treatdetail
                       where t.tre_no == d.tre_no
                       select new TreatDatailVO { Treat = t, Detail = d };

            if (tra_peouid.HasValue) {
                data = data.Where(x => x.Treat.peo_uid == tra_peouid || x.Detail.peo_uid==tra_peouid);
            
            }
            //狀態判斷(1執行中 2 完成 3 執行中但是逾期)
            if (!String.IsNullOrEmpty(status))
            {

                if (status != "3")
                {
                    data = data.Where(x => x.Detail.tde_status == status);
                }
                else {
                    data = data.Where(x => x.Detail.tde_status == "1" && x.Treat.tre_edate.Value<DateTime.Now);
                }
            }


            if (!String.IsNullOrEmpty(keyword)) {
                data=data.Where(x => x.Treat.tre_name.Contains(keyword));
                }

            data= data.OrderBy(x => x.Treat.tre_sdate).ThenBy(x=>x.Treat.tre_createtime);

            return data;

        }


        public int GetTreatDataCount(string status,int? tra_peouid,string keyword){ 
            return this.GetTreatData(status,tra_peouid,keyword).Count();
        }

        
        public IQueryable<TreatDatailVO> GetTreatData(string status,int? tra_peouid,string keyword,int startRowIndex, int maximumRows){
            return GetTreatData(status,tra_peouid,keyword).Skip(startRowIndex).Take(maximumRows);
        }

        /// <summary>
        /// 取我交辦給別人的資料
        /// </summary>
        /// <param name="status"></param>
        /// <param name="tra_peouid"></param>
        /// <returns></returns>
        public IQueryable<TreatDatailVO> GetMyExplainData(int peo_uid)
        {

         

            var data = from t in model.treat
                       from d in model.treatdetail
                       where t.tre_no == d.tre_no
                       && t.peo_uid == peo_uid
                       && d.tde_status!="3"
                       orderby t.tre_edate,t.tre_sdate,t.tre_createtime
                       select new TreatDatailVO { Treat = t, Detail = d };



            return data;
        }

        public IQueryable<TreatDatailVO> GetMyExplainData(int tre_peouid, int startRowIndex, int maximumRows) {
            return GetMyExplainData(tre_peouid).Skip(startRowIndex).Take(maximumRows);
        }

        public int GetMyExplainDataCount(int tre_peouid)
        {
            return GetMyExplainData(tre_peouid).Count();
        }

    }
}