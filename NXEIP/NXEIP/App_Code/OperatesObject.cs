using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NXEIP.DAO;
using Entity;

/// <summary>
/// Operates 的摘要描述
/// </summary>
public class OperatesObject
{
    public OperatesObject()
    {
        //
        // TODO: 在此加入建構函式的程式碼
        //
    }

    /// <summary>
    /// 新增操作記錄
    /// </summary>
    /// <param name="sfu_no">功能編號</param>
    /// <param name="peo_uid">人員編號</param>
    /// <param name="fuction">操作代碼1:新增 2:查詢 3:更新 4:刪除 5:保留</param>
    /// <param name="memo">備註</param>
    public void ExecuteOperates(int sfu_no, string peo_uid, int fuction, string memo)
    {
        string ope_no = Guid.NewGuid().ToString("N");
        
        operates data = new operates();
        data.ope_no = ope_no;
        data.sfu_no = sfu_no;
        data.peo_uid = Convert.ToInt32(peo_uid);
        data.ope_logintime = System.DateTime.Now;
        data.ope_fuction = fuction;
        data.ope_memo = memo;

        //新增
        OperatesDAO oDao = new OperatesDAO();
        oDao.Addoperates(data);
        oDao.Update();

    }

    /// <summary>
    /// 新增人員登入記錄
    /// </summary>
    /// <param name="peo_uid"></param>
    /// <param name="acc_no"></param>
    /// <param name="loginIP"></param>
    /// <param name="sessionID"></param>
    public void ExecuteLoginLog(int peo_uid,int acc_no,string loginIP,string sessionID)
    {
        loginlog log = new loginlog();

        log.log_no = Guid.NewGuid().ToString("N");
        log.log_sessionid = sessionID;
        log.log_peouid = peo_uid;
        log.log_logintime = System.DateTime.Now;
        log.log_accno = acc_no;
        log.log_ip = loginIP;
        log.log_status = "1";

        loginlogDAO logDao = new loginlogDAO();
        logDao.Addloginlog(log);
        logDao.Update();

    }
}
