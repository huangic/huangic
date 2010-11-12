using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;
using NXEIP.Lib;

namespace NXEIP.Widget
{
    public class WidgetParam
    {
        private String param = "";

        private JObject paramJson = null;
        public WidgetParam(String json) {
            this.param = json;
            try
            {
                paramJson = JObject.Parse(param);
            }
            catch { 
            
            }
        }

        public String this[String name]{
           //取這個name的值

            get {
                try
                {

                    return (String)paramJson[name];
                }
                catch {
                    return String.Empty;
                }
            }
        }
    }
}