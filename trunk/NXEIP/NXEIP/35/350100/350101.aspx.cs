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

    }




    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        // 置換UID 為PEOPLE_NAME

        if (e.Row.RowType == DataControlRowType.DataRow) { 
        
           int uid=System.Convert.ToInt32(e.Row.Cells[2].Text);

          // e.Row.Cells[2].Text = dao.GetPeopleNameByUid(uid);


        }

    }

    protected void btn_ok_Click(object sender, EventArgs e)
    {

    }
    protected void btn_cancel_Click(object sender, EventArgs e)
    {

    }
}
