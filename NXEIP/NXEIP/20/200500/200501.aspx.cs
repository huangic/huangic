using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NXEIP.DAO;
using Entity;
using NLog;

public partial class _20_200500_200501 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            if (Request.QueryString["newS06no"] != null && Request.QueryString["dc09S06no"] != null && Request.QueryString["sfu_no"] != null)
            {
                //功能名稱
                using (NXEIPEntities model = new NXEIPEntities())
                {
                    int sfu_no = int.Parse(Request.QueryString["sfu_no"]);
                    string sfu_name = (from d in model.sysfuction where d.sfu_no == sfu_no select d.sfu_name).FirstOrDefault();

                    this.lab_newtitle.Text = sfu_name;
                    this.lab_d09title.Text = sfu_name;
                }

                this.hidd_newS06no.Value = Request.QueryString["newS06no"];

                this.hidd_d09S06no.Value = Request.QueryString["dc09S06no"];

                this.newLoadData(this.hidd_newS06no.Value, string.Empty);
                this.d09LoadData(this.hidd_d09S06no.Value, string.Empty);
            }
        }
    }

    private void newLoadData(string newS06no,string key)
    {
        this.ods_new01.SelectParameters["s06_no"].DefaultValue = newS06no;
        this.ods_new01.SelectParameters["key"].DefaultValue = key;
        this.ListView1.DataBind();
    }

    private void d09LoadData(string d09S06no, string key)
    {
        this.ods_doc10.SelectParameters["s06_no"].DefaultValue = d09S06no;
        this.ods_doc10.SelectParameters["key"].DefaultValue = key;
        this.GridView1.DataBind();
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        if (this.tbox_newkey.Text.Length == 0)
        {
            this.newLoadData(this.hidd_newS06no.Value, string.Empty);
        }
        else
        {
            this.newLoadData(this.hidd_newS06no.Value, this.tbox_newkey.Text);
        }
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        if (this.tbox_d09key.Text.Length == 0)
        {
            this.d09LoadData(this.hidd_d09S06no.Value, string.Empty);
        }
        else
        {
            this.d09LoadData(this.hidd_d09S06no.Value, this.tbox_d09key.Text);
        }
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            using (NXEIPEntities model = new NXEIPEntities())
            {
                int d09_no = int.Parse(e.Row.Cells[0].Text);

                doc09 data = (from d in model.doc09 where d.d09_no == d09_no select d).FirstOrDefault();
                sys06 s06data = (from d in model.sys06 where d.s06_no == data.s06_no select d).FirstOrDefault();
                if (s06data.s06_parent == 0)
                {
                    e.Row.Cells[0].Text = s06data.s06_name;
                    e.Row.Cells[1].Text = "";
                }
                else
                {
                    e.Row.Cells[0].Text = (from d in model.sys06 where d.s06_no == s06data.s06_parent select d.s06_name).FirstOrDefault();
                    e.Row.Cells[1].Text = s06data.s06_name;
                }

                e.Row.Cells[3].Text = new ChangeObject()._ADtoROC(data.d09_date.Value);
                e.Row.Cells[4].Text = (from d in model.departments where d.dep_no == data.d09_depno select d.dep_name).FirstOrDefault();

                ((Label)e.Row.Cells[5].FindControl("lab_name")).Text = (from d in model.people where d.peo_uid == data.d09_peouid select d.peo_name).FirstOrDefault();
                ((lib_PeopleDetail)e.Row.Cells[5].FindControl("PeopleDetail1")).peo_uid = data.d09_peouid.ToString();
                                       
            }
        }
    }

    


    
}