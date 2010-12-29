using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NXEIP.DAO;
using Entity;

public partial class login2 : SessionObject
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            this.ImageButton1.Attributes.Add("onmouseout", "MM_swapImgRestore()");
            this.ImageButton1.Attributes.Add("onmouseover", "MM_swapImage('ImageButton1','','image/login-06-1.gif',1)");

            this.Image1.Attributes.Add("onmouseout", "MM_swapImgRestore()");
            this.Image1.Attributes.Add("onmouseover", "MM_swapImage('Image1','','image/login_PIN.jpg',1)");
        }
    }

    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        this.CheckUI();
    }

    private void CheckUI()
    {
        if (this.CheckCode())
        {
            if (this.cardDN.Value.Length > 0)
            {
                try
                {
                    people peoData = new PeopleDAO().GetByPeoCardDN(this.cardDN.Value);

                    if (peoData != null)
                    {
                        accounts accData = new AccountsDAO().GetByPeoUID(peoData.peo_uid);

                        string loginID = Guid.NewGuid().ToString("N");
                        
                        //save session data
                        this.sessionUserID = peoData.peo_uid.ToString();
                        this.sessionUserName = peoData.peo_name;
                        this.sessionUserDepartID = peoData.dep_no.ToString();
                        this.sessionUserDepartName = peoData.departments.dep_name;
                        this.sessionUserAccount = accData.acc_login;
                        this.sessionLogInID = loginID;

                        //login log
                        new OperatesObject().ExecuteLogInLog(loginID,peoData.peo_uid, accData.acc_no, this.GetIpAddress(), this.SessionID);
                        
                        //goto index
                        Server.Transfer("Default.aspx");
                    }
                    else
                    {
                        this.ShowMessage("系統查無此憑證資料，請使用帳號/密碼登入，並進行自然人憑證註冊!!");
                    }
                }
                catch
                {
                    this.ShowMessage("請重新驗證!");
                }
            }
            else
            {
                this.ShowMessage("請重新驗證!");
            }
        }

    }

    /// <summary>
    /// 檢查驗證碼
    /// </summary>
    /// <returns></returns>
    private bool CheckCode()
    {
        bool ret = false;

        if (Session["CheckCode"] != null)
        {

            DateTime checkDateTime = Convert.ToDateTime(Session["CheckCode_DateTime"].ToString()).AddMinutes(Convert.ToDouble(Session["CheckCode_limit"]));

            if (checkDateTime > System.DateTime.Now)
            {
                if (String.Compare(Session["CheckCode"].ToString(), this.tbox_code.Text, true) == 0)
                {
                    ret = true;
                }
                else
                {
                    this.ShowMessage("驗證碼輸入錯誤!");
                }
            }
            else
            {
                this.ShowMessage("驗證碼過期!");
            }
        }
        else
        {
            this.ShowMessage("驗證碼過期!");
        }

        if (!ret)
        {
            this.img1.Src = "lib/ValidateCode.ashx?ran=" + new Random().Next(1000);
        }

        this.tbox_code.Text = "";

        return ret;
    }

    private new void ShowMessage(string msg)
    {
        this.ClientScript.RegisterStartupScript(this.GetType(), "MyScript", "<script>alert('" + msg + "');</script>");
    }
    
}