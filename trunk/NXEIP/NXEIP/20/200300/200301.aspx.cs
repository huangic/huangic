using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Specialized;
using AjaxControlToolkit;
using NXEIP.DAO;
using System.Data.Objects.SqlClient;
using Entity;
using NLog;
using System.Data;
using System.Web.UI.HtmlControls;

public partial class _20_200300_200301 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            Sys06DAO dao = new Sys06DAO();
            sys06 d = dao.GetS06FromSufNO(200301).First();
            this.hidden_1.Value = d.s06_no + "";
            this.ShowFood();

        }
    }

    /// <summary>
    /// 查詢
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btn_ok_Click(object sender, EventArgs e)
    {
        if (this.tbox_name.Text.Trim().Length > 0)
        {
            FoodsDAO dao = new FoodsDAO();
            var data = dao.GetData_Search(this.tbox_name.Text);
            if (data.Count() > 0)
            {
                foreach (var d in data)
                {
                    this.GenDiv(d);
                }
            }
            else
            {
                JsUtil.AlertJs(this, "查無資料!");
            }
        }
        else
        {
            JsUtil.AlertJs(this, "請輸入商店關鍵字");
        }
    }

    private void ShowFood()
    {
        FoodsDAO dao = new FoodsDAO();
        var data = dao.GetS06No_Data(int.Parse(this.hidden_1.Value));
        if (data.Count() > 0)
        {
            foreach (var d in data)
            {
                this.GenDiv(d);
            }
        }
        else
        {
            JsUtil.AlertJs(this, "查無資料!");
        }
    }

    private void GenDiv(foods d)
    {
        //<div class="box">
        //       <div class="head">維也納餐廳-素食</div>
        //          <div class="content">
        //            <div class="b1"></div>
        //               <div class="b2">
        //            <a class="a-letter-t1" href="#">素食-火鍋-簡餐</a> </div>
        //            <div class="b3"><a class="a-letter-t2" href="#">(2986088 府前路-後門 2010-10-12)</a></div>
        //       </div>
        //     </div>

        HtmlGenericControl DivBox = new HtmlGenericControl("div");
        HtmlGenericControl DivHead = new HtmlGenericControl("div");
        HtmlGenericControl DivContent = new HtmlGenericControl("div");
        HtmlGenericControl DivB1 = new HtmlGenericControl("div");
        HtmlGenericControl DivB2 = new HtmlGenericControl("div");
        HtmlGenericControl DivB3 = new HtmlGenericControl("div");

        DivBox.Controls.Add(DivHead);
        DivBox.Controls.Add(DivContent);
        DivContent.Controls.Add(DivB1);
        DivContent.Controls.Add(DivB2);
        DivContent.Controls.Add(DivB3);

        DivBox.Attributes["class"] = "box";
        DivHead.Attributes["class"] = "head";
        DivContent.Attributes["class"] = "content";
        DivB1.Attributes["class"] = "b1";
        DivB2.Attributes["class"] = "b2 a-letter-t1";
        DivB3.Attributes["class"] = "b3 a-letter-t2";

        DivHead.InnerText = d.foo_name;

        DivB2.InnerText = d.foo_descript;

        DivB3.InnerHtml = "電話：" + d.foo_tel; 
        DivB3.InnerHtml += " 位置：" + d.foo_area;
        DivB3.InnerHtml += " 網址：" + d.foo_www;

        this.div_foods.Controls.Add(DivBox);
    }

    protected void lv_food_ItemCommand(object sender, ListViewCommandEventArgs e)
    {
        if (e.CommandName == "click")
        {
            int index = Convert.ToInt32(e.Item.DataItemIndex);

            this.hidden_1.Value = this.lv_food.DataKeys[index].Value.ToString();

            this.ShowFood();

            this.lv_food.DataBind();
        }
    }


    protected void Button1_Click(object sender, EventArgs e)
    {
        Response.Redirect("200301-1.aspx");
    }
}