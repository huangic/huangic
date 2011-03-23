using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NXEIP.DAO;
using Entity;
using System.IO;
using NLog;

public partial class _10_100600_100601_5 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            int mee_no = int.Parse(Request.QueryString["mee_no"]);

            this.hidd_meeno.Value = mee_no.ToString();

            this.ObjectDataSource1.SelectParameters["mee_no"].DefaultValue = this.hidd_meeno.Value;
            this.GridView1.DataBind();

            this.ObjectDataSource2.SelectParameters["mee_no"].DefaultValue = this.hidd_meeno.Value;
            this.GridView2.DataBind();

            this.ObjectDataSource3.SelectParameters["mee_no"].DefaultValue = this.hidd_meeno.Value;
            this.GridView3.DataBind();

            meetings d = new _100601DAO().GetMeetings(mee_no);
            ChangeObject cobj = new ChangeObject();
            UtilityDAO udao = new UtilityDAO();

            this.lab_reason.Text = d.mee_reason;
            this.lab_place.Text = d.mee_place;
            this.lab_host.Text = udao.Get_PeopleName(d.mee_host.Value);
            this.lab_date.Text = cobj._ADtoROCDT(d.mee_sdate.Value) + "~" + cobj._ADtoROCDT(d.mee_edate.Value);
            this.lab_peoname.Text = udao.Get_PeopleName(d.mee_peouid.Value);
            this.lab_tel.Text = d.mee_tel;
            this.lab_record.Text = udao.Get_PeopleName(d.mee_recorduid.Value);

            
        }
    }

   
}