using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NXEIP.DAO;
using Entity;
using NLog;

public partial class _30_300600_300604_1 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {

            if (Request.QueryString["mode"] != null)
            {
                if (Request.QueryString["mode"] == "modify")
                {
                    this.Navigator1.SubFunc = "編輯廠商";
                    this.hidd_r04no.Value = Request.QueryString["r04_no"];

                    rep04 data = new Rep04DAO().GetRep04(int.Parse(this.hidd_r04no.Value));

                    this.tbox_cont.Text = data.r04_contact;
                    this.tbox_fax.Text = data.r04_fax;
                    this.tbox_mail.Text = data.r04_email;
                    this.tbox_name.Text = data.r04_name;
                    this.tbox_tel.Text = data.r04_tel;
                    
                }
                else
                {
                    this.Navigator1.SubFunc = "新增廠商";

                }

            }
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        if (this.CheckInput())
        {
            string msg = "";

            Rep04DAO dao = new Rep04DAO();

            if (this.hidd_r04no.Value != "")
            {

                rep04 data = dao.GetRep04(int.Parse(this.hidd_r04no.Value));

                data.r04_contact = this.tbox_cont.Text;
                data.r04_createtime = DateTime.Now;
                data.r04_createuid = int.Parse(new SessionObject().sessionUserID);
                data.r04_email = this.tbox_mail.Text;
                data.r04_fax = this.tbox_fax.Text;
                data.r04_name = this.tbox_name.Text;
                data.r04_tel = this.tbox_tel.Text;

                dao.Update();

                msg = "修改完成!";

                OperatesObject.OperatesExecute(300604, 3, string.Format("修改廠商 r04_no:{0}", data.r04_no));
            }
            else
            {
                rep04 data = new rep04();

                data.r04_contact = this.tbox_cont.Text;
                data.r04_createtime = DateTime.Now;
                data.r04_createuid = int.Parse(new SessionObject().sessionUserID);
                data.r04_email = this.tbox_mail.Text;
                data.r04_fax = this.tbox_fax.Text;
                data.r04_name = this.tbox_name.Text;
                data.r04_tel = this.tbox_tel.Text;
                data.r04_status = "1";

                dao.AddToRep04(data);
                dao.Update();

                msg = "新增完成!";

                OperatesObject.OperatesExecute(300604, 1, string.Format("新增廠商 r04_no:{0}", data.r04_no));
            }

            

            this.Page.ClientScript.RegisterStartupScript(this.GetType(), "closeThickBox", "self.parent.update('" + msg + "');", true);

        }



    }

    private bool CheckInput()
    {
        if (this.tbox_name.Text.Trim().Length == 0)
        {
            JsUtil.AlertJs(this, "請輸入廠商名稱!");
            return false;
        }

        if (this.tbox_cont.Text.Trim().Length == 0)
        {
            JsUtil.AlertJs(this, "請輸入連絡人!");
            return false;
        }
        if (this.tbox_tel.Text.Trim().Length == 0)
        {
            JsUtil.AlertJs(this, "請輸入連絡電話!");
            return false;
        }
        

        return true;
    }

}