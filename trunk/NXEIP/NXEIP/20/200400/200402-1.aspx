﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="200402-1.aspx.cs" Inherits="_20_200400_200402_1" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../../lib/Navigator.ascx" TagName="Navigator" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <asp:HiddenField ID="hidd_no" runat="server" />
    <asp:HiddenField ID="hidd_check" runat="server" />
<uc1:Navigator ID="Navigator1" runat="server" SysFuncNo="200402" />
    <div class="tableDiv">
        <div class="header">
            <div class="h1">
            </div>
            <div class="h2">
            </div>
            <div class="h3">
            </div>
        </div>
        <table id="table1">
            <tr>
                <th>
                    學習機構
                </th>
                <td>
                    <asp:Label ID="lab_mechani" runat="server"></asp:Label>
                </td>
                <th>
                    課程代碼
                </th>
                <td>
                    <asp:Label ID="lab_code" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <th>
                    課程類別
                </th>
                <td>
                    <asp:Label ID="lab_typ_name" runat="server"></asp:Label>
                </td>
                <th>
                    課程名稱(期別)
                </th>
                <td>
                    <asp:Label ID="lab_name_flag" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <th>
                    課程簡介
                </th>
                <td>
                    <asp:Label ID="lab_memo" runat="server"></asp:Label>
                </td>
                <th>
                    資格條件說明
                </th>
                <td>
                    <asp:Label ID="lab_limit" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <th>
                    上課地點
                </th>
                <td>
                    <asp:Label ID="lab_e01_name" runat="server"></asp:Label>
                </td>
                <th>
                    招收名額上限
                </th>
                <td>
                    <asp:Label ID="lab_people" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <th>
                    認證時數
                </th>
                <td>
                    <asp:Label ID="lab_hour" runat="server"></asp:Label>
                </td>
                <th>
                    報名審核狀況
                </th>
                <td>
                    <asp:Label ID="lab_check" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <th>
                    講師姓名
                </th>
                <td>
                    <asp:Label ID="lab_teacher" runat="server"></asp:Label>
                </td>
                <th>
                    上線開放日期
                </th>
                <td>
                    <asp:Label ID="lab_opendate" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <th>
                    報名起迄日期
                </th>
                <td colspan="3">
                    <asp:Label ID="lab_signdate" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <th>
                    上課起迄時間
                </th>
                <td colspan="3">
                    <asp:Label ID="lab_date" runat="server"></asp:Label>
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
            <asp:Button ID="Button1" runat="server" CssClass="b-input" Text="報名課程" 
                onclick="Button1_Click"/>
            &nbsp;&nbsp;
            <asp:Button ID="btn_cancel" runat="server" CssClass="a-input" Text="回管理課程" OnClick="btn_cancel_Click" />
        </div>
    </div>
</asp:Content>

