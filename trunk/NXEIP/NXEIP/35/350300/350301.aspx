<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="350301.aspx.cs" Inherits="_35_350300_350301" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../../lib/tree/jQueryDepartTree.ascx" TagName="jQueryDepartTree"
    TagPrefix="uc1" %>
<%@ Register Src="../../lib/tree/jQueryPeopleTree.ascx" TagName="jQueryPeopleTree"
    TagPrefix="uc2" %>
<%@ Register Src="../../lib/Navigator.ascx" TagName="Navigator" TagPrefix="uc3" %>
<%@ Register Src="../../lib/calendar.ascx" TagName="calendar" TagPrefix="uc4" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <uc3:Navigator ID="Navigator1" runat="server" SysFuncNo="350301" />
    <div class="tableDiv">
        <div class="header">
            <div class="h1">
            </div>
            <div class="h2">
            </div>
            <div class="h3">
            </div>
        </div>
        <table>
            <tr>
                <th style="text-align: right; width: 15%;">
                    起始時間
                </th>
                <td>
                    <table>
                        <tr>
                            <td>
                                起：<uc4:calendar ID="calendar1" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                迄：<uc4:calendar ID="calendar2" runat="server" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <th style="text-align: right;">
                    功能項目
                </th>
                <td>
                    主功能&nbsp;
                    <asp:DropDownList ID="DropDownList2" runat="server">
                    </asp:DropDownList>
                    子功能&nbsp;<asp:DropDownList ID="DropDownList3" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <th style="text-align: right;">
                    功能狀態
                </th>
                <td>
                    <asp:DropDownList ID="DropDownList1" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <th style="text-align: right;">
                    請選擇欲查詢人員
                </th>
                <td>
                    <table border="0" cellpadding="0" cellspacing="0">
                        <tr>
                            <td style="width: 15%">
                                <asp:RadioButton ID="rb_all" runat="server" Text="全部" />
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:RadioButton ID="rb_workid" runat="server" Text="人事編號：" />
                            </td>
                            <td>
                                <asp:TextBox ID="tbox_workid" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:RadioButton ID="rb_account" runat="server" Text="員工帳號：" />
                            </td>
                            <td>
                                <asp:TextBox ID="tbox_account" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:RadioButton ID="rb_people" runat="server" Text="請選擇人員" />
                            </td>
                            <td>
                                <uc2:jQueryPeopleTree ID="jQueryPeopleTree1" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:RadioButton ID="rb_depart" runat="server" Text="請選擇單位" />
                            </td>
                            <td>
                                <uc1:jQueryDepartTree ID="jQueryDepartTree1" runat="server" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <div class="footer">
            <div class="f1">
            </div>
            <div class="f2">
            </div>
            <div class="f3">
            </div>
        </div>
        <div class="pager">
            <asp:DataPager ID="DataPager1" runat="server" PagedControlID="GridView1" PageSize="10">
                <Fields>
                    <asp:NextPreviousPagerField ShowNextPageButton="False" />
                    <asp:NumericPagerField />
                    <asp:NextPreviousPagerField ShowPreviousPageButton="False" />
                </Fields>
            </asp:DataPager>
        </div>
    </div>
</asp:Content>
