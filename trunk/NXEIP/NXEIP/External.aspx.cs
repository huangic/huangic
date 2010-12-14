using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class External : System.Web.UI.Page
{

    public string url = "";
    
    
    protected void Page_Load(object sender, EventArgs e)
    {
        String url = Request["url"];

        if (url.Contains("?")) { 
            url+="&";
        }else{
            url += "?";
        }

        url += "token=" + new SessionObject().sessionLogInID;

        this.url = url;
    }
}