using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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
        string strSQL = "insert into operates (ope_no,sfu_no,peo_uid,ope_logintime,ope_fuction,ope_memo) values ('" + ope_no + "'," + sfu_no + "," + peo_uid + ",GETDATE()," + fuction + ",'" + memo + "')";

        new DBObject().ExecuteNonQuery(strSQL);
    }
}
