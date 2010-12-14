﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using NXEIP.DAO;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data;
using System.Text;
using Entity;



public partial class lib_HeaderMenu : System.Web.UI.UserControl
{
    private NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
    SessionObject sessionUtil = new SessionObject();
    private NXEIPEntities model = new NXEIPEntities();

    
    protected void Page_Load(object sender, EventArgs e)
    {


        if (!Page.IsPostBack)
        {

            //ScriptManager.RegisterClientScriptInclude(this, typeof(UserControl), "JQuery", ResolveClientUrl("~/js/jquery-1.4.1.js"));
            //ScriptManager.RegisterClientScriptInclude(this, typeof(UserControl), "Menu", ResolveClientUrl("~/js/jquery.menu.js"));
            ScriptManager.RegisterClientScriptInclude(this, typeof(UserControl), "Hover", ResolveClientUrl("~/js/jquery.hoverIntent.js"));
            ScriptManager.RegisterClientScriptInclude(this, typeof(UserControl), "Menu", ResolveClientUrl("~/js/superfish.js"));


            String MenuScript = "$(\".mainmenu\").superfish();";

            ScriptManager.RegisterStartupScript(this, typeof(UserControl), "MenuStart", MenuScript, true);

            //取快取MENU
            
           
                generateMenu();
            

        }
        
    }

    protected override void OnPreRender(EventArgs e)
    {
        //ScriptManager.RegisterClientScriptInclude(this, typeof(UserControl), "Menu", ResolveClientUrl("~/js/mlmenu.js"));
        
        
        

        
        base.OnPreRender(e);
    }


    
    //產生MENU
    private void generateMenu() {


       
        //取使用者
        //String user_login=sessionUtil.sessionUserAccount;

       String user_login=null;

        DataSet menudataset=null;

        
            user_login = (String)sessionUtil.sessionUserAccount;

            if (String.IsNullOrEmpty(user_login))
            {
                //從COOKIE拿一下(錯誤轉頁"CustomErrors mode="RedirecrReWrite""的部分不支援SESSION
                try
                {
                    user_login = (String)Request.Cookies["UserAccount"].Value;
                }catch{
                }
            }else{
                try
                {
                    Response.Cookies.Add(new HttpCookie("UserAccount",user_login));
                }
                catch (Exception ex) {
                    logger.Debug(ex.Message);
                }
            }

            try
            {


            menudataset = (DataSet)HttpContext.Current.Session["menu"];
        }
        catch
        {
        
        }



        //從快取中拿DATESET
        if (menudataset != null)
        {
            menudataset = (DataSet)HttpContext.Current.Session["menu"];
            GetMenuFromCache();
            logger.Debug("MENU使用Session快取");
        }
        else
        {

            //資料庫連線
            Database db = DatabaseFactory.CreateDatabase("NXEIPConnectionString");

            //直接把帳號的角色權限功能全列
            String sqlCmd = @"SELECT DISTINCT sysfunc.sfu_no, sysfunc.sys_no, sysfunc.sfu_name, sysfunc.sfu_catalog, sysfunc.sfu_order, sysfunc.sfu_path, sysfunc.sfu_defaltpic, sysfunc.sfu_overpicture, sysfunc.sfu_parent, sysfunc.sfu_status, sysfunc.sfu_createuid, sysfunc.sfu_createtime,sysfunc.sfu_token
                        FROM sysfuction AS sysfunc 
                        INNER JOIN accounts AS acc 
                        INNER JOIN roleaccount AS racco 
                        ON acc.acc_no = racco.acc_no 
                        INNER JOIN rauthority AS rauth 
                        ON racco.rol_no = rauth.rol_no 
                        
                        ON sysfunc.sfu_no = rauth.sfu_no 
                        WHERE (acc.acc_login = @user_id) AND (sysfunc.sfu_status = 1)";

            DbCommand cmd = db.GetSqlStringCommand(sqlCmd);
            db.AddInParameter(cmd, "user_id", DbType.String, user_login);

            //放到DataSET之後用LINQ排內容
            menudataset = db.ExecuteDataSet(cmd);

           // using (NXEIPEntities model = new NXEIPEntities()) {
           //     var menu = (from sysfunc in model.sysfuction
           //                 from account in model.accounts
           //                 from roleacc in model.roleaccount
           //                 from rauth in model.rauthority
           //                 where sysfunc.sfu_status == "1"
           //                 && roleacc.acc_no == account.acc_no
           //                 && rauth.rol_no == roleacc.rol_no
           //                 && sysfunc.sfu_no == rauth.rol_no
           //                 && account.acc_login == user_login
           //                 orderby sysfunc.sfu_parent orderby sysfunc.sfu_order
           //                 select sysfunc);
           //}

            
        }
      
     

        //把測試用的HTML清掉

        mlmenu.Controls.Clear();



        DataTable dt = menudataset.Tables[0];

        
        //第一層目錄
        HtmlGenericControl MainMenu = new HtmlGenericControl("ul");
        MainMenu.Attributes["class"] = "mainmenu";
        

        //get sys_no
        //把DataTable 做Grouping
        var sysNos = from s in dt.AsEnumerable()
                     orderby s.Field<Int32>("sfu_order")
                     group s by s.Field<Int32>("sys_no") into g
                     select g;
        StringBuilder style = new StringBuilder();

      //SysNos is Grouping and key is SysNo;
        foreach (var item in sysNos) {
            int sys_no = item.Key;
               
            //LEVEL ONE SYS
            HtmlGenericControl menuone=new HtmlGenericControl("li");
            MainMenu.Controls.Add(menuone);
            
            
            HtmlAnchor a=new HtmlAnchor();
           


           sys MainSys=GetSysBySysNo(sys_no);
            
            
            a.ID="item"+sys_no;
            a.Attributes["alt"] = MainSys.sys_name;
            
            //a.InnerText =MainSys.sys_name;
            //因為第一層比較特殊需要SPAN來做隱藏
            
            HtmlGenericControl span2=new HtmlGenericControl("span"){
               InnerText=MainSys.sys_name
               };
            HtmlGenericControl span1 = new HtmlGenericControl("span");
            span1.Controls.Add(span2);

            a.Controls.Add(span1);
              
            
            
            a.Attributes["class"] ="imageMenu "+"item" + sys_no;
            //CSS handle Level one Pic;

            //default image

            String imgPath = "~/style/Green/image/";


            String pic = Page.ResolveUrl(imgPath + MainSys.sys_defaultpic);
            String overpic = Page.ResolveUrl(imgPath + MainSys.sys_overpicture);


            style.Append("." + a.ClientID + "{ background: url(" + pic + ") no-repeat;}\n");
            style.Append("." + a.ClientID + ":hover{ background: url(" + overpic + ") no-repeat;}\n");


            
            menuone.Controls.Add(a);

            HtmlGenericControl child=generateChildMenu(sys_no,0,dt);
            if(child!=null){
                menuone.Controls.Add(child);
            }
        
        }


        this.CssLiteral.Text="<style type=\"text/css\">"+style.ToString()+"</style>";





        mlmenu.Controls.Add(MainMenu);

        try
        {
            Session["menu"] = menudataset;
            Session["menuCss"] = this.CssLiteral.Text;
        }
        catch { 
        }


        menudataset.Dispose();
    
    }

