using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class lib_calendar : System.Web.UI.UserControl
{
    public DateTime _ADDate
    {
        get
        {
            try
            {

                return new ChangeObject()._ROCtoAD(this.tbox_date.Text);
                
            }
            catch
            {
                throw new Exception("日期格式錯誤!!");
            }
        }

    }

    protected void Page_Load(object sender, EventArgs e)
    {
        ScriptManager.RegisterClientScriptInclude(this, typeof(UserControl), "calendar_js", ResolveClientUrl("~/js/calendar.js"));
        
    }
    
}
