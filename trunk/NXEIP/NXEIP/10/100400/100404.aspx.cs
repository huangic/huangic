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

public partial class _10_100400_100404 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            this.DataSource.SelectParameters[0].DefaultValue = "";


            this.DataPager1.PagedControlID = "GridView1";
            this.GridView1.DataBind();

            
        }

        
        if (Request["__EVENTTARGET"] == this.UpdatePanel1.ClientID && String.IsNullOrEmpty(Request["__EVENTARGUMENT"]))
        {
            this.GridView1.DataBind();
        }
    }

   

   
    protected void btn_form_Click(object sender, EventArgs e)
    {
        this.show_1.Visible = true;
       
        this.show_2.Visible = false;
        this.GridView1.DataBind();
    }
    protected void btn_submit_Click(object sender, EventArgs e)
    {
        this.show_2.Visible = true;

        this.show_1.Visible = false;
        this.ObjectDataSource_submit.SelectParameters[0].DefaultValue = new SessionObject().sessionUserID;
        this.ObjectDataSource_submit.SelectParameters[1].DefaultValue = this.tb_keyword.Text;
        this.GridView2.DataBind();
                
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        Search();

    }

    private void Search() {
        if (this.GridView1.Visible == true)
        {
            this.DataSource.SelectParameters[0].DefaultValue = this.tb_keyword.Text;
            this.GridView1.DataBind();
        }
        else
        {
            this.ObjectDataSource_submit.SelectParameters[0].DefaultValue = new SessionObject().sessionUserID;
            this.ObjectDataSource_submit.SelectParameters[1].DefaultValue = this.tb_keyword.Text;
            this.GridView2.DataBind();
        }
    }

}
