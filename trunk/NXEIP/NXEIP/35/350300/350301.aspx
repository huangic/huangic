<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="350301.aspx.cs" Inherits="_35_350300_350301" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../../lib/Navigator.ascx" TagName="Navigator" TagPrefix="uc3" %>
<%@ Register Src="../../lib/calendar.ascx" TagName="calendar" TagPrefix="uc4" %>
<%@ Register src="../../lib/tree/DepartTreeListBox.ascx" tagname="DepartTreeListBox" tagprefix="uc5" %>
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
                    <asp:DropDownList ID="ddl_sys" runat="server" AppendDataBoundItems="True" 
                        AutoPostBack="True">
                        <asp:ListItem Value="0">請選擇</asp:ListItem>
                    </asp:DropDownList>
                    次功能&nbsp;
                    <asp:DropDownList ID="ddl_sfu_parent" runat="server" AutoPostBack="True">
                    </asp:DropDownList>
                    子功能&nbsp;<asp:DropDownList ID="ddl_sfu_no" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <th style="text-align: right;">
                    操作狀態
                </th>
                <td>
                    <asp:DropDownList ID="ddl_opt_status" runat="server">
                        <asp:ListItem Value="0">全部</asp:ListItem>
                        <asp:ListItem Value="1">新增</asp:ListItem>
                        <asp:ListItem Value="2">更新</asp:ListItem>
                        <asp:ListItem Value="3">刪除</asp:ListItem>
                        <asp:ListItem Value="4">查詢</asp:ListItem>
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
                                <asp:RadioButton ID="rb_all" runat="server" Text="全部" GroupName="G1" />
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:RadioButton ID="rb_workid" runat="server" Text="人事編號：" GroupName="G1" />
                            </td>
                            <td>
                                <asp:TextBox ID="tbox_workid" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:RadioButton ID="rb_account" runat="server" Text="員工帳號：" GroupName="G1" />
                            </td>
                            <td>
                                <asp:TextBox ID="tbox_account" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:RadioButton ID="rb_people" runat="server" Text="請選擇人員" GroupName="G1" />
                            </td>
                            <td>
                                <uc5:DepartTreeListBox ID="DepartTreeListBox1" runat="server" 
                                    LeafType="People" />
                                </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:RadioButton ID="rb_depart" runat="server" Text="請選擇單位" GroupName="G1" />
                            </td>
                            <td>
                                <uc5:DepartTreeListBox ID="DepartTreeListBox2" runat="server" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <div class="bottom">
            <asp:Button ID="btn_ok" runat="server" CssClass="b-input" Text="確定" OnClick="btn_ok_Click" />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="btn_cancel" runat="server" CssClass="a-input" Text="取消" OnClientClick="self.parent.tb_remove()"
                UseSubmitBehavior="false" onclick="btn_cancel_Click" />
        </div>
    </div>
</asp:Content>
