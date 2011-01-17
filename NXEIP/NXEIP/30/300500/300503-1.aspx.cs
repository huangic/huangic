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

public partial class _30_300500_300503_1 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        int size = 0;

        int.TryParse(new ArgumentsObject().Get_argValue("300503_size"), out size);
        
        this.UC_SWFUpload1.SwfUploadInfo = new SWFUploadInfo()
        {
            UploadMode = UpMode.LIST,
            File_size_limit = size,
            SubmitButtonId = this.btn_ok.ClientID,
            Path="/upload/tmp/",
            File_upload_limit=1

        };



        SWFUploadFileInfo uf = new SWFUploadFileInfo();
        DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(SWFUploadFileInfo));
        System.IO.MemoryStream ms = new System.IO.MemoryStream();
        serializer.WriteObject(ms, uf);
        SessionObject sessionObj=new SessionObject();

        //init
        if (!Page.IsPostBack) {

            if (Request["mode"] != "edit")
            {
                           

              
                this.lb_size.Text = String.Format("(單一檔案限制{0}MB)", size);

              
            }
            else {

                int off_no = int.Parse(Request["ID"]);


                using (NXEIPEntities model = new NXEIPEntities())
                {
                    //編輯模式
                    officials o =(from d in model.officials where d.off_no==off_no select d).First();

                    this.DropDownList1.SelectedValue = o.off_type;
                    this.tb_memo.Text = o.off_memo;

                }

            
            }




        }

    }

    //private void 
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

    private void add() {
        SessionObject sessionObj = new SessionObject();


        //存檔
        using (NXEIPEntities model = new NXEIPEntities())
        {

            officials o = new officials();

            o.off_depname = sessionObj.sessionUserDepartName;
            o.off_createuid = int.Parse(sessionObj.sessionUserID);
            o.off_memo = this.tb_memo.Text;

            o.off_peouid = int.Parse(sessionObj.sessionUserID);
            o.off_createtime = DateTime.Now;
            o.off_type = this.DropDownList1.SelectedValue;
            o.off_status = "1";
            o.off_tel = new UtilityDAO().Get_PeopleTel(int.Parse(sessionObj.sessionUserID));


            //存檔
            if (this.UC_SWFUpload1.SWFUploadFileInfoList.Count > 0) {
                SWFUploadFileInfo infos = this.UC_SWFUpload1.SWFUploadFileInfoList[0];

                o.off_name = infos.OriginalFileName;
                //讀黨

                ArgumentsObject args = new ArgumentsObject();

                string path = "/upload/tmp/";
                string fileabspath = path + infos.FileName;
                fileabspath = this.Server.MapPath("~/"+fileabspath);
                o.off_file = File.ReadAllBytes(fileabspath);
                //o.off_file=infos.Path

                File.Delete(fileabspath);
            
            }


            //文檔存檔
            model.officials.AddObject(o);
            model.SaveChanges();
            OperatesObject.OperatesExecute(200104, 1, String.Format("新增公物電話 off_no:{0}",o.off_no));



        }







        JsUtil.UpdateParentJs(this, "存檔成功");

    }


    private void edit() {
        SessionObject sessionObj = new SessionObject();
        int off_no = int.Parse(Request["ID"]);

        //存檔
        using (NXEIPEntities model = new NXEIPEntities())
        {

            officials o = new officials();
            o.off_no = off_no;

            model.officials.Attach(o);


            o.off_depname = sessionObj.sessionUserDepartName;
            o.off_createuid = int.Parse(sessionObj.sessionUserID);
            o.off_memo = this.tb_memo.Text;

            o.off_peouid = int.Parse(sessionObj.sessionUserID);
            o.off_createtime = DateTime.Now;
            o.off_type = this.DropDownList1.SelectedValue;
            o.off_status = "1";
            o.off_tel = new UtilityDAO().Get_PeopleTel(int.Parse(sessionObj.sessionUserID));


            //存檔
            if (this.UC_SWFUpload1.SWFUploadFileInfoList.Count > 0)
            {
                SWFUploadFileInfo infos = this.UC_SWFUpload1.SWFUploadFileInfoList[0];

                o.off_name = infos.OriginalFileName;
                //讀黨

                ArgumentsObject args = new ArgumentsObject();

                string path = "/upload/tmp/";
                string fileabspath = path + infos.FileName;
                fileabspath = this.Server.MapPath("~/" + fileabspath);
                o.off_file = File.ReadAllBytes(fileabspath);
                //o.off_file=infos.Path

                File.Delete(fileabspath);
            }


            //文檔存檔
            model.SaveChanges();
            OperatesObject.OperatesExecute(200104, 1, String.Format("修改公物電話 off_no:{0}", o.off_no));



        }







        JsUtil.UpdateParentJs(this, "修改成功");
    }

    protected void btn_ok_Click(object sender, EventArgs e)
    {

        if (Request["mode"] != "edit")
        {
            add();
        }
        else {
            edit();
        }
        
        
    }
}