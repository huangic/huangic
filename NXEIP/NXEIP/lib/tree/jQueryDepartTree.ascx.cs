using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Collections;

public partial class lib_tree_jQueryDepartTree : System.Web.UI.UserControl
{
    
    public List<KeyValuePair<String,String>> Items{
        get{
           return (List<KeyValuePair<String,String>>)Session["selectDepart"];
        }
    }
    
    
    protected void Page_Load(object sender, EventArgs e)
    {
       

        //註冊THICKBOX INIT事件

        string thickbox=@"function pageLoad(sender, args) {
           if (args.get_isPartialLoad()) {
               //  reapply the thick box stuff
               tb_init('a.thickbox');
           }
       }";



        //Page.ClientScript.RegisterClientScriptBlock(typeof(UserControl), "thickboxInit", thickbox, true);



        if(!Page.IsPostBack){
        Session["selectDepart"] = null;
        
        }
        this.HyperLink1.NavigateUrl = "~/lib/tree/JQueryDepartTree.aspx?TB_iframe=true&height=420&width=540&modal=true&clientID=" + this.UpdatePanel1.ClientID;
        
        

        
            try
            {
                List<KeyValuePair<String,String>> departs = this.Items;

                ListBox lb=(ListBox)this.UpdatePanel1.FindControl("ListBox1");

                lb.Items.Clear();
                  
                foreach(KeyValuePair<String,String> value in departs){
                    lb.Items.Add(new ListItem(value.Value,value.Key));
                }

            }
            catch { 
            
            }

     

    }


    
}
