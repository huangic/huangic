using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entity;
using NXEIP.DAO;

public partial class _30_300500_300501 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            OperatesObject.OperatesExecute(300501, new SessionObject().sessionUserID, 2, "查詢類別管理");
        }
    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int rowIndex = System.Convert.ToInt32(e.CommandArgument);
        int s06_no = System.Convert.ToInt32(this.GridView1.DataKeys[rowIndex].Value.ToString());

        if (e.CommandName.Equals("del"))
        {
            Sys06DAO dao = new Sys06DAO();
            sys06 data = dao.GetByS06No(s06_no);
            data.s06_status = "2";
            dao.Update();
            this.GridView1.DataBind();
        }
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            sysfuction sfuData = new SysfuctionDAO().GetBySfuNo(int.Parse(e.Row.Cells[0].Text));
            e.Row.Cells[0].Text = sfuData.sfu_name;

            if (e.Row.Cells[1].Text.Equals("0"))
            {
                e.Row.Cells[1].Text = e.Row.Cells[2].Text;
                e.Row.Cells[2].Text = "&nbsp;";
            }
            else
            {
                sys06 data = new Sys06DAO().GetByS06No(int.Parse(e.Row.Cells[1].Text));
                e.Row.Cells[1].Text = data.s06_name;
            }

            PeopleDAO dao = new PeopleDAO();
            int uid = System.Convert.ToInt32(e.Row.Cells[3].Text);
            e.Row.Cells[3].Text = dao.GetPeopleNameByUid(uid);
            e.Row.Cells[4].Text = new ChangeObject().ADDTtoROCDT(e.Row.Cells[4].Text);
        }
    }
}