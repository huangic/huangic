using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace NXEIP.Widget
{
    /// <summary>
    /// WidgetPlace 的摘要描述
    /// </summary>
    [Serializable]
    public class WidgetPlace
    {
        public WidgetPlace()
        {
            
        }


        public WidgetPlace(String name){
            this.Name = name;
        }

        

        public String Name { get; set; }
       
        public WidgetBlock[] Block { get; set; }




    }
}