using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //this.WebPartManager1.DisplayMode=this.WebPartManager1.DisplayModes["Design"];
        //CacheUtil.AddItem("AAA", "BBB");
        // SessionObject sessionObj = new SessionObject();

        //作SESSION

        //沒有登入就轉登入頁
        //if (String.IsNullOrEmpty(sessionObj.sessionUserID))
        //{
        // Response.Redirect("~/login.aspx");
        //}
        //else {

        //throw new Exception("測試錯誤訊息");

        Response.Redirect("~/10/100500/100501.aspx");
        //}  

        //登入過就轉

    }
}
