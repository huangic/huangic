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

public partial class _20_200600_200601_3 : System.Web.UI.Page
{

    NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

    SessionObject sessionObj = new SessionObject();

    String mode = String.Empty;
    bool CanWrite;
    protected void Page_Load(object sender, EventArgs e)
    {

        //取模式
        mode = Request["f"];
        




        InitHyperLink();


        //判斷權限
        //取這個討論區
        int tao_no = int.Parse(Request["tao_no"]);
        int t01_no = int.Parse(Request["t01_no"]);
        int peo_uid = int.Parse(sessionObj.sessionUserID);
       
        _200601DAO dao = new _200601DAO();

        Forum f = dao.GetFourumById(tao_no, peo_uid);
         String permission = f.Permission;



         bool HasPermission = Forum.GetPermission(permission, Forum.ForumPermission.ReadContent);

         this.CanWrite = Forum.GetPermission(permission, Forum.ForumPermission.Write);

         if (HasPermission)
         {

             //判斷文章是否還存在(被刪除的話就不秀出來了)
             int Topic = 0;


             using (NXEIPEntities model = new NXEIPEntities()) {
                 Topic = (from d in model.tao01 where d.tao_no == tao_no && d.t01_no == t01_no && d.t01_status=="1" select d).Count();

                
             }
             //主題必須存在
             if (Topic > 0)
             {
                 if (!Page.IsPostBack)
                 {
                     this.ObjectDataSource1.SelectParameters[0].DefaultValue = tao_no.ToString();
                     this.ObjectDataSource1.SelectParameters[1].DefaultValue = t01_no.ToString();
                     this.ObjectDataSource1.SelectParameters[2].DefaultValue = sessionObj.sessionUserID;
                     this.ListView1.DataBind();
                 }

                 //判斷來自JS 使用_doPostBack(updatePanel,"") 的情況 
                 if (Request["__EVENTTARGET"] == this.UpdatePanel1.ClientID && String.IsNullOrEmpty(Request["__EVENTARGUMENT"]))
                 {
                     this.ListView1.DataBind();
                 }
             }
             else {
                 JsUtil.AlertJs(this, "主題已經不存在!!!");
             }

         }
         else {
             //顯示錯誤訊息
             //this.ShowTopic.Visible = false;
             JsUtil.AlertJs(this, "你沒有觀看內容的權限");
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


    private void InitHyperLink() {
        this.hl_list.NavigateUrl = String.Format("200601-2.aspx?tao_no={0}", Request["tao_no"]);
    
    }

    protected void ListView1_ItemCommand(object sender, ListViewCommandEventArgs e)
    {
        
        ListViewDataItem dataItem = (ListViewDataItem)e.Item;

        int tao_no = int.Parse(ListView1.DataKeys[dataItem.DisplayIndex].Values["ForumId"].ToString());
        int t01_no = int.Parse(ListView1.DataKeys[dataItem.DisplayIndex].Values["Id"].ToString());
        int parent_no = int.Parse(ListView1.DataKeys[dataItem.DisplayIndex].Values["ParentId"].ToString());

        #region //刪除回應
        if (e.CommandName == "del") { 
        //刪除回應
           
            tao01 t = new tao01();

            t.tao_no = tao_no;
            t.t01_no = t01_no;

            using (NXEIPEntities model = new NXEIPEntities()) {
                model.tao01.Attach(t);
                t.t01_status = "2";
                model.SaveChanges();
            
            }

            this.ListView1.DataBind();

        }
        #endregion

        #region 加入收藏區

        if (e.CommandName == "AddFolder")
        {
            //取文章的編號 (如果是回復就是抓整篇)
            using (NXEIPEntities model = new NXEIPEntities())
            {
                if (parent_no != 0) {
                    t01_no = parent_no;
                }

                tao05 t05= new tao05();

                t05.t01_no = t01_no;
                t05.tao_no = tao_no;
                t05.t05_peouid = int.Parse(sessionObj.sessionUserID);
                t05.t05_type = "1";

                model.tao05.AddObject(t05);
                model.SaveChanges();
            
            }
        }
        #endregion

        #region 加入追蹤區

        if (e.CommandName == "AddTrack")
        {
            //取文章的編號 (如果是回復就是抓整篇)
            using (NXEIPEntities model = new NXEIPEntities())
            {
                if (parent_no != 0)
                {
                    t01_no = parent_no;
                }

                tao05 t05 = new tao05();

                t05.t01_no = t01_no;
                t05.tao_no = tao_no;
                t05.t05_peouid = int.Parse(sessionObj.sessionUserID);
                t05.t05_type = "2";

                model.tao05.AddObject(t05);
                model.SaveChanges();

            }
        }
        #endregion



        #region 加入精華

        if (e.CommandName == "AddFeatured")
        {
            if (parent_no != 0)
            {
                t01_no = parent_no;
            }

            using (NXEIPEntities model = new NXEIPEntities())
            {
                tao02 t02 = new tao02();

                t02.t01_no = t01_no;
                t02.tao_no = tao_no;
                //t02. = int.Parse(sessionObj.sessionUserID);

                model.tao02.AddObject(t02);
                model.SaveChanges();
            }


        }

        #endregion
    }
    /// <summary>
    /// 回應的權限
    /// </summary>
    /// <returns></returns>
    protected bool IsCanWrite() {
        return this.CanWrite;
    }

}