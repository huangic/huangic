using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using NLog;

public partial class lib_tree_JQueryDepartTree : System.Web.UI.Page
{

    private static Logger logger = LogManager.GetCurrentClassLogger();
    



    protected void Page_Load(object sender, EventArgs e)
    {


       
        
        
        if (!Page.IsPostBack)
        {

            string SessionName = Request["session"];
            TreeJson node = new TreeJson();
            using (Entity.NXEIPEntities model = new Entity.NXEIPEntities())
            {

                //LINQ
                var root = (from d in model.departments where d.dep_parentid == 0 && d.dep_status == "1" select d).First();

                this.Label1.Text = root.dep_name;


            }


            //ListBox 取Session 的值


            try
            {
                List<KeyValuePair<String, String>> list = (List<KeyValuePair<String, String>>)Session[SessionName];
                foreach(KeyValuePair<String,String> key in list){
                    this.ListBox2.Items.Add(new ListItem(key.Value,key.Key));
                }
            
            }
            catch { 
            
            }
            String AjaxUrl="var ajaxUrl='TreeMethod.ashx?session="+SessionName+"';";

            Page.ClientScript.RegisterStartupScript(this.GetType(), "AjaxUrl", AjaxUrl, true);

        }
    }

    protected void OkButton_Click(object sender, EventArgs e)
    {
        foreach (ListItem item in this.ListBox2.Items) {

            logger.Debug(item.Text);
        }
    }
}
