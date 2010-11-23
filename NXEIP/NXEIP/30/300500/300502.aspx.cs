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

public partial class _30_300500_300502 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            
        }

     

    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int rowIndex = System.Convert.ToInt32(e.CommandArgument);
        int flo_no = System.Convert.ToInt32(this.GridView1.DataKeys[rowIndex].Value.ToString());

        if (e.CommandName.Equals("disable"))
        {
            delete(flo_no);
            return;
        }
    }




    private void delete(int flo_no)
    {
        using (NXEIPEntities model = new NXEIPEntities()) {
            floors f = new floors();
            f.flo_no = flo_no;

            model.floors.Attach(f);
            f.flo_status = "2";

            model.SaveChanges();
        }

        this.GridView1.DataBind();
        
        //this.OkMessagebox1.showMessagebox("delete"+dep_no);
    }

    

}
