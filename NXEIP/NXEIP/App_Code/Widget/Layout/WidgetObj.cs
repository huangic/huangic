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

        private WidgetPlace[] _Place;

        public WidgetPlace[] Place
        {
            get { return _Place; }
            set { this._Place = value; }
        }




    }
}