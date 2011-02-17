using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entity;
using NXEIP.DAO;

public partial class _30_300300_300302_2 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {

            if (Request["typ_no"] != null)
            {
                this.HiddenField1.Value = Request["typ_no"];
                this.div_title.InnerHtml = "課程名稱:" + Request["typ_cname"];
                this.ObjectDataSource1.SelectParameters["typ_parent"].DefaultValue = this.HiddenField1.Value;
                OperatesObject.OperatesExecute(300302, new SessionObject().sessionUserID, 2, "查詢子類別課程 typ_parent:" + this.HiddenField1.Value);
            }
            else
            {
                this.ObjectDataSource1.SelectParameters["typ_parent"].DefaultValue = "-1";
            }
        }
        if (Request["__EVENTTARGET"] == this.UpdatePanel1.ClientID && String.IsNullOrEmpty(Request["__EVENTARGUMENT"]))
        {
            this.GridView1.DataBind();
        }
    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int rowIndex = System.Convert.ToInt32(e.CommandArgument);
        int typ_no = System.Convert.ToInt32(this.GridView1.DataKeys[rowIndex].Value.ToString());

        if (e.CommandName.Equals("del"))
        {
            TypesDAO dao = new TypesDAO();
            types t = dao.GetTypes(typ_no);
            t.typ_status = "2";
            dao.Update();
            this.GridView1.DataBind();
            OperatesObject.OperatesExecute(300302, new SessionObject().sessionUserID, 4, "刪除課程 typ_no:" + typ_no);
        }
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            PeopleDAO dao = new PeopleDAO();
            int uid = System.Convert.ToInt32(e.Row.Cells[3].Text);

            e.Row.Cells[3].Text = dao.GetPeopleNameByUid(uid);

            e.Row.Cells[4].Text = new ChangeObject().ADDTtoROCDT(e.Row.Cells[4].Text);
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        Response.Redirect("300302.aspx");
    }
}