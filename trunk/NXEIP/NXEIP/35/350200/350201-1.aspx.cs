using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using NXEIP.DAO;
using Entity;

public partial class _35_350200_350201_1 : System.Web.UI.Page
{

    private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
    

    protected void Page_Load(object sender, EventArgs e)
    {
        //編輯模式
        
        //送出後DISABLE按鈕
        this.btn_ok.Attributes.Add("onclick", "this.value='送出中...';this.disabled=true;" + 
            ClientScript.GetPostBackEventReference(this.btn_ok, "").ToString());

        if (!Page.IsPostBack)
        {
            String mode = Request.QueryString["mode"];
            String ID = Request.QueryString["ID"];

            this.hidden_typ_no.Value = ID;

            TypesDAO dao = new TypesDAO();

            if (mode != null && mode.Equals("edit"))
            {
                logger.Info("mode:edit");
                this.Navigator1.SubFunc = "修改";
                //取資料
                types t = dao.GetTypes(System.Convert.ToInt32(this.hidden_typ_no.Value));

                this.tbx_typ_number.Text = t.typ_number;
                this.tbx_typ_cname.Text = t.typ_cname;
                this.tbx_typ_order.Text = t.typ_order.ToString();
                this.tbx_typ_ename.Text = t.typ_ename;

            }
            else
            {
                //新增模式
                this.Navigator1.SubFunc = "新增";
                logger.Info("mode:new");
            }

        }

    }
    protected void btn_ok_Click(object sender, EventArgs e)
    {
        logger.Debug("OK");
        String msg = "";
        logger.Debug(Request["clientID"]);

        if (this.hidden_typ_no.Value != "")
        {
            logger.Debug("EDIT");
            Edit(this.hidden_typ_no.Value);
            msg = "修改成功";
        }
        else
        {
            Add();
            msg = "新增成功";
        }

        //呼叫UPATE()關閉此頁面 並且更新updatepanel (parent page 必須做一個UPDATE的FUNCTION)
        this.Page.ClientScript.RegisterStartupScript(typeof(_35_350200_350201_1), "closeThickBox", "self.parent.update('" + msg + "');", true);


    }

    public void Add()
    {
        SessionObject sessionObj = new SessionObject();
        String typ_code = "profess";
        String typ_number = this.tbx_typ_number.Text;
        String typ_cname = this.tbx_typ_cname.Text;
        String typ_ename = this.tbx_typ_ename.Text;
        String typ_order = this.tbx_typ_order.Text;


        //新增模式
        Entity.types newType = new Entity.types();

        newType.typ_code = typ_code;
        newType.typ_cname = typ_cname;
        newType.typ_number = typ_number;
        newType.typ_status = "1";
        newType.typ_ename = typ_ename;
        newType.typ_createtime = DateTime.Now;
        try
        {
            newType.typ_createuid = System.Convert.ToInt32(sessionObj.sessionUserID);
        }
        catch
        {

        }


        try
        {
            newType.typ_order = System.Convert.ToInt32(typ_order);
        }
        catch
        {
            newType.typ_order = 1;
        }

        //Dao
        TypesDAO dao = new TypesDAO();
        dao.AddTypes(newType);
        dao.Update();
    }



    public void Edit(String id)
    {
        TypesDAO dao = new TypesDAO();
        SessionObject sessionObj = new SessionObject();

        String typ_number = this.tbx_typ_number.Text;
        String typ_cname = this.tbx_typ_cname.Text;
        String typ_ename = this.tbx_typ_ename.Text;
        String typ_order = this.tbx_typ_order.Text;


        //新增模式
        Entity.types newType = dao.GetTypes(System.Convert.ToInt32(id));

        newType.typ_number = typ_number;
        newType.typ_cname = typ_cname;
        newType.typ_ename = typ_ename;
        newType.typ_createtime = DateTime.Now;
        try
        {
            newType.typ_createuid = System.Convert.ToInt32(sessionObj.sessionUserID);
        }
        catch
        {

        }

        try
        {
            newType.typ_order = System.Convert.ToInt32(typ_order);
        }
        catch
        {
            newType.typ_order = 1;
        }

        //Dao
        //dao.AddTypes(newType);
        dao.Update();
    }

}
