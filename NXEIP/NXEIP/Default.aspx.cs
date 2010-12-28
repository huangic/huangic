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
        //登入成功之後的前置作業

          //LAYOUT的設定

        String layout = "Green";


        //寫入SESSION
        Session["layout_css"] = layout;

        //




        Response.Redirect("~/10/100500/100501.aspx");
        //}  

        //登入過就轉

    }
}
