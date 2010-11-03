using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using Newtonsoft.Json.Linq;
namespace NXEIP.Lib
{
    /// <summary>
    /// JsonLib 的摘要描述
    /// </summary>
    public class JsonLib
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();


        public JsonLib()
        {
            
        }


        private static String GetRequestJson(HttpRequest request)
        {
            using (Stream stream = request.InputStream)
            {
                string json = string.Empty;
                string responseJson = string.Empty;
                if (stream.Length != 0)
                {
                    using (System.IO.StreamReader streamReader = new StreamReader(stream))
                    {
                        json = streamReader.ReadToEnd();

                        logger.Debug(json);
                    }

                }
                return json;
            }


        }


        public static JObject GetRequestJsonObject(HttpRequest request)
        {
            try
            {

                return JObject.Parse(GetRequestJson(request));
            }
            catch
            {
                return null;
            }

        }

        public static String GetRequestJsonString(HttpRequest request)
        {
            return GetRequestJson(request);
        }
    }
}