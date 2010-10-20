using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Reflection;
using ComLib.Reflection;



namespace NXEIP.Lib
{
    /// <summary>
    /// EntityLib 的摘要描述
    /// </summary>
    public class EntityLib
    {
        public EntityLib()
        {
            //
            // TODO: 在此加入建構函式的程式碼
            //
        }

        public static String[] GetAllEntityProperties(Object obj)
        {
            IList<String> properties = new List<String>();
            PropertyInfo[] propInfo = GetEntityPropertyInfo(obj);

            return propInfo.Select(d => d.Name).ToArray();
        }

        public static PropertyInfo[] GetEntityPropertyInfo(Object obj)
        {
            PropertyInfo[] propInfo = obj.GetType().GetProperties();


            var properties = (from p in propInfo
                              where p.CanWrite == true && p.Name.Contains("_")
                              select p).ToArray();




            return properties;
        }

        public static void CopyProperties(Object source, Object dist)
        {
            foreach (var propInfo in EntityLib.GetEntityPropertyInfo(source))
            {
                ReflectionUtils.CopyPropertyValue(source, dist, propInfo);
            }
        }
    }
}