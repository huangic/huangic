using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NXEIP.DAO;
using Entity;
using System.IO;
using NLog;

public partial class _10_100600_100601_3 : System.Web.UI.Page
{
    private static Logger logger = LogManager.GetCurrentClassLogger();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            int mee_no = int.Parse(Request.QueryString["mee_no"]);

            this.hidd_meeno.Value = mee_no.ToString();

            this.ObjectDataSource1.SelectParameters["mee_no"].DefaultValue = this.hidd_meeno.Value;
            this.GridView1.DataBind();

            meetings d = new _100601DAO().GetMeetings(mee_no);
            ChangeObject cobj = new ChangeObject();
            UtilityDAO udao = new UtilityDAO();

            this.lab_reason.Text = d.mee_reason;
            this.lab_place.Text = d.mee_place;
            this.lab_host.Text = udao.Get_PeopleName(d.mee_host.Value);
            this.lab_date.Text = cobj._ADtoROCDT(d.mee_sdate.Value) + "~" + cobj._ADtoROCDT(d.mee_edate.Value);

            if (DateTime.Now <= d.mee_edate.Value)
            {
                JsUtil.AlertJs(this,"會議尚未結束!");
                this.FileUpload1.Enabled = false;
                this.FileUpload2.Enabled = false;
                this.FileUpload3.Enabled = false;
                this.btn_ok.Enabled = false;
            }
        }
    }

    protected void btn_ok_Click(object sender, EventArgs e)
    {
        using (NXEIPEntities model = new NXEIPEntities())
        {
            int mee_no = int.Parse(this.hidd_meeno.Value);

            String FilePath = "/upload/100601/";
            String uploadDir = new ArgumentsObject().Get_argValue("100601_dir");
            Directory.CreateDirectory(uploadDir + FilePath);

            for (int i = 1; i <= 3; i++)
            {
                FileUpload fu = (FileUpload)this.FindControl("FileUpload" + i);

                if (fu.HasFile && !string.IsNullOrEmpty(uploadDir))
                {
                    string filename = DateTime.Now.ToString("yMdhhmmssfff") + Path.GetExtension(fu.FileName);

                    //上傳檔案
                    try
                    {
                        fu.SaveAs(uploadDir + FilePath + filename);
                    }
                    catch (Exception ex)
                    {
                        logger.Debug(ex.Message);
                    }

                    //會議紀錄
                    conferen d = new conferen();

                    int max = 1;
                    try
                    {
                        max = (from p in model.conferen where p.mee_no == mee_no select p.con_no).Max();
                        max++;
                    }
                    catch
                    {
                    }

                    d.mee_no = mee_no;
                    d.con_no = max;
                    d.con_type = Path.GetExtension(fu.FileName);
                    d.con_path = FilePath + filename;
                    d.con_file = fu.FileName;

                    model.conferen.AddObject(d);
                    model.SaveChanges();

                }
            }
        }

        this.GridView1.DataBind();


    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "del")
        {
            int mee_no = int.Parse(this.GridView1.DataKeys[int.Parse(e.CommandArgument.ToString())].Values[0].ToString());
            int con_no = int.Parse(this.GridView1.DataKeys[int.Parse(e.CommandArgument.ToString())].Values[1].ToString());
            
            using (NXEIPEntities model = new NXEIPEntities())
            {
                conferen c = (from d in model.conferen where d.mee_no == mee_no && d.con_no == con_no select d).FirstOrDefault();
                if (c != null)
                {
                    string filename = c.con_file;
                    model.conferen.DeleteObject(c);
                    model.SaveChanges();
                    OperatesObject.OperatesExecute(100601, 4, String.Format("刪除會議記錄 mee_no:{0} FileName:{1}", mee_no, filename));
                }
            }

            this.GridView1.DataBind();
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        this.Page.ClientScript.RegisterStartupScript(this.GetType(), "closeThickBox", "self.parent.update2();", true);
    }
}