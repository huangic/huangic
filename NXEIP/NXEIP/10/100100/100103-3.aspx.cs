using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NXEIP.DAO;
using Entity;
using System.IO;

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

    protected void btn_del_Click(object sender, EventArgs e)
    {
        //刪除相簿

        String FilePath = new ArgumentsObject().Get_argValue("100103_dir");

        //取有打勾的

        //this.ListView1.Items
        using (NXEIPEntities model = new NXEIPEntities())
        {

            foreach (var item in this.ListView1.Items)
            {
                int albumId = int.Parse(this.ListView1.DataKeys[item.DataItemIndex].Values[0].ToString());
                int photoId = int.Parse(this.ListView1.DataKeys[item.DataItemIndex].Values[1].ToString());

                CheckBox cb = (CheckBox)item.FindControl("CheckBox1");
                if (cb.Checked)
                {
                   
                    //取相片刪檔案

                    var photo = (from d in model.photo where d.alb_no == albumId && d.pho_no == photoId select d).First();
                    try
                    {
                        File.Delete(FilePath + photo.pho_file);
                        File.Delete(FilePath + photo.pho_thumb);
                    }
                    catch { 
                    
                    }


                    // album a = (album)o;

                    model.photo.DeleteObject(photo);
                   



                }

            }

            model.SaveChanges();
        }

        this.ListView1.DataBind();
    }

    protected void btn_cover_Click(object sender, EventArgs e)
    {

        //設定封面
        int check = 0;
        int cover =0;
        int album = 0;

        //this.ListView1.Items
        using (NXEIPEntities model = new NXEIPEntities())
        {


            foreach (var item in this.ListView1.Items)
            {
                int albumId = int.Parse(this.ListView1.DataKeys[item.DataItemIndex].Values[0].ToString());
                int photoId = int.Parse(this.ListView1.DataKeys[item.DataItemIndex].Values[1].ToString());

                CheckBox cb = (CheckBox)item.FindControl("CheckBox1");
                if (cb.Checked)
                {
                    check++;
                    //取相片刪檔案
                    cover = photoId;
                    album = albumId;

                    // album a = (album)o;

                }

            }

            if (check == 1)
            {



                //更新最後的為封面
                var a = (from d in model.album where d.alb_no == album select d).First();

                a.alb_cover = cover;

                model.SaveChanges();
                JsUtil.AlertJs(this, "封面設定完成");
            }
            else {
                JsUtil.AlertJs(this,"請選擇一個相片作為封面");
            }
        }



    }
}