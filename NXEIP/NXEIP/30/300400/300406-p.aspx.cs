using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using NXEIP.DAO;
using Entity;


/// <summary>
/// 功能名稱：管理作業 / 場地管理 / 場地使用情況--列印
/// 功能編號：30/300400/300406
/// 撰寫者：Lina
/// 撰寫時間：2011/03/15
/// </summary>
public partial class _30_300400_300406_p : System.Web.UI.Page
{
    ChangeObject changeobj = new ChangeObject();
    DBObject dbo = new DBObject();
    SessionObject sobj = new SessionObject();
    CheckObject checkobj = new CheckObject();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            if (Request["sdate"] != null) this.lab_sdate.Text = Request["sdate"];
            if (Request["edate"] != null) this.lab_edate.Text = Request["edate"];
            if (Request["spot1"] != null) this.lab_spot1.Text = Request["spot1"];
            if (Request["rooms1"] != null) this.lab_rooms1.Text = Request["rooms1"];

            if (this.lab_sdate.Text.Length > 0 && this.lab_edate.Text.Length > 0)
            {
                int pagesize = new PetitionDAO().GetAllCount(this.lab_sdate.Text, this.lab_edate.Text, "0", Convert.ToInt32(this.lab_spot1.Text), Convert.ToInt32(this.lab_rooms1.Text), -1);

                this.ObjectDataSource1.SelectParameters["sdate"].DefaultValue = this.lab_sdate.Text;
                this.ObjectDataSource1.SelectParameters["edate"].DefaultValue = this.lab_edate.Text;
                this.ObjectDataSource1.SelectParameters["status"].DefaultValue = "0";
                this.ObjectDataSource1.SelectParameters["spots1"].DefaultValue = this.lab_spot1.Text;
                this.ObjectDataSource1.SelectParameters["rooms1"].DefaultValue = this.lab_rooms1.Text;
                this.ObjectDataSource1.SelectParameters["loginuser"].DefaultValue = "-1";
                this.ObjectDataSource1.DataBind();
                this.GridView1.DataBind();
                this.GridView1.PageSize = pagesize;
                //登入記錄(功能編號,人員編號,操作代碼[1新增 2查詢 3更新 4刪除 5保留],備註)
                new OperatesObject().ExecuteOperates(300406, sobj.sessionUserID, 2, "場地使用情況--列印");
            }
        }
    }

    #region 調整輸出格式
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string pkno = ((GridView)sender).DataKeys[e.Row.RowIndex].Value.ToString();
            e.Row.Cells[2].Text = new DepartmentsDAO().GetNameByNo(Convert.ToInt32(e.Row.Cells[2].Text));
            e.Row.Cells[3].Text = e.Row.Cells[3].Text.Replace(System.Environment.NewLine, "<br />");
            if (e.Row.Cells[8].Text.Equals("1"))
                e.Row.Cells[8].Text = "送審中";
            else if (e.Row.Cells[8].Text.Equals("2"))
                e.Row.Cells[8].Text = "核可";
            else if (e.Row.Cells[8].Text.Equals("3"))
                e.Row.Cells[8].Text = "不核可";
            else
                e.Row.Cells[8].Text = "自行取消";
        }
    }
    #endregion
}