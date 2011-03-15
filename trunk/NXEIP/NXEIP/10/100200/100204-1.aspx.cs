using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NXEIP.DAO;
using Entity;
using NLog;
using IMMENSITY.SWFUploadAPI;
using System.Runtime.Serialization.Json;
using NXEIP.MyGov;

public partial class _10_100200_100204_1 : System.Web.UI.Page
{
    private NXEIPEntities model = new NXEIPEntities();
    private Logger logger = LogManager.GetCurrentClassLogger();

    protected void Page_Load(object sender, EventArgs e)
    {
        //檔案上傳元件設定
        int size = 0;
        int.TryParse(new ArgumentsObject().Get_argValue("100204_size"), out size);
        this.UC_SWFUpload1.SwfUploadInfo = new SWFUploadInfo()
        {
            UploadMode = UpMode.LIST,
            File_size_limit = size,
            SubmitButtonId = this.Button1.ClientID,
            Path = "/upload/100204/",
            PathArg = "100204_dir"

        };


        if (!this.IsPostBack)
        {
            
            //類別
            this.ObjectDataSource1.SelectParameters["sfu_no"].DefaultValue = "100204";
            
            this.lb_size.Text = String.Format("(單一檔案限制{0}MB)", size);

            UtilityDAO dao = new UtilityDAO();

            //發佈日期
            this.calendar1._ADDate = DateTime.Now;

            //期限
            this.calendar2._ADDate = DateTime.Now;
            this.calendar3._ADDate = DateTime.Now;

            if (Request.QueryString["mode"].Equals("edit"))
            {
                this.Navigator1.SubFunc = "修改";

                this.hidd_no.Value = Request.QueryString["n01_no"];
                int n01_no = int.Parse(this.hidd_no.Value);
                new01 data = (from d in model.new01 where d.n01_no == n01_no select d).FirstOrDefault();

                this.lab_people.Text = dao.Get_PeopleName(data.n01_peouid.Value);
                this.lab_dep.Text = dao.Get_DepartmentName(data.n01_depno.Value);

                this.calendar1._ADDate = data.n01_date.Value;
                
                //有無期限 1:沒有 2:有
                if (data.n01_deadline != null)
                {
                    this.rbl_line.SelectedItem.Selected = false;
                    this.rbl_line.Items.FindByValue(data.n01_deadline).Selected = true;

                    //有期限
                    if (data.n01_deadline == "2" && data.n01_sdate.HasValue && data.n01_edate.HasValue)
                    {
                        this.calendar2._ADDate = data.n01_sdate.Value;
                        this.calendar3._ADDate = data.n01_edate.Value;
                    }
                }

                //置頂
                if (data.n01_top != null)
                {
                    this.rbl_top.SelectedItem.Selected = false;
                    this.rbl_top.Items.FindByValue(data.n01_top).Selected = true;
                }
                
                this.ddl_sys06.DataBind();
                if (data.s06_no.HasValue)
                {
                    this.ddl_sys06.Items.FindByValue(data.s06_no.Value.ToString()).Selected = true;
                }
                this.tbox_subject.Text = data.n01_subject;
                this.rbl_use.Items.FindByValue(data.n01_use).Selected = true;
                this.tbox_content.Text = data.n01_content;

                //相關連結
                var n03data = (from d in model.new03 where d.n01_no == n01_no select d);
                int t = 1;
                ContentPlaceHolder mpContentPlaceHolder = (ContentPlaceHolder)Master.FindControl("ContentPlaceHolder1");
                foreach (var x in n03data)
                {
                    ((TextBox)mpContentPlaceHolder.FindControl("tbox_http_" + t)).Text = x.n03_address;
                    t++;
                }

                //檔案
                this.ObjectDataSource2.SelectParameters["n01_no"].DefaultValue = this.hidd_no.Value;

            }
            else
            {
                this.Navigator1.SubFunc = "新增";
                this.div_file.Style.Add("display", "none");

                SessionObject sobj = new SessionObject();

                this.lab_people.Text = dao.Get_PeopleName(int.Parse(sobj.sessionUserID));
                this.lab_dep.Text = dao.Get_DepartmentName(int.Parse(sobj.sessionUserDepartID));
                
                
            }
        }
    }


