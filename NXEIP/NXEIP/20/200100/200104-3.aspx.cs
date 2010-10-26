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
using System.Data.Objects.SqlClient;

public partial class _20_200100_200104_3 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        int size = 0;

        int.TryParse(new ArgumentsObject().Get_argValue("upload_file_size_limit"), out size);
        
        this.UC_SWFUpload1.SwfUploadInfo = new SWFUploadInfo()
        {
            UploadMode = UpMode.LIST,
            File_size_limit = size,
            SubmitButtonId = this.btn_ok.ClientID,
            Path="/upload/document/"
        };



        SWFUploadFileInfo uf = new SWFUploadFileInfo();
        DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(SWFUploadFileInfo));
        System.IO.MemoryStream ms = new System.IO.MemoryStream();
        serializer.WriteObject(ms, uf);
        SessionObject sessionObj=new SessionObject();

        //init
        if (!Page.IsPostBack) {

            

            this.hidden_d06_no.Value = Request["id"];
            
            int id=int.Parse(this.hidden_d06_no.Value);


            this.Label2.Text = sessionObj.sessionUserName;
            this.Label1.Text = sessionObj.sessionUserDepartName;
            this.lb_size.Text = String.Format("(單一檔案限制{0}MB)", size);

            //init attbute

            using (NXEIPEntities model = new NXEIPEntities()) { 
                var doc06=(from d in model.doc06 where d.d06_no==id select d).First();

                this.tb_ext.Text = doc06.d06_ext;
                this.tb_number.Text = doc06.d06_number;
                this.tb_tel.Text = doc06.d06_tel;
                this.RadioButtonList1.SelectedValue = doc06.d06_open;

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
            
            //取ID欄位
            int id = int.Parse(this.hidden_d06_no.Value);


            //存檔
            using (NXEIPEntities model = new NXEIPEntities()) {
                doc06 d06 = new doc06();
                d06.d06_no = id;
                model.doc06.Attach(d06);
                
                
                
                
                //d06.d06_createuid = int.Parse(sessionObj.sessionUserID);
                //d06.d06_depno = int.Parse(sessionObj.sessionUserDepartID);
                d06.d06_date = DateTime.Now;
                d06.d06_tel = tb_tel.Text;
                d06.d06_number = tb_number.Text;
                d06.d06_open = this.RadioButtonList1.SelectedValue;
                d06.d06_peouid = int.Parse(sessionObj.sessionUserID);
                d06.d06_ext = tb_ext.Text;


                //文檔存檔
                //model.doc06.AddObject(d06);
                model.SaveChanges();


                foreach (var f in UC_SWFUpload1.SWFUploadFileInfoList)
                {
                    doc07 file = new doc07();
                    file.d07_count = 0;
                    file.d07_file = f.OriginalFileName;
                    file.d07_path = f.Path+f.FileName;
                    file.d07_type = f.Extension;
                    file.d06_no = d06.d06_no;
                    //取最大值
                    int max=1;
                    try{
                        max=(from d in model.doc07 where d.d06_no==d06.d06_no select d.d07_no).Max();
                        max++;
                    }catch{
                    
                    }


                    file.d07_no = max;

                    model.doc07.AddObject(file);
                    model.SaveChanges();

                }

            }



            



            this.Page.ClientScript.RegisterStartupScript(this.GetType(), "closeThickBox", "self.parent.update();", true);

       
    }
}