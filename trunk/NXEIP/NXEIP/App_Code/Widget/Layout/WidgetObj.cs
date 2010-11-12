using System;
using System.Collections;
using System.Linq;
using System.Web;


namespace NXEIP.Widget
{
    /// <summary>
    /// WidgetObj 的摘要描述
    /// </summary>
    [Serializable]
    public class WidgetObj
    {
        public WidgetObj()
        {
           
        }

       

        public WidgetPlace[] Place { get; set; }
       



        public static WidgetObj GetInstance(String[] div){
            WidgetObj wobj = new WidgetObj();
            wobj.Place = new WidgetPlace[div.Length];

            for (int i = 0; i < div.Length; i++) {
                wobj.Place[i] = new WidgetPlace(div[i]);
            }

            return wobj;
        }

    }
}