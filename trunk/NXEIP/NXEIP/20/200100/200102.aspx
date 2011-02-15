<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="200102.aspx.cs" Inherits="_20_200100_200102" EnableEventValidation="false" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxtoolkit" %>
<%@ Register Src="../../lib/Navigator.ascx" TagName="Navigator" TagPrefix="uc1" %>
<%@ Register Src="../../lib/calendar.ascx" TagName="calendar" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<ajaxtoolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" 
        CombineScripts="False">
    </ajaxtoolkit:ToolkitScriptManager>
    <uc1:Navigator ID="Navigator1" runat="server" SysFuncNo="200102" />
    <div class="PersonalCalendarLayout">
        <div class="left">
            <div class="tabHeader">
                <div class="tabHead">
                    <ul>
                        <li><asp:HyperLink ID="current" runat="server" NavigateUrl="200102.aspx">月</asp:HyperLink></li>
                        <li><asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="200102-1.aspx">週</asp:HyperLink></li>
                        <li><asp:HyperLink ID="HyperLink3" runat="server" NavigateUrl="200102-2.aspx">日</asp:HyperLink></li>
                    </ul>
                </div>
            </div>
            <div class="block-1">
                <div class="header">
                    <div class="h1"><asp:HyperLink ID="hl_Pre" runat="server" NavigateUrl="?todays=2009-01-01"><span>箭頭</span></asp:HyperLink></div>
                    <div class="h2"><asp:Label ID="lab_CYM" runat="server" CssClass="name">99年</asp:Label></div>
                    <div class="h3"><asp:HyperLink ID="hl_Nxt" runat="server" NavigateUrl="?todays=2011-01-01"><span>箭頭</span></asp:HyperLink></div>
                </div>
                <div class="block-0">
                    <asp:Table ID="Table1" runat="server" CssClass="calendar-smonth">
                        <asp:TableRow ID="row0" runat="server">
                            <asp:TableCell ID="TableCell1" runat="server">1月</asp:TableCell>
                            <asp:TableCell ID="TableCell2" runat="server">2月</asp:TableCell>
                            <asp:TableCell ID="TableCell3" runat="server">3月</asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow ID="row1" runat="server">
                            <asp:TableCell ID="TableCell4" runat="server">4月</asp:TableCell>
                            <asp:TableCell ID="TableCell5" runat="server">5月</asp:TableCell>
                            <asp:TableCell ID="TableCell6" runat="server">6月</asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow ID="row2" runat="server">
                            <asp:TableCell ID="TableCell7" runat="server">7月</asp:TableCell>
                            <asp:TableCell ID="TableCell8" runat="server">8月</asp:TableCell>
                            <asp:TableCell ID="TableCell9" runat="server">9月</asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow ID="row3" runat="server">
                            <asp:TableCell ID="TableCell10" runat="server">10月</asp:TableCell>
                            <asp:TableCell ID="TableCell11" runat="server">11月</asp:TableCell>
                            <asp:TableCell ID="TableCell12" runat="server">12月</asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                </div>
            </div>
            <div class="center">
                <span class="a-letter-2">
                    <asp:Label ID="lab_today" runat="server">今天是 99-10-20 星期三</asp:Label></span>
            </div>
            <div class="block-2">
                <div class="border-bottom-block">
                    <div class="header">
                        <div class="h1"></div>
                        <div class="h2 a-letter-1">我要預約的首長</div>
                    </div>
                    <div class="headerW">
                        <div class="h1 a-letter-1">姓名</div>
                        <div class="h2">
                            <asp:DropDownList ID="ddl_leading" runat="server">
                            </asp:DropDownList>
                            <ajaxtoolkit:cascadingdropdown ID="ddl_leading_CascadingDropDown" runat="server" 
                            Category="leading" LoadingText="讀取中..." 
                                ServiceMethod="GetLeading" ServicePath="../../WebService/calendar.asmx" UseContextKey="True"
                                TargetControlID="ddl_leading">
                            </ajaxtoolkit:cascadingdropdown>
                        </div>
                        <div class="h3">
                            <asp:Button ID="btn_Change" runat="server" CssClass="b-input" Text="搜尋" 
                                OnClick="btn_Change_Click" /></div>
                    </div>
                </div>
            </div>
            <div class="block-2">
                <div class="border-bottom-block">
                    <div class="header">
                        <div class="h1"></div>
                        <div class="h2 a-letter-1">我的預約記錄</div>
                    </div>
                </div>
            </div>
        </div>
        <div class="right">
            <div class="layoutDiv">
                <div class="header">
                    <div class="h1">
                    </div>
                    <div class="h2">
                        <div class="name">
                            <asp:Label ID="lab_show" runat="server"></asp:Label>&nbsp;/
                            <asp:Label ID="lab_name" runat="server"></asp:Label>
                            <asp:Label ID="lab_people" runat="server" Visible="False"></asp:Label>
                            <asp:Label ID="lab_date" runat="server" Visible="False"></asp:Label>
                        </div>
                    </div>
                    <div class="h3">
                    </div>
                </div>
                <asp:Calendar ID="Calendar1" runat="server" CssClass="calendar-big" DayNameFormat="Shortest"
                    BorderWidth="0px" CellPadding="-1" CellSpacing="-1" NextMonthText="" PrevMonthText=""
                    OnDayRender="Calendar1_DayRender" ShowTitle="False">
                    <DayHeaderStyle CssClass="headtitle" />
                    <DayStyle CssClass="Nholiday_bg" />
                    <WeekendDayStyle CssClass="holiday_bg" />
                </asp:Calendar>
                <div class="footer">
                    <div class="f1">
                    </div>
                    <div class="f2">
                    </div>
                    <div class="f3">
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
