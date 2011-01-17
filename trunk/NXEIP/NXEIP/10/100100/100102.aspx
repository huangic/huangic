<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="100102.aspx.cs" Inherits="_10_100100_100102" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<%@ Register src="../../lib/Navigator.ascx" tagname="Navigator" tagprefix="uc1" %>

<%@ Register src="../../lib/calendar.ascx" tagname="calendar" tagprefix="uc2" %>

<%@ Register src="../../lib/FileUpload.ascx" tagname="FileUpload" tagprefix="uc3" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <uc1:Navigator ID="Navigator1" runat="server" SysFuncNo="100102" />
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
                <th style="width:15%">
                身分證字號
                </th>
                <td style="width:35%">
                    <asp:Label ID="lab_idcard" runat="server" ></asp:Label>
                </td>
                <th style="width:15%">
                姓名
                </th>
                <td>
                    <asp:Label ID="lab_name" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <th style="width:15%">
                員工帳號
                </th>
                <td>
                    <asp:Label ID="lab_account" runat="server" ></asp:Label>
                </td>
                <th style="width:15%">
                人事編號
                </th>
                <td>
                    <asp:Label ID="lab_workid" runat="server" ></asp:Label>
                </td>
            </tr>
            <tr>
                <th style="width:15%">
                服務單位
                </th>
                <td>
                    <asp:Label ID="lab_depart" runat="server" ></asp:Label>
                </td>
                <th style="width:15%">
                個人照片
                </th>
                <td>
                    <uc3:FileUpload ID="FileUpload1" runat="server" />
                </td>
            </tr>
            <tr>
                <th style="width:15%">
                職稱
                </th>
                <td>
                    <asp:Label ID="lab_proname" runat="server" ></asp:Label>
                </td>
                <th style="width:15%">
                人員類別
                </th>
                <td>
                   <asp:Label ID="lab_ptyname" runat="server" ></asp:Label>
                </td>
            </tr>
            <tr>
                <th style="width:15%">
                到職日期
                </th>
                <td>
                    <uc2:calendar ID="calendar1" runat="server" />
                </td>
                <th style="width:15%">
                生日
                </th>
                <td>
                    <uc2:calendar ID="calendar2" runat="server" />
                </td>
            </tr>
            <tr>
                <th style="width:15%">
                連絡地址
                </th>
                <td colspan="3">
                    <asp:TextBox ID="tbox_addr" runat="server" Width="300px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th style="width:15%">
                連絡電話
                </th>
                <td>
                    <asp:TextBox ID="tbox_tel" runat="server"></asp:TextBox>
                </td>
                <th style="width:15%">
                行動電話
                </th>
                <td>
                  <asp:TextBox ID="tbox_phone" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th style="width:15%">
                電子郵件
                </th>
                <td>
                    <asp:TextBox ID="tbox_mail" runat="server" Width="250px"></asp:TextBox>
                </td>
                <th style="width:15%">
                辦公室電話
                </th>
                <td>
                    <asp:TextBox ID="tbox_offtel" runat="server"></asp:TextBox>
                    &nbsp;分機：<asp:TextBox ID="tbox_offext" runat="server" Width="75px"></asp:TextBox>
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
        <div class="bottom">
            <asp:Button ID="Button1" runat="server" Text="確定" CssClass="b-input" 
                onclick="Button1_Click" />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="Button2" runat="server" Text="取消" CssClass="a-input" 
                onclick="Button2_Click" />
        </div>
    </div>
</asp:Content>

