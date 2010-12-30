using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NXEIP.DAO;
using Entity;

public partial class _10_100100_100103_3 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!Page.IsPostBack) {
            int albumId = 0;

            int.TryParse(Request["album"], out albumId);
            
            
            
            //init DataSource

            SessionObject sessionObj = new SessionObject();

            int peo_uid=0;

            int.TryParse(sessionObj.sessionUserID, out peo_uid);


            this.ObjectDataSource1.SelectParameters[0].DefaultValue = Request["album"];

            


            this.ListView1.DataBind();



            //判斷相簿的權限

            using(NXEIPEntities model=new NXEIPEntities()){

            try{
             var Album=(from d in model.album where d.alb_no==albumId && d.peo_uid==peo_uid select d).First();
                if(Album!=null){
                    this.Control.Visible=true;
                    
                }
            
            }catch{
                //this.Control.Visible = true;
            }
            
            }


            //計算幾張相片
            _100103DAO dao=new _100103DAO();

            int count = dao.GetAlbumPhoto(albumId).Count();

            this.lit_photo_count.Text = count + "";

        }



        if (Request["__EVENTTARGET"] == this.UpdatePanel1.ClientID && String.IsNullOrEmpty(Request["__EVENTARGUMENT"]))
        {
            this.ListView1.DataBind();
        }

    }



    protected bool CheckPermission(int peo_uid)
    {

        SessionObject sessionObj = new SessionObject();


        return peo_uid == int.Parse(sessionObj.sessionUserID);

    }


    
}