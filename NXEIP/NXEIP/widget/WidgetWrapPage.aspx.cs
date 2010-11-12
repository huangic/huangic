using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entity;
using NXEIP.Widget;

public partial class widget_WidgetWrapPage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //取Widget 編號

        String widget_no=Request["widget"];

        int wid_no=int.Parse(widget_no);


        using (NXEIPEntities model = new NXEIPEntities())
        {


           widget wid=( from w in model.widget where w.wid_no == wid_no select w).FirstOrDefault();





        WidgetBaseControl control = (WidgetBaseControl)Page.LoadControl("~/" + wid.wid_url);
        control.Title = wid.wid_name;
        control.WidgetID = wid.wid_no;
        control.IsEditable = true;

        this.form1.Controls.Add(control);
        }

    }
}