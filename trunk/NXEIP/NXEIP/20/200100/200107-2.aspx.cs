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
using System.Collections.Specialized;
using AjaxControlToolkit;
using NXEIP.DAO;
using System.Data.Objects.SqlClient;

public partial class _20_200100_200107_2 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        int size = 0;

        int.TryParse(new ArgumentsObject().Get_argValue("200107_size"), out size);
        
        this.UC_SWFUpload1.SwfUploadInfo = new SWFUploadInfo()
        {
            UploadMode = UpMode.LIST,
            File_size_limit = size,
            SubmitButtonId = this.btn_ok.ClientID,
            Path="/upload/200107/",
            PathArg="200107_dir"
        };



        SWFUploadFileInfo uf = new SWFUploadFileInfo();
        DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(SWFUploadFileInfo));
        System.IO.MemoryStream ms = new System.IO.MemoryStream();
        serializer.WriteObject(ms, uf);
        SessionObject sessionObj=new SessionObject();

        //init
        if (!Page.IsPostBack) {
            int peo_uid=int.Parse(sessionObj.sessionUserID);
            
            
            this.Label2.Text = sessionObj.sessionUserName;
            this.Label1.Text = sessionObj.sessionUserDepartName;
            this.lb_size.Text = String.Format("(單一檔案限制{0}MB)", size);
            
          


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
            
            //類別的判斷

            string cat = String.IsNullOrEmpty(this.ddl_childcat.SelectedValue) ? this.ddl_cat.SelectedValue : this.ddl_childcat.SelectedValue;

            int cat_no = int.Parse(cat);

            
            //存檔
            using (NXEIPEntities model = new NXEIPEntities()) {
                doc09 d09 = new doc09();
                d09.d09_createtime = DateTime.Now;
                d09.d09_createuid = int.Parse(sessionObj.sessionUserID);
                d09.d09_depno = int.Parse(sessionObj.sessionUserDepartID);
                d09.d09_date = DateTime.Now;
                d09.d09_open = this.RadioButtonList2.SelectedValue;
                d09.d09_peouid = int.Parse(sessionObj.sessionUserID);
                d09.s06_no = cat_no;
                d09.d09_note = this.tb_note.Text;

                //判斷是否需要審核

                String audit = new ArgumentsDAO().GetValueByVariable("200107_audit");

                if (audit == "1")
                {
                    d09.d09_status = "3";
                }
                else {
                    d09.d09_status = "1";
                    }


                OperatesObject.OperatesExecute(200107, 1, String.Format("新增檔案區 d09_no:{0}",d09.d09_no));
        
                //文檔存檔
                model.doc09.AddObject(d09);
                model.SaveChanges();


                foreach (var f in UC_SWFUpload1.SWFUploadFileInfoList)
                {
                    doc10 file = new doc10();
                    file.d10_count = 0;
                    file.d10_file = f.OriginalFileName;
                    file.d10_path = f.Path + f.FileName;
                    file.d10_type = f.Extension;
                    file.d09_no = d09.d09_no;
                    //取最大值
                    int max=1;
                    try{
                        max=(from d in model.doc10 where d.d09_no==d09.d09_no select d.d10_no).Max();
                        max++;
                    }catch{
                    
                    }


                    file.d10_no = max;

                    model.doc10.AddObject(file);
                    model.SaveChanges();
                    OperatesObject.OperatesExecute(200107, 1, String.Format("新增檔案區附件 d09_no:{0},d10_no:{1}", d09.d09_no,max));
        
                }

            }



            



            this.Page.ClientScript.RegisterStartupScript(this.GetType(), "closeThickBox", "self.parent.update();", true);

        }
        else {
            this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('尚未上傳檔案')", true);

        }
    }

    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static AjaxControlToolkit.CascadingDropDownNameValue[] GetDropDownContents(string knownCategoryValues, string category)
    {
        try
        {
            StringDictionary kv = CascadingDropDown.ParseKnownCategoryValuesString(knownCategoryValues);



            int parentId = int.Parse(kv["undefined"]);


            Sys06DAO dao = new Sys06DAO();

            var data = dao.GetS06FromParentS06(parentId);
            List<CascadingDropDownNameValue> sArray = (from d in data select new CascadingDropDownNameValue { name = d.s06_name, value = SqlFunctions.StringConvert((double)d.s06_no) }).ToList();



            return sArray.ToArray();


        }
        catch
        {


            return default(AjaxControlToolkit.CascadingDropDownNameValue[]);
        }
    }
}