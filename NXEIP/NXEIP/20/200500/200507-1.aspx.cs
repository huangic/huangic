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
/// 功能名稱：全府應用 / 業務資訊 / 相關網站--新增、修改
/// 功能編號：20/200500/200507
/// 撰寫者：Lina
/// 撰寫時間：2011/03/17
/// </summary>
public partial class _20_200500_200507_1 : System.Web.UI.Page
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

            string sqlstr = "select s06_no, s06_name from sys06 where (sfu_no = 200507) and (s06_status = '1') order by s06_order";
            DataTable dt = new DataTable();
            dt = dbo.ExecuteQuery(sqlstr);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    this.ddl_sys06.Items.Add(new ListItem(dt.Rows[i]["s06_name"].ToString(), dt.Rows[i]["s06_no"].ToString()));
                }
            }
            this.ddl_sys06.Items.Insert(0, new ListItem("請選擇", "0"));

            if (this.lab_mode.Text.Equals("modify"))
            {
                this.Navigator1.SubFunc = "修改";
                sqlstr = "select com_name,com_www,com_sys06no from commend where com_no=" + this.lab_no.Text;
                dt.Clear();
                dt = dbo.ExecuteQuery(sqlstr);
                if (dt.Rows.Count > 0)
                {
                    this.txt_name.Text = dt.Rows[0]["com_name"].ToString();
                    this.txt_www.Text = dt.Rows[0]["com_www"].ToString();
                    try
                    {
                        this.ddl_sys06.Items.FindByValue(dt.Rows[0]["com_sys06no"].ToString()).Selected = true;
                    }
                    catch { }
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
        #region 輸入值檢查--分類名稱
        if (this.ddl_sys06.SelectedValue.Equals("0"))
        {
            ShowMSG("請選擇 分類名稱");
            return false;
        }
        #endregion
        #region 輸入值檢查--網站名稱
        if (string.IsNullOrEmpty(this.txt_name.Text.Trim()))
        {
            ShowMSG("請輸入 網站名稱");
            return false;
        }
        else if (!checkobj.IsValidLen(this.txt_name.Text.Trim(), 30))
        {
            ShowMSG("網站名稱 長度不可超過30個中文字");
            return false;
        }
        #endregion
        #region 輸入值檢查--連結網址
        if (string.IsNullOrEmpty(this.txt_www.Text.Trim()))
        {
            ShowMSG("請輸入 連結網址");
            return false;
        }
        else if (!checkobj.IsValidLen(this.txt_www.Text.Trim(), 200))
        {
            ShowMSG("連結網址 長度不可超過200個數文字");
            return false;
        }
        else
        {
            if (txt_www.Text.Trim().IndexOf("http://", 0) < 0)
            {
                if (this.txt_www.Text.Trim().IndexOf("//", 0) == 0)
                {
                    txt_www.Text = "http:" + txt_www.Text;
                }
                else
                {
                    txt_www.Text = "http://" + txt_www.Text;
                }
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
                int logfun=1;
                string logmemo="";
                CommendDAO tbDAO=new CommendDAO();
                if (this.lab_mode.Text.Equals("modify"))
                {
                    commend newRow = tbDAO.GetByNo(Convert.ToInt32(this.lab_no.Text));
                    newRow.com_createtime = System.DateTime.Now;
                    newRow.com_name = this.txt_name.Text;
                    newRow.com_sys06no = Convert.ToInt32(this.ddl_sys06.SelectedValue);
                    newRow.com_www = this.txt_www.Text;
                    tbDAO.Update();
                    logfun=3;
                    logmemo="編號：" + this.lab_no.Text + ",網站名稱：" + this.txt_name.Text.Trim();
                    msg = "修改成功";
                    
                }
                else
                {
                    commend newRow = new commend();
                    newRow.com_createuid=Convert.ToInt32(sobj.sessionUserID);
                    newRow.com_status = "1";
                    newRow.com_createtime = System.DateTime.Now;
                    newRow.com_name = this.txt_name.Text;
                    newRow.com_sys06no = Convert.ToInt32(this.ddl_sys06.SelectedValue);
                    newRow.com_www = this.txt_www.Text;
                    tbDAO.AddCommend(newRow);
                    tbDAO.Update();
                    logfun=1;
                    logmemo = "網站名稱：" + this.txt_name.Text.Trim();
                    msg = "新增成功";
                }

                //登入記錄(功能編號,人員編號,操作代碼[1新增 2查詢 3更新 4刪除 5保留],備註)
                new OperatesObject().ExecuteOperates(300401, sobj.sessionUserID, logfun, logmemo);
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "closeThickBox", "self.parent.update('" + msg + "');", true);
            }
        }
        catch (Exception ex)
        {
            aMSG = "功能名稱：相關網站-" + this.Navigator1.SubFunc + "<br>錯誤訊息:" + ex.ToString();
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