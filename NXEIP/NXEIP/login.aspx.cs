using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.SessionState;
using NXEIP.DAO;
using Entity;
using NXEIP.Lib;

public partial class login : SessionObject
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            this.ImageButton1.Attributes.Add("onmouseout", "MM_swapImgRestore()");
            this.ImageButton1.Attributes.Add("onmouseover", "MM_swapImage('ImageButton1','','image/login-06-1.gif',1)");

            this.ImageButton2.Attributes.Add("onmouseout", "MM_swapImgRestore()");
            this.ImageButton2.Attributes.Add("onmouseover", "MM_swapImage('ImageButton2','','image/login-07-1.gif',1)");

            this.Image1.Attributes.Add("onmouseout", "MM_swapImgRestore()");
            this.Image1.Attributes.Add("onmouseover", "MM_swapImage('Image1','','image/login_ID.jpg',1)");

            CacheUtil.Clear();
        }
    }

    
    /// <summary>
    /// 使用者登入
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        this.CheckUI();
    }

    private void CheckUI()
    {
        if (this.CheckCode())
        {
            if (!string.IsNullOrEmpty(this.tbox_id.Text) && !string.IsNullOrEmpty(this.tbox_pw.Text))
            {
                try
                {
                    accounts accData = new AccountsDAO().GetByIDPW(this.tbox_id.Text, this.tbox_pw.Text);

                    if (accData != null)
                    {
                        if (accData.acc_status.Equals("1"))
                        {
                            //取回人員資料
                            people peoData = new PeopleDAO().GetByPeoUID(accData.peo_uid);

                            string loginID = Guid.NewGuid().ToString("N");

                            //save session data
                            this.sessionUserID = peoData.peo_uid.ToString();
                            this.sessionUserName = peoData.peo_name;
                            this.sessionUserDepartID = peoData.dep_no.ToString();
                            this.sessionUserDepartName = new UtilityDAO().Get_DepartmentName(peoData.dep_no);
                            this.sessionUserAccount = accData.acc_login;
                            this.sessionLogInID = loginID;
                            

                            //login log
                            new OperatesObject().ExecuteLogInLog(loginID,peoData.peo_uid, accData.acc_no, this.GetIpAddress(), this.SessionID);

                            //goto index
                            Server.Transfer("Default.aspx");
                        }

                        //未啟用
                        if (accData.acc_status.Equals("2"))
                        {

                            this.ShowMessage("您的帳號尚未啟用，請洽貴單位系統管理員：，公務電話：");
                        }

                        //已停用
                        if (accData.acc_status.Equals("3"))
                        {

                            this.ShowMessage("您的帳號已被停用，請洽貴單位系統管理員：，公務電話：");
                        }
                        
                    }
                    else
                    {
                        this.ShowMessage("帳號密碼錯誤!");
                    }
                }
                catch(System.Exception ex)
                {
                    this.ShowMessage("帳號或密碼錯誤!");
                }
            }
            else
            {
                this.ShowMessage("請輸入帳號密碼!");
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

}
