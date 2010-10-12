using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IMMENSITY.SWFUploadAPI;
using System.Runtime.Serialization.Json;
using Entity;
using System.Security.Cryptography;
using System.Text;
using NXEIP.DAO;

public partial class _10_100100_100105_2 : System.Web.UI.Page
{

    NLog.Logger logger = NLog.LogManager.GetLogger("_10_100100_100105_2");

    SessionObject sessionObj = new SessionObject();
    
    protected void Page_Load(object sender, EventArgs e)
    {

        //DocPermissionDAO dao = new DocPermissionDAO();

       // this.ObjectDataSource1.SelectParameters.Add();
       //取Cookies String //Cookie %2C取代成,

        string permissionFile=(Request.Cookies["PermissionFiles"].Value).Replace("%2C",",");
        //int[] permissionFileValue = Array.ConvertAll(permissionFile,new Converter<string,int>(StringToInt));

        

        this.ObjectDataSource1.SelectParameters["docNoString"].DefaultValue = permissionFile;

        

        this.GridView1.DataBind();
        

       
    }
    /// <summary>
    /// 取消
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button2_Click(object sender, EventArgs e)
    {

       


        this.Page.ClientScript.RegisterStartupScript(typeof(_10_100100_100105_2), "closeThickBox", "self.parent.update();", true);

    }
    /// <summary>
    /// 確定
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button1_Click(object sender, EventArgs e)
    {

       


        this.Page.ClientScript.RegisterStartupScript(typeof(_10_100100_100105_2), "closeThickBox", "self.parent.update();", true);

    }




    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int rowIndex = System.Convert.ToInt32(e.CommandArgument);


        int doc03_no = System.Convert.ToInt32(this.GridView1.DataKeys[rowIndex].Value.ToString());


        if (e.CommandName.Equals("modify"))
        {

            return;
        };

        if (e.CommandName.Equals("disable"))
        {
            // delete(dep_no);
            logger.Debug("disable");
            delete(doc03_no);
            return;
        };
    }

    private void delete(int doc03_no) { 
        using(EntityBatchUpdater<NXEIPEntities> model=new EntityBatchUpdater<NXEIPEntities>()){
           
            doc04 d4=new doc04();
            doc05 d5=new doc05();
            doc03 d3=new doc03();

            
          
           

            model.TrackEntity(d4);
            model.ObjectContext.DeleteObject(d4);
            model.UpdateBatch(d4, from d in model.ObjectContext.doc04 where d.d03_no == doc03_no select d);

            model.TrackEntity(d5);
            model.ObjectContext.DeleteObject(d5);
            model.UpdateBatch(d5, from d in model.ObjectContext.doc05 where d.d03_no == doc03_no select d);


            model.TrackEntity(d3);
            model.ObjectContext.DeleteObject(d3);
            model.UpdateBatch(d3, from d in model.ObjectContext.doc03 where d.d03_no == doc03_no select d);

           

            
            
            

            


        }
    }

}