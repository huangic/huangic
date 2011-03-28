using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using NXEIP.DAO;
using Entity;

/// <summary>
/// 功能名稱：管理作業 / 資料管理 / 電腦管理--新增、修改
/// 功能編號：30/300500/300504
/// 撰寫者：Lina
/// 撰寫時間：2011/03/28
/// </summary>
public partial class _30_300500_300504_1 : System.Web.UI.Page
{
    DBObject dbo = new DBObject();
    CheckObject checkobj = new CheckObject();
    SessionObject sobj = new SessionObject();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            if (Request["mode"] != null) this.lab_mode.Text = Request["mode"];
            if (Request["no"] != null) this.lab_no.Text = Request["no"];

            this.DepartTreeTextBox1.Clear();
            this.DepartTreeTextBox2.Clear();

            #region 初始值
            if (this.lab_mode.Text.Equals("modify"))
            {
                this.Navigator1.SubFunc = "修改";
                Entity.dispatch tbData = new DispatchDAO().GetByNo(Convert.ToInt32(this.lab_no.Text));
                if (tbData != null)
                {
                    this.txt_name.Text = tbData.dis_name;
                    this.txt_memo.Text = tbData.dis_memo;
                    if (tbData.dis_outpeouid.HasValue) this.DepartTreeTextBox1.Add(tbData.dis_outpeouid.ToString());
                    if (tbData.dis_inpeouid.HasValue) this.DepartTreeTextBox2.Add(tbData.dis_inpeouid.ToString());
                    if (tbData.dis_outdate.HasValue) this.cl_outdate._ADDate = Convert.ToDateTime(tbData.dis_outdate.Value.ToString("yyyy/MM/dd"));
                    if (tbData.dis_indate.HasValue) this.cl_indate._ADDate = Convert.ToDateTime(tbData.dis_indate.Value.ToString("yyyy/MM/dd"));
                    try
                    {
                        this.rbl_change.Items.FindByValue(tbData.dis_change).Selected = true;
                    }
                    catch { }
                    try
                    {
                        this.rbl_retun.Items.FindByValue(tbData.dis_return).Selected = true;
                    }
                    catch { }
                }
            }
            else
            {
                this.Navigator1.SubFunc = "新增";
                this.rbl_change.Items[0].Selected = true;
                this.rbl_retun.Items[0].Selected = true;
                this.cl_indate._ADDate = System.DateTime.Today;
                this.cl_outdate._ADDate = System.DateTime.Today;
            }
            #endregion
        }
    }
    #region 輸入值檢查
    private bool CheckInputValue()
    {
        #region 電腦名稱
        if (string.IsNullOrEmpty(this.txt_name.Text))
        {
            ShowMSG("請輸入 電腦名稱");
            return false;
        }
        else if (!checkobj.IsValidLen(this.txt_name.Text.Trim(), 40))
        {
            ShowMSG("電腦名稱 長度不可超過40個中文字");
            return false;
        }
        #endregion
        #region 移送記錄
        if (!checkobj.IsValidLen(this.txt_memo.Text.Trim(), 200))
        {
            ShowMSG("移送記錄 長度不可超過200個中文字");
            return false;
        }
        #endregion
        #region 移出人
        if (this.DepartTreeTextBox1.Items.Count <= 0 || this.DepartTreeTextBox1.Items == null)
        {
            ShowMSG("請選擇 移出人");
            return false;
        }
        #endregion
        #region 移入人
        if (this.DepartTreeTextBox2.Items.Count <= 0 || this.DepartTreeTextBox2.Items == null)
        {
            ShowMSG("請選擇 移入人");
            return false;
        }
        #endregion
        #region 移出日期、移入日期
        DateTime outdate = new DateTime();
        DateTime indate = new DateTime();
        try
        {
            outdate = Convert.ToDateTime(this.cl_outdate._ADDate.ToString("yyyy/MM/dd"));
        }
        catch
        {
            ShowMSG("移出日期 錯誤!!");
            return false;
        }
        try
        {
            indate = Convert.ToDateTime(this.cl_indate._ADDate.ToString("yyyy/MM/dd"));
        }
        catch
        {
            ShowMSG("移入日期 錯誤!!");
            return false;
        }
        #endregion
        
        return true;
    }
    #endregion

    #region 確定
    protected void btn_submit_Click(object sender, EventArgs e)
    {
        string aMSG = "";
        try
        {
            string msg = "";
            if (CheckInputValue())
            {
                if (this.lab_mode.Text.Equals("modify"))
                {
                    #region 修改
                    DispatchDAO tbDAO = new DispatchDAO();
                    dispatch newRow = tbDAO.GetByNo(Convert.ToInt32(this.lab_no.Text));
                    newRow.dis_change = this.rbl_change.SelectedValue;
                    newRow.dis_createtime = System.DateTime.Now;
                    newRow.dis_createuid=Convert.ToInt32(sobj.sessionUserID);
                    newRow.dis_indate=this.cl_indate._ADDate;
                    newRow.dis_inpeouid=Convert.ToInt32(this.DepartTreeTextBox2.Value);
                    newRow.dis_memo=this.txt_memo.Text;
                    newRow.dis_name=this.txt_name.Text;
                    newRow.dis_outdate=this.cl_outdate._ADDate;
                    newRow.dis_outpeouid=Convert.ToInt32(this.DepartTreeTextBox1.Value);
                    newRow.dis_return=this.rbl_retun.SelectedValue;
                    tbDAO.Update();

                    msg = "修改成功";
                    //登入記錄(功能編號,人員編號,操作代碼[1新增 2查詢 3更新 4刪除 5保留],備註)
                    new OperatesObject().ExecuteOperates(300504, sobj.sessionUserID, 3, "編號：" + this.lab_no.Text + ",問卷名稱：" + this.txt_name.Text.Trim());
                    #endregion
                }
                else
                {
                    #region 新增
                    DispatchDAO tbDAO = new DispatchDAO();
                    dispatch newRow = new dispatch();
                    newRow.dis_change = this.rbl_change.SelectedValue;
                    newRow.dis_createtime = System.DateTime.Now;
                    newRow.dis_createuid = Convert.ToInt32(sobj.sessionUserID);
                    newRow.dis_indate = this.cl_indate._ADDate;
                    newRow.dis_inpeouid = Convert.ToInt32(this.DepartTreeTextBox2.Value);
                    newRow.dis_memo = this.txt_memo.Text;
                    newRow.dis_name = this.txt_name.Text;
                    newRow.dis_outdate = this.cl_outdate._ADDate;
                    newRow.dis_outpeouid = Convert.ToInt32(this.DepartTreeTextBox1.Value);
                    newRow.dis_return = this.rbl_retun.SelectedValue;
                    newRow.dis_status = "1";
                    tbDAO.AddDispatch(newRow);
                    tbDAO.Update();

                    msg = "新增成功";
                    //登入記錄(功能編號,人員編號,操作代碼[1新增 2查詢 3更新 4刪除 5保留],備註)
                    new OperatesObject().ExecuteOperates(300504, sobj.sessionUserID, 1, "電腦管理：" + this.txt_name.Text.Trim());
                    #endregion
                }

                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "closeThickBox", "self.parent.update('" + msg + "');", true);
            }
        }
        catch (Exception ex)
        {
            aMSG = "功能名稱：電腦管理-" + this.Navigator1.SubFunc + "<br>錯誤訊息:" + ex.ToString();
            Response.Write(aMSG);
        }
    }
    #endregion

    #region 顯示錯誤訊息
    private void ShowMSG(string msg)
    {
        string script = "<script>alert('" + msg + "');</script>";
        this.ClientScript.RegisterStartupScript(this.GetType(), "msg", script);
    }
    #endregion
}