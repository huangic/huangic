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
            
            
            this.lb_peo.Text = sessionObj.sessionUserName;
            this.lb_dep.Text = sessionObj.sessionUserDepartName;
            this.lb_date.Text = new ChangeObject()._ADtoROC(DateTime.Now);
            this.lb_size.Text = String.Format("(單一檔案限制{0}MB)", size);
            
            using(NXEIPEntities model=new NXEIPEntities()){
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
        if (UC_SWFUpload1.SWFUploadFileInfoList.Count > 0)
        {
            SessionObject sessionObj=new SessionObject();
            
            
            //存檔
            using (NXEIPEntities model = new NXEIPEntities()) {
                doc11 doc = new doc11();


                doc.d11_date = DateTime.Now;
                doc.d11_edate = this.calendar1._ADDate;
                doc.d11_peouid = int.Parse(sessionObj.sessionUserID);
                doc.d11_ext = this.tb_ext.Text;
                doc.d11_depno = int.Parse(sessionObj.sessionUserDepartID);
                doc.d11_subject = this.tb_subject.Text;
                doc.d11_tel = this.tb_tel.Text;
                doc.d11_use = this.tb_use.Text;
                doc.d11_createtime = DateTime.Now;
                doc.d11_createuid = int.Parse(sessionObj.sessionUserID);
                doc.d11_status = "1";

                //取第一筆回傳區的類別
                sys06 sys = (from d in model.sys06 where d.s06_status == "1" && d.sfu_no == 200105 orderby d.s06_order orderby d.s06_no select d).First();


                doc.s06_no = sys.s06_no;
                


                //文檔存檔
                model.doc11.AddObject(doc);
                model.SaveChanges();


                foreach (var f in UC_SWFUpload1.SWFUploadFileInfoList)
                {

                    doc12 file = new doc12();


                    file.d12_count = 0;
                    file.d12_file = f.OriginalFileName;
                    
                    
                   
                    file.d12_path = f.Path+f.FileName;
                    file.d12_type = f.Extension;
                    file.d11_no = doc.d11_no;
                    //取最大值
                    int max=1;
                    try{
                        max=(from d in model.doc12 where d.d11_no==doc.d11_no select d.d12_no).Max();
                        max++;
                    }catch{
                    
                    }


                    file.d12_no = max;

                    model.doc12.AddObject(file);
                    model.SaveChanges();

                }

            }



            



            this.Page.ClientScript.RegisterStartupScript(this.GetType(), "closeThickBox", "self.parent.update();", true);

        }
        else {
            this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('尚未上傳附件')", true);

        }
    }
}