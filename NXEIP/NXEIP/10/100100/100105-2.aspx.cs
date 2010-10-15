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
using System.Data.Objects;

public partial class _10_100100_100105_2 : System.Web.UI.Page
{

    NLog.Logger logger = NLog.LogManager.GetLogger("_10_100100_100105_2");

    SessionObject sessionObj = new SessionObject();
    
    protected void Page_Load(object sender, EventArgs e)
    {

        //DocPermissionDAO dao = new DocPermissionDAO();

       // this.ObjectDataSource1.SelectParameters.Add();
       //取Cookies String //Cookie %2C取代成,

        string permissionFile=(Request.Cookies["PermissionFiles"].Value);
        //int[] permissionFileValue = Array.ConvertAll(permissionFile,new Converter<string,int>(StringToInt));



        //取單一檔案權限 //如果他沒有權限職的話就直接建立一個空的





        this.ObjectDataSource1.SelectParameters["doc_no"].DefaultValue = permissionFile;

        

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


        string  type = this.GridView1.DataKeys[rowIndex]["type"].ToString();
        int id = System.Convert.ToInt32(this.GridView1.DataKeys[rowIndex]["id"].ToString());
        int doc03_no = System.Convert.ToInt32(this.GridView1.DataKeys[rowIndex]["d03_no"].ToString());


        if (e.CommandName.Equals("modify"))
        {

            return;
        };

        if (e.CommandName.Equals("disable"))
        {
            // delete(dep_no);
            logger.Debug("disable");
            delete(id, type, doc03_no);
            return;
        };
    }

    private void delete(int id,String type,int doc03_no) { 
        using(NXEIPEntities model=new NXEIPEntities()){
           
           //remove selected permission item
      
            //type mean people or department

           

            if (type == "P")
            {
                var deleteItem = (from d in model.doc05 where d.d05_no == id && d.d03_no==doc03_no select d);

                foreach (var item in deleteItem) {
                    model.doc05.DeleteObject(item);
                }

            }
            if(type=="D"){
                var deleteItem = (from d in model.doc04 where d.d04_no == id && d.d03_no==doc03_no select d);
                foreach (var item in deleteItem)
                {
                    model.doc04.DeleteObject(item);
                }
            }

            model.SaveChanges();

            this.GridView1.DataBind();
            //string js = "刪除成功";
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "", js, true);

        }
    }

}