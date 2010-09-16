using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Practices.EnterpriseLibrary.Caching;
using NLog;

/// <summary>
/// 全域快取物件
/// 設定於Web.config
/// </summary>
public class CacheUtil
{


    static Logger logger = LogManager.GetCurrentClassLogger();
    public CacheUtil()
	{
		//
		// TODO: 在此加入建構函式的程式碼
		//
        
	}
    /// <summary>
    /// 加入快取
    /// </summary>
    /// <param name="key">KEY</param>
    /// <param name="obj">快取物件</param>
    public static void AddItem(String key, Object obj) {
        try
        {
            ICacheManager cache = CacheFactory.GetCacheManager();

            cache.Add(key, obj);
        }
        catch (Exception ex) {
            logger.Debug(ex.Message);
        }
    }

    /// <summary>
    /// 取得快取物件
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public static Object GetItem(String key){

        try
        {
            ICacheManager cache = CacheFactory.GetCacheManager();

            return cache.GetData(key);
        }
        catch (Exception ex)
        {
            logger.Debug(ex.Message);
            return null;
        }
       
    }

    /// <summary>
    /// 移除快取
    /// </summary>
    /// <param name="key"></param>
    public static void Remove(String key) {
        try{
        ICacheManager cache = CacheFactory.GetCacheManager();
        cache.Remove(key);
        }
        catch (Exception ex)
        {
            logger.Debug(ex.Message);
        }
    }

    /// <summary>
    /// 清除全部快取
    /// </summary>
    /// <param name="key"></param>
    public static void Clear()
    {

        try
        {
            ICacheManager cache = CacheFactory.GetCacheManager();
            cache.Flush();
        }
        catch (Exception ex)
        {
            logger.Debug(ex.Message);
        }
    }


    
}