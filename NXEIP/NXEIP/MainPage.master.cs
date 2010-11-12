using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NXEIP.Lib;
using System.Globalization;

public partial class MainPage : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //註冊THICKBOX 的變數
        String ThickboxInit = "var tb_pathToImage = \"" + Page.ResolveUrl("~/image/loadingAnimation.gif") + "\"";

        try
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(MasterPage), "ThickBox", ThickboxInit, true);
        }
        catch { 
        
        }
        //顯示名稱與日期
        SessionObject sessionObj = new SessionObject();
        this.lt_name.Text = sessionObj.sessionUserName;


        TaiwanCalendar tc = new TaiwanCalendar();


        this.lt_date.Text = tc.GetYear(DateTime.Now) + "年" + tc.GetMonth(DateTime.Now) + "月" + tc.GetDayOfMonth(DateTime.Now) + "日";

        
    }

    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
        Page.Header.DataBind();
    }

    protected void logout_Click(object sender, EventArgs e)
    {
        //登出記錄
        new OperatesObject().ExecuteLogOutLog(new SessionObject().sessionLogInID);

        //移除SESSION
        Session.RemoveAll();
        CacheUtil.Clear();
        Response.Redirect("~/Default.aspx");
    }
}
