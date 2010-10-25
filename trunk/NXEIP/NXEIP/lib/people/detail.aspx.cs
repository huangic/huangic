using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NXEIP.DAO;

public partial class lib_people_detail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        int id = int.Parse(Request["id"]);

        if (!Page.IsPostBack) {
            PeopleDAO dao = new PeopleDAO();
            var p = dao.GetByPeoUID(id);
            this.lb_name.Text = p.peo_name;
           
            this.lb_tel.Text = p.peo_tel;
            this.lb_ext.Text = p.peo_extension;
            this.lb_email.Text =String.Format("<a href=\"mailto:{0}\">{0}</a>",p.peo_email);

        
        }
    }
}