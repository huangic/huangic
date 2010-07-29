using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _35_350100_350101_2 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            this.SqlDataSource1.SelectParameters["rol_no"].DefaultValue = Request["rol_no"];
            this.GridView1.DataBind();
        }
    }
}
