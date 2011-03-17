using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NLog;
using Entity;

public partial class public_200104 : System.Web.UI.Page
{
    private static Logger logger = LogManager.GetCurrentClassLogger();

    protected void Page_Load(object sender, EventArgs e)
    {

        if (!Page.IsPostBack)
        {
            //建立GRID VIEW
            //this.GridView1.DataBind();


            //NXEIPEntities model = new NXEIPEntities();




        }
        this.GridView1.DataBind();
    }


    protected static bool GetModifyVisible(int peo_uid)
    {
        //HttpContext.Current.Session[""]
        SessionObject session = new SessionObject();
        return (int.Parse(session.sessionUserID) == peo_uid);


    }

    protected static string GetDepartmentName(int dep_no)
    {
        using (NXEIPEntities model = new NXEIPEntities())
        {
            var dep = (from d in model.departments where d.dep_no == dep_no select d).First();
            if (dep.dep_level > 1)
            {
                var parent_dep = (from d in model.departments where d.dep_no == dep.dep_parentid select d).First();

                return parent_dep.dep_name + "-" + dep.dep_name;
            }
            else
            {
                return dep.dep_name;
            }
        }

    }


    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //Bind內部的DataSource
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            ObjectDataSource ods = (ObjectDataSource)e.Row.FindControl("ObjectDataSource2");




            var v = (doc06)e.Row.DataItem;

            ods.SelectParameters[0].DefaultValue = v.d06_no.ToString();

        }

    }
    /// <summary>
    /// 依條件查詢
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button1_Click(object sender, EventArgs e)
    {
        
        String number = "";
        String peo_name = "";

              

        //keyword = this.tb_word.Text;

        number = this.tb_number.Text;
        peo_name = this.tb_peoname.Text;

        


        this.ObjectDataSource3.SelectParameters[0].DefaultValue = number;
        this.ObjectDataSource3.SelectParameters[1].DefaultValue = peo_name;

        try
        {
            this.ObjectDataSource3.SelectParameters[2].DefaultValue = this.calendar1._AD;
        }
        catch { 
        
        }

        this.GridView1.DataBind();
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "del")
        {
            int index = ((GridViewRow)((LinkButton)e.CommandSource).NamingContainer).RowIndex;

            int id = Convert.ToInt32(this.GridView1.DataKeys[index].Value);


            using (NXEIPEntities model = new NXEIPEntities())
            {
                doc06 d06 = new doc06();
                d06.d06_no = id;
                model.doc06.Attach(d06);

                d06.d06_status = "2";

                /*
                    var d07=(from d in model.doc07 where d.d06_no==id select d);
                    var d08 = (from d in model.doc08 where d.d06_no == id select d);


                    foreach (var d in d08)
                    {
                        model.doc08.DeleteObject(d);
                    }

                    foreach (var d in d07)
                    {
                        model.doc07.DeleteObject(d);
                    }
                    model.doc06.DeleteObject(d06);
                 * 
                 * 
                 */
                model.SaveChanges();
            }
            this.GridView1.DataBind();
        }
    }
}