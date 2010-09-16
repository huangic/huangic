using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NXEIP.DAO;
using Entity;

public partial class _10_100100_100104 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
    }

    protected void btn_ok_Click(object sender, EventArgs e)
    {
        string cardDN = this.cardDN.Value;

        PeopleDAO peoDao = new PeopleDAO();
        people peoData = peoDao.GetByPeoUID(Convert.ToInt32(new SessionObject().sessionUserID));

        if (peoData != null && cardDN.Length > 0)
        {
            peoData.peo_pincode = cardDN;
            peoDao.Update();

            this.ShowMessage("註冊完成!!");
            this.cardDN.Value = "";
            this.tbox_pin.Text = "";
        }
        else
        {
            this.ShowMessage("註冊失敗，請重新登入系統!!");
        }
    }

    private void ShowMessage(string msg)
    {
        this.ClientScript.RegisterStartupScript(this.GetType(), "MyScript", "<script>alert('" + msg + "');</script>");
    }
}