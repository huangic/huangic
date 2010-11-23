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

public partial class _20_200300_200303 : System.Web.UI.Page
{
    private static Logger logger = LogManager.GetCurrentClassLogger();
    
    protected void Page_Load(object sender, EventArgs e)
    {



            //載入場地
        if (!Page.IsPostBack) {
            _200303DAO dao = new _200303DAO();
            spot s=dao.GetFloorsSpot().First();

            this.hidden_spot.Value = s.spo_no+"" ;

            ShowFloor();
        }


          
    }






    private void ShowFloor(){
        int spot_no = int.Parse(this.hidden_spot.Value);

        FloorsDAO dao = new FloorsDAO();

        IQueryable<floors> floors=dao.GetDataBySpotNo(spot_no);

        var groups = from d in floors
                     group d by d.flo_level into f
                     select f;

        //  <div class="box">
        //   	   <div class="head">14樓</div>
         	    
        // 	      <div class="content">
        //            <div class="b1"></div>
       	//	           <div class="b2">
         //           巿政資料中心 (Iinstitute For informatin Industry)、員工圖書室會議室 (Iinstitute For informatin Industry)、員工圖書室 (Iinstitute For informatin Industry)、會議室 (Iinstitute For informatin Industry)、員工圖書室會議室 (Iinstitute For informatin Industry)</div>
         
        //            </div>
        //       </div>
        //  </div>

        //分開地下樓跟一般樓
        var general = from d1 in groups where !d1.Key.Contains("B") orderby d1.Key descending select d1;
        var ground = from d2 in groups where d2.Key.Contains("B") select d2;


        foreach (var a in general)
        {
            String[] array = (from d in a  orderby d.flo_order select d.flo_name + "(" + d.flo_ename + ")").ToArray();
            GenDiv(a.Key,String.Join("、",array));            
        }

        foreach (var a in ground)
        {
            String[] array = (from d in a  orderby d.flo_order select d.flo_name + "(" + d.flo_ename + ")").ToArray();
            GenDiv(a.Key,String.Join("、",array));            
        }


    }

    private void GenDiv(string key,string value) {
        HtmlGenericControl DivBox = new HtmlGenericControl("div");
        HtmlGenericControl DivHead = new HtmlGenericControl("div");
        HtmlGenericControl DivContent = new HtmlGenericControl("div");
        HtmlGenericControl DivB1 = new HtmlGenericControl("div");
        HtmlGenericControl DivB2 = new HtmlGenericControl("div");

        DivBox.Controls.Add(DivHead);
        DivBox.Controls.Add(DivContent);
        DivContent.Controls.Add(DivB1);
        DivContent.Controls.Add(DivB2);

        DivBox.Attributes["class"] = "box";
        DivHead.Attributes["class"] = "head";
        DivContent.Attributes["class"] = "content";
        DivB1.Attributes["class"] = "b1";
        DivB2.Attributes["class"] = "b2";

        DivHead.InnerText = key + "樓";
        DivB2.InnerText = value;
        this.div_floor.Controls.Add(DivBox);
    }


    protected void lv_spot_ItemCommand(object sender, ListViewCommandEventArgs e)
    {
        if (e.CommandName == "click") {
            int index = Convert.ToInt32(e.Item.DataItemIndex);

            this.hidden_spot.Value = this.lv_spot.DataKeys[index].Value.ToString();

            this.ShowFloor();
            this.lv_spot.DataBind();
        }
    }
}