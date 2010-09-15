using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _10_100300_100301 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
        }
    }

    #region 日曆建立時
    protected void Calendar1_DayRender(object sender, DayRenderEventArgs e)
    {
        try
        {
            if (e.Day.IsOtherMonth)
            {
                #region 其他月份
                if (e.Day.IsWeekend) e.Cell.CssClass = "othermonthholiday"; //假日

                //e.Cell.Text = "<a href=\"cal0101.aspx?today=" + lib.WealthyBR.ADDTtoROCDT(e.Day.Date.ToString("yyyy-MM-dd")) + "&peo_uid=" + this.lab_peo_uid.Text + "&depart=" + this.ddl_depart.SelectedValue + "\"class=\"no_\">" + e.Day.Date.Day.ToString() + "</a>";
                #endregion
            }
            else
            {
                #region 本月
                //e.Cell.Text = "<a href=\"cal0101.aspx?today=" + lib.WealthyBR.ADDTtoROCDT(e.Day.Date.ToString("yyyy-MM-dd")) + "&peo_uid=" + this.lab_peo_uid.Text + "&depart=" + this.ddl_depart.SelectedValue + "\"class=\"no_\"><font color=#E0E0E0>" + e.Day.Date.Day.ToString() + "</font></a>";
                #endregion
            }
        }
        catch (Exception ex)
        {
            string aMSG = "功能名稱:個人行事曆/本月日曆初始化<br>錯誤訊息:" + ex.ToString();
            Response.Write(aMSG);
        }
    }
    #endregion
}