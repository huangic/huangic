using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NXEIP.DAO;
using Entity;

public partial class _10_100100_100103 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!Page.IsPostBack)
        {
            //init DataSource


            this.hidden_mode.Value = "1";

            this.Search();



            //如果有PAGETYPE 
            if (!String.IsNullOrEmpty(Request["PageType"]))
            {
                String Command = Request["PageType"];
                if (Command == "D")
                {
                    InitButtonCss(this.Button2);                    
                    hidden_mode.Value = "2";
                    this.Control.Visible = false;
                    this.Search();
                }

                if (Command == "U")
                {
                    InitButtonCss(this.Button3); 
                    
                    hidden_mode.Value = "3";
                    this.Control.Visible = false;
                    this.Search();
                }

            }


        }



        if (Request["__EVENTTARGET"] == this.UpdatePanel1.ClientID && String.IsNullOrEmpty(Request["__EVENTARGUMENT"]))
        {
            this.ListView1.DataBind();
        }

    }


    protected void Button_Click(object sender, EventArgs e)
    {
        Button button = sender as Button;
        String Command = button.CommandName;

        InitButtonCss(button);

        if (Command == "peo")
        {
            hidden_mode.Value = "1";
            this.Control.Visible = true;
            this.Search();
        }

        if (Command == "dep")
        {
            hidden_mode.Value = "2";
            this.Control.Visible = false;
            this.Search();
        }

        if (Command == "all")
        {
            hidden_mode.Value = "3";
            this.Control.Visible = false;
            this.Search();
        }

        //if()
    }



    private void Search()
    {

        //init DataSource

        SessionObject sessoionObj = new SessionObject();

        this.ObjectDataSource1.SelectParameters[0].DefaultValue = sessoionObj.sessionUserID;

        this.ObjectDataSource1.SelectParameters[1].DefaultValue = sessoionObj.sessionUserDepartID;

        this.ObjectDataSource1.SelectParameters[2].DefaultValue = this.hidden_mode.Value;


        this.ListView1.DataBind();


        //計算幾本相簿
        _100103DAO dao = new _100103DAO();

        int count = dao.GetPeopleAlbumCount(int.Parse(sessoionObj.sessionUserID), int.Parse(sessoionObj.sessionUserDepartID), this.hidden_mode.Value);

        this.lit_album_count.Text = count + "";
    }


    /// <summary>
    /// 初始化與設定按鈕的CSS屬性
    /// </summary>
    /// <param name="b"></param>
    private void InitButtonCss(Button b)
    {
        this.Button1.CssClass = "a-input";
        this.Button2.CssClass = "a-input";
        this.Button3.CssClass = "a-input";

        b.CssClass = "b-input2";
    }


    protected bool CheckPermission(object a)
    {
        //如果是建立人救回傳TRUE
        int peo_uid = int.Parse(new SessionObject().sessionUserID);

        album alb = a as album;

        if (alb.peo_uid == peo_uid)
        {
            return true;
        }


        return false;

    }
    protected void btn_del_Click(object sender, EventArgs e)
    {
        //刪除相簿

        //取有打勾的

        //this.ListView1.Items
        using (NXEIPEntities model = new NXEIPEntities())
        {

            foreach (var item in this.ListView1.Items)
            {
                var o = this.ListView1.DataKeys[item.DataItemIndex].Value;

                CheckBox cb = (CheckBox)item.FindControl("CheckBox1");
                if (cb.Checked)
                {
                    album a = (album)o;

                    model.album.Attach(a);
                    a.alb_status = "4";

                }

            }

            model.SaveChanges();
        }

        this.ListView1.DataBind();
    }
}