using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class lib_messagebox_ConfirmMessagebox : System.Web.UI.UserControl
{

    public void showMessagebox(String title, String message) {

        this.lab_title.Text = title;
        this.lab_meg.Text = message;
        this.confirmModal.Show();
    
    }

    public void showMessagebox(String message) {
        this.lab_title.Text = "";
        this.lab_meg.Text = message;
        this.confirmModal.Show();
    }



    
    
    
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!Page.IsPostBack)
        {
            
        
            
            String script ="function confirmMsgbox(sender, msg) {"+
                "this._Source = sender;"+
        "this._msg=$get('" + this.lab_meg.ClientID + "');"+
        "this._msg.innerHTML = msg;"+
        "this._ConfirmPop = $find('confirmModal');"+
        "this._ConfirmPop.show();}";
        
  
    




            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "ConfirmMsg", script, true);
        }
        
    }
    
}
