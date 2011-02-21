using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NXEIP.DAO;
using Entity;
using NLog;

public partial class _10_100200_100204 : System.Web.UI.Page
{
    private NXEIPEntities model = new NXEIPEntities();
    private Logger logger = LogManager.GetCurrentClassLogger();

    protected void Page_Load(object sender, EventArgs e)
    {
        this.Navigator1.SubFunc = "我的最新消息";

        if (!this.IsPostBack)
        {
            //上方最新消息
            this.ObjectDataSource1.SelectParameters["use"].DefaultValue = "2";
            this.ObjectDataSource1.SelectParameters["key"].DefaultValue = "-1";
            this.hidd_use.Value = "2";
            
            //個人最新消息
            this.ObjectDataSource2.SelectParameters["peo_uid"].DefaultValue = new SessionObject().sessionUserID;
            if (Request.QueryString["status"] != null)
            {
                this.ObjectDataSource2.SelectParameters["status"].DefaultValue = Request["status"];
                this.GridView1.DataBind();
                this.GridView1.PageIndex = int.Parse(Request.QueryString["pageIndex"]);
            }
            else
            {
                this.ObjectDataSource2.SelectParameters["status"].DefaultValue = "0";
            }
            
        }
    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int n01_no = int.Parse(this.GridView1.DataKeys[int.Parse(e.CommandArgument.ToString())].Value.ToString());

        if (e.CommandName.Equals("del"))
        {
            New01DAO dao = new New01DAO();
            new01 d = dao.GetByNo(n01_no);
            d.n01_status = "4";
            dao.SaveChang();
            this.GridView1.DataBind();
            this.ListView1.DataBind();
        }
        
        if (e.CommandName.Equals("edit"))
        {
            Response.Redirect("100204-1.aspx?mode=edit&n01_no=" + n01_no + "&status=" + this.ddl_status.SelectedValue + "&pageIndex=" + this.GridView1.PageIndex);
        }
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            UtilityDAO dao = new UtilityDAO();

            if (e.Row.Cells[1].Text.Equals("1"))
            {
                e.Row.Cells[1].Text = "通過";
            }
            if (e.Row.Cells[1].Text.Equals("2"))
            {
                e.Row.Cells[1].Text = "未通過";
            }
            if (e.Row.Cells[1].Text.Equals("3"))
            {
                e.Row.Cells[1].Text = "送審中";
            }
            try
            {
                e.Row.Cells[2].Text = dao.Get_PeopleName(int.Parse(e.Row.Cells[2].Text));
            }
            catch { }
            try
            {
                e.Row.Cells[3].Text = new ChangeObject().ADDTtoROCDT(e.Row.Cells[3].Text);
                if (e.Row.Cells[3].Text.Equals("0"))
                {
                    e.Row.Cells[3].Text = "";
                }
            }
            catch { }
        }
    }

    protected void ddl_status_SelectedIndexChanged(object sender, EventArgs e)
    {
        //個人最新消息
        this.ObjectDataSource2.SelectParameters["peo_uid"].DefaultValue = new SessionObject().sessionUserID;
        this.ObjectDataSource2.SelectParameters["status"].DefaultValue = this.ddl_status.SelectedValue;
        this.GridView1.DataBind();
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        Response.Redirect("100204-1.aspx?mode=new&status=" + this.ddl_status.SelectedValue+"&pageIndex="+this.GridView1.PageIndex);
    }

    /// <summary>
    /// 全府
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button7_Click(object sender, EventArgs e)
    {
        this.ObjectDataSource1.SelectParameters["use"].DefaultValue = "2";
        this.ObjectDataSource1.SelectParameters["key"].DefaultValue = "-1";
        this.ListView1.DataBind();
        this.hidd_use.Value = "2";
    }

    /// <summary>
    /// 單位
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button8_Click(object sender, EventArgs e)
    {
        this.ObjectDataSource1.SelectParameters["use"].DefaultValue = "1";
        this.ObjectDataSource1.SelectParameters["key"].DefaultValue = "-1";
        this.ListView1.DataBind();
        this.hidd_use.Value = "1";
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (this.tbox_search.Text.Length > 0)
        {
            this.ObjectDataSource1.SelectParameters["use"].DefaultValue = this.hidd_use.Value;
            this.ObjectDataSource1.SelectParameters["key"].DefaultValue = this.tbox_search.Text;
            this.ListView1.DataBind();
        }
        else
        {
           
        }
    }
}