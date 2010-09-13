using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entity;
using System.Web.UI.HtmlControls;

public partial class lib_SubHeaderMenu : System.Web.UI.UserControl,ISubMenuControl
{
    private NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
    SessionObject sessionUtil = new SessionObject();
    public String SysFuncCode { get;set; }



    
    protected void Page_Load(object sender, EventArgs e)
    {

    }


    protected override void Render(HtmlTextWriter writer)
    {
        if (String.IsNullOrEmpty(SysFuncCode)) {
            throw new ArgumentNullException("未指定MENU項目的系統功能編號");
           
        }
        
        //建立MENU

        //取系統編號的父代
        using (NXEIPEntities model = new NXEIPEntities()) { 
            int sfu_no=int.Parse(SysFuncCode);
            var currentFunc=(from f in model.sysfuction where f.sfu_no==sfu_no select f).First();


            var SameLevelFunc = (from f in model.sysfuction where f.sfu_parent == currentFunc.sfu_parent && f.sfu_status == "1" orderby f.sfu_order select f);

            HtmlGenericControl htmlUl = new HtmlGenericControl("ul");

            foreach(var func in SameLevelFunc){
                HtmlGenericControl htmlLi = new HtmlGenericControl("li");
                HtmlAnchor htmla = new HtmlAnchor();
                htmlLi.Controls.Add(htmla);
                htmlUl.Controls.Add(htmlLi);
                htmla.HRef = "~/"+func.sfu_path;
                htmla.Attributes["alt"] = func.sfu_name;
                htmla.Attributes["title"] = func.sfu_name;
                htmla.InnerHtml = "<span>"+func.sfu_name+"</span>"; 
            }
            this.submenu.Controls.Clear();
            this.submenu.Controls.Add(htmlUl);

        }



        
        
        base.Render(writer);
    }





    public void SetCode(string code)
    {
        this.SysFuncCode = code;
    }

    public String GetCode()
    {
        return this.SysFuncCode;
    }
}