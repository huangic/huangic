using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using NXEIP.DAO;
using Entity;

public partial class _30_300200_300202_a : System.Web.UI.Page
{
    DBObject dbo = new DBObject();
    CheckObject checkobj = new CheckObject();
    SessionObject sobj = new SessionObject();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            if (Request["que_no"] != null) this.lab_queno.Text = Request["que_no"];
            if (Request["the_no"] != null) this.lab_theno.Text = Request["the_no"];
            if (Request["ans_no"] != null) this.lab_ansno.Text = Request["ans_no"];

            this.Navigator1.SubFunc = "清單";
            string the_type = "";
            #region 問卷基本資料
            Entity.theme theData = new ThemeDAO().GetByNo(Convert.ToInt32(this.lab_queno.Text), Convert.ToInt32(this.lab_theno.Text));
            if (theData != null)
            {
                this.lab_thename.Text = theData.the_name;
                this.lab_quename.Text = theData.questionary.que_name;
                the_type = theData.the_type;
            }
            #endregion
        }
    }

    #region 調整輸出格式
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[3].Text = new ChangeObject().ADDTtoROCDT(e.Row.Cells[3].Text);
        }
    }
    #endregion
}