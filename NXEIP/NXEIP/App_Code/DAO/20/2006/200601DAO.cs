using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using Entity;

/// <summary>
/// _200601DAO 的摘要描述
/// </summary>
/// 
namespace NXEIP.DAO
{
    [DataObject(true)]
    public class _200601DAO
    {
        public _200601DAO()
        {
            //
            // TODO: 在此加入建構函式的程式碼
            //
        }

        NXEIPEntities model = new NXEIPEntities();




        public Forum GetFourumById(int id, int peo_uid) {

            Forum forums = (from d in model.taolun
                      where d.tao_status == "1"
                      && d.tao_no == id
                      select new Forum
                      {
                          Id = d.tao_no,
                          Name = d.tao_name,
                          EngName = d.tao_ename,
                          Desc = d.tao_descript,
                          Layout = d.tao_type,
                          ClickCount = d.tao_count ?? 0,
                            NotifyFlag=d.tao_model

                      }).First();



            ProcessManager(forums);
            ProcessPermission(forums, peo_uid);
            ProcessSubscribe(forums, peo_uid);
           





            return forums;
        }



        /// <summary>
        /// 取使用者可以看到了討論區
        /// </summary>
        /// <param name="peouid"></param>
        public IQueryable<Forum> GetForums(int peo_uid)
        {

            //取出所有使用者可以看到的討論區


            //因為會對內容做修改 所以不能使用Quaeryable介面

            List<Forum> forums = (from d in model.taolun
                                       where d.tao_status == "1"

                                       select new Forum
                                       {
                                           Id = d.tao_no,
                                           Name = d.tao_name,
                                           EngName = d.tao_ename,
                                           Desc = d.tao_descript,
                                           Layout = d.tao_type,
                                           ClickCount = d.tao_count ?? 0,
                                           NotifyFlag=d.tao_model
                                           
                                       }).ToList();



            foreach (Forum d in forums)
            {
                //這個有先後順序 不能亂換
                ProcessManager(d);
                ProcessLastModify(d);
                ProcessPermission(d, peo_uid);
                ProcessSubscribe(d, peo_uid);
                ProcessRoot(d, peo_uid);
            }



            

            //IQueryable<Forum> forums=from d in model.taolun w
            return forums.AsQueryable().Where(x=>x.Permission!="00000").Select(x=>x);
        }

        /// <summary>
        /// 設定使用者的權限
        /// </summary>
        /// <param name="f"></param>
        /// <param name="peo_uid"></param>
        private void ProcessPermission(Forum f, int peo_uid)
        {
            //依照使用者版型 與使用者是否登入會員來決定權限表




                //是否為管理員 (屬於管理員)
            if (f.Manager.Select(x => x.peo_uid).Contains(peo_uid)) {
                f.Permission = "11111";
                return;
            }
            
                //是否為會員
            if (CheckPermission(f, peo_uid)) {
                f.Permission = "01111";
                return;
            }
                
                


                //是否為公開版型

            if (f.Layout == "1") {
                f.Permission = "01111";
                return;
            }

            //半公開
            if (f.Layout == "2")
            {
                f.Permission = "00111";
                return;
            }

            //半機密
            if (f.Layout == "3")
            {
                f.Permission = "00011";
                return;
            }

            //全機密 (送他全部都是0)

            f.Permission = "00000";
        }

        /// <summary>
        /// 設定版主
        /// </summary>
        /// <param name="f"></param>
        private void ProcessManager(Forum f)
        {

            IQueryable<people> managers = (from d in model.tao04 where d.tao_no == f.Id select d.people);

            f.Manager = managers.ToList();

        }


        /// <summary>
        /// 設定使用者訂閱狀態
        /// </summary>
        /// <param name="f"></param>
        /// <param name="peo_uid"></param>
        private void ProcessSubscribe(Forum f, int peo_uid)
        {

            //設定使用者的訂閱狀態
            //取會員資料
            tao06 subscribe = (from d in model.tao06
                         where d.tao_no == f.Id
                         && d.peo_uid == peo_uid
                         && d.t06_order=="1"
                         select d).DefaultIfEmpty().First();

            if (subscribe != null)
            {
                f.Subscribe = true;
            }


        }

        /// <summary>
        /// 檢查討論區的機密權限
        /// </summary>
        /// <param name="t"></param>
        /// <param name="peo_uid"></param>
        /// <returns></returns>
        private bool CheckPermission(Forum t, int peo_uid)
        {

            //機密型討論區要看他是不是會員
                int count = (from d in model.tao03
                             where d.tao_no == t.Id
                             && d.t03_status == "1"
                             && d.peo_uid == peo_uid
                             select d).Count();

                if (count >= 1)
                {
                    return true;

                }
                else
                {
                    return false;
                }
            


        }

        /// <summary>
        /// 找出最後的更新日
        /// </summary>
        /// <param name="f"></param>
        private void ProcessLastModify(Forum f)
        { 
            //子代的最後更新文章
            DateTime? t = (from d in model.tao01
                       where d.tao_no == f.Id
                       && d.t01_status == "1"
                       select d).Max(x => x.t01_date);
            f.LastModifyDate = t;
        
        }


        /// <summary>
        /// 設定是否為總管理者
        /// </summary>
        /// <param name="f"></param>
        private void ProcessRoot(Forum f,int peo_uid) {
            //判斷是否為總管理者

            int count =(from d in model.manager where d.peo_uid==peo_uid && d.man_type=="2" select d).Count();

            if (count > 0)
            {
                f.IsRoot = true;

            }
            else {
                f.IsRoot = false;
            }
        }


       

        
    }
}