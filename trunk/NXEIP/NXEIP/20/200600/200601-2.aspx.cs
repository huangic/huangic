using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IMMENSITY.SWFUploadAPI;
using System.Runtime.Serialization.Json;
using Entity;
using System.Security.Cryptography;
using System.Text;
using NXEIP.DAO;

public partial class _20_200600_200601_2 : System.Web.UI.Page
{
   
    NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

    SessionObject sessionObj = new SessionObject();

    String mode=String.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        //取模式
        mode = Request["f"];

        


        //驗證觀看權限

        //取這個討論區
        int tao_no = int.Parse(Request["tao_no"]);
        int peo_uid = int.Parse(sessionObj.sessionUserID);
       
        _200601DAO dao = new _200601DAO();

        Forum f = dao.GetFourumById(tao_no, peo_uid);
         String permission = f.Permission;



         bool HasPermission = Forum.GetPermission(permission, Forum.ForumPermission.ReadTopic);

        if (HasPermission)
        {

            



            if (Page.PreviousPage != null && PreviousPage.IsCrossPagePostBack)
            {
                //取查詢條件
                String keyword;
                String option;
                bool timeFlag;
                DateTime? sdate = null, edate =null;

                keyword = ((TextBox)Page.PreviousPage.Master.FindControl("ContentPlaceHolder1").FindControl("tb_keyword")).Text;

                option = ((RadioButtonList)Page.PreviousPage.Master.FindControl("ContentPlaceHolder1").FindControl("rb_option")).SelectedValue;

                timeFlag = ((CheckBox)Page.PreviousPage.Master.FindControl("ContentPlaceHolder1").FindControl("timeFlag")).Checked;

                object target = Page.PreviousPage.Master.FindControl("ContentPlaceHolder1").FindControl("sdate");

                try
                {
                    sdate = (DateTime)target.GetType().GetProperty("_ADDate").GetValue(target, null);
                }
                catch { 
                
                }

                try
                {
                    target = Page.PreviousPage.Master.FindControl("ContentPlaceHolder1").FindControl("edate");
                    edate = (DateTime)target.GetType().GetProperty("_ADDate").GetValue(target, null);
                }
                catch { 
                
                }

                //查詢
                this.ObjectDataSource1.SelectParameters[3].DefaultValue = keyword;
                this.ObjectDataSource1.SelectParameters[4].DefaultValue = option;

                
                //如果有判斷日期
                if (timeFlag)
                {
                    this.ObjectDataSource1.SelectParameters[5].DefaultValue = sdate.ToString();
                    this.ObjectDataSource1.SelectParameters[6].DefaultValue = edate.ToString();
                }
                else {
                    //this.ObjectDataSource1.SelectParameters[5].DefaultValue = "";
                    //this.ObjectDataSource1.SelectParameters[6].DefaultValue = "";
                
                }

                this.GridView1.DataBind();

                logger.Debug("Search");

            }
            else
            {

                if (!Page.IsPostBack)
                {


                    InitHyperLink(permission);



                    if (!String.IsNullOrEmpty(mode))
                    {
                        this.ObjectDataSource1.SelectParameters[2].DefaultValue = "True";
                        this.Navigator1.SubFunc = "精華區主題列表";
                    }

                    this.ObjectDataSource1.SelectParameters[1].DefaultValue = sessionObj.sessionUserID;

                    this.GridView1.DataBind();

                    this.Navigator1.SubFunc += String.Format("-{0}", f.Name);




                    //主題的計數器

                    using (NXEIPEntities model = new NXEIPEntities())
                    {
                        taolun tao = new taolun();

                        tao.tao_no = f.Id;
                        model.taolun.Attach(tao);

                        tao.tao_count = f.ClickCount + 1;

                        model.SaveChanges();
                    }
                }

            }
            
            

            //判斷來自JS 使用_doPostBack(updatePanel,"") 的情況 
            if (Request["__EVENTTARGET"] == this.UpdatePanel1.ClientID && String.IsNullOrEmpty(Request["__EVENTARGUMENT"]))
            {
                this.GridView1.DataBind();
            }



        }
        else { 
            //顯示錯誤訊息
            //this.ShowTopic.Visible = false;
            JsUtil.AlertJs(this,"你沒有觀看主題的權限");
        
        }
        
    }

    protected String GetROCDT(DateTime? dt)
    {
        if (dt.HasValue)
        {
            return new ChangeObject().ADDTtoROCDT(dt.Value);

        }
        return "";
    }


    private void InitHyperLink(String permission)
    {
        //初始化所有連結按鈕

        
        
        //依照權限決定按鈕

            //讀取權限
        if (Forum.GetPermission(permission, Forum.ForumPermission.ReadForum))
            if (Forum.GetPermission(permission, Forum.ForumPermission.ReadTopic))
            {

           



                //建立按鈕

                if (String.IsNullOrEmpty(mode))
                {
                    //精華區
                    this.hl_featured.Visible = true;
                    this.hl_featured.NavigateUrl = String.Format("200601-2.aspx?tao_no={0}&f=1", Request["tao_no"]);

                    
                
                }
                else {
                    //返回主題區
                    this.hl_featured.Visible = true;
                    this.hl_featured.NavigateUrl = String.Format("200601-2.aspx?tao_no={0}", Request["tao_no"]);
                    this.hl_featured.Text = "主題列表";
                
                }

                //SEARCH
                this.hl_search.Visible = true;
                this.hl_search.NavigateUrl = String.Format("200601-11.aspx?tao_no={0}&f={1}", Request["tao_no"],mode);

                

                //有寫入權限才有(MODE 是精華區列表)
                if (String.IsNullOrEmpty(mode) && Forum.GetPermission(permission, Forum.ForumPermission.Write))
                {
                    //新主題
                    this.hl_post.Visible = true;
                    this.hl_post.NavigateUrl = String.Format("200601-6.aspx?tao_no={0}&TB_iframe=true&height=450&width=650&modal=true", Request["tao_no"]);

                   
                
                }


                if (String.IsNullOrEmpty(mode) && Forum.GetPermission(permission, Forum.ForumPermission.Manager)) {
                    //會員管理
                    this.hl_manager.Visible = true;
                    this.hl_manager.NavigateUrl = String.Format("200601-9.aspx?tao_no={0}", Request["tao_no"]);
                }



            }

        //沒有寫入權限都可以聲請會員
            if (String.IsNullOrEmpty(mode)&&!Forum.GetPermission(permission, Forum.ForumPermission.Write))
            { 
            //秀加入會員按鈕

                this.hl_member.Visible = true;
                this.hl_member.NavigateUrl = String.Format("200601-7.aspx?tao_no={0}&TB_iframe=true&height=450&width=650&modal=true", Request["tao_no"]);

            }
            



        
       

    }




    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int index = ((GridViewRow)((Button)e.CommandSource).NamingContainer).RowIndex;

        int tao_no = int.Parse(GridView1.DataKeys[index].Values["ForumId"].ToString());
        int t01_no = int.Parse(GridView1.DataKeys[index].Values["Id"].ToString());

        #region //刪除回應
        if (e.CommandName == "del")
        {
            //刪除回應

            tao01 t = new tao01();

            t.tao_no = tao_no;
            t.t01_no = t01_no;

            using (NXEIPEntities model = new NXEIPEntities())
            {
                model.tao01.Attach(t);
                t.t01_status = "2";
                model.SaveChanges();

            }

            this.GridView1.DataBind();

        }
        #endregion

    }
}