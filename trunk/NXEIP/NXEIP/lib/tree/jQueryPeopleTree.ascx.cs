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
           return (List<KeyValuePair<String,String>>)Session["selectPeople"];
        }
    }

    public void Add(String key)
    {
        //加入KEY直
        //從KEY值去取VALUE
        int peo_uid = int.Parse(key);

        using (NXEIPEntities model = new NXEIPEntities())
        {
            people peo = (from p in model.people where p.peo_uid == peo_uid select p).FirstOrDefault();

            if (peo != null)
            {
                KeyValuePair<String, String> value = new KeyValuePair<string, string>(key, peo.peo_name);

                List<KeyValuePair<string, string>> item = this.Items;

                if (item == null)
                {
                    item = new List<KeyValuePair<string, string>>();
                }
                item.Add(value);
                Session["selectPeople"] = item;
            }
        }
    }


    public void Clear()
    {
        Session["selectPeople"] = null;
    }

    protected void Page_Init(object sender, EventArgs e)
    {
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
        this.HyperLink1.NavigateUrl = "~/lib/tree/JQueryPeopleTree.aspx?TB_iframe=true&height=420&width=540&modal=true&clientID=" + this.UpdatePanel1.ClientID;
        //String  tbInit="<script>tb_init('a.thickbox, area.thickbox, input.thickbox,');//pass where to apply thickbox</script>";
       // Page.ClientScript.RegisterStartupScript(typeof(lib_tree_jQueryDepartTree), "tbInit", tbInit);
        //if (UpdatePanel1.IsInPartialRendering) { 
                //更新值
       // }
    }
}
