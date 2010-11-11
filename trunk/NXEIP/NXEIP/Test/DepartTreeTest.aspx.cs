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


public partial class _Test_DepartTreeTest : System.Web.UI.Page
{
    private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
    



    protected void Page_Load(object sender, EventArgs e)
    {

        if (!this.IsPostBack)
        {
            this.DepartmentPanel1.Clear();
            this.DepartmentPanel1.Add(7);


            this.DepartTreeListBox1.Clear();

            this.DepartTreeTextBox1.Clear();


        }

       
        



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
}
