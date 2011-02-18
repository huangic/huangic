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

public partial class _30_300800_300802 : System.Web.UI.Page
{
    private static Logger logger = LogManager.GetCurrentClassLogger();


    SessionObject sessionObj = new SessionObject();
    
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!Page.IsPostBack)
        {
            this.ObjectDataSource3.SelectParameters[0].DefaultValue = sessionObj.sessionUserID; 
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

    protected String GetDepartmentName(int dep_no)
    {
       return new UtilityDAO().Get_DepartmentName(dep_no);
    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        //
 
        SessionObject sessionObj=new SessionObject();
        PersonalMessageUtil msgUtil = new PersonalMessageUtil();


        int index = ((GridViewRow)((Button)e.CommandSource).NamingContainer).RowIndex;

        int alb_no = int.Parse(GridView1.DataKeys[index].Values["alb_no"].ToString());





        if (e.CommandName == "close") {
            using (NXEIPEntities model = new NXEIPEntities()) {
                album album = (from d in model.album where d.alb_no == alb_no select d).Single();

                album.alb_status = "2";
                album.alb_checktime = DateTime.Now;
                album.alb_checkuid = int.Parse(sessionObj.sessionUserID);

                model.SaveChanges();

                
                  //通知申請人被退件了
                msgUtil.SendMessage(String.Format("討論區審核未通過"), String.Format("您所申請之相簿主題為「{0}」管理者不同意您申請，請洽單位管理者{1}", album.alb_name, sessionObj.sessionUserName), "", album.peo_uid, int.Parse(sessionObj.sessionUserID), true, false, false);

            }   
 
          
        }

        if (e.CommandName == "apply")
        {
            using (NXEIPEntities model = new NXEIPEntities())
            {
                album album = (from d in model.album where d.alb_no == alb_no select d).Single();

                album.alb_status = "1";
                album.alb_checktime = DateTime.Now;
                album.alb_checkuid = int.Parse(sessionObj.sessionUserID);

                model.SaveChanges();

               




                             

               

                
                //通知申請人被通過了
                msgUtil.SendMessage(String.Format("相簿審核通過"), String.Format("您所申請之相簿主題為「{0}」管理者已同意您申請，您可至個人相簿中上傳照片。", album.alb_name), "", album.peo_uid, int.Parse(sessionObj.sessionUserID), true, false, false);



            }

         

        }
        this.GridView1.DataBind();
    }
}


  



   