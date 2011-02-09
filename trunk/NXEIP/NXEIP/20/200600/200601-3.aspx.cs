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

public partial class _20_200600_200601_3 : System.Web.UI.Page
{

    NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

    SessionObject sessionObj = new SessionObject();
    
    protected void Page_Load(object sender, EventArgs e)
    {

        InitHyperLink();


        if (!Page.IsPostBack) {
            this.ObjectDataSource1.SelectParameters[1].DefaultValue = sessionObj.sessionUserID;
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
        this.hl_list.NavigateUrl = String.Format("200601-2.aspx?tao_no={0}", Request["tao_no"]);
    
    }
    
}