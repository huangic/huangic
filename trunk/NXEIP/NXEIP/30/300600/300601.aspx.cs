using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NXEIP.DAO;
using Entity;
using NLog;

public partial class _30_300600_300601 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            this.calendar1._ADDate = Convert.ToDateTime(DateTime.Now.ToString("yyyy-01-01"));
            this.calendar2._ADDate = DateTime.Now;

            //叫修分類管理者
            this.ObjectDataSource2.SelectParameters["peo_uid"].DefaultValue = new SessionObject().sessionUserID;
            this.lv_cat.DataBind();
            if (this.lv_cat.Items.Count > 0)
            {
                this.hidd_r05no.Value = this.lv_cat.DataKeys[0].Value.ToString();
                this.loadData();
            }
        }

        if (Request["__EVENTTARGET"] == this.UpdatePanel1.ClientID && String.IsNullOrEmpty(Request["__EVENTARGUMENT"]))
        {
            this.GridView1.DataBind();
        }
    }

    private void loadData()
    {
        this.ObjectDataSource1.SelectParameters["r05_no"].DefaultValue = this.hidd_r05no.Value;
        this.ObjectDataSource1.SelectParameters["sd"].DefaultValue = this.calendar1._ADDate.ToString("yyyy-MM-dd 00:00:00");
        this.ObjectDataSource1.SelectParameters["ed"].DefaultValue = this.calendar2._ADDate.ToString("yyyy-MM-dd 23:59:59");
        this.GridView1.DataBind();
    }

    protected void lv_cat_ItemCommand(object sender, ListViewCommandEventArgs e)
    {
        if (e.CommandName == "click_cat")
        {
            this.hidd_r05no.Value = this.lv_cat.DataKeys[e.Item.DataItemIndex].Value.ToString();
            this.loadData();
        }
        
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int r02_no = int.Parse(this.GridView1.DataKeys[int.Parse(e.CommandArgument.ToString())].Value.ToString());

        if (e.CommandName.Equals("del"))
        {
            _100403DAO dao = new _100403DAO();
            rep02 d = dao.GetRep02ByNo(r02_no);
            d.r02_status = "4";
            d.r02_createuid = int.Parse(new SessionObject().sessionUserID);
            d.r02_createtime = DateTime.Now;
            dao.UpData();
            OperatesObject.OperatesExecute(300601, 4, "刪除叫修紀錄 r02_no:" + r02_no);
            this.GridView1.DataBind();
        }
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            UtilityDAO dao = new UtilityDAO();

            e.Row.Cells[0].Text = dao.Get_DepartmentName(Convert.ToInt32(e.Row.Cells[0].Text));

            int peo_uid = Convert.ToInt32(e.Row.Cells[1].Text);

            e.Row.Cells[1].Text = dao.Get_PeopleName(peo_uid) + "(" + dao.Get_PeopleExtension(peo_uid) + ")";

            DateTime date = Convert.ToDateTime(e.Row.Cells[2].Text);

            e.Row.Cells[2].Text = new ChangeObject()._ADtoROC(date) + " " + date.ToString("HH:mm");

        }
    }
    protected void Button4_Click(object sender, EventArgs e)
    {
        if (this.calendar1._ADDate > this.calendar2._ADDate)
        {
            this.ShowMsg("起始日期需小於迄日期");
        }
        else
        {
            this.loadData();
        }
    }
    private void ShowMsg(string msg)
    {
        string script = "<script>window.alert('" + msg + "');</script>";
        this.ClientScript.RegisterStartupScript(this.GetType(), "MSG", script);
    }
    /// <summary>
    /// 下載Excel
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button1_Click(object sender, EventArgs e)
    {

    }

   
}