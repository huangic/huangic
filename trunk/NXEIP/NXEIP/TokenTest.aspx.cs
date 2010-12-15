using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using localhost.sso;

public partial class TokenTest : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        //取TOKEN

        String token = Request["token"];
        String sysId = Request["sysId"];


        using (TokenAuth service = new TokenAuth()) {
            UserData u=service.Auth(token, sysId);

            this.TextBox1.Text = "";
            this.TextBox1.Text +=String.Format("Message:{0}\n",u.Message);
            this.TextBox1.Text += String.Format("Account:{0}\n", u.Account);
            this.TextBox1.Text += String.Format("UID:{0}\n", u.UID);
        }


    }
}