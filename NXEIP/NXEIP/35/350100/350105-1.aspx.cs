using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NXEIP.DAO;
using Entity;

public partial class _35_350100_350105_1 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            if (Request["mode"].Equals("modify"))
            {
                this.navigator1.SubFunc = "修改系統";
                this.tbox_sfuNo.Visible = false;
                this.span_1.Visible = false;

                sysfuction sysfuData = new SysfuctionDAO().GetBySfuNo(Convert.ToInt32(Request["sfu_no"]));

                this.lab_sfuNo.Text = sysfuData.sfu_no.ToString();
                this.tbox_name.Text = sysfuData.sfu_name;
                this.tbox_order.Text = sysfuData.sfu_order.ToString();
                this.tbox_path.Text = sysfuData.sfu_path;

                this.rbl_status.DataBind();
                this.rbl_status.SelectedItem.Selected = false;
                this.rbl_status.Items.FindByValue(sysfuData.sfu_status).Selected = true;

                this.rbl_open.SelectedItem.Selected = false;
                this.rbl_open.Items.FindByValue(sysfuData.sys_open).Selected = true;

                this.ddl_sys.DataBind();
                this.ddl_sys.Items.FindByValue(sysfuData.sys_no.ToString()).Selected = true;

                //子系統
                this.ObjectDataSource2.SelectParameters["sys_no"].DefaultValue = sysfuData.sys_no.ToString();
                this.ddl_sysfuction.DataBind();
                this.ddl_sysfuction.Items.FindByValue(sysfuData.sfu_parent.ToString()).Selected = true;


                this.rb_token.SelectedValue = sysfuData.sfu_token ?? "0";

            }
            else
            {
                this.navigator1.SubFunc = "新增系統";
                this.lab_sfuNo.Visible = false;

                this.ddl_sys.DataBind();
                this.ddl_sys.Items[0].Selected = true;

                //子系統
                this.ObjectDataSource2.SelectParameters["sys_no"].DefaultValue = this.ddl_sys.SelectedValue;
                this.ddl_sysfuction.DataBind();
            }
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        if (this.Check_Ui())
        {
            SysfuctionDAO sysfuctionDao = new SysfuctionDAO();
            sysfuction sysfucData = null;
            
            if (Request["mode"].Equals("new"))
            {
                sysfucData = new sysfuction();
                sysfucData.sfu_no = Convert.ToInt32(this.tbox_sfuNo.Text);
            }
            else
            {
                sysfucData = sysfuctionDao.GetBySfuNo(Convert.ToInt32(Request["sfu_no"]));
            }

            sysfucData.sys_no = Convert.ToInt32(this.ddl_sys.SelectedValue);
            sysfucData.sfu_parent = Convert.ToInt32(this.ddl_sysfuction.SelectedValue);
            sysfucData.sfu_name = this.tbox_name.Text;
            sysfucData.sfu_order = Convert.ToInt32(this.tbox_order.Text);
            sysfucData.sfu_path = this.tbox_path.Text;
            sysfucData.sfu_status = this.rbl_status.SelectedValue;
            sysfucData.sfu_createtime = System.DateTime.Now;
            sysfucData.sfu_createuid = Convert.ToInt32(new SessionObject().sessionUserID);
            sysfucData.sys_open = this.rbl_open.SelectedValue;
            sysfucData.sfu_token = this.rb_token.SelectedValue;


            if (Request["mode"].Equals("new"))
            {
                sysfuctionDao.AddSysfuction(sysfucData);
                sysfuctionDao.Update();

                //操作記錄
                new OperatesObject().ExecuteOperates(350105, new SessionObject().sessionUserID,1, "查詢功能編號:" + this.tbox_sfuNo.Text);

                this.ShowMsg_URL("新增完成!", "350105.aspx");
            }
            else
            {
                sysfuctionDao.Update();

                //操作記錄
                new OperatesObject().ExecuteOperates(350105, new SessionObject().sessionUserID, 3, "更新功能編號:" + this.lab_sfuNo.Text);

                this.ShowMsg_URL("修改完成!", "350105.aspx?pageIndex=" + Request["pageIndex"]);
            }
        }
    }

    private bool Check_Ui()
    {
        bool check = true;

        //檢查sys_no
        if (Request["mode"].Equals("new"))
        {
            if (!string.IsNullOrEmpty(this.tbox_sfuNo.Text))
            {
                try
                {
                    Convert.ToInt32(this.tbox_sfuNo.Text);

                    if (Convert.ToInt32(new DBObject().ExecuteScalar("select count(*) as total from sysfuction where sfu_no = " + this.tbox_sfuNo.Text)) > 0)
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

        if (string.IsNullOrEmpty(this.tbox_path.Text))
        {
            this.ShowMsg("請輸入系統路徑!");
            check = false;
        }

        return check;
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        string url = "";

        if (Request["mode"].Equals("new"))
        {
            url = "350105.aspx";
        }
        else
        {
            url = "350105.aspx?pageIndex=" + Request["pageIndex"];
        }
        this.ClientScript.RegisterStartupScript(this.GetType(), "MyScript", "<script>location.replace('" + url + "');</script>");
        //Response.Write("<script>location.replace('" + url + "')</script>");
    }

    private void ShowMsg(string msg)
    {
        string script = "<script>window.alert('" + msg + "');</script>";
        this.ClientScript.RegisterStartupScript(this.GetType(), "MSG", script);
    }

    private void ShowMsg_URL(string msg, string url)
    {
        string script = "<script>window.alert('" + msg + "');location.replace('" + url + "')</script>";
        this.ClientScript.RegisterStartupScript(this.GetType(), "MyScript", script);
    }

    protected void ddl_sys_SelectedIndexChanged(object sender, EventArgs e)
    {
        //子系統
        this.ObjectDataSource2.SelectParameters["sys_no"].DefaultValue = this.ddl_sys.SelectedValue;
        this.ddl_sysfuction.DataBind();
    }
}