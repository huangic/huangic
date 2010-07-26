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


        private int _WidgetID;
        public int WidgetID
        {
            get { return _WidgetID; }
            set { _WidgetID = value; }
        }

        private String _Order;
        public String Order
        {
            get { return Order; }
            set { Order = value; }
        }



    }
}