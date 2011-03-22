using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NXEIP.DAO;
using Entity;
using NLog;

public partial class _10_100400_100403_0 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            this.calendar1._ADDate = Convert.ToDateTime(DateTime.Now.ToString("yyyy-01-01"));
            this.calendar2._ADDate = DateTime.Now;

            //預設個人
            this.hidd_type.Value = "3";
            if (Request.QueryString["r05_no"] != null)
            {
                this.hidd_r05_no.Value = Request.QueryString["r05_no"];

                //維修類別名稱
                using (NXEIPEntities model = new NXEIPEntities())
                {
                    int r05_no = int.Parse(this.hidd_r05_no.Value);
                    this.Navigator1.SubFunc = (from d in model.rep05 where d.r05_no == r05_no select d.r05_name).FirstOrDefault();
                }

                //取資料
                this.LoadData();
            }
        }

        if (Request["__EVENTTARGET"] == this.UpdatePanel1.ClientID && String.IsNullOrEmpty(Request["__EVENTARGUMENT"]))
        {
            this.GridView1.DataBind();
        }
    }

    /// <summary>
    /// 撈資料 1:全府 2:單位 3:個人
    /// </summary>
    private void LoadData()
    {
        SessionObject sobj = new SessionObject();
        this.ObjectDataSource1.SelectParameters["type"].DefaultValue = this.hidd_type.Value;
        this.ObjectDataSource1.SelectParameters["sd"].DefaultValue = this.calendar1._ADDate.ToString("yyyy-MM-dd 00:00:00");
        this.ObjectDataSource1.SelectParameters["ed"].DefaultValue = this.calendar2._ADDate.ToString("yyyy-MM-dd 23:59:59");
        this.ObjectDataSource1.SelectParameters["peo_uid"].DefaultValue = sobj.sessionUserID;
        this.ObjectDataSource1.SelectParameters["dep_no"].DefaultValue = sobj.sessionUserDepartID;
        this.ObjectDataSource1.SelectParameters["r05_no"].DefaultValue = this.hidd_r05_no.Value;

        this.GridView1.DataBind();
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        string[] status = { "", "未處理", "進行中", "已完成" };

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            UtilityDAO dao = new UtilityDAO();

            e.Row.Cells[0].Text = dao.Get_DepartmentName(Convert.ToInt32(e.Row.Cells[0].Text));

            int peo_uid = Convert.ToInt32(e.Row.Cells[1].Text);

            e.Row.Cells[1].Text = dao.Get_PeopleName(peo_uid) + "(" + dao.Get_PeopleExtension(peo_uid) + ")";

            DateTime date = Convert.ToDateTime(e.Row.Cells[2].Text);

            e.Row.Cells[2].Text = new ChangeObject()._ADtoROC(date) + " " + date.ToString("HH:mm");

            //if (e.Row.Cells[4].Text != "3")
            //{
            //    e.Row.Cells[6].Text = "&nbsp;";
            //}

            e.Row.Cells[4].Text = status[int.Parse(e.Row.Cells[4].Text)];

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
            OperatesObject.OperatesExecute(100403, 4, "刪除叫修紀錄 r02_no:" + r02_no);
            this.GridView1.DataBind();
        }


    }

    //刪除按鈕
    protected static bool ShowHide(int peo_uid)
    {
        SessionObject session = new SessionObject();

        if (int.Parse(session.sessionUserID) == peo_uid)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    //評分按鈕是否出現
    protected static bool ShowHide(int peo_uid,string status)
    {
        SessionObject session = new SessionObject();

        if (int.Parse(session.sessionUserID) == peo_uid)
        {
            if (status == "3")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
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
            this.LoadData();
        }
    }

    private void ShowMsg(string msg)
    {
        //string script = "<script>window.alert('" + msg + "');</script>";
        //this.ClientScript.RegisterStartupScript(this.GetType(), "MSG", script);
        JsUtil.AlertJs(this, msg);
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        this.hidd_type.Value = "3";
        this.LoadData();
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        this.hidd_type.Value = "2";
        this.LoadData();
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        this.hidd_type.Value = "1";
        this.LoadData();
    }
    protected void Button5_Click(object sender, EventArgs e)
    {
        Response.Redirect("100403.aspx");
    }
}