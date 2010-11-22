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

public partial class _10_100200_100202_3 : System.Web.UI.Page
{
   
    String FilePath="/upload/100202/";
    
    protected void Page_Load(object sender, EventArgs e)
    {

        int size = 0;

        int.TryParse(new ArgumentsObject().Get_argValue("100202_size"), out size);

       
            
        
        SessionObject sessionObj=new SessionObject();

        //init
        if (!Page.IsPostBack) {


            this.hidden_tde_no.Value = Request["id"];

            int peo_uid=int.Parse(sessionObj.sessionUserID);
                 
            this.lb_size.Text = String.Format("(單一檔案限制{0}MB)", size);

          
            //取工作項目
            int id=int.Parse(Request["id"]);

            using(NXEIPEntities model=new NXEIPEntities()){
                treatdetail d = (from td in model.treatdetail where td.tde_no == id select td).First();

                this.lb_name.Text = d.treat.tre_name;
                this.tb_work.Text = d.tde_description;
                this.DropDownList1.SelectedValue = d.tde_achieved.HasValue ?d.tde_achieved.Value.ToString() :"0";
                this.lb_achievedd.Text = d.tde_achieved.HasValue ? d.tde_achieved.Value.ToString() : "0";

            }



        }

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        

        this.Page.ClientScript.RegisterStartupScript(this.GetType(), "closeThickBox", "self.parent.update();", true);

    }
    protected void btn_ok_Click(object sender, EventArgs e)
    {
        this.save("1");
    }



    private void save(string status) {
        //if (UC_SWFUpload1.SWFUploadFileInfoList.Count > 0)
        //{
        SessionObject sessionObj = new SessionObject();

        int id = int.Parse(this.hidden_tde_no.Value);

        //存檔
        using (NXEIPEntities model = new NXEIPEntities())
        {

            OperatesObject.OperatesExecute(200104, 1, String.Format("更新待辦 Tde_no:{0}", id));

            //寫入待辦

            treatdetail td = new treatdetail();

            td.tde_no = id;

            model.treatdetail.Attach(td);


            td.tde_relydate = DateTime.Now;

            td.tde_achieved = int.Parse(DropDownList1.SelectedValue);

            td.tde_description = this.tb_work.Text;

            td.tde_status = status;






            model.SaveChanges();





            #region 檔案存檔


            String uploadDir = new ArgumentsObject().Get_argValue("100202_dir");


            //check file

            for (int i = 1; i <= 3; i++)
            {
                FileUpload fu = (FileUpload)this.FindControl("FileUpload" + i);
                if (fu.HasFile)
                {
                    //刪除檔案
                    var fs = (from d in model.goback where d.tde_no == id select d);
                    foreach (var f in fs)
                    {
                        model.goback.Detach(f);
                    }
                    model.SaveChanges();
                    break;
                }
            }


            for (int i = 1; i <= 3; i++)
            {

                FileUpload fu = (FileUpload)this.FindControl("FileUpload" + i);

                if (!fu.HasFile)
                    continue;

                TreatFile f = new TreatFile(fu);

                goback file = new goback();

                //存檔

                Directory.CreateDirectory(uploadDir + FilePath);

                fu.SaveAs(uploadDir + FilePath + f.FileName);

                file.tde_no = id;

                file.gob_file = f.OriginalFileName;

                file.gob_path = FilePath + f.FileName;
                file.gob_type = f.Extension;


                String desc = String.Empty;
                try
                {
                    desc = ((TextBox)this.FindControl("tb_file" + i)).Text;
                }
                catch
                {

                }
                file.gob_subject = desc;


                //file.d06_no = d06.d06_no;
                //取最大值
                int max = 1;
                try
                {
                    max = (from d in model.goback where d.tde_no == id select d.gob_no).Max();
                    max++;
                }
                catch
                {

                }


                file.gob_no = max;

                model.goback.AddObject(file);
                model.SaveChanges();
                OperatesObject.OperatesExecute(200104, 1, "新增待辦回復附件 tde_no:{0},gob_no", id, file.gob_no);


            }
            #endregion

        }







        this.Page.ClientScript.RegisterStartupScript(this.GetType(), "closeThickBox", "self.parent.update();", true);

        //}
        //else {
        //    this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('尚未上傳附件')", true);

        //}
    }

    protected void btn_complete_Click(object sender, EventArgs e)
    {

        if (this.DropDownList1.SelectedValue == "100")
        {
           this.save("2");
        }
        else {
            this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('進度非100%')", true);
        }
    }
}