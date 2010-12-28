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
        

    }

   

    protected override void Render(HtmlTextWriter writer)
    {

        String layout = Session["layout_css"]==null?(String)Session["layout_css"]:"Green";

        


        HtmlLink link = new HtmlLink();

        String rootDir = Context.Request.ApplicationPath;

        if (rootDir.Length == 1) {
            rootDir = "";
        }


        link.Href = rootDir + "/style/" + layout + "/css/eip.css";
        link.Attributes.Add("type", "text/css");
        link.Attributes.Add("rel", "stylesheet");
        link.Attributes.Add("media", "screen");

        HtmlLink link2 = new HtmlLink();
        link2.Href = rootDir + "/style/" + layout + "/css/eip-print.css";
        link2.Attributes.Add("type", "text/css");
        link2.Attributes.Add("rel", "stylesheet");
        link2.Attributes.Add("media", "print");



        this.Controls.Clear();
        this.Controls.Add(link);
        this.Controls.Add(link2);
        base.Render(writer);
    }

   

    
}