<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeFile="100301-p.aspx.cs" Inherits="_10_100300_100301_p" %>
<%@ Register src="../../lib/CssLayout.ascx" tagname="CssLayout" tagprefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>行事曆列印</title>
     <uc1:CssLayout ID="CssLayout1" runat="server" />
     <style type="text/css">.timecss { FONT-WEIGHT: bold; HEIGHT: 24px; TEXT-DECORATION: none;font-size:13px }
	.timecss1 { FONT-WEIGHT: bold; COLOR: #000000; TEXT-DECORATION: none;font-size:13px }
	.timecss2 { BORDER-RIGHT: 1px solid; BORDER-TOP: 1px solid; FONT-WEIGHT: bold; BORDER-LEFT: 1px solid; BORDER-BOTTOM: 1px solid; TEXT-DECORATION: none;font-size:13px }
	.href1 { COLOR: #000000; TEXT-DECORATION: none;font-size:13px }
	.table1 { BORDER-RIGHT: medium none; BORDER-TOP: medium none;BORDER-LEFT: medium none; BORDER-BOTTOM: medium none; BORDER-COLLAPSE: collapse;font-size:13px}
    .style1{height: 50px;font-size:13px}
	 </style>
</head>
<body>
    <form id="form1" runat="server">
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
      <tr>
        <td>
            <asp:Label ID="lab_people" runat="server" Visible="False"></asp:Label>
            <asp:Label ID="lab_printtype" runat="server" Visible="False"></asp:Label>
            <asp:Label ID="lab_right" runat="server" Visible="False"></asp:Label>
            <asp:Label ID="lab_dep_no" runat="server" Visible="False"></asp:Label>
            <asp:Label ID="lab_today" runat="server" Visible="False"></asp:Label>
          </td>
      </tr>
      <tr>
        <td>
            <table cellspacing="0" cellpadding="3" width="100%" border="0">
                <tr align="center" style="height:30px">
                    <td align="left" width="25%">
                        <asp:Label ID="lab_date" runat="server" CssClass="timecss1"></asp:Label>&nbsp;
                        <asp:Label ID="lab_name" runat="server" CssClass="timecss1"></asp:Label>
                    </td>
                    <td align="center" width="50%">
                        <asp:Label ID="lab_Month" runat="server" CssClass="timecss1"></asp:Label>
                    </td>
                    <td align="right" width="25%">
                    </td>
                </tr>
            </table>
            <asp:Panel ID="Panel1" runat="server">
                <asp:Table ID="Table1" runat="server" CssClass="table1" bordercolor="Black" CellSpacing="0"
                    Width="100%" BorderWidth="1px" CellPadding="2">
                    <asp:TableRow BorderWidth="1px" BorderColor="Black">
                        <asp:TableCell Width="50px" HorizontalAlign="Center" Text="時間" CssClass="timecss2" Height="26"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="Center" Text="行程" CssClass="timecss2"></asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </asp:Panel>
            <asp:panel id="Panel2" runat="server">
                <table cellspacing="2" cellpadding="3" width="100%" class="table1">
                    <tr align="center">
                        <td class="timecss2" style="width:45px;height:26px">時間</td>
                        <td class="timecss2">行程</td>
                    </tr>
                    <tr>
                        <td class="timecss2" align="center" height="50">星期日</td>
                        <td class="timecss2" valign="top">
                            <asp:HyperLink ID="hl_0" runat="server" CssClass="timecss1"></asp:HyperLink><br />
                            <asp:Label ID="lab_0" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="timecss2" align="center" height="50">星期一</td>
                        <td class="timecss2" valign="top">
                            <asp:HyperLink ID="hl_1" runat="server" CssClass="timecss1"></asp:HyperLink><br />
                            <asp:Label ID="lab_1" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="timecss2" align="center" height="50">星期二</td>
                        <td class="timecss2" valign="top">
                            <asp:HyperLink ID="hl_2" runat="server" CssClass="timecss1"></asp:HyperLink><br />
                            <asp:Label ID="lab_2" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="timecss2" align="center" height="50">星期三</td>
                        <td class="timecss2" valign="top">
                            <asp:HyperLink ID="hl_3" runat="server" CssClass="timecss1"></asp:HyperLink><br />
                            <asp:Label ID="lab_3" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="timecss2" align="center" height="50">星期四</td>
                        <td class="timecss2" valign="top">
                            <asp:HyperLink ID="hl_4" runat="server" CssClass="timecss1"></asp:HyperLink><br />
                            <asp:Label ID="lab_4" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="timecss2" align="center" height="50">星期五</td>
                        <td class="timecss2" valign="top">
                            <asp:HyperLink ID="hl_5" runat="server" CssClass="timecss1"></asp:HyperLink><br />
                            <asp:Label ID="lab_5" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="timecss2" align="center" height="50">星期六</td>
                        <td class="timecss2" valign="top">
                            <asp:HyperLink ID="hl_6" runat="server" CssClass="timecss1"></asp:HyperLink><br />
                            <asp:Label ID="lab_6" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
            </asp:panel>
            <asp:panel id="Panel3" runat="server">
                <asp:Calendar ID="Calendar1" runat="server" CssClass="calendar-big"
                    Width="100%" BorderWidth="0px" CellPadding="-1" ShowTitle="False"
                    PrevMonthText="" NextMonthText="" CellSpacing="-1" 
                    DayNameFormat="Shortest" ondayrender="Calendar1_DayRender">
                    <DayStyle CssClass="Nholiday_bg"></DayStyle>
                    <DayHeaderStyle Font-Bold="True" CssClass="headtitle">
                    </DayHeaderStyle>
                    <TitleStyle Font-Bold="True">
                    </TitleStyle>
                    <WeekendDayStyle CssClass="holiday_bg"></WeekendDayStyle>
                </asp:Calendar>
            </asp:panel>
        </td>
      </tr>
      <tr>
        <td></td>
      </tr>
    </table>
    </form>
</body>
</html>
