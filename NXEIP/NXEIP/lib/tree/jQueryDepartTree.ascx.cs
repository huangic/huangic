using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Collections;
using Entity;

public partial class lib_tree_jQueryDepartTree : System.Web.UI.UserControl
{
    
    public List<KeyValuePair<String,String>> Items{
        get{
           return (List<KeyValuePair<String,String>>)Session["selectDepart"];
        }
    }


    public void Add(String key) { 
        //加入KEY直


        //從KEY值去取VALUE

        int deo_no=int.Parse(key);



        using(NXEIPEntities model =new NXEIPEntities()){
            departments dep=(from d in model.departments where d.dep_no==deo_no select d).First();



             KeyValuePair<String, String> value = new KeyValuePair<string, string>(key,dep.dep_name);


             List<KeyValuePair<string, string>> item = this.Items;

             if (item == null)
             {
                 item = new List<KeyValuePair<string, string>>();
             }

             item.Add(value);


             Session["selectDepart"]=item;
        }



    }


    public void Clear() {
        Session["selectDepart"] = null;
    }




    protected override void OnPreRender(EventArgs e)
    {
        //base.OnPreRender(e);


        try
        {
            List<KeyValuePair<String, String>> departs = this.Items;

            ListBox lb = (ListBox)this.UpdatePanel1.FindControl("ListBox1");

            lb.Items.Clear();

            foreach (KeyValuePair<String, String> value in departs)
            {
                lb.Items.Add(new ListItem(value.Value, value.Key));
            }

        }
        catch
        {

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



        
        this.HyperLink1.NavigateUrl = "~/lib/tree/JQueryDepartTree.aspx?TB_iframe=true&height=420&width=540&modal=true&clientID=" + this.UpdatePanel1.ClientID;
        
        
    }


    
}
