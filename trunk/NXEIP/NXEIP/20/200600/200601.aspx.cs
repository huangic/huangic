using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NXEIP.DAO;
using Entity;
using NLog;

public partial class _20_200600_200601 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        SessionObject sessionObj = new SessionObject();
        
        
        
        if (!this.IsPostBack)
        {
            this.ObjectDataSource_forum.SelectParameters[0].DefaultValue = sessionObj.sessionUserID;

            this.GridView1.DataBind();

        }


        //判斷來自JS 使用_doPostBack(updatePanel,"") 的情況 
        if (Request["__EVENTTARGET"] == this.UpdatePanel1.ClientID && String.IsNullOrEmpty(Request["__EVENTARGUMENT"]))
        {
            this.GridView1.DataBind();
        }



    }


    protected String GetLayoutName(String layout_code) {
        switch (layout_code)
        {
            case "1":
                return "開放型";

            case "2":
                return "半開放型";
            case "3":
                return "半封閉型";
            case "4":
                return "封閉型";

        }
                return "";
        }


    protected String GetROCDT(DateTime? dt) {
        if (dt.HasValue) {
            return new ChangeObject().ADDTtoROCDT(dt.Value);
            
        }
        return "";
    }
    
}