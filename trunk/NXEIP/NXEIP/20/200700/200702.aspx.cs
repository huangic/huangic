using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entity;
using NXEIP.DAO;

public partial class _20_200700_200702 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            this.LoadData("", null, "");

        }

        if (Request["__EVENTTARGET"] == this.UpdatePanel1.ClientID && String.IsNullOrEmpty(Request["__EVENTARGUMENT"]))
        {
            this.GridView1.DataBind();
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="self"></param>
    /// <param name="qat_no"></param>
    /// <param name="key"></param>
    private void LoadData(string self, int? qat_no, string key)
    {
        this.ObjectDataSource1.SelectParameters["self"].DefaultValue = self;
        if (qat_no.HasValue)
        {
            this.ObjectDataSource1.SelectParameters["qat_no"].DefaultValue = qat_no.Value.ToString();
        }
        else
        {
            this.ObjectDataSource1.SelectParameters["qat_no"].DefaultValue = string.Empty;
        }
        this.ObjectDataSource1.SelectParameters["key"].DefaultValue = key;

        this.GridView1.DataBind();
    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int ask_no = int.Parse(this.GridView1.DataKeys[e.Row.DataItemIndex].Value.ToString());

            ask data = new _200702DAO().Get_ask(ask_no);
            
            ChangeObject cobj = new ChangeObject();

            string str = "問：" + e.Row.Cells[0].Text;

            if (!string.IsNullOrEmpty(data.ask_answer))
            {
                str += "<br/>答：" + data.ask_answer;
            }

            //e.Row.Cells[0].Text = str;

            e.Row.Cells[1].Text = cobj._ADtoROCDT(data.ask_date.Value);
            if (data.ask_rdate.HasValue)
            {
                e.Row.Cells[2].Text = cobj._ADtoROCDT(data.ask_rdate.Value);
            }
            e.Row.Cells[3].Text = new UtilityDAO().Get_PeopleName(int.Parse(new SessionObject().sessionUserID));
        }
    }
}