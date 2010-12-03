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
   
    String FilePath="/upload/100202/";
    
    protected void Page_Load(object sender, EventArgs e)
    {

        int size = 0;

        int.TryParse(new ArgumentsObject().Get_argValue("100202_size"), out size);

       
            
        
        SessionObject sessionObj=new SessionObject();

        //init
        if (!Page.IsPostBack) {

         
            int peo_uid=int.Parse(sessionObj.sessionUserID);
                 
            this.lb_size.Text = String.Format("(單一檔案限制{0}MB)", size);

           this.DepartTreeListBox1.Clear();
        }

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        SWFUploadFile uf = new SWFUploadFile();

       



        this.Page.ClientScript.RegisterStartupScript(this.GetType(), "closeThickBox", "self.parent.update();", true);

    }
    protected void btn_ok_Click(object sender, EventArgs e)
    {
        //if (UC_SWFUpload1.SWFUploadFileInfoList.Count > 0)
        //{
            SessionObject sessionObj=new SessionObject();


            if (!CheckFields()) {
                return;
            }



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
                    d.tde_achieved = 0;

                    model.treatdetail.AddObject(d);
                    model.SaveChanges();

                }
                else {
                    foreach (var p in DepartTreeListBox1.ItemsValue) {
                        treatdetail d = new treatdetail();
                        d.peo_uid = int.Parse(p);
                        d.tre_no = t.tre_no;
                        d.tde_status = "1";
                        d.tde_achieved = 0;
                        model.treatdetail.AddObject(d);
                    }
                    model.SaveChanges();
                }

               

                #region 檔案存檔


                String uploadDir = new ArgumentsObject().Get_argValue("100202_dir");



                


                for (int i = 1; i <= 3; i++)
                {
                   
                    FileUpload fu=(FileUpload)this.FindControl("FileUpload"+i);

                    if(!fu.HasFile)
                        continue;

                        TreatFile f=new TreatFile(fu);

                        turning file = new turning();
                    
                        //存檔

                        Directory.CreateDirectory(uploadDir + FilePath);

                        fu.SaveAs(uploadDir + FilePath + f.FileName);

                        file.tre_no = t.tre_no;
                        
                        file.tur_file = f.OriginalFileName;
                        
                        file.tur_path = FilePath + f.FileName;
                        file.tur_type = f.Extension;
                        
                    
                        String desc = String.Empty;
                        try
                        {
                            desc = ((TextBox)this.FindControl("tb_file"+i)).Text;
                        }
                        catch
                        {

                        }
                        file.tur_subject = desc;


                        //file.d06_no = d06.d06_no;
                        //取最大值
                        int max = 1;
                        try
                        {
                            max = (from d in model.turning where d.tre_no == t.tre_no select d.tur_no).Max();
                            max++;
                        }
                        catch
                        {

                        }


                        file.tur_no = max;

                        model.turning.AddObject(file);
                        model.SaveChanges();
                        OperatesObject.OperatesExecute(200104, 1, "新增待辦附件 tre_no:{0},tur_no", t.tre_no, file.tur_no);


                    }
                #endregion
                
            }



            



            this.Page.ClientScript.RegisterStartupScript(this.GetType(), "closeThickBox", "self.parent.update();", true);

        //}
        //else {
        //    this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('尚未上傳附件')", true);

        //}
    }

    private bool CheckFields() {

        String msg = "";
        try
        {
            if (this.calendar1._ADDate == null);
            if (this.calendar2._ADDate == null);
        }catch{
            msg += "請輸入工作期間\\n";
        }
       

        if (String.IsNullOrEmpty(msg))
        {

            return true;
        }
        else
        {
            JsUtil.AlertJs(this, msg);
            return false;
        }

    }
}