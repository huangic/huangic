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

public partial class _20_200600_200601_10 : System.Web.UI.Page
{

    NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

    SessionObject sessionObj = new SessionObject();


    String mode = String.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {

        




        if (!this.IsPostBack) {



            this.DepartmentPanel1.Clear();

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

        //msg = CheckField();

        if (String.IsNullOrEmpty(msg))
        {
            int tao_no = int.Parse(Request["tao_no"]);
            
                        




                using (NXEIPEntities model = new NXEIPEntities())
                {

                    foreach (var peo_uid in this.DepartmentPanel1.ItemsValue)
                    {


                        //取主旨
                        tao03 t03 = new tao03();


                        t03.tao_no = tao_no;
                        t03.peo_uid = int.Parse(peo_uid);
                        t03.t03_memo = String.Format("版主{0}設定加入會員", sessionObj.sessionUserName);
                        t03.t03_date = DateTime.Now;
                        t03.t03_status = "1";

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
                   


                }

                //t.tao_model
                //通知

                JsUtil.UpdateParentJs(this, "已加入會員");
            
            
        }
        else {
            JsUtil.AlertJs(this, msg);
        }

    }


    
}