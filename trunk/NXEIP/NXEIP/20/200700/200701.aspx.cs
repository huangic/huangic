using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NXEIP.DAO;
using Entity;

public partial class _20_200700_200701 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {

        }

        if (Request["__EVENTTARGET"] == this.UpdatePanel1.ClientID && String.IsNullOrEmpty(Request["__EVENTARGUMENT"]))
        {
            this.GridView1.DataBind();
        }
    }


    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int rowIndex = int.Parse(e.CommandArgument.ToString());
        int qat_no = int.Parse(this.GridView1.DataKeys[rowIndex].Values[4].ToString());

        if (e.CommandName.Equals("del"))
        {
            _200701DAO dao = new _200701DAO();

            qatype d = dao.Get_qatype(qat_no);
            d.qat_status = "2";
            dao.Update();

            OperatesObject.OperatesExecute(200701, 4, "刪除問答類別 qat_no:" + d.qat_no);

            this.GridView1.DataBind();
        }
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int qat_no = int.Parse(e.Row.Cells[0].Text);

            //qat_name,qat_self,qat_s06no,qat_r05no
            string self = this.GridView1.DataKeys[e.Row.DataItemIndex].Values[1].ToString();

            if (self == "1")
            {
                e.Row.Cells[0].Text = "自訂類別";
            }

            if (self == "2")
            {
                int sfu_no = int.Parse(this.GridView1.DataKeys[e.Row.DataItemIndex].Values[2].ToString());
                e.Row.Cells[0].Text = "業務資訊類";
                e.Row.Cells[1].Text = new SysfuctionDAO().GetNameByNO(sfu_no);
            }

            if (self == "3")
            {
                int r05_no = int.Parse(this.GridView1.DataKeys[e.Row.DataItemIndex].Values[3].ToString());
                e.Row.Cells[0].Text = "維修類";
                e.Row.Cells[1].Text = new Rep05DAO().GetRep05Name(r05_no);
            }

        }
    }
}