using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IMMENSITY.SWFUploadAPI;
using System.Runtime.Serialization.Json;
using Entity;
using System.Security.Cryptography;
using System.Text;

public partial class _20_200600_200601_9 : System.Web.UI.Page
{

    NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

    SessionObject sessionObj = new SessionObject();
    
    protected void Page_Load(object sender, EventArgs e)
    {

        InitHyperLink();


        if (!Page.IsPostBack) {
            this.ObjectDataSource1.SelectParameters[0].DefaultValue = Request["tao_no"];
            this.GridView1.DataBind();
        }

        //判斷來自JS 使用_doPostBack(updatePanel,"") 的情況 
        if (Request["__EVENTTARGET"] == this.UpdatePanel1.ClientID && String.IsNullOrEmpty(Request["__EVENTARGUMENT"]))
        {
            this.GridView1.DataBind();
        }


    }

    protected String GetROCDT(DateTime? dt)
    {
        if (dt.HasValue)
        {
            return new ChangeObject().ADDTtoROCDT(dt.Value);

        }
        return "";
    }

    protected String GetStatus(String status)
    {
        if (status == "0") {
            return "審核中";

        }else{
            return "通過";
        }
    }

    


    private void InitHyperLink() {
     //   this.hl_list.NavigateUrl = String.Format("200601-2.aspx?tao_no={0}", Request["tao_no"]);
    
    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {

        int index = ((GridViewRow)((Button)e.CommandSource).NamingContainer).RowIndex;

        int tao_no = int.Parse(GridView1.DataKeys[index].Values["tao_no"].ToString());
        int t03_no = int.Parse(GridView1.DataKeys[index].Values["t03_no"].ToString());
     

        #region //刪除會員
        if (e.CommandName == "del")
        {
            //刪除回應

            tao03 t = new tao03();

            t.tao_no = tao_no;
            t.t03_no = t03_no;


            using (NXEIPEntities model = new NXEIPEntities())
            {
                model.tao03.Attach(t);
                t.t03_status = "2";
                
                model.SaveChanges();

            }

            this.GridView1.DataBind();

        }
        #endregion


        #region //通過會員
        if (e.CommandName == "apply")
        {
            //刪除回應

            tao03 t = new tao03();

            t.tao_no = tao_no;
            t.t03_no = t03_no;


            using (NXEIPEntities model = new NXEIPEntities())
            {
                model.tao03.Attach(t);
                t.t03_status = "1";


                model.SaveChanges();

            }

            this.GridView1.DataBind();

        }
        #endregion

    }


}