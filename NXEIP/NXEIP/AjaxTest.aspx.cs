using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using IMMENSITY.SWFUploadAPI;
using System.Runtime.Serialization.Json;
using System.Data.SqlClient;
using Microsoft.Practices.EnterpriseLibrary.Caching;
using NXEIP.Lib;
using Entity;
using NXEIP.MyGov;


public partial class AjaxTest : System.Web.UI.Page
{
    private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
    



    protected void Page_Load(object sender, EventArgs e)
    {

        if (!this.IsPostBack)
        {
            this.DepartmentPanel1.Clear();
            this.DepartmentPanel1.Add(7);
            this.DepartTreeListBox1.Add(1);


            String[] array={"1","2"};

        
        
        }

        //測試CACHE 項目

        



    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        //取TREE 值

        logger.Debug(this.DepartmentPanel1.Items);
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        logger.Debug(this.DepartTreeListBox1.Items);
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        //db check

        NXEIPEntities model = new NXEIPEntities();
        //model.departments.
        this.TextBox1.Text=model.CreateDatabaseScript();

    }
    protected void Button4_Click(object sender, EventArgs e)
    {
        String msg = "";
        msg=MyMessageUtil.send("待審核最新消息", "cougar", "你有一件待審核資料<br/>", DateTime.Now, DateTime.Now, "", "", EIPGroup.EIP_Todo_VerifyNew);
        logger.Debug(msg);
    }
}
