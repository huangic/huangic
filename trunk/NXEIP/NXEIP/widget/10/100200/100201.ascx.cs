using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class widget_10_100200_100201 : NXEIP.Widget.WidgetBaseControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.ObjectDataSource1.SelectParameters["peo_uid"].DefaultValue = new SessionObject().sessionUserID;
        this.DataList1.DataBind();
    }

    public override string Name
    {
        get { return "message"; }
    }

    public override void loadWidget()
    {

    }
}