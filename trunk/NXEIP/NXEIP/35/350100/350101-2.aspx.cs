using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NXEIP.DAO;
using Entity;

public partial class _35_350100_350101_2 : System.Web.UI.Page
{
    private NXEIPEntities model = new NXEIPEntities();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            this.Navigator1.SubFunc = "人員明細";

            int peo_jobtype = (from d in model.types
                               where d.typ_number == "1" && d.typ_code == "work" && d.typ_status == "1"
                               select d.typ_no).FirstOrDefault();
            this.SqlDataSource1.SelectParameters["rol_no"].DefaultValue = Request["rol_no"];
            this.SqlDataSource1.SelectParameters["peo_jobtype"].DefaultValue = peo_jobtype.ToString();
            this.GridView1.DataBind();
        }
    }
}
