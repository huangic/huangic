using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NXEIP.Widget;

public partial class widget_00_CurrentUserAndTime : WidgetBaseControl
{
    //編修用
    public override String EditPanel { get { return "Panel1"; } }
    
    
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            this.TextBox1.Text = this.WidgetParam["BBB"];
        }
        catch
        {

        }
    }

    //畫面class名稱
    public override string Name
    {
        get { return "UserAndTime"; }
    }

    //實作頁面
    public override void loadWidget()
    {
       //init Time;
        try
        {

            this.Literal1.Text = this.WidgetParam["BBB"];
        }
        catch { 
        
        }

        this.Literal2.Text = DateTime.Now.ToShortDateString();
    }
}
