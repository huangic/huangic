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

public partial class _30_300900_300901_4 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            
        }

        
        if (Request["__EVENTTARGET"] == this.UpdatePanel1.ClientID && String.IsNullOrEmpty(Request["__EVENTARGUMENT"]))
        {
            this.GridView1.DataBind();
        }
    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int rowIndex = System.Convert.ToInt32(e.CommandArgument);
        int no = System.Convert.ToInt32(this.GridView1.DataKeys[rowIndex].Value.ToString());

        if (e.CommandName.Equals("delete"))
        {
            delete(no);
            return;
        }

        if (e.CommandName.Equals("enable"))
        {
            enable(no);
            return;
        }

        if (e.CommandName.Equals("disable"))
        {
            disable(no);
            return;
        }
    }




    private void delete(int f01_no)
    {
        using (NXEIPEntities model = new NXEIPEntities()) {
            form01 f = new form01();
            f.f01_no = f01_no;

            model.form01.Attach(f);
            f.f01_status = "3";

            model.SaveChanges();
        }

        this.GridView1.DataBind();
        
        //this.OkMessagebox1.showMessagebox("delete"+dep_no);
    }

    private void enable(int f01_no)
    {
        using (NXEIPEntities model = new NXEIPEntities())
        {
            form01 f = new form01();
            f.f01_no = f01_no;

            model.form01.Attach(f);
            f.f01_status = "1";

            model.SaveChanges();
        }

        this.GridView1.DataBind();

        //this.OkMessagebox1.showMessagebox("delete"+dep_no);
    }

    private void disable(int f01_no)
    {
        using (NXEIPEntities model = new NXEIPEntities())
        {
            form01 f = new form01();
            f.f01_no = f01_no;

            model.form01.Attach(f);
            f.f01_status = "2";

            model.SaveChanges();
        }

        this.GridView1.DataBind();

        //this.OkMessagebox1.showMessagebox("delete"+dep_no);
    }


    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        this.GridView1.DataBind();
    }

    public bool IsEnable(string code) {
        if (code == "1")
        {
            return true;
        }
        else {
            return false;
        }

    }

    public String ShowStatus(string code)
    {
        if (code == "1")
        {
            return "啟用";
        }
        else {
            return "停用";
        }
    }
}
