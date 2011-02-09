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

            InitHyperLink(permission);


            if (!Page.IsPostBack)
            {
                
                if (!String.IsNullOrEmpty(mode)) {
                    this.ObjectDataSource1.SelectParameters[2].DefaultValue = "True";
                    this.Navigator1.SubFunc = "精華區主題列表";
                }
                
                this.ObjectDataSource1.SelectParameters[1].DefaultValue = sessionObj.sessionUserID;
                
                this.GridView1.DataBind();

                this.Navigator1.SubFunc += String.Format("-{0}", f.Name);
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

                

                //有寫入權限才有
                if (String.IsNullOrEmpty(mode)&&Forum.GetPermission(permission, Forum.ForumPermission.Write))
                {
                    //新主題
                    this.hl_post.Visible = true;
                    this.hl_post.NavigateUrl = String.Format("200601-4.aspx?TB_iframe=true&height=450&width=650&modal=true&tao_no={0}", Request["tao_no"]);
                }
               

            }
            else { 
            //只秀加入會員按鈕

                this.hl_member.Visible = true;
                this.hl_member.NavigateUrl = String.Format("200601-5.aspx?TB_iframe=true&height=450&width=650&modal=true&tao_no={0}", Request["tao_no"]);

            }
            



        
       

    }

    

}