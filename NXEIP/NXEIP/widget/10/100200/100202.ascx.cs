using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class  widget_10_100200_100202 : NXEIP.Widget.WidgetBaseControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.ObjectDataSource1.SelectParameters[0].DefaultValue = "1";
        this.ObjectDataSource1.SelectParameters[1].DefaultValue = new SessionObject().sessionUserID;
        this.ObjectDataSource1.SelectParameters[2].DefaultValue = "";

        this.DataList1.DataBind();
    }


    

    public override string Name
    {
        get { return "Treat"; }
    }

    public override void loadWidget()
    {

        

      
    }


    


   
}