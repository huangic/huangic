using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using NXEIP.DAO;
using Entity;

public partial class _10_100300_100302_1 : System.Web.UI.Page
{
    SessionObject sobj = new SessionObject();
    DBObject dbo = new DBObject();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            //登入記錄(功能編號,人員編號,操作代碼[1新增 2查詢 3更新 4刪除 5保留],備註)
            new OperatesObject().ExecuteOperates(100302, sobj.sessionUserID, 2, "新增開放人員");
            if (this.DepartmentPanel1.Items != null) this.DepartmentPanel1.Items.Clear();
        }
    }
    #region 確定
    protected void btn_submit_Click(object sender, EventArgs e)
    {
        string aMSG = "";   //記錄錯誤訊息
        try
        {
            if (CheckInputValue())
            {
                for (int i = 0; i < this.DepartmentPanel1.Items.Count; i++)
                {
                    if (!this.DepartmentPanel1.Items[i].Key.Equals(sobj.sessionUserID))
                    {
                        string InsStr = "insert into c01 (peo_uid,c01_peouid,c01_createtime) values(" + sobj.sessionUserID + "," + this.DepartmentPanel1.Items[i].Key + ",getdate())";
                        dbo.ExecuteNonQuery(InsStr);

                        //登入記錄(功能編號,人員編號,操作代碼[1新增 2查詢 3更新 4刪除 5保留],備註)
                        new OperatesObject().ExecuteOperates(100302, sobj.sessionUserID, 1, "新增開放人員");
                    }
                }
                this.Page.ClientScript.RegisterStartupScript(typeof(_10_100300_100302_1), "closeThickBox", "self.parent.update('新增成功');self.parent.location.reload(true);self.parent.tb_remove();", true);
            }
        }
        catch (Exception ex)
        {
            aMSG = "功能名稱：開放人員-" + this.Navigator1.SubFunc + "<br>錯誤訊息:" + ex.ToString();
            Response.Write(aMSG);
        }
    }
    #endregion

    #region 檢查輸入值
    private bool CheckInputValue()
    {
        if (this.DepartmentPanel1.Items.Count <= 0 || this.DepartmentPanel1.Items == null)
        {
            ShowMSG("請選擇人員");
            return false;
        }
        else
        {
            #region 檢查是否已新增
            for (int i = 0; i < this.DepartmentPanel1.Items.Count; i++)
            {
                int icount = new C01DAO().GetCountByC01PeoUid(Convert.ToInt32(sobj.sessionUserID), Convert.ToInt32(this.DepartmentPanel1.Items[i].Key));
                if (icount > 0)
                {
                    ShowMSG(this.DepartmentPanel1.Items[i].Value + " 已在名單中");
                    return false;
                }
            }
            #endregion
        }
        return true;

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