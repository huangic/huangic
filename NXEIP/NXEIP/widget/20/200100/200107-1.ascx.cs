using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entity;
using System.Web.UI.HtmlControls;
using System.Data.Objects.SqlClient;
using NXEIP.DAO;

public partial class widget_20_200100_200107_1 : NXEIP.Widget.WidgetBaseControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
         SessionObject sessionObj = new SessionObject();

        string user_login = sessionObj.sessionUserAccount;
        int peouid = int.Parse(sessionObj.sessionUserID);

        //this.ObjectDataSource1.SelectParameters[0].DefaultValue = sessionObj.sessionUserDepartID;

        this.DataList1.DataBind();


    }

    public override string Name
    {
        get { return "NewUnitFile"; }
    }

    public override void loadWidget()
    {
       
    }

    protected String GetS06Name(int s06no) {
        using (NXEIPEntities model = new NXEIPEntities())
        {
            var cat = (from c in model.sys06 where c.s06_no == s06no select c).First();

            if (cat.s06_level == 1)
            {
                return cat.s06_name;
            }
            else
            {
                var p_cat = (from c in model.sys06 where c.s06_no == cat.s06_parent select c).First();
                return p_cat.s06_name;
            }



        }
    
    }
}