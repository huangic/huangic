using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entity;
using System.Web.UI.HtmlControls;

public partial class _10_100500_100507 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        SessionObject sessionObj = new SessionObject();

        string user_login = sessionObj.sessionUserAccount;

        //取使用者所有的可用功能
        using (NXEIPEntities model = new NXEIPEntities())
        {

            var menu = (from s in model.sys
                        from sysfunc in model.sysfuction
                        from account in model.accounts
                        from roleacc in model.roleaccount
                        from rauth in model.rauthority
                        where sysfunc.sfu_status == "1"
                        && roleacc.acc_no == account.acc_no
                        && rauth.rol_no == roleacc.rol_no
                        && sysfunc.sfu_no == rauth.sfu_no
                        && account.acc_login == user_login
                        && s.sys_no == sysfunc.sys_no
                        orderby s.sys_order
                        select new { sysFunc = sysfunc, sys = s }).Distinct();

           

            //grouping

            var groupmenu = from m in menu
                            orderby m.sys.sys_order,m.sysFunc.sfu_order
                            group m.sysFunc by m.sys into ms
                            select ms;

            foreach (var m in groupmenu)
            {
                HtmlControl divBoxA = new HtmlGenericControl("div");

                divBoxA.Attributes.Add("class", "boxA");

                HtmlControl divBox = new HtmlGenericControl("div");
                divBox.Attributes.Add("class", "box");

                HtmlControl divBoxHeader = new HtmlGenericControl("div");
                divBoxHeader.Attributes.Add("class", "head");
                divBoxHeader.Controls.Add(new Literal() { Text = m.Key.sys_name.ToString() });
                divBoxA.Controls.Add(divBoxHeader);
                divBoxA.Controls.Add(divBox);
               

                HtmlControl divBoxContent=null;

                //建立內容
                foreach(var s in m){

                    if (!String.IsNullOrEmpty(s.sfu_path))
                    {
                        HtmlAnchor a = new HtmlAnchor();
                        a.HRef = "~/" + s.sfu_path;
                        a.Attributes["alt"] = s.sfu_name;
                        a.Title = s.sfu_name;
                        a.InnerText = s.sfu_name;
                        a.Attributes.Add("class", "a-letter-t2");

                        HtmlGenericControl li = new HtmlGenericControl("li");
                        li.Attributes.Add("class", "ps2 border-bottom-block3");
                        li.Controls.Add(a);

                        divBoxContent.Controls.Add(li);
                    }
                    else {
                        divBoxContent = new HtmlGenericControl("div");
                        divBoxContent.Attributes.Add("class", "content");

                        
                        
                        
                        HtmlControl divBoxB1 = new HtmlGenericControl("div");
                        HtmlAnchor a = new HtmlAnchor();
                        a.Title = s.sfu_name;
                        a.Attributes["alt"] = s.sfu_name;
                        a.InnerText = s.sfu_name;
                        a.Attributes.Add("class", "a-letter-t1");
                        HtmlGenericControl li = new HtmlGenericControl("li");
                        li.Attributes.Add("class", "ps1");
                        li.Controls.Add(a);

                        divBoxB1.Attributes.Add("class", "b2");
                        divBoxB1.Controls.Add(li);
                        divBoxContent.Controls.Add(divBoxB1);
                        divBox.Controls.Add(divBoxContent);
                        
                    }


                }

               
                


                this.application.Controls.Add(divBoxA);
            }

        }
    }


    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);
        
       
    }

}