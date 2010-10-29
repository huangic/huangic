using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _20_200400_200402_2 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            this.Navigator1.SubFunc = "講義下載";

            if (Request.QueryString["e02_no"] != null)
            {
                this.hidd_no.Value = Request.QueryString["e02_no"];
                this.ODS_1.SelectParameters["e02_no"].DefaultValue = this.hidd_no.Value;

            }
        }
    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }

    
}