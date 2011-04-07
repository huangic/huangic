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
		
	}

	private NXEIPEntities model = new NXEIPEntities();

	/// <summary>
	/// 檢查身份證是否重覆 true:重覆
	/// </summary>
	/// <returns></returns>
	public bool CheckIDCard(string idcard)
	{
        idcard = idcard.ToUpper();
		//在職人員
		int count = (from p in model.types
					 where p.typ_code == "work" && p.typ_number == "1" && p.typ_status == "1"
					 from d in model.people
					 where d.peo_idcard == idcard && d.peo_jobtype == p.typ_no
					 select d).Count();
		return count > 0 ? true : false; 
	}

    /// <summary>
    /// 檢查身份證是否重覆(排除本身) true:重覆
    /// </summary>
	public bool CheckIDCard(string idcard, int peo_uid)
	{
        idcard = idcard.ToUpper();
        int count = (from p in model.types
                     where p.typ_code == "work" && p.typ_number == "1" && p.typ_status == "1"
                     from d in model.people
                     where d.peo_idcard == idcard && d.peo_jobtype == p.typ_no && d.peo_uid != peo_uid
                     select d).Count();
		return count > 0 ? true : false;
	}



	/// <summary>
	/// 檢查帳號是否重覆 true:重覆
	/// </summary>
	/// <returns></returns>
	public bool CheckAccount(string accStr)
	{
		//account 啟用帳號
		int count = (from d in model.accounts where d.acc_status == "1" && d.acc_login == accStr select d).Count();

		//帳號申請表 送審中
		count += (from d in model.applys where d.app_login == accStr && d.app_check == "0" select d).Count();

		return count > 0 ? true : false;
	}


	public int GetPeoUidByAccount(string account)
	{
		return ((from d in model.accounts where d.acc_login == account select d).First()).peo_uid;
	}

	public people Get_People(int peo_uid)
	{
		return (from p in model.people where p.peo_uid == peo_uid select p).FirstOrDefault();
	}

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
	/// 取得員工分機
	/// </summary>
	/// <param name="peo_uid">員工UID</param>
	/// <returns></returns>
	public string Get_PeopleExtension(int peo_uid)
	{
		return (from p in model.people where p.peo_uid == peo_uid select p.peo_extension).FirstOrDefault();
	}

	/// <summary>
	/// 取得員工電話
	/// </summary>
	/// <param name="peo_uid">員工UID</param>
	/// <returns></returns>
	public string Get_PeopleTel(int peo_uid)
	{
		return (from p in model.people where p.peo_uid == peo_uid select p.peo_tel).FirstOrDefault();
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
	/// <param name="typ_no">類別ID</param>
	/// <returns></returns>
	public string Get_TypesNumber(int typ_no)
	{
		return (from d in model.types where d.typ_no == typ_no select d.typ_number).FirstOrDefault();
	}

	/// <summary>
	/// 取得類別ID
	/// </summary>
	/// <param name="code">類別代碼(ptype,work,profess)</param>
	/// <param name="number">子類別代碼</param>
	/// <returns></returns>
	public int Get_TypesTypNo(string code, string number)
	{
		return (from d in model.types where d.typ_code == code && d.typ_number == number select d.typ_no).FirstOrDefault();

	}
}