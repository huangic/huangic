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

public partial class _20_200600_200601_1 : System.Web.UI.Page
{

    NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

    SessionObject sessionObj = new SessionObject();
    
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!this.IsPostBack) {

            lab_manager.Text = sessionObj.sessionUserName;




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
            //討論區存檔
            taolun t = new taolun();

            t.peo_uid = int.Parse(sessionObj.sessionUserID);
            t.tao_count = 0;
            t.tao_depno = int.Parse(sessionObj.sessionUserDepartID);
            t.tao_descript = this.TextBox1.Text;
            t.tao_name = this.tb_name.Text;
            t.tao_ename = this.tb_ename.Text;
            t.tao_status = "0";
            t.tao_type = this.RadioButtonList1.SelectedValue;


            //t.tao_model
            //通知

            int checkValue = 0;
            
            foreach(ListItem c in this.cb_notify.Items){
                if (c.Selected) {
                    checkValue += int.Parse(c.Value);
                }
            }

            //轉為2進位字串
            String CheckFlag =Convert.ToString(checkValue, 2).PadLeft(3,'0');

            t.tao_model = CheckFlag;

            



            //通知總管理者

            //取總管理者
            List<people> root = new ManagerDAO().GetRootManager();


            PersonalMessageUtil msgUtil = new PersonalMessageUtil();

            foreach (var p in root)
            {
                msgUtil.SendMessage("申請討論區", "申請討論區", "", p.peo_uid, int.Parse(sessionObj.sessionUserID), true, false, false);
            }


                                 


        }
        else {
            JsUtil.AlertJs(this, msg);
        }

    }


    private String CheckField() { 
        //討論區名稱欄位
        String msg = String.Empty;

        if (String.IsNullOrEmpty(this.tb_name.Text)) {
            msg += "請輸入討論區名稱\\n";


        }

        return msg;

        //
    }
}