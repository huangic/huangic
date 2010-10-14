using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Collections;

public partial class _10_100100_PermissionTree : System.Web.UI.Page
{

    private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
    
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {

            SessionObject sessionObj=new SessionObject();
           
            using (Entity.NXEIPEntities model = new Entity.NXEIPEntities())
            {

                //LINQ
                var root = (from d in model.departments where d.dep_parentid == 0 && d.dep_status == "1" select d).First();

                this.Label1.Text = root.dep_name;

                //設定TREE的預設節點

                Response.Cookies["permissionTree_selected"].Path = Request.Path.Replace("PermissionTree.aspx","");

                Response.Cookies["permissionTree_selected"].Value = "%23" + sessionObj.sessionUserDepartID;
            }


            

            String AjaxUrl = "var ajaxUrl='PermissionTreeMethod.ashx';";

            Page.ClientScript.RegisterStartupScript(this.GetType(), "AjaxUrl", AjaxUrl, true);


        }
    }

    
}
