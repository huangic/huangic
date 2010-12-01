using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entity;

public partial class widget_10_100500_100507 : NXEIP.Widget.WidgetBaseControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
         SessionObject sessionObj = new SessionObject();

        string user_login = sessionObj.sessionUserAccount;

        //取使用者所有的可用功能
        this.ObjectDataSource1.SelectParameters[0].DefaultValue = user_login;
    }

    public override string Name
    {
        get { return "ApplicationCenter"; }
    }

    public override void loadWidget()
    {
       
    }
}