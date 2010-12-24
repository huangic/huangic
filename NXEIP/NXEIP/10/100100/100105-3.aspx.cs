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
using System.IO;
using NXEIP.Lib;

public partial class _10_100100_100105_3 : System.Web.UI.Page
{

    NLog.Logger logger = NLog.LogManager.GetLogger("_10_100100_100105_2");

    SessionObject sessionObj = new SessionObject();
    
    protected void Page_Load(object sender, EventArgs e)
    {

        //DocPermissionDAO dao = new DocPermissionDAO();

       // this.ObjectDataSource1.SelectParameters.Add();
       //取Cookies String //Cookie %2C取代成,

        //string permissionFile=(Request.Cookies["PermissionFiles"].Value);
        //int[] permissionFileValue = Array.ConvertAll(permissionFile,new Converter<string,int>(StringToInt));



        //取單一檔案權限 //如果他沒有權限職的話就直接建立一個空的





        //this.ObjectDataSource1.SelectParameters["doc_no"].DefaultValue = permissionFile;

        

        this.GridView1.DataBind();
        

       
    }
    /// <summary>
    /// 取消
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button2_Click(object sender, EventArgs e)
    {

       


        this.Page.ClientScript.RegisterStartupScript(this.GetType(), "closeThickBox", "self.parent.update();", true);

    }
    /// <summary>
    /// 確定
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button1_Click(object sender, EventArgs e)
    {




        this.Page.ClientScript.RegisterStartupScript(this.GetType(), "closeThickBox", "self.parent.update();", true);

    }




    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int rowIndex = System.Convert.ToInt32(e.CommandArgument);


        int doc01_no= System.Convert.ToInt32(this.GridView1.DataKeys[rowIndex]["d01_no"].ToString());
        int doc02_no = System.Convert.ToInt32(this.GridView1.DataKeys[rowIndex]["d02_no"].ToString());

             

        if (e.CommandName.Equals("public"))
        {
            // delete(dep_no);
            logger.Debug("public");
            //delete(doc01_no,doc02_no);

            try
            {
                SetPublic(doc01_no, doc02_no);
            }
            catch { 
            
            }
            
            this.GridView1.DataBind();
            return;

            
        };




    }


    private void SetPublic(int d01_no, int d02_no) {
        using (NXEIPEntities model = new NXEIPEntities()) {
            var oldPublic = (from d in model.doc02 where d.d01_no == d01_no && d.d02_open == "2" select d).First();

            oldPublic.d02_open = "1";

            doc02 newPublic = new doc02();

            newPublic.d01_no = d01_no;
            newPublic.d02_no = d02_no;

            model.doc02.Attach(newPublic);

            newPublic.d02_open = "2";

            model.SaveChanges();


        }
        
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
                OperatesObject.OperatesExecute(100105, 1, String.Format("刪除部門權限 doc03_no:{0},doc05_no", +doc03_no, id));

            }
            if(type=="D"){
                var deleteItem = (from d in model.doc04 where d.d04_no == id && d.d03_no==doc03_no select d);
                foreach (var item in deleteItem)
                {
                    model.doc04.DeleteObject(item);
                }
                OperatesObject.OperatesExecute(100105, 1, String.Format("刪除部門權限 doc03_no:{0},doc04_no", +doc03_no, id));
            }

            model.SaveChanges();
           

            this.GridView1.DataBind();
            //string js = "刪除成功";
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "", js, true);

        }
    }

    protected void Button1_Click1(object sender, EventArgs e)
    {
        
        //取現在發行檔
        int doc01_no = int.Parse(Request["id"]);

        
        using (NXEIPEntities model = new NXEIPEntities())
        {


            var doc02=(from d in model.doc02 where d.d01_no==doc01_no && d.d02_open=="2" select d).First(); 

            //檔案路徑要抓原始的檔案路徑
            String FilePath = doc02.d02_path.Substring(0,doc02.d02_path.LastIndexOf('/')+1);

            doc02.d02_open = "1";

            #region 檔案存檔


            String uploadDir = new ArgumentsObject().Get_argValue("100105_dir");




            FileUpload fu = (FileUpload)this.FileUpload1;


            TreatFile f = new TreatFile(fu);

            //turning file = new turning();

            //存檔

            Directory.CreateDirectory(uploadDir + FilePath);

            fu.SaveAs(uploadDir + FilePath + f.FileName);


            //存檔DOC02

            doc02 doc = new doc02();

            //複製原始屬性
            EntityLib.CopyProperties(doc02, doc);



            doc.d02_KB = f.Size/1024;
            doc.d02_open = "2";
            doc.d02_path = FilePath + f.FileName;
            //doc.d02_public = "1";
            
                       


            String desc = String.Empty;
          
                desc = this.TextBox1.Text;
                doc.d02_description = desc;            
            
            //file.tur_subject = desc;


            //file.d06_no = d06.d06_no;
            //取最大值

            int max = 1;
            try
            {
                max = (from d in model.doc02 where d.d01_no == doc01_no select d.d02_no).Max();
                max++;
            }
            catch
            {

            }

            doc.d02_no = max;


            int ver = 1;
            try
            {
                ver = (from d in model.doc02 where d.d01_no == doc01_no select d.d02_version.Value).Max();
                ver++;
            }
            catch
            {

            }

            doc.d02_version = ver;




            //file.tur_no = max;

            model.doc02.AddObject(doc);
            model.SaveChanges();
            OperatesObject.OperatesExecute(100105,1, "新增附件版本 d01_no:{0},d02_no", doc01_no,doc.d02_no);



            #endregion


            this.GridView1.DataBind();
        }
    }
}