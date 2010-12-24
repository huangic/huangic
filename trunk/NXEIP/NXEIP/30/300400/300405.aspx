<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="300405.aspx.cs" Inherits="_30_300400_300405" %>
<%@ Register Src="../../lib/Navigator.ascx" TagName="Navigator" TagPrefix="uc1" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxtoolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <ajaxtoolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" CombineScripts="False">
    </ajaxtoolkit:ToolkitScriptManager>
    <uc1:Navigator ID="Navigator1" runat="server" SysFuncNo="300405" />
    <div class="PersonalCalendarLayout">
        <div class="tabHeader-bigmonth1">
            <div class="select-s">
                <div class="b1"><li class="p1"><span class="a-letter-ss">所在地</span></li></div>
                <div class="b2">
                    <asp:DropDownList ID="ddl_spot" runat="server" CssClass="select4" AutoPostBack="True"
                        OnSelectedIndexChanged="ddl_spot_SelectedIndexChanged">
                    </asp:DropDownList>
                </div>
                <div class="b3">
                    <li class="p1"><span class="a-letter-ss">場地</span></li>
                </div>
                <div class="b2">
                    <asp:DropDownList ID="ddl_rooms" runat="server" CssClass="select4" 
                        AutoPostBack="True" onselectedindexchanged="ddl_rooms_SelectedIndexChanged">
                    </asp:DropDownList>
                </div>
                <div class="b4">
                    <div class="tabHead_big">
                        <ul>
                            <li class="currentTab"><a id="ctl00_ContentPlaceHolder1_current" href="#">月 </a>
                            </li>
                            <li>
                                <asp:HyperLink ID="hl_weeks" runat="server" NavigateUrl="300405-1.aspx">周</asp:HyperLink>
                            </li>
                        </ul>
                    </div>
                </div>
            </div>
            <div class="tabHeader-bigmonth">
                <div class="t1">
                </div>
                <div class="t2">
                    <div class="t21">
                        <asp:DropDownList ID="ddl_year" runat="server" AutoPostBack="True" 
                            onselectedindexchanged="ddl_YearOrMonth_SelectedIndexChanged">
                        </asp:DropDownList>
                        年<asp:DropDownList ID="ddl_month" runat="server" AutoPostBack="True" 
                            onselectedindexchanged="ddl_YearOrMonth_SelectedIndexChanged">
                            <asp:ListItem Value="01">01</asp:ListItem>
                            <asp:ListItem Value="02">02</asp:ListItem>
                            <asp:ListItem Value="03">03</asp:ListItem>
                            <asp:ListItem Value="04">04</asp:ListItem>
                            <asp:ListItem Value="05">05</asp:ListItem>
                            <asp:ListItem Value="06">06</asp:ListItem>
                            <asp:ListItem Value="07">07</asp:ListItem>
                            <asp:ListItem Value="08">08</asp:ListItem>
                            <asp:ListItem Value="09">09</asp:ListItem>
                            <asp:ListItem Value="10">10</asp:ListItem>
                            <asp:ListItem Value="11">11</asp:ListItem>
                            <asp:ListItem Value="12">12</asp:ListItem>
                        </asp:DropDownList>
                        月
                    </div>
                    <div class="t22">
                        <ul>
                            <li><asp:LinkButton ID="lbtn_premonth" runat="server" onclick="ChangeMonth_Click" 
                                    CommandArgument="Previous">上個月</asp:LinkButton></li>
                            <li class="line">
                                <asp:LinkButton ID="lbtn_thismonth" runat="server" 
                                    onclick="ChangeMonth_Click" CommandArgument="This">本月</asp:LinkButton></li>
                            <li class="line">
                                <asp:LinkButton ID="lbtn_nextmonth" runat="server" 
                                    onclick="ChangeMonth_Click" CommandArgument="Next">下個月</asp:LinkButton></li>
                        </ul>
                    </div>
                    <div class="t23">
                        <li class="p1"><span class="a-letter-bm"><asp:HyperLink ID="hl_print" runat="server">列印</asp:HyperLink></span></li>
                    </div>
                </div>
                <div class="t3"></div>
            </div>
            <asp:Calendar ID="Calendar1" runat="server" CssClass="calendar-bigmonth" DayNameFormat="Shortest"
                BorderWidth="0px" CellPadding="-1" CellSpacing="-1" NextMonthText="" PrevMonthText=""
                OnDayRender="Calendar1_DayRender" ShowTitle="False">
                <DayHeaderStyle CssClass="head" />
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
            <div class="bottom">
                &nbsp;
            </div>
        </div>
    </div>
</asp:Content>