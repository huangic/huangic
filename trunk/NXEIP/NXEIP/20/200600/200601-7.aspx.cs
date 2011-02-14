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

public partial class _20_200600_200601_5 : System.Web.UI.Page
{

    NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

    SessionObject sessionObj = new SessionObject();



    protected void Page_Load(object sender, EventArgs e)
    {
        int tao_no = int.Parse(Request["tao_no"]);
            

        if (!this.IsPostBack)
        {

            //取討論區

            Forum f = new _200601DAO().GetFourumById(tao_no,int.Parse(sessionObj.sessionUserID));

            this.lab_forum.Text = f.Name;

            this.lab_people.Text = sessionObj.sessionUserName;

            this.lab_dep.Text = sessionObj.sessionUserDepartName;

        }



    }


    protected void Button2_Click(object sender, EventArgs e)
    {
               
        JsUtil.UpdateParentJs(this, null);


    }




    protected void button_ok_Click(object sender, EventArgs e)
    {
        //申請討論區

        //欄位檢查
        String msg = String.Empty;

       

        if (String.IsNullOrEmpty(msg))
        {
            int tao_no = int.Parse(Request["tao_no"]);

            //會員存檔
            //寫入會員

            using (NXEIPEntities model = new NXEIPEntities())
            {

                //取主旨
                tao03 t03 = new tao03();

               // t.t01_subject = this.tb_subject.Text;
                t03.tao_no = tao_no;
                t03.peo_uid = int.Parse(sessionObj.sessionUserID);
                t03.t03_memo = this.TextBox1.Text;
                t03.t03_date = DateTime.Now;
                t03.t03_status = "0";

                //取最大值
                int max = 1;
                try
                {
                    max = (from d in model.tao03 where d.tao_no == tao_no select d.t03_no).Max();
                    max++;
                }
                catch
                {

                }

                t03.t03_no = max;


                model.tao03.AddObject(t03);
                model.SaveChanges();


            }

            //t.tao_model
            //通知

            JsUtil.UpdateParentJs(this, "已送出申請");
        }



        else
        {
            JsUtil.AlertJs(this, msg);
        }
    }



  
}