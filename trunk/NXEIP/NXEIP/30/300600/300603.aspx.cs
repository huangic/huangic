using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NXEIP.DAO;
using Entity;
using NLog;

public partial class _30_300600_300603 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {

        }

        if (Request["__EVENTTARGET"] == this.UpdatePanel1.ClientID && String.IsNullOrEmpty(Request["__EVENTARGUMENT"]))
        {
            this.GridView1.DataBind();
        }
    }


    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int r05_no = int.Parse(this.GridView1.DataKeys[int.Parse(e.CommandArgument.ToString())].Value.ToString());

        if (e.CommandName.Equals("del"))
        {
            Rep05DAO dao = new Rep05DAO();
            rep05 d = dao.GetRep05(r05_no);
            d.r05_status = "2";
            d.r05_createuid = int.Parse(new SessionObject().sessionUserID);
            d.r05_createtime = DateTime.Now;
            dao.Update();
            OperatesObject.OperatesExecute(300602, 4, string.Format("刪除維修項目 r05_no:{0}", r05_no));
            this.GridView1.DataBind();
        }

        if (e.CommandName.Equals("edit"))
        {
            Response.Redirect("300603-2.aspx?r05_no="+r05_no);
        }
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        UtilityDAO udao = new UtilityDAO();
        ChangeObject cdao = new ChangeObject();

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[1].Text = udao.Get_PeopleName(int.Parse(e.Row.Cells[1].Text));

            e.Row.Cells[2].Text = cdao.ADDTtoROCDT(e.Row.Cells[2].Text);

        }
    }
}