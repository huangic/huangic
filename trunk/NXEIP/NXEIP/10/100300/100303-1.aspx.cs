using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data;
using NXEIP.DAO;
using Entity;

public partial class _10_100300_100303_1 : System.Web.UI.Page
{
    SessionObject sobj = new SessionObject();
    DBObject dbo = new DBObject();
    protected string keyvalue = "-1";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            //登入記錄(功能編號,人員編號,操作代碼[1新增 2查詢 3更新 4刪除 5保留],備註)
            new OperatesObject().ExecuteOperates(100303, sobj.sessionUserID, 2, "新增查看權限頁面");
            if (this.DepartTreeListBox1.Items != null) this.DepartTreeListBox1.Items.Clear();
            if (this.DepartTreeListBox2.Items != null) this.DepartTreeListBox2.Items.Clear();
        }
    }
    #region 檢查輸入值
    private bool CheckInputValue()
    {
        string sqlstr = "";
        DataTable dt = new DataTable();

        if (this.rb_1.Checked)
        {
            #region 人事編號
            if (this.txt_workid.Text.Trim().Length <= 0)
            {
                ShowMSG("請輸入 人事編號");
                return false;
            }
            else
            {
                #region 檢查是否已新增
                sqlstr = "SELECT c04.peo_uid, people.peo_name FROM c04 INNER JOIN people ON c04.peo_uid = people.peo_uid WHERE (people.peo_workid = '" + this.txt_workid.Text + "')";
                dt = dbo.ExecuteQuery(sqlstr);
                if (dt.Rows.Count > 0)
                {
                    ShowMSG(dt.Rows[0]["peo_name"].ToString() + " 已在名單中");
                    return false;
                }
                else
                {
                    return true;
                }
                #endregion
            }
            #endregion
        }
        else if (this.rb_2.Checked)
        {
            #region 人員清單
            if (this.DepartTreeListBox1.Items.Count <= 0 || this.DepartTreeListBox1.Items == null)
            {
                ShowMSG("請選擇人員");
                return false;
            }
            else
            {
                #region 檢查是否已新增
                for (int i = 0; i < this.DepartTreeListBox1.Items.Count; i++)
                {
                    keyvalue += "," + this.DepartTreeListBox1.Items[i].Key;
                }
                sqlstr = "SELECT c04.peo_uid, people.peo_name FROM c04 INNER JOIN people ON c04.peo_uid = people.peo_uid WHERE (people.peo_uid in (" + keyvalue + "))";
                dt = dbo.ExecuteQuery(sqlstr);
                if (dt.Rows.Count > 0)
                {
                    string peo_name = "";//儲存已新增人員名單
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (peo_name.Length > 0) peo_name += "、";
                        peo_name += dt.Rows[i]["peo_name"].ToString();
                    }
                    ShowMSG(peo_name + " 已在名單中");
                    return false;
                }
                else
                {
                    return true;
                }
                #endregion
            }
            #endregion
        }
        else
        {
            #region 單位清單
            if (this.DepartTreeListBox2.Items.Count <= 0 || this.DepartTreeListBox2.Items == null)
            {
                ShowMSG("請選擇單位");
                return false;
            }
            else
            {
                #region 檢查是否已新增
                for (int i = 0; i < this.DepartTreeListBox2.Items.Count; i++)
                {
                    keyvalue += "," + this.DepartTreeListBox2.Items[i].Key;
                }
                return true;
                #endregion
            }
            #endregion
        }
    }
    #endregion

    #region 確定
    protected void btn_submit_Click(object sender, EventArgs e)
    {
        string aMSG = "";   //記錄錯誤訊息
        bool isSuccess = false;
        try
        {
            string sqlstr = "";
            DataTable dt = new DataTable();

            if (CheckInputValue())
            {
                if (this.rb_1.Checked)
                {
                    #region 人事編號
                    sqlstr = "select peo_uid,peo_name from people where peo_workid='" + this.txt_workid.Text + "'";
                    dt.Clear();
                    dt = dbo.ExecuteQuery(sqlstr);
                    if (dt.Rows.Count > 0)
                    {
                        string InsStr = "insert into c04 (peo_uid,c04_right,c04_createuid,c04_createtime) values(" + dt.Rows[0]["peo_uid"].ToString() + ",'" + this.rbl_right.SelectedValue + "'," + sobj.sessionUserID + ",getdate())";
                        dbo.ExecuteNonQuery(InsStr);

                        //登入記錄(功能編號,人員編號,操作代碼[1新增 2查詢 3更新 4刪除 5保留],備註)
                        new OperatesObject().ExecuteOperates(100303, sobj.sessionUserID, 1, "新增 " + dt.Rows[0]["peo_name"].ToString() + "可查看：" + this.rbl_right.SelectedItem.Text);

                        isSuccess = true;
                    }
                    else
                    {
                        ShowMSG("查無此人事編號");
                    }
                    #endregion
                }
                else if (this.rb_2.Checked)
                {
                    #region 人員清單
                    for (int i = 0; i < this.DepartTreeListBox1.Items.Count; i++)
                    {
                        string InsStr = "insert into c04 (peo_uid,c04_right,c04_createuid,c04_createtime) values(" + this.DepartTreeListBox1.Items[i].Key + ",'" + this.rbl_right.SelectedValue + "'," + sobj.sessionUserID + ",getdate())";
                        dbo.ExecuteNonQuery(InsStr);

                        //登入記錄(功能編號,人員編號,操作代碼[1新增 2查詢 3更新 4刪除 5保留],備註)
                        new OperatesObject().ExecuteOperates(100303, sobj.sessionUserID, 1, "新增 " + this.DepartTreeListBox1.Items[i].Value + "可查看：" + this.rbl_right.SelectedItem.Text);
                    }
                    isSuccess = true;
                    #endregion
                }
                else
                {
                    #region 部門清單
                    sqlstr = "select peo_uid,peo_name from people where dep_no in (" + keyvalue + ") and peo_jobtype=" + PCalendarUtil.GetPeoJobtype();
                    dt.Clear();
                    dt = dbo.ExecuteQuery(sqlstr);
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            if (new C04DAO().GetNoByPeoUid(Convert.ToInt32(dt.Rows[i]["peo_uid"].ToString()))==0)
                            {
                                string InsStr = "insert into c04 (peo_uid,c04_right,c04_createuid,c04_createtime) values(" + dt.Rows[i]["peo_uid"].ToString() + ",'" + this.rbl_right.SelectedValue + "'," + sobj.sessionUserID + ",getdate())";
                                dbo.ExecuteNonQuery(InsStr);

                                //登入記錄(功能編號,人員編號,操作代碼[1新增 2查詢 3更新 4刪除 5保留],備註)
                                new OperatesObject().ExecuteOperates(100303, sobj.sessionUserID, 1, "新增 " + dt.Rows[i]["peo_name"].ToString() + "可查看：" + this.rbl_right.SelectedItem.Text);
                            }
                        }
                    }
                    isSuccess = true;
                    #endregion
                }
            }
        }
        catch (Exception ex)
        {
            aMSG = "功能名稱:新增開放人員--確定<br>錯誤訊息:" + ex.ToString();
            Response.Write(aMSG);
        }
        if (isSuccess)
        {
            this.Server.Transfer("100303.aspx");
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