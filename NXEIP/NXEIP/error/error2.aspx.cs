using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

public partial class error_error2 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //取錯誤訊息
        Exception ex = Server.GetLastError();

        //String url=Server.
        String url = Request.Url.ToString();
        String msg = "";
        string method = "";
        if (ex == null)
        {
            msg = "找不到網頁,請確認網址正確";
        }
        else
        {
            ex = ex.InnerException;
            msg = ex.Message;
            method = ex.TargetSite.ToString();
        }


        StringBuilder sb = new StringBuilder();

        sb.Append("錯誤網頁:" + url);
        sb.Append("<br/>");
        sb.Append("錯誤訊息:" + msg);
        sb.Append("<br/>");
        sb.Append("錯誤方法:" + method);
        sb.Append("<br/>");
        this.errorMsg.Text = sb.ToString();

        if (ex != null)
        {
            detail.Text = ex.StackTrace.Replace(Environment.NewLine, "<br/>");
        }


        Server.ClearError();
    }
}