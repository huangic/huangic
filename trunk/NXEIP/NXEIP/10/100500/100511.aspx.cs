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
using System.IO;
using NXEIP.Lib;

public partial class _10_100500_100511 : System.Web.UI.Page
{
    private static Logger logger = LogManager.GetCurrentClassLogger();
    
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!Page.IsPostBack) { 
            //建立GRID VIEW
            //this.GridView1.DataBind();


            //NXEIPEntities model = new NXEIPEntities();

            

            this.ListView1.DataBind();
        }




        //判斷來自JS 使用_doPostBack(updatePanel,"") 的情況 
        if (Request["__EVENTTARGET"] == this.UpdatePanel1.ClientID && String.IsNullOrEmpty(Request["__EVENTARGUMENT"]))
        {
            this.ListView1.DataBind();
        }

        

        
    }
    
    protected void ListView1_ItemCommand(object sender, ListViewCommandEventArgs e)
    {

        ListViewDataItem dataItem = (ListViewDataItem)e.Item;

         String Layout= ListView1.DataKeys[dataItem.DisplayIndex].Values["Name"].ToString();
       
        //存檔CSS設定

         int peo_uid = int.Parse(new SessionObject().sessionUserID);

         using (NXEIPEntities model = new NXEIPEntities())
         {
            //刪除
             var data = from d in model.setting where d.peo_uid == peo_uid && d.set_variable == "CssLayout" select d;

             foreach (var d in data)
             {
                 model.setting.DeleteObject(d);
             }

             model.SaveChanges();
            

             //存檔
             setting set = new setting();
             set.set_variable = "CssLayout";
             set.set_value = Layout;
             set.peo_uid = peo_uid;

             model.setting.AddObject(set);
             model.SaveChanges();

         }


               



         CssUtil.GetInitCssLayout();


         JsUtil.AlertJs(this, "設定完成");

    }
}