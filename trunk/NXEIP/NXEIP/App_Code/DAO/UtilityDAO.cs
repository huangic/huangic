using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Entity;

/// <summary>
/// UtilityDAO 的摘要描述
/// </summary>
public class UtilityDAO
{
	public UtilityDAO()
	{
		//
		// TODO: 在此加入建構函式的程式碼
		//
	}

    private NXEIPEntities model = new NXEIPEntities();

    /// <summary>
    /// 取得員工姓名
    /// </summary>
    /// <param name="peo_uid">員工UID</param>
    /// <returns></returns>
    public string Get_PeopleName(int peo_uid)
    {
        return (from p in model.people where p.peo_uid == peo_uid select p.peo_name).FirstOrDefault();
    }

    /// <summary>
    /// 取得員工身份證字號
    /// </summary>
    /// <param name="peo_uid">員工UID</param>
    /// <returns></returns>
    public string Get_PeopleIDCard(int peo_uid)
    {
        return (from p in model.people where p.peo_uid == peo_uid select p.peo_idcard).FirstOrDefault();
    }

    /// <summary>
    /// 使用員工UID取得部門名稱
    /// </summary>
    /// <param name="dep_no">部門ID</param>
    /// <returns></returns>
    public string Get_DepartmentNameByUID(int peo_uid)
    {
        int dep_no = (from p in model.people where p.peo_uid == peo_uid select p.dep_no).FirstOrDefault();
        return Get_DepartmentName(dep_no);
    }

    /// <summary>
    /// 取得員工職稱
    /// </summary>
    /// <param name="peo_uid"></param>
    /// <returns></returns>
    public string Get_PeopleProName(int peo_uid)
    {
        int pro_no = (from p in model.people where p.peo_uid == peo_uid select p.peo_pfofess.Value).FirstOrDefault();
        return Get_TypesCName(pro_no);
    }

    /// <summary>
    /// 取得部門名稱
    /// </summary>
    /// <param name="dep_no">部門ID</param>
    /// <returns></returns>
    public string Get_DepartmentName(int dep_no)
    {
        return (from d in model.departments where d.dep_no == dep_no  select d.dep_name).FirstOrDefault();
    }

    /// <summary>
    /// 取得各類別中文名稱
    /// </summary>
    /// <param name="dep_no">類別ID</param>
    /// <returns></returns>
    public string Get_TypesCName(int typ_no)
    {
        return (from d in model.types where d.typ_no == typ_no select d.typ_cname).FirstOrDefault();
    }

    /// <summary>
    /// 取得員工職稱編號typ_no
    /// </summary>
    /// <param name="dep_no">員工UID</param>
    /// <returns></returns>
    public int Get_TypesNo(int peo_uid)
    {
        return (from d in model.people where d.peo_uid == peo_uid select d.peo_pfofess.Value).FirstOrDefault();
    }

    /// <summary>
    /// 取得各類別代碼
    /// </summary>
    /// <param name="dep_no">類別ID</param>
    /// <returns></returns>
    public string Get_TypesNumber(int typ_no)
    {
        return (from d in model.types where d.typ_no == typ_no select d.typ_number).FirstOrDefault();
    }
}