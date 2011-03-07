using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Specialized;
using AjaxControlToolkit;
using NXEIP.DAO;
using System.Data.Objects.SqlClient;
using Entity;
using NLog;
using System.Data;

public partial class _30_300700_300703 : System.Web.UI.Page
{
    private static Logger logger = LogManager.GetCurrentClassLogger();

    protected void Page_Load(object sender, EventArgs e)
    {

        if (!Page.IsPostBack)
        {

           
            this.GridView1.DataBind();
        }
      




       
    }






    protected static string GetDepartmentName(int dep_no)
    {
        using (NXEIPEntities model = new NXEIPEntities())
        {
            var dep = (from d in model.departments where d.dep_no == dep_no select d).First();

            return dep.dep_name;

        }

    }







    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {




    }
   





    protected string GetPeopleName(int? peoUid)
    {
        if (peoUid.HasValue)
        {
            return new PeopleDAO().GetPeopleNameByUid(peoUid.Value);
        }
        else
        {
            return "";
        }
    }

    protected string GetROCDate(DateTime? date)
    {
        if (date.HasValue)
        {
            return new ChangeObject()._ADtoROC(date.Value).ToString();
        }
        else
        {
            return "";
        }
    }


    protected string GetStatus(string status)
    {
        if (status == "1")
        {
            return "通過";
        }
        if (status == "2")
        {
            return "未通過";
        }
        if (status == "3")
        {
            return "審核中";
        }
        if (status == "4")
        {
            return "刪除";
        }
        return "";
    }


    protected void btn_ok_Click(object sender, EventArgs e)
    {
        int peo_uid = int.Parse(new SessionObject().sessionUserID);

        using (NXEIPEntities model = new NXEIPEntities())
        {

            //核可全部選擇的項目
            for (int i = 0; i < this.GridView1.Rows.Count; i++)
            {
                if (((CheckBox)(this.GridView1.Rows[i].FindControl("cbox"))).Checked)
                {
                    int id = System.Convert.ToInt32(this.GridView1.DataKeys[i].Value.ToString());
                    email mail;

                    mail=(from d in model.email where d.ema_no==id select d).Single();

                                       
                    

                    mail.ema_status = "1";
                    mail.ema_checkdate = DateTime.Now;
                    mail.ema_check = peo_uid;

                    //更新此使用者的EMAIL

                    people peo = new people();
                    peo.peo_uid = mail.ema_peouid.Value;

                    model.people.Attach(peo);
                    peo.peo_email = mail.ema_mail;



                    OperatesObject.OperatesExecute(300703, 3, "電子郵件審核通過 ema_no:{0},ema_status:通過",id);
                }


                
            }

            model.SaveChanges();

        }
        this.GridView1.DataBind();

    }



    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        //logger.Debug("msg" + this.hidden_reason.Value);
        
        int peo_uid = int.Parse(new SessionObject().sessionUserID);
        string reason=this.hidden_reason.Value;
        using (NXEIPEntities model = new NXEIPEntities())
        {

            //退回核可全部選擇的項目
            for (int i = 0; i < this.GridView1.Rows.Count; i++)
            {
                if (((CheckBox)(this.GridView1.Rows[i].FindControl("cbox"))).Checked)
                {
                    int id = System.Convert.ToInt32(this.GridView1.DataKeys[i].Value.ToString());
                    email mail = new email();


                    mail.ema_no = id;


                    model.email.Attach(mail);
                    mail.ema_status = "2";
                    mail.ema_checkdate = DateTime.Now;
                    mail.ema_check = peo_uid;
                    mail.ema_note = reason;
                    OperatesObject.OperatesExecute(300703, 3, "檔案區審核 ema_no:{0},ema_status:未通過,d09_reason:{1}", id, reason);
                }



            }

            model.SaveChanges();

        }
        this.GridView1.DataBind();

    }
}