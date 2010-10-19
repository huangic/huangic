using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NXEIP.Widget;
using NLog;

public partial class widget_10_100100_100102 : WidgetBaseControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    private static Logger logger = LogManager.GetCurrentClassLogger();
    

    public override string Name
    {
        get { return "PersonInfo"; }
    }

    
    


    public override void loadWidget()
    {
      //not thing
        
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        logger.Debug("Widget ==");
    }
}
