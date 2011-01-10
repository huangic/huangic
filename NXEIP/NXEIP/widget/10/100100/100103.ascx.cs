using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NXEIP.Widget;
using NLog;
using Entity;

public partial class widget_10_100100_100103 : WidgetBaseControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //載入JS

        //this.Page.RegisterStartupScript
        ScriptManager.RegisterClientScriptInclude(this, typeof(UserControl), "Cycle", ResolveClientUrl("~/js/jquery.cycle.all.min.js"));


        // script = "$('.block-1 .photo').cycle();";


        //ScriptManager.RegisterStartupScript(this, typeof(UserControl), "CycleStartup", script, true);

        
    }

    private static Logger logger = LogManager.GetCurrentClassLogger();
    

    public override string Name
    {
        get { return "ImageGallery"; }
    }

    
    


    public override void loadWidget()
    {
      //not thing
        
        //取最新上傳的10個個人相片

        SessionObject sessionObj = new SessionObject();

        this.ObjectDataSource1.SelectParameters[0].DefaultValue = "5";
        this.ObjectDataSource1.SelectParameters[1].DefaultValue = sessionObj.sessionUserID;

        this.ListView1.DataBind();

        

        
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        logger.Debug("Widget ==");
    }

 


}
