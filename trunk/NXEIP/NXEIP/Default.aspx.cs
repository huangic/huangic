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

        if (!Page.IsPostBack)
        {
            //作SESSION

            #if DEBUG
            SessionObject sessionObj = new SessionObject();
            sessionObj.sessionUserDepartID = "1";
            sessionObj.sessionUserAccount = "admin";
            sessionObj.sessionUserID = "1";
            #endif

        }
    }
    
}
