using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entity;
using log4net;
using System.Web.UI.HtmlControls;
using NXEIP.Widget;





public partial class _10_100500_100503 : WidgetPageTemplate
{

    //需要OVERRIDE 

    //此頁面使用的SESSION;
    public override String SessionName { get { return "UnitWidgetObj"; } }
    //此頁面使用的編修用SESSION
    public override String SessionTmpName { get { return "TmpUnitWidgetObj"; } }
    //遠端AJAX使用的頁面
    protected override String RemoteUrl { get { return "~/widget/WidgetMethod.aspx"; } }
    /// <summary>
    /// 頁面形態
    /// </summary>
    public override String PageType { get { return "U"; } }
    /// <summary>
    /// 頁面UID (不是PAGE_NO) 全府沒得判斷直接給他0
    /// </summary>
    public override string Uid
    {
        get { return "0"; }
    }
    
   




   
}
