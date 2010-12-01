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

public partial class _20_200100_200108_1 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            //取ID 

           

            AbstractFormFactory factory = new EntityFormFactory(); 
            Form form= factory.GetInstance( Request["ID"]);

                this.lb_description.Text = form.Description;
                this.lb_name.Text = form.Name;
                this.lb_people.Text = form.HandleUser;
            


                //建立動態物件


                ColumnFactory ColumnFactoy = new ColumnFactory();
                List<WebControl[]> list = ColumnFactoy.ConvertColumsToWebControl(form.Columns);

                //使用者控制群
            
                //加入欄位

                //一欄是
                foreach (WebControl[] w in list) {
                    TableRow tr = new TableRow();
                    TableHeaderCell th = new TableHeaderCell();
                    th.Controls.Add(w[0]);
                    TableCell td = new TableCell();
                    td.Controls.Add(w[1]);
                    tr.Controls.Add(th);
                    tr.Controls.Add(td);

                    this.DynamicTable.Controls.Add(tr);
                }

        }

        
       
    }

   

    
}
