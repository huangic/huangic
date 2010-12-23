<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeFile="300405-2.aspx.cs" Inherits="_30_300400_300405_2" %>
<%@ Register src="../../lib/CssLayout.ascx" tagname="CssLayout" tagprefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>場地使用記錄列印</title>
     <uc1:CssLayout ID="CssLayout1" runat="server" />
     <style type="text/css">.timecss { FONT-WEIGHT: bold; HEIGHT: 24px; TEXT-DECORATION: none;font-size:13px }
	.timecss1 { FONT-WEIGHT: bold; COLOR: #000000; TEXT-DECORATION: none;font-size:13px }
	.timecss2 { BORDER-RIGHT: 1px solid; BORDER-TOP: 1px solid; FONT-WEIGHT: bold; BORDER-LEFT: 1px solid; BORDER-BOTTOM: 1px solid; TEXT-DECORATION: none;font-size:13px }
	.href1 { COLOR: #000000; TEXT-DECORATION: none;font-size:13px }
	.table1 { BORDER-RIGHT: medium none; BORDER-TOP: medium none;BORDER-LEFT: medium none; BORDER-BOTTOM: medium none; BORDER-COLLAPSE: collapse;font-size:13px}
     </style>
</head>
<body onload="print();">
    <form id="form1" runat="server">
    <asp:Label ID="lab_printtype" runat="server" Visible="False"></asp:Label>
    <asp:Label ID="lab_today" runat="server" Visible="False"></asp:Label>
    <asp:Label ID="lab_spot" runat="server" Visible="False"></asp:Label>
    <asp:Label ID="lab_rooms" runat="server" Visible="False"></asp:Label>
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
      <tr>
        <td>
            <table cellspacing="0" cellpadding="3" width="100%" border="0">
                <tr align="center" style="height:30px">
                    <td align="center">
                        <asp:Label ID="lab_Month" runat="server" CssClass="timecss1"></asp:Label>
                    </td>
                </tr>
            </table>
            <asp:panel id="Panel1" runat="server">
                <table cellspacing="2" cellpadding="3" class="calendar-bigweek">
                    <tbody>
                        <tr>
                            <td class="title_time_bg">
                                <span class="title_time">星期</span>
                            </td>
                            <td class="title_schedule_bg">
                                <span class="title_time">申請記錄</span>
                            </td>
                        </tr>
                        <tr>
                            <td class="title_time_bg"><span class="title_time">日</span></td>
                            <td class="row_holiday_bg">
                                <asp:HyperLink ID="hl_0" runat="server" CssClass="row_scheduleT"></asp:HyperLink><br />
                                <asp:Label ID="lab_0" runat="server" CssClass="row_schedule"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="title_time_bg"><span class="title_time">一</span></td>
                            <td class="row_Nholiday_bg">
                                <asp:HyperLink ID="hl_1" runat="server" CssClass="row_scheduleT"></asp:HyperLink><br />
                                <asp:Label ID="lab_1" runat="server" CssClass="row_schedule"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="title_time_bg"><span class="title_time">二</span></td>
                            <td class="row_Nholiday_bg">
                                <asp:HyperLink ID="hl_2" runat="server" CssClass="row_scheduleT"></asp:HyperLink><br />
                                <asp:Label ID="lab_2" runat="server" CssClass="row_schedule"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="title_time_bg"><span class="title_time">三</span></td>
                            <td class="row_Nholiday_bg">
                                <asp:HyperLink ID="hl_3" runat="server" CssClass="row_scheduleT"></asp:HyperLink><br />
                                <asp:Label ID="lab_3" runat="server" CssClass="row_schedule"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="title_time_bg"><span class="title_time">四</span></td>
                            <td class="row_Nholiday_bg">
                                <asp:HyperLink ID="hl_4" runat="server" CssClass="row_scheduleT"></asp:HyperLink><br />
                                <asp:Label ID="lab_4" runat="server" CssClass="row_schedule"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="title_time_bg"><span class="title_time">五</span></td>
                            <td class="row_Nholiday_bg">
                                <asp:HyperLink ID="hl_5" runat="server" CssClass="row_scheduleT"></asp:HyperLink><br />
                                <asp:Label ID="lab_5" runat="server" CssClass="row_schedule"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="title_time_bg"><span class="title_time">六</span></td>
                            <td class="row_holiday_bg">
                                <asp:HyperLink ID="hl_6" runat="server" CssClass="row_scheduleT"></asp:HyperLink><br />
                                <asp:Label ID="lab_6" runat="server" CssClass="row_schedule"></asp:Label>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </asp:panel>
            <asp:panel id="Panel2" runat="server">
                <asp:Calendar ID="Calendar1" runat="server" CssClass="calendar-bigmonth"
                    Width="100%" BorderWidth="0px" CellPadding="-1" ShowTitle="False"
                    PrevMonthText="" NextMonthText="" CellSpacing="-1" 
                    DayNameFormat="Shortest" ondayrender="Calendar1_DayRender">
                    <DayStyle CssClass="Nholiday_bg"></DayStyle>
                    <DayHeaderStyle Font-Bold="True" CssClass="head">
                    </DayHeaderStyle>
                    <TitleStyle Font-Bold="True">
                    </TitleStyle>
                    <WeekendDayStyle CssClass="holiday_bg"></WeekendDayStyle>
                </asp:Calendar>
            </asp:panel>
        </td>
      </tr>
    </table>
    </form>
</body>
</html>
