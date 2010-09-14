using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Practices.EnterpriseLibrary.Caching;

/// <summary>
/// 全域快取物件
/// 設定於Web.config
/// </summary>
public class CacheUtil
{
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
        ICacheManager cache=CacheFactory.GetCacheManager();

        cache.Add(key, obj);

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
        catch {
            return null;
        }
       
    }

    /// <summary>
    /// 移除快取
    /// </summary>
    /// <param name="key"></param>
    public static void Remove(String key) {
        ICacheManager cache = CacheFactory.GetCacheManager();
        cache.Remove(key);
    }

    /// <summary>
    /// 清除全部快取
    /// </summary>
    /// <param name="key"></param>
    public static void Clear(String key)
    {
        ICacheManager cache = CacheFactory.GetCacheManager();
        cache.Flush();
    }


    
}