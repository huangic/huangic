<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="100301.aspx.cs" Inherits="_10_100300_100301" %>

<%@ Register Src="../../lib/Navigator.ascx" TagName="Navigator" TagPrefix="uc1" %>
<%@ Register src="../../lib/calendar.ascx" tagname="calendar" tagprefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <uc1:Navigator ID="Navigator1" runat="server" SysFuncNo="100301" />
    <div class="PersonalCalendarLayout">
        <div class="left">
            <div class="tabHeader">
                <div class="currentTab">
                    <div class="t1">
                    </div>
                    <div class="t2">
                        日</div>
                    <div class="t3">
                    </div>
                </div>
                <div class="Tab">
                    <div class="t1">
                    </div>
                    <div class="t2">
                        <a href="100301-1.aspx">週</a></div>
                    <div class="t3">
                    </div>
                </div>
                <div class="Tab">
                    <div class="t1">
                    </div>
                    <div class="t2">
                        <a href="100301-2.aspx">月</a></div>
                    <div class="t3">
                    </div>
                </div>
                <div class="Tab">
                    <div class="t1">
                    </div>
                    <div class="t2">
                        <a href="100301-3.aspx">年</a></div>
                    <div class="t3">
                    </div>
                </div>
                <div class="Tab">
                    <div class="t1">
                    </div>
                    <div class="t2">
                        <a href="100301-4.aspx">列表</a></div>
                    <div class="t3">
                    </div>
                </div>
            </div>
            <div class="block-1">
                <div class="header">
                    <div class="h1">
                    </div>
                    <div class="h2">
                        <asp:Label ID="lab_CYM" runat="server"></asp:Label></div>
                    <div class="h3">
                    </div>
                </div>
                <asp:Calendar ID="Calendar2" runat="server" CssClass="calendar" DayNameFormat="Shortest"
                    BorderWidth="0px" CellPadding="-1" CellSpacing="-1" NextMonthText="&gt;&gt;"
                    PrevMonthText="&lt;&lt;" ShowTitle="False">
                    <OtherMonthDayStyle CssClass="othermonth" />
                    <TodayDayStyle CssClass="today" />
                    <WeekendDayStyle CssClass="holiday" />
                </asp:Calendar>
            </div>
            <div class="center">
                <span class="a-letter-2">今天是<asp:Label ID="lab_today" runat="server"></asp:Label>
                    <asp:Label ID="lab_tweek" runat="server"></asp:Label>
                </span>&nbsp;</div>
            <div class="border-bottom-block">
                <span class="icon a-letter-1">新增行事曆<br />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;事件標題
                <asp:TextBox ID="txt_title" runat="server" Columns="20"></asp:TextBox>
                <br />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;行程日期
                <uc2:calendar ID="calendar3" runat="server" /><br />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;事件時間
                <asp:DropDownList ID="ddl_stime" runat="server">
                </asp:DropDownList>&nbsp;~
                <asp:DropDownList ID="ddl_etime" runat="server">
                </asp:DropDownList> </span><br />
                <span class="a-letter-1">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="btn_submit" runat="server" CssClass="b-input" Text="確定" />&nbsp;&nbsp;
                <asp:Button ID="btn_cencel" runat="server" CssClass="a-input" Text="取消" />
            </div>
            <div class="border-bottom-block">
                <span class="icon a-letter-1">可查看之他人行事曆<br />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 部門
                <asp:DropDownList ID="ddl_QryDepart" runat="server">
                </asp:DropDownList>&nbsp;<br />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 姓名 
                <asp:DropDownList ID="ddl_QryPeople" runat="server">
                </asp:DropDownList></span>&nbsp;
                <asp:Button ID="btn_QrySubmit" runat="server" CssClass="b-input" Text="搜尋" />&nbsp;</div>
            <div class="border-bottom-block">
                <span class="icon a-letter-1">可設定之他人行事曆<br />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;姓名
                <asp:DropDownList ID="ddl_SetPeople" runat="server">
                </asp:DropDownList></span>&nbsp;
                <asp:Button ID="btn_SetSubmit0" runat="server" CssClass="b-input" Text="搜尋" />&nbsp;</div>
        </div>
        <div class="right">
            <div class="layoutDiv">
                <div class="header">
                    <div class="h1">
                    </div>
                    <div class="h2">
                        <div class="name">
                            <asp:Label ID="lab_DYM" runat="server"></asp:Label>&nbsp;/
                            <asp:Label ID="lab_DPName" runat="server"></asp:Label>
                        </div>
                        <div class="function">
                            <asp:Button ID="btn_print" runat="server" CssClass="b-input" Text="列印" />
                        </div>
                    </div>
                    <div class="h3">
                    </div>
                </div>
                <asp:Table ID="Table1" runat="server" CssClass="big-calendar-time">
                    <asp:TableRow runat="server">
                        <asp:TableCell runat="server" CssClass="title">時間</asp:TableCell>
                        <asp:TableCell runat="server">行程</asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
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
