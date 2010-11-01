<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="100301-1.aspx.cs" Inherits="_10_100300_100301_1" %>

<%@ Register src="../../lib/Navigator.ascx" tagname="Navigator" tagprefix="uc1" %>

<%@ Register src="../../lib/calendar.ascx" tagname="calendar" tagprefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <uc1:Navigator ID="Navigator1" runat="server" SysFuncNo="100301" />
    <div class="PersonalCalendarLayout">
        <div class="left">
            <div class="tabHeader">
                <div class="tabHead">
                    <ul>
                        <li><asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="100301.aspx">日</asp:HyperLink></li>
                        <li><asp:HyperLink ID="current" runat="server" NavigateUrl="100301-1.aspx">週</asp:HyperLink></li>
                        <li><asp:HyperLink ID="HyperLink3" runat="server" NavigateUrl="100301-2.aspx">月</asp:HyperLink></li>
                        <li><asp:HyperLink ID="HyperLink4" runat="server" NavigateUrl="100301-3.aspx">年</asp:HyperLink></li>
                        <li><asp:HyperLink ID="HyperLink5" runat="server" NavigateUrl="100301-4.aspx">列表</asp:HyperLink></li>
                    </ul>
                </div>
            </div>
            <div class="block-1">
                <div class="header">
                    <div class="h1">
                        <asp:HyperLink ID="hl_Pre" runat="server" NavigateUrl="?todays=2010-08-01"><span>箭頭</span></asp:HyperLink></div>
                    <div class="h2">
                        <asp:Label ID="lab_CYM" runat="server" CssClass="name">2010年09月</asp:Label></div>
                    <div class="h3">
                        <asp:HyperLink ID="hl_Nxt" runat="server" NavigateUrl="?todays=2010-08-01"><span>箭頭</span></asp:HyperLink></div>
                </div>
                <div class="block-0">
                    <asp:Calendar ID="Calendar1" runat="server" CssClass="calendar" DayNameFormat="Shortest"
                        BorderWidth="0px" CellPadding="-1" CellSpacing="-1" NextMonthText=""
                        PrevMonthText="" OnDayRender="Calendar1_DayRender"
                        ShowTitle="False" onvisiblemonthchanged="Calendar1_VisibleMonthChanged">
                        <DayHeaderStyle CssClass="headtitle" />
                        <DayStyle CssClass="Nholiday_bg" />
                        <TodayDayStyle CssClass="today" />
                        <WeekendDayStyle CssClass="holiday_bg" />
                    </asp:Calendar>
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
                        <div class="h2">
                            <uc2:calendar ID="cl_date" runat="server" _Show="False" />
                        </div>
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
                            <asp:DropDownList ID="ddl_QryDepart" runat="server" AutoPostBack="True" 
                                onselectedindexchanged="ddl_QryDepart_SelectedIndexChanged">
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
                        <div class="h3"><asp:Button ID="btn_QrySubmit" runat="server" CssClass="b-input" Text="搜尋" onclick="btn_QrySubmit_Click" /></div>
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
                        <div class="h3"><asp:Button ID="btn_SetSubmit0" runat="server" CssClass="b-input" Text="搜尋" onclick="btn_SetSubmit0_Click" /></div>
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
                            <asp:Button ID="btn_print" runat="server" CssClass="b-input" Text="列印" 
                                onclick="btn_print_Click" />&nbsp;
                            <asp:Button ID="btn_back" runat="server" CssClass="b-input" Text="返回使用者" 
                                onclick="btn_back_Click" />
                        </div>
                    </div>
                    <div class="h3">
                    </div>
                </div>
                <table class="big-calendar-time">
                    <tbody>
                        <tr>
                            <td class="title_time_bg"><span class="title_time">星期</span></td>
                            <td class="title_schedule_bg"><span class="title_time">行程</span></td>
                        </tr>
                        <tr>
                            <td class="title_time_bg"><span class="title_time">星期日</span></td>
                            <td class="row_holiday_bg">
                                <asp:HyperLink ID="hl_0" runat="server" CssClass="thickbox">[hl_0]</asp:HyperLink>
                                <br />
                                <asp:Label ID="lab_0" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="title_time_bg"><span class="title_time">星期一</span></td>
                            <td class="row_Nholiday_bg">
                                <asp:HyperLink ID="hl_1" runat="server" CssClass="thickbox">[hl_1]</asp:HyperLink>
                                <br />
                                <asp:Label ID="lab_1" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="title_time_bg"><span class="title_time">星期二</span></td>
                            <td class="row_Nholiday_bg">
                                <asp:HyperLink ID="hl_2" runat="server" CssClass="thickbox">[hl_2]</asp:HyperLink><br />
                                <asp:Label ID="lab_2" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="title_time_bg"><span class="title_time">星期三</span></td>
                            <td class="row_Nholiday_bg">
                                <asp:HyperLink ID="hl_3" runat="server" CssClass="thickbox">[hl_3]</asp:HyperLink><br />
                                <asp:Label ID="lab_3" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="title_time_bg"><span class="title_time">星期四</span></td>
                            <td class="row_Nholiday_bg">
                                <asp:HyperLink ID="hl_4" runat="server" CssClass="thickbox">[hl_4]</asp:HyperLink><br />
                                <asp:Label ID="lab_4" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="title_time_bg"><span class="title_time">星期五</span></td>
                            <td class="row_Nholiday_bg">
                                <asp:HyperLink ID="hl_5" runat="server" CssClass="thickbox">[hl_5]</asp:HyperLink><br />
                                <asp:Label ID="lab_5" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="title_time_bg"><span class="title_time">星期六</span></td>
                            <td class="row_holiday_bg">
                                <asp:HyperLink ID="hl_6" runat="server" CssClass="thickbox">[hl_6]</asp:HyperLink><br />
                                <asp:Label ID="lab_6" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </tbody>
                </table>
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
