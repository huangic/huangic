using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using NLog;
using NXEIP.Tree;

public partial class lib_tree_DepartTreePage : System.Web.UI.Page
{

    private static Logger logger = LogManager.GetCurrentClassLogger();
    



    protected void Page_Load(object sender, EventArgs e)
    {

        if (!Page.IsPostBack)
        {

            //DpartPanel INITIAL
            //TODO 多狀態要加這邊

            DepartTreeEnum setting = new DepartTreeEnum(Request);


            this.DepartmentPanel1.InitSetting(setting);




            string SessionName = Request["session"];
            this.DepartmentPanel1.ParentSessionID = SessionName;

            
            this.DepartmentPanel1.Clear();




            //設定init 的VALUE

            //ListBox 取Session 的值
            this.DepartmentPanel1.Items = (List<KeyValuePair<String,String>>)Session[SessionName];             

        }
    }

    
}
