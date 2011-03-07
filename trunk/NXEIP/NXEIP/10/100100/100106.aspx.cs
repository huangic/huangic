using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Specialized;
using AjaxControlToolkit;
using NXEIP.DAO;
using System.Data.Objects.SqlClient;
using Entity;
using NLog;
using System.Data;

public partial class _10_100100_100106 : System.Web.UI.Page
{
    private static Logger logger = LogManager.GetCurrentClassLogger();
    
    
    
    
    protected void Page_Load(object sender, EventArgs e)
    {

        SessionObject sessionObj=new SessionObject();

        if (!Page.IsPostBack) { 
            //建立GRID VIEW
            //this.GridView1.DataBind();


            //NXEIPEntities model = new NXEIPEntities();

            this.ObjectDataSource_d11.SelectParameters[0].DefaultValue = sessionObj.sessionUserID;

            
        }




        //判斷來自JS 使用_doPostBack(updatePanel,"") 的情況 
        if (Request["__EVENTTARGET"] == this.UpdatePanel1.ClientID && String.IsNullOrEmpty(Request["__EVENTARGUMENT"]))
        {
            this.GridView1.DataBind();
        }

        

        
    }

     
   
   

    protected static bool GetModifyVisible(int peo_uid) { 
        //HttpContext.Current.Session[""]
        SessionObject session = new SessionObject();
        return (int.Parse(session.sessionUserID) == peo_uid);
            
        
    }


    protected String GetROCDT(DateTime? dt) {
        if (dt.HasValue)
        {
           return new ChangeObject()._ADtoROCDT(dt.Value);
        }
        else { 
            return "";
        }
    }


    protected String GetPeoName(int? peo_uid) {
        if (peo_uid.HasValue)
        {
            return new UtilityDAO().Get_PeopleName(peo_uid.Value);
        }
        else {
            return "";
        }
    }

    protected String GetStatus(String status)
    {
        switch(status){
            case "1":
                return "通過";
                break;
            case "2":
                return "不通過";
                break;
            case "3":
                return "送審中";
                break;

        }
        return "";
    }
  
}