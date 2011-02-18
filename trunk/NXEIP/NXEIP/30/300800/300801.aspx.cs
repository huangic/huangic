using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _30_300800_300801 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            this.calendar1._ADDate = DateTime.Now;
            this.calendar2._ADDate = DateTime.Now;

            this.ObjectDataSource1.SelectParameters["dep_no"].DefaultValue = new SessionObject().sessionUserDepartID;
            this.ObjectDataSource1.SelectParameters["sd"].DefaultValue = this.calendar1._ADDate.ToString("yyyy-MM-dd 00:00:00");
            this.GridView1.DataBind();

        }

        //判斷來自JS 使用_doPostBack(updatePanel,"") 的情況 
        if (Request["__EVENTTARGET"] == this.UpdatePanel1.ClientID && String.IsNullOrEmpty(Request["__EVENTARGUMENT"]))
        {
            this.GridView1.DataBind();
        }
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            ChangeObject cObj = new ChangeObject();
            UtilityDAO udao = new UtilityDAO();

            DateTime sd = Convert.ToDateTime(e.Row.Cells[2].Text);
            e.Row.Cells[2].Text = cObj._ADtoROC(sd);
            e.Row.Cells[1].Text = udao.Get_PeopleName(int.Parse(e.Row.Cells[1].Text));
            
            //核審狀態
            //1.審核通過
            //2.審核不通過
            //3.送審中
            //4.刪除
            string tmp = e.Row.Cells[3].Text;
            if (tmp == "1")
            {
                e.Row.Cells[3].Text = "通過";
            }
            if (tmp == "2")
            {
                e.Row.Cells[3].Text = "不通過";
            }
            if (tmp == "3")
            {
                e.Row.Cells[3].Text = "未審核";
            }
            
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        if (this.calendar1.CheckDateTime() && this.calendar2.CheckDateTime())
        {
            this.ObjectDataSource1.SelectParameters["dep_no"].DefaultValue = new SessionObject().sessionUserDepartID;
            this.ObjectDataSource1.SelectParameters["sd"].DefaultValue = this.calendar1._ADDate.ToString("yyyy-MM-dd 00:00:00");
            this.ObjectDataSource1.SelectParameters["ed"].DefaultValue = this.calendar2._ADDate.ToString("yyyy-MM-dd 23:59:59");
            this.GridView1.DataBind();
        }
        else
        {
            JsUtil.AlertJs(this, "請輸入正確日期!");
        }
    }


}