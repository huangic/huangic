using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NXEIP.DAO;
using Entity;
using NLog;

public partial class _30_300600_300602 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (this.IsPostBack)
        {
            //string.Format("300602-1.aspx?mode=modify&modal=true&r01_no={0}&r05_no={1}&TB_iframe=true", Eval("r01_no"), Eval("r05_no"));
        }

        if (Request["__EVENTTARGET"] == this.UpdatePanel1.ClientID && String.IsNullOrEmpty(Request["__EVENTARGUMENT"]))
        {
            this.GridView1.DataBind();
        }
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int r01_no = int.Parse(this.GridView1.DataKeys[int.Parse(e.CommandArgument.ToString())].Values[0].ToString());
        int r05_no = int.Parse(this.GridView1.DataKeys[int.Parse(e.CommandArgument.ToString())].Values[1].ToString());

        if (e.CommandName.Equals("del"))
        {
            Rep01DAO dao = new Rep01DAO();
            rep01 d = dao.GetRep01(r05_no, r01_no);
            int peo_uid = d.r01_peouid.Value;
            dao.deleteRep01(d);
            dao.Update();

            OperatesObject.OperatesExecute(300602, 4, string.Format("刪除管理者 r05_no:{0} peo_uid:{1}", r05_no, peo_uid));
            this.GridView1.DataBind();
        }
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        UtilityDAO udao = new UtilityDAO();
        Rep05DAO r05dao = new Rep05DAO();

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].Text = r05dao.GetRep05Name(int.Parse(e.Row.Cells[0].Text));

            e.Row.Cells[1].Text = udao.Get_DepartmentNameByUID(int.Parse(e.Row.Cells[1].Text));

            e.Row.Cells[2].Text = udao.Get_PeopleName(int.Parse(e.Row.Cells[2].Text));

            e.Row.Cells[3].Text = e.Row.Cells[3].Text.Substring(0, 1) == "1" ? "是" : "否";

            e.Row.Cells[4].Text = e.Row.Cells[4].Text.Substring(1, 1) == "1" ? "是" : "否";

            e.Row.Cells[5].Text = e.Row.Cells[5].Text.Substring(2, 1) == "1" ? "是" : "否";
        }
    }
}