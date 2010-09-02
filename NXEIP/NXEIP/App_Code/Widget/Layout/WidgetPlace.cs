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
            //
            // TODO: 在此加入建構函式的程式碼
            //
        }


        public WidgetPlace(String name){
            this.Name = name;
        }

        private String _Name;

        public String Name
        {
            get { return _Name; }
            set { _Name = value; }
        }
        private WidgetBlock[] _Block;

        public WidgetBlock[] Block
        {
            get { return _Block; }
            set { _Block = value; }
        }




    }
}