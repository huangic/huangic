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
        if (!this.IsPostBack)
        {
            this.ObjectDataSource1.SelectParameters[0].DefaultValue = new SessionObject().sessionUserID;
            this.GridView1.DataBind();
        }
    }

    protected void btn_ok_Click(object sender, EventArgs e)
    {
        string reason = this.tbox_reason.Text;
        this.Page.ClientScript.RegisterStartupScript(typeof(lib_Reason), "closeThickBox", "self.parent.update('" + reason + "');", true);
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        //this.div_show.Visible = true;

    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("sel"))
        {
            this.tbox_reason.Text += this.GridView1.Rows[Convert.ToInt32(e.CommandArgument)].Cells[0].Text;
        }
    }
}