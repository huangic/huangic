using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// DateUtil 的摘要描述
/// </summary>
public static class DateUtil
{
	public DateUtil()
	{
		//
		// TODO: 在此加入建構函式的程式碼
		//
	}
    /// <summary>
    /// 轉換成yyyy-MM-dd-00-00-00
    /// </summary>
    /// <param name="dt"></param>
    /// <returns></returns>
    public static DateTime ConvertToZeroHour(this DateTime dt) {
        //DateTime newDt = dt;
        String dt_str=dt.ToString("yyyy-MM-dd")+"-00-00-00";

        return DateTime.ParseExact(dt_str,"yyyy-MM-dd-HH-mm-ss",null);

    }
    /// <summary>
    /// 轉換成yyyy-MM-dd-23-59-59
    /// </summary>
    /// <param name="dt"></param>
    /// <returns></returns>
    public static DateTime ConvertToMaxHour(this DateTime dt)
    {
        //DateTime newDt = dt;
        String dt_str = dt.ToString("yyyy-MM-dd") + "-23-59-59";

        return DateTime.ParseExact(dt_str, "yyyy-MM-dd-HH-mm-ss", null);

    }

}