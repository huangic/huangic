using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;

using Entity;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using NXEIP.DAO;
using NLog;



namespace NXEIP.Widget
{



    /// <summary>
    /// WidgetPageTemplate 的摘要描述
    /// </summary>
    public abstract class WidgetPageTemplate : System.Web.UI.Page
    {
        public WidgetPageTemplate()
        {
         
        }


        //編輯狀態
        protected bool IsEditable = false;
        //是否有頁面可以顯示
        protected bool IsHasWidgetPage = true;


     
        /// <summary>
        ///    //此頁面使用的SESSION; 子代OVERRIDE
        /// </summary>
        public abstract String SessionName { get; }
        /// <summary>
        /// 此頁面使用的編修用SESSION  子代OVERRIDE
        /// </summary>

        public abstract String SessionTmpName { get ; }



        

        /// <summary>
        /// 此頁面的UID 子代OVERRIDE
        /// </summary>
        public abstract String Uid { get; }

        //頁面的三塊可編輯區塊
        protected String[] Divs  = { "leftDiv", "centerDiv", "rightDiv" };


        //編修時會看到的DIV
        protected String[] EditPanelDiv = { "holder1", "holder2", "holder3", "Panel1" };
        //設定區
        protected String WidgetFuncDiv = "func";
        

      
        /// <summary>
        /// 此頁面的型態 P為個人頁 子代OVERRIDE
        /// </summary>
        public abstract String PageType { get; }

        /// <summary>
        /// 遠端AJAX使用的頁面  子代OVERRIDE
        /// </summary>

        protected virtual String RemoteUrl { get { return "~/widget/WidgetMethod.aspx"; } }



        protected virtual String WidgetWrapPage { get { return "~/widget/WidgetWrapPage.aspx"; } }
        static Logger logger = LogManager.GetCurrentClassLogger();


        protected void Page_Load(object sender, EventArgs e)
        {

            //throw new Exception("TEST");

            InitWidget();


        }



        protected void InitWidget(){
            logger.Debug("Start Load Widget");
            try
            {
                IsEditable = Boolean.Parse(Request.QueryString["edit"]);
            }
            catch
            {
                
                //NULL就NULL不管他
            }

           


           


            this.LoadAllWidget();



            #region 編修狀態加上JS與變更DIV
            if (IsEditable&&IsHasWidgetPage)
            {
               

                foreach (String div in EditPanelDiv)
                {

                    Master.FindControl("ContentPlaceHolder1").FindControl(div).Visible = true;
                }





                Page.ClientScript.RegisterClientScriptInclude("JQUERYUI", ResolveUrl("~/js/jquery-ui-1.8.2.custom.min.js"));
                Page.ClientScript.RegisterClientScriptInclude("Widget", ResolveUrl("~/js/jquery.easywidgets.js"));
                Page.ClientScript.RegisterClientScriptInclude("WidgetEnable", ResolveUrl("~/js/jquery.easywidgets.enable.js"));
                Page.ClientScript.RegisterClientScriptInclude("JSON2", ResolveUrl("~/js/json2.js"));

                String WidgetUrl = "var widgetRemoteUrl = \"" + ResolveUrl(this.RemoteUrl) + "\";";

                WidgetJson json = new WidgetJson();

                json.SessionName = this.SessionName;
                json.SessionTmpName = this.SessionTmpName;
                json.Uid = this.Uid;
                json.PageType = this.PageType;



                String JsonWidget = "var widgetSetting=" + JsonConvert.SerializeObject(json)+";";

                Page.ClientScript.RegisterStartupScript(typeof(Page), "WidgetUrl", WidgetUrl, true);
                Page.ClientScript.RegisterStartupScript(typeof(Page), "WidgetSetting", JsonWidget, true);
                CreateWidgetPanel();

            }
            #endregion


            logger.Debug("End Load Widget");
        
        }

        /// <summary>
        /// 讀取此頁面的所有WIDGET區塊 
        /// 呼叫流程
        /// 取出此頁的PAGE_NO
        /// 生成WidgetObj物件放置SESSION
        /// 讀取WidgetObj物件真正生成Widget
        /// </summary>
        protected virtual void LoadAllWidget()
        {

            try
            {
                int page_no = this.GetCurrentPage();



              //如果WidgetPage的狀態是FALSE那就表示沒有他的頁面也沒有父代的那就不生成了(變空白頁)
                if (this.IsHasWidgetPage)
                {


                    //如果SESSION 沒有東西 那就是RELOAD
                    if (Session[SessionName] == null)
                    {
                        CreateWidget(page_no);
                    }


                    //載入所有WIDGET物件
                    LoadZoneWidgetSession(page_no);
                }

            }
            catch (Exception ex)
            {

            }
        }

        /// <summary>
        /// 取目前的頁面ID 如果他自己沒有就抓父代(DEFAULT) 各頁實作 自己的父代
        /// </summary>
        /// <returns></returns>

