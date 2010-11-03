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

public partial class _20_200100_200105_2 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        int size = 0;

        int.TryParse(new ArgumentsObject().Get_argValue("200105_size"), out size);
        
        this.UC_SWFUpload1.SwfUploadInfo = new SWFUploadInfo()
        {
            UploadMode = UpMode.LIST,
            File_size_limit = size,
            SubmitButtonId = this.btn_ok.ClientID,
            Path="/upload/200105/",
            File_upload_limit=1,
            PathArg="200105_dir"

        };



        SWFUploadFileInfo uf = new SWFUploadFileInfo();
        DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(SWFUploadFileInfo));
        System.IO.MemoryStream ms = new System.IO.MemoryStream();
        serializer.WriteObject(ms, uf);
        SessionObject sessionObj=new SessionObject();

        //init
        if (!Page.IsPostBack) {
            int peo_uid=int.Parse(sessionObj.sessionUserID);
            //取檔案

        

           

            
            this.lb_peo.Text = sessionObj.sessionUserName;
            this.lb_dep.Text = sessionObj.sessionUserDepartName;
            this.lb_date.Text = new ChangeObject()._ADtoROC(DateTime.Now);
            this.lb_size.Text = String.Format("(單一檔案限制{0}MB)", size);
            this.hidden_doc11no.Value = Request["id"];



            using(NXEIPEntities model=new NXEIPEntities()){
                 int id=int.Parse(Request["id"]);

                 var d11 = (from d in model.doc11 where d.d11_no == id select d).First();

                 
                 
                
                var people =(from p in model.people where p.peo_uid==peo_uid select p).First();
                this.tb_tel.Text = people.peo_tel;
                this.tb_ext.Text = people.peo_extension;

            }
            

        }

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        SWFUploadFile uf = new SWFUploadFile();

        foreach (var f in UC_SWFUpload1.SWFUploadFileInfoList)
        {

            String del_msg = uf.Delete(f.Path, f.FileName, true);

            //logger.Debug(del_msg);

        }



        this.Page.ClientScript.RegisterStartupScript(this.GetType(), "closeThickBox", "self.parent.update();", true);

    }
    protected void btn_ok_Click(object sender, EventArgs e)
    {
      
            SessionObject sessionObj=new SessionObject();
            



            
            //存檔
            using (NXEIPEntities model = new NXEIPEntities()) {
            
                
                int id=int.Parse(this.hidden_doc11no.Value);

                int peo_uid = int.Parse(sessionObj.sessionUserID);
                //取使用者ID

                doc13 doc = (from d in model.doc13 where d.d11_no == id && d.d13_peouid == peo_uid select d).FirstOrDefault();

                doc.d13_depno = int.Parse(sessionObj.sessionUserDepartID);
                doc.d11_no = id;
                doc.d13_date = DateTime.Now;
                doc.d13_ext = this.tb_ext.Text;
                doc.d13_tel = this.tb_tel.Text;
                doc.d13_peouid=peo_uid;
                
                //取第一筆檔案
                SWFUploadFileInfo file = UC_SWFUpload1.SWFUploadFileInfoList[0];

                doc.d13_path = file.Path+file.FileName;

                doc.d13_type = file.Extension;
                doc.d13_name = file.OriginalFileName;


                //文檔存檔
                model.doc13.AddObject(doc);
                model.SaveChanges();

                
            }



            



            this.Page.ClientScript.RegisterStartupScript(this.GetType(), "closeThickBox", "self.parent.update();", true);

       
    }
}