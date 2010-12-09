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
    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" 
        OldValuesParameterFormatString="original_{0}" SelectMethod="GetAll_2" 
        TypeName="NXEIP.DAO.SysDAO"></asp:ObjectDataSource>
    <asp:ObjectDataSource ID="ObjectDataSource2" runat="server" 
        OldValuesParameterFormatString="original_{0}" SelectMethod="GetUse_sfuParent" 
        TypeName="NXEIP.DAO.SysfuctionDAO">
        <SelectParameters>
            <asp:Parameter Name="sys_no" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="ObjectDataSource3" runat="server" 
        OldValuesParameterFormatString="original_{0}" SelectMethod="GetUse_sfu" 
        TypeName="NXEIP.DAO.SysfuctionDAO">
        <SelectParameters>
            <asp:Parameter Name="sfu_no" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
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
                        AutoPostBack="True" DataSourceID="ObjectDataSource1" 
                        DataTextField="sys_name" DataValueField="sys_no" 
                        onselectedindexchanged="ddl_sys_SelectedIndexChanged">
                        <asp:ListItem Value="0">全部</asp:ListItem>
                    </asp:DropDownList>
                    &nbsp;次功能&nbsp;
                    <asp:DropDownList ID="ddl_sfu_parent" runat="server" AutoPostBack="True" 
                        DataSourceID="ObjectDataSource2" DataTextField="sfu_name" 
                        DataValueField="sfu_no" 
                        onselectedindexchanged="ddl_sfu_parent_SelectedIndexChanged">
                    </asp:DropDownList>
                    &nbsp;子功能&nbsp;<asp:DropDownList ID="ddl_sfu_no" runat="server" 
                        DataSourceID="ObjectDataSource3" DataTextField="sfu_name" 
                        DataValueField="sfu_no">
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
                            <td style="width: 12%">
                                <asp:RadioButton ID="rb_all" runat="server" Text="全部" GroupName="G1" 
                                    Checked="True" />
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:RadioButton ID="rb_workid" runat="server" Text="人事編號" GroupName="G1" />
                            </td>
                            <td>
                                <asp:TextBox ID="tbox_workid" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:RadioButton ID="rb_account" runat="server" Text="員工帳號" GroupName="G1" />
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
                                <uc5:DepartTreeListBox ID="DepartTreeListBox_people" runat="server" 
                                    LeafType="People" />
                                </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:RadioButton ID="rb_depart" runat="server" Text="請選擇單位" GroupName="G1" />
                            </td>
                            <td>
                                <uc5:DepartTreeListBox ID="DepartTreeListBox_depart" runat="server" />
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
