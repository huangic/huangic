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
using NXEIP.Lib;

public partial class _20_200800_200802_1 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        String mode = Request["mode"];
        
        
        int size = 0;

        int.TryParse(new ArgumentsObject().Get_argValue("200801_size"), out size);
        
        this.UC_SWFUpload1.SwfUploadInfo = new SWFUploadInfo()
        {
            UploadMode = UpMode.LIST,
            File_size_limit = size,
            File_upload_limit= 1,
            SubmitButtonId = this.btn_ok.ClientID,
            Path="/upload/200801/",
            File_types = "*.jpg;*.gif;*.png",
            File_types_description = "圖片檔",
            PathArg="200801_dir"

        };



        SWFUploadFileInfo uf = new SWFUploadFileInfo();
        DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(SWFUploadFileInfo));
        System.IO.MemoryStream ms = new System.IO.MemoryStream();
        serializer.WriteObject(ms, uf);
        SessionObject sessionObj=new SessionObject();

        //init
        if (!Page.IsPostBack) {
            int peo_uid=int.Parse(sessionObj.sessionUserID);
            
            
            //this.Label2.Text = sessionObj.sessionUserName;
            //this.Label1.Text = sessionObj.sessionUserDepartName;
            this.lb_size.Text = String.Format("(單一檔案限制{0}MB)", size);

            //因為上傳套件會跟樹狀鬼打牆 上傳檔案會觸發!PageIsPostBack
            if (Session["init"] == null)
            {
                
                this.DepartTreeTextBox1.Clear();
                Session["init"] = "1";

                if (!String.IsNullOrEmpty(mode)) {
                    this.PreEdit();
                }
            
            }

            


        }

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        this.UC_SWFUpload1.ClearFile();
        Session["init"] = null;

        JsUtil.UpdateParentJs(this, "");
        //this.Page.ClientScript.RegisterStartupScript(this.GetType(), "closeThickBox", "self.parent.update();", true);

    }
    protected void btn_ok_Click(object sender, EventArgs e)
    {
       

        String mode=Request["mode"];


        String msg=CheckFields();
        if (!String.IsNullOrEmpty(msg)) {
            JsUtil.AlertJs(this, msg);
            return;
        }

        if (String.IsNullOrEmpty(mode))
        {

            if (UC_SWFUpload1.SWFUploadFileInfoList.Count == 0)
            {
                JsUtil.AlertJs(this, "尚未上傳圖片");
                return;
            }

            Add();
        }
        else { 
            
            //修改
            Edit();
        }
        



        Session["init"] = null;
        JsUtil.UpdateParentJs(this, "新增成功");

       
    }

    private String CheckFields() {
        String msg = String.Empty;
        //姓名
        if (this.DepartTreeTextBox1.Items.Count==0) {
            msg += "請輸入姓名\\n";
        }

        //身高

        //體重

        //年齡

        //學歷

        //興趣

        //聯絡方式

        if (!this.tb_order.Text.IsNumber())
        {
            msg += "請輸入順序\\n";
        }


        return msg;
    }


    private void Add() {

        //新增資料

        using (NXEIPEntities model = new NXEIPEntities())
        {


            people p = new PeopleDAO().GetByPeoUID(int.Parse(this.DepartTreeTextBox1.Value));
            //取使用者


            //寫入未婚

            unmarried u = new unmarried();


            u.unm_name = p.peo_name;
            u.unm_depno = p.dep_no;
            u.unm_typno = p.peo_pfofess;

            u.unm_sex = this.RadioButtonList1.SelectedValue;
            u.unm_height = this.tb_height.Text;
            u.unm_weight = this.tb_wight.Text;
            u.unm_age = this.tb_age.Text;
            u.unm_order = int.Parse(this.tb_order.Text);
            u.unm_school = this.tb_school.Text;
            u.unm_interest = this.tb_interest.Text;
            u.unm_contact = this.tb_contact.Text;
            u.unm_condition = this.tb_condition.Text;
            u.unm_introduce = this.tb_introduce.Text;



            u.unm_open = "1";
            u.unm_createtime = DateTime.Now;
            u.unm_createuid = int.Parse(new SessionObject().sessionUserID);


            //檔案存檔
            SWFUploadFileInfo file = this.UC_SWFUpload1.SWFUploadFileInfoList[0];


            u.unm_path = file.Path + file.FileName;
            u.unm_file = file.OriginalFileName;
            u.unm_type = file.Extension;

            model.unmarried.AddObject(u);
            model.SaveChanges();

        }
    
    }

    private void PreEdit() {
        int id = int.Parse(Request["ID"]);

        this.Image1.Visible = true;
        this.Image1.ImageUrl = String.Format("200801-1.ashx?id={0}", id);
        

        //設定欄位

        using(NXEIPEntities model=new NXEIPEntities()){
            //取 未婚資料
            unmarried u = (from d in model.unmarried where d.unm_no == id select d).FirstOrDefault();

            people peo = (from d in model.people where d.peo_name == u.unm_name && u.unm_depno == u.unm_depno select d).FirstOrDefault();

            this.DepartTreeTextBox1.Add(peo.peo_uid);

            this.RadioButtonList1.SelectedValue = u.unm_sex;



            this.tb_height.Text = u.unm_height;
            this.tb_wight.Text = u.unm_weight;
            this.tb_age.Text = u.unm_age;
            this.tb_order.Text = u.unm_order + "";
            this.tb_school.Text = u.unm_school;
            this.tb_interest.Text = u.unm_interest;
            this.tb_contact.Text = u.unm_contact;
            this.tb_condition.Text = u.unm_condition;
            this.tb_introduce.Text = u.unm_introduce;



        }



    }


    private void Edit()
    {
        int id = int.Parse(Request["ID"]);
        //新增資料

        using (NXEIPEntities model = new NXEIPEntities())
        {


            people p = new PeopleDAO().GetByPeoUID(int.Parse(this.DepartTreeTextBox1.Value));
            //取使用者


            //寫入未婚

            unmarried u = (from d in model.unmarried where d.unm_no == id select d).FirstOrDefault();

            

            u.unm_name = p.peo_name;
            u.unm_depno = p.dep_no;
            u.unm_typno = p.peo_pfofess;

            u.unm_sex = this.RadioButtonList1.SelectedValue;
            u.unm_height = this.tb_height.Text;
            u.unm_weight = this.tb_height.Text;
            u.unm_age = this.tb_age.Text;
            u.unm_order = int.Parse(this.tb_order.Text);
            u.unm_school = this.tb_school.Text;
            u.unm_interest = this.tb_interest.Text;
            u.unm_contact = this.tb_contact.Text;
            u.unm_condition = this.tb_condition.Text;
            u.unm_introduce = this.tb_introduce.Text;



            u.unm_open = "1";
            u.unm_createtime = DateTime.Now;
            u.unm_createuid = int.Parse(new SessionObject().sessionUserID);


            //檔案存檔


            if (this.UC_SWFUpload1.SWFUploadFileInfoList.Count > 0)
            {
                //刪除舊檔案
                SWFUploadFile uf = new SWFUploadFile();
                String rootDir = new ArgumentsObject().Get_argValue("200801_dir")+"/upload/200801/";

                String msg=uf.Delete(rootDir,u.unm_path,true);

                
                //載入新的
                SWFUploadFileInfo file = this.UC_SWFUpload1.SWFUploadFileInfoList[0];
                u.unm_path = file.Path + file.FileName;
                u.unm_file = file.OriginalFileName;
                u.unm_type = file.Extension;
            }
           
            model.SaveChanges();

        }

    }
}