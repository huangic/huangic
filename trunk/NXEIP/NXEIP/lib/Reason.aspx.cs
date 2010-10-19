using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class lib_Reason : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btn_ok_Click(object sender, EventArgs e)
    {
        string reason = this.tbox_reason.Text;
        
        this.Page.ClientScript.RegisterStartupScript(typeof(lib_Reason), "closeThickBox", "self.parent.update('" + reason + "');", true);
    }
}