using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;
using NXEIP.DAO;
using Entity;
using RssToolkit.Web.WebControls;

public partial class _10_100200_100203 : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            this.ObjectDataSource1.SelectParameters["peo_uid"].DefaultValue = new SessionObject().sessionUserID;
            this.GridView1.DataBind();
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {
            //取得網址資料
            string url = this.tbox_url.Text.Trim();
            XDocument xd = XDocument.Load(url);

            //取得標題
            string title = xd.XPathSelectElement("/rss/channel/title").Value;

            int order = 1;
            try
            {
                order = Convert.ToInt32(this.tbox_order.Text);
            }
            catch { }

            int peo_uid = int.Parse(new SessionObject().sessionUserID);
            RssDAO dao = new RssDAO();

            rss d = new rss();

            d.peo_uid = peo_uid;
            d.rss_createtime = DateTime.Now;
            d.rss_address = this.tbox_url.Text.Trim();
            d.rss_createuid = peo_uid;
            d.rss_status = "1";
            d.rss_subject = title;
            d.rss_order = order;
            d.rss_no = dao.Get_MAXRssNO(peo_uid) + 1;

            dao.AddToRss(d);
            dao.Update();

            this.GridView1.DataBind();

            this.tbox_order.Text = "";
            this.tbox_url.Text = "";
        }
        catch
        {
            JsUtil.AlertJs(this, "請輸入正確網址與排序!!");
        }
    }


    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        string url = ((LinkButton)sender).CommandArgument;
        this.LoadRss(url);
    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        
        if (e.CommandName.Equals("del"))
        {
            int peo_uid = int.Parse(new SessionObject().sessionUserID);
            int rowIndex = int.Parse(e.CommandArgument.ToString());
            int rss_no = int.Parse(this.GridView1.DataKeys[rowIndex].Value.ToString());

            RssDAO dao = new RssDAO();

            rss d = dao.Get_Rss(peo_uid, rss_no);
            d.rss_status = "2";
            dao.Update();

            OperatesObject.OperatesExecute(100203, 4, "刪除Rss訂閱 rss_no:" + rss_no + " peo_uid:" + peo_uid);
            this.GridView1.DataBind();

        }
    }

    private void LoadRss(string url)
    {
        RssDataSource rssDS = new RssDataSource();
        rssDS.Url = url;
        this.GridView2.DataSource = rssDS;
        this.GridView2.DataBind();
    }

    protected void GridView1_DataBound(object sender, EventArgs e)
    {
        int peo_uid = int.Parse(new SessionObject().sessionUserID);
        RssDAO dao = new RssDAO();

        if (dao.Get_RssDataCount(peo_uid) > 0)
        {
            rss data = dao.Get_RssData(peo_uid).FirstOrDefault();
            this.LoadRss(data.rss_address);
        }
        else
        {
            this.GridView2.DataSource = null;
            this.GridView2.DataBind();
        }
        
    }
}