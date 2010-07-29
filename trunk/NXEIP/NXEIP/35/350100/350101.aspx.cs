using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NXEIP.DAO;

public partial class _35_350100_350101 : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int rowIndex = System.Convert.ToInt32(e.CommandArgument);
        int rol_no = System.Convert.ToInt32(this.GridView1.DataKeys[rowIndex].Value.ToString());

        if (e.CommandName.Equals("disable"))
        {
            new DBObject().ExecuteNonQuery("delete from role where rol_no = " );
            this.GridView1.DataBind();
        }

        if (e.CommandName.Equals("set"))
        {
            Server.Transfer("350101-3.aspx?rol_no=" + rol_no);
        }
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        // 置換UID 為PEOPLE_NAME

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            PeopleDAO dao = new PeopleDAO();
            
            int uid = System.Convert.ToInt32(e.Row.Cells[2].Text);

            e.Row.Cells[2].Text = dao.GetPeopleNameByUid(uid);

            //e.Row.Cells[3].Text = new ChangeObject().ADDTtoROCDT(e.Row.Cells[2].Text);

        }



    }

    
}