        protected virtual int GetCurrentPage() {
            WidgetDAO Dao = new WidgetDAO();

            int uid = System.Convert.ToInt32(this.Uid);


            int? page_no = Dao.GetPageNo(uid, this.PageType);

          


            logger.Info(string.Format("使用PAGE_NO={0}的設定",page_no));

            return page_no.Value;
        
        }
       



        /// <summary>
        /// 從SESSION中取出WIDGET物件
        /// </summary>
        /// <param name="page_no"></param>
        protected void LoadZoneWidgetSession(int page_no)
        {
            WidgetObj widgetObj = null;
            //從Session 拿 Widget

            #region 編輯模式要從一般SESSION 取出一份放置編輯SESSION中,不然都抓一般SESSION的
          
                //從暫存取WIDGET物件

                //widgetObj = Session[SessionTmpName] != null && IsEditable ? (WidgetObj)Session[SessionTmpName] : (WidgetObj)Session[SessionName];

                widgetObj = (WidgetObj)Session[SessionName];


          
                //widgetObj 應該不可能為NULL 


            #endregion


            #region Session 的物件轉成WIDGET畫面
            foreach (WidgetPlace p in widgetObj.Place)
            {
                foreach (WidgetBlock b in p.Block)
                {
                    Entity.widget widget = GetWidget(b.WidgetID);

                    WidgetBaseControl control = (WidgetBaseControl)Page.LoadControl("~/" + widget.wid_url);
                    control.Title = widget.wid_name;
                    control.WidgetID = widget.wid_no;
                    control.IsEditable = this.IsEditable;

                    Master.FindControl("ContentPlaceHolder1").FindControl(p.Name).Controls.Add(control);

                }
            }
            #endregion


        }





      


     


        /// <summary>
        /// 取WIDGET 的詳細資料
        /// </summary>
        /// <param name="wid_no"></param>
        /// <returns></returns>
        protected Entity.widget GetWidget(int wid_no)
        {
            using (NXEIPEntities model = new NXEIPEntities())
            {
                
                return (from w in model.widget where w.wid_no == wid_no select w).FirstOrDefault();
            }
        }

        #region 讀取物件放置於SESSION
        /// <summary>
        /// 讀取物件放置於SESSION
        /// </summary>
        /// <param name="page_no"></param>
        protected void CreateWidget(int page_no)
        {

            WidgetDAO Dao = new WidgetDAO();
            
            
            //讀每一個ZONE WIDGET 
            WidgetObj widgetObj = new WidgetObj();
            widgetObj.Place = new WidgetPlace[Divs.Length];
            int place_position = 0;


            foreach (String div in Divs)
            {
                WidgetPlace place = new WidgetPlace();
                place.Name = div;


                var widgets = Dao.GetZoneWidget(page_no, div);
                place.Block = new WidgetBlock[widgets.Count()];

                int position = 0;
                foreach (var w in widgets)
                {
                    WidgetBlock block = new WidgetBlock(w.wid_no);
                    place.Block[position] = block;
                    position++;
                    //塞到block物件裡面



                }

                widgetObj.Place[place_position] = place;
                place_position++;


            }


            Session[SessionName] = widgetObj;

        }
        #endregion


        /// <summary>
        /// 目前可用的WIDGET新增面板
        /// </summary>
        protected void CreateWidgetPanel()
        {

            NXEIPEntities model = new NXEIPEntities();


            //this.func.Controls.Clear();
            HtmlGenericControl HtmlUl = new HtmlGenericControl("ul");

            //Control HtmlUl = Master.FindControl("ContentPlaceHolder1").FindControl(this.WidgetFuncDiv).FindControl("funcUl");



            //取出可以用的WIDGET(TYPE =P status=1)
            var widgets = from w in model.widget where w.wid_status.Equals("1") && w.wid_type.Equals(this.PageType) select w;



            foreach (var w in widgets)
            {



                //TODO: ajax
                //*加上ajax的新增方式*
                HtmlGenericControl HtmlLi2 = new HtmlGenericControl("li");
                HtmlAnchor alb = new HtmlAnchor();
                alb.HRef="javascript:AddWidgetBlock(" + w.wid_no + ")";
                alb.ID = "Widget-" + w.wid_no;
               
                alb.InnerText = w.wid_name + "";
                HtmlLi2.Controls.Add(alb);
                HtmlUl.Controls.Add(HtmlLi2);


            }

            Control ParentDiv = Master.FindControl("ContentPlaceHolder1").FindControl(this.WidgetFuncDiv);



            ParentDiv.Controls.Add(HtmlUl);

            HtmlGenericControl htmlDiv=new HtmlGenericControl("div");
            htmlDiv.Attributes["class"]="footer";



            ParentDiv.Controls.Add(htmlDiv);

        }
  



    }
}