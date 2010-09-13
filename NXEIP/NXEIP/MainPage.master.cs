using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MainPage : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //註冊THICKBOX 的變數
        String ThickboxInit = "var tb_pathToImage = \"" + Page.ResolveUrl("~/image/loadingAnimation.gif") + "\"";
        ScriptManager.RegisterClientScriptBlock(this, typeof(MasterPage), "ThickBox", ThickboxInit, true);


        
    }

    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
        Page.Header.DataBind();
    }

}
