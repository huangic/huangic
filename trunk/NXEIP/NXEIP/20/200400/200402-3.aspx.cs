using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NXEIP.DAO;
using Entity;

public partial class _20_200400_200402_3 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            if (Request.QueryString["e02_no"] != null)
            {
                this.hidd_no.Value = Request["e02_no"];

                using (NXEIPEntities model = new NXEIPEntities())
                {
                    e02 d = new e02DAO().GetBye02NO(Convert.ToInt32(this.hidd_no.Value));
                    this.lab_mechani.Text = d.e02_mechani;
                    this.lab_code.Text = d.e02_code;
                    this.lab_typ_name.Text = (from t in model.types where t.typ_no == d.typ_no select t.typ_cname).FirstOrDefault();
                    this.lab_name_flag.Text = d.e02_name + "(第" + d.e02_flag + "期)";
                    this.lab_memo.Text = d.e02_memo;
                    this.lab_limit.Text = d.e02_limit;

                    this.lab_e01_name.Text = d.e02_place;
                    
                    this.lab_teacher.Text = d.e02_teacher;
                    if (d.e02_hour.HasValue)
                    {
                        this.lab_hour.Text = d.e02_hour.ToString();
                    }
                    if (d.e02_people.HasValue)
                    {
                        this.lab_people.Text = d.e02_people.ToString();
                    }
                    if (d.e02_check.Equals("1"))
                    {
                        this.lab_check.Text = "審核";
                    }
                    else
                    {
                        this.lab_check.Text = "不審核";
                    }


                    ChangeObject cboj = new ChangeObject();
                    this.lab_opendate.Text = cboj._ROCtoROCYMD(cboj._ADtoROC(Convert.ToDateTime(d.e02_opendate.ToString())));
                    this.lab_signdate.Text = cboj._ROCtoROCYMD(cboj._ADtoROC(Convert.ToDateTime(d.e02_signdate.ToString()))) + "至" + cboj._ROCtoROCYMD(cboj._ADtoROC(Convert.ToDateTime(d.e02_signedate.ToString())));
                    this.lab_date.Text = cboj._ROCtoROCYMD(cboj._ADtoROC(Convert.ToDateTime(d.e02_sdate.ToString()))) + cboj._ADtoTime(d.e02_sdate.Value) + " 至 " + cboj._ROCtoROCYMD(cboj._ADtoROC(Convert.ToDateTime(d.e02_edate.ToString()))) + cboj._ADtoTime(d.e02_edate.Value);
                }

                this.ODS_1.SelectParameters["e02_no"].DefaultValue = this.hidd_no.Value;
                this.GridView1.DataBind();
            }
        }
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            UtilityDAO dao = new UtilityDAO();

            e.Row.Cells[0].Text = dao.Get_DepartmentName(int.Parse(e.Row.Cells[0].Text));

            e.Row.Cells[1].Text = dao.Get_PeopleName(int.Parse(e.Row.Cells[1].Text));

            e.Row.Cells[2].Text = new ChangeObject().ADDTtoROCDT(e.Row.Cells[2].Text);
        }
    }
}