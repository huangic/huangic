using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class login2 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            this.ImageButton1.Attributes.Add("onmouseout", "MM_swapImgRestore()");
            this.ImageButton1.Attributes.Add("onmouseover", "MM_swapImage('ImageButton1','','image/login-06-1.gif',1)");

            this.Image1.Attributes.Add("onmouseout", "MM_swapImgRestore()");
            this.Image1.Attributes.Add("onmouseover", "MM_swapImage('Image1','','image/login_PIN_1.jpg',1)");
        }
    }

    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {

    }
}