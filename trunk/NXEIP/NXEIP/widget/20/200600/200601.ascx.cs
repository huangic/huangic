using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entity;
using System.Web.UI.HtmlControls;
using System.Data.Objects.SqlClient;

public partial class widget_20_200600_200601 : NXEIP.Widget.WidgetBaseControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
         SessionObject sessionObj = new SessionObject();

        string user_login = sessionObj.sessionUserAccount;
        int peouid = int.Parse(sessionObj.sessionUserID);
       


       


    }

    public override string Name
    {
        get { return "NewTopic"; }
    }

    public override void loadWidget()
    {
       
    }
}