    protected void Button1_Click(object sender, EventArgs e)
    {
        if (this.checkInput())
        {
            SessionObject sobj = new SessionObject();

            if (this.hidd_no.Value != "")
            {
                int n01_no = int.Parse(this.hidd_no.Value);
                new01 data = (from d in model.new01 where d.n01_no == n01_no select d).FirstOrDefault();

                if (data.n01_status.Equals("2"))
                {
                    //審核未通過,新增一筆資料,保留舊資料

                    if (SaveData())
                    {
                        //加入原有檔案
                        int new_n01no = int.Parse(this.hidd_newno.Value);
                        int max_no = 1;
                        try
                        {
                            max_no = (from d in model.new02 where d.n01_no == new_n01no select d.n02_no).Max() + 1;
                        }
                        catch { }
                        var od = (from d in model.new02 where d.n01_no == n01_no select d);
                        foreach (var of in od)
                        {
                            new02 nd = new new02();
                            nd.n01_no = new_n01no;
                            nd.n02_file = of.n02_file;
                            nd.n02_path = of.n02_path;
                            nd.n02_type = of.n02_type;
                            nd.n02_no = max_no++;
                            model.new02.AddObject(nd);
                        }
                        model.SaveChanges();

                        this.ShowMsg_URL("新增完成!", this.GetURL());
                    }
                    else
                    {
                        this.ShowMsg("系統發生錯誤!!");
                    }


                }
                else
                {
                    //修改
                    data.n01_content = this.tbox_content.Text.Trim();
                    data.n01_subject = this.tbox_subject.Text.Trim();
                    data.n01_use = this.rbl_use.SelectedValue;
                    data.n01_createuid = int.Parse(sobj.sessionUserID);
                    data.n01_createtime = DateTime.Now;
                    if (!this.ddl_sys06.SelectedValue.Equals("0"))
                    {
                        data.s06_no = int.Parse(this.ddl_sys06.SelectedValue);
                    }

                    //發佈日期
                    data.n01_date = this.calendar1._ADDate;

                    //是否置頂
                    data.n01_top = this.rbl_top.SelectedValue;

                    //期限
                    data.n01_deadline = this.rbl_line.SelectedValue;
                    if (this.rbl_line.SelectedValue == "2")
                    {
                        data.n01_sdate = this.calendar2._ADDate;
                        data.n01_edate = this.calendar3._ADDate;
                    }
                    else
                    {
                        data.n01_sdate = this.calendar1._ADDate;
                        data.n01_edate = this.calendar1._ADDate.AddYears(20);
                    }

                    model.SaveChanges();

                    //刪除舊有相關連結
                    var hdata = (from n03 in model.new03 where n03.n01_no == n01_no select n03);
                    foreach (var h in hdata)
                    {
                        model.new03.DeleteObject(h);
                    }
                    model.SaveChanges();

                    //新增相關連結
                    this.SaveLink(n01_no);

                    //存檔案
                    this.SaveFile(n01_no);

                    OperatesObject.OperatesExecute(100204, sobj.sessionUserID,3, "更新最新消息 n01_no:" + n01_no);



                    this.ShowMsg_URL("修改完成!", this.GetURL());
                }
            }
            else
            {
                //儲存新資料
                if (SaveData())
                {
                    this.ShowMsg_URL("新增完成!", this.GetURL());
                }
                else
                {
                    this.ShowMsg("系統發生錯誤!!");
                }
            }

        }
    }

    private bool SaveData()
    {
        try
        {
            SessionObject sobj = new SessionObject();

            new01 data = new new01();
            data.n01_content = this.tbox_content.Text.Trim();
            data.n01_subject = this.tbox_subject.Text.Trim();
            data.n01_use = this.rbl_use.SelectedValue;
            data.n01_peouid = int.Parse(sobj.sessionUserID);
            data.n01_depno = int.Parse(sobj.sessionUserDepartID);
            data.n01_date = this.calendar1._ADDate;
            data.n01_createuid = int.Parse(sobj.sessionUserID);
            data.n01_createtime = DateTime.Now;
            if (!this.ddl_sys06.SelectedValue.Equals("0"))
            {
                data.s06_no = int.Parse(this.ddl_sys06.SelectedValue);
            }
            //是否需審核 1:是 2:否
            string ischeck = new ArgumentsObject().Get_argValue("100204_ischeck");

            //全府
            if (this.rbl_use.SelectedValue == "2")
            {
                if (ischeck.Equals("1"))
                {
                    data.n01_status = "3";
                }
                else
                {
                    data.n01_status = "1";
                }
            }
            else
            {
                data.n01_status = "1";
            }

            //是否置頂
            data.n01_top = this.rbl_top.SelectedValue;

            //期限
            data.n01_deadline = this.rbl_line.SelectedValue;
            if (this.rbl_line.SelectedValue == "2")
            {
                data.n01_sdate = this.calendar2._ADDate;
                data.n01_edate = this.calendar3._ADDate;
            }
            else
            {
                data.n01_sdate = this.calendar1._ADDate;
                data.n01_edate = this.calendar1._ADDate.AddYears(20);
            }
            

            model.new01.AddObject(data);
            model.SaveChanges();

            int n01_no = data.n01_no;
            this.hidd_newno.Value = n01_no.ToString();

            //發送訊息至審核人
            if (ischeck.Equals("1"))
            {
                int me = int.Parse(sobj.sessionUserID);
                int dep_no = int.Parse(sobj.sessionUserDepartID);

                string body = string.Format("{0}欲發佈至全府之最新消息，類別「{1}」，標題「{2}」，請您至最新消息審核功能進行審核", sobj.sessionUserName, this.ddl_sys06.SelectedItem.Text, this.tbox_subject.Text.Trim());

                PersonalMessageUtil msg = new PersonalMessageUtil();

                //單位管理者
                var m = (from d in model.manager where d.dep_no == dep_no && d.man_type == "1" select d);

                foreach (var d in m)
                {
                    msg.SendMessage("最新消息審核", body, "", d.peo_uid, me, true, false, false);
                }
            }

            //相關連結
            this.SaveLink(n01_no);

            //存檔案
            this.SaveFile(n01_no);

            OperatesObject.OperatesExecute(100204, sobj.sessionUserID, 1, "新增最新消息 n01_no:" + n01_no);

            //發送訊息至E公務平台 (送至審核人Account)
            if (ischeck.Equals("1"))
            {
                MyMessageUtil.send(this.tbox_subject.Text.Trim(), "cougar", this.tbox_content.Text.Trim(), "", "", EIPGroup.EIP_Todo_VerifyNew);
            }

            return true;
        }
        catch (System.Exception ex)
        {
            logger.Debug(ex.ToString());
            return false;
        }

    }

