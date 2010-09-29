﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entity;
using NXEIP.DAO;

public partial class _30_300300_300302 : System.Web.UI.Page
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
        int typ_no = System.Convert.ToInt32(this.GridView1.DataKeys[rowIndex].Value.ToString());
        string typ_cname = this.GridView1.Rows[rowIndex].Cells[1].Text;

        if (e.CommandName.Equals("del"))
        {
            TypesDAO dao = new TypesDAO();
            types t = dao.GetTypes(typ_no);

            t.typ_status = "2";

            dao.Update();

            this.GridView1.DataBind();
        }
        if (e.CommandName.Equals("sel"))
        {
            Response.Redirect("300302-2.aspx?typ_no=" + typ_no + "&typ_cname=" + typ_cname, true);
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
}