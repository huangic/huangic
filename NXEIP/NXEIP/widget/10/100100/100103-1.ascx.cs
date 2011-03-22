using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NXEIP.Widget;
using NLog;

public partial class widget_10_100100_100103_1 : WidgetBaseControl
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
        

        //取單位的相片

        if (this.PageType == "D")
        {
            this.Label.Text = "單位相簿";
            
            SessionObject sessionObj = new SessionObject();

            this.ObjectDataSource_Depart.SelectParameters[0].DefaultValue = "10";
            this.ObjectDataSource_Depart.SelectParameters[1].DefaultValue = sessionObj.sessionUserID;

            this.ListView1.DataSourceID = "ObjectDataSource_Depart";

            this.ListView1.DataBind();


        }


        if (this.PageType == "U")
        {
            this.Label.Text = "全府相簿";

            this.ObjectDataSource_Unit.SelectParameters[0].DefaultValue = "10";

            this.ListView1.DataSourceID = "ObjectDataSource_Unit";

            this.ListView1.DataBind();


        }

        this.HyperLink1.NavigateUrl += "?PageType=" + this.PageType;
        
        //取最新上傳的10個個人相片

       

    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        logger.Debug("Widget ==");
    }
    protected String GetDepartName(int dep_no)
    {
        UtilityDAO dao = new UtilityDAO();


        return dao.Get_DepartmentName(dep_no);
    }

    protected String GetPeopleName(int peo_uid)
    {

        UtilityDAO dao = new UtilityDAO();


        return dao.Get_PeopleName(peo_uid);
    }

}
