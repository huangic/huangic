using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Web.Configuration;
using System.Web.UI.WebControls;

/// <summary>
/// SessionObject 的摘要描述
/// </summary>
public class SessionObject : System.Web.UI.Page
{
	public SessionObject()
	{
		
	}

    #region 存取設定Session UserID
    /// <summary>
    ///		存取設定Session UserID
    ///		用途:存放 員工序號
    /// </summary>
    public string sessionUserID
    {
        get
        {
            try
            {
         
                    return (string)Session["UserID"];
            
            }
            catch
            {
                
                    return null;
            }
        }
        set
        {
            Session.Add("UserID", value);
        }
    }
    #endregion

    #region 存取設定Session UserName
    /// <summary>
    ///		存取設定Session UserName
    ///		用途:存放	員工名稱
    /// </summary>
    public string sessionUserName
    {
        get
        {
            try
            {
                return (string)Session["UserName"];
            }
            catch
            {
                return null;
            }
        }
        set
        {
            Session.Add("UserName", value);
        }
    }
    #endregion

    #region 存取設定Session UserDepartName
    /// <summary>
    ///		存取設定Session UserDepartName
    ///		用途:存放	員工部門名稱
    /// </summary>
    public string sessionUserDepartName
    {
        get
        {
            try
            {
                return (string)Session["UserDepartName"];
            }
            catch
            {
                return null;
            }
        }
        set
        {
            Session.Add("UserDepartName", value);
        }
    }
    #endregion

    #region 存取設定Session UserDepartID
    /// <summary>
    ///		存取設定Session UserDepartID
    ///		用途:存放	員工部門ID
    /// </summary>
    public string sessionUserDepartID
    {
        get
        {
            try
            {
                return (string)Session["UserDepartID"];
            }
            catch
            {
                return null;
            }
        }
        set
        {
            Session.Add("UserDepartID", value);
        }
    }
    #endregion

    #region 存取設定Session UserAccount
    /// <summary>
    ///		存取設定Session UserAccount
    ///		用途:存放	班級名稱
    /// </summary>
    public string sessionUserAccount
    {
        get
        {
            try
            {
                return (string)Session["UserAccount"];
            }
            catch
            {
                return null;
            }
        }
        set
        {
            Session.Add("UserAccount", value);
        }
    }
    #endregion

    #region 存取設定Session UserPeopleTypeID
    /// <summary>
    ///		存取設定Session UserPeopleTypeID
    ///		用途:存放	人員類別ID
    /// </summary>
    public string sessionUserPeopleTypeID
    {
        get
        {
            try
            {
                return (string)Session["UserPeopleTypeID"];
            }
            catch
            {
                return null;
            }
        }
        set
        {
            Session.Add("UserPeopleTypeID", value);
        }
    }
    #endregion

    #region 存取設定Session UserPeopleTypeCode
    /// <summary>
    ///		存取設定Session UserPeopleTypeCode
    ///		用途:存放	人員類別Code
    /// </summary>
    public string sessionUserPeopleTypeCode
    {
        get
        {
            try
            {
                return (string)Session["UserPeopleTypeCode"];
            }
            catch
            {
                return null;
            }
        }
        set
        {
            Session.Add("UserPeopleTypeCode", value);
        }
    }
    #endregion

    #region 判斷 Session 是否存在
    /// <summary>
    /// 判斷 Session 是否存在 不存在:自動導回登入畫面
    /// </summary>
    /// <returns></returns>
    public void CheckSession()
    {
        try
        {
            if (Session["UserID"] == null)
            {
                string path = "login.aspx";
                try
                {
                    path = WebConfigurationManager.AppSettings["LogoutPath"];
                }
                catch
                {
                    path = "login.aspx";
                }

                path = "../../" + path;
                this.Response.Write("<script language='JavaScript'>\n window.alert('閒置過久，請重新登入!');\n</script>");
                this.Session.RemoveAll();
                Response.Write("<script>window.open('" + path + "','_top','')</script>");
            }

        }
        catch
        {
            string path = "login.aspx";
            try
            {
                path = System.Configuration.ConfigurationManager.AppSettings["LogoutPath"];
            }
            catch
            {
                path = "login.aspx";
            }
            path = "../../" + path;
            Response.Write("<script>alert('閒置過久，請重新登入!')</script>");
            this.Session.RemoveAll();
            Response.Write("<script>window.open('" + path + "','_top','')</script>");
        }
    }

