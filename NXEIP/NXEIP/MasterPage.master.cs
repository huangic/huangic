using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterPage : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {

      //註冊THICKBOX 的變數
        String ThickboxInit="var tb_pathToImage = \""+Page.ResolveUrl("~/image/loadingAnimation.gif")+"\"";
        ScriptManager.RegisterClientScriptBlock(this, typeof(MasterPage), "ThickBox", ThickboxInit, true);


        


    }


    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
        Page.Header.DataBind();
    }



    protected void logout_Click(object sender, EventArgs e)
    {
        //移除SESSION
        Session.RemoveAll();

        Response.Redirect("~/Default.aspx");

    }
}
