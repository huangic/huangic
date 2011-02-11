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

public partial class _20_200600_200601_8 : System.Web.UI.Page
{

    NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

    SessionObject sessionObj = new SessionObject();
    
    protected void Page_Load(object sender, EventArgs e)
    {

        InitHyperLink();


        if (!Page.IsPostBack) {
            this.ObjectDataSource1.SelectParameters[0].DefaultValue = sessionObj.sessionUserID;
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


    private void InitHyperLink() {
     //   this.hl_list.NavigateUrl = String.Format("200601-2.aspx?tao_no={0}", Request["tao_no"]);
    
    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {

        int index = ((GridViewRow)((Button)e.CommandSource).NamingContainer).RowIndex;

        int tao_no = int.Parse(GridView1.DataKeys[index].Values["ForumId"].ToString());
        int t01_no = int.Parse(GridView1.DataKeys[index].Values["Id"].ToString());
        int t05_no = int.Parse(GridView1.DataKeys[index].Values["FolderId"].ToString());

        #region //刪除回應
        if (e.CommandName == "del")
        {
            //刪除回應

            tao05 t = new tao05();

            t.t05_no = t05_no;


            using (NXEIPEntities model = new NXEIPEntities())
            {
                model.tao05.Attach(t);
                model.tao05.DeleteObject(t);
                model.SaveChanges();

            }

            this.GridView1.DataBind();

        }
        #endregion
        
        
       

    }


}