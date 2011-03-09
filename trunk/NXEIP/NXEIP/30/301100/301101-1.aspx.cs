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
/// 功能名稱：管理作業 / 車輛管理 / 選項設定--新增、修改
/// 功能編號：30/301100/301101
/// 撰寫者：Lina
/// 撰寫時間：2011/03/09
/// </summary>
public partial class _30_301100_301101_1 : System.Web.UI.Page
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
            if (Request["number"] != null) this.lab_number.Text = Request["number"];

            #region 初始值
            if (this.lab_mode.Text.Equals("modify"))
            {
                this.Navigator1.SubFunc = "修改";
                Entity.m01 tbData = new M01DAO().GetByNo(Convert.ToInt32(this.lab_no.Text));
                if (tbData != null)
                {
                    this.txt_name.Text = tbData.m01_name;
                    this.txt_code.Text = tbData.m01_code;
                    try
                    {
                        this.rbl_number.Items.FindByValue(tbData.m01_number).Selected = true;
                    }
                    catch { }
                }
            }
            else
            {
                this.Navigator1.SubFunc = "新增";
                if (this.lab_number.Text.Trim().Length > 0)
                {
                    try
                    {
                        this.rbl_number.Items.FindByValue(this.lab_number.Text).Selected = true;
                    }
                    catch { }
                }
            }
            #endregion
        }
    }

    private bool CheckInputValue()
    {
        #region 輸入值檢查--選項代碼
        if (string.IsNullOrEmpty(this.txt_code.Text))
        {
            ShowMSG("請輸入 選項代碼");
            return false;
        }
        else if (!checkobj.IsValidLen(this.txt_code.Text.Trim(), 2))
        {
            ShowMSG("選項代碼 長度不可超過2個英數字");
            return false;
        }
        #endregion
        #region 輸入值檢查--選項名稱
        if (string.IsNullOrEmpty(this.txt_name.Text))
        {
            ShowMSG("請輸入 選項名稱");
            return false;
        }
        else if (!checkobj.IsValidLen(this.txt_name.Text.Trim(), 30))
        {
            ShowMSG("選項名稱 長度不可超過30個數文字");
            return false;
        }
        #endregion
        #region 檢查此代碼是否已新增
        int pkno = 0;
        if (this.lab_mode.Text.Equals("modify")) pkno = Convert.ToInt32(this.lab_no.Text);
        if ((new M01DAO().GetByNumberCodeCount(this.rbl_number.SelectedValue, this.txt_code.Text.Trim(),pkno))>0)
        {
            ShowMSG("此選項代碼已存在，請修改選項代碼");
            return false;
        }
        #endregion

        return true;
    }

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
                    M01DAO tbDAO = new M01DAO();
                    m01 newRow = tbDAO.GetByNo(Convert.ToInt32(this.lab_no.Text));
                    newRow.m01_code = this.txt_code.Text;
                    newRow.m01_name = this.txt_name.Text;
                    newRow.m01_number = this.rbl_number.SelectedValue;
                    newRow.m01_createtime = System.DateTime.Now;
                    newRow.m01_createuid = Convert.ToInt32(sobj.sessionUserID);
                    tbDAO.Update();

                    msg = "修改成功";
                    //登入記錄(功能編號,人員編號,操作代碼[1新增 2查詢 3更新 4刪除 5保留],備註)
                    new OperatesObject().ExecuteOperates(301101, sobj.sessionUserID, 3, "編號："+this.lab_no.Text+",類別："+this.rbl_number.SelectedItem.Text+",代碼：" + this.txt_code.Text + ",名稱：" + this.txt_name.Text.Trim());
                    #endregion
                }
                else
                {
                    #region 新增
                    M01DAO tbDAO = new M01DAO();
                    m01 newRow = new m01();
                    newRow.m01_code = this.txt_code.Text;
                    newRow.m01_name = this.txt_name.Text;
                    newRow.m01_number = this.rbl_number.SelectedValue;
                    newRow.m01_createtime = System.DateTime.Now;
                    newRow.m01_createuid = Convert.ToInt32(sobj.sessionUserID);
                    newRow.m01_status = "1";
                    tbDAO.AddM01(newRow);
                    tbDAO.Update();

                    msg = "新增成功";
                    //登入記錄(功能編號,人員編號,操作代碼[1新增 2查詢 3更新 4刪除 5保留],備註)
                    new OperatesObject().ExecuteOperates(301101, sobj.sessionUserID, 1, "類別：" + this.rbl_number.SelectedItem.Text + ",代碼：" + this.txt_code.Text + ",名稱：" + this.txt_name.Text.Trim());
                    #endregion
                }
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "closeThickBox", "self.parent.update('" + msg + "');", true);
            }
        }
        catch (Exception ex)
        {
            aMSG = "功能名稱：選項設定-" + this.Navigator1.SubFunc + "<br>錯誤訊息:" + ex.ToString();
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