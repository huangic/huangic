<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="300303-7.aspx.cs" Inherits="_30_300300_300303_7" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register src="../../lib/Navigator.ascx" tagname="Navigator" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <asp:HiddenField ID="hidd_no" runat="server" />
    <uc1:Navigator ID="Navigator1" runat="server" SysFuncNo="300303" />
    <div class="tableDiv">
        <div class="header">
            <div class="h1">
            </div>
            <div class="h2">
                <div class="name">
                    <asp:Label ID="lab_titile" runat="server"></asp:Label>
                </div>
            </div>
            <div class="h3">
            </div>
        </div>
        <table>
            <tr>
                <td align="center">
                    <asp:CheckBox ID="cbox_dep" Text="部門" runat="server" />
                </td>
                <td align="center">
                    <asp:CheckBox ID="cbox_profess" Text="職稱" runat="server" />
                </td>
                <td align="center">
                    <asp:CheckBox ID="cbox_name" Text="姓名" runat="server" />
                </td>
                <td align="center">
                    <asp:CheckBox ID="cbox_idcard" Text="身份證字號" runat="server" />
                </td>
                <td align="center">
                    <asp:CheckBox ID="cbox_tel" Text="電話" runat="server" />
                </td>
            </tr>
        </table>
        <br />
        <div class="bottom">
            <asp:Button ID="Button1" runat="server" CssClass="b-input" Text="列印報名表" 
                onclick="Button1_Click" />
            &nbsp;
            <asp:Button ID="Button2" runat="server" CssClass="b-input" Text="下載報名表" 
                onclick="Button2_Click" />
            &nbsp;
            <asp:Button ID="btn_cancel" runat="server" CssClass="a-input" Text="回管理課程" OnClick="btn_cancel_Click" />
            &nbsp;
            <asp:Button ID="btn_cancel2" runat="server" CssClass="a-input" Text="回課程檢視" OnClick="btn_cancel2_Click" />
        </div>
    </div>
</asp:Content>

