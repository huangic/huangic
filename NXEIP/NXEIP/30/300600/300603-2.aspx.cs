using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NXEIP.DAO;
using Entity;
using NLog;

public partial class _30_300600_300603_2 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            this.hidd_r05no.Value = Request.QueryString["r05_no"];

            this.Navigator1.SubFunc = new Rep05DAO().GetRep05Name(int.Parse(this.hidd_r05no.Value));

            this.ObjectDataSource1.SelectParameters["r05_no"].DefaultValue = this.hidd_r05no.Value;
        }

        if (Request["__EVENTTARGET"] == this.UpdatePanel1.ClientID && String.IsNullOrEmpty(Request["__EVENTARGUMENT"]))
        {
            this.GridView1.DataBind();
        }
    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int r06_no = int.Parse(this.GridView1.DataKeys[int.Parse(e.CommandArgument.ToString())].Values[0].ToString());

        if (e.CommandName.Equals("del"))
        {
            Rep06DAO dao = new Rep06DAO();
            rep06 d = dao.GetRep06(r06_no);
            d.r06_status = "2";
            d.r06_createuid = int.Parse(new SessionObject().sessionUserID);
            d.r06_createtime = DateTime.Now;
            dao.Update();
            OperatesObject.OperatesExecute(300603, 4, string.Format("刪除維修類別 r06_no:{0}", r06_no));
            this.GridView1.DataBind();
        }
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        UtilityDAO udao = new UtilityDAO();
        ChangeObject cdao = new ChangeObject();
        Rep06DAO dao = new Rep06DAO();

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.Cells[0].Text == "0")
            {
                e.Row.Cells[0].Text = e.Row.Cells[1].Text;
                e.Row.Cells[1].Text = "&nbsp;";
            }
            else
            {
                e.Row.Cells[0].Text = dao.GetRep06Name(int.Parse(e.Row.Cells[0].Text));
            }


            e.Row.Cells[3].Text = udao.Get_PeopleName(int.Parse(e.Row.Cells[3].Text));

            e.Row.Cells[4].Text = cdao.ADDTtoROCDT(e.Row.Cells[4].Text);

        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        JsUtil.RedirectJs(this, "300603.aspx");
    }
}