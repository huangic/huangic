using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Entity;

namespace NXEIP.Lib
{
    /// <summary>
    /// CssUtil 的摘要描述
    /// </summary>
    public class CssUtil
    {
        public CssUtil()
        {
            //
            // TODO: 在此加入建構函式的程式碼
            //
        }

        public static String GetInitCssLayout()
        {

            String layout = "Green";
            int peo_uid = int.Parse(new SessionObject().sessionUserID);
            
            using (NXEIPEntities model = new NXEIPEntities())
            {
                //刪除
                try
                {
                    layout = (from d in model.setting where d.peo_uid == peo_uid && d.set_variable == "CssLayout" select d.set_value).Single();
                }
                catch { 
                
                }   
              

            }



            return layout;
        }


    }
}