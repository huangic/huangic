using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

public partial class lib_CssLayout : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        String layout = "Default";
        
        
        HtmlLink link = new HtmlLink();
        link.Href = Context.Request.ApplicationPath + "/"+layout+"/css/eip.css";
        link.Attributes.Add("type", "text/css");
        link.Attributes.Add("rel", "stylesheet");
        Page.Header.Controls.Add(link);

    }
}