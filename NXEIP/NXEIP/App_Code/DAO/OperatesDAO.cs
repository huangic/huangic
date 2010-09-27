using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Web;
using Entity;

/// <summary>
/// OperatesDAO 的摘要描述
/// </summary>
public class OperatesDAO
{
	public OperatesDAO()
	{
		//
		// TODO: 在此加入建構函式的程式碼
		//
	}

    private NXEIPEntities model = new NXEIPEntities();

    /// <summary>
    /// 查詢所有資料
    /// </summary>
    /// <returns></returns>
    public IQueryable<operates> GetAll()
    {
        return (from data in model.operates orderby data.ope_logintime descending select data);
    }

    /// <summary>
    /// 查詢符合筆數之資料
    /// </summary>
    /// <param name="startRowIndex">起始筆數</param>
    /// <param name="maximumRows">結束筆數</param>
    /// <returns></returns>
    public IQueryable<operates> GetAll(int startRowIndex, int maximumRows)
    {
        return GetAll().Skip(startRowIndex).Take(maximumRows);
    }

    /// <summary>
    /// 查詢總筆數
    /// </summary>
    /// <returns></returns>
    public int GetAllCount()
    {
        return GetAll().Count();
    }

    public void Addoperates(operates operates)
    {
        model.AddTooperates(operates);
    }

    public int Update()
    {
        return model.SaveChanges();
    }
}