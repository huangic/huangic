using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Objects;
using NXEIP.DAO;

public partial class _35_350300_350301_1 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            if (Request["date"] != null && Request["sfu"] != null && Request["opt"] != null && Request["key"] != null && Request["value"] != null)
            {
                this.ObjectDataSource1.SelectParameters["date"].DefaultValue = Request["date"];
                this.ObjectDataSource1.SelectParameters["sfu"].DefaultValue = Request["sfu"];
                this.ObjectDataSource1.SelectParameters["opt"].DefaultValue = Request["opt"];
                this.ObjectDataSource1.SelectParameters["key"].DefaultValue = Request["key"];
                this.ObjectDataSource1.SelectParameters["value"].DefaultValue = Request["value"];

                this.GridView1.DataBind();

                object[] list = new object[Request.QueryString.Count];
                for (int i = 0; i < Request.QueryString.Count; i++)
                {
                    list[i] = Request.QueryString[i];
                }

                OperatesObject.OperatesExecute(350301, 2, "查詢操作記錄,起迄日期:{0},功能:{1},操作模式{2},人員查詢{3}:{4}",list);
                

            }
        }
    }

    protected void btn_ok_Click(object sender, EventArgs e)
    {
        JsUtil.RedirectJs(this, "350301.aspx");
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        SysfuctionDAO sfudao = new SysfuctionDAO();
        UtilityDAO udao = new UtilityDAO();
        ChangeObject changObj = new ChangeObject();

        string[] opt = {"新增","查詢","更新","刪除" };

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].Text = sfudao.GetNameByNO(int.Parse(e.Row.Cells[0].Text));
            
            e.Row.Cells[1].Text = udao.Get_PeopleName(int.Parse(e.Row.Cells[1].Text));

            e.Row.Cells[2].Text = changObj.ADDTtoROCDT(e.Row.Cells[2].Text);

            e.Row.Cells[3].Text = opt[int.Parse(e.Row.Cells[3].Text) - 1];

        }
    }
}