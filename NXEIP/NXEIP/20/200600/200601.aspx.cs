﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NXEIP.DAO;
using Entity;
using NLog;

public partial class _20_200600_200601 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        SessionObject sessionObj = new SessionObject();
        
        
        
        if (!this.IsPostBack)
        {
            this.ObjectDataSource_forum.SelectParameters[0].DefaultValue = sessionObj.sessionUserID;

            this.GridView1.DataBind();

        }
    }

   
      


    
}