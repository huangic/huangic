using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class lib_PeopleDetail : System.Web.UI.UserControl
{

    public string peo_uid { get; set; }
    
    
    protected void Page_Load(object sender, EventArgs e)
    {
        ScriptManager.RegisterClientScriptInclude(this, typeof(UserControl), "jquery_popup", ResolveClientUrl("~/js/jquery.open.js"));


        string script = @"$(function(){$('.popup').popupWindow({'centerBrowser':1,'width':300,'height':160 });    })";

        ScriptManager.RegisterClientScriptBlock(this, typeof(UserControl), "Popup", script, true);
    }

    protected override void Render(HtmlTextWriter writer)
    {
       

        
        this.hl_detail.NavigateUrl = String.Format(this.hl_detail.NavigateUrl,peo_uid);
        
        
        base.Render(writer);
    }


}