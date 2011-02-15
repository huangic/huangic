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





        int size = 0;

        int.TryParse(new ArgumentsObject().Get_argValue("200601_size"), out size);

        this.UC_SWFUpload1.SwfUploadInfo = new SWFUploadInfo()
        {
            UploadMode = UpMode.LIST,
            File_size_limit = size,
            File_upload_limit = 1,
            SubmitButtonId = this.button_ok.ClientID,
            Path = "/upload/200601/",
            PathArg = "200601_dir"

        };



        SWFUploadFileInfo uf = new SWFUploadFileInfo();
        DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(SWFUploadFileInfo));
        System.IO.MemoryStream ms = new System.IO.MemoryStream();
        serializer.WriteObject(ms, uf);





        if (!this.IsPostBack)
        {

            lab_manager.Text = sessionObj.sessionUserName;





        }



    }


    protected void Button2_Click(object sender, EventArgs e)
    {

        //移除上傳過的東西

        SWFUploadFile uf = new SWFUploadFile();

        foreach (var f in UC_SWFUpload1.SWFUploadFileInfoList)
        {

            String del_msg = uf.Delete(f.Path, f.FileName, true);

            logger.Debug(del_msg);

        }



        JsUtil.UpdateParentJs(this, null);


    }




    protected void button_ok_Click(object sender, EventArgs e)
    {
        //申請討論區

        //欄位檢查
        String msg = String.Empty;

        msg = CheckField();

        if (String.IsNullOrEmpty(msg))
        {
            int tao_no = int.Parse(Request["tao_no"]);





            //討論區存檔
            //寫入回應

            tao01 t = new tao01();
            t.tao_no = tao_no;
            t.t01_parent = 0;

            t.t01_depno = int.Parse(sessionObj.sessionUserDepartID);
            t.t01_peouid = int.Parse(sessionObj.sessionUserID);

            t.t01_content = this.TextBox1.Text;
            t.t01_date = DateTime.Now;
            t.t01_status = "1";
            t.t01_order = 0;

            //檔案處理

            if (UC_SWFUpload1.SWFUploadFileInfoList.Count > 0)
            {
                var f = UC_SWFUpload1.SWFUploadFileInfoList[0];
                t.t01_file = f.OriginalFileName;
                t.t01_path = f.Path + f.FileName;
                t.t01_type = f.Extension;
            }





            using (NXEIPEntities model = new NXEIPEntities())
            {

                //取主旨


                t.t01_subject = this.tb_subject.Text;



                //取最大值
                int max = 1;
                try
                {
                    max = (from d in model.tao01 where d.tao_no == tao_no select d.t01_no).Max();
                    max++;
                }
                catch
                {

                }

                t.t01_no = max;


                model.tao01.AddObject(t);
                model.SaveChanges();


            }

            //t.tao_model
            //通知
            this.Boardcast(t.tao_no, t.t01_no,t.t01_subject,t.t01_content);

            JsUtil.UpdateParentJs(this, "已送出回應");
        }



        else
        {
            JsUtil.AlertJs(this, msg);
        }
    }



    private String CheckField()
    {
        //討論區名稱欄位
        String msg = String.Empty;

        if (String.IsNullOrEmpty(this.tb_subject.Text))
        {
            msg += "請填入主旨\\n";
        }




        if (String.IsNullOrEmpty(this.TextBox1.Text))
        {
            msg += "請填入內容\\n";


        }

        return msg;

        //
    }


    //回應通知
    public void Boardcast(int tao_no, int t01_no, string subject, string content)
    {
        //TODO 討論區通知

        PersonalMessageUtil MsgUtil = new PersonalMessageUtil();
        int sendPeouid = int.Parse(sessionObj.sessionUserID);

        using (NXEIPEntities model = new NXEIPEntities())
        {



            //通知版主

            var manager = (from d in model.tao04 where d.tao_no == tao_no select d.peo_uid).ToList();
            //取得通知方式
            var forum = (from d in model.taolun where d.tao_no == tao_no select d).First();
            int SubscribeType = System.Convert.ToInt32(forum.tao_model, 2);
            foreach (var peo in manager)
            {
                //電子郵件 1 個人訊息 2 手機 4
                MsgUtil.SendMessage(String.Format("[新文章]{0}", subject), content, "", peo, sendPeouid, (SubscribeType & 2) == 2, (SubscribeType & 1) == 1, (SubscribeType & 4) == 4);
            }


            //通知訂閱者
            //找訂閱者
            var subs = (from d in model.tao06 where d.tao_no == tao_no && d.t06_order == "1" select d);

            foreach (var sub in subs)
            {
                int SubType = System.Convert.ToInt32(sub.t06_model, 2);
                MsgUtil.SendMessage(String.Format("[新文章]{0}", subject), content, "", sub.peo_uid, sendPeouid, (SubType & 2) == 2, (SubType & 1) == 1, (SubType & 4) == 4);

            }
           

        }
    }


}