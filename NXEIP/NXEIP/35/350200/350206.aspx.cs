using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entity;
using NXEIP.DAO;
using IMMENSITY.SWFUploadAPI;
using System.Runtime.Serialization.Json;

public partial class _35_350200_350206 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            
        }


        //判斷來自JS 使用_doPostBack(updatePanel,"") 的情況 
        if (Request["__EVENTTARGET"] == this.UpdatePanel1.ClientID && String.IsNullOrEmpty(Request["__EVENTARGUMENT"]))
        {
            this.GridView1.DataBind();
        }


    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        //int rowIndex = e.
        int rowIndex = ((GridViewRow)((Button)e.CommandSource).NamingContainer).RowIndex;

        int dep_no = System.Convert.ToInt32(this.GridView1.DataKeys[rowIndex].Values[0].ToString());
        int man_no = System.Convert.ToInt32(this.GridView1.DataKeys[rowIndex].Values[1].ToString());


        if (e.CommandName.Equals("disable"))
        {
            delete(dep_no,man_no);
            return;
        }
    }

    


    private void delete(int dep_no,int man_no)
    {

        manager m = new manager();

        m.dep_no = dep_no;
        m.man_no = man_no;

        using (NXEIPEntities model = new NXEIPEntities()) {

            model.manager.Attach(m);

            model.manager.DeleteObject(m);

            model.SaveChanges();
        
        }




        

        this.GridView1.DataBind();
        
        //this.OkMessagebox1.showMessagebox("delete"+dep_no);
    }

    

}