    //遞回生子代
    private HtmlGenericControl generateChildMenu(int groupId, int parent, DataTable dt)
    {
        
        //find child item

        var childs = from ch in dt.AsEnumerable()
                    where ch.Field<Int32>("sys_no") == groupId && ch.Field<Int32>("sfu_parent") == parent
                    orderby ch.Field<Int32>("sfu_order")
                    select ch;
        
        HtmlGenericControl thisUl=new HtmlGenericControl("ul");
        foreach (var child in childs) {
            



            HtmlGenericControl liChild = new HtmlGenericControl("li");
            thisUl.Controls.Add(liChild);
            


            HtmlAnchor a = new HtmlAnchor();
            
            liChild.Controls.Add(a);
            
            
            a.Attributes["alt"] = child.Field<String>("sfu_name");
                  
            a.InnerText = child.Field<String>("sfu_name");

            if (child.Field<String>("sfu_token") == "1")
            {
                a.HRef =String.Format("~/External.aspx?url={0}&signId={1}",child.Field<String>("sfu_path"),(child.Field<Int32>("sfu_no")).ToString());
            }
            else {

                if (child.Field<String>("sfu_path") != null)
                {
                    a.HRef = "~/" + child.Field<String>("sfu_path");
                }

        }
            HtmlGenericControl childUl = generateChildMenu(groupId, child.Field<Int32>("sfu_no"),dt);

            if (childUl != null) {
                liChild.Controls.Add(childUl);
            }


        }


        if (thisUl.Controls.Count > 0) {

            return thisUl;
        }
        
        
        return null;
    }

    private sys GetSysBySysNo(int sysNo)
        {
            return (from s in model.sys
                        where s.sys_status=="1" && s.sys_no==sysNo
                    select s).FirstOrDefault();
                  

        }


    private void  GetMenuFromCache(){
        String user_login = sessionUtil.sessionUserAccount;
        //String menuHtml = (String)CacheUtil.GetItem("menu_" + user_login);
        
        //mlmenu.InnerHtml=menuHtml;

        String css = (String)HttpContext.Current.Session["menuCss"];
        this.CssLiteral.Text = css;

    }


   
}
