﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using NXEIP.DAO;
using Entity;

public partial class lib_Autocomplete : System.Web.UI.UserControl
{
    public static string SrcUrl;

    protected void Page_Load(object sender, EventArgs e)
    {
        SrcUrl = this.ResolveClientUrl("~/lib/AutoComplete/ACDataSrc.aspx");
    }

    /// <summary>
    /// 回傳 部門,姓名,workid,peo_uid,dep_no
    /// </summary>
    /// <returns></returns>
    public string GetValue()
    {
        return this.hidd_value.Value;
    }
}