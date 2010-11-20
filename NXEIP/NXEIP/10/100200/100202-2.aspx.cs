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
using NXEIP.DAO;

public partial class _10_100200_100202_2 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        int size = 0;

        int.TryParse(new ArgumentsObject().Get_argValue("100202_size"), out size);

        this.UC_SWFUpload1.SwfUploadInfo = new SWFUploadInfo()
        {
            UploadMode = UpMode.LIST,
            File_size_limit = size,
            SubmitButtonId = this.btn_ok.ClientID,
            Path = "/upload/100202/",
            PathArg = "100202_dir"

        };


        SWFUploadFileInfo uf = new SWFUploadFileInfo();
        DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(SWFUploadFileInfo));
        System.IO.MemoryStream ms = new System.IO.MemoryStream();
        serializer.WriteObject(ms, uf);
            
        
        SessionObject sessionObj=new SessionObject();

        //init
        if (!Page.IsPostBack) {

         
            int peo_uid=int.Parse(sessionObj.sessionUserID);
                 
            this.lb_size.Text = String.Format("(單一檔案限制{0}MB)", size);

            this.GridView1.DataSource = TreatFile.ConvertUploadFile(this.UC_SWFUpload1.SWFUploadFileInfoList);
            this.DepartTreeListBox1.Clear();
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
        //if (UC_SWFUpload1.SWFUploadFileInfoList.Count > 0)
        //{
            SessionObject sessionObj=new SessionObject();
            
            
            //存檔
            using (NXEIPEntities model = new NXEIPEntities()) {
               
                OperatesObject.OperatesExecute(200104, 1, String.Format("新增待辦 d06_no:{0}","1"));
            
                //寫入待辦

                treat t = new treat();


                t.tre_sdate = this.calendar1._ADDate;

                t.tre_edate = this.calendar2._ADDate;

                t.tre_name = this.tb_name.Text;

                t.tre_work = this.tb_work.Text;

                t.tre_createtime = DateTime.Now;
                t.peo_uid = int.Parse(new SessionObject().sessionUserID);


                model.treat.AddObject(t);

                model.SaveChanges();


                #region 建立TreatDetail

                #endregion

                if (this.RadioButtonList1.SelectedValue == "0")
                {
                    treatdetail d = new treatdetail();
                    d.tre_no=t.tre_no;
                    d.peo_uid = int.Parse(new SessionObject().sessionUserID);

                    d.tde_status = "1";

                    model.treatdetail.AddObject(d);
                    model.SaveChanges();

                }
                else {
                    foreach (var p in DepartTreeListBox1.ItemsValue) {
                        treatdetail d = new treatdetail();
                        d.peo_uid = int.Parse(p);
                        d.tre_no = t.tre_no;
                        d.tde_status = "1";

                        model.treatdetail.AddObject(d);
                    }
                    model.SaveChanges();
                }

               

                #region 檔案存檔

                int index=0;
                foreach (var f in UC_SWFUpload1.SWFUploadFileInfoList)
                {

                    turning file = new turning();




                    file.tre_no = t.tre_no;
                    file.tur_file = f.OriginalFileName;
                    file.tur_path = f.Path + f.FileName;
                    file.tur_type = f.Extension;
                    String desc=String.Empty;
                        try{
                    desc=((TextBox)this.GridView1.Rows[index].Cells[0].FindControl("TextBox1")).Text;
                        }catch{
                        
                        }
                        file.tur_subject = desc;
                    
                    
                    //file.d06_no = d06.d06_no;
                    //取最大值
                    int max=1;
                    try{
                        max=(from d in model.turning where d.tre_no==t.tre_no select d.tur_no).Max();
                        max++;
                    }catch{
                    
                    }


                    file.tur_no = max;

                    model.turning.AddObject(file);
                    model.SaveChanges();
                    OperatesObject.OperatesExecute(200104, 1, "新增待辦附件 tre_no:{0},tur_no", t.tre_no,file.tur_no);
            

                }
                #endregion

            }



            



            this.Page.ClientScript.RegisterStartupScript(this.GetType(), "closeThickBox", "self.parent.update();", true);

        //}
        //else {
        //    this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('尚未上傳附件')", true);

        //}
    }
    protected void Button2_Click(object sender, EventArgs e)
    {

        if (this.UC_SWFUpload1.SWFUploadFileInfoList.Count > 0)
        {

            this.Panel_upload.Visible = false;
            this.Panel_uploaded.Visible = true;

            this.GridView1.DataSource = TreatFile.ConvertUploadFile(this.UC_SWFUpload1.SWFUploadFileInfoList);
            this.GridView1.DataBind();
        
        }
        



    }
    
}