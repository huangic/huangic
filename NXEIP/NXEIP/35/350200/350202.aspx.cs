using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entity;
using NXEIP.DAO;
using IMMENSITY.SWFUploadAPI;
using System.Runtime.Serialization.Json;
using NLog;


public partial class _35_350200_350202 : System.Web.UI.Page
{
    private static Logger logger = LogManager.GetCurrentClassLogger();
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            
        }


        //判斷來自JS 使用_doPostBack(updatePanel,"") 的情況 
        if (Request["__EVENTTARGET"] == this.UpdatePanel1.ClientID && String.IsNullOrEmpty(Request["__EVENTARGUMENT"]))
        {
            this.GridView1.DataBind();
        }


    }

  
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int rowIndex = System.Convert.ToInt32(e.CommandArgument);

        int typ_no = System.Convert.ToInt32(this.GridView1.DataKeys[rowIndex].Value.ToString());

        if (e.CommandName.Equals("modify"))
        {
            
            // InitEdit(dep_no);
            return;
        }

        if (e.CommandName.Equals("disable"))
        {
            // delete(dep_no);
            logger.Debug("disable");
            delete(typ_no);
            return;
        }
    }

    private void delete(int typ_no) {
        TypesDAO dao = new TypesDAO();
        types t=dao.GetTypes(typ_no);

        t.typ_status = "2";

        dao.Update();

        this.GridView1.DataBind();

    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            PeopleDAO dao = new PeopleDAO();
            int uid = System.Convert.ToInt32(e.Row.Cells[3].Text);

            if (e.Row.Cells[2].Text.Equals("1"))
            {
                e.Row.Cells[2].Text = "人事管理";
            }
            if (e.Row.Cells[2].Text.Equals("2"))
            {
                e.Row.Cells[2].Text = "行政管理";
            }

            e.Row.Cells[3].Text = dao.GetPeopleNameByUid(uid);
            e.Row.Cells[4].Text = new ChangeObject().ADDTtoROCDT(e.Row.Cells[4].Text);
        }

    }
}
