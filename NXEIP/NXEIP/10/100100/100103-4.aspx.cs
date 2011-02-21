using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IMMENSITY.SWFUploadAPI;
using System.Runtime.Serialization.Json;
using Entity;
using System.IO;

public partial class _10_100100_100103_4 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!this.IsPostBack) {


            String albumId=Request["id"];
            int fileSize = int.Parse(new ArgumentsObject().Get_argValue("100103_size"));

            this.Label2.Text = String.Format("單張相片最大{0}MB", fileSize);


            this.UC_SWFUpload1.SwfUploadInfo = new SWFUploadInfo()
            {
                UploadMode = UpMode.LIST,
                //File_size_limit = size,

                 File_types="*.jpg;*.gif;*.png",
                 File_types_description="圖片檔",
                 IsSmall=true,
                //相簿目錄
                Path = "/upload/100103/"+albumId+"/",
                //部門目錄  
                PathArg="100103_dir",
                //File_size_limit

                File_size_limit=fileSize,


                Upload_url = "~/lib/SWFUpload/uploadPhoto.aspx",

                SubmitButtonId = this.btn_ok.ClientID
            };





            SWFUploadFileInfo uf = new SWFUploadFileInfo();
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(SWFUploadFileInfo));
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            serializer.WriteObject(ms, uf);   

        
        }



    }
   
    protected void btn_ok_Click(object sender, EventArgs e)
    {

        SessionObject sessionObj = new SessionObject();
        ArgumentsObject args = new ArgumentsObject();

        String albumId = Request["id"];
        int alb_no = 0;
        int.TryParse(albumId, out alb_no);


        String uploadDir = "/upload/100103/" + albumId + "/";
        String uploadSmallDir = "/upload/100103/" + albumId+"/s/";


         //取檔案
        using (NXEIPEntities model = new NXEIPEntities())
        {
            List<String> fileNo = new List<string>();

             foreach (var f in UC_SWFUpload1.SWFUploadFileInfoList)
             {
                
                 //寫入每個檔案 然後抓後面要抓這次寫入的檔案設定描述
                 photo p = new photo();

                 p.alb_no = alb_no;
                 p.pho_createuid = int.Parse(sessionObj.sessionUserID);
                 p.pho_createtime = DateTime.Now;

                 p.pho_name = f.OriginalFileName;
                 p.pho_file = uploadDir + f.FileName;
                 p.pho_thumb = uploadSmallDir + f.FileName;
                


                 //取最大值
                 int max=0;

                 try
                 {
                     max = (from d in model.photo where d.alb_no == alb_no select d.pho_no).Max();
                 }
                 catch { 
                 
                 }
                 max++;

                 p.pho_no = max;

                 model.photo.AddObject(p);
                 model.SaveChanges();

                 fileNo.Add(max + "");

                 OperatesObject.OperatesExecute(100105, 1, String.Format("新增相片 alb_no:{0},pho_no:{1}", +p.alb_no,p.pho_no));
             }





        
                
               
        

            //顯示Grid 要用來設定描述
             this.panel_upload.Visible = false;
             this.showUpload.Visible = true;

             this.ObjectDataSource1.SelectParameters[0].DefaultValue = albumId;
             this.ObjectDataSource1.SelectParameters[1].DefaultValue = String.Join(",",fileNo);

             this.GridView1.DataBind();

             this.btn_ok.Visible = false;
             this.btn_save.Visible = true;
            //this.Page.ClientScript.RegisterStartupScript(this.GetType(), "closeThickBox", "self.parent.update('新增成功');", true);
        }
    }






    protected void btn_save_Click(object sender, EventArgs e)
    {
        //存檔描述
        using (NXEIPEntities model = new NXEIPEntities())
        { 
            
        foreach(GridViewRow item in this.GridView1.Rows ){
            TextBox tb=item.Cells[2].FindControl("TextBox1") as TextBox;

            //取ITEM
            int albumId = int.Parse(this.GridView1.DataKeys[item.DataItemIndex].Values[0].ToString());
            int photoId = int.Parse(this.GridView1.DataKeys[item.DataItemIndex].Values[1].ToString());

            photo p = new photo();

            p.alb_no = albumId;
            p.pho_no = photoId;

            model.photo.Attach(p);

            p.pho_desc = tb.Text;
           
            


        }


        model.SaveChanges();
        }


        //this.Page.ClientScript.RegisterStartupScript(this.GetType(), "closeThickBox", "self.parent.update('新增成功');", true);
        JsUtil.UpdateParentJs(this, "新增成功");

    }
}