    private void SaveFile(int n01_no)
    {
        foreach (var f in UC_SWFUpload1.SWFUploadFileInfoList)
        {
            int max_no = 1;
            try
            {
                max_no = (from d in model.new02 where d.n01_no == n01_no select d.n02_no).Max() + 1;
            }
            catch { }

            new02 dd = new new02();
            dd.n01_no = n01_no;
            dd.n02_no = max_no;
            dd.n02_path = f.Path + f.FileName;
            dd.n02_file = f.OriginalFileName;
            dd.n02_type = f.Extension;

            model.new02.AddObject(dd);
            model.SaveChanges();
        }
    }

    private void SaveLink(int n01_no)
    {
        ContentPlaceHolder mpContentPlaceHolder = (ContentPlaceHolder)Master.FindControl("ContentPlaceHolder1");
        for (int i = 1; i <= 5; i++)
        {
            string http = ((TextBox)mpContentPlaceHolder.FindControl("tbox_http_" + i)).Text;
            if (http.Contains("http://") && http.Length > 7)
            {
                new03 dd = new new03();
                dd.n01_no = n01_no;
                dd.n03_no = i;
                dd.n03_address = http;
                model.new03.AddObject(dd);
                model.SaveChanges();
            }
        }
    }

    private bool checkInput()
    {
        if (this.tbox_subject.Text.Trim().Length == 0)
        {
            this.ShowMsg("請輸入標題!");
            return false;
        }

        if (this.tbox_content.Text.Trim().Length == 0)
        {
            this.ShowMsg("請輸入消息內容!");
            return false;
        }

        try
        {
            DateTime tmp = this.calendar1._ADDate;
        }
        catch
        {
            this.ShowMsg("請輸入正確發佈日期!");
            return false;
        }

        //期限
        if (rbl_line.SelectedValue == "2")
        {
            try
            {
                DateTime tmps = Convert.ToDateTime(this.calendar2._ADDate.ToString("yyyy-MM-dd"));
                DateTime tmpe = Convert.ToDateTime(this.calendar3._ADDate.ToString("yyyy-MM-dd"));
                if (tmps > tmpe)
                {
                    this.ShowMsg("開始日期需早於結束日期!");
                    return false;
                }
            }
            catch
            {
                this.ShowMsg("請輸入正確期限日期!");
                return false;
            }
        }

        //相關連結
        ContentPlaceHolder mpContentPlaceHolder = (ContentPlaceHolder)Master.FindControl("ContentPlaceHolder1");
        for (int i = 1; i <= 5; i++)
        {
            string http = ((TextBox)mpContentPlaceHolder.FindControl("tbox_http_" + i)).Text;
            
            if (!http.Equals("http://"))
            {
                if (!http.Contains("http://"))
                {
                    this.ShowMsg("請輸入正確網址!");
                    return false;
                }
            }
        }

        return true;
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        SWFUploadFile uf = new SWFUploadFile();

        foreach (var f in UC_SWFUpload1.SWFUploadFileInfoList)
        {
            String del_msg = uf.Delete(f.Path, f.FileName, true);
            //logger.Debug(del_msg);
        }

        Response.Redirect(this.GetURL());
    }

    protected void ListView1_ItemCommand(object sender, ListViewCommandEventArgs e)
    {
        int index = Convert.ToInt32(e.Item.DisplayIndex);

        if (e.CommandName.Equals("del"))
        {
            int n01_no = int.Parse(this.ListView1.DataKeys[index].Values[0].ToString());
            int n02_no = int.Parse(this.ListView1.DataKeys[index].Values[1].ToString());

            new02 d = (from x in model.new02 where x.n01_no == n01_no && x.n02_no == n02_no select x).FirstOrDefault();

            model.new02.DeleteObject(d);
            model.SaveChanges();
            this.ListView1.DataBind();
        }
    }

    private void ShowMsg(string msg)
    {
        JsUtil.AlertJs(this, msg);
    }

    private void ShowMsg_URL(string msg, string url)
    {
        //string script = "<script>window.alert('" + msg + "');location.replace('" + url + "')</script>";
        //this.ClientScript.RegisterStartupScript(this.GetType(), "MyScript", script);
        JsUtil.AlertAndRedirectJs(this, msg, url);
    }

    private string GetURL()
    {
        return "100204.aspx?status=" + Request.QueryString["status"] + "&pageIndex=" + Request.QueryString["pageIndex"];
    }
}