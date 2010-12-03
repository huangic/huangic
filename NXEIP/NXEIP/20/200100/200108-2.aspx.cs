using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NXEIP.DAO;
using Entity;
using NLog;
using NXEIP.DynamicForm;
using Newtonsoft.Json;

public partial class _20_200100_200108_2 : System.Web.UI.Page
{
    private static Logger logger = LogManager.GetCurrentClassLogger();
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            String ID = Request.QueryString["ID"];
            String SID = Request.QueryString["SID"];

            //取ID 
            AbstractFormFactory formfactory = new EntityFormFactory();
            AbstractFormFactory submitfactory = new EntitySubmitFactory();
            Form form = formfactory.GetInstance(ID);
            Form submit = submitfactory.GetInstance(SID);


            this.lb_description.Text = form.Description;
            this.lb_name.Text = form.Name;
            this.lb_people.Text = form.HandleUser;

            this.lb_submit.Text = submit.HandleUser;
            this.lb_submit_date.Text = new ChangeObject()._ADtoROC(submit.CreateTime);



            //建立動態物件


            ColumnFactory ColumnFactoy = new ColumnFactory();
            List<WebControl[]> list = ColumnFactoy.ConvertColumsToDisplayWebControl(submit.Columns);
            //使用者控制群

            //加入欄位

            //一欄是
            foreach (WebControl[] w in list)
            {
                TableRow tr = new TableRow();
                TableHeaderCell th = new TableHeaderCell();
                th.Width = new Unit("200px");
                th.Controls.Add(w[0]);
                TableCell td = new TableCell();
                td.Controls.Add(w[1]);
                tr.Controls.Add(th);
                tr.Controls.Add(td);

                this.DynamicTable.Controls.Add(tr);
            }
           
            //建立FOOTER
            TableRow foottr = new TableRow();
            foreach (var c in form.Footer)
            {
               
                TableHeaderCell th = new TableHeaderCell();
                //th.Width = new Unit("200px");
                th.Controls.Add(new Label() { Text = c.Name });
                               
                foottr.Controls.Add(th);

                this.DynamicFooter.Controls.Add(foottr);
            }

            TableRow foottr2 = new TableRow();
            foreach (var c in form.Footer)
            {

                TableCell td = new TableCell();
                //th.Width = new Unit("200px");
                td.Controls.Add(new Literal(){ Text="&nbsp;"});
                td.Height = new Unit("50px");
                foottr2.Controls.Add(td);
    
            }
            this.DynamicFooter.Controls.Add(foottr2);

        }
    }

  

   
  




   
   
}
