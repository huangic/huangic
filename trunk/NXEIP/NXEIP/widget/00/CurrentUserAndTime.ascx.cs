using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NXEIP.Widget;

public partial class widget_00_CurrentUserAndTime : WidgetBaseControl
{
    public override String SettingUrl{ get{return "AAA";}}
    
    
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public override string Name
    {
        get { return "UserAndTime"; }
    }

    public override void loadWidget()
    {
       //init Time;

        this.Literal2.Text = DateTime.Now.ToShortDateString();
    }
}