    public void CheckSession_root()
    {
        try
        {
            if (Session["UserID"] == null)
            {
                string path = "login.aspx";
                try
                {
                    path = WebConfigurationManager.AppSettings["LogoutPath"];
                }
                catch
                {
                    path = "login.aspx";
                }
                this.Response.Write("<script language='JavaScript'>\n window.alert('閒置過久，請重新登入!');\n</script>");
                this.Session.RemoveAll();
                Response.Write("<script>window.open('" + path + "','_top','')</script>");
            }
        }
        catch
        {
            string path = "login.aspx";
            try
            {
                path = System.Configuration.ConfigurationManager.AppSettings["LogoutPath"];
            }
            catch
            {
                path = "login.aspx";
            }
            Response.Write("<script>alert('閒置過久，請重新登入!')</script>");
            this.Session.RemoveAll();
            Response.Write("<script>window.open('" + path + "','_top','')</script>");
        }

    }
    #endregion 判斷 Session 是否存在

    #region 清除所有 Session
    /// <summary>
    /// 清除所有 Session
    /// </summary>
    public void RemoveSession()
    {
        this.Session.RemoveAll();
    }
    #endregion

    #region 登出系統
    /// <summary>
    /// 登出系統
    /// </summary>
    public void LogOut()
    {
        string path = "login.aspx";

        try
        {
            path = System.Configuration.ConfigurationManager.AppSettings["LogoutPath"];
        }
        catch
        {
            path = "login.aspx";
        }

        path = "http://" + Request.Url.Authority + "/" + Request.PhysicalApplicationPath.Split('\\')[1] + "/" + path;
        this.Session.RemoveAll();

        Response.Write("<script>window.open('" + path + "','_top','')</script>");

    }
    #endregion

    #region 取得客戶端真實IP Address
    /// <summary>    
    /// 取得客戶端真實IP Address    
    /// </summary>    
    /// <returns></returns>    
    public string GetIpAddress()
    {
        string strIpAddr = string.Empty;

        if (HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] == null || HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].IndexOf("unknown") > 0)
        {
            strIpAddr = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
        }
        else if (HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].IndexOf(",") > 0)
        {
            strIpAddr = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].Substring(1, HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].IndexOf(",") - 1);
        }
        else if (HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].IndexOf(";") > 0)
        {
            strIpAddr = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].Substring(1, HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].IndexOf(";") - 1);
        }
        else
        {
            strIpAddr = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        }
        return strIpAddr; ;
    }
    #endregion

    #region 設定 Session Timeout
    /// <summary>
    /// 標題：設定 Session Timeout
    /// 備註：
    /// </summary>
    /// <param name="e"></param>
    protected override void OnPreLoad(EventArgs e)
    {
        //base.OnPreLoad(e);

        //CheckSession();

        //try
        //{
        //    Session.Timeout = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["SessionTimeOut"]);
        //}
        //catch
        //{
        //    Session.Timeout = 30;
        //}
    }
    #endregion

    #region 組合錯誤訊息的顯示格式
    /// <summary>
    /// 組合錯誤訊息的顯示格式
    /// </summary>
    /// <param name="aMSG">錯誤訊息</param>
    /// <returns>格式化的錯誤訊息</returns>
    public string ShowErrorMessage(string aMSG)
    {
        string MSG = "<table width=\"100%\" border=\"0\" cellpadding=\"4\" cellspacing=\"0\" class=\"table-top\"><tr>"
            + "<td height=\"24\" valign=\"bottom\" align=\"center\"><font size=\"3\">錯誤訊息通知</font></td></tr></table>"
            + "<table width=\"100%\" border=\"0\" cellpadding=\"2\" cellspacing=\"1\" bgcolor=\"#666666\"><tr><td bgcolor=\"#ffffff\">"
            + "<table width=\"100%\" border=\"0\" cellpadding=\"1\" cellspacing=\"1\" bgcolor=\"#506b63\"><tr>"
            + "<td class=\"table-04\" height=\"99\" bgcolor=\"#ffffff\" align=\"center\"><font size=\"5\" color=\"#ff0000\">此為錯誤訊息通知頁，請通知系統管理者，並列印以下錯誤訊息，我們將盡快為您解決</font>"
            + "</td></tr><tr bgcolor=\"#ffffff\"><td class=\"table-04\">" + aMSG + "</td>"
            + "</tr></table></td></tr></table>";
        return MSG;
    }
    #endregion

    public void ShowMessage(string msg)
    {
        Page.ClientScript.RegisterStartupScript(typeof(SessionObject), "msg", "alert('" + msg + "');", true);
    }
}
