using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entity;

public partial class External : System.Web.UI.Page
{

    public string url = "";
    
    
    protected void Page_Load(object sender, EventArgs e)
    {
     
        int sfu_no=int.Parse(Request["sysId"]);
        
        
        
        //取SYSFUNCTION

        using (NXEIPEntities model = new NXEIPEntities())
        {

            var sysfun = (from d in model.sysfuction where d.sfu_no == sfu_no select d).First();

            if (sysfun != null)
            {
                String url = sysfun.sfu_path;

                if (url.Contains("?"))
                {
                    url += "&";
                }
                else
                {
                    url += "?";
                }

                url += String.Format("token={0}&sysId={1}", new SessionObject().sessionLogInID, sfu_no);

                this.url = url;
            }
            else {
                JsUtil.AlertJs(this, "未設定要整合的系統網址!");
            }


        }



        
        
    }
}