using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NXEIP.DAO;

public partial class _10_100100_100103 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!Page.IsPostBack) { 
            //init DataSource

            SessionObject sessoionObj = new SessionObject();

            this.ObjectDataSource1.SelectParameters[0].DefaultValue = sessoionObj.sessionUserID;

            this.ObjectDataSource1.SelectParameters[1].DefaultValue = sessoionObj.sessionUserDepartID;

            this.ObjectDataSource1.SelectParameters[2].DefaultValue = "1";


            this.ListView1.DataBind();

            //計算幾本相簿
            _100103DAO dao=new _100103DAO();

            int count = dao.GetPeopleAlbumCount(int.Parse(sessoionObj.sessionUserID),int.Parse(sessoionObj.sessionUserDepartID),"1");

            this.lit_album_count.Text = count + "";

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

        if (Command == "peo") {
            
            this.Control.Visible = true;
        }

        if (Command == "dep") {
            this.Control.Visible = false;
        }

        if (Command == "all") {
            this.Control.Visible = false;
        }
        
        //if()
    }


    /// <summary>
    /// 初始化與設定按鈕的CSS屬性
    /// </summary>
    /// <param name="b"></param>
    private void InitButtonCss(Button b){
        this.Button1.CssClass="b-input";
        this.Button2.CssClass = "b-input";
        this.Button3.CssClass = "b-input";

        b.CssClass = "b-input2";
    }
}