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

public partial class _20_200100_200104_2 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.UC_SWFUpload1.SwfUploadInfo = new SWFUploadInfo()
        {
            UploadMode = UpMode.LIST,
            File_size_limit = 10,
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
            this.Label2.Text = sessionObj.sessionUserName;
            this.Label1.Text = sessionObj.sessionUserDepartName;
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
                doc06 d06 = new doc06();
                d06.d06_createuid = int.Parse(sessionObj.sessionUserID);
                d06.d06_depno = int.Parse(sessionObj.sessionUserDepartID);
                d06.d06_date = DateTime.Now;
                d06.d06_tel = tb_tel.Text;
                d06.d06_number = tb_number.Text;
                d06.d06_open = this.RadioButtonList1.SelectedValue;
                d06.d06_peouid = int.Parse(sessionObj.sessionUserID);
                d06.d06_ext = tb_ext.Text;


                //文檔存檔
                model.doc06.AddObject(d06);
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
        else {
            this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('尚未上傳附件')", true);

        }
    }
}