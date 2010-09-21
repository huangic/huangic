using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entity;
using NXEIP.DAO;

public partial class _35_350300_350302 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {

        }
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int rowIndex = System.Convert.ToInt32(e.CommandArgument);
        int arg_no = System.Convert.ToInt32(this.GridView1.DataKeys[rowIndex].Value.ToString());

        if (e.CommandName.Equals("del"))
        {
            ArgumentsDAO dao = new ArgumentsDAO();
            arguments data = dao.GetByArgNo(arg_no);
            dao.DeleteArguments(data);
            dao.Update();
        }
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            
        }
    }
}