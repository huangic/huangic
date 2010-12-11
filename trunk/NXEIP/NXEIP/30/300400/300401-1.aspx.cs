using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

/// <summary>
/// 功能名稱：管理作業 / 場地管理 / 所在地管理--新增、修改
/// 功能編號：30/300400/300401
/// 撰寫者：Lina
/// 撰寫時間：2010/09/17
/// </summary>
public partial class _30_300400_300401_1 : System.Web.UI.Page
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

            if (this.lab_mode.Text.Equals("modify"))
            {
                this.Navigator1.SubFunc = "修改";
                string sqlstr = "select spo_name,spo_function from spot where spo_no=" + this.lab_no.Text;
                DataTable dt = new DataTable();
                dt = dbo.ExecuteQuery(sqlstr);
                if (dt.Rows.Count > 0)
                {
                    this.txt_name.Text = dt.Rows[0]["spo_name"].ToString();
                    if (dt.Rows[0]["spo_function"].ToString().Length > 0)
                    {
                        char[] fun = dt.Rows[0]["spo_function"].ToString().ToArray();

                        for (int i = 0; i < this.cbl_function.Items.Count; i++)
                        {
                            if (fun[i].Equals(Convert.ToChar("1"))) this.cbl_function.Items[i].Selected = true;
                        }
                    }
                }
            }
            else
            {
                this.Navigator1.SubFunc = "新增";
            }
        }
    }

    private bool CheckInputValue()
    {
        #region 輸入值檢查--所在地
        if (string.IsNullOrEmpty(this.txt_name.Text))
        {
            ShowMSG("請輸入 所在地");
            return false;
        }
        else if (!checkobj.IsValidLen(this.txt_name.Text.Trim(), 20))
        {
            ShowMSG("所在地 長度不可超過20個數文字");
            return false;
        }
        #endregion

        #region 檢查是否重複
        DataTable dt = new DataTable();
        if (this.lab_mode.Text.Equals("modify"))
        {
            string sqlstr = "select spo_no from spot where spo_name=N'" + this.txt_name.Text + "' and spo_no<>" + this.lab_no.Text + " and spo_status='1'";
            dt = dbo.ExecuteQuery(sqlstr);
            if (dt.Rows.Count > 0)
            {
                ShowMSG("此 所在地[" + this.txt_name.Text + "] 已存在");
                return false;
            }
        }
        else
        {
            string sqlstr = "select spo_no from spot where spo_name=N'" + this.txt_name.Text + "' and spo_status='1'";
            dt = dbo.ExecuteQuery(sqlstr);
            if (dt.Rows.Count > 0)
            {
                ShowMSG("此 所在地[" + this.txt_name.Text + "] 已存在");
                return false;
            }
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
                string spo_function = "";
                for (int i = 0; i < this.cbl_function.Items.Count; i++)
                {
                    if (this.cbl_function.Items[i].Selected)
                        spo_function += "1";
                    else
                        spo_function += "0";
                }
                for (int i = 0; i < (20 - this.cbl_function.Items.Count); i++)
                {
                    spo_function += "0";
                }
                
                if (this.lab_mode.Text.Equals("modify"))
                {
                    #region 修改
                    string UpdStr = "update spot set spo_name=N'" + this.txt_name.Text + "',spo_function='" + spo_function + "',spo_createuid=" + sobj.sessionUserID + ",spo_createtime=getdate() where spo_no=" + this.lab_no.Text;
                    dbo.ExecuteNonQuery(UpdStr);
                    msg = "修改成功";
                    //登入記錄(功能編號,人員編號,操作代碼[1新增 2查詢 3更新 4刪除 5保留],備註)
                    new OperatesObject().ExecuteOperates(300401, sobj.sessionUserID, 3, "編號：" + this.lab_no.Text + ",所在地：" + this.txt_name.Text.Trim());
                    #endregion
                }
                else
                {
                    #region 新增
                    string InsStr = "insert into spot (spo_name,spo_status,spo_createuid,spo_createtime,spo_function) values(N'" + this.txt_name.Text + "','1'," + sobj.sessionUserID + ",getdate(),'" + spo_function + "')";
                    dbo.ExecuteNonQuery(InsStr);
                    msg = "新增成功";
                    //登入記錄(功能編號,人員編號,操作代碼[1新增 2查詢 3更新 4刪除 5保留],備註)
                    new OperatesObject().ExecuteOperates(300401, sobj.sessionUserID, 1, "所在地：" + this.txt_name.Text.Trim());
                    #endregion                    
                }

                this.Page.ClientScript.RegisterStartupScript(typeof(_30_300400_300401_1), "closeThickBox", "self.parent.update('" + msg + "');", true);
            }
        }
        catch (Exception ex)
        {
            aMSG = "功能名稱：所在地-"+this.Navigator1.SubFunc+"<br>錯誤訊息:" + ex.ToString();
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