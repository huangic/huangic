<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="200102-3.aspx.cs" Inherits="_20_200100_200102_3" EnableEventValidation="false" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxtoolkit" %>
<%@ Register Assembly="MattBerseth.WebControls" Namespace="MattBerseth.WebControls"
    TagPrefix="cc1" %>
<%@ Register Src="../../lib/Navigator.ascx" TagName="Navigator" TagPrefix="uc1" %>
<%@ Register Src="../../lib/calendar.ascx" TagName="calendar" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <ajaxtoolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" CombineScripts="False">
    </ajaxtoolkit:ToolkitScriptManager>
    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" EnablePaging="True" SelectCountMethod="GetAllCount" SelectMethod="GetAll" 
        TypeName="NXEIP.DAO.C02DAO" OldValuesParameterFormatString="original_{0}">
        <SelectParameters>
            <asp:Parameter Name="sdate" Type="String" />
            <asp:Parameter Name="edate" Type="String" />
            <asp:Parameter Name="status" Type="String" />
            <asp:Parameter Name="loginuser" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <uc1:Navigator ID="Navigator1" runat="server" SysFuncNo="200102" />
    <div class="PersonalCalendarLayout">
        <div class="left">
            <div class="tabHeader">
                <div class="tabHead">
                    <ul>
                        <li><asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="200102.aspx">月</asp:HyperLink></li>
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
                    <asp:Calendar ID="Calendar1" runat="server" CssClass="calendar" DayNameFormat="Shortest"
                        BorderWidth="0px" CellPadding="-1" CellSpacing="-1" NextMonthText="" PrevMonthText=""
                        OnDayRender="Calendar1_DayRender" ShowTitle="False">
                        <DayHeaderStyle CssClass="headtitle" />
                        <DayStyle CssClass="Nholiday_bg" />
                        <TodayDayStyle CssClass="today" />
                        <WeekendDayStyle CssClass="holiday_bg" />
                    </asp:Calendar>
                </div>
            </div>
            <div class="center">
                <span class="a-letter-2">
                    <asp:Label ID="lab_today" runat="server">今天是 99-10-20 星期三</asp:Label></span>
            </div>
            <div class="block-2">
                <div class="border-bottom-block">
                    <div class="header">
                        <div class="h1">
                        </div>
                        <div class="h2 a-letter-1">
                            <asp:HyperLink ID="HyperLink4" runat="server" NavigateUrl="200102-3.aspx">我的預約記錄</asp:HyperLink>
                            <asp:Label ID="lab_people" runat="server" Visible="False"></asp:Label>
                            <asp:Label ID="lab_date" runat="server" Visible="False"></asp:Label>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="right">
            <div class="tableDiv">
                <table>
                    <tr>
                        <td>
                            欲查詢日期：起&nbsp;<uc2:calendar ID="calendar3" runat="server" _Show="False" />
                            迄&nbsp;
                            <uc2:calendar ID="calendar4" runat="server" />
                            &nbsp;&nbsp; 預約狀況：<asp:RadioButtonList ID="rbl_check" runat="server" RepeatDirection="Horizontal"
                                RepeatLayout="Flow">
                                <asp:ListItem Selected="True" Value="-1">全部皆有</asp:ListItem>
                                <asp:ListItem Value="0">預約中</asp:ListItem>
                                <asp:ListItem Value="1">核可</asp:ListItem>
                                <asp:ListItem Value="2">退回</asp:ListItem>
                            </asp:RadioButtonList>
                            <asp:Button ID="btn_submit" runat="server" CssClass="b-input" Text="查詢" OnClick="btn_submit_Click" />
                        </td>
                    </tr>
                </table>
                <div class="bottom">
                </div>
                <div class="header">
                    <div class="h1"></div>
                    <div class="h2">
                        <div class="name">
                            &nbsp;</div>
                    </div>
                    <div class="h3"></div>
                </div>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <cc1:GridView ID="GridView1" runat="server" DataSourceID="ObjectDataSource1" AllowPaging="True"
                            AutoGenerateColumns="False" CellPadding="3" CellSpacing="3" CssClass="tableData"
                            EmptyDataText="查無資料" OnRowDataBound="GridView1_RowDataBound" GridLines="None"
                            DataKeyNames="peo_uid,c02_no">
                            <Columns>
                                <asp:BoundField DataField="peo_name" HeaderText="被預約者" 
                                    SortExpression="peo_name" />
                                <asp:BoundField DataField="stet" HeaderText="預約時間" SortExpression="stet" />
                                <asp:BoundField DataField="c02_title" HeaderText="事件標題" 
                                    SortExpression="c02_title" />
                                <asp:BoundField DataField="c02_check" HeaderText="預約狀態" 
                                    SortExpression="c02_check" />
                                <asp:BoundField DataField="c02_reason" HeaderText="原因" 
                                    SortExpression="c02_reason" />
                            </Columns>
                            <HeaderStyle HorizontalAlign="Left" />
                        </cc1:GridView>
                        <div class="footer">
                            <div class="f1">
                            </div>
                            <div class="f2">
                            </div>
                            <div class="f3">
                            </div>
                        </div>
                        <div class="pager">
                            <asp:DataPager ID="DataPager1" runat="server" PagedControlID="GridView1" PageSize="25">
                                <Fields>
                                    <NXEIP:GooglePagerField />
                                </Fields>
                            </asp:DataPager>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
</asp:Content>
