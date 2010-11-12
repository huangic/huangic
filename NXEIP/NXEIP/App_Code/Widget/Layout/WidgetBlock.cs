using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NXEIP.Widget
{
    /// <summary>
    /// WidgetBlock 的摘要描述
    /// </summary>
    [Serializable]
    public class WidgetBlock
    {
        public WidgetBlock()
        {
            
        }


        public WidgetBlock(int WidgetID)
        {
            this.WidgetID = WidgetID;

        }



        public int WidgetID { get; set; }


        public String Order { get; set; }

        public String Param { get; set; }

    }
}