using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NXEIP.DAO;
using Entity;

public partial class _35_350100_350105 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request["pageIndex"]))
            {
                this.GridView1.DataBind();
                this.GridView1.PageIndex = Convert.ToInt32(Request["pageIndex"]);
            }
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        Response.Write("<script>location.replace('350105-1.aspx?mode=new')</script>");
    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("modify"))
        {
            string sfu_no = this.GridView1.DataKeys[Convert.ToInt32(e.CommandArgument)].Value.ToString();
            string pageIndex = this.GridView1.PageIndex.ToString();

            //操作記錄
            new OperatesObject().ExecuteOperates(350105, new SessionObject().sessionUserID, 2, "查詢功能編號:" + sfu_no);

            string url = "350105-1.aspx?mode=modify&sfu_no=" + sfu_no + "&pageIndex=" + pageIndex;
            Response.Redirect(url);
        }

        if (e.CommandName.Equals("del"))
        {
            string sfu_no = this.GridView1.DataKeys[Convert.ToInt32(e.CommandArgument)].Value.ToString();
        }
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.Cells[5].Text.Equals("1"))
            {
                e.Row.Cells[5].Text = "上架";
            }
            if (e.Row.Cells[5].Text.Equals("2"))
            {
                e.Row.Cells[5].Text = "<span style='color: #FF0000'>下架</span>";
            }

            e.Row.Cells[1].Text = new SysDAO().GetNameBySysNo(Convert.ToInt32(e.Row.Cells[1].Text));
            e.Row.Cells[6].Text = new PeopleDAO().GetPeopleNameByUid(Convert.ToInt32(e.Row.Cells[6].Text));
            e.Row.Cells[7].Text = new ChangeObject().ADDTtoROCDT(e.Row.Cells[7].Text);
        }
    }
}