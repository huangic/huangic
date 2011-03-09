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
    private int[] my_qatno = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            this.LoadData("", null, "");
            my_qatno = new _200702DAO().Get_MyQAtype(int.Parse(new SessionObject().sessionUserID));
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

            e.Row.Cells[1].Text = cobj._ADtoROCDT(data.ask_date.Value);
            if (data.ask_rdate.HasValue)
            {
                e.Row.Cells[2].Text = cobj._ADtoROCDT(data.ask_rdate.Value);
            }
            e.Row.Cells[3].Text = new UtilityDAO().Get_PeopleName(int.Parse(new SessionObject().sessionUserID));

            //是否為自己發問
            int peo_uid = int.Parse(new SessionObject().sessionUserID);
            if (data.ask_peouid != peo_uid)
            {
                e.Row.Cells[5].Text = "&nbsp;";
            }

            //是否可回覆
            bool yes = false;
            if (my_qatno != null)
            {
                for (int i = 0; i < my_qatno.Length; i++)
                {
                    if (my_qatno[i] == data.qat_no)
                    {
                        yes = true;
                        break;
                    }
                }
            }

            if (!yes)
            {
                e.Row.Cells[4].Text = "&nbsp;";
            }

        }
    }



}