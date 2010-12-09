<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="100301-2.aspx.cs" Inherits="_10_100300_100301_2" EnableEventValidation="false" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxtoolkit" %>
<%@ Register Src="../../lib/Navigator.ascx" TagName="Navigator" TagPrefix="uc1" %>
<%@ Register Src="../../lib/calendar.ascx" TagName="calendar" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<ajaxtoolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </ajaxtoolkit:ToolkitScriptManager>
    <uc1:Navigator ID="Navigator1" runat="server" SysFuncNo="100301" />
    <div class="PersonalCalendarLayout">
        <div class="left">
            <div class="tabHeader">
                <div class="tabHead">
                    <ul>
                        <li>
                            <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="100301.aspx">日</asp:HyperLink></li>
                        <li>
                            <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="100301-1.aspx">週</asp:HyperLink></li>
                        <li>
                            <asp:HyperLink ID="current" runat="server" NavigateUrl="100301-2.aspx">月</asp:HyperLink></li>
                        <li>
                            <asp:HyperLink ID="HyperLink4" runat="server" NavigateUrl="100301-3.aspx">年</asp:HyperLink></li>
                        <li>
                            <asp:HyperLink ID="HyperLink5" runat="server" NavigateUrl="100301-4.aspx">列表</asp:HyperLink></li>
                    </ul>
                </div>
            </div>
            <div class="block-1">
                <div class="header">
                    <div class="h1">
                        <asp:HyperLink ID="hl_Pre" runat="server" NavigateUrl="?todays=2009-01-01"><span>箭頭</span></asp:HyperLink></div>
                    <div class="h2">
                        <asp:Label ID="lab_CYM" runat="server" CssClass="name">99年</asp:Label></div>
                    <div class="h3">
                        <asp:HyperLink ID="hl_Nxt" runat="server" NavigateUrl="?todays=2011-01-01"><span>箭頭</span></asp:HyperLink></div>
                </div>
                <div class="block-0">
                    <asp:Table ID="Table1" runat="server" CssClass="calendar-smonth">
                        <asp:TableRow ID="row0" runat="server">
                            <asp:TableCell runat="server">1月</asp:TableCell>
                            <asp:TableCell runat="server">2月</asp:TableCell>
                            <asp:TableCell runat="server">3月</asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow ID="row1" runat="server">
                            <asp:TableCell runat="server">4月</asp:TableCell>
                            <asp:TableCell runat="server">5月</asp:TableCell>
                            <asp:TableCell runat="server">6月</asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow ID="row2" runat="server">
                            <asp:TableCell runat="server">7月</asp:TableCell>
                            <asp:TableCell runat="server">8月</asp:TableCell>
                            <asp:TableCell runat="server">9月</asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow ID="row3" runat="server">
                            <asp:TableCell runat="server">10月</asp:TableCell>
                            <asp:TableCell runat="server">11月</asp:TableCell>
                            <asp:TableCell runat="server">12月</asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                </div>
            </div>
            <div class="center">
                <span class="a-letter-2">
                    <asp:Label ID="lab_today" runat="server">今天是 99-10-20 星期三</asp:Label></span>
            </div>
            <asp:Panel ID="Panel1" runat="server">
            <div class="block-2">
                <div class="border-bottom-block">
                    <div class="header">
                        <div class="h1">
                        </div>
                        <div class="h2 a-letter-1">
                            新增行事曆</div>
                    </div>
                    <div class="headerW">
                        <div class="h1 a-letter-1">
                            事件標題</div>
                        <div class="h2">
                            <asp:TextBox ID="txt_title" runat="server" Columns="20"></asp:TextBox></div>
                    </div>
                    <div class="headerW">
                        <div class="h1 a-letter-1">
                            行程日期</div>
                        <div class="h2">
                            <uc2:calendar ID="cl_date" runat="server" _Show="False" />
                        </div>
                    </div>
                    <div class="headerW">
                        <div class="h1 a-letter-1">
                            事件時間</div>
                        <div class="h2">
                            <asp:DropDownList ID="ddl_stime" runat="server">
                            </asp:DropDownList>
                            <ajaxtoolkit:CascadingDropDown ID="ddl_stime_CascadingDropDown" runat="server" 
                            Category="stime" LoadingText="讀取中..." PromptText="請選擇" PromptValue="0" 
                                ServiceMethod="GetTimes" ServicePath="../../WebService/calendar.asmx" UseContextKey="True"
                                TargetControlID="ddl_stime">
                            </ajaxtoolkit:CascadingDropDown>
                        </div>
                        <div class="hd a-letter-1">
                            &nbsp;</div>
                        <div class="h2">
                            <asp:DropDownList ID="ddl_etime" runat="server">
                            </asp:DropDownList>
                            <ajaxtoolkit:CascadingDropDown ID="ddl_etime_CascadingDropDown" runat="server" 
                            Category="etime" LoadingText="讀取中..." PromptText="請選擇" PromptValue="0" 
                                ServiceMethod="GetTimes" ServicePath="../../WebService/calendar.asmx" UseContextKey="True"
                                TargetControlID="ddl_etime">
                            </ajaxtoolkit:CascadingDropDown>
                        </div>
                    </div>
                    <div class="headerW">
                        <div class="h1">
                        </div>
                        <div class="h3">
                        </div>
                        <div class="h3">
                            <asp:Button ID="btn_submit" runat="server" CssClass="b-input" Text="確定" OnClick="btn_submit_Click" /></div>
                    </div>
                </div>
            </div>
            </asp:Panel>
            <div class="block-2">
                <div class="border-bottom-block">
                    <div class="header">
                        <div class="h1">
                        </div>
                        <div class="h2 a-letter-1">
                            可查看之他人行事曆</div>
                    </div>
                    <div class="headerW">
                        <div class="h1 a-letter-1">
                            部門</div>
                        <div class="h2">
                            <asp:DropDownList ID="ddl_QryDepart" runat="server">
                            </asp:DropDownList>
                            <ajaxtoolkit:cascadingdropdown ID="ddl_QryDepart_CascadingDropDown" runat="server" 
                                Category="departs" LoadingText="讀取中..." PromptText="請選擇" PromptValue="0" 
                                ServiceMethod="GetViewDepart" TargetControlID="ddl_QryDepart" 
                                ServicePath="../../WebService/calendar.asmx" UseContextKey="True">
                            </ajaxtoolkit:cascadingdropdown>
                        </div>
                        <div class="h3">
                        </div>
                    </div>
                    <div class="headerW">
                        <div class="h1 a-letter-1">
                            姓名</div>
                        <div class="h2">
                            <asp:DropDownList ID="ddl_QryPeople" runat="server">
                            </asp:DropDownList>
                            <ajaxtoolkit:cascadingdropdown ID="ddl_QryPeople_CascadingDropDown" runat="server" 
                                Category="people" LoadingText="讀取中..." ParentControlID="ddl_QryDepart" 
                                PromptText="請選擇" PromptValue="0" ServiceMethod="GetViewPeople" 
                                TargetControlID="ddl_QryPeople" 
                                ServicePath="../../WebService/calendar.asmx" UseContextKey="True">
                            </ajaxtoolkit:cascadingdropdown>
                        </div>
                        <div class="h3">
                            <asp:Button ID="btn_QrySubmit" runat="server" CssClass="b-input" Text="搜尋" OnClick="btn_QrySubmit_Click" /></div>
                    </div>
                </div>
            </div>
            <div class="block-2">
                <div class="border-bottom-block">
                    <div class="header">
                        <div class="h1">
                        </div>
                        <div class="h2 a-letter-1">
                            可設定之他人行事曆</div>
                    </div>
                    <div class="headerW">
                        <div class="h1 a-letter-1">
                            姓名</div>
                        <div class="h2">
                            <asp:DropDownList ID="ddl_c01" runat="server">
                            </asp:DropDownList>
                            <ajaxtoolkit:cascadingdropdown ID="ddl_c01_CascadingDropDown" runat="server" 
                            Category="c01" LoadingText="讀取中..." PromptText="請選擇" PromptValue="0" 
                                ServiceMethod="GetC01" ServicePath="../../WebService/calendar.asmx" UseContextKey="True"
                                TargetControlID="ddl_c01">
                            </ajaxtoolkit:cascadingdropdown>
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
                            <asp:Label ID="lab_isAdd" runat="server" Visible="False"></asp:Label>
                        </div>
                        <div class="function_print">
                          <ul>
                             <li><asp:HyperLink ID="hl_print" runat="server" CssClass="b-print">列印</asp:HyperLink></li>
                             <li><asp:HyperLink ID="hl_back" runat="server" CssClass="b-back">返回使用者</asp:HyperLink></li>
                           </ul>
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
