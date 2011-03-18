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
using NXEIP.DynamicForm;
using Newtonsoft.Json;

public partial class _30_300900_300901_2 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            
        }

        
        if ((Request["__EVENTTARGET"] == this.UpdatePanel1.ClientID) && String.IsNullOrEmpty(Request["__EVENTARGUMENT"]))
        {
            this.GridView1.DataBind();
           
        }

        if ((Request["__EVENTTARGET"] == this.UpdatePanel2.ClientID) && String.IsNullOrEmpty(Request["__EVENTARGUMENT"]))
        {
           
            this.GridView2.DataBind();
        }

    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int rowIndex = System.Convert.ToInt32(e.CommandArgument);
        String no = this.GridView1.DataKeys[rowIndex].Value.ToString();

        if (e.CommandName.Equals("disable"))
        {
            delete(no);
            return;
        }
    }

    protected void GridView2_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int rowIndex = System.Convert.ToInt32(e.CommandArgument);
        String no = this.GridView2.DataKeys[rowIndex].Value.ToString();

        if (e.CommandName.Equals("disable"))
        {
            delete_footer(no);
            return;
        }
    }




    private void delete(String uid)
    {
        using (NXEIPEntities model = new NXEIPEntities())
        {
            int id = int.Parse(Request["ID"]);
            Form01DAO dao = new Form01DAO();
            var columns = dao.GetColumnsByFormNO(id).ToList();

            Form f = new Form();

            f.Columns = columns.ToList();

            Column col = f.GetColumn(uid);

            f.Columns.Remove(col);

           

            form01 form = new form01 { f01_no = id };

            model.form01.Attach(form);


            form.f01_columns = JsonConvert.SerializeObject(f.Columns);



            //取COLUMN



            model.SaveChanges();
        }

        this.GridView1.DataBind();
        
        //this.OkMessagebox1.showMessagebox("delete"+dep_no);
    }


    private void delete_footer(String uid)
    {
        using (NXEIPEntities model = new NXEIPEntities())
        {
            int id = int.Parse(Request["ID"]);
            Form01DAO dao = new Form01DAO();
            var columns = dao.GetFooterByFormNO(id).ToList();

            Form f = new Form();

            f.Footer = columns.ToList();

            Column col = f.GetFooter(uid);

            f.Footer.Remove(col);



            form01 form = new form01 { f01_no = id };

            model.form01.Attach(form);


            form.f01_footer = JsonConvert.SerializeObject(f.Footer);



            //取COLUMN



            model.SaveChanges();
        }

        this.GridView2.DataBind();

        //this.OkMessagebox1.showMessagebox("delete"+dep_no);
    }



    protected void Button2_Click(object sender, EventArgs e)
    {
        JsUtil.RedirectJs(this, String.Format("300901.aspx"));
    }
}
