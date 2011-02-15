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

public partial class _30_300700_300702 : System.Web.UI.Page
{
    private static Logger logger = LogManager.GetCurrentClassLogger();

    protected void Page_Load(object sender, EventArgs e)
    {

        if (!Page.IsPostBack)
        {
                     
            this.GridView1.DataBind();
        }

    }

    

   

    protected string GetROCDate(DateTime? date)
    {
        if (date.HasValue)
        {
            return new ChangeObject()._ADtoROC(date.Value).ToString();
        }
        else
        {
            return "";
        }
    }


    protected string GetStatus(string status)
    {
        if (status == "1")
        {
            return "通過";
        }
        if (status == "2")
        {
            return "未通過";
        }
        if (status == "3")
        {
            return "審核中";
        }
        if (status == "4")
        {
            return "刪除";
        }
        return "";
    }

    protected String GetLayoutName(String layout_code)
    {
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

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        //
 
        SessionObject sessionObj=new SessionObject();
        PersonalMessageUtil msgUtil = new PersonalMessageUtil();


        int index = ((GridViewRow)((Button)e.CommandSource).NamingContainer).RowIndex;

        int tao_no = int.Parse(GridView1.DataKeys[index].Values["tao_no"].ToString());





        if (e.CommandName == "close") {
            using (NXEIPEntities model = new NXEIPEntities()) {
                taolun t = (from d in model.taolun where d.tao_no == tao_no select d).First();
                
                t.tao_status = "2";
                t.tao_checkdate = DateTime.Now;
                t.tao_checkuid = int.Parse(sessionObj.sessionUserID);

                model.SaveChanges();

                
                  //通知申請人被退件了
                msgUtil.SendMessage(String.Format("討論區審核未通過"), String.Format("您申請的討論區:{0}，審核未通過",t.tao_name), "", t.peo_uid, int.Parse(sessionObj.sessionUserID), true, false, false);

            }   
 
          
        }

        if (e.CommandName == "apply")
        {
            using (NXEIPEntities model = new NXEIPEntities())
            {
                taolun t = (from d in model.taolun where d.tao_no == tao_no select d).First();
                
                t.tao_status = "1";
                t.tao_checkdate = DateTime.Now;
                t.tao_checkuid = int.Parse(sessionObj.sessionUserID);

                model.SaveChanges();

               




                             

                //建立版主

                tao04 manager = new tao04();

                

                int max = 1;
                try
                {
                    max = (from d in model.tao04 where d.tao_no == tao_no select d.t04_no).Max();
                    max++;
                }
                catch { 
                
                }

                manager.tao_no = tao_no;
                manager.peo_uid = t.peo_uid;
                manager.t04_no = max;

                model.tao04.AddObject(manager);
                model.SaveChanges();

                
                //通知申請人被通過了
                msgUtil.SendMessage(String.Format("討論區審核通過"), String.Format("您申請的討論區:{0}，審核通過", t.tao_name), "", t.peo_uid, int.Parse(sessionObj.sessionUserID), true, false, false);



            }

         

        }
        this.GridView1.DataBind();
    }
}


  



   