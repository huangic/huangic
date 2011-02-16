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

public partial class _20_200600_200601_12 : System.Web.UI.Page
{

    NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

    SessionObject sessionObj = new SessionObject();
    
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!this.IsPostBack) {

            //lab_manager.Text = sessionObj.sessionUserName;

        }
        


    }
   
   
    protected void button_ok_Click(object sender, EventArgs e)
    {
        //申請討論區

            //欄位檢查
        String msg = String.Empty;

        msg = CheckField();

        if (String.IsNullOrEmpty(msg))
        {

            using (NXEIPEntities model = new NXEIPEntities())
            {
                int tao_no=int.Parse(Request["tao_no"]);
                int peo_uid=int.Parse(sessionObj.sessionUserID);

                int count = (from d in model.tao06 where d.tao_no == tao_no && d.peo_uid == peo_uid select d).Count();
                
                
                //討論區訂閱存檔
                tao06 t = new tao06();


                t.tao_no = tao_no;
                t.peo_uid = peo_uid;

                if (count > 0)
                {
                    model.tao06.Attach(t);
                }
                else {
                    model.tao06.AddObject(t);
                }
                t.t06_order = "1";

                //t.tao_model
                //通知

                int checkValue = 0;

                foreach (ListItem c in this.cb_notify.Items)
                {
                    if (c.Selected)
                    {
                        checkValue += int.Parse(c.Value);
                    }
                }

                //轉為2進位字串
                String CheckFlag = Convert.ToString(checkValue, 2).PadLeft(3, '0');

                t.t06_model = CheckFlag;


                //

                //model.tao06.AddObject(t);
                model.SaveChanges();

            }

            //通知總管理者




            JsUtil.UpdateParentJs(this, "設定完成");

        }
        else {
            JsUtil.AlertJs(this, msg);
        }

    }


    private String CheckField() { 
        //討論區名稱欄位
        String msg = String.Empty;

        //if (String.IsNullOrEmpty(this.tb_name.Text)) {
        //    msg += "請輸入討論區名稱\\n";


        //}

        return msg;

        //
    }
}