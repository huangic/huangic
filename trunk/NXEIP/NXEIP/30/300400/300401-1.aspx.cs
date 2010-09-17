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
            if (Request["pageIndex"] != null) this.lab_pageIndex.Text = Request["pageIndex"];
            if (Request["mode"] != null) this.lab_mode.Text = Request["mode"];
            if (Request["no"] != null) this.lab_no.Text = Request["no"];

            if (this.lab_mode.Text.Equals("modify"))
            {
                this.Navigator1.SubFunc = "所在地-修改";
                string sqlstr = "select spo_name from spot where spo_no="+this.lab_no.Text;
                DataTable dt = new DataTable();
                dt = dbo.ExecuteQuery(sqlstr);
                if (dt.Rows.Count > 0)
                {
                    this.txt_name.Text = dt.Rows[0]["spo_name"].ToString();
                }
            }
            else
            {
                this.Navigator1.SubFunc = "所在地-新增";
            }
        }
    }

    #region 顯示回應訊息
    private void ShowMsg(string msg)
    {
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "msg", "alert('" + msg + "');", true);
    }
    #endregion

    #region 確定
    protected void btn_submit_Click(object sender, EventArgs e)
    {
        string aMSG = "";
        try
        {
            #region 輸入值檢查--所在地
            if (string.IsNullOrEmpty(this.txt_name.Text.Trim()))
            {
                this.ShowMsg("請輸入 所在地");
                return;
            }
            else if (!checkobj.IsValidLen(this.txt_name.Text.Trim(), 60))
            {
                this.ShowMsg("所在地 長度不可超過20個數文字");
                return;
            }
            #endregion

            if (this.lab_mode.Text.Equals("modify"))
            {
                #region 修改
                string sqlstr = "select spo_no from spot where spo_name=N'"+this.txt_name.Text+"' and spo_no<>"+this.lab_no.Text;
                DataTable dt = new DataTable();
                dt = dbo.ExecuteQuery(sqlstr);
                if (dt.Rows.Count > 0)
                {
                    this.ShowMsg("此 所在地 已存在");
                    return;
                }
                else
                {
                    string UpdStr = "update spot set spo_name=N'" + this.txt_name.Text + "',spo_createuid=" + sobj.sessionUserID + ",spo_createtime=getdate() where spo_no=" + this.lab_no.Text;
                    dbo.ExecuteNonQuery(UpdStr);
                }
                #endregion

                //登入記錄(功能編號,人員編號,操作代碼[1新增 2查詢 3更新 4刪除 5保留],備註)
                new OperatesObject().ExecuteOperates(300401, sobj.sessionUserID, 3, "編號：" + this.lab_no.Text+",所在地："+this.txt_name.Text.Trim());
            }
            else
            {
                #region 新增
                string sqlstr = "select spo_no from spot where spo_name=N'" + this.txt_name.Text + "'";
                DataTable dt = new DataTable();
                dt = dbo.ExecuteQuery(sqlstr);
                if (dt.Rows.Count > 0)
                {
                    this.ShowMsg("此 所在地 已存在");
                    return;
                }
                else
                {
                    string InsStr = "insert into spot (spo_name,spo_status,spo_createuid,spo_createtime) values(N'"+this.txt_name.Text+"','1',"+sobj.sessionUserID+",getdate())";
                    dbo.ExecuteNonQuery(InsStr);
                }
                #endregion

                //登入記錄(功能編號,人員編號,操作代碼[1新增 2查詢 3更新 4刪除 5保留],備註)
                new OperatesObject().ExecuteOperates(300401, sobj.sessionUserID, 1, "所在地：" + this.txt_name.Text.Trim());
            }

            Response.Write(PCalendarUtil.ShowMsg_URL("", "300401.aspx?pageIndex=" + this.lab_pageIndex.Text + "&count=" + new System.Random().Next(10000).ToString()));
        }
        catch (Exception ex)
        {
            aMSG = "功能名稱："+this.Navigator1.SubFunc+"<br>錯誤訊息:" + ex.ToString();
            Response.Write(aMSG);
        }
    }
    #endregion

    #region 取消
    protected void btn_cancel_Click(object sender, EventArgs e)
    {
        Response.Write(PCalendarUtil.ShowMsg_URL("", "300401.aspx?pageIndex=" + this.lab_pageIndex.Text + "&count=" + new System.Random().Next(10000).ToString()));
    }
    #endregion
}