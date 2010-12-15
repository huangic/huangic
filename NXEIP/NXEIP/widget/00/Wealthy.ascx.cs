using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

public partial class widget_00_Wealthy : NXEIP.Widget.WidgetBaseControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string idcard = new UtilityDAO().Get_PeopleIDCard(int.Parse(new SessionObject().sessionUserID));

        //差勤連結路徑
        string path = ConfigurationManager.AppSettings["Wealthy_SSO_Path"] + "?mode=2&val=" + idcard;

        //簽核資料WebService
        using (Wealthy_WebReference.PeopleSign sign = new Wealthy_WebReference.PeopleSign())
        {

            //取得WebService路徑
            string peopleSign_path = ConfigurationManager.AppSettings["Wealthy_WebReference.PeopleSign"];

            //變更WebService路徑
            sign.Url = peopleSign_path;

            //重新整理WebService
            sign.Discover();

            //假單簽核資料 待簽核-代理簽核-送審文件 0-0-0
            string[] signCount = sign.Get_SignRecord_Count1(idcard).Split('-');

            this.HyperLink1.NavigateUrl = path;
            this.HyperLink1.Text = "待簽核文件數:" + signCount[0];

            this.HyperLink2.NavigateUrl = path;
            this.HyperLink2.Text = "代理簽核文件數:" + signCount[1];


        }

        //今日代理人件數WebService
        using (Wealthy2_WebReference.peopleAgent peoAgent = new Wealthy2_WebReference.peopleAgent())
        {
            string peoAgent_path = ConfigurationManager.AppSettings["Wealthy2_WebReference.peopleAgent"];
            peoAgent.Url = peoAgent_path;
            peoAgent.Discover();
        


        string peoAgent_Count = peoAgent.get_PeopleAgentCount("2", idcard).ToString();
        this.HyperLink3.NavigateUrl = path;
        this.HyperLink3.Text = "今日代理人件數:" + peoAgent_Count;
        }

        //本日刷卡紀錄WebService
        using (Wealthy3_WebReference.CardLog cardlog = new Wealthy3_WebReference.CardLog())
        {
            string cardlog_path = ConfigurationManager.AppSettings["Wealthy3_WebReference.CardLog"];
            cardlog.Url = cardlog_path;
            cardlog.Discover();

            string cardlog_count = cardlog.getCardLogCount("2", idcard).ToString();
            this.HyperLink4.NavigateUrl = path;
            this.HyperLink4.Text = "本日刷卡紀錄:" + cardlog_count;

        }

    }

    public override string Name
    {
        get { return "wealthy"; }
    }

    public override void loadWidget()
    {


    }
}