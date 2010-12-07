using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entity;
using System.Web.UI.HtmlControls;
using NXEIP.Widget;





public partial class _10_100500_100510 : WidgetPageTemplate
{   

    //需要OVERRIDE 

    //此頁面使用的SESSION;
    public override String SessionName { get { return "WidgetObj"; } }
   
    //遠端AJAX使用的頁面
    protected override String RemoteUrl { get { return "~/widget/WidgetMethod.aspx"; } }
    /// <summary>
    /// 頁面形態
    /// </summary>
    public override String PageType { get { return "U"; } }
    /// <summary>
    /// 頁面UID (不是PAGE_NO)
    /// </summary>
    public override string Uid
    {
        get { return "0" }
    }

    protected override bool IsEditable{get{return true;}}

   
}
