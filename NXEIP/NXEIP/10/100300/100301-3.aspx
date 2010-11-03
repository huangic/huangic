<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="100301-3.aspx.cs" Inherits="_10_100300_100301_3" %>

<%@ Register Src="../../lib/Navigator.ascx" TagName="Navigator" TagPrefix="uc1" %>
<%@ Register Src="../../lib/calendar.ascx" TagName="calendar" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <uc1:Navigator ID="Navigator1" runat="server" />
    <div class="PersonalCalendarLayout">
        <div class="left">
            <div class="tabHeader">
                <div class="tabHead">
                    <ul>
                        <li><asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="100301.aspx">日</asp:HyperLink></li>
                        <li><asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="100301-1.aspx">週</asp:HyperLink></li>
                        <li><asp:HyperLink ID="HyperLink3" runat="server" NavigateUrl="100301-2.aspx">月</asp:HyperLink></li>
                        <li><asp:HyperLink ID="current" runat="server" NavigateUrl="100301-3.aspx">年</asp:HyperLink></li>
                        <li><asp:HyperLink ID="HyperLink5" runat="server" NavigateUrl="100301-4.aspx">列表</asp:HyperLink></li>
                    </ul>
                </div>
            </div>
            <div class="block-1">
                <div class="header">
                    <div class="h1"><asp:HyperLink ID="hl_Pre" runat="server" NavigateUrl="?todays=2009-01-01"><span>箭頭</span></asp:HyperLink></div>
                    <div class="h2"><asp:Label ID="lab_CYM" runat="server" CssClass="name">2010年</asp:Label></div>
                    <div class="h3"><asp:HyperLink ID="hl_Nxt" runat="server" NavigateUrl="?todays=2011-01-01"><span>箭頭</span></asp:HyperLink></div>
                </div>
            </div>
            <div class="center">
                <span class="a-letter-2"><asp:Label ID="lab_today" runat="server">今天是 99-10-20 星期三</asp:Label></span>
            </div>
            <asp:Panel ID="Panel1" runat="server">
            <div class="block-2">
                <div class="border-bottom-block">
                    <div class="header">
                        <div class="h1"></div>
                        <div class="h2 a-letter-1">新增行事曆</div>
                    </div>
                    <div class="headerW">
                        <div class="h1 a-letter-1">事件標題</div>
                        <div class="h2"><asp:TextBox ID="txt_title" runat="server" Columns="20"></asp:TextBox></div>
                    </div>
                    <div class="headerW">
                        <div class="h1 a-letter-1">行程日期</div>
                        <div class="h2"><uc2:calendar ID="cl_date" runat="server" _Show="False" /></div>
                    </div>
                    <div class="headerW">
                        <div class="h1 a-letter-1">事件時間</div>
                        <div class="h2">
                            <asp:DropDownList ID="ddl_stime" runat="server">
                            </asp:DropDownList>
                        </div>
                        <div class="hd a-letter-1">&nbsp;</div>
                        <div class="h2">
                            <asp:DropDownList ID="ddl_etime" runat="server">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="headerW">
                        <div class="h1"></div>
                        <div class="h3"></div>
                        <div class="h3"><asp:Button ID="btn_submit" runat="server" CssClass="b-input" Text="確定" OnClick="btn_submit_Click" /></div>
                    </div>
                </div>
            </div>
            </asp:Panel>
            <div class="block-2">
                <div class="border-bottom-block">
                    <div class="header">
                        <div class="h1"></div>
                        <div class="h2 a-letter-1">可查看之他人行事曆</div>
                    </div>
                    <div class="headerW">
                        <div class="h1 a-letter-1">部門</div>
                        <div class="h2">
                            <asp:DropDownList ID="ddl_QryDepart" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddl_QryDepart_SelectedIndexChanged">
                            </asp:DropDownList>
                        </div>
                        <div class="h3"></div>
                    </div>
                    <div class="headerW">
                        <div class="h1 a-letter-1">姓名</div>
                        <div class="h2">
                            <asp:DropDownList ID="ddl_QryPeople" runat="server">
                            </asp:DropDownList>
                        </div>
                        <div class="h3">
                            <asp:Button ID="btn_QrySubmit" runat="server" CssClass="b-input" Text="搜尋" OnClick="btn_QrySubmit_Click" /></div>
                    </div>
                </div>
            </div>
            <div class="block-2">
                <div class="border-bottom-block">
                    <div class="header">
                        <div class="h1"></div>
                        <div class="h2 a-letter-1">可設定之他人行事曆</div>
                    </div>
                    <div class="headerW">
                        <div class="h1 a-letter-1">姓名</div>
                        <div class="h2">
                            <asp:DropDownList ID="ddl_c01" runat="server">
                            </asp:DropDownList>
                        </div>
                        <div class="h3">
                            <asp:Button ID="btn_SetSubmit0" runat="server" CssClass="b-input" Text="搜尋" OnClick="btn_SetSubmit0_Click" /></div>
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
                        <div class="function">
                           <ul>
                             <li><asp:LinkButton ID="btn_back" runat="server" CssClass="b-back" onclick="btn_back_Click">返回使用者</asp:LinkButton></li>
                           </ul>
                        </div>
                    </div>
                    <div class="h3">
                    </div>
                </div>
                <div class="yearLayout">
                    <div class="col">
                        <asp:Calendar ID="Calendar01" runat="server" CssClass="calendar-year" DayNameFormat="Shortest"
                            BorderWidth="0px" CellPadding="-1" CellSpacing="-1" NextMonthText="" 
                            PrevMonthText="" TitleFormat="Month" 
                            VisibleDate="2010-01-01" ondayrender="Calendar_DayRender">
                            <DayHeaderStyle CssClass="headtitle" />
                            <DayStyle CssClass="Nholiday_bg" />
                            <TitleStyle CssClass="head" />
                            <TodayDayStyle CssClass="today" />
                            <WeekendDayStyle CssClass="holiday_bg" />
                        </asp:Calendar>
                    </div>
                    <div class="col">
                        <asp:Calendar ID="Calendar02" runat="server" CssClass="calendar-year" DayNameFormat="Shortest"
                            BorderWidth="0px" CellPadding="-1" CellSpacing="-1" NextMonthText="" 
                            PrevMonthText="" TitleFormat="Month" 
                            VisibleDate="2010-02-01" ondayrender="Calendar_DayRender">
                            <DayHeaderStyle CssClass="headtitle" />
                            <DayStyle CssClass="Nholiday_bg" />
                            <TitleStyle CssClass="head" />
                            <TodayDayStyle CssClass="today" />
                            <WeekendDayStyle CssClass="holiday_bg" />
                        </asp:Calendar>
                    </div>
                    <div class="col">
                        <asp:Calendar ID="Calendar03" runat="server" CssClass="calendar-year" DayNameFormat="Shortest"
                            BorderWidth="0px" CellPadding="-1" CellSpacing="-1" NextMonthText="" 
                            PrevMonthText="" TitleFormat="Month" 
                            VisibleDate="2010-03-01" ondayrender="Calendar_DayRender">
                            <DayHeaderStyle CssClass="headtitle" />
                            <DayStyle CssClass="Nholiday_bg" />
                            <TitleStyle CssClass="head" />
                            <TodayDayStyle CssClass="today" />
                            <WeekendDayStyle CssClass="holiday_bg" />
                        </asp:Calendar>
                    </div>
                    <div class="col">
                        <asp:Calendar ID="Calendar04" runat="server" CssClass="calendar-year" DayNameFormat="Shortest"
                            BorderWidth="0px" CellPadding="-1" CellSpacing="-1" NextMonthText="" 
                            PrevMonthText="" TitleFormat="Month" 
                            VisibleDate="2010-04-01" ondayrender="Calendar_DayRender">
                            <DayHeaderStyle CssClass="headtitle" />
                            <DayStyle CssClass="Nholiday_bg" />
                            <TitleStyle CssClass="head" />
                            <TodayDayStyle CssClass="today" />
                            <WeekendDayStyle CssClass="holiday_bg" />
                        </asp:Calendar>
                    </div>
                    <div class="col">
                        <asp:Calendar ID="Calendar05" runat="server" CssClass="calendar-year" DayNameFormat="Shortest"
                            BorderWidth="0px" CellPadding="-1" CellSpacing="-1" NextMonthText="" 
                            PrevMonthText="" TitleFormat="Month" 
                            VisibleDate="2010-05-01" ondayrender="Calendar_DayRender">
                            <DayHeaderStyle CssClass="headtitle" />
                            <DayStyle CssClass="Nholiday_bg" />
                            <TitleStyle CssClass="head" />
                            <TodayDayStyle CssClass="today" />
                            <WeekendDayStyle CssClass="holiday_bg" />
                        </asp:Calendar>
                    </div>
                    <div class="col">
                        <asp:Calendar ID="Calendar06" runat="server" CssClass="calendar-year" DayNameFormat="Shortest"
                            BorderWidth="0px" CellPadding="-1" CellSpacing="-1" NextMonthText="" 
                            PrevMonthText="" TitleFormat="Month" 
                            VisibleDate="2010-06-01" ondayrender="Calendar_DayRender">
                            <DayHeaderStyle CssClass="headtitle" />
                            <DayStyle CssClass="Nholiday_bg" />
                            <TitleStyle CssClass="head" />
                            <TodayDayStyle CssClass="today" />
                            <WeekendDayStyle CssClass="holiday_bg" />
                        </asp:Calendar>
                    </div>
                    <div class="col">
                        <asp:Calendar ID="Calendar07" runat="server" CssClass="calendar-year" DayNameFormat="Shortest"
                            BorderWidth="0px" CellPadding="-1" CellSpacing="-1" NextMonthText="" 
                            PrevMonthText="" TitleFormat="Month" 
                            VisibleDate="2010-07-01" ondayrender="Calendar_DayRender">
                            <DayHeaderStyle CssClass="headtitle" />
                            <DayStyle CssClass="Nholiday_bg" />
                            <TitleStyle CssClass="head" />
                            <TodayDayStyle CssClass="today" />
                            <WeekendDayStyle CssClass="holiday_bg" />
                        </asp:Calendar>
                    </div>
                    <div class="col">
                        <asp:Calendar ID="Calendar08" runat="server" CssClass="calendar-year" DayNameFormat="Shortest"
                            BorderWidth="0px" CellPadding="-1" CellSpacing="-1" NextMonthText="" 
                            PrevMonthText="" TitleFormat="Month" 
                            VisibleDate="2010-08-01" ondayrender="Calendar_DayRender">
                            <DayHeaderStyle CssClass="headtitle" />
                            <DayStyle CssClass="Nholiday_bg" />
                            <TitleStyle CssClass="head" />
                            <TodayDayStyle CssClass="today" />
                            <WeekendDayStyle CssClass="holiday_bg" />
                        </asp:Calendar>
                    </div>
                    <div class="col">
                        <asp:Calendar ID="Calendar09" runat="server" CssClass="calendar-year" DayNameFormat="Shortest"
                            BorderWidth="0px" CellPadding="-1" CellSpacing="-1" NextMonthText="" 
                            PrevMonthText="" TitleFormat="Month" 
                            VisibleDate="2010-09-01" ondayrender="Calendar_DayRender">
                            <DayHeaderStyle CssClass="headtitle" />
                            <DayStyle CssClass="Nholiday_bg" />
                            <TitleStyle CssClass="head" />
                            <TodayDayStyle CssClass="today" />
                            <WeekendDayStyle CssClass="holiday_bg" />
                        </asp:Calendar>
                    </div>
                    <div class="col">
                        <asp:Calendar ID="Calendar10" runat="server" CssClass="calendar-year" DayNameFormat="Shortest"
                            BorderWidth="0px" CellPadding="-1" CellSpacing="-1" NextMonthText="" 
                            PrevMonthText="" TitleFormat="Month" 
                            VisibleDate="2010-10-01" ondayrender="Calendar_DayRender">
                            <DayHeaderStyle CssClass="headtitle" />
                            <DayStyle CssClass="Nholiday_bg" />
                            <TitleStyle CssClass="head" />
                            <TodayDayStyle CssClass="today" />
                            <WeekendDayStyle CssClass="holiday_bg" />
                        </asp:Calendar>
                    </div>
                    <div class="col">
                        <asp:Calendar ID="Calendar11" runat="server" CssClass="calendar-year" DayNameFormat="Shortest"
                            BorderWidth="0px" CellPadding="-1" CellSpacing="-1" NextMonthText="" 
                            PrevMonthText="" TitleFormat="Month" 
                            VisibleDate="2010-11-01" ondayrender="Calendar_DayRender">
                            <DayHeaderStyle CssClass="headtitle" />
                            <DayStyle CssClass="Nholiday_bg" />
                            <TitleStyle CssClass="head" />
                            <TodayDayStyle CssClass="today" />
                            <WeekendDayStyle CssClass="holiday_bg" />
                        </asp:Calendar>
                    </div>
                    <div class="col">
                        <asp:Calendar ID="Calendar12" runat="server" CssClass="calendar-year" DayNameFormat="Shortest"
                            BorderWidth="0px" CellPadding="-1" CellSpacing="-1" NextMonthText="" 
                            PrevMonthText="" TitleFormat="Month" 
                            VisibleDate="2010-12-01" ondayrender="Calendar_DayRender">
                            <DayHeaderStyle CssClass="headtitle" />
                            <DayStyle CssClass="Nholiday_bg" />
                            <TitleStyle CssClass="head" />
                            <TodayDayStyle CssClass="today" />
                            <WeekendDayStyle CssClass="holiday_bg" />
                        </asp:Calendar>
                    </div>
                </div>
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
