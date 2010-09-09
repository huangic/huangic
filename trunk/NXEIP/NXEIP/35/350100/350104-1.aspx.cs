using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NXEIP.DAO;
using Entity;

public partial class _35_350100_350104_1 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            if (Request["mode"].Equals("modify"))
            {
                this.Navigator1.SubFunc = "修改分類";
                this.tbox_sysNo.Visible = false;
                this.span_1.Visible = false;

                sys sysData = new SysDAO().GetBySysNo(Convert.ToInt32(Request["sys_no"]));

                this.lab_sysNo.Text = sysData.sys_no.ToString();
                this.tbox_name.Text = sysData.sys_name;
                this.tbox_order.Text = sysData.sys_order.ToString();

                this.rbl_status.DataBind();
                this.rbl_status.SelectedItem.Selected = false;
                this.rbl_status.Items.FindByValue(sysData.sys_status).Selected = true;

                string src = "";
                src = ResolveClientUrl("~/image/" + sysData.sys_defaultpic);
                this.div_defpic.InnerHtml = "<img src=" + src + " />";

                src = ResolveClientUrl("~/image/" + sysData.sys_overpicture);
                this.div_ovepic.InnerHtml = "<img src=" + src + " />";

            }
            else
            {
                this.Navigator1.SubFunc = "新增分類";
                this.lab_sysNo.Visible = false;
            }
        }
    }


    protected void Button1_Click(object sender, EventArgs e)
    {
        if (Check_Ui())
        {
            if (Request["mode"].Equals("modify"))
            {
                SysDAO sysDao = new SysDAO();
                sys sysData = sysDao.GetBySysNo(Convert.ToInt32(Request["sys_no"]));

                sysData.sys_name = this.tbox_name.Text;
                sysData.sys_order = Convert.ToInt32(this.tbox_order.Text);
                sysData.sys_status = this.rbl_status.SelectedValue;

                string fileSavePath = Server.MapPath("~") + "\\image\\";
                if (this.FileUpload1.HasFile)
                {
                    this.FileUpload1.SaveAs(fileSavePath + this.FileUpload1.FileName);
                    sysData.sys_defaultpic = this.FileUpload1.FileName;
                }

                if (this.FileUpload2.HasFile)
                {
                    this.FileUpload1.SaveAs(fileSavePath + this.FileUpload2.FileName);
                    sysData.sys_overpicture = this.FileUpload2.FileName;
                }

                sysDao.Update();

                //操作記錄
                new OperatesObject().ExecuteOperates(350104, new SessionObject().sessionUserID,3, "更新分類功能編號:" + this.lab_sysNo.Text);

                string url = "350104.aspx?pageIndex=" + Request["pageIndex"];
                this.ShowMsg_URL("修改完成!", url);
            }
            else
            {
                Entity.sys sysData = new sys();

                sysData.sys_name = this.tbox_name.Text;
                sysData.sys_order = Convert.ToInt32(this.tbox_order.Text);
                sysData.sys_status = this.rbl_status.SelectedValue;

                string fileSavePath = Server.MapPath("~") + "\\image\\";
                if (this.FileUpload1.HasFile)
                {
                    this.FileUpload1.SaveAs(fileSavePath + this.FileUpload1.FileName);
                    sysData.sys_defaultpic = this.FileUpload1.FileName;
                }

                if (this.FileUpload2.HasFile)
                {
                    this.FileUpload1.SaveAs(fileSavePath + this.FileUpload2.FileName);
                    sysData.sys_overpicture = this.FileUpload2.FileName;
                }

                SysDAO sysDao = new SysDAO();
                sysDao.AddSys(sysData);
                sysDao.Update();

                //操作記錄
                new OperatesObject().ExecuteOperates(350104, new SessionObject().sessionUserID,1, "新增分類功能編號:" + this.tbox_sysNo.Text);

                this.ShowMsg_URL("新增完成!", "350104.aspx");
            }
        }
    }

    private bool Check_Ui()
    {
        bool check = true;

        //檢查sys_no
        if (Request["mode"].Equals("new"))
        {
            if (!string.IsNullOrEmpty(this.tbox_sysNo.Text))
            {
                try
                {
                    Convert.ToInt32(this.tbox_sysNo.Text);

                    if (Convert.ToInt32(new DBObject().ExecuteScalar("select count(*) as total from sys where sys_no = " + this.tbox_sysNo.Text)) > 0)
                    {
                        this.ShowMsg("系統編號重覆!");
                        check = false;
                    }
                }
                catch
                {
                    this.ShowMsg("系統編號需為數字!");
                    check = false;
                }
            }
            else
            {
                this.ShowMsg("請輸入系統編號!");
                check = false;
            }

            if (!this.FileUpload2.HasFile)
            {
                this.ShowMsg("請上傳未選圖示!");
                check = false;
            }

            if (!this.FileUpload1.HasFile)
            {
                this.ShowMsg("請上傳己選圖示!");
                check = false;
            }
        }

        if (string.IsNullOrEmpty(this.tbox_name.Text))
        {
            this.ShowMsg("請輸入系統名稱!");
            check = false;
        }

        if (string.IsNullOrEmpty(this.tbox_order.Text))
        {
            this.ShowMsg("請輸入系統排序!");
            check = false;
        }
        else
        {
            try
            {
                Convert.ToInt32(this.tbox_order.Text);
            }
            catch
            {
                this.ShowMsg("系統排序需為數字!");
                check = false;
            }
        }

        return check;
    }

    private void ShowMsg(string msg)
    {
        string script = "<script>window.alert('" + msg + "');</script>";
        this.ClientScript.RegisterStartupScript(this.GetType(), "MSG", script);
    }

    private void ShowMsg_URL(string msg,string url)
    {
        string script = "<script>window.alert('" + msg + "');location.replace='" + url + "'</script>";
        this.ClientScript.RegisterStartupScript(this.GetType(), "MyScript", script);
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        string url = "";

        if (Request["mode"].Equals("new"))
        {
            url = "350104.aspx";
        }
        else
        {
            url = "350104.aspx?pageIndex=" + Request["pageIndex"];
        }
        this.ClientScript.RegisterStartupScript(this.GetType(), "MyScript", "<script>location.replace('" + url + "');</script>");
        //Response.Write("<script>location.replace('" + url + "')</script>");
    }
}