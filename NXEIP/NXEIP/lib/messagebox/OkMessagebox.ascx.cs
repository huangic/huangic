using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class lib_messagebox_OkMessagebox : System.Web.UI.UserControl
{

    public void showMessagebox(String title, String message) {

        this.lab_title.Text = title;
        this.lab_meg.Text = message;
        this.mdlPopupMsgBox.Show();
    
    }

    public void showMessagebox(String message) {
        this.lab_title.Text = "";
        this.lab_meg.Text = message;
        this.mdlPopupMsgBox.Show();
    }



    
    
    
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!Page.IsPostBack)
        {
            this.btnYes.OnClientClick = String.Format("fnClickOK('{0}','{1}')", btnYes.UniqueID, "");
            this.btnNo.OnClientClick = String.Format("fnClickNo('{0}','{1}')", btnNo.UniqueID, "");
        }
    }
    
}
