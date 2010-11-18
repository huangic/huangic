﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entity;
using System.Web.UI.HtmlControls;
using NXEIP.Widget;





public partial class _10_100500_100502 : WidgetPageTemplate
{

    //需要OVERRIDE 

    //此頁面使用的SESSION;
    public override String SessionName { get { return "DepartWidgetObj"; } }
   
    //遠端AJAX使用的頁面
    protected override String RemoteUrl { get { return "~/widget/WidgetMethod.aspx"; } }
    /// <summary>
    /// 頁面形態
    /// </summary>
    public override String PageType { get { return "D"; } }
    /// <summary>
    /// 頁面UID (不是PAGE_NO)
    /// </summary>
    public override string Uid
    {
        get { return new SessionObject().sessionUserDepartID; }
    }

    protected override bool IsEditable
    {
        get { return false; }
    }


    protected override int GetCurrentPage()
    {
        //取自己的頁面 如果沒有就抓預設值的範本頁面


        return 1;
    }

   
